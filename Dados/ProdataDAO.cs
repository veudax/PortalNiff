using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ProdataDAO
    {
        IDataReader dadosReader;
        

        public List<SIGOM> ListarGlobus(string empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Publicas.stringConexao = Publicas._conexaoString;
            Sessao sessao = new Sessao();
            List<SIGOM> _lista = new List<SIGOM>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select l.codigolinha, cv.prefixoveic, d.cod_tipopagtarifa, d.qtd_passag_trans");
                query.Append("     , To_char(Min(v.qtd_hora_ini),'dd/mm/yyyy hh24:mi') InicioJornada");
                query.Append("     , To_char(Max(v.qtd_hora_final),'dd/mm/yyyy hh24:mi') FimJornada  ");
                query.Append("     , g.cod_guia, g.cod_seq_guia, v.cod_seq_viagem");
                query.Append("     , t.nom_descricao, Sum(d.vlr_receb) valor");

                query.Append("     , (Select Distinct tp.vlr_tarifa");
                query.Append("          From t_Trf_Tarifa_Tipopagto Tp,");
                query.Append("               Bgm_Cadlinhas          l,");
                query.Append("               t_Trf_Linha_Secao      Ls,");
                query.Append("               t_Trf_Secao            s");
                query.Append("         Where Tp.Cod_Tipopagtarifa = d.cod_tipopagtarifa");
                query.Append("           And s.Cod_Tarifa = Tp.Cod_Tarifa");
                query.Append("           And s.Cod_Seq_Secao = Ls.Cod_Seq_Secao");
                query.Append("           And Ls.Flg_Disponivel = 'S'");
                query.Append("           And Ls.Cod_Linha = l.Codintlinha");
                query.Append("           And l.codintlinha = d.codintlinha");
                query.Append("           And ls.flg_secao_default = 'S'");
                query.Append("           And l.Codigoempresa = 2");
                query.Append("           And (Tp.Dat_Iniciovigencia = (Select Max(Dat_Iniciovigencia)");
                query.Append("                                           From t_Trf_Tarifa_Tipopagto");
                query.Append("                                          Where Cod_Tipopagtarifa = d.cod_tipopagtarifa");
                query.Append("                                            And Dat_Iniciovigencia <= g.dat_viagem_guia)");
                query.Append("            Or tp.dat_finalvigencia Is Null)) Tarifa");

                query.Append("  From t_arr_guia g");
                query.Append("     , t_arr_detalhe_guia d");
                query.Append("     , t_arr_viagens_guia v");
                query.Append("     , Frt_Cadveiculos cv");
                query.Append("     , bgm_cadlinhas l");
                query.Append("     , t_trf_tipopagto t");
                query.Append(" Where lPad(g.Cod_empresa,3,'0') || '/' || lPad(g.Codigofl,3,'0') = '" + empresa + "'");
                query.Append("   And g.cod_seq_guia = d.cod_seq_guia");
                query.Append("   And d.codintlinha = l.codintlinha");
                query.Append("   And d.cod_seq_viagem = v.cod_seq_viagem");
                query.Append("   And v.cod_seq_guia = g.cod_seq_guia");
                query.Append("   And cv.codigoveic = v.cod_veiculo");
                query.Append("   And d.cod_tipopagtarifa = t.cod_tipopagto");
                //query.Append("   And g.cod_seq_guia = 11828601");
                //query.Append("   And cv.prefixoveic = '0001039'");
                //query.Append("   And l.codigolinha = '25'");
                query.Append("   And g.dat_viagem_guia between To_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("   And To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append(" Group By l.codigolinha, cv.prefixoveic, d.cod_tipopagtarifa, d.qtd_passag_trans, v.qtd_hora_ini ");
                query.Append("     , v.qtd_hora_final, g.cod_guia, t.nom_descricao, g.cod_seq_guia, v.cod_seq_viagem");
                query.Append("     , d.codintlinha, g.dat_viagem_guia");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        SIGOM _indi = new SIGOM();
                        _indi.IdTipoPagtoGlobus = dadosReader["cod_tipopagtarifa"].ToString();

                        _indi.TipoPagtoGlobus = dadosReader["nom_descricao"].ToString();
                        _indi.QuantidadeGirosGlobus = Convert.ToInt32(dadosReader["qtd_passag_trans"].ToString());
                        _indi.ValorGlobus = Convert.ToDecimal(dadosReader["valor"].ToString());
                        _indi.Prefixo = dadosReader["prefixoveic"].ToString();
                        _indi.CodigoLinha = dadosReader["codigolinha"].ToString();
                        _indi.GuiaGlobus = dadosReader["cod_guia"].ToString();

                        _indi.ValorTarifa = Convert.ToDecimal(dadosReader["tarifa"].ToString());
                        _indi.InicioJornada = Convert.ToDateTime(dadosReader["InicioJornada"].ToString());
                        _indi.FimJornadaGlobus = Convert.ToDateTime(dadosReader["FimJornada"].ToString());
                        _indi.MotoristaGlobus = new SIGOMDAO().BuscarFuncionario(Convert.ToInt32(dadosReader["cod_seq_guia"].ToString()), Convert.ToInt32(dadosReader["cod_seq_viagem"].ToString()));

                        _lista.Add(_indi);
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

        public List<SIGOM> ListarProdata(int empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            SessaoProdata sessao = new SessaoProdata();
            List<SIGOM> _lista = new List<SIGOM>();
            List<SIGOM> _listaM = new List<SIGOM>();
            List<SIGOM> _listaF = new List<SIGOM>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select decode(CTDT.Ctd_SfId, 3, 3, 12, 3, CTDT.Ctd_SfId) TIP_ID");
                query.Append("     , decode(CTDT.Ctd_SfId, 3, 'DEFICIENTE', 12, 'DEFICIENTE', SF.Sf_Desc) TIP_DESCRICAO");
                query.Append("     , Sum(Nvl(CTDT.Ctd_Qty,0)) qtGiros");
                query.Append("     , (Nvl(Sum(CTDT.Ctd_Qty),0) * Nvl(Sum(CTDT.Ctd_Value),0)) valor");
                query.Append("     , Nvl(CTDT.Ctd_Value, 0) tarifa");
                query.Append("     , LD.Ld_DescShort LIN_CODIFICACAO, CTMT.Ctm_Vehid VEI_PREFIXO");
                query.Append("     , To_char(Min(CTMT.Ctm_StaDate), 'dd/mm/yyyy hh24:mi') InicioJornada");
                query.Append("     , To_char(Max(CTMT.Ctm_EndDate), 'dd/mm/yyyy hh24:mi') FimJornada");
                //query.Append("     , CTDT.Ctd_SeqNBr");
                query.Append("  From Collects COL");
                query.Append("     , CollectTranMt CTMT");
                query.Append("     , CollectTranDt CTDT");
                query.Append("     , LineDetails LD");
                query.Append("     , TransportProviders TP");
                query.Append("     , StatisticalFamilies SF");
                query.Append(" Where Col.Tp_ID = CTMT.Tp_ID");
                query.Append("   And Col.Dp_Id = CTMT.Dp_Id" );
                query.Append("   And trunc(CTMT.Ctm_StaDate) between To_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("   And To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("   And Col.Cde_Id = CTMT.Cde_Id");
                query.Append("   And Col.Cse_Id = CTMT.Cse_Id");
                query.Append("   And Col.Col_Id = CTMT.Col_Id");
                query.Append("   And CTMT.Tp_ID = CTDT.Tp_ID(+)");
                query.Append("   And CTMT.Dp_Id = CTDT.Dp_Id(+)");
                query.Append("   And CTMT.Cde_Id = CTDT.Cde_Id(+)");
                query.Append("   And CTMT.Cse_Id = CTDT.Cse_Id(+) ");
                query.Append("   And CTMT.Col_Id = CTDT.Col_Id(+) ");
                query.Append("   And CTMT.Ctm_SrvId = CTDT.Ctm_SrvId(+) ");
                query.Append("   And LD.Ld_Id = CTMT.Ctm_LmId ");
                query.Append("   And TP.Tp_Id = CTMT.Tp_Id ");
                query.Append("   And CTDT.Ctd_SfId = SF.Sf_Id  ");

                //query.Append("   And CTMT.Ctm_Vehid = '1039' And Ld_DescShort like 'L25%'");

                //query.Append("   And /*col.col_id = 78 And*/ Ld_DescShort Like 'L01%'");

                query.Append(" Group By decode(CTDT.Ctd_SfId, 3, 3, 12, 3, CTDT.Ctd_SfId), decode(CTDT.Ctd_SfId, 3, 'DEFICIENTE', 12, 'DEFICIENTE', SF.Sf_Desc)");
                query.Append("     , LD.Ld_DescShort, CTMT.Ctm_Vehid, CTDT.Ctd_Value, CTMT.Ctm_StaDate, CTMT.Ctm_EndDate");
                query.Append(" Order by LIN_CODIFICACAO, VEI_PREFIXO, InicioJornada, TIP_ID, tarifa");

                QueryProdata executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        SIGOM _indi = new SIGOM();
                        _indi.IdTipoPagtoSigom = Convert.ToInt32(dadosReader["TIP_ID"].ToString());

                        _indi.TipoPagtoSigom = dadosReader["TIP_DESCRICAO"].ToString();
                        _indi.QuantidadeGirosSigom = Convert.ToInt32(dadosReader["qtGiros"].ToString());
                        _indi.ValorSigom = Convert.ToDecimal(dadosReader["valor"].ToString());
                        _indi.Prefixo = dadosReader["VEI_PREFIXO"].ToString();
                        _indi.Prefixo = _indi.Prefixo.PadLeft(7, '0');
                        _indi.CodigoLinha = dadosReader["LIN_CODIFICACAO"].ToString();

                        _indi.ValorTarifa = Convert.ToDecimal(dadosReader["tarifa"].ToString());
                        _indi.InicioJornada = Convert.ToDateTime(dadosReader["InicioJornada"].ToString());
                        _indi.InicioJornadaSigom = _indi.InicioJornada;
                        _indi.FimJornadaSigom = Convert.ToDateTime(dadosReader["FimJornada"].ToString());
                        //_indi.MotoristaSigom = dadosReader["OPE_NOME"].ToString();

                        _lista.Add(_indi);
                    }
                }

                foreach (var item in _lista.GroupBy(g => new { g.CodigoLinha, g.Prefixo, g.FimJornadaSigom })
                                           .Select(s => new { s.Key.CodigoLinha, s.Key.Prefixo, s.Key.FimJornadaSigom }))
                {
                    _listaM = _lista.Where(w => w.FimJornadaSigom == item.FimJornadaSigom &&
                                                            w.CodigoLinha == item.CodigoLinha &&
                                                            w.Prefixo == item.Prefixo).ToList();
                    foreach (var itemG in _listaM)
                    {
                        DateTime _data = _listaM.Min(m => m.InicioJornada);
                        itemG.InicioJornada = _data;
                    }

                    _listaF.AddRange(_listaM);
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
            return _listaF;
        }

        public List<SIGOM> ListarTipos(string empresa)
        {
            StringBuilder query = new StringBuilder();
            Publicas.stringConexao = Publicas._conexaoString;
            Sessao sessao = new Sessao();
            List<SIGOM> _lista = new List<SIGOM>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select Distinct P.CODIGO_GLOBUS, eliminanaonumericos(P.CODIGO_SPARK) CODIGO_SPARK");
                query.Append("  From t_Arr_Param_Prdt P");
                query.Append(" Where lPad(P.Cod_empresa,3,'0') || '/' || lPad(P.Codigofl, 3, '0') = '" + empresa + "'");
                query.Append("   And P.Tipo In('T','M')");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        SIGOM _indi = new SIGOM();
                        _indi.IdTipoPagtoGlobus = dadosReader["CODIGO_GLOBUS"].ToString();
                        _indi.TipoPagtoGlobus = dadosReader["CODIGO_SPARK"].ToString();

                        _lista.Add(_indi);
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
