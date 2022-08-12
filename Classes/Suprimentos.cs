using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Suprimentos
    {
        public class Metas
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int IdUsuarioInclusao { get; set; }
            public int IdUsuarioAlteracao { get; set; }
            public int Referencia { get; set; }
            public int Ano { get; set; }
            public decimal CodIntFunc { get; set; }
            public decimal ValorMeta { get; set; }
            public DateTime DataInclusao { get; set; }
            public DateTime DataAlteracao { get; set; }
            public string Funcionario { get; set; }
            public string UsuarioInclusao { get; set; }
            public string UsuarioAlteracao { get; set; }
            public string ReferenciaFormatada { get; set; }
            public bool Existe { get; set; }
        }

        public class Pedidos
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int IdUsuarioInclusao { get; set; }
            public int IdUsuarioAlteracao { get; set; }
            public int Referencia { get; set; }
            public string NumeroPedido { get; set; }
            public Publicas.TipoPedido TipoPedido { get; set; } 
            public DateTime DataPedido { get; set; }
            public string UsuarioInclusao { get; set; }
            public string UsuarioAlteracao { get; set; }
            public string ReferenciaFormatada { get; set; }
            public string Fornecedor { get; set; }
            public string Aprovador { get; set; }
            public string Status { get; set; }
            public decimal Total { get; set; }
            public decimal TotalGrupo500e510 { get; set; }
            public bool Existe { get; set; }
        }

        public class ItensPedido
        {
            public int Id { get; set; }
            public int GrupoDespesa { get; set; }
            public string NumeroPedido { get; set; }
            public decimal Quantidade { get; set; }
            public decimal ValorUnitario { get; set; }
            public DateTime? DataAprovacao { get; set; }
            public string Aprovador { get; set; }
            public string Material { get; set; }
            public decimal Total { get; set; }
        }
    }
}
