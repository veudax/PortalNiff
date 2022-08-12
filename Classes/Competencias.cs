using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Competencias // tabela Niff_ADS_Competencias
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public Publicas.TipoCompetencias Tipo { get; set; }
        public string DescricaoTipo { get; set; }
        public string TextoExplicativo { get; set; }
        public bool Existe { get; set; }
    }
}
