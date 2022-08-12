using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class FuncionariosGlobus
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Chapa { get; set; }
        public string Nome { get; set; }
        public string Area { get; set; }
        public string Sexo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public DateTime DataAdmissao { get; set; }
        public DateTime DataDesligamento { get; set; }
        public decimal Salario { get; set; }
        public bool Existe { get; set; }
        public bool ColaboradorCadastrado { get; set; }
        public bool Marcado { get; set; }
    }
}
