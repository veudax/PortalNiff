using Classes;
using Negocio;
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

namespace Suportte.BolaoCopadoMundo
{
    public partial class Selecoes : Form
    {
        public Selecoes()
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
            AnoTextBox.Text = DateTime.Now.Year.ToString();
        }

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

        Classes.BolaoTimes _selecoes;

        private void limparButton_Click(object sender, EventArgs e)
        {
            SiglaTextBox.Text = string.Empty;
            AnoTextBox.Text = DateTime.Now.Year.ToString();
            GrupoTextBox.Text = string.Empty;
            NomeTextBox.Text = string.Empty;
            BandeiraPictureBox.Image = null;

            SiglaTextBox.Focus();
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_selecoes == null)
                _selecoes = new BolaoTimes();

            _selecoes.Sigla = SiglaTextBox.Text;
            _selecoes.Ano = Convert.ToInt32(AnoTextBox.Text);
            _selecoes.Nome = NomeTextBox.Text;
            _selecoes.Grupo = GrupoTextBox.Text;

            try
            {

                FileStream fs = new FileStream(BandeiraPictureBox.ImageLocation, FileMode.Open, FileAccess.Read);

                // Create a byte array of file stream length
                byte[] ImageData = new byte[fs.Length];

                //Read block of bytes from stream into the byte array
                fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));
                _selecoes.Bandeira = ImageData;

                //Close the File Stream
                fs.Dispose();
                fs.Close();
            }
            catch
            {
                _selecoes.Bandeira = null;
            }

            if (!new BolaoTimesBO().Gravar(_selecoes))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new BolaoTimesBO().Excluir(_selecoes.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void PesquisaSiglaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SiglaTextBox.Text.Trim()))
            {
                new Pesquisas.Selecoes().ShowDialog();

                SiglaTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(SiglaTextBox.Text) || SiglaTextBox.Text == "0")
                {
                    SiglaTextBox.Text = string.Empty;
                    SiglaTextBox.Focus();
                    return;
                }

                SiglaTextBox_Validating(sender, new CancelEventArgs());
            }

        }

        private void SiglaTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            PesquisaSiglaButton.Enabled = string.IsNullOrEmpty(SiglaTextBox.Text.Trim());
        }

        private void SiglaTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaSiglaButton.Enabled = string.IsNullOrEmpty(SiglaTextBox.Text.Trim());
        }

        private void SiglaTextBox_Validating(object sender, CancelEventArgs e)
        {
            SiglaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                SiglaTextBox.Text = string.Empty;
                PesquisaSiglaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(SiglaTextBox.Text.Trim()))
            {
                new Pesquisas.Selecoes().ShowDialog();

                SiglaTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(SiglaTextBox.Text) || SiglaTextBox.Text == "0")
                {
                    SiglaTextBox.Text = string.Empty;
                    SiglaTextBox.Focus();
                    return;
                }
            }

            _selecoes = new BolaoTimesBO().Consultar(DateTime.Now.Year, SiglaTextBox.Text.Trim());

            if (_selecoes.Existe)
            {
                AnoTextBox.Text = DateTime.Now.Year.ToString();
                GrupoTextBox.Text = _selecoes.Grupo;
                NomeTextBox.Text = _selecoes.Nome;

                try
                {
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        mStream.Write(_selecoes.Bandeira, 0, _selecoes.Bandeira.Length);
                        mStream.Seek(0, SeekOrigin.Begin);

                        BandeiraPictureBox.Image = new Bitmap(mStream);
                    }

                    BandeiraPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    BandeiraPictureBox.Refresh();
                }
                catch { }
            }

            excluirButton.Enabled = _selecoes.Existe;
            gravarButton.Enabled = true;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BandeiraPictureBox_Click(object sender, EventArgs e)
        {
            //abri foto
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                new Notificacoes.Mensagem("Seleção cancelada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            BandeiraPictureBox.ImageLocation = openFileDialog.FileName;
        }

        private void AnoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void SiglaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
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

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                NomeTextBox.Focus();
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

        private void GrupoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                NomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SiglaTextBox.Focus();
            }
        }

        private void NomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoTextBox.Focus();
            }
        }
    }
}
