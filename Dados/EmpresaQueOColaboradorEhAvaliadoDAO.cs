using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class EmpresaQueOColaboradorEhAvaliadoDAO
    {
        IDataReader dataReader;

        public List<EmpresaQueOColaboradorEhAvaliado> Listar(int Colaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<EmpresaQueOColaboradorEhAvaliado> _lista = new List<EmpresaQueOColaboradorEhAvaliado>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select f.IdEmpAvalia, f.idEmpresa, f.inicio, f.fim, e.Nomeabreviado");
                query.Append("  From Niff_ads_EmpresasColAvalia f, niff_chm_empresas e");
                query.Append(" Where IdColaborador = " + Colaborador);
                query.Append("   And e.Idempresa = f.idempresa");
                query.Append("   And e.AvaliaColaboradores = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        EmpresaQueOColaboradorEhAvaliado _tipo = new EmpresaQueOColaboradorEhAvaliado();

                        _tipo.Existe = true;
                        _tipo.Empresa = dataReader["Nomeabreviado"].ToString();

                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["IdEmpAvalia"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdColoborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Inicio = Convert.ToDateTime(dataReader["Inicio"].ToString());
                            if (_tipo.Inicio != DateTime.MinValue)
                                _tipo.DataInicio = _tipo.Inicio.ToShortDateString();
                        }
                        catch { }
                        try
                        {
                            _tipo.Fim = Convert.ToDateTime(dataReader["Fim"].ToString());

                            if (_tipo.Fim != DateTime.MinValue)
                                _tipo.DataFinal = _tipo.Fim.ToShortDateString();
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

        public EmpresaQueOColaboradorEhAvaliado Consultar(int Colaborador, int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            EmpresaQueOColaboradorEhAvaliado _tipo = new EmpresaQueOColaboradorEhAvaliado();

            try
            {
                query.Append("Select f.IdEmpAvalia, f.idEmpresa, f.inicio, f.fim, e.Nomeabreviado");
                query.Append("  From Niff_ads_EmpresasColAvalia f, niff_chm_empresas e");
                query.Append(" Where IdColaborador = " + Colaborador);
                query.Append("   And e.Idempresa = " + empresa);
                query.Append("   And e.Idempresa = f.idempresa");
                query.Append("   And e.AvaliaColaboradores = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Empresa = dataReader["Nomeabreviado"].ToString();

                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["IdEmpAvalia"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdColoborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Inicio = Convert.ToDateTime(dataReader["Inicio"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.Fim = Convert.ToDateTime(dataReader["Fim"].ToString());
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


        public bool Gravar(List<EmpresaQueOColaboradorEhAvaliado> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            try
            {
                foreach (var _pontuacao in _lista)
                {
                    query.Clear();
                    try
                    {
                        if (_pontuacao.DataInicio != "")
                            _pontuacao.Inicio = Convert.ToDateTime(_pontuacao.DataInicio);
                        else
                            _pontuacao.Inicio = DateTime.MinValue;
                    }
                    catch { }

                    try
                    {
                        if (_pontuacao.DataFinal != "")
                            _pontuacao.Fim = Convert.ToDateTime(_pontuacao.DataFinal);
                        else
                            _pontuacao.Fim = DateTime.MinValue;
                    }
                    catch
                    { }

                    if (!_pontuacao.Existe)
                    {
                        query.Append("Insert into Niff_ads_EmpresasColAvalia");
                        query.Append(" ( IdEmpAvalia, idEmpresa, IdColaborador, inicio, fim )");
                        query.Append(" Values ( SQ_NIFF_IdEmpAvalia.NextVal " );
                        query.Append("        , " + _pontuacao.IdEmpresa);
                        query.Append("        , " + _pontuacao.IdColoborador);


                        query.Append("        , To_date('" + _pontuacao.Inicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("        , To_date('" + _pontuacao.Fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append(" )");
                    }
                    else
                    {
                        query.Append("Update Niff_ads_EmpresasColAvalia");
                        query.Append("   set Inicio = To_date('" + _pontuacao.Inicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("     , Fim = To_date('" + _pontuacao.Fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append(" where IdEmpAvalia = " + _pontuacao.Id);                       
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

        public bool Excluir(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_ads_EmpresasColAvalia");
                query.Append(" Where IdEmpAvalia = " + codigo);
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
