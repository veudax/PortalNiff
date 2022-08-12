using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Favoritos //NIFF_Chm_Favoritos
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string NomeMenu { get; set; }
        public string Name { get; set; }
        public string MenuAnterior { get; set; }
        public DateTime Data { get; set; }
        public bool Existe { get; set; }
    }
}
