using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /*Tabela NIFF_CHM_Chat*/
    public class Chat
    {
        public int IdChat { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataHora { get; set; }
        public int IdUsuarioOrigem { get; set; }
        public int IdUsuarioDestino { get; set; }
        public string NomeEnviada { get; set; }
        public string NomeRecebida { get; set; }
        public string Mensagem { get; set; }
        public bool Excluida { get; set; }
        public DateTime DataExclusao { get; set; }
        public bool Enviada { get; set; }
        public bool Lida { get; set; }

    }
}
