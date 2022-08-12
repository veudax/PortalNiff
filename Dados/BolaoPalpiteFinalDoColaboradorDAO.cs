using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class BolaoPalpiteFinalDoColaboradorDAO
    {
        IDataReader dataReader;

        public List<BolaoPalpiteFinalDoColaborador> Listar(int Ano, int Colaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<BolaoPalpiteFinalDoColaborador> _lista = new List<BolaoPalpiteFinalDoColaborador>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select p.id, p.idcolaborador, p.idtimevencedor, p.idtimevice, p.idtime3lugar, p.data, p.dataalteracao");
                query.Append("     , t.Sigla Sigla1, t.Nome Nome1, t2.Sigla Sigla2, t2.Nome Nome2, c.Nome NomeColaborador ");
                query.Append("     , t3.Sigla Sigla3, t3.Nome Nome3");

                query.Append("  from Niff_bol_PalpiteFinal p, Niff_bol_Times t, Niff_bol_Times t2, Niff_bol_Times t3, Niff_Ads_Colaboradores c");
                query.Append(" Where p.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");

                if (Colaborador != 0)
                    query.Append("   And c.IdColaborador = " + Colaborador);

                query.Append("   And p.idtimevencedor = t.Id");
                query.Append("   And p.idtimevice = t2.Id");
                query.Append("   And p.idtime3lugar = t3.Id");
                query.Append("   And c.IdColaborador = p.IdColaborador ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        BolaoPalpiteFinalDoColaborador _tipo = new BolaoPalpiteFinalDoColaborador();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome1 = dataReader["nome1"].ToString();
                        _tipo.Nome2 = dataReader["nome2"].ToString();
                        _tipo.Nome3 = dataReader["nome3"].ToString();
                        _tipo.Sigla1 = dataReader["Sigla1"].ToString();
                        _tipo.Sigla2 = dataReader["Sigla2"].ToString();
                        _tipo.Sigla3 = dataReader["Sigla3"].ToString();
                        _tipo.NomeColaborador = dataReader["NomeColaborador"].ToString();

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataAlteracao = Convert.ToDateTime(dataReader["DataAlteracao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTimeCampeao = Convert.ToInt32(dataReader["idtimevencedor"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTimeVice = Convert.ToInt32(dataReader["idtimevice"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTime3Lugar = Convert.ToInt32(dataReader["idtime3lugar"].ToString());
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

        public List<BolaoPalpiteFinalDoColaborador> Listar(int Ano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<BolaoPalpiteFinalDoColaborador> _lista = new List<BolaoPalpiteFinalDoColaborador>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select p.id, p.idcolaborador, p.idtimevencedor, p.idtimevice, p.idtime3lugar, p.data, p.dataalteracao");
                query.Append("     , t.Sigla Sigla1, t.Nome Nome1, t2.Sigla Sigla2, t2.Nome Nome2, c.Nome NomeColaborador ");
                query.Append("     , t3.Sigla Sigla3, t3.Nome Nome3");

                query.Append("  from Niff_bol_PalpiteFinal p, Niff_bol_Times t, Niff_bol_Times t3, Niff_bol_Times t2, Niff_Ads_Colaboradores c");
                query.Append(" Where p.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");

                query.Append("   And p.idtimevencedor = t.Id");
                query.Append("   And p.idtimevice = t2.Id");
                query.Append("   And p.idtime3lugar = t3.Id");
                query.Append("   And c.IdColaborador = p.IdColaborador ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        BolaoPalpiteFinalDoColaborador _tipo = new BolaoPalpiteFinalDoColaborador();

                        _tipo.Existe = true;
                        _tipo.Id = 1;

                        _tipo.Nome1 = dataReader["nome1"].ToString();
                        _tipo.NomeColaborador = dataReader["NomeColaborador"].ToString();

                        _lista.Add(_tipo);

                        _tipo = new BolaoPalpiteFinalDoColaborador();

                        _tipo.Existe = true;
                        _tipo.Id = 2;

                        _tipo.Nome1 = dataReader["nome2"].ToString();
                        _tipo.NomeColaborador = dataReader["NomeColaborador"].ToString();

                        _lista.Add(_tipo);

                        _tipo = new BolaoPalpiteFinalDoColaborador();

                        _tipo.Existe = true;
                        _tipo.Id = 3;

                        _tipo.Nome1 = dataReader["nome3"].ToString();
                        _tipo.NomeColaborador = dataReader["NomeColaborador"].ToString();

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

        public BolaoPalpiteFinalDoColaborador Consultar(int IdColaborador, int Ano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            BolaoPalpiteFinalDoColaborador _tipo = new BolaoPalpiteFinalDoColaborador();

            try
            {
                query.Append("Select p.id, p.idcolaborador, p.idtimevencedor, p.idtimevice, p.idtime3lugar, p.data, p.dataalteracao, p.pontuacao");
                query.Append("     , t.Sigla Sigla1, t.Nome Nome1, t2.Sigla Sigla2, t2.Nome Nome2, c.Nome NomeColaborador ");
                query.Append("     , t.Sigla Sigla3, t.Nome Nome3, p.AcertouCampeao, p.AcertouViceCampeao, p.acertou3lugar");

                query.Append("  from Niff_bol_PalpiteFinal p, Niff_bol_Times t, Niff_bol_Times t2, Niff_bol_Times t3, Niff_Ads_Colaboradores c");
                query.Append(" Where p.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                query.Append("   And c.IdColaborador = " + IdColaborador);

                query.Append("   And p.idtimevencedor = t.Id");
                query.Append("   And p.idtimevice = t2.Id");
                query.Append("   And p.idtime3lugar = t3.Id");
                query.Append("   And c.IdColaborador = p.IdColaborador ");

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
                        _tipo.Nome3 = dataReader["nome3"].ToString();
                        _tipo.Sigla1 = dataReader["Sigla1"].ToString();
                        _tipo.Sigla2 = dataReader["Sigla2"].ToString();
                        _tipo.Sigla3 = dataReader["Sigla3"].ToString();
                        _tipo.NomeColaborador = dataReader["NomeColaborador"].ToString();
                        _tipo.AcertouCampeao = dataReader["AcertouCampeao"].ToString() == "S";
                        _tipo.AcertouViceCampeao = dataReader["AcertouViceCampeao"].ToString() == "S";
                        _tipo.Acertou3Lugar = dataReader["Acertou3Lugar"].ToString() == "S";
                        _tipo.Pontuacao = Convert.ToInt32(dataReader["Pontuacao"].ToString());

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataAlteracao = Convert.ToDateTime(dataReader["DataAlteracao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTimeCampeao = Convert.ToInt32(dataReader["idtimevencedor"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTimeVice = Convert.ToInt32(dataReader["idtimevice"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdTime3Lugar = Convert.ToInt32(dataReader["idtime3lugar"].ToString());
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

        public bool Grava(BolaoPalpiteFinalDoColaborador times)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                times.Empritado = Publicas.Encrypta(times.IdColaborador.ToString() + times.IdTimeCampeao.ToString() + times.IdTimeVice.ToString() + times.IdTime3Lugar.ToString() + times.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                if (!times.Existe)
                {

                    query.Clear();
                    query.Append("Insert into Niff_bol_PalpiteFinal");
                    query.Append("   (id, idcolaborador,  idtimevencedor, idtimevice, idtime3lugar, data, Encripta");

                    query.Append("  ) Values ( SQ_NIFF_IdBolPalpiteFim.NextVal ");
                    query.Append(", " + times.IdColaborador);
                    query.Append(", " + times.IdTimeCampeao);
                    query.Append(", " + times.IdTimeVice);
                    query.Append(", " + times.IdTime3Lugar);
                    query.Append(", Sysdate");
                    query.Append(", '" + times.Empritado + "'");

                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_bol_PalpiteFinal");
                    query.Append("   set dataalteracao = SysDate");
                    query.Append("     , idtimevencedor = " + times.IdTimeCampeao);
                    query.Append("     , idtimevice = " + times.IdTimeVice);
                    query.Append("     , idtime3lugar = " + times.IdTime3Lugar);
                    query.Append("     , Encripta = '" + times.Empritado + "'");
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

        public bool GravaPontuacao(BolaoPalpiteFinalDoColaborador times)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                times.Empritado = Publicas.Encrypta(times.IdColaborador.ToString() + times.IdTimeCampeao.ToString() + times.IdTimeVice.ToString() + times.IdTime3Lugar.ToString() + times.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                query.Clear();
                query.Append("Update Niff_bol_PalpiteFinal");
                query.Append("   set dataalteracao = SysDate");
                query.Append("     , pontuacao = " + times.Pontuacao);
                query.Append("     , AcertouCampeao = '" + (times.AcertouCampeao ? "S" : "N") + "'");
                query.Append("     , AcertouViceCampeao = '" + (times.AcertouViceCampeao ? "S" : "N") + "'");
                query.Append("     , Acertou3Lugar = '" + (times.Acertou3Lugar ? "S" : "N") + "'");
                query.Append("     , Encripta = '" + times.Empritado + "'");
                query.Append(" Where Id = " + times.Id);

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
                query.Append("Delete Niff_bol_PalpiteFinal");
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
