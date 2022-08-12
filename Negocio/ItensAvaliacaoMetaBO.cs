using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ItensAvaliacaoMetaBO
    {
        //public List<ItensAvaliacaoMetas> Listar(int idCargo, int idColaborador, string referencia)
        //{
        //    return new ItensAvaliacaoMetasDAO().Listar(idCargo, idColaborador, referencia);
        //}

        public bool Gravar(List<ItensAvaliacaoMetas> _lista)
        {
            return new ItensAvaliacaoMetasDAO().Gravar(_lista);
        }

        public bool Excluir(int codigo)
        {
            return new ItensAvaliacaoMetasDAO().Excluir(codigo);
        }

        public decimal CalculoFormula(string formula)
        {
            return new ItensAvaliacaoMetasDAO().CalculoFormula(formula);
        }
    }
}
