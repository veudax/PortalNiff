using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class NotaFiscalEscrituracaoGlobusDAO
    {
        IDataReader NFReader;

        public List<NotasFiscaisEscrituracaoGlobus> Consultar(string numero, string tipoDocumento, string empresa, string tipoNF)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<NotasFiscaisEscrituracaoGlobus> _lista = new List<NotasFiscaisEscrituracaoGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select e.codintnf, Nvl(e.coddoctoesf,0) coddoctoesf, s.itemsaida, s.nrdocsaida, e.numeronf, e.serienf, e.tiponf,");
                query.Append("       e.dataemissao, e.DATAHORAENTSAI, e.statusnf,  e.codclassfisc,  e.codtpdoc,");
                query.Append("       e.dadosadicionais, e.basecalcicms, s.codoperfiscal_icmssaida opfiscal, s.codsittributaria, ");
                query.Append("       e.valoricms, e.aliquotaicms, e.icmsisentas, e.icmsoutras,");
                query.Append("       e.valortotalnf, e.valorfrete, e.valorseguro, e.valor_desconto, e.valoroutrasdespesas,");
                query.Append("       e.basecalcicms_subst, e.valoricms_subst, Nvl(e.valorbaseipi,0) valorbaseipi, Nvl(e.valoraliqipi,0) valoraliqipi, Nvl(e.valortotalipi,0) valortotalipi, ");
                query.Append("       n.chavedeacesso, n.status, n.mensagemrecibo, n.recibo, n.protocolo, n.data_protocolo,");
                query.Append("       decode(e.codcli, Null, f.nrforn || ' - ' || f.Rsocialforn, c.nrcli || ' - ' || c.rsocialcli) ClienteFornecedor");
                query.Append("  From esfnotafiscal E,");
                query.Append("       bgm_notafiscal_eletronica N,");
                query.Append("       Bgm_Cliente C,");
                query.Append("       bgm_fornecedor F,");
                query.Append("       esfsaida S");
                query.Append(" Where n.numeronfe = " + numero);
                query.Append("   And n.codintnf_esfnf = e.codintnf");
                query.Append("   And c.codcli(+) = e.codcli");
                query.Append("   And f.codigoforn(+) = e.codigoforn");
                query.Append("   And e.tiponf = '" + tipoNF + "'");
                query.Append("   And e.codtpdoc = '" + tipoDocumento + "'");
                query.Append("   And LPad(e.codigoempresa, 3, '0') || '/' || LPad(e.codigofl, 3, '0') = '" + empresa + "'");
                query.Append("   And s.nrdocsaida = Lpad(n.numeronfe,9,'0')");
                query.Append("   And s.codtpdoc = e.codtpdoc");
                query.Append("   And s.codigoempresa = e.codigoempresa");
                query.Append("   And s.codigofl = e.codigofl");
                query.Append("   And s.seriesaida = e.serienf");
                query.Append("   And s.dtemissaosaida = e.dataemissao");

                Query executar = sessao.CreateQuery(query.ToString());

                NFReader = executar.ExecuteQuery();

                using (NFReader)
                {
                    while (NFReader.Read())
                    {
                        NotasFiscaisEscrituracaoGlobus _notas = new NotasFiscaisEscrituracaoGlobus();

                        _notas.Id= Convert.ToInt32(NFReader["codintnf"].ToString());
                        _notas.IdCodDoctoESF = Convert.ToInt32(NFReader["coddoctoesf"].ToString());

                        _notas.NumeroNF = Convert.ToInt32(NFReader["numeronf"].ToString());
                        _notas.Serie = NFReader["serienf"].ToString();
                        _notas.Item = NFReader["itemsaida"].ToString();
                        _notas.Documento = NFReader["nrdocsaida"].ToString();
                        
                        _notas.Emissao = Convert.ToDateTime(NFReader["dataemissao"].ToString());
                        _notas.StatusNF = (NFReader["statusnf"].ToString() == "A" ? "Aberta" : "Cancelada");
                        _notas.CFOP = Convert.ToInt32(NFReader["codclassfisc"].ToString());
                        _notas.TipoDeDocumento = NFReader["codtpdoc"].ToString();
                        _notas.DadosAdicionais = NFReader["dadosadicionais"].ToString();
                        _notas.ClienteFornecedor = NFReader["ClienteFornecedor"].ToString();
                        _notas.ChaveDeAcesso = NFReader["chavedeacesso"].ToString();
                        _notas.StatusSefaz = (NFReader["status"].ToString() == "P" ? "Processando" :
                                             (NFReader["status"].ToString() == "R" ? "Rejeitada" :
                                             (NFReader["status"].ToString() == "D" ? "Denegada" :
                                             (NFReader["status"].ToString() == "C" ? "Cancelada" :
                                             (NFReader["status"].ToString() == "I" ? "Inutilizada" : "Autorizada")))));
                            
                        _notas.UltimaMensagemSefaz = NFReader["mensagemrecibo"].ToString();
                        _notas.ReciboSefaz = NFReader["recibo"].ToString();
                        _notas.ProtocoloSefaz = NFReader["protocolo"].ToString();
                        _notas.DataSefaz = NFReader["data_protocolo"].ToString();
                        _notas.SituacaoTributaria = NFReader["codsittributaria"].ToString();

                        try
                        {
                            _notas.OperacaoFiscal = Convert.ToInt32(NFReader["opfiscal"].ToString());
                        }
                        catch { }

                        
                        _notas.BaseICMS = Convert.ToDecimal(NFReader["basecalcicms"].ToString());
                        _notas.ValorICMS = Convert.ToDecimal(NFReader["valoricms"].ToString());
                        _notas.AliquotaICMS = Convert.ToDecimal(NFReader["aliquotaicms"].ToString());
                        _notas.ValorICMS = Convert.ToDecimal(NFReader["valoricms"].ToString());
                        _notas.IsentasICMS = Convert.ToDecimal(NFReader["icmsisentas"].ToString());
                        _notas.OutrasICMS = Convert.ToDecimal(NFReader["icmsoutras"].ToString());
                        _notas.TotalNF = Convert.ToDecimal(NFReader["valortotalnf"].ToString());
                        _notas.Frete = Convert.ToDecimal(NFReader["valorfrete"].ToString());
                        _notas.Seguro = Convert.ToDecimal(NFReader["valorseguro"].ToString());
                        _notas.Desconto = Convert.ToDecimal(NFReader["valor_desconto"].ToString());
                        _notas.Outros = Convert.ToDecimal(NFReader["valoroutrasdespesas"].ToString());
                        _notas.BaseICMSSubstituicao = Convert.ToDecimal(NFReader["basecalcicms_subst"].ToString());
                        _notas.ValorICMSSubstituicao = Convert.ToDecimal(NFReader["valoricms_subst"].ToString());
                        _notas.BaseIPI = Convert.ToDecimal(NFReader["valorbaseipi"].ToString());
                        try
                        {
                            _notas.AliquotaIPI = Convert.ToDecimal(NFReader["valoraliqipi"].ToString());
                        }
                        catch { }
                        try
                        {
                            _notas.ValorIPI = Convert.ToDecimal(NFReader["valortotalipi"].ToString());
                        }
                        catch { }
                        _notas.Existe = true;

                        _lista.Add(_notas);
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

        public List<NotasFiscaisEscrituracaoGlobus> ConsultarEstoque(string numero, string tipoDocumento, string empresa, string tipoNF)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<NotasFiscaisEscrituracaoGlobus> _lista = new List<NotasFiscaisEscrituracaoGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select e.codintnf, 0 coddoctoesf, s.itemsaida, s.nrdocsaida, e.numeronf, e.serienf, e.tipooperacaonf tiponf,");
                query.Append("       e.dataemissaonf dataemissao, e.DATASAIDANF DATAHORAENTSAI, e.statusnf,  e.codclassfisc,  e.codtpdoc,");
                query.Append("       e.DADOSADICIONAISIMP dadosadicionais, e.basecalcicmsnf basecalcicms, s.codoperfiscal_icmssaida opfiscal, s.codsittributaria, ");
                query.Append("       e.valoricmsnf valoricms, e.aliquotaicmsnf aliquotaicms, nvl(e.icms_isentanf,0) icmsisentas, nvl(e.icms_outrasnf,0) icmsoutras,");
                query.Append("       e.valortotalnf, nvl(e.valorfretenf,0) valorfrete, nvl(e.valorseguronf,0) valorseguro, nvl(e.valordescontonf,0) valor_desconto, nvl(e.outrasdespesasnf,0) valoroutrasdespesas,");
                query.Append("       nvl(e.baseicmsubst,0) basecalcicms_subst, nvl(e.valoricmssubst,0) valoricms_subst, Nvl(e.basecalcipinf,0) valorbaseipi, Nvl(e.aliquotaipinf,0) valoraliqipi, Nvl(e.valoripinf,0) valortotalipi, ");
                query.Append("       n.chavedeacesso, n.status, n.mensagemrecibo, n.recibo, n.protocolo, n.data_protocolo,");
                query.Append("       decode(e.codcli, Null, f.nrforn || ' - ' || f.Rsocialforn, c.nrcli || ' - ' || c.rsocialcli) ClienteFornecedor");
                query.Append("  From Bgm_Notafiscal E,");
                query.Append("       bgm_notafiscal_eletronica N,");
                query.Append("       Bgm_Cliente C,");
                query.Append("       bgm_fornecedor F,");
                query.Append("       esfsaida S");
                query.Append(" Where n.numeronfe = " + numero);
                query.Append("   And n.codintnf_bgmnf(+) = e.codintnf");
                query.Append("   And c.codcli(+) = e.codcli");
                query.Append("   And f.codigoforn(+) = e.codigoforn");
                query.Append("   And e.tipooperacaonf = '" + tipoNF + "'");
                query.Append("   And e.codtpdoc = '" + tipoDocumento + "'");
                query.Append("   And LPad(e.codigoempresa, 3, '0') || '/' || LPad(e.codigofl, 3, '0') = '" + empresa + "'");
                query.Append("   And s.coddoctoesf(+) = e.coddoctoesf");

                Query executar = sessao.CreateQuery(query.ToString());

                NFReader = executar.ExecuteQuery();

                using (NFReader)
                {
                    while (NFReader.Read())
                    {
                        NotasFiscaisEscrituracaoGlobus _notas = new NotasFiscaisEscrituracaoGlobus();
                        _notas.Id = Convert.ToInt32(NFReader["codintnf"].ToString());
                        _notas.IdCodDoctoESF = Convert.ToInt32(NFReader["coddoctoesf"].ToString());

                        _notas.NumeroNF = Convert.ToInt32(NFReader["numeronf"].ToString());
                        _notas.Serie = NFReader["serienf"].ToString();
                        _notas.Emissao = Convert.ToDateTime(NFReader["dataemissao"].ToString());
                        _notas.StatusNF = (NFReader["statusnf"].ToString() == "C" ? "Cancelada" : "Aberta");
                        _notas.CFOP = Convert.ToInt32(NFReader["codclassfisc"].ToString());
                        _notas.TipoDeDocumento = NFReader["codtpdoc"].ToString();
                        _notas.DadosAdicionais = NFReader["dadosadicionais"].ToString();
                        _notas.ClienteFornecedor = NFReader["ClienteFornecedor"].ToString();
                        _notas.ChaveDeAcesso = NFReader["chavedeacesso"].ToString();
                        _notas.StatusSefaz = (NFReader["status"].ToString() == "P" ? "Processando" :
                                             (NFReader["status"].ToString() == "R" ? "Rejeitada" :
                                             (NFReader["status"].ToString() == "D" ? "Denegada" :
                                             (NFReader["status"].ToString() == "C" ? "Cancelada" :
                                             (NFReader["status"].ToString() == "I" ? "Inutilizada" : "Autorizada")))));

                        _notas.UltimaMensagemSefaz = NFReader["mensagemrecibo"].ToString();
                        _notas.ReciboSefaz = NFReader["recibo"].ToString();
                        _notas.ProtocoloSefaz = NFReader["protocolo"].ToString();
                        _notas.DataSefaz = NFReader["data_protocolo"].ToString();
                        _notas.SituacaoTributaria = NFReader["codsittributaria"].ToString();
                        _notas.Item = NFReader["itemsaida,"].ToString();
                        _notas.Documento = NFReader["nrdocsaida"].ToString();

                        try
                        {
                            _notas.OperacaoFiscal = Convert.ToInt32(NFReader["opfiscal"].ToString());
                        }
                        catch { }


                        _notas.BaseICMS = Convert.ToDecimal(NFReader["basecalcicms"].ToString());
                        _notas.ValorICMS = Convert.ToDecimal(NFReader["valoricms"].ToString());
                        _notas.AliquotaICMS = Convert.ToDecimal(NFReader["aliquotaicms"].ToString());
                        _notas.ValorICMS = Convert.ToDecimal(NFReader["valoricms"].ToString());
                        _notas.IsentasICMS = Convert.ToDecimal(NFReader["icmsisentas"].ToString());
                        _notas.OutrasICMS = Convert.ToDecimal(NFReader["icmsoutras"].ToString());
                        _notas.TotalNF = Convert.ToDecimal(NFReader["valortotalnf"].ToString());
                        _notas.Frete = Convert.ToDecimal(NFReader["valorfrete"].ToString());
                        _notas.Seguro = Convert.ToDecimal(NFReader["valorseguro"].ToString());
                        _notas.Desconto = Convert.ToDecimal(NFReader["valor_desconto"].ToString());
                        _notas.Outros = Convert.ToDecimal(NFReader["valoroutrasdespesas"].ToString());
                        _notas.BaseICMSSubstituicao = Convert.ToDecimal(NFReader["basecalcicms_subst"].ToString());
                        _notas.ValorICMSSubstituicao = Convert.ToDecimal(NFReader["valoricms_subst"].ToString());
                        _notas.BaseIPI = Convert.ToDecimal(NFReader["valorbaseipi"].ToString());
                        try
                        {
                            _notas.AliquotaIPI = Convert.ToDecimal(NFReader["valoraliqipi"].ToString());
                        }
                        catch { }
                        try
                        {
                            _notas.ValorIPI = Convert.ToDecimal(NFReader["valortotalipi"].ToString());
                        }
                        catch { }
                        _notas.Existe = true;

                        _lista.Add(_notas);
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

        public List<ItensNotasFiscaisEscrituracaoGlobus> ListarItens(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ItensNotasFiscaisEscrituracaoGlobus> _lista = new List<ItensNotasFiscaisEscrituracaoGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select Rownum coditem, m.codigointernomaterial Codproduto, m.descricaomat descricao, ");
                query.Append("       i.CODSITTRIBUTARIA sittributaria, i.CODCLASSFISC cfop, i.codoperfiscal, ");
                query.Append("       i.QTDEITENSNF qtde, i.VALORUNITARIOITENSNF vlrunitario, i.VALORTOTALITENSNF vlrtotal, ");
                query.Append("       i.ALIQUOTAICMSITENSNF aliqicms, i.ALIQUOTAIPIITENSNF aliqipi, i.ALIQCONFINSITNF aliq_cofins,");
                query.Append("       i.ALIQPISITNF aliq_pis, i.ALIQDIFALITENSNF aliq_icms_dif_aliq, i.BASECALCICMSITENSNF basecalc, ");
                query.Append("       i.BASECALCICMSSUBSTITENSNF baseicmssubst, i.BASECALCIPIITENSNF baseipi,");
                query.Append("       i.VALORICMSITENSNF valoricms, i.VALORICMSSUBSITENSNF valoricmssubst, i.VALORIPIITENSNF valoripi, ");
                query.Append("       i.VALORDIFALITENSNF valor_icms_dif_aliq, i.ISENTAICMSITENSNF Isentas, ");
                query.Append("       i.OUTRASICMSITENSNF outras, i.VALORFRETEITENSNF vlr_prop_frete,");
                query.Append("       i.VALORSEGUROITENSNF vlr_prop_seguro, i.VLROUTRASDESPESASITNF vlr_prop_outras, ");
                query.Append("       i.VALORSERVICOITENSNF vlr_prop_servicos, i.VALORDESCONTOITENSNF vlr_prop_desconto,");
                query.Append("       i.VLRCONFINSITNF vl_cofins, i.VLRPISITNF vl_pis, m.CODIGOMARCAMAT, i.CODIGOMATINT, i.CODIGOLOCAL");
                query.Append("  From Esfnotafiscal_Item i");
                query.Append(" Where i.codintnf = " + id);

                Query executar = sessao.CreateQuery(query.ToString());

                NFReader = executar.ExecuteQuery();

                using (NFReader)
                {
                    while (NFReader.Read())
                    {
                        ItensNotasFiscaisEscrituracaoGlobus _item = new ItensNotasFiscaisEscrituracaoGlobus();

                        _item.Item = Convert.ToInt32(NFReader["coditem"].ToString());
                        _item.Marca = Convert.ToInt32(NFReader["CODIGOMARCAMAT"].ToString());
                        _item.IdMaterial = Convert.ToInt32(NFReader["CODIGOMATINT"].ToString());
                        _item.Local = Convert.ToInt32(NFReader["CODIGOLOCAL"].ToString());

                        _item.CFOP = Convert.ToInt32(NFReader["CFOP"].ToString());
                        _item.OperacaoFiscal = Convert.ToInt32(NFReader["codoperfiscal"].ToString());
                        _item.Quantidade = Convert.ToInt32(NFReader["qtde"].ToString());

                        _item.Produto = NFReader["Codproduto"].ToString() + " - " + NFReader["descricao"].ToString();
                        _item.SituacaoTributaria = NFReader["sittributaria"].ToString();
                        
                        _item.ValorUnitario = Convert.ToDecimal(NFReader["vlrunitario"].ToString());
                        _item.BaseICMS = Convert.ToDecimal(NFReader["basecalc"].ToString());
                        _item.ValorICMS = Convert.ToDecimal(NFReader["valoricms"].ToString());
                        _item.AliquotaICMS = Convert.ToDecimal(NFReader["aliqicms"].ToString());
                        _item.IsentasICMS = Convert.ToDecimal(NFReader["Isentas"].ToString());
                        _item.OutrasICMS = Convert.ToDecimal(NFReader["outras"].ToString());

                        _item.ValorTotal = Convert.ToDecimal(NFReader["vlrtotal"].ToString());

                        try
                        {
                            _item.Frete = Convert.ToDecimal(NFReader["vlr_prop_frete"].ToString());
                        }
                        catch { }

                        try
                        {
                            _item.Seguro = Convert.ToDecimal(NFReader["vlr_prop_seguro"].ToString());
                        }
                        catch { }
                        try { 
                        _item.Desconto = Convert.ToDecimal(NFReader["vlr_prop_desconto"].ToString());
                        }
                        catch { }

                        try { 
                        _item.Outros = Convert.ToDecimal(NFReader["vlr_prop_outras"].ToString());
                        }
                        catch { }

                        _item.BaseICMSSubstituicao = Convert.ToDecimal(NFReader["baseicmssubst"].ToString());
                        _item.ValorICMSSubstituicao = Convert.ToDecimal(NFReader["valoricmssubst"].ToString());
                        _item.BaseIPI = Convert.ToDecimal(NFReader["baseipi"].ToString());

                        try
                        {
                            _item.AliquotaIPI = Convert.ToDecimal(NFReader["aliqipi"].ToString());
                        }
                        catch { }

                        try
                        {
                            _item.ValorIPI = Convert.ToDecimal(NFReader["valoripi"].ToString());
                        }
                        catch { }

                        _item.Cofins = Convert.ToDecimal(NFReader["vl_cofins"].ToString());

                        _item.PIS = Convert.ToDecimal(NFReader["vl_pis"].ToString());

                        try
                        {
                            _item.ValorDiferencial = Convert.ToDecimal(NFReader["valor_icms_dif_aliq"].ToString());
                        }
                        catch { }


                        try
                        {
                            _item.AliquotaDiferencial = Convert.ToDecimal(NFReader["aliq_icms_dif_aliq"].ToString());
                        }
                        catch { }
                        

                        _lista.Add(_item);
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

        public bool Gravar(string tipo, NotasFiscaisEscrituracaoGlobus notas, List<ItensNotasFiscaisEscrituracaoGlobus> itens )
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;

            try
            {
                if (tipo == Publicas.GetDescription(Publicas.TipoNfeGlobus.Saida, ""))
                {
                    #region Gravar no ESFSAida
                    query.Clear();
                    query.Append("Update ESFSaida");
                    query.Append("   set DTSAIDASAIDA = To_Date('" + notas.EntradaSaida.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , DTEMISSAOSAIDA = To_Date('" + notas.Emissao.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , ICMSBASESAIDA = " + notas.BaseICMS.ToString().Replace(".", "").Replace(",","."));
                    query.Append("     , ICMSOUTRASSAIDA = " + notas.OutrasICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ICMSVALORSAIDA = " + notas.ValorICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ICMSISENTASAIDA = " + notas.IsentasICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ICMSALIQSAIDA = " + notas.AliquotaICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , IPIBASESAIDA = " + notas.BaseIPI.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , IPIVALORSAIDA = " + notas.ValorIPI.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , IPIALIQSAIDA = " + notas.AliquotaIPI.ToString().Replace(".", "").Replace(",", "."));

                    query.Append("     , CODSITTRIBUTARIA = '" + notas.SituacaoTributaria + "'");
                    query.Append("     , CODOPERFISCAL_ICMSSAIDA = " + notas.OperacaoFiscal);
                    query.Append("     , CODCLASSFISC = " + notas.CFOP);

                    query.Append(" Where CODDOCTOESF = " + notas.IdCodDoctoESF);
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
                    #endregion

                   
                }
                else
                {
                    #region Gravar no ESFEntra
                    query.Clear();
                    query.Append("Update ESFEntra");
                    query.Append("   set DTENTRADAENTRA = To_Date('" + notas.EntradaSaida.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , DTEMISSAOENTRA = To_Date('" + notas.Emissao.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , ICMSBASEENTRA = " + notas.BaseICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ICMSOUTRASENTRA = " + notas.OutrasICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ICMSVALORENTRA = " + notas.ValorICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ICMSISENTAENTRA = " + notas.IsentasICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ICMSALIQENTRA = " + notas.AliquotaICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , IPIBASEENTRA = " + notas.BaseIPI.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , IPIVALORENTRA = " + notas.ValorIPI.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , IPIALIQENTRA = " + notas.AliquotaIPI.ToString().Replace(".", "").Replace(",", "."));

                    query.Append("     , CODSITTRIBUTARIA = '" + notas.SituacaoTributaria + "'");
                    query.Append("     , CODOPERFISCAL_ICMSSAIDA = " + notas.OperacaoFiscal);
                    query.Append("     , CODCLASSFISC = " + notas.CFOP);

                    query.Append(" Where CODDOCTOESF = " + notas.IdCodDoctoESF);
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
                    #endregion
                }
                #region Gravar no esfnotafiscal
                query.Clear();
                query.Append("Update ESFNotaFiscal");
                query.Append("   set CODCLASSFISC = " + notas.CFOP);
                query.Append("     , CODSITTRIBUTARIA = '" + notas.SituacaoTributaria + "'");
                query.Append("     , DADOSADICIONAIS = '" + notas.DadosAdicionais + "'");
                query.Append("     , DATAHORAENTSAI = To_Date('" + notas.EntradaSaida.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("     , DATAEMISSAO = To_Date('" + notas.Emissao.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("     , BASECALCICMS = " + notas.BaseICMS.ToString().Replace(".", "").Replace(",", "."));
                query.Append("     , ICMSOUTRAS = " + notas.OutrasICMS.ToString().Replace(".", "").Replace(",", "."));
                query.Append("     , VALORICMS = " + notas.ValorICMS.ToString().Replace(".", "").Replace(",", "."));
                query.Append("     , ICMSISENTAS = " + notas.IsentasICMS.ToString().Replace(".", "").Replace(",", "."));
                query.Append("     , ALIQUOTAICMS = " + notas.AliquotaICMS.ToString().Replace(".", "").Replace(",", "."));
                query.Append("     , VALORBASEIPI = " + notas.BaseIPI.ToString().Replace(".", "").Replace(",", "."));
                query.Append("     , VALORTOTALIPI = " + notas.ValorIPI.ToString().Replace(".", "").Replace(",", "."));
                query.Append("     , VALORALIQIPI = " + notas.AliquotaIPI.ToString().Replace(".", "").Replace(",", "."));
                query.Append(" Where CODINTNF = " + notas.Id);
                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                #endregion

                #region Gravar no esfnotafiscal_item

                foreach (var item in itens)
                {
                    query.Clear();
                    query.Append("Update ESFNotaFiscal_Item");
                    query.Append("   set SITTRIBUTARIA = '" + item.SituacaoTributaria + "'");
                    query.Append("     , CFOP = " + item.CFOP);
                    query.Append("     , CODOPERFISCAL = " + item.OperacaoFiscal);
                    query.Append("     , BASECALC = " + item.BaseICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , OUTRAS = " + notas.OutrasICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , VALORICMS = " + notas.ValorICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ISENTAS = " + notas.IsentasICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ALIQICMS = " + item.AliquotaICMS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , BASEIPI = " + item.BaseIPI.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , VALORIPI = " + item.ValorIPI.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ALIQIPI = " + item.AliquotaIPI.ToString().Replace(".", "").Replace(",", "."));

                    query.Append(" Where CODINTNF = " + notas.Id);
                    query.Append("   and CODITEM = " + item.Item);
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
                }

                #endregion
                
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

        public bool Cancelar(string tipo, NotasFiscaisEscrituracaoGlobus notas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;

            try
            {
                if (tipo == Publicas.GetDescription(Publicas.TipoNfeGlobus.Saida, ""))
                {
                    #region Gravar no ESFSAida
                    query.Clear();
                    query.Append("Update ESFSaida");
                    query.Append("   set STATUSSAIDA = 'C'");
                    query.Append("     , OBSSAIDA = 'CANCELADA'");
                    query.Append("     , ICMSBASESAIDA = 0");
                    query.Append("     , ICMSOUTRASSAIDA = 0");
                    query.Append("     , ICMSVALORSAIDA = 0");
                    query.Append("     , ICMSISENTASAIDA = 0");
                    query.Append("     , ICMSALIQSAIDA = 0");
                    query.Append("     , IPIBASESAIDA = 0");
                    query.Append("     , IPIVALORSAIDA = 0");
                    query.Append("     , IPIALIQSAIDA = 0");
                    query.Append("     , VLCONTABILSAIDA = 0");

                    query.Append(" Where CODDOCTOESF = " + notas.IdCodDoctoESF);
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
                    #endregion

                }
                else
                {
                    #region Gravar no ESFEntra
                    query.Clear();
                    query.Append("Update ESFEntra");
                    query.Append("   set STATUSENTRA = 'C'");
                    query.Append("     , OBSENTRA = 'CANCELADA'");
                    query.Append("     , ICMSBASEENTRA = 0");
                    query.Append("     , ICMSOUTRASENTRA = 0");
                    query.Append("     , ICMSVALORENTRA = 0");
                    query.Append("     , ICMSISENTAENTRA = 0");
                    query.Append("     , ICMSALIQENTRA = 0");
                    query.Append("     , IPIBASEENTRA = 0");
                    query.Append("     , IPIVALORENTRA = 0");
                    query.Append("     , IPIALIQENTRA = 0");
                    query.Append("     , VLSERVICOENTRA = 0");

                    query.Append(" Where CODDOCTOESF = " + notas.IdCodDoctoESF);
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
                    #endregion
                }
                #region Gravar no esfnotafiscal
                query.Clear();
                query.Append("Update ESFNotaFiscal");
                query.Append("   set STATUSNF = 'C'");
                query.Append(" Where CODINTNF = " + notas.Id);
                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                #endregion

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
    }
}
