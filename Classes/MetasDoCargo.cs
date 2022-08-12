using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class MetasDoCargo // tabela Niff_ADS_MetasDoCargo
    {
        public int Id { get; set; }
        public int IdMetas { get; set; }
        public int IdCargo { get; set; }
        public bool Marcado { get; set; }
        public string Descricao { get; set; }
        public int Peso { get; set; }
        public bool Existe { get; set; }
    }
}
