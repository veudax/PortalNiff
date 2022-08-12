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

namespace Suportte.Biblioteca
{
    public partial class Livros : Form
    {
        public Livros()
        {
            InitializeComponent();

            dataDateTimePicker.BackColor = codigoTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                    fotoPictureBox.BackColor = Publicas._fundo;
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.CategoriaLivros> _categorias;
        Classes.Livros _livros;
        Classes.Colaboradores _colaborador;
        int IdCategoria;

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

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void tipoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CategoriaComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void CategoriaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoComboBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ColaboradorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CategoriaComboBox.Focus();
            }
        }


        private void ColaboradorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ConservacaoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void ConservacaoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                AtivoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ColaboradorTextBox.Focus();
            }
        }

        private void dataDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                FisicoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AtivoCheckBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SinopseTextBox.Focus();
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

        private void nomeTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void ColaboradorTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            PesquisaColaboradorButton.Enabled = string.IsNullOrEmpty(ColaboradorTextBox.Text.Trim());
        }

        private void tipoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void dataDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
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

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a código.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a descrição.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }
            

            foreach (var item in _categorias.Where(w => w.Descricao == CategoriaComboBox.Text))
            {
                IdCategoria = item.Id;
                break;
            }

            _livros.Id = Convert.ToInt32(codigoTextBox.Text);
            _livros.Nome = nomeTextBox.Text;
            _livros.IdColaborador = _colaborador.Id;
            _livros.IdCategoria = IdCategoria;
            _livros.DataDevolucao = dataDateTimePicker.Value;
            _livros.TipoCessao = tipoComboBox.Text.Substring(0, 1);
            _livros.Conservacao = ConservacaoComboBox.Text.Substring(0, 1);
            _livros.Sinopse = SinopseTextBox.Text;
            _livros.Ativo = AtivoCheckBox.Checked;
            _livros.Fisico = FisicoCheckBox.Checked;
            _livros.EBook = EbookCheckBox.Checked;
            _livros.LocalArmazenamento = LocalTextBox.Text;

            if (ArquivoTextBox.Text.Trim() != "")
                _livros.NomeArquivo = Path.GetFileName(ArquivoTextBox.Text);

            try
            {

                FileStream fs = new FileStream(fotoPictureBox.ImageLocation, FileMode.Open, FileAccess.Read);

                // Create a byte array of file stream length
                byte[] ImageData = new byte[fs.Length];

                //Read block of bytes from stream into the byte array
                fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));
                _livros.Imagem = ImageData;

                //Close the File Stream
                fs.Dispose();
                fs.Close();
            }
            catch
            {
                
            }

            if (ArquivoTextBox.Text.Trim() != "" && LocalTextBox.Text.Trim() == "")
            {
                try
                {

                    FileStream fs = new FileStream(ArquivoTextBox.Text.Trim(), FileMode.Open, FileAccess.Read);

                    // Create a byte array of file stream length
                    byte[] ImageData = new byte[fs.Length];

                    //Read block of bytes from stream into the byte array
                    fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));
                    _livros.Arquivo= ImageData;

                    //Close the File Stream
                    fs.Dispose();
                    fs.Close();
                }
                catch
                {

                }
            }

            if (!new LivrosBO().Gravar(_livros))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            ColaboradorTextBox.Text = string.Empty;
            NomeColaboradorTextBox.Text = string.Empty;
            SinopseTextBox.Text = string.Empty;
            tipoComboBox.SelectedIndex = 0;
            ConservacaoComboBox.SelectedIndex = 0;
            dataDateTimePicker.Value = DateTime.MinValue;
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            fotoPictureBox.Image = null;
            fotoPictureBox.ImageLocation = "";
            AtivoCheckBox.Checked = false;
            ArquivoTextBox.Text = string.Empty;
            LocalTextBox.Text = string.Empty;

            codigoTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new LivrosBO().Excluir(_livros.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void pesquisaCategoriaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()))
            {
                new Pesquisas.Livros().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            codigoTextBox_Validating(sender, new CancelEventArgs());
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new LivrosBO().Proximo().ToString();
            tipoComboBox.Focus();
        }

        private void PesquisaColaboradorButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()))
            {
                new Pesquisas.Colaborador().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }

                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            codigoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                codigoTextBox.Text = string.Empty;
                pesquisaCategoriaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()))
            {
                new Pesquisas.Livros().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _livros = new LivrosBO().Consultar(Convert.ToInt32(codigoTextBox.Text.Trim()));

            if (_livros.Existe)
            {
                nomeTextBox.Text = _livros.Nome;

                foreach (var item in _categorias.Where(w => w.Id == _livros.IdCategoria))
                {
                    for (int i = 0; i < CategoriaComboBox.Items.Count; i++)
                    {
                        CategoriaComboBox.SelectedIndex = i;
                        if (CategoriaComboBox.Text == item.Descricao)
                            break;
                    }
                }
                ColaboradorTextBox.Text = _livros.IdColaborador.ToString();
                ColaboradorTextBox_Validating(sender, e);

                dataDateTimePicker.Value = _livros.DataDevolucao;
                tipoComboBox.SelectedIndex = (_livros.TipoCessao == "T" ? 0 : 1);
                ConservacaoComboBox.SelectedIndex = (_livros.Conservacao == "R" ? 0 :
                    (_livros.Conservacao == "B" ? 1 : 2));
                SinopseTextBox.Text = _livros.Sinopse;
                AtivoCheckBox.Checked = _livros.Ativo;

                FisicoCheckBox.Checked = _livros.Fisico;
                EbookCheckBox.Checked = _livros.EBook;
                LocalTextBox.Text = _livros.LocalArmazenamento;

                try
                {
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        mStream.Write(_livros.Imagem, 0, _livros.Imagem.Length);
                        mStream.Seek(0, SeekOrigin.Begin);

                        fotoPictureBox.Image = new Bitmap(mStream);
                    }

                    fotoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    fotoPictureBox.Refresh();
                }
                catch { }
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _livros.Existe;
        }

        private void ColaboradorTextBox_Validating(object sender, CancelEventArgs e)
        {
            ColaboradorTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                ColaboradorTextBox.Text = string.Empty;
                PesquisaColaboradorButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(ColaboradorTextBox.Text.Trim()))
            {
                new Pesquisas.Colaborador().ShowDialog();

                ColaboradorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(ColaboradorTextBox.Text) || ColaboradorTextBox.Text == "0")
                {
                    ColaboradorTextBox.Text = string.Empty;
                    ColaboradorTextBox.Focus();
                    return;
                }
            }

            _colaborador = new ColaboradoresBO().ConsultaColaborador(Convert.ToInt32(ColaboradorTextBox.Text.Trim()));

            if (!_colaborador.Existe)
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ColaboradorTextBox.Focus();
                return;
            }

            NomeColaboradorTextBox.Text = _colaborador.Nome;
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            nomeTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void tipoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
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
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void ColaboradorTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaColaboradorButton.Enabled = string.IsNullOrEmpty(ColaboradorTextBox.Text.Trim());
        }

        private void Livros_Shown(object sender, EventArgs e)
        {
            _categorias = new CategoriaLivrosBO().Listar();

            foreach (var item in _categorias.Where(w => w.Ativo).OrderBy(o => o.Id))
            {
                CategoriaComboBox.Items.Add(item.Descricao);
            }

            tipoComboBox.Items.AddRange(new object[] { "Temporário", "Permanente" });
            ConservacaoComboBox.Items.AddRange(new object[] { "Ruim", "Bom", "Ótima" });
            CategoriaComboBox.SelectedIndex = 0;
            tipoComboBox.SelectedIndex = 0;
            ConservacaoComboBox.SelectedIndex = 0;
        }

        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '+')
            {
                codigoTextBox.Text = string.Empty;
                proximoButton_Click(sender, e);
            }
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

        private void SinopseTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataDateTimePicker.Focus();
            }
        }

        private void AtivoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (dataDateTimePicker.Visible)
                    dataDateTimePicker.Focus();
                else
                    FisicoCheckBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ConservacaoComboBox.Focus();
            }
        }

        private void FisicoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EbookCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (dataDateTimePicker.Visible)
                    dataDateTimePicker.Focus();
                else
                    AtivoCheckBox.Focus();
            }
        }

        private void EbookRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ArquivoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FisicoCheckBox.Focus();
            }
        }

        private void AudioBookRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ArquivoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                LocalTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EbookCheckBox.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                LocalTextBox.Focus();
            }
        }

        private void LocalTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SinopseTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ArquivoTextBox.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                SinopseTextBox.Focus();
            }
        }

        private void ArquivoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ArquivoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                return;
            }

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                new Notificacoes.Mensagem("Seleção cancelada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            ArquivoTextBox.Text = openFileDialog.FileName;
        }

        private void LocalTextBox_Validating(object sender, CancelEventArgs e)
        {
            LocalTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                return;
            }

            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                new Notificacoes.Mensagem("Seleção cancelada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            LocalTextBox.Text = folderBrowserDialog1.SelectedPath;
        }

        private void VerEBookButton_Click(object sender, EventArgs e)
        {
            string _nomeArquivo = "";
            Publicas._chamouPelaTeladeLivros = true;

            if (ArquivoTextBox.Text.Trim() == "" && LocalTextBox.Text.Trim() == "")
            {
                try
                {
                    if (!System.IO.Directory.Exists("C:\\Temp\\"))
                        System.IO.Directory.CreateDirectory("C:\\Temp\\");

                    using (FileStream fs = new FileStream
                                        ("C:\\Temp\\" + _livros.NomeArquivo, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(_livros.Arquivo, 0, _livros.Arquivo.Length);
                    }
                }
                catch { }

                _nomeArquivo = "C:\\Temp\\" + _livros.NomeArquivo;
            }
            else
            {
                if (ArquivoTextBox.Text.Trim() != "")
                    _nomeArquivo = ArquivoTextBox.Text;

                if (LocalTextBox.Text.Trim() != "")
                    _nomeArquivo = LocalTextBox.Text;
            }

            Leitor _tela = new Leitor();
            _tela._arquivo = _nomeArquivo;
            _tela.Size = new Size(Publicas._widthTelaInicial - 5, Publicas._heigthTelaInicial - (Publicas._heigthTituloTelaInicial + Publicas._heigthBarraTelaInicial + 2));
            _tela.Show();
        }

        private void tipoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Visible = tipoComboBox.SelectedIndex == 0;
            dataDateTimePicker.Visible = tipoComboBox.SelectedIndex == 0;
        }

        private void EbookCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            VerEBookButton.Enabled = EbookCheckBox.Checked;
        }
    }
}
