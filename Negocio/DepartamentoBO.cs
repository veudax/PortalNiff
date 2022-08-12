using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DepartamentoBO
    {
        public List<Departamento> ListarDepartamentos()
        {
            return new DepartamentoDAO().ListaDepartamentos();
        }

        public List<DepartamentosGerenciadosPeloColaborador> ListarColaboradores(int idColaborador, int idEmpresa)
        {
            return new DepartamentoDAO().ListarColaboradores(idColaborador, idEmpresa);
        }        

        public Departamento Consultar(int codigo)
        {
            return new DepartamentoDAO().Consulta(codigo);
        }

        public bool Gravar(Departamento departamento)
        {
            return new DepartamentoDAO().Grava(departamento);
        }

        public bool Excluir(Departamento departamento)
        {
            return new DepartamentoDAO().Exclui(departamento);
        }

        public int Proximo()
        {
            return new DepartamentoDAO().Proximo();
        }

        public List<DepartamentosGerenciadosPeloColaborador> Listar(int idColaborador)
        {
            return new DepartamentoDAO().Listar(idColaborador);
        }

        public List<DepartamentosGerenciadosPeloColaborador> ListarIdUsuario(int idUsuario)
        {
            return new DepartamentoDAO().ListarIdUsuario(idUsuario);
        }

        public DepartamentosGerenciadosPeloColaborador ListarIdUsuario(int idUsuario, int idDepartamento)
        {
            return new DepartamentoDAO().ListarIdUsuario(idUsuario, idDepartamento);
        }
    }
}
