using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class BancoDeHorasDAO
    {
        IDataReader dadosReader;

        public List<BancoDeHoras> Listar(int idColaborador, int idSupervisor, DateTime Inicio, DateTime Fim, int idDepartamento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<BancoDeHoras> _lista = new List<BancoDeHoras>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select To_Date(To_Char(f.Dtdigit, 'dd/mm/yyyy') || ' ' || To_Char(f.Entradigit, 'hh24:mi:ss'), 'dd/mm/yyyy hh24:mi:ss') Entrada");
                query.Append("     , To_Date(To_Char(f.Dtdigit, 'dd/mm/yyyy') || ' ' || To_Char(f.Intidigit, 'hh24:mi:ss'), 'dd/mm/yyyy hh24:mi:ss') Saidaalmoco");
                query.Append("     , To_Date(To_Char(f.Dtdigit, 'dd/mm/yyyy') || ' ' || To_Char(f.Intfdigit, 'hh24:mi:ss'), 'dd/mm/yyyy hh24:mi:ss') Voltaalmoco");
                query.Append("     , To_Date(To_Char(f.Dtdigit, 'dd/mm/yyyy') || ' ' || To_Char(f.Saidadigit, 'hh24:mi:ss'), 'dd/mm/yyyy hh24:mi:ss') Saida");
                query.Append("     , f.Dtdigit Data, c.Idcolaborador, o.nomefunc, To_Char(Dtdigit, 'D') Tipo, f.Codintfunc");
                query.Append("  From Frq_Digitacaomovimento f, Niff_Ads_Colaboradores c, flp_funcionarios o");
                query.Append(" Where f.Codintfunc = c.Codintfunc");
                query.Append("   And o.codintfunc = c.codintfunc");
                query.Append("   And c.Idcolaborador = " + idColaborador);
                query.Append("   And c.idempresa = 1");
                query.Append("   And f.dtdigit Between  To_date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') And To_date('" + Fim.ToShortDateString() + "', 'dd/mm/yyyy')"); 
                query.Append("   And Statusdigit = 'N'");
                query.Append("   And f.Codocorr In ( 121, 337, 589, 590)");                

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        BancoDeHoras _horas = new BancoDeHoras();

                        _horas.Existe = true;
                        _horas.Inicio = Inicio;
                        _horas.Fim = Fim;
                        _horas.IdColaborador = Convert.ToInt32(dadosReader["Idcolaborador"].ToString());
                        _horas.Data = Convert.ToDateTime(dadosReader["Data"].ToString());
                        _horas.NomeColaborador = dadosReader["Nomefunc"].ToString();
                        _horas.Entrada = Convert.ToDateTime(dadosReader["Entrada"].ToString());
                        _horas.Saida = Convert.ToDateTime(dadosReader["Saida"].ToString());
                        _horas.SaidaAlmoco = Convert.ToDateTime(dadosReader["Saidaalmoco"].ToString());
                        _horas.EntradaAlmoco = Convert.ToDateTime(dadosReader["Voltaalmoco"].ToString());
                        
                        _lista.Add(_horas);
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

        public List<BancoDeHoras> Listarold(int idColaborador, int idSupervisor, DateTime Inicio, DateTime Fim, int idDepartamento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<BancoDeHoras> _lista = new List<BancoDeHoras>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select fu.Nomefunc, Sum(TExtra) extra, Sum(TIncompletas) Incompletas, Max(Data) data");
                query.Append("     , Case When Sum(Textra) > Sum(TIncompletas) Then Sum(TExtra) - Sum(TIncompletas) Else 0 End Credito");
                query.Append("     , Case When Sum(Textra) > Sum(TIncompletas) Then 0 Else Sum(TIncompletas) - Sum(TExtra) End Deve");
                query.Append("     , ConverteDecimalEmHorasString( Sum(TExtra) ) ExtraF");
                query.Append("     , ConverteDecimalEmHorasString( Sum(TIncompletas) ) IncompletasF");
                query.Append("     , ConverteDecimalEmHorasString( Case When Sum(Textra) > Sum(TIncompletas) Then Sum(TExtra) - Sum(TIncompletas) Else 0 End) CreditoF");
                query.Append("     , ConverteDecimalEmHorasString( Case When Sum(Textra) > Sum(TIncompletas) Then 0 Else Sum(TIncompletas) - Sum(TExtra) End) DeveF");

                query.Append("  From Flp_Funcionarios fu, ");
                query.Append("     ( Select Data, horas, extra, Incompletas, codintfunc, ConverteDecimalEmHorasString(extra) ExtraF");
                query.Append("            , ConverteDecimalEmHorasString(Incompletas) IncompletasF");
                query.Append("            , (Case When extra > Incompletas Then extra - Incompletas Else 0 End) TExtra");
                query.Append("            , (Case When extra<Incompletas Then Incompletas - extra Else 0 End) TIncompletas");
                query.Append("        From ( Select Data, horas, tipo, codintfunc");
                query.Append("                    , Decode(dataferiado, data, 0, Case When(tipo = 1 Or tipo = 7) And horas = 0 Then 0");
                query.Append("                                                        When(tipo = 1 Or tipo = 7) And horas > 0 Then horas");
                query.Append("                                                        Else eEntrada + eSaida + eSaidaAlmoco + eVoltaAlmoco End) Extra");
                query.Append("                    , Decode(dataferiado, data, 0, Case When(tipo = 1 Or tipo = 7) And horas = 0 Then 0 ");
                query.Append("                                                        Else iEntrada + iSaida + iSaidaAlmoco + iVoltaAlmoco End) Incompletas");
                query.Append("                From ( Select Data, dataferiado, Round(QtdHr( entrada, Saida) - QtdHr( SaidaAlmoco, VoltaAlmoco),2) horas, tipo, codintfunc");
                query.Append(" /* Calculo Extra */        , Case When entrada < To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '07:55', 'dd/mm/yyyy hh24:mi')");
                query.Append("                                   Then Round(QtdHr(entrada, To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || '08:00', 'dd/mm/yyyy hh24:mi')), 2)");
                query.Append("                                   Else 0 End eEntrada");
                query.Append("                            , Case When Saida > To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:05', '18:05'), 'dd/mm/yyyy hh24:mi') ");
                query.Append("                                   Then Round(QtdHr(To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:00', '18:00'), 'dd/mm/yyyy hh24:mi'), saida), 2)");
                query.Append("                                   Else 0 End eSaida");
                query.Append("                            , Case When saidaAlmoco > To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || '12:00', 'dd/mm/yyyy hh24:mi')");
                query.Append("                                   Then Round(QtdHr(To_Date(To_char(Data,'dd/mm/yyyy') || ' ' || '12:00', 'dd/mm/yyyy hh24:mi'), saidaAlmoco ),2)");
                query.Append("                                   Else 0 End eSaidaAlmoco");
                query.Append("                            , Case When VoltaAlmoco < To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || '13:00', 'dd/mm/yyyy hh24:mi')");
                query.Append("                                   Then Round(QtdHr(VoltaAlmoco, To_Date(To_char(Data,'dd/mm/yyyy') || ' ' || '13:00', 'dd/mm/yyyy hh24:mi') ),2) ");
                query.Append("                                   Else 0 End eVoltaAlmoco");
                query.Append(" /* Calculo Incompletas */  , Case When entrada > To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '08:05', 'dd/mm/yyyy hh24:mi')");
                query.Append("                                   Then Round(QtdHr(To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || '08:00', 'dd/mm/yyyy hh24:mi'), entrada), 2)");
                query.Append("                                   Else 0 End iEntrada");
                query.Append("                            , Case When Saida < To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:00', '18:00'), 'dd/mm/yyyy hh24:mi')");
                query.Append("                                   Then Round(QtdHr(saida, To_Date(To_char(Data,'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:00', '18:00'), 'dd/mm/yyyy hh24:mi')  ),2) ");
                query.Append("                                   Else 0 End iSaida");
                query.Append("                            , Case When saidaAlmoco < To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '12:00', 'dd/mm/yyyy hh24:mi')");
                query.Append("                                   Then Round(QtdHr(saidaAlmoco, To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || '12:00', 'dd/mm/yyyy hh24:mi')), 2)");
                query.Append("                                   Else 0 End iSaidaAlmoco");
                query.Append("                            , Case When VoltaAlmoco > To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || '13:00', 'dd/mm/yyyy hh24:mi')");
                query.Append("                                   Then Round(QtdHr(To_Date(To_char(Data,'dd/mm/yyyy') || ' ' || '13:00', 'dd/mm/yyyy hh24:mi'), VoltaAlmoco ),2) ");
                query.Append("                                   Else 0 End iVoltaAlmoco");
                query.Append("                         From ( Select Min(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.entradigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) entrada");
                query.Append("                                     , Max(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.Intidigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) SaidaAlmoco");
                query.Append("                                     , Max(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.Intfdigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) VoltaAlmoco");
                query.Append("                                     , Max(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.saidadigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) Saida");
                query.Append("                                     , f.dtdigit Data, to_char(dtdigit, 'D') tipo, dataferiado, f.codintfunc");
                query.Append("                                  From frq_digitacaomovimento f, niff_ads_colaboradores c  ");
                query.Append("                                     , ( Select dataferiado From finferia_empresafilial f Where codigoempresa = 5 And codigofl = 1) ff");
                query.Append("                                 Where f.codintfunc = c.codintfunc");

                    if (!Publicas._usuario.VisualizaBancoHorasDoDepartamento)
                        query.Append("                                   And c.Idcolaborador = " + idColaborador);
                    else
                    {
                        query.Append("                                   And c.idempresa = 1");
                        query.Append("                                   And c.Iddepartamento = " + idDepartamento );
                    }

                query.Append("                                   And f.dtdigit Between  To_date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') And To_date('" + Fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("                                   And statusdigit = 'N'");
                query.Append("                                   And f.CodOcorr <> 582");
                query.Append("                                   And ff.dataferiado(+) = f.dtdigit");
                query.Append("                                 Group By f.dtdigit, dataferiado, f.codintfunc )))) x");
                query.Append(" Where fu.codintfunc = x.codintfunc");
                query.Append(" Group By fu.Nomefunc ");

                if (idSupervisor != 0)
                {
                    query.Append(" Union all ");

                    query.Append("Select fu.Nomefunc, Sum(TExtra) extra, Sum(TIncompletas) Incompletas, Max(Data) data");
                    query.Append("     , Case When Sum(Textra) > Sum(TIncompletas) Then Sum(TExtra) - Sum(TIncompletas) Else 0 End Credito");
                    query.Append("     , Case When Sum(Textra) > Sum(TIncompletas) Then 0 Else Sum(TIncompletas) - Sum(TExtra) End Deve");
                    query.Append("     , ConverteDecimalEmHorasString( Sum(TExtra) ) ExtraF");
                    query.Append("     , ConverteDecimalEmHorasString( Sum(TIncompletas) ) IncompletasF");
                    query.Append("     , ConverteDecimalEmHorasString( Case When Sum(Textra) > Sum(TIncompletas) Then Sum(TExtra) - Sum(TIncompletas) Else 0 End) CreditoF");
                    query.Append("     , ConverteDecimalEmHorasString( Case When Sum(Textra) > Sum(TIncompletas) Then 0 Else Sum(TIncompletas) - Sum(TExtra) End) DeveF");

                    query.Append("  From Flp_Funcionarios fu, ");
                    query.Append("     ( Select Data, horas, extra, Incompletas, codintfunc, ConverteDecimalEmHorasString(extra) ExtraF");
                    query.Append("            , ConverteDecimalEmHorasString(Incompletas) IncompletasF");
                    query.Append("            , (Case When extra > Incompletas Then extra - Incompletas Else 0 End) TExtra");
                    query.Append("            , (Case When extra<Incompletas Then Incompletas - extra Else 0 End) TIncompletas");
                    query.Append("        From ( Select Data, horas, tipo, codintfunc");
                    query.Append("                    , Decode(dataferiado, data, 0, Case When(tipo = 1 Or tipo = 7) And horas = 0 Then 0");
                    query.Append("                                                        When(tipo = 1 Or tipo = 7) And horas > 0 Then horas");
                    query.Append("                                                        Else eEntrada + eSaida + eSaidaAlmoco + eVoltaAlmoco End) Extra");
                    query.Append("                    , Decode(dataferiado, data, 0, Case When(tipo = 1 Or tipo = 7) And horas = 0 Then 0 ");
                    query.Append("                                                        Else iEntrada + iSaida + iSaidaAlmoco + iVoltaAlmoco End) Incompletas");
                    query.Append("                From ( Select Data, dataferiado, Round(QtdHr( entrada, Saida) - QtdHr( SaidaAlmoco, VoltaAlmoco),2) horas, tipo, codintfunc");
                    query.Append(" /* Calculo Extra */        , Case When entrada < To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '07:55', 'dd/mm/yyyy hh24:mi')");
                    query.Append("                                   Then Round(QtdHr(entrada, To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || '08:00', 'dd/mm/yyyy hh24:mi')), 2)");
                    query.Append("                                   Else 0 End eEntrada");
                    query.Append("                            , Case When Saida > To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:05', '18:05'), 'dd/mm/yyyy hh24:mi') ");
                    query.Append("                                   Then Round(QtdHr(To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:00', '18:00'), 'dd/mm/yyyy hh24:mi'), saida), 2)");
                    query.Append("                                   Else 0 End eSaida");
                    query.Append("                            , Case When saidaAlmoco > To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || '12:00', 'dd/mm/yyyy hh24:mi')");
                    query.Append("                                   Then Round(QtdHr(To_Date(To_char(Data,'dd/mm/yyyy') || ' ' || '12:00', 'dd/mm/yyyy hh24:mi'), saidaAlmoco ),2)");
                    query.Append("                                   Else 0 End eSaidaAlmoco");
                    query.Append("                            , Case When VoltaAlmoco < To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || '13:00', 'dd/mm/yyyy hh24:mi')");
                    query.Append("                                   Then Round(QtdHr(VoltaAlmoco, To_Date(To_char(Data,'dd/mm/yyyy') || ' ' || '13:00', 'dd/mm/yyyy hh24:mi') ),2) ");
                    query.Append("                                   Else 0 End eVoltaAlmoco");
                    query.Append(" /* Calculo Incompletas */  , Case When entrada > To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '08:05', 'dd/mm/yyyy hh24:mi')");
                    query.Append("                                   Then Round(QtdHr(To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || '08:00', 'dd/mm/yyyy hh24:mi'), entrada), 2)");
                    query.Append("                                   Else 0 End iEntrada");
                    query.Append("                            , Case When Saida < To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:00', '18:00'), 'dd/mm/yyyy hh24:mi')");
                    query.Append("                                   Then Round(QtdHr(saida, To_Date(To_char(Data,'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:00', '18:00'), 'dd/mm/yyyy hh24:mi')  ),2) ");
                    query.Append("                                   Else 0 End iSaida");
                    query.Append("                            , Case When saidaAlmoco < To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '12:00', 'dd/mm/yyyy hh24:mi')");
                    query.Append("                                   Then Round(QtdHr(saidaAlmoco, To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || '12:00', 'dd/mm/yyyy hh24:mi')), 2)");
                    query.Append("                                   Else 0 End iSaidaAlmoco");
                    query.Append("                            , Case When VoltaAlmoco > To_Date(To_char(Data, 'dd/mm/yyyy') || ' ' || '13:00', 'dd/mm/yyyy hh24:mi')");
                    query.Append("                                   Then Round(QtdHr(To_Date(To_char(Data,'dd/mm/yyyy') || ' ' || '13:00', 'dd/mm/yyyy hh24:mi'), VoltaAlmoco ),2) ");
                    query.Append("                                   Else 0 End iVoltaAlmoco");
                    query.Append("                         From ( Select Min(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.entradigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) entrada");
                    query.Append("                                     , Max(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.Intidigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) SaidaAlmoco");
                    query.Append("                                     , Max(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.Intfdigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) VoltaAlmoco");
                    query.Append("                                     , Max(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.saidadigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) Saida");
                    query.Append("                                     , f.dtdigit Data, to_char(dtdigit, 'D') tipo, dataferiado, f.codintfunc");
                    query.Append("                                  From frq_digitacaomovimento f, niff_ads_colaboradores c  ");
                    query.Append("                                     , ( Select dataferiado From finferia_empresafilial f Where codigoempresa = 5 And codigofl = 1) ff");

                    query.Append("                                     , (Select * ");
                    query.Append("                                          From (Select subStr(referenciainicio, 1, 2) || '/' || subStr(referenciainicio, 3, 2) || '/' ||");
                    query.Append("                                                       Case When referenciainicio = 2112 Then To_char(To_date(To_char(Sysdate,'rrrr')-1,'yyyy'),'yyyy') Else To_char(Sysdate,'rrrr') End Inicio");
                    query.Append("                                                     , subStr(referenciafim, 1, 2) || '/' || subStr(referenciafim, 3, 2) || '/' ||");
                    query.Append("                                                       Case When referenciafim = 2001 Then To_char(To_date(To_char(Sysdate,'rrrr')+1,'yyyy'),'yyyy') Else To_char(Sysdate,'rrrr') End Fim");
                    query.Append("                                                     , Idcolaborador");
                    query.Append("                                                  From Niff_Pto_ColabPeriodo) ");
                    query.Append("                                                 Where trunc(Sysdate) Between To_Date(Inicio,'dd/mm/yyyy') And to_Date(Fim,'dd/mm/yyyy') ) p");

                    query.Append("                                 Where f.codintfunc = c.codintfunc");

                    query.Append("                                   And c.Idcolaborador In (Select idcolaborador From Niff_Ads_Colaboradores s Where s.Idsuperior = " + idSupervisor + ")"); // buscar todos do supervisor
                    query.Append("                                   And p.idColaborador = c.Idcolaborador");
                    query.Append("                                   And f.dtdigit Between To_Date(p.Inicio,'dd/mm/yyyy') And to_Date(p.Fim,'dd/mm/yyyy')");
                    query.Append("                                   And statusdigit = 'N'");
                    query.Append("                                   And f.CodOcorr <> 582");
                    query.Append("                                   And ff.dataferiado(+) = f.dtdigit");
                    query.Append("                                 Group By f.dtdigit, dataferiado, f.codintfunc )))) x");
                    query.Append(" Where fu.codintfunc = x.codintfunc");
                    query.Append(" Group By fu.Nomefunc");

                }

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        BancoDeHoras _horas = new BancoDeHoras();

                        _horas.Existe = true;
                        _horas.Inicio = Inicio;
                        _horas.Fim = Fim;
                        _horas.Data = Convert.ToDateTime(dadosReader["Data"].ToString());
                        _horas.NomeColaborador = dadosReader["Nomefunc"].ToString();
                        _horas.TotalExtras = dadosReader["ExtraF"].ToString();
                        _horas.TotalIncompletas = dadosReader["IncompletasF"].ToString();
                        _horas.TotalLiquido = (dadosReader["CreditoF"].ToString() != "00:00" ? dadosReader["CreditoF"].ToString() : dadosReader["DeveF"].ToString());
                        _horas.Tipo = "Horas " + (dadosReader["CreditoF"].ToString() != "00:00" ? "Extras" : "Incompletas");

                        _lista.Add(_horas);
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
    }
}
