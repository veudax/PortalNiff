using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class TipoDeAtendimentoEMTUDAO
    {
        IDataReader tipoReader;

        public List<TipoDeAtendimentoEMTU> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<TipoDeAtendimentoEMTU> _lista = new List<TipoDeAtendimentoEMTU>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select e.IdEmtu ");
                query.Append("     , e.Descricao");
                query.Append("     , e.Codigo");
                query.Append("     , e.Titulo");
                query.Append("     , e.IdDepartamento");
                query.Append("     , e.IdTpAtendimento");
                query.Append("     , e.Ativo");
                query.Append("     , t.descricao TipoAtendimento ");
                query.Append("     , d.descricao Departamento ");

                query.Append("  From NIFF_CHM_EMTUAtendimento e, niff_chm_tipoatendimento t, Niff_Chm_Departamento d ");
                query.Append(" Where t.Idtipoatendimento(+) = e.Idtpatendimento ");
                query.Append("   And d.Iddepartamento(+) = e.Iddepartamento ");

                Query executar = sessao.CreateQuery(query.ToString());

                tipoReader = executar.ExecuteQuery();

                using (tipoReader)
                {
                    while (tipoReader.Read())
                    {
                        TipoDeAtendimentoEMTU _tipo = new TipoDeAtendimentoEMTU();
                        _tipo.Id = Convert.ToInt32(tipoReader["IdEmtu"].ToString());

                        try
                        {
                            _tipo.IdDepartamento = Convert.ToInt32(tipoReader["IdDepartamento"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IdTipoAtendimento = Convert.ToInt32(tipoReader["IdTpAtendimento"].ToString());
                        }
                        catch { }

                        _tipo.Descricao = tipoReader["Descricao"].ToString();
                        _tipo.Titulo = tipoReader["Titulo"].ToString();
                        _tipo.Codigo = tipoReader["Codigo"].ToString();
                        _tipo.Ativo = tipoReader["Ativo"].ToString() == "S";

                        if (tipoReader["departamento"].ToString() != "")
                            _tipo.Departamento = tipoReader["Iddepartamento"].ToString() + " - " + tipoReader["departamento"].ToString();

                        if (tipoReader["TipoAtendimento"].ToString() != "")
                            _tipo.TipoAtendimento = tipoReader["Idtpatendimento"].ToString() + " - " + tipoReader["TipoAtendimento"].ToString();

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

        public TipoDeAtendimentoEMTU Consulta(string codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            TipoDeAtendimentoEMTU _tipo = new TipoDeAtendimentoEMTU();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select e.IdEmtu ");
                query.Append("     , e.Descricao");
                query.Append("     , e.Codigo");
                query.Append("     , e.Titulo");
                query.Append("     , e.IdDepartamento");
                query.Append("     , e.IdTpAtendimento");
                query.Append("     , e.Ativo");
                query.Append("     , t.descricao TipoAtendimento ");
                query.Append("     , d.descricao Departamento ");

                query.Append("  From NIFF_CHM_EMTUAtendimento e, niff_chm_tipoatendimento t, Niff_Chm_Departamento d ");
                query.Append(" Where t.Idtipoatendimento(+) = e.Idtpatendimento ");
                query.Append("   And d.Iddepartamento(+) = e.Iddepartamento ");

                query.Append("   AND Codigo = '" + codigo + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                tipoReader = executar.ExecuteQuery();

                using (tipoReader)
                {
                    if (tipoReader.Read())
                    {
                        _tipo.Id = Convert.ToInt32(tipoReader["IdEmtu"].ToString());

                        try
                        {
                            _tipo.IdDepartamento = Convert.ToInt32(tipoReader["IdDepartamento"].ToString());
                        }
                        catch { }
                        try
                        {
                            _tipo.IdTipoAtendimento = Convert.ToInt32(tipoReader["IdTpAtendimento"].ToString());
                        }
                        catch { }

                        _tipo.Descricao = tipoReader["Descricao"].ToString();
                        _tipo.Titulo = tipoReader["Titulo"].ToString();
                        _tipo.Codigo = tipoReader["Codigo"].ToString();
                        _tipo.Ativo = tipoReader["Ativo"].ToString() == "S";

                        if (tipoReader["departamento"].ToString() != "")
                            _tipo.Departamento = tipoReader["departamento"].ToString();

                        if (tipoReader["TipoAtendimento"].ToString() != "")
                            _tipo.TipoAtendimento = tipoReader["TipoAtendimento"].ToString();

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

        public TipoDeAtendimentoEMTU ConsultaPorId(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            TipoDeAtendimentoEMTU _tipo = new TipoDeAtendimentoEMTU();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select e.IdEmtu ");
                query.Append("     , e.Codigo");                
                query.Append("  From NIFF_CHM_EMTUAtendimento e");
                query.Append(" Where e.IdEmtu = " + codigo );

                Query executar = sessao.CreateQuery(query.ToString());

                tipoReader = executar.ExecuteQuery();

                using (tipoReader)
                {
                    if (tipoReader.Read())
                    {
                        return Consulta(tipoReader["Codigo"].ToString());
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

        public bool Grava(TipoDeAtendimentoEMTU tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!tipo.Existe)
                {
                    query.Clear();
                    query.Append("Insert into NIFF_CHM_EMTUAtendimento");
                    query.Append("   (IdEmtu, Codigo, Titulo, Descricao, ativo, IdDepartamento, IdTpAtendimento) ");
                    query.Append("   Values (" + ProximoId().ToString());
                    query.Append(", '" + tipo.Codigo + "'");
                    query.Append(", '" + tipo.Titulo + "'");
                    query.Append(", '" + tipo.Descricao + "'");
                    query.Append(", '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append(", " + tipo.IdDepartamento + ", " + tipo.IdTipoAtendimento + ")");
                }
                else
                {
                    query.Clear();
                    query.Append("Update NIFF_CHM_EMTUAtendimento");
                    query.Append("   set descricao = '" + tipo.Descricao + "', ");
                    query.Append("       Titulo = '" + tipo.Titulo + "', ");
                    query.Append("       codigo = '" + tipo.Codigo + "', ");
                    query.Append("       ativo = '" + (tipo.Ativo ? "S" : "N") + "', ");
                    query.Append("       IdDepartamento = " + tipo.IdDepartamento + ", ");
                    query.Append("       IdTpAtendimento = " + tipo.IdTipoAtendimento);
                    query.Append(" Where IdEmtu = " + tipo.Id);
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

        public bool Exclui(TipoDeAtendimentoEMTU tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (tipo.Id != 0)
                {
                    query.Append("Delete NIFF_CHM_EMTUAtendimento");
                    query.Append(" Where IdEmtu = " + tipo.Id);
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

        public int ProximoId()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Max(IdEmtu) + 1 next From NIFF_CHM_EMTUAtendimento");
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

        public string Proximo(string codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            string retorno = "";
            try
            {
                if (string.IsNullOrEmpty(codigo))
                {
                    query.Append("Select Max(codigo) + 1 next ");
                    query.Append("  From NIFF_CHM_EMTUAtendimento");
                }
                else
                {// calcula o proximo na faixa
                    query.Append("Select Max(To_number(SubStr(codigo,pos('.',codigo)+1,length(codigo)))) + 1 next ");
                    query.Append("  From NIFF_CHM_EMTUAtendimento");
                    query.Append(" Where codigo Like '" + codigo + "%'");
                }
                Query executar = sessao.CreateQuery(query.ToString());

                tipoReader = executar.ExecuteQuery();

                using (tipoReader)
                {
                    if (tipoReader.Read())
                    {
                        if (string.IsNullOrEmpty(codigo))
                            retorno = tipoReader["next"].ToString() + ".1";
                        else
                            retorno = codigo + tipoReader["next"].ToString();
                    }
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
