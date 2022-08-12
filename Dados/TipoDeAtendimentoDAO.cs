using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class TipoDeAtendimentoDAO
    {
        IDataReader tipoReader;

        public List<TipoDeAtendimento> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<TipoDeAtendimento> _lista = new List<TipoDeAtendimento>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IdTipoAtendimento ");
                query.Append("     , Descricao");
                query.Append("     , ativo");

                query.Append("  From Niff_Chm_TipoAtendimento ");

                Query executar = sessao.CreateQuery(query.ToString());

                tipoReader = executar.ExecuteQuery();

                using (tipoReader)
                {
                    while (tipoReader.Read())
                    {
                        TipoDeAtendimento _tipo = new TipoDeAtendimento();
                        _tipo.IdTipoAtendimento = Convert.ToInt32(tipoReader["IdTipoAtendimento"].ToString());
                        _tipo.Descricao = tipoReader["Descricao"].ToString();
                        _tipo.Ativo = tipoReader["ativo"].ToString() == "S";
                        _tipo.Existe = true;

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

        public TipoDeAtendimento Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            TipoDeAtendimento _tipo = new TipoDeAtendimento();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IdTipoAtendimento ");
                query.Append("     , Descricao");
                query.Append("     , ativo");

                query.Append("  From Niff_Chm_TipoAtendimento ");
                query.Append(" Where IdTipoAtendimento = " + codigo.ToString());

                Query executar = sessao.CreateQuery(query.ToString());

                tipoReader = executar.ExecuteQuery();

                using (tipoReader)
                {
                    if (tipoReader.Read())
                    {
                        _tipo.IdTipoAtendimento = Convert.ToInt32(tipoReader["IdTipoAtendimento"].ToString());
                        _tipo.Descricao = tipoReader["Descricao"].ToString();
                        _tipo.Ativo = tipoReader["ativo"].ToString() == "S";
                        _tipo.Existe = true;
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

        public bool Grava(TipoDeAtendimento tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!tipo.Existe)
                {
                    query.Clear();
                    query.Append("Insert into Niff_Chm_TipoAtendimento");
                    query.Append("   (IdTipoAtendimento, descricao, ativo) ");
                    query.Append("   Values (" + tipo.IdTipoAtendimento);
                    query.Append(", '" + tipo.Descricao + "'");
                    query.Append(", '" + (tipo.Ativo ? "S" : "N") + "')");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Chm_TipoAtendimento");
                    query.Append("   set descricao = '" + tipo.Descricao + "', ");
                    query.Append("       ativo = '" + (tipo.Ativo ? "S" : "N") + "' ");
                    query.Append(" Where IdTipoAtendimento = " + tipo.IdTipoAtendimento);
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

        public bool Exclui(TipoDeAtendimento tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (tipo.IdTipoAtendimento != 0)
                {
                    query.Append("Delete Niff_Chm_TipoAtendimento");
                    query.Append(" Where IdTipoAtendimento = " + tipo.IdTipoAtendimento);
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

        public int Proximo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Max(IdTipoAtendimento) + 1 next From Niff_Chm_TipoAtendimento");
                Query executar = sessao.CreateQuery(query.ToString());

                tipoReader = executar.ExecuteQuery();

                using (tipoReader)
                {
                    if (tipoReader.Read())
                        retorno = Convert.ToInt32(tipoReader["next"].ToString());
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
    }
}
