using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public bool Existe { get; set; }
    }

    public class DepartamentosGerenciadosPeloColaborador
    {
        public int Id { get; set; }
        public int IdDepartamento { get; set; }
        public int IdColaborador { get; set; }
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public string NomeColaborador { get; set; }
        public bool Ativo { get; set; }
        public bool Existe { get; set; }
    }
}
