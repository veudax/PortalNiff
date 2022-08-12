using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class CentroDeCustoFinanceiroDAO
    {
        IDataReader custoReader;

        public List<CentroDeCustoFinanceiro> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<CentroDeCustoFinanceiro> _lista = new List<CentroDeCustoFinanceiro>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select codigo, descricao, classificador, aceitalancamento");
                query.Append("  From ctbcusto");                

                Query executar = sessao.CreateQuery(query.ToString());
                custoReader = executar.ExecuteQuery();
                using (custoReader)
                {
                    while (custoReader.Read())
                    {
                        CentroDeCustoFinanceiro centro = new CentroDeCustoFinanceiro();

                        centro.Codigo = Convert.ToInt32(custoReader["codigo"].ToString());
                        centro.Descricao = custoReader["descricao"].ToString();
                        centro.Classificador = custoReader["classificador"].ToString();
                        centro.AceitaLancamento = custoReader["aceitalancamento"].ToString() == "S";
                        centro.Existe = true;
                        _lista.Add(centro);
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

        public CentroDeCustoFinanceiro Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            CentroDeCustoFinanceiro centro = new CentroDeCustoFinanceiro();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select codigo, descricao, classificador, aceitalancamento");
                query.Append("  From ctbcusto");

                Query executar = sessao.CreateQuery(query.ToString());
                custoReader = executar.ExecuteQuery();
                using (custoReader)
                {
                    while (custoReader.Read())
                    {                        
                        centro.Codigo = Convert.ToInt32(custoReader["codigo"].ToString());
                        centro.Descricao = custoReader["descricao"].ToString();
                        centro.Classificador = custoReader["classificador"].ToString();
                        centro.AceitaLancamento = custoReader["aceitalancamento"].ToString() == "S";
                        centro.Existe = true;
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
            return centro;
        }
    }
}
