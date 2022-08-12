using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class PowerBIDAO
    {
        IDataReader dataReader;

        #region Email de Acesso

        public List<PowerBI.EmailDeAcesso> Listar(bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PowerBI.EmailDeAcesso> _lista = new List<PowerBI.EmailDeAcesso>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id_email, email, nomegrupo, nome, senha, data, ativo");
                query.Append("  from Pbi_Emails");

                if (apenasAtivos)
                    query.Append(" Where ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        PowerBI.EmailDeAcesso _tipo = new PowerBI.EmailDeAcesso();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id_email"].ToString());

                        _tipo.Email = dataReader["email"].ToString();
                        _tipo.Nome = dataReader["Nome"].ToString();

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }

                        _tipo.Grupo = dataReader["nomegrupo"].ToString();
                        _tipo.Senha = dataReader["Senha"].ToString();
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
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

        public PowerBI.EmailDeAcesso Consulta(string email)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            PowerBI.EmailDeAcesso _tipo = new PowerBI.EmailDeAcesso();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id_email, email, nomegrupo, nome, senha, data, ativo");
                query.Append("  from Pbi_Emails");
                query.Append(" Where email = '" + email + "'");
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id_email"].ToString());

                        _tipo.Email = dataReader["email"].ToString();
                        _tipo.Nome = dataReader["Nome"].ToString();

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }
                        _tipo.Grupo = dataReader["nomegrupo"].ToString();
                        _tipo.Senha = dataReader["Senha"].ToString();
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
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

        public PowerBI.EmailDeAcesso Consulta(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            PowerBI.EmailDeAcesso _tipo = new PowerBI.EmailDeAcesso();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id_email, email, nomegrupo, nome, senha, data, ativo");
                query.Append("  from Pbi_Emails");
                query.Append(" Where id_email = " + id);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id_email"].ToString());

                        _tipo.Email = dataReader["email"].ToString();
                        _tipo.Nome = dataReader["Nome"].ToString();

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }

                        _tipo.Grupo = dataReader["nomegrupo"].ToString();
                        _tipo.Senha = dataReader["Senha"].ToString();
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
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

        public bool Gravar(PowerBI.EmailDeAcesso tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into Pbi_Emails");
                    query.Append(" ( id_email, email, nomegrupo, nome, senha, data, ativo )");
                    query.Append(" Values ( (Select nvl(max(Id_Email), 0) + 1 next From PBI_Emails)");
                    query.Append("        ,'" + tipo.Email + "'");
                    query.Append("        ,'" + tipo.Grupo + "'");
                    query.Append("        ,'" + tipo.Nome + "'");
                    query.Append("        ,'" + tipo.Senha + "'");
                    query.Append("        , To_date('" + tipo.Data.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append("        ,'" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update PBI_Emails");
                    query.Append("   set Ativo = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("     , NomeGrupo = '" + tipo.Grupo + "'");
                    query.Append("     , Nome = '" + tipo.Nome + "'");
                    query.Append("     , Senha = '" + tipo.Senha + "'");
                    query.Append("     , Data = To_date('" + tipo.Data.ToShortDateString() + "', 'dd/mm/yyyy')");

                    query.Append(" Where id_email = " + tipo.Id);
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

        public bool Excluir(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete PBI_Emails");
                query.Append(" Where id_email = " + id);
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

        #region Empresas Autorizados por E-mail de Acesso
        public List<PowerBI.EmpresasAutorizadas> Listar(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PowerBI.EmpresasAutorizadas> _lista = new List<PowerBI.EmpresasAutorizadas>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select EMPFIL, ID_EMAIL, 'S' selecionado, e.codigoglobus || ' ' || e.nomeabreviado empresa  ");
                query.Append("  from pbi_acessosporempresa a, niff_chm_empresas e ");
                query.Append(" Where ID_EMAIL = " + id);
                query.Append("   and a.empfil = e.codigoglobus");
                query.Append(" Union all ");
                query.Append("Select e.codigoglobus, '0', 'N', e.codigoglobus || ' ' || e.nomeabreviado empresa   ");
                query.Append("  From niff_chm_empresas e ");
                query.Append(" Where ativo = 'S'");
                query.Append("   And e.Codigoglobus Not In(Select Empfil");
                query.Append("                              From Pbi_Acessosporempresa a");
                query.Append("                              Where Id_Email = " + id + ")");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        PowerBI.EmpresasAutorizadas _tipo = new PowerBI.EmpresasAutorizadas();

                        _tipo.Existe = true;

                        _tipo.Selecionado = dataReader["Selecionado"].ToString() == "S";
                        _tipo.SelAnterior = dataReader["Selecionado"].ToString() == "S";
                        _tipo.IdEmail = Convert.ToInt32(dataReader["id_email"].ToString());
                        
                        _tipo.CodigoEmpresa = dataReader["EMPFIL"].ToString();
                        _tipo.Empresa = dataReader["Empresa"].ToString();

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

        public bool Gravar(List<PowerBI.EmpresasAutorizadas> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                foreach (var item in _lista)
                {
                    query.Clear();
                    if (!item.SelAnterior && item.Selecionado)
                    {
                        query.Append("Insert into pbi_acessosporempresa");
                        query.Append(" ( EMPFIL, ID_EMAIL )");
                        query.Append(" Values ( '" + item.CodigoEmpresa + "'");
                        query.Append("        ," + item.IdEmail );
                        query.Append(" )");
                        if (!sessao.ExecuteSqlTransaction(query.ToString()))
                            return false;
                    }
                    else
                    if (item.Existe && !item.Selecionado)
                    {
                        query.Append("Delete pbi_acessosporempresa");
                        query.Append(" Where EMPFIL = '" + item.CodigoEmpresa + "'");
                        query.Append("   and ID_EMAIL = " + item.IdEmail);
                        if (!sessao.ExecuteSqlTransaction(query.ToString()))
                            return false;
                    }
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
        #endregion

        #region Usuarios Autorizados por E-mail de Acesso
        public List<PowerBI.UsuariosAutorizados> ListarUsuarios(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PowerBI.UsuariosAutorizados> _lista = new List<PowerBI.UsuariosAutorizados>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select ue.*, u.Nome");
                query.Append("  from Niff_Pbi_UsuariosPorEmail ue, niff_chm_usuarios u ");
                query.Append(" Where ue.Idusuario = u.Idusuario");
                if (id != 0)
                    query.Append("   and IdEMAIL = " + id);
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        PowerBI.UsuariosAutorizados _tipo = new PowerBI.UsuariosAutorizados();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdEmail = Convert.ToInt32(dataReader["idEmail"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["idUsuario"].ToString());

                        _tipo.Nome = dataReader["Nome"].ToString();

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

        public bool Gravar(List<PowerBI.UsuariosAutorizados> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                foreach (var item in _lista)
                {
                    query.Clear();
                    if (!item.Existe)
                    {
                        query.Append("Insert into Niff_Pbi_UsuariosPorEmail");
                        query.Append(" ( id, idemail, idusuario)");
                        query.Append(" Values ( (Select nvl(max(Id), 0) + 1 next From Niff_Pbi_UsuariosPorEmail)");
                        query.Append("        ," + item.IdEmail);
                        query.Append("        ," + item.IdUsuario);
                        query.Append(" )");

                        if (!sessao.ExecuteSqlTransaction(query.ToString()))
                            return false;
                    }
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

        public bool ExcluirUsuario(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Pbi_UsuariosPorEmail");
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

        public bool ExcluirTodos(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Pbi_UsuariosPorEmail");
                query.Append(" Where idEmail = " + id);
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

        #region Relatorios

        public List<PowerBI.Relatorios> ListarRelatorios(bool ativos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PowerBI.Relatorios> _lista = new List<PowerBI.Relatorios>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, nome, ativo");
                query.Append("  from niff_pbi_Relatorios");

                if (ativos)
                    query.Append(" Where ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        PowerBI.Relatorios _tipo = new PowerBI.Relatorios();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
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

        public PowerBI.Relatorios ConsultaRelatorios(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            PowerBI.Relatorios _tipo = new PowerBI.Relatorios();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, nome, ativo");
                query.Append("  from niff_pbi_Relatorios");
                query.Append(" Where id = " + id);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
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

        public bool Gravar(PowerBI.Relatorios tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into niff_pbi_Relatorios");
                    query.Append(" ( id, nome, ativo )");
                    query.Append(" Values ( " + tipo.Id);
                    query.Append("        ,'" + tipo.Nome + "'");
                    query.Append("        ,'" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update niff_pbi_Relatorios");
                    query.Append("   set Ativo = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("     , Nome = '" + tipo.Nome + "'");
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

        public bool ExcluirRelatorios(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete niff_pbi_Relatorios");
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

                query.Append("Select Nvl(Max(id),0) +1 next From niff_pbi_Relatorios");
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

        #region Quantidade de acessos

        public PowerBI.Acessos ConsultaAcesso(int email, int relatorio, DateTime data)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            PowerBI.Acessos _tipo = new PowerBI.Acessos();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select a.id, idemail, data, quantidade, idrelatorio, r.nome");
                query.Append("  from niff_pbi_Acessos a, niff_pbi_Relatorios r");
                query.Append(" Where idEmail = " + email);
                query.Append("   and r.id = " + relatorio);
                query.Append("   and data = To_Date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   and a.idrelatorio = r.id");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.IdEmail = Convert.ToInt32(dataReader["idemail"].ToString());
                        _tipo.IdRelatorios = Convert.ToInt32(dataReader["idrelatorio"].ToString());
                        _tipo.Quantidade = Convert.ToInt32(dataReader["quantidade"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["data"].ToString());
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

        public List<PowerBI.Acessos> ResumoAcesso(DateTime dataInicio, DateTime dataFim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<PowerBI.Acessos> _lista = new List<PowerBI.Acessos>();

            try
            {
                query.Append("Select Sum(a.quantidade) Quantidade, a.idemail, e.nome NomeGrupo, r.nome, r.Id, Max(a.Data) Data");
                query.Append("  from niff_pbi_Acessos a, niff_pbi_Relatorios r, pbi_emails e");
                query.Append(" Where a.Data between To_Date('" + dataInicio.ToShortDateString() + "','dd/mm/yyyy') and To_Date('" + dataFim.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   and a.idrelatorio = r.id");
                query.Append("   And e.Id_Email = a.Idemail");
                query.Append(" Group By e.nome, r.nome, r.Id,  a.idemail");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        PowerBI.Acessos _tipo = new PowerBI.Acessos();

                        _tipo.Existe = true;

                        _tipo.Nome = dataReader["nome"].ToString();
                        _tipo.NomeEmail = dataReader["NomeGrupo"].ToString();
                        _tipo.IdEmail = Convert.ToInt32(dataReader["idemail"].ToString());
                        _tipo.IdRelatorios = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.Quantidade = Convert.ToInt32(dataReader["quantidade"].ToString());
                        _tipo.Data = Convert.ToDateTime(dataReader["data"].ToString());

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

        public bool Gravar(PowerBI.Acessos tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into niff_pbi_Acessos");
                    query.Append(" (id, idemail, data, quantidade, idrelatorio )");
                    query.Append(" Values (  (Select nvl(max(Id), 0) + 1 next From niff_pbi_Acessos)");
                    query.Append("        ," + tipo.IdEmail);
                    query.Append("        ,to_date('" + tipo.Data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("        ," + tipo.Quantidade);
                    query.Append("        ," + tipo.IdRelatorios);
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update niff_pbi_Acessos");
                    query.Append("   set quantidade = " + tipo.Quantidade);
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

        public bool ExcluirAcessos(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete niff_pbi_Acessos");
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
    }
}
