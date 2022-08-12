using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ReservaLivrosDAO //niff_bib_reserva
    {
        IDataReader dataReader;

        public List<ReservaLivros> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ReservaLivros> _lista = new List<ReservaLivros>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select e.idreserva, e.idlivros, e.idcolaborador, e.data, e.dataemprestimo");
                query.Append("     , l.nome NomeLivro, c.Nome NomeColaborador ");
                query.Append("  From niff_bib_reserva e, niff_bib_livros l, niff_ads_colaboradores c");
                query.Append(" Where dataemprestimo is null");
                query.Append("   and e.IdLivros = l.IdLivros");
                query.Append("   and e.IdColaborador = c.idcolaborador");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ReservaLivros _livros = new ReservaLivros();

                        _livros.Existe = true;
                        _livros.Id = Convert.ToInt32(dataReader["idreserva"].ToString());
                        _livros.IdLivro = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livros.NomeColaborador = dataReader["NomeColaborador"].ToString();
                        _livros.NomeLivro = dataReader["NomeLivro"].ToString();

                        try
                        {
                            _livros.DataSolicitado = Convert.ToDateTime(dataReader["data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataEmprestimo = Convert.ToDateTime(dataReader["dataemprestimo"].ToString());
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

        public ReservaLivros Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            ReservaLivros _livros = new ReservaLivros();

            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select idreserva, idlivros, e.idcolaborador, data, dataemprestimo, c.nome");
                query.Append("  From niff_bib_reserva e, niff_ads_colaboradores c ");
                query.Append(" Where idlivros = " + codigo);
                query.Append("   and (dataemprestimo in null or dataemprestimo = '01-jan-0001')");
                query.Append("   and c.IdColaborador = e.idcolaborador");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _livros.Existe = true;

                        _livros.Existe = true;
                        _livros.Id = Convert.ToInt32(dataReader["idreserva"].ToString());
                        _livros.IdLivro = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());
                        _livros.NomeColaborador = dataReader["Nome"].ToString();

                        try
                        {
                            _livros.DataSolicitado = Convert.ToDateTime(dataReader["data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataEmprestimo = Convert.ToDateTime(dataReader["dataemprestimo"].ToString());
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

        public ReservaLivros Consulta(int codigo, int Colaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            ReservaLivros _livros = new ReservaLivros();

            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select idreserva, idlivros, idcolaborador, data, dataemprestimo");
                query.Append("  From niff_bib_reserva ");
                query.Append(" Where idlivros = " + codigo);
                query.Append("   And idcolaborador = " + Colaborador);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _livros.Existe = true;

                        _livros.Existe = true;
                        _livros.Id = Convert.ToInt32(dataReader["idreserva"].ToString());
                        _livros.IdLivro = Convert.ToInt32(dataReader["idlivros"].ToString());
                        _livros.IdColaborador = Convert.ToInt32(dataReader["idcolaborador"].ToString());

                        try
                        {
                            _livros.DataSolicitado = Convert.ToDateTime(dataReader["data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _livros.DataEmprestimo = Convert.ToDateTime(dataReader["dataemprestimo"].ToString());
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

        public bool Grava(ReservaLivros _livros)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!_livros.Existe)
                {
                    query.Clear();
                    query.Append("Insert into niff_bib_reserva");
                    query.Append(" ( idreserva, idlivros, idcolaborador, data)");
                    query.Append("   Values ( (Select  Nvl(Max(idreserva),0) + 1 next From niff_bib_reserva )");
                    query.Append("   , " + _livros.IdLivro);
                    query.Append("   , " + _livros.IdColaborador);
                    query.Append("   , To_date('" + _livros.DataSolicitado.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("   )");
                }
                else
                {
                    query.Clear();
                    query.Append("Update niff_bib_reserva");
                    query.Append("   set dataemprestimo = To_date('" + _livros.DataEmprestimo.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append(" Where idreserva = " + _livros.Id);
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
                query.Append("Delete niff_bib_reserva");
                query.Append(" Where idreserva = " + codigo);

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

                query.Append("Select Nvl(Max(idreserva),0) + 1 next From niff_bib_reserva");
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
