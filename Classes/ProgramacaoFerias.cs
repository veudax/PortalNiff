using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class PeriodoAquisitivo
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public decimal CodIntFunc { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public DateTime Limite { get; set; }
        public int Ano { get; set; }
        public string Periodo { get; set; }
        public bool Existe { get; set; }
    }

    public class ProgramacaoFerias
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int CodIntFunc { get; set; }
        public string Funcionario { get; set; }
        public int IdUsuario { get; set; }
        public int IdUsuarioAutorizacao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int QuantidadeDias { get; set; }
        public DateTime DataAutorizacao { get; set; }
        public string Status { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public bool Gozadas { get; set; }
        public bool Existe { get; set; }
        public bool VisualizadoPeloGerente { get; set; }
        public bool VisualizadoPeloCoordenador { get; set; }
        public bool VisualizadoPeloDiretor { get; set; }
        public bool Visualizado { get; set; }
        public bool IntegradoGlobus { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public DateTime IniPeriodoAquisitivo { get; set; }
        public DateTime FimPeriodoAquisitivo { get; set; }
        public DateTime Limite { get; set; }
        public string MotivoReprovacao { get; set; }
    }

    public class ProgramacaoFeriasGlobus
    {
        public decimal CodIntFunc { get; set; }
        public DateTime AquisitivoInicial { get; set; }
        public DateTime AquisitivoFinal { get; set; }
        public DateTime GozoInicial { get; set; }
        public DateTime Data { get; set; }
        public string Usuario { get; set; }
        public int DiasFerias { get; set; }
        public int IdProgramacao { get; set; }
    }
}
