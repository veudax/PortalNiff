using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class AnexoDoHistoricoDAO
    {
        IDataReader anexosReader;

        public List<AnexoDoHistorico> Listar(int IdChamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<AnexoDoHistorico> _lista = new List<AnexoDoHistorico>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select a.idhistorico, a.idanexo, a.anexo, a.NomeArquivo");
                query.Append("  from niff_chm_anexoshistorico a       ");
                query.Append("     , Niff_Chm_Histochamado h          ");
                query.Append(" Where h.Idhistorico = a.Idhistorico    ");
                query.Append("   And h.Idchamado = " + IdChamado);
                Query executar = sessao.CreateQuery(query.ToString());

                anexosReader = executar.ExecuteQuery();

                using (anexosReader)
                {
                    while (anexosReader.Read())
                    {
                        AnexoDoHistorico _anexo = new AnexoDoHistorico();
                        _anexo.IdAnexo = Convert.ToInt32(anexosReader["idanexo"].ToString());
                        _anexo.IdHistorico = Convert.ToInt32(anexosReader["idhistorico"].ToString());
                        _anexo.NomeArquivo = anexosReader["NomeArquivo"].ToString();
                        try
                        {
                            _anexo.Anexo = (byte[])(anexosReader["anexo"]);
                        }
                        catch { }

                        _lista.Add(_anexo);
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

        public bool Grava(List<AnexoDoHistorico> anexos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            OracleParameter parametro = new OracleParameter();
            List<OracleParameter> parametros = new List<OracleParameter>();
            bool retorno = true;

            foreach (AnexoDoHistorico item in anexos)
            {
                query.Clear();
                query.Append("Insert into niff_chm_anexoshistorico ");
                query.Append(" (idanexo, idhistorico, anexo, NomeArquivo)       ");
                query.Append(" Values( SQ_NIFF_IDAnexo.NextVal     ");
                query.Append("       , " + item.IdHistorico         );

                query.Append(", :panexo ");
                parametros.Clear();
                parametro.ParameterName = ":panexo";
                parametro.Value = item.Anexo;
                parametro.OracleType = OracleType.Blob;
                parametros.Add(parametro);

                query.Append(", '" + item.NomeArquivo + "')");

                try
                {
                    retorno = sessao.ExecuteSqlTransaction(query.ToString(), parametros.ToArray());
                    if (!retorno)
                    {
                        query.Clear();
                        query.Append("Delete niff_chm_anexoshistorico ");
                        query.Append(" Where idhistorico = " + item.IdHistorico);

                        sessao.ExecuteSqlTransaction(query.ToString());

                        query.Clear();
                        query.Append("Delete niff_chm_histochamado ");
                        query.Append(" Where idhistorico = " + item.IdHistorico);

                        sessao.ExecuteSqlTransaction(query.ToString());
                        break;
                    }
                }
                catch (Exception ex)
                {
                    // deu erro apaga os que foram gravados
                    query.Clear();
                    query.Append("Delete niff_chm_anexoshistorico ");
                    query.Append(" Where idhistorico = " + item.IdHistorico);

                    sessao.ExecuteSqlTransaction(query.ToString());

                    query.Clear();
                    query.Append("Delete niff_chm_histochamado ");
                    query.Append(" Where idhistorico = " + item.IdHistorico);

                    sessao.ExecuteSqlTransaction(query.ToString());
                    Publicas.mensagemDeErro = ex.Message;
                    retorno = false;
                    break;
                }
                
            }
            sessao.Desconectar();
            return retorno;
        }

    }
}
