using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class SalaDeReuniaoDAO
    {
        IDataReader salaReader;

        public List<SalaDeReuniao> Listar()
        {
            Sessao sessao = new Sessao();
            StringBuilder query = new StringBuilder();
            List<SalaDeReuniao> _lista = new List<SalaDeReuniao>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IdSala ");
                query.Append("     , Descricao");
                query.Append("     , ativo");
                query.Append("     , capacidade");
                query.Append("     , e.IdEmpresa");
                query.Append("     , e.Nome");
                
                query.Append("  From Niff_Chm_SalaReuniao s ");
                query.Append("     , Niff_Chm_Empresas e");
                query.Append(" Where s.IdEmpresa = e.IdEmpresa");
                
                Query executar = sessao.CreateQuery(query.ToString());

                salaReader = executar.ExecuteQuery();

                using (salaReader)
                {
                    while (salaReader.Read())
                    {
                        SalaDeReuniao _tipo = new SalaDeReuniao();
                        _tipo.IdSala = Convert.ToInt32(salaReader["IdSala"].ToString());
                        _tipo.Descricao = salaReader["Descricao"].ToString();
                        _tipo.Ativo = salaReader["ativo"].ToString() == "S";
                        try
                        {
                            _tipo.Capacidade = Convert.ToInt32(salaReader["capacidade"].ToString());
                        }
                        catch { }
                        _tipo.IdEmpresa = Convert.ToInt32(salaReader["IdEmpresa"].ToString());
                        _tipo.Nome = salaReader["Nome"].ToString();

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

        public SalaDeReuniao Consultar(int codigo)
        {
            Sessao sessao = new Sessao();
            StringBuilder query = new StringBuilder();
            SalaDeReuniao _tipo = new SalaDeReuniao();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IdSala ");
                query.Append("     , Descricao");
                query.Append("     , ativo");
                query.Append("     , capacidade");
                query.Append("     , e.IdEmpresa");
                query.Append("     , e.Nome");

                query.Append("  From Niff_Chm_SalaReuniao s ");
                query.Append("     , Niff_Chm_Empresas e");
                query.Append(" Where s.IdEmpresa = e.IdEmpresa");
                query.Append("   and idSala = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                salaReader = executar.ExecuteQuery();

                using (salaReader)
                {
                    if (salaReader.Read())
                    {
                        _tipo.IdSala = Convert.ToInt32(salaReader["IdSala"].ToString());
                        _tipo.Descricao = salaReader["Descricao"].ToString();
                        _tipo.Ativo = salaReader["ativo"].ToString() == "S";
                        try
                        {
                            _tipo.Capacidade = Convert.ToInt32(salaReader["capacidade"].ToString());
                        }
                        catch { }
                        _tipo.IdEmpresa = Convert.ToInt32(salaReader["IdEmpresa"].ToString());
                        _tipo.Nome = salaReader["Nome"].ToString();

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

        public bool Grava(SalaDeReuniao tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!tipo.Existe)
                {
                    query.Clear();
                    query.Append("Insert into Niff_Chm_SalaReuniao");
                    query.Append("   (IDSALA, descricao, ativa, CAPACIDADE, IDEMPRESA) ");
                    query.Append("   Values (" + tipo.IdSala);
                    query.Append(", '" + tipo.Descricao + "'");
                    query.Append(", '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append(", " + tipo.Capacidade);
                    query.Append(", " + tipo.IdEmpresa);
                    query.Append(") ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Chm_SalaReuniao");
                    query.Append("   set descricao = '" + tipo.Descricao + "' ");
                    query.Append("     , ativa = '" + (tipo.Ativo ? "S" : "N") + "' ");
                    query.Append("     , capacidade = " + tipo.Capacidade);
                    query.Append("     , IdEmpresa = " + tipo.IdEmpresa);
                    query.Append(" Where IDSALA = " + tipo.IdSala);
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

        public bool Exclui(SalaDeReuniao tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (tipo.Existe)
                {
                    query.Append("Delete Niff_Chm_SalaReuniao");
                    query.Append(" Where IdSala = " + tipo.IdSala);
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

                query.Append("Select Max(IdSala) + 1 next From Niff_Chm_SalaReuniao");
                Query executar = sessao.CreateQuery(query.ToString());

                salaReader = executar.ExecuteQuery();

                using (salaReader)
                {
                    if (salaReader.Read())
                        retorno = Convert.ToInt32(salaReader["next"].ToString());
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
