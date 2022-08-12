using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControleDeHorasBO
    {
        public List<ControleDeHoras> Listar(int idUsuario, DateTime inicio, DateTime fim, int idEmpresa)
        {
            return new ControleDeHorasDAO().Listar(idUsuario, inicio, fim, idEmpresa);
        }

        public List<ControleDeHoras> Listar(DateTime inicio, DateTime fim, int idGerente)
        {
            return new ControleDeHorasDAO().Listar(inicio, fim, idGerente);
        }

        public bool Gravar(List<ControleDeHoras> horas)
        {
            return new ControleDeHorasDAO().Gravar(horas);
        }

        public bool Excluir(ControleDeHoras tipo)
        {
            return new ControleDeHorasDAO().Excluir(tipo);
        }
    }
}
