using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Atendimento
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int IdUsuario { get; set; }
        public int IdUsuarioResponsavel { get; set; }
        public int IdUsuarioFinaliza { get; set; }
        public int IdUsuarioCancela { get; set; }
        public int IdUsuarioRetorno { get; set; }
        public int IdTipoAtendimento { get; set; }
        public bool ClienteAnonimo { get; set; }
        public string TextoAtendimento { get; set; }
        public string TextoResposta { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataResposta { get; set; }
        public DateTime DataFinalizado { get; set; }
        public DateTime DataCancelado { get; set; }
        public Publicas.StatusAtendimento Status { get; set; }
        public Publicas.SituacaoAtendimento Situacao { get; set; }
        public Publicas.OpcaoDeRetornoAtendimento OpcoesDeRetorno { get; set; }
        public Publicas.OrigemAtendimento Origem { get; set; }
        public Publicas.TipoDeSatisfacaoAtendimento Satisfacao { get; set; }
        public bool AguardaSatisfacaoCliente { get; set; }
        public bool Retornou { get; set; }
        public string MotivoRetorno { get; set; }
        public string MotivoCancelamento { get; set; }
        public string MotivoSatisfacao { get; set; }
        public string CodigoLinha { get; set; }
        public int IdEmtu { get; set; }
        public string NomeCliente { get; set; }
        public decimal CPFCliente { get; set; }
        public string RGCliente { get; set; }
        public string EnderecoCliente { get; set; }
        public string CidadeCliente { get; set; }
        public string UFCliente { get; set; }
        public string EmailCliente { get; set; }
        public decimal TelefoneCliente { get; set; }
        public decimal Celular { get; set; }
        public string CodigoEmtu { get; set; }
        public string DescricaoEmtu { get; set; }
        public string TextoRetornoAoCliente { get; set; }
        public int IdUsuarioRetornoAoCliente { get; set; }
        public DateTime DataRetornoAoCliente { get; set; }

        public string DescricaoDaSituacao { get; set; }
        public string DescricaoDaOrigem { get; set; }
        public string ResponsavelPelaAbertura { get; set; } //nome
        public string NomeDoUsuarioUltimoStatus { get; set; }
        public DateTime SLA { get; set; }
        public DateTime SLAAtendente { get; set; }
        public bool Existe { get; set; }
        public string TelefoneFormatado { get; set; }
        public string CelularFormatado { get; set; }
        public int idEmpresa { get; set; }
        public int CodSeqSecao { get; set; }
        public decimal CodIntFunc { get; set; }
        public string ReclamacaoProcede { get; set; }
        public string ProcedeGrid { get; set; }

        public class Anexos
        {
            public int Id { get; set; }
            public int IdAtendimento { get; set; }
            public byte[] Anexo { get; set; }
            public string NomeArquivo { get; set; }
            public bool Existe { get; set; }
        }
    }
}
