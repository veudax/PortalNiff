using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ItensArquivei // NIFF_FIS_ItensArquivei
    {
        public int Id {get; set;}
        public int IdArquivei {get; set;}
        public string ChaveDeAcesso { get; set; }
        public decimal ValorICMS {get; set;}
        public decimal AliquotaICMS {get; set;}
        public decimal ValorICMSSub {get; set;}
        public decimal ValorIPI {get; set;}
        public decimal Desconto {get; set;}
        public decimal Seguro {get; set;}
        public decimal OutrasDespesas {get; set;}
        public decimal ValorFrete {get; set;}
        public string CCe {get; set;}
        public string CST {get; set;}
        public string CSTICMS {get; set;}
        public int CFOP {get; set;}
        public decimal ValorTotal { get; set; }
        public bool ComDiferencas { get; set; }
        public string CSTComparar { get; set; }
        public string Operacao { get; set; }
        public string CFOPComparar { get; set; }
        public bool Encontrado { get; set; }
        public string Origem { get; set; }
        public bool Existe { get; set; }
        // iformações que tem xlm e não no excel do arquivei
        public decimal ValorCofins { get; set; }
        public decimal ValorPis { get; set; }
        public decimal ValorIR { get; set; }
        public decimal ValorCsll { get; set; }
        public decimal ValorISS { get; set; }
        public decimal BaseCofins { get; set; }
        public decimal BasePis { get; set; }
        public decimal BaseIR { get; set; }
        public decimal BaseCsll { get; set; }
        public decimal BaseISS { get; set; }
        public decimal AliquotaCofins { get; set; }
        public decimal AliquotaPis { get; set; }
        public decimal AliquotaIR { get; set; }
        public decimal AliquotaCsll { get; set; }
        public decimal AliquotaISS { get; set; }
        public decimal BaseIPI { get; set; }
        public decimal AliquotaIPI { get; set; }
        public decimal AliquotaICMSSub { get; set; }
        public decimal ValorII { get; set; }
        public decimal ValorIOF { get; set; }
        public string NCM { get; set; }
        public decimal ValorTotalRecebido { get; set; }
    }

    public class ItensComparacao : ItensArquivei
    {
        public string NCMArquivo { get; set; }
        public string NCMGlobus { get; set; }
        public bool NCMValido { get; set; }

        public int CodIntNf { get; set; }
        public decimal ValorICMSGlobus { get; set; }
        public bool ICMSValido { get; set; }
        public decimal AliquotaICMSGlobus { get; set; }
        public bool AliquotaValido { get; set; }
        public decimal ValorICMSSubGlobus { get; set; }
        public bool ICMSSTValido { get; set; }
        public decimal ValorIPIGlobus { get; set; }
        public bool IPIValido { get; set; }
        public decimal DescontoGlobus { get; set; }
        public bool DescontoValido { get; set; }
        public decimal SeguroGlobus { get; set; }
        public bool SeguroValido { get; set; }
        public decimal OutrasDespesasGlobus { get; set; }
        public bool OutrasDespesasValido { get; set; }
        public decimal ValorFreteGlobus { get; set; }
        public bool FreteValido { get; set; }
        public int CSTGlobus { get; set; }
        public bool CSTValido { get; set; }
        public int CFOPGlobus { get; set; }
        public bool CFOPValido { get; set; }
        public int OperacaoGlobus { get; set; }
        public bool OperacaoValido { get; set; }
        public decimal ValorTotalGlobus { get; set; }
        public decimal Quantidade { get; set; }
        public bool TotalValido { get; set; }
        public bool Copiado { get; set; }

        // incluir para compara os itens novos do xml
        public decimal ValorCofinsGlobus { get; set; }
        public decimal ValorPisGlobus { get; set; }
        public decimal ValorIRGlobus { get; set; }
        public decimal ValorCsllGlobus { get; set; }
        public decimal ValorISSGlobus { get; set; }
        public decimal BaseCofinsGlobus { get; set; }
        public decimal BasePisGlobus { get; set; }
        public decimal BaseIRGlobus { get; set; }
        public decimal BaseCsllGlobus { get; set; }
        public decimal BaseISSGlobus { get; set; }
        public decimal AliquotaCofinsGlobus { get; set; }
        public decimal AliquotaPisGlobus { get; set; }
        public decimal AliquotaIRGlobus { get; set; }
        public decimal AliquotaCsllGlobus { get; set; }
        public decimal AliquotaISSGlobus { get; set; }
        public decimal BaseIPIGlobus { get; set; }
        public decimal AliquotaIPIGlobus { get; set; }
        public decimal AliquotaICMSSubGlobus { get; set; }
        public decimal ValorIIGlobus { get; set; }
        public decimal ValorIOFGlobus { get; set; }

        public string Descricao { get; set; }
        public string Lei { get; set; }
    }
}
