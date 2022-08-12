using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Feriado // finferia_empresafilial - finferia  Globus
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        //public string Empresa { get; set; }
        public bool Existe { get; set; }
    }

    public class FeriadoEmenda
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public string Tipo { get; set; }
        public string TipoDescricao { get; set; }
        public bool Existe { get; set; }
        public int Ano { get; set; }
    }
}
