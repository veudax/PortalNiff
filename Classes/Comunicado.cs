using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Comunicado //Table Niff_Jur_Comunicados
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int IdVara { get; set; }
        public int IdTipo { get; set; }
        public int IdUsuario { get; set; }
        public int IdUsuarioAprovacao { get; set; }
        public int IdUsuarioCancelado { get; set; }
        public int IdUsuarioReprovador { get; set; }
        public int IdUsuarioFinaliza { get; set; }
        public int IdUsuarioAltera { get; set; }
        public DateTime Abertura { get; set; }
        public DateTime Confirmacao { get; set; }
        public DateTime Reprovacao { get; set; }
        public DateTime Cancelamento { get; set; }
        public DateTime Alteracao { get; set; }
        public DateTime Finalizado { get; set; }
        public Publicas.StatusComunicado Status { get; set; }
        public string Solicitante { get; set; }
        public string Processo { get; set; }
        public string Autor { get; set; }
        public Publicas.TipoPessoa TipoAutor { get; set; }
        public decimal CPFDoAutor { get; set; }
        public string PisDoAutor { get; set; }
        public string MotivoTipoOutros { get; set; }
        public bool Reembolso { get; set; }
        public bool Seguro { get; set; }
        public decimal ValorDoReembolso { get; set; }
        public decimal Total { get; set; }
        public int QuantidadeDeParcelas { get; set; }
        public bool NotaFiscal { get; set; }
        public decimal ValorDescontoNotaFiscal { get; set; }
        public string Observacoes { get; set; }
        public string Favorecido { get; set; }
        public Publicas.TipoPessoa TipoFavorecido { get; set; }
        public decimal CPFFavorecido { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public int Referencia { get; set; }
        public string Resumo { get; set; }
        public string NovoProcesso { get; set; }
        public string EmailEnviado { get; set; }
        public int CentroDeCustos { get; set; }
        public string Vara { get; set; }
        public string Tipo { get; set; }
        public string Parcelas { get; set; }
        public string UsuarioAprovador { get; set; }
        public string UsuarioReprovador { get; set; }
        public string UsuarioCancelador { get; set; }
        public string UsuarioAlterador { get; set; }
        public string UsuarioFinaliza { get; set; }
        public string Custo { get; set; }
        public string Empresa { get; set; }
        public bool Existe { get; set; }
        public string MotivoCancelamento { get; set; }
        
    }
}
