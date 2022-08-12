using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_Chamado*/
    public class Chamado
    {
        public int IdChamado { get; set; }
        public int IdUsuario { get; set; }
        public int IdCategoria { get; set; }
        public int IdTela { get; set; }
        public int IdEmpresa { get; set; }
        public string Assunto { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataRetorno { get; set; }
        public DateTime DataOrdenacao { get; set; }
        public DateTime SLA { get; set; }
        public DateTime DataAvaliacao { get; set; }
        public string Numero { get; set; }
        public Publicas.StatusChamado Status { get; set; }
        public Publicas.Origem Origens { get; set; }
        public Publicas.Prioridades Prioridade { get; set; }
        public string Adequacao { get; set; }
        public DateTime DataAdequacao { get; set; }
        public Publicas.TipoDeSatisfacaoAtendimento Avaliacao { get; set; }
        public string NomeSolicitante { get; set; }
        public string NomeAnalista { get; set; }
        public string DescricaoAvaliacao { get; set; }
        public string DescricaoStatus { get; set; }
        public string DescricaoDaOrigem { get; set; }
        public string DescricaoPrioridade { get; set; }
        public string DescricaoTipo { get; set; }
        public string Categoria { get; set; }
        public string Modulo { get; set; }
        public string Tela { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public Publicas.TipoChamado Tipo { get; set; }
        public bool ProblemaResolvido { get; set; }
        public bool DentroDoPrazo { get; set; }
        public int IdChamadoAgrupado { get; set; }
        public int IdUsuarioAgrupou { get; set; }
        public DateTime DataAgrupou { get; set; }
        public bool Agrupar { get; set; }
        public bool Existe { get; set; }
        public string TipoPrazos { get; set; }
        public string Retorno { get; set; }
        public int IdChamadoAssociado { get; set; }
        public bool Lido { get; set; }
        public bool AtendenteFoiCortez { get; set; }
        public DateTime DataAvaliacaoDoSolicitante { get; set; }
        public bool SolicitanteFoiCortez { get; set; }
        public bool SolicitanteDentroPrazo { get; set; }
        public bool SolicitanteAbriuCorreto { get; set; }
        public string DescricaoAvaliacaoSolic { get; set; }
        public Publicas.TipoDeSatisfacaoAtendimento AvaliacaoSolicitante { get; set; }
        public int PrazoDesenvolvimento { get; set; }
        public int IdAtendente { get; set; }
        public int DiasDoLembrete { get; set; }
        public DateTime DataLembrete { get; set; }
        public string MotivoLembrete { get; set; }
        public bool ApenasAtendendeQueEncerra { get; set; }
        public int IdUsuarioAutorizacao { get; set; }
        public int IdUsuarioAcompanhamento { get; set; }
        public string MotivoNegacaoDaAutorizacao { get; set; }
        public bool Autorizado { get; set; }
        public bool AguardarAutorizado { get; set; }
        public bool Exibir { get; set; }
        public bool Reavaliar { get; set; }
        public bool Reavaliado { get; set; }
        public string NomeAutorizador { get; set; }
        public DateTime DataReavaliacao { get; set; }
        public int IdEmpresaSolicitante { get; set; }
        public string EmpresaSolicitante { get; set; }
        public string EmpresaSelecionada { get; set; }
        public bool TrocouCategoria { get; set; }
        public string UltimoUsuario { get; set; }
        public string PrimeiroAtendente { get; set; }
        public bool RespondeuAutorizacao { get; set; }

        public int MinutosEstimados { get; set; } 
        public string HorasEstimadas { get; set; }
        public DateTime InicioTemporizador { get; set; }
        public DateTime FimTemporizador { get; set; }
        public int MinutosTemporizador { get; set; }
        public string Temporizador { get; set; }
        public bool TemporizadorEmAndamento { get; set; }
        public int IdTemporizador { get; set; }
        public bool Privado { get; set; }
    }

    public class Lembrete
    {
        public int Id { get; set; }
        public int IdChamado { get; set; }
        public DateTime Data { get; set; }
    }

    public class TempoExecucao
    {
        public int Id { get; set; }
        public int IdChamado { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataInicio { get; set; }// data e hora
        public DateTime DataFim { get; set; } // data e  hora
        public int Minutos { get; set; }
        public string TempoEmHoras { get; set; }
        public string NomeUsuario { get; set; }
        public bool Existe { get; set; }
    }
}
