using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class CategoriaLivrosDAO
    {
        IDataReader dataReader;

        public List<CategoriaLivros> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<CategoriaLivros> _lista = new List<CategoriaLivros>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IdCategLivros ");
                query.Append("     , Descricao");
                query.Append("     , ativo");

                query.Append("  From Niff_Bib_Categorias ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        CategoriaLivros _categoria = new CategoriaLivros();
                        _categoria.Id = Convert.ToInt32(dataReader["IdCategLivros"].ToString());

                        _categoria.Descricao = dataReader["Descricao"].ToString();
                        _categoria.Ativo = dataReader["ativo"].ToString() == "S";
                        _categoria.Existe = true;

                        _lista.Add(_categoria);
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

        public CategoriaLivros Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            CategoriaLivros _categoria = new CategoriaLivros();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IdCategLivros ");
                query.Append("     , Descricao");
                query.Append("     , ativo");

                query.Append("  From Niff_Bib_Categorias ");
                query.Append(" Where IdCategLivros = " + codigo.ToString());

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _categoria.Id = Convert.ToInt32(dataReader["IdCategLivros"].ToString());

                        _categoria.Descricao = dataReader["Descricao"].ToString();
                        _categoria.Ativo = dataReader["ativo"].ToString() == "S";
                        _categoria.Existe = true;
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
            return _categoria;
        }

        public bool Grava(CategoriaLivros categoria)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!categoria.Existe)
                {
                    query.Clear();
                    query.Append("Insert into Niff_Bib_Categorias");
                    query.Append("   (IdCategLivros, descricao, ativo) ");
                    query.Append("   Values (" + categoria.Id);
                    query.Append(", '" + categoria.Descricao + "'");
                    query.Append(", '" + (categoria.Ativo ? "S" : "N") + "'");
                    query.Append(")");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Bib_Categorias");
                    query.Append("   set descricao = '" + categoria.Descricao + "', ");
                    query.Append("       ativo = '" + (categoria.Ativo ? "S" : "N") + "'");
                    query.Append(" Where IdCategLivros = " + categoria.Id);
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

        public bool Exclui(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Bib_Categorias");
                query.Append(" Where IdCategLivros = " + codigo);

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

        public int Proximo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Max(IdCategLivros) + 1 next From Niff_Bib_Categorias");
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
    }
}
