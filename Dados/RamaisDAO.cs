using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class RamaisDAO
    {
        IDataReader dataReader;

        public List<Telefone> ListarTelefones(int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Telefone> _lista = new List<Telefone>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, idEmpresa, numero, operadora");
                query.Append("  From Niff_Chm_Telefone C");
                query.Append(" Where idEmpresa = " + idEmpresa);

                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Telefone _tipo = new Telefone();

                        _tipo.Existe = true;
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        _tipo.Operadora = dataReader["Operadora"].ToString();

                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Numero = Convert.ToDecimal(dataReader["Numero"].ToString());
                            _tipo.TelefoneFormatado = _tipo.Numero.ToString("(00) 0000-0000");
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

        public List<Ramais> ListarRamais(int idTelefone)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Ramais> _lista = new List<Ramais>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, Idtelefone, Numero, Grupo");
                query.Append("  From NIFF_CHM_Ramal C");
                query.Append(" Where c.IdTelefone = " + idTelefone);
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Ramais _tipo = new Ramais();

                        _tipo.Existe = true;
                        _tipo.Grupo = dataReader["Grupo"].ToString();
                        _tipo.IdTelefone = Convert.ToInt32(dataReader["IdTelefone"].ToString());

                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Numero = Convert.ToInt32(dataReader["Numero"].ToString());
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

        public List<RamaisAssociadosAoColaborador> ListarColaboradoresAssociados(int IdRamal)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<RamaisAssociadosAoColaborador> _lista = new List<RamaisAssociadosAoColaborador>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select t.id, t.idRamal, c.idcolaborador, t.complemento, t.nome");
                query.Append("  From NIFF_CHM_ColabRamal t, niff_ads_Colaboradores c");
                query.Append(" Where t.IdRamal = " + IdRamal);
                query.Append("   and c.IdColaborador(+) = t.IdColaborador");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        RamaisAssociadosAoColaborador _tipo = new RamaisAssociadosAoColaborador();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdRamal = IdRamal;
                        _tipo.NomeColaborador = dataReader["Nome"].ToString();
                        _tipo.Complemento = dataReader["complemento"].ToString();

                        try
                        {
                            _tipo.IdColaborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());
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

        public Telefone Consultar(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Telefone _tipo = new Telefone();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, numero, idEmpresa, operadora");
                query.Append("  From Niff_Chm_Telefone C");
                query.Append(" Where c.Id = " + id);
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Operadora = dataReader["Operadora"].ToString();
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());

                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Numero = Convert.ToDecimal(dataReader["Numero"].ToString());
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
                sessao.Desconectar();
            }
            return _tipo;
        }

        public Telefone Consultar(int idEmpresa, decimal Telefone)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Telefone _tipo = new Telefone();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, numero, idEmpresa, Operadora");
                query.Append("  From Niff_Chm_Telefone C");
                query.Append(" Where c.numero = " + Telefone);
                query.Append("   and c.IdEmpresa = " + idEmpresa);
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Operadora = dataReader["Operadora"].ToString();
                        _tipo.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());

                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Numero = Convert.ToDecimal(dataReader["Numero"].ToString());
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
                sessao.Desconectar();
            }
            return _tipo;
        }

        public Ramais ConsultarRamal(int idTelefone, int numero)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Ramais _tipo = new Ramais();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select id, numero, grupo, idTelefone");
                query.Append("  From NIFF_CHM_Ramal C");
                query.Append(" Where c.IdTelefone = " + idTelefone);
                query.Append("   and numero = " + numero);
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {

                        _tipo.Existe = true;
                        _tipo.Grupo = dataReader["Grupo"].ToString();
                        _tipo.IdTelefone = Convert.ToInt32(dataReader["IdTelefone"].ToString());

                        try
                        {
                            _tipo.Id = Convert.ToInt32(dataReader["id"].ToString());
                        }
                        catch { }

                        try
                        {
                            _tipo.Numero = Convert.ToInt32(dataReader["Numero"].ToString());
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
                sessao.Desconectar();
            }
            return _tipo;
        }

        public bool Gravar(Telefone tel, Ramais ramais, List<RamaisAssociadosAoColaborador> colaboradores)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int id = 0;
            bool retorno = false;
            try
            {
                query.Clear();

                if (!tel.Existe)
                {
                    query.Append(" select nvl(Max(id),0)+1 Next from Niff_Chm_Telefone ");
                    Query executar = sessao.CreateQuery(query.ToString());
                    dataReader = executar.ExecuteQuery();
                    using (dataReader)
                    {
                        if (dataReader.Read())
                            id = Convert.ToInt32(dataReader["next"].ToString());
                    }

                    query.Clear();

                    query.Append("Insert into Niff_Chm_Telefone");
                    query.Append(" ( id, numero, idEmpresa, operadora )");
                    query.Append(" Values ( " + id);
                    query.Append("        , " + tel.Numero);
                    query.Append("        , " + tel.IdEmpresa);
                    query.Append("        , '" + tel.Operadora + "'");
                    query.Append(" )");
                }
                else
                {
                    id = tel.Id;

                    query.Append("Update Niff_Chm_Telefone");
                    query.Append("   set Operadora = '" + tel.Operadora + "'");
                    query.Append(" Where id = " + id);
                }
                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (retorno && colaboradores != null && colaboradores.Count() > 0)
                {
                    ramais.IdTelefone = id;
                    retorno = GravarRamais(ramais, colaboradores);
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

        public bool ExcluirAssociacao(int idColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete NIFF_CHM_ColabRamal");
                query.Append(" Where idColaborador = " + idColaborador);
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

        public bool GravarAssociacao(List<RamaisAssociadosAoColaborador> colaboradores)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            int id = 0;

            try
            {
                foreach (var item in colaboradores)
                {
                    query.Clear();

                    if (item.Excluir)
                    {
                        query.Append("Delete NIFF_CHM_ColabRamal");
                        query.Append(" Where id = " + item.Id);
                        retorno = sessao.ExecuteSqlTransaction(query.ToString());
                        
                    }
                    else
                    {
                        query.Clear();
                        if (!item.Existe)
                        {
                            query.Append(" select nvl(Max(id),0)+1 Next from NIFF_CHM_ColabRamal ");
                            Query executar = sessao.CreateQuery(query.ToString());
                            dataReader = executar.ExecuteQuery();
                            using (dataReader)
                            {
                                if (dataReader.Read())
                                    id = Convert.ToInt32(dataReader["next"].ToString());
                            }

                            query.Clear();

                            query.Append("Insert into NIFF_CHM_ColabRamal");
                            query.Append(" ( id, idRamal, idcolaborador, nome, complemento)");
                            query.Append(" Values ( " + id);
                            query.Append("        , " + item.IdRamal);
                            query.Append("        , " + (item.IdColaborador == 0 ? "null" : item.IdColaborador.ToString()));
                            query.Append("        , '" + item.NomeColaborador + "'");
                            query.Append("        , '" + item.Complemento + "'");
                            query.Append(" )");                            
                        }
                        else
                        {
                            id = item.Id;
                            query.Append("Update NIFF_CHM_ColabRamal");
                            query.Append("   set Complemento = '" + item.Complemento + "'");
                            query.Append("     , Nome = '" + item.NomeColaborador + "'");
                            query.Append(" Where id = " + item.Id);
                        }
                        retorno = sessao.ExecuteSqlTransaction(query.ToString());
                    }

                    if (!retorno)
                        break;
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

        public bool GravarRamais(Ramais ramais, List<RamaisAssociadosAoColaborador> colaboradores)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            int id = 0;

            try
            {

                query.Clear();

                if (!ramais.Existe)
                {
                    query.Append(" select nvl(Max(id),0)+1 Next from NIFF_CHM_Ramal ");
                    Query executar = sessao.CreateQuery(query.ToString());
                    dataReader = executar.ExecuteQuery();
                    using (dataReader)
                    {
                        if (dataReader.Read())
                            id = Convert.ToInt32(dataReader["next"].ToString());
                    }

                    query.Clear();

                    query.Append("Insert into NIFF_CHM_Ramal");
                    query.Append(" ( id, idTelefone, Grupo, numero)");
                    query.Append(" Values ( " + id);
                    query.Append("        , " + ramais.IdTelefone);
                    query.Append("        , '" + ramais.Grupo + "'");
                    query.Append("        , " + ramais.Numero );
                    query.Append(" )");
                }
                else
                {
                    id = ramais.Id;
                    query.Append("Update NIFF_CHM_Ramal");
                    query.Append("   set Grupo = '" + ramais.Grupo + "'");
                    query.Append(" Where id = " + ramais.Id);
                }
                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                if (!retorno)
                    return false;
                else
                {
                    if (retorno && colaboradores != null && colaboradores.Count() > 0)
                    {
                        colaboradores.ForEach(u => u.IdRamal = id);
                        retorno = GravarAssociacao(colaboradores);
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

        public bool Excluir(int Id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();

                query.Append("Delete NIFF_CHM_ColabRamal");
                query.Append(" Where idRamal in (select Id from NIFF_CHM_Ramal where IdTelefone = " + Id + ")");

                if (!sessao.ExecuteSqlTransaction(query.ToString()))
                    return false;
                else
                {
                    query.Clear();

                    query.Append("Delete NIFF_CHM_Ramal");
                    query.Append(" Where IdTelefone = " + Id);
                    if (!sessao.ExecuteSqlTransaction(query.ToString()))
                        return false;
                    else
                    {
                        query.Clear();

                        query.Append("Delete Niff_Chm_Telefone");
                        query.Append(" Where Id = " + Id);
                        return sessao.ExecuteSqlTransaction(query.ToString());
                    }
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
        }

        public List<LocalizaRamais> ListarRamais()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<LocalizaRamais> _lista = new List<LocalizaRamais>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select t.numero Telefone, r.numero ramal, cr.nome, e.nomeabreviado empresa");
                query.Append("     , Decode(trim(r.grupo), Null, cr.complemento, r.grupo) grupo");
                query.Append("     , Decode(trim(r.grupo), Null, null, cr.complemento) complemento");
                query.Append("  From niff_chm_telefone t, niff_chm_ramal r, niff_chm_colabramal cr, niff_chm_empresas e");
                query.Append(" Where t.Id = r.Idtelefone");
                query.Append("   And r.Id = cr.idramal  ");
                query.Append("   And t.idempresa = e.Idempresa");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        LocalizaRamais _tipo = new LocalizaRamais();

                        _tipo.Complemento = dataReader["complemento"].ToString();
                        _tipo.Nome = dataReader["Nome"].ToString() + " " + _tipo.Complemento;
                        _tipo.Empresa = dataReader["Empresa"].ToString();
                        _tipo.Grupo = dataReader["Grupo"].ToString();

                        try
                        {
                            _tipo.Numero = Convert.ToDecimal(dataReader["Telefone"].ToString());
                            _tipo.Telefone = _tipo.Numero.ToString("(00) 0000-0000");

                        }
                        catch { }

                        try
                        {
                            if (Convert.ToInt32(dataReader["Ramal"].ToString()) == 0)
                                _tipo.Ramal = _tipo.Telefone;
                            else
                                _tipo.Ramal = dataReader["Ramal"].ToString();
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

        public RamaisAssociadosAoColaborador ColaboradoresAssociados(int IdColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            RamaisAssociadosAoColaborador _tipo = new RamaisAssociadosAoColaborador();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select t.id, t.idRamal, c.idcolaborador, t.complemento, t.nome");
                query.Append("  From NIFF_CHM_ColabRamal t, niff_ads_Colaboradores c");
                query.Append(" Where t.IdColaborador = " + IdColaborador);
                query.Append("   and c.IdColaborador(+) = t.IdColaborador");
                Query executar = sessao.CreateQuery(query.ToString());

                dataReader = executar.ExecuteQuery();

                using (dataReader)
                {
                    if (dataReader.Read())
                    {
                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dataReader["Id"].ToString());
                        _tipo.IdRamal = Convert.ToInt32(dataReader["IdRamal"].ToString()); 
                        _tipo.NomeColaborador = dataReader["Nome"].ToString();
                        _tipo.Complemento = dataReader["complemento"].ToString();

                        try
                        {
                            _tipo.IdColaborador = Convert.ToInt32(dataReader["IdColaborador"].ToString());
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
                sessao.Desconectar();
            }
            return _tipo;
        }
    }
}
