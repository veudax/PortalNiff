using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class BolaoNotificacao // Niff_bol_NotificacaoJogos
    {
        public int Id { get; set; }
        public int IdColaborador { get; set; }
        public int IdJogo { get; set; }
        public bool Existe { get; set; }
    }
}
