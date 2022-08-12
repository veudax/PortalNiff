using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class TipoDeAtendimento
    {
        public int IdTipoAtendimento { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public bool Existe { get; set; }
    }
}
