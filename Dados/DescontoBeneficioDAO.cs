using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class DescontoBeneficioDAO
    {
        IDataReader atendimentoReader;

        public List<DescontoBeneficios> CalculaDescontoPorFerias(DateTime inicio, DateTime fim, List<EmpresaDoUsuario> empresas, List<OcorrenciasGlobus> justificadas, List<OcorrenciasGlobus> injustificadas)
        {
            List<DescontoBeneficios> _lista = new List<DescontoBeneficios>();
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            string _empresas = "";
            string _justificadas = "";
            string _injustificadas = "";

            foreach (var item in empresas)
            {
                _empresas = _empresas + "'" + item.CodigoEmpresaGlobus + "',";
            }

            _empresas = _empresas.Substring(0, _empresas.Length - 1);

            foreach (var item in justificadas)
            {
                _justificadas = _justificadas + item.Codigo + ",";
            }

            _justificadas = _justificadas.Substring(0, _justificadas.Length - 1);

            foreach (var item in injustificadas)
            {
                _injustificadas = _injustificadas + item.Codigo + ",";
            }

            _injustificadas = _injustificadas.Substring(0, _injustificadas.Length - 1);

            try
            {

                query.Append("Select codintfunc, codfunc, NOMEFUNC,");
                query.Append("       Gozoinifer, gozofinfer, aquiinifer,");
                query.Append("       aquifinfer, DTNASCTOFUNC, nrdocto,");
                query.Append("       QtdInjustificada,");
                query.Append("       QtdJustificada,");
                query.Append("       LPad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') Empresa");
                query.Append("  From (Select codintfunc, codfunc, NOMEFUNC, Gozoinifer, gozofinfer, aquiinifer,");
                query.Append("               aquifinfer, DTNASCTOFUNC, nrdocto, CodigoEmpresa, CodigoFl,");
                query.Append("               Sum(QtdInjustificada) QtdInjustificada,");
                query.Append("               Sum(QtdJustificada) QtdJustificada");
                query.Append("          From (Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC, f.Gozoinifer, f.gozofinfer, f.aquiinifer,");
                query.Append("                       f.aquifinfer, d.dtdigit, fu.DTNASCTOFUNC, dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,");
                query.Append("                       1 QtdInjustificada, 0 QtdJustificada ");
                query.Append("                  From Flp_Ferias f, ");
                query.Append("                       Vw_Funcionarios Fu,");
                query.Append("                       Flp_Documentos dc,");
                query.Append("                       Frq_Digitacaomovimento d");
                query.Append("                 Where Fu.Codintfunc = f.Codintfunc");
                query.Append("                   And f.Gozoinifer Between To_date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("                   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("                   And Fu.Situacaofunc = 'A'");
                query.Append("                   And dc.codintfunc = fu.CODINTFUNC");
                query.Append("                   And dc.tipodocto = 'CPF'");
                query.Append("                   And fu.CODINTFUNC = d.codintfunc");
                query.Append("                   And trunc(d.dtdigit) Between f.aquiinifer And f.aquifinfer");
                query.Append("                   And LPad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') In ( " + _empresas + " )");
                query.Append("                   And d.codocorr In ( " + _injustificadas + " )");
                query.Append("                   And f.statusferias = 'N'");
                query.Append("                   And d.tipodigit = 'F'");
                query.Append("                   And d.statusdigit = 'N'");
                query.Append("                 Union All ");
                query.Append("                Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC, f.Gozoinifer, f.gozofinfer, f.aquiinifer,");
                query.Append("                       f.aquifinfer, d.dtdigit, fu.DTNASCTOFUNC, dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,");
                query.Append("                       0 QtdInjustificada, 1 QtdJustificada");
                query.Append("                  From Flp_Ferias f,");
                query.Append("                       Vw_Funcionarios Fu, ");
                query.Append("                       Flp_Documentos dc,");
                query.Append("                       Frq_Digitacaomovimento d");
                query.Append("                 Where Fu.Codintfunc = f.Codintfunc");
                query.Append("                   And f.Gozoinifer Between To_date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("                   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("                   And Fu.Situacaofunc = 'A'");
                query.Append("                   And dc.codintfunc = fu.CODINTFUNC");
                query.Append("                   And dc.tipodocto = 'CPF'");
                query.Append("                   And fu.CODINTFUNC = d.codintfunc");
                query.Append("                   And trunc(d.dtdigit) Between f.aquiinifer And f.aquifinfer");
                query.Append("                   And LPad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') In ( " + _empresas + " )");
                query.Append("                   And d.codocorr In ( " + _justificadas + " )");
                query.Append("                   And f.statusferias = 'N'");
                query.Append("                   And d.tipodigit = 'F'");
                query.Append("                   And d.statusdigit = 'N' )");
                query.Append("         Group By codintfunc, codfunc, NOMEFUNC, CodigoEmpresa, CodigoFl, Gozoinifer, gozofinfer, aquiinifer,");
                query.Append("                  aquifinfer, DTNASCTOFUNC, nrdocto ) ");

                Query executar = sessao.CreateQuery(query.ToString());

                atendimentoReader = executar.ExecuteQuery();

                using (atendimentoReader)
                {
                    while (atendimentoReader.Read())
                    {
                        DescontoBeneficios _desconto = new DescontoBeneficios();

                        _desconto.Codigo = atendimentoReader["codfunc"].ToString();
                        _desconto.Id = Convert.ToInt32(atendimentoReader["codintfunc"].ToString());
                        _desconto.Nome = atendimentoReader["NOMEFUNC"].ToString();

                        _desconto.InicioFerias = Convert.ToDateTime( atendimentoReader["Gozoinifer"].ToString());
                        _desconto.FimFerias = Convert.ToDateTime(atendimentoReader["gozofinfer"].ToString());
                        _desconto.InicioAquisicao = Convert.ToDateTime(atendimentoReader["aquiinifer"].ToString());
                        _desconto.FimAquisicao = Convert.ToDateTime(atendimentoReader["aquifinfer"].ToString());
                        _desconto.DataNascimento = Convert.ToDateTime(atendimentoReader["DTNASCTOFUNC"].ToString());

                        _desconto.QuantidadeFaltasJustificadas = Convert.ToInt32(atendimentoReader["QtdJustificada"].ToString());
                        _desconto.QuantidadeFaltasInJustificadas = Convert.ToInt32(atendimentoReader["QtdInjustificada"].ToString());

                        _desconto.Empresa = atendimentoReader["Empresa"].ToString();
                        _desconto.CPF = atendimentoReader["nrdocto"].ToString();

                        foreach (var item in empresas.Where(w => w.CodigoEmpresaGlobus == _desconto.Empresa))
                        {
                            _desconto.QuantidadeFaltasDescontadasJustificadas = 0;
                            if (_desconto.QuantidadeFaltasJustificadas > item.QuantidadeFaltasJustificadasSuperior)
                                _desconto.QuantidadeFaltasDescontadasJustificadas = (_desconto.QuantidadeFaltasJustificadas - item.QuantidadeFaltasJustificadasSuperior);

                            _desconto.ValorADescontarFaltasJustificadas = _desconto.QuantidadeFaltasDescontadasJustificadas * item.ValorDescontoSobreFaltaJustificada;

                            _desconto.QuantidadeFaltasDescontadasInjustificadas = 0;
                            if (_desconto.QuantidadeFaltasInJustificadas > item.QuantidadeFaltasInjustificadasSuperior)
                                _desconto.QuantidadeFaltasDescontadasInjustificadas = (_desconto.QuantidadeFaltasInJustificadas - item.QuantidadeFaltasInjustificadasSuperior);

                            _desconto.ValorADescontarFaltasInjustificadas = _desconto.QuantidadeFaltasDescontadasJustificadas * item.ValorDescontoSobreFaltaInjustificada;
                        }

                        _lista.Add(_desconto);
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

        public List<DescontoBeneficios> CalculaDesconto(DateTime inicio, DateTime fim, List<EmpresaDoUsuario> empresas, List<OcorrenciasGlobus> justificadas, List<OcorrenciasGlobus> injustificadas)
        {
            List<DescontoBeneficios> _lista = new List<DescontoBeneficios>();
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            string _empresas = "";
            string _justificadas = "";
            string _injustificadas = "";

            foreach (var item in empresas)
            {
                _empresas = _empresas + "'" + item.CodigoEmpresaGlobus + "',";
            }

            _empresas = _empresas.Substring(0, _empresas.Length - 1);

            foreach (var item in justificadas)
            {
                _justificadas = _justificadas + item.Codigo + ",";
            }

            _justificadas = _justificadas.Substring(0, _justificadas.Length - 1);

            foreach (var item in injustificadas)
            {
                _injustificadas = _injustificadas + item.Codigo + ",";
            }

            _injustificadas = _injustificadas.Substring(0, _injustificadas.Length - 1);

            try
            {

                query.Append("Select codintfunc, codfunc, NOMEFUNC,");
                query.Append("       dtdigit, DTNASCTOFUNC, nrdocto,");
                query.Append("       QtdInjustificada,");
                query.Append("       QtdJustificada,");
                query.Append("       LPad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') Empresa");
                query.Append("  From (Select codintfunc, codfunc, NOMEFUNC, dtdigit, ");
                query.Append("               DTNASCTOFUNC, nrdocto, CodigoEmpresa, CodigoFl,");
                query.Append("               Sum(QtdInjustificada) QtdInjustificada,");
                query.Append("               Sum(QtdJustificada) QtdJustificada");
                query.Append("          From (Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC, ");
                query.Append("                       d.dtdigit, fu.DTNASCTOFUNC, dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,");
                query.Append("                       1 QtdInjustificada, 0 QtdJustificada ");
                query.Append("                  From Vw_Funcionarios Fu,");
                query.Append("                       Flp_Documentos dc,");
                query.Append("                       Frq_Digitacaomovimento d");
                query.Append("                 Where d.dtdigit Between To_date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("                   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("                   And Fu.Situacaofunc = 'A'");
                query.Append("                   And dc.codintfunc = fu.CODINTFUNC");
                query.Append("                   And dc.tipodocto = 'CPF'");
                query.Append("                   And fu.CODINTFUNC = d.codintfunc");
                query.Append("                   And LPad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') In ( " + _empresas + " )");
                query.Append("                   And d.codocorr In ( " + _injustificadas + " )");
                query.Append("                   And d.tipodigit = 'F'");
                query.Append("                   And d.statusdigit = 'N'");
                query.Append("                 Union All ");
                query.Append("                Select fu.codintfunc, fu.codfunc, fu.NOMEFUNC, ");
                query.Append("                       d.dtdigit, fu.DTNASCTOFUNC, dc.nrdocto, fu.CODIGOEMPRESA, fu.codigofl,");
                query.Append("                       0 QtdInjustificada, 1 QtdJustificada");
                query.Append("                  From Vw_Funcionarios Fu, ");
                query.Append("                       Flp_Documentos dc,");
                query.Append("                       Frq_Digitacaomovimento d");
                query.Append("                 Where d.dtdigit Between To_date('" + inicio.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("                   And To_date('" + fim.ToShortDateString() + "', 'dd/mm/yyyy')");
                query.Append("                   And Fu.Situacaofunc = 'A'");
                query.Append("                   And dc.codintfunc = fu.CODINTFUNC");
                query.Append("                   And dc.tipodocto = 'CPF'");
                query.Append("                   And fu.CODINTFUNC = d.codintfunc");
                query.Append("                   And LPad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') In ( " + _empresas + " )");
                query.Append("                   And d.codocorr In ( " + _justificadas + " )");
                query.Append("                   And d.tipodigit = 'F'");
                query.Append("                   And d.statusdigit = 'N' )");
                query.Append("         Group By codintfunc, codfunc, NOMEFUNC, CodigoEmpresa, CodigoFl, ");
                query.Append("                  dtdigit, DTNASCTOFUNC, nrdocto ) ");

                Query executar = sessao.CreateQuery(query.ToString());

                atendimentoReader = executar.ExecuteQuery();

                using (atendimentoReader)
                {
                    while (atendimentoReader.Read())
                    {
                        DescontoBeneficios _desconto = new DescontoBeneficios();

                        _desconto.Codigo = atendimentoReader["codfunc"].ToString();
                        _desconto.Id = Convert.ToInt32(atendimentoReader["codintfunc"].ToString());
                        _desconto.Nome = atendimentoReader["NOMEFUNC"].ToString();

                        _desconto.DataNascimento = Convert.ToDateTime(atendimentoReader["DTNASCTOFUNC"].ToString());

                        _desconto.QuantidadeFaltasJustificadas = Convert.ToInt32(atendimentoReader["QtdJustificada"].ToString());
                        _desconto.QuantidadeFaltasInJustificadas = Convert.ToInt32(atendimentoReader["QtdInjustificada"].ToString());

                        _desconto.Empresa = atendimentoReader["Empresa"].ToString();
                        _desconto.CPF = atendimentoReader["nrdocto"].ToString();

                        foreach (var item in empresas.Where(w => w.CodigoEmpresaGlobus == _desconto.Empresa))
                        {
                            _desconto.QuantidadeFaltasDescontadasJustificadas = 0;
                            if (_desconto.QuantidadeFaltasJustificadas > item.QuantidadeFaltasJustificadasSuperior)
                                _desconto.QuantidadeFaltasDescontadasJustificadas = (_desconto.QuantidadeFaltasJustificadas - item.QuantidadeFaltasJustificadasSuperior);

                            _desconto.ValorADescontarFaltasJustificadas = _desconto.QuantidadeFaltasDescontadasJustificadas * item.ValorDescontoSobreFaltaJustificada;

                            _desconto.QuantidadeFaltasDescontadasInjustificadas = 0;
                            if (_desconto.QuantidadeFaltasInJustificadas > item.QuantidadeFaltasInjustificadasSuperior)
                                _desconto.QuantidadeFaltasDescontadasInjustificadas = (_desconto.QuantidadeFaltasInJustificadas - item.QuantidadeFaltasInjustificadasSuperior);

                            _desconto.ValorADescontarFaltasInjustificadas = _desconto.QuantidadeFaltasDescontadasJustificadas * item.ValorDescontoSobreFaltaInjustificada;
                        }

                        _lista.Add(_desconto);
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
    }
}
