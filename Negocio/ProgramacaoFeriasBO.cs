using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProgramacaoFeriasBO
    {
        public List<PeriodoAquisitivo> Listar(int idEmpresa, decimal CodIntFunc)
        {
            return new ProgramacaoFeriasDAO().Listar(idEmpresa, CodIntFunc);
        }

        public List<PeriodoAquisitivo> Listar(int idEmpresa, decimal CodIntFunc, DateTime inicio)
        {
            return new ProgramacaoFeriasDAO().Listar(idEmpresa, CodIntFunc, inicio);
        }

        public bool Gravar(List<PeriodoAquisitivo> lista)
        {
            return new ProgramacaoFeriasDAO().Gravar(lista);
        }

        public bool ExcluirPeriodoAquisitivo(int id)
        {
            return new ProgramacaoFeriasDAO().ExcluirPeriodoAquisitivo(id);
        }

        public bool ExcluirTodosPeriodosAquisitivos(decimal codIntFunc)
        {
            return new ProgramacaoFeriasDAO().ExcluirTodosPeriodosAquisitivos(codIntFunc);
        }

        public List<ProgramacaoFerias> Listar(int idEmpresa, DateTime dataInicio, DateTime dataFim, bool somenteAprovadas, bool somenteNaoGozadas = true)
        {
            return new ProgramacaoFeriasDAO().Listar(idEmpresa, dataInicio, dataFim, somenteAprovadas, somenteNaoGozadas);
        }

        public List<ProgramacaoFerias> Listar(int codIntFunc)
        {
            return new ProgramacaoFeriasDAO().Listar(codIntFunc);
        }

        public List<ProgramacaoFerias> Listar()
        {
            return new ProgramacaoFeriasDAO().Listar();
        }

        public List<ProgramacaoFerias> ListarPeriodoIguaisDeFerias(int codigo, DateTime data, DateTime dataFim, int idEmpresa, int idDepartamento)
        {
            return new ProgramacaoFeriasDAO().ListarPeriodoIguaisDeFerias(codigo, data, dataFim, idEmpresa, idDepartamento);
        }

        public List<ProgramacaoFerias> ListarTodas()
        {
            return new ProgramacaoFeriasDAO().ListarTodas();
        }

        public ProgramacaoFerias Consultar(int codIntFunc, DateTime dataInicio)
        {
            return new ProgramacaoFeriasDAO().Consultar(codIntFunc, dataInicio);
        }

        public ProgramacaoFerias ConsultarInicioFerias(decimal codIntFunc, DateTime data)
        {
            return new ProgramacaoFeriasDAO().ConsultarInicioFerias(codIntFunc, data);
        }

        public bool Gravar(ProgramacaoFerias tipo)
        {
            return new ProgramacaoFeriasDAO().Gravar(tipo);
        }

        public bool GravarComoVisto(int id)
        {
            return new ProgramacaoFeriasDAO().GravarComoVisto(id);
        }

        public bool GravarFeriasGozadas(int id)
        {
            return new ProgramacaoFeriasDAO().GravarFeriasGozadas(id);
        }
        public bool Excluir(int id)
        {
            return new ProgramacaoFeriasDAO().Excluir(id);
        }

        public bool Gravar(List<ProgramacaoFeriasGlobus> lista)
        {
            return new ProgramacaoFeriasDAO().Gravar(lista);
        }
    }
}
