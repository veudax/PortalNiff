using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class TipoDePagamentoDAO
    {
        IDataReader tipoReader;

        public List<TipoDePagamento> Listar(bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<TipoDePagamento> _lista = new List<TipoDePagamento>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idtipo, descricao, ativo");
                query.Append("  from Niff_Jur_Tipo");
                if (apenasAtivos)
                    query.Append(" Where ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                tipoReader = executar.ExecuteQuery();

                using (tipoReader)
                {
                    while (tipoReader.Read())
                    {
                        TipoDePagamento _tipo = new TipoDePagamento();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(tipoReader["idTipo"].ToString());
                        _tipo.Descricao = tipoReader["Descricao"].ToString();
                        _tipo.Ativo = tipoReader["Ativo"].ToString() == "S";

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

        public TipoDePagamento Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            TipoDePagamento _tipo = new TipoDePagamento();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idtipo, descricao, ativo");
                query.Append("  from Niff_Jur_Tipo");
                query.Append(" Where idTipo = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                tipoReader = executar.ExecuteQuery();

                using (tipoReader)
                {
                    if (tipoReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(tipoReader["idTipo"].ToString());
                        _tipo.Descricao = tipoReader["Descricao"].ToString();
                        _tipo.Ativo = tipoReader["Ativo"].ToString() == "S";
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

                query.Append("Select Max(idTipo) + 1 next From Niff_Jur_Tipo");
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

        public bool Gravar(TipoDePagamento tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into Niff_Jur_Tipo");
                    query.Append(" (IDTIPO, DESCRICAO, ATIVO )");
                    query.Append(" Values (" + tipo.Id);
                    query.Append("        ,'" + tipo.Descricao + "'");
                    query.Append("        ,'" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_Jur_Tipo");
                    query.Append("   set DESCRICAO = '" + tipo.Descricao + "'");
                    query.Append("     , ATIVO = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append(" Where IdTipo = " + tipo.Id);
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

        public bool Excluir(TipoDePagamento tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Jur_Tipo");
                query.Append(" Where IdTipo = " + tipo.Id);
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
