using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class LalurDAO
    {
        #region Formulas
        IDataReader dataReader;
        IDataReader dataReader1;

        public List<Lalur.Formulas> Listar(int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Lalur.Formulas> _lista = new List<Lalur.Formulas>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, codigo, ativo, totalizador, ordem, descricao, formula, Destacar");
                query.Append("  From Niff_CTB_FormulaLalur C");
                query.Append(" Where c.idempresa = " + idEmpresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Lalur.Formulas _tipo = new Lalur.Formulas();

                        _tipo.Existe = true;
                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                            _tipo.Codigo = Convert.ToInt32(dataReader["codigo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Formula = dataReader["Formula"].ToString();

                        _tipo.Ordem = Convert.ToInt32(dataReader["Ordem"].ToString());                        
                        
                        _tipo.Ativo = dataReader["ativo"].ToString() == "S";
                        _tipo.Totalizador = dataReader["Totalizador"].ToString() == "S";
                        _tipo.Destacar = dataReader["Destacar"].ToString() == "S";

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

        public Lalur.Formulas Consultar(int idEmpresa, int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Lalur.Formulas _tipo = new Lalur.Formulas();

            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, codigo, ativo, totalizador, ordem, descricao, formula, Destacar");
                query.Append("  From Niff_CTB_FormulaLalur C");
                query.Append(" Where c.idempresa = " + idEmpresa);
                query.Append("   and c.codigo = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                            _tipo.Codigo = Convert.ToInt32(dataReader["codigo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Formula = dataReader["Formula"].ToString();

                        _tipo.Ordem = Convert.ToInt32(dataReader["Ordem"].ToString());

                        _tipo.Ativo = dataReader["ativo"].ToString() == "S";
                        _tipo.Totalizador = dataReader["Totalizador"].ToString() == "S";
                        _tipo.Destacar = dataReader["Destacar"].ToString() == "S";
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

        public Lalur.Formulas Consultar(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Lalur.Formulas _tipo = new Lalur.Formulas();

            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, codigo, ativo, totalizador, ordem, descricao, formula, Destacar");
                query.Append("  From Niff_CTB_FormulaLalur C");
                query.Append(" Where c.id = " + id);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                            _tipo.Codigo = Convert.ToInt32(dataReader["codigo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Formula = dataReader["Formula"].ToString();

                        _tipo.Ordem = Convert.ToInt32(dataReader["Ordem"].ToString());

                        _tipo.Ativo = dataReader["ativo"].ToString() == "S";
                        _tipo.Totalizador = dataReader["Totalizador"].ToString() == "S";
                        _tipo.Destacar = dataReader["Destacar"].ToString() == "S";
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

        public bool Gravar(Lalur.Formulas item, List<Lalur.ContasDaFormula> contas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            int id = 0;
            try
            {
                query.Clear();

                if (!item.Existe)
                {
                    query.Append("Select nvl(Max(id), 0) +1 next from Niff_CTB_FormulaLalur");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            id = Convert.ToInt32(dataReader["next"].ToString());
                    }
                    query.Clear();

                    query.Append("Insert into Niff_CTB_FormulaLalur");
                    query.Append(" ( id, idempresa, codigo, ativo, totalizador, ordem, descricao, formula, Destacar )");
                    query.Append(" Values ( " + id);
                    query.Append("        , " + item.IdEmpresa);
                    query.Append("        , " + item.Codigo);
                    query.Append("        , '" + (item.Ativo ? "S" : "N") + "'");
                    query.Append("        , '" + (item.Totalizador ? "S" : "N") + "'");
                    query.Append("        , " + item.Ordem);
                    query.Append("        , '" + item.Descricao + "'");
                    query.Append("        , '" + item.Formula + "'");
                    query.Append("        , '" + (item.Destacar ? "S" : "N") + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_CTB_FormulaLalur");
                    query.Append("   set ativo = '" + (item.Ativo ? "S" : "N") + "'");
                    query.Append("     , totalizador = '" + (item.Totalizador ? "S" : "N") + "'");
                    query.Append("     , ordem = " + item.Ordem);
                    query.Append("     , Descricao = '" + item.Descricao + "'");
                    query.Append("     , Formula = '" + item.Formula + "'");
                    query.Append("     , Destacar = '" + (item.Destacar ? "S" : "N") + "'");
                    
                    query.Append(" where Id = " + item.Id);
                    id = item.Id;
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    contas.ForEach(u => u.IdFormula = id);
                    foreach (var items in contas)
                    {
                        query.Clear();
                        if (!items.Existe)
                        {
                            query.Append("Insert into Niff_CTB_ContasFormulaLalur");
                            query.Append(" ( id, idformula, nroplano, codcontactb, regra )"); 
                            query.Append(" Values ( ( Select nvl(Max(id), 0) +1 from Niff_CTB_ContasFormulaLalur )");
                            query.Append("        , " + items.IdFormula);
                            query.Append("        , " + items.NumeroPlano);
                            query.Append("        , " + items.CodigoConta);
                            query.Append("        , '" + items.Regra + "'");
                            query.Append(" )");

                            retorno = sessao.ExecuteSqlTransaction(query.ToString());

                            if (!retorno)
                                break;
                        }
                        else
                        {
                            query.Append("Update Niff_CTB_ContasFormulaLalur");
                            query.Append("   set regra = '" + items.Regra + "'");
                            query.Append(" where id = " + items.Id);

                            retorno = sessao.ExecuteSqlTransaction(query.ToString());

                            if (!retorno)
                                break;
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

        public bool Excluir(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            try
            {
                query.Append("Delete Niff_CTB_ContasFormulaLalur");
                query.Append(" Where idFormula = " + id);
                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    query.Clear();
                    query.Append("Delete Niff_CTB_FormulaLalur");
                    query.Append(" Where id = " + id);

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

        public int Proximo(int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Nvl(Max(codigo),0) +1 next From Niff_CTB_FormulaLalur where idEmpresa = " + idEmpresa);
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        retorno = Convert.ToInt32(dataReader["next"].ToString());
                }
                return retorno;
            }
            catch
            {
                return retorno;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        #endregion

        #region Contas Contabeis Formula
        public List<Lalur.ContasDaFormula> ListarContas(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Lalur.ContasDaFormula> _lista = new List<Lalur.ContasDaFormula>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select f.id, f.idformula, f.nroplano, f.codcontactb, f.regra");
                query.Append("     , c.classificador || ' ' || c.NomeConta nome");
                query.Append("  From Niff_CTB_ContasFormulaLalur f, ctbConta c");
                query.Append(" Where f.idFormula = " + id);
                query.Append("   and f.codcontactb = c.codcontactb");
                query.Append("   and f.nroplano = c.nroplano");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Lalur.ContasDaFormula _tipo = new Lalur.ContasDaFormula();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdFormula = Convert.ToInt32(dataReader["IdFormula"].ToString());
                        _tipo.NumeroPlano = Convert.ToInt32(dataReader["nroplano"].ToString());
                        _tipo.CodigoConta  = Convert.ToInt32(dataReader["codcontactb"].ToString());
                        _tipo.NomeConta = dataReader["Nome"].ToString();
                        _tipo.Regra = dataReader["Regra"].ToString();

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

        public bool ExcluirConta(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_CTB_ContasFormulaLalur");
                query.Append(" Where id = " + id);
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

        #region Parametros
        public Lalur.Parametros ConsultarParametros(int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Lalur.Parametros _tipo = new Lalur.Parametros();

            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, perccompbasecalculo, perccsll, percirpj, valorparcelaisenta, percadicionalpagar, percpat, LimiteIRPJ, LimiteCSLL");
                query.Append("  From Niff_Ctb_ParamLalur C");
                query.Append(" Where c.idempresa = " + idEmpresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        _tipo.PercentualAdicionalPagar = Convert.ToDecimal(dataReader["PercAdicionalPagar"].ToString());
                        _tipo.PercentualCompensacaoNegativa = Convert.ToDecimal(dataReader["PercCompBaseCalculo"].ToString());
                        _tipo.PercentualCSLL = Convert.ToDecimal(dataReader["PercCSLL"].ToString());
                        _tipo.PercentualIRPJ = Convert.ToDecimal(dataReader["PercIRPJ"].ToString());
                        _tipo.PercentualPat = Convert.ToDecimal(dataReader["PercPat"].ToString());
                        _tipo.ValorParcelaIsenta = Convert.ToDecimal(dataReader["ValorParcelaIsenta"].ToString());
                        _tipo.LimiteCSLL = Convert.ToDecimal(dataReader["LimiteCSLL"].ToString());
                        _tipo.LimiteIRPJ = Convert.ToDecimal(dataReader["LimiteIRPJ"].ToString());
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

        public bool Gravar(Lalur.Parametros item)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            int id = 0;
            try
            {
                query.Clear();

                if (!item.Existe)
                {
                    query.Append("Select nvl(Max(id), 0) +1 next from Niff_Ctb_ParamLalur");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            id = Convert.ToInt32(dataReader["next"].ToString());
                    }
                    query.Clear();

                    query.Append("Insert into Niff_Ctb_ParamLalur");
                    query.Append(" ( id, idempresa, perccompbasecalculo, perccsll, percirpj, valorparcelaisenta, percadicionalpagar, percpat, LimiteIRPJ, LimiteCSLL )");
                    query.Append(" Values ( " + id);
                    query.Append("        , " + item.IdEmpresa);
                    query.Append("        , " + item.PercentualCompensacaoNegativa.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.PercentualCSLL.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.PercentualIRPJ.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.ValorParcelaIsenta.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.PercentualAdicionalPagar.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.PercentualPat.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.LimiteIRPJ.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.LimiteCSLL.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_Ctb_ParamLalur");
                    query.Append("   set perccompbasecalculo = " + item.PercentualCompensacaoNegativa.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , perccsll = " + item.PercentualCSLL.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , percirpj = " + item.PercentualIRPJ.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , valorparcelaisenta = " + item.ValorParcelaIsenta.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , percadicionalpagar = " + item.PercentualAdicionalPagar.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , percpat = " + item.PercentualPat.ToString().Replace(".", "").Replace(",", "."));

                    query.Append("     , limiteIRPJ = " + item.LimiteIRPJ.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , limiteCSLL = " + item.LimiteCSLL.ToString().Replace(".", "").Replace(",", "."));

                    query.Append(" where Id = " + item.Id);
                    id = item.Id;
                }

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

        public bool ExcluirParametros(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            try
            {
                query.Append("Delete Niff_Ctb_ParamLalur");
                query.Append(" Where id = " + id);

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
        #endregion

        #region Apuracao
        public Lalur.Apuracao Consultar(int idEmpresa, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Lalur.Apuracao _tipo = new Lalur.Apuracao();

            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, referencia, periodoencerrado");
                query.Append("  From Niff_Ctb_Lalur C");
                query.Append(" Where idempresa = " + idEmpresa);
                query.Append("   and referencia = " + referencia);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        _tipo.Referencia = dataReader["Referencia"].ToString();
                        _tipo.Fechado = dataReader["periodoencerrado"].ToString() == "S";
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

        public List<Lalur.Valores> ListarApuracao(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Lalur.Valores> _lista = new List<Lalur.Valores>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.id, c.idlalur, c.idformula, c.valor, c.Percentual, f.descricao, f.Ordem, f.Destacar ");
                query.Append("  From niff_ctb_lalurvalores C, niff_ctb_formulalalur f");
                query.Append(" Where c.idlalur = " + id);
                query.Append("   and f.Id = c.IdFormula");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Lalur.Valores _tipo = new Lalur.Valores();

                        _tipo.Existe = true;
                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                            _tipo.IdLalur = Convert.ToInt32(dataReader["IdLalur"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdFormula = Convert.ToInt32(dataReader["IdFormula"].ToString());
                        }
                        catch { }

                        _tipo.Ordem = Convert.ToInt32(dataReader["Ordem"].ToString());

                        _tipo.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Destacar = dataReader["Destacar"].ToString() == "S";

                        _tipo.Contas = new List<Lalur.ValoresContas>();

                        query.Clear();
                        query.Append("Select id, idlalur, idlalurvalor, c.nroplano, c.codcontactb, valor, valorreal");
                        query.Append("     , c.classificador || ' ' || c.NomeConta nome");
                        query.Append("  From niff_ctb_lalurvalorctb v , ctbconta c");
                        query.Append(" Where v.idlalur = " + id);
                        query.Append("   And v.IdLalurValor = " + _tipo.Id);
                        query.Append("   And c.codcontactb = v.codcontactb");
                        query.Append("   And c.nroplano = v.nroplano");

                        executar = sessao.CreateQuery(query.ToString());

                        dataReader1 = executar.ExecuteQuery();

                        using (dataReader1)
                        {
                            while (dataReader1.Read())
                            {
                                Lalur.ValoresContas _val = new Lalur.ValoresContas();
                                _val.Existe = true;
                                _val.IdFormula = _tipo.IdFormula;
                                _val.IdLalurValor = _tipo.Id;
                                _val.Id = Convert.ToInt32(dataReader1["Id"].ToString());
                                _val.Valor = Math.Abs(Convert.ToDecimal(dataReader1["Valor"].ToString()));
                                _val.ValorReal = Convert.ToDecimal(dataReader1["valorreal"].ToString());
                                _val.Codigo = Convert.ToInt32(dataReader1["codcontactb"].ToString());
                                _val.NomeConta = dataReader1["Nome"].ToString();
                                _val.Plano = Convert.ToInt32(dataReader1["nroplano"].ToString());

                                _tipo.Contas.Add(_val);
                            }
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

        public decimal MesAnterior(int idFormula, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Lalur.Valores> _lista = new List<Lalur.Valores>();
            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;

            try
            {
                query.Append("Select c.id, c.idlalur, c.idformula, c.valor, c.Percentual, f.descricao, f.Ordem");
                query.Append("  From niff_ctb_lalurvalores C, niff_ctb_formulalalur f, niff_ctb_lalur l");
                query.Append(" Where f.Id = " + idFormula);
                query.Append("   and l.Referencia = " + referencia);
                query.Append("   and f.Id = c.IdFormula");
                query.Append("   and l.Id = c.IdLalur");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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

        public bool Gravar(Lalur.Apuracao item, List<Lalur.Valores> valores)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            int id = 0;
            int idvalor = 0;
            int idConta = 0;
            try
            {
                query.Clear();

                if (!item.Existe)
                {
                    query.Append("Select nvl(Max(id), 0) +1 next from Niff_Ctb_Lalur");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            id = Convert.ToInt32(dataReader["next"].ToString());
                    }
                    query.Clear();

                    query.Append("Insert into Niff_Ctb_Lalur");
                    query.Append(" ( id, idempresa, referencia, periodoencerrado)");
                    query.Append(" Values ( " + id);
                    query.Append("        , " + item.IdEmpresa);
                    query.Append("        , " + item.Referencia);
                    query.Append("        , '" + (item.Fechado ? "S" : "N") + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_Ctb_Lalur");
                    query.Append("   set periodoencerrado = '" + (item.Fechado ? "S" : "N") + "'");
                    query.Append(" where Id = " + item.Id);
                    id = item.Id;
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    foreach (var itemV in valores)
                    {
                        query.Clear();

                        if (!itemV.Existe)
                        {
                            query.Append("Select nvl(Max(id), 0) +1 next from niff_ctb_lalurvalores");
                            Query executar = sessao.CreateQuery(query.ToString());

                            dataReader = executar.ExecuteQuery();

                            using (dataReader)
                            {
                                if (dataReader.Read())
                                    idvalor = Convert.ToInt32(dataReader["next"].ToString());
                            }

                            query.Clear();

                            query.Append("Insert into niff_ctb_lalurvalores");
                            query.Append(" ( id, idlalur, idformula, valor, Percentual)");
                            query.Append(" Values ( " + idvalor);
                            query.Append("        , " + id);
                            query.Append("        , " + itemV.IdFormula);
                            query.Append("        , " + itemV.Valor.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("        , " + itemV.Percentual.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" )");
                        }
                        else
                        {
                            query.Append("Update niff_ctb_lalurvalores");
                            query.Append("   set valor = " + itemV.Valor.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , percentual = " + itemV.Percentual.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" where Id = " + itemV.Id);
                            idvalor = itemV.Id;
                        }

                        retorno = sessao.ExecuteSqlTransaction(query.ToString());

                        if (!retorno)
                            break;
                        else
                        {
                            if (itemV.Contas != null)
                            { 
                            foreach (var itemC in itemV.Contas)
                            {
                                query.Clear();

                                if (!itemC.Existe)
                                {
                                    query.Append("Select nvl(Max(id), 0) +1 next from niff_ctb_LalurValorCTB");
                                    Query executar = sessao.CreateQuery(query.ToString());

                                    dataReader = executar.ExecuteQuery();

                                    using (dataReader)
                                    {
                                        if (dataReader.Read())
                                            idConta = Convert.ToInt32(dataReader["next"].ToString());
                                    }

                                    query.Clear();

                                    query.Append("Insert into niff_ctb_LalurValorCTB");
                                    query.Append(" ( id, idlalur, idlalurvalor, nroplano, codcontactb, valor, valorreal)");
                                    query.Append(" Values ( " + idConta);
                                    query.Append("        , " + id);
                                    query.Append("        , " + idvalor);
                                    query.Append("        , " + itemC.Plano);
                                    query.Append("        , " + itemC.Codigo);
                                    query.Append("        , " + itemC.Valor.ToString().Replace(".", "").Replace(",", "."));
                                    query.Append("        , " + itemC.ValorReal.ToString().Replace(".", "").Replace(",", "."));
                                    query.Append(" )");
                                }
                                else
                                {
                                    query.Append("Update niff_ctb_LalurValorCTB");
                                    query.Append("   set valor = " + itemC.Valor.ToString().Replace(".", "").Replace(",", "."));
                                    query.Append("     , valorReal = " + itemC.ValorReal.ToString().Replace(".", "").Replace(",", "."));
                                    query.Append("     , nroplano = " + itemC.Plano);
                                    query.Append("     , codcontactb = " + itemC.Codigo);
                                    query.Append(" where Id = " + itemC.Id);
                                    idvalor = itemV.Id;
                                }

                                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                                if (!retorno)
                                    break;
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

        public bool ExcluirApuracao(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            try
            {
                query.Append("Delete niff_ctb_lalurvalorctb");
                query.Append(" Where idlalur = " + id);

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    query.Clear();
                    query.Append("Delete niff_ctb_lalurvalores");
                    query.Append(" Where idlalur = " + id);

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (retorno)
                    {
                        query.Clear();
                        query.Append("Delete Niff_Ctb_Lalur");
                        query.Append(" Where id = " + id);

                        retorno = sessao.ExecuteSqlTransaction(query.ToString());
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

        #endregion

        public decimal ValoresContabeis( string Empresa, string referencia, string referenciaFim, int IdFormula, int idEmpresa, bool consolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Round(Sum(nvl(SaldoAcumulado,0)),2) Valor");
                //query.Append("  from (Select Sum(s.vlcreditosaldo) - Sum(s.vldebitosaldo) saldoAcumulado");
                query.Append("  from (Select Sum(decode(mc.regra,'C-D', s.vlcreditosaldo - s.vldebitosaldo, s.vldebitosaldo - s.vlcreditosaldo)) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, niff_ctb_contasformulalalur mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo between '" + referencia + "' and '" + referenciaFim + "'");
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.idFormula = " + IdFormula);

                #region empresas
                if (consolidar)
                {
                    if (Empresa == "001/001")
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

                    if (Empresa == "008/001")
                        query.Append("           And s.codigoEmpresa = 8");

                    if (Empresa == "009/001")
                        query.Append("           And s.codigoEmpresa = 9");

                    if (Empresa == "011/001")
                        query.Append("           And s.codigoEmpresa = 11");

                    if (Empresa == "013/001")
                        query.Append("           And s.codigoEmpresa = 13");

                    if (Empresa == "014/001")
                        query.Append("           And s.codigoEmpresa = 14");

                    if (Empresa == "015/001")
                        query.Append("           And s.codigoEmpresa = 15");

                    if (Empresa == "026/001")
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

                    if (Empresa == "008/001")
                        query.Append("           And s.codigoEmpresa = 8 and s.CodigoFl = 1");

                    if (Empresa == "009/001")
                        query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                    if (Empresa == "011/001")
                        query.Append("           And s.codigoEmpresa = 11 and s.CodigoFl = 1");

                    if (Empresa == "013/001")
                        query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                    if (Empresa == "014/001")
                        query.Append("           And s.codigoEmpresa = 14 and s.CodigoFl = 1");

                    if (Empresa == "015/001")
                        query.Append("           And s.codigoEmpresa = 15 and s.CodigoFl = 1");

                    if (Empresa == "026/001")
                        query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                    if (Empresa == "026/003")
                        query.Append("           And s.codigoEmpresa = 26 and or s.CodigoFl = 3 ");

                    if (Empresa == "029/001")
                        query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1");

                }
                #endregion

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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

        public List<Lalur.ValoresContas> ValoresContabeisDetalhado(int id, string Empresa, string referencia, string referenciaFim, int IdFormula, int idEmpresa, bool consolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<Lalur.ValoresContas> _lista = new List<Lalur.ValoresContas>();

            try
            {
                query.Append("Select Round(Sum(nvl(SaldoAcumulado,0)),2) Valor, codcontactb, Nome, nroplano");
                //query.Append("  from (Select Sum(s.vlcreditosaldo) - Sum(s.vldebitosaldo) saldoAcumulado");
                query.Append("  from (Select Sum(decode(mc.regra,'C-D', s.vlcreditosaldo - s.vldebitosaldo, s.vldebitosaldo - s.vlcreditosaldo)) saldoAcumulado");
                query.Append("     , s.codcontactb, c.classificador || ' ' || c.NomeConta nome, s.nroplano");
                query.Append("          from Ctbsaldo s, ctbconta c, niff_ctb_contasformulalalur mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo between '" + referencia + "' and '" + referenciaFim + "'");
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.idFormula = " + IdFormula);

                #region empresas
                if (consolidar)
                {
                    if (Empresa == "001/001")
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

                    if (Empresa == "008/001")
                        query.Append("           And s.codigoEmpresa = 8");

                    if (Empresa == "009/001")
                        query.Append("           And s.codigoEmpresa = 9");

                    if (Empresa == "011/001")
                        query.Append("           And s.codigoEmpresa = 11");

                    if (Empresa == "013/001")
                        query.Append("           And s.codigoEmpresa = 13");

                    if (Empresa == "014/001")
                        query.Append("           And s.codigoEmpresa = 14");

                    if (Empresa == "015/001")
                        query.Append("           And s.codigoEmpresa = 15");

                    if (Empresa == "026/001")
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

                    if (Empresa == "008/001")
                        query.Append("           And s.codigoEmpresa = 8 and s.CodigoFl = 1");

                    if (Empresa == "009/001")
                        query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                    if (Empresa == "011/001")
                        query.Append("           And s.codigoEmpresa = 11 and s.CodigoFl = 1");

                    if (Empresa == "013/001")
                        query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                    if (Empresa == "014/001")
                        query.Append("           And s.codigoEmpresa = 14 and s.CodigoFl = 1");

                    if (Empresa == "015/001")
                        query.Append("           And s.codigoEmpresa = 15 and s.CodigoFl = 1");

                    if (Empresa == "026/001")
                        query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                    if (Empresa == "026/003")
                        query.Append("           And s.codigoEmpresa = 26 and or s.CodigoFl = 3 ");

                    if (Empresa == "029/001")
                        query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1");

                }
                #endregion

                query.Append("  group by s.codcontactb, c.classificador || ' ' || c.NomeConta, s.nroplano) ");
                query.Append("  group by  codcontactb, Nome, nroplano");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Lalur.ValoresContas _val = new Lalur.ValoresContas();
                        _val.IdFormula = IdFormula;
                        _val.IdLalurValor = id;
                        _val.Valor =  Math.Abs(Convert.ToDecimal(dataReader["Valor"].ToString()));
                        _val.ValorReal = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _val.Codigo = Convert.ToInt32(dataReader["codcontactb"].ToString());
                        _val.NomeConta = dataReader["Nome"].ToString();
                        _val.Plano = Convert.ToInt32(dataReader["nroplano"].ToString());

                        _lista.Add(_val);
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
