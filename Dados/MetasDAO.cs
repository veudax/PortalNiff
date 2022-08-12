using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class MetasDAO
    {
        IDataReader dataReader;

        public List<Metas> Listar(bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Metas> _lista = new List<Metas>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idmetas, ativo, tipo, perspectiva, descricao, TextoBI, Formula, RegraFormula");

                query.Append("     , usaparaavaliacao, usaparagestao,  previstocalcularpor,  previstoqtdmes, previstoaplicadiasuteis");
                query.Append("     , previstopermitealterar,  realizadopermitealterar, idbi, UsaKmRodado, UsarColunaPrevistoParaCalculo ");
                query.Append("     , QtdeDecimais, ExibirNoDRE, GrupoTotalizador, FormulaTotalizador, NivelCalculo, TipoFrota");

                query.Append("  from Niff_Ads_Metas c     ");
                if (apenasAtivos)
                    query.Append(" Where ativo = 'S'");                
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Metas _tipo = new Metas();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idmetas"].ToString());
                        _tipo.NivelCalculo = Convert.ToInt32(dataReader["NivelCalculo"].ToString());                        

                        _tipo.Descricao = dataReader["Descricao"].ToString();

                        _tipo.Tipo = (dataReader["Tipo"].ToString() == "C" ? Publicas.TipoDeMetas.Crescimento : Publicas.TipoDeMetas.Resultado);

                        _tipo.Perspectiva = (dataReader["perspectiva"].ToString() == "C" ? Publicas.Perspectivas.Cliente :
                                            (dataReader["perspectiva"].ToString() == "P" ? Publicas.Perspectivas.Processos :
                                            (dataReader["perspectiva"].ToString() == "F" ? Publicas.Perspectivas.Financeira : Publicas.Perspectivas.Aprendizagem)));
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.ExibeNoDRE = dataReader["ExibirNoDRE"].ToString() == "S";
                        _tipo.GrupoTotalizador = dataReader["GrupoTotalizador"].ToString() == "S";
                        
                        _tipo.TextoBI = dataReader["TextoBI"].ToString();
                        _tipo.FormulaTotalizador = dataReader["FormulaTotalizador"].ToString();

                        _tipo.TipoDeFrota = dataReader["TipoFrota"].ToString();

                        _tipo.Formula = dataReader["Formula"].ToString();
                        _tipo.Regra = (dataReader["RegraFormula"].ToString() == "=" ? Publicas.RegraFormulaMetas.Igual :
                                      (dataReader["RegraFormula"].ToString() == ">" ? Publicas.RegraFormulaMetas.MaiorMelhor : Publicas.RegraFormulaMetas.MenorMelhor));
                            
                        _tipo.UsaNaAvaliacao = dataReader["usaparaavaliacao"].ToString() == "S";
                        _tipo.UsaNaGestao = dataReader["usaparagestao"].ToString() == "S";
                        _tipo.PrevistoAplicaDiasUteis = dataReader["previstoaplicadiasuteis"].ToString() == "S";
                        _tipo.PrevistoPermiteAlterar = dataReader["previstopermitealterar"].ToString() == "S";
                        _tipo.RealizadoPermiteAlterar = dataReader["realizadopermitealterar"].ToString() == "S";
                        _tipo.UsaKMRodado = dataReader["UsaKmRodado"].ToString() == "S";
                        _tipo.UsarColunaPrevistoParaCalculo = dataReader["UsarColunaPrevistoParaCalculo"].ToString() == "S";                        

                        _tipo.PrevistoCalculaPor = dataReader["previstocalcularpor"].ToString();
                        _tipo.PrevistoQdtMeses = Convert.ToInt32(dataReader["previstoqtdmes"].ToString());
                        _tipo.QuantidadeDecimais = Convert.ToInt32(dataReader["QtdeDecimais"].ToString());

                        try
                        {
                            _tipo.IdBI = Convert.ToInt32(dataReader["IdBI"].ToString());
                        }
                        catch { }

                        _lista.Add(_tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public Metas Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Metas _tipo = new Metas();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idmetas, ativo, tipo, perspectiva, descricao, TextoBI, Formula, RegraFormula");
                query.Append("     , usaparaavaliacao, usaparagestao,  previstocalcularpor,  previstoqtdmes, previstoaplicadiasuteis");
                query.Append("     , previstopermitealterar,  realizadopermitealterar, idbi, UsaKmRodado, UsarColunaPrevistoParaCalculo ");
                query.Append("     , QtdeDecimais, ExibirNoDRE, GrupoTotalizador, FormulaTotalizador, NivelCalculo, TipoFrota");
                query.Append("  from Niff_Ads_Metas c     ");
                query.Append(" Where idmetas = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idmetas"].ToString());
                        _tipo.NivelCalculo = Convert.ToInt32(dataReader["NivelCalculo"].ToString());
                        _tipo.Descricao = dataReader["Descricao"].ToString();

                        _tipo.Tipo = (dataReader["Tipo"].ToString() == "C" ? Publicas.TipoDeMetas.Crescimento : Publicas.TipoDeMetas.Resultado);

                        _tipo.Perspectiva = (dataReader["perspectiva"].ToString() == "C" ? Publicas.Perspectivas.Cliente :
                                            (dataReader["perspectiva"].ToString() == "P" ? Publicas.Perspectivas.Processos :
                                            (dataReader["perspectiva"].ToString() == "F" ? Publicas.Perspectivas.Financeira : Publicas.Perspectivas.Aprendizagem)));
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.TextoBI = dataReader["TextoBI"].ToString();
                        _tipo.ExibeNoDRE = dataReader["ExibirNoDRE"].ToString() == "S";
                        _tipo.GrupoTotalizador = dataReader["GrupoTotalizador"].ToString() == "S";

                        _tipo.FormulaTotalizador = dataReader["FormulaTotalizador"].ToString();
                        _tipo.Formula = dataReader["Formula"].ToString();
                        _tipo.TipoDeFrota = dataReader["TipoFrota"].ToString();

                        _tipo.Regra = (dataReader["RegraFormula"].ToString() == "=" ? Publicas.RegraFormulaMetas.Igual :
                                      (dataReader["RegraFormula"].ToString() == ">" ? Publicas.RegraFormulaMetas.MaiorMelhor : Publicas.RegraFormulaMetas.MenorMelhor));

                        _tipo.UsaNaAvaliacao = dataReader["usaparaavaliacao"].ToString() == "S";
                        _tipo.UsaNaGestao = dataReader["usaparagestao"].ToString() == "S";
                        _tipo.PrevistoAplicaDiasUteis = dataReader["previstoaplicadiasuteis"].ToString() == "S";
                        _tipo.PrevistoPermiteAlterar = dataReader["previstopermitealterar"].ToString() == "S";
                        _tipo.RealizadoPermiteAlterar = dataReader["realizadopermitealterar"].ToString() == "S";
                        _tipo.UsaKMRodado = dataReader["UsaKmRodado"].ToString() == "S";
                        _tipo.UsarColunaPrevistoParaCalculo = dataReader["UsarColunaPrevistoParaCalculo"].ToString() == "S";

                        _tipo.PrevistoCalculaPor = dataReader["previstocalcularpor"].ToString();
                        _tipo.PrevistoQdtMeses = Convert.ToInt32(dataReader["previstoqtdmes"].ToString());
                        _tipo.QuantidadeDecimais = Convert.ToInt32(dataReader["QtdeDecimais"].ToString());

                        try
                        {
                            _tipo.IdBI = Convert.ToInt32(dataReader["IdBI"].ToString());
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _tipo;
        }

        public int Proximo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Nvl(Max(idmetas),0) +1 next From Niff_Ads_Metas");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        retorno = Convert.ToInt32(dataReader["next"].ToString());
                }
                return retorno;
            }
            catch
            {
                return retorno;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool Gravar(Metas tipo, List<MetasBIItens> _lista, List<MetasContasContabeis> contas, List<MetasBIItens> _listaDesmarcados)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into Niff_Ads_Metas");
                    query.Append(" ( idmetas, ativo, tipo, perspectiva, descricao, TextoBI, Formula, RegraFormula ");
                    query.Append("     , usaparaavaliacao, usaparagestao,  previstocalcularpor,  previstoqtdmes, previstoaplicadiasuteis");
                    query.Append("     , previstopermitealterar,  realizadopermitealterar, idbi, UsaKmRodado, UsarColunaPrevistoParaCalculo ");
                    query.Append("     , QtdeDecimais, ExibirNoDRE, GrupoTotalizador, FormulaTotalizador, NivelCalculo, TipoFrota )");

                    query.Append(" Values (" + tipo.Id);
                    query.Append("        ,'" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.Tipo == Publicas.TipoDeMetas.Crescimento ? "C" : "R") + "'");
                    query.Append("        ,'" + (tipo.Perspectiva == Publicas.Perspectivas.Aprendizagem ? "A" :
                        (tipo.Perspectiva == Publicas.Perspectivas.Cliente ? "C" :
                        (tipo.Perspectiva == Publicas.Perspectivas.Financeira ? "F" : "P"))) + "'");
                    query.Append("        ,'" + tipo.Descricao + "'");
                    query.Append("        ,'" + tipo.TextoBI + "'");
                    query.Append("        ,'" + tipo.Formula + "'");
                    query.Append("        ,'" + (tipo.Regra == Publicas.RegraFormulaMetas.Igual ? "=" :
                                                (tipo.Regra == Publicas.RegraFormulaMetas.MaiorMelhor ? ">" : "<")) + "'");
                    query.Append("        ,'" + (tipo.UsaNaAvaliacao ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.UsaNaGestao ? "S" : "N") + "'");
                    query.Append("        ,'" + tipo.PrevistoCalculaPor + "'");
                    query.Append("        , " + tipo.PrevistoQdtMeses);
                    query.Append("        ,'" + (tipo.PrevistoAplicaDiasUteis ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.PrevistoPermiteAlterar ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.RealizadoPermiteAlterar ? "S" : "N") + "'");

                    if (tipo.IdBI == 0)
                        query.Append("        , null");
                    else
                        query.Append("        , " + tipo.IdBI);

                    query.Append("        ,'" + (tipo.UsaKMRodado ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.UsarColunaPrevistoParaCalculo ? "S" : "N") + "'");
                    query.Append("        ," + tipo.QuantidadeDecimais);
                    query.Append("        ,'" + (tipo.ExibeNoDRE ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.GrupoTotalizador ? "S" : "N") + "'");
                    query.Append("        ,'" + tipo.FormulaTotalizador + "'");
                    query.Append("        , " + tipo.NivelCalculo);
                    query.Append("        , '" + tipo.TipoDeFrota + "'");

                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_Ads_Metas");
                    query.Append("   set descricao = '" + tipo.Descricao + "'");
                    query.Append("     , TextoBI = '" + tipo.TextoBI + "'");
                    query.Append("     , ativo = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("     , tipo = '" + (tipo.Tipo == Publicas.TipoDeMetas.Crescimento ? "C" : "R") + "'");
                    query.Append("     , perspectiva = '" + (tipo.Perspectiva == Publicas.Perspectivas.Aprendizagem ? "A" :
                                                            (tipo.Perspectiva == Publicas.Perspectivas.Cliente ? "C" :
                                                            (tipo.Perspectiva == Publicas.Perspectivas.Financeira ? "F" : "P"))) + "'");

                    query.Append("     , Formula = '" + tipo.Formula + "'");
                    query.Append("     , RegraFormula = '" + (tipo.Regra == Publicas.RegraFormulaMetas.Igual ? "=" :
                                                (tipo.Regra == Publicas.RegraFormulaMetas.MaiorMelhor ? ">" : "<")) + "'");

                    query.Append("        , UsaParaAvaliacao = '" + (tipo.UsaNaAvaliacao ? "S" : "N") + "'");
                    query.Append("        , UsaParaGestao ='" + (tipo.UsaNaGestao ? "S" : "N") + "'");
                    query.Append("        , PrevistoCalcularPor = '" + tipo.PrevistoCalculaPor + "'");
                    query.Append("        , PrevistoQtdMes = " + tipo.PrevistoQdtMeses);
                    query.Append("        , PrevistoAplicaDiasUteis = '" + (tipo.PrevistoAplicaDiasUteis ? "S" : "N") + "'");
                    query.Append("        , PrevistoPermiteAlterar = '" + (tipo.PrevistoPermiteAlterar ? "S" : "N") + "'");
                    query.Append("        , RealizadoPermiteAlterar = '" + (tipo.RealizadoPermiteAlterar ? "S" : "N") + "'");

                    if (tipo.IdBI == 0)
                        query.Append("        , IdBI = null");
                    else
                        query.Append("        , IdBI = " + tipo.IdBI);

                    query.Append("        , UsaKmRodado = '" + (tipo.UsaKMRodado ? "S" : "N") + "'");
                    query.Append("        , UsarColunaPrevistoParaCalculo = '" + (tipo.UsarColunaPrevistoParaCalculo ? "S" : "N") + "'");
                    query.Append("        , QtdeDecimais = " + tipo.QuantidadeDecimais);
                    query.Append("        , ExibirNoDRE = '" + (tipo.ExibeNoDRE ? "S" : "N") + "'");
                    query.Append("        , GrupoTotalizador = '" + (tipo.GrupoTotalizador ? "S" : "N") + "'");
                    query.Append("        , FormulaTotalizador = '" + tipo.FormulaTotalizador + "'");
                    query.Append("        , NivelCalculo = " + tipo.NivelCalculo);
                    query.Append("        , TipoFrota = '" + tipo.TipoDeFrota + "'");
                    query.Append(" Where idmetas = " + tipo.Id);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    if (_listaDesmarcados.Count() != 0 && retorno)
                        retorno = ExcluiDetalheDesmarcado(_listaDesmarcados);

                    //retorno = ExcluiDetalhe(tipo.Id);

                    _lista.ForEach(u => u.IdMetas = tipo.Id);
                    contas.ForEach(u => u.IdMetas = tipo.Id);

                    if (retorno)
                        retorno = GravaDetalhes(_lista);

                    if (retorno)
                        retorno = Gravar(contas);
                }

                return retorno;
                
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool Excluir(Metas tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!ExcluiDetalhe(tipo.Id))
                    return false;
                else
                {
                    if (!Excluir(tipo.Id))
                        return false;
                    else
                    {
                        query.Append("Delete Niff_Ads_Metas");
                        query.Append(" Where idmetas = " + tipo.Id);
                        return sessao.ExecuteSqlTransaction(query.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public List<MetasBIItens> Listar(int IdMetas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<MetasBIItens> _lista = new List<MetasBIItens>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idmetas, tipo, codigo, descricao");
                query.Append("  from Niff_Ads_MetasBIItens c  ");
                query.Append(" Where IdMetas = " + IdMetas);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        MetasBIItens _tipo = new MetasBIItens();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdMetas = Convert.ToInt32(dataReader["idmetas"].ToString());

                        _tipo.Tipo = dataReader["Tipo"].ToString();

                        _tipo.Codigo = Convert.ToInt32(dataReader["Codigo"].ToString());
                        _tipo.Descricao = dataReader["descricao"].ToString();

                        _lista.Add(_tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public bool GravaDetalhes(List<MetasBIItens> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                foreach (var times in _lista)
                {
                    query.Clear();

                    if (!times.Existe)
                    {

                        query.Append("Insert into Niff_Ads_MetasBIItens");
                        query.Append("   (id, idmetas, tipo, codigo, descricao");
                        query.Append("  ) Values ( (Select nvl(Max(Id),0)+1 From Niff_Ads_MetasBIItens ) ");
                        query.Append(", " + times.IdMetas);
                        query.Append(", '" + times.Tipo + "'");
                        query.Append(", " + times.Codigo);
                        query.Append(", '" + times.Descricao + "'");
                        query.Append(") ");

                        retorno = sessao.ExecuteSqlTransaction(query.ToString());

                        if (!retorno)
                            break;
                    }

                }
                return retorno;
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool ExcluiDetalheDesmarcado(List<MetasBIItens> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                foreach (var item in _lista)
                {
                    query.Clear();
                        
                    query.Append("Delete Niff_Ads_MetasBIItens");
                    query.Append(" Where id = " + item.Id);

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                        break;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool ExcluiDetalhe(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            return true;
            //try
            //{
            //    query.Append("Delete Niff_Ads_MetasBIItens");
            //    query.Append(" Where idmetas = " + id);

            //    return sessao.ExecuteSqlTransaction(query.ToString());
            //}
            //catch (Exception ex)
            //{
            //    Publicas.mensagemDeErro = ex.Message;
            //    return false;
            //}
            //finally
            //{
            //    sessao.Desconectar();
            //}
        }

        public List<ValoresDasMetas> Listar(bool apenasAtivos, int idEmpresa, string referencia, string referenciaInicial, int idMeta)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ValoresDasMetas> _lista = new List<ValoresDasMetas>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.descricao, c.perspectiva, v.id, v.idmetas, referencia, Round(previsto,4) previsto, Round(realizado,4) realizado");
                query.Append("     , idusuariogerou, datagerou, idusuarioeditou, dataeditou ");
                query.Append("     , c.IdBI, c.ativo, aplicoucontrato, motivoedicaoprevisto, Round(nvl(previstooriginal,0),4) previstooriginal ");
                query.Append("     , Round(nvl(realizadooriginal,0),4) realizadooriginal, idempresa, motivoedicaoreal ");
                query.Append("     , nvl(v.quantidadediasuteis,0) quantidadediasuteis, c.QtdeDecimais, nvl(v.DiasCorridos,0) DiasCorridos");
                query.Append("     , FeriasBase, PlrPrevisto, PlrRealizado, Dissidio, QtdFeriado, DataCorteFinanceiro, DataCorteOperacional");
                query.Append("     , c.DESCRICAO NomeMeta");
                query.Append("  from niff_ads_valoresmetas v, niff_ads_metas c    ");
                
                query.Append(" Where v.idmetas = c.idmetas");
                query.Append("   And v.Idempresa = " + idEmpresa);

                if (referenciaInicial == "")
                    query.Append("   And v.referencia = '" + referencia + "'");
                else
                {
                    query.Append("   And v.referencia between '" + referenciaInicial + "' and '" + referencia + "'");
                    if (idMeta != 0)
                        query.Append("   And c.idMetas = " + idMeta);
                    
                }

                query.Append("   And c.usaparagestao = 'S'");

                if (apenasAtivos)
                    query.Append("   And c.ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ValoresDasMetas _tipo = new ValoresDasMetas();

                        _tipo.Existe = true;
                        
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdMetas = Convert.ToInt32(dataReader["idmetas"].ToString()); // 06|44|165|166|167|168|169|170|171 
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idempresa"].ToString());

                        int Decimais = Convert.ToInt32(dataReader["QtdeDecimais"].ToString());

                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Referencia = dataReader["Referencia"].ToString();
                        _tipo.Mes = _tipo.Referencia.Substring(4, 2) + "/" + _tipo.Referencia.Substring(0, 4);
                        _tipo.AplicouNoContratoDeMetas = dataReader["aplicoucontrato"].ToString() == "S";
                        _tipo.MotivoEdicaoPrevisto = dataReader["motivoedicaoPrevisto"].ToString();
                        _tipo.MotivoEdicaoReal = dataReader["motivoedicaoReal"].ToString();
                        _tipo.Metas = dataReader["NomeMeta"].ToString();                        

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        _tipo.PrevistoOriginal = Convert.ToDecimal(dataReader["previstooriginal"].ToString());
                        _tipo.RealizadoOriginal = Convert.ToDecimal(dataReader["realizadooriginal"].ToString());

                        _tipo.IdUsuarioQueGerou = Convert.ToInt32(dataReader["idusuariogerou"].ToString());

                        _tipo.DataQueGerou = Convert.ToDateTime(dataReader["datagerou"].ToString());
                        
                        _tipo.FeriasBase = Convert.ToDecimal(dataReader["FeriasBase"].ToString());
                        _tipo.PLRPrevisto = Convert.ToDecimal(dataReader["PlrPrevisto"].ToString());
                        _tipo.PLRRealizado = Convert.ToDecimal(dataReader["PlrRealizado"].ToString());
                        _tipo.Dissidio = Convert.ToDecimal(dataReader["Dissidio"].ToString());
                        _tipo.DiasFeriados = Convert.ToInt32(dataReader["QtdFeriado"].ToString());

                        try
                        {
                            _tipo.DiasUteis = Convert.ToInt32(dataReader["quantidadediasuteis"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DiasCorridos = Convert.ToInt32(dataReader["DiasCorridos"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdUsuarioQueEditou = Convert.ToInt32(dataReader["idusuarioeditou"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataQueAlterou = Convert.ToDateTime(dataReader["dataeditou"].ToString());

                        }
                        catch { }

                        try
                        {
                            _tipo.MediaUPrevisto = Math.Round(_tipo.Previsto / _tipo.DiasUteis, Decimais);
                        }
                        catch { }

                        try
                        {
                            _tipo.MediaURealizado = Math.Round(_tipo.Realizado / _tipo.DiasUteis, Decimais);
                        }
                        catch { }

                        try
                        {
                            _tipo.DataCorteFinanceiro = Convert.ToDateTime(dataReader["DataCorteFinanceiro"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataCorteOperacional = Convert.ToDateTime(dataReader["DataCorteOperacional"].ToString());
                        }
                        catch { }

                        _tipo.Perspectiva = (dataReader["perspectiva"].ToString() == "C" ? Publicas.Perspectivas.Cliente :
                                            (dataReader["perspectiva"].ToString() == "P" ? Publicas.Perspectivas.Processos :
                                            (dataReader["perspectiva"].ToString() == "F" ? Publicas.Perspectivas.Financeira : Publicas.Perspectivas.Aprendizagem)));

                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";

                        try
                        {
                            _tipo.IdBI = Convert.ToInt32(dataReader["IdBI"].ToString());
                        }
                        catch { }

                        _lista.Add(_tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public List<ValoresDasMetas> ListarDRE(bool apenasAtivos, int idEmpresa, string referencia, string referenciaInicial, int idMeta)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ValoresDasMetas> _lista = new List<ValoresDasMetas>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.descricao, c.perspectiva, v.id, v.idmetas, referencia, Round(previsto,4) previsto, Round(realizado,4) realizado");
                query.Append("     , idusuariogerou, datagerou, idusuarioeditou, dataeditou ");
                query.Append("     , c.IdBI, c.ativo, aplicoucontrato, motivoedicaoprevisto, Round(nvl(previstooriginal,0),4) previstooriginal ");
                query.Append("     , Round(nvl(realizadooriginal,0),4) realizadooriginal, idempresa, motivoedicaoreal ");
                query.Append("     , nvl(v.quantidadediasuteis,0) quantidadediasuteis, c.QtdeDecimais, nvl(v.DiasCorridos,0) DiasCorridos");
                query.Append("     , FeriasBase, PlrPrevisto, PlrRealizado, Dissidio, QtdFeriado, DataCorteFinanceiro, DataCorteOperacional");
                query.Append("     , c.DESCRICAO NomeMeta");
                query.Append("  from niff_ads_valoresmetas v, niff_ads_metas c    ");

                query.Append(" Where v.idmetas = c.idmetas");
                query.Append("   And v.Idempresa = " + idEmpresa);

                if (referenciaInicial == "")
                    query.Append("   And v.referencia = '" + referencia + "'");
                else
                {
                    query.Append("   And v.referencia between '" + referenciaInicial + "' and '" + referencia + "'");
                    if (idMeta != 0)
                        query.Append("   And c.idMetas = " + idMeta);
                }

                if (apenasAtivos)
                    query.Append("   And c.ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ValoresDasMetas _tipo = new ValoresDasMetas();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdMetas = Convert.ToInt32(dataReader["idmetas"].ToString());
                        /*inicio captuta idMeta = 172*/
                        if (_tipo.IdMetas==172)
                        {
                            Console.WriteLine("idMeta = 172");
                        }
                        /*fim captuta idMeta = 172*/
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idempresa"].ToString());

                        int Decimais = Convert.ToInt32(dataReader["QtdeDecimais"].ToString());

                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Referencia = dataReader["Referencia"].ToString();
                        _tipo.Mes = _tipo.Referencia.Substring(4, 2) + "/" + _tipo.Referencia.Substring(0, 4);
                        _tipo.AplicouNoContratoDeMetas = dataReader["aplicoucontrato"].ToString() == "S";
                        _tipo.MotivoEdicaoPrevisto = dataReader["motivoedicaoPrevisto"].ToString();
                        _tipo.MotivoEdicaoReal = dataReader["motivoedicaoReal"].ToString();
                        _tipo.Metas = dataReader["NomeMeta"].ToString();

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        _tipo.PrevistoOriginal = Convert.ToDecimal(dataReader["previstooriginal"].ToString());
                        _tipo.RealizadoOriginal = Convert.ToDecimal(dataReader["realizadooriginal"].ToString());

                        _tipo.IdUsuarioQueGerou = Convert.ToInt32(dataReader["idusuariogerou"].ToString());

                        _tipo.DataQueGerou = Convert.ToDateTime(dataReader["datagerou"].ToString());

                        _tipo.FeriasBase = Convert.ToDecimal(dataReader["FeriasBase"].ToString());
                        _tipo.PLRPrevisto = Convert.ToDecimal(dataReader["PlrPrevisto"].ToString());
                        _tipo.PLRRealizado = Convert.ToDecimal(dataReader["PlrRealizado"].ToString());
                        _tipo.Dissidio = Convert.ToDecimal(dataReader["Dissidio"].ToString());
                        _tipo.DiasFeriados = Convert.ToInt32(dataReader["QtdFeriado"].ToString());

                        try
                        {
                            _tipo.DiasUteis = Convert.ToInt32(dataReader["quantidadediasuteis"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DiasCorridos = Convert.ToInt32(dataReader["DiasCorridos"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.IdUsuarioQueEditou = Convert.ToInt32(dataReader["idusuarioeditou"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataQueAlterou = Convert.ToDateTime(dataReader["dataeditou"].ToString());

                        }
                        catch { }

                        try
                        {
                            _tipo.MediaUPrevisto = Math.Round(_tipo.Previsto / _tipo.DiasUteis, Decimais);
                        }
                        catch { }

                        try
                        {
                            _tipo.MediaURealizado = Math.Round(_tipo.Realizado / _tipo.DiasUteis, Decimais);
                        }
                        catch { }

                        try
                        {
                            _tipo.DataCorteFinanceiro = Convert.ToDateTime(dataReader["DataCorteFinanceiro"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataCorteOperacional = Convert.ToDateTime(dataReader["DataCorteOperacional"].ToString());
                        }
                        catch { }

                        _tipo.Perspectiva = (dataReader["perspectiva"].ToString() == "C" ? Publicas.Perspectivas.Cliente :
                                            (dataReader["perspectiva"].ToString() == "P" ? Publicas.Perspectivas.Processos :
                                            (dataReader["perspectiva"].ToString() == "F" ? Publicas.Perspectivas.Financeira : Publicas.Perspectivas.Aprendizagem)));

                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";

                        try
                        {
                            _tipo.IdBI = Convert.ToInt32(dataReader["IdBI"].ToString());
                        }
                        catch { }

                        _lista.Add(_tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public CalculoMetas Consultar(bool apenasAtivos, int idEmpresa, string referencia, int idMeta)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            CalculoMetas _tipo = new CalculoMetas();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.descricao, c.perspectiva, v.id, v.idmetas, referencia, Round(percentual,4) percentual, IdEmpresa");
                query.Append("     , idusuariogerou, datagerou, idusuarioeditou, dataeditou, MotivoEdicao ");
                query.Append("     , c.ativo, Round(nvl(ValorCalculado,0),4) ValorCalculado, DiasUteis, DiasCorridos");
                query.Append("     , Round(nvl(ValorResultado,0),4) ValorResultado, Round(nvl(ValorResultadoOriginal,0),4) ValorResultadoOriginal");
                query.Append("     , Aumentou, UsouDiasUteis, UsouDiasCorridos, UsouPrevisto, UsouRealizado, PermitiuAlterar");
                query.Append("     , FeriasBase, PlrPrevisto, Dissidio, QtdFeriado");
                query.Append("  from Niff_ads_CalculoMetas v, niff_ads_metas c    ");
                query.Append(" Where v.idmetas = c.idmetas");
                query.Append("   And v.idmetas = " + idMeta);
                query.Append("   And v.Idempresa = " + idEmpresa);
                query.Append("   And v.referencia = " + referencia);
                query.Append("   And c.usaparagestao = 'S'");

                if (apenasAtivos)
                    query.Append("   And c.ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdMetas = Convert.ToInt32(dataReader["idmetas"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idempresa"].ToString());

                        _tipo.Descricao = dataReader["Descricao"].ToString();
                        _tipo.Referencia = dataReader["Referencia"].ToString();
                        _tipo.UsouDiasCorridos = dataReader["UsouDiasCorridos"].ToString() == "S";
                        _tipo.UsouDiasUteis = dataReader["UsouDiasUteis"].ToString() == "S";
                        _tipo.Aumentou = dataReader["Aumentou"].ToString() == "S";
                        _tipo.PermitiuAlterar = dataReader["PermitiuAlterar"].ToString() == "S";

                        _tipo.UsouPrevisto = dataReader["UsouPrevisto"].ToString() == "S";
                        _tipo.UsouRealizado = dataReader["UsouRealizado"].ToString() == "S";

                        _tipo.ValorCalculado = Convert.ToDecimal(dataReader["ValorCalculado"].ToString());
                        _tipo.ValorResultado = Convert.ToDecimal(dataReader["ValorResultado"].ToString());
                        _tipo.ValorResultadoOriginal = Convert.ToDecimal(dataReader["ValorResultadoOriginal"].ToString());
                        _tipo.Percentual = Convert.ToDecimal(dataReader["Percentual"].ToString());

                        _tipo.DiasCorridos = Convert.ToInt32(dataReader["DiasCorridos"].ToString());
                        _tipo.DiasUteis = Convert.ToInt32(dataReader["DiasUteis"].ToString());

                        _tipo.IdUsuarioQueGerou = Convert.ToInt32(dataReader["idusuariogerou"].ToString());

                        _tipo.DataQueGerou = Convert.ToDateTime(dataReader["datagerou"].ToString());
                        _tipo.FeriasBase = Convert.ToDecimal(dataReader["FeriasBase"].ToString());
                        _tipo.PLRPrevisto = Convert.ToDecimal(dataReader["PlrPrevisto"].ToString());
                        _tipo.Dissidio = Convert.ToDecimal(dataReader["Dissidio"].ToString());
                        _tipo.DiasFeriados = Convert.ToInt32(dataReader["QtdFeriado"].ToString());

                        try
                        {
                            _tipo.IdUsuarioQueEditou = Convert.ToInt32(dataReader["idusuarioeditou"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataQueAlterou = Convert.ToDateTime(dataReader["dataeditou"].ToString());

                        }
                        catch { }


                        _tipo.Perspectiva = (dataReader["perspectiva"].ToString() == "C" ? Publicas.Perspectivas.Cliente :
                                            (dataReader["perspectiva"].ToString() == "P" ? Publicas.Perspectivas.Processos :
                                            (dataReader["perspectiva"].ToString() == "F" ? Publicas.Perspectivas.Financeira : Publicas.Perspectivas.Aprendizagem)));

                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                                                
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _tipo;
        }

        public List<MesesUsadoNoCalculo> ListarMesesUtilizados(int IdCalculo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<MesesUsadoNoCalculo> _lista = new List<MesesUsadoNoCalculo>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select r.Id, r.IdCalculo, r.IdValorMetas, r.Previsto, r.Realizado, v.Referencia, c.UsouDiasUteis, c.UsouDiasCorridos, c.UsouPrevisto, c.UsouRealizado");
                query.Append("  from Niff_Ads_RefCalculoMetas r, Niff_Ads_CalculoMetas c, Niff_Ads_Valoresmetas v");
                query.Append(" Where r.IdCalculo = " + IdCalculo);
                query.Append("   and r.IdCalculo = c.Id");
                query.Append("   And r.Idvalormetas = v.Id");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        MesesUsadoNoCalculo _tipo = new MesesUsadoNoCalculo();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdCalculo = Convert.ToInt32(dataReader["IdCalculo"].ToString());
                        _tipo.IdValorMetas = Convert.ToInt32(dataReader["IdValorMetas"].ToString());

                        _tipo.Previsto = Convert.ToDecimal(dataReader["Previsto"].ToString());
                        _tipo.Realizado = Convert.ToDecimal(dataReader["Realizado"].ToString());
                        
                        _tipo.Referencia = dataReader["Referencia"].ToString();
                        _lista.Add(_tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public bool Gravar(CalculoMetas tipo, List<MesesUsadoNoCalculo> mesesSelecionados)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            int id = 1;

            try
            {
                query.Append("Select Nvl(Max(Id),0) +1 next From niff_ads_calculometas");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        id = Convert.ToInt32(dataReader["next"].ToString());
                }

                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into niff_ads_calculometas");
                    query.Append(" ( id, idempresa, idmetas, referencia, percentual, aumentou, usoudiasuteis, usoudiascorridos ");
                    query.Append(" , usouprevisto, usourealizado, permitiualterar, valorcalculado, valorresultado, valorresultadooriginal");
                    query.Append(" , datagerou, idusuariogerou, motivoedicao, diasuteis, diascorridos, FeriasBase, PlrPrevisto, Dissidio, QtdFeriado)");

                    query.Append(" Values ( " + id);
                    query.Append("        ," + tipo.IdEmpresa);
                    query.Append("        ," + tipo.IdMetas);
                    query.Append("        ,'" + tipo.Referencia + "'");
                    query.Append("        ," + tipo.Percentual.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        ,'" + (tipo.Aumentou ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.UsouDiasUteis ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.UsouDiasCorridos ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.UsouPrevisto ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.UsouRealizado ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.PermitiuAlterar ? "S" : "N") + "'");
                    query.Append("        ," + tipo.ValorCalculado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        ," + tipo.ValorResultado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        ," + tipo.ValorResultadoOriginal.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , Sysdate" );
                    query.Append("        ," + Publicas._usuario.Id);
                    query.Append("        , null");//tratar
                    query.Append("        ," + tipo.DiasUteis);
                    query.Append("        ," + tipo.DiasCorridos);
                    query.Append("        ," + tipo.FeriasBase.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        ," + tipo.PLRPrevisto.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        ," + tipo.Dissidio.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        ," + tipo.DiasFeriados);
                    query.Append("        )");

                }
                else
                {
                    id = tipo.Id;
                    query.Append("Update niff_ads_calculometas");
                    query.Append("   set DataEditou = sysdate");
                    query.Append("     , IdUsuarioEditou = " + Publicas._usuario.Id);
                    query.Append("     , Percentual = " + tipo.Percentual.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Aumentou = '" + (tipo.Aumentou ? "S" : "N") + "'");
                    query.Append("     , UsouDiasUteis = '" + (tipo.UsouDiasUteis ? "S" : "N") + "'");
                    query.Append("     , UsouDiasCorridos = '" + (tipo.UsouDiasCorridos ? "S" : "N") + "'");
                    query.Append("     , UsouPrevisto = '" + (tipo.UsouPrevisto ? "S" : "N") + "'");
                    query.Append("     , UsouRealizado = '" + (tipo.UsouRealizado ? "S" : "N") + "'");
                    query.Append("     , PermitiuAlterar = '" + (tipo.PermitiuAlterar ? "S" : "N") + "'");
                    query.Append("     , ValorCalculado = " + tipo.ValorCalculado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , valorresultado = " + tipo.ValorResultado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , valorresultadooriginal = " + tipo.ValorResultadoOriginal.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , motivoedicao = null");//tratar
                    query.Append("     , diasuteis = " + tipo.DiasUteis);
                    query.Append("     , diascorridos = " + tipo.DiasCorridos);
                    query.Append("     , FeriasBase = " + tipo.FeriasBase.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , PlrPrevisto = " + tipo.PLRPrevisto.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Dissidio = " + tipo.Dissidio.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , QtdFeriado = " + tipo.DiasFeriados);

                    query.Append(" Where id = " + tipo.Id);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                mesesSelecionados.ForEach(u => u.IdCalculo = id);

                if (retorno)
                {
                    retorno = ExcluiMesesSelecionados(id);

                    if (retorno)
                    {
                        retorno = GravaMesesSelecionados(mesesSelecionados);
                    }
                }

                return retorno;

            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool GravaMesesSelecionados(List<MesesUsadoNoCalculo> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                foreach (var times in _lista)
                {
                    query.Clear();
                    
                    query.Append("Insert into Niff_Ads_RefCalculoMetas");
                    query.Append("   (id, idcalculo, idvalormetas, previsto, realizado, diasuteis, diascorridos )");
                    query.Append(" Values ( (Select nvl(Max(Id),0)+1 From Niff_Ads_RefCalculoMetas ) ");
                    query.Append("      , " + times.IdCalculo);
                    query.Append("      , " + times.IdValorMetas);
                    query.Append("      , " + times.Previsto.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("      , " + times.Realizado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("      , " + times.DiasUteis);
                    query.Append("      , " + times.DiasCorridos);
                    query.Append("  ) ");

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                        break;
                }
                return retorno;
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool ExcluiMesesSelecionados(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                query.Clear();

                query.Append("Delete Niff_Ads_RefCalculoMetas");
                query.Append(" Where idcalculo = " + id);

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                return retorno;
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool ExcluiCalculoMetas(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                retorno = ExcluiMesesSelecionados(id);

                if (retorno)
                {
                    query.Clear();
                    query.Append("Delete niff_ads_calculometas");
                    query.Append(" Where id = " + id);
                }
                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                return retorno;
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public decimal FeriasBase(int idEmpresa, string ano, int idMeta)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;
            try
            {
                query.Append("select Max(t.feriasbase) base from niff_ads_calculometas t");
                query.Append(" Where substr(t.referencia,1,4) = " + ano);
                query.Append("   and idEmpresa = " + idEmpresa);
                query.Append("   And idMetas = " + idMeta);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _valor  = Convert.ToDecimal(dataReader["base"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _valor;
        }

        public BSCEmEdicao Consulta(int empresa, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            BSCEmEdicao _tipo = new BSCEmEdicao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.id, c.idempresa, c.referencia, c.idusuario, c.Tela, u.nome");
                query.Append("  from niff_ads_BSCEmAlteracao c, niff_chm_usuarios u     ");
                query.Append(" Where c.idEmpresa = " + empresa);
                query.Append("   and c.referencia = '" + referencia + "'");
                query.Append("   and c.IdUsuario = u.IdUsuario");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["idUsuario"].ToString());

                        _tipo.Referencia = dataReader["Referencia"].ToString();
                        _tipo.Tela = dataReader["Tela"].ToString();
                        _tipo.NomeUsuario = dataReader["Nome"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _tipo;
        }

        public bool Gravar(BSCEmEdicao tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into niff_ads_BSCEmAlteracao");
                    query.Append(" ( id, idempresa, referencia, idusuario, Tela )");

                    query.Append(" Values ((Select Nvl(Max(id),0) +1 next From niff_ads_BSCEmAlteracao)");
                    query.Append("        ," + tipo.IdEmpresa);
                    query.Append("        ,'" + tipo.Referencia + "'");
                    query.Append("        ," + tipo.IdUsuario);
                    query.Append("        ,'" + tipo.Tela + "'");
                    query.Append(" )");
                }                

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                return retorno;

            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool Excluir(BSCEmEdicao tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete niff_ads_BSCEmAlteracao");
                query.Append(" Where id = " + tipo.Id);
                return sessao.ExecuteSqlTransaction(query.ToString());
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public List<MetasContasContabeis> ListarContasMetas(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<MetasContasContabeis> _lista = new List<MetasContasContabeis>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select m.id, m.idmetas, m.idempresa, m.codcontactb, m.nroplano, m.tipo, m.formula");
                query.Append("     , c.classificador || ' ' || c.NomeConta nome");
                query.Append("     , e.CodigoGlobus || ' - ' || e.NomeAbreviado Empresa");
                query.Append("  from Niff_Ads_MetasContasCTB m, ctbConta c, niff_chm_empresas e    ");
                query.Append(" Where idmetas = " + id);
                query.Append("   and c.codcontactb = m.codcontactb");
                query.Append("   and c.nroplano = m.nroplano");
                query.Append("   and e.Idempresa = m.Idempresa");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        MetasContasContabeis _tipo = new MetasContasContabeis();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdMetas = Convert.ToInt32(dataReader["idmetas"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Conta = Convert.ToInt32(dataReader["codcontactb"].ToString());
                        _tipo.Plano = Convert.ToInt32(dataReader["nroplano"].ToString());
                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Empresa = dataReader["Empresa"].ToString();
                        _tipo.Formula = dataReader["Formula"].ToString();

                        _tipo.Tipo = dataReader["tipo"].ToString();
                        _lista.Add(_tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public List<MetasContasContabeis> ListarContasMetas(int idEmpresa, int plano, int idMetas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<MetasContasContabeis> _lista = new List<MetasContasContabeis>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select m.id, m.idmetas, m.idempresa, m.codcontactb, m.nroplano, m.tipo, m.formula");
                query.Append("     , c.classificador || ' ' || c.NomeConta nome");
                query.Append("     , e.CodigoGlobus || ' - ' || e.NomeAbreviado Empresa");
                query.Append("  from Niff_Ads_MetasContasCTB m, ctbConta c, niff_chm_empresas e    ");
                query.Append(" Where m.idEmpresa = " + idEmpresa);
                query.Append("   and c.nroplano = " + plano);
                query.Append("   and idmetas = " + idMetas);
                query.Append("   and c.codcontactb = m.codcontactb");
                query.Append("   and c.nroplano = m.nroplano");
                query.Append("   and e.Idempresa = m.Idempresa");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        MetasContasContabeis _tipo = new MetasContasContabeis();

                        _tipo.Existe = true;

                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdMetas = Convert.ToInt32(dataReader["idmetas"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Conta = Convert.ToInt32(dataReader["codcontactb"].ToString());
                        _tipo.Plano = Convert.ToInt32(dataReader["nroplano"].ToString());
                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Empresa = dataReader["Empresa"].ToString();
                        _tipo.Formula = dataReader["Formula"].ToString();

                        _tipo.Tipo = dataReader["tipo"].ToString();

                        _lista.Add(_tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public bool Gravar(List<MetasContasContabeis> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                foreach (var tipo in _lista)
                {

                    if (!tipo.Existe)
                    {
                        query.Clear();
                        query.Append("Insert into Niff_ads_MetasContasCTB");
                        query.Append(" ( id, idmetas, idempresa, codcontactb, nroplano, tipo, Formula )");

                        query.Append(" Values ((Select Nvl(Max(id),0) +1 next From Niff_ads_MetasContasCTB)");
                        query.Append("        ," + tipo.IdMetas);
                        query.Append("        ," + tipo.IdEmpresa);
                        query.Append("        ," + tipo.Conta);
                        query.Append("        ," + tipo.Plano);
                        query.Append("        ,'" + tipo.Tipo + "'");
                        query.Append("        ,'" + tipo.Formula + "'");
                        query.Append(" )");

                        retorno = sessao.ExecuteSqlTransaction(query.ToString());
                    }
                }

                return retorno;

            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool ExcluiConta(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_ads_MetasContasCTB");
                query.Append(" Where id = " + id);

                return sessao.ExecuteSqlTransaction(query.ToString());
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool Excluir(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_ads_MetasContasCTB");
                query.Append(" Where idMetas = " + id);
                return sessao.ExecuteSqlTransaction(query.ToString());
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

    }
}
