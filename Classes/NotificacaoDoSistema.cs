using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class NotificacaoDoSistema // tabela Niff_Chm_NotificacoesSistema
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataDoAviso { get; set; }
        public DateTime DataDaAcao { get; set; }
        public DateTime DataFimDaAcao { get; set; }
        public string Motivo { get; set; }
        public string TipoAtualizacao { get; set; }
        public bool Status { get; set; }
        public bool Existe { get; set; }
    }
}
