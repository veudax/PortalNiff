using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class HistoricosDoColaborador // tabela Niff_Ads_HistoricoColaborador
    {
        public int Id { get; set; }
        public int IdColaborador { get; set; }
        public int IdCargo { get; set; }
        public int IdDepartamento { get; set; }
        public int IdSuperior { get; set; }
        public int IdEscolaridade { get; set; }
        public DateTime Data { get; set; }
        public decimal Salario { get; set; }
        public bool Existe { get; set; }
        public string DescricaoCargo { get; set; }
        public string DescricaoDepartamento { get; set; }
        public string NomeSuperior { get; set; }
        public string DescricaoEscolaridade { get; set; }
    }
}
