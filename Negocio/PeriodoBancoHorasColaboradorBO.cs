using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PeriodoBancoHorasColaboradorBO
    {
        public List<PeriodoBancoHorasColaborador> Listar(bool apenasAtivos)
        {
            return new PeriodoBancoHorasColaboradorDAO().Listar(apenasAtivos);
        }

        public List<PeriodoBancoHorasColaborador> Listar(int idColaborador, bool cadastro = false)
        {
            return new PeriodoBancoHorasColaboradorDAO().Listar(idColaborador, cadastro);
        }

        public PeriodoBancoHorasColaborador Consultar(int codigo, string referencia)
        {
            return new PeriodoBancoHorasColaboradorDAO().Consulta(codigo, referencia);
        }

        public bool Gravar(PeriodoBancoHorasColaborador tipo)
        {
            return new PeriodoBancoHorasColaboradorDAO().Gravar(tipo);
        }

        public bool Excluir(int idPeriodo)
        {
            return new PeriodoBancoHorasColaboradorDAO().Excluir(idPeriodo);
        }
    }
}
