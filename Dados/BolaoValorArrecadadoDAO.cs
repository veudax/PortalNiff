using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class BolaoValorArrecadadoDAO
    {
        IDataReader dataReader;

        public List<BolaoValorArrecadado> Listar(int Ano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<BolaoValorArrecadado> _lista = new List<BolaoValorArrecadado>();

            try
            {
                query.Append("Select Id, Data, IdColaborador, Valor ");
                query.Append("  from Niff_bol_ValorArrecadado ");
                query.Append(" Where data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {

                        BolaoValorArrecadado _tipo = new BolaoValorArrecadado();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdColaborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());

                        _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());                        

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
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

        public BolaoValorArrecadado Consultar(int Ano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            BolaoValorArrecadado _tipo = new BolaoValorArrecadado();

            try
            {
                query.Append("Select Sum(Valor) Valor ");
                query.Append("  from Niff_bol_ValorArrecadado ");
                query.Append(" Where data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {


                        _tipo.Existe = true;

                        _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());

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

        public BolaoValorArrecadado Consultar(int Ano, int IdColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            BolaoValorArrecadado _tipo = new BolaoValorArrecadado();

            try
            {
                query.Append("Select  Id, Data, IdColaborador, Valor  ");
                query.Append("  from Niff_bol_ValorArrecadado ");
                query.Append(" Where data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                query.Append("   And IdColaborador = " + IdColaborador);
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {


                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdColaborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());

                        _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
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

        public bool Grava(BolaoValorArrecadado times)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                if (!times.Existe)
                {

                    query.Clear();
                    query.Append("Insert into Niff_bol_ValorArrecadado");
                    query.Append("   (id, data, IdColaborador, Valor");
                    query.Append("  ) Values ( (Select nvl(Max(Id),1)+1 From Niff_bol_ValorArrecadado) ");
                    query.Append(", Sysdate");
                    query.Append(", " + times.IdColaborador);
                    query.Append(", " + times.Valor.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_bol_ValorArrecadado");
                    query.Append("   set Valor = " + times.Valor.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" Where Id = " + times.Id);
                }

                return sessao.ExecuteSqlTransaction(query.ToString(), null);

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
                query.Append("Delete Niff_bol_ValorArrecadado");
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
