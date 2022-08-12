using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class NotaFiscalServicoDAO
    {
        IDataReader dataReader;
        IDataReader dataReader2;
        IDataReader impostoReader;

        // parametros codigo servico
        #region parametros

        public List<ParametrosCodigoServico> Listar(int idEmpresa, bool servicoUnico)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ParametrosCodigoServico> lista = new List<ParametrosCodigoServico>();
            Publicas.mensagemDeErro = string.Empty;
            
            try
            {
                if (servicoUnico)
                    query.Append("Select distinct idempresa, codigoglobus");
                else
                    query.Append("Select id, idempresa, codigoxml, codigoglobus");
                query.Append("  From niff_fis_ParamCodServico ");
                query.Append(" Where Idempresa = " + idEmpresa);
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ParametrosCodigoServico _tipo = new ParametrosCodigoServico();

                        _tipo.Existe = true;
                        if (!servicoUnico)
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                            _tipo.CodigoServicoXML = dataReader["codigoxml"].ToString();
                        }
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        
                        _tipo.CodigoServicoGlobus = dataReader["codigoglobus"].ToString();                        

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

        public ParametrosCodigoServico Consultar(int idEmpresa, string codigoXml)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            ParametrosCodigoServico _tipo = new ParametrosCodigoServico();

            Publicas.mensagemDeErro = string.Empty;            

            try
            {
                query.Append("Select id, idempresa, codigoxml, codigoglobus");
                query.Append("  From niff_fis_ParamCodServico ");
                query.Append(" Where Idempresa = " + idEmpresa);
                query.Append("   and codigoxml = '" + codigoXml + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.CodigoServicoXML = dataReader["codigoxml"].ToString();
                        _tipo.CodigoServicoGlobus = dataReader["codigoglobus"].ToString();
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
                
        public bool Gravar(List<ParametrosCodigoServico> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = _lista.Count() == 0;

            try
            {
                foreach (var item in _lista)
                {
                    query.Clear();
                    if (!item.Existe)
                    {
                        query.Append("Insert into niff_fis_ParamCodServico");
                        query.Append(" ( id, idempresa, codigoxml, codigoglobus");
                        query.Append(") Values ( (Select nvl(Max(id),0) +1 from niff_fis_ParamCodServico) ");
                        query.Append("        , " + item.IdEmpresa);
                        query.Append("        , '" + item.CodigoServicoXML + "'");
                        query.Append("        , '" + item.CodigoServicoGlobus + "'");

                        query.Append(" )");
                    }
                    else
                    {
                        query.Append("Update niff_fis_ParamCodServico");
                        query.Append("   set codigoglobus = '" + item.CodigoServicoGlobus + "'");

                        query.Append(" Where Id = " + item.Id);
                    }

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

        public bool ExcluirItem(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Delete niff_fis_ParamCodServico");
                query.Append(" Where Id = " + id);

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

        public bool ExcluirTudo(int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Delete niff_fis_ParamCodServico");
                query.Append(" Where idEmpresa = " + idEmpresa);

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

        #endregion


        public List<Arquivei> ListarXmlNFSe(string empresa, DateTime inicial, DateTime final)
        {

            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Arquivei> _lista = new List<Arquivei>();
            Publicas.mensagemDeErro = string.Empty;


            try
            {
                query.Append("Select a.NumeroNF, a.Serie, a.DataEmissao, a.naturezaoperacao");
                query.Append("     , a.BaseICMS, a.ValorTotalNF, a.chavedeacesso, a.DATAIMPORTADO");
                query.Append("     , a.ModeloNF ModeloArquivo");
                query.Append("     , formatacnpjcpf(Lpad(a.Cnpjemitente, 14, '0'), 'CNPJ') cnpjFornecedor");
                query.Append("     , a.razaosocialemitente");
                query.Append("     , formatacnpjcpf(Lpad(a.Cnpjdestinatario, 14, '0'), 'CNPJ') cnpjEmpresa ");
                query.Append("     , a.razaosocialdestinatario RSocialEmpresaArquivo");
                query.Append("     , a.IdEmpresa, a.idArquivei, a.Tipo, a.Status, a.Operacao, a.DadosAdicionais, a.ComDiferencas");
                query.Append("     , a.valorproduto");
                query.Append("     , a.TipoArquivo");
                query.Append("     , a.municorigem, a.municdestino, d.DescMunic cidadeforn");

                query.Append("     , formatacnpjcpf(Lpad(a.CnpjTomador, 14, '0'), 'CNPJ') cnpjTomador ");
                query.Append("     , eliminanaonumericos(a.ieTomador) IETomador");
                query.Append("     , a.razaosocialTomador RSocialTomador");
                query.Append("     , a.enderecoTomador EnderecoTomador");
                query.Append("     , a.numeroendTomador NumeroEndTomador");
                query.Append("     , a.bairroTomador bairroTomador");
                query.Append("     , eliminanaonumericos(a.cepTomador) cepTomador");
                query.Append("     , a.datacancelamento, a.codigoservico, a.discriminacao");
                query.Append("     , a.valortotalnf, a.valorservico, a.valorcredito");
                query.Append("     , a.valoriss, a.valorpis, a.valorcofins, a.valorir, a.ValorInss");
                query.Append("     , a.valorcsll, a.aliquotaservico, a.ISSREtido");
                query.Append("     , a.ComentarioUsuario");
                query.Append("  From niff_fis_arquivei a, niff_chm_empresas e, Bgm_Fornecedor F, DVS_MUNICIPIO d");
                query.Append(" Where e.Idempresa = a.Idempresa");

                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("           And e.codigoglobus in ('001/001','001/004')");
                else
                    query.Append("   And e.codigoglobus = '" + empresa + "'");

                query.Append("   And a.tipodocto = 'NFSe'");
                query.Append("   And f.nrinscricaoforn = Lpad(a.Cnpjemitente, 14, '0')");
                query.Append("  And a.Dataemissao between To_date('" + inicial.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + final.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("   And f.condicaoforn <> 'I'");
                query.Append("   And f.CodMunic = d.CodMunic(+)");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Arquivei _tipo = new Arquivei();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idArquivei"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.CNPJDestinatario = dataReader["Cnpjempresa"].ToString();
                        _tipo.RazaoSocialDestinatario = dataReader["Rsocialempresaarquivo"].ToString();
                        _tipo.CNPJEmitente = dataReader["Cnpjfornecedor"].ToString();
                        _tipo.RazaoSocialEmitente = dataReader["Razaosocialemitente"].ToString();
                        _tipo.CidadeEmitente = dataReader["cidadeforn"].ToString();
                        _tipo.Tipo = dataReader["Tipo"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.StatusOld = dataReader["Status"].ToString();
                        _tipo.Operacao = dataReader["Operacao"].ToString();
                        _tipo.ComentarioUsuario = dataReader["ComentarioUsuario"].ToString();
                        _tipo.TipoDocto = "NFSe";

                        _tipo.ChaveDeAcesso = dataReader["ChaveDeAcesso"].ToString();
                        _tipo.NumeroNF = Convert.ToInt32(dataReader["NumeroNF"].ToString());
                        
                        _tipo.Discriminacao = dataReader["Discriminacao"].ToString();

                        _tipo.CodigoServico = dataReader["CodigoServico"].ToString();
                        
                        _tipo.ValorTotalNF = Convert.ToDecimal(dataReader["ValorTotalNF"].ToString());
                        _tipo.ValorProduto = Convert.ToDecimal(dataReader["ValorProduto"].ToString());

                        _tipo.ValorServico = Convert.ToDecimal(dataReader["ValorServico"].ToString());
                        _tipo.AliquotaServico = Convert.ToDecimal(dataReader["AliquotaServico"].ToString());
                        _tipo.ValorISS = Convert.ToDecimal(dataReader["ValorISS"].ToString());
                        _tipo.ValorCredito = Convert.ToDecimal(dataReader["ValorCredito"].ToString());
                        _tipo.ValorPis = Convert.ToDecimal(dataReader["ValorPis"].ToString());
                        _tipo.ValorCofins = Convert.ToDecimal(dataReader["ValorCofins"].ToString());
                        _tipo.ValorIR = Convert.ToDecimal(dataReader["ValorIR"].ToString());
                        _tipo.ValorCSLL = Convert.ToDecimal(dataReader["ValorCSLL"].ToString());
                        _tipo.ValorINSS = Convert.ToDecimal(dataReader["ValorINSS"].ToString());
                        
                        
                        try
                        {
                            _tipo.DataEmissao = Convert.ToDateTime(dataReader["DataEmissao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataCancelamento = Convert.ToDateTime(dataReader["DataCancelamento"].ToString());
                        }
                        catch { }

                        
                        _tipo.CNPJTomador = dataReader["cnpjTomador"].ToString();
                        _tipo.IETomador = dataReader["IETomador"].ToString();
                        _tipo.RazaoSocialTomador = dataReader["RSocialTomador"].ToString();
                        _tipo.EnderecoTomador = dataReader["EnderecoTomador"].ToString();
                        _tipo.NumeroEndTomador = dataReader["NumeroEndTomador"].ToString();
                        _tipo.BairroTomador = dataReader["bairroTomador"].ToString();
                        _tipo.CEPTomador = dataReader["cepTomador"].ToString();

                        _tipo.TipoTomadorDestinatario = "Destinatário";


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

        public List<NotasFiscaisServico> ListarNotaFiscais(string tipoDocumento, string empresa, string tipo, DateTime inicial, DateTime final, bool integradas, int idParametro)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<NotasFiscaisServico> _lista = new List<NotasFiscaisServico>();
            Publicas.mensagemDeErro = string.Empty;


            decimal PercBaseCalculo = 0;
            decimal baseCalculoCofins = 0;
            decimal aliquotaCofins = 0;
            decimal cofins = 0;

            decimal baseCalculoPis = 0;
            decimal aliquotaPis = 0;
            decimal pis = 0;

            decimal baseCalculoCsll = 0;
            decimal aliquotaCsll = 0;
            decimal csll = 0;
            
            decimal baseCalculoIr = 0;
            decimal aliquotaIr = 0;
            decimal ir = 0;

            decimal baseCalculoIss = 0;
            decimal aliquotaIss = 0;
            decimal iss = 0;

            decimal baseCalculoInss = 0;
            decimal aliquotaInss = 0;
            decimal inss = 0;

            try
            {
                query.Append("Select Distinct n.codintnf, n.numeronf, n.entradasaidanf, n.dataemissaonf, n.codigoforn, n.serienf, n.valortotalnf, n.aliqpisnf, n.vlrpisnf");
                query.Append("     , n.aliqconfinsnf, n.vlrconfinsnf, n.aliqinssnf, n.vlrinssnf, n.aliqissnf, n.vlrissnf, n.aliqirnf, n.vlraliqnf ValorIR");
                query.Append("     , n.aliqcsllnf, n.vlrcsllnf, n.codclassfisc, v.datavenctonf");
                query.Append("     , f.nrforn || ' - ' || f.rsocialforn Fornecedor");
                query.Append("     , formatacnpjcpf(f.Nrinscricaoforn, 'CNPJ') CNPJForncedorGlobus");
                query.Append("     , n.codigoEmpresa, n.codigofl, n.coddoctocpg");
                query.Append("     , basepisnf, baseconfinsnf, baseinssnf, baseissnf, baseirnf, basecsllnf");

                query.Append("     , a.Idarquivei, a.numeronf NumeroNfXml, formatacnpjcpf(Lpad(a.Cnpjemitente, 14, '0'), 'CNPJ') cnpjFornecedorXml");
                query.Append("     , a.Dataemissao DataemissaoXml, a.datacancelamento");
                query.Append("     , a.razaosocialemitente, a.chavedeacesso, a.valortotalnf valortotalnfXml, a.valorservico, a.valorcredito");
                query.Append("     , a.valoriss valorissXml, a.valorpis valorpisXml, a.valorcofins valorcofinsXml, a.ValorInss valorInssXml");
                query.Append("     , a.valorir valorirXml, a.valorcsll valorcsllXml, a.aliquotaservico aliquotaservicoXml, a.ISSREtido ISSREtidoXml");
                query.Append("     , a.discriminacao, a.codigoservico, d.DescMunic cidadeforn");

                query.Append("  From Bgm_Notafiscal n");
                query.Append("     , Bgm_Fornecedor F, DVS_MUNICIPIO d");
                query.Append("     , (Select Min(datavenctonf) datavenctonf, codintnf From EST_VENCTONF Group By CodintNf) v");
                query.Append("     , (Select a.Idarquivei, a.numeronf, a.cnpjemitente");
                query.Append("             , a.Dataemissao, a.datacancelamento, a.codigoservico");
                query.Append("             , a.razaosocialemitente, a.chavedeacesso, a.discriminacao");
                query.Append("             , a.valortotalnf, a.valorservico, a.valorcredito");
                query.Append("             , a.valoriss, a.valorpis, a.valorcofins, a.valorir, a.ValorInss");
                query.Append("             , a.valorcsll, a.aliquotaservico, a.ISSREtido");
                query.Append("          From niff_fis_arquivei a, niff_chm_empresas e");
                query.Append("         Where e.Idempresa = a.Idempresa");
                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("           And e.codigoglobus in ('001/001','001/004')");
                else
                    query.Append("           And e.codigoglobus = '" + empresa + "'");

                query.Append("          And a.tipodocto = 'NFSe') a"); 
                 
                query.Append(" Where n.codtpdoc = '" + tipoDocumento + "'");
                query.Append("   And n.dataemissaonf between To_date('" + inicial.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + final.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("   And n.tipooperacaonf = 'E'");
                query.Append("   And V.Codintnf = n.codintnf"); 
                 
                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || lPad(n.codigofl,3,'0') in ('001/001','001/004')");
                else
                    query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || lPad(n.codigofl,3,'0') = '" + empresa + "'");

                query.Append("   And n.lanctointegradoesf = '" + (integradas ? "S" : "N") + "'");
                query.Append("   And f.codigoforn = n.codigoforn");
                query.Append("   And n.codintnf not in (Select codintnf From niff_esf_issintegrado)");
                query.Append("   And n.serienf <> 'PNEU'");

                query.Append("   And a.NumeroNf(+) = To_number(n.Numeronf)");
                query.Append("   And Lpad(a.Cnpjemitente(+), 14, '0') = f.Nrinscricaoforn");
                query.Append("   And f.condicaoforn <> 'I'");
                query.Append("   And f.CodMunic = d.CodMunic(+)");
                Query executar = sessao.CreateQuery(query.ToString());

                 dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        NotasFiscaisServico nota = new NotasFiscaisServico();

                        nota.CodigoInternoNotaFiscal = Convert.ToDecimal(dataReader["codintnf"].ToString());
                        nota.CodigoFornecedor = Convert.ToInt32(dataReader["codigoforn"].ToString());
                        nota.CFOP = Convert.ToInt32(dataReader["codclassfisc"].ToString());
                        nota.CodigoEmpresa = Convert.ToInt32(dataReader["CodigoEmpresa"].ToString());
                        nota.CodigoFilial= Convert.ToInt32(dataReader["CodigoFl"].ToString());
                        
                        nota.DataEmissao = Convert.ToDateTime(dataReader["dataemissaonf"].ToString());
                        nota.DataEntrada = Convert.ToDateTime(dataReader["entradasaidanf"].ToString());
                        nota.DataVencimento = Convert.ToDateTime(dataReader["datavenctonf"].ToString());

                        try
                        {
                            nota.DataCancelamento = Convert.ToDateTime(dataReader["DataCancelamento"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.DataEmissaoXml = Convert.ToDateTime(dataReader["DataemissaoXml"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.IdArquivei = Convert.ToDecimal(dataReader["Idarquivei"].ToString());
                        }
                        catch { } 

                        nota.Fornecedor = dataReader["Fornecedor"].ToString();
                        nota.CidadeFornecedor = dataReader["CIDADEFORN"].ToString();
                        
                        nota.NumeroNotaFiscal = dataReader["numeronf"].ToString();
                        nota.CNPJFornecedor = dataReader["CNPJForncedorGlobus"].ToString();

                        nota.FornecedorXml = dataReader["razaosocialemitente"].ToString();
                        nota.CNPJFornecedorXml = dataReader["cnpjFornecedorXml"].ToString();
                        nota.DiscriminacaoXml = dataReader["Discriminacao"].ToString();
                        nota.CodigoServico = dataReader["CodigoServico"].ToString();

                        nota.Serie = dataReader["serienf"].ToString();
                        nota.IntegradaCPG = dataReader["CodDoctoCPG"].ToString() != "";
                        
                        try
                        {
                            nota.CodigoDoctoCPG = Convert.ToDecimal(dataReader["CodDoctoCPG"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorTotal = Convert.ToDecimal(dataReader["valortotalnf"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorTotalXml = Convert.ToDecimal(dataReader["valortotalnfXml"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.AliquotaPIS = Convert.ToDecimal(dataReader["aliqpisnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaCofins = Convert.ToDecimal(dataReader["aliqconfinsnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaINSS = Convert.ToDecimal(dataReader["aliqinssnf"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.AliquotaISS = Convert.ToDecimal(dataReader["aliqissnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorISS = Convert.ToDecimal(dataReader["vlrissnf"].ToString());
                        }
                        catch { }

                        nota.IssRetidoXml = dataReader["ISSREtidoXml"].ToString() == "S";

                        if (nota.IssRetidoXml)
                        {
                            
                            try
                            {
                                nota.AliquotaISSXml = Convert.ToDecimal(dataReader["aliquotaservicoXml"].ToString());
                            }
                            catch { }
                           

                            try
                            {
                                nota.ValorISSXml = Convert.ToDecimal(dataReader["valorissXml"].ToString());
                            }
                            catch { }
                        }
                        try
                        {
                            nota.AliquotaIR = Convert.ToDecimal(dataReader["aliqirnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaCSLL = Convert.ToDecimal(dataReader["aliqcsllnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorPIS = Convert.ToDecimal(dataReader["vlrpisnf"].ToString());
                        }   
                        catch { }
                        try
                        {
                            nota.ValorPISXml = Convert.ToDecimal(dataReader["valorpisXml"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorCofins = Convert.ToDecimal(dataReader["vlrconfinsnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorCofinsXml = Convert.ToDecimal(dataReader["valorcofinsXml"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorINSS = Convert.ToDecimal(dataReader["vlrinssnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorINSSXml = Convert.ToDecimal(dataReader["valorInssXml"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            nota.ValorIR = Convert.ToDecimal(dataReader["ValorIR"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorIRXml = Convert.ToDecimal(dataReader["valorirXml"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorCSLL = Convert.ToDecimal(dataReader["vlrcsllnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorCSLLXml = Convert.ToDecimal(dataReader["valorcsllXml"].ToString());
                        }
                        catch { }


                        try
                        {
                            nota.ValorLiquido = nota.ValorTotal - nota.ValorPIS - nota.ValorISS - nota.ValorIR - nota.ValorINSS - nota.ValorCSLL - nota.ValorCofins;
                        }
                        catch { }
                        
                        try
                        {
                            nota.ValorLiquidoSemImpostos = nota.ValorTotal - nota.ValorISS;
                        }
                        catch { }

                        try
                        {
                            nota.ValorLiquidoXml = nota.ValorTotalXml - nota.ValorPISXml - nota.ValorISSXml - nota.ValorIRXml - nota.ValorINSSXml - nota.ValorCSLLXml - nota.ValorCofinsXml;
                        }
                        catch { } 

                        try
                        {
                            nota.BasePIS = Convert.ToDecimal(dataReader["BASEPISNF"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseCofins = Convert.ToDecimal(dataReader["BASECONFINSNF"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseINSS = Convert.ToDecimal(dataReader["BASEINSSNF"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseISS = Convert.ToDecimal(dataReader["BASEISSNF"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseIR = Convert.ToDecimal(dataReader["BASEIRNF"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseCSLL = Convert.ToDecimal(dataReader["BASECSLLNF"].ToString());
                        }
                        catch { }

                        string sqlVlComplementarPadrao = "Select w.codgrupovalores, a.codvalores, v.descricao, Nvl(v.percentual_aliquota,0) percentual_aliquota, " +
                        "Nvl(v.percentual_calc_base,0) percentual_calc_base, v.imposto_retido" +
                        "  From Est_Parametro w, cpgvlragrupamento a, cpgvalores v" +
                        " Where w.codigoempresa = " + nota.CodigoEmpresa.ToString() +
                        "   And w.codigoFl = " + nota.CodigoFilial.ToString() +
                        "   And a.codgrupovalores = w.codgrupovalores" +
                        "   And a.codvalores = v.codvalores";

                        #region Cofins
                        cofins = 0;
                        baseCalculoCofins = 0;
                        aliquotaCofins = 0;

                        query.Clear();

                        query.Append(sqlVlComplementarPadrao);
                        query.Append("   And v.descricao Like 'COFINS%' ");
                        executar = sessao.CreateQuery(query.ToString());
                        impostoReader = executar.ExecuteQuery();

                        using (impostoReader)
                        {
                            if (impostoReader.Read())
                            {
                                nota.CodigoCofins = Convert.ToInt32(impostoReader["codvalores"].ToString());
                                nota.GrupoCofins = Convert.ToInt32(impostoReader["codgrupovalores"].ToString());
                                nota.CofinsRetido = impostoReader["imposto_retido"].ToString() == "S";
                                PercBaseCalculo = Convert.ToDecimal(impostoReader["percentual_calc_base"].ToString());
                            }
                        }

                        if (nota.BaseCofins != 0 && nota.ValorCofins != 0 && nota.AliquotaCofins == 0)
                        {
                            aliquotaCofins = Math.Round((nota.ValorCofins / baseCalculoCofins) * 100, 2);
                            nota.AliquotaPIS = aliquotaPis;
                        }
                        else
                        {
                            if (nota.ValorCofins != 0)
                            {
                                if (nota.BaseCofins != 0)
                                    baseCalculoCofins = nota.BaseCofins;
                                else
                                {
                                    if (PercBaseCalculo != 100 && PercBaseCalculo != 0)
                                    {
                                        baseCalculoCofins = nota.ValorTotal * (PercBaseCalculo / 100);
                                        nota.BaseCofins = baseCalculoCofins;
                                    }
                                    else
                                        baseCalculoCofins = nota.ValorTotal;
                                }
                                aliquotaCofins = nota.AliquotaCofins;
                                if (nota.AliquotaCofins == 0)
                                {
                                    aliquotaCofins = Math.Round((nota.ValorCofins / baseCalculoCofins) * 100, 2);
                                    nota.AliquotaCofins = aliquotaCofins;
                                }

                                cofins = Math.Round(baseCalculoCofins * (aliquotaCofins / 100), 2);
                                nota.ValorCofins = cofins;
                            }
                        }

                        #endregion

                        #region PIS
                        pis = 0;
                        baseCalculoPis = 0;
                        aliquotaPis = 0;

                        query.Clear();

                        query.Append(sqlVlComplementarPadrao);
                        query.Append("   And v.descricao Like 'PIS%' ");
                        executar = sessao.CreateQuery(query.ToString());
                        impostoReader = executar.ExecuteQuery();

                        using (impostoReader)
                        {
                            if (impostoReader.Read())
                            {
                                nota.CodigoPis = Convert.ToInt32(impostoReader["codvalores"].ToString());
                                nota.GrupoPis = Convert.ToInt32(impostoReader["codgrupovalores"].ToString());
                                nota.PisRetido = impostoReader["imposto_retido"].ToString() == "S";
                                PercBaseCalculo = Convert.ToDecimal(impostoReader["percentual_calc_base"].ToString());
                            }
                        }

                        if (nota.BasePIS != 0 && nota.ValorPIS != 0 && nota.AliquotaPIS == 0)
                        {
                            aliquotaPis = Math.Round((nota.ValorPIS / baseCalculoPis) * 100, 2);
                            nota.AliquotaPIS = aliquotaPis;
                        }
                        else
                        {
                            if (nota.ValorPIS != 0)
                            {
                                if (nota.BasePIS != 0)
                                    baseCalculoPis = nota.BasePIS;
                                else
                                {
                                    if (PercBaseCalculo != 100 && PercBaseCalculo != 0)
                                    {
                                        baseCalculoPis = nota.ValorTotal * (PercBaseCalculo / 100);
                                        nota.BasePIS = baseCalculoPis;
                                    }
                                    else
                                        baseCalculoPis = nota.ValorTotal;
                                }

                                aliquotaPis = nota.AliquotaPIS;
                                if (nota.AliquotaPIS == 0)
                                {
                                    aliquotaPis = Math.Round((nota.ValorPIS / baseCalculoPis) * 100, 2);
                                    nota.AliquotaPIS = aliquotaPis;
                                }
                                pis = Math.Round(baseCalculoPis * (aliquotaPis / 100), 2);
                                nota.ValorPIS = pis;
                            }
                        }
                        #endregion

                        #region IR
                        ir = 0;
                        baseCalculoIr = 0;
                        aliquotaIr = 0;

                        query.Clear();

                        query.Append(sqlVlComplementarPadrao);
                        query.Append("   And v.descricao Like 'IR%' ");
                        executar = sessao.CreateQuery(query.ToString());
                        impostoReader = executar.ExecuteQuery();

                        using (impostoReader)
                        {
                            if (impostoReader.Read())
                            {
                                nota.CodigoIR = Convert.ToInt32(impostoReader["codvalores"].ToString());
                                nota.GrupoIR = Convert.ToInt32(impostoReader["codgrupovalores"].ToString());
                                nota.IRRetido = impostoReader["imposto_retido"].ToString() == "S";
                                PercBaseCalculo = Convert.ToDecimal(impostoReader["percentual_calc_base"].ToString());
                            }
                        }

                        if (nota.BaseIR != 0 && nota.ValorIR != 0 && nota.AliquotaIR == 0)
                        {
                            aliquotaIr = Math.Round((nota.ValorIR / baseCalculoIr) * 100, 2);
                            nota.AliquotaIR = aliquotaIr;
                        }
                        else
                        {
                            if (nota.ValorIR != 0)
                            {
                                if (nota.BaseIR != 0)
                                    baseCalculoIr = nota.BaseIR;
                                else
                                {
                                    if (PercBaseCalculo != 100 && PercBaseCalculo != 0)
                                    {
                                        baseCalculoIr = nota.ValorTotal * (PercBaseCalculo / 100);
                                        nota.BaseIR = baseCalculoIr;
                                    }
                                    else
                                        baseCalculoIr = nota.ValorTotal;
                                }

                                aliquotaIr = nota.AliquotaIR;
                                if (nota.AliquotaIR == 0)
                                {
                                    aliquotaIr = Math.Round((nota.ValorIR / baseCalculoIr) * 100, 2);
                                    nota.AliquotaIR = aliquotaIr;
                                }
                                ir = Math.Round(baseCalculoIr * (aliquotaIr / 100), 2);
                                nota.ValorIR = ir;
                            }
                        }
                        #endregion

                        #region Csll
                        csll = 0;
                        baseCalculoCsll = 0;
                        aliquotaCsll = 0;

                        query.Clear();

                        query.Append(sqlVlComplementarPadrao);
                        query.Append("   And v.descricao Like 'CSL%' ");
                        executar = sessao.CreateQuery(query.ToString());
                        impostoReader = executar.ExecuteQuery();

                        using (impostoReader)
                        {
                            if (impostoReader.Read())
                            {
                                nota.CodigoCsll = Convert.ToInt32(impostoReader["codvalores"].ToString());
                                nota.GrupoCsll = Convert.ToInt32(impostoReader["codgrupovalores"].ToString());
                                nota.CsllRetido = impostoReader["imposto_retido"].ToString() == "S";
                                PercBaseCalculo = Convert.ToDecimal(impostoReader["percentual_calc_base"].ToString());
                            }
                        }

                        if (nota.BaseCSLL != 0 && nota.ValorCSLL != 0 && nota.AliquotaCSLL == 0)
                        {
                            aliquotaCsll = Math.Round((nota.ValorCSLL / baseCalculoCsll) * 100, 2);
                            nota.AliquotaCSLL = aliquotaCsll;
                        }
                        else
                        {
                            if (nota.ValorCSLL != 0)
                            {
                                if (nota.BaseCSLL != 0)
                                    baseCalculoCsll = nota.BaseCSLL;
                                else
                                {
                                    if (PercBaseCalculo != 100 && PercBaseCalculo != 0)
                                    {
                                        baseCalculoCsll = nota.ValorTotal * (PercBaseCalculo / 100);
                                        nota.BaseCSLL = baseCalculoCsll;
                                    }
                                    else
                                        baseCalculoCsll = nota.ValorTotal;
                                }

                                aliquotaCsll = nota.AliquotaCSLL;
                                if (nota.AliquotaCSLL == 0)
                                {
                                    aliquotaCsll = Math.Round((nota.ValorCSLL / baseCalculoCsll) * 100, 2);
                                    nota.AliquotaCSLL = aliquotaCsll;
                                }
                                csll = Math.Round(baseCalculoCsll * (aliquotaCsll / 100), 2);
                                nota.ValorCSLL = csll;
                            }
                        }
                        #endregion

                        #region ISS
                        iss = 0;
                        baseCalculoIss = 0;
                        aliquotaIss = 0;

                        query.Clear();

                        query.Append(sqlVlComplementarPadrao);
                        query.Append("   And v.descricao Like 'ISS%' ");
                        executar = sessao.CreateQuery(query.ToString());
                        impostoReader = executar.ExecuteQuery();

                        using (impostoReader)
                        {
                            if (impostoReader.Read())
                            {
                                nota.CodigoIss = Convert.ToInt32(impostoReader["codvalores"].ToString());
                                nota.GrupoIss = Convert.ToInt32(impostoReader["codgrupovalores"].ToString());
                                nota.IssRetido = impostoReader["imposto_retido"].ToString() == "S";
                                PercBaseCalculo = Convert.ToDecimal(impostoReader["percentual_calc_base"].ToString());
                            }
                        }

                        if (nota.BaseISS != 0 && nota.ValorISS != 0 && nota.AliquotaISS == 0)
                        {
                            aliquotaIss = Math.Round((nota.ValorISS / baseCalculoIss) * 100, 2);
                            nota.AliquotaISS = aliquotaIss;
                        }
                        else
                        {
                            if (nota.ValorISS != 0)
                            {
                                if (nota.BaseISS != 0)
                                    baseCalculoIss = nota.BaseISS;
                                else
                                {
                                    if (PercBaseCalculo != 100 && PercBaseCalculo != 0)
                                    {
                                        baseCalculoIss = nota.ValorTotal * (PercBaseCalculo / 100);
                                        nota.BaseISS = baseCalculoIss;
                                    }
                                    else
                                        baseCalculoIss = nota.ValorTotal;
                                }
                                
                                if (PercBaseCalculo != 100 && PercBaseCalculo != 0)
                                {
                                    baseCalculoIss = nota.ValorTotal * (PercBaseCalculo / 100);
                                    nota.BaseISS = baseCalculoIss;
                                }
                                aliquotaIss = nota.AliquotaISS;

                                if (nota.AliquotaISS == 0)
                                {
                                    aliquotaIss = Math.Round((nota.ValorISS / baseCalculoIss) * 100, 2);
                                    nota.AliquotaISS = aliquotaIss;
                                }
                                iss = Math.Round(baseCalculoIss * (aliquotaIss / 100), 2);
                                nota.ValorISS = iss;
                            }
                        }
                        #endregion

                        #region INSS
                        inss = 0;
                        baseCalculoInss = 0;
                        aliquotaInss = 0;

                        query.Clear();

                        query.Append(sqlVlComplementarPadrao);
                        query.Append("   And v.descricao Like 'INSS%' ");
                        executar = sessao.CreateQuery(query.ToString());
                        impostoReader = executar.ExecuteQuery();

                        using (impostoReader)
                        {
                            if (impostoReader.Read())
                            {
                                nota.CodigoInss = Convert.ToInt32(impostoReader["codvalores"].ToString());
                                nota.GrupoInss = Convert.ToInt32(impostoReader["codgrupovalores"].ToString());
                                nota.InssRetido = impostoReader["imposto_retido"].ToString() == "S";
                                PercBaseCalculo = Convert.ToDecimal(impostoReader["percentual_calc_base"].ToString());
                            }
                        }

                        if (nota.BaseINSS != 0 && nota.ValorINSS != 0 && nota.AliquotaINSS == 0)
                        {
                            aliquotaInss = Math.Round((nota.ValorINSS / baseCalculoInss) * 100, 2);
                            nota.AliquotaINSS = aliquotaInss;
                        }
                        else
                        {
                            if (nota.ValorINSS != 0)
                            {
                                if (nota.BaseINSS != 0)
                                    baseCalculoInss = nota.BaseINSS;
                                else
                                {
                                    if (PercBaseCalculo != 100 && PercBaseCalculo != 0)
                                    {
                                        baseCalculoInss = nota.ValorTotal * (PercBaseCalculo / 100);
                                        nota.BaseINSS = baseCalculoInss;
                                    }
                                    else
                                        baseCalculoInss = nota.ValorTotal;
                                }
                                
                                aliquotaInss = nota.AliquotaINSS;
                                if (nota.AliquotaINSS == 0)
                                {
                                    aliquotaInss = Math.Round((nota.ValorINSS / baseCalculoInss) * 100, 2);
                                    nota.AliquotaINSS = aliquotaInss;
                                }
                                inss = Math.Round(baseCalculoInss * (aliquotaInss / 100), 2);
                                nota.ValorINSS = inss;
                            }
                        }
                        #endregion

                        List<Classes.ItensParametrosArquivei> _itens = new ItensParametrosArquiveiDAO().Listar(idParametro);

                        if (_itens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.CNPJEmitente, "")).Count() != 0)
                        {
                            nota.ValidaFornecedor = nota.CNPJFornecedor == nota.CNPJFornecedorXml;
                            nota.Diferencas = nota.Diferencas || !nota.ValidaFornecedor;
                        }
                        
                        if (_itens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.DataEmissao, "")).Count() != 0)
                        {
                            if (nota.DataEmissaoXml != null)
                                nota.ValidaEmissao = nota.DataEmissao.Date == nota.DataEmissaoXml.Value.Date ;

                            nota.Diferencas = nota.Diferencas || !nota.ValidaEmissao;
                        }

                        if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ValorTotalNF, "")).Count() != 0)
                        {
                            nota.ValidaTotal = nota.ValorTotal == nota.ValorTotalXml;
                            nota.Diferencas = nota.Diferencas || !nota.ValidaTotal;

                            nota.ValidaLiquido = nota.ValorLiquido == nota.ValorLiquidoXml || nota.ValorLiquidoSemImpostos == nota.ValorLiquidoXml;
                            nota.Diferencas = nota.Diferencas || !nota.ValidaLiquido;                            
                        }

                        nota.ValidaISS = nota.ValorISS == nota.ValorISSXml;
                        nota.Diferencas = nota.Diferencas || !nota.ValidaISS;

                        nota.ValidaINSS = nota.ValorINSS == nota.ValorINSSXml;
                        nota.Diferencas = nota.Diferencas || !nota.ValidaINSS;

                        nota.ValidaIR = nota.ValorIR == nota.ValorIRXml;
                        nota.Diferencas = nota.Diferencas || !nota.ValidaIR;

                        nota.ValidaCSLL = nota.ValorCSLL== nota.ValorCSLLXml;
                        nota.Diferencas = nota.Diferencas || !nota.ValidaCSLL;
                        
                        nota.ValidaPis = nota.ValorPIS == nota.ValorPISXml;
                        nota.Diferencas = nota.Diferencas || !nota.ValidaPis;

                        nota.ValidaCofins = nota.ValorCofins == nota.ValorCofinsXml;
                        nota.Diferencas = nota.Diferencas || !nota.ValidaCofins;

                        _lista.Add(nota);
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

        public List<ItensNotasFiscaisServico> ListarItensNotaFiscais(string tipoDocumento, string empresa, string tipo, DateTime inicial, DateTime final, bool integradas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ItensNotasFiscaisServico> _lista = new List<ItensNotasFiscaisServico>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select n.codintnf, n.numeronf, n.entradasaidanf, n.entradasaidanf, n.codigoforn, n.serienf, n.valortotalnf, n.aliqpisnf, n.vlrpisnf");
                query.Append("     , n.aliqconfinsnf, n.vlrconfinsnf, n.aliqinssnf, n.vlrinssnf, n.aliqissnf, n.vlrissnf, n.aliqirnf, n.vlrimpostorendanf");
                query.Append("     , n.aliqcsllnf, n.vlrcsllnf, m.codtpdespesa, i.qtdeitensnf, i.valorunitarioitensnf, i.valortotalitensnf, i.codsittributaria");
                query.Append("     , i.codoperfiscal, i.codclassfisc, v.datavenctonf, c.codcontactb, m.descricaomat, m.codigointernomaterial, o.codserv");
                query.Append("     , 3313 codCusto"); // Fixo conforme chamado Q1A-DPN-9Y7Y (Número do ticket: 15524)
                query.Append("  From Bgm_Notafiscal n");
                query.Append("     , est_itensnf i");
                query.Append("     , Est_Cadmaterial m");
                query.Append("     , Cpgtpdes_Ctbconta c");
                query.Append("     , Esfopfis o");
                query.Append("     , (Select Min(datavenctonf) datavenctonf, codintnf From EST_VENCTONF Group By CodintNf) v");
                query.Append(" Where n.codtpdoc = '" + tipoDocumento + "'");
                query.Append("   And n.dataemissaonf between To_date('" + inicial.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + final.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("   And n.tipooperacaonf = 'E'");
                query.Append("   And m.codigomatint = i.codigomatint");
                query.Append("   And V.Codintnf = n.codintnf");
                query.Append("   And c.codtpdespesa = m.codtpdespesa");

                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || lPad(n.codigofl,3,'0') in ('001/001','001/004')");
                else
                    query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || lPad(n.codigofl,3,'0') = '" + empresa + "'"); 

                query.Append("   And n.lanctointegradoesf = '" + (integradas ? "S" : "N") + "'");
                query.Append("   And c.nroplano = 10");
                query.Append("   And i.codintnf = n.codintnf");
                query.Append("   And o.codoperfiscal = i.codoperfiscal");
                query.Append("   And n.serienf <> 'PNEU'");

                query.Append(" Union All ");

                query.Append("Select n.codintnf, n.numeronf, n.entradasaidanf, n.entradasaidanf, n.codigoforn, n.serienf, n.valortotalnf, n.aliqpisnf, n.vlrpisnf");
                query.Append("     , n.aliqconfinsnf, n.vlrconfinsnf, n.aliqinssnf, n.vlrinssnf, n.aliqissnf, n.vlrissnf, n.aliqirnf, n.vlrimpostorendanf");
                query.Append("     , n.aliqcsllnf, n.vlrcsllnf, i.codtpdespesa, i.qtdenfservico qtdeitensnf, i.vlrunitarionfservico Valorunitarioitensnf, i.valornfservico Valortotalitensnf, i.codsittributaria");
                query.Append("     , i.codoperfiscal, i.codclassfisc, v.datavenctonf, i.codcontactb, m.descricaomatavulso Descricaomat, m.codigomatavulso Codigointernomaterial, o.codserv");
                query.Append("     , Decode(i.codCusto, null, 3313, i.codCusto) codCusto");
                query.Append("  From Bgm_Notafiscal n");
                query.Append("     , est_nfservico i");
                query.Append("     , Est_Cadmaterialavulso m");
                query.Append("     , Esfopfis o");
                query.Append("     , (Select Min(datavenctonf) datavenctonf, codintnf From EST_VENCTONF Group By CodintNf) v");
                query.Append(" Where i.codintnf = n.codintnf");
                query.Append("   And n.codtpdoc = '" + tipoDocumento + "'");
                query.Append("   And n.dataemissaonf between To_date('" + inicial.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + final.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("   And n.tipooperacaonf = 'E'");
                query.Append("   And m.codigomatavulso = i.codigomatavulso");
                query.Append("   And V.Codintnf = n.codintnf");
                query.Append("   And n.lanctointegradoesf = '" + (integradas ? "S" : "N") + "'");
                query.Append("   And o.codoperfiscal(+) = i.codoperfiscal");
                query.Append("   And n.serienf <> 'PNEU'");

                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || lPad(n.codigofl,3,'0') in ('001/001','001/004')");
                else
                    query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || lPad(n.codigofl,3,'0') = '" + empresa + "'");


                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ItensNotasFiscaisServico nota = new ItensNotasFiscaisServico();

                        nota.CodigoInternoNotaFiscal = Convert.ToDecimal(dataReader["codintnf"].ToString());
                        nota.CFOP = Convert.ToInt32(dataReader["codclassfisc"].ToString());
                        nota.CodCusto = dataReader["CodCusto"].ToString();

                        nota.NumeroNotaFiscal = dataReader["numeronf"].ToString();
                        nota.Serie = dataReader["serienf"].ToString();
                        nota.DescricaoProduto = dataReader["descricaomat"].ToString();
                        nota.Material = dataReader["codigointernomaterial"].ToString();
                        nota.CodigoTipoDeDespesa = dataReader["codtpdespesa"].ToString();
                        nota.SituacaoTributaria = dataReader["codsittributaria"].ToString();

                        try
                        {
                            nota.OperacaoFiscal = Convert.ToInt32(dataReader["codoperfiscal"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.CodContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.CodigoServico = dataReader["codserv"].ToString();
                        }
                        catch { }

                        try
                        {
                            nota.Quantidade = Convert.ToInt32(dataReader["qtdeitensnf"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorUnitario = Convert.ToDecimal(dataReader["valorunitarioitensnf"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorTotal = Math.Round(Convert.ToDecimal(dataReader["valortotalitensnf"].ToString()),2);
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaPIS = Math.Round(Convert.ToDecimal(dataReader["aliqpisnf"].ToString()),2);
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaCofins = Math.Round(Convert.ToDecimal(dataReader["aliqconfinsnf"].ToString()),2);
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaINSS = Math.Round(Convert.ToDecimal(dataReader["aliqinssnf"].ToString()),2);
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaISS = Math.Round(Convert.ToDecimal(dataReader["aliqissnf"].ToString()),2);
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaIR = Math.Round(Convert.ToDecimal(dataReader["aliqirnf"].ToString()),2);
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaCSLL = Math.Round(Convert.ToDecimal(dataReader["aliqcsllnf"].ToString()),2);
                        }
                        catch { }
                        try
                        {
                            nota.ValorPIS = Math.Round(Convert.ToDecimal(dataReader["vlrpisnf"].ToString()),2);
                        }
                        catch { }
                        try
                        {
                            nota.ValorCofins = Math.Round(Convert.ToDecimal(dataReader["aliqconfinsnf"].ToString()),2);
                        }
                        catch { }
                        try
                        {
                            nota.ValorINSS = Math.Round(Convert.ToDecimal(dataReader["vlrinssnf"].ToString()),2);
                        }
                        catch { }
                        try
                        {
                            nota.ValorISS = Math.Round(Convert.ToDecimal(dataReader["vlrissnf"].ToString()),2);
                        }
                        catch { }
                        try
                        {
                            nota.ValorIR = Math.Round(Convert.ToDecimal(dataReader["vlrimpostorendanf"].ToString()),2);
                        }
                        catch { }
                        try
                        {
                            nota.ValorCSLL = Math.Round(Convert.ToDecimal(dataReader["vlrcsllnf"].ToString()),2);
                        }
                        catch { }

                        _lista.Add(nota);
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

        public bool Integrar(string tipoDocumento, List<NotasFiscaisServico> notas, List<ItensNotasFiscaisServico> itens)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int codigoISS = 0;
            bool retorno = true;
            int i = 0;

            decimal baseCalculoCofins = 0;
            decimal aliquotaCofins = 0;
            decimal cofins = 0;
            decimal totalCofins = 0;
            int codigoCofins = 0;
            int codigoGrupo = 0;

            decimal baseCalculoPis = 0;
            decimal aliquotaPis = 0;
            decimal pis = 0;
            decimal totalPis = 0;
            int codigoPis = 0;

            decimal baseCalculoCsll = 0;
            decimal aliquotaCsll = 0;
            decimal csll = 0;
            decimal totalCsll = 0;
            int codigoCsll = 0;

            decimal baseCalculoIr = 0;
            decimal aliquotaIr = 0;
            decimal ir = 0;
            decimal totalIr = 0;
            int codigoIr = 0;

            decimal baseCalculoIss = 0;
            decimal aliquotaIss = 0;
            decimal iss = 0;
            decimal totalIss = 0;
            int codigoComplementarIss = 0;

            decimal baseCalculoInss = 0;
            decimal aliquotaInss = 0;
            decimal inss = 0;
            decimal totalInss = 0;
            int codigoInss = 0;

            string docto = "";
            try
            {

                foreach (var item in notas)
                {

                    string sqlVlComplementarPadrao = "Select w.codgrupovalores, a.codvalores, v.descricao, Nvl(v.percentual_aliquota,0) percentual_aliquota, Nvl(v.percentual_calc_base,0) percentual_calc_base, v.imposto_retido" +
                                 "  From Est_Parametro w, cpgvlragrupamento a, cpgvalores v" +
                                 " Where w.codigoempresa = " + item.CodigoEmpresa.ToString() +
                                 "   And w.codigoFl = " + item.CodigoFilial.ToString() +
                                 "   And a.codgrupovalores = w.codgrupovalores" +
                                 "   And a.codvalores = v.codvalores";
    
                    #region Calcula o proximo
                    query.Clear();
                    query.Append("Select Max(codissint) + 1 Next From Esfiss_Entra");

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            codigoISS = Convert.ToInt32(dataReader["Next"].ToString());
                    }
                    #endregion

                    docto = "NF " + item.NumeroNotaFiscal + " serie " + item.Serie + " Fornecedor " + item.CodigoFornecedor + " TpDocto " + tipoDocumento;
                    // não encontrado o campo para o valor liquido
                    query.Clear();
                    query.Append("Insert into Esfiss_Entra (codissint, codigoempresa, codigoforn, codigofl, serie, documentoini, codtpdoc, documentofin,");
                    query.Append(" emissao, vlrservico, baseiss, aliquotaiss, valoriss, baseirrf, aliquotairrf, valorirrf, observacoes,");
                    query.Append(" sistema, usuario, codserv, dtvencimento, entrada, codintnf, irrfretido, baseinss, aliquotainss,");
                    query.Append(" valorinss, inssretido, basepis, aliquotapis, valorpis, pisretido, basecofins, aliquotacofins,");
                    query.Append(" valorcofins, cofinsretido, basecsll, aliquotacsll, valorcsll, csllretido, Conferido, LogAltDados) ");
                    query.Append(" Values (" + codigoISS);
                    query.Append("  , " + item.CodigoEmpresa);
                    query.Append("  , " + item.CodigoFornecedor);

                    if (item.CodigoEmpresa == 1 && item.CodigoFilial == 4) // integra tudo na 1 conforme chamado 202005-0214
                        query.Append("  , 1 " );
                    else
                        query.Append("  , " + item.CodigoFilial);

                    query.Append("  , '" + item.Serie + "'");
                    query.Append("  , '" + item.NumeroNotaFiscal + "'");
                    query.Append("  , '" + tipoDocumento + "'");
                    query.Append("  , '" + item.NumeroNotaFiscal + "'");
                    query.Append("  , To_date('" + item.DataEmissao.ToShortDateString() + "', 'dd/mm/yyyy') ");
                    query.Append("  , " + item.ValorTotal.ToString().Replace(".", "").Replace(",", "."));

                    query.Append("  , " + item.BaseISS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.AliquotaISS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.ValorISS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.BaseIR.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.AliquotaIR.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.ValorIR.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , 'Integrado pela NIFF - Origem Estoque'");
                    query.Append("  , 'ESF'");
                    query.Append("  , '" + Publicas._usuario.UsuarioAcesso + "'");
                    query.Append("  , 0 " ); // codigoserviço
                    query.Append("  , To_date('" + item.DataVencimento.ToShortDateString() + "', 'dd/mm/yyyy') ");
                    query.Append("  , To_date('" + item.DataEntrada.ToShortDateString() + "', 'dd/mm/yyyy') ");
                    query.Append("  , " + item.CodigoInternoNotaFiscal);
                    query.Append("  , '" + (item.ValorIR == 0 ? "N" : (item.IRRetido ? "S" : "N")) + "'"); // irrfretido
                    query.Append("  , " + item.BaseINSS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.AliquotaINSS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.ValorINSS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , '" + (item.ValorINSS == 0 ? "N" : (item.InssRetido ? "S" : "N")) + "'"); // inssretido
                    query.Append("  , " + item.BasePIS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.AliquotaPIS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.ValorPIS.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , '" + (item.ValorPIS == 0 ? "N" : (item.PisRetido ? "S" : "N")) + "'"); // Pisretido
                    query.Append("  , " + item.BaseCofins.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.AliquotaCofins.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.ValorCofins.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , '" + (item.ValorCofins == 0 ? "N" : (item.CofinsRetido? "S" : "N")) + "'"); // cofinsretido
                    query.Append("  , " + item.BaseCSLL.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.AliquotaCSLL.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , " + item.ValorCSLL.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("  , '" + (item.ValorCSLL == 0 ? "N" : (item.CsllRetido ? "S" : "N")) + "'"); // Csllretido
                    query.Append("  , '" + (item.Conferida && item.IntegradaCPG ? "S" : "N") + "'");
                    query.Append("  , " + (item.Conferida && item.IntegradaCPG ? "'" + DateTime.Now.Date.ToShortDateString() + " - " + Publicas._usuario.UsuarioAcesso + "'" : "null") );

                    query.Append("  )");

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    string codigoDoctoSub = "";
                    bool temParcelas = false;

                    if (retorno)
                    {
                        // Se estiver integrado irá gravar no CPG como liberado
                        if (item.Conferida && item.IntegradaCPG)
                        {
                            string codigoDocto = "";
                            // Traz o Codigo interno do Docto e do Documento que o Substituiu
                            query.Clear();
                            query.Append("Select coddoctocpg, coddoctocpgsubst, nrodoctocpg, nroparcelacpg, seriedoctocpg,codtpdoc, codigoforn, codigoempresa, codigofl ");
                            query.Append("  from CPGDocto");
                            query.Append(" where coddoctocpg = " + item.CodigoDoctoCPG);

                            executar = sessao.CreateQuery(query.ToString());
                            dataReader = executar.ExecuteQuery();

                            using (dataReader)
                            {
                                if (dataReader.Read())
                                {
                                    codigoDocto = dataReader["coddoctocpg"].ToString();
                                    codigoDoctoSub = dataReader["coddoctocpgsubst"].ToString();

                                    // Verifica se tem Parcelas;

                                    query.Clear();
                                    query.Append("Select coddoctocpg, coddoctocpgsubst");
                                    query.Append("  from CPGDocto");
                                    query.Append(" where nrodoctocpg = '" + dataReader["nrodoctocpg"].ToString() + "'");
                                    query.Append("   And seriedoctocpg = '" + dataReader["seriedoctocpg"].ToString() + "'");
                                    query.Append("   And codtpdoc = '" + dataReader["codtpdoc"].ToString() + "'");
                                    query.Append("   And codigoforn = " + dataReader["codigoforn"].ToString());
                                    query.Append("   And codigoempresa = " + dataReader["codigoempresa"].ToString());
                                    query.Append("   And codigofl = " + dataReader["codigofl"].ToString());
                                    query.Append("   And statusdoctocpg = 'N'");

                                    executar = sessao.CreateQuery(query.ToString());
                                    dataReader = executar.ExecuteQuery();

                                    using (dataReader)
                                    {
                                        while (dataReader.Read())
                                        {
                                            temParcelas = true;

                                            if (dataReader["coddoctocpgsubst"].ToString() == "")
                                                codigoDocto = dataReader["coddoctocpg"].ToString();
                                            else
                                                codigoDocto = dataReader["coddoctocpgsubst"].ToString();

                                            if (codigoDocto != "")
                                                retorno = new ArquiveiDAO().LiberaDoctoCPG(codigoDocto, item.Conferida);
                                        }
                                    }
                                }
                            }

                            if (!temParcelas)
                            {
                                if (codigoDoctoSub != "")
                                    codigoDocto = codigoDoctoSub;

                                if (codigoDocto != "")
                                    retorno = new ArquiveiDAO().LiberaDoctoCPG(codigoDocto, item.Conferida);
                            }
                        }

                        i = 0;

                        query.Clear();
                        query.Append("Insert Into Niff_ESF_ISSIntegrado (idissint, codintnf, codissint, data, idusuario)");
                        query.Append(" values ((select Nvl(Max(idissint),0) + 1 from Niff_ESF_ISSIntegrado)");
                        query.Append("  , " + item.CodigoInternoNotaFiscal);
                        query.Append("  , " + codigoISS);
                        query.Append("  , SysDate");
                        query.Append("  , " + Publicas._idUsuario);
                        query.Append("  )");

                        retorno = sessao.ExecuteSqlTransaction(query.ToString());

                        if (retorno)
                        {
                            totalCofins = 0;
                            totalCsll = 0;
                            totalInss = 0;
                            totalIr = 0;
                            totalIss = 0;
                            totalPis = 0;
                            baseCalculoCofins = 0;
                            baseCalculoPis = 0;
                            baseCalculoCsll = 0;
                            baseCalculoIss = 0;
                            baseCalculoInss = 0;
                            baseCalculoIr = 0;

                            foreach (var items in itens.Where(w => w.CodigoInternoNotaFiscal == item.CodigoInternoNotaFiscal && w.CodigoServico != ""))
                            {
                                query.Clear();
                                i++;

                                query.Append("Insert Into Esfitemissentra (codissint, nroplano, codtpdespesa, codoperfiscal, itemiss, qtdiss, codcontactb, vlunit, ");
                                query.Append(" compldescricao, baseiss, iss, aliqiss, codserv, codcfps, CodCusto)");
                                query.Append(" values (" + codigoISS);
                                query.Append(" , 10");
                                query.Append(" , '" + items.CodigoTipoDeDespesa + "'");
                                query.Append(" , " + (items.OperacaoFiscal == 0 ? "null" : items.OperacaoFiscal.ToString() ));
                                query.Append(" , " + i);
                                query.Append(" , 1"); // + items.Quantidade);
                                query.Append(" , " + (items.CodContaContabil == 0 ? "null" : items.CodContaContabil.ToString()));
                                query.Append("  , " + item.ValorTotal.ToString().Replace(".", "").Replace(",", ".")); // item.ValorTotal
                                query.Append("  , '" + items.DescricaoProduto.Replace("'"," ") + "'");
                                query.Append("  , " + (items.ValorISS == 0 ? "0" : item.BaseISS.ToString().Replace(".", "").Replace(",", ".")));
                                query.Append("  , " + item.ValorISS.ToString().Replace(".", "").Replace(",", "."));
                                query.Append("  , " + item.AliquotaISS.ToString().Replace(".", "").Replace(",", "."));
                                query.Append("  , '" + items.CodigoServico + "'");
                                query.Append("  , " + items.CFOP);
                                query.Append("  , " + (items.CodContaContabil == 0 ? "null" : items.CodCusto));
                                query.Append("  )");

                                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                                if (retorno)
                                {
                                    #region Valores complementares
                                    cofins = 0;
                                    ir = 0;
                                    csll = 0;
                                    iss = 0;
                                    inss = 0;
                                    pis = 0;
                                    baseCalculoCofins = 0;
                                    baseCalculoCsll = 0;
                                    baseCalculoInss = 0;
                                    baseCalculoIss = 0;
                                    baseCalculoPis = 0;
                                    baseCalculoIr = 0;

                                    #region Cofins

                                    if (item.ValorCofins != 0)
                                    {
                                        query.Clear();
                                         
                                        query.Append(sqlVlComplementarPadrao);
                                        query.Append("   And v.descricao Like 'COFINS%' ");
                                        executar = sessao.CreateQuery(query.ToString());
                                        dataReader = executar.ExecuteQuery();

                                        aliquotaCofins = Math.Round((item.ValorCofins / item.ValorTotal) * 100, 2);
                                        cofins = Math.Round(items.ValorTotal * (aliquotaCofins / 100), 2);

                                        using (dataReader)
                                        {
                                            if (dataReader.Read())
                                            {
                                                codigoCofins = Convert.ToInt32(dataReader["codvalores"].ToString());
                                                codigoGrupo = Convert.ToInt32(dataReader["codgrupovalores"].ToString());

                                                aliquotaCofins = Convert.ToDecimal(dataReader["percentual_aliquota"].ToString());
                                                baseCalculoCofins = Convert.ToDecimal(dataReader["percentual_calc_base"].ToString());

                                                if (item.BaseCofins != 0 && item.ValorCofins != 0 && item.AliquotaCofins == 0)
                                                {
                                                    baseCalculoCofins = item.BaseCofins;
                                                    aliquotaCofins = Math.Round((items.ValorCofins / baseCalculoCofins) * 100, 2);
                                                }
                                                else
                                                {
                                                    if (item.BaseCofins != 0)
                                                        baseCalculoCofins = item.BaseCofins;
                                                    else
                                                    {
                                                        if (baseCalculoCofins != 100 && baseCalculoCofins != 0)
                                                            baseCalculoCofins = item.ValorTotal * (baseCalculoCofins / 100);
                                                        else
                                                            baseCalculoCofins = item.ValorTotal;
                                                    }

                                                    aliquotaCofins = item.AliquotaCofins;

                                                    if (item.AliquotaCofins == 0)
                                                        aliquotaCofins = Math.Round((item.ValorCofins / baseCalculoCofins) * 100, 2);

                                                    cofins = Math.Round(baseCalculoCofins * (aliquotaCofins / 100), 2);
                                                    
                                                }
                                            }
                                        }

                                        totalCofins = totalCofins + cofins;

                                        query.Clear();
                                        query.Append("Insert into Esfiss_EntraVlCompItem");
                                        query.Append(" (codissint, itemiss, codvalores, codgrupovalores, valor, vlr_base, aliquota)");
                                        query.Append(" Values (" + codigoISS);
                                        query.Append("    , " + i);
                                        query.Append("    , " + codigoCofins);
                                        query.Append("    , " + codigoGrupo);
                                        query.Append("    , " + item.ValorCofins.ToString().Replace(".", "").Replace(",", ".")); //cofins.ToString().Replace(".", "").Replace(",", "."));
                                        query.Append("    , " + item.BaseCofins.ToString().Replace(".", "").Replace(",", ".")); // baseCalculoCofins
                                        query.Append("    , " + item.AliquotaCofins.ToString().Replace(".", "").Replace(",", ".")); //aliquotaCofins
                                        query.Append(" )");

                                        retorno = sessao.ExecuteSqlTransaction(query.ToString());
                                    }
                                    #endregion

                                    #region PIS
                                    if (item.ValorPIS != 0)
                                    {
                                        query.Clear();

                                        aliquotaPis = Math.Round((item.ValorPIS / item.ValorTotal) * 100, 2);
                                        pis = Math.Round(items.ValorTotal * (aliquotaPis / 100), 2);

                                        query.Append(sqlVlComplementarPadrao);
                                        query.Append("   And v.descricao Like 'PIS%'");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReader = executar.ExecuteQuery();

                                        using (dataReader)
                                        {
                                            if (dataReader.Read())
                                            {
                                                codigoPis = Convert.ToInt32(dataReader["codvalores"].ToString());
                                                codigoGrupo = Convert.ToInt32(dataReader["codgrupovalores"].ToString());

                                                aliquotaPis = Convert.ToDecimal(dataReader["percentual_aliquota"].ToString());
                                                baseCalculoPis = Convert.ToDecimal(dataReader["percentual_calc_base"].ToString());

                                                if (item.BasePIS != 0 && item.ValorPIS != 0 && item.AliquotaPIS == 0)
                                                {
                                                    baseCalculoPis = item.BasePIS;
                                                    aliquotaPis = Math.Round((items.ValorPIS / baseCalculoPis) * 100, 2);
                                                }
                                                else
                                                {
                                                    if (item.BasePIS != 0)
                                                        baseCalculoPis = item.BasePIS;
                                                    else
                                                    {
                                                        if (baseCalculoPis != 100 && baseCalculoPis != 0)
                                                            baseCalculoPis = item.ValorTotal * (baseCalculoPis / 100);
                                                        else
                                                            baseCalculoPis = item.ValorTotal;
                                                    }

                                                    aliquotaPis = item.AliquotaPIS;

                                                    if (item.AliquotaPIS == 0)
                                                        aliquotaPis = Math.Round((item.ValorPIS / baseCalculoPis) * 100, 2);

                                                    pis = Math.Round(baseCalculoPis * (aliquotaPis / 100), 2);

                                                }
                                            }
                                        }

                                        totalPis = totalPis + pis;

                                        query.Clear();
                                        query.Append("Insert into Esfiss_EntraVlCompItem");
                                        query.Append(" (codissint, itemiss, codvalores, codgrupovalores, valor, vlr_base, aliquota)");
                                        query.Append(" Values (" + codigoISS);
                                        query.Append("    , " + i);
                                        query.Append("    , " + codigoPis);
                                        query.Append("    , " + codigoGrupo);
                                        query.Append("    , " + item.ValorPIS.ToString().Replace(".", "").Replace(",", "."));//pis
                                        query.Append("    , " + item.BasePIS.ToString().Replace(".", "").Replace(",", "."));//baseCalculoPis
                                        query.Append("    , " + item.AliquotaPIS.ToString().Replace(".", "").Replace(",", ".")); //aliquotaPis
                                        query.Append(" )");
                                        retorno = sessao.ExecuteSqlTransaction(query.ToString());
                                    }

                                    #endregion

                                    #region CSLL
                                    if (item.ValorCSLL != 0)
                                    {
                                        query.Clear();

                                        aliquotaCsll = Math.Round((item.ValorCSLL / item.ValorTotal) * 100, 2);
                                        csll = Math.Round(items.ValorTotal * (aliquotaCsll / 100), 2);

                                        query.Append(sqlVlComplementarPadrao);
                                        query.Append("   And v.descricao Like 'CSL%'");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReader = executar.ExecuteQuery();

                                        using (dataReader)
                                        {
                                            if (dataReader.Read())
                                            {
                                                codigoCsll = Convert.ToInt32(dataReader["codvalores"].ToString());
                                                codigoGrupo = Convert.ToInt32(dataReader["codgrupovalores"].ToString());

                                                aliquotaCsll = Convert.ToDecimal(dataReader["percentual_aliquota"].ToString());
                                                baseCalculoCsll = Convert.ToDecimal(dataReader["percentual_calc_base"].ToString());

                                                if (item.BaseCSLL != 0 && item.ValorCSLL != 0 && item.AliquotaCSLL == 0)
                                                {
                                                    baseCalculoCsll = item.BaseCSLL;
                                                    aliquotaCsll = Math.Round((items.ValorCSLL / baseCalculoCsll) * 100, 2);
                                                }
                                                else
                                                {
                                                    if (item.BaseCSLL != 0)
                                                        baseCalculoCsll = item.BaseCSLL;
                                                    else
                                                    {
                                                        if (baseCalculoCsll != 100 && baseCalculoCsll != 0)
                                                            baseCalculoCsll = item.ValorTotal * (baseCalculoCsll / 100);
                                                        else
                                                            baseCalculoCsll = item.ValorTotal;
                                                    }

                                                    aliquotaCsll = item.AliquotaCSLL;

                                                    if (item.AliquotaCSLL == 0)
                                                        aliquotaCsll = Math.Round((item.ValorCSLL / baseCalculoCsll) * 100, 2);

                                                    csll = Math.Round(baseCalculoCsll * (aliquotaCsll / 100), 2);
                                                }

                                            }
                                        }

                                        totalCsll = totalCsll + csll;

                                        query.Clear();
                                        query.Append("Insert into Esfiss_EntraVlCompItem");
                                        query.Append(" (codissint, itemiss, codvalores, codgrupovalores, valor, vlr_base, aliquota)");
                                        query.Append(" Values (" + codigoISS);
                                        query.Append("    , " + i);
                                        query.Append("    , " + codigoCsll);
                                        query.Append("    , " + codigoGrupo);
                                        query.Append("    , " + item.ValorCSLL.ToString().Replace(".", "").Replace(",", "."));//csll
                                        query.Append("    , " + item.BaseCSLL.ToString().Replace(".", "").Replace(",", "."));//baseCalculoCsll
                                        query.Append("    , " + item.AliquotaCSLL.ToString().Replace(".", "").Replace(",", "."));//aliquotaCsll
                                        query.Append(" )");
                                        retorno = sessao.ExecuteSqlTransaction(query.ToString());
                                    }
                                    #endregion

                                    #region INSS
                                    if (item.ValorINSS != 0)
                                    {
                                        query.Clear();

                                        aliquotaInss = Math.Round((item.ValorINSS / item.ValorTotal) * 100, 2);
                                        inss = Math.Round(items.ValorTotal * (aliquotaInss / 100), 2);
                                        query.Append(sqlVlComplementarPadrao);
                                        query.Append("   And v.descricao Like 'INSS%'");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReader = executar.ExecuteQuery();

                                        using (dataReader)
                                        {
                                            if (dataReader.Read())
                                            {
                                                codigoInss = Convert.ToInt32(dataReader["codvalores"].ToString());
                                                codigoGrupo = Convert.ToInt32(dataReader["codgrupovalores"].ToString());

                                                aliquotaInss = Convert.ToDecimal(dataReader["percentual_aliquota"].ToString());
                                                baseCalculoInss = Convert.ToDecimal(dataReader["percentual_calc_base"].ToString());

                                                if (item.BaseINSS != 0 && item.ValorINSS != 0 && item.AliquotaINSS == 0)
                                                {
                                                    baseCalculoInss = item.BaseINSS;
                                                    aliquotaInss = Math.Round((items.ValorINSS / baseCalculoInss) * 100, 2);
                                                }
                                                else
                                                {
                                                    if (item.BaseINSS != 0)
                                                        baseCalculoInss = item.BaseINSS;
                                                    else
                                                    {
                                                        if (baseCalculoInss != 100 && baseCalculoInss != 0)
                                                            baseCalculoInss = item.ValorTotal * (baseCalculoInss / 100);
                                                        else
                                                            baseCalculoInss = item.ValorTotal;
                                                    }

                                                    aliquotaInss = item.AliquotaINSS;

                                                    if (item.AliquotaINSS == 0)
                                                        aliquotaInss = Math.Round((item.ValorINSS / baseCalculoInss) * 100, 2);

                                                    inss = Math.Round(baseCalculoInss * (aliquotaInss / 100), 2);
                                                }
                                            }
                                        }

                                        totalInss = totalInss + inss;

                                        query.Clear();
                                        query.Append("Insert into Esfiss_EntraVlCompItem");
                                        query.Append(" (codissint, itemiss, codvalores, codgrupovalores, valor, vlr_base, aliquota)");
                                        query.Append(" Values (" + codigoISS);
                                        query.Append("    , " + i);
                                        query.Append("    , " + codigoInss);
                                        query.Append("    , " + codigoGrupo);
                                        query.Append("    , " + item.ValorINSS.ToString().Replace(".", "").Replace(",", ".")); //inss
                                        query.Append("    , " + item.BaseINSS.ToString().Replace(".", "").Replace(",", ".")); //baseCalculoInss
                                        query.Append("    , " + item.AliquotaINSS.ToString().Replace(".", "").Replace(",", "."));//aliquotaInss
                                        query.Append(" )");
                                        retorno = sessao.ExecuteSqlTransaction(query.ToString());
                                    }
                                    #endregion

                                    #region ISS
                                    if (item.ValorISS != 0)
                                    {
                                        query.Clear();

                                        aliquotaIss = Math.Round((item.ValorISS / item.ValorTotal) * 100, 2);
                                        iss = Math.Round(items.ValorTotal * (aliquotaIss / 100), 2);
                                        query.Append(sqlVlComplementarPadrao);
                                        query.Append("   And v.descricao Like 'ISS%'");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReader = executar.ExecuteQuery();

                                        using (dataReader)
                                        {
                                            if (dataReader.Read())
                                            {
                                                codigoComplementarIss = Convert.ToInt32(dataReader["codvalores"].ToString());
                                                codigoGrupo = Convert.ToInt32(dataReader["codgrupovalores"].ToString());

                                                aliquotaIss = Convert.ToDecimal(dataReader["percentual_aliquota"].ToString());
                                                baseCalculoIss = Convert.ToDecimal(dataReader["percentual_calc_base"].ToString());

                                                if (item.BaseISS != 0 && item.ValorISS != 0 && item.AliquotaISS == 0)
                                                {
                                                    baseCalculoIss = item.BaseISS;
                                                    aliquotaIss = Math.Round((items.ValorISS / baseCalculoIss) * 100, 2);
                                                }
                                                else
                                                {
                                                    if (item.BaseISS != 0)
                                                        baseCalculoIss = item.BaseISS;
                                                    else
                                                    {
                                                        if (baseCalculoIss != 100 && baseCalculoIss != 0)
                                                            baseCalculoIss = item.ValorTotal * (baseCalculoIss / 100);
                                                        else
                                                            baseCalculoIss = item.ValorTotal;
                                                    }

                                                    aliquotaIss = item.AliquotaISS;

                                                    if (item.AliquotaISS == 0)
                                                        aliquotaIss = Math.Round((item.ValorISS / baseCalculoIss) * 100, 2);

                                                    iss = Math.Round(baseCalculoIss * (aliquotaIss / 100), 2);
                                                }
                                            }
                                        }

                                        totalIss = totalIss + iss;

                                        query.Clear();
                                        query.Append("Insert into Esfiss_EntraVlCompItem");
                                        query.Append(" (codissint, itemiss, codvalores, codgrupovalores, valor, vlr_base, aliquota)");
                                        query.Append(" Values (" + codigoISS);
                                        query.Append("    , " + i);
                                        query.Append("    , " + codigoComplementarIss);
                                        query.Append("    , " + codigoGrupo);
                                        query.Append("    , " + item.ValorISS.ToString().Replace(".", "").Replace(",", "."));//iss
                                        query.Append("    , " + item.BaseISS.ToString().Replace(".", "").Replace(",", ".")); //baseCalculoIss
                                        query.Append("    , " + item.AliquotaISS.ToString().Replace(".", "").Replace(",", "."));//aliquotaIss
                                        query.Append(" )");
                                        retorno = sessao.ExecuteSqlTransaction(query.ToString());
                                    }
                                    #endregion

                                    #region IR

                                    if (item.ValorIR != 0)
                                    {
                                        query.Clear();

                                        aliquotaIr = Math.Round((item.ValorIR / item.ValorTotal) * 100, 2);
                                        ir = Math.Round(items.ValorTotal * (aliquotaIr / 100), 2);
                                        query.Append(sqlVlComplementarPadrao);
                                        query.Append("   And v.descricao Like 'IR%'");

                                        executar = sessao.CreateQuery(query.ToString());

                                        dataReader = executar.ExecuteQuery();

                                        using (dataReader)
                                        {
                                            if (dataReader.Read())
                                            {
                                                codigoIr = Convert.ToInt32(dataReader["codvalores"].ToString());
                                                codigoGrupo = Convert.ToInt32(dataReader["codgrupovalores"].ToString());

                                                aliquotaIr = Convert.ToDecimal(dataReader["percentual_aliquota"].ToString());
                                                baseCalculoIr = Convert.ToDecimal(dataReader["percentual_calc_base"].ToString());

                                                if (item.BaseIR != 0 && item.ValorIR != 0 && item.AliquotaIR == 0)
                                                {
                                                    baseCalculoIr = item.BaseIR;
                                                    aliquotaIr = Math.Round((items.ValorIR / baseCalculoIr) * 100, 2);
                                                }
                                                else
                                                {
                                                    if (item.BaseIR != 0)
                                                        baseCalculoIr = item.BaseIR;
                                                    else
                                                    {
                                                        if (baseCalculoIr != 100 && baseCalculoIr != 0)
                                                            baseCalculoIr = item.ValorTotal * (baseCalculoIr / 100);
                                                        else
                                                            baseCalculoIr = item.ValorTotal;
                                                    }

                                                    aliquotaIr = item.AliquotaIR;

                                                    if (item.AliquotaIR == 0)
                                                        aliquotaIr = Math.Round((item.ValorIR / baseCalculoIr) * 100, 2);

                                                    ir = Math.Round(baseCalculoIr * (aliquotaIr / 100), 2);
                                                }
                                            }
                                        }

                                        totalIr = totalIr + ir;

                                        query.Clear();
                                        query.Append("Insert into Esfiss_EntraVlCompItem");
                                        query.Append(" (codissint, itemiss, codvalores, codgrupovalores, valor, vlr_base, aliquota)");
                                        query.Append(" Values (" + codigoISS);
                                        query.Append("    , " + i);
                                        query.Append("    , " + codigoIr);
                                        query.Append("    , " + codigoGrupo);
                                        query.Append("    , " + item.ValorIR.ToString().Replace(".", "").Replace(",", "."));//ir
                                        query.Append("    , " + item.BaseIR.ToString().Replace(".", "").Replace(",", "."));//baseCalculoIr
                                        query.Append("    , " + item.AliquotaIR.ToString().Replace(".", "").Replace(",", "."));//aliquotaIr
                                        query.Append(" )");
                                        retorno = sessao.ExecuteSqlTransaction(query.ToString());
                                    }
                                    #endregion
                                    #endregion

                                    if (!retorno)
                                    {
                                        return false;
                                    }
                                }

                                break;
                            }

                            #region Verifica valores impostos
                            if (totalCofins != item.ValorCofins && item.ValorCofins != 0)
                            {
                                if (totalCofins > item.ValorCofins)
                                    cofins = cofins - (totalCofins - item.ValorCofins);
                                else
                                    cofins = cofins + (item.ValorCofins - totalCofins);

                                query.Clear();
                                query.Append("Update Esfiss_EntraVlCompItem");
                                query.Append("   set valor = " + cofins.ToString().Replace(".", "").Replace(",", "."));
                                query.Append(" where codissint = " + codigoISS);
                                query.Append("   and itemiss = " + i);
                                query.Append("   and codvalores = " + codigoCofins);
                                query.Append("   and codgrupovalores = " + codigoGrupo);

                                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                            }

                            if (totalCsll != item.ValorCSLL && item.ValorCSLL != 0)
                            {
                                if (totalCsll > item.ValorCSLL)
                                    csll = csll - (totalCsll - item.ValorCSLL);
                                else
                                    csll = csll + (item.ValorCSLL - totalCsll);

                                query.Clear();
                                query.Append("Update Esfiss_EntraVlCompItem");
                                query.Append("   set valor = " + csll.ToString().Replace(".", "").Replace(",", "."));
                                query.Append(" where codissint = " + codigoISS);
                                query.Append("   and itemiss = " + i);
                                query.Append("   and codvalores = " + codigoCsll);
                                query.Append("   and codgrupovalores = " + codigoGrupo);

                                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                            }

                            if (totalPis != item.ValorPIS && item.ValorPIS != 0)
                            {
                                if (totalPis > item.ValorPIS)
                                    pis = pis - (totalPis - item.ValorPIS);
                                else
                                    pis = pis + (item.ValorPIS - totalPis);

                                query.Clear();
                                query.Append("Update Esfiss_EntraVlCompItem");
                                query.Append("   set valor = " + pis.ToString().Replace(".", "").Replace(",", "."));
                                query.Append(" where codissint = " + codigoISS);
                                query.Append("   and itemiss = " + i);
                                query.Append("   and codvalores = " + codigoPis);
                                query.Append("   and codgrupovalores = " + codigoGrupo);

                                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                            }

                            if (totalIss != item.ValorISS && item.ValorISS != 0)
                            {
                                if (totalIss > item.ValorISS)
                                    iss = iss - (totalIss - item.ValorISS);
                                else
                                    iss = iss + (item.ValorISS - totalIss);

                                query.Clear();
                                query.Append("Update Esfiss_EntraVlCompItem");
                                query.Append("   set valor = " + iss.ToString().Replace(".", "").Replace(",", "."));
                                query.Append(" where codissint = " + codigoISS);
                                query.Append("   and itemiss = " + i);
                                query.Append("   and codvalores = " + codigoComplementarIss);
                                query.Append("   and codgrupovalores = " + codigoGrupo);

                                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                            }

                            if (totalInss != item.ValorINSS && item.ValorINSS != 0)
                            {
                                if (totalInss > item.ValorINSS)
                                    inss = inss - (totalInss - item.ValorINSS);
                                else
                                    inss = inss + (item.ValorINSS - totalInss);

                                query.Clear();
                                query.Append("Update Esfiss_EntraVlCompItem");
                                query.Append("   set valor = " + inss.ToString().Replace(".", "").Replace(",", "."));
                                query.Append(" where codissint = " + codigoISS);
                                query.Append("   and itemiss = " + i);
                                query.Append("   and codvalores = " + codigoInss);
                                query.Append("   and codgrupovalores = " + codigoGrupo);

                                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                            }

                            if (totalIr != item.ValorIR && item.ValorIR != 0)
                            {
                                if (totalIr > item.ValorIR)
                                    ir = ir - (totalIr - item.ValorIR);
                                else
                                    ir = ir + (item.ValorIR - totalIr);

                                query.Clear();
                                query.Append("Update Esfiss_EntraVlCompItem");
                                query.Append("   set valor = " + ir.ToString().Replace(".", "").Replace(",", "."));
                                query.Append(" where codissint = " + codigoISS);
                                query.Append("   and itemiss = " + i);
                                query.Append("   and codvalores = " + codigoIr);
                                query.Append("   and codgrupovalores = " + codigoGrupo);

                                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                            }
                        }
                        #endregion
                    }
                }
                return retorno;
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message + Environment.NewLine + " erro ao gravar essa Nota de serviço [ " + docto + " ]";
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public List<NotasFiscaisServico> ListarNotaFiscaisIntegradas(string tipoDocumento, string empresa, string tipo, DateTime inicial, DateTime final, bool integradas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<NotasFiscaisServico> _lista = new List<NotasFiscaisServico>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select e.codintnf, e.documentoini numeroNf, e.entrada entradasaidanf, e.emissao dataemissaonf, e.codigoforn");
                query.Append("     , e.serie serienf, e.vlrservico valortotalnf, e.aliquotapis aliqpisnf, e.valorpis vlrpisnf");
                query.Append("     , e.aliquotacofins aliqconfinsnf, e.valorcofins vlrconfinsnf, e.aliquotainss aliqinssnf");
                query.Append("     , e.valorinss vlrinssnf, e.aliquotaiss aliqissnf, e.valoriss vlrissnf, e.aliquotairrf aliqirnf");
                query.Append("     , e.valorirrf ValorIR, e.aliquotacsll aliqcsllnf, e.aliquotacsll vlrcsllnf");
                query.Append("     , e.dtvencimento datavenctonf, e.codserv, f.nrforn || ' - ' || f.rsocialforn Fornecedor");
                query.Append("     , t.data, u.Nome, e.CODISSINT ");
                query.Append("     , e.codigoEmpresa, e.codigofl");
                query.Append("     , e.BaseCofins, e.baseCsll, e.baseINSS, e.baseIRRF, e.basePIS, e.BaseISS");
                query.Append("     , e.Conferido, d.DescMunic cidadeforn, a.TIPOARQUIVO, a.Idarquivei");

                query.Append("  From niff_esf_issintegrado t, Esfiss_Entra e, Bgm_Fornecedor F, Dvs_Municipio d");
                query.Append("     , niff_chm_usuarios u");
                query.Append("     , (Select a.Idarquivei, a.numeronf, a.cnpjemitente");
                query.Append("             , a.Dataemissao, a.datacancelamento, a.codigoservico");
                query.Append("             , a.razaosocialemitente, a.chavedeacesso, a.discriminacao");
                query.Append("             , a.valortotalnf, a.valorservico, a.valorcredito");
                query.Append("             , a.valoriss, a.valorpis, a.valorcofins, a.valorir, a.ValorInss");
                query.Append("             , a.valorcsll, a.aliquotaservico, a.ISSREtido, a.TIPOARQUIVO");
                query.Append("          From niff_fis_arquivei a, niff_chm_empresas e");
                query.Append("         Where e.Idempresa = a.Idempresa");

                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("           And e.codigoglobus in ('001/001','001/004')");
                else
                    query.Append("           And e.codigoglobus = '" + empresa + "'");

                query.Append("          And a.tipodocto = 'NFSe') a");
                query.Append(" Where t.codintnf = e.codintnf");
                query.Append("   And e.emissao Between To_date('" + inicial.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + final.ToShortDateString() + "', 'dd/mm/yyyy')");

                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("   And Lpad(e.codigoempresa,3,'0') || '/' || lPad(e.codigofl,3,'0') in ('001/001','001/004')");
                else
                    query.Append("   And Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(e.Codigofl, 3, '0') = '" + empresa + "'");

                query.Append("   And f.codigoforn = e.codigoforn");
                query.Append("   And f.condicaoforn <> 'I'");
                query.Append("   And t.idusuario = u.Idusuario");

                query.Append("   And a.NumeroNf(+) = To_number(e.Documentoini)");
                query.Append("   And Lpad(a.Cnpjemitente(+), 14, '0') = f.Nrinscricaoforn");
                query.Append("   And f.CodMunic = d.CodMunic(+)");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        NotasFiscaisServico nota = new NotasFiscaisServico();

                        nota.CodigoInternoNotaFiscal = Convert.ToDecimal(dataReader["codintnf"].ToString());
                        nota.CodigoFornecedor = Convert.ToInt32(dataReader["codigoforn"].ToString());
                        //nota.CFOP = Convert.ToInt32(dataReader["codclassfisc"].ToString());
                        nota.CodigoEmpresa = Convert.ToInt32(dataReader["CodigoEmpresa"].ToString());
                        nota.CodigoFilial = Convert.ToInt32(dataReader["CodigoFl"].ToString());
                        nota.IdISS = Convert.ToInt32(dataReader["CODISSINT"].ToString());

                        nota.DataEmissao = Convert.ToDateTime(dataReader["dataemissaonf"].ToString());
                        nota.DataEntrada = Convert.ToDateTime(dataReader["entradasaidanf"].ToString());
                        nota.DataVencimento = Convert.ToDateTime(dataReader["datavenctonf"].ToString());
                        nota.Data = Convert.ToDateTime(dataReader["data"].ToString());

                        nota.Fornecedor = dataReader["Fornecedor"].ToString();

                        nota.CidadeFornecedor = dataReader["CIDADEFORN"].ToString();
                        nota.NumeroNotaFiscal = dataReader["numeronf"].ToString();
                        nota.Serie = dataReader["serienf"].ToString();
                        nota.Nome = dataReader["nome"].ToString();
                        nota.Conferida = dataReader["Conferido"].ToString() == "S";
                        
                        try
                        {
                            nota.IdArquivei = Convert.ToDecimal(dataReader["Idarquivei"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorTotal = Convert.ToDecimal(dataReader["valortotalnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaPIS = Convert.ToDecimal(dataReader["aliqpisnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaCofins = Convert.ToDecimal(dataReader["aliqconfinsnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaINSS = Convert.ToDecimal(dataReader["aliqinssnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaISS = Convert.ToDecimal(dataReader["aliqissnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaIR = Convert.ToDecimal(dataReader["aliqirnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaCSLL = Convert.ToDecimal(dataReader["aliqcsllnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorPIS = Convert.ToDecimal(dataReader["vlrpisnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorCofins = Convert.ToDecimal(dataReader["vlrconfinsnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorINSS = Convert.ToDecimal(dataReader["vlrinssnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorISS = Convert.ToDecimal(dataReader["vlrissnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorIR = Convert.ToDecimal(dataReader["ValorIR"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorCSLL = Convert.ToDecimal(dataReader["vlrcsllnf"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.BasePIS = Convert.ToDecimal(dataReader["BasePis"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseCofins = Convert.ToDecimal(dataReader["BaseCofins"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseCSLL = Convert.ToDecimal(dataReader["BaseCSLL"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseISS = Convert.ToDecimal(dataReader["BaseISS"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseINSS = Convert.ToDecimal(dataReader["BaseINSS"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseIR = Convert.ToDecimal(dataReader["BaseIR"].ToString());
                        }
                        catch { }

                        query.Clear();
                        query.Append("Select coddoctocpg ");
                        query.Append("  from Bgm_Notafiscal");
                        query.Append(" where codintnf = " + nota.CodigoInternoNotaFiscal);

                        executar = sessao.CreateQuery(query.ToString());
                        dataReader2 = executar.ExecuteQuery();
                        string codigoDocto = "0";

                        using (dataReader2)
                        {
                            if (dataReader2.Read())
                            {
                                codigoDocto = dataReader2["coddoctocpg"].ToString();
                                nota.IntegradaCPG = codigoDocto != "" && codigoDocto != "0";

                                if (nota.IntegradaCPG)
                                {
                                    // Traz o Codigo interno do Docto e do Documento que o Substituiu
                                    query.Clear();
                                    query.Append("Select coddoctocpg, coddoctocpgsubst ");
                                    query.Append("  from CPGDocto");
                                    query.Append(" where coddoctocpg = " + codigoDocto);

                                    executar = sessao.CreateQuery(query.ToString());
                                    dataReader2 = executar.ExecuteQuery();

                                    using (dataReader2)
                                    {
                                        if (dataReader2.Read())
                                        {
                                            if (dataReader2["coddoctocpgsubst"].ToString() == "")
                                                codigoDocto = dataReader2["coddoctocpg"].ToString();
                                            else
                                                codigoDocto = dataReader2["coddoctocpgsubst"].ToString();
                                        }
                                    }
                                }
                            }
                        }
                        nota.CodigoDoctoCPG = Convert.ToDecimal(codigoDocto);

                        _lista.Add(nota);
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

        public List<ItensNotasFiscaisServico> ListarItensNotaFiscaisIntegrados(string tipoDocumento, string empresa, string tipo, DateTime inicial, DateTime final, bool integradas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ItensNotasFiscaisServico> _lista = new List<ItensNotasFiscaisServico>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                /*query.Append("Select n.codintnf, n.numeronf, n.entradasaidanf, n.entradasaidanf, n.codigoforn, n.serienf, n.valortotalnf, n.aliqpisnf, n.vlrpisnf");
                query.Append("     , n.aliqconfinsnf, n.vlrconfinsnf, n.aliqinssnf, n.vlrinssnf, n.aliqissnf, n.vlrissnf, n.aliqirnf, n.vlrimpostorendanf");
                query.Append("     , n.aliqcsllnf, n.vlrcsllnf, m.codtpdespesa, i.qtdeitensnf, i.valorunitarioitensnf, i.valortotalitensnf, i.codsittributaria");
                query.Append("     , i.codoperfiscal, i.codclassfisc, v.datavenctonf, c.codcontactb, m.descricaomat, m.codigointernomaterial, o.codserv");
                query.Append("     , null CodCusto");
                query.Append("  From Bgm_Notafiscal n");
                query.Append("     , est_itensnf i");
                query.Append("     , Est_Cadmaterial m");
                query.Append("     , Cpgtpdes_Ctbconta c");
                query.Append("     , Esfopfis o");
                query.Append("     , (Select Min(datavenctonf) datavenctonf, codintnf From EST_VENCTONF Group By CodintNf) v");
                query.Append("     , niff_esf_issintegrado x");
                query.Append(" Where n.codtpdoc = '" + tipoDocumento + "'");
                query.Append("   And n.dataemissaonf between To_date('" + inicial.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + final.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("   And n.tipooperacaonf = 'E'");
                query.Append("   And m.codigomatint = i.codigomatint");
                query.Append("   And V.Codintnf = n.codintnf");
                query.Append("   And c.codtpdespesa = m.codtpdespesa");
                query.Append("   And Lpad(n.codigoempresa,3,'0') || '/' || lPad(n.codigofl,3,'0') = '" + empresa + "'");
                query.Append("   And n.lanctointegradoesf = '" + (integradas ? "S" : "N") + "'");
                query.Append("   And c.nroplano = 10");
                query.Append("   And i.codintnf = n.codintnf");
                query.Append("   And n.codintnf = x.codintnf");
                query.Append("   And o.codoperfiscal = i.codoperfiscal");

                query.Append(" Union All ");

                query.Append("Select n.codintnf, n.numeronf, n.entradasaidanf, n.entradasaidanf, n.codigoforn, n.serienf, n.valortotalnf, n.aliqpisnf, n.vlrpisnf");
                query.Append("     , n.aliqconfinsnf, n.vlrconfinsnf, n.aliqinssnf, n.vlrinssnf, n.aliqissnf, n.vlrissnf, n.aliqirnf, n.vlrimpostorendanf");
                query.Append("     , n.aliqcsllnf, n.vlrcsllnf, i.codtpdespesa, i.qtdenfservico qtdeitensnf, i.vlrunitarionfservico Valorunitarioitensnf, i.valornfservico Valortotalitensnf, i.codsittributaria");
                query.Append("     , i.codoperfiscal, i.codclassfisc, v.datavenctonf, i.codcontactb, m.descricaomatavulso Descricaomat, m.codigomatavulso Codigointernomaterial, o.codserv");
                query.Append("     , i.CodCusto");
                query.Append("  From Bgm_Notafiscal n");
                query.Append("     , est_nfservico i");
                query.Append("     , Est_Cadmaterialavulso m");
                query.Append("     , Esfopfis o");
                query.Append("     , (Select Min(datavenctonf) datavenctonf, codintnf From EST_VENCTONF Group By CodintNf) v");
                query.Append("     , niff_esf_issintegrado x");
                query.Append(" Where i.codintnf = n.codintnf");
                query.Append("   And n.codtpdoc = '" + tipoDocumento + "'");
                query.Append("   And n.dataemissaonf between To_date('" + inicial.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + final.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("   And n.tipooperacaonf = 'E'");
                query.Append("   And m.codigomatavulso = i.codigomatavulso");
                query.Append("   And V.Codintnf = n.codintnf");
                query.Append("   And n.codintnf = x.codintnf");
                query.Append("   And n.lanctointegradoesf = '" + (integradas ? "S" : "N") + "'");
                query.Append("   And o.codoperfiscal(+) = i.codoperfiscal");*/

                query.Append("Select e.codintnf, e.documentoini numeroNf, e.entrada entradasaidanf, e.emissao dataemissaonf, e.codigoforn");
                query.Append("     , e.serie serienf, e.vlrservico valortotalnf, e.aliquotapis aliqpisnf, e.valorpis vlrpisnf");
                query.Append("     , e.aliquotacofins aliqconfinsnf, e.valorcofins vlrconfinsnf, e.aliquotainss aliqinssnf");
                query.Append("     , e.valorinss vlrinssnf, e.aliquotaiss aliqissnf, e.valoriss vlrissnf, e.aliquotairrf aliqirnf");
                query.Append("     , e.valorirrf ValorIR, e.aliquotacsll aliqcsllnf, e.aliquotacsll vlrcsllnf, i.QTDISS qtdeitensnf");
                query.Append("     , i.codtpdespesa, i.qtdiss, i.vlunit Valorunitarioitensnf, i.qtdiss* i.vlunit Valortotalitensnf, i.codoperfiscal"); 
                query.Append("     , i.codcontactb, i.codcusto, e.dtvencimento datavenctonf, i.codserv, i.compldescricao Descricaomat");
                query.Append("     , t.data, e.CODISSINT ");
                query.Append("     , e.codigoEmpresa, e.codigofl");

                query.Append("  From niff_esf_issintegrado t, Esfiss_Entra e, Esfitemissentra i");
                query.Append(" Where t.codintnf = e.codintnf");
                query.Append("   And e.emissao Between To_date('" + inicial.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + final.ToShortDateString() + "', 'dd/mm/yyyy')");
                
                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("   And Lpad(e.codigoempresa,3,'0') || '/' || lPad(e.codigofl,3,'0') in ('001/001','001/004')");
                else
                    query.Append("   And Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(e.Codigofl, 3, '0') = '" + empresa + "'");

                query.Append("   And e.codissint = i.codissint");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ItensNotasFiscaisServico nota = new ItensNotasFiscaisServico();

                        nota.CodigoInternoNotaFiscal = Convert.ToDecimal(dataReader["codintnf"].ToString());
                        //nota.CFOP = Convert.ToInt32(dataReader["codclassfisc"].ToString());

                        nota.NumeroNotaFiscal = dataReader["numeronf"].ToString();
                        nota.Serie = dataReader["serienf"].ToString();
                        nota.DescricaoProduto = dataReader["descricaomat"].ToString();
                        //nota.Material = dataReader["codigointernomaterial"].ToString();
                        nota.CodigoTipoDeDespesa = dataReader["codtpdespesa"].ToString();
                        //nota.SituacaoTributaria = dataReader["codsittributaria"].ToString();
                        nota.CodCusto = dataReader["CodCusto"].ToString();

                        try
                        {
                            nota.OperacaoFiscal = Convert.ToInt32(dataReader["codoperfiscal"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.CodContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.CodigoServico = dataReader["codserv"].ToString();
                        }
                        catch { }

                        try
                        {
                            nota.Quantidade = Convert.ToInt32(dataReader["qtdeitensnf"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorUnitario = Convert.ToDecimal(dataReader["valorunitarioitensnf"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorTotal = Convert.ToDecimal(dataReader["valortotalitensnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaPIS = Convert.ToDecimal(dataReader["aliqpisnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaCofins = Convert.ToDecimal(dataReader["aliqconfinsnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaINSS = Convert.ToDecimal(dataReader["aliqinssnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaISS = Convert.ToDecimal(dataReader["aliqissnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaIR = Convert.ToDecimal(dataReader["aliqirnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaCSLL = Convert.ToDecimal(dataReader["aliqcsllnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorPIS = Convert.ToDecimal(dataReader["vlrpisnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorCofins = Convert.ToDecimal(dataReader["aliqconfinsnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorINSS = Convert.ToDecimal(dataReader["vlrinssnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorISS = Convert.ToDecimal(dataReader["vlrissnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorIR = Convert.ToDecimal(dataReader["vlrimpostorendanf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorCSLL = Convert.ToDecimal(dataReader["vlrcsllnf"].ToString());
                        }
                        catch { }

                        _lista.Add(nota);
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

        public List<NotasFiscaisServico> ListarNotaFiscaisEscrituracao(string tipoDocumento, string empresa, string tipo, DateTime inicial, DateTime final, int idParametro)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<NotasFiscaisServico> _lista = new List<NotasFiscaisServico>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select e.codintnf, e.documentoini numeroNf, e.entrada entradasaidanf, e.emissao dataemissaonf, e.codigoforn");
                query.Append("     , e.serie serienf, e.vlrservico valortotalnf, e.aliquotapis aliqpisnf, e.valorpis vlrpisnf");
                query.Append("     , e.aliquotacofins aliqconfinsnf, e.valorcofins vlrconfinsnf, e.aliquotainss aliqinssnf");
                query.Append("     , e.valorinss vlrinssnf, e.aliquotaiss aliqissnf, e.valoriss vlrissnf, e.aliquotairrf aliqirnf");
                query.Append("     , e.valorirrf ValorIR, e.aliquotacsll aliqcsllnf, e.valorcsll vlrcsllnf");
                query.Append("     , e.dtvencimento datavenctonf, e.codserv, f.nrforn || ' - ' || f.rsocialforn Fornecedor");
                query.Append("     , e.CODISSINT, e.codigoEmpresa, e.codigofl");
                query.Append("     , e.BaseCofins, e.baseCsll, e.baseINSS, e.baseIRRF, e.basePIS, e.BaseISS");
                query.Append("     , e.Conferido, e.codDoctoCPG, 0 coddoctoesf");
                query.Append("     , 0 icmsbaseentra, 0 icmsaliqentra, 0 icmsvalorentra, 0 icmsisentaentra, 0 icmsoutrasentra");
                query.Append("     , 0 icmssubstbase, 0 icmssubstvalor, 0 codclassfisc");
                query.Append("     , u.nomeusuario");

                query.Append("     , a.Idarquivei, a.numeronf NumeroNfXml, formatacnpjcpf(Lpad(a.Cnpjemitente, 14, '0'), 'CNPJ') cnpjFornecedorXml");
                query.Append("     , a.Dataemissao DataemissaoXml, a.datacancelamento");
                query.Append("     , a.razaosocialemitente, a.chavedeacesso, a.valortotalnf valortotalnfXml, a.valorservico, a.valorcredito");
                query.Append("     , a.valoriss valorissXml, a.valorpis valorpisXml, a.valorcofins valorcofinsXml, a.ValorInss valorInssXml");
                query.Append("     , a.valorir valorirXml, a.valorcsll valorcsllXml, a.aliquotaservico aliquotaservicoXml, a.ISSREtido ISSREtidoXml");
                query.Append("     , a.discriminacao, a.codigoservico, d.DescMunic cidadeforn");


                query.Append("  From Esfiss_Entra e, Bgm_Fornecedor F, Ctr_Cadastrodeusuarios u, Dvs_Municipio d");
                query.Append("     , (Select a.Idarquivei, a.numeronf, a.cnpjemitente");
                query.Append("             , a.Dataemissao, a.datacancelamento, a.codigoservico");
                query.Append("             , a.razaosocialemitente, a.chavedeacesso, a.discriminacao");
                query.Append("             , a.valortotalnf, a.valorservico, a.valorcredito");
                query.Append("             , a.valoriss, a.valorpis, a.valorcofins, a.valorir, a.ValorInss");
                query.Append("             , a.valorcsll, a.aliquotaservico, a.ISSREtido");
                query.Append("          From niff_fis_arquivei a, niff_chm_empresas e");
                query.Append("         Where e.Idempresa = a.Idempresa");

                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("           And e.codigoglobus in ('001/001','001/004')");
                else
                    query.Append("           And e.codigoglobus = '" + empresa + "'");

                query.Append("          And a.tipodocto = 'NFSe') a");

                query.Append(" Where e.emissao Between To_date('" + inicial.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + final.ToShortDateString() + "', 'dd/mm/yyyy')");

                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("   And Lpad(e.codigoempresa,3,'0') || '/' || lPad(e.codigofl,3,'0') in ('001/001','001/004')");
                else
                    query.Append("   And Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(e.Codigofl, 3, '0') = '" + empresa + "'");

                query.Append("   And f.codigoforn = e.codigoforn");
                //query.Append("   And e.sistema = 'ESF'");
                query.Append("   And e.CODTPDOC = '" + tipoDocumento + "'");
                query.Append("   And (e.observacoes Not Like '%Origem Estoque' Or e.Observacoes Is Null)");
                query.Append("   And e.usuario = u.usuario");

                query.Append("   And a.NumeroNf(+) = To_number(e.Documentoini)");
                query.Append("   And Lpad(a.Cnpjemitente(+), 14, '0') = f.Nrinscricaoforn");
                query.Append("   And f.condicaoforn <> 'I'");
                query.Append("   And f.CodMunic = d.CodMunic(+)");

                query.Append(" Union all ");
                query.Append("Select 0 codintnf, e.nrdocentra, e.dtentradaentra, e.dtemissaoentra, e.codigoforn, e.serieentra");
                query.Append("     , e.vlcontabilentra, 0 Aliqpisnf, 0 Vlrpisnf, 0 Aliqconfinsnf, 0 Vlrconfinsnf, 0 Aliqinssnf");
                query.Append("     , 0 Vlrinssnf, 0 Aliqissnf, 0 Vlrissnf, 0 Aliqirnf, 0 Valorir, 0 Aliqcsllnf, 0 Vlrcsllnf");
                query.Append("     , e.dt_vencimento, null Codserv, f.Nrforn || ' - ' || f.Rsocialforn Fornecedor");
                query.Append("     , 0, e.Codigoempresa, e.Codigofl, 0 Basecofins, 0 Basecsll, 0 Baseinss");
                query.Append("     , 0 Baseirrf, 0 Basepis, 0 Baseiss, e.DocConciliado Conferido");
                query.Append("     , d.coddoctocpg Coddoctocpg, e.coddoctoesf");
                query.Append("     , e.icmsbaseentra, e.icmsaliqentra, e.icmsvalorentra, e.icmsisentaentra, e.icmsoutrasentra");
                query.Append("     , e.icmssubstbase, e.icmssubstvalor, e.codclassfisc");
                query.Append("     , u.nomeusuario");
                query.Append("     , 0 Idarquivei, null NumeroNfXml, null cnpjFornecedorXml");
                query.Append("     , null DataemissaoXml, null datacancelamento");
                query.Append("     , null razaosocialemitente, null chavedeacesso, 0 valortotalnfXml, 0 valorservico, 0 valorcredito");
                query.Append("     , 0 valorissXml, 0 valorpisXml, 0 valorcofinsXml, 0 valorInssXml");
                query.Append("     , 0 valorirXml, 0 valorcsllXml, 0 aliquotaservicoXml, 'N' ISSREtidoXml");
                query.Append("     , null discriminacao, null codigoservico, d.DescMunic cidadeforn");

                query.Append(" From ESFENTRA E, Bgm_Fornecedor F, Cpgdocto d, Ctr_Cadastrodeusuarios u, Dvs_Municipio d");
                query.Append(" Where f.Codigoforn = e.Codigoforn");
                query.Append("   And d.coddoctoesf(+) = e.coddoctoesf");
                query.Append("   And E.DTEMISSAOENTRA Between To_date('" + inicial.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + final.ToShortDateString() + "', 'dd/mm/yyyy')");

                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("   And Lpad(e.codigoempresa,3,'0') || '/' || lPad(e.codigofl,3,'0') in ('001/001','001/004')");
                else
                    query.Append("   And Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(e.Codigofl, 3, '0')  = '" + empresa + "'");

                query.Append("   And e.Codtpdoc = '" + tipoDocumento + "'");
                query.Append("   And e.usuario = u.usuario");
                query.Append("   And f.condicaoforn <> 'I'");
                query.Append("   And f.CodMunic = d.CodMunic(+)");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        NotasFiscaisServico nota = new NotasFiscaisServico();

                        nota.CodigoInternoNotaFiscal = Convert.ToDecimal(dataReader["codintnf"].ToString());
                        nota.CodigoFornecedor = Convert.ToInt32(dataReader["codigoforn"].ToString());
                        //nota.CFOP = Convert.ToInt32(dataReader["codclassfisc"].ToString());
                        nota.CodigoEmpresa = Convert.ToInt32(dataReader["CodigoEmpresa"].ToString());
                        nota.CodigoFilial = Convert.ToInt32(dataReader["CodigoFl"].ToString());
                        nota.IdISS = Convert.ToInt32(dataReader["CODISSINT"].ToString());
                        nota.CodDoctoEsf = Convert.ToInt32(dataReader["CodDoctoEsf"].ToString());

                        if (nota.IdISS == 0)
                            nota.IdISS = (int)nota.CodDoctoEsf;

                            nota.DataEmissao = Convert.ToDateTime(dataReader["dataemissaonf"].ToString());
                        nota.DataEntrada = Convert.ToDateTime(dataReader["entradasaidanf"].ToString());
                        nota.DataVencimento = Convert.ToDateTime(dataReader["datavenctonf"].ToString());

                        try
                        {
                            nota.DataCancelamento = Convert.ToDateTime(dataReader["DataCancelamento"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.DataEmissaoXml = Convert.ToDateTime(dataReader["DataemissaoXml"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.IdArquivei = Convert.ToDecimal(dataReader["Idarquivei"].ToString());
                        }
                        catch { }

                        nota.Fornecedor = dataReader["Fornecedor"].ToString();
                        nota.CidadeFornecedor = dataReader["CIDADEFORN"].ToString();
                        nota.NumeroNotaFiscal = dataReader["numeronf"].ToString();
                        nota.Serie = dataReader["serienf"].ToString();
                        nota.Conferida = dataReader["Conferido"].ToString() == "S";

                        nota.FornecedorXml = dataReader["razaosocialemitente"].ToString();
                        nota.CNPJFornecedorXml = dataReader["cnpjFornecedorXml"].ToString();
                        nota.DiscriminacaoXml = dataReader["Discriminacao"].ToString();
                        nota.CodigoServico = dataReader["CodigoServico"].ToString();

                        try
                        {
                            nota.ValorTotal = Convert.ToDecimal(dataReader["valortotalnf"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorTotalXml = Convert.ToDecimal(dataReader["valortotalnfXml"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.AliquotaPIS = Convert.ToDecimal(dataReader["aliqpisnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaCofins = Convert.ToDecimal(dataReader["aliqconfinsnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaINSS = Convert.ToDecimal(dataReader["aliqinssnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaISS = Convert.ToDecimal(dataReader["aliqissnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaIR = Convert.ToDecimal(dataReader["aliqirnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaCSLL = Convert.ToDecimal(dataReader["aliqcsllnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorPIS = Convert.ToDecimal(dataReader["vlrpisnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorCofins = Convert.ToDecimal(dataReader["vlrconfinsnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorINSS = Convert.ToDecimal(dataReader["vlrinssnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorISS = Convert.ToDecimal(dataReader["vlrissnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorIR = Convert.ToDecimal(dataReader["ValorIR"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorCSLL = Convert.ToDecimal(dataReader["vlrcsllnf"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorPISXml = Convert.ToDecimal(dataReader["valorpisXml"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorCofinsXml = Convert.ToDecimal(dataReader["valorcofinsXml"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorINSSXml = Convert.ToDecimal(dataReader["valorInssXml"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorIRXml = Convert.ToDecimal(dataReader["valorirXml"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorCSLLXml = Convert.ToDecimal(dataReader["valorcsllXml"].ToString());
                        }
                        catch { }


                        nota.IssRetidoXml = dataReader["ISSREtidoXml"].ToString() == "S";

                        if (nota.IssRetidoXml)
                        {

                            try
                            {
                                nota.AliquotaISSXml = Convert.ToDecimal(dataReader["aliquotaservicoXml"].ToString());
                            }
                            catch { }


                            try
                            {
                                nota.ValorISSXml = Convert.ToDecimal(dataReader["valorissXml"].ToString());
                            }
                            catch { }
                        }

                        try
                        {
                            nota.BasePIS = Convert.ToDecimal(dataReader["BasePis"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseCofins = Convert.ToDecimal(dataReader["BaseCofins"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseCSLL = Convert.ToDecimal(dataReader["BaseCSLL"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseISS = Convert.ToDecimal(dataReader["BaseISS"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseINSS = Convert.ToDecimal(dataReader["BaseINSS"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseIR = Convert.ToDecimal(dataReader["baseIRRF"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorLiquido = nota.ValorTotal - nota.ValorPIS - nota.ValorISS - nota.ValorIR - nota.ValorINSS - nota.ValorCSLL - nota.ValorCofins;
                        }
                        catch { }

                        try
                        {
                            nota.ValorLiquidoSemImpostos = nota.ValorTotal - nota.ValorISS;
                        }
                        catch { }

                        try
                        {
                            nota.ValorLiquidoXml = nota.ValorTotalXml - nota.ValorPISXml - nota.ValorISSXml - nota.ValorIRXml - nota.ValorINSSXml - nota.ValorCSLLXml - nota.ValorCofinsXml;
                        }
                        catch { }

                        try
                        {
                            nota.CodigoDoctoCPG = Convert.ToDecimal(dataReader["coddoctocpg"].ToString());
                            nota.IntegradaCPG = true;
                        }
                        catch { }

                        try
                        {
                            nota.BaseICMS = Convert.ToDecimal(dataReader["icmsbaseentra"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaICMS = Convert.ToDecimal(dataReader["icmsaliqentra"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorICMS = Convert.ToDecimal(dataReader["icmsvalorentra"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.IsentaICMS = Convert.ToDecimal(dataReader["icmsisentaentra"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.OutrasICMS = Convert.ToDecimal(dataReader["icmsoutrasentra"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.BaseSubICMS = Convert.ToDecimal(dataReader["icmssubstbase"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorSubICMS = Convert.ToDecimal(dataReader["icmssubstvalor"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.CFOP = Convert.ToInt32(dataReader["codclassfisc"].ToString());
                        }
                        catch { }

                        nota.Usuario = dataReader["Nomeusuario"].ToString();
                        try
                        {
                            nota.Usuario = nota.Usuario.ToUpper()
                                           .Replace("ABCT", "")
                                           .Replace("ABC", "")
                                           .Replace("CISNE", "")
                                           .Replace("ARUJA", "")
                                           .Replace("ARUJÁ", "")
                                           .Replace("CAMPIBUS", "")
                                           .Replace("EOVG1", "")
                                           .Replace("EOVG2", "")
                                           .Replace("EOVG", "")
                                           .Replace("VUG", "")
                                           .Replace("RIBE", "")
                                           .Replace("RPDO", "")
                                           .Replace("NIFF", "")
                                           .Replace("SUPPORT", "")
                                           .Replace("(", "")
                                           .Replace(")", "")
                                           .Replace("-", "");

                            nota.Usuario = nota.Usuario.Trim();
                            string[] nomes = nota.Usuario.Split(' ');
                            nota.Usuario = nomes[0] + " " + nomes[nomes.Length - 1];
                        }
                        catch { }

                        if (nota.CodigoDoctoCPG == 0 && nota.CodigoInternoNotaFiscal != 0)
                        {
                            query.Clear();
                            query.Append("Select coddoctocpg ");
                            query.Append("  from Bgm_Notafiscal");
                            query.Append(" where codintnf = " + nota.CodigoInternoNotaFiscal);

                            executar = sessao.CreateQuery(query.ToString());
                            dataReader2 = executar.ExecuteQuery();
                            string codigoDocto = "";

                            using (dataReader2)
                            {
                                if (dataReader2.Read())
                                {
                                    codigoDocto = dataReader2["coddoctocpg"].ToString();
                                    nota.IntegradaCPG = codigoDocto != "";

                                    // Traz o Codigo interno do Docto e do Documento que o Substituiu
                                    query.Clear();
                                    query.Append("Select coddoctocpg, coddoctocpgsubst ");
                                    query.Append("  from CPGDocto");
                                    query.Append(" where coddoctocpg = " + codigoDocto);

                                    executar = sessao.CreateQuery(query.ToString());
                                    dataReader2 = executar.ExecuteQuery();

                                    using (dataReader2)
                                    {
                                        if (dataReader2.Read())
                                        {
                                            if (dataReader2["coddoctocpgsubst"].ToString() == "")
                                                codigoDocto = dataReader2["coddoctocpg"].ToString();
                                            else
                                                codigoDocto = dataReader2["coddoctocpgsubst"].ToString();
                                        }
                                    }
                                }
                            }
                            try
                            {
                                nota.CodigoDoctoCPG = Convert.ToDecimal(codigoDocto);
                            }
                            catch { }
                        }

                        if (nota.CodigoInternoNotaFiscal == 0)
                        {// busca valores complementares

                            string sqlVlComplementarPadrao = "Select w.codgrupovalores, a.codvalores, v.descricao, Nvl(v.percentual_aliquota,0) percentual_aliquota, " +
                            "Nvl(v.percentual_calc_base,0) percentual_calc_base, v.imposto_retido" +
                            "  From Est_Parametro w, cpgvlragrupamento a, cpgvalores v" +
                            " Where w.codigoempresa = " + nota.CodigoEmpresa.ToString() +
                            "   And w.codigoFl = " + nota.CodigoFilial.ToString() +
                            "   And a.codgrupovalores = w.codgrupovalores" +
                            "   And a.codvalores = v.codvalores";


                            query.Clear();

                            query.Append("Select codintnf From esfnotafiscal n ");
                            query.Append("Where n.coddoctoesf = " + nota.CodDoctoEsf);

                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();
                            decimal _codintNota = 0;

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                    _codintNota = Convert.ToDecimal(impostoReader["codintnf"].ToString());
                            }

                            string sqlValores = "Select v.aliquota, v.vlr_base, v.valor" +
                                "  From esfnotafiscal_vlcompl v";

                            #region Cofins

                            query.Clear();

                            query.Append(sqlVlComplementarPadrao);
                            query.Append("   And v.descricao Like 'COFINS%' ");
                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                {
                                    nota.CodigoCofins = Convert.ToInt32(impostoReader["codvalores"].ToString());
                                    nota.GrupoCofins = Convert.ToInt32(impostoReader["codgrupovalores"].ToString());
                                    nota.CofinsRetido = impostoReader["imposto_retido"].ToString() == "S";
                                }
                            }

                            query.Clear();

                            query.Append(sqlValores);
                            query.Append(" Where v.codintnf = " + _codintNota);
                            query.Append("   and v.codvalores = " + nota.CodigoCofins);
                            query.Append("   and v.codgrupovalores = " + nota.GrupoCofins);

                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                {
                                    nota.AliquotaCofins = Convert.ToDecimal(impostoReader["Aliquota"].ToString());
                                    nota.BaseCofins = Convert.ToDecimal(impostoReader["vlr_base"].ToString());
                                    nota.ValorCofins = Convert.ToDecimal(impostoReader["valor"].ToString());
                                }
                            }
                            #endregion

                            #region PIS
                            query.Clear();

                            query.Append(sqlVlComplementarPadrao);
                            query.Append("   And v.descricao Like 'PIS%' ");
                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                {
                                    nota.CodigoPis = Convert.ToInt32(impostoReader["codvalores"].ToString());
                                    nota.GrupoPis = Convert.ToInt32(impostoReader["codgrupovalores"].ToString());
                                    nota.PisRetido = impostoReader["imposto_retido"].ToString() == "S";
                                }
                            }

                            query.Clear();

                            query.Append(sqlValores);
                            query.Append(" Where v.codintnf = " + _codintNota);
                            query.Append("   and v.codvalores = " + nota.CodigoPis);
                            query.Append("   and v.codgrupovalores = " + nota.GrupoPis);

                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                {
                                    nota.AliquotaPIS = Convert.ToDecimal(impostoReader["Aliquota"].ToString());
                                    nota.BasePIS = Convert.ToDecimal(impostoReader["vlr_base"].ToString());
                                    nota.ValorPIS = Convert.ToDecimal(impostoReader["valor"].ToString());
                                }
                            }
                            #endregion

                            #region CSLL
                            query.Clear();

                            query.Append(sqlVlComplementarPadrao);
                            query.Append("   And v.descricao Like 'CSL%' ");
                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                {
                                    nota.CodigoCsll = Convert.ToInt32(impostoReader["codvalores"].ToString());
                                    nota.GrupoCsll = Convert.ToInt32(impostoReader["codgrupovalores"].ToString());
                                    nota.CsllRetido = impostoReader["imposto_retido"].ToString() == "S";
                                }
                            }

                            query.Clear();

                            query.Append(sqlValores);
                            query.Append(" Where v.codintnf = " + _codintNota);
                            query.Append("   and v.codvalores = " + nota.CodigoCsll);
                            query.Append("   and v.codgrupovalores = " + nota.GrupoCsll);

                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                {
                                    nota.AliquotaCSLL = Convert.ToDecimal(impostoReader["Aliquota"].ToString());
                                    nota.BaseCSLL = Convert.ToDecimal(impostoReader["vlr_base"].ToString());
                                    nota.ValorCSLL = Convert.ToDecimal(impostoReader["valor"].ToString());
                                }
                            }
                            #endregion

                            #region ISS
                            query.Clear();

                            query.Append(sqlVlComplementarPadrao);
                            query.Append("   And v.descricao Like 'ISS%' ");
                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                {
                                    nota.CodigoIss = Convert.ToInt32(impostoReader["codvalores"].ToString());
                                    nota.GrupoIss = Convert.ToInt32(impostoReader["codgrupovalores"].ToString());
                                    nota.IssRetido = impostoReader["imposto_retido"].ToString() == "S";
                                }
                            }

                            query.Clear();

                            query.Append(sqlValores);
                            query.Append(" Where v.codintnf = " + _codintNota);
                            query.Append("   and v.codvalores = " + nota.CodigoIss);
                            query.Append("   and v.codgrupovalores = " + nota.GrupoIss);

                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                {
                                    nota.AliquotaISS = Convert.ToDecimal(impostoReader["Aliquota"].ToString());
                                    nota.BaseISS = Convert.ToDecimal(impostoReader["vlr_base"].ToString());
                                    nota.ValorISS = Convert.ToDecimal(impostoReader["valor"].ToString());
                                }
                            }
                            #endregion

                            #region INSS
                            query.Clear();

                            query.Append(sqlVlComplementarPadrao);
                            query.Append("   And v.descricao Like 'INSS%' ");
                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                {
                                    nota.CodigoInss = Convert.ToInt32(impostoReader["codvalores"].ToString());
                                    nota.GrupoInss = Convert.ToInt32(impostoReader["codgrupovalores"].ToString());
                                    nota.InssRetido = impostoReader["imposto_retido"].ToString() == "S";
                                }
                            }

                            query.Clear();

                            query.Append(sqlValores);
                            query.Append(" Where v.codintnf = " + _codintNota);
                            query.Append("   and v.codvalores = " + nota.CodigoInss);
                            query.Append("   and v.codgrupovalores = " + nota.GrupoInss);

                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                {
                                    nota.AliquotaINSS = Convert.ToDecimal(impostoReader["Aliquota"].ToString());
                                    nota.BaseINSS = Convert.ToDecimal(impostoReader["vlr_base"].ToString());
                                    nota.ValorINSS = Convert.ToDecimal(impostoReader["valor"].ToString());
                                }
                            }
                            #endregion

                            #region IR
                            query.Clear();

                            query.Append(sqlVlComplementarPadrao);
                            query.Append("   And v.descricao Like 'IR%' ");
                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                {
                                    nota.CodigoIR = Convert.ToInt32(impostoReader["codvalores"].ToString());
                                    nota.GrupoIR = Convert.ToInt32(impostoReader["codgrupovalores"].ToString());
                                    nota.IRRetido = impostoReader["imposto_retido"].ToString() == "S";
                                }
                            }

                            query.Clear();

                            query.Append(sqlValores);
                            query.Append(" Where v.codintnf = " + _codintNota);
                            query.Append("   and v.codvalores = " + nota.CodigoIR);
                            query.Append("   and v.codgrupovalores = " + nota.GrupoIR);

                            executar = sessao.CreateQuery(query.ToString());
                            impostoReader = executar.ExecuteQuery();

                            using (impostoReader)
                            {
                                if (impostoReader.Read())
                                {
                                    nota.AliquotaIR = Convert.ToDecimal(impostoReader["Aliquota"].ToString());
                                    nota.BaseIR = Convert.ToDecimal(impostoReader["vlr_base"].ToString());
                                    nota.ValorIR = Convert.ToDecimal(impostoReader["valor"].ToString());
                                }
                            }
                            #endregion
                        }

                        if (nota.IdArquivei == 0)
                        {
                            nota.ValidaFornecedor = true;
                            nota.ValidaEmissao = true;

                            nota.ValidaTotal = true;
                            nota.ValidaLiquido = true;

                            nota.ValidaISS = true;

                            nota.ValidaINSS = true;

                            nota.ValidaIR = true;

                            nota.ValidaCSLL = true;

                            nota.ValidaPis = true;

                            nota.ValidaCofins = true;
                            nota.Diferencas = false;
                        }
                        else
                        {
                            List<Classes.ItensParametrosArquivei> _itens = new ItensParametrosArquiveiDAO().Listar(idParametro);

                            if (_itens.Where(w => w.ValidarCampo &&
                                                  w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.DataEmissao, "")).Count() != 0)
                            {
                                if (nota.DataEmissaoXml != null)
                                    nota.ValidaEmissao = nota.DataEmissao.Date == nota.DataEmissaoXml.Value.Date;
                                nota.Diferencas = nota.Diferencas || !nota.ValidaEmissao;
                            }

                            if (_itens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ValorTotalNF, "")).Count() != 0)
                            {
                                nota.ValidaTotal = nota.ValorTotal == nota.ValorTotalXml;
                                nota.Diferencas = nota.Diferencas || !nota.ValidaTotal;

                                nota.ValidaLiquido = nota.ValorLiquido == nota.ValorLiquidoXml || nota.ValorLiquidoSemImpostos == nota.ValorLiquidoXml;
                                nota.Diferencas = nota.Diferencas || !nota.ValidaLiquido;
                            }

                            nota.ValidaISS = nota.ValorISS == nota.ValorISSXml;
                            nota.Diferencas = nota.Diferencas || !nota.ValidaISS;

                            nota.ValidaINSS = nota.ValorINSS == nota.ValorINSSXml;
                            nota.Diferencas = nota.Diferencas || !nota.ValidaINSS;

                            nota.ValidaIR = nota.ValorIR == nota.ValorIRXml;
                            nota.Diferencas = nota.Diferencas || !nota.ValidaIR;

                            nota.ValidaCSLL = nota.ValorCSLL == nota.ValorCSLLXml;
                            nota.Diferencas = nota.Diferencas || !nota.ValidaCSLL;

                            nota.ValidaPis = nota.ValorPIS == nota.ValorPISXml;
                            nota.Diferencas = nota.Diferencas || !nota.ValidaPis;

                            nota.ValidaCofins = nota.ValorCofins == nota.ValorCofinsXml;
                            nota.Diferencas = nota.Diferencas || !nota.ValidaCofins;
                        }
                        _lista.Add(nota);
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

        public List<ItensNotasFiscaisServico> ListarItensNotaFiscaisEscrituracao(string tipoDocumento, string empresa, string tipo, DateTime inicial, DateTime final)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ItensNotasFiscaisServico> _lista = new List<ItensNotasFiscaisServico>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                
                query.Append("Select e.codintnf, e.documentoini numeroNf, e.entrada entradasaidanf, e.emissao dataemissaonf, e.codigoforn");
                query.Append("     , e.serie serienf, e.vlrservico valortotalnf, e.aliquotapis aliqpisnf, e.valorpis vlrpisnf");
                query.Append("     , e.aliquotacofins aliqconfinsnf, e.valorcofins vlrconfinsnf, e.aliquotainss aliqinssnf");
                query.Append("     , e.valorinss vlrinssnf, e.aliquotaiss aliqissnf, e.valoriss vlrissnf, e.aliquotairrf aliqirnf");
                query.Append("     , e.valorirrf ValorIR, e.aliquotacsll aliqcsllnf, e.aliquotacsll vlrcsllnf, i.QTDISS qtdeitensnf");
                query.Append("     , i.codtpdespesa, i.qtdiss, i.vlunit Valorunitarioitensnf, i.qtdiss* i.vlunit Valortotalitensnf, i.codoperfiscal");
                query.Append("     , i.codcontactb, i.codcusto, e.dtvencimento datavenctonf, i.codserv, i.compldescricao Descricaomat");
                query.Append("     , e.CODISSINT, 0 coddoctoesf ");
                query.Append("     , e.codigoEmpresa, e.codigofl, i.ITEMISS, '   ' sittributaria");

                query.Append("  From Esfiss_Entra e, Esfitemissentra i");
                query.Append(" Where e.emissao Between To_date('" + inicial.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + final.ToShortDateString() + "', 'dd/mm/yyyy')");

                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("   And Lpad(e.codigoempresa,3,'0') || '/' || lPad(e.codigofl,3,'0') in ('001/001','001/004')");
                else
                    query.Append("   And Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(e.Codigofl, 3, '0') = '" + empresa + "'");

                query.Append("   And e.codissint = i.codissint");
                //query.Append("   And e.sistema = 'ESF'");
                query.Append("   And (e.observacoes Not Like '%Origem Estoque' Or e.Observacoes Is Null)");
                query.Append("   And e.Codtpdoc = '" + tipoDocumento + "'");

                query.Append("   UNION ALL ");

                query.Append("Select e.codintnf, e.numeronf numeroNf, e.datahoraentsai entradasaidanf, e.dataEmissao dataemissaonf, e.codigoforn");
                query.Append("     , e.serienf serienf, e.valortotal valortotalnf, i.aliq_pis aliqpisnf, i.vl_pis vlrpisnf");
                query.Append("     ,  i.aliq_cofins aliqconfinsnf, i.Vl_Cofins vlrconfinsnf, 0 aliqinssnf");
                query.Append("     , 0 vlrinssnf, 0 aliqissnf, 0 vlrissnf, 0 aliqirnf");
                query.Append("     , 0 ValorIR, 0 aliqcsllnf, 0 vlrcsllnf, i.qtde qtdeitensnf");
                query.Append("     , i.codtpdespesa, i.qtde, i.vlrunitario Valorunitarioitensnf, i.qtde * i.vlrunitario Valortotalitensnf, i.codoperfiscal");
                query.Append("     , i.codcontactb, i.codcustoctb Codcusto, e.datavencto datavenctonf, null codserv, 'Nota fiscal' Descricaomat");
                query.Append("     , e.CODISSINT, e.coddoctoesf ");
                query.Append("     , e.codigoEmpresa, e.codigofl, i.coditem Itemiss, i.sittributaria");

                query.Append("  From Esfnotafiscal e, esfnotafiscal_item i");
                query.Append(" Where e.dataemissao Between To_date('" + inicial.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + final.ToShortDateString() + "', 'dd/mm/yyyy')");

                if (empresa == "001/001" || empresa == "001/004")
                    query.Append("   And Lpad(e.codigoempresa,3,'0') || '/' || lPad(e.codigofl,3,'0') in ('001/001','001/004')");
                else
                    query.Append("   And Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(e.Codigofl, 3, '0') = '" + empresa + "'");

                query.Append("   And e.codintnf = i.codintnf");
                //query.Append("   And e.sistema = 'ESF'");
                
                query.Append("   And e.Codtpdoc = '" + tipoDocumento + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ItensNotasFiscaisServico nota = new ItensNotasFiscaisServico();

                        nota.CodigoInternoNotaFiscal = Convert.ToDecimal(dataReader["CODISSINT"].ToString());

                        if (nota.CodigoInternoNotaFiscal == 0)
                            nota.CodigoInternoNotaFiscal = Convert.ToDecimal(dataReader["coddoctoesf"].ToString());

                        nota.IdItensISS = Convert.ToInt32(dataReader["ITEMISS"].ToString());
                        //nota.CFOP = Convert.ToInt32(dataReader["codclassfisc"].ToString());

                        nota.NumeroNotaFiscal = dataReader["numeronf"].ToString();
                        nota.Serie = dataReader["serienf"].ToString();
                        nota.DescricaoProduto = dataReader["descricaomat"].ToString();
                        //nota.Material = dataReader["codigointernomaterial"].ToString();
                        nota.CodigoTipoDeDespesa = dataReader["codtpdespesa"].ToString();
                        nota.SituacaoTributaria = dataReader["sittributaria"].ToString();
                        nota.CodCusto = dataReader["CodCusto"].ToString();

                        try
                        {
                            nota.OperacaoFiscal = Convert.ToInt32(dataReader["codoperfiscal"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.CodContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.CodigoServico = dataReader["codserv"].ToString();
                        }
                        catch { }

                        try
                        {
                            nota.Quantidade = Convert.ToInt32(dataReader["qtdeitensnf"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorUnitario = Convert.ToDecimal(dataReader["valorunitarioitensnf"].ToString());
                        }
                        catch { }

                        try
                        {
                            nota.ValorTotal = Convert.ToDecimal(dataReader["valortotalitensnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaPIS = Convert.ToDecimal(dataReader["aliqpisnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaCofins = Convert.ToDecimal(dataReader["aliqconfinsnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaINSS = Convert.ToDecimal(dataReader["aliqinssnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaISS = Convert.ToDecimal(dataReader["aliqissnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaIR = Convert.ToDecimal(dataReader["aliqirnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.AliquotaCSLL = Convert.ToDecimal(dataReader["aliqcsllnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorPIS = Convert.ToDecimal(dataReader["vlrpisnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorCofins = Convert.ToDecimal(dataReader["aliqconfinsnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorINSS = Convert.ToDecimal(dataReader["vlrinssnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorISS = Convert.ToDecimal(dataReader["vlrissnf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorIR = Convert.ToDecimal(dataReader["vlrimpostorendanf"].ToString());
                        }
                        catch { }
                        try
                        {
                            nota.ValorCSLL = Convert.ToDecimal(dataReader["vlrcsllnf"].ToString());
                        }
                        catch { }

                        _lista.Add(nota);
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

        public bool Conferir(List<NotasFiscaisServico> notas, List<ItensNotasFiscaisServico> itens)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                foreach (var item in notas)
                {
                    if (item.IdISS != 0 && item.CodDoctoEsf != item.IdISS)
                    {
                        query.Clear();
                        query.Append("Update Esfiss_Entra ");
                        query.Append("   set Conferido = '" + (item.Conferida ? "S" : "N") + "'");
                        if (item.Conferida)
                            query.Append("  , LogAltDados = '" + DateTime.Now.Date.ToShortDateString() + " - " + Publicas._usuario.UsuarioAcesso + "'");
                        else
                            query.Append("  , LogAltDados = null");

                        query.Append(" Where codissint = " + item.IdISS);

                        retorno = sessao.ExecuteSqlTransaction(query.ToString());

                        if (retorno)
                        {
                            foreach (var itemI in itens.Where(w => w.CodigoInternoNotaFiscal == item.IdISS))
                            {
                                query.Clear();
                                query.Append("Update Esfitemissentra ");
                                query.Append("   set CODSERV = '" + itemI.CodigoServico + "'");

                                query.Append(" Where codissint = " + itemI.CodigoInternoNotaFiscal);
                                query.Append("   and ITEMISS = " + itemI.IdItensISS);

                                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                            }
                        }

                    }
                    else
                    {

                        query.Clear();
                        query.Append("Update EsfEntra ");
                        query.Append("   set DocConciliado = '" + (item.Conferida ? "S" : "N") + "'");
                        if (item.Conferida)
                            query.Append("     , LogAltDados = '" + DateTime.Now.Date.ToShortDateString() + " - " + Publicas._usuario.UsuarioAcesso + "'");
                        else
                            query.Append("     , LogAltDados = null");
                        query.Append(" where CODDOCTOESF = " + item.CodDoctoEsf);

                        retorno = sessao.ExecuteSqlTransaction(query.ToString());
                    }
                    
                    if (retorno)
                    {
                        // Se estiver integrado irá gravar no CPG como liberado
                        if (item.Conferida && item.IntegradaCPG)
                        {
                            string codigoDocto = "";
                            string codigoDoctoSub = "";
                            bool temParcelas = false;

                            // Traz o Codigo interno do Docto e do Documento que o Substituiu
                            query.Clear();
                            query.Append("Select coddoctocpg, coddoctocpgsubst, nrodoctocpg, nroparcelacpg, seriedoctocpg,codtpdoc, codigoforn, codigoempresa, codigofl ");
                            query.Append("  from CPGDocto");
                            query.Append(" where coddoctocpg = " + item.CodigoDoctoCPG);

                            Query executar = sessao.CreateQuery(query.ToString());
                            dataReader = executar.ExecuteQuery();

                            using (dataReader)
                            {
                                if (dataReader.Read())
                                {
                                    codigoDocto = dataReader["coddoctocpg"].ToString();
                                    codigoDoctoSub = dataReader["coddoctocpgsubst"].ToString();


                                    // Traz o Codigo interno do Docto e do Documento que o Substituiu
                                    query.Clear();
                                    query.Append("Select coddoctocpg, coddoctocpgsubst, nrodoctocpg, nroparcelacpg, seriedoctocpg,codtpdoc, codigoforn, codigoempresa, codigofl  ");
                                    query.Append("  from CPGDocto");
                                    query.Append(" where coddoctocpg = " + codigoDocto);

                                    executar = sessao.CreateQuery(query.ToString());
                                    dataReader = executar.ExecuteQuery();

                                    using (dataReader)
                                    {
                                        if (dataReader.Read())
                                        {
                                            codigoDocto = dataReader["coddoctocpg"].ToString();
                                            codigoDoctoSub = dataReader["coddoctocpgsubst"].ToString();

                                            // Verifica se tem Parcelas;

                                            query.Clear();
                                            query.Append("Select coddoctocpg, coddoctocpgsubst");
                                            query.Append("  from CPGDocto");
                                            query.Append(" where nrodoctocpg = '" + dataReader["nrodoctocpg"].ToString() + "'");
                                            query.Append("   And seriedoctocpg = '" + dataReader["seriedoctocpg"].ToString() + "'");
                                            query.Append("   And codtpdoc = '" + dataReader["codtpdoc"].ToString() + "'");
                                            query.Append("   And codigoforn = " + dataReader["codigoforn"].ToString());
                                            query.Append("   And codigoempresa = " + dataReader["codigoempresa"].ToString());
                                            query.Append("   And codigofl = " + dataReader["codigofl"].ToString());
                                            query.Append("   And statusdoctocpg = 'N'");

                                            executar = sessao.CreateQuery(query.ToString());
                                            dataReader = executar.ExecuteQuery();
                                            using (dataReader)
                                            {
                                                while (dataReader.Read())
                                                {
                                                    temParcelas = true;

                                                    if (dataReader["coddoctocpgsubst"].ToString() == "")
                                                        codigoDocto = dataReader["coddoctocpg"].ToString();
                                                    else
                                                        codigoDocto = dataReader["coddoctocpgsubst"].ToString();

                                                    if (codigoDocto != "")
                                                        retorno = new ArquiveiDAO().LiberaDoctoCPG(codigoDocto, item.Conferida);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (!temParcelas)
                            {
                                if (codigoDoctoSub != "")
                                    codigoDocto = codigoDoctoSub;

                                if (codigoDocto != "")
                                    retorno = new ArquiveiDAO().LiberaDoctoCPG(codigoDocto, item.Conferida);
                            }
                        }
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

        public bool Revogar(List<NotasFiscaisServico> notas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            
            try
            {
                foreach (var item in notas)
                {
                    // exclui valor complementar NF
                    query.Clear();
                    query.Append("Delete Esfiss_Entravlcompitem ");
                    query.Append(" Where codissint = " + item.IdISS);

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (retorno)
                    {
                        // exclui o item da NF

                        query.Clear();
                        query.Append("Delete Esfitemissentra ");
                        query.Append(" Where codissint = " + item.IdISS);

                        retorno = sessao.ExecuteSqlTransaction(query.ToString());

                        if (retorno)
                        {
                            // exclui documento NF
                            query.Clear();
                            query.Append("Delete Esfiss_Entra ");
                            query.Append(" Where codissint = " + item.IdISS);

                            retorno = sessao.ExecuteSqlTransaction(query.ToString());

                            if (retorno)
                            {
                                // Exclui informacao de integração
                                query.Clear();
                                query.Append("Delete Niff_ESF_ISSIntegrado ");
                                query.Append(" Where codissint = " + item.IdISS);

                                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                            }
                            
                            if (retorno)
                            {
                                // Se estiver integrado irá gravar no CPG como liberado
                                if (item.IntegradaCPG)
                                {
                                    string codigoDocto = "";
                                    string codigoDoctoSub = "";
                                    bool temParcelas = false;

                                    // Traz o Codigo interno do Docto e do Documento que o Substituiu
                                    query.Clear();
                                    query.Append("Select coddoctocpg, coddoctocpgsubst, nrodoctocpg, nroparcelacpg, seriedoctocpg,codtpdoc, codigoforn, codigoempresa, codigofl ");
                                    query.Append("  from CPGDocto");
                                    query.Append(" where coddoctocpg = " + item.CodigoDoctoCPG);

                                    Query executar = sessao.CreateQuery(query.ToString());
                                    dataReader = executar.ExecuteQuery();

                                    using (dataReader)
                                    {
                                        if (dataReader.Read())
                                        {
                                            codigoDocto = dataReader["coddoctocpg"].ToString();
                                            codigoDoctoSub = dataReader["coddoctocpgsubst"].ToString();

                                            // Verifica se tem Parcelas;

                                            query.Clear();
                                            query.Append("Select coddoctocpg, coddoctocpgsubst");
                                            query.Append("  from CPGDocto");
                                            query.Append(" where nrodoctocpg = '" + dataReader["nrodoctocpg"].ToString() + "'");
                                            query.Append("   And seriedoctocpg = '" + dataReader["seriedoctocpg"].ToString() + "'");
                                            query.Append("   And codtpdoc = '" + dataReader["codtpdoc"].ToString() + "'");
                                            query.Append("   And codigoforn = " + dataReader["codigoforn"].ToString());
                                            query.Append("   And codigoempresa = " + dataReader["codigoempresa"].ToString());
                                            query.Append("   And codigofl = " + dataReader["codigofl"].ToString());
                                            query.Append("   And statusdoctocpg = 'N'");

                                            executar = sessao.CreateQuery(query.ToString());
                                            dataReader = executar.ExecuteQuery();

                                            using (dataReader)
                                            {
                                                while (dataReader.Read())
                                                {
                                                    temParcelas = true;

                                                    if (dataReader["coddoctocpgsubst"].ToString() == "")
                                                        codigoDocto = dataReader["coddoctocpg"].ToString();
                                                    else
                                                        codigoDocto = dataReader["coddoctocpgsubst"].ToString();

                                                    if (codigoDocto != "")
                                                        retorno = new ArquiveiDAO().LiberaDoctoCPG(codigoDocto, false);
                                                }
                                            }
                                        }
                                    }

                                    if (!temParcelas)
                                    {
                                        if (codigoDoctoSub != "")
                                            codigoDocto = codigoDoctoSub;

                                        if (codigoDocto != "")
                                            retorno = new ArquiveiDAO().LiberaDoctoCPG(codigoDocto, false);
                                    }
                                }
                            }
                        }
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

        public bool AtualizaStatusNFArquivei(List<Arquivei> arquivei)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int Id = 1;

            bool retorno = false;

            try
            {
                foreach (var item in arquivei)
                {
                    
                    
                    Id = item.Id;
                    query.Clear();
                    query.Append("Update Niff_FIS_Arquivei ");
                    query.Append("   set Status = '" + item.Status + "'");

                    if (item.IdUsuarioVisualizou != 0)
                    {
                        query.Append("     , IdUsuarioVisualizou = " + item.IdUsuarioVisualizou);
                        query.Append("     , DataVisualizou = sysDate");
                    }

                    query.Append("     , ComentarioUsuario = '" + item.ComentarioUsuario + "'");

                    query.Append(" Where IdArquivei = " + Id);
                
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
                                        
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

    }
}
