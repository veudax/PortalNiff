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
    public class BolaoNotificacaoDAO
    {
        IDataReader dataReader;
        
        public bool Grava(BolaoJogos times)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Insert into Niff_Bol_Jogos");
                query.Append("   (id, data, idtime1, idtime2, placar1, placar2, datalimite, localizacao, fase");

                query.Append("  ) Values ( (Select nvl(Max(Id),1)+1 From Niff_Bol_Jogos ) ");
                query.Append(", To_Date('" + times.Data.ToShortDateString() + " " + times.Data.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                query.Append(", " + times.IdTime1);
                query.Append(", " + times.IdTime2);
                query.Append(", " + times.Placar1);
                query.Append(", " + times.Placar2);
                query.Append(", To_Date('" + times.LimitePalpite.ToShortDateString() + " " + times.LimitePalpite.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");

                query.Append(", '" + times.Localizacao + "'");
                query.Append(", '" + times.Fase + "'");

                query.Append(") ");
                

                return sessao.ExecuteSqlTransaction(query.ToString(), null);
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
