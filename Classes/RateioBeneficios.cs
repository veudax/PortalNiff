using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class RateioBeneficios
    {
        public class PlanoContabil
        {
            public int NumeroPlano { get; set; }
            public string Nome { get; set; }
            public bool Existe { get; set; }
        }

        public class Setor
        {
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public bool Existe { get; set; }
        }

        public class ContasContabeis
        {
            public int NumeroPlano { get; set; }
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public string Classificador { get; set; }
            public bool AceitaLancamento { get; set; }
            public bool Existe { get; set; }
        }

        public class Parametros
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int NumeroPlano { get; set; }
            public string Lote { get; set; }
            public bool Ativo { get; set; }
            public bool IgnorarFuncoes { get; set; }
            public string CodigoFuncoes { get; set; }
            public string HistoricoPadrao { get; set; }
            public bool IgnorarFuncionariosSemBeneficios { get; set; }
            public string CodigoContaValeRefeicao { get; set; }
            public string CodigoContaValeTransporte { get; set; }
            public string CodigoContaConvenioMedico { get; set; }
            public string CodigoContaConvenioOdontologio { get; set; }
            public string CodigoContaCestaBasica { get; set; }
            public string CodigoEventosValeTransporte { get; set; }
            public bool RegraEspecificaVT { get; set; }
            public bool RegraEspecificaConvenios { get; set; }
            public bool IgnorarFuncionarioSemConvenioMedico { get; set; }
            public bool IgnorarFuncionarioSemConvenioOdontologico { get; set; }

            public bool Existe { get; set; }
        }

        public class Custos
        {
            public int Id { get; set; }
            public int IdParam { get; set; }
            public int CodigoCusto { get; set; }
            public string Nome { get; set; }
            public bool Existe { get; set; }
        }

        public class Associacao
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int IdParam { get; set; }
            public int CodigoCusto { get; set; }
            public string Nome { get; set; }
            public string Conta { get; set; }
            public int CodConta { get; set; }
            public string NomeConta { get; set; }
            public int CodContaDestino { get; set; }
            public string NomeContaDestino { get; set; }
            public int CodigoSetor { get; set; }
            public string NomeSetor { get; set; }
            public bool Existe { get; set; }
            public bool Selecionado { get; set; }
            public bool SelecionadoAnterior { get; set; }
        }

        public class RateioPercentualCustoSetor
        {
            public int Id { get; set; }
            public int IdRateio { get; set; }
            public int CodigoCusto { get; set; }
            public string Custo { get; set; }
            public int CodigoSetor { get; set; }
            public string Setor { get; set; }
            public int Quantidade { get; set; }
            public decimal Percentual { get; set; }
            public string Regra { get; set; }
            public bool Existe { get; set; }
        }

        public class ValoresParaRatear
        {
            public int CodigoConta { get; set; }
            public int CodigoCusto { get; set; }
            public decimal Valor { get; set; }
        }

        public class Rateio
        {
            public int Id { get; set; }
            public int IdEmpresa { get; set; }
            public int Referencia { get; set; }
            public int IdUsuario { get; set; }
            public DateTime Data { get; set; }
            public bool ArquivoGerado { get; set; }
            public bool Existe { get; set; }
        }

        public class ValoresRateados
        {
            public int Id { get; set; }
            public int IdRateio { get; set; }
            public string Documento { get; set; }
            public string Lote { get; set; }
            public int CodigoConta { get; set; }
            public string NomeConta { get; set; }
            public string Conta { get; set; }
            public int ContraPartida { get; set; }
            public string NomeContraPartida { get; set; }
            public int CodigoCusto { get; set; }
            public int CodigoCustoCredito { get; set; }
            public string NomeCusto { get; set; }
            public decimal Debito { get; set; }
            public decimal Credito { get; set; }
            public string Historico { get; set; }
            public bool Existe { get; set; }
        }

    }
}
