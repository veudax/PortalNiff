using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class FeriadoDAO
    {
        IDataReader dataReader;
        IDataReader dataReader1;

        // Verifica se o feriado está cadastrado no Globus
        public Feriado Consulta(DateTime data)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            Feriado _feriado = new Feriado();
            try
            {
                query.Append("Select f.dataferiado, f.descferiado");
                query.Append("  From finferia f");
                query.Append(" Where ");

                if (data != DateTime.MinValue)
                    query.Append("   f.dataferiado = To_Date('" + data.Date.ToShortDateString() + "', 'dd/mm/yyyy')");
                else
                    query.Append("   f.dataferiado = trunc(Sysdate) + 1 ");

                Query executar = sessao.CreateQuery(query.ToString());
                dataReader1 = executar.ExecuteQuery();

                using (dataReader1)
                {
                    if (dataReader1.Read())
                    {
                        _feriado.Existe = true;
                        try
                        {
                            _feriado.Data = Convert.ToDateTime(dataReader1["dataferiado"].ToString());
                        }
                        catch { }

                        _feriado.Descricao = dataReader1["descferiado"].ToString();
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
            return _feriado;
        }

        public FeriadoEmenda Consulta(DateTime data, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            FeriadoEmenda _feriado = new FeriadoEmenda();
            try
            {
                query.Append("Select id, idempresa, data, tipo, Descricao");
                query.Append("  From Niff_Ads_FeriadosEmendas f");
                query.Append(" Where idEmpresa = " + idEmpresa);
                query.Append("   And f.data = To_Date('" + data.Date.ToShortDateString() + "', 'dd/mm/yyyy')");

                Query executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _feriado.Existe = true;

                        _feriado.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _feriado.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _feriado.Tipo = dataReader["tipo"].ToString();
                        _feriado.Nome = dataReader["Descricao"].ToString().ToUpper();

                        try
                        {
                            _feriado.Data = Convert.ToDateTime(dataReader["data"].ToString());
                        }
                        catch { }

                        _feriado.Ano = _feriado.Data.Year;
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
            return _feriado;
        }

        public List<FeriadoEmenda> Listar(int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<FeriadoEmenda> _lista = new List<FeriadoEmenda>();

            try
            {
                query.Append("Select id, idempresa, data, tipo, Descricao");
                query.Append("  From Niff_Ads_FeriadosEmendas f");
                query.Append(" Where idEmpresa = " + idEmpresa);

                Query executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        FeriadoEmenda _feriado = new FeriadoEmenda();
                        _feriado.Existe = true;

                        _feriado.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _feriado.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _feriado.Tipo = dataReader["tipo"].ToString();

                        _feriado.TipoDescricao = (_feriado.Tipo == "E" ? "Emenda" : "Feriado");

                        try
                        {
                            _feriado.Data = Convert.ToDateTime(dataReader["data"].ToString());
                        }
                        catch { }

                        _feriado.Nome = dataReader["Descricao"].ToString().ToUpper();
                        _feriado.Ano = _feriado.Data.Year;

                        _lista.Add(_feriado);
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

        public bool Gravar(List<FeriadoEmenda> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                foreach (var tipo in _lista)
                {

                    query.Clear();
                    if (!tipo.Existe)
                    {
                        query.Append("Insert into Niff_Ads_FeriadosEmendas");
                        query.Append(" ( id, idempresa, data, tipo, descricao )");
                        query.Append(" Values ((Select Nvl(Max(id),0) +1 next From Niff_Ads_FeriadosEmendas)");
                        query.Append("        , " + tipo.IdEmpresa);
                        query.Append("        ,To_date('" + tipo.Data.ToShortDateString() + "','dd/mm/yyyy')");
                        query.Append("        ,'" + tipo.Tipo + "'");
                        query.Append("        ,'" + tipo.Nome + "'");

                        query.Append(" )");
                    }
                    else
                    {
                        query.Append("Update Niff_Ads_FeriadosEmendas");
                        query.Append("   set Tipo = '" + tipo.Tipo + "'");
                        query.Append("     , Descricao = '" + tipo.Nome + "'");
                        query.Append(" Where id = " + tipo.Id);
                    }

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

        public bool Excluir(int id, bool todos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Delete Niff_Ads_FeriadosEmendas");
                if (todos)
                    query.Append(" Where idempresa = " + id);
                else
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

        //public Feriado Consulta(int idEmpresa, DateTime data)
        //{
        //    StringBuilder query = new StringBuilder();
        //    Sessao sessao = new Sessao();
        //    Publicas.mensagemDeErro = string.Empty;
        //    Feriado _feriado = new Feriado();
        //    try
        //    {
        //        query.Append("Select f.dataferiado, f.descferiado, n.Nome Empresa ");
        //        query.Append("  From finferia_empresafilial e, finferia f, niff_chm_empresas n ");
        //        query.Append(" Where f.dataferiado = e.dataferiado ");
        //        query.Append("   And n.codigoglobus = Lpad(e.codigoempresa,3,'0') || '/' || Lpad(e.codigofl,3,'0')");
        //        query.Append("   And n.idempresa = " + idEmpresa);

        //        if (data != DateTime.MinValue)
        //            query.Append("   And f.dataferiado = To_Date('" + data.Date.ToShortDateString() + "', 'dd/mm/yyyy')");
        //        else
        //            query.Append("   And f.dataferiado = trunc(Sysdate) + 1 ");


        //        Query executar = sessao.CreateQuery(query.ToString());
        //        dataReader = executar.ExecuteQuery();

        //        using (dataReader)
        //        {
        //            if (dataReader.Read())
        //            {
        //                _feriado.Existe = true;
        //                try
        //                {
        //                    _feriado.Data = Convert.ToDateTime(dataReader["dataferiado"].ToString());
        //                }
        //                catch { }

        //                _feriado.Descricao = dataReader["descferiado"].ToString();
        //                //_feriado.Empresa = dataReader["Empresa"].ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Publicas.mensagemDeErro = ex.Message;
        //    }
        //    finally
        //    {
        //        sessao.Desconectar();
        //    }
        //    return _feriado;
        //}

        public int DiasUteis(int idEmpresa, DateTime data, DateTime dataFim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 0;
            try
            {
                query.Append("Select Sum(QtdDiasUteis) - Sum(QtdFeriado) DiasUteis");
                query.Append("  From (Select Count(*) QtdFeriado, 0 QtdDiasUteis");
                query.Append("          From niff_ads_feriadosemendas f, Niff_Calendario c ");
                query.Append("         Where f.dataferiado = e.dataferiado(+) ");
                query.Append("           And e.idempresa = " + idEmpresa);
                query.Append("           And f.data between To_Date('" + data.Date.ToShortDateString() + "', 'dd/mm/yyyy') ");
                query.Append("           And To_date('" + dataFim.Date.ToShortDateString() + "', 'dd/mm/yyyy') ");
                query.Append("           And c.fimsemana = 'N' ");// Apenas Feriados que cae em dias úteis
                query.Append("           And c.Data = f.data ");

                query.Append("         Union All ");
                query.Append("        Select 0 QtdFeriado, Count(*) QtdDiasUteis");
                query.Append("          From Niff_Calendario ");
                query.Append("         Where Data between To_Date('" + data.Date.ToShortDateString() + "', 'dd/mm/yyyy') ");
                query.Append("           And To_date('" + dataFim.Date.ToShortDateString() + "', 'dd/mm/yyyy') ");
                query.Append("           And FiMSemana = 'N') ");


                Query executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        try
                        {
                            retorno = Convert.ToInt32(dataReader["DiasUteis"].ToString());
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
            return retorno;
        }

        //public int FolgaComplementar(int idEmpresa, DateTime data, DateTime dataFim)
        //{
        //    StringBuilder query = new StringBuilder();
        //    Sessao sessao = new Sessao();
        //    Publicas.mensagemDeErro = string.Empty;
        //    int retorno = 0;
        //    try
        //    {
        //        query.Append("Select Sum(qtd) Qdt");
        //        query.Append("  From (Select Count(*) Qtd");
        //        query.Append("          From finferia_empresafilial e, finferia f, niff_chm_empresas n, Niff_Calendario c ");
        //        query.Append("         Where f.dataferiado = e.dataferiado(+) ");
        //        query.Append("           And n.codigoglobus = Lpad(e.codigoempresa,3,'0') || '/' || Lpad(e.codigofl,3,'0')");
        //        query.Append("           And n.idempresa = " + idEmpresa);
        //        query.Append("           And f.dataferiado between To_Date('" + data.Date.ToShortDateString() + "', 'dd/mm/yyyy') ");
        //        query.Append("           And To_date('" + dataFim.Date.ToShortDateString() + "', 'dd/mm/yyyy') ");
        //        query.Append("           And Upper(c.Diasemana) <> 'DOMINGO' ");// Apenas Feriados que cae não caia no domingo
        //        query.Append("           And c.Data = f.dataferiado ");

        //        query.Append("         Union All ");
        //        query.Append("        Select Count(*) Qtd");
        //        query.Append("          From Niff_Calendario ");
        //        query.Append("         Where Data between To_Date('" + data.Date.ToShortDateString() + "', 'dd/mm/yyyy') ");
        //        query.Append("           And To_date('" + dataFim.Date.ToShortDateString() + "', 'dd/mm/yyyy') ");
        //        query.Append("           And Upper(Diasemana) = 'DOMINGO') ");


        //        Query executar = sessao.CreateQuery(query.ToString());
        //        dataReader = executar.ExecuteQuery();

        //        using (dataReader)
        //        {
        //            if (dataReader.Read())
        //            {
        //                try
        //                {
        //                    retorno = Convert.ToInt32(dataReader["Qdt"].ToString());
        //                }
        //                catch { }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Publicas.mensagemDeErro = ex.Message;
        //    }
        //    finally
        //    {
        //        sessao.Desconectar();
        //    }
        //    return retorno;
        //}

        //public int QuantidadeDeFeriados(int idEmpresa, DateTime data, DateTime dataFim)
        //{
        //    StringBuilder query = new StringBuilder();
        //    Sessao sessao = new Sessao();
        //    Publicas.mensagemDeErro = string.Empty;
        //    int retorno = 0;
        //    try
        //    {
        //        query.Append("Select Count(*) Qtd");
        //        query.Append("  From finferia_empresafilial e, finferia f, niff_chm_empresas n, Niff_Calendario c ");
        //        query.Append(" Where f.dataferiado = e.dataferiado(+) ");
        //        query.Append("   And n.codigoglobus = Lpad(e.codigoempresa,3,'0') || '/' || Lpad(e.codigofl,3,'0')");
        //        query.Append("   And n.idempresa = " + idEmpresa);
        //        query.Append("   And f.dataferiado between To_Date('" + data.Date.ToShortDateString() + "', 'dd/mm/yyyy') ");
        //        query.Append("   And To_date('" + dataFim.Date.ToShortDateString() + "', 'dd/mm/yyyy') ");
        //        query.Append("   And Upper(c.Diasemana) <> 'DOMINGO' ");// Apenas Feriados que cae não caia no domingo
        //        query.Append("   And c.Data = f.dataferiado ");

        //        Query executar = sessao.CreateQuery(query.ToString());
        //        dataReader = executar.ExecuteQuery();

        //        using (dataReader)
        //        {
        //            if (dataReader.Read())
        //            {
        //                try
        //                {
        //                    retorno = Convert.ToInt32(dataReader["Qtd"].ToString());
        //                }
        //                catch { }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Publicas.mensagemDeErro = ex.Message;
        //    }
        //    finally
        //    {
        //        sessao.Desconectar();
        //    }
        //    return retorno;
        //}


    }
}
