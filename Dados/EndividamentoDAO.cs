using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class EndividamentoDAO
    {
        IDataReader dataReader;
        IDataReader dataReaderAux;
        IDataReader dataReader2;

        #region Parâmetros
        public List<Endividamento.Parametros> Listar(int idEmpresa, string consulta)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Endividamento.Parametros> lista = new List<Endividamento.Parametros>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select p.Id, p.IdEmpresa, p.CodigoForn, p.CodTpDoc, p.Modalidade, f.CondicaoForn");
                query.Append("     , p.NumeroPlano, p.CodigoContaJurosDebito, p.CodigoContaJurosCredito");
                query.Append("     , p.CodigoContaVariacaoDebito, p.CodigoContaVariacaoCredito");
                query.Append("     , p.CodigoContaCurtoPrazo, p.CodigoContaLongoPrazo");
                query.Append("     , p.HistoricoJuros, p.HistoricoVariacao, p.Lote");
                query.Append("     , p.CodigoContaCurtoPrevisto, p.CodigoContaLongoPrevisto");
                query.Append("     , p.HistoricoJurosConciliacao, p.HistoricoPrevisto");
                query.Append("     , p.CodCustoCTBJuros, p.CodCustoCTBVariacao, p.CodCustoCTBJurosConci, p.CodCustoCTBJurosPrev");
                query.Append("     , e.nomeabreviado, f.nfantasiaforn, f.NrForn");
                query.Append("     , t.desctpdoc, d.codtpdespesa, d.desctpdespesa");
                query.Append("     , p.IdUsuario, u.Nome, u.Email");
                query.Append("  from Niff_CTB_ParametrosEndividamento p, Niff_Chm_Empresas e, Bgm_Fornecedor f, Niff_Chm_usuarios u ");
                query.Append("     , cprtpdoc t, cpgtpdes d");
                query.Append(" Where e.idEmpresa = " + idEmpresa);
                query.Append("   and e.Idempresa = p.Idempresa");
                query.Append("   And f.codigoforn(+) = p.codigoforn");
                query.Append("   And u.IdUsuario(+) = p.IdUsuario");
                query.Append("   And d.codtpdespesa(+) = p.codtpdespesa");
                query.Append("   And t.codtpdoc(+) = p.codtpdoc");
                query.Append("   And lpad(t.codigoempresa(+),3,'0') || '/' || lpad(t.codigoFl(+),3,'0') = e.codigoglobus");
                
                if (consulta == "T")
                    query.Append("   And p.codtpdoc is not null");                    
                else
                {
                    if (consulta == "F")
                        query.Append("   And p.codigoforn is not null");
                    else
                    if (consulta == "U")
                        query.Append("   And p.IdUsuario is not null");
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Endividamento.Parametros _tipo = new Endividamento.Parametros();
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());

                        try
                        {
                            _tipo.CodigoFornecedor = Convert.ToDecimal(dataReader["CodigoForn"].ToString());
                        }
                        catch { }

                        _tipo.NomeFantasia = dataReader["NrForn"].ToString() + " - " + dataReader["NFantasiaForn"].ToString();
                        _tipo.NomeEmpresa = dataReader["nomeabreviado"].ToString();
                        _tipo.Modalidade = dataReader["Modalidade"].ToString();
                        _tipo.CodigoTipoDocumento = dataReader["CodTpDoc"].ToString();
                        _tipo.TipoDocto = dataReader["desctpdoc"].ToString();

                        _tipo.TipoDocto = dataReader["CodTpDoc"].ToString() + " - " + dataReader["desctpdoc"].ToString();
                        _tipo.TipoDespesa = dataReader["codtpdespesa"].ToString() + " - " + dataReader["desctpdespesa"].ToString();
                        _tipo.CodigoDespesa = dataReader["codtpdespesa"].ToString();

                        _tipo.HistoricoJuros = dataReader["HistoricoJuros"].ToString();
                        _tipo.HistoricoVariacao = dataReader["HistoricoVariacao"].ToString();
                        _tipo.Lote = dataReader["Lote"].ToString();

                        _tipo.HistoricoJurosConciliacao = dataReader["HistoricoJurosConciliacao"].ToString();
                        _tipo.HistoricoPrevisto = dataReader["HistoricoPrevisto"].ToString();

                        try
                        {
                            _tipo.Plano = Convert.ToInt32(dataReader["NumeroPlano"].ToString());
                        }
                        catch { }

                        RateioBeneficios.ContasContabeis _conta = new RateioBeneficios.ContasContabeis();
                        try
                        {
                            _tipo.CodigoContaCurtoPrazo = Convert.ToInt32(dataReader["Codigocontacurtoprazo"].ToString());
                            _conta = new RateioBeneficiosDAO().Consulta(_tipo.Plano, _tipo.CodigoContaCurtoPrazo);
                            _tipo.ContaCurtoPrazo = _conta.Codigo + " " + _conta.Nome;
                        }
                        catch { }

                        try
                        {
                            _tipo.CodigoContaLongoPrazo = Convert.ToInt32(dataReader["Codigocontalongoprazo"].ToString());
                            _conta = new RateioBeneficiosDAO().Consulta(_tipo.Plano, _tipo.CodigoContaLongoPrazo);
                            _tipo.ContaLongoPrazo = _conta.Codigo + " " + _conta.Nome;
                        }
                        catch { }

                        try
                        {
                            _tipo.CodigoContaCurtoPrevisto = Convert.ToInt32(dataReader["CodigoContaCurtoPrevisto"].ToString());
                            _conta = new RateioBeneficiosDAO().Consulta(_tipo.Plano, _tipo.CodigoContaCurtoPrevisto);
                            _tipo.ContaCurtoPrevisto = _conta.Codigo + " " + _conta.Nome;
                        }
                        catch { }


                        try
                        {
                            _tipo.CodigoContaLongoPrevisto = Convert.ToInt32(dataReader["CodigoContaLongoPrevisto"].ToString());
                            _conta = new RateioBeneficiosDAO().Consulta(_tipo.Plano, _tipo.CodigoContaLongoPrevisto);
                            _tipo.ContaLongoPrevisto = _conta.Codigo + " " + _conta.Nome;
                        }
                        catch { }

                        try
                        {
                            _tipo.CodigoContaJurosDebito = Convert.ToInt32(dataReader["Codigocontajurosdebito"].ToString());
                            _conta = new RateioBeneficiosDAO().Consulta(_tipo.Plano, _tipo.CodigoContaJurosDebito);
                            _tipo.ContaJurosDebito = _conta.Codigo + " " + _conta.Nome;
                        }
                        catch { }

                        try
                        {
                            _tipo.CodigoContaJurosCredito = Convert.ToInt32(dataReader["Codigocontajuroscredito"].ToString());
                            _conta = new RateioBeneficiosDAO().Consulta(_tipo.Plano, _tipo.CodigoContaJurosCredito);
                            _tipo.ContaJurosCredito = _conta.Codigo + " " + _conta.Nome;
                        }
                        catch { }

                        try
                        {
                            _tipo.CodigoContaVariacaoDebito = Convert.ToInt32(dataReader["Codigocontavariacaodebito"].ToString());
                            _conta = new RateioBeneficiosDAO().Consulta(_tipo.Plano, _tipo.CodigoContaVariacaoDebito);
                            _tipo.ContaVariacaoDebito = _conta.Codigo + " " + _conta.Nome;
                        }
                        catch { }

                        try
                        {
                            _tipo.CodigoContaVariacaoCredito = Convert.ToInt32(dataReader["Codigocontavariacaocredito"].ToString());
                            _conta = new RateioBeneficiosDAO().Consulta(_tipo.Plano, _tipo.CodigoContaVariacaoCredito);
                            _tipo.ContaVariacaoCredito = _conta.Codigo + " " + _conta.Nome;
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.CustoJuros = Convert.ToInt32(dataReader["CodCustoCTBJuros"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CustoVariacao = Convert.ToInt32(dataReader["CodCustoCTBVariacao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CustoJurosConciliacao = Convert.ToInt32(dataReader["CodCustoCTBJurosConci"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CustoPrevsto = Convert.ToInt32(dataReader["CodCustoCTBJurosPrev"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        }
                        catch { }

                        _tipo.NomeUsuario = dataReader["Nome"].ToString();
                        _tipo.Email = dataReader["Email"].ToString();
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

        public bool Gravar(List<Endividamento.Parametros> _lista)
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
                        query.Append("Insert into Niff_CTB_ParametrosEndividamento");
                        query.Append(" ( id, idempresa, codigoforn, codtpdoc, Modalidade, CodTpDespesa");
                        query.Append(" , NumeroPlano, CodigoContaJurosDebito, CodigoContaJurosCredito");
                        query.Append(" , CodigoContaVariacaoDebito, CodigoContaVariacaoCredito");
                        query.Append(" , CodigoContaCurtoPrazo, CodigoContaLongoPrazo");
                        query.Append(" , HistoricoJuros, HistoricoVariacao, Lote");
                        query.Append(" , CodigoContaCurtoPrevisto, CodigoContaLongoPrevisto");
                        query.Append(" , HistoricoJurosConciliacao, HistoricoPrevisto");
                        query.Append(" , CodCustoCTBJuros, CodCustoCTBVariacao, CodCustoCTBJurosConci, CodCustoCTBJurosPrev, idUsuario");
                        query.Append(") Values ( (Select nvl(Max(id),0) +1 from Niff_CTB_ParametrosEndividamento) ");
                        query.Append("        , " + item.IdEmpresa);

                        if (item.CodigoFornecedor == 0)
                            query.Append("        , null");
                        else
                            query.Append("        , " + item.CodigoFornecedor);

                        query.Append("        , '" + item.CodigoTipoDocumento + "'");
                        query.Append("        , '" + item.Modalidade + "'");
                        query.Append("        , '" + item.CodigoDespesa + "'");

                        if (item.Plano != 0)
                            query.Append("     , " + item.Plano);
                        else
                            query.Append("     , null");

                        if (item.CodigoContaJurosDebito != 0)
                            query.Append("     , " + item.CodigoContaJurosDebito);
                        else
                            query.Append("     , null");

                        if (item.CodigoContaJurosCredito != 0)
                            query.Append("     , " + item.CodigoContaJurosCredito);
                        else
                            query.Append("     , null");

                        if (item.CodigoContaVariacaoDebito != 0)
                            query.Append("     , " + item.CodigoContaVariacaoDebito);
                        else
                            query.Append("     , null");

                        if (item.CodigoContaVariacaoCredito != 0)
                            query.Append("     , " + item.CodigoContaVariacaoCredito);
                        else
                            query.Append("     , null");

                        if (item.CodigoContaCurtoPrazo != 0)
                            query.Append("     , " + item.CodigoContaCurtoPrazo);
                        else
                            query.Append("     , null");


                        if (item.CodigoContaLongoPrazo != 0)
                            query.Append("     , " + item.CodigoContaLongoPrazo);
                        else
                            query.Append("     , null");

                        query.Append("     , '" + item.HistoricoJuros + "'");
                        query.Append("     , '" + item.HistoricoVariacao + "'");
                        query.Append("     , '" + item.Lote + "'");

                        if (item.CodigoContaCurtoPrevisto != 0)
                            query.Append("     , " + item.CodigoContaCurtoPrevisto);
                        else
                            query.Append("     , null");

                        if (item.CodigoContaLongoPrevisto != 0)
                            query.Append("     , " + item.CodigoContaLongoPrevisto);
                        else
                            query.Append("     , null");

                        query.Append("     , '" + item.HistoricoJurosConciliacao + "'");
                        query.Append("     , '" + item.HistoricoPrevisto + "'");


                        if (item.CustoJuros != 0 && item.CustoJuros != null)
                            query.Append("     , " + item.CustoJuros);
                        else
                            query.Append("     , null");


                        if (item.CustoVariacao != 0 && item.CustoVariacao != null)
                            query.Append("     , " + item.CustoVariacao);
                        else
                            query.Append("     , null");


                        if (item.CustoJurosConciliacao != 0 && item.CustoJurosConciliacao != null)
                            query.Append("     , " + item.CustoJurosConciliacao);
                        else
                            query.Append("     , null");


                        if (item.CustoPrevsto != 0 && item.CustoPrevsto != null)
                            query.Append("     , " + item.CustoPrevsto);
                        else
                            query.Append("     , null");

                        if (item.IdUsuario != 0)
                            query.Append("     , " + item.IdUsuario);
                        else
                            query.Append("     , null");

                        query.Append(" )");
                    }
                    else
                    {
                        query.Append("Update Niff_CTB_ParametrosEndividamento");
                        query.Append("   set modalidade = '" + item.Modalidade + "'");
                        query.Append("     , CodTpDespesa = '" + item.CodigoDespesa + "'");
                        if (item.Plano != 0)
                            query.Append("     , NumeroPlano = " + item.Plano);

                        if (item.CodigoContaJurosDebito != 0)
                            query.Append("     , CodigoContaJurosDebito = " + item.CodigoContaJurosDebito);
                        else
                            query.Append("     , CodigoContaJurosDebito = null " );

                        if (item.CodigoContaJurosCredito != 0)
                            query.Append("     , CodigoContaJurosCredito = " + item.CodigoContaJurosCredito);
                        else
                            query.Append("     , CodigoContaJurosCredito = null " );

                        if (item.CodigoContaVariacaoDebito != 0)
                            query.Append("     , CodigoContaVariacaoDebito = " + item.CodigoContaVariacaoDebito);
                        else
                            query.Append("     , CodigoContaVariacaoDebito = null ");

                        if (item.CodigoContaVariacaoCredito != 0)
                            query.Append("     , CodigoContaVariacaoCredito = " + item.CodigoContaVariacaoCredito);
                        else
                            query.Append("     , CodigoContaVariacaoCredito = null");

                        if (item.CodigoContaCurtoPrazo != 0)
                            query.Append("     , CodigoContaCurtoPrazo = " + item.CodigoContaCurtoPrazo);
                        else
                            query.Append("     , CodigoContaCurtoPrazo = null");

                        if (item.CodigoContaLongoPrazo != 0)
                            query.Append("     , CodigoContaLongoPrazo = " + item.CodigoContaLongoPrazo);
                        else
                            query.Append("     , CodigoContaLongoPrazo = null");

                        query.Append("     , HistoricoJuros = '" + item.HistoricoJuros + "'");
                        query.Append("     , HistoricoVariacao = '" + item.HistoricoVariacao + "'");
                        query.Append("     , Lote = '" + item.Lote + "'");

                        if (item.CodigoContaCurtoPrevisto != 0)
                            query.Append("     , CodigoContaCurtoPrevisto = " + item.CodigoContaCurtoPrevisto);
                        else
                            query.Append("     , CodigoContaCurtoPrevisto = null");

                        if (item.CodigoContaLongoPrevisto != 0)
                            query.Append("     , CodigoContaLongoPrevisto = " + item.CodigoContaLongoPrevisto);
                        else
                            query.Append("     , CodigoContaLongoPrevisto = null");

                        query.Append("     , HistoricoJurosConciliacao = '" + item.HistoricoJurosConciliacao + "'");
                        query.Append("     , HistoricoPrevisto = '" + item.HistoricoPrevisto + "'");

                        if (item.CustoJuros != 0 && item.CustoJuros != null)
                            query.Append("     , CodCustoCTBJuros = " + item.CustoJuros);
                        else
                            query.Append("     , CodCustoCTBJuros = null");

                        if (item.CustoVariacao != 0 && item.CustoVariacao != null)
                            query.Append("     , CodCustoCTBVariacao = " + item.CustoVariacao);
                        else
                            query.Append("     , CodCustoCTBVariacao = null");

                        if (item.CustoJurosConciliacao != 0 && item.CustoJurosConciliacao != null)
                            query.Append("     , CodCustoCTBJurosConci = " + item.CustoJurosConciliacao);
                        else
                            query.Append("     , CodCustoCTBJurosConci = null");

                        if (item.CustoPrevsto != 0 && item.CustoPrevsto != null)
                            query.Append("     , CodCustoCTBJurosPrev = " + item.CustoPrevsto);
                        else
                            query.Append("     , CodCustoCTBJurosPrev = null");

                        if (item.IdUsuario != 0)
                            query.Append("     , IdUsuario = " + item.IdUsuario);

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

        public bool ExcluirParametros(int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_CTB_ParametrosEndividamento");
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

        public bool ExcluirFornecedor(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_CTB_ParametrosEndividamento");
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
        #endregion

        #region Endividamento
        public Endividamento.Valores BuscarDocumentoContasPagar(Classes.Empresa empresa,
                                                                      decimal codigoFornecedor, string modalidade, string tipo, string documento, int parcela, string serie)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Endividamento.Valores _tipo = new Endividamento.Valores();

            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Sum(i.valoritemdoc) Previsto");
                query.Append("     , Sum(fc_cpg_vlrliquido(d.coddoctocpg)) Realizado");
                query.Append("     , d.codtpdoc, decode(d.nrodoctocpg, null, null, d.nrodoctocpg || ' parcela ' || d.nroparcelacpg || ' série ' || d.seriedoctocpg) Documento");
                query.Append("     , d.vencimentoCpg, d.pagamentocpg, d.coddoctoCPG, d.seriedoctocpg");
                query.Append("  from cpgdocto d, cpgitdoc i, bgm_fornecedor f, cpgtpdes tp, niff_ctb_parametrosendividamento pe, niff_chm_empresas e ");
                query.Append(" Where i.coddoctocpg = d.coddoctocpg");
                query.Append("   and d.codtpdoc In (Select codtpDoc From niff_ctb_parametrosendividamento Where idEmpresa = e.Idempresa)");
                query.Append("   And d.statusdoctocpg <> 'C'");
                query.Append("   And f.codigoforn = d.codigoforn");
                query.Append("   And i.codtpdespesa = tp.codtpdespesa");
                query.Append("   And pe.codigoforn = f.codigoforn");
                query.Append("   And pe.codtpdespesa = tp.codtpdespesa");
                query.Append("   And lpad(d.codigoempresa,3,'0') || '/' || lpad(d.codigoFl, 3, '0') = e.codigoglobus");
                query.Append("   And e.Idempresa = pe.Idempresa");
                query.Append("   And e.IdEmpresa = " + empresa.IdEmpresa);
                query.Append("   And pe.CodigoForn = " + codigoFornecedor);
                query.Append("   And pe.Modalidade = '" + modalidade + "'");

                query.Append("   And lPad(d.nrodoctocpg,10,'0') = '" + documento + "'");
                query.Append("   And d.nroparcelacpg = " + parcela);
                query.Append("   And d.codtpdoc = '" + tipo + "'");
                query.Append("   And d.seriedoctocpg = '" + serie + "'");
                query.Append("   Group by d.Codtpdoc, d.Nrodoctocpg, d.Nroparcelacpg, d.Vencimentocpg, d.Pagamentocpg, d.coddoctoCPG, d.seriedoctocpg");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                int i = 1;
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.IdEmpresa = empresa.IdEmpresa;

                        try
                        {
                            _tipo.CodigoFornecedor = codigoFornecedor;
                        }
                        catch { }

                        _tipo.Id = i++;
                        _tipo.Modalidade = modalidade;
                        _tipo.CodigoTipoDocumento = dataReader["CodTpDoc"].ToString();
                        _tipo.Documento = dataReader["Documento"].ToString();

                        try
                        {
                            _tipo.CodigoInternoCPG = Convert.ToDecimal(dataReader["coddoctoCPG"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PrevistoCPG = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Previsto = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        }
                        catch { }

                        _tipo.Variacao = _tipo.Realizado - _tipo.Previsto;

                        try
                        {
                            _tipo.Vencimento = Convert.ToDateTime(dataReader["vencimentoCpg"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Pagamento = Convert.ToDateTime(dataReader["pagamentocpg"].ToString());
                            _tipo.Pagamentos = _tipo.Pagamento.ToShortDateString();
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

        public List<Endividamento.Valores> BuscarDocumentoContasPagar(Classes.Empresa empresa, 
                                                                      decimal codigoFornecedor, string modalidade, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Endividamento.Valores> lista = new List<Endividamento.Valores>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Sum(i.valoritemdoc) Previsto");
                query.Append("     , Sum(fc_cpg_vlrliquido(d.coddoctocpg)) Realizado");
                query.Append("     , d.codtpdoc, decode(d.nrodoctocpg, null, null, d.nrodoctocpg || ' parcela ' || d.nroparcelacpg || ' série ' || d.seriedoctocpg) Documento");
                query.Append("     , d.vencimentoCpg, d.pagamentocpg, d.coddoctoCPG");
                query.Append("  from cpgdocto d, cpgitdoc i, bgm_fornecedor f, cpgtpdes tp, niff_ctb_parametrosendividamento pe, niff_chm_empresas e ");
                query.Append(" Where i.coddoctocpg = d.coddoctocpg");
                query.Append("   and d.codtpdoc In (Select codtpDoc From niff_ctb_parametrosendividamento Where idEmpresa = e.Idempresa)");
                query.Append("   And d.statusdoctocpg <> 'C'");
                query.Append("   And d.vencimentoCpg BETWEEN To_Date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And (d.pagamentocpg Is Null Or d.pagamentocpg >= To_Date('" + inicio.ToShortDateString() + "','dd/mm/yyyy'))");
                query.Append("   And f.codigoforn = d.codigoforn");
                query.Append("   And i.codtpdespesa = tp.codtpdespesa");
                query.Append("   And pe.codigoforn = f.codigoforn");
                query.Append("   And pe.codtpdespesa = tp.codtpdespesa");
                query.Append("   And lpad(d.codigoempresa,3,'0') || '/' || lpad(d.codigoFl, 3, '0') = e.codigoglobus");
                query.Append("   And e.Idempresa = pe.Idempresa");
                query.Append("   And e.IdEmpresa = " + empresa.IdEmpresa);
                query.Append("   And pe.CodigoForn = " + codigoFornecedor);
                query.Append("   And pe.Modalidade = '" + modalidade + "'");
                query.Append("   Group by d.Codtpdoc, d.Nrodoctocpg, d.Nroparcelacpg, d.Vencimentocpg, d.Pagamentocpg, d.coddoctoCPG, d.seriedoctocpg");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                int i = 1;
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Endividamento.Valores _tipo = new Endividamento.Valores();
                        _tipo.IdEmpresa = empresa.IdEmpresa;

                        try
                        {
                            _tipo.CodigoFornecedor = codigoFornecedor;
                        }
                        catch { }

                        _tipo.Id = i++;
                        _tipo.Modalidade = modalidade;
                        _tipo.CodigoTipoDocumento = dataReader["CodTpDoc"].ToString();
                        _tipo.Documento = dataReader["Documento"].ToString();

                        try
                        {
                            _tipo.CodigoInternoCPG = Convert.ToDecimal(dataReader["coddoctoCPG"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PrevistoCPG = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Previsto = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        }
                        catch { }

                        _tipo.Variacao = _tipo.Realizado - _tipo.Previsto;

                        try
                        {
                            _tipo.Vencimento = Convert.ToDateTime(dataReader["vencimentoCpg"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Pagamento = Convert.ToDateTime(dataReader["pagamentocpg"].ToString());
                            _tipo.Pagamentos = _tipo.Pagamento.ToShortDateString();
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

        private Endividamento.Valores BuscarDocumentoContasPagar(decimal coddoctocpg)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Endividamento.Valores _tipo = new Endividamento.Valores();

            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select d.codtpdoc, d.nrodoctocpg || ' parcela ' || d.nroparcelacpg || ' série ' || d.seriedoctocpg Documento");
                query.Append("     , d.vencimentoCpg, d.pagamentocpg, d.coddoctoCPG");
                query.Append("  from cpgdocto d");
                query.Append(" Where d.statusdoctocpg <> 'C'");
                query.Append("   And d.coddoctoCPG = " + coddoctocpg );
                query.Append("   Group by d.Codtpdoc, d.Nrodoctocpg, d.Nroparcelacpg, d.Vencimentocpg, d.Pagamentocpg, d.coddoctoCPG, d.seriedoctocpg");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReaderAux = executar.ExecuteQuery();
                                
                using (dataReaderAux)
                {
                    while (dataReaderAux.Read())
                    {
                        _tipo.CodigoTipoDocumento = dataReaderAux["CodTpDoc"].ToString();
                        _tipo.Documento = dataReaderAux["Documento"].ToString();

                        try
                        {
                            _tipo.CodigoInternoCPG = Convert.ToDecimal(dataReaderAux["coddoctoCPG"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Vencimento = Convert.ToDateTime(dataReaderAux["vencimentoCpg"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Pagamento = Convert.ToDateTime(dataReaderAux["pagamentocpg"].ToString());
                            _tipo.Pagamentos = _tipo.Pagamento.ToShortDateString();
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

        public decimal ValorContasPagar(Classes.Empresa empresa, decimal codigoFornecedor, string modalidade, DateTime fim, string tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;
            
            DateTime _data = fim;

            if (tipo == "C")
                fim = fim.AddDays(1);
            else
                fim = fim.AddMonths(12);

            DateTime _dataFim = _data.AddMonths(12);

            try
            {
                query.Append("Select Sum(i.valoritemdoc) Previsto");
                query.Append("  from cpgdocto d, cpgitdoc i, bgm_fornecedor f, cpgtpdes tp, niff_ctb_parametrosendividamento pe, niff_chm_empresas e ");
                query.Append(" Where i.coddoctocpg = d.coddoctocpg");
                query.Append("   and d.codtpdoc In (Select codtpDoc From niff_ctb_parametrosendividamento Where idEmpresa = e.Idempresa)");
                query.Append("   And d.statusdoctocpg <> 'C'");

                if (tipo == "C")
                    query.Append("   And d.vencimentoCpg between To_Date('" + fim.ToShortDateString() + "','dd/mm/yyyy') and To_Date('" + _dataFim.ToShortDateString() + "','dd/mm/yyyy')"); 
                else
                    query.Append("   And d.vencimentoCpg > To_Date('" + fim.ToShortDateString() + "','dd/mm/yyyy')");

                query.Append("   And (d.pagamentocpg Is Null Or d.pagamentocpg >= To_Date('" + fim.ToShortDateString() + "','dd/mm/yyyy')) ");
                query.Append("   And f.codigoforn = d.codigoforn");
                query.Append("   And i.codtpdespesa = tp.codtpdespesa");
                query.Append("   And pe.codigoforn = f.codigoforn");
                query.Append("   And pe.codtpdespesa = tp.codtpdespesa");
                query.Append("   And lpad(d.codigoempresa,3,'0') || '/' || lpad(d.codigoFl, 3, '0') = e.codigoglobus");
                query.Append("   And e.Idempresa = pe.Idempresa");
                query.Append("   And e.IdEmpresa = " + empresa.IdEmpresa);
                query.Append("   And pe.CodigoForn = " + codigoFornecedor);
                query.Append("   And pe.Modalidade = '" + modalidade + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReaderAux = executar.ExecuteQuery();

                using (dataReaderAux)
                {
                    if (dataReaderAux.Read())
                        _valor = Convert.ToDecimal(dataReaderAux["Previsto"].ToString());
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
            return _valor;
        }

        public Endividamento.Valores Consultar(int IdEmpresa, decimal codigoFornecedor, string modalidade, string tipo, string referencia, string referenciaAnterior = "", bool ignorarAntecipados = false)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            Endividamento.Valores _tipo = new Endividamento.Valores();

            try
            {
                // esse busca na tela e para buscar o futuro. para a tela deve ignorar os excluidosNoCPG, porem no futuro nao pode se a data de referencia da exclusão for menor. 
                
                query.Append("Select e.id, e.idempresa, e.referencia, e.codigoforn, e.modalidade, e.tipo, e.encerrado, f.Nfantasiaforn, f.nrforn");
                query.Append("     , Sum(v.previsto) previsto, sum(v.realizado) realizado, sum(v.juros) juros");
                query.Append("  from Niff_CTB_Endividamento e, bgm_fornecedor f, niff_ctb_valoresendividamento v, CPGDocto d");
                query.Append(" Where e.IdEmpresa = " + IdEmpresa);
                query.Append("   And e.CodigoForn = " + codigoFornecedor);
                query.Append("   And e.Modalidade = '" + modalidade + "'");
                query.Append("   And e.Tipo = '" + tipo + "'");
                query.Append("   And e.Referencia = " + referencia);
                query.Append("   And f.codigoforn = e.codigoforn");
                query.Append("   And v.Idendividamento = e.Id");
                query.Append("   And d.coddoctocpg(+) = v.coddoctocpg");

                if (ignorarAntecipados)
                    query.Append("   And (d.pagamentocpg Is Null Or d.pagamentocpg >= To_Date('01/' || substr(referencia,5,2) || '/' || substr(referencia,1,4) ,'dd/mm/yyyy'))");

                // if (referenciaAnterior == "")
                query.Append("   And v.ExcluidoNoCPG = 'N'");
                /*else
                {
                    query.Append("   And (v.ExcluidoNoCPG = 'N'");
                    query.Append("    or (v.UtilizadoNaReferencia = " + referenciaAnterior);
                    query.Append("   And v.ExcluidoNoCPG = 'S'))");
                }*/

                query.Append(" Group By e.id, e.idempresa, e.referencia, e.codigoforn, e.modalidade, e.tipo, e.encerrado, f.Nfantasiaforn, f.nrforn");
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        
                        _tipo.IdEmpresa = IdEmpresa;

                        try
                        {
                            _tipo.CodigoFornecedor = codigoFornecedor;
                        }
                        catch { }

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Modalidade = modalidade;
                        _tipo.Tipo = tipo;
                        _tipo.Encerrado = dataReader["Encerrado"].ToString() == "S";
                        _tipo.Referencia = dataReader["Referencia"].ToString();

                        try
                        {
                            _tipo.Previsto = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Juros = Convert.ToDecimal(dataReader["Juros"].ToString());
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

        public Endividamento.Valores ConsultarPorId(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            Endividamento.Valores _tipo = new Endividamento.Valores();

            try
            {
                query.Append("Select e.id, e.idempresa, e.referencia, e.codigoforn, e.modalidade, e.tipo, e.encerrado, f.Nfantasiaforn, f.nrforn");
                query.Append("     , Sum(v.previsto) previsto, sum(v.realizado) realizado, sum(v.juros) juros");
                query.Append("  from Niff_CTB_Endividamento e, bgm_fornecedor f, niff_ctb_valoresendividamento v");
                query.Append(" Where e.id = " + id);
                query.Append("   And f.codigoforn = e.codigoforn");
                query.Append("   And v.Idendividamento = e.Id");
                query.Append(" Group By e.id, e.idempresa, e.referencia, e.codigoforn, e.modalidade, e.tipo, e.encerrado, f.Nfantasiaforn, f.nrforn");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {

                        
                        try
                        {
                            _tipo.CodigoFornecedor = Convert.ToDecimal(dataReader["codigoforn"].ToString());
                        }
                        catch { }

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        //_tipo.Modalidade = modalidade;
                        //_tipo.Tipo = tipo;
                        _tipo.Encerrado = dataReader["Encerrado"].ToString() == "S";
                        _tipo.Referencia = dataReader["Referencia"].ToString();

                        try
                        {
                            _tipo.Previsto = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Juros = Convert.ToDecimal(dataReader["Juros"].ToString());
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

        public decimal ConsultarJuros(int IdEmpresa, decimal codigoFornecedor, string modalidade, string tipo,  DateTime fim, bool curtoPrazo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            decimal _valor = 0;
            DateTime _data = fim;

            if (curtoPrazo)
                fim = fim.AddDays(1);
            else
                fim = fim.AddMonths(12);

            DateTime _dataFim = _data.AddMonths(12);

            string referencia = fim.Year.ToString() + fim.Month.ToString("00");
            string referenciaFim = _dataFim.Year.ToString() + _dataFim.Month.ToString("00");

            try
            {
                query.Append("Select sum(v.juros) juros");
                query.Append("  from Niff_CTB_Endividamento e, Niff_Ctb_Valoresendividamento v, CPGDocto d");
                query.Append(" Where e.IdEmpresa = " + IdEmpresa);
                query.Append("   And e.CodigoForn = " + codigoFornecedor);
                query.Append("   And e.Modalidade = '" + modalidade + "'");
                query.Append("   And e.Tipo = '" + tipo + "'");
                query.Append("   And v.Idendividamento = e.Id");
                query.Append("   And v.ExcluidoNoCPG = 'N'");

                query.Append("   And d.coddoctocpg(+) = v.coddoctocpg");
                query.Append("   And (d.pagamentocpg Is Null Or d.pagamentocpg >= To_Date('01/' || substr(referencia,5,2) || '/' || substr(referencia,1,4) ,'dd/mm/yyyy'))");

                if (curtoPrazo)
                    query.Append("   And e.Referencia between " + referencia + " and " + referenciaFim);
                else
                    query.Append("   And e.Referencia > " + referencia );

                Query executar = sessao.CreateQuery(query.ToString());

                dataReaderAux = executar.ExecuteQuery();

                using (dataReaderAux)
                {
                    if (dataReaderAux.Read())
                    {

                        _valor = Convert.ToDecimal(dataReaderAux["Juros"].ToString());
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

            return _valor;
        }
        
        public List<Endividamento.Valores> Consultar(int IdEmpresa, string tipo, string referencia, Empresa empresa, DateTime _dataFim, bool consolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<Endividamento.Valores> _lista = new List<Endividamento.Valores>();
            Int32 _referenciaAux = Convert.ToInt32(referencia) + 100;
            Endividamento.Valores _val = new Endividamento.Valores();

            List<Endividamento.Parametros> _listaParametros = new EndividamentoDAO().Listar(IdEmpresa, "F");

            try
            {
                query.Append("Select e.id, e.idempresa, e.referencia, e.codigoforn, e.modalidade, e.tipo, e.encerrado, f.Nfantasiaforn, f.nrforn");
                query.Append("     , Sum(v.previsto) previsto, sum(v.realizado) realizado, sum(v.juros) juros"); 
                query.Append("  from Niff_CTB_Endividamento e, bgm_fornecedor f, niff_ctb_valoresendividamento v");
                query.Append(" Where e.IdEmpresa = " + IdEmpresa);
                query.Append("   And e.Tipo = '" + tipo + "'");
                query.Append("   And e.Referencia = " + referencia);
                query.Append("   And f.codigoforn = e.codigoforn");
                query.Append("   And v.Idendividamento = e.Id");
                query.Append("   And v.ExcluidoNoCPG = 'N'");

                query.Append(" Group By e.id, e.idempresa, e.referencia, e.codigoforn, e.modalidade, e.tipo, e.encerrado, f.Nfantasiaforn, f.nrforn");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader2 = executar.ExecuteQuery();

                using (dataReader2)
                {
                    while (dataReader2.Read())
                    {
                        Endividamento.Valores _tipo = new Endividamento.Valores();

                        _tipo.IdEmpresa = IdEmpresa;

                        try
                        {
                            _tipo.CodigoFornecedor = Convert.ToDecimal(dataReader2["codigoforn"].ToString()); 
                        }
                        catch { }

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader2["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader2["IdEmpresa"].ToString());
                        _tipo.Modalidade = dataReader2["Modalidade"].ToString();
                        _tipo.Tipo = dataReader2["Tipo"].ToString();
                        _tipo.Encerrado = dataReader2["Encerrado"].ToString() == "S";
                        _tipo.Referencia = dataReader2["Referencia"].ToString();
                        _tipo.Fornecedor = dataReader2["Nrforn"].ToString() + " - " + dataReader2["Nfantasiaforn"].ToString();

                        /*
                        try
                        {
                            _tipo.Previsto = Convert.ToDecimal(dataReader2["Previsto"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Realizado = Convert.ToDecimal(dataReader2["Realizado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Juros = Convert.ToDecimal(dataReader2["Juros"].ToString());
                        }
                        catch { }*/

                        _tipo.Previsto = ConsultarValor(IdEmpresa, _tipo.Tipo, _tipo.Referencia, _tipo.Modalidade, _tipo.CodigoFornecedor, "P");
                        _tipo.Realizado = ConsultarValor(IdEmpresa, _tipo.Tipo, _tipo.Referencia, _tipo.Modalidade, _tipo.CodigoFornecedor, "R");
                        _tipo.Juros = ConsultarValor(IdEmpresa, _tipo.Tipo, _tipo.Referencia, _tipo.Modalidade, _tipo.CodigoFornecedor, "J");

                        _tipo.Variacao = _tipo.Realizado - _tipo.Previsto;

                        decimal _valorCTB = new EndividamentoDAO().SaldoContabil(empresa.CodigoEmpresaGlobus, referencia, IdEmpresa, _tipo.CodigoFornecedor, _tipo.Modalidade, "P", "C", consolidar);
                        _tipo.CTBCurto = Math.Abs(_valorCTB);

                        _valorCTB = new EndividamentoDAO().SaldoContabil(empresa.CodigoEmpresaGlobus, referencia, IdEmpresa, _tipo.CodigoFornecedor, _tipo.Modalidade, "P", "L", consolidar);
                        _tipo.CTBLongo = Math.Abs(_valorCTB);

                        decimal _valorCPG = new EndividamentoDAO().ValorContasPagar(empresa, _tipo.CodigoFornecedor, _tipo.Modalidade, _dataFim, "C");
                        _tipo.CPGCurto = _valorCPG;

                        _valorCPG = new EndividamentoDAO().ValorContasPagar(empresa, _tipo.CodigoFornecedor, _tipo.Modalidade, _dataFim, "L");
                        _tipo.CPGLongo = _valorCPG;

                        _tipo.VariacaoCurto = _tipo.CPGCurto - _tipo.CTBCurto;
                        _tipo.VariacaoLongo = _tipo.CPGLongo - _tipo.CTBLongo;
                        _val = Consultar(empresa.IdEmpresa, _tipo.CodigoFornecedor, _tipo.Modalidade, _tipo.Tipo, _referenciaAux.ToString(), referencia, true);
                        _tipo.CPGJuros = _val.Juros;
                        _tipo.CPGPrevisto = _val.Previsto;

                        _valorCPG = new EndividamentoDAO().ConsultarJuros(empresa.IdEmpresa, _tipo.CodigoFornecedor, _tipo.Modalidade, _tipo.Tipo, _dataFim, true); 
                        _tipo.CPGJurosCurto = _valorCPG;

                        _valorCPG = new EndividamentoDAO().ConsultarJuros(empresa.IdEmpresa, _tipo.CodigoFornecedor, _tipo.Modalidade, _tipo.Tipo, _dataFim, false);
                        _tipo.CPGJurosLongo = _valorCPG; 
                        
                        _valorCTB = new EndividamentoDAO().SaldoContabil(empresa.CodigoEmpresaGlobus, referencia.ToString(), IdEmpresa, _tipo.CodigoFornecedor, _tipo.Modalidade, "J", "C", consolidar);
                        _tipo.CTBJurosCurto = Math.Abs(_valorCTB);

                        _valorCTB = new EndividamentoDAO().SaldoContabil(empresa.CodigoEmpresaGlobus, _referenciaAux.ToString(), IdEmpresa, _tipo.CodigoFornecedor, _tipo.Modalidade, "J", "L", consolidar);
                        _tipo.CTBJurosLongo = Math.Abs(_valorCTB);

                        _tipo.VariacaoJurosCurto = _tipo.CPGJurosCurto - _tipo.CTBJurosCurto;
                        _tipo.VariacaoJurosLongo = _tipo.CPGJurosLongo - _tipo.CTBJurosLongo;

                        _tipo.TemContaJuros = _listaParametros.Where(w => w.CodigoContaJurosDebito != 0 && 
                                                                          w.CodigoContaJurosCredito != 0 &&
                                                                          w.CodigoFornecedor == _tipo.CodigoFornecedor && 
                                                                          w.Modalidade == _tipo.Modalidade).Count() != 0;

                        _tipo.TemContaVariacao = _listaParametros.Where(w => w.CodigoContaVariacaoCredito != 0 &&
                                                                          w.CodigoContaVariacaoDebito != 0 &&
                                                                          w.CodigoFornecedor == _tipo.CodigoFornecedor &&
                                                                          w.Modalidade == _tipo.Modalidade).Count() != 0;

                        _tipo.TemContaJurosPrazo = _listaParametros.Where(w => w.CodigoContaCurtoPrazo != 0 &&
                                                                          w.CodigoContaLongoPrazo != 0 &&
                                                                          w.CodigoFornecedor == _tipo.CodigoFornecedor &&
                                                                          w.Modalidade == _tipo.Modalidade).Count() != 0;

                        _tipo.TemContaPrevistoPrazo = _listaParametros.Where(w => w.CodigoContaCurtoPrevisto != 0 &&
                                                                          w.CodigoContaLongoPrevisto != 0 &&
                                                                          w.CodigoFornecedor == _tipo.CodigoFornecedor &&
                                                                          w.Modalidade == _tipo.Modalidade).Count() != 0;
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

        public decimal ConsultarValor(int IdEmpresa, string tipo, string referencia, string modalidade, decimal fornecedor, string campoRetorno)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<Endividamento.Valores> _lista = new List<Endividamento.Valores>();
            Int32 _referenciaAux = Convert.ToInt32(referencia) + 100;
            decimal _valor = 0;

            try
            {
                query.Append("Select e.id, e.idempresa, e.referencia, e.codigoforn, e.modalidade, e.tipo, e.encerrado, f.Nfantasiaforn, f.nrforn");
                query.Append("     , Sum(v.previsto) previsto, sum(v.realizado) realizado, sum(v.juros) juros");
                query.Append("  from Niff_CTB_Endividamento e, bgm_fornecedor f, niff_ctb_valoresendividamento v, Cpgdocto d");
                query.Append(" Where e.IdEmpresa = " + IdEmpresa);
                query.Append("   And e.Tipo = '" + tipo + "'");
                query.Append("   And e.Referencia = " + referencia);
                query.Append("   And e.Modalidade = '" + modalidade + "'");
                query.Append("   And f.codigoforn = " + fornecedor);
                query.Append("   And f.codigoforn = e.codigoforn");
                query.Append("   And v.Idendividamento = e.Id");
                query.Append("   And v.ExcluidoNoCPG = 'N'");
                query.Append("   And (d.pagamentocpg Is Null Or to_number(to_char(d.pagamentocpg, 'yyyymm')) >= e.referencia)");
                query.Append("   And v.coddoctocpg = d.coddoctocpg(+)");

                query.Append(" Group By e.id, e.idempresa, e.referencia, e.codigoforn, e.modalidade, e.tipo, e.encerrado, f.Nfantasiaforn, f.nrforn");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReaderAux = executar.ExecuteQuery();

                using (dataReaderAux)
                {
                    if (dataReaderAux.Read())
                    {
                        try
                        {
                            if (campoRetorno == "R")
                                _valor = Convert.ToDecimal(dataReaderAux["Realizado"].ToString());
                            else
                            {
                                if (campoRetorno == "P")
                                    _valor = Convert.ToDecimal(dataReaderAux["Previsto"].ToString());
                                else
                                    _valor = Convert.ToDecimal(dataReaderAux["Juros"].ToString());
                            }
                        }
                        catch
                        {
                            _valor = 0;
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
            return _valor;
        }

        public List<Endividamento.Valores> ListarValores(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Endividamento.Valores> lista = new List<Endividamento.Valores>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select v.Previsto, v.PrevistoCPG, v.Realizado, v.Juros, v.Contrato, v.Id");
                query.Append("     , d.codtpdoc, decode(d.nrodoctocpg, null, null, d.nrodoctocpg || ' parcela ' || d.nroparcelacpg || ' série ' || d.seriedoctocpg) Documento");
                query.Append("     , d.vencimentoCpg, d.pagamentocpg, v.coddoctocpg, d.statusdoctocpg");
                query.Append("  from cpgdocto d, Niff_CTB_ValoresEndividamento v");
                query.Append(" Where v.coddoctocpg = d.coddoctocpg(+)");

                query.Append("   And v.IdEndividamento = " + id);
                query.Append("   And v.ExcluidoNoCPG = 'N'");

                Query executar = sessao.CreateQuery(query.ToString());

                 dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Endividamento.Valores _tipo = new Endividamento.Valores();
                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEndividamento = id;
                        _tipo.CodigoTipoDocumento = dataReader["CodTpDoc"].ToString();
                        _tipo.Documento = dataReader["Documento"].ToString();
                        _tipo.Contrato = dataReader["Contrato"].ToString();
                        _tipo.Cancelada = dataReader["StatusDoctoCPG"].ToString() == "C";

                        try
                        {
                            _tipo.CodigoInternoCPG = Convert.ToDecimal(dataReader["coddoctocpg"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PrevistoCPG = Convert.ToDecimal(dataReader["PrevistoCPG"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Previsto = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Juros = Convert.ToDecimal(dataReader["Juros"].ToString());
                        }
                        catch { }

                        _tipo.Variacao = _tipo.Realizado - _tipo.Previsto;

                        try
                        {
                            _tipo.Vencimento = Convert.ToDateTime(dataReader["vencimentoCpg"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Pagamento = Convert.ToDateTime(dataReader["pagamentocpg"].ToString());
                            _tipo.Pagamentos = _tipo.Pagamento.ToShortDateString();
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

        public List<Endividamento.Valores> ListarValoresFuturosCancelados(int idEmpresa, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Endividamento.Valores> lista = new List<Endividamento.Valores>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select v.Previsto, v.PrevistoCPG, v.Realizado, v.Juros, v.Contrato, v.Id");
                query.Append("     , d.codtpdoc, decode(d.nrodoctocpg, null, null, d.nrodoctocpg || ' parcela ' || d.nroparcelacpg || ' série ' || d.seriedoctocpg) Documento");
                query.Append("     , d.vencimentoCpg, d.pagamentocpg, v.coddoctocpg, d.statusdoctocpg, v.idendividamento");
                query.Append("     , f.nrforn || ' - ' || f.rsocialforn Fornecedor, e.modalidade, e.tipo");
                query.Append("  from cpgdocto d, Niff_CTB_ValoresEndividamento v, Niff_Ctb_Endividamento e, Bgm_Fornecedor f");
                query.Append(" Where v.coddoctocpg = d.coddoctocpg(+)");

                query.Append("   And v.idendividamento = e.Id");
                query.Append("   And e.Idempresa = " + idEmpresa);
                query.Append("   And e.referencia > " + referencia);
                query.Append("   And d.statusdoctocpg = 'C'");
                query.Append("   And f.codigoforn = d.codigoforn");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Endividamento.Valores _tipo = new Endividamento.Valores();
                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEndividamento = Convert.ToInt32(dataReader["idendividamento"].ToString());
                        _tipo.CodigoTipoDocumento = dataReader["CodTpDoc"].ToString();
                        _tipo.Documento = dataReader["Documento"].ToString();
                        _tipo.Contrato = dataReader["Contrato"].ToString();
                        _tipo.Cancelada = dataReader["StatusDoctoCPG"].ToString() == "C";
                        _tipo.Modalidade = dataReader["Modalidade"].ToString();
                        _tipo.Tipo = dataReader["Tipo"].ToString();
                        _tipo.Fornecedor = dataReader["Fornecedor"].ToString();

                        try
                        {
                            _tipo.CodigoInternoCPG = Convert.ToDecimal(dataReader["coddoctocpg"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PrevistoCPG = Convert.ToDecimal(dataReader["PrevistoCPG"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Previsto = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Juros = Convert.ToDecimal(dataReader["Juros"].ToString());
                        }
                        catch { }

                        _tipo.Variacao = _tipo.Realizado - _tipo.Previsto;

                        try
                        {
                            _tipo.Vencimento = Convert.ToDateTime(dataReader["vencimentoCpg"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Pagamento = Convert.ToDateTime(dataReader["pagamentocpg"].ToString());
                            _tipo.Pagamentos = _tipo.Pagamento.ToShortDateString();
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

        public List<Endividamento.Valores> ListarValoresDaEmpresa(int idEmpresa, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Endividamento.Valores> lista = new List<Endividamento.Valores>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select v.Previsto, v.PrevistoCPG, v.Realizado, v.Juros, v.Contrato, v.Id, v.Idendividamento");
                query.Append("     , v.coddoctocpg, e.modalidade, f.nfantasiaforn, e.tipo, e.referencia,  e.encerrado");
                query.Append("  from Niff_CTB_ValoresEndividamento v, Niff_Ctb_Endividamento e, Bgm_Fornecedor f");
                query.Append(" Where e.Id = v.idendividamento");
                query.Append("   And e.codigoforn = f.codigoforn");
                query.Append("   And e.Idempresa = " + idEmpresa);
                query.Append("   And e.referencia = " + referencia);
                query.Append("   And v.ExcluidoNoCPG = 'N'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Endividamento.Valores _tipo = new Endividamento.Valores();
                        Endividamento.Valores _cpg = new Endividamento.Valores();
                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEndividamento = Convert.ToInt32(dataReader["Idendividamento"].ToString());
                        _tipo.Contrato = dataReader["Contrato"].ToString();
                        _tipo.Fornecedor = dataReader["nfantasiaforn"].ToString();
                        _tipo.Tipo = dataReader["tipo"].ToString();
                        _tipo.Modalidade = dataReader["Modalidade"].ToString();
                        _tipo.Encerrado = dataReader["Encerrado"].ToString() == "S";

                        try
                        {
                            _tipo.PrevistoCPG = Convert.ToDecimal(dataReader["PrevistoCPG"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Previsto = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Juros = Convert.ToDecimal(dataReader["Juros"].ToString());
                        }
                        catch { }

                        _tipo.Variacao = _tipo.Realizado - _tipo.Previsto;

                        try
                        {
                            _cpg = BuscarDocumentoContasPagar(Convert.ToDecimal(dataReader["coddoctocpg"].ToString()));

                            _tipo.Documento = _cpg.Documento;
                            _tipo.CodigoTipoDocumento = _cpg.CodigoTipoDocumento;
                            _tipo.Vencimento = _cpg.Vencimento;
                            _tipo.Pagamento = _cpg.Pagamento;
                            _tipo.Pagamentos = _cpg.Pagamentos;
                        }
                        catch { };

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

        public bool Gravar(Endividamento.Valores _valor, List<Endividamento.Valores> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = _lista.Count() == 0;
            Query executar;
            int id = 1;

            try
            {
                query.Clear();
                query.Append("Select Nvl(Max(Id),0) + 1 next From Niff_CTB_Endividamento");
                executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        id = Convert.ToInt32(dataReader["next"].ToString());
                }

                query.Clear();
                
                if (!_valor.Existe)
                {
                    query.Append("Insert into Niff_CTB_Endividamento");
                    query.Append(" ( id, idempresa, referencia, codigoforn, modalidade, tipo, encerrado, previsto, realizado, juros)");
                    query.Append(" Values ( " + id);
                    query.Append("        , " + _valor.IdEmpresa);
                    query.Append("        , " + _valor.Referencia);
                    query.Append("        , " + _valor.CodigoFornecedor);
                    query.Append("        , '" + _valor.Modalidade + "'");
                    query.Append("        , '" + _valor.Tipo + "'");
                    query.Append("        , '" + (_valor.Encerrado ? "S" : "N") + "'");
                    query.Append("        , " + _valor.Previsto.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + _valor.Realizado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + _valor.Juros.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_CTB_Endividamento");
                    query.Append("   Set encerrado = '" + (_valor.Encerrado ? "S" : "N") + "'");
                    query.Append("     , previsto = " + _valor.Previsto.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , realizado = " + _valor.Realizado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , juros = " + _valor.Juros.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" Where Id = " + _valor.Id);
                    id = _valor.Id;
                }

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;
                else
                {
                    query.Clear();

                    Int32 _referenciaAux = Convert.ToInt32(_valor.Referencia) + 100;

                    query.Append("Update Niff_CTB_ValoresEndividamento");
                    query.Append("   set utilizadonareferencia = " + _valor.Referencia);
                    query.Append(" Where utilizadonareferencia is null ");
                    query.Append("   And Idendividamento in ( select Id from Niff_Ctb_Endividamento e");
                    query.Append("                               Where e.Idempresa = " + _valor.IdEmpresa);
                    query.Append("                                 And e.Codigoforn = " + _valor.CodigoFornecedor);
                    query.Append("                                 And e.Modalidade = '" + _valor.Modalidade + "'");
                    query.Append("                                 And e.Tipo = '" + _valor.Tipo + "'");
                    query.Append("                                 And e.Referencia = " + _referenciaAux + " ) ");

                    if (!sessao.ExecuteSqlTransaction(query.ToString()))
                        return false;
                    
                    foreach (var item in _lista.Where(w => !w.Excluir))
                    {
                        if (item.RealizadoAlterado && item.RealizadoAtual != 0)
                            item.Realizado = item.RealizadoAtual;

                        query.Clear();
                        if (!item.Existe)
                        {
                            query.Append("Insert into Niff_CTB_ValoresEndividamento");
                            query.Append(" ( id, idendividamento, coddoctocpg, previstocpg, previsto, realizado, juros, contrato)");
                            query.Append(" Values ( (Select nvl(Max(id),0) +1 from Niff_CTB_ValoresEndividamento) ");
                            query.Append("        , " + id);

                            if (item.CodigoInternoCPG == 0)
                                query.Append("        , null");
                            else
                                query.Append("        , " + item.CodigoInternoCPG);

                            query.Append("     , " + item.PrevistoCPG.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , " + item.Previsto.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , " + item.Realizado.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , " + item.Juros.ToString().Replace(".", "").Replace(",", "."));

                            query.Append("     , '" + item.Contrato + "'");
                            query.Append(" )");
                        }
                        else
                        {
                            query.Append("Update Niff_CTB_ValoresEndividamento");
                            query.Append("   set previsto = " + item.Previsto.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , realizado = " + item.Realizado.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , juros = " + item.Juros.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , PrevistoCPG = " + item.PrevistoCPG.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , Contrato = '" + item.Contrato + "'");
                            query.Append(" Where Id = " + item.Id);
                        }
                        retorno = sessao.ExecuteSqlTransaction(query.ToString());

                        if (!retorno)
                            break;
                    }

                    foreach (var item in _lista.Where(w => w.Excluir))
                    {
                        query.Clear();
                        // query.Append("Delete Niff_CTB_ValoresEndividamento");
                        // não irá mais excluir. irá desativar
                        query.Append("Update Niff_CTB_ValoresEndividamento");
                        query.Append("   set ExcluidoNoCPG = 'S'");
                        query.Append("     , ExcluidoNoCPGReferecia = " + _valor.Referencia);
                        query.Append("     , UtilizadoNaReferencia = (Select referencia-100");
                        query.Append("                                  From Niff_CTB_Endividamento ");
                        query.Append("                                 Where Id = (Select v.idendividamento");
                        query.Append("                                               From niff_ctb_valoresendividamento v");
                        query.Append("                                              Where Id = " + item.Id + "))");
                        query.Append(" Where Id = " + item.Id);
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

        public bool Encerra_Cancela(int idempresa, string referencia, string encerra)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Update Niff_CTB_Endividamento");
                query.Append("   set encerrado = '" + encerra + "'");
                query.Append(" Where IdEmpresa = " + idempresa);
                query.Append("   and referencia = " + referencia);

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
                query.Append("Delete Niff_CTB_ValoresEndividamento");
                query.Append(" Where idendividamento = " + id);

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;
                else
                {
                    query.Clear();
                    query.Append("Delete Niff_CTB_Endividamento");
                    query.Append(" Where Id = " + id);

                    if (!sessao.ExecuteSqlTransaction(query.ToString()))
                        return false;
                    else
                        return true;
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

        private StringBuilder MontaConsultaConta(string Empresa, string referencia, int idEmpresa, decimal codigoFornecedor, string modalidade, string tipoConta, string TipoConta2, bool consolidar)
        {
            StringBuilder query = new StringBuilder();

            query.Append("        Select Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) saldoAcumulado");
            query.Append("          from Ctbsaldo s, ctbconta c");

            query.Append("             , (Select  Distinct mc.modalidade, mc.CodigoForn, mc.IdEmpresa, mc.NumeroPlano");

            if (TipoConta2 == "J")
                query.Append("             , Codigocontacurtoprazo, Codigocontalongoprazo");
            else
                query.Append("             , Codigocontacurtoprevisto, Codigocontalongoprevisto");

            query.Append("                 from Niff_Ctb_Parametrosendividamento mc ) MC");
            query.Append("         Where s.nroplano = mc.Numeroplano");

            if (tipoConta == "C")
                query.Append("           And s.periodosaldo between " + referencia.Substring(0, 4) + "01 and " + referencia);
            else
                query.Append("           And s.periodosaldo <= " + referencia);

            query.Append("           And s.nroplano = c.nroplano");
            query.Append("           And s.codcontactb = c.codcontactb");

            if (TipoConta2 == "J")
            {
                if (tipoConta == "C")
                    query.Append("           And c.codcontactb = mc.Codigocontacurtoprazo");
                else
                    query.Append("           And c.codcontactb = mc.Codigocontalongoprazo");
            }
            else
            {
                if (tipoConta == "C")
                    query.Append("           And c.codcontactb = mc.Codigocontacurtoprevisto");
                else
                    query.Append("           And c.codcontactb = mc.Codigocontalongoprevisto");
            }
            query.Append("           And c.nroplano = mc.Numeroplano");
            query.Append("           And mc.Idempresa = " + idEmpresa);
            query.Append("           And mc.CodigoForn = " + codigoFornecedor);
            query.Append("           And mc.Modalidade = '" + modalidade + "'");

            #region empresas
            if (consolidar)
            {
                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 ");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 ");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 ");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 ");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 ");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 ");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 ");

                if (Empresa == "007/001")
                    query.Append("           And s.codigoEmpresa = 7 ");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 ");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13");

                if (Empresa == "015/001")
                    query.Append("           And s.codigoEmpresa = 15");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 ");
            }
            else
            {
                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "007/001")
                    query.Append("           And s.codigoEmpresa = 7 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "015/001")
                    query.Append("           And s.codigoEmpresa = 15 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");
            }
            #endregion

            return query;
        }

        private StringBuilder MontaConsultaContaSaldoInicial(string Empresa, string referencia, int idEmpresa, decimal codigoFornecedor, string modalidade, string tipoConta, string TipoConta2, bool consolidar)
        {
            StringBuilder query = new StringBuilder();

            query.Append("        Select Sum(s.VLDEBANTSALDO) -  Sum(s.VLCREDANTSALDO) saldoAcumulado");
            query.Append("          from Ctbsaldo s, ctbconta c");

            query.Append("             , (Select  Distinct mc.modalidade, mc.CodigoForn, mc.IdEmpresa, mc.NumeroPlano");

            if (TipoConta2 == "J")
                query.Append("             , Codigocontacurtoprazo, Codigocontalongoprazo");
            else
                query.Append("             , Codigocontacurtoprevisto, Codigocontalongoprevisto");

            query.Append("                 from Niff_Ctb_Parametrosendividamento mc ) MC");
            query.Append("         Where s.nroplano = mc.Numeroplano");

            query.Append("           And s.periodosaldo = " + referencia.Substring(0,4) + "01");

            query.Append("           And s.nroplano = c.nroplano");
            query.Append("           And s.codcontactb = c.codcontactb");

            if (TipoConta2 == "J")
            {
                if (tipoConta == "C")
                    query.Append("           And c.codcontactb = mc.Codigocontacurtoprazo");
                else
                    query.Append("           And c.codcontactb = mc.Codigocontalongoprazo");
            }
            else
            {
                if (tipoConta == "C")
                    query.Append("           And c.codcontactb = mc.Codigocontacurtoprevisto");
                else
                    query.Append("           And c.codcontactb = mc.Codigocontalongoprevisto");
            }
            query.Append("           And c.nroplano = mc.Numeroplano");
            query.Append("           And mc.Idempresa = " + idEmpresa);
            query.Append("           And mc.CodigoForn = " + codigoFornecedor);
            query.Append("           And mc.Modalidade = '" + modalidade + "'");

            #region empresas
            if (consolidar)
            {
                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 ");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 ");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 ");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 ");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 ");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 ");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 ");

                if (Empresa == "007/001")
                    query.Append("           And s.codigoEmpresa = 7 ");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 ");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13");

                if (Empresa == "015/001")
                    query.Append("           And s.codigoEmpresa = 15");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 ");
            }
            else
            {
                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "007/001")
                    query.Append("           And s.codigoEmpresa = 7 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "015/001")
                    query.Append("           And s.codigoEmpresa = 15 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");
            }
            #endregion

            return query;
        }

        public decimal SaldoContabil(string Empresa, string referencia, int idEmpresa, decimal codigoFornecedor, string modalidade, string tipo, string Total, bool consolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;


            try
            {
                query.Append("Select Round(Sum(nvl(SaldoAcumulado,0)),2) Valor");
                query.Append("  from (");

                if (Total == "C" || Total == "T")
                {
                    query.Append(MontaConsultaConta(Empresa, referencia, idEmpresa, codigoFornecedor, modalidade, "C", tipo, consolidar));
                    query.Append(" Union all ");
                    query.Append(MontaConsultaContaSaldoInicial(Empresa, referencia, idEmpresa, codigoFornecedor, modalidade, "C", tipo, consolidar));
                }

                if (Total == "T")
                    query.Append(" Union all ");

                if (Total == "L" || Total == "T")
                    query.Append(MontaConsultaConta(Empresa, referencia, idEmpresa, codigoFornecedor, modalidade, "P", tipo, consolidar));
                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReaderAux = executar.ExecuteQuery();

                using (dataReaderAux)
                {
                    if (dataReaderAux.Read())
                        valor = Convert.ToDecimal(dataReaderAux["Valor"].ToString());
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

        public decimal SaldoContabil(string Empresa, string referencia, decimal contaCTB, bool consolidar = false)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;


            try
            {
                query.Append("Select Round(Sum(nvl(SaldoAcumulado,0)),2) Valor");
                query.Append("  from (");

                query.Append("        Select Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c");
                query.Append("         Where s.periodosaldo <= " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");

                query.Append("           And c.codcontactb = " + contaCTB);

                #region empresas
                if (consolidar)
                {
                    if (Empresa == "001/001")
                        query.Append("           And s.codigoEmpresa = 1");

                    if (Empresa == "001/002")
                        query.Append("           And s.codigoEmpresa = 1");

                    if (Empresa == "002/001")
                        query.Append("           And s.codigoEmpresa = 2");

                    if (Empresa == "003/001")
                        query.Append("           And s.codigoEmpresa = 3");

                    if (Empresa == "004/001")
                        query.Append("           And s.codigoEmpresa = 4");

                    if (Empresa == "005/001")
                        query.Append("           And s.codigoEmpresa = 5");

                    if (Empresa == "006/001")
                        query.Append("           And s.codigoEmpresa = 6");

                    if (Empresa == "007/001")
                        query.Append("           And s.codigoEmpresa = 7");

                    if (Empresa == "009/001")
                        query.Append("           And s.codigoEmpresa = 9");

                    if (Empresa == "013/001")
                        query.Append("           And s.codigoEmpresa = 13");

                    if (Empresa == "015/001")
                        query.Append("           And s.codigoEmpresa = 15");

                    if (Empresa == "026/001")
                        query.Append("           And s.codigoEmpresa = 26");

                    if (Empresa == "026/003")
                        query.Append("           And s.codigoEmpresa = 26");

                    if (Empresa == "029/001")
                        query.Append("           And s.codigoEmpresa = 29");
                }
                else
                {
                    if (Empresa == "001/001")
                        query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                    if (Empresa == "001/002")
                        query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                    if (Empresa == "002/001")
                        query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                    if (Empresa == "003/001")
                        query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                    if (Empresa == "004/001")
                        query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                    if (Empresa == "005/001")
                        query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                    if (Empresa == "006/001")
                        query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                    if (Empresa == "007/001")
                        query.Append("           And s.codigoEmpresa = 7 and s.CodigoFl = 1");

                    if (Empresa == "009/001")
                        query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                    if (Empresa == "013/001")
                        query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                    if (Empresa == "015/001")
                        query.Append("           And s.codigoEmpresa = 15 and s.CodigoFl = 1");

                    if (Empresa == "026/001")
                        query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                    if (Empresa == "026/003")
                        query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                    if (Empresa == "029/001")
                        query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");
                }
                #endregion
                query.Append(" )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReaderAux = executar.ExecuteQuery();

                using (dataReaderAux)
                {
                    if (dataReaderAux.Read())
                        valor = Convert.ToDecimal(dataReaderAux["Valor"].ToString());
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

        public List<Endividamento.Conciliado> ListarConciliacao(int idEmpresa, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Endividamento.Conciliado> lista = new List<Endividamento.Conciliado>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, referencia, c.codigoforn, modalidade, tipo");
                query.Append("     , realizado, previsto, juros, previstocurtocpg, previstolongocpg");
                query.Append("     , previstocurtoctb, previstolongoctb, juroscurtocpg, juroslongocpg");
                query.Append("     , juroscurtoctb, juroslongoctb, previstoconciliado, jurosconciliado ");
                query.Append("     , f.nfantasiaforn, f.NrForn");
                query.Append("  from niff_ctb_EndividamentoConciliado c,  Bgm_Fornecedor f ");
                query.Append(" Where Idempresa = " + idEmpresa);
                query.Append("   And referencia = " + referencia);
                query.Append("   And f.codigoforn = c.codigoforn");
                query.Append("   And c.Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Endividamento.Conciliado _tipo = new Endividamento.Conciliado();
                        
                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.CodigoFornecedor = Convert.ToDecimal(dataReader["codigoforn"].ToString());
                        _tipo.Tipo = dataReader["tipo"].ToString();
                        _tipo.Modalidade = dataReader["Modalidade"].ToString();
                        _tipo.Referencia = dataReader["Referencia"].ToString();
                        _tipo.Fornecedor = dataReader["NrForn"].ToString() + " - " + dataReader["NFantasiaForn"].ToString();

                        try
                        {
                            _tipo.Previsto = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Juros = Convert.ToDecimal(dataReader["Juros"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PrevistoConciliado = Convert.ToDecimal(dataReader["previstoconciliado"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.JurosConciliado = Convert.ToDecimal(dataReader["jurosconciliado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PrevistoCPGCurto = Convert.ToDecimal(dataReader["previstocurtocpg"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.PrevistoCPGLongo = Convert.ToDecimal(dataReader["previstolongocpg"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.PrevistoCTBCurto = Convert.ToDecimal(dataReader["previstocurtoctb"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.PrevistoCTBLongo = Convert.ToDecimal(dataReader["previstolongoctb"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.JurosCPGCurto = Convert.ToDecimal(dataReader["Juroscurtocpg"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.JurosCPGLongo = Convert.ToDecimal(dataReader["Juroslongocpg"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.JurosCTBCurto = Convert.ToDecimal(dataReader["Juroscurtoctb"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.JurosCTBLongo = Convert.ToDecimal(dataReader["Juroslongoctb"].ToString());
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

        public List<Endividamento.Valores> ListarValorConciliado(int idEmpresa, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Endividamento.Valores> lista = new List<Endividamento.Valores>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, referencia, c.codigoforn, modalidade, tipo");
                query.Append("     , realizado, previsto, juros, previstocurtocpg, previstolongocpg");
                query.Append("     , previstocurtoctb, previstolongoctb, juroscurtocpg, juroslongocpg");
                query.Append("     , juroscurtoctb, juroslongoctb, previstoconciliado, jurosconciliado ");
                query.Append("     , f.nfantasiaforn, f.NrForn");
                query.Append("  from niff_ctb_EndividamentoConciliado c,  Bgm_Fornecedor f ");
                query.Append(" Where Idempresa = " + idEmpresa);
                query.Append("   And referencia = " + referencia);
                query.Append("   And f.codigoforn = c.codigoforn");
                query.Append("   And c.Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Endividamento.Valores _tipo = new Endividamento.Valores();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.CodigoFornecedor = Convert.ToDecimal(dataReader["codigoforn"].ToString());
                        _tipo.Tipo = dataReader["tipo"].ToString();
                        _tipo.Modalidade = dataReader["Modalidade"].ToString();
                        _tipo.Referencia = dataReader["Referencia"].ToString();
                        _tipo.Fornecedor = dataReader["NrForn"].ToString() + " - " + dataReader["NFantasiaForn"].ToString();

                        try
                        {
                            _tipo.Previsto = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        }
                        catch { }

                        _tipo.Variacao = _tipo.Realizado - _tipo.Previsto;

                        try
                        {
                            _tipo.Juros = Convert.ToDecimal(dataReader["Juros"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.CPGPrevisto = Convert.ToDecimal(dataReader["previstoconciliado"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CPGJuros = Convert.ToDecimal(dataReader["jurosconciliado"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CPGCurto = Convert.ToDecimal(dataReader["previstocurtocpg"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CPGLongo = Convert.ToDecimal(dataReader["previstolongocpg"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CTBCurto = Convert.ToDecimal(dataReader["previstocurtoctb"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CTBLongo = Convert.ToDecimal(dataReader["previstolongoctb"].ToString());
                        }
                        catch { }

                        _tipo.VariacaoCurto = _tipo.CPGCurto - _tipo.CTBCurto;
                        _tipo.VariacaoLongo = _tipo.CPGLongo - _tipo.CTBLongo;

                        try
                        {
                            _tipo.CPGJurosCurto = Convert.ToDecimal(dataReader["Juroscurtocpg"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CPGJurosLongo = Convert.ToDecimal(dataReader["Juroslongocpg"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CTBJurosCurto = Convert.ToDecimal(dataReader["Juroscurtoctb"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CTBJurosLongo = Convert.ToDecimal(dataReader["Juroslongoctb"].ToString());
                        }
                        catch { }

                        _tipo.VariacaoJurosCurto = _tipo.CPGJurosCurto - _tipo.CTBJurosCurto;
                        _tipo.VariacaoJurosLongo = _tipo.CPGJurosLongo - _tipo.CTBJurosLongo;
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

        public bool GravarConciliacao(List<Endividamento.Conciliado> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            Query executar;
            int id = 1;

            try
            {
                

                foreach (var _valor in _lista)
                {

                    if (!_valor.Existe)
                    {
                        query.Clear();
                        query.Append("Select Nvl(Max(Id),0) + 1 next From niff_ctb_EndividamentoConciliado");
                        executar = sessao.CreateQuery(query.ToString());

                        dataReader = executar.ExecuteQuery();

                        using (dataReader)
                        {
                            if (dataReader.Read())
                                id = Convert.ToInt32(dataReader["next"].ToString());
                        }

                        query.Clear();
                        query.Append("Insert into niff_ctb_EndividamentoConciliado");
                        query.Append(" ( id, idempresa, referencia, codigoforn, modalidade, tipo");
                        query.Append(" , realizado, previsto, juros, previstocurtocpg, previstolongocpg");
                        query.Append(" , previstocurtoctb, previstolongoctb, juroscurtocpg, juroslongocpg");
                        query.Append(" , juroscurtoctb, juroslongoctb, previstoconciliado, jurosconciliado) ");
                        query.Append(" Values ( " + id);
                        query.Append("        , " + _valor.IdEmpresa);
                        query.Append("        , " + _valor.Referencia);
                        query.Append("        , " + _valor.CodigoFornecedor);
                        query.Append("        , '" + _valor.Modalidade + "'");
                        query.Append("        , '" + _valor.Tipo + "'");
                        query.Append("        , " + _valor.Realizado.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + _valor.Previsto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + _valor.Juros.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + _valor.PrevistoCPGCurto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + _valor.PrevistoCPGLongo.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + _valor.PrevistoCTBCurto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + _valor.PrevistoCTBLongo.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + _valor.JurosCPGCurto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + _valor.JurosCPGLongo.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + _valor.JurosCTBCurto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + _valor.JurosCTBLongo.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + _valor.PrevistoConciliado.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + _valor.JurosConciliado.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(" )");
                    }
                    else
                    {
                        query.Clear();
                        query.Append("Update niff_ctb_EndividamentoConciliado");
                        query.Append("   set Ativo = 'N'"); // quando cancela o encerramento
                        query.Append("     , DataCancelamento = sysdate");

                        /*query.Append("   Set previsto = " + _valor.Previsto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , realizado = " + _valor.Realizado.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , juros = " + _valor.Juros.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , previstocurtocpg = " + _valor.PrevistoCPGCurto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , previstolongocpg = " + _valor.PrevistoCPGLongo.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , previstocurtoctb = " + _valor.PrevistoCTBCurto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , previstolongoctb = " + _valor.PrevistoCTBLongo.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , juroscurtocpg = " + _valor.JurosCPGCurto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , juroslongocpg = " + _valor.JurosCPGLongo.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , juroscurtoctb = " + _valor.JurosCTBCurto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , juroslongoctb = " + _valor.JurosCTBLongo.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , previstoconciliado = " + _valor.PrevistoConciliado.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , jurosconciliado = " + _valor.JurosConciliado.ToString().Replace(".", "").Replace(",", "."));*/

                        query.Append(" Where Id = " + _valor.Id);
                        id = _valor.Id;
                    }

                    if (!sessao.ExecuteSqlTransaction(query.ToString()))
                        return false;
                
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

        #endregion

        #region Selic
        public List<Endividamento.Selic> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Endividamento.Selic> lista = new List<Endividamento.Selic>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Id, Referencia, Valor, UFG, SubStr(Referencia,5,2) || '/' || Substr(Referencia, 1,4) MesAno");
                query.Append("     , Substr(Referencia, 1, 4) Ano");
                query.Append("  from NIFF_CTB_Selic ");
                query.Append(" order by Referencia Desc");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Endividamento.Selic _tipo = new Endividamento.Selic();
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.Referencia = Convert.ToInt32(dataReader["Referencia"].ToString());
                        _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.ValorUFG = Convert.ToDecimal(dataReader["UFG"].ToString());
                        _tipo.MesAno = dataReader["MesAno"].ToString();
                        _tipo.Ano = Convert.ToInt32(dataReader["Ano"].ToString());

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

        public bool Gravar(List<Endividamento.Selic> _lista)
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
                        query.Append("Insert into NIFF_CTB_Selic");
                        query.Append(" ( id, Referencia, Valor, UFG");
                        query.Append(") Values ( (Select nvl(Max(id),0) +1 from NIFF_CTB_Selic) ");
                        query.Append("        , " + item.Referencia);
                        query.Append("        , " + item.Valor.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + item.ValorUFG.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(" )");
                    }
                    else
                    {
                        query.Append("Update NIFF_CTB_Selic");
                        query.Append("   set Valor = " + item.Valor.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , UFG = " + item.ValorUFG.ToString().Replace(".", "").Replace(",", "."));
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

        public bool ExcluirSelic(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete NIFF_CTB_Selic");
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

        #endregion

        #region Parcelamento
        public List<Endividamento.Contrato> Listar(int idEmpresa, decimal codigoFornecedor, string modalidade)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Endividamento.Contrato> lista = new List<Endividamento.Contrato>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.id, c.idempresa, c.codigofornecedor, c.contrato, c.pedido, c.valor");
                query.Append("     , c.juros, c.multa, c.total, c.qtdoparcelas, c.vencimento, c.dia, c.percjuros, c.AplicaSelic");
                query.Append("     , c.Modalidade, f.nfantasiaforn, f.NrForn");
                query.Append("     , c.PercJurosDif, c.PercMultaDif, c.PercSelicDif, c.ParcelaMinima, c.AplicaJurosDif");
                query.Append("     , c.ParcelaDiferenciada, c.PercParcelaAVista, c.ValorAdesao, c.QtdeParcelaAdesao");
                query.Append("     , c.ZerarParcelas, c.AplicarUFG, c.ZerarDaParcela, c.Honorarios");
                query.Append("     , c.Correcao, c.Reducao, c.Custas");
                query.Append("  from Niff_Ctb_Contrato c, Bgm_Fornecedor f ");
                query.Append(" Where c.idEmpresa = " + idEmpresa);
                query.Append("   And f.codigoforn = " + codigoFornecedor);
                query.Append("   And c.modalidade = '" + modalidade + "'");
                query.Append("   And f.codigoforn = c.codigofornecedor");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Endividamento.Contrato _tipo = new Endividamento.Contrato();
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Dia = Convert.ToInt32(dataReader["Dia"].ToString());
                        _tipo.Quantidade = Convert.ToInt32(dataReader["qtdoparcelas"].ToString());
                        _tipo.AplicarSelic = dataReader["AplicaSelic"].ToString() == "S";

                        try
                        {
                            _tipo.CodigoFornecedor = Convert.ToDecimal(dataReader["codigofornecedor"].ToString());
                        }
                        catch { }

                        _tipo.NomeFantasia = dataReader["NrForn"].ToString() + " - " + dataReader["NFantasiaForn"].ToString();
                        _tipo.NumeroContrato = dataReader["contrato"].ToString();
                        _tipo.Modalidade = dataReader["Modalidade"].ToString();
                        _tipo.Pedido = dataReader["Pedido"].ToString();

                        try
                        {
                            _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Juros = Convert.ToDecimal(dataReader["Juros"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Multa = Convert.ToDecimal(dataReader["multa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Consolidado = Convert.ToDecimal(dataReader["Total"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.PercentualJuros = Convert.ToDecimal(dataReader["PercJuros"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Vencimento = Convert.ToDateTime(dataReader["Vencimento"].ToString());
                        }
                        catch { }

                        _tipo.AplicarJurosDiferenciado = dataReader["AplicaJurosDif"].ToString() == "S";

                        try
                        {
                            _tipo.PercentualJurosDiferenciado = Convert.ToDecimal(dataReader["PercJurosDif"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PercentualMultaDiferenciada = Convert.ToDecimal(dataReader["PercMultaDif"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PercentualSelicDiferenciada = Convert.ToDecimal(dataReader["PercSelicDif"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.ParcelaMinima = Convert.ToDecimal(dataReader["ParcelaMinima"].ToString());
                        }
                        catch { }

                        _tipo.AplicarParcelasDiferenciado = dataReader["ParcelaDiferenciada"].ToString() == "S";

                        try
                        {
                            _tipo.PercentualAVista = Convert.ToDecimal(dataReader["PercParcelaAVista"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.ValorAdesao = Convert.ToDecimal(dataReader["ValorAdesao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.QtdeParcelasAdesao = Convert.ToDecimal(dataReader["QtdeParcelaAdesao"].ToString());
                        }
                        catch { }

                        _tipo.ZerarParcelas = dataReader["ZerarParcelas"].ToString() == "S";
                        _tipo.AplicarUFG = dataReader["AplicarUFG"].ToString() == "S";


                        try
                        {
                            _tipo.ZerarParcelaApartirDe = Convert.ToInt32(dataReader["ZerarDaParcela"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Honorarios = Convert.ToDecimal(dataReader["Honorarios"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Correcao = Convert.ToDecimal(dataReader["Correcao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Custas = Convert.ToDecimal(dataReader["Custas"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Reducao = Convert.ToDecimal(dataReader["Reducao"].ToString());
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

        public List<Endividamento.Contrato> Listar(int idEmpresa, bool consolidar = false)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Endividamento.Contrato> lista = new List<Endividamento.Contrato>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.id, c.idempresa, c.codigofornecedor, c.contrato, c.pedido, c.valor");
                query.Append("     , c.juros, c.multa, c.total, c.qtdoparcelas, c.vencimento, c.dia, c.percjuros, c.AplicaSelic");
                query.Append("     , c.Modalidade, f.nfantasiaforn, f.NrForn");
                query.Append("     , c.PercJurosDif, c.PercMultaDif, c.PercSelicDif, c.ParcelaMinima, c.AplicaJurosDif");
                query.Append("     , c.ParcelaDiferenciada, c.PercParcelaAVista, c.ValorAdesao, c.QtdeParcelaAdesao");
                query.Append("     , c.ZerarParcelas, c.AplicarUFG, c.ZerarDaParcela, c.Honorarios");
                query.Append("     , c.Correcao, c.Reducao, c.Custas");
                query.Append("  from Niff_Ctb_Contrato c, Bgm_Fornecedor f ");
                query.Append(" Where f.codigoforn = c.codigofornecedor");
                if (!consolidar)
                    query.Append("   And c.idEmpresa = " + idEmpresa);
                else
                {
                    if (idEmpresa == 2 || idEmpresa == 3)
                        query.Append("   And c.idEmpresa in (2,3)");
                    else
                    {
                        if (idEmpresa == 5 || idEmpresa == 18)
                            query.Append("   And c.idEmpresa in (5,18)");
                        else
                            query.Append("   And c.idEmpresa = " + idEmpresa);
                    }
                }
                

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Endividamento.Contrato _tipo = new Endividamento.Contrato();
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Dia = Convert.ToInt32(dataReader["Dia"].ToString());
                        _tipo.Quantidade = Convert.ToInt32(dataReader["qtdoparcelas"].ToString());
                        _tipo.AplicarSelic = dataReader["AplicaSelic"].ToString() == "S";

                        try
                        {
                            _tipo.CodigoFornecedor = Convert.ToDecimal(dataReader["codigofornecedor"].ToString());
                        }
                        catch { }

                        _tipo.NomeFantasia = dataReader["NrForn"].ToString() + " - " + dataReader["NFantasiaForn"].ToString();
                        _tipo.NumeroContrato = dataReader["contrato"].ToString();
                        _tipo.Modalidade = dataReader["Modalidade"].ToString();
                        _tipo.Pedido = dataReader["Pedido"].ToString();

                        try
                        {
                            _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Juros = Convert.ToDecimal(dataReader["Juros"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Multa = Convert.ToDecimal(dataReader["multa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Consolidado = Convert.ToDecimal(dataReader["Total"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PercentualJuros = Convert.ToDecimal(dataReader["PercJuros"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Vencimento = Convert.ToDateTime(dataReader["Vencimento"].ToString());
                        }
                        catch { }

                        _tipo.AplicarJurosDiferenciado = dataReader["AplicaJurosDif"].ToString() == "S";

                        try
                        {
                            _tipo.PercentualJurosDiferenciado = Convert.ToDecimal(dataReader["PercJurosDif"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PercentualMultaDiferenciada = Convert.ToDecimal(dataReader["PercMultaDif"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PercentualSelicDiferenciada = Convert.ToDecimal(dataReader["PercSelicDif"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.ParcelaMinima = Convert.ToDecimal(dataReader["ParcelaMinima"].ToString());
                        }
                        catch { }

                        _tipo.AplicarParcelasDiferenciado = dataReader["ParcelaDiferenciada"].ToString() == "S";

                        try
                        {
                            _tipo.PercentualAVista = Convert.ToDecimal(dataReader["PercParcelaAVista"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorAdesao = Convert.ToDecimal(dataReader["ValorAdesao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.QtdeParcelasAdesao = Convert.ToDecimal(dataReader["QtdeParcelaAdesao"].ToString());
                        }
                        catch { }

                        _tipo.ZerarParcelas = dataReader["ZerarParcelas"].ToString() == "S";
                        _tipo.AplicarUFG = dataReader["AplicarUFG"].ToString() == "S";
                        
                        try
                        {
                            _tipo.ZerarParcelaApartirDe = Convert.ToInt32(dataReader["ZerarDaParcela"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Honorarios = Convert.ToDecimal(dataReader["Honorarios"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Correcao = Convert.ToDecimal(dataReader["Correcao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Custas = Convert.ToDecimal(dataReader["Custas"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Reducao = Convert.ToDecimal(dataReader["Reducao"].ToString());
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

        public Endividamento.Contrato Consultar(int idEmpresa, decimal codigoFornecedor, string contrato, string modalidade)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Endividamento.Contrato _tipo = new Endividamento.Contrato();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.id, c.idempresa, c.codigofornecedor, c.contrato, c.pedido, c.valor");
                query.Append("     , c.juros, c.multa, c.total, c.qtdoparcelas, c.vencimento, c.dia, c.percjuros, c.AplicaSelic");
                query.Append("     , c.Modalidade, f.nfantasiaforn, f.NrForn");
                query.Append("     , c.PercJurosDif, c.PercMultaDif, c.PercSelicDif, c.ParcelaMinima, c.AplicaJurosDif");
                query.Append("     , c.ParcelaDiferenciada, c.PercParcelaAVista, c.ValorAdesao, c.QtdeParcelaAdesao");
                query.Append("     , c.ZerarParcelas, c.AplicarUFG, c.ZerarDaParcela, c.Honorarios");
                query.Append("     , c.Correcao, c.Reducao, c.Custas");
                query.Append("  from Niff_Ctb_Contrato c, Bgm_Fornecedor f ");
                query.Append(" Where c.idEmpresa = " + idEmpresa);
                query.Append("   And f.codigoforn = " + codigoFornecedor);
                query.Append("   And c.contrato = '" + contrato + "'");
                query.Append("   And c.modalidade = '" + modalidade + "'");
                query.Append("   And f.codigoforn = c.codigofornecedor");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Dia = Convert.ToInt32(dataReader["Dia"].ToString());
                        _tipo.Quantidade = Convert.ToInt32(dataReader["qtdoparcelas"].ToString());
                        _tipo.AplicarSelic = dataReader["AplicaSelic"].ToString() == "S";
                        _tipo.Modalidade = dataReader["Modalidade"].ToString();

                        try
                        {
                            _tipo.CodigoFornecedor = Convert.ToDecimal(dataReader["codigofornecedor"].ToString());
                        }
                        catch { }

                        _tipo.NomeFantasia = dataReader["NrForn"].ToString() + " - " + dataReader["NFantasiaForn"].ToString();
                        _tipo.NumeroContrato = dataReader["contrato"].ToString();
                        _tipo.Pedido = dataReader["Pedido"].ToString();

                        try
                        {
                            _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Juros = Convert.ToDecimal(dataReader["Juros"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Multa = Convert.ToDecimal(dataReader["multa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Consolidado = Convert.ToDecimal(dataReader["Total"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PercentualJuros = Convert.ToDecimal(dataReader["PercJuros"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Vencimento = Convert.ToDateTime(dataReader["Vencimento"].ToString());
                        }
                        catch { }

                        _tipo.AplicarJurosDiferenciado = dataReader["AplicaJurosDif"].ToString() == "S";

                        try
                        {
                            _tipo.PercentualJurosDiferenciado = Convert.ToDecimal(dataReader["PercJurosDif"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PercentualMultaDiferenciada = Convert.ToDecimal(dataReader["PercMultaDif"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PercentualSelicDiferenciada = Convert.ToDecimal(dataReader["PercSelicDif"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.ParcelaMinima = Convert.ToDecimal(dataReader["ParcelaMinima"].ToString());
                        }
                        catch { }

                        _tipo.AplicarParcelasDiferenciado = dataReader["ParcelaDiferenciada"].ToString() == "S";

                        try
                        {
                            _tipo.PercentualAVista = Convert.ToDecimal(dataReader["PercParcelaAVista"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorAdesao = Convert.ToDecimal(dataReader["ValorAdesao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.QtdeParcelasAdesao = Convert.ToDecimal(dataReader["QtdeParcelaAdesao"].ToString());
                        }
                        catch { }

                        _tipo.ZerarParcelas = dataReader["ZerarParcelas"].ToString() == "S";
                        _tipo.AplicarUFG = dataReader["AplicarUFG"].ToString() == "S";
                        
                        try
                        {
                            _tipo.ZerarParcelaApartirDe = Convert.ToInt32(dataReader["ZerarDaParcela"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Honorarios = Convert.ToDecimal(dataReader["Honorarios"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Correcao = Convert.ToDecimal(dataReader["Correcao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Custas = Convert.ToDecimal(dataReader["Custas"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Reducao = Convert.ToDecimal(dataReader["Reducao"].ToString());
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

        public Endividamento.Contrato Consultar(int idContrato)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Endividamento.Contrato _tipo = new Endividamento.Contrato();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.id, c.idempresa, c.codigofornecedor, c.contrato, c.pedido, c.valor");
                query.Append("     , c.juros, c.multa, c.total, c.qtdoparcelas, c.vencimento, c.dia, c.percjuros, c.AplicaSelic");
                query.Append("     , c.Modalidade, f.nfantasiaforn, f.NrForn");
                query.Append("     , c.PercJurosDif, c.PercMultaDif, c.PercSelicDif, c.ParcelaMinima, c.AplicaJurosDif");
                query.Append("     , c.ParcelaDiferenciada, c.PercParcelaAVista, c.ValorAdesao, c.QtdeParcelaAdesao");
                query.Append("     , c.ZerarParcelas, c.AplicarUFG, c.ZerarDaParcela, c.Honorarios");
                query.Append("     , c.Correcao, c.Reducao, c.Custas");
                query.Append("  from Niff_Ctb_Contrato c, Bgm_Fornecedor f ");
                query.Append(" Where c.id = " + idContrato);
                query.Append("   And f.codigoforn = c.codigofornecedor");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Dia = Convert.ToInt32(dataReader["Dia"].ToString());
                        _tipo.Quantidade = Convert.ToInt32(dataReader["qtdoparcelas"].ToString());
                        _tipo.AplicarSelic = dataReader["AplicaSelic"].ToString() == "S";
                        _tipo.Modalidade = dataReader["Modalidade"].ToString();

                        try
                        {
                            _tipo.CodigoFornecedor = Convert.ToDecimal(dataReader["codigofornecedor"].ToString());
                        }
                        catch { }

                        _tipo.NomeFantasia = dataReader["NrForn"].ToString() + " - " + dataReader["NFantasiaForn"].ToString();
                        _tipo.NumeroContrato = dataReader["contrato"].ToString();
                        _tipo.Pedido = dataReader["Pedido"].ToString();

                        try
                        {
                            _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Juros = Convert.ToDecimal(dataReader["Juros"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Multa = Convert.ToDecimal(dataReader["multa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Consolidado = Convert.ToDecimal(dataReader["Total"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PercentualJuros = Convert.ToDecimal(dataReader["PercJuros"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Vencimento = Convert.ToDateTime(dataReader["Vencimento"].ToString());
                        }
                        catch { }

                        _tipo.AplicarJurosDiferenciado = dataReader["AplicaJurosDif"].ToString() == "S";

                        try
                        {
                            _tipo.PercentualJurosDiferenciado = Convert.ToDecimal(dataReader["PercJurosDif"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PercentualMultaDiferenciada = Convert.ToDecimal(dataReader["PercMultaDif"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PercentualSelicDiferenciada = Convert.ToDecimal(dataReader["PercSelicDif"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.ParcelaMinima = Convert.ToDecimal(dataReader["ParcelaMinima"].ToString());
                        }
                        catch { }

                        _tipo.AplicarParcelasDiferenciado = dataReader["ParcelaDiferenciada"].ToString() == "S";

                        try
                        {
                            _tipo.PercentualAVista = Convert.ToDecimal(dataReader["PercParcelaAVista"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorAdesao = Convert.ToDecimal(dataReader["ValorAdesao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.QtdeParcelasAdesao = Convert.ToDecimal(dataReader["QtdeParcelaAdesao"].ToString());
                        }
                        catch { }

                        _tipo.ZerarParcelas = dataReader["ZerarParcelas"].ToString() == "S";
                        _tipo.AplicarUFG = dataReader["AplicarUFG"].ToString() == "S";


                        try
                        {
                            _tipo.ZerarParcelaApartirDe = Convert.ToInt32(dataReader["ZerarDaParcela"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Honorarios = Convert.ToDecimal(dataReader["Honorarios"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Correcao = Convert.ToDecimal(dataReader["Correcao"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Custas = Convert.ToDecimal(dataReader["Custas"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Reducao = Convert.ToDecimal(dataReader["Reducao"].ToString());
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

        public List<Endividamento.Parcelamento> ListarParcelamento(int idContrato, decimal total, bool aplicaSelic)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Endividamento.Parcelamento> lista = new List<Endividamento.Parcelamento>();
            Publicas.mensagemDeErro = string.Empty;
            decimal _encargos = 0;
            decimal _saldoDevedor = 0;
            decimal _saldoAtual = 0;
            decimal _jurosMes = 0;
            bool _atualizada = false;

            List<Classes.Endividamento.Selic> _listaSelic = new EndividamentoDAO().Listar();
            DateTime _dataselic = DateTime.MinValue;

            Endividamento.Contrato _contrato = new EndividamentoDAO().Consultar(idContrato);
            List<Endividamento.Parametros> _listaParametros = new EndividamentoDAO().Listar(_contrato.IdEmpresa, "F");

            try
            {
                query.Append("Select id, idcontrato, parcela, vencimento, valorparcelado, Last_day(Vencimento) Vencimento2");
                query.Append("     , jurosparcelado, multaparcelado, percjuros, selic");
                query.Append("     , juros, valorpagar, ValorPrincipalAtual, ValorJurosDif, ValorMultaDif");
                query.Append("     , SaldoDevedor, SaldoDevedorAnt, CurtoPrazo, LongoPrazo, JurosMes, ParcelaDiferenciada, UFG, ParcelaEmUFG");
                query.Append("     , HonorariosParcelado, CustasParcelada, ReducaoParcelada, CorrecaoParcelada");
                query.Append("  from Niff_Ctb_Parcelamento ");
                query.Append(" Where idcontrato = " + idContrato);
                query.Append(" Order by vencimento, Parcela");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Endividamento.Parcelamento _tipo = new Endividamento.Parcelamento();
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdContrato = Convert.ToInt32(dataReader["idcontrato"].ToString());
                        _tipo.Parcela = Convert.ToInt32(dataReader["parcela"].ToString());
                        _tipo.CodigoFornecedor = _contrato.CodigoFornecedor;
                        _tipo.Fornecedor = _contrato.NomeFantasia;
                        _tipo.Modalidade = _contrato.Modalidade;
                        _tipo.Contrato = _contrato.NumeroContrato;
                        _tipo.Diferenciada = dataReader["ParcelaDiferenciada"].ToString() == "S";

                        try
                        {
                            _tipo.Vencimento = Convert.ToDateTime(dataReader["Vencimento"].ToString());
                            _dataselic = _tipo.Vencimento.AddMonths(-1);
                        }
                        catch { }

                        try
                        {
                            _tipo.Vencimento2 = Convert.ToDateTime(dataReader["Vencimento2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorParcelado = Math.Round(Convert.ToDecimal(dataReader["valorparcelado"].ToString()),2);
                        }
                        catch { }

                        try
                        {
                            _tipo.JurosParcelado = Math.Round(Convert.ToDecimal(dataReader["jurosparcelado"].ToString()),2);
                        }
                        catch { }

                        try
                        {
                            _tipo.MultaParcelada = Math.Round(Convert.ToDecimal(dataReader["multaparcelado"].ToString()),2);
                        }
                        catch { }

                        try
                        {
                            _tipo.Honorarios = Math.Round(Convert.ToDecimal(dataReader["HonorariosParcelado"].ToString()), 2);
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.Custas = Math.Round(Convert.ToDecimal(dataReader["CustasParcelada"].ToString()), 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.Reducao = Math.Round(Convert.ToDecimal(dataReader["ReducaoParcelada"].ToString()), 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.Correcao = Math.Round(Convert.ToDecimal(dataReader["CorrecaoParcelada"].ToString()), 2);
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorConsolidado = Math.Round((_tipo.ValorParcelado + _tipo.JurosParcelado + _tipo.MultaParcelada +
                                _tipo.Honorarios + _tipo.Correcao + _tipo.Custas) - _tipo.Reducao,2);
                               // (!_contrato.AplicarUFG ? 0 : (_tipo.Honorarios + _tipo.Correcao + _tipo.Custas) - _tipo.Reducao),2);
                        }
                        catch { }

                        try
                        {
                            _tipo.Selic = Convert.ToDecimal(dataReader["selic"].ToString());

                            if (_tipo.Selic == 0 && _tipo.Parcela > 1 && _contrato.AplicarSelic)
                            {
                                if ((_tipo.Parcela > 1 && _contrato.AplicarJurosDiferenciado) ||
                                     (_tipo.Parcela > 2 && !_contrato.AplicarJurosDiferenciado))
                                {
                                    foreach (var item in _listaSelic.Where(w => w.MesAno == _dataselic.Month.ToString("00") + "/" + _dataselic.Year.ToString()))
                                    {
                                        _tipo.Selic = item.Valor;
                                    }
                                }
                            }
                        }
                        catch { }

                        try
                        {
                            _tipo.UFG = Convert.ToDecimal(dataReader["UFG"].ToString());

                            if (_tipo.UFG == 0 && _contrato.AplicarUFG)
                            {
                                foreach (var item in _listaSelic.Where(w => w.Ano == _dataselic.Year && w.ValorUFG > 0))
                                {
                                    _tipo.UFG = item.ValorUFG;
                                }
                            }
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.ParcelaEmUFG = Convert.ToDecimal(dataReader["ParcelaEmUFG"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.PercentualJuros = Convert.ToDecimal(dataReader["PercJuros"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.SaldoDevedor = Math.Round(Convert.ToDecimal(dataReader["SaldoDevedor"].ToString()),2);
                        }
                        catch { }
                        
                        try
                        {// nome na tabela errado. 
                            _tipo.SaldoDevedorAtual = Math.Round(Convert.ToDecimal(dataReader["SaldoDevedorAnt"].ToString()), 2);
                        }
                        catch { }

                        if (!_tipo.Diferenciada && (_tipo.Diferenciada || _contrato.ValorAdesao == 0))
                        {
                            try
                            {
                                _tipo.SaldoCurto = Math.Round(Convert.ToDecimal(dataReader["CurtoPrazo"].ToString()), 2);
                            }
                            catch { }

                            try
                            {
                                _tipo.SaldoLongo = Math.Round(Convert.ToDecimal(dataReader["LongoPrazo"].ToString()), 2);
                            }
                            catch { }

                            try
                            {
                                _tipo.JurosMes = Math.Round(Convert.ToDecimal(dataReader["JurosMes"].ToString()), 2);
                            }
                            catch { }
                        }

                        try
                        {
                            _tipo.Juros = Math.Round(Convert.ToDecimal(dataReader["Juros"].ToString()),2);
                        }
                        catch { }

                        if (!_contrato.AplicarUFG)
                        {
                            if (_tipo.Parcela == 1 && (!_tipo.Diferenciada || (_tipo.Diferenciada && _contrato.ValorAdesao == 0)))
                            {
                                _saldoDevedor = total - _tipo.ValorConsolidado;
                                _saldoAtual = _saldoDevedor;

                                if (_contrato.ValorAdesao == 0)
                                    _encargos = 0;
                                else
                                {
                                    _tipo.Encargos = _encargos + _tipo.Selic + _tipo.UFG;
                                    _encargos = _tipo.Encargos;
                                    if (_contrato.ValorAdesao != 0)
                                        _saldoAtual = _saldoDevedor + (_saldoDevedor * (_tipo.Encargos / 100));
                                }
                            }
                            else
                            {
                                if ((_tipo.Parcela == 2 && !_tipo.Diferenciada && _contrato.ValorAdesao == 0) ||
                                    (_tipo.Parcela == 2 && _tipo.Diferenciada && _contrato.ValorAdesao != 0))
                                    _encargos = _tipo.PercentualJuros;

                                try
                                {
                                    _tipo.Encargos = _encargos + _tipo.Selic + _tipo.UFG;
                                }
                                catch { }

                                _saldoDevedor = _saldoDevedor - _tipo.ValorConsolidado;
                                _jurosMes = _saldoAtual;
                                _saldoAtual = _saldoDevedor + (_saldoDevedor * (_tipo.Encargos / 100));

                                if (!_tipo.Diferenciada || (_tipo.Diferenciada && _contrato.ValorAdesao == 0))
                                    _tipo.JurosMes = _saldoAtual - (_jurosMes - _tipo.ValorConsolidado);

                                _encargos = _tipo.Encargos;

                            }

                            if (!_tipo.Diferenciada || (_tipo.Diferenciada && _contrato.ValorAdesao == 0))
                            {
                                _tipo.SaldoDevedor = _saldoDevedor;
                                _tipo.SaldoDevedorAtual = _saldoAtual;
                            }
                        }
                          
                        try
                        {
                            _tipo.JurosEMulta = (_tipo.JurosParcelado + _tipo.MultaParcelada + _tipo.Honorarios + _tipo.Custas + _tipo.Correcao) - _tipo.Reducao;
                        }
                        catch { }

                        try
                        {
                            _tipo.ValorPagar = Math.Round(Convert.ToDecimal(dataReader["valorpagar"].ToString()), 2);
                        }
                        catch { }

                        if (!_contrato.AplicarUFG)
                            _tipo.Juros = Math.Round(_tipo.ValorConsolidado * (_tipo.Encargos / 100), 2);

                        if (_tipo.Selic != Convert.ToDecimal(dataReader["selic"].ToString()))
                        {
                            _tipo.ValorPagar = _tipo.ValorConsolidado + _tipo.Juros;
                            _atualizada = true;                            
                        }

                        if (_tipo.UFG != Convert.ToDecimal(dataReader["UFG"].ToString()))
                            _atualizada = true;

                                                // Atualizou uma Selic ou UFG terá que recalcular os Saldo Curto e Longo desta data e das posteriores
                        _tipo.Atualizada = _atualizada;
                        try
                        {
                            if (Convert.ToDecimal(dataReader["ValorJurosDif"].ToString()) > 0)
                            {
                                _tipo.PercentualJurosDiferenciado = _contrato.PercentualJurosDiferenciado;
                                _tipo.PercentualMultaDiferenciada = _contrato.PercentualMultaDiferenciada;
                                _tipo.PercentualSelicDiferenciada = _contrato.PercentualSelicDiferenciada;

                                _tipo.ValorPrincipalAtualizado = Math.Round(_tipo.ValorParcelado + (_tipo.ValorParcelado * (_tipo.Encargos / 100)), 2);

                                _tipo.MultaDiferenciada = Math.Round(_tipo.ValorPrincipalAtualizado * (_tipo.PercentualMultaDiferenciada / 100), 2);

                                if (_contrato.AplicarJurosDiferenciado)
                                {
                                    _tipo.Juros = Math.Round(_tipo.ValorPrincipalAtualizado * (_tipo.PercentualSelicDiferenciada / 100), 2);

                                    _tipo.JurosDiferenciado = Math.Round((_tipo.ValorPrincipalAtualizado + _tipo.Juros) * (_tipo.PercentualJurosDiferenciado / 100), 2);
                                    _tipo.ValorPagar = _tipo.ValorPrincipalAtualizado + _tipo.MultaDiferenciada + _tipo.JurosDiferenciado + _tipo.Juros;
                                }
                            }
                        }
                        catch { }

                        _tipo.TemContaJuros = false;
                        _tipo.TemContaCurtoLongo = false;

                        foreach (var item in _listaParametros.Where(w => w.CodigoFornecedor == _tipo.CodigoFornecedor &&
                                                                         w.Modalidade == _tipo.Modalidade))
                        {
                            _tipo.TemContaJuros = (item.CodigoContaJurosDebito != 0 && item.CodigoContaJurosCredito != 0);
                            _tipo.CTBJurosDebito = item.CodigoContaJurosDebito;
                            _tipo.CTBJurosCredito = item.CodigoContaJurosCredito;

                            _tipo.TemContaCurtoLongo = (item.CodigoContaCurtoPrevisto != 0 && item.CodigoContaLongoPrevisto != 0);
                            _tipo.CTBCurto = item.CodigoContaCurtoPrevisto; 
                            _tipo.CTBLongo = item.CodigoContaLongoPrevisto;
                        }

                        lista.Add(_tipo);
                    }
                }

                int qtd = 12;
                // Recalcula o Saldo Curto e Longo quando tiver nova Selic ou os valores estiverem zerado
                
                if (_contrato.AplicarUFG)
                {
                    decimal _totalaPagar = lista.Sum(s => s.ValorPagar);

                    foreach (var item in lista.OrderBy(o => o.Parcela))
                    {
                        item.SaldoDevedor = Math.Round(_totalaPagar,2) - Math.Round(item.ValorPagar,2);
                        _totalaPagar = item.SaldoDevedor;
                    }
                }

                if (_contrato.ValorAdesao == 0)
                {
                    foreach (var item in lista//.Where(w => w.SaldoCurto == 0) // || w.Atualizada)
                                          .OrderBy(o => o.Parcela))
                    {
                        List<Endividamento.Parcelamento> listaC = new List<Endividamento.Parcelamento>();

                        DateTime dataAuxiliar = item.Vencimento2.AddMonths(1);
                        DateTime dataAuxiliar2 = item.Vencimento2.AddMonths(qtd);

                        listaC = lista.Where(w => Convert.ToInt32(w.Vencimento2.Year.ToString("0000") + w.Vencimento2.Month.ToString("00")) >=
                            Convert.ToInt32(dataAuxiliar.Year.ToString("0000") + dataAuxiliar.Month.ToString("00")) &&
                            Convert.ToInt32(w.Vencimento2.Year.ToString("0000") + w.Vencimento2.Month.ToString("00")) <= Convert.ToInt32(dataAuxiliar2.Year.ToString("0000") + dataAuxiliar2.Month.ToString("00"))).ToList();

                        if (_contrato.AplicarUFG)
                            item.SaldoCurto = listaC.Sum(s => s.ValorPagar);
                        else
                            item.SaldoCurto = listaC.Sum(s => s.ValorConsolidado);

                        if (item.Parcela > 1)
                            item.SaldoCurto = item.SaldoCurto + (item.SaldoCurto * item.Encargos / 100);

                        listaC = lista.Where(w => Convert.ToInt32(w.Vencimento2.Year.ToString("0000") + w.Vencimento2.Month.ToString("00")) > Convert.ToInt32(dataAuxiliar2.Year.ToString("0000") + dataAuxiliar2.Month.ToString("00"))).ToList();

                        if (_contrato.AplicarUFG)
                            item.SaldoLongo = listaC.Sum(s => s.ValorPagar);
                        else
                            item.SaldoLongo = listaC.Sum(s => s.ValorConsolidado);

                        if (item.Parcela > 1)
                            item.SaldoLongo = item.SaldoLongo + (item.SaldoLongo * item.Encargos / 100);
                    }
                }
                else
                {
                    foreach (var item in lista.Where(w => !w.Diferenciada && w.SaldoCurto == 0 || w.Atualizada)
                         .OrderBy(o => o.Parcela))
                    {
                        List<Endividamento.Parcelamento> listaC = new List<Endividamento.Parcelamento>();

                        DateTime dataAuxiliar = item.Vencimento2.AddMonths(1);
                        DateTime dataAuxiliar2 = item.Vencimento2.AddMonths(qtd);

                        listaC = lista.Where(w => !w.Diferenciada &&
                            Convert.ToInt32(w.Vencimento2.Year.ToString("0000") + w.Vencimento2.Month.ToString("00")) >=
                            Convert.ToInt32(dataAuxiliar.Year.ToString("0000") + dataAuxiliar.Month.ToString("00")) &&
                            Convert.ToInt32(w.Vencimento2.Year.ToString("0000") + w.Vencimento2.Month.ToString("00")) <= Convert.ToInt32(dataAuxiliar2.Year.ToString("0000") + dataAuxiliar2.Month.ToString("00"))).ToList();

                        if (_contrato.AplicarUFG)
                            item.SaldoCurto = listaC.Sum(s => s.ValorPagar);
                        else
                            item.SaldoCurto = listaC.Sum(s => s.ValorConsolidado);

                        if (item.Parcela > 1)
                            item.SaldoCurto = item.SaldoCurto + (item.SaldoCurto * item.Encargos / 100);

                        listaC = lista.Where(w => !w.Diferenciada && 
                                                    Convert.ToInt32(w.Vencimento2.Year.ToString("0000") + w.Vencimento2.Month.ToString("00")) > Convert.ToInt32(dataAuxiliar2.Year.ToString("0000") + dataAuxiliar2.Month.ToString("00"))).ToList();

                        if (_contrato.AplicarUFG)
                            item.SaldoLongo = listaC.Sum(s => s.ValorPagar);
                        else
                            item.SaldoLongo = listaC.Sum(s => s.ValorConsolidado);

                        if (item.Parcela > 1)
                            item.SaldoLongo = item.SaldoLongo + (item.SaldoLongo * item.Encargos / 100);
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

        public List<Endividamento.Parcelamento> ListarParcelamento(int idEmpresa, DateTime inicio, bool consolidar = false)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            
            List<Endividamento.Parcelamento> lista = new List<Endividamento.Parcelamento>();
            Publicas.mensagemDeErro = string.Empty;
            List<Classes.Endividamento.Selic> _listaSelic = new EndividamentoDAO().Listar();
            List<Classes.Endividamento.Contrato> _contratos = new EndividamentoDAO().Listar(idEmpresa, consolidar);

            DateTime _dataselic = DateTime.MinValue;

            try
            {
                foreach (var itemC in _contratos)
                {
                    List<Endividamento.Parcelamento> listaPorContrato = ListarParcelamento(itemC.Id, itemC.Consolidado, itemC.AplicarSelic);

                    lista.AddRange(listaPorContrato);
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

        public List<Endividamento.Arquivo> Arquivos(int idContrato)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Endividamento.Arquivo> lista = new List<Endividamento.Arquivo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idcontrato, Arquivo, NomeArquivo");
                query.Append("  from Niff_Ctb_ArquivosContrato ");
                query.Append(" Where idcontrato = " + idContrato);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Endividamento.Arquivo _tipo = new Endividamento.Arquivo();
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdContrato = Convert.ToInt32(dataReader["idcontrato"].ToString());
                        _tipo.NomeArquivo = dataReader["NomeArquivo"].ToString();

                        try
                        {
                            _tipo.Imagem = (byte[])(dataReader["Arquivo"]);
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

        public bool Gravar(Endividamento.Contrato contrato, List<Endividamento.Parcelamento> _lista, List<Endividamento.Arquivo> _arquivos, List<Endividamento.Arquivo> _arquivosAntes)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = _lista.Count() == 0;
            int id = 01;

            OracleParameter parametro = new OracleParameter();
            List<OracleParameter> parametros = new List<OracleParameter>();

            try
            {
                if (!contrato.Existe)
                {
                    query.Clear();
                    query.Append("Select nvl(Max(id),0) +1 next from Niff_Ctb_Contrato");

                    Query executar = sessao.CreateQuery(query.ToString());
                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                        {
                            id = Convert.ToInt32(dataReader["next"].ToString());
                        }
                    }

                    query.Clear();
                    query.Append("Insert into Niff_Ctb_Contrato");
                    query.Append(" ( id, idempresa, codigofornecedor, contrato, pedido, valor");
                    query.Append("     , juros, multa, total, qtdoparcelas, vencimento, dia, percjuros, AplicaSelic, Modalidade");
                    query.Append("     , PercJurosDif, PercMultaDif, PercSelicDif, ParcelaMinima, AplicaJurosDif");
                    query.Append("     , ParcelaDiferenciada, PercParcelaAVista, ValorAdesao, QtdeParcelaAdesao");
                    query.Append("     , ZerarParcelas, AplicarUFG, ZerarDaParcela, Honorarios");
                    query.Append("     , Correcao, Reducao, Custas");
                    query.Append(") Values ( " + id);
                    query.Append("        , " + contrato.IdEmpresa);
                    query.Append("        , " + contrato.CodigoFornecedor);
                    query.Append("        , '" + contrato.NumeroContrato + "'");
                    query.Append("        , '" + contrato.Pedido + "'");
                    query.Append("        , " + contrato.Valor.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + contrato.Juros.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + contrato.Multa.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + contrato.Consolidado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + contrato.Quantidade);
                    query.Append("        , To_date('" + contrato.Vencimento.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("        , " + contrato.Dia);
                    query.Append("        , " + contrato.PercentualJuros.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , '" + (contrato.AplicarSelic ? "S" : "N")+ "'");
                    query.Append("        , '" + contrato.Modalidade + "'");
                    query.Append("        , " + contrato.PercentualJurosDiferenciado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + contrato.PercentualMultaDiferenciada.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + contrato.PercentualSelicDiferenciada.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + contrato.ParcelaMinima.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , '" + (contrato.AplicarJurosDiferenciado ? "S" : "N") + "'");
                    query.Append("        , '" + (contrato.AplicarParcelasDiferenciado ? "S" : "N") + "'");

                    query.Append("        , " + contrato.PercentualAVista.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + contrato.ValorAdesao.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + contrato.QtdeParcelasAdesao);
                    query.Append("        , '" + (contrato.ZerarParcelas ? "S" : "N") + "'");
                    query.Append("        , '" + (contrato.AplicarUFG ? "S" : "N") + "'");
                    query.Append("        , " + contrato.ZerarParcelaApartirDe);
                    query.Append("        , " + contrato.Honorarios.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + contrato.Correcao.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + contrato.Reducao.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + contrato.Custas.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" )");
                }
                else
                {
                    id = contrato.Id;
                    query.Append("Update Niff_Ctb_Contrato");
                    query.Append("   set pedido = '" + contrato.Pedido + "'");
                    query.Append("     , Valor = " + contrato.Valor.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Juros = " + contrato.Juros.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Multa = " + contrato.Multa.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Total = " + contrato.Consolidado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , qtdoparcelas = " + contrato.Quantidade);
                    query.Append("     , Vencimento = To_date('" + contrato.Vencimento.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("     , Dia = " + contrato.Dia);
                    query.Append("     , percjuros = " + contrato.PercentualJuros.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , AplicaSelic = '" + (contrato.AplicarSelic ? "S" : "N") + "'");
                    query.Append("     , Modalidade = '" + contrato.Modalidade + "'");
                    query.Append("     , PercJurosDif = " + contrato.PercentualJurosDiferenciado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , PercMultaDif = " + contrato.PercentualMultaDiferenciada.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , PercSelicDif = " + contrato.PercentualSelicDiferenciada.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ParcelaMinima = " + contrato.ParcelaMinima.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , AplicaJurosDif = '" + (contrato.AplicarJurosDiferenciado ? "S" : "N") + "'");

                    query.Append("     , ParcelaDiferenciada = '" + (contrato.AplicarParcelasDiferenciado ? "S" : "N") + "'");

                    query.Append("     , PercParcelaAVista = " + contrato.PercentualAVista.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ValorAdesao = " + contrato.ValorAdesao.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , QtdeParcelaAdesao = " + contrato.QtdeParcelasAdesao);
                    query.Append("     , ZerarParcelas = '" + (contrato.ZerarParcelas ? "S" : "N") + "'");
                    query.Append("     , AplicarUFG = '" + (contrato.AplicarUFG ? "S" : "N") + "'");
                    query.Append("     , ZerarDaParcela = " + contrato.ZerarParcelaApartirDe);
                    query.Append("     , Honorarios = " + contrato.Honorarios.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Correcao = " + contrato.Correcao.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Reducao = " + contrato.Reducao.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Custas = " + contrato.Custas.ToString().Replace(".", "").Replace(",", "."));

                    query.Append(" Where Id = " + contrato.Id);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (!retorno)
                    return false;
                else
                {
                    foreach (var item in _arquivosAntes)
                    {
                        if (_arquivos.Where(w => w.Id == item.Id).Count() == 0)
                        {
                            // foi excluido, apaga da tabela
                            query.Clear();
                            query.Append("Delete Niff_Ctb_ArquivosContrato");
                            query.Append(" Where Id = " + id);
                            retorno = sessao.ExecuteSqlTransaction(query.ToString());
                        }
                    }

                    foreach (var item in _arquivos)
                    {
                        if (!item.Existe)
                        {
                            query.Clear();
                            query.Append("Insert into Niff_Ctb_ArquivosContrato");
                            query.Append(" ( id, idcontrato, NomeArquivo, Arquivo");
                            query.Append(") Values ( (Select nvl(Max(id),0) +1 from Niff_Ctb_ArquivosContrato)");
                            query.Append("        , " + id);
                            query.Append("        , '" + item.NomeArquivo + "'");

                            if (item.Imagem == null)
                                query.Append(", null ");
                            else
                            {
                                query.Append(", :pfoto ");
                                parametro.ParameterName = ":pfoto";
                                parametro.Value = item.Imagem;
                                parametro.OracleType = OracleType.Blob;
                                parametros.Add(parametro);
                            }

                            query.Append(" )");
                        }
                        else
                        {
                            if (item.Imagem != null)
                            {
                                query.Clear();
                                query.Append("Update Niff_Ctb_ArquivosContrato");
                                query.Append("  set Arquivo = :pFoto");
                                query.Append("    , NomeArquivo = '" + item.NomeArquivo + "'");

                                parametro.ParameterName = ":pfoto";
                                parametro.Value = item.Imagem;
                                parametro.OracleType = OracleType.Blob;
                                parametros.Add(parametro);
                                query.Append(" Where Id = " + item.Id);
                            }
                        }

                        retorno = sessao.ExecuteSqlTransaction(query.ToString(), parametros.ToArray());

                        if (!retorno)
                            break;
                    }

                    if (!retorno)
                         return false;

                    foreach (var item in _lista)
                    {
                        if (!item.Existe)
                        {
                            query.Clear();
                            query.Append("Insert into Niff_Ctb_Parcelamento");
                            query.Append(" ( id, idcontrato, parcela, vencimento, valorparcelado");
                            query.Append("     , jurosparcelado, multaparcelado, percjuros, selic");
                            query.Append("     , juros, valorpagar, ValorPrincipalAtual, ValorJurosDif, ValorMultaDif");
                            query.Append("     , SaldoDevedor, SaldoDevedorAnt, CurtoPrazo, LongoPrazo, JurosMes, ParcelaDiferenciada");
                            query.Append("     , UFG, ParcelaEmUFG, HonorariosParcelado, ReducaoParcelada, CorrecaoParcelada, CustasParcelada");
                            query.Append(") Values ( (Select nvl(Max(id),0) +1 from Niff_Ctb_Parcelamento)");
                            query.Append("        , " + id);
                            query.Append("        , " + item.Parcela);
                            query.Append("        , To_date('" + item.Vencimento.ToShortDateString() + "','dd/mm/yyyy')");
                            query.Append("        , " + Math.Round(item.ValorParcelado,2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.JurosParcelado, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.MultaParcelada, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.PercentualJuros, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + item.Selic.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.Juros, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.ValorPagar, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.ValorPrincipalAtualizado, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.JurosDiferenciado, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.MultaDiferenciada, 2).ToString().Replace(".", "").Replace(",", "."));

                            query.Append("        , " + Math.Round(item.SaldoDevedor, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.SaldoDevedorAtual, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.SaldoCurto, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.SaldoLongo, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.JurosMes, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , '" + (item.Diferenciada ? "S" : "N") + "'");
                            query.Append("        , " + Math.Round(item.UFG, 4).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.ParcelaEmUFG, 4).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.Honorarios, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.Reducao, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.Correcao, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + Math.Round(item.Custas, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" )");
                        }
                        else
                        {                            
                            query.Clear();
                            query.Append("Update Niff_Ctb_Parcelamento");
                            query.Append("  set parcela = " + item.Parcela);
                            query.Append("    , vencimento = To_date('" + item.Vencimento.ToShortDateString() + "','dd/mm/yyyy')");
                            query.Append("    , Valorparcelado = " + Math.Round(item.ValorParcelado,2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , JurosParcelado = " + Math.Round(item.JurosParcelado,2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , MultaParcelado = " + Math.Round(item.MultaParcelada, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , PercJuros = " + Math.Round(item.PercentualJuros, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , Selic = " + item.Selic.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , Juros = " + Math.Round(item.Juros, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , ValorPagar = " + Math.Round(item.ValorPagar, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , ValorPrincipalAtual = " + Math.Round(item.ValorPrincipalAtualizado, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , ValorJurosDif = " + Math.Round(item.JurosDiferenciado, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , ValorMultaDif = " + Math.Round(item.MultaDiferenciada, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , SaldoDevedor = " + Math.Round(item.SaldoDevedor, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , SaldoDevedorAnt = " + Math.Round(item.SaldoDevedorAtual, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , CurtoPrazo = " + Math.Round(item.SaldoCurto, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , LongoPrazo = " + Math.Round(item.SaldoLongo, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , JurosMes = " + Math.Round(item.JurosMes, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , ParcelaDiferenciada = '" + (item.Diferenciada ? "S" : "N") + "'");
                            query.Append("    , UFG = " + Math.Round(item.UFG, 4).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , ParcelaEmUFG = " + Math.Round(item.ParcelaEmUFG, 4).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , HonorariosParcelado = " + Math.Round(item.Honorarios, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , ReducaoParcelada = " + Math.Round(item.Reducao, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , CorrecaoParcelada = " + Math.Round(item.Correcao, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("    , CustasParcelada = " + Math.Round(item.Custas, 2).ToString().Replace(".", "").Replace(",", "."));

                            query.Append(" Where id = " + item.Id);

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

        public bool EditarNumeroContrato(Endividamento.Contrato contrato)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            OracleParameter parametro = new OracleParameter();
            List<OracleParameter> parametros = new List<OracleParameter>();

            try
            {
                
                query.Append("Update Niff_Ctb_Contrato");
                query.Append("   set CONTRATO = '" + contrato.NumeroContrato + "'");

                query.Append(" Where Id = " + contrato.Id);

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


        public bool ExcluirParcelamento(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ctb_ArquivosContrato");
                query.Append(" Where IdContrato = " + id);
                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;
                else
                {
                    query.Clear();
                    query.Append("Delete Niff_Ctb_Parcelamento");
                    query.Append(" Where IdContrato = " + id);
                    if (!sessao.ExecuteSqlTransaction(query.ToString()))
                        return false;
                    else
                    {
                        query.Clear();
                        query.Append("Delete Niff_Ctb_Contrato");
                        query.Append(" Where Id = " + id);
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
        #endregion
    }
}
