using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_ChatAnexos*/
    public class AnexoDoChat
    {
        public int IdChat { get; set; }
        public int IdAnexo { get; set; }
        public byte[] Anexo { get; set; }
    }
}
