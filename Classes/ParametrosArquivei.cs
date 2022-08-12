using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ParametrosArquivei // NIFF_FIS_ParametrosArquivei
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public string Diretorio { get; set; }
        public string DiretorioDacte { get; set; }
        public string DiretorioExportacao { get; set; }
        public string DiretorioNFSe { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string AcaoComArquivo { get; set; }
        public bool Existe { get; set; }
    }
}
