using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class RadarBIDAO
    {
        IDataReader radarReader;

        public List<RadarBI> Listar (string empresa, string rubrica)
        {
            List<RadarBI> _lista = new List<RadarBI>();

            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select grupo, empfil, data, valor, percentual, ordem, dataalterado, ");
                query.Append("       valoranterior, percentualanterior, tipo ");
                query.Append("  From pbi_radar_operacional ");
                query.Append(" Where EmpFil = '" + empresa + "'");
                query.Append("   and grupo = '" + rubrica + "'");
                Query executar = sessao.CreateQuery(query.ToString());

                radarReader = executar.ExecuteQuery();

                using (radarReader)
                {
                    while (radarReader.Read())
                    {
                        RadarBI _radar = new RadarBI();

                        _radar.Grupo = radarReader["Grupo"].ToString();
                        _radar.EmpresaFilial = radarReader["empfil"].ToString();
                        _radar.Data = Convert.ToDateTime( radarReader["Data"].ToString());

                        _radar.Valor = Convert.ToDecimal(radarReader["valor"].ToString());
                        _radar.Percentual = Convert.ToDecimal(radarReader["percentual"].ToString());

                        _radar.ValorAnterior = Convert.ToDecimal(radarReader["valor"].ToString());
                        _radar.PercentualAnterior = Convert.ToDecimal(radarReader["percentual"].ToString());

                        _lista.Add(_radar);
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

        public RadarBI Consultar (string empresa, string rubrica, DateTime data)
        {
            RadarBI _radar = new RadarBI();

            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select grupo, empfil, data, valor, percentual, ordem, dataalterado, ");
                query.Append("       valoranterior, percentualanterior, tipo ");
                query.Append("  From pbi_radar_operacional ");
                query.Append(" Where EmpFil = '" + empresa + "'");
                query.Append("   and grupo = '" + rubrica + "'");
                query.Append("   and data = to_date(' " + data.ToShortDateString() + "','dd/mm/yyyy')");
                Query executar = sessao.CreateQuery(query.ToString());

                radarReader = executar.ExecuteQuery();

                using (radarReader)
                {
                    if (radarReader.Read())
                    {
                        
                        _radar.Grupo = radarReader["Grupo"].ToString();
                        _radar.EmpresaFilial = radarReader["empfil"].ToString();
                        _radar.Data = Convert.ToDateTime(radarReader["Data"].ToString());

                        _radar.Valor = Convert.ToDecimal(radarReader["valor"].ToString());
                        _radar.Percentual = Convert.ToDecimal(radarReader["percentual"].ToString());

                        _radar.ValorAnterior = Convert.ToDecimal(radarReader["valor"].ToString());
                        _radar.PercentualAnterior = Convert.ToDecimal(radarReader["percentual"].ToString());
                        _radar.Existe = true;
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
            return _radar;
        }

        public bool Gravar(RadarBI radar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();

            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!radar.Existe)
                {
                    query.Clear();
                    query.Append("Insert into pbi_radar_operacional ");
                    query.Append("(grupo, empfil, data, valor, percentual, ordem, dataalterado, tipo) ");
                    query.Append(" values ('" + radar.Grupo + "' ");
                    query.Append(" , '" + radar.EmpresaFilial + "'");
                    query.Append(" , To_date('" + radar.Data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append(" , " + radar.Valor.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" , " + radar.Percentual.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" , " + radar.Ordem);
                    query.Append(" , sysdate ");
                    query.Append(" , '" + radar.Tipo + "' )");
                }
                else
                {
                    query.Clear();
                    query.Append("Update pbi_radar_operacional");
                    query.Append("   set Valor = " + radar.Valor.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Percentual = " + radar.Percentual.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , VALORANTERIOR = " + radar.ValorAnterior.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , PERCENTUALANTERIOR = " + radar.PercentualAnterior.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , dataalterado = sysdate ");
                    query.Append(" Where EmpFil = '" + radar.EmpresaFilial + "'");
                    query.Append("   and grupo = '" + radar.Grupo + "'");
                    query.Append("   and data = to_date(' " + radar.Data.ToShortDateString() + "','dd/mm/yyyy')");
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

        public bool Excluir(string empresa, string rubrica, DateTime data)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();

            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Delete pbi_radar_operacional");
                query.Append(" Where EmpFil = '" + empresa + "'");
                query.Append("   and grupo = '" + rubrica + "'");
                query.Append("   and data = to_date(' " + data.ToShortDateString() + "','dd/mm/yyyy')");
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
