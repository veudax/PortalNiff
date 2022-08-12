using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ConciliacaoContabilBO
    {
        public int Proximo()
        {
            return new ConciliacaoContabilDAO.Ativo().Proximo();
        }

        public List<ConciliacaoContabil.Ativo.Resumo> Listar(int idEmpresa, string empresa, string referenciaIni, string referenciaAtual,
                                                     DateTime inicio, DateTime fim, int plano, bool naoConfirmados, bool consolidar)

        {
            return new ConciliacaoContabilDAO.Ativo().Listar(idEmpresa, empresa, referenciaIni, referenciaAtual, inicio, fim, plano, naoConfirmados, consolidar);
        }

        public List<ConciliacaoContabil.Ativo.DetalheATF> ListarDetalhe(int idEmpresa, string empresa, string referenciaIni, string referenciaAtual,
                                                                 DateTime inicio, DateTime fim, int plano, bool consolidar)
        {
            return new ConciliacaoContabilDAO.Ativo().ListarDetalheATF(idEmpresa, empresa, referenciaIni, referenciaAtual, inicio, fim, plano, consolidar);
        }

        public List<ConciliacaoContabil.Ativo.DetalheCTB> ListarDetalhe(int idEmpresa, string empresa, string referenciaIni, string referenciaAtual, int plano, bool consolidar)
        {
            return new ConciliacaoContabilDAO.Ativo().ListarDetalheCTB(idEmpresa, empresa, referenciaIni, referenciaAtual, plano, consolidar);
        }

        public List<ConciliacaoContabil.Ativo.DetalheESF> ListarDetalheESF(string empresa, DateTime dataIni, DateTime dataFim, bool consolidar)
        {
            return new ConciliacaoContabilDAO.Ativo().ListarDetalheESF(empresa, dataIni, dataFim, consolidar);
        }

        public bool Gravar(List<ConciliacaoContabil.Ativo.Resumo> _lista)
        {
            return new ConciliacaoContabilDAO.Ativo().Gravar(_lista);
        }

        public List<ConciliacaoContabil.Ativo.ItemAtivo> Listar(int idEmpresa, bool consolidar)
        {
            return new ConciliacaoContabilDAO.Ativo().Listar(idEmpresa, consolidar);
        }

        public ConciliacaoContabil.Ativo.ItemAtivo Consultar(int idEmpresa, int codigo, bool consolidar, string empresaGlobus)
        {
            return new ConciliacaoContabilDAO.Ativo().Consultar (idEmpresa,codigo, consolidar, empresaGlobus);
        }

        public List<ConciliacaoContabil.Ativo.Parametros> ListarParametros(int idEmpresa, bool paraTelaDePesquisa, bool consolidar)
        {
            return new ConciliacaoContabilDAO.Ativo().ListarParametros(idEmpresa, paraTelaDePesquisa, consolidar);
        }

        public bool Gravar(List<ConciliacaoContabil.Ativo.Parametros> _lista)
        {
            return new ConciliacaoContabilDAO.Ativo().Gravar(_lista);
        }

        public bool ExcluirItem(int id)
        {
            return new ConciliacaoContabilDAO.Ativo().ExcluirItem(id);
        }

        public bool ExcluirTudo(int codigo, int idEmpresa)
        {
            return new ConciliacaoContabilDAO.Ativo().ExcluirTudo(codigo, idEmpresa);
        }

        public List<ConciliacaoContabil.Bancaria.Resumo> Listar(string empresa, int plano,
                                                                    string referencia, string referenciaIni, string referenciaAnterior,
                                                                    DateTime dataIni, DateTime dataFin, bool consolidar, bool naoConfirmados, int idEmpresa)
        {
            return new ConciliacaoContabilDAO.Bancaria().Listar(empresa, plano, referencia, referenciaIni, referenciaAnterior
                                                               , dataIni, dataFin, consolidar, naoConfirmados, idEmpresa);
        }

        public List<ConciliacaoContabil.Bancaria.Detalhe> Listar(string empresa, int plano,
                                                                    DateTime dataIni, DateTime dataFin, bool consolidar)
        {
            return new ConciliacaoContabilDAO.Bancaria().Listar(empresa, plano, dataIni, dataFin, consolidar);
        }

        public bool Gravar(List<ConciliacaoContabil.Bancaria.Resumo> _lista)
        {
            return new ConciliacaoContabilDAO.Bancaria().Gravar(_lista);
        }

        public List<ConciliacaoContabil.Fornecedores.Resumo> Listar(string empresa, int plano,
                                                        string referencia, string referenciaIni, string tipoDocto,
                                                        string classIni, string classFin,
                                                        DateTime dataEntrada, DateTime dataEmissao, bool consolidar, bool naoConfirmados,
                                                        int idEmpresa, bool incluirDoctoSubstituidos)
        {
            return new ConciliacaoContabilDAO.Fornecedores().Listar(empresa, plano, referencia, referenciaIni, tipoDocto
                                                                   , classIni, classFin, dataEntrada, dataEmissao, consolidar
                                                                   , naoConfirmados, idEmpresa, incluirDoctoSubstituidos);
        }

        public List<ConciliacaoContabil.Fornecedores.Detalhe> ListarDetalhes(string empresa, int plano,
                                                                                string tipoDocto,
                                                                                string classIni, string classFin,
                                                                                DateTime dataEntrada, DateTime dataEmissao, bool consolidar, bool naoConfirmados,
                                                                                int idEmpresa, bool incluirDoctoSubstituidos)
        {
            return new ConciliacaoContabilDAO.Fornecedores().ListarDetalhes(empresa, plano, tipoDocto
                                                                   , classIni, classFin, dataEntrada, dataEmissao, consolidar
                                                                   , naoConfirmados, idEmpresa, incluirDoctoSubstituidos);
        }

        public List<ConciliacaoContabil.Fornecedores.FornAssociados> ListarFornecedores(int plano)
        {
            return new ConciliacaoContabilDAO.Fornecedores().ListarFornecedores(plano);
        }

        public bool Gravar(List<ConciliacaoContabil.Fornecedores.Resumo> _lista)
        {
            return new ConciliacaoContabilDAO.Fornecedores().Gravar(_lista);
        }

        public List<ConciliacaoContabil.Clientes.Resumo> ListarClientes(string empresa, int plano,
                                                        string referencia, string referenciaIni, string tipoDocto,
                                                        string classIni, string classFin,
                                                        DateTime dataEntrada, DateTime dataEmissao, bool consolidar, bool naoConfirmados,
                                                        int idEmpresa, bool incluirDoctoSubstituidos)
        {
            return new ConciliacaoContabilDAO.Clientes().Listar(empresa, plano, referencia, referenciaIni, tipoDocto
                                                                   , classIni, classFin, dataEntrada, dataEmissao, consolidar
                                                                   , naoConfirmados, idEmpresa, incluirDoctoSubstituidos);
        }

        public List<ConciliacaoContabil.Clientes.Detalhe> ListarDetalhesClientes(string empresa, int plano,
                                                                                string tipoDocto,
                                                                                string classIni, string classFin,
                                                                                DateTime dataEntrada, DateTime dataEmissao, bool consolidar, bool naoConfirmados,
                                                                                int idEmpresa, bool incluirDoctoSubstituidos)
        {
            return new ConciliacaoContabilDAO.Clientes().ListarDetalhes(empresa, plano, tipoDocto
                                                                   , classIni, classFin, dataEntrada, dataEmissao, consolidar
                                                                   , naoConfirmados, idEmpresa, incluirDoctoSubstituidos);
        }

        public List<ConciliacaoContabil.Clientes.CliAssociados> ListarClientes(int plano)
        {
            return new ConciliacaoContabilDAO.Clientes().ListarClientes(plano);
        }

        public bool Gravar(List<ConciliacaoContabil.Clientes.Resumo> _lista)
        {
            return new ConciliacaoContabilDAO.Clientes().Gravar(_lista);
        }
    }
}
