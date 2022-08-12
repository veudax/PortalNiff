using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class FuncionariosGlobusDAO
    {
        IDataReader usuarioReader;

        public List<FuncionariosGlobus> Lista(string empresa, decimal idSuperior = 0, bool participaAvaliacao = false)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<FuncionariosGlobus> _lista = new List<FuncionariosGlobus>();

            try
            {
                query.Clear();
                query.Append("Select distinct * from (");

                query.Append("Select f.CodIntFunc, decode(c.Nome, null, f.NomeFunc, c.Nome) NomeFunc, f.CodFunc, f.ChapaFunc, f.DescArea, f.SituacaoFunc ");
                query.Append("     , Decode(c.codintfunc, Null, 'N', 'S') cadastrado, f.SEXOFUNC ");
                query.Append("     , f.SALBASE, f.DTADMFUNC, d.nrdocto, f.DTNASCTOFUNC, Max(q.dtdesligquita) dtdesligquita");
                query.Append("  from vw_funcionarios f");
                query.Append("     , flp_documentos d , flp_quitacao q, Niff_Ads_Colaboradores c ");
                query.Append(" Where SituacaoFunc = 'A'"); // apenas os Ativos
                query.Append("   and d.codintfunc = f.CODINTFUNC(+) ");
                query.Append("   and d.tipodocto = 'CPF'");
                query.Append("   and f.CODINTFUNC = q.codintfunc(+)");
                query.Append("   and f.codintfunc = c.CODINTFUNC(+)");

                if (participaAvaliacao)
                    query.Append("   and c.participaavaliacao = 'S'");

                if (empresa == "009/001")
                    query.Append("   And Lpad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') = '009/002'");
                else
                    query.Append("   And Lpad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                
                query.Append(" group by f.CodIntFunc, decode(c.Nome, null, f.NomeFunc, c.Nome), f.CodFunc, f.ChapaFunc, f.DescArea, f.SituacaoFunc ");
                query.Append("     , f.SALBASE, f.DTADMFUNC, d.nrdocto, f.DTNASCTOFUNC, c.codintfunc, f.SEXOFUNC  ");

                query.Append(" Union all ");

                query.Append("Select c.codintfunc, c.Nome Nomefunc, f.Codfunc, f.Chapafunc, f.Descarea, f.Situacaofunc, Decode(c.Codintfunc, Null, 'N', 'S') Cadastrado");
                query.Append("     , f.Sexofunc, f.Salbase, f.Dtadmfunc, d.Nrdocto, f.Dtnasctofunc, Max(q.Dtdesligquita) Dtdesligquita ");
                query.Append("  From Vw_Funcionarios f, Flp_Documentos         d, Flp_Quitacao q, Niff_Ads_Colaboradores c, niff_ads_empresascolavalia e");
                query.Append(" Where Situacaofunc = 'A'");
                query.Append("   And d.Codintfunc = f.Codintfunc(+)");
                query.Append("   And d.Tipodocto = 'CPF'");
                query.Append("   And f.Codintfunc = q.Codintfunc(+)");
                query.Append("   And f.Codintfunc = c.Codintfunc(+)");
                query.Append("   And c.Participaavaliacao = 'S'");
                query.Append("   And e.idcolaborador(+) = c.idcolaborador");
                query.Append("   And e.idempresa = " + Publicas._idEmpresa);
                query.Append("   And e.inicio <> '01-jan-0001'");
                query.Append("   And(e.fim = '01-jan-0001' Or e.fim >= Sysdate)");
                query.Append(" Group By f.Codintfunc, c.Nome, f.Codfunc, f.Chapafunc, f.Descarea, f.Situacaofunc, f.Salbase, f.Dtadmfunc, d.Nrdocto");
                query.Append("     , f.Dtnasctofunc, c.Codintfunc, f.Sexofunc )");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        FuncionariosGlobus _funcionarios = new FuncionariosGlobus();

                        _funcionarios.Nome = usuarioReader["NomeFunc"].ToString();
                        _funcionarios.Codigo = usuarioReader["CodFunc"].ToString();
                        _funcionarios.Chapa = usuarioReader["ChapaFunc"].ToString();
                        _funcionarios.Area = usuarioReader["DescArea"].ToString();
                        _funcionarios.Sexo = usuarioReader["SEXOFUNC"].ToString();
                        _funcionarios.Ativo = usuarioReader["SituacaoFunc"].ToString() == "A";
                        _funcionarios.Id = Convert.ToInt32(usuarioReader["CodIntFunc"].ToString());
                        _funcionarios.DataAdmissao = Convert.ToDateTime(usuarioReader["DTADMFUNC"].ToString());
                        _funcionarios.DataNascimento = Convert.ToDateTime(usuarioReader["DTNASCTOFUNC"].ToString());

                        _funcionarios.ColaboradorCadastrado = usuarioReader["cadastrado"].ToString() == "S";

                        try
                        {
                            _funcionarios.DataDesligamento = Convert.ToDateTime(usuarioReader["dtdesligquita"].ToString());
                        }
                        catch { }
                        _funcionarios.CPF = usuarioReader["nrdocto"].ToString();
                        _funcionarios.Salario = Convert.ToDecimal(usuarioReader["SALBASE"].ToString());
                        _funcionarios.Existe = true;
                        _lista.Add(_funcionarios);
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

        public List<FuncionariosGlobus> Listar(string empresa, bool somenteAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<FuncionariosGlobus> _lista = new List<FuncionariosGlobus>();

            try
            {
                query.Clear();
                query.Append("Select f.CodIntFunc, f.NomeFunc, f.CodFunc, f.ChapaFunc, f.DescArea, f.SituacaoFunc ");
                query.Append("     , f.SEXOFUNC, f.DTADMFUNC, d.nrdocto, f.DTNASCTOFUNC ");
                query.Append("  from vw_funcionarios f");
                query.Append("     , flp_documentos d , flp_quitacao q");
                query.Append(" Where d.codintfunc = f.CODINTFUNC(+) ");
                query.Append("   and d.tipodocto = 'CPF'");
                query.Append("   and f.CODINTFUNC = q.codintfunc(+)");
                if (somenteAtivos)
                    query.Append("   and f.SituacaoFunc = 'A'");
                else
                    query.Append("   and f.SituacaoFunc <> 'D'");

                if (empresa == "009/001")
                    query.Append("   And Lpad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') = '009/002'");
                else
                    query.Append("   And Lpad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') = '" + empresa + "'");

                query.Append(" group by f.CodIntFunc, f.NomeFunc, f.CodFunc, f.ChapaFunc, f.DescArea");
                query.Append("     , f.DTADMFUNC, d.nrdocto, f.DTNASCTOFUNC, f.SEXOFUNC, f.SituacaoFunc  ");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        FuncionariosGlobus _funcionarios = new FuncionariosGlobus();

                        _funcionarios.Nome = usuarioReader["NomeFunc"].ToString();
                        _funcionarios.Codigo = usuarioReader["CodFunc"].ToString();
                        _funcionarios.Chapa = usuarioReader["ChapaFunc"].ToString();
                        _funcionarios.Area = usuarioReader["DescArea"].ToString();
                        _funcionarios.Sexo = usuarioReader["SEXOFUNC"].ToString();
                        _funcionarios.Ativo = usuarioReader["SituacaoFunc"].ToString() == "A";
                        _funcionarios.Id = Convert.ToInt32(usuarioReader["CodIntFunc"].ToString());
                        _funcionarios.DataAdmissao = Convert.ToDateTime(usuarioReader["DTADMFUNC"].ToString());
                        _funcionarios.DataNascimento = Convert.ToDateTime(usuarioReader["DTNASCTOFUNC"].ToString());

                        _funcionarios.CPF = usuarioReader["nrdocto"].ToString();
                        _funcionarios.Existe = true;
                        _lista.Add(_funcionarios);
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

        public FuncionariosGlobus ConsultaFuncionarioGlobus(string registro, string empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            FuncionariosGlobus _funcionarios = new FuncionariosGlobus();

            try
            {
                query.Clear();
                query.Append("Select f.CodIntFunc, f.NomeFunc, f.CodFunc, f.ChapaFunc, f.DescArea, f.SituacaoFunc, f.SEXOFUNC ");
                query.Append("     , f.SALBASE, f.DTADMFUNC, d.nrdocto, f.DTNASCTOFUNC, Max(q.dtdesligquita) dtdesligquita");
                query.Append("  from vw_funcionarios f");
                query.Append("     , flp_documentos d , flp_quitacao q");
                query.Append(" Where CodFunc = '" + registro + "'");
                if (empresa == "009/001")
                    query.Append("   And Lpad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') = '009/002'");
                else
                    query.Append("   And Lpad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') = '" + empresa + "'");
                query.Append("   and d.codintfunc = f.CODINTFUNC(+) ");
                query.Append("   and d.tipodocto = 'CPF'");
                query.Append("   and f.CODINTFUNC = q.codintfunc(+)");
                query.Append(" group by f.CodIntFunc, f.NomeFunc, f.CodFunc, f.ChapaFunc, f.DescArea, f.SituacaoFunc ");
                query.Append("     , f.SALBASE, f.DTADMFUNC, d.nrdocto, f.DTNASCTOFUNC, f.SEXOFUNC  ");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    if (usuarioReader.Read())
                    {
                        _funcionarios.Nome = usuarioReader["NomeFunc"].ToString();
                        _funcionarios.Codigo = usuarioReader["CodFunc"].ToString();
                        _funcionarios.Chapa = usuarioReader["ChapaFunc"].ToString();
                        _funcionarios.Area = usuarioReader["DescArea"].ToString();
                        _funcionarios.Sexo = usuarioReader["SEXOFUNC"].ToString();
                        _funcionarios.Id = Convert.ToInt32(usuarioReader["CodIntFunc"].ToString());
                        _funcionarios.Existe = true;
                        _funcionarios.Ativo = usuarioReader["SituacaoFunc"].ToString() == "A";
                        _funcionarios.DataAdmissao = Convert.ToDateTime(usuarioReader["DTADMFUNC"].ToString());
                        _funcionarios.DataNascimento = Convert.ToDateTime(usuarioReader["DTNASCTOFUNC"].ToString());
                        try
                        {
                            _funcionarios.DataDesligamento = Convert.ToDateTime(usuarioReader["dtdesligquita"].ToString());
                        }
                        catch { }
                        _funcionarios.CPF = usuarioReader["nrdocto"].ToString();
                        _funcionarios.Salario = Convert.ToDecimal(usuarioReader["SALBASE"].ToString());
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
            return _funcionarios;
        }

        public FuncionariosGlobus ConsultaFuncionarioGlobus(decimal codintFunc)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            FuncionariosGlobus _funcionarios = new FuncionariosGlobus();

            try
            {
                query.Clear();
                query.Append("Select f.CodIntFunc, f.NomeFunc, f.CodFunc, f.ChapaFunc, f.DescArea, f.SituacaoFunc, f.SEXOFUNC ");
                query.Append("     , f.SALBASE, f.DTADMFUNC, d.nrdocto, f.DTNASCTOFUNC, Max(q.dtdesligquita) dtdesligquita");
                query.Append("  from vw_funcionarios f");
                query.Append("     , flp_documentos d , flp_quitacao q");
                query.Append(" Where f.CodIntFunc = '" + codintFunc + "'");
                query.Append("   and d.codintfunc = f.CODINTFUNC(+) ");
                query.Append("   and d.tipodocto = 'CPF'");
                query.Append("   and f.CODINTFUNC = q.codintfunc(+)");
                query.Append(" group by f.CodIntFunc, f.NomeFunc, f.CodFunc, f.ChapaFunc, f.DescArea, f.SituacaoFunc ");
                query.Append("     , f.SALBASE, f.DTADMFUNC, d.nrdocto, f.DTNASCTOFUNC, f.SEXOFUNC  ");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    if (usuarioReader.Read())
                    {
                        _funcionarios.Nome = usuarioReader["NomeFunc"].ToString();
                        _funcionarios.Codigo = usuarioReader["CodFunc"].ToString();
                        _funcionarios.Chapa = usuarioReader["ChapaFunc"].ToString();
                        _funcionarios.Area = usuarioReader["DescArea"].ToString();
                        _funcionarios.Sexo = usuarioReader["SEXOFUNC"].ToString();
                        _funcionarios.Id = Convert.ToInt32(usuarioReader["CodIntFunc"].ToString());
                        _funcionarios.Existe = true;
                        _funcionarios.Ativo = usuarioReader["SituacaoFunc"].ToString() == "A";
                        _funcionarios.DataAdmissao = Convert.ToDateTime(usuarioReader["DTADMFUNC"].ToString());
                        _funcionarios.DataNascimento = Convert.ToDateTime(usuarioReader["DTNASCTOFUNC"].ToString());
                        try
                        {
                            _funcionarios.DataDesligamento = Convert.ToDateTime(usuarioReader["dtdesligquita"].ToString());
                        }
                        catch { }
                        _funcionarios.CPF = usuarioReader["nrdocto"].ToString();
                        _funcionarios.Salario = Convert.ToDecimal(usuarioReader["SALBASE"].ToString());
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
            return _funcionarios;
        }
    }
}
