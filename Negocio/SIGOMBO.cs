using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class SIGOMBO
    {
        public List<SIGOM> ListarGlobus(string empresa, DateTime inicio, DateTime fim)
        {
            return new SIGOMDAO().ListarGlobus(empresa, inicio, fim);
        }

        public List<SIGOM> ListarSigon(int empresa, DateTime inicio, DateTime fim)
        {
            return new SIGOMDAO().ListarSigon(empresa, inicio, fim);
        }

        public List<SIGOM> Listar(Empresa empresa, DateTime inicio, DateTime fim)
        {
            int _empresa = (empresa.IdEmpresa == 2 ? 3 :
                           (empresa.IdEmpresa == 10 ? 5 :
                           (empresa.IdEmpresa == 7 ? 26 : 0)));

            List<SIGOM> _listaGlobus = new SIGOMDAO().ListarGlobus(empresa.CodigoEmpresaGlobus, inicio, fim);
            List<SIGOM> _listaSigom = new SIGOMDAO().ListarSigon(_empresa, inicio, fim);
            List<SIGOM> _lista = new List<SIGOM>();

            foreach (var itemGlobus in _listaGlobus.Where(w => !w.Lido).OrderBy(o => o.CodigoImportacaoGlobus))
            {
                foreach (var itemSigom in _listaSigom.Where(w => !w.Lido &&
                                                            w.CodigoLinha == itemGlobus.CodigoLinha &&
                                                            w.Prefixo == itemGlobus.Prefixo &&
                                                            w.InicioJornada == itemGlobus.InicioJornada &&
                                                            w.IdTipoPagtoSigom == itemGlobus.CodigoImportacaoGlobus)
                                                     .OrderBy(o => o.IdTipoPagtoGlobus))
                {
                    if (itemGlobus.TipoPagtoGlobus.ToUpper().StartsWith("INT") && !itemSigom.TipoPagtoSigom.ToUpper().StartsWith("INT"))
                        continue;

                    _lista.Add(new SIGOM()
                    {
                        CodigoLinha = itemGlobus.CodigoLinha,
                        Prefixo = itemGlobus.Prefixo,
                        InicioJornada = itemGlobus.InicioJornada,
                        FimJornadaSigom = itemSigom.FimJornadaSigom,
                        FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                        MotoristaSigom = itemSigom.MotoristaSigom,
                        MotoristaGlobus = itemGlobus.MotoristaGlobus,
                        IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                        IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                        TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                        TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                        QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                        QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                        ValorSigom = itemSigom.ValorSigom,
                        ValorGlobus = itemGlobus.ValorGlobus,
                        GuiaGlobus = itemGlobus.GuiaGlobus,
                        CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                        DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemSigom.QuantidadeGirosSigom,
                        DiferencaValor = itemGlobus.ValorGlobus != itemSigom.ValorSigom
                    });

                    itemGlobus.Lido = true;
                    itemSigom.Lido = true;
                }

                if (!itemGlobus.Lido)
                {
                    _lista.Add(new SIGOM()
                    {
                        CodigoLinha = itemGlobus.CodigoLinha,
                        Prefixo = itemGlobus.Prefixo,
                        InicioJornada = itemGlobus.InicioJornada,
                        FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                        MotoristaGlobus = itemGlobus.MotoristaGlobus,
                        IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                        TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                        QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                        ValorGlobus = itemGlobus.ValorGlobus,
                        GuiaGlobus = itemGlobus.GuiaGlobus,
                        CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                        DiferencaValor = true,
                        DiferencaQuantidade = true
                    });
                }
            }

            foreach (var itemSigom in _listaSigom.Where(w => !w.Lido))
            {
                _lista.Add(new SIGOM()
                {
                    CodigoLinha = itemSigom.CodigoLinha,
                    Prefixo = itemSigom.Prefixo,
                    InicioJornada = itemSigom.InicioJornada,
                    FimJornadaSigom = itemSigom.FimJornadaSigom,
                    MotoristaSigom = itemSigom.MotoristaSigom,
                    IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                    TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                    QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                    ValorSigom = itemSigom.ValorSigom,
                    DiferencaValor = true,
                    DiferencaQuantidade = true
                });
            }

            return _lista;
        }

        public bool Gravar(List<SIGOM> lista)
        {
            return new SIGOMDAO().Gravar(lista);
        }

        public List<SIGOM> ListarResumo(int empresa, DateTime inicio, DateTime fim, string tipo = "S")
        {
            return new SIGOMDAO().ListarResumo(empresa, inicio, fim, tipo);
        }
    }
}
