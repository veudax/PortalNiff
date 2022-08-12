using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class CompetenciasDAO
    {
        IDataReader dadosReader;

        public List<Competencias> Listar(bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Competencias> _lista = new List<Competencias>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idcompetencia, descricao, tipo, ativo, TextoExplicativo");
                query.Append("  from Niff_ADS_Competencias");
                if (apenasAtivos)
                    query.Append(" Where ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Competencias _tipo = new Competencias();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dadosReader["idcompetencia"].ToString());
                        _tipo.Descricao = dadosReader["Descricao"].ToString();
                        _tipo.Ativo = dadosReader["Ativo"].ToString() == "S";
                        _tipo.Tipo = (dadosReader["Tipo"].ToString() == "T" ? Publicas.TipoCompetencias.Tecnica : Publicas.TipoCompetencias.Comportamental);
                        _tipo.TextoExplicativo = dadosReader["TextoExplicativo"].ToString();
                        _tipo.DescricaoTipo = Publicas.GetDescription(_tipo.Tipo, "");
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

        public Competencias Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Competencias _tipo = new Competencias();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select idcompetencia, descricao, tipo, ativo, TextoExplicativo");
                query.Append("  from Niff_ADS_Competencias");
                query.Append(" Where idcompetencia = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    if (dadosReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dadosReader["idcompetencia"].ToString());
                        _tipo.Descricao = dadosReader["Descricao"].ToString();
                        _tipo.Ativo = dadosReader["Ativo"].ToString() == "S";
                        _tipo.Tipo = (dadosReader["Tipo"].ToString() == "T" ? Publicas.TipoCompetencias.Tecnica : Publicas.TipoCompetencias.Comportamental);
                        _tipo.DescricaoTipo = Publicas.GetDescription(_tipo.Tipo, "");
                        _tipo.TextoExplicativo = dadosReader["TextoExplicativo"].ToString();
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

                query.Append("Select Max(idcompetencia) +1 next From Niff_ADS_Competencias");
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

        public bool Gravar(Competencias tipo, List<SubCompetencias> _lista)
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
                    query.Append("Insert into Niff_ADS_Competencias");
                    query.Append(" ( idcompetencia, descricao, tipo, ativo, TextoExplicativo )");
                    query.Append(" Values (" + tipo.Id);
                    query.Append("        ,'" + tipo.Descricao + "'");
                    query.Append("        ,'" + (tipo.Tipo == Publicas.TipoCompetencias.Tecnica ? "T" : "C") + "'");
                    query.Append("        ,'" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("        ,'" + tipo.TextoExplicativo + "'");
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_ADS_Competencias");
                    query.Append("   set descricao = '" + tipo.Descricao + "'");
                    query.Append("     , ativo = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("     , tipo = '" + (tipo.Tipo == Publicas.TipoCompetencias.Tecnica ? "T" : "C") + "'");
                    query.Append("     , TextoExplicativo = '" + tipo.TextoExplicativo + "'");
                    query.Append(" Where idcompetencia = " + tipo.Id);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    retorno = new SubCompetenciasDAO().Gravar(_lista);
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

        public bool Excluir(Competencias tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            if (!new SubCompetenciasDAO().Excluir(tipo.Id))
                return false;
            else
            {
                try
                {
                    query.Append("Delete Niff_ADS_Competencias");
                    query.Append(" Where idcompetencia = " + tipo.Id);
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

}
