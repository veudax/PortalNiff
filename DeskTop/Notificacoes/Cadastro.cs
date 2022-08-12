using Classes;
using Negocio;
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

namespace Suportte.Notificacoes
{
    public partial class Cadastro : Form
    {
        public Cadastro()
        {
            InitializeComponent();

            dataDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataDateTimePicker.BackColor = motivoTextBox.BackColor;
            dataDateTimePicker.Value = DateTime.Now;

            dataAcaoDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataAcaoDateTimePicker.BackColor = motivoTextBox.BackColor;
            dataAcaoDateTimePicker.Value = DateTime.Now;

            horaAcaoDateTimePicker.BorderColor = Publicas._bordaSaida;
            horaAcaoDateTimePicker.BackColor = motivoTextBox.BackColor;
            horaAcaoDateTimePicker.Value = DateTime.Now;

            dataAcaoFimDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataAcaoFimDateTimePicker.BackColor = motivoTextBox.BackColor;
            dataAcaoFimDateTimePicker.Value = DateTime.Now;

            horaAcaoFimDateTimePicker.BorderColor = Publicas._bordaSaida;
            horaAcaoFimDateTimePicker.BackColor = motivoTextBox.BackColor;
            horaAcaoFimDateTimePicker.Value = DateTime.Now;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;

            dataDateTimePicker.Focus();
        }

        Classes.NotificacaoDoSistema _notificacao;

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

        private void limparButton_Click(object sender, EventArgs e)
        {
            motivoTextBox.Text = string.Empty;
            dataAcaoFimDateTimePicker.Value = DateTime.Now;
            dataAcaoFimDateTimePicker.Value = DateTime.Now;
            dataDateTimePicker.Value = DateTime.Now;
            horaAcaoDateTimePicker.Value = DateTime.Now;
            horaAcaoFimDateTimePicker.Value = DateTime.Now;

            dataDateTimePicker.Focus();
        }

        private void dataDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataAcaoDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataDateTimePicker.Focus();
            }
        }

        private void dataAcaoDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                horaAcaoDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataDateTimePicker.Focus();
            }
        }

        private void horaAcaoDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataAcaoFimDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                horaAcaoDateTimePicker.Focus();
            }
        }

        private void dataAcaoFimDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                horaAcaoFimDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                horaAcaoDateTimePicker.Focus();
            }
        }

        private void horaAcaoFimDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataAcaoFimDateTimePicker.Focus();
            }
        }

        private void motivoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                motivoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                horaAcaoFimDateTimePicker.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                motivoTextBox.Focus();
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

        private void dataDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void motivoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
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

        private void dataAcaoDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (dataAcaoDateTimePicker.Value.Date < DateTime.Now.Date)
            {
                new Notificacoes.Mensagem("Data início da ação deve ser maior ou igual a data atual.", Publicas.TipoMensagem.Alerta).ShowDialog();
                dataAcaoDateTimePicker.Focus();
                return;
            }

            if (dataAcaoDateTimePicker.Value.Date < dataDateTimePicker.Value.Date)
            {
                new Notificacoes.Mensagem("Data início da ação deve ser maior ou igual a data da notificação.", Publicas.TipoMensagem.Alerta).ShowDialog();
                dataAcaoDateTimePicker.Focus();
                return;
            }
        }

        private void dataAcaoFimDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            //if (dataAcaoFimDateTimePicker.Value.Date < DateTime.Now.Date)
            //{
            //    new Notificacoes.Mensagem("Data final da ação deve ser maior ou igual a data atual.", Publicas.TipoMensagem.Alerta).ShowDialog();
            //    dataAcaoFimDateTimePicker.Focus();
            //    return;
            //}

            //if (dataAcaoFimDateTimePicker.Value.Date < dataDateTimePicker.Value.Date)
            //{
            //    new Notificacoes.Mensagem("Data final da ação deve ser maior ou igual a data da notificação.", Publicas.TipoMensagem.Alerta).ShowDialog();
            //    dataAcaoFimDateTimePicker.Focus();
            //    return;
            //}

            //if (dataAcaoFimDateTimePicker.Value.Date < dataAcaoDateTimePicker.Value.Date)
            //{
            //    new Notificacoes.Mensagem("Data final da ação deve ser maior ou igual a data início da ação.", Publicas.TipoMensagem.Alerta).ShowDialog();
            //    dataAcaoFimDateTimePicker.Focus();
            //    return;
            //}
        }

        private void horaAcaoDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (horaAcaoDateTimePicker.Value < DateTime.Now)
            {
                new Notificacoes.Mensagem("Hora início da ação deve ser maior ou igual a hora atual.", Publicas.TipoMensagem.Alerta).ShowDialog();
                horaAcaoDateTimePicker.Focus();
                return;
            }
        }

        private void horaAcaoFimDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (horaAcaoFimDateTimePicker.Value < DateTime.Now)
            {
                new Notificacoes.Mensagem("Hora final da ação deve ser maior ou igual a hora atual.", Publicas.TipoMensagem.Alerta).ShowDialog();
                horaAcaoFimDateTimePicker.Focus();
                return;
            }

            if (horaAcaoFimDateTimePicker.Value < horaAcaoDateTimePicker.Value)
            {
                new Notificacoes.Mensagem("Hora final da ação deve ser maior ou igual a hora início da ação.", Publicas.TipoMensagem.Alerta).ShowDialog();
                horaAcaoFimDateTimePicker.Focus();
                return;
            }
        }

        private void dataDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _notificacao = new NotificacaoDoSistemaBO().Consultar(dataDateTimePicker.Value.Date);

            if (_notificacao.Existe)
            {
                dataAcaoDateTimePicker.Value = _notificacao.DataDaAcao;
                horaAcaoDateTimePicker.Value = _notificacao.DataDaAcao;

                dataAcaoFimDateTimePicker.Value = _notificacao.DataFimDaAcao;
                horaAcaoFimDateTimePicker.Value = _notificacao.DataFimDaAcao;

                ativoCheckBox.Checked = _notificacao.Status;

                motivoTextBox.Text = _notificacao.Motivo;
                TipoAtualizacaoComboBox.SelectedIndex = (_notificacao.TipoAtualizacao == "P" ? 0 : 1);
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _notificacao.Existe;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_notificacao == null)
                _notificacao = new NotificacaoDoSistema();

            _notificacao.DataDaAcao = new DateTime(dataAcaoDateTimePicker.Value.Year, dataAcaoDateTimePicker.Value.Month, dataAcaoDateTimePicker.Value.Day, horaAcaoDateTimePicker.Value.Hour, horaAcaoDateTimePicker.Value.Minute, 0) ;
            _notificacao.DataFimDaAcao = new DateTime(dataAcaoFimDateTimePicker.Value.Year, dataAcaoFimDateTimePicker.Value.Month, dataAcaoFimDateTimePicker.Value.Day, horaAcaoFimDateTimePicker.Value.Hour, horaAcaoFimDateTimePicker.Value.Minute, 0);
            _notificacao.DataDoAviso = dataDateTimePicker.Value;
            _notificacao.Status = ativoCheckBox.Checked;
            _notificacao.Motivo = motivoTextBox.Text;
            _notificacao.IdUsuario = Publicas._usuario.Id;
            _notificacao.TipoAtualizacao = (TipoAtualizacaoComboBox.SelectedIndex == 0 ? "P" : "G");

            if (!new NotificacaoDoSistemaBO().Gravar(_notificacao))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            List<Classes.Usuario> _listaUsuarios = new List<Usuario>();
            
            try
            {
                _listaUsuarios = new UsuarioBO().ListarUsuarios(false);
            }
            catch
            {
            }

            #region Envia Email

            string[] _dadosEmail = new string[50];
            _dadosEmail[0] = dataAcaoDateTimePicker.Value.ToShortDateString() ;
            _dadosEmail[1] = horaAcaoDateTimePicker.Value.Hour.ToString("00") + "h" + horaAcaoDateTimePicker.Value.Minute.ToString("00");
            _dadosEmail[2] = horaAcaoFimDateTimePicker.Value.Hour.ToString("00") + "h" + horaAcaoFimDateTimePicker.Value.Minute.ToString("00");

            string emailDestino = "";

            foreach (var itemU in _listaUsuarios.Where(w => w.Ativo && w.Email != ""))
            {
                emailDestino = emailDestino + itemU.Email + "; ";
            }

            Classes.Publicas.EnviarEmailNotificacao(_dadosEmail, emailDestino, "Atualização do Sistema Interno", FinalizadaCheckBoxAdv1.Checked);

            #endregion


            limparButton_Click(sender, e);
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new NotificacaoDoSistemaBO().Exclui(_notificacao.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }
    }
}
