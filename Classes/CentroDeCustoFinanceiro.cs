using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class CentroDeCustoFinanceiro // tabela cpgcustos (globus)
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Classificador { get; set; }
        public bool AceitaLancamento { get; set; }
        public bool Existe { get; set; }
    }
}
