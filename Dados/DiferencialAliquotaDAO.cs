using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class DiferencialAliquotaDAO
    {
        IDataReader dataReader;
        IDataReader dataReaderAux;

        public List<DiferencialAliquota.Documento> Listar(int idEmpresa, string referencia, string CFOPs)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<DiferencialAliquota.Documento> lista = new List<DiferencialAliquota.Documento>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select d.id, d.idempresa, d.referencia ");
                query.Append("     , d.base, d.debito, d.credito, d.diferenca");
                query.Append("     , f.nrforn || ' - ' || f.nfantasiaforn Fornecedor, n.coddoctoesf");
                query.Append("     , n.nrdocentra, n.codtpdoc, n.dtemissaoentra, n.dtentradaentra");
                query.Append("     , n.serieentra, n.Codigoforn, n.codigoempresa, n.codigofl");
                query.Append("  From Niff_Fis_DiferencialAliquota D, esfEntra n, Niff_Chm_Empresas e, Bgm_Fornecedor f");
                query.Append(" Where e.Idempresa = " + idEmpresa);
                query.Append("   And e.Idempresa = d.Idempresa");
                query.Append("   And d.referencia = " + referencia);
                query.Append("   And f.codigoforn = n.codigoforn");
                query.Append("   And n.nrdocentra = d.Documento");
                query.Append("   And n.codigoforn = d.codigoforn");
                query.Append("   And n.serieentra = d.Serie");
                query.Append("   And n.codtpdoc = d.codtpdoc");
                query.Append("   And lpad(n.codigoempresa, 3, '0') || '/' || lpad(n.codigoFl, 3, '0') = e.codigoglobus");
                query.Append(" Group by d.id, d.idempresa, d.referencia ");
                query.Append("     , d.base, d.debito, d.credito, d.diferenca");
                query.Append("     , f.nrforn || ' - ' || f.nfantasiaforn, n.coddoctoesf");
                query.Append("     , n.nrdocentra, n.codtpdoc, n.dtemissaoentra, n.dtentradaentra");
                query.Append("     , n.serieentra, n.Codigoforn, n.codigoempresa, n.codigofl"); 
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        DiferencialAliquota.Documento _tipo = new DiferencialAliquota.Documento();
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Referencia = dataReader["Referencia"].ToString();
                        _tipo.Numero = dataReader["nrdocentra"].ToString();
                        _tipo.Serie = dataReader["serieentra"].ToString();
                        _tipo.CodTipoDoc = dataReader["codtpdoc"].ToString();
                        _tipo.CodigoEmpresa = Convert.ToInt32(dataReader["codigoEmpresa"].ToString());
                        _tipo.CodigoFl = Convert.ToInt32(dataReader["codigofl"].ToString());
                        _tipo.CodigoForn = Convert.ToDecimal(dataReader["CodigoForn"].ToString());
                        _tipo.Fornecedor = dataReader["Fornecedor"].ToString();

                        try
                        {
                            _tipo.Emissao = Convert.ToDateTime(dataReader["dtemissaoentra"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Entrada = Convert.ToDateTime(dataReader["dtentradaentra"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodDoctoESF = Convert.ToDecimal(dataReader["CodDoctoESF"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Base = Convert.ToDecimal(dataReader["Base"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Debito = Convert.ToDecimal(dataReader["Debito"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Credito = Convert.ToDecimal(dataReader["Credito"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.Diferenca = Convert.ToDecimal(dataReader["Diferenca"].ToString());
                        }
                        catch { }
                        
                        decimal baseItens = ValorNFEscrituracao(_tipo.Numero, _tipo.Serie, _tipo.CodTipoDoc, 0, _tipo.CodigoForn, _tipo.CodigoEmpresa, _tipo.CodigoFl, CFOPs);
                        _tipo.ValorESF = baseItens;

                        lista.Add(_tipo);
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
            return lista;
        }

        public List<DiferencialAliquota.Diferencial> Listar(int idEmpresa, string referencia, string CFOPs, string Aliquotas, decimal AliquotaPadrao)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<DiferencialAliquota.Diferencial> lista = new List<DiferencialAliquota.Diferencial>();
            Publicas.mensagemDeErro = string.Empty;
            // quando cadastrado
            try
            {
                query.Append("Select Distinct d.id, d.Iddiferencial, d.idempresa, d.referencia, d.coddoctoesf, d.cfop, d.aliquotaexterna");
                query.Append("     , d.aliquota, d.base, d.debito, d.credito, d.diferenca");
                query.Append("     , f.nrforn || ' - ' || f.nfantasiaforn Fornecedor, n.coddoctoesf");
                query.Append("     , n.nrdocentra, n.codtpdoc, n.dtemissaoentra, n.dtentradaentra");
                query.Append("     , n.serieentra, n.Codigoforn, n.codigoempresa, n.codigofl");
                query.Append("  From Niff_Fis_DiferencialAliquotaCFOP D, esfEntra n, Niff_Chm_Empresas e, Bgm_Fornecedor f");
                query.Append(" Where e.Idempresa = " + idEmpresa);
                query.Append("   And e.Idempresa = d.Idempresa");
                query.Append("   And n.coddoctoesf = d.coddoctoesf");
                query.Append("   And d.referencia = " + referencia);
                query.Append("   And f.codigoforn = n.codigoforn");
                query.Append("   And lpad(n.codigoempresa, 3, '0') || '/' || lpad(n.codigoFl, 3, '0') = e.codigoglobus");
                query.Append("   And n.codclassfisc in (" + CFOPs + ")");
                //query.Append("   And n.coddoctoesf = 5124216");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        DiferencialAliquota.Diferencial _tipo = new DiferencialAliquota.Diferencial();
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdDiferencial = Convert.ToInt32(dataReader["Iddiferencial"].ToString());                        
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Referencia = dataReader["Referencia"].ToString();
                        _tipo.Documento = dataReader["nrdocentra"].ToString();
                        _tipo.Serie = dataReader["serieentra"].ToString();
                        _tipo.CodTipoDoc = dataReader["codtpdoc"].ToString();
                        _tipo.CodigoEmpresa = Convert.ToInt32(dataReader["codigoEmpresa"].ToString());
                        _tipo.CodigoFl = Convert.ToInt32(dataReader["codigofl"].ToString());
                        _tipo.CodigoForn = Convert.ToDecimal(dataReader["CodigoForn"].ToString());
                        _tipo.Fornecedor = dataReader["Fornecedor"].ToString();

                        try
                        {
                            _tipo.Emissao = Convert.ToDateTime(dataReader["dtemissaoentra"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Entrada = Convert.ToDateTime(dataReader["dtentradaentra"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodDoctoESF = Convert.ToDecimal(dataReader["CodDoctoESF"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CFOP = Convert.ToInt32(dataReader["CFOP"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.AliquotaExterna = Convert.ToInt32(dataReader["aliquotaexterna"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Aliquota = Convert.ToInt32(dataReader["aliquota"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Valor = Convert.ToDecimal(dataReader["Base"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Debito = Convert.ToDecimal(dataReader["Debito"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Credito = Convert.ToDecimal(dataReader["Credito"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.Diferenca = Convert.ToDecimal(dataReader["Diferenca"].ToString());
                        }
                        catch { }

                        _tipo.Detalhamento = new List<DiferencialAliquota.Detalhes>();

                        _tipo.Detalhamento = Listar(idEmpresa, _tipo.CodDoctoESF, _tipo.Aliquota, CFOPs);

                        lista.Add(_tipo);
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
            return lista;
        }
        
        public decimal ValorNFEscrituracao(string Numero, string Serie, string codtpdoc, int cfop, decimal codigoForn, int codigoempresa, int codigofl, string CFOPs)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;
            try
            {
                query.Append("Select Sum(n.vlcontabilentra) valor");
                query.Append("  From Esfentra n");
                query.Append(" Where n.nrdocentra = '" + Numero + "'");
                query.Append("   And n.serieentra = '" + Serie + "'");
                query.Append("   And n.codtpdoc = '" + codtpdoc + "'");
                query.Append("   And n.codigoforn = " + codigoForn);
                query.Append("   And n.codigoempresa = " + codigoempresa);
                query.Append("   And n.codigofl = " + codigofl);
                query.Append("   And n.codclassfisc in (" + CFOPs + ")");

                if (cfop != 0)
                    query.Append("   And n.codclassfisc = " + cfop);

                Query executar = sessao.CreateQuery(query.ToString());
                dataReaderAux = executar.ExecuteQuery();
                using (dataReaderAux)
                {
                    if (dataReaderAux.Read())
                    {
                        valor = Convert.ToDecimal(dataReaderAux["Valor"].ToString());
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
            return valor;
        }

        public List<DiferencialAliquota.Diferencial> Listar(int idEmpresa, DateTime inicio, DateTime fim, decimal aliquota, string CFOPs, string AliquotasValidas, decimal AliquotaPadrao)
        {
            // quando nao cadastrado
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<DiferencialAliquota.Diferencial> lista = new List<DiferencialAliquota.Diferencial>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select n.nrdocentra, n.itementra, n.codtpdoc, n.dtemissaoentra, n.dtentradaentra");
                query.Append("     , i.aliquotaicms, i.cfop, n.codclassfisc");
                query.Append("     , f.nrforn || ' - ' || f.nfantasiaforn Fornecedor, n.coddoctoesf");
                query.Append("     , n.serieentra, n.Codigoforn, n.codigoempresa, n.codigofl");
                query.Append("     , Sum((i.valortotal + i.valorfrete + i.valoripi + i.seguro + i.outrasdespesas) - i.desconto) valortotal");
                query.Append("     , Sum(i.valoricms) valoricms");
                query.Append("  From esfentra n, Niff_Fis_Arquivei a, Niff_Fis_Itensarquivei i, Bgm_Fornecedor f, niff_fis_cfopcst p");
                query.Append(" Where a.Idempresa = " + idEmpresa);
                query.Append("   And n.dtentradaentra between To_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') and To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy')");
                //query.Append("   And a.coddoctoesf = n.coddoctoesf");
                query.Append("   And(a.Coddoctoesf Like '%' || To_char(n.Coddoctoesf) || '%'");
                query.Append("    Or a.Coddoctoesf = To_char(n.Coddoctoesf))");
                query.Append("   And a.Idarquivei = i.Idarquivei");
                query.Append("   And f.codigoforn = n.codigoforn");
                query.Append("   And i.aliquotaicms <> " + (int)aliquota);
                query.Append("   And n.codclassfisc in (" + CFOPs + ")");
                query.Append("   And n.codclassfisc = p.cfopentrada"); 
                query.Append("   And i.cfop = p.cfopsaida");
                //query.Append("   And n.coddoctoesf = 5124216");
                query.Append(" Group By n.nrdocentra, n.itementra, n.codtpdoc, n.dtemissaoentra, n.dtentradaentra");
                query.Append("     , i.aliquotaicms, i.cfop, n.codclassfisc");
                query.Append("     , f.nrforn || ' - ' || f.nfantasiaforn, n.coddoctoesf");
                query.Append("     , n.serieentra, n.Codigoforn, n.codigoempresa, n.codigofl");

                Query executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        if (!CFOPs.Contains(dataReader["codclassfisc"].ToString()))
                            continue;

                        DiferencialAliquota.Diferencial _tipo = new DiferencialAliquota.Diferencial();
                        _tipo.Existe = false;
                        _tipo.Id = 0;
                        _tipo.IdEmpresa = idEmpresa;
                        _tipo.Documento = dataReader["nrdocentra"].ToString();
                        _tipo.Serie = dataReader["serieentra"].ToString();
                        _tipo.CodTipoDoc = dataReader["codtpdoc"].ToString();
                        _tipo.CodigoEmpresa = Convert.ToInt32(dataReader["codigoEmpresa"].ToString());
                        _tipo.CodigoFl = Convert.ToInt32(dataReader["codigofl"].ToString());
                        _tipo.CodigoForn = Convert.ToDecimal(dataReader["CodigoForn"].ToString());
                        _tipo.Fornecedor = dataReader["Fornecedor"].ToString();

                        try
                        {
                            _tipo.Emissao = Convert.ToDateTime(dataReader["dtemissaoentra"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Entrada = Convert.ToDateTime(dataReader["dtentradaentra"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodDoctoESF = Convert.ToDecimal(dataReader["CodDoctoESF"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CFOP = Convert.ToInt32(dataReader["codclassfisc"].ToString());
                        }
                        catch { }

                        _tipo.AliquotaExterna = aliquota;

                        try
                        {
                            _tipo.Valor = Convert.ToDecimal(dataReader["valortotal"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Credito = Convert.ToDecimal(dataReader["valoricms"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Aliquota = Convert.ToDecimal(dataReader["aliquotaicms"].ToString());
                            _tipo.AliquotaOriginal = _tipo.Aliquota;

                            if (_tipo.Aliquota == 0 || !AliquotasValidas.Contains(_tipo.Aliquota.ToString()))
                            {
                                _tipo.AliquotaZerada = true;
                                _tipo.Aliquota = AliquotaPadrao;
                                _tipo.Credito = _tipo.Valor * (_tipo.Aliquota / 100);
                            }
                        }
                        catch { }
                       

                        _tipo.Debito = Math.Round(_tipo.Valor * (aliquota / 100),2);
                        _tipo.Diferenca = _tipo.Debito - _tipo.Credito;
                        
                        _tipo.Detalhamento = new List<DiferencialAliquota.Detalhes>();

                        _tipo.Detalhamento = Listar(idEmpresa, _tipo.CodDoctoESF, _tipo.AliquotaOriginal, _tipo.CFOP.ToString());


                        lista.Add(_tipo);

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
            return lista;
        }

        private List<DiferencialAliquota.Detalhes> Listar(int idEmpresa, decimal codigoEsf, decimal aliquota, string CFOPs)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<DiferencialAliquota.Detalhes> lista = new List<DiferencialAliquota.Detalhes>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select n.nrdocentra, n.itementra, n.codtpdoc, n.dtemissaoentra, n.dtentradaentra");
                query.Append("     , i.aliquotaicms, i.valoricms, i.cfop, n.codclassfisc");
                query.Append("     , (i.valortotal + i.valorfrete + i.valoripi + i.seguro + i.outrasdespesas) - i.desconto valortotal");
                query.Append("     , f.nrforn || ' - ' || f.nfantasiaforn Fornecedor, n.coddoctoesf");
                query.Append("  From esfentra n, Niff_Fis_Arquivei a, Niff_Fis_Itensarquivei i, Bgm_Fornecedor f");
                query.Append(" Where a.Idempresa = " + idEmpresa);
                query.Append("   And n.coddoctoesf = " + codigoEsf);
                //query.Append("   And a.coddoctoesf = n.coddoctoesf");
                query.Append("   And(a.Coddoctoesf Like '%' || To_char(n.Coddoctoesf) || '%'");
                query.Append("    Or a.Coddoctoesf = To_char(n.Coddoctoesf))");     
                query.Append("   And a.Idarquivei = i.Idarquivei");
                query.Append("   And n.codclassfisc in (" + CFOPs + ")");
                query.Append("   And f.codigoforn = n.codigoforn");
                query.Append("   And i.aliquotaicms = " + aliquota.ToString().Replace(".", "").Replace(",", "."));

                Query executar = sessao.CreateQuery(query.ToString());
                dataReaderAux = executar.ExecuteQuery();

                using (dataReaderAux)
                {
                    while (dataReaderAux.Read())
                    {
                        if (!CFOPs.Contains(dataReaderAux["codclassfisc"].ToString()))
                            continue;

                        DiferencialAliquota.Detalhes _tipo = new DiferencialAliquota.Detalhes();
                        _tipo.Existe = false;
                        _tipo.Id = 0;
                        _tipo.Documento = dataReaderAux["nrdocentra"].ToString();
                        _tipo.Item = dataReaderAux["itementra"].ToString();
                        _tipo.TipoDocto = dataReaderAux["codtpdoc"].ToString();
                        _tipo.Fornecedor = dataReaderAux["Fornecedor"].ToString();

                        try
                        {
                            _tipo.Emissao = Convert.ToDateTime(dataReaderAux["dtemissaoentra"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Entrada = Convert.ToDateTime(dataReaderAux["dtentradaentra"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CodDoctoESF = Convert.ToDecimal(dataReaderAux["CodDoctoESF"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CFOPNota = Convert.ToInt32(dataReaderAux["CFOP"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Aliquota = Convert.ToDecimal(dataReaderAux["aliquotaicms"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Base = Convert.ToDecimal(dataReaderAux["valortotal"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.ICMS = Convert.ToDecimal(dataReaderAux["valoricms"].ToString());
                        }
                        catch { }


                        lista.Add(_tipo);
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
            return lista;
        }

        public bool Gravar(List<DiferencialAliquota.Documento> _documentos, List<DiferencialAliquota.Diferencial> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            decimal id = 1;
            try
            {
                foreach (var itemd in _documentos)
                {
                    query.Clear();
                    if (!itemd.Existe)
                    {
                        query.Append("Select nvl(Max(Id),0)+1 next from Niff_Fis_DiferencialAliquota");

                        Query executar = sessao.CreateQuery(query.ToString());
                        dataReaderAux = executar.ExecuteQuery();

                        using (dataReaderAux)
                        {
                            if (dataReaderAux.Read())
                                id = Convert.ToInt32(dataReaderAux["Next"].ToString());
                        }

                        query.Clear();
                        query.Append("Insert into Niff_Fis_DiferencialAliquota");
                        query.Append(" ( id, idempresa, referencia, documento, serie, codigoforn");
                        query.Append(" , codtpdoc, base, debito, credito, diferenca)");
                        query.Append(" Values (" + id);
                        query.Append(" , " + itemd.IdEmpresa);
                        query.Append(" , " + itemd.Referencia);
                        query.Append(" , '" + itemd.Numero + "'");
                        query.Append(" , '" + itemd.Serie + "'");
                        query.Append(" , " + itemd.CodigoForn);
                        query.Append(" , '" + itemd.CodTipoDoc + "'");
                        query.Append(" , " + itemd.Base.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(" , " + itemd.Debito.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(" , " + itemd.Credito.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(" , " + itemd.Diferenca.ToString().Replace(".", "").Replace(",", "."));

                        query.Append(" ) ");
                    }
                    else
                    {
                        id = itemd.Id;
                        query.Append("Update Niff_Fis_DiferencialAliquota");
                        query.Append("   set Base = " + itemd.Base.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , Debito = " + itemd.Debito.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , Credito = " + itemd.Credito.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , Diferenca = " + itemd.Diferenca.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(" where Id = " + itemd.Id);
                    }

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                        break;

                    foreach (var item in _lista.Where(w => w.IdDiferencial == itemd.Id))
                    {
                        query.Clear();
                        if (!item.Existe)
                        {
                            query.Append("Insert into Niff_Fis_DiferencialAliquotaCFOP");
                            query.Append(" ( id, IdDiferencial, idempresa, referencia, coddoctoesf, cfop, aliquotaexterna, aliquota, base, debito, credito, diferenca)");
                            query.Append(" Values ((Select nvl(Max(Id),0)+1 From Niff_Fis_DiferencialAliquotaCFOP)");
                            query.Append(" , " + id);
                            query.Append(" , " + item.IdEmpresa);
                            query.Append(" , " + item.Referencia);
                            query.Append(" , " + item.CodDoctoESF);
                            query.Append(" , " + item.CFOP);
                            query.Append(" , " + item.AliquotaExterna.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" , " + item.Aliquota.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" , " + item.Valor.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" , " + item.Debito.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" , " + item.Credito.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" , " + item.Diferenca.ToString().Replace(".", "").Replace(",", "."));

                            query.Append(" ) ");
                        }
                        else
                        {
                            query.Append("Update Niff_Fis_DiferencialAliquotaCFOP");
                            query.Append("   set CodDoctoESF = " + item.CodDoctoESF);
                            query.Append("     , CFOP = " + item.CFOP);
                            query.Append("     , AliquotaExterna = " + item.AliquotaExterna.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , Aliquota = " + item.Aliquota.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , Base = " + item.Valor.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , Debito = " + item.Debito.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , Credito = " + item.Credito.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , Diferenca = " + item.Diferenca.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" where Id = " + item.Id);
                        }

                        retorno = sessao.ExecuteSqlTransaction(query.ToString());

                        if (!retorno)
                            break;
                    }
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

        public bool Excluir(int idEmpresa, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();

                query.Append("delete Niff_Fis_DiferencialAliquotaCFOP");
                query.Append(" where IdEmpresa = " + idEmpresa);
                query.Append("   and Referencia = " + referencia);

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;

                query.Clear();
                query.Append("delete Niff_Fis_DiferencialAliquota");
                query.Append(" where IdEmpresa = " + idEmpresa);
                query.Append("   and Referencia = " + referencia);
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

        public bool Excluir(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();

                query.Append("delete Niff_Fis_DiferencialAliquotaCFOP");
                query.Append(" where Id = " + id);

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
