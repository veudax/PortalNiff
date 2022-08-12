using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class NotificacaoChamadoDAO
    {
        IDataReader dataReader;

        public NotificacaoChamado Consultar(int id, int idHitorico)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            NotificacaoChamado _notificacao = new NotificacaoChamado();
            
            try
            {
                query.Append("Select idnotificacao, idchamado, idusuario, idhistorico, data ");
                query.Append("  From niff_chm_notifChamado n ");
                if (id != 0 && idHitorico != 0)
                {
                    query.Append(" where idusuario = " + id);
                    query.Append("   and idhistorico = " + idHitorico);
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _notificacao.Existe = true;
                        _notificacao.Id = Convert.ToInt32(dataReader["idnotificacao"].ToString());
                        _notificacao.IdChamado = Convert.ToInt32(dataReader["Idchamado"].ToString());
                        _notificacao.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        _notificacao.IdHistorico = Convert.ToInt32(dataReader["IdHistorico"].ToString());

                        try
                        {
                            _notificacao.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }                        
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
            return _notificacao;
        }

        public List<NotificacaoChamado> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<NotificacaoChamado> _lista = new List<NotificacaoChamado>();

            try
            {
                query.Append("Select idnotificacao, idchamado, idusuario, idhistorico, data ");
                query.Append("  From niff_chm_notifChamado n ");

                //query.Append(" Where h.idchamado = n.Idchamado(+) ");
                //query.Append("   And h.status = n.status(+) ");
                //query.Append("   And h.IdUsuario = u.IdUsuario(+) ");
                //query.Append("   And c.idchamado = h.Idchamado ");
                //query.Append("   And e.idempresa = c.Idempresa ");
                //query.Append("   And n.status Is Null ");
                //query.Append("   And h.IdUsuario <> " + Publicas._idUsuario);

                //if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante)
                //    query.Append("   And u.iddepartamento = " + Publicas._usuario.IdDepartamento);

                //query.Append(" Group by c.numero, c.Idchamado, h.idusuario, h.status ");
                //query.Append("     , u.Nome, e.codigoglobus || ' - ' || e.nomeabreviado, c.Assunto ");

                //Query executar = sessao.CreateQuery(query.ToString());

                //dataReader = executar.ExecuteQuery();

                //using (dataReader)
                //{
                //    while (dataReader.Read())
                //    {
                //        NotificacaoChamado _notificacao = new NotificacaoChamado();
                //        try
                //        {
                //            _notificacao.IdChamado = Convert.ToInt32(dataReader["Idchamado"].ToString());
                //        }
                //        catch { }
                //        try
                //        {
                //            _notificacao.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                //        }
                //        catch { }
                //        try
                //        {
                //            _notificacao.IdUsuarioAnterior = Convert.ToInt32(dataReader["IdUsuario2"].ToString());
                //        }
                //        catch { }

                //        _notificacao.Numero = dataReader["Numero"].ToString();
                //        _notificacao.Assunto = dataReader["Assunto"].ToString();
                //        try
                //        {
                //            _notificacao.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                //        }
                //        catch { }

                //        _notificacao.Empresa = dataReader["empresa"].ToString();

                //        switch (dataReader["Status"].ToString())
                //        {
                //            case "A":
                //                _notificacao.Status = Publicas.StatusChamado.Adequacao;
                //                break;
                //            case "N":
                //                _notificacao.Status = Publicas.StatusChamado.Novo;
                //                break;
                //            case "P":
                //                _notificacao.Status = Publicas.StatusChamado.Pendente;
                //                break;
                //            case "R":
                //                _notificacao.Status = Publicas.StatusChamado.Reaberto;
                //                break;
                //            case "C":
                //                _notificacao.Status = Publicas.StatusChamado.Cancelado;
                //                break;
                //            case "E":
                //                _notificacao.Status = Publicas.StatusChamado.EmAndamento;
                //                break;
                //            case "F":
                //                _notificacao.Status = Publicas.StatusChamado.Finalizado;
                //                break;
                //        }

                //        _notificacao.NomeAnalista = dataReader["NomeUsuario"].ToString();
                        
                //        _lista.Add(_notificacao);
                //    }
                //}
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

        public bool Gravar(NotificacaoChamado notificacao)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Insert into niff_chm_notifChamado");
                query.Append(" (idnotificacao, idchamado, idusuario,  idhistorico, data)");
                query.Append(" Values( SQ_NIFF_NotifCham.NextVal, ");
                query.Append(notificacao.IdChamado + ", ");
                query.Append(Publicas._idUsuario + ", ");
                query.Append(notificacao.IdHistorico + ", sysdate");
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
