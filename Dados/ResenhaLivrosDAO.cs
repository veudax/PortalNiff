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
    public class ResenhaLivrosDAO
    {
        IDataReader dataReader;

        public List<ResenhaLivros> Listar(bool resenhasLiberadas, int colaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ResenhaLivros> _lista = new List<ResenhaLivros>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select r.idresenha, r.idlivros, r.idcolaborador, r.data, r.resenha, r.ativo, r.DataLiberacao, l.Nome, c.Nome NomeColaborador, r.Pontuacao, l.Imagem, r.temResenha, r.TemPerguntas");
                query.Append("  From niff_bib_resenha r, niff_bib_livros l, niff_ads_colaboradores c ");

                query.Append(" Where r.idlivros = l.idlivros ");
                query.Append("   and c.IdColaborador = r.IdColaborador");

                if (!resenhasLiberadas)
                    query.Append("    and DataLiberacao is null And Ativo = 'N'");
                else
                {
                    query.Append("    and DataLiberacao is not null and trunc(DataLiberacao)+4 >= trunc(sysDate)");
                    query.Append("    and r.IdColaborador = " + colaborador);
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ResenhaLivros _livros = new ResenhaLivros();

                        _livros.Existe = true;

                        _livros.NomeLivro = dataReader["Nome"].ToString();
                        _livros.NomeColaborador = dataReader["NomeColaborador"].ToString();
                        _livros.IdLivros = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.Id = Convert.ToInt32(dataReader["idresenha"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livros.Resenha = dataReader["Resenha"].ToString();
                        _livros.Ativo = dataReader["Ativo"].ToString() == "S";
                        _livros.TemResenha = dataReader["Temresenha"].ToString() == "S";
                        _livros.TemPerguntas = dataReader["Temperguntas"].ToString() == "S";

                        _livros.Pontuacao = Convert.ToInt32(dataReader["pontuacao"].ToString());
                        try
                        {
                            _livros.Data = Convert.ToDateTime(dataReader["data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataLiberacao = Convert.ToDateTime(dataReader["DataLiberacao"].ToString());
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

        public List<ResenhaLivros> ListarComResenha(bool mostraSinopse)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ResenhaLivros> _lista = new List<ResenhaLivros>();
            Publicas.mensagemDeErro = string.Empty;

            List<Livros> _livros = new LivrosDAO().Listar(true, true);

            try
            {
                query.Append("Select r.idresenha, r.idlivros, r.idcolaborador, r.data, r.resenha, r.ativo, r.DataLiberacao, l.Nome, c.Nome NomeColaborador, r.Pontuacao, l.Imagem, l.sinopse");
                query.Append("     , l.LocalDeArmazenamento, l.QDownLoad, l.ebook, l.NomeArquivo, e.Arquivo, l.Fisico, r.temResenha, r.TemPerguntas");
                query.Append("  From niff_bib_resenha r, niff_bib_livros l, niff_ads_colaboradores c, niff_bib_eBook e ");

                query.Append(" Where r.idlivros = l.idlivros ");
                query.Append("   and c.IdColaborador = r.IdColaborador");

                query.Append("   and DataLiberacao is not null");
                query.Append("   and l.Ativo = 'S'");
                query.Append("   and l.Idlivros = e.Idlivros(+)");


                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ResenhaLivros _livro = new ResenhaLivros();

                        _livro.Existe = true;

                        _livro.NomeLivro = dataReader["Nome"].ToString();
                        _livro.NomeColaborador = dataReader["NomeColaborador"].ToString();
                        _livro.IdLivros = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livro.Id = Convert.ToInt32(dataReader["idresenha"].ToString());
                        _livro.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livro.Resenha = dataReader["Resenha"].ToString();
                        _livro.Sinopse = dataReader["Sinopse"].ToString();
                        _livro.Ativo = dataReader["Ativo"].ToString() == "S";
                        _livro.EBook = dataReader["Ebook"].ToString() == "S";
                        _livro.Fisico = dataReader["Fisico"].ToString() == "S";
                        _livro.NomeArquivo = dataReader["NomeArquivo"].ToString();
                        _livro.TemResenha = dataReader["Temresenha"].ToString() == "S";
                        _livro.TemPerguntas = dataReader["Temperguntas"].ToString() == "S";

                        _livro.LocalArmazenamento = dataReader["LocalDeArmazenamento"].ToString();
                        _livro.QuantidadeDownload = Convert.ToInt32(dataReader["QDownLoad"].ToString());

                        _livro.Pontuacao = Convert.ToInt32(dataReader["pontuacao"].ToString());
                        try
                        {
                            _livro.Data = Convert.ToDateTime(dataReader["data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livro.DataLiberacao = Convert.ToDateTime(dataReader["DataLiberacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livro.Imagem = (byte[])(dataReader["Imagem"]);
                        }
                        catch { }

                        Classes.EmprestimoLivros _emprestimo = new EmprestimoLivrosDAO().Consulta(_livro.IdLivros, Publicas._idColaborador);
                        _livro.Lendo = (_emprestimo.Existe && !_emprestimo.Devolvido);

                        _lista.Add(_livro);
                    }
                }

                if (mostraSinopse)
                {
                    foreach (var item in _livros)
                    {
                        if (_lista.Where(w => w.IdLivros == item.Id).Count() == 0)
                        {
                            ResenhaLivros _livro = new ResenhaLivros();

                            Classes.EmprestimoLivros _emprestimo = new EmprestimoLivrosDAO().Consulta(item.Id, Publicas._idColaborador);
                            _livro.Lendo = (_emprestimo.Existe && !_emprestimo.Devolvido);

                            _livro.Existe = true;

                            _livro.NomeLivro = item.Nome;
                            _livro.IdLivros = item.Id;
                            _livro.Sinopse = item.Sinopse;

                            _livro.EBook = item.EBook;
                            _livro.NomeArquivo = item.NomeArquivo;

                            _livro.LocalArmazenamento = item.LocalArmazenamento;
                            _livro.QuantidadeDownload = item.QuantidadeDownload;
                            _livro.Fisico = item.Fisico;
                            try
                            {
                                _livro.Imagem = item.Imagem;
                            }
                            catch { }
                            
                            _lista.Add(_livro);
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
            return _lista;
        }

        public List<ResenhaLivros> ListarLivroSemResenha(int colaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ResenhaLivros> _lista = new List<ResenhaLivros>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select l.idlivros, e.idcolaborador, l.Nome, c.Nome NomeColaborador, e.devolvidoEm data");
                query.Append("  From niff_bib_emprestimo e, niff_bib_resenha r, niff_bib_livros l, Niff_Ads_Colaboradores c");

                query.Append(" Where e.idlivros = l.idlivros ");
                query.Append("   and e.idlivros = r.idlivros(+) ");
                query.Append("   and e.IdColaborador = c.IdColaborador");
                query.Append("   and e.devolvido = 'S'");
                query.Append("   and r.idLivros is null"); // apenas os sem resenha cadastrado
                query.Append("   and trunc(devolvidoEm) + 4 >= trunc(sysDate)");

                if (colaborador != 0)
                    query.Append("    and e.IdColaborador = " + colaborador);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ResenhaLivros _livros = new ResenhaLivros();

                        _livros.Existe = true;

                        _livros.NomeLivro = dataReader["Nome"].ToString();
                        _livros.NomeColaborador = dataReader["NomeColaborador"].ToString();
                        _livros.IdLivros = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());

                        try
                        {
                            _livros.Data = Convert.ToDateTime(dataReader["data"].ToString());
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

        public List<PerguntasLivros> ListarPerguntas(int IdResenha, int idLivro)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PerguntasLivros> _lista = new List<PerguntasLivros>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select p.idpergunta, p.idresenha, p.pergunta, p.resposta");
                query.Append("  From Niff_Bib_Perguntas p");

                if (idLivro == 0)
                    query.Append(" Where idResenha = " + IdResenha);
                else
                {
                    query.Append("     , Niff_Bib_Resenha r");
                    query.Append(" Where p.idResenha = r.IdResenha");
                    query.Append("   and r.idlivros = " + idLivro);
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        PerguntasLivros _livros = new PerguntasLivros();

                        _livros.Existe = true;

                        _livros.Id = Convert.ToInt32(dataReader["idpergunta"].ToString());
                        _livros.IdResenha = Convert.ToInt32(dataReader["idresenha"].ToString());
                        _livros.Pergunta = dataReader["Pergunta"].ToString();
                        _livros.Resposta = dataReader["Resposta"].ToString();

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

        public List<RespostasLivros> ListarRespostas(int IdColaborador, int idLivro)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RespostasLivros> _lista = new List<RespostasLivros>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select r.id, r.idpergunta, r.idcolaborador, r.data, r.resposta, r.aprovada, r.pontuacao, r.dataaprovacao, r.Certa");
                query.Append("     , p.Idresenha, p.Pergunta, p.Resposta RespostaOriginal, r.IdUsuario");
                query.Append("  From Niff_Bib_Respostas r, Niff_Bib_Perguntas p, Niff_Bib_Resenha s");
                query.Append(" Where p.idResenha = s.IdResenha");
                query.Append("   and p.IdPergunta = r.idPergunta");
                query.Append("   and s.idlivros = " + idLivro);
                query.Append("   and r.idColaborador = " + IdColaborador);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        RespostasLivros _livros = new RespostasLivros();

                        _livros.Existe = true;

                        _livros.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _livros.IdPergunta = Convert.ToInt32(dataReader["idpergunta"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livros.Pergunta = dataReader["Pergunta"].ToString();
                        _livros.Resposta = dataReader["Resposta"].ToString();
                        _livros.RespostaOriginal = dataReader["RespostaOriginal"].ToString();
                        _livros.Aprovada = dataReader["aprovada"].ToString() == "S";
                        _livros.Certa = dataReader["Certa"].ToString() == "S";

                        try
                        {
                            _livros.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataAprovacao = Convert.ToDateTime(dataReader["dataaprovacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.Data = Convert.ToDateTime(dataReader["data"].ToString());
                        }
                        catch { }

                        _lista.Add(_livros);
                    }

                    _lista.ForEach(u => u.TemPerguntasSemResposta = _lista.Where(w => w.Resposta.Trim() == "").Count() != 0);
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

        public List<RespostasLivros> ListarLivrosComRespostas(bool rh)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RespostasLivros> _lista = new List<RespostasLivros>();
            Publicas.mensagemDeErro = string.Empty;

            int qdtDias = (DateTime.Now.DayOfWeek == DayOfWeek.Monday ? 3 : 1);

            try
            {
                if (rh && !Publicas._usuario.Administrador)
                    query.Append("Select Max(trunc(r.data)) Data, c.Nome, l.Nome NomeLivro, r.aprovada ");
                else
                    query.Append("Select Max(trunc(r.dataaprovacao)) Data, c.Nome, l.Nome NomeLivro,  r.aprovada ");

                query.Append("  From Niff_Bib_Respostas r, Niff_Bib_Perguntas p, Niff_Bib_Resenha s");
                query.Append("     , Niff_Ads_Colaboradores c, Niff_Bib_Livros l");
                query.Append(" Where p.idResenha = s.IdResenha");
                query.Append("   and p.IdPergunta = r.idPergunta");
                query.Append("   And l.Idlivros = s.Idlivros");
                query.Append("   And c.Idcolaborador = r.Idcolaborador");

                if (rh && !Publicas._usuario.Administrador)
                {
                    query.Append("  And r.aprovada = 'N'");
                    query.Append("  And trunc(r.Data) + " + qdtDias + " >= Sysdate");
                }
                else
                {
                    query.Append("  And r.aprovada = 'S'");
                    query.Append("  And trunc(r.dataaprovacao) + " + qdtDias + " >= Sysdate");
                    query.Append("  And c.Idcolaborador = " + Publicas._idColaborador);
                }

                query.Append("  group By c.Nome, l.Nome, r.aprovada   ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        RespostasLivros _livros = new RespostasLivros();

                        _livros.Existe = true;

                        _livros.NomeColaborador = dataReader["Nome"].ToString();
                        _livros.NomeLivro = dataReader["NomeLivro"].ToString();
                        _livros.Aprovada = dataReader["aprovada"].ToString() == "S";

                        try
                        {
                            _livros.Data = Convert.ToDateTime(dataReader["data"].ToString());
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

        public List<ResenhaLivros> ListarPontuacao(int IdColaborador, bool mostrarLivros)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ResenhaLivros> _lista = new List<ResenhaLivros>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select RowNum Clas, a.*");
                query.Append("  From (");

                query.Append("Select PontoResposta, PontoResenha, PontoPerguntas, PontoEmprestimo, Pontos, NomeColaborador, Data");
                if (mostrarLivros)
                    query.Append("     , Nome");
                query.Append("  From (");
                query.Append("Select Sum(PontoResposta) PontoResposta, Sum(PontoResenha) PontoResenha, Sum(PontoPerguntas) PontoPerguntas, Sum(PontoEmprestimo) PontoEmprestimo");
                query.Append("     , Sum(PontoResposta) +Sum(PontoResenha) + Sum(PontoPerguntas) + Sum(PontoEmprestimo) Pontos");
                query.Append("     , Max(Data) Data, NomeColaborador");
                if (mostrarLivros)
                    query.Append("     , Nome");
                query.Append("  From (select 0 PontoResposta");
                query.Append("             , Decode(s.temresenha, 'S', 5, 0) PontoResenha");
                query.Append("             , Decode(s.temperguntas, 'S', 5, 0) PontoPerguntas");
                query.Append("             , 0 PontoEmprestimo");
                query.Append("             , c.Nome NomeColaborador");
                query.Append("             , l.Nome, s.data");
                query.Append("         From Niff_Bib_Resenha s, niff_bib_livros l, niff_Ads_Colaboradores c");
                query.Append("        Where s.idlivros = l.Idlivros");
                query.Append("          And s.Idcolaborador = c.Idcolaborador");
                query.Append("          And s.Ativo = 'S'");

                if (IdColaborador != 0)
                    query.Append("   and c.idColaborador = " + IdColaborador);
                query.Append(" Group By c.Nome, l.Nome, s.temresenha, s.temperguntas, s.data");

                query.Append("        Union All");

                query.Append("       select 0 PontoResposta");
                query.Append("            , 0 PontoResenha");
                query.Append("            , 0 PontoPerguntas");
                query.Append("            , Nvl(Sum(e.Pontuacao),1) PontoEmprestimo");
                query.Append("            , c.Nome NomeColaborador");
                query.Append("            , l.Nome, e.dataemprestimo Data");
                query.Append("         From Niff_Bib_Emprestimo e, Niff_Ads_Colaboradores c,  Niff_Bib_Livros l");
                query.Append("        Where e.devolvido = 'S'");
                query.Append("          And e.Idcolaborador = c.Idcolaborador");
                query.Append("          And e.Idlivros = l.Idlivros");

                if (IdColaborador != 0)
                    query.Append("          And c.idColaborador = " + IdColaborador);

                query.Append("        Group By c.nome, l.Nome, e.dataemprestimo");

                query.Append("        Union All");

                query.Append("       select Sum(r.pontuacao) PontoResposta");
                query.Append("            , 0 PontoResenha");
                query.Append("            , 0 PontoPerguntas");
                query.Append("            , 0 PontoEmprestimo");
                query.Append("            , c.Nome NomeColaborador");
                query.Append("            , l.Nome, r.data ");
                query.Append("         From Niff_Bib_Resenha s, Niff_Ads_Colaboradores c,  Niff_Bib_Livros l ");
                query.Append("            , Niff_Bib_Respostas r, Niff_Bib_Perguntas p");
                query.Append("        Where r.Idcolaborador = c.Idcolaborador");
                query.Append("          And s.Idlivros = l.Idlivros");
                query.Append("          And s.idResenha = p.IdResenha");
                query.Append("          And r.Idpergunta = p.Idpergunta");
                query.Append("          And s.idlivros = l.Idlivros");
                query.Append("          And r.Aprovada = 'S'");

                if (IdColaborador != 0)
                    query.Append("          And c.idColaborador = " + IdColaborador);

                query.Append("        Group By c.nome, l.Nome, r.data ");

                query.Append(") Group By NomeColaborador");
                if (mostrarLivros)
                    query.Append("     , Nome");

                query.Append(" ) Order By Pontos Desc, data, NomeColaborador");
                query.Append("  ) a");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ResenhaLivros _livros = new ResenhaLivros();

                        _livros.Existe = true;

                        _livros.NomeColaborador = dataReader["NomeColaborador"].ToString();
                        _livros.Pontuacao = Convert.ToInt32(dataReader["Pontos"].ToString());
                        _livros.Classificacao = Convert.ToInt32(dataReader["Clas"].ToString());
                        _livros.PontoEmprestimo = Convert.ToInt32(dataReader["PontoEmprestimo"].ToString());
                        _livros.PontoPergunta = Convert.ToInt32(dataReader["PontoPerguntas"].ToString());
                        _livros.PontoResenha = Convert.ToInt32(dataReader["PontoResenha"].ToString());
                        _livros.PontoResposta = Convert.ToInt32(dataReader["PontoResposta"].ToString());

                        if (mostrarLivros)
                            _livros.NomeLivro = dataReader["Nome"].ToString();

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

        public ResenhaLivros Consulta(int codigo, int idColaborado, bool diferente)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            ResenhaLivros _livros = new ResenhaLivros();

            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select r.idresenha, r.idlivros, r.idcolaborador, r.data, r.resenha, r.ativo, r.DataLiberacao, l.Nome, r.Temresenha, r.Temperguntas");
                query.Append("  From niff_bib_resenha r, niff_bib_livros l");
                query.Append(" Where r.idlivros = " + codigo);
                query.Append("   and r.idlivros = l.idlivros ");
                if (diferente)
                query.Append("   and r.idcolaborador <> " + idColaborado);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _livros.Existe = true;

                        _livros.NomeLivro = dataReader["Nome"].ToString();
                        _livros.IdLivros = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.Id = Convert.ToInt32(dataReader["idresenha"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livros.Resenha = dataReader["Resenha"].ToString();
                        _livros.Ativo = dataReader["Ativo"].ToString() == "S";
                        _livros.TemResenha = dataReader["Temresenha"].ToString() == "S";
                        _livros.TemPerguntas = dataReader["Temperguntas"].ToString() == "S";
                        _livros.TemResenha = dataReader["Temresenha"].ToString() == "S";
                        _livros.TemPerguntas = dataReader["Temperguntas"].ToString() == "S";

                        try
                        {
                            _livros.Data = Convert.ToDateTime(dataReader["data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataLiberacao = Convert.ToDateTime(dataReader["DataLiberacao"].ToString());
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

        public ResenhaLivros Consulta(int livro, int colaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            ResenhaLivros _livros = new ResenhaLivros();

            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select r.idresenha, r.idlivros, r.idcolaborador, r.data, r.resenha, r.ativo, r.DataLiberacao, l.Nome, r.Temresenha, r.Temperguntas");
                query.Append("  From niff_bib_resenha r, niff_bib_livros l");
                query.Append(" Where r.idlivros = " + livro);
                query.Append("   and r.idcolaborador = " + colaborador);
                query.Append("   and r.idlivros = l.idlivros ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _livros.Existe = true;

                        _livros.NomeLivro = dataReader["Nome"].ToString();
                        _livros.IdLivros = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.Id = Convert.ToInt32(dataReader["idresenha"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livros.Resenha = dataReader["Resenha"].ToString();
                        _livros.Ativo = dataReader["Ativo"].ToString() == "S";
                        _livros.TemResenha = dataReader["Temresenha"].ToString() == "S";
                        _livros.TemPerguntas = dataReader["Temperguntas"].ToString() == "S";

                        try
                        {
                            _livros.Data = Convert.ToDateTime(dataReader["data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataLiberacao = Convert.ToDateTime(dataReader["DataLiberacao"].ToString());
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

        public bool Grava(ResenhaLivros _livros, List<PerguntasLivros> _perguntas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            int id = 1;
            int pontuacao = 0;
            try
            {
                if (!string.IsNullOrEmpty(_livros.Resenha.Trim()))
                    pontuacao = 5;

                if (_perguntas.Count >= 5)
                    pontuacao = pontuacao + 5;
                else
                    pontuacao = pontuacao + _perguntas.Count * 1;

                if (!_livros.Existe)
                {
                    id = Proximo();
                    query.Clear();
                    query.Append("Insert into niff_bib_resenha");
                    query.Append("   (idresenha, idlivros, idcolaborador, data, resenha, ativo, Temresenha, Temperguntas ");

                    if (_livros.Ativo)
                        query.Append(" , Pontuacao");
                    query.Append(" )  Values (" + id);
                    query.Append("   , " + _livros.IdLivros);
                    query.Append("   , " + _livros.IdColaborador);
                    query.Append("   , To_date('" + _livros.Data.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("   , :pResenha");
                    query.Append("   , '" + (_livros.Ativo ? "S" : "N") + "'");
                    query.Append("   , '" + (_livros.TemResenha ? "S" : "N") + "'");
                    query.Append("   , '" + (_livros.TemPerguntas ? "S" : "N") + "'");

                    if (_livros.Ativo)
                        query.Append("   , " + pontuacao);

                    query.Append("   )");
                }
                else
                {
                    id = _livros.Id;
                    query.Clear();
                    query.Append("Update niff_bib_resenha");
                    query.Append("   set resenha = :pResenha");

                    query.Append("     , TemResenha = '" + (_livros.TemResenha ? "S" : "N") + "'");
                    query.Append("     , TemPerguntas = '" + (_livros.TemPerguntas ? "S" : "N") + "'");

                    if (_livros.Ativo)
                    {
                        query.Append("     , DataLiberacao = SysDate");
                        query.Append("     , Ativo = 'S'");
                        query.Append("     , IdUsuario = " + _livros.IdUsuario);
                        query.Append("     , Pontuacao = " + pontuacao);
                    }
                    query.Append(" Where idresenha = " + _livros.Id);
                }

                OracleParameter _parametro = new OracleParameter();
                _parametro.ParameterName = "pResenha";
                _parametro.Value = _livros.Resenha;

                List<OracleParameter> _lista = new List<OracleParameter>();
                _lista.Add(_parametro);

                retorno=  sessao.ExecuteSqlTransaction(query.ToString(), _lista.ToArray());

                _perguntas.ForEach(u => u.IdResenha = id);

                if (retorno && _perguntas.Count() > 0)
                    retorno = GravaPeguntas(_perguntas);

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

        public bool GravaPeguntas(List<PerguntasLivros> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            bool retorno = false;

            try
            {
                foreach (var _livros in _lista)
                {

                    if (!_livros.Existe)
                    {
                        query.Clear();
                        query.Append("Insert into Niff_Bib_Perguntas");
                        query.Append("   (idpergunta, idresenha, pergunta, resposta) ");
                        query.Append("   Values (" + ProximaPergunta());
                        query.Append("   , " + _livros.IdResenha);
                        query.Append("   , :pPergunta");
                        query.Append("   , :pResposta");
                        query.Append("   )");
                    }
                    else
                    {
                        query.Clear();
                        query.Append("Update Niff_Bib_Perguntas");
                        query.Append("   set pergunta = :pPergunta");
                        query.Append("   , resposta = :pResposta");
                        query.Append(" Where idpergunta = " + _livros.Id);
                    }

                    OracleParameter _perguntas = new OracleParameter();
                    _perguntas.ParameterName = "pPergunta";
                    _perguntas.Value = _livros.Pergunta;

                    OracleParameter _resposta = new OracleParameter();
                    _resposta.ParameterName = "pResposta";
                    _resposta.Value = _livros.Resposta;

                    List<OracleParameter> _listaParametros = new List<OracleParameter>();
                    _listaParametros.Add(_perguntas);
                    _listaParametros.Add(_resposta);

                    retorno = sessao.ExecuteSqlTransaction(query.ToString(), _listaParametros.ToArray());

                    if (!retorno)
                        return false;
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

        public bool GravaRespostas(List<RespostasLivros> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            bool retorno = false;

            try
            {
                foreach (var _livros in _lista)
                {
                        if (!_livros.Existe)
                        {
                            query.Clear();
                            query.Append("Insert into niff_bib_respostas");
                            query.Append("   (id, idpergunta, idcolaborador, data, resposta) ");
                            query.Append("   Values (" + ProximaResposta());
                            query.Append("   , " + _livros.IdPergunta);
                            query.Append("   , " + _livros.IdColaborador);
                            query.Append("   , sysdate");
                            query.Append("   , :pResposta");
                            query.Append("   )");
                        }
                        else
                        {
                            query.Clear();
                            query.Append("Update niff_bib_respostas");
                            query.Append("   set resposta = :pResposta");

                            if (_livros.Aprovada && _livros.DataAprovacao == DateTime.MinValue)
                            {
                                query.Append("     , Aprovada = '" + (_livros.Aprovada ? "S" : "N") + "'");
                                query.Append("     , DataAprovacao = sysdate");
                                query.Append("     , IdUsuario = " + _livros.IdUsuario);
                            }
                            query.Append("     , Certa = '" + (_livros.Certa ? "S" : "N") + "'");
                            query.Append("     , Pontuacao = " + (_livros.Certa ? 1 : 0));

                            query.Append(" Where id = " + _livros.Id);
                        }

                        OracleParameter _resposta = new OracleParameter();
                        _resposta.ParameterName = "pResposta";
                        _resposta.Value = (_livros.Resposta == null ? " " : _livros.Resposta);

                        List<OracleParameter> _listaParametros = new List<OracleParameter>();
                        _listaParametros.Add(_resposta);

                        retorno = sessao.ExecuteSqlTransaction(query.ToString(), _listaParametros.ToArray());

                        if (!retorno)
                            return false;
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

            try
            {
                if (!ExcluiPerguntas(codigo))
                    return false;
                else
                {

                    query.Append("Delete niff_bib_resenha");
                    query.Append(" Where idresenha = " + codigo);

                    return sessao.ExecuteSqlTransaction(query.ToString());
                }
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

        public bool ExcluiPerguntas(int Resenha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Bib_Perguntas");
                query.Append(" Where idresenha = " + Resenha);

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

                query.Append("Select Max(idresenha) + 1 next From niff_bib_resenha");
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

        public int ProximaPergunta()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Max(idpergunta) + 1 next From Niff_Bib_Perguntas");
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

        public int ProximaResposta()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Max(id) + 1 next From niff_bib_respostas");
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
