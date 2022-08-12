using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class Pontuacao9BoxDAO
    {
        IDataReader dataReader;

        private StringBuilder MontaConsulta()
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select idninebox, referencia, idcargo, idcolaborador, ativo, pontodominancia, pontoextroversao, pontopaciencia");
            query.Append("     , pontoformalidade, toleranciadominancia,  toleranciaextroversao, toleranciapaciencia, toleranciaformalidade, descontar");
            query.Append("  From Niff_Ads_PontuacaoNineBox ");

            return query;
        }

        private Pontuacao9Box RetornoConsulta(IDataReader data)
        {
            Pontuacao9Box _tipo = new Pontuacao9Box();

            _tipo.Existe = true;

            try
            {
                _tipo.ReferenciaFormatada = Convert.ToInt32(data["referencia"].ToString()).ToString("00/0000");
            }
            catch { }

            try
            {
                _tipo.Id = Convert.ToInt32(data["idninebox"].ToString());
            }
            catch { }

            try
            {
                _tipo.IdCargo = Convert.ToInt32(data["idcargo"].ToString());
            }
            catch { }

            try
            {
                _tipo.IdColaborador = Convert.ToInt32(data["idcolaborador"].ToString());
            }
            catch { }

            try
            {
                _tipo.Referencia = Convert.ToInt32(data["referencia"].ToString());
            }
            catch { }

            try
            {
                _tipo.Ativo = data["ativo"].ToString() == "S";
            }
            catch { }

            try
            {
                _tipo.PontoDominancia = Convert.ToDecimal(data["pontodominancia"].ToString());
            }
            catch { }
            try
            {
                _tipo.PontoExtroversao = Convert.ToDecimal(data["pontoextroversao"].ToString());
            }
            catch { }
            try
            {
                _tipo.PontoPaciencia = Convert.ToDecimal(data["pontopaciencia"].ToString());
            }
            catch { }
            try
            {
                _tipo.PontoFormalidade = Convert.ToDecimal(data["pontoformalidade"].ToString());
            }
            catch { }
            try
            {
                _tipo.ToleranciaDominancia = Convert.ToDecimal(data["toleranciadominancia"].ToString());
            }
            catch { }

            try
            {
                _tipo.ToleranciaExtroversao = Convert.ToDecimal(data["toleranciaextroversao"].ToString());
            }
            catch { }

            try
            {
                _tipo.ToleranciaPaciencia = Convert.ToDecimal(data["toleranciapaciencia"].ToString());
            }
            catch { }

            try
            {
                _tipo.ToleranciaFormalidade = Convert.ToDecimal(data["toleranciaformalidade"].ToString());
            }
            catch { }

            try
            {
                _tipo.Descontar = Convert.ToDecimal(data["descontar"].ToString());
            }
            catch { }

            return _tipo;
        }

        public List<Pontuacao9Box> Listar(bool apenasAtivo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Pontuacao9Box> _lista = new List<Pontuacao9Box>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query = MontaConsulta();

                if (apenasAtivo)
                    query.Append(" Where Ativo = 'S'");
                
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _lista.Add(RetornoConsulta(dataReader));
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

        public List<Pontuacao9Box> Listar(bool apenasAtivo, int idCargo, int idColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Pontuacao9Box> _lista = new List<Pontuacao9Box>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query = MontaConsulta();

                if (apenasAtivo)
                {
                    query.Append(" Where Ativo = 'S'");

                    if (idCargo != 0)
                        query.Append("   And IdCargo = " + idCargo);
                    else
                        query.Append("   And idColaborador = " + idColaborador);
                }
                else
                {
                    if (idCargo != 0)
                        query.Append(" Where IdCargo = " + idCargo);
                    else
                        query.Append(" Where idColaborador = " + idColaborador);
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        _lista.Add(RetornoConsulta(dataReader));
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

        public Pontuacao9Box Consultar(int Id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            Pontuacao9Box _tipo = new Pontuacao9Box();

            try
            {
                query = MontaConsulta();

                query.Append(" Where IDNINEBOX = " + Id);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo = RetornoConsulta(dataReader);
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

        public Pontuacao9Box Consultar(string referencia, int IdCargo, int IdColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            Pontuacao9Box _tipo = new Pontuacao9Box();

            try
            {
                query = MontaConsulta();

                query.Append(" Where Referencia = " + referencia);
                if (IdCargo != 0)
                    query.Append("   And IdCargo = " + IdCargo);
                else
                    query.Append("   And idColaborador = " + IdColaborador);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo = RetornoConsulta(dataReader);                        
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

        public bool Gravar(Pontuacao9Box _pontuacao)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();

                if (!_pontuacao.Existe)
                {
                    query.Append("Insert into Niff_Ads_PontuacaoNineBox");
                    query.Append(" ( idninebox, referencia ");

                    if (_pontuacao.IdCargo != 0)
                        query.Append(" , idcargo");
                    if (_pontuacao.IdColaborador != 0)
                        query.Append(" , idcolaborador");

                    query.Append("     , ativo, pontodominancia, pontoextroversao, pontopaciencia");
                    query.Append("     , pontoformalidade, toleranciadominancia, toleranciaextroversao, toleranciapaciencia, toleranciaformalidade, descontar )");
                    query.Append(" Values ( SQ_NIFF_AdsIdPontos9box.NextVal ");
                    query.Append("        , " + _pontuacao.Referencia);

                    if (_pontuacao.IdCargo != 0)
                        query.Append("        , " + _pontuacao.IdCargo);
                    if (_pontuacao.IdColaborador != 0)
                        query.Append("        , " + _pontuacao.IdColaborador);

                    query.Append("        , '" + (_pontuacao.Ativo ? "S" : "N") + "'");
                    query.Append("        , " + _pontuacao.PontoDominancia.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + _pontuacao.PontoExtroversao.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + _pontuacao.PontoPaciencia.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + _pontuacao.PontoFormalidade.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + _pontuacao.ToleranciaDominancia.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + _pontuacao.ToleranciaExtroversao.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + _pontuacao.ToleranciaPaciencia.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + _pontuacao.ToleranciaFormalidade.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + _pontuacao.Descontar.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_Ads_PontuacaoNineBox");
                    query.Append("   set Ativo = '" + (_pontuacao.Ativo ? "S" : "N") + "'");
                    query.Append("        , pontodominancia = " + _pontuacao.PontoDominancia.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , pontoextroversao = " + _pontuacao.PontoExtroversao.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , pontopaciencia = " + _pontuacao.PontoPaciencia.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , pontoformalidade  " + _pontuacao.PontoFormalidade.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , toleranciadominancia = " + _pontuacao.ToleranciaDominancia.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , toleranciaextroversao = " + _pontuacao.ToleranciaExtroversao.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , toleranciapaciencia = " + _pontuacao.ToleranciaPaciencia.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , toleranciaformalidade = " + _pontuacao.ToleranciaFormalidade.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , descontar = " + _pontuacao.Descontar.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" where idninebox = " + _pontuacao.Id);
                }

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

        public bool Excluir(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ads_PontuacaoNineBox");
                query.Append(" Where idninebox = " + codigo);
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
