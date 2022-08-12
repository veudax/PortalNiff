using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class NotificacaoComunicadoDAO
    {
        IDataReader comunicadoReader;

        public List<NotificacaoComunicado> Listar(int ano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<NotificacaoComunicado> _lista = new List<NotificacaoComunicado>();

            try
            {
                query.Append("Select c.status, c.processo, c.dataabertura, c.Dataconfirmacao, ");
                query.Append("       c.datareprovacao, c.dataalteracao, c.datafinalizado, c.datacancela, ");
                query.Append("       v.Nome vara, u.Nome usuarioAbertura, ua.Nome UsuarioAprovador, ");
                query.Append("       ur.Nome UsuarioReprovador, uc.Nome UsuarioCancelador, ut.Nome UsuarioAlteracao, ");
                query.Append("       uf.Nome UsuarioFinalizado, e.codigoglobus || ' - ' || e.nomeabreviado empresa, ");
                query.Append("       c.idcomunicado");

                query.Append("  From niff_jur_comunicados c ");
                query.Append("     , Niff_Jur_NotifComunicado n ");
                query.Append("     , Niff_Jur_Vara v ");
                query.Append("     , Niff_Chm_Usuarios u ");
                query.Append("     , niff_chm_Usuarios ua ");
                query.Append("     , niff_chm_Usuarios ur ");
                query.Append("     , niff_chm_Usuarios uc ");
                query.Append("     , niff_chm_Usuarios uf ");
                query.Append("     , niff_chm_Usuarios ut ");
                query.Append("     , Niff_chm_Empresas e ");

                query.Append(" Where c.Idcomunicado = n.Idcomunicado(+) ");
                query.Append("   And To_Char(c.dataabertura,'yyyy') >= '" + ano.ToString() + "' ");
                query.Append("   And c.status = n.status(+) ");
                query.Append("   And v.Idvara = c.Idvara ");
                query.Append("   And u.IdUsuario(+)  = c.IdUsuario ");
                query.Append("   And ua.IdUsuario(+) = c.Idusuarioaprovacao ");
                query.Append("   And ur.IdUsuario(+) = c.IdUsuarioReprovador ");
                query.Append("   And uc.IdUsuario(+) = c.IdUsuarioCancela ");
                query.Append("   And ut.IdUsuario(+) = c.IdUsuarioAltera ");
                query.Append("   And uf.IdUsuario(+) = c.IdUsuarioFinaliza ");
                query.Append("   And e.idempresa = c.Idempresa ");
                query.Append("   And n.status Is Null ");
                query.Append("   And (c.IdUsuario <> " + Publicas._idUsuario);
                query.Append("   or c.Idusuarioaprovacao <> " + Publicas._idUsuario);
                query.Append("   or c.IdUsuarioReprovador <> " + Publicas._idUsuario);
                query.Append("   or c.IdUsuarioCancela <> " + Publicas._idUsuario);
                query.Append("   or c.IdUsuarioAltera <> " + Publicas._idUsuario);
                query.Append("   or c.IdUsuarioFinaliza <> " + Publicas._idUsuario + ")");

                Query executar = sessao.CreateQuery(query.ToString());

                comunicadoReader = executar.ExecuteQuery();

                using (comunicadoReader)
                {
                    while (comunicadoReader.Read())
                    {
                        NotificacaoComunicado _comunicado = new NotificacaoComunicado();

                        _comunicado.IdComunicado = Convert.ToInt32(comunicadoReader["idcomunicado"].ToString());

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

                        _comunicado.Empresa = comunicadoReader["empresa"].ToString();

                        _comunicado.Status = (comunicadoReader["status"].ToString() == "N" ? Publicas.StatusComunicado.Novo :
                            (comunicadoReader["status"].ToString() == "A" ? Publicas.StatusComunicado.Aprovado :
                            (comunicadoReader["status"].ToString() == "R" ? Publicas.StatusComunicado.Reprovado :
                            (comunicadoReader["status"].ToString() == "C" ? Publicas.StatusComunicado.Cancelado :
                            (comunicadoReader["status"].ToString() == "L" ? Publicas.StatusComunicado.Alterado :
                            Publicas.StatusComunicado.Finalizado)))));

                        _comunicado.Solicitante = comunicadoReader["usuarioAbertura"].ToString();
                        _comunicado.Vara = comunicadoReader["vara"].ToString();

                        _comunicado.UsuarioAprovador = comunicadoReader["UsuarioAprovador"].ToString();
                        _comunicado.UsuarioReprovador = comunicadoReader["UsuarioReprovador"].ToString();
                        _comunicado.UsuarioCancelador = comunicadoReader["UsuarioCancelador"].ToString();
                        _comunicado.UsuarioAlterador = comunicadoReader["UsuarioAlteracao"].ToString();
                        _comunicado.UsuarioFinaliza = comunicadoReader["UsuarioFinalizado"].ToString();
                        _comunicado.Processo = comunicadoReader["processo"].ToString();

                        _lista.Add(_comunicado);
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

        public bool Gravar(NotificacaoComunicado notificacao)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Insert into Niff_Jur_Notifcomunicado");
                query.Append(" (idnotificacao, idcomunicado, idusuario, status)");
                query.Append(" Values( SQ_NIFF_JurIdNotif.NextVal, ");
                query.Append(notificacao.IdComunicado + ", ");
                query.Append(Publicas._idUsuario + ", ");
                query.Append("'" + (notificacao.Status == Publicas.StatusComunicado.Novo ? "N" :
                    (notificacao.Status == Publicas.StatusComunicado.Alterado ? "L" :
                    (notificacao.Status == Publicas.StatusComunicado.Aprovado ? "A" :
                    (notificacao.Status == Publicas.StatusComunicado.Cancelado ? "C" :
                    (notificacao.Status == Publicas.StatusComunicado.Finalizado ? "F" : "R"))))) + "'");
                query.Append(")");
                
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
