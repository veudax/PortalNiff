using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class CigamDAO
    {
        IDataReader dataReader;
        IDataReader dataReaderAux;

        public List<Cigam> ListarGlobus(int idEmpresa, string Empresa, DateTime Inicio, DateTime Fim)
        {
            // lê do globus
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Cigam> lista = new List<Cigam>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select l.codigoempresa ,l.codigofl ,l.codlanca ,i.coditemlanca , l.dtlanca dtlanca,l.lotelanca ,l.sistema ");
                query.Append("     , l.documentolanca, 'I' Situacao, l.lctomodificado, l.usuario, sysdate, i.codcontactb");
                query.Append("     , decode(i.contrapartitemlanca, null, null, i.contrapartitemlanca) contrapartitemlanca");
                query.Append("     , i.vritemlanca, decode(i.codcusto, null, null, i.codcusto) codcusto, i.debitocreditoitemlanca");
                query.Append("     , '0' O, tiraecomercial(i.historicoitemlanca) descricao");
                query.Append("  from ctblanca l, ctbitlnc i, niff_chm_empresas e ");
                query.Append(" Where l.codlanca = i.codlanca");
                query.Append("   And l.dtlanca between to_date('" + Inicio.ToShortDateString() + "','dd/mm/yyyy') and to_date('" + Fim.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And lpad(l.codigoempresa,3,'0') || '/' || lpad(l.codigoFl, 3, '0') = e.codigoglobus");
                query.Append("   And e.IdEmpresa = " + idEmpresa);
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Cigam _tipo = new Cigam();

                        _tipo.CodigoEmpresa = Convert.ToInt32(dataReader["CodigoEmpresa"].ToString());
                        _tipo.CodigoFl = Convert.ToInt32(dataReader["CodigoFl"].ToString());
                        _tipo.CodLanca = Convert.ToDecimal(dataReader["CodLanca"].ToString());
                        _tipo.CoditemLanca = Convert.ToDecimal(dataReader["coditemlanca"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["dtlanca"].ToString());

                        _tipo.Lote = dataReader["lotelanca"].ToString();
                        _tipo.Origem = dataReader["sistema"].ToString();
                        _tipo.Documento = dataReader["documentolanca"].ToString();
                        _tipo.Modificado = dataReader["lctomodificado"].ToString();
                        _tipo.Situacao = dataReader["Situacao"].ToString();
                        
                        _tipo.Usuario = dataReader["usuario"].ToString();
                        _tipo.TipoLancamento = dataReader["debitocreditoitemlanca"].ToString();
                        _tipo.Historico = dataReader["descricao"].ToString();

                        _tipo.CodContaContabil = Convert.ToInt32(dataReader["codcontactb"].ToString());
                        try
                        {
                            _tipo.CodContraPartida = Convert.ToInt32(dataReader["contrapartitemlanca"].ToString());
                        }
                        catch { }

                        _tipo.Valor = Convert.ToDecimal(dataReader["vritemlanca"].ToString());
                        try
                        {
                            _tipo.CodCusto = Convert.ToInt32(dataReader["codcusto"].ToString());
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
        
        public List<Cigam> ListarCigam(string idEmpresa, DateTime Inicio, DateTime Fim)
        {
            // lê do cigam
            StringBuilder query = new StringBuilder();
            SessaoCigam sessaoCigam = new SessaoCigam();
            List<Cigam> lista = new List<Cigam>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CoditemLanca, codlanca, Situacao, idrepos");
                query.Append("  from niff_reposcontabil r");
                query.Append(" Where data between to_date('" + Inicio.ToShortDateString() + "',dd/mm/yyyy') and to_date('" + Fim.ToShortDateString() + "',dd/mm/yyyy')");
                query.Append("   And lpad(codigoempresa,3,'0') || '/' || lpad(codigoFl, 3, '0') = '" + idEmpresa + "'");

                QueryCigam executar = sessaoCigam.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Cigam _tipo = new Cigam();

                        _tipo.Id = Convert.ToInt32(dataReader["idrepos"].ToString());
                        _tipo.CodLanca = Convert.ToDecimal(dataReader["CodLanca"].ToString());
                        _tipo.CoditemLanca = Convert.ToDecimal(dataReader["coditemlanca"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["dtlanca"].ToString());

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
                sessaoCigam.Desconectar();
            }
            return lista;
        }
        
        public bool Gravar(List<Cigam> _lista, string Empresa, DateTime Inicio, DateTime Fim)
        {
            StringBuilder query = new StringBuilder();
            SessaoCigam sessaoCigam = new SessaoCigam();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = _lista.Count() == 0;
            int id = 1;

            try
            {
                // grava na Cigam ou produção ou homologação
                foreach (var item in _lista)
                {

                    query.Clear();
                    query.Append("Select Nvl(max(IDREPOS),0)+1 next from NIFF_REPOSCONTABIL_AUX");
                    QueryCigam executar = sessaoCigam.CreateQuery(query.ToString());

                    dataReaderAux = executar.ExecuteQuery();

                    using (dataReaderAux)
                    {
                        if (dataReaderAux.Read())
                        {
                            id = Convert.ToInt32(dataReaderAux["next"].ToString());
                        }
                    }

                    query.Clear();

                    query.Append("Insert into NIFF_REPOSCONTABIL_AUX");
                    query.Append(" ( idrepos, codigoempresa, codigofl, codlanca, coditemlanca, data, lote, origem, documento");
                    query.Append(" , situacao, modificado, usuariocriacao, datacriacao, contacontabil, contrapartida, valor");
                    query.Append(" , codigocusto, debcred, status, historico, numero ");
                    query.Append(") Values ( " + id);
                    query.Append("        , " + item.CodigoEmpresa);
                    query.Append("        , " + item.CodigoFl);

                    query.Append("        , " + item.CodLanca);
                    query.Append("        , " + item.CoditemLanca);
                    query.Append("        , To_Date('" + item.Data.ToShortDateString() + "','dd/mm/yyyy')");
                        
                    query.Append("     , '" + item.Lote + "'");
                    query.Append("     , '" + item.Origem + "'");
                    query.Append("     , '" + item.Documento + "'");
                    query.Append("     , '" + item.Situacao + "'");
                    query.Append("     , '" + item.Modificado + "'");
                    query.Append("     , '" + item.Usuario + "'");
                    query.Append("     , sysDate");
                    query.Append("        , " + item.CodContaContabil);

                    if (item.CodContraPartida == null)
                        query.Append("        , null");
                    else
                        query.Append("        , " + item.CodContraPartida);

                    query.Append("        , " + item.Valor.ToString().Replace(".", "").Replace(",", "."));

                    if (item.CodCusto == null)
                        query.Append("        , null");
                    else
                        query.Append("        , " + item.CodCusto);

                    query.Append("     , '" + item.TipoLancamento + "'");
                    query.Append("     , '0'");
                    query.Append("     , '" + item.Historico + "'");
                    query.Append("     , " + id);
                    query.Append(" )");
                
                    retorno = sessaoCigam.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                        break;
                }

                if (retorno)
                {
                    retorno = VerificaSeFoiExcluido(Empresa, Inicio, Fim);
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
                sessaoCigam.Desconectar();
            }
        }

        public bool VerificaSeFoiExcluido(string idEmpresa, DateTime Inicio, DateTime Fim)
        {
            StringBuilder query = new StringBuilder();
            SessaoCigam sessaoCigam = new SessaoCigam();
            Publicas.mensagemDeErro = string.Empty;
            List<Cigam> _lista = new List<Cigam>();

            bool retorno = _lista.Count() == 0;
            bool encontrou = false; 
            try
            {
                _lista = ListarCigam(idEmpresa, Inicio, Fim);

                foreach (var item in _lista)
                {
                    query.Clear();

                    query.Append("Select CoditemLanca, codlanca, Situacao, idrepos");
                    query.Append("  from niff_reposcontabil_Aux r");
                    query.Append(" Where CoditemLanca = " + item.CoditemLanca);
                    query.Append("   And Codlanca = " + item.CodLanca);

                    QueryCigam executar = sessaoCigam.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();
                    encontrou = false;

                    using (dataReader)
                    {
                        if (dataReader.Read())
                        {
                            encontrou = true;
                        }
                    }

                    if (!encontrou)
                    {
                        query.Clear();

                        query.Append("Update NIFF_REPOSCONTABIL");
                        query.Append("   set Situacao = 'E'");
                        query.Append(" where idrepos = " + item.Id);
                        
                        retorno = sessaoCigam.ExecuteSqlTransaction(query.ToString());

                        if (!retorno)
                            break;
                    }
                }

                query.Clear();

                query.Append("Delete niff_reposcontabil_Aux");
                query.Append("  where data between to_date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') and to_date('" + Fim.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And lpad(codigoempresa,3,'0') || '/' || lpad(codigoFl, 3, '0') = '" + idEmpresa + "'");

                retorno = sessaoCigam.ExecuteSqlTransaction(query.ToString());

                return retorno;
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessaoCigam.Desconectar();
            }
        }

        public bool LimparBase(string idEmpresa, DateTime Inicio, DateTime Fim)
        {
            StringBuilder query = new StringBuilder();
            SessaoCigam sessaoCigam = new SessaoCigam();
            Publicas.mensagemDeErro = string.Empty;
            List<Cigam> _lista = new List<Cigam>();

            bool retorno = false;
            
            try
            {

                query.Clear();

                query.Append("Delete niff_reposcontabil_Aux");
                query.Append("  where data between to_date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') and to_date('" + Fim.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And lpad(codigoempresa,3,'0') || '/' || lpad(codigoFl, 3, '0') = '" + idEmpresa + "'");

                retorno = sessaoCigam.ExecuteSqlTransaction(query.ToString());
                if (retorno)
                {
                    query.Clear();

                    query.Append("Delete niff_reposcontabil");
                    query.Append("  where data between to_date('" + Inicio.ToShortDateString() + "', 'dd/mm/yyyy') and to_date('" + Fim.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   And lpad(codigoempresa,3,'0') || '/' || lpad(codigoFl, 3, '0') = '" + idEmpresa + "'");

                    retorno = sessaoCigam.ExecuteSqlTransaction(query.ToString());
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
                sessaoCigam.Desconectar();
            }
        }

    }
}
