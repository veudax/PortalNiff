using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class FinanceiroDAO
    {
        IDataReader dataReader;
        IDataReader auxDataReader;

        #region Tipo de Despesa e Receita do Globus
        public List<Financeiro.DespesaReceitaGlobus> Listar(string tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.DespesaReceitaGlobus> _lista = new List<Financeiro.DespesaReceitaGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (tipo == "D")
                {
                    query.Append("Select codtpdespesa, desctpdespesa, classificador, aceitalancamento");
                    query.Append("  from Cpgtpdes");
                    query.Append(" Where aceitalancamento = 'S'");

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            Financeiro.DespesaReceitaGlobus _tipo = new Financeiro.DespesaReceitaGlobus();
                            _tipo.Existe = true;
                            _tipo.Codigo = dataReader["codtpdespesa"].ToString();

                            _tipo.Descricao = dataReader["desctpdespesa"].ToString();
                            _tipo.Classificador = dataReader["classificador"].ToString();
                            _tipo.AceitaLancamento = dataReader["aceitalancamento"].ToString() == "S";

                            _lista.Add(_tipo);
                        }
                    }
                }
                else
                {
                    query.Append("Select codtpreceita, desctpreceita, classificador, aceitalancamento");
                    query.Append("  from crctprec");
                    query.Append(" Where aceitalancamento = 'S'");

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            Financeiro.DespesaReceitaGlobus _tipo = new Financeiro.DespesaReceitaGlobus();
                            _tipo.Existe = true;
                            _tipo.Codigo = dataReader["codtpreceita"].ToString();

                            _tipo.Descricao = dataReader["desctpreceita"].ToString();
                            _tipo.Classificador = dataReader["classificador"].ToString();
                            _tipo.AceitaLancamento = dataReader["aceitalancamento"].ToString() == "S";
                            _lista.Add(_tipo);
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

        public Financeiro.DespesaReceitaGlobus Consultar(string codigo, string tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.DespesaReceitaGlobus _tipo = new Financeiro.DespesaReceitaGlobus();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (tipo == "D")
                {
                    query.Append("Select codtpdespesa, desctpdespesa, classificador, aceitalancamento");
                    query.Append("  from Cpgtpdes");
                    query.Append(" Where codtpdespesa = " + codigo);

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            _tipo.Existe = true;
                            _tipo.Codigo = dataReader["codtpdespesa"].ToString();

                            _tipo.Descricao = dataReader["desctpdespesa"].ToString();
                            _tipo.Classificador = dataReader["classificador"].ToString();
                            _tipo.AceitaLancamento = dataReader["aceitalancamento"].ToString() == "S";
                        }
                    }
                }
                else
                {
                    query.Append("Select codtpreceita, desctpreceita, classificador, aceitalancamento");
                    query.Append("  from crctprec");
                    query.Append(" Where codtpreceita = " + codigo);

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            _tipo.Existe = true;
                            _tipo.Codigo = dataReader["codtpreceita"].ToString();

                            _tipo.Descricao = dataReader["desctpreceita"].ToString();
                            _tipo.Classificador = dataReader["classificador"].ToString();
                            _tipo.AceitaLancamento = dataReader["aceitalancamento"].ToString() == "S";
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
            return _tipo;
        }

        public Financeiro.ContasContabeisDespesasReceitaGlobus Consultar(string codigo, string tipo, int plano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ContasContabeisDespesasReceitaGlobus _tipo = new Financeiro.ContasContabeisDespesasReceitaGlobus();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (tipo == "D")
                {
                    query.Append("Select d.codtpdespesa, d.nroplano, d.codcontactb, d.permitealterar, d.codcontactb_forn");
                    query.Append("     , d.codcontactbpis, d.codcontactbcofins,  d.codcentrocustopis, d.codcentrocustocofins");
                    query.Append("     , c.nomeconta");
                    query.Append("  from cpgtpdes_ctbconta d, ctbconta c");
                    query.Append(" Where d.codtpdespesa = " + codigo);
                    query.Append("   and d.nroPlano = " + plano);
                    query.Append("   and d.nroPlano = c.nroPlano");
                    query.Append("   and d.codcontactb = c.codcontactb");

                    Query executar = sessao.CreateQuery(query.ToString());

                    auxDataReader = executar.ExecuteQuery();

                    using (auxDataReader)
                    {
                        while (auxDataReader.Read())
                        {
                            _tipo.Existe = true;
                            _tipo.Codigo = auxDataReader["codtpdespesa"].ToString();

                            try
                            {
                                _tipo.CentroCustoPis = Convert.ToInt32(auxDataReader["codcentrocustopis"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.CentroCustoCofins = Convert.ToInt32(auxDataReader["codcentrocustocofins"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.ContaContabilCofins = Convert.ToInt32(auxDataReader["codcontactbcofins"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.ContaContabilPis = Convert.ToInt32(auxDataReader["codcontactbpis"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.ContaContabilFornecedor = Convert.ToInt32(auxDataReader["codcontactb_forn"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.ContaContabil = Convert.ToInt32(auxDataReader["codcontactb"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.Plano = Convert.ToInt32(auxDataReader["nroplano"].ToString());
                            }
                            catch { }

                            _tipo.NomeConta = auxDataReader["NomeConta"].ToString();
                        }
                    }
                }
                else
                {
                    query.Append("Select d.codtpreceita, d.nroplano, d.codcontactb, d.permitealterar, d.codcusto");
                    query.Append("  from crctprec_ctbconta d, ctbconta c");
                    query.Append(" Where d.codtpreceita = " + codigo);
                    query.Append("   and d.nroPlano = " + plano);
                    query.Append("   and d.nroPlano = c.nroPlano");
                    query.Append("   and d.codcontactb = c.codcontactb");

                    Query executar = sessao.CreateQuery(query.ToString());

                    auxDataReader = executar.ExecuteQuery();

                    using (auxDataReader)
                    {
                        while (auxDataReader.Read())
                        {
                            _tipo.Existe = true;
                            _tipo.Codigo = auxDataReader["codtpreceita"].ToString();

                            try
                            {
                                _tipo.CentroCusto = Convert.ToInt32(auxDataReader["codcusto"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.ContaContabil = Convert.ToInt32(auxDataReader["codcontactb"].ToString());
                            }
                            catch { }

                            try
                            {
                                _tipo.Plano = Convert.ToInt32(auxDataReader["nroplano"].ToString());
                            }
                            catch { }
                            _tipo.NomeConta = auxDataReader["NomeConta"].ToString();

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
            return _tipo;
        }
        #endregion

        #region Colunas
        public List<Financeiro.Colunas> Listar(bool ApenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.Colunas> _lista = new List<Financeiro.Colunas>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, nome, tipo, transferencia, ativo, TipoOperacaoLinha, Origem");
                query.Append("  from niff_fin_colunas");

                if (ApenasAtivos)
                    query.Append(" Where Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Financeiro.Colunas _tipo = new Financeiro.Colunas();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.Tipo = dataReader["Tipo"].ToString();
                        _tipo.Transferencia = dataReader["Transferencia"].ToString();
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.TipoOperacao = dataReader["TipoOperacaoLinha"].ToString();
                        _tipo.Origem = dataReader["Origem"].ToString();

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

        public Financeiro.Colunas Consultar(int Id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Colunas _tipo = new Financeiro.Colunas();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, nome, tipo, transferencia, ativo, TipoOperacaoLinha, Origem");
                query.Append("  from niff_fin_colunas");
                query.Append(" Where id = " + Id);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.Tipo = dataReader["Tipo"].ToString();
                        _tipo.Transferencia = dataReader["Transferencia"].ToString();
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.TipoOperacao = dataReader["TipoOperacaoLinha"].ToString();
                        _tipo.Origem = dataReader["Origem"].ToString();

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

        public bool Grava(Financeiro.Colunas _dados)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!_dados.Existe)
                {

                    query.Clear();
                    query.Append("Insert into niff_fin_colunas");
                    query.Append("   (id, nome, tipo, transferencia, ativo, TipoOperacaoLinha, Origem");
                    query.Append("  ) Values ( " + _dados.Id);
                    query.Append(", '" + _dados.Nome + "'");
                    query.Append(", '" + _dados.Tipo + "'");
                    query.Append(", '" + _dados.Transferencia + "'");
                    query.Append(", '" + (_dados.Ativo ? "S" : "N") + "'");
                    query.Append(", '" + _dados.TipoOperacao + "'");
                    query.Append(", '" + _dados.Origem + "'");

                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update niff_fin_colunas");
                    query.Append("   set Nome = '" + _dados.Nome + "'");
                    query.Append("     , Ativo = '" + (_dados.Ativo ? "S" : "N") + "'");
                    query.Append("     , Tipo = '" + _dados.Tipo + "'");
                    query.Append("     , transferencia = '" + _dados.Transferencia + "'");
                    query.Append("     , TipoOperacaoLinha = '" + _dados.TipoOperacao + "'");
                    query.Append("     , Origem = '" + _dados.Origem + "'");                                     
                    
                    query.Append(" Where Id = " + _dados.Id);
                }

                return sessao.ExecuteSqlTransaction(query.ToString(), null);
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

        public bool Exclui(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete niff_fin_colunas");
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

        public int Proximo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Nvl(Max(id),0) +1 next From niff_fin_colunas");
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

        #region Bancos
        public List<Financeiro.Bancos> ListarBancos(int empresa, bool ApenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.Bancos> _lista = new List<Financeiro.Bancos>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, nome, saldoinicial, ativo, codigo");
                query.Append("     , CodBanco, CodAgencia, CodConta");
                query.Append("     , CodBancoCartoes, CodAgenciaCartoes, CodContaCartoes");
                query.Append("     , Consolidar, idEmpresaConsolidar, IdBancoConsolidar");
                query.Append("  from niff_fin_bancos");
                query.Append(" Where idEmpresa = " + empresa);

                if (ApenasAtivos)
                    query.Append("  And Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Financeiro.Bancos _tipo = new Financeiro.Bancos();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());
                        try
                        {
                            _tipo.CodigoBanco = Convert.ToInt32(dataReader["CodBanco"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CodigoAgencia = Convert.ToInt32(dataReader["CodAgencia"].ToString());
                        }
                        catch { }

                        _tipo.CodigoConta = dataReader["CodConta"].ToString();

                        try
                        {
                            _tipo.CodigoBancoCartoes = Convert.ToInt32(dataReader["CodBancoCartoes"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CodigoAgenciaCartoes = Convert.ToInt32(dataReader["CodAgenciaCartoes"].ToString());
                        }
                        catch { }

                        _tipo.CodigoContaCartoes = dataReader["CodContaCartoes"].ToString();

                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.SaldoInicial = Convert.ToDecimal(dataReader["saldoinicial"].ToString());
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";

                        _tipo.Consolidar = dataReader["Consolidar"].ToString() == "S";

                        try
                        {
                            _tipo.IdEmpresaConsolidado = Convert.ToInt32(dataReader["idEmpresaConsolidar"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IdBancoConsolidado = Convert.ToInt32(dataReader["IdBancoConsolidar"].ToString());
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

        public Financeiro.Bancos ConsultarBancos(int codigo, int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Bancos _tipo = new Financeiro.Bancos();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select b.id, b.idempresa, b.nome, b.saldoinicial, b.ativo, b.codigo");
                query.Append("     , b.CodBanco, b.CodAgencia, b.CodConta");
                query.Append("     , b.CodBancoCartoes, b.CodAgenciaCartoes, b.CodContaCartoes");
                query.Append("     , b.Consolidar, b.idEmpresaConsolidar, b.IdBancoConsolidar");
                query.Append("     , e.codigoglobus");
                query.Append("  from niff_fin_bancos b, Niff_Chm_Empresas e");
                query.Append(" Where b.codigo = " + codigo);
                query.Append("   and b.IdEmpresa = " + empresa);
                query.Append("   and b.idempresaconsolidar = e.Idempresa(+)");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());

                        try
                        {
                            _tipo.CodigoBanco = Convert.ToInt32(dataReader["CodBanco"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CodigoAgencia = Convert.ToInt32(dataReader["CodAgencia"].ToString());
                        }
                        catch { }

                        _tipo.CodigoConta = dataReader["CodConta"].ToString();
                        try
                        {
                            _tipo.CodigoBancoCartoes = Convert.ToInt32(dataReader["CodBancoCartoes"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CodigoAgenciaCartoes = Convert.ToInt32(dataReader["CodAgenciaCartoes"].ToString());
                        }
                        catch { }

                        _tipo.CodigoContaCartoes = dataReader["CodContaCartoes"].ToString();
                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        
                        _tipo.Consolidar = dataReader["Consolidar"].ToString() == "S";

                        try
                        {
                            _tipo.IdEmpresaConsolidado = Convert.ToInt32(dataReader["idEmpresaConsolidar"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IdBancoConsolidado = Convert.ToInt32(dataReader["IdBancoConsolidar"].ToString());
                        }
                        catch { }

                        _tipo.SaldoInicial = Convert.ToDecimal(dataReader["saldoinicial"].ToString());

                        // não usa mais
                        //if (_tipo.Consolidar)
                        //{
                        //    _tipo.CodigoEmpresaGlobus = dataReader["codigoglobus"].ToString();

                        //    try
                        //    {
                        //        _tipo.SaldoInicial = ConsultarBancos(_tipo.IdBancoConsolidado).SaldoInicial;
                        //    }
                        //    catch { }
                        //}                           

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

        public Financeiro.Bancos ConsultarBancos(int Id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Bancos _tipo = new Financeiro.Bancos();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select b.id, b.idempresa, b.nome, b.saldoinicial, b.ativo, b.codigo");
                query.Append("     , b.CodBanco, b.CodAgencia, b.CodConta");
                query.Append("     , b.CodBancoCartoes, b.CodAgenciaCartoes, b.CodContaCartoes");
                query.Append("     , b.Consolidar, b.idEmpresaConsolidar, b.IdBancoConsolidar");
                query.Append("     , e.codigoglobus");
                query.Append("  from niff_fin_bancos b, Niff_Chm_Empresas e");
                query.Append(" Where b.id = " + Id);
                query.Append("   and b.idempresaconsolidar = e.Idempresa(+)");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());
                        try
                        {
                            _tipo.CodigoBanco = Convert.ToInt32(dataReader["CodBanco"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.CodigoAgencia = Convert.ToInt32(dataReader["CodAgencia"].ToString());
                        }
                        catch { }

                        _tipo.CodigoConta = dataReader["CodConta"].ToString();

                        try
                        {
                            _tipo.CodigoBancoCartoes = Convert.ToInt32(dataReader["CodBancoCartoes"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.CodigoAgenciaCartoes = Convert.ToInt32(dataReader["CodAgenciaCartoes"].ToString());
                        }
                        catch { }

                        _tipo.CodigoContaCartoes = dataReader["CodContaCartoes"].ToString();

                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.SaldoInicial = Convert.ToDecimal(dataReader["saldoinicial"].ToString());
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";

                        _tipo.Consolidar = dataReader["Consolidar"].ToString() == "S";

                        _tipo.CodigoEmpresaGlobus = dataReader["codigoglobus"].ToString();

                        try
                        {
                            _tipo.IdEmpresaConsolidado = Convert.ToInt32(dataReader["idEmpresaConsolidar"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IdBancoConsolidado = Convert.ToInt32(dataReader["IdBancoConsolidar"].ToString());
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

        public int ProximoBanco(int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Nvl(Max(codigo),0) +1 next From niff_fin_bancos");
                query.Append(" Where idEmpresa = " + empresa);
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

        public bool GravarBancos(Financeiro.Bancos _dados, List<Financeiro.ColunasDoBanco> _colunas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            int id = 0;
            int idcoluna = 0;
            try
            {
                if (!_dados.Existe)
                {
                    query.Clear();
                    query.Append("Select Nvl(Max(id),0) +1 next From niff_fin_bancos");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            id = Convert.ToInt32(dataReader["next"].ToString());
                    }

                    query.Clear();
                    query.Append("Insert into niff_fin_bancos");
                    query.Append("   (id, idempresa, nome, saldoinicial, ativo, codigo, CodBanco, CodAgencia, CodConta");
                    query.Append("     , Consolidar, idEmpresaConsolidar, IdBancoConsolidar");
                    query.Append("     , CodBancoCartoes, CodAgenciaCartoes, CodContaCartoes");
                    query.Append("  ) Values ( " + id);
                    query.Append(", " + _dados.IdEmpresa);
                    query.Append(", '" + _dados.Nome + "'");
                    query.Append(", " + _dados.SaldoInicial.ToString().Replace(".", "").Replace(",", ".")); 
                    query.Append(", '" + (_dados.Ativo ? "S" : "N") + "'");
                    query.Append(", " + _dados.Codigo);
                    query.Append(", " + _dados.CodigoBanco);
                    query.Append(", " + _dados.CodigoAgencia);
                    query.Append(", '" + _dados.CodigoConta + "'");

                    query.Append(", '" + (_dados.Consolidar ? "S" : "N") + "'");

                    if (!_dados.Consolidar)
                        query.Append(", null, null");
                    else
                    {
                        query.Append(", " + _dados.IdEmpresaConsolidado);
                        query.Append(", " + _dados.IdBancoConsolidado);
                    }
                    
                    if (_dados.CodigoBancoCartoes == 0)
                        query.Append(", null, null, null");
                    else
                    {
                        query.Append(", " + _dados.CodigoBancoCartoes);
                        query.Append(", " + _dados.CodigoAgenciaCartoes);
                        query.Append(", '" + _dados.CodigoContaCartoes + "'");
                    }
                    query.Append(") ");
                }
                else
                {
                    id = _dados.Id;
                    query.Clear();
                    query.Append("Update niff_fin_bancos");
                    query.Append("   set Nome = '" + _dados.Nome + "'");
                    query.Append("     , IdEmpresa = " + _dados.IdEmpresa);
                    query.Append("     , Ativo = '" + (_dados.Ativo ? "S" : "N") + "'");
                    query.Append("     , SaldoInicial = " + _dados.SaldoInicial.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Codbanco = " + _dados.CodigoBanco);
                    query.Append("     , CodAgencia = " + _dados.CodigoAgencia);
                    query.Append("     , CodConta = '" + _dados.CodigoConta + "'");

                    query.Append("     , Consolidar = '" + (_dados.Consolidar ? "S" : "N") + "'");

                    if (_dados.Consolidar)                        
                    {
                        query.Append("     , IdEmpresaConsolidar = " + _dados.IdEmpresaConsolidado);
                        query.Append("     , IdBancoConsolidar = " + _dados.IdBancoConsolidado);
                    }
                    else
                    {
                        query.Append("     , IdEmpresaConsolidar = null " );
                        query.Append("     , IdBancoConsolidar = null ");
                    }

                    if (_dados.CodigoBancoCartoes == 0)
                    {
                        query.Append(", CodBancoCartoes = null");
                        query.Append(", CodAgenciaCartoes = null");
                        query.Append(", CodContaCartoes = null");
                    }
                    else
                    {
                        query.Append(", CodBancoCartoes = " + _dados.CodigoBancoCartoes);
                        query.Append(", CodAgenciaCartoes = " + _dados.CodigoAgenciaCartoes);
                        query.Append(", CodContaCartoes = '" + _dados.CodigoContaCartoes + "'");
                    }

                    query.Append(" Where Id = " + _dados.Id);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString(), null);

                if (retorno)
                {
                    foreach (var item in _colunas.GroupBy(g => new { g.Id, g.IdColuna, g.Nome, g.Ativo, g.Existe }))
                    {
                        query.Clear();
                        if (!item.Key.Existe)
                        {
                            query.Clear();
                            query.Append("Select Nvl(Max(id),0) +1 next From niff_fin_colunasbanco");
                            Query executar = sessao.CreateQuery(query.ToString());

                            dataReader = executar.ExecuteQuery();

                            using (dataReader)
                            {
                                if (dataReader.Read())
                                    idcoluna = Convert.ToInt32(dataReader["next"].ToString());
                            }

                            query.Clear();
                            query.Append("Insert into niff_fin_colunasbanco (id, idbanco, idcoluna, ativo)");
                            query.Append(" Values ( " + idcoluna);
                            query.Append("     , " + id);
                            query.Append("     , " + item.Key.IdColuna);
                            query.Append("     , '" + (item.Key.Ativo ? "S" : "N") + "'");
                            query.Append(" )");
                        }
                        else
                        {
                            idcoluna = item.Key.Id;
                            query.Append("Update niff_fin_colunasbanco ");
                            query.Append("   set Ativo = '" + (item.Key.Ativo ? "S" : "N") + "'");
                            query.Append(" Where Id = " + item.Key.Id);
                        }

                        retorno = sessao.ExecuteSqlTransaction(query.ToString(), null);

                        if (!retorno)
                            break;
                        else
                        {
                            foreach (var itemT in _colunas.GroupBy(g => new { g.Id, g.IdColuna, g.TipoCodigo, g.TipoNome, g.Tipo, g.TipoExiste })
                                                          .Where(w => w.Key.IdColuna == item.Key.IdColuna))
                            {
                                query.Clear();
                                if (!itemT.Key.TipoExiste)
                                {
                                    query.Append("Insert into Niff_Fin_Despreccolunas (id, idColBanco, CodTpDespesa, CodTpReceita)");
                                    query.Append(" Values ((Select Nvl(max(id),0) +1 from Niff_Fin_Despreccolunas) ");
                                    query.Append("     , " + idcoluna);
                                    query.Append("     , " + ((itemT.Key.Tipo == "EN" || itemT.Key.Tipo == "TE") ? "null" : "'" + itemT.Key.TipoCodigo + "'"));
                                    query.Append("     , " + ((itemT.Key.Tipo == "EN" || itemT.Key.Tipo == "TE") ? "'" + itemT.Key.TipoCodigo + "'" : "null"));
                                    query.Append(" )");

                                    retorno = sessao.ExecuteSqlTransaction(query.ToString(), null);

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

        public bool ExcluirBancos(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Delete NIFF_FIN_DespRecColunas");
                query.Append(" Where Idcolbanco in (Select Id from NIFF_FIN_ColunasBanco where IdBanco = " + id + ")");

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;
                else
                {
                    query.Clear();
                    query.Append("Delete NIFF_FIN_ColunasBanco");
                    query.Append(" Where IdBanco = " + id );

                    if (!sessao.ExecuteSqlTransaction(query.ToString()))
                        return false;
                    else
                    {
                        query.Clear();

                        query.Append("Delete niff_fin_bancos");
                        query.Append(" Where id = " + id);

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

        public bool ExcluirTipos(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Delete NIFF_FIN_DespRecColunas");
                query.Append(" Where id = " + id );

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

        public bool ExcluirColunas(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Delete NIFF_FIN_DespRecColunas");
                query.Append(" Where Idcolbanco = " + id);

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;

                query.Clear();
                query.Append("Delete niff_fin_colunasbanco");
                query.Append(" Where id = " + id );

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

        public List<Financeiro.ColunasDoBanco> ListarColunasDoBanco(int banco, bool ApenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.ColunasDoBanco> _lista = new List<Financeiro.ColunasDoBanco>();
            Publicas.mensagemDeErro = string.Empty;
            Financeiro.ColunasDoBanco _tipo;

            try
            {
                query.Append("Select b.id, b.idbanco, b.idcoluna, b.ativo, c.Nome, C.TIPO "); 
                query.Append("  from NIFF_FIN_ColunasBanco b, Niff_FIN_Colunas c");
                query.Append(" Where Idbanco = " + banco);
                query.Append("   And b.idcoluna = c.Id");

                if (ApenasAtivos)
                    query.Append(" And c.Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {

                        List<Financeiro.DespesasReceitasDasColunasDoBanco> _listaAux = new List<Financeiro.DespesasReceitasDasColunasDoBanco>();
                        try
                        {
                            _listaAux = ListarTipoAssociados(Convert.ToInt32(dataReader["Id"].ToString()), dataReader["Tipo"].ToString());

                            foreach (var item in _listaAux)
                            {
                                _tipo = new Financeiro.ColunasDoBanco();
                                     
                                _tipo.Existe = true;
                                _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                                _tipo.IdBanco = Convert.ToInt32(dataReader["IdBanco"].ToString());
                                _tipo.IdColuna = Convert.ToInt32(dataReader["IdColuna"].ToString());
                                _tipo.Nome = dataReader["nome"].ToString();
                                _tipo.Tipo = dataReader["Tipo"].ToString();
                                _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                                _tipo.IdAssociado = item.Id;
                                _tipo.TipoExiste = item.Existe;

                                _tipo.Selecionado = item.Selecionado;

                                if (_tipo.Tipo == "EN" || _tipo.Tipo == "TE")
                                {
                                    _tipo.TipoCodigo = item.CodigoTipoReceita;
                                    _tipo.TipoNome = item.NomeReceita;
                                }
                                else
                                {
                                    _tipo.TipoCodigo = item.CodigoTipoDespesa;
                                    _tipo.TipoNome = item.NomeDespesa;
                                }
                                _lista.Add(_tipo);
                            }
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
            return _lista;
        }

        public List<Financeiro.DespesasReceitasDasColunasDoBanco> ListarTipoAssociados(int id, string tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.DespesasReceitasDasColunasDoBanco> _lista = new List<Financeiro.DespesasReceitasDasColunasDoBanco>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select 'S' Selecionado, c.Id, c.Idcolbanco, c.Codtpdespesa, d.desctpdespesa, d.classificador ClassD");
                query.Append("     , c.codtpreceita, r.desctpreceita, r.classificador ClassR"); 
                query.Append("  from Niff_Fin_Despreccolunas c, Cpgtpdes d, crctprec r");
                query.Append(" Where idcolbanco = " + id);
                query.Append("   And c.codtpdespesa = d.codtpdespesa(+)  ");
                query.Append("   And c.codtpreceita = r.codtpreceita(+)  ");
                
                Query executar = sessao.CreateQuery(query.ToString());

                auxDataReader = executar.ExecuteQuery();

                using (auxDataReader)
                {
                    while (auxDataReader.Read())
                    {
                        Financeiro.DespesasReceitasDasColunasDoBanco _tipo = new Financeiro.DespesasReceitasDasColunasDoBanco();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(auxDataReader["Id"].ToString());
                        _tipo.Selecionado = auxDataReader["Selecionado"].ToString() == "S";
                        _tipo.IdColunaBanco = Convert.ToInt32(auxDataReader["Idcolbanco"].ToString());
                        _tipo.CodigoTipoDespesa = auxDataReader["codtpdespesa"].ToString();
                        _tipo.CodigoTipoReceita = auxDataReader["codtpreceita"].ToString();
                        _tipo.NomeDespesa = auxDataReader["ClassD"].ToString() + " - " + auxDataReader["desctpdespesa"].ToString();
                        _tipo.NomeReceita = auxDataReader["ClassR"].ToString() + " - " + auxDataReader["desctpreceita"].ToString();

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
        #endregion

        #region Variaveis
        public List<Financeiro.Variaveis> Listar(int empresa, bool ApenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.Variaveis> _lista = new List<Financeiro.Variaveis>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, idcoluna, calculomediapor, qtderetroativo, feriadoporano, consideraemendasadado, feriaapenasdinheiro");
                query.Append("     , reduzir, aumentar, percentualreduzir, percentualaumentar, nome, ativo, codigo, CalcularFinaisDeSemana");
                query.Append("  from Niff_Fin_Variaveis");
                query.Append(" where IdEmpresa = " + empresa);

                if (ApenasAtivos)
                    query.Append("  and Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Financeiro.Variaveis _tipo = new Financeiro.Variaveis();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdColuna = Convert.ToInt32(dataReader["IdColuna"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.CalculoPor = dataReader["calculomediapor"].ToString();

                        _tipo.Quantidade = Convert.ToInt32(dataReader["qtderetroativo"].ToString());
                        _tipo.PercentualReduzir = Convert.ToInt32(dataReader["percentualreduzir"].ToString());
                        _tipo.PercentualAumentar = Convert.ToInt32(dataReader["percentualaumentar"].ToString());
                        
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.FeriadoPorAno = dataReader["feriadoporano"].ToString() == "S";
                        _tipo.EmendaSabado = dataReader["consideraemendasadado"].ToString() == "S";
                        _tipo.FeriaDinheiro = dataReader["feriaapenasdinheiro"].ToString() == "S";
                        _tipo.Reduzir = dataReader["reduzir"].ToString() == "S";
                        _tipo.Aumentar = dataReader["aumentar"].ToString() == "S";
                        _tipo.CalcularFinaisDeSemana = dataReader["CalcularFinaisDeSemana"].ToString() == "S";                        

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

        public List<Financeiro.Variaveis> Listar(int empresa, int codigo, bool ApenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.Variaveis> _lista = new List<Financeiro.Variaveis>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, idcoluna, calculomediapor, qtderetroativo, feriadoporano, consideraemendasadado, feriaapenasdinheiro");
                query.Append("     , reduzir, aumentar, percentualreduzir, percentualaumentar, nome, ativo, codigo, CalcularFinaisDeSemana");
                query.Append("  from Niff_Fin_Variaveis");
                query.Append(" where IdEmpresa = " + empresa);
                query.Append("   and Codigo = " + codigo);

                if (ApenasAtivos)
                    query.Append("  and Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Financeiro.Variaveis _tipo = new Financeiro.Variaveis();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdColuna = Convert.ToInt32(dataReader["IdColuna"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.CalculoPor = dataReader["calculomediapor"].ToString();

                        _tipo.Quantidade = Convert.ToInt32(dataReader["qtderetroativo"].ToString());
                        _tipo.PercentualReduzir = Convert.ToInt32(dataReader["percentualreduzir"].ToString());
                        _tipo.PercentualAumentar = Convert.ToInt32(dataReader["percentualaumentar"].ToString());

                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.FeriadoPorAno = dataReader["feriadoporano"].ToString() == "S";
                        _tipo.EmendaSabado = dataReader["consideraemendasadado"].ToString() == "S";
                        _tipo.FeriaDinheiro = dataReader["feriaapenasdinheiro"].ToString() == "S";
                        _tipo.Reduzir = dataReader["reduzir"].ToString() == "S";
                        _tipo.Aumentar = dataReader["aumentar"].ToString() == "S";
                        _tipo.CalcularFinaisDeSemana = dataReader["CalcularFinaisDeSemana"].ToString() == "S";

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

        public Financeiro.Variaveis Consultar(int empresa, int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Variaveis _tipo = new Financeiro.Variaveis();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, idcoluna, calculomediapor, qtderetroativo, feriadoporano, consideraemendasadado, feriaapenasdinheiro");
                query.Append("     , reduzir, aumentar, percentualreduzir, percentualaumentar, nome, ativo, codigo, CalcularFinaisDeSemana");
                query.Append("  from Niff_Fin_Variaveis");
                query.Append(" Where idEmpresa = " + empresa);
                query.Append("   and Codigo = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdColuna = Convert.ToInt32(dataReader["IdColuna"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.CalculoPor = dataReader["calculomediapor"].ToString();

                        _tipo.Quantidade = Convert.ToInt32(dataReader["qtderetroativo"].ToString());
                        _tipo.PercentualReduzir = Convert.ToInt32(dataReader["percentualreduzir"].ToString());
                        _tipo.PercentualAumentar = Convert.ToInt32(dataReader["percentualaumentar"].ToString());

                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.FeriadoPorAno = dataReader["feriadoporano"].ToString() == "S";
                        _tipo.EmendaSabado = dataReader["consideraemendasadado"].ToString() == "S";
                        _tipo.FeriaDinheiro = dataReader["feriaapenasdinheiro"].ToString() == "S";
                        _tipo.Reduzir = dataReader["reduzir"].ToString() == "S";
                        _tipo.Aumentar = dataReader["aumentar"].ToString() == "S";
                        _tipo.CalcularFinaisDeSemana = dataReader["CalcularFinaisDeSemana"].ToString() == "S";
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

        public Financeiro.Variaveis Consultar(int empresa, int codigo, int idColuna)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Variaveis _tipo = new Financeiro.Variaveis();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, idcoluna, calculomediapor, qtderetroativo, feriadoporano, consideraemendasadado, feriaapenasdinheiro");
                query.Append("     , reduzir, aumentar, percentualreduzir, percentualaumentar, nome, ativo, codigo, CalcularFinaisDeSemana");
                query.Append("  from Niff_Fin_Variaveis");
                query.Append(" Where idEmpresa = " + empresa);
                query.Append("   and Codigo = " + codigo);
                query.Append("   and idcoluna = " + idColuna);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdColuna = Convert.ToInt32(dataReader["IdColuna"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.CalculoPor = dataReader["calculomediapor"].ToString();

                        _tipo.Quantidade = Convert.ToInt32(dataReader["qtderetroativo"].ToString());
                        _tipo.PercentualReduzir = Convert.ToInt32(dataReader["percentualreduzir"].ToString());
                        _tipo.PercentualAumentar = Convert.ToInt32(dataReader["percentualaumentar"].ToString());

                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.FeriadoPorAno = dataReader["feriadoporano"].ToString() == "S";
                        _tipo.EmendaSabado = dataReader["consideraemendasadado"].ToString() == "S";
                        _tipo.FeriaDinheiro = dataReader["feriaapenasdinheiro"].ToString() == "S";
                        _tipo.Reduzir = dataReader["reduzir"].ToString() == "S";
                        _tipo.Aumentar = dataReader["aumentar"].ToString() == "S";
                        _tipo.CalcularFinaisDeSemana = dataReader["CalcularFinaisDeSemana"].ToString() == "S";
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

        public Financeiro.Variaveis ConsultarVariaveisPelaEmpresaIdColuna(int empresa, int idColuna)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Variaveis _tipo = new Financeiro.Variaveis();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, idcoluna, calculomediapor, qtderetroativo, feriadoporano, consideraemendasadado, feriaapenasdinheiro");
                query.Append("     , reduzir, aumentar, percentualreduzir, percentualaumentar, nome, ativo, codigo, CalcularFinaisDeSemana");
                query.Append("  from Niff_Fin_Variaveis");
                query.Append(" Where idEmpresa = " + empresa);
                query.Append("   and Idcoluna = " + idColuna);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdColuna = Convert.ToInt32(dataReader["IdColuna"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.CalculoPor = dataReader["calculomediapor"].ToString();

                        _tipo.Quantidade = Convert.ToInt32(dataReader["qtderetroativo"].ToString());
                        _tipo.PercentualReduzir = Convert.ToInt32(dataReader["percentualreduzir"].ToString());
                        _tipo.PercentualAumentar = Convert.ToInt32(dataReader["percentualaumentar"].ToString());

                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.FeriadoPorAno = dataReader["feriadoporano"].ToString() == "S";
                        _tipo.EmendaSabado = dataReader["consideraemendasadado"].ToString() == "S";
                        _tipo.FeriaDinheiro = dataReader["feriaapenasdinheiro"].ToString() == "S";
                        _tipo.Reduzir = dataReader["reduzir"].ToString() == "S";
                        _tipo.Aumentar = dataReader["aumentar"].ToString() == "S";
                        _tipo.CalcularFinaisDeSemana = dataReader["CalcularFinaisDeSemana"].ToString() == "S";
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

        public Financeiro.Variaveis ConsultarVariavelPeloId(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Variaveis _tipo = new Financeiro.Variaveis();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, idcoluna, calculomediapor, qtderetroativo, feriadoporano, consideraemendasadado, feriaapenasdinheiro");
                query.Append("     , reduzir, aumentar, percentualreduzir, percentualaumentar, nome, ativo, codigo, CalcularFinaisDeSemana");
                query.Append("  from Niff_Fin_Variaveis");
                query.Append(" Where id = " + id);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdColuna = Convert.ToInt32(dataReader["IdColuna"].ToString());

                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.CalculoPor = dataReader["calculomediapor"].ToString();

                        _tipo.Quantidade = Convert.ToInt32(dataReader["qtderetroativo"].ToString());
                        _tipo.PercentualReduzir = Convert.ToInt32(dataReader["percentualreduzir"].ToString());
                        _tipo.PercentualAumentar = Convert.ToInt32(dataReader["percentualaumentar"].ToString());

                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.FeriadoPorAno = dataReader["feriadoporano"].ToString() == "S";
                        _tipo.EmendaSabado = dataReader["consideraemendasadado"].ToString() == "S";
                        _tipo.FeriaDinheiro = dataReader["feriaapenasdinheiro"].ToString() == "S";
                        _tipo.Reduzir = dataReader["reduzir"].ToString() == "S";
                        _tipo.Aumentar = dataReader["aumentar"].ToString() == "S";
                        _tipo.CalcularFinaisDeSemana = dataReader["CalcularFinaisDeSemana"].ToString() == "S";
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

        public bool Grava(Financeiro.Variaveis _dados)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!_dados.Existe)
                {

                    query.Clear();
                    query.Append("Insert into Niff_Fin_Variaveis");
                    query.Append("   (id, idempresa, idcoluna, calculomediapor, qtderetroativo, feriadoporano, consideraemendasadado, feriaapenasdinheiro");
                    query.Append("     , reduzir, aumentar, percentualreduzir, percentualaumentar, nome, ativo, codigo, CalcularFinaisDeSemana");
                    query.Append("  ) Values ( (Select Nvl(Max(id),0) +1 next From Niff_Fin_Variaveis) ");
                    query.Append(", " + _dados.IdEmpresa );
                    query.Append(", " + _dados.IdColuna);
                    query.Append(", '" + _dados.CalculoPor + "'");
                    query.Append(", " + _dados.Quantidade);
                    query.Append(", '" + (_dados.FeriadoPorAno ? "S" : "N") + "'");
                    query.Append(", '" + (_dados.EmendaSabado ? "S" : "N") + "'");
                    query.Append(", '" + (_dados.FeriaDinheiro ? "S" : "N") + "'");
                    query.Append(", '" + (_dados.Reduzir ? "S" : "N") + "'");
                    query.Append(", '" + (_dados.Aumentar ? "S" : "N") + "'");
                    query.Append(", " + _dados.PercentualReduzir.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(", " + _dados.PercentualAumentar.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(", '" + _dados.Nome + "'");
                    query.Append(", '" + (_dados.Ativo ? "S" : "N") + "'");
                    query.Append(", " + _dados.Codigo);
                    query.Append(", '" + (_dados.CalcularFinaisDeSemana ? "S" : "N") + "'");
                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Fin_Variaveis");
                    query.Append("   set Nome = '" + _dados.Nome + "'");
                    query.Append("     , Ativo = '" + (_dados.Ativo ? "S" : "N") + "'");
                    query.Append("     , calculomediapor = '" + _dados.CalculoPor + "'");
                    query.Append("     , qtderetroativo = " + _dados.Quantidade);
                    query.Append("     , feriadoporano = '" + (_dados.FeriadoPorAno ? "S" : "N") + "'");
                    query.Append("     , consideraemendasadado = '" + (_dados.EmendaSabado ? "S" : "N") + "'");
                    query.Append("     , feriaapenasdinheiro = '" + (_dados.FeriaDinheiro ? "S" : "N") + "'");
                    query.Append("     , reduzir = '" + (_dados.Reduzir ? "S" : "N") + "'");
                    query.Append("     , aumentar = '" + (_dados.Aumentar ? "S" : "N") + "'");
                    query.Append("     , percentualreduzir = " + _dados.PercentualReduzir.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , percentualaumentar = " + _dados.PercentualAumentar.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , CalcularFinaisDeSemana = '" + (_dados.CalcularFinaisDeSemana ? "S" : "N") + "'");
                    query.Append(" Where Id = " + _dados.Id);
                }

                return sessao.ExecuteSqlTransaction(query.ToString(), null);
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

        public bool ExcluiVariavel(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Fin_Variaveis");
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

        public int ProximoCodigoVariavel(int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Nvl(Max(codigo),0) +1 next From Niff_Fin_Variaveis");
                query.Append(" where IdEmpresa = " + empresa);
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

        #region Demonstrativos

        public Financeiro.Demonstrativo ConsultaDemonstrativo(int empresa, int idBanco, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Demonstrativo _tipo = new Financeiro.Demonstrativo();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, idBanco, referencia, SaldoInicial, SaldoFinal");
                query.Append("  from NIFF_FIN_Demonstrativo");
                query.Append(" Where idEmpresa = " + empresa);
                query.Append("   and IdBanco = " + idBanco);
                query.Append("   and Referencia = '" + referencia + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdBanco = Convert.ToInt32(dataReader["IdBanco"].ToString());

                        _tipo.Referencia = dataReader["Referencia"].ToString();

                        _tipo.SaldoFinal = Convert.ToDecimal(dataReader["SaldoFinal"].ToString());
                        _tipo.SaldoInicial = Convert.ToDecimal(dataReader["SaldoInicial"].ToString());

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

        public Decimal SaldoInicial(int empresa, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;

            try
            {
                query.Append("Select sum(SaldoInicial) SaldoInicial");
                query.Append("  from NIFF_FIN_Demonstrativo");
                query.Append(" Where idEmpresa = " + empresa);
                query.Append("   and Referencia = '" + referencia + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _valor = Convert.ToDecimal(dataReader["SaldoInicial"].ToString());

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


        public Financeiro.Demonstrativo ConsultaDemonstrativo(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Demonstrativo _tipo = new Financeiro.Demonstrativo();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, idBanco, referencia, SaldoInicial, SaldoFinal");
                query.Append("  from NIFF_FIN_Demonstrativo");
                query.Append(" Where id = " + id);
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdBanco = Convert.ToInt32(dataReader["IdBanco"].ToString());

                        _tipo.Referencia = dataReader["Referencia"].ToString();

                        _tipo.SaldoFinal = Convert.ToDecimal(dataReader["SaldoFinal"].ToString());
                        _tipo.SaldoInicial = Convert.ToDecimal(dataReader["SaldoInicial"].ToString());

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

        public List<Financeiro.Demonstrativo> ListarDemonstrativo(int empresa, int idBanco)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.Demonstrativo> _lista = new List<Financeiro.Demonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, idBanco, referencia, SaldoInicial, SaldoFinal");
                query.Append("  from NIFF_FIN_Demonstrativo");
                query.Append(" Where idEmpresa = " + empresa);
                query.Append("   and IdBanco = " + idBanco);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Financeiro.Demonstrativo _tipo = new Financeiro.Demonstrativo();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdBanco = Convert.ToInt32(dataReader["IdBanco"].ToString());

                        _tipo.Referencia = dataReader["Referencia"].ToString();

                        _tipo.SaldoFinal = Convert.ToDecimal(dataReader["SaldoFinal"].ToString());
                        _tipo.SaldoInicial = Convert.ToDecimal(dataReader["SaldoInicial"].ToString());

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

        public List<Financeiro.ColunasDemonstrativo> ListarColunasDemonstrativo(int IdDemonstrativo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, iddemonstrativo, idcoluna, data, round(previsto,2) previsto, round(realizado,2) realizado, round(realizadoBco,2) realizadoBco");
                query.Append("  from NIFF_FIN_ColDemonstrativo");
                query.Append(" Where IdDemonstrativo = " + IdDemonstrativo);
                query.Append(" Order by data, idcoluna");

                Query executar = sessao.CreateQuery(query.ToString());

                auxDataReader = executar.ExecuteQuery();

                using (auxDataReader)
                {
                    while (auxDataReader.Read())
                    {
                        Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(auxDataReader["Id"].ToString());
                        _tipo.IdDemonstrativo = Convert.ToInt32(auxDataReader["IdDemonstrativo"].ToString());
                        _tipo.IdColuna = Convert.ToInt32(auxDataReader["IdColuna"].ToString());

                        Financeiro.Colunas _col = Consultar(_tipo.IdColuna);

                        _tipo.Data = Convert.ToDateTime(auxDataReader["data"].ToString());
                        _tipo.Docto = _col.Nome;

                        _tipo.Previsto = Math.Round(Convert.ToDecimal(auxDataReader["Previsto"].ToString()), 2);
                        _tipo.Realizado = Math.Round(Convert.ToDecimal(auxDataReader["Realizado"].ToString()), 2);
                        _tipo.RealizadoBCO = Math.Round(Convert.ToDecimal(auxDataReader["RealizadoBCO"].ToString()), 2);

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

        public List<Financeiro.HistoricoDemonstrativo> ListarHistoricoDemonstrativo(int IdDemonstrativo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.HistoricoDemonstrativo> _lista = new List<Financeiro.HistoricoDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idcoldemonst, dataalteracao, round(previsto,2) previsto, round(realizado,2) realizado, round(realizadoBco,2) realizadoBco, motivoalteracaoprevisto, motivoalteracaorealizado, idusuario, data");
                query.Append("  from Niff_Fin_HistoDemonstrativo");
                query.Append(" Where idcoldemonst in (select Id from NIFF_FIN_ColDemonstrativo where IdDemonstrativo = " + IdDemonstrativo + ")");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Financeiro.HistoricoDemonstrativo _tipo = new Financeiro.HistoricoDemonstrativo();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        _tipo.IdColunaDemonstrativo = Convert.ToInt32(dataReader["idcoldemonst"].ToString());

                        _tipo.DataAlteracao = Convert.ToDateTime(dataReader["dataalteracao"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["data"].ToString());

                        _tipo.Previsto = Math.Round(Convert.ToDecimal(dataReader["Previsto"].ToString()),2);
                        _tipo.Realizado = Math.Round(Convert.ToDecimal(dataReader["Realizado"].ToString()),2);
                        _tipo.RealizadoBCO = Math.Round(Convert.ToDecimal(dataReader["RealizadoBCO"].ToString()),2);

                        _tipo.MotivoPrevisto = dataReader["motivoalteracaoprevisto"].ToString();
                        _tipo.MotivoRealizado = dataReader["motivoalteracaorealizado"].ToString();

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

        public bool Gravar(Financeiro.Demonstrativo _dados, List<Financeiro.ColunasDemonstrativo> _colunas, List<Financeiro.HistoricoDemonstrativo> _historico,
                           string[,] crc, string[,] cpg) 
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int id = 0;
            int idColDemonstrativo = 0;
            int idHistorico = 0;

            try
            {
                if (!_dados.Existe)
                {
                    query.Clear();
                    query.Append("Select Nvl(Max(id),0) +1 next From NIFF_FIN_Demonstrativo");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            id = Convert.ToInt32(dataReader["next"].ToString());
                    }

                    query.Clear();
                    query.Append("Insert into NIFF_FIN_Demonstrativo");
                    query.Append("   (id, idempresa, idBanco, referencia, SaldoInicial, SaldoFinal");
                    query.Append("  ) Values (" + id);
                    query.Append(", " + _dados.IdEmpresa);
                    query.Append(", " + _dados.IdBanco);
                    query.Append(", '" + _dados.Referencia  + "'");
                    query.Append(", " + _dados.SaldoInicial.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(", " + _dados.SaldoFinal.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update NIFF_FIN_Demonstrativo");
                    query.Append("   set SaldoInicial = " + _dados.SaldoInicial.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , SaldoFinal = " + _dados.SaldoFinal.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" Where Id = " + _dados.Id);

                    id = _dados.Id;
                }

                if (!sessao.ExecuteSqlTransaction(query.ToString(), null))
                    return false;
                else
                {
                    foreach (var item in _colunas.Where(w => w.IdDemonstrativo == _dados.Id))
                    {
                        if (!item.Existe)
                        {
                            query.Clear();
                            query.Append("Select Nvl(Max(id),0) +1 next From NIFF_FIN_ColDemonstrativo");
                            Query executar = sessao.CreateQuery(query.ToString());

                            dataReader = executar.ExecuteQuery();

                            using (dataReader)
                            {
                                if (dataReader.Read())
                                    idColDemonstrativo = Convert.ToInt32(dataReader["next"].ToString());
                            }

                            query.Clear();
                            query.Append("Insert into NIFF_FIN_ColDemonstrativo");
                            query.Append("   (id, iddemonstrativo, idcoluna, data, previsto, realizado, realizadoBCO");
                            query.Append("  ) Values (" + idColDemonstrativo);
                            query.Append(", " + id);
                            query.Append(", " + item.IdColuna);
                            query.Append(", To_Date('" + item.Data.ToShortDateString() + "', 'dd/mm/yyyy')");
                            query.Append(", " + Math.Round(item.Previsto,2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append(", " + Math.Round(item.Realizado,2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append(", " + Math.Round(item.RealizadoBCO,2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append(") ");
                        }
                        else
                        {
                            query.Clear();
                            query.Append("Update NIFF_FIN_ColDemonstrativo");
                            query.Append("   set Previsto = " + Math.Round(item.Previsto, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , Realizado = " + Math.Round(item.Realizado, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , RealizadoBCO = " + Math.Round(item.RealizadoBCO, 2).ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" Where Id = " + item.Id);

                            idColDemonstrativo = item.Id;
                        }

                        if (!sessao.ExecuteSqlTransaction(query.ToString(), null))
                            return false;
                        else
                        {
                            foreach (var itemH in _historico.Where(w => w.IdColunaDemonstrativo == item.Id))
                            {
                                if (!itemH.Existe)
                                {
                                    query.Clear();
                                    query.Append("Select Nvl(Max(id),0) +1 next From Niff_Fin_HistoDemonstrativo");
                                    Query executar = sessao.CreateQuery(query.ToString());

                                    dataReader = executar.ExecuteQuery();

                                    using (dataReader)
                                    {
                                        if (dataReader.Read())
                                            idHistorico = Convert.ToInt32(dataReader["next"].ToString());
                                    }

                                    query.Clear();
                                    query.Append("Insert into Niff_Fin_HistoDemonstrativo");
                                    query.Append("   (id, idcoldemonst, dataalteracao, previsto, realizado, realizadoBCO, motivoalteracaoprevisto, motivoalteracaorealizado, idusuario, data");
                                    query.Append("  ) Values (" + idHistorico);
                                    query.Append(", " + idColDemonstrativo);
                                    query.Append(", sysdate ");
                                    query.Append(", " + itemH.Previsto.ToString().Replace(".", "").Replace(",", "."));
                                    query.Append(", " + itemH.Realizado.ToString().Replace(".", "").Replace(",", "."));
                                    query.Append(", " + itemH.RealizadoBCO.ToString().Replace(".", "").Replace(",", "."));
                                    query.Append(", '" + itemH.MotivoPrevisto + "'");
                                    query.Append(", '" + itemH.MotivoRealizado + "'");
                                    query.Append(", " + itemH.IdUsuario);
                                    query.Append(", To_Date('" + item.Data.ToShortDateString() + "', 'dd/mm/yyyy')");
                                    query.Append(") ");

                                    if (!sessao.ExecuteSqlTransaction(query.ToString(), null))
                                        return false;
                                }
                            }
                        }
                    }
                }

                DateTime _dataVencimento = DateTime.MinValue;
                decimal _idDocto = 0;

                //foreach (var item in crc)
                for (int i = 0; i < crc.Length; i++)
                {
                    try
                    {
                        if (crc[i, 0] != null)
                        {
                            _dataVencimento = Convert.ToDateTime(crc[i, 0]);
                            _idDocto = Convert.ToDecimal(crc[i, 1]);

                            query.Clear();
                            query.Append("Update CRCDocto");
                            query.Append("   set vencimentocrc = to_date('" + _dataVencimento.ToShortDateString() + "','dd/mm/yyyy')");
                            query.Append(" where CodDoctoCRC = " + _idDocto);

                            if (!sessao.ExecuteSqlTransaction(query.ToString(), null))
                                return false;
                        }
                    }
                    catch { }
                }

                for (int i = 0; i < cpg.Length; i++)
                {
                    try
                    {
                        if (cpg[i, 0] != null)
                        {

                            _dataVencimento = Convert.ToDateTime(cpg[i, 0]);
                            _idDocto = Convert.ToDecimal(cpg[i, 1]);

                            query.Clear();
                            query.Append("Update CPGDocto");
                            query.Append("   set vencimentoCPG = to_date('" + _dataVencimento.ToShortDateString() + "','dd/mm/yyyy')");
                            query.Append(" where CodDoctoCPG = " + _idDocto);

                            if (!sessao.ExecuteSqlTransaction(query.ToString(), null))
                                return false;
                        }
                    }
                    catch { }
                }

                return true;
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

        #region Previsto
        public decimal ConsultarReceitaPrevista(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percentual,
            string tipoLinha, string empresaConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            
            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;
            try
            {
                if (porMeses)
                    data = data.AddMonths(-1);
                else
                    data = data.AddYears(-1);

                query.Append("Select Sum(vlr_receb) Valor");
                query.Append("  From (Select Sum(d.vlr_receb) Vlr_Receb");
                query.Append("         from t_arr_Guia g");
                query.Append("            , t_Arr_Detalhe_Guia d");
                query.Append("            , t_Trf_Tipopagto t, Bgm_Cadlinhas L");
                query.Append("        Where g.cod_seq_guia = d.cod_seq_guia");
                query.Append("          And d.cod_tipopagtarifa = t.cod_tipopagto");
                query.Append("          And t.flg_normal = 'S'");
                if (empresaConsolidar == "")
                    query.Append("          And lpad(Cod_empresa,3,'0') || '/' || lpad(g.CodigoFl,3,'0')= '" + empresa + "'");
                else
                {
                    query.Append("          And (lpad(Cod_empresa,3,'0') || '/' || lpad(g.CodigoFl,3,'0')= '" + empresa + "'");
                    query.Append("           or lpad(Cod_empresa, 3, '0') || '/' || lpad(g.CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }
                query.Append("          And g.dat_viagem_guia = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("          And l.codintlinha = d.codintlinha");

                if (tipoLinha == "R")
                    query.Append("          And l.codigotplinha = 0");
                else
                {
                    if (tipoLinha == "M")
                        query.Append("          And l.flg_munic_interest = 'M'");
                    else
                        if (tipoLinha == "I")
                        query.Append("          And l.flg_munic_interest = 'U'");
                }

                for (int i = 0; i < quantidade-1; i++)
                {
                    if (porMeses)
                        data = data.AddMonths(-1);
                    else
                        data = data.AddYears(-1);

                    query.Append("        Union All ");
                    query.Append("       Select Sum(d.vlr_receb)");
                    query.Append("         from t_arr_Guia g");
                    query.Append("            , t_Arr_Detalhe_Guia d");
                    query.Append("            , t_Trf_Tipopagto t, Bgm_Cadlinhas L");
                    query.Append("        Where g.cod_seq_guia = d.cod_seq_guia");
                    query.Append("          And d.cod_tipopagtarifa = t.cod_tipopagto");
                    query.Append("          And t.flg_normal = 'S'");

                    if (empresaConsolidar == "")
                        query.Append("          And lpad(Cod_empresa,3,'0') || '/' || lpad(g.CodigoFl,3,'0')= '" + empresa + "'");
                    else
                    {
                        query.Append("          And (lpad(Cod_empresa,3,'0') || '/' || lpad(g.CodigoFl,3,'0')= '" + empresa + "'");
                        query.Append("           or lpad(Cod_empresa, 3, '0') || '/' || lpad(g.CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                    }

                    query.Append("          And g.dat_viagem_guia = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("          And l.codintlinha = d.codintlinha");

                    if (tipoLinha == "R")
                        query.Append("          And l.codigotplinha = 0");
                    else
                    {
                        if (tipoLinha == "M")
                            query.Append("          And l.flg_munic_interest = 'M'");
                        else
                            if (tipoLinha == "I")
                            query.Append("          And l.flg_munic_interest = 'U'");
                    }
                }
                query.Append(" )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _valor = _valor / quantidade;

                        if (aumentarReduzir == "A")
                            _valor = _valor + (_valor * (percentual / 100));
                        if (aumentarReduzir == "R")
                            _valor = _valor - (_valor * (percentual / 100));
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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarReceitaPrevistaDetalhada(string empresa, DateTime data, int quantidade, bool porMeses,
            string aumentarReduzir, decimal percetual, string tipoLinha, string empresaConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (porMeses)
                    data = data.AddMonths(-1);
                else
                    data = data.AddYears(-1);

                query.Append("Select Sum(d.vlr_receb) Valor, dat_viagem_guia ");
                query.Append("  from t_arr_Guia g");
                query.Append("     , t_Arr_Detalhe_Guia d");
                query.Append("     , t_Trf_Tipopagto t, Bgm_Cadlinhas L");
                query.Append(" Where g.cod_seq_guia = d.cod_seq_guia");
                query.Append("   And d.cod_tipopagtarifa = t.cod_tipopagto");
                query.Append("   And t.flg_normal = 'S'");
                if (empresaConsolidar == "")
                    query.Append("          And lpad(Cod_empresa,3,'0') || '/' || lpad(g.CodigoFl,3,'0')= '" + empresa + "'");
                else
                {
                    query.Append("          And (lpad(Cod_empresa,3,'0') || '/' || lpad(g.CodigoFl,3,'0')= '" + empresa + "'");
                    query.Append("           or lpad(Cod_empresa, 3, '0') || '/' || lpad(g.CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }
                query.Append("   And g.dat_viagem_guia = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And l.codintlinha = d.codintlinha");

                if (tipoLinha == "R")
                    query.Append("          And l.codigotplinha = 0");
                else
                {
                    if (tipoLinha == "M")
                        query.Append("          And l.flg_munic_interest = 'M'");
                    else
                        if (tipoLinha == "I")
                        query.Append("          And l.flg_munic_interest = 'U'");
                }

                query.Append(" group by dat_viagem_guia");

                for (int i = 0; i < quantidade - 1; i++)
                {
                    query.Append(" Union All ");
                    query.Append("Select Sum(d.vlr_receb) Valor, dat_viagem_guia");
                    query.Append("  from t_arr_Guia g");
                    query.Append("     , t_Arr_Detalhe_Guia d");
                    query.Append("     , t_Trf_Tipopagto t, Bgm_Cadlinhas L");
                    query.Append(" Where g.cod_seq_guia = d.cod_seq_guia");
                    query.Append("   And d.cod_tipopagtarifa = t.cod_tipopagto");
                    query.Append("   And t.flg_normal = 'S'");
                    if (empresaConsolidar == "")
                        query.Append("          And lpad(Cod_empresa,3,'0') || '/' || lpad(g.CodigoFl,3,'0')= '" + empresa + "'");
                    else
                    {
                        query.Append("          And (lpad(Cod_empresa,3,'0') || '/' || lpad(g.CodigoFl,3,'0')= '" + empresa + "'");
                        query.Append("           or lpad(Cod_empresa, 3, '0') || '/' || lpad(g.CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                    }
                    query.Append("   And l.codintlinha = d.codintlinha");

                    if (porMeses)
                        data = data.AddMonths(-1);
                    else
                        data = data.AddYears(-1);

                    query.Append("   And g.dat_viagem_guia = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                    if (tipoLinha == "R")
                        query.Append("          And l.codigotplinha = 0");
                    else
                    {
                        if (tipoLinha == "M")
                            query.Append("          And l.flg_munic_interest = 'M'");
                        else
                            if (tipoLinha == "I")
                            query.Append("          And l.flg_munic_interest = 'U'");
                    }
                    query.Append(" group by dat_viagem_guia");
                }
                //query.Append(" )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["dat_viagem_guia"].ToString());

                        if (aumentarReduzir == "N")
                        {
                            _tipo.Realizado = _tipo.Previsto;
                            _tipo.Percentual = "Nenhum calculo";
                        }
                        if (aumentarReduzir == "A")
                        {
                            _tipo.Realizado = _tipo.Previsto + (_tipo.Previsto * (percetual / 100));
                            _tipo.Percentual = "Aumento de " + string.Format("{0:0.00}", percetual) + " %";
                        }
                        if (aumentarReduzir == "R")
                        {
                            _tipo.Realizado = _tipo.Previsto - (_tipo.Previsto * (percetual / 100));
                            _tipo.Percentual = "Redução de " + string.Format("{0:0.00}", percetual) + " %";
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

        public decimal ConsultarContaReceberPrevista(string empresa, DateTime data, int quantidade, bool porMeses, 
            string aumentarReduzir, decimal percetual, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Variaveis _tipo = new Financeiro.Variaveis();
            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;
            decimal _qdt = 0;

            try
            {
                if (porMeses)
                    data = data.AddMonths(-1);
                else
                    data = data.AddYears(-1);
   

                query.Append("Select Sum(valor) Valor, Count(recebimentocrc) qtde");
                query.Append("  From (Select Sum(i.valoritemdoc) Valor, recebimentocrc");// -- +d.acrescimocrc - (d.vlrinsscrc + d.vlrirrfcrc + d.vlrpiscrc + d.vlrcofinscrc + d.vlrcslcrc + d.vlrisscrc + d.descontocrc + d.descfinanceirocrc)) valor");
                query.Append("         From crcdocto d");
                query.Append("            , crcitdoc i");
                query.Append("        Where d.coddoctocrc = i.coddoctocrc");
                query.Append("          And d.statusdoctocrc = 'B'");

                if (empresaConsolidar == "")
                    query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                else
                {
                    query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    query.Append("           or lpad(codigoEmpresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }

                query.Append("          And d.recebimentocrc = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("          And i.codtpreceita In (Select x.codtpreceita");
                query.Append("                                   From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                                  Where x.idcolbanco = c.Id");

                if (empresaConsolidar == "")
                   query.Append("                                    And c.Idbanco = " + idBanco);
                else
                    query.Append("                                   And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                query.Append("          And c.Idcoluna = " + idColuna + ") Group by recebimentocrc");

                for (int i = 0; i < quantidade - 1; i++)
                {
                    if (porMeses)
                        data = data.AddMonths(-1);
                    else
                        data = data.AddYears(-1);

                    query.Append("        Union All ");
                    query.Append("       Select Sum(i.valoritemdoc) Valor, recebimentocrc");// -- +d.acrescimocrc - (d.vlrinsscrc + d.vlrirrfcrc + d.vlrpiscrc + d.vlrcofinscrc + d.vlrcslcrc + d.vlrisscrc + d.descontocrc + d.descfinanceirocrc)) valor");
                    query.Append("         From crcdocto d");
                    query.Append("            , crcitdoc i");
                    query.Append("        Where d.coddoctocrc = i.coddoctocrc");
                    query.Append("          And d.statusdoctocrc = 'B'");

                    if (empresaConsolidar == "")
                        query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    else
                    {
                        query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                        query.Append("           or lpad(codigoEmpresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                    }

                    query.Append("          And d.recebimentocrc = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("          And i.codtpreceita In (Select x.codtpreceita");
                    query.Append("                                   From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                    query.Append("                                   Where x.idcolbanco = c.Id");

                    if (empresaConsolidar == "")
                        query.Append("                                    And c.Idbanco = " + idBanco);
                    else
                        query.Append("                                   And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                    query.Append("          And c.Idcoluna = " + idColuna + ") Group by recebimentocrc");
                }
                query.Append(" ) group by recebimentocrc");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _valor = _valor + Convert.ToDecimal(dataReader["Valor"].ToString());
                        _qdt = _qdt + Convert.ToInt32(dataReader["qtde"].ToString());
                    }
                    if (_valor != 0)
                    {
                        if (_qdt < quantidade)
                            _valor = _valor / _qdt;
                        else
                            _valor = _valor / quantidade;

                        if (aumentarReduzir == "A")
                            _valor = _valor + (_valor * (percetual / 100));
                        if (aumentarReduzir == "R")
                            _valor = _valor - (_valor * (percetual / 100));
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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarContaReceberDetalhada(string empresa, DateTime data, int quantidade, bool porMeses, 
            string aumentarReduzir, decimal percetual, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (porMeses)
                    data = data.AddMonths(-1);
                else
                    data = data.AddYears(-1);


                query.Append("Select Sum(valor) Valor, codtpreceita, recebimentocrc");
                query.Append("             , Docto, seriedoctocrc, statusdoctocrc, rsocialcli, coddoctocrc");
                query.Append("  From (Select Sum(i.valoritemdoc) Valor, i.codtpreceita, recebimentocrc ");
                query.Append("             , d.nrodoctocrc || '/' || d.nroparcelacrc Docto, d.seriedoctocrc, d.statusdoctocrc, c.rsocialcli, d.coddoctocrc");
                query.Append("          From crcdocto d");
                query.Append("             , crcitdoc i, bgm_cliente c");
                query.Append("         Where d.coddoctocrc = i.coddoctocrc");
                query.Append("           And d.statusdoctocrc = 'B'");
                query.Append("           And c.codcli = d.codcli");

                if (empresaConsolidar == "")
                    query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                else
                {
                    query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    query.Append("           or lpad(codigoEmpresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }

                query.Append("           And d.recebimentocrc = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("           And i.codtpreceita In(Select x.codtpreceita");
                query.Append("                                   From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                                  Where x.idcolbanco = c.Id");

                if (empresaConsolidar == "")
                    query.Append("                                    And c.Idbanco = " + idBanco);
                else
                    query.Append("                                   And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                query.Append("                                    And c.Idcoluna = " + idColuna + ")");
                query.Append("         Group by i.codtpreceita, recebimentocrc");
                query.Append("             , d.nrodoctocrc || '/' || d.nroparcelacrc, d.seriedoctocrc, d.statusdoctocrc, c.rsocialcli, d.coddoctocrc");

                for (int i = 0; i < quantidade - 1; i++)
                {
                    if (porMeses)
                        data = data.AddMonths(-1);
                    else
                        data = data.AddYears(-1);

                    query.Append("        Union All ");
                    query.Append("       Select Sum(i.valoritemdoc) Valor, i.codtpreceita, recebimentocrc");
                    query.Append("            , d.nrodoctocrc || '/' || d.nroparcelacrc Docto, d.seriedoctocrc, d.statusdoctocrc, c.rsocialcli, d.coddoctocrc");
                    query.Append("         From crcdocto d");
                    query.Append("            , crcitdoc i, bgm_cliente c");
                    query.Append("        Where d.coddoctocrc = i.coddoctocrc");
                    query.Append("          And d.statusdoctocrc = 'B'");
                    query.Append("          And c.codcli = d.codcli");

                    if (empresaConsolidar == "")
                        query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    else
                    {
                        query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                        query.Append("           or lpad(codigoEmpresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                    }

                    query.Append("          And d.recebimentocrc = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("          And i.codtpreceita In(Select x.codtpreceita");
                    query.Append("                                  From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                    query.Append("                                 Where x.idcolbanco = c.Id");

                    if (empresaConsolidar == "")
                        query.Append("                                    And c.Idbanco = " + idBanco);
                    else
                        query.Append("                                   And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                    query.Append("                                   And c.Idcoluna = " + idColuna + ")");
                    query.Append("        Group by i.codtpreceita, recebimentocrc");
                    query.Append("            , d.nrodoctocrc || '/' || d.nroparcelacrc, d.seriedoctocrc, d.statusdoctocrc, c.rsocialcli, d.coddoctocrc");
                }
                query.Append(" ) Group by codtpreceita, recebimentocrc");
                query.Append("       , Docto, seriedoctocrc, statusdoctocrc, rsocialcli, coddoctocrc");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["recebimentocrc"].ToString());
                        _tipo.Auxiliar = dataReader["codtpreceita"].ToString();
                        _tipo.Docto = dataReader["Docto"].ToString();
                        _tipo.Serie = dataReader["seriedoctocrc"].ToString();
                        _tipo.Status = dataReader["statusdoctocrc"].ToString();
                        _tipo.Razao = dataReader["rsocialcli"].ToString();
                        _tipo.IdDocto = Convert.ToDecimal(dataReader["coddoctocrc"].ToString());

                        if (aumentarReduzir == "N")
                        {
                            _tipo.Realizado = _tipo.Previsto;
                            _tipo.Percentual = "Nenhum calculo";
                        }
                        if (aumentarReduzir == "A")
                        {
                            _tipo.Realizado = _tipo.Previsto + (_tipo.Previsto * (percetual / 100));
                            _tipo.Percentual = "Aumento de " + string.Format("{0:0.00}", percetual) + " %";
                        }
                        if (aumentarReduzir == "R")
                        {
                            _tipo.Realizado = _tipo.Previsto - (_tipo.Previsto * (percetual / 100));
                            _tipo.Percentual = "Redução de " + string.Format("{0:0.00}", percetual) + " %";
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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarContaReceberDetalhadaPeloVencimento(string empresa, DateTime data, int quantidade, bool porMeses, 
            string aumentarReduzir, decimal percetual, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select Sum(i.valoritemdoc) Valor, i.codtpreceita, vencimentocrc, vencimentooriginalcrc");
                query.Append("            , d.nrodoctocrc || '/' || d.nroparcelacrc Docto, d.seriedoctocrc, d.statusdoctocrc, c.rsocialcli, d.coddoctocrc");
                query.Append("  From crcdocto d");
                query.Append("     , crcitdoc i, bgm_cliente c");
                query.Append(" Where d.coddoctocrc = i.coddoctocrc");
                query.Append("   And d.statusdoctocrc <> 'C'");
                query.Append("   And c.codcli = d.codcli");

                if (empresaConsolidar == "")
                    query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                else
                {
                    query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    query.Append("           or lpad(codigoEmpresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }

                query.Append("   And d.vencimentocrc = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And i.codtpreceita In (Select x.codtpreceita");
                query.Append("                            From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                           Where x.idcolbanco = c.Id");
                if (empresaConsolidar == "")
                    query.Append("                                    And c.Idbanco = " + idBanco);
                else
                    query.Append("                                   And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                query.Append("                             And c.Idcoluna = " + idColuna + ")");
                query.Append(" Group by i.codtpreceita, vencimentocrc, vencimentooriginalcrc");
                query.Append("     , d.nrodoctocrc || '/' || d.nroparcelacrc, d.seriedoctocrc, d.statusdoctocrc, c.rsocialcli, d.coddoctocrc");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["vencimentocrc"].ToString());
                        _tipo.VencimentoAnterior = Convert.ToDateTime(dataReader["vencimentooriginalcrc"].ToString());
                        _tipo.Auxiliar = dataReader["codtpreceita"].ToString();
                        _tipo.Docto = dataReader["Docto"].ToString();
                        _tipo.Serie = dataReader["seriedoctocrc"].ToString();
                        _tipo.Status = dataReader["statusdoctocrc"].ToString();
                        _tipo.Razao = dataReader["rsocialcli"].ToString();
                        _tipo.IdDocto = Convert.ToDecimal(dataReader["coddoctocrc"].ToString());
                        _tipo.IdColuna = idColuna;

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

        public decimal ConsultarContaPagarPrevista(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual,
            int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Variaveis _tipo = new Financeiro.Variaveis();
            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;
            decimal _qdt = 0;

            try
            {
                if (porMeses)
                    data = data.AddMonths(-1);
                else
                    data = data.AddYears(-1);


                query.Append("Select Sum(valor) Valor, Count(Pagamentocpg) qtde");
                query.Append("  From (Select Sum(i.valoritemdoc) Valor, Pagamentocpg");// -- +d.acrescimocpg - (d.vlrinsscpg + d.vlrirrfcpg + d.vlrpiscpg + d.vlrcofinscpg + d.vlrcslcpg + d.vlrisscpg + d.descontocpg + d.descfinanceirocpg)) valor");
                query.Append("         From cpgdocto d");
                query.Append("            , cpgitdoc i");
                query.Append("        Where d.coddoctocpg = i.coddoctocpg");
                query.Append("          And d.statusdoctocpg = 'B'");

                if (empresaConsolidar == "")
                    query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                else
                {
                    query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresaConsolidar + "')");
                }

                query.Append("          And d.Pagamentocpg = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("          And i.codtpDespesa In (Select x.codtpDespesa");
                query.Append("                                   From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                                  Where x.idcolbanco = c.Id");
                if (empresaConsolidar == "")
                    query.Append("                                    And c.Idbanco = " + idBanco);
                else
                    query.Append("                                    And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");
                query.Append("                                    And c.Idcoluna = " + idColuna + ") group by Pagamentocpg");

                for (int i = 0; i < quantidade - 1; i++)
                {
                    if (porMeses)
                        data = data.AddMonths(-1);
                    else
                        data = data.AddYears(-1);

                    query.Append("        Union All ");
                    query.Append("       Select Sum(i.valoritemdoc) Valor, Pagamentocpg ");// -- +d.acrescimocpg - (d.vlrinsscpg + d.vlrirrfcpg + d.vlrpiscpg + d.vlrcofinscpg + d.vlrcslcpg + d.vlrisscpg + d.descontocpg + d.descfinanceirocpg)) valor");
                    query.Append("         From cpgdocto d");
                    query.Append("            , cpgitdoc i");
                    query.Append("        Where d.coddoctocpg = i.coddoctocpg");
                    query.Append("          And d.statusdoctocpg = 'B'");

                    if (empresaConsolidar == "")
                        query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    else
                    {
                        query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                        query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresaConsolidar + "')");
                    }

                    query.Append("          And d.Pagamentocpg = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("          And i.codtpdespesa In (Select x.codtpdespesa");
                    query.Append("                                  From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                    query.Append("                                 Where x.idcolbanco = c.Id");

                    if (empresaConsolidar == "")
                        query.Append("                                    And c.Idbanco = " + idBanco);
                    else
                        query.Append("                                    And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                    query.Append("                                   And c.Idcoluna = " + idColuna + ") group by Pagamentocpg");
                }
                query.Append(" ) ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _valor = _valor + Convert.ToDecimal(dataReader["Valor"].ToString());
                        _qdt = _qdt + Convert.ToInt32(dataReader["qtde"].ToString());
                    }
                    if (_valor != 0)
                    {
                        if (_qdt < quantidade)
                            _valor = _valor / _qdt;
                        else
                            _valor = _valor / quantidade;

                        if (aumentarReduzir == "A")
                            _valor = _valor + (_valor * (percetual / 100));
                        if (aumentarReduzir == "R")
                            _valor = _valor - (_valor * (percetual / 100));
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

        public decimal ConsultarContaPagarBancosPrevista(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual,
            int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();

            Financeiro.Bancos _banco = ConsultarBancos(idBanco);

            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;
            decimal _qdt = 0;

            try
            {
                if (porMeses)
                    data = data.AddMonths(-1);
                else
                    data = data.AddYears(-1);


                query.Append("Select Sum(valor) Valor, Count(Pagamentocpg) qtde");
                query.Append("  From (Select Sum(i.valoritemdoc) Valor, Pagamentocpg");// -- +d.acrescimocpg - (d.vlrinsscpg + d.vlrirrfcpg + d.vlrpiscpg + d.vlrcofinscpg + d.vlrcslcpg + d.vlrisscpg + d.descontocpg + d.descfinanceirocpg)) valor");
                query.Append("         From cpgdocto d");
                query.Append("            , cpgitdoc i");
                query.Append("        Where d.coddoctocpg = i.coddoctocpg");
                query.Append("          And d.statusdoctocpg = 'B'");

                if (empresaConsolidar == "")
                {
                    query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    if (empresa == "029/001" && data.Year == 2019)
                        query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '005/001')");
                    else
                        query.Append(")");
                }
                else
                {
                    query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresaConsolidar + "')");
                }

                query.Append("          And d.Pagamentocpg = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("          And i.codtpDespesa In (Select x.codtpDespesa");
                query.Append("                                   From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                                  Where x.idcolbanco = c.Id");
                if (empresaConsolidar == "")
                    query.Append("                                    And c.Idbanco = " + idBanco);
                else
                    query.Append("                                    And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");
                query.Append("                                    And c.Idcoluna = " + idColuna + ") group by Pagamentocpg");

                query.Append("        Union all ");

                query.Append("        Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data");
                query.Append("          From bcomovto m");
                query.Append("         Where m.docmovtobco not Like 'TB%'");

                if (empresaConsolidar == "")
                {
                    query.Append("           And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    if (empresa == "029/001" && data.Year == 2019)
                        query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '005/001')");
                    else
                        query.Append(")");
                }
                else
                {
                    query.Append("          And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    query.Append("           or lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }

                query.Append("           And m.vlmovtobco < 0");

                query.Append("           And m.statusmovtobco <> 'C'");
                query.Append("           And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                query.Append("           And m.Sistema = 'BCO'");

                if (empresa == "029/001" && data.Year == 2019)
                {
                    query.Append("           And (m.codbanco = 341 or m.codbanco = " + _banco.CodigoBanco + ")" );
                    query.Append("           And (m.codagencia = 46 or m.codagencia = " + _banco.CodigoAgencia + ")");
                    query.Append("           And (m.codcontabco = '55793' or m.codcontabco = '" + _banco.CodigoConta + "')");
                }
                else
                {
                    query.Append("           And m.codbanco = " + _banco.CodigoBanco);
                    query.Append("           And m.codagencia = " + _banco.CodigoAgencia);
                    query.Append("           And m.codcontabco = '" + _banco.CodigoConta + "'");
                }

                query.Append("           And m.codtpdespesa In(Select x.codtpdespesa");
                query.Append("                                   From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                                  Where x.idcolbanco = c.Id");

                if (empresaConsolidar == "")
                    query.Append("                                And c.Idbanco = " + idBanco);
                else
                    query.Append("                                And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                query.Append("                                    And c.Idcoluna = " + idColuna + ")");

                query.Append("         Group By m.dtefetivamovtobco");

                for (int i = 0; i < quantidade - 1; i++)
                {
                    if (porMeses)
                        data = data.AddMonths(-1);
                    else
                        data = data.AddYears(-1);

                    query.Append("        Union All ");
                    query.Append("       Select Sum(i.valoritemdoc) Valor, Pagamentocpg ");// -- +d.acrescimocpg - (d.vlrinsscpg + d.vlrirrfcpg + d.vlrpiscpg + d.vlrcofinscpg + d.vlrcslcpg + d.vlrisscpg + d.descontocpg + d.descfinanceirocpg)) valor");
                    query.Append("         From cpgdocto d");
                    query.Append("            , cpgitdoc i");
                    query.Append("        Where d.coddoctocpg = i.coddoctocpg");
                    query.Append("          And d.statusdoctocpg = 'B'");

                    if (empresaConsolidar == "")
                    {
                        query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                        if (empresa == "029/001" && data.Year == 2019)
                            query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '005/001')");
                        else
                            query.Append(")");
                    }
                    else
                    {
                        query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                        query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresaConsolidar + "')");
                    }

                    query.Append("          And d.Pagamentocpg = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("          And i.codtpdespesa In (Select x.codtpdespesa");
                    query.Append("                                  From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                    query.Append("                                 Where x.idcolbanco = c.Id");

                    if (empresaConsolidar == "")
                        query.Append("                                    And c.Idbanco = " + idBanco);
                    else
                        query.Append("                                    And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                    query.Append("                                   And c.Idcoluna = " + idColuna + ") group by Pagamentocpg");

                    query.Append("        Union all ");

                    query.Append("        Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data");
                    query.Append("          From bcomovto m");
                    query.Append("         Where m.docmovtobco not Like 'TB%'");

                    if (empresaConsolidar == "")
                    {
                        query.Append("           And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                        if (empresa == "029/001" && data.Year == 2019)
                            query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '005/001')");
                        else
                            query.Append(")");
                    }
                    else
                    {
                        query.Append("          And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                        query.Append("           or lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                    }

                    query.Append("           And m.vlmovtobco < 0");

                    query.Append("           And m.statusmovtobco <> 'C'");
                    query.Append("           And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                    query.Append("           And m.Sistema = 'BCO'");

                    if (empresa == "029/001" && data.Year == 2019)
                    {
                        query.Append("           And (m.codbanco = 341 or m.codbanco = " + _banco.CodigoBanco + ")");
                        query.Append("           And (m.codagencia = 46 or m.codagencia = " + _banco.CodigoAgencia + ")");
                        query.Append("           And (m.codcontabco = '55793' or m.codcontabco = '" + _banco.CodigoConta + "')");
                    }
                    else
                    {
                        query.Append("           And m.codbanco = " + _banco.CodigoBanco);
                        query.Append("           And m.codagencia = " + _banco.CodigoAgencia);
                        query.Append("           And m.codcontabco = '" + _banco.CodigoConta + "'");
                    }

                    query.Append("           And m.codtpdespesa In(Select x.codtpdespesa");
                    query.Append("                                   From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                    query.Append("                                  Where x.idcolbanco = c.Id");

                    if (empresaConsolidar == "")
                        query.Append("                                And c.Idbanco = " + idBanco);
                    else
                        query.Append("                                And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                    query.Append("                                    And c.Idcoluna = " + idColuna + ")");

                    query.Append("         Group By m.dtefetivamovtobco");
                }
                query.Append(" ) ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _valor = _valor + Convert.ToDecimal(dataReader["Valor"].ToString());
                        _qdt = _qdt + Convert.ToInt32(dataReader["qtde"].ToString());
                    }
                    if (_valor != 0)
                    {
                        if (_qdt < quantidade)
                            _valor = _valor / _qdt;
                        else
                            _valor = _valor / quantidade;

                        if (aumentarReduzir == "A")
                            _valor = _valor + (_valor * (percetual / 100));
                        if (aumentarReduzir == "R")
                            _valor = _valor - (_valor * (percetual / 100));
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

        public decimal ConsultarContaReceberBancosPrevista(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual,
            int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();

            Financeiro.Bancos _banco = ConsultarBancos(idBanco);

            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;
            decimal _qdt = 0;

            try
            {
                if (porMeses)
                    data = data.AddMonths(-1);
                else
                    data = data.AddYears(-1);

                query.Append("Select Sum(valor) Valor, Count(recebimentocrc) qtde");
                query.Append("  From (Select Sum(i.valoritemdoc) Valor, recebimentocrc");// -- +d.acrescimocrc - (d.vlrinsscrc + d.vlrirrfcrc + d.vlrpiscrc + d.vlrcofinscrc + d.vlrcslcrc + d.vlrisscrc + d.descontocrc + d.descfinanceirocrc)) valor");
                query.Append("         From crcdocto d");
                query.Append("            , crcitdoc i");
                query.Append("        Where d.coddoctocrc = i.coddoctocrc");
                query.Append("          And d.statusdoctocrc = 'B'");

                if (empresaConsolidar == "")
                {
                    query.Append("           And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    if (empresa == "029/001" && data.Year == 2019)
                        query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '005/001')");
                    else
                        query.Append(")");
                }
                else
                {
                    query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    query.Append("           or lpad(codigoEmpresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }

                query.Append("          And d.recebimentocrc = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("          And i.codtpreceita In (Select x.codtpreceita");
                query.Append("                                   From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                                  Where x.idcolbanco = c.Id");

                if (empresaConsolidar == "")
                    query.Append("                                    And c.Idbanco = " + idBanco);
                else
                    query.Append("                                   And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                query.Append("          And c.Idcoluna = " + idColuna + ") Group by recebimentocrc");                

                query.Append("        Union all ");

                query.Append("        Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data");
                query.Append("          From bcomovto m");
                query.Append("         Where m.docmovtobco not Like 'TB%'");

                if (empresaConsolidar == "")
                {
                    query.Append("           And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    if (empresa == "029/001" && data.Year == 2019)
                        query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '005/001')");
                    else
                        query.Append(")");
                }
                else
                {
                    query.Append("          And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    query.Append("           or lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }

                query.Append("           And m.vlmovtobco > 0");

                query.Append("           And m.statusmovtobco <> 'C'");
                query.Append("           And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                query.Append("           And m.Sistema = 'BCO'");
                if (empresa == "029/001" && data.Year == 2019)
                {
                    query.Append("           And (m.codbanco = 341 or m.codbanco = " + _banco.CodigoBanco + ")");
                    query.Append("           And (m.codagencia = 46 or m.codagencia = " + _banco.CodigoAgencia + ")");
                    query.Append("           And (m.codcontabco = '55793' or m.codcontabco = '" + _banco.CodigoConta + "')");
                }
                else
                {
                    query.Append("           And m.codbanco = " + _banco.CodigoBanco);
                    query.Append("           And m.codagencia = " + _banco.CodigoAgencia);
                    query.Append("           And m.codcontabco = '" + _banco.CodigoConta + "'");
                }

                query.Append("           And m.codtpreceita In(Select x.codtpreceita");
                query.Append("                                   From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                                  Where x.idcolbanco = c.Id");

                if (empresaConsolidar == "")
                    query.Append("                                And c.Idbanco = " + idBanco);
                else
                    query.Append("                                And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                query.Append("                                    And c.Idcoluna = " + idColuna + ")");

                query.Append("         Group By m.dtefetivamovtobco");

                for (int i = 0; i < quantidade - 1; i++)
                {
                    if (porMeses)
                        data = data.AddMonths(-1);
                    else
                        data = data.AddYears(-1);

                    query.Append("        Union All ");
                    query.Append("       Select Sum(i.valoritemdoc) Valor, recebimentocrc");// -- +d.acrescimocrc - (d.vlrinsscrc + d.vlrirrfcrc + d.vlrpiscrc + d.vlrcofinscrc + d.vlrcslcrc + d.vlrisscrc + d.descontocrc + d.descfinanceirocrc)) valor");
                    query.Append("         From crcdocto d");
                    query.Append("            , crcitdoc i");
                    query.Append("        Where d.coddoctocrc = i.coddoctocrc");
                    query.Append("          And d.statusdoctocrc = 'B'");

                    if (empresaConsolidar == "")
                    {
                        query.Append("           And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                        if (empresa == "029/001" && data.Year == 2019)
                            query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0') = '005/001')");
                        else
                            query.Append(")");
                    }
                    else
                    {
                        query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                        query.Append("           or lpad(codigoEmpresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                    }

                    query.Append("          And d.recebimentocrc = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("          And i.codtpreceita In (Select x.codtpreceita");
                    query.Append("                                   From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                    query.Append("                                   Where x.idcolbanco = c.Id");

                    if (empresaConsolidar == "")
                        query.Append("                                    And c.Idbanco = " + idBanco);
                    else
                        query.Append("                                   And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                    query.Append("          And c.Idcoluna = " + idColuna + ") Group by recebimentocrc");

                    query.Append("        Union all ");

                    query.Append("        Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data");
                    query.Append("          From bcomovto m");
                    query.Append("         Where m.docmovtobco not Like 'TB%'");

                    if (empresaConsolidar == "")
                    {
                        query.Append("           And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                        if (empresa == "029/001" && data.Year == 2019)
                            query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0') = '005/001')");
                        else
                            query.Append(")");
                    }
                    else
                    {
                        query.Append("          And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                        query.Append("           or lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                    }

                    query.Append("           And m.vlmovtobco > 0");

                    query.Append("           And m.statusmovtobco <> 'C'");
                    query.Append("           And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                    query.Append("           And m.Sistema = 'BCO'");

                    if (empresa == "029/001" && data.Year == 2019)
                    {
                        query.Append("           And (m.codbanco = 341 or m.codbanco = " + _banco.CodigoBanco + ")");
                        query.Append("           And (m.codagencia = 46 or m.codagencia = " + _banco.CodigoAgencia + ")");
                        query.Append("           And (m.codcontabco = '55793' or m.codcontabco = '" + _banco.CodigoConta + "')");
                    }
                    else
                    {
                        query.Append("           And m.codbanco = " + _banco.CodigoBanco);
                        query.Append("           And m.codagencia = " + _banco.CodigoAgencia);
                        query.Append("           And m.codcontabco = '" + _banco.CodigoConta + "'");
                    }

                    query.Append("           And m.codtpreceita In(Select x.codtpreceita");
                    query.Append("                                   From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                    query.Append("                                  Where x.idcolbanco = c.Id");

                    if (empresaConsolidar == "")
                        query.Append("                                And c.Idbanco = " + idBanco);
                    else
                        query.Append("                                And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                    query.Append("                                    And c.Idcoluna = " + idColuna + ")");

                    query.Append("         Group By m.dtefetivamovtobco");
                }
                query.Append(" ) ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _valor = _valor + Convert.ToDecimal(dataReader["Valor"].ToString());
                        _qdt = _qdt + Convert.ToInt32(dataReader["qtde"].ToString());
                    }
                    if (_valor != 0)
                    {
                        if (_qdt < quantidade)
                            _valor = _valor / _qdt;
                        else
                            _valor = _valor / quantidade;

                        if (aumentarReduzir == "A")
                            _valor = _valor + (_valor * (percetual / 100));
                        if (aumentarReduzir == "R")
                            _valor = _valor - (_valor * (percetual / 100));
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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarContaPagarDetalhada(string empresa, DateTime data, int quantidade, bool porMeses, 
            string aumentarReduzir, decimal percetual, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (porMeses)
                    data = data.AddMonths(-1);
                else
                    data = data.AddYears(-1);


                query.Append("Select Sum(valor) Valor, codtpDespesa, Pagamentocpg");
                query.Append("     , Docto, seriedoctocpg, statusdoctocpg, rsocialforn, coddoctoCPG");
                query.Append("  From (Select Sum(i.valoritemdoc) Valor, i.codtpDespesa, Pagamentocpg");// -- +d.acrescimoCPG - (d.vlrinssCPG + d.vlrirrfCPG + d.vlrpisCPG + d.vlrcofinsCPG + d.vlrcslCPG + d.vlrissCPG + d.descontoCPG + d.descfinanceiroCPG)) valor");
                query.Append("             , d.nrodoctocpg || '/' || d.nroparcelacpg Docto, d.seriedoctocpg, d.statusdoctocpg, f.rsocialforn, d.coddoctoCPG");
                query.Append("         From CPGdocto d");
                query.Append("            , CPGitdoc i, Bgm_Fornecedor f");
                query.Append("        Where d.coddoctoCPG = i.coddoctoCPG");
                query.Append("          And d.statusdoctoCPG = 'B'");
                query.Append("          And d.codigoforn = f.codigoforn");

                if (empresaConsolidar == "")
                    query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                else
                {
                    query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresaConsolidar + "')");
                }

                query.Append("          And d.Pagamentocpg = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("          And i.codtpDespesa In(Select x.codtpDespesa");
                query.Append("                      From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("          Where x.idcolbanco = c.Id");
                if (empresaConsolidar == "")
                    query.Append("                                    And c.Idbanco = " + idBanco);
                else
                    query.Append("                                    And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                query.Append("          And c.Idcoluna = " + idColuna + ")");
                query.Append("        Group by i.codtpDespesa, Pagamentocpg");
                query.Append("            , d.nrodoctocpg || '/' || d.nroparcelacpg , d.seriedoctocpg, d.statusdoctocpg, f.rsocialforn, d.coddoctoCPG");

                for (int i = 0; i < quantidade - 1; i++)
                {
                    if (porMeses)
                        data = data.AddMonths(-1);
                    else
                        data = data.AddYears(-1);

                    query.Append("        Union All ");
                    query.Append("       Select Sum(i.valoritemdoc) Valor, i.codtpDespesa, Pagamentocpg");// -- +d.acrescimoCPG - (d.vlrinssCPG + d.vlrirrfCPG + d.vlrpisCPG + d.vlrcofinsCPG + d.vlrcslCPG + d.vlrissCPG + d.descontoCPG + d.descfinanceiroCPG)) valor");
                    query.Append("            , d.nrodoctocpg || '/' || d.nroparcelacpg Docto, d.seriedoctocpg, d.statusdoctocpg, f.rsocialforn, d.coddoctoCPG");
                    query.Append("         From CPGdocto d");
                    query.Append("            , CPGitdoc i, Bgm_Fornecedor f");
                    query.Append("        Where d.coddoctoCPG = i.coddoctoCPG");
                    query.Append("          And d.codigoforn = f.codigoforn");
                    query.Append("          And d.statusdoctoCPG = 'B'");

                    if (empresaConsolidar == "")
                        query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    else
                    {
                        query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                        query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresaConsolidar + "')");
                    }

                    query.Append("          And d.Pagamentocpg = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("          And i.codtpDespesa In(Select x.codtpDespesa");
                    query.Append("                      From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                    query.Append("          Where x.idcolbanco = c.Id");
                    if (empresaConsolidar == "")
                        query.Append("                                    And c.Idbanco = " + idBanco);
                    else
                        query.Append("                                    And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                    query.Append("          And c.Idcoluna = " + idColuna + ")");
                    query.Append("        Group by i.codtpDespesa, Pagamentocpg");
                    query.Append("            , d.nrodoctocpg || '/' || d.nroparcelacpg, d.seriedoctocpg, d.statusdoctocpg, f.rsocialforn, d.coddoctoCPG");
                }
                query.Append(" ) Group by codtpDespesa, Pagamentocpg");
                query.Append("     , Docto, seriedoctocpg, statusdoctocpg, rsocialforn, coddoctoCPG");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["PagamentoCPG"].ToString());
                        _tipo.Auxiliar = dataReader["codtpDespesa"].ToString();
                        _tipo.Docto = dataReader["Docto"].ToString();
                        _tipo.Serie = dataReader["seriedoctocpg"].ToString();
                        _tipo.Status = dataReader["statusdoctocpg"].ToString();
                        _tipo.Razao = dataReader["rsocialforn"].ToString();
                        _tipo.IdDocto = Convert.ToDecimal(dataReader["coddoctoCPG"].ToString());

                        if (aumentarReduzir == "N")
                        {
                            _tipo.Realizado = _tipo.Previsto;
                            _tipo.Percentual = "Nenhum calculo";
                        }
                        if (aumentarReduzir == "A")
                        {
                            _tipo.Realizado = _tipo.Previsto + (_tipo.Previsto * (percetual / 100));
                            _tipo.Percentual = "Aumento de " + string.Format("{0:0.00}", percetual) + " %";
                        }
                        if (aumentarReduzir == "R")
                        {
                            _tipo.Realizado = _tipo.Previsto - (_tipo.Previsto * (percetual / 100));
                            _tipo.Percentual = "Redução de " + string.Format("{0:0.00}", percetual) + " %";
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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarContaPagarDetalhadaPeloVencimento(string empresa, DateTime data, int quantidade,
            bool porMeses, string aumentarReduzir, decimal percetual, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select Sum(i.valoritemdoc) Valor, i.codtpDespesa, vencimentoCPG, vencprorrogcpg");
                query.Append("     , d.nrodoctocpg || '/' || d.nroparcelacpg Docto, d.seriedoctocpg, d.statusdoctocpg, f.rsocialforn, d.coddoctoCPG");
                query.Append("  From CPGdocto d");
                query.Append("     , CPGitdoc i, Bgm_Fornecedor f");
                query.Append(" Where d.coddoctoCPG = i.coddoctoCPG");
                query.Append("   And d.statusdoctoCPG <> 'C'");
                query.Append("   And d.codigoforn = f.codigoforn");

                if (empresaConsolidar == "")
                    query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                else
                {
                    query.Append("          And (lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                    query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresaConsolidar + "')");
                }

                query.Append("   And d.vencimentoCPG = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And i.codtpDespesa In(Select x.codtpDespesa");
                query.Append("               From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("   Where x.idcolbanco = c.Id");
                if (empresaConsolidar == "")
                    query.Append("                                    And c.Idbanco = " + idBanco);
                else
                    query.Append("                                    And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");
                query.Append("   And c.Idcoluna = " + idColuna + ")");
                query.Append(" Group by i.codtpDespesa, vencimentoCPG, vencprorrogcpg");
                query.Append("     , d.nrodoctocpg || '/' || d.nroparcelacpg, d.seriedoctocpg, d.statusdoctocpg, f.rsocialforn, d.coddoctoCPG");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["vencimentoCPG"].ToString());
                        _tipo.VencimentoAnterior = Convert.ToDateTime(dataReader["vencprorrogcpg"].ToString());
                        _tipo.Auxiliar = dataReader["codtpDespesa"].ToString();
                        _tipo.Docto = dataReader["Docto"].ToString();
                        _tipo.Serie = dataReader["seriedoctocpg"].ToString();
                        _tipo.Status = dataReader["statusdoctocpg"].ToString();
                        _tipo.Razao = dataReader["rsocialforn"].ToString();
                        _tipo.IdDocto = Convert.ToDecimal(dataReader["coddoctoCPG"].ToString());
                        _tipo.IdColuna = idColuna;

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

        public decimal ConsultaTransferenciasPrevistas(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percentual, int codigoBCO, int codigoAgencia, string codigoConta, string Tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;

            try
            {
                if (porMeses)
                    data = data.AddMonths(-1);
                else
                    data = data.AddYears(-1);

                query.Append("Select Sum(Valor) Valor");
                query.Append("  From (Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data");
                query.Append("  From bcomovto m");
                query.Append(" Where lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                query.Append("   And m.docmovtobco Like 'TB%'");

                if (Tipo == "TS")
                    query.Append("   And m.vlmovtobco < 0");
                else
                    query.Append("   And m.vlmovtobco >= 0");

                query.Append("   And m.statusmovtobco <> 'C'");
                query.Append("   And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And m.codbanco = " + codigoBCO);
                query.Append("   And m.codagencia = " + codigoAgencia);
                query.Append("   And m.codcontabco = '" + codigoConta + "'");
                query.Append(" Group By m.dtefetivamovtobco");

                for (int i = 0; i < quantidade - 1; i++)
                {
                    if (porMeses)
                        data = data.AddMonths(-1);
                    else
                        data = data.AddYears(-1);

                    query.Append(" Union All ");
                    query.Append("Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data");
                    query.Append("  From bcomovto m");
                    query.Append(" Where lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    query.Append("   And m.docmovtobco Like 'TB%'");

                    if (Tipo == "TS")
                        query.Append("   And m.vlmovtobco < 0");
                    else
                        query.Append("   And m.vlmovtobco >= 0");


                    query.Append("   And m.statusmovtobco <> 'C'");
                    query.Append("   And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   And m.codbanco = " + codigoBCO);
                    query.Append("   And m.codagencia = " + codigoAgencia);
                    query.Append("   And m.codcontabco = '" + codigoConta + "'");
                    query.Append(" Group By m.dtefetivamovtobco ");
                }
                query.Append(" )");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _valor = _valor / quantidade;

                        if (aumentarReduzir == "A")
                            _valor = _valor + (_valor * (percentual / 100));
                        if (aumentarReduzir == "R")
                            _valor = _valor - (_valor * (percentual / 100));

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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultaTransferenciasPrevistasDetalhada(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percentual, int codigoBCO, int codigoAgencia, string codigoConta, string Tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (porMeses)
                    data = data.AddMonths(-1);
                else
                    data = data.AddYears(-1);

                query.Append("Select Sum(Valor) Valor, data ");
                query.Append("  From (Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data");
                query.Append("  From bcomovto m");
                query.Append(" Where lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                query.Append("   And m.docmovtobco Like 'TB%'");

                if (Tipo == "TS")
                    query.Append("   And m.vlmovtobco < 0");
                else
                    query.Append("   And m.vlmovtobco >= 0");

                query.Append("   And m.statusmovtobco <> 'C'");
                query.Append("   And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And m.codbanco = " + codigoBCO);
                query.Append("   And m.codagencia = " + codigoAgencia);
                query.Append("   And m.codcontabco = '" + codigoConta + "'");
                query.Append(" Group By m.dtefetivamovtobco");

                for (int i = 0; i < quantidade - 1; i++)
                {
                    if (porMeses)
                        data = data.AddMonths(-1);
                    else
                        data = data.AddYears(-1);

                    query.Append(" Union All ");
                    query.Append("Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data");
                    query.Append("  From bcomovto m");
                    query.Append(" Where lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    query.Append("   And m.docmovtobco Like 'TB%'");

                    if (Tipo == "TS")
                        query.Append("   And m.vlmovtobco < 0");
                    else
                        query.Append("   And m.vlmovtobco >= 0");


                    query.Append("   And m.statusmovtobco <> 'C'");
                    query.Append("   And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   And m.codbanco = " + codigoBCO);
                    query.Append("   And m.codagencia = " + codigoAgencia);
                    query.Append("   And m.codcontabco = '" + codigoConta + "'");
                    query.Append(" Group By m.dtefetivamovtobco ");

                }
                query.Append(" ) group by data");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());

                        if (aumentarReduzir == "N")
                        {
                            _tipo.Realizado = _tipo.Previsto;
                            _tipo.Percentual = "Nenhum calculo";
                        }
                        if (aumentarReduzir == "A")
                        {
                            _tipo.Realizado = _tipo.Previsto + (_tipo.Previsto * (percentual / 100));
                            _tipo.Percentual = "Aumento de " + string.Format("{0:0.00}", percentual) + " %";
                        }
                        if (aumentarReduzir == "R")
                        {
                            _tipo.Realizado = _tipo.Previsto - (_tipo.Previsto * (percentual / 100));
                            _tipo.Percentual = "Redução de " + string.Format("{0:0.00}", percentual) + " %";
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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultaTransferenciasPrevistasNaDataDetalhada(string empresa, DateTime data, int codbanco, int codAgencia, string codConta, string Tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Abs(Sum(m.vlmovtobco)) Valor, m.dtmovtobco Data, m.histmovtobco, docmovtobco");
                query.Append("  From bcomovto m");
                query.Append(" Where lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                query.Append("   And m.docmovtobco Like 'TB%'");

                if (Tipo == "TS")
                    query.Append("   And m.vlmovtobco < 0");
                else
                    query.Append("   And m.vlmovtobco >= 0");


                query.Append("   And m.statusmovtobco <> 'C'");
                query.Append("   And m.dtmovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And m.codbanco = " + codbanco);
                query.Append("   And m.codagencia = " + codAgencia);
                query.Append("   And m.codcontabco = '" + codConta + "'");
                query.Append(" Group By m.dtmovtobco, m.histmovtobco, docmovtobco ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        _tipo.Auxiliar = dataReader["histmovtobco"].ToString();
                        _tipo.Docto = dataReader["docmovtobco"].ToString();

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

        public decimal ConsultaMovtoBCOPrevisto(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percentual, int codigoBCO, int codigoAgencia, 
            string codigoConta, string Tipo, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            Financeiro.Bancos _banco = ConsultarBancos(idBanco);

            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;

            try
            {
                if (porMeses)
                    data = data.AddMonths(-1);
                else
                    data = data.AddYears(-1);

                query.Append("Select Sum(Valor) Valor");
                query.Append("  From (Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data");
                query.Append("  From bcomovto m");
                query.Append(" Where m.docmovtobco not Like 'TB%'");

                if (empresaConsolidar == "")
                {
                    query.Append("   And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    if (empresa == "029/001" && data.Year == 2019)
                        query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0') = '005/001')");
                    else
                        query.Append(")");
                }
                else
                {
                    query.Append("   And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    query.Append("    or lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }

                if (Tipo == "SA")
                {
                    query.Append("   And m.vlmovtobco < 0");
                    query.Append("   And m.codtpdespesa In(Select x.codtpdespesa");
                    query.Append("                           From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                    query.Append("                          Where x.idcolbanco = c.Id");

                    if (empresaConsolidar == "")
                        query.Append("                            And c.Idbanco = " + idBanco);
                    else
                        query.Append("                            And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                    query.Append("                            And c.Idcoluna = " + idColuna + ")");
                }
                else
                {
                    query.Append("   And m.vlmovtobco >= 0");
                    query.Append("   And m.codtpreceita In(Select x.codtpreceita");
                    query.Append("                           From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                    query.Append("                          Where x.idcolbanco = c.Id");

                    if (empresaConsolidar == "")
                        query.Append("                            And c.Idbanco = " + idBanco);
                    else
                        query.Append("                            And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                    query.Append("                            And c.Idcoluna = " + idColuna + ")");
                }

                query.Append("   And m.Sistema = 'BCO'");
                query.Append("   And m.statusmovtobco <> 'C'");
                query.Append("   And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                if (empresa == "029/001" && data.Year == 2019)
                {
                    query.Append("           And (m.codbanco = 341 or m.codbanco = " + codigoBCO + ")");
                    query.Append("           And (m.codagencia = 46 or m.codagencia = " + codigoAgencia + ")");
                    query.Append("           And (m.codcontabco = '55793' or m.codcontabco = '" + codigoConta + "')");
                }
                else
                {
                    if (_banco.CodigoBancoCartoes != 0)
                    {
                        query.Append("           And (m.codbanco = " + codigoBCO + " or m.codbanco = " + _banco.CodigoBancoCartoes + ")");
                        query.Append("           And (m.codagencia = " + codigoAgencia + " or m.codagencia = " + _banco.CodigoAgenciaCartoes + ")");
                        query.Append("           And (m.codcontabco = '" + codigoConta + "' or m.codcontabco = '" + _banco.CodigoContaCartoes + "')");
                    }
                    else
                    {
                        query.Append("           And m.codbanco = " + codigoBCO);
                        query.Append("           And m.codagencia = " + codigoAgencia);
                        query.Append("           And m.codcontabco = '" + codigoConta + "'");
                    }
                }
                query.Append(" Group By m.dtefetivamovtobco");

                for (int i = 0; i < quantidade - 1; i++)
                {
                    if (porMeses)
                        data = data.AddMonths(-1);
                    else
                        data = data.AddYears(-1);

                    query.Append(" Union All ");
                    query.Append("Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data");
                    query.Append("  From bcomovto m");
                    query.Append(" Where m.docmovtobco not Like 'TB%'");

                    if (empresaConsolidar == "")
                    {
                        query.Append("           And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                        if (empresa == "029/001" && data.Year == 2019)
                            query.Append("           Or lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0') = '005/001')");
                        else
                            query.Append(")");
                    }
                    else
                    {
                        query.Append("   And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                        query.Append("    or lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                    }

                    if (Tipo == "SA")
                    {
                        query.Append("   And m.vlmovtobco < 0");
                        query.Append("   And m.codtpdespesa In(Select x.codtpdespesa");
                        query.Append("                           From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                        query.Append("                          Where x.idcolbanco = c.Id");

                        if (empresaConsolidar == "")
                            query.Append("                            And c.Idbanco = " + idBanco);
                        else
                            query.Append("                            And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                        query.Append("                            And c.Idcoluna = " + idColuna + ")");
                    }
                    else
                    {
                        query.Append("   And m.vlmovtobco >= 0");
                        query.Append("   And m.codtpreceita In(Select x.codtpreceita");
                        query.Append("                           From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                        query.Append("                          Where x.idcolbanco = c.Id");

                        if (empresaConsolidar == "")
                            query.Append("                            And c.Idbanco = " + idBanco);
                        else
                            query.Append("                            And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                        query.Append("                            And c.Idcoluna = " + idColuna + ")");
                    }

                    query.Append("   And m.Sistema = 'BCO'");
                    query.Append("   And m.statusmovtobco <> 'C'");
                    query.Append("   And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                    if (empresa == "029/001" && data.Year == 2019)
                    {
                        query.Append("           And (m.codbanco = 341 or m.codbanco = " + codigoBCO + ")");
                        query.Append("           And (m.codagencia = 46 or m.codagencia = " + codigoAgencia + ")");
                        query.Append("           And (m.codcontabco = '55793' or m.codcontabco = '" + codigoConta + "')");
                    }
                    else
                    {
                        if (_banco.CodigoBancoCartoes != 0)
                        {
                            query.Append("           And (m.codbanco = " + codigoBCO + " or m.codbanco = " + _banco.CodigoBancoCartoes + ")");
                            query.Append("           And (m.codagencia = " + codigoAgencia + " or m.codagencia = " + _banco.CodigoAgenciaCartoes + ")");
                            query.Append("           And (m.codcontabco = '" + codigoConta + "' or m.codcontabco = '" + _banco.CodigoContaCartoes + "')");
                        }
                        else
                        {
                            query.Append("           And m.codbanco = " + codigoBCO);
                            query.Append("           And m.codagencia = " + codigoAgencia);
                            query.Append("           And m.codcontabco = '" + codigoConta + "'");
                        }
                    }

                    query.Append(" Group By m.dtefetivamovtobco ");
                }
                query.Append(" )");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _valor = _valor / quantidade;

                        if (aumentarReduzir == "A")
                            _valor = _valor + (_valor * (percentual / 100));
                        if (aumentarReduzir == "R")
                            _valor = _valor - (_valor * (percentual / 100));
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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultaMovtoBCOPrevistoDetalhada(string empresa, DateTime data, int quantidade, bool porMeses, 
            string aumentarReduzir, decimal percentual, int codigoBCO, int codigoAgencia, string codigoConta, string Tipo, int idBanco, int idColuna,
            string empresaConsolidar, int idBancoConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (porMeses)
                    data = data.AddMonths(-1);
                else
                    data = data.AddYears(-1);

                query.Append("Select Sum(Valor) Valor, data ");
                query.Append("  From (Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data");
                query.Append("  From bcomovto m");
                query.Append(" Where m.docmovtobco not Like 'TB%'");

                if (empresaConsolidar == "")
                    query.Append("   And lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                else
                {
                    query.Append("   And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    query.Append("    or lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }

                if (Tipo == "SA")
                    if (Tipo == "SA")
                    {
                        query.Append("   And m.vlmovtobco < 0");
                        query.Append("   And m.codtpdespesa In(Select x.codtpdespesa");
                        query.Append("                           From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                        query.Append("                          Where x.idcolbanco = c.Id");

                        if (empresaConsolidar == "")
                            query.Append("                            And c.Idbanco = " + idBanco);
                        else
                            query.Append("                            And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                        query.Append("                            And c.Idcoluna = " + idColuna + ")");
                    }
                    else
                    {
                        query.Append("   And m.vlmovtobco >= 0");
                        query.Append("   And m.codtpreceita In(Select x.codtpreceita");
                        query.Append("                           From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                        query.Append("                          Where x.idcolbanco = c.Id");

                        if (empresaConsolidar == "")
                            query.Append("                            And c.Idbanco = " + idBanco);
                        else
                            query.Append("                            And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                        query.Append("                            And c.Idcoluna = " + idColuna + ")");
                    }

                query.Append("   And m.statusmovtobco <> 'C'");
                query.Append("   And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And m.codbanco = " + codigoBCO);
                query.Append("   And m.codagencia = " + codigoAgencia);
                query.Append("   And m.codcontabco = '" + codigoConta + "'");
                
                query.Append(" Group By m.dtefetivamovtobco");

                for (int i = 0; i < quantidade - 1; i++)
                {
                    if (porMeses)
                        data = data.AddMonths(-1);
                    else
                        data = data.AddYears(-1);

                    query.Append(" Union All ");
                    query.Append("Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data");
                    query.Append("  From bcomovto m");
                    query.Append(" Where m.docmovtobco not Like 'TB%'");

                    if (empresaConsolidar == "")
                        query.Append("   And lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    else
                    {
                        query.Append("   And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                        query.Append("    or lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                    }

                    if (Tipo == "SA")
                        query.Append("   And m.vlmovtobco < 0");
                    else
                        query.Append("   And m.vlmovtobco >= 0");


                    query.Append("   And m.statusmovtobco <> 'C'");
                    query.Append("   And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   And m.codbanco = " + codigoBCO);
                    query.Append("   And m.codagencia = " + codigoAgencia);
                    query.Append("   And m.codcontabco = '" + codigoConta + "'");

                    query.Append("   And m.codtpreceita In(Select x.codtpreceita");
                    query.Append("                           From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                    query.Append("                          Where x.idcolbanco = c.Id");

                    if (empresaConsolidar == "")
                        query.Append("                            And c.Idbanco = " + idBanco);
                    else
                        query.Append("                            And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                    query.Append("                            And c.Idcoluna = " + idColuna + ")");

                    query.Append(" Group By m.dtefetivamovtobco ");

                }
                query.Append(" ) group by data");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());

                        if (aumentarReduzir == "N")
                        {
                            _tipo.Realizado = _tipo.Previsto;
                            _tipo.Percentual = "Nenhum calculo";
                        }
                        if (aumentarReduzir == "A")
                        {
                            _tipo.Realizado = _tipo.Previsto + (_tipo.Previsto * (percentual / 100));
                            _tipo.Percentual = "Aumento de " + string.Format("{0:0.00}", percentual) + " %";
                        }
                        if (aumentarReduzir == "R")
                        {
                            _tipo.Realizado = _tipo.Previsto - (_tipo.Previsto * (percentual / 100));
                            _tipo.Percentual = "Redução de " + string.Format("{0:0.00}", percentual) + " %";
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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultaMovtoBCOPrevistoNaDataDetalhada(string empresa, DateTime data, int codbanco, int codAgencia,
            string codConta, string Tipo, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Abs(Sum(m.vlmovtobco)) Valor, m.dtmovtobco Data, m.histmovtobco, docmovtobco");
                query.Append("  From bcomovto m");
                query.Append(" Where m.docmovtobco not Like 'TB%'");

                if (empresaConsolidar == "")
                    query.Append("   And lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                else
                {
                    query.Append("   And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    query.Append("    or lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }
                

                    if (Tipo == "SA")
                    {
                        query.Append("   And m.vlmovtobco < 0");
                        query.Append("   And m.codtpdespesa In(Select x.codtpdespesa");
                        query.Append("                           From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                        query.Append("                          Where x.idcolbanco = c.Id");

                        if (empresaConsolidar == "")
                            query.Append("                            And c.Idbanco = " + idBanco);
                        else
                            query.Append("                            And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                        query.Append("                            And c.Idcoluna = " + idColuna + ")");
                    }
                    else
                    {
                        query.Append("   And m.vlmovtobco >= 0");
                        query.Append("   And m.codtpreceita In(Select x.codtpreceita");
                        query.Append("                           From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                        query.Append("                          Where x.idcolbanco = c.Id");

                        if (empresaConsolidar == "")
                            query.Append("                            And c.Idbanco = " + idBanco);
                        else
                            query.Append("                            And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                        query.Append("                            And c.Idcoluna = " + idColuna + ")");
                    }


                query.Append("   And m.statusmovtobco <> 'C'");
                query.Append("   And m.dtmovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And m.codbanco = " + codbanco);
                query.Append("   And m.codagencia = " + codAgencia);
                query.Append("   And m.codcontabco = '" + codConta + "'");

                query.Append(" Group By m.dtmovtobco, m.histmovtobco, docmovtobco ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        _tipo.Auxiliar = dataReader["histmovtobco"].ToString();
                        _tipo.Docto = dataReader["docmovtobco"].ToString();

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


        #endregion

        #region Realizado

        public decimal ConsultarReceitaRealizada(string empresa, DateTime data, string tipoLinha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Variaveis _tipo = new Financeiro.Variaveis();
            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;
            try
            {
                query.Append("Select Sum(vlr_receb) Valor");
                query.Append("  From (Select Sum(d.vlr_receb) Vlr_Receb");
                query.Append("         from t_arr_Guia g");
                query.Append("            , t_Arr_Detalhe_Guia d");
                query.Append("            , t_Trf_Tipopagto t, Bgm_Cadlinhas L");
                query.Append("        Where g.cod_seq_guia = d.cod_seq_guia");
                query.Append("          And d.cod_tipopagtarifa = t.cod_tipopagto");
                query.Append("          And t.flg_normal = 'S'");
                query.Append("          And lpad(Cod_empresa,3,'0') || '/' || lpad(g.CodigoFl,3,'0')= '" + empresa + "'");
                query.Append("          And g.dat_viagem_guia = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("          And l.codintlinha = d.codintlinha");

                if (tipoLinha == "R")
                    query.Append("          And l.codigotplinha = 0");
                else
                {
                    if (tipoLinha == "M")
                        query.Append("          And l.flg_munic_interest = 'M'");
                    else
                        if (tipoLinha == "I")
                        query.Append("          And l.flg_munic_interest = 'U'");
                }
                query.Append(" )");

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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarReceitaRealizadaDetalhada(string empresa, DateTime data, string tipoLinha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();

            Publicas.mensagemDeErro = string.Empty;
            
            try
            {
                query.Append("Select Sum(d.vlr_receb) Valor, dat_viagem_guia, COD_GUIA ");
                query.Append("  from t_arr_Guia g");
                query.Append("     , t_Arr_Detalhe_Guia d");
                query.Append("     , t_Trf_Tipopagto t, Bgm_Cadlinhas L");
                query.Append(" Where g.cod_seq_guia = d.cod_seq_guia");
                query.Append("   And d.cod_tipopagtarifa = t.cod_tipopagto");
                query.Append("   And t.flg_normal = 'S'");
                query.Append("   And lpad(Cod_empresa,3,'0') || '/' || lpad(g.CodigoFl,3,'0')= '" + empresa + "'");
                query.Append("   And g.dat_viagem_guia = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And l.codintlinha = d.codintlinha");

                if (tipoLinha == "R")
                    query.Append("          And l.codigotplinha = 0");
                else
                {
                    if (tipoLinha == "M")
                        query.Append("          And l.flg_munic_interest = 'M'");
                    else
                        if (tipoLinha == "I")
                        query.Append("          And l.flg_munic_interest = 'U'");
                }
                query.Append(" group by dat_viagem_guia, COD_GUIA  Having Sum(d.Vlr_Receb)  > 0");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["dat_viagem_guia"].ToString());
                        _tipo.Percentual = dataReader["COD_GUIA"].ToString();

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

        public decimal ConsultarContasReceberRealizada(string empresa, DateTime data, int idBanco, int idColuna)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Variaveis _tipo = new Financeiro.Variaveis();
            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;
            try
            {
                query.Append("Select Sum(valor) Valor");
                query.Append("  From (Select Sum(i.valoritemdoc) Valor ");// -- +d.acrescimocrc - (d.vlrinsscrc + d.vlrirrfcrc + d.vlrpiscrc + d.vlrcofinscrc + d.vlrcslcrc + d.vlrisscrc + d.descontocrc + d.descfinanceirocrc)) valor");
                query.Append("         rom crcdocto d");
                query.Append("            , crcitdoc i");
                query.Append("        Where d.coddoctocrc = i.coddoctocrc");
                query.Append("          And d.statusdoctocrc = 'B'");
                query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                query.Append("          And d.recebimentocrc = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("          And i.codtpreceita In(Select x.codtpreceita");
                query.Append("                                  From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                                 Where x.idcolbanco = c.Id");
                query.Append("                                   And c.Idbanco = " + idBanco);
                query.Append("                                   And c.Idcoluna = " + idColuna + ")");
                query.Append(" )");

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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarContasReceberRealizadaDetalhada(string empresa, DateTime data, int idBanco, int idColuna)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();

            Publicas.mensagemDeErro = string.Empty;
            
            try
            {
                query.Append("Select Sum(valor) Valor, recebimentocrc, codtpreceita");
                query.Append("            , Docto, seriedoctocrc, statusdoctocrc, rsocialcli, coddoctocrc");

                query.Append("  From (Select Sum(i.valoritemdoc) Valor, recebimentocrc, i.codtpreceita");// -- +d.acrescimocrc - (d.vlrinsscrc + d.vlrirrfcrc + d.vlrpiscrc + d.vlrcofinscrc + d.vlrcslcrc + d.vlrisscrc + d.descontocrc + d.descfinanceirocrc)) valor");
                query.Append("            , d.nrodoctocrc || '/' || d.nroparcelacrc Docto, d.seriedoctocrc, d.statusdoctocrc, c.rsocialcli, d.coddoctocrc");
                query.Append("         From crcdocto d");
                query.Append("            , crcitdoc i, bgm_cliente c");
                query.Append("        Where d.coddoctocrc = i.coddoctocrc");
                query.Append("          And c.codcli = d.codcli");
                query.Append("          And d.statusdoctocrc = 'B'");
                query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                query.Append("          And d.recebimentocrc = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("          And i.codtpreceita In(Select x.codtpreceita");
                query.Append("                                  From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                                 Where x.idcolbanco = c.Id");
                query.Append("                                   And c.Idbanco = " + idBanco);
                query.Append("                                   And c.Idcoluna = " + idColuna + ") group by recebimentocrc, i.codtpreceita");
                query.Append("            , d.nrodoctocrc || '/' || d.nroparcelacrc, d.seriedoctocrc, d.statusdoctocrc, c.rsocialcli, d.coddoctocrc");
                query.Append(" ) Group by Recebimentocrc, Codtpreceita");
                query.Append("       , Docto, seriedoctocrc, statusdoctocrc, rsocialcli, coddoctocrc");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["recebimentocrc"].ToString());
                        _tipo.Auxiliar = dataReader["codtpreceita"].ToString();
                        _tipo.Docto = dataReader["Docto"].ToString();
                        _tipo.Serie = dataReader["seriedoctocrc"].ToString();
                        _tipo.Status = dataReader["statusdoctocrc"].ToString();
                        _tipo.Razao = dataReader["rsocialcli"].ToString();
                        _tipo.IdDocto = Convert.ToDecimal(dataReader["coddoctocrc"].ToString());

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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarContasPagarRealizadaDetalhada(string empresa, DateTime data, int idBanco, int idColuna)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();

            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Sum(valor) Valor, codtpdespesa, pagamentocpg, docto,seriedoctocpg, statusdoctocpg, rsocialforn, coddoctocpg  ");
                query.Append("  From (Select Sum(i.valoritemdoc) Valor, i.codtpdespesa, pagamentocpg");// -- +d.acrescimocpg - (d.vlrinsscpg + d.vlrirrfcpg + d.vlrpiscpg + d.vlrcofinscpg + d.vlrcslcpg + d.vlrisscpg + d.descontocpg + d.descfinanceirocpg)) valor");
                query.Append("             , d.nrodoctocpg || '/' || d.nroparcelacpg Docto, d.seriedoctocpg, d.statusdoctocpg, f.rsocialforn, d.coddoctocpg ");
                query.Append("         From CPGdocto d");
                query.Append("            , CPGitdoc i, Bgm_Fornecedor f");
                query.Append("        Where d.coddoctocpg = i.coddoctocpg");
                query.Append("          And d.statusdoctocpg = 'B'");
                query.Append("          And d.codigoforn = f.codigoforn");
                query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                query.Append("          And d.pagamentocpg = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("          And i.codtpdespesa In(Select x.codtpdespesa");
                query.Append("                                  From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                                 Where x.idcolbanco = c.Id");
                query.Append("                                   And c.Idbanco = " + idBanco);
                query.Append("                                   And c.Idcoluna = " + idColuna + ") group by pagamentocpg, i.codtpdespesa ");
                query.Append("             , d.nrodoctocpg || '/' || d.nroparcelacpg, d.seriedoctocpg, d.statusdoctocpg, f.rsocialforn, d.coddoctocpg ");
                query.Append(" ) group by codtpdespesa, pagamentocpg, docto, seriedoctocpg, statusdoctocpg, rsocialforn, coddoctocpg ");
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["pagamentocpg"].ToString());
                        _tipo.Auxiliar = dataReader["codtpDespesa"].ToString();
                        _tipo.Docto = dataReader["Docto"].ToString();
                        _tipo.Serie = dataReader["seriedoctocpg"].ToString();
                        _tipo.Status = dataReader["statusdoctocpg"].ToString();
                        _tipo.Razao = dataReader["rsocialforn"].ToString();
                        _tipo.IdDocto = Convert.ToDecimal(dataReader["coddoctocpg"].ToString());

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

        public decimal ConsultarContasPagarRealizada(string empresa, DateTime data, int idBanco, int idColuna)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.Variaveis _tipo = new Financeiro.Variaveis();
            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;
            try
            {
                query.Append("Select Sum(valor) Valor");
                query.Append("  From (Select Sum(i.valoritemdoc) Valor");// -- +d.acrescimocpg - (d.vlrinsscpg + d.vlrirrfcpg + d.vlrpiscpg + d.vlrcofinscpg + d.vlrcslcpg + d.vlrisscpg + d.descontocpg + d.descfinanceirocpg)) valor");
                query.Append("         From CPGdocto d");
                query.Append("            , CPGitdoc i");
                query.Append("        Where d.coddoctocpg = i.coddoctocpg");
                query.Append("          And d.statusdoctocpg = 'B'");
                query.Append("          And lpad(codigoEmpresa,3,'0') || '/' || lpad(CodigoFl,3,'0')= '" + empresa + "'");
                query.Append("          And d.pagamentocpg = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("          And i.codtpdespesa In(Select x.codtpdespesa");
                query.Append("                                  From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                                 Where x.idcolbanco = c.Id");
                query.Append("                                   And c.Idbanco = " + idBanco);
                query.Append("                                   And c.Idcoluna = " + idColuna + ")");
                query.Append(" )");

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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarTransferenciasRealizadaDetalhada(string empresa, DateTime data, int codbanco, int codAgencia, string codConta, 
            string tipo, string empresaConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Abs(Sum(m.vlmovtobco)) Valor, m.dtefetivamovtobco Data, m.histmovtobco, docmovtobco");
                query.Append("  From bcomovto m");
                query.Append(" Where m.docmovtobco Like 'TB%'");

                if (empresaConsolidar == "")
                    query.Append("   And lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                else
                {
                    query.Append("   And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    query.Append("    Or lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }

                if (tipo == "TS")
                    query.Append("   And m.vlmovtobco < 0");
                else
                    query.Append("   And m.vlmovtobco >= 0");


                query.Append("   And m.statusmovtobco <> 'C'");
                query.Append("   And m.dtefetivamovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And m.codbanco = " + codbanco);
                query.Append("   And m.codagencia = " + codAgencia);
                query.Append("   And m.codcontabco = '" + codConta + "'");
                query.Append(" Group By m.dtefetivamovtobco, m.histmovtobco, docmovtobco ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        _tipo.Auxiliar = dataReader["histmovtobco"].ToString();
                        _tipo.Docto = dataReader["docmovtobco"].ToString();


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

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultaMovtoBCORealizadoNaDataDetalhada(string empresa, DateTime data, int codbanco, int codAgencia, string codConta, 
            string Tipo, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ColunasDemonstrativo _tipo = new Financeiro.ColunasDemonstrativo();
            List<Financeiro.ColunasDemonstrativo> _lista = new List<Financeiro.ColunasDemonstrativo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Abs(Sum(m.vlmovtobco)) Valor, m.dtmovtobco Data, m.histmovtobco, docmovtobco");
                query.Append("  From bcomovto m");
                query.Append(" Where m.docmovtobco Like 'TB%'");

                if (empresaConsolidar == "")
                    query.Append("   And lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                else
                {
                    query.Append("   And (lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                    query.Append("    Or lpad(codigoempresa, 3, '0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresaConsolidar + "')");
                }

                if (Tipo == "SA")
                    query.Append("   And m.vlmovtobco < 0");
                else
                    query.Append("   And m.vlmovtobco >= 0");


                query.Append("   And m.statusmovtobco <> 'C'");
                query.Append("   And m.dtmovtobco = to_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And m.codbanco = " + codbanco);
                query.Append("   And m.codagencia = " + codAgencia);
                query.Append("   And m.codcontabco = '" + codConta + "'");

                query.Append("   And m.codtpreceita In(Select x.codtpreceita");
                query.Append("                           From niff_fin_despreccolunas x, niff_fin_colunasbanco c");
                query.Append("                          Where x.idcolbanco = c.Id");
                if (empresaConsolidar == "")
                    query.Append("                            And c.Idbanco = " + idBanco);
                else
                    query.Append("                            And (c.Idbanco = " + idBanco + " or c.Idbanco = " + idBancoConsolidar + ")");

                query.Append("                            And c.Idcoluna = " + idColuna + ")");

                query.Append(" Group By m.dtmovtobco, m.histmovtobco, docmovtobco ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _tipo = new Financeiro.ColunasDemonstrativo();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Valor"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        _tipo.Auxiliar = dataReader["histmovtobco"].ToString();
                        _tipo.Docto = dataReader["docmovtobco"].ToString();

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

        #endregion

        #endregion

        #region Dados bancarios Globus

        public List<Financeiro.BancosGlobus> ListarBancosGlobus(string empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.BancosGlobus> _lista = new List<Financeiro.BancosGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select b.codbanco, b.nrobanco, b.nomebanco");
                query.Append("  From Bcobanco b, bcoconta c");
                query.Append(" Where b.codbanco = c.codbanco");
                query.Append("   And lpad(codigoempresa,3,'0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Financeiro.BancosGlobus centro = new Financeiro.BancosGlobus();

                        centro.Codigo = Convert.ToInt32(dataReader["codbanco"].ToString());
                        centro.Numero = Convert.ToInt32(dataReader["nrobanco"].ToString());
                        centro.Nome = dataReader["nomebanco"].ToString();
                        centro.Existe = true;
                        _lista.Add(centro);
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

        public Financeiro.BancosGlobus ConsultaBancosGlobus(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.BancosGlobus centro = new Financeiro.BancosGlobus();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select codbanco, nrobanco, nomebanco");
                query.Append("  From Bcobanco c");
                query.Append(" where nrobanco = " + codigo);
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        centro.Codigo = Convert.ToInt32(dataReader["codbanco"].ToString());
                        centro.Numero = Convert.ToInt32(dataReader["nrobanco"].ToString());
                        centro.Nome = dataReader["nomebanco"].ToString();
                        centro.Existe = true;
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
            return centro;
        }

        public List<Financeiro.AgenciaGlobus> ListarAgenciaGlobus(int banco, string empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.AgenciaGlobus> _lista = new List<Financeiro.AgenciaGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select a.codbanco, a.codagencia, a.nomeagencbco");
                query.Append("  From bcoagenc a, bcoconta c");
                query.Append(" where a.codbanco = " + banco);
                query.Append("   And a.codbanco = c.codbanco");
                query.Append("   And a.codagencia = c.codagencia");
                query.Append("   And lpad(codigoempresa,3,'0') || '/' || lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Financeiro.AgenciaGlobus centro = new Financeiro.AgenciaGlobus();

                        centro.Banco = Convert.ToInt32(dataReader["codbanco"].ToString());
                        centro.Codigo = Convert.ToInt32(dataReader["codagencia"].ToString());
                        centro.Nome = dataReader["nomeagencbco"].ToString();
                        centro.Existe = true;
                        _lista.Add(centro);
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

        public Financeiro.AgenciaGlobus ConsultaAgenciaGlobus(int banco, int agencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.AgenciaGlobus centro = new Financeiro.AgenciaGlobus();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select codbanco, codagencia, nomeagencbco");
                query.Append("  From bcoagenc c");
                query.Append(" where codbanco = " + banco);
                query.Append("   and codagencia = " + agencia);
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        centro.Banco = Convert.ToInt32(dataReader["codbanco"].ToString());
                        centro.Codigo = Convert.ToInt32(dataReader["codagencia"].ToString());
                        centro.Nome = dataReader["nomeagencbco"].ToString();
                        centro.Existe = true;
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
            return centro;
        }

        public List<Financeiro.ContaGlobus> ListarContasGlobus(string empresa, int banco, int agencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.ContaGlobus> _lista = new List<Financeiro.ContaGlobus>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select codbanco, codagencia, codcontabco, nomecontabco, conta_ativa");
                query.Append("  From bcoconta c");
                query.Append(" where codbanco = " + banco);
                query.Append("   and codagencia = " + agencia);
                query.Append("   and lpad(codigoempresa,3,'0') || '/' || lpad(CodigoFl,3,'0') = '" + empresa + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Financeiro.ContaGlobus centro = new Financeiro.ContaGlobus();

                        centro.Banco = Convert.ToInt32(dataReader["codbanco"].ToString());
                        centro.Agencia = Convert.ToInt32(dataReader["codagencia"].ToString());
                        centro.Conta = dataReader["codcontabco"].ToString();
                        centro.Nome = dataReader["nomecontabco"].ToString();
                        centro.Ativa = dataReader["conta_ativa"].ToString() == "S";
                        centro.Existe = true;
                        _lista.Add(centro);
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

        public Financeiro.ContaGlobus ConsultaContasGlobus(string empresa, int banco, int agencia, string conta)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Financeiro.ContaGlobus centro = new Financeiro.ContaGlobus();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select codbanco, codagencia, codcontabco, nomecontabco, conta_ativa");
                query.Append("  From bcoconta c");
                query.Append(" where codbanco = " + banco);
                query.Append("   and codagencia = " + agencia);
                query.Append("   and codcontabco = '" + conta + "'");
                query.Append("   and lpad(codigoempresa,3,'0') || '/' || lpad(CodigoFl,3,'0') = '" + empresa + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        centro.Banco = Convert.ToInt32(dataReader["codbanco"].ToString());
                        centro.Agencia = Convert.ToInt32(dataReader["codagencia"].ToString());
                        centro.Conta = dataReader["codcontabco"].ToString();
                        centro.Nome = dataReader["nomecontabco"].ToString();
                        centro.Ativa = dataReader["conta_ativa"].ToString() == "S";

                        centro.Existe = true;
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
            return centro;
        }
        #endregion

        #region Resumo
        public List<Financeiro.Resumo> ListarResumo(string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Financeiro.Resumo>  _lista = new List<Financeiro.Resumo>();
            Publicas.mensagemDeErro = string.Empty;

            DateTime _inicio = Convert.ToDateTime("01/" + referencia.Substring(4,2) + "/" + referencia.Substring(0,4));
            DateTime _fim = _inicio.AddMonths(1).AddDays(-1);
            DateTime _data = _inicio;
            decimal _valor = 0;

            bool encontrou = false;
            try
            {
                // Busca a movimentação diaria por empresa e consolidado
                while (_data <= _fim)
                {
                    query.Clear();
                    query.Append("Select e.Nomeabreviado, e.Idempresa");
                    query.Append("     , Decode(c.tipo, 'EN', 'Receitas', 'SA', 'Pagamentos', 'Transferências') Descricao");
                    query.Append("     , Decode(c.tipo, 'EN', 1, 'SA', 2, 3) Ordem");
                    query.Append("     , v.data");
                    query.Append("     , Sum(Decode(c.tipo, 'EN', 1, 'SA', -1, 'TE', 1, -1) * ");
                    query.Append("       Case When (Select Max(realizadobco) ");
                    query.Append("                    From Niff_Fin_Demonstrativo d1, Niff_Fin_Coldemonstrativo v1 ");
                    query.Append("                   Where d1.Id = v1.Iddemonstrativo  ");
                    query.Append("                     And d1.Referencia = d.referencia");
                    query.Append("                     And d1.Idempresa = e.Idempresa");
                    query.Append("                     And v1.Data = v.Data) = 0 then v.previsto Else v.Realizadobco End) Valor");
                    query.Append("  From niff_fin_demonstrativo d, Niff_Fin_Coldemonstrativo v, niff_chm_empresas e, Niff_Fin_Colunas c, Niff_Fin_Bancos b");
                    query.Append(" Where d.Id = v.Iddemonstrativo");
                    query.Append("   And c.Id = v.idcoluna");
                    query.Append("   And e.Idempresa = d.Idempresa");
                    query.Append("   And d.referencia = " + referencia);
                    query.Append("   And v.Data = To_date('" + _data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   And b.Id = d.Idbanco");
                    query.Append("   And b.ativo = 'S'");

                    query.Append(" Group By e.Nomeabreviado, e.Idempresa");
                    query.Append("        , Decode(c.tipo, 'EN', 'Receitas', 'SA', 'Pagamentos', 'Transferências')");
                    query.Append("        , v.data, Decode(c.tipo, 'EN', 1, 'SA', 2, 3)");

                    query.Append(" Union All ");

                    query.Append("Select 'ZConsolidado' Nomeabreviado, 99999 Idempresa");
                    query.Append("     , Decode(c.tipo, 'EN', 'Receitas', 'SA', 'Pagamentos', 'Transferências') Descricao");
                    query.Append("     , Decode(c.tipo, 'EN', 1, 'SA', 2, 3) Ordem");
                    query.Append("     , v.data");
                    query.Append("     , Sum(Decode(c.tipo, 'EN', 1, 'SA', -1, 'TE', 1, -1) * ");
                    query.Append("       Case When (Select Max(realizadobco) ");
                    query.Append("                    From Niff_Fin_Demonstrativo d1, Niff_Fin_Coldemonstrativo v1 ");
                    query.Append("                   Where d1.Id = v1.Iddemonstrativo  ");
                    query.Append("                     And d1.Referencia = d.referencia");
                    query.Append("                     And d1.Idempresa = d.Idempresa");
                    query.Append("                     And v1.Data = v.Data) = 0 then v.previsto Else v.Realizadobco End) Valor");
                    query.Append("  From niff_fin_demonstrativo d, Niff_Fin_Coldemonstrativo v, Niff_Fin_Colunas c, Niff_Fin_Bancos b");
                    query.Append(" Where d.Id = v.Iddemonstrativo");
                    query.Append("   And c.Id = v.idcoluna");
                    query.Append("   And d.referencia = " + referencia);
                    query.Append("   And v.Data = To_date('" + _data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   And b.Id = d.Idbanco");
                    query.Append("   And b.ativo = 'S'");
                    query.Append(" Group By Decode(c.tipo, 'EN', 'Receitas', 'SA', 'Pagamentos', 'Transferências')");
                    query.Append("        , v.data, Decode(c.tipo, 'EN', 1, 'SA', 2, 3)");

                    Query executar = sessao.CreateQuery(query.ToString());

                    auxDataReader = executar.ExecuteQuery();
                    using (auxDataReader)
                    {
                        while (auxDataReader.Read())
                        {
                            encontrou = false;
                            _valor = Convert.ToDecimal(auxDataReader["Valor"].ToString());

                            foreach (var item in _lista.Where(w => w.Descricao.Trim() == auxDataReader["Descricao"].ToString() &&
                                                                   w.Empresa == auxDataReader["Nomeabreviado"].ToString()))
                            {
                                encontrou = true;

                                switch (_data.Day)
                                {
                                    case 1:
                                        item.Valor01 = _valor;
                                        break;
                                    case 2:
                                        item.Valor02 = _valor;
                                        break;
                                    case 3:
                                        item.Valor03 = _valor;
                                        break;
                                    case 4:
                                        item.Valor04 = _valor;
                                        break;
                                    case 5:
                                        item.Valor05 = _valor;
                                        break;
                                    case 6:
                                        item.Valor06 = _valor;
                                        break;
                                    case 7:
                                        item.Valor07 = _valor;
                                        break;
                                    case 8:
                                        item.Valor08 = _valor;
                                        break;
                                    case 9:
                                        item.Valor09 = _valor;
                                        break;
                                    case 10:
                                        item.Valor10 = _valor;
                                        break;
                                    case 11:
                                        item.Valor11 = _valor;
                                        break;
                                    case 12:
                                        item.Valor12 = _valor;
                                        break;
                                    case 13:
                                        item.Valor13 = _valor;
                                        break;
                                    case 14:
                                        item.Valor14 = _valor;
                                        break;
                                    case 15:
                                        item.Valor15 = _valor;
                                        break;
                                    case 16:
                                        item.Valor16 = _valor;
                                        break;
                                    case 17:
                                        item.Valor17 = _valor;
                                        break;
                                    case 18:
                                        item.Valor18 = _valor;
                                        break;
                                    case 19:
                                        item.Valor19 = _valor;
                                        break;
                                    case 20:
                                        item.Valor20 = _valor;
                                        break;
                                    case 21:
                                        item.Valor21 = _valor;
                                        break;
                                    case 22:
                                        item.Valor22 = _valor;
                                        break;
                                    case 23:
                                        item.Valor23 = _valor;
                                        break;
                                    case 24:
                                        item.Valor24 = _valor;
                                        break;
                                    case 25:
                                        item.Valor25 = _valor;
                                        break;
                                    case 26:
                                        item.Valor26 = _valor;
                                        break;
                                    case 27:
                                        item.Valor27 = _valor;
                                        break;
                                    case 28:
                                        item.Valor28 = _valor;
                                        break;
                                    case 29:
                                        item.Valor29 = _valor;
                                        break;
                                    case 30:
                                        item.Valor30 = _valor;
                                        break;
                                    case 31:
                                        item.Valor31 = _valor;
                                        break;
                                }
                            }

                            if (!encontrou)
                            {
                                Financeiro.Resumo item = new Financeiro.Resumo();

                                item.Descricao = "     " + auxDataReader["descricao"].ToString();
                                item.Empresa = auxDataReader["Nomeabreviado"].ToString();
                                item.IdEmpresa = Convert.ToInt32(auxDataReader["Idempresa"].ToString());
                                item.Ordem = Convert.ToInt32(auxDataReader["Ordem"].ToString());

                                //if (item.Ordem == 1 && item.IdEmpresa != 99999)
                                  //  item.SaldoInicial = SaldoInicial(item.IdEmpresa, referencia);

                                switch (_data.Day)
                                {
                                    case 1:
                                        item.Valor01 = _valor;
                                        break;
                                    case 2:
                                        item.Valor02 = _valor;
                                        break;
                                    case 3:
                                        item.Valor03 = _valor;
                                        break;
                                    case 4:
                                        item.Valor04 = _valor;
                                        break;
                                    case 5:
                                        item.Valor05 = _valor;
                                        break;
                                    case 6:
                                        item.Valor06 = _valor;
                                        break;
                                    case 7:
                                        item.Valor07 = _valor;
                                        break;
                                    case 8:
                                        item.Valor08 = _valor;
                                        break;
                                    case 9:
                                        item.Valor09 = _valor;
                                        break;
                                    case 10:
                                        item.Valor10 = _valor;
                                        break;
                                    case 11:
                                        item.Valor11 = _valor;
                                        break;
                                    case 12:
                                        item.Valor12 = _valor;
                                        break;
                                    case 13:
                                        item.Valor13 = _valor;
                                        break;
                                    case 14:
                                        item.Valor14 = _valor;
                                        break;
                                    case 15:
                                        item.Valor15 = _valor;
                                        break;
                                    case 16:
                                        item.Valor16 = _valor;
                                        break;
                                    case 17:
                                        item.Valor17 = _valor;
                                        break;
                                    case 18:
                                        item.Valor18 = _valor;
                                        break;
                                    case 19:
                                        item.Valor19 = _valor;
                                        break;
                                    case 20:
                                        item.Valor20 = _valor;
                                        break;
                                    case 21:
                                        item.Valor21 = _valor;
                                        break;
                                    case 22:
                                        item.Valor22 = _valor;
                                        break;
                                    case 23:
                                        item.Valor23 = _valor;
                                        break;
                                    case 24:
                                        item.Valor24 = _valor;
                                        break;
                                    case 25:
                                        item.Valor25 = _valor;
                                        break;
                                    case 26:
                                        item.Valor26 = _valor;
                                        break;
                                    case 27:
                                        item.Valor27 = _valor;
                                        break;
                                    case 28:
                                        item.Valor28 = _valor;
                                        break;
                                    case 29:
                                        item.Valor29 = _valor;
                                        break;
                                    case 30:
                                        item.Valor30 = _valor;
                                        break;
                                    case 31:
                                        item.Valor31 = _valor;
                                        break;
                                }
                                _lista.Add(item);
                            }
                        }
                    }
                    _data = _data.AddDays(1);
                }

                if (_lista.Count() != 0)
                {
                    // Busca o Total da movimentado por tipo para encontrar o acumulado do mês.
                    query.Clear();
                    query.Append("Select e.Nomeabreviado, e.Idempresa");
                    query.Append("     , Decode(c.tipo, 'EN', 'Receitas', 'SA', 'Pagamentos', 'Transferências') Descricao");
                    query.Append("     , Decode(c.tipo, 'EN', 1, 'SA', 2, 3) Ordem");
                    query.Append("     , Sum(Decode(c.tipo, 'EN', 1, 'SA', -1, 'TE', 1, -1) * ");
                    query.Append("       Case When (Select Max(realizadobco) ");
                    query.Append("                    From Niff_Fin_Demonstrativo d1, Niff_Fin_Coldemonstrativo v1 ");
                    query.Append("                   Where d1.Id = v1.Iddemonstrativo  ");
                    query.Append("                     And d1.Referencia = d.referencia");
                    query.Append("                     And d1.Idempresa = e.Idempresa");
                    query.Append("                     And v1.Data = v.Data) = 0 then v.previsto Else v.Realizadobco End) Valor");
                    query.Append("  From niff_fin_demonstrativo d, Niff_Fin_Coldemonstrativo v, niff_chm_empresas e, Niff_Fin_Colunas c, Niff_Fin_Bancos b");
                    query.Append(" Where d.Id = v.Iddemonstrativo");
                    query.Append("   And c.Id = v.idcoluna");
                    query.Append("   And e.Idempresa = d.Idempresa");
                    query.Append("   And d.referencia = " + referencia);
                    query.Append("   And b.Id = d.Idbanco");
                    query.Append("   And b.ativo = 'S'");

                    query.Append(" Group By e.Nomeabreviado, e.Idempresa");
                    query.Append("        , Decode(c.tipo, 'EN', 'Receitas', 'SA', 'Pagamentos', 'Transferências')");
                    query.Append("        , Decode(c.tipo, 'EN', 1, 'SA', 2, 3)");

                    query.Append(" Union All ");

                    query.Append("Select 'ZConsolidado' Nomeabreviado, 99999 Idempresa");
                    query.Append("     , Decode(c.tipo, 'EN', 'Receitas', 'SA', 'Pagamentos', 'Transferências') Descricao");
                    query.Append("     , Decode(c.tipo, 'EN', 1, 'SA', 2, 3) Ordem");
                    query.Append("     , Sum(Decode(c.tipo, 'EN', 1, 'SA', -1, 'TE', 1, -1) * ");
                    query.Append("       Case When (Select Max(realizadobco) ");
                    query.Append("                    From Niff_Fin_Demonstrativo d1, Niff_Fin_Coldemonstrativo v1 ");
                    query.Append("                   Where d1.Id = v1.Iddemonstrativo  ");
                    query.Append("                     And d1.Referencia = d.referencia");
                    query.Append("                     And d1.Idempresa = d.Idempresa");
                    query.Append("                     And v1.Data = v.Data) = 0 then v.previsto Else v.Realizadobco End) Valor");
                    query.Append("  From niff_fin_demonstrativo d, Niff_Fin_Coldemonstrativo v, Niff_Fin_Colunas c, Niff_Fin_Bancos b");
                    query.Append(" Where d.Id = v.Iddemonstrativo");
                    query.Append("   And c.Id = v.idcoluna");
                    query.Append("   And d.referencia = " + referencia);
                    query.Append("   And b.Id = d.Idbanco");
                    query.Append("   And b.ativo = 'S'");
                    query.Append(" Group By Decode(c.tipo, 'EN', 'Receitas', 'SA', 'Pagamentos', 'Transferências')");
                    query.Append("        , Decode(c.tipo, 'EN', 1, 'SA', 2, 3)");

                    Query executar = sessao.CreateQuery(query.ToString());

                    auxDataReader = executar.ExecuteQuery();
                    using (auxDataReader)
                    {
                        while (auxDataReader.Read())
                        {
                            encontrou = false;
                            _valor = Convert.ToDecimal(auxDataReader["Valor"].ToString());

                            foreach (var item in _lista.Where(w => w.Descricao.Trim() == auxDataReader["Descricao"].ToString() &&
                                                                   w.Empresa == auxDataReader["Nomeabreviado"].ToString()))
                            {
                                encontrou = true;
                                item.AcumuladoMes = _valor;
                            }
                        }
                    }
                }

                _data = _inicio;
                // Busca o totalizador diario da empresa
                while (_data <= _fim)
                {
                    query.Clear();
                    query.Append("Select e.Nomeabreviado, e.Idempresa");
                    query.Append("     , e.Nomeabreviado Descricao");
                    query.Append("     , 0 Ordem");
                    query.Append("     , Sum(Decode(c.tipo, 'EN', 1, 'SA', -1, 'TE', 1, -1) * ");
                    query.Append("       Case When (Select Max(realizadobco) ");
                    query.Append("                    From Niff_Fin_Demonstrativo d1, Niff_Fin_Coldemonstrativo v1 ");
                    query.Append("                   Where d1.Id = v1.Iddemonstrativo  ");
                    query.Append("                     And d1.Referencia = d.referencia");
                    query.Append("                     And d1.Idempresa = e.Idempresa");
                    query.Append("                     And v1.Data = v.Data) = 0 then v.previsto Else v.Realizadobco End) Valor");
                    query.Append("  From niff_fin_demonstrativo d, Niff_Fin_Coldemonstrativo v, niff_chm_empresas e, Niff_Fin_Colunas c, Niff_Fin_Bancos b");
                    query.Append(" Where d.Id = v.Iddemonstrativo");
                    query.Append("   And c.Id = v.idcoluna");
                    query.Append("   And e.Idempresa = d.Idempresa");
                    query.Append("   And d.referencia = " + referencia);
                    query.Append("   And v.Data = To_date('" + _data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   And b.Id = d.Idbanco");
                    query.Append("   And b.ativo = 'S'");
                    query.Append(" Group By e.Nomeabreviado, e.Idempresa");

                    query.Append(" Union All ");

                    query.Append("Select 'ZConsolidado' Nomeabreviado, 99999 Idempresa");
                    query.Append("     , 'Consolidado' Descricao");
                    query.Append("     , 0 Ordem");
                    query.Append("     , Sum(Decode(c.tipo, 'EN', 1, 'SA', -1, 'TE', 1, -1) * ");
                    query.Append("       Case When (Select Max(realizadobco) ");
                    query.Append("                    From Niff_Fin_Demonstrativo d1, Niff_Fin_Coldemonstrativo v1 ");
                    query.Append("                   Where d1.Id = v1.Iddemonstrativo  ");
                    query.Append("                     And d1.Referencia = d.referencia");
                    query.Append("                     And d1.Idempresa = d.Idempresa");
                    query.Append("                     And v1.Data = v.Data) = 0 then v.previsto Else v.Realizadobco End) Valor");
                    query.Append("  From niff_fin_demonstrativo d, Niff_Fin_Coldemonstrativo v, Niff_Fin_Colunas c, Niff_Fin_Bancos b");
                    query.Append(" Where d.Id = v.Iddemonstrativo");
                    query.Append("   And c.Id = v.idcoluna");
                    query.Append("   And d.referencia = " + referencia);
                    query.Append("   And v.Data = To_date('" + _data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   And b.Id = d.Idbanco");
                    query.Append("   And b.ativo = 'S'");

                    Query executar = sessao.CreateQuery(query.ToString());

                    auxDataReader = executar.ExecuteQuery();
                    using (auxDataReader)
                    {

                        while (auxDataReader.Read())
                        {
                            encontrou = false;
                            _valor = Convert.ToDecimal(auxDataReader["Valor"].ToString());

                            foreach (var item in _lista.Where(w => w.Descricao.Trim() == auxDataReader["Descricao"].ToString() &&
                                                                   w.Empresa == auxDataReader["Nomeabreviado"].ToString()))
                            {
                                encontrou = true;

                                switch (_data.Day)
                                {
                                    case 1:
                                        item.Valor01 = _valor;
                                        break;
                                    case 2:
                                        item.Valor02 = _valor;
                                        break;
                                    case 3:
                                        item.Valor03 = _valor;
                                        break;
                                    case 4:
                                        item.Valor04 = _valor;
                                        break;
                                    case 5:
                                        item.Valor05 = _valor;
                                        break;
                                    case 6:
                                        item.Valor06 = _valor;
                                        break;
                                    case 7:
                                        item.Valor07 = _valor;
                                        break;
                                    case 8:
                                        item.Valor08 = _valor;
                                        break;
                                    case 9:
                                        item.Valor09 = _valor;
                                        break;
                                    case 10:
                                        item.Valor10 = _valor;
                                        break;
                                    case 11:
                                        item.Valor11 = _valor;
                                        break;
                                    case 12:
                                        item.Valor12 = _valor;
                                        break;
                                    case 13:
                                        item.Valor13 = _valor;
                                        break;
                                    case 14:
                                        item.Valor14 = _valor;
                                        break;
                                    case 15:
                                        item.Valor15 = _valor;
                                        break;
                                    case 16:
                                        item.Valor16 = _valor;
                                        break;
                                    case 17:
                                        item.Valor17 = _valor;
                                        break;
                                    case 18:
                                        item.Valor18 = _valor;
                                        break;
                                    case 19:
                                        item.Valor19 = _valor;
                                        break;
                                    case 20:
                                        item.Valor20 = _valor;
                                        break;
                                    case 21:
                                        item.Valor21 = _valor;
                                        break;
                                    case 22:
                                        item.Valor22 = _valor;
                                        break;
                                    case 23:
                                        item.Valor23 = _valor;
                                        break;
                                    case 24:
                                        item.Valor24 = _valor;
                                        break;
                                    case 25:
                                        item.Valor25 = _valor;
                                        break;
                                    case 26:
                                        item.Valor26 = _valor;
                                        break;
                                    case 27:
                                        item.Valor27 = _valor;
                                        break;
                                    case 28:
                                        item.Valor28 = _valor;
                                        break;
                                    case 29:
                                        item.Valor29 = _valor;
                                        break;
                                    case 30:
                                        item.Valor30 = _valor;
                                        break;
                                    case 31:
                                        item.Valor31 = _valor;
                                        break;
                                }
                            }

                            if (!encontrou)
                            {
                                Financeiro.Resumo item = new Financeiro.Resumo();

                                item.Descricao = auxDataReader["descricao"].ToString();
                                item.Empresa = auxDataReader["Nomeabreviado"].ToString();
                                item.IdEmpresa = Convert.ToInt32(auxDataReader["Idempresa"].ToString());
                                item.Ordem = Convert.ToInt32(auxDataReader["Ordem"].ToString());

                                if (item.Ordem == 0 && item.IdEmpresa != 99999)
                                    item.SaldoInicial = SaldoInicial(item.IdEmpresa, referencia);

                                switch (_data.Day)
                                {
                                    case 1:
                                        item.Valor01 = _valor;
                                        break;
                                    case 2:
                                        item.Valor02 = _valor;
                                        break;
                                    case 3:
                                        item.Valor03 = _valor;
                                        break;
                                    case 4:
                                        item.Valor04 = _valor;
                                        break;
                                    case 5:
                                        item.Valor05 = _valor;
                                        break;
                                    case 6:
                                        item.Valor06 = _valor;
                                        break;
                                    case 7:
                                        item.Valor07 = _valor;
                                        break;
                                    case 8:
                                        item.Valor08 = _valor;
                                        break;
                                    case 9:
                                        item.Valor09 = _valor;
                                        break;
                                    case 10:
                                        item.Valor10 = _valor;
                                        break;
                                    case 11:
                                        item.Valor11 = _valor;
                                        break;
                                    case 12:
                                        item.Valor12 = _valor;
                                        break;
                                    case 13:
                                        item.Valor13 = _valor;
                                        break;
                                    case 14:
                                        item.Valor14 = _valor;
                                        break;
                                    case 15:
                                        item.Valor15 = _valor;
                                        break;
                                    case 16:
                                        item.Valor16 = _valor;
                                        break;
                                    case 17:
                                        item.Valor17 = _valor;
                                        break;
                                    case 18:
                                        item.Valor18 = _valor;
                                        break;
                                    case 19:
                                        item.Valor19 = _valor;
                                        break;
                                    case 20:
                                        item.Valor20 = _valor;
                                        break;
                                    case 21:
                                        item.Valor21 = _valor;
                                        break;
                                    case 22:
                                        item.Valor22 = _valor;
                                        break;
                                    case 23:
                                        item.Valor23 = _valor;
                                        break;
                                    case 24:
                                        item.Valor24 = _valor;
                                        break;
                                    case 25:
                                        item.Valor25 = _valor;
                                        break;
                                    case 26:
                                        item.Valor26 = _valor;
                                        break;
                                    case 27:
                                        item.Valor27 = _valor;
                                        break;
                                    case 28:
                                        item.Valor28 = _valor;
                                        break;
                                    case 29:
                                        item.Valor29 = _valor;
                                        break;
                                    case 30:
                                        item.Valor30 = _valor;
                                        break;
                                    case 31:
                                        item.Valor31 = _valor;
                                        break;
                                }
                                _lista.Add(item);
                            }
                        }
                    }
                    _data = _data.AddDays(1);
                }

                if (_lista.Count() != 0)
                {
                    query.Clear();
                    query.Append("Select e.Nomeabreviado, e.Idempresa");
                    query.Append("     , e.Nomeabreviado Descricao");
                    query.Append("     , 0 Ordem");
                    query.Append("     , Sum(Decode(c.tipo, 'EN', 1, 'SA', -1, 'TE', 1, -1) * ");
                    query.Append("       Case When (Select Max(realizadobco) ");
                    query.Append("                    From Niff_Fin_Demonstrativo d1, Niff_Fin_Coldemonstrativo v1 ");
                    query.Append("                   Where d1.Id = v1.Iddemonstrativo  ");
                    query.Append("                     And d1.Referencia = d.referencia");
                    query.Append("                     And d1.Idempresa = e.Idempresa");
                    query.Append("                     And v1.Data = v.Data) = 0 then v.previsto Else v.Realizadobco End) Valor");
                    query.Append("  From niff_fin_demonstrativo d, Niff_Fin_Coldemonstrativo v, niff_chm_empresas e, Niff_Fin_Colunas c, Niff_Fin_Bancos b");
                    query.Append(" Where d.Id = v.Iddemonstrativo");
                    query.Append("   And c.Id = v.idcoluna");
                    query.Append("   And e.Idempresa = d.Idempresa");
                    query.Append("   And d.referencia = " + referencia);
                    query.Append("   And b.Id = d.Idbanco");
                    query.Append("   And b.ativo = 'S'");
                    query.Append(" Group By e.Nomeabreviado, e.Idempresa");

                    query.Append(" Union All ");

                    query.Append("Select 'ZConsolidado' Nomeabreviado, 99999 Idempresa");
                    query.Append("     , 'Consolidado' Descricao");
                    query.Append("     , 0 Ordem");
                    query.Append("     , Sum(Decode(c.tipo, 'EN', 1, 'SA', -1, 'TE', 1, -1) * ");
                    query.Append("       Case When (Select Max(realizadobco) ");
                    query.Append("                    From Niff_Fin_Demonstrativo d1, Niff_Fin_Coldemonstrativo v1 ");
                    query.Append("                   Where d1.Id = v1.Iddemonstrativo  ");
                    query.Append("                     And d1.Referencia = d.referencia");
                    query.Append("                     And d1.Idempresa = d.Idempresa");
                    query.Append("                     And v1.Data = v.Data) = 0 then v.previsto Else v.Realizadobco End) Valor");
                    query.Append("  From niff_fin_demonstrativo d, Niff_Fin_Coldemonstrativo v, Niff_Fin_Colunas c, Niff_Fin_Bancos b");
                    query.Append(" Where d.Id = v.Iddemonstrativo");
                    query.Append("   And c.Id = v.idcoluna");
                    query.Append("   And d.referencia = " + referencia);
                    query.Append("   And b.Id = d.Idbanco");
                    query.Append("   And b.ativo = 'S'");

                    Query executar = sessao.CreateQuery(query.ToString());

                    auxDataReader = executar.ExecuteQuery();
                    using (auxDataReader)
                    {

                        while (auxDataReader.Read())
                        {
                            encontrou = false;
                            _valor = Convert.ToDecimal(auxDataReader["Valor"].ToString());

                            foreach (var item in _lista.Where(w => w.Descricao.Trim() == auxDataReader["Descricao"].ToString() &&
                                                                   w.Empresa == auxDataReader["Nomeabreviado"].ToString()))
                            {
                                encontrou = true;
                                item.AcumuladoMes = _valor;
                                item.SaldoFinal = item.SaldoInicial + _valor;
                            }
                        }
                    }
                }

                _valor = _lista.Where(w => w.IdEmpresa != 0)
                               .Sum(s => s.SaldoInicial);

                foreach (var item in _lista.Where(w => w.IdEmpresa == 99999 && w.Ordem == 0))
                {
                    item.SaldoInicial = _valor;
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
        #endregion
    }
}
