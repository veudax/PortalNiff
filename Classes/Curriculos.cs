using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /*link maps endereço 
     * https://www.google.com/maps/place/Empresa+de+%C3%94nibus+Vila+Galv%C3%A3o/@-23.4806866,-46.5333115,17z/data=!4m5!3m4!1s0x94ce5ff3c3b76d27:0x38014bae22872254!8m2!3d-23.4810261!4d-46.5305542
    */

    public class Curriculos
    {
        public int IdCandidato { get; set; }
        public int IdVagas { get; set; }
        public string NomeCandidato { get; set; }
        public string NomeVaga { get; set; }
        public bool PreSelecionado { get; set; }
        public bool AprovadoGestor { get; set; }
        public bool Contato { get; set; }
        public bool SemContato { get; set; }
        public bool Aprovado { get; set; }
        public bool ReprovadoGestor { get; set; }
        public bool Reprovado { get; set; }
        public bool Cancelar { get; set; }
        public string Data1Entrevista { get; set; }
        public string Data2Entrevista { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public DateTime DataPrimeiraEntrevista { get; set; }
        public DateTime DataSegundaEntrevista { get; set; }
        public bool EnviarEmail { get; set; }
        public string Status { get; set; }
        public bool CV { get; set; }
        public bool PorSkype { get; set; }
        public string Skype { get; set; }
        public Byte[] CVArquivo { get; set; }
        public string MotivoContato { get; set; }
        public string MotivoSemContato { get; set; }
        public string MotivoAprovado { get; set; }
        public string MotivoAprovadoGestor { get; set; }
        public string MotivoReprovadoGestor { get; set; }
        public string MotivoReprovado { get; set; }
        public string MotivoCancelar { get; set; }

        public List<HistoricoDoCandidato> Historico { get; set; }
    }

    public class Candidatos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Telefone { get; set; }
        public decimal Celular { get; set; }
        public string TelefoneFormatado { get; set; }
        public string CelularFormatado { get; set; }
        public string Email { get; set; }
        public bool Indicado { get; set; }
        public bool Catho { get; set; }
        public bool Infojobs { get; set; }
        public bool LinkedIn { get; set; }
        public bool Outros { get; set; }
        public string DescricaoOutros { get; set; }
        public bool Contratado { get; set; }
        public bool Existe { get; set; }
    }

    public class Vagas
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Abertura { get; set; }
        public bool Confidencial { get; set; }
        public bool Indicado { get; set; }
        public bool Catho { get; set; }
        public bool Infojobs { get; set; }
        public bool LinkedIn { get; set; }
        public bool Outros { get; set; }
        public string DescricaoOutros { get; set; }
        public string Detalhamento { get; set; }
        public DateTime Encerramento { get; set; }
        public int IdEmpresa { get; set; }
        public string NomeEmpresa { get; set; }
        public int IdCandidato { get; set; }
        public string Status { get; set; }
        public string DescricaoStatus { get; set; }
        public int IdEmpresaEntrevista { get; set; }
        public string EnderecoEntevista { get; set; }
        public string InformacoesGerais { get; set; }
        public bool Existe { get; set; }
    }

    public class ArquivosDoCandidato : Candidatos
    {
        public int IdCandidato { get; set; }
        public string Tipo { get; set; }
        public string Extensao { get; set; }
        public Byte[] Arquivo { get; set; }
    }

    public class CandidatosDaVaga : Candidatos
    {
        public int IdVaga { get; set; }
        public int IdCandidato { get; set; }
        public DateTime Data { get; set; }
        public int IdEmpresa { get; set; }
        public Byte[] CVArquivo { get; set; }

    }

    public class HistoricoDoCandidato 
    {
        public int Id { get; set; }
        public int IdVaga { get; set; }
        public int IdCandidato { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataEntrevista { get; set; }
        public string Entrevista { get; set; }
        public string Status { get; set; }
        public string DescricaoStatus { get; set; }
        public string DescricaoVaga { get; set; }
        public string Motivo { get; set; }
        public bool Existe { get; set; }
    }
}
