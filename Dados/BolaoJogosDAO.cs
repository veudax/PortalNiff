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
    public class BolaoJogosDAO
    {
        IDataReader dataReader;

        public List<BolaoJogos> Listar(int Ano, bool ApenasOsEncerrados)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<BolaoJogos> _lista = new List<BolaoJogos>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select j.id, j.data, j.idtime1, j.idtime2, j.placar1, j.placar2, j.datalimite, j.localizacao, j.fase");
                query.Append("     , t.Sigla Sigla1, t.Nome Nome1, t2.Sigla Sigla2, t2.Nome Nome2, j.JogoEncerrado, t.bandeira bandeira1, t2.Bandeira bandeira2");
                query.Append("     , j.Penalti1, j.Penalti2");

                query.Append("  from Niff_Bol_Jogos j, Niff_bol_Times t, Niff_bol_Times t2");

                query.Append(" Where j.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                
                query.Append("   And j.idTime1 = t.Id");
                query.Append("   And j.idTime2 = t2.Id");

                if (ApenasOsEncerrados) // os encerrados não notificados
                {
                    query.Append("   And j.JogoEncerrado = 'S'");
                    query.Append("   And j.Id Not In (Select n.idjogo From niff_bol_notificacaojogos n");
                    query.Append("                     where n.Idcolaborador = " + Publicas._idColaborador + ")");
                }
                else
                {
                    if (Publicas._jogosNaoFinalizados)
                        query.Append("   And  j.jogoencerrado = 'N'");
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        BolaoJogos _tipo = new BolaoJogos();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome1 = dataReader["nome1"].ToString();
                        _tipo.Nome2 = dataReader["nome2"].ToString();
                        _tipo.Sigla1 = dataReader["Sigla1"].ToString();
                        _tipo.Sigla2 = dataReader["Sigla2"].ToString();
                        _tipo.Localizacao = dataReader["Localizacao"].ToString();
                        _tipo.Fase = dataReader["Fase"].ToString();
                        _tipo.Encerrado = dataReader["JogoEncerrado"].ToString() == "S";

                        try
                        {
                            _tipo.Penalti1 = Convert.ToInt32(dataReader["Penalti1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Penalti1 = Convert.ToInt32(dataReader["Penalti2"].ToString());
                        }
                        catch { }

                        if (ApenasOsEncerrados)
                        {
                            try
                            {
                                _tipo.IdColaborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                            }
                            catch { }
                        }

                        try
                        {
                            _tipo.Bandeira1 = (byte[])(dataReader["bandeira1"]);
                        }
                        catch { }

                        try
                        {
                            _tipo.Bandeira2 = (byte[])(dataReader["bandeira2"]);
                        }
                        catch { }

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.LimitePalpite = Convert.ToDateTime(dataReader["datalimite"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTime1 = Convert.ToInt32(dataReader["IdTime1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTime2 = Convert.ToInt32(dataReader["IdTime2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Placar1 = Convert.ToInt32(dataReader["Placar1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Placar2 = Convert.ToInt32(dataReader["Placar2"].ToString());
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

        public List<BolaoJogos> Listar(int Ano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<BolaoJogos> _lista = new List<BolaoJogos>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select j.id, j.data, j.idtime1, j.idtime2, j.placar1, j.placar2, j.datalimite, j.localizacao, j.fase");
                query.Append("     , t.Sigla Sigla1, t.Nome Nome1, t2.Sigla Sigla2, t2.Nome Nome2, j.JogoEncerrado, t.bandeira bandeira1, t2.Bandeira bandeira2");
                query.Append("     , j.Penalti1, j.Penalti2");

                query.Append("  from Niff_Bol_Jogos j, Niff_bol_Times t, Niff_bol_Times t2");
                query.Append(" Where j.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");

                query.Append("   And j.idTime1 = t.Id");
                query.Append("   And j.idTime2 = t2.Id");
                query.Append("   And j.JogoEncerrado = 'N'");
                query.Append("   And j.Id not in (Select IdJogo From niff_bol_palpites p Where p.Idcolaborador = " + Publicas._idColaborador + ")");
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        BolaoJogos _tipo = new BolaoJogos();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome1 = dataReader["nome1"].ToString();
                        _tipo.Nome2 = dataReader["nome2"].ToString();
                        _tipo.Sigla1 = dataReader["Sigla1"].ToString();
                        _tipo.Sigla2 = dataReader["Sigla2"].ToString();
                        _tipo.Localizacao = dataReader["Localizacao"].ToString();
                        _tipo.Fase = dataReader["Fase"].ToString();
                        _tipo.Encerrado = dataReader["JogoEncerrado"].ToString() == "S";

                        try
                        {
                            _tipo.Penalti1 = Convert.ToInt32(dataReader["Penalti1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Penalti2 = Convert.ToInt32(dataReader["Penalti2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Bandeira1 = (byte[])(dataReader["bandeira1"]);
                        }
                        catch { }

                        try
                        {
                            _tipo.Bandeira2 = (byte[])(dataReader["bandeira2"]);
                        }
                        catch { }

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.LimitePalpite = Convert.ToDateTime(dataReader["datalimite"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTime1 = Convert.ToInt32(dataReader["IdTime1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTime2 = Convert.ToInt32(dataReader["IdTime2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Placar1 = Convert.ToInt32(dataReader["Placar1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Placar2 = Convert.ToInt32(dataReader["Placar2"].ToString());
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

        public List<BolaoJogos> Listar(DateTime _data)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<BolaoJogos> _lista = new List<BolaoJogos>();

            try
            {
                query.Append("Select j.id, j.data, j.idtime1, j.idtime2, j.placar1, j.placar2, j.datalimite, j.localizacao, j.fase");
                query.Append("     , t.Sigla Sigla1, t.Nome Nome1, t2.Sigla Sigla2, t2.Nome Nome2, j.JogoEncerrado");
                query.Append("     , j.Penalti1, j.Penalti2");
                query.Append("  from Niff_Bol_Jogos j, Niff_bol_Times t, Niff_bol_Times t2");
                query.Append(" Where j.data = To_date('" + _data.ToShortDateString() + " " + _data.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");

                query.Append("   And j.idTime1 = t.Id");
                query.Append("   And j.idTime2 = t2.Id");
                if (Publicas._jogosNaoFinalizados)
                    query.Append("   And  j.jogoencerrado = 'N'");


                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        BolaoJogos _tipo = new BolaoJogos();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome1 = dataReader["nome1"].ToString();
                        _tipo.Nome2 = dataReader["nome2"].ToString();
                        _tipo.Sigla1 = dataReader["Sigla1"].ToString();
                        _tipo.Sigla2 = dataReader["Sigla2"].ToString();
                        _tipo.Localizacao = dataReader["Localizacao"].ToString();
                        _tipo.Fase = dataReader["Fase"].ToString();
                        _tipo.Encerrado = dataReader["JogoEncerrado"].ToString() == "S";

                        try
                        {
                            _tipo.Penalti1 = Convert.ToInt32(dataReader["Penalti1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Penalti2 = Convert.ToInt32(dataReader["Penalti2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.LimitePalpite = Convert.ToDateTime(dataReader["datalimite"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTime1 = Convert.ToInt32(dataReader["IdTime1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTime2 = Convert.ToInt32(dataReader["IdTime2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Placar1 = Convert.ToInt32(dataReader["Placar1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Placar2 = Convert.ToInt32(dataReader["Placar2"].ToString());
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

        public BolaoJogos Consultar(DateTime _data)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            BolaoJogos _tipo = new BolaoJogos();

            try
            {
                query.Append("Select j.id, j.data, j.idtime1, j.idtime2, j.placar1, j.placar2, j.datalimite, j.localizacao, j.fase");
                query.Append("     , t.Sigla Sigla1, t.Nome Nome1, t2.Sigla Sigla2, t2.Nome Nome2, j.JogoEncerrado");
                query.Append("     , j.Penalti1, j.Penalti2");
                query.Append("  from Niff_Bol_Jogos j, Niff_bol_Times t, Niff_bol_Times t2");
                query.Append(" Where j.data = To_date('" + _data.ToShortDateString() + " " + _data.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");

                query.Append("   And j.idTime1 = t.Id");
                query.Append("   And j.idTime2 = t2.Id");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome1 = dataReader["nome1"].ToString();
                        _tipo.Nome2 = dataReader["nome2"].ToString();
                        _tipo.Sigla1 = dataReader["Sigla1"].ToString();
                        _tipo.Sigla2 = dataReader["Sigla2"].ToString();
                        _tipo.Localizacao = dataReader["Localizacao"].ToString();
                        _tipo.Fase = dataReader["Fase"].ToString();
                        _tipo.Encerrado = dataReader["JogoEncerrado"].ToString() == "S";

                        try
                        {
                            _tipo.Penalti1 = Convert.ToInt32(dataReader["Penalti1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Penalti2 = Convert.ToInt32(dataReader["Penalti2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.LimitePalpite = Convert.ToDateTime(dataReader["datalimite"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTime1 = Convert.ToInt32(dataReader["IdTime1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTime2 = Convert.ToInt32(dataReader["IdTime2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Placar1 = Convert.ToInt32(dataReader["Placar1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Placar2 = Convert.ToInt32(dataReader["Placar2"].ToString());
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

        public BolaoJogos Consultar(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            BolaoJogos _tipo = new BolaoJogos();

            try
            {
                query.Append("Select j.id, j.data, j.idtime1, j.idtime2, j.placar1, j.placar2, j.datalimite, j.localizacao, j.fase");
                query.Append("     , t.Sigla Sigla1, t.Nome Nome1, t2.Sigla Sigla2, t2.Nome Nome2, j.JogoEncerrado");
                query.Append("     , j.Penalti1, j.Penalti2");
                query.Append("  from Niff_Bol_Jogos j, Niff_bol_Times t, Niff_bol_Times t2");
                query.Append(" Where j.id = " + id);

                query.Append("   And j.idTime1 = t.Id");
                query.Append("   And j.idTime2 = t2.Id");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome1 = dataReader["nome1"].ToString();
                        _tipo.Nome2 = dataReader["nome2"].ToString();
                        _tipo.Sigla1 = dataReader["Sigla1"].ToString();
                        _tipo.Sigla2 = dataReader["Sigla2"].ToString();
                        _tipo.Localizacao = dataReader["Localizacao"].ToString();
                        _tipo.Fase = dataReader["Fase"].ToString();
                        _tipo.Encerrado = dataReader["JogoEncerrado"].ToString() == "S";

                        try
                        {
                            _tipo.Penalti1 = Convert.ToInt32(dataReader["Penalti1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Penalti2 = Convert.ToInt32(dataReader["Penalti2"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.LimitePalpite = Convert.ToDateTime(dataReader["datalimite"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTime1 = Convert.ToInt32(dataReader["IdTime1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTime2 = Convert.ToInt32(dataReader["IdTime2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Placar1 = Convert.ToInt32(dataReader["Placar1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Placar2 = Convert.ToInt32(dataReader["Placar2"].ToString());
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

        public BolaoJogos Consultar(int Ano, string Fase) 
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            BolaoJogos _tipo = new BolaoJogos();

            try
            {
                query.Append("Select Max(j.data) Data");
                query.Append("  from Niff_Bol_Jogos j");
                query.Append(" Where j.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                query.Append("   And Fase = '" + Fase + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
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

        public bool Grava(BolaoJogos times)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            OracleParameter parametro = new OracleParameter();
            List<OracleParameter> parametros = new List<OracleParameter>();

            try
            {
                times.Empritado = Publicas.Encrypta(times.Id.ToString() + times.Placar1.ToString() + times.Placar2.ToString() + times.Data.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                if (!times.Existe)
                {

                    query.Clear();
                    query.Append("Insert into Niff_Bol_Jogos");
                    query.Append("   (id, data, idtime1, idtime2, placar1, placar2, datalimite, localizacao, fase, Encripta");

                    query.Append("  ) Values ( (Select nvl(Max(Id),1)+1 From Niff_Bol_Jogos ) ");
                    query.Append(", To_Date('" + times.Data.ToShortDateString() + " " + times.Data.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                    query.Append(", " + times.IdTime1);
                    query.Append(", " + times.IdTime2 );
                    query.Append(", " + times.Placar1);
                    query.Append(", " + times.Placar2);
                    query.Append(", To_Date('" + times.LimitePalpite.ToShortDateString() + " " + times.LimitePalpite.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");

                    query.Append(", '" + times.Localizacao + "'");
                    query.Append(", '" + times.Fase + "'");
                    query.Append(", '" + times.Empritado + "'");

                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Bol_Jogos");
                    query.Append("   set Data = To_Date('" + times.Data.ToShortDateString() + " " + times.Data.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                    query.Append("     , IdTime1 = " + times.IdTime1);
                    query.Append("     , IdTime2 = " + times.IdTime2);
                    query.Append("     , Placar1 = " + times.Placar1);
                    query.Append("     , Placar2 = " + times.Placar2);
                    query.Append("     , datalimite = To_Date('" + times.LimitePalpite.ToShortDateString() + " " + times.LimitePalpite.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                    query.Append("     , JogoEncerrado = '" + (times.Encerrado ? "S" : "N") + "'");
                    query.Append("     , localizacao = '" + times.Localizacao + "'");
                    query.Append("     , fase = '" + times.Fase + "'");
                    query.Append("     , Encripta = '" + times.Empritado + "'");
                    query.Append("     , Penalti1 = " + times.Penalti1);
                    query.Append("     , Penalti2 = " + times.Penalti2);

                    query.Append(" Where Id = " + times.Id);
                }

                return sessao.ExecuteSqlTransaction(query.ToString(), parametros.ToArray());
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
                query.Append("Delete Niff_Bol_Jogos");
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
