using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class LogSAC
    {
        public int Id { get; set; }
        public int IdAtendimento { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int IdUsuario { get; set; }
        public bool Existe { get; set; }
    }
}
