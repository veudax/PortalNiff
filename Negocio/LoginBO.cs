using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;
using Dados;

namespace Negocio
{
    public class LoginBO
    {
        public Publicas.ValidacaoUsuario ValidarUsuario(string usuario, string senha, string conexao)
        {
            Publicas.stringConexao = conexao;

            Publicas.ValidacaoUsuario retorno = Publicas.ValidacaoUsuario.UsuarioOk;

            Usuario _usuario = new UsuarioDAO().ConsultaUsuario(usuario);

            if (_usuario == null)
            {
                if (Publicas.mensagemDeErro.Contains("Problemas ao conectar"))
                    retorno = Publicas.ValidacaoUsuario.ProblemaAoConectar;
                else
                if (string.IsNullOrEmpty(Publicas.mensagemDeErro))
                    retorno = Publicas.ValidacaoUsuario.UsuarioNaoCadastrado;
                else
                    retorno = Publicas.ValidacaoUsuario.ErroConsulta;
            }
            else
            {
                if (!_usuario.Existe)
                    retorno = Publicas.ValidacaoUsuario.UsuarioNaoCadastrado;
                else
                {
                    if (!string.IsNullOrEmpty(senha) && (_usuario.Senha != senha))
                        retorno = Publicas.ValidacaoUsuario.SenhaInvalida;

                    Publicas._idUsuario = _usuario.Id;
                    Publicas._usuariologado = _usuario.Nome;

                    if (!_usuario.Ativo)
                        retorno = Publicas.ValidacaoUsuario.UsuarioInativo;
                }
            }

            Publicas._usuario = _usuario;
            
            return retorno;
        }

        public bool AlterarStatusUsuario(int idUsuario, Publicas.StatusUsuario status, string conexao)
        {
            return new UsuarioDAO().AlteraStatusUsuario(idUsuario, status);
        }
    }
}
