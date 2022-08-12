using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dados;
using Classes;

namespace Negocio
{
    public class EmpresaBO
    {
        public List<Empresa> Listar(bool todas = true)
        {
            return new EmpresaDAO().TrazEmpresa(todas);
        }

        public Empresa Consultar(int codigo)
        {
            return new EmpresaDAO().ConsultaEmpresa(codigo);
        }

        public Empresa ConsultarPeloCodigoGlobus(string codigo)
        {
            return new EmpresaDAO().ConsultaEmpresa(codigo);
        }

        public bool Gravar(Empresa empresa, List<EmailEnvioComunicado> listaEmail)
        {
            return new EmpresaDAO().GravaEmpresa(empresa, listaEmail);
        }

        public bool Excluir(Empresa empresa)
        {
            return new EmpresaDAO().ExcluiEmpresa(empresa);
        }

        public int Proximo()
        {
            return new EmpresaDAO().Proximo();
        }
    }
}
