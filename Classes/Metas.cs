using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Metas // tabela Niff_Ads_Metas
    {
        public int Id { get; set; }
        public Publicas.TipoDeMetas Tipo { get; set; }
        public Publicas.Perspectivas Perspectiva { get; set; }
        public string Descricao { get; set; }
        public string TextoBI { get; set; }
        public bool Ativo { get; set; }
        public Publicas.RegraFormulaMetas Regra { get; set; }
        public string Formula { get; set; }
        public bool UsaNaAvaliacao { get; set; }
        public bool UsaNaGestao { get; set; }
        public bool UsaKMRodado { get; set; }
        public string PrevistoCalculaPor { get; set; }
        public int PrevistoQdtMeses { get; set; }
        public bool PrevistoAplicaDiasUteis { get; set; }
        public bool PrevistoPermiteAlterar { get; set; }
        public bool RealizadoPermiteAlterar { get; set; }
        public bool UsarColunaPrevistoParaCalculo { get; set; }
        public int IdBI { get; set; }
        public int QuantidadeDecimais { get; set; }
        public bool Existe { get; set; }
        public bool ExibeNoDRE { get; set; }
        public bool GrupoTotalizador { get; set; }
        public string FormulaTotalizador { get; set; }
        public int NivelCalculo { get; set; }
        public string TipoDeFrota { get; set; }
    }

    public class MetasBIItens
    {
        public int Id { get; set; }
        public int IdMetas { get; set; }
        public string Tipo { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Existe { get; set; }
    }

    public class ValoresDasMetas :Metas// Niff_Ads_ValoresMetas
    {
        public int IdMetas { get; set; }
        public int IdEmpresa { get; set; }
        public string Referencia { get; set; }
        public string Mes { get; set; }
        public string MotivoEdicaoPrevisto { get; set; }
        public string MotivoEdicaoReal { get; set; }
        public decimal Previsto { get; set; }
        public decimal Realizado { get; set; }
        public decimal PrevistoOriginal { get; set; }
        public decimal RealizadoOriginal { get; set; }
        public int IdUsuarioQueGerou { get; set; }
        public int IdUsuarioQueEditou { get; set; }
        public DateTime DataQueGerou { get; set; }
        public DateTime DataQueAlterou { get; set; }
        public bool AplicouNoContratoDeMetas { get; set; } // Gravou a definição de metas.
        public int DiasUteis { get; set; }
        public int DiasCorridos { get; set; }
        public int FolgaComplementares { get; set; }
        public int DiasFeriados { get; set; }
        public bool Marcado { get; set; }
        public decimal MediaCPrevisto { get; set; }
        public decimal MediaUPrevisto { get; set; }
        public decimal MediaCRealizado { get; set; }
        public decimal MediaURealizado { get; set; }
        public decimal RealizadoPuro { get; set; }
        public decimal FolgaComplementarRealizada { get; set; }
        public decimal FeriasBase { get; set; }
        public decimal PLRPrevisto { get; set; }
        public decimal PLRRealizado { get; set; }
        public decimal Dissidio { get; set; }
        public DateTime DataCorteFinanceiro { get; set; }
        public DateTime DataCorteOperacional { get; set; }
        public string Metas { get; set; }
    }

    public class CalculoMetas : Metas // Niff_Ads_CalculoMetas
    {
        public int IdMetas { get; set; }
        public int IdEmpresa { get; set; }
        public string Referencia { get; set; }
        public decimal Percentual { get; set; }
        public bool Aumentou { get; set; }
        public bool UsouDiasUteis { get; set; }
        public bool UsouDiasCorridos { get; set; }
        public bool UsouPrevisto { get; set; }
        public bool UsouRealizado { get; set; }
        public bool PermitiuAlterar { get; set; }
        public decimal ValorCalculado { get; set; }
        public decimal ValorResultado { get; set; }
        public decimal ValorResultadoOriginal { get; set; }
        public int IdUsuarioQueGerou { get; set; }
        public int IdUsuarioQueEditou { get; set; }
        public DateTime DataQueGerou { get; set; }
        public DateTime DataQueAlterou { get; set; }
        public int DiasUteis { get; set; }
        public int DiasCorridos { get; set; }
        public decimal FeriasBase { get; set; }
        public decimal PLRPrevisto { get; set; }
        public decimal Dissidio { get; set; }
        public int DiasFeriados { get; set; }

    }

    public class MesesUsadoNoCalculo
    {
        public int Id { get; set; }
        public int IdCalculo { get; set; }
        public string Referencia { get; set; }
        public int IdValorMetas { get; set; }
        public decimal Previsto { get; set; }
        public decimal Realizado { get; set; }
        public bool Existe { get; set; }
        public int DiasUteis { get; set; }
        public int DiasCorridos { get; set; }
    }

    public class ValoresPorMes
    {
        public string Tipo { get; set; }
        public decimal ValorJan { get; set; }
        public byte[] ImagemJan { get; set; }
        public decimal DesempenhoJan { get; set; }
        public decimal ValorFev { get; set; }
        public byte[] ImagemFev { get; set; }
        public decimal DesempenhoFev { get; set; }
        public decimal ValorMar { get; set; }
        public byte[] ImagemMar { get; set; }
        public decimal DesempenhoMar { get; set; }
        public decimal ValorAbr { get; set; }
        public byte[] ImagemAbr { get; set; }
        public decimal DesempenhoAbr { get; set; }
        public decimal ValorMai { get; set; }
        public byte[] ImagemMai { get; set; }
        public decimal DesempenhoMai { get; set; }
        public decimal ValorJun { get; set; }
        public byte[] ImagemJun { get; set; }
        public decimal DesempenhoJun { get; set; }
        public decimal ValorJul { get; set; }
        public byte[] ImagemJul { get; set; }
        public decimal DesempenhoJul { get; set; }
        public decimal ValorAgo { get; set; }
        public byte[] ImagemAgo { get; set; }
        public decimal DesempenhoAgo { get; set; }
        public decimal ValorSet { get; set; }
        public byte[] ImagemSet { get; set; }
        public decimal DesempenhoSet { get; set; }
        public decimal ValorOut { get; set; }
        public byte[] ImagemOut { get; set; }
        public decimal DesempenhoOut { get; set; }
        public decimal ValorNov { get; set; }
        public byte[] ImagemNov { get; set; }
        public decimal DesempenhoNov { get; set; }
        public decimal ValorDez { get; set; }
        public byte[] ImagemDez { get; set; }
        public decimal DesempenhoDez { get; set; }
    }

    public class BSCEmEdicao
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Referencia { get; set; }
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string Tela { get; set; }
        public bool Existe { get; set; }
    }

    public class MetasContasContabeis
    {
        public int Id { get; set; }
        public int IdMetas { get; set; }
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int Conta { get; set; }
        public string Nome { get; set; }
        public int Plano { get; set; }
        public string Tipo { get; set; }
        public bool Existe { get; set; }
        public bool Marcado { get; set; }
        public string Formula { get; set; }
    }

    public class DRE
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Referencia { get; set; }
        public int IdUsuario { get; set; }
        public int IdUsuarioFechamento { get; set; }
        public DateTime DataFechamento { get; set; }
        public bool Fechado { get; set; }
        public decimal Dissidio { get; set; }
        public bool Existe { get; set; }
    }
}
