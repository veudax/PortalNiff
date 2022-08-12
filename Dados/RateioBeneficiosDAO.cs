using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class RateioBeneficiosDAO
    {
        IDataReader dataReader;
        IDataReader dataReader2;

        #region Cadastros
        #region Plano 

        public List<RateioBeneficios.PlanoContabil> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RateioBeneficios.PlanoContabil> _lista = new List<RateioBeneficios.PlanoContabil>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select nroplano, descplano");
                query.Append("  From ctbplano c");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        RateioBeneficios.PlanoContabil centro = new RateioBeneficios.PlanoContabil();

                        centro.NumeroPlano = Convert.ToInt32(dataReader["nroplano"].ToString());
                        centro.Nome = dataReader["descplano"].ToString();
                        centro.Existe = true;
                        _lista.Add(centro);
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

        public RateioBeneficios.PlanoContabil Consulta(int plano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            RateioBeneficios.PlanoContabil centro = new RateioBeneficios.PlanoContabil();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select nroplano, descplano");
                query.Append("  From ctbplano c");
                query.Append(" where nroplano = " + plano);
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        centro.NumeroPlano = Convert.ToInt32(dataReader["nroplano"].ToString());
                        centro.Nome = dataReader["descplano"].ToString();
                        centro.Existe = true;
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
            return centro;
        }
        #endregion

        #region Setor 

        public List<RateioBeneficios.Setor> ListarSetores()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RateioBeneficios.Setor> _lista = new List<RateioBeneficios.Setor>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodSetor, descSetor");
                query.Append("  From flp_setor c");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        RateioBeneficios.Setor centro = new RateioBeneficios.Setor();

                        centro.Codigo = Convert.ToInt32(dataReader["CodSetor"].ToString());
                        centro.Nome = dataReader["descsetor"].ToString();
                        centro.Existe = true;
                        _lista.Add(centro);
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

        public RateioBeneficios.Setor ConsultaSetor(int setor)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            RateioBeneficios.Setor centro = new RateioBeneficios.Setor();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select CodSetor, descSetor");
                query.Append("  From flp_setor c");
                query.Append(" where CodSetor = " + setor);
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        centro.Codigo = Convert.ToInt32(dataReader["CodSetor"].ToString());
                        centro.Nome = dataReader["descsetor"].ToString();
                        centro.Existe = true;
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
            return centro;
        }
        #endregion

        #region Conta contabil Globus

        public List<RateioBeneficios.ContasContabeis> Listar(int plano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RateioBeneficios.ContasContabeis> _lista = new List<RateioBeneficios.ContasContabeis>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select nroplano, codcontactb, classificador, digito, nomeconta, lancamento, informacustosconta");
                query.Append("  From Ctbconta c");
                query.Append(" Where c.nroplano = " + plano);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader2 = executar.ExecuteQuery();
                using (dataReader2)
                {
                    while (dataReader2.Read())
                    {
                        RateioBeneficios.ContasContabeis centro = new RateioBeneficios.ContasContabeis();

                        centro.Codigo = Convert.ToInt32(dataReader2["codcontactb"].ToString());
                        centro.NumeroPlano = Convert.ToInt32(dataReader2["nroplano"].ToString());
                        centro.Nome = dataReader2["nomeconta"].ToString();
                        centro.Classificador = dataReader2["classificador"].ToString();
                        centro.AceitaLancamento = dataReader2["lancamento"].ToString() == "S";
                        centro.Existe = true;
                        _lista.Add(centro);
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

        public RateioBeneficios.ContasContabeis Consulta(int plano, int conta)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            RateioBeneficios.ContasContabeis centro = new RateioBeneficios.ContasContabeis();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select nroplano, codcontactb, classificador, digito, nomeconta, lancamento, informacustosconta");
                query.Append("  From Ctbconta c");
                query.Append(" Where c.nroplano = " + plano);
                query.Append("   and c.codcontactb = " + conta);
                Query executar = sessao.CreateQuery(query.ToString());
                dataReader2 = executar.ExecuteQuery();

                using (dataReader2)
                {
                    if (dataReader2.Read())
                    {
                        centro.Codigo = Convert.ToInt32(dataReader2["codcontactb"].ToString());
                        centro.NumeroPlano = Convert.ToInt32(dataReader2["nroplano"].ToString());
                        centro.Nome = dataReader2["nomeconta"].ToString();
                        centro.Classificador = dataReader2["classificador"].ToString();
                        centro.AceitaLancamento = dataReader2["lancamento"].ToString() == "S";
                        centro.Existe = true;
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
            return centro;
        }
        #endregion

        #region Parametros
        public List<RateioBeneficios.Parametros> Listar(int empresa, bool somentAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RateioBeneficios.Parametros> _lista = new List<RateioBeneficios.Parametros>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, nroplano, lote, ativo, IgnorarFuncoes, CodigoFuncoes");
                query.Append("     , ignorarfuncsembeneficios, codcontavr, codcontavt, codcontamedico");
                query.Append("     , codcontaodontologio, codcontacbasica, HistoricoPadrao, CodEventoVT, RegraEspecificaVT");
                query.Append("     , RegraEspecificaConvenios, IgnorarFuncSemConvenioMedico, IgnorarFuncSemConvenioOdonto");
                query.Append("  From niff_ctb_param c");
                query.Append(" Where c.IdEmpresa = " + empresa);

                if (somentAtivos)
                    query.Append("   and c.Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        RateioBeneficios.Parametros centro = new RateioBeneficios.Parametros();

                        centro.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        centro.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        centro.NumeroPlano = Convert.ToInt32(dataReader["nroplano"].ToString());
                        centro.Lote = dataReader["Lote"].ToString();
                        centro.Ativo = dataReader["Ativo"].ToString() == "S";
                        centro.HistoricoPadrao = dataReader["HistoricoPadrao"].ToString();
                        centro.IgnorarFuncoes = dataReader["IgnorarFuncoes"].ToString() == "S";
                        centro.CodigoFuncoes = dataReader["CodigoFuncoes"].ToString();
                        centro.IgnorarFuncionariosSemBeneficios = dataReader["IgnorarFuncSemBeneficios"].ToString() == "S";
                        centro.CodigoContaValeRefeicao = dataReader["CodContaVR"].ToString();
                        centro.CodigoContaValeTransporte = dataReader["CodContaVT"].ToString();
                        centro.CodigoContaConvenioMedico = dataReader["CodContaMedico"].ToString();
                        centro.CodigoContaConvenioOdontologio = dataReader["CodContaOdontologio"].ToString();
                        centro.CodigoContaCestaBasica = dataReader["CodContaCBasica"].ToString();
                        centro.CodigoEventosValeTransporte = dataReader["CodEventoVT"].ToString();
                        centro.RegraEspecificaVT = dataReader["RegraEspecificaVT"].ToString() == "S";

                        centro.RegraEspecificaConvenios = dataReader["RegraEspecificaConvenios"].ToString() == "S";
                        centro.IgnorarFuncionarioSemConvenioMedico = dataReader["IgnorarFuncSemConvenioMedico"].ToString() == "S";
                        centro.IgnorarFuncionarioSemConvenioOdontologico = dataReader["IgnorarFuncSemConvenioOdonto"].ToString() == "S";
                        centro.Existe = true;
                        _lista.Add(centro);
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

        public RateioBeneficios.Parametros Consultar(int empresa, int plano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            RateioBeneficios.Parametros centro = new RateioBeneficios.Parametros();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, nroplano, lote, ativo, IgnorarFuncoes, CodigoFuncoes");
                query.Append("     , ignorarfuncsembeneficios, codcontavr, codcontavt, codcontamedico");
                query.Append("     , codcontaodontologio, codcontacbasica, HistoricoPadrao, CodEventoVT, RegraEspecificaVT");
                query.Append("     , RegraEspecificaConvenios, IgnorarFuncSemConvenioMedico, IgnorarFuncSemConvenioOdonto");
                query.Append("  From niff_ctb_param c");
                query.Append(" Where c.IdEmpresa = " + empresa);
                query.Append("   and c.nroPlano = " + plano);

                Query executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        centro.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        centro.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        centro.NumeroPlano = Convert.ToInt32(dataReader["nroplano"].ToString());
                        centro.Lote = dataReader["Lote"].ToString();
                        centro.HistoricoPadrao = dataReader["HistoricoPadrao"].ToString();
                        centro.Ativo = dataReader["Ativo"].ToString() == "S";
                        centro.IgnorarFuncoes = dataReader["IgnorarFuncoes"].ToString() == "S";
                        centro.CodigoFuncoes = dataReader["CodigoFuncoes"].ToString();
                        centro.IgnorarFuncionariosSemBeneficios = dataReader["IgnorarFuncSemBeneficios"].ToString() == "S";
                        centro.CodigoContaValeRefeicao = dataReader["CodContaVR"].ToString();
                        centro.CodigoContaValeTransporte = dataReader["CodContaVT"].ToString();
                        centro.CodigoContaConvenioMedico = dataReader["CodContaMedico"].ToString();
                        centro.CodigoContaConvenioOdontologio = dataReader["CodContaOdontologio"].ToString();
                        centro.CodigoContaCestaBasica = dataReader["CodContaCBasica"].ToString();
                        centro.CodigoEventosValeTransporte = dataReader["CodEventoVT"].ToString();
                        centro.RegraEspecificaVT = dataReader["RegraEspecificaVT"].ToString() == "S";

                        centro.RegraEspecificaConvenios = dataReader["RegraEspecificaConvenios"].ToString() == "S";
                        centro.IgnorarFuncionarioSemConvenioMedico = dataReader["IgnorarFuncSemConvenioMedico"].ToString() == "S";
                        centro.IgnorarFuncionarioSemConvenioOdontologico = dataReader["IgnorarFuncSemConvenioOdonto"].ToString() == "S";

                        centro.Existe = true;
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
            return centro;
        }

        public RateioBeneficios.Parametros Consultar(int empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            RateioBeneficios.Parametros centro = new RateioBeneficios.Parametros();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idempresa, nroplano, lote, ativo, IgnorarFuncoes, CodigoFuncoes");
                query.Append("     , ignorarfuncsembeneficios, codcontavr, codcontavt, codcontamedico");
                query.Append("     , codcontaodontologio, codcontacbasica, HistoricoPadrao, CodEventoVT, RegraEspecificaVT");
                query.Append("     , RegraEspecificaConvenios, IgnorarFuncSemConvenioMedico, IgnorarFuncSemConvenioOdonto");
                query.Append("  From niff_ctb_param c");
                query.Append(" Where c.IdEmpresa = " + empresa);
                query.Append("   and c.ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());
                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        centro.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        centro.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        centro.NumeroPlano = Convert.ToInt32(dataReader["nroplano"].ToString());
                        centro.Lote = dataReader["Lote"].ToString();
                        centro.HistoricoPadrao = dataReader["HistoricoPadrao"].ToString();
                        centro.Ativo = dataReader["Ativo"].ToString() == "S";
                        centro.IgnorarFuncoes = dataReader["IgnorarFuncoes"].ToString() == "S";
                        centro.CodigoFuncoes = dataReader["CodigoFuncoes"].ToString();
                        centro.IgnorarFuncionariosSemBeneficios = dataReader["IgnorarFuncSemBeneficios"].ToString() == "S";
                        centro.CodigoContaValeRefeicao = dataReader["CodContaVR"].ToString();
                        centro.CodigoContaValeTransporte = dataReader["CodContaVT"].ToString();
                        centro.CodigoContaConvenioMedico = dataReader["CodContaMedico"].ToString();
                        centro.CodigoContaConvenioOdontologio = dataReader["CodContaOdontologio"].ToString();
                        centro.CodigoContaCestaBasica = dataReader["CodContaCBasica"].ToString();
                        centro.CodigoEventosValeTransporte = dataReader["CodEventoVT"].ToString();
                        centro.RegraEspecificaVT = dataReader["RegraEspecificaVT"].ToString() == "S";

                        centro.RegraEspecificaConvenios = dataReader["RegraEspecificaConvenios"].ToString() == "S";
                        centro.IgnorarFuncionarioSemConvenioMedico = dataReader["IgnorarFuncSemConvenioMedico"].ToString() == "S";
                        centro.IgnorarFuncionarioSemConvenioOdontologico = dataReader["IgnorarFuncSemConvenioOdonto"].ToString() == "S";

                        centro.Existe = true;
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
            return centro;
        }

        public List<RateioBeneficios.Custos> ListarCustosDoParametro(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RateioBeneficios.Custos> _lista = new List<RateioBeneficios.Custos>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select pc.id, idparam, codcustoctb, c.classcustoctb || ' ' || c.Desccusto nome");
                query.Append("  From niff_ctb_paramcustos pc, niff_ctb_param p, ctbcusto c");
                query.Append(" Where pc.idparam = " + id);
                query.Append("   and p.id = pc.idparam");
                query.Append("   and c.nroplano = p.nroplano");
                query.Append("   and c.codcusto = pc.codcustoctb");
                query.Append("   and p.ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        RateioBeneficios.Custos centro = new RateioBeneficios.Custos();

                        centro.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        centro.IdParam = Convert.ToInt32(dataReader["idparam"].ToString());
                        centro.CodigoCusto = Convert.ToInt32(dataReader["codcustoctb"].ToString());
                        centro.Nome = dataReader["nome"].ToString();
                        centro.Existe = true;
                        _lista.Add(centro);
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

        public bool Grava(RateioBeneficios.Parametros param, List<RateioBeneficios.Custos> custos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            int id = 1;

            try
            {
                if (!param.Existe)
                {
                    query.Clear();
                    query.Append("Select Nvl(Max(Id),0) + 1 next From niff_ctb_param");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            id = Convert.ToInt32(dataReader["next"].ToString());
                    }

                    query.Clear();
                    query.Append("Insert into niff_ctb_param");
                    query.Append("   (id, idempresa, nroplano, lote, ativo, IgnorarFuncoes, CodigoFuncoes ");
                    query.Append("     , ignorarfuncsembeneficios, codcontavr, codcontavt, codcontamedico");
                    query.Append("     , codcontaodontologio, codcontacbasica, HistoricoPadrao, CodEventoVT, RegraEspecificaVT ");
                    query.Append("     , RegraEspecificaConvenios, IgnorarFuncSemConvenioMedico, IgnorarFuncSemConvenioOdonto");
                    query.Append(" )  Values (" + id);
                    query.Append(", " + param.IdEmpresa);
                    query.Append(", " + param.NumeroPlano);
                    query.Append(", '" + param.Lote + "'");
                    query.Append(", '" + (param.Ativo ? "S" : "N") + "'");
                    query.Append(", '" + (param.IgnorarFuncoes ? "S" : "N") + "'");
                    query.Append(", '" + param.CodigoFuncoes + "'");
                    query.Append(", '" + (param.IgnorarFuncionariosSemBeneficios ? "S" : "N") + "'");
                    query.Append(", '" + param.CodigoContaValeRefeicao + "'");
                    query.Append(", '" + param.CodigoContaValeTransporte + "'");
                    query.Append(", '" + param.CodigoContaConvenioMedico + "'");
                    query.Append(", '" + param.CodigoContaConvenioOdontologio + "'");
                    query.Append(", '" + param.CodigoContaCestaBasica + "'");
                    query.Append(", '" + param.HistoricoPadrao + "'");
                    query.Append(", '" + param.CodigoEventosValeTransporte + "'");
                    query.Append(", '" + (param.RegraEspecificaVT ? "S" : "N") + "'");

                    query.Append(", '" + (param.RegraEspecificaConvenios ? "S" : "N") + "'");
                    query.Append(", '" + (param.IgnorarFuncionarioSemConvenioMedico ? "S" : "N") + "'");
                    query.Append(", '" + (param.IgnorarFuncionarioSemConvenioOdontologico ? "S" : "N") + "'");

                    query.Append(")");
                }
                else
                {
                    id = param.Id;
                    query.Clear();
                    query.Append("Update niff_ctb_param");
                    query.Append("   set Lote = '" + param.Lote + "'");
                    query.Append("     , ativo = '" + (param.Ativo ? "S" : "N") + "'");
                    query.Append("     , IgnorarFuncoes = '" + (param.IgnorarFuncoes ? "S" : "N") + "'");
                    query.Append("     , CodigoFuncoes = '" + param.CodigoFuncoes + "'");
                    query.Append("     , IgnorarFuncSemBeneficios = '" + (param.IgnorarFuncionariosSemBeneficios ? "S" : "N") + "'");
                    query.Append("     , CodContaVR = '" + param.CodigoContaValeRefeicao + "'");
                    query.Append("     , CodContaVT = '" + param.CodigoContaValeTransporte + "'");
                    query.Append("     , CodContaMedico = '" + param.CodigoContaConvenioMedico + "'");
                    query.Append("     , CodContaOdontologio = '" + param.CodigoContaConvenioOdontologio + "'");
                    query.Append("     , CodContaCBasica = '" + param.CodigoContaCestaBasica + "'");
                    query.Append("     , HistoricoPadrao = '" + param.HistoricoPadrao + "'");
                    query.Append("     , CodEventoVT = '" + param.CodigoEventosValeTransporte + "'");
                    query.Append("     , RegraEspecificaVT = '" + (param.RegraEspecificaVT ? "S" : "N") + "'");
                    query.Append("     , RegraEspecificaConvenios = '" + (param.RegraEspecificaConvenios ? "S" : "N") + "'");
                    query.Append("     , IgnorarFuncSemConvenioMedico = '" + (param.IgnorarFuncionarioSemConvenioMedico ? "S" : "N") + "'");
                    query.Append("     , IgnorarFuncSemConvenioOdonto = '" + (param.IgnorarFuncionarioSemConvenioOdontologico ? "S" : "N") + "'");
                    
                    query.Append(" Where Id = " + param.Id);
                }

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;
                else
                {
                    custos.ForEach(u => u.IdParam = id);
                    foreach (var item in custos)
                    {
                        if (!item.Existe)
                        {
                            query.Clear();
                            query.Append("Insert into niff_ctb_paramcustos");
                            query.Append("   (id, idparam, codcustoctb) ");
                            query.Append("   Values ((Select Nvl(Max(id),0) + 1 next From niff_ctb_paramcustos)");
                            query.Append(", " + id);
                            query.Append(", " + item.CodigoCusto);
                            query.Append(")");

                            if (!sessao.ExecuteSqlTransaction(query.ToString()))
                                return false;
                        }
                    }
                }
                return true;
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

        public bool ExcluiParametros(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete niff_ctb_paramcustos");
                query.Append(" Where IdParam = " + id);

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;
                else
                {
                    query.Clear();
                    query.Append("Delete niff_ctb_param");
                    query.Append(" Where Id = " + id);

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

        public bool ExcluiCustosDoParametro(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete niff_ctb_paramcustos");
                query.Append(" Where Id = " + id);

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

        #region Associa
        public List<RateioBeneficios.Associacao> ListarAssociacoes(int empresa, int idParam)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RateioBeneficios.Associacao> _lista = new List<RateioBeneficios.Associacao>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.id, c.idempresa, c.idparam, c.codcustoctb, c.codcontactb, c.codsetor, c.CodContaCTBDestino");
                query.Append("     , u.classcustoctb || ' ' || u.Desccusto nome, a.NomeConta DescConta");
                query.Append("     , a.Classificador || ' ' || a.NomeConta nomeConta, s.DescSetor, a.NroPlano");

                query.Append("  From niff_ctb_associacustosetor c, niff_ctb_param p, ctbcusto u, ctbconta a, flp_setor s");
                query.Append(" Where c.IdEmpresa = " + empresa);
                query.Append("   and c.idparam = " + idParam);
                query.Append("   and p.id = c.idparam");
                query.Append("   and u.nroplano = p.nroplano");
                query.Append("   and u.codcusto = c.codcustoctb");
                query.Append("   and a.nroplano = p.nroplano");
                query.Append("   and a.codcontactb = c.codcontactb");
                query.Append("   and s.codsetor = c.codsetor");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        RateioBeneficios.Associacao centro = new RateioBeneficios.Associacao();

                        centro.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        centro.IdParam = Convert.ToInt32(dataReader["idparam"].ToString());
                        centro.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        centro.CodigoCusto = Convert.ToInt32(dataReader["codcustoctb"].ToString());
                        centro.CodConta = Convert.ToInt32(dataReader["codcontactb"].ToString());
                        centro.CodigoSetor = Convert.ToInt32(dataReader["codsetor"].ToString());

                        try
                        {
                            centro.CodContaDestino = Convert.ToInt32(dataReader["CodContaCTBDestino"].ToString());
                        }
                        catch { }
                        

                        centro.NomeConta = dataReader["NomeConta"].ToString();
                        centro.Nome = dataReader["Nome"].ToString();
                        centro.NomeSetor = dataReader["DescSetor"].ToString();
                        centro.Conta = dataReader["DescConta"].ToString();

                        RateioBeneficios.ContasContabeis _conta = Consulta(Convert.ToInt32(dataReader["NroPlano"].ToString()), centro.CodContaDestino);
                        centro.NomeContaDestino = _conta.Classificador + " " + _conta.Nome;

                        centro.Existe = true;
                        _lista.Add(centro);
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

        public bool Grava(List<RateioBeneficios.Associacao> _listaParam)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            int id = 1;

            try
            {
                foreach (var param in _listaParam)
                {

                    if (!param.Existe)
                    {
                        query.Clear();
                        query.Append("Select Nvl(Max(Id),0) + 1 next From niff_ctb_associacustosetor");
                        Query executar = sessao.CreateQuery(query.ToString());

                        dataReader = executar.ExecuteQuery();

                        using (dataReader)
                        {
                            if (dataReader.Read())
                                id = Convert.ToInt32(dataReader["next"].ToString());
                        }

                        query.Clear();
                        query.Append("Insert into niff_ctb_associacustosetor");
                        query.Append("   (id, idempresa, idparam, codcustoctb, codcontactb, codsetor, CodContaCTBDestino) ");
                        query.Append("   Values (" + id);
                        query.Append(", " + param.IdEmpresa);
                        query.Append(", " + param.IdParam);
                        query.Append(", " + param.CodigoCusto);
                        query.Append(", " + param.CodConta);
                        query.Append(", " + param.CodigoSetor);
                        query.Append(", " + param.CodContaDestino);
                        query.Append(")");

                        if (!sessao.ExecuteSqlTransaction(query.ToString()))
                            return false;
                    }

                }
                return true;
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

        public bool ExcluiAssociacoes(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete niff_ctb_associacustosetor");
                query.Append(" Where Id = " + id);
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

        public bool ExcluiTodasAssociacoes(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete niff_ctb_associacustosetor");
                query.Append(" Where IdParam = " + id);
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

        #endregion

        #region Rateio

        public List<RateioBeneficios.RateioPercentualCustoSetor> ListarPercentual(int rateio, int plano, string regra)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RateioBeneficios.RateioPercentualCustoSetor> _lista = new List<RateioBeneficios.RateioPercentualCustoSetor>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select r.id, r.IdRateio, r.codcusto, r.codsetor, r.quantidade, r.percentual");
                query.Append("     , c.DescCusto, c.CLASSCUSTOCTB, s.DescSetor, r.Regra");
                query.Append("  From Niff_CTB_PercentualRateio r, CtbCusto c, Flp_Setor s");
                query.Append(" Where IdRateio = " + rateio);
                query.Append("   and c.NroPlano = " + plano);
                query.Append("   and r.CodCusto = c.CodCusto");
                query.Append("   and r.CodSetor = s.CodSetor");
                query.Append("   and r.Regra = '" + regra + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        RateioBeneficios.RateioPercentualCustoSetor centro = new RateioBeneficios.RateioPercentualCustoSetor();

                        centro.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        centro.IdRateio = Convert.ToInt32(dataReader["IdRateio"].ToString());

                        centro.CodigoCusto = Convert.ToInt32(dataReader["codcusto"].ToString());
                        centro.CodigoSetor = Convert.ToInt32(dataReader["codsetor"].ToString());
                        centro.Custo = centro.CodigoCusto + " - " + dataReader["CLASSCUSTOCTB"].ToString() + " " + dataReader["DescCusto"].ToString();
                        centro.Setor = centro.CodigoSetor + " - " + dataReader["DescSetor"].ToString();

                        centro.Quantidade = Convert.ToInt32(dataReader["quantidade"].ToString());
                        centro.Percentual = Convert.ToDecimal(dataReader["Percentual"].ToString());
                        centro.Regra = dataReader["Regra"].ToString();

                        centro.Existe = true;
                        _lista.Add(centro);
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

        public List<RateioBeneficios.RateioPercentualCustoSetor> ListarFuncionariosSetor(string empresa, DateTime data, List<RateioBeneficios.Associacao> associacoes, string funcoes)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RateioBeneficios.RateioPercentualCustoSetor> _lista = new List<RateioBeneficios.RateioPercentualCustoSetor>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select f.CODSETOR, f.DESCSETOR, Count(*) quantidade");
                query.Append("  From Vw_Funcionarios f, Flp_Fichafinanceira Ff");
                query.Append(" Where Ff.Competficha = To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                if (empresa == "009/001")
                    query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '009/002'");
                else
                    query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '" + empresa + "'");
                query.Append("   and Ff.Codintfunc = f.Codintfunc");
                query.Append("   and Ff.Situacaoffinan = 'A'");
                //query.Append("   and f.SITUACAONACOMPET = 'A'");
                query.Append("   and  (Ff.Tipofolha = 1)");

                if (!string.IsNullOrEmpty(funcoes.Trim()))
                    query.Append("   and f.CodFuncao not in (" + funcoes + ")");

                query.Append(" Group By f.CODSETOR, f.DESCSETOR ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader) 
                {
                    while (dataReader.Read())
                    {
                        RateioBeneficios.RateioPercentualCustoSetor centro = new RateioBeneficios.RateioPercentualCustoSetor();

                        centro.CodigoSetor = Convert.ToInt32(dataReader["codsetor"].ToString());
                        centro.Setor = centro.CodigoSetor + " - " + dataReader["DescSetor"].ToString();

                        centro.Quantidade = Convert.ToInt32(dataReader["quantidade"].ToString());

                        foreach (var item in associacoes.GroupBy(g => new { g.CodigoCusto, g.CodigoSetor, g.Nome })
                                                        .Where(w => w.Key.CodigoSetor == centro.CodigoSetor))
                        {
                            centro.CodigoCusto = item.Key.CodigoCusto;
                            centro.Custo = centro.CodigoCusto + " - " + item.Key.Nome;
                        }

                        _lista.Add(centro);
                    }
                }

                if (_lista.Count() == 0) // não encontrou no mês, irá buscar o mês anterior e somar os adimitidos/transferidos e subtrair os demitidos e afastados
                {
                    query.Clear();
                    query.Append("Select CODSETOR, DESCSETOR, Sum(quantidade) quantidade");
                    query.Append("  from (");
                    query.Append("Select f.CODSETOR, f.DESCSETOR, Count(*) quantidade");
                    query.Append("  From Vw_Funcionarios f, Flp_Fichafinanceira Ff");
                    query.Append(" Where Ff.Competficha = Last_day(ADD_MONTHS(To_date('" + data.ToShortDateString() + "','dd/mm/yyyy'),-1))");

                    if (empresa == "009/001")
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '009/002'");
                    else
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '" + empresa + "'");

                    query.Append("   and Ff.Codintfunc = f.Codintfunc");
                    query.Append("   and Ff.Situacaoffinan = 'A'");
                    query.Append("   and f.SITUACAONACOMPET = 'A'");
                    query.Append("   and  (Ff.Tipofolha = 1)");

                    if (!string.IsNullOrEmpty(funcoes.Trim()))
                        query.Append("   and f.CodFuncao not in (" + funcoes + ")");

                    query.Append(" Group By f.CODSETOR, f.DESCSETOR ");

                    query.Append(" Union all ");

                    query.Append("Select f.Codsetor, f.Descsetor, Count(*) Quantidade ");
                    query.Append("  from vw_funcionarios f");
                    query.Append(" where f.dtadmfunc between  Last_day(ADD_MONTHS(To_date('" + data.ToShortDateString() + "','dd/mm/yyyy'),-1))+1 and To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                    if (empresa == "009/001")
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '009/002'");
                    else
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '" + empresa + "'");

                    if (!string.IsNullOrEmpty(funcoes.Trim()))
                        query.Append("   and f.CodFuncao not in (" + funcoes + ")");

                    query.Append("Group By f.Codsetor, f.Descsetor");

                    query.Append(" Union all ");

                    query.Append("Select f.Codsetor, f.Descsetor, Count(*) Quantidade ");
                    query.Append("  from vw_funcionarios f");
                    query.Append(" where f.dttransffunc between  Last_day(ADD_MONTHS(To_date('" + data.ToShortDateString() + "','dd/mm/yyyy'),-1))+1 and To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                    if (empresa == "009/001")
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '009/002'");
                    else
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '" + empresa + "'");

                    if (!string.IsNullOrEmpty(funcoes.Trim()))
                        query.Append("   and f.CodFuncao not in (" + funcoes + ")");

                    query.Append("Group By f.Codsetor, f.Descsetor");

                    query.Append(" Union all ");

                    query.Append("Select f.Codsetor, f.Descsetor, Count(*)*-1 Quantidade ");
                    query.Append("  from vw_funcionarios f, flp_quitacao q");
                    query.Append(" where q.dtdesligquita between  Last_day(ADD_MONTHS(To_date('" + data.ToShortDateString() + "','dd/mm/yyyy'),-1))+1 and To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   and q.codintfunc = f.codintfunc and q.statusquita = 'N'");

                    if (empresa == "009/001")
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '009/002'");
                    else
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '" + empresa + "'");

                    if (!string.IsNullOrEmpty(funcoes.Trim()))
                        query.Append("   and f.CodFuncao not in (" + funcoes + ")");

                    query.Append("Group By f.Codsetor, f.Descsetor");

                    query.Append(") Group By CODSETOR, DESCSETOR ");
                    executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();
                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            RateioBeneficios.RateioPercentualCustoSetor centro = new RateioBeneficios.RateioPercentualCustoSetor();

                            centro.CodigoSetor = Convert.ToInt32(dataReader["codsetor"].ToString());
                            centro.Setor = centro.CodigoSetor + " - " + dataReader["DescSetor"].ToString();

                            centro.Quantidade = Convert.ToInt32(dataReader["quantidade"].ToString());

                            foreach (var item in associacoes.GroupBy(g => new { g.CodigoCusto, g.CodigoSetor, g.Nome })
                                                            .Where(w => w.Key.CodigoSetor == centro.CodigoSetor))
                            {
                                centro.CodigoCusto = item.Key.CodigoCusto;
                                centro.Custo = centro.CodigoCusto + " - " + item.Key.Nome;
                            }

                            _lista.Add(centro);
                        }
                    }
                }

                decimal total = _lista.Sum(s => s.Quantidade);
                decimal diferenca = 0;

                foreach (var item in _lista)
                {
                    item.Percentual = Math.Round((item.Quantidade / total) *100,3);
                }

                total = _lista.Sum(s => s.Percentual);
                 
                if (100 != total)
                {
                    diferenca = 100 - total;

                    foreach (var itemD in _lista.OrderByDescending(o => o.CodigoCusto))
                    {
                        itemD.Percentual = itemD.Percentual + diferenca;
                        break;
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

        public List<RateioBeneficios.RateioPercentualCustoSetor> ListarFuncionariosSetorPorEvento(string empresa, DateTime data, List<RateioBeneficios.Associacao> associacoes, string funcoes, string eventos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RateioBeneficios.RateioPercentualCustoSetor> _lista = new List<RateioBeneficios.RateioPercentualCustoSetor>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select f.CODSETOR, f.DESCSETOR, Count(*) quantidade");
                query.Append("  From Vw_Funcionarios f, Flp_Fichafinanceira Ff, flp_fichaeventos Fe");
                query.Append(" Where Ff.Competficha = To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                if (empresa == "009/001")
                    query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '009/002'");
                else
                    query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '" + empresa + "'");
                query.Append("   and Ff.Codintfunc = f.Codintfunc");

                //query.Append("   and Ff.Situacaoffinan = 'A'");

                query.Append("   and Ff.Tipofolha = 1");

                if (!string.IsNullOrEmpty(funcoes.Trim()))
                    query.Append("   and f.CodFuncao not in (" + funcoes + ")");

                query.Append("   and Fe.Tipofolha = 1");
                query.Append("   and Fe.Codintfunc = f.Codintfunc");
                query.Append("   and Fe.competficha = ff.competficha");

                if (!string.IsNullOrEmpty(eventos.Trim()))
                    query.Append("   and fe.codevento in (" + eventos+ ")");

                query.Append(" Group By f.CODSETOR, f.DESCSETOR ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        RateioBeneficios.RateioPercentualCustoSetor centro = new RateioBeneficios.RateioPercentualCustoSetor();

                        centro.CodigoSetor = Convert.ToInt32(dataReader["codsetor"].ToString());
                        centro.Setor = centro.CodigoSetor + " - " + dataReader["DescSetor"].ToString();

                        centro.Quantidade = Convert.ToInt32(dataReader["quantidade"].ToString());

                        foreach (var item in associacoes.GroupBy(g => new { g.CodigoCusto, g.CodigoSetor, g.Nome }) 
                                                        .Where(w => w.Key.CodigoSetor == centro.CodigoSetor))
                        {
                            centro.CodigoCusto = item.Key.CodigoCusto;
                            centro.Custo = centro.CodigoCusto + " - " + item.Key.Nome;
                        }

                        _lista.Add(centro);
                    }
                }

                //if (_lista.Count() == 0) // para teste
                //{
                //    query.Clear();
                //    query.Append("Select f.CODSETOR, f.DESCSETOR, Count(*) quantidade");
                //    query.Append("  From Vw_Funcionarios f, Flp_Fichafinanceira Ff, flp_fichaeventos Fe");
                //    query.Append(" Where Ff.Competficha = To_date('30/03/2019','dd/mm/yyyy')");
                //    if (empresa == "009/001")
                //        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '009/002'");
                //    else
                //        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '" + empresa + "'");
                //    query.Append("   and Ff.Codintfunc = f.Codintfunc");

                //    //query.Append("   and Ff.Situacaoffinan = 'A'");

                //    query.Append("   and Ff.Tipofolha = 1");

                //    if (!string.IsNullOrEmpty(funcoes.Trim()))
                //        query.Append("   and f.CodFuncao not in (" + funcoes + ")");

                //    query.Append("   and Fe.Tipofolha = 1");
                //    query.Append("   and Fe.Codintfunc = f.Codintfunc");
                //    query.Append("   and Fe.competficha = ff.competficha");

                //    if (!string.IsNullOrEmpty(eventos.Trim()))
                //        query.Append("   and fe.codevento in (" + eventos + ")");

                //    query.Append(" Group By f.CODSETOR, f.DESCSETOR ");

                //    executar = sessao.CreateQuery(query.ToString());

                //    dataReader = executar.ExecuteQuery();
                //    using (dataReader)
                //    {
                //        while (dataReader.Read())
                //        {
                //            RateioBeneficios.RateioPercentualCustoSetor centro = new RateioBeneficios.RateioPercentualCustoSetor();

                //            centro.CodigoSetor = Convert.ToInt32(dataReader["codsetor"].ToString());
                //            centro.Setor = centro.CodigoSetor + " - " + dataReader["DescSetor"].ToString();

                //            centro.Quantidade = Convert.ToInt32(dataReader["quantidade"].ToString());

                //            foreach (var item in associacoes.GroupBy(g => new { g.CodigoCusto, g.CodigoSetor, g.Nome })
                //                                            .Where(w => w.Key.CodigoSetor == centro.CodigoSetor))
                //            {
                //                centro.CodigoCusto = item.Key.CodigoCusto;
                //                centro.Custo = centro.CodigoCusto + " - " + item.Key.Nome;
                //            }

                //            _lista.Add(centro);
                //        }
                //    }
                //}

                decimal total = _lista.Sum(s => s.Quantidade);
                decimal diferenca = 0;

                foreach (var item in _lista)
                {
                    item.Percentual = Math.Round((item.Quantidade / total) * 100, 3);
                }

                total = _lista.Sum(s => s.Percentual);

                if (100 != total)
                {
                    diferenca = 100 - total;

                    foreach (var itemD in _lista.OrderByDescending(o => o.CodigoCusto))
                    {
                        itemD.Percentual = itemD.Percentual + diferenca;
                        break;
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

        public List<RateioBeneficios.RateioPercentualCustoSetor> ListarFuncionariosComConvenio(string empresa, DateTime data, List<RateioBeneficios.Associacao> associacoes, string funcoes, bool ignorarMedico, bool IgnorarOdonto)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RateioBeneficios.RateioPercentualCustoSetor> _lista = new List<RateioBeneficios.RateioPercentualCustoSetor>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select f.CODSETOR, f.DESCSETOR, Count(*) quantidade");
                query.Append("  From Vw_Funcionarios f, Flp_Fichafinanceira Ff");
                query.Append(" Where Ff.Competficha = To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                if (empresa == "009/001")
                    query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '009/002'");
                else
                    query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '" + empresa + "'");
                query.Append("   and Ff.Codintfunc = f.Codintfunc");
                query.Append("   and Ff.Situacaoffinan = 'A'");
                //query.Append("   and f.SITUACAONACOMPET = 'A'");
                query.Append("   and  (Ff.Tipofolha = 1)");

                if (!string.IsNullOrEmpty(funcoes.Trim()))
                    query.Append("   and f.CodFuncao not in (" + funcoes + ")");

                if (ignorarMedico)
                    query.Append("   and f.CODASMED is not null");

                if (IgnorarOdonto)
                    query.Append("   and f.CODODONT is not null");

                query.Append(" Group By f.CODSETOR, f.DESCSETOR ");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        RateioBeneficios.RateioPercentualCustoSetor centro = new RateioBeneficios.RateioPercentualCustoSetor();

                        centro.CodigoSetor = Convert.ToInt32(dataReader["codsetor"].ToString());
                        centro.Setor = centro.CodigoSetor + " - " + dataReader["DescSetor"].ToString();

                        centro.Quantidade = Convert.ToInt32(dataReader["quantidade"].ToString());

                        foreach (var item in associacoes.GroupBy(g => new { g.CodigoCusto, g.CodigoSetor, g.Nome })
                                                        .Where(w => w.Key.CodigoSetor == centro.CodigoSetor))
                        {
                            centro.CodigoCusto = item.Key.CodigoCusto;
                            centro.Custo = centro.CodigoCusto + " - " + item.Key.Nome;
                        }

                        _lista.Add(centro);
                    }
                }

                if (_lista.Count() == 0) // não encontrou no mês, irá buscar o mês anterior e somar os adimitidos/transferidos e subtrair os demitidos e afastados
                {
                    query.Clear();
                    query.Append("Select CODSETOR, DESCSETOR, Sum(quantidade) quantidade");
                    query.Append("  from (");
                    query.Append("Select f.CODSETOR, f.DESCSETOR, Count(*) quantidade");
                    query.Append("  From Vw_Funcionarios f, Flp_Fichafinanceira Ff");
                    query.Append(" Where Ff.Competficha = Last_day(ADD_MONTHS(To_date('" + data.ToShortDateString() + "','dd/mm/yyyy'),-1))");

                    if (empresa == "009/001")
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '009/002'");
                    else
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '" + empresa + "'");

                    query.Append("   and Ff.Codintfunc = f.Codintfunc");
                    query.Append("   and Ff.Situacaoffinan = 'A'");
                    query.Append("   and f.SITUACAONACOMPET = 'A'");
                    query.Append("   and  (Ff.Tipofolha = 1)");

                    if (!string.IsNullOrEmpty(funcoes.Trim()))
                        query.Append("   and f.CodFuncao not in (" + funcoes + ")");
                    
                    if (ignorarMedico)
                        query.Append("   and f.CODASMED is not null");

                    if (IgnorarOdonto)
                        query.Append("   and f.CODODONT is not null");

                    query.Append(" Group By f.CODSETOR, f.DESCSETOR ");

                    query.Append(" Union all ");

                    query.Append("Select f.Codsetor, f.Descsetor, Count(*) Quantidade ");
                    query.Append("  from vw_funcionarios f");
                    query.Append(" where f.dtadmfunc between  Last_day(ADD_MONTHS(To_date('" + data.ToShortDateString() + "','dd/mm/yyyy'),-1))+1 and To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                    if (empresa == "009/001")
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '009/002'");
                    else
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '" + empresa + "'");

                    if (!string.IsNullOrEmpty(funcoes.Trim()))
                        query.Append("   and f.CodFuncao not in (" + funcoes + ")");

                    query.Append("Group By f.Codsetor, f.Descsetor");

                    query.Append(" Union all ");

                    query.Append("Select f.Codsetor, f.Descsetor, Count(*) Quantidade ");
                    query.Append("  from vw_funcionarios f");
                    query.Append(" where f.dttransffunc between  Last_day(ADD_MONTHS(To_date('" + data.ToShortDateString() + "','dd/mm/yyyy'),-1))+1 and To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");

                    if (empresa == "009/001")
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '009/002'");
                    else
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '" + empresa + "'");

                    if (!string.IsNullOrEmpty(funcoes.Trim()))
                        query.Append("   and f.CodFuncao not in (" + funcoes + ")");

                    query.Append("Group By f.Codsetor, f.Descsetor");

                    query.Append(" Union all ");

                    query.Append("Select f.Codsetor, f.Descsetor, Count(*)*-1 Quantidade ");
                    query.Append("  from vw_funcionarios f, flp_quitacao q");
                    query.Append(" where q.dtdesligquita between  Last_day(ADD_MONTHS(To_date('" + data.ToShortDateString() + "','dd/mm/yyyy'),-1))+1 and To_date('" + data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append("   and q.codintfunc = f.codintfunc and q.statusquita = 'N'");

                    if (empresa == "009/001")
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '009/002'");
                    else
                        query.Append("   and Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(f.Codigofl,3,'0') = '" + empresa + "'");

                    if (!string.IsNullOrEmpty(funcoes.Trim()))
                        query.Append("   and f.CodFuncao not in (" + funcoes + ")");

                    query.Append("Group By f.Codsetor, f.Descsetor");

                    query.Append(") Group By CODSETOR, DESCSETOR ");
                    executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();
                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            RateioBeneficios.RateioPercentualCustoSetor centro = new RateioBeneficios.RateioPercentualCustoSetor();

                            centro.CodigoSetor = Convert.ToInt32(dataReader["codsetor"].ToString());
                            centro.Setor = centro.CodigoSetor + " - " + dataReader["DescSetor"].ToString();

                            centro.Quantidade = Convert.ToInt32(dataReader["quantidade"].ToString());

                            foreach (var item in associacoes.GroupBy(g => new { g.CodigoCusto, g.CodigoSetor, g.Nome })
                                                            .Where(w => w.Key.CodigoSetor == centro.CodigoSetor))
                            {
                                centro.CodigoCusto = item.Key.CodigoCusto;
                                centro.Custo = centro.CodigoCusto + " - " + item.Key.Nome;
                            }

                            _lista.Add(centro);
                        }
                    }
                }

                decimal total = _lista.Sum(s => s.Quantidade);
                decimal diferenca = 0;

                foreach (var item in _lista)
                {
                    item.Percentual = Math.Round((item.Quantidade / total) * 100, 3);
                }

                total = _lista.Sum(s => s.Percentual);

                if (100 != total)
                {
                    diferenca = 100 - total;

                    foreach (var itemD in _lista.OrderByDescending(o => o.CodigoCusto))
                    {
                        itemD.Percentual = itemD.Percentual + diferenca;
                        break;
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


        public List<RateioBeneficios.ValoresParaRatear> ListarLancamentosCTBComCustoTransitorio(string empresa, DateTime inicio, DateTime fim, int param)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RateioBeneficios.ValoresParaRatear> _lista = new List<RateioBeneficios.ValoresParaRatear>();
            Publicas.mensagemDeErro = string.Empty;

            List<RateioBeneficios.Custos> _custosTransitorios = ListarCustosDoParametro(param);

            try
            {
                foreach (var item in _custosTransitorios)
                {
                    query.Clear();
                    query.Append("Select Sum(decode(i.debitocreditoitemlanca, 'D', 1, -1) * i.vritemlanca) Valor, i.codcontactb, i.codcusto");
                    query.Append("  From ctblanca l, ctbitlnc i");
                    query.Append(" Where Lpad(l.Codigoempresa, 3, '0') || '/' || Lpad(l.Codigofl,3,'0') = '" + empresa + "'");
                    query.Append("   and l.dtlanca between To_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy') and ");
                    query.Append("   To_date('" + fim.ToShortDateString() + "','dd/mm/yyyy') ");
                    query.Append("   and i.codcusto = " + item.CodigoCusto);
                    //query.Append("   and sistema <> 'CTB'");
                    query.Append("   and l.codlanca = i.codlanca");
                    query.Append(" Group By i.codcontactb, i.codcusto  ");

                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();
                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            RateioBeneficios.ValoresParaRatear centro = new RateioBeneficios.ValoresParaRatear();

                            centro.Valor = Convert.ToDecimal(dataReader["Valor"].ToString());
                            centro.CodigoCusto = Convert.ToInt32(dataReader["CodCusto"].ToString());
                            centro.CodigoConta = Convert.ToInt32(dataReader["codcontactb"].ToString());
                            _lista.Add(centro);
                        }
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

        public RateioBeneficios.Rateio ConsultarRateio(int empresa, int referencia)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            RateioBeneficios.Rateio centro = new RateioBeneficios.Rateio();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Select id, idempresa, referencia, idusuario, datagravacao, geradoarquivo");
                query.Append("  From Niff_CTB_Rateio c");
                query.Append(" Where c.IdEmpresa = " + empresa);
                query.Append("   and c.Referencia = " + referencia);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        centro.Existe = true;
                        centro.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        centro.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        centro.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        centro.Referencia = Convert.ToInt32(dataReader["Referencia"].ToString());

                        centro.Data = Convert.ToDateTime(dataReader["datagravacao"].ToString());
                        centro.ArquivoGerado = dataReader["geradoarquivo"].ToString() == "S";
                    
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
            return centro;
        }

        public List<RateioBeneficios.ValoresRateados> ListarRateio(int id, int plano)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RateioBeneficios.ValoresRateados> _lista = new List<RateioBeneficios.ValoresRateados>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Select r.id, r.idrateio, r.codcontactb, r.contrapartida, r.codcusto, r.debito, r.credito, r.historico, r.CodCustoCredito");
                query.Append("     , r.Documento, cc.classcustoctb || ' ' || cc.Desccusto nomeCusto");
                query.Append("     , t.Classificador || ' ' || t.NomeConta nomeConta, t.NomeConta descConta");

                query.Append("  From Niff_CTB_Rateio c, Niff_CTB_ValoresRateio r");
                query.Append("     , CTBConta t, CTBCusto cc");
                query.Append(" Where c.Id = " + id);
                query.Append("   and r.idrateio = c.Id");
                query.Append("   and r.codcontactb = t.codcontactb");
                query.Append("   and t.nroplano = " + plano);
                query.Append("   and r.codcusto = cc.codcusto");
                query.Append("   and cc.nroplano = " + plano);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        RateioBeneficios.ValoresRateados centro = new RateioBeneficios.ValoresRateados();

                        centro.Existe = true;
                        centro.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        centro.IdRateio = Convert.ToInt32(dataReader["IdRateio"].ToString());

                        centro.Debito = Convert.ToDecimal(dataReader["Debito"].ToString());
                        centro.Credito = Convert.ToDecimal(dataReader["Credito"].ToString());

                        centro.CodigoCusto = Convert.ToInt32(dataReader["CodCusto"].ToString());
                        try
                        {
                            centro.CodigoCustoCredito = Convert.ToInt32(dataReader["CodCustoCredito"].ToString());
                        }
                        catch { }

                        centro.CodigoConta = Convert.ToInt32(dataReader["codcontactb"].ToString());

                        centro.NomeConta = centro.CodigoConta + " - " + dataReader["nomeConta"].ToString();
                        centro.NomeCusto = centro.CodigoCusto + " - " + dataReader["nomeCusto"].ToString();
                        centro.Conta = dataReader["descConta"].ToString();
                        try
                        {
                            centro.ContraPartida = Convert.ToInt32(dataReader["contrapartida"].ToString());

                            RateioBeneficios.ContasContabeis _conta = Consulta(plano, centro.ContraPartida);
                            centro.NomeContraPartida = centro.ContraPartida + " - " + _conta.Classificador + " " + _conta.Nome;

                        }
                        catch { }

                        centro.Historico = dataReader["historico"].ToString();
                        centro.Documento = dataReader["Documento"].ToString();
                        _lista.Add(centro);
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

        public bool Grava(RateioBeneficios.Rateio _rateio, List<RateioBeneficios.RateioPercentualCustoSetor> _percentual, List<RateioBeneficios.ValoresRateados> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            int id = 1;
            int idValores = 1;
            int idPercentual = 1;
            Query executar;

            try
            {
                if (!_rateio.Existe)
                {
                    query.Clear();
                    query.Append("Select Nvl(Max(Id),0) + 1 next From Niff_CTB_Rateio");
                    executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            id = Convert.ToInt32(dataReader["next"].ToString());
                    }

                    query.Clear();
                    query.Append("Insert into Niff_CTB_Rateio");
                    query.Append("   (id, idempresa, referencia, idusuario, datagravacao, geradoarquivo) ");
                    query.Append("   Values (" + id);
                    query.Append(", " + _rateio.IdEmpresa);
                    query.Append(", " + _rateio.Referencia);
                    query.Append(", " + _rateio.IdUsuario);
                    query.Append(", sysdate ");
                    query.Append(", '" + (_rateio.ArquivoGerado ? "S" : "N") + "'");
                    query.Append(")");
                }
                else
                {
                    id = _rateio.Id;
                    query.Clear();
                    query.Append("Update Niff_CTB_Rateio");
                    query.Append("   set idusuario = " + _rateio.IdUsuario );
                    query.Append("     , geradoarquivo = '" + (_rateio.ArquivoGerado ? "S" : "N") + "'");
                    query.Append("     , datagravacao = sysdate");
                    query.Append(" Where Id = " + _rateio.Id);
                }

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;
                else
                {
                    foreach (var param in _lista)
                    {
                        if (!param.Existe)
                        {
                            query.Clear();
                            query.Append("Select Nvl(Max(Id),0) + 1 next From Niff_CTB_ValoresRateio");
                            executar = sessao.CreateQuery(query.ToString());

                            dataReader = executar.ExecuteQuery();

                            using (dataReader)
                            {
                                if (dataReader.Read())
                                    idValores = Convert.ToInt32(dataReader["next"].ToString());
                            }

                            query.Clear();
                            query.Append("Insert into Niff_CTB_ValoresRateio");
                            query.Append("   (id, idrateio, codcontactb, contrapartida, codcusto, debito, credito, historico, documento, CodCustoCredito) ");
                            query.Append("   Values (" + idValores);
                            query.Append(", " + id);
                            query.Append(", " + param.CodigoConta);

                            if (param.ContraPartida == 0)
                                query.Append(", null ");
                            else
                                query.Append(", " + param.ContraPartida);

                            query.Append(", " + param.CodigoCusto);
                            query.Append(", " + param.Debito.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(", " + param.Credito.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(", '" + param.Historico + "'");
                            query.Append(", '" + param.Documento + "'");
                            query.Append(", " + param.CodigoCustoCredito);
                            query.Append(")");

                            if (!sessao.ExecuteSqlTransaction(query.ToString()))
                                return false;
                        }
                        else
                        {
                            query.Clear();
                            query.Append("Update Niff_CTB_ValoresRateio");
                            query.Append("   set Debito = " + param.Debito.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , Credito = " + param.Credito.ToString().Replace(".", "").Replace(",", "."));
                            query.Append("     , Historico = '" + param.Historico + "'");
                            query.Append("     , CodCustoCredito = " + param.CodigoCustoCredito);
                            query.Append(" Where Id = " + param.Id);

                            if (!sessao.ExecuteSqlTransaction(query.ToString()))
                                return false;
                        }
                    }

                    foreach (var item in _percentual)
                    {
                        if (!item.Existe)
                        {
                            query.Clear();
                            query.Append("Select Nvl(Max(Id),0) + 1 next From Niff_CTB_PercentualRateio");
                            executar = sessao.CreateQuery(query.ToString());

                            dataReader = executar.ExecuteQuery();

                            using (dataReader)
                            {
                                if (dataReader.Read())
                                    idPercentual = Convert.ToInt32(dataReader["next"].ToString());
                            }

                            query.Clear();
                            query.Append("Insert into Niff_CTB_PercentualRateio");
                            query.Append("   (id, codcusto, codsetor, quantidade, percentual, idrateio, Regra) ");
                            query.Append("   Values (" + idPercentual);
                            query.Append(", " + item.CodigoCusto);
                            query.Append(", " + item.CodigoSetor);
                            query.Append(", " + item.Quantidade);
                            query.Append(", " + item.Percentual.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(", " + id);
                            query.Append(", '" + item.Regra + "'");
                            query.Append(")");

                            if (!sessao.ExecuteSqlTransaction(query.ToString()))
                                return false;
                        }
                        else
                        {
                            query.Clear();
                            query.Append("Update Niff_CTB_PercentualRateio");
                            query.Append("   set quantidade = " + item.Quantidade);
                            query.Append("     , Percentual = " + item.Percentual.ToString().Replace(".", "").Replace(",", "."));
                            query.Append(" Where Id = " + item.Id);

                            if (!sessao.ExecuteSqlTransaction(query.ToString()))
                                return false;
                        }
                    }
                }
                return true;
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

        public bool ExcluirRateio(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_CTB_ValoresRateio");
                query.Append(" Where IdRateio = " + id);

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;
                else
                {
                    query.Clear();
                    query.Append("Delete Niff_CTB_PercentualRateio");
                    query.Append(" Where IdRateio = " + id);

                    if (!sessao.ExecuteSqlTransaction(query.ToString()))
                        return false;
                    else
                    {
                        query.Clear();
                        query.Append("Delete Niff_CTB_Rateio");
                        query.Append(" Where Id = " + id);

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
        #endregion
    }
}

