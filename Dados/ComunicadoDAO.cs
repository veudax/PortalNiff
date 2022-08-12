using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ComunicadoDAO
    {
        IDataReader comunicadoReader;

        private StringBuilder ConsultaPadrao()
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select c.idcomunicado, c.dataabertura, c.dataconfirmacao, c.status, c.solicitante, c.idempresa, c.processo");
            query.Append("     , c.idvara, c.autor, c.tipoautor, c.cpfautor, c.pisautor, c.idtipo, c.motivooutros, c.reembolso ");
            query.Append("     , c.valorreembolso, c.seguro, c.valortotal, c.qtdparcela, c.notafiscal, c.valornotafiscal, c.observacao");
            query.Append("     , c.favorecido, c.cpffavorecido, c.agencia, c.conta, c.referencia, c.resumo, c.novoprocesso, c.idusuario");
            query.Append("     , c.emailenviado, c.centrocusto, c.vara, c.banco, c.tipo, c.parcelas, c.idusuarioaprovacao, c.datareprovacao ");
            query.Append("     , c.IdUsuarioReprovador, c.IdUsuarioCancela, c.TipoFavorecido, c.DataAlteracao, c.IdUsuarioAltera, c.IdUsuarioFinaliza ");
            query.Append("     , u.Nome usuarioAbertura, c.DataFinalizado, c.DataCancela, t.descricao NomeTipo, v.nome nomeVara");
            query.Append("     , ua.Nome UsuarioAprovador, ur.Nome UsuarioReprovador, uc.Nome UsuarioCancelador, ut.Nome UsuarioAlteracao");
            query.Append("     , uf.Nome UsuarioFinalizado, decode(ctb.CodCusto, Null, Null, ctb.Classcustoctb || ' - ' || ctb.desccusto) custo ");
            query.Append("     , e.codigoglobus || ' - ' || e.nomeabreviado empresa, c.MotivoCancelamento ");
            query.Append("  From niff_jur_comunicados c, ");
            query.Append("       niff_chm_Usuarios u, ");
            query.Append("       niff_chm_Usuarios ua, ");
            query.Append("       niff_chm_Usuarios ur, ");
            query.Append("       niff_chm_Usuarios uc, ");
            query.Append("       niff_chm_Usuarios uf, ");
            query.Append("       niff_chm_Usuarios ut, ");
            query.Append("       niff_jur_Vara v, ");
            query.Append("       Niff_Jur_Tipo t, ");
            query.Append("       ctbcusto ctb, ");
            query.Append("       Niff_chm_Empresas e");
            query.Append(" Where u.IdUsuario(+) = c.IdUsuario ");
            query.Append("   And ua.IdUsuario(+) = c.Idusuarioaprovacao ");
            query.Append("   And ur.IdUsuario(+) = c.IdUsuarioReprovador ");
            query.Append("   And uc.IdUsuario(+) = c.IdUsuarioCancela ");
            query.Append("   And ut.IdUsuario(+) = c.IdUsuarioAltera ");
            query.Append("   And uf.IdUsuario(+) = c.IdUsuarioFinaliza ");
            query.Append("   And v.Idvara(+) = c.idvara ");
            query.Append("   And t.Idtipo(+) = c.Idtipo ");
            query.Append("   And ctb.codcusto(+) = c.centrocusto");
            query.Append("   And e.idempresa = c.Idempresa");
            return query;
        }

        private List<Comunicado> Popular(IDataReader comunicadoReader)
        {
            List<Comunicado> _lista = new List<Comunicado>();

            try
            {
                using (comunicadoReader)
                {
                    while (comunicadoReader.Read())
                    {
                        Comunicado _comunicado = new Comunicado();

                        _comunicado.Existe = true;
                        _comunicado.Id = Convert.ToInt32(comunicadoReader["idcomunicado"].ToString());
                        _comunicado.IdEmpresa = Convert.ToInt32(comunicadoReader["idEmpresa"].ToString());
                        try
                        {
                            _comunicado.IdUsuario = Convert.ToInt32(comunicadoReader["idUsuario"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdUsuarioAprovacao = Convert.ToInt32(comunicadoReader["idusuarioaprovacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdUsuarioCancelado = Convert.ToInt32(comunicadoReader["IdUsuarioCancela"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdUsuarioReprovador = Convert.ToInt32(comunicadoReader["IdUsuarioReprovador"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdUsuarioFinaliza = Convert.ToInt32(comunicadoReader["IdUsuarioFinaliza"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdUsuarioAltera = Convert.ToInt32(comunicadoReader["IdUsuarioAltera"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdVara = Convert.ToInt32(comunicadoReader["IdVara"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdTipo = Convert.ToInt32(comunicadoReader["IdTipo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.QuantidadeDeParcelas = Convert.ToInt32(comunicadoReader["qtdparcela"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.CentroDeCustos = Convert.ToInt32(comunicadoReader["centrocusto"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Referencia = Convert.ToInt32(comunicadoReader["referencia"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Abertura = Convert.ToDateTime(comunicadoReader["dataabertura"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Confirmacao = Convert.ToDateTime(comunicadoReader["dataconfirmacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Reprovacao = Convert.ToDateTime(comunicadoReader["datareprovacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Alteracao = Convert.ToDateTime(comunicadoReader["DataAlteracao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Finalizado = Convert.ToDateTime(comunicadoReader["DataFinalizado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Cancelamento = Convert.ToDateTime(comunicadoReader["DataCancela"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.CPFDoAutor = Convert.ToDecimal(comunicadoReader["CPFAutor"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.CPFFavorecido = Convert.ToDecimal(comunicadoReader["cpffavorecido"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.ValorDoReembolso = Convert.ToDecimal(comunicadoReader["valorreembolso"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Total = Convert.ToDecimal(comunicadoReader["valortotal"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.ValorDescontoNotaFiscal = Convert.ToDecimal(comunicadoReader["valornotafiscal"].ToString());
                        }
                        catch { }

                        _comunicado.Empresa = comunicadoReader["empresa"].ToString();

                        _comunicado.Status = (comunicadoReader["status"].ToString() == "N" ? Publicas.StatusComunicado.Novo :
                            (comunicadoReader["status"].ToString() == "A" ? Publicas.StatusComunicado.Aprovado :
                            (comunicadoReader["status"].ToString() == "R" ? Publicas.StatusComunicado.Reprovado :
                            (comunicadoReader["status"].ToString() == "C" ? Publicas.StatusComunicado.Cancelado :
                            (comunicadoReader["status"].ToString() == "L" ? Publicas.StatusComunicado.Alterado :
                            Publicas.StatusComunicado.Finalizado)))));

                        try
                        {
                            if (comunicadoReader["idUsuario"].ToString() == "")
                                _comunicado.Solicitante = comunicadoReader["solicitante"].ToString();
                            else
                                _comunicado.Solicitante = comunicadoReader["usuarioAbertura"].ToString();
                        }
                        catch
                        {
                            try
                            {
                                if (comunicadoReader["solicitante"].ToString() != "")
                                    _comunicado.Solicitante = comunicadoReader["solicitante"].ToString();
                            }
                            catch { }

                        }

                        try
                        {
                            if (comunicadoReader["vara"].ToString() != "")
                                _comunicado.Vara = comunicadoReader["vara"].ToString();
                            else
                                _comunicado.Vara = comunicadoReader["nomeVara"].ToString();
                        }
                        catch { }

                        try
                        {
                            if (comunicadoReader["Tipo"].ToString() != "")
                                _comunicado.Tipo = comunicadoReader["Tipo"].ToString();
                            else
                                _comunicado.Tipo = comunicadoReader["nomeTipo"].ToString();
                        }
                        catch { }

                        try
                        {
                            if (comunicadoReader["Custo"].ToString() != "")
                                _comunicado.Custo = comunicadoReader["custo"].ToString();
                        }
                        catch { }

                        _comunicado.Processo = comunicadoReader["processo"].ToString();
                        try
                        {
                            _comunicado.Autor = comunicadoReader["Autor"].ToString();
                        }
                        catch { }

                        try
                        {
                            _comunicado.TipoAutor = (comunicadoReader["TipoAutor"].ToString() == "J" ? Publicas.TipoPessoa.Juridica : Publicas.TipoPessoa.Fisica);
                        }
                        catch { }

                        try
                        {
                            _comunicado.PisDoAutor = comunicadoReader["PisAutor"].ToString();
                        }
                        catch { }

                        try
                        {
                            _comunicado.MotivoTipoOutros = comunicadoReader["MotivoOutros"].ToString();
                        }
                        catch { }

                        _comunicado.Reembolso = comunicadoReader["Reembolso"].ToString() == "S";
                        _comunicado.Seguro = comunicadoReader["Seguro"].ToString() == "S";
                        _comunicado.NotaFiscal = comunicadoReader["NotaFiscal"].ToString() == "S";
                        _comunicado.EmailEnviado = comunicadoReader["emailenviado"].ToString();

                        try
                        {
                            _comunicado.Observacoes = comunicadoReader["observacao"].ToString();
                        }
                        catch { }

                        try
                        {
                            _comunicado.Favorecido = comunicadoReader["Favorecido"].ToString();
                        }
                        catch { }

                        try
                        {
                            _comunicado.Banco = comunicadoReader["Banco"].ToString();
                        }
                        catch { }

                        try
                        {
                            _comunicado.Agencia = comunicadoReader["Agencia"].ToString();
                        }
                        catch { }

                        try
                        {
                            _comunicado.Conta = comunicadoReader["Conta"].ToString();
                        }
                        catch { }

                        _comunicado.TipoFavorecido = (comunicadoReader["TipoFavorecido"].ToString() == "J" ? Publicas.TipoPessoa.Juridica : Publicas.TipoPessoa.Fisica);
                        _comunicado.Resumo = comunicadoReader["Resumo"].ToString();

                        try
                        {
                            _comunicado.NovoProcesso = comunicadoReader["NovoProcesso"].ToString();
                        }
                        catch { }

                        _comunicado.UsuarioAprovador = comunicadoReader["UsuarioAprovador"].ToString();
                        _comunicado.UsuarioReprovador = comunicadoReader["UsuarioReprovador"].ToString();
                        _comunicado.UsuarioCancelador = comunicadoReader["UsuarioCancelador"].ToString();
                        _comunicado.UsuarioAlterador = comunicadoReader["UsuarioAlteracao"].ToString();
                        _comunicado.UsuarioFinaliza = comunicadoReader["UsuarioFinalizado"].ToString();
                        _comunicado.MotivoCancelamento = comunicadoReader["MotivoCancelamento"].ToString();
                        _lista.Add(_comunicado);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            return _lista;
        }

        public List<Comunicado> Listar(int idEmpresa, int ano, Publicas.StatusComunicado _status, string processo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<Comunicado> _lista = new List<Comunicado>();

            try
            {
                query.Append(ConsultaPadrao());
                if (idEmpresa != 0)
                {
                    query.Append("   And c.IdEmpresa = " + idEmpresa);

                    if (!string.IsNullOrEmpty(processo))
                        query.Append("   And c.Processo = '" + processo + "'");
                }

                if (_status != Publicas.StatusComunicado.Todos)
                {
                    if (ano == 0)
                        query.Append("   And c.Dataabertura Between last_day(add_Months(Trunc(Sysdate),-13))+1 And Sysdate");
                    else
                    {
                        DateTime _inicio = new DateTime(ano, 01, 01);
                        DateTime _fim = new DateTime(ano, 12, 31);
                        query.Append("   And c.Dataabertura Between To_Date('" + _inicio.ToShortDateString() + " 00:00', 'dd/mm/yyyy hh24:mi') And To_Date('" + _fim.ToShortDateString() + " 23:59', 'dd/mm/yyyy hh24:mi') ");
                    }
                }
                else
                {
                    if (ano != 0)
                    {
                        DateTime _inicio = new DateTime(ano-1, 01, 01);
                        DateTime _fim = new DateTime(ano, 12, 31);
                        query.Append("   And c.Dataabertura Between To_Date('" + _inicio.ToShortDateString() + " 00:00', 'dd/mm/yyyy hh24:mi') And To_Date('" + _fim.ToShortDateString() + " 23:59', 'dd/mm/yyyy hh24:mi') ");
                    }
                }

                switch (_status)
                {
                    case Publicas.StatusComunicado.Novo:
                        query.Append("   And Status in ('N','L')");
                        break;
                    case Publicas.StatusComunicado.Reprovado:
                        query.Append("   And Status = 'R'");
                        break;
                    case Publicas.StatusComunicado.Aprovado:
                        query.Append("   And Status = 'A'");
                        break;
                    case Publicas.StatusComunicado.Finalizado:
                        query.Append("   And Status = 'F'");
                        break;
                    case Publicas.StatusComunicado.Cancelado:
                        query.Append("   And Status = 'C'");
                        break;
                }

                Query executar = sessao.CreateQuery(query.ToString());

                comunicadoReader = executar.ExecuteQuery();

                _lista = Popular(comunicadoReader);
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

        public Comunicado Consulta(int id, int empresa, string processo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Comunicado _comunicado = new Comunicado();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append(ConsultaPadrao());
                if (id != 0)
                    query.Append("   And c.IdComunicado = " + id);

                if (empresa != 0)
                {
                    query.Append("   And c.IdEmpresa = " + empresa);
                    query.Append("   And c.Processo = '" + processo + "'");
                }

                Query executar = sessao.CreateQuery(query.ToString());

                comunicadoReader = executar.ExecuteQuery();

                using (comunicadoReader)
                {
                    if (comunicadoReader.Read())
                    {
                        
                        _comunicado.Existe = true;
                        _comunicado.Id = Convert.ToInt32(comunicadoReader["idcomunicado"].ToString());
                        _comunicado.IdEmpresa = Convert.ToInt32(comunicadoReader["idEmpresa"].ToString());
                        try
                        {
                            _comunicado.IdUsuario = Convert.ToInt32(comunicadoReader["idUsuario"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdUsuarioAprovacao = Convert.ToInt32(comunicadoReader["idusuarioaprovacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdUsuarioCancelado = Convert.ToInt32(comunicadoReader["IdUsuarioCancela"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdUsuarioReprovador = Convert.ToInt32(comunicadoReader["IdUsuarioReprovador"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdUsuarioFinaliza = Convert.ToInt32(comunicadoReader["IdUsuarioFinaliza"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdUsuarioAltera = Convert.ToInt32(comunicadoReader["IdUsuarioAltera"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdVara = Convert.ToInt32(comunicadoReader["IdVara"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.IdTipo = Convert.ToInt32(comunicadoReader["IdTipo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.QuantidadeDeParcelas = Convert.ToInt32(comunicadoReader["qtdparcela"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.CentroDeCustos = Convert.ToInt32(comunicadoReader["centrocusto"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Referencia = Convert.ToInt32(comunicadoReader["referencia"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Abertura = Convert.ToDateTime(comunicadoReader["dataabertura"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Confirmacao = Convert.ToDateTime(comunicadoReader["dataconfirmacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Reprovacao = Convert.ToDateTime(comunicadoReader["datareprovacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Alteracao = Convert.ToDateTime(comunicadoReader["DataAlteracao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Finalizado = Convert.ToDateTime(comunicadoReader["DataFinalizado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Cancelamento = Convert.ToDateTime(comunicadoReader["DataCancela"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.CPFDoAutor = Convert.ToDecimal(comunicadoReader["CPFAutor"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.CPFFavorecido = Convert.ToDecimal(comunicadoReader["cpffavorecido"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.ValorDoReembolso = Convert.ToDecimal(comunicadoReader["valorreembolso"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.Total = Convert.ToDecimal(comunicadoReader["valortotal"].ToString());
                        }
                        catch { }

                        try
                        {
                            _comunicado.ValorDescontoNotaFiscal = Convert.ToDecimal(comunicadoReader["valornotafiscal"].ToString());
                        }
                        catch { }

                        _comunicado.Empresa = comunicadoReader["empresa"].ToString();
                        _comunicado.Status = (comunicadoReader["status"].ToString() == "N" ? Publicas.StatusComunicado.Novo :
                            (comunicadoReader["status"].ToString() == "A" ? Publicas.StatusComunicado.Aprovado :
                            (comunicadoReader["status"].ToString() == "R" ? Publicas.StatusComunicado.Reprovado :
                            (comunicadoReader["status"].ToString() == "C" ? Publicas.StatusComunicado.Cancelado :
                            (comunicadoReader["status"].ToString() == "L" ? Publicas.StatusComunicado.Alterado :
                            Publicas.StatusComunicado.Finalizado)))));

                        if (comunicadoReader["idUsuario"].ToString() == "")
                            _comunicado.Solicitante = comunicadoReader["solicitante"].ToString();
                        else
                            _comunicado.Solicitante = comunicadoReader["usuarioAbertura"].ToString();

                        try
                        {
                            if (comunicadoReader["vara"].ToString() != "")
                                _comunicado.Vara = comunicadoReader["vara"].ToString();
                            else
                                _comunicado.Vara = comunicadoReader["nomeVara"].ToString();
                        }
                        catch { }

                        try
                        {
                            if (comunicadoReader["Tipo"].ToString() != "")
                                _comunicado.Tipo = comunicadoReader["Tipo"].ToString();
                            else
                                _comunicado.Tipo = comunicadoReader["nomeTipo"].ToString();
                        }
                        catch { }

                        try
                        {
                            if (comunicadoReader["Custo"].ToString() != "")
                                _comunicado.Custo = comunicadoReader["custo"].ToString();
                        }
                        catch { }

                        _comunicado.Processo = comunicadoReader["processo"].ToString();
                        _comunicado.Autor = comunicadoReader["Autor"].ToString();
                        _comunicado.TipoAutor = (comunicadoReader["TipoAutor"].ToString() == "J" ? Publicas.TipoPessoa.Juridica : Publicas.TipoPessoa.Fisica);
                        _comunicado.PisDoAutor = comunicadoReader["PisAutor"].ToString();
                        _comunicado.MotivoTipoOutros = comunicadoReader["MotivoOutros"].ToString();

                        _comunicado.Reembolso = comunicadoReader["Reembolso"].ToString() == "S";
                        _comunicado.Seguro = comunicadoReader["Seguro"].ToString() == "S";
                        _comunicado.NotaFiscal = comunicadoReader["NotaFiscal"].ToString() == "S";
                        _comunicado.EmailEnviado = comunicadoReader["emailenviado"].ToString();

                        _comunicado.Observacoes = comunicadoReader["observacao"].ToString();
                        _comunicado.Favorecido = comunicadoReader["Favorecido"].ToString();
                        _comunicado.Banco = comunicadoReader["Banco"].ToString();
                        _comunicado.Agencia = comunicadoReader["Agencia"].ToString();
                        _comunicado.Conta = comunicadoReader["Conta"].ToString();
                        _comunicado.TipoFavorecido = (comunicadoReader["TipoFavorecido"].ToString() == "J" ? Publicas.TipoPessoa.Juridica : Publicas.TipoPessoa.Fisica);
                        _comunicado.Resumo = comunicadoReader["Resumo"].ToString();
                        _comunicado.NovoProcesso = comunicadoReader["NovoProcesso"].ToString();

                        _comunicado.UsuarioAprovador = comunicadoReader["UsuarioAprovador"].ToString();
                        _comunicado.UsuarioReprovador = comunicadoReader["UsuarioReprovador"].ToString();
                        _comunicado.UsuarioCancelador = comunicadoReader["UsuarioCancelador"].ToString();
                        _comunicado.UsuarioAlterador = comunicadoReader["UsuarioAlteracao"].ToString();
                        _comunicado.UsuarioFinaliza = comunicadoReader["UsuarioFinalizado"].ToString();
                        _comunicado.MotivoCancelamento = comunicadoReader["MotivoCancelamento"].ToString();
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
            return _comunicado;
        }
                
        public List<int> Datas()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<int> _datas = new List<int>();

            try
            {
                query.Append("Select Distinct ano ");
                query.Append("  From ( Select Distinct To_char(dataAbertura,'yyyy') ano ");
                query.Append("           From niff_jur_comunicados ");
                query.Append("          Union ALL");
                query.Append("         Select To_char(sysDate,'yyyy') ano ");
                query.Append("           From dual )");
                query.Append("Order By ano Desc");

                Query executar = sessao.CreateQuery(query.ToString());

                comunicadoReader = executar.ExecuteQuery();

                using (comunicadoReader)
                {
                    while (comunicadoReader.Read())
                    {
                        _datas.Add(Convert.ToInt32(comunicadoReader["ano"].ToString()));
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

        public bool Aprovar(Comunicado _comunicado, List<ParcelasDoComunicado> _listaParcelas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;

            try
            {
                query.Clear();
                query.Append("Update Niff_Jur_Comunicados");
                query.Append("   set IdUsuarioAprovacao = " + Publicas._idUsuario);
                query.Append("     , DataConfirmacao = sysDate");
                query.Append("     , Status = 'A'");
                query.Append("     , idempresa = " + _comunicado.IdEmpresa);
                query.Append("     , idvara = " + _comunicado.IdVara);
                query.Append("     , autor = '" + _comunicado.Autor.Replace("'", "").Replace("\"", "").Trim() + "'");
                query.Append("     , tipoautor = '" + (_comunicado.TipoAutor == Publicas.TipoPessoa.Fisica ? "F" : "J") + "'");
                query.Append("     , cpfautor = " + _comunicado.CPFDoAutor);
                query.Append("     , pisautor = '" + _comunicado.PisDoAutor + "'");
                query.Append("     , idtipo = " + _comunicado.IdTipo);
                if (_comunicado.MotivoTipoOutros != "")
                    query.Append("     , motivooutros = '" + _comunicado.MotivoTipoOutros.Replace("'", "").Replace("\"", "").Trim() + "'");
                query.Append("     , reembolso = '" + (_comunicado.Reembolso ? "S" : "N") + "'");
                query.Append("     , valorreembolso = " + _comunicado.ValorDoReembolso.ToString().Replace(".", "").Replace(",", "."));
                query.Append("     , seguro = '" + (_comunicado.Seguro ? "S" : "N") + "'");
                query.Append("     , valortotal = " + _comunicado.Total.ToString().Replace(".", "").Replace(",", "."));
                query.Append("     , qtdparcela = " + _comunicado.QuantidadeDeParcelas);
                query.Append("     , notafiscal = '" + (_comunicado.NotaFiscal ? "S" : "N") + "'");
                query.Append("     , valornotafiscal = " + _comunicado.ValorDescontoNotaFiscal.ToString().Replace(".", "").Replace(",", "."));
                if (_comunicado.Observacoes != "")
                    query.Append("     , observacao = '" + _comunicado.Observacoes.Replace("'", "").Replace("\"", "").Trim() + "'");
                if (_comunicado.Favorecido != "")
                    query.Append("     , favorecido = '" + _comunicado.Favorecido.Replace("'", "").Replace("\"", "").Trim() + "'");
                query.Append("     , cpffavorecido = " + _comunicado.CPFFavorecido);
                query.Append("     , agencia = '" + _comunicado.Agencia + "'");
                query.Append("     , conta = '" + _comunicado.Conta + "'");
                query.Append("     , referencia = " + _comunicado.Referencia);
                query.Append("     , resumo = '" + _comunicado.Resumo.Replace("'", "").Replace("\"", "").Trim() + "'");
                query.Append("     , novoprocesso = '" + _comunicado.NovoProcesso + "'");
                query.Append("     , IdUsuarioAltera = " + Publicas._idUsuario);
                query.Append("     , centrocusto = " + _comunicado.CentroDeCustos);
                query.Append("     , banco = '" + _comunicado.Banco + "'");
                query.Append("     , tipofavorecido = '" + (_comunicado.TipoFavorecido == Publicas.TipoPessoa.Fisica ? "F" : "J") + "'");
                query.Append(" Where idcomunicado = " + _comunicado.Id);

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                _listaParcelas.ForEach(w => w.IdComunicado = _comunicado.Id);

                if (retorno)
                {
                    if (_comunicado.Existe)
                        retorno = new ParcelasDoComunicadoDAO().Excluir(_comunicado.Id);

                    if (retorno)
                        retorno = new ParcelasDoComunicadoDAO().Gravar(_listaParcelas);
                }
                return retorno;
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

        public bool Reprovar(int _id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Update Niff_Jur_Comunicados");
                query.Append("   set IdUsuarioReprovador = " + Publicas._idUsuario);
                query.Append("     , DataReprovacao = sysDate");
                query.Append("     , Status = 'R'");
                query.Append(" Where idcomunicado = " + _id);

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

        public bool Cancelar(int _id, string motivo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Update Niff_Jur_Comunicados");
                query.Append("   set IdUsuarioCancela = " + Publicas._idUsuario);
                query.Append("     , DataCancela = sysDate");
                query.Append("     , Status = 'C'");

                if (!string.IsNullOrEmpty(motivo))
                    query.Append("     , MotivoCancelamento = '" + motivo + "'");

                query.Append(" Where idcomunicado = " + _id);

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

        public bool Finalizar(int _id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Update Niff_Jur_Comunicados");
                query.Append("   set IdUsuarioFinaliza = " + Publicas._idUsuario);
                query.Append("     , DataFinalizado = sysDate");
                query.Append("     , Status = 'F'");
                query.Append(" Where idcomunicado = " + _id);

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

        public bool Gravar(Comunicado _comunicado, List<ParcelasDoComunicado> _listaParcelas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            Int32 Id = 1;

            try
            {
                if (!_comunicado.Existe)
                {
                    query.Clear();
                    query.Append("Select SQ_NIFF_JurIdComun.NextVal next from dual");
                    Query executar = sessao.CreateQuery(query.ToString());

                    comunicadoReader = executar.ExecuteQuery();

                    using (comunicadoReader)
                    {
                        if (comunicadoReader.Read())
                            Id = Convert.ToInt32(comunicadoReader["next"].ToString());
                    }

                    query.Clear();
                    query.Append("Insert into Niff_Jur_Comunicados");
                    query.Append(" (idcomunicado, dataabertura, status, solicitante, idempresa, processo, idvara,");
                    query.Append("  autor, tipoautor, cpfautor, pisautor, idtipo");
                    if (_comunicado.MotivoTipoOutros != "")
                        query.Append("  , motivooutros");
                    query.Append("  , reembolso, valorreembolso, seguro,");
                    query.Append("  valortotal, qtdparcela, notafiscal, valornotafiscal ");
                    if (_comunicado.Observacoes != "")
                        query.Append("  , observacao");
                    if (_comunicado.Favorecido != "")
                        query.Append("  , favorecido");
                    query.Append("  , cpffavorecido,");
                    query.Append("  agencia, conta, referencia, resumo, novoprocesso, idusuario, centrocusto, banco, tipofavorecido ) ");
                    query.Append(" Values ( " + Id );
                    query.Append(" , SysDate ");
                    query.Append(" , 'N'");
                    query.Append(" , '" + _comunicado.Solicitante + "'");
                    query.Append(" , " + _comunicado.IdEmpresa);
                    query.Append(" , '" + _comunicado.Processo.Trim() + "'");
                    query.Append(" , " + _comunicado.IdVara);
                    query.Append(" , '" + _comunicado.Autor.Replace("'", "").Replace("\"", "").Trim() + "'");
                    query.Append(" , '" + (_comunicado.TipoAutor == Publicas.TipoPessoa.Fisica ? "F" : "J") + "'");
                    query.Append(" , " + _comunicado.CPFDoAutor);
                    query.Append(" , '" + _comunicado.PisDoAutor + "'");
                    query.Append(" , " + _comunicado.IdTipo);
                    if (_comunicado.MotivoTipoOutros != "")
                        query.Append(" , '" + _comunicado.MotivoTipoOutros.Replace("'", "").Replace("\"", "").Trim() + "'");
                    query.Append(" , '" + (_comunicado.Reembolso ? "S" : "N") + "'");
                    query.Append(" , " + _comunicado.ValorDoReembolso.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" , '" + (_comunicado.Seguro ? "S" : "N") + "'");
                    query.Append(" , " + _comunicado.Total.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" , " + _comunicado.QuantidadeDeParcelas);
                    query.Append(" , '" + (_comunicado.NotaFiscal ? "S" : "N") + "'");
                    query.Append(" , " + _comunicado.ValorDescontoNotaFiscal.ToString().Replace(".", "").Replace(",", "."));
                    if (_comunicado.Observacoes != "")
                        query.Append(" , '" + _comunicado.Observacoes.Replace("'", "").Replace("\"", "").Trim() + "'");
                    if (_comunicado.Favorecido != "")
                        query.Append(" , '" + _comunicado.Favorecido.Replace("'", "").Replace("\"", "").Trim() + "'");
                    query.Append(" , " + _comunicado.CPFFavorecido );
                    query.Append(" , '" + _comunicado.Agencia + "'");
                    query.Append(" , '" + _comunicado.Conta + "'");
                    query.Append(" , " + _comunicado.Referencia);
                    query.Append(" , '" + _comunicado.Resumo.Replace("'", "").Replace("\"","").Trim() + "'");
                    query.Append(" , '" + _comunicado.NovoProcesso + "'");
                    query.Append(" , " + _comunicado.IdUsuario);
                    query.Append(" , " + _comunicado.CentroDeCustos);
                    query.Append(" , '" + _comunicado.Banco + "'");
                    query.Append(" , '" + (_comunicado.TipoFavorecido == Publicas.TipoPessoa.Fisica ? "F" : "J") + "'");
                    query.Append(" )"); 
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Jur_Comunicados");
                    query.Append("   set status = 'L'");
                    query.Append("     , idempresa = " + _comunicado.IdEmpresa);
                    query.Append("     , idvara = " + _comunicado.IdVara);
                    query.Append("     , autor = '" + _comunicado.Autor.Replace("'", "").Replace("\"", "").Trim() + "'");
                    query.Append("     , tipoautor = '" + (_comunicado.TipoAutor == Publicas.TipoPessoa.Fisica ? "F" : "J") + "'");
                    query.Append("     , cpfautor = " + _comunicado.CPFDoAutor);
                    query.Append("     , pisautor = '" + _comunicado.PisDoAutor + "'");
                    query.Append("     , idtipo = " + _comunicado.IdTipo);
                    if (_comunicado.MotivoTipoOutros != "")
                        query.Append("     , motivooutros = '" + _comunicado.MotivoTipoOutros.Replace("'", "").Replace("\"", "").Trim() + "'");
                    query.Append("     , reembolso = '" + (_comunicado.Reembolso ? "S" : "N") + "'");
                    query.Append("     , valorreembolso = " + _comunicado.ValorDoReembolso.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , seguro = '" + (_comunicado.Seguro ? "S" : "N") + "'");
                    query.Append("     , valortotal = " + _comunicado.Total.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , qtdparcela = " + _comunicado.QuantidadeDeParcelas);
                    query.Append("     , notafiscal = '" + (_comunicado.NotaFiscal ? "S" : "N") + "'");
                    query.Append("     , valornotafiscal = " + _comunicado.ValorDescontoNotaFiscal.ToString().Replace(".", "").Replace(",", "."));
                    if (_comunicado.Observacoes != "")
                        query.Append("     , observacao = '" + _comunicado.Observacoes.Replace("'", "").Replace("\"", "").Trim() + "'");
                    if (_comunicado.Favorecido != "")
                        query.Append("     , favorecido = '" + _comunicado.Favorecido.Replace("'", "").Replace("\"", "").Trim() + "'");
                    query.Append("     , cpffavorecido = " + _comunicado.CPFFavorecido);
                    query.Append("     , agencia = '" + _comunicado.Agencia + "'");
                    query.Append("     , conta = '" + _comunicado.Conta + "'");
                    query.Append("     , referencia = " + _comunicado.Referencia);
                    query.Append("     , resumo = '" + _comunicado.Resumo.Replace("'", "").Replace("\"", "").Trim() + "'");
                    query.Append("     , novoprocesso = '" + _comunicado.NovoProcesso + "'");
                    query.Append("     , IdUsuarioAltera = " + Publicas._idUsuario);
                    query.Append("     , centrocusto = " + _comunicado.CentroDeCustos);
                    query.Append("     , banco = '" + _comunicado.Banco + "'");
                    query.Append("     , tipofavorecido = '" + (_comunicado.TipoFavorecido == Publicas.TipoPessoa.Fisica ? "F" : "J") + "'");
                    query.Append("     , DataAlteracao = SysDate");
                    query.Append(" Where idcomunicado = " + _comunicado.Id);

                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (!_comunicado.Existe)
                    _listaParcelas.ForEach(w => w.IdComunicado = Id);
                else
                    _listaParcelas.ForEach(w => w.IdComunicado = _comunicado.Id);

                Publicas._idComunicado = (_comunicado.Existe ? _comunicado.Id : Id);
                if (retorno)
                {
                    if (_comunicado.Existe)
                        retorno = new ParcelasDoComunicadoDAO().Excluir(_comunicado.Id);

                    if (retorno)
                        retorno = new ParcelasDoComunicadoDAO().Gravar(_listaParcelas);
                }
                return retorno;
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

        public bool AplicarStatusEmail(int id, string status)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Update Niff_Jur_Comunicados");
                query.Append("   set EmailEnviado = '" + status + "'");
                query.Append(" Where idcomunicado = " + id);

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
        
    }
}
