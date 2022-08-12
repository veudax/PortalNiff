using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FeriadoBO
    {
        public Feriado Consultar(DateTime data)
        {
            return new FeriadoDAO().Consulta(data);
        }

        public FeriadoEmenda Consultar(DateTime data, int idEmpresa)
        {
            return new FeriadoDAO().Consulta(data, idEmpresa);
        }

        public List<FeriadoEmenda> Listar(int idEmpresa)
        {
            return new FeriadoDAO().Listar(idEmpresa);
        }

        public bool Gravar(List<FeriadoEmenda> tipo)
        {
            return new FeriadoDAO().Gravar(tipo);
        }

        public bool Excluir(int id, bool todos)
        {
            return new FeriadoDAO().Excluir(id, todos);
        }

        //public int DiasUteis(int idEmpresa, DateTime data, DateTime dataFim)
        //{
        //    return new FeriadoDAO().DiasUteis(idEmpresa, data, dataFim);
        //}

        //public int FolgaComplementar(int idEmpresa, DateTime data, DateTime dataFim)
        //{
        //    return new FeriadoDAO().FolgaComplementar(idEmpresa, data, dataFim);
        //}

        //public int QuantidadeDeFeriados(int idEmpresa, DateTime data, DateTime dataFim)
        //{
        //    return new FeriadoDAO().QuantidadeDeFeriados(idEmpresa, data, dataFim);
        //}

    }
}
