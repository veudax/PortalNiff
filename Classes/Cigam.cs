using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Cigam
    {
        public int Id { get; set; }
        public int CodigoEmpresa { get; set; }
        public int CodigoFl { get; set; }
        public decimal CodLanca { get; set; }
        public decimal CoditemLanca { get; set; }
        public DateTime Data { get; set; }
        public string Lote { get; set; }
        public string Origem { get; set; }
        public string Documento { get; set; }
        public string Situacao { get; set; }
        public string Modificado { get; set; }
        public string Usuario { get; set; }
        public int CodContaContabil { get; set; }
        public int? CodContraPartida { get; set; }
        public decimal Valor { get; set; }
        public int? CodCusto { get; set; }
        public string TipoLancamento { get; set; } // D/C
        public string Historico { get; set; }
    }
}
