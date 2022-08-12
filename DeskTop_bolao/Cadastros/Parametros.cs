using Classes;
using Negocio;
using Suportte.Notificacoes;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.Cadastros
{
    public partial class Parametros : Form
    {
        public Parametros()
        {
            InitializeComponent();

            horaFimDateTimePicker.BorderColor = Publicas._bordaSaida;
            horaInicioDateTimePicker.BorderColor = Publicas._bordaSaida;

            horaFimDateTimePicker.BackColor = emailTextBox.BackColor;
            horaInicioDateTimePicker.BackColor = emailTextBox.BackColor;
            responderCurrencyTextBox.BackGroundColor = emailTextBox.BackColor;
            cancelarCurrencyTextBox.BackGroundColor = emailTextBox.BackColor;
            reaberturaCurrencyTextBox.BackGroundColor = emailTextBox.BackColor;
            mesesDashCurrencyTextBox.BackGroundColor = emailTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Parametro _parametro;
        Parametro _parametroLog;
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

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Parametros_Shown(object sender, EventArgs e)
        {
            UsuarioComboBox.Items.AddRange(new object[] { "Solicitante", "Atendente", "Administrador" });
            tipoFormatoComboBox.Items.AddRange(new object[] { "Ano", "Ano e mês", "Sequencial" });
            separadorComboBox.Items.AddRange(new object[] { "", "-", "/", "." });

            UsuarioComboBox.SelectedIndex = 2;
            tipoFormatoComboBox.SelectedIndex = 1;
            separadorComboBox.SelectedIndex = 1;

            // buscar o já gravado
            _parametro = new ParametrosBO().Consultar();

            if (_parametro.Existe)
            {
                _parametroLog = new ParametrosBO().Consultar();
                emailTextBox.Text = _parametro.Email;
                smtpTextBox.Text = _parametro.Smtp;
                portaTextBox.Text = _parametro.PortaSmtp.ToString();
                senhaTextBox.Text = _parametro.Senha;
                autenticaCheckBox.Checked = _parametro.Autentica;
                autenticaSSLCheckBox.Checked = _parametro.AutenticaSLL;

                responderCurrencyTextBox.DecimalValue = _parametro.PrazoRetorno;
                cancelarCurrencyTextBox.DecimalValue = _parametro.CancelarVisivelPor;
                reaberturaCurrencyTextBox.DecimalValue = _parametro.PrazoReabertura;

                horaInicioDateTimePicker.Value = _parametro.HoraInicioAgenda;
                horaFimDateTimePicker.Value = _parametro.HoraFimAgenda;

                usuariosMesmoDepartamentoCheckBox.Checked = _parametro.UsuarioComMesmoDepartamentoPodeVerChamados;
                exigeAvaliacaoCheckBox.Checked = _parametro.ExigeAvaliacao;
                atendentePodeAbrirCheckBox.Checked = _parametro.AtentendePodeAbrirChamado;
                atendentePodeConcluirCheckBox.Checked = _parametro.AtentendePodeConcluirChamado;

                UsuarioComboBox.SelectedIndex = (_parametro.UsuarioQuePodeCancelarChamado == Publicas.TipoUsuarioCancela.Solicitante ? 0 :
                    (_parametro.UsuarioQuePodeCancelarChamado == Publicas.TipoUsuarioCancela.Atendente ? 0 : 2));

                tipoFormatoComboBox.SelectedIndex = (_parametro.FormatoChamado == Publicas.TipoCalculoChamado.Ano ? 0 :
                                                    (_parametro.FormatoChamado == Publicas.TipoCalculoChamado.AnoMes ? 1 : 2 ));

                separadorComboBox.SelectedIndex = (_parametro.Separador == "-" ? 1 :
                    (_parametro.Separador == "/" ? 2 :
                    (_parametro.Separador == "." ? 3 : 0)));

                mesesDashCurrencyTextBox.DecimalValue = _parametro.MesesConsultaDashboardChamados;
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _parametro.Existe;
            emailTextBox.Focus();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            emailTextBox.Text = string.Empty;
            smtpTextBox.Text = string.Empty;
            portaTextBox.Text = string.Empty;
            senhaTextBox.Text = string.Empty;
            autenticaCheckBox.Checked = true;
            autenticaSSLCheckBox.Checked = false;

            responderCurrencyTextBox.DecimalValue = 0;
            cancelarCurrencyTextBox.DecimalValue = 0;
            reaberturaCurrencyTextBox.DecimalValue = 0;

            horaInicioDateTimePicker.Value = DateTime.Now;
            horaFimDateTimePicker.Value = DateTime.Now;

            usuariosMesmoDepartamentoCheckBox.Checked = false;
            exigeAvaliacaoCheckBox.Checked = false;
            atendentePodeAbrirCheckBox.Checked = false;
            atendentePodeConcluirCheckBox.Checked = false;

            UsuarioComboBox.SelectedIndex = 2;
            tipoFormatoComboBox.SelectedIndex = 1;
            separadorComboBox.SelectedIndex = 1;
            mesesDashCurrencyTextBox.DecimalValue = 3;
            emailTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new ParametrosBO().Excluir(_parametro))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu o Parâmetro geral";
            _log.Tela = "Cadastro de Parâmetros";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_parametro == null)
                _parametro = new Parametro();

            _parametro.Email = emailTextBox.Text;
            _parametro.Smtp = smtpTextBox.Text;

            if (!string.IsNullOrEmpty(portaTextBox.Text.Trim()))
                _parametro.PortaSmtp = Convert.ToInt32(portaTextBox.Text);

            _parametro.Senha = senhaTextBox.Text;
            _parametro.Autentica = autenticaCheckBox.Checked;
            _parametro.AutenticaSLL = autenticaSSLCheckBox.Checked;

            _parametro.PrazoRetorno = Convert.ToInt32(responderCurrencyTextBox.DecimalValue);
            _parametro.CancelarVisivelPor = Convert.ToInt32(cancelarCurrencyTextBox.DecimalValue);
            _parametro.PrazoReabertura = Convert.ToInt32(reaberturaCurrencyTextBox.DecimalValue);

            _parametro.HoraInicioAgenda = horaInicioDateTimePicker.Value;
            _parametro.HoraFimAgenda = horaFimDateTimePicker.Value;

            _parametro.UsuarioComMesmoDepartamentoPodeVerChamados = usuariosMesmoDepartamentoCheckBox.Checked;
            _parametro.ExigeAvaliacao = exigeAvaliacaoCheckBox.Checked;
            _parametro.AtentendePodeAbrirChamado = atendentePodeAbrirCheckBox.Checked;
            _parametro.AtentendePodeConcluirChamado = atendentePodeConcluirCheckBox.Checked;

            _parametro.UsuarioQuePodeCancelarChamado = (UsuarioComboBox.SelectedIndex == 0 ? Publicas.TipoUsuarioCancela.Solicitante :
                (UsuarioComboBox.SelectedIndex == 1 ? Publicas.TipoUsuarioCancela.Atendente : Publicas.TipoUsuarioCancela.Administrador));

            _parametro.FormatoChamado = (tipoFormatoComboBox.SelectedIndex == 0 ? Publicas.TipoCalculoChamado.Ano : 
                                        (tipoFormatoComboBox.SelectedIndex == 1 ? Publicas.TipoCalculoChamado.AnoMes : Publicas.TipoCalculoChamado.Sequencial));

            _parametro.Separador = separadorComboBox.Text;
            _parametro.MesesConsultaDashboardChamados = (int)mesesDashCurrencyTextBox.DecimalValue;
            if (!new ParametrosBO().Gravar(_parametro))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            string _descricaoLog  = "";

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;

            if (_parametro.Existe)
            {
                _descricaoLog = (_parametro.Email == _parametroLog.Email ? "" : " [Email] de " + _parametro.Email + " para " + _parametro.Email + "") +
                    (_parametro.PortaSmtp == _parametroLog.PortaSmtp ? "" : " [Smtp] de " + _parametro.PortaSmtp.ToString() + " para " + _parametro.PortaSmtp + "") +
                    (_parametro.Smtp == _parametroLog.Smtp ? "" : " [Smtp] de " + _parametro.Smtp + " para " + _parametro.Smtp + "") +
                    (_parametro.Autentica == _parametroLog.Autentica ? "" : " [Autentica] de " + _parametro.Autentica.ToString() + " para " + _parametro.Autentica.ToString() + "") +
                    (_parametro.AutenticaSLL == _parametroLog.AutenticaSLL ? "" : " [AutenticaSLL] de " + _parametro.AutenticaSLL.ToString() + " para " + _parametro.AutenticaSLL.ToString() + "") +
                    (_parametro.PrazoRetorno == _parametroLog.PrazoRetorno ? "" : " [PrazoRetorno] de " + _parametro.PrazoRetorno.ToString() + " para " + _parametro.PrazoRetorno.ToString() + "") +
                    (_parametro.CancelarVisivelPor == _parametroLog.CancelarVisivelPor ? "" : " [CancelarVisivelPor] de " + _parametro.CancelarVisivelPor.ToString() + " para " + _parametro.CancelarVisivelPor.ToString() + "") +
                    (_parametro.HoraInicioAgenda == _parametroLog.HoraInicioAgenda ? "" : " [HoraInicioAgenda] de " + _parametro.HoraInicioAgenda.ToString() + " para " + _parametro.HoraInicioAgenda.ToString() + "") +
                    (_parametro.HoraFimAgenda == _parametroLog.HoraFimAgenda ? "" : " [HoraFimAgenda] de " + _parametro.HoraFimAgenda.ToString() + " para " + _parametro.HoraFimAgenda.ToString() + "") +
                    (_parametro.UsuarioComMesmoDepartamentoPodeVerChamados == _parametroLog.UsuarioComMesmoDepartamentoPodeVerChamados ? "" : " [UsuarioComMesmoDepartamentoPodeVerChamados] de " + _parametro.UsuarioComMesmoDepartamentoPodeVerChamados.ToString() + " para " + _parametro.UsuarioComMesmoDepartamentoPodeVerChamados.ToString() + "") +
                    (_parametro.ExigeAvaliacao == _parametroLog.ExigeAvaliacao ? "" : " [ExigeAvaliacao] de " + _parametro.ExigeAvaliacao.ToString() + " para " + _parametro.ExigeAvaliacao.ToString() + "") +
                    (_parametro.AtentendePodeAbrirChamado == _parametroLog.AtentendePodeAbrirChamado ? "" : " [AtentendePodeAbrirChamado] de " + _parametro.AtentendePodeAbrirChamado.ToString() + " para " + _parametro.AtentendePodeAbrirChamado.ToString() + "") +
                    (_parametro.AtentendePodeConcluirChamado == _parametroLog.AtentendePodeConcluirChamado ? "" : " [AtentendePodeConcluirChamado] de " + _parametro.AtentendePodeConcluirChamado.ToString() + " para " + _parametro.AtentendePodeConcluirChamado.ToString() + "") +
                    (_parametro.UsuarioQuePodeCancelarChamado == _parametroLog.UsuarioQuePodeCancelarChamado ? "" : " [UsuarioQuePodeCancelarChamado] de " + _parametro.UsuarioQuePodeCancelarChamado.ToString() + " para " + _parametro.UsuarioQuePodeCancelarChamado.ToString() + "") +
                    (_parametro.FormatoChamado == _parametroLog.FormatoChamado ? "" : " [FormatoChamado] de " + _parametro.FormatoChamado.ToString() + " para " + _parametro.FormatoChamado.ToString() + "") +
                    (_parametro.Separador == _parametroLog.Separador ? "" : " [Separador] de " + _parametro.Separador.ToString() + " para " + _parametro.Separador.ToString() + "");
            }

            _log.Descricao = (_parametro.Existe ? "Alterou" : "Incluiu") + " o Parâmetro geral" + _descricaoLog;

            _log.Tela = "Cadastro de Parâmetros";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void emailTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void responderCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void tipoFormatoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void horaInicioDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void emailTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void responderCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;
        }

        private void tipoFormatoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void horaInicioDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void emailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                senhaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                emailTextBox.Focus();
            }
        }

        private void senhaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                smtpTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                emailTextBox.Focus();
            }
        }

        private void smtpTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                portaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                senhaTextBox.Focus();
            }
        }

        private void portaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                autenticaCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                smtpTextBox.Focus();
            }
        }

        private void autenticaCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                autenticaSSLCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                portaTextBox.Focus();
            }
        }

        private void autenticaSSLCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                responderCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                autenticaCheckBox.Focus();
            }
        }

        private void responderCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cancelarCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                autenticaSSLCheckBox.Focus();
            }
        }

        private void cancelarCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                reaberturaCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                responderCurrencyTextBox.Focus();
            }
        }

        private void reaberturaCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                horaInicioDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cancelarCurrencyTextBox.Focus();
            }
        }

        private void horaInicioDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                horaFimDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                reaberturaCurrencyTextBox.Focus();
            }
        }

        private void horaFimDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoFormatoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                horaInicioDateTimePicker.Focus();
            }
        }

        private void usuariosMesmoDepartamentoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                exigeAvaliacaoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                mesesDashCurrencyTextBox.Focus();
            }
        }

        private void exigeAvaliacaoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                atendentePodeAbrirCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                usuariosMesmoDepartamentoCheckBox.Focus();
            }
        }

        private void atendentePodeAbrirCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                atendentePodeConcluirCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                exigeAvaliacaoCheckBox.Focus();
            }
        }

        private void atendentePodeConcluirCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                atendentePodeAbrirCheckBox.Focus();
            }
        }

        private void tipoFormatoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                separadorComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                horaFimDateTimePicker.Focus();
            }
        }

        private void separadorComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                UsuarioComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoFormatoComboBox.Focus();
            }
        }

        private void UsuarioComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                mesesDashCurrencyTextBox.Focus();

            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                separadorComboBox.Focus();
            }
        }


        private void gravarButton_Enter(object sender, EventArgs e)
        {
            gravarButton.BackColor = Publicas._botaoFocado;
            gravarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void limparButton_Enter(object sender, EventArgs e)
        {
            limparButton.BackColor = Publicas._botaoFocado;
            limparButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void excluirButton_Enter(object sender, EventArgs e)
        {
            excluirButton.BackColor = Publicas._botaoFocado;
            excluirButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void excluirButton_Validating(object sender, CancelEventArgs e)
        {
            excluirButton.BackColor = Publicas._botao;
            excluirButton.ForeColor = Publicas._fonteBotao;
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            limparButton.BackColor = Publicas._botao;
            limparButton.ForeColor = Publicas._fonteBotao;
        }

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                atendentePodeConcluirCheckBox.Focus();
            }
        }

        private void limparButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gravarButton.Focus();
            }
        }

        private void excluirButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                limparButton.Focus();
            }
        }

        private void mesesDashCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                usuariosMesmoDepartamentoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                UsuarioComboBox.Focus();
            }
        }
    }
}
