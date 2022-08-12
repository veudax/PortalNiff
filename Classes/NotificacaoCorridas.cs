using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class NotificacaoCorridas // Niff_Chm_NotifCorrida
    {
        public int Id { get; set; }
        public int IdCorrida { get; set; }
        public int IdUsuario { get; set; }
        public bool Existe { get; set; }
    }
}
