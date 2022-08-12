using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class BancoDeDadosDAO
    {
        public DateTime DataBanco()
        {
            Sessao sessao = new Sessao();
            Query executar = sessao.CreateQuery("Select SysDate data from Dual");

            IDataReader dataReader = executar.ExecuteQuery();

            using (dataReader)
            {
                dataReader.Read();
                return Convert.ToDateTime(dataReader["Data"].ToString());
            }
        }

    }
}
