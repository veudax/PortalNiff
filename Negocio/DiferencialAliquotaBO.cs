using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DiferencialAliquotaBO
    {
        public List<DiferencialAliquota.Documento> Listar(int idEmpresa, string referencia, string CFOPs)
        {
            return new DiferencialAliquotaDAO().Listar(idEmpresa, referencia, CFOPs);
        }

        public List<DiferencialAliquota.Diferencial> Listar(int idEmpresa, string referencia, string CFOPs, string Aliquotas, decimal AliquotaPadrao)
        {
            return new DiferencialAliquotaDAO().Listar(idEmpresa, referencia, CFOPs, Aliquotas, AliquotaPadrao);
        }

        public List<DiferencialAliquota.Diferencial> Listar(int idEmpresa, DateTime inicio, DateTime fim, decimal aliquota, string CFOPs, string Aliquotas, decimal AliquotaPadrao)
        {
            return new DiferencialAliquotaDAO().Listar(idEmpresa, inicio, fim, aliquota, CFOPs, Aliquotas, AliquotaPadrao);
        }

        public bool Gravar(List<DiferencialAliquota.Documento> _documentos, List<DiferencialAliquota.Diferencial> _lista)
        {
            return new DiferencialAliquotaDAO().Gravar(_documentos, _lista);
        }

        public bool Excluir(int idEmpresa, string referencia)
        {
            return new DiferencialAliquotaDAO().Excluir(idEmpresa, referencia);
        }

        public decimal ValorNFEscrituracao(string Numero, string Serie, string codtpdoc, int cfop, decimal codigoForn, int codigoempresa, int codigofl, string CFOPs)
        {
            return new DiferencialAliquotaDAO().ValorNFEscrituracao(Numero, Serie, codtpdoc, cfop, codigoForn, codigoempresa, codigofl, CFOPs);
        }

        public bool Excluir(int id)
        {
            return new DiferencialAliquotaDAO().Excluir(id);
        }
    }
}
