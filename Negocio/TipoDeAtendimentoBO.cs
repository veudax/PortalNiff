using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TipoDeAtendimentoBO
    {
        public List<TipoDeAtendimento> Listar()
        {
            return new TipoDeAtendimentoDAO().Listar();
        }

        public TipoDeAtendimento Consultar(int codigo)
        {
            return new TipoDeAtendimentoDAO().Consulta(codigo);
        }

        public bool Gravar(TipoDeAtendimento tipo)
        {
            return new TipoDeAtendimentoDAO().Grava(tipo);
        }

        public bool Excluir(TipoDeAtendimento tipo)
        {
            return new TipoDeAtendimentoDAO().Exclui(tipo);
        }

        public int Proximo()
        {
            return new TipoDeAtendimentoDAO().Proximo();
        }
    }
}
