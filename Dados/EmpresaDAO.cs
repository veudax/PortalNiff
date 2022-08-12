using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class EmpresaDAO
    {
        IDataReader empresaReader;

        public List<Empresa> TrazEmpresa(bool todas)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Empresa> _lista = new List<Empresa>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IDEMPRESA ");
                query.Append("     , NOME");
                query.Append("     , ativo, telefoneSAC, senha");
                query.Append("     , CODIGOGLOBUS, TextoSacpadrao, FormatoCodigoSAC, Separador");
                query.Append("     , NomeAbreviado, email, smtp, Autentica, autenticaSSL, PortaSMTP ");
                query.Append("     , Nvl(ValorDescFaltaJustificada,0) ValorDescFaltaJustificada ");
                query.Append("     , Nvl(ValorDescFaltaInjustificada,0) ValorDescFaltaInjustificada ");
                query.Append("     , Nvl(QtdfaltasJustificadasSuperior,0) QtdfaltasJustificadasSuperior ");
                query.Append("     , Nvl(QtdfaltasInjustifSuperior,0) QtdfaltasInjustifSuperior ");
                query.Append("     , codigoGlobus || ' - ' || NomeAbreviado empresa, ExibirOsCanceladosNoSACDias");
                query.Append("     , ResponderEmDiasSAC, SemretornoEmDiasSAC, AvaliaColaboradores, Zero800, AtendenteRespEmDiasSAC");
                query.Append("  From NIFF_CHM_Empresas ");

                if (!todas)
                    query.Append("  Where ativo = 'S' ");

                Query executar = sessao.CreateQuery(query.ToString());

                empresaReader = executar.ExecuteQuery();

                using (empresaReader)
                {
                    while (empresaReader.Read())
                    {
                        Empresa _empresa = new Empresa();

                        _empresa.IdEmpresa = Convert.ToInt32(empresaReader["IdEmpresa"].ToString()); //analisando empresa '2'

                        _empresa.Nome = empresaReader["Nome"].ToString();
                        _empresa.Ativo = empresaReader["ativo"].ToString() == "S";
                        _empresa.CodigoEmpresaGlobus = empresaReader["CODIGOGLOBUS"].ToString();
                        _empresa.NomeAbreviado = empresaReader["NomeAbreviado"].ToString();

                        _empresa.CodigoeNome = _empresa.CodigoEmpresaGlobus + " - " + _empresa.NomeAbreviado;

                        _empresa.TextoPadraoSAC = empresaReader["TextoSacpadrao"].ToString();
                        _empresa.Separador = empresaReader["Separador"].ToString();
                        _empresa.Email = empresaReader["email"].ToString();
                        _empresa.Smtp = empresaReader["Smtp"].ToString();
                        _empresa.Autentica = empresaReader["Autentica"].ToString() == "S";
                        _empresa.AutenticaSLL = empresaReader["AutenticaSSL"].ToString() == "S";
                        _empresa.AvaliaColaboradores = empresaReader["AvaliaColaboradores"].ToString() == "S";
                        _empresa.Zero800 = empresaReader["Zero800"].ToString() == "S";
                        _empresa.Telefone = empresaReader["telefoneSAC"].ToString();

                        try
                        {
                            if (_empresa.Zero800)
                                _empresa.Telefone = string.Format("{0:0000-000-0000}", Convert.ToDecimal(_empresa.Telefone));
                            else
                                _empresa.Telefone = string.Format("{0:(00) 0000-0000}", Convert.ToDecimal(_empresa.Telefone));
                        }
                        catch { }

                        _empresa.Senha = empresaReader["Senha"].ToString();
                        _empresa.ValorDescontoSobreFaltaInjustificada = Convert.ToDecimal(empresaReader["ValorDescFaltaInjustificada"].ToString());
                        _empresa.ValorDescontoSobreFaltaJustificada = Convert.ToDecimal(empresaReader["ValorDescFaltaJustificada"].ToString());
                        _empresa.QuantidadeFaltasJustificadasSuperior = Convert.ToInt32(empresaReader["QtdfaltasJustificadasSuperior"].ToString());
                        _empresa.QuantidadeFaltasInjustificadasSuperior = Convert.ToInt32(empresaReader["QtdfaltasInjustifSuperior"].ToString());
                        _empresa.PortaSmtp = Convert.ToInt32(empresaReader["PortaSMTP"].ToString());
                        _empresa.CodigoeNome = empresaReader["Empresa"].ToString();
                        _empresa.QuantidadeDiasCanceladoNoGrid = Convert.ToInt32(empresaReader["ExibirOsCanceladosNoSACDias"].ToString());
                        _empresa.QuantidadeDiasSemRetornoNoGrid = Convert.ToInt32(empresaReader["SemretornoEmDiasSAC"].ToString());
                        _empresa.QuantidadeDiasParaResponder = Convert.ToInt32(empresaReader["ResponderEmDiasSAC"].ToString());
                        _empresa.AtendenteRespEmDiasSAC = Convert.ToInt32(empresaReader["AtendenteRespEmDiasSAC"].ToString());

                        switch (empresaReader["FormatoCodigoSAC"].ToString())
                        {
                            case "A": _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.Ano;
                                break;
                            case "AM":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.AnoMes;
                                break;
                            case "EA":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.EmpresaAno;
                                break;
                            case "EM":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes;
                                break;
                            case "S":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.Sequencial;
                                break;
                        }
                        
                        _empresa.Existe = true;

                        _lista.Add(_empresa);
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

        public Empresa ConsultaEmpresa(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Empresa _empresa = new Empresa();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IDEMPRESA ");
                query.Append("     , NOME");
                query.Append("     , ativo, telefoneSAC, senha");
                query.Append("     , CODIGOGLOBUS, TextoSacpadrao, FormatoCodigoSAC, Separador");
                query.Append("     , NomeAbreviado, email, smtp, Autentica, autenticaSSL, PortaSMTP ");
                query.Append("     , Nvl(ValorDescFaltaJustificada,0) ValorDescFaltaJustificada ");
                query.Append("     , Nvl(ValorDescFaltaInjustificada,0) ValorDescFaltaInjustificada ");
                query.Append("     , Nvl(QtdfaltasJustificadasSuperior,0) QtdfaltasJustificadasSuperior ");
                query.Append("     , Nvl(QtdfaltasInjustifSuperior,0) QtdfaltasInjustifSuperior ");
                query.Append("     , ExibirOsCanceladosNoSACDias ");
                query.Append("     , ResponderEmDiasSAC, SemretornoEmDiasSAC, AvaliaColaboradores, Zero800, AtendenteRespEmDiasSAC");


                query.Append("  From NIFF_CHM_Empresas ");
                query.Append(" Where IdEmpresa = " + codigo.ToString() );

                Query executar = sessao.CreateQuery(query.ToString());

                empresaReader = executar.ExecuteQuery();

                using (empresaReader)
                {
                    if (empresaReader.Read())
                    {
                        _empresa.IdEmpresa = Convert.ToInt32(empresaReader["IdEmpresa"].ToString());

                        _empresa.Nome = empresaReader["Nome"].ToString();
                        _empresa.Ativo = empresaReader["ativo"].ToString() == "S";
                        _empresa.CodigoEmpresaGlobus = empresaReader["CODIGOGLOBUS"].ToString();
                        _empresa.TextoPadraoSAC = empresaReader["TextoSacpadrao"].ToString();
                        _empresa.Separador = empresaReader["Separador"].ToString();
                        _empresa.NomeAbreviado = empresaReader["NomeAbreviado"].ToString();
                        _empresa.CodigoeNome = _empresa.CodigoEmpresaGlobus + " - " + _empresa.NomeAbreviado;

                        _empresa.Email = empresaReader["email"].ToString();
                        _empresa.Smtp = empresaReader["Smtp"].ToString();
                        _empresa.Autentica = empresaReader["Autentica"].ToString() == "S";
                        _empresa.AutenticaSLL = empresaReader["AutenticaSSL"].ToString() == "S";
                        _empresa.AvaliaColaboradores = empresaReader["AvaliaColaboradores"].ToString() == "S";
                        _empresa.Zero800 = empresaReader["Zero800"].ToString() == "S";

                        _empresa.Telefone = empresaReader["telefoneSAC"].ToString();

                        try
                        {
                            if (_empresa.Zero800)
                                _empresa.Telefone = string.Format("{0:0000-000-0000}", Convert.ToDecimal(_empresa.Telefone));
                            else
                                _empresa.Telefone = string.Format("{0:(00) 0000-0000}", Convert.ToDecimal(_empresa.Telefone));
                                   
                        }
                        catch { }

                        _empresa.Senha = empresaReader["Senha"].ToString();
                        _empresa.PortaSmtp = Convert.ToInt32(empresaReader["PortaSMTP"].ToString());

                        _empresa.ValorDescontoSobreFaltaInjustificada = Convert.ToDecimal(empresaReader["ValorDescFaltaInjustificada"].ToString());
                        _empresa.ValorDescontoSobreFaltaJustificada = Convert.ToDecimal(empresaReader["ValorDescFaltaJustificada"].ToString());
                        _empresa.QuantidadeFaltasJustificadasSuperior = Convert.ToInt32(empresaReader["QtdfaltasJustificadasSuperior"].ToString());
                        _empresa.QuantidadeFaltasInjustificadasSuperior = Convert.ToInt32(empresaReader["QtdfaltasInjustifSuperior"].ToString());
                        _empresa.QuantidadeDiasCanceladoNoGrid = Convert.ToInt32(empresaReader["ExibirOsCanceladosNoSACDias"].ToString());
                        _empresa.QuantidadeDiasCanceladoNoGrid = Convert.ToInt32(empresaReader["ExibirOsCanceladosNoSACDias"].ToString());
                        _empresa.QuantidadeDiasSemRetornoNoGrid = Convert.ToInt32(empresaReader["SemretornoEmDiasSAC"].ToString());
                        _empresa.QuantidadeDiasParaResponder = Convert.ToInt32(empresaReader["ResponderEmDiasSAC"].ToString());
                        _empresa.AtendenteRespEmDiasSAC = Convert.ToInt32(empresaReader["AtendenteRespEmDiasSAC"].ToString());

                        switch (empresaReader["FormatoCodigoSAC"].ToString())
                        {
                            case "A":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.Ano;
                                break;
                            case "AM":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.AnoMes;
                                break;
                            case "EA":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.EmpresaAno;
                                break;
                            case "EM":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes;
                                break;
                            case "S":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.Sequencial;
                                break;
                        }
                        _empresa.Existe = true;
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
            return _empresa;
        }

        public Empresa ConsultaEmpresa(string codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Empresa _empresa = new Empresa();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select IDEMPRESA ");
                query.Append("     , NOME");
                query.Append("     , ativo, telefoneSAC, senha");
                query.Append("     , CODIGOGLOBUS, TextoSacpadrao, FormatoCodigoSAC, Separador");
                query.Append("     , NomeAbreviado, email, smtp, Autentica, autenticaSSL, PortaSMTP ");
                query.Append("     , Nvl(ValorDescFaltaJustificada,0) ValorDescFaltaJustificada ");
                query.Append("     , Nvl(ValorDescFaltaInjustificada,0) ValorDescFaltaInjustificada ");
                query.Append("     , Nvl(QtdfaltasJustificadasSuperior,0) QtdfaltasJustificadasSuperior ");
                query.Append("     , Nvl(QtdfaltasInjustifSuperior,0) QtdfaltasInjustifSuperior ");
                query.Append("     , ExibirOsCanceladosNoSACDias ");
                query.Append("     , ResponderEmDiasSAC, SemretornoEmDiasSAC, AvaliaColaboradores, Zero800, AtendenteRespEmDiasSAC");


                query.Append("  From NIFF_CHM_Empresas ");
                query.Append(" Where CODIGOGLOBUS = '" + codigo.ToString() + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                empresaReader = executar.ExecuteQuery();

                using (empresaReader)
                {
                    if (empresaReader.Read())
                    {
                        _empresa.IdEmpresa = Convert.ToInt32(empresaReader["IdEmpresa"].ToString());

                        _empresa.Nome = empresaReader["Nome"].ToString();
                        _empresa.Ativo = empresaReader["ativo"].ToString() == "S";
                        _empresa.CodigoEmpresaGlobus = empresaReader["CODIGOGLOBUS"].ToString();
                        _empresa.TextoPadraoSAC = empresaReader["TextoSacpadrao"].ToString();
                        _empresa.Separador = empresaReader["Separador"].ToString();
                        _empresa.NomeAbreviado = empresaReader["NomeAbreviado"].ToString();
                        _empresa.CodigoeNome = _empresa.CodigoEmpresaGlobus + " - " + _empresa.NomeAbreviado;

                        _empresa.Email = empresaReader["email"].ToString();
                        _empresa.Smtp = empresaReader["Smtp"].ToString();
                        _empresa.Autentica = empresaReader["Autentica"].ToString() == "S";
                        _empresa.AutenticaSLL = empresaReader["AutenticaSSL"].ToString() == "S";
                        _empresa.AvaliaColaboradores = empresaReader["AvaliaColaboradores"].ToString() == "S";
                        _empresa.Zero800 = empresaReader["Zero800"].ToString() == "S";

                        _empresa.Telefone = empresaReader["telefoneSAC"].ToString();

                        try
                        {
                            if (_empresa.Zero800)
                                _empresa.Telefone = string.Format("{0:0000-000-0000}", Convert.ToDecimal(_empresa.Telefone));
                            else
                                _empresa.Telefone = string.Format("{0:(00) 0000-0000}", Convert.ToDecimal(_empresa.Telefone));

                        }
                        catch { }

                        _empresa.Senha = empresaReader["Senha"].ToString();
                        _empresa.PortaSmtp = Convert.ToInt32(empresaReader["PortaSMTP"].ToString());

                        _empresa.ValorDescontoSobreFaltaInjustificada = Convert.ToDecimal(empresaReader["ValorDescFaltaInjustificada"].ToString());
                        _empresa.ValorDescontoSobreFaltaJustificada = Convert.ToDecimal(empresaReader["ValorDescFaltaJustificada"].ToString());
                        _empresa.QuantidadeFaltasJustificadasSuperior = Convert.ToInt32(empresaReader["QtdfaltasJustificadasSuperior"].ToString());
                        _empresa.QuantidadeFaltasInjustificadasSuperior = Convert.ToInt32(empresaReader["QtdfaltasInjustifSuperior"].ToString());
                        _empresa.QuantidadeDiasCanceladoNoGrid = Convert.ToInt32(empresaReader["ExibirOsCanceladosNoSACDias"].ToString());
                        _empresa.QuantidadeDiasCanceladoNoGrid = Convert.ToInt32(empresaReader["ExibirOsCanceladosNoSACDias"].ToString());
                        _empresa.QuantidadeDiasSemRetornoNoGrid = Convert.ToInt32(empresaReader["SemretornoEmDiasSAC"].ToString());
                        _empresa.QuantidadeDiasParaResponder = Convert.ToInt32(empresaReader["ResponderEmDiasSAC"].ToString());
                        _empresa.AtendenteRespEmDiasSAC = Convert.ToInt32(empresaReader["AtendenteRespEmDiasSAC"].ToString());

                        switch (empresaReader["FormatoCodigoSAC"].ToString())
                        {
                            case "A":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.Ano;
                                break;
                            case "AM":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.AnoMes;
                                break;
                            case "EA":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.EmpresaAno;
                                break;
                            case "EM":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes;
                                break;
                            case "S":
                                _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.Sequencial;
                                break;
                        }
                        _empresa.Existe = true;
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
            return _empresa;
        }

        public bool GravaEmpresa(Empresa empresa, List<EmailEnvioComunicado> listaEmail)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Empresa _empresa = new Empresa();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                if (!empresa.Existe)
                {
                    query.Clear();
                    query.Append("Insert into NIFF_CHM_Empresas");
                    query.Append("   (idEmpresa, nome, ativo, CODIGOGLOBUS, TextoSacpadrao, FormatoCodigoSAC, Separador, NomeAbreviado, ");
                    query.Append("       email, smtp, Autentica, autenticaSSL, PortaSMTP, telefoneSAC, senha, ");
                    query.Append("       ValorDescFaltaJustificada, ValorDescFaltaInjustificada, ");
                    query.Append("       QtdfaltasJustificadasSuperior, QtdfaltasInjustifSuperior, ExibirOsCanceladosNoSACDias, ");
                    query.Append("       ResponderEmDiasSAC, SemretornoEmDiasSAC, AvaliaColaboradores, Zero800, AtendenteRespEmDiasSAC ");
                    query.Append("  ) Values (" + empresa.IdEmpresa);
                    query.Append(", '" + empresa.Nome + "'");
                    query.Append(", '" + (empresa.Ativo ? "S" : "N") + "'");
                    query.Append(", '" + empresa.CodigoEmpresaGlobus + "'");
                    query.Append(", '" + empresa.TextoPadraoSAC + "'");
                    query.Append(", '" + (empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.Ano ? "A" :
                                          (empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.AnoMes ? "AM" :
                                           (empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAno ? "EA" :
                                            (empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes ? "EM" : "S")))) + "'");
                    query.Append(", '" + empresa.Separador + "', '" + empresa.NomeAbreviado + "' ");
                    query.Append(", '" + empresa.Email + "', '" + empresa.Smtp + "'");
                    query.Append(", '" + (empresa.Autentica ? "S" : "N") + "', '" + (empresa.AutenticaSLL ? "S" : "N") + "'");
                    query.Append(", " + empresa.PortaSmtp);
                    query.Append(", '" + empresa.Telefone + "' ");
                    query.Append(", '" + empresa.Senha + "' ");
                    query.Append(", " + empresa.ValorDescontoSobreFaltaJustificada.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(", " + empresa.ValorDescontoSobreFaltaInjustificada.ToString().Replace(".", "").Replace(",", "."));
                    query.Append(", " + empresa.QuantidadeFaltasJustificadasSuperior);
                    query.Append(", " + empresa.QuantidadeFaltasInjustificadasSuperior);
                    query.Append(", " + empresa.QuantidadeDiasCanceladoNoGrid);
                    query.Append(", " + empresa.QuantidadeDiasSemRetornoNoGrid);
                    query.Append(", " + empresa.QuantidadeDiasParaResponder);
                    query.Append(", '" + (empresa.AvaliaColaboradores ? "S" : "N") + "'");
                    query.Append(", '" + (empresa.Zero800? "S" : "N") + "'");
                    query.Append(", " + empresa.AtendenteRespEmDiasSAC);
                    query.Append(" )");
                }
                else
                {
                    query.Clear();
                    query.Append("Update NIFF_CHM_Empresas");
                    query.Append("   set nome = '" + empresa.Nome + "', ");
                    query.Append("       ativo = '" + (empresa.Ativo ? "S" : "N") + "', ");
                    query.Append("       CODIGOGLOBUS = '" + empresa.CodigoEmpresaGlobus + "', ");
                    query.Append("       TextoSacpadrao = '" + empresa.TextoPadraoSAC + "', ");
                    query.Append("       FormatoCodigoSAC = '" + (empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.Ano ? "A" :
                                                                  (empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.AnoMes ? "AM" :
                                                                   (empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAno ? "EA" :
                                                                    (empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes ? "EM" : "S")))) + "', ");

                    query.Append("       Separador = '" + empresa.Separador + "', ");
                    query.Append("       NomeAbreviado = '" + empresa.NomeAbreviado + "', ");
                    query.Append("       Email = '" + empresa.Email + "', ");
                    query.Append("       Smtp = '" + empresa.Smtp + "', ");
                    query.Append("       Autentica = '" + (empresa.Autentica ? "S" : "N") + "', ");
                    query.Append("       AutenticaSSL = '" + (empresa.AutenticaSLL ? "S" : "N") + "', ");
                    query.Append("       PortaSMTP = " + empresa.PortaSmtp + ", ");
                    query.Append("       TelefoneSAC = '" + empresa.Telefone + "', ");
                    query.Append("       Senha = '" + empresa.Senha + "', ");
                    query.Append("       ValorDescFaltaJustificada = " + empresa.ValorDescontoSobreFaltaJustificada.ToString().Replace(".","").Replace(",",".") + ", "); 
                    query.Append("       ValorDescFaltaInjustificada = " + empresa.ValorDescontoSobreFaltaInjustificada.ToString().Replace(".", "").Replace(",", ".") + ", ");
                    query.Append("       QtdfaltasJustificadasSuperior = " + empresa.QuantidadeFaltasJustificadasSuperior + ", ");
                    query.Append("       QtdfaltasInjustifSuperior = " + empresa.QuantidadeFaltasInjustificadasSuperior + ", ");
                    query.Append("       ExibirOsCanceladosNoSACDias = " + empresa.QuantidadeDiasCanceladoNoGrid + ", ");
                    query.Append("       SemretornoEmDiasSAC = " + empresa.QuantidadeDiasSemRetornoNoGrid + ", ");
                    query.Append("       ResponderEmDiasSAC = " + empresa.QuantidadeDiasParaResponder + ", ");
                    query.Append("       AvaliaColaboradores  = '" + (empresa.AvaliaColaboradores ? "S" : "N") + "', ");
                    query.Append("       Zero800 = '" + (empresa.Zero800 ? "S" : "N") + "'");
                    query.Append("    , AtendenteRespEmDiasSAC = " + empresa.AtendenteRespEmDiasSAC);
                    query.Append(" Where idEmpresa = " + empresa.IdEmpresa);
                }

                retorno = sessao.ExecuteSqlTransaction(query.ToString());

                listaEmail.ForEach(w => w.IdEmpresa = empresa.IdEmpresa);

                if (retorno)
                {
                    retorno = new EmailEnvioComunicadoDAO().Gravar(listaEmail);
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
        
        public bool ExcluiEmpresa(Empresa empresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Empresa _empresa = new Empresa();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = true;

            try
            {
                if (empresa.IdEmpresa != 0)
                {
                    retorno = new EmailEnvioComunicadoDAO().Excluir(empresa.IdEmpresa);

                    if (retorno)
                    {

                        query.Append("Delete NIFF_CHM_Empresas");
                        query.Append(" Where idEmpresa = " + empresa.IdEmpresa);

                        retorno = sessao.ExecuteSqlTransaction(query.ToString());
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

        public int Proximo()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            int retorno = 1;
            try
            {

                query.Append("Select Max(IdEmpresa) + 1 next From niff_chm_Empresas");
                Query executar = sessao.CreateQuery(query.ToString());

                empresaReader = executar.ExecuteQuery();

                using (empresaReader)
                {
                    if (empresaReader.Read())
                        retorno = Convert.ToInt32(empresaReader["next"].ToString());
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
    }
}
