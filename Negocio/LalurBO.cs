using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LalurBO
    {
        public List<Lalur.Formulas> Listar(int idEmpresa)
        {
            return new LalurDAO().Listar(idEmpresa);
        }

        public Lalur.Formulas Consultar(int idEmpresa, int codigo)
        {
            return new LalurDAO().Consultar(idEmpresa, codigo);
        }

        public Lalur.Formulas Consultar(int id)
        {
            return new LalurDAO().Consultar(id);
        }

        public bool Gravar(Lalur.Formulas item, List<Lalur.ContasDaFormula> contas)
        {
            return new LalurDAO().Gravar(item, contas);
        }

        public bool Excluir(int id)
        {
            return new LalurDAO().Excluir(id);
        }

        public int Proximo(int idEmpresa)
        {
            return new LalurDAO().Proximo(idEmpresa);
        }

        public List<Lalur.ContasDaFormula> ListarContas(int id)
        {
            return new LalurDAO().ListarContas(id);
        }

        public bool ExcluirConta(int id)
        {
            return new LalurDAO().ExcluirConta(id);
        }

        public Lalur.Parametros ConsultarParametros(int idEmpresa)
        {
            return new LalurDAO().ConsultarParametros(idEmpresa);
        }

        public bool Gravar(Lalur.Parametros item)
        {
            return new LalurDAO().Gravar(item);
        }

        public bool ExcluirParametros(int id)
        {
            return new LalurDAO().ExcluirParametros(id);
        }

        public Lalur.Apuracao Consultar(int idEmpresa, string referencia)
        {
            return new LalurDAO().Consultar(idEmpresa, referencia);
        }

        public List<Lalur.Valores> ListarApuracao(int id)
        {
            return new LalurDAO().ListarApuracao(id);
        }

        public decimal MesAnterior(int idFormula, string referencia)
        {
            return new LalurDAO().MesAnterior(idFormula, referencia);
        }

        public bool Gravar(Lalur.Apuracao item, List<Lalur.Valores> valores)
        {
            return new LalurDAO().Gravar(item, valores);
        }

        public bool ExcluirApuracao(int id)
        {
            return new LalurDAO().ExcluirApuracao(id);
        }

        public decimal ValoresContabeis(string Empresa, string referencia, string referenciaFim, int IdFormula, int idEmpresa, bool consolidar)
        {
            return new LalurDAO().ValoresContabeis(Empresa, referencia, referenciaFim, IdFormula, idEmpresa, consolidar);
        }

        public List<Lalur.ValoresContas> ValoresContabeisDetalhado(int id, string Empresa, string referencia, string referenciaFim, int IdFormula, int idEmpresa, bool consolidar)
        {
            return new LalurDAO().ValoresContabeisDetalhado(id, Empresa, referencia, referenciaFim, IdFormula, idEmpresa, consolidar);
        }
    }
}
