using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ModuloDAO
    {
        IDataReader moduloReader;

        public List<Modulo> Listar(int idCategoria, bool somenteAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Modulo> _lista = new List<Modulo>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select m.IdCategoria ");
                query.Append("     , m.IdModulo");
                query.Append("     , m.Nome");
                query.Append("     , m.ativo");
                query.Append("     , m.Fornecedor");
                query.Append("     , c.Descricao as Categoria");

                query.Append("  From NIFF_CHM_Modulos m, niff_chm_categorias c ");
                query.Append(" Where c.Idcategoria(+) = m.Idcategoria");

                if (idCategoria != 0)
                    query.Append("   and m.Idcategoria = " + idCategoria);

                if (somenteAtivos)
                    query.Append("   and m.Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                moduloReader = executar.ExecuteQuery();

                using (moduloReader)
                {
                    while (moduloReader.Read())
                    {
                        Modulo _modulo = new Modulo();

                        _modulo.IdCategoria = Convert.ToInt32(moduloReader["IdCategoria"].ToString());
                        _modulo.IdModulo = Convert.ToInt32(moduloReader["IdModulo"].ToString());

                        _modulo.Nome = moduloReader["Nome"].ToString();
                        _modulo.Ativo = moduloReader["ativo"].ToString() == "S";
                        _modulo.Fornecedor = moduloReader["Fornecedor"].ToString();
                        _modulo.Categoria = moduloReader["Categoria"].ToString();
                        _modulo.Existe = true;

                        _lista.Add(_modulo);
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

        public Modulo Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Modulo _modulo = new Modulo();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IdCategoria ");
                query.Append("     , IdModulo");
                query.Append("     , Nome");
                query.Append("     , ativo");
                query.Append("     , Fornecedor");

                query.Append("  From NIFF_CHM_Modulos ");
                query.Append(" Where IdModulo = " + codigo.ToString());

                Query executar = sessao.CreateQuery(query.ToString());

                moduloReader = executar.ExecuteQuery();

                using (moduloReader)
                {
                    if (moduloReader.Read())
                    {
                        _modulo.IdCategoria = Convert.ToInt32(moduloReader["IdCategoria"].ToString());
                        _modulo.IdModulo = Convert.ToInt32(moduloReader["IdModulo"].ToString());

                        _modulo.Nome = moduloReader["Nome"].ToString();
                        _modulo.Ativo = moduloReader["ativo"].ToString() == "S";
                        _modulo.Fornecedor = moduloReader["Fornecedor"].ToString();
                        _modulo.Existe = true;
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
            return _modulo;
        }

        public bool Grava(Modulo modulo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!modulo.Existe)
                {
                    query.Clear();
                    query.Append("Insert into NIFF_CHM_Modulos");
                    query.Append("   (idcategoria, idModulo, nome, ativo, Fornecedor) ");
                    query.Append("   Values (" + modulo.IdCategoria);
                    query.Append(", " + modulo.IdModulo );
                    query.Append(", '" + modulo.Nome + "'");
                    query.Append(", '" + (modulo.Ativo ? "S" : "N") + "'");
                    query.Append(", '" + modulo.Fornecedor + "')");
                }
                else
                {
                    query.Clear();
                    query.Append("Update NIFF_CHM_Modulos");
                    query.Append("   set nome = '" + modulo.Nome + "', ");
                    query.Append("       ativo = '" + (modulo.Ativo ? "S" : "N") + "', ");
                    query.Append("       Fornecedor = '" + modulo.Fornecedor + "'");
                    query.Append(" Where idModulo = " + modulo.IdModulo);
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

        public bool Exclui(Modulo modulo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (modulo.IdModulo != 0)
                {
                    query.Append("Delete NIFF_CHM_Modulos");
                    query.Append(" Where idModulo = " + modulo.IdModulo);
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

        public bool ExcluiTodosOsModulosDaCategoria(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                
                query.Append("Delete NIFF_CHM_Modulos");
                query.Append(" Where idCategoria = " + codigo);

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

                query.Append("Select Max(IdModulo) + 1 next From niff_chm_Modulos");
                Query executar = sessao.CreateQuery(query.ToString());

                moduloReader = executar.ExecuteQuery();

                using (moduloReader)
                {
                    if (moduloReader.Read())
                        retorno = Convert.ToInt32(moduloReader["next"].ToString());
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
