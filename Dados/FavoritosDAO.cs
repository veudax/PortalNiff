using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class FavoritosDAO
    {
        IDataReader dataReader;

        public List<Favoritos> Listar()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Favoritos> _lista = new List<Favoritos>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idusuario, nomemenu, namemenu, datafavoritou, MenuPai");                
                query.Append("  from NIFF_Chm_Favoritos ");
                query.Append(" Where idUsuario = " + Publicas._idUsuario);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Favoritos _tipo = new Favoritos();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());

                        _tipo.IdUsuario = Convert.ToInt32(dataReader["idUsuario"].ToString());
                        _tipo.NomeMenu = dataReader["nomemenu"].ToString();
                        _tipo.Name = dataReader["namemenu"].ToString();
                        _tipo.MenuAnterior = dataReader["MenuPai"].ToString();

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["datafavoritou"].ToString());
                        }
                        catch { }

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

        public bool Grava(Favoritos times)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                
                query.Clear();
                query.Append("Insert into NIFF_Chm_Favoritos");
                query.Append("   (id, idusuario, nomemenu, namemenu, datafavoritou, MenuPai )");

                query.Append("  Values ( (Select nvl(Max(Id),1)+1 From NIFF_Chm_Favoritos ) ");
                query.Append(", " + Publicas._idUsuario);
                query.Append(", '" + times.NomeMenu + "'");
                query.Append(", '" + times.Name + "'");
                query.Append(", Sysdate");
                query.Append(", '" + times.MenuAnterior + "'");
                query.Append(") ");
                

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

        public bool Exclui(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete NIFF_Chm_Favoritos");
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
    }
}
