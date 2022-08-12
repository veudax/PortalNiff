using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ItensNotasFiscaisEscrituracaoGlobus
    {
        public int Item { get; set; }
        public string Produto { get; set; }
        public string SituacaoTributaria { get; set; }
        public int CFOP { get; set; }
        public int OperacaoFiscal { get; set; }
        public int Quantidade { get; set; }
        public int Marca { get; set; }
        public int Local { get; set; }
        public int IdMaterial { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal BaseICMS { get; set; }
        public decimal ValorICMS { get; set; }
        public decimal IsentasICMS { get; set; }
        public decimal AliquotaICMS { get; set; }
        public decimal OutrasICMS { get; set; }
        public decimal TotalNF { get; set; }
        public decimal Frete { get; set; }
        public decimal Seguro { get; set; }
        public decimal Desconto { get; set; }
        public decimal Outros { get; set; }
        public decimal BaseICMSSubstituicao { get; set; }
        public decimal ValorICMSSubstituicao { get; set; }
        public decimal BaseIPI { get; set; }
        public decimal ValorIPI { get; set; }
        public decimal AliquotaIPI { get; set; }
        public decimal IsentaIPI { get; set; }
        public decimal OutrasIPI { get; set; }
        public decimal PIS { get; set; }
        public decimal BasePIS { get; set; }
        public decimal BaseCofins { get; set; }
        public decimal Cofins { get; set; }
        public bool PossuiDiferencialDeAliquota { get; set; }
        public decimal BaseDiferencial { get; set; }
        public decimal ValorDiferencial { get; set; }
        public decimal AliquotaDiferencial { get; set; }
        public decimal PercentualICMSDiferido { get; set; }
        public decimal ValorICMSDiferido { get; set; }

    }
}
