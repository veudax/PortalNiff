using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EndividamentoBO
    {
        public List<Endividamento.Parametros> Listar(int idEmpresa, string consulta)
        {
            return new EndividamentoDAO().Listar(idEmpresa, consulta);
        }

        public bool Gravar(List<Endividamento.Parametros> _lista)
        {
            return new EndividamentoDAO().Gravar(_lista);
        }

        public bool EditarNumeroContrato(Endividamento.Contrato contrato)
        {
            return new EndividamentoDAO().EditarNumeroContrato(contrato);
        }

        public bool ExcluirParametros(int idEmpresa)
        {
            return new EndividamentoDAO().ExcluirParametros(idEmpresa);
        }

        public bool ExcluirFornecedor(int id)
        {
            return new EndividamentoDAO().ExcluirFornecedor(id);
        }

        public List<Endividamento.Valores> BuscarDocumentoContasPagar(Classes.Empresa empresa,
                                                              decimal codigoFornecedor, string modalidade, DateTime inicio, DateTime fim)

        {
            return new EndividamentoDAO().BuscarDocumentoContasPagar(empresa, codigoFornecedor, modalidade, inicio, fim);
        }

        public Endividamento.Valores BuscarDocumentoContasPagar(Classes.Empresa empresa,
                                                                      decimal codigoFornecedor, string modalidade, string tipo, string documento, int parcela,
                                                                      string serie)
        {
            return new EndividamentoDAO().BuscarDocumentoContasPagar(empresa, codigoFornecedor, modalidade, tipo, documento, parcela, serie);
        }


        public Endividamento.Valores Consultar(int IdEmpresa, decimal codigoFornecedor, string modalidade, string tipo, string referencia)
        {
            return new EndividamentoDAO().Consultar(IdEmpresa, codigoFornecedor, modalidade, tipo, referencia);
        }


        public List<Endividamento.Valores> Consultar(int IdEmpresa, string tipo, string referencia, Empresa empresa, DateTime _dataFim, bool consolidar)
        {
            return new EndividamentoDAO().Consultar(IdEmpresa, tipo, referencia, empresa, _dataFim, consolidar);
        }

        public Endividamento.Valores ConsultarPorId(int Id)
        {
            return new EndividamentoDAO().ConsultarPorId(Id);
        }

        public List<Endividamento.Valores> ListarValores(int id)
        {
            return new EndividamentoDAO().ListarValores(id);
        }

        public List<Endividamento.Valores> ListarValoresFuturosCancelados(int idEmpresa, string referencia)
        {
            return new EndividamentoDAO().ListarValoresFuturosCancelados(idEmpresa, referencia);
        }

        public List<Endividamento.Valores> ListarValoresDaEmpresa(int idempresa, string referencia)
        {
            return new EndividamentoDAO().ListarValoresDaEmpresa(idempresa, referencia);
        }

        public List<Endividamento.Conciliado> ListarConciliacao(int idEmpresa, string referencia)
        {
            return new EndividamentoDAO().ListarConciliacao(idEmpresa, referencia);
        }

        public List<Endividamento.Valores> ListarValorConciliado(int idEmpresa, string referencia)
        {
            return new EndividamentoDAO().ListarValorConciliado(idEmpresa, referencia);
        }

        public bool GravarConciliacao(List<Endividamento.Conciliado> _lista)
        {
            return new EndividamentoDAO().GravarConciliacao(_lista);
        }

        public bool Gravar(Endividamento.Valores _valor, List<Endividamento.Valores> _lista)
        {
            return new EndividamentoDAO().Gravar(_valor, _lista);
        }

        public bool Encerra_Cancela_Conciliacao(int idempresa, string referencia, string encerra)
        {
            return new EndividamentoDAO().Encerra_Cancela(idempresa, referencia, encerra);
        }

        public bool Excluir(int id)
        {
            return new EndividamentoDAO().Excluir(id);
        }

        public decimal SaldoContabil(string Empresa, string referencia, int idEmpresa, decimal codigoFornecedor, string modalidade, string tipo, string Total, bool consolidar)
        {
            return new EndividamentoDAO().SaldoContabil(Empresa, referencia, idEmpresa, codigoFornecedor, modalidade, tipo, Total, consolidar);
        }

        public decimal SaldoContabil(string Empresa, string referencia, decimal contaCTB, bool consolidar = false)
        {
            return new EndividamentoDAO().SaldoContabil(Empresa, referencia, contaCTB, consolidar);
        }

        public decimal ValorContasPagar(Classes.Empresa empresa, decimal codigoFornecedor, string modalidade, DateTime fim, string tipo)
        {
            return new EndividamentoDAO().ValorContasPagar(empresa, codigoFornecedor, modalidade, fim, tipo);
        }

        public decimal ConsultarJuros(int IdEmpresa, decimal codigoFornecedor, string modalidade, string tipo, DateTime fim, bool curtoPrazo)
        {
            return new EndividamentoDAO().ConsultarJuros(IdEmpresa, codigoFornecedor, modalidade, tipo, fim, curtoPrazo);
        }

        public List<Endividamento.Selic> Listar()
        {
            return new EndividamentoDAO().Listar();
        }

        public bool Gravar(List<Endividamento.Selic> _lista)
        {
            return new EndividamentoDAO().Gravar(_lista);
        }

        public bool ExcluirSelic(int id)
        {
            return new EndividamentoDAO().ExcluirSelic(id);
        }

        public List<Endividamento.Contrato> Listar(int idEmpresa, decimal codigoFornecedor, string modalidade)
        {
            return new EndividamentoDAO().Listar(idEmpresa, codigoFornecedor, modalidade);
        }

        public List<Endividamento.Contrato> Listar(int idEmpresa)
        {
            return new EndividamentoDAO().Listar(idEmpresa);
        }

        public Endividamento.Contrato Consultar(int idEmpresa, decimal codigoFornecedor, string contrato, string modalidade)
        {
            return new EndividamentoDAO().Consultar(idEmpresa, codigoFornecedor, contrato, modalidade);
        }

        public List<Endividamento.Parcelamento> ListarParcelamento(int idContrato, decimal total, bool aplicaSelic)
        {
            return new EndividamentoDAO().ListarParcelamento(idContrato, total, aplicaSelic);
        }

        public List<Endividamento.Parcelamento> ListarParcelamento(int idEmpresa, DateTime inicio, bool consolidar = false)
        {
            return new EndividamentoDAO().ListarParcelamento(idEmpresa, inicio, consolidar);
        }

        public List<Endividamento.Arquivo> Arquivos(int idContrato)
        {
            return new EndividamentoDAO().Arquivos(idContrato);
        }

        public bool Gravar(Endividamento.Contrato contrato, List<Endividamento.Parcelamento> _lista, List<Endividamento.Arquivo> _arquivos, List<Endividamento.Arquivo> _arquivosAntes)
        {
            return new EndividamentoDAO().Gravar(contrato, _lista, _arquivos, _arquivosAntes);
        }

        public bool ExcluirParcelamento(int id)
        {
            return new EndividamentoDAO().ExcluirParcelamento(id);
        }
    }
}
