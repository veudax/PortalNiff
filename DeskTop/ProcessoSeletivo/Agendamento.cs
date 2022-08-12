using Classes;
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

namespace Suportte.ProcessoSeletivo
{
    public partial class Agendamento : Form
    {
        public Agendamento()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Vagas _vagas;
        Classes.Empresa _empresa;

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

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (!Entrevista1CheckBox.Checked && !Entrevista2CheckBox.Checked)
            {
                new Notificacoes.Mensagem("Selecione se é Primeira ou Segunda entrevista.", Classes.Publicas.TipoMensagem.Alerta).ShowDialog();
                Entrevista1CheckBox.Focus();
                return;
            }

            Classes.HistoricoDoCandidato _histo = new Classes.HistoricoDoCandidato();

            if (ReagendamentoCheckBox.Checked)
                _histo.Status = (Entrevista1CheckBox.Checked ? "8" : "9");
            else
            {
                if (CancelamentoCheckBox.Visible)
                    _histo.Status = (Entrevista1CheckBox.Checked ? "12" : "13");
                else
                    _histo.Status = (Entrevista1CheckBox.Checked ? "6" : "7");
            }

            _histo.IdVaga = Convert.ToInt32(VagasTextBox.Text);
            _histo.IdCandidato = Convert.ToInt32(candidatoTextBox.Text);

            if (DataTextBox.Enabled)
                _histo.DataEntrevista = Convert.ToDateTime(DataTextBox.Text);

            _histo.Motivo = MotivoTextBox.Text;

            List<Classes.HistoricoDoCandidato> _lista = new List<Classes.HistoricoDoCandidato>();

            _lista.Add(_histo);

            if (!new Negocio.CurriculosBO().GravarHistorico(_lista))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (EnviarEmailCheckBox.Checked && !CancelamentoCheckBox.Checked)
            {
                string[] _dadosEmail = new string[50];
                _dadosEmail[0] = DescricaoVagaTextBox.Text;
                _dadosEmail[1] = _histo.DataEntrevista.ToShortDateString();
                _dadosEmail[2] = _histo.DataEntrevista.ToShortTimeString();

                if (!SkypeRapidoButton.Checked)
                    _dadosEmail[3] = _empresa.Nome;

                _dadosEmail[4] = (DateTime.Now.TimeOfDay < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0).TimeOfDay ? "Bom dia" :
                    (DateTime.Now.TimeOfDay < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0).TimeOfDay ? "Boa Tarde" : "Boa Noite"));

                _dadosEmail[5] = nomeTextBox.Text.Split(' ')[0];

                if (!SkypeRapidoButton.Checked)
                {
                    _dadosEmail[10] = "no seguinte endereço:";
                    _dadosEmail[6] = _vagas.EnderecoEntevista;
                }
                else
                {
                    _dadosEmail[6] = "Nossa entrevista será efetuada através do skype </br>" + SkypeTextBox.Text;
                    _dadosEmail[10] = ".";
                }

                _dadosEmail[7] = Publicas._usuario.Nome;
                _dadosEmail[8] = (_vagas.Confidencial ? "Lembrando que a vaga é confidencial." : "") ;
                _dadosEmail[9] = _vagas.InformacoesGerais + "</br>"; 

                Publicas.EnviarEmailProcessoSeletivo(_dadosEmail, Publicas._usuario.Email, EmailTextBox.Text, "", true);
            }

            limparButton_Click(sender, e);
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EnviarEmailCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (SkypeTextBox.Enabled)
                    SkypeTextBox.Focus();
                else
                    MotivoTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PresencialRadioButton.Focus();
            }
        }

        private void DataTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (ReagendamentoCheckBox.Visible)
                    ReagendamentoCheckBox.Focus();
                else
                    CancelamentoCheckBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DataTextBox.Focus();
            }
        }

        private void ReagendamentoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                Entrevista1CheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (DataTextBox.Enabled)
                    DataTextBox.Focus();
                else
                    ReagendamentoCheckBox.Focus();
            }
        }

        private void Entrevista1CheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                Entrevista2CheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (ReagendamentoCheckBox.Visible)
                    ReagendamentoCheckBox.Focus();
                else
                    CancelamentoCheckBox.Focus();
            }
        }

        private void Entrevista2CheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PresencialRadioButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                Entrevista1CheckBox.Focus();
            }
        }

        private void CancelamentoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                Entrevista1CheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (DataTextBox.Enabled)
                    DataTextBox.Focus();
                else
                    EnviarEmailCheckBox.Focus();
            }
        }

        private void MotivoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (SkypeTextBox.Enabled)
                    SkypeTextBox.Focus();
                else
                    EnviarEmailCheckBox.Focus();
            }
        }

        private void SkypeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                MotivoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EnviarEmailCheckBox.Focus();
            }
        }

        private void PresencialRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SkypeRapidoButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                Entrevista2CheckBox.Focus();
            }
        }

        private void SkypeRapidoButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EnviarEmailCheckBox.Focus();    
            
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PresencialRadioButton.Focus();
            }
        }

        private void PresencialRadioButton_CheckChanged(object sender, EventArgs e)
        {
            SkypeTextBox.Enabled = SkypeRapidoButton.Checked;
        }

        private void Agendamento_Shown(object sender, EventArgs e)
        {
            if (VagasTextBox.Text != "")
            {
                _vagas = new Negocio.CurriculosBO().ConsultarVaga(Convert.ToInt32(VagasTextBox.Text.Trim()));

                if (!_vagas.Existe)
                {
                    new Notificacoes.Mensagem("Vaga não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    VagasTextBox.Focus();
                    return;
                }

                if (_vagas.Status != "A")
                    new Notificacoes.Mensagem("Vaga encerrada ou congelada.", Publicas.TipoMensagem.Alerta).ShowDialog();

                DescricaoVagaTextBox.Text = _vagas.Descricao;

                _empresa = new Negocio.EmpresaBO().Consultar(_vagas.IdEmpresaEntrevista);
            }
        }

        private void DataTextBox_Validating(object sender, CancelEventArgs e)
        {
            DataTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            try
            {
                Convert.ToDateTime(DataTextBox.Text);
            }
            catch
            {
                new Notificacoes.Mensagem("Data inválida." + Environment.NewLine + 
                    "Verifique se informou corretamente a data e a hora da entrevista.", Publicas.TipoMensagem.Alerta).ShowDialog();
                DataTextBox.Focus();
                return;
            }
        }

        private void DataTextBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void SkypeTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void SkypeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }
    }
}
