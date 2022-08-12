using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class CargosDAO
    {
        IDataReader dataReader;

        public List<Cargos> Listar(bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Cargos> _lista = new List<Cargos>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idcargo, descricao, ativo, requerexperiencia, idescola, salariominimo, salariomaximo, TipoDoCargo");
                query.Append("  from Niff_ADS_Cargos c     ");
                if (apenasAtivos)
                    query.Append(" Where ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Cargos _tipo = new Cargos();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idcargo"].ToString());

                        _tipo.Descricao = dataReader["Descricao"].ToString();

                        _tipo.IdEscolaridade = Convert.ToInt32(dataReader["IdEscola"].ToString());
                        
                        _tipo.SalarioMinimo = Convert.ToDecimal(dataReader["SalarioMinimo"].ToString());
                        _tipo.SalarioMaximo = Convert.ToDecimal(dataReader["SalarioMaximo"].ToString());                        

                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.RequerExperiencia = dataReader["RequerExperiencia"].ToString() == "S";
                        _tipo.TipoDoCargo = dataReader["TipoDoCargo"].ToString();

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

        public Cargos Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Cargos _tipo = new Cargos();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idcargo, descricao, ativo, requerexperiencia, idescola, salariominimo, salariomaximo, TipoDoCargo");
                query.Append("  from Niff_ADS_Cargos c     ");
                query.Append(" Where IdCargo = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idcargo"].ToString());

                        _tipo.Descricao = dataReader["Descricao"].ToString();

                        _tipo.IdEscolaridade = Convert.ToInt32(dataReader["IdEscola"].ToString());

                        _tipo.SalarioMinimo = Convert.ToDecimal(dataReader["SalarioMinimo"].ToString());
                        _tipo.SalarioMaximo = Convert.ToDecimal(dataReader["SalarioMaximo"].ToString());

                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.RequerExperiencia = dataReader["RequerExperiencia"].ToString() == "S";
                        _tipo.TipoDoCargo = dataReader["TipoDoCargo"].ToString();
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

                query.Append("Select Max(IdCargo) +1 next From Niff_ADS_Cargos");
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

        public bool Gravar(Cargos tipo, List<CompetenciasDoCargo> competencias, List<MetasDoCargo> metas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into Niff_ADS_Cargos");
                    query.Append(" ( idcargo, descricao, ativo, requerexperiencia, idescola, salariominimo, salariomaximo, TipoDoCargo )");
                    query.Append(" Values (" + tipo.Id);
                    query.Append("        ,'" + tipo.Descricao + "'");
                    query.Append("        ,'" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.RequerExperiencia ? "S" : "N") + "'");
                    query.Append("        , " + tipo.IdEscolaridade);
                    query.Append("        , " + tipo.SalarioMinimo.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , " + tipo.SalarioMaximo.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("        , '" + tipo.TipoDoCargo + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_ADS_Cargos");
                    query.Append("   set descricao = '" + tipo.Descricao + "'");
                    query.Append("     , ativo = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("     , requerexperiencia = '" + (tipo. RequerExperiencia ? "S" : "N") + "'");

                    query.Append("     , idescola = " + tipo.IdEscolaridade);
                    query.Append("     , salariominimo = " + tipo.SalarioMinimo.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , salariomaximo = " + tipo.SalarioMaximo.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , TipoDoCargo = '" + tipo.TipoDoCargo + "'");
                    query.Append(" Where idCargo = " + tipo.Id);

                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    if (tipo.Existe)
                    {

                        if (!new MetasDoCargoDAO().Excluir(tipo.Id))
                            retorno = false;
                    }

                    if (competencias.Count() > 0)
                    {
                        if (!new CompetenciasDoCargosDAO().Gravar(competencias))
                            retorno = false;
                    }

                    if (retorno && metas.Count() > 0)
                    {
                        if (!new MetasDoCargoDAO().Gravar(metas))
                            retorno = false;
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

        public bool Excluir(Cargos tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!new CompetenciasDoCargosDAO().Excluir(tipo.Id))
                    return false;

                if (!new MetasDoCargoDAO().Excluir(tipo.Id))
                    return false;

                query.Append("Delete Niff_ADS_Cargos");
                query.Append(" Where IdCargo = " + tipo.Id);
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
