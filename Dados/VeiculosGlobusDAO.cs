using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class VeiculosGlobusDAO
    {
        IDataReader dataReader;

        public List<Classes.VeiculosGlobus> Listar(string empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<VeiculosGlobus> _lista = new List<VeiculosGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select v.codigoveic, v.placaatualveic, v.prefixoveic, v.condicaoveic");
                query.Append("  From frt_cadveiculos v");
                query.Append(" Where condicaoveic = 'A'");

                #region empresas
                if (empresa == "001/001")
                    query.Append("           And v.codigoempresa = 1 and v.CodigoFl = 1");

                if (empresa == "001/002")
                    query.Append("           And v.codigoempresa = 1 and v.CodigoFl = 2");

                if (empresa == "002/001")
                    query.Append("           And v.codigoempresa = 2 and v.CodigoFl = 1");

                if (empresa == "003/001")
                    query.Append("           And v.codigoempresa = 3 and v.CodigoFl = 1");

                if (empresa == "004/001")
                    query.Append("           And v.codigoempresa = 4 and v.CodigoFl = 1");

                if (empresa == "005/001")
                    query.Append("           And v.codigoempresa = 5 and v.CodigoFl = 1");

                if (empresa == "006/001")
                    query.Append("           And v.codigoempresa = 6 and v.CodigoFl = 1");

                if (empresa == "009/001")
                    query.Append("           And v.codigoempresa = 9 and v.CodigoFl = 1");

                if (empresa == "013/001")
                    query.Append("           And v.codigoempresa = 13 and v.CodigoFl = 1");

                if (empresa == "026/001")
                    query.Append("           And v.codigoempresa = 26 and v.CodigoFl = 1 ");

                if (empresa == "026/003")
                    query.Append("           And v.codigoEmpresa = 26 and v.CodigoFl = 3 ");

                if (empresa == "029/001")
                    query.Append("           And v.codigoEmpresa = 29 and v.CodigoFl = 1 ");
                #endregion

                Query executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        VeiculosGlobus _veic = new VeiculosGlobus();
                        _veic.Existe = true;

                        _veic.Id = Convert.ToDecimal(dataReader["CodigoVeic"].ToString());
                        _veic.Placa = dataReader["placaatualveic"].ToString();
                        _veic.Prefixo = dataReader["prefixoveic"].ToString();
                        _veic.Ativo = dataReader["condicaoveic"].ToString() == "A";
                        
                        _lista.Add(_veic);
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

        public Classes.VeiculosGlobus Consultar(string empresa, string prefixo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            VeiculosGlobus _veic = new VeiculosGlobus();

            try
            {

                query.Append("Select v.codigoveic, v.placaatualveic, v.prefixoveic, v.condicaoveic");
                query.Append("     , codigoEmpresa, codigoFl");
                query.Append("  From frt_cadveiculos v");
                query.Append(" Where prefixoveic = '" + prefixo + "'");

                #region empresas
                if (empresa == "001/001")
                    query.Append("           And v.codigoempresa = 1 and v.CodigoFl = 1");

                if (empresa == "001/002")
                    query.Append("           And v.codigoempresa = 1 and v.CodigoFl = 2");

                if (empresa == "002/001")
                    query.Append("           And v.codigoempresa = 2 and v.CodigoFl = 1");

                if (empresa == "003/001")
                    query.Append("           And v.codigoempresa = 3 and v.CodigoFl = 1");

                if (empresa == "004/001")
                    query.Append("           And v.codigoempresa = 4 and v.CodigoFl = 1");

                if (empresa == "005/001")
                    query.Append("           And v.codigoempresa = 5 and v.CodigoFl = 1");

                if (empresa == "006/001")
                    query.Append("           And v.codigoempresa = 6 and v.CodigoFl = 1");

                if (empresa == "009/001")
                    query.Append("           And v.codigoempresa = 9 and v.CodigoFl = 1");

                if (empresa == "013/001")
                    query.Append("           And v.codigoempresa = 13 and v.CodigoFl = 1");

                if (empresa == "026/001")
                    query.Append("           And v.codigoempresa = 26 and v.CodigoFl = 1 ");

                if (empresa == "026/003")
                    query.Append("           And v.codigoEmpresa = 26 and v.CodigoFl = 3 ");

                if (empresa == "029/001")
                    query.Append("           And v.codigoEmpresa = 29 and v.CodigoFl = 1 ");
                #endregion

                Query executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _veic.Existe = true;

                        _veic.Id = Convert.ToDecimal(dataReader["CodigoVeic"].ToString());
                        _veic.Placa = dataReader["placaatualveic"].ToString();
                        _veic.Prefixo = dataReader["prefixoveic"].ToString();
                        _veic.Ativo = dataReader["condicaoveic"].ToString() == "A";
                        _veic.Empresa = Convert.ToInt32(dataReader["CodigoEmpresa"].ToString()).ToString("000") + "/" +
                            Convert.ToInt32(dataReader["CodigoFl"].ToString()).ToString("000");

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
            return _veic;

        }

    }
}
