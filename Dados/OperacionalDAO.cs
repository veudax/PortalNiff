using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class OperacionalDAO
    {
        IDataReader dadosReader;
        IDataReader dadosReader2;
        IDataReader dadosReader3;

        #region Indicadores

        public List<Operacional.Indicadores> Listar(bool somenteAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Indicadores> _lista = new List<Operacional.Indicadores>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select Id ");
                query.Append("     , Descricao");
                query.Append("     , ativo");
                query.Append("     , Valores");
                query.Append("     , Abreviado");

                query.Append("  From Niff_Ope_Indicadores ");

                if (somenteAtivos)
                    query.Append(" Where Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Operacional.Indicadores _indi = new Operacional.Indicadores();
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());

                        _indi.Descricao = dadosReader["Descricao"].ToString();
                        _indi.Abreviado = dadosReader["Abreviado"].ToString();
                        _indi.TipoDeValores = dadosReader["valores"].ToString();
                        _indi.Ativo = dadosReader["ativo"].ToString() == "S";
                        _indi.Existe = true;

                        _lista.Add(_indi);
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

        public Operacional.Indicadores Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Operacional.Indicadores _ind = new Operacional.Indicadores();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select Id ");
                query.Append("     , Descricao");
                query.Append("     , ativo");
                query.Append("     , Valores");
                query.Append("     , Abreviado");

                query.Append("  From Niff_Ope_Indicadores ");
                query.Append(" Where Id = " + codigo.ToString());

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {
                        _ind.Id = Convert.ToInt32(dadosReader["Id"].ToString());

                        _ind.Descricao = dadosReader["Descricao"].ToString();
                        _ind.Abreviado = dadosReader["Abreviado"].ToString();
                        _ind.TipoDeValores = dadosReader["valores"].ToString();
                        _ind.Ativo = dadosReader["ativo"].ToString() == "S";
                        _ind.Existe = true;
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
            return _ind;
        }

        public bool Grava(Operacional.Indicadores item)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!item.Existe)
                {
                    query.Clear();
                    query.Append("Insert into Niff_Ope_Indicadores");
                    query.Append("   (id, ativo, descricao, valores, abreviado) ");
                    query.Append("   Values (" + item.Id);
                    query.Append(", '" + (item.Ativo ? "S" : "N") + "'");
                    query.Append(", '" + item.Descricao + "'");
                    query.Append(", '" + item.TipoDeValores + "'");
                    query.Append(", '" + item.Abreviado + "' )");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Ope_Indicadores");
                    query.Append("   set descricao = '" + item.Descricao + "', ");
                    query.Append("       ativo = '" + (item.Ativo ? "S" : "N") + "', ");
                    query.Append("       Valores = '" + item.TipoDeValores + "', ");
                    query.Append("       abreviado = '" + item.Abreviado + "'");
                    query.Append(" Where id = " + item.Id);
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

        public bool Exclui(int Codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Categoria _empresa = new Categoria();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ope_Indicadores");
                query.Append(" Where id = " + Codigo);
                
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

        public int Proximo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select nvl(Max(Id),0) + 1 next From Niff_Ope_Indicadores");
                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                        retorno = Convert.ToInt32(dadosReader["next"].ToString());
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

        #endregion

        #region Setores

        public List<Operacional.Setor> ListarSetores(bool somenteAtivos, int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Setor> _lista = new List<Operacional.Setor>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, ativo, descricao, IdEmpresa, Codigo");
                query.Append("  From Niff_Ope_Setor ");

                if (!somenteAtivos)
                    query.Append(" Where IdEmpresa = " + empresa);
                else
                {
                    query.Append(" Where Ativo = 'S'");
                    query.Append("   and IdEmpresa = " + empresa);
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Operacional.Setor _indi = new Operacional.Setor();
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdEmpresa = Convert.ToInt32(dadosReader["IdEmpresa"].ToString());
                        _indi.Codigo = Convert.ToInt32(dadosReader["Codigo"].ToString());

                        _indi.Descricao = dadosReader["Descricao"].ToString();
                        _indi.Ativo = dadosReader["ativo"].ToString() == "S";
                        _indi.Existe = true;

                        _lista.Add(_indi);
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

        public Operacional.Setor ConsultaSetor(int codigo, int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Operacional.Setor _indi = new Operacional.Setor();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, ativo, descricao, IdEmpresa, Codigo");
                query.Append("  From Niff_Ope_Setor ");
                query.Append(" Where Codigo = " + codigo.ToString());
                query.Append("   and IdEmpresa = " + empresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdEmpresa = Convert.ToInt32(dadosReader["IdEmpresa"].ToString());
                        _indi.Codigo = Convert.ToInt32(dadosReader["Codigo"].ToString());

                        _indi.Descricao = dadosReader["Descricao"].ToString();
                        _indi.Ativo = dadosReader["ativo"].ToString() == "S";
                        _indi.Existe = true;
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
            return _indi;
        }

        public bool Grava(Operacional.Setor item, List<Operacional.Vigencia> vigencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool Retorno = true;
            try
            {
                if (!item.Existe)
                {
                    item.Id = ProximoSetor();

                    query.Clear();
                    query.Append("Insert into Niff_Ope_Setor");
                    query.Append("   (id, ativo, descricao, IdEmpresa, Codigo) ");
                    query.Append("   Values (" + item.Id);
                    query.Append(", '" + (item.Ativo ? "S" : "N") + "'");
                    query.Append(", '" + item.Descricao + "'");
                    query.Append(", " + item.IdEmpresa );
                    query.Append(", " + item.Codigo + ")");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Ope_Setor");
                    query.Append("   set descricao = '" + item.Descricao + "', ");
                    query.Append("       ativo = '" + (item.Ativo ? "S" : "N") + "'");
                    query.Append(" Where id = " + item.Id);
                }

                Retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (Retorno)
                {
                    vigencia.ForEach(u => u.IdSetor = item.Id);
                    Retorno = Grava(vigencia);
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
            return Retorno;
        }

        public bool ExcluiSetor(int Codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!ExcluiTodasAsVigenciasDoSetor(Codigo))
                    return false;
                else
                {
                    query.Append("Delete Niff_Ope_Setor");
                    query.Append(" Where id = " + Codigo);

                    return sessao.ExecuteSqlTransaction(query.ToString());
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

        public int ProximoSetor()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select nvl(Max(Id),0) + 1 next From Niff_Ope_Setor");
                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                        retorno = Convert.ToInt32(dadosReader["next"].ToString());
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

        public int ProximoCodigoSetorEmpresa(int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select nvl(Max(Codigo),0) + 1 next From Niff_Ope_Setor");
                query.Append(" where IdEmpresa = " + empresa);
                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                        retorno = Convert.ToInt32(dadosReader["next"].ToString());
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

        public List<Operacional.Vigencia> ListarVigencias(int setor, bool grid)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Vigencia> _lista = new List<Operacional.Vigencia>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                if (grid)
                    query.Append("Select vigencia");
                else
                    query.Append("Select id, idsetor, codintlinha, vigencia, TemCobrador");

                query.Append("  From Niff_Ope_Setorlinhas s ");
                query.Append(" Where IdSetor = " + setor);

                if (grid)
                    query.Append("Group by vigencia");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Operacional.Vigencia _indi = new Operacional.Vigencia();
                        _indi.Data = Convert.ToDateTime(dadosReader["Vigencia"].ToString());

                        if (!grid)
                        {
                            _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                            _indi.IdSetor = Convert.ToInt32(dadosReader["IdSetor"].ToString());

                            _indi.CodigoInternoLinha = Convert.ToInt32(dadosReader["codintlinha"].ToString());
                            _indi.TemCobrador = dadosReader["TemCobrador"].ToString() == "S";

                            Linha _linha = new LinhaDAO().Consultar(_indi.CodigoInternoLinha);

                            _indi.CodigoLinha = _linha.Codigo;
                            _indi.NomeLinha = _linha.Nome;
                            _indi.Classificacao = _linha.DescricaoClassificacao;
                        }
                        _indi.Existe = true;

                        _lista.Add(_indi);
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

        public List<Operacional.Vigencia> ListarLinhasVigencias(int setor, DateTime vigencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Vigencia> _lista = new List<Operacional.Vigencia>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, idsetor, codintlinha, vigencia, TemCobrador");
                query.Append("  From Niff_Ope_Setorlinhas s ");
                query.Append(" Where IdSetor = " + setor);
                query.Append("   and vigencia = To_date('" + vigencia.ToShortDateString() + "','dd/mm/yyyy')");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Operacional.Vigencia _indi = new Operacional.Vigencia();
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdSetor = Convert.ToInt32(dadosReader["IdSetor"].ToString());

                        _indi.Data = Convert.ToDateTime(dadosReader["Vigencia"].ToString());
                        _indi.CodigoInternoLinha = Convert.ToInt32(dadosReader["codintlinha"].ToString());
                        _indi.TemCobrador = dadosReader["TemCobrador"].ToString() == "S";

                        Linha _linha = new LinhaDAO().Consultar(_indi.CodigoInternoLinha);

                        _indi.CodigoLinha = _linha.Codigo;
                        _indi.NomeLinha = _linha.Nome;
                        _indi.Classificacao = _linha.DescricaoClassificacao;

                        _indi.Existe = true;

                        _lista.Add(_indi);
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

        public List<Operacional.Vigencia> ListarLinhasVigenciasAtual(int empresa, DateTime vigencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Vigencia> _lista = new List<Operacional.Vigencia>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Clear();
                query.Append("Select sl.id, sl.idsetor, sl.codintlinha, sl.vigencia, sl.TemCobrador, s.descricao");
                query.Append("  From Niff_Ope_Setorlinhas sl, Niff_Ope_Setor s ");
                query.Append(" Where s.Id = sl.IdSetor");
                query.Append("   and s.IdEmpresa = " + empresa);
                query.Append("   and vigencia = (Select Max(Vigencia) From Niff_Ope_Setorlinhas l");
                query.Append("                    Where l.Vigencia <= To_date('" + vigencia.ToShortDateString() + "','dd/mm/yyyy') ");
                query.Append("                      And IdSetor = s.Id)");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Operacional.Vigencia _indi = new Operacional.Vigencia();
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdSetor = Convert.ToInt32(dadosReader["IdSetor"].ToString());

                        _indi.Data = Convert.ToDateTime(dadosReader["Vigencia"].ToString());
                        _indi.CodigoInternoLinha = Convert.ToInt32(dadosReader["codintlinha"].ToString());
                        _indi.TemCobrador = dadosReader["TemCobrador"].ToString() == "S";
                        _indi.Setor = dadosReader["Descricao"].ToString();

                        Linha _linha = new LinhaDAO().Consultar(_indi.CodigoInternoLinha);

                        _indi.CodigoLinha = _linha.Codigo;
                        _indi.NomeLinha = _linha.Nome;
                        _indi.Classificacao = _linha.DescricaoClassificacao;

                        _indi.Existe = true;

                        _lista.Add(_indi);
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

        public bool Grava(List<Operacional.Vigencia> vigencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool Retorno = true;

            try
            {
                foreach (var item in vigencia)
                {
                    if (!item.Existe)
                    {
                        query.Clear();
                        query.Append("Insert into Niff_Ope_Setorlinhas");
                        query.Append("   (id, idsetor, codintlinha, vigencia, TemCobrador) ");
                        query.Append("   Values (( Select nvl(Max(id),0) + 1 From Niff_Ope_Setorlinhas)");
                        query.Append(", " + item.IdSetor);
                        query.Append(", " + item.CodigoInternoLinha);
                        query.Append(", To_date('" + item.Data.ToShortDateString() + "','dd/mm/yyyy') ");
                        query.Append(", '" + (item.TemCobrador ? "S" : "N") + "' )");

                        Retorno = sessao.ExecuteSqlTransaction(query.ToString());

                        if (!Retorno)
                            break;
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
            return Retorno;
        }

        public bool ExcluiLinhaDaVigencia(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ope_Setorlinhas");
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

        public bool ExcluiTodasAsVigenciasDoSetor(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ope_Setorlinhas");
                query.Append(" Where idSetor = " + id);

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

        public bool ExcluiVigencia(DateTime vigencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ope_Setorlinhas");
                query.Append(" Where vigencia = To_date('" + vigencia.ToShortDateString() + "','dd/mm/yyyy') ");

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

        #region Metas

        public List<Operacional.Metas> ListarMetas(bool somenteAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Metas> _lista = new List<Operacional.Metas>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, idindicador, ativo, peso, idempresa, vigencia, Meta");
                query.Append("  From Niff_Ope_Metas ");

                if (somenteAtivos)
                    query.Append(" Where Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Operacional.Metas _indi = new Operacional.Metas();
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdIndicador = Convert.ToInt32(dadosReader["idindicador"].ToString());
                        _indi.IdEmpresa = Convert.ToInt32(dadosReader["idempresa"].ToString());
                        _indi.Peso = Convert.ToDecimal(dadosReader["Peso"].ToString());
                        _indi.Meta = Convert.ToDecimal(dadosReader["Meta"].ToString());
                        _indi.Data = Convert.ToDateTime(dadosReader["Vigencia"].ToString());

                        _indi.Ativo = dadosReader["ativo"].ToString() == "S";
                        _indi.Existe = true;

                        _lista.Add(_indi);
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

        public List<Operacional.Metas> ListarMetas(int empresa, int indicador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Metas> _lista = new List<Operacional.Metas>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, idindicador, ativo, peso, idempresa, vigencia");
                query.Append("  From Niff_Ope_Metas ");

                query.Append(" Where IdIndicador = " + indicador.ToString());
                query.Append("   And IdEmpresa = " + empresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Operacional.Metas _indi = new Operacional.Metas();
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdIndicador = Convert.ToInt32(dadosReader["idindicador"].ToString());
                        _indi.IdEmpresa = Convert.ToInt32(dadosReader["idempresa"].ToString());
                        _indi.Peso = Convert.ToDecimal(dadosReader["Peso"].ToString());
                        _indi.Data = Convert.ToDateTime(dadosReader["Vigencia"].ToString());

                        _indi.Ativo = dadosReader["ativo"].ToString() == "S";
                        _indi.Existe = true;

                        _lista.Add(_indi);
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

        public Operacional.Metas Consulta(int empresa, int indicador, DateTime vigencia, bool maiorVigencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Operacional.Metas _ind = new Operacional.Metas();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, idindicador, ativo, peso, idempresa, vigencia, Meta");
                query.Append("  From Niff_Ope_Metas ");
                query.Append(" Where IdIndicador = " + indicador.ToString());
                query.Append("   And IdEmpresa = " + empresa);
                if (!maiorVigencia)
                    query.Append("   And Vigencia = To_date('" + vigencia.ToShortDateString() + "','dd/mm/yyyy')");
                else
                {
                    query.Append("   And Vigencia = (Select Max(Vigencia)");
                    query.Append("                     From niff_ope_metas l");
                    query.Append("                    Where l.IdIndicador = " + indicador);
                    query.Append("                      And l.idEmpresa = " + empresa);
                    query.Append("                      And l.Vigencia <= To_date('" + vigencia.ToShortDateString() + "','dd/mm/yyyy'))");
                }
                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {
                        _ind.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _ind.IdIndicador = Convert.ToInt32(dadosReader["idindicador"].ToString());
                        _ind.IdEmpresa = Convert.ToInt32(dadosReader["idempresa"].ToString());
                        _ind.Peso = Convert.ToDecimal(dadosReader["Peso"].ToString());
                        _ind.Meta = Convert.ToDecimal(dadosReader["Meta"].ToString());
                        _ind.Data = Convert.ToDateTime(dadosReader["Vigencia"].ToString());

                        _ind.Ativo = dadosReader["ativo"].ToString() == "S";
                        _ind.Existe = true;
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
            return _ind;
        }

        public bool Grava(Operacional.Metas item)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!item.Existe)
                {
                    query.Clear();
                    query.Append("Insert into Niff_Ope_Metas");
                    query.Append("   ( id, idindicador, ativo, peso, idempresa, vigencia, meta) ");
                    query.Append("   Values ( (Select nvl(Max(Id),0) + 1 next From Niff_Ope_Metas)");
                    query.Append("   , " + item.IdIndicador);
                    query.Append("   , '" + (item.Ativo ? "S" : "N") + "'");
                    query.Append("   , " + item.Peso.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("   , " + item.IdEmpresa);
                    query.Append("   , To_Date('" + item.Data.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("   , " + item.Meta.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" )");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Ope_Metas");
                    query.Append("   set peso = " + item.Peso.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , ativo = '" + (item.Ativo ? "S" : "N") + "'");
                    query.Append("     , meta = " + item.Meta.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" Where id = " + item.Id);
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

        public bool ExcluiMetas(int Codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ope_Metas");
                query.Append(" Where id = " + Codigo);

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

        #region Pontuacao
        public List<Operacional.Pontuacao> Listar(bool somenteAtivos, int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Pontuacao> _lista = new List<Operacional.Pontuacao>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, idempresa, codigo, descricao, ativo");
                query.Append("  From Niff_Ope_Pontuacao ");

                if (!somenteAtivos)
                    query.Append(" Where IdEmpresa = " + empresa);
                else
                {
                    query.Append(" Where Ativo = 'S'");
                    query.Append("   And IdEmpresa = " + empresa);
                }
                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Operacional.Pontuacao _indi = new Operacional.Pontuacao();
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdEmpresa = Convert.ToInt32(dadosReader["IdEmpresa"].ToString());
                        _indi.Codigo = Convert.ToInt32(dadosReader["Codigo"].ToString());

                        _indi.Descricao = dadosReader["Descricao"].ToString();
                        _indi.Ativo = dadosReader["ativo"].ToString() == "S";
                        _indi.Existe = true;

                        _lista.Add(_indi);
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

        public Operacional.Pontuacao Consulta(int codigo, int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Operacional.Pontuacao _ind = new Operacional.Pontuacao();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, idempresa, codigo, descricao, ativo");
                query.Append("  From Niff_Ope_Pontuacao ");
                query.Append(" Where Codigo = " + codigo.ToString());
                query.Append("   And IdEmpresa = " + empresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {
                        _ind.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _ind.IdEmpresa = Convert.ToInt32(dadosReader["IdEmpresa"].ToString());
                        _ind.Codigo = Convert.ToInt32(dadosReader["Codigo"].ToString());

                        _ind.Descricao = dadosReader["Descricao"].ToString();
                        _ind.Ativo = dadosReader["ativo"].ToString() == "S";
                        _ind.Existe = true;
                        _ind.Existe = true;
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
            return _ind;
        }

        public decimal Consulta(int empresa, DateTime data)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;
            try
            {

                query.Append("Select Max(Fim) pontos");
                query.Append("  From niff_ope_vigenciapontuacao n, niff_ope_pontuacao p");
                query.Append(" Where n.idpontuacao = p.Id");
                query.Append("   And p.idempresa = " + empresa);
                query.Append("   And n.vigencia = (Select Max(Vigencia)");
                query.Append("                       From niff_ope_vigenciapontuacao n, niff_ope_pontuacao p");
                query.Append("                      Where n.idpontuacao = p.Id");
                query.Append("                        And p.idempresa = " + empresa );
                query.Append("                        And vigencia <= To_date('" + data.ToShortDateString() + "', 'dd/mm/yyyy'))");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {
                        valor = Convert.ToDecimal(dadosReader["Pontos"].ToString());
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

        public string Consulta(int empresa, DateTime data, bool trazValor, decimal pontuacao)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            string valor = "0";
            try
            {

                if (trazValor)
                    query.Append("Select Max(Fim) pontos");
                else
                    query.Append("Select p.descricao pontos");
                query.Append("  From niff_ope_vigenciapontuacao n, niff_ope_pontuacao p");
                query.Append(" Where n.idpontuacao = p.Id");
                query.Append("   And p.idempresa = " + empresa);

                if (!trazValor)
                    query.Append("   And " + pontuacao.ToString().Replace(".", "").Replace(",", ".") + " between n.inicio And n.fim");

                query.Append("   And n.vigencia = (Select Max(Vigencia)");
                query.Append("                       From niff_ope_vigenciapontuacao n, niff_ope_pontuacao p");
                query.Append("                      Where n.idpontuacao = p.Id");
                query.Append("                        And p.idempresa = " + empresa);
                query.Append("                        And vigencia <= To_date('" + data.ToShortDateString() + "', 'dd/mm/yyyy'))");


                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {
                        valor = dadosReader["Pontos"].ToString();
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


        public bool Grava(Operacional.Pontuacao item, Operacional.VigenciaPontuacao vigencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool Retorno = true;

            try
            {
                if (!item.Existe)
                {
                    query.Clear();
                    query.Append("Select nvl(Max(Id),0) + 1 next From Niff_Ope_Pontuacao");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dadosReader = executar.ExecuteQuery();

                    using (dadosReader)
                    {
                        if (dadosReader.Read())
                            item.Id = Convert.ToInt32(dadosReader["next"].ToString());
                    }

                    query.Clear();
                    query.Append("Insert into Niff_Ope_Pontuacao");
                    query.Append("   (id, idempresa, codigo, descricao, ativo) ");
                    query.Append("   Values ( " + item.Id);
                    query.Append(", " + item.IdEmpresa);
                    query.Append(", " + item.Codigo);
                    query.Append(", '" + item.Descricao + "'");
                    query.Append(", '" + (item.Ativo ? "S" : "N") + "'");
                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Ope_Pontuacao");
                    query.Append("   set descricao = '" + item.Descricao + "', ");
                    query.Append("       ativo = '" + (item.Ativo ? "S" : "N") + "'");
                    query.Append(" Where id = " + item.Id);
                }

                vigencia.IdPontuacao = item.Id;
            
                Retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (Retorno)
                    Retorno = Grava(vigencia);

                return Retorno;
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

        public bool ExcluiPontuacao(int Codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Categoria _empresa = new Categoria();
            Publicas.mensagemDeErro = string.Empty;
            bool Retorno = true;

            try
            {
                query.Append("Delete Niff_Ope_VigenciaPontuacao");
                query.Append(" Where idPontuacao = " + Codigo);

                if (sessao.ExecuteSqlTransaction(query.ToString()))
                {
                    query.Clear();
                    query.Append("Delete Niff_Ope_Pontuacao");
                    query.Append(" Where id = " + Codigo);

                    Retorno = sessao.ExecuteSqlTransaction(query.ToString());
                }
                return Retorno;
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

        public int ProximaPontuacao(int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select nvl(Max(Codigo),0) + 1 next From Niff_Ope_Pontuacao");
                query.Append(" Where IdEmpresa = " + empresa);
                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                        retorno = Convert.ToInt32(dadosReader["next"].ToString());
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

        public List<Operacional.VigenciaPontuacao> ListarVigenciasPontuacao(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.VigenciaPontuacao> _lista = new List<Operacional.VigenciaPontuacao>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, idpontuacao, vigencia, inicio, fim");
                query.Append("  From Niff_Ope_VigenciaPontuacao ");

                query.Append(" Where idPontuacao = " + id);

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Operacional.VigenciaPontuacao _indi = new Operacional.VigenciaPontuacao();
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdPontuacao = Convert.ToInt32(dadosReader["idPontuacao"].ToString());

                        _indi.Data =Convert.ToDateTime( dadosReader["Vigencia"].ToString());
                        _indi.Inicio = Convert.ToDecimal(dadosReader["Inicio"].ToString());
                        _indi.Fim = Convert.ToDecimal(dadosReader["Fim"].ToString());
                        _indi.Existe = true;

                        _lista.Add(_indi);
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

        public Operacional.VigenciaPontuacao Consultar(int id, DateTime vigencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Operacional.VigenciaPontuacao _ind = new Operacional.VigenciaPontuacao();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, idpontuacao, vigencia, inicio, fim");
                query.Append("  From Niff_Ope_VigenciaPontuacao ");

                query.Append(" Where idPontuacao = " + id);
                query.Append("   And vigencia = To_date('" + vigencia.ToShortDateString() + "','dd/mm/yyyy')");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {
                        _ind.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _ind.IdPontuacao = Convert.ToInt32(dadosReader["idPontuacao"].ToString());

                        _ind.Data = Convert.ToDateTime(dadosReader["Vigencia"].ToString());
                        _ind.Inicio = Convert.ToDecimal(dadosReader["Inicio"].ToString());
                        _ind.Fim = Convert.ToDecimal(dadosReader["Fim"].ToString());
                        
                        _ind.Existe = true;
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
            return _ind;
        }

        public bool Grava(Operacional.VigenciaPontuacao item)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!item.Existe)
                {
                    query.Clear();
                    query.Append("Insert into Niff_Ope_VigenciaPontuacao");
                    query.Append("   ( id, idpontuacao, vigencia, inicio, fim) ");
                    query.Append("   Values (  (Select nvl(Max(Id),0) + 1 next From Niff_Ope_VigenciaPontuacao)");
                    query.Append(", " + item.IdPontuacao);
                    query.Append(", To_date('" + item.Data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append(", " + item.Inicio.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(", " + item.Fim.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Ope_VigenciaPontuacao");
                    query.Append("   set Inicio = " + item.Inicio.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Fim =" + item.Fim.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" Where id = " + item.Id);
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

        public bool ExcluiVigenciaPontuacao(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ope_VigenciaPontuacao");
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
        #endregion

        #region Valores
        public List<Operacional.Valores> Listar(int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Valores> _lista = new List<Operacional.Valores>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select v.id, v.idempresa, v.data, v.idindicador, v.periodo, v.codintlinha, v.programado, v.realizado, v.pontuacao");
                query.Append("     , s.descricao Setor, l.Nomelinha, l.codigolinha, i.descricao Indicador, i.abreviado");
                query.Append("  From Niff_Ope_Valores v ");
                query.Append("     , niff_ope_setorlinhas sl");
                query.Append("     , niff_ope_setor s");
                query.Append("     , Niff_Ope_Indicadores i");
                query.Append("     , Bgm_Cadlinhas l");

                query.Append(" Where sl.codintlinha = v.codintlinha  ");
                query.Append("   And s.Id = sl.idsetor  ");
                query.Append("   And l.codintlinha = v.codintlinha  ");
                query.Append("   And i.Id = v.Idindicador  ");
                query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                query.Append("                       Where l.codintlinha = v.codintlinha");
                query.Append("                         And l.vigencia <= v.data)  ");
                query.Append("   And v.IdEmpresa = " + empresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Operacional.Valores _indi = new Operacional.Valores();
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdEmpresa = Convert.ToInt32(dadosReader["IdEmpresa"].ToString());
                        _indi.IdIndicador = Convert.ToInt32(dadosReader["IdIndicador"].ToString());
                        _indi.CodigoInternoLinha = Convert.ToInt32(dadosReader["CodIntLinha"].ToString());

                        _indi.Programado = Convert.ToDecimal(dadosReader["Programado"].ToString());
                        _indi.Realizado = Convert.ToDecimal(dadosReader["Realizado"].ToString());

                        _indi.Data = Convert.ToDateTime( dadosReader["Data"].ToString());

                        _indi.Periodo = dadosReader["Periodo"].ToString();
                        _indi.Setor = dadosReader["Setor"].ToString();
                        _indi.NomeLinha = dadosReader["NomeLinha"].ToString();
                        _indi.CodigoLinha = dadosReader["CodigoLinha"].ToString();
                        _indi.Indicador = dadosReader["Indicador"].ToString();
                        _indi.Abreviado = dadosReader["Abreviado"].ToString();
                        _indi.DescricaoPeriodo = (dadosReader["Periodo"].ToString() == "1" ? "1º Período" : 
                                                  (dadosReader["Periodo"].ToString() == "2" ? "2º Período" : "Único"));
                        
                        _indi.Existe = true;

                        _lista.Add(_indi);
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

        public Operacional.Valores Consultar(int empresa, DateTime data, int indicador, string periodo, int linha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Operacional.Valores _indi = new Operacional.Valores();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select v.id, v.idempresa, v.data, v.idindicador, v.periodo, v.codintlinha, v.programado, v.realizado, v.pontuacao");
                query.Append("     , s.descricao Setor, l.Nomelinha, l.codigolinha, i.descricao Indicador, i.abreviado");
                query.Append("  From Niff_Ope_Valores v ");
                query.Append("     , niff_ope_setorlinhas sl");
                query.Append("     , niff_ope_setor s");
                query.Append("     , Niff_Ope_Indicadores i");
                query.Append("     , Bgm_Cadlinhas l");

                query.Append(" Where sl.codintlinha = v.codintlinha  ");
                query.Append("   And s.Id = sl.idsetor  ");
                query.Append("   And l.codintlinha = v.codintlinha  ");
                query.Append("   And i.Id = v.Idindicador  ");
                query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                query.Append("                       Where l.codintlinha = v.codintlinha");
                query.Append("                         And l.vigencia <= v.data)  ");
                query.Append("   And v.IdEmpresa = " + empresa);
                query.Append("   And v.idindicador = " + indicador);
                query.Append("   And v.Periodo = '" + periodo.ToUpper() + "'");
                query.Append("   And v.codintlinha = " + linha);
                query.Append("   And v.Data = To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                
                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {
                        
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdEmpresa = Convert.ToInt32(dadosReader["IdEmpresa"].ToString());
                        _indi.IdIndicador = Convert.ToInt32(dadosReader["IdIndicador"].ToString());
                        _indi.CodigoInternoLinha = Convert.ToInt32(dadosReader["CodIntLinha"].ToString());

                        _indi.Programado = Convert.ToDecimal(dadosReader["Programado"].ToString());
                        _indi.Realizado = Convert.ToDecimal(dadosReader["Realizado"].ToString());

                        _indi.Data = Convert.ToDateTime(dadosReader["Data"].ToString());

                        _indi.Periodo = dadosReader["Periodo"].ToString();
                        _indi.Setor = dadosReader["Setor"].ToString();
                        _indi.NomeLinha = dadosReader["NomeLinha"].ToString();
                        _indi.CodigoLinha = dadosReader["CodigoLinha"].ToString();
                        _indi.Indicador = dadosReader["Indicador"].ToString();
                        _indi.Abreviado = dadosReader["Abreviado"].ToString();
                        _indi.DescricaoPeriodo = (dadosReader["Periodo"].ToString() == "1" ? "1º Período" : "2º Período");

                        _indi.Existe = true;

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
            return _indi;
        }

        public Operacional.Valores Consultar(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Operacional.Valores _indi = new Operacional.Valores();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select v.id, v.idempresa, v.data, v.idindicador, v.periodo, v.codintlinha, v.programado, v.realizado, v.pontuacao");
                query.Append("     , s.descricao Setor, l.Nomelinha, l.codigolinha, i.descricao Indicador, i.abreviado");
                query.Append("  From Niff_Ope_Valores v ");
                query.Append("     , niff_ope_setorlinhas sl");
                query.Append("     , niff_ope_setor s");
                query.Append("     , Niff_Ope_Indicadores i");
                query.Append("     , Bgm_Cadlinhas l");

                query.Append(" Where sl.codintlinha = v.codintlinha  ");
                query.Append("   And s.Id = sl.idsetor  ");
                query.Append("   And l.codintlinha = v.codintlinha  ");
                query.Append("   And i.Id = v.Idindicador  ");
                query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                query.Append("                       Where l.codintlinha = v.codintlinha");
                query.Append("                         And l.vigencia <= v.data)  ");
                query.Append("   And v.Id = " + id);
                
                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {

                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdEmpresa = Convert.ToInt32(dadosReader["IdEmpresa"].ToString());
                        _indi.IdIndicador = Convert.ToInt32(dadosReader["IdIndicador"].ToString());
                        _indi.CodigoInternoLinha = Convert.ToInt32(dadosReader["CodIntLinha"].ToString());

                        _indi.Programado = Convert.ToDecimal(dadosReader["Programado"].ToString());
                        _indi.Realizado = Convert.ToDecimal(dadosReader["Realizado"].ToString());

                        _indi.Data = Convert.ToDateTime(dadosReader["Data"].ToString());

                        _indi.Periodo = dadosReader["Periodo"].ToString();
                        _indi.Setor = dadosReader["Setor"].ToString();
                        _indi.NomeLinha = dadosReader["NomeLinha"].ToString();
                        _indi.CodigoLinha = dadosReader["CodigoLinha"].ToString();
                        _indi.Indicador = dadosReader["Indicador"].ToString();
                        _indi.Abreviado = dadosReader["Abreviado"].ToString();
                        _indi.DescricaoPeriodo = (dadosReader["Periodo"].ToString() == "1" ? "1º Período" : (dadosReader["Periodo"].ToString() == "2" ? "2º Período" : "Único"));

                        _indi.Existe = true;

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
            return _indi;
        }

        public bool Grava(Operacional.Valores item, bool encerraSessao = true)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!item.Existe)
                {
                    query.Clear();
                    query.Append("Insert into Niff_Ope_Valores");
                    query.Append("   ( id, idempresa, data, idindicador, periodo, codintlinha, programado, realizado, pontuacao) ");
                    query.Append("   Values ( (Select nvl(Max(Id),0) + 1 next From Niff_Ope_Valores)");
                    query.Append("   , " + item.IdEmpresa);
                    query.Append("   , To_Date('" + item.Data.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("   , " + item.IdIndicador);
                    query.Append("   , '" + item.Periodo + "'");
                    query.Append("   , " + item.CodigoInternoLinha);
                    query.Append("   , " + item.Programado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("   , " + item.Realizado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("   , 0 )");

                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Ope_Valores");
                    query.Append("   set Programado = " + item.Programado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Realizado = " + item.Realizado.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , Pontuacao = " + item.Pontuacao.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(" Where id = " + item.Id);
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
                if (encerraSessao)
                    sessao.Desconectar();
            }
        }

        public bool ExcluiValores(int Codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ope_Valores");
                query.Append(" Where id = " + Codigo);

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

        #region Linhas
        public List<Operacional.Linhas> ListarLinhas(int IdLinha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Linhas> _lista = new List<Operacional.Linhas>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select m.id, m.CodIntLinha, m.CodIntLinha2");
                query.Append("     , l.NomeLinha, l.codigolinha ");
                query.Append("  From Niff_Ope_Linhas m, bgm_cadlinhas l ");
                query.Append(" Where m.CodIntLinha = " + IdLinha);
                query.Append("   and m.CodIntLinha2 = l.CodIntLinha");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Operacional.Linhas _indi = new Operacional.Linhas();
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdLinha = Convert.ToInt32(dadosReader["CodIntLinha"].ToString());
                        _indi.CodigoInternoLinha = Convert.ToInt32(dadosReader["CodIntLinha2"].ToString());
                        _indi.CodigoLinha = dadosReader["codigolinha"].ToString();
                        _indi.NomeLinha = dadosReader["NomeLinha"].ToString();
                        _indi.Existe = true;

                        _lista.Add(_indi);
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

        public bool Grava(List<Operacional.Linhas> linhas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool Retorno = true;
            try
            {
                foreach (var item in linhas)
                {

                    if (!item.Existe)
                    {
                        query.Clear();
                        query.Append("Insert into Niff_Ope_Linhas");
                        query.Append("   ( id, CodIntLinha, CodIntLinha2) ");
                        query.Append("   Values ( (Select nvl(Max(Id),0) + 1 next From Niff_Ope_Linhas)");
                        query.Append("   , " + item.IdLinha);
                        query.Append("   , " + item.CodigoInternoLinha);
                        query.Append("   )");

                        Retorno = sessao.ExecuteSqlTransaction(query.ToString());

                        if (!Retorno)
                            break;
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
            return Retorno;
        }

        public bool ExcluiLinhas(int Codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ope_Linhas");
                query.Append(" Where CodIntLinha = " + Codigo);

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

        public bool ExcluiLinhasAssociada(int Codigo, int linha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Ope_Linhas");
                query.Append(" Where CodIntLinha2 = " + linha);
                query.Append("   and CodIntLinha = " + Codigo);

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

        #region Consultas
        public List<Operacional.Demonstrativo> ListarDemonstrativo(int empresa, DateTime inicio, DateTime Fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Demonstrativo> _lista = new List<Operacional.Demonstrativo>();
            List<Operacional.Demonstrativo> _listaDetalhe = new List<Operacional.Demonstrativo>();
            List<Operacional.Indicadores> _listaIndicador = Listar(true);
            List<Operacional.Vigencia> _listaLinha = ListarLinhasVigenciasAtual(empresa,Fim);
            Operacional.Valores _valores;

            Publicas.mensagemDeErro = string.Empty;
            decimal _valor  = 0;
            DateTime _data = inicio;
            try
            {
                foreach (var linha in _listaLinha)
                {

                    _data = inicio;

                    while (_data <= Fim)
                    {
                        Operacional.Demonstrativo _demonstrativo = new Operacional.Demonstrativo();
                        _demonstrativo.CodigoLinha = linha.CodigoLinha;
                        _demonstrativo.NomeLinha = linha.NomeLinha;
                        _demonstrativo.Setor = linha.Setor;
                        _demonstrativo.Data = _data;

                        FeriadoEmenda _feriado = new FeriadoDAO().Consulta(_data, empresa);
                        _demonstrativo.DataGrupo = _data.ToShortDateString() + " " + Publicas.DiaDaSemana(_data.DayOfWeek) + 
                            (_feriado.Existe ?  _feriado.TipoDescricao : "");

                        foreach (var item in _listaIndicador.OrderBy(o => o.Id))
                        {
                            // Programados no Globus
                            if (item.Descricao.ToUpper().Contains("QUADRO"))
                            {
                                _demonstrativo.Quadro = QuantidadeFuncionariosEscalados(empresa, _data, linha.CodigoInternoLinha, "Ú");
                                _valores = Consultar(empresa, _data, item.Id, "Ú", linha.CodigoInternoLinha);
                                if (!_valores.Existe)
                                {
                                    _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                    _valores.Data = _data;
                                    _valores.IdEmpresa = empresa;
                                    _valores.IdIndicador = item.Id;
                                    _valores.Periodo = "Ú";
                                }
                                _valores.Programado = _demonstrativo.Quadro;
                                Grava(_valores, false);
                                continue;
                            }

                            if (item.Descricao.ToUpper().Contains("KM"))
                            {
                                _demonstrativo.Km = KM(empresa, _data, linha.CodigoInternoLinha);
                                _valores = Consultar(empresa, _data, item.Id, "Ú", linha.CodigoInternoLinha);
                                if (!_valores.Existe)
                                {
                                    _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                    _valores.Data = _data;
                                    _valores.IdEmpresa = empresa;
                                    _valores.IdIndicador = item.Id;
                                    _valores.Periodo = "Ú";
                                }
                                _valores.Programado = _demonstrativo.Quadro;
                                Grava(_valores, false);
                                continue;
                            }

                            if (item.Descricao.ToUpper().Contains("CONSUMO"))
                            {
                                _demonstrativo.Consumo = Consumo(empresa, _data, linha.CodigoInternoLinha);
                                _valores = Consultar(empresa, _data, item.Id, "Ú", linha.CodigoInternoLinha);
                                if (!_valores.Existe)
                                {
                                    _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                    _valores.Data = _data;
                                    _valores.IdEmpresa = empresa;
                                    _valores.IdIndicador = item.Id;
                                    _valores.Periodo = "Ú";
                                }
                                _valores.Programado = _demonstrativo.Quadro;
                                Grava(_valores, false);
                                continue;
                            }

                            if (item.Descricao.Contains("FCV") || item.Descricao.Contains("F.C.V"))
                            {
                                _demonstrativo.FCVProg = FCV(empresa, _data, linha.CodigoInternoLinha, "1");
                                _demonstrativo.FCVReal = _demonstrativo.FCVProg;
                                _demonstrativo.FCVPercentual = 100;

                                _valores = Consultar(empresa, _data, item.Id, "1", linha.CodigoInternoLinha);
                                _valores.Programado  = _demonstrativo.FCVProg;
                                _valores.Realizado = _valores.Programado;
                                if (!_valores.Existe)
                                {
                                    _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                    _valores.Data = _data;
                                    _valores.IdEmpresa = empresa;
                                    _valores.IdIndicador = item.Id;
                                    _valores.Periodo = "1";
                                }

                                Grava(_valores, false);
                                continue;
                            }

                            if (item.Descricao.Contains("RA ") || item.Descricao.Contains("RECOLHIDA ANORMAL"))
                            {
                                _demonstrativo.RA = RA(empresa, _data, linha.CodigoInternoLinha);
                                _valores = Consultar(empresa, _data, item.Id, "Ú", linha.CodigoInternoLinha);
                                _valores.Realizado = _demonstrativo.RA ?? 0;
                                if (!_valores.Existe)
                                {
                                    _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                    _valores.Data = _data;
                                    _valores.IdEmpresa = empresa;
                                    _valores.IdIndicador = item.Id;
                                    _valores.Periodo = "Ú";
                                }

                                Grava(_valores, false);
                                continue;
                            }

                            if (item.Descricao.Contains("SOCORRO") || item.Descricao.Contains("S.O.S"))
                            {
                                _demonstrativo.SOS = SOS(empresa, _data, linha.CodigoInternoLinha);
                                _valores = Consultar(empresa, _data, item.Id, "Ú", linha.CodigoInternoLinha);
                                _valores.Realizado = _demonstrativo.SOS ?? 0;
                                if (!_valores.Existe)
                                {
                                    _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                    _valores.Data = _data;
                                    _valores.IdEmpresa = empresa;
                                    _valores.IdIndicador = item.Id;
                                    _valores.Periodo = "Ú";
                                }

                                Grava(_valores, false);
                                continue;
                            }

                            if (item.Descricao.Contains("QUANTIDADE"))
                            {
                                _demonstrativo.PAX = PAX(empresa, _data, linha.CodigoInternoLinha);
                                _valores = Consultar(empresa, _data, item.Id, "Ú", linha.CodigoInternoLinha);
                                _valores.Realizado = _demonstrativo.PAX;
                                if (!_valores.Existe)
                                {
                                    _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                    _valores.Data = _data;
                                    _valores.IdEmpresa = empresa;
                                    _valores.IdIndicador = item.Id;
                                    _valores.Periodo = "Ú";
                                }

                                Grava(_valores, false);
                                continue;
                            }

                            if (item.Descricao.ToUpper().StartsWith("FROTA"))
                            {
                                _demonstrativo.FrotaP1 = QuantidadeVeiculosEscalados(empresa, _data, linha.CodigoInternoLinha, "1");
                                _valores = Consultar(empresa, _data, item.Id, "1", linha.CodigoInternoLinha);
                                _valores.Programado = _demonstrativo.FrotaP1;
                                if (!_valores.Existe)
                                {
                                    _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                    _valores.Data = _data;
                                    _valores.IdEmpresa = empresa;
                                    _valores.IdIndicador = item.Id;
                                    _valores.Periodo = "1";
                                }

                                Grava(_valores, false);
                                _demonstrativo.FrotaP2 = QuantidadeVeiculosEscalados(empresa, _data, linha.CodigoInternoLinha, "2");
                                _valores = Consultar(empresa, _data, item.Id, "2", linha.CodigoInternoLinha);
                                _valores.Programado = _demonstrativo.FrotaP2;
                                if (!_valores.Existe)
                                {
                                    _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                    _valores.Data = _data;
                                    _valores.IdEmpresa = empresa;
                                    _valores.IdIndicador = item.Id;
                                    _valores.Periodo = "2";
                                }

                                Grava(_valores, false);
                            }

                            if (item.Descricao.ToUpper().StartsWith("RECEITA T"))
                            {
                                if (linha.Classificacao.ToUpper().StartsWith("MUNICI"))
                                    _demonstrativo.ReceitaTotal = Math.Round(ReceitaMunicipal(empresa, _data, linha.CodigoInternoLinha),2);
                                else
                                    _demonstrativo.ReceitaTotal = Math.Round(Receita(empresa, _data, linha.CodigoInternoLinha),2);

                                _valores = Consultar(empresa, _data, item.Id, "Ú", linha.CodigoInternoLinha);
                                _valores.Realizado = _demonstrativo.ReceitaTotal;
                                if (!_valores.Existe)
                                {
                                    _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                    _valores.Data = _data;
                                    _valores.IdEmpresa = empresa;
                                    _valores.IdIndicador = item.Id;
                                    _valores.Periodo = "Ú";
                                }

                                Grava(_valores, false);
                                continue;
                            }

                            query.Clear();
                            // Diario
                            query.Append("Select v.id, v.idempresa,  v.Data, v.idindicador, v.periodo, v.codintlinha, trunc(v.programado,2) programado, Trunc(v.realizado,2) Realizado");
                            query.Append("     , s.descricao Setor, l.Nomelinha, l.codigolinha, i.descricao Indicador, i.abreviado");
                            query.Append("  From Niff_Ope_Valores v ");
                            query.Append("     , niff_ope_setorlinhas sl");
                            query.Append("     , niff_ope_setor s");
                            query.Append("     , Niff_Ope_Indicadores i");
                            query.Append("     , Bgm_Cadlinhas l");

                            query.Append(" Where sl.codintlinha = v.codintlinha  ");
                            query.Append("   And s.Id = sl.idsetor  ");
                            query.Append("   And l.codintlinha = v.codintlinha  ");
                            query.Append("   And i.Id = v.Idindicador  ");
                            query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                            query.Append("                       Where l.codintlinha = v.codintlinha");
                            query.Append("                         And l.vigencia <= v.data)  ");
                            query.Append("   And v.idIndicador = " + item.Id);
                            query.Append("   And v.IdEmpresa = " + empresa);
                            query.Append("   And v.CodIntLinha = " + linha.CodigoInternoLinha);
                            query.Append("   And v.data = To_Date('" + _data.ToShortDateString() + "', 'dd/mm/yyyy') ");
                            //query.Append("   And v.data between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + Fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                            Query executar = sessao.CreateQuery(query.ToString());

                            dadosReader2 = executar.ExecuteQuery();

                            using (dadosReader2)
                            {
                                while (dadosReader2.Read())
                                {
                                    Operacional.Valores _indi = new Operacional.Valores();
                                    _indi.Id = Convert.ToInt32(dadosReader2["Id"].ToString());
                                    _indi.IdEmpresa = Convert.ToInt32(dadosReader2["IdEmpresa"].ToString());
                                    _indi.IdIndicador = Convert.ToInt32(dadosReader2["IdIndicador"].ToString());
                                    _indi.CodigoInternoLinha = Convert.ToInt32(dadosReader2["CodIntLinha"].ToString());
                                    _indi.Data = Convert.ToDateTime(dadosReader2["Data"].ToString());

                                    try
                                    {
                                        _indi.Programado = Convert.ToDecimal(dadosReader2["Programado"].ToString());
                                    }
                                    catch { }
                                    try
                                    {
                                        _indi.Realizado = Convert.ToDecimal(dadosReader2["Realizado"].ToString());
                                    }
                                    catch { }

                                    _indi.Periodo = dadosReader2["Periodo"].ToString();
                                    _indi.Setor = dadosReader2["Setor"].ToString();
                                    _indi.NomeLinha = dadosReader2["NomeLinha"].ToString();
                                    _indi.CodigoLinha = dadosReader2["CodigoLinha"].ToString();
                                    _indi.Indicador = dadosReader2["Indicador"].ToString();
                                    _indi.Abreviado = dadosReader2["Abreviado"].ToString();
                                    _indi.DescricaoPeriodo = (dadosReader2["Periodo"].ToString() == "1" ? "1º Período" :
                                                              (dadosReader2["Periodo"].ToString() == "2" ? "2º Período" : "Único"));

                                    _indi.Existe = true;

                                    if (item.Descricao.ToUpper().StartsWith("FROTA"))
                                    {
                                        if (_indi.Periodo == "1")
                                            _demonstrativo.FrotaR1 = _indi.Realizado;
                                        else
                                            _demonstrativo.FrotaR2 = _indi.Realizado;
                                    }

                                    if (item.Descricao.ToUpper().Contains("PERDA"))
                                    {
                                        _demonstrativo.FCVPerda = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                        try
                                        {
                                            _demonstrativo.FCVReal = _demonstrativo.FCVProg - Convert.ToDecimal(_demonstrativo.FCVPerda);
                                        }
                                        catch
                                        {
                                            _demonstrativo.FCVReal = _demonstrativo.FCVProg;
                                        }

                                        _valor = _demonstrativo.FCVReal / _demonstrativo.FCVProg;
                                        _demonstrativo.FCVPercentual = Math.Round(_valor * 100, 2);

                                        foreach (var itemAux in _listaIndicador.Where(w => w.Descricao.ToUpper().Contains("FCV") ||
                                                                            w.Descricao.ToUpper().Contains("F.C.V")))
                                        {
                                            _valores = Consultar(empresa, _data, itemAux.Id, "1", linha.CodigoInternoLinha);
                                            _valores.Realizado = _demonstrativo.FCVReal;
                                            if (!_valores.Existe)
                                            {
                                                _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                                _valores.Data = _data;
                                                _valores.IdEmpresa = empresa;
                                                _valores.IdIndicador = itemAux.Id;
                                                _valores.Periodo = "1";
                                            }

                                            Grava(_valores, false);
                                        }
                                    }

                                    if (item.Descricao.ToUpper().Contains("PONTUAL"))
                                    {
                                        _demonstrativo.Pontual = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    }

                                    if (item.Descricao.Contains("SAC") || item.Descricao.Contains("S.A.C"))
                                    {
                                        _demonstrativo.SAC = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    }

                                    if (item.Descricao.Contains("ACIDENTE") || item.Descricao.Contains("ACID."))
                                    {
                                        _demonstrativo.Acidente = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    }

                                    if (item.Descricao.Contains("MOT"))
                                    {
                                        _demonstrativo.AbsenteismoMot = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    }

                                    if (item.Descricao.Contains("COB"))
                                    {
                                        _demonstrativo.AbsenteismoCob = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    }

                                    if (item.Descricao.Contains("REFEI"))
                                    {
                                        _demonstrativo.RefeicaoProg = _indi.Programado;
                                        _demonstrativo.RefeicaoReal = _indi.Realizado;
                                        _demonstrativo.RefeicaoPercentual = Math.Round((_demonstrativo.RefeicaoReal / _demonstrativo.RefeicaoProg) * 100,2);
                                    }

                                }
                            }
                        } // loop indicador

                        if (_demonstrativo.Km != 0 && _demonstrativo.Consumo != 0)
                            _demonstrativo.Media = Math.Round((_demonstrativo.Km ?? 0) / (_demonstrativo.Consumo ?? 0),2);

                        if (_demonstrativo.Pontual != 0 && _demonstrativo.Pontual != null)
                        {
                            if (_data.Year <= 2018)
                                _demonstrativo.PontualPercentual = Math.Round(((_demonstrativo.FrotaP1 - Convert.ToDecimal(_demonstrativo.Pontual)) / _demonstrativo.FrotaP1) * 100, 2);
                            else
                                _demonstrativo.PontualPercentual = Math.Round(((_demonstrativo.FCVReal - Convert.ToDecimal(_demonstrativo.Pontual)) / _demonstrativo.FCVReal) * 100, 2);
                        }
                        else
                            _demonstrativo.PontualPercentual = 100;

                        _demonstrativo.TotalAbsenteismo = Math.Round((_demonstrativo.AbsenteismoCob ?? 0) + (_demonstrativo.AbsenteismoMot ?? 0),2);

                        if (_demonstrativo.Quadro != 0)
                            _demonstrativo.Indice = (_demonstrativo.TotalAbsenteismo / _demonstrativo.Quadro);

                        if (_demonstrativo.FrotaR1 + _demonstrativo.FrotaR2 != 0)
                        {
                            _demonstrativo.PVD = Math.Round(_demonstrativo.PAX / ((_demonstrativo.FrotaR1 + _demonstrativo.FrotaR2) / 2),0);
                            foreach (var item in _listaIndicador.Where(w => w.Descricao.ToUpper().Contains("PASSAGEIROS POR VEICULO DIA") ||
                                                                            w.Descricao.ToUpper().Contains("PDV")))
                            {
                                _valores = Consultar(empresa, _data, item.Id, "Ú", linha.CodigoInternoLinha);
                                _valores.Realizado = _demonstrativo.PVD;
                                if (!_valores.Existe)
                                {
                                    _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                    _valores.Data = _data;
                                    _valores.IdEmpresa = empresa;
                                    _valores.IdIndicador = item.Id;
                                    _valores.Periodo = "Ú";
                                }

                                Grava(_valores, false);
                            } 
                            _demonstrativo.ReceitaPorCarro = Math.Round(_demonstrativo.ReceitaTotal / ((_demonstrativo.FrotaR1 + _demonstrativo.FrotaR2) / 2),2);
                            foreach (var item in _listaIndicador.Where(w => w.Descricao.ToUpper().Contains("RECEITA POR CARRO")))
                            {
                                _valores = Consultar(empresa, _data, item.Id, "Ú", linha.CodigoInternoLinha);
                                _valores.Realizado = _demonstrativo.ReceitaPorCarro;
                                if (!_valores.Existe)
                                {
                                    _valores.CodigoInternoLinha = linha.CodigoInternoLinha;
                                    _valores.Data = _data;
                                    _valores.IdEmpresa = empresa;
                                    _valores.IdIndicador = item.Id;
                                    _valores.Periodo = "Ú";
                                }

                                Grava(_valores, false);
                            }
                        }

                        _listaDetalhe.Add(_demonstrativo);

                        _data = _data.AddDays(1);
                    } // loop data
                } // loop linha
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _listaDetalhe;
        }

        public List<Operacional.Demonstrativo> ListarDemonstrativoMensal(int empresa, DateTime inicio, DateTime Fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Demonstrativo> _listaDetalhe = new List<Operacional.Demonstrativo>();
            List<Operacional.Indicadores> _listaIndicador = Listar(true);
            List<Operacional.Vigencia> _listaLinha = ListarLinhasVigenciasAtual(empresa, Fim);
            Empresa _empresa = new EmpresaDAO().ConsultaEmpresa(empresa);
            Operacional.Metas _meta = new Operacional.Metas();

            Publicas.mensagemDeErro = string.Empty;
            try
            {
                //foreach (var linha in _listaLinha.Where(w => w.CodigoLinha == "234"))
                foreach (var linha in _listaLinha)
                {

                    Operacional.Demonstrativo _demonstrativo = new Operacional.Demonstrativo();
                    _demonstrativo.CodigoLinha = linha.CodigoLinha;
                    _demonstrativo.NomeLinha = linha.NomeLinha;
                    _demonstrativo.Setor = linha.Setor;
                    _demonstrativo.Data = inicio;

                    FeriadoEmenda _feriado = new FeriadoDAO().Consulta(inicio, empresa);
                    _demonstrativo.DataGrupo = inicio.ToShortDateString() + " " + Publicas.DiaDaSemana(inicio.DayOfWeek) +
                        (_feriado.Existe ? _feriado.TipoDescricao : "");

                    foreach (var item in _listaIndicador.OrderBy(o => o.Id))
                    {
                        _meta = Consulta(empresa, item.Id, Fim, true);

                        query.Clear();
                        query.Append("Select v.Idempresa, v.Idindicador, v.Periodo, v.Codintlinha, s.Descricao Setor, l.Nomelinha");
                        query.Append("     , l.Codigolinha, i.Descricao Indicador, i.Abreviado");
                        query.Append("     , Trunc(Sum(v.Programado),2) Programado, Trunc(Sum(v.Realizado),2) Realizado");
                        query.Append("     , Data");
                        query.Append("  From Niff_Ope_Valores v ");
                        query.Append("     , niff_ope_setorlinhas sl");
                        query.Append("     , niff_ope_setor s");
                        query.Append("     , Niff_Ope_Indicadores i");
                        query.Append("     , Bgm_Cadlinhas l");

                        query.Append(" Where sl.codintlinha = v.codintlinha  ");
                        query.Append("   And s.Id = sl.idsetor  ");
                        query.Append("   And l.codintlinha = v.codintlinha  ");
                        query.Append("   And i.Id = v.Idindicador  ");
                        query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                        query.Append("                       Where l.codintlinha = v.codintlinha");
                        query.Append("                         And l.vigencia <= v.data)  ");
                        query.Append("   And v.idIndicador = " + item.Id);
                        query.Append("   And v.IdEmpresa = " + empresa);
                        query.Append("   And v.CodIntLinha = " + linha.CodigoInternoLinha);
                        query.Append("   And v.data between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + Fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append(" Group by v.Idempresa, v.Idindicador, v.Periodo, v.Codintlinha, s.Descricao, l.Nomelinha");
                        query.Append("     , l.Codigolinha, i.Descricao, i.Abreviado, Data");

                        Query executar = sessao.CreateQuery(query.ToString());

                        dadosReader2 = executar.ExecuteQuery();

                        using (dadosReader2)
                        {
                            while (dadosReader2.Read())
                            {
                                Operacional.Valores _indi = new Operacional.Valores();
                                _indi.IdEmpresa = Convert.ToInt32(dadosReader2["IdEmpresa"].ToString());
                                _indi.IdIndicador = Convert.ToInt32(dadosReader2["IdIndicador"].ToString());
                                _indi.CodigoInternoLinha = Convert.ToInt32(dadosReader2["CodIntLinha"].ToString());

                                try
                                {
                                    _indi.Programado = Convert.ToDecimal(dadosReader2["Programado"].ToString());
                                }
                                catch { }
                                try
                                {
                                    _indi.Realizado = Convert.ToDecimal(dadosReader2["Realizado"].ToString());
                                }
                                catch { }

                                _indi.Periodo = dadosReader2["Periodo"].ToString();
                                _indi.Setor = dadosReader2["Setor"].ToString();
                                _indi.NomeLinha = dadosReader2["NomeLinha"].ToString();
                                _indi.CodigoLinha = dadosReader2["CodigoLinha"].ToString();
                                _indi.Indicador = dadosReader2["Indicador"].ToString();
                                _indi.Abreviado = dadosReader2["Abreviado"].ToString();
                                _indi.DescricaoPeriodo = (dadosReader2["Periodo"].ToString() == "1" ? "1º Período" :
                                                            (dadosReader2["Periodo"].ToString() == "2" ? "2º Período" : "Único"));

                                _indi.Existe = true;

                                if (item.Descricao.ToUpper().Contains("QUADRO"))
                                    _demonstrativo.Quadro = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);

                                if (item.Descricao.Contains("FCV") || item.Descricao.Contains("F.C.V"))
                                {
                                    _demonstrativo.FCVProg = _indi.Programado;
                                    _demonstrativo.FCVReal = _indi.Realizado;
                                    _demonstrativo.FCVPercentual = Math.Round((_indi.Realizado / _indi.Programado) * 100,2) ;
                                }

                                if (item.Descricao.Contains("RA ") || item.Descricao.Contains("RECOLHIDA ANORMAL"))
                                {
                                    _demonstrativo.RA = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    if (_demonstrativo.RA == 0)
                                        _demonstrativo.RA = null;
                                }

                                if (item.Descricao.Contains("SOCORRO") || item.Descricao.Contains("S.O.S"))
                                {
                                    _demonstrativo.SOS = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    if (_demonstrativo.SOS == 0)
                                        _demonstrativo.SOS = null;
                                }

                                if (item.Descricao.ToUpper().StartsWith("FROTA"))
                                {
                                    if (_indi.Periodo == "1")
                                    {
                                        _demonstrativo.FrotaP1 = _indi.Programado;
                                        _demonstrativo.FrotaR1 = _indi.Realizado;
                                    }
                                    else
                                    {
                                        _demonstrativo.FrotaP2 = _indi.Programado;
                                        _demonstrativo.FrotaR2 = _indi.Realizado;
                                    }
                                }

                                if (item.Descricao.ToUpper().StartsWith("RECEITA T"))
                                    _demonstrativo.ReceitaTotal = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);

                                if (item.Descricao.ToUpper().StartsWith("QUANTIDADE DE PASSAGEIROS"))
                                    _demonstrativo.PAX = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);

                                if (item.Descricao.ToUpper().Contains("PERDA"))
                                {
                                    _demonstrativo.FCVPerda = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    if (_demonstrativo.FCVPerda == 0)
                                        _demonstrativo.FCVPerda = null;
                                }

                                if (item.Descricao.ToUpper().Contains("PONTUAL"))
                                {
                                    _demonstrativo.Pontual = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    if (_demonstrativo.Pontual == 0)
                                        _demonstrativo.Pontual = null;
                                }

                                if (item.Descricao.Contains("SAC") || item.Descricao.Contains("S.A.C"))
                                {
                                    _demonstrativo.SAC = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    if (_demonstrativo.SAC == 0)
                                        _demonstrativo.SAC = null;
                                }

                                if (item.Descricao.Contains("ACIDENTE") || item.Descricao.Contains("ACID."))
                                {
                                    _demonstrativo.Acidente = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    if (_demonstrativo.Acidente == 0)
                                        _demonstrativo.Acidente = null;
                                }

                                if (item.Descricao.Contains("MOT"))
                                {
                                    _demonstrativo.AbsenteismoMot = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    if (_demonstrativo.AbsenteismoMot == 0)
                                        _demonstrativo.AbsenteismoMot = null;
                                }

                                if (item.Descricao.Contains("COB"))
                                {
                                    _demonstrativo.AbsenteismoCob = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    if (_demonstrativo.AbsenteismoCob == 0)
                                        _demonstrativo.AbsenteismoCob = null;
                                }

                                if (item.Descricao.Contains("REFEI"))
                                {
                                    _demonstrativo.RefeicaoProg = _indi.Programado;
                                    _demonstrativo.RefeicaoReal = _indi.Realizado;
                                    _demonstrativo.RefeicaoPercentual = Math.Round((_demonstrativo.RefeicaoReal / _demonstrativo.RefeicaoProg) * 100, 2);
                                }

                                if (item.Descricao.Contains("KM"))
                                {
                                    _demonstrativo.Km = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    if (_demonstrativo.Km == 0)
                                        _demonstrativo.Km = null;
                                }

                                if (item.Descricao.Contains("CONSUMO"))
                                {
                                    _demonstrativo.Consumo = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    if (_demonstrativo.Consumo == 0)
                                        _demonstrativo.Consumo = null;
                                }
                            }
                        }

                    } // loop indicador

                    if (_demonstrativo.Pontual != 0 && _demonstrativo.Pontual != null)
                    {
                        if (inicio.Year <= 2018)
                            _demonstrativo.PontualPercentual = Math.Round(((_demonstrativo.FrotaP1 - Convert.ToDecimal(_demonstrativo.Pontual)) / _demonstrativo.FrotaP1) * 100, 2);
                        else
                            _demonstrativo.PontualPercentual = Math.Round(((_demonstrativo.FCVReal - Convert.ToDecimal(_demonstrativo.Pontual)) / _demonstrativo.FCVReal) * 100, 2);
                    }
                    else
                        _demonstrativo.PontualPercentual = 100;

                    _demonstrativo.TotalAbsenteismo = _demonstrativo.AbsenteismoCob ?? 0 + _demonstrativo.AbsenteismoMot ?? 0;

                    if (_demonstrativo.Quadro != 0)
                        _demonstrativo.Indice = (_demonstrativo.TotalAbsenteismo / _demonstrativo.Quadro);

                    if (_demonstrativo.FrotaR1 + _demonstrativo.FrotaR2 != 0)
                    {
                        _demonstrativo.PVD = _demonstrativo.PAX / ((_demonstrativo.FrotaR1 + _demonstrativo.FrotaR2) / 2);
                        _demonstrativo.ReceitaPorCarro = _demonstrativo.ReceitaTotal / ((_demonstrativo.FrotaR1 + _demonstrativo.FrotaR2) / 2);
                    }

                    if (_demonstrativo.Consumo != 0)
                        _demonstrativo.Media = Math.Round( _demonstrativo.Km ?? 0/ _demonstrativo.Consumo ?? 0,2) ;
                    _listaDetalhe.Add(_demonstrativo);

                } // loop linha
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _listaDetalhe;
        }

        public bool Grava(Operacional.Avaliacao item)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool Retorno = true;
            try
            {
                if (!item.Existe)
                {
                    item.Id = ProximoSetor();

                    query.Clear();
                    query.Append("Insert into niff_ope_Avaliacao");
                    query.Append("   (id, idindicador, idEmpresa, CodIntLinha, referencia, meta, valor, pontuacao) ");
                    query.Append("   Values ((select Nvl(Max(Id),0) + 1 from niff_ope_Avaliacao)");
                    query.Append("        , " + item.IdIndicador);
                    query.Append("        , " + item.IdEmpresa);
                    query.Append("        , " + item.IdLinha);
                    query.Append("        , '" + item.Referencia + "'");
                    query.Append("        , " + item.Meta.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.Valor.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + item.Pontuacao.ToString().Replace(".", "").Replace(",", ".") + ")");
                }
                else
                {
                    query.Clear();
                    query.Append("Update niff_ope_Avaliacao");
                    query.Append("   set meta = " + item.Meta.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , valor = " + item.Valor.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , pontuacao = " + item.Pontuacao.ToString().Replace(".", "").Replace(",", ".") + ")");
                    query.Append(" Where id = " + item.Id);
                }

                Retorno = sessao.ExecuteSqlTransaction(query.ToString());

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
            return Retorno;
        }

        public Operacional.Avaliacao Consultar(int empresa, DateTime data, int indicador, int linha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Operacional.Avaliacao _indi = new Operacional.Avaliacao();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select id, idindicador, idEmpresa, CodIntLinha, referencia, meta, valor, pontuacao");
                query.Append("  From niff_ope_Avaliacao  ");

                query.Append(" Where IdEmpresa = " + empresa);
                query.Append("   And idindicador = " + indicador);
                query.Append("   And Referencia = '" + data.Year.ToString("0000")+ data.Month.ToString("00") + "'");
                query.Append("   And codintlinha = " + linha);

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {

                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdEmpresa = Convert.ToInt32(dadosReader["IdEmpresa"].ToString());
                        _indi.IdIndicador = Convert.ToInt32(dadosReader["IdIndicador"].ToString());
                        _indi.IdIndicador = Convert.ToInt32(dadosReader["CodIntLinha"].ToString());

                        _indi.Meta = Convert.ToDecimal(dadosReader["meta"].ToString());
                        _indi.Valor = Convert.ToDecimal(dadosReader["Valor"].ToString());
                        _indi.Pontuacao = Convert.ToDecimal(dadosReader["Pontuacao"].ToString());

                        _indi.Referencia = dadosReader["referencia"].ToString();

                        _indi.Existe = true;

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
            return _indi;
        }

        public List<Operacional.IQO> ListarIQO(int empresa, DateTime inicio, DateTime Fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.IQO> _listaDetalhe = new List<Operacional.IQO>();
            List<Operacional.Indicadores> _listaIndicador = Listar(true);
            List<Operacional.Vigencia> _listaLinha = ListarLinhasVigenciasAtual(empresa, Fim);
            Empresa _empresa = new EmpresaDAO().ConsultaEmpresa(empresa);
            Operacional.Metas _meta = new Operacional.Metas();

            Publicas.mensagemDeErro = string.Empty;
            decimal _valor = 0;
            decimal _valorKm = 0;
            DateTime _data = inicio;
            try
            {
                foreach (var linha in _listaLinha)//.Where(w => w.CodigoLinha == "234"))
                {

                    Operacional.IQO _demonstrativo = new Operacional.IQO();
                    _demonstrativo.CodigoLinha = linha.CodigoLinha;
                    _demonstrativo.NomeLinha = linha.NomeLinha;
                    _demonstrativo.Setor = linha.Setor;

                    foreach (var item in _listaIndicador.OrderBy(o => o.Id))
                    {
                        _meta = Consulta(empresa, item.Id, Fim, true);

                        query.Clear();
                        query.Append("Select v.Idempresa, v.Idindicador, v.Periodo, v.Codintlinha, s.Descricao Setor, l.Nomelinha");
                        query.Append("     , l.Codigolinha, i.Descricao Indicador, i.Abreviado");
                        query.Append("     , Trunc(Sum(v.Programado),2) Programado, Trunc(Sum(v.Realizado),2) Realizado");
                        query.Append("  From Niff_Ope_Valores v ");
                        query.Append("     , niff_ope_setorlinhas sl");
                        query.Append("     , niff_ope_setor s");
                        query.Append("     , Niff_Ope_Indicadores i");
                        query.Append("     , Bgm_Cadlinhas l");

                        query.Append(" Where sl.codintlinha = v.codintlinha  ");
                        query.Append("   And s.Id = sl.idsetor  ");
                        query.Append("   And l.codintlinha = v.codintlinha  ");
                        query.Append("   And i.Id = v.Idindicador  ");
                        query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                        query.Append("                       Where l.codintlinha = v.codintlinha");
                        query.Append("                         And l.vigencia <= v.data)  ");
                        query.Append("   And v.idIndicador = " + item.Id);
                        query.Append("   And v.IdEmpresa = " + empresa);
                        query.Append("   And v.CodIntLinha = " + linha.CodigoInternoLinha);
                        query.Append("   And v.data between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + Fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append(" Group by v.Idempresa, v.Idindicador, v.Periodo, v.Codintlinha, s.Descricao, l.Nomelinha");
                        query.Append("     , l.Codigolinha, i.Descricao, i.Abreviado");

                        Query executar = sessao.CreateQuery(query.ToString());

                        dadosReader2 = executar.ExecuteQuery();
                        Operacional.Avaliacao _nota = new Operacional.Avaliacao();

                        using (dadosReader2)
                        {
                            while (dadosReader2.Read())
                            {

                                Operacional.Valores _indi = new Operacional.Valores();
                                _indi.IdEmpresa = Convert.ToInt32(dadosReader2["IdEmpresa"].ToString());
                                _indi.IdIndicador = Convert.ToInt32(dadosReader2["IdIndicador"].ToString());
                                _indi.CodigoInternoLinha = Convert.ToInt32(dadosReader2["CodIntLinha"].ToString());

                                try
                                {
                                    _indi.Programado = Convert.ToDecimal(dadosReader2["Programado"].ToString());
                                }
                                catch { }
                                try
                                {
                                    _indi.Realizado = Convert.ToDecimal(dadosReader2["Realizado"].ToString());
                                }
                                catch { }

                                _indi.Periodo = dadosReader2["Periodo"].ToString();
                                _indi.Setor = dadosReader2["Setor"].ToString();
                                _indi.NomeLinha = dadosReader2["NomeLinha"].ToString();
                                _indi.CodigoLinha = dadosReader2["CodigoLinha"].ToString();
                                _indi.Indicador = dadosReader2["Indicador"].ToString();
                                _indi.Abreviado = dadosReader2["Abreviado"].ToString();
                                _indi.DescricaoPeriodo = (dadosReader2["Periodo"].ToString() == "1" ? "1º Período" :
                                                            (dadosReader2["Periodo"].ToString() == "2" ? "2º Período" : "Único"));

                                _indi.Existe = true;

                                if (item.Descricao.ToUpper().StartsWith("QUADRO"))
                                    _demonstrativo.Quadro = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);

                                if (item.Descricao.ToUpper().StartsWith("QUANTIDADE DE PASSAGEIROS"))
                                    _demonstrativo.PAX = _indi.Realizado;

                                if (item.Descricao.ToUpper().StartsWith("FROTA"))
                                {
                                    _demonstrativo.FrotaProg = _demonstrativo.FrotaProg + _indi.Programado;
                                    _demonstrativo.FrotaR = _demonstrativo.FrotaR + _indi.Realizado;
                                    _demonstrativo.FrotaMeta = _meta.Meta;
                                }

                                if (item.Descricao.Contains("FCV") || item.Descricao.Contains("F.C.V"))
                                {
                                    _demonstrativo.FCVProg = _indi.Programado;
                                    _demonstrativo.FCVReal = 100;
                                    _demonstrativo.FCVMeta = _meta.Meta;
                                }

                                if (item.Descricao.ToUpper().Contains("PERDA"))
                                {
                                    try
                                    {
                                        _demonstrativo.FCVReal = _demonstrativo.FCVProg - (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    }
                                    catch
                                    {
                                        _demonstrativo.FCVReal = _demonstrativo.FCVProg;
                                    }

                                    _valor = _demonstrativo.FCVReal / _demonstrativo.FCVProg;
                                    _demonstrativo.FCVReal = Math.Round(_valor * 100, 2);

                                }

                                if (item.Descricao.ToUpper().Contains("PONTUAL"))
                                {
                                    if (inicio.Year <= 2018)
                                        _demonstrativo.PontualidadeReal = Math.Round(((_demonstrativo.FrotaProg - (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado)) / _demonstrativo.FrotaProg) * 100, 2);
                                    else
                                        _demonstrativo.PontualidadeReal = Math.Round(((_demonstrativo.FCVReal - (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado)) / _demonstrativo.FCVReal) * 100, 2);
                                    _demonstrativo.PontualidadeMeta = _meta.Meta;
                                    _demonstrativo.PontualidadePontuacao = (_demonstrativo.PontualidadeReal * _meta.Peso) / _meta.Meta;
                                    if (_demonstrativo.PontualidadePontuacao > _meta.Peso)
                                        _demonstrativo.PontualidadePontuacao = _meta.Peso;

                                    _nota = Consultar(empresa, inicio, item.Id, linha.CodigoInternoLinha);
                                    _nota.IdEmpresa = empresa;
                                    _nota.IdIndicador = item.Id;
                                    _nota.IdLinha = linha.CodigoInternoLinha;
                                    _nota.Meta = _meta.Meta;
                                    _nota.Valor = Math.Round(_demonstrativo.PontualidadeReal,3);
                                    _nota.Pontuacao = Math.Round(_demonstrativo.PontualidadePontuacao,3);
                                    _nota.Referencia = inicio.Year.ToString("0000") + inicio.Month.ToString("00");

                                    Grava(_nota);
                                }

                                if (item.Descricao.Contains("SAC") || item.Descricao.Contains("S.A.C"))
                                {
                                    _demonstrativo.SAC = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    _demonstrativo.SACMeta = _meta.Meta;
                                }

                                if (item.Descricao.Contains("ACIDENTE") || item.Descricao.Contains("ACID."))
                                {
                                    _demonstrativo.Acidentes = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    _demonstrativo.AcidenteMeta = _meta.Meta;
                                }

                                if (item.Descricao.Contains("MOT"))
                                {
                                    _demonstrativo.AbsenteismoReal = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                    _demonstrativo.AbsenteismoMeta = _meta.Meta;
                                }

                                if (item.Descricao.Contains("COB"))
                                    _demonstrativo.AbsenteismoReal = _demonstrativo.AbsenteismoReal + (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);

                                if (item.Descricao.Contains("REFEI"))
                                {
                                    _demonstrativo.RefeicaoMeta = _meta.Meta;
                                    _demonstrativo.RefeicaoReal = (_indi.Realizado / _indi.Programado) * 100;

                                    _demonstrativo.RefeicaoPontuacao = (_demonstrativo.RefeicaoReal * _meta.Peso) / _meta.Meta;
                                    if (_demonstrativo.RefeicaoPontuacao > _meta.Peso)
                                        _demonstrativo.RefeicaoPontuacao = _meta.Peso;

                                    _nota = Consultar(empresa, inicio, item.Id, linha.CodigoInternoLinha);
                                    _nota.IdEmpresa = empresa;
                                    _nota.IdIndicador = item.Id;
                                    _nota.IdLinha = linha.CodigoInternoLinha;
                                    _nota.Meta = _meta.Meta;
                                    _nota.Valor = Math.Round(_demonstrativo.RefeicaoReal,3);
                                    _nota.Pontuacao = Math.Round(_demonstrativo.RefeicaoPontuacao,3);
                                    _nota.Referencia = inicio.Year.ToString("0000") + inicio.Month.ToString("00");

                                    Grava(_nota);
                                }
                                
                                if (item.Descricao.Contains("AVARIAS"))
                                {
                                    _demonstrativo.AvariaMeta = _meta.Meta;
                                    _demonstrativo.AvariaReal = _indi.Realizado;

                                    _demonstrativo.AvariaPontuacao = (_demonstrativo.AvariaReal * _meta.Peso) / _meta.Meta;
                                    if (_demonstrativo.AvariaPontuacao > _meta.Peso)
                                        _demonstrativo.AvariaPontuacao = _meta.Peso;

                                    _nota = Consultar(empresa, inicio, item.Id, linha.CodigoInternoLinha);
                                    _nota.IdEmpresa = empresa;
                                    _nota.IdIndicador = item.Id;
                                    _nota.IdLinha = linha.CodigoInternoLinha;
                                    _nota.Meta = _meta.Meta;
                                    _nota.Valor = Math.Round(_demonstrativo.AvariaReal,3);
                                    _nota.Pontuacao = Math.Round(_demonstrativo.AvariaPontuacao,3);
                                    _nota.Referencia = inicio.Year.ToString("0000") + inicio.Month.ToString("00");

                                    Grava(_nota);
                                }

                                if (item.Descricao.Contains("LIMPEZA"))
                                {
                                    _demonstrativo.LimpezaMeta = _meta.Meta;
                                    _demonstrativo.LimpezaReal = _indi.Realizado;

                                    _demonstrativo.LimpezaPontuacao = (_demonstrativo.LimpezaReal * _meta.Peso) / _meta.Meta;
                                    if (_demonstrativo.LimpezaPontuacao > _meta.Peso)
                                        _demonstrativo.LimpezaPontuacao = _meta.Peso;

                                    _nota = Consultar(empresa, inicio, item.Id, linha.CodigoInternoLinha);
                                    _nota.IdEmpresa = empresa;
                                    _nota.IdIndicador = item.Id;
                                    _nota.IdLinha = linha.CodigoInternoLinha;
                                    _nota.Meta = _meta.Meta;
                                    _nota.Valor = Math.Round(_demonstrativo.LimpezaReal,3);
                                    _nota.Pontuacao = Math.Round(_demonstrativo.LimpezaPontuacao,3);
                                    _nota.Referencia = inicio.Year.ToString("0000") + inicio.Month.ToString("00");

                                    Grava(_nota);
                                }

                                if (item.Descricao.Contains("RA ") || item.Descricao.Contains("RECOLHIDA ANORMAL"))
                                {
                                    _demonstrativo.RA = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                }

                                if (item.Descricao.Contains("SOCORRO") || item.Descricao.Contains("S.O.S"))
                                {
                                    _demonstrativo.SOS = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                }

                                if (item.Descricao.Contains("KM"))
                                {
                                    _demonstrativo.KM = (item.TipoDeValores == "P" ? _indi.Programado : _indi.Realizado);
                                }
                            }
                        }
                        if (item.Descricao.ToUpper().StartsWith("FROTA"))
                        {
                            _demonstrativo.FrotaReal = Math.Round((_demonstrativo.FrotaR / _demonstrativo.FrotaProg) * 100, 2);
                            _demonstrativo.FrotaPontuacao = (_demonstrativo.FrotaReal * _meta.Peso) / _meta.Meta;
                            if (_demonstrativo.FrotaPontuacao > _meta.Peso)
                                _demonstrativo.FrotaPontuacao = _meta.Peso;

                            _nota = Consultar(empresa, inicio, item.Id, linha.CodigoInternoLinha);
                            _nota.IdEmpresa = empresa;
                            _nota.IdIndicador = item.Id;
                            _nota.IdLinha = linha.CodigoInternoLinha;
                            _nota.Meta = _meta.Meta;
                            _nota.Valor = Math.Round(_demonstrativo.FrotaReal,3);
                            _nota.Pontuacao = Math.Round(_demonstrativo.FrotaPontuacao,3);
                            _nota.Referencia = inicio.Year.ToString("0000") + inicio.Month.ToString("00");

                            Grava(_nota);
                        }
                        if (item.Descricao.Contains("SAC") || item.Descricao.Contains("S.A.C") || item.Descricao.ToUpper().StartsWith("QUANTIDADE DE PASSAGEIROS"))
                        { 
                            if (_demonstrativo.SAC != 0)
                                _demonstrativo.SACReal = Math.Round((_demonstrativo.PAX / _demonstrativo.SAC), 0);
                            else
                                _demonstrativo.SACReal = _demonstrativo.SACMeta;

                            _demonstrativo.SACPontuacao = (_demonstrativo.SACReal * _meta.Peso) / _meta.Meta;
                            if (_demonstrativo.SACPontuacao > _meta.Peso)
                                _demonstrativo.SACPontuacao = _meta.Peso;

                            _nota = Consultar(empresa, inicio, item.Id, linha.CodigoInternoLinha);
                            _nota.IdEmpresa = empresa;
                            _nota.IdIndicador = item.Id;
                            _nota.IdLinha = linha.CodigoInternoLinha;
                            _nota.Meta = _meta.Meta;
                            _nota.Valor = Math.Round(_demonstrativo.SACReal,3);
                            _nota.Pontuacao = Math.Round(_demonstrativo.SACPontuacao,3);
                            _nota.Referencia = inicio.Year.ToString("0000") + inicio.Month.ToString("00");

                            Grava(_nota);
                        }

                        if (item.Descricao.Contains("FCV") || item.Descricao.Contains("F.C.V") || item.Descricao.ToUpper().Contains("PERDA"))
                        {
                            foreach (var itemAux in _listaIndicador.Where(w => w.Descricao.ToUpper().Contains("FCV") ||
                                                                            w.Descricao.ToUpper().Contains("F.C.V")))
                            {
                                _meta = Consulta(empresa, itemAux.Id, Fim, true);
                                _demonstrativo.FCVPontuacao = (_demonstrativo.FCVReal * _meta.Peso) / _meta.Meta;
                                if (_demonstrativo.FCVPontuacao > _meta.Peso)
                                    _demonstrativo.FCVPontuacao = _meta.Peso;

                                _nota = Consultar(empresa, inicio, item.Id, linha.CodigoInternoLinha);
                                _nota.IdEmpresa = empresa;
                                _nota.IdIndicador = item.Id;
                                _nota.IdLinha = linha.CodigoInternoLinha;
                                _nota.Meta = _meta.Meta;
                                _nota.Valor = Math.Round(_demonstrativo.FCVReal,3);
                                _nota.Pontuacao = Math.Round(_demonstrativo.FCVPontuacao,3);
                                _nota.Referencia = inicio.Year.ToString("0000") + inicio.Month.ToString("00");

                                Grava(_nota);
                            }
                        }

                        if (item.Descricao.Contains("MKBF"))
                        {
                            _valorKm = _demonstrativo.KM;
                            _demonstrativo.KMMeta = _meta.Meta;
                            _demonstrativo.KMReal = (_valorKm / (_demonstrativo.RA + _demonstrativo.SOS));
                            _demonstrativo.KMPontuacao = (_demonstrativo.KMReal * _meta.Peso) / _meta.Meta;
                            if (_demonstrativo.KMPontuacao > _meta.Peso)
                                _demonstrativo.KMPontuacao = _meta.Peso;

                            _nota = Consultar(empresa, inicio, item.Id, linha.CodigoInternoLinha);
                            _nota.IdEmpresa = empresa;
                            _nota.IdIndicador = item.Id;
                            _nota.IdLinha = linha.CodigoInternoLinha;
                            _nota.Meta = _meta.Meta;
                            _nota.Valor = Math.Round(_demonstrativo.KMReal,3);
                            _nota.Pontuacao = Math.Round(_demonstrativo.KMPontuacao,3);
                            _nota.Referencia = inicio.Year.ToString("0000") + inicio.Month.ToString("00");

                            Grava(_nota);
                        }

                        if (item.Descricao.Contains("ACIDENTE") || item.Descricao.Contains("ACID."))
                        {
                            _valorKm = _demonstrativo.KM;
                            if (_demonstrativo.Acidentes != 0)
                                _demonstrativo.AcidenteReal = Math.Round((_valorKm / _demonstrativo.Acidentes), 0);
                            else
                                _demonstrativo.AcidenteReal = _demonstrativo.AcidenteMeta;

                            _demonstrativo.AcidentePontuacao = (_demonstrativo.AcidenteReal * _meta.Peso) / _meta.Meta;
                            if (_demonstrativo.AcidentePontuacao > _meta.Peso)
                                _demonstrativo.AcidentePontuacao = _meta.Peso;

                            _nota = Consultar(empresa, inicio, item.Id, linha.CodigoInternoLinha);
                            _nota.IdEmpresa = empresa;
                            _nota.IdIndicador = item.Id;
                            _nota.IdLinha = linha.CodigoInternoLinha;
                            _nota.Meta = _meta.Meta;
                            _nota.Valor = Math.Round(_demonstrativo.AcidenteReal,3);
                            _nota.Pontuacao = Math.Round(_demonstrativo.AcidentePontuacao,3);
                            _nota.Referencia = inicio.Year.ToString("0000") + inicio.Month.ToString("00");

                            Grava(_nota);
                        }

                    } // loop indicador


                    if (_demonstrativo.Quadro != 0)
                        _demonstrativo.AbsenteismoReal = (_demonstrativo.AbsenteismoReal / _demonstrativo.Quadro) * 100;

                    foreach (var item in _listaIndicador.Where(w => w.Descricao.ToUpper().Contains("ABSEN")))
                    {
                        _meta = Consulta(empresa, item.Id, Fim, true);

                        if (_demonstrativo.AbsenteismoReal != 0)
                        _demonstrativo.AbsenteismoPontuacao = (_meta.Meta / _demonstrativo.AbsenteismoReal) * _meta.Peso ;
                        if (_demonstrativo.AbsenteismoPontuacao > _meta.Peso)
                            _demonstrativo.AbsenteismoPontuacao = _meta.Peso;

                        Operacional.Avaliacao _nota = Consultar(empresa, inicio, item.Id, linha.CodigoInternoLinha);
                        _nota.IdEmpresa = empresa;
                        _nota.IdIndicador = item.Id;
                        _nota.IdLinha = linha.CodigoInternoLinha;
                        _nota.Meta = _meta.Meta;
                        _nota.Valor = Math.Round(_demonstrativo.AbsenteismoReal,3);
                        _nota.Pontuacao = Math.Round(_demonstrativo.AbsenteismoPontuacao,3);
                        _nota.Referencia = inicio.Year.ToString("0000") + inicio.Month.ToString("00");

                        Grava(_nota);
                        break;
                    }

                    _demonstrativo.PontuacaoReal = _demonstrativo.FrotaPontuacao + _demonstrativo.FCVPontuacao + _demonstrativo.PontualidadePontuacao +
                        _demonstrativo.SACPontuacao + _demonstrativo.AcidentePontuacao + _demonstrativo.KMPontuacao + _demonstrativo.AbsenteismoPontuacao +
                        _demonstrativo.RefeicaoPontuacao + _demonstrativo.LimpezaPontuacao + _demonstrativo.AvariaPontuacao;

                    _demonstrativo.PontuacaoMeta = Convert.ToDecimal(Consulta(empresa, Fim, true, 0));
                    _demonstrativo.Resultado = Consulta(empresa, Fim, false, _demonstrativo.PontuacaoReal).ToUpper();

                    _listaDetalhe.Add(_demonstrativo);

                } // loop linha
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _listaDetalhe;
        }

        public List<Operacional.Valores> Listar(int empresa, DateTime inicio, DateTime Fim)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Valores> _lista = new List<Operacional.Valores>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select v.id, v.idempresa, v.data, v.idindicador, v.periodo, v.codintlinha, v.programado, v.realizado, v.pontuacao");
                query.Append("     , s.descricao Setor, l.Nomelinha, l.codigolinha, i.descricao Indicador, i.abreviado");
                query.Append("  From Niff_Ope_Valores v ");
                query.Append("     , niff_ope_setorlinhas sl");
                query.Append("     , niff_ope_setor s");
                query.Append("     , Niff_Ope_Indicadores i");
                query.Append("     , Bgm_Cadlinhas l");

                query.Append(" Where sl.codintlinha = v.codintlinha  ");
                query.Append("   And s.Id = sl.idsetor  ");
                query.Append("   And l.codintlinha = v.codintlinha  ");
                query.Append("   And i.Id = v.Idindicador  ");
                query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                query.Append("                       Where l.codintlinha = v.codintlinha");
                query.Append("                         And l.vigencia <= v.data)  ");
                query.Append("   And v.IdEmpresa = " + empresa);
                query.Append("   And v.data between To_Date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy') and To_date('" + Fim.ToShortDateString() + "', 'dd/mm/yyyy)"); 

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Operacional.Valores _indi = new Operacional.Valores();
                        _indi.Id = Convert.ToInt32(dadosReader["Id"].ToString());
                        _indi.IdEmpresa = Convert.ToInt32(dadosReader["IdEmpresa"].ToString());
                        _indi.IdIndicador = Convert.ToInt32(dadosReader["IdIndicador"].ToString());
                        _indi.CodigoInternoLinha = Convert.ToInt32(dadosReader["CodIntLinha"].ToString());

                        _indi.Programado = Convert.ToDecimal(dadosReader["Programado"].ToString());
                        _indi.Realizado = Convert.ToDecimal(dadosReader["Realizado"].ToString());

                        _indi.Data = Convert.ToDateTime(dadosReader["Data"].ToString());

                        _indi.Periodo = dadosReader["Periodo"].ToString();
                        _indi.Setor = dadosReader["Setor"].ToString();
                        _indi.NomeLinha = dadosReader["NomeLinha"].ToString();
                        _indi.CodigoLinha = dadosReader["CodigoLinha"].ToString();
                        _indi.Indicador = dadosReader["Indicador"].ToString();
                        _indi.Abreviado = dadosReader["Abreviado"].ToString();
                        _indi.DescricaoPeriodo = (dadosReader["Periodo"].ToString() == "1" ? "1º Período" :
                                                  (dadosReader["Periodo"].ToString() == "2" ? "2º Período" : "Único"));

                        _indi.Existe = true;

                        _lista.Add(_indi);
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

        public int QuantidadeFuncionariosEscalados(int empresa, DateTime data, int linha, string periodo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Linhas> _lista = ListarLinhas(linha);
            Publicas.mensagemDeErro = string.Empty;
            int valor = 0;


            try
            {
                query.Append("Select decode(sl.temcobrador, 'S', Count(*) * 2, Count(s.cod_motorista)) total ");

                query.Append("  From t_esc_escaladiaria d, t_esc_servicodiaria s, niff_ope_setorlinhas sl, niff_ope_setor st");

                query.Append(" Where d.dat_escala = To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                if (_lista.Count() == 0)
                {
                    query.Append("   And d.COD_INTLINHA = " + linha);
                    query.Append("   And d.COD_INTLINHA = sl.codintlinha");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha = d.COD_INTLINHA");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }
                else
                {
                    query.Append("   And d.COD_INTLINHA in ((Select codintlinha2");
                    query.Append("                             From niff_ope_linhas l");
                    query.Append("                            Where l.codintlinha = " + linha + ")");
                    query.Append("                            Union all");
                    query.Append("                            (Select codintlinha");
                    query.Append("                             From niff_ope_linhas l");
                    query.Append("                            Where l.codintlinha = " + linha + "))");
                    query.Append("   And sl.codintlinha in ((Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha2 = d.cod_intlinha)");
                    query.Append("   union all (Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha = d.cod_intlinha))");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha in ((Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha2 = d.cod_intLinha)");
                    query.Append("                                                Union all");
                    query.Append("                                                (Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha = d.cod_intLinha))");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }

                query.Append("   And s.dat_escala = d.dat_escala  ");
                query.Append("   And s.cod_intescala = d.cod_intescala  ");
                query.Append("   And st.Id = sl.idsetor");
                query.Append("   And st.IdEmpresa = " + empresa);
                query.Append("   And (s.cod_motorista Is Not Null Or s.cod_cobrador Is Not Null)");

                if (periodo != "Ú")
                {
                    if (periodo == "1")
                        query.Append("   And s.cod_servdiaria Like '%A'");
                    else
                        query.Append("   And (s.cod_servdiaria Like '%B' or s.cod_servdiaria Like '%C')");
                }
                query.Append(" Group By Sl.Temcobrador");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {

                        valor = Convert.ToInt32(dadosReader["Total"].ToString());
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

        public int QuantidadeVeiculosEscalados(int empresa, DateTime data, int linha, string periodo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Linhas> _lista = ListarLinhas(linha);
            Publicas.mensagemDeErro = string.Empty;
            int valor = 0;

            try
            {
                query.Append("Select Count(s.Cod_Servdiaria) total ");

                query.Append("  From t_esc_escaladiaria d, t_esc_servicodiaria s, niff_ope_setorlinhas sl, niff_ope_setor st");

                query.Append(" Where d.dat_escala = To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                if (_lista.Count() == 0)
                {
                    query.Append("   And d.COD_INTLINHA = " + linha);
                    query.Append("   And d.COD_INTLINHA = sl.codintlinha");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha = d.COD_INTLINHA");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }
                else
                {
                    query.Append("   And d.COD_INTLINHA in ((Select codintlinha2");
                    query.Append("                             From niff_ope_linhas l");
                    query.Append("                            Where l.codintlinha = " + linha + ")");
                    query.Append("                            Union all");
                    query.Append("                            (Select codintlinha");
                    query.Append("                             From niff_ope_linhas l");
                    query.Append("                            Where l.codintlinha = " + linha + "))");
                    query.Append("   And sl.codintlinha in ((Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha2 = d.cod_intlinha)");
                    query.Append("   union all (Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha = d.cod_intlinha))");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha in ((Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha2 = d.cod_intLinha)");
                    query.Append("                                                Union all");
                    query.Append("                                                (Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha = d.cod_intLinha))");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }

                query.Append("   And s.dat_escala = d.dat_escala  ");
                query.Append("   And s.cod_intescala = d.cod_intescala  ");
                query.Append("   And st.Id = sl.idsetor");
                query.Append("   And st.IdEmpresa = " + empresa);

                if (periodo != "Ú")
                {
                    if (periodo == "1")
                        query.Append("   And s.cod_servdiaria Like '%A'");
                    else
                        query.Append("   And (s.cod_servdiaria Like '%B' or s.cod_servdiaria Like '%C')");
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {

                        valor = Convert.ToInt32(dadosReader["Total"].ToString());
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

        public int FCV(int empresa, DateTime data, int linha, string periodo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Linhas> _lista = ListarLinhas(linha);
            Publicas.mensagemDeErro = string.Empty;
            int valor = 0;

            try
            {
                query.Append("Select Count(*) total ");

                query.Append("  From t_esc_escaladiaria d, t_esc_servicodiaria s, t_esc_horariodiaria h, niff_ope_setorlinhas sl, niff_ope_setor st");

                query.Append(" Where d.dat_escala = To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                if (_lista.Count() == 0)
                {
                    query.Append("   And d.COD_INTLINHA = " + linha);
                    query.Append("   And d.COD_INTLINHA = sl.codintlinha");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha = d.COD_INTLINHA");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }
                else
                {
                    query.Append("   And d.COD_INTLINHA in ((Select codintlinha2");
                    query.Append("                             From niff_ope_linhas l");
                    query.Append("                            Where l.codintlinha = " + linha + ")");
                    query.Append("                            Union all");
                    query.Append("                            (Select codintlinha");
                    query.Append("                             From niff_ope_linhas l");
                    query.Append("                            Where l.codintlinha = " + linha + "))");
                    query.Append("   And sl.codintlinha in ((Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha2 = d.cod_intlinha)");
                    query.Append("   union all (Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha = d.cod_intlinha))");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha in ((Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha2 = d.cod_intLinha)");
                    query.Append("                                                Union all");
                    query.Append("                                                (Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha = d.cod_intLinha))");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }

                query.Append("   And s.dat_escala = d.dat_escala  ");
                query.Append("   And s.cod_intescala = d.cod_intescala  ");
                query.Append("   And h.dat_escala = d.dat_escala");
                query.Append("   And h.cod_intescala = d.cod_intescala");
                query.Append("   And h.cod_intservdiaria = s.cod_servdiaria");
                query.Append("   And h.cod_atividade = 5");
                query.Append("   And st.Id = sl.idsetor");
                query.Append("   And st.IdEmpresa = " + empresa);

                
                query.Append(" Order by Total");
                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {
                        valor = Convert.ToInt32(dadosReader["Total"].ToString());
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

        public int? SOS(int empresa, DateTime data, int linha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Linhas> _lista = ListarLinhas(linha);
            Publicas.mensagemDeErro = string.Empty;
            int? valor = null;

            try
            {
                query.Append("Select Count(*) total ");

                query.Append("  From man_os o, niff_ope_setorlinhas sl, niff_ope_setor st");

                query.Append(" Where o.dataaberturaos = To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                if (_lista.Count() == 0)
                {
                    query.Append("   And o.codintlinha = " + linha);
                    query.Append("   And o.codintlinha = sl.codintlinha");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha = o.codintlinha");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }
                else
                {
                    query.Append("   And o.codintlinha in ((Select codintlinha2");
                    query.Append("                            From niff_ope_linhas l");
                    query.Append("                           Where l.codintlinha = " + linha + ")");
                    query.Append("                           Union all");
                    query.Append("                           (Select codintlinha");
                    query.Append("                            From niff_ope_linhas l");
                    query.Append("                           Where l.codintlinha = " + linha + ")) ");
                    query.Append("   And sl.codintlinha in ((Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha2 = o.codintlinha)");
                    query.Append("   union all (Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha = o.codintlinha))");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha in ((Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha2 = o.codintlinha)");
                    query.Append("                                                Union all");
                    query.Append("                                                (Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha = o.codintlinha))");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }

                query.Append("   And st.Id = sl.idsetor");
                query.Append("   And st.IdEmpresa = " + empresa);
                query.Append("   And o.codorigos = 3");


                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {

                        valor = Convert.ToInt32(dadosReader["Total"].ToString());
                        if (valor == 0)
                            valor = null;
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

        public int? RA(int empresa, DateTime data, int linha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Linhas> _lista = ListarLinhas(linha);
            Publicas.mensagemDeErro = string.Empty;
            int? valor = null;

            try
            {
                query.Append("Select Count(*) total ");

                query.Append("  From man_os o, niff_ope_setorlinhas sl, niff_ope_setor st");

                query.Append(" Where o.dataaberturaos = To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                if (_lista.Count() == 0)
                {
                    query.Append("   And o.codintlinha = " + linha);
                    query.Append("   And o.codintlinha = sl.codintlinha");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha = o.codintlinha");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }
                else
                {
                    query.Append("   And o.codintlinha in ((Select codintlinha2");
                    query.Append("                            From niff_ope_linhas l");
                    query.Append("                           Where l.codintlinha = " + linha + ")");
                    query.Append("                           Union all");
                    query.Append("                           (Select codintlinha");
                    query.Append("                            From niff_ope_linhas l");
                    query.Append("                           Where l.codintlinha = " + linha + ")) ");
                    query.Append("   And sl.codintlinha in ((Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha2 = o.codintlinha)");
                    query.Append("   union all (Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha = o.codintlinha))");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha in ((Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha2 = o.codintlinha)");
                    query.Append("                                                Union all");
                    query.Append("                                                (Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha = o.codintlinha))");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }


                query.Append("   And st.Id = sl.idsetor");
                query.Append("   And st.IdEmpresa = " + empresa);
                query.Append("   And o.codorigos = 2");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {

                        valor = Convert.ToInt32(dadosReader["Total"].ToString());
                        if (valor == 0)
                            valor = null;
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

        public int PAX(int empresa, DateTime data, int linha, string tipoPago = null)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Linhas> _lista = ListarLinhas(linha);
            Publicas.mensagemDeErro = string.Empty;
            int valor = 0;

            try
            {
                query.Append("Select Nvl(Sum(d.Qtd_Passag_Trans),0) total ");

                query.Append("  From t_arr_Guia g, t_arr_detalhe_guia d, niff_ope_setorlinhas sl, niff_ope_setor st");
                if (tipoPago != null)
                    query.Append("     , t_trf_agrupatipopagto_tipos T");

                query.Append(" Where g.dat_prest_contas = To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And d.cod_seq_guia = g.cod_seq_guia");

                if (_lista.Count() == 0)
                {
                    query.Append("   And d.codintlinha = " + linha);
                    query.Append("   And d.codintlinha = sl.codintlinha");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha = d.codintlinha");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }
                else
                {
                    query.Append("   And d.codintlinha in ((Select codintlinha2");
                    query.Append("                            From niff_ope_linhas l");
                    query.Append("                           Where l.codintlinha = " + linha + ")");
                    query.Append("                           Union all");
                    query.Append("                           (Select codintlinha");
                    query.Append("                            From niff_ope_linhas l");
                    query.Append("                           Where l.codintlinha = " + linha + ")) ");
                    query.Append("   And sl.codintlinha in ((Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha2 = d.codintlinha)");
                    query.Append("   union all (Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha = d.codintlinha))");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha in ((Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha2 = d.codintlinha)");
                    query.Append("                                                Union all");
                    query.Append("                                                (Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha = d.codintlinha))");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }

                query.Append("   And st.Id = sl.idsetor");
                query.Append("   And st.IdEmpresa = " + empresa);

                if (tipoPago != null)
                {
                    query.Append("   And t.Tipoagrupa = 51");
                    query.Append("   And d.Cod_Tipopagtarifa = t.Cod_Tipopagto");
                    query.Append("   And t.codagrupa = " + tipoPago);
                }

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader3 = executar.ExecuteQuery();

                using (dadosReader3)
                {
                    if (dadosReader3.Read())
                    {

                        valor = Convert.ToInt32(dadosReader3["Total"].ToString());
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

        public decimal ReceitaMunicipal(int empresa, DateTime data, int linha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Valores> _lista = new List<Operacional.Valores>();
            Publicas.mensagemDeErro = string.Empty;

            decimal valor = 0;
            decimal integracao = PAX(empresa, data, linha, "34");
            decimal bilhetinho = PAX(empresa, data, linha, "45");
            decimal funcionario = PAX(empresa, data, linha, "16");
            decimal pax = PAX(empresa, data, linha, "3");
            decimal valorIndice = 0;
            decimal valorRemuneracao = 0;
            decimal coeficiente = 0;
            decimal valorPassageiro = 0;

            decimal fatorIntegracao = (pax - funcionario - bilhetinho) / (pax - funcionario - bilhetinho - integracao);

            Empresa _empresa = new EmpresaDAO().ConsultaEmpresa(empresa);

            try
            {
                query.Clear();
                query.Append("Select valor_indice, valor_remuneracao");
                query.Append("  from t_arr_indiceboletim");
                query.Append(" where lPad(codempresa,3,'0') || '/' || lPad(codfilial,3,'0') = '" + _empresa.CodigoEmpresaGlobus + "'");
                query.Append("   and Data_vigencia = (Select Max(Data_Vigencia) from t_arr_indiceboletim where Data_vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy'))");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {

                        valorIndice = Convert.ToDecimal(dadosReader["valor_indice"].ToString());
                        valorRemuneracao = Convert.ToDecimal(dadosReader["valor_remuneracao"].ToString());
                    }
                }

                coeficiente =  valorIndice / fatorIntegracao;
                valorPassageiro = coeficiente * valorRemuneracao;

                valor = (pax - funcionario - bilhetinho) * valorPassageiro;


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

        public decimal Receita(int empresa, DateTime data, int linha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Linhas> _lista = ListarLinhas(linha);
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;
            
            try
            {
                query.Clear(); 

                query.Append("Select Sum(d.vlr_receb)  total ");
                query.Append("  From t_arr_Guia g, t_arr_detalhe_guia d, niff_ope_setorlinhas sl, niff_ope_setor st");

                query.Append(" Where g.dat_prest_contas = To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                query.Append("   And d.cod_seq_guia = g.cod_seq_guia");

                if (_lista.Count() == 0)
                {
                    query.Append("   And d.codintlinha = " + linha);
                    query.Append("   And d.codintlinha = sl.codintlinha");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha = d.codintlinha");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }
                else
                {
                    query.Append("   And d.codintlinha in ((Select codintlinha2");
                    query.Append("                            From niff_ope_linhas l");
                    query.Append("                           Where l.codintlinha = " + linha + ")");
                    query.Append("                           Union all");
                    query.Append("                           (Select codintlinha");
                    query.Append("                            From niff_ope_linhas l");
                    query.Append("                           Where l.codintlinha = " + linha + ")) ");
                    query.Append("   And sl.codintlinha in ((Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha2 = d.codintlinha)");
                    query.Append("   union all (Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha = d.codintlinha))");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha in ((Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha2 = d.codintlinha)");
                    query.Append("                                                Union all");
                    query.Append("                                                (Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha = d.codintlinha))");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }

                query.Append("   And st.Id = sl.idsetor");
                query.Append("   And st.IdEmpresa = " + empresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {

                        valor = Convert.ToDecimal(dadosReader["Total"].ToString());
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

        public decimal KM(int empresa, DateTime data, int linha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Linhas> _lista = ListarLinhas(linha);
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Clear();

                query.Append("Select Sum(Vel.Kmpercorridoveloc) total ");
                query.Append("  From Bgm_Velocimetro Vel, Frt_Cadveiculos Frt, Bgm_Cadlinhas Lin ");
                query.Append("     , niff_ope_setorlinhas sl, niff_ope_setor st");
                query.Append("     , (Select a.Dataabastcarro, a.Sequenciaabastcarro, a.Codigoveic, It.Horaitemabast");
                query.Append("          from Aba_Abastecimentocarro a, Aba_Itemabastcarro It");
                query.Append("         Where a.Codigoveic = It.Codigoveic");
                query.Append("           And a.Dataabastcarro = It.Dataabastcarro");
                query.Append("           And a.Sequenciaabastcarro = It.Sequenciaabastcarro");
                query.Append("           And a.Dataabastcarro = To_Date('" + data.ToShortDateString() + "', 'DD/MM/YYYY')");
                query.Append("         Group By a.Dataabastcarro, a.Sequenciaabastcarro, a.Codigoveic, It.Horaitemabast) Aba");
                query.Append(" Where Vel.Horaveloc = Aba.Horaitemabast(+)");
                query.Append("   And Vel.Dataveloc = Aba.Dataabastcarro(+)");
                query.Append("   And Vel.Sequenciaveloc = Aba.Sequenciaabastcarro(+)");
                query.Append("   And Vel.Codigoveic = Aba.Codigoveic(+)");
                query.Append("   And Vel.Codintlinha = Lin.Codintlinha(+)");
                query.Append("   And Vel.Codigoveic = Frt.Codigoveic");
                query.Append("   And Vel.Dataveloc = To_Date('" + data.ToShortDateString() + "', 'DD/MM/YYYY')");

                if (_lista.Count() == 0)
                {
                    query.Append("   And Lin.Codintlinha = " + linha);
                    query.Append("   And Lin.codintlinha = sl.codintlinha");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha = Lin.codintlinha");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }
                else
                {
                    query.Append("   And Lin.codintlinha in ((Select codintlinha2");
                    query.Append("                            From niff_ope_linhas l");
                    query.Append("                           Where l.codintlinha = " + linha + ")");
                    query.Append("                           Union all");
                    query.Append("                           (Select codintlinha");
                    query.Append("                            From niff_ope_linhas l");
                    query.Append("                           Where l.codintlinha = " + linha + ")) ");
                    query.Append("   And sl.codintlinha in ((Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha2 = Lin.codintlinha)");
                    query.Append("   union all (Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha = Lin.codintlinha))");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha in ((Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha2 = Lin.codintlinha)");
                    query.Append("                                                Union all");
                    query.Append("                                                (Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha = Lin.codintlinha))");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }

                query.Append("   And st.Id = sl.idsetor");
                query.Append("   And st.IdEmpresa = " + empresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {

                        valor = Convert.ToDecimal(dadosReader["Total"].ToString());
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

        public decimal Consumo(int empresa, DateTime data, int linha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Operacional.Linhas> _lista = ListarLinhas(linha);
            Publicas.mensagemDeErro = string.Empty;
            decimal valor = 0;

            try
            {
                query.Clear();

                query.Append("Select Sum(Nvl(Ab1.Qtdeitemabastcarro, 0)) total ");
                query.Append("  From Aba_Itemabastcarro Ab1, Aba_Abastecimentocarro Aba, Aba_Cadtipooleocombo Tpo, Bgm_Cadlinhas Lin");
                query.Append("     , niff_ope_setorlinhas sl, niff_ope_setor st");
                query.Append(" Where Ab1.Codintlinha = Lin.Codintlinha(+)");
                query.Append("   And Ab1.Codigotpoleo = Tpo.Codigotpoleo");
                query.Append("   And Tpo.Tipotpoleo = 'CO'");
                query.Append("   And Aba.Sequenciaabastcarro = Ab1.Sequenciaabastcarro(+)");
                query.Append("   And Aba.Tipoabastcarro = Ab1.Tipoabastcarro(+)");
                query.Append("   And Aba.Dataabastcarro = Ab1.Dataabastcarro(+)");
                query.Append("   And Aba.Codigoveic = Ab1.Codigoveic(+)");
                query.Append("   And Aba.Dataabastcarro = To_Date('" + data.ToShortDateString() + "', 'DD/MM/YYYY')");

                if (_lista.Count() == 0)
                {
                    query.Append("   And Lin.Codintlinha = " + linha);
                    query.Append("   And Lin.codintlinha = sl.codintlinha");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha = Lin.codintlinha");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }
                else
                {
                    query.Append("   And Lin.codintlinha in ((Select codintlinha2");
                    query.Append("                            From niff_ope_linhas l");
                    query.Append("                           Where l.codintlinha = " + linha + ")");
                    query.Append("                           Union all");
                    query.Append("                           (Select codintlinha");
                    query.Append("                            From niff_ope_linhas l");
                    query.Append("                           Where l.codintlinha = " + linha + ")) ");
                    query.Append("   And sl.codintlinha in ((Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha2 = Lin.codintlinha)");
                    query.Append("   union all (Select Codintlinha From Niff_Ope_Linhas l Where l.Codintlinha = Lin.codintlinha))");
                    query.Append("   And sl.vigencia = (Select Max(vigencia) From niff_ope_setorlinhas l");
                    query.Append("                       Where l.codintlinha in ((Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha2 = Lin.codintlinha)");
                    query.Append("                                                Union all");
                    query.Append("                                                (Select codintlinha");
                    query.Append("                                                 From niff_ope_linhas l");
                    query.Append("                                                Where l.codintlinha = Lin.codintlinha))");
                    query.Append("                         And l.vigencia <= To_date('" + data.ToShortDateString() + "','dd/mm/yyyy') )  ");
                }

                query.Append("   And st.Id = sl.idsetor");
                query.Append("   And st.IdEmpresa = " + empresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {

                        valor = Convert.ToDecimal(dadosReader["Total"].ToString());
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
        #endregion
    }
}
