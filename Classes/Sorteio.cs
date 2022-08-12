using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Sorteio
    {
        public int Id { get; set; }
        public int CodigoInternoFuncionario { get; set; }
        public int IdColaborador { get; set; }
        public int PosicaoSorteio { get; set; }
        public int IdGrupo { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string Campeonato { get; set; }
        public bool PreEliminatoria { get; set; }
        public bool Existe { get; set; }             
    }

    public class ParticipantesSorteio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; } 
        public bool Sorteado { get; set; }
    }

}
