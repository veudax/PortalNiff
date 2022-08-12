using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ResenhaLivros //niff_bib_resenha
    {
        public int Id { get; set; }
        public int IdLivros { get; set; }
        public int IdColaborador { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataLiberacao { get; set; }
        public string Resenha { get; set; }
        public string Sinopse { get; set; }
        public bool Ativo { get; set; }
        public string NomeLivro { get; set; }
        public string NomeColaborador { get; set; }
        public int Classificacao { get; set; }
        public int Pontuacao { get; set; }
        public int PontoEmprestimo { get; set; }
        public int PontoResenha { get; set; }
        public int PontoPergunta { get; set; }
        public int PontoResposta { get; set; }
        public int IdUsuario { get; set; }
        public Byte[] Imagem { get; set; }
        public Byte[] Arquivo { get; set; }
        public string LocalArmazenamento { get; set; }
        public int QuantidadeDownload { get; set; }
        public bool EBook { get; set; }
        public bool Fisico { get; set; }
        public bool Existe { get; set; }
        public string NomeArquivo { get; set; }
        public bool Lendo { get; set; }
        public bool TemPerguntas { get; set; }
        public bool TemResenha { get; set; }
    }

    public class PerguntasLivros // niff_bib_Perguntas
    {
        public int Id { get; set; }
        public int IdResenha { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public bool Existe { get; set; }
    }

    public class RespostasLivros // niff_bib_Respostas
    {
        public int Id { get; set; }
        public int IdPergunta { get; set; }
        public int IdColaborador { get; set; }
        public int Pontuacao { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataAprovacao { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public string RespostaOriginal { get; set; }
        public bool Certa { get; set; }
        public bool TemPerguntasSemResposta { get; set; }
        public bool Aprovada { get; set; }
        public int IdUsuario { get; set; }
        public bool Existe { get; set; }
        public string NomeLivro { get; set; }
        public string NomeColaborador { get; set; }
    }
}
