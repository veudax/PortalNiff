using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class MetasDoCargoDAO
    {
        IDataReader dataReader;

        public List<MetasDoCargo> Listar(int cargo, string tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<MetasDoCargo> _lista = new List<MetasDoCargo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select 'S' Marcado, c.Descricao, c.IdMetas, cc.Idcargo, c.tipo, cc.Peso");
                query.Append("  From Niff_ADS_MetasDoCargo CC");
                query.Append("     , niff_ads_Metas C");
                query.Append(" Where cc.IdMetas = c.IdMetas");
                query.Append("   And cc.Idcargo = " + cargo);
                query.Append("   And c.tipo = '" + tipo + "'");
                query.Append("   And c.ativo = 'S'");
                query.Append("   And c.USAPARAAVALIACAO = 'S'");
                query.Append(" Union All ");
                query.Append("Select 'N' Marcado, c.Descricao, c.IdMetas, Null IdCargo, c.tipo, 0 Peso");
                query.Append("  From niff_ads_Metas C");
                query.Append(" Where C.IdMetas Not In (Select IdMetas ");
                query.Append("                           From Niff_ADS_MetasDoCargo cc ");
                query.Append("                          Where cc.Idcargo = " + cargo + ")");
                query.Append("   And c.tipo = '" + tipo + "'");
                query.Append("   And c.ativo = 'S'");
                query.Append("   And c.USAPARAAVALIACAO = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        MetasDoCargo _tipo = new MetasDoCargo();

                        _tipo.Existe = true;
                        _tipo.IdMetas = Convert.ToInt32(dataReader["IdMetas"].ToString());

                        try
                        {
                            _tipo.IdCargo = Convert.ToInt32(dataReader["Idcargo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Peso = Convert.ToInt32(dataReader["Peso"].ToString());
                        }
                        catch { }

                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Marcado = dataReader["Marcado"].ToString() == "S";

                        _lista.Add(_tipo);
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

        public bool Gravar(List<MetasDoCargo> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            try
            {
                foreach (var item in _lista)
                {
                    query.Clear();

                    query.Append("Insert into Niff_ADS_MetasDoCargo");
                    query.Append(" ( idassoc, IdMetas, idcargo, Peso)");
                    query.Append(" Values ( SQ_NIFF_AdsIdMetasCargo.NextVal ");
                    query.Append("        , " + item.IdMetas);
                    query.Append("        ," + item.IdCargo);
                    query.Append("        ," + item.Peso);
                    query.Append(" )");

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                        break;
                }

                return retorno;
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

        public bool Excluir(int cargo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_ADS_MetasDoCargo");
                query.Append(" Where IdCargo = " + cargo);
                return sessao.ExecuteSqlTransaction(query.ToString());
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
