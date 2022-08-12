using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Aniversarios : Usuario // Tabela Niff_CHM_Aniversario
    {
        public int IdAniversario { get; set; }
        public DateTime Data { get; set; }
        public bool MostrarMensagem { get; set; }
        public string Mensagem { get; set; }
        public bool Existe { get; set; }
    }
}
