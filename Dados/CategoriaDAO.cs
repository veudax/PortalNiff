using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class CategoriaDAO
    {
        IDataReader categoriaReader;

        public List<Categoria> Listar(bool somenteAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Categoria> _lista = new List<Categoria>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IdCategoria ");
                query.Append("     , Descricao");
                query.Append("     , ativo");
                query.Append("     , possuiModulos");

                query.Append("  From NIFF_CHM_Categorias ");

                if (somenteAtivos)
                    query.Append(" Where Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                categoriaReader = executar.ExecuteQuery();

                using (categoriaReader)
                {
                    while (categoriaReader.Read())
                    {
                        Categoria _categoria = new Categoria();
                        _categoria.IdCategoria = Convert.ToInt32(categoriaReader["IdCategoria"].ToString());

                        _categoria.Descricao = categoriaReader["Descricao"].ToString();
                        _categoria.Ativo = categoriaReader["ativo"].ToString() == "S";
                        _categoria.PossuiModulos = categoriaReader["possuiModulos"].ToString() == "S";
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

        public Categoria Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Categoria _categoria = new Categoria();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IdCategoria ");
                query.Append("     , Descricao");
                query.Append("     , ativo");
                query.Append("     , possuiModulos");

                query.Append("  From NIFF_CHM_Categorias ");
                query.Append(" Where IdCategoria = " + codigo.ToString());

                Query executar = sessao.CreateQuery(query.ToString());

                categoriaReader = executar.ExecuteQuery();

                using (categoriaReader)
                {
                    if (categoriaReader.Read())
                    {
                        _categoria.IdCategoria = Convert.ToInt32(categoriaReader["IdCategoria"].ToString());

                        _categoria.Descricao = categoriaReader["Descricao"].ToString();
                        _categoria.Ativo = categoriaReader["ativo"].ToString() == "S";
                        _categoria.PossuiModulos = categoriaReader["possuiModulos"].ToString() == "S";
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

        public bool Grava(Categoria categoria)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Categoria _empresa = new Categoria();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!categoria.Existe)
                {
                    query.Clear();
                    query.Append("Insert into NIFF_CHM_Categorias");
                    query.Append("   (idcategoria, descricao, ativo, possuiModulos) ");
                    query.Append("   Values (" + categoria.IdCategoria);
                    query.Append(", '" + categoria.Descricao + "'");
                    query.Append(", '" + (categoria.Ativo ? "S" : "N") + "'");
                    query.Append(", '" + (categoria.PossuiModulos ? "S" : "N")  + "')");
                }
                else
                {
                    query.Clear();
                    query.Append("Update NIFF_CHM_Categorias");
                    query.Append("   set descricao = '" + categoria.Descricao + "', ");
                    query.Append("       ativo = '" + (categoria.Ativo ? "S" : "N") + "', ");
                    query.Append("       PossuiModulos = '" + (categoria.PossuiModulos ? "S" : "N") + "'");
                    query.Append(" Where idcategoria = " + categoria.IdCategoria);
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

        public bool Exclui(Categoria categoria)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Categoria _empresa = new Categoria();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (categoria.IdCategoria != 0)
                {
                    query.Append("Delete NIFF_CHM_Categorias");
                    query.Append(" Where idcategoria = " + categoria.IdCategoria);
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

        public int Proximo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select nvl(Max(IdCategoria),0) + 1 next From niff_chm_categorias");
                Query executar = sessao.CreateQuery(query.ToString());

                categoriaReader = executar.ExecuteQuery();

                using (categoriaReader)
                {
                    if (categoriaReader.Read())
                        retorno = Convert.ToInt32(categoriaReader["next"].ToString());
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
