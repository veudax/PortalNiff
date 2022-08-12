using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ChatBO
    {
        
        public List<Chat> BuscarChatNaoLida(int idUsuario)
        {
            return new ChatDAO().BuscaChatNaoLida(idUsuario);
        }

        public List<UsuarioLogado> ConsultarUsuariosLogados()
        {
            return new ChatDAO().ConsultaUsuariosLogados();
        }

        public List<Chat> ConsultarHistorico(int idUsuario, int idUsuarioDestino, bool todas)
        {
            return new ChatDAO().ConsultaHistorico(idUsuario, idUsuarioDestino, todas);
        }

        public bool EnviarChat(Chat chat)
        {
            return new ChatDAO().EnviaChat(chat);
        }

        public bool GravarChatComoLido(int idChat)
        {
            return new ChatDAO().GravaChatComoLido(idChat);
        }

        public bool MarcarChatParaExcluir(int idChat)
        {
            return new ChatDAO().MarcaChatParaExcluir(idChat);
        }
    }
}
