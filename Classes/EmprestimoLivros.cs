using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class EmprestimoLivros
    {
        public int Id { get; set; }
        public int IdLivro { get; set; }
        public int IdColaborador { get; set; }
        public int QuantidadeDiasEmprestimo { get; set; }
        public int QuantidadeDiasRenovacao { get; set; }
        public int Pontuacao { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataAcompanhamento { get; set; }
        public DateTime DataDevolucao { get; set; }
        public DateTime DataRenovacao { get; set; }
        public DateTime DevolvidoEm { get; set; }
        public string Conservacao { get; set; }
        public string NomeColaborador { get; set; }
        public string NomeLivro { get; set; }
        public bool Existe { get; set; }
        public bool Devolvido { get; set; }
        public bool Ebook { get; set; }
        public DateTime DataDownLoad { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public int Pagina { get; set; }
        public int TotalPagina { get; set; }

    }
}
