using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class SIGOMDAO
    {
        IDataReader dadosReader;
        IDataReader dadosReader2;

        public List<SIGOM> ListarSigon(int empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            SessaoSigom sessao = new SessaoSigom();
            List<SIGOM> _lista = new List<SIGOM>();
            List<SIGOM> _listaM = new List<SIGOM>();
            List<SIGOM> _listaF = new List<SIGOM>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select t.TIP_ID, t.TIP_DESCRICAO, Sum(rt.ret_qtdade_giros) qtGiros, sum(rt.ret_valor)/100 valor");
                query.Append("     , l.LIN_CODIFICACAO, v.VEI_PREFIXO, Min(r.rev_catraca_ini) InicioRoleta, Max(r.rev_catraca_fim) FimRoleta");
                query.Append("     , To_char(Min(r.rev_dthora_ope_ini), 'dd/mm/yyyy hh24:mi') InicioJornada");
                query.Append("     , To_char(Min(r.rev_dthora_jornada_ini), 'dd/mm/yyyy hh24:mi') InicioJornada2");
                query.Append("     , To_char(Max(r.rev_dthora_jornada_Fim), 'dd/mm/yyyy hh24:mi') FimJornada");
                query.Append("     , ope_id_operador_ini, o.OPE_NOME");
                query.Append("  From plus.rev_resumo_viagem r");
                query.Append("     , plus.view_lin_linha l");
                query.Append("     , plus.ret_resumo_viagem_tipo rt");
                query.Append("     , plus.view_tip_tipo_cartao t");
                query.Append("     , plus.view_vei_veiculo v");
                query.Append("     , plus.view_ope_operador o");
                query.Append(" Where l.LIN_ID = r.lin_id");
                query.Append("   And r.Ent_Id = " + empresa);
                query.Append("   And trunc(r.rev_dthora_jornada_ini) between To_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("   And To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("   And r.tab_id Is Not Null");
                query.Append("   And rt.rev_id = r.rev_id");
                query.Append("   And rt.tip_id = t.TIP_ID");
                query.Append("   And rt.TIP_OPE_USU = t.tip_ope_usu");
                query.Append("   And r.vei_id = v.VEI_ID");
                query.Append("   And o.OPE_ID = r.ope_id_operador_ini");
                //query.Append("   And v.Vei_Prefixo = '030552'");
                //query.Append("   And l.LIN_CODIFICACAO = '137TRO'");
                query.Append(" Group By t.TIP_ID, t.TIP_DESCRICAO");
                query.Append("     , l.LIN_CODIFICACAO, v.VEI_PREFIXO, ope_id_operador_ini, o.OPE_NOME");
                query.Append(" Union all ");
                query.Append("Select t.TIP_ID, 'INTEGRACAO' TIP_DESCRICAO, sum(rt.ret_qtde_integra) qtGiros, sum(rt.ret_valor_integra) / 100 valor");
                query.Append("     , l.LIN_CODIFICACAO , v.VEI_PREFIXO , Min(r.rev_catraca_ini) InicioRoleta, Max(r.rev_catraca_fim) FimRoleta");
                query.Append("     , To_char(Min(r.rev_dthora_ope_ini), 'dd/mm/yyyy hh24:mi') InicioJornada");
                query.Append("     , To_char(Min(r.rev_dthora_jornada_ini), 'dd/mm/yyyy hh24:mi') InicioJornada2");
                query.Append("     , To_char(Max(r.rev_dthora_jornada_Fim), 'dd/mm/yyyy hh24:mi') FimJornada");
                query.Append("     , ope_id_operador_ini, o.OPE_NOME");
                query.Append("  From plus.rev_resumo_viagem r");
                query.Append("     , plus.view_lin_linha l");
                query.Append("     , plus.ret_resumo_viagem_tipo rt");
                query.Append("     , plus.view_tip_tipo_cartao t");
                query.Append("     , plus.view_vei_veiculo v");
                query.Append("     , plus.view_ope_operador o");
                query.Append(" Where l.LIN_ID = r.lin_id");
                query.Append("   And r.Ent_Id = " + empresa);
                query.Append("   And trunc(r.rev_dthora_jornada_ini) between To_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("   And To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("   And r.tab_id Is Not Null");
                query.Append("   And rt.rev_id = r.rev_id"); 
                query.Append("   And rt.tip_id = t.TIP_ID");
                query.Append("   And rt.TIP_OPE_USU = t.tip_ope_usu");
                query.Append("   And r.vei_id = v.VEI_ID");
                query.Append("   And o.OPE_ID = r.ope_id_operador_ini");
                query.Append("   And rt.ret_qtde_integra > 0");
                //query.Append("   And v.Vei_Prefixo = '030552'");
                //query.Append("   And l.LIN_CODIFICACAO = '137TRO'");

                query.Append(" Group By t.TIP_ID, l.LIN_CODIFICACAO , v.VEI_PREFIXO, ope_id_operador_ini, o.OPE_NOME");
                query.Append(" Union all ");
                query.Append("Select 0 TIP_ID, 'PAGANTES' TIP_DESCRICAO, Sum(r.rev_total_botoeira) qtGiros, Sum(r.rev_total_botoeira * r.rev_valor_tarifa) valor");
                query.Append("     , l.LIN_CODIFICACAO , v.VEI_PREFIXO , Min(r.rev_catraca_ini) InicioRoleta, Max(r.rev_catraca_fim) FimRoleta");
                query.Append("     , To_char(Min(r.rev_dthora_ope_ini), 'dd/mm/yyyy hh24:mi') InicioJornada");
                query.Append("     , To_char(Min(r.rev_dthora_jornada_ini), 'dd/mm/yyyy hh24:mi') InicioJornada2");
                query.Append("     , To_char(Max(r.rev_dthora_jornada_Fim), 'dd/mm/yyyy hh24:mi') FimJornada");
                query.Append("     , ope_id_operador_ini, o.OPE_NOME");
                query.Append("  From plus.rev_resumo_viagem r");
                query.Append("     , plus.view_lin_linha l");
                query.Append("     , plus.view_vei_veiculo v");
                query.Append("     , plus.view_ope_operador o");
                query.Append(" Where l.LIN_ID = r.lin_id");
                query.Append("    And r.Ent_Id = " + empresa);
                query.Append("   And trunc(r.rev_dthora_jornada_ini) between To_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("   And To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("   And r.tab_id Is Not Null");
                query.Append("   And r.vei_id = v.VEI_ID");

                //query.Append("   And v.Vei_Prefixo = '030552'");
                //query.Append("   And l.LIN_CODIFICACAO = '137TRO'");
                query.Append("   And o.OPE_ID = r.ope_id_operador_ini");
                query.Append(" Group By  l.LIN_CODIFICACAO , v.VEI_PREFIXO, ope_id_operador_ini, o.OPE_NOME");
                
                QuerySigom executar = sessao.CreateQuery(query.ToString());

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
                        _indi.CatracaInicial = Convert.ToDecimal(dadosReader["InicioRoleta"].ToString());
                        _indi.CatracaFinal = Convert.ToDecimal(dadosReader["FimRoleta"].ToString());

                        _indi.InicioJornada = Convert.ToDateTime(dadosReader["InicioJornada"].ToString());
                        _indi.InicioJornadaSigom = Convert.ToDateTime(dadosReader["InicioJornada2"].ToString());
                        _indi.FimJornadaSigom = Convert.ToDateTime(dadosReader["FimJornada"].ToString());
                        _indi.MotoristaSigom = dadosReader["OPE_NOME"].ToString();

                        if (_indi.QuantidadeGirosSigom != 0 || _indi.ValorSigom != 0)
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
                query.Append("     , t.cod_tipopagto_imp, t.nom_descricao, Sum(d.vlr_receb) valor");
                query.Append("  From t_arr_guia g");
                query.Append("     , t_arr_detalhe_guia d");
                query.Append("     , t_arr_viagens_guia v");
                query.Append("     , Frt_Cadveiculos cv");
                query.Append("     , bgm_cadlinhas l");
                query.Append("     , t_trf_tipopagto t");
                query.Append("     , t_trf_parametros_linha p");
                query.Append(" Where lPad(g.Cod_empresa,3,'0') || '/' || lPad(g.Codigofl,3,'0') = '" + empresa + "'");
                query.Append("   And g.cod_seq_guia = d.cod_seq_guia");
                query.Append("   And d.codintlinha = l.codintlinha");
                query.Append("   And d.cod_seq_viagem = v.cod_seq_viagem");
                query.Append("   And v.cod_seq_guia = g.cod_seq_guia");
                query.Append("   And cv.codigoveic = v.cod_veiculo");
                query.Append("   And d.cod_tipopagtarifa = t.cod_tipopagto");
                query.Append("   And p.codintlinha = l.codintlinha");
                query.Append("   And p.flg_linha_disponivel = 'S'");

                //query.Append("   And cv.prefixoveic = '0030552'");
                //query.Append("   And l.codigolinha = '137TRO'");
                query.Append("   And g.dat_viagem_guia between To_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("   And To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append(" Group By l.codigolinha, cv.prefixoveic, d.cod_tipopagtarifa, d.qtd_passag_trans, v.qtd_hora_ini ");
                query.Append("     , v.qtd_hora_final, g.cod_guia, t.cod_tipopagto_imp, t.nom_descricao, g.cod_seq_guia, v.cod_seq_viagem");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        SIGOM _indi = new SIGOM();
                        _indi.IdTipoPagtoGlobus = dadosReader["cod_tipopagtarifa"].ToString();
                        _indi.CodigoImportacaoGlobus = Convert.ToInt32(dadosReader["cod_tipopagto_imp"].ToString());

                        _indi.TipoPagtoGlobus = dadosReader["nom_descricao"].ToString();
                        _indi.QuantidadeGirosGlobus = Convert.ToInt32(dadosReader["qtd_passag_trans"].ToString());
                        _indi.ValorGlobus = Convert.ToDecimal(dadosReader["valor"].ToString());
                        _indi.Prefixo = dadosReader["prefixoveic"].ToString();
                        _indi.CodigoLinha = dadosReader["codigolinha"].ToString();
                        _indi.GuiaGlobus = dadosReader["cod_guia"].ToString();

                        //_indi.CatracaInicial = Convert.ToDecimal(dadosReader["InicioRoleta"].ToString());
                        //_indi.CatracaFinal = Convert.ToDecimal(dadosReader["FimRoleta"].ToString());

                        _indi.InicioJornada = Convert.ToDateTime(dadosReader["InicioJornada"].ToString());
                        _indi.FimJornadaGlobus = Convert.ToDateTime(dadosReader["FimJornada"].ToString());
                        _indi.MotoristaGlobus = BuscarFuncionario(Convert.ToInt32(dadosReader["cod_seq_guia"].ToString()), Convert.ToInt32(dadosReader["cod_seq_viagem"].ToString()));

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

        public String BuscarFuncionario(decimal guia, decimal viagem)
        {
            StringBuilder query = new StringBuilder();
            Publicas.stringConexao = Publicas._conexaoString;
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            string nomeCob = "";
            string nomeMot = "";
            try
            {
                query.Clear();
                query.Append("Select f.Nomefunc, tf.flg_mot_cob");
                query.Append("  From T_ARR_TROCAS_FUNC tf, Flp_Funcionarios f");
                query.Append(" Where tf.cod_seq_guia = " + guia);
                query.Append("   And tf.cod_seq_viagem = " + viagem);
                query.Append("   And tf.codintfunc = f.codintfunc");
                query.Append(" Order by flg_mot_cob");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader2 = executar.ExecuteQuery();

                using (dadosReader2)
                {                    
                    while (dadosReader2.Read())
                    {
                        if (dadosReader2["flg_mot_cob"].ToString() == "C")
                            nomeCob = dadosReader2["Nomefunc"].ToString();
                        else
                            nomeMot = dadosReader2["Nomefunc"].ToString();
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
            return (nomeCob != "" ? nomeCob : nomeMot);
        }

        public bool Gravar(List<SIGOM> lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                foreach (var item in lista)
                {
                    query.Clear();
                    query.Append("Insert into Niff_Rec_SIGOM");
                    query.Append("( id, ");
                    query.Append("codigolinha, ");
                    query.Append("prefixo, ");
                    query.Append("iniciojornadaglobus, ");
                    query.Append("iniciojornadasigom, ");
                    query.Append("fimjornadaglobus, ");
                    query.Append("fimjornadasigom, ");
                    query.Append("responsavelglobus, ");
                    query.Append("responsavelsigom, ");
                    query.Append("idtipopagtoglobus, ");
                    query.Append("idtipopagtosigom, ");
                    query.Append("tipopagtoglobus, ");
                    query.Append("tipopagtosigom, ");
                    query.Append("quantidadeglobus, ");
                    query.Append("quantidadesigom, ");
                    query.Append("valorglobus, ");
                    query.Append("valorsigom, ");
                    query.Append("guiaglobus, ");
                    query.Append("codigoimportacaoglobus, ");
                    query.Append("diferencaquantidade, ");
                    query.Append("diferencavalor, IdEmpresa, Tipo )");
                    query.Append(" values ((select Nvl(Max(id),0)+1 next From Niff_Rec_SIGOM ) ");
                    query.Append("   , '" + item.CodigoLinha + "'");
                    query.Append("   , '" + item.Prefixo + "'");
                    query.Append("   , to_date('" + item.InicioJornada.ToShortDateString() + " " + item.InicioJornada.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                    query.Append("   , to_date('" + item.InicioJornadaSigom.ToShortDateString() + " " + item.InicioJornadaSigom.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                    query.Append("   , to_date('" + item.FimJornadaGlobus.ToShortDateString() + " " + item.FimJornadaGlobus.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                    query.Append("   , to_date('" + item.FimJornadaSigom.ToShortDateString() + " " + item.FimJornadaSigom.ToShortTimeString() + "', 'dd/mm/yyyy hh24:mi')");
                    query.Append("   , '" + item.MotoristaGlobus + "'");
                    query.Append("   , '" + item.MotoristaSigom + "'");
                    query.Append("   , '" + item.IdTipoPagtoGlobus + "'");
                    query.Append("   , '" + item.IdTipoPagtoSigom + "'");
                    query.Append("   , '" + item.TipoPagtoGlobus + "'");
                    query.Append("   , '" + item.TipoPagtoSigom + "'");
                    query.Append("   , '" + item.QuantidadeGirosGlobus + "'");
                    query.Append("   , '" + item.QuantidadeGirosSigom + "'");
                    query.Append("   , " + item.ValorGlobus.ToString().Replace(".", "").Replace(",", ".") );
                    query.Append("   , " + item.ValorSigom.ToString().Replace(".", "").Replace(",", ".") );
                    query.Append("   , '" + item.GuiaGlobus + "'");
                    query.Append("   , '" + item.CodigoImportacaoGlobus + "'");
                    query.Append("   , '" + (item.DiferencaQuantidade ? "S" : "N") + "'");
                    query.Append("   , '" + (item.DiferencaValor ? "S" : "N") + "'");
                    query.Append("   , " + item.IdEmpresa );
                    query.Append("   , '" + item.Tipo + "'");
                    query.Append("   ) ");

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

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
            return retorno;
        }

        public List<SIGOM> ListarResumo(int empresa, DateTime inicio, DateTime fim, string tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<SIGOM> _lista = new List<SIGOM>();

            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, codigolinha, prefixo, iniciojornadaglobus, iniciojornadasigom, fimjornadaglobus, fimjornadasigom");
                query.Append("     , responsavelglobus, responsavelsigom, idtipopagtoglobus, idtipopagtosigom, tipopagtoglobus, tipopagtosigom");
                query.Append("     , quantidadeglobus, quantidadesigom, valorglobus, valorsigom, guiaglobus, codigoimportacaoglobus, diferencaquantidade");
                query.Append("     , diferencavalor, idEmpresa");
                query.Append("  From niff_rec_sigom ");
                query.Append(" Where idEmpresa = " + empresa);
                query.Append("   and Tipo = '" + tipo + "'");
                query.Append("   and ((Trunc(iniciojornadaglobus) between To_date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and ");
                query.Append("                                           To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy'))  ");
                query.Append("    or (Trunc(iniciojornadasigom) between To_date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and ");
                query.Append("                                           To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy') )) ");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        SIGOM _indi = new SIGOM();
                        _indi.IdTipoPagtoGlobus = dadosReader["idtipopagtoglobus"].ToString();
                        _indi.IdTipoPagtoSigom = Convert.ToInt32(dadosReader["idtipopagtosigom"].ToString());
                        _indi.CodigoImportacaoGlobus = Convert.ToInt32(dadosReader["codigoimportacaoglobus"].ToString());

                        _indi.TipoPagtoGlobus = dadosReader["tipopagtoglobus"].ToString();
                        _indi.TipoPagtoSigom = dadosReader["tipopagtosigom"].ToString();

                        _indi.QuantidadeGirosGlobus = Convert.ToInt32(dadosReader["quantidadeglobus"].ToString());
                        _indi.QuantidadeGirosSigom = Convert.ToInt32(dadosReader["quantidadesigom"].ToString());

                        _indi.ValorGlobus = Convert.ToDecimal(dadosReader["valorglobus"].ToString());
                        _indi.ValorSigom = Convert.ToDecimal(dadosReader["valorsigom"].ToString());

                        _indi.Prefixo = dadosReader["prefixo"].ToString();
                        _indi.CodigoLinha = dadosReader["codigolinha"].ToString();
                        _indi.GuiaGlobus = dadosReader["guiaglobus"].ToString();

                        _indi.InicioJornada = Convert.ToDateTime(dadosReader["iniciojornadaglobus"].ToString());
                        _indi.FimJornadaGlobus = Convert.ToDateTime(dadosReader["fimjornadaglobus"].ToString());
                        _indi.InicioJornadaSigom = Convert.ToDateTime(dadosReader["iniciojornadasigom"].ToString());
                        _indi.FimJornadaSigom = Convert.ToDateTime(dadosReader["fimjornadasigom"].ToString());

                        _indi.MotoristaGlobus = dadosReader["responsavelglobus"].ToString();
                        _indi.MotoristaSigom = dadosReader["responsavelsigom"].ToString();
                        _indi.IdEmpresa = Convert.ToInt32(dadosReader["idEmpresa"].ToString());

                        _indi.DiferencaQuantidade = dadosReader["diferencaquantidade"].ToString() == "S";
                        _indi.DiferencaValor = dadosReader["diferencavalor"].ToString() == "S";
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
