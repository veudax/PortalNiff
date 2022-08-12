using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Telefone
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public decimal Numero { get; set; }
        public string TelefoneFormatado { get; set; }
        public string Operadora { get; set; }
        public bool Existe { get; set; }
    }

    public class Ramais
    {
        public int Id { get; set; }
        public int IdTelefone { get; set; }
        public int Numero { get; set; }
        public string Grupo { get; set; }
        public bool Existe { get; set; }
    }

    public class RamaisAssociadosAoColaborador
    {
        public int Id { get; set; }
        public int IdRamal { get; set; }
        public int IdColaborador { get; set; }
        public string NomeColaborador { get; set; }
        public string Complemento { get; set; }
        public bool Excluir { get; set; }
        public bool Existe { get; set; }
    }

    public class LocalizaRamais
    {
        public decimal Numero { get; set; }
        public string Telefone { get; set; }
        public string Grupo { get; set; }
        public string Ramal { get; set; }
        public string Nome { get; set; }
        public string Complemento { get; set; }
        public string Empresa { get; set; }
    }
}
