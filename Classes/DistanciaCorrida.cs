using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class DistanciaCorrida // tabela NIFF_CHM_Distancias
    {
        public int Id { get; set; }
        public int IdCorrida { get; set; }
        public int Km { get; set; }
        public bool Existe { get; set; }
    }

}
