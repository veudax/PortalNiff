using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class BolaoEnqueteDAO
    {
        IDataReader dataReader;

        public List<BolaoEnquete> Listar(int idColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<BolaoEnquete> _lista = new List<BolaoEnquete>();

            try
            {
                query.Append("Select id, IdPergunta, IdColaborador, Opcao, Sugestao ");
                query.Append("  from niff_bol_enqueteresp");
                query.Append(" Where IdColaborador = " + idColaborador);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        BolaoEnquete _bolao = new BolaoEnquete();
                        _bolao.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _bolao.IdColaborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        _bolao.IdPergunta = Convert.ToInt32(dataReader["IdPergunta"].ToString());
                        _bolao.Opcao = dataReader["Opcao"].ToString();
                        _bolao.Sugestao = dataReader["Sugestao"].ToString();

                        _lista.Add(_bolao);
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

        public bool Consultar(int idColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select opcao ");
                query.Append("  from niff_bol_enqueteresp");
                query.Append(" Where IdColaborador = " + idColaborador);
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        return true;
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
            return false;
        }

        public int[] ListarQuantidadeParticipantes()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int[] _Qtd = new int[3];

            try
            {
                query.Append("Select Count(*) qtd, opcao ");
                query.Append("  from niff_bol_enqueteresp");
                query.Append(" Where Idpergunta = 7");
                query.Append(" Group by Opcao");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        if (dataReader["Opcao"].ToString() == "S")
                            _Qtd[0] = Convert.ToInt32(dataReader["qtd"].ToString());
                        else
                            _Qtd[1] = Convert.ToInt32(dataReader["qtd"].ToString());

                        _Qtd[2] = _Qtd[0] + _Qtd[1];
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

            return _Qtd;
        }

        public int[] QuantidadeGostouBolao()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int[] _Qtd = new int[3];

            try
            {
                query.Append("Select Count(*) qtd, opcao ");
                query.Append("  from niff_bol_enqueteresp");
                query.Append(" Where Idpergunta = 1");
                query.Append(" Group by Opcao");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        if (dataReader["Opcao"].ToString() == "S")
                            _Qtd[0] = Convert.ToInt32(dataReader["qtd"].ToString());
                        else
                            _Qtd[1] = Convert.ToInt32(dataReader["qtd"].ToString());

                        _Qtd[2] = _Qtd[0] + _Qtd[1];
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

            return _Qtd;
        }

        public int[] QuantidadeMudarDivisaoPremiacao()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int[] _Qtd = new int[3];

            try
            {
                query.Append("Select Count(*) qtd, opcao ");
                query.Append("  from niff_bol_enqueteresp");
                query.Append(" Where Idpergunta = 3");
                query.Append(" Group by Opcao");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        if (dataReader["Opcao"].ToString() == "S")
                            _Qtd[0] = Convert.ToInt32(dataReader["qtd"].ToString());
                        else
                            _Qtd[1] = Convert.ToInt32(dataReader["qtd"].ToString());

                        _Qtd[2] = _Qtd[0] + _Qtd[1];
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

            return _Qtd;
        }

        public int[] QuantidadeMudarPontuacao()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int[] _Qtd = new int[3];

            try
            {
                query.Append("Select Count(*) qtd, opcao ");
                query.Append("  from niff_bol_enqueteresp");
                query.Append(" Where Idpergunta = 4");
                query.Append(" Group by Opcao");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        if (dataReader["Opcao"].ToString() == "S")
                            _Qtd[0] = Convert.ToInt32(dataReader["qtd"].ToString());
                        else
                            _Qtd[1] = Convert.ToInt32(dataReader["qtd"].ToString());

                        _Qtd[2] = _Qtd[0] + _Qtd[1];
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

            return _Qtd;
        }

        public int[] QuantidadeMudarArtilheiro()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int[] _Qtd = new int[3];

            try
            {
                query.Append("Select Count(*) qtd, opcao ");
                query.Append("  from niff_bol_enqueteresp");
                query.Append(" Where Idpergunta = 5");
                query.Append(" Group by Opcao");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        if (dataReader["Opcao"].ToString() == "S")
                            _Qtd[0] = Convert.ToInt32(dataReader["qtd"].ToString());
                        else
                            _Qtd[1] = Convert.ToInt32(dataReader["qtd"].ToString());

                        _Qtd[2] = _Qtd[0] + _Qtd[1];
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

            return _Qtd;
        }

        public int[] QuantidadeMudarPlacarInverso()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int[] _Qtd = new int[3];

            try
            {
                query.Append("Select Count(*) qtd, opcao ");
                query.Append("  from niff_bol_enqueteresp");
                query.Append(" Where Idpergunta = 6");
                query.Append(" Group by Opcao");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        if (dataReader["Opcao"].ToString() == "S")
                            _Qtd[0] = Convert.ToInt32(dataReader["qtd"].ToString());
                        else
                            _Qtd[1] = Convert.ToInt32(dataReader["qtd"].ToString());

                        _Qtd[2] = _Qtd[0] + _Qtd[1];
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

            return _Qtd;
        }

        public bool Grava(BolaoEnquete times)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {                
                    query.Clear();
                    query.Append("Insert into niff_bol_enqueteresp");
                    query.Append("   (id, IdPergunta, IdColaborador, Opcao, Sugestao");

                    query.Append("  ) Values ( (Select nvl(Max(Id),1)+1 From niff_bol_enqueteresp ) ");
                    query.Append(", " + times.IdPergunta);
                    query.Append(", " + (times.IdColaborador == 0 ? "null" : times.IdColaborador.ToString()));
                    query.Append(", '" + times.Opcao + "'");
                    query.Append(", '" + times.Sugestao + "'");
                    query.Append(") ");
                
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
