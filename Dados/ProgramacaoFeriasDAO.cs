using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ProgramacaoFeriasDAO
    {
        IDataReader dataReader;
        
        // Periodo Aquisitivo
        public List<PeriodoAquisitivo> Listar(int idEmpresa, decimal CodIntFunc)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PeriodoAquisitivo> _lista = new List<PeriodoAquisitivo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, codintfunc, inicio, fim, Limite");
                query.Append("  from Niff_Dep_PeriodoAquisitivoFerias p");
                query.Append(" where p.CodIntFunc = " + CodIntFunc);
                query.Append("   and IdEmpresa = " + idEmpresa );                

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        PeriodoAquisitivo _tipo = new PeriodoAquisitivo();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.CodIntFunc = Convert.ToInt32(dataReader["CodIntFunc"].ToString());

                        _tipo.Inicio = Convert.ToDateTime(dataReader["inicio"].ToString());
                        _tipo.Fim = Convert.ToDateTime(dataReader["fim"].ToString());
                        _tipo.Ano = _tipo.Inicio.Year;

                        try
                        {
                            _tipo.Limite = Convert.ToDateTime(dataReader["Limite"].ToString());
                        }
                        catch { }

                        _tipo.Periodo = "Inicio: " + _tipo.Inicio.ToShortDateString() +
                            " Fim: " + _tipo.Fim.ToShortDateString() +
                            " Limite: " + _tipo.Limite.ToShortDateString();
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

        public List<PeriodoAquisitivo> Listar(int idEmpresa, decimal CodIntFunc, DateTime inicio)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PeriodoAquisitivo> _lista = new List<PeriodoAquisitivo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, codintfunc, inicio, fim, Limite");
                query.Append("  from Niff_Dep_PeriodoAquisitivoFerias p");
                query.Append(" where p.CodIntFunc = " + CodIntFunc);
                query.Append("   and IdEmpresa = " + idEmpresa);
                query.Append("   and (Inicio >= To_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("    or Fim >= To_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("    or Limite >= To_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy'))");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        PeriodoAquisitivo _tipo = new PeriodoAquisitivo();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.CodIntFunc = Convert.ToInt32(dataReader["CodIntFunc"].ToString());

                        _tipo.Inicio = Convert.ToDateTime(dataReader["inicio"].ToString());
                        _tipo.Fim = Convert.ToDateTime(dataReader["fim"].ToString());
                        _tipo.Ano = _tipo.Inicio.Year;

                        try
                        {
                            _tipo.Limite = Convert.ToDateTime(dataReader["Limite"].ToString());
                        }
                        catch { }

                        _tipo.Periodo = "Inicio: " + _tipo.Inicio.ToShortDateString() +
                            " Fim: " + _tipo.Fim.ToShortDateString() +
                            " Limite: " + _tipo.Limite.ToShortDateString();
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

        public bool Gravar(List<PeriodoAquisitivo> lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            try
            {
                foreach (var tipo in lista)
                {

                    query.Clear();
                    if (!tipo.Existe)
                    {
                        query.Append("Insert into Niff_Dep_PeriodoAquisitivoFerias");
                        query.Append(" ( id, idempresa, codintfunc, inicio, fim, Limite)");
                        query.Append(" Values ( (Select Nvl(Max(id),0) +1 next From Niff_Dep_PeriodoAquisitivoFerias)");
                        query.Append("        ," + tipo.IdEmpresa);
                        query.Append("        ," + tipo.CodIntFunc);
                        query.Append("        , To_date('" + tipo.Inicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("        , To_date('" + tipo.Fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("        , To_date('" + tipo.Limite.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append(" )");
                    }
                    else
                    {
                        query.Append("Update Niff_Dep_PeriodoAquisitivoFerias");
                        query.Append("   set IdEmpresa =" + tipo.IdEmpresa);
                        query.Append("     , Inicio = To_date('" + tipo.Inicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("     , Fim = To_date('" + tipo.Fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("     , Limite = To_date('" + tipo.Limite.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append(" Where id = " + tipo.Id);
                    }

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
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
            return retorno;
        }

        public bool ExcluirPeriodoAquisitivo(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Dep_PeriodoAquisitivoFerias");
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

        public bool ExcluirTodosPeriodosAquisitivos(decimal codIntFunc)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Dep_PeriodoAquisitivoFerias");
                query.Append(" Where codintFunc = " + codIntFunc);
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

        // Programação de Férias
        public List<ProgramacaoFerias> Listar(int idEmpresa, DateTime dataInicio, DateTime dataFim, bool somenteAprovadas, bool somenteNaoGozadas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ProgramacaoFerias> _lista = new List<ProgramacaoFerias>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, p.idempresa, p.codintfunc, p.idusuario, idusuarioautorizacao, datainicio, datafim, quantidadedias");
                query.Append("     , dataautorep, datasolicitacao, f.CodFunc || ' - ' ||  f.NomeFunc Funcionario");
                query.Append("     , VizualizadoPeloGerente, VizualizadoPeloCoordenador, Vizualizado, VisualizadoPeloDiretor");
                query.Append("     , IntegradoGlobus, d.descricao departamento, d.iddepartamento");
                query.Append("     , decode(p.Gozadas, 'S', 'Z', decode(p.Integradoglobus, 'S', 'I', Status)) Status");
                query.Append("     , p.IniPeriodoAquisitivo, p.FinPeriodoAquisitivo, p.limite, p.MotivoReprovacao");
                query.Append("  from Niff_Dep_ProgramacaoFerias p, flp_funcionarios f");
                query.Append("     , niff_chm_usuarios u, Niff_Chm_Departamento d");
                query.Append(" where f.CodintFunc = p.CodIntFunc");
                query.Append("   And u.codfunc = f.codintfunc");
                query.Append("   And u.Ativo = 'S'");
                query.Append("   And d.iddepartamento = u.Iddepartamento");
                query.Append("   and datainicio between To_date('" + dataInicio.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("   and To_date('" + dataFim.ToShortDateString() + "','dd/mm/yyyy') ");

                if (somenteNaoGozadas)
                    query.Append("   and p.gozadas = 'N'");

                if (Publicas._usuario.PodeIntegrarProgramacaoFerias)
                {
                    if (idEmpresa == 19)
                        query.Append("   and (p.IdEmpresa = " + idEmpresa + " or u.Iddepartamento = 26)");

                    if (somenteAprovadas)
                    {
                        query.Append("   and p.Status = 'A'");
                        query.Append("   and p.IntegradoGlobus = 'N'");
                    }
                }
                else
                {
                    query.Append("   and (p.codintfunc = " + Publicas._usuario.CodigoInternoFuncionarioGlobus);
                    query.Append("     or p.codintfunc in (Select u.codfunc ");
                    query.Append("                         From Niff_Chm_Usuarios u");
                    query.Append("                        Where (u.Iddepartamento In (Select Iddepartamento ");
                    query.Append("                                                     From niff_ads_colabdepartamento");
                    query.Append("                                                    Where idUsuario = " + Publicas._usuario.Id + ")");
                    if (Publicas._usuario.Diretor)
                        query.Append("                            or u.gerente = 'S')");
                    else
                        query.Append("                            or u.Iddepartamento = " + Publicas._usuario.IdDepartamento + ")");

                    if (Publicas._usuario.UsuarioAcesso == "VBSANTOS")
                        query.Append("                          and (IdEmpresa = " + idEmpresa + " or IdEmpresa = 19))");
                    else
                    {
                        if (Publicas._usuario.IdDepartamento != 26 || (Publicas._usuario.IdDepartamento == 26 && idEmpresa != 19 && idEmpresa != 2))
                        {
                            if (Publicas._usuario.UsuarioAcesso != "FFLOPES")
                                query.Append("                          and IdEmpresa = " + idEmpresa + ")");
                            else
                                query.Append("                          and (IdEmpresa = " + idEmpresa + " or IdEmpresa = 2))");
                        }
                        else
                            query.Append("                          and (IdEmpresa = " + idEmpresa + " or IdEmpresa = 2))");
                    }
                    query.Append("     or p.codintfunc in (Select u.codfunc ");
                    query.Append("                         From Niff_Chm_Usuarios u");
                    query.Append("                        Where u.idUsuario In (Select idUsuario ");
                    query.Append("                                                From niff_ads_colabdepartamento");
                    query.Append("                                               Where Iddepartamento = " + Publicas._usuario.IdDepartamento + ")");
                    if (Publicas._usuario.UsuarioAcesso == "VBSANTOS")
                        query.Append("                          and (IdEmpresa = " + idEmpresa + " or IdEmpresa = 19)))");
                    else
                    {
                        if (Publicas._usuario.IdDepartamento != 26 || (Publicas._usuario.IdDepartamento == 26 && idEmpresa != 19 && idEmpresa != 2))
                            query.Append("                          and IdEmpresa = " + idEmpresa + "))");
                        else
                            query.Append("                          and (IdEmpresa = " + idEmpresa + " or IdEmpresa = 2)))");
                    }
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ProgramacaoFerias _tipo = new ProgramacaoFerias();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Funcionario = dataReader["Funcionario"].ToString();

                        _tipo.MotivoReprovacao = dataReader["MotivoReprovacao"].ToString();

                        _tipo.CodIntFunc = Convert.ToInt32(dataReader["CodIntFunc"].ToString());
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.Visualizado = dataReader["Vizualizado"].ToString() == "S";
                        _tipo.VisualizadoPeloCoordenador = dataReader["VizualizadoPeloCoordenador"].ToString() == "S";
                        _tipo.VisualizadoPeloGerente = dataReader["VizualizadoPeloGerente"].ToString() == "S";
                        _tipo.VisualizadoPeloDiretor = dataReader["VisualizadoPeloDiretor"].ToString() == "S";
                        _tipo.IntegradoGlobus = dataReader["IntegradoGlobus"].ToString() == "S";
                        _tipo.Departamento = dataReader["Departamento"].ToString();

                        try
                        {
                            _tipo.IdDepartamento = Convert.ToInt32(dataReader["Iddepartamento"].ToString());
                        }
                        catch { }

                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        _tipo.QuantidadeDias = Convert.ToInt32(dataReader["quantidadedias"].ToString());
                        try
                        {
                            _tipo.IdUsuarioAutorizacao = Convert.ToInt32(dataReader["idusuarioautorizacao"].ToString());
                        }
                        catch {}

                        _tipo.DataInicio = Convert.ToDateTime(dataReader["datainicio"].ToString());
                        _tipo.DataFim = Convert.ToDateTime(dataReader["datafim"].ToString());
                        _tipo.DataSolicitacao = Convert.ToDateTime(dataReader["datasolicitacao"].ToString());

                        try
                        {
                            _tipo.DataAutorizacao = Convert.ToDateTime(dataReader["dataautorep"].ToString());
                        }
                        catch { }
                        
                        try
                        {
                            _tipo.IniPeriodoAquisitivo = Convert.ToDateTime(dataReader["IniPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.FimPeriodoAquisitivo = Convert.ToDateTime(dataReader["FinPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Limite = Convert.ToDateTime(dataReader["Limite"].ToString());
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

        public List<ProgramacaoFerias> Listar(int codIntFunc)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ProgramacaoFerias> _lista = new List<ProgramacaoFerias>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, p.codintfunc, idusuario, idusuarioautorizacao, datainicio, datafim, quantidadedias");
                query.Append("     , dataautorep, status, datasolicitacao, Gozadas, f.CodFunc || ' - ' ||  f.NomeFunc Funcionario");
                query.Append("     , VizualizadoPeloGerente, VizualizadoPeloCoordenador, Vizualizado, VisualizadoPeloDiretor");
                query.Append("     , p.IniPeriodoAquisitivo, p.FinPeriodoAquisitivo, p.limite, p.MotivoReprovacao");
                query.Append("  from Niff_Dep_ProgramacaoFerias p, flp_funcionarios f");
                query.Append(" where f.CodintFunc = p.CodIntFunc");
                query.Append("   and f.CodIntFunc = " + codIntFunc);
                query.Append("   and p.Gozadas = 'N'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ProgramacaoFerias _tipo = new ProgramacaoFerias();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Funcionario = dataReader["Funcionario"].ToString();
                        _tipo.MotivoReprovacao = dataReader["MotivoReprovacao"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.Gozadas = dataReader["Gozadas"].ToString() == "S";
                        _tipo.Visualizado = dataReader["Vizualizado"].ToString() == "S";
                        _tipo.VisualizadoPeloCoordenador = dataReader["VizualizadoPeloCoordenador"].ToString() == "S";
                        _tipo.VisualizadoPeloGerente = dataReader["VizualizadoPeloGerente"].ToString() == "S";
                        _tipo.VisualizadoPeloDiretor = dataReader["VisualizadoPeloDiretor"].ToString() == "S";

                        _tipo.CodIntFunc = Convert.ToInt32(dataReader["CodIntFunc"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        _tipo.QuantidadeDias = Convert.ToInt32(dataReader["quantidadedias"].ToString());
                        try
                        {
                            _tipo.IdUsuarioAutorizacao = Convert.ToInt32(dataReader["idusuarioautorizacao"].ToString());
                        }
                        catch { }

                        _tipo.DataInicio = Convert.ToDateTime(dataReader["datainicio"].ToString());
                        _tipo.DataFim = Convert.ToDateTime(dataReader["datafim"].ToString());
                        _tipo.DataSolicitacao = Convert.ToDateTime(dataReader["datasolicitacao"].ToString());

                        try
                        {
                            _tipo.DataAutorizacao = Convert.ToDateTime(dataReader["dataautorep"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IniPeriodoAquisitivo = Convert.ToDateTime(dataReader["IniPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.FimPeriodoAquisitivo = Convert.ToDateTime(dataReader["FinPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Limite = Convert.ToDateTime(dataReader["Limite"].ToString());
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

        public List<ProgramacaoFerias> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ProgramacaoFerias> _lista = new List<ProgramacaoFerias>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, p.codintfunc, idusuario, idusuarioautorizacao, datainicio, datafim, quantidadedias");
                query.Append("     , dataautorep, status, datasolicitacao, Gozadas, f.CodFunc || ' - ' ||  f.NomeFunc Funcionario");
                query.Append("     , VizualizadoPeloGerente, VizualizadoPeloCoordenador, Vizualizado, VisualizadoPeloDiretor");
                query.Append("     , p.IniPeriodoAquisitivo, p.FinPeriodoAquisitivo, p.limite, p.MotivoReprovacao");
                query.Append("  from Niff_Dep_ProgramacaoFerias p, flp_funcionarios f");
                query.Append(" where f.CodintFunc = p.CodIntFunc");
                query.Append("   and Status = 'G'");
                query.Append("   and Gozadas = 'N'");

                if (Publicas._usuario.Gerente)
                    query.Append("   and VizualizadoPeloGerente = 'N'");
                if (Publicas._usuario.Coordenador)
                    query.Append("   and VizualizadoPeloCoordenador = 'N'");
                if (Publicas._usuario.Diretor)
                    query.Append("   and VisualizadoPeloDiretor = 'N'");

                query.Append("   and (p.codintfunc = " + Publicas._usuario.CodigoInternoFuncionarioGlobus);
                query.Append("     or p.codintfunc in (Select u.codfunc ");
                query.Append("                         From Niff_Chm_Usuarios u");
                query.Append("                        Where (u.Iddepartamento In (Select Iddepartamento ");
                query.Append("                                                     From niff_ads_colabdepartamento");
                query.Append("                                                    Where idUsuario = " + Publicas._usuario.Id + ")");
                if (Publicas._usuario.Diretor)
                    query.Append("                            or u.gerente = 'S')");
                else
                    query.Append("                            or u.Iddepartamento = " + Publicas._usuario.IdDepartamento + ")");
                
                if (Publicas._usuario.UsuarioAcesso == "VBSANTOS") 
                    query.Append("                          and (IdEmpresa = " + Publicas._usuario.IdEmpresa + " or IdEmpresa = 19))");
                else
                {
                    if (Publicas._usuario.IdDepartamento != 26 || (Publicas._usuario.IdDepartamento == 26 && Publicas._usuario.IdEmpresa != 19 && Publicas._usuario.IdEmpresa != 2))
                    {
                        if (Publicas._usuario.UsuarioAcesso != "FFLOPES")
                            query.Append("                          and IdEmpresa = " + Publicas._usuario.IdEmpresa + ")");
                        else
                            query.Append("                          and (IdEmpresa = " + Publicas._usuario.IdEmpresa + " or IdEmpresa = 2))");
                    }
                    else
                        query.Append("                          and (IdEmpresa = " + Publicas._usuario.IdEmpresa + " or IdEmpresa = 2))");
                }

                query.Append("     or p.codintfunc in (Select u.codfunc ");
                query.Append("                         From Niff_Chm_Usuarios u");
                query.Append("                        Where u.idUsuario In (Select idUsuario ");
                query.Append("                                                From niff_ads_colabdepartamento");
                query.Append("                                               Where Iddepartamento = " + Publicas._usuario.IdDepartamento + ")");
                if (Publicas._usuario.UsuarioAcesso == "VBSANTOS")
                    query.Append("                          and (IdEmpresa = " + Publicas._usuario.IdEmpresa + " or IdEmpresa = 19)))");
                else
                {
                    if (Publicas._usuario.IdDepartamento != 26 || (Publicas._usuario.IdDepartamento == 26 && Publicas._usuario.IdEmpresa != 19 && Publicas._usuario.IdEmpresa != 2))
                        query.Append("                          and IdEmpresa = " + Publicas._usuario.IdEmpresa + "))");
                    else
                        query.Append("                          and (IdEmpresa = " + Publicas._usuario.IdEmpresa + " or IdEmpresa = 2)))");
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ProgramacaoFerias _tipo = new ProgramacaoFerias();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Funcionario = dataReader["Funcionario"].ToString();
                        _tipo.MotivoReprovacao = dataReader["MotivoReprovacao"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.Gozadas = dataReader["Gozadas"].ToString() == "S";
                        _tipo.Visualizado = dataReader["Vizualizado"].ToString() == "S";
                        _tipo.VisualizadoPeloCoordenador = dataReader["VizualizadoPeloCoordenador"].ToString() == "S";
                        _tipo.VisualizadoPeloGerente = dataReader["VizualizadoPeloGerente"].ToString() == "S";
                        _tipo.VisualizadoPeloDiretor = dataReader["VisualizadoPeloDiretor"].ToString() == "S";

                        _tipo.CodIntFunc = Convert.ToInt32(dataReader["CodIntFunc"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        _tipo.QuantidadeDias = Convert.ToInt32(dataReader["quantidadedias"].ToString());
                        try
                        {
                            _tipo.IdUsuarioAutorizacao = Convert.ToInt32(dataReader["idusuarioautorizacao"].ToString());
                        }
                        catch { }

                        _tipo.DataInicio = Convert.ToDateTime(dataReader["datainicio"].ToString());
                        _tipo.DataFim = Convert.ToDateTime(dataReader["datafim"].ToString());
                        _tipo.DataSolicitacao = Convert.ToDateTime(dataReader["datasolicitacao"].ToString());

                        try
                        {
                            _tipo.DataAutorizacao = Convert.ToDateTime(dataReader["dataautorep"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IniPeriodoAquisitivo = Convert.ToDateTime(dataReader["IniPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.FimPeriodoAquisitivo = Convert.ToDateTime(dataReader["FinPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Limite = Convert.ToDateTime(dataReader["Limite"].ToString());
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

        public List<ProgramacaoFerias> ListarTodas()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ProgramacaoFerias> _lista = new List<ProgramacaoFerias>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, p.codintfunc, idusuario, idusuarioautorizacao, datainicio, datafim, quantidadedias");
                query.Append("     , dataautorep, status, datasolicitacao, Gozadas, f.CodFunc || ' - ' ||  f.NomeFunc Funcionario");
                query.Append("     , VizualizadoPeloGerente, VizualizadoPeloCoordenador, Vizualizado, VisualizadoPeloDiretor");
                query.Append("     , p.IniPeriodoAquisitivo, p.FinPeriodoAquisitivo, p.limite, p.MotivoReprovacao");
                query.Append("  from Niff_Dep_ProgramacaoFerias p, flp_funcionarios f");
                query.Append(" where f.CodintFunc = p.CodIntFunc");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ProgramacaoFerias _tipo = new ProgramacaoFerias();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Funcionario = dataReader["Funcionario"].ToString();
                        _tipo.MotivoReprovacao = dataReader["MotivoReprovacao"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.Gozadas = dataReader["Gozadas"].ToString() == "S";
                        _tipo.Visualizado = dataReader["Vizualizado"].ToString() == "S";
                        _tipo.VisualizadoPeloCoordenador = dataReader["VizualizadoPeloCoordenador"].ToString() == "S";
                        _tipo.VisualizadoPeloGerente = dataReader["VizualizadoPeloGerente"].ToString() == "S";
                        _tipo.VisualizadoPeloDiretor = dataReader["VisualizadoPeloDiretor"].ToString() == "S";

                        _tipo.CodIntFunc = Convert.ToInt32(dataReader["CodIntFunc"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        _tipo.QuantidadeDias = Convert.ToInt32(dataReader["quantidadedias"].ToString());
                        try
                        {
                            _tipo.IdUsuarioAutorizacao = Convert.ToInt32(dataReader["idusuarioautorizacao"].ToString());
                        }
                        catch { }

                        _tipo.DataInicio = Convert.ToDateTime(dataReader["datainicio"].ToString());
                        _tipo.DataFim = Convert.ToDateTime(dataReader["datafim"].ToString());
                        _tipo.DataSolicitacao = Convert.ToDateTime(dataReader["datasolicitacao"].ToString());

                        try
                        {
                            _tipo.DataAutorizacao = Convert.ToDateTime(dataReader["dataautorep"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IniPeriodoAquisitivo = Convert.ToDateTime(dataReader["IniPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.FimPeriodoAquisitivo = Convert.ToDateTime(dataReader["FinPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Limite = Convert.ToDateTime(dataReader["Limite"].ToString());
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

        public List<ProgramacaoFerias> ListarPeriodoIguaisDeFerias(int codigo, DateTime data, DateTime dataFim, int idEmpresa, int idDepartamento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ProgramacaoFerias> _lista = new List<ProgramacaoFerias>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, p.idempresa, p.codintfunc, p.idusuario, idusuarioautorizacao, datainicio, datafim, quantidadedias");
                query.Append("     , dataautorep, status, datasolicitacao, Gozadas, f.CodFunc || ' - ' ||  f.NomeFunc Funcionario");
                query.Append("     , VizualizadoPeloGerente, VizualizadoPeloCoordenador, Vizualizado, VisualizadoPeloDiretor, d.Iddepartamento");
                query.Append("     , p.IniPeriodoAquisitivo, p.FinPeriodoAquisitivo, p.limite");
                query.Append("  from Niff_Dep_ProgramacaoFerias p, flp_funcionarios f");
                query.Append("     , niff_chm_usuarios u, Niff_Chm_Departamento d");
                query.Append(" where f.CodintFunc = p.CodIntFunc");
                query.Append("   And u.codfunc = f.codintfunc");
                query.Append("   And d.iddepartamento = u.Iddepartamento");
                query.Append("   and (To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') between p.datainicio And p.datafim");
                query.Append("    or  To_date('" + dataFim.ToShortDateString() + "','dd/mm/yyyy') between p.datainicio And p.datafim");
                query.Append("    or  p.datainicio between To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') and To_date('" + dataFim.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("    or  p.datafim between To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') and To_date('" + dataFim.ToShortDateString() + "','dd/mm/yyyy')) ");
                query.Append("   and p.codintfunc <> " + codigo);
                query.Append("   and p.Status <> 'R'");
                query.Append("   and (p.codintfunc in (Select u.codfunc ");
                query.Append("                           From Niff_Chm_Usuarios u");
                query.Append("                          Where u.idUsuario In (Select idUsuario ");
                query.Append("                                                  From niff_ads_colabdepartamento");
                query.Append("                                                  Where Iddepartamento = " + idDepartamento + ")");
                query.Append("                            and IdEmpresa = " + idEmpresa + ")");
                query.Append("    or p.codintfunc in (Select u.codfunc ");
                query.Append("                          From Niff_Chm_Usuarios u");
                query.Append("                        Where Iddepartamento = " + idDepartamento );
                query.Append("                          and IdEmpresa = " + idEmpresa + "))");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ProgramacaoFerias _tipo = new ProgramacaoFerias();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Funcionario = dataReader["Funcionario"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.Gozadas = dataReader["Gozadas"].ToString() == "S";
                        _tipo.Visualizado = dataReader["Vizualizado"].ToString() == "S";
                        _tipo.VisualizadoPeloCoordenador = dataReader["VizualizadoPeloCoordenador"].ToString() == "S";
                        _tipo.VisualizadoPeloGerente = dataReader["VizualizadoPeloGerente"].ToString() == "S";
                        _tipo.VisualizadoPeloDiretor = dataReader["VisualizadoPeloDiretor"].ToString() == "S";

                        _tipo.CodIntFunc = Convert.ToInt32(dataReader["CodIntFunc"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        _tipo.QuantidadeDias = Convert.ToInt32(dataReader["quantidadedias"].ToString());

                        try
                        {
                            _tipo.IdDepartamento = Convert.ToInt32(dataReader["IdDepartamento"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdUsuarioAutorizacao = Convert.ToInt32(dataReader["idusuarioautorizacao"].ToString());
                        }
                        catch { }

                        _tipo.DataInicio = Convert.ToDateTime(dataReader["datainicio"].ToString());
                        _tipo.DataFim = Convert.ToDateTime(dataReader["datafim"].ToString());
                        _tipo.DataSolicitacao = Convert.ToDateTime(dataReader["datasolicitacao"].ToString());

                        try
                        {
                            _tipo.DataAutorizacao = Convert.ToDateTime(dataReader["dataautorep"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IniPeriodoAquisitivo = Convert.ToDateTime(dataReader["IniPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.FimPeriodoAquisitivo = Convert.ToDateTime(dataReader["FinPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Limite = Convert.ToDateTime(dataReader["Limite"].ToString());
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

        public ProgramacaoFerias Consultar(int codIntFunc, DateTime dataInicio)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            ProgramacaoFerias _tipo = new ProgramacaoFerias();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select p.id, p.idempresa, p.codintfunc, p.idusuario, p.idusuarioautorizacao, p.datainicio, p.datafim, p.quantidadedias");
                query.Append("     , p.dataautorep, p.status, p.datasolicitacao, Gozadas, f.CodFunc || ' - ' ||  f.NomeFunc Funcionario");
                query.Append("     , VizualizadoPeloGerente, VizualizadoPeloCoordenador, Vizualizado, VisualizadoPeloDiretor");
                query.Append("     , p.IniPeriodoAquisitivo, p.FinPeriodoAquisitivo, p.limite, p.MotivoReprovacao");
                query.Append("  from Niff_Dep_ProgramacaoFerias p, flp_funcionarios f");
                query.Append(" where f.CodintFunc = p.CodIntFunc");
                query.Append("   and f.CodIntFunc = " + codIntFunc);
                query.Append("   and DataInicio = To_date('" + dataInicio.ToShortDateString() + "','dd/mm/yyyy')");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Funcionario = dataReader["Funcionario"].ToString();
                        _tipo.MotivoReprovacao = dataReader["MotivoReprovacao"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.Gozadas = dataReader["Gozadas"].ToString() == "S";
                        _tipo.Visualizado = dataReader["Vizualizado"].ToString() == "S";
                        _tipo.VisualizadoPeloCoordenador = dataReader["VizualizadoPeloCoordenador"].ToString() == "S";
                        _tipo.VisualizadoPeloGerente = dataReader["VizualizadoPeloGerente"].ToString() == "S";
                        _tipo.VisualizadoPeloDiretor = dataReader["VisualizadoPeloDiretor"].ToString() == "S";

                        _tipo.CodIntFunc = Convert.ToInt32(dataReader["CodIntFunc"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        _tipo.QuantidadeDias = Convert.ToInt32(dataReader["quantidadedias"].ToString());
                        try
                        {
                            _tipo.IdUsuarioAutorizacao = Convert.ToInt32(dataReader["idusuarioautorizacao"].ToString());
                        }
                        catch { }

                        _tipo.DataInicio = Convert.ToDateTime(dataReader["datainicio"].ToString());
                        _tipo.DataFim = Convert.ToDateTime(dataReader["datafim"].ToString());
                        _tipo.DataSolicitacao = Convert.ToDateTime(dataReader["datasolicitacao"].ToString());

                        try
                        {
                            _tipo.DataAutorizacao = Convert.ToDateTime(dataReader["dataautorep"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IniPeriodoAquisitivo = Convert.ToDateTime(dataReader["IniPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.FimPeriodoAquisitivo = Convert.ToDateTime(dataReader["FinPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Limite = Convert.ToDateTime(dataReader["Limite"].ToString());
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

        public ProgramacaoFerias ConsultarInicioFerias(decimal codIntFunc, DateTime data)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            ProgramacaoFerias _tipo = new ProgramacaoFerias();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select p.id, p.idempresa, p.codintfunc, p.idusuario, p.idusuarioautorizacao, p.datainicio, p.datafim, p.quantidadedias");
                query.Append("     , p.dataautorep, p.status, p.datasolicitacao, Gozadas, f.CodFunc || ' - ' ||  f.NomeFunc Funcionario");
                query.Append("     , VizualizadoPeloGerente, VizualizadoPeloCoordenador, Vizualizado, VisualizadoPeloDiretor");
                query.Append("     , p.IniPeriodoAquisitivo, p.FinPeriodoAquisitivo, p.limite, p.MotivoReprovacao");
                query.Append("  from Niff_Dep_ProgramacaoFerias p, flp_funcionarios f");
                query.Append(" where f.CodintFunc = p.CodIntFunc");
                query.Append("   and f.CodIntFunc = " + codIntFunc);
                query.Append("   and DataInicio = (Select Max(DataInicio) From niff_dep_programacaoferias ");
                query.Append("                      where CodintFunc = " + codIntFunc);
                query.Append("                        and dataInicio <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy'))");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Funcionario = dataReader["Funcionario"].ToString();
                        _tipo.MotivoReprovacao = dataReader["MotivoReprovacao"].ToString();
                        _tipo.Status = dataReader["Status"].ToString();
                        _tipo.Gozadas = dataReader["Gozadas"].ToString() == "S";
                        _tipo.Visualizado = dataReader["Vizualizado"].ToString() == "S";
                        _tipo.VisualizadoPeloCoordenador = dataReader["VizualizadoPeloCoordenador"].ToString() == "S";
                        _tipo.VisualizadoPeloGerente = dataReader["VizualizadoPeloGerente"].ToString() == "S";
                        _tipo.VisualizadoPeloDiretor = dataReader["VisualizadoPeloDiretor"].ToString() == "S";

                        _tipo.CodIntFunc = Convert.ToInt32(dataReader["CodIntFunc"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        _tipo.QuantidadeDias = Convert.ToInt32(dataReader["quantidadedias"].ToString());
                        try
                        {
                            _tipo.IdUsuarioAutorizacao = Convert.ToInt32(dataReader["idusuarioautorizacao"].ToString());
                        }
                        catch { }

                        _tipo.DataInicio = Convert.ToDateTime(dataReader["datainicio"].ToString());
                        _tipo.DataFim = Convert.ToDateTime(dataReader["datafim"].ToString());
                        _tipo.DataSolicitacao = Convert.ToDateTime(dataReader["datasolicitacao"].ToString());

                        try
                        {
                            _tipo.DataAutorizacao = Convert.ToDateTime(dataReader["dataautorep"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IniPeriodoAquisitivo = Convert.ToDateTime(dataReader["IniPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.FimPeriodoAquisitivo = Convert.ToDateTime(dataReader["FinPeriodoAquisitivo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Limite = Convert.ToDateTime(dataReader["Limite"].ToString());
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

        public bool Gravar(ProgramacaoFerias tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into Niff_Dep_ProgramacaoFerias");
                    query.Append(" ( id, idempresa, codintfunc, idusuario, datainicio, datafim, quantidadedias");
                    query.Append("     , status, datasolicitacao, gozadas, IniPeriodoAquisitivo, FinPeriodoAquisitivo, limite)");
                    query.Append(" Values ( (Select Nvl(Max(id),0) +1 next From Niff_Dep_ProgramacaoFerias)");
                    query.Append("        ," + tipo.IdEmpresa );
                    query.Append("        ," + tipo.CodIntFunc);
                    query.Append("        ," + Publicas._usuario.Id);
                    query.Append("        , To_date('" + tipo.DataInicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        , To_date('" + tipo.DataFim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        ," + tipo.QuantidadeDias);
                    query.Append("        ,'" + tipo.Status + "'");
                    query.Append("        , sysdate");
                    query.Append("        , '" + (tipo.Gozadas ? "S" : "N") + "'");
                    query.Append("        , To_date('" + tipo.IniPeriodoAquisitivo.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        , To_date('" + tipo.FimPeriodoAquisitivo.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        , To_date('" + tipo.Limite.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_Dep_ProgramacaoFerias");
                    query.Append("   set IdEmpresa =" + tipo.IdEmpresa );
                    query.Append("     , IdUsuario =" + Publicas._usuario.Id);
                    query.Append("     , DataInicio = To_date('" + tipo.DataInicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , DataFim = To_date('" + tipo.DataFim.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , QuantidadeDias = " + tipo.QuantidadeDias);
                    query.Append("     , Status = '" + tipo.Status + "'");
                    query.Append("     , Gozadas = '" + (tipo.Gozadas ? "S" : "N")+ "'");
                    query.Append("     , VizualizadoPeloGerente = '" + (tipo.VisualizadoPeloGerente ? "S" : "N") + "'");
                    query.Append("     , VizualizadoPeloCoordenador = '" + (tipo.VisualizadoPeloCoordenador ? "S" : "N") + "'");
                    query.Append("     , Vizualizado = '" + (tipo.Visualizado ? "S" : "N") + "'");
                    query.Append("     , VisualizadoPeloDiretor = '" + (tipo.VisualizadoPeloDiretor ? "S" : "N") + "'");

                    query.Append("     , IniPeriodoAquisitivo = To_date('" + tipo.IniPeriodoAquisitivo.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , FinPeriodoAquisitivo = To_date('" + tipo.FimPeriodoAquisitivo.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , Limite = To_date('" + tipo.Limite.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("     , MotivoReprovacao = '" + tipo.MotivoReprovacao + "'");

                    if (tipo.Status != "G" && tipo.DataAutorizacao == DateTime.MinValue)
                    {
                        query.Append("     , DataAutoRep = sysDate");
                        query.Append("     , IdUsuarioAutorizacao = " + Publicas._usuario.Id);
                    }
                    query.Append(" Where id = " + tipo.Id);
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

        public bool GravarComoVisto(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();

                query.Append("Update Niff_Dep_ProgramacaoFerias");
                query.Append("   set ");
                if (Publicas._usuario.Gerente)
                    query.Append("     VizualizadoPeloGerente = 'S'");
                else
                {
                    if (Publicas._usuario.Coordenador)
                        query.Append("     VizualizadoPeloCoordenador = 'S'");
                    else
                    {
                        if (Publicas._usuario.Diretor)
                            query.Append("     VisualizadoPeloDiretor = 'S'");
                        else
                            query.Append("     Vizualizado = 'S'");
                    }
                }
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

        public bool GravarFeriasGozadas(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();

                query.Append("Update Niff_Dep_ProgramacaoFerias");
                query.Append("   set Gozadas = 'S'");
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

        public bool GravarComoIntegrado(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();

                query.Append("Update Niff_Dep_ProgramacaoFerias");
                query.Append("   set IntegradoGlobus = 'S'");
                query.Append("     , idUsuarioIntGlobus = " + Publicas._usuario.Id);
                query.Append("     , DataIntegradoGlobus = Sysdate");
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

        public bool Excluir(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Dep_ProgramacaoFerias");
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

        // integra Globus
        public bool Gravar(List<ProgramacaoFeriasGlobus> lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                foreach (var tipo in lista)
                {
                    query.Clear();
                    query.Append("Insert into Flp_Programacaoferias");
                    query.Append(" ( codintfunc, peraquiiniprog, peraquifinprog, gozoiniprog, dataprog, usuarioprog, dias_ferias )");
                    query.Append(" Values ( " + tipo.CodIntFunc);
                    query.Append("        , To_date('" + tipo.AquisitivoInicial.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        , To_date('" + tipo.AquisitivoFinal.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        , To_date('" + tipo.GozoInicial.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        , sysdate");
                    query.Append("        ,'" + Publicas._usuario.UsuarioAcesso + "'");
                    query.Append("        ," + tipo.DiasFerias);
                    query.Append(" )");

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (retorno)
                        retorno = GravarComoIntegrado(tipo.IdProgramacao);
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
