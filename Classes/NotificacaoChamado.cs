using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class NotificacaoChamado  // tabela Niff_CHM_NotifChamado
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdChamado { get; set; }
        public int IdHistorico { get; set; }
        public DateTime Data { get; set; }
        public bool Existe { get; set; }
    }
}
