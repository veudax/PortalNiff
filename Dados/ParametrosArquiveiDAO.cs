using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ParametrosArquiveiDAO
    {
        IDataReader dataReader;

        public List<ParametrosArquivei> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ParametrosArquivei> _lista = new List<ParametrosArquivei>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idparametro, idempresa, diretorio, datacadastro, dataalteracao, AcaoComArquivo, DiretorioExportacao, DiretorioDacte, DiretorioNFSE");
                query.Append("  from NIFF_FIS_ParametrosArquivei");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    { // inicio criação lista _tipo
                        ParametrosArquivei _tipo = new ParametrosArquivei();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idparametro"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());

                        _tipo.Diretorio = dataReader["Diretorio"].ToString();
                        _tipo.DiretorioExportacao = dataReader["DiretorioExportacao"].ToString();
                        _tipo.DiretorioDacte = dataReader["DiretorioDacte"].ToString();
                        _tipo.DiretorioNFSe = dataReader["DiretorioNFSe"].ToString();

                        _tipo.AcaoComArquivo = dataReader["AcaoComArquivo"].ToString();
                        try
                        {
                            _tipo.DataCadastro = Convert.ToDateTime(dataReader["DataCadastro"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataAlteracao = Convert.ToDateTime(dataReader["DataAlteracao"].ToString());
                        }
                        catch { }

                        _lista.Add(_tipo);
                    } // Fim criação lista tipo
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

        public ParametrosArquivei Consultar(int IdEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            ParametrosArquivei _tipo = new ParametrosArquivei();

            try
            {
                query.Append("Select idparametro, idempresa, diretorio, datacadastro, dataalteracao, AcaoComArquivo, DiretorioExportacao, DiretorioDacte, DiretorioNFSE");
                query.Append("  from NIFF_FIS_ParametrosArquivei");
                query.Append(" Where IdEmpresa = " + IdEmpresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idparametro"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["idEmpresa"].ToString());
                        _tipo.AcaoComArquivo = dataReader["AcaoComArquivo"].ToString();
                        _tipo.Diretorio = dataReader["Diretorio"].ToString();
                        _tipo.DiretorioExportacao = dataReader["DiretorioExportacao"].ToString();
                        _tipo.DiretorioDacte = dataReader["DiretorioDacte"].ToString();
                        _tipo.DiretorioNFSe = dataReader["DiretorioNFSE"].ToString();

                        try
                        {
                            _tipo.DataCadastro = Convert.ToDateTime(dataReader["DataCadastro"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.DataAlteracao = Convert.ToDateTime(dataReader["DataAlteracao"].ToString());
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

        public bool Grava(ParametrosArquivei parametros, List<ItensParametrosArquivei> itens)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int Id = 1;

            bool retorno = false;

            try
            {
                if (!parametros.Existe)
                {
                    query.Clear();
                    query.Append("Select nvl(Max(idparametro),0)+1 next From NIFF_FIS_ParametrosArquivei ");
                    Query executar = sessao.CreateQuery(query.ToString());

                    dataReader = executar.ExecuteQuery();

                    using (dataReader)
                    {
                        if (dataReader.Read())
                            Id = Convert.ToInt32(dataReader["next"].ToString());
                    }

                    query.Clear();
                    query.Append("Insert into NIFF_FIS_ParametrosArquivei");
                    query.Append("   (idparametro, idempresa, diretorio, datacadastro, AcaoComArquivo, DiretorioExportacao, DiretorioDacte, DiretorioNFSE"); 

                    query.Append("  ) Values ( " + Id);
                    query.Append(", " + parametros.IdEmpresa);
                    query.Append(", '" + parametros.Diretorio + "'");
                    query.Append(", SysDate" );
                    query.Append(", '" + parametros.AcaoComArquivo + "'");
                    query.Append(", '" + parametros.DiretorioExportacao + "'");
                    query.Append(", '" + parametros.DiretorioDacte + "'");
                    query.Append(", '" + parametros.DiretorioNFSe + "'");
                    query.Append(") ");
                }
                else
                {
                    Id = parametros.Id;
                    query.Clear();
                    query.Append("Update NIFF_FIS_ParametrosArquivei");
                    query.Append("   set Diretorio = '" + parametros.Diretorio + "'");
                    query.Append("     , dataAlteracao = SysDate");
                    query.Append("     , AcaoComArquivo = '" + parametros.AcaoComArquivo + "'");
                    query.Append("     , DiretorioExportacao = '" + parametros.DiretorioExportacao + "'");
                    query.Append("     , DiretorioDacte = '" + parametros.DiretorioDacte + "'");
                    query.Append("     , DiretorioNFSE = '" + parametros.DiretorioNFSe + "'");
                    query.Append(" Where Idparametro = " + parametros.Id);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    itens.ForEach(u => u.IdParametro = Id);
                    retorno = new ItensParametrosArquiveiDAO().Grava(itens);
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

        public bool Exclui(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!new ItensParametrosArquiveiDAO().Exclui(id))
                    return false;

                query.Append("Delete NIFF_FIS_ParametrosArquivei");
                query.Append(" Where Idparametro = " + id);

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
