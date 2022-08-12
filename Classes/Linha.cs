using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Linha
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string Classificacao { get; set; }
        public string DescricaoClassificacao { get; set; }
        public string Empresa { get; set; }
        public bool Existe { get; set; }
    }

    public class SecaoDaLinha
    {
        public int Id { get; set; }
        public int IdLinha { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string NomeExibicao { get; set; }
        public bool Existe { get; set; }
    }
}
