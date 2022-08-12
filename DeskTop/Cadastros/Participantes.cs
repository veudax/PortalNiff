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
    public partial class Participantes : Form
    {
        public Participantes()
        {
            InitializeComponent();

            valorCurrencyTextBox.BackGroundColor = codigoTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Corridas _corridas;
        List<Classes.DistanciaCorrida> _listaDistancias;
        Classes.ParticipanteCorrida _participante;
        Classes.Usuario _usuario;
        Classes.Usuario _usuarioCapitao;

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

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void valorCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
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

        private void tipoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
            nomeTextBox.BorderColor = Publicas._bordaSaida;
        }

        private void cpfMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void cpfMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(cpfMaskedEditBox.ClipText.Trim()))
            {
                new Pesquisas.Usuarios().ShowDialog();

                cpfMaskedEditBox.Text = Publicas._codigoRetornoPesquisa;

                if (cpfMaskedEditBox.Text.Trim() == "" || cpfMaskedEditBox.Text.Trim() == "0")
                {
                    cpfMaskedEditBox.Text = string.Empty;
                    cpfMaskedEditBox.Focus();
                    return;
                }

                cpfMaskedEditBox_Validating(sender, new CancelEventArgs());
            }


            _participante = new ParticipanteCorridaBO().Consultar(0, _corridas.Id, Convert.ToDecimal(cpfMaskedEditBox.ClipText.Trim()));

            if (_participante.Existe)
            {
                foreach (var item in _listaDistancias.Where(w => w.Id == _participante.IdDistancia))
                {
                    for (int i = 0; i < distanciaComboBox.Items.Count; i++)
                    {
                        distanciaComboBox.SelectedIndex = i;
                        if (distanciaComboBox.Text.Contains(item.Km.ToString()))
                            break;
                    }
                }

                excluirButton.Enabled = _participante.Existe;
                nomeTextBox.Text = _participante.Nome;
                inscricaoEmGrupoCheckBox.Checked = _participante.InscricaoEmGrupo;
                inscricaoPagaCheckBox.Checked = _participante.InscricaoPaga;
                sexoComboBox.SelectedIndex = (_participante.Sexo == "F" ? 0 : 1);

                if (_participante.IdUsuarioGrupo != 0)
                {
                    _usuarioCapitao = new UsuarioBO().ConsultarPorId(_participante.IdUsuarioGrupo);
                    nomeCapitaoTextBox.Text = _usuarioCapitao.Nome;
                }
            }
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {

            codigoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Corridas().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _corridas = new CorridasBO().Consultar(Convert.ToInt32(codigoTextBox.Text));

            if (!_corridas.Existe)
            {
                new Notificacoes.Mensagem("Corrida não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }
        
            _listaDistancias = new CorridasBO().ListarDistancias(_corridas.Id);

            if (!_corridas.Ativo)
            {
                new Notificacoes.Mensagem("Corrida está inativa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            descricaoCorridaTextBox.Text = _corridas.Nome;
            valorCurrencyTextBox.DecimalValue = _corridas.Valor;
            dataLabel.Text = _corridas.Data.ToShortDateString();
            valorGrupoLabel.Text = "Valor grupo " + string.Format("{0:0.00}", _corridas.ValorGrupo);
            
            foreach (var item in _listaDistancias.OrderBy(o => o.Km))
                distanciaComboBox.Items.Add(item.Km + " Km");        

            if (Publicas._idRetornoPesquisa != 0)
                usuarioTextBox.Focus();
        }

        private void usuarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                participanteExternoCheckBox.Enabled = true;
                participanteExternoCheckBox.Checked = true;
                nomeTextBox.Enabled = true;
                cpfMaskedEditBox.Enabled = true;
                Publicas._setaParaBaixo = false;
                return;
            }

            if (usuarioTextBox.Text.Trim() == "")
            {
                new Pesquisas.Usuarios().ShowDialog();

                usuarioTextBox.Text = Publicas._usuarioAcesso;

                if (usuarioTextBox.Text.Trim() == "" || usuarioTextBox.Text.Trim() == "0")
                {
                    usuarioTextBox.Text = string.Empty;
                    usuarioTextBox.Focus();
                    return;
                }
            }

            _usuario = new UsuarioBO().Consultar(usuarioTextBox.Text);

            if (!_usuario.Existe)
            {
                new Notificacoes.Mensagem("Usuário não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }
            if (!_usuario.Ativo)
            {
                new Notificacoes.Mensagem("Usuário está inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _usuario.Nome;
            cpfMaskedEditBox.Text = _usuario.CPF.ToString();
            cpfMaskedEditBox.Enabled = false;
            nomeTextBox.Enabled = false;
            participanteExternoCheckBox.Enabled = false;

            _participante = new ParticipanteCorridaBO().Consultar(_usuario.Id, _corridas.Id);

            if (_participante != null)
            {
                foreach (var item in _listaDistancias.Where(w => w.Id == _participante.IdDistancia))
                {
                    for (int i = 0; i < distanciaComboBox.Items.Count; i++)
                    {
                        distanciaComboBox.SelectedIndex = i;
                        if (distanciaComboBox.Text.Contains(item.Km.ToString()))
                            break;
                    }
                }

                excluirButton.Enabled = _participante.Existe;
                inscricaoEmGrupoCheckBox.Checked = _participante.InscricaoEmGrupo;
                inscricaoPagaCheckBox.Checked = _participante.InscricaoPaga;
                sexoComboBox.SelectedIndex = (_participante.Sexo == "F" ? 0 : 1);
                if (_participante.IdUsuarioGrupo != 0)
                {
                    _usuarioCapitao = new UsuarioBO().ConsultarPorId(_participante.IdUsuarioGrupo);
                    usuarioCapitaoTextBox.Text = _usuarioCapitao.UsuarioAcesso;
                    nomeCapitaoTextBox.Text = _usuarioCapitao.Nome;
                }
            }
            

            if (Publicas._usuarioAcesso != "")
                distanciaComboBox.Focus();
        }

        private void distanciaComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (distanciaComboBox.SelectedIndex == -1)
            {
                new Notificacoes.Mensagem("Escolha a distância que irá participar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                distanciaComboBox.Focus();
                return;
            }
        }

        private void usuarioCapitaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
            
            if (usuarioCapitaoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Usuarios().ShowDialog();

                usuarioCapitaoTextBox.Text = Publicas._usuarioAcesso;

                if (usuarioCapitaoTextBox.Text.Trim() == "" || usuarioCapitaoTextBox.Text.Trim() == "0")
                {
                    usuarioCapitaoTextBox.Text = string.Empty;
                    usuarioCapitaoTextBox.Focus();
                    return;
                }
            }

            _usuarioCapitao = new UsuarioBO().Consultar(usuarioCapitaoTextBox.Text);

            if (!_usuarioCapitao.Existe)
            {
                new Notificacoes.Mensagem("Usuário não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            if (!_usuarioCapitao.Ativo)
            {
                new Notificacoes.Mensagem("Usuário está inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioCapitaoTextBox.Focus();
                return;
            }

            nomeCapitaoTextBox.Text = _usuarioCapitao.Nome;
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

        private void inscricaoEmGrupoCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            usuarioCapitaoTextBox.Enabled = inscricaoEmGrupoCheckBox.Checked;
            valorGrupoLabel.Visible = inscricaoEmGrupoCheckBox.Checked;
        }
        
        private void participanteExternoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                usuarioTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void usuarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (participanteExternoCheckBox.Enabled)
                    participanteExternoCheckBox.Focus();
                else
                    distanciaComboBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                participanteExternoCheckBox.Enabled = true;
                participanteExternoCheckBox.Focus();
            }
        }

        private void distanciaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (cpfMaskedEditBox.Enabled)
                    cpfMaskedEditBox.Focus();
                else
                    inscricaoPagaCheckBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (participanteExternoCheckBox.Enabled)
                    participanteExternoCheckBox.Focus();
                else
                    usuarioTextBox.Focus();
            }
        }

        private void cpfMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (nomeTextBox.Enabled)
                     nomeTextBox.Focus();
                else
                    inscricaoPagaCheckBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                distanciaComboBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                sexoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (cpfMaskedEditBox.Enabled)
                    cpfMaskedEditBox.Focus();
                else
                    distanciaComboBox.Focus();
            }
        }

        private void inscricaoPagaCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                inscricaoEmGrupoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                sexoComboBox.Focus();
            }
        }

        private void inscricaoEmGrupoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (usuarioCapitaoTextBox.Enabled)
                    usuarioCapitaoTextBox.Focus();
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                inscricaoPagaCheckBox.Focus();
            }
        }

        private void usuarioCapitaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                inscricaoEmGrupoCheckBox.Focus();
            }
        }
        
        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (usuarioCapitaoTextBox.Enabled)
                    usuarioCapitaoTextBox.Focus();
                else
                    inscricaoEmGrupoCheckBox.Focus();
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

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            nomeCapitaoTextBox.Text = string.Empty;
            descricaoCorridaTextBox.Text = string.Empty;
            usuarioCapitaoTextBox.Text = string.Empty;
            usuarioTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            valorCurrencyTextBox.DecimalValue = 0;
            distanciaComboBox.SelectedIndex = -1;
            distanciaComboBox.Text = string.Empty;
            participanteExternoCheckBox.Checked = false;
            inscricaoEmGrupoCheckBox.Checked = false;
            inscricaoPagaCheckBox.Checked = false;
            cpfMaskedEditBox.Text = string.Empty;
            dataLabel.Text = string.Empty;
            valorCurrencyTextBox.DecimalValue = 0;
            sexoComboBox.SelectedIndex = 0;
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

            codigoTextBox.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o nome do participante.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            int idDistancia = 0;

            foreach (var item in _listaDistancias.Where(w => w.Km == Convert.ToInt32(distanciaComboBox.Text.Substring(0, 2).Trim())))
            {
                idDistancia = item.Id;
            }

            if (_participante == null)
                _participante = new ParticipanteCorrida();

            _participante.IdDistancia = idDistancia;

            if (usuarioTextBox.Text != "")
                _participante.IdUsuario = _usuario.Id;

            _participante.InscricaoEmGrupo = inscricaoEmGrupoCheckBox.Checked;
            _participante.InscricaoPaga = inscricaoPagaCheckBox.Checked;
            _participante.Sexo = sexoComboBox.Text.Substring(0, 1);
            _participante.ValorInscrito = (inscricaoEmGrupoCheckBox.Checked ? _corridas.ValorGrupo : 0);

            if (cpfMaskedEditBox.Enabled)
                _participante.CPF = Convert.ToDecimal(cpfMaskedEditBox.ClipText);
            
            if (usuarioCapitaoTextBox.Text != "")
                _participante.IdUsuarioGrupo = _usuarioCapitao.Id;

            _participante.Nome = nomeTextBox.Text;

            if (!new ParticipanteCorridaBO().Gravar(_participante))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (usuarioCapitaoTextBox.Text != "")
            {
                if (!new ParticipanteCorridaBO().AtualizaValorInscricaoCapitao(_participante.IdUsuarioGrupo, _participante.IdDistancia, _corridas.ValorGrupo))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }

            limparButton_Click(sender, e);
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new ParticipanteCorridaBO().Excluir(_participante))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nomeTextBox_TextChanged(object sender, EventArgs e)
        {
            gravarButton.Enabled = !string.IsNullOrEmpty(nomeTextBox.Text);
        }

        private void participanteExternoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (participanteExternoCheckBox.Checked)
                cpfLabel.Font = new Font(cpfLabel.Font, FontStyle.Bold);
            else
                cpfLabel.Font = new Font(cpfLabel.Font, cpfLabel.Font.Style & ~FontStyle.Bold);
        }

        private void participanteExternoCheckBox_Enter(object sender, EventArgs e)
        {
            nomeTextBox.BorderColor = Publicas._bordaSaida;
        }

        private void Participantes_Shown(object sender, EventArgs e)
        {
            sexoComboBox.Items.AddRange(new object[] { "Feminino", "Masculino" });
            sexoComboBox.SelectedIndex = 0;
            if (codigoTextBox.Text != "")
            {
                codigoTextBox_Validating(sender, new CancelEventArgs());
                usuarioTextBox.Text = Publicas._usuario.UsuarioAcesso;
                usuarioTextBox.Focus();
            }
        }

        private void sexoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                inscricaoPagaCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (nomeTextBox.Enabled)
                    nomeTextBox.Focus();
                else
                    distanciaComboBox.Focus();
            }
        }

        private void sexoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void usuarioTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void cpfMaskedEditBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCPFButton.Enabled = string.IsNullOrEmpty(cpfMaskedEditBox.ClipText.Trim());
        }

        private void usuarioCapitaoTextBox_TextChanged(object sender, EventArgs e)
        {
            usuarioCapitaoTextBox.Enabled = string.IsNullOrEmpty(cpfMaskedEditBox.ClipText.Trim());
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Corridas().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }

                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaUsuarioButton_Click(object sender, EventArgs e)
        {
            if (usuarioTextBox.Text.Trim() == "")
            {
                new Pesquisas.Usuarios().ShowDialog();

                usuarioTextBox.Text = Publicas._usuarioAcesso;

                if (usuarioTextBox.Text.Trim() == "" || usuarioTextBox.Text.Trim() == "0")
                {
                    usuarioTextBox.Text = string.Empty;
                    usuarioTextBox.Focus();
                    return;
                }

                usuarioTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaCPFButton_Click(object sender, EventArgs e)
        {
            if (cpfMaskedEditBox.ClipText.Trim() == "")
            {
                new Pesquisas.Usuarios().ShowDialog();

                cpfMaskedEditBox.Text = Publicas._codigoRetornoPesquisa;

                if (cpfMaskedEditBox.Text.Trim() == "" || cpfMaskedEditBox.Text.Trim() == "0")
                {
                    cpfMaskedEditBox.Text = string.Empty;
                    cpfMaskedEditBox.Focus();
                    return;
                }

                cpfMaskedEditBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaCapitaoButton_Click(object sender, EventArgs e)
        {
            if (usuarioCapitaoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Usuarios().ShowDialog();

                usuarioCapitaoTextBox.Text = Publicas._usuarioAcesso;

                if (usuarioCapitaoTextBox.Text.Trim() == "" || usuarioCapitaoTextBox.Text.Trim() == "0")
                {
                    usuarioCapitaoTextBox.Text = string.Empty;
                    usuarioCapitaoTextBox.Focus();
                    return;
                }
                usuarioCapitaoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
