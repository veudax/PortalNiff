using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class VaraDAO
    {
        IDataReader varaReader;

        public List<Vara> Listar(bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Vara> _lista = new List<Vara>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idvara, nome, ativa");
                query.Append("  from Niff_Jur_Vara");
                if (apenasAtivos)
                    query.Append(" Where ativa = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                varaReader = executar.ExecuteQuery();

                using (varaReader)
                {
                    while (varaReader.Read())
                    {
                        Vara _tipo = new Vara();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(varaReader["idvara"].ToString());
                        _tipo.Descricao = varaReader["Nome"].ToString();
                        _tipo.Ativo = varaReader["Ativa"].ToString() == "S";

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

        public Vara Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Vara _tipo = new Vara();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idvara, nome, ativa");
                query.Append("  from Niff_Jur_Vara");
                query.Append(" Where idvara = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                varaReader = executar.ExecuteQuery();

                using (varaReader)
                {
                    if (varaReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(varaReader["idvara"].ToString());
                        _tipo.Descricao = varaReader["Nome"].ToString();
                        _tipo.Ativo = varaReader["Ativa"].ToString() == "S";
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

                query.Append("Select Max(idVara) +1 next From Niff_Jur_Vara");
                Query executar = sessao.CreateQuery(query.ToString());

                varaReader = executar.ExecuteQuery();

                using (varaReader)
                {
                    if (varaReader.Read())
                        retorno = Convert.ToInt32(varaReader["next"].ToString());
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

        public bool Gravar(Vara tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into Niff_Jur_Vara");
                    query.Append(" ( idvara, nome, ativa )");
                    query.Append(" Values (" + tipo.Id);
                    query.Append("        ,'" + tipo.Descricao + "'");
                    query.Append("        ,'" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_Jur_Vara");
                    query.Append("   set nome = '" + tipo.Descricao + "'");
                    query.Append("     , ativa = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append(" Where idvara = " + tipo.Id);
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

        public bool Excluir(Vara tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Jur_Vara");
                query.Append(" Where idvara = " + tipo.Id);
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
