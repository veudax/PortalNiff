using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ItensNotasFiscaisLivroISS
    {
        public int CodigoInterno { get; set; }
        public int NumeroPlano { get; set; }
        public string CodigoTipoDeDespesa { get; set; }
        public int OperacaoFiscal { get; set; }
        public int Item { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public int CodContaContabil { get; set; }
        public string Complemento { get; set; }

        public decimal BaseISS { get; set; }
        public decimal AliquotaISS { get; set; }
        public decimal ValorISS { get; set; }

        public string CodigoServico { get; set; }
        public int CFOP { get; set; }
        
    }
}
