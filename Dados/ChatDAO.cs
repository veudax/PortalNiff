using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ChatDAO
    {
        IDataReader chatReader;
        int _idChat;

        public List<Chat> BuscaChatNaoLida(int idUsuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Chat> _lista = new List<Chat>();
            Chat _chat;
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idChat ");
                query.Append("     , IDUSUARIOORIGEM");
                query.Append("     , IDUSUARIODESTINO");
                query.Append("     , MENSAGEM");
                query.Append("     , EXCLUIDA");
                query.Append("     , DATAEXCLUSAO");
                query.Append("     , Lida");
                query.Append("     , DATA dataHora");
                query.Append("     , Trunc(DATA) data");
                query.Append("     , 'S' Enviadas");
                query.Append("     , U.NOME ");

                query.Append("  From niff_chm_chat c, niff_CHM_usuarios u");
                query.Append(" Where IDUSUARIODESTINO = " + Publicas._idUsuario.ToString());
                query.Append("   and Excluida = 'N'");
                query.Append("   and u.IdUsuario = IDUSUARIOORIGEM ");

                query.Append("   and Lida = 'N'");

                query.Append("   Order By Data");
                Query executar = sessao.CreateQuery(query.ToString());

                chatReader = executar.ExecuteQuery();

                using (chatReader)
                {
                    while (chatReader.Read())
                    {
                        _chat = new Chat();

                        _chat.IdChat = Convert.ToInt32(chatReader["idChat"].ToString());
                        _chat.IdUsuarioDestino = Convert.ToInt32(chatReader["IDUSUARIODESTINO"].ToString());
                        _chat.IdUsuarioOrigem = Convert.ToInt32(chatReader["IDUSUARIOORIGEM"].ToString());
                        _chat.Mensagem = chatReader["mensagem"].ToString();
                        _chat.Lida = chatReader["Lida"].ToString() == "S";
                        _chat.Data = Convert.ToDateTime(chatReader["DATA"].ToString());
                        _chat.DataHora = Convert.ToDateTime(chatReader["dataHora"].ToString());
                        _chat.Enviada = chatReader["Enviadas"].ToString() == "S";
                        _chat.NomeRecebida = chatReader["Nome"].ToString();

                        _lista.Add(_chat);
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

        public List<Chat> ConsultaHistorico(int idUsuario, int idUsuarioDestino, bool todas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Chat> _lista = new List<Chat>();
            Chat _chat;
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idChat ");
                query.Append("     , IDUSUARIOORIGEM");
                query.Append("     , IDUSUARIODESTINO");
                query.Append("     , MENSAGEM");
                query.Append("     , EXCLUIDA");
                query.Append("     , DATAEXCLUSAO");
                query.Append("     , Lida");
                query.Append("     , DATA dataHora");
                query.Append("     , Trunc(DATA) data");
                query.Append("     , 'S' Enviadas");
                query.Append("     , u.Nome");

                query.Append("  From niff_chm_chat c, niff_CHM_usuarios u");
                query.Append(" Where IDUSUARIOORIGEM = " + idUsuario.ToString());
                query.Append("   and IDUSUARIODESTINO = " + idUsuarioDestino.ToString());
                query.Append("   and Excluida = 'N'");
                query.Append("   and u.IdUsuario = IDUSUARIODESTINO ");

                if (!todas)
                    query.Append("   and Lida = 'N'");

                query.Append(" Union ALL ");
                query.Append("Select idChat ");
                query.Append("     , IDUSUARIOORIGEM");
                query.Append("     , IDUSUARIODESTINO");
                query.Append("     , MENSAGEM");
                query.Append("     , EXCLUIDA");
                query.Append("     , DATAEXCLUSAO");
                query.Append("     , Lida");
                query.Append("     , DATA dataHora");
                query.Append("     , Trunc(DATA) data");
                query.Append("     , 'N' Enviadas");
                query.Append("     , u.Nome");

                query.Append("  From niff_chm_chat c, niff_CHM_usuarios u ");
                query.Append(" Where IDUSUARIOORIGEM = " + idUsuarioDestino.ToString());
                query.Append("   and IDUSUARIODESTINO = " + idUsuario.ToString());
                query.Append("   and Excluida = 'N'");
                query.Append("   and u.IdUsuario = IDUSUARIOORIGEM ");

                if (!todas)
                    query.Append("   and Lida = 'N'");

                query.Append("   Order By Data");
                Query executar = sessao.CreateQuery(query.ToString());

                chatReader = executar.ExecuteQuery();

                using (chatReader)
                {
                    while (chatReader.Read())
                    {
                        _chat = new Chat();

                        _chat.IdChat = Convert.ToInt32(chatReader["idChat"].ToString());
                        _chat.IdUsuarioDestino = Convert.ToInt32(chatReader["IDUSUARIODESTINO"].ToString());
                        _chat.IdUsuarioOrigem = Convert.ToInt32(chatReader["IDUSUARIOORIGEM"].ToString());
                        _chat.Mensagem = chatReader["mensagem"].ToString();
                        _chat.Lida = chatReader["Lida"].ToString() == "S";
                        _chat.Data = Convert.ToDateTime(chatReader["DATA"].ToString());
                        _chat.DataHora = Convert.ToDateTime(chatReader["dataHora"].ToString());
                        _chat.Enviada = chatReader["Enviadas"].ToString() == "S";
                        _chat.NomeEnviada = chatReader["nome"].ToString();
                        _chat.NomeRecebida = chatReader["nome"].ToString();

                        _lista.Add(_chat);
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

        public List<UsuarioLogado> ConsultaUsuariosLogados()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            UsuarioLogado _chat;
            Publicas.mensagemDeErro = string.Empty;
            List<UsuarioLogado> _lista = new List<UsuarioLogado>();
            try
            {
                query.Append("Select u.IDUSUARIO ");
                query.Append("     , l.DATALOGADO");
                query.Append("     , l.STATUS");
                query.Append("     , u.Nome || decode(u.ramal, 0, '', ' ( ramal ' || u.Ramal || ' )') Nome");
                query.Append("     , u.foto");
                query.Append("     , u.Ramal");
                query.Append("     , d.descricao");
                query.Append("     , e.NomeAbreviado Empresa");

                query.Append("  From NIFF_CHM_UsuarioLogado l ");
                query.Append("     , NIFF_CHM_Usuarios u");
                query.Append("     , niff_chm_empresas e");
                query.Append("     , niff_chm_departamento d");
                query.Append(" Where u.IdUsuario = l.IdUsuario(+) ");
                query.Append("   and u.IdEmpresa(+) = e.idEmpresa ");
                query.Append("   and u.IdDepartamento = d.IdDepartamento(+)");
                query.Append("   and u.Ativo = 'S'");
                query.Append("   and u.IdUsuario <> " + Publicas._idUsuario);

                Query executar = sessao.CreateQuery(query.ToString());

                chatReader = executar.ExecuteQuery();

                using (chatReader)
                {
                    while (chatReader.Read())
                    {
                        _chat = new UsuarioLogado();

                        _chat.Id = Convert.ToInt32(chatReader["IDUSUARIO"].ToString());
                        _chat.Nome = chatReader["Nome"].ToString();
                        _chat.Empresa = chatReader["Empresa"].ToString();
                        _chat.Departamento = chatReader["Descricao"].ToString();

                        try
                        {
                            _chat.DataLogado = Convert.ToDateTime(chatReader["DATALOGADO"].ToString());
                        }
                        catch { }


                        try
                        {
                            _chat.Ramal = Convert.ToInt32(chatReader["Ramal"].ToString());
                        }
                        catch { }


                        try
                        {
                            _chat.Foto = (byte[])(chatReader["foto"]);
                        }
                        catch
                        { }

                        switch (chatReader["STATUS"].ToString())
                        {
                            case "O": _chat.Status = Publicas.StatusUsuario.OnLine;
                                break;
                            case "F":
                                _chat.Status = Publicas.StatusUsuario.OffLine;
                                break;
                            case "A":
                                _chat.Status = Publicas.StatusUsuario.Ausente;
                                break;
                            case "B":
                                _chat.Status = Publicas.StatusUsuario.Ocupado;
                                break;
                            default: _chat.Status = Publicas.StatusUsuario.OffLine;
                                break;
                        }

                        _lista.Add(_chat);
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

        public bool EnviaChat(Chat _chat)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Select SQ_NIFF_IDChat.Nextval next from dual");

                Query executar = sessao.CreateQuery(query.ToString());

                chatReader = executar.ExecuteQuery();

                using (chatReader)
                {
                    if (chatReader.Read())
                    {
                        _idChat = Convert.ToInt32(chatReader["next"].ToString());
                    }
                }

                query.Clear();
                query.Append("Insert into Niff_Chm_Chat");
                query.Append("  (idchat, idusuarioorigem, idusuariodestino, mensagem, data, lida)");
                query.Append(" Values (" + _idChat + ", " + _chat.IdUsuarioOrigem + ", " ) ;
                query.Append(_chat.IdUsuarioDestino + ", '" + _chat.Mensagem + "', sysdate, ");
                query.Append("'N' )");

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

        public bool GravaChatComoLido(int idChat)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
               
                query.Clear();
                query.Append("Update Niff_Chm_Chat");
                query.Append("   set Lida = 'S'");
                query.Append(" Where IDCHAT = " + idChat);

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

        public bool MarcaChatParaExcluir(int idChat)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Clear();
                query.Append("Update Niff_Chm_Chat");
                query.Append("   set EXCLUIDA = 'S'");
                query.Append("     , DatExclusao = sysDate");
                query.Append(" Where IDCHAT = " + idChat);

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
