using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Livros
    {
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public int IdColaborador { get; set; }
        public string Nome { get; set; }
        public string NomeOriginal { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataDevolucao { get; set; }
        public string TipoCessao { get; set; }
        public string Conservacao { get; set; }
        public string Sinopse { get; set; }
        public Byte[] Imagem { get; set; }
        public bool Ativo { get; set; }
        public bool Existe { get; set; }
        public bool Fisico { get; set; }
        public bool EBook { get; set; }
        public bool AudioBook { get; set; }
        public string NomeArquivo { get; set; }
        public string LocalArmazenamento { get; set; }
        public int QuantidadeDownload { get; set; }
        public Byte[] Arquivo { get; set; }

    }

    public class Leitura
    {
        public int Id { get; set; }
        public int IdLivros { get; set; }
        public int IdColaborador { get; set; }
        public int Pagina { get; set; }
        public int TotalPagina { get; set; }
        public bool Existe { get; set; }
        public DateTime DataDownLoad { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public DateTime DataDevolucao { get; set; }
        public bool EfetuouDownLoad { get; set; }
    }

    public class Ebook
    {
        public int Id { get; set; }
        public int IdLivros { get; set; }
        public Byte[] Arquivo { get; set; }
        public bool Existe { get; set; }
    }
}
