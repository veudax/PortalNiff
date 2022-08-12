using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TipoDePagamentoBO
    {
        public List<TipoDePagamento> Listar(bool apenasAtivos = false)
        {
            return new TipoDePagamentoDAO().Listar(apenasAtivos);
        }

        public TipoDePagamento Consultar(int codigo)
        {
            return new TipoDePagamentoDAO().Consulta(codigo);
        }

        public int Proximo()
        {
            return new TipoDePagamentoDAO().Proximo();
        }

        public bool Gravar(TipoDePagamento tipo)
        {
            return new TipoDePagamentoDAO().Gravar(tipo);
        }

        public bool Excluir(TipoDePagamento tipo)
        {
            return new TipoDePagamentoDAO().Excluir(tipo);
        }
    }
}
