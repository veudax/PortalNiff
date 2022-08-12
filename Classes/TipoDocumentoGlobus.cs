using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class TipoDocumentoGlobus // tabela cprtpdoc
    {
        public int CodigoEmpresa { get; set; }
        public int CodigoFilial { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeCasasDecimais { get; set; }
        public bool IntegraComLivroDeISS { get; set; }
        public bool Existe { get; set; }
    }
}
