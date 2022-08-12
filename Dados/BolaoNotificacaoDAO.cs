using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class BolaoNotificacaoDAO
    {
        //IDataReader dataReader;

        public bool Grava(BolaoNotificacao times)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Insert into Niff_bol_NotificacaoJogos");
                query.Append("   (id, idJogo, IdColaborador ");
                query.Append("  ) Values ( SQ_NIFF_IdBolNotificacao.Nextval ");
                query.Append(", " + times.IdJogo);
                query.Append(", " + times.IdColaborador);
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
