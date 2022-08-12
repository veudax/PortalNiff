using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class FornecedoresGlobusDAO
    {
        IDataReader dataReader;

        public FornecedoresGlobus Consulta(decimal codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            FornecedoresGlobus _tipo = new FornecedoresGlobus();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodigoForn, NrForn, RSocialForn, NFantasiaForn, CondicaoForn");
                query.Append("  from bgm_fornecedor ");
                query.Append("  Where CodigoForn = " + codigo );

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Numero = dataReader["NrForn"].ToString();
                        _tipo.CodigoFornecedor = Convert.ToDecimal(dataReader["CodigoForn"].ToString());
                        _tipo.NomeFantasia = dataReader["NFantasiaForn"].ToString();
                        _tipo.RazaoSocial = dataReader["RSocialForn"].ToString();
                        _tipo.Ativo = dataReader["CondicaoForn"].ToString() != "I";
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

        public FornecedoresGlobus Consulta(string codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            FornecedoresGlobus _tipo = new FornecedoresGlobus();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodigoForn, NrForn, RSocialForn, NFantasiaForn, CondicaoForn");
                query.Append("  from bgm_fornecedor ");
                query.Append("  Where NrForn = '" + codigo + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Numero = dataReader["NrForn"].ToString(); 
                        _tipo.CodigoFornecedor = Convert.ToDecimal(dataReader["CodigoForn"].ToString());
                        _tipo.NomeFantasia = dataReader["NFantasiaForn"].ToString();
                        _tipo.RazaoSocial = dataReader["RSocialForn"].ToString();
                        _tipo.Ativo = dataReader["CondicaoForn"].ToString() != "I";
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

        public List<FornecedoresGlobus> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<FornecedoresGlobus> lista = new List<FornecedoresGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodigoForn, NrForn, RSocialForn, NFantasiaForn, CondicaoForn");
                query.Append("  from bgm_fornecedor ");
                query.Append(" Where CondicaoForn <> 'I'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        FornecedoresGlobus _tipo = new FornecedoresGlobus();
                        _tipo.Existe = true;
                        _tipo.Numero = dataReader["NrForn"].ToString();
                        _tipo.CodigoFornecedor = Convert.ToDecimal(dataReader["CodigoForn"].ToString());
                        _tipo.NomeFantasia = dataReader["NFantasiaForn"].ToString();
                        _tipo.RazaoSocial = dataReader["RSocialForn"].ToString();
                        _tipo.Ativo = dataReader["CondicaoForn"].ToString() != "I";

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
