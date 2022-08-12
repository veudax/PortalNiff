using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ChamadoDAO
    {
        IDataReader chamadoReader;
        IDataReader chamadoReader2;
        IDataReader chamadoReader3;
        IDataReader chamadoReader4;

        public List<int> Datas()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<int> _datas = new List<int>();

            try
            {
                query.Append("Select Distinct ano ");
                query.Append("  From ( Select Distinct To_char(DATA,'yyyy') ano ");
                query.Append("           From Niff_Chm_Chamado ");
                query.Append("          Union ALL");
                query.Append("         Select To_char(sysDate,'yyyy') ano ");
                query.Append("           From dual )");
                query.Append("Order By ano Desc");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    while (chamadoReader.Read())
                    {
                        _datas.Add(Convert.ToInt32(chamadoReader["ano"].ToString()));
                    }

                    if (_datas.Where(w => w.Equals(DateTime.Now.Year)).Count() == 0)
                        _datas.Add(DateTime.Now.Year);
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
            return _datas;
        }

        public List<Chamado> ListarNotificacoes(int departamento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Chamado> _lista = new List<Chamado>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.numero, Max(h.data) data, c.assunto, g.descricao Categoria, t.nome tela, u.nome, u.Idusuario, u.tipo, 'N' Status");
                query.Append("     , c.IdChamado, h.tipoUsuario");
                query.Append("  From niff_chm_histochamado h, niff_chm_chamado c, niff_chm_categorias g, niff_chm_telas t, Niff_Chm_Usuarios u");
                query.Append(" Where h.Idusuario <> " + Publicas._usuario.Id);

                if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante)
                    query.Append("  And c.idusuario = " + Publicas._usuario.Id); // que o usuário abriu.

                query.Append("  And h.Idchamado = c.Idchamado");
                query.Append("  And c.idcategoria = g.Idcategoria");
                query.Append("  And h.Idusuario = u.idusuario");
                query.Append("  And c.idtela = t.idtela");
                query.Append("  And h.privado = 'N'");
                query.Append("  And h.status = 'N'");
                query.Append("  And trunc(h.Data) = trunc(Sysdate)");

                if (departamento != 0)
                {
                    query.Append("   And u.IdEmpresa = " + Publicas._usuario.IdEmpresa);
                    query.Append("   and (u.IdDepartamento = " + departamento);
                    query.Append("    or u.Iddepartamento In (Select d.iddepartamento From niff_ads_colabdepartamento d Where d.idcolaborador = " + Publicas._idColaborador + "))");
                }
                else
                {
                    query.Append("   And (g.idcategoria In (Select idCategoria From Niff_Chm_Categautousuario Where Idusuario = " + Publicas._idUsuario + ")");
                    query.Append("    or c.Idusuario = " + Publicas._usuario.Id + ")");
                }

                query.Append("  And(h.idchamado, h.idhistorico) Not In(Select n.idchamado, n.idhistorico");
                query.Append("                                           From niff_chm_notifchamado n, niff_chm_histochamado h");
                query.Append("                                          Where n.Idusuario = " + Publicas._usuario.Id);
                query.Append("                                            And n.Idchamado = h.idchamado");
                query.Append("                                            And n.Idhistorico = h.Idhistorico)");
                query.Append(" Group By c.numero, c.Assunto, g.descricao, t.nome, u.nome, u.Idusuario, u.tipo");
                query.Append("     , c.Idchamado, h.tipoUsuario");
                query.Append(" Union All ");

                query.Append("Select c.numero, Max(h.data) data, c.assunto, g.descricao Categoria, t.nome tela, u.nome, u.Idusuario, u.tipo, 'R' Status");
                query.Append("     , c.Idchamado, h.tipoUsuario");
                query.Append("  From niff_chm_histochamado h, niff_chm_chamado c, niff_chm_categorias g, niff_chm_telas t, Niff_Chm_Usuarios u");
                query.Append(" Where h.Idusuario <> " + Publicas._usuario.Id);

                if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante)
                    query.Append("  And c.idusuario = " + Publicas._usuario.Id); // que o usuário abriu.

                query.Append("  And h.Idchamado = c.Idchamado");
                query.Append("  And c.idcategoria = g.Idcategoria");
                query.Append("  And h.Idusuario = u.idusuario");
                query.Append("  And c.idtela = t.idtela");
                query.Append("  And h.privado = 'N'");
                query.Append("  And h.status Not In ('N','F', 'C')");
                query.Append("  And trunc(h.Data) = trunc(Sysdate)");
                query.Append("  And h.idChamado In(Select IdChamado From Niff_Chm_Histochamado Where idUsuario = " + Publicas._usuario.Id + ")");

                if (departamento != 0)
                {
                    query.Append("  And u.IdEmpresa = " + Publicas._usuario.IdEmpresa);
                    query.Append("   and (u.IdDepartamento = " + departamento);
                    query.Append("    or u.Iddepartamento In (Select d.iddepartamento From niff_ads_colabdepartamento d Where d.idcolaborador = " + Publicas._idColaborador + "))");
                }
                else
                {
                    query.Append("   And (g.idcategoria In (Select idCategoria From Niff_Chm_Categautousuario Where Idusuario = " + Publicas._idUsuario + ")");
                    query.Append("    or c.Idusuario = " + Publicas._usuario.Id + ")");
                }

                query.Append("  And(h.idchamado, h.idhistorico) Not In(Select n.idchamado, n.idhistorico");
                query.Append("                                           From niff_chm_notifchamado n, niff_chm_histochamado h");
                query.Append("                                          Where n.Idusuario = " + Publicas._usuario.Id);
                query.Append("                                            And n.Idchamado = h.idchamado");
                query.Append("                                            And n.Idhistorico = h.Idhistorico)");
                query.Append(" Group By c.numero, c.Assunto, g.descricao, t.nome, u.nome, u.Idusuario, u.tipo");
                query.Append("     , c.Idchamado, h.tipoUsuario");
                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    while (chamadoReader.Read())
                    {
                        Chamado _chamado = new Chamado();

                        _chamado.Existe = true;

                        _chamado.Numero = chamadoReader["NUMERO"].ToString();
                        _chamado.Assunto = chamadoReader["Assunto"].ToString();
                        _chamado.IdUsuario = Convert.ToInt32(chamadoReader["Idusuario"].ToString());
                        _chamado.IdChamado = Convert.ToInt32(chamadoReader["IdChamado"].ToString());

                        string[] solicitante = chamadoReader["Nome"].ToString().Split(' ');
                        _chamado.NomeSolicitante = solicitante[0] + " " + solicitante[solicitante.Length - 1];

                        _chamado.Categoria = chamadoReader["Categoria"].ToString();
                        _chamado.Tela = chamadoReader["Tela"].ToString();
                        _chamado.DescricaoTipo = (chamadoReader["tipoUsuario"].ToString() != ""? 
                            (chamadoReader["tipoUsuario"].ToString() == "S" ? "Solicitante" : "Atendente") : 
                            chamadoReader["Tipo"].ToString());
                        _chamado.DescricaoStatus = chamadoReader["Status"].ToString();

                        _chamado.Data = Convert.ToDateTime(chamadoReader["Data"].ToString());

                        _lista.Add(_chamado);
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

        public int GetIdTemporizador(DateTime dataInicial, int idChamado, int idUsuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int id = 0;

            try
            {
                query.Append("Select id ");
                query.Append("  From niff_chm_tempoexecucao");
                query.Append(" where idchamado = " + idChamado);
                query.Append("   and datainicio = to_date('" + dataInicial.ToShortDateString() + " " + dataInicial.ToLongTimeString() + "', 'dd/mm/yyyy hh24:mi:ss')");
                query.Append("   and idUsuario = " + idUsuario);

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader4 = executar.ExecuteQuery();

                using (chamadoReader4)
                {
                    if (chamadoReader4.Read())
                    {
                        id = Convert.ToInt32(chamadoReader4["id"].ToString());
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
            return id;
        }

        public List<TempoExecucao> Temporizador(int idChamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<TempoExecucao> _lista = new List<TempoExecucao>();

            try
            {
                query.Append("Select e.id, e.idchamado, e.datainicio, e.datafim, e.tempomin, e.idusuario ");
                query.Append("     , SUBSTR( u.nome, 1, INSTR( u.nome,' ')-1 ) || Substr( u.nome, INSTR( u.nome, ' ', -1)) nome");
                query.Append("  From niff_chm_tempoexecucao e, niff_chm_usuarios u");
                query.Append(" where e.idchamado = " + idChamado);
                query.Append("   and e.idUsuario = u.IdUsuario");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader4 = executar.ExecuteQuery();

                using (chamadoReader4)
                {
                    while (chamadoReader4.Read())
                    {
                        TempoExecucao _tempo = new TempoExecucao();

                        _tempo.Id = Convert.ToInt32(chamadoReader4["id"].ToString());
                        _tempo.IdChamado = Convert.ToInt32(chamadoReader4["idChamado"].ToString());
                        _tempo.IdUsuario = Convert.ToInt32(chamadoReader4["idUsuario"].ToString());
                        _tempo.DataInicio = Convert.ToDateTime(chamadoReader4["DataInicio"].ToString());
                        _tempo.NomeUsuario = chamadoReader4["Nome"].ToString();

                        try
                        {
                            _tempo.DataFim = Convert.ToDateTime(chamadoReader4["datafim"].ToString());
                        }
                        catch { }

                        _tempo.Minutos = Convert.ToInt32(chamadoReader4["tempomin"].ToString());

                        _lista.Add(_tempo);
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

        public bool IniciarTemporizador(int idChamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Insert into niff_chm_tempoexecucao ( id, idchamado, datainicio, datafim, tempomin, idusuario )");
                query.Append(" Values ((Select Nvl(Max(Id),0) +1 From niff_chm_tempoexecucao) ");
                query.Append("      , " + idChamado);
                query.Append("      , sysdate, null, 0, " + Publicas._usuario.Id + ")");

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;
                
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return true;
        }

        public bool PausarTemporizador(int idTemporizador, DateTime inicio)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            DateTime _dataLog = DateTime.MinValue;

            try
            {
                query.Clear();
                query.Append("Select Max(l.data) data");
                query.Append("  From niff_chm_log l");
                query.Append(" Where idusuario = " + Publicas._usuario.Id);
                query.Append("   And Upper(fc_niff_LongToVarchar(l.idlog)) Like upper('%logoff%')");
                query.Append("   and trunc(data) = to_date('" + inicio.ToShortDateString() + "','dd/mm/yyyy')");
                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader4 = executar.ExecuteQuery();
                using (chamadoReader4)
                {
                    if (chamadoReader4.Read())
                    {
                        try
                        {
                            _dataLog = Convert.ToDateTime(chamadoReader4["Data"].ToString());
                        }
                        catch { }
                    }
                }

                query.Clear();
                query.Append("Update niff_chm_tempoexecucao ");

                if (inicio.Date == DateTime.Now.Date || _dataLog == DateTime.MinValue)
                {
                    query.Append("  set datafim = Sysdate");
                    query.Append("    , tempomin = trunc((mod(SysDate - DataInicio, 1) * 24) *60)");
                }
                else
                {
                    query.Append("  set datafim = To_date('" + _dataLog.ToShortDateString() + " " + _dataLog.ToLongTimeString() + "','dd/mm/yyyy hh24:mi:ss')");
                    query.Append("    , tempomin = " + _dataLog.Subtract(inicio).TotalMinutes );
                }
                query.Append(" where id = " + idTemporizador);

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;

            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return true;
        }

        public Chamado PopulaChamado(IDataReader chamadoReader)
        {
            Chamado _chamado = new Chamado();
            HistoricoDoChamado _historico = new HistoricoDoChamado();

            _chamado.Existe = true;
            _chamado.IdChamado = Convert.ToInt32(chamadoReader["IDCHAMADO"].ToString());
            _chamado.IdEmpresa = Convert.ToInt32(chamadoReader["IDEMPRESA"].ToString());
            try
            {
                _chamado.IdEmpresaSolicitante = Convert.ToInt32(chamadoReader["IDEMPRESASOLICITANTE"].ToString());
            }
            catch { }

            if (_chamado.IdEmpresa == _chamado.IdEmpresaSolicitante)
            {
                Classes.Empresa _empresa = new EmpresaDAO().ConsultaEmpresa(_chamado.IdEmpresa);
                _chamado.EmpresaSelecionada = _empresa.NomeAbreviado;
            }
            else
            {
                Classes.Empresa _empresa = new EmpresaDAO().ConsultaEmpresa(_chamado.IdEmpresaSolicitante);
                _chamado.EmpresaSolicitante = _empresa.NomeAbreviado;
                _empresa = new EmpresaDAO().ConsultaEmpresa(_chamado.IdEmpresa);
                _chamado.EmpresaSelecionada = _empresa.NomeAbreviado;
            }

                _chamado.IdUsuario = Convert.ToInt32(chamadoReader["IdUsuario"].ToString());
            _chamado.IdCategoria = Convert.ToInt32(chamadoReader["IDCATEGORIA"].ToString());

            try
            {
                _chamado.IdUsuarioAcompanhamento = Convert.ToInt32(chamadoReader["IdUsuarioAcompanhamento"].ToString());
            }
            catch { }
            
            try
            {
                _chamado.IdChamadoAssociado = Convert.ToInt32(chamadoReader["IdChamadoAssociado"].ToString());
            }
            catch { }

            try
            {
                _chamado.IdUsuarioAutorizacao = Convert.ToInt32(chamadoReader["Idusuarioautorizacao"].ToString());
                _chamado.MotivoNegacaoDaAutorizacao = chamadoReader["motivoautorizacao"].ToString();
                _chamado.Autorizado = chamadoReader["autorizado"].ToString() == "S";
                _chamado.AguardarAutorizado = chamadoReader["aguardarretornoauto"].ToString() == "S";
            }
            catch { }

            try
            {
                _chamado.IdTela = Convert.ToInt32(chamadoReader["IDTELA"].ToString());
            }
            catch { }

            _chamado.Exibir = true;
            _chamado.Numero = chamadoReader["NUMERO"].ToString();
            _chamado.Assunto = chamadoReader["Assunto"].ToString();
            _chamado.Adequacao = chamadoReader["NUMADEQFORN"].ToString();
            _chamado.DescricaoAvaliacao = chamadoReader["DescricaoAvaliacao"].ToString();
            _chamado.DescricaoAvaliacaoSolic = chamadoReader["DescricaoAvaliacaoSolic"].ToString();
            _chamado.PrazoDesenvolvimento = Convert.ToInt32(chamadoReader["PRAZODESENVOLVIMENTO"].ToString());

            string[] solicitante = chamadoReader["UsuarioAbertura"].ToString().Split(' ');
            _chamado.NomeSolicitante = solicitante[0] + " " + solicitante[solicitante.Length - 1];

            _chamado.Categoria = chamadoReader["Categoria"].ToString();
            _chamado.Reavaliar = chamadoReader["Reavaliar"].ToString() == "S";
            _chamado.Reavaliado = chamadoReader["Reavaliado"].ToString() == "S";
            _chamado.TrocouCategoria = chamadoReader["TrocouCategoria"].ToString() == "S";

            try
            {
                _chamado.DataReavaliacao = Convert.ToDateTime(chamadoReader["DataReavaliacao"].ToString());
            }
            catch { }
            
            if (_chamado.IdTela != 0)
            {
                Tela _tela = new TelaDAO().Consulta(_chamado.IdTela);
                _chamado.Tela = _tela.Nome;

                Modulo _modulo = new ModuloDAO().Consulta(_tela.IdModulo);
                _chamado.Modulo = _modulo.Nome;
            }

            _chamado.Data = Convert.ToDateTime(chamadoReader["Data"].ToString());

            #region Opções
            switch (chamadoReader["TipoChamado"].ToString())
            {
                case "D":
                    _chamado.Tipo = Publicas.TipoChamado.Duvida;
                    break;
                case "E":
                    _chamado.Tipo = Publicas.TipoChamado.Erro;
                    break;
                case "I":
                    _chamado.Tipo = Publicas.TipoChamado.Implementacao;
                    break;
                case "A":
                    _chamado.Tipo = Publicas.TipoChamado.Acesso;
                    break;
                case "J":
                    _chamado.Tipo = Publicas.TipoChamado.Ajustes;
                    break;
                case "P":
                    _chamado.Tipo = Publicas.TipoChamado.Projeto;
                    break;
                case "S":
                    _chamado.Tipo = Publicas.TipoChamado.Solicitacao;
                    break;
            }

            switch (chamadoReader["Status"].ToString())
            {
                case "A":
                    _chamado.Status = Publicas.StatusChamado.Adequacao;
                    break;
                case "N":
                    _chamado.Status = Publicas.StatusChamado.Novo;
                    break;
                case "P":
                    _chamado.Status = Publicas.StatusChamado.Pendente;
                    break;
                case "R":
                    _chamado.Status = Publicas.StatusChamado.Reaberto;
                    break;
                case "C":
                    _chamado.Status = Publicas.StatusChamado.Cancelado;
                    break;
                case "E":
                    _chamado.Status = Publicas.StatusChamado.EmAndamento;
                    break;
                case "F":
                    _chamado.Status = Publicas.StatusChamado.Finalizado;
                    break;
                case "D":
                    _chamado.Status = Publicas.StatusChamado.EmDesenvolvimento;
                    break;
                case "U":
                    _chamado.Status = Publicas.StatusChamado.AguardandoAutorizacao;
                    break;
                case "S":
                    _chamado.Status = Publicas.StatusChamado.AguardandoConserto;
                    break;
                case "G":
                    _chamado.Status = Publicas.StatusChamado.AguardandoCronograma;
                    break;
            }

            switch (chamadoReader["ORIGEM"].ToString())
            {
                case "E":
                    _chamado.Origens = Publicas.Origem.Email;
                    break;
                case "T":
                    _chamado.Origens = Publicas.Origem.Telefone;
                    break;
                case "C":
                    _chamado.Origens = Publicas.Origem.OnLine;
                    break;
                case "H":
                    _chamado.Origens = Publicas.Origem.Chat;
                    break;
            }

            switch (chamadoReader["Prioridade"].ToString())
            {
                case "A":
                    _chamado.Prioridade = Publicas.Prioridades.Alta;
                    break;
                case "B":
                    _chamado.Prioridade = Publicas.Prioridades.Baixa;
                    break;
                case "C":
                    _chamado.Prioridade = Publicas.Prioridades.Critico;
                    break;
                case "M":
                    _chamado.Prioridade = Publicas.Prioridades.Media;
                    break;
            }

            _chamado.Ordem = (chamadoReader["Prioridade"].ToString() == "C" && _chamado.Status != Publicas.StatusChamado.Novo ? 1 :
                             (_chamado.Status == Publicas.StatusChamado.Novo || _chamado.Status == Publicas.StatusChamado.Reaberto ? 0 :
                             (_chamado.Status == Publicas.StatusChamado.AguardandoAutorizacao ? 2 :
                             ((_chamado.Status == Publicas.StatusChamado.EmAndamento || _chamado.Status == Publicas.StatusChamado.EmDesenvolvimento) &&
                               Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente ? 3 :
                             ((_chamado.Status == Publicas.StatusChamado.EmAndamento || _chamado.Status == Publicas.StatusChamado.EmDesenvolvimento) &&
                               Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante ? 4 :
                             (_chamado.Status == Publicas.StatusChamado.Pendente && Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante ? 3 :
                             (_chamado.Status == Publicas.StatusChamado.Pendente && Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente ? 4 :
                             (_chamado.Status == Publicas.StatusChamado.Adequacao || _chamado.Status == Publicas.StatusChamado.AguardandoConserto ? 5 : 6))))))));

            _chamado.DescricaoStatus = Publicas.GetDescription(_chamado.Status, "");
            _chamado.DescricaoDaOrigem = Publicas.GetDescription(_chamado.Origens, "");
            _chamado.DescricaoPrioridade = Publicas.GetDescription(_chamado.Prioridade, "");
            _chamado.DescricaoTipo = Publicas.GetDescription(_chamado.Tipo, "");

            switch (chamadoReader["Avaliacao"].ToString())
            {
                case "1":
                    _chamado.Avaliacao = Publicas.TipoDeSatisfacaoAtendimento.Ruim;
                    break;
                case "2":
                    _chamado.Avaliacao = Publicas.TipoDeSatisfacaoAtendimento.Regular;
                    break;
                case "4":
                    _chamado.Avaliacao = Publicas.TipoDeSatisfacaoAtendimento.MuitoBom;
                    break;
                case "3":
                    _chamado.Avaliacao = Publicas.TipoDeSatisfacaoAtendimento.Bom;
                    break;
                case "5":
                    _chamado.Avaliacao = Publicas.TipoDeSatisfacaoAtendimento.Excelente;
                    break;
                default:
                    _chamado.Avaliacao = Publicas.TipoDeSatisfacaoAtendimento.SemAvaliacao;
                    break;
            }

            switch (chamadoReader["AvaliacaoSolicitante"].ToString())
            {
                case "1":
                    _chamado.AvaliacaoSolicitante = Publicas.TipoDeSatisfacaoAtendimento.Ruim;
                    break;
                case "2":
                    _chamado.AvaliacaoSolicitante = Publicas.TipoDeSatisfacaoAtendimento.Regular;
                    break;
                case "4":
                    _chamado.AvaliacaoSolicitante = Publicas.TipoDeSatisfacaoAtendimento.MuitoBom;
                    break;
                case "3":
                    _chamado.AvaliacaoSolicitante = Publicas.TipoDeSatisfacaoAtendimento.Bom;
                    break;
                case "5":
                    _chamado.AvaliacaoSolicitante = Publicas.TipoDeSatisfacaoAtendimento.Excelente;
                    break;
                default:
                    _chamado.AvaliacaoSolicitante = Publicas.TipoDeSatisfacaoAtendimento.SemAvaliacao;
                    break;
            }

            _chamado.DescricaoAvaliacao = Publicas.GetDescription(_chamado.Avaliacao, "");
            _chamado.DescricaoAvaliacaoSolic = Publicas.GetDescription(_chamado.AvaliacaoSolicitante, "");

            #endregion

            _chamado.DiasDoLembrete = Convert.ToInt32(chamadoReader["LembrarDentreDeDias"].ToString());
            _chamado.MotivoLembrete = chamadoReader["MotivoLembrete"].ToString();

            try
            {
                if (_chamado.Status == Publicas.StatusChamado.Novo)
                    _chamado.Descricao = new HistoricosChamadoDAO().RetornaDescricaodaMenorData(_chamado.IdChamado).Descricao;
                else
                {
                    _chamado.Descricao = new HistoricosChamadoDAO().RetornaDescricaodaMaiorData(_chamado.IdChamado);
                    _chamado.Privado = new HistoricosChamadoDAO().RetornaSeUltimoTramiteFoiPrivado(_chamado.IdChamado);
                }
            }
            catch { }

            _chamado.UltimoUsuario = new HistoricosChamadoDAO().TrazoUltimoSolicitante(_chamado.IdChamado);
            _chamado.PrimeiroAtendente = new HistoricosChamadoDAO().TrazoPrimeiroAtendete(_chamado.IdChamado);

            _historico = new HistoricosChamadoDAO().RetornaMaiorDataDeRetorno(_chamado.IdChamado, true, _chamado.IdUsuario);

            
            if (_chamado.Status != Publicas.StatusChamado.Novo)
            {
                try
                {
                    solicitante = _historico.Nome.Split(' ');
                    _chamado.NomeAnalista = solicitante[0] + " " + solicitante[solicitante.Length - 1];
                    _chamado.IdAtendente = _historico.IdUsuario;
                    _chamado.ApenasAtendendeQueEncerra = _historico.PodeEncerrar;
                }
                catch { }

                _historico = new HistoricosChamadoDAO().RetornaMaiorDataDeRetorno(_chamado.IdChamado, false, 0);
                _chamado.DataRetorno = _historico.Data;
            }

            if (_chamado.DataRetorno != DateTime.MinValue)
            {
                List<HistoricoDoChamado> _listaUsuarios = new HistoricosChamadoDAO().RetornaUsuariosDoChamado(_chamado.IdChamado);

                _chamado.Retorno = _chamado.DataRetorno.ToShortDateString() + " " + _chamado.DataRetorno.ToShortTimeString();

                // fica true quando o usuário não é o responsável. 
                _chamado.Lido = true;
                foreach (var item in _listaUsuarios.Where(w => w.IdUsuario == Publicas._usuario.Id))
                {
                    NotificacaoChamado _not = new NotificacaoChamadoDAO().Consultar(Publicas._usuario.Id, _historico.IdHistorico);
                    _chamado.Lido = _not.Existe;
                }
            }
            else
            {
                // Se nao tiver sido respondido por um atendente irá fica em destaque até que seja. 
                if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente)
                    _chamado.Lido = false;
            }

            int _diasResposta = 0;

            // Os chamados novos ficam com a data e hora atual, para sempre ficaram primeiro na lista
            _chamado.DataOrdenacao = DateTime.MinValue;
            //_chamado.DataOrdenacao = DateTime.Now;

            /*if (_chamado.Status != Publicas.StatusChamado.Novo)
                _chamado.DataOrdenacao = _chamado.DataRetorno;*/

            _chamado.DataOrdenacao = _chamado.DataOrdenacao.AddMinutes(_chamado.Ordem);

            try
            {
                if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Socilitante &&
                    _chamado.UltimoUsuario == Publicas._usuario.Nome[0] + " " + Publicas._usuario.Nome[Publicas._usuario.Nome.Length - 1])
                {
                    if (_chamado.Ordem > 1)
                    {
                        if (_chamado.Prioridade == Publicas.Prioridades.Baixa)
                            _chamado.DataOrdenacao = _chamado.DataOrdenacao.AddSeconds(-05);
                        if (_chamado.Prioridade == Publicas.Prioridades.Media)
                            _chamado.DataOrdenacao = _chamado.DataOrdenacao.AddSeconds(-10);
                        if (_chamado.Prioridade == Publicas.Prioridades.Alta)
                            _chamado.DataOrdenacao = _chamado.DataOrdenacao.AddSeconds(-20);
                    }
                }

                if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente &&
                    _chamado.IdAtendente == Publicas._usuario.Id)
                {
                    if (_chamado.Ordem > 1)
                    {
                        if (_chamado.Prioridade == Publicas.Prioridades.Baixa)
                            _chamado.DataOrdenacao = _chamado.DataOrdenacao.AddSeconds(-05);
                        if (_chamado.Prioridade == Publicas.Prioridades.Media)
                            _chamado.DataOrdenacao = _chamado.DataOrdenacao.AddSeconds(-10);
                        if (_chamado.Prioridade == Publicas.Prioridades.Alta)
                            _chamado.DataOrdenacao = _chamado.DataOrdenacao.AddSeconds(-20);
                    }
                }

                if (_chamado.Ordem == 0)
                {
                    if (_chamado.Prioridade == Publicas.Prioridades.Baixa)
                        _chamado.DataOrdenacao = _chamado.DataOrdenacao.AddSeconds(50);
                    if (_chamado.Prioridade == Publicas.Prioridades.Media)
                        _chamado.DataOrdenacao = _chamado.DataOrdenacao.AddSeconds(30);
                    if (_chamado.Prioridade == Publicas.Prioridades.Alta)
                        _chamado.DataOrdenacao = _chamado.DataOrdenacao.AddSeconds(10);
                }

            }
            catch {}

            if (_chamado.DataRetorno.DayOfWeek == DayOfWeek.Saturday || _chamado.DataRetorno.DayOfWeek == DayOfWeek.Sunday)
            {
                if (_chamado.DataRetorno.DayOfWeek == DayOfWeek.Saturday)
                    _chamado.DataRetorno = _chamado.DataRetorno.AddDays(2);
                else
                    _chamado.DataRetorno = _chamado.DataRetorno.AddDays(1);
            }

            if (_chamado.Status == Publicas.StatusChamado.EmDesenvolvimento || _chamado.PrazoDesenvolvimento != 0)
                _diasResposta = _chamado.PrazoDesenvolvimento;
            else
                _diasResposta = Convert.ToInt32(chamadoReader["dias"].ToString());

            // Aguardando cronograma, pega a data/hora do dia e acrescenta a quantidade de dias definido. 
            if (_chamado.Status == Publicas.StatusChamado.AguardandoCronograma)
                _chamado.SLA = DateTime.Now.AddDays(_diasResposta);
            else
            {
                if (_chamado.Status == Publicas.StatusChamado.Pendente || _chamado.Status == Publicas.StatusChamado.AguardandoAutorizacao ||
                    _chamado.Status == Publicas.StatusChamado.Reaberto) // Aguardando Solicitante 
                    _chamado.SLA = _chamado.DataRetorno.AddDays(Convert.ToInt32(chamadoReader["dias"].ToString()));
                else
                {
                    _chamado.SLA = (_chamado.NomeAnalista == "" || _chamado.NomeAnalista == null ? _chamado.Data.AddDays(_diasResposta) :
                                        _chamado.DataRetorno.AddDays(_diasResposta));
                }
            }

            //Periodo de férias Analista
            List<Classes.ProgramacaoFerias> _listaProgramacao = new ProgramacaoFeriasDAO().ListarTodas();

            if (_chamado.Status != Publicas.StatusChamado.Pendente && _chamado.Status != Publicas.StatusChamado.Finalizado &&
                _chamado.Status != Publicas.StatusChamado.AguardandoAutorizacao && _chamado.Status != Publicas.StatusChamado.Cancelado &&
                _chamado.Status != Publicas.StatusChamado.Adequacao && _chamado.Status != Publicas.StatusChamado.AguardandoCronograma)
            {
                
                Classes.Usuario _usuarioAtendente = new UsuarioDAO().ConsultaUsuarioPorID(_chamado.IdAtendente);

                // soma 2 dias para o retorno de ferias
                foreach (var item in _listaProgramacao.Where(w => w.CodIntFunc == _usuarioAtendente.CodigoInternoFuncionarioGlobus
                                                               && (_chamado.SLA.Date >= w.DataInicio && _chamado.SLA.Date <= w.DataFim.AddDays(2))))
                {
                    _chamado.SLA = _chamado.DataRetorno.AddDays(item.QuantidadeDias+2);
                }

                if (_chamado.SLA.Date < DateTime.Now.Date)
                {
                    foreach (var item in _listaProgramacao.Where(w => w.CodIntFunc == _usuarioAtendente.CodigoInternoFuncionarioGlobus
                                                               && ( DateTime.Now.Date >= w.DataInicio && DateTime.Now.Date <= w.DataFim.AddDays(2) )))
                    {
                        _chamado.SLA = _chamado.DataRetorno.AddDays(item.QuantidadeDias + 2);
                    }
                }

            }

            if (_chamado.Status == Publicas.StatusChamado.Novo)
            {
                if (_chamado.Data.DayOfWeek == DayOfWeek.Saturday || _chamado.Data.DayOfWeek == DayOfWeek.Sunday)
                    _chamado.SLA = _chamado.Data.AddDays(_diasResposta + 2);
            }

            //Feriado _feriado = new FeriadoDAO().Consulta(Publicas._usuario.IdEmpresa, _chamado.SLA);
            Feriado _feriado = new FeriadoDAO().Consulta(_chamado.SLA);

            if (_feriado.Existe)
                _chamado.SLA = _chamado.SLA.AddDays(1);

            // se cair o dia maximo de retorno em um sabado ou domingo muda para o próximo dia util.
            if (_chamado.SLA.DayOfWeek == DayOfWeek.Saturday || _chamado.SLA.DayOfWeek == DayOfWeek.Sunday)
            {
                if (_chamado.SLA.DayOfWeek == DayOfWeek.Saturday)
                    _chamado.SLA = _chamado.SLA.AddDays(3);
                else
                    _chamado.SLA = _chamado.SLA.AddDays(2);
            }

            DateTime _dataLembrete = DateTime.MinValue;

            try
            {
                _dataLembrete = Convert.ToDateTime(chamadoReader["Lembrete"].ToString());
            }
            catch
            {
            }

            if (_dataLembrete != DateTime.MinValue)
            {
                _chamado.DiasDoLembrete = Convert.ToInt32(_dataLembrete.Subtract(_chamado.DataRetorno).TotalDays);
                _chamado.DataLembrete = _dataLembrete;

                //_feriado = new FeriadoDAO().Consulta(Publicas._usuario.IdEmpresa, _chamado.DataLembrete);
                _feriado = new FeriadoDAO().Consulta(_chamado.DataLembrete);

                if (_feriado.Existe)
                    _chamado.DataLembrete = _chamado.DataLembrete.AddDays(1);

                // se cair o dia maximo de retorno em uma sabado ou domingo muda para o próximo dia util.
                if (_chamado.DataLembrete.DayOfWeek == DayOfWeek.Saturday || _chamado.DataLembrete.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (_chamado.DataLembrete.DayOfWeek == DayOfWeek.Saturday)
                        _chamado.DataLembrete = _chamado.DataLembrete.AddDays(2);
                    else
                        _chamado.DataLembrete = _chamado.DataLembrete.AddDays(1);
                }
            }
            else
            if (_chamado.DiasDoLembrete != 0)
            {
                _chamado.DataLembrete = _chamado.DataRetorno.AddDays(_chamado.DiasDoLembrete);

                _feriado = new FeriadoDAO().Consulta(_chamado.DataLembrete);

                if (_feriado.Existe)
                    _chamado.DataLembrete = _chamado.DataLembrete.AddDays(1);

                // se cair o dia maximo de retorno em uma sabado ou domingo muda para o próximo dia util.
                if (_chamado.DataLembrete.DayOfWeek == DayOfWeek.Saturday || _chamado.DataLembrete.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (_chamado.DataLembrete.DayOfWeek == DayOfWeek.Saturday)
                        _chamado.DataLembrete = _chamado.DataLembrete.AddDays(2);
                    else
                        _chamado.DataLembrete = _chamado.DataLembrete.AddDays(1);
                }
            }
            

            if (DateTime.Now > _chamado.SLA)
                _chamado.TipoPrazos = "F"; // Fora do prazo
            else
            {
                if (DateTime.Now >= _chamado.SLA.AddHours(-3))
                    _chamado.TipoPrazos = "C"; // finalizando o prazo
                else
                    _chamado.TipoPrazos = "N"; // Dentro do prazo
            }

            #region Temporizador
            try
            {
                _chamado.MinutosEstimados = Convert.ToInt32(chamadoReader["TEMPOESTIMADOMIN"].ToString());

                DateTime _dataEstimada = DateTime.MinValue.AddMinutes(_chamado.MinutosEstimados);

                if (_chamado.MinutosEstimados > 0)
                    _chamado.HorasEstimadas = (((_dataEstimada.Day - 1) * 24) + _dataEstimada.Hour).ToString("00") + ":" + _dataEstimada.Minute.ToString("00");
            }

            catch { }
            try
            {
                _chamado.InicioTemporizador = Convert.ToDateTime(chamadoReader["InicioTemp"].ToString());

                _chamado.IdTemporizador = GetIdTemporizador(_chamado.InicioTemporizador, _chamado.IdChamado, _chamado.IdAtendente);

                try
                {
                    _chamado.FimTemporizador = Convert.ToDateTime(chamadoReader["FimTemp"].ToString());
                    _chamado.MinutosTemporizador = 0;
                }
                catch
                {
                    _chamado.MinutosTemporizador = (int)DateTime.Now.Subtract(_chamado.InicioTemporizador).TotalMinutes;
                }

                List<Classes.TempoExecucao> _listaTemposExecucao = Temporizador(_chamado.IdChamado);

                try
                {
                    _chamado.MinutosTemporizador = _chamado.MinutosTemporizador + _listaTemposExecucao.Sum(s => s.Minutos);
                    
                    DateTime _dataEstimada = DateTime.MinValue.AddMinutes(_chamado.MinutosTemporizador);
                    _chamado.Temporizador = (((_dataEstimada.Day - 1) * 24) + _dataEstimada.Hour).ToString("00") + ":" + _dataEstimada.Minute.ToString("00");

                }
                catch 
                { }

                _chamado.TemporizadorEmAndamento = _chamado.FimTemporizador == DateTime.MinValue;

            }
            catch { }
            #endregion

            _chamado.NomeAutorizador = BuscaNomeAprovador(_chamado.IdChamado);
            return _chamado;
        }

        //public Chamado ChamadosParaAutorizar(Chamado _lista, int chamado)
        public List<Chamado> ChamadosParaAutorizar(List<Chamado> _lista, string status = "T", string ano = "")
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            
            Publicas.mensagemDeErro = string.Empty;
            Chamado _chamado = new Chamado();
            try
            {
                query.Clear();
                query.Append("Select c.IDCHAMADO    ");
                query.Append("     , c.IDUSUARIO    ");
                query.Append("     , c.IDCATEGORIA  ");
                query.Append("     , c.IDTELA       ");
                query.Append("     , c.ASSUNTO      ");
                query.Append("     , c.ASSUNTO      ");
                query.Append("     , c.IDEMPRESA    ");
                query.Append("     , c.DATA         ");
                query.Append("     , c.NUMERO       ");
                query.Append("     , c.STATUS       ");
                query.Append("     , c.ORIGEM       ");
                query.Append("     , c.PRIORIDADE   ");
                query.Append("     , c.NUMADEQFORN  ");
                query.Append("     , c.DATAENTADEQ  ");
                query.Append("     , c.AVALIACAO    ");
                query.Append("     , c.TipoChamado  ");
                query.Append("     , c.DescricaoAvaliacao          ");
                query.Append("     , ua.Nome UsuarioAbertura       ");
                query.Append("     , ct.descricao categoria        ");
                query.Append("     , c.IDCHAMADOASSOCIADO          ");
                query.Append("     , c.AtendenteFoiCortez          ");
                query.Append("     , c.DataAvaliacaoDoSolicitante  ");
                query.Append("     , c.SolicitanteAbriuCorretamente");
                query.Append("     , c.SolicitanteRespDentroDePrazo");
                query.Append("     , c.SolicitanteFoiCortez        ");
                query.Append("     , c.AvaliacaoSolicitante        ");
                query.Append("     , c.DescricaoAvaliacaoSolic     ");
                query.Append("     , p.qtddiasretornochamado dias  ");
                query.Append("     , c.PRAZODESENVOLVIMENTO        ");
                query.Append("     , c.LembrarDentreDeDias         ");
                query.Append("     , c.MotivoLembrete              ");
                query.Append("     , h.Idusuarioautorizacao        ");
                query.Append("     , h.motivoautorizacao           ");
                query.Append("     , h.autorizado                  ");
                query.Append("     , h.aguardarretornoauto         ");
                query.Append("     , c.Reavaliar                   ");
                query.Append("     , c.Reavaliado                   ");
                query.Append("     , c.DataReavaliacao                   ");
                query.Append("     , u.Nome Autorizador");
                query.Append("     , c.IDEMPRESASOLICITANTE");
                query.Append("     , c.TrocouCategoria");
                query.Append("     , (Select Data");
                query.Append("          From niff_chm_lembretechamados l");
                query.Append("         Where l.Idchamado = c.IdChamado");
                query.Append("           And trunc(sysdate) = Data) Lembrete");

                query.Append("  From Niff_Chm_Chamado c, Niff_CHM_Usuarios ua, Niff_Chm_Parametros p   ");
                query.Append("     , Niff_Chm_Categorias CT, Niff_Chm_Histochamado h, Niff_CHM_Usuarios u");

                query.Append(" Where c.idUsuario = ua.IdUsuario    ");
                query.Append("   and ct.Idcategoria = c.idcategoria");
                query.Append("   and c.idchamadoagrupado Is Null   ");

                if (ano != "")
                {
                    if (DateTime.Now.Month < 3 && ano == DateTime.Now.Year.ToString())
                        query.Append("   and trunc(c.Data) between To_date('31/07/2019','dd/mm/yyyy') and to_date('" + DateTime.Now.Date.ToShortDateString() + "','dd/mm/yyyy')");
                    else
                        query.Append("   and To_char(c.Data,'yyyy') = '" + ano + "'");
                }

                if (status == "A") // aberto e em andamento
                    query.Append("   and c.Status not in ('F','C')");
                else
                {
                    if (status == "F" || status == "S") // Finalizados ou sem avaliacao
                    {
                        query.Append("   and c.Status = 'F'");
                        if (status == "S")
                            query.Append("   and (Nvl(c.Avaliacao,0) = 0 Or nvl(Avaliacaosolicitante,0) = 0) ");
                    }
                    else
                    {
                        if (status == "C")
                            query.Append("   and c.Status = 'C'");
                    }
                }

                query.Append("   And c.Idchamado = h.Idchamado");
                query.Append("   And h.Privado = 'N'");
                query.Append("   And h.respondeuautorizacao = 'N'");
                query.Append("   And H.Idusuarioautorizacao = " + Publicas._usuario.Id);
                query.Append("   And H.Idusuarioautorizacao = u.IdUsuario ");
                query.Append("   And h.Idhistorico = (Select Max(Idhistorico) From Niff_Chm_Histochamado Where idChamado = c.idchamado and privado = 'N')");
                //query.Append("   And c.Idchamado = " + chamado);

                query.Append("Group By c.IDCHAMADO ");
                query.Append("    , c.IDUSUARIO ");
                query.Append("    , c.IDCATEGORIA ");
                query.Append("    , c.IDTELA ");
                query.Append("    , c.ASSUNTO ");
                query.Append("    , c.ASSUNTO ");
                query.Append("    , c.IDEMPRESA ");
                query.Append("    , c.DATA ");
                query.Append("    , c.NUMERO ");
                query.Append("    , c.STATUS ");
                query.Append("    , c.ORIGEM ");
                query.Append("    , c.PRIORIDADE ");
                query.Append("    , c.NUMADEQFORN ");
                query.Append("    , c.DATAENTADEQ ");
                query.Append("    , c.AVALIACAO ");
                query.Append("    , c.TipoChamado ");
                query.Append("    , c.DescricaoAvaliacao ");
                query.Append("    , ua.Nome ");
                query.Append("    , ct.descricao ");
                query.Append("    , c.IDCHAMADOASSOCIADO ");
                query.Append("    , c.AtendenteFoiCortez ");
                query.Append("    , c.DataAvaliacaoDoSolicitante ");
                query.Append("    , c.SolicitanteAbriuCorretamente ");
                query.Append("    , c.SolicitanteRespDentroDePrazo ");
                query.Append("    , c.SolicitanteFoiCortez ");
                query.Append("    , c.AvaliacaoSolicitante ");
                query.Append("    , c.DescricaoAvaliacaoSolic ");
                query.Append("    , p.qtddiasretornochamado ");
                query.Append("    , c.PRAZODESENVOLVIMENTO");
                query.Append("     , c.LembrarDentreDeDias         ");
                query.Append("     , c.MotivoLembrete              ");
                query.Append("     , h.Idusuarioautorizacao         ");
                query.Append("     , h.Idusuarioautorizacao        ");
                query.Append("     , h.motivoautorizacao           ");
                query.Append("     , h.autorizado                  ");
                query.Append("     , h.aguardarretornoauto         ");
                query.Append("     , c.Reavaliar                   ");
                query.Append("     , c.Reavaliado                   ");
                query.Append("     , c.IDEMPRESASOLICITANTE");
                query.Append("     , c.DataReavaliacao, u.Nome, c.TrocouCategoria                    ");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    while (chamadoReader.Read())
                    {
                        _chamado = PopulaChamado(chamadoReader);

                        _chamado.IdUsuarioAutorizacao = Convert.ToInt32(chamadoReader["Idusuarioautorizacao"].ToString());
                        _chamado.MotivoNegacaoDaAutorizacao = chamadoReader["motivoautorizacao"].ToString();
                        _chamado.Autorizado = chamadoReader["autorizado"].ToString() == "S";
                        _chamado.AguardarAutorizado = chamadoReader["aguardarretornoauto"].ToString() == "S";

                        if ((!_chamado.Autorizado && !_chamado.AguardarAutorizado && string.IsNullOrEmpty(_chamado.MotivoNegacaoDaAutorizacao)) ||
                           (!_chamado.Autorizado && _chamado.AguardarAutorizado))
                            _chamado.Exibir = true;
                        else
                            _chamado.Exibir = false;

                        if (_lista.Where(w => w.IdChamado == _chamado.IdChamado).Count() == 0)
                        {
                            _lista.Add(_chamado);
                        }
                        else
                        {
                            foreach (var item in _lista.Where(w => w.IdChamado == _chamado.IdChamado))
                            {
                                item.IdUsuarioAutorizacao = _chamado.IdUsuarioAutorizacao;
                                item.MotivoNegacaoDaAutorizacao = _chamado.MotivoNegacaoDaAutorizacao;
                                item.Autorizado = _chamado.Autorizado;
                                item.AguardarAutorizado = _chamado.AguardarAutorizado;
                            }
                        }
                    }
                }

                //using (chamadoReader2)
                //{
                //    while (chamadoReader2.Read())
                //    {
                //        _chamado = PopulaChamado(chamadoReader2);
                //        _chamado.IdUsuarioAutorizacao = Convert.ToInt32(chamadoReader2["Idusuarioautorizacao"].ToString());
                //        _chamado.MotivoNegacaoDaAutorizacao = chamadoReader2["motivoautorizacao"].ToString();
                //        _chamado.Autorizado = chamadoReader2["autorizado"].ToString() == "S";
                //        _chamado.AguardarAutorizado = chamadoReader2["aguardarretornoauto"].ToString() == "S";

                //    }

                //    if (_chamado.Existe)
                //    {
                //        if ((!_chamado.Autorizado && !_chamado.AguardarAutorizado && string.IsNullOrEmpty(_chamado.MotivoNegacaoDaAutorizacao)) ||
                //           (!_chamado.Autorizado && _chamado.AguardarAutorizado))
                //        {
                //            _lista.IdUsuarioAutorizacao = _chamado.IdUsuarioAutorizacao;
                //            _lista.MotivoNegacaoDaAutorizacao = _chamado.MotivoNegacaoDaAutorizacao;
                //            _lista.Autorizado = _chamado.Autorizado;
                //            _lista.AguardarAutorizado = _chamado.AguardarAutorizado;
                //            _lista.Exibir = true;
                //        }
                //        else
                //        {
                //            _lista.Exibir = false;
                //        }
                //    }
                //}               
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

        public List<Chamado> ChamadosParaRespondidos(List<Chamado> _lista, string status = "T", string ano = "")
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();

            Publicas.mensagemDeErro = string.Empty;
            Chamado _chamado = new Chamado();
            try
            {
                query.Clear();
                query.Append("Select c.IDCHAMADO    ");
                query.Append("     , c.IDUSUARIO    ");
                query.Append("     , c.IDCATEGORIA  ");
                query.Append("     , c.IDTELA       ");
                query.Append("     , c.ASSUNTO      ");
                query.Append("     , c.ASSUNTO      ");
                query.Append("     , c.IDEMPRESA    ");
                query.Append("     , c.DATA         ");
                query.Append("     , c.NUMERO       ");
                query.Append("     , c.STATUS       ");
                query.Append("     , c.ORIGEM       ");
                query.Append("     , c.PRIORIDADE   ");
                query.Append("     , c.NUMADEQFORN  ");
                query.Append("     , c.DATAENTADEQ  ");
                query.Append("     , c.AVALIACAO    ");
                query.Append("     , c.TipoChamado  ");
                query.Append("     , c.DescricaoAvaliacao          ");
                query.Append("     , ua.Nome UsuarioAbertura       ");
                query.Append("     , ct.descricao categoria        ");
                query.Append("     , c.IDCHAMADOASSOCIADO          ");
                query.Append("     , c.AtendenteFoiCortez          ");
                query.Append("     , c.DataAvaliacaoDoSolicitante  ");
                query.Append("     , c.SolicitanteAbriuCorretamente");
                query.Append("     , c.SolicitanteRespDentroDePrazo");
                query.Append("     , c.SolicitanteFoiCortez        ");
                query.Append("     , c.AvaliacaoSolicitante        ");
                query.Append("     , c.DescricaoAvaliacaoSolic     ");
                query.Append("     , p.qtddiasretornochamado dias  ");
                query.Append("     , c.PRAZODESENVOLVIMENTO        ");
                query.Append("     , c.LembrarDentreDeDias         ");
                query.Append("     , c.MotivoLembrete              ");
                query.Append("     , h.Idusuarioautorizacao        ");
                query.Append("     , h.motivoautorizacao           ");
                query.Append("     , h.autorizado                  ");
                query.Append("     , h.aguardarretornoauto         ");
                query.Append("     , c.Reavaliar                   ");
                query.Append("     , c.Reavaliado                   ");
                query.Append("     , c.DataReavaliacao                   ");
                query.Append("     , u.Nome Autorizador");
                query.Append("     , c.IDEMPRESASOLICITANTE");
                query.Append("     , c.TrocouCategoria");
                query.Append("     , (Select Data");
                query.Append("          From niff_chm_lembretechamados l");
                query.Append("         Where l.Idchamado = c.IdChamado");
                query.Append("           And trunc(sysdate) = Data) Lembrete");
                query.Append("     , h.respondeuautorizacao");
                query.Append("     , c.TEMPOESTIMADOMIN");

                query.Append("  From Niff_Chm_Chamado c, Niff_CHM_Usuarios ua, Niff_Chm_Parametros p   ");
                query.Append("     , Niff_Chm_Categorias CT, Niff_Chm_Histochamado h, Niff_Chm_Histochamado h2, Niff_CHM_Usuarios u");

                query.Append(" Where c.idUsuario = ua.IdUsuario    ");
                query.Append("   and ct.Idcategoria = c.idcategoria");
                query.Append("   and c.idchamadoagrupado Is Null   ");

                if (ano != "")
                {
                    if (DateTime.Now.Month < 3 && ano == DateTime.Now.Year.ToString())
                        query.Append("   and trunc(c.Data) between To_date('31/07/2019','dd/mm/yyyy') and to_date('" + DateTime.Now.Date.ToShortDateString() + "','dd/mm/yyyy')");
                    else
                        query.Append("   and To_char(c.Data,'yyyy') = '" + ano + "'");
                }

                if (status == "A") // aberto e em andamento
                    query.Append("   and c.Status not in ('F','C')");
                else
                {
                    if (status == "F" || status == "S") // Finalizados ou sem avaliacao
                    {
                        query.Append("   and c.Status = 'F'");
                        if (status == "S")
                            query.Append("   and (Nvl(c.Avaliacao,0) = 0 Or nvl(Avaliacaosolicitante,0) = 0) ");
                    }
                    else
                    {
                        if (status == "C")
                            query.Append("   and c.Status = 'C'");
                    }
                }

                query.Append("   And (ct.idcategoria In (Select idCategoria From Niff_Chm_Categautousuario Where Idusuario = " + Publicas._idUsuario + ")");
                query.Append("    or c.Idusuario = " + Publicas._usuario.Id + ")");

                if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente &&
                    Publicas._usuario.IdEmpresa != 19 && Publicas._usuario.UsuarioAcesso != "ELSILVA")
                    query.Append("   And c.IdEmpresa = " + Publicas._usuario.IdEmpresa);

                query.Append("   And c.Idchamado = h.Idchamado");
                query.Append("   And h.Privado = 'N'");
                query.Append("   And h.respondeuautorizacao = 'S'");
                query.Append("   And H.Idusuarioautorizacao = u.IdUsuario ");
                query.Append("   And h.Idhistorico = (Select Max(Idhistorico) From Niff_Chm_Histochamado Where idChamado = c.idchamado and privado = 'N')");
                query.Append("   And c.Idchamado = h2.Idchamado");

                query.Append("   And h2.Idusuario = " + Publicas._usuario.Id);
                query.Append("   And h2.Privado = 'N'");

                query.Append("Group By c.IDCHAMADO ");
                query.Append("    , c.IDUSUARIO ");
                query.Append("    , c.IDCATEGORIA ");
                query.Append("    , c.IDTELA ");
                query.Append("    , c.ASSUNTO ");
                query.Append("    , c.ASSUNTO ");
                query.Append("    , c.IDEMPRESA ");
                query.Append("    , c.DATA ");
                query.Append("    , c.NUMERO ");
                query.Append("    , c.STATUS ");
                query.Append("    , c.ORIGEM ");
                query.Append("    , c.PRIORIDADE ");
                query.Append("    , c.NUMADEQFORN ");
                query.Append("    , c.DATAENTADEQ ");
                query.Append("    , c.AVALIACAO ");
                query.Append("    , c.TipoChamado ");
                query.Append("    , c.DescricaoAvaliacao ");
                query.Append("    , ua.Nome ");
                query.Append("    , ct.descricao ");
                query.Append("    , c.IDCHAMADOASSOCIADO ");
                query.Append("    , c.AtendenteFoiCortez ");
                query.Append("    , c.DataAvaliacaoDoSolicitante ");
                query.Append("    , c.SolicitanteAbriuCorretamente ");
                query.Append("    , c.SolicitanteRespDentroDePrazo ");
                query.Append("    , c.SolicitanteFoiCortez ");
                query.Append("    , c.AvaliacaoSolicitante ");
                query.Append("    , c.DescricaoAvaliacaoSolic ");
                query.Append("    , p.qtddiasretornochamado ");
                query.Append("    , c.PRAZODESENVOLVIMENTO");
                query.Append("     , c.LembrarDentreDeDias         ");
                query.Append("     , c.MotivoLembrete              ");
                query.Append("     , h.Idusuarioautorizacao         ");
                query.Append("     , h.Idusuarioautorizacao        ");
                query.Append("     , h.motivoautorizacao           ");
                query.Append("     , h.autorizado                  ");
                query.Append("     , h.aguardarretornoauto         ");
                query.Append("     , c.Reavaliar                   ");
                query.Append("     , c.Reavaliado                   ");
                query.Append("     , c.IDEMPRESASOLICITANTE");
                query.Append("     , c.DataReavaliacao, u.Nome, c.TrocouCategoria                    ");
                query.Append("     , h.respondeuautorizacao");
                query.Append("     , c.TEMPOESTIMADOMIN");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    while (chamadoReader.Read())
                    {
                        _chamado = PopulaChamado(chamadoReader);

                        _chamado.IdUsuarioAutorizacao = Convert.ToInt32(chamadoReader["Idusuarioautorizacao"].ToString());
                        _chamado.MotivoNegacaoDaAutorizacao = chamadoReader["motivoautorizacao"].ToString();
                        _chamado.Autorizado = chamadoReader["autorizado"].ToString() == "S";
                        _chamado.AguardarAutorizado = chamadoReader["aguardarretornoauto"].ToString() == "S";
                        _chamado.RespondeuAutorizacao = chamadoReader["RespondeuAutorizacao"].ToString() == "S";


                        if (_lista.Where(w => w.IdChamado == _chamado.IdChamado).Count() == 0)
                        {
                            _lista.Add(_chamado);
                        }
                        else
                            foreach (var item in _lista.Where(w => w.IdChamado == _chamado.IdChamado))
                            {
                                item.RespondeuAutorizacao = _chamado.RespondeuAutorizacao;
                                item.IdUsuarioAutorizacao = _chamado.IdUsuarioAutorizacao;
                                item.Autorizado = _chamado.Autorizado;
                                item.AguardarAutorizado = _chamado.AguardarAutorizado;
                            }

                    }
                }

                //using (chamadoReader2)
                //{
                //    while (chamadoReader2.Read())
                //    {
                //        _chamado = PopulaChamado(chamadoReader2);
                //        _chamado.IdUsuarioAutorizacao = Convert.ToInt32(chamadoReader2["Idusuarioautorizacao"].ToString());
                //        _chamado.MotivoNegacaoDaAutorizacao = chamadoReader2["motivoautorizacao"].ToString();
                //        _chamado.Autorizado = chamadoReader2["autorizado"].ToString() == "S";
                //        _chamado.AguardarAutorizado = chamadoReader2["aguardarretornoauto"].ToString() == "S";

                //    }

                //    if (_chamado.Existe)
                //    {
                //        if ((!_chamado.Autorizado && !_chamado.AguardarAutorizado && string.IsNullOrEmpty(_chamado.MotivoNegacaoDaAutorizacao)) ||
                //           (!_chamado.Autorizado && _chamado.AguardarAutorizado))
                //        {
                //            _lista.IdUsuarioAutorizacao = _chamado.IdUsuarioAutorizacao;
                //            _lista.MotivoNegacaoDaAutorizacao = _chamado.MotivoNegacaoDaAutorizacao;
                //            _lista.Autorizado = _chamado.Autorizado;
                //            _lista.AguardarAutorizado = _chamado.AguardarAutorizado;
                //            _lista.Exibir = true;
                //        }
                //        else
                //        {
                //            _lista.Exibir = false;
                //        }
                //    }
                //}               
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

        public List<Chamado> ChamadosParaAcompanhar(List<Chamado> _lista, string status = "T", string ano = "")
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();

            Publicas.mensagemDeErro = string.Empty;
            Chamado _chamado = new Chamado();
            try
            {
                query.Clear();
                query.Append("Select c.IDCHAMADO    ");
                query.Append("     , c.IDUSUARIO    ");
                query.Append("     , c.IDCATEGORIA  ");
                query.Append("     , c.IDTELA       ");
                query.Append("     , c.ASSUNTO      ");
                query.Append("     , c.ASSUNTO      ");
                query.Append("     , c.IDEMPRESA    ");
                query.Append("     , c.DATA         ");
                query.Append("     , c.NUMERO       ");
                query.Append("     , c.STATUS       ");
                query.Append("     , c.ORIGEM       ");
                query.Append("     , c.PRIORIDADE   ");
                query.Append("     , c.NUMADEQFORN  ");
                query.Append("     , c.DATAENTADEQ  ");
                query.Append("     , c.AVALIACAO    ");
                query.Append("     , c.TipoChamado  ");
                query.Append("     , c.DescricaoAvaliacao          ");
                query.Append("     , ua.Nome UsuarioAbertura       ");
                query.Append("     , ct.descricao categoria        ");
                query.Append("     , c.IDCHAMADOASSOCIADO          ");
                query.Append("     , c.AtendenteFoiCortez          ");
                query.Append("     , c.DataAvaliacaoDoSolicitante  ");
                query.Append("     , c.SolicitanteAbriuCorretamente");
                query.Append("     , c.SolicitanteRespDentroDePrazo");
                query.Append("     , c.SolicitanteFoiCortez        ");
                query.Append("     , c.AvaliacaoSolicitante        ");
                query.Append("     , c.DescricaoAvaliacaoSolic     ");
                query.Append("     , p.qtddiasretornochamado dias  ");
                query.Append("     , c.PRAZODESENVOLVIMENTO        ");
                query.Append("     , c.LembrarDentreDeDias         ");
                query.Append("     , c.MotivoLembrete              "); 
                query.Append("     , 0 Idusuarioautorizacao        ");
                query.Append("     , 'N' motivoautorizacao           ");
                query.Append("     , 'N' autorizado                  ");
                query.Append("     , 'N' aguardarretornoauto         ");
                query.Append("     , c.Reavaliar                   ");
                query.Append("     , c.Reavaliado                   ");
                query.Append("     , c.DataReavaliacao                   ");
                query.Append("     , c.IDEMPRESASOLICITANTE");
                query.Append("     , c.TrocouCategoria");
                query.Append("     , (Select Data");
                query.Append("          From niff_chm_lembretechamados l");
                query.Append("         Where l.Idchamado = c.IdChamado");
                query.Append("           And trunc(sysdate) = Data) Lembrete");
                query.Append("     , Idusuarioacompanhamento");
                query.Append("     , c.TEMPOESTIMADOMIN");

                query.Append("  From Niff_Chm_Chamado c, Niff_CHM_Usuarios ua, Niff_Chm_Parametros p   ");
                query.Append("     , Niff_Chm_Categorias CT");

                query.Append(" Where c.idUsuario = ua.IdUsuario    ");
                query.Append("   and ct.Idcategoria = c.idcategoria");
                query.Append("   and c.idchamadoagrupado Is Null   ");

                query.Append("   And c.Idusuarioacompanhamento = " + Publicas._usuario.Id);

                if (ano != "")
                {
                    if (DateTime.Now.Month < 3 && ano == DateTime.Now.Year.ToString())
                        query.Append("   and trunc(c.Data) between To_date('31/07/2019','dd/mm/yyyy') and to_date('" + DateTime.Now.Date.ToShortDateString() + "','dd/mm/yyyy')");
                    else
                        query.Append("   and To_char(c.Data,'yyyy') = '" + ano + "'");
                }
                
                if (status == "A") // aberto e em andamento
                    query.Append("   and c.Status not in ('F','C')");
                else
                {
                    if (status == "F" || status == "S") // Finalizados ou sem avaliacao
                    {
                        query.Append("   and c.Status = 'F'");
                        if (status == "S")
                            query.Append("   and (Nvl(c.Avaliacao,0) = 0 Or nvl(Avaliacaosolicitante,0) = 0) ");
                    }
                    else
                    {
                        if (status == "C")
                            query.Append("   and c.Status = 'C'");
                    }
                }

                query.Append("Group By c.IDCHAMADO ");
                query.Append("    , c.IDUSUARIO ");
                query.Append("    , c.IDCATEGORIA ");
                query.Append("    , c.IDTELA ");
                query.Append("    , c.ASSUNTO ");
                query.Append("    , c.ASSUNTO ");
                query.Append("    , c.IDEMPRESA ");
                query.Append("    , c.DATA ");
                query.Append("    , c.NUMERO ");
                query.Append("    , c.STATUS ");
                query.Append("    , c.ORIGEM ");
                query.Append("    , c.PRIORIDADE ");
                query.Append("    , c.NUMADEQFORN ");
                query.Append("    , c.DATAENTADEQ ");
                query.Append("    , c.AVALIACAO ");
                query.Append("    , c.TipoChamado ");
                query.Append("    , c.DescricaoAvaliacao ");
                query.Append("    , ua.Nome ");
                query.Append("    , ct.descricao ");
                query.Append("    , c.IDCHAMADOASSOCIADO ");
                query.Append("    , c.AtendenteFoiCortez ");
                query.Append("    , c.DataAvaliacaoDoSolicitante ");
                query.Append("    , c.SolicitanteAbriuCorretamente ");
                query.Append("    , c.SolicitanteRespDentroDePrazo ");
                query.Append("    , c.SolicitanteFoiCortez ");
                query.Append("    , c.AvaliacaoSolicitante ");
                query.Append("    , c.DescricaoAvaliacaoSolic ");
                query.Append("    , p.qtddiasretornochamado ");
                query.Append("    , c.PRAZODESENVOLVIMENTO");
                query.Append("     , c.LembrarDentreDeDias         ");
                query.Append("     , c.MotivoLembrete              ");
                query.Append("     , c.Reavaliar                   ");
                query.Append("     , c.Reavaliado                   ");
                query.Append("     , c.DataReavaliacao              ");
                query.Append("     , c.IDEMPRESASOLICITANTE");
                query.Append("     , c.TrocouCategoria");
                query.Append("     , Idusuarioacompanhamento");
                query.Append("     , c.TEMPOESTIMADOMIN");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader2 = executar.ExecuteQuery();

                using (chamadoReader2)
                {
                    while (chamadoReader2.Read())
                    {
                        _chamado = PopulaChamado(chamadoReader2);

                        if (_lista.Where(w => w.IdChamado == _chamado.IdChamado).Count() == 0)
                        {
                            _lista.Add(_chamado);
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
            return _lista;
        }

        public List<Chamado> Listar(int departamento = 0, bool apenasDoUsuario = false, string status = "T", string ano = "")
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Chamado> _lista = new List<Chamado>();
            HistoricoDoChamado _historico = new HistoricoDoChamado();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select IDCHAMADO    ");
                query.Append("     , IDUSUARIO    ");
                query.Append("     , IDCATEGORIA  ");
                query.Append("     , IDTELA       ");
                query.Append("     , ASSUNTO      ");
                query.Append("     , IDEMPRESA    ");
                query.Append("     , DATA         ");
                query.Append("     , NUMERO       ");
                query.Append("     , STATUS       ");
                query.Append("     , ORIGEM       ");
                query.Append("     , PRIORIDADE   ");
                query.Append("     , NUMADEQFORN  ");
                query.Append("     , DATAENTADEQ  ");
                query.Append("     , AVALIACAO    ");
                query.Append("     , TipoChamado  ");
                query.Append("     , DescricaoAvaliacao          ");
                query.Append("     , UsuarioAbertura       ");
                query.Append("     , Categoria        ");
                query.Append("     , IDCHAMADOASSOCIADO          ");
                query.Append("     , AtendenteFoiCortez          ");
                query.Append("     , DataAvaliacaoDoSolicitante  ");
                query.Append("     , SolicitanteAbriuCorretamente");
                query.Append("     , SolicitanteRespDentroDePrazo");
                query.Append("     , SolicitanteFoiCortez        ");
                query.Append("     , AvaliacaoSolicitante        ");
                query.Append("     , DescricaoAvaliacaoSolic     ");
                query.Append("     , Dias  ");
                query.Append("     , PRAZODESENVOLVIMENTO        ");
                query.Append("     , LembrarDentreDeDias         ");
                query.Append("     , MotivoLembrete              ");
                query.Append("     , Reavaliar                   ");
                query.Append("     , Reavaliado                   ");
                query.Append("     , DataReavaliacao                   ");
                query.Append("     , IdUsuarioAcompanhamento      ");
                query.Append("     , Lembrete");
                query.Append("     , IDEMPRESASOLICITANTE");
                query.Append("     , TrocouCategoria");

                query.Append("     , InicioTemp");
                query.Append("     , FimTemp");
                query.Append("     , Temp");
                query.Append("     , TempoCalc");
                query.Append("     , TEMPOESTIMADOMIN");
                query.Append(" From (");

                if (!apenasDoUsuario || Publicas._usuario.Id == 82) //(Claudia Support enxerga todas as empresas para os modulos dela)) // Todos
                {

                    query.Append("Select c.IDCHAMADO    ");
                    query.Append("     , c.IDUSUARIO    ");
                    query.Append("     , c.IDCATEGORIA  ");
                    query.Append("     , c.IDTELA       ");
                    query.Append("     , c.ASSUNTO      ");
                    query.Append("     , c.IDEMPRESA    ");
                    query.Append("     , c.DATA         ");
                    query.Append("     , c.NUMERO       ");
                    query.Append("     , c.STATUS       ");
                    query.Append("     , c.ORIGEM       ");
                    query.Append("     , c.PRIORIDADE   ");
                    query.Append("     , c.NUMADEQFORN  ");
                    query.Append("     , c.DATAENTADEQ  ");
                    query.Append("     , c.AVALIACAO    ");
                    query.Append("     , c.TipoChamado  ");
                    query.Append("     , c.DescricaoAvaliacao          ");
                    query.Append("     , ua.Nome UsuarioAbertura       ");
                    query.Append("     , ct.descricao categoria        ");
                    query.Append("     , c.IDCHAMADOASSOCIADO          ");
                    query.Append("     , c.AtendenteFoiCortez          ");
                    query.Append("     , c.DataAvaliacaoDoSolicitante  ");
                    query.Append("     , c.SolicitanteAbriuCorretamente");
                    query.Append("     , c.SolicitanteRespDentroDePrazo");
                    query.Append("     , c.SolicitanteFoiCortez        ");
                    query.Append("     , c.AvaliacaoSolicitante        ");
                    query.Append("     , c.DescricaoAvaliacaoSolic     ");
                    query.Append("     , p.qtddiasretornochamado dias  ");
                    query.Append("     , c.PRAZODESENVOLVIMENTO        ");
                    query.Append("     , c.LembrarDentreDeDias         ");
                    query.Append("     , c.MotivoLembrete              ");
                    query.Append("     , c.Reavaliar                   ");
                    query.Append("     , c.Reavaliado                   ");
                    query.Append("     , c.DataReavaliacao                   ");
                    query.Append("     , c.IdUsuarioAcompanhamento      ");
                    query.Append("     , c.IDEMPRESASOLICITANTE");
                    query.Append("     , c.TrocouCategoria");
                    query.Append("     , (Select Data");
                    query.Append("          From niff_chm_lembretechamados l");
                    query.Append("         Where l.Idchamado = c.IdChamado");
                    query.Append("           And trunc(sysdate) = Data) Lembrete");

                    query.Append("     ,  t.Datainicio InicioTemp");
                    query.Append("     ,  t.DataFim FimTemp");
                    query.Append("     ,  t.Tempomin Temp");
                    query.Append("     ,  t.TempoCalc");
                    query.Append("     , c.TEMPOESTIMADOMIN");

                    query.Append("  From Niff_Chm_Chamado c, Niff_CHM_Usuarios ua, Niff_Chm_Parametros p   ");
                    query.Append("     , Niff_Chm_Categorias CT");

                    query.Append("     , (Select dataInicio, Datafim, IdChamado, idUsuario, Sysdate");
                    query.Append("     ,         T.TEMPOMIN, Decode(DataFim, Null,trunc((mod(Sysdate - DataInicio, 1) * 24) *60), t.tempoMin) TempoCalc");
                    query.Append("          From Niff_Chm_Tempoexecucao t");
                    query.Append("         Where DataInicio = (Select Max(dataInicio) From Niff_Chm_Tempoexecucao M");
                    query.Append("                              Where m.idchamado = t.Idchamado");
                    query.Append("                                And m.idusuario = t.Idusuario");
                    query.Append("                              Group By IdChamado, idUsuario) ) T");

                    if (Publicas._usuario.Id == 82)
                    {
                        query.Append("     , niff_chm_telas t, niff_chm_modulos m");
                    }

                    query.Append(" Where c.idUsuario = ua.IdUsuario  ");
                    query.Append("   and ct.Idcategoria = c.idcategoria");

                    query.Append("   And c.idchamado = t.idchamado(+)"); // novo

                    if (status != "F" && status != "C")
                        query.Append("   and c.idchamadoagrupado Is Null   ");

                    if (ano != "")
                    {
                        if (DateTime.Now.Month < 3 && ano == DateTime.Now.Year.ToString())
                            query.Append("   and trunc(c.Data) between To_date('31/07/2019','dd/mm/yyyy') and to_date('" + DateTime.Now.Date.ToShortDateString() + "','dd/mm/yyyy')");
                        else
                            query.Append("   and To_char(c.Data,'yyyy') = '" + ano + "'");
                    }

                    if (status == "A") // aberto e em andamento
                        query.Append("   and c.Status not in ('F','C')");
                    else
                    {
                        if (status == "F" || status == "S") // Finalizados ou sem avaliacao
                        {
                            query.Append("   and c.Status = 'F'");
                            if (status == "S")
                                query.Append("   and (Nvl(c.Avaliacao,0) = 0 Or nvl(Avaliacaosolicitante,0) = 0) ");
                        }
                        else
                        {
                            if (status == "C")
                                query.Append("   and c.Status = 'C'");
                        }
                    }

                    if (departamento != 0)
                    {
                        if (Publicas._usuario.Id != 82)
                        {
                            query.Append("   And ua.IdEmpresa = " + Publicas._usuario.IdEmpresa);
                        }
                        else
                        {// para a Claudia da Support irá ver todas as empresas mas os Modulos de Folha, Frequencia e RH, para a empresa Support verá tudo do departamento
                            query.Append("   And t.idmodulo = m.Idmodulo");
                            query.Append("   And m.Idcategoria = c.idcategoria");
                            query.Append("   And ((c.idempresasolicitante <> 19 And m.Idmodulo In(103, 106, 113)) Or c.idempresasolicitante = 19 )");
                        }

                        query.Append("   and (ua.IdDepartamento = " + departamento);
                        query.Append("    or ua.Iddepartamento In (Select d.iddepartamento From niff_ads_colabdepartamento d Where d.idcolaborador = " + Publicas._idColaborador + ")");
                        query.Append("    or ua.Iddepartamento In (Select iddepartamento From Niff_Chm_Usuarios Where Idusuario In (Select Idusuario From Niff_Ads_Colabdepartamento Where Iddepartamento = " + Publicas._usuario.IdDepartamento + ")))");

                    }
                    else
                    {
                        query.Append("   And (ct.idcategoria In (Select idCategoria From Niff_Chm_Categautousuario Where Idusuario = " + Publicas._idUsuario + ")");
                        query.Append("    or c.Idusuario = " + Publicas._usuario.Id + ")");

                        if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente &&
                            Publicas._usuario.IdEmpresa != 19 && Publicas._usuario.UsuarioAcesso != "ELSILVA")
                            query.Append("   And c.IdEmpresa = " + Publicas._usuario.IdEmpresa);
                    }
                    

                    query.Append("    Order by IdChamado");
                }
                else
                {
                    // traz os status novo sem importar o usuário
                    query.Append("Select c.IDCHAMADO    ");
                    query.Append("     , c.IDUSUARIO    ");
                    query.Append("     , c.IDCATEGORIA  ");
                    query.Append("     , c.IDTELA       ");
                    query.Append("     , c.ASSUNTO      ");
                    query.Append("     , c.IDEMPRESA    ");
                    query.Append("     , c.DATA         ");
                    query.Append("     , c.NUMERO       ");
                    query.Append("     , c.STATUS       ");
                    query.Append("     , c.ORIGEM       ");
                    query.Append("     , c.PRIORIDADE   ");
                    query.Append("     , c.NUMADEQFORN  ");
                    query.Append("     , c.DATAENTADEQ  ");
                    query.Append("     , c.AVALIACAO    ");
                    query.Append("     , c.TipoChamado  ");
                    query.Append("     , c.DescricaoAvaliacao          ");
                    query.Append("     , ua.Nome UsuarioAbertura       ");
                    query.Append("     , ct.descricao categoria        ");
                    query.Append("     , c.IDCHAMADOASSOCIADO          ");
                    query.Append("     , c.AtendenteFoiCortez          ");
                    query.Append("     , c.DataAvaliacaoDoSolicitante  ");
                    query.Append("     , c.SolicitanteAbriuCorretamente");
                    query.Append("     , c.SolicitanteRespDentroDePrazo");
                    query.Append("     , c.SolicitanteFoiCortez        ");
                    query.Append("     , c.AvaliacaoSolicitante        ");
                    query.Append("     , c.DescricaoAvaliacaoSolic     ");
                    query.Append("     , p.qtddiasretornochamado dias  ");
                    query.Append("     , c.PRAZODESENVOLVIMENTO        ");
                    query.Append("     , c.LembrarDentreDeDias         ");
                    query.Append("     , c.MotivoLembrete              ");
                    query.Append("     , c.Reavaliar                   ");
                    query.Append("     , c.Reavaliado                   ");
                    query.Append("     , c.DataReavaliacao                   ");
                    query.Append("     , c.IdUsuarioAcompanhamento      ");
                    query.Append("     , c.IDEMPRESASOLICITANTE");
                    query.Append("     , c.TrocouCategoria");
                    query.Append("     , (Select Data");
                    query.Append("          From niff_chm_lembretechamados l");
                    query.Append("         Where l.Idchamado = c.IdChamado");
                    query.Append("           And trunc(sysdate) = Data) Lembrete");

                    query.Append("     ,  null InicioTemp");
                    query.Append("     ,  Null FimTemp");
                    query.Append("     ,  0 Temp");
                    query.Append("     ,  0 TempoCalc");
                    query.Append("     , c.TEMPOESTIMADOMIN");

                    query.Append("  From Niff_Chm_Chamado c, Niff_CHM_Usuarios ua, Niff_Chm_Parametros p   ");
                    query.Append("     , Niff_Chm_Categorias CT");

                    if (Publicas._usuario.Id == 82)
                    {
                        query.Append("     , niff_chm_telas t, niff_chm_modulos m");
                    }

                    query.Append(" Where c.idUsuario = ua.IdUsuario    ");
                    query.Append("   and ct.Idcategoria = c.idcategoria");
                    if (status != "F" && status != "C")
                        query.Append("   and c.idchamadoagrupado Is Null   ");
                    query.Append("   and c.Status = 'N'");
                    query.Append("   And c.Idusuario != " + Publicas._usuario.Id);

                    if (departamento != 0)
                    {
                        if (Publicas._usuario.Id != 82)
                        {
                            query.Append("   And ua.IdEmpresa = " + Publicas._usuario.IdEmpresa);
                        }
                        else
                        {// para a Claudia da Support irá ver todas as empresas mas os Modulos de Folha, Frequencia e RH, para a empresa Support verá tudo do departamento
                            query.Append("   and t.idmodulo = m.Idmodulo");
                            query.Append("   and m.Idcategoria = c.idcategoria");
                            query.Append("   And ((c.idempresasolicitante <> 19 And m.Idmodulo In(103, 106, 113)) Or c.idempresasolicitante = 19 )");
                        }
                        query.Append("   and (ua.IdDepartamento = " + departamento);
                        query.Append("    or ua.Iddepartamento In (Select d.iddepartamento From niff_ads_colabdepartamento d Where d.idcolaborador = " + Publicas._idColaborador + ")");
                        query.Append("    or ua.Iddepartamento In (Select iddepartamento From Niff_Chm_Usuarios Where Idusuario In (Select Idusuario From Niff_Ads_Colabdepartamento Where Iddepartamento = " + Publicas._usuario.IdDepartamento + ")))");
                    }
                    else
                    {
                        query.Append("   And (ct.idcategoria In (Select idCategoria From Niff_Chm_Categautousuario Where Idusuario = " + Publicas._idUsuario + ")");
                        query.Append("    or c.Idusuario = " + Publicas._usuario.Id + ")");

                        if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && 
                            Publicas._usuario.IdEmpresa != 19 && Publicas._usuario.UsuarioAcesso != "ELSILVA")
                            query.Append("   And c.IdEmpresa = " + Publicas._usuario.IdEmpresa);

                    }

                    if (ano != "")
                    {
                        if (DateTime.Now.Month < 3 && ano == DateTime.Now.Year.ToString())
                            query.Append("   and trunc(c.Data) between To_date('31/07/2019','dd/mm/yyyy') and to_date('" + DateTime.Now.Date.ToShortDateString() + "','dd/mm/yyyy')");
                        else
                            query.Append("   and To_char(c.Data,'yyyy') = '" + ano + "'");
                    }


                    if (status == "A") // aberto e em andamento
                        query.Append("   and c.Status not in ('F','C')");
                    else
                    {
                        if (status == "F" || status == "S") // Finalizados ou sem avaliacao
                        {
                            query.Append("   and c.Status = 'F'");
                            if (status == "S")
                                query.Append("   and (Nvl(c.Avaliacao,0) = 0 Or nvl(Avaliacaosolicitante,0) = 0) ");
                        }
                        else
                        {
                            if (status == "C")
                                query.Append("   and c.Status = 'C'");
                        }
                    }

                    query.Append(" Union all ");

                    query.Append("Select c.IDCHAMADO    ");
                    query.Append("     , c.IDUSUARIO    ");
                    query.Append("     , c.IDCATEGORIA  ");
                    query.Append("     , c.IDTELA       ");
                    query.Append("     , c.ASSUNTO      ");
                    query.Append("     , c.IDEMPRESA    ");
                    query.Append("     , c.DATA         ");
                    query.Append("     , c.NUMERO       ");
                    query.Append("     , c.STATUS       ");
                    query.Append("     , c.ORIGEM       ");
                    query.Append("     , c.PRIORIDADE   ");
                    query.Append("     , c.NUMADEQFORN  ");
                    query.Append("     , c.DATAENTADEQ  ");
                    query.Append("     , c.AVALIACAO    ");
                    query.Append("     , c.TipoChamado  ");
                    query.Append("     , c.DescricaoAvaliacao          ");
                    query.Append("     , ua.Nome UsuarioAbertura       ");
                    query.Append("     , ct.descricao categoria        ");
                    query.Append("     , c.IDCHAMADOASSOCIADO          ");
                    query.Append("     , c.AtendenteFoiCortez          ");
                    query.Append("     , c.DataAvaliacaoDoSolicitante  ");
                    query.Append("     , c.SolicitanteAbriuCorretamente");
                    query.Append("     , c.SolicitanteRespDentroDePrazo");
                    query.Append("     , c.SolicitanteFoiCortez        ");
                    query.Append("     , c.AvaliacaoSolicitante        ");
                    query.Append("     , c.DescricaoAvaliacaoSolic     ");
                    query.Append("     , p.qtddiasretornochamado dias  ");
                    query.Append("     , c.PRAZODESENVOLVIMENTO        ");
                    query.Append("     , c.LembrarDentreDeDias         ");
                    query.Append("     , c.MotivoLembrete              ");
                    query.Append("     , c.Reavaliar                   ");
                    query.Append("     , c.Reavaliado                   ");
                    query.Append("     , c.DataReavaliacao                   ");
                    query.Append("     , c.IdUsuarioAcompanhamento      ");
                    query.Append("     , c.IDEMPRESASOLICITANTE");
                    query.Append("     , c.TrocouCategoria");
                    query.Append("     , (Select Data");
                    query.Append("          From niff_chm_lembretechamados l");
                    query.Append("         Where l.Idchamado = c.IdChamado");
                    query.Append("           And trunc(sysdate) = Data) Lembrete");

                    query.Append("     ,  t.Datainicio InicioTemp");
                    query.Append("     ,  t.DataFim FimTemp");
                    query.Append("     ,  t.Tempomin Temp");
                    query.Append("     ,  t.TempoCalc");
                    query.Append("     , c.TEMPOESTIMADOMIN");

                    query.Append("  From Niff_Chm_Chamado c, Niff_CHM_Usuarios ua, Niff_Chm_Parametros p   ");
                    query.Append("     , Niff_Chm_Categorias CT, Niff_Chm_Histochamado h");


                    query.Append("     , (Select dataInicio, Datafim, IdChamado, idUsuario, Sysdate");
                    query.Append("             , T.TEMPOMIN, Decode(DataFim, Null,trunc((mod(Sysdate - DataInicio, 1) * 24) *60), t.tempoMin) TempoCalc");
                    query.Append("          From Niff_Chm_Tempoexecucao t");
                    query.Append("         Where DataInicio = (Select Max(dataInicio) From Niff_Chm_Tempoexecucao M");
                    query.Append("                              Where m.idchamado = t.Idchamado");
                    query.Append("                                And m.idusuario = t.Idusuario");
                    query.Append("                              Group By IdChamado, idUsuario) ) T");

                    if (Publicas._usuario.Id == 82)
                    {
                        query.Append("     , niff_chm_telas t, niff_chm_modulos m");
                    }

                    query.Append(" Where c.idUsuario = ua.IdUsuario  ");
                    query.Append("   and ct.Idcategoria = c.idcategoria");
                    
                    query.Append("   And c.idchamado = t.idchamado(+)"); // novo

                    if (status != "F" && status != "C") 
                        query.Append("   and c.idchamadoagrupado Is Null   ");
                    query.Append("   And c.Idchamado = h.Idchamado");

                    query.Append("   And h.Idusuario = " + Publicas._usuario.Id );
                    query.Append("   And h.Privado = 'N'");

                    if (departamento != 0)
                    {
                        if (Publicas._usuario.Id != 82)
                        {
                            query.Append("   And ua.IdEmpresa = " + Publicas._usuario.IdEmpresa);
                        }
                        else
                        {  // para a Claudia da Support irá ver todas as empresas mas os Modulos de Folha, Frequencia e RH, para a empresa Support verá tudo do departamento
                            query.Append("   and t.idmodulo = m.Idmodulo");
                            query.Append("   and m.Idcategoria = c.idcategoria");
                            query.Append("   And ((c.idempresasolicitante <> 19 And m.Idmodulo In(103, 106, 113)) Or c.idempresasolicitante = 19 )");
                        }
                        query.Append("   and (ua.IdDepartamento = " + departamento);
                        query.Append("    or ua.Iddepartamento In (Select d.iddepartamento From niff_ads_colabdepartamento d Where d.idcolaborador = " + Publicas._idColaborador + ")");
                        query.Append("    or ua.Iddepartamento In (Select iddepartamento From Niff_Chm_Usuarios Where Idusuario In (Select Idusuario From Niff_Ads_Colabdepartamento Where Iddepartamento = " + Publicas._usuario.IdDepartamento + ")))");
                    }
                    else
                    {
                        query.Append("   And (ct.idcategoria In (Select idCategoria From Niff_Chm_Categautousuario Where Idusuario = " + Publicas._idUsuario + ")");
                        query.Append("    or c.Idusuario = " + Publicas._usuario.Id + ")");

                        if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && 
                            Publicas._usuario.IdEmpresa != 19 && Publicas._usuario.UsuarioAcesso != "ELSILVA")
                            query.Append("   And c.IdEmpresa = " + Publicas._usuario.IdEmpresa);
                    }

                    if (ano != "")
                    {
                        if (DateTime.Now.Month < 3 && ano == DateTime.Now.Year.ToString())
                            query.Append("   and trunc(h.Data) between To_date('31/07/2019','dd/mm/yyyy') and to_date('" + DateTime.Now.Date.ToShortDateString() + "','dd/mm/yyyy')");
                        else
                            query.Append("   and To_char(h.Data,'yyyy') = '" + ano + "'");
                    }

                    if (status == "A") // aberto e em andamento
                        query.Append("   and c.Status not in ('F','C')");
                    else
                    {
                        if (status == "F" || status == "S") // Finalizados ou sem avaliacao
                        {
                            query.Append("   and c.Status = 'F'");
                            if (status == "S")
                                query.Append("   and (Nvl(c.Avaliacao,0) = 0 Or nvl(Avaliacaosolicitante,0) = 0) ");
                        }
                        else
                        {
                            if (status == "C")
                                query.Append("   and c.Status = 'C'");
                        }
                    }

                    query.Append("Group By c.IDCHAMADO ");
                    query.Append("    , c.IDUSUARIO ");
                    query.Append("    , c.IDCATEGORIA ");
                    query.Append("    , c.IDTELA ");
                    query.Append("    , c.ASSUNTO ");
                    query.Append("    , c.IDEMPRESA ");
                    query.Append("    , c.DATA ");
                    query.Append("    , c.NUMERO ");
                    query.Append("    , c.STATUS ");
                    query.Append("    , c.ORIGEM ");
                    query.Append("    , c.PRIORIDADE ");
                    query.Append("    , c.NUMADEQFORN ");
                    query.Append("    , c.DATAENTADEQ ");
                    query.Append("    , c.AVALIACAO ");
                    query.Append("    , c.TipoChamado ");
                    query.Append("    , c.DescricaoAvaliacao ");
                    query.Append("    , ua.Nome ");
                    query.Append("    , ct.descricao ");
                    query.Append("    , c.IDCHAMADOASSOCIADO ");
                    query.Append("    , c.AtendenteFoiCortez ");
                    query.Append("    , c.DataAvaliacaoDoSolicitante ");
                    query.Append("    , c.SolicitanteAbriuCorretamente ");
                    query.Append("    , c.SolicitanteRespDentroDePrazo ");
                    query.Append("    , c.SolicitanteFoiCortez ");
                    query.Append("    , c.AvaliacaoSolicitante ");
                    query.Append("    , c.DescricaoAvaliacaoSolic ");
                    query.Append("    , p.qtddiasretornochamado ");
                    query.Append("    , c.PRAZODESENVOLVIMENTO");
                    query.Append("     , c.LembrarDentreDeDias         ");
                    query.Append("     , c.MotivoLembrete              ");
                    query.Append("     , c.Reavaliar                   ");
                    query.Append("     , c.Reavaliado                   ");
                    query.Append("     , c.DataReavaliacao                   ");
                    query.Append("     , c.IdUsuarioAcompanhamento      ");
                    query.Append("     , c.IDEMPRESASOLICITANTE");
                    query.Append("     , c.TrocouCategoria");

                    query.Append("     ,  t.Datainicio ");
                    query.Append("     ,  t.DataFim ");
                    query.Append("     ,  t.Tempomin ");
                    query.Append("     ,  t.TempoCalc");
                    query.Append("     , c.TEMPOESTIMADOMIN");
                    query.Append("    Order by IdChamado");
                }

                query.Append(") Group by IDCHAMADO    ");
                query.Append("     , IDUSUARIO    ");
                query.Append("     , IDCATEGORIA  ");
                query.Append("     , IDTELA       ");
                query.Append("     , ASSUNTO      ");
                query.Append("     , IDEMPRESA    ");
                query.Append("     , DATA         ");
                query.Append("     , NUMERO       ");
                query.Append("     , STATUS       ");
                query.Append("     , ORIGEM       ");
                query.Append("     , PRIORIDADE   ");
                query.Append("     , NUMADEQFORN  ");
                query.Append("     , DATAENTADEQ  ");
                query.Append("     , AVALIACAO    ");
                query.Append("     , TipoChamado  ");
                query.Append("     , DescricaoAvaliacao          ");
                query.Append("     , UsuarioAbertura       ");
                query.Append("     , Categoria        ");
                query.Append("     , IDCHAMADOASSOCIADO          ");
                query.Append("     , AtendenteFoiCortez          ");
                query.Append("     , DataAvaliacaoDoSolicitante  ");
                query.Append("     , SolicitanteAbriuCorretamente");
                query.Append("     , SolicitanteRespDentroDePrazo");
                query.Append("     , SolicitanteFoiCortez        ");
                query.Append("     , AvaliacaoSolicitante        ");
                query.Append("     , DescricaoAvaliacaoSolic     ");
                query.Append("     , Dias  ");
                query.Append("     , PRAZODESENVOLVIMENTO        ");
                query.Append("     , LembrarDentreDeDias         ");
                query.Append("     , MotivoLembrete              ");
                query.Append("     , Reavaliar                   ");
                query.Append("     , Reavaliado                   ");
                query.Append("     , DataReavaliacao                   ");
                query.Append("     , IdUsuarioAcompanhamento      ");
                query.Append("     , IDEMPRESASOLICITANTE");
                query.Append("     , TrocouCategoria");
                query.Append("     , Lembrete");
                query.Append("     , InicioTemp");
                query.Append("     , FimTemp");
                query.Append("     , Temp");
                query.Append("     , TempoCalc");
                query.Append("     , TEMPOESTIMADOMIN");
                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();
                 
                using (chamadoReader)
                {
                    while (chamadoReader.Read())
                    {
                        Chamado _chamado = PopulaChamado(chamadoReader);
                                                
                        //_chamado = ChamadosParaAutorizar(_chamado, _chamado.IdChamado);
                        _lista.Add(_chamado);
                    }
                }

                _lista = ChamadosParaAutorizar(_lista, status, ano);
                _lista = ChamadosParaAcompanhar(_lista, status, ano);
                _lista = ChamadosParaRespondidos(_lista, status, ano);
                
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

        public List<Chamado> Pesquisar(string pesquisa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Chamado> _lista = new List<Chamado>();
            HistoricoDoChamado _historico = new HistoricoDoChamado();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select c.IDCHAMADO    ");
                query.Append("     , c.IDUSUARIO    ");
                query.Append("     , c.IDCATEGORIA  ");
                query.Append("     , c.IDTELA       ");
                query.Append("     , c.ASSUNTO      ");
                query.Append("     , c.IDEMPRESA    ");
                query.Append("     , c.DATA         ");
                query.Append("     , c.NUMERO       ");
                query.Append("     , c.STATUS       ");
                query.Append("     , c.ORIGEM       ");
                query.Append("     , c.PRIORIDADE   ");
                query.Append("     , c.NUMADEQFORN  ");
                query.Append("     , c.DATAENTADEQ  ");
                query.Append("     , c.AVALIACAO    ");
                query.Append("     , c.TipoChamado  ");
                query.Append("     , c.DescricaoAvaliacao          ");
                query.Append("     , ua.Nome UsuarioAbertura       ");
                query.Append("     , ct.descricao categoria        ");
                query.Append("     , c.IDCHAMADOASSOCIADO          ");
                query.Append("     , c.AtendenteFoiCortez          ");
                query.Append("     , c.DataAvaliacaoDoSolicitante  ");
                query.Append("     , c.SolicitanteAbriuCorretamente");
                query.Append("     , c.SolicitanteRespDentroDePrazo");
                query.Append("     , c.SolicitanteFoiCortez        ");
                query.Append("     , c.AvaliacaoSolicitante        ");
                query.Append("     , c.DescricaoAvaliacaoSolic     ");
                query.Append("     , p.qtddiasretornochamado dias  ");
                query.Append("     , c.PRAZODESENVOLVIMENTO        ");
                query.Append("     , c.LembrarDentreDeDias         ");
                query.Append("     , c.MotivoLembrete              ");
                query.Append("     , c.Reavaliar                   ");
                query.Append("     , c.Reavaliado                   ");
                query.Append("     , c.DataReavaliacao                   ");
                query.Append("     , c.IdUsuarioAcompanhamento      ");
                query.Append("     , c.IDEMPRESASOLICITANTE");
                query.Append("     , c.TrocouCategoria");
                query.Append("     , (Select Data");
                query.Append("          From niff_chm_lembretechamados l");
                query.Append("         Where l.Idchamado = c.IdChamado");
                query.Append("           And trunc(sysdate) = Data) Lembrete");
                query.Append("     , c.TEMPOESTIMADOMIN");

                query.Append("  From Niff_Chm_Chamado c, Niff_CHM_Usuarios ua, Niff_Chm_Parametros p   ");
                query.Append("     , Niff_Chm_Categorias CT, NIFF_CHM_HISTOCHAMADO H");

                query.Append(" Where c.idUsuario = ua.IdUsuario  ");
                query.Append("   and ct.Idcategoria = c.idcategoria");
                query.Append("   and h.idChamado = c.idChamado");

                if (Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente && 
                    Publicas._usuario.IdEmpresa != 19 && Publicas._usuario.UsuarioAcesso != "ELSILVA")
                    query.Append("   And c.IdEmpresa = " + Publicas._usuario.IdEmpresa);

                query.Append("   and (c.numero = '" + pesquisa + "'");
                query.Append("    or Upper(c.Assunto) Like '%" + pesquisa.ToUpper() + "%'");
                query.Append("    or Upper(ua.Nome) Like '%" + pesquisa.ToUpper() + "%'");
                query.Append("    or Upper(C.numadeqforn) Like '%" + pesquisa.ToUpper() + "%'");
                query.Append("    or upper(h.descricao) Like '%" + pesquisa.ToUpper() + "%'");
                query.Append("   )");

                query.Append("Group By c.IDCHAMADO ");
                query.Append("    , c.IDUSUARIO ");
                query.Append("    , c.IDCATEGORIA ");
                query.Append("    , c.IDTELA ");
                query.Append("    , c.ASSUNTO ");
                query.Append("    , c.IDEMPRESA ");
                query.Append("    , c.DATA ");
                query.Append("    , c.NUMERO ");
                query.Append("    , c.STATUS ");
                query.Append("    , c.ORIGEM ");
                query.Append("    , c.PRIORIDADE ");
                query.Append("    , c.NUMADEQFORN ");
                query.Append("    , c.DATAENTADEQ ");
                query.Append("    , c.AVALIACAO ");
                query.Append("    , c.TipoChamado ");
                query.Append("    , c.DescricaoAvaliacao ");
                query.Append("    , ua.Nome ");
                query.Append("    , ct.descricao ");
                query.Append("    , c.IDCHAMADOASSOCIADO ");
                query.Append("    , c.AtendenteFoiCortez ");
                query.Append("    , c.DataAvaliacaoDoSolicitante ");
                query.Append("    , c.SolicitanteAbriuCorretamente ");
                query.Append("    , c.SolicitanteRespDentroDePrazo ");
                query.Append("    , c.SolicitanteFoiCortez ");
                query.Append("    , c.AvaliacaoSolicitante ");
                query.Append("    , c.DescricaoAvaliacaoSolic ");
                query.Append("    , p.qtddiasretornochamado ");
                query.Append("    , c.PRAZODESENVOLVIMENTO");
                query.Append("     , c.LembrarDentreDeDias         ");
                query.Append("     , c.MotivoLembrete              ");
                query.Append("     , c.Reavaliar                   ");
                query.Append("     , c.Reavaliado                   ");
                query.Append("     , c.DataReavaliacao                   ");
                query.Append("     , c.IdUsuarioAcompanhamento      ");
                query.Append("     , c.IDEMPRESASOLICITANTE");
                query.Append("     , c.TrocouCategoria");
                query.Append("     , c.TEMPOESTIMADOMIN");


                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    while (chamadoReader.Read())
                    {
                        Chamado _chamado = PopulaChamado(chamadoReader);

                        _lista.Add(_chamado);
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

        public Chamado Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            HistoricoDoChamado _historico = new HistoricoDoChamado();
            Publicas.mensagemDeErro = string.Empty;
            Chamado _chamado = new Chamado();

            try
            {

                query.Append("Select c.IDCHAMADO    ");
                query.Append("     , c.IDUSUARIO    ");
                query.Append("     , c.IDCATEGORIA  ");
                query.Append("     , c.IDTELA       ");
                query.Append("     , c.ASSUNTO      ");
                query.Append("     , c.IDEMPRESA    ");
                query.Append("     , c.DATA         ");
                query.Append("     , c.NUMERO       ");
                query.Append("     , c.STATUS       ");
                query.Append("     , c.ORIGEM       ");
                query.Append("     , c.PRIORIDADE   ");
                query.Append("     , c.NUMADEQFORN  ");
                query.Append("     , c.DATAENTADEQ  ");
                query.Append("     , c.AVALIACAO    ");
                query.Append("     , c.TipoChamado  ");
                query.Append("     , c.DescricaoAvaliacao          ");
                query.Append("     , ua.Nome UsuarioAbertura       ");
                query.Append("     , ct.descricao categoria        ");
                query.Append("     , c.IDCHAMADOASSOCIADO          ");
                query.Append("     , p.qtddiasretornochamado dias  ");
                query.Append("     , c.AtendenteFoiCortez          ");
                query.Append("     , c.DataAvaliacaoDoSolicitante  ");
                query.Append("     , c.SolicitanteAbriuCorretamente");
                query.Append("     , c.SolicitanteRespDentroDePrazo");
                query.Append("     , c.SolicitanteFoiCortez        ");
                query.Append("     , c.AvaliacaoSolicitante        ");
                query.Append("     , c.DescricaoAvaliacaoSolic     ");
                query.Append("     , c.PRAZODESENVOLVIMENTO        ");
                query.Append("     , c.LembrarDentreDeDias         ");
                query.Append("     , c.MotivoLembrete              ");
                query.Append("     , c.Reavaliar                   ");
                query.Append("     , c.Reavaliado                   ");
                query.Append("     , c.DataReavaliacao              ");
                query.Append("     , c.IdUsuarioAcompanhamento      ");
                query.Append("     , c.IDEMPRESASOLICITANTE");
                query.Append("     , c.TrocouCategoria");
                query.Append("     , (Select Data"); 
                query.Append("          From niff_chm_lembretechamados l");
                query.Append("         Where l.Idchamado = c.IdChamado");
                query.Append("           And trunc(sysdate) = Data) Lembrete");
                query.Append("     , c.TEMPOESTIMADOMIN");

                query.Append("  From Niff_Chm_Chamado c, Niff_CHM_Usuarios ua, Niff_Chm_Parametros p   ");
                query.Append("     , Niff_Chm_Categorias CT");

                query.Append(" Where c.idUsuario = ua.IdUsuario");
                query.Append("   and ct.Idcategoria = c.idcategoria");

                query.Append("   and c.IdChamado = " + codigo );

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    if (chamadoReader.Read())
                    {
                        _chamado = PopulaChamado(chamadoReader);

                        List<Chamado> _lista = ChamadosParaAutorizar(new List<Chamado>());
                        foreach (var item in _lista.Where(w => w.IdChamado == codigo))
                        {
                            _chamado.IdUsuarioAutorizacao = item.IdUsuarioAutorizacao;
                            _chamado.MotivoNegacaoDaAutorizacao = item.MotivoNegacaoDaAutorizacao;
                            _chamado.Autorizado = item.Autorizado;
                            _chamado.AguardarAutorizado = item.AguardarAutorizado;
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
            return _chamado;
        }

        public List<Chamado> ConsultaAgrupados(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Chamado> _dados = new List<Chamado>();
            Publicas.mensagemDeErro = string.Empty;
            Chamado _chamado = new Chamado();

            try
            {

                query.Append("Select c.IDCHAMADO    ");
                query.Append("     , c.IDUSUARIO    ");
                query.Append("     , c.IDCATEGORIA  ");
                query.Append("     , c.IDTELA       ");
                query.Append("     , c.ASSUNTO      ");
                query.Append("     , c.IDEMPRESA    ");
                query.Append("     , c.DATA         ");
                query.Append("     , c.NUMERO       ");
                query.Append("     , c.STATUS       ");
                query.Append("     , c.ORIGEM       ");
                query.Append("     , c.PRIORIDADE   ");
                query.Append("     , c.NUMADEQFORN  ");
                query.Append("     , c.DATAENTADEQ  ");
                query.Append("     , c.AVALIACAO    ");
                query.Append("     , c.TipoChamado  ");
                query.Append("     , c.DescricaoAvaliacao          ");
                query.Append("     , ua.Nome UsuarioAbertura       ");
                query.Append("     , ct.descricao categoria        ");
                query.Append("     , c.IDCHAMADOASSOCIADO          ");
                query.Append("     , p.qtddiasretornochamado dias  ");
                query.Append("     , c.AtendenteFoiCortez          ");
                query.Append("     , c.DataAvaliacaoDoSolicitante  ");
                query.Append("     , c.SolicitanteAbriuCorretamente");
                query.Append("     , c.SolicitanteRespDentroDePrazo");
                query.Append("     , c.SolicitanteFoiCortez        ");
                query.Append("     , c.AvaliacaoSolicitante        ");
                query.Append("     , c.DescricaoAvaliacaoSolic     ");
                query.Append("     , c.PRAZODESENVOLVIMENTO        ");
                query.Append("     , c.LembrarDentreDeDias         ");
                query.Append("     , c.MotivoLembrete              ");
                query.Append("     , c.Reavaliar                   ");
                query.Append("     , c.Reavaliado                   ");
                query.Append("     , c.DataReavaliacao              ");
                query.Append("     , c.IdUsuarioAcompanhamento      ");
                query.Append("     , c.IDEMPRESASOLICITANTE");
                query.Append("     , c.TrocouCategoria");
                query.Append("     , (Select Data");
                query.Append("          From niff_chm_lembretechamados l");
                query.Append("         Where l.Idchamado = c.IdChamado");
                query.Append("           And trunc(sysdate) = Data) Lembrete");
                query.Append("     , c.TEMPOESTIMADOMIN");

                query.Append("  From Niff_Chm_Chamado c, Niff_CHM_Usuarios ua, Niff_Chm_Parametros p   ");
                query.Append("     , Niff_Chm_Categorias CT");

                query.Append(" Where c.idUsuario = ua.IdUsuario");
                query.Append("   and ct.Idcategoria = c.idcategoria");

                query.Append("   and c.IDCHAMADOAGRUPADO = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    if (chamadoReader.Read())
                    {
                        _chamado = PopulaChamado(chamadoReader);

                        List<Chamado> _lista = ChamadosParaAutorizar(new List<Chamado>());
                        foreach (var item in _lista.Where(w => w.IdChamado == codigo))
                        {
                            _chamado.IdUsuarioAutorizacao = item.IdUsuarioAutorizacao;
                            _chamado.MotivoNegacaoDaAutorizacao = item.MotivoNegacaoDaAutorizacao;
                            _chamado.Autorizado = item.Autorizado;
                            _chamado.AguardarAutorizado = item.AguardarAutorizado;
                        }
                        _dados.Add(_chamado);
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
            return _dados;
        }

        public int ConsultaQuantidadeSemAvaliacao()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int qdt = 0;

            try
            {

                query.Append("Select Count(*) Qtde    ");
                query.Append("  From Niff_Chm_Chamado c");

                query.Append(" Where status = 'F'");
                query.Append("   and nvl(avaliacao,0) = 0 ");
                query.Append("   and c.IdUsuario = " + Publicas._idUsuario);

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    if (chamadoReader.Read())
                        qdt = Convert.ToInt32(chamadoReader["Qtde"].ToString());
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
            return qdt;
        }

        public bool Grava(Chamado chamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int _idChamado = 0;
            LogSAC _log = new LogSAC();
            bool retorno = false;

            try
            {
                if (!chamado.Existe)
                {
                    query.Clear();
                    query.Append("(select SQ_NIFF_IDChamado.Nextval next from dual)");
                    Query executar = sessao.CreateQuery(query.ToString());

                    chamadoReader = executar.ExecuteQuery();

                    using (chamadoReader)
                    {
                        if (chamadoReader.Read())
                        {
                            _idChamado = Convert.ToInt32(chamadoReader["next"].ToString());
                        }
                    }

                    Publicas._idChamado = _idChamado;
                    query.Clear();
                    query.Append("Insert into Niff_Chm_Chamado");
                    query.Append("   ( idchamado, ");
                    query.Append("   idusuario,");
                    query.Append("   idcategoria,");
                    query.Append("   idtela,");
                    query.Append("   assunto,");
                    query.Append("   idempresa,");
                    query.Append("   data,");
                    query.Append("   numero,");
                    query.Append("   status,");
                    query.Append("   origem,");
                    query.Append("   prioridade,");
                    query.Append("   TipoChamado  ");
                    if (chamado.IdChamadoAssociado != 0)
                        query.Append("   , IdChamadoAssociado  ");
                    if (chamado.IdUsuarioAcompanhamento != 0)
                        query.Append("   , IdUsuarioAcompanhamento  ");

                    query.Append("  , IdEmpresaSolicitante");
                    query.Append("  ) Values ( " + _idChamado);
                    query.Append(", " + chamado.IdUsuario.ToString());
                    query.Append(", " + chamado.IdCategoria);
                    query.Append(", " + chamado.IdTela);
                    query.Append(", '" + chamado.Assunto + "'");
                    query.Append(", " + chamado.IdEmpresa );
                    query.Append(", sysdate ");
                    query.Append(", '" + chamado.Numero + "'");
                    query.Append(", 'N'");

                    query.Append(", '" + (chamado.Origens == Publicas.Origem.Email ? "E" :
                                         (chamado.Origens == Publicas.Origem.Telefone ? "T" :
                                         (chamado.Origens == Publicas.Origem.Chat ? "H" :
                                         "C"))) + "'");

                    query.Append(", '" + (chamado.Prioridade == Publicas.Prioridades.Alta ? "A" :
                                         (chamado.Prioridade == Publicas.Prioridades.Baixa ? "B" :
                                         (chamado.Prioridade == Publicas.Prioridades.Critico ? "C" : "M"))) + "'");

                    
                    query.Append(", '" + (chamado.Tipo == Publicas.TipoChamado.Duvida ? "D" :
                                         (chamado.Tipo == Publicas.TipoChamado.Erro ? "E" :
                                         (chamado.Tipo == Publicas.TipoChamado.Acesso ? "A" :
                                         (chamado.Tipo == Publicas.TipoChamado.Implementacao ? "I" :
                                         (chamado.Tipo == Publicas.TipoChamado.Ajustes ? "J" :
                                         (chamado.Tipo == Publicas.TipoChamado.Projeto ? "P" :
                                         "S")))))) + "'");

                    if (chamado.IdChamadoAssociado != 0)
                        query.Append(", " + chamado.IdChamadoAssociado.ToString());
                    if (chamado.IdUsuarioAcompanhamento != 0)
                        query.Append(", " + chamado.IdUsuarioAcompanhamento.ToString());

                    query.Append("  , " + chamado.IdEmpresaSolicitante);
                    query.Append(")");
                    
                }
                else
                {
                    query.Clear();
                    query.Append("Update Niff_Chm_Chamado");

                    query.Append("   Set Assunto = '" + chamado.Assunto + "'");
                    query.Append("     , IdCategoria = " + chamado.IdCategoria );
                    query.Append("     , IdTela = " + chamado.IdTela);
                    query.Append("     , IdEmpresa = " + chamado.IdEmpresa);
                    query.Append("     , IdUsuario = " + chamado.IdUsuario);

                    if (chamado.Avaliacao != Publicas.TipoDeSatisfacaoAtendimento.SemAvaliacao)
                    {
                        query.Append("     , DataAvaliacao = sysdate");
                        query.Append("     , Avaliacao = " + (chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Ruim ? "1" :
                       (chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Bom ? "3" :
                       (chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Regular ? "2" :
                       (chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.MuitoBom ? "4" :
                       (chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Excelente ? "5" :
                       "0"))))) );
                        query.Append("     , DescricaoAvaliacao = '" + chamado.DescricaoAvaliacao + "'");
                        query.Append("     , ProblemaResolvido = '" + (chamado.ProblemaResolvido ? "S" : "N") + "'");
                        query.Append("     , DentroDoPrazo = '" + (chamado.DentroDoPrazo ? "S" : "N") + "'");
                        query.Append("     , AtendenteFoiCortez = '" + (chamado.AtendenteFoiCortez ? "S" : "N") + "'");
                    }

                    if (chamado.AvaliacaoSolicitante != Publicas.TipoDeSatisfacaoAtendimento.SemAvaliacao)
                    {
                        query.Append("     , DataAvaliacaoDoSolicitante = sysdate");
                        query.Append("     , AvaliacaoSolicitante = " + (chamado.AvaliacaoSolicitante == Publicas.TipoDeSatisfacaoAtendimento.Ruim ? "1" :
                       (chamado.AvaliacaoSolicitante == Publicas.TipoDeSatisfacaoAtendimento.Bom ? "3" :
                       (chamado.AvaliacaoSolicitante == Publicas.TipoDeSatisfacaoAtendimento.Regular ? "2" :
                       (chamado.AvaliacaoSolicitante == Publicas.TipoDeSatisfacaoAtendimento.MuitoBom ? "4" :
                       (chamado.AvaliacaoSolicitante == Publicas.TipoDeSatisfacaoAtendimento.Excelente ? "5" :
                       "0"))))));
                        query.Append("     , DescricaoAvaliacaoSolic = '" + chamado.DescricaoAvaliacaoSolic + "'");
                        query.Append("     , SolicitanteAbriuCorretamente = '" + (chamado.SolicitanteAbriuCorreto ? "S" : "N") + "'");
                        query.Append("     , SolicitanteRespDentroDePrazo = '" + (chamado.SolicitanteDentroPrazo ? "S" : "N") + "'");
                        query.Append("     , SolicitanteFoiCortez = '" + (chamado.SolicitanteFoiCortez ? "S" : "N") + "'");
                    }

                    if (!string.IsNullOrEmpty(chamado.Adequacao))
                        query.Append("     , NUMADEQFORN = '" + chamado.Adequacao + "'");

                    if (chamado.Status == Publicas.StatusChamado.EmDesenvolvimento || chamado.Status == Publicas.StatusChamado.Adequacao || 
                        chamado.Status == Publicas.StatusChamado.AguardandoCronograma || chamado.Status == Publicas.StatusChamado.AguardandoConserto)
                        query.Append("     , PRAZODESENVOLVIMENTO = " + chamado.PrazoDesenvolvimento);
                    
                    query.Append("     , TipoChamado = '" + (chamado.Tipo == Publicas.TipoChamado.Duvida ? "D" :
                                         (chamado.Tipo == Publicas.TipoChamado.Erro ? "E" :
                                         (chamado.Tipo == Publicas.TipoChamado.Acesso ? "A" :
                                         (chamado.Tipo == Publicas.TipoChamado.Implementacao ? "I" :
                                         (chamado.Tipo == Publicas.TipoChamado.Ajustes ? "J" :
                                         (chamado.Tipo == Publicas.TipoChamado.Projeto ? "P" :
                                         "S")))))) + "'");

                    query.Append("     , Status = '" + (chamado.Status == Publicas.StatusChamado.Adequacao ? "A" :
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

                    query.Append("     , Prioridade = '" + (chamado.Prioridade == Publicas.Prioridades.Alta ? "A" :
                       (chamado.Prioridade == Publicas.Prioridades.Baixa ? "B" :
                        (chamado.Prioridade == Publicas.Prioridades.Critico ? "C" : "M"))) + "'");

                    if (chamado.IdUsuarioAcompanhamento != 0)
                        query.Append("   , IdUsuarioAcompanhamento = " + chamado.IdUsuarioAcompanhamento );

                    query.Append("     , TrocouCategoria = '" + (chamado.TrocouCategoria ? "S" : "N") + "'");

                    query.Append("     , TEMPOESTIMADOMIN = " + chamado.MinutosEstimados );

                    query.Append(" Where idChamado = " + chamado.IdChamado);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                
                if (retorno)
                {
                    // verifica se tem agrupado para mudar o status quando encerrado/cancelado
                    if (chamado.Status == Publicas.StatusChamado.Finalizado ||
                        chamado.Status == Publicas.StatusChamado.Cancelado)
                    {
                        List<Chamado> _chamadAgrupado = ConsultaAgrupados(chamado.IdChamado);
                        _chamadAgrupado.ForEach(u => u.Status = chamado.Status);

                        foreach (var item in _chamadAgrupado)
                        {
                            retorno = GravaStatus(item);
                            if (!retorno)
                                break;
                        }
                        
                    }
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

        public bool GravaAvaliacaoAtendente(Chamado chamado)
        { // o solicitante que efetua a avaliação
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            LogSAC _log = new LogSAC();
            bool retorno = false;

            try
            {

                query.Clear();
                query.Append("Update Niff_Chm_Chamado");

                query.Append("   Set DataAvaliacao = sysdate");
                query.Append("     , Avaliacao = " + (chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Ruim ? "1" :
                       (chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Bom ? "3" :
                       (chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Regular ? "2" :
                       (chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.MuitoBom ? "4" :
                       (chamado.Avaliacao == Publicas.TipoDeSatisfacaoAtendimento.Excelente ? "5" :
                       "0"))))));
                query.Append("     , DescricaoAvaliacao = '" + chamado.DescricaoAvaliacao + "'");
                query.Append("     , ProblemaResolvido = '" + (chamado.ProblemaResolvido ? "S" : "N") + "'");
                query.Append("     , DentroDoPrazo = '" + (chamado.DentroDoPrazo ? "S" : "N") + "'");
                query.Append("     , AtendenteFoiCortez = '" + (chamado.AtendenteFoiCortez ? "S" : "N") + "'");

                if (!chamado.Reavaliar)
                    query.Append("     , AvaliouNoPrazoSolicitante = '" + (DateTime.Now.Date > chamado.DataRetorno.AddDays(2) ? "N" : "S") + "'");
                else
                {
                    query.Append("     , Reavaliado = 'S'");
                    query.Append("     , DataReavaliacao = sysdate");
                }

                query.Append(" Where idChamado = " + chamado.IdChamado);
                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno)
                {
                    List<Chamado> _chamadAgrupado = ConsultaAgrupados(chamado.IdChamado);
                    _chamadAgrupado.ForEach(u => { u.Avaliacao = chamado.Avaliacao;
                        u.DescricaoAvaliacao = chamado.DescricaoAvaliacao;
                        u.ProblemaResolvido = chamado.ProblemaResolvido;
                        u.DentroDoPrazo = chamado.DentroDoPrazo;
                        u.AtendenteFoiCortez = chamado.AtendenteFoiCortez;
                        });

                    foreach (var item in _chamadAgrupado)
                    {
                        retorno = GravaAvaliacaoAtendente(item);
                        if (!retorno)
                            break;
                    }
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

        public bool GravaAvaliacaoSolicitante(Chamado chamado)
        {// o atendente que efetua a avaliação
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            LogSAC _log = new LogSAC();
            bool retorno = false;

            try
            {

                query.Clear();
                query.Append("Update Niff_Chm_Chamado");

                query.Append("   Set DataAvaliacaoDoSolicitante = sysdate");
                query.Append("     , AvaliacaoSolicitante = " + (chamado.AvaliacaoSolicitante == Publicas.TipoDeSatisfacaoAtendimento.Ruim ? "1" :
                                                                (chamado.AvaliacaoSolicitante == Publicas.TipoDeSatisfacaoAtendimento.Bom ? "3" :
                                                                (chamado.AvaliacaoSolicitante == Publicas.TipoDeSatisfacaoAtendimento.Regular ? "2" :
                                                                (chamado.AvaliacaoSolicitante == Publicas.TipoDeSatisfacaoAtendimento.MuitoBom ? "4" :
                                                                (chamado.AvaliacaoSolicitante == Publicas.TipoDeSatisfacaoAtendimento.Excelente ? "5" :
                                                                "0"))))));
                query.Append("     , DescricaoAvaliacaoSolic = '" + chamado.DescricaoAvaliacaoSolic + "'");
                query.Append("     , SolicitanteAbriuCorretamente = '" + (chamado.SolicitanteAbriuCorreto ? "S" : "N") + "'");
                query.Append("     , SolicitanteRespDentroDePrazo = '" + (chamado.SolicitanteDentroPrazo ? "S" : "N") + "'");
                query.Append("     , SolicitanteFoiCortez = '" + (chamado.SolicitanteFoiCortez ? "S" : "N") + "'");
                query.Append("     , AvaliouNoPrazoAtendente = '" + (DateTime.Now.Date > chamado.DataRetorno.AddDays(2) ? "N" : "S") + "'");
                query.Append(" Where idChamado = " + chamado.IdChamado);
                retorno = sessao.ExecuteSqlTransaction(query.ToString());
                
                if (retorno)
                {
                    List<Chamado> _chamadAgrupado = ConsultaAgrupados(chamado.IdChamado);
                    _chamadAgrupado.ForEach(u => {
                        u.AvaliacaoSolicitante = chamado.AvaliacaoSolicitante;
                        u.DescricaoAvaliacaoSolic = chamado.DescricaoAvaliacaoSolic;
                        u.SolicitanteAbriuCorreto = chamado.SolicitanteAbriuCorreto;
                        u.SolicitanteDentroPrazo = chamado.SolicitanteDentroPrazo;
                        u.SolicitanteFoiCortez = chamado.SolicitanteFoiCortez;
                    });

                    foreach (var item in _chamadAgrupado)
                    {
                        retorno = GravaAvaliacaoSolicitante(item);
                        if (!retorno)
                            break;
                    }
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

        public bool LiberarParaReavaliacao(Chamado chamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            LogSAC _log = new LogSAC();
            bool retorno = false;

            try
            {

                query.Clear();
                query.Append("Update Niff_Chm_Chamado");
                query.Append("   Set Reavaliar = 'S'");
                query.Append(" Where idChamado = " + chamado.IdChamado);
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

        public bool AgruparChamado(List<Chamado> chamados)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            LogSAC _log = new LogSAC();
            bool retorno = false;
            int idChamado = 0;
            string numero = "";
            Usuario _usuario;

            try
            { 
                foreach (var item in chamados.Where(w => w.Agrupar).OrderBy(o => o.Numero))
                {
                    if (idChamado == 0)
                    {
                        idChamado = item.IdChamado;
                        numero = item.Numero;
                    }
                    else
                    {
                        query.Clear();
                        query.Append("Update Niff_Chm_Chamado");
                        query.Append("   set IdChamadoAgrupado = " + idChamado);
                        query.Append("     , IdUsuarioAgrupou = " + Publicas._idUsuario);
                        query.Append("     , DataAgrupou = sysdate");
                        query.Append(" Where idChamado = " + item.IdChamado);

                        retorno = sessao.ExecuteSqlTransaction(query.ToString());

                        // Enviar email informando ao usuário que o chamado foi agrupado para ele seguir o novo chamado.
                        string _emailDestino = "";
                        string[] _dadosEmail = new string[50];

                        _usuario = new UsuarioDAO().ConsultaUsuarioPorID(item.IdUsuario);

                        _emailDestino = _usuario.Email + ";" + _usuario.EmailDepartamento;

                        _dadosEmail[0] = "Alteração no chamado";

                        _dadosEmail[2] = _usuario.Nome;

                        _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                        _dadosEmail[4] = item.Numero;

                        _dadosEmail[15] = numero;

                        string[] nome = Publicas._usuario.Nome.Split(' ');

                        if (Publicas._usuario.AssinaturaChamado == "")
                        {
                            _dadosEmail[16] = "<p style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 13px'/> Atenciosamente,</br> " +
                                           nome[0] + " " + nome[nome.Length - 1] +
                                           "</font></p>";
                        }
                        else
                        {
                            _dadosEmail[16] = "<p style = 'color: #4169e1; font-family: arial, sans-serif; font-size: 13px'/>" +
                                Publicas._usuario.AssinaturaChamado + "</font></p>";
                        }

                        Publicas.EnviarEmailChamado(_dadosEmail, true, false, true, _emailDestino, _dadosEmail[0]);                        
                    }
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

        public string ProximoCodigo(Publicas.TipoCalculoChamado tipo, string separador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            string retorno = "1";

            try
            {
                switch (tipo)
                {
                    case Publicas.TipoCalculoChamado.Ano:
                        query.Append("Select Nvl(Max(SubStr(Numero, 6,length(Numero))),0) +1 Next From Niff_Chm_Chamado");
                        query.Append(" Where Numero like To_char(Sysdate,'yyyy') || '%'");
                        break;
                    case Publicas.TipoCalculoChamado.AnoMes:
                        query.Append("Select Nvl(Max(SubStr(Numero, 8,length(Numero))),0) +1 Next From Niff_Chm_Chamado");
                        query.Append(" Where Numero like To_char(Sysdate,'yyyymm') || '%'");
                        break;
                    case Publicas.TipoCalculoChamado.Sequencial:
                        query.Append("Select nvl(Max(Numero),0) + 1 Next From Niff_Chm_Chamado");
                        break;
                }

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader = executar.ExecuteQuery();

                using (chamadoReader)
                {
                    if (chamadoReader.Read())
                    {
                        switch (tipo)
                        {
                            case Publicas.TipoCalculoChamado.Ano:
                                retorno = DateTime.Now.Year.ToString("0000") + separador + chamadoReader["next"].ToString().PadLeft(6,'0');
                                break;
                            case Publicas.TipoCalculoChamado.AnoMes:
                                retorno = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + separador + chamadoReader["next"].ToString().PadLeft(4, '0');
                                break;
                            case Publicas.TipoCalculoChamado.Sequencial:
                                retorno = chamadoReader["next"].ToString().PadLeft(10, '0');
                                break;
                        }
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

        public bool GravaStatus(Chamado chamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            LogSAC _log = new LogSAC();
            bool retorno = false;

            try
            {
                query.Clear();
                query.Append("Update Niff_Chm_Chamado");
                query.Append("   Set Status = '" + (chamado.Status == Publicas.StatusChamado.Adequacao ? "A" :
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
                query.Append(" Where idChamado = " + chamado.IdChamado);
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

        public bool GravaTempoEstimado(Chamado chamado)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            LogSAC _log = new LogSAC();
            bool retorno = false;

            try
            {
                query.Clear();
                query.Append("Update Niff_Chm_Chamado");
                query.Append("   Set TEMPOESTIMADOMIN = " + chamado.MinutosEstimados );
                query.Append(" Where idChamado = " + chamado.IdChamado);
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


        public bool Grava(List<Lembrete> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            LogSAC _log = new LogSAC();
            bool retorno = false;

            try
            {
                foreach (var chamado in _lista)
                {
            
                    query.Clear();
                    query.Append("Insert into Niff_Chm_LembreteChamados");
                    query.Append("   ( Id, idchamado, data)");
                    query.Append("  Values ( (Select Nvl(Max(id),0) + 1 from Niff_Chm_LembreteChamados), " + chamado.IdChamado);
                    query.Append(", To_date('" + chamado.Data.ToShortDateString() + "','dd/mm/yyyy')");
                    query.Append(")");
                    
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
                }
                
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
            return retorno;
        }

        public string BuscaNomeAprovador(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();

            Publicas.mensagemDeErro = string.Empty;
            string[] nomes;
            string nome = "";
            try
            {
                query.Clear();
                query.Append("Select u.nome");
                query.Append("  From niff_chm_histochamado h, Niff_Chm_Usuarios u");
                query.Append(" Where h.Idchamado = " + id);
                query.Append("   And h.Idusuarioautorizacao = u.idusuario");

                Query executar = sessao.CreateQuery(query.ToString());

                chamadoReader3 = executar.ExecuteQuery();

                using (chamadoReader3)
                {
                    if (chamadoReader3.Read())
                    {
                        nomes = chamadoReader3["nome"].ToString().Split(' ');
                        nome = nomes[0] + " " + nomes[nomes.Length - 1];
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

        public void AvaliacaoAutomatica()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();

            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                // tira um ponto da avaliação do solicitante quando avaliaçao maior que 1 senao fica 1. por demorar para avaliar
                query.Append("Update niff_chm_chamado");
                query.Append("   set avaliacao = 5, problemaresolvido = 'S', atendentefoicortez = 'S', dentrodoprazo = 'S'");
                query.Append("     , AVALIACAOSOLICITANTE = Case When AVALIACAOSOLICITANTE <=1 Then 1 Else AVALIACAOSOLICITANTE -1 End");
                query.Append("     , DESCRICAOAVALIACAO = DESCRICAOAVALIACAO || ' passou de 5 dias sem avaliação do solicitante - avaliação automatica'");
                query.Append("     , DataAvaliacao = sysdate");

                query.Append(" Where IdChamado in (Select c.IdChamado");
                query.Append("                       From niff_chm_chamado c, niff_chm_usuarios u");
                query.Append("                          , (Select IdChamado, Max(data) DataFinal");
                query.Append("                               From niff_chm_histochamado");
                query.Append("                              Group By IdChamado) Tc");
                query.Append("                      Where c.avaliacao Is Null");
                query.Append("                        And c.status = 'F'");
                query.Append("                        And tc.IdChamado = c.IdChamado");
                query.Append("                        And u.idusuario = c.idusuario");
                query.Append("                        And trunc(tc.DataFinal) <= trunc(Sysdate) - 5)");

                sessao.ExecuteSqlTransaction(query.ToString());

                query.Clear();
                query.Append("Update niff_chm_chamado");
                query.Append("   set avaliacao = 5, problemaresolvido = 'S', atendentefoicortez = 'S', dentrodoprazo = 'S'");
                query.Append("     , DESCRICAOAVALIACAO = DESCRICAOAVALIACAO || ' usuário está inativo avaliação automatica'");
                query.Append("     , DataAvaliacao = sysdate");
                query.Append(" Where IdChamado in (Select c.IdChamado");
                query.Append("                       From niff_chm_chamado c, niff_chm_usuarios u");
                query.Append("                          , (Select IdChamado, Max(data) DataFinal");
                query.Append("                               From niff_chm_histochamado");
                query.Append("                              Group By IdChamado) Tc");
                query.Append("                      Where c.avaliacao Is Null");
                query.Append("                        And c.status = 'F'");
                query.Append("                        And tc.IdChamado = c.IdChamado");
                query.Append("                        And u.idusuario = c.idusuario");
                query.Append("                        And u.ativo = 'N')");

                sessao.ExecuteSqlTransaction(query.ToString());
                
                //if (DateTime.Now.Date >= Convert.ToDateTime("13/07/2020"))
                {
                    query.Clear();
                    query.Append("Update niff_chm_chamado");
                    query.Append("   set avaliacaosolicitante = 5, solicitanteabriucorretamente = 'S', solicitantefoicortez = 'S', solicitanterespdentrodeprazo = 'S'");
                    query.Append("     , descricaoavaliacaosolic = descricaoavaliacaosolic || ' passou de 5 dias sem avaliação do atendente - avaliação automatica'");
                    query.Append("     , DataAvaliacaodoSolicitante = sysdate");
                    query.Append("     , AVALIACAO = Case When AVALIACAO <= 1 Then 1 Else AVALIACAO -1 End");
                    query.Append(" Where IdChamado in (Select c.IdChamado");
                    query.Append("                       From niff_chm_chamado c");
                    query.Append("                      Where c.status = 'F'");
                    query.Append("                        And Nvl(c.avaliacaosolicitante,0) = 0");
                    query.Append("                        And c.avaliacao > 0");
                    query.Append("                        And (trunc(c.DataAvaliacao) <= trunc(Sysdate) - 5");
                    query.Append("                         Or c.DataAvaliacao Is Null))");
                    
                    sessao.ExecuteSqlTransaction(query.ToString());
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

        }
    }
}
