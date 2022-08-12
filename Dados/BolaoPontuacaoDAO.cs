using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class BolaoPontuacaoDAO
    {
        IDataReader dataReader;

        public List<BolaoPontuacao> Listar(int Ano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<BolaoPontuacao> _lista = new List<BolaoPontuacao>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, nome, pontos, ano");
                query.Append("  from Niff_Bol_Pontuacao     ");
                query.Append(" Where Ano = " + Ano);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        BolaoPontuacao _tipo = new BolaoPontuacao();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();

                        try
                        {
                            _tipo.Ano = Convert.ToInt32(dataReader["Ano"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Ponto = Convert.ToInt32(dataReader["pontos"].ToString());
                        }
                        catch { }

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

        public BolaoPontuacao Consultar(int Ano, string Nome)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            BolaoPontuacao _tipo = new BolaoPontuacao();

            try
            {
                query.Append("Select id, nome, pontos, ano");
                query.Append("  from Niff_Bol_Pontuacao     ");
                query.Append(" Where Nome = '" + Nome + "'");
                query.Append("   And Ano = " + Ano);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();

                        try
                        {
                            _tipo.Ano = Convert.ToInt32(dataReader["Ano"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Ponto = Convert.ToInt32(dataReader["pontos"].ToString());
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
            return _tipo;
        }

        public bool Grava(BolaoPontuacao times)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!times.Existe)
                {

                    query.Clear();
                    query.Append("Insert into Niff_Bol_Pontuacao");
                    query.Append("   (id, nome, pontos, ano");

                    query.Append("  ) Values ( (Select nvl(Max(Id),1)+1 From Niff_Bol_Pontuacao ) ");
                    query.Append(", '" + times.Nome + "'");
                    query.Append(", " + times.Ponto);
                    query.Append(", " + times.Ano );

                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Bol_Pontuacao");
                    query.Append("   set nome = '" + times.Nome + "'");
                    query.Append("     , Ano = " + times.Ano);
                    query.Append("     , Pontos = '" + times.Ponto + "'");
                    query.Append(" Where Id = " + times.Id);
                }

                return sessao.ExecuteSqlTransaction(query.ToString(), null);
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

        public bool Exclui(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Bol_Pontuacao");
                query.Append(" Where id = " + id);

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
