using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_Telas*/
    public class Tela
    {
        public int IdTela { get; set; }
        public int IdModulo { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }
        public string NomeCompleto { get; set; }
        public bool Ativo { get; set; }
        public Publicas.TipoDeTela Tipo { get; set; } 
        public bool Existe { get; set; }
    }
}
