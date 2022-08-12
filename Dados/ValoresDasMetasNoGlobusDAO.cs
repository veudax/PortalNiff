using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ValoresDasMetasNoGlobusDAO
    {
        IDataReader dataReader;

        #region DRE
        public List<DRE> Listar(int IdEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<DRE> _lista = new List<DRE>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, referencia, fechado, datafechamento, idusuario, idusuariofecha, Dissidio");
                query.Append("  from NIFF_CTB_DRE c     ");                
                query.Append(" Where idempresa = " + IdEmpresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        DRE _tipo = new DRE();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idempresa"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["idusuario"].ToString());
                        _tipo.Dissidio = Convert.ToDecimal(dataReader["Dissidio"].ToString());

                        try
                        {
                            _tipo.IdUsuarioFechamento = Convert.ToInt32(dataReader["id"].ToString());
                        }
                        catch { }

                        _tipo.Referencia = dataReader["referencia"].ToString();

                        _tipo.Fechado = dataReader["fechado"].ToString() == "S";
                        
                        _tipo.DataFechamento = Convert.ToDateTime(dataReader["datafechamento"].ToString());

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

        public DRE ConsultarDRE(int IdEmpresa, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            DRE _tipo = new DRE();

            try
            {
                query.Append("Select id, idempresa, referencia, fechado, datafechamento, idusuario, idusuariofecha, Dissidio");
                query.Append("  from NIFF_CTB_DRE c     ");
                query.Append(" Where idempresa = " + IdEmpresa);
                query.Append("   and referencia = '" + referencia + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idempresa"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["idusuario"].ToString());
                        _tipo.Dissidio = Convert.ToDecimal(dataReader["Dissidio"].ToString());

                        try
                        {
                            _tipo.IdUsuarioFechamento = Convert.ToInt32(dataReader["id"].ToString());
                        }
                        catch { }

                        _tipo.Referencia = dataReader["referencia"].ToString();

                        _tipo.Fechado = dataReader["fechado"].ToString() == "S";

                        _tipo.DataFechamento = Convert.ToDateTime(dataReader["datafechamento"].ToString());

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

        public bool Gravar(DRE _dre, List<ValoresDasMetas> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            
            try
            {
                if (!_dre.Existe)
                {
                    query.Append("Insert into NIFF_CTB_DRE");
                    query.Append(" ( id, idempresa, referencia, fechado, idusuario, datafechamento, idusuariofecha, Dissidio)");
                    query.Append(" Values ((Select nvl(Max(Id),0)+1 From NIFF_CTB_DRE)");
                    query.Append(" , " + _dre.IdEmpresa);
                    query.Append(" , " + _dre.Referencia);
                    query.Append(" , '" + (_dre.Fechado ? "S" : "N") + "'");
                    query.Append(" , " + Publicas._usuario.Id);

                    if (_dre.Fechado && _dre.DataFechamento != DateTime.MinValue)
                    {
                        query.Append(" , sysdate");
                        query.Append(" , " + Publicas._usuario.Id);
                    }
                    else
                        query.Append(" , null, null");

                    query.Append(" , " + _dre.Dissidio.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" ) ");
                }
                else
                {
                    query.Append("Update NIFF_CTB_DRE");
                    query.Append("   set Fechado = '" + (_dre.Fechado ? "S" : "N") + "'");
                    query.Append("     , idusuario = " + Publicas._usuario.Id);
                    query.Append("     , Dissidio = " + _dre.Dissidio.ToString().Replace(".", "").Replace(",", "."));
                    
                    if (_dre.Fechado && _dre.DataFechamento != DateTime.MinValue)
                    {
                        query.Append(" , datafechamento = sysdate");
                        query.Append(" , idusuariofecha = " + Publicas._usuario.Id);
                    }

                    query.Append(" where Id = " + _dre.Id);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    foreach (var item in _lista)
                    {
                        query.Clear();

                        if (!item.Existe)
                        {
                            query.Append("Insert into niff_ads_valoresmetas");
                            query.Append(" ( id, idmetas, referencia, previsto, realizado, idusuariogerou, datagerou, motivoedicaoprevisto, previstooriginal,");
                            query.Append(" realizadooriginal, idempresa, motivoedicaoreal, quantidadediasuteis, DataCorteFinanceiro, DataCorteOperacional)");
                            query.Append(" Values ((Select nvl(Max(Id),0)+1 From niff_ads_valoresmetas)");
                            query.Append(" , " + item.IdMetas);
                            query.Append(" , " + item.Referencia);
                            query.Append(" , " + item.Previsto.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" , " + item.Realizado.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" , " + Publicas._usuario.Id);
                            query.Append(" , sysdate ");
                            query.Append(" , '" + item.MotivoEdicaoPrevisto + "'");
                            query.Append(" , " + item.PrevistoOriginal.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" , " + item.RealizadoOriginal.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" , " + item.IdEmpresa);
                            query.Append(" , '" + item.MotivoEdicaoReal + "'");
                            query.Append(" , " + item.DiasUteis);
                            if (item.DataCorteFinanceiro != DateTime.MaxValue)
                                query.Append(" , To_date('" + item.DataCorteFinanceiro.ToShortDateString() + "','dd/mm/yyyy')");
                            else
                                query.Append(" , null");
                            if (item.DataCorteOperacional != DateTime.MaxValue)
                                query.Append(" , To_date('" + item.DataCorteOperacional.ToShortDateString() + "','dd/mm/yyyy')");
                            else
                                query.Append(" , null");
                            query.Append(" ) ");
                        }
                        else
                        {
                            query.Append("Update niff_ads_valoresmetas");
                            query.Append("   set previsto = " + item.Previsto.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , realizado = " + item.Realizado.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , IdUsuarioEditou = " + Publicas._usuario.Id);
                            query.Append("     , DataEditou = sysdate");
                            query.Append("     , MotivoEdicaoPrevisto = '" + item.MotivoEdicaoPrevisto + "'");
                            query.Append("     , MotivoEdicaoReal = '" + item.MotivoEdicaoReal + "'");
                            query.Append("     , QuantidadeDiasUteis = " + item.DiasUteis);
                            query.Append("     , PrevistoOriginal = " + item.PrevistoOriginal.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , realizadoOriginal = " + item.RealizadoOriginal.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , FeriasBase = " + item.FeriasBase.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , PlrPrevisto = " + item.PLRPrevisto.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , Dissidio = " + item.Dissidio.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , QtdFeriado = " + item.DiasFeriados);

                            if (item.DataCorteFinanceiro != DateTime.MaxValue)
                                query.Append(" , DataCorteFinanceiro = To_date('" + item.DataCorteFinanceiro.ToShortDateString() + "','dd/mm/yyyy')");
                            if (item.DataCorteOperacional != DateTime.MaxValue)
                                query.Append(" , DataCorteOperacional = To_date('" + item.DataCorteOperacional.ToShortDateString() + "','dd/mm/yyyy')");

                            query.Append(" where Id = " + item.Id);
                        }

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

        public bool Excluir(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            
            try
            {
                query.Clear();

                query.Append("delete NIFF_CTB_DRE");
                query.Append(" where Id = " + id);

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

        #endregion


        public bool Gravar(List<ValoresDasMetas> _lista, List<ItensAvaliacaoMetas> _listaContratoMetas, string referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            int dias = 0;
            DateTime _data = DateTime.MinValue;
            Empresa _empresa = new Empresa();
            string _formula = "";
            try
            {
                Pontuacao _pontuacao = new PontuacaoDAO().ConsultarMaiorReferencia(Convert.ToInt32(referencia));

                foreach (var item in _lista)
                {
                    query.Clear();

                    if (!item.Existe)
                    {
                        query.Append("Insert into niff_ads_valoresmetas");
                        query.Append(" ( id, idmetas, referencia, previsto, realizado, idusuariogerou, datagerou, motivoedicaoprevisto, previstooriginal,");
                        query.Append(" realizadooriginal, idempresa, motivoedicaoreal, quantidadediasuteis, DataCorteFinanceiro, DataCorteOperacional)");
                        query.Append(" Values ((Select nvl(Max(Id),0)+1 From niff_ads_valoresmetas)");
                        query.Append(" , " + item.IdMetas);
                        query.Append(" , " + item.Referencia);
                        query.Append(" , " + item.Previsto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(" , " + item.Realizado.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(" , " + Publicas._usuario.Id);
                        query.Append(" , sysdate ");
                        query.Append(" , '" + item.MotivoEdicaoPrevisto + "'");
                        query.Append(" , " + item.PrevistoOriginal.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(" , " + item.RealizadoOriginal.ToString().Replace(".", "").Replace(",", "."));
                        query.Append(" , " + item.IdEmpresa);
                        query.Append(" , '" + item.MotivoEdicaoReal + "'");
                        query.Append(" , " + item.DiasUteis);
                        if (item.DataCorteFinanceiro != DateTime.MaxValue)
                            query.Append(" , To_date('" + item.DataCorteFinanceiro.ToShortDateString() + "','dd/mm/yyyy')");
                        else
                            query.Append(" , null");
                        if (item.DataCorteOperacional != DateTime.MaxValue)
                            query.Append(" , To_date('" + item.DataCorteOperacional.ToShortDateString() + "','dd/mm/yyyy')");
                        else
                            query.Append(" , null");
                        query.Append(" ) ");
                    }
                    else
                    {
                        query.Append("Update niff_ads_valoresmetas");
                        query.Append("   set previsto = " + item.Previsto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , realizado = " + item.Realizado.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , IdUsuarioEditou = " + Publicas._usuario.Id);
                        query.Append("     , DataEditou = sysdate");
                        query.Append("     , MotivoEdicaoPrevisto = '" + item.MotivoEdicaoPrevisto + "'");
                        query.Append("     , MotivoEdicaoReal = '" + item.MotivoEdicaoReal + "'");
                        query.Append("     , QuantidadeDiasUteis = " + item.DiasUteis);
                        query.Append("     , PrevistoOriginal = " + item.PrevistoOriginal.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , realizadoOriginal = " + item.RealizadoOriginal.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , FeriasBase = " + item.FeriasBase.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , PlrPrevisto = " + item.PLRPrevisto.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , Dissidio = " + item.Dissidio.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , QtdFeriado = " + item.DiasFeriados);

                        if (item.DataCorteFinanceiro != DateTime.MaxValue)
                            query.Append(" , DataCorteFinanceiro = To_date('" + item.DataCorteFinanceiro.ToShortDateString() + "','dd/mm/yyyy')");
                        if (item.DataCorteOperacional != DateTime.MaxValue)
                            query.Append(" , DataCorteOperacional = To_date('" + item.DataCorteOperacional.ToShortDateString() + "','dd/mm/yyyy')");

                        query.Append(" where Id = " + item.Id);
                    }

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    _data = Convert.ToDateTime("01/" + item.Referencia.Substring(4, 2) + "/" + item.Referencia.Substring(0, 4));
                    _empresa = new EmpresaDAO().ConsultaEmpresa(item.IdEmpresa);

                    if (retorno)
                    {
                        // verifica se existe a meta na referencia no contrato. 
                        foreach (var itemC in _listaContratoMetas.Where(w => w.IdMetas == item.IdMetas))
                        {
                            _formula = itemC.Formula;

                            itemC.ValorEsperado = item.Previsto;
                            itemC.Realizado = item.Realizado;

                            try
                            {
                                if (itemC.ValorEsperado != 0)
                                    itemC.Eficiencia = Math.Round(new ItensAvaliacaoMetasDAO().CalculoFormula(_formula.Replace("Realizado", itemC.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                             .Replace("Esperado", itemC.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                else
                                {
                                    if (_formula.Contains("/ Esperado") && _formula.Contains("200"))
                                        itemC.Eficiencia = Math.Round(new ItensAvaliacaoMetasDAO().CalculoFormula(_formula.Replace("Realizado", itemC.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                             .Replace("Esperado", 1.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    else
                                    {
                                        if (!_formula.Contains("/ Esperado"))
                                            itemC.Eficiencia = Math.Round(new ItensAvaliacaoMetasDAO().CalculoFormula(_formula.Replace("Realizado", itemC.Realizado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }
                                }
                            }
                            catch { }

                            if (itemC.ValorEsperado == 0 && itemC.Realizado == 0)
                            {
                                itemC.Eficiencia = 100;
                            }

                            itemC.Resultado = Math.Abs(Math.Round(itemC.Eficiencia != 0 ? ((itemC.Peso * itemC.Eficiencia) / 100) : 0, 2));

                            itemC.EficienciaPonderada = (itemC.Eficiencia > _pontuacao.Base100 ? _pontuacao.Base100 : (itemC.Eficiencia < _pontuacao.Piso ? _pontuacao.Piso : itemC.Eficiencia));
                            itemC.ResultadoPonderado = Math.Abs(Math.Round(itemC.EficienciaPonderada != 0 ? ((itemC.Peso * itemC.EficienciaPonderada) / 100) : 0, 2));

                            retorno = new ItensAvaliacaoMetasDAO().Alterar(itemC);
                        }
                        
                        if (item.Realizado != 0)
                        {
                            RadarBI _radar = new RadarBI();
                            // grava os valores 

                            if (item.Descricao.ToUpper().Contains("LIMPEZA"))
                            {
                                _radar = new RadarBIDAO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas.GetDescription(Publicas.RadarBI.LimpezaDeFrota, ""), _data);
                                if (_radar.Existe)
                                    _radar.PercentualAnterior = _radar.Percentual;
                                else
                                {
                                    _radar.EmpresaFilial = _empresa.CodigoEmpresaGlobus;
                                    _radar.Data = _data;
                                    _radar.Grupo = Publicas.GetDescription(Publicas.RadarBI.LimpezaDeFrota, "");
                                    _radar.Ordem = Convert.ToInt32(Publicas.GetDescription(Publicas.OrdemRadarBI.LimpezaDeFrota, ""));
                                    _radar.Tipo = Publicas.GetDescription(Publicas.TipoRadarBI.LimpezaDeFrota, "");
                                }
                                _radar.Percentual = item.Realizado;
                                retorno = new RadarBIDAO().Gravar(_radar);
                            }
                            if (retorno && item.Descricao.ToUpper().Contains("MULTA"))
                            {
                                _radar = new RadarBIDAO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas.GetDescription(Publicas.RadarBI.MultaOrgaoGestor, ""), _data);
                                if (_radar.Existe)
                                    _radar.PercentualAnterior = _radar.Percentual;
                                else
                                {
                                    _radar.EmpresaFilial = _empresa.CodigoEmpresaGlobus;
                                    _radar.Data = _data;
                                    _radar.Grupo = Publicas.GetDescription(Publicas.RadarBI.MultaOrgaoGestor, "");
                                    _radar.Ordem = Convert.ToInt32(Publicas.GetDescription(Publicas.OrdemRadarBI.MultaOrgaoGestor, ""));
                                    _radar.Tipo = Publicas.GetDescription(Publicas.TipoRadarBI.MultaOrgaoGestor, "");
                                }
                                _radar.Percentual = item.Realizado;
                                retorno = new RadarBIDAO().Gravar(_radar);
                            }
                            if (retorno && item.Descricao.ToUpper().Contains("COMPRA"))
                            {
                                _radar = new RadarBIDAO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas.GetDescription(Publicas.RadarBI.ComprasEmergenciais, ""), _data);
                                if (_radar.Existe)
                                    _radar.PercentualAnterior = _radar.Percentual;
                                else
                                {
                                    _radar.EmpresaFilial = _empresa.CodigoEmpresaGlobus;
                                    _radar.Data = _data;
                                    _radar.Grupo = Publicas.GetDescription(Publicas.RadarBI.ComprasEmergenciais, "");
                                    _radar.Ordem = Convert.ToInt32(Publicas.GetDescription(Publicas.OrdemRadarBI.ComprasEmergenciais, ""));
                                    _radar.Tipo = Publicas.GetDescription(Publicas.TipoRadarBI.ComprasEmergenciais, "");
                                }

                                _radar.Percentual = item.Realizado;
                                retorno = new RadarBIDAO().Gravar(_radar);
                            }
                            if (retorno && item.Descricao.ToUpper().Contains("RETIDO"))
                            {
                                _radar = new RadarBIDAO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas.GetDescription(Publicas.RadarBI.CarrosRetidos, ""), _data);
                                if (_radar.Existe)
                                    _radar.PercentualAnterior = _radar.Percentual;
                                else
                                {
                                    _radar.EmpresaFilial = _empresa.CodigoEmpresaGlobus;
                                    _radar.Data = _data;
                                    _radar.Grupo = Publicas.GetDescription(Publicas.RadarBI.CarrosRetidos, "");
                                    _radar.Ordem = Convert.ToInt32(Publicas.GetDescription(Publicas.OrdemRadarBI.CarrosRetidos, ""));
                                    _radar.Tipo = Publicas.GetDescription(Publicas.TipoRadarBI.CarrosRetidos, "");
                                }

                                _radar.Percentual = item.Realizado;
                                retorno = new RadarBIDAO().Gravar(_radar);
                            }
                            if (item.Descricao.ToUpper().Contains("RECLAMA"))
                            {
                                _radar = new RadarBIDAO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas.GetDescription(Publicas.RadarBI.ReclamaCaoPassageiro, ""), _data);
                                if (_radar.Existe)
                                    _radar.PercentualAnterior = _radar.Percentual;
                                else
                                {
                                    _radar.EmpresaFilial = _empresa.CodigoEmpresaGlobus;
                                    _radar.Data = _data;
                                    _radar.Grupo = Publicas.GetDescription(Publicas.RadarBI.ReclamaCaoPassageiro, "");
                                    _radar.Ordem = Convert.ToInt32(Publicas.GetDescription(Publicas.OrdemRadarBI.ReclamaCaoPassageiro, ""));
                                    _radar.Tipo = Publicas.GetDescription(Publicas.TipoRadarBI.ReclamaCaoPassageiro, "");
                                }

                                _radar.Valor = item.Realizado;
                                retorno = new RadarBIDAO().Gravar(_radar);
                            }
                            if (item.Descricao.ToUpper().Contains("CUMPRIMENTO"))
                            {
                                _radar = new RadarBIDAO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas.GetDescription(Publicas.RadarBI.CumprimentoDePartida, ""), _data);
                                if (_radar.Existe)
                                    _radar.PercentualAnterior = _radar.Percentual;
                                else
                                {
                                    _radar.EmpresaFilial = _empresa.CodigoEmpresaGlobus;
                                    _radar.Data = _data;
                                    _radar.Grupo = Publicas.GetDescription(Publicas.RadarBI.CumprimentoDePartida, "");
                                    _radar.Ordem = Convert.ToInt32(Publicas.GetDescription(Publicas.OrdemRadarBI.CumprimentoDePartida, ""));
                                    _radar.Tipo = Publicas.GetDescription(Publicas.TipoRadarBI.CumprimentoDePartida, "");
                                }

                                _radar.Percentual = item.Realizado;
                                retorno = new RadarBIDAO().Gravar(_radar);
                            }
                            if (item.Descricao.ToUpper().Contains("EXAME"))
                            {
                                _radar = new RadarBIDAO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas.GetDescription(Publicas.RadarBI.ExamesVencidos, ""), _data);
                                if (_radar.Existe)
                                    _radar.ValorAnterior = _radar.Valor;
                                else
                                {
                                    _radar.EmpresaFilial = _empresa.CodigoEmpresaGlobus;
                                    _radar.Data = _data;
                                    _radar.Grupo = Publicas.GetDescription(Publicas.RadarBI.ExamesVencidos, "");
                                    _radar.Ordem = Convert.ToInt32(Publicas.GetDescription(Publicas.OrdemRadarBI.ExamesVencidos, ""));
                                    _radar.Tipo = Publicas.GetDescription(Publicas.TipoRadarBI.ExamesVencidos, "");
                                }

                                _radar.Valor = item.Realizado;
                                retorno = new RadarBIDAO().Gravar(_radar);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (retorno)
                {
                    RadarBI _radar = new RadarBI();

                    _radar = new RadarBIDAO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas.GetDescription(Publicas.RadarBI.DiasUteis, ""), _data);
                    if (_radar.Existe)
                        _radar.PercentualAnterior = _radar.Percentual;
                    else
                    {
                        _radar.EmpresaFilial = _empresa.CodigoEmpresaGlobus;
                        _radar.Data = _data;
                        _radar.Grupo = Publicas.GetDescription(Publicas.RadarBI.DiasUteis, "");
                        _radar.Ordem = Convert.ToInt32(Publicas.GetDescription(Publicas.OrdemRadarBI.DiasUteis, ""));
                        _radar.Tipo = Publicas.GetDescription(Publicas.TipoRadarBI.DiasUteis, "");
                    }

                    _radar.Percentual = dias;
                    retorno = new RadarBIDAO().Gravar(_radar);
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

        public bool Excluir(List<ValoresDasMetas> _lista)
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

                    query.Append("delete niff_ads_valoresmetas");
                    query.Append(" where Id = " + item.Id);

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                    {
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

        public decimal Receitas(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(nvl(SaldoAcumulado,0)) Valor");
                query.Append("  from (Select Sum(s.vlcreditosaldo) - Sum(s.vldebitosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                #region empresas
                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");
                #endregion
                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal Despesas(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;


            try
            {
                query.Append("Select Round(Sum(nvl(SaldoAcumulado,0)),2) Valor");
                query.Append("  from (Select Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                #region empresas
                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                #endregion

                query.Append(SqlDesoneracao(Empresa, referencia, idMeta));

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }



        public decimal ReceitaBruta (string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(nvl(Abs(SaldoAcumulado),0)) Valor");
                query.Append("  from (Select Sum(s.vlcreditosaldo) - Sum(s.vldebitosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal ReceitaSubsidio(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(nvl(Abs(SaldoAcumulado),0)) Valor");
                query.Append("  from (Select Sum(s.vlcreditosaldo) - Sum(s.vldebitosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal DeducoesSobreReceita(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(nvl(Abs(SaldoAcumulado),0)) Valor");
                query.Append("  from (Select Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo)  saldoAcumulado"); 
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        private StringBuilder SqlDesoneracao(string Empresa, string referencia, int idMeta)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            string formula = "";

            List<MetasContasContabeis> _contas = new MetasDAO().ListarContasMetas(idMeta);

            formula = _contas.Max(s => s.Formula);

            if (!string.IsNullOrEmpty(formula))
            {
                string[] aux = formula.Split('*');
                string conta3 = aux[1].Replace("(", "").Replace(")", "");
                aux = aux[0].Split('/');
                string conta1 = aux[0].Replace("(", "").Replace(")", "");
                string conta2 = aux[1].Replace("(", "").Replace(")", "");

                query.Append(" Union All");
                query.Append("        Select Decode(Cta02, 0, 0, (Cta01 / Cta02) * Cta03) SaldoAcumulado");
                query.Append("          from (Select Sum(Cta01) Cta01, Sum(Cta02) Cta02, Sum(Cta03) Cta03 ");
                query.Append("                  from (Select Sum(s.vldebitosaldo) - Sum(s.vlcreditosaldo) Cta01,");
                query.Append("                               0 Cta02, 0 Cta03");
                query.Append("                          from Ctbsaldo s, ctbconta c");
                query.Append("                         Where s.nroplano = 10");
                query.Append("                           And s.periodosaldo = " + referencia);
                query.Append("                           And s.nroplano = c.nroplano");
                query.Append("                           And s.codcontactb = c.codcontactb");
                query.Append("                           And c.codcontactb in (" + conta1 + ")");
                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");


                query.Append("                         Union All");

                query.Append("                        Select 0 Cta01,");
                query.Append("                               Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) Cta02, 0 Cta03");
                query.Append("                          from Ctbsaldo s, ctbconta c");
                query.Append("                         Where s.nroplano = 10");
                query.Append("                           And s.periodosaldo = " + referencia);
                query.Append("                           And s.nroplano = c.nroplano");
                query.Append("                           And s.codcontactb = c.codcontactb");
                query.Append("                           And c.codcontactb In (" + conta2 + ")");
                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("                         Union All");
                query.Append("                        Select 0 Cta01,");
                query.Append("                               0 Cta02, Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) Cta03");
                query.Append("                          from Ctbsaldo s, ctbconta c");
                query.Append("                         Where s.nroplano = 10");
                query.Append("                           And s.periodosaldo = " + referencia);
                query.Append("                           And s.nroplano = c.nroplano");
                query.Append("                           And s.codcontactb = c.codcontactb");
                query.Append("                           And c.codcontactb in (" + conta3 + ")");
                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("       ))");
            }
            return query;
        }

        public decimal FolhaAdministracao(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;
           

            try
            {
                query.Append("Select Round(Sum(nvl(Abs(SaldoAcumulado),0)),2) Valor");
                query.Append("  from (Select Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                //query.Append("           And c.codcontactb In (50416,50417,50418,50419,50422,50424,50426,50428,50429,50430, 50450, 50466, ");
                //query.Append("           50472, 50482, 50485, 50486, 50487, 50488, 50496, 50539, 50603, 50647, 50656, 50676, 50677, 50696)");

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                //query.Append("         Union all ");

                query.Append(SqlDesoneracao(Empresa, referencia, idMeta));
                
                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal FolhaOperacao(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Round(Sum(nvl(Abs(SaldoAcumulado),0)),2) Valor");
                query.Append("  from (Select Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                //query.Append("         Union all ");

                query.Append(SqlDesoneracao(Empresa, referencia, idMeta));

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal FolhaManutencao(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Round(Sum(nvl(Abs(SaldoAcumulado),0)),2) Valor");
                query.Append("  from (Select Sum(s.vldebitosaldo) -  Sum(s.vlcreditosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                //query.Append("         Union all ");

                query.Append(SqlDesoneracao(Empresa, referencia, idMeta));
                
                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal ManutencaoFrota (string Empresa, string referencia, int idMeta, int idEmpresa) 
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(nvl(Abs(SaldoAcumulado),0)) Valor");
                query.Append("  from (Select Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal OutrosCustosDespesas(string Empresa, string referencia, int idMeta, int idEmpresa) 
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(nvl(Abs(SaldoAcumulado),0)) Valor");
                query.Append("  from (Select Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                //query.Append("         Union all ");

                query.Append(SqlDesoneracao(Empresa, referencia, idMeta));

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal Pecas(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(nvl(Abs(SaldoAcumulado),0)) Valor");
                query.Append("  from (Select Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal Pneus(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(nvl(Abs(SaldoAcumulado),0)) Valor");
                query.Append("  from (Select Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal HorasExtrasOperacao(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(nvl(Abs(SaldoAcumulado),0)) Valor");
                query.Append("  from (Select Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal HorasExtrasAdministracao(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(nvl(Abs(SaldoAcumulado),0)) Valor");
                query.Append("  from (Select Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal HorasExtrasManutencao(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(nvl(Abs(SaldoAcumulado),0)) Valor");
                query.Append("  from (Select Sum(s.vldebitosaldo)- Sum(s.vlcreditosaldo) saldoAcumulado");
                query.Append("          from Ctbsaldo s, ctbconta c, Niff_Ads_Metascontasctb mc");
                query.Append("         Where s.nroplano = mc.Nroplano");
                query.Append("           And s.periodosaldo = " + referencia);
                query.Append("           And s.nroplano = c.nroplano");
                query.Append("           And s.codcontactb = c.codcontactb");
                query.Append("           And c.codcontactb = mc.codcontactb");
                query.Append("           And c.nroplano = mc.nroplano");
                query.Append("           And mc.Idempresa = " + idEmpresa);
                query.Append("           And mc.idMetas = " + idMeta);

                if (Empresa == "001/001")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");

                if (Empresa == "001/002")
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");

                if (Empresa == "002/001")
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");

                if (Empresa == "003/001")
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");

                if (Empresa == "004/001")
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");

                if (Empresa == "005/001")
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");

                if (Empresa == "006/001")
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");

                if (Empresa == "009/001")
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");

                if (Empresa == "013/001")
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");

                if (Empresa == "026/001")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1 ");

                if (Empresa == "026/003")
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("       )");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal KmRodado(string Empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select sum(s.kmpercorridoveloc) Valor");
                query.Append("  from bgm_velocimetro s");
                query.Append(" Where s.dataveloc Between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')"); 

                if (Empresa == "001/001")
                {
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");
                }

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal KmRodadoModal(string Empresa, DateTime inicio, DateTime fim, int IdMetas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select sum(s.kmpercorridoveloc) Valor");
                query.Append("  from bgm_velocimetro s, frt_cadveiculos v, frt_tipodefrota f, niff_ads_metas t");
                query.Append(" Where s.dataveloc Between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("   And f.codigotpfrota = v.codigotpfrota");
                query.Append("   And v.codigoveic = s.codigoveic");
                query.Append("   And t.Idmetas = " + IdMetas);
                query.Append("   And ',' || t.Tipofrota || ',' Like '%,' || f.Codigotpfrota || ','");

                if (Empresa == "001/001")
                {
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And s.codigoEmpresa = 1 and s.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And s.codigoEmpresa = 2 and s.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And s.codigoEmpresa = 3 and (s.CodigoFl = 1 or s.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And s.codigoEmpresa = 4 and s.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And s.codigoEmpresa = 5 and s.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And s.codigoEmpresa = 6 and s.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And s.codigoEmpresa = 9 and s.CodigoFl = 1");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And s.codigoEmpresa = 13 and s.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And s.codigoEmpresa = 26 and s.CodigoFl = 3 ");
                }

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal MKBF(string Empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                // buscar por tipo de frota
                query.Append("Select Nvl(Sum(totalRaSos),0) Valor");
                query.Append("  from (Select count(distinct m.numeroos) totalRaSos, 0 totalkm ");
                query.Append("          from man_os m");
                query.Append("         Where m.dataaberturaos Between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("           And m.codorigos in (2,3)");

                if (Empresa == "001/001")
                {
                    query.Append("           And m.codigoEmpresa = 1 and m.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And m.codigoEmpresa = 1 and m.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And m.codigoEmpresa = 2 and m.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And m.codigoEmpresa = 3 and (m.CodigoFl = 1 or m.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And m.codigoEmpresa = 4 and m.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And m.codigoEmpresa = 5 and m.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And m.codigoEmpresa = 6 and m.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And m.codigoEmpresa = 9 and m.CodigoFl = 1");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And m.codigoEmpresa = 13 and m.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And m.codigoEmpresa = 26 and m.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And m.codigoEmpresa = 26 and m.CodigoFl = 3 ");
                }

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("           )");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal MKBFModal(string Empresa, DateTime inicio, DateTime fim, int IdMetas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                
                query.Append("Select Nvl(Sum(totalRaSos),0) Valor");
                query.Append("  from (Select count(distinct m.numeroos) totalRaSos");
                query.Append("          from man_os m, frt_cadveiculos v, frt_tipodefrota f, niff_ads_metas t");
                query.Append("         Where m.dataaberturaos Between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("           And m.codorigos in (2,3)");
                query.Append("           And f.codigotpfrota = v.codigotpfrota");
                query.Append("           And v.codigoveic = m.codigoveic");
                query.Append("           And t.Idmetas = " + IdMetas);
                query.Append("           And ',' || t.Tipofrota || ',' Like '%,' || f.Codigotpfrota || ','");

                if (Empresa == "001/001")
                {
                    query.Append("           And m.codigoEmpresa = 1 and m.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And m.codigoEmpresa = 1 and m.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And m.codigoEmpresa = 2 and m.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And m.codigoEmpresa = 3 and (m.CodigoFl = 1 or m.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And m.codigoEmpresa = 4 and m.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And m.codigoEmpresa = 5 and m.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And m.codigoEmpresa = 6 and m.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And m.codigoEmpresa = 9 and m.CodigoFl = 1");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And m.codigoEmpresa = 13 and m.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And m.codigoEmpresa = 26 and m.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And m.codigoEmpresa = 26 and m.CodigoFl = 3 ");
                }

                if (Empresa == "029/001")
                    query.Append("           And s.codigoEmpresa = 29 and s.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And s.codigoEmpresa = 31 and s.CodigoFl = 1 ");

                query.Append("           )");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal ConsultarRadar(string Empresa, DateTime inicio, DateTime fim, string texto, string tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                if (tipo == "V")
                    query.Append("Select Valor");
                else
                    query.Append("Select Percentual Valor");

                query.Append("  from Pbi_Radar_Operacional m");
                query.Append(" Where EmpFil = '" + Empresa + "'");
                query.Append("   And Grupo = '" + texto + "'");
                query.Append("   And Data Between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal CNHVencidas(string Empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Count(*) Valor");
                query.Append("  from vwflp_doctos d, Vw_Funcionarios f");
                query.Append(" Where f.CODINTFUNC = d.CODINTFUNC");
                query.Append("   And f.SITUACAOFUNC = 'A'");
                query.Append("   And Trunc(D.CNHVENCTO)+30 Between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("   And f.CODFUNCAO In(Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                       Where m.Idmetas = i.Idmetas");
                query.Append("                         And Upper(m.descricao)  Like '%CNH%'");
                query.Append("                         And i.tipo = 'F')");

                if (Empresa == "001/001")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And f.codigoEmpresa = 2 and f.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And f.codigoEmpresa = 3 and (f.CodigoFl = 1 or f.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And f.codigoEmpresa = 4 and f.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And f.codigoEmpresa = 5 and f.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And f.codigoEmpresa = 6 and f.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And f.codigoEmpresa = 9 and f.CodigoFl = 1");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And f.codigoEmpresa = 13 and f.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 3 ");
                }
                if (Empresa == "029/001")
                    query.Append("           And f.codigoEmpresa = 29 and f.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And f.codigoEmpresa = 31 and f.CodigoFl = 1 ");


                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;


        }

        public decimal Avarias(string Empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select count(f.nomefunc) Valor");
                query.Append("  from flp_historico t, flp_funcionarios f");
                query.Append(" Where f.CODINTFUNC = t.CODINTFUNC");
                query.Append("   And Trunc(t.dthist) Between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("   And t.codocorr In (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                       Where m.Idmetas = i.Idmetas");
                query.Append("                         And Upper(m.descricao)  Like '%AVARIA%'");
                query.Append("                         And i.tipo = 'O')");

                if (Empresa == "001/001")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And f.codigoEmpresa = 2 and f.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And f.codigoEmpresa = 3 and (f.CodigoFl = 1 or f.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And f.codigoEmpresa = 4 and f.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And f.codigoEmpresa = 5 and f.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And f.codigoEmpresa = 6 and f.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And f.codigoEmpresa = 9 and f.CodigoFl = 2");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And f.codigoEmpresa = 13 and f.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 3 ");
                }
                if (Empresa == "029/001")
                    query.Append("           And f.codigoEmpresa = 29 and f.CodigoFl = 1 ");

                if (Empresa == "031/001")
                    query.Append("           And f.codigoEmpresa = 31 and f.CodigoFl = 1 ");


                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal Acidentes(string Empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select count(f.nomefunc) Valor");
                query.Append("  from flp_historico t, flp_funcionarios f");
                query.Append(" Where f.CODINTFUNC = t.CODINTFUNC");
                query.Append("   And Trunc(t.dthist) Between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("   And t.codocorr In (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                       Where m.Idmetas = i.Idmetas");
                query.Append("                         And Upper(m.descricao)  Like '%ACIDENTE%'");
                query.Append("                         And i.tipo = 'O')");

                if (Empresa == "001/001")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And f.codigoEmpresa = 2 and f.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And f.codigoEmpresa = 3 and (f.CodigoFl = 1 or f.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And f.codigoEmpresa = 4 and f.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And f.codigoEmpresa = 5 and f.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And f.codigoEmpresa = 6 and f.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And f.codigoEmpresa = 9 and f.CodigoFl = 2");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And f.codigoEmpresa = 13 and f.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 3 ");
                }
                if (Empresa == "029/001")
                {
                    query.Append("           And f.codigoEmpresa = 29 and f.CodigoFl = 1 ");
                }
                if (Empresa == "031/001")
                    query.Append("           And f.codigoEmpresa = 31 and f.CodigoFl = 1 ");     

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal FuncionariosAtivos(string Empresa, DateTime inicio, DateTime fim, string texto, bool ignorarAprendiz)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select count(*) Valor");
                query.Append("  from (");

                query.Append("Select FUNC_TRAZSITUACAOFUNC(f.codintfunc, To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy'), 'F', Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit");
                query.Append("     , fc_niff_retornaAreaAnterior(f.CODINTFUNC, " + fim.Year.ToString() + fim.Month.ToString("00") + ") CodArea  ");
                query.Append("     , fc_niff_retornaFuncaoAnterior(f.CODINTFUNC, " + fim.Year.ToString() + fim.Month.ToString("00") + ") CodFuncao  ");
                query.Append("  from vw_funcionarios f");
                query.Append(" Where ");

                if (Empresa == "001/001")
                {
                    query.Append("   f.codigoEmpresa = 1 and f.CodigoFl = 1");
                }
                if (Empresa == "001/002")
                {
                    query.Append("   f.codigoEmpresa = 1 and f.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("    f.codigoEmpresa = 2 and f.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("    f.codigoEmpresa = 3 and (f.CodigoFl = 1 or f.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("    f.codigoEmpresa = 4 and f.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("    f.codigoEmpresa = 5 and f.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("    f.codigoEmpresa = 6 and f.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("   f.codigoEmpresa = 9 and f.CodigoFl = 2");
                }
                if (Empresa == "013/001")
                {
                    query.Append("   f.codigoEmpresa = 13 and f.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("   f.codigoEmpresa = 26 and f.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("   f.codigoEmpresa = 26 and f.CodigoFl = 3 ");
                }
                if (Empresa == "029/001")
                {
                    query.Append("   f.codigoEmpresa = 29 and f.CodigoFl = 1 ");
                }                    

                if (Empresa == "031/001")
                    query.Append("   f.codigoEmpresa = 31 and f.CodigoFl = 1 ");


                query.Append(" ) ");

                query.Append(" where Sit = 'A'");
                query.Append("   And CodArea in (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                       Where m.Idmetas = i.Idmetas");
                query.Append("                         And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                query.Append("                         And i.tipo = 'A')");

                // Absenteísmo traz apenas os funcinários ativos e que tenham ficha financeira ativa.
                // Turn over traz todos funcionarios que tenham ficha financeira ativa e não traz os aprendizes
                if (ignorarAprendiz)
                {
                    query.Append("   And CodFuncao not in (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                    query.Append("                            Where m.Idmetas = i.Idmetas");
                    query.Append("                              And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                    query.Append("                              And i.tipo = 'F')");
                }


                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                }

                if (valor == 0)
                {
                    query.Clear();
                    query.Append("Select count(*) Valor");
                    query.Append("  from (");
                    query.Append("Select Func_Trazsituacaofunc(f.Codintfunc, Last_Day(trunc(Sysdate)), 'F', Null, f.Dtadmfunc, f.Situacaofunc, f.Dttransffunc) Sit");
                    query.Append("  from vw_funcionarios f");
                    query.Append(" Where f.CodArea in (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                    query.Append("                       Where m.Idmetas = i.Idmetas");
                    query.Append("                         And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                    query.Append("                         And i.tipo = 'A')");

                    if (ignorarAprendiz)
                    {
                        query.Append("   And f.CodFuncao not in (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                        query.Append("                            Where m.Idmetas = i.Idmetas");
                        query.Append("                              And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                        query.Append("                              And i.tipo = 'F')");
                    }

                    if (Empresa == "001/001")
                    {
                        query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 1");
                    }
                    if (Empresa == "001/002")
                    {
                        query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 2");
                    }
                    if (Empresa == "002/001")
                    {
                        query.Append("           And f.codigoEmpresa = 2 and f.CodigoFl = 1");
                    }
                    if (Empresa == "003/001")
                    {
                        query.Append("           And f.codigoEmpresa = 3 and (f.CodigoFl = 1 or f.CodigoFl = 15)");
                    }
                    if (Empresa == "004/001")
                    {
                        query.Append("           And f.codigoEmpresa = 4 and f.CodigoFl = 1");
                    }
                    if (Empresa == "005/001")
                    {
                        query.Append("           And f.codigoEmpresa = 5 and f.CodigoFl = 1");
                    }
                    if (Empresa == "006/001")
                    {
                        query.Append("           And f.codigoEmpresa = 6 and f.CodigoFl = 1");
                    }
                    if (Empresa == "009/001")
                    {
                        query.Append("           And f.codigoEmpresa = 9 and f.CodigoFl = 2");
                    }
                    if (Empresa == "013/001")
                    {
                        query.Append("           And f.codigoEmpresa = 13 and f.CodigoFl = 1");
                    }
                    if (Empresa == "026/001")
                    {
                        query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 1");
                    }
                    if (Empresa == "026/003")
                    {
                        query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 3 ");
                    }
                    if (Empresa == "029/001")
                    {
                        query.Append("           And f.codigoEmpresa = 29 and f.CodigoFl = 1 ");
                    }
                    if (Empresa == "031/001")
                        query.Append("           And f.codigoEmpresa = 31 and f.CodigoFl = 1 ");
                    
                    query.Append(" ) ");

                    query.Append(" where Sit = 'A'");
                    executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal Absenteismo(string Empresa, DateTime inicio, DateTime fim, string texto)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select count(f.nomefunc) Valor");
                query.Append("  from flp_historico t, vw_funcionarios f");
                query.Append(" Where f.CODINTFUNC = t.CODINTFUNC");
                query.Append("   And Trunc(t.dthist) Between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("   And (t.codocorr In (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                       Where m.Idmetas = i.Idmetas");
                query.Append("                         And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                query.Append("                         And i.tipo = 'O')");

                if (texto.ToUpper() == "ABSENTEÍSMO EMPRESA")
                    query.Append("    or (t.CodOcorr in (552,553) and f.CodArea = 40))");
                else
                    query.Append("    )");

               query.Append("   And f.CodArea in (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                       Where m.Idmetas = i.Idmetas");
                query.Append("                         And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                query.Append("                         And i.tipo = 'A')");

                if (Empresa == "001/001")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And f.codigoEmpresa = 2 and f.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And f.codigoEmpresa = 3 and (f.CodigoFl = 1 or f.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And f.codigoEmpresa = 4 and f.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And f.codigoEmpresa = 5 and f.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And f.codigoEmpresa = 6 and f.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And f.codigoEmpresa = 9 and f.CodigoFl = 2");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And f.codigoEmpresa = 13 and f.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And f.codigoEmpresa = 26  and f.CodigoFl = 3 ");
                }
                if (Empresa == "029/001")
                {
                    query.Append("           And f.codigoEmpresa = 29 and f.CodigoFl = 1 ");
                }
                if (Empresa == "031/001")
                    query.Append("           And f.codigoEmpresa = 31 and f.CodigoFl = 1 ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal FuncionariosAdmitidos(string Empresa, DateTime inicio, DateTime fim, string texto)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(Valor) Valor");
                query.Append("   from (Select count(f.nomefunc) Valor");
                query.Append("           from vw_funcionarios f");
                query.Append("          Where Trunc(f.dttransffunc) Between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("            And f.CodFuncao not in (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                                Where m.Idmetas = i.Idmetas");
                query.Append("                                  And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                query.Append("                                  And i.tipo = 'F')");
                query.Append("            And f.CodArea in (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                                Where m.Idmetas = i.Idmetas");
                query.Append("                                  And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                query.Append("                                  And i.tipo = 'A')");


                if (Empresa == "001/001")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And f.codigoEmpresa = 2 and f.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And f.codigoEmpresa = 3 and (f.CodigoFl = 1 or f.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And f.codigoEmpresa = 4 and f.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And f.codigoEmpresa = 5 and f.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And f.codigoEmpresa = 6 and f.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And f.codigoEmpresa = 9 and f.CodigoFl = 2");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And f.codigoEmpresa = 13 and f.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 3 ");
                }
                if (Empresa == "029/001")
                {
                    query.Append("           And f.codigoEmpresa = 29 and f.CodigoFl = 1 ");
                }
                if (Empresa == "031/001")
                    query.Append("           And f.codigoEmpresa = 31 and f.CodigoFl = 1 ");

                query.Append("          Union All");

                query.Append("         Select count(f.nomefunc) Valor");
                query.Append("           from vw_funcionarios f");
                query.Append("          Where Trunc(f.DTADMFUNC) Between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("            And f.CodFuncao not in (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                                Where m.Idmetas = i.Idmetas");
                query.Append("                                  And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                query.Append("                                  And i.tipo = 'F')");
                query.Append("            And f.CodArea in (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                                Where m.Idmetas = i.Idmetas");
                query.Append("                                  And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                query.Append("                                  And i.tipo = 'A')");

                if (Empresa == "001/001")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And f.codigoEmpresa = 2 and f.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And f.codigoEmpresa = 3 and (f.CodigoFl = 1 or f.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And f.codigoEmpresa = 4 and f.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And f.codigoEmpresa = 5 and f.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And f.codigoEmpresa = 6 and f.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And f.codigoEmpresa = 9 and f.CodigoFl = 2");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And f.codigoEmpresa = 13 and f.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 3 ");
                }
                if (Empresa == "029/001")
                {
                    query.Append("           And f.codigoEmpresa = 29 and f.CodigoFl = 1 ");
                }
                if (Empresa == "031/001")
                    query.Append("           And f.codigoEmpresa = 31 and f.CodigoFl = 1 ");

                query.Append(")");

               Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal FuncionariosDemitidos(string Empresa, DateTime inicio, DateTime fim, string texto)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select count(f.nomefunc) Valor");
                query.Append("  from flp_quitacao q, vw_funcionarios f");
                query.Append(" Where f.CODINTFUNC = q.CODINTFUNC");
                query.Append("   and q.statusquita = 'N'");
                query.Append("   And Trunc(q.dtdesligquita) Between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_Date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("   And f.CodFuncao not in (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                       Where m.Idmetas = i.Idmetas");
                query.Append("                         And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                query.Append("                         And i.tipo = 'F')");
                query.Append("   And f.CodArea in (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                       Where m.Idmetas = i.Idmetas");
                query.Append("                         And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                query.Append("                         And i.tipo = 'A')");


                if (Empresa == "001/001")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And f.codigoEmpresa = 2 and f.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And f.codigoEmpresa = 3 and (f.CodigoFl = 1 or f.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And f.codigoEmpresa = 4 and f.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And f.codigoEmpresa = 5 and f.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And f.codigoEmpresa = 6 and f.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And f.codigoEmpresa = 9 and f.CodigoFl = 2");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And f.codigoEmpresa = 13 and f.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 3 ");
                }
                if (Empresa == "029/001")
                {
                    query.Append("           And f.codigoEmpresa = 29 and f.CodigoFl = 1 ");
                }
                if (Empresa == "031/001")
                    query.Append("           And f.codigoEmpresa = 31 and f.CodigoFl = 1 ");


                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal ValorPorEvento(string Empresa, DateTime competencia, string texto, string evento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select Sum(e.ValorFicha) /1000 Valor");
                query.Append("  from flp_fichaeventos e, vw_funcionarios f");
                query.Append(" Where e.codevento in (" + evento + ")");
                query.Append("   And e.competficha = Last_day(To_date('" + competencia.ToShortDateString() + "','dd/mm/yyyy'))");
                query.Append("   And e.codintfunc = f.CODINTFUNC");
                query.Append("   And e.tipofolha = 1");

                query.Append("   And f.CodArea in (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m");
                query.Append("                       Where m.Idmetas = i.Idmetas");
                query.Append("                         And Upper(m.descricao)  Like '%" + texto.ToUpper() + "%'");
                query.Append("                         And i.tipo = 'A')");
                

                if (Empresa == "001/001")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And f.codigoEmpresa = 1 and f.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And f.codigoEmpresa = 2 and f.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And f.codigoEmpresa = 3 and (f.CodigoFl = 1 or f.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And f.codigoEmpresa = 4 and f.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And f.codigoEmpresa = 5 and f.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And f.codigoEmpresa = 6 and f.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And f.codigoEmpresa = 9 and f.CodigoFl = 2");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And f.codigoEmpresa = 13 and f.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And f.codigoEmpresa = 26 and f.CodigoFl = 3 ");
                }
                if (Empresa == "029/001")
                {
                    query.Append("           And f.codigoEmpresa = 29 and f.CodigoFl = 1 ");
                }
                if (Empresa == "031/001")
                    query.Append("           And f.codigoEmpresa = 31 and f.CodigoFl = 1 ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal LitrosConsumidos(string Empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select nvl(Sum(im.qtdeitensmovto),0) Valor");
                query.Append("  from EST_ITENSDEESTOQUE I, EST_CADMATERIAL M, CTR_CADLOCAL L, CTR_CADEMP C");
                query.Append("     , CTR_EMPAUTORIZADAS E, CTR_FILIAL CL, CTR_EMPAUTORIZADAS EL, Est_Itensmovto IM");
                query.Append("     , Est_movto MV, Est_Historicomovto H");
                query.Append(" Where M.CODIGOGRD In 520");
                query.Append("   And EL.CODINTEMPAUT = CL.CODINTEMPAUT");
                query.Append("   And E.CODINTEMPAUT = C.CODINTEMPAUT");
                query.Append("   And CL.CODIGOFL = L.CODIGOFL");
                query.Append("   And CL.CODIGOEMPRESA = L.CODIGOEMPRESA");
                query.Append("   And C.CODIGOEMPRESA = L.CODIGOEMPRESA ");
                query.Append("   And L.CODIGOLOCAL = I.CODIGOLOCAL ");
                query.Append("   And I.CODIGOMATINT = M.CODIGOMATINT");
                query.Append("   And IM.Seqmovto = MV.Seqmovto");
                query.Append("   And IM.Datamovto = Mv.Datamovto ");
                query.Append("   And H.Codigohismov = MV.Codigohismov ");
                query.Append("   And IM.Codigomatint = m.codigomatint");
                query.Append("   And im.codigolocal = l.codigolocal");
                query.Append("   And mv.codigoempresa = l.codigoempresa");
                query.Append("   And mv.codigofl = l.codigofl");
                query.Append("   And 'DE,TS,SA,SV,SI,RA' Like '%' || h.tipohismov || '%'");
                query.Append("   And mv.datamovto Between To_Date('" + inicio.ToShortDateString() + "','dd/mm/yyyy')  and To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy')");


                if (Empresa == "001/001")
                {
                    query.Append("           And l.codigoEmpresa = 1 and l.CodigoFl = 1");
                }

                if (Empresa == "001/002")
                {
                    query.Append("           And l.codigoEmpresa = 1 and l.CodigoFl = 2");
                }
                if (Empresa == "002/001")
                {
                    query.Append("           And l.codigoEmpresa = 2 and l.CodigoFl = 1");
                }
                if (Empresa == "003/001")
                {
                    query.Append("           And l.codigoEmpresa = 3 and (l.CodigoFl = 1 or l.CodigoFl = 15)");
                }
                if (Empresa == "004/001")
                {
                    query.Append("           And l.codigoEmpresa = 4 and l.CodigoFl = 1");
                }
                if (Empresa == "005/001")
                {
                    query.Append("           And l.codigoEmpresa = 5 and l.CodigoFl = 1");
                }
                if (Empresa == "006/001")
                {
                    query.Append("           And l.codigoEmpresa = 6 and l.CodigoFl = 1");
                }
                if (Empresa == "009/001")
                {
                    query.Append("           And l.codigoEmpresa = 9 and l.CodigoFl = 1");
                }
                if (Empresa == "013/001")
                {
                    query.Append("           And l.codigoEmpresa = 13 and l.CodigoFl = 1");
                }
                if (Empresa == "026/001")
                {
                    query.Append("           And l.codigoEmpresa = 26 and l.CodigoFl = 1");
                }
                if (Empresa == "026/003")
                {
                    query.Append("           And l.codigoEmpresa = 26 and l.CodigoFl = 3 ");
                }
                if (Empresa == "029/001")
                    query.Append("           And l.codigoEmpresa = 29 and l.CodigoFl = 1 ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal Gratuidade(string Empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select SUM(Decode(A.COD_TIPOPAGTARIFA, '403',0, Decode(A.COD_TIPOPAGTARIFA, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) Valor");
                query.Append("  from T_ARR_GUIA G, T_ARR_DETALHE_GUIA A, T_TRF_TIPOPAGTO P");
                query.Append(" Where P.COD_TIPOPAGTO = A.COD_TIPOPAGTARIFA");
                query.Append("   And G.COD_SEQ_GUIA  = A.COD_SEQ_GUIA");
                query.Append("   And upper(p.nom_descricao) Not Like '%INT%'");
                query.Append("   And A.VLR_RECEB = 0 ");
                query.Append("   And g.dat_prest_contas Between To_Date('" + inicio.ToShortDateString() + "','dd/mm/yyyy')  and To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy')");
                
                if (Empresa == "001/001")
                    query.Append("           And G.Cod_Empresa = 1 and G.CodigoFl = 1");
                if (Empresa == "001/002")
                    query.Append("           And G.Cod_Empresa = 1 and G.CodigoFl = 2");
                if (Empresa == "002/001")
                    query.Append("           And G.Cod_Empresa = 2 and G.CodigoFl = 1");
                if (Empresa == "003/001")
                    query.Append("           And G.Cod_Empresa = 3 and (G.CodigoFl = 1 or G.CodigoFl = 15)");
                if (Empresa == "004/001")
                    query.Append("           And G.Cod_Empresa = 4 and G.CodigoFl = 1");
                if (Empresa == "005/001")
                    query.Append("           And G.Cod_Empresa = 5 and G.CodigoFl = 1");
                if (Empresa == "006/001")
                    query.Append("           And G.Cod_Empresa = 6 and G.CodigoFl = 1");
                if (Empresa == "009/001")
                    query.Append("           And G.Cod_Empresa = 9 and G.CodigoFl = 1");
                if (Empresa == "013/001")
                    query.Append("           And G.Cod_Empresa = 13 and G.CodigoFl = 1");
                if (Empresa == "026/001")
                    query.Append("           And G.Cod_Empresa = 26 and G.CodigoFl = 1");
                if (Empresa == "026/003")
                    query.Append("           And G.Cod_Empresa = 26 and G.CodigoFl = 3 ");
                if (Empresa == "029/001")
                    query.Append("           And G.Cod_Empresa = 29 and G.CodigoFl = 1 ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal IntegracoesSemValor(string Empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select SUM(Decode(A.COD_TIPOPAGTARIFA, '403',0, Decode(A.COD_TIPOPAGTARIFA, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) Valor");
                query.Append("  from T_ARR_GUIA G, T_ARR_DETALHE_GUIA A, T_TRF_TIPOPAGTO P");
                query.Append(" Where P.COD_TIPOPAGTO = A.COD_TIPOPAGTARIFA");
                query.Append("   And G.COD_SEQ_GUIA  = A.COD_SEQ_GUIA");
                query.Append("   And upper(p.nom_descricao) Like '%INT%'");
                query.Append("   And A.VLR_RECEB = 0 ");
                query.Append("   And g.dat_prest_contas Between To_Date('" + inicio.ToShortDateString() + "','dd/mm/yyyy')  and To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy')");

                if (Empresa == "001/001")
                    query.Append("           And G.Cod_Empresa = 1 and G.CodigoFl = 1");
                if (Empresa == "001/002")
                    query.Append("           And G.Cod_Empresa = 1 and G.CodigoFl = 2");
                if (Empresa == "002/001")
                    query.Append("           And G.Cod_Empresa = 2 and G.CodigoFl = 1");
                if (Empresa == "003/001")
                    query.Append("           And G.Cod_Empresa = 3 and (G.CodigoFl = 1 or G.CodigoFl = 15)");
                if (Empresa == "004/001")
                    query.Append("           And G.Cod_Empresa = 4 and G.CodigoFl = 1");
                if (Empresa == "005/001")
                    query.Append("           And G.Cod_Empresa = 5 and G.CodigoFl = 1");
                if (Empresa == "006/001")
                    query.Append("           And G.Cod_Empresa = 6 and G.CodigoFl = 1");
                if (Empresa == "009/001")
                    query.Append("           And G.Cod_Empresa = 9 and G.CodigoFl = 1");
                if (Empresa == "013/001")
                    query.Append("           And G.Cod_Empresa = 13 and G.CodigoFl = 1");
                if (Empresa == "026/001")
                    query.Append("           And G.Cod_Empresa = 26 and G.CodigoFl = 1");
                if (Empresa == "026/003")
                    query.Append("           And G.Cod_Empresa = 26 and G.CodigoFl = 3 ");
                if (Empresa == "029/001")
                    query.Append("           And G.Cod_Empresa = 29 and G.CodigoFl = 1 ");
                if (Empresa == "031/001")
                    query.Append("           And G.Cod_Empresa = 31 and G.CodigoFl = 1 ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

        public decimal Pagantes(string Empresa, DateTime inicio, DateTime fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Append("Select SUM(Decode(A.COD_TIPOPAGTARIFA, '403',0, Decode(A.COD_TIPOPAGTARIFA, 'X', -1, '956', -1,1)) * A.QTD_PASSAG_TRANS) Valor");
                query.Append("  from T_ARR_GUIA G, T_ARR_DETALHE_GUIA A, T_TRF_TIPOPAGTO P");
                query.Append(" Where P.COD_TIPOPAGTO = A.COD_TIPOPAGTARIFA");
                query.Append("   And G.COD_SEQ_GUIA  = A.COD_SEQ_GUIA");
                query.Append("   And A.VLR_RECEB <> 0 ");
                query.Append("   And g.dat_prest_contas Between To_Date('" + inicio.ToShortDateString() + "','dd/mm/yyyy')  and To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy')");

                if (Empresa == "001/001")
                    query.Append("           And G.Cod_Empresa = 1 and G.CodigoFl = 1");
                if (Empresa == "001/002")
                    query.Append("           And G.Cod_Empresa = 1 and G.CodigoFl = 2");
                if (Empresa == "002/001")
                    query.Append("           And G.Cod_Empresa = 2 and G.CodigoFl = 1");
                if (Empresa == "003/001")
                    query.Append("           And G.Cod_Empresa = 3 and (G.CodigoFl = 1 or G.CodigoFl = 15)");
                if (Empresa == "004/001")
                    query.Append("           And G.Cod_Empresa = 4 and G.CodigoFl = 1");
                if (Empresa == "005/001")
                    query.Append("           And G.Cod_Empresa = 5 and G.CodigoFl = 1");
                if (Empresa == "006/001")
                    query.Append("           And G.Cod_Empresa = 6 and G.CodigoFl = 1");
                if (Empresa == "009/001")
                    query.Append("           And G.Cod_Empresa = 9 and G.CodigoFl = 1");
                if (Empresa == "013/001")
                    query.Append("           And G.Cod_Empresa = 13 and G.CodigoFl = 1");
                if (Empresa == "026/001")
                    query.Append("           And G.Cod_Empresa = 26 and G.CodigoFl = 1");
                if (Empresa == "026/003")
                    query.Append("           And G.Cod_Empresa = 26 and G.CodigoFl = 3 ");
                if (Empresa == "029/001")
                    query.Append("           And G.Cod_Empresa = 29 and G.CodigoFl = 1 ");
                if (Empresa == "031/001")
                    query.Append("           And G.Cod_Empresa = 31 and G.CodigoFl = 1 ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        valor = Convert.ToDecimal(dataReader["Valor"].ToString());
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
            return valor;
        }

    }
}
