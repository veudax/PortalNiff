using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* Tabela NIFF_CHM_AgendaUsu */
    public class ParticipanteDaAgenda
    {
        public int IdAgenda { get; set; }
        public int IdUsuario { get; set; }
        public bool ConviteAceito { get; set; }
        public string Empresa { get; set; }
        public string Nome { get; set; }
        public bool Marcado { get; set; }
        public bool Avisado { get; set; }
        public bool Existe { get; set; }
    }
}
