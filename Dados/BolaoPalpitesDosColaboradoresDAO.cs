using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class BolaoPalpitesDosColaboradoresDAO
    {
        IDataReader dataReader;

        public List<BolaoPalpitesDosColaboradores> Listar(int Ano, int IdColaborador, bool naoCadastro)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<BolaoPalpitesDosColaboradores> _lista = new List<BolaoPalpitesDosColaboradores>();

            try
            {
                if (naoCadastro)
                {
                    query.Append("Select j.Data, j.id, 0 IdPalpite, j.Idtime1, j.Idtime2, 0 placar1, 0 placar2, j.datalimite, j.localizacao, 0 idColaborador");
                    query.Append("     , t1.nome Nome1, t1.bandeira bandeira1, t2.Nome nome2, t2.bandeira bandeira2, 0 Pontuacao, t1.Grupo grupo1, t2.Grupo grupo2");
                    query.Append("     , j.placar1 placarOficial1, j.placar2 placarOficial2, j.Fase, j.JogoEncerrado, null Encripta, null DataPalpite, null dataalteracao");
                    query.Append("     , j.Penalti1, j.Penalti2");
                    query.Append("  from Niff_Bol_Jogos j, Niff_bol_Times t1, Niff_bol_Times t2");
                    query.Append(" Where j.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                    query.Append("   And j.idTime1 = t1.Id");
                    query.Append("   And j.idTime2 = t2.Id");
                    query.Append("   And j.Id Not In (Select IdJogo From Niff_Bol_Palpites p Where p.Idcolaborador = " + IdColaborador + ")");
                }
                else
                {
                    query.Append("Select j.Data, j.id, p.id IdPalpite, j.Idtime1, j.Idtime2, p.placar1, p.placar2, j.datalimite, j.localizacao, p.IdColaborador");
                    query.Append("     , t1.nome Nome1, t1.bandeira bandeira1, t2.Nome nome2, t2.bandeira bandeira2, p.Pontuacao, t1.Grupo grupo1, t2.Grupo grupo2 ");
                    query.Append("     , j.placar1 placarOficial1, j.placar2 placarOficial2, j.Fase, j.JogoEncerrado, p.Encripta, p.Data DataPalpite, p.dataalteracao");
                    query.Append("     , j.Penalti1, j.Penalti2");
                    query.Append("  from Niff_Bol_Jogos j, Niff_bol_Times t1, Niff_bol_Times t2, niff_bol_palpites p");
                    query.Append(" Where j.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                    query.Append("   And j.idTime1 = t1.Id");
                    query.Append("   And j.idTime2 = t2.Id");
                    query.Append("   And j.Id = p.idjogo");
                    query.Append("   And p.Idcolaborador = " + IdColaborador);
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {

                        BolaoPalpitesDosColaboradores _tipo = new BolaoPalpitesDosColaboradores();

                        _tipo.Id = Convert.ToInt32(dataReader["IdPalpite"].ToString());
                        _tipo.Existe = _tipo.Id != 0;

                        _tipo.Nome1 = dataReader["nome1"].ToString();
                        _tipo.Nome2 = dataReader["nome2"].ToString();
                        _tipo.Grupo1 = dataReader["grupo1"].ToString();
                        _tipo.Grupo2 = dataReader["grupo2"].ToString();
                        _tipo.Fase = dataReader["Fase"].ToString();
                        _tipo.Localizacao = dataReader["localizacao"].ToString();
                        _tipo.Encerrado = dataReader["JogoEncerrado"].ToString() == "S";

                        _tipo.IdColaborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());

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
                            _tipo.Empritado = dataReader["Encripta"].ToString();
                        }
                        catch { }

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataDoPalpite = Convert.ToDateTime(dataReader["DataPalpite"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataAlteracao = Convert.ToDateTime(dataReader["DataAlteracao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataLimite = Convert.ToDateTime(dataReader["datalimite"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Pontuacao = Convert.ToInt32(dataReader["Pontuacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdJogo = Convert.ToInt32(dataReader["Id"].ToString());
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

                        try
                        {
                            _tipo.PlacarOficial1 = Convert.ToInt32(dataReader["PlacarOficial1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PlacarOficial2 = Convert.ToInt32(dataReader["PlacarOficial2"].ToString());
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

        public List<BolaoPalpitesDosColaboradores> Listar(int IdJogos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<BolaoPalpitesDosColaboradores> _lista = new List<BolaoPalpitesDosColaboradores>();

            try
            {
                query.Append("Select j.Data, j.id, p.id IdPalpite, j.Idtime1, j.Idtime2, p.placar1, p.placar2, j.datalimite, j.localizacao");
                query.Append("     , t1.nome Nome1, t1.bandeira bandeira1, t2.Nome nome2, t2.bandeira bandeira2, p.Pontuacao, t1.Grupo grupo1, t2.Grupo grupo2 ");
                query.Append("     , j.placar1 placarOficial1, j.placar2 placarOficial2, j.Fase, p.DataAlteracao, p.Data DataDoPalpite, p.IdColaborador");
                query.Append("     , j.Penalti1, j.Penalti2");
                query.Append("  from Niff_Bol_Jogos j, Niff_bol_Times t1, Niff_bol_Times t2, niff_bol_palpites p");
                query.Append(" Where j.idTime1 = t1.Id");
                query.Append("   And j.idTime2 = t2.Id");
                query.Append("   And j.Id = p.idjogo");
                query.Append("   And p.idjogo = " + IdJogos);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {

                        BolaoPalpitesDosColaboradores _tipo = new BolaoPalpitesDosColaboradores();

                        _tipo.Id = Convert.ToInt32(dataReader["IdPalpite"].ToString());
                        _tipo.Existe = _tipo.Id != 0;

                        _tipo.Nome1 = dataReader["nome1"].ToString();
                        _tipo.Nome2 = dataReader["nome2"].ToString();
                        _tipo.Grupo1 = dataReader["grupo1"].ToString();
                        _tipo.Grupo2 = dataReader["grupo2"].ToString();
                        _tipo.Fase = dataReader["Fase"].ToString();
                        _tipo.Localizacao = dataReader["localizacao"].ToString();
                        _tipo.IdColaborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());

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
                            _tipo.DataLimite = Convert.ToDateTime(dataReader["datalimite"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataAlteracao = Convert.ToDateTime(dataReader["DataAlteracao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataDoPalpite = Convert.ToDateTime(dataReader["DataDoPalpite"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Pontuacao = Convert.ToInt32(dataReader["Pontuacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdJogo = Convert.ToInt32(dataReader["Id"].ToString());
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

                        try
                        {
                            _tipo.PlacarOficial1 = Convert.ToInt32(dataReader["PlacarOficial1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PlacarOficial2 = Convert.ToInt32(dataReader["PlacarOficial2"].ToString());
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

        public List<BolaoPalpitesDosColaboradores> AcompanharJogos(int Ano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<BolaoPalpitesDosColaboradores> _lista = new List<BolaoPalpitesDosColaboradores>();

            try
            {
                query.Append("Select c.Nome nomeColaborador, t1.nome nome1, t1.bandeira bandeira1, p.placar1, p.placar2, t2.nome nome2, t2.bandeira bandeira2, j.data");
                query.Append("     , j.placar1 PlacarOficial1, j.placar2 PlacarOficial2, j.Penalti1, j.Penalti2");
                query.Append("  from Niff_Bol_Jogos j, Niff_bol_Times t1, Niff_bol_Times t2, niff_bol_palpites p, Niff_Ads_Colaboradores c");
                query.Append(" Where j.idTime1 = t1.Id");
                query.Append("   And j.idTime2 = t2.Id");
                query.Append("   And j.Id = p.idjogo");
                query.Append("   And p.Idcolaborador = c.IdColaborador");
                query.Append("   And j.datalimite <= Sysdate");
                query.Append("   And trunc(j.Data) = trunc(Sysdate)");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {

                        BolaoPalpitesDosColaboradores _tipo = new BolaoPalpitesDosColaboradores();

                        _tipo.Existe = true;

                        _tipo.NomeColaborador = dataReader["nomeColaborador"].ToString();
                        _tipo.Nome1 = dataReader["nome1"].ToString();
                        _tipo.Nome2 = dataReader["nome2"].ToString();
                        _tipo.Grupo1 = "x";

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
                            _tipo.Placar1 = Convert.ToInt32(dataReader["Placar1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Placar2 = Convert.ToInt32(dataReader["Placar2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PlacarOficial1 = Convert.ToInt32(dataReader["PlacarOficial1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PlacarOficial2 = Convert.ToInt32(dataReader["PlacarOficial2"].ToString());
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

        public List<BolaoPalpitesDosColaboradores> Ranking(int Ano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<BolaoPalpitesDosColaboradores> _lista = new List<BolaoPalpitesDosColaboradores>();

            try
            {
                query.Append("Select Rownum Clas, a.*");
                query.Append("  From ( ");

                query.Append("Select Pontuacao, Qtdextatos, Qtdganhadorplacar, Qtdganhador, Qtdempates, Qtde1placar, Nome, Id ");
                query.Append("  From ( ");
                query.Append("Select Sum(pontuacao) Pontuacao, Sum(QtdExtatos) QtdExtatos, Sum(QtdGanhadorPlacar) QtdGanhadorPlacar");
                query.Append("     , Sum(QtdGanhador) QtdGanhador, Sum(QtdEmpates) QtdEmpates, Sum(Qtde1Placar) Qtde1Placar");
                query.Append("     , nome, id");
                query.Append("  From (Select Nvl(Sum(p.pontuacao),0) Pontuacao, c.nome, c.Idcolaborador id,  0 QtdExtatos, 0 QtdGanhadorPlacar, 0 QtdGanhador, 0 QtdEmpates, 0 Qtde1Placar");
                query.Append("          From niff_bol_palpites p, Niff_Ads_Colaboradores c");
                query.Append("         Where c.Idcolaborador = p.Idcolaborador");
                query.Append("           And p.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                query.Append("         Group by c.Nome, c.Idcolaborador");
                query.Append("         Union All ");
                query.Append("        Select 0 Pontuacao, c.nome, c.Idcolaborador id,  Count(Pontuacao) QtdExtatos, 0 QtdGanhadorPlacar, 0 QtdGanhador, 0 QtdEmpates, 0 Qtde1Placar");
                query.Append("          From niff_bol_palpites p, Niff_Ads_Colaboradores c");
                query.Append("         Where c.Idcolaborador = p.Idcolaborador");
                query.Append("           And p.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                query.Append("           And p.pontuacao = (Select o.Pontos From niff_bol_pontuacao o Where o.nome = 'Exato')");
                query.Append("         Group by c.Nome, c.Idcolaborador");
                query.Append("         Union All ");
                query.Append("        Select 0 Pontuacao, c.nome, c.Idcolaborador id,  0 QtdExtatos, Count(Pontuacao) QtdGanhadorPlacar, 0 QtdGanhador, 0 QtdEmpates, 0 Qtde1Placar");
                query.Append("          From niff_bol_palpites p, Niff_Ads_Colaboradores c");
                query.Append("         Where c.Idcolaborador = p.Idcolaborador");
                query.Append("           And p.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                query.Append("           And p.pontuacao = (Select o.Pontos From niff_bol_pontuacao o Where o.nome = 'Ganhador + 1 Placar')");
                query.Append("         Group by c.Nome, c.Idcolaborador");
                query.Append("         Union All ");
                query.Append("        Select 0 Pontuacao, c.nome, c.Idcolaborador id,  0 QtdExtatos, 0 QtdGanhadorPlacar, Count(Pontuacao) QtdGanhador, 0 QtdEmpates, 0 Qtde1Placar");
                query.Append("          From niff_bol_palpites p, Niff_Ads_Colaboradores c");
                query.Append("         Where c.Idcolaborador = p.Idcolaborador");
                query.Append("           And p.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                query.Append("           And p.pontuacao = (Select o.Pontos From niff_bol_pontuacao o Where o.nome = 'Ganhador')");
                query.Append("         Group by c.Nome, c.Idcolaborador");
                query.Append("         Union All ");
                query.Append("        Select 0 Pontuacao, c.nome, c.Idcolaborador id,  0 QtdExtatos, 0 QtdGanhadorPlacar, 0 QtdGanhador, Count(Pontuacao) QtdEmpates, 0 Qtde1Placar");
                query.Append("          From niff_bol_palpites p, Niff_Ads_Colaboradores c");
                query.Append("         Where c.Idcolaborador = p.Idcolaborador");
                query.Append("           And p.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                query.Append("           And p.pontuacao = (Select o.Pontos From niff_bol_pontuacao o Where o.nome = 'Empates placar diferente')");
                query.Append("         Group by c.Nome, c.Idcolaborador");
                query.Append("         Union All ");
                query.Append("        Select 0 Pontuacao, c.nome, c.Idcolaborador id,  0 QtdExtatos, 0 QtdGanhadorPlacar, 0 QtdGanhador, 0 QtdEmpates, Count(Pontuacao)  Qtde1Placar");
                query.Append("          From niff_bol_palpites p, Niff_Ads_Colaboradores c");
                query.Append("         Where c.Idcolaborador = p.Idcolaborador");
                query.Append("           And p.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                query.Append("           And p.pontuacao = (Select o.Pontos From niff_bol_pontuacao o Where o.nome = '1 Placar')");
                query.Append("         Group by c.Nome, c.Idcolaborador");
                query.Append("         Union All ");
                query.Append("        Select Nvl(Sum(p.pontuacao),0) Pontuacao, c.nome, c.Idcolaborador id,  0 QtdExtatos, 0 QtdGanhadorPlacar, 0 QtdGanhador, 0 QtdEmpates, 0 Qtde1Placar");
                query.Append("          From niff_bol_palpitefinal p, Niff_Ads_Colaboradores c");
                query.Append("         Where c.Idcolaborador = p.Idcolaborador");
                query.Append("           And p.data between To_date('" + new DateTime(Ano, 01, 01) + "', 'dd/mm/yyyy hh24:mi:ss') and To_date('" + new DateTime(Ano, 12, 31) + "', 'dd/mm/yyyy hh24:mi:ss')");
                query.Append("         Group by c.Nome, c.Idcolaborador ");
                query.Append("       ) Group by Nome, id");
                query.Append(" ) Order By Pontuacao Desc, Qtdextatos Desc, Qtdganhadorplacar Desc, Qtdganhador Desc, Qtdempates Desc, Qtde1placar Desc, Nome");
                query.Append(" ) a");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        BolaoValorArrecadado _valor = new BolaoValorArrecadadoDAO().Consultar(Ano);

                        BolaoPalpitesDosColaboradores _tipo = new BolaoPalpitesDosColaboradores();

                        _tipo.Existe = true;

                        _tipo.NomeColaborador = dataReader["Nome"].ToString();

                        try
                        {
                            _tipo.IdColaborador = Convert.ToInt32(dataReader["Id"].ToString());
                        }
                        catch { }

                        BolaoPalpiteFinalDoColaborador _palpites = new BolaoPalpiteFinalDoColaboradorDAO().Consultar(_tipo.IdColaborador, Ano);

                        _tipo.Acetou3Lugar = _palpites.Acertou3Lugar;
                        _tipo.AcetouCampeao = _palpites.AcertouCampeao;
                        _tipo.AcetouViceCampeao = _palpites.AcertouViceCampeao;

                        try
                        {
                            _tipo.Classificacao = Convert.ToInt32(dataReader["Clas"].ToString());
                        }
                        catch { }

                        _tipo.ValorPremio = 0;

                        switch (_tipo.Classificacao)
                        {
                            case 1: _tipo.ValorPremio = (_valor.Valor * 80 / 100);
                                break;
                            case 2:
                                _tipo.ValorPremio = (_valor.Valor * 15 / 100);
                                break;
                            case 3:
                                _tipo.ValorPremio = (_valor.Valor * 5 / 100);
                                break;
                        }

                        try
                        {
                            _tipo.Pontuacao = Convert.ToInt32(dataReader["Pontuacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.QuantidadeAcertosPlacarExato = Convert.ToInt32(dataReader["QtdExtatos"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.QuantidadeAcertosGanhadorEPlacar = Convert.ToInt32(dataReader["QtdGanhadorPlacar"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.QuantidadeAcertosGanhador = Convert.ToInt32(dataReader["QtdGanhador"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.QuantidadeAcertosEmpates = Convert.ToInt32(dataReader["QtdEmpates"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.QuantidadeAcertos1Placar = Convert.ToInt32(dataReader["Qtde1Placar"].ToString());
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

        public BolaoPalpitesDosColaboradores Consultar(int IdColaborador, int IdJogo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            BolaoPalpitesDosColaboradores _tipo = new BolaoPalpitesDosColaboradores();

            try
            {
                query.Append("Select p.id, p.data DataDoPalpite, j.idtime1, j.idtime2, p.placar1, p.placar2, p.DataAlteracao, p.Pontuacao, p.IdJogo");
                query.Append("     , t.Sigla Sigla1, t.Nome Nome1, t2.Sigla Sigla2, t2.Nome Nome2, c.Nome NomeColaborador, j.Data, p.idColaborador ");
                query.Append("     , j.Penalti1, j.Penalti2");
                query.Append("  from Niff_bol_Palpites p, Niff_Bol_Jogos j, Niff_bol_Times t, Niff_bol_Times t2, Niff_Ads_Colaboradores c");
                query.Append(" Where j.Id = " + IdJogo);
                query.Append("   And c.IdColaborador = " + IdColaborador);
                query.Append("   And j.id = p.IdJogo");
                query.Append("   And j.idTime1 = t.Id");
                query.Append("   And j.idTime2 = t2.Id");
                query.Append("   And c.IdColaborador = p.IdColaborador ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome1 = dataReader["nome1"].ToString();
                        _tipo.Nome2 = dataReader["nome2"].ToString();
                        _tipo.Sigla1 = dataReader["Sigla1"].ToString();
                        _tipo.Sigla2 = dataReader["Sigla2"].ToString();
                        _tipo.NomeColaborador = dataReader["NomeColaborador"].ToString();
                        _tipo.IdColaborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());

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
                            _tipo.DataDoPalpite = Convert.ToDateTime(dataReader["DataDoPalpite"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Pontuacao = Convert.ToInt32(dataReader["Pontuacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdJogo = Convert.ToInt32(dataReader["IdJogo"].ToString());
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

        public bool Grava(List<BolaoPalpitesDosColaboradores> listaPalpites)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            DateTime _dataBancoDados;

            try
            {
                foreach (var times in listaPalpites.OrderBy(o => o.Data))
                {
                    _dataBancoDados = new BancoDeDadosDAO().DataBanco();
                    DateTime _dataSistema = DateTime.Now;

                    if ((times.DataLimite >= _dataSistema || string.IsNullOrEmpty(times.Empritado)))
                        times.Empritado = Publicas.Encrypta(times.IdColaborador.ToString() + times.IdJogo.ToString() +
                            times.Placar1.ToString() + times.Placar2.ToString() + times.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                    if (!times.Existe)
                    {

                        query.Clear();
                        query.Append("Insert into Niff_bol_Palpites");
                        query.Append("   (id, data,  idcolaborador, idjogo, placar1, placar2, pontuacao, Encripta");
                        query.Append("  ) Values ( SQ_NIFF_IdBolPalpite.NextVal ");
                        query.Append(", To_Date('" + _dataBancoDados.ToShortDateString() + " " + _dataBancoDados.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                        query.Append(", " + times.IdColaborador);
                        query.Append(", " + times.IdJogo);
                        query.Append(", " + times.Placar1);
                        query.Append(", " + times.Placar2);
                        query.Append(", 0");
                        query.Append(", '" + times.Empritado + "'");
                        query.Append(") ");
                    }
                    else
                    {
                        query.Clear();
                        if (times.DataLimite >= _dataSistema)
                        {
                            query.Append("Update Niff_bol_Palpites");
                            query.Append("   set Placar1 = " + times.Placar1);
                            query.Append("     , Placar2 = " + times.Placar2);
                            query.Append("     , dataalteracao = To_Date('" + _dataBancoDados.ToShortDateString() + " " + _dataBancoDados.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                            query.Append("     , Encripta = '" + times.Empritado + "'");
                            query.Append(" Where Id = " + times.Id);
                        }
                    }

                    retorno = true;

                    if (!times.Existe || (times.Existe && times.DataLimite >= _dataSistema))
                        retorno = sessao.ExecuteSqlTransaction(query.ToString(), null);

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

        public bool GravaPontuacao(List<BolaoPalpitesDosColaboradores> listaPalpites)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;

            try
            {
                foreach (var times in listaPalpites)
                {
                    BolaoValorArrecadado _valor = new BolaoValorArrecadadoDAO().Consultar(DateTime.Now.Year, times.Id);

                    //if (!_valor.Existe && DateTime.Now.Date > new DateTime(2018,06,21)) // para quem não efetuou o pagamento até dia 20
                    //    times.Pontuacao = 0;
                    //else
                        times.Empritado = Publicas.Encrypta(times.IdColaborador.ToString() + times.IdJogo.ToString() +
                                                            times.Placar1.ToString() + times.Placar2.ToString() + 
                                                            times.Pontuacao.ToString()
                                                           , Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                    query.Clear();
                    query.Append("Update Niff_bol_Palpites");
                    query.Append("   set pontuacao = " + times.Pontuacao);
                    query.Append("     , Encripta = '" + times.Empritado + "'");
                    query.Append(" Where Id = " + times.Id);
                    
                    retorno = sessao.ExecuteSqlTransaction(query.ToString(), null);

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

        public bool Exclui(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_bol_Palpites");
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
