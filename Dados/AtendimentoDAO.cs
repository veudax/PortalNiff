using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class AtendimentoDAO
    {
        IDataReader atendimentoReader;
        IDataReader anexosReader;

        public List<int> Datas()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<int> _datas = new List<int>();

            try
            {
                query.Append("Select Distinct ano ");
                query.Append("  From ( Select Distinct To_char(DataAbertura,'yyyy') ano ");
                query.Append("           From niff_chm_Atendimento ");
                query.Append("          Union ALL");
                query.Append("         Select To_char(sysDate,'yyyy') ano ");
                query.Append("           From dual )");
                query.Append("Order By ano Desc");

                Query executar = sessao.CreateQuery(query.ToString());

                atendimentoReader = executar.ExecuteQuery();

                using (atendimentoReader)
                {
                    while (atendimentoReader.Read())
                    {
                        _datas.Add(Convert.ToInt32(atendimentoReader["ano"].ToString()));
                    }

                    if (_datas.Where(w => w.Equals(DateTime.Now.Year)).Count() == 0)
                        _datas.Add(DateTime.Now.Year);
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _datas;
        }

        public List<Atendimento> Listar(int empresa, 
                                        Publicas.TelaPesquisaSAC telaQueChamou, 
                                        int departamento = 0,
                                        int ano = 0,
                                        string status = "A")
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Atendimento> _lista = new List<Atendimento>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select a.IdAtendimento ");
                query.Append("     , a.Codigo");
                query.Append("     , a.IdUsuario");
                query.Append("     , a.IdTpAtendimento");
                query.Append("     , a.ClienteAnonimo");
                query.Append("     , a.TextoAtendimento");
                query.Append("     , a.TextoResposta");
                query.Append("     , a.DataAbertura");
                query.Append("     , a.DataFinalizado");
                query.Append("     , a.DataResposta");
                query.Append("     , a.DataCancelado");
                query.Append("     , a.Status");
                query.Append("     , a.Retorno");
                query.Append("     , a.AguardaSatisfacaoCliente");
                query.Append("     , a.Retornou");
                query.Append("     , a.MotivoRetorno");
                query.Append("     , a.MotivoCancelamento");
                query.Append("     , a.Satisfacao");
                query.Append("     , a.MotivoSatisfacao");
                query.Append("     , a.codigolinha");
                query.Append("     , a.IdEmtu");
                query.Append("     , a.codfunc");
                query.Append("     , a.Nome");
                query.Append("     , a.RG");
                query.Append("     , a.CPF");
                query.Append("     , a.Endereco");
                query.Append("     , a.Cidade");
                query.Append("     , a.UF");
                query.Append("     , a.Email");
                query.Append("     , a.Telefone");
                query.Append("     , a.Celular");
                query.Append("     , a.IdUsuarioResponsavel");
                query.Append("     , a.Origem");
                query.Append("     , a.IdUsuarioFinaliza");
                query.Append("     , a.IdUsuarioRetorno");
                query.Append("     , a.IDUSUARIOCANCELA");
                query.Append("     , a.Situacao");
                query.Append("     , e.Codigo CodigoEMTU");
                query.Append("     , e.Descricao");
                query.Append("     , u.Nome UsuarioAbertura");
                query.Append("     , ur.Nome UsuarioResponsavel");
                query.Append("     , uf.Nome UsuarioFinalizador");
                query.Append("     , uc.Nome UsuarioCliente");
                query.Append("     , ut.Nome UsuarioRetorno");
                query.Append("     , p.RESPONDEREMDIASSAC");
                query.Append("     , p.SEMRETORNOEMDIASSAC");
                query.Append("     , p.EXIBIROSCANCELADOSNOSACDIAS");
                query.Append("     , p.ATENDENTERESPEMDIASSAC");
                query.Append("     , a.Cod_seq_Secao");
                query.Append("     , a.ReclamacaoProcede");

                query.Append("  From niff_chm_Atendimento a, NIFF_CHM_EMTUAtendimento e, Niff_CHM_Usuarios ur");
                query.Append("     , Niff_CHM_Usuarios u, Niff_CHM_Usuarios uf, Niff_CHM_Usuarios uc");
                query.Append("     , Niff_CHM_Usuarios ut, Niff_Chm_Empresas p");
                query.Append(" Where e.IdEmtu(+) = a.IdEmtu");
                query.Append("   and ur.idUsuario(+) = a.IdUsuarioResponsavel");
                query.Append("   And u.Idempresa = p.Idempresa ");

                query.Append("   and u.idUsuario(+) = a.IdUsuario");
                query.Append("   and uf.idUsuario(+) = a.IdUsuarioFinaliza");
                query.Append("   and uc.idUsuario(+) = a.IdUsuarioRetornouAoCliente");
                query.Append("   and ut.idUsuario(+) = a.IdUsuarioRetorno");

                if (empresa != 0)
                    query.Append("   and a.IdEmpresa = " + empresa);

                if (telaQueChamou != Publicas.TelaPesquisaSAC.Grid)
                    query.Append("   and a.DataAbertura Between last_day(add_Months(Trunc(Sysdate), -13)) + 1 And Sysdate");
                
                switch (telaQueChamou)
                {
                    case Publicas.TelaPesquisaSAC.Grid:
                        //query.Append("  and ((AguardaSatisfacaoCliente = 'S' and a.retornou = 'S') ");
                        //query.Append("   or  (AguardaSatisfacaoCliente = 'N' and a.retornou = 'N')) ");
                        query.Append("  and (trunc(a.datacancelado) >= trunc(Sysdate) - p.exibiroscanceladosnosacdias Or a.datacancelado Is Null)");

                        if (status != "T")
                            query.Append("  and MotivoSatisfacao is null");

                        if (ano != 0)
                        {
                            //query.Append("  and To_char(a.DataAbertura,'yyyy') = " + ano);

                            if (DateTime.Now.Month < 3 && ano == DateTime.Now.Year)
                                query.Append("   and trunc(a.DataAbertura) between To_date('31/07/2019','dd/mm/yyyy') and to_date('" + DateTime.Now.Date.ToShortDateString() + "','dd/mm/yyyy')");
                            else
                                query.Append("   and To_char(a.DataAbertura,'yyyy') = '" + ano + "'");
                        }

                        if (status == "A")
                            query.Append("  and a.status in ('A','R')");
                        if (status == "F")
                            query.Append("  and a.status = 'F'");
                        if (status == "C")
                            query.Append("  and a.status = 'C'");
                        if (status == "R")
                            query.Append("  and a.status = 'R'");
                        break;

                    case Publicas.TelaPesquisaSAC.Atendimento:
                        query.Append("  and (a.status = 'A' or a.status = 'R') ");
                        query.Append("  and a.situacao = 'MA'");
                        break;
                    case Publicas.TelaPesquisaSAC.Finaliza:
                        query.Append("  and (a.status = 'A' or a.status = 'R') ");
                        query.Append("  and a.situacao = 'EF'");
                        break;
                    case Publicas.TelaPesquisaSAC.Responde:
                        query.Append("  and a.status = 'A'");
                        query.Append("  and a.situacao = 'EC'");
                        query.Append("  and ur.IdDepartamento = " + departamento);
                        break;
                    case Publicas.TelaPesquisaSAC.Retorno:
                        query.Append("  and a.status = 'F'");
                        query.Append("  and (a.situacao = 'EF' or a.situacao = 'AC')");
                        query.Append("  and a.retorno in ('F', 'T')");
                        query.Append("  and (a.retornou = 'N' or a.retornou = null)");
                        break;
                    case Publicas.TelaPesquisaSAC.Satisfacao:
                        query.Append("  and a.status = 'F'");
                        query.Append("  and a.retornou = 'S'");
                        query.Append("  and AguardaSatisfacaoCliente = 'S'");
                        break;

                }
                
                Query executar = sessao.CreateQuery(query.ToString());

                atendimentoReader = executar.ExecuteQuery();

                using (atendimentoReader)
                {
                    while (atendimentoReader.Read())
                    {
                        Atendimento _atendimento = new Atendimento();

                        _atendimento.Existe = true;
                        _atendimento.Id = Convert.ToInt32(atendimentoReader["IDATENDIMENTO"].ToString());

                        _atendimento.Codigo = atendimentoReader["Codigo"].ToString();
                        _atendimento.IdUsuario = Convert.ToInt32(atendimentoReader["IDUSUARIO"].ToString());
                        _atendimento.IdEmtu = Convert.ToInt32(atendimentoReader["IDEMTU"].ToString());
                        _atendimento.CodigoEmtu = atendimentoReader["CodigoEMTU"].ToString();
                        _atendimento.DescricaoEmtu = atendimentoReader["Descricao"].ToString();
                        _atendimento.IdTipoAtendimento = Convert.ToInt32(atendimentoReader["IDTPATENDIMENTO"].ToString());
                        try
                        {
                            _atendimento.CodIntFunc = Convert.ToDecimal(atendimentoReader["CodFunc"].ToString());
                        }
                        catch { }
                        if (atendimentoReader["IDUSUARIOCANCELA"].ToString() != "")
                            _atendimento.IdUsuarioCancela = Convert.ToInt32(atendimentoReader["IDUSUARIOCANCELA"].ToString());

                        if (atendimentoReader["IDUSUARIOFINALIZA"].ToString() != "")
                            _atendimento.IdUsuarioFinaliza = Convert.ToInt32(atendimentoReader["IDUSUARIOFINALIZA"].ToString());

                        if (atendimentoReader["IDUSUARIORETORNO"].ToString() != "")
                            _atendimento.IdUsuarioRetorno = Convert.ToInt32(atendimentoReader["IDUSUARIORETORNO"].ToString());

                        if (atendimentoReader["IDUSUARIORESPONSAVEL"].ToString() != "")
                            _atendimento.IdUsuarioResponsavel = Convert.ToInt32(atendimentoReader["IDUSUARIORESPONSAVEL"].ToString());

                        _atendimento.MotivoCancelamento = atendimentoReader["MOTIVOCANCELAMENTO"].ToString();
                        _atendimento.MotivoRetorno = atendimentoReader["MOTIVORETORNO"].ToString();
                        _atendimento.MotivoSatisfacao = atendimentoReader["MOTIVOSATISFACAO"].ToString();
                        _atendimento.TextoAtendimento = atendimentoReader["TextoAtendimento"].ToString();
                        _atendimento.TextoResposta = atendimentoReader["TextoResposta"].ToString();

                        _atendimento.AguardaSatisfacaoCliente = atendimentoReader["AguardaSatisfacaoCliente"].ToString() == "S";
                        _atendimento.Retornou = atendimentoReader["Retornou"].ToString() == "S";
                        _atendimento.ReclamacaoProcede = atendimentoReader["ReclamacaoProcede"].ToString();
                        _atendimento.ProcedeGrid = (_atendimento.ReclamacaoProcede == "" ? "" :
                                                   (_atendimento.ReclamacaoProcede == "N" ? "Não Procede" : "Procede"));


                        _atendimento.NomeCliente = atendimentoReader["Nome"].ToString();

                        if (atendimentoReader["Telefone"].ToString() != "")
                        {
                            _atendimento.TelefoneCliente = Convert.ToDecimal(atendimentoReader["Telefone"].ToString());
                            _atendimento.TelefoneFormatado = _atendimento.TelefoneCliente.ToString("(00) 0000-0000");
                        }

                        if (atendimentoReader["Celular"].ToString() != "")
                        {
                            _atendimento.Celular = Convert.ToDecimal(atendimentoReader["Celular"].ToString());
                            _atendimento.CelularFormatado = _atendimento.Celular.ToString("(00) 00000-0000");
                        }
                        _atendimento.RGCliente = atendimentoReader["RG"].ToString();
                        _atendimento.UFCliente = atendimentoReader["UF"].ToString();
                        _atendimento.ClienteAnonimo = atendimentoReader["ClienteAnonimo"].ToString() == "S";
                        _atendimento.EmailCliente = atendimentoReader["Email"].ToString();
                        _atendimento.EnderecoCliente = atendimentoReader["Endereco"].ToString();
                        _atendimento.CidadeCliente = atendimentoReader["Cidade"].ToString();

                        if (atendimentoReader["CPF"].ToString() != "")
                            _atendimento.CPFCliente = Convert.ToDecimal(atendimentoReader["CPF"].ToString());

                        if (atendimentoReader["DataAbertura"].ToString() != "")
                            _atendimento.DataAbertura = Convert.ToDateTime(atendimentoReader["DataAbertura"].ToString());

                        if (atendimentoReader["DataCancelado"].ToString() != "")
                            _atendimento.DataCancelado = Convert.ToDateTime(atendimentoReader["DataCancelado"].ToString());

                        if (atendimentoReader["DataFinalizado"].ToString() != "")
                            _atendimento.DataFinalizado = Convert.ToDateTime(atendimentoReader["DataFinalizado"].ToString());

                        if (atendimentoReader["DataResposta"].ToString() != "")
                            _atendimento.DataResposta = Convert.ToDateTime(atendimentoReader["DataResposta"].ToString());

                        _atendimento.CodigoLinha = atendimentoReader["CodigoLinha"].ToString();
                        try
                        {
                            _atendimento.CodSeqSecao = Convert.ToInt32(atendimentoReader["Cod_seq_Secao"].ToString());
                        }
                        catch { }

                        #region Opções
                        switch (atendimentoReader["Status"].ToString())
                        {
                            case "A": _atendimento.Status = Publicas.StatusAtendimento.Ativo;
                                    break;
                            case "F": _atendimento.Status = Publicas.StatusAtendimento.Finalizado;
                                break;
                            case "C": _atendimento.Status = Publicas.StatusAtendimento.Cancelado;
                                break;
                            case "R": _atendimento.Status = Publicas.StatusAtendimento.Respondido;
                                break;
                        }

                        switch (atendimentoReader["Situacao"].ToString())
                        {
                            case "EC":
                                _atendimento.Situacao = Publicas. SituacaoAtendimento.EnviadoAoColaborador;
                                break;
                            case "EF":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.EnviadoAoFinalizador;
                                break;
                            case "MA":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.ManterComAtendente;
                                break;
                            case "FC":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.Cancelado;
                                break;
                            case "FF":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.Finalizado;
                                break;
                            case "AS":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.AguardandoSatisfacao;
                                break;
                            case "AC":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.AguardandoRetornoAoCliente;
                                break;
                        }

                        _atendimento.DescricaoDaSituacao = Publicas.GetDescription(_atendimento.Situacao, "");

                        switch (atendimentoReader["Origem"].ToString())
                        {
                            case "E":
                                _atendimento.Origem = Publicas.OrigemAtendimento.Email;
                                break;
                            case "J":
                                _atendimento.Origem = Publicas.OrigemAtendimento.JornalRadio;
                                break;
                            case "S":
                                _atendimento.Origem = Publicas.OrigemAtendimento.Internet;
                                break;
                            case "P":
                                _atendimento.Origem = Publicas.OrigemAtendimento.Pessoalmente;
                                break;
                            case "T":
                                _atendimento.Origem = Publicas.OrigemAtendimento.Telefone;
                                break;
                            case "C":
                                _atendimento.Origem = Publicas.OrigemAtendimento.OrgaoGestor;
                                break;
                        }

                        _atendimento.DescricaoDaOrigem = Publicas.GetDescription(_atendimento.Origem, "");

                        switch (atendimentoReader["Retorno"].ToString())
                        {
                            case "N":
                                _atendimento.OpcoesDeRetorno = Publicas.OpcaoDeRetornoAtendimento.Nenhum;
                                break;
                            case "F":
                                _atendimento.OpcoesDeRetorno = Publicas.OpcaoDeRetornoAtendimento.Fax;
                                break;
                            case "E":
                                _atendimento.OpcoesDeRetorno = Publicas.OpcaoDeRetornoAtendimento.Email;
                                break;
                            case "T":
                                _atendimento.OpcoesDeRetorno = Publicas.OpcaoDeRetornoAtendimento.Telefone;
                                break;
                        }

                        switch (atendimentoReader["SAtisfacao"].ToString())
                        {
                            case "R":
                                _atendimento.Satisfacao = Publicas.TipoDeSatisfacaoAtendimento.Ruim;
                                break;
                            case "B":
                                _atendimento.Satisfacao = Publicas.TipoDeSatisfacaoAtendimento.Bom;
                                break;
                            case "M":
                                _atendimento.Satisfacao = Publicas.TipoDeSatisfacaoAtendimento.MuitoBom;
                                break;
                            case "G":
                                _atendimento.Satisfacao = Publicas.TipoDeSatisfacaoAtendimento.Regular;
                                break;
                            case "E":
                                _atendimento.Satisfacao = Publicas.TipoDeSatisfacaoAtendimento.Excelente;
                                break;
                        }
                        #endregion

                        query.Append("     , u.Nome UsuarioAbertura");
                        query.Append("     , ur.Nome UsuarioResponsavel");
                        query.Append("     , uf.Nome UsuarioFinalizador");
                        query.Append("     , uc.Nome UsuarioColaborador");

                        _atendimento.NomeDoUsuarioUltimoStatus = (atendimentoReader["UsuarioFinalizador"].ToString() != "" ? atendimentoReader["UsuarioFinalizador"].ToString() :
                                                                 (atendimentoReader["UsuarioCliente"].ToString() != "" ? atendimentoReader["UsuarioCliente"].ToString() :
                                                                 (atendimentoReader["UsuarioRetorno"].ToString() != "" ? atendimentoReader["UsuarioRetorno"].ToString() : atendimentoReader["UsuarioAbertura"].ToString())));

                        int _diasResposta = Convert.ToInt32(atendimentoReader["RESPONDEREMDIASSAC"].ToString());
                        int _diasEtapa = Convert.ToInt32(atendimentoReader["ATENDENTERESPEMDIASSAC"].ToString());

                        _atendimento.SLAAtendente = (atendimentoReader["UsuarioFinalizador"].ToString() != "" ? _atendimento.DataFinalizado.AddDays(_diasEtapa) :
                                           (atendimentoReader["UsuarioCliente"].ToString() != "" ? _atendimento.DataRetornoAoCliente.AddDays(_diasEtapa) :
                                           (atendimentoReader["UsuarioRetorno"].ToString() != "" ? _atendimento.DataResposta.AddDays(_diasEtapa) :
                                           _atendimento.DataAbertura.AddDays(_diasEtapa))));

                        _atendimento.SLA = _atendimento.DataAbertura.AddDays(_diasResposta);

                        /* if (_atendimento.MotivoSatisfacao != "")
                         {
                             _atendimento.DescricaoDaSituacao = Publicas.GetDescription(Publicas.SituacaoAtendimento.Satisfacao, "");
                         }*/

                        _lista.Add(_atendimento);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public Atendimento Consulta(string codigo, int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Atendimento _atendimento = new Atendimento();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select a.IdAtendimento ");
                query.Append("     , a.Codigo");
                query.Append("     , a.IdUsuario");
                query.Append("     , a.IdTpAtendimento");
                query.Append("     , a.ClienteAnonimo");
                query.Append("     , a.TextoAtendimento");
                query.Append("     , a.TextoResposta");
                query.Append("     , a.DataAbertura");
                query.Append("     , a.DataFinalizado");
                query.Append("     , a.DataResposta");
                query.Append("     , a.DataCancelado");
                query.Append("     , a.Status");
                query.Append("     , a.Retorno");
                query.Append("     , a.Retornou");
                query.Append("     , a.MotivoRetorno");
                query.Append("     , a.MotivoCancelamento");
                query.Append("     , a.Satisfacao");
                query.Append("     , a.MotivoSatisfacao");
                query.Append("     , a.codigolinha");
                query.Append("     , a.IdEmtu");
                query.Append("     , a.codfunc");
                query.Append("     , a.Nome");
                query.Append("     , a.RG");
                query.Append("     , a.CPF");
                query.Append("     , a.Endereco");
                query.Append("     , a.Cidade");
                query.Append("     , a.UF");
                query.Append("     , a.Email");
                query.Append("     , a.Telefone");
                query.Append("     , a.Celular");
                query.Append("     , a.IdUsuarioResponsavel");
                query.Append("     , a.Origem");
                query.Append("     , a.IdUsuarioFinaliza");
                query.Append("     , a.IdUsuarioRetorno");
                query.Append("     , a.IDUSUARIOCANCELA");
                query.Append("     , a.Situacao");
                query.Append("     , a.RespostaAoCliente");
                query.Append("     , a.DataRetornoAoCliente");
                query.Append("     , a.IdUsuarioRetornouAoCliente");
                query.Append("     , e.Codigo CodigoEMTU");
                query.Append("     , e.Descricao");
                query.Append("     , a.AguardaSatisfacaoCliente");
                query.Append("     , a.Cod_seq_Secao");
                query.Append("     , a.ReclamacaoProcede");

                query.Append("  From niff_chm_Atendimento a, NIFF_CHM_EMTUAtendimento e");
                query.Append(" Where a.Codigo = '" + codigo + "'" );
                query.Append("   and e.IdEmtu(+) = a.IdEmtu");
                query.Append("   and a.idempresa = " + empresa);

                Query executar = sessao.CreateQuery(query.ToString());

                atendimentoReader = executar.ExecuteQuery();

                using (atendimentoReader)
                {
                    if (atendimentoReader.Read())
                    {
                        _atendimento.Existe = true;
                        _atendimento.Id = Convert.ToInt32(atendimentoReader["IDATENDIMENTO"].ToString());

                        _atendimento.Codigo = atendimentoReader["Codigo"].ToString();
                        _atendimento.IdUsuario = Convert.ToInt32(atendimentoReader["IDUSUARIO"].ToString());
                        _atendimento.IdEmtu = Convert.ToInt32(atendimentoReader["IDEMTU"].ToString());
                        _atendimento.CodigoEmtu = atendimentoReader["CodigoEMTU"].ToString();
                        _atendimento.DescricaoEmtu = atendimentoReader["Descricao"].ToString();
                        _atendimento.IdTipoAtendimento = Convert.ToInt32(atendimentoReader["IDTPATENDIMENTO"].ToString());
                        try
                        {
                            _atendimento.CodIntFunc = Convert.ToDecimal(atendimentoReader["CodFunc"].ToString());
                        }
                        catch { }

                        if (atendimentoReader["IDUSUARIOCANCELA"].ToString() != "")
                            _atendimento.IdUsuarioCancela = Convert.ToInt32(atendimentoReader["IDUSUARIOCANCELA"].ToString());

                        if (atendimentoReader["IDUSUARIOFINALIZA"].ToString() != "")
                            _atendimento.IdUsuarioFinaliza = Convert.ToInt32(atendimentoReader["IDUSUARIOFINALIZA"].ToString());

                        if (atendimentoReader["IDUSUARIORETORNO"].ToString() != "")
                            _atendimento.IdUsuarioRetorno = Convert.ToInt32(atendimentoReader["IDUSUARIORETORNO"].ToString());

                        if (atendimentoReader["IDUSUARIORESPONSAVEL"].ToString() != "")
                            _atendimento.IdUsuarioResponsavel = Convert.ToInt32(atendimentoReader["IDUSUARIORESPONSAVEL"].ToString());

                        _atendimento.MotivoCancelamento = atendimentoReader["MOTIVOCANCELAMENTO"].ToString();
                        _atendimento.MotivoRetorno = atendimentoReader["MOTIVORETORNO"].ToString();
                        _atendimento.MotivoSatisfacao = atendimentoReader["MOTivOSATISFACAO"].ToString();
                        _atendimento.TextoAtendimento = atendimentoReader["TextoAtendimento"].ToString();
                        _atendimento.TextoResposta = atendimentoReader["TextoResposta"].ToString();
                        _atendimento.AguardaSatisfacaoCliente = atendimentoReader["AguardaSatisfacaoCliente"].ToString() == "S";
                        _atendimento.Retornou = atendimentoReader["Retornou"].ToString() == "S";
                        _atendimento.ReclamacaoProcede = atendimentoReader["ReclamacaoProcede"].ToString();
                        _atendimento.ProcedeGrid = (_atendimento.ReclamacaoProcede == "" ? "" :
                                                   (_atendimento.ReclamacaoProcede == "N" ? "Não Procede" : "Procede"));
                        _atendimento.NomeCliente = atendimentoReader["Nome"].ToString();

                        if (atendimentoReader["Telefone"].ToString() != "")
                            _atendimento.TelefoneCliente = Convert.ToDecimal(atendimentoReader["Telefone"].ToString());

                        if (atendimentoReader["Celular"].ToString() != "")
                            _atendimento.Celular = Convert.ToDecimal(atendimentoReader["Celular"].ToString());


                        _atendimento.RGCliente = atendimentoReader["RG"].ToString();
                        _atendimento.UFCliente = atendimentoReader["UF"].ToString();
                        _atendimento.ClienteAnonimo = atendimentoReader["ClienteAnonimo"].ToString() == "S";
                        _atendimento.EmailCliente = atendimentoReader["Email"].ToString();
                        _atendimento.EnderecoCliente = atendimentoReader["Endereco"].ToString();
                        _atendimento.CidadeCliente = atendimentoReader["Cidade"].ToString();
                        if (atendimentoReader["CPF"].ToString() != "")
                            _atendimento.CPFCliente = Convert.ToDecimal(atendimentoReader["CPF"].ToString());

                        if (atendimentoReader["DataAbertura"].ToString() != "")
                            _atendimento.DataAbertura = Convert.ToDateTime(atendimentoReader["DataAbertura"].ToString());

                        if (atendimentoReader["DataCancelado"].ToString() != "")
                            _atendimento.DataCancelado = Convert.ToDateTime(atendimentoReader["DataCancelado"].ToString());

                        if (atendimentoReader["DataFinalizado"].ToString() != "")
                            _atendimento.DataFinalizado = Convert.ToDateTime(atendimentoReader["DataFinalizado"].ToString());

                        if (atendimentoReader["DataResposta"].ToString() != "")
                            _atendimento.DataResposta = Convert.ToDateTime(atendimentoReader["DataResposta"].ToString());

                        _atendimento.CodigoLinha = atendimentoReader["CodigoLinha"].ToString();

                        try
                        {
                            _atendimento.CodSeqSecao = Convert.ToInt32(atendimentoReader["Cod_seq_Secao"].ToString());
                        }
                        catch { }

                        try
                        {
                            if (atendimentoReader["DataRetornoAoCliente"].ToString() != "")
                                _atendimento.DataRetornoAoCliente = Convert.ToDateTime(atendimentoReader["DataRetornoAoCliente"].ToString());
                        }
                        catch {}

                        if (atendimentoReader["IdUsuarioRetornouAoCliente"].ToString() != "")
                            _atendimento.IdUsuarioRetornoAoCliente = Convert.ToInt16(atendimentoReader["IdUsuarioRetornouAoCliente"].ToString());

                        _atendimento.TextoRetornoAoCliente = atendimentoReader["RespostaAoCliente"].ToString();

                        #region Opções
                        switch (atendimentoReader["Status"].ToString())
                        {
                            case "A":
                                _atendimento.Status = Publicas.StatusAtendimento.Ativo;
                                break;
                            case "F":
                                _atendimento.Status = Publicas.StatusAtendimento.Finalizado;
                                break;
                            case "C":
                                _atendimento.Status = Publicas.StatusAtendimento.Cancelado;
                                break;
                            case "R":
                                _atendimento.Status = Publicas.StatusAtendimento.Respondido;
                                break;
                        }

                        switch (atendimentoReader["Situacao"].ToString())
                        {
                            case "EC":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.EnviadoAoColaborador;
                                break;
                            case "EF":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.EnviadoAoFinalizador;
                                break;
                            case "MA":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.ManterComAtendente;
                                break;
                            case "FC":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.Cancelado;
                                break;
                            case "FF":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.Finalizado;
                                break;
                            case "AS":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.AguardandoSatisfacao;
                                break;
                            case "AC":
                                _atendimento.Situacao = Publicas.SituacaoAtendimento.AguardandoRetornoAoCliente;
                                break;
                        }

                        switch (atendimentoReader["Origem"].ToString())
                        {
                            case "E":
                                _atendimento.Origem = Publicas.OrigemAtendimento.Email;
                                break;
                            case "J":
                                _atendimento.Origem = Publicas.OrigemAtendimento.JornalRadio;
                                break;
                            case "S":
                                _atendimento.Origem = Publicas.OrigemAtendimento.Internet;
                                break;
                            case "P":
                                _atendimento.Origem = Publicas.OrigemAtendimento.Pessoalmente;
                                break;
                            case "T":
                                _atendimento.Origem = Publicas.OrigemAtendimento.Telefone;
                                break;
                            case "C":
                                _atendimento.Origem = Publicas.OrigemAtendimento.OrgaoGestor;
                                break;
                        }

                        switch (atendimentoReader["Retorno"].ToString())
                        {
                            case "N":
                                _atendimento.OpcoesDeRetorno = Publicas.OpcaoDeRetornoAtendimento.Nenhum;
                                break;
                            case "F":
                                _atendimento.OpcoesDeRetorno = Publicas.OpcaoDeRetornoAtendimento.Fax;
                                break;
                            case "E":
                                _atendimento.OpcoesDeRetorno = Publicas.OpcaoDeRetornoAtendimento.Email;
                                break;
                            case "T":
                                _atendimento.OpcoesDeRetorno = Publicas.OpcaoDeRetornoAtendimento.Telefone;
                                break;
                        }

                        switch (atendimentoReader["SAtisfacao"].ToString())
                        {
                            case "R":
                                _atendimento.Satisfacao = Publicas.TipoDeSatisfacaoAtendimento.Ruim;
                                break;
                            case "B":
                                _atendimento.Satisfacao = Publicas.TipoDeSatisfacaoAtendimento.Bom;
                                break;
                            case "M":
                                _atendimento.Satisfacao = Publicas.TipoDeSatisfacaoAtendimento.MuitoBom;
                                break;
                            case "G":
                                _atendimento.Satisfacao = Publicas.TipoDeSatisfacaoAtendimento.Regular;
                                break;
                            case "E":
                                _atendimento.Satisfacao = Publicas.TipoDeSatisfacaoAtendimento.Excelente;
                                break;

                        }
                        #endregion

                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _atendimento;
        }

        public bool Grava(Atendimento atendimento, Atendimento atendimentoLog, List<Atendimento.Anexos> anexos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int _idAtendimento = 0;
            LogSAC _log = new LogSAC();
            OracleParameter parametro = new OracleParameter();
            List<OracleParameter> parametros = new List<OracleParameter>();

            try
            {
                if (!atendimento.Existe)
                {
                    query.Clear();
                    query.Append("(select Max(IdAtendimento) + 1 next from Niff_CHM_Atendimento)");
                    Query executar = sessao.CreateQuery(query.ToString());

                    atendimentoReader = executar.ExecuteQuery();

                    using (atendimentoReader)
                    {
                        if (atendimentoReader.Read())
                        {
                            _idAtendimento = Convert.ToInt32(atendimentoReader["next"].ToString());
                        }
                    }

                    query.Clear();
                    query.Append("Insert into niff_chm_atendimento");
                    query.Append("   ( IdAtendimento ");
                    query.Append("     , Codigo");
                    query.Append("     , IdUsuario");
                    query.Append("     , IdTpAtendimento");
                    query.Append("     , ClienteAnonimo");
                    query.Append("     , TextoAtendimento");
                    query.Append("     , TextoResposta");
                    query.Append("     , DataAbertura");
                    if (!string.IsNullOrEmpty(atendimento.TextoResposta))
                        query.Append("     , DataResposta");

                    query.Append("     , Status");
                    query.Append("     , Retorno");
                    query.Append("     , Retornou");
                    query.Append("     , MotivoRetorno");
                    query.Append("     , MotivoCancelamento");
                    query.Append("     , Satisfacao");
                    query.Append("     , codigolinha");
                    query.Append("     , IdEmtu");
                    query.Append("     , Nome");
                    query.Append("     , RG");
                    query.Append("     , CPF");
                    query.Append("     , Endereco");
                    query.Append("     , Cidade");
                    query.Append("     , UF");
                    query.Append("     , Email");
                    query.Append("     , Telefone");
                    query.Append("     , Celular");
                    query.Append("     , IdUsuarioResponsavel");
                    query.Append("     , Origem");
                    if (Publicas._telaQueChamouPesquisaDeAtendimento == Publicas.TelaPesquisaSAC.Finaliza)
                        query.Append("     , DataFinalizado, IdUsuarioFinaliza");

                    if (atendimento.IdUsuarioRetorno != 0)
                        query.Append("     , IdUsuarioRetorno");
                    query.Append("     , Situacao");

                    if (atendimento.TextoRetornoAoCliente != "" &&
                        Publicas._telaQueChamouPesquisaDeAtendimento == Publicas.TelaPesquisaSAC.Finaliza)
                    {
                        query.Append("     , RespostaAoCliente ");
                        query.Append("     , DataRetornoAoCliente");
                        query.Append("     , IdUsuarioRetornouAoCliente");
                    }

                    query.Append("     , IdEmpresa, MotivoSatisfacao");
                    query.Append("     , Cod_seq_Secao");
                    query.Append("     , ReclamacaoProcede");


                    query.Append("  ) Values ( " + _idAtendimento );
                    query.Append(", '" + atendimento.Codigo + "'");
                    query.Append(", " + Publicas._idUsuario.ToString() );
                    query.Append(", " + atendimento.IdTipoAtendimento );
                    query.Append(", '" + (atendimento.ClienteAnonimo ? "S" : "N") + "'");
                    //query.Append(", '" + atendimento.TextoAtendimento + "'");
                    //query.Append(", '" + atendimento.TextoResposta + "'");

                    query.Append(", :textoAtendimento");
                    query.Append(", :textoResposta");

                    query.Append(", sysdate ");

                    if (!string.IsNullOrEmpty(atendimento.TextoResposta))
                        query.Append(", sysdate ");

                    query.Append(", '" + (atendimento.Status == Publicas.StatusAtendimento.Ativo ? "A" :
                                           (atendimento.Status == Publicas.StatusAtendimento.Cancelado ? "C" :
                                            (atendimento.Status == Publicas.StatusAtendimento.Respondido ? "R" : "F"))) + "'");

                    query.Append(", '" + (atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Email ? "E" :
                                           (atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Fax ? "F" :
                                            (atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Telefone ? "T" : "N"))) + "'");

                    query.Append(", '" + (atendimento.Retornou ? "S" : "N") + "'");

                    //query.Append(", '" + atendimento.MotivoRetorno + "'");
                    //query.Append(", '" + atendimento.MotivoCancelamento + "'");
                    query.Append(", :MotivoRetorno");
                    query.Append(", :MotivoCancelamento");

                    query.Append(", '" + (atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.Bom ? "B" :
                                           (atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.MuitoBom ? "M" :
                                           (atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.Regular ? "G" :
                                           (atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.Excelente ? "E" :
                                           (atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.Ruim ? "R" :
                                           "S"))))) + "'");

                    query.Append(", " + atendimento.CodigoLinha + "");
                    query.Append(", " + atendimento.IdEmtu);
                    query.Append(", '" + atendimento.NomeCliente + "'");
                    query.Append(", '" + atendimento.RGCliente + "'");
                    query.Append(", '" + atendimento.CPFCliente+ "'");
                    query.Append(", '" + atendimento.EnderecoCliente + "'");
                    query.Append(", '" + atendimento.CidadeCliente + "'");
                    query.Append(", '" + atendimento.UFCliente + "'");
                    query.Append(", '" + atendimento.EmailCliente + "'");
                    query.Append(", " + atendimento.TelefoneCliente);
                    query.Append(", " + atendimento.Celular);

                    query.Append(", " + atendimento.IdUsuarioResponsavel);

                    query.Append(", '" + (atendimento.Origem == Publicas.OrigemAtendimento.Email ? "E" :
                                           (atendimento.Origem == Publicas.OrigemAtendimento.Internet ? "S" :
                                            (atendimento.Origem == Publicas.OrigemAtendimento.JornalRadio ? "J" :
                                             (atendimento.Origem == Publicas.OrigemAtendimento.OrgaoGestor ? "C" :
                                              (atendimento.Origem == Publicas.OrigemAtendimento.Pessoalmente ? "P" : "T"))))) + "'");

                    if (Publicas._telaQueChamouPesquisaDeAtendimento == Publicas.TelaPesquisaSAC.Finaliza)
                    {
                        query.Append("     , sysdate");
                        query.Append("     , " + Publicas._idUsuario);
                    }

                    if (atendimento.IdUsuarioRetorno != 0)
                        query.Append(", " + atendimento.IdUsuarioRetorno);

                    query.Append(", '" + (atendimento.Situacao == Publicas.SituacaoAtendimento.Cancelado ? "FC" :
                                          (atendimento.Situacao == Publicas.SituacaoAtendimento.EnviadoAoColaborador ? "EC" :
                                            (atendimento.Situacao == Publicas.SituacaoAtendimento.EnviadoAoFinalizador ? "EF" :
                                              (atendimento.Situacao == Publicas.SituacaoAtendimento.ManterComAtendente ? "MA" :
                                              (atendimento.Situacao == Publicas.SituacaoAtendimento.AguardandoRetornoAoCliente ? "AC" :
                                              (atendimento.Situacao == Publicas.SituacaoAtendimento.AguardandoSatisfacao ? "AS" : "FF"))))))+ "'");
                    
                    if (atendimento.TextoRetornoAoCliente != "" &&
                        Publicas._telaQueChamouPesquisaDeAtendimento == Publicas.TelaPesquisaSAC.Finaliza)
                    {
                        //query.Append("     , '" + atendimento.TextoRetornoAoCliente + "'");
                        query.Append("     , :TextoRetornoAoCliente");
                        query.Append("     , sysdate ");
                        query.Append("     , " + Publicas._idUsuario);
                    }
                    query.Append("     , " + atendimento.idEmpresa);
                    query.Append("     , :MotivoSatisfacao");

                    query.Append("     , " + atendimento.CodSeqSecao);
                    query.Append("     , '" + atendimento.ReclamacaoProcede + "'");
                    query.Append(")");

                    #region prepara o Grava log
                    _log.Descricao = "Atendimento nº " + atendimento.Codigo +
                        " foi aberto pelo Usuário " + Publicas._usuario.UsuarioAcesso;
                    _log.IdUsuario = Publicas._idUsuario;
                    _log.IdAtendimento = _idAtendimento;

                    #endregion
                }
                else
                {
                    #region Prepara log
                    string descricaoLog = "Atendimento nº " + atendimento.Codigo +
                        " foi alterado pelo Usuário " + Publicas._usuario.UsuarioAcesso;
                    string de = "";
                    string para = "";

                    #region CodigoEMTU
                    if (atendimento.IdEmtu != atendimentoLog.IdEmtu)
                    {
                        
                        TipoDeAtendimentoEMTU _tipoEMTU = new TipoDeAtendimentoEMTUDAO().ConsultaPorId(atendimentoLog.IdEmtu);
                        de = _tipoEMTU.Codigo;

                        _tipoEMTU = new TipoDeAtendimentoEMTUDAO().ConsultaPorId(atendimento.IdEmtu);
                        para = _tipoEMTU.Codigo;

                        descricaoLog = descricaoLog + " (Código EMTU de [" + de + "] para [" + para + "])";
                    }
                    #endregion

                    #region Textos
                    if (atendimento.TextoAtendimento != atendimentoLog.TextoAtendimento)
                        descricaoLog = descricaoLog + " (Descrição atendimento de [" + atendimentoLog.TextoAtendimento + "] para [" + atendimento.TextoAtendimento + "])";

                    if (atendimento.TextoResposta != atendimentoLog.TextoResposta)
                        descricaoLog = descricaoLog + " (Texto resposta de [" + atendimentoLog.TextoResposta + "] para [" + atendimento.TextoResposta + "])";

                    if (atendimento.MotivoCancelamento != atendimentoLog.MotivoCancelamento)
                        descricaoLog = descricaoLog + " (Motivo Cancelamento de [" + atendimentoLog.MotivoCancelamento + "] para [" + atendimento.MotivoCancelamento + "])";

                    if (atendimento.MotivoRetorno != atendimentoLog.MotivoRetorno)
                        descricaoLog = descricaoLog + " (Motivo Retorno de [" + atendimentoLog.MotivoRetorno + "] para [" + atendimento.MotivoRetorno + "])";

                    if (atendimento.MotivoSatisfacao != atendimentoLog.MotivoSatisfacao)
                        descricaoLog = descricaoLog + " (Motivo Satisfação de [" + atendimentoLog.MotivoSatisfacao + "] para [" + atendimento.MotivoSatisfacao + ")";

                    if (atendimento.TextoRetornoAoCliente != atendimentoLog.TextoRetornoAoCliente)
                        descricaoLog = descricaoLog + " (Retorno ao cliente de [" + atendimentoLog.TextoRetornoAoCliente + "] para [" + atendimento.TextoRetornoAoCliente + ")";

                    #endregion

                    #region Opções
                    if (atendimento.Status != atendimentoLog.Status)
                        descricaoLog = descricaoLog + " ( Status de [" + atendimentoLog.Status.ToString() + "] para [" + atendimento.Status.ToString() + "])";
                    if (atendimento.OpcoesDeRetorno != atendimentoLog.OpcoesDeRetorno)
                        descricaoLog = descricaoLog + " ( Opções de retorno de [" + atendimentoLog.OpcoesDeRetorno.ToString() + "] para " + atendimento.OpcoesDeRetorno.ToString() + "])";
                    if (atendimento.Situacao != atendimentoLog.Situacao)
                        descricaoLog = descricaoLog + " ( Situação de [" + atendimentoLog.Situacao.ToString() + "] para [" + atendimento.Situacao.ToString() + "])";
                    if (atendimento.Origem != atendimentoLog.Origem)
                        descricaoLog = descricaoLog + " ( Origem de [" + atendimentoLog.Origem.ToString() + "] para [" + atendimento.Origem.ToString() + "])";
                    #endregion

                    #region Cliente
                    if (atendimento.NomeCliente != atendimentoLog.NomeCliente)
                        descricaoLog = descricaoLog + "( Nome do Cliente de [" + atendimentoLog.NomeCliente + "] para [" + atendimento.NomeCliente + "])";
                    if (atendimento.EnderecoCliente != atendimentoLog.EnderecoCliente)
                        descricaoLog = descricaoLog + "( Endereço do Cliente de [" + atendimentoLog.EnderecoCliente + "] para [" + atendimento.EnderecoCliente + "])";
                    if (atendimento.CidadeCliente != atendimentoLog.CidadeCliente)
                        descricaoLog = descricaoLog + "( Cidade do Cliente de [" + atendimentoLog.CidadeCliente + "] para [" + atendimento.CidadeCliente + "])";
                    if (atendimento.CPFCliente != atendimentoLog.CPFCliente)
                        descricaoLog = descricaoLog + "( CPF do Cliente de [" + atendimentoLog.CPFCliente + "] para [" + atendimento.CPFCliente + "])";
                    if (atendimento.RGCliente != atendimentoLog.RGCliente)
                        descricaoLog = descricaoLog + "( RG do Cliente de [" + atendimentoLog.RGCliente + "] para [" + atendimento.RGCliente + "])";
                    if (atendimento.UFCliente != atendimentoLog.UFCliente)
                        descricaoLog = descricaoLog + "( UF do Cliente de [" + atendimentoLog.UFCliente + "] para [" + atendimento.UFCliente + "])";
                    if (atendimento.EmailCliente != atendimentoLog.EmailCliente)
                        descricaoLog = descricaoLog + "( Email do Cliente de [" + atendimentoLog.EmailCliente + "] para [" + atendimento.EmailCliente + "])";
                    if (atendimento.TelefoneCliente != atendimentoLog.TelefoneCliente)
                        descricaoLog = descricaoLog + "( Telefone do Cliente de [" + atendimentoLog.TelefoneCliente + "] para [" + atendimento.TelefoneCliente + "])";
                    if (atendimento.Celular != atendimentoLog.Celular)
                        descricaoLog = descricaoLog + "( Celular do Cliente de [" + atendimentoLog.Celular + "] para [" + atendimento.Celular + ")";
                    if (atendimento.ClienteAnonimo != atendimentoLog.ClienteAnonimo)
                        descricaoLog = descricaoLog + "( Cliente anonimo de [" + (atendimentoLog.ClienteAnonimo ? "S" : "N") + "] para [" + (atendimento.ClienteAnonimo ? "S" : "N") + "])";

                    #endregion

                    #region Responsavel
                    if (atendimento.IdUsuarioResponsavel != atendimentoLog.IdUsuarioResponsavel)
                    {
                        Usuario _usuario = new UsuarioDAO().ConsultaUsuarioPorID(atendimentoLog.IdUsuarioResponsavel);
                        de = _usuario.UsuarioAcesso;
                        _usuario = new UsuarioDAO().ConsultaUsuarioPorID(atendimento.IdUsuarioResponsavel);
                        para = _usuario.UsuarioAcesso;

                        descricaoLog = descricaoLog + " ( Responsável de [" + de + "] para [" + para + "])";
                    }
                    #endregion

                    _log.Descricao = descricaoLog;
                    _log.IdAtendimento = atendimento.Id;
                    _log.IdUsuario = Publicas._idUsuario;

                    #endregion

                    query.Clear();
                    query.Append("Update niff_chm_atendimento");

                    query.Append("   Set IdTpAtendimento = " + atendimento.IdTipoAtendimento);
                    query.Append("     , ClienteAnonimo = '" + (atendimento.ClienteAnonimo ? "S" : "N") + "'");
                    query.Append("     , TextoAtendimento = :textoAtendimento");// + atendimento.TextoAtendimento + "'");
                    query.Append("     , TextoResposta = :textoResposta");// + atendimento.TextoResposta + "'");

                    if (Publicas._telaQueChamouPesquisaDeAtendimento == Publicas.TelaPesquisaSAC.Finaliza)
                    {
                        query.Append("     , DataFinalizado = sysdate");
                        query.Append("     , IdUsuarioFinaliza = " + Publicas._idUsuario);
                    }

                    if (!string.IsNullOrEmpty(atendimento.MotivoCancelamento))
                    {
                        query.Append("     , DataCancelado = sysdate");
                        query.Append("     , IdUsuarioCancela = " + Publicas._idUsuario);
                    }

                    if (!String.IsNullOrEmpty(atendimento.MotivoSatisfacao))
                        query.Append("     , DataSatisfacao = sysdate");
                    
                    if (!string.IsNullOrEmpty(atendimento.TextoResposta) && Publicas._telaQueChamouPesquisaDeAtendimento == Publicas.TelaPesquisaSAC.Responde)
                    {
                        query.Append("     , DataResposta = sysdate");
                        query.Append("     , IdUsuarioRetorno = " + Publicas._idUsuario);
                    }

                    query.Append("     , Status = '" + (atendimento.Status == Publicas.StatusAtendimento.Ativo ? "A" :
                                           (atendimento.Status == Publicas.StatusAtendimento.Cancelado ? "C" :
                                            (atendimento.Status == Publicas.StatusAtendimento.Respondido ? "R" : "F"))) + "'");

                    query.Append("     , Retorno = '" + (atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Email ? "E" :
                       (atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Fax ? "F" :
                        (atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Telefone ? "T" : "N"))) + "'");

                    query.Append("     , Retornou = '" + (atendimento.Retornou ? "S" : "N") + "'");
                    query.Append("     , AguardaSatisfacaoCliente = '" + (atendimento.AguardaSatisfacaoCliente ? "S" : "N") + "'");

                    query.Append("     , MotivoRetorno = :MotivoRetorno"); 
                    query.Append("     , MotivoCancelamento = :MotivoCancelamento");

                    query.Append("     , Satisfacao = '" + (atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.Bom ? "B" :
                       (atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.MuitoBom ? "M" :
                       (atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.Regular ? "G" :
                       (atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.Excelente ? "E" :
                       (atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.Ruim ? "R" :
                       "S"))))) + "'");
                    query.Append("     , MotivoSatisfacao = :MotivoSatisfacao");// + atendimento.MotivoSatisfacao + "'");

                    query.Append("     , codigolinha = '" + atendimento.CodigoLinha + "'");
                    query.Append("     , IdEmtu = " + atendimento.IdEmtu);
                    
                    query.Append("     , Nome = '" + atendimento.NomeCliente + "'");
                    query.Append("     , RG = '" + atendimento.RGCliente + "'");
                    query.Append("     , CPF = '" + atendimento.CPFCliente + "'");
                    query.Append("     , Endereco = '" +atendimento.EnderecoCliente + "'");
                    query.Append("     , Cidade = '" + atendimento.CidadeCliente + "'");
                    query.Append("     , UF = '" + atendimento.UFCliente + "'");
                    query.Append("     , Email = '" + atendimento.EmailCliente + "'");
                    query.Append("     , Telefone = " + atendimento.TelefoneCliente);
                    query.Append("     , Celular = " + atendimento.Celular);
                    query.Append("     , IdUsuarioResponsavel = " + atendimento.IdUsuarioResponsavel);
                    query.Append("     , Origem = '" + (atendimento.Origem == Publicas.OrigemAtendimento.Email ? "E" :
                                           (atendimento.Origem == Publicas.OrigemAtendimento.Internet ? "S" :
                                            (atendimento.Origem == Publicas.OrigemAtendimento.JornalRadio ? "J" :
                                             (atendimento.Origem == Publicas.OrigemAtendimento.OrgaoGestor ? "C" :
                                              (atendimento.Origem == Publicas.OrigemAtendimento.Pessoalmente ? "P" : "T"))))) + "'");

                    query.Append("     , Situacao = '" + (atendimento.Situacao == Publicas.SituacaoAtendimento.Cancelado ? "FC" :
                                          (atendimento.Situacao == Publicas.SituacaoAtendimento.EnviadoAoColaborador ? "EC" :
                                            (atendimento.Situacao == Publicas.SituacaoAtendimento.EnviadoAoFinalizador ? "EF" :
                                              (atendimento.Situacao == Publicas.SituacaoAtendimento.ManterComAtendente ? "MA" :
                                              (atendimento.Situacao == Publicas.SituacaoAtendimento.AguardandoRetornoAoCliente ? "AC" :
                                              (atendimento.Situacao == Publicas.SituacaoAtendimento.AguardandoSatisfacao ? "AS" : "FF")))))) + "'");

                    if (atendimento.TextoRetornoAoCliente != "")
                    {
                        query.Append("     , RespostaAoCliente = :TextoRetornoAoCliente");// + atendimento.TextoRetornoAoCliente + "'");

                        if (Publicas._telaQueChamouPesquisaDeAtendimento == Publicas.TelaPesquisaSAC.Retorno ||
                            ((Publicas._telaQueChamouPesquisaDeAtendimento == Publicas.TelaPesquisaSAC.Finaliza && 
                              atendimento.Status == Publicas.StatusAtendimento.Finalizado) && 
                             (atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Email || 
                              atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Nenhum) ))
                        {
                            query.Append("     , DataRetornoAoCliente = sysdate ");
                            query.Append("     , IdUsuarioRetornouAoCliente = " + Publicas._idUsuario);
                        }
                    }
                    if (atendimento.CodIntFunc != 0)
                        query.Append("     , CODFUNC = " + atendimento.CodIntFunc);

                    query.Append("     , Cod_seq_Secao = " + atendimento.CodSeqSecao);

                    query.Append("     , ReclamacaoProcede = '" + atendimento.ReclamacaoProcede + "'");
                    query.Append(" Where idAtendimento = " + atendimento.Id);
                    _idAtendimento = atendimento.Id;
                }

                if (anexos != null)
                    anexos.ForEach(w => w.IdAtendimento = _idAtendimento);

                try
                {
                    parametro = new OracleParameter();
                    parametro.ParameterName = ":textoAtendimento";
                    parametro.Value = atendimento.TextoAtendimento.Replace("'", "");
                    parametro.OracleType = OracleType.VarChar;
                    parametros.Add(parametro);
                }
                catch { }
                try
                {
                    parametro = new OracleParameter();
                    parametro.ParameterName = ":textoResposta";
                    parametro.Value = atendimento.TextoResposta.Replace("'", "");
                    parametro.OracleType = OracleType.VarChar;
                    parametros.Add(parametro);
                }
                catch { }
                parametro = new OracleParameter();
                parametro.ParameterName = ":MotivoRetorno";
                try
                {
                    parametro.Value = atendimento.MotivoRetorno.Replace("'", "");
                }
                catch
                {
                    if (atendimento.MotivoRetorno != "" && atendimento.MotivoRetorno != null)
                        parametro.Value = atendimento.MotivoRetorno;
                    else
                        parametro.Value = "";
                }

                parametro.OracleType = OracleType.VarChar;
                parametros.Add(parametro);
                parametro = new OracleParameter();
                parametro.ParameterName = ":MotivoCancelamento";
                try
                {
                    parametro.Value = atendimento.MotivoCancelamento.Replace("'", "");
                }
                catch
                {
                    if (atendimento.MotivoCancelamento != "" && atendimento.MotivoCancelamento != null)
                        parametro.Value = atendimento.MotivoCancelamento;
                    else
                        parametro.Value = "";
                }
                parametro.OracleType = OracleType.VarChar;
                parametros.Add(parametro);

                if (atendimento.TextoRetornoAoCliente != "" && atendimento.TextoRetornoAoCliente != null)
                {
                    parametro = new OracleParameter();
                    parametro.ParameterName = ":TextoRetornoAoCliente";
                    try
                    {
                        parametro.Value = atendimento.TextoRetornoAoCliente.Replace("'", "");
                    }
                    catch
                    {
                        if (atendimento.TextoRetornoAoCliente != "" && atendimento.TextoRetornoAoCliente != null)
                            parametro.Value = atendimento.TextoRetornoAoCliente;
                        else
                            parametro.Value = "";
                    }
                    parametro.OracleType = OracleType.VarChar;
                    parametros.Add(parametro);
                }

                parametro = new OracleParameter();
                parametro.ParameterName = ":MotivoSatisfacao";
                try
                {
                    parametro.Value = atendimento.MotivoSatisfacao.Replace("'", "");
                }
                catch
                {
                    if (atendimento.MotivoSatisfacao != "" && atendimento.MotivoSatisfacao != null)
                        parametro.Value = atendimento.MotivoSatisfacao;
                    else
                        parametro.Value = "";
                }

                parametro.OracleType = OracleType.VarChar;
                parametros.Add(parametro);

                if (!sessao.ExecuteSqlTransaction(query.ToString(),parametros.ToArray()))
                    return false;

                if (!Grava(anexos))
                    return false;

                return new LogSACDAO().Gravar(_log);
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool Exclui(Atendimento atendimento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (atendimento.Id != 0)
                {
                    query.Append("Delete niff_chm_atendimento");
                    query.Append(" Where idAtendimento = " + atendimento.Id);
                }

                return sessao.ExecuteSqlTransaction(query.ToString());
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public int Proximo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Max(IdAtendimento) + 1 next From niff_chm_atendimento");
                Query executar = sessao.CreateQuery(query.ToString());

                atendimentoReader = executar.ExecuteQuery();

                using (atendimentoReader)
                {
                    if (atendimentoReader.Read())
                        retorno = Convert.ToInt32(atendimentoReader["next"].ToString());
                }
                return retorno;
            }
            catch
            {
                return retorno;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public string ProximoCodigo(Publicas.TipoCalculoCodigoSAC tipo, string separador, DateTime data, int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            string retorno = "1";
            int proximo = 0;

            try
            {
                switch (tipo)
                {
                    case Publicas.TipoCalculoCodigoSAC.Ano:
                        query.Append("Select Nvl(Max(To_number(SubStr(Codigo, 6,length(codigo)))),0) +1 Next From niff_chm_atendimento");
                        query.Append(" Where Codigo like '" + data.Year.ToString("0000") + "%'");
                        break;
                    case Publicas.TipoCalculoCodigoSAC.EmpresaAno:
                        query.Append("Select nvl(Max(To_number(SubStr(Codigo, 9,length(codigo)))),0) +1 Next From niff_chm_atendimento");
                        query.Append(" Where Codigo like '" + Publicas._usuario.IdEmpresa.ToString("000") + "' || " + data.Year.ToString("0000") + "%'");
                        break;
                    case Publicas.TipoCalculoCodigoSAC.AnoMes:
                        query.Append("Select Nvl(Max(To_number(SubStr(Codigo, 8,length(codigo)))),0) +1 Next From niff_chm_atendimento");
                        query.Append(" Where Codigo like '" + data.Year.ToString("0000") + data.Month.ToString("00") + "%'");
                        break;
                    case Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes:
                        query.Append("Select Nvl(Max(To_number(SubStr(Codigo, 11,length(codigo)))),0) +1 Next From niff_chm_atendimento");
                        query.Append(" Where Codigo like '" + Publicas._usuario.IdEmpresa.ToString("000") + "' || " + data.Year.ToString("0000") + data.Month.ToString("00") + "%'");
                        break;
                    case Publicas.TipoCalculoCodigoSAC.Sequencial:
                        query.Append("Select nvl(Max(To_number(codigo)),0) + 1 Next From niff_chm_atendimento");
                        break;
                }
                query.Append(" and IdEmpresa = " + empresa);

                Query executar = sessao.CreateQuery(query.ToString());

                atendimentoReader = executar.ExecuteQuery();

                using (atendimentoReader)
                {
                    if (atendimentoReader.Read())
                    {
                        proximo = Convert.ToInt32(atendimentoReader["next"].ToString());
                        switch (tipo)
                        {
                            case Publicas.TipoCalculoCodigoSAC.Ano:
                                retorno = data.Year.ToString("0000") + separador + proximo.ToString("000000");
                                break;
                            case Publicas.TipoCalculoCodigoSAC.EmpresaAno:
                                retorno = Publicas._usuario.IdEmpresa.ToString("000") + data.Year.ToString("0000") + separador + proximo.ToString("000000");
                                break;
                            case Publicas.TipoCalculoCodigoSAC.AnoMes:
                                retorno = data.Year.ToString("0000") + data.Month.ToString("00") + separador + proximo.ToString("000000");
                                break;
                            case Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes:
                                retorno = Publicas._usuario.IdEmpresa.ToString("000") + data.Year.ToString("0000") + data.Month.ToString("00") + separador + proximo.ToString("00000");
                                break;
                            case Publicas.TipoCalculoCodigoSAC.Sequencial:
                                retorno = proximo.ToString("000000");
                                break;
                                
                        }
                    }
                }
                return retorno;
            }
            catch
            {
                return retorno;
            }
            finally
            {
                sessao.Desconectar();
            }
        }
        
        public List<Classes.Atendimento.Anexos> Listar(int IdAtendimento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Atendimento.Anexos > _lista = new List<Atendimento.Anexos>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select a.id, a.idatendimento, a.anexo, a.NomeArquivo");
                query.Append("  from niff_chm_AnexosAtendimento a       ");
                query.Append(" Where a.IdAtendimento = " + IdAtendimento);
                Query executar = sessao.CreateQuery(query.ToString());

                anexosReader = executar.ExecuteQuery();

                using (anexosReader)
                {
                    while (anexosReader.Read())
                    {
                        Atendimento.Anexos _anexo = new Atendimento.Anexos();
                        _anexo.Id = Convert.ToInt32(anexosReader["id"].ToString());
                        _anexo.IdAtendimento = Convert.ToInt32(anexosReader["idatendimento"].ToString());
                        _anexo.NomeArquivo = anexosReader["NomeArquivo"].ToString();
                        try
                        {
                            _anexo.Anexo = (byte[])(anexosReader["anexo"]);
                        }
                        catch { }

                        _lista.Add(_anexo);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public bool Grava(List<Atendimento.Anexos> anexos)
        {
            if (anexos == null)
                return true;

            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            OracleParameter parametro = new OracleParameter();
            List<OracleParameter> parametros = new List<OracleParameter>();
            bool retorno = true;

            foreach (Atendimento.Anexos item in anexos)
            {
                if (!item.Existe)
                {
                    query.Clear();
                    query.Append("Insert into niff_chm_AnexosAtendimento ");
                    query.Append(" (id, idatendimento, anexo, NomeArquivo)       ");
                    query.Append(" Values( (Select nvl(max(id),0)+1 from niff_chm_AnexosAtendimento) ");
                    query.Append("       , " + item.IdAtendimento);

                    query.Append(", :panexo ");
                    parametros.Clear();
                    parametro.ParameterName = ":panexo";
                    parametro.Value = item.Anexo;
                    parametro.OracleType = OracleType.Blob;
                    parametros.Add(parametro);

                    query.Append(", '" + item.NomeArquivo + "')");

                    try
                    {
                        retorno = sessao.ExecuteSqlTransaction(query.ToString(), parametros.ToArray());
                    }
                    catch (Exception ex)
                    {
                        Publicas.mensagemDeErro = ex.Message;
                        retorno = false;
                        break;
                    }
                }
            }
            sessao.Desconectar();
            return retorno;
        }

        public bool Excluir(string NomeArquivo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            query.Clear();
            query.Append("Delete niff_chm_AnexosAtendimento ");
            query.Append(" Where NomeArquivo like '" + NomeArquivo + "%'");

            try
            {
                retorno = sessao.ExecuteSqlTransaction(query.ToString());
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                retorno = false;
            }
                        
            sessao.Desconectar();
            return retorno;
        }
    }
}
