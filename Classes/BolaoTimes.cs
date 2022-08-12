using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class BolaoTimes // Niff_bol_Times
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public byte[] Bandeira { get; set; }
        public int Ano { get; set; }
        public string Grupo { get; set; }
        public string Sigla { get; set; }
        public bool Existe { get; set; }
    }
}
