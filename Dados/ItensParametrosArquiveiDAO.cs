using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ItensParametrosArquiveiDAO
    {
        IDataReader dataReader;

        public List<ItensParametrosArquivei> Listar(int IdParametro)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ItensParametrosArquivei> _lista = new List<ItensParametrosArquivei>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select iditens, idparametro, campovalidar, campoexibir, nomecampo, tipo");
                query.Append("  from NIFF_FIS_ItensParamArquivei");
                query.Append(" Where idparametro = " + IdParametro);
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ItensParametrosArquivei _tipo = new ItensParametrosArquivei();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["IdItens"].ToString());
                        _tipo.IdParametro = Convert.ToInt32(dataReader["idParametro"].ToString());

                        _tipo.ValidarCampo = dataReader["CampoValidar"].ToString() == "S";
                        _tipo.ExibirCampo = dataReader["CampoExibir"].ToString() == "S";
                        _tipo.NomeCampo = dataReader["NomeCampo"].ToString();
                        _tipo.Tipo = dataReader["Tipo"].ToString();

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

        public bool Grava(List<ItensParametrosArquivei> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int Id = 1;
            bool retorno = false;

            try
            {
                foreach (var parametros in _lista)
                {

                    if (!parametros.Existe)
                    {
                        query.Clear();
                        query.Append("Select nvl(Max(iditens),0)+1 next From NIFF_FIS_ItensParamArquivei ");
                        Query executar = sessao.CreateQuery(query.ToString());
                        dataReader = executar.ExecuteQuery();

                        using (dataReader)
                        {
                            if (dataReader.Read())
                                Id = Convert.ToInt32(dataReader["next"].ToString());
                        }

                        query.Clear();
                        query.Append("Insert into NIFF_FIS_ItensParamArquivei");
                        query.Append("   (iditens, idparametro, campovalidar, campoexibir, nomecampo, tipo");

                        query.Append("  ) Values ( " + Id);
                        query.Append(", " + parametros.IdParametro);
                        query.Append(", '" + (parametros.ValidarCampo ? "S" : "N") + "'");
                        query.Append(", '" + (parametros.ExibirCampo ? "S" : "N") + "'");
                        query.Append(", '" + parametros.NomeCampo + "'");
                        query.Append(", '" + parametros.Tipo + "'");
                        query.Append(") ");
                    }
                    else
                    {
                        query.Clear();
                        query.Append("Update NIFF_FIS_ItensParamArquivei");
                        query.Append("   set NomeCampo = '" + parametros.NomeCampo + "'");
                        query.Append("     , CampoValidar = '" + (parametros.ValidarCampo ? "S" : "N") + "'");
                        query.Append("     , CampoExibir = '" + (parametros.ExibirCampo ? "S" : "N") + "'");
                        query.Append(" Where iditens = " + parametros.Id);
                    }

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                        return false;
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
                query.Append("Delete NIFF_FIS_ItensParamArquivei");
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
