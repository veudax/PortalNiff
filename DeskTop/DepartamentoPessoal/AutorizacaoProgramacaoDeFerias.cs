using Classes;
using Negocio;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.DepartamentoPessoal
{
    public partial class AutorizacaoProgramacaoDeFerias : Form
    {
        public AutorizacaoProgramacaoDeFerias()
        {
            InitializeComponent();
            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                inicialDateTimePicker.Style = VisualStyle.Office2016Black;
                FimDateTimePicker.Style = VisualStyle.Office2016Black;
            }
            inicialDateTimePicker.Value = DateTime.Now;
            FimDateTimePicker.Value = DateTime.Now;
            QuantidadeCurrencyTextBox.BackGroundColor = empresaComboBoxAdv.BackColor;
            inicialDateTimePicker.BorderColor = Publicas._bordaSaida;
            FimDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._TemaBlack)
                QuantidadeCurrencyTextBox.PositiveColor = Publicas._fonte;

            Publicas._mensagemSistema = string.Empty;
        }

        Classes.ProgramacaoFerias _programacao;
        Classes.Empresa _empresa;
        Classes.Usuario _usuario;
        List<Classes.Empresa> _listaEmpresas;
        Classes.FuncionariosGlobus _funcionariosGlobus;
        List<Usuario> _listaUsuarios;

        #region Move tela
        bool clicouNoPanel;
        int posIniX;
        int posIniY;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            clicouNoPanel = true;
            posIniX = e.X;
            posIniY = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            clicouNoPanel = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicouNoPanel)
            {
                this.SetDesktopLocation(MousePosition.X - posIniX, MousePosition.Y - posIniY);
            }
        }
        #endregion

        private void AutorizacaoProgramacaoDeFerias_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }

            _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(usuarioTextBox.Text, _empresa.CodigoEmpresaGlobus);

            _programacao = new ProgramacaoFeriasBO().Consultar(_funcionariosGlobus.Id, inicialDateTimePicker.Value);

            gravarButton.Enabled = true;

            StatusLabel.Text = "Aguardando Aprovação";

            _usuario = new UsuarioBO().ConsultaUsuarioPorCodigoFuncionarioGlobus(_funcionariosGlobus.Id);
            nomeTextBox.Text = _usuario.Nome;

            if (_programacao.Existe)
            {

                QuantidadeCurrencyTextBox.DecimalValue = _programacao.QuantidadeDias;
                FimDateTimePicker.Value = _programacao.DataFim;
                StatusLabel.Text = (_programacao.Status == "G" ? "Aguardando Aprovação" : (_programacao.Status == "A" ? "Aprovado" : "Reprovado"));
                
                if (_programacao.IniPeriodoAquisitivo != DateTime.MinValue)
                {
                    PeriodoAquisitivoLabel.Text = "Admissão: " + _usuario.DataAdmissao.ToShortDateString() +
                    " - Período Aquisitivo: " + _programacao.IniPeriodoAquisitivo.ToShortDateString() + " - " + _programacao.FimPeriodoAquisitivo.ToShortDateString() +
                    " - Limite: " + _programacao.Limite.ToShortDateString();
                }
            }

            _listaUsuarios = new List<Usuario>();

            try
            {
                _listaUsuarios = new UsuarioBO().ListarUsuarios(true, 19); //support
            }
            catch
            {
                
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AutorizadoRadioButton.Focus();
            }
        }

        private void gravarButton_Enter(object sender, EventArgs e)
        {
            gravarButton.BackColor = Publicas._botaoFocado;
            gravarButton.ForeColor = Publicas._fonteBotaoFocado;
        }
        
        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (!AutorizadoRadioButton.Checked && !reprovadoRadioButton.Checked)
            {
                new Notificacoes.Mensagem("Informe se Autoriza ou Reprova a Programação de Férias.", Publicas.TipoMensagem.Alerta).ShowDialog();
                AutorizadoRadioButton.Focus();
                return;
            }

            _programacao.QuantidadeDias = (int)QuantidadeCurrencyTextBox.DecimalValue;
            _programacao.DataInicio = inicialDateTimePicker.Value;
            _programacao.DataFim = FimDateTimePicker.Value;
            _programacao.CodIntFunc = _funcionariosGlobus.Id;
            _programacao.IdEmpresa = _empresa.IdEmpresa;

            if (_programacao.Status != "R" && reprovadoRadioButton.Checked)
                _programacao.MotivoReprovacao = Publicas._motivoCancelamentoDevolucao;

            if (Publicas._usuario.Diretor)
                _programacao.VisualizadoPeloDiretor = true;
            else
            {
                if (Publicas._usuario.Gerente)
                    _programacao.VisualizadoPeloGerente = true;
                else
                {
                    if (Publicas._usuario.Coordenador)
                        _programacao.VisualizadoPeloCoordenador = true;
                    else
                        _programacao.Visualizado = true;
                }
            }

            _programacao.Status = (AutorizadoRadioButton.Checked ? "A" : "R");

            if (!new ProgramacaoFeriasBO().Gravar(_programacao))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }
            
            string[] _dadosEmail = new string[50];
            string[] nome = nomeTextBox.Text.Split(' ');
            string[] gestor = Publicas._usuario.Nome.Split(' ');

            _dadosEmail[0] = nome[0] + " " + nome[nome.Length - 1];
            _dadosEmail[1] = inicialDateTimePicker.Value.ToShortDateString();
            _dadosEmail[2] = FimDateTimePicker.Value.ToShortDateString() + " (" + QuantidadeCurrencyTextBox.Text + " dias )";
            _dadosEmail[3] = (AutorizadoRadioButton.Checked ? " Aprovada " : " Reprovada ");
            _dadosEmail[4] = gestor[0] + " " + gestor[gestor.Length -1];
            _dadosEmail[5] = DateTime.Now.Date.ToShortDateString();
            _dadosEmail[6] = "";

            if (_programacao.Status == "R")
                _dadosEmail[6] = "<p> motivo: " + _programacao.MotivoReprovacao + "</p>";

            string emailDestino = _usuario.Email + "; " + Publicas._usuario.Email;

            string emailCopia = "";

            foreach (var item in _listaUsuarios.Where(w => w.PodeIntegrarProgramacaoFerias && w.IdEmpresa == _empresa.IdEmpresa))
            {
                if (item.Email.Contains("mdmunoz"))
                    continue;

                emailCopia = emailCopia + item.EmailDepartamento + "; ";
            }
            
            Publicas.mensagemDeErro = "";

            Classes.Publicas.EnviarEmailProgramacaoFerias(_dadosEmail, Publicas._usuario.Email, emailDestino, emailCopia, "Programação de Férias");

            if (Publicas.mensagemDeErro != "")
                new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Alerta).ShowDialog();
            else
                new Notificacoes.Mensagem("E-mail enviado com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (AutorizadoRadioButton.Checked ? "Aprovou" : "Reprovou") + " Programação de Férias de " +nomeTextBox.Text + 
                " periodo de " + inicialDateTimePicker.Value.ToShortDateString() + " ate " + FimDateTimePicker.Value.ToShortDateString() +
                " da empresa " + empresaComboBoxAdv.Text;

            _log.Tela = "Recursos Humanos - Programação Férias";
            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            Close(); 
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void reprovadoRadioButton_Click(object sender, EventArgs e)
        {
            if (reprovadoRadioButton.Checked)
            {
                new SAC.Motivo().ShowDialog();

                if (Publicas._cancelouMotivo)
                    new Notificacoes.Mensagem("Informe o motivo da Repovação.", Publicas.TipoMensagem.Alerta).ShowDialog();
                
            }
        }
    }
}
