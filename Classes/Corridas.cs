using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Corridas //tabela NIFF_CHM_Corridas
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
        public string LinkWeb { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorGrupo { get; set; }
        public DateTime PrazoLimite { get; set; }
        public int IdUsuario { get; set; }
        public bool Existe { get; set; }
    }
}
