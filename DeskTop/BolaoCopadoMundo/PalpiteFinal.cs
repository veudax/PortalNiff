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
    public partial class PalpiteFinal : Form
    {
        public PalpiteFinal()
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

        Classes.BolaoTimes _selecao1;
        Classes.BolaoTimes _selecao2;
        Classes.BolaoTimes _selecao3;
        Classes.BolaoPalpiteFinalDoColaborador _palpites;
        Classes.BolaoJogos _jogos;

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Selecao1TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                Selecao2TextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                Selecao1TextBox.Focus();
            }
        }

        private void Selecao2TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                Selecao3TextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                Selecao1TextBox.Focus();
            }
        }

        private void Selecao3TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                Selecao2TextBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                Selecao3TextBox.Focus();
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

        private void Selecao1TextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            PesquisaSelecao1Button.Enabled = string.IsNullOrEmpty(Selecao1TextBox.Text.Trim());
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

        private void Selecao2TextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            PesquisaSelecao2Button.Enabled = string.IsNullOrEmpty(Selecao2TextBox.Text.Trim());
        }

        private void Selecao3TextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            PesquisaSelecao3Button.Enabled = string.IsNullOrEmpty(Selecao1TextBox.Text.Trim());
        }

        private void PesquisaSelecao1Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Selecao1TextBox.Text.Trim()))
            {
                new Pesquisas.Selecoes().ShowDialog();

                Selecao1TextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(Selecao1TextBox.Text) || Selecao1TextBox.Text == "0")
                {
                    Selecao1TextBox.Text = string.Empty;
                    Selecao1TextBox.Focus();
                    return;
                }

                Selecao1TextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void PesquisaSelecao2Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Selecao2TextBox.Text.Trim()))
            {
                new Pesquisas.Selecoes().ShowDialog();

                Selecao2TextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(Selecao2TextBox.Text) || Selecao2TextBox.Text == "0")
                {
                    Selecao2TextBox.Text = string.Empty;
                    Selecao2TextBox.Focus();
                    return;
                }

                Selecao2TextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void PesquisaSelecao3Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Selecao3TextBox.Text.Trim()))
            {
                new Pesquisas.Selecoes().ShowDialog();

                Selecao3TextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(Selecao3TextBox.Text) || Selecao3TextBox.Text == "0")
                {
                    Selecao3TextBox.Text = string.Empty;
                    Selecao3TextBox.Focus();
                    return;
                }

                Selecao3TextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_palpites == null)
                _palpites = new BolaoPalpiteFinalDoColaborador();

            _palpites.IdTimeCampeao = _selecao1.Id;
            _palpites.IdTimeVice = _selecao2.Id;
            _palpites.IdTime3Lugar = _selecao3.Id;
            _palpites.IdColaborador = Publicas._idColaborador;

            if (!new BolaoPalpiteFinalDoColaboradorBO().Gravar(_palpites))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Close();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            Selecao1TextBox.Text = string.Empty;
            Selecao2TextBox.Text = string.Empty;
            Selecao3TextBox.Text = string.Empty;
            DescricaoSelecao1TextBox.Text = string.Empty;
            DescricaoSelecao2TextBox.Text = string.Empty;
            DescricaoSelecao3TextBox.Text = string.Empty;
            Bandeira1PictureBox.Image = null;
            Bandeira2PictureBox.Image = null;
            Bandeira3PictureBox.Image = null;
            Selecao1TextBox.Focus();

            gravarButton.Enabled = false;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new BolaoPalpiteFinalDoColaboradorBO().Excluir(_palpites.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void Selecao1TextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaSelecao1Button.Enabled = string.IsNullOrEmpty(Selecao1TextBox.Text.Trim());
            gravarButton.Enabled = !string.IsNullOrEmpty(Selecao3TextBox.Text.Trim()) &&
                !string.IsNullOrEmpty(Selecao2TextBox.Text.Trim()) &&
                !string.IsNullOrEmpty(Selecao1TextBox.Text.Trim());
        }

        private void Selecao2TextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaSelecao2Button.Enabled = string.IsNullOrEmpty(Selecao2TextBox.Text.Trim());
            gravarButton.Enabled = !string.IsNullOrEmpty(Selecao3TextBox.Text.Trim()) &&
                !string.IsNullOrEmpty(Selecao2TextBox.Text.Trim()) &&
                !string.IsNullOrEmpty(Selecao1TextBox.Text.Trim());
        }

        private void Selecao3TextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaSelecao3Button.Enabled = string.IsNullOrEmpty(Selecao3TextBox.Text.Trim());
            gravarButton.Enabled = !string.IsNullOrEmpty(Selecao3TextBox.Text.Trim()) &&
                !string.IsNullOrEmpty(Selecao2TextBox.Text.Trim()) &&
                !string.IsNullOrEmpty(Selecao1TextBox.Text.Trim());
        }

        private void Selecao1TextBox_Validating(object sender, CancelEventArgs e)
        {
            Selecao1TextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Selecao1TextBox.Text = string.Empty;
                PesquisaSelecao1Button.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(Selecao1TextBox.Text.Trim()))
            {
                new Pesquisas.Selecoes().ShowDialog();

                Selecao1TextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(Selecao1TextBox.Text) || Selecao1TextBox.Text == "0")
                {
                    Selecao1TextBox.Text = string.Empty;
                    Selecao1TextBox.Focus();
                    return;
                }
            }

            _selecao1 = new BolaoTimesBO().Consultar(DateTime.Now.Year, Selecao1TextBox.Text.Trim());

            if (!_selecao1.Existe)
            {
                new Notificacoes.Mensagem("Seleção não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                Selecao1TextBox.Focus();
                return;
            }

            DescricaoSelecao1TextBox.Text = _selecao1.Nome;

            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    mStream.Write(_selecao1.Bandeira, 0, _selecao1.Bandeira.Length);
                    mStream.Seek(0, SeekOrigin.Begin);

                    Bandeira1PictureBox.Image = new Bitmap(mStream);
                }

                Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Bandeira1PictureBox.Refresh();
            }
            catch { }
        }

        private void Selecao2TextBox_Validating(object sender, CancelEventArgs e)
        {
            Selecao2TextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Selecao2TextBox.Text = string.Empty;
                PesquisaSelecao1Button.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(Selecao2TextBox.Text.Trim()))
            {
                new Pesquisas.Selecoes().ShowDialog();

                Selecao2TextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(Selecao2TextBox.Text) || Selecao2TextBox.Text == "0")
                {
                    Selecao2TextBox.Text = string.Empty;
                    Selecao2TextBox.Focus();
                    return;
                }
            }

            _selecao2 = new BolaoTimesBO().Consultar(DateTime.Now.Year, Selecao2TextBox.Text.Trim());

            if (!_selecao2.Existe)
            {
                new Notificacoes.Mensagem("Seleção não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                Selecao2TextBox.Focus();
                return;
            }

            DescricaoSelecao2TextBox.Text = _selecao2.Nome;

            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    mStream.Write(_selecao2.Bandeira, 0, _selecao2.Bandeira.Length);
                    mStream.Seek(0, SeekOrigin.Begin);

                    Bandeira2PictureBox.Image = new Bitmap(mStream);
                }

                Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Bandeira2PictureBox.Refresh();
            }
            catch { }
        }

        private void Selecao3TextBox_Validating(object sender, CancelEventArgs e)
        {
            Selecao3TextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado) 
            {
                Selecao3TextBox.Text = string.Empty;
                PesquisaSelecao1Button.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(Selecao3TextBox.Text.Trim()))
            {
                new Pesquisas.Selecoes().ShowDialog();

                Selecao3TextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(Selecao3TextBox.Text) || Selecao3TextBox.Text == "0")
                {
                    Selecao3TextBox.Text = string.Empty;
                    Selecao3TextBox.Focus();
                    return;
                }
            }

            _selecao3 = new BolaoTimesBO().Consultar(DateTime.Now.Year, Selecao3TextBox.Text.Trim());

            if (!_selecao3.Existe)
            {
                new Notificacoes.Mensagem("Seleção não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                Selecao3TextBox.Focus();
                return;
            }

            DescricaoSelecao3TextBox.Text = _selecao3.Nome;

            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    mStream.Write(_selecao3.Bandeira, 0, _selecao3.Bandeira.Length);
                    mStream.Seek(0, SeekOrigin.Begin);

                    Bandeira3PictureBox.Image = new Bitmap(mStream);
                }

                Bandeira3PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Bandeira3PictureBox.Refresh();
            }
            catch { }
        }

        private void PalpiteFinal_Shown(object sender, EventArgs e)
        {
            _palpites = new BolaoPalpiteFinalDoColaboradorBO().Consultar(Publicas._idColaborador, DateTime.Now.Year);

            _selecao1 = new BolaoTimesBO().Consultar(_palpites.IdTimeCampeao);
            Selecao1TextBox.Text = _selecao1.Sigla;
            DescricaoSelecao1TextBox.Text = _selecao1.Nome;

            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    mStream.Write(_selecao1.Bandeira, 0, _selecao1.Bandeira.Length);
                    mStream.Seek(0, SeekOrigin.Begin);

                    Bandeira1PictureBox.Image = new Bitmap(mStream);
                }

                Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Bandeira1PictureBox.Refresh();
            }
            catch { }

            _selecao2 = new BolaoTimesBO().Consultar(_palpites.IdTimeVice);
            Selecao2TextBox.Text = _selecao2.Sigla;
            DescricaoSelecao2TextBox.Text = _selecao2.Nome;

            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    mStream.Write(_selecao2.Bandeira, 0, _selecao2.Bandeira.Length);
                    mStream.Seek(0, SeekOrigin.Begin);

                    Bandeira2PictureBox.Image = new Bitmap(mStream);
                }

                Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Bandeira2PictureBox.Refresh();
            }
            catch { }

            _selecao3 = new BolaoTimesBO().Consultar(_palpites.IdTime3Lugar);
            Selecao3TextBox.Text = _selecao3.Sigla;
            DescricaoSelecao3TextBox.Text = _selecao3.Nome;

            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    mStream.Write(_selecao3.Bandeira, 0, _selecao3.Bandeira.Length);
                    mStream.Seek(0, SeekOrigin.Begin);

                    Bandeira3PictureBox.Image = new Bitmap(mStream);
                }

                Bandeira3PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Bandeira3PictureBox.Refresh();
            }
            catch { }

            _jogos = new BolaoJogosBO().Consultar(DateTime.Now.Year, "GP");

            label3.Text = "Data limite para o palpite " + _jogos.Data.AddDays(1).ToShortDateString();
            if (DateTime.Now.Date > _jogos.Data.AddDays(1).Date)
            {
                Selecao1TextBox.Enabled = false;
                Selecao2TextBox.Enabled = false;
                Selecao3TextBox.Enabled = false;
            }

        }

    }
}
