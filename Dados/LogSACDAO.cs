using Classes;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class LogSACDAO
    {
        
        public bool Gravar(LogSAC log)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            OracleParameter parametro = new OracleParameter();
            List<OracleParameter> parametros = new List<OracleParameter>();

            try
            {
                query.Clear();
                query.Append("Insert into NIFF_CHM_LogSAC");
                query.Append("   ( IDLOG, IdAtendimento, Descricao, Data, idUsuario ) ");
                query.Append(" Values (SQ_NIFF_IdLogSac.NextVal, ");
                query.Append(log.IdAtendimento + ", ");
                query.Append(" :Descricao, ");
                query.Append(" sysdate, ");
                query.Append(log.IdUsuario + ") ");

                parametro = new OracleParameter();
                parametro.ParameterName = ":Descricao";
                parametro.Value = log.Descricao;
                parametro.OracleType = OracleType.VarChar;
                parametros.Add(parametro);

                return sessao.ExecuteSqlTransaction(query.ToString(), parametros.ToArray());
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
