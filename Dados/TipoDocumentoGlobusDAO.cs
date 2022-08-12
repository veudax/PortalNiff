using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class TipoDocumentoGlobusDAO
    {
        IDataReader dataReader;

        public TipoDocumentoGlobus Consulta(string empresa, string codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            TipoDocumentoGlobus _tipo = new TipoDocumentoGlobus();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodigoEmpresa, CodigoFl, CodTpDoc, DescTpDoc, qtd_decimais, integra_iss");
                query.Append("  from cprtpdoc ");
                query.Append("  Where Lpad(codigoempresa,3,'0') || '/' || lPad(codigofl,3,'0') = '" + empresa + "'");
                query.Append("    And CodTpDoc = '" + codigo + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Codigo = dataReader["CodTpDoc"].ToString(); ;
                        _tipo.CodigoEmpresa = Convert.ToInt32(dataReader["CodigoEmpresa"].ToString());
                        _tipo.CodigoFilial = Convert.ToInt32(dataReader["CodigoFl"].ToString());
                        _tipo.Descricao = dataReader["DescTpDoc"].ToString();
                        _tipo.QuantidadeCasasDecimais = Convert.ToInt32(dataReader["qtd_decimais"].ToString());
                        _tipo.IntegraComLivroDeISS = dataReader["integra_iss"].ToString() == "S";
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

        public List<TipoDocumentoGlobus> Listar(string empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<TipoDocumentoGlobus> lista = new List<TipoDocumentoGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodigoEmpresa, CodigoFl, CodTpDoc, DescTpDoc, qtd_decimais, integra_iss");
                query.Append("  from cprtpdoc ");
                query.Append("  Where Lpad(codigoempresa,3,'0') || '/' || lPad(codigofl,3,'0') = '" + empresa + "'");
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        TipoDocumentoGlobus _tipo = new TipoDocumentoGlobus();
                        _tipo.Existe = true;
                        _tipo.Codigo = dataReader["CodTpDoc"].ToString(); ;
                        _tipo.CodigoEmpresa = Convert.ToInt32(dataReader["CodigoEmpresa"].ToString());
                        _tipo.CodigoFilial = Convert.ToInt32(dataReader["CodigoFl"].ToString());
                        _tipo.Descricao = dataReader["DescTpDoc"].ToString();
                        _tipo.QuantidadeCasasDecimais = Convert.ToInt32(dataReader["qtd_decimais"].ToString());
                        _tipo.IntegraComLivroDeISS = dataReader["integra_iss"].ToString() == "S";

                        lista.Add(_tipo);
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
            return lista;
        }
    }
}
