using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class PeriodoBancoHorasColaborador // tabela Niff_Pto_ColabPeriodo
    {
        public int Id { get; set; }
        public int IdColaborador { get; set; }
        public int ReferenciaInicial { get; set; }
        public int ReferenciaFinal { get; set; }
        public bool Ativo { get; set; }
        public bool Existe { get; set; }
        public string ReferenciaInicioFormatada { get; set; }
        public string ReferenciaFimFormatada { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
    }
}
