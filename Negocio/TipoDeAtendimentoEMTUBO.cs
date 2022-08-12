using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TipoDeAtendimentoEMTUBO
    {
        public List<TipoDeAtendimentoEMTU> Listar()
        {
            return new TipoDeAtendimentoEMTUDAO().Listar();
        }

        public TipoDeAtendimentoEMTU Consultar(string codigo)
        {
            return new TipoDeAtendimentoEMTUDAO().Consulta(codigo);
        }

        public TipoDeAtendimentoEMTU ConsultarPorId(int codigo)
        {
            return new TipoDeAtendimentoEMTUDAO().ConsultaPorId(codigo);
        }


        public bool Gravar(TipoDeAtendimentoEMTU tipo)
        {
            return new TipoDeAtendimentoEMTUDAO().Grava(tipo);
        }

        public bool Excluir(TipoDeAtendimentoEMTU tipo)
        {
            return new TipoDeAtendimentoEMTUDAO().Exclui(tipo);
        }

        public int ProximoId()
        {
            return new TipoDeAtendimentoEMTUDAO().ProximoId();
        }

        public string Proximo(string codigo)
        {
            return new TipoDeAtendimentoEMTUDAO().Proximo(codigo);
        }
    }
}
