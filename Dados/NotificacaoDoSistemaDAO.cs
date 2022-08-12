using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class NotificacaoDoSistemaDAO
    {
        IDataReader dataReader;

        public NotificacaoDoSistema Consulta(DateTime data)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            NotificacaoDoSistema _tipo = new NotificacaoDoSistema();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idnotificacao, dataaviso, dataacao, idusuario, motivo, dataFimAcao, Status, TIPOATUALIZACAO");
                query.Append("  from Niff_Chm_NotificacoesSistema");
                query.Append(" Where trunc(dataaviso) = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idnotificacao"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["idusuario"].ToString());
                        _tipo.Motivo = dataReader["motivo"].ToString();
                        _tipo.Status = dataReader["status"].ToString() == "S";
                        _tipo.TipoAtualizacao = dataReader["TIPOATUALIZACAO"].ToString();
                        _tipo.DataDoAviso = Convert.ToDateTime(dataReader["dataaviso"].ToString());
                        _tipo.DataDaAcao = Convert.ToDateTime(dataReader["dataacao"].ToString());
                        _tipo.DataFimDaAcao = Convert.ToDateTime(dataReader["dataFimAcao"].ToString());
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
            return _tipo;
        }

        public NotificacaoDoSistema Consulta()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            NotificacaoDoSistema _tipo = new NotificacaoDoSistema();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idnotificacao, dataaviso, dataacao, idusuario, motivo, dataFimAcao, Status, TIPOATUALIZACAO");
                query.Append("  from Niff_Chm_NotificacoesSistema");
                query.Append(" Where trunc(dataaviso) = trunc(sysDate)");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idnotificacao"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["idusuario"].ToString());
                        _tipo.Motivo = dataReader["motivo"].ToString();
                        _tipo.Status = dataReader["status"].ToString() == "S";
                        _tipo.TipoAtualizacao = dataReader["TIPOATUALIZACAO"].ToString();
                        _tipo.DataDoAviso = Convert.ToDateTime(dataReader["dataaviso"].ToString());
                        _tipo.DataDaAcao = Convert.ToDateTime(dataReader["dataacao"].ToString());
                        _tipo.DataFimDaAcao = Convert.ToDateTime(dataReader["dataFimAcao"].ToString());
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
            return _tipo;
        }

        public bool Gravar(NotificacaoDoSistema tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into Niff_Chm_NotificacoesSistema");
                    query.Append(" ( idnotificacao, dataaviso, dataacao, idusuario, motivo, dataFimAcao, Status, TIPOATUALIZACAO )");
                    query.Append(" Values ( SQ_NIFF_AdsNotSistema.NextVal");
                    query.Append("        , To_date('" + tipo.DataDoAviso.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        , To_date('" + tipo.DataDaAcao.ToShortDateString() + " " + tipo.DataDaAcao.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                    query.Append("        ," + tipo.IdUsuario );
                    query.Append("        ,'" + tipo.Motivo + "'");
                    query.Append("        , To_date('" + tipo.DataFimDaAcao.ToShortDateString() + " " + tipo.DataFimDaAcao.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                    query.Append("        ,'" + (tipo.Status ? "S" : "N") + "'");
                    query.Append("        ,'" + tipo.TipoAtualizacao + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_Chm_NotificacoesSistema");
                    query.Append("   set motivo = '" + tipo.Motivo + "'");
                    query.Append("     , Status = '" + (tipo.Status ? "S" : "N") + "'");
                    query.Append("     , dataAcao = To_date('" + tipo.DataDaAcao.ToShortDateString() + " " + tipo.DataDaAcao.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                    query.Append("     , dataFimAcao = To_date('" + tipo.DataFimDaAcao.ToShortDateString() + " " + tipo.DataFimDaAcao.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                    query.Append("     , TIPOATUALIZACAO = '" + tipo.TipoAtualizacao + "'");
                    query.Append(" Where idnotificacao = " + tipo.Id);

                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

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

        public bool Exclui(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (id != 0)
                {
                    query.Append("Delete Niff_Chm_NotificacoesSistema");
                    query.Append(" Where idnotificacao = " + id);
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
