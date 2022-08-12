using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class BolaoEnquete
    {
        public int Id { get; set; }
        public int IdPergunta { get; set; }
        public int IdColaborador { get; set; }
        public string Opcao { get; set; }
        public string Sugestao { get; set; }
    }
}
