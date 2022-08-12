using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class EmailEnvioComunicadoDAO
    {
        IDataReader emailReader;

        public List<EmailEnvioComunicado> Listar(int idEmpresa, bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<EmailEnvioComunicado> _lista = new List<EmailEnvioComunicado>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idemail, idempresa, email, ativo, TipoEmail");
                query.Append("  from Niff_Jur_Email");

                if (idEmpresa != 0)
                {
                    query.Append(" Where IdEmpresa = " + idEmpresa);
                    if (apenasAtivos)
                        query.Append("   and Ativo = 'S'");
                }
                else
                {
                    if (apenasAtivos)
                        query.Append(" Where Ativo = 'S'");
                }

                Query executar = sessao.CreateQuery(query.ToString());
                emailReader = executar.ExecuteQuery();

                using (emailReader)
                {
                    while (emailReader.Read())
                    {
                        EmailEnvioComunicado _email = new EmailEnvioComunicado();
                        _email.Existe = true;
                        _email.Ativo = emailReader["Ativo"].ToString() == "S";
                        _email.Email = emailReader["Email"].ToString();
                        _email.Id = Convert.ToInt32(emailReader["IdEmail"].ToString());
                        _email.IdEmpresa = Convert.ToInt32(emailReader["IdEmpresa"].ToString());
                        _email.TipoEmail = (emailReader["TipoEmail"].ToString() == "J" ? Publicas.TipoEmailComunicado.Juridico :
                            (emailReader["TipoEmail"].ToString() == "F" ? Publicas.TipoEmailComunicado.Financeiro : 
                             Publicas.TipoEmailComunicado.Diretoria));
                        _email.DescricaoTipoEmail = Publicas.GetDescription(_email.TipoEmail, "");
                        _lista.Add(_email);
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

        public EmailEnvioComunicado Consulta(int idEmpresa, string email)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            EmailEnvioComunicado _email = new EmailEnvioComunicado();

            try
            {
                query.Append("Select idemail, idempresa, email, ativo");
                query.Append("  from Niff_Jur_Email");

                query.Append(" Where email = '" + email + "'");
                query.Append("   and idEmpresa = " + idEmpresa);

                Query executar = sessao.CreateQuery(query.ToString());
                emailReader = executar.ExecuteQuery();

                using (emailReader)
                {
                    if (emailReader.Read())
                    {
                        _email.Existe = true;
                        _email.Ativo = emailReader["Ativo"].ToString() == "S";
                        _email.Email = emailReader["Email"].ToString();
                        _email.Id = Convert.ToInt32(emailReader["IdEmail"].ToString());
                        _email.IdEmpresa = Convert.ToInt32(emailReader["IdEmpresa"].ToString());
                        _email.TipoEmail = (emailReader["TipoEmail"].ToString() == "J" ? Publicas.TipoEmailComunicado.Juridico :
                            (emailReader["TipoEmail"].ToString() == "F" ? Publicas.TipoEmailComunicado.Financeiro :
                             Publicas.TipoEmailComunicado.Diretoria));
                        _email.DescricaoTipoEmail = Publicas.GetDescription(_email.TipoEmail, "");
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
            return _email;
        }

        public bool Gravar(List<EmailEnvioComunicado> email)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int Id = 1;
            bool retorno = true ;
            try
            {
                foreach (var item in email)
                {
                    if (!item.Existe)
                    {
                        query.Clear();
                        query.Append("Select SQ_NIFF_JurIdEmail.NextVal next from dual");

                        Query executar = sessao.CreateQuery(query.ToString());
                        emailReader = executar.ExecuteQuery();

                        using (emailReader)
                        {
                            if (emailReader.Read())
                                Id = Convert.ToInt32(emailReader["Next"].ToString());
                        }

                        query.Clear();
                        query.Append("Insert into Niff_Jur_Email");
                        query.Append(" (idemail, idempresa, email, ativo, TipoEmail)");
                        query.Append(" Values(" + Id);
                        query.Append("       ," + item.IdEmpresa);
                        query.Append("       ,'" + item.Email + "'");
                        query.Append("       ,'" + (item.Ativo ? "S" : "N") + "'");
                        query.Append("       ,'" + (item.TipoEmail == Publicas.TipoEmailComunicado.Diretoria ? "D" :
                            (item.TipoEmail == Publicas.TipoEmailComunicado.Financeiro ? "F" : "J")) + "'");
                        query.Append(")");
                    }
                    else
                    {
                        query.Clear();
                        if (!item.Excluido)
                        {
                            query.Append("Update Niff_Jur_Email");
                            query.Append("   set Ativo = '" + (item.Ativo ? "S" : "N") + "'");
                            query.Append("     , Email = '" + item.Email + "'");
                            query.Append("     , TipoEmail = '" + (item.TipoEmail == Publicas.TipoEmailComunicado.Diretoria ? "D" :
                            (item.TipoEmail == Publicas.TipoEmailComunicado.Financeiro ? "F" : "J")) + "'");
                            query.Append(" Where IdEmail = " + item.Id);
                        }
                        else
                        {
                            query.Append("Delete Niff_Jur_Email");
                            query.Append(" Where IdEmail = " + item.Id);
                        }
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

        public bool Excluir(int IdEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Jur_Email");
                query.Append(" Where IdEmpresa = " + IdEmpresa);
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
