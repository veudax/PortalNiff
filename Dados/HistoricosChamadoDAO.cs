using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class HistoricosChamadoDAO
    {
        IDataReader chamadoReader;
        IDataReader chamadoReader2;

        public List<HistoricoDoChamado> RetornaUsuariosDoChamado(int idChamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            
            List<HistoricoDoChamado> _lista = new List<HistoricoDoChamado>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Distinct u.tipo, u.Idusuario                  ");
                query.Append("  From niff_chm_histochamado h, Niff_Chm_Usuarios u  ");
                query.Append(" Where h.Idchamado = " + idChamado);
                query.Append("   And h.idusuario = u.idusuario                     ");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    while (chamadoReader.Read())
                    {
                        HistoricoDoChamado _atendimento = new HistoricoDoChamado();
                        _atendimento.Existe = true;
                        _atendimento.IdUsuario = Convert.ToInt32(chamadoReader["Idusuario"].ToString());
                        _atendimento.TipoUsuario = chamadoReader["Tipo"].ToString();
                        _lista.Add(_atendimento);
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

        public HistoricoDoChamado RetornaMaiorDataDeRetorno(int IdChamado, bool soAnalista, int IdSolicitante)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            HistoricoDoChamado _atendimento = new HistoricoDoChamado();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Max(h.data) DataRetorno, u.nome, h.Idhistorico, h.IdUsuario, u.PodeFinalizarChamado ");
                query.Append("  From niff_chm_histochamado h, Niff_Chm_Usuarios u  ");
                query.Append(" Where h.Idchamado = " + IdChamado                    );
                query.Append("   And h.idusuario = u.idusuario                     ");
                if (soAnalista)
                {
                    query.Append("   And ((u.tipo In('A','T') and h.TipoUsuario is null) or h.TipoUsuario = 'A')");// Apenas os historicos de atendentes
                    query.Append("   And h.IdUsuario <> " + IdSolicitante);
                }
                query.Append("   And h.Privado = 'N'                               ");
                //query.Append("   And h.Autorizado = 'N'                               ");
                query.Append("   And H.Idusuario Not In (Select idUsuario");
                query.Append("                             From Niff_Chm_Histochamado");
                query.Append("                            Where IdChamado = " + IdChamado);
                query.Append("                              And Autorizado = 'S')");

                query.Append(" Group By u.Nome, h.Idhistorico, h.IdUsuario, u.PodeFinalizarChamado       ");
                query.Append(" Order By  DataRetorno Desc                          ");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    if (chamadoReader.Read())
                    {
                        _atendimento.Existe = true;
                        _atendimento.Data = Convert.ToDateTime(chamadoReader["DataRetorno"].ToString());
                        _atendimento.Nome = chamadoReader["Nome"].ToString();
                        _atendimento.IdHistorico = Convert.ToInt32(chamadoReader["Idhistorico"].ToString());
                        _atendimento.IdUsuario = Convert.ToInt32(chamadoReader["IdUsuario"].ToString()); 
                        _atendimento.PodeEncerrar = chamadoReader["PodeFinalizarChamado"].ToString() == "S";                        
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
            return _atendimento;
        }

        public string TrazoUltimoSolicitante(int IdChamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            HistoricoDoChamado _atendimento = new HistoricoDoChamado();
            Publicas.mensagemDeErro = string.Empty;
            string nome = ""; 
            try
            {
                query.Append("Select Max(h.data) DataRetorno, h.Idhistorico, h.IdUsuario, u.PodeFinalizarChamado ");
                query.Append("     , SUBSTR(u.nome, 1, INSTR(u.nome, ' ') - 1) || Substr(u.nome, INSTR(u.nome, ' ', -1)) nome");
                query.Append("  From niff_chm_histochamado h, Niff_Chm_Usuarios u  ");
                query.Append(" Where h.Idchamado = " + IdChamado);
                query.Append("   And h.idusuario = u.idusuario                     ");
                query.Append("   And ((u.tipo = 'S' and h.TipoUsuario is null) or h.TipoUsuario = 'S')");// Apenas os historicos de atendentes
                query.Append(" Group By u.Nome, h.Idhistorico, h.IdUsuario, u.PodeFinalizarChamado       ");
                query.Append(" Order By  DataRetorno Desc                          ");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    if (chamadoReader.Read())
                    {
                        nome = chamadoReader["Nome"].ToString();
                        
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
            return nome;
        }

        public string TrazoPrimeiroAtendete(int IdChamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            HistoricoDoChamado _atendimento = new HistoricoDoChamado();
            Publicas.mensagemDeErro = string.Empty;
            string nome = "";
            try
            {
                query.Append("Select Max(h.data) DataRetorno, h.Idhistorico, h.IdUsuario, u.PodeFinalizarChamado ");
                query.Append("     , SUBSTR(u.nome, 1, INSTR(u.nome, ' ') - 1) || Substr(u.nome, INSTR(u.nome, ' ', -1)) nome");
                query.Append("  From niff_chm_histochamado h, Niff_Chm_Usuarios u  ");
                query.Append(" Where h.Idchamado = " + IdChamado);
                query.Append("   And h.idusuario = u.idusuario                     ");
                query.Append("   And h.Privado = 'N'                               ");
                query.Append("   And ((u.tipo = 'A' and h.TipoUsuario is null) or h.TipoUsuario = 'A')");// Apenas os historicos de atendentes
                query.Append(" Group By u.Nome, h.Idhistorico, h.IdUsuario, u.PodeFinalizarChamado       ");
                query.Append(" Order By  DataRetorno                           ");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    if (chamadoReader.Read())
                    {
                        nome = chamadoReader["Nome"].ToString();

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
            return nome;
        }

        public HistoricoDoChamado RetornaDescricaodaMenorData(int IdChamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            HistoricoDoChamado _atendimento = new HistoricoDoChamado();

            try
            {

                query.Append("Select h.descricao ");
                query.Append("  From niff_chm_histochamado h");
                query.Append(" Where h.Idchamado = " + IdChamado);
                query.Append("   and Data = (Select Min(Data) From Niff_Chm_Histochamado Where idchamado  = " + IdChamado + ")");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader2 = executar.ExecuteQuery();

                using (chamadoReader2)
                {
                    if (chamadoReader2.Read())
                    {
                        _atendimento = new HistoricoDoChamado();

                        _atendimento.Descricao = chamadoReader2["DESCRICAO"].ToString();
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
            return _atendimento;
        }

        public string RetornaDescricaodaMaiorData(int IdChamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            string _descricao = "";
            try
            {

                query.Append("Select h.descricao ");
                query.Append("  From niff_chm_histochamado h");
                query.Append(" Where h.Idchamado = " + IdChamado);
                query.Append("   and Data = (Select Max(Data) From Niff_Chm_Histochamado Where idchamado  = " + IdChamado + ")");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader2 = executar.ExecuteQuery();

                using (chamadoReader2)
                {
                    if (chamadoReader2.Read())
                    {

                        _descricao = chamadoReader2["DESCRICAO"].ToString();
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
            return _descricao;
        }

        public bool RetornaSeUltimoTramiteFoiPrivado(int IdChamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool _privado = false;
            try
            {

                query.Append("Select h.privado ");
                query.Append("  From niff_chm_histochamado h");
                query.Append(" Where h.Idchamado = " + IdChamado);
                query.Append("   and Data = (Select Max(Data) From Niff_Chm_Histochamado Where idchamado  = " + IdChamado + ")");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader2 = executar.ExecuteQuery();

                using (chamadoReader2)
                {
                    if (chamadoReader2.Read())
                    {

                        _privado = chamadoReader2["privado"].ToString() == "S";
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
            return _privado;
        }

        public List<HistoricoDoChamado> Listar(int IdChamado, bool trazerUltimoTramite = false, bool naoPrivados = false)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<HistoricoDoChamado> _lista = new List<HistoricoDoChamado>();

            List<HistoricoDoChamado> _listaUsuarios = RetornaUsuariosDoChamado(IdChamado);

            Publicas.mensagemDeErro = string.Empty;
            int IdUsuarioAbertura = 0;
            try
            {
                query.Append("Select h.data, u.Nome, h.IDHISTORICO, h.IDUSUARIO, h.DESCRICAO, h.Status, u.tipo, h.Privado, c.numero, 'N' Associado, 'N' Agrupado, c.IdUsuario IdUsuarioAbertura ");
                query.Append("     , h.Adequacao, h.prazo, h.IDUSUARIOAUTORIZACAO, h.MOTIVOAUTORIZACAO, h.AUTORIZADO, h.AGUARDARRETORNOAUTO, h.TipoUsuario");
                query.Append("  From niff_chm_histochamado h, Niff_Chm_Usuarios u, NIFF_CHM_CHAMADO C");
                query.Append(" Where h.Idchamado = " + IdChamado);
                query.Append("   And h.idusuario = u.idusuario                   ");
                query.Append("   And h.Idchamado = C.IDCHAMADO                   ");
                if (naoPrivados)
                    query.Append("   And h.Privado = 'N'                             ");
                if (trazerUltimoTramite)
                {
                    query.Append("   And u.idusuario <> " + Publicas._idUsuario);
                    if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
                        query.Append("   And h.Status in ('N','E')");
                }
                query.Append("Union All ");
                query.Append("Select h.data, u.Nome, h.IDHISTORICO, h.IDUSUARIO, h.DESCRICAO, h.Status, u.tipo, h.Privado, c.numero, 'N' Associado, 'S' Agrupado, c.IdUsuario IdUsuarioAbertura ");
                query.Append("     , h.Adequacao, h.prazo, h.IDUSUARIOAUTORIZACAO, h.MOTIVOAUTORIZACAO, h.AUTORIZADO, h.AGUARDARRETORNOAUTO, h.TipoUsuario");
                query.Append("  From niff_chm_histochamado h, Niff_Chm_Usuarios u, NIFF_CHM_CHAMADO C");
                query.Append(" Where c.Idchamadoagrupado = " + IdChamado);
                query.Append("   And h.idusuario = u.idusuario                   ");
                query.Append("   And h.Idchamado = C.IDCHAMADO                   ");
                if (naoPrivados)
                    query.Append("   And h.Privado = 'N'                             ");
                if (trazerUltimoTramite)
                {
                    query.Append("   And u.idusuario <> " + Publicas._idUsuario);
                    if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
                        query.Append("   And h.Status in ('N','E')");
                }

                query.Append("Union All ");
                query.Append("Select h.data, u.Nome, h.IDHISTORICO, h.IDUSUARIO, h.DESCRICAO, h.Status, u.tipo, h.Privado, c.numero, 'S' Associado, 'N' Agrupado, c.IdUsuario IdUsuarioAbertura ");
                query.Append("     , h.Adequacao, h.prazo, h.IDUSUARIOAUTORIZACAO, h.MOTIVOAUTORIZACAO, h.AUTORIZADO, h.AGUARDARRETORNOAUTO, h.TipoUsuario");
                query.Append("  From niff_chm_histochamado h, Niff_Chm_Usuarios u, NIFF_CHM_CHAMADO C");
                query.Append(" Where h.Idchamado = (select IdChamadoAssociado From Niff_Chm_Chamado c Where idChamado = " + IdChamado + ")");
                query.Append("   And h.idusuario = u.idusuario                   ");
                query.Append("   And h.Idchamado = C.IDCHAMADO                   ");
                if (naoPrivados)
                    query.Append("   And h.Privado = 'N'                             ");
                if (trazerUltimoTramite)
                {
                    query.Append("   And u.idusuario <> " + Publicas._idUsuario);
                    if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
                        query.Append("   And h.Status in ('N','E')");
                }
                query.Append(" Order By  Data Desc                        ");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    while (chamadoReader.Read())
                    {
                        HistoricoDoChamado _atendimento = new HistoricoDoChamado();
                        _atendimento.Existe = true;
                        _atendimento.Data = Convert.ToDateTime(chamadoReader["Data"].ToString());
                        _atendimento.Nome = chamadoReader["Nome"].ToString();
                        _atendimento.Descricao = chamadoReader["DESCRICAO"].ToString();
                        _atendimento.Tipo = chamadoReader["tipo"].ToString();
                        _atendimento.Privado = chamadoReader["Privado"].ToString() == "S";
                        _atendimento.Associado = chamadoReader["Associado"].ToString() == "S";
                        _atendimento.Agrupado = chamadoReader["Agrupado"].ToString() == "S";
                        _atendimento.Adequacao = chamadoReader["Adequacao"].ToString();
                        _atendimento.Usuario = chamadoReader["TipoUsuario"].ToString();

                        try
                        {
                            _atendimento.Prazo = Convert.ToInt32(chamadoReader["Prazo"].ToString());
                        }
                        catch { }

                        _atendimento.NumeroChamado = chamadoReader["Numero"].ToString();

                        _atendimento.IdHistorico = Convert.ToInt32(chamadoReader["IDHISTORICO"].ToString());
                        _atendimento.IdUsuario = Convert.ToInt32(chamadoReader["IDUSUARIO"].ToString());
                        IdUsuarioAbertura = Convert.ToInt32(chamadoReader["IDUSUARIO"].ToString());

                        NotificacaoChamado _not = null;

                        if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
                        {
                            if (_atendimento.IdUsuario != Publicas._usuario.Id)
                                _atendimento.Lido = true;
                            else
                            { // Verificar se o Solicitante leu a resposta do Atendente
                                foreach (var item in _listaUsuarios.Where(w => w.IdUsuario != _atendimento.IdUsuario && w.TipoUsuario == "S"))
                                {
                                    _not = new NotificacaoChamadoDAO().Consultar(item.IdUsuario, _atendimento.IdHistorico);
                                    _atendimento.Lido = _atendimento.Lido || _not.Existe;
                                }                                
                            }
                        }
                        else
                        {
                            if (_atendimento.IdUsuario != Publicas._usuario.Id)
                                _atendimento.Lido = true;
                            else
                            { // Verificar se o Atendente leu a resposta do Solicitante
                                foreach (var item in _listaUsuarios.Where(w => w.IdUsuario != _atendimento.IdUsuario && w.TipoUsuario == "A"))
                                {
                                    _not = new NotificacaoChamadoDAO().Consultar(item.IdUsuario, _atendimento.IdHistorico);
                                    _atendimento.Lido = _atendimento.Lido || _not.Existe;
                                }
                            }
                        }

                        switch (chamadoReader["Status"].ToString())
                        {
                            case "A":
                                _atendimento.Status = Publicas.StatusChamado.Adequacao;
                                break;
                            case "N":
                                _atendimento.Status = Publicas.StatusChamado.Novo;
                                break;
                            case "P":
                                _atendimento.Status = Publicas.StatusChamado.Pendente;
                                break;
                            case "R":
                                _atendimento.Status = Publicas.StatusChamado.Reaberto;
                                break;
                            case "C":
                                _atendimento.Status = Publicas.StatusChamado.Cancelado;
                                break;
                            case "E":
                                _atendimento.Status = Publicas.StatusChamado.EmAndamento;
                                break;
                            case "F":
                                _atendimento.Status = Publicas.StatusChamado.Finalizado;
                                break;
                            case "D":
                                _atendimento.Status = Publicas.StatusChamado.EmDesenvolvimento;
                                break;
                            case "U":
                                _atendimento.Status = Publicas.StatusChamado.AguardandoAutorizacao;
                                break;
                            case "S":
                                _atendimento.Status = Publicas.StatusChamado.AguardandoConserto;
                                break;
                            case "G":
                                _atendimento.Status = Publicas.StatusChamado.AguardandoCronograma;
                                break;
                        }

                        try
                        {
                            _atendimento.IdUsuarioAutorizacao = Convert.ToInt32(chamadoReader["IDUSUARIOAUTORIZACAO"].ToString());
                        }
                        catch { }
                        _lista.Add(_atendimento);
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

        public HistoricoDoChamado Consultar(int IdHistorico)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            HistoricoDoChamado _atendimento = new HistoricoDoChamado();

            Publicas.mensagemDeErro = string.Empty;
            int IdUsuarioAbertura = 0;
            try
            {
                query.Append("Select h.data, u.Nome, h.IDHISTORICO, h.IDUSUARIO, h.DESCRICAO, h.Status, u.tipo, h.Privado, c.numero, 'N' Associado, 'N' Agrupado, c.IdUsuario IdUsuarioAbertura, h.TipoUsuario ");
                query.Append("  From niff_chm_histochamado h, Niff_Chm_Usuarios u, NIFF_CHM_CHAMADO C");
                query.Append(" Where h.IDHISTORICO = " + IdHistorico);
                query.Append("   And h.idusuario = u.idusuario                   ");
                query.Append("   And h.Idchamado = C.IDCHAMADO                   ");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    if (chamadoReader.Read())
                    {
                        _atendimento.Existe = true;
                        _atendimento.Data = Convert.ToDateTime(chamadoReader["Data"].ToString());
                        _atendimento.Nome = chamadoReader["Nome"].ToString();
                        _atendimento.Descricao = chamadoReader["DESCRICAO"].ToString();
                        _atendimento.Tipo = chamadoReader["tipo"].ToString();
                        _atendimento.Privado = chamadoReader["Privado"].ToString() == "S";
                        _atendimento.Associado = chamadoReader["Associado"].ToString() == "S";
                        _atendimento.Agrupado = chamadoReader["Agrupado"].ToString() == "S";
                        _atendimento.Usuario = chamadoReader["TipoUsuario"].ToString();
                        _atendimento.NumeroChamado = chamadoReader["Numero"].ToString();

                        _atendimento.IdHistorico = Convert.ToInt32(chamadoReader["IDHISTORICO"].ToString());
                        _atendimento.IdUsuario = Convert.ToInt32(chamadoReader["IDUSUARIO"].ToString());
                        IdUsuarioAbertura = Convert.ToInt32(chamadoReader["IDUSUARIO"].ToString());

                        switch (chamadoReader["Status"].ToString())
                        {
                            case "A":
                                _atendimento.Status = Publicas.StatusChamado.Adequacao;
                                break;
                            case "N":
                                _atendimento.Status = Publicas.StatusChamado.Novo;
                                break;
                            case "P":
                                _atendimento.Status = Publicas.StatusChamado.Pendente;
                                break;
                            case "R":
                                _atendimento.Status = Publicas.StatusChamado.Reaberto;
                                break;
                            case "C":
                                _atendimento.Status = Publicas.StatusChamado.Cancelado;
                                break;
                            case "E":
                                _atendimento.Status = Publicas.StatusChamado.EmAndamento;
                                break;
                            case "F":
                                _atendimento.Status = Publicas.StatusChamado.Finalizado;
                                break;
                            case "D":
                                _atendimento.Status = Publicas.StatusChamado.EmDesenvolvimento;
                                break;
                            case "U":
                                _atendimento.Status = Publicas.StatusChamado.AguardandoAutorizacao;
                                break;
                            case "S":
                                _atendimento.Status = Publicas.StatusChamado.AguardandoConserto;
                                break;
                            case "G":
                                _atendimento.Status = Publicas.StatusChamado.AguardandoCronograma;
                                break;
                        }
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
            return _atendimento;
        }

        public bool Grava(HistoricoDoChamado chamado, List<AnexoDoHistorico> anexos, DateTime sla)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int _idHistorico = 0;
            bool retorno = true;

            try
            {
                if (!chamado.Existe)
                {
                    query.Clear();
                    query.Append("(select SQ_NIFF_IDHistorico.Nextval next from dual)");
                    Query executar = sessao.CreateQuery(query.ToString());

                    chamadoReader = executar.ExecuteQuery();

                    using (chamadoReader)
                    {
                        if (chamadoReader.Read())
                        {
                            _idHistorico = Convert.ToInt32(chamadoReader["next"].ToString());
                        }
                    }

                    query.Clear();
                    query.Append("Insert into niff_chm_histochamado");
                    query.Append("   ( IdChamado, ");
                    query.Append("   IdHistorico, ");
                    query.Append("   idusuario,");
                    query.Append("   data,");
                    query.Append("   descricao, Status, Privado, adequacao, prazo ");

                    if (chamado.IdUsuarioAutorizacao != 0)
                        query.Append("   , idusuarioAutorizacao, Autorizado, AguardarRetornoAuto");

                    if (sla != DateTime.MinValue)
                        query.Append("   , Sla");

                    query.Append("   , RespondeuAutorizacao ");
                    query.Append("   , TipoUsuario ");

                    query.Append("  ) Values ( " + chamado.IdChamado);
                    query.Append(", " + _idHistorico.ToString());
                    query.Append(", " + chamado.IdUsuario.ToString());
                    query.Append(", sysdate ");
                    query.Append(", '" + chamado.Descricao.Trim().Replace("'","") + "'");

                    query.Append(", '" + (chamado.Status == Publicas.StatusChamado.Adequacao ? "A" :
                                           (chamado.Status == Publicas.StatusChamado.Cancelado ? "C" :
                                           (chamado.Status == Publicas.StatusChamado.EmAndamento ? "E" :
                                           (chamado.Status == Publicas.StatusChamado.Finalizado ? "F" :
                                           (chamado.Status == Publicas.StatusChamado.Novo ? "N" :
                                           (chamado.Status == Publicas.StatusChamado.Pendente ? "P" :
                                           (chamado.Status == Publicas.StatusChamado.EmDesenvolvimento ? "D" :
                                           (chamado.Status == Publicas.StatusChamado.AguardandoAutorizacao ? "U" :
                                           (chamado.Status == Publicas.StatusChamado.AguardandoCronograma ? "G" :
                                           (chamado.Status == Publicas.StatusChamado.AguardandoConserto ? "S" :
                                           "R")))))))))) + "'");

                    query.Append(", '" + (chamado.Privado ? "S" : "N") + "'");

                    query.Append(", '" + chamado.Adequacao + "'");
                    query.Append(", " + chamado.Prazo);

                    if (chamado.IdUsuarioAutorizacao != 0)
                    {
                        query.Append(", " + chamado.IdUsuarioAutorizacao);
                        query.Append(", '" + (chamado.Autorizado ? "S": "N") + "'" );
                        query.Append(", '" + (chamado.AguardarAutorizado ? "S" : "N") + "'");
                    }

                    if (sla != DateTime.MinValue)
                        query.Append("   , To_date('" + sla.ToShortDateString() + " " + sla.ToShortTimeString() + "','dd/mm/yyyy hh24:mi')" );

                    query.Append(", '" + (chamado.RespondeuAutorizacao ? "S" : "N") + "'");
                    query.Append(", '" + chamado.Usuario + "'");
                    query.Append(")");
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (anexos != null && anexos.Count() != 0 && retorno)
                {
                    retorno = new NotificacaoChamadoDAO().Gravar(new NotificacaoChamado()
                    {
                        IdChamado = chamado.IdChamado,
                        IdHistorico = chamado.IdHistorico,
                        IdUsuario = Publicas._usuario.Id
                    });

                    anexos.ForEach(u => u.IdHistorico = _idHistorico);
                    retorno = new AnexoDoHistoricoDAO().Grava(anexos);
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

        public bool Altera(HistoricoDoChamado chamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                query.Clear();
                query.Append("Update niff_chm_histochamado");
                query.Append("   set descricao = '" + chamado.Descricao + "'");
                query.Append(" where IDHISTORICO = " + chamado.IdHistorico);

                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                                
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
    }
}
