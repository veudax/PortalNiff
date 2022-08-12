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
    public partial class Jogos : Form
    {
        public Jogos()
        {
            InitializeComponent();

            DataJogoMaskedEditBox.BorderColor = Publicas._bordaSaida;
            DataJogoMaskedEditBox.BackColor = DescricaoSelecao1TextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.BolaoJogos _jogos;
        Classes.BolaoTimes _selecao1;
        Classes.BolaoTimes _selecao2;

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

        private void DataJogoMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                Selecao1TextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DataJogoMaskedEditBox.Focus();
            }
        }

        private void selecao1TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                Selecao2TextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DataJogoMaskedEditBox.Focus();
            }
        }

        private void Selecao2TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                DataLimiteMaskedEdit.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                Selecao1TextBox.Focus();
            }
        }

        private void DataLimiteMaskedEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                LocalTextBox.Focus();
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
                LocalTextBox.Focus();
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

        private void LocalTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DataLimiteMaskedEdit.Focus();
            }
        }

        private void Selecao1TextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            PesquisaSelecao1Button.Enabled = string.IsNullOrEmpty(Selecao1TextBox.Text.Trim());
        }

        private void DataJogoMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim());
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

        private void Selecao2TextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            PesquisaSelecao1Button.Enabled = string.IsNullOrEmpty(Selecao1TextBox.Text.Trim());
            PesquisaSelecao2Button.Enabled = string.IsNullOrEmpty(Selecao2TextBox.Text.Trim());
        }

        private void pesquisaReferenciaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim()))
            {
                new Pesquisas.Jogos().ShowDialog();

                DataJogoMaskedEditBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim()) || DataJogoMaskedEditBox.ClipText.Trim() == "0")
                {
                    DataJogoMaskedEditBox.Text = string.Empty;
                    DataJogoMaskedEditBox.Focus();
                    return;
                }

                DataJogoMaskedEditBox_Validating(sender, new CancelEventArgs());
            }
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

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_jogos == null)
                _jogos = new BolaoJogos();

            _jogos.IdTime1 = _selecao1.Id;
            _jogos.IdTime2 = _selecao2.Id;
            _jogos.LimitePalpite = Convert.ToDateTime(DataLimiteMaskedEdit.Text.Trim());
            _jogos.Data = Convert.ToDateTime(DataJogoMaskedEditBox.Text.Trim());
            _jogos.Localizacao = LocalTextBox.Text;
            _jogos.Fase = (faseComboBox.SelectedIndex == 0 ? "GP" :
                (faseComboBox.SelectedIndex == 1 ? "8F" :
                (faseComboBox.SelectedIndex == 2 ? "4F" :
                (faseComboBox.SelectedIndex == 3 ? "SF" :
                (faseComboBox.SelectedIndex == 4 ? "3L" : "FN")))));

            if (!new BolaoJogosBO().Gravar(_jogos))
            {
                new Notificacoes.Mensagem("Problemas durante a gravar." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            DataLimiteMaskedEdit.Text = string.Empty;
            DataJogoMaskedEditBox.Text = string.Empty;
            Selecao1TextBox.Text = string.Empty;
            Selecao2TextBox.Text = string.Empty;
            DescricaoSelecao1TextBox.Text = string.Empty;
            DescricaoSelecao2TextBoxExt.Text = string.Empty;
            LocalTextBox.Text = string.Empty;
            Bandeira1PictureBox.Image = null;
            Bandeira2PictureBox.Image = null;
            DataJogoMaskedEditBox.Focus();

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new BolaoJogosBO().Excluir(_jogos.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void DataJogoMaskedEditBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim());
        }

        private void Selecao1TextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaSelecao1Button.Enabled = string.IsNullOrEmpty(Selecao1TextBox.Text.Trim());
        }

        private void Selecao2TextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaSelecao2Button.Enabled = string.IsNullOrEmpty(Selecao2TextBox.Text.Trim());
        }

        private void DataJogoMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            DataJogoMaskedEditBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                DataJogoMaskedEditBox.Text = string.Empty;
                pesquisaReferenciaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            Publicas._dataPesquisa = DateTime.MinValue;
            Publicas._codigoRetornoPesquisa = "";

            if (string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim()))
            {
                new Pesquisas.Jogos().ShowDialog();

                DataJogoMaskedEditBox.Text = Publicas._codigoRetornoPesquisa;
                //Ver que esta dando erro aqui
                if (string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim()) || DataJogoMaskedEditBox.ClipText.Trim() == "0")
                {
                    DataJogoMaskedEditBox.Text = string.Empty;
                    DataJogoMaskedEditBox.Focus();
                    return;
                }
            }

            List<BolaoJogos> _lista = new BolaoJogosBO().Listar(Convert.ToDateTime(DataJogoMaskedEditBox.Text.Trim()));

            if (_lista.Count() > 1)
            {
                Publicas._dataPesquisa = Convert.ToDateTime(DataJogoMaskedEditBox.Text.Trim());

                new Pesquisas.Jogos().ShowDialog();

                if (Publicas._idRetornoPesquisa == 0)
                {
                    DataJogoMaskedEditBox.Text = string.Empty;
                    DataJogoMaskedEditBox.Focus();
                    return;
                }

                _jogos = new BolaoJogosBO().Consultar(Publicas._idRetornoPesquisa);
                Publicas._codigoRetornoPesquisa = _jogos.Data.ToString();

                DataJogoMaskedEditBox.Text = _jogos.Data.ToString();

                if (string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim()) || DataJogoMaskedEditBox.ClipText.Trim() == "0")
                {
                    DataJogoMaskedEditBox.Text = string.Empty;
                    DataJogoMaskedEditBox.Focus();
                    return;
                }

                PopulaCampos();
                return;
            }

            _jogos = new BolaoJogosBO().Consultar(Convert.ToDateTime(DataJogoMaskedEditBox.Text.Trim()));
            PopulaCampos();
        }

        private void PopulaCampos()
        {
            
            DataLimiteMaskedEdit.Text = Convert.ToDateTime(DataJogoMaskedEditBox.Text.Trim()).AddHours(-1).ToString();

            if (_jogos.Existe)
            {
                if (Publicas._codigoRetornoPesquisa == "")
                {
                    if (new Notificacoes.Mensagem("Ja existe um jogo nessa data e horario." + Environment.NewLine +
                        "Deseja incluir um novo jogo nesse horario ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                    {
                        _jogos = null;
                        gravarButton.Enabled = true;
                        return;
                    }
                }

                _selecao1 = new BolaoTimesBO().Consultar(_jogos.IdTime1);
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

                _selecao2 = new BolaoTimesBO().Consultar(_jogos.IdTime2);
                Selecao2TextBox.Text = _selecao2.Sigla;
                DescricaoSelecao2TextBoxExt.Text = _selecao2.Nome;

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

                LocalTextBox.Text = _jogos.Localizacao;
                DataLimiteMaskedEdit.Text = _jogos.LimitePalpite.ToString();

                faseComboBox.SelectedIndex = (_jogos.Fase == "GP" ? 0 :
                (_jogos.Fase == "8F" ? 1 :
                (_jogos.Fase == "4F" ? 2 :
                (_jogos.Fase == "SF" ? 3 :
                (_jogos.Fase == "3L" ? 4 : 5)))));
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _jogos.Existe;
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

            DescricaoSelecao2TextBoxExt.Text = _selecao2.Nome;

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

        private void DataLimiteMaskedEdit_Validating(object sender, CancelEventArgs e)
        {
            DataLimiteMaskedEdit.BorderColor = Publicas._bordaSaida;
        }

        private void LocalTextBox_Validating(object sender, CancelEventArgs e)
        {
            LocalTextBox.BorderColor = Publicas._bordaSaida;
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

        private void Jogos_Shown(object sender, EventArgs e)
        {
            faseComboBox.Items.AddRange(new object[] { "Fase de Grupos", "Oitavas de final", "Quartas de Final", "SemiFinais", "Decisão 3º Lugar", "Final"});
            faseComboBox.SelectedIndex = 0;
        }
    }
}
