using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class AniversarioDAO
    {
        IDataReader aniversarioReader;

        public Aniversarios Consulta()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Aniversarios _aniversario = new Aniversarios();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select a.idaniversario, a.idusuario, a.data, a.mostrarmensagem, a.mensagem, u.nome");
                query.Append("  From NIFF_CHM_Aniversario a, Niff_Chm_Usuarios u ");
                query.Append(" Where a.idusuario = " + Publicas._idUsuario.ToString());
                query.Append("   And a.idusuario = u.Idusuario");
                query.Append("   And trunc(a.Data) = trunc(Sysdate)");

                Query executar = sessao.CreateQuery(query.ToString());

                aniversarioReader = executar.ExecuteQuery();

                using (aniversarioReader)
                {
                    if (aniversarioReader.Read())
                    {
                        _aniversario.IdAniversario = Convert.ToInt32(aniversarioReader["idaniversario"].ToString());
                        _aniversario.Id = Convert.ToInt32(aniversarioReader["idusuario"].ToString());

                        _aniversario.Nome = aniversarioReader["Nome"].ToString();
                        _aniversario.Mensagem = aniversarioReader["mensagem"].ToString();
                        _aniversario.MostrarMensagem = aniversarioReader["mostrarmensagem"].ToString() == "S";

                        _aniversario.Data = Convert.ToDateTime(aniversarioReader["Data"].ToString());
                        _aniversario.Existe = true;
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
            return _aniversario;
        }

        public bool Grava(Aniversarios aniversario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!aniversario.Existe)
                {
                    query.Clear();
                    query.Append("Insert into NIFF_CHM_Aniversario");
                    query.Append("   (idaniversario, idusuario, data, mostrarmensagem, mensagem) ");
                    query.Append("   Values ( SQ_NIFF_IdAniversario.NextVal" );
                    query.Append(", " + aniversario.Id);
                    query.Append(", sysdate ");
                    query.Append(", '" + (aniversario.MostrarMensagem ? "S" : "N") + "'");
                    query.Append(", '" + aniversario.Mensagem + "')");
                }
                else
                {
                    query.Clear();
                    query.Append("Update NIFF_CHM_Aniversario");
                    query.Append("   set mostrarmensagem = '" + (aniversario.MostrarMensagem ? "S" : "N") + "' ");
                    query.Append(" Where idaniversario = " + aniversario.IdAniversario);
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

    }
}
