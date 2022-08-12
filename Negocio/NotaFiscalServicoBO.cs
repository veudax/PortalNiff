using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NotaFiscalServicoBO
    {
        #region parametros

        public List<ParametrosCodigoServico> Listar(int idEmpresa, bool servicoUnico)
        {
            return new NotaFiscalServicoDAO().Listar(idEmpresa, servicoUnico);
        }

        public ParametrosCodigoServico Consultar(int idEmpresa, string codigoXml)
        {
            return new NotaFiscalServicoDAO().Consultar(idEmpresa, codigoXml);
        }

        public bool Gravar(List<ParametrosCodigoServico> _lista)
        {
            return new NotaFiscalServicoDAO().Gravar(_lista);
        }

        public bool ExcluirItem(int id)
        {
            return new NotaFiscalServicoDAO().ExcluirItem(id);
        }

        public bool ExcluirTudo(int idEmpresa)
        {
            return new NotaFiscalServicoDAO().ExcluirTudo(idEmpresa);
        }

        #endregion


        public List<Arquivei> ListarXmlNFSe(string empresa, DateTime inicial, DateTime final)
        {
            return new NotaFiscalServicoDAO().ListarXmlNFSe(empresa, inicial, final);
        }

        public List<NotasFiscaisServico> ListarNotaFiscais(string tipoDocumento, string empresa, string tipo, DateTime inicial, DateTime final, bool integradas, int idParametro)
        {
            return new NotaFiscalServicoDAO().ListarNotaFiscais(tipoDocumento, empresa, tipoDocumento, inicial, final, integradas, idParametro);
        }

        public List<ItensNotasFiscaisServico> ListarItensNotaFiscais(string tipoDocumento, string empresa, string tipo, DateTime inicial, DateTime final, bool integradas)
        {
            return new NotaFiscalServicoDAO().ListarItensNotaFiscais(tipoDocumento, empresa, tipoDocumento, inicial, final, integradas);
        }

        public bool Integrar(string tipoDocumento, List<NotasFiscaisServico> notas, List<ItensNotasFiscaisServico> itens)
        {
            return new NotaFiscalServicoDAO().Integrar(tipoDocumento, notas, itens);
        }

        public List<NotasFiscaisServico> ListarNotaFiscaisIntegradas(string tipoDocumento, string empresa, string tipo, DateTime inicial, DateTime final, bool integradas)
        {
            return new NotaFiscalServicoDAO().ListarNotaFiscaisIntegradas(tipoDocumento, empresa, tipoDocumento, inicial, final, integradas);
        }

        public List<ItensNotasFiscaisServico> ListarItensNotaFiscaisIntegrados(string tipoDocumento, string empresa, string tipo, DateTime inicial, DateTime final, bool integradas)
        {
            return new NotaFiscalServicoDAO().ListarItensNotaFiscaisIntegrados(tipoDocumento, empresa, tipoDocumento, inicial, final, integradas);
        }

        public List<NotasFiscaisServico> ListarNotaFiscaisEscrituracao(string tipoDocumento, string empresa, string tipo, DateTime inicial, DateTime final, int idParametro)
        {
            return new NotaFiscalServicoDAO().ListarNotaFiscaisEscrituracao(tipoDocumento, empresa, tipoDocumento, inicial, final, idParametro);
        }

        public List<ItensNotasFiscaisServico> ListarItensNotaFiscaisEscrituracao(string tipoDocumento, string empresa, string tipo, DateTime inicial, DateTime final)
        {
            return new NotaFiscalServicoDAO().ListarItensNotaFiscaisEscrituracao(tipoDocumento, empresa, tipoDocumento, inicial, final);
        }

        public bool Conferir(List<NotasFiscaisServico> notas, List<ItensNotasFiscaisServico> itens)
        {
            return new NotaFiscalServicoDAO().Conferir(notas, itens);
        }

        public bool Revogar(List<NotasFiscaisServico> notas)
        {
            return new NotaFiscalServicoDAO().Revogar(notas);
        }

        public bool AtualizaStatusNFArquivei(List<Arquivei> arquivei)
        {
            return new NotaFiscalServicoDAO().AtualizaStatusNFArquivei(arquivei);
        }
    }
}
