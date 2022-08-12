using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class CompetenciasDoColaborador // tabela Niff_ADS_CompetenciasDaPessoa
    {
        public int Id { get; set; }
        public int IdColaborador { get; set; }
        public int IdCompetenciasDoCargo { get; set; }
        public string Descricao { get; set; }
        public bool Marcado { get; set; }
        public bool Ativo { get; set; }
        public bool Existe { get; set; }
    }
}
