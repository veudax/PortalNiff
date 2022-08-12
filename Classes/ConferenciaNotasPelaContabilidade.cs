using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ConferenciaNotasPelaContabilidade
    {
        public class Parametros
        {
            public int Id { get; set; }
            public int CodigoGrupo { get; set; }
            public string Grupo { get; set; }
            public string CodigoTipo { get; set; }
            public string NomeTipo { get; set; }
            public int NumeroPlano { get; set; }
            public int CodigoConta { get; set; }
            public string NomeConta { get; set; }
            public string CodigoTipoOriginal { get; set; }
            public int CodigoContaOriginal { get; set; }
            public int NumeroPlanoOriginal { get; set; }
            public bool Alterado { get; set; }
            public bool Existe { get; set; }
        }

        public class GrupoDespesas
        {
            public int Codigo;
            public string Descricao;
            public bool Existe;
        }

        public class Conferencia
        {
            public decimal CodIntNF { get; set; }
            public decimal CodDoctoESF { get; set; }
            public string Fornecedor { get; set; }
            public Decimal NumeroNF { get; set; }
            public DateTime Entrada { get; set; }
            public string CodTipoDocto { get; set; }
            public decimal Valor { get; set; }
            public string ObservacaoCPG { get; set; }
            public string ObservacaoCPGOriginal { get; set; }
            public bool Valido { get; set; }
            public bool Existe { get; set; }
            public string Documento { get; set; }
            public string Origem { get; set; }
            public bool Conferida { get; set; }
            public decimal CodDoctoCPG { get; set; }
            public decimal CodISSInt { get; set; }
        }

        public class ItensConferencia
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int Referencia { get; set; }
            public decimal CodIntNF { get; set; }
            public decimal CodDoctoESF { get; set; }
            public decimal CodMaterial { get; set; }
            public string Material { get; set; }
            public string Fornecedor { get; set; }
            public decimal NumeroNF { get; set; }
            public DateTime Entrada { get; set; }
            public string CodTipoDocto { get; set; }
            public decimal Valor { get; set; }
            public int CodGrupo { get; set; }
            public string GrupoDespesa { get; set; }
            public string TipoDespesaItem { get; set; }
            public string TipoDespesaNota { get; set; }
            public string ContaContabilItem { get; set; }
            public string ContaContabilNota { get; set; }
            public string ContaContabilCTB { get; set; }
            public string NomeContaContabilItem { get; set; }
            public string NomeContaContabilNota { get; set; }
            public string NomeContaContabilCTB { get; set; }
            public string CentroCusto { get; set; }
            public string ObservacaoCPG { get; set; }
            public string ObservacaoCPGOriginal { get; set; }
            public bool Valido1 { get; set; }
            public bool Valido2 { get; set; }
            public bool Valido3 { get; set; }
            public bool Valido4 { get; set; }
            public bool Conferido { get; set; }
            public int IdUsuarioConferido { get; set; }
            public DateTime DataConferido { get; set; }
            public string UsuarioConferido { get; set; }
            public bool Validado { get; set; }
            public int IdUsuarioValidador { get; set; }
            public DateTime DataValidado { get; set; }
            public string UsuarioValidador { get; set; }
            public decimal CodLanca { get; set; }
            public int Plano { get; set; }
            public string Origem { get; set; }
            public bool ValidadoOriginal { get; set; }
            public string CodigoGlobus { get; set; }
            public int ReferenciaValidado { get; set; }
            public bool ConferidoESF { get; set; }
            public decimal CodDoctoCPG { get; set; }
            public decimal CodISSInt { get; set; }

            public bool Existe { get; set; }
        }
    }
}
