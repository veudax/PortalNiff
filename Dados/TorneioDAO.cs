using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class TorneioDAO
    {
        IDataReader dataReader;

        public List<Torneio> Listar(bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Torneio> _lista = new List<Torneio>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, nome, ativo, mariograndprix, mariobattle, arms, pinball, minimo, maximo");
                query.Append("  from Niff_Tor_Torneio c     ");
                if (apenasAtivos)
                    query.Append(" Where ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Torneio _tipo = new Torneio();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.MarioGrandPrix = dataReader["mariograndprix"].ToString() == "S";
                        _tipo.MarioBattle = dataReader["mariobattle"].ToString() == "S";
                        _tipo.Arms = dataReader["Arms"].ToString() == "S";
                        _tipo.Pinball = dataReader["pinball"].ToString() == "S";

                        _tipo.Minimo = Convert.ToInt32(dataReader["Minimo"].ToString());
                        _tipo.Maximo = Convert.ToInt32(dataReader["maximo"].ToString());

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

        public Torneio Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Torneio _tipo = new Torneio();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, nome, ativo, mariograndprix, mariobattle, arms, pinball, minimo, maximo");
                query.Append("  from Niff_Tor_Torneio c     ");
                query.Append(" Where Id = " + codigo);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.Nome = dataReader["Nome"].ToString();
                        _tipo.Ativo = dataReader["Ativo"].ToString() == "S";
                        _tipo.MarioGrandPrix = dataReader["mariograndprix"].ToString() == "S";
                        _tipo.MarioBattle = dataReader["mariobattle"].ToString() == "S";
                        _tipo.Arms = dataReader["Arms"].ToString() == "S";
                        _tipo.Pinball = dataReader["pinball"].ToString() == "S";

                        _tipo.Minimo = Convert.ToInt32(dataReader["Minimo"].ToString());
                        _tipo.Maximo = Convert.ToInt32(dataReader["maximo"].ToString());
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

                query.Append("Select nvl(Max(Id),0) +1 next From Niff_Tor_Torneio");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                        retorno = Convert.ToInt32(dataReader["next"].ToString());
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

        public bool Gravar(Torneio tipo)
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
                    query.Append("Insert into Niff_Tor_Torneio");
                    query.Append(" ( id, nome, ativo, mariograndprix, mariobattle, arms, pinball, minimo, maximo )");
                    query.Append(" Values (" + tipo.Id);
                    query.Append("        ,'" + tipo.Nome + "'");
                    query.Append("        ,'" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.MarioGrandPrix ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.MarioBattle ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.Arms ? "S" : "N") + "'");
                    query.Append("        ,'" + (tipo.Pinball ? "S" : "N") + "'");
                    query.Append("        , " + tipo.Minimo);
                    query.Append("        , " + tipo.Maximo);
                    query.Append(" )");
                }
                else
                {
                    query.Append("Update Niff_Tor_Torneio");
                    query.Append("   set Nome = '" + tipo.Nome + "'");
                    query.Append("     , ativo = '" + (tipo.Ativo ? "S" : "N") + "'");
                    query.Append("     , mariograndprix = '" + (tipo.MarioGrandPrix ? "S" : "N") + "'");
                    query.Append("     , mariobattle = '" + (tipo.MarioBattle ? "S" : "N") + "'");
                    query.Append("     , arms = '" + (tipo.Arms ? "S" : "N") + "'");
                    query.Append("     , pinball = '" + (tipo.Pinball ? "S" : "N") + "'");
                    query.Append("     , minimo = " + tipo.Minimo);
                    query.Append("     , maximo = " + tipo.Maximo);
                    query.Append(" Where id = " + tipo.Id);

                }

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

        public bool Excluir(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Tor_Torneio");
                query.Append(" Where Id = " + id);
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

        public PartidasDoTorneio ConsultarPartida(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            PartidasDoTorneio _tipo = new PartidasDoTorneio();

            try
            {
                query.Append("Select p.Id, p.data, p.round1, p.round2, p.round3, p.round4, p.total");
                query.Append("     , SUBSTR( c.nome, 1, INSTR( c.nome,' ')-1 ) || Substr( c.nome, INSTR( c.nome, ' ', -1)) NomeColaborador, t.nome Torneio, p.Nomepartida");
                query.Append("     , p.IdEmpresa, p.idTorneio, p.IdUsuario, c.UsuarioAcesso, p.Sexo");
                query.Append("  from niff_tor_partidas p, Niff_Chm_Usuarios c, Niff_Tor_Torneio t ");
                query.Append(" Where p.IdUsuario = c.IdUsuario");
                query.Append("   And p.Idtorneio = t.Id");
                query.Append("   and t.ativo = 'S'");
                query.Append("   and p.Id = " + id);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.Nome = dataReader["NomeColaborador"].ToString();
                        _tipo.NomePartida = dataReader["Nomepartida"].ToString();
                        _tipo.Torneio = dataReader["Torneio"].ToString();
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdTorneio = Convert.ToInt32(dataReader["IdTorneio"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        _tipo.Usuario = dataReader["UsuarioAcesso"].ToString();
                        _tipo.Sexo = dataReader["Sexo"].ToString();

                        try
                        {
                            _tipo.Round1 = Convert.ToDecimal(dataReader["Round1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Round2 = Convert.ToDecimal(dataReader["Round2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Round3 = Convert.ToDecimal(dataReader["Round3"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Round4 = Convert.ToDecimal(dataReader["Round4"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Total = Convert.ToDecimal(dataReader["Total"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                        }
                        catch { }

                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                //sessao.Desconectar();
            }
            return _tipo;
        }

        public List<PartidasDoTorneio> ListarPartidas(int idColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PartidasDoTorneio> _lista = new List<PartidasDoTorneio>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select p.Id, p.data, p.round1, p.round2, p.round3, p.round4, p.total");
                query.Append("     , SUBSTR( c.nome, 1, INSTR( c.nome,' ')-1 ) || Substr( c.nome, INSTR( c.nome, ' ', -1)) NomeColaborador, t.nome Torneio, p.Nomepartida");
                query.Append("     , p.IdEmpresa, p.idTorneio, p.IdUsuario, c.UsuarioAcesso, p.Sexo");
                query.Append("  from niff_tor_partidas p, Niff_Chm_Usuarios c, Niff_Tor_Torneio t ");
                query.Append(" Where p.IdUsuario = c.IdUsuario");
                query.Append("   And p.Idtorneio = t.Id");
                query.Append("   and t.ativo = 'S'");

                if (idColaborador != 0)
                    query.Append("   and c.Idusuario = " + idColaborador);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        PartidasDoTorneio _tipo = new PartidasDoTorneio();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.Nome = dataReader["NomeColaborador"].ToString();

                        _tipo.Usuario = dataReader["UsuarioAcesso"].ToString();
                        _tipo.Sexo = dataReader["Sexo"].ToString();

                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdTorneio = Convert.ToInt32(dataReader["IdTorneio"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());
                        _tipo.NomePartida = dataReader["Nomepartida"].ToString();

                        _tipo.Torneio = dataReader["Torneio"].ToString() + " " + dataReader["Nomepartida"].ToString();

                        try
                        {
                            _tipo.Round1 = Convert.ToDecimal(dataReader["Round1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Round2 = Convert.ToDecimal(dataReader["Round2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Round3 = Convert.ToDecimal(dataReader["Round3"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Round4 = Convert.ToDecimal(dataReader["Round4"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Total = Convert.ToDecimal(dataReader["Total"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
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
                //sessao.Desconectar();
            }
            return _lista;
        }

        public List<PartidasDoTorneio> ListarPartidas(int IdTorneio, DateTime data, string sexo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PartidasDoTorneio> _lista = new List<PartidasDoTorneio>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select p.Id, p.data, p.round1, p.round2, p.round3, p.round4, p.total");
                query.Append("     , SUBSTR( c.nome, 1, INSTR( c.nome,' ')-1 ) || Substr( c.nome, INSTR( c.nome, ' ', -1)) NomeColaborador, t.nome Torneio, p.Nomepartida");
                query.Append("     , p.IdEmpresa, p.idTorneio, p.IdUsuario, c.UsuarioAcesso, p.Sexo");
                query.Append("  from niff_tor_partidas p, Niff_Chm_Usuarios c, Niff_Tor_Torneio t ");
                query.Append(" Where p.IdUsuario = c.IdUsuario");
                query.Append("   And p.Idtorneio = t.Id");
                query.Append("   and t.ativo = 'S'");
                query.Append("   and t.Id = " + IdTorneio);
                query.Append("   and p.Data = To_date('" + data.ToShortDateString() + "', 'dd/mm/yyyy')");

                query.Append("   and p.Sexo = '" + sexo + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        PartidasDoTorneio _tipo = new PartidasDoTorneio();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa= Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdTorneio = Convert.ToInt32(dataReader["IdTorneio"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());

                        _tipo.Usuario = dataReader["UsuarioAcesso"].ToString();
                        _tipo.Sexo = dataReader["Sexo"].ToString();

                        _tipo.Nome = dataReader["NomeColaborador"].ToString();
                        _tipo.NomePartida = dataReader["Nomepartida"].ToString();
                        _tipo.Torneio = dataReader["Torneio"].ToString();

                        try
                        {
                            _tipo.Round1 = Convert.ToDecimal(dataReader["Round1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Round2 = Convert.ToDecimal(dataReader["Round2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Round3 = Convert.ToDecimal(dataReader["Round3"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Round4 = Convert.ToDecimal(dataReader["Round4"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Total = Convert.ToDecimal(dataReader["Total"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
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
                //sessao.Desconectar();
            }
            return _lista;
        }

        public List<PartidasDoTorneio> ListarPartidas(int IdTorneio, DateTime data)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<PartidasDoTorneio> _lista = new List<PartidasDoTorneio>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select p.Id, p.data, p.round1, p.round2, p.round3, p.round4, p.total");
                query.Append("     , SUBSTR( c.nome, 1, INSTR( c.nome,' ')-1 ) || Substr( c.nome, INSTR( c.nome, ' ', -1)) NomeColaborador, t.nome Torneio, p.Nomepartida");
                query.Append("     , p.IdEmpresa, p.idTorneio, p.IdUsuario, c.UsuarioAcesso, p.Sexo");
                query.Append("  from niff_tor_partidas p, Niff_Chm_Usuarios c, Niff_Tor_Torneio t ");
                query.Append(" Where p.IdUsuario = c.IdUsuario");
                query.Append("   And p.Idtorneio = t.Id");
                query.Append("   and t.ativo = 'S'");
                query.Append("   and t.Id = " + IdTorneio);
                query.Append("   and p.Data = To_date('" + data.ToShortDateString() + "', 'dd/mm/yyyy')");

                

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        PartidasDoTorneio _tipo = new PartidasDoTorneio();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.IdTorneio = Convert.ToInt32(dataReader["IdTorneio"].ToString());
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"].ToString());

                        _tipo.Usuario = dataReader["UsuarioAcesso"].ToString();
                        _tipo.Sexo = dataReader["Sexo"].ToString();

                        _tipo.Nome = dataReader["NomeColaborador"].ToString();
                        _tipo.NomePartida = dataReader["Nomepartida"].ToString();
                        _tipo.Torneio = dataReader["Torneio"].ToString();

                        try
                        {
                            _tipo.Round1 = Convert.ToDecimal(dataReader["Round1"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Round2 = Convert.ToDecimal(dataReader["Round2"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Round3 = Convert.ToDecimal(dataReader["Round3"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Round4 = Convert.ToDecimal(dataReader["Round4"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Total = Convert.ToDecimal(dataReader["Total"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
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
                //sessao.Desconectar();
            }
            return _lista;
        }

        public List<AgendaTorneio> ListarPartidas()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<AgendaTorneio> _lista = new List<AgendaTorneio>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select p.data, p.Nomepartida");
                query.Append("     , SUBSTR( c.nome, 1, INSTR( c.nome,' ')-1 ) || Substr( c.nome, INSTR( c.nome, ' ', -1)) NomeColaborador");
                query.Append("  from niff_tor_partidas p, Niff_Chm_Usuarios c");
                query.Append(" Where p.IdUsuario = c.IdUsuario");
                query.Append(" Order by p.sexo, p.Data, p.Id");

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();
                DateTime _data = DateTime.MinValue;
                AgendaTorneio _tipo = new AgendaTorneio();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        if (_data != Convert.ToDateTime(dataReader["Data"].ToString()))
                        {
                            _tipo = new AgendaTorneio();
                            _tipo.Partida = dataReader["Nomepartida"].ToString();
                            _tipo.Data = Convert.ToDateTime(dataReader["Data"].ToString());
                            _data = Convert.ToDateTime(dataReader["Data"].ToString());
                            _lista.Add(_tipo);
                        }

                        if (_tipo.Nome1 == null)
                            _tipo.Nome1 = dataReader["NomeColaborador"].ToString();
                        else
                        if (_tipo.Nome2 == null)
                            _tipo.Nome2 = dataReader["NomeColaborador"].ToString();
                        else
                        if (_tipo.Nome3 == null)
                            _tipo.Nome3 = dataReader["NomeColaborador"].ToString();
                        else
                        if (_tipo.Nome4 == null)
                            _tipo.Nome4 = dataReader["NomeColaborador"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                //sessao.Desconectar();
            }
            return _lista;
        }

        public List<Participantes> ListarClassificacao()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Participantes> _lista = new List<Participantes>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select Rownum Clas, a.*");
                query.Append("  from ( Select Total, NomeColaborador, Idusuario, 'Feminino' Sexo ");
                query.Append("           From (Select Sum(Nvl(p.Total,0)) Total, SUBSTR( c.nome, 1, INSTR( c.nome,' ')-1 ) || Substr( c.nome, INSTR( c.nome, ' ', -1))  NomeColaborador, c.Idusuario");
                query.Append("                   From niff_tor_partidas p, Niff_Chm_Usuarios c");
                query.Append("                  Where p.Idusuario = c.Idusuario");
                query.Append("                    And p.sexo = 'F'");
                query.Append("                  Group By c.Nome, c.Idusuario ) c");
                query.Append("          Order by Total Desc, Nomecolaborador) a");
                query.Append(" Union All ");
                query.Append("Select Rownum Clas, a.*");
                query.Append("  from ( Select Total, NomeColaborador, Idusuario, 'Masculino' Sexo ");
                query.Append("           From (Select Sum(Nvl(p.Total,0)) Total, SUBSTR( c.nome, 1, INSTR( c.nome,' ')-1 ) || Substr( c.nome, INSTR( c.nome, ' ', -1))  NomeColaborador, c.Idusuario");
                query.Append("                   From niff_tor_partidas p, Niff_Chm_Usuarios c");
                query.Append("                  Where p.Idusuario = c.Idusuario");
                query.Append("                    And p.sexo = 'M'");
                query.Append("                  Group By c.Nome, c.Idusuario ) c");
                query.Append("          Order by Total Desc, Nomecolaborador) a");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Participantes _tipo = new Participantes();

                        _tipo.Existe = true;
                        _tipo.IdUsuario = Convert.ToInt32(dataReader["Idusuario"].ToString());
                        _tipo.NomeColaborador = dataReader["NomeColaborador"].ToString();
                        _tipo.Sexo = dataReader["sexo"].ToString();
                        _tipo.Total = Convert.ToDecimal(dataReader["Total"].ToString());

                        try
                        {
                            _tipo.Classificacao = Convert.ToInt32(dataReader["Clas"].ToString());
                        }
                        catch { }

                        _tipo.Partidas = new List<PartidasDoTorneio>();
                        _lista.Add(_tipo);
                    }
                }

                foreach (var item in _lista)
                {
                    item.Partidas = ListarPartidas(item.IdUsuario);
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

        public bool GravarPartidas(List<PartidasDoTorneio> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            try
            {
                foreach (var tipo in _lista)
                {
                    query.Clear();
                    if (!tipo.Existe)
                    {
                        query.Append("Insert into Niff_Tor_Partidas");
                        query.Append(" ( id, data, Idusuario, idempresa, idtorneio, nomepartida, sexo)");
                        query.Append(" Values ( (Select nvl(Max(Id),0) +1 From Niff_Tor_Partidas) ");
                        query.Append("        ,To_date('" + tipo.Data.ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("        ," + tipo.IdUsuario);
                        query.Append("        ," + tipo.IdEmpresa);
                        query.Append("        ," + tipo.IdTorneio);
                        query.Append("        ,'" + tipo.NomePartida + "'");
                        query.Append("        ,'" + tipo.Sexo + "'");
                        query.Append(" )");
                    }
                    else
                    {
                        query.Append("Update Niff_Tor_Partidas");
                        query.Append("   set Nomepartida = '" + tipo.NomePartida + "'");
                        query.Append("     , Round1 = " + tipo.Round1);
                        query.Append("     , Round2 = " + tipo.Round2);
                        query.Append("     , Round3 = " + tipo.Round3);
                        query.Append("     , Round4 = " + tipo.Round4);
                        query.Append("     , Total = " + tipo.Total);
                        query.Append("     , Sexo = '" + tipo.Sexo + "'");

                        query.Append(" Where id = " + tipo.Id);

                    }

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
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

        public bool AlterarDataDaPartida(List<PartidasDoTorneio> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;
            try
            {
                foreach (var tipo in _lista)
                {
                    query.Clear();

                    query.Append("Update Niff_Tor_Partidas");
                    query.Append("   set DataOriginal = Data");
                    query.Append("     , Data = To_date('" + tipo.Data.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append(" Where id = " + tipo.Id);

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
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

        public bool ExcluirPartidas(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Tor_Partidas");
                query.Append(" Where Id = " + id);
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
