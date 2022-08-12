using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ParcelasDoComunicado //Niff_Jur_Parcela
    {
        public int Id { get; set; }
        public int IdComunicado { get; set; }
        public int Parcela { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public bool Existe { get; set; }
        public Publicas.TipoVencimento Tipo { get; set; }
    }
}
