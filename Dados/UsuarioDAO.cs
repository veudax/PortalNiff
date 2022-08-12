using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Classes;
using System.Data.OracleClient;

namespace Dados
{
    public class UsuarioDAO
    {
        IDataReader usuarioReader;

        public List<Usuario> ListaAniversariantesDaSemana(int idEmpresa, int qtdDias, int qtdDiasAntes)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Usuario> _lista = new List<Usuario>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select u.idusuario ");
                query.Append("     , u.nome");
                query.Append("     , u.telefone");
                query.Append("     , u.ramal");
                query.Append("     , u.email");
                query.Append("     , u.dtnascimento");
                query.Append("     , u.foto");
                query.Append("     , e.nome Empresa");
                query.Append("     , e.nomeabreviado EmpresaAbreviada");
                query.Append("     , To_date(decode(dtnascimento, Null, Null, To_Char(dtnascimento, 'dd/mm/') || To_Char(Sysdate, 'yyyy')), 'dd/mm/yyyy') Data"); 

                query.Append("  From NIFF_CHM_Usuarios u, niff_chm_empresas e");
                query.Append(" Where e.Idempresa(+) = u.Idempresa ");
                query.Append("   And To_date(decode(dtnascimento, Null, Null, To_Char(dtnascimento,'dd/mm/') || To_Char(Sysdate,'yyyy')),'dd/mm/yyyy') ");
                query.Append("           Between trunc(Sysdate) - " + qtdDiasAntes + " And trunc(Sysdate) + " + qtdDias);
                query.Append("   And u.ativo = 'S'");

                Empresa _empresa = new EmpresaDAO().ConsultaEmpresa(idEmpresa);

                if (idEmpresa != 0)
                    query.Append("   And u.Idempresa = " + idEmpresa);

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        Usuario _usuario = new Usuario();
                        _usuario.Existe = true;

                        _usuario.Id = Convert.ToInt32(usuarioReader["IdUsuario"].ToString());
                        
                        _usuario.Nome = usuarioReader["Nome"].ToString();
                        _usuario.Empresa = usuarioReader["EmpresaAbreviada"].ToString();
                        try
                        {
                            _usuario.Telefone = Convert.ToDecimal(usuarioReader["telefone"].ToString());
                        }
                        catch { }

                        try
                        {
                            _usuario.Ramal = Convert.ToInt32(usuarioReader["ramal"].ToString());
                        }
                        catch { }

                        _usuario.Email = usuarioReader["email"].ToString();

                        try
                        {
                            _usuario.DataNascimento = Convert.ToDateTime(usuarioReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _usuario.Foto = (byte[])(usuarioReader["foto"]);
                        }
                        catch { }

                        
                        _lista.Add(_usuario);
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

        public Usuario ListaAniversariantesDaEmpresa()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            Usuario _usuario = new Usuario();
            FeriadoEmenda _feriado = null;

            try
            {

                query.Append("Select u.idusuario ");
                query.Append("     , u.nome");
                query.Append("     , u.telefone");
                query.Append("     , u.ramal");
                query.Append("     , u.email");
                query.Append("     , u.dtnascimento");
                query.Append("     , u.foto");
                query.Append("     , e.nome Empresa");
                query.Append("     , e.nomeabreviado EmpresaAbreviada");
                query.Append("     , trunc((trunc(sysdate) - dataAdmissao)/365) QuantidadeDeAnos");

                query.Append("  From NIFF_CHM_Usuarios u, niff_chm_empresas e");
                query.Append(" Where e.Idempresa(+) = u.Idempresa ");

                if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday)
                {
                    // verifica se sexta foi feriado
                    _feriado = new FeriadoDAO().Consulta(DateTime.Now.Date.AddDays(-3), Publicas._usuario.IdEmpresa);

                    if (_feriado.Existe)
                        query.Append("   And to_Char(u.Dataadmissao,'mmdd') between to_char(sysdate-3,'mmdd') and to_char(sysdate,'mmdd')");
                    else
                        query.Append("   And to_Char(u.Dataadmissao,'mmdd') between to_char(sysdate-2,'mmdd') and to_char(sysdate,'mmdd')");
                }
                else
                {
                    _feriado = new FeriadoDAO().Consulta(DateTime.Now.Date.AddDays(-1), Publicas._usuario.IdEmpresa);

                    if (_feriado.Existe)
                        query.Append("   And to_Char(u.Dataadmissao,'mmdd') between to_char(sysdate-1,'mmdd') and to_char(sysdate,'mmdd')");
                    else
                        query.Append("   And to_Char(u.Dataadmissao,'mmdd') = to_char(sysdate,'mmdd')");
                }

                query.Append("   And IdUsuario = " + Publicas._idUsuario);
                query.Append("   And u.ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        _usuario.Existe = true;

                        _usuario.Id = Convert.ToInt32(usuarioReader["IdUsuario"].ToString());

                        _usuario.Nome = usuarioReader["Nome"].ToString();
                        _usuario.Empresa = usuarioReader["EmpresaAbreviada"].ToString();

                        try
                        {
                            _usuario.QuantidadeDeAnos = Convert.ToInt32(usuarioReader["QuantidadeDeAnos"].ToString());
                        }
                        catch { }

                        try
                        {
                            _usuario.Telefone = Convert.ToDecimal(usuarioReader["telefone"].ToString());
                        }
                        catch { }

                        try
                        {
                            _usuario.Ramal = Convert.ToInt32(usuarioReader["ramal"].ToString());
                        }
                        catch { }

                        _usuario.Email = usuarioReader["email"].ToString();

                        try
                        {
                            _usuario.DataNascimento = Convert.ToDateTime(usuarioReader["Data"].ToString());
                        }
                        catch { }

                        try
                        {
                            _usuario.Foto = (byte[])(usuarioReader["foto"]);
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
            return _usuario;
        }

        private StringBuilder MontaConsulta()
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select u.idusuario ");
            query.Append("     , u.tipo");
            query.Append("     , u.nome");
            query.Append("     , u.ativo");
            query.Append("     , u.administrador");
            query.Append("     , u.ipmaquina");
            query.Append("     , u.nomemaquina");
            query.Append("     , u.telefone");
            query.Append("     , u.ramal");
            query.Append("     , u.email");
            query.Append("     , u.usuarioacesso");
            query.Append("     , u.senha");
            query.Append("     , u.cargo");
            query.Append("     , u.dtnascimento");
            query.Append("     , u.foto");
            query.Append("     , u.AcessaAgenda");
            query.Append("     , u.AcessaChat");
            query.Append("     , u.PermiteExcluirChat");
            query.Append("     , u.AcessaBI");
            query.Append("     , u.PermiteIncExcFotosFesta");
            query.Append("     , u.emailAcessoPowerBi");
            query.Append("     , u.IdEmpresa");
            query.Append("     , u.IdDepartamento");
            query.Append("     , u.AcessaSAC");
            query.Append("     , u.TipoUsuarioSAC");
            query.Append("     , u.CodFunc");
            query.Append("     , f.nomefunc");
            query.Append("     , f.chapafunc");
            query.Append("     , f.codfunc registro");
            query.Append("     , u.CPF");
            query.Append("     , u.EmailDepto");
            query.Append("     , u.AcessaDescontoBeneficio");
            query.Append("     , u.AcessaJuridico");
            query.Append("     , u.IdCargo");
            query.Append("     , u.AcessaCadastroJuridico");
            query.Append("     , u.PermiteAprovarComunicado");
            query.Append("     , u.PermiteReprovarComunicado");
            query.Append("     , u.PermiteCancelarComunicado");
            query.Append("     , u.PermiteAlterarComunicado");

            query.Append("     , u.AcessaDashBoardChamados");
            query.Append("     , u.AcessaAvaliacaoDesempenho");
            query.Append("     , u.AcessoDeRH");
            query.Append("     , u.AcessoDeGestor");
            query.Append("     , u.AcessoDeColaborador");
            query.Append("     , u.AcessoDeControladoria");
            query.Append("     , u.NaoNotificaCorridas");
            query.Append("     , u.AniversariantesApenasDaEmpresa");
            query.Append("     , u.VisualizaRadarCompleto");
            query.Append("     , u.VisualizaBancoHorasDoDepto");
            query.Append("     , u.ParticipaBolaoCopa");
            query.Append("     , u.AdministraBolaoCopa");
            query.Append("     , u.AdministraBiblioteca");
            query.Append("     , u.AdministraCorridas");
            query.Append("     , u.AcessaBSC");
            query.Append("     , u.AcessaMetasFinanceiras");
            query.Append("     , u.AcessaMetasOperacionais");
            query.Append("     , u.PermiteBuscarResultado");
            query.Append("     , u.PermiteAlterarBSC");

            query.Append("     , u.SoChamadosDesseUsuario");
            query.Append("     , u.AcessaRecebedoria");
            query.Append("     , u.PodeExportarSigomExcel");
            query.Append("     , u.AcessaOperacional");
            query.Append("     , u.AcessaCadastroOperacional");
            query.Append("     , u.AcessaDemonstrativo");
            query.Append("     , u.AcessaIQO");
            query.Append("     , u.PodeFinalizarChamado");
            query.Append("     , u.AssinaturaChamado");
            query.Append("     , u.DataAdmissao");
            query.Append("     , u.AlteraBSCIndicadoresManuais");
            query.Append("     , u.AcessaFinanceio");
            query.Append("     , u.AcessaCadastroFin");

            query.Append("     , u.AcessaContabilidade");
            query.Append("     , u.AcessaEscrituracaoFiscal");
            query.Append("     , u.AcessaRamais");

            query.Append("     , u.AcessaCadastroBenRateio");
            query.Append("     , u.AcessaBeneficioRateio");
            query.Append("     , u.AcessaCalculoRateioBen");
            query.Append("     , u.AcessaRateio");
            query.Append("     , u.AcessaDepartamentoPessoal");
            query.Append("     , u.ProcessaArquivei");

            query.Append("     , u.Desenvolvedor");
            query.Append("     , u.Coordenador");
            query.Append("     , u.Gerente");
            query.Append("     , u.Diretor");

            query.Append("     , u.AcessaDRE");
            query.Append("     , u.AcessaCadastroMetas");
            query.Append("     , u.PermiteReabrirDRE");
            query.Append("     , u.ApenasConsultaDre");
            query.Append("     , u.ApenasPrevistoDRE");

            query.Append("     , u.AcessaLalur");
            query.Append("     , u.AcessaCadastroLalur");
            query.Append("     , u.AcessaCalculoLalur");

            query.Append("     , u.AcessaDemonstrativoFluxoCaixa");
            query.Append("     , u.AcessaResumoFluxoCaixa");

            query.Append("     , u.SempreMostrarListaDeChamados");
            query.Append("     , u.AGENDALIBERACARROS");
            query.Append("     , u.ACESSAENDIVIDAMENTO");
            query.Append("     , u.AcessaParcelamento");
            query.Append("     , u.ReprocessaParcelamento");
            query.Append("     , u.AcessaCigam");
            query.Append("     , u.AcessaCTBNotasFicais");
            query.Append("     , u.ACESSANOTASFISCAISCTB");
            query.Append("     , u.PodeIntegrarProgramacaoFerias");
            query.Append("     , u.AcessaPerAquisitoFerias");
            query.Append("     , u.AcessaSuprimentos");
            query.Append("     , u.AcessaMetasSuprimentos");
            query.Append("     , u.AcessaConciliacaoCTB");
            query.Append("     , u.AcessaConciliacaoBCOApenasConsulta");
            query.Append("     , u.RecebeEmailDifRecebedoria");
            query.Append("     , u.AbreServicoExcel");
            query.Append("     , d.descricao Departamento, e.nomeabreviado ");

            query.Append("  From NIFF_CHM_Usuarios u, flp_funcionarios f, Niff_Chm_Departamento d, niff_chm_empresas e ");
            query.Append(" Where f.codintfunc(+) = u.codfunc ");
            query.Append("   And d.iddepartamento(+) = u.Iddepartamento ");
            query.Append("   And e.idempresa(+) = u.idempresa");

            return query;
        }

        private Usuario PopulaCampos(IDataReader usuarioReader)
        {
            Usuario _usuario = new Usuario();
            _usuario.Existe = true;

            _usuario.Id = Convert.ToInt32(usuarioReader["IdUsuario"].ToString());
            switch (usuarioReader["tipo"].ToString())
            {
                case "S":
                    _usuario.Tipo = Publicas.TipoUsuario.Socilitante;
                    break;
                case "A":
                    _usuario.Tipo = Publicas.TipoUsuario.Atendente;
                    break;
                case "B":
                    _usuario.Tipo = Publicas.TipoUsuario.BI;
                    break;
                case "T":
                    _usuario.Tipo = Publicas.TipoUsuario.Todos;
                    break;
            }

            switch (usuarioReader["TipoUsuarioSAC"].ToString())
            {
                case "A":
                    _usuario.TipoSac = Publicas.TipoUsuarioSAC.Atendente;
                    break;
                case "U":
                    _usuario.TipoSac = Publicas.TipoUsuarioSAC.UsuarioComum;
                    break;
                case "F":
                    _usuario.TipoSac = Publicas.TipoUsuarioSAC.Finalizador;
                    break;
                case "D":
                    _usuario.TipoSac = Publicas.TipoUsuarioSAC.Administrador;
                    break;
                case "M":
                    _usuario.TipoSac = Publicas.TipoUsuarioSAC.Mediador;
                    break;
            }

            _usuario.Nome = usuarioReader["Nome"].ToString();
            _usuario.Ativo = usuarioReader["ativo"].ToString() == "S";
            _usuario.Administrador = usuarioReader["administrador"].ToString() == "S";
            _usuario.Senha = usuarioReader["senha"].ToString();
            _usuario.UsuarioAcesso = usuarioReader["usuarioacesso"].ToString();
            _usuario.AcessaBSC = usuarioReader["AcessaBSC"].ToString() == "S";

            try
            {
                _usuario.IpMaquina = usuarioReader["ipmaquina"].ToString();
            }
            catch { }

            try
            {
                _usuario.NomeMaquina = usuarioReader["nomemaquina"].ToString();
            }
            catch { }

            try
            {
                _usuario.Telefone = Convert.ToDecimal(usuarioReader["telefone"].ToString());
            }
            catch { }
            try
            {
                _usuario.Ramal = Convert.ToInt32(usuarioReader["ramal"].ToString());
            }
            catch { }

            _usuario.Email = usuarioReader["email"].ToString();
            _usuario.Cargo = usuarioReader["cargo"].ToString();
            _usuario.EmailDepartamento = usuarioReader["EmailDepto"].ToString();
            try
            {
                //_usuario.CPF = Convert.ToDecimal(usuarioReader["CPF"].ToString());
                _usuario.CPF = usuarioReader["CPF"].ToString();
            }
            catch { }

            try
            {
                _usuario.DataNascimento = Convert.ToDateTime(usuarioReader["dtnascimento"].ToString());
            }
            catch { }

            try
            {
                _usuario.DataAdmissao = Convert.ToDateTime(usuarioReader["DataAdmissao"].ToString());
            }
            catch { }

            try
            {
                _usuario.Foto = (byte[])(usuarioReader["foto"]);
            }
            catch { }

            _usuario.AcessaAgenda = usuarioReader["AcessaAgenda"].ToString() == "S";
            _usuario.AcessaChat = usuarioReader["AcessaChat"].ToString() == "S";
            _usuario.AcessaSac = usuarioReader["AcessaSAC"].ToString() == "S";
            _usuario.PermiteExcluirChat = usuarioReader["PermiteExcluirChat"].ToString() == "S";
            _usuario.AcessaBI = usuarioReader["AcessaBI"].ToString() == "S";
            _usuario.AcessaDescontoBeneficio = usuarioReader["AcessaDescontoBeneficio"].ToString() == "S";
            _usuario.PermiteIncluirExcluirFoto = usuarioReader["PERMITEINCEXCFOTOSFESTA"].ToString() == "S";
            _usuario.EmailAcessoPowerBi = usuarioReader["emailAcessoPowerBi"].ToString();
            _usuario.AcessaJuridico = usuarioReader["AcessaJuridico"].ToString() == "S";

            _usuario.AcessaCadastroJuridico = usuarioReader["AcessaCadastroJuridico"].ToString() == "S";
            _usuario.PermiteAprovarComunicado = usuarioReader["PermiteAprovarComunicado"].ToString() == "S";
            _usuario.PermiteCancelarComunicado = usuarioReader["PermiteCancelarComunicado"].ToString() == "S";
            _usuario.PermiteReprovarComunicado = usuarioReader["PermiteReprovarComunicado"].ToString() == "S";
            _usuario.PermiteAlterarComunicado = usuarioReader["PermiteAlterarComunicado"].ToString() == "S";
            _usuario.AcessaDashBoardChamados = usuarioReader["AcessaDashBoardChamados"].ToString() == "S";

            _usuario.AcessaAvaliacaoDesempenho = usuarioReader["AcessaAvaliacaoDesempenho"].ToString() == "S";
            _usuario.AcessoDeRH = usuarioReader["AcessoDeRH"].ToString() == "S";
            _usuario.AcessoDeGestor = usuarioReader["AcessoDeGestor"].ToString() == "S";
            _usuario.AcessoDeColaborador = usuarioReader["AcessoDeColaborador"].ToString() == "S";
            _usuario.AcessoDeControladoria = usuarioReader["AcessoDeControladoria"].ToString() == "S";
            _usuario.NaoNotificaCorridas = usuarioReader["NaoNotificaCorridas"].ToString() == "S";
            _usuario.AniversariantesApenasDaEmpresa = usuarioReader["AniversariantesApenasDaEmpresa"].ToString() == "S";
            _usuario.VisualizaRadarCompleto = usuarioReader["VisualizaRadarCompleto"].ToString() == "S";
            _usuario.VisualizaBancoHorasDoDepartamento = usuarioReader["VisualizaBancoHorasDoDepto"].ToString() == "S";
            _usuario.ParticipaBolaoCopa = usuarioReader["ParticipaBolaoCopa"].ToString() == "S";
            _usuario.AdministraBolaoCopa = usuarioReader["AdministraBolaoCopa"].ToString() == "S";
            _usuario.AdministraBiblioteca = usuarioReader["AdministraBiblioteca"].ToString() == "S";
            _usuario.AdministraCorridas = usuarioReader["AdministraCorridas"].ToString() == "S";

            _usuario.AcessaMetasFinanceiras = usuarioReader["AcessaMetasFinanceiras"].ToString() == "S";
            _usuario.AcessaMetasOperacionais = usuarioReader["AcessaMetasOperacionais"].ToString() == "S";
            _usuario.PermiteBuscarResultado = usuarioReader["PermiteBuscarResultado"].ToString() == "S";
            _usuario.PermiteAlterarBSC = usuarioReader["PermiteAlterarBSC"].ToString() == "S";
            _usuario.SoChamadosDesseUsuario = usuarioReader["SoChamadosDesseUsuario"].ToString() == "S";

            _usuario.AcessaRecebedoria = usuarioReader["AcessaRecebedoria"].ToString() == "S";
            _usuario.PodeExportarSigomExcel = usuarioReader["PodeExportarSigomExcel"].ToString() == "S";
            _usuario.AcessaOperacional = usuarioReader["AcessaOperacional"].ToString() == "S";
            _usuario.AcessaCadastroOperacional = usuarioReader["AcessaCadastroOperacional"].ToString() == "S";
            _usuario.AcessaDemonstrativo = usuarioReader["AcessaDemonstrativo"].ToString() == "S";
            _usuario.AcessaIQO = usuarioReader["AcessaIQO"].ToString() == "S";
            _usuario.PodeFinalizarChamado = usuarioReader["PodeFinalizarChamado"].ToString() == "S";
            _usuario.AlteraBSCIndicadoresManuais = usuarioReader["AlteraBSCIndicadoresManuais"].ToString() == "S";

            _usuario.AcessaFinanceiro = usuarioReader["AcessaFinanceio"].ToString() == "S";
            _usuario.AcessaCadastrosFinanceiro = usuarioReader["AcessaCadastroFin"].ToString() == "S";
            _usuario.AssinaturaChamado = usuarioReader["AssinaturaChamado"].ToString();

            _usuario.AcessaContabilidade = usuarioReader["AcessaContabilidade"].ToString() == "S";
            _usuario.AcessaEscrituracaoFiscal = usuarioReader["AcessaEscrituracaoFiscal"].ToString() == "S";
            _usuario.AcessaRamais = usuarioReader["AcessaRamais"].ToString() == "S";

            _usuario.AcessaRateioCTB = usuarioReader["AcessaRateio"].ToString() == "S";
            _usuario.AcessaCadastroBeneficioRateio = usuarioReader["AcessaCadastroBenRateio"].ToString() == "S";
            _usuario.AcessaBeneficioRateio = usuarioReader["AcessaBeneficioRateio"].ToString() == "S";
            _usuario.AcessaCalculoRateio = usuarioReader["AcessaCalculoRateioBen"].ToString() == "S";
            _usuario.AcessaDepartamentoPessoal = usuarioReader["AcessaDepartamentoPessoal"].ToString() == "S";
            _usuario.RecebeEmailNotaFiscal = usuarioReader["ProcessaArquivei"].ToString() == "S";

            _usuario.Desenvolvedor = usuarioReader["Desenvolvedor"].ToString() == "S";
            _usuario.Gerente = usuarioReader["Gerente"].ToString() == "S";
            _usuario.Coordenador = usuarioReader["Coordenador"].ToString() == "S";
            _usuario.Diretor = usuarioReader["Diretor"].ToString() == "S";

            _usuario.AcessaDRE = usuarioReader["AcessaDRE"].ToString() == "S";
            _usuario.AcessaCadastroMetas = usuarioReader["AcessaCadastroMetas"].ToString() == "S";
            _usuario.PermiteReabrirDRE = usuarioReader["PermiteReabrirDRE"].ToString() == "S";
            _usuario.ApenasConsultaDRE = usuarioReader["ApenasConsultaDre"].ToString() == "S";
            _usuario.ApenasEditarPrevistoDRE = usuarioReader["ApenasPrevistoDRE"].ToString() == "S";

            _usuario.AcessaLalur = usuarioReader["AcessaLalur"].ToString() == "S";
            _usuario.AcessaCadastroLalur = usuarioReader["AcessaCadastroLalur"].ToString() == "S";
            _usuario.AcessaCalculoLalur = usuarioReader["AcessaCalculoLalur"].ToString() == "S";

            _usuario.AcessaDemonstrativoFluxoCaixa = usuarioReader["AcessaDemonstrativoFluxoCaixa"].ToString() == "S";
            _usuario.AcessaResumoFluxoCaixa = usuarioReader["AcessaResumoFluxoCaixa"].ToString() == "S";

            _usuario.SempreMostrarListaDeChamados = usuarioReader["SempreMostrarListaDeChamados"].ToString() == "S";

            _usuario.AgendaLiberaCarros = usuarioReader["AGENDALIBERACARROS"].ToString() == "S";
            _usuario.AcessaEndividamento = usuarioReader["ACESSAENDIVIDAMENTO"].ToString() == "S";
            _usuario.AcessaParcelamento = usuarioReader["AcessaParcelamento"].ToString() == "S";
            _usuario.ReprocessaParcelamento = usuarioReader["ReprocessaParcelamento"].ToString() == "S";
            _usuario.AcessaCigam = usuarioReader["AcessaCigam"].ToString() == "S";
            _usuario.AcessaCTBNotasFicais = usuarioReader["AcessaCTBNotasFicais"].ToString() == "S";
            _usuario.AcessaCadastrosCTBNotasFiscais = usuarioReader["ACESSANOTASFISCAISCTB"].ToString() == "S";

            _usuario.PodeIntegrarProgramacaoFerias = usuarioReader["PodeIntegrarProgramacaoFerias"].ToString() == "S";
            _usuario.AcessaPerAquisitoFerias = usuarioReader["AcessaPerAquisitoFerias"].ToString() == "S";
            _usuario.AcessaSuprimentos = usuarioReader["AcessaSuprimentos"].ToString() == "S";
            _usuario.AcessaMetasSuprimentos = usuarioReader["AcessaMetasSuprimentos"].ToString() == "S";
            _usuario.AcessaConciliacaoContabil = usuarioReader["AcessaConciliacaoCTB"].ToString() == "S";
            _usuario.AcessaConciliacaoBCOApenasConsulta = usuarioReader["AcessaConciliacaoBCOApenasConsulta"].ToString() == "S";

            _usuario.RecebeEmailDasDiferencasDoSigonProdata = usuarioReader["RecebeEmailDifRecebedoria"].ToString() == "S";
            _usuario.AbreServicoExcel = usuarioReader["AbreServicoExcel"].ToString() == "S";


            try
            {
                _usuario.IdCargo = Convert.ToInt32(usuarioReader["IdCargo"].ToString());
            }
            catch { }

            try
            {
                _usuario.IdEmpresa = Convert.ToInt32(usuarioReader["IdEmpresa"].ToString());
            }
            catch { }

            try
            {
                _usuario.IdDepartamento = Convert.ToInt32(usuarioReader["IdDepartamento"].ToString());
            }
            catch { }


            try
            {
                _usuario.CodigoInternoFuncionarioGlobus = Convert.ToInt32(usuarioReader["CodFunc"].ToString());
            }
            catch { }

            _usuario.RegistroFuncionario = usuarioReader["Registro"].ToString();
            _usuario.Departamento = usuarioReader["Departamento"].ToString();
            _usuario.Empresa = usuarioReader["nomeabreviado"].ToString();

            return _usuario;
        }

        /* Ok */
        public List<Usuario> ListaUsuario(bool apenasAtivos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Usuario> _lista = new List<Usuario>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query = query = MontaConsulta();

                if (apenasAtivos)
                    query.Append(" And u.ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        Usuario _usuario = PopulaCampos(usuarioReader);

                        _lista.Add(_usuario);
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

        /* ok */
        public List<Usuario> ListaUsuario(bool apenasAtivos, int idEmpresa, bool sac, string tipoSAC)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Usuario> _lista = new List<Usuario>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query = query = MontaConsulta();

                if (apenasAtivos)
                    query.Append(" And u.ativo = 'S'");

                query.Append(" And u.IdEmpresa = " + idEmpresa);

                if (sac)
                {
                    query.Append(" And u.AcessaSAC = 'S'");
                    if (tipoSAC == "F")
                        query.Append(" And u.TipoUsuarioSAC in ('F','M')");
                    else
                        query.Append(" And u.TipoUsuarioSAC in ('U','M')");
                }

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        Usuario _usuario = PopulaCampos(usuarioReader);

                        _lista.Add(_usuario);
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



        /*ok*/
        public Usuario ConsultaUsuario(string login)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();

            if (Publicas.mensagemDeErro != null && Publicas.mensagemDeErro != "")
            {
                return null;
            }

            Usuario _usuario = new Usuario();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query = MontaConsulta();
                query.Append("   and UsuarioAcesso = '" + login.ToUpper() + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();
                                 
                using (usuarioReader)
                {
                    if (usuarioReader.Read())
                    {
                        _usuario = PopulaCampos(usuarioReader);
                        
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
            return _usuario;
        }

        /*ok*/
        public Usuario ConsultaUsuarioPorID(int login)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Usuario _usuario = new Usuario();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query = MontaConsulta();                
                query.Append("   And idusuario = " + login );
                
                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    if (usuarioReader.Read())
                    {
                        _usuario = PopulaCampos(usuarioReader);
                        
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
            return _usuario;
        }

        /*ok*/
        public Usuario ConsultaUsuarioPorCodigoFuncionarioGlobus(int codIntFunc)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Usuario _usuario = new Usuario();
            Publicas.mensagemDeErro = string.Empty;
            try
            {
                query = MontaConsulta();
                query.Append("   And f.Codintfunc = " + codIntFunc);

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    if (usuarioReader.Read())
                    {
                        _usuario = PopulaCampos(usuarioReader);

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
            return _usuario;
        }

        public bool TrocaSenha(Usuario usuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Clear();
                query.Append("Update niff_chm_usuarios");
                query.Append("   set senha = '" + usuario.Senha + "'");
                query.Append(" Where idusuario = " + usuario.Id);
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

        public bool Grava(Usuario usuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            OracleParameter parametro = new OracleParameter();
            List<OracleParameter> parametros = new List<OracleParameter>();

            try
            {
                if (!usuario.Existe)
                {
                    query.Clear();
                    query.Append("Select SQ_NIFF_IDUsuario.Nextval next from dual");

                    Query executar = sessao.CreateQuery(query.ToString());

                    usuarioReader = executar.ExecuteQuery();

                    using (usuarioReader)
                    {
                        if (usuarioReader.Read())
                        {
                            Publicas._idUsuarioNovo = Convert.ToInt32(usuarioReader["next"].ToString());
                        }
                    }

                    query.Clear();
                    query.Append("Insert into niff_chm_usuarios");
                    query.Append("   (idusuario, tipo, nome, ativo, administrador, ipmaquina, nomemaquina, ");
                    query.Append("    telefone, ramal, email, usuarioacesso, senha, cargo, dtnascimento,  ");
                    query.Append("    acessaagenda, acessachat, permiteexcluirchat, acessabi, permiteincexcfotosfesta,");
                    query.Append("    emailacessopowerbi, setor, IdEmpresa, AcessaSAC, TipoUsuarioSAC, foto ");
                    
                    if (usuario.IdDepartamento != 0)
                        query.Append("    , IdDepartamento");

                    if (usuario.CPF != "")
                        query.Append("    , CPF");

                    if (usuario.CodigoInternoFuncionarioGlobus != 0)
                        query.Append("    , CodFunc");

                    query.Append("   , EmailDepto, AcessaDescontoBeneficio, AcessaJuridico ");

                    if (usuario.IdCargo != 0)
                        query.Append("   , IdCargo");

                    query.Append("   , AcessaCadastroJuridico, PermiteAprovarComunicado, PermiteReprovarComunicado, PermiteCancelarComunicado");
                    query.Append("   , AcessaDashBoardChamados, AcessaAvaliacaoDesempenho, AcessoDeRH, AcessoDeGestor, AcessoDeColaborador");
                    query.Append("   , PermiteAlterarComunicado, AcessoDeControladoria, VisualizaRadarCompleto, VisualizaBancoHorasDoDepto, ParticipaBolaoCopa ");
                    query.Append("   , AcessaBSC, AcessaMetasFinanceiras, AcessaMetasOperacionais, PermiteBuscarResultado, PermiteAlterarBSC");
                    query.Append("   , AcessaRecebedoria, PodeExportarSigomExcel, AcessaOperacional, AcessaCadastroOperacional, AcessaDemonstrativo, AcessaIQO");
                    query.Append("   , PodeFinalizarChamado, AssinaturaChamado, AlteraBSCIndicadoresManuais");
                    query.Append("   , AcessaFinanceio, AcessaCadastroFin, AcessaContabilidade, AcessaEscrituracaoFiscal, AcessaRamais");
                    query.Append("   , AcessaRateio, AcessaCadastroBenRateio, AcessaBeneficioRateio, AcessaCalculoRateioBen, AcessaDepartamentoPessoal, ProcessaArquivei");
                    query.Append("   , Desenvolvedor, Gerente, Coordenador, AcessaDRE, AcessaCadastroMetas, PermiteReabrirDRE, ApenasConsultaDre");
                    query.Append("   , AcessaLalur, AcessaCadastroLalur, AcessaCalculoLalur, ApenasPrevistoDRE, AcessaResumoFluxoCaixa, AcessaDemonstrativoFluxoCaixa");
                    query.Append("   , AgendaLiberaCarros, AcessaEndividamento, AcessaParcelamento, ReprocessaParcelamento, AcessaCigam, AcessaCTBNotasFicais, PodeIntegrarProgramacaoFerias");
                    query.Append("   , Diretor, AcessaPerAquisitoFerias, AcessaSuprimentos, AcessaMetasSuprimentos, AcessaConciliacaoCTB, ACESSANOTASFISCAISCTB, AcessaConciliacaoBCOApenasConsulta");
                    query.Append("   , RecebeEmailDifRecebedoria, AbreServicoExcel");
                    query.Append("  ) Values ( " + Publicas._idUsuarioNovo );
                    query.Append(", '" + (usuario.Tipo == Publicas.TipoUsuario.Atendente ? "A" :
                                         (usuario.Tipo == Publicas.TipoUsuario.Socilitante ? "S" : "T")) + "'");
                    query.Append(", '" + usuario.Nome + "'");
                    query.Append(", '" + (usuario.Ativo ? "S" : "N") + "'");
                    query.Append(", '" + (usuario.Administrador ? "S" : "N") + "'");
                    query.Append(", '" + usuario.IpMaquina + "'");
                    query.Append(", '" + usuario.NomeMaquina + "'");
                    query.Append(", "  + (usuario.Telefone == 0 ? "null" : usuario.Telefone.ToString()) );
                    query.Append(", "  + (usuario.Ramal == 0 ? "null" : usuario.Ramal.ToString()) );
                    query.Append(", '" + usuario.Email + "'");
                    query.Append(", '" + usuario.UsuarioAcesso + "'");
                    query.Append(", '" + usuario.Senha + "'");
                    query.Append(", '" + usuario.Cargo + "'");
                    query.Append(", To_date('" + usuario.DataNascimento.ToShortDateString() + "', 'dd/mm/yyyy')");
                    query.Append(", '" + (usuario.AcessaAgenda ? "S" : "N") + "'");
                    query.Append(", '" + (usuario.AcessaChat ? "S" : "N") + "'");
                    query.Append(", '" + (usuario.PermiteExcluirChat? "S" : "N") + "'");
                    query.Append(", '" + (usuario.AcessaBI ? "S" : "N") + "'");
                    query.Append(", '" + (usuario.PermiteIncluirExcluirFoto ? "S" : "N") + "'");
                    query.Append(", '" + usuario.EmailAcessoPowerBi + "'");
                    query.Append(", '" + usuario.Setor + "', " + usuario.IdEmpresa );
                    query.Append(", '" + (usuario.AcessaSac ? "S" : "N") + "'");
                    query.Append(", '" + (usuario.TipoSac == Publicas.TipoUsuarioSAC.Atendente ? "A" :
                                         (usuario.TipoSac == Publicas.TipoUsuarioSAC.UsuarioComum ? "U" :
                                         (usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador ? "D" :
                                         (usuario.TipoSac == Publicas.TipoUsuarioSAC.Finalizador ? "F" : "M")))) + "'");

                    if (usuario.Foto == null)
                    {
                        query.Append(", null ");
                    }
                    else
                    {
                        query.Append(", :pfoto ");
                        parametro.ParameterName = ":pfoto";
                        parametro.Value = usuario.Foto;
                        parametro.OracleType = OracleType.Blob;
                        parametros.Add(parametro);
                    }

                    if (usuario.IdDepartamento != 0)
                        query.Append(", " + usuario.IdDepartamento);

                    if (usuario.CPF != "")
                        query.Append(", '" + usuario.CPF + "'");

                    if (usuario.CodigoInternoFuncionarioGlobus != 0)
                        query.Append(", '" + usuario.CodigoInternoFuncionarioGlobus + "' ");

                    query.Append(" , '" + usuario.EmailDepartamento + "' ");
                    query.Append(" , '" + (usuario.AcessaDescontoBeneficio ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaJuridico ? "S" : "N") + "' ");

                    if (usuario.IdCargo != 0)
                        query.Append(" , " + usuario.IdCargo);

                    query.Append(" , '" + (usuario.AcessaCadastroJuridico ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.PermiteAprovarComunicado ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.PermiteReprovarComunicado ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.PermiteCancelarComunicado ? "S" : "N") + "' ");

                    query.Append(" , '" + (usuario.AcessaDashBoardChamados ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaAvaliacaoDesempenho ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessoDeRH ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessoDeGestor ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessoDeColaborador ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.PermiteAlterarComunicado ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessoDeControladoria ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.VisualizaRadarCompleto ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.VisualizaBancoHorasDoDepartamento ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.ParticipaBolaoCopa ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaBSC ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaMetasFinanceiras ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaMetasOperacionais ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.PermiteBuscarResultado ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.PermiteAlterarBSC ? "S" : "N") + "' ");

                    query.Append(" , '" + (usuario.AcessaRecebedoria ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.PodeExportarSigomExcel ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaOperacional ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaCadastroOperacional ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaDemonstrativo ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaIQO ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.PodeFinalizarChamado ? "S" : "N") + "' ");
                    query.Append(" , '" + usuario.AssinaturaChamado + "' ");
                    query.Append(" , '" + (usuario.AlteraBSCIndicadoresManuais ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaFinanceiro ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaCadastrosFinanceiro ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaContabilidade ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaEscrituracaoFiscal ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaRamais ? "S" : "N") + "' ");

                    query.Append(" , '" + (usuario.AcessaRateioCTB ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaCadastroBeneficioRateio ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaBeneficioRateio ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaCalculoRateio ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaDepartamentoPessoal ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.RecebeEmailNotaFiscal ? "S" : "N") + "' ");

                    query.Append(" , '" + (usuario.Desenvolvedor ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.Gerente ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.Coordenador ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaDRE ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaCadastroMetas ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.PermiteReabrirDRE ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.ApenasConsultaDRE ? "S" : "N") + "' ");

                    query.Append(" , '" + (usuario.AcessaLalur? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaCadastroLalur ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaCalculoLalur ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.ApenasEditarPrevistoDRE ? "S" : "N") + "' ");

                    query.Append(" , '" + (usuario.AcessaResumoFluxoCaixa ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaDemonstrativoFluxoCaixa ? "S" : "N") + "' "); 

                    query.Append(" , '" + (usuario.AgendaLiberaCarros ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaEndividamento ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaParcelamento ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.ReprocessaParcelamento ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaCigam ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaCTBNotasFicais ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.PodeIntegrarProgramacaoFerias ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.Diretor ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaPerAquisitoFerias ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaSuprimentos ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaMetasSuprimentos ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaConciliacaoContabil ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaCadastrosCTBNotasFiscais ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AcessaConciliacaoBCOApenasConsulta ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.RecebeEmailDasDiferencasDoSigonProdata ? "S" : "N") + "' ");
                    query.Append(" , '" + (usuario.AbreServicoExcel ? "S" : "N") + "' ");
                    query.Append(")");
                }
                else
                {
                    Publicas._idUsuarioNovo = usuario.Id;

                    query.Clear();
                    query.Append("Update niff_chm_usuarios");
                    query.Append("   set nome = '" + usuario.Nome + "', ");
                    query.Append("       ativo = '" + (usuario.Ativo ? "S" : "N") + "', ");
                    query.Append("       Tipo = '" + (usuario.Tipo == Publicas.TipoUsuario.Atendente ? "A" :
                                                     (usuario.Tipo == Publicas.TipoUsuario.Socilitante ? "S" : "T")) + "', ");
                    query.Append("       Administrador = '" + (usuario.Administrador ? "S" : "N") + "', ");
                    query.Append("       Ipmaquina = '" + usuario.IpMaquina + "', ");
                    query.Append("       Nomemaquina = '" + usuario.NomeMaquina + "', ");
                    query.Append("       Telefone = " + usuario.Telefone + ", ");
                    query.Append("       ramal = " + usuario.Ramal + ", ");
                    query.Append("       Email = '" + usuario.Email + "', ");
                    query.Append("       UsuarioAcesso = '" + usuario.UsuarioAcesso + "', ");
                    query.Append("       Senha = '" + usuario.Senha + "', ");
                    query.Append("       Cargo = '" + usuario.Cargo + "', ");
                    query.Append("       dtnascimento = To_date('" + usuario.DataNascimento.ToShortDateString() + "', 'dd/mm/yyyy'), ");
                    query.Append("       AcessaAgenda = '" + (usuario.AcessaAgenda ? "S" : "N") + "', ");
                    query.Append("       AcessaChat ='" + (usuario.AcessaChat ? "S" : "N") + "', ");
                    query.Append("       permiteexcluirchat = '" + (usuario.PermiteExcluirChat ? "S" : "N") + "', ");
                    query.Append("       AcessaBI = '" + (usuario.AcessaBI ? "S" : "N") + "', ");
                    query.Append("       permiteincexcfotosfesta = '" + (usuario.PermiteIncluirExcluirFoto ? "S" : "N") + "', ");
                    query.Append("       emailacessopowerbi = '" + usuario.EmailAcessoPowerBi + "', ");
                    query.Append("       IdEmpresa = " + usuario.IdEmpresa + ", ");
                    query.Append("       AcessaSAC = '" + (usuario.AcessaSac ? "S" : "N") + "', ");
                    query.Append("       TipoUsuarioSAC = '" + (usuario.TipoSac == Publicas.TipoUsuarioSAC.Atendente ? "A" :
                                                     (usuario.TipoSac == Publicas.TipoUsuarioSAC.UsuarioComum ? "U" :
                                                     (usuario.TipoSac == Publicas.TipoUsuarioSAC.Administrador ? "D" :
                                                     (usuario.TipoSac == Publicas.TipoUsuarioSAC.Finalizador ? "F" : "M")))) + "'");

                    if (usuario.Foto == null)
                    {
                        query.Append(", foto = null ");
                    }
                    else
                    {
                        query.Append(", foto = :pfoto ");

                        parametro.ParameterName = ":pfoto";
                        parametro.Value = usuario.Foto;
                        parametro.OracleType = OracleType.Blob;
                        parametros.Add(parametro);
                    }
                    if (usuario.IdDepartamento != 0)
                        query.Append("    , IdDepartamento = " + usuario.IdDepartamento);

                    if (usuario.CPF != "")
                        query.Append("     , CPF = '" + usuario.CPF + "'");

                    if (usuario.CodigoInternoFuncionarioGlobus != 0)
                        query.Append("     , CodFunc = " + usuario.CodigoInternoFuncionarioGlobus);

                    query.Append("   , EmailDepto = '" + usuario.EmailDepartamento + "' ");
                    query.Append("   , AcessaDescontoBeneficio = '" + (usuario.AcessaDescontoBeneficio ? "S" : "N") + "' ");
                    query.Append("   , AcessaJuridico = '" + (usuario.AcessaJuridico ? "S" : "N") + "' ");

                    if (usuario.IdCargo != 0)
                        query.Append("     , IdCargo = " + usuario.IdCargo);

                    query.Append("   , AcessaCadastroJuridico = '" + (usuario.AcessaCadastroJuridico ? "S" : "N") + "' ");
                    query.Append("   , PermiteAprovarComunicado = '" + (usuario.PermiteAprovarComunicado ? "S" : "N") + "' ");
                    query.Append("   , PermiteReprovarComunicado = '" + (usuario.PermiteReprovarComunicado ? "S" : "N") + "' ");
                    query.Append("   , PermiteCancelarComunicado = '" + (usuario.PermiteCancelarComunicado ? "S" : "N") + "' ");
                    query.Append("   , PermiteAlterarComunicado = '" + (usuario.PermiteAlterarComunicado ? "S" : "N") + "' ");
                    query.Append("   , AcessaDashBoardChamados = '" + (usuario.AcessaDashBoardChamados ? "S" : "N") + "' ");
                    query.Append("   , AcessaAvaliacaoDesempenho = '" + (usuario.AcessaAvaliacaoDesempenho ? "S" : "N") + "' ");
                    query.Append("   , AcessoDeRH = '" + (usuario.AcessoDeRH ? "S" : "N") + "' ");
                    query.Append("   , AcessoDeGestor = '" + (usuario.AcessoDeGestor ? "S" : "N") + "' ");
                    query.Append("   , AcessoDeColaborador = '" + (usuario.AcessoDeColaborador ? "S" : "N") + "' ");
                    query.Append("   , NaoNotificaCorridas = '" + (usuario.NaoNotificaCorridas ? "S" : "N") + "' ");
                    query.Append("   , AniversariantesApenasDaEmpresa = '" + (usuario.AniversariantesApenasDaEmpresa ? "S" : "N") + "' ");
                    query.Append("   , AcessoDeControladoria = '" + (usuario.AcessoDeControladoria ? "S" : "N") + "' ");

                    query.Append("   , VisualizaRadarCompleto = '" + (usuario.VisualizaRadarCompleto ? "S" : "N") + "' ");
                    query.Append("   , VisualizaBancoHorasDoDepto = '" + (usuario.VisualizaBancoHorasDoDepartamento ? "S" : "N") + "' ");
                    query.Append("   , ParticipaBolaoCopa = '" + (usuario.ParticipaBolaoCopa ? "S" : "N") + "' ");
                    query.Append("   , AdministraBolaoCopa = '" + (usuario.AdministraBolaoCopa ? "S" : "N") + "' ");
                    query.Append("   , AdministraBiblioteca = '" + (usuario.AdministraBiblioteca ? "S" : "N") + "' ");
                    query.Append("   , AdministraCorridas = '" + (usuario.AdministraCorridas ? "S" : "N") + "' ");
                    query.Append("   , AcessaBSC = '" + (usuario.AcessaBSC ? "S" : "N") + "' ");
                    query.Append("   , AcessaMetasFinanceiras = '" + (usuario.AcessaMetasFinanceiras ? "S" : "N") + "' ");
                    query.Append("   , AcessaMetasOperacionais = '" + (usuario.AcessaMetasOperacionais ? "S" : "N") + "' ");
                    query.Append("   , PermiteBuscarResultado  = '" + (usuario.PermiteBuscarResultado ? "S" : "N") + "' ");
                    query.Append("   , PermiteAlterarBSC = '" + (usuario.PermiteAlterarBSC ? "S" : "N") + "' ");

                    query.Append("   , AcessaRecebedoria = '" + (usuario.AcessaRecebedoria ? "S" : "N") + "' ");
                    query.Append("   , PodeExportarSigomExcel = '" + (usuario.PodeExportarSigomExcel ? "S" : "N") + "' ");
                    query.Append("   , AcessaOperacional = '" + (usuario.AcessaOperacional ? "S" : "N") + "' ");
                    query.Append("   , AcessaCadastroOperacional = '" + (usuario.AcessaCadastroOperacional ? "S" : "N") + "' ");
                    query.Append("   , AcessaDemonstrativo = '" + (usuario.AcessaDemonstrativo ? "S" : "N") + "' ");
                    query.Append("   , AcessaIQO = '" + (usuario.AcessaIQO ? "S" : "N") + "' ");
                    query.Append("   , PodeFinalizarChamado = '" + (usuario.PodeFinalizarChamado ? "S" : "N") + "' ");
                    query.Append("   , AssinaturaChamado = '" + usuario.AssinaturaChamado + "' ");
                    query.Append("   , AlteraBSCIndicadoresManuais = '" + (usuario.AlteraBSCIndicadoresManuais ? "S" : "N") + "' ");
                    query.Append("   , AcessaFinanceio = '" + (usuario.AcessaFinanceiro ? "S" : "N") + "' ");
                    query.Append("   , AcessaCadastroFin = '" + (usuario.AcessaCadastrosFinanceiro ? "S" : "N") + "' ");

                    query.Append("   , AcessaContabilidade = '" + (usuario.AcessaContabilidade ? "S" : "N") + "' ");
                    query.Append("   , AcessaEscrituracaoFiscal = '" + (usuario.AcessaEscrituracaoFiscal ? "S" : "N") + "' ");
                    query.Append("   , AcessaRamais = '" + (usuario.AcessaRamais ? "S" : "N") + "' ");

                    query.Append("   , AcessaRateio = '" + (usuario.AcessaRateioCTB ? "S" : "N") + "' ");
                    query.Append("   , AcessaCadastroBenRateio = '" + (usuario.AcessaCadastroBeneficioRateio ? "S" : "N") + "' ");
                    query.Append("   , AcessaBeneficioRateio = '" + (usuario.AcessaBeneficioRateio ? "S" : "N") + "' ");
                    query.Append("   , AcessaCalculoRateioBen = '" + (usuario.AcessaCalculoRateio ? "S" : "N") + "' ");
                    query.Append("   , AcessaDepartamentoPessoal = '" + (usuario.AcessaDepartamentoPessoal ? "S" : "N") + "' ");
                    query.Append("   , ProcessaArquivei = '" + (usuario.RecebeEmailNotaFiscal ? "S" : "N") + "' ");

                    query.Append("   , Desenvolvedor = '" + (usuario.Desenvolvedor ? "S" : "N") + "' ");
                    query.Append("   , Gerente = '" + (usuario.Gerente ? "S" : "N") + "' ");
                    query.Append("   , Coordenador = '" + (usuario.Coordenador ? "S" : "N") + "' ");
                    query.Append("   , Diretor = '" + (usuario.Diretor ? "S" : "N") + "' ");

                    query.Append("   , AcessaDRE = '" + (usuario.AcessaDRE ? "S" : "N") + "' ");
                    query.Append("   , AcessaCadastroMetas = '" + (usuario.AcessaCadastroMetas ? "S" : "N") + "' ");
                    query.Append("   , PermiteReabrirDRE = '" + (usuario.PermiteReabrirDRE ? "S" : "N") + "' ");
                    query.Append("   , ApenasConsultaDre = '" + (usuario.ApenasConsultaDRE ? "S" : "N") + "' ");

                    query.Append("   , AcessaLalur = '" + (usuario.AcessaLalur ? "S" : "N") + "' ");
                    query.Append("   , AcessaCadastroLalur = '" + (usuario.AcessaCadastroLalur ? "S" : "N") + "' ");
                    query.Append("   , AcessaCalculoLalur = '" + (usuario.AcessaCalculoLalur ? "S" : "N") + "' ");
                    query.Append("   , ApenasPrevistoDRE = '" + (usuario.ApenasEditarPrevistoDRE ? "S" : "N") + "' ");

                    query.Append("   , AcessaResumoFluxoCaixa = '" + (usuario.AcessaResumoFluxoCaixa ? "S" : "N") + "' ");
                    query.Append("   , AcessaDemonstrativoFluxoCaixa = '" + (usuario.AcessaDemonstrativoFluxoCaixa ? "S" : "N") + "' ");
                    query.Append("   , SempreMostrarListaDeChamados = '" + (usuario.SempreMostrarListaDeChamados ? "S" : "N") + "' ");

                    query.Append("   , AgendaLiberaCarros = '" + (usuario.AgendaLiberaCarros ? "S" : "N") + "' ");
                    query.Append("   , AcessaEndividamento = '" + (usuario.AcessaEndividamento ? "S" : "N") + "' ");
                    query.Append("   , AcessaParcelamento = '" + (usuario.AcessaParcelamento ? "S" : "N") + "' ");
                    query.Append("   , ReprocessaParcelamento = '" + (usuario.ReprocessaParcelamento ? "S" : "N") + "' ");
                    query.Append("   , AcessaCigam = '" + (usuario.AcessaCigam ? "S" : "N") + "' ");
                    query.Append("   , AcessaCTBNotasFicais = '" + (usuario.AcessaCTBNotasFicais ? "S" : "N") + "' ");
                    query.Append("   , PodeIntegrarProgramacaoFerias = '" + (usuario.PodeIntegrarProgramacaoFerias ? "S" : "N") + "' ");
                    query.Append("   , AcessaPerAquisitoFerias = '" + (usuario.AcessaPerAquisitoFerias ? "S" : "N") + "' ");
                    query.Append("   , AcessaSuprimentos = '" + (usuario.AcessaSuprimentos ? "S" : "N") + "' ");
                    query.Append("   , AcessaMetasSuprimentos = '" + (usuario.AcessaMetasSuprimentos ? "S" : "N") + "' ");
                    query.Append("   , AcessaConciliacaoCTB = '" + (usuario.AcessaConciliacaoContabil ? "S" : "N") + "' ");
                    query.Append("   , ACESSANOTASFISCAISCTB = '" + (usuario.AcessaCadastrosCTBNotasFiscais ? "S" : "N") + "' ");
                    query.Append("   , AcessaConciliacaoBCOApenasConsulta = '" + (usuario.AcessaConciliacaoBCOApenasConsulta ? "S" : "N") + "' ");

                    query.Append("   , RecebeEmailDifRecebedoria = '" + (usuario.RecebeEmailDasDiferencasDoSigonProdata ? "S" : "N") + "' ");
                    query.Append("   , AbreServicoExcel = '" + (usuario.AbreServicoExcel ? "S" : "N") + "' ");

                    query.Append(" Where idusuario = " + usuario.Id);
                }

                return sessao.ExecuteSqlTransaction(query.ToString(), parametros.ToArray());
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

        public bool GravaEmpresas(List<EmpresaDoUsuario> lista, int idUsuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno;
            try
            {
                retorno = ExcluiEmpresas(idUsuario);

                if (!retorno)
                    return retorno;
                

                foreach (EmpresaDoUsuario item in lista)
                {
                    query.Clear();
                    query.Append("Insert into niff_chm_empautousuario");
                    query.Append(" (IdEmpresa, IdUsuario) ");
                    query.Append(" values ( " + item.IdEmpresa + ", " + idUsuario + ")");

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

        public bool GravaCategoria(List<CategoriaDoUsuario> lista, int idUsuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno;
            try
            {
                retorno = ExcluiCategorias(idUsuario);

                if (!retorno)
                    return retorno;

                foreach (CategoriaDoUsuario item in lista)
                {
                    query.Clear();
                    query.Append("Insert into niff_chm_categautousuario");
                    query.Append(" (IdCategoria, IdUsuario) ");
                    query.Append(" values ( " + item.IdCategoria + ", " + idUsuario + ")");

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

        public bool GravaModulo(List<ModuloDoUsuario> lista, int idUsuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno;
            try
            {
                retorno = ExcluiModulos(idUsuario);

                if (!retorno)
                    return retorno;


                foreach (ModuloDoUsuario item in lista)
                {
                    query.Clear();
                    query.Append("Insert into niff_chm_modautousuario");
                    query.Append(" (IdModulo, IdUsuario) ");
                    query.Append(" values ( " + item.IdModulo + ", " + idUsuario + ")");

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

        public bool Exclui(int idUsuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (idUsuario != 0)
                {
                    query.Append("Delete niff_chm_usuarios");
                    query.Append(" Where idUsuario = " + idUsuario);
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

        public bool ExcluiEmpresas(int idUsuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (idUsuario != 0)
                {
                    query.Append("Delete niff_chm_empautousuario");
                    query.Append(" Where idUsuario = " + idUsuario);
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

        public bool ExcluiCategorias(int idUsuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (idUsuario != 0)
                {
                    query.Append("Delete niff_chm_categautousuario");
                    query.Append(" Where idUsuario = " + idUsuario);
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

        public bool ExcluiModulos(int idUsuario)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                if (idUsuario != 0)
                {
                    query.Append("Delete niff_chm_modautousuario");
                    query.Append(" Where idUsuario = " + idUsuario);
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

        public bool AlteraStatusUsuario(int idUsuario, Publicas.StatusUsuario status)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            string statusLogado = "O";

            query.Append("Select idusuario, datalogado, status");
            query.Append("  From Niff_Chm_Usuariologado");
            query.Append(" Where IdUsuario = " + idUsuario);

            Query executar = sessao.CreateQuery(query.ToString());

            usuarioReader = executar.ExecuteQuery();

            switch (status)
            {
                case Publicas.StatusUsuario.Ausente:
                    statusLogado = "A";
                    break;
                case Publicas.StatusUsuario.Ocupado:
                    statusLogado = "B";
                    break;
                case Publicas.StatusUsuario.OffLine:
                    statusLogado = "F";
                    break;
                case Publicas.StatusUsuario.OnLine:
                    statusLogado = "O";
                    break;
            }

            using (usuarioReader)
            {
                if (!usuarioReader.Read())
                {
                    query.Clear();
                    query.Append("Insert into Niff_Chm_Usuariologado");
                    query.Append("   (idusuario, datalogado, status) ");
                    query.Append("   Values (" + idUsuario + ", sysdate, '" + statusLogado + "')");

                    return sessao.ExecuteSqlTransaction(query.ToString());
                }
                else
                {
                    if (!string.IsNullOrEmpty(usuarioReader["status"].ToString()))
                    {
                        query.Clear();
                        query.Append("Update Niff_Chm_Usuariologado");
                        query.Append("   set DataLogado = sysDate  ");
                        query.Append("     , Status = '" + statusLogado + "'");
                        query.Append(" Where idusuario = " + idUsuario);

                        return sessao.ExecuteSqlTransaction(query.ToString());
                    }

                    query.Clear();
                    query.Append("Insert into Niff_Chm_Usuariologado");
                    query.Append("   (idusuario, datalogado, status) ");
                    query.Append("   Values (" + idUsuario + ", sysdate, '" + statusLogado + "')");

                    return sessao.ExecuteSqlTransaction(query.ToString());
                }
            }
        }

        public List<EmpresaDoUsuario> ConsultaEmpresasAutorizadasDoUsuario(int idUsuario)
        {

            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<EmpresaDoUsuario> _listaEmpresas = new List<EmpresaDoUsuario>();

            try
            {
                query.Append("Select idempresa     ");
                query.Append("     , Nome          ");
                query.Append("     , codigoglobus  ");
                query.Append("     , ativo         ");
                query.Append("     , EmpAutorizada");
                query.Append("     , Idusuario       ");
                query.Append("     , ValorDescFaltaJustificada ");
                query.Append("     , ValorDescFaltaInjustificada ");
                query.Append("     , QtdfaltasJustificadasSuperior ");
                query.Append("     , QtdfaltasInjustifSuperior ");
                query.Append("     , Empresa");
                query.Append("  From (");


                query.Append("Select e.idempresa     ");
                query.Append("     , e.Nome          ");
                query.Append("     , e.codigoglobus  ");
                query.Append("     , e.ativo         ");
                query.Append("     , 'N' EmpAutorizada");
                query.Append("     , 0 Idusuario       ");
                query.Append("     , Nvl(ValorDescFaltaJustificada,0) ValorDescFaltaJustificada ");
                query.Append("     , Nvl(ValorDescFaltaInjustificada,0) ValorDescFaltaInjustificada ");
                query.Append("     , Nvl(QtdfaltasJustificadasSuperior,0) QtdfaltasJustificadasSuperior ");
                query.Append("     , Nvl(QtdfaltasInjustifSuperior,0) QtdfaltasInjustifSuperior ");
                query.Append("     , e.codigoGlobus || ' - ' || e.NomeAbreviado empresa");

                query.Append("  From niff_chm_empresas e                 ");
                query.Append(" Where e.Idempresa not in  (Select Idempresa From Niff_Chm_Empautousuario ");
                query.Append("                             Where Idusuario = " + idUsuario.ToString() + ")");
                query.Append("   And e.ativo = 'S'                        ");
                query.Append(" Union All ");

                query.Append("Select e.idempresa     ");
                query.Append("     , e.Nome          ");
                query.Append("     , e.codigoglobus  ");
                query.Append("     , e.ativo         ");
                query.Append("     , Decode(eu.Idempresa,Null, 'N','S') EmpAutorizada");
                query.Append("     , nvl(eu.Idusuario, 0) Idusuario       ");
                query.Append("     , Nvl(ValorDescFaltaJustificada,0) ValorDescFaltaJustificada ");
                query.Append("     , Nvl(ValorDescFaltaInjustificada,0) ValorDescFaltaInjustificada ");
                query.Append("     , Nvl(QtdfaltasJustificadasSuperior,0) QtdfaltasJustificadasSuperior ");
                query.Append("     , Nvl(QtdfaltasInjustifSuperior,0) QtdfaltasInjustifSuperior ");
                query.Append("     , e.codigoGlobus || ' - ' || e.NomeAbreviado empresa");

                query.Append("  From niff_chm_empresas e,                 ");
                query.Append("       niff_chm_empautousuario eu           ");
                query.Append(" Where eu.idempresa = e.Idempresa        ");
                query.Append("   And eu.Idusuario = " + idUsuario.ToString());
                query.Append("   And e.ativo = 'S'                        ");

                query.Append(" Union All ");

                query.Append("Select e.idempresa     ");
                query.Append("     , e.Nome          ");
                query.Append("     , e.codigoglobus  ");
                query.Append("     , e.ativo         ");
                query.Append("     , Decode(eu.Idempresa,Null, 'N','S') EmpAutorizada");
                query.Append("     , nvl(eu.Idusuario, 0) Idusuario       ");
                query.Append("     , Nvl(ValorDescFaltaJustificada,0) ValorDescFaltaJustificada ");
                query.Append("     , Nvl(ValorDescFaltaInjustificada,0) ValorDescFaltaInjustificada ");
                query.Append("     , Nvl(QtdfaltasJustificadasSuperior,0) QtdfaltasJustificadasSuperior ");
                query.Append("     , Nvl(QtdfaltasInjustifSuperior,0) QtdfaltasInjustifSuperior ");
                query.Append("     , e.codigoGlobus || ' - ' || e.NomeAbreviado empresa");

                query.Append("  From niff_chm_empresas e,              ");
                query.Append("       Niff_Chm_Usuarios eu              ");
                query.Append(" Where eu.idempresa = e.Idempresa        ");
                query.Append("   And eu.Idusuario = " + idUsuario.ToString());
                query.Append("   And e.ativo = 'S'    )                   ");

                query.Append(" group by idempresa     ");
                query.Append("     , Nome          ");
                query.Append("     , codigoglobus  ");
                query.Append("     , ativo         ");
                query.Append("     , EmpAutorizada");
                query.Append("     , Idusuario       ");
                query.Append("     , ValorDescFaltaJustificada ");
                query.Append("     , ValorDescFaltaInjustificada ");
                query.Append("     , QtdfaltasJustificadasSuperior ");
                query.Append("     , QtdfaltasInjustifSuperior ");
                query.Append("     , Empresa");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        EmpresaDoUsuario _empresaDoUsuario = new EmpresaDoUsuario();

                        _empresaDoUsuario.IdUsuario = idUsuario;
                        _empresaDoUsuario.IdEmpresa = Convert.ToInt32(usuarioReader["idempresa"].ToString());

                        _empresaDoUsuario.Nome = usuarioReader["Nome"].ToString();
                        _empresaDoUsuario.CodigoeNome = usuarioReader["Empresa"].ToString();
                        _empresaDoUsuario.Ativo = usuarioReader["ativo"].ToString() == "S";
                        _empresaDoUsuario.EmpresaAutoriza = usuarioReader["EmpAutorizada"].ToString() == "S";
                        _empresaDoUsuario.CodigoEmpresaGlobus = usuarioReader["codigoglobus"].ToString();

                        _empresaDoUsuario.ValorDescontoSobreFaltaInjustificada = Convert.ToDecimal(usuarioReader["ValorDescFaltaInjustificada"].ToString());
                        _empresaDoUsuario.ValorDescontoSobreFaltaJustificada = Convert.ToDecimal(usuarioReader["ValorDescFaltaJustificada"].ToString());
                        _empresaDoUsuario.QuantidadeFaltasJustificadasSuperior = Convert.ToInt32(usuarioReader["QtdfaltasJustificadasSuperior"].ToString());
                        _empresaDoUsuario.QuantidadeFaltasInjustificadasSuperior = Convert.ToInt32(usuarioReader["QtdfaltasInjustifSuperior"].ToString());

                        _listaEmpresas.Add(_empresaDoUsuario);
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
            return _listaEmpresas;
        }

        public List<EmpresaDoUsuario> ConsultaUsuarioPorEmpresaAutorizada(int idEmpresa)
        {

            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<EmpresaDoUsuario> _listaEmpresas = new List<EmpresaDoUsuario>();

            try
            {

                query.Append("Select e.idempresa     ");
                query.Append("     , e.Nome          ");
                query.Append("     , e.codigoglobus  ");
                query.Append("     , e.ativo         ");
                query.Append("     , 'N' EmpAutorizada");
                query.Append("     , 0 Idusuario       ");
                query.Append("     , Nvl(ValorDescFaltaJustificada,0) ValorDescFaltaJustificada ");
                query.Append("     , Nvl(ValorDescFaltaInjustificada,0) ValorDescFaltaInjustificada ");
                query.Append("     , Nvl(QtdfaltasJustificadasSuperior,0) QtdfaltasJustificadasSuperior ");
                query.Append("     , Nvl(QtdfaltasInjustifSuperior,0) QtdfaltasInjustifSuperior ");
                query.Append("     , e.codigoGlobus || ' - ' || e.NomeAbreviado empresa");

                query.Append("  From niff_chm_empresas e                 ");
                query.Append(" Where e.Idempresa not in  (Select Idempresa From Niff_Chm_Empautousuario ");
                query.Append("                             Where IdEmpresa = " + idEmpresa.ToString() + ")");
                query.Append("   And e.ativo = 'S'                        ");
                query.Append(" Union All ");


                query.Append("Select e.idempresa     ");
                query.Append("     , e.Nome          ");
                query.Append("     , e.codigoglobus  ");
                query.Append("     , e.ativo         ");
                query.Append("     , Decode(eu.Idempresa,Null, 'N','S') EmpAutorizada");
                query.Append("     , nvl(eu.Idusuario, 0) Idusuario       ");
                query.Append("     , Nvl(ValorDescFaltaJustificada,0) ValorDescFaltaJustificada ");
                query.Append("     , Nvl(ValorDescFaltaInjustificada,0) ValorDescFaltaInjustificada ");
                query.Append("     , Nvl(QtdfaltasJustificadasSuperior,0) QtdfaltasJustificadasSuperior ");
                query.Append("     , Nvl(QtdfaltasInjustifSuperior,0) QtdfaltasInjustifSuperior ");
                query.Append("     , e.codigoGlobus || ' - ' || e.NomeAbreviado empresa");

                query.Append("  From niff_chm_empresas e,                 ");
                query.Append("       niff_chm_empautousuario eu           ");
                query.Append(" Where eu.idempresa = e.Idempresa        ");
                query.Append("   And eu.IdEmpresa = " + idEmpresa.ToString());
                query.Append("   And e.ativo = 'S'                        ");


                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        EmpresaDoUsuario _empresaDoUsuario = new EmpresaDoUsuario();

                        _empresaDoUsuario.IdUsuario = Convert.ToInt32(usuarioReader["IdUsuario"].ToString());
                        _empresaDoUsuario.IdEmpresa = Convert.ToInt32(usuarioReader["idempresa"].ToString());

                        _empresaDoUsuario.Nome = usuarioReader["Nome"].ToString();
                        _empresaDoUsuario.CodigoeNome = usuarioReader["Empresa"].ToString();
                        _empresaDoUsuario.Ativo = usuarioReader["ativo"].ToString() == "S";
                        _empresaDoUsuario.EmpresaAutoriza = usuarioReader["EmpAutorizada"].ToString() == "S";
                        _empresaDoUsuario.CodigoEmpresaGlobus = usuarioReader["codigoglobus"].ToString();

                        _empresaDoUsuario.ValorDescontoSobreFaltaInjustificada = Convert.ToDecimal(usuarioReader["ValorDescFaltaInjustificada"].ToString());
                        _empresaDoUsuario.ValorDescontoSobreFaltaJustificada = Convert.ToDecimal(usuarioReader["ValorDescFaltaJustificada"].ToString());
                        _empresaDoUsuario.QuantidadeFaltasJustificadasSuperior = Convert.ToInt32(usuarioReader["QtdfaltasJustificadasSuperior"].ToString());
                        _empresaDoUsuario.QuantidadeFaltasInjustificadasSuperior = Convert.ToInt32(usuarioReader["QtdfaltasInjustifSuperior"].ToString());

                        _listaEmpresas.Add(_empresaDoUsuario);
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
            return _listaEmpresas;
        }


        public List<CategoriaDoUsuario> ConsultaCategoriasAutorizadasDoUsuario(int idUsuario)
        {

            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<CategoriaDoUsuario> _listacategorias = new List<CategoriaDoUsuario>();

            try
            {

                query.Append("Select e.Idcategoria     ");
                query.Append("     , e.descricao          ");
                query.Append("     , e.ativo         ");
                query.Append("     , 'N' CatAutorizada");
                query.Append("     , 0 Idusuario       ");

                query.Append("  From niff_chm_categorias e");
                query.Append(" Where e.idcategoria not in (Select IdCategoria from niff_chm_categautousuario  ");
                query.Append("                              where idUsuario = " + idUsuario.ToString() + ")");
                query.Append("   And e.ativo = 'S'                        ");
                query.Append("   Union All ");

                query.Append("Select e.Idcategoria     ");
                query.Append("     , e.descricao          ");
                query.Append("     , e.ativo         ");
                query.Append("     , Decode(ca.Idcategoria,Null, 'N','S') CatAutorizada");
                query.Append("     , nvl(ca.Idusuario, 0) Idusuario       ");

                query.Append("  From niff_chm_categorias e,                 ");
                query.Append("       niff_chm_categautousuario ca           ");
                query.Append(" Where ca.idcategoria = e.idcategoria        ");
                query.Append("   And ca.Idusuario = " + idUsuario.ToString() );
                query.Append("   And e.ativo = 'S'                        ");


                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        CategoriaDoUsuario _categoriaDoUsuario = new CategoriaDoUsuario();

                        _categoriaDoUsuario.IdUsuario = idUsuario;
                        _categoriaDoUsuario.IdCategoria = Convert.ToInt32(usuarioReader["IdCategoria"].ToString());

                        _categoriaDoUsuario.Descricao = usuarioReader["Descricao"].ToString();
                        _categoriaDoUsuario.Ativo = usuarioReader["ativo"].ToString() == "S";
                        _categoriaDoUsuario.CategoriaAutoriza = usuarioReader["CatAutorizada"].ToString() == "S";

                        _listacategorias.Add(_categoriaDoUsuario);
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
            return _listacategorias;
        }

        public List<ModuloDoUsuario> ConsultaModulosAutorizadasDoUsuario(int idUsuario)
        {

            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<ModuloDoUsuario> _listaModulos = new List<ModuloDoUsuario>();

            try
            {

                query.Append("Select e.Idmodulo        ");
                query.Append("     , e.nome            ");
                query.Append("     , e.ativo           ");
                query.Append("     , 'N' ModAutorizada");
                query.Append("     , 0 Idusuario       ");
                query.Append("     , c.descricao                          ");
                query.Append("     , e.Idcategoria                          ");

                query.Append("  From niff_chm_modulos e, niff_chm_categorias c           ");

                query.Append(" Where e.Idmodulo not in (Select IdModulo from niff_chm_modautousuario     ");
                query.Append("                           Where IdUsuario = "+ idUsuario.ToString() + ")");
                query.Append("   And c.Idcategoria(+) = e.idcategoria    ");
                query.Append("   And e.ativo = 'S'                        ");

                query.Append(" Union all ");
                query.Append("Select e.Idmodulo        ");
                query.Append("     , e.nome            ");
                query.Append("     , e.ativo           ");
                query.Append("     , Decode(ca.Idmodulo,Null, 'N','S') ModAutorizada");
                query.Append("     , nvl(ca.Idusuario, 0) Idusuario       ");
                query.Append("     , c.descricao                          ");
                query.Append("     , c.Idcategoria                          ");

                query.Append("  From niff_chm_modulos e,                 ");
                query.Append("       niff_chm_modautousuario ca,         ");
                query.Append("       niff_chm_categorias c               ");

                query.Append(" Where ca.Idmodulo = e.Idmodulo         ");
                query.Append("   And c.Idcategoria(+) = e.idcategoria    ");
                query.Append("   And ca.Idusuario = " + idUsuario.ToString());
                query.Append("   And e.ativo = 'S'                        ");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        ModuloDoUsuario _moduloDoUsuario = new ModuloDoUsuario();

                        _moduloDoUsuario.IdUsuario = idUsuario;
                        _moduloDoUsuario.IdCategoria = Convert.ToInt32(usuarioReader["Idcategoria"].ToString());
                        _moduloDoUsuario.IdModulo = Convert.ToInt32(usuarioReader["Idmodulo"].ToString());

                        _moduloDoUsuario.Nome = usuarioReader["Nome"].ToString();
                        _moduloDoUsuario.Ativo = usuarioReader["ativo"].ToString() == "S";
                        _moduloDoUsuario.ModuloAutoriza = usuarioReader["ModAutorizada"].ToString() == "S";
                        _moduloDoUsuario.DescricaoCategoria = usuarioReader["descricao"].ToString();
                        _listaModulos.Add(_moduloDoUsuario);
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
            return _listaModulos;
        }

        public List<Usuario> ConsultaAtendentesParaACategoria(int idCategoria)
        {

            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            List<Usuario> _lista = new List<Usuario>();

            try
            {

                query.Clear();
                query.Append("Select u.email, u.nome, u.IdUsuario, u.idEmpresa, u.UsuarioAcesso");
                query.Append("  From Niff_Chm_Categautousuario c, Niff_Chm_Usuarios u");
                query.Append(" Where u.Idusuario = c.idusuario");
                query.Append("   And c.idcategoria = " + idCategoria.ToString());
                query.Append("   And u.Tipo In('A','T')");
                query.Append("   And u.Usuarioacesso <> 'MANAGER'");
                query.Append("   And u.Ativo = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader)
                {
                    while (usuarioReader.Read())
                    {
                        Usuario _usuario = new Usuario();

                        _usuario.Nome = usuarioReader["Nome"].ToString();
                        _usuario.Email = usuarioReader["Email"].ToString();
                        _usuario.UsuarioAcesso = usuarioReader["UsuarioAcesso"].ToString();

                        try
                        {
                            _usuario.IdEmpresa = Convert.ToInt32(usuarioReader["IdEmpresa"].ToString());
                        }
                        catch { }
                        _usuario.Id = Convert.ToInt32(usuarioReader["IdUsuario"].ToString());
                        _lista.Add(_usuario);
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

        public void IncluiUsuariosCriadoNoGlobus()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                // com funcionario associado no globus ao usuário
                query.Clear();
                query.Append("Select ub.codintfunc, e.Idempresa ");
                query.Append("     , ub.usuario, f.dtadmfunc, d.nrdocto cpf, e.Nome, f.nomefunc, f.dtnasctofunc, f.dtadmfunc");
                query.Append("  From Niff_Chm_Usuarios u, ctr_cadastrodeusuarios ub");
                query.Append("     , flp_funcionarios f, niff_chm_empresas e, Flp_Documentos d");
                query.Append(" Where u.usuarioacesso(+) = ub.usuario");
                query.Append("   And f.codintfunc = ub.codintfunc");
                query.Append("   And ub.ativo = 'S'");
                query.Append("   And d.codintfunc = f.codintfunc");
                query.Append("   And d.tipodocto = 'CPF'");
                query.Append("   And e.codigoglobus = lpad(f.codigoempresa, 3, '0') || '/' || decode(f.Codigoempresa,9, '001', lPad(f.Codigofl, 3, '0'))");
                query.Append("   And u.usuarioacesso Is Null");
                query.Append("   And ub.usuario Not Like '%2'");
                query.Append("   And ub.usuario Not In ('BGM', 'ECF', 'GLOBUS', 'BGMSUPORTE', 'MONITABC', 'MONITCAMP', 'TRAFEGOURBANO', 'PORTARIA', 'TERMINALCAMP', 'PLANTAO', 'TRAFEGORODOV','TOTEMCAMP', 'MANUTENCAO', 'PLANTAOEOVG', 'PORTARIACISNE', 'PLANTAOVUG', 'TERMINALRPDO', 'TERMINAL', 'TERMINALCISNE', 'AMBULATORIO09')");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader) // ajustar o CPF
                {
                    while (usuarioReader.Read())
                    {
                        query.Clear();
                        query.Append("Insert Into Niff_Chm_Usuarios (idusuario, nome, ativo, usuarioacesso, senha, DtNascimento, Idempresa, Cpf, dataadmissao)");
                        query.Append(" Values( SQ_NIFF_IDUsuario.Nextval, '" + usuarioReader["nomefunc"].ToString() + "', 'S', '");
                        query.Append(  usuarioReader["usuario"].ToString() + "', '" + usuarioReader["Cpf"].ToString().Substring(0, 6) + "'");
                        query.Append(", To_date('" + Convert.ToDateTime(usuarioReader["dtnasctofunc"].ToString()).ToShortDateString() + "', 'dd/mm/yyyy')");
                        query.Append("," + usuarioReader["Idempresa"].ToString() + ", '" + usuarioReader["CPF"].ToString() + "' ");
                        query.Append(", To_date('" + Convert.ToDateTime(usuarioReader["dtadmfunc"].ToString()).ToShortDateString() + "', 'dd/mm/yyyy') )");

                        if (sessao.ExecuteSqlTransaction(query.ToString()))
                        {
                            Log _log = new Log();
                            _log.IdUsuario = Publicas._usuario.Id;
                            _log.Descricao = "Incluiu o usuário " + usuarioReader["usuario"].ToString() + " - Automaticamente";
                            _log.Tela = "Cadastro de Usuários";

                            try
                            {
                                new LogDAO().Gravar(_log);
                            }
                            catch { }
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
        }

        public void DesativaUsuarios()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                // Traz os usuários inativos no globus que estão ativos 
                query.Clear();
                query.Append("Select Ub.Usuario");
                query.Append("  From Niff_Chm_Usuarios u");
                query.Append(" Where u.ativo = 'S'");
                query.Append(" Where CodFunc In (Select CodIntFunc From Vw_Funcionarios f");
                query.Append("                    Where f.CODINTFUNC In(Select codIntFunc");
                query.Append("                     From niff_ads_colaboradores t");
                query.Append("                    Where codIntFunc Is Not Null)");
                query.Append("                      And f.SITUACAOFUNC = 'D')");

                Query executar = sessao.CreateQuery(query.ToString());

                usuarioReader = executar.ExecuteQuery();

                using (usuarioReader) // ajustar o CPF
                {
                    while (usuarioReader.Read())
                    {
                        query.Clear();
                        query.Append("UpDate Niff_Chm_Usuarios ");
                        query.Append("   set ativo = 'N'");
                        query.Append(" Where usuarioacesso = '" + usuarioReader["usuario"].ToString() + "'");

                        if (sessao.ExecuteSqlTransaction(query.ToString()))
                        {
                            Log _log = new Log();
                            _log.IdUsuario = Publicas._usuario.Id;
                            _log.Descricao = "Alterou o usuário " + usuarioReader["usuario"].ToString() + " [ATIVO] de SIM para NÃO - Automaticamente";
                            _log.Tela = "Cadastro de Usuários";

                            try
                            {
                                new LogDAO().Gravar(_log);
                            }
                            catch { }
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
        }

        public void AplicarTema()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                // Traz os usuários inativos no globus que estão ativos no Radiance
                query.Clear();
                query.Append("UpDate Niff_Chm_Usuarios ");
                query.Append("   set TemaBlackSelecionado = '" + (Publicas._usuario.TemaBlackSelecionado ? "S" : "N") + "'");
                query.Append(" Where idUsuario = " + Publicas._usuario.Id);

                if (sessao.ExecuteSqlTransaction(query.ToString()))
                {
                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Alterou o usuário " + usuarioReader["usuario"].ToString() + " [TemaBlackSelecionado] de SIM para NÃO";
                    _log.Tela = "Cadastro de Usuários";

                    try
                    {
                        new LogDAO().Gravar(_log);
                    }
                    catch { }
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

        public void FiltroChamado()
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                // Traz os usuários inativos no globus que estão ativos no Radiance
                query.Clear();
                query.Append("UpDate Niff_Chm_Usuarios ");
                query.Append("   set SoChamadosDesseUsuario = '" + (Publicas._usuario.SoChamadosDesseUsuario ? "S" : "N") + "'");
                query.Append(" Where idUsuario = " + Publicas._usuario.Id);

                if (sessao.ExecuteSqlTransaction(query.ToString()))
                {
                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    try
                    {
                        _log.Descricao = "Alterou o usuário " + usuarioReader["usuario"].ToString() + " [SoChamadosDesseUsuario] de SIM para NÃO";
                        _log.Tela = "Cadastro de Usuários";
                    }
                    catch (NullReferenceException excessao)
                    {
                        Console.WriteLine(excessao.StackTrace);
                    }

                    try
                    {
                        new LogDAO().Gravar(_log);
                    }
                    catch{}
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
            