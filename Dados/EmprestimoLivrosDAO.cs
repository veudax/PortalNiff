using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class EmprestimoLivrosDAO
    {
        IDataReader dataReader;

        public List<EmprestimoLivros> ListarDownload(int colaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<EmprestimoLivros> _lista = new List<EmprestimoLivros>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select e.idemprestimo, e.idlivros, e.idcolaborador, e.dataemprestimo, e.dataprorrogacao");
                query.Append("     , e.datadevolucao, e.qtdediasemprestimo, e.qtdediasprorrogacao, e.conservacao");
                query.Append("     , e.Devolvido, e.DevolvidoEm, Nvl(e.Pontuacao,0) Pontuacao");
                query.Append("     , l.nome NomeLivro, c.Nome NomeColaborador, e.Ebook, t.parounapagina, t.ultimoacesso, t.datadownload, t.totalpaginas");
                query.Append("  From niff_bib_emprestimo e, niff_bib_livros l, niff_ads_colaboradores c, niff_bib_Leitura t");
                query.Append(" Where Devolvido = 'N'");
                query.Append("   and e.IdLivros = l.IdLivros");
                query.Append("   and e.IdColaborador = c.idcolaborador");
                query.Append("   And t.idlivros = l.Idlivros");
                query.Append("   And t.idcolaborador = c.idcolaborador");
                query.Append("   And e.ebook = 'S'");

                if (colaborador != 0)
                    query.Append("   And e.IdColaborador = " + colaborador);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        EmprestimoLivros _livros = new EmprestimoLivros();

                        _livros.Existe = true;
                        _livros.Id = Convert.ToInt32(dataReader["idemprestimo"].ToString());
                        _livros.IdLivro = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livros.Devolvido = dataReader["Devolvido"].ToString() == "S";
                        _livros.Ebook = dataReader["Ebook"].ToString() == "S";
                        _livros.NomeColaborador = dataReader["NomeColaborador"].ToString();
                        _livros.NomeLivro = dataReader["NomeLivro"].ToString();
                        _livros.Pontuacao = Convert.ToInt32(dataReader["Pontuacao"].ToString());

                        try
                        {
                            _livros.TotalPagina = Convert.ToInt32(dataReader["TotalPaginas"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.Pagina = Convert.ToInt32(dataReader["parounapagina"].ToString());
                        }
                        catch { }

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

                        
                        try
                        {
                            _livros.QuantidadeDiasEmprestimo = Convert.ToInt32(dataReader["qtdediasemprestimo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.QuantidadeDiasRenovacao = Convert.ToInt32(dataReader["qtdediasprorrogacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.Data = Convert.ToDateTime(dataReader["dataemprestimo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataDevolucao = Convert.ToDateTime(dataReader["datadevolucao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataRenovacao = Convert.ToDateTime(dataReader["dataprorrogacao"].ToString());
                        }
                        catch { }

                        if (_livros.DataRenovacao != DateTime.MinValue)
                            _livros.DataAcompanhamento = _livros.DataRenovacao;
                        else
                            _livros.DataAcompanhamento = _livros.DataDevolucao;

                        try
                        {
                            _livros.DevolvidoEm = Convert.ToDateTime(dataReader["DevolvidoEm"].ToString());
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
        

        public List<EmprestimoLivros> Listar(bool comDevolucaoEm5Dias, int colaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<EmprestimoLivros> _lista = new List<EmprestimoLivros>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select e.idemprestimo, e.idlivros, e.idcolaborador, e.dataemprestimo, e.dataprorrogacao");
                query.Append("     , e.datadevolucao, e.qtdediasemprestimo, e.qtdediasprorrogacao, e.conservacao");
                query.Append("     , e.Devolvido, e.DevolvidoEm, Nvl(e.Pontuacao,0) Pontuacao");
                query.Append("     , l.nome NomeLivro, c.Nome NomeColaborador, e.Ebook");
                query.Append("  From niff_bib_emprestimo e, niff_bib_livros l, niff_ads_colaboradores c");
                query.Append(" Where Devolvido = 'N'");
                query.Append("   and e.IdLivros = l.IdLivros");
                query.Append("   and e.IdColaborador = c.idcolaborador");

                if (comDevolucaoEm5Dias)
                {
                    query.Append("   and (((e.datadevolucao Between trunc(Sysdate) And trunc(Sysdate) + 15");
                    query.Append("    Or e.dataprorrogacao Between trunc(Sysdate) And trunc(Sysdate) + 15)");
                    query.Append("   And e.devolvidoem Is Null)");
                    query.Append("   Or (e.Datadevolucao < Trunc(Sysdate) And e.Dataprorrogacao < Trunc(Sysdate) And e.Devolvidoem Is Null))");

                    if (colaborador != 0)
                        query.Append("   And e.IdColaborador = " + colaborador); 
                }
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        EmprestimoLivros _livros = new EmprestimoLivros();

                        _livros.Existe = true;
                        _livros.Id = Convert.ToInt32(dataReader["idemprestimo"].ToString());
                        _livros.IdLivro = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livros.Devolvido = dataReader["Devolvido"].ToString() == "S";
                        _livros.Ebook = dataReader["Ebook"].ToString() == "S";
                        _livros.NomeColaborador = dataReader["NomeColaborador"].ToString();
                        _livros.NomeLivro = dataReader["NomeLivro"].ToString();
                        _livros.Pontuacao = Convert.ToInt32(dataReader["Pontuacao"].ToString());

                        try
                        {
                            _livros.QuantidadeDiasEmprestimo = Convert.ToInt32(dataReader["qtdediasemprestimo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.QuantidadeDiasRenovacao = Convert.ToInt32(dataReader["qtdediasprorrogacao"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _livros.Data = Convert.ToDateTime(dataReader["dataemprestimo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataDevolucao = Convert.ToDateTime(dataReader["datadevolucao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataRenovacao = Convert.ToDateTime(dataReader["dataprorrogacao"].ToString());
                        }
                        catch { }

                        if (_livros.DataRenovacao != DateTime.MinValue)
                            _livros.DataAcompanhamento = _livros.DataRenovacao;
                        else
                            _livros.DataAcompanhamento = _livros.DataDevolucao;

                        try
                        {
                            _livros.DevolvidoEm = Convert.ToDateTime(dataReader["DevolvidoEm"].ToString());
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

        public EmprestimoLivros Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            EmprestimoLivros _livros = new EmprestimoLivros();

            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select e.idemprestimo, e.idlivros, e.idcolaborador, e.dataemprestimo, e.dataprorrogacao");
                query.Append("     , e.datadevolucao, e.qtdediasemprestimo, e.qtdediasprorrogacao, e.conservacao");
                query.Append("     , e.Devolvido, e.DevolvidoEm, c.Nome, Nvl(e.Pontuacao,0) Pontuacao, e.Ebook");
                query.Append("  From niff_bib_emprestimo e, niff_ads_colaboradores c ");
                query.Append(" Where idlivros = " + codigo);
                query.Append("   and Devolvido = 'N'");
                query.Append("   and c.IdColaborador = e.idcolaborador");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _livros.Existe = true;
                        _livros.Id = Convert.ToInt32(dataReader["idemprestimo"].ToString());
                        _livros.IdLivro = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livros.Devolvido = dataReader["Devolvido"].ToString() == "S";
                        _livros.Ebook = dataReader["Ebook"].ToString() == "S";
                        _livros.NomeColaborador = dataReader["Nome"].ToString();
                        _livros.Pontuacao = Convert.ToInt32(dataReader["Pontuacao"].ToString());

                        try
                        {
                            _livros.QuantidadeDiasEmprestimo = Convert.ToInt32(dataReader["qtdediasemprestimo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.QuantidadeDiasRenovacao = Convert.ToInt32(dataReader["qtdediasprorrogacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.Data = Convert.ToDateTime(dataReader["dataemprestimo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataDevolucao = Convert.ToDateTime(dataReader["datadevolucao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataRenovacao = Convert.ToDateTime(dataReader["dataprorrogacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DevolvidoEm = Convert.ToDateTime(dataReader["DevolvidoEm"].ToString());
                        }
                        catch { }

                        if (_livros.DataRenovacao != DateTime.MinValue)
                            _livros.DataAcompanhamento = _livros.DataRenovacao;
                        else
                            _livros.DataAcompanhamento = _livros.DataDevolucao;
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

        public EmprestimoLivros Consulta(int codigo, int Colaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            EmprestimoLivros _livros = new EmprestimoLivros();

            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select idemprestimo, idlivros, idcolaborador,  dataemprestimo, dataprorrogacao");
                query.Append("     , datadevolucao, qtdediasemprestimo, qtdediasprorrogacao, conservacao");
                query.Append("     , Devolvido, DevolvidoEm, Nvl(Pontuacao,0) Pontuacao, Ebook");
                query.Append("  From niff_bib_emprestimo ");
                query.Append(" Where idlivros = " + codigo);
                query.Append("   And idcolaborador = " + Colaborador);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _livros.Existe = true;
                        _livros.Id = Convert.ToInt32(dataReader["idemprestimo"].ToString());
                        _livros.IdLivro = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livros.Devolvido = dataReader["Devolvido"].ToString() == "S";
                        _livros.Ebook = dataReader["Ebook"].ToString() == "S";
                        _livros.Pontuacao = Convert.ToInt32(dataReader["Pontuacao"].ToString());

                        try
                        {
                            _livros.QuantidadeDiasEmprestimo = Convert.ToInt32(dataReader["qtdediasemprestimo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.QuantidadeDiasRenovacao = Convert.ToInt32(dataReader["qtdediasprorrogacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.Data = Convert.ToDateTime(dataReader["dataemprestimo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataDevolucao = Convert.ToDateTime(dataReader["datadevolucao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataRenovacao = Convert.ToDateTime(dataReader["dataprorrogacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DevolvidoEm = Convert.ToDateTime(dataReader["DevolvidoEm"].ToString());
                        }
                        catch { }

                        if (_livros.DataRenovacao != DateTime.MinValue)
                            _livros.DataAcompanhamento = _livros.DataRenovacao;
                        else
                            _livros.DataAcompanhamento = _livros.DataDevolucao;
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

        public bool Grava(EmprestimoLivros _livros)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!_livros.Existe)
                {
                    query.Clear();
                    query.Append("Insert into niff_bib_emprestimo");
                    query.Append("   (idemprestimo, idlivros, idcolaborador,  dataemprestimo,  datadevolucao, qtdediasemprestimo, ebook ) ");
                    query.Append("   Values ( (Select  Nvl(Max(Idemprestimo),0) + 1 next From niff_bib_emprestimo )");
                    query.Append("   , " + _livros.IdLivro);
                    query.Append("   , " + _livros.IdColaborador);
                    query.Append("   , To_date('" + _livros.Data.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("   , To_date('" + _livros.DataDevolucao.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("   , '" + _livros.QuantidadeDiasEmprestimo + "'");
                    query.Append("   , '" + (_livros.Ebook ? "S" : "N") + "'");
                    query.Append("   )");
                }
                else
                {
                    query.Clear();
                    query.Append("Update niff_bib_emprestimo");
                    query.Append("   set datadevolucao = To_date('" + _livros.DataDevolucao.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , dataprorrogacao = To_date('" + _livros.DataRenovacao.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , qtdediasprorrogacao = " + _livros.QuantidadeDiasRenovacao);
                    query.Append(" Where idemprestimo = " + _livros.Id);
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

        public bool Devolucao(EmprestimoLivros _livros)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Update niff_bib_emprestimo");
                query.Append("   set DevolvidoEm = To_date('" + _livros.DevolvidoEm.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("     , Devolvido = 'S'");
                query.Append("     , Conservacao = '" + _livros.Conservacao + "'");
                query.Append("     , Pontuacao = 1");
                query.Append(" Where idemprestimo = " + _livros.Id);
                
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
                query.Append("Delete niff_bib_emprestimo");
                query.Append(" Where idemprestimo = " + codigo);

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

                query.Append("Select Max(idemprestimo) + 1 next From niff_bib_emprestimo");
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
