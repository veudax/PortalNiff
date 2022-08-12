using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class DepartamentoDAO
    {
        IDataReader moduloReader;

        public List<Departamento> ListaDepartamentos()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Departamento> _lista = new List<Departamento>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IdDepartamento ");
                query.Append("     , Descricao");
                query.Append("     , ativo");

                query.Append("  From niff_chm_departamento ");

                Query executar = sessao.CreateQuery(query.ToString());

                moduloReader = executar.ExecuteQuery();

                using (moduloReader)
                {
                    while (moduloReader.Read())
                    {
                        Departamento _modulo = new Departamento();

                        _modulo.Id = Convert.ToInt32(moduloReader["IdDepartamento"].ToString());

                        _modulo.Descricao = moduloReader["Descricao"].ToString();
                        _modulo.Ativo = moduloReader["ativo"].ToString() == "S";
                        _modulo.Existe = true;

                        _lista.Add(_modulo);
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

        public Departamento Consulta(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Departamento _modulo = new Departamento();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IdDepartamento ");
                query.Append("     , Descricao");
                query.Append("     , ativo");

                query.Append("  From niff_chm_departamento ");
                query.Append(" Where IdDepartamento = " + codigo.ToString());

                Query executar = sessao.CreateQuery(query.ToString());

                moduloReader = executar.ExecuteQuery();

                using (moduloReader)
                {
                    if (moduloReader.Read())
                    {
                        _modulo.Id = Convert.ToInt32(moduloReader["IdDepartamento"].ToString());

                        _modulo.Descricao = moduloReader["Descricao"].ToString();
                        _modulo.Ativo = moduloReader["ativo"].ToString() == "S";
                        _modulo.Existe = true;
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
            return _modulo;
        }

        public bool Grava(Departamento departamento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (!departamento.Existe)
                {
                    query.Clear();
                    query.Append("Insert into niff_chm_departamento");
                    query.Append("   (idDepartamento, descricao, ativo) ");
                    query.Append("   Values (" + departamento.Id);
                    query.Append(", '" + departamento.Descricao + "'");
                    query.Append(", '" + (departamento.Ativo ? "S" : "N") + "') ");
                }
                else
                {
                    query.Clear();
                    query.Append("Update niff_chm_departamento");
                    query.Append("   set descricao = '" + departamento.Descricao + "', ");
                    query.Append("       ativo = '" + (departamento.Ativo ? "S" : "N") + "'");
                    query.Append(" Where idDepartamento = " + departamento.Id);
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

        public bool Exclui(Departamento departamento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (departamento.Id != 0)
                {
                    query.Append("Delete niff_chm_departamento");
                    query.Append(" Where idDepartamento = " + departamento.Id);
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
            List<Departamento> _lista = new List<Departamento>();
            Publicas.mensagemDeErro = string.Empty;
            int retorno =1;
            try
            {

                query.Append("Select Max(Iddepartamento) + 1 next From niff_chm_departamento");
                Query executar = sessao.CreateQuery(query.ToString());

                moduloReader = executar.ExecuteQuery();

                using (moduloReader)
                {
                    if (moduloReader.Read())
                        retorno = Convert.ToInt32(moduloReader["next"].ToString());
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

        public List<DepartamentosGerenciadosPeloColaborador> ListarIdUsuario(int idUsuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<DepartamentosGerenciadosPeloColaborador> _lista = new List<DepartamentosGerenciadosPeloColaborador>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select d.IdDepartamento, x.Id ");
                query.Append("     , d.Descricao");
                query.Append("     , x.ativo");
                query.Append("  From niff_chm_departamento d, Niff_ADS_ColabDepartamento x ");
                query.Append(" where x.IdDepartamento = d.IdDepartamento");
                query.Append("   And x.IdUsuario = " + idUsuario);

                Query executar = sessao.CreateQuery(query.ToString());

                moduloReader = executar.ExecuteQuery();

                using (moduloReader)
                {
                    while (moduloReader.Read())
                    {
                        DepartamentosGerenciadosPeloColaborador _modulo = new DepartamentosGerenciadosPeloColaborador();

                        _modulo.Id = Convert.ToInt32(moduloReader["Id"].ToString());
                        _modulo.IdDepartamento = Convert.ToInt32(moduloReader["IdDepartamento"].ToString());

                        _modulo.Descricao = moduloReader["Descricao"].ToString();
                        _modulo.Ativo = moduloReader["ativo"].ToString() == "S";
                        _modulo.Existe = true;

                        _lista.Add(_modulo);
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

        public DepartamentosGerenciadosPeloColaborador ListarIdUsuario(int idUsuario, int idDepartamento)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            DepartamentosGerenciadosPeloColaborador _modulo = new DepartamentosGerenciadosPeloColaborador();

            try
            {

                query.Append("Select d.IdDepartamento, x.Id ");
                query.Append("     , d.Descricao");
                query.Append("     , x.ativo");
                query.Append("  From niff_chm_departamento d, Niff_ADS_ColabDepartamento x ");
                query.Append(" where x.IdDepartamento = d.IdDepartamento");
                query.Append("   And x.IdUsuario = " + idUsuario);
                query.Append("   And x.IdDepartamento = " + idDepartamento);

                Query executar = sessao.CreateQuery(query.ToString());

                moduloReader = executar.ExecuteQuery();

                using (moduloReader)
                {
                    while (moduloReader.Read())
                    {

                        _modulo.Id = Convert.ToInt32(moduloReader["Id"].ToString());
                        _modulo.IdDepartamento = Convert.ToInt32(moduloReader["IdDepartamento"].ToString());

                        _modulo.Descricao = moduloReader["Descricao"].ToString();
                        _modulo.Ativo = moduloReader["ativo"].ToString() == "S";
                        _modulo.Existe = true;
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
            return _modulo;
        }

        public List<DepartamentosGerenciadosPeloColaborador> Listar(int idColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<DepartamentosGerenciadosPeloColaborador> _lista = new List<DepartamentosGerenciadosPeloColaborador>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select d.IdDepartamento, x.Id ");
                query.Append("     , d.Descricao");
                query.Append("     , x.ativo");
                query.Append("  From niff_chm_departamento d, Niff_ADS_ColabDepartamento x ");
                query.Append(" where x.IdDepartamento = d.IdDepartamento");
                query.Append("   And x.IdColaborador = " + idColaborador);

                Query executar = sessao.CreateQuery(query.ToString());

                moduloReader = executar.ExecuteQuery();

                using (moduloReader)
                {
                    while (moduloReader.Read())
                    {
                        DepartamentosGerenciadosPeloColaborador _modulo = new DepartamentosGerenciadosPeloColaborador();

                        _modulo.Id = Convert.ToInt32(moduloReader["Id"].ToString());
                        _modulo.IdDepartamento = Convert.ToInt32(moduloReader["IdDepartamento"].ToString());

                        _modulo.Descricao = moduloReader["Descricao"].ToString();
                        _modulo.Ativo = moduloReader["ativo"].ToString() == "S";
                        _modulo.Existe = true;

                        _lista.Add(_modulo);
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

        public List<DepartamentosGerenciadosPeloColaborador> ListarColaboradores(int idColaborador, int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<DepartamentosGerenciadosPeloColaborador> _lista = new List<DepartamentosGerenciadosPeloColaborador>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query.Append("Select Distinct Idcolaborador");
                query.Append(" from ( ");
                query.Append("Select Distinct c.Idcolaborador");
                query.Append("  From niff_chm_departamento d, Niff_ADS_ColabDepartamento x, Niff_Ads_Colaboradores c, niff_pto_colabperiodo p");
                query.Append(" where x.IdDepartamento(+) = d.IdDepartamento");
                query.Append("   And c.Iddepartamento = d.Iddepartamento");
                query.Append("   And c.Idcolaborador = p.Idcolaborador");
                query.Append("   And c.Idempresa = " + idEmpresa);
                query.Append("   And P.ATIVO = 'S'");
                query.Append("   And X.ATIVO = 'S'");

                if (idColaborador != 0)
                    query.Append("   And c.IdColaborador = " + idColaborador);

                query.Append(" Union all ");
                query.Append("Select Distinct c.Idcolaborador");
                query.Append("  From niff_chm_departamento d, Niff_ADS_ColabDepartamento x, Niff_Ads_Colaboradores c, niff_pto_colabperiodo p");
                query.Append(" where x.IdDepartamento(+) = d.IdDepartamento");
                query.Append("   And c.Iddepartamento = d.Iddepartamento");
                query.Append("   And c.Idcolaborador = p.Idcolaborador");
                query.Append("   And c.Idempresa = " + idEmpresa);
                query.Append("   And P.ATIVO = 'S'");
                query.Append("   And X.ATIVO = 'S'");

                if (idColaborador != 0)
                    query.Append("   And x.IdColaborador = " + idColaborador);
                query.Append(") ");
                Query executar = sessao.CreateQuery(query.ToString());

                moduloReader = executar.ExecuteQuery();

                using (moduloReader)
                {
                    while (moduloReader.Read())
                    {
                        DepartamentosGerenciadosPeloColaborador _modulo = new DepartamentosGerenciadosPeloColaborador();

                        _modulo.IdColaborador = Convert.ToInt32(moduloReader["Idcolaborador"].ToString());

                        _lista.Add(_modulo);
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


        public bool Gravar(List<DepartamentosGerenciadosPeloColaborador> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;
            try
            {
                foreach (var departamento in _lista)
                {

                    if (!departamento.Existe)
                    {
                        query.Clear();
                        query.Append("Insert into Niff_ADS_ColabDepartamento");
                        query.Append("   (id, idDepartamento, idColaborador, ativo, idUsuario) ");
                        query.Append("   Values ( (Select nvl(Max(Id),0) + 1 next From Niff_ADS_ColabDepartamento) ");
                        query.Append("          , " + departamento.IdDepartamento);
                        query.Append("          , " + departamento.IdColaborador);
                        query.Append("          , '" + (departamento.Ativo ? "S" : "N") + "'");
                        query.Append("          , " + departamento.IdUsuario);
                        query.Append("          )");
                    }
                    else
                    {
                        query.Clear();
                        query.Append("Update Niff_ADS_ColabDepartamento");
                        query.Append("   set ativo = '" + (departamento.Ativo ? "S" : "N") + "'");
                        query.Append("     , idUsuario = " + departamento.IdUsuario);
                        query.Append(" Where id = " + departamento.Id);
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

        public bool Exclui(int idColaborador)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                    query.Append("Delete Niff_ADS_ColabDepartamento");
                    query.Append(" Where IdColaborador = " + idColaborador);

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
