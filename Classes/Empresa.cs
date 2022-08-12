using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_Empresas*/
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string CodigoEmpresaGlobus { get; set; }
        public string TextoPadraoSAC { get; set; }
        public Publicas.TipoCalculoCodigoSAC FormatoCodigo { get; set; }
        public string Separador { get; set; }
        public string NomeAbreviado { get; set; }
        public string Email { get; set; }
        public string Smtp { get; set; }
        public bool Autentica { get; set; } 
        public bool AutenticaSLL { get; set; }
        public int PortaSmtp { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public string CodigoeNome { get; set; }
        public decimal ValorDescontoSobreFaltaJustificada { get; set; }
        public decimal ValorDescontoSobreFaltaInjustificada { get; set; }
        public int QuantidadeFaltasJustificadasSuperior { get; set; }
        public int QuantidadeFaltasInjustificadasSuperior { get; set; }
        public int QuantidadeDiasCanceladoNoGrid { get; set; }
        public int QuantidadeDiasSemRetornoNoGrid { get; set; }
        public int QuantidadeDiasParaResponder { get; set; }
        public bool Existe { get; set; }
        public bool AvaliaColaboradores { get; set; }
        public bool Zero800 { get; set; }
        public int AtendenteRespEmDiasSAC { get; set; }

    }
}
