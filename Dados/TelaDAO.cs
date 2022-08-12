using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class TelaDAO
    {
        IDataReader categoriaReader;

        public List<Tela> Listar(int idModulo, bool somenteAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Tela> _lista = new List<Tela>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select idtela, idmodulo, lower(nome) nome, lower(caminho) caminho, ativo, tipo");
                query.Append("  From Niff_Chm_Telas ");

                if (idModulo != 0)
                {
                    query.Append(" Where IdModulo = " + idModulo);
                    if (somenteAtivos)
                        query.Append("   And Ativo = 'S'");
                }
                else
                {
                    if (somenteAtivos)
                        query.Append(" Where Ativo = 'S'");
                }

                Query executar = sessao.CreateQuery(query.ToString());

                categoriaReader = executar.ExecuteQuery();

                using (categoriaReader)
                {
                    while (categoriaReader.Read())
                    {
                        Tela _tela = new Tela();
                        _tela.IdTela = Convert.ToInt32(categoriaReader["idtela"].ToString());
                        _tela.IdModulo = Convert.ToInt32(categoriaReader["idmodulo"].ToString());

                        _tela.Nome = categoriaReader["Nome"].ToString();
                        _tela.Caminho = categoriaReader["caminho"].ToString();

                        _tela.Ativo = categoriaReader["ativo"].ToString() == "S";
                        _tela.Tipo = (categoriaReader["tipo"].ToString() == "C" ? Publicas.TipoDeTela.Cadastro :
                                     (categoriaReader["tipo"].ToString() == "R" ? Publicas.TipoDeTela.Relatorio :
                                     (categoriaReader["tipo"].ToString() == "M" ? Publicas.TipoDeTela.Movimentacao :
                                     (categoriaReader["tipo"].ToString() == "I" ? Publicas.TipoDeTela.Integracao : Publicas.TipoDeTela.GeracaoDeArquivo))));

                        _tela.NomeCompleto = _tela.Nome + (_tela.Caminho == "" ? "" :
                            " => ( " + _tela.Caminho + " )");
                        _tela.Existe = true;

                        _lista.Add(_tela);
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

        public Tela Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Tela _tela = new Tela();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select idtela, idmodulo, nome, caminho, ativo, tipo");
                query.Append("  From Niff_Chm_Telas ");
                query.Append(" Where IdTela = " + codigo.ToString() ); 

                Query executar = sessao.CreateQuery(query.ToString());

                categoriaReader = executar.ExecuteQuery();

                using (categoriaReader)
                {
                    if (categoriaReader.Read())
                    {
                        _tela.IdTela = Convert.ToInt32(categoriaReader["idtela"].ToString());
                        _tela.IdModulo = Convert.ToInt32(categoriaReader["idmodulo"].ToString());

                        _tela.Nome = categoriaReader["Nome"].ToString();
                        _tela.Caminho = categoriaReader["caminho"].ToString();

                        _tela.Ativo = categoriaReader["ativo"].ToString() == "S";
                        _tela.Tipo = (categoriaReader["tipo"].ToString() == "C" ? Publicas.TipoDeTela.Cadastro :
                                     (categoriaReader["tipo"].ToString() == "R" ? Publicas.TipoDeTela.Relatorio :
                                     (categoriaReader["tipo"].ToString() == "M" ? Publicas.TipoDeTela.Movimentacao :
                                     (categoriaReader["tipo"].ToString() == "I" ? Publicas.TipoDeTela.Integracao : Publicas.TipoDeTela.GeracaoDeArquivo))));
                        _tela.NomeCompleto = _tela.Nome + (_tela.Caminho == "" ? "" : 
                            " => ( " + _tela.Caminho + " )");

                        _tela.Existe = true;
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
            return _tela;
        }

        public bool Grava(Tela tela)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!tela.Existe)
                {
                    query.Clear();
                    query.Append("Insert into Niff_Chm_Telas");
                    query.Append("   (idtela, idmodulo, nome, caminho, ativo, tipo) ");
                    query.Append("   Values (" + tela.IdTela);
                    query.Append("        , " + tela.IdModulo );
                    query.Append("        , '" + tela.Nome + "'");
                    query.Append("        , '" + tela.Caminho + "'");
                    query.Append("        , '" + (tela.Ativo ? "S" : "N") + "'");
                    query.Append("        , '" + (tela.Tipo == Publicas.TipoDeTela.Cadastro ? "C" :
                                         (tela.Tipo == Publicas.TipoDeTela.GeracaoDeArquivo ? "G" :
                                         (tela.Tipo == Publicas.TipoDeTela.Integracao ? "I" :
                                         (tela.Tipo == Publicas.TipoDeTela.Movimentacao ? "M" : "R")))) + "')");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Chm_Telas");
                    query.Append("   set Nome = '" + tela.Nome + "'");
                    query.Append("     , idmodulo = " + tela.IdModulo + "");
                    query.Append("     , caminho = '" + tela.Caminho + "'");
                    query.Append("     , ativo = '" + (tela.Ativo ? "S" : "N") + "'");
                    query.Append("     , tipo = '" + (tela.Tipo == Publicas.TipoDeTela.Cadastro ? "C" :
                                         (tela.Tipo == Publicas.TipoDeTela.GeracaoDeArquivo ? "G" :
                                         (tela.Tipo == Publicas.TipoDeTela.Integracao ? "I" :
                                         (tela.Tipo == Publicas.TipoDeTela.Movimentacao ? "M" : "R")))) + "'");
                    query.Append(" Where idtela = " + tela.IdTela);
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

        public bool Exclui(Tela tela)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (tela.IdTela != 0)
                {
                    query.Append("Delete Niff_Chm_Telas");
                    query.Append(" Where idTela = " + tela.IdTela);
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

                query.Append("Select Max(IdTela) + 1 next From Niff_Chm_Telas");
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
