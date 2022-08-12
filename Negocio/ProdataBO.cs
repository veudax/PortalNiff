using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProdataBO
    {
        public List<SIGOM> ListarProdata(int empresa, DateTime inicio, DateTime fim)
        {
            return new ProdataDAO().ListarProdata(empresa, inicio, fim);
        }

        public List<SIGOM> ListarGlobus(string empresa, DateTime inicio, DateTime fim)
        {
            return new ProdataDAO().ListarGlobus(empresa, inicio, fim);
        }

        public List<SIGOM> ListarTipos(string empresa)
        {
            return new ProdataDAO().ListarTipos(empresa);
        }

        //public List<SIGOM> Comparar(Empresa empresa, DateTime inicio, DateTime fim)
        //{
        //    int _empresa = (empresa.IdEmpresa == 2 ? 3 :
        //                   (empresa.IdEmpresa == 10 ? 5 :
        //                   (empresa.IdEmpresa == 7 ? 26 : 0)));

        //    List<SIGOM> _listaGlobus = new ProdataDAO().ListarGlobus(empresa.CodigoEmpresaGlobus, inicio, fim);
        //    List<SIGOM> _listaSigom = new ProdataDAO().ListarProdata(_empresa, inicio, fim);

        //    List<SIGOM> _listaTipos = new ProdataDAO().ListarTipos(empresa.CodigoEmpresaGlobus);

        //    List<SIGOM> _lista = new List<SIGOM>();

        //    foreach (var itemGlobus in _listaGlobus.Where(w => !w.Lido).OrderBy(o => o.TipoPagtoGlobus))
        //    {
        //        foreach (var itemT in _listaTipos.Where(w => w.IdTipoPagtoGlobus == itemGlobus.IdTipoPagtoGlobus))
        //        {
        //            foreach (var itemProdata in _listaSigom.Where(w => !w.Lido &&
        //                                                        w.CodigoLinha == itemGlobus.CodigoLinha &&
        //                                                        w.Prefixo == itemGlobus.Prefixo &&
        //                                                        w.InicioJornada == itemGlobus.InicioJornada &&
        //                                                        itemT.TipoPagtoGlobus.Contains(w.IdTipoPagtoSigom.ToString()))
        //                                                 .OrderBy(o => o.IdTipoPagtoGlobus))
        //            {
        //                //if (itemGlobus.TipoPagtoGlobus.ToUpper().StartsWith("INT") && !itemProdata.TipoPagtoSigom.ToUpper().StartsWith("INT"))
        //                  //  continue;

        //                _lista.Add(new SIGOM()
        //                {
        //                    CodigoLinha = itemGlobus.CodigoLinha,
        //                    Prefixo = itemGlobus.Prefixo,
        //                    InicioJornada = itemGlobus.InicioJornada,
        //                    FimJornadaSigom = itemProdata.FimJornadaSigom,
        //                    FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
        //                    MotoristaSigom = itemProdata.MotoristaSigom,
        //                    MotoristaGlobus = itemGlobus.MotoristaGlobus,
        //                    IdTipoPagtoSigom = itemProdata.IdTipoPagtoSigom,
        //                    IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
        //                    TipoPagtoSigom = itemProdata.TipoPagtoSigom,
        //                    TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
        //                    QuantidadeGirosSigom = itemProdata.QuantidadeGirosSigom,
        //                    QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
        //                    ValorSigom = itemProdata.ValorSigom,
        //                    ValorGlobus = itemGlobus.ValorGlobus,
        //                    GuiaGlobus = itemGlobus.GuiaGlobus,
        //                    CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
        //                    DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemProdata.QuantidadeGirosSigom,
        //                    DiferencaValor = itemGlobus.ValorGlobus != itemProdata.ValorSigom
        //                });

        //                itemGlobus.Lido = true;
        //                itemProdata.Lido = true;
        //            }

        //        }


        //        if (!itemGlobus.Lido)
        //        {
        //            _lista.Add(new SIGOM()
        //            {
        //                CodigoLinha = itemGlobus.CodigoLinha,
        //                Prefixo = itemGlobus.Prefixo,
        //                InicioJornada = itemGlobus.InicioJornada,
        //                FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
        //                MotoristaGlobus = itemGlobus.MotoristaGlobus,
        //                IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
        //                TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
        //                QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
        //                ValorGlobus = itemGlobus.ValorGlobus,
        //                GuiaGlobus = itemGlobus.GuiaGlobus,
        //                CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
        //                DiferencaValor = true,
        //                DiferencaQuantidade = true
        //            });
        //        }
        //    }

        //    foreach (var itemSigom in _listaSigom.Where(w => !w.Lido))
        //    {
        //        _lista.Add(new SIGOM()
        //        {
        //            CodigoLinha = itemSigom.CodigoLinha,
        //            Prefixo = itemSigom.Prefixo,
        //            InicioJornada = itemSigom.InicioJornada,
        //            FimJornadaSigom = itemSigom.FimJornadaSigom,
        //            MotoristaSigom = itemSigom.MotoristaSigom,
        //            IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
        //            TipoPagtoSigom = itemSigom.TipoPagtoSigom,
        //            QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
        //            ValorSigom = itemSigom.ValorSigom,
        //            DiferencaValor = true,
        //            DiferencaQuantidade = true
        //        });
        //    }

        //    return _lista;
        //}
    }
}
