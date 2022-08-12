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
    public class LivrosDAO
    {
        IDataReader dataReader;

        public List<Livros> Sugestoes(bool ebook)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Livros> _lista = new List<Livros>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select l.Nome ");
                query.Append("  From niff_bib_livros l");
                query.Append(" Where Ativo = 'S'");
                if (ebook)
                    query.Append("  And l.eBook = 'S'");
                else
                    query.Append("  And l.fisico = 'S'");

                query.Append("   And l.idlivros Not In (Select idLivros ");
                query.Append("                            From niff_bib_emprestimo e");
                query.Append("                           Where e.Idcolaborador = " + Publicas._idColaborador + ")");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Livros _livros = new Livros();

                        _livros.Existe = true;
                        _livros.Nome = dataReader["Nome"].ToString();
                        _lista.Add(_livros);
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

        public int QuantidadeLivros(bool ebook)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            int qtd = 0;

            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select Count(*) qtd");
                query.Append("  From niff_bib_livros l");

                if (ebook)
                    query.Append(" Where l.eBook = 'S'");
                else
                    query.Append(" Where l.fisico = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        
                        qtd = Convert.ToInt32(dataReader["qtd"].ToString());
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
            return qtd;
        }

        public List<Livros> Listar(bool apenasAtivos, bool naoLidos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Livros> _lista = new List<Livros>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select l.idlivros, l.idcateglivros, l.idcolaborador, l.nome, l.nomeoriginal, l.dataregistro");
                query.Append("     , l.tipocessao, l.datadevolucao, l.conservacao, l.Imagem, l.sinopse, l.ativo, l.NomeArquivo");
                query.Append("     , l.Fisico, l.Ebook, l.AudioBook, l.LocalDeArmazenamento, l.QDownLoad");
                query.Append("  From niff_bib_livros l");

                if (naoLidos && apenasAtivos)
                {
                    query.Append(" Where l.Idlivros Not In (Select idLivros ");
                    query.Append("                            From Niff_Bib_Emprestimo e");
                    query.Append("                           Where e.idcolaborador = " + Publicas._idColaborador);
                    query.Append("                             And e.devolvido = 'S')");

                    if (apenasAtivos)
                        query.Append("  And Ativo = 'S'");
                }
                else
                    if (apenasAtivos)
                    query.Append(" Where Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Livros _livros = new Livros();

                        _livros.Existe = true;
                        _livros.Id = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.IdCategoria = Convert.ToInt32(dataReader["IdCategLivros"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livros.Ativo = dataReader["Ativo"].ToString() == "S";

                        _livros.Fisico = dataReader["Fisico"].ToString() == "S";
                        _livros.EBook = dataReader["Ebook"].ToString() == "S";
                        _livros.AudioBook = dataReader["AudioBook"].ToString() == "S";

                        _livros.NomeArquivo = dataReader["NomeArquivo"].ToString();
                        _livros.LocalArmazenamento = dataReader["LocalDeArmazenamento"].ToString();
                        _livros.QuantidadeDownload = Convert.ToInt32(dataReader["QDownLoad"].ToString());

                        _livros.Nome = dataReader["Nome"].ToString();
                        _livros.TipoCessao = dataReader["tipocessao"].ToString();
                        _livros.Conservacao = dataReader["conservacao"].ToString();

                        _livros.Sinopse = dataReader["Sinopse"].ToString();

                        try
                        {
                            _livros.Data = Convert.ToDateTime(dataReader["dataregistro"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataDevolucao = Convert.ToDateTime(dataReader["datadevolucao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.Imagem = (byte[])(dataReader["Imagem"]);
                        }
                        catch { }

                        _lista.Add(_livros);
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

        public Livros Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Livros _livros = new Livros();

            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select l.idlivros, l.idcateglivros, l.idcolaborador, l.nome, l.nomeoriginal, l.dataregistro");
                query.Append("     , l.tipocessao, l.datadevolucao, l.conservacao, l.Imagem, l.sinopse, l.ativo, l.NomeArquivo");
                query.Append("     , l.Fisico, l.Ebook, l.AudioBook, l.LocalDeArmazenamento, l.QDownLoad, e.Arquivo");
                query.Append("  From niff_bib_livros l, niff_bib_eBook e");
                query.Append(" Where l.idlivros = " + codigo);
                query.Append("   and l.Idlivros = e.Idlivros(+)");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _livros.Existe = true;
                        _livros.Id = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.IdCategoria = Convert.ToInt32(dataReader["IdCategLivros"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livros.Ativo = dataReader["Ativo"].ToString() == "S";

                        _livros.Fisico = dataReader["Fisico"].ToString() == "S";
                        _livros.EBook = dataReader["Ebook"].ToString() == "S";
                        _livros.AudioBook = dataReader["AudioBook"].ToString() == "S";

                        _livros.NomeArquivo = dataReader["NomeArquivo"].ToString();
                        _livros.LocalArmazenamento = dataReader["LocalDeArmazenamento"].ToString();
                        _livros.QuantidadeDownload = Convert.ToInt32(dataReader["QDownLoad"].ToString());

                        _livros.Nome = dataReader["Nome"].ToString();
                        _livros.TipoCessao = dataReader["tipocessao"].ToString();
                        _livros.Conservacao = dataReader["conservacao"].ToString();
                        _livros.Sinopse = dataReader["Sinopse"].ToString();
                        try
                        {
                            _livros.Data = Convert.ToDateTime(dataReader["dataregistro"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataDevolucao = Convert.ToDateTime(dataReader["datadevolucao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.Imagem = (byte[])(dataReader["Imagem"]);
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
            return _livros;
        }

        public Ebook ConsultaEbook(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Ebook _livros = new Ebook();

            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select idlivros, arquivo");
                query.Append("  From niff_bib_eBook e");
                query.Append(" Where e.idlivros = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _livros.Existe = true;
                        _livros.IdLivros = Convert.ToInt32(dataReader["idlivros"].ToString());

                        try
                        {
                            _livros.Arquivo = (byte[])(dataReader["Arquivo"]);
                        }
                        catch
                        {
                        }
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
            return _livros;
        }

        public bool AtualizaQuantidadeDownload(int idLivro)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Update niff_bib_livros");
                query.Append("   set QDownLoad = QDownLoad + 1");
                query.Append(" Where idlivros = " + idLivro);

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

        public bool Grava(Livros _livros)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            OracleParameter parametro = new OracleParameter();
            List<OracleParameter> parametros = new List<OracleParameter>();
            bool retorno = true;

            try
            {
                if (!_livros.Existe)
                {
                    query.Clear();
                    query.Append("Insert into niff_bib_livros");
                    query.Append("   (idlivros, idcateglivros, idcolaborador, nome, tipocessao, datadevolucao, conservacao, DATAREGISTRO, Imagem, Sinopse, ativo ");
                    query.Append("   , Fisico, Ebook, AudioBook, LocalDeArmazenamento, QDownLoad, NomeArquivo");
                    query.Append("  ) Values (" + _livros.Id);
                    query.Append("   , " + _livros.IdCategoria);
                    query.Append("   , " + _livros.IdColaborador);
                    query.Append("   , '" + _livros.Nome + "'");
                    query.Append("   , '" + _livros.TipoCessao + "'");
                    if (_livros.TipoCessao == "P")
                        query.Append("   , null");
                    else
                        query.Append("   , To_date('" + _livros.DataDevolucao.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("   , '" + _livros.Conservacao + "'");
                    query.Append("   , SysDate");

                    if (_livros.Imagem == null)
                        query.Append("   , null");
                    else
                    {
                        query.Append(", :pfoto ");
                        parametro.ParameterName = ":pfoto";
                        parametro.Value = _livros.Imagem;
                        parametro.OracleType = OracleType.Blob;
                        parametros.Add(parametro);
                    }

                    query.Append("   , :psinopse");
                    query.Append("   , '" + (_livros.Ativo ? "S" : "N") + "'");
                    query.Append("   , '" + (_livros.Fisico ? "S" : "N") + "'");
                    query.Append("   , '" + (_livros.EBook ? "S" : "N") + "'");
                    query.Append("   , '" + (_livros.AudioBook ? "S" : "N") + "'");
                    query.Append("   , '" + _livros.LocalArmazenamento + "'");
                    query.Append("   , 0 ");
                    query.Append("   , '" + _livros.NomeArquivo + "'");

                    parametro = new OracleParameter();
                    parametro.ParameterName = ":psinopse";
                    parametro.Value = _livros.Sinopse;
                    parametro.OracleType = OracleType.VarChar;
                    parametros.Add(parametro);

                    query.Append("   )");
                }
                else
                {
                    query.Clear();
                    query.Append("Update niff_bib_livros");
                    query.Append("   set idcateglivros = " + _livros.IdCategoria);
                    query.Append("     , idcolaborador = " + _livros.IdColaborador);
                    query.Append("     , nome = '" + _livros.Nome + "'");
                    query.Append("     , tipocessao = '" + _livros.TipoCessao + "'");
                    if (_livros.TipoCessao == "P")
                        query.Append("   , datadevolucao = null");
                    else
                        query.Append("     , datadevolucao = To_date('" + _livros.DataDevolucao.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , conservacao = '" + _livros.Conservacao + "'");
                    query.Append("     , sinopse = :psinopse");
                    query.Append("     , ativo = '" + (_livros.Ativo ? "S" : "N") + "'");
                    query.Append("     , Fisico = '" + (_livros.Fisico ? "S" : "N") + "'");
                    query.Append("     , Ebook = '" + (_livros.EBook ? "S" : "N") + "'");
                    query.Append("     , AudioBook = '" + (_livros.AudioBook ? "S" : "N") + "'");
                    query.Append("     , LocalDeArmazenamento ='" + _livros.LocalArmazenamento + "'");
                    query.Append("     , NomeArquivo = '" + _livros.NomeArquivo + "'");

                    parametro = new OracleParameter();
                    parametro.ParameterName = ":psinopse";
                    parametro.Value = _livros.Sinopse;
                    parametro.OracleType = OracleType.VarChar;
                    parametros.Add(parametro);

                    if (_livros.Imagem == null)
                    {
                        query.Append(", Imagem = null ");
                    }
                    else
                    {
                        query.Append(", Imagem = :pfoto ");

                        parametro = new OracleParameter();
                        parametro.ParameterName = ":pfoto";
                        parametro.Value = _livros.Imagem;
                        parametro.OracleType = OracleType.Blob;
                        parametros.Add(parametro);
                    }

                    query.Append(" Where idlivros = " + _livros.Id);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString(), parametros.ToArray());

                if (retorno)
                {
                    query.Clear();

                    Classes.Ebook _ebook = ConsultaEbook(_livros.Id);

                    if (_livros.Arquivo != null)
                    {                        
                        if (!_ebook.Existe)
                        {
                            query.Append("Insert into niff_bib_eBook");
                            query.Append("   (idlivros, Arquivo )");
                            query.Append("  Values (" + _livros.Id);
                            query.Append("  , :pArquivo )");
                        }
                        else
                        {
                            query.Append("Update niff_bib_eBook");
                            query.Append("   set Arquivo = :pArquivo");
                            query.Append(" Where idlivros = " + _livros.Id);
                        }

                        parametros.Clear();
                        parametro = new OracleParameter();
                        parametro.ParameterName = ":pArquivo";
                        parametro.Value = _livros.Arquivo;
                        parametro.OracleType = OracleType.Blob;
                        parametros.Add(parametro);

                        retorno = sessao.ExecuteSqlTransaction(query.ToString(), parametros.ToArray());
                    }
                    else
                    {
                        if (!_ebook.Existe)
                        {
                            query.Append("Delete niff_bib_eBook");
                            query.Append(" Where idlivros = " + _livros.Id);

                            retorno = sessao.ExecuteSqlTransaction(query.ToString());
                        }
                    }
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

        public bool Exclui(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            try
            {
                query.Append("Delete niff_bib_eBook");
                query.Append(" Where idlivros = " + codigo);
                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    query.Clear();
                    query.Append("Delete niff_bib_livros");
                    query.Append(" Where idlivros = " + codigo);

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
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

        public int Proximo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Max(idlivros) + 1 next From niff_bib_livros");
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

        public Leitura ConsultaLeitura(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Leitura _livros = new Leitura();

            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, idlivros, idcolaborador, parounapagina ");
                query.Append("     , EfetuouDownLoad, DataDownload, UltimoAcesso, Nvl(TotalPaginas,0) TotalPaginas ");
                query.Append("  From niff_bib_Leitura e");
                query.Append(" Where idlivros = " + codigo);
                query.Append("   and IdColaborador = " + Publicas._idColaborador);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _livros.Existe = true;
                        _livros.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _livros.IdLivros = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());

                        _livros.TotalPagina = Convert.ToInt32(dataReader["TotalPaginas"].ToString());
                        _livros.Pagina = Convert.ToInt32(dataReader["parounapagina"].ToString());

                        try
                        {
                            _livros.UltimoAcesso = Convert.ToDateTime(dataReader["UltimoAcesso"].ToString());
                        }
                        catch { }
                        try
                        {
                            _livros.DataDownLoad = Convert.ToDateTime(dataReader["DataDownLoad"].ToString());
                        }
                        catch { }

                        _livros.EfetuouDownLoad = dataReader["EfetuouDownLoad"].ToString() == "S";
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
            return _livros;
        }

        public bool GravaLeitura(Leitura _livros)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                if (!_livros.Existe)
                {
                    query.Clear();
                    query.Append("Insert into niff_bib_Leitura");
                    query.Append("   (id, idlivros, idcolaborador, ParouNaPagina ");
                    query.Append("   , EfetuouDownLoad, DataDownload, UltimoAcesso )");
                    query.Append("  Values ((Select Nvl(Max(Id),0) + 1 next From niff_bib_Leitura )");
                    query.Append("   , " + _livros.IdLivros);
                    query.Append("   , " + _livros.IdColaborador);
                    query.Append("   , " + _livros.Pagina);
                    query.Append("   , '" + (_livros.EfetuouDownLoad ? "S" : "N") + "'");

                    if (_livros.DataDownLoad != DateTime.MinValue)
                        query.Append("   , To_Date('" + _livros.DataDownLoad + "','dd/mm/yyyy hh24:mi:ss')");
                    else
                        query.Append("   , null");

                    if (_livros.UltimoAcesso != DateTime.MinValue)
                        query.Append("   , To_Date('" + _livros.UltimoAcesso + "','dd/mm/yyyy hh24:mi:ss')");
                    else
                        query.Append("   , null");

                    query.Append("   )");
                }
                else
                {
                    query.Clear();
                    query.Append("Update niff_bib_Leitura");
                    query.Append("   set ParouNaPagina = " + _livros.Pagina);

                    query.Append("   , EfetuouDownLoad = '" + (_livros.EfetuouDownLoad ? "S" : "N") + "'");
                    query.Append("   , TotalPaginas = " + _livros.TotalPagina);

                    if (_livros.DataDownLoad != DateTime.MinValue)
                        query.Append("   , DataDownload = To_Date('" + _livros.DataDownLoad + "','dd/mm/yyyy hh24:mi:ss')");
                    else
                        query.Append("   , DataDownload = null");

                    if (_livros.UltimoAcesso != DateTime.MinValue)
                        query.Append("   , UltimoAcesso = To_Date('" + _livros.UltimoAcesso + "','dd/mm/yyyy hh24:mi:ss')");
                    else
                        query.Append("   , UltimoAcesso = null");
                    query.Append(" Where id = " + _livros.Id);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                                
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

    }
}
