using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* Tabela NIFF_CHM_AGENDA*/
    public class Agenda : SalaDeReuniao 
    {
        public int IdAgenda { get; set; }
        public DateTime Data { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime DataFim { get; set; }
        public DateTime DataFimReal { get; set; }
        public DateTime HoraFim { get; set; }
        public DateTime DataLembrete { get; set; }
        public Publicas.TipoAgenda TipoAgenda { get; set; }
        public string DescricaoTipoAgenda { get; set; }
        public Decimal CodigoVeiculo { get; set; } // globus
        public string Local { get; set; }
        public int IdUsuario { get; set; }
        public bool DiaTodo { get; set; }
        public int Lembrar { get; set; }
        public Publicas.StatusAgenda Status { get; set; }
        public string Texto { get; set; }
        public string Prefixo { get; set; }
        public string Placa { get; set; }
        public bool Existe { get; set; }
        public int Tempo { get; set; }
        public string Adiar { get; set; }

    }
}
