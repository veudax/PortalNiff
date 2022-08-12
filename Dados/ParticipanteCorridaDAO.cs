using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ParticipanteCorridaDAO
    {
        IDataReader dataReader;

        public List<ParticipanteCorrida> ListarCPF()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ParticipanteCorrida> _lista = new List<ParticipanteCorrida>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Distinct p.nome, p.cpf");
                query.Append("  from NIFF_CHM_Participantes p");
                query.Append(" Where cpf Is Not Null");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ParticipanteCorrida _tipo = new ParticipanteCorrida();

                        _tipo.Existe = true;

                        try
                        {
                            _tipo.CPF = Convert.ToDecimal(dataReader["cpf"].ToString());
                            _tipo.CPFFormatado = Convert.ToDecimal(dataReader["cpf"].ToString()).ToString("000.000.000-00");
                        }
                        catch { }

                        _tipo.Nome = dataReader["Nome"].ToString();

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

        public List<ParticipanteCorrida> Listar(int corrida)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<ParticipanteCorrida> _lista = new List<ParticipanteCorrida>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select p.idparticipante, p.iddistancia, p.idusuario, p.nome, p.inscricaopaga, p.tempobruto, p.tempoliquido, p.Visualizado");
                query.Append("     , p.ritmo, p.classificacaogeral, p.classificacao, p.cpf, p.inscricaoemgrupo, p.idusuariogrupo, d.Km, p.sexo, p.ValorInscrito, NumeroPeito");
                query.Append("  from NIFF_CHM_Participantes p");
                query.Append("     , niff_chm_distancias d");

                query.Append(" Where p.Iddistancia = d.IdDistancia");
                query.Append("   and d.Idcorrida = " + corrida);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        ParticipanteCorrida _tipo = new ParticipanteCorrida();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idparticipante"].ToString());
                        _tipo.IdDistancia = Convert.ToInt32(dataReader["iddistancia"].ToString());
                        try
                        {
                            _tipo.IdUsuario = Convert.ToInt32(dataReader["idusuario"].ToString());
                        }
                        catch
                        { }
                        try
                        {
                            _tipo.IdUsuarioGrupo = Convert.ToInt32(dataReader["idusuariogrupo"].ToString());
                        }
                        catch
                        { }

                        try
                        {
                            _tipo.CPF = Convert.ToDecimal(dataReader["cpf"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.KM = Convert.ToInt32(dataReader["km"].ToString());
                        }
                        catch
                        { }
                        _tipo.Nome = dataReader["Nome"].ToString();

                        try
                        {
                            _tipo.TempoBruto = Convert.ToInt32(dataReader["tempobruto"].ToString());
                        }
                        catch
                        { }

                        try
                        {
                            _tipo.TempoLiquido = Convert.ToInt32(dataReader["tempoliquido"].ToString());
                        }
                        catch
                        { }

                        try
                        {
                            _tipo.Ritmo = Convert.ToInt32(dataReader["ritmo"].ToString());
                        }
                        catch
                        { }
                        
                        try
                        {
                            _tipo.ClassificacaoGeral = Convert.ToInt32(dataReader["classificacaogeral"].ToString());
                        }
                        catch
                        { }

                        try
                        {
                            _tipo.Classificacao = Convert.ToInt32(dataReader["classificacao"].ToString());
                        }
                        catch
                        { }

                        _tipo.Sexo = dataReader["sexo"].ToString();
                        _tipo.InscricaoEmGrupo = dataReader["inscricaoemgrupo"].ToString() == "S";
                        _tipo.InscricaoPaga = dataReader["inscricaopaga"].ToString() == "S";
                        _tipo.Visualizado = dataReader["Visualizado"].ToString() == "S";
                        _tipo.RitmoFormatado = _tipo.Ritmo.ToString("00:00");
                        _tipo.TempoLiquidoFormatado = _tipo.TempoLiquido.ToString("00:00:00");
                        _tipo.TempoBrutoFormatado = _tipo.TempoBruto.ToString("00:00:00");

                        try
                        {
                            _tipo.NumeroDePeito = Convert.ToInt32(dataReader["NumeroPeito"].ToString());
                        }
                        catch
                        { }
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

        public ParticipanteCorrida Consulta(int codigo, int corrida, decimal cpf)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            ParticipanteCorrida _tipo = new ParticipanteCorrida();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select p.idparticipante, p.iddistancia, p.idusuario, p.nome, p.inscricaopaga, p.tempobruto, p.tempoliquido");
                query.Append("     , p.ritmo, p.classificacaogeral, p.classificacao, p.cpf, p.inscricaoemgrupo, p.idusuariogrupo, d.Km");
                query.Append("     , c.Data, p.sexo, p.Visualizado, p.ValorInscrito, p.NumeroPeito");
                query.Append("  from NIFF_CHM_Participantes p");
                query.Append("     , niff_chm_distancias d");
                query.Append("     , niff_chm_Corridas c");

                query.Append(" Where p.Iddistancia = d.IdDistancia");
                query.Append("   and c.idCorrida = d.IdCorrida");
                query.Append("   and c.Ativo = 'S'");
                if (corrida != 0)
                    query.Append("   and d.Idcorrida = " + corrida );

                if (codigo != 0)
                    query.Append("   and p.idusuario = " + codigo);

                if (cpf != 0)
                    query.Append("   and p.cpf = " + cpf);


                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["idparticipante"].ToString());
                        _tipo.IdDistancia = Convert.ToInt32(dataReader["iddistancia"].ToString());
                        try
                        {
                            _tipo.IdUsuario = Convert.ToInt32(dataReader["idusuario"].ToString());
                        }
                        catch
                        { }
                        try
                        {
                            _tipo.IdUsuarioGrupo = Convert.ToInt32(dataReader["idusuariogrupo"].ToString());
                        }
                        catch
                        { }
                        try
                        {
                            _tipo.DataCorrida = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch
                        { }

                        try
                        {
                            _tipo.KM = Convert.ToInt32(dataReader["km"].ToString());
                        }
                        catch
                        { }
                        try
                        {
                            _tipo.CPF = Convert.ToDecimal(dataReader["cpf"].ToString());
                        }
                        catch { }

                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Sexo = dataReader["sexo"].ToString();

                        try
                        {
                            _tipo.TempoBruto = Convert.ToInt32(dataReader["tempobruto"].ToString());
                        }
                        catch
                        { }

                        try
                        {
                            _tipo.TempoLiquido = Convert.ToInt32(dataReader["tempoliquido"].ToString());
                        }
                        catch
                        { }

                        try
                        {
                            _tipo.Ritmo = Convert.ToInt32(dataReader["ritmo"].ToString());
                        }
                        catch
                        { }

                        try
                        {
                            _tipo.ClassificacaoGeral = Convert.ToInt32(dataReader["classificacaogeral"].ToString());
                        }
                        catch
                        { }

                        try
                        {
                            _tipo.Classificacao = Convert.ToInt32(dataReader["classificacao"].ToString());
                        }
                        catch
                        { }

                        _tipo.InscricaoEmGrupo = dataReader["inscricaoemgrupo"].ToString() == "S";
                        _tipo.InscricaoPaga = dataReader["inscricaopaga"].ToString() == "S";
                        _tipo.Visualizado = dataReader["Visualizado"].ToString() == "S";
                        _tipo.RitmoFormatado = _tipo.Ritmo.ToString("00:00");
                        _tipo.TempoLiquidoFormatado = _tipo.TempoLiquido.ToString("00:00:00");
                        _tipo.TempoBrutoFormatado = _tipo.TempoBruto.ToString("00:00:00");
                        try
                        {
                            _tipo.NumeroDePeito = Convert.ToInt32(dataReader["NumeroPeito"].ToString());
                        }
                        catch
                        { }

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
        
        public bool Gravar(ParticipanteCorrida tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                if (!tipo.Existe)
                {
                    query.Append("Insert into NIFF_CHM_Participantes");
                    query.Append(" ( idparticipante, iddistancia ");

                    if (tipo.IdUsuario != 0)
                        query.Append(" , idusuario");

                    query.Append(" , nome");

                    if (tipo.IdUsuario == 0)
                        query.Append(" , cpf");

                    query.Append(" , inscricaopaga, inscricaoemgrupo, sexo, ValorInscrito");

                    if (tipo.IdUsuarioGrupo != 0)
                        query.Append(" , idusuariogrupo ");

                    query.Append(") Values (SQ_NIFF_CHMIdParticipa.nextVal");
                    query.Append("        , " + tipo.IdDistancia);

                    if (tipo.IdUsuario != 0)
                        query.Append("        , " + tipo.IdUsuario);

                    query.Append("        ,'" + tipo.Nome + "'");

                    if (tipo.IdUsuario == 0)
                        query.Append("        , " + tipo.CPF.ToString());

                    query.Append("        ,'" + (tipo.InscricaoPaga ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.InscricaoEmGrupo ? "S" : "N") + "'");
                    query.Append("        ,'" + tipo.Sexo + "'");

                    query.Append("        , " + tipo.ValorInscrito.ToString().Replace(".", "").Replace(",", "."));

                    if (tipo.IdUsuarioGrupo != 0)
                        query.Append("        , " + tipo.IdUsuarioGrupo);
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update NIFF_CHM_Participantes");

                    query.Append("   Set Nome = '" + tipo.Nome + "'");

                    if (tipo.IdUsuario != 0)
                        query.Append("     , IdUsuario = " + tipo.IdUsuario);

                    if (tipo.IdUsuarioGrupo != 0)
                        query.Append("     , idusuariogrupo = " + tipo.IdUsuarioGrupo);

                    if (tipo.IdUsuario == 0)
                        query.Append("     , CPF = " + tipo.CPF.ToString());

                    query.Append("     , inscricaopaga = '" + (tipo.InscricaoPaga ? "S" : "N") + "'");
                    query.Append("     , inscricaoemgrupo = '" + (tipo.InscricaoEmGrupo ? "S" : "N") + "'");

                    query.Append("     , classificacao = " + tipo.Classificacao.ToString());
                    query.Append("     , ClassificacaoGeral = " + tipo.ClassificacaoGeral.ToString());
                    query.Append("     , Ritmo = " + tipo.Ritmo.ToString());
                    query.Append("     , TempoBruto = " + tipo.TempoBruto.ToString());
                    query.Append("     , TempoLiquido = " + tipo.TempoLiquido.ToString());
                    query.Append("     , Sexo = '" + tipo.Sexo + "'");

                    query.Append("     , ValorInscrito = " + tipo.ValorInscrito.ToString().Replace(".", "").Replace(",", "."));
                    query.Append("     , NumeroPeito = " + tipo.NumeroDePeito);

                    query.Append(" Where idparticipante = " + tipo.Id);
                    query.Append("   and iddistancia = " + tipo.IdDistancia);
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

        public bool Excluir(ParticipanteCorrida tipo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!new DistanciaCorridaDAO().Excluir(tipo.Id))
                    return false;

                query.Append("Delete NIFF_CHM_Participantes");
                query.Append(" Where idparticipante = " + tipo.Id);
                query.Append("   and iddistancia = " + tipo.IdDistancia);
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

        public bool ResultadoVisualizado(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Update NIFF_CHM_Participantes");
                query.Append("   set Visualizado = 'S'");
                query.Append(" Where idparticipante = " + codigo);
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

        public bool AtualizaValorInscricaoCapitao(int usuario, int corrida, decimal valor)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Update NIFF_CHM_Participantes");
                query.Append("   set ValorInscrito = " + valor.ToString().Replace(".", "").Replace(",", "."));
                query.Append(" Where idUsuario = " + usuario);
                query.Append("   and IdDistancia = " + corrida);
                
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
