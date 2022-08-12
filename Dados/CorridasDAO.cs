using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class CorridasDAO
    {
        IDataReader dataReader;

        public List<Corridas> Listar(bool apenasAtivos, int usuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Corridas> _lista = new List<Corridas>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.idCorrida, c.data, c.nome, c.local, c.linkweb, c.ativo, c.valor, c.prazoinscricao, null idUsuario, c.ValorGrupo");
                query.Append("  from NIFF_CHM_Corridas c     ");
                if (apenasAtivos)
                    query.Append(" Where ativo = 'S'");

                if (usuario != 0)
                {
                    query.Append(" Union ALL ");
                    query.Append("Select c.idCorrida, c.data, c.nome, c.local, c.linkweb, c.ativo, c.valor, c.prazoinscricao, p.idUsuario, c.ValorGrupo");
                    query.Append("  from NIFF_CHM_Corridas c     ");
                    query.Append("     , NIFF_CHM_Participantes p");
                    query.Append("     , Niff_Chm_Distancias d   ");
                    query.Append(" Where c.IdCorrida = d.IdCorrida(+)");
                    query.Append("   and d.IdDistancia = p.IdDistancia(+)");

                    if (apenasAtivos)
                    {
                        query.Append("  and ativo = 'S'");
                        query.Append("  and idUsuario = " + usuario);
                    }
                    query.Append(" Order by IdUsuario");

                }                

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Corridas _tipo = new Corridas();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idCorrida"].ToString());

                        try
                        {
                            _tipo.IdUsuario = Convert.ToInt32(dataReader["idUsuario"].ToString());
                        }
                        catch { }

                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Local = dataReader["Local"].ToString();
                        _tipo.LinkWeb = dataReader["linkweb"].ToString();
                        _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        _tipo.PrazoLimite = Convert.ToDateTime(dataReader["prazoinscricao"].ToString());
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        try
                        {
                            _tipo.ValorGrupo = Convert.ToDecimal(dataReader["ValorGrupo"].ToString());
                        }
                        catch { }
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

        public Corridas Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Corridas _tipo = new Corridas();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idcorrida, data, nome, local, linkweb, ativo, valor, prazoinscricao, ValorGrupo");
                query.Append("  from NIFF_CHM_Corridas");
                query.Append(" Where idcorrida = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idCorrida"].ToString());
                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Local = dataReader["Local"].ToString();
                        _tipo.LinkWeb = dataReader["linkweb"].ToString();
                        _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        _tipo.PrazoLimite = Convert.ToDateTime(dataReader["prazoinscricao"].ToString());
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.ValorGrupo = Convert.ToDecimal(dataReader["ValorGrupo"].ToString());
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

        public int Proximo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Max(idcorrida) +1 next From NIFF_CHM_Corridas");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        retorno = Convert.ToInt32(dataReader["next"].ToString());
                }
                return retorno;
            }
            catch
            {
                return retorno;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool Gravar(Corridas tipo, List<DistanciaCorrida> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into NIFF_CHM_Corridas");
                    query.Append(" ( idcorrida, data, nome, local, linkweb, ativo, valor, prazoinscricao, ValorGrupo )");
                    query.Append(" Values (" + tipo.Id);
                    query.Append("        , To_date('" + tipo.Data.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        ,'" + tipo.Nome + "'");
                    query.Append("        ,'" + tipo.Local + "'");
                    query.Append("        ,'" + tipo.LinkWeb + "'");
                    query.Append("        ,'" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("        ," + tipo.Valor.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , To_date('" + tipo.PrazoLimite.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        ," + tipo.ValorGrupo.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" )");
                }
                else
                { 
                    query.Append("Update NIFF_CHM_Corridas");
                    query.Append("   set Nome = '" + tipo.Nome + "'");
                    query.Append("     , ativo = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("     , data = To_date('" + tipo.Data.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , local = '" + tipo.Local + "'");
                    query.Append("     , LinkWeb = '" + tipo.LinkWeb + "'");
                    query.Append("     , Valor = " + tipo.Valor.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , prazoinscricao = To_date('" + tipo.PrazoLimite.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , ValorGrupo = " + tipo.ValorGrupo.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" Where idcorrida = " + tipo.Id);

                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    if (tipo.Existe)
                    {
                        if (!new DistanciaCorridaDAO().Excluir(tipo.Id))
                            retorno = false;
                    }

                    if (!new DistanciaCorridaDAO().Gravar(_lista))
                        retorno = false;
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

        public bool Excluir(Corridas tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!new DistanciaCorridaDAO().Excluir(tipo.Id))
                    return false;

                query.Append("Delete NIFF_CHM_Corridas");
                query.Append(" Where idcorrida = " + tipo.Id);
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
