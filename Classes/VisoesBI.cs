using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class VisoesBI
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public bool EspeficoDeUmaConta { get; set; }
        public bool Existe { get; set; }
        public bool Selecionado { get; set; }
    }

    public class DetalheVisoes : VisoesBI
    {
        public int IdDetalhe { get; set; }
        public string Rubricas { get; set; }
        public bool Excluir { get; set; }

    }
}
