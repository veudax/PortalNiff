using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ParametrosDAO
    {
        IDataReader parametrosReader;

        public Parametro Consulta()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Parametro _parametro = new Parametro();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select idparam, prazoreabertura, exigeavaliacao, formatochamado, usuautocancelar,");
                query.Append("       horainicioagenda, horafimagenda, qtddiasretornochamado, emailchamado,");
                query.Append("       smtp, autentica, autenticasmtp, porta, senhaemail, exibircancelados,");
                query.Append("       usuariomesmodepto, atendenteconcluichamado, atendentepodeabrirchamado, Separador, ");
                query.Append("       MesesConsultaDashBoardChamados");
                query.Append("  From Niff_Chm_Parametros ");

                Query executar = sessao.CreateQuery(query.ToString());

                parametrosReader = executar.ExecuteQuery();

                using (parametrosReader)
                {
                    if (parametrosReader.Read())
                    {
                        _parametro.Existe = true;

                        _parametro.IdParam = Convert.ToInt32(parametrosReader["idparam"].ToString());

                        _parametro.PrazoReabertura = Convert.ToInt32(parametrosReader["prazoreabertura"].ToString());
                        _parametro.CancelarVisivelPor = Convert.ToInt32(parametrosReader["exibircancelados"].ToString());
                        _parametro.PrazoRetorno = Convert.ToInt32(parametrosReader["qtddiasretornochamado"].ToString());
                        _parametro.MesesConsultaDashboardChamados = Convert.ToInt32(parametrosReader["MesesConsultaDashBoardChamados"].ToString());

                        _parametro.ExigeAvaliacao = parametrosReader["exigeavaliacao"].ToString() == "S";
                        _parametro.UsuarioComMesmoDepartamentoPodeVerChamados = parametrosReader["usuariomesmodepto"].ToString() == "S";
                        _parametro.AtentendePodeConcluirChamado = parametrosReader["atendenteconcluichamado"].ToString() == "S";
                        _parametro.AtentendePodeAbrirChamado= parametrosReader["atendentepodeabrirchamado"].ToString() == "S";
                        _parametro.Separador = parametrosReader["Separador"].ToString();

                        try
                        {
                            _parametro.HoraFimAgenda = Convert.ToDateTime(parametrosReader["horafimagenda"].ToString());
                        }
                        catch { }

                        try
                        {
                            _parametro.HoraInicioAgenda = Convert.ToDateTime(parametrosReader["horainicioagenda"].ToString());
                        }
                        catch { }

                        _parametro.Email = parametrosReader["emailchamado"].ToString();
                        _parametro.Smtp = parametrosReader["Smtp"].ToString();
                        _parametro.Autentica = parametrosReader["Autentica"].ToString() == "S";
                        _parametro.AutenticaSLL = parametrosReader["autenticasmtp"].ToString() == "S";
                        try
                        {
                            _parametro.Senha = parametrosReader["Senha"].ToString();
                        }
                        catch { }

                        try
                        {
                            _parametro.PortaSmtp = Convert.ToInt32(parametrosReader["porta"].ToString());
                        }
                        catch { }

                        switch (parametrosReader["formatochamado"].ToString())
                        {
                            case "A":
                                _parametro.FormatoChamado = Publicas.TipoCalculoChamado.Ano;
                                break;
                            case "AM":
                                _parametro.FormatoChamado = Publicas.TipoCalculoChamado.AnoMes;
                                break;
                            case "S":
                                _parametro.FormatoChamado = Publicas.TipoCalculoChamado.Sequencial;
                                break;
                        }

                        switch (parametrosReader["usuautocancelar"].ToString())
                        {
                            case "A":
                                _parametro.UsuarioQuePodeCancelarChamado = Publicas.TipoUsuarioCancela.Atendente;
                                break;
                            case "D":
                                _parametro.UsuarioQuePodeCancelarChamado = Publicas.TipoUsuarioCancela.Administrador;
                                break;
                            case "S":
                                _parametro.UsuarioQuePodeCancelarChamado = Publicas.TipoUsuarioCancela.Solicitante;
                                break;
                        }
                        
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
            return _parametro;
        }

        public bool Grava(Parametro parametro)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!parametro.Existe)
                {
                    query.Clear();
                    query.Append("Insert into Niff_Chm_Parametros");
                    query.Append("   (idparam, prazoreabertura, exigeavaliacao, formatochamado, usuautocancelar,");
                    query.Append("       horainicioagenda, horafimagenda, qtddiasretornochamado, emailchamado,");
                    query.Append("       smtp, autentica, autenticasmtp, porta, senhaemail, exibircancelados,");
                    query.Append("       usuariomesmodepto, atendenteconcluichamado, atendentepodeabrirchamado, Separador, ");
                    query.Append("       MesesConsultaDashBoardChamados ");
                    query.Append("  ) Values ( SQ_NIFF_IdParametro.NextVal" );
                    query.Append(", " + parametro.PrazoReabertura );
                    query.Append(", '" + (parametro.ExigeAvaliacao ? "S" : "N") + "'");

                    query.Append(", '" + (parametro.FormatoChamado == Publicas.TipoCalculoChamado.Ano ? "A" :
                                          (parametro.FormatoChamado == Publicas.TipoCalculoChamado.AnoMes ? "AM" : "S")) + "'");

                    query.Append(", '" + (parametro.UsuarioQuePodeCancelarChamado == Publicas.TipoUsuarioCancela.Atendente ? "A" :
                                          (parametro.UsuarioQuePodeCancelarChamado == Publicas.TipoUsuarioCancela.Administrador ? "D" : "S")) + "'");

                    query.Append(", To_Date('" + parametro.HoraInicioAgenda + "','dd/mm/yyyy hh24:mi:ss')");
                    query.Append(", To_Date('" + parametro.HoraFimAgenda + "','dd/mm/yyyy hh24:mi:ss')");
                    query.Append(", " + parametro.PrazoRetorno);

                    query.Append(", '" + parametro.Email + "', '" + parametro.Smtp + "'");
                    query.Append(", '" + (parametro.Autentica ? "S" : "N") + "', '" + (parametro.AutenticaSLL ? "S" : "N") + "'");
                    query.Append(", " + parametro.PortaSmtp);
                    query.Append(", '" + parametro.Senha + "' ");

                    query.Append(", " + parametro.CancelarVisivelPor );
                    query.Append(", '" + (parametro.UsuarioComMesmoDepartamentoPodeVerChamados ? "S" : "N") + "'");
                    query.Append(", '" + (parametro.AtentendePodeConcluirChamado ? "S" : "N") + "'");
                    query.Append(", '" + (parametro.AtentendePodeAbrirChamado ? "S" : "N") + "'");

                    query.Append(", '" + parametro.Separador + "'");
                    query.Append(", " + parametro.MesesConsultaDashboardChamados.ToString());

                    query.Append(" )");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Chm_Parametros");
                    query.Append("   set prazoreabertura = " + parametro.PrazoReabertura);
                    query.Append("     , exigeavaliacao = '" + (parametro.ExigeAvaliacao ? "S" : "N") + "'");

                    query.Append("     , formatochamado = '" + (parametro.FormatoChamado == Publicas.TipoCalculoChamado.Ano ? "A" :
                                          (parametro.FormatoChamado == Publicas.TipoCalculoChamado.AnoMes ? "AM" : "S")) + "'");

                    query.Append("     , usuautocancelar = '" + (parametro.UsuarioQuePodeCancelarChamado == Publicas.TipoUsuarioCancela.Atendente ? "A" :
                                          (parametro.UsuarioQuePodeCancelarChamado == Publicas.TipoUsuarioCancela.Administrador ? "D" : "S")) + "'");

                    query.Append("     , horainicioagenda = To_Date('" + parametro.HoraInicioAgenda + "','dd/mm/yyyy hh24:mi:ss')");
                    query.Append("     , horafimagenda = To_Date('" + parametro.HoraFimAgenda + "','dd/mm/yyyy hh24:mi:ss')");
                    query.Append("     , qtddiasretornochamado = " + parametro.PrazoRetorno);

                    query.Append("     , emailchamado = '" + parametro.Email + "'");
                    query.Append("     , smtp = '" + parametro.Smtp + "'");
                    query.Append("     , autentica = '" + (parametro.Autentica ? "S" : "N") + "'");
                    query.Append("     , autenticasmtp = '" + (parametro.AutenticaSLL ? "S" : "N") + "'");
                    query.Append("     , porta = " + parametro.PortaSmtp);
                    query.Append("     , senhaemail = '" + parametro.Senha + "' ");
                    
                    query.Append("     , exibircancelados = " + parametro.CancelarVisivelPor);
                    query.Append("     , usuariomesmodepto = '" + (parametro.UsuarioComMesmoDepartamentoPodeVerChamados ? "S" : "N") + "'");
                    query.Append("     , atendenteconcluichamado = '" + (parametro.AtentendePodeConcluirChamado ? "S" : "N") + "'");
                    query.Append("     , atendentepodeabrirchamado = '" + (parametro.AtentendePodeAbrirChamado ? "S" : "N") + "'");

                    query.Append("     , Separador = '" + parametro.Separador + "'");
                    query.Append("     , MesesConsultaDashBoardChamados = " + parametro.MesesConsultaDashboardChamados.ToString());

                    query.Append(" Where idparam = " + parametro.IdParam);
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

        public bool Exclui(Parametro parametro)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Empresa _empresa = new Empresa();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (parametro.IdParam != 0)
                {
                    query.Append("Delete Niff_Chm_Parametros");
                    query.Append(" Where idparam = " + parametro.IdParam);
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

    }
}
