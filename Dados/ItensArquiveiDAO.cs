using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ItensArquiveiDAO
    {
        IDataReader dataReader;

        public List<ItensArquivei> Listar(int IdArquivei, bool apenasComDiferencas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ItensArquivei> _lista = new List<ItensArquivei>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select iditens, idarquivei, valoricms, aliquotaicms, valoricmsst, valoripi, desconto, seguro, outrasdespesas");
                query.Append(", valorfrete, cce, cst, csticms, cfop, valortotal, comdiferencas, ncm, valorpis, aliquotapis, basepis");
                query.Append(", valorcofins, aliquotacofins, basecofins, aliquotaipi, baseipi, aliquotaiss, baseiss, valoriss, aliquotaicmsst"); 
                query.Append("  from NIFF_FIS_ItensArquivei");
                query.Append(" Where idarquivei = " + IdArquivei);

                if (apenasComDiferencas)
                    query.Append("   and ComDiferencas = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ItensArquivei _tipo = new ItensArquivei();

                        _tipo.Existe = true;
                        _tipo.IdArquivei = Convert.ToInt32(dataReader["idArquivei"].ToString());
                        _tipo.Id = Convert.ToInt32(dataReader["iditens"].ToString());

                        _tipo.ComDiferencas = dataReader["ComDiferencas"].ToString() == "S";

                        _tipo.ValorICMS = Convert.ToDecimal(dataReader["ValorICMS"].ToString());
                        _tipo.AliquotaICMS = Convert.ToDecimal(dataReader["AliquotaICMS"].ToString());
                        _tipo.ValorIPI = Convert.ToDecimal(dataReader["ValorPIS"].ToString());
                        _tipo.ValorICMSSub = Convert.ToDecimal(dataReader["ValorICMSST"].ToString());
                        _tipo.Desconto = Convert.ToDecimal(dataReader["Desconto"].ToString());
                        _tipo.Seguro = Convert.ToDecimal(dataReader["Seguro"].ToString());
                        _tipo.OutrasDespesas = Convert.ToDecimal(dataReader["OutrasDespesas"].ToString());
                        _tipo.ValorFrete = Convert.ToDecimal(dataReader["ValorFrete"].ToString());
                        _tipo.ValorTotal = Convert.ToDecimal(dataReader["ValorTotal"].ToString());

                        _tipo.CCe = dataReader["CCE"].ToString();
                        _tipo.CST = dataReader["CST"].ToString();
                        _tipo.CSTICMS = dataReader["CSTICMS"].ToString();
                        _tipo.CFOP = Convert.ToInt32(dataReader["CFOP"].ToString());
                                                
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

        public bool Grava(List<ItensArquivei> itens)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int Id = 1;

            bool retorno = true;

            try
            {
                foreach (var item in itens)
                {
                    if (!item.Existe)
                    {
                        query.Clear();
                        query.Append("Select SQ_NIFF_IdItensArquivei.NextVal next From dual");
                        Query executar = sessao.CreateQuery(query.ToString());

                        dataReader = executar.ExecuteQuery();

                        using (dataReader)
                        {
                            if (dataReader.Read())
                                Id = Convert.ToInt32(dataReader["next"].ToString());
                        }

                        query.Clear();
                        query.Append("Insert into NIFF_FIS_ItensArquivei (");
                        query.Append("       iditens, idarquivei, valoricms,  aliquotaicms, valoricmsst, valoripi, desconto, seguro, outrasdespesas");
                        query.Append("     , valorfrete, AliquotaPis, BasePis, ValorPis, ValorCofins, AliquotaCofins, BaseCofins, AliquotaICMSST");
                        query.Append("     , AliquotaIPI, BaseIPI, AliquotaISS, BaseISS, ValorISS, NCM"); 


                        if (item.CCe != "" && item.CCe != null)
                            query.Append("     ,  cce");
                        if (item.CST != "" && item.CST != null)
                            query.Append("     , cst");
                        if (item.CSTICMS != "" && item.CSTICMS != null)
                            query.Append("     , csticms");

                        query.Append("     , cfop, valortotal");

                        query.Append("  ) Values ( " + Id);
                        query.Append("  , " + item.IdArquivei );
                        query.Append("  , " + item.ValorICMS.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.AliquotaICMS.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorICMSSub.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorIPI.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.Desconto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.Seguro.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.OutrasDespesas.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorFrete.ToString().Replace(".", "").Replace(",", "."));

                        query.Append("  , " + item.AliquotaPis.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.BasePis.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorPis.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorCofins.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.AliquotaCofins.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.BaseCofins.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.AliquotaICMSSub.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.AliquotaIPI.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.BaseIPI.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.AliquotaISS.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.BaseISS.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorISS.ToString().Replace(".", "").Replace(",", "."));

                        query.Append("  , '" + item.NCM + "'");

                        if (item.CCe != "" && item.CCe != null)
                            query.Append("  , '" + item.CCe.Replace("'", "") + "'");
                        if (item.CST != "" && item.CST != null)
                            query.Append("  , '" + item.CST + "'");
                        if (item.CSTICMS != "" && item.CSTICMS != null)
                            query.Append("  , '" + item.CSTICMS + "'");

                        query.Append("  , " + item.CFOP);
                        query.Append("  , " + item.ValorTotal.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(") ");
                    }
                    else
                    {
                        Id = item.Id;
                        query.Clear();
                        query.Append("Update NIFF_FIS_ItensArquivei ");
                        query.Append("   set ComDiferencas = '" + (item.ComDiferencas ? "S" : "N") + "'");

                        if (item.CCe != "" && item.CCe != null)
                            query.Append("  , cce = '" + item.CCe.Replace("'", "") + "'");

                        query.Append(" Where iditens = " + Id);
                    }

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                    {
                        Log _log = new Log();
                        _log.IdUsuario = Publicas._usuario.Id;
                        _log.Descricao = "Erro ao gravar os itens da Nota Fiscal Arquivei chave " + item.ChaveDeAcesso; 
                        _log.Tela = "Principal - Arquivei - Erro ao gravar itens";

                        try
                        {
                            new LogDAO().Gravar(_log);
                        }
                        catch { }
                        return false;
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

        public List<ItensComparacao> ListarItensArquivei(int IdEmpresa, DateTime Inicio, DateTime fim, bool dataEntrada, string tipoProcessamento = "Recebidos")
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ItensComparacao> _lista = new List<ItensComparacao>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (tipoProcessamento == "Recebidos")
                {
                    if (!dataEntrada)
                    {
                        query.Append("Select a.Idarquivei, i.valoricms, i.aliquotaicms, i.valoricmsst");
                        query.Append("     , i.valoripi, i.desconto, i.seguro, i.outrasdespesas");
                        query.Append("     , i.valorfrete, i.valortotal, i.cst, i.csticms, i.cce, i.cfop");
                        query.Append("     , i.comdiferencas");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao(i.cfop, 'C') Cfopentrada");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao(i.cfop, 'S') Cstcomp");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao(i.cfop, 'O') Operacao");
                        query.Append("     , a.chavedeacesso, i.NCM");
                        query.Append("     , i.ValorCofins, i.ValorPis, I.ValorISS");
                        query.Append("  From Niff_Fis_Arquivei a");
                        query.Append("     , Niff_Fis_Itensarquivei i");
                        query.Append(" Where i.Idarquivei = a.Idarquivei");
                        query.Append("   And a.dataemissao Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("   And a.idempresa = " + IdEmpresa);
                        query.Append("   And a.TIPOPROCESSAMENTO = 'Recebidos'");
                        query.Append("   And a.tipodocto <> 'NFSe'");
                        //if (ApenasConferidas)
                        //  query.Append("   And a.Conferido = 'S'");
                        //query.Append("   And a.idarquivei = 62189");
                    }
                    else
                    {
                        query.Append("Select a.Idarquivei, i.valoricms, i.aliquotaicms, i.valoricmsst");
                        query.Append("     , i.valoripi, i.desconto, i.seguro, i.outrasdespesas");
                        query.Append("     , i.valorfrete, i.valortotal, i.cst, i.csticms, i.cce, i.cfop");
                        query.Append("     , i.comdiferencas");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao(i.cfop, 'C') Cfopentrada");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao(i.cfop, 'S') Cstcomp");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao(i.cfop, 'O') Operacao");

                        query.Append("     , a.chavedeacesso, i.NCM");
                        query.Append("     , i.ValorCofins, i.ValorPis, I.ValorISS");
                        query.Append("  From Niff_Fis_Arquivei a");
                        query.Append("     , Niff_Fis_Itensarquivei i");
                        query.Append("     , bgm_notafiscal e");
                        query.Append(" Where i.Idarquivei = a.Idarquivei");
                        query.Append("   And a.idempresa = " + IdEmpresa);
                        query.Append("   And e.codintnf = a.codintnf");
                        query.Append("   And e.entradasaidanf Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("   And a.TIPOPROCESSAMENTO = 'Recebidos'");
                        query.Append("   And a.tipodocto <> 'NFSe'");
                        //if (ApenasConferidas)
                        //  query.Append("   And a.Conferido = 'S'");
                        // query.Append("   And a.idarquivei = 62189");
                        query.Append("   Union all ");

                        query.Append("Select a.Idarquivei, i.valoricms, i.aliquotaicms, i.valoricmsst");
                        query.Append("     , i.valoripi, i.desconto, i.seguro, i.outrasdespesas");
                        query.Append("     , i.valorfrete, i.valortotal, i.cst, i.csticms, i.cce, i.cfop");
                        query.Append("     , i.comdiferencas");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao(i.cfop, 'C') Cfopentrada");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao(i.cfop, 'S') Cstcomp");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao(i.cfop, 'O') Operacao");

                        query.Append("     , a.chavedeacesso, i.NCM");
                        query.Append("     , i.ValorCofins, i.ValorPis, I.ValorISS");
                        query.Append("  From Niff_Fis_Arquivei a");
                        query.Append("     , Niff_Fis_Itensarquivei i");
                        query.Append("     , esfentra e");
                        query.Append(" Where i.Idarquivei = a.Idarquivei");
                        query.Append("   And a.idempresa = " + IdEmpresa);
                        query.Append("   And e.coddoctoesf = a.coddoctoesf");
                        query.Append("   And e.sistema <> 'EST'");
                        query.Append("   And e.dtentradaentra Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("   And a.TIPOPROCESSAMENTO = 'Recebidos'");
                        query.Append("   And a.tipodocto <> 'NFSe'");
                        //if (ApenasConferidas)
                        //  query.Append("   And a.Conferido = 'S'");
                        //query.Append("   And a.idarquivei = 62189");
                    }
                }
                else // Emitidos
                {
                    if (!dataEntrada)
                    {
                        query.Append("Select a.Idarquivei, i.valoricms, i.aliquotaicms, i.valoricmsst");
                        query.Append("     , i.valoripi, i.desconto, i.seguro, i.outrasdespesas");
                        query.Append("     , i.valorfrete, i.valortotal, i.cst, i.csticms, i.cce, i.cfop");
                        query.Append("     , i.comdiferencas, a.naturezaoperacao");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao_Emitidas(i.cfop, Upper(a.naturezaoperacao), 'C', " + IdEmpresa + ", i.idarquivei) Cfopentrada");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao_Emitidas(i.cfop, Upper(a.naturezaoperacao), 'S', " + IdEmpresa + ", i.idarquivei) Cstcomp");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao_Emitidas(i.cfop, Upper(a.naturezaoperacao), 'O', " + IdEmpresa + ", i.idarquivei) Operacao");
                        
                        query.Append("     , a.chavedeacesso, i.NCM");
                        query.Append("     , i.ValorCofins, i.ValorPis, I.ValorISS");
                        query.Append("  From Niff_Fis_Arquivei a");
                        query.Append("     , Niff_Fis_Itensarquivei i");
                        query.Append(" Where i.Idarquivei = a.Idarquivei");
                        query.Append("   And a.dataemissao Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("   And a.idempresa = " + IdEmpresa);
                        query.Append("   And a.TIPOPROCESSAMENTO = 'Emitidos'");
                        query.Append("   And a.tipodocto <> 'NFSe'");
                        //if (ApenasConferidas)
                        //  query.Append("   And a.Conferido = 'S'");
                        //query.Append("   And a.idarquivei = 62189");
                    }
                    else
                    {
                        query.Append("Select a.Idarquivei, i.valoricms, i.aliquotaicms, i.valoricmsst");
                        query.Append("     , i.valoripi, i.desconto, i.seguro, i.outrasdespesas");
                        query.Append("     , i.valorfrete, i.valortotal, i.cst, i.csticms, i.cce, i.cfop");
                        query.Append("     , i.comdiferencas, a.naturezaoperacao");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao_Emitidas(i.cfop, Upper(a.naturezaoperacao), 'C', " + IdEmpresa + ", i.idarquivei) Cfopentrada");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao_Emitidas(i.cfop, Upper(a.naturezaoperacao), 'S', " + IdEmpresa + ", i.idarquivei) Cstcomp");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao_Emitidas(i.cfop, Upper(a.naturezaoperacao), 'O', " + IdEmpresa + ", i.idarquivei) Operacao");
                        
                        query.Append("     , a.chavedeacesso, i.NCM");
                        query.Append("     , i.ValorCofins, i.ValorPis, I.ValorISS");
                        query.Append("  From Niff_Fis_Arquivei a");
                        query.Append("     , Niff_Fis_Itensarquivei i");
                        query.Append("     , bgm_notafiscal e");
                        query.Append(" Where i.Idarquivei = a.Idarquivei");
                        query.Append("   And a.idempresa = " + IdEmpresa);
                        query.Append("   And e.codintnf = a.codintnf");
                        query.Append("   And e.entradasaidanf Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("   And a.TIPOPROCESSAMENTO = 'Emitidos'");
                        query.Append("   And a.tipodocto <> 'NFSe'");
                        //if (ApenasConferidas)
                        //  query.Append("   And a.Conferido = 'S'");
                        // query.Append("   And a.idarquivei = 62189");
                        query.Append("   Union all ");

                        query.Append("Select a.Idarquivei, i.valoricms, i.aliquotaicms, i.valoricmsst");
                        query.Append("     , i.valoripi, i.desconto, i.seguro, i.outrasdespesas");
                        query.Append("     , i.valorfrete, i.valortotal, i.cst, i.csticms, i.cce, i.cfop");
                        query.Append("     , i.comdiferencas, a.naturezaoperacao");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao_Emitidas(i.cfop, Upper(a.naturezaoperacao), 'C', " + IdEmpresa + ", i.idarquivei) Cfopentrada");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao_Emitidas(i.cfop, Upper(a.naturezaoperacao), 'S', " + IdEmpresa + ", i.idarquivei) Cstcomp");
                        query.Append("     , FC_NIFF_CFOPCSTOperacao_Emitidas(i.cfop, Upper(a.naturezaoperacao), 'O', " + IdEmpresa + ", i.idarquivei) Operacao");
                        
                        query.Append("     , a.chavedeacesso, i.NCM");
                        query.Append("     , i.ValorCofins, i.ValorPis, I.ValorISS");
                        query.Append("  From Niff_Fis_Arquivei a");
                        query.Append("     , Niff_Fis_Itensarquivei i");
                        query.Append("     , esfSaida e");
                        query.Append(" Where i.Idarquivei = a.Idarquivei");
                        query.Append("   And a.idempresa = " + IdEmpresa);
                        query.Append("   And e.coddoctoesf = a.coddoctoesf");
                        query.Append("   And e.sistema <> 'EST'");
                        query.Append("   And e.dtsaidaSaida Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("   And a.TIPOPROCESSAMENTO = 'Emitidos'");
                        query.Append("   And a.tipodocto <> 'NFSe'");
                        //if (ApenasConferidas)
                        //query.Append("   And a.Conferido = 'S'");
                        //query.Append("   And a.idarquivei = 62189");
                    }
                }
                Query executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ItensComparacao _tipo = new ItensComparacao();

                        _tipo.IdArquivei = Convert.ToInt32(dataReader["idArquivei"].ToString());
                        _tipo.ComDiferencas = dataReader["ComDiferencas"].ToString() == "S";
                        _tipo.ChaveDeAcesso = dataReader["chavedeacesso"].ToString();
                        _tipo.NCMArquivo = dataReader["NCM"].ToString();
                        _tipo.CCe = dataReader["cce"].ToString();
                        

                        try
                        {
                            _tipo.ValorICMS = Math.Round(Convert.ToDecimal(dataReader["ValorIcms"].ToString()),2);
                        }
                        catch { }

                        try
                        {
                            _tipo.AliquotaICMS = Math.Round(Convert.ToDecimal(dataReader["aliquotaICMS"].ToString()),2);
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorICMSSub = Math.Round(Convert.ToDecimal(dataReader["ValorICMSST"].ToString()), 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorIPI = Math.Round(Convert.ToDecimal(dataReader["ValorIPI"].ToString()), 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.Desconto = Math.Round(Convert.ToDecimal(dataReader["Desconto"].ToString()), 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.Seguro = Math.Round(Convert.ToDecimal(dataReader["Seguro"].ToString()), 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.OutrasDespesas = Math.Round(Convert.ToDecimal(dataReader["OutrasDespesas"].ToString()), 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorFrete = Math.Round(Convert.ToDecimal(dataReader["ValorFrete"].ToString()), 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorTotal = Math.Round(Convert.ToDecimal(dataReader["ValorTotal"].ToString()), 2);
                        }
                        catch { }


                        try
                        {
                            _tipo.ValorCofins = Math.Round(Convert.ToDecimal(dataReader["ValorCofins"].ToString()), 2);
                        }
                        catch { }


                        try
                        {
                            _tipo.ValorPis = Math.Round(Convert.ToDecimal(dataReader["ValorPis"].ToString()), 2);
                        }
                        catch { }


                        try
                        {
                            _tipo.ValorISS = Math.Round(Convert.ToDecimal(dataReader["ValorISS"].ToString()), 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.CST = dataReader["CST"].ToString();
                        }
                        catch { }

                        try
                        {
                            _tipo.CSTICMS = dataReader["CSTICMS"].ToString();
                        }
                        catch { }

                        try
                        {
                            _tipo.CSTComparar = dataReader["CSTComp"].ToString();
                        }
                        catch { }

                        try
                        {
                            _tipo.Operacao = dataReader["Operacao"].ToString();
                        }
                        catch { }

                        try
                        {
                            _tipo.CFOP = Convert.ToInt32(dataReader["CFOP"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CFOPComparar = dataReader["cfopentrada"].ToString();
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

        public List<ItensComparacao> ListarItensGlobus(int IdEmpresa, DateTime Inicio, DateTime fim, bool dataEntrada, string tipoProcessamento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ItensComparacao> _lista = new List<ItensComparacao>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (tipoProcessamento == "Recebidos")
                {
                    query.Append("Select a.idarquivei, i.codintnf, i.valoricmsitensnf, i.aliquotaicmsitensnf, i.valoricmssubsitensnf");
                    query.Append("     , i.valoripiitensnf, i.valordescontoitensnf, i.valorseguroitensnf");
                    query.Append("     , i.vlroutrasdespesasitnf, i.valorfreteitensnf, i.valortotalitensnf");
                    query.Append("     , to_char(i.codsittributaria) codsittributaria, to_char(i.codclassfisc) codclassfisc, i.codoperfiscal, i.qtdeitensnf, 'EST' Origem ");
                    query.Append("     , Decode(n.StatusNF, 'C', 'Cancelada', 'A', 'Ativa', 'F', 'Fechada', 'Inutilizada') StatusNF");
                    query.Append("     , i.VLRCONFINSITNF, i.VLRPISITNF, i.VLRISSITNF, m.descricaomat descricao");
                    query.Append("  From Niff_Fis_Arquivei a");
                    query.Append("     , Bgm_Notafiscal N");
                    query.Append("     , est_itensnf I");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append("     , est_cadmaterial m");
                    query.Append(" Where n.chavedeacessonfe = a.chavedeacesso");
                    query.Append("   And m.codigomatint = i.codigomatint");
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Recebidos'");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    if (!dataEntrada)
                    {
                        query.Append("   And a.dataemissao Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.entradasaidanf Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.idempresa = " + IdEmpresa);
                    query.Append("   And n.codintnf = i.codintnf");
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') = em.codigoglobus");

                   // if (ApenasConferidas)
                     //   query.Append("   And a.Conferido = 'S'");
                    //query.Append("   And a.idarquivei = 62189");
                    query.Append(" Union All ");

                    query.Append("Select a.idarquivei, e.codintnf, e.valoricmsmatavulso valoricmsitensnf, e.aliqicmsmatavulso aliquotaicmsitensnf, e.valoricmssubstituicao valoricmssubsitensnf");
                    query.Append("     , e.valoripi valoripiitensnf, e.vlrdescontonfserv valordescontoitensnf, e.valorseguro valorseguroitensnf");
                    query.Append("     , e.vlroutrasdespnfserv vlroutrasdespesasitnf, e.valorfrete valorfreteitensnf, e.valornfservico valortotalitensnf");
                    query.Append("     , to_char(e.codsittributaria) codsittributaria, to_Char(e.codclassfisc) codclassfisc, e.codoperfiscal, e.qtdenfservico qtdeitensnf, 'EST' Origem");
                    query.Append("     , Decode(n.StatusNF, 'C', 'Cancelada', 'A', 'Ativa', 'F', 'Fechada', 'Inutilizada') StatusNF");
                    query.Append("     , e.VALORCOFINSNFSERV VLRCONFINSITNF, e.VALORPISNFSERV VLRPISITNF, e.VALORISSNFSERV VLRISSITNF");
                    query.Append("     , e.descricaonfservico descricao");
                    query.Append("  From Niff_Fis_Arquivei a");
                    query.Append("     , Bgm_Notafiscal N");
                    query.Append("     , Est_Nfservico e");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append(" Where n.chavedeacessonfe = a.chavedeacesso");
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Recebidos'");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    if (!dataEntrada)
                    {
                        query.Append("   And a.dataemissao Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.entradasaidanf Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.idempresa = " + IdEmpresa);
                    query.Append("   And n.codintnf = e.codintnf");
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') = em.codigoglobus");

                    //if (ApenasConferidas)
                        //query.Append("   And a.Conferido = 'S'");
                    //query.Append("   And a.idarquivei = 62189");

                    query.Append(" Union All ");

                    // busca as NF na escrituração fiscal
                    // alterado para buscar da tabela de NF da ESF. Se nao achar neste select irá buscar pelo documento na ESF

                    query.Append("Select a.Idarquivei, n.Coddoctoesf Codintnf, i.valoricms Valoricmsitensnf, i.aliqicms Aliquotaicmsitensnf");
                    query.Append("     , i.valoricmssubst Valoricmssubsitensnf, i.valoripi Valorpisnfserv, i.vlr_prop_desconto Valordescontoitensnf");
                    query.Append("     , i.vlr_prop_seguro Valorseguroitensnf, i.vlr_prop_outras Vlroutrasdespesasitnf");
                    query.Append("     , i.vlr_prop_frete Valorfreteitensnf, i.vlrtotal Valortotalitensnf");
                    query.Append("     , i.sittributaria Codsittributaria, i.cfop, i.codoperfiscal Codoperfiscal, i.qtde Qtdeitensnf, 'ESF' Origem");
                    query.Append("     , Decode(n.statusnf, 'C', 'Cancelada', 'Ativa') Statusnf, vl_cofins Vlrconfinsitnf, vl_pis Vlrpisitnf, 0 Vlrissitnf");
                    query.Append("     , i.descricao");
                    query.Append("  from Niff_Fis_Arquivei a, Esfnotafiscal n, esfnotafiscal_item i, Bgm_Fornecedor f");
                    query.Append("     , Ctr_Empautorizadas e, Ctr_Filial l, Niff_Fis_Parametrosarquivei p, Niff_Chm_Empresas Em");
                    query.Append(" Where n.codigoforn = f.codigoforn");
                    query.Append("   And n.chavedeacesso = a.chavedeacesso");
                    query.Append("   And e.codintempaut = l.codintempaut");
                    query.Append("   And l.Codigoempresa = n.codigoEmpresa");
                    query.Append("   And l.codigofl = n.codigofl");
                    query.Append("   And p.Idempresa = a.Idempresa");
                    query.Append("   And i.codintnf = n.codintnf");
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Recebidos'");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    if (!dataEntrada)
                    {
                        query.Append("   And a.dataemissao Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.datahoraentsai Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.idempresa = " + IdEmpresa);
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') = em.codigoglobus");

                    // if (ApenasConferidas)
                      // query.Append("   And a.Conferido = 'S'");
                    //query.Append("   And a.idarquivei = 62189");

                    query.Append(" Union all ");
                    query.Append("Select a.idarquivei, n.coddoctoesf codintnf, n.icmsvalorentra valoricmsitensnf, n.icmsaliqentra aliquotaicmsitensnf");
                    query.Append("     , n.icmssubstvalor valoricmssubsitensnf, n.ipivalorentra valorpisnfserv, 0 valordescontoitensnf, 0 valorseguroitensnf");
                    query.Append("     , 0 vlroutrasdespesasitnf, 0 valorfreteitensnf, n.vlcontabilentra valortotalitensnf");
                    query.Append("     , To_char(n.codsittributaria) codsittributaria, To_char(n.codclassfisc) codclassfisc");
                    query.Append("     , n.codoperfiscal_icmsentra codoperfiscal, 0 qtdeitensnf, n.sistema Origem, Decode(n.StatusEntra,'C', 'Cancelada','Ativa') StatusNF");
                    query.Append("     , 0 VLRCONFINSITNF, 0 VLRPISITNF, 0 VLRISSITNF, 'DACTE' Descricao");
                    query.Append("  From Niff_Fis_Arquivei a");
                    query.Append("     , esfentra N");
                    query.Append("     , Bgm_Fornecedor F");
                    query.Append("     , Ctr_Empautorizadas e");
                    query.Append("     , Ctr_Filial l");
                    query.Append("     , Niff_Fis_Parametrosarquivei p ");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append(" Where n.codigoforn = f.codigoforn");
                    query.Append("   And n.chavedeacesso = a.chavedeacesso");
                    query.Append("   And e.codintempaut = l.codintempaut");
                    query.Append("   And l.Codigoempresa = n.codigoEmpresa");
                    query.Append("   And l.codigofl = n.codigofl");
                    query.Append("   And p.Idempresa = a.Idempresa");
                    query.Append("   And n.Sistema <> 'EST'");
                    query.Append("   And n.CodTpDoc = 'DAC'");
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Recebidos'");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    if (!dataEntrada)
                    {
                        query.Append("   And a.dataemissao Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.dtentradaentra Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.idempresa = " + IdEmpresa);
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') = em.codigoglobus");

                    //if (ApenasConferidas)
                        //query.Append("   And a.Conferido = 'S'");
                    //query.Append("   And a.idarquivei = 62189");
                }
                else // Emitidos
                {
                    query.Append("Select a.idarquivei, i.codintnf, i.valoricmsitensnf, i.aliquotaicmsitensnf, i.valoricmssubsitensnf");
                    query.Append("     , i.valoripiitensnf, i.valordescontoitensnf, i.valorseguroitensnf");
                    query.Append("     , i.vlroutrasdespesasitnf, i.valorfreteitensnf, i.valortotalitensnf");
                    query.Append("     , to_char(i.codsittributaria) codsittributaria, to_char(i.codclassfisc) codclassfisc, i.codoperfiscal, i.qtdeitensnf, 'EST' Origem ");
                    query.Append("     , Decode(n.StatusNF, 'C', 'Cancelada', 'A', 'Ativa', 'F', 'Fechada', 'Inutilizada') StatusNF");
                    query.Append("     , i.VLRCONFINSITNF, i.VLRPISITNF, i.VLRISSITNF, m.descricaomat descricao");
                    query.Append("  From Niff_Fis_Arquivei a");
                    query.Append("     , Bgm_Notafiscal N");
                    query.Append("     , est_itensnf I");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append("     , est_cadmaterial m");
                    query.Append("     , Bgm_Cliente F");
                    query.Append("     , bgm_notafiscal_eletronica ne");
                    query.Append(" Where n.codcli = f.codcli");
                    query.Append("   And To_number(n.numeronf) = a.numeronf");
                    query.Append("   And (n.serienf = a.serie or n.serienf = Lpad(a.serie,3,'0'))");
                    query.Append("   And f.nrinscricaocli = Lpad(a.CnpjDestinatario, 14, '0') ");
                    query.Append("   And m.codigomatint = i.codigomatint");
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Emitidos'");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    query.Append("   And ne.Id_Nfe = a.Idnfeglobus");
                    query.Append("   And ne.codintnf_bgmnf = n.codintnf");
                    query.Append("   And ne.chavedeacesso = a.Chavedeacesso");

                    if (!dataEntrada)
                    {
                        query.Append("   And a.dataemissao Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.entradasaidanf Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.idempresa = " + IdEmpresa);
                    query.Append("   And n.codintnf = i.codintnf");
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') = em.codigoglobus");

                    //if (ApenasConferidas)
                      //  query.Append("   And a.Conferido = 'S'");
                    //query.Append("   And a.idarquivei = 62189");
                    query.Append(" Union All ");

                    query.Append("Select a.idarquivei, e.codintnf, e.valoricmsmatavulso valoricmsitensnf, e.aliqicmsmatavulso aliquotaicmsitensnf, e.valoricmssubstituicao valoricmssubsitensnf");
                    query.Append("     , e.valoripi valoripiitensnf, e.vlrdescontonfserv valordescontoitensnf, e.valorseguro valorseguroitensnf");
                    query.Append("     , e.vlroutrasdespnfserv vlroutrasdespesasitnf, e.valorfrete valorfreteitensnf, e.valornfservico valortotalitensnf");
                    query.Append("     , to_char(e.codsittributaria) codsittributaria, to_Char(e.codclassfisc) codclassfisc, e.codoperfiscal, e.qtdenfservico qtdeitensnf, 'EST' Origem");
                    query.Append("     , Decode(n.StatusNF, 'C', 'Cancelada', 'A', 'Ativa', 'F', 'Fechada', 'Inutilizada') StatusNF");
                    query.Append("     , e.VALORCOFINSNFSERV VLRCONFINSITNF, e.VALORPISNFSERV VLRPISITNF, e.VALORISSNFSERV VLRISSITNF");
                    query.Append("     , e.descricaonfservico descricao");
                    query.Append("  From Niff_Fis_Arquivei a");
                    query.Append("     , Bgm_Notafiscal N");
                    query.Append("     , Est_Nfservico e");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append("     , Bgm_Cliente F");
                    query.Append("     , bgm_notafiscal_eletronica ne");
                    query.Append(" Where n.codcli = f.codcli");
                    query.Append("   And To_number(n.numeronf) = a.numeronf");
                    query.Append("   And (n.serienf = a.serie or n.serienf = Lpad(a.serie,3,'0'))");
                    query.Append("   And f.nrinscricaocli = Lpad(a.CnpjDestinatario, 14, '0') ");
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Emitidos'");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    query.Append("   And ne.Id_Nfe = a.Idnfeglobus");
                    query.Append("   And ne.codintnf_bgmnf = n.codintnf");
                    query.Append("   And ne.chavedeacesso = a.Chavedeacesso");

                    if (!dataEntrada)
                    {
                        query.Append("   And a.dataemissao Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.entradasaidanf Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.idempresa = " + IdEmpresa);
                    query.Append("   And n.codintnf = e.codintnf");
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') = em.codigoglobus");

                    //if (ApenasConferidas)
                      //  query.Append("   And a.Conferido = 'S'");
                    //query.Append("   And a.idarquivei = 62189");

                    query.Append(" Union All ");

                    query.Append("Select a.idarquivei, e.codintnfavul codintnf, e.icmsitem valoricmsitensnf, e.aliquotaicmsitem aliquotaicmsitensnf, e.VALORICMSSUBSITEM valoricmssubsitensnf");
                    query.Append("     , e.ipiitem valoripiitensnf, e.valordescontoitem valordescontoitensnf, 0 valorseguroitensnf");
                    query.Append("     , 0 vlroutrasdespesasitnf, e.valorfreteitem valorfreteitensnf, e.vlrunititem * e.qtdeitem valortotalitensnf");
                    query.Append("     , to_char(e.codsittributaria) codsittributaria, to_Char(e.codclassfisc) codclassfisc, e.codoperfiscal, e.qtdeitem qtdeitensnf, 'EST' Origem");
                    query.Append("     , Decode(n.StatusNF, 'C', 'Cancelada', 'A', 'Ativa', 'F', 'Fechada', 'Inutilizada') StatusNF");
                    query.Append("     , 0 VLRCONFINSITNF, 0 VLRPISITNF, 0 VLRISSITNF");
                    query.Append("     , m.descricaomatavulso descricao");
                    query.Append("  From Niff_Fis_Arquivei a");
                    query.Append("     , Est_Materialavulsonf N");
                    query.Append("     , Est_Itensmaterialavulsonf e");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append("     , Bgm_Cliente F,        Est_Cadmaterialavulso     m");
                    query.Append("     , bgm_notafiscal_eletronica ne");
                    query.Append(" Where n.codcli = f.codcli");
                    query.Append("   And To_number(n.numeronf) = a.numeronf");
                    query.Append("   And (n.serienf = a.serie or n.serienf = Lpad(a.serie,3,'0'))");
                    query.Append("   And f.nrinscricaocli = Lpad(a.CnpjDestinatario, 14, '0') ");
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Emitidos'");
                    query.Append("   And m.codigomatavulso = e.codigomatavulso");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    query.Append("   And ne.Id_Nfe = a.Idnfeglobus");
                    query.Append("   And ne.codintnfavul = n.codintnfavul");
                    query.Append("   And ne.chavedeacesso = a.Chavedeacesso");

                    if (!dataEntrada)
                    {
                        query.Append("   And a.dataemissao Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.datanf Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.idempresa = " + IdEmpresa);
                    query.Append("   And n.Codintnfavul = e.Codintnfavul");
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') = em.codigoglobus");

                    //if (ApenasConferidas)
                        //query.Append("   And a.Conferido = 'S'");
                    //query.Append("   And a.idarquivei = 62189");

                    query.Append(" Union All ");

                    // busca as NF na escrituração fiscal
                    // alterado para buscar da tabela de NF da ESF. Se nao achar neste select irá buscar pelo documento na ESF
                    query.Append("Select a.idarquivei, n.coddoctoesf codintnf, i.valoricms valoricmsitensnf, i.aliqicms aliquotaicmsitensnf");
                    query.Append("     , i.valoricmssubst valoricmssubsitensnf, 0 valorpisnfserv, i.vlr_prop_desconto valordescontoitensnf, i.vlr_prop_seguro valorseguroitensnf");
                    query.Append("     , i.vlr_prop_outras vlroutrasdespesasitnf, i.vlr_prop_frete valorfreteitensnf, i.vlrtotal valortotalitensnf");
                    query.Append("     , To_char(i.sittributaria) codsittributaria, To_char(i.cfop) codclassfisc");
                    query.Append("     , i.codoperfiscal codoperfiscal, i.qtde qtdeitensnf, n.sistema Origem, Decode(n.Statussaida,'C', 'Cancelada','Ativa') StatusNF");
                    query.Append("     , i.vl_cofins VLRCONFINSITNF, i.vl_pis VLRPISITNF, 0 VLRISSITNF, i.descricao Descricao");
                    query.Append("  From Niff_Fis_Arquivei a");
                    query.Append("     , esfSaida N");
                    query.Append("     , Bgm_cliente F");
                    query.Append("     , Ctr_Empautorizadas e");
                    query.Append("     , Ctr_Filial l");
                    query.Append("     , Niff_Fis_Parametrosarquivei p ");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append("     , bgm_notafiscal_eletronica ne");
                    query.Append("     , Esfnotafiscal ns");
                    query.Append("     , esfnotafiscal_item i");
                    query.Append(" Where n.codcli = f.codcli");
                    query.Append("   And LPad(n.nrdocsaida, 10, '0') = LPad(a.numeronf, 10, '0')");
                    query.Append("   And (n.seriesaida = a.serie or n.seriesaida = lPad(a.serie,3,'0'))");
                    query.Append("   And f.nrinscricaocli = Lpad(a.CnpjDestinatario, 14, '0') ");
                    query.Append("   And e.codintempaut = l.codintempaut");
                    query.Append("   And l.Codigoempresa = n.codigoEmpresa");
                    query.Append("   And l.codigofl = n.codigofl");
                    query.Append("   And p.Idempresa = a.Idempresa");
                    query.Append("   And n.Sistema <> 'EST'");                    
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Emitidos'");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    query.Append("   And ne.Id_Nfe = a.Idnfeglobus");
                    query.Append("   And ne.codintnf_esfnf = ns.codintnf");
                    query.Append("   And ne.chavedeacesso = a.Chavedeacesso");
                    query.Append("   And ns.coddoctoesf = n.coddoctoesf");
                    query.Append("   And i.codintnf = NS.Codintnf");

                    if (!dataEntrada)
                    {
                        query.Append("   And a.dataemissao Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.dtSaidaSaida Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.idempresa = " + IdEmpresa);
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || Lpad(n.codigofl, 3, '0') = em.codigoglobus");

                    //if (ApenasConferidas)
                        //query.Append("   And a.Conferido = 'S'");
                    //query.Append("   And a.idarquivei = 62189");
                }

                Query executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ItensComparacao _tipo = new ItensComparacao();

                        _tipo.IdArquivei = Convert.ToInt32(dataReader["idArquivei"].ToString());
                        _tipo.CodIntNf = Convert.ToInt32(dataReader["CodIntNF"].ToString());

                        _tipo.Descricao = dataReader["Descricao"].ToString();

                        try
                        {
                            _tipo.Quantidade = Math.Round(Convert.ToDecimal(dataReader["qtdeitensnf"].ToString()), 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorICMSGlobus = Math.Round(Convert.ToDecimal(dataReader["valoricmsitensnf"].ToString()),2);
                        }
                        catch { }

                        try
                        {
                            _tipo.AliquotaICMSGlobus = Math.Round(Convert.ToDecimal(dataReader["aliquotaicmsitensnf"].ToString()), 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorICMSSubGlobus = Math.Round(Convert.ToDecimal(dataReader["valoricmssubsitensnf"].ToString()) * _tipo.Quantidade, 2);
                            //_tipo.ValorICMSSubGlobus = _tipo.ValorICMSSubGlobus * _tipo.Quantidade;
                        }
                        catch { }
                         
                        try
                        {
                            _tipo.ValorIPIGlobus = Math.Round(Convert.ToDecimal(dataReader["valoripiitensnf"].ToString()) * _tipo.Quantidade, 2);
                            //_tipo.ValorIPIGlobus = _tipo.ValorIPIGlobus * _tipo.Quantidade;
                        }
                        catch { }

                        try
                        {
                            //_tipo.DescontoGlobus = Math.Round(Convert.ToDecimal(dataReader["valordescontoitensnf"].ToString()) , 2);
                            // tirado a multiplicação pela quantidade pelo chamado 202008-0065 - 07/08/2020

                            // Retornado a multiplicação pela quantidade pelo chamado 202008-0306 - 28/08/2020
                            _tipo.DescontoGlobus = Math.Round(Convert.ToDecimal(dataReader["valordescontoitensnf"].ToString()) * _tipo.Quantidade, 2);
                            
                        }
                        catch { }

                        try
                        {
                            _tipo.SeguroGlobus = Math.Round(Convert.ToDecimal(dataReader["valorseguroitensnf"].ToString()) * _tipo.Quantidade, 2);
                            //_tipo.SeguroGlobus = _tipo.SeguroGlobus * _tipo.Quantidade;
                        }
                        catch { }

                        try
                        {
                            _tipo.OutrasDespesasGlobus = Math.Round(Convert.ToDecimal(dataReader["vlroutrasdespesasitnf"].ToString()) * _tipo.Quantidade, 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorFreteGlobus = Math.Round(Convert.ToDecimal(dataReader["valorfreteitensnf"].ToString()) * _tipo.Quantidade, 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorCofinsGlobus = Math.Round(Convert.ToDecimal(dataReader["VLRCONFINSITNF"].ToString()) * _tipo.Quantidade, 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorPisGlobus = Math.Round(Convert.ToDecimal(dataReader["VLRPISITNF"].ToString()) * _tipo.Quantidade, 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorISSGlobus = Math.Round(Convert.ToDecimal(dataReader["VLRISSITNF"].ToString()) * _tipo.Quantidade, 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorTotalGlobus = Math.Round(Convert.ToDecimal(dataReader["valortotalitensnf"].ToString()), 2) - 
                                (_tipo.ValorFreteGlobus + 
                                _tipo.ValorIPIGlobus + 
                                _tipo.SeguroGlobus +
                                _tipo.OutrasDespesasGlobus +
                                _tipo.ValorICMSSubGlobus) + _tipo.DescontoGlobus;
                        }
                        catch { }

                        try
                        {
                            _tipo.CSTGlobus = Convert.ToInt32(dataReader["codsittributaria"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.OperacaoGlobus = Convert.ToInt32(dataReader["codoperfiscal"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CFOPGlobus = Convert.ToInt32(dataReader["codclassfisc"].ToString());
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
    }
}
