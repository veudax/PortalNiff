using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Colaboradores// tabela Niff_ADS_Colaboradores
    {
        public int Id { get; set; }
        public decimal CodigoInternoFuncionarioGlobus { get; set; } 
        public int IdEmpresa { get; set; }
        public int IdDepartamento { get; set; }
        public int IdCargo { get; set; }
        public int IdSupervisor { get; set; }
        public int IdEscolaridade { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public DateTime DataAdmissao { get; set; }
        public DateTime DataDesligamento { get; set; }
        public decimal Salario { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
        public bool ParticipaDaAvaliacao { get; set; }
        public bool Ativo { get; set; }
        public bool Existe { get; set; }
    }
}
