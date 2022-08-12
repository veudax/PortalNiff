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

        Classes.Departamento _departamento;
        Classes.Ramais _ramais;
        Classes.Ramais _ramaisAntes;
        Classes.Telefone _telefone;
        List<Classes.RamaisAssociadosAoColaborador> _listaColaboradores;

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
            GrupoRamalTextBox.Text = string.Empty;
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
            
            cpfMaskedEditBox.Text = Publicas._usuario.CPF.ToString();

            nomeTextBox.Text = Publicas._usuario.Nome;
            dataNascimentoDateTimePicker.Value = Publicas._usuario.DataNascimento;
            
            telefoneTextBox.Text = Publicas._usuario.Telefone.ToString();
            ramalTextBox.Text = Publicas._usuario.Ramal.ToString();

            if (telefoneTextBox.ClipText.Trim() != "")
                telefoneTextBox_Validating_1(sender, new CancelEventArgs());

            if (ramalTextBox.Text != "")
            {
                ramalTextBox_Validating(sender, new CancelEventArgs());
                _ramaisAntes = new RamaisBO().ConsultarRamal(_telefone.Id, Convert.ToInt32(ramalTextBox.Text.Trim()));
            }

            emailTextBox.Text = Publicas._usuario.Email;
            nomeMaquinaTextBox.Text = Publicas._usuario.NomeMaquina;

            setorTextBox.Text = Publicas._usuario.IdDepartamento.ToString();
            notificacaoCorridaCheckBox.Checked = Publicas._usuario.NaoNotificaCorridas;
            aniversariantesCheckBox.Checked = Publicas._usuario.AniversariantesApenasDaEmpresa;
            ParticipaBolaoCopaCheckBox.Checked = Publicas._usuario.ParticipaBolaoCopa;
            AtendidosPorMimCheckBoxAdv.Checked = Publicas._usuario.PodeFinalizarChamado;
            AtendidosPorMimCheckBoxAdv.Enabled = Publicas._usuario.Tipo == Publicas.TipoUsuario.Atendente;
            AssinaturaTextBox.Text = Publicas._usuario.AssinaturaChamado;

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
            string descricao = "";

            if (!string.IsNullOrEmpty(cpfMaskedEditBox.ClipText.Trim()))
            {
                descricao = descricao + (Publicas._usuario.CPF.ToString() == cpfMaskedEditBox.ClipText ? "" :
                    " [CPF] de " + Publicas._usuario.CPF.ToString() + " para " + cpfMaskedEditBox.ClipText + "");
                Publicas._usuario.CPF = cpfMaskedEditBox.ClipText;
            }

            if (!string.IsNullOrEmpty(telefoneTextBox.ClipText.Trim()))
            {
                descricao = descricao + (Publicas._usuario.Telefone.ToString() == telefoneTextBox.ClipText ? "" :
                    " [Telefone] de " + Publicas._usuario.Telefone.ToString() + " para " + telefoneTextBox.ClipText + "");
                Publicas._usuario.Telefone = Convert.ToDecimal(telefoneTextBox.ClipText);
            }

            if (!string.IsNullOrEmpty(ramalTextBox.Text.Trim()))
            {
                descricao = descricao + (Publicas._usuario.Ramal.ToString() == ramalTextBox.Text ? "" :
                   " [Ramal] de " + Publicas._usuario.Ramal.ToString() + " para " + ramalTextBox.Text + "");
                Publicas._usuario.Ramal = Convert.ToInt32(ramalTextBox.Text);

            }

            if (!string.IsNullOrEmpty(setorTextBox.Text.Trim()))
            {
                descricao = descricao + (Publicas._usuario.IdDepartamento.ToString() == setorTextBox.Text ? "" :
                   " [IdDepartamento] de " + Publicas._usuario.IdDepartamento.ToString() + " para " + setorTextBox.Text + "");
                Publicas._usuario.IdDepartamento = Convert.ToInt32(setorTextBox.Text);
            }

            descricao = descricao + (Publicas._usuario.Nome == nomeTextBox.Text ? "" :
                    " [Nome] de " + Publicas._usuario.Nome + " para " + nomeTextBox.Text + "") +
                                    (Publicas._usuario.DataNascimento.ToShortDateString() == dataNascimentoDateTimePicker.Value.ToShortDateString() ? "" :
                    " [DataNascimento] de " + Publicas._usuario.DataNascimento.ToShortDateString() + " para " + dataNascimentoDateTimePicker.Value.ToShortDateString() + "") +
                                     (Publicas._usuario.Email == emailTextBox.Text ? "" :
                    " [Email] de " + Publicas._usuario.Email + " para " + emailTextBox.Text + "") +
                                     (Publicas._usuario.NaoNotificaCorridas == notificacaoCorridaCheckBox.Checked ? "" :
                    " [NaoNotificaCorridas] de " + Publicas._usuario.NaoNotificaCorridas + " para " + notificacaoCorridaCheckBox.Checked + "") +
                                     (Publicas._usuario.AniversariantesApenasDaEmpresa == aniversariantesCheckBox.Checked ? "" :
                    " [AniversariantesApenasDaEmpresa] de " + Publicas._usuario.AniversariantesApenasDaEmpresa + " para " + aniversariantesCheckBox.Checked + "") +
                                     (Publicas._usuario.NomeMaquina == nomeMaquinaTextBox.Text ? "" :
                    " [NomeMaquina] de " + Publicas._usuario.NomeMaquina + " para " + nomeMaquinaTextBox.Text + "") +
                                     (Publicas._usuario.ParticipaBolaoCopa == ParticipaBolaoCopaCheckBox.Checked ? "" :
                    " [PodeFinalizarChamado] de " + Publicas._usuario.ParticipaBolaoCopa + " para " + ParticipaBolaoCopaCheckBox.Checked + "") +
                                     (Publicas._usuario.PodeFinalizarChamado == AtendidosPorMimCheckBoxAdv.Checked ? "" :
                    " [PodeFinalizarChamado] de " + Publicas._usuario.PodeFinalizarChamado + " para " + AtendidosPorMimCheckBoxAdv.Checked + "")  +
                                     (Publicas._usuario.AssinaturaChamado == AssinaturaTextBox.Text ? "" :
                    " [AssinaturaChamado] de " + Publicas._usuario.AssinaturaChamado + " para " + AssinaturaTextBox.Text + "");

            Publicas._usuario.Nome = nomeTextBox.Text;
            Publicas._usuario.DataNascimento = dataNascimentoDateTimePicker.Value;
            
            Publicas._usuario.Email = emailTextBox.Text.Trim();
            Publicas._usuario.NaoNotificaCorridas = notificacaoCorridaCheckBox.Checked;
            Publicas._usuario.AniversariantesApenasDaEmpresa = aniversariantesCheckBox.Checked;
            Publicas._usuario.NomeMaquina = nomeMaquinaTextBox.Text;
            Publicas._usuario.ParticipaBolaoCopa = ParticipaBolaoCopaCheckBox.Checked;
            Publicas._usuario.PodeFinalizarChamado = AtendidosPorMimCheckBoxAdv.Checked;
            Publicas._usuario.AssinaturaChamado = AssinaturaTextBox.Text;
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

            //List<RamaisAssociadosAoColaborador> _ListaRamais = new List<RamaisAssociadosAoColaborador>();

            //RamaisAssociadosAoColaborador _assoc = new RamaisAssociadosAoColaborador();
            //_assoc.IdRamal = _ramais.Id;
            //_assoc.IdColaborador = Publicas._idColaborador;
            //_assoc.Complemento = (GrupoRamalTextBox.Text.Split(' ')[1] != "" ? GrupoRamalTextBox.Text.Split(' ')[1] : "");
            //_assoc.NomeColaborador = nomeTextBox.Text.Split(' ')[0].ToUpper();

            //_ListaRamais.Add(_assoc);

            //if (_ramaisAntes.Numero != _ramais.Numero)
            //{
            //    _assoc.Existe = false;
            //    if (!new RamaisBO().ExcluirAssociacao(Publicas._idColaborador))
            //    {
            //        new Notificacoes.Mensagem("Problemas durante a atualização do ramal." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
            //        return;
            //    }
            //}
            //else
            //{
            //    _assoc.Existe = true;
            //    foreach (var item in _listaColaboradores.Where(w => w.IdColaborador == Publicas._idColaborador))
            //    {
            //        _assoc.Id = item.Id;
            //    }
            //}

            //if (!new RamaisBO().GravarAssociacao(_ListaRamais))
            //{
            //    new Notificacoes.Mensagem("Problemas durante a atualização do ramal." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
            //    return;
            //}

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Editou seu Perfil" + descricao;
            _log.Tela = "Editar Perfil";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

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
                GrupoRamalTextBox.Focus();
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
                GrupoRamalTextBox.Focus();
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

        private void cpfMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                cpfMaskedEditBox.Focus();
                return;
            }

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
                nomeMaquinaTextBox.Focus();
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

        private void telefoneTextBox_Validating_1(object sender, CancelEventArgs e)
        {
            telefoneTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                cpfMaskedEditBox.Focus();
                return;
            }

            Publicas._telefone = 0;

            if (telefoneTextBox.ClipText.Trim() == "")
            {
                Publicas._idEmpresa = Publicas._usuario.IdEmpresa;
                new Pesquisas.Telefone().ShowDialog();

                if (Publicas._idRetornoPesquisa == 0)
                {
                    telefoneTextBox.Text = string.Empty;
                    telefoneTextBox.Focus();
                    return;
                }

                _telefone = new RamaisBO().Consultar(Publicas._idRetornoPesquisa);

                if (_telefone.Existe)
                {
                    telefoneTextBox.Text = _telefone.Numero.ToString();
                }
                Publicas._idEmpresa = 0;
                return;
            }

            _telefone = new RamaisBO().Consultar(Publicas._usuario.IdEmpresa, Convert.ToDecimal(telefoneTextBox.ClipText.Trim()));

            if (!_telefone.Existe)
            {
                new Notificacoes.Mensagem("Telefone não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                telefoneTextBox.Focus();
                return;
            }
        }

        private void ramalTextBox_Validating(object sender, CancelEventArgs e)
        {
            ramalTextBox.BorderColor = Publicas._bordaSaida;
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._telefone = _telefone.Id;

            if (ramalTextBox.Text.Trim() == "")
            {
                new Pesquisas.Ramais().ShowDialog();

                if (Publicas._idRetornoPesquisa == 0)
                {
                    ramalTextBox.Text = string.Empty;
                    ramalTextBox.Focus();
                    return;
                }

                ramalTextBox.Text = Publicas._idRetornoPesquisa.ToString();
            }
            Publicas._telefone = 0;

            _ramais = new RamaisBO().ConsultarRamal(_telefone.Id, Convert.ToInt32(ramalTextBox.Text.Trim()));
            
            if (!_ramais.Existe)
            {
                new Notificacoes.Mensagem("Ramal não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ramalTextBox.Focus();
                return;
            }

            _listaColaboradores = new RamaisBO().ListarColaboradoresAssociados(_ramais.Id);

            if (_listaColaboradores.Where(w => w.IdColaborador == Publicas._idColaborador).Count() == 0)
                GrupoRamalTextBox.Text = _ramais.Grupo;
            else
            {
                foreach (var item in _listaColaboradores.Where(w => w.IdColaborador == Publicas._idColaborador))
                {
                    GrupoRamalTextBox.Text = _ramais.Grupo + " " + item.Complemento;
                }
            }
        }

        private void GrupoRamalTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void nomeMaquinaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                notificacaoCorridaCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                setorTextBox.Focus();
            }
        }
    }
}
