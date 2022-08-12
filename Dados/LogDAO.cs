using Classes;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class LogDAO
    {
        public bool Gravar(Log log)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Insert into NIFF_CHM_Log");
                query.Append("   (IdLog, Descricao, Data, IdUsuario, Tela) ");
                query.Append("   Values ( SQ_NIFF_Log.NextVal ");
                query.Append(", :plog");
                query.Append(", sysdate ");
                query.Append(", " + log.IdUsuario );
                query.Append(", '" + log.Tela + "' )");

                OracleParameter _parametro = new OracleParameter();
                _parametro.ParameterName = "plog";
                _parametro.Value = log.Descricao.Trim();

                List<OracleParameter> _lista = new List<OracleParameter>();
                _lista.Add(_parametro);

                return sessao.ExecuteSqlTransaction(query.ToString(), _lista.ToArray());
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
