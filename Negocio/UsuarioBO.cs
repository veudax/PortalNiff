using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;
using Dados;

namespace Negocio
{
    public class UsuarioBO
    {
        public List<Usuario> ListarAniversariantesDaSemana(int idEmpresa, int qtdDias, int qtdDiasAntes)
        {
            return new UsuarioDAO().ListaAniversariantesDaSemana(idEmpresa, qtdDias, qtdDiasAntes);
        }

        public Usuario ListaAniversariantesDaEmpresa()
        {
            return new UsuarioDAO().ListaAniversariantesDaEmpresa();
        }

        public List<Usuario> ListarUsuarios(bool apenasAtivos = false, int idEmpresa = 0, bool sac = false, string tipoSAC = "")
        {
            return new UsuarioDAO().ListaUsuario(apenasAtivos, idEmpresa, sac, tipoSAC);
        }

        public List<Usuario> ListarUsuarios(bool apenasAtivos = false)
        {
            return new UsuarioDAO().ListaUsuario(apenasAtivos);
        }

        public Usuario Consultar(string usuario)
        {
            return new UsuarioDAO().ConsultaUsuario(usuario);
        }

        public Usuario ConsultarPorId(int usuario)
        {
            return new UsuarioDAO().ConsultaUsuarioPorID(usuario);
        }

        public Usuario ConsultaUsuarioPorCodigoFuncionarioGlobus(int codIntFunc)
        {
            return new UsuarioDAO().ConsultaUsuarioPorCodigoFuncionarioGlobus(codIntFunc);
        }

        public List<EmpresaDoUsuario> ConsultaEmpresasAutorizadasDoUsuario(int idUsuario)
        {
            return new UsuarioDAO().ConsultaEmpresasAutorizadasDoUsuario(idUsuario);
        }

        public List<EmpresaDoUsuario> ConsultaUsuarioPorEmpresaAutorizada(int idEmpresa)
        {
            return new UsuarioDAO().ConsultaUsuarioPorEmpresaAutorizada(idEmpresa);
        }

        public List<CategoriaDoUsuario> ConsultaCategoriasAutorizadasDoUsuario(int idUsuario)
        {
            return new UsuarioDAO().ConsultaCategoriasAutorizadasDoUsuario(idUsuario);
        }

        public List<ModuloDoUsuario> ConsultaModulosAutorizadasDoUsuario(int idUsuario)
        {
            return new UsuarioDAO().ConsultaModulosAutorizadasDoUsuario(idUsuario);
        }

        public bool TrocarSenha(Usuario usuario)
        {
            return new UsuarioDAO().TrocaSenha(usuario);
        }

        public bool Gravar(Usuario usuario)
        {
            return new UsuarioDAO().Grava(usuario);
        }

        public bool GravarEmpresasAutorizadas(List<EmpresaDoUsuario> lista, int idUsuario)
        {
            return new UsuarioDAO().GravaEmpresas(lista, idUsuario);
        }

        public bool GravarCategoriasAutorizadas(List<CategoriaDoUsuario> lista, int idUsuario)
        {
            return new UsuarioDAO().GravaCategoria(lista, idUsuario);
        }

        public bool GravarModulosAutorizadas(List<ModuloDoUsuario> lista, int idUsuario)
        {
            return new UsuarioDAO().GravaModulo(lista, idUsuario);
        }

        public bool Excluir(int idUsuario)
        {
            return new UsuarioDAO().Exclui(idUsuario);
        }

        public bool ExcluirEmpresasAutorizadas(int codigo)
        {
            return new UsuarioDAO().ExcluiEmpresas(codigo);
        }

        public bool ExcluirCategoriasAutorizadas(int codigo)
        {
            return new UsuarioDAO().ExcluiCategorias(codigo);
        }

        public bool ExcluirModulosAutorizadas(int codigo)
        {
            return new UsuarioDAO().ExcluiModulos(codigo);
        }

        public List<Usuario> ConsultaAtendentesParaACategoria(int idCategoria)
        {
            return new UsuarioDAO().ConsultaAtendentesParaACategoria(idCategoria);
        }

        public void IncluirUsuariosCriadoNoGlobus()
        {
            new UsuarioDAO().IncluiUsuariosCriadoNoGlobus();
        }

        public void DesativaUsuarios()
        {
            new UsuarioDAO().DesativaUsuarios();
        }

        public void AplicarTema()
        {
            new UsuarioDAO().AplicarTema();
        }

        public void FiltroChamado()
        {
            new UsuarioDAO().FiltroChamado();
        }
        

    }
}
