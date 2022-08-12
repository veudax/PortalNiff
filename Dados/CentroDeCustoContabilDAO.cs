using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class CentroDeCustoContabilDAO
    {
        IDataReader custoReader;

        public List<CentroDeCustoContabil> Listar(string empresaGlobus)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<CentroDeCustoContabil> _lista = new List<CentroDeCustoContabil>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select c.codcusto, c.nroplano, c.desccusto, c.classcustoctb, c.aceitalancamento ");
                query.Append("  From ctbcusto c, ctbexerc e ");
                query.Append(" Where c.nroplano = e.nroplano ");
                if (DateTime.Now.Date <= Convert.ToDateTime("15/01/" + DateTime.Now.Date.Year))
                    query.Append("   And e.data_ini_ex = trunc(Trunc(SYSDATE, 'rr') - 1, 'rr')");
                else
                    query.Append("   And e.data_ini_ex = Trunc(SYSDATE,'rr') ");
                query.Append("   And lPad(e.codigoempresa,3,'0') || '/' || lPad(e.codigofl, 3,'0') = '" + empresaGlobus + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                custoReader = executar.ExecuteQuery();
                using (custoReader)
                {
                    while (custoReader.Read())
                    {
                        CentroDeCustoContabil centro = new CentroDeCustoContabil();

                        centro.Codigo = Convert.ToInt32(custoReader["CodCusto"].ToString());
                        centro.NumeroDoPlano = Convert.ToInt32(custoReader["nroplano"].ToString());
                        centro.Descricao = custoReader["desccusto"].ToString();
                        centro.Classificador = custoReader["classcustoctb"].ToString();
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

        public CentroDeCustoContabil Consulta(int codigo, string empresaGlobus)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            CentroDeCustoContabil centro = new CentroDeCustoContabil();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select c.codcusto, c.nroplano, c.desccusto, c.classcustoctb, c.aceitalancamento ");
                query.Append("  From ctbcusto c, ctbexerc e ");
                query.Append(" Where c.nroplano = e.nroplano ");
                if (DateTime.Now.Date <= Convert.ToDateTime("15/01/" + DateTime.Now.Date.Year))
                    query.Append("   And e.data_ini_ex = trunc(Trunc(SYSDATE, 'rr') - 1, 'rr')");
                else
                    query.Append("   And e.data_ini_ex = Trunc(SYSDATE,'rr') ");
                query.Append("   And lPad(e.codigoempresa,3,'0') || '/' || lPad(e.codigofl, 3,'0') = '" + empresaGlobus + "'");
                query.Append("   And c.CodCusto = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());
                custoReader = executar.ExecuteQuery();
                using (custoReader)
                {
                    if (custoReader.Read())
                    {
                        centro.Codigo = Convert.ToInt32(custoReader["CodCusto"].ToString());
                        centro.NumeroDoPlano = Convert.ToInt32(custoReader["nroplano"].ToString());
                        centro.Descricao = custoReader["desccusto"].ToString();
                        centro.Classificador = custoReader["classcustoctb"].ToString();
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

        public CentroDeCustoContabil Consulta(int codigo, int plano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            CentroDeCustoContabil centro = new CentroDeCustoContabil();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select c.codcusto, c.nroplano, c.desccusto, c.classcustoctb, c.aceitalancamento ");
                query.Append("  From ctbcusto c");
                query.Append(" Where c.nroplano = " + plano);
                query.Append("   And c.CodCusto = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());
                custoReader = executar.ExecuteQuery();
                using (custoReader)
                {
                    if (custoReader.Read())
                    {
                        centro.Codigo = Convert.ToInt32(custoReader["CodCusto"].ToString());
                        centro.NumeroDoPlano = Convert.ToInt32(custoReader["nroplano"].ToString());
                        centro.Descricao = custoReader["desccusto"].ToString();
                        centro.Classificador = custoReader["classcustoctb"].ToString();
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
