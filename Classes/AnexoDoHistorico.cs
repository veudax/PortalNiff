using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /*tabela NIFF_CHM_AnexosHistorico*/
    public class AnexoDoHistorico
    {
        public int IdHistorico { get; set; }
        public int IdAnexo { get; set; }
        public byte[] Anexo { get; set; }
        public string NomeArquivo { get; set; }
        public bool Existe { get; set; }
    }
}
