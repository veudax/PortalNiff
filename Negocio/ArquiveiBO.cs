using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArquiveiBO
    {
        public List<Arquivei> Listar(DateTime Inicio, DateTime Fim, int IdEmpresa)
        {
            return new ArquiveiDAO().Listar(Inicio, Fim, IdEmpresa);
        }

        public Arquivei Consultar(int IdEmpresa, string Chave, int Id)
        {
            return new ArquiveiDAO().Consulta(IdEmpresa, Chave, Id);
        }

        public Arquivei Consultar(int IdEmpresa, string nota, string CNPJEmitente)
        {
            return new ArquiveiDAO().Consulta(IdEmpresa, nota, CNPJEmitente);
        }

        public List<Arquivei> Importados()
        {
            return new ArquiveiDAO().Importados();
        }

        public List<CFOPEmitidas> ListarCFOPEmitidas(int idEmpresa)
        {
            return new ArquiveiDAO().ListarCFOPEmitidas(idEmpresa);
        }

        public bool GravarCFOPEmitidas(CFOPEmitidas _cfop)
        {
            return new ArquiveiDAO().GravaCFOPEmitidas(_cfop);
        }

        public bool GravarSerieEmitidas(int idEmpresa, string serieG, string serieC)
        {
            return new ArquiveiDAO().GravaSerieEmitidas(idEmpresa, serieG, serieC);
        }

        public bool GravarNatureza(string naturezaOld, string naturezaNew)
        {
            return new ArquiveiDAO().GravaNatureza(naturezaOld, naturezaNew);
        }

        public bool ExcluirCFOPEmitidas(int Id)
        {
            return new ArquiveiDAO().ExcluiCFOPEmitidas(Id);
        }

        public List<CFOPeCST> ListarCFOP(string _tipo)
        {
            return new ArquiveiDAO().ListarCFOP(_tipo);
        }

        public CFOPeCST ConsultaCFOP(int codigo)
        {
            return new ArquiveiDAO().ConsultaCFOP(codigo);
        }

        public bool GravarCFOP(CFOPeCST _cfop)
        {
            return new ArquiveiDAO().GravaCFOP(_cfop);
        }

        public bool ExcluirCFOP(int Id)
        {
            return new ArquiveiDAO().ExcluiCFOP(Id);
        }

        public List<NotasArquivei> ListarParaComparar(int IdEmpresa,  DateTime Inicio, DateTime fim, string Conferidas, string tipoProcessamento, bool dataEntrada = false)
        {
            return new ArquiveiDAO().ListarParaComparar(IdEmpresa, Inicio, fim, Conferidas, dataEntrada, tipoProcessamento);
        }

        public bool GravarStatus(List<Arquivei> arquivei)
        {
            return new ArquiveiDAO().GravaStatus(arquivei);
        }

        public bool Gravar(List<Arquivei> arquivei, List<ItensArquivei> itens, bool Conferir = false)
        {
            return new ArquiveiDAO().Grava(arquivei, itens, Conferir);
        }

        public bool Gravar(List<ItensArquivei> itens)
        {
            return new ItensArquiveiDAO().Grava(itens);
        }

        public List<ItensComparacao> ListarItensArquivei (int IdEmpresa, DateTime Inicio, DateTime fim, int IdParametro, string tipoProcessamento, bool dataEntrada = false)
        {
            List<ItensComparacao> _itens = new ItensArquiveiDAO().ListarItensArquivei(IdEmpresa, Inicio, fim, dataEntrada, tipoProcessamento);

            List<ItensComparacao> _lista = new ItensArquiveiDAO().ListarItensGlobus(IdEmpresa, Inicio, fim, dataEntrada, tipoProcessamento); 

            List<Classes.ItensParametrosArquivei> _validacaoItens = new ItensParametrosArquiveiDAO().Listar(IdParametro);

            bool encontrado = false;
            foreach (var item in _itens.OrderBy(o => o.IdArquivei))
            {
                encontrado = false;

                //foreach (var glob in _lista.Where(w => w.ValorTotalGlobus == item.ValorTotal && w.IdArquivei == item.IdArquivei && w.CFOPGlobus == item.CFOPComparar && !w.Copiado))
                foreach (var glob in _lista.Where(w => w.ValorTotalGlobus == item.ValorTotal && w.IdArquivei == item.IdArquivei && item.CFOPComparar.Contains(w.CFOPGlobus.ToString()) && !w.Copiado))
                {
                    encontrado = true;

                    item.AliquotaICMSGlobus = glob.AliquotaICMSGlobus;
                    item.ValorTotalGlobus = glob.ValorTotalGlobus;
                    item.ValorIPIGlobus = glob.ValorIPIGlobus;
                    item.ValorICMSSubGlobus = glob.ValorICMSSubGlobus;
                    item.ValorICMSGlobus = glob.ValorICMSGlobus;
                    item.ValorFreteGlobus = glob.ValorFreteGlobus;
                    item.SeguroGlobus = glob.Seguro;
                    item.OutrasDespesasGlobus = glob.OutrasDespesasGlobus;
                    item.DescontoGlobus = glob.DescontoGlobus;
                    item.OperacaoGlobus = glob.OperacaoGlobus;
                    item.CSTGlobus = glob.CSTGlobus;
                    item.CFOPGlobus = glob.CFOPGlobus;
                    item.CodIntNf = glob.CodIntNf;
                    item.ValorCofinsGlobus = glob.ValorCofinsGlobus;
                    item.ValorPisGlobus = glob.ValorPisGlobus;
                    item.ValorISSGlobus = glob.ValorISSGlobus;
                    item.Descricao = glob.Descricao;
                    glob.Copiado = true;
                    item.Copiado = true;
                    break;
                }
                item.Encontrado = encontrado;
            }

            foreach (var item in _itens.Where(w => !w.Copiado).OrderBy(o => o.IdArquivei))
            {
                encontrado = false;
                decimal centavos = (decimal)0.01;

                foreach (var glob in _lista.Where(w => (w.ValorTotalGlobus == item.ValorTotal || // valore igual
                                                        w.ValorTotalGlobus == item.ValorTotal - centavos || // subtrai 1 centavo
                                                        w.ValorTotalGlobus == item.ValorTotal + centavos) // soma 1 centavo
                                                     && w.IdArquivei == item.IdArquivei && !w.Copiado))
                {
                    encontrado = true;

                    item.AliquotaICMSGlobus = glob.AliquotaICMSGlobus;
                    item.ValorTotalGlobus = glob.ValorTotalGlobus;
                    item.ValorIPIGlobus = glob.ValorIPIGlobus;
                    item.ValorICMSSubGlobus = glob.ValorICMSSubGlobus;
                    item.ValorICMSGlobus = glob.ValorICMSGlobus;
                    item.ValorFreteGlobus = glob.ValorFreteGlobus;
                    item.SeguroGlobus = glob.Seguro;
                    item.OutrasDespesasGlobus = glob.OutrasDespesasGlobus;
                    item.DescontoGlobus = glob.DescontoGlobus;
                    item.OperacaoGlobus = glob.OperacaoGlobus;
                    item.CSTGlobus = glob.CSTGlobus;
                    item.CFOPGlobus = glob.CFOPGlobus;
                    item.CodIntNf = glob.CodIntNf;
                    item.ValorCofinsGlobus = glob.ValorCofinsGlobus;
                    item.ValorPisGlobus = glob.ValorPisGlobus;
                    item.ValorISSGlobus = glob.ValorISSGlobus;
                    item.Descricao = glob.Descricao;
                    glob.Copiado = true;
                    break;
                }
                item.Encontrado = encontrado;
            }

            foreach (var item in _itens.Where(w => !w.Encontrado).OrderBy(o => o.IdArquivei).OrderBy(o => o.ValorTotal))
            {
                //ref. chamado 201912-0095
                // soma os itens não copiados do globus para comparar com o não encontrado do arquivei. se a soma for igual irá considerar senão traz o primeiro itens.
                // tentei usar o ncm que vem no arquivei porem o do material não confere.

                encontrado = false;
                decimal totalItens = _lista.Where(w => !w.Copiado && w.IdArquivei == item.IdArquivei).Sum(s => s.ValorTotalGlobus);

                decimal centavos = (decimal)0.01;

                if ((totalItens == item.ValorTotal || // valor igual
                     totalItens == item.ValorTotal - centavos || // subtrai 1 centavo
                     totalItens == item.ValorTotal + centavos))
                {
                    encontrado = true;
                    foreach (var glob in _lista.Where(w => !w.Copiado && w.IdArquivei == item.IdArquivei).OrderBy(o => o.ValorTotalGlobus))
                    {
                        glob.Copiado = true;
                        item.ValorTotalGlobus = item.ValorTotalGlobus + glob.ValorTotalGlobus;
                        item.ValorIPIGlobus = item.ValorIPIGlobus + glob.ValorIPIGlobus;
                        item.ValorICMSSubGlobus = item.ValorICMSSubGlobus + glob.ValorICMSSubGlobus;
                        item.ValorICMSGlobus = item.ValorICMSGlobus + glob.ValorICMSGlobus;
                        item.ValorFreteGlobus = item.ValorFreteGlobus + glob.ValorFreteGlobus;
                        item.SeguroGlobus = item.SeguroGlobus + glob.Seguro;
                        item.OutrasDespesasGlobus = item.OutrasDespesasGlobus + glob.OutrasDespesasGlobus;
                        item.DescontoGlobus = item.DescontoGlobus + glob.DescontoGlobus;
                        item.ValorCofinsGlobus = item.ValorCofinsGlobus + glob.ValorCofinsGlobus;
                        item.ValorPisGlobus = item.ValorPisGlobus + glob.ValorPisGlobus;
                        item.ValorISSGlobus = item.ValorISSGlobus + glob.ValorISSGlobus;
                        item.Descricao = item.Descricao + " _e_ " + glob.Descricao;

                        item.OperacaoGlobus = glob.OperacaoGlobus;
                        item.CSTGlobus = glob.CSTGlobus;
                        item.CodIntNf = glob.CodIntNf;
                        item.CFOPGlobus = glob.CFOPGlobus;
                        item.AliquotaICMSGlobus = glob.AliquotaICMSGlobus;
                        glob.Copiado = true;
                    }
                    item.Encontrado = encontrado;
                }
            }

            foreach (var item in _itens.Where(w => !w.Encontrado).OrderBy(o => o.IdArquivei).OrderBy(o => o.ValorTotal))
            {
                foreach (var glob in _lista.Where(w => !w.Copiado && w.IdArquivei == item.IdArquivei).OrderBy(o => o.ValorTotalGlobus))
                {
                    encontrado = true;
                    item.ValorTotalGlobus = glob.ValorTotalGlobus;
                    item.ValorIPIGlobus = glob.ValorIPIGlobus;
                    item.ValorICMSSubGlobus = glob.ValorICMSSubGlobus;
                    item.ValorICMSGlobus = glob.ValorICMSGlobus;
                    item.ValorFreteGlobus = glob.ValorFreteGlobus;
                    item.SeguroGlobus = glob.Seguro;
                    item.OutrasDespesasGlobus = glob.OutrasDespesasGlobus;
                    item.OperacaoGlobus = glob.OperacaoGlobus;
                    item.DescontoGlobus = glob.DescontoGlobus;
                    item.CSTGlobus = glob.CSTGlobus;
                    item.CodIntNf = glob.CodIntNf;
                    item.CFOPGlobus = glob.CFOPGlobus;
                    item.AliquotaICMSGlobus = glob.AliquotaICMSGlobus;
                    item.ValorCofinsGlobus = glob.ValorCofinsGlobus;
                    item.ValorPisGlobus = glob.ValorPisGlobus;
                    item.ValorISSGlobus = glob.ValorISSGlobus;
                    item.Descricao = glob.Descricao;
                    glob.Copiado = true;
                    break;
                }
                item.Encontrado = encontrado;
            }

            foreach (var _tipo in _itens.OrderBy(o => o.IdArquivei))
            {
                _tipo.ComDiferencas = false;
                if (_validacaoItens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.OperacaoFiscal, "")).Count() != 0)
                {
                    _tipo.OperacaoValido = _tipo.Operacao.Contains(_tipo.OperacaoGlobus.ToString());
                    _tipo.ComDiferencas = _tipo.ComDiferencas || !_tipo.OperacaoValido;
                }

                if (_validacaoItens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.CSTICMS, "")).Count() != 0)
                {
                    _tipo.CSTValido = _tipo.CSTComparar.Contains(_tipo.CSTGlobus.ToString());
                    _tipo.ComDiferencas = _tipo.ComDiferencas || !_tipo.CSTValido;
                }

                if (_validacaoItens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.CFOP, "")).Count() != 0)
                {
                    _tipo.CFOPValido = _tipo.CFOPComparar.Contains(_tipo.CFOPGlobus.ToString());
                    _tipo.ComDiferencas = _tipo.ComDiferencas || !_tipo.CFOPValido;
                }

                if (_validacaoItens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.OutrasDespesas, "")).Count() != 0)
                {
                    _tipo.OutrasDespesasValido = _tipo.OutrasDespesas == _tipo.OutrasDespesasGlobus;
                    _tipo.ComDiferencas = _tipo.ComDiferencas || !_tipo.OutrasDespesasValido;
                }

                if (_validacaoItens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.Seguro, "")).Count() != 0)
                {
                    _tipo.SeguroValido = _tipo.Seguro == _tipo.SeguroGlobus;
                    _tipo.ComDiferencas = _tipo.ComDiferencas || !_tipo.SeguroValido;
                }

                if (_validacaoItens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.Desconto, "")).Count() != 0)
                {
                    _tipo.DescontoValido = _tipo.Desconto == _tipo.DescontoGlobus;
                    _tipo.ComDiferencas = _tipo.ComDiferencas || !_tipo.DescontoValido;
                }

                if (_validacaoItens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorFrete, "")).Count() != 0)
                {
                    _tipo.FreteValido = _tipo.ValorFrete == _tipo.ValorFreteGlobus;
                    _tipo.ComDiferencas = _tipo.ComDiferencas || !_tipo.FreteValido;
                }

                if (_validacaoItens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorICMSST, "")).Count() != 0)
                {
                    _tipo.ICMSSTValido = _tipo.ValorICMSSub == _tipo.ValorICMSSubGlobus;
                    _tipo.ComDiferencas = _tipo.ComDiferencas || !_tipo.ICMSSTValido;
                }

                if (_validacaoItens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorICMS, "")).Count() != 0)
                {
                    _tipo.ICMSValido = _tipo.ValorICMS == _tipo.ValorICMSGlobus;
                    _tipo.ComDiferencas = _tipo.ComDiferencas || !_tipo.ICMSValido;
                }

                if (_validacaoItens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorTotal, "")).Count() != 0)
                {
                    _tipo.TotalValido = _tipo.ValorTotal == _tipo.ValorTotalGlobus;
                    _tipo.ComDiferencas = _tipo.ComDiferencas || !_tipo.TotalValido;
                }

                if (_validacaoItens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorIPI, "")).Count() != 0)
                {
                    _tipo.IPIValido = _tipo.ValorIPI == _tipo.ValorIPIGlobus;
                    _tipo.ComDiferencas = _tipo.ComDiferencas || !_tipo.IPIValido;
                }

                if (_validacaoItens.Where(w => w.ValidarCampo &&
                                              w.NomeCampo == Publicas.GetDescription(Publicas.CamposItensValidarArquivei.AliquotaICMS, "")).Count() != 0)
                {
                    _tipo.AliquotaValido = _tipo.AliquotaICMS == _tipo.AliquotaICMSGlobus;
                    _tipo.ComDiferencas = _tipo.ComDiferencas || !_tipo.AliquotaValido;
                }
            }
            return _itens;
        }

        public ImportandoArquivei ConsultarArquivo(string nomeCompleto, string arquivo)
        {
            return new ArquiveiDAO().ConsultarArquivo(nomeCompleto, arquivo);
        }

        public bool GravarImportando(ImportandoArquivei _arq)
        {
            return new ArquiveiDAO().GravaImportando(_arq);
        }

        public bool ExcluirImportando(ImportandoArquivei _arq)
        {
            return new ArquiveiDAO().ExcluirImportando(_arq);
        }

        public List<ImportandoArquivei> ListarArquivosImportador(int empresa, DateTime inicio, DateTime fim)
        {
            return new ArquiveiDAO().ListarArquivosImportador(empresa, inicio, fim);
        }

        public bool ExcluirArquivosImportandos(string nomearquivo)
        {
            return new ArquiveiDAO().ExcluirArquivosImportandos(nomearquivo);
        }

        public List<ItensArquivei> Listar(int IdArquivei, bool apenasComDiferencas)
        {
            return new ItensArquiveiDAO().Listar(IdArquivei, apenasComDiferencas);
        }

        public List<XmlNFe_Globus> ImportarNFe_XmlGlobus(int idEmpresa)
        {
            return new ArquiveiDAO().ImportaNFe_XmlGlobus(idEmpresa);
        }

        public List<SequencialNFE> SequencialNFe_Emitidas(int idEmpresa, DateTime inicio, DateTime fim)
        {
            return new ArquiveiDAO().SequencialNFe_Emitidas(idEmpresa, inicio, fim);
        }

    }
}
