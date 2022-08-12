using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class DiferencialAliquota
    {
        public class Documento
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public string Referencia { get; set; }
            public string Numero { get; set; }
            public decimal Base { get; set; }
            public decimal AliquotaExterna { get; set; }
            public decimal Aliquota { get; set; }
            public decimal Debito { get; set; }
            public decimal Credito { get; set; }
            public decimal Diferenca { get; set; }
            public decimal ValorESF { get; set; }
            public string Serie { get; set; }
            public string CodTipoDoc { get; set; }
            public decimal CodigoForn { get; set; }
            public int CodigoEmpresa { get; set; }
            public int CodigoFl { get; set; }
            public decimal CodDoctoESF { get; set; }
            public string Fornecedor { get; set; }
            public DateTime Emissao { get; set; }
            public DateTime Entrada { get; set; }

            public bool Existe { get; set; }
        }

        public class Diferencial
        {
            public int Id { get; set; }
            public int IdDiferencial { get; set; }
            public int IdEmpresa { get; set; }
            public string Referencia { get; set; }
            public decimal CodDoctoESF { get; set; }
            public int CFOP { get; set; }
            public string Documento { get; set; }
            public decimal Valor { get; set; }
            public decimal Aliquota { get; set; }
            public decimal AliquotaExterna { get; set; }
            public decimal Debito { get; set; }
            public decimal Credito { get; set; }
            public decimal Diferenca { get; set; }
            public decimal ValorESF { get; set; }
            public List<Detalhes> Detalhamento { get; set; }
            public bool Existe { get; set; }
            public bool AliquotaZerada { get; set; }
            public bool DiferencaGlobusArquivei { get; set; }
            public string Serie { get; set; }
            public string CodTipoDoc { get; set; }
            public decimal CodigoForn { get; set; }
            public int CodigoEmpresa { get; set; }
            public int CodigoFl { get; set; }
            public decimal AliquotaOriginal { get; set; }
            public string Fornecedor { get; set; }
            public DateTime Emissao { get; set; }
            public DateTime Entrada { get; set; }

        }

        public class Detalhes
        {
            public int Id { get; set; }
            public string Documento { get; set; }
            public string Item { get; set; }
            public string TipoDocto { get; set; }
            public decimal CodDoctoESF { get; set; }
            public string Fornecedor { get; set; }
            public DateTime Emissao { get; set; }
            public DateTime Entrada { get; set; }
            public decimal Base { get; set; }
            public decimal Aliquota { get; set; }
            public decimal ICMS { get; set; }
            public int CFOPNota { get; set; }
            public bool Existe { get; set; }
        }
    }

}
