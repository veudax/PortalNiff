using System;
using System.Configuration;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Classes;

namespace Dados
{  
    public class QueryCigam
    {
        private OracleCommand comando;
        public  string mensagemErroConexao;

        public QueryCigam(String sql, OracleConnection connection)
        {
            try
            {
                comando = connection.CreateCommand();
                comando.CommandText = sql;
                comando.CommandType = CommandType.Text;
            }
            catch (Exception ex)
            {
                comando.Dispose();
                Publicas.mensagemDeErro = ex.Message;
            }
        }

        public QueryCigam(String sql, OracleConnection connection, string conexao)
        {
            OracleTransaction trans = connection.BeginTransaction();
            try
            {
                comando = connection.CreateCommand();

                //comando.Connection = connection;
                //comando.Transaction = trans;

                comando = new OracleCommand(sql, connection, trans);
                comando.CommandType = CommandType.Text;
                comando.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                comando.Dispose();
                trans.Rollback();
                Publicas.mensagemDeErro = ex.Message;
            }
        }

        public DataSet ExecuteDataSet(string table)
        {
            OracleDataAdapter da = new OracleDataAdapter(comando);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public void ExecuteNonQuery()
        {
            comando.ExecuteNonQuery();
        }

        public OracleDataReader ExecuteQuery()
        {
            try
            {
                comando.Prepare();
            }
            catch
            {
            }
            return comando.ExecuteReader();           
        }

        public DataTable fillDataTable(string table)
        {
            OracleDataAdapter da = new OracleDataAdapter(comando);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public QueryCigam SetParameter(String nome, object valor, OracleType tipo)
        {
            var parametro = comando.CreateParameter();
            parametro.ParameterName = nome;
            parametro.Value = valor;
            parametro.OracleType = tipo;
            comando.Parameters.Add(parametro);
            return this;
        }

    }

    public class SessaoCigam
    {
        // mudar para a tela de login
        //public string stringConexaoSigom = ConfigurationManager.ConnectionStrings["OraConnStr"].ConnectionString;

        private OracleConnection conexao;

        public SessaoCigam()
        {
            try
            {
                  conexao = new OracleConnection(Publicas.stringConexaoCigam);

                if (conexao.State == ConnectionState.Closed)
                    conexao.Open();

            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = "Problemas ao conectar com o banco de dados. " + ex.Message;
            }
        }

        public void Desconectar()
        {
            if (conexao != null)
            {
                if (conexao.State == ConnectionState.Open)
                    conexao.Close();
            }
        }

        public QueryCigam CreateQuery(String sql)
        {
            return new QueryCigam(sql, conexao);
        }

        public QueryCigam QueryCalculo(string formula)
        {
            return new QueryCigam("Select trunc(" + formula + ",4) Resultado From Dual", conexao);
        }

        

        public QueryCigam ExecuteTransaction(string query)
        {
            return new QueryCigam(query, conexao, string.Empty);
        }

        public bool ExecuteSqlTransaction(string query, OracleParameter[] parametros = null)
        {
            OracleCommand command = conexao.CreateCommand();
            OracleTransaction transaction;
            int i = 0;
            transaction = conexao.BeginTransaction();

            command.Connection = conexao;
            command.Transaction = transaction;

            try
            {
                command.CommandText = query;

                if (parametros != null)
                    command.Parameters.AddRange(parametros);

                command.ExecuteNonQuery();

                if (parametros != null)
                {
                    i = command.Parameters.Count;
                    
                    while (command.Parameters.Count > 0)
                    {
                        command.Parameters.Remove(parametros[i-1]);
                        i--;
                    }
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    Publicas.mensagemDeErro = ex.Message;
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    Publicas.mensagemDeErro = ex.Message + Environment.NewLine + ex2.Message;
                }
                return false;
            }
        }
    }
}
