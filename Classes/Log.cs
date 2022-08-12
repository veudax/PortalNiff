using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Log
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int IdUsuario { get; set; }
        public string Tela { get; set; }
        public bool Existe { get; set; }
    }
}
