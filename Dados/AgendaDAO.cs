using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class AgendaDAO
    {
        IDataReader agendaReader;

        public List<Agenda> Listar(int usuario, Publicas.TipoAgenda tipoAgenda)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Agenda> _lista = new List<Agenda>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select a.IdAgenda, a.Data, a.DataFim, a.dataFimReal");
                query.Append("     , a.TipoAgenda, a.IdSala, a.CodigoVeic, a.LocalVisita");
                query.Append("     , a.IdEmpresa, a.IdUsuario, a.DiaTodo, a.Lembrar, a.DataLembrete");
                query.Append("     , a.Status, a.Texto");
                query.Append("     , s.descricao sala");
                query.Append("     , e.Nomeabreviado");
                query.Append("     , decode(a.codigoveic, Null, '', 'Placa ' || v.placaatualveic || ' Prefixo ' || v.prefixoveic ) veiculo");
                query.Append("     , SUBSTR(u.nome, 1, INSTR(u.nome, ' ') - 1) || Substr(u.nome, INSTR(u.nome, ' ', -1)) Nome");

                query.Append("  From NIFF_CHM_Agenda a, Niff_Chm_Salareuniao s, niff_chm_empresas e, frt_cadveiculos v, niff_chm_usuarios u");
                query.Append(" Where (a.IdUsuario = " + usuario);
                query.Append("    Or a.idAgenda in (Select idAgenda From Niff_Chm_Agendausu u Where idusuario =" + usuario + ") ");
                query.Append("    Or u.Iddepartamento = " + Publicas._usuario.IdDepartamento + ")");

                query.Append("   And s.Idsala(+) = a.Idsala");
                query.Append("   And e.Idempresa(+) = a.Idempresa");
                query.Append("   And v.codigoveic(+) = a.codigoveic");
                query.Append("   And u.Idusuario = a.Idusuario");

                if (tipoAgenda != Publicas.TipoAgenda.Todos)
                    query.Append("   and TipoAgenda = '" + (tipoAgenda == Publicas.TipoAgenda.Carro ? "C" :
                                                            (tipoAgenda == Publicas.TipoAgenda.Particular ? "P" :
                                                            (tipoAgenda == Publicas.TipoAgenda.Visita ? "V" :
                                                            (tipoAgenda == Publicas.TipoAgenda.SalaDeReuniao ? "S" :
                                                            (tipoAgenda == Publicas.TipoAgenda.TreinamentoInterno ? "I" :
                                                            (tipoAgenda == Publicas.TipoAgenda.TreinamentoExterno ? "E" :
                                                            (tipoAgenda == Publicas.TipoAgenda.Ferias ? "F" : "A"))))))) + "'");

                Query executar = sessao.CreateQuery(query.ToString());
                agendaReader = executar.ExecuteQuery();

                using (agendaReader)
                {
                    while (agendaReader.Read())
                    {
                        Agenda _agenda = new Agenda();
                        _agenda.IdAgenda = Convert.ToInt32(agendaReader["IdAgenda"].ToString());
                        _agenda.IdUsuario = Convert.ToInt32(agendaReader["IdUsuario"].ToString());

                        try
                        {
                            _agenda.IdEmpresa = Convert.ToInt32(agendaReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.IdSala = Convert.ToInt32(agendaReader["IdSala"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.CodigoVeiculo = Convert.ToInt32(agendaReader["CodigoVeic"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.Lembrar = Convert.ToInt32(agendaReader["Lembrar"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.Data = Convert.ToDateTime(agendaReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.DataFim = Convert.ToDateTime(agendaReader["DataFim"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.DataFimReal = Convert.ToDateTime(agendaReader["DataFimReal"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.DataLembrete = Convert.ToDateTime(agendaReader["DataLembrete"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.HoraInicio = Convert.ToDateTime(agendaReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.HoraFim = Convert.ToDateTime(agendaReader["DataFim"].ToString());
                        }
                        catch { }

                        _agenda.Descricao = agendaReader["Sala"].ToString();
                        _agenda.Placa = agendaReader["Veiculo"].ToString();
                        _agenda.NomeAbreviado = agendaReader["Nomeabreviado"].ToString();
                        _agenda.Nome = agendaReader["Nome"].ToString();

                        _agenda.Texto = agendaReader["Texto"].ToString();
                        _agenda.Local = agendaReader["LocalVisita"].ToString();

                        _agenda.DiaTodo = agendaReader["diaTodo"].ToString() == "S";
                        _agenda.Status = (agendaReader["Status"].ToString() == "A" ? Publicas.StatusAgenda.Ativo :
                                         (agendaReader["Status"].ToString() == "C" ? Publicas.StatusAgenda.Cancelado :
                                         (agendaReader["Status"].ToString() == "R" ? Publicas.StatusAgenda.Reservado :
                                         (agendaReader["Status"].ToString() == "S" ? Publicas.StatusAgenda.SolicitacaoCarro :
                                         Publicas.StatusAgenda.Finalizado))));

                        switch (agendaReader["TipoAgenda"].ToString())
                        {
                            case "S": _agenda.TipoAgenda = Publicas.TipoAgenda.SalaDeReuniao;
                                break;
                            case "C":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.Carro;
                                break;
                            case "I":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.TreinamentoInterno;
                                break;
                            case "E":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.TreinamentoExterno;
                                break;
                            case "P":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.Particular;
                                break;
                            case "V":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.Visita;
                                break;
                            case "F":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.Ferias;
                                break;
                            case "A":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.AtestadoMedico;
                                break;
                        }

                        _agenda.DescricaoTipoAgenda = Publicas.GetDescription(_agenda.TipoAgenda, "");
                        _agenda.Existe = true;

                        _lista.Add(_agenda);
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

        public List<Agenda> Consultar(DateTime dataInicio, DateTime dataFim, 
                                      Publicas.TipoAgenda tipoAgenda,
                                      bool diaTodo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Agenda> _lista = new List<Agenda>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select a.IdAgenda, a.Data, a.DataFim, a.dataFimReal");
                query.Append("     , a.TipoAgenda, a.IdSala, a.CodigoVeic, a.LocalVisita");
                query.Append("     , a.IdEmpresa, a.IdUsuario, a.DiaTodo, a.Lembrar, a.DataLembrete");
                query.Append("     , a.Status, a.Texto");
                query.Append("     , SUBSTR(u.nome, 1, INSTR(u.nome, ' ') - 1) || Substr(u.nome, INSTR(u.nome, ' ', -1)) Nome");

                query.Append("  From NIFF_CHM_Agenda a, niff_chm_usuarios u");

                if (diaTodo)
                {
                    query.Append(" Where trunc(Data) = To_Date('" + dataInicio.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   and trunc(DataFim) = To_Date('" + dataFim.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   and DiaTodo = 'S'");
                }
                else
                {
                    query.Append(" Where Data = To_Date('" + dataInicio.ToString() + "','dd/mm/yyyy hh24:mi:ss')");
                    query.Append("   and DataFim = To_Date('" + dataFim.ToString() + "','dd/mm/yyyy hh24:mi:ss')");
                }
                query.Append("   And u.Idusuario = a.Idusuario");
                query.Append("   and TipoAgenda = '" + (tipoAgenda == Publicas.TipoAgenda.Carro ? "C" :
                                                        (tipoAgenda == Publicas.TipoAgenda.Particular ? "P" :
                                                        (tipoAgenda == Publicas.TipoAgenda.Visita ? "V" :
                                                        (tipoAgenda == Publicas.TipoAgenda.SalaDeReuniao ? "S" :
                                                        (tipoAgenda == Publicas.TipoAgenda.TreinamentoInterno ? "I" :
                                                        (tipoAgenda == Publicas.TipoAgenda.TreinamentoExterno ? "E" :
                                                        (tipoAgenda == Publicas.TipoAgenda.Ferias ? "F" : "A"))))))) + "'");

                Query executar = sessao.CreateQuery(query.ToString());
                agendaReader = executar.ExecuteQuery();

                using (agendaReader)
                {
                    while (agendaReader.Read())
                    {
                        Agenda _agenda = new Agenda();
                        _agenda.IdAgenda = Convert.ToInt32(agendaReader["IdAgenda"].ToString());
                        _agenda.IdUsuario = Convert.ToInt32(agendaReader["IdUsuario"].ToString());

                        try
                        {
                            _agenda.IdEmpresa = Convert.ToInt32(agendaReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.IdSala = Convert.ToInt32(agendaReader["IdSala"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.CodigoVeiculo = Convert.ToInt32(agendaReader["CodigoVeic"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.Lembrar = Convert.ToInt32(agendaReader["Lembrar"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.Data = Convert.ToDateTime(agendaReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.DataFim = Convert.ToDateTime(agendaReader["DataFim"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.DataFimReal = Convert.ToDateTime(agendaReader["DataFimReal"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _agenda.DataLembrete = Convert.ToDateTime(agendaReader["DataLembrete"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.HoraInicio = Convert.ToDateTime(agendaReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _agenda.HoraFim = Convert.ToDateTime(agendaReader["DataFim"].ToString());
                        }
                        catch { }

                        _agenda.Texto = agendaReader["Texto"].ToString();
                        _agenda.Local = agendaReader["LocalVisita"].ToString();
                        _agenda.Nome = agendaReader["Nome"].ToString();

                        _agenda.DiaTodo = agendaReader["diaTodo"].ToString() == "S";
                        _agenda.Status = (agendaReader["Status"].ToString() == "A" ? Publicas.StatusAgenda.Ativo :
                                         (agendaReader["Status"].ToString() == "C" ? Publicas.StatusAgenda.Cancelado :
                                         (agendaReader["Status"].ToString() == "R" ? Publicas.StatusAgenda.Reservado :
                                         (agendaReader["Status"].ToString() == "S" ? Publicas.StatusAgenda.SolicitacaoCarro :
                                         Publicas.StatusAgenda.Finalizado)))); 

                        switch (agendaReader["TipoAgenda"].ToString())
                        {
                            case "S":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.SalaDeReuniao;
                                break;
                            case "C":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.Carro;
                                break;
                            case "I":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.TreinamentoInterno;
                                break;
                            case "E":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.TreinamentoExterno;
                                break;
                            case "P":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.Particular;
                                break;
                            case "V":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.Visita;
                                break;
                            case "F":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.Ferias;
                                break;
                            case "A":
                                _agenda.TipoAgenda = Publicas.TipoAgenda.AtestadoMedico;
                                break;
                        }

                        _agenda.DescricaoTipoAgenda = Publicas.GetDescription(_agenda.TipoAgenda, "");
                        _agenda.Existe = true;

                        _lista.Add(_agenda);
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

        public List<ParticipanteDaAgenda> Listar(int idAgenda)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ParticipanteDaAgenda> _lista = new List<ParticipanteDaAgenda>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select a.idagenda, a.idusuario, a.conviteaceito, a.avisar");
                query.Append("     , e.Nomeabreviado");
                query.Append("     , SUBSTR(u.nome, 1, INSTR(u.nome, ' ') - 1) || Substr(u.nome, INSTR(u.nome, ' ', -1)) Nome");

                query.Append("  From niff_chm_agendausu a, niff_chm_empresas e, niff_chm_usuarios u");
                query.Append(" Where e.Idempresa(+) = u.Idempresa");
                query.Append("   And u.Idusuario = a.Idusuario");
                query.Append("   And a.idagenda = " + idAgenda);

                Query executar = sessao.CreateQuery(query.ToString());
                agendaReader = executar.ExecuteQuery();

                using (agendaReader)
                {
                    while (agendaReader.Read())
                    {
                        ParticipanteDaAgenda _agenda = new ParticipanteDaAgenda();
                        _agenda.IdAgenda = Convert.ToInt32(agendaReader["IdAgenda"].ToString());
                        _agenda.IdUsuario = Convert.ToInt32(agendaReader["IdUsuario"].ToString());

                        _agenda.Empresa = agendaReader["Nomeabreviado"].ToString();
                        _agenda.Nome = agendaReader["Nome"].ToString();

                        _agenda.Avisado = agendaReader["Avisar"].ToString() == "S";
                        _agenda.ConviteAceito = agendaReader["ConviteAceito"].ToString() == "S";

                        _agenda.Existe = true;

                        _lista.Add(_agenda);
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

        public bool Gravar(Agenda agenda, List<ParticipanteDaAgenda> participantes)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            int IdAgenda = 1;
            try
            {
                if (!agenda.Existe)
                {

                    query.Clear();
                    query.Append("Select SQ_NIFF_IDAgenda.NextVal next from dual");

                    Query executar = sessao.CreateQuery(query.ToString());
                    agendaReader = executar.ExecuteQuery();

                    using (agendaReader)
                    {
                        if (agendaReader.Read())
                            IdAgenda = Convert.ToInt32(agendaReader["Next"].ToString());
                    }

                    query.Clear();
                    query.Append("Insert into NIFF_CHM_Agenda");
                    query.Append("   (idAgenda, Data, DataFim ");
                    query.Append("     , TipoAgenda, LocalVisita");
                    query.Append("     , IdUsuario, DiaTodo, Lembrar");
                    if (agenda.IdSala != 0)
                        query.Append("     , IdSala ");
                    if (agenda.CodigoVeiculo != 0)
                        query.Append("     , CodigoVeic ");
                    if (agenda.IdEmpresa != 0)
                        query.Append("     , IdEmpresa ");
                    query.Append("     , Status, Texto, DataFimReal");

                    if (agenda.DataLembrete != DateTime.MinValue)
                        query.Append("     , DataLembrete ");

                    query.Append(")   Values (" + IdAgenda);
                    query.Append(", To_date('" + agenda.Data.ToString() + "','dd/mm/yyyy hh24:mi:ss')");
                    query.Append(", To_date('" + agenda.DataFim.ToString() + "','dd/mm/yyyy hh24:mi:ss')");
                    query.Append(", '" + (agenda.TipoAgenda == Publicas.TipoAgenda.Carro ? "C" :
                                         (agenda.TipoAgenda == Publicas.TipoAgenda.Particular ? "P" :
                                         (agenda.TipoAgenda == Publicas.TipoAgenda.SalaDeReuniao ? "S" :
                                         (agenda.TipoAgenda == Publicas.TipoAgenda.TreinamentoExterno ? "E" :
                                         (agenda.TipoAgenda == Publicas.TipoAgenda.TreinamentoInterno ? "I" :
                                         (agenda.TipoAgenda == Publicas.TipoAgenda.Visita ? "V" :
                                         (agenda.TipoAgenda == Publicas.TipoAgenda.Ferias ? "F" : "A"))))))) + "'"); 

                    query.Append(", '" + agenda.Local + "'");
                    query.Append(", " + agenda.IdUsuario + "");
                    query.Append(", '" + (agenda.DiaTodo ? "S" : "N") + "'");
                    query.Append(", " + agenda.Lembrar);
                    if (agenda.IdSala != 0)
                        query.Append(", " + agenda.IdSala);
                    if (agenda.CodigoVeiculo != 0)
                        query.Append(", " + agenda.CodigoVeiculo);
                    if (agenda.IdEmpresa != 0)
                        query.Append(", " + agenda.IdEmpresa);

                    query.Append(", '" + (agenda.Status == Publicas.StatusAgenda.Ativo ? "A" :
                                         (agenda.Status == Publicas.StatusAgenda.Cancelado ? "C" :
                                         (agenda.Status == Publicas.StatusAgenda.Reservado ? "R" :
                                         (agenda.Status == Publicas.StatusAgenda.SolicitacaoCarro ? "S" : "F")))) + "'");
                    query.Append(", '" + agenda.Texto + "'");
                    query.Append(", To_date('" + agenda.DataFimReal.ToString() + "','dd/mm/yyyy hh24:mi:ss') ");

                    if (agenda.DataLembrete != DateTime.MinValue)
                        query.Append(", To_date('" + agenda.DataLembrete.ToString() + "','dd/mm/yyyy hh24:mi:ss') ");

                    query.Append(")");
                }
                else
                {
                    query.Clear();
                    query.Append("Update NIFF_CHM_Agenda");
                    query.Append("   Set Data = To_date('" + agenda.Data.ToString()  + "','dd/mm/yyyy hh24:mi:ss')");
                    query.Append("     , DataFim = To_date('" + agenda.DataFim.ToString() + "','dd/mm/yyyy hh24:mi:ss')");
                    query.Append("     , TipoAgenda = '" + (agenda.TipoAgenda == Publicas.TipoAgenda.Carro ? "C" :
                                         (agenda.TipoAgenda == Publicas.TipoAgenda.Particular ? "P" :
                                         (agenda.TipoAgenda == Publicas.TipoAgenda.SalaDeReuniao ? "S" :
                                         (agenda.TipoAgenda == Publicas.TipoAgenda.TreinamentoExterno ? "E" :
                                         (agenda.TipoAgenda == Publicas.TipoAgenda.TreinamentoInterno ? "I" :
                                         (agenda.TipoAgenda == Publicas.TipoAgenda.Visita ? "V" :
                                         (agenda.TipoAgenda == Publicas.TipoAgenda.Ferias ? "F" : "A"))))))) + "'");
                    query.Append("     , LocalVisita = '" + agenda.Local + "'");
                    query.Append("     , IdUsuario = " + agenda.IdUsuario + "");
                    query.Append("     , DiaTodo = '" + (agenda.DiaTodo ? "S" : "N") + "'");
                    query.Append("     , Lembrar = " + agenda.Lembrar);
                    
                    if (agenda.IdSala != 0)
                        query.Append("     , IdSala = " + agenda.IdSala);
                    if (agenda.CodigoVeiculo != 0)
                        query.Append("     , CodigoVeic = " + agenda.CodigoVeiculo);
                    if (agenda.IdEmpresa != 0)
                        query.Append("     , IdEmpresa = " + agenda.IdEmpresa);

                    query.Append("     , Status = '" + (agenda.Status == Publicas.StatusAgenda.Ativo ? "A" :
                                                       (agenda.Status == Publicas.StatusAgenda.Cancelado ? "C" :
                                                       (agenda.Status == Publicas.StatusAgenda.Reservado ? "R" :
                                                       (agenda.Status == Publicas.StatusAgenda.SolicitacaoCarro ? "S" : "F")))) + "'");

                    query.Append("     , Texto = '" + agenda.Texto + "'");
                    query.Append("     , DataFimReal = To_date('" + agenda.DataFimReal.ToString() + "','dd/mm/yyyy hh24:mi:ss')");

                    if (agenda.DataLembrete != DateTime.MinValue)
                        query.Append("    ,  DataLembrete = To_date('" + agenda.DataLembrete.ToString() + "','dd/mm/yyyy hh24:mi:ss') ");

                    query.Append(" Where idAgenda = " + agenda.IdAgenda);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (!retorno)
                    return retorno;
                else
                {
                    if (participantes != null)
                    {
                        foreach (var item in participantes)
                        {
                            if (!item.Existe)
                            {
                                query.Clear();
                                query.Append("Insert into Niff_Chm_AgendaUsu");
                                query.Append("   (idagenda, idusuario, conviteaceito, avisar )");
                                query.Append("   Values (" + IdAgenda);
                                query.Append("          , " + item.IdUsuario);
                                query.Append("          , 'N'");
                                query.Append("          , '" + (item.Avisado ? "S" : "N") + "')");

                                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                                if (!retorno)
                                    return false;
                            }
                        }
                    }
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

        public bool Excluir(int idAgenda)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Delete Niff_Chm_AgendaUsu");
                query.Append(" Where idAgenda = " + idAgenda);

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;
                else
                {
                    query.Clear();
                    query.Append("Delete NIFF_CHM_Agenda");
                    query.Append(" Where idAgenda = " + idAgenda);

                    return sessao.ExecuteSqlTransaction(query.ToString());
                }
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
