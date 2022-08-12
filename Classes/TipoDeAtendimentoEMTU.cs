using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class TipoDeAtendimentoEMTU
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int IdDepartamento { get; set; }
        public int IdTipoAtendimento { get; set; }
        public string Departamento { get; set; }
        public string TipoAtendimento { get; set; }
        public bool Ativo { get; set; }
        public bool Existe { get; set; }
    }
}
