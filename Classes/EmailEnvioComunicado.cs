using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class EmailEnvioComunicado // Niff_Jur_Email
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public bool Existe { get; set; }
        public bool Excluido { get; set; }
        public Publicas.TipoEmailComunicado TipoEmail { get; set; }
        public string DescricaoTipoEmail { get; set; }
    }
}
