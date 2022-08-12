using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_HistoChamado*/
    public class HistoricoDoChamado
    {
        public int IdChamado { get; set; }
        public DateTime Data { get; set; }
        public int IdHistorico { get; set; }
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public Publicas.StatusChamado Status { get; set; }
        public bool Existe { get; set; }
        public bool Privado { get; set; } // Privado só aparece para quem tem status Atendente
        public string NumeroChamado { get; set; }
        public bool Associado { get; set; }
        public bool Agrupado { get; set; }
        public bool Lido { get; set; }
        public string TipoUsuario { get; set; }
        public string Adequacao { get; set; }
        public int Prazo { get; set; }
        public int IdUsuarioAutorizacao { get; set; }
        public string MotivoNegacaoDaAutorizacao { get; set; }
        public bool Autorizado { get; set; }
        public bool AguardarAutorizado { get; set; }
        public bool PodeEncerrar { get; set; }
        public bool RespondeuAutorizacao { get; set; }
        public string Usuario { get; set; }
    }
}
