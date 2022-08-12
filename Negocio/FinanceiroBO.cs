using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FinanceiroBO
    {
        #region Tipo de Despesa e Receita do Globus
        public List<Financeiro.DespesaReceitaGlobus> Listar(string tipo)
        {
            return new FinanceiroDAO().Listar(tipo);
        }

        public Financeiro.DespesaReceitaGlobus Consultar(string codigo, string tipo)
        {
            return new FinanceiroDAO().Consultar(codigo, tipo);
        }

        public Financeiro.ContasContabeisDespesasReceitaGlobus Consultar(string codigo, string tipo, int plano)
        {
            return new FinanceiroDAO().Consultar(codigo, tipo, plano);
        }
        #endregion

        #region Colunas
        public List<Financeiro.Colunas> Listar(bool ApenasAtivos)
        {
            return new FinanceiroDAO().Listar(ApenasAtivos);
        }

        public Financeiro.Colunas Consultar(int Id)
        {
            return new FinanceiroDAO().Consultar(Id);
        }

        public bool Gravar(Financeiro.Colunas _dados)
        {
            return new FinanceiroDAO().Grava(_dados);
        }

        public bool Excluir(int id)
        {
            return new FinanceiroDAO().Exclui(id);
        }

        public int Proximo()
        {
            return new FinanceiroDAO().Proximo();
        }
        #endregion

        #region Bancos
        public List<Financeiro.Bancos> ListarBancos(int empresa, bool ApenasAtivos)
        {
            return new FinanceiroDAO().ListarBancos(empresa, ApenasAtivos);
        }

        public Financeiro.Bancos ConsultarBancos(int Id)
        {
            return new FinanceiroDAO().ConsultarBancos(Id);
        }

        public Financeiro.Bancos ConsultarBancos(int codigo, int empresa)
        {
            return new FinanceiroDAO().ConsultarBancos(codigo, empresa);
        }

        public int ProximoBanco(int empresa)
        {
            return new FinanceiroDAO().ProximoBanco(empresa);
        }

        public bool GravarBancos(Financeiro.Bancos _dados, List<Financeiro.ColunasDoBanco> _colunas)
        {
            return new FinanceiroDAO().GravarBancos(_dados, _colunas);
        }

        public bool ExcluirBancos(int id)
        {
            return new FinanceiroDAO().ExcluirBancos(id);
        }

        public bool ExcluirTipos(int id)
        {
            return new FinanceiroDAO().ExcluirTipos(id);
        }

        public bool ExcluirColunas(int id)
        {
            return new FinanceiroDAO().ExcluirColunas(id);
        }

        public List<Financeiro.ColunasDoBanco> ListarColunasDoBanco(int banco, bool ApenasAtivos)
        {
            return new FinanceiroDAO().ListarColunasDoBanco(banco, ApenasAtivos);
        }
        
        public List<Financeiro.DespesasReceitasDasColunasDoBanco> ListarTipoAssociados(int id, string tipo)
        {
            return new FinanceiroDAO().ListarTipoAssociados(id, tipo);
        }
        #endregion

        #region Variaveis
        public List<Financeiro.Variaveis> Listar(int empresa, bool ApenasAtivos)
        {
            return new FinanceiroDAO().Listar(empresa, ApenasAtivos);
        }

        public List<Financeiro.Variaveis> Listar(int empresa, int codigo, bool ApenasAtivos)
        {
            return new FinanceiroDAO().Listar(empresa, codigo, ApenasAtivos);
        }

        public Financeiro.Variaveis Consultar(int empresa, int codigo)
        {
            return new FinanceiroDAO().Consultar(empresa, codigo);
        }

        public Financeiro.Variaveis ConsultarVariaveisPelaEmpresaIdColuna(int empresa, int idColuna)
        {
            return new FinanceiroDAO().ConsultarVariaveisPelaEmpresaIdColuna(empresa, idColuna);
        }

        public Financeiro.Variaveis Consultar(int empresa, int codigo, int idColuna)
        {
            return new FinanceiroDAO().Consultar(empresa, codigo, idColuna);
        }

        public Financeiro.Variaveis ConsultarVariavelPeloId(int id)
        {
            return new FinanceiroDAO().ConsultarVariavelPeloId(id);
        }

        public bool Gravar(Financeiro.Variaveis _dados)
        {
            return new FinanceiroDAO().Grava(_dados);
        }

        public bool ExcluirVariavel(int id)
        {
            return new FinanceiroDAO().ExcluiVariavel(id);
        }

        public int ProximoCodigoVariavel(int empresa)
        {
            return new FinanceiroDAO().ProximoCodigoVariavel(empresa);
        }
        #endregion

        #region Demonstrativo
        public Financeiro.Demonstrativo ConsultaDemonstrativo(int empresa, int idBanco, string referencia)
        {
            return new FinanceiroDAO().ConsultaDemonstrativo(empresa, idBanco, referencia);
        }

        public Financeiro.Demonstrativo ConsultaDemonstrativo(int id)
        {
            return new FinanceiroDAO().ConsultaDemonstrativo(id);
        }

        public List<Financeiro.Demonstrativo> ListarDemonstrativo(int empresa, int idBanco)
        {
            return new FinanceiroDAO().ListarDemonstrativo(empresa, idBanco);
        }

        public List<Financeiro.ColunasDemonstrativo> ListarColunasDemonstrativo(int IdDemonstrativo)
        {
            return new FinanceiroDAO().ListarColunasDemonstrativo(IdDemonstrativo);
        }

        public List<Financeiro.HistoricoDemonstrativo> ListarHistoricoDemonstrativo(int IdDemonstrativo)
        {
            return new FinanceiroDAO().ListarHistoricoDemonstrativo(IdDemonstrativo);
        }

        public bool Gravar(Financeiro.Demonstrativo _dados, List<Financeiro.ColunasDemonstrativo> _colunas, List<Financeiro.HistoricoDemonstrativo> _historico, string[,] crc, string[,] cpg)
        {
            return new FinanceiroDAO().Gravar(_dados, _colunas, _historico, crc, cpg);
        }

        #region Previsto
        public decimal ConsultarReceitaPrevista(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual, string tipoLinha, string empresaConsolidar)
        {
            return new FinanceiroDAO().ConsultarReceitaPrevista(empresa, data, quantidade, porMeses, aumentarReduzir, percetual, tipoLinha, empresaConsolidar);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarReceitaPrevistaDetalhada(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual, string tipoLinha, string empresaConsolidar)
        {
            return new FinanceiroDAO().ConsultarReceitaPrevistaDetalhada(empresa, data, quantidade, porMeses, aumentarReduzir, percetual, tipoLinha, empresaConsolidar);
        }

        public decimal ConsultarContaReceberPrevista(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            return new FinanceiroDAO().ConsultarContaReceberPrevista(empresa, data, quantidade, porMeses, aumentarReduzir, percetual, idBanco, idColuna, empresaConsolidar, idBancoConsolidar);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarContaReceberDetalhada(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolida)
        {
            return new FinanceiroDAO().ConsultarContaReceberDetalhada(empresa, data, quantidade, porMeses, aumentarReduzir, percetual, idBanco, idColuna, empresaConsolidar, idBancoConsolida);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarContaReceberDetalhadaPeloVencimento(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolida)
        {
            return new FinanceiroDAO().ConsultarContaReceberDetalhadaPeloVencimento(empresa, data, quantidade, porMeses, aumentarReduzir, percetual, idBanco, idColuna, empresaConsolidar, idBancoConsolida);
        }

        public decimal ConsultarContaPagarBancosPrevista(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual,
            int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            return new FinanceiroDAO().ConsultarContaPagarBancosPrevista(empresa, data, quantidade, porMeses, aumentarReduzir, percetual, idBanco, idColuna, empresaConsolidar, idBancoConsolidar);
        }

        public decimal ConsultarContaReceberBancosPrevista(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual,
            int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            return new FinanceiroDAO().ConsultarContaReceberBancosPrevista(empresa, data, quantidade, porMeses, aumentarReduzir, percetual, idBanco, idColuna, empresaConsolidar, idBancoConsolidar);
        }

        public decimal ConsultarContaPagarPrevista(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            return new FinanceiroDAO().ConsultarContaPagarPrevista(empresa, data, quantidade, porMeses, aumentarReduzir, percetual, idBanco, idColuna, empresaConsolidar, idBancoConsolidar);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarContaPagarDetalhada(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            return new FinanceiroDAO().ConsultarContaPagarDetalhada(empresa, data, quantidade, porMeses, aumentarReduzir, percetual, idBanco, idColuna, empresaConsolidar, idBancoConsolidar);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarContaPagarDetalhadaPeloVencimento(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            return new FinanceiroDAO().ConsultarContaPagarDetalhadaPeloVencimento(empresa, data, quantidade, porMeses, aumentarReduzir, percetual, idBanco, idColuna, empresaConsolidar, idBancoConsolidar);
        }

        public decimal ConsultaTransferenciasPrevistas(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percetual, int codigoBCO, int codigoAgencia, string codigoConta, string Tipo)
        {
            return new FinanceiroDAO().ConsultaTransferenciasPrevistas(empresa, data, quantidade, porMeses, aumentarReduzir, percetual, codigoBCO, codigoAgencia, codigoConta, Tipo);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultaTransferenciasPrevistasDetalhada(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percentual, int codigoBCO, int codigoAgencia, string codigoConta, string Tipo)
        {
            return new FinanceiroDAO().ConsultaTransferenciasPrevistasDetalhada(empresa, data, quantidade, porMeses, aumentarReduzir, percentual, codigoBCO, codigoAgencia, codigoConta, Tipo);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultaTransferenciasPrevistasNaDataDetalhada(string empresa, DateTime data, int codbanco, int codAgencia, string codConta, string Tipo)
        {
            return new FinanceiroDAO().ConsultaTransferenciasPrevistasNaDataDetalhada(empresa, data, codbanco, codAgencia, codConta, Tipo);
        }

        public decimal ConsultaMovtoBCOPrevisto(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percentual, int codigoBCO, int codigoAgencia, string codigoConta, string Tipo, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            return new FinanceiroDAO().ConsultaMovtoBCOPrevisto(empresa, data, quantidade, porMeses, aumentarReduzir, percentual, codigoBCO, codigoAgencia, codigoConta, Tipo, idBanco, idColuna, empresaConsolidar, idBancoConsolidar);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultaMovtoBCOPrevistoDetalhada(string empresa, DateTime data, int quantidade, bool porMeses, string aumentarReduzir, decimal percentual, int codigoBCO, int codigoAgencia, string codigoConta, string Tipo, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            return new FinanceiroDAO().ConsultaMovtoBCOPrevistoDetalhada(empresa, data, quantidade, porMeses, aumentarReduzir, percentual, codigoBCO, codigoAgencia, codigoConta, Tipo, idBanco, idColuna, empresaConsolidar, idBancoConsolidar);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultaMovtoBCOPrevistoNaDataDetalhada(string empresa, DateTime data, int codbanco, int codAgencia, string codConta, string Tipo, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            return new FinanceiroDAO().ConsultaMovtoBCOPrevistoNaDataDetalhada(empresa, data, codbanco, codAgencia, codConta, Tipo, idBanco, idColuna, empresaConsolidar, idBancoConsolidar);
        }
        #endregion

        #region Realizado
        public decimal ConsultarReceitaRealizada(string empresa, DateTime data, string tipoLinha)
        {
            return new FinanceiroDAO().ConsultarReceitaRealizada(empresa, data, tipoLinha);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarReceitaRealizadaDetalhada(string empresa, DateTime data, string tipoLinha)
        {
            return new FinanceiroDAO().ConsultarReceitaRealizadaDetalhada(empresa, data, tipoLinha);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarContasReceberRealizadaDetalhada(string empresa, DateTime data, int idBanco, int idColuna)
        {
            return new FinanceiroDAO().ConsultarContasReceberRealizadaDetalhada(empresa, data, idBanco, idColuna);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarContasPagarRealizadaDetalhada(string empresa, DateTime data, int idBanco, int idColuna)
        {
            return new FinanceiroDAO().ConsultarContasPagarRealizadaDetalhada(empresa, data, idBanco, idColuna);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultarTransferenciasRealizadaDetalhada(string empresa, DateTime data, int codbanco, int codAgencia, string codConta, string tipo, string empresaConsolidar)
        {
            return new FinanceiroDAO().ConsultarTransferenciasRealizadaDetalhada(empresa, data, codbanco, codAgencia, codConta, tipo, empresaConsolidar);
        }

        public List<Classes.Financeiro.ColunasDemonstrativo> ConsultaMovtoBCORealizadoNaDataDetalhada(string empresa, DateTime data, int codbanco, int codAgencia, string codConta, string Tipo, int idBanco, int idColuna, string empresaConsolidar, int idBancoConsolidar)
        {
            return new FinanceiroDAO().ConsultaMovtoBCORealizadoNaDataDetalhada(empresa, data, codbanco, codAgencia, codConta, Tipo, idBanco, idColuna, empresaConsolidar, idBancoConsolidar);
        }
        #endregion
        #endregion

        #region Dados bancarios Globus

        public List<Financeiro.BancosGlobus> ListarBancosGlobus(string empresa)
        {
            return new FinanceiroDAO().ListarBancosGlobus(empresa);
        }

        public Financeiro.BancosGlobus ConsultarBancosGlobus(int codigo)
        {
            return new FinanceiroDAO().ConsultaBancosGlobus(codigo);
        }

        public List<Financeiro.AgenciaGlobus> ListarAgenciaGlobus(int banco, string empresa)
        {
            return new FinanceiroDAO().ListarAgenciaGlobus(banco, empresa);
        }

        public Financeiro.AgenciaGlobus ConsultarAgenciaGlobus(int banco, int agencia)
        {
            return new FinanceiroDAO().ConsultaAgenciaGlobus(banco, agencia);
        }

        public List<Financeiro.ContaGlobus> ListarContasGlobus(string empresa, int banco, int agencia)
        {
            return new FinanceiroDAO().ListarContasGlobus(empresa, banco, agencia);
        }

        public Financeiro.ContaGlobus ConsultarContasGlobus(string empresa, int banco, int agencia, string conta)
        {
            return new FinanceiroDAO().ConsultaContasGlobus(empresa, banco, agencia, conta);
        }
        #endregion

        #region Resumo
        public List<Financeiro.Resumo> ListarResumo(string referencia)
        {
            return new FinanceiroDAO().ListarResumo(referencia);
        }
        #endregion
    }
}
