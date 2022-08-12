using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class AutoAvaliacao // tabela Niff_ADS_Avaliacao
    {
        public int Id { get; set; }
        public int IdColaborador { get; set; }
        public string Colaborador { get; set; }
        public string Email { get; set; }
        public int MesReferencia { get; set; }
        public Publicas.TipoPrazos Tipo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool Existe { get; set; }
        public string Status { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int IdUsuario { get; set; }
        public int IdUsuarioAlteracao { get; set; }
        public string FeedbackGestor { get; set; }
        public string Comentario { get; set; }
        public decimal TotalAvaliacao { get; set; }
        public int Ano { get; set; }
        public string ReferenciaFormatada { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public string Registro { get; set; }
        public int IdEmpresa { get; set; }
        public int Ordem { get; set; }
    }
}
