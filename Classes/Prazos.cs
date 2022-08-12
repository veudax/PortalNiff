using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Prazos
    {
        public int Id { get; set; }
        public string Referencia { get; set; }
        public Publicas.TipoPrazos Tipo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public string DescricaoTipo { get; set; }
        public string EnvioEmail { get; set; }
        public bool Ativo { get; set; }
        public bool Existe { get; set; }
    }
}
