using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PrazosBO
    {
        public List<Prazos> Listar(bool apenasAtivos = false, bool apenasReferencia = false, bool apenasAno = false)
        {
            return new PrazosDAO().Listar(apenasAtivos, apenasReferencia, apenasAno);
        }

        public Prazos Consultar(int refencia, Publicas.TipoPrazos tipo, int codigo = 0)
        {
            return new PrazosDAO().Consulta(refencia, tipo, codigo);
        }

        public Prazos Consultar(DateTime data, string tipo, string referencia = "")
        {
            return new PrazosDAO().Consulta(data, tipo, referencia);
        }

        public Prazos ConsultarCicloAvaliacao(int referencia)
        {
            return new PrazosDAO().ConsultaCicloAvaliacao(referencia);
        }

        public bool Gravar(Prazos tipo)
        {
            return new PrazosDAO().Gravar(tipo);
        }

        public bool Excluir(Prazos tipo)
        {
            return new PrazosDAO().Excluir(tipo);
        }
    }
}
