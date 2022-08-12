using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class DistanciaCorridaDAO
    {
        IDataReader dataReader;

        public List<DistanciaCorrida> Listar(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<DistanciaCorrida> _lista = new List<DistanciaCorrida>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select iddistancia, idcorrida, km");
                query.Append("  from NIFF_CHM_Distancias");
                query.Append(" where idcorrida = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        DistanciaCorrida _tipo = new DistanciaCorrida();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["iddistancia"].ToString());
                        _tipo.IdCorrida = Convert.ToInt32(dataReader["idCorrida"].ToString());
                        _tipo.Km = Convert.ToInt32(dataReader["km"].ToString());                        

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

        public bool Gravar(List<DistanciaCorrida> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            try
            {
                foreach (var tipo in _lista)
                {
                    query.Clear();
                    query.Append("Insert into NIFF_CHM_Distancias");
                    query.Append(" ( iddistancia, idcorrida, km )");
                    query.Append(" Values ( SQ_NIFF_CHMIdDistancia.NextVal");
                    query.Append("        , " + tipo.IdCorrida);
                    query.Append("        , " + tipo.Km);
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

        public bool Excluir(int idCorrida)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete NIFF_CHM_Distancias");
                query.Append(" Where idcorrida = " + idCorrida);
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
