using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NotaFiscalEscrituracaoGlobusBO
    {
        public List<NotasFiscaisEscrituracaoGlobus> Consultar(string numero, string tipoDocumento, string empresa, string tipoNF)
        {
            return new NotaFiscalEscrituracaoGlobusDAO().Consultar(numero, tipoDocumento, empresa, tipoNF);
        }

        public List<NotasFiscaisEscrituracaoGlobus> ConsultarEstoque(string numero, string tipoDocumento, string empresa, string tipoNF)
        {
            return new NotaFiscalEscrituracaoGlobusDAO().ConsultarEstoque(numero, tipoDocumento, empresa, tipoNF);
        }

        public List<ItensNotasFiscaisEscrituracaoGlobus> ListarItens(int id)
        {
            return new NotaFiscalEscrituracaoGlobusDAO().ListarItens(id);
        }

        public bool Gravar(string tipo, NotasFiscaisEscrituracaoGlobus notas, List<ItensNotasFiscaisEscrituracaoGlobus> itens)
        {
            return new NotaFiscalEscrituracaoGlobusDAO().Gravar(tipo, notas, itens);
        }

        public bool Cancelar(string tipo, NotasFiscaisEscrituracaoGlobus notas)
        {
            return new NotaFiscalEscrituracaoGlobusDAO().Cancelar(tipo, notas);
        }
    }
}
