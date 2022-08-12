using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class RateioBeneficioBO
    {
        #region Plano 

        public List<RateioBeneficios.PlanoContabil> Listar()
        {
            return new RateioBeneficiosDAO().Listar();
        }

        public RateioBeneficios.PlanoContabil Consultar(int plano)
        {
            return new RateioBeneficiosDAO().Consulta(plano);
        }
        #endregion

        #region Conta contabil Globus

        public List<RateioBeneficios.ContasContabeis> Listar(int plano)
        {
            return new RateioBeneficiosDAO().Listar(plano);
        }

        public RateioBeneficios.ContasContabeis Consultar(int plano, int conta)
        {
            return new RateioBeneficiosDAO().Consulta(plano, conta);
        }
        #endregion

        #region Parametros
        public List<RateioBeneficios.Parametros> Listar(int empresa, bool somentAtivos)
        {
            return new RateioBeneficiosDAO().Listar(empresa, somentAtivos);
        }

        public RateioBeneficios.Parametros ConsultarParametro(int empresa)
        {
            return new RateioBeneficiosDAO().Consultar(empresa);
        }

        public RateioBeneficios.Parametros ConsultarParametro(int empresa, int plano)
        {
            return new RateioBeneficiosDAO().Consultar(empresa, plano);
        }

        public List<RateioBeneficios.Custos> ListarCustosDoParametro(int id)
        {
            return new RateioBeneficiosDAO().ListarCustosDoParametro(id);
        }

        public bool Gravar(RateioBeneficios.Parametros param, List<RateioBeneficios.Custos> custos)
        {
            return new RateioBeneficiosDAO().Grava(param, custos);
        }

        public bool ExcluirParametros(int id)
        {
            return new RateioBeneficiosDAO().ExcluiParametros(id);
        }

        public bool ExcluirCustosDoParametro(int id)
        {
            return new RateioBeneficiosDAO().ExcluiCustosDoParametro(id);
        }

        #endregion

        #region Setor 

        public List<RateioBeneficios.Setor> ListarSetores()
        {
            return new RateioBeneficiosDAO().ListarSetores();
        }

        public RateioBeneficios.Setor ConsultarSetor(int setor)
        {
            return new RateioBeneficiosDAO().ConsultaSetor(setor);
        }
        #endregion

        #region Associa
        public List<RateioBeneficios.Associacao> ListarAssociacoes(int empresa, int idParam)
        {
            return new RateioBeneficiosDAO().ListarAssociacoes(empresa, idParam);
        }

        public bool Gravar(List<RateioBeneficios.Associacao> _listaParam)
        {
            return new RateioBeneficiosDAO().Grava(_listaParam);
        }

        public bool ExcluiAssociacoes(int id)
        {
            return new RateioBeneficiosDAO().ExcluiAssociacoes(id);
        }

        public bool ExcluiTodasAssociacoes(int id)
        {
            return new RateioBeneficiosDAO().ExcluiTodasAssociacoes(id);
        }
        #endregion


        #region Rateio
        public List<RateioBeneficios.RateioPercentualCustoSetor> ListarPercentual(int idRateio, int plano, string regra)
        {
            return new RateioBeneficiosDAO().ListarPercentual(idRateio, plano, regra);
        }

        public List<RateioBeneficios.RateioPercentualCustoSetor> ListarFuncionariosSetor(string empresa, DateTime data, List<RateioBeneficios.Associacao> associacoes, string funcoes)
        {
            return new RateioBeneficiosDAO().ListarFuncionariosSetor(empresa, data, associacoes, funcoes);
        }

        public List<RateioBeneficios.RateioPercentualCustoSetor> ListarFuncionariosSetorPorEvento(string empresa, DateTime data, List<RateioBeneficios.Associacao> associacoes, string funcoes, string eventos)
        {
            return new RateioBeneficiosDAO().ListarFuncionariosSetorPorEvento(empresa, data, associacoes, funcoes, eventos);
        }

        public List<RateioBeneficios.RateioPercentualCustoSetor> ListarFuncionariosComConvenio(string empresa, DateTime data, List<RateioBeneficios.Associacao> associacoes, string funcoes, bool ignorarMedico, bool IgnorarOdonto)
        {
            return new RateioBeneficiosDAO().ListarFuncionariosComConvenio(empresa, data, associacoes, funcoes, ignorarMedico, IgnorarOdonto);
        }

        public List<RateioBeneficios.ValoresRateados> ListarLancamentosCTBComCustoTransitorio(string empresa, DateTime inicio, DateTime fim, int param
                                             , List<RateioBeneficios.Associacao> associacoes
                                             , List<RateioBeneficios.RateioPercentualCustoSetor> percentuais
                                             , List<RateioBeneficios.RateioPercentualCustoSetor> percentuaisVT
                                             , List<RateioBeneficios.RateioPercentualCustoSetor> percentuaisConvenio)
        {
            RateioBeneficios.Parametros _param = new RateioBeneficiosDAO().Consultar(param);

            List<RateioBeneficios.ValoresParaRatear> _valoresARatear = new RateioBeneficiosDAO().ListarLancamentosCTBComCustoTransitorio(empresa, inicio, fim, _param.Id);

            List<RateioBeneficios.ValoresRateados> _valoresRateados = new List<RateioBeneficios.ValoresRateados>();

            decimal _valor = 0;
            decimal _total = 0;
            decimal _totalDebitos = 0;
            decimal _totalCreditos = 0;
            decimal _diferencaDebito = 0;
            decimal _diferencaCredito = 0;

            int docto = 1;
            bool _temRateio = false;
            int codconta = 0;
            string nomeconta = "";
            string conta = "";

            foreach (var item in _valoresARatear)//.Where(w => w.CodigoConta == 50416))
            {
                _temRateio = false;
                _total = 0;

                foreach (var itemA in associacoes.Where(w => w.CodConta == item.CodigoConta).OrderBy(o => o.CodigoCusto))
                {
                    codconta = itemA.CodConta;
                    nomeconta = itemA.NomeConta;
                    conta = itemA.Conta;

                    // Regra padrão
                    if (!_param.CodigoContaValeTransporte.Contains(item.CodigoConta.ToString()) &&
                        !_param.CodigoContaConvenioMedico.Contains(item.CodigoConta.ToString()) &&
                        !_param.CodigoContaConvenioOdontologio.Contains(item.CodigoConta.ToString()) )
                    {
                        foreach (var itemP in percentuais.Where(w => w.CodigoCusto == itemA.CodigoCusto))
                        {
                            _temRateio = true;

                            _valor = Math.Round((item.Valor * itemP.Percentual) / 100, 2);

                            if (itemA.CodigoCusto < 3000)
                            {
                                // só muda de centro de custo
                                _total = _total + _valor;
                                _valoresRateados.Add(new RateioBeneficios.ValoresRateados()
                                {
                                    Lote = _param.Lote,
                                    Documento = docto.ToString().PadLeft(10, '0'),
                                    CodigoConta = itemA.CodContaDestino,
                                    NomeConta = itemA.CodContaDestino + " - " + itemA.NomeContaDestino,
                                    Conta = itemA.Conta,
                                    CodigoCusto = itemA.CodigoCusto,
                                    CodigoCustoCredito = item.CodigoCusto,
                                    Debito = _valor,
                                    Credito = _valor

                                });
                            }
                            else
                            {
                                // muda de centro de custo e conta
                                _valoresRateados.Add(new RateioBeneficios.ValoresRateados()
                                {
                                    Lote = _param.Lote,
                                    Documento = docto.ToString().PadLeft(10, '0'),
                                    CodigoConta = itemA.CodContaDestino,
                                    NomeConta = itemA.CodContaDestino + " - " + itemA.NomeContaDestino,
                                    Conta = itemA.Conta,
                                    CodigoCusto = itemA.CodigoCusto,
                                    CodigoCustoCredito = item.CodigoCusto,
                                    ContraPartida = itemA.CodConta,
                                    NomeContraPartida = itemA.CodConta + " - " + itemA.NomeConta,
                                    Debito = _valor,
                                    Credito = _valor
                                });
                            }
                        }
                    }
                    else // Regra VT, Convenios
                    {
                        if (_param.RegraEspecificaVT)
                        {
                            if (_param.CodigoContaValeTransporte.Contains(item.CodigoConta.ToString()))
                            {
                                foreach (var itemP in percentuaisVT.Where(w => w.CodigoCusto == itemA.CodigoCusto))
                                {
                                    _temRateio = true;

                                    _valor = Math.Round((item.Valor * itemP.Percentual) / 100, 2);

                                    if (itemA.CodigoCusto < 3000)
                                    {
                                        // só muda de centro de custo
                                        _total = _total + _valor;
                                        _valoresRateados.Add(new RateioBeneficios.ValoresRateados()
                                        {
                                            Lote = _param.Lote,
                                            Documento = docto.ToString().PadLeft(10, '0'),
                                            CodigoConta = itemA.CodContaDestino,
                                            NomeConta = itemA.CodContaDestino + " - " + itemA.NomeContaDestino,
                                            Conta = itemA.Conta,
                                            CodigoCusto = itemA.CodigoCusto,
                                            CodigoCustoCredito = item.CodigoCusto,
                                            Debito = _valor,
                                            Credito = _valor

                                        });
                                    }
                                    else
                                    {
                                        // muda de centro de custo e conta
                                        _valoresRateados.Add(new RateioBeneficios.ValoresRateados()
                                        {
                                            Lote = _param.Lote,
                                            Documento = docto.ToString().PadLeft(10, '0'),
                                            CodigoConta = itemA.CodContaDestino,
                                            NomeConta = itemA.CodContaDestino + " - " + itemA.NomeContaDestino,
                                            Conta = itemA.Conta,
                                            CodigoCusto = itemA.CodigoCusto,
                                            CodigoCustoCredito = item.CodigoCusto,
                                            ContraPartida = itemA.CodConta,
                                            NomeContraPartida = itemA.CodConta + " - " + itemA.NomeConta,
                                            Debito = _valor,
                                            Credito = _valor
                                        });
                                    }
                                }
                            }
                        }
                        if (_param.RegraEspecificaConvenios)
                        {
                            if (_param.CodigoContaConvenioMedico.Contains(item.CodigoConta.ToString()) ||
                                _param.CodigoContaConvenioOdontologio.Contains(item.CodigoConta.ToString()))
                            {
                                foreach (var itemP in percentuaisConvenio.Where(w => w.CodigoCusto == itemA.CodigoCusto))
                                {
                                    _temRateio = true;

                                    _valor = Math.Round((item.Valor * itemP.Percentual) / 100, 2);

                                    if (itemA.CodigoCusto < 3000)
                                    {
                                        // só muda de centro de custo
                                        _total = _total + _valor;
                                        _valoresRateados.Add(new RateioBeneficios.ValoresRateados()
                                        {
                                            Lote = _param.Lote,
                                            Documento = docto.ToString().PadLeft(10, '0'),
                                            CodigoConta = itemA.CodContaDestino,
                                            NomeConta = itemA.CodContaDestino + " - " + itemA.NomeContaDestino,
                                            Conta = itemA.Conta,
                                            CodigoCusto = itemA.CodigoCusto,
                                            CodigoCustoCredito = item.CodigoCusto,
                                            Debito = _valor,
                                            Credito = _valor

                                        });
                                    }
                                    else
                                    {
                                        // muda de centro de custo e conta
                                        _valoresRateados.Add(new RateioBeneficios.ValoresRateados()
                                        {
                                            Lote = _param.Lote,
                                            Documento = docto.ToString().PadLeft(10, '0'),
                                            CodigoConta = itemA.CodContaDestino,
                                            NomeConta = itemA.CodContaDestino + " - " + itemA.NomeContaDestino,
                                            Conta = itemA.Conta,
                                            CodigoCusto = itemA.CodigoCusto,
                                            CodigoCustoCredito = item.CodigoCusto,
                                            ContraPartida = itemA.CodConta,
                                            NomeContraPartida = itemA.CodConta + " - " + itemA.NomeConta,
                                            Debito = _valor,
                                            Credito = _valor
                                        });
                                    }
                                }
                            }
                        }
                    }
                }

                if (_temRateio)
                {
                    _totalDebitos = _valoresRateados.Where(w => w.Documento == docto.ToString().PadLeft(10, '0'))
                        .Sum(s => s.Debito);

                    _totalCreditos = _valoresRateados.Where(w => w.Documento == docto.ToString().PadLeft(10, '0'))
                        .Sum(s => s.Credito);

                    if (_totalCreditos != item.Valor || _totalDebitos != item.Valor)
                    {
                        _diferencaDebito = item.Valor - _totalDebitos;
                        _diferencaCredito = item.Valor - _totalCreditos;

                        foreach (var itemD in _valoresRateados.Where(w => w.Documento == docto.ToString().PadLeft(10, '0')).OrderByDescending(o => o.CodigoCusto))
                        {
                            itemD.Credito = itemD.Credito + _diferencaCredito;
                            itemD.Debito = itemD.Debito + _diferencaDebito;
                            break;
                        }
                    }

                    docto++;
                }
            }

            return _valoresRateados;

        }

        public RateioBeneficios.Rateio ConsultarRateio(int empresa, int referencia)
        {
            return new RateioBeneficiosDAO().ConsultarRateio(empresa, referencia);
        }

        public List<RateioBeneficios.ValoresRateados> ListarRateio(int idRateio, int plano)
        {
            return new RateioBeneficiosDAO().ListarRateio(idRateio,  plano);
        }

        public bool Gravar(RateioBeneficios.Rateio _rateio, List<RateioBeneficios.RateioPercentualCustoSetor> _percentual, List<RateioBeneficios.ValoresRateados> _lista)
        {
            return new RateioBeneficiosDAO().Grava(_rateio, _percentual, _lista);
        }

        public bool ExcluirRateio(int id)
        {
            return new RateioBeneficiosDAO().ExcluirRateio(id);
        }
        #endregion
    }
}
