using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class BolaoTimesDAO
    {
        IDataReader dataReader;
        
        public List<BolaoTimes> Listar(int Ano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<BolaoTimes> _lista = new List<BolaoTimes>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select t.id, t.nome, t.bandeira, t.ano, t.grupo, t.sigla");
                query.Append("  from Niff_bol_Times t     ");                
                query.Append(" Where t.Ano = " + Ano);
                query.Append("   And T.grupo in ('A','B','C','D','E','F','G','H')");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        BolaoTimes _tipo = new BolaoTimes();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();

                        try
                        {
                            _tipo.Ano = Convert.ToInt32(dataReader["Ano"].ToString());
                        }
                        catch { }

                        _tipo.Grupo = dataReader["Grupo"].ToString();
                        _tipo.Sigla = dataReader["Sigla"].ToString();

                        try
                        {
                            _tipo.Bandeira = (byte[])(dataReader["bandeira"]);
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

        public BolaoTimes Consultar(int Ano, string Sigla)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            BolaoTimes _tipo = new BolaoTimes();

            try
            {
                query.Append("Select t.id, t.nome, t.bandeira, t.ano, t.grupo, t.sigla");
                query.Append("  from Niff_bol_Times t     ");
                query.Append(" Where t.Ano = " + Ano );
                query.Append("   And t.Sigla = '" + Sigla + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();

                        try
                        {
                            _tipo.Ano = Convert.ToInt32(dataReader["Ano"].ToString());
                        }
                        catch { }

                        _tipo.Grupo = dataReader["Grupo"].ToString();
                        _tipo.Sigla = dataReader["Sigla"].ToString();

                        try
                        {
                            _tipo.Bandeira = (byte[])(dataReader["bandeira"]);
                        }
                        catch { }

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

        public BolaoTimes Consultar(int Id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            BolaoTimes _tipo = new BolaoTimes();

            try
            {
                query.Append("Select t.id, t.nome, t.bandeira, t.ano, t.grupo, t.sigla");
                query.Append("  from Niff_bol_Times t     ");
                query.Append(" Where t.id = " + Id);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();

                        try
                        {
                            _tipo.Ano = Convert.ToInt32(dataReader["Ano"].ToString());
                        }
                        catch { }

                        _tipo.Grupo = dataReader["Grupo"].ToString();
                        _tipo.Sigla = dataReader["Sigla"].ToString();

                        try
                        {
                            _tipo.Bandeira = (byte[])(dataReader["bandeira"]);
                        }
                        catch { }

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

        public bool Grava(BolaoTimes times)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            OracleParameter parametro = new OracleParameter();
            List<OracleParameter> parametros = new List<OracleParameter>();

            try
            {
                if (!times.Existe)
                {
                    
                    query.Clear();
                    query.Append("Insert into Niff_bol_Times");
                    query.Append("   (id, nome, ano, grupo, sigla, bandeira ");

                    query.Append("  ) Values ( (Select nvl(Max(Id),1)+1 From Niff_bol_Times ) " );
                    query.Append(", '" + times.Nome + "'");
                    query.Append(", " + times.Ano);
                    query.Append(", '" + times.Grupo + "'");
                    query.Append(", '" + times.Sigla + "'");

                    if (times.Bandeira == null)
                    {
                        query.Append(", null ");
                    }
                    else
                    {
                        query.Append(", :pfoto ");
                        parametro.ParameterName = ":pfoto";
                        parametro.Value = times.Bandeira;
                        parametro.OracleType = OracleType.Blob;
                        parametros.Add(parametro);
                    }
                                        
                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_bol_Times");
                    query.Append("   set nome = '" + times.Nome + "'");
                    query.Append("     , Ano = " + times.Ano );
                    query.Append("     , Grupo = '" + times.Grupo + "'");
                    query.Append("     , Sigla = '" + times.Sigla + "'");

                    if (times.Bandeira == null)
                    {
                        query.Append("     , Bandeira = null ");
                    }
                    else
                    {
                        query.Append("     , Bandeira = :pfoto ");
                        parametro.ParameterName = ":pfoto";
                        parametro.Value = times.Bandeira;
                        parametro.OracleType = OracleType.Blob;
                        parametros.Add(parametro);
                    }
                    query.Append(" Where Id = " + times.Id);
                }

                return sessao.ExecuteSqlTransaction(query.ToString(), parametros.ToArray());
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

        public bool Exclui(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_bol_Times");
                query.Append(" Where id = " + id);

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
