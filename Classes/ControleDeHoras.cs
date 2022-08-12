using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ControleDeHoras
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdColaborador { get; set; }
        public DateTime Data { get; set; }
        public string DataExtensa { get; set; }
        public string Entrada { get; set; }
        public string Saida { get; set; }
        public string SaidaAlmoco { get; set; }
        public string VoltaAlmoco { get; set; }
        public double Extra { get; set; }
        public double Incompletas { get; set; }
        public string ExtraFormatada { get; set; }
        public string IncompletaFormatada { get; set; }
        public bool Existe { get; set; }
        public bool Atestado { get; set; }
        public bool Declaracao { get; set; }
        public bool Compensacao { get; set; }
        public bool Ausencia { get; set; }
        public string Motivo { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
    }
}
