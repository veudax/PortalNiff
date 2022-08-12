using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suportte
{
    public static class EstruturaMenus
    {
        public enum MenuPrincipal
        {
            [Description("Recursos Humanos")]
            RecursosHumanos,
            [Description("Jurídico")]
            Juridico,
            [Description("Contabilidade")]
            Contabilidade,
            [Description("Atendimento")]
            Atendimento,
            [Description("Controladoria")]
            Controladoria,
            [Description("Tecnologia da Informação")]
            TecnologiaDaInformacao,
            [Description("Financeiro")]
            Financeiro
        }

        public enum MenuRecursosHumanos
        {
            [Description("Avaliação de Desempenho")]
            AvaliacaoDeDesempenho,
            [Description("Biblioteca")]
            Biblioteca,
            [Description("Corridas")]
            Corridas,
            [Description("Período Banco de horas")]
            PeriodoBancoDeHoras
        }

        public enum MenuJuridico
        {
            [Description("Cadastros")]
            Cadastros,
            [Description("Novo Comunicado")]
            NovoComunicado
        }

        public enum MenuCadastroJuridico
        {
            [Description("Vara")]
            Vara,
            [Description("Tipo de Pagamento")]
            TipoDePagamento
        }

        public enum MenuAvaliacaoDesempenho
        {
            [Description("Controladoria")]
            Controladoria,
            [Description("Recursos Humanos")]
            RecursosHumanos,
            [Description("Colaborador")]
            Colaborador,
            [Description("Gestor")]
            Gestor
        }

        public enum MenuControladoriaAvaliacaoDesempenho
        {
            [Description("Cadastro de Metas")]
            Metas,
            [Description("Definição de Metas")]
            DefinicaoMetas
        }

        public enum MenuRHAvaliacaoDesempenho
        {
            [Description("Cadastros")]
            Cadastros,
            [Description("Metas Numéricas")]
            MetasNumericas,
            [Description("Avaliação Qualitativas")]
            AvaliacaoQualitativa,
            [Description("Feedback")]
            Feedback,
            [Description("Radar")]
            Radar,
            [Description("Notas")]
            Notas
        }

        public enum MenuCadastrosRHAvaliacaoDesempenho
        {
            [Description("Colaboradores")]
            Colaboradores,
            [Description("Prazos")]
            Prazos,
            [Description("Cargos")]
            Cargos,
            [Description("Escolaridade")]
            Escolaridade,
            [Description("Competências")]
            Competencias,
            [Description("Departamento")]
            Departamento,
            [Description("Pontuação")]
            Pontuacao,
            [Description("9Box")]
            NineBox
        }
    }
}
