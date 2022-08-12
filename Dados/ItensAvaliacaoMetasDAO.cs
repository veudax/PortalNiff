using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ItensAvaliacaoMetasDAO
    {
        IDataReader dataReader;

        public List<ItensAvaliacaoMetas> Listar(int idCargo, int idColaborador, string referencia, string referenciaFim, int idEmpresa, bool naoCadastrado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ItensAvaliacaoMetas> _lista = new List<ItensAvaliacaoMetas>();
            Publicas.mensagemDeErro = string.Empty;
            string _formula = "";

            try
            {
                if (naoCadastrado)
                {
                    query.Append("Select m.Idmetas, m.descricao, Nvl(mc.Peso,0) peso, 0 iditensauto, 0 idautoavaliacao, " + referencia + " referencia");
                    query.Append("     ,  v.previsto ValorEsperado, v.realizado Realizado, 0 Eficiencia, 0 Resultado, m.perspectiva, m.Formula");
                    query.Append("     ,  Decode(m.Perspectiva, 'F', 1, 'C', 2, 'P', 3, 4) OrdemPerspectiva");
                    query.Append("     ,  v.DataCorteFinanceiro, v.DataCorteOperacional");
                    query.Append("  from Niff_Ads_Metas m, Niff_Ads_Metasdocargo mc, Niff_Ads_Valoresmetas v");
                    query.Append(" Where mc.idcargo = " + idCargo);
                    query.Append("   And m.ativo = 'S'");
                    query.Append("   And m.tipo = 'R'");
                    query.Append("   and m.Idmetas = mc.Idmetas");
                    query.Append("   And m.Idmetas = v.Idmetas");
                    query.Append("   And v.referencia = " + referencia.Substring(2, 4) + referencia.Substring(0, 2));
                    query.Append("   And v.IdEmpresa = " + idEmpresa);

                    query.Append("   And m.Idmetas Not In (Select IdMetas ");
                    query.Append("                           From Niff_Ads_Itensavaliacaometas am, Niff_Ads_Avaliacao a, Niff_Ads_Colaboradores cl");
                    query.Append("                          Where am.idautoavaliacao = a.idautoavaliacao");
                    query.Append("                            And a.idcolaborador = cl.idcolaborador");
                    query.Append("                            And cl.Idcolaborador = " + idColaborador);
                    query.Append("                            And a.idEmpresa = " + idEmpresa);
                    query.Append("                            And a.tipo = 'MN'");

                    if (!string.IsNullOrEmpty(referenciaFim))
                        query.Append("                            And a.mesReferencia = " + referenciaFim);
                    else
                        query.Append("                            And a.mesReferencia = " + referencia);

                    query.Append("                            And cl.Idcargo = " + idCargo + ")");
                }

                //query.Append(" Union All ");
                else
                {
                    query.Append("Select m.Idmetas, m.descricao, am.peso, am.iditensauto, am.idautoavaliacao, a.MESREFERENCIA referencia");
                    query.Append("     ,  am.ValorEsperado, am.Realizado, am.Eficiencia, am.Resultado, m.perspectiva, m.Formula");
                    query.Append("     ,  Decode(m.Perspectiva, 'F', 1, 'C', 2, 'P', 3, 4) OrdemPerspectiva");
                    query.Append("     ,  v.DataCorteFinanceiro, v.DataCorteOperacional");
                    query.Append("  From Niff_Ads_Avaliacao a, niff_ads_itensavaliacaometas am, Niff_Ads_Metas m");
                    query.Append("     , Niff_Ads_Colaboradores c, Niff_Ads_Valoresmetas v");
                    query.Append(" Where a.idautoavaliacao = am.idautoavaliacao ");
                    query.Append("   And am.Idmetas = m.Idmetas");
                    query.Append("   And c.Idcolaborador = a.Idcolaborador");
                    query.Append("   And a.idcolaborador = " + idColaborador);
                    query.Append("   And a.idEmpresa = " + idEmpresa);
                    query.Append("   and a.tipo = 'MN'");

                    query.Append("   And v.idmetas = m.Idmetas");
                    query.Append("   And v.Idempresa = a.idempresa");
                    query.Append("   And v.referencia = '" + referencia.ToString().Substring(2,4) + referencia.ToString().Substring(0, 2) + "'");

                    if (string.IsNullOrEmpty(referenciaFim))
                        query.Append("   And a.mesReferencia = " + referencia);
                    else
                        query.Append("   And Substr(lPad(a.Mesreferencia,6,'0'),3,4) || Substr(lPad(a.Mesreferencia,6,'0'),1,2) between " + referencia.Substring(2, 4) + referencia.Substring(0, 2) + " and " + referenciaFim.Substring(2, 4) + referenciaFim.Substring(0, 2));
                }
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ItensAvaliacaoMetas _tipo = new ItensAvaliacaoMetas();

                        _tipo.Existe = true;
                        _tipo.IdMetas = Convert.ToInt32(dataReader["Idmetas"].ToString());
                        _tipo.Descricao = dataReader["descricao"].ToString();
                        _tipo.Peso = Convert.ToDecimal(dataReader["Peso"].ToString());
                        _tipo.Id = Convert.ToInt32(dataReader["iditensauto"].ToString());
                        _tipo.Referencia = dataReader["referencia"].ToString();
                        _tipo.Perspectiva = (dataReader["perspectiva"].ToString() == "C" ? Publicas.Perspectivas.Cliente :
                                            (dataReader["perspectiva"].ToString() == "P" ? Publicas.Perspectivas.Processos :
                                            (dataReader["perspectiva"].ToString() == "F" ? Publicas.Perspectivas.Financeira : Publicas.Perspectivas.Aprendizagem)));

                        _tipo.DescricaoPerspectiva = dataReader["OrdemPerspectiva"].ToString() + " " + Publicas.GetDescription(_tipo.Perspectiva, "");
                        _tipo.Formula = dataReader["Formula"].ToString();
                        _tipo.OrdemPerpectiva = Convert.ToInt32( dataReader["OrdemPerspectiva"].ToString() );

                        _formula = _tipo.Formula;

                        try
                        {
                            _tipo.ValorEsperado = Convert.ToDecimal(dataReader["ValorEsperado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        }
                        catch { }

                        //try
                        //{ 
                        //    _tipo.Eficiencia = Convert.ToDecimal(dataReader["Eficiencia"].ToString());                                
                        //}
                        //catch { }

                        try
                        {
                            _tipo.DataCorteFinanceiro = Convert.ToDateTime(dataReader["DataCorteFinanceiro"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataCorteOperacional = Convert.ToDateTime(dataReader["DataCorteOperacional"].ToString());
                        }
                        catch { }
                        _lista.Add(_tipo);
                    }
                }

                Pontuacao _pontuacao = new PontuacaoDAO().ConsultarMaiorReferencia(Convert.ToInt32(referencia));

                foreach (var item in _lista)
                {
                    _formula = item.Formula;
                    if ((item.DataCorteFinanceiro != DateTime.MinValue && item.Perspectiva == Publicas.Perspectivas.Financeira) ||
                        (item.DataCorteOperacional != DateTime.MinValue && item.Perspectiva != Publicas.Perspectivas.Financeira))
                    {

                        try
                        {
                            if (item.ValorEsperado != 0)
                                item.Eficiencia = Math.Round(CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                         .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                            else
                            {
                                if (_formula.Contains("/ Esperado") && _formula.Contains("200"))
                                    item.Eficiencia = Math.Round(CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                         .Replace("Esperado", 1.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                else
                                {
                                    if (!_formula.Contains("/ Esperado"))
                                        item.Eficiencia = Math.Round(CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                }
                            }
                        }
                        catch { }

                        if (item.ValorEsperado == 0 && item.Realizado == 0)
                        {
                            item.Eficiencia = 100;
                        }

                        item.Resultado = Math.Abs(Math.Round(item.Eficiencia != 0 ? ((item.Peso * item.Eficiencia) / 100) : 0, 2));

                        item.EficienciaPonderada = (item.Eficiencia > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia));
                        item.ResultadoPonderado = Math.Abs(Math.Round(item.EficienciaPonderada != 0 ? ((item.Peso * item.EficienciaPonderada) / 100) : 0, 2));
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

        public List<ItensAvaliacaoMetas> Listar(string referencia, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ItensAvaliacaoMetas> _lista = new List<ItensAvaliacaoMetas>();
            Publicas.mensagemDeErro = string.Empty;
            string _formula = "";

            try
            {
                query.Append("Select a.mesreferencia referencia, a.idautoavaliacao, ia.Iditensauto, ia.Idmetas, ia.valoresperado, ia.realizado, Nvl(ia.peso,0) Peso, m.formula ") ;
                query.Append("  from Niff_Ads_Itensavaliacaometas IA, Niff_Ads_Avaliacao A, Niff_Ads_Metas M");
                query.Append(" Where a.Idautoavaliacao = ia.idautoavaliacao");
                query.Append("   And ia.Idmetas = m.Idmetas");
                query.Append("   And a.mesreferencia = " + referencia);
                query.Append("   And a.IdEmpresa = " + idEmpresa);
                query.Append("   and a.tipo = 'MN'");
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ItensAvaliacaoMetas _tipo = new ItensAvaliacaoMetas();

                        _tipo.Existe = true;
                        _tipo.IdMetas = Convert.ToInt32(dataReader["Idmetas"].ToString());
                        _tipo.Peso = Convert.ToDecimal(dataReader["Peso"].ToString());
                        _tipo.Id = Convert.ToInt32(dataReader["iditensauto"].ToString());
                        _tipo.IdAvaliacao = Convert.ToInt32(dataReader["idautoavaliacao"].ToString());
                        _tipo.Referencia = dataReader["referencia"].ToString();
                        _tipo.Formula = dataReader["Formula"].ToString();

                        try
                        {
                            _tipo.ValorEsperado = Convert.ToDecimal(dataReader["ValorEsperado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        }
                        catch { }


                        _lista.Add(_tipo);
                    }
                }

                Pontuacao _pontuacao = new PontuacaoDAO().ConsultarMaiorReferencia(Convert.ToInt32(referencia));

                foreach (var item in _lista)
                {
                    _formula = item.Formula;

                    try
                    {
                        if (item.ValorEsperado != 0)
                            item.Eficiencia = Math.Round(CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                     .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                        else
                        {
                            if (_formula.Contains("/ Esperado") && _formula.Contains("200"))
                                item.Eficiencia = Math.Round(CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                     .Replace("Esperado", 1.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                            else
                            {
                                if (!_formula.Contains("/ Esperado"))
                                    item.Eficiencia = Math.Round(CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                            }
                        }
                    }
                    catch { }

                    if (item.ValorEsperado == 0 && item.Realizado == 0)
                    {
                        item.Eficiencia = 100;
                    }

                    item.Resultado = Math.Abs(Math.Round(item.Eficiencia != 0 ? ((item.Peso * item.Eficiencia) / 100) : 0, 2));

                    item.EficienciaPonderada = (item.Eficiencia > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia));
                    item.ResultadoPonderado = Math.Abs(Math.Round(item.EficienciaPonderada != 0 ? ((item.Peso * item.EficienciaPonderada) / 100) : 0, 2));
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


        public decimal CalculoFormula (string formula)
        {
            IDataReader formulaReader;
            Sessao sessao = new Sessao();

            Query executar = sessao.QueryCalculo(formula);

            try
            {
                formulaReader = executar.ExecuteQuery();
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return 0;
            }

            Publicas.mensagemDeErro = "";
            using (formulaReader)
            {
                if (formulaReader.Read())
                {
                    try
                    {
                        return Convert.ToDecimal(formulaReader["Resultado"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Publicas.mensagemDeErro = ex.Message;
                        return 0;
                    }
                }
                return 0;
           }
        }

        public bool Gravar(List<ItensAvaliacaoMetas> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;

            try
            {
                foreach (var item in _lista)
                {
                    query.Clear();

                    query.Append("Insert into NIFF_Ads_ItensAvaliacaoMetas");
                    query.Append(" ( iditensauto, idautoavaliacao, idmetas, Peso, ValorEsperado, Realizado, Eficiencia, Resultado)");
                    query.Append(" Values ( SQ_NIFF_AdsIdItensAval.NextVal");
                    query.Append("        , " + item.IdAvaliacao);
                    query.Append("        , " + item.IdMetas);
                    query.Append("        , " + item.Peso.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.Realizado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.Eficiencia.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.Resultado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        ) ");
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                        break;
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

        public bool Alterar(ItensAvaliacaoMetas item)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;

            try
            {
                query.Clear();

                query.Append("Update NIFF_Ads_ItensAvaliacaoMetas");
                query.Append("   set ValorEsperado = " + item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."));
                query.Append("     , Realizado = " + item.Realizado.ToString().Replace(".", "").Replace(",", "."));
                query.Append("     , Eficiencia = " + item.Eficiencia.ToString().Replace(".", "").Replace(",", "."));
                query.Append("     , Resultado = " + item.Resultado.ToString().Replace(".", "").Replace(",", "."));
                query.Append(" where Iditensauto = " + item.Id);

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

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

        public bool Excluir(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete NIFF_Ads_ItensAvaliacaoMetas");
                query.Append(" Where idautoavaliacao = " + codigo);
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
