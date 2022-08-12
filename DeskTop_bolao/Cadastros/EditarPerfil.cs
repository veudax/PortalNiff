using Classes;
using Negocio;
using Suportte.Notificacoes;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.Cadastros
{
    public partial class EditarPerfil : Form
    {
        public EditarPerfil()
        {
            InitializeComponent();

            dataNascimentoDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataNascimentoDateTimePicker.BackColor = usuarioTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Departamento _departamento;

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
            cpfMaskedEditBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            dataNascimentoDateTimePicker.Value = DateTime.Now.Date;
            cargoTextBox.Text = string.Empty;
            telefoneTextBox.Text = string.Empty;
            ramalTextBox.Text = string.Empty;
            setorTextBox.Text = string.Empty;
            descricaoDeptoTextBox.Text = string.Empty;
            fotoPictureBox.Image = null;
            emailTextBox.Text = string.Empty;
            cpfMaskedEditBox.Focus();
        }

        private void EditarPerfil_Shown(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Arquivos de imagem (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            usuarioTextBox.Text = Publicas._usuario.UsuarioAcesso;
            
            if (Publicas._usuario.CPF != 0)
                cpfMaskedEditBox.Text = Publicas._usuario.CPF.ToString();

            nomeTextBox.Text = Publicas._usuario.Nome;
            dataNascimentoDateTimePicker.Value = Publicas._usuario.DataNascimento;
            cargoTextBox.Text = Publicas._usuario.Cargo;

            telefoneTextBox.Text = Publicas._usuario.Telefone.ToString();
            ramalTextBox.Text = Publicas._usuario.Ramal.ToString();
            emailTextBox.Text = Publicas._usuario.Email;
            nomeMaquinaTextBox.Text = Publicas._usuario.NomeMaquina;

            setorTextBox.Text = Publicas._usuario.IdDepartamento.ToString();
            notificacaoCorridaCheckBox.Checked = Publicas._usuario.NaoNotificaCorridas;
            aniversariantesCheckBox.Checked = Publicas._usuario.AniversariantesApenasDaEmpresa;
            ParticipaBolaoCopaCheckBox.Checked = Publicas._usuario.ParticipaBolaoCopa;

            if (!string.IsNullOrEmpty(setorTextBox.Text.Trim()))
            {
                _departamento = new DepartamentoBO().Consultar(Convert.ToInt32(setorTextBox.Text));

                if (_departamento.Existe)
                    descricaoDeptoTextBox.Text = _departamento.Descricao;
            }
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    mStream.Write(Publicas._usuario.Foto, 0, Publicas._usuario.Foto.Length);
                    mStream.Seek(0, SeekOrigin.Begin);

                    fotoPictureBox.Image = new Bitmap(mStream);
                }

                fotoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                fotoPictureBox.Refresh();
            }
            catch { }
        }

        private void fotoPictureBox_Click(object sender, EventArgs e)
        {
            
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(cpfMaskedEditBox.ClipText.Trim()))
                Publicas._usuario.CPF = Convert.ToDecimal(cpfMaskedEditBox.ClipText);

            Publicas._usuario.Nome = nomeTextBox.Text;
            Publicas._usuario.DataNascimento = dataNascimentoDateTimePicker.Value;
            Publicas._usuario.Cargo = cargoTextBox.Text;

            if (!string.IsNullOrEmpty(telefoneTextBox.ClipText.Trim()))
                Publicas._usuario.Telefone = Convert.ToInt32(telefoneTextBox.ClipText);

            if (!string.IsNullOrEmpty(ramalTextBox.Text.Trim()))
             Publicas._usuario.Ramal = Convert.ToInt32(ramalTextBox.Text);

            if (!string.IsNullOrEmpty(setorTextBox.Text.Trim()))
                Publicas._usuario.IdDepartamento = Convert.ToInt32(setorTextBox.Text);
            
            Publicas._usuario.Email = emailTextBox.Text.Trim();
            Publicas._usuario.NaoNotificaCorridas = notificacaoCorridaCheckBox.Checked;
            Publicas._usuario.AniversariantesApenasDaEmpresa = aniversariantesCheckBox.Checked;
            Publicas._usuario.NomeMaquina = nomeMaquinaTextBox.Text;
            Publicas._usuario.ParticipaBolaoCopa = ParticipaBolaoCopaCheckBox.Checked;

            try
            {

                FileStream fs = new FileStream(fotoPictureBox.ImageLocation, FileMode.Open, FileAccess.Read);

                // Create a byte array of file stream length
                byte[] ImageData = new byte[fs.Length];

                //Read block of bytes from stream into the byte array
                fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));
                Publicas._usuario.Foto = ImageData;

                //Close the File Stream
                fs.Dispose();
                fs.Close();
            }
            catch
            {
                Publicas._usuario.Foto = null;
            }

            if (!new UsuarioBO().Gravar(Publicas._usuario))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Close();
        }

        private void cpfMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void telefoneTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void dataNascimentoDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        #region MyRegion
        private void cpfMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataNascimentoDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cpfMaskedEditBox.Focus();
            }
        }

        private void dataNascimentoDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                telefoneTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cpfMaskedEditBox.Focus();
            }
        }

        private void telefoneTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ramalTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataNascimentoDateTimePicker.Focus();
            }
        }

        private void ramalTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cargoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                telefoneTextBox.Focus();
            }
        }

        private void cargoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ramalTextBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                setorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cargoTextBox.Focus();
            }
        }

        private void setorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                emailTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }


        #endregion

        private void telefoneTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void cpfMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                cpfMaskedEditBox.Focus();
                return;
            }

            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;
        }

        private void dataNascimentoDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
        }

        private void setorTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
            
            if (String.IsNullOrEmpty(setorTextBox.Text))
            {
                new Pesquisas.Departamentos().ShowDialog();

                setorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (setorTextBox.Text == "" || setorTextBox.Text == "0")
                {
                    setorTextBox.Text = string.Empty;
                    setorTextBox.Focus();
                    return;
                }
            }

            _departamento = new DepartamentoBO().Consultar(Convert.ToInt32(setorTextBox.Text));

            if (_departamento == null || !_departamento.Existe)
            {
                new Notificacoes.Mensagem("Departamento não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                setorTextBox.Focus();
                return;
            }

            if (!_departamento.Ativo)
            {
                new Notificacoes.Mensagem("Departamento inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                setorTextBox.Focus();
                return;
            }

            setorTextBox.Text = _departamento.Id.ToString();
            descricaoDeptoTextBox.Text = _departamento.Descricao;

        }

        private void limparFotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fotoPictureBox.Image = null;
        }

        private void trocarFotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //abri foto
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                new Notificacoes.Mensagem("Seleção cancelada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            fotoPictureBox.ImageLocation = openFileDialog.FileName;
        }


        private void gravarButton_Enter(object sender, EventArgs e)
        {
            gravarButton.BackColor = Publicas._botaoFocado;
            gravarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void limparButton_Enter(object sender, EventArgs e)
        {
            testeEmailButton.BackColor = Publicas._botaoFocado;
            testeEmailButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            testeEmailButton.BackColor = Publicas._botao;
            testeEmailButton.ForeColor = Publicas._fonteBotao;
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
                emailTextBox.Focus();
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

        private void emailTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void emailTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void emailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                setorTextBox.Focus();
            }
        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        {
            if (!Publicas.EnviarEmailTeste("informatica@niff.com.br", emailTextBox.Text))
            {
                new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            new Notificacoes.Mensagem("Processo finalizado.", Publicas.TipoMensagem.Sucesso).ShowDialog();
        }

        private void setorTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaDepartamentoButton.Enabled = string.IsNullOrEmpty(setorTextBox.Text.Trim());
        }

        private void pesquisaDepartamentoButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(setorTextBox.Text))
            {
                new Pesquisas.Departamentos().ShowDialog();

                setorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (setorTextBox.Text == "" || setorTextBox.Text == "0")
                {
                    setorTextBox.Text = string.Empty;
                    setorTextBox.Focus();
                    return;
                }

                setorTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void setorTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
