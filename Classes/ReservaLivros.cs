using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ReservaLivros
    {
        public int Id { get; set; }
        public int IdLivro { get; set; }
        public int IdColaborador { get; set; }
        public DateTime DataSolicitado { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public string NomeColaborador { get; set; }
        public string NomeLivro { get; set; }
        public bool Existe { get; set; }
    }
}
