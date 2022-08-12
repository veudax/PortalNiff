using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Operacional
    {
        public class Indicadores
        {
            public int Id { get; set; }
            public string Descricao { get; set; }
            public string Abreviado { get; set; }
            public string TipoDeValores { get; set; }
            public bool Ativo { get; set; }
            public bool Existe { get; set; }
        }

        public class Setor
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int Codigo { get; set; }
            public string Descricao { get; set; }
            public bool Ativo { get; set; }
            public bool Existe { get; set; }
        }

        public class Vigencia
        {
            public int Id { get; set; }
            public int IdSetor { get; set; }
            public DateTime Data { get; set; }
            public string CodigoLinha { get; set; }
            public int CodigoInternoLinha { get; set; }
            public string NomeLinha { get; set; }
            public string Classificacao { get; set; }
            public string Setor { get; set; }
            public bool TemCobrador { get; set; }
            public bool Existe { get; set; }
        }

        public class Metas
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int IdIndicador { get; set; }
            public DateTime Data { get; set; }
            public decimal Peso { get; set; }
            public decimal Meta { get; set; }
            public bool Ativo { get; set; }
            public bool Existe { get; set; }
        }

        public class Pontuacao
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int Codigo { get; set; }
            public string Descricao { get; set; }
            public bool Ativo { get; set; }
            public bool Existe { get; set; }
        }

        public class VigenciaPontuacao
        {
            public int Id { get; set; }
            public int IdPontuacao { get; set; }
            public DateTime Data { get; set; }
            public decimal Inicio { get; set; }
            public decimal Fim { get; set; }
            public bool Existe { get; set; }
        }

        public class Valores
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int IdIndicador { get; set; }
            public int CodigoInternoLinha { get; set; }
            public string Periodo { get; set; }
            public DateTime Data { get; set; }
            public decimal Programado { get; set; }
            public decimal Realizado { get; set; }
            public decimal Pontuacao { get; set; }
            public string Setor { get; set; }
            public string NomeLinha { get; set; }
            public string CodigoLinha { get; set; }
            public string Indicador { get; set; }
            public string Abreviado { get; set; }
            public string DescricaoPeriodo { get; set; }
            public bool Existe { get; set; }
        }

        public class Linhas
        {
            public int Id { get; set; }
            public int IdLinha { get; set; }// linha principal
            public int CodigoInternoLinha { get; set; }
            public string NomeLinha { get; set; }
            public string CodigoLinha { get; set; }
            public bool Existe { get; set; }
        }

        public class Avaliacao
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int IdIndicador { get; set; }
            public int IdLinha { get; set; }
            public string Referencia { get; set; }
            public decimal Valor { get; set; }
            public decimal Meta { get; set; }
            public decimal Pontuacao { get; set; }
            public bool Existe { get; set; }
        }

        public class Demonstrativo
        {
            public DateTime Data { get; set; }
            public string DataGrupo { get; set; }
            public string CodigoLinha { get; set; }
            public string NomeLinha { get; set; }
            public string Setor { get;set; }
            public decimal Quadro { get; set; }
            public decimal FrotaP1 { get; set; }
            public decimal FrotaR1 { get; set; }
            public decimal FrotaP2 { get; set; }
            public decimal FrotaR2 { get; set; }
            public decimal FCVProg { get; set; }
            public decimal FCVReal { get; set; }
            public decimal FCVPercentual { get; set; }
            public decimal? FCVPerda { get; set; }
            public decimal? Pontual { get; set; }
            public decimal PontualPercentual { get; set; }
            public decimal? SAC { get; set; }
            public decimal? Acidente { get; set; }
            public decimal? RA { get; set; }
            public decimal? SOS{ get; set; }
            public decimal? AbsenteismoMot { get; set; }
            public decimal? AbsenteismoCob { get; set; }
            public decimal? TotalAbsenteismo { get; set; }
            public decimal? Indice { get; set; }
            public decimal RefeicaoProg { get; set; }
            public decimal RefeicaoReal { get; set; }
            public decimal RefeicaoPercentual { get; set; }
            public decimal PAX { get; set; }
            public decimal PVD { get; set; }
            public decimal ReceitaTotal { get; set; }
            public decimal ReceitaPorCarro { get; set; }
            public decimal? Km { get; set; }
            public decimal? Consumo { get; set; }
            public decimal Media { get; set; }
        }

        public class IQO
        {
            public string CodigoLinha { get; set; }
            public string NomeLinha { get; set; }
            public string Setor { get; set; }
            public decimal PAX { get; set; }
            public decimal FrotaMeta { get; set; }
            public decimal FrotaProg { get; set; }
            public decimal FrotaR { get; set; }
            public decimal FrotaReal { get; set; }
            public decimal FrotaPontuacao { get; set; }
            public decimal FCVProg { get; set; }
            public decimal FCVMeta { get; set; }
            public decimal FCVReal { get; set; }
            public decimal FCVPontuacao { get; set; }
            public decimal PontualidadeMeta { get; set; }
            public decimal PontualidadeReal { get; set; }
            public decimal PontualidadePontuacao { get; set; }
            public decimal SAC { get; set; }
            public decimal SACMeta { get; set; }
            public decimal SACReal { get; set; }
            public decimal SACPontuacao { get; set; }
            public decimal Acidentes { get; set; }
            public decimal AcidenteMeta { get; set; }
            public decimal AcidenteReal { get; set; }
            public decimal AcidentePontuacao { get; set; }
            public decimal RA { get; set; }
            public decimal SOS { get; set; }
            public decimal KM { get; set; }
            public decimal KMMeta { get; set; }
            public decimal KMReal { get; set; }
            public decimal KMPontuacao { get; set; }
            public decimal AbsenteismoMeta { get; set; }
            public decimal AbsenteismoReal { get; set; }
            public decimal AbsenteismoPontuacao { get; set; }
            public decimal RefeicaoMeta { get; set; }
            public decimal RefeicaoReal { get; set; }
            public decimal RefeicaoPontuacao { get; set; }
            public decimal LimpezaMeta { get; set; }
            public decimal LimpezaReal { get; set; }
            public decimal LimpezaPontuacao { get; set; }
            public decimal AvariaMeta { get; set; }
            public decimal AvariaReal { get; set; }
            public decimal AvariaPontuacao { get; set; }
            public decimal PontuacaoMeta { get; set; }
            public decimal PontuacaoReal { get; set; }
            public string Resultado { get; set; }
            public decimal Quadro { get; set; }
        }
    }
}
