using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class OcorrenciasGlobus
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string CodigoENome { get; set; }
        public bool Selecionado { get; set; }
        public bool Existe { get; set; }
        public int Id { get; set; }
    }

    public class AreaGlobus
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string CodigoENome { get; set; }
        public bool Selecionado { get; set; }
        public bool Existe { get; set; }
        public int Id { get; set; }
    }

    public class FuncoesGlobus
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string CodigoENome { get; set; }
        public bool Selecionado { get; set; }
        public bool Existe { get; set; }
        public int Id { get; set; }
    }

    public class TipoDeFrota
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Existe { get; set;}
    }

}
