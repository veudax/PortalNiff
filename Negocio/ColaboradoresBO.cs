using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ColaboradoresBO
    {
        public List<Colaboradores> Listar(string tipoDeCargo)
        {
            return new ColaboradoresDAO().ListarEmailPorTipoCargo(tipoDeCargo);
        }

        public List<Colaboradores> Listar(bool apenasAtivos)
        {
            return new ColaboradoresDAO().Listar(apenasAtivos);
        }
        
        public List<Colaboradores> ListarColaboradoresBolao(int IdEmpresa)
        {
            return new ColaboradoresDAO().ListarColaboradoresBolao(IdEmpresa);
        }

        public List<Colaboradores> ListarColaboradoresParticipantes(int IdEmpresa, int Campanha, bool Participa)
        {
            return new ColaboradoresDAO().ListarColaboradoresParticipantes(IdEmpresa, Campanha, Participa);
        }

        public List<EmpresaQueOColaboradorEhAvaliado> Listar(int Colaborador)
        {
            return new EmpresaQueOColaboradorEhAvaliadoDAO().Listar(Colaborador);
        }

        public EmpresaQueOColaboradorEhAvaliado Consultar(int Colaborador, int empresa)
        {
            return new EmpresaQueOColaboradorEhAvaliadoDAO().Consultar(Colaborador, empresa);
        }

        public Colaboradores Consultar(string empresa, string codigo, bool filtrarEmpresa)
        {
            return new ColaboradoresDAO().Consulta(empresa, codigo, filtrarEmpresa);
        }

        public Colaboradores Consultar(int codigo)
        {
            return new ColaboradoresDAO().Consulta(codigo);
        }

        public Colaboradores ConsultaColaborador(int codigo)
        {
            return new ColaboradoresDAO().ConsultaColaborador(codigo);
        }

        public Colaboradores ConsultaSeEhSupervisor(int codigo)
        {
            return new ColaboradoresDAO().ConsultaSeEhSupervisor(codigo);
        }

        public string EmailDoColaborado(int codigo)
        {
            return new ColaboradoresDAO().EmailDoColaborado(codigo);
        }

        public string EmailAdministradorBiblioteca()
        {
            return new ColaboradoresDAO().EmailAdministradorBiblioteca();
        }

        public bool Gravar(Colaboradores tipo, List<Cursos> cursos, HistoricosDoColaborador historico, 
                           List<CompetenciasDoColaborador> compencias, List<EmpresaQueOColaboradorEhAvaliado> empresas,
                           List<DepartamentosGerenciadosPeloColaborador> departamentos)
        {
            return new ColaboradoresDAO().Gravar(tipo, cursos, historico, compencias, empresas, departamentos);
        }

        public bool Excluir(Colaboradores tipo)
        {
            return new ColaboradoresDAO().Excluir(tipo);
        }

        public bool AtualizaStatus()
        {
            return new ColaboradoresDAO().AtualizaStatus();
        }
    }
}
