using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    
    /* tabela NIFF_CHM_Usuarios*/

    [Serializable]
    public class Usuario
    {
        #region propriedades
        public int Id { get; set; }
        public string UsuarioAcesso { get; set; }
        public Publicas.TipoUsuario Tipo { get; set; }
        public Publicas.TipoUsuarioSAC TipoSac { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public bool Administrador { get; set; }
        public string IpMaquina { get; set; }
        public string NomeMaquina { get; set; }
        public decimal Telefone { get; set; }
        public int Ramal { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Cargo { get; set; }
        public DateTime DataNascimento { get; set; }
        public Byte[] Foto { get; set; }
        public bool AcessaAgenda { get; set; }
        public bool AcessaChat { get; set; }
        public bool PermiteExcluirChat { get; set; }
        public bool AcessaBI { get; set; }
        public bool PermiteIncluirExcluirFoto { get; set; }
        public string EmailAcessoPowerBi { get; set; }
        public string Setor { get; set; }
        public bool Existe { get; set; }
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public decimal CodigoInternoFuncionarioGlobus { get; set; }
        public string RegistroFuncionario { get; set; }
        public bool AcessaSac { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public string CPF { get; set; }
        public string EmailDepartamento { get; set; }
        public bool AcessaDescontoBeneficio { get; set; }
        public bool AcessaJuridico { get; set; }
        public bool AcessaCadastroJuridico { get; set; }
        public bool PermiteAprovarComunicado { get; set; }
        public bool PermiteReprovarComunicado { get; set; }
        public bool PermiteCancelarComunicado { get; set; }
        public int IdCargo { get; set; }
        public bool PermiteAlterarComunicado { get; set; }
        public bool PermiteFinalizarComunicado { get; set; }
        public bool AcessaDashBoardChamados { get; set; }
        public bool AcessaAvaliacaoDesempenho { get; set; }
        public bool AcessoDeRH { get; set; }
        public bool AcessoDeGestor { get; set; }
        public bool AcessoDeColaborador { get; set; }
        public bool AcessoDeControladoria { get; set; }
        public bool NaoNotificaCorridas { get; set; }
        public bool AniversariantesApenasDaEmpresa { get; set; }
        public bool VisualizaRadarCompleto { get; set; }
        public bool VisualizaBancoHorasDoDepartamento { get; set; }
        public bool ParticipaBolaoCopa { get; set; }
        public bool AdministraBolaoCopa { get; set; }
        public bool AdministraBiblioteca { get; set; }
        public bool AdministraCorridas { get; set; }
        public bool AcessaBSC { get; set; }
        public bool AcessaMetasFinanceiras { get; set; }
        public bool AcessaMetasOperacionais { get; set; }
        public bool PermiteBuscarResultado { get; set; }
        public bool PermiteAlterarBSC { get; set; }
        public bool TemaBlackSelecionado { get; set; }
        public bool SoChamadosDesseUsuario { get; set; }

        public bool AcessaRecebedoria { get; set; }
        public bool PodeExportarSigomExcel{ get; set; }
        public bool AcessaOperacional { get; set; }
        public bool AcessaCadastroOperacional { get; set; }
        public bool AcessaDemonstrativo { get; set; }
        public bool AcessaIQO { get; set; }
        public bool PodeFinalizarChamado { get; set; }

        public string AssinaturaChamado { get; set; }
        public DateTime DataAdmissao { get; set; }
        public int QuantidadeDeAnos { get; set; }

        public bool AlteraBSCIndicadoresManuais { get; set; }
        public bool AcessaFinanceiro { get; set; }
        public bool AcessaCadastrosFinanceiro { get; set; }
        public bool AcessaDemonstrativoFluxoCaixa { get; set; }
        public bool AcessaResumoFluxoCaixa { get; set; }

        public bool AcessaContabilidade { get; set; }
        public bool AcessaEscrituracaoFiscal { get; set; }

        public bool AcessaRamais { get; set; }

        public bool AcessaRateioCTB { get; set; }
        public bool AcessaCadastroBeneficioRateio { get; set; }
        public bool AcessaBeneficioRateio { get; set; }
        public bool AcessaCalculoRateio { get; set; }
        public bool AcessaDepartamentoPessoal { get; set; }
        public bool RecebeEmailNotaFiscal { get; set; }

        public bool Desenvolvedor { get; set; }
        public bool Gerente { get; set; }
        public bool Coordenador { get; set; }
        public bool Diretor { get; set; }

        public bool AcessaDRE { get; set; }
        public bool AcessaCadastroMetas { get; set; }
        public bool PermiteReabrirDRE { get; set; }
        public bool ApenasConsultaDRE { get; set; }
        public bool ApenasEditarPrevistoDRE { get; set; }

        public bool AcessaLalur { get; set; }
        public bool AcessaCadastroLalur { get; set; }
        public bool AcessaCalculoLalur { get; set; }

        public bool AgendaLiberaCarros { get; set; }
        public bool AcessaEndividamento { get; set; }
        public bool AcessaParcelamento { get; set; }

        public bool SempreMostrarListaDeChamados { get; set; }
        public bool ReprocessaParcelamento { get; set; }
        public bool AcessaCigam { get; set; }
        public bool AcessaCTBNotasFicais { get; set; }

        public bool PodeIntegrarProgramacaoFerias { get; set; }
        public bool AcessaPerAquisitoFerias { get; set; }
          
        public bool AcessaSuprimentos { get; set; }
        public bool AcessaMetasSuprimentos { get; set; }
        public bool AcessaConciliacaoContabil { get; set; }
        public bool AcessaCadastrosCTBNotasFiscais { get; set; }
        public bool AcessaConciliacaoBCOApenasConsulta { get; set; }

        public bool RecebeEmailDasDiferencasDoSigonProdata { get; set; }

        public bool AbreServicoExcel { get; set; }
        #endregion

    }
}
