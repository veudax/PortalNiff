using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class EmpresaQueOColaboradorEhAvaliado // Niff_ads_EmpresasColAvalia
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int IdColoborador { get; set; }
        public string Empresa { get; set; }
        public string DataInicio { get; set; }
        public string DataFinal { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public bool Existe { get; set; }
    }
}
