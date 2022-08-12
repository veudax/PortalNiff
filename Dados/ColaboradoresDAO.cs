using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ColaboradoresDAO
    {
        IDataReader dataReader;

        public List<Colaboradores> ListarEmailPorTipoCargo(string tipoCargo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Colaboradores> _lista = new List<Colaboradores>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select f.CodIntFunc, u.nome, SituacaoFunc, u.email, c.IdColaborador, c.Ativo");
                query.Append("  from niff_ads_colaboradores c     ");
                query.Append("     , vw_funcionarios f, niff_chm_usuarios u, niff_ads_cargos g");

                query.Append(" Where c.codintfunc = f.CODINTFUNC ");
                query.Append("   And c.idcargo = g.idcargo");
                query.Append("   And c.codintfunc = u.codfunc");
                query.Append("   And g.tipodocargo in (" + tipoCargo + ")");
                query.Append("   And c.Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Colaboradores _tipo = new Colaboradores();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Email = dataReader["Email"].ToString();

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

        public List<Colaboradores> Listar(bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Colaboradores> _lista = new List<Colaboradores>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select f.CodIntFunc, NomeFunc, CodFunc, ChapaFunc, DescArea, f.DTNASCTOFUNC, SituacaoFunc, f.SALBASE, f.DTADMFUNC, d.nrdocto, q.dtdesligquita");
                query.Append("     , c.iddepartamento, c.idcargo, c.Idcolaborador, c.idsuperior, c.Idempresa, c.IdEscolaridade, c.ParticipaAvaliacao, c.Sexo, c.Ativo");
                query.Append("  from niff_ads_colaboradores c     ");
                query.Append("     , vw_funcionarios f, flp_documentos d, flp_quitacao q");

                query.Append(" Where c.codintfunc = f.CODINTFUNC ");

                if (apenasAtivos)
                    query.Append("   And q.dtdesligquita is null");

                query.Append("   And d.codintfunc = f.CODINTFUNC(+)");
                query.Append("   And f.CODINTFUNC = q.codintfunc(+)");
                query.Append("   And d.tipodocto = 'CPF'");
                query.Append("   And c.Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Colaboradores _tipo = new Colaboradores();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        try
                        {
                            _tipo.IdSupervisor = Convert.ToInt32(dataReader["idsuperior"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdCargo = Convert.ToInt32(dataReader["IdCargo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdDepartamento = Convert.ToInt32(dataReader["IdDepartamento"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEscolaridade = Convert.ToInt32(dataReader["IdEscolaridade"].ToString());
                        }
                        catch { }

                        _tipo.Nome = dataReader["NomeFunc"].ToString();
                        _tipo.Codigo = dataReader["CodFunc"].ToString();
                        _tipo.CodigoInternoFuncionarioGlobus = Convert.ToInt32(dataReader["CodIntFunc"].ToString());
                        _tipo.DataAdmissao = Convert.ToDateTime(dataReader["DTADMFUNC"].ToString());
                        _tipo.DataNascimento = Convert.ToDateTime(dataReader["DTNASCTOFUNC"].ToString());
                        _tipo.DataDesligamento = Convert.ToDateTime(dataReader["dtdesligquita"].ToString());
                        _tipo.CPF = dataReader["nrdocto"].ToString();
                        _tipo.Salario = Convert.ToDecimal(dataReader["SALBASE"].ToString());
                        _tipo.ParticipaDaAvaliacao = dataReader["ParticipaAvaliacao"].ToString() == "S";
                        _tipo.Sexo = dataReader["Sexo"].ToString();
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

        public List<Colaboradores> ListarColaboradoresBolao(int IdEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Colaboradores> _lista = new List<Colaboradores>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Nome, c.Idcolaborador, c.sexo, c.Ativo");
                query.Append("  from niff_ads_colaboradores c ");
                query.Append(" Where c.IdEmpresa = " + IdEmpresa);
                query.Append("   And c.Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Colaboradores _tipo = new Colaboradores();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Sexo = dataReader["Sexo"].ToString();
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

        public List<Colaboradores> ListarColaboradoresParticipantes(int IdEmpresa, int Campanha, bool participante)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Colaboradores> _lista = new List<Colaboradores>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Nome, c.Idcolaborador, c.sexo, c.Ativo");
                query.Append("  from niff_ads_colaboradores c, niff_bol_enqueteresp e, niff_bol_enquete p");
                query.Append(" Where e.Idpergunta = " + Campanha);
                query.Append("   and p.IdEmpresa = " + IdEmpresa);
                query.Append("   and e.Idcolaborador = c.Idcolaborador");
                query.Append("   and e.Idpergunta = p.Id");
                query.Append("   And c.Ativo = 'S'");

                if (participante)
                    query.Append("   and Opcao = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Colaboradores _tipo = new Colaboradores();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Sexo = dataReader["Sexo"].ToString();
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

        public Colaboradores Consulta(string empresa, string codigo, bool filtrarEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Colaboradores _tipo = new Colaboradores();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select f.CodIntFunc, NomeFunc, CodFunc, ChapaFunc, DescArea, f.DTNASCTOFUNC, SituacaoFunc, f.SALBASE, f.DTADMFUNC, d.nrdocto, q.dtdesligquita");
                query.Append("     , c.iddepartamento, c.idcargo, c.Idcolaborador, c.idsuperior, c.Idempresa, c.IdEscolaridade, c.ParticipaAvaliacao, c.sexo, c.Ativo");
                query.Append("     , (Select max(h.salario) From niff_ads_historicocolaborador h Where h.Idcolaborador = c.Idcolaborador) Salario");
                query.Append("  from niff_ads_colaboradores c     ");
                query.Append("     , vw_funcionarios f, flp_documentos d, flp_quitacao q");

                query.Append(" Where c.codintfunc = f.CODINTFUNC ");
                query.Append("   And f.CodFunc = '" + codigo + "'");

                if (filtrarEmpresa)
                {
                    if (empresa == "009/001")
                        query.Append("   And Lpad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') = '009/002'");
                    else
                        query.Append("   And Lpad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                }

                query.Append("   And d.codintfunc = f.CODINTFUNC(+)");
                query.Append("   And f.CODINTFUNC = q.codintfunc(+)");
                query.Append("   And d.tipodocto = 'CPF'");
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        try
                        {
                            _tipo.IdSupervisor = Convert.ToInt32(dataReader["idsuperior"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdCargo = Convert.ToInt32(dataReader["IdCargo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdDepartamento = Convert.ToInt32(dataReader["IdDepartamento"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEscolaridade = Convert.ToInt32(dataReader["IdEscolaridade"].ToString());
                        }
                        catch { }

                        _tipo.Nome = dataReader["NomeFunc"].ToString();
                        _tipo.Codigo = dataReader["CodFunc"].ToString();
                        _tipo.CodigoInternoFuncionarioGlobus = Convert.ToInt32(dataReader["CodIntFunc"].ToString());
                        _tipo.DataAdmissao = Convert.ToDateTime(dataReader["DTADMFUNC"].ToString());
                        _tipo.DataNascimento = Convert.ToDateTime(dataReader["DTNASCTOFUNC"].ToString());
                        _tipo.ParticipaDaAvaliacao = dataReader["ParticipaAvaliacao"].ToString() == "S";
                        _tipo.Sexo = dataReader["Sexo"].ToString();

                        try
                        {
                            _tipo.DataDesligamento = Convert.ToDateTime(dataReader["dtdesligquita"].ToString());
                        }
                        catch { }
                        _tipo.CPF = dataReader["nrdocto"].ToString();

                        if (Convert.ToDecimal(dataReader["Salario"].ToString()) != 0)
                            _tipo.Salario = Convert.ToDecimal(dataReader["Salario"].ToString());
                        else
                            _tipo.Salario = Convert.ToDecimal(dataReader["SALBASE"].ToString());
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

        public Colaboradores Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Colaboradores _tipo = new Colaboradores();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select f.CodIntFunc, NomeFunc, CodFunc, ChapaFunc, DescArea, f.DTNASCTOFUNC, SituacaoFunc, f.SALBASE, f.DTADMFUNC, d.nrdocto, q.dtdesligquita");
                query.Append("     , c.iddepartamento, c.idcargo, c.Idcolaborador, c.idsuperior, c.Idempresa, c.IdEscolaridade, c.ParticipaAvaliacao, c.sexo, c.Ativo");
                query.Append("     , (Select max(h.salario) From niff_ads_historicocolaborador h Where h.Idcolaborador = c.Idcolaborador) Salario");
                query.Append("  from niff_ads_colaboradores c     ");
                query.Append("     , vw_funcionarios f, flp_documentos d, flp_quitacao q");

                query.Append(" Where c.codintfunc = f.CODINTFUNC ");
                query.Append("   And c.Idcolaborador = " + codigo);
                query.Append("   And d.codintfunc = f.CODINTFUNC(+)");
                query.Append("   And f.CODINTFUNC = q.codintfunc(+)");
                query.Append("   And d.tipodocto = 'CPF'");
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";

                        try
                        {
                            _tipo.IdSupervisor = Convert.ToInt32(dataReader["idsuperior"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdCargo = Convert.ToInt32(dataReader["IdCargo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdDepartamento = Convert.ToInt32(dataReader["IdDepartamento"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEscolaridade = Convert.ToInt32(dataReader["IdEscolaridade"].ToString());
                        }
                        catch { }

                        _tipo.Nome = dataReader["NomeFunc"].ToString();
                        _tipo.Codigo = dataReader["CodFunc"].ToString();
                        _tipo.CodigoInternoFuncionarioGlobus = Convert.ToInt32(dataReader["CodIntFunc"].ToString());
                        _tipo.DataAdmissao = Convert.ToDateTime(dataReader["DTADMFUNC"].ToString());
                        _tipo.DataNascimento = Convert.ToDateTime(dataReader["DTNASCTOFUNC"].ToString());
                        _tipo.ParticipaDaAvaliacao = dataReader["ParticipaAvaliacao"].ToString() == "S";
                        _tipo.Sexo = dataReader["Sexo"].ToString();

                        try
                        {
                            _tipo.DataDesligamento = Convert.ToDateTime(dataReader["dtdesligquita"].ToString());
                        }
                        catch { }
                        _tipo.CPF = dataReader["nrdocto"].ToString();

                        if (Convert.ToDecimal(dataReader["Salario"].ToString()) != 0)
                            _tipo.Salario = Convert.ToDecimal(dataReader["Salario"].ToString());
                        else
                            _tipo.Salario = Convert.ToDecimal(dataReader["SALBASE"].ToString());
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

        public Colaboradores ConsultaSeEhSupervisor(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Colaboradores _tipo = new Colaboradores();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select f.CodIntFunc, NomeFunc, CodFunc, ChapaFunc, DescArea, f.DTNASCTOFUNC, SituacaoFunc, f.SALBASE, f.DTADMFUNC, d.nrdocto, q.dtdesligquita");
                query.Append("     , c.iddepartamento, c.idcargo, c.Idcolaborador, c.idsuperior, c.Idempresa, c.IdEscolaridade, c.ParticipaAvaliacao, c.sexo, c.Ativo");
                query.Append("     , (Select max(h.salario) From niff_ads_historicocolaborador h Where h.Idcolaborador = c.Idcolaborador) Salario");
                query.Append("  from niff_ads_colaboradores c     ");
                query.Append("     , vw_funcionarios f, flp_documentos d, flp_quitacao q");

                query.Append(" Where c.codintfunc = f.CODINTFUNC ");
                query.Append("   And c.idsuperior = " + codigo);
                query.Append("   And d.codintfunc = f.CODINTFUNC(+)");
                query.Append("   And f.CODINTFUNC = q.codintfunc(+)");
                query.Append("   And d.tipodocto = 'CPF'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        try
                        {
                            _tipo.IdSupervisor = Convert.ToInt32(dataReader["idsuperior"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdCargo = Convert.ToInt32(dataReader["IdCargo"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdDepartamento = Convert.ToInt32(dataReader["IdDepartamento"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdEscolaridade = Convert.ToInt32(dataReader["IdEscolaridade"].ToString());
                        }
                        catch { }

                        _tipo.Nome = dataReader["NomeFunc"].ToString();
                        _tipo.Codigo = dataReader["CodFunc"].ToString();
                        _tipo.CodigoInternoFuncionarioGlobus = Convert.ToInt32(dataReader["CodIntFunc"].ToString());
                        _tipo.DataAdmissao = Convert.ToDateTime(dataReader["DTADMFUNC"].ToString());
                        _tipo.DataNascimento = Convert.ToDateTime(dataReader["DTNASCTOFUNC"].ToString());
                        _tipo.ParticipaDaAvaliacao = dataReader["ParticipaAvaliacao"].ToString() == "S";
                        _tipo.Sexo = dataReader["Sexo"].ToString();

                        try
                        {
                            _tipo.DataDesligamento = Convert.ToDateTime(dataReader["dtdesligquita"].ToString());
                        }
                        catch { }
                        _tipo.CPF = dataReader["nrdocto"].ToString();

                        if (Convert.ToDecimal(dataReader["Salario"].ToString()) != 0)
                            _tipo.Salario = Convert.ToDecimal(dataReader["Salario"].ToString());
                        else
                            _tipo.Salario = Convert.ToDecimal(dataReader["SALBASE"].ToString());
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

        public Colaboradores ConsultaColaborador(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            Colaboradores _tipo = new Colaboradores();

            try
            {
                query.Append("Select Nome, c.Idcolaborador, c.sexo, c.Ativo");
                query.Append("  from niff_ads_colaboradores c ");
                query.Append(" Where c.Idcolaborador = " + codigo);
                query.Append("   And c.Ativo = 'S'");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["IdColaborador"].ToString());
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Sexo = dataReader["Sexo"].ToString();
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

        public string EmailDoColaborado(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            string email = "";

            try
            {
                query.Append("Select u.email From Niff_Ads_Colaboradores c, niff_chm_usuarios u");
                query.Append(" Where u.codfunc = c.codintfunc");
                query.Append(" And idcolaborador = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        email = dataReader["Email"].ToString();
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
            return email;
            
        }

        public string EmailAdministradorBiblioteca()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            string email = "";

            try
            {
                query.Append("Select u.email From niff_chm_usuarios u");
                query.Append(" Where u.administrabiblioteca = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        email = email + dataReader["Email"].ToString() + "; ";
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
            return email;

        }

        public bool Gravar(Colaboradores tipo, List<Cursos> cursos, HistoricosDoColaborador historico, 
                          List<CompetenciasDoColaborador> compencias, 
                          List<EmpresaQueOColaboradorEhAvaliado> _empresas,
                          List<DepartamentosGerenciadosPeloColaborador> _departamento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            int id = 0;

            try
            {

                if (!tipo.Existe)
                {
                    query.Clear();
                    query.Append("select SQ_NIFF_ADSIdColaborador.NextVal next from dual");
                    Query executar = sessao.CreateQuery(query.ToString());
                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            id = Convert.ToInt32(dataReader["Next"].ToString());
                    }

                    query.Clear();
                    query.Append("Insert into niff_ads_colaboradores");
                    query.Append(" ( idcolaborador, codintfunc, idempresa, iddepartamento, idcargo, IdEscolaridade, ParticipaAvaliacao, Nome, Sexo, Ativo");
                    if (tipo.IdSupervisor != 0)
                        query.Append(" , idsuperior");

                    query.Append(") Values (" + id.ToString());
                    query.Append("        , " + tipo.CodigoInternoFuncionarioGlobus);
                    query.Append("        ," + tipo.IdEmpresa);
                    query.Append("        ," + tipo.IdDepartamento);
                    query.Append("        ," + tipo.IdCargo);
                    query.Append("        ," + tipo.IdEscolaridade);
                    query.Append("        , '" + (tipo.ParticipaDaAvaliacao ? "S" : "N") + "'");
                    query.Append("        , '" + tipo.Nome + "'");
                    query.Append("        , '" + tipo.Sexo + "'");
                    query.Append("        , '" + (tipo.Ativo ? "S" : "N") + "'");
                    if (tipo.IdSupervisor != 0)
                        query.Append("        ," + tipo.IdSupervisor);
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update niff_ads_colaboradores");
                    query.Append("   set IdDepartamento = " + tipo.IdDepartamento);
                    query.Append("     , idCargo = " + tipo.IdCargo);

                    if (tipo.IdSupervisor != 0)
                        query.Append("     , idsuperior = " + tipo.IdSupervisor);

                    if (tipo.IdSupervisor != 0)
                        query.Append("     , IdEscolaridade = " + tipo.IdEscolaridade);

                    query.Append("        , ParticipaAvaliacao = '" + (tipo.ParticipaDaAvaliacao ? "S" : "N") + "'");
                    query.Append("        , Nome = '" + tipo.Nome + "'");
                    query.Append("        , Sexo = '" + tipo.Sexo + "'");
                    query.Append("        , Ativo = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append(" Where idcolaborador = " + tipo.Id);

                    id = tipo.Id;
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    if (tipo.Existe)
                    {
                        if (!new CursosDAO().Excluir(tipo.Id))
                            retorno = false;

                        if (!new CompetenciasDoColaboradorDAO().Excluir(tipo.Id))
                            retorno = false;
                    }

                    compencias.ForEach(e => e.IdColaborador = id);
                    cursos.ForEach(e => e.IdColaborador = id);
                    _empresas.ForEach(e => e.IdColoborador = id);

                    if (!new CursosDAO().Gravar(cursos))
                        retorno = false;

                    if (!new CompetenciasDoColaboradorDAO().Gravar(compencias))
                        retorno = false;

                    if (!new EmpresaQueOColaboradorEhAvaliadoDAO().Gravar(_empresas))
                        retorno = false;

                    if (historico != null)
                    {
                        historico.IdColaborador = id;
                        if (!new HistoricosDoColaboradorDAO().Gravar(historico))
                            retorno = false;
                    }

                    if (_departamento != null)
                    {
                        _departamento.ForEach(u => u.IdColaborador = id);

                        if (!new DepartamentoDAO().Gravar(_departamento))
                            retorno = false;
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

        public bool Excluir(Colaboradores tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!new HistoricosDoColaboradorDAO().Excluir(tipo.Id))
                    return false;

                if (!new CursosDAO().Excluir(tipo.Id))
                    return false;

                if (!new CompetenciasDoColaboradorDAO().Excluir(tipo.Id))
                    return false;

                if (!new EmpresaQueOColaboradorEhAvaliadoDAO().Excluir(tipo.Id))
                    return false;

                if (!new DepartamentoDAO().Exclui(tipo.Id))
                    return false;

                query.Append("Delete niff_ads_colaboradores");
                query.Append(" Where idcolaborador = " + tipo.Id);
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

        public bool AtualizaStatus()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                query.Append("Update niff_ads_colaboradores");
                query.Append("   Set Ativo = 'N'");
                query.Append(" Where CodIntFunc In (Select CodIntFunc From Vw_Funcionarios f");
                query.Append("                      Where f.CODINTFUNC In(Select codIntFunc");
                query.Append("                       From niff_ads_colaboradores t");
                query.Append("                      Where codIntFunc Is Not Null)");
                query.Append("                        And f.SITUACAOFUNC = 'D')");
                query.Append("   And Ativo = 'S'");

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                query.Clear();
                query.Append("Select Idcolaborador");
                query.Append("  from niff_ads_colaboradores");
                query.Append(" Where CodIntFunc In (Select CodIntFunc From Vw_Funcionarios f");
                query.Append("                      Where f.CODINTFUNC In(Select codIntFunc");
                query.Append("                       From niff_ads_colaboradores t");
                query.Append("                      Where codIntFunc Is Not Null)");
                query.Append("                        And f.SITUACAOFUNC = 'D')");
                query.Append("   And Ativo = 'S'");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        retorno = new RamaisDAO().ExcluirAssociacao(Convert.ToInt32(dataReader["IdColaborador"].ToString()));

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
    }
}

