using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class NotificacaoCorridasDAO
    {
        IDataReader dataReader;

        public NotificacaoCorridas Consulta(int idCorrida)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            NotificacaoCorridas _tipo = new NotificacaoCorridas();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select n.Idnotifcorrida");
                query.Append("  From Niff_Chm_Notifcorrida n, niff_chm_corridas c");
                query.Append(" Where c.Idcorrida = n.Idcorrida(+) ");
                query.Append("   And c.Ativo = 'S' ");
                query.Append("   And n.idusuario = " + Publicas._idUsuario);
                query.Append("   And c.Idcorrida = " + idCorrida);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Idnotifcorrida"].ToString());
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

        public bool Gravar(int idCorrida)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Clear();
                query.Append("Insert into Niff_Chm_Notifcorrida");
                query.Append(" ( idnotifcorrida, idcorrida, idusuario)");
                query.Append(" Values ( SQ_NIFF_chmNotCorrida.NextVal");
                query.Append("        , " + idCorrida);
                query.Append("        ," + Publicas._idUsuario);
                query.Append(" )");
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
