using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class EscolaridadeDAO
    {
        IDataReader dataReader;

        public List<Escolaridade> Listar(bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Escolaridade> _lista = new List<Escolaridade>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select IdEscola, Descricao, ativa");
                query.Append("  from Niff_ADS_Escolaridade");
                if (apenasAtivos)
                    query.Append(" Where ativa = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Escolaridade _tipo = new Escolaridade();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["IdEscola"].ToString());
                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Ativo = dataReader["Ativa"].ToString() == "S";

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

        public Escolaridade Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Escolaridade _tipo = new Escolaridade();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select IdEscola, Descricao, ativa");
                query.Append("  from Niff_ADS_Escolaridade");
                query.Append(" Where IdEscola = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["IdEscola"].ToString());
                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Ativo = dataReader["Ativa"].ToString() == "S";
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

                query.Append("Select Max(IdEscola) +1 next From Niff_ADS_Escolaridade");
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

        public bool Gravar(Escolaridade tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into Niff_ADS_Escolaridade");
                    query.Append(" ( IdEscola, Descricao, ativa )");
                    query.Append(" Values (" + tipo.Id);
                    query.Append("        ,'" + tipo.Descricao + "'");
                    query.Append("        ,'" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_ADS_Escolaridade");
                    query.Append("   set Descricao = '" + tipo.Descricao + "'");
                    query.Append("     , ativa = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append(" Where IdEscola = " + tipo.Id);
                }

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

        public bool Excluir(Escolaridade tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_ADS_Escolaridade");
                query.Append(" Where IdEscola = " + tipo.Id);
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
