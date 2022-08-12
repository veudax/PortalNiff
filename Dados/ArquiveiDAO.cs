using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ArquiveiDAO
    {
        IDataReader dataReader;
        IDataReader dataReader2;
        IDataReader dataReader3;

        public List<Arquivei> Listar(DateTime Inicio, DateTime Fim, int IdEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Arquivei> _lista = new List<Arquivei>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Distinct i.cfop, a.idarquivei, dataimportado, nomearquivo, coddoctoesf, codintnf, e.idempresa, idusuariovisualizou, datavisualizou");
                query.Append("     , formatacnpjcpf(Lpad(cnpjdestinatario, 14, '0'), 'CNPJ') cnpjdestinatario");
                query.Append("     , iedestinatario, enderecodestinatario, bairrodestinatario, cepdestinatario, razaosocialdestinatario");
                query.Append("     , formatacnpjcpf(Lpad(a.Cnpjemitente, 14, '0'), 'CNPJ') cnpjemitente, ieemitente");
                query.Append("     , razaosocialemitente, chavedeacesso, dataemissao, numeronf, modelonf, serie, naturezaoperacao, valortotalnf, valorproduto, baseicms");
                query.Append("     , dadosadicionais, a.ComDiferencas, NumeroEndDestinatario, Tipo, Status, Operacao, e.NomeAbreviado, a.TipoDocto, a.TipoProcessamento ");

                query.Append("     , formatacnpjcpf(ea.Inscricaoempresa, 'CNPJ') CNPJEmpresaGlobus");
                query.Append("     , formatacnpjcpf(Lpad(a.CnpjTomador, 14, '0'), 'CNPJ') cnpjTomador ");
                query.Append("     , eliminanaonumericos(a.ieTomador) IETomador");
                query.Append("     , a.razaosocialTomador RSocialTomador");
                query.Append("     , a.enderecoTomador EnderecoTomador");
                query.Append("     , a.numeroendTomador NumeroEndTomador");
                query.Append("     , a.bairroTomador bairroTomador");
                query.Append("     , eliminanaonumericos(a.cepTomador) cepTomador");


                query.Append("  from NIFF_FIS_Arquivei a, NIFF_CHM_Empresas e, Niff_Fis_Itensarquivei i");
                query.Append("     , Ctr_Filial l");
                query.Append("     , Ctr_Empautorizadas ea");

                query.Append(" Where CodDoctoEsf is null and CodIntNF is null");
                query.Append("   and e.IdEmpresa = a.IdEmpresa");
                query.Append("   And i.Idarquivei = a.Idarquivei");
                query.Append("   And e.idEmpresa = " + IdEmpresa);

                query.Append("   And ea.codintempaut = l.codintempaut");
                query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') = e.codigoglobus");

                if (Inicio != DateTime.MinValue)
                {
                    query.Append("   And a.DataEmissao between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                    query.Append("   And To_date('" + Fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Arquivei _tipo = new Arquivei();

                        _tipo.Existe = true;
                        _tipo.CFOP = Convert.ToInt32(dataReader["CFOP"].ToString());
                        _tipo.Id = Convert.ToInt32(dataReader["idArquivei"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.NomeEmpresa = dataReader["NomeAbreviado"].ToString();
                        _tipo.NomeArquivo = dataReader["NomeArquivo"].ToString();
                        _tipo.CNPJDestinatario = dataReader["CnpjDestinatario"].ToString();
                        _tipo.IEDestinatario = dataReader["IEDestinatario"].ToString();
                        _tipo.EnderecoDestinatario = dataReader["EnderecoDestinatario"].ToString();
                        _tipo.BairroDestinatario = dataReader["BairroDestinatario"].ToString();
                        _tipo.CEPDestinatario = dataReader["cepdestinatario"].ToString();
                        _tipo.RazaoSocialDestinatario = dataReader["RazaoSocialDestinatario"].ToString();
                        _tipo.CNPJEmitente = dataReader["CnpjEmitente"].ToString();
                        _tipo.IEEmitente = dataReader["IEEmitente"].ToString();
                        _tipo.RazaoSocialEmitente = dataReader["RazaoSocialEmitente"].ToString();
                        _tipo.NumeroEndDestinatario = dataReader["NumeroEndDestinatario"].ToString();
                        _tipo.Tipo = dataReader["Tipo"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.Operacao = dataReader["Operacao"].ToString();
                        _tipo.TipoDocto = dataReader["TipoDocto"].ToString();

                        _tipo.ChaveDeAcesso = dataReader["ChaveDeAcesso"].ToString();
                        _tipo.NumeroNF = Convert.ToInt32(dataReader["NumeroNF"].ToString());
                        _tipo.ModeloNF = dataReader["ModeloNF"].ToString();
                        _tipo.Serie = dataReader["Serie"].ToString();
                        _tipo.NaturezaOperacao = dataReader["NaturezaOperacao"].ToString();
                        _tipo.DadosAdicionais = dataReader["DadosAdicionais"].ToString();
                        _tipo.TipoProcessamento = dataReader["TipoProcessamento"].ToString();
                        _tipo.ComDiferencas = dataReader["ComDiferencas"].ToString() == "S";

                        _tipo.ValorTotalNF = Convert.ToDecimal(dataReader["ValorTotalNF"].ToString());
                        _tipo.ValorProduto = Convert.ToDecimal(dataReader["ValorProduto"].ToString());
                        _tipo.BaseICMS = Convert.ToDecimal(dataReader["BaseICMS"].ToString());

                        try
                        {
                            _tipo.IdUsuarioVisualizou = Convert.ToInt32(dataReader["IdUsuarioVisualizou"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodIntNF = Convert.ToInt32(dataReader["CodIntNF"].ToString());
                        }
                        catch { }

                        try
                        {
                            //_tipo.CodDoctoESF = Convert.ToInt32(dataReader["CodDoctoESF"].ToString());
                            _tipo.CodDoctoESF = dataReader["CodDoctoESF"].ToString();
                        }
                        catch { }

                        try
                        {
                            _tipo.DataEmissao = Convert.ToDateTime(dataReader["DataEmissao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataVisualizou = Convert.ToDateTime(dataReader["DataVisualizou"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataImportado = Convert.ToDateTime(dataReader["DataImportado"].ToString());
                        }
                        catch { }

                        _tipo.CNPJEmpresaGlobus = dataReader["CNPJEmpresaGlobus"].ToString();

                        _tipo.CNPJTomador = dataReader["cnpjTomador"].ToString();
                        _tipo.IETomador = dataReader["IETomador"].ToString();
                        _tipo.RazaoSocialTomador = dataReader["RSocialTomador"].ToString();
                        _tipo.EnderecoTomador = dataReader["EnderecoTomador"].ToString();
                        _tipo.NumeroEndTomador = dataReader["NumeroEndTomador"].ToString();
                        _tipo.BairroTomador = dataReader["bairroTomador"].ToString();
                        _tipo.CEPTomador = dataReader["cepTomador"].ToString();

                        _tipo.TipoTomadorDestinatario = "Destinatário";

                        if (_tipo.CNPJTomador == _tipo.CNPJEmpresaGlobus && _tipo.CNPJTomador != "")
                        {
                            _tipo.CNPJDestinatario = dataReader["cnpjTomador"].ToString();
                            _tipo.IEDestinatario = dataReader["IETomador"].ToString();
                            _tipo.RazaoSocialDestinatario = dataReader["RSocialTomador"].ToString();
                            _tipo.EnderecoDestinatario = dataReader["EnderecoTomador"].ToString();
                            _tipo.BairroDestinatario = dataReader["bairroTomador"].ToString();
                            _tipo.CEPDestinatario = dataReader["cepTomador"].ToString();
                            _tipo.TipoTomadorDestinatario = "Tomador";
                        }

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

        public Arquivei Consulta(int IdEmpresa, string Chave, int Id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Arquivei _tipo = new Arquivei();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idarquivei, dataimportado, nomearquivo, coddoctoesf, codintnf, e.idempresa, idusuariovisualizou, datavisualizou, cnpjdestinatario");
                query.Append("     , iedestinatario, enderecodestinatario, bairrodestinatario, cepdestinatario, razaosocialdestinatario, cnpjemitente, ieemitente");
                query.Append("     , razaosocialemitente, chavedeacesso, dataemissao, numeronf, modelonf, serie, naturezaoperacao, valortotalnf, valorproduto, baseicms");
                query.Append("     , dadosadicionais, ComDiferencas, NumeroEndDestinatario, Tipo, Status, Operacao, e.NomeAbreviado, a.TipoProcessamento");
                query.Append("  from NIFF_FIS_Arquivei a, NIFF_CHM_Empresas e");

                query.Append(" Where e.IdEmpresa = a.IdEmpresa");
                query.Append("   and e.IdEmpresa = " + IdEmpresa);

                if (Id != 0)
                    query.Append("   and a.idarquivei = " + Id);
                else
                {
                    query.Append("   and a.chavedeacesso = '" + Chave + "'");
                    //query.Append("   and CodDoctoEsf is null and CodIntNF is null");
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idArquivei"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.NomeEmpresa = dataReader["NomeAbreviado"].ToString();
                        _tipo.NomeArquivo = dataReader["NomeArquivo"].ToString();
                        _tipo.CNPJDestinatario = dataReader["CnpjDestinatario"].ToString();
                        _tipo.IEDestinatario = dataReader["IEDestinatario"].ToString();
                        _tipo.EnderecoDestinatario = dataReader["EnderecoDestinatario"].ToString();
                        _tipo.BairroDestinatario = dataReader["BairroDestinatario"].ToString();
                        _tipo.CEPDestinatario = dataReader["cepdestinatario"].ToString();
                        _tipo.RazaoSocialDestinatario = dataReader["RazaoSocialDestinatario"].ToString();
                        _tipo.CNPJEmitente = dataReader["CnpjEmitente"].ToString();
                        _tipo.IEEmitente = dataReader["IEEmitente"].ToString();
                        _tipo.RazaoSocialEmitente = dataReader["RazaoSocialEmitente"].ToString();
                        _tipo.NumeroEndDestinatario = dataReader["NumeroEndDestinatario"].ToString();
                        _tipo.Tipo = dataReader["Tipo"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.Operacao = dataReader["Operacao"].ToString();
                        _tipo.TipoProcessamento = dataReader["TipoProcessamento"].ToString();

                        _tipo.ChaveDeAcesso = dataReader["ChaveDeAcesso"].ToString();
                        _tipo.NumeroNF = Convert.ToInt32(dataReader["NumeroNF"].ToString());
                        _tipo.ModeloNF = dataReader["ModeloNF"].ToString();
                        _tipo.Serie = dataReader["Serie"].ToString();
                        _tipo.NaturezaOperacao = dataReader["NaturezaOperacao"].ToString();
                        _tipo.DadosAdicionais = dataReader["DadosAdicionais"].ToString();
                        _tipo.ComDiferencas = dataReader["ComDiferencas"].ToString() == "S";

                        _tipo.ValorTotalNF = Convert.ToDecimal(dataReader["ValorTotalNF"].ToString());
                        _tipo.ValorProduto = Convert.ToDecimal(dataReader["ValorProduto"].ToString());
                        _tipo.BaseICMS = Convert.ToDecimal(dataReader["BaseICMS"].ToString());

                        try
                        {
                            _tipo.IdUsuarioVisualizou = Convert.ToInt32(dataReader["IdUsuarioVisualizou"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodIntNF = Convert.ToInt32(dataReader["CodIntNF"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodDoctoESF = dataReader["CodDoctoESF"].ToString();
                            //_tipo.CodDoctoESF = Convert.ToInt32(dataReader["CodDoctoESF"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataEmissao = Convert.ToDateTime(dataReader["DataEmissao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataVisualizou = Convert.ToDateTime(dataReader["DataVisualizou"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataImportado = Convert.ToDateTime(dataReader["DataImportado"].ToString());
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

        public Arquivei Consulta(int IdEmpresa, string nota, string CNPJEmitente)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Arquivei _tipo = new Arquivei();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idarquivei, dataimportado, nomearquivo, coddoctoesf, codintnf, e.idempresa, idusuariovisualizou, datavisualizou, cnpjdestinatario");
                query.Append("     , iedestinatario, enderecodestinatario, bairrodestinatario, cepdestinatario, razaosocialdestinatario, cnpjemitente, ieemitente");
                query.Append("     , razaosocialemitente, chavedeacesso, dataemissao, numeronf, modelonf, serie, naturezaoperacao, valortotalnf, valorproduto, baseicms");
                query.Append("     , dadosadicionais, ComDiferencas, NumeroEndDestinatario, Tipo, Status, Operacao, e.NomeAbreviado, a.TipoProcessamento");
                query.Append("  from NIFF_FIS_Arquivei a, NIFF_CHM_Empresas e");

                query.Append(" Where e.IdEmpresa = a.IdEmpresa");
                query.Append("   and e.IdEmpresa = " + IdEmpresa);

                query.Append("   and a.NumeroNf = '" + nota + "'");
                query.Append("   and lpad(a.cnpjemitente,20,'0') = lpad('" + CNPJEmitente + "',20,'0')");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idArquivei"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.NomeEmpresa = dataReader["NomeAbreviado"].ToString();
                        _tipo.NomeArquivo = dataReader["NomeArquivo"].ToString();
                        _tipo.CNPJDestinatario = dataReader["CnpjDestinatario"].ToString();
                        _tipo.IEDestinatario = dataReader["IEDestinatario"].ToString();
                        _tipo.EnderecoDestinatario = dataReader["EnderecoDestinatario"].ToString();
                        _tipo.BairroDestinatario = dataReader["BairroDestinatario"].ToString();
                        _tipo.CEPDestinatario = dataReader["cepdestinatario"].ToString();
                        _tipo.RazaoSocialDestinatario = dataReader["RazaoSocialDestinatario"].ToString();
                        _tipo.CNPJEmitente = dataReader["CnpjEmitente"].ToString();
                        _tipo.IEEmitente = dataReader["IEEmitente"].ToString();
                        _tipo.RazaoSocialEmitente = dataReader["RazaoSocialEmitente"].ToString();
                        _tipo.NumeroEndDestinatario = dataReader["NumeroEndDestinatario"].ToString();
                        _tipo.Tipo = dataReader["Tipo"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.Operacao = dataReader["Operacao"].ToString();
                        _tipo.TipoProcessamento = dataReader["TipoProcessamento"].ToString();

                        _tipo.ChaveDeAcesso = dataReader["ChaveDeAcesso"].ToString();
                        _tipo.NumeroNF = Convert.ToInt32(dataReader["NumeroNF"].ToString());
                        _tipo.ModeloNF = dataReader["ModeloNF"].ToString();
                        _tipo.Serie = dataReader["Serie"].ToString();
                        _tipo.NaturezaOperacao = dataReader["NaturezaOperacao"].ToString();
                        _tipo.DadosAdicionais = dataReader["DadosAdicionais"].ToString();
                        _tipo.ComDiferencas = dataReader["ComDiferencas"].ToString() == "S";

                        _tipo.ValorTotalNF = Convert.ToDecimal(dataReader["ValorTotalNF"].ToString());
                        _tipo.ValorProduto = Convert.ToDecimal(dataReader["ValorProduto"].ToString());
                        _tipo.BaseICMS = Convert.ToDecimal(dataReader["BaseICMS"].ToString());

                        try
                        {
                            _tipo.IdUsuarioVisualizou = Convert.ToInt32(dataReader["IdUsuarioVisualizou"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodIntNF = Convert.ToInt32(dataReader["CodIntNF"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodDoctoESF = dataReader["CodDoctoESF"].ToString();
                            //_tipo.CodDoctoESF = Convert.ToInt32(dataReader["CodDoctoESF"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataEmissao = Convert.ToDateTime(dataReader["DataEmissao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataVisualizou = Convert.ToDateTime(dataReader["DataVisualizou"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataImportado = Convert.ToDateTime(dataReader["DataImportado"].ToString());
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

        public List<Arquivei> Importados()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Arquivei> _lista = new List<Arquivei>();
            Publicas.mensagemDeErro = string.Empty;

            int qdtDias = (DateTime.Now.DayOfWeek == DayOfWeek.Monday ? 3 : 1);

            try
            {
                query.Append("Select Distinct trunc(dataimportado) DataImportado, nomearquivo, e.NomeAbreviado");
                query.Append("  from NIFF_FIS_Arquivei a, NIFF_CHM_Empresas e");
                query.Append(" Where CodDoctoEsf is null and CodIntNF is null");
                query.Append("   And e.IdEmpresa = a.IdEmpresa");
                query.Append("   And trunc(dataimportado)+ " + qdtDias + " >= trunc(Sysdate)");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Arquivei _tipo = new Arquivei();

                        _tipo.Existe = true;
                        _tipo.NomeEmpresa = dataReader["NomeAbreviado"].ToString();
                        _tipo.NomeArquivo = dataReader["NomeArquivo"].ToString();

                        try
                        {
                            _tipo.DataImportado = Convert.ToDateTime(dataReader["DataImportado"].ToString());
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

        public List<CFOPEmitidas> ListarCFOPEmitidas(int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<CFOPEmitidas> _lista = new List<CFOPEmitidas>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select a.*, c.descclassfisc SAIDA, l.DESCLEISVIG TextoLei, e.codigoglobus || ' - ' || e.nomeabreviado empresa");
                query.Append("  From niff_fis_cfopEmitidas a, esfclass c, est_cadleisvigentes L,  niff_chm_empresas e");
                query.Append(" Where a.cfop = c.codclassfisc ");
                query.Append("   and a.lei = l.CODLEISVIG");
                query.Append("   and e.IdEmpresa = " + idEmpresa);
                query.Append("   and e.IdEmpresa = a.IdEmpresa");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        CFOPEmitidas _cfop = new CFOPEmitidas();
                        _cfop.Existe = true;
                        _cfop.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _cfop.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _cfop.CFOPCodigo = Convert.ToInt32(dataReader["Cfop"].ToString());
                        _cfop.Lei = Convert.ToInt32(dataReader["Lei"].ToString());

                        _cfop.Natureza = dataReader["Natureza"].ToString();
                        _cfop.TextoLei = dataReader["TextoLei"].ToString();
                        _cfop.Empresa = dataReader["Empresa"].ToString();

                        _cfop.SerieGlobus = dataReader["serieglobus"].ToString();
                        _cfop.SerieComparar = dataReader["seriecompara"].ToString();

                        _cfop.CFOP = _cfop.CFOPCodigo + " - " + dataReader["Saida"].ToString();
                        try
                        {
                            _cfop.CST = Convert.ToInt32(dataReader["CST"].ToString());
                        }
                        catch { }

                        try
                        {
                            _cfop.Operacao = Convert.ToInt32(dataReader["Operacao"].ToString());
                        }
                        catch { }

                        _lista.Add(_cfop);
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

        public bool GravaCFOPEmitidas(CFOPEmitidas _cfop)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!_cfop.Existe)
                {
                    query.Clear();
                    query.Append("Insert into niff_fis_cfopEmitidas (");
                    query.Append("       id, cfop, natureza, lei, cst, operacao, idEmpresa");
                    query.Append("     , SerieGlobus, SerieCompara");
                    query.Append("  ) Values ( (select Nvl(Max(id),0) +1 next from niff_fis_cfopEmitidas) ");
                    query.Append("  , " + _cfop.CFOPCodigo);
                    query.Append("  , '" + _cfop.Natureza + "'");
                    query.Append("  , " + _cfop.Lei);
                    query.Append("  , " + (_cfop.CST == 0 || _cfop.CST == null ? "null" : _cfop.CST.ToString()));
                    query.Append("  , " + (_cfop.Operacao == 0 || _cfop.Operacao == null ? "null" : _cfop.Operacao.ToString()));
                    query.Append("  , " + _cfop.IdEmpresa);
                    query.Append("  , '" + _cfop.SerieGlobus + "'");
                    query.Append("  , '" + _cfop.SerieComparar + "'");

                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update niff_fis_cfopEmitidas ");
                    query.Append("   set CST = " + (_cfop.CST == 0 || _cfop.CST == null ? "null" : _cfop.CST.ToString()));
                    query.Append("     , Operacao = " + (_cfop.Operacao == 0 || _cfop.Operacao == null ? "null" : _cfop.Operacao.ToString()));
                    query.Append("     , lei = " + _cfop.Lei);
                    query.Append("     , SerieGlobus = '" + _cfop.SerieGlobus + "'");
                    query.Append("     , SerieCompara = '" + _cfop.SerieComparar + "'");
                    query.Append(" Where Id = " + _cfop.Id);
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

        public bool GravaNatureza(string naturezaOld, string naturezaNew)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Update niff_fis_cfopEmitidas ");
                query.Append("   set natureza = '" + naturezaNew + "'");
                query.Append(" Where natureza = '" + naturezaOld + "'");                

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

        public bool GravaSerieEmitidas(int idEmpresa, string serieG, string serieC)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Update niff_fis_cfopEmitidas ");
                query.Append("   set SerieGlobus = '" + serieG + "'");
                query.Append("     , SerieCompara = '" + serieC + "'");
                query.Append(" Where IdEmpresa = " + idEmpresa);

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

        public bool ExcluiCFOPEmitidas(int Id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Delete niff_fis_cfopEmitidas ");
                query.Append(" Where Id = " + Id);

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

        public List<CFOPeCST> ListarCFOP(string _tipo = "E")
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<CFOPeCST> _lista = new List<CFOPeCST>();
            Publicas.mensagemDeErro = string.Empty;

            try 
            {
                query.Append("Select a.*, c.descclassfisc SAIDA, e.descclassfisc ENTRADA");
                query.Append("  From Niff_Fis_CFOPCST a, esfclass c, esfclass e  ");
                query.Append(" Where a.cfopsaida = c.codclassfisc(+)   ");
                query.Append("   And a.cfopentrada = e.codclassfisc(+) ");
                query.Append("   And (a.Tipo = '" + _tipo + "' or a.Tipo is null)");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                 
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        CFOPeCST _cfop = new CFOPeCST();
                        _cfop.Existe = true;
                        _cfop.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _cfop.CFOPEntrada = Convert.ToInt32(dataReader["CfopEntrada"].ToString());
                        _cfop.CFOPSaida = Convert.ToInt32(dataReader["CfopSaida"].ToString());

                        try
                        {
                            _cfop.CST = Convert.ToInt32(dataReader["CST"].ToString());
                        }
                        catch { }

                        _cfop.CFOPE = _cfop.CFOPEntrada + " - " + dataReader["Entrada"].ToString();
                        _cfop.CFOP = _cfop.CFOPSaida + " - " + dataReader["Saida"].ToString();
                        _cfop.Tipo = dataReader["Tipo"].ToString();

                        try
                        {
                            _cfop.Operacao = Convert.ToInt32(dataReader["Operacao"].ToString());
                        }
                        catch { }

                        _lista.Add(_cfop);
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

        public CFOPeCST ConsultaCFOP(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            CFOPeCST _cfop = new CFOPeCST();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select * from Niff_Fis_CFOPCST");
                query.Append(" where CFOPSAIDA = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _cfop.Existe = true;
                        _cfop.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _cfop.CFOPEntrada = Convert.ToInt32(dataReader["CfopEntrada"].ToString());
                        _cfop.CFOPSaida = Convert.ToInt32(dataReader["CfopSaida"].ToString());
                        _cfop.Tipo = dataReader["Tipo"].ToString();
                        try
                        {
                            _cfop.CST = Convert.ToInt32(dataReader["CST"].ToString());
                        }
                        catch { }

                        try
                        {
                            _cfop.Operacao = Convert.ToInt32(dataReader["Operacao"].ToString());
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

            return _cfop;
        }

        public bool GravaCFOP(CFOPeCST _cfop)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!_cfop.Existe)
                {
                    query.Clear();
                    query.Append("Insert into Niff_Fis_CFOPCST (");
                    query.Append("       id, cfopsaida, cfopentrada, cst, operacao, tipo");
                    query.Append("  ) Values ( (select Nvl(Max(id),0) +1 next from Niff_Fis_CFOPCST) ");
                    query.Append("  , " + _cfop.CFOPSaida);
                    query.Append("  , " + _cfop.CFOPEntrada);
                    query.Append("  , " + (_cfop.CST == 0 || _cfop.CST == null ? "null" : _cfop.CST.ToString()));
                    query.Append("  , " + (_cfop.Operacao == 0 || _cfop.Operacao == null ? "null" : _cfop.Operacao.ToString()));
                    query.Append("  , 'E'");
                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Fis_CFOPCST ");
                    query.Append("   set CFOPEntrada = " + _cfop.CFOPEntrada);
                    query.Append("     , CST = " + (_cfop.CST == 0 || _cfop.CST == null ? "null" : _cfop.CST.ToString()));
                    query.Append("     , Operacao = " + (_cfop.Operacao == 0 || _cfop.Operacao == null ? "null" : _cfop.Operacao.ToString()));
                    query.Append("     , CFOPSaida = " + _cfop.CFOPSaida);
                    query.Append("     , Tipo = 'E'");
                    query.Append(" Where Id = " + _cfop.Id);
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

        public bool ExcluiCFOP(int Id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Delete Niff_Fis_CFOPCST ");
                query.Append(" Where Id = " + Id);

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

        public bool GravaStatus(List<Arquivei> arquivei)
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
                    if (item.Existe)
                    {
                        Id = item.Id;
                        query.Clear();
                        query.Append("Update Niff_FIS_Arquivei ");
                        query.Append("   set Status = '" + item.Status + "'");
                        query.Append(" Where IdArquivei = " + Id);
                    }

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                    {
                        Log _log = new Log();
                        _log.IdUsuario = Publicas._usuario.Id;
                        _log.Descricao = "Erro ao gravar a Nota Fiscal Arquivei " + item.NumeroNF + " chave " + item.ChaveDeAcesso + " empresa " + item.IdEmpresa +
                            " Emissao " + item.DataEmissao.ToShortDateString();
                        _log.Tela = "Principal - Arquivei - Erro ao gravar";

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

        public bool Grava(List<Arquivei> arquivei, List<ItensArquivei> itens, bool Conferir)
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
                    if (!item.Existe)
                    {
                        query.Clear();
                        query.Append("Select SQ_NIFF_IDArquivei.NextVal next From dual");
                        Query executar = sessao.CreateQuery(query.ToString());

                        dataReader = executar.ExecuteQuery();

                        using (dataReader)
                        {
                            if (dataReader.Read())
                                Id = Convert.ToInt32(dataReader["next"].ToString());
                        }

                        query.Clear();
                        query.Append("Insert into niff_fis_arquivei (");
                        query.Append("       idarquivei, dataimportado, nomearquivo, idempresa, cnpjdestinatario, iedestinatario, enderecodestinatario");
                        query.Append("     , bairrodestinatario, cepdestinatario, razaosocialdestinatario, cnpjemitente, ieemitente, razaosocialemitente");
                        query.Append("     , chavedeacesso, dataemissao, numeronf, modelonf, serie, naturezaoperacao, valortotalnf, valorproduto, baseicms");
                        query.Append("     , dadosadicionais, NumeroEndDestinatario, Tipo, Status, Operacao, TipoArquivo, TipoDocto, MunicOrigem");
                        query.Append("     , MunicDestino, CNPJTomador, IETomador, ENDERECOTomador, BAIRROTomador, CEPTomador, RAZAOSOCIALTomador, NUMEROENDTomador");
                        query.Append("     , Tributacao, OpcaoSimples, ValorServico, CodigoServico, AliquotaServico, ValorISS, ValorCredito, ISSRetido, Discriminacao");
                        query.Append("     , ValorPis, ValorCofins, ValorIR, ValorCsll, ValorInss, DataCancelamento, TipoProcessamento, idnfeGlobus");

                        query.Append("  ) Values ( " + Id);
                        query.Append("  , sysdate ");
                        query.Append("  , '" + item.NomeArquivo + "'");
                        query.Append("  , " + item.IdEmpresa);
                        if (item.CNPJDestinatario == null || string.IsNullOrEmpty(item.CNPJDestinatario.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.CNPJDestinatario.Replace(".", "").Replace("/", "").Replace("-", "") + "'");

                        if (item.IEDestinatario == null || string.IsNullOrEmpty(item.IEDestinatario.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.IEDestinatario.Replace(" ", "") + "'");

                        if (item.EnderecoDestinatario == null || string.IsNullOrEmpty(item.EnderecoDestinatario.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.EnderecoDestinatario.Replace("'", "") + "'");

                        if (item.BairroDestinatario == null || string.IsNullOrEmpty(item.BairroDestinatario.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.BairroDestinatario.Replace("'", "") + "'");

                        query.Append("  , '" + item.CEPDestinatario + "'");

                        if (item.RazaoSocialDestinatario == null || string.IsNullOrEmpty(item.RazaoSocialDestinatario.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.RazaoSocialDestinatario.Replace("'", "").Replace("&", "") + "'");

                        if (item.CNPJEmitente == null || string.IsNullOrEmpty(item.CNPJEmitente.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.CNPJEmitente.Replace(".", "").Replace("/", "").Replace("-", "") + "'");

                        if (item.IEEmitente == null || string.IsNullOrEmpty(item.IEEmitente.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.IEEmitente.Replace(" ", "") + "'");

                        if (item.RazaoSocialEmitente == null || string.IsNullOrEmpty(item.RazaoSocialEmitente.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.RazaoSocialEmitente.Replace("'", "").Replace("&", "") + "'");

                        query.Append("  , '" + item.ChaveDeAcesso.Replace(" ", "") + "'");
                        query.Append("  , To_Date('" + item.DataEmissao.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("  , '" + item.NumeroNF + "'");
                        query.Append("  , '" + item.ModeloNF + "'");
                        query.Append("  , '" + item.Serie + "'");

                        if (item.NaturezaOperacao == null || string.IsNullOrEmpty(item.NaturezaOperacao.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.NaturezaOperacao.Replace("'", "") + "'");

                        query.Append("  , " + item.ValorTotalNF.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorProduto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.BaseICMS.ToString().Replace(".", "").Replace(",", "."));

                        if (string.IsNullOrEmpty(item.DadosAdicionais))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.DadosAdicionais.Replace("'", "").Replace("&", "") + "'");

                        if (string.IsNullOrEmpty(item.NumeroEndDestinatario))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.NumeroEndDestinatario.Replace("'", "").Replace("&", "") + "'");

                        query.Append("  , '" + item.Tipo + "'");
                        query.Append("  , '" + item.Status + "'");
                        query.Append("  , '" + item.Operacao + "'");
                        query.Append("  , '" + item.TipoArquivo + "'");

                        query.Append("  , '" + item.TipoDocto + "'");
                        query.Append("  , '" + item.MunicipioOrigem + "'");
                        query.Append("  , '" + item.MunicipioDestino + "'");

                        if (item.CNPJTomador == null || string.IsNullOrEmpty(item.CNPJTomador.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.CNPJTomador.Replace(".", "").Replace("/", "").Replace("-", "") + "'");

                        if (item.IETomador == null || string.IsNullOrEmpty(item.IETomador.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.IETomador.Replace(" ", "") + "'");

                        if (item.EnderecoTomador == null || string.IsNullOrEmpty(item.EnderecoTomador.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.EnderecoTomador.Replace("'", "") + "'");

                        if (item.BairroTomador == null || string.IsNullOrEmpty(item.BairroTomador.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.BairroTomador.Replace("'", "") + "'");

                        query.Append("  , '" + item.CEPTomador + "'");

                        if (item.RazaoSocialTomador == null || string.IsNullOrEmpty(item.RazaoSocialTomador.Trim()))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.RazaoSocialTomador.Replace("'", "").Replace("&", "") + "'");

                        if (string.IsNullOrEmpty(item.NumeroEndTomador))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.NumeroEndTomador.Replace("'", "").Replace("&", "") + "'");

                        if (string.IsNullOrEmpty(item.Tributacao))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.Tributacao + "'");

                        if (string.IsNullOrEmpty(item.OpcaoSimples))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.OpcaoSimples + "'");

                        query.Append("  , " + item.ValorServico.ToString().Replace(".", "").Replace(",", "."));
                        if (string.IsNullOrEmpty(item.CodigoServico))
                            query.Append("  , null");
                        else
                            query.Append("  , '" + item.CodigoServico + "'");

                        query.Append("  , " + item.AliquotaServico.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorISS.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorCredito.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , '" + (item.ISSRetido ? "S" : "N") + "'");

                        if (string.IsNullOrEmpty(item.Discriminacao))
                            query.Append("  , null");
                        else
                        {
                            if (item.Discriminacao.Length > 4000)
                                query.Append("  , '" + item.Discriminacao.Substring(0, 3999) + "'");
                            else
                                query.Append("  , '" + item.Discriminacao + "'");
                        }

                        query.Append("  , " + item.ValorPis.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorCofins.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorIR.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorCSLL.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("  , " + item.ValorINSS.ToString().Replace(".", "").Replace(",", "."));

                        if (item.DataCancelamento == DateTime.MinValue)
                            query.Append("  , null");
                        else
                            query.Append("  , To_date('" + item.DataCancelamento.ToShortDateString() + "', 'dd/mm/yyyy')");

                        query.Append("  , '" + (item.TipoProcessamento == null ? "Recebidos" : item.TipoProcessamento) + "'");

                        if (item.IdNFEGlobus == 0)
                            query.Append("  , null");
                        else
                            query.Append("  , " + item.IdNFEGlobus);


                        query.Append(") ");
                    }
                    else
                    {
                        Id = item.Id;
                        query.Clear();
                        query.Append("Update Niff_FIS_Arquivei ");
                        query.Append("   set ComDiferencas = '" + (item.ComDiferencas ? "S" : "N") + "'");

                        if (item.IdUsuarioVisualizou != 0)
                        {
                            query.Append("     , IdUsuarioVisualizou = " + item.IdUsuarioVisualizou);
                            query.Append("     , DataVisualizou = sysDate");
                        }

                        query.Append("     , liberado = '" + (item.Liberado ? "S" : "N") + "'");
                        query.Append("     , observacaodofiscal = '" + item.Observacao + "'");

                        query.Append("     , Conferido = '" + (item.Conferido ? "S" : "N") + "'");

                        if (item.Conferido)
                            query.Append("     , DataConferido = sysdate");
                        else
                            query.Append("     , DataConferido = null");

                        if (item.DataCancelamento != DateTime.MinValue)
                            query.Append("     , DataConferido = To_date('" + item.DataCancelamento.ToShortDateString() + "','dd/mm/yyyy')");

                        if (item.CodIntNF != 0)
                            query.Append("     , CodIntNF = " + item.CodIntNF);

                        if (item.CodDoctoESF != "")
                            query.Append("     , CodDoctoESF = '" + item.CodDoctoESF + "'");

                        query.Append(" Where IdArquivei = " + Id);
                    }

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                    {
                        Log _log = new Log();
                        _log.IdUsuario = 1;
                        _log.Descricao = "Erro ao gravar a Nota Fiscal Arquivei " + item.NumeroNF + " chave " + item.ChaveDeAcesso + " empresa " + item.IdEmpresa +
                            " Emissao " + item.DataEmissao.ToShortDateString() + Publicas.mensagemDeErro;
                        _log.Tela = "Principal - Arquivei - Erro ao gravar";

                        try
                        {
                            new LogDAO().Gravar(_log);
                        }
                        catch { }
                        return false;

                    }
                    else
                    {
                        if (Conferir)
                            retorno = AtualizaNotaESFComoConferido(item.CodDoctoESF, item.Conferido, item.CodIntNF, item.Origem, item.IntegradoCPG, item.IntegradoESF, item.TipoProcessamento);

                        if (itens.Count() != 0)
                        {
                            List<ItensArquivei> _lista = itens.Where(w => w.IdArquivei == item.Id).ToList();
                            _lista.ForEach(u => u.IdArquivei = Id);
                            retorno = new ItensArquiveiDAO().Grava(_lista);

                            if (!retorno)
                                return false;
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

        public bool AtualizaNotaESFComoConferido(string codigo, bool conferir, decimal codigoNF, string origem, bool integradoCPG, bool integradoESF, string tipoProcessamento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                query.Clear();
                if (tipoProcessamento == "Recebidos")
                    query.Append("Update EsfEntra ");
                else
                    query.Append("Update EsfSaida ");

                query.Append("   set DocConciliado = '" + (conferir ? "S" : "N") + "'");
                if (conferir)
                    query.Append("     , LogAltDados = '" + DateTime.Now.Date.ToShortDateString() + " - " + Publicas._usuario.UsuarioAcesso + "'");
                else
                    query.Append("     , LogAltDados = null");

                query.Append(" where CODDOCTOESF in (" + codigo + ")");
                //query.Append(" where CODDOCTOESF = " + codigo);

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (!retorno)
                    return false;

                // Libera o Docto no CPG
                if (origem == "ESF" && integradoCPG && tipoProcessamento == "Recebidos")
                {
                    string codigoDocto = "";
                    string codigoDoctoSub = "";
                    bool temParcelas = false;

                    // Traz o Codigo interno do Docto e do Documento que o Substituiu
                    query.Clear();
                    query.Append("Select coddoctocpg, coddoctocpgsubst, nrodoctocpg, nroparcelacpg, seriedoctocpg,codtpdoc, codigoforn, codigoempresa, codigofl ");
                    query.Append("  from CPGDocto");
                    query.Append(" where CODDOCTOESF in (" + codigo + ")");
                    //query.Append(" where CODDOCTOESF = " + codigo);

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
                                        retorno = LiberaDoctoCPG(codigoDocto, conferir);
                                }
                            }
                        }
                    }

                    // Atualiza documento CPG 
                    if (!temParcelas)
                    {
                        if (codigoDoctoSub != "")
                            codigoDocto = codigoDoctoSub;

                        if (codigoDocto != "")
                            retorno = LiberaDoctoCPG(codigoDocto, conferir);
                    }
                }

                if (origem == "EST" && integradoCPG && tipoProcessamento == "Recebidos")
                {
                    string codigoDocto = "";
                    string codigoDoctoSub = "";
                    bool temParcelas = false;

                    // Traz o Codigo interno do Docto do CPG
                    query.Clear();
                    query.Append("Select coddoctocpg ");
                    query.Append("  from Bgm_Notafiscal");
                    query.Append(" where codintnf = " + codigoNF);

                    Query executar = sessao.CreateQuery(query.ToString());
                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                        {
                            codigoDocto = dataReader["coddoctocpg"].ToString();

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
                                                retorno = LiberaDoctoCPG(codigoDocto, conferir);
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
                            retorno = LiberaDoctoCPG(codigoDocto, conferir);
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

        public bool LiberaDoctoCPG(string coddocto, bool conferir)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Update CPGDocto ");
                query.Append("   set PagamentoLiberado = '" + (conferir ? "S" : "N") + "'");
                if (conferir)
                {
                    query.Append("     , Usuario_Liberou_pagto = '" + Publicas._usuario.UsuarioAcesso + "'");
                    query.Append("     , DataLiberacaoPgto = To_date('" + DateTime.Now.Date.ToShortDateString() + "','dd/mm/yyyy')");
                }
                else
                {
                    query.Append("     , Usuario_Liberou_pagto = null");
                    query.Append("     , DataLiberacaoPgto = null");
                }

                query.Append(" where coddoctocpg = " + coddocto);

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

        private string EstaIntegradoContasPagar(string coddoctoesf, string tipoProcessamento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            string codigo = "";

            try
            {
                query.Clear();
                if (tipoProcessamento == "Recebidos")
                {
                    query.Append("Select coddoctocpg ");
                    query.Append("  from CPGDocto");
                }
                else
                {
                    query.Append("Select coddoctocrc coddoctocpg ");
                    query.Append("  from CRCDocto");
                }
                query.Append(" where CodDoctoESF in (" + coddoctoesf + ")");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader2 = executar.ExecuteQuery();

                using (dataReader2)
                {
                    if (dataReader2.Read())
                        codigo = dataReader2["coddoctocpg"].ToString();
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

            return codigo;
        }

        private string UsuarioDocumentoESF(string coddoctoesf)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            string codigo = "";

            try
            {
                query.Clear();
                query.Append("Select Distinct u.Nomeusuario ");
                query.Append("  from EsfEntra e, ctr_cadastrodeusuarios u");
                query.Append(" where CodDoctoESF in (" + coddoctoesf + ")");
                query.Append("   and e.usuario = u.usuario");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader2 = executar.ExecuteQuery();

                using (dataReader2)
                {
                    if (dataReader2.Read())
                    {
                        codigo = dataReader2["Nomeusuario"].ToString();
                        try
                        {
                            codigo = codigo.ToUpper()
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

                            codigo = codigo.Trim();
                            string[] nomes = codigo.Split(' ');
                            codigo = nomes[0] + " " + nomes[nomes.Length - 1];
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

            return codigo;
        }

        public List<NotasArquivei> ListarParaComparar(int IdEmpresa, DateTime Inicio, DateTime fim, string Conferidas, bool dataEntrada, string tipoProcessamento = "Recebidos")
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<NotasArquivei> _lista = new List<NotasArquivei>();
            List<NotasArquivei> _listaExcluir = new List<NotasArquivei>();
            Publicas.mensagemDeErro = string.Empty;
            int idParametro = 0;

            try
            {
                //Estoque - Recebidas
                if (tipoProcessamento == "Recebidos")
                {
                    query.Append("Select N.Codigoempresa, N.Codigofl, N.Codtpdoc, To_Number(n.Numeronf) NumeroNFGlobus, a.NumeroNF NumeroNFArquivo");
                    query.Append("     , n.Serienf SerieGlobus, a.Serie SerieArquivo, n.dataemissaonf EmissaoGlobus, a.DataEmissao EmissaoArquivo");
                    query.Append("     , f.Nrforn, n.entradasaidanf, n.naturezaoperacaonf naturezaoperacaoGlobus, a.naturezaoperacao NaturezaOperacaoArquivo ");
                    query.Append("     , n.basecalcicmsnf baseICMSGlobus, a.BaseICMS BaseICMSArquivo, n.Icms_Isentanf, n.Icms_Outrasnf");
                    query.Append("     , n.valortotalnf ValorTotalGlobus, a.ValorTotalNF ValorTotalArquivo, To_Char(n.coddoctoesf) coddoctoesf, n.CodIntNF");
                    query.Append("     , n.chavedeacessonfe chavedeacessoGlobus, a.chavedeacesso chavedeacessoArquivo, a.DATAIMPORTADO");
                    query.Append("     , n.codmodelo ModeloGlobus, a.ModeloNF ModeloArquivo");
                    query.Append("     , formatacnpjcpf(f.Nrinscricaoforn, 'CNPJ') CNPJForncedorGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.Cnpjemitente, 14, '0'), 'CNPJ') cnpjFornecedorArquivo");
                    query.Append("     , eliminanaonumericos(f.inscestadualforn) IEFornecedorGlobus, eliminanaonumericos(a.ieemitente) IEFornecedorArquivo");
                    query.Append("     , f.rsocialforn RSocialFornecedorGlobus, a.razaosocialemitente RSocialFornecedorArquivo");
                    query.Append("     , formatacnpjcpf(e.Inscricaoempresa, 'CNPJ') CNPJEmpresaGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.Cnpjdestinatario, 14, '0'), 'CNPJ') cnpjEmpresaArquivo ");
                    query.Append("     , eliminanaonumericos(l.iestadualfl) IEEmpresaGlobus, eliminanaonumericos(a.iedestinatario) IEEmpresaArquivo");
                    query.Append("     , e.rsocialempresa RSocialEmpresaGlobus, a.razaosocialdestinatario RSocialEmpresaArquivo");
                    query.Append("     , l.enderecofl EnderecoGlobus, a.enderecodestinatario EnderecoArquivo");
                    query.Append("     , l.numeroendfl NumeroEndGlobus, a.numeroenddestinatario NumeroEndArquivo");
                    //query.Append("     , Nvl((Select Distinct esf.Docconciliado From Esfentra esf Where esf.coddoctoesf = n.coddoctoesf),'N') Conferido");
                    query.Append("     , 'N' Conferido, a.DataConferido");
                    query.Append("     , l.bairrofl bairroGlobus, a.bairrodestinatario bairroArquivo, a.liberado, a.observacaoDoFiscal");
                    query.Append("     , l.cepfl CEPGlobus, eliminanaonumericos(a.cepdestinatario) cepArquivo, 'EST' Origem");
                    query.Append("     , a.IdEmpresa, a.idArquivei, a.Tipo, a.Status, a.Operacao, a.DadosAdicionais, a.ComDiferencas, p.Idparametro");
                    query.Append("     , (Select Round(Sum(Valorproduto),2) valorProduto");
                    query.Append("          From (Select Nvl(Sum(o.valorunitarioitensnf * o.qtdeitensnf) , 0) valorProduto, CodIntNF");
                    query.Append("                  From est_itensnf o");
                    query.Append("                 Group By CodIntNF");
                    query.Append("                 Union All");
                    query.Append("                Select Nvl(Sum(e.Valornfservico), 0) valorProduto, CodIntNF");
                    query.Append("                  From Est_Nfservico e"); 
                    query.Append("                 Group By CodIntNF) x");
                    query.Append("         Where x.CodIntNF = n.CodIntNF ) VlProdutoGlobus, a.valorproduto VlProdutoArquivo");

                    query.Append("     , (Select Round(Sum(DescFrete),2) DescFrete");
                    query.Append("          From (Select Nvl(sum(o.VALORDESCONTOITENSNF),0) + Nvl(Sum(o.VALORFRETEITENSNF),0) DescFrete, CodIntNF");
                    query.Append("                  From est_itensnf o");
                    query.Append("                 Group By CodIntNF");
                    query.Append("                 Union All");
                    query.Append("                Select Nvl(sum(e.vlrdescontonfserv),0) + Nvl(Sum(e.valorfrete),0) DescFrete, CodIntNF");
                    query.Append("                  From Est_Nfservico e");
                    query.Append("                 Group By CodIntNF) x");
                    query.Append("         Where x.CodIntNF = n.CodIntNF ) VlDescFreteItens ");

                    query.Append("     , Decode(n.StatusNF, 'C', 'Cancelada', 'D', 'Rejeitada', 'I','Inutilizada', 'Ativa' ) StatusNF, a.TipoArquivo");
                    query.Append("     , n.codmunicfederal_origem MunicOrigemGlobus, n.codmunicfederal_destino MunicDestinoGlobus");
                    query.Append("     , a.municorigem, a.municdestino, n.CodDoctoCPG");

                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjTomador, 14, '0'), 'CNPJ') cnpjTomador ");
                    query.Append("     , eliminanaonumericos(a.ieTomador) IETomador");
                    query.Append("     , a.razaosocialTomador RSocialTomador");
                    query.Append("     , a.enderecoTomador EnderecoTomador");
                    query.Append("     , a.numeroendTomador NumeroEndTomador");
                    query.Append("     , a.bairroTomador bairroTomador");
                    query.Append("     , eliminanaonumericos(a.cepTomador) cepTomador, n.valordescontonf, n.valorfretenf");
                    query.Append("  From Bgm_Notafiscal N");
                    query.Append("     , Bgm_Fornecedor F");
                    query.Append("     , Niff_Fis_Arquivei a ");
                    query.Append("     , Ctr_Empautorizadas e");
                    query.Append("     , Ctr_Filial l");
                    query.Append("     , Niff_Fis_Parametrosarquivei p");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append(" Where n.codigoforn = f.codigoforn");
                    query.Append("   And n.chavedeacessonfe = a.chavedeacesso");
                    query.Append("   And e.codintempaut = l.codintempaut");
                    query.Append("   And l.Codigoempresa = n.codigoEmpresa");
                    query.Append("   And l.codigofl = n.codigofl");
                    query.Append("   And p.Idempresa = a.Idempresa");
                    query.Append("   And a.idEmpresa = " + IdEmpresa);
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Recebidos'");

                    if (!dataEntrada)
                    {
                        query.Append("   And a.DataEmissao between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.entradasaidanf between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') = em.codigoglobus");

                    // if (ApenasConferidas)
                    //     query.Append("   And a.Conferido = 'S'");

                    //query.Append("   And a.idarquivei = 62189");

                    query.Append(" Union All ");

                    query.Append("Select N.Codigoempresa, N.Codigofl, N.Codtpdoc, To_Number(n.nrdocentra) NumeroNFGlobus, a.NumeroNF NumeroNFArquivo");
                    query.Append("     , n.serieentra SerieGlobus, a.Serie SerieArquivo, n.dtemissaoentra EmissaoGlobus, a.DataEmissao EmissaoArquivo");
                    query.Append("     , f.Nrforn, n.dtentradaentra, null naturezaoperacaoGlobus, a.naturezaoperacao NaturezaOperacaoArquivo ");

                    query.Append("     , Sum(n.Icmsbaseentra) baseICMSGlobus, a.BaseICMS BaseICMSArquivo, Sum(n.Icmsisentaentra) Icms_Isentanf, Sum(n.Icmsoutrasentra) Icms_Outrasnf");
                    query.Append("     , Sum(n.vlcontabilentra) ValorTotalGlobus, a.ValorTotalNF ValorTotalArquivo");
                    query.Append("     , fc_niff_coddoctosesf(a.chavedeacesso) Coddoctoesf, 0 CodIntNF");
                    query.Append("     , n.Chavedeacesso chavedeacessoGlobus, a.chavedeacesso chavedeacessoArquivo, a.DATAIMPORTADO");
                    query.Append("     , n.codmodelo ModeloGlobus, a.ModeloNF ModeloArquivo");
                    query.Append("     , formatacnpjcpf(f.Nrinscricaoforn, 'CNPJ') CNPJForncedorGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.Cnpjemitente, 14, '0'), 'CNPJ') cnpjFornecedorArquivo");
                    query.Append("     , eliminanaonumericos(f.inscestadualforn) IEFornecedorGlobus, eliminanaonumericos(a.ieemitente) IEFornecedorArquivo");
                    query.Append("     , f.rsocialforn RSocialFornecedorGlobus, a.razaosocialemitente RSocialFornecedorArquivo");
                    query.Append("     , formatacnpjcpf(e.Inscricaoempresa, 'CNPJ') CNPJEmpresaGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.Cnpjdestinatario, 14, '0'), 'CNPJ') cnpjEmpresaArquivo ");
                    query.Append("     , eliminanaonumericos(l.iestadualfl) IEEmpresaGlobus, eliminanaonumericos(a.iedestinatario) IEEmpresaArquivo");
                    query.Append("     , e.rsocialempresa RSocialEmpresaGlobus, a.razaosocialdestinatario RSocialEmpresaArquivo");
                    query.Append("     , l.enderecofl EnderecoGlobus, a.enderecodestinatario EnderecoArquivo");
                    query.Append("     , l.numeroendfl NumeroEndGlobus, a.numeroenddestinatario NumeroEndArquivo, n.docconciliado, a.DataConferido");
                    query.Append("     , l.bairrofl bairroGlobus, a.bairrodestinatario bairroArquivo, a.liberado, a.observacaoDoFiscal");
                    query.Append("     , l.cepfl CEPGlobus, eliminanaonumericos(a.cepdestinatario) cepArquivo, n.sistema Origem");
                    query.Append("     , a.IdEmpresa, a.idArquivei, a.Tipo, a.Status, a.Operacao, a.DadosAdicionais, a.ComDiferencas, p.Idparametro");
                    query.Append("     , Sum(n.vlcontabilentra) -Sum(n.VLSERVICOENTRA) VlProdutoGlobus, a.valorproduto VlProdutoArquivo, 0 VlDescFreteItens");
                    query.Append("     , Decode(n.Statusentra, 'C', 'Cancelada', 'D', 'Rejeitada', 'I','Inutilizada', 'Ativa' ) StatusNF, a.TipoArquivo");
                    query.Append("     , n.codmunicfederal_origem MunicOrigemGlobus, n.codmunicfederal_destino MunicDestinoGlobus");
                    query.Append("     , a.municorigem, a.municdestino, 0 CodDoctoCPG");

                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjTomador, 14, '0'), 'CNPJ') cnpjTomador ");
                    query.Append("     , eliminanaonumericos(a.ieTomador) IETomador");
                    query.Append("     , a.razaosocialTomador RSocialTomador");
                    query.Append("     , a.enderecoTomador EnderecoTomador");
                    query.Append("     , a.numeroendTomador NumeroEndTomador");
                    query.Append("     , a.bairroTomador bairroTomador");
                    query.Append("     , eliminanaonumericos(a.cepTomador) cepTomador, 0 valordescontonf, 0 valorfretenf");

                    query.Append("  From esfentra N");
                    query.Append("     , Bgm_Fornecedor F");
                    query.Append("     , Niff_Fis_Arquivei a ");
                    query.Append("     , Ctr_Empautorizadas e");
                    query.Append("     , Ctr_Filial l");
                    query.Append("     , Niff_Fis_Parametrosarquivei p");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append(" Where n.codigoforn = f.codigoforn");
                    query.Append("   And n.chavedeacesso = a.chavedeacesso");
                    query.Append("   And e.codintempaut = l.codintempaut");
                    query.Append("   And l.Codigoempresa = n.codigoEmpresa");
                    query.Append("   And l.codigofl = n.codigofl");
                    query.Append("   And p.Idempresa = a.Idempresa");
                    query.Append("   And a.idEmpresa = " + IdEmpresa);
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Recebidos'");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    if (!dataEntrada)
                    {
                        query.Append("   And a.DataEmissao between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.dtentradaentra between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') = em.codigoglobus");

                    query.Append("   And n.Sistema <> 'EST'");
                    //if (ApenasConferidas)
                    //  query.Append("   And a.Conferido = 'S'");
                    //query.Append("   And a.idarquivei = 62189");
                    query.Append(" Group By n.codigoempresa, n.codigofl, n.codtpdoc, To_number(n.nrdocentra), a.numeronf, n.serieentra");
                    query.Append("     , a.Serie, n.dtemissaoentra, a.Dataemissao, f.Nrforn, n.dtentradaentra, a.Naturezaoperacao, a.Baseicms");
                    query.Append("     , n.Chavedeacesso, a.Chavedeacesso, n.Codmodelo, a.Modelonf, Formatacnpjcpf(f.Nrinscricaoforn, 'CNPJ')");
                    query.Append("     , Formatacnpjcpf(Lpad(a.Cnpjemitente, 14, '0'), 'CNPJ'), Eliminanaonumericos(f.Inscestadualforn)");
                    query.Append("     , a.Ieemitente, f.Rsocialforn, a.Razaosocialemitente, Formatacnpjcpf(e.Inscricaoempresa, 'CNPJ')");
                    query.Append("     , Formatacnpjcpf(Lpad(a.Cnpjdestinatario, 14, '0'), 'CNPJ'), eliminanaonumericos(l.Iestadualfl), eliminanaonumericos(a.Iedestinatario), a.Valortotalnf");
                    query.Append("     , e.Rsocialempresa, a.Razaosocialdestinatario, l.Enderecofl, a.Enderecodestinatario, l.Numeroendfl");
                    query.Append("     , a.Numeroenddestinatario, l.Bairrofl, a.Bairrodestinatario, l.Cepfl, Eliminanaonumericos(a.Cepdestinatario)");
                    query.Append("     , n.sistema, a.Idempresa, a.Idarquivei, a.Tipo, a.Status, a.Operacao, a.Dadosadicionais, a.Comdiferencas");
                    query.Append("     , p.Idparametro, a.Valorproduto, a.liberado, a.observacaoDoFiscal, n.docconciliado, a.DataConferido ");
                    query.Append("     , Decode(n.Statusentra, 'C', 'Cancelada', 'D', 'Rejeitada', 'I','Inutilizada', 'Ativa' ), a.TipoArquivo, a.DATAIMPORTADO ");
                    query.Append("     , n.codmunicfederal_origem, n.codmunicfederal_destino");
                    query.Append("     , a.municorigem, a.municdestino");
                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjTomador, 14, '0'), 'CNPJ')  ");
                    query.Append("     , eliminanaonumericos(a.ieTomador) ");
                    query.Append("     , a.razaosocialTomador ");
                    query.Append("     , a.enderecoTomador ");
                    query.Append("     , a.numeroendTomador ");
                    query.Append("     , a.bairroTomador ");
                    query.Append("     , eliminanaonumericos(a.cepTomador) ");
                }
                else // Emitidos
                {
                    query.Append("Select N.Codigoempresa, N.Codigofl, N.Codtpdoc, To_Number(n.Numeronf) NumeroNFGlobus, a.NumeroNF NumeroNFArquivo");
                    query.Append("     , n.Serienf SerieGlobus, a.Serie SerieArquivo, n.dataemissaonf EmissaoGlobus, a.DataEmissao EmissaoArquivo");
                    query.Append("     , f.Nrcli NrForn, n.entradasaidanf, n.naturezaoperacaonf naturezaoperacaoGlobus, a.naturezaoperacao NaturezaOperacaoArquivo ");
                    query.Append("     , n.basecalcicmsnf baseICMSGlobus, a.BaseICMS BaseICMSArquivo, n.Icms_Isentanf, n.Icms_Outrasnf");
                    query.Append("     , n.valortotalnf ValorTotalGlobus, a.ValorTotalNF ValorTotalArquivo, To_Char(n.coddoctoesf) coddoctoesf, n.CodIntNF");
                    query.Append("     , ne.chavedeacesso chavedeacessoGlobus, a.chavedeacesso chavedeacessoArquivo, a.DATAIMPORTADO");
                    query.Append("     , n.codmodelo ModeloGlobus, a.ModeloNF ModeloArquivo");
                    query.Append("     , formatacnpjcpf(f.Nrinscricaocli, 'CNPJ') CNPJForncedorGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjDestinatario, 14, '0'), 'CNPJ') cnpjFornecedorArquivo");
                    query.Append("     , eliminanaonumericos(f.inscestadualcli) IEFornecedorGlobus, eliminanaonumericos(a.ieDestinatario) IEFornecedorArquivo");
                    query.Append("     , f.rsocialcli RSocialFornecedorGlobus, a.razaosocialDestinatario RSocialFornecedorArquivo");
                    query.Append("     , formatacnpjcpf(e.Inscricaoempresa, 'CNPJ') CNPJEmpresaGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjEmitente, 14, '0'), 'CNPJ') cnpjEmpresaArquivo ");
                    query.Append("     , eliminanaonumericos(l.iestadualfl) IEEmpresaGlobus, eliminanaonumericos(a.ieEmitente) IEEmpresaArquivo");
                    query.Append("     , e.rsocialempresa RSocialEmpresaGlobus, a.razaosocialEmitente RSocialEmpresaArquivo");
                    query.Append("     , l.enderecofl EnderecoGlobus, null EnderecoArquivo");
                    query.Append("     , l.numeroendfl NumeroEndGlobus, null NumeroEndArquivo");
                    query.Append("     , 'N' Conferido, a.DataConferido");
                    query.Append("     , l.bairrofl bairroGlobus, null bairroArquivo, a.liberado, a.observacaoDoFiscal");
                    query.Append("     , l.cepfl CEPGlobus, null cepArquivo, 'EST' Origem");
                    query.Append("     , a.IdEmpresa, a.idArquivei, a.Tipo, ne.mensagemrecibo Status, a.Operacao, a.DadosAdicionais, a.ComDiferencas, p.Idparametro");
                    query.Append("     , (Select Round(Sum(Valorproduto),2) valorProduto");
                    query.Append("          From (Select Nvl(Sum(o.valorunitarioitensnf * o.qtdeitensnf) , 0) valorProduto, CodIntNF");
                    query.Append("                  From est_itensnf o");
                    query.Append("                 Group By CodIntNF");
                    query.Append("                 Union All");
                    query.Append("                Select Nvl(Sum(e.Valornfservico), 0) valorProduto, CodIntNF");
                    query.Append("                  From Est_Nfservico e");
                    query.Append("                 Group By CodIntNF) x");
                    query.Append("         Where x.CodIntNF = n.CodIntNF ) VlProdutoGlobus, a.valorproduto VlProdutoArquivo");

                    query.Append("     , (Select Round(Sum(DescFrete),2) DescFrete");
                    query.Append("          From (Select Nvl(sum(o.VALORDESCONTOITENSNF),0) + Nvl(Sum(o.VALORFRETEITENSNF),0) DescFrete, CodIntNF");
                    query.Append("                  From est_itensnf o");
                    query.Append("                 Group By CodIntNF");
                    query.Append("                 Union All");
                    query.Append("                Select Nvl(sum(e.vlrdescontonfserv),0) + Nvl(Sum(e.valorfrete),0) DescFrete, CodIntNF");
                    query.Append("                  From Est_Nfservico e");
                    query.Append("                 Group By CodIntNF) x");
                    query.Append("         Where x.CodIntNF = n.CodIntNF ) VlDescFreteItens ");

                    query.Append("     , Decode(n.StatusNF, 'C', 'Cancelada', 'D', 'Rejeitada', 'I','Inutilizada', 'Ativa' ) StatusNF, a.TipoArquivo");
                    query.Append("     , n.codmunicfederal_origem MunicOrigemGlobus, n.codmunicfederal_destino MunicDestinoGlobus");
                    query.Append("     , a.municorigem, a.municdestino, n.CodDoctoCRC CodDoctoCPG");

                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjTomador, 14, '0'), 'CNPJ') cnpjTomador ");
                    query.Append("     , eliminanaonumericos(a.ieTomador) IETomador");
                    query.Append("     , a.razaosocialTomador RSocialTomador");
                    query.Append("     , a.enderecoTomador EnderecoTomador");
                    query.Append("     , a.numeroendTomador NumeroEndTomador");
                    query.Append("     , a.bairroTomador bairroTomador");
                    query.Append("     , eliminanaonumericos(a.cepTomador) cepTomador");
                    query.Append("     , FC_NIFF_CFOPCSTOperacao_Emitidas(n.codclassfisc, n.naturezaoperacaonf, 'L', " + IdEmpresa + ", a.idarquivei) Lei, n.dadosadicionaisimp");
                    query.Append("     , FC_NIFF_Serie_Emitidas(" + IdEmpresa + ") SerieComparar, n.valordescontonf, n.valorfretenf");
                    query.Append("  From Bgm_Notafiscal N");
                    query.Append("     , bgm_notafiscal_eletronica ne");
                    query.Append("     , Bgm_Cliente F");
                    query.Append("     , Niff_Fis_Arquivei a ");
                    query.Append("     , Ctr_Empautorizadas e");
                    query.Append("     , Ctr_Filial l");
                    query.Append("     , Niff_Fis_Parametrosarquivei p");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append(" Where n.codcli = f.codcli");
                    query.Append("   And e.codintempaut = l.codintempaut");
                    query.Append("   And l.Codigoempresa = n.codigoEmpresa");
                    query.Append("   And l.codigofl = n.codigofl");
                    query.Append("   And p.Idempresa = a.Idempresa");
                    query.Append("   And a.idEmpresa = " + IdEmpresa);

                    query.Append("   And ne.Id_Nfe = a.Idnfeglobus");
                    query.Append("   And ne.codintnf_bgmnf = n.codintnf");
                    query.Append("   And ne.chavedeacesso = a.Chavedeacesso");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Emitidos'");

                    if (!dataEntrada)
                    {
                        query.Append("   And a.DataEmissao between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.entradasaidanf between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') = em.codigoglobus");

                    //  if (ApenasConferidas)
                    //    query.Append("   And a.Conferido = 'S'");

                    //query.Append("   And a.idarquivei = 62189");

                }

                Query executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        NotasArquivei _tipo = new NotasArquivei();

                        _tipo.Existe = true;
                        idParametro = Convert.ToInt32(dataReader["Idparametro"].ToString());
                        _tipo.TipoProcessamento = tipoProcessamento;

                        _tipo.CodigoEmpresa = Convert.ToInt32(dataReader["codigoEmpresa"].ToString());
                        _tipo.CodigoFilial = Convert.ToInt32(dataReader["codigofl"].ToString());

                        _tipo.IdArquivei = Convert.ToInt32(dataReader["idArquivei"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        //_tipo.CFOP = Convert.ToInt32(dataReader["CFOP"].ToString());

                        _tipo.DataImportado = Convert.ToDateTime(dataReader["DATAIMPORTADO"].ToString());

                        _tipo.Tipo = dataReader["Tipo"].ToString();
                        _tipo.Origem = dataReader["Origem"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.StatusGlobus = dataReader["StatusNF"].ToString();
                        _tipo.Operacao = dataReader["Operacao"].ToString();
                        _tipo.DadosAdicionais = dataReader["dadosAdicionais"].ToString();

                        try
                        {
                            _tipo.Lei = dataReader["lei"].ToString();
                            _tipo.DadosAdicionaisGlobus = dataReader["dadosadicionaisimp"].ToString();
                        }
                        catch { }

                        _tipo.TipoArquivo = dataReader["TipoArquivo"].ToString();

                        _tipo.Conferido = dataReader["Conferido"].ToString() == "S";

                        if (_tipo.Origem == "EST")
                            _tipo.IntegradoCPG = dataReader["CodDoctoCPG"].ToString() != "";
                        else
                            _tipo.IntegradoCPG = EstaIntegradoContasPagar(dataReader["CodDoctoESF"].ToString(), tipoProcessamento) != "";

                        _tipo.Liberado = dataReader["Liberado"].ToString() == "S" && _tipo.IntegradoCPG;

                        try
                        {
                            _tipo.DataConferido = Convert.ToDateTime(dataReader["DataConferido"].ToString());
                        }
                        catch { }

                        _tipo.Observacao = dataReader["observacaoDoFiscal"].ToString();

                        _tipo.TipoDocumento = dataReader["CodTpDoc"].ToString();

                        _tipo.NumeroNFGlobus = Convert.ToDecimal(dataReader["NumeroNFGlobus"].ToString());
                        _tipo.NumeroNFArquivo = Convert.ToDecimal(dataReader["NumeroNFArquivo"].ToString());
                        _tipo.IntegradaLivro = dataReader["Coddoctoesf"].ToString() != "";

                        _tipo.SerieGlobus = dataReader["SerieGlobus"].ToString();
                        _tipo.SerieArquivo = dataReader["SerieArquivo"].ToString();

                        try
                        {
                            _tipo.SerieComparar = dataReader["SerieComparar"].ToString();
                        }
                        catch { }

                        _tipo.CodigoFornecedor = dataReader["Nrforn"].ToString();

                        _tipo.NaturezaOperacaoGlobus = dataReader["naturezaoperacaoGlobus"].ToString();
                        _tipo.NaturezaOperacaoArquivo = dataReader["NaturezaOperacaoArquivo"].ToString();

                        _tipo.ChaveAcessoGlobus = dataReader["chavedeacessoGlobus"].ToString();
                        _tipo.ChaveAcessoArquivo = dataReader["chavedeacessoArquivo"].ToString();

                        _tipo.CodigoModeloGlobus = dataReader["ModeloGlobus"].ToString();
                        _tipo.CodigoModeloArquivo = dataReader["ModeloArquivo"].ToString();

                        _tipo.CNPJFornecedor = dataReader["CNPJForncedorGlobus"].ToString();
                        _tipo.CNPJFornecedorArquivo = dataReader["cnpjFornecedorArquivo"].ToString();

                        _tipo.IEFornecedor = dataReader["IEFornecedorGlobus"].ToString();
                        _tipo.IEFornecedorArquivo = dataReader["IEFornecedorArquivo"].ToString();

                        _tipo.RazaoSocialFornecedor = dataReader["RSocialFornecedorGlobus"].ToString();
                        _tipo.RazaoSocialFornecedorArquivo = dataReader["RSocialFornecedorArquivo"].ToString();

                        _tipo.CNPJEmpresaGlobus = dataReader["CNPJEmpresaGlobus"].ToString();
                        _tipo.CNPJEmpresaArquivo = dataReader["cnpjEmpresaArquivo"].ToString();

                        _tipo.IEEmpresaGlobus = dataReader["IEEmpresaGlobus"].ToString();
                        _tipo.IEEmpresaArquivo = dataReader["IEEmpresaArquivo"].ToString();

                        _tipo.RazaoSocialEmpresaGlobus = dataReader["RSocialEmpresaGlobus"].ToString();
                        _tipo.RazaoSocialEmpresaArquivo = dataReader["RSocialEmpresaArquivo"].ToString();

                        _tipo.EnderecoEmpresaGlobus = dataReader["EnderecoGlobus"].ToString();
                        _tipo.EnderecoEmpresaArquivo = dataReader["EnderecoArquivo"].ToString();

                        _tipo.NumeroEnderecoGlobus = dataReader["NumeroEndGlobus"].ToString();
                        _tipo.NumeroEnderecoEmpresaArquivo = dataReader["NumeroEndArquivo"].ToString();

                        _tipo.BairroEmpresaGlobus = dataReader["bairroGlobus"].ToString();
                        _tipo.BairroEmpresaArquivo = dataReader["bairroArquivo"].ToString();

                        _tipo.CEPEmpresaGlobus = dataReader["CEPGlobus"].ToString();
                        _tipo.CEPEmpresaArquivo = dataReader["cepArquivo"].ToString();

                        _tipo.MunicipioOrigemGlobus = dataReader["MunicOrigemGlobus"].ToString();
                        _tipo.MunicipioOrigemArquivo = dataReader["municorigem"].ToString();
                        _tipo.MunicipioDestinoGlobus = dataReader["MunicDestinoGlobus"].ToString();
                        _tipo.MunicipioDestinoArquivo = dataReader["municdestino"].ToString();

                        try
                        {
                            _tipo.Entrada = Convert.ToDateTime(dataReader["EntradaSaidaNF"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.EmissaoGlobus = Convert.ToDateTime(dataReader["EmissaoGlobus"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.EmissaoArquivo = Convert.ToDateTime(dataReader["EmissaoArquivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.BaseICMSGlobus = Convert.ToDecimal(dataReader["baseICMSGlobus"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.BaseICMSArquivo = Convert.ToDecimal(dataReader["BaseICMSArquivo"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.ValorIsentasGlobus = Convert.ToDecimal(dataReader["Icms_Isentanf"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorOutrasGlobus = Convert.ToDecimal(dataReader["Icms_Outrasnf"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorTotalNFGlobus = Convert.ToDecimal(dataReader["ValorTotalGlobus"].ToString());
                            if (_tipo.ValorOutrasGlobus != 0 && _tipo.ValorOutrasGlobus != _tipo.ValorTotalNFGlobus)
                                _tipo.ValorOutrasGlobus = _tipo.ValorTotalNFGlobus;
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorTotalNFArquivo = Convert.ToDecimal(dataReader["ValorTotalArquivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            //_tipo.CodDoctoEsf = Convert.ToDecimal(dataReader["coddoctoesf"].ToString());
                            _tipo.CodDoctoEsf = dataReader["coddoctoesf"].ToString();
                        }
                        catch { }

                        try
                        {
                            _tipo.CodIntNF = Convert.ToDecimal(dataReader["CodIntNF"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorProdutosArquivo = Convert.ToDecimal(dataReader["VlProdutoArquivo"].ToString());
                        }
                        catch { }

                        try 
                        {
                            decimal vlDescItens = 0;
                            try
                            {
                                vlDescItens = Convert.ToDecimal(dataReader["VlDescFreteItens"].ToString());
                            }
                            catch { }

                            _tipo.ValorProdutosGlobus = Convert.ToDecimal(dataReader["VlProdutoGlobus"].ToString());

                            if (_tipo.ValorProdutosArquivo != _tipo.ValorProdutosGlobus)
                            {
                                _tipo.ValorProdutosGlobus = _tipo.ValorProdutosGlobus + Convert.ToDecimal(dataReader["valordescontonf"].ToString()) +
                                Convert.ToDecimal(dataReader["valorfretenf"].ToString());

                                if (_tipo.ValorProdutosArquivo != _tipo.ValorProdutosGlobus)
                                    _tipo.ValorProdutosGlobus = _tipo.ValorProdutosGlobus + vlDescItens;
                            }                        
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
                        if (_tipo.CNPJTomador == _tipo.CNPJEmpresaGlobus && _tipo.CNPJTomador != "")
                        {
                            _tipo.CNPJEmpresaArquivo = dataReader["cnpjTomador"].ToString();
                            _tipo.IEEmpresaArquivo = dataReader["IETomador"].ToString();
                            _tipo.RazaoSocialEmpresaArquivo = dataReader["RSocialTomador"].ToString();
                            _tipo.EnderecoEmpresaArquivo = dataReader["EnderecoTomador"].ToString();
                            _tipo.NumeroEnderecoEmpresaArquivo = dataReader["NumeroEndTomador"].ToString();
                            _tipo.BairroEmpresaArquivo = dataReader["bairroTomador"].ToString();
                            _tipo.CEPEmpresaArquivo = dataReader["cepTomador"].ToString();
                            _tipo.TipoTomadorDestinatario = "Tomador";
                        }

                        if (_tipo.ChaveAcessoGlobus == null || _tipo.ChaveAcessoGlobus == "")
                            _tipo.ChaveAcessoGlobus = ChaveDeAcessoNFE_Globus(_tipo.CodIntNF);
                        _tipo.Usuario = UsuarioDocumentoESF(_tipo.CodDoctoEsf);

                        _lista.Add(_tipo);

                    }
                }

                // Verifica pela combinação de informações se a Nota existe - Recebidos
                query.Clear();
                if (tipoProcessamento == "Recebidos")
                {
                    query.Append("Select N.Codigoempresa, N.Codigofl, N.Codtpdoc, To_Number(n.Numeronf) NumeroNFGlobus, a.NumeroNF NumeroNFArquivo");
                    query.Append("     , n.Serienf SerieGlobus, a.Serie SerieArquivo, n.dataemissaonf EmissaoGlobus, a.DataEmissao EmissaoArquivo");
                    query.Append("     , f.Nrforn, n.entradasaidanf, n.naturezaoperacaonf naturezaoperacaoGlobus, a.naturezaoperacao NaturezaOperacaoArquivo ");
                    query.Append("     , n.basecalcicmsnf baseICMSGlobus, a.BaseICMS BaseICMSArquivo, n.Icms_Isentanf, n.Icms_Outrasnf");
                    query.Append("     , n.valortotalnf ValorTotalGlobus, a.ValorTotalNF ValorTotalArquivo, n.coddoctoesf, n.CodIntNF");
                    query.Append("     , n.chavedeacessonfe chavedeacessoGlobus, a.chavedeacesso chavedeacessoArquivo, a.DATAIMPORTADO");
                    query.Append("     , n.codmodelo ModeloGlobus, a.ModeloNF ModeloArquivo");
                    query.Append("     , formatacnpjcpf(f.Nrinscricaoforn, 'CNPJ') CNPJForncedorGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.Cnpjemitente, 14, '0'), 'CNPJ') cnpjFornecedorArquivo");
                    query.Append("     , eliminanaonumericos(f.inscestadualforn) IEFornecedorGlobus, eliminanaonumericos(a.ieemitente) IEFornecedorArquivo");
                    query.Append("     , f.rsocialforn RSocialFornecedorGlobus, a.razaosocialemitente RSocialFornecedorArquivo");
                    query.Append("     , formatacnpjcpf(e.Inscricaoempresa, 'CNPJ') CNPJEmpresaGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.Cnpjdestinatario, 14, '0'), 'CNPJ') cnpjEmpresaArquivo ");
                    query.Append("     , eliminanaonumericos(l.iestadualfl) IEEmpresaGlobus, eliminanaonumericos(a.iedestinatario) IEEmpresaArquivo");
                    query.Append("     , e.rsocialempresa RSocialEmpresaGlobus, a.razaosocialdestinatario RSocialEmpresaArquivo");
                    query.Append("     , l.enderecofl EnderecoGlobus, a.enderecodestinatario EnderecoArquivo");
                    query.Append("     , l.numeroendfl NumeroEndGlobus, a.numeroenddestinatario NumeroEndArquivo");
                    query.Append("     , 'N' Conferido");
                    query.Append("     , a.DataConferido");
                    query.Append("     , l.bairrofl bairroGlobus, a.bairrodestinatario bairroArquivo, a.liberado, a.observacaoDoFiscal");
                    query.Append("     , l.cepfl CEPGlobus, eliminanaonumericos(a.cepdestinatario) cepArquivo, 'EST' Origem");
                    query.Append("     , a.IdEmpresa, a.idArquivei, a.Tipo, a.Status, a.Operacao, a.DadosAdicionais, a.ComDiferencas, p.Idparametro");
                    query.Append("     , (Select Round(Sum(Valorproduto),2) valorProduto");
                    query.Append("          From (Select Nvl(Sum(o.valorunitarioitensnf * o.qtdeitensnf) , 0) valorProduto, CodIntNF");
                    query.Append("                  From est_itensnf o");
                    query.Append("                 Group By CodIntNF");
                    query.Append("                 Union All");
                    query.Append("                Select Nvl(Sum(e.valornfservico), 0) valorProduto, CodIntNF");
                    query.Append("                  From Est_Nfservico e");
                    query.Append("                 Group By CodIntNF) x");
                    query.Append("         Where x.CodIntNF = n.CodIntNF ) VlProdutoGlobus, a.valorproduto VlProdutoArquivo");

                    query.Append("     , (Select Round(Sum(DescFrete),2) DescFrete");
                    query.Append("          From (Select Nvl(sum(o.VALORDESCONTOITENSNF),0) + Nvl(Sum(o.VALORFRETEITENSNF),0) DescFrete, CodIntNF");
                    query.Append("                  From est_itensnf o");
                    query.Append("                 Group By CodIntNF");
                    query.Append("                 Union All");
                    query.Append("                Select Nvl(sum(e.vlrdescontonfserv),0) + Nvl(Sum(e.valorfrete),0) DescFrete, CodIntNF");
                    query.Append("                  From Est_Nfservico e");
                    query.Append("                 Group By CodIntNF) x");
                    query.Append("         Where x.CodIntNF = n.CodIntNF ) VlDescFreteItens ");

                    query.Append("     , Decode(n.StatusNF, 'C', 'Cancelada', 'D', 'Rejeitada', 'I','Inutilizada', 'Ativa' ) StatusNF, a.TipoArquivo");
                    query.Append("     , n.codmunicfederal_origem MunicOrigemGlobus, n.codmunicfederal_destino MunicDestinoGlobus");
                    query.Append("     , a.municorigem, a.municdestino, n.CodDoctoCPG");

                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjTomador, 14, '0'), 'CNPJ') cnpjTomador ");
                    query.Append("     , eliminanaonumericos(a.ieTomador) IETomador");
                    query.Append("     , a.razaosocialTomador RSocialTomador");
                    query.Append("     , a.enderecoTomador EnderecoTomador");
                    query.Append("     , a.numeroendTomador NumeroEndTomador");
                    query.Append("     , a.bairroTomador bairroTomador");
                    query.Append("     , eliminanaonumericos(a.cepTomador) cepTomador, n.valordescontonf, n.valorfretenf");

                    query.Append("  From Bgm_Notafiscal N");
                    query.Append("     , Bgm_Fornecedor F");
                    query.Append("     , Niff_Fis_Arquivei a ");
                    query.Append("     , Ctr_Empautorizadas e");
                    query.Append("     , Ctr_Filial l");
                    query.Append("     , Niff_Fis_Parametrosarquivei p");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append(" Where n.codigoforn = f.codigoforn");
                    query.Append("   And To_number(n.numeronf) = a.numeronf");
                    query.Append("   And n.serienf = a.serie");
                    query.Append("   And f.nrinscricaoforn = Lpad(a.Cnpjemitente, 14, '0') ");
                    query.Append("   And e.codintempaut = l.codintempaut");
                    query.Append("   And l.Codigoempresa = n.codigoEmpresa");
                    query.Append("   And l.codigofl = n.codigofl");
                    query.Append("   And p.Idempresa = a.Idempresa");
                    query.Append("   And a.idEmpresa = " + IdEmpresa);
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Recebidos'");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    if (!dataEntrada)
                    {
                        query.Append("   And a.DataEmissao between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.entradasaidanf between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') = em.codigoglobus");

                    //if (ApenasConferidas)
                    //query.Append("   And a.Conferido = 'S'");
                    //query.Append("   And a.idarquivei = 62189");

                    query.Append(" Union All ");

                    query.Append("Select N.Codigoempresa, N.Codigofl, N.Codtpdoc, To_Number(n.nrdocentra) NumeroNFGlobus, a.NumeroNF NumeroNFArquivo");
                    query.Append("     , n.serieentra SerieGlobus, a.Serie SerieArquivo, n.dtemissaoentra EmissaoGlobus, a.DataEmissao EmissaoArquivo");
                    query.Append("     , f.Nrforn, n.dtentradaentra, null naturezaoperacaoGlobus, a.naturezaoperacao NaturezaOperacaoArquivo ");
                    query.Append("     , Sum(n.Icmsbaseentra) baseICMSGlobus, a.BaseICMS BaseICMSArquivo, Sum(n.Icmsisentaentra) Icms_Isentanf, Sum(n.Icmsoutrasentra) Icms_Outrasnf");
                    query.Append("     , Sum(n.vlcontabilentra) ValorTotalGlobus, a.ValorTotalNF ValorTotalArquivo, n.Coddoctoesf, 0 CodIntNF");
                    query.Append("     , n.Chavedeacesso chavedeacessoGlobus, a.chavedeacesso chavedeacessoArquivo, a.DATAIMPORTADO");
                    query.Append("     , n.codmodelo ModeloGlobus, a.ModeloNF ModeloArquivo");
                    query.Append("     , formatacnpjcpf(f.Nrinscricaoforn, 'CNPJ') CNPJForncedorGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.Cnpjemitente, 14, '0'), 'CNPJ') cnpjFornecedorArquivo");
                    query.Append("     , eliminanaonumericos(f.inscestadualforn) IEFornecedorGlobus, eliminanaonumericos(a.ieemitente) IEFornecedorArquivo");
                    query.Append("     , f.rsocialforn RSocialFornecedorGlobus, a.razaosocialemitente RSocialFornecedorArquivo");
                    query.Append("     , formatacnpjcpf(e.Inscricaoempresa, 'CNPJ') CNPJEmpresaGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.Cnpjdestinatario, 14, '0'), 'CNPJ') cnpjEmpresaArquivo ");
                    query.Append("     , eliminanaonumericos(l.iestadualfl) IEEmpresaGlobus, eliminanaonumericos(a.iedestinatario) IEEmpresaArquivo");
                    query.Append("     , e.rsocialempresa RSocialEmpresaGlobus, a.razaosocialdestinatario RSocialEmpresaArquivo");
                    query.Append("     , l.enderecofl EnderecoGlobus, a.enderecodestinatario EnderecoArquivo");
                    query.Append("     , l.numeroendfl NumeroEndGlobus, a.numeroenddestinatario NumeroEndArquivo, n.docconciliado, a.DataConferido");
                    query.Append("     , l.bairrofl bairroGlobus, a.bairrodestinatario bairroArquivo, a.liberado, a.observacaoDoFiscal");
                    query.Append("     , l.cepfl CEPGlobus, eliminanaonumericos(a.cepdestinatario) cepArquivo, n.sistema Origem");
                    query.Append("     , a.IdEmpresa, a.idArquivei, a.Tipo, a.Status, a.Operacao, a.DadosAdicionais, a.ComDiferencas, p.Idparametro");
                    query.Append("     , Sum(n.vlcontabilentra) -Sum(n.VLSERVICOENTRA) VlProdutoGlobus, a.valorproduto VlProdutoArquivo, 0 VlDescFreteItens");
                    query.Append("     , Decode(n.Statusentra, 'C', 'Cancelada', 'D', 'Rejeitada', 'I','Inutilizada', 'Ativa' ) StatusNF, a.TipoArquivo");
                    query.Append("     , n.codmunicfederal_origem MunicOrigemGlobus, n.codmunicfederal_destino MunicDestinoGlobus");
                    query.Append("     , a.municorigem, a.municdestino, 0 CodDoctoCPG");

                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjTomador, 14, '0'), 'CNPJ') cnpjTomador ");
                    query.Append("     , eliminanaonumericos(a.ieTomador) IETomador");
                    query.Append("     , a.razaosocialTomador RSocialTomador");
                    query.Append("     , a.enderecoTomador EnderecoTomador");
                    query.Append("     , a.numeroendTomador NumeroEndTomador");
                    query.Append("     , a.bairroTomador bairroTomador");
                    query.Append("     , eliminanaonumericos(a.cepTomador) cepTomador, 0 valordescontonf, 0 valorfretenf");

                    query.Append("  From esfentra N");
                    query.Append("     , Bgm_Fornecedor F");
                    query.Append("     , Niff_Fis_Arquivei a ");
                    query.Append("     , Ctr_Empautorizadas e");
                    query.Append("     , Ctr_Filial l");
                    query.Append("     , Niff_Fis_Parametrosarquivei p");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append(" Where n.codigoforn = f.codigoforn");
                    query.Append("   And LPad(n.nrdocentra, 10, '0') = LPad(a.numeronf, 10, '0')");
                    query.Append("   And n.serieentra = a.serie");
                    query.Append("   And f.nrinscricaoforn = Lpad(a.Cnpjemitente, 14, '0') ");
                    query.Append("   And e.codintempaut = l.codintempaut");
                    query.Append("   And l.Codigoempresa = n.codigoEmpresa");
                    query.Append("   And l.codigofl = n.codigofl");
                    query.Append("   And p.Idempresa = a.Idempresa");
                    query.Append("   And a.idEmpresa = " + IdEmpresa);
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Recebidos'");
                    query.Append("   And a.tipodocto <> 'NFSe'");

                    if (!dataEntrada)
                    {
                        query.Append("   And a.DataEmissao between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.dtentradaentra between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') = em.codigoglobus");

                    query.Append("   And n.Sistema <> 'EST'");
                    //if (ApenasConferidas)
                    //  query.Append("   And a.Conferido = 'S'");

                    //query.Append("   And a.idarquivei = 62189");
                    query.Append(" Group By n.codigoempresa, n.codigofl, n.codtpdoc, To_number(n.nrdocentra), a.numeronf, n.serieentra");
                    query.Append("     , a.Serie, n.dtemissaoentra, a.Dataemissao, f.Nrforn, n.dtentradaentra, a.Naturezaoperacao, a.Baseicms");
                    query.Append("     , n.Chavedeacesso, a.Chavedeacesso, n.Codmodelo, a.Modelonf, Formatacnpjcpf(f.Nrinscricaoforn, 'CNPJ')");
                    query.Append("     , Formatacnpjcpf(Lpad(a.Cnpjemitente, 14, '0'), 'CNPJ'), Eliminanaonumericos(f.Inscestadualforn)");
                    query.Append("     , eliminanaonumericos(a.Ieemitente), f.Rsocialforn, a.Razaosocialemitente, Formatacnpjcpf(e.Inscricaoempresa, 'CNPJ')");
                    query.Append("     , Formatacnpjcpf(Lpad(a.Cnpjdestinatario, 14, '0'), 'CNPJ'), eliminanaonumericos(l.Iestadualfl), eliminanaonumericos(a.Iedestinatario), a.Valortotalnf");
                    query.Append("     , e.Rsocialempresa, a.Razaosocialdestinatario, l.Enderecofl, a.Enderecodestinatario, l.Numeroendfl");
                    query.Append("     , a.Numeroenddestinatario, l.Bairrofl, a.Bairrodestinatario, l.Cepfl, Eliminanaonumericos(a.Cepdestinatario)");
                    query.Append("     , n.sistema, a.Idempresa, a.Idarquivei, a.Tipo, a.Status, a.Operacao, a.Dadosadicionais, a.Comdiferencas");
                    query.Append("     , p.Idparametro, a.Valorproduto, n.Coddoctoesf, a.liberado, a.observacaoDoFiscal, n.docconciliado, a.DataConferido ");
                    query.Append("     , Decode(n.Statusentra, 'C', 'Cancelada', 'D', 'Rejeitada', 'I','Inutilizada', 'Ativa' ) , a.DATAIMPORTADO ");
                    query.Append("     , n.codmunicfederal_origem, n.codmunicfederal_destino");
                    query.Append("     , a.municorigem, a.municdestino");

                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjTomador, 14, '0'), 'CNPJ')  ");
                    query.Append("     , eliminanaonumericos(a.ieTomador) ");
                    query.Append("     , a.razaosocialTomador ");
                    query.Append("     , a.enderecoTomador ");
                    query.Append("     , a.numeroendTomador ");
                    query.Append("     , a.bairroTomador ");
                    query.Append("     , eliminanaonumericos(a.cepTomador), a.TipoArquivo ");
                }
                else // Emitidas
                {
                    query.Append("Select N.Codigoempresa, N.Codigofl, N.Codtpdoc, To_Number(n.Numeronf) NumeroNFGlobus, a.NumeroNF NumeroNFArquivo");
                    query.Append("     , n.Serienf SerieGlobus, a.Serie SerieArquivo, n.datanf EmissaoGlobus, a.DataEmissao EmissaoArquivo");
                    query.Append("     , f.Nrcli NrForn, n.datanf entradasaidanf, n.naturezaop naturezaoperacaoGlobus, a.naturezaoperacao NaturezaOperacaoArquivo ");
                    query.Append("     , n.basecalculoicms baseICMSGlobus, a.BaseICMS BaseICMSArquivo, 0 Icms_Isentanf, 0 Icms_Outrasnf");
                    query.Append("     , n.basecalculoicms ValorTotalGlobus, a.ValorTotalNF ValorTotalArquivo, n.coddoctoesf, n.codintnfavul CodIntNF");
                    query.Append("     , ne.chavedeacesso chavedeacessoGlobus, a.chavedeacesso chavedeacessoArquivo, a.DATAIMPORTADO");
                    query.Append("     , n.codmodelo ModeloGlobus, a.ModeloNF ModeloArquivo");
                    query.Append("     , formatacnpjcpf(f.Nrinscricaocli, 'CNPJ') CNPJForncedorGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjDestinatario, 14, '0'), 'CNPJ') cnpjFornecedorArquivo");
                    query.Append("     , eliminanaonumericos(f.inscestadualcli) IEFornecedorGlobus, eliminanaonumericos(a.ieDestinatario) IEFornecedorArquivo");
                    query.Append("     , f.rsocialcli RSocialFornecedorGlobus, a.razaosocialDestinatario RSocialFornecedorArquivo");
                    query.Append("     , formatacnpjcpf(e.Inscricaoempresa, 'CNPJ') CNPJEmpresaGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjEmitente, 14, '0'), 'CNPJ') cnpjEmpresaArquivo ");
                    query.Append("     , eliminanaonumericos(l.iestadualfl) IEEmpresaGlobus, eliminanaonumericos(a.ieEmitente) IEEmpresaArquivo");
                    query.Append("     , e.rsocialempresa RSocialEmpresaGlobus, a.razaosocialEmitente RSocialEmpresaArquivo");
                    query.Append("     , l.enderecofl EnderecoGlobus, null EnderecoArquivo");
                    query.Append("     , l.numeroendfl NumeroEndGlobus, null NumeroEndArquivo");
                    query.Append("     , 'N' Conferido");
                    query.Append("     , a.DataConferido");
                    query.Append("     , l.bairrofl bairroGlobus, null bairroArquivo, a.liberado, a.observacaoDoFiscal");
                    query.Append("     , l.cepfl CEPGlobus, null cepArquivo, 'EST' Origem");
                    query.Append("     , a.IdEmpresa, a.idArquivei, a.Tipo, a.Status, a.Operacao, a.DadosAdicionais, a.ComDiferencas, p.Idparametro");
                    query.Append("     , (Select Round(Sum(Valorproduto),2) valorProduto");
                    query.Append("          From (Select Nvl(Sum(o.vlrunititem * o.qtdeitem), 0) Valorproduto, codintnfavul");
                    query.Append("                  From Est_Itensmaterialavulsonf o"); 
                    query.Append("                 Group By codintnfavul) x");
                    query.Append("         Where x.codintnfavul = n.codintnfavul ) VlProdutoGlobus, a.valorproduto VlProdutoArquivo");

                    query.Append("     , (Select Round(Sum(DescFrete),2) DescFrete");
                    query.Append("          From (Select Nvl(Sum(o.VALORDESCONTOITEM), 0) + nvl(sum(o.VALORFRETEITEM),0) DescFrete, codintnfavul");
                    query.Append("                  From Est_Itensmaterialavulsonf o");
                    query.Append("                 Group By codintnfavul) x");
                    query.Append("         Where x.codintnfavul = n.codintnfavul ) VlDescFreteItens");


                    query.Append("     , Decode(n.StatusNF, 'C', 'Cancelada', 'D', 'Rejeitada', 'I','Inutilizada', 'Ativa' ) StatusNF, a.TipoArquivo");
                    query.Append("     , null MunicOrigemGlobus, null MunicDestinoGlobus");
                    query.Append("     , a.municorigem, a.municdestino, null CodDoctoCPG");

                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjTomador, 14, '0'), 'CNPJ') cnpjTomador ");
                    query.Append("     , eliminanaonumericos(a.ieTomador) IETomador");
                    query.Append("     , a.razaosocialTomador RSocialTomador");
                    query.Append("     , a.enderecoTomador EnderecoTomador");
                    query.Append("     , a.numeroendTomador NumeroEndTomador");
                    query.Append("     , a.bairroTomador bairroTomador");
                    query.Append("     , eliminanaonumericos(a.cepTomador) cepTomador");
                    query.Append("     , FC_NIFF_CFOPCSTOperacao_Emitidas(n.codclassfisc, n.naturezaop, 'L', " + IdEmpresa + ", a.idarquivei) Lei, n.dadosadicionaisimp");
                    query.Append("     , FC_NIFF_Serie_Emitidas(" + IdEmpresa + ") SerieComparar, 0 valordescontonf, 0 valorfretenf");
                    query.Append("  From Est_Materialavulsonf N");
                    query.Append("     , bgm_notafiscal_eletronica ne");
                    query.Append("     , Bgm_Cliente F");
                    query.Append("     , Niff_Fis_Arquivei a ");
                    query.Append("     , Ctr_Empautorizadas e");
                    query.Append("     , Ctr_Filial l");
                    query.Append("     , Niff_Fis_Parametrosarquivei p");
                    query.Append("     , Niff_chm_empresas em");
                    query.Append(" Where n.codcli = f.codcli");
                    query.Append("   And To_number(n.numeronf) = a.numeronf");
                    query.Append("   And (n.serienf = a.serie or n.serienf = Lpad(a.serie,3,'0'))");
                    query.Append("   And f.nrinscricaocli = Lpad(a.CnpjDestinatario, 14, '0') ");
                    query.Append("   And e.codintempaut = l.codintempaut");
                    query.Append("   And l.Codigoempresa = n.codigoEmpresa");
                    query.Append("   And l.codigofl = n.codigofl");
                    query.Append("   And p.Idempresa = a.Idempresa");
                    query.Append("   And a.idEmpresa = " + IdEmpresa);

                    query.Append("   And ne.Id_Nfe = a.Idnfeglobus");
                    query.Append("   And ne.codintnfavul = n.codintnfavul");
                    query.Append("   And ne.chavedeacesso = a.Chavedeacesso");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Emitidos'");
                    if (!dataEntrada)
                    {
                        query.Append("   And a.DataEmissao between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.datanf between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') = em.codigoglobus");

                    //if (ApenasConferidas)
                    //  query.Append("   And a.Conferido = 'S'");
                    //query.Append("   And a.idarquivei = 62189");

                    query.Append(" Union All ");

                    query.Append("Select N.Codigoempresa, N.Codigofl, N.Codtpdoc, To_Number(n.nrdocSaida) NumeroNFGlobus, a.NumeroNF NumeroNFArquivo");
                    query.Append("     , n.serieSaida SerieGlobus, a.Serie SerieArquivo, n.dtemissaoSaida EmissaoGlobus, a.DataEmissao EmissaoArquivo");
                    query.Append("     , f.Nrcli NrForn, n.dtsaidasaida, ns.naturezaoperacao naturezaoperacaoGlobus, a.naturezaoperacao NaturezaOperacaoArquivo ");
                    query.Append("     , Sum(n.Icmsbasesaida) baseICMSGlobus, a.BaseICMS BaseICMSArquivo, Sum(n.Icmsisentasaida) Icms_Isentanf, Sum(n.Icmsoutrassaida) Icms_Outrasnf");
                    query.Append("     , Sum(n.vlcontabilsaida) ValorTotalGlobus, a.ValorTotalNF ValorTotalArquivo, n.Coddoctoesf, 0 CodIntNF");
                    query.Append("     , ne.chavedeacesso chavedeacessoGlobus, a.chavedeacesso chavedeacessoArquivo, a.DATAIMPORTADO");
                    query.Append("     , n.codmodelo ModeloGlobus, a.ModeloNF ModeloArquivo");
                    query.Append("     , formatacnpjcpf(f.Nrinscricaocli, 'CNPJ') CNPJForncedorGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjDestinatario, 14, '0'), 'CNPJ') cnpjFornecedorArquivo");
                    query.Append("     , eliminanaonumericos(f.inscestadualcli) IEFornecedorGlobus, eliminanaonumericos(a.ieDestinatario) IEFornecedorArquivo");
                    query.Append("     , f.rsocialcli RSocialFornecedorGlobus, a.razaosocialDestinatario RSocialFornecedorArquivo");
                    query.Append("     , formatacnpjcpf(e.Inscricaoempresa, 'CNPJ') CNPJEmpresaGlobus");
                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjEmitente, 14, '0'), 'CNPJ') cnpjEmpresaArquivo ");
                    query.Append("     , eliminanaonumericos(l.iestadualfl) IEEmpresaGlobus, eliminanaonumericos(a.ieEmitente) IEEmpresaArquivo");
                    query.Append("     , e.rsocialempresa RSocialEmpresaGlobus, a.razaosocialEmitente RSocialEmpresaArquivo");
                    query.Append("     , l.enderecofl EnderecoGlobus, null EnderecoArquivo");
                    query.Append("     , l.numeroendfl NumeroEndGlobus, null NumeroEndArquivo, n.docconciliado, a.DataConferido");
                    query.Append("     , l.bairrofl bairroGlobus, null bairroArquivo, a.liberado, a.observacaoDoFiscal");
                    query.Append("     , l.cepfl CEPGlobus, null cepArquivo, n.sistema Origem");
                    query.Append("     , a.IdEmpresa, a.idArquivei, a.Tipo, a.Status, a.Operacao, a.DadosAdicionais, a.ComDiferencas, p.Idparametro");
                    query.Append("     , Sum(n.vlcontabilsaida) -Sum(n.VLSERVICOsaida) VlProdutoGlobus, a.valorproduto VlProdutoArquivo, 0 VlDescFreteItens");
                    query.Append("     , Decode(n.Statussaida, 'C', 'Cancelada', 'D', 'Rejeitada', 'I','Inutilizada', 'Ativa' ) StatusNF, a.TipoArquivo");
                    query.Append("     , n.codmunicfederal_origem MunicOrigemGlobus, n.codmunicfederal_destino MunicDestinoGlobus");
                    query.Append("     , a.municorigem, a.municdestino, 0 CodDoctoCPG");

                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjTomador, 14, '0'), 'CNPJ') cnpjTomador ");
                    query.Append("     , eliminanaonumericos(a.ieTomador) IETomador");
                    query.Append("     , a.razaosocialTomador RSocialTomador");
                    query.Append("     , a.enderecoTomador EnderecoTomador");
                    query.Append("     , a.numeroendTomador NumeroEndTomador");
                    query.Append("     , a.bairroTomador bairroTomador");
                    query.Append("     , eliminanaonumericos(a.cepTomador) cepTomador");
                    query.Append("     , FC_NIFF_CFOPCSTOperacao_Emitidas(n.codclassfisc, ns.naturezaoperacao, 'L', " + IdEmpresa + ", a.idarquivei) Lei,  ns.dadosadicionais dadosadicionaisimp");
                    query.Append("     , FC_NIFF_Serie_Emitidas(" + IdEmpresa + ") SerieComparar, 0 valordescontonf, 0 valorfretenf");
                    query.Append("  From esfsaida N");
                    query.Append("     , bgm_notafiscal_eletronica ne");
                    query.Append("     , Esfnotafiscal ns");
                    query.Append("     , Bgm_cliente F");
                    query.Append("     , Niff_Fis_Arquivei a ");
                    query.Append("     , Ctr_Empautorizadas e");
                    query.Append("     , Ctr_Filial l");
                    query.Append("     , Niff_Fis_Parametrosarquivei p");
                    query.Append("     , Niff_chm_empresas em"); 
                    query.Append(" Where n.codcli = f.codcli");
                    query.Append("   And LPad(n.nrdocsaida, 10, '0') = LPad(a.numeronf, 10, '0')");
                    query.Append("   And (n.seriesaida = a.serie or n.seriesaida = Lpad(a.serie,3,'0'))");
                    query.Append("   And f.nrinscricaocli = Lpad(a.CnpjDestinatario, 14, '0') ");
                    query.Append("   And e.codintempaut = l.codintempaut");
                    query.Append("   And l.Codigoempresa = n.codigoEmpresa");
                    query.Append("   And l.codigofl = n.codigofl");
                    query.Append("   And p.Idempresa = a.Idempresa");
                    query.Append("   And a.idEmpresa = " + IdEmpresa);

                    query.Append("   And ne.Id_Nfe = a.Idnfeglobus");
                    query.Append("   And ne.codintnf_esfnf = ns.codintnf");
                    query.Append("   And ne.chavedeacesso = a.Chavedeacesso");
                    query.Append("   And ns.coddoctoesf = n.coddoctoesf");
                    query.Append("   And a.tipodocto <> 'NFSe'");
                    query.Append("   And a.TIPOPROCESSAMENTO = 'Emitidos'");

                    if (!dataEntrada)
                    {
                        query.Append("   And a.DataEmissao between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    else
                    {
                        query.Append("   And n.dtSaidaSaida between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                        query.Append("   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    }
                    query.Append("   And a.Idempresa = em.Idempresa");

                    if (IdEmpresa == 2)
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') in ('001/001','001/004')");
                    else
                        query.Append("   And Lpad(l.codigoempresa,3,'0') || '/' || Lpad(l.codigofl, 3, '0') = em.codigoglobus");

                    // query.Append("   And n.Sistema <> 'EST'");
                    //if (ApenasConferidas)
                    //  query.Append("   And a.Conferido = 'S'");

                    //query.Append("   And a.idarquivei = 62189");
                    query.Append(" Group By n.codigoempresa, n.codigofl, n.codtpdoc, To_number(n.nrdocsaida), a.numeronf, n.seriesaida");
                    query.Append("     , a.Serie, n.dtemissaosaida, a.Dataemissao, f.Nrcli, n.dtsaidaSaida, a.Naturezaoperacao, a.Baseicms");
                    query.Append("     , ne.chavedeacesso, a.Chavedeacesso, n.Codmodelo, a.Modelonf, Formatacnpjcpf(f.Nrinscricaocli, 'CNPJ')");
                    query.Append("     , Formatacnpjcpf(Lpad(a.CnpjDestinatario, 14, '0'), 'CNPJ'), Eliminanaonumericos(f.Inscestadualcli)");
                    query.Append("     , eliminanaonumericos(a.IeDestinatario), f.Rsocialcli, a.RazaosocialDestinatario, Formatacnpjcpf(e.Inscricaoempresa, 'CNPJ')");
                    query.Append("     , Formatacnpjcpf(Lpad(a.CnpjEmitente, 14, '0'), 'CNPJ'), eliminanaonumericos(l.Iestadualfl), eliminanaonumericos(a.IeEmitente), a.Valortotalnf");
                    query.Append("     , e.Rsocialempresa, a.RazaosocialEmitente, l.Enderecofl, a.Enderecodestinatario, l.Numeroendfl");
                    query.Append("     , a.Numeroenddestinatario, l.Bairrofl, null, l.Cepfl, Eliminanaonumericos(a.Cepdestinatario)");
                    query.Append("     , n.sistema, a.Idempresa, a.Idarquivei, a.Tipo, a.Status, a.Operacao, a.Dadosadicionais, a.Comdiferencas");
                    query.Append("     , p.Idparametro, a.Valorproduto, n.Coddoctoesf, a.liberado, a.observacaoDoFiscal, n.docconciliado, a.DataConferido ");
                    query.Append("     , Decode(n.Statussaida, 'C', 'Cancelada', 'D', 'Rejeitada', 'I','Inutilizada', 'Ativa' ) , a.DATAIMPORTADO ");
                    query.Append("     , n.codmunicfederal_origem, n.codmunicfederal_destino");
                    query.Append("     , a.municorigem, a.municdestino, a.TipoArquivo");

                    query.Append("     , formatacnpjcpf(Lpad(a.CnpjTomador, 14, '0'), 'CNPJ')  ");
                    query.Append("     , eliminanaonumericos(a.ieTomador) ");
                    query.Append("     , a.razaosocialTomador ");
                    query.Append("     , a.enderecoTomador ");
                    query.Append("     , a.numeroendTomador ");
                    query.Append("     , a.bairroTomador ");
                    query.Append("     , eliminanaonumericos(a.cepTomador), n.codclassfisc, ns.naturezaoperacao,  ns.dadosadicionais");
                }

                executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                int idArquivei = 0;
                bool encontrou = false;

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        idArquivei = Convert.ToInt32(dataReader["idArquivei"].ToString());
                        idParametro = Convert.ToInt32(dataReader["Idparametro"].ToString());

                        encontrou = false; 
                        foreach (var item in _lista.Where(w => w.IdArquivei == idArquivei))
                        {
                            encontrou = true;
                            break;
                        }

                        if (!encontrou)
                        {
                            NotasArquivei _tipo = new NotasArquivei();

                            _tipo.TipoProcessamento = tipoProcessamento;
                            _tipo.CodigoEmpresa = Convert.ToInt32(dataReader["codigoEmpresa"].ToString());
                            _tipo.CodigoFilial = Convert.ToInt32(dataReader["codigofl"].ToString());

                            _tipo.Existe = true;
                            _tipo.IdArquivei = Convert.ToInt32(dataReader["idArquivei"].ToString());
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                            //_tipo.CFOP = Convert.ToInt32(dataReader["CFOP"].ToString());

                            _tipo.Tipo = dataReader["Tipo"].ToString();
                            _tipo.Origem = dataReader["Origem"].ToString();
                            _tipo.Status = dataReader["Status"].ToString();
                            _tipo.StatusGlobus = dataReader["StatusNF"].ToString();
                            _tipo.Operacao = dataReader["Operacao"].ToString();
                            _tipo.DadosAdicionais = dataReader["dadosAdicionais"].ToString();
                            try
                            {
                                _tipo.DadosAdicionaisGlobus = dataReader["dadosadicionaisimp"].ToString();
                                _tipo.Lei = dataReader["lei"].ToString();
                            }
                            catch { }
                            _tipo.DataImportado = Convert.ToDateTime(dataReader["DATAIMPORTADO"].ToString());
                                                        
                            _tipo.Conferido = dataReader["Conferido"].ToString() == "S";
                            _tipo.TipoArquivo = dataReader["TipoArquivo"].ToString();

                            if (_tipo.Origem == "EST")
                                _tipo.IntegradoCPG = dataReader["CodDoctoCPG"].ToString() != "";
                            else
                                _tipo.IntegradoCPG = EstaIntegradoContasPagar(dataReader["CodDoctoESF"].ToString(), tipoProcessamento) != "";

                            _tipo.Liberado = dataReader["Liberado"].ToString() == "S" && _tipo.IntegradoCPG;
                            try
                            {
                                _tipo.DataConferido = Convert.ToDateTime(dataReader["DataConferido"].ToString());
                            }
                            catch { }
                            _tipo.Observacao = dataReader["observacaoDoFiscal"].ToString();

                            _tipo.TipoDocumento = dataReader["CodTpDoc"].ToString();

                            _tipo.NumeroNFGlobus = Convert.ToDecimal(dataReader["NumeroNFGlobus"].ToString());
                            _tipo.NumeroNFArquivo = Convert.ToDecimal(dataReader["NumeroNFArquivo"].ToString());
                            _tipo.IntegradaLivro = dataReader["Coddoctoesf"].ToString() != "";

                            _tipo.SerieGlobus = dataReader["SerieGlobus"].ToString();
                            _tipo.SerieArquivo = dataReader["SerieArquivo"].ToString();
                            
                            try
                            {
                                _tipo.SerieComparar = dataReader["SerieComparar"].ToString();
                            }
                            catch { }

                            _tipo.CodigoFornecedor = dataReader["Nrforn"].ToString();

                            _tipo.NaturezaOperacaoGlobus = dataReader["naturezaoperacaoGlobus"].ToString();
                            _tipo.NaturezaOperacaoArquivo = dataReader["NaturezaOperacaoArquivo"].ToString();

                            _tipo.ChaveAcessoGlobus = dataReader["chavedeacessoGlobus"].ToString();
                            _tipo.ChaveAcessoArquivo = dataReader["chavedeacessoArquivo"].ToString();

                            _tipo.CodigoModeloGlobus = dataReader["ModeloGlobus"].ToString();
                            _tipo.CodigoModeloArquivo = dataReader["ModeloArquivo"].ToString();

                            _tipo.CNPJFornecedor = dataReader["CNPJForncedorGlobus"].ToString();
                            _tipo.CNPJFornecedorArquivo = dataReader["cnpjFornecedorArquivo"].ToString();

                            _tipo.IEFornecedor = dataReader["IEFornecedorGlobus"].ToString();
                            _tipo.IEFornecedorArquivo = dataReader["IEFornecedorArquivo"].ToString();

                            _tipo.RazaoSocialFornecedor = dataReader["RSocialFornecedorGlobus"].ToString();
                            _tipo.RazaoSocialFornecedorArquivo = dataReader["RSocialFornecedorArquivo"].ToString();

                            _tipo.CNPJEmpresaGlobus = dataReader["CNPJEmpresaGlobus"].ToString();
                            _tipo.CNPJEmpresaArquivo = dataReader["cnpjEmpresaArquivo"].ToString();

                            _tipo.IEEmpresaGlobus = dataReader["IEEmpresaGlobus"].ToString();
                            _tipo.IEEmpresaArquivo = dataReader["IEEmpresaArquivo"].ToString();

                            _tipo.RazaoSocialEmpresaGlobus = dataReader["RSocialEmpresaGlobus"].ToString();
                            _tipo.RazaoSocialEmpresaArquivo = dataReader["RSocialEmpresaArquivo"].ToString();

                            _tipo.EnderecoEmpresaGlobus = dataReader["EnderecoGlobus"].ToString();
                            _tipo.EnderecoEmpresaArquivo = dataReader["EnderecoArquivo"].ToString();

                            _tipo.NumeroEnderecoGlobus = dataReader["NumeroEndGlobus"].ToString();
                            _tipo.NumeroEnderecoEmpresaArquivo = dataReader["NumeroEndArquivo"].ToString();

                            _tipo.BairroEmpresaGlobus = dataReader["bairroGlobus"].ToString();
                            _tipo.BairroEmpresaArquivo = dataReader["bairroArquivo"].ToString();

                            _tipo.CEPEmpresaGlobus = dataReader["CEPGlobus"].ToString();
                            _tipo.CEPEmpresaArquivo = dataReader["cepArquivo"].ToString();

                            _tipo.MunicipioOrigemGlobus = dataReader["MunicOrigemGlobus"].ToString();
                            _tipo.MunicipioOrigemArquivo = dataReader["municorigem"].ToString();
                            _tipo.MunicipioDestinoGlobus = dataReader["MunicDestinoGlobus"].ToString();
                            _tipo.MunicipioDestinoArquivo = dataReader["municdestino"].ToString();

                            try
                            {
                                _tipo.Entrada = Convert.ToDateTime(dataReader["EntradaSaidaNF"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.EmissaoGlobus = Convert.ToDateTime(dataReader["EmissaoGlobus"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.EmissaoArquivo = Convert.ToDateTime(dataReader["EmissaoArquivo"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.BaseICMSGlobus = Convert.ToDecimal(dataReader["baseICMSGlobus"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.BaseICMSArquivo = Convert.ToDecimal(dataReader["BaseICMSArquivo"].ToString());
                            }
                            catch { }
                            try
                            {
                                _tipo.ValorIsentasGlobus = Convert.ToDecimal(dataReader["Icms_Isentanf"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.ValorOutrasGlobus = Convert.ToDecimal(dataReader["Icms_Outrasnf"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.ValorTotalNFGlobus = Convert.ToDecimal(dataReader["ValorTotalGlobus"].ToString());
                                if (_tipo.ValorOutrasGlobus != 0 && _tipo.ValorOutrasGlobus != _tipo.ValorTotalNFGlobus)
                                    _tipo.ValorOutrasGlobus = _tipo.ValorTotalNFGlobus;
                            }
                            catch { }

                            try
                            {
                                _tipo.ValorTotalNFArquivo = Convert.ToDecimal(dataReader["ValorTotalArquivo"].ToString());
                            }
                            catch { }

                            try
                            {
                                //_tipo.CodDoctoEsf = Convert.ToDecimal(dataReader["coddoctoesf"].ToString());
                                _tipo.CodDoctoEsf = dataReader["coddoctoesf"].ToString();
                            }
                            catch { }

                            try
                            {
                                _tipo.CodIntNF = Convert.ToDecimal(dataReader["CodIntNF"].ToString());
                            }
                            catch { }


                            try
                            {
                                _tipo.ValorProdutosArquivo = Convert.ToDecimal(dataReader["VlProdutoArquivo"].ToString());
                            }
                            catch { }

                            try
                            {
                                decimal vlDescItens = 0;
                                try
                                {
                                    vlDescItens = Convert.ToDecimal(dataReader["VlDescFreteItens"].ToString()); 
                                }
                                catch { }

                                _tipo.ValorProdutosGlobus = Convert.ToDecimal(dataReader["VlProdutoGlobus"].ToString());

                                if (_tipo.ValorProdutosArquivo != _tipo.ValorProdutosGlobus)
                                {
                                    _tipo.ValorProdutosGlobus = _tipo.ValorProdutosGlobus + Convert.ToDecimal(dataReader["valordescontonf"].ToString()) +
                                    Convert.ToDecimal(dataReader["valorfretenf"].ToString());

                                    if (_tipo.ValorProdutosArquivo != _tipo.ValorProdutosGlobus)
                                        _tipo.ValorProdutosGlobus = _tipo.ValorProdutosGlobus + vlDescItens;

                                }
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

                            if (_tipo.CNPJTomador == _tipo.CNPJEmpresaGlobus && _tipo.CNPJTomador != "")
                            {
                                _tipo.CNPJEmpresaArquivo = dataReader["cnpjTomador"].ToString();
                                _tipo.IEEmpresaArquivo = dataReader["IETomador"].ToString();
                                _tipo.RazaoSocialEmpresaArquivo = dataReader["RSocialTomador"].ToString();
                                _tipo.EnderecoEmpresaArquivo = dataReader["EnderecoTomador"].ToString();
                                _tipo.NumeroEnderecoEmpresaArquivo = dataReader["NumeroEndTomador"].ToString();
                                _tipo.BairroEmpresaArquivo = dataReader["bairroTomador"].ToString();
                                _tipo.CEPEmpresaArquivo = dataReader["cepTomador"].ToString();
                                _tipo.TipoTomadorDestinatario = "Tomador";
                            }

                            if (_tipo.ChaveAcessoGlobus == null || _tipo.ChaveAcessoGlobus == "")
                                _tipo.ChaveAcessoGlobus = ChaveDeAcessoNFE_Globus(_tipo.CodIntNF);

                            _tipo.Usuario = UsuarioDocumentoESF(_tipo.CodDoctoEsf);

                            _lista.Add(_tipo);
                        }
                    }
                }

                #region Validação dos campos

                foreach (var _tipo in _lista)
                {
                    query.Clear();
                    if (_tipo.CodDoctoEsf != "")
                    {
                        query.Append("Select e.docconciliado");
                        if (tipoProcessamento == "Recebidos")
                            query.Append("  from esfentra e");
                        else
                            query.Append("  from esfsaida e");

                        query.Append(" where  e.coddoctoesf in ( " + _tipo.CodDoctoEsf + ")");

                        executar = sessao.CreateQuery(query.ToString());
                        dataReader = executar.ExecuteQuery();

                        using (dataReader)
                        {
                            if (dataReader.Read())
                            {
                                _tipo.Conferido = dataReader["docconciliado"].ToString() == "S";
                            }
                        }
                    }

                    List<Classes.ItensParametrosArquivei> _itens = new ItensParametrosArquiveiDAO().Listar(idParametro);

                    if (_itens.Where(w => w.ValidarCampo &&
                                                  w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.NumeroNF, "")).Count() != 0)
                    {
                        _tipo.NumeroNFValido = _tipo.NumeroNFArquivo == _tipo.NumeroNFGlobus;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.NumeroNFValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.Serie, "")).Count() != 0)
                    {
                        if (_tipo.SerieComparar != "" && _tipo.SerieComparar != null)
                            _tipo.SerieValida = _tipo.SerieComparar == _tipo.SerieGlobus;
                        else
                            _tipo.SerieValida = _tipo.SerieArquivo == _tipo.SerieGlobus;

                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.SerieValida;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.NaturezaOperacao, "")).Count() != 0)
                    {
                        _tipo.NaturezaValida = _tipo.NaturezaOperacaoArquivo == _tipo.NaturezaOperacaoGlobus;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.NaturezaValida;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ChaveDeAcesso, "")).Count() != 0)
                    {
                        _tipo.ChaveAcessoValida = _tipo.ChaveAcessoGlobus == _tipo.ChaveAcessoArquivo;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.ChaveAcessoValida;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ModeloNF, "")).Count() != 0)
                    {
                        _tipo.ModeloValido = _tipo.CodigoModeloArquivo.Contains(_tipo.CodigoModeloGlobus);
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.ModeloValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.CNPJEmitente, "")).Count() != 0)
                    {
                        _tipo.CNPJFornecedorValido = _tipo.CNPJFornecedor == _tipo.CNPJFornecedorArquivo;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.CNPJFornecedorValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.IEEmitente, "")).Count() != 0)
                    {
                        _tipo.IEFornecedorValido = _tipo.IEFornecedorArquivo == _tipo.IEFornecedor;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.IEFornecedorValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.RazaoSocialEmitente, "")).Count() != 0)
                    {
                        _tipo.RazaoSocialFornecedorValida = _tipo.RazaoSocialFornecedorArquivo == _tipo.RazaoSocialFornecedor;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.RazaoSocialFornecedorValida;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.CNPJDestinatario, "")).Count() != 0)
                    {
                        _tipo.CNPJEmpresaValido = _tipo.CNPJEmpresaArquivo == _tipo.CNPJEmpresaGlobus;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.CNPJEmpresaValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.IEDestinatario, "")).Count() != 0)
                    {
                        _tipo.IEEmpresaValido = _tipo.IEEmpresaArquivo == _tipo.IEEmpresaGlobus;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.IEEmpresaValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.RazaoSocialDestinatario, "")).Count() != 0)
                    {
                        _tipo.RazaoSocialEmpresaValida = _tipo.RazaoSocialEmpresaGlobus == _tipo.RazaoSocialEmpresaArquivo;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.RazaoSocialEmpresaValida;
                    }
                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.EnderecoDestinatario, "")).Count() != 0)
                    {
                        _tipo.EnderecoValido = _tipo.EnderecoEmpresaArquivo == _tipo.EnderecoEmpresaGlobus;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.EnderecoValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.EnderecoDestinatario, "")).Count() != 0)
                    {
                        _tipo.NumeroEnderecoValido = _tipo.NumeroEnderecoEmpresaArquivo == _tipo.NumeroEnderecoGlobus;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.NumeroEnderecoValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.BairroDestinatario, "")).Count() != 0)
                    {
                        _tipo.BairroValido = _tipo.BairroEmpresaGlobus == _tipo.BairroEmpresaArquivo;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.BairroValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.CEPDestinatario, "")).Count() != 0)
                    {
                        _tipo.CEPValido = _tipo.CEPEmpresaGlobus == _tipo.CEPEmpresaArquivo;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.CEPValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                         w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.MunicipioOrigem, "")).Count() != 0)
                    {
                        _tipo.MunicipioOrigemValido = _tipo.MunicipioOrigemGlobus == _tipo.MunicipioOrigemArquivo;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.MunicipioOrigemValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                         w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.MunicipioDestino, "")).Count() != 0)
                    {
                        _tipo.MunicipioDestinoValido = _tipo.MunicipioDestinoGlobus == _tipo.MunicipioDestinoArquivo;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.MunicipioDestinoValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.DataEmissao, "")).Count() != 0)
                    {
                        _tipo.EmissaoValida = _tipo.EmissaoGlobus.Date == _tipo.EmissaoArquivo.Date;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.EmissaoValida;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.BaseICMS, "")).Count() != 0)
                    {
                        _tipo.BaseICMSValida = _tipo.BaseICMSGlobus == _tipo.BaseICMSArquivo || _tipo.BaseICMSGlobus + _tipo.ValorIsentasGlobus + _tipo.ValorOutrasGlobus == _tipo.BaseICMSArquivo;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.BaseICMSValida;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ValorTotalNF, "")).Count() != 0)
                    {
                        _tipo.TotalNFValido = _tipo.ValorTotalNFArquivo == _tipo.ValorTotalNFGlobus;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.TotalNFValido;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.IsentasOutrasIgualContabil, "")).Count() != 0)
                    {
                        if (_tipo.ValorOutrasGlobus + _tipo.ValorIsentasGlobus == 0)
                            _tipo.ValidaVlContabil = true;
                        else
                            _tipo.ValidaVlContabil = _tipo.ValorOutrasGlobus + _tipo.ValorIsentasGlobus == _tipo.ValorTotalNFArquivo;

                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.ValidaVlContabil;
                    }

                    if (_itens.Where(w => w.ValidarCampo &&
                                          w.NomeCampo == Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ValorProduto, "")).Count() != 0)
                    {
                        _tipo.ValorProdutoValido = _tipo.ValorProdutosGlobus == _tipo.ValorProdutosArquivo;
                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.ValorProdutoValido;
                    }

                    try
                    {
                        if ((_tipo.Lei == null || _tipo.Lei == "") && _tipo.DadosAdicionaisGlobus != "" && _tipo.DadosAdicionaisGlobus != null)
                            _tipo.ValidaDadosAdicionais = false;
                        else
                            _tipo.ValidaDadosAdicionais = _tipo.Lei.Contains(_tipo.DadosAdicionaisGlobus) || _tipo.DadosAdicionaisGlobus.Contains(_tipo.Lei);

                        _tipo.ComDiferenca = _tipo.ComDiferenca || !_tipo.ValidaDadosAdicionais;
                    }
                    catch { }

                }

                if (Conferidas != "T")
                {
                    foreach (var item in _lista)
                    {
                        if ((!item.Conferido && Conferidas == "S") ||
                            (item.Conferido && Conferidas == "N"))
                            _listaExcluir.Add(item);
                    }

                    foreach (var item in _listaExcluir)
                        _lista.Remove(item);
                }

                foreach (var item in _lista.GroupBy(g => g.ChaveAcessoArquivo).Select(s => s.Key))
                {
                    if (_lista.Where(w => w.ChaveAcessoArquivo == item).Count() > 1)
                    {
                        foreach (var itemD in _lista.Where(w => w.ChaveAcessoArquivo == item))
                        {
                            itemD.Duplicidade = true;
                        }
                    }
                }
                #endregion


                // trazer as inutilizadas
                if (tipoProcessamento != "Recebidos")
                {
                    query.Clear();
                    query.Append("Select n.Codigoempresa, n.Codigofl, n.Codtpdoc, Numeronfglobus, Serieglobus, Emissaoglobus");
                    query.Append("     , f.Nrcli Nrforn, n.Entradasaidanf, Naturezaoperacaoglobus, Baseicmsglobus");
                    query.Append("     , n.Icms_Isentanf, n.Icms_Outrasnf, Valortotalglobus, Coddoctoesf");
                    query.Append("     , n.Codintnf, Ne.Chavedeacesso Chavedeacessoglobus, Modeloglobus");
                    query.Append("     , Formatacnpjcpf(f.Nrinscricaocli, 'CNPJ') Cnpjforncedorglobus");
                    query.Append("     , Eliminanaonumericos(f.Inscestadualcli) Iefornecedorglobus");
                    query.Append("     , f.Rsocialcli Rsocialfornecedorglobus, 'N' Conferido, ne.mensagemrecibo Status");
                    query.Append("     , Statusnf, Origem, chavedeacessonfe, ne.mensagemrecibo Status");
                    query.Append("     , Municorigemglobus, Municdestinoglobus, Coddoctocpg, n.Dadosadicionaisimp, Fc_Niff_Serie_Emitidas(5) Seriecomparar");
                    query.Append("  From (Select n.Codigoempresa, n.Codigofl, n.Codtpdoc, To_Number(n.Numeronf) Numeronfglobus, n.Serienf Serieglobus");
                    query.Append("             , n.Dataemissaonf Emissaoglobus, n.Entradasaidanf, n.Naturezaoperacaonf Naturezaoperacaoglobus");
                    query.Append("             , n.Basecalcicmsnf Baseicmsglobus, n.Icms_Isentanf, n.Icms_Outrasnf, n.Valortotalnf Valortotalglobus");
                    query.Append("             , To_Char(n.Coddoctoesf) Coddoctoesf, n.Codintnf, n.Codmodelo Modeloglobus");
                    query.Append("             , Decode(n.Statusnf, 'C', 'Cancelada',");
                    query.Append("                                  'D', 'Rejeitada',");
                    query.Append("                                  'I', 'Inutilizada', 'Ativa') Statusnf");
                    query.Append("             , n.Codmunicfederal_Origem Municorigemglobus");
                    query.Append("             , n.Codmunicfederal_Destino Municdestinoglobus");
                    query.Append("             ,  n.Coddoctocrc Coddoctocpg, n.Dadosadicionaisimp, 'EST' Origem, n.Codcli, n.chavedeacessonfe");
                    query.Append("          From Bgm_Notafiscal n, Niff_chm_empresas em");
                    query.Append("         Where Lpad(n.Codigoempresa, 3, '0') || '/' || Lpad(n.Codigofl, 3, '0') = em.codigoglobus");
                    query.Append("           And em.idEmpresa = " + IdEmpresa);
                    query.Append("           And n.dataemissaonf Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                    query.Append("           And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("         Union All ");
                    query.Append("        Select e.Codigoempresa, e.Codigofl, e.Codtpdoc, To_Number(e.Numeronf) Numeronfglobus, e.Serienf Serieglobus");
                    query.Append("             , e.dataemissao Emissaoglobus, e.datahoraentsai Entradasaidanf, e.naturezaoperacao Naturezaoperacaoglobus");
                    query.Append("             , e.basecalcicms Baseicmsglobus, e.icmsisentas, e.icmsoutras, e.Valortotalnf Valortotalglobus");
                    query.Append("             , To_Char(e.Coddoctoesf) Coddoctoesf, e.Codintnf, e.Codmodelo Modeloglobus");
                    query.Append("             , Decode(e.Statusnf, 'C', 'Cancelada',");
                    query.Append("                                  'D', 'Rejeitada',");
                    query.Append("                                  'I', 'Inutilizada', 'Ativa') Statusnf");
                    query.Append("             , Null Municorigemglobus, Null Municdestinoglobus, null Coddoctocpg, e.dadosadicionais Dadosadicionaisimp");
                    query.Append("             , 'ESF' Origem, e.Codcli,e.chavedeacesso ");
                    query.Append("          From esfnotafiscal e, Niff_chm_empresas em");
                    query.Append("         Where Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(e.Codigofl, 3, '0') = em.codigoglobus");
                    query.Append("           And em.idEmpresa = " + IdEmpresa);
                    query.Append("           And e.dataemissao Between To_Date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                    query.Append("           And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')) n");
                    query.Append("       , Bgm_Notafiscal_Eletronica   Ne");
                    query.Append("       , Bgm_Cliente                 f");
                    query.Append("   Where n.Codcli = f.Codcli");
                    query.Append("     And (Ne.Codintnf_Bgmnf = n.Codintnf Or ne.codintnf_esfnf = n.codintnf Or ne.codintnfavul = n.codintnf )");
                    query.Append("     And ne.mensagemrecibo Like '%102%'");

                    executar = sessao.CreateQuery(query.ToString());
                    dataReader = executar.ExecuteQuery();

                    encontrou = false;

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            encontrou = false;
                            NotasArquivei _tipo = new NotasArquivei();

                            _tipo.NumeroNFGlobus = Convert.ToDecimal(dataReader["NumeroNFGlobus"].ToString());

                            foreach (var item in _lista.Where(w => w.NumeroNFGlobus == _tipo.NumeroNFGlobus))
                            {
                                encontrou = true;
                                break;
                            }

                            if (!encontrou)
                            {
                                _tipo.TipoProcessamento = tipoProcessamento;
                                _tipo.CodigoEmpresa = Convert.ToInt32(dataReader["codigoEmpresa"].ToString());
                                _tipo.CodigoFilial = Convert.ToInt32(dataReader["codigofl"].ToString());

                                _tipo.Existe = true;
                                _tipo.IdEmpresa = IdEmpresa;

                                _tipo.Origem = dataReader["Origem"].ToString();
                                _tipo.Status = dataReader["Status"].ToString();
                                _tipo.StatusGlobus = dataReader["StatusNF"].ToString();

                                try
                                {
                                    _tipo.DadosAdicionaisGlobus = dataReader["dadosadicionaisimp"].ToString();
                                }
                                catch { }

                                _tipo.Conferido = dataReader["Conferido"].ToString() == "S";

                                if (_tipo.Origem == "EST")
                                    _tipo.IntegradoCPG = dataReader["CodDoctoCPG"].ToString() != "";
                                else
                                    _tipo.IntegradoCPG = EstaIntegradoContasPagar(dataReader["CodDoctoESF"].ToString(), tipoProcessamento) != "";

                                _tipo.TipoDocumento = dataReader["CodTpDoc"].ToString();

                                _tipo.NumeroNFGlobus = Convert.ToDecimal(dataReader["NumeroNFGlobus"].ToString());
                                _tipo.NumeroNFArquivo = _tipo.NumeroNFGlobus;
                                _tipo.IntegradaLivro = dataReader["Coddoctoesf"].ToString() != "";

                                _tipo.SerieGlobus = dataReader["SerieGlobus"].ToString();
                                _tipo.SerieArquivo = _tipo.SerieGlobus;

                                _tipo.CodigoFornecedor = dataReader["Nrforn"].ToString();

                                _tipo.NaturezaOperacaoGlobus = dataReader["naturezaoperacaoGlobus"].ToString();

                                _tipo.ChaveAcessoGlobus = dataReader["chavedeacessonfe"].ToString();

                                _tipo.CodigoModeloGlobus = dataReader["ModeloGlobus"].ToString();

                                _tipo.CNPJFornecedor = dataReader["CNPJForncedorGlobus"].ToString();
                                _tipo.CNPJFornecedorArquivo = _tipo.CNPJFornecedor;

                                _tipo.IEFornecedor = dataReader["IEFornecedorGlobus"].ToString();
                                _tipo.IEFornecedorArquivo = _tipo.IEFornecedor;

                                _tipo.RazaoSocialFornecedor = dataReader["RSocialFornecedorGlobus"].ToString();
                                _tipo.RazaoSocialFornecedorArquivo = _tipo.RazaoSocialFornecedor;


                                _tipo.MunicipioOrigemGlobus = dataReader["MunicOrigemGlobus"].ToString();
                                _tipo.MunicipioDestinoGlobus = dataReader["MunicDestinoGlobus"].ToString();

                                try
                                {
                                    _tipo.Entrada = Convert.ToDateTime(dataReader["EntradaSaidaNF"].ToString());
                                }
                                catch { }

                                try
                                {
                                    _tipo.EmissaoGlobus = Convert.ToDateTime(dataReader["EmissaoGlobus"].ToString());
                                    _tipo.EmissaoArquivo = _tipo.EmissaoGlobus;
                                }
                                catch { }


                                try
                                {
                                    _tipo.BaseICMSGlobus = Convert.ToDecimal(dataReader["baseICMSGlobus"].ToString());
                                }
                                catch { }

                                try
                                {
                                    _tipo.ValorIsentasGlobus = Convert.ToDecimal(dataReader["Icms_Isentanf"].ToString());
                                }
                                catch { }

                                try
                                {
                                    _tipo.ValorOutrasGlobus = Convert.ToDecimal(dataReader["Icms_Outrasnf"].ToString());
                                }
                                catch { }

                                try
                                {
                                    _tipo.ValorTotalNFGlobus = Convert.ToDecimal(dataReader["ValorTotalGlobus"].ToString());
                                    if (_tipo.ValorOutrasGlobus != 0 && _tipo.ValorOutrasGlobus != _tipo.ValorTotalNFGlobus)
                                        _tipo.ValorOutrasGlobus = _tipo.ValorTotalNFGlobus;
                                }
                                catch { }

                                try
                                {
                                    //_tipo.CodDoctoEsf = Convert.ToDecimal(dataReader["coddoctoesf"].ToString());
                                    _tipo.CodDoctoEsf = dataReader["coddoctoesf"].ToString();
                                }
                                catch { }

                                try
                                {
                                    _tipo.CodIntNF = Convert.ToDecimal(dataReader["CodIntNF"].ToString());
                                }
                                catch { }


                                if (_tipo.ChaveAcessoGlobus == null || _tipo.ChaveAcessoGlobus == "")
                                    _tipo.ChaveAcessoGlobus = ChaveDeAcessoNFE_Globus(_tipo.CodIntNF);

                                _tipo.Usuario = UsuarioDocumentoESF(_tipo.CodDoctoEsf);

                                _lista.Add(_tipo);
                            }
                        }
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

        public ImportandoArquivei ConsultarArquivo(string nomeCompleto, string arquivo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            ImportandoArquivei _cfop = new ImportandoArquivei();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select * from niff_fis_ImportandoArquivei");
                query.Append(" where Arquivo = '" + nomeCompleto + "'");
                query.Append("    or Arquivo = '%" + arquivo + "%'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _cfop.Existe = true;
                        _cfop.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _cfop.Importando = dataReader["Importando"].ToString() == "S";
                        _cfop.Arquivo = dataReader["Arquivo"].ToString();
                        _cfop.Data = Convert.ToDateTime(dataReader["data"].ToString());

                        try
                        {
                            _cfop.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
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

            return _cfop;
        }

        public List<ImportandoArquivei> ListarArquivosImportador(int empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ImportandoArquivei> _lista = new List<ImportandoArquivei>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Distinct nomearquivo, dataimportado from niff_fis_arquivei");
                query.Append(" where IdEmpresa = " + empresa);
                query.Append("   and trunc(dataimportado) between to_date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') ");
                query.Append("   and to_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        ImportandoArquivei _cfop = new ImportandoArquivei();
                        _cfop.Existe = true;
                        _cfop.Arquivo = dataReader["nomearquivo"].ToString();
                        _cfop.Data = Convert.ToDateTime(dataReader["dataimportado"].ToString());
                        _lista.Add(_cfop);
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

        public bool GravaImportando(ImportandoArquivei _arq)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!_arq.Existe)
                {
                    query.Clear();
                    query.Append("Insert into niff_fis_ImportandoArquivei (");
                    query.Append("       id, Importando, Arquivo, Data, IdUsuario, idnfeGlobus");
                    query.Append("  ) Values ( (select Nvl(Max(id),0) +1 next from niff_fis_ImportandoArquivei) ");
                    query.Append("  , '" + (_arq.Importando ? "S" : "N") + "'");
                    query.Append("  , '" + _arq.Arquivo + "'");
                    query.Append("  , SysDate");
                    query.Append("  , " + _arq.IdUsuario);
                    if (_arq.IdNFEGlobus == 0)
                        query.Append("  , null");
                    else
                        query.Append("  , " + _arq.IdNFEGlobus);

                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update niff_fis_ImportandoArquivei ");
                    query.Append("   set Arquivo = '" + _arq.Arquivo + "'");
                    query.Append("     , Importando = '" + (_arq.Importando ? "S" : "N") + "'");
                    query.Append("     , DataFinalizado = SysDate");
                    query.Append(" Where Id = " + _arq.Id);
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

        public bool ExcluirImportando(ImportandoArquivei _arq)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Clear();
                query.Append("Delete niff_fis_ImportandoArquivei ");
                query.Append(" Where Id = " + _arq.Id);

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

        public bool ExcluirArquivosImportandos(string nomearquivo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Clear();
                query.Append("Delete niff_fis_itensarquivei ");
                query.Append(" Where idarquivei In (Select t.Idarquivei from niff_fis_arquivei t Where t.Nomearquivo = '" + nomearquivo + "')");

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;
                else
                {

                    query.Clear();
                    query.Append("Delete niff_fis_arquivei ");
                    query.Append(" Where Nomearquivo = '" + nomearquivo + "'");

                    if (!sessao.ExecuteSqlTransaction(query.ToString()))
                        return false;
                    else
                    {
                        query.Clear();
                        query.Append("Delete niff_fis_importandoarquivei ");
                        query.Append(" Where arquivo = '" + nomearquivo + "'");

                        return sessao.ExecuteSqlTransaction(query.ToString());
                    }
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
        }

        private string ChaveDeAcessoNFE_Globus(decimal codintNF)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ImportandoArquivei> _lista = new List<ImportandoArquivei>();
            Publicas.mensagemDeErro = string.Empty;
            string retorno = "";

            try
            {
                query.Append("Select chavedeacesso from bgm_notafiscal_eletronica");
                query.Append(" where codintnf_bgmnf = " + codintNF + " or codintnf_esfnf = " + codintNF + " or codintnf = " + codintNF + " or codintnfavul = " + codintNF);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader3 = executar.ExecuteQuery();

                using (dataReader3)
                {
                    if (dataReader3.Read())
                    {
                        retorno = dataReader3["chavedeacesso"].ToString();
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

            return retorno;
        }

        public List<XmlNFe_Globus> ImportaNFe_XmlGlobus(int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<XmlNFe_Globus> _lista = new List<XmlNFe_Globus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select n.conteudonf, n.id_nfe, decode(idnfeGlobus, Null, 'N','S') Existe, n.chavedeacesso, n.MensagemRecibo");
                query.Append("  From bgm_notafiscal_eletronica n, niff_chm_empresas e, niff_fis_importandoarquivei i");
                query.Append(" where dataemissao between To_date('" + (new DateTime(DateTime.Now.Year, DateTime.Now.Month-1, 1)).ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And sysdate");
                query.Append("   And e.codigoglobus = lPad(empresa,3,'0') || '/' || lPad(filial,3,'0')");
                query.Append("   And n.Id_Nfe = i.idnfeGlobus(+)");
                query.Append("   And IdEmpresa = " + idEmpresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader3 = executar.ExecuteQuery();

                using (dataReader3)
                {
                    while (dataReader3.Read())
                    {
                        XmlNFe_Globus _xml = new XmlNFe_Globus();
                        _xml.ChaveAcesso = dataReader3["chavedeacesso"].ToString();
                        _xml.Status = dataReader3["MensagemRecibo"].ToString();
                        _xml.Existe = dataReader3["Existe"].ToString() == "S";
                        _xml.IdNFEGlobus = Convert.ToDecimal(dataReader3["id_nfe"].ToString());
                        _xml.Xml = dataReader3["conteudonf"].ToString();

                        _lista.Add(_xml);
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

        public List<SequencialNFE> SequencialNFe_Emitidas(int idEmpresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<SequencialNFE> _lista = new List<SequencialNFE>();
            List<SequencialNFE> _listaNumerosFaltantes = new List<SequencialNFE>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Distinct numeronfe, serienfe ");
                query.Append("  From Bgm_Notafiscal_Eletronica e, niff_chm_empresas e");
                query.Append(" where dataemissao between To_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy')");

                if (idEmpresa == 2)
                    query.Append("   And Lpad(empresa,3,'0') || '/' || Lpad(filial, 3, '0') in ('001/001','001/004')");
                else
                     query.Append("   And e.codigoglobus = lPad(empresa,3,'0') || '/' || lPad(filial,3,'0')");

                query.Append("   And e.IdEmpresa = " + idEmpresa);
                query.Append(" Order By numeronfe");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader3 = executar.ExecuteQuery();

                SequencialNFE _xml = new SequencialNFE();

                using (dataReader3)
                {
                    while (dataReader3.Read())
                    {
                        _xml = new SequencialNFE();
                        _xml.Numero = Convert.ToInt32(dataReader3["numeronfe"].ToString());
                        _xml.Serie = dataReader3["serienfe"].ToString();
                        _lista.Add(_xml);
                    }
                }

                _xml = new SequencialNFE();
                int _max = Convert.ToInt32(_lista.Max(m => m.Numero));

                
                foreach (var item in _lista.OrderBy(o => o.Numero))
                {
                    if (_xml.Numero == 0)
                        _xml = item;
                    else
                    {
                        if (item.Numero < _max)
                        {
                            while (_lista.Where(w => w.Numero == _xml.Numero + 1).Count() == 0) // Não encontrou a sequencia.
                            {
                                _xml.Numero = _xml.Numero + 1;
                                _listaNumerosFaltantes.Add(_xml);
                            }
                            _xml = item;
                        }
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

            return _listaNumerosFaltantes;
        }
    }
}
