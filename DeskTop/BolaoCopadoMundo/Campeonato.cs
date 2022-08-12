using Classes;
using Negocio;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.BolaoCopadoMundo
{
    public partial class Campeonato : Form
    {
        public Campeonato()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }

            dataDateTimePicker.BackColor = ColaboradorTextBox.BackColor;
            NovaDataTimePickerAdv1.BackColor = ColaboradorTextBox.BackColor;
            dataDateTimePicker.BorderColor = Publicas._bordaSaida;
            NovaDataTimePickerAdv1.BorderColor = Publicas._bordaSaida;

            dataDateTimePicker.Style = VisualStyle.Office2016Black;
            NovaDataTimePickerAdv1.Style = VisualStyle.Office2016Black;
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Torneio _torneio;
        Classes.PartidasDoTorneio _partidas;
        Classes.Usuario _usuario = new Usuario();
        Classes.Usuario _usuario2 = new Usuario();
        Classes.Usuario _usuario3 = new Usuario();
        Classes.Usuario _usuario4 = new Usuario();
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Torneio> _listaTorneios;
        List<Classes.PartidasDoTorneio> _listaPartidas;

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

        private void Campeonato_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(true);
            _listaTorneios = new TorneioBO().ListarTorneio(true);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            TorneioComboBox.DataSource = _listaTorneios.OrderBy(o => o.Nome).ToList();
            TorneioComboBox.DisplayMember = "Nome";

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

            for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
            {
                empresaComboBoxAdv.SelectedIndex = i;
                if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                {
                    break;
                }
            }

            dataDateTimePicker.Value = DateTime.Now;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                TorneioComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void TorneioComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SexoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }
        
        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            empresaComboBoxAdv.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }
        }

        private void TorneioComboBox_Validating(object sender, CancelEventArgs e)
        {
            TorneioComboBox.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaTorneios.Where(w => w.Nome == TorneioComboBox.Text))
            {
                _torneio = item;
            }
        }
              
        
        private void gravarButton_Click(object sender, EventArgs e)
        {
            bool _encontrou = false;
            foreach (var item in _listaPartidas.Where(w => w.IdUsuario == _usuario.Id))
            {
                _encontrou = true;
                item.NomePartida = NomePartidaTextBox.Text;
                item.Round1 = Convert.ToDecimal(Round1TextBox.Text);
                item.Round2 = Convert.ToDecimal(Round2TextBox.Text);
                item.Round3 = Convert.ToDecimal(Round3TextBox.Text);
                item.Round4 = Convert.ToDecimal(Round4TextBox.Text);
                item.Total = Convert.ToDecimal(TotalTextBox.Text);
            }

            if (!_encontrou)
            {
                _partidas = new PartidasDoTorneio();
                _partidas.NomePartida = NomePartidaTextBox.Text;
                _partidas.IdEmpresa = _empresa.IdEmpresa;
                _partidas.Data = dataDateTimePicker.Value.Date;
                _partidas.IdTorneio = _torneio.Id;
                _partidas.Sexo = (SexoComboBox.SelectedIndex == 0 ? "F" : "M");
                _partidas.Round1 = Convert.ToDecimal(Round1TextBox.Text);
                _partidas.Round2 = Convert.ToDecimal(Round2TextBox.Text);
                _partidas.Round3 = Convert.ToDecimal(Round3TextBox.Text);
                _partidas.Round4 = Convert.ToDecimal(Round4TextBox.Text);
                _partidas.Total = Convert.ToDecimal(TotalTextBox.Text);
            }

            if (Usuario2TextBox.Text != "")
            {
                _encontrou = false;
                foreach (var item in _listaPartidas.Where(w => w.IdUsuario == _usuario2.Id))
                {
                    _encontrou = true;
                    item.NomePartida = NomePartidaTextBox.Text;
                    item.Round1 = Convert.ToDecimal(Round1Usuario2TextBox.Text);
                    item.Round2 = Convert.ToDecimal(Round2Usuario2TextBox.Text);
                    item.Round3 = Convert.ToDecimal(Round3Usuario2TextBox.Text);
                    item.Round4 = Convert.ToDecimal(Round4Usuario2TextBox.Text);
                    item.Total = Convert.ToDecimal(Total2Usuario2TextBox.Text);
                }

                if (!_encontrou)
                {
                    _partidas.NomePartida = NomePartidaTextBox.Text;
                    _partidas.IdEmpresa = _empresa.IdEmpresa;
                    _partidas.Data = dataDateTimePicker.Value.Date;
                    _partidas.IdTorneio = _torneio.Id;
                    _partidas.Sexo = (SexoComboBox.SelectedIndex == 0 ? "F" : "M");
                    _partidas.Round1 = Convert.ToDecimal(Round1Usuario2TextBox.Text);
                    _partidas.Round2 = Convert.ToDecimal(Round2Usuario2TextBox.Text);
                    _partidas.Round3 = Convert.ToDecimal(Round3Usuario2TextBox.Text);
                    _partidas.Round4 = Convert.ToDecimal(Round4Usuario2TextBox.Text);
                    _partidas.Total = Convert.ToDecimal(Total2Usuario2TextBox.Text);
                }
            }

            if (Usuario3TextBox.Text != "")
            {
                _encontrou = false;
                foreach (var item in _listaPartidas.Where(w => w.IdUsuario == _usuario3.Id))
                {
                    _encontrou = true;
                    item.NomePartida = NomePartidaTextBox.Text;
                    item.Round1 = Convert.ToDecimal(Round1Usuario3RextBox.Text);
                    item.Round2 = Convert.ToDecimal(Round2Usuario3TextBox.Text);
                    item.Round3 = Convert.ToDecimal(Round3Usuario3TextBox.Text);
                    item.Round4 = Convert.ToDecimal(Round4Usuario3TextBox.Text);
                    item.Total = Convert.ToDecimal(Total3Usuario3TextBox.Text);
                }

                if (!_encontrou)
                {
                    _partidas.NomePartida = NomePartidaTextBox.Text;
                    _partidas.IdEmpresa = _empresa.IdEmpresa;
                    _partidas.Data = dataDateTimePicker.Value.Date;
                    _partidas.IdTorneio = _torneio.Id;
                    _partidas.Sexo = (SexoComboBox.SelectedIndex == 0 ? "F" : "M");
                    _partidas.Round1 = Convert.ToDecimal(Round1Usuario3RextBox.Text);
                    _partidas.Round2 = Convert.ToDecimal(Round2Usuario3TextBox.Text);
                    _partidas.Round3 = Convert.ToDecimal(Round3Usuario3TextBox.Text);
                    _partidas.Round4 = Convert.ToDecimal(Round4Usuario3TextBox.Text);
                    _partidas.Total = Convert.ToDecimal(Total3Usuario3TextBox.Text);
                }
            }

            if (Usuario4TextBox.Text != "")
            {
                _encontrou = false;
                foreach (var item in _listaPartidas.Where(w => w.IdUsuario == _usuario4.Id))
                {
                    _encontrou = true;
                    item.Round1 = Convert.ToDecimal(Round1Usuario4TextBox.Text);
                    item.Round2 = Convert.ToDecimal(Round2Usuario4textBox.Text);
                    item.Round3 = Convert.ToDecimal(Round3Usuario4TextBox.Text);
                    item.Round4 = Convert.ToDecimal(Round4Usuario4TextBox.Text);
                    item.Total = Convert.ToDecimal(Total4Usuario4TextBox.Text);
                }

                if (!_encontrou)
                {
                    _partidas.IdEmpresa = _empresa.IdEmpresa;
                    _partidas.NomePartida = NomePartidaTextBox.Text;
                    _partidas.Data = dataDateTimePicker.Value.Date;
                    _partidas.IdTorneio = _torneio.Id;
                    _partidas.Sexo = (SexoComboBox.SelectedIndex == 0 ? "F" : "M");
                    _partidas.Round1 = Convert.ToDecimal(Round1Usuario3RextBox.Text);
                    _partidas.Round2 = Convert.ToDecimal(Round2Usuario3TextBox.Text);
                    _partidas.Round3 = Convert.ToDecimal(Round3Usuario3TextBox.Text);
                    _partidas.Round4 = Convert.ToDecimal(Round4Usuario3TextBox.Text);
                    _partidas.Total = Convert.ToDecimal(Total3Usuario3TextBox.Text);
                }
            }

            if (!AtivoCheckBox.Checked)
            {
                if (!new TorneioBO().GravarPartidas(_listaPartidas))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }
            else
            {
                _listaPartidas.ForEach(u => u.Data = NovaDataTimePickerAdv1.Value);
                
                if (!new TorneioBO().AlterarDataDaPartida(_listaPartidas))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            ColaboradorTextBox.Text = string.Empty;
            NomeColaboradorTextBox.Text = string.Empty;
            NomePartidaTextBox.Text = string.Empty;
            Round1TextBox.Text = "0";
            Round2TextBox.Text = "0";
            Round3TextBox.Text = "0";
            Round4TextBox.Text = "0";
            TotalTextBox.Text = "0";

            Usuario2TextBox.Text = string.Empty;
            Nome2TextBox.Text = string.Empty;
            Round1Usuario2TextBox.Text = "0";
            Round2Usuario2TextBox.Text = "0";
            Round3Usuario2TextBox.Text = "0";
            Round4Usuario2TextBox.Text = "0";
            Total2Usuario2TextBox.Text = "0";

            Usuario3TextBox.Text = string.Empty;
            Nome3TextBox.Text = string.Empty;
            Round1Usuario3RextBox.Text = "0";
            Round2Usuario3TextBox.Text = "0";
            Round3Usuario3TextBox.Text = "0";
            Round4Usuario3TextBox.Text = "0";
            Total3Usuario3TextBox.Text = "0";

            Usuario4TextBox.Text = string.Empty;
            Nome4TextBox.Text = string.Empty;
            Round1Usuario4TextBox.Text = "0";
            Round2Usuario4textBox.Text = "0";
            Round3Usuario4TextBox.Text = "0";
            Round4Usuario4TextBox.Text = "0";
            Total4Usuario4TextBox.Text = "0";

            AtivoCheckBox.Checked = false;

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            TorneioComboBox.Focus();
        }

        private void dataDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                AtivoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                TorneioComboBox.Focus();
            }
        }

        private void AtivoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (NovaDataTimePickerAdv1.Visible)
                    NovaDataTimePickerAdv1.Focus();
                else
                    NomePartidaTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataDateTimePicker.Focus();
            }
        }

        private void ColaboradorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                Round1TextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                NomePartidaTextBox.Focus();
            }
        }

        private void Round1TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                Round2TextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ColaboradorTextBox.Focus();
            }
        }

        private void Round2TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                Round3TextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                Round1TextBox.Focus();
            }
        }

        private void Round3TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                Round4TextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                Round2TextBox.Focus();
            }
        }

        private void Round4TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                Round3TextBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                Round4TextBox.Focus();
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
        private void ColaboradorTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            PesquisaColaboradorButton.Enabled = string.IsNullOrEmpty(ColaboradorTextBox.Text.Trim());
        }

        private void AtivoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            NovaDataTimePickerAdv1.Visible = AtivoCheckBox.Checked;
            label5.Visible = AtivoCheckBox.Checked;
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

            Publicas._usuarioAcesso = "";
            if (string.IsNullOrEmpty(ColaboradorTextBox.Text.Trim()))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;

                new Pesquisas.Usuarios().ShowDialog();

                ColaboradorTextBox.Text = Publicas._usuarioAcesso.ToString();

                if (string.IsNullOrEmpty(ColaboradorTextBox.Text) || ColaboradorTextBox.Text == "0")
                {
                    ColaboradorTextBox.Text = string.Empty;
                    ColaboradorTextBox.Focus();
                    return;
                }
            }

            _usuario = new UsuarioBO().Consultar(ColaboradorTextBox.Text.Trim());

            if (_usuario != null)
            {
                if (!_usuario.Existe)
                {
                    new Notificacoes.Mensagem("Usuário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    ColaboradorTextBox.Focus();
                    return;
                }

                NomeColaboradorTextBox.Text = _usuario.Nome;
            }
        }

        private void Round1TextBox_Validating(object sender, CancelEventArgs e)
        {
            Round1TextBox.BorderColor = Publicas._bordaSaida;
            Round2TextBox.BorderColor = Publicas._bordaSaida;
            Round3TextBox.BorderColor = Publicas._bordaSaida;
            Round4TextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            decimal _total = Convert.ToDecimal(Round1TextBox.Text) +
                Convert.ToDecimal(Round2TextBox.Text) +
                Convert.ToDecimal(Round3TextBox.Text) +
                Convert.ToDecimal(Round4TextBox.Text);

            TotalTextBox.Text = _total.ToString();
            Refresh();
        }

        private void PesquisaColaboradorButton_Click(object sender, EventArgs e)
        {
            Publicas._usuarioAcesso = "";
            if (string.IsNullOrEmpty(ColaboradorTextBox.Text.Trim()))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                new Pesquisas.Colaborador().ShowDialog();

                ColaboradorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(ColaboradorTextBox.Text) || ColaboradorTextBox.Text == "0")
                {
                    ColaboradorTextBox.Text = string.Empty;
                    ColaboradorTextBox.Focus();
                    return;
                }

                ColaboradorTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void dataDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void dataDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            dataDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _listaPartidas = new TorneioBO().ListarPartidas(_torneio.Id, dataDateTimePicker.Value, (SexoComboBox.SelectedIndex == 0 ? "F" : "M"));
            
            int i = 0;
            foreach (var item in _listaPartidas.OrderBy( o => o.Id ))
            {
                if (i == 0)
                {
                    NomePartidaTextBox.Text = item.NomePartida;
                    ColaboradorTextBox.Text = item.Usuario;
                    Round1TextBox.Text = item.Round1.ToString();
                    Round2TextBox.Text = item.Round2.ToString();
                    Round3TextBox.Text = item.Round3.ToString();
                    Round4TextBox.Text = item.Round4.ToString();
                    TotalTextBox.Text = item.Total.ToString();

                    if (ColaboradorTextBox.Text != "")
                        ColaboradorTextBox_Validating(sender, e);
                }

                if (i == 1)
                {
                    Usuario2TextBox.Text = item.Usuario;
                    Round1Usuario2TextBox.Text = item.Round1.ToString();
                    Round2Usuario2TextBox.Text = item.Round2.ToString();
                    Round3Usuario2TextBox.Text = item.Round3.ToString();
                    Round4Usuario2TextBox.Text = item.Round4.ToString();
                    Total2Usuario2TextBox.Text = item.Total.ToString();

                    if (Usuario2TextBox.Text != "")
                        Usuario2TextBox_Validating(sender, e);
                }

                if (i == 2)
                {
                    Usuario3TextBox.Text = item.Usuario;
                    Round1Usuario3RextBox.Text = item.Round1.ToString();
                    Round2Usuario3TextBox.Text = item.Round2.ToString();
                    Round3Usuario3TextBox.Text = item.Round3.ToString();
                    Round4Usuario3TextBox.Text = item.Round4.ToString();
                    Total3Usuario3TextBox.Text = item.Total.ToString();

                    if (Usuario3TextBox.Text != "")
                        Usuario3TextBox_Validating(sender, e);
                }

                if (i == 3)
                {
                    Usuario4TextBox.Text = item.Usuario;
                    Round1Usuario4TextBox.Text = item.Round1.ToString();
                    Round2Usuario4textBox.Text = item.Round2.ToString();
                    Round3Usuario4TextBox.Text = item.Round3.ToString();
                    Round4Usuario4TextBox.Text = item.Round4.ToString();
                    Total4Usuario4TextBox.Text = item.Total.ToString();

                    if (Usuario4TextBox.Text != "")
                        Usuario4TextBox_Validating(sender, e);
                }
                i++;
            }

            NomePartidaTextBox.Focus();
            gravarButton.Enabled = true;
            excluirButton.Enabled = _listaPartidas.Count() != 0;
        }

        private void NomePartidaTextBox_Validating(object sender, CancelEventArgs e)
        {
            NomePartidaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                NomePartidaTextBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }

        }

        private void NovaDataTimePickerAdv1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                NomePartidaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AtivoCheckBox.Focus();
            }
        }

        private void NomePartidaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ColaboradorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (NovaDataTimePickerAdv1.Visible)
                    NovaDataTimePickerAdv1.Focus();
                else
                    AtivoCheckBox.Focus();
            }
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new TorneioBO().ExcluirPartidas(_partidas.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
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

        private void Usuario2TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void Usuario2TextBox_Validating(object sender, CancelEventArgs e)
        {
            Usuario2TextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Usuario2TextBox.Text = string.Empty;
                PesquisaUsuario2Button.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            Publicas._usuarioAcesso = "";
            if (string.IsNullOrEmpty(Usuario2TextBox.Text.Trim()))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;

                new Pesquisas.Usuarios().ShowDialog();

                Usuario2TextBox.Text = Publicas._usuarioAcesso.ToString();

                if (string.IsNullOrEmpty(Usuario2TextBox.Text) || Usuario2TextBox.Text == "0")
                {
                    Usuario2TextBox.Text = string.Empty;
                    Usuario2TextBox.Focus();
                    return;
                }
            }

            _usuario2 = new UsuarioBO().Consultar(Usuario2TextBox.Text.Trim());

            if (_usuario2 != null)
            {
                if (!_usuario2.Existe)
                {
                    new Notificacoes.Mensagem("Usuário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    Usuario2TextBox.Focus();
                    return;
                }

                Nome2TextBox.Text = _usuario2.Nome;
            }
        }

        private void Usuario3TextBox_Validating(object sender, CancelEventArgs e)
        {
            Usuario3TextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Usuario3TextBox.Text = string.Empty;
                PesquisaUsuario3Button.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            Publicas._usuarioAcesso = "";
            if (string.IsNullOrEmpty(Usuario3TextBox.Text.Trim()))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;

                new Pesquisas.Usuarios().ShowDialog();

                Usuario3TextBox.Text = Publicas._usuarioAcesso.ToString();

                if (string.IsNullOrEmpty(Usuario3TextBox.Text) || Usuario3TextBox.Text == "0")
                {
                    Usuario3TextBox.Text = string.Empty;
                    Usuario3TextBox.Focus();
                    return;
                }
            }

            _usuario3 = new UsuarioBO().Consultar(Usuario3TextBox.Text.Trim());


            if (_usuario3 != null)
            {
                if (!_usuario3.Existe)
                {
                    new Notificacoes.Mensagem("Usuário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    Usuario3TextBox.Focus();
                    return;
                }

                Nome3TextBox.Text = _usuario3.Nome;
            }
            
        }

        private void Usuario4TextBox_Validating(object sender, CancelEventArgs e)
        {
            Usuario4TextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Usuario4TextBox.Text = string.Empty;
                PesquisaUsuario4Button.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            Publicas._usuarioAcesso = "";
            if (string.IsNullOrEmpty(Usuario4TextBox.Text.Trim()))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;

                new Pesquisas.Usuarios().ShowDialog();

                Usuario4TextBox.Text = Publicas._usuarioAcesso.ToString();

                if (string.IsNullOrEmpty(Usuario4TextBox.Text) || Usuario4TextBox.Text == "0")
                {
                    Usuario4TextBox.Text = string.Empty;
                    Usuario4TextBox.Focus();
                    return;
                }
            }

            _usuario4 = new UsuarioBO().Consultar(Usuario4TextBox.Text.Trim());

            if (_usuario4 != null)
            {
                if (!_usuario4.Existe)
                {
                    new Notificacoes.Mensagem("Usuário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    Usuario4TextBox.Focus();
                    return;
                }

                Nome4TextBox.Text = _usuario4.Nome;
            }
        }

        private void Round1Usuario2TextBox_Validating(object sender, CancelEventArgs e)
        {
            Round1Usuario2TextBox.BorderColor = Publicas._bordaSaida;
            Round2Usuario2TextBox.BorderColor = Publicas._bordaSaida;
            Round3Usuario2TextBox.BorderColor = Publicas._bordaSaida;
            Round4Usuario2TextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            decimal _total = Convert.ToDecimal(Round1Usuario2TextBox.Text) +
                Convert.ToDecimal(Round2Usuario2TextBox.Text) +
                Convert.ToDecimal(Round3Usuario2TextBox.Text) +
                Convert.ToDecimal(Round4Usuario2TextBox.Text);

            Total2Usuario2TextBox.Text = _total.ToString();
        }

        private void Round1Usuario3RextBox_Validating(object sender, CancelEventArgs e)
        {
            Round1Usuario3RextBox.BorderColor = Publicas._bordaSaida;
            Round2Usuario3TextBox.BorderColor = Publicas._bordaSaida;
            Round3Usuario3TextBox.BorderColor = Publicas._bordaSaida;
            Round4Usuario3TextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            decimal _total = Convert.ToDecimal(Round1Usuario3RextBox.Text) +
                Convert.ToDecimal(Round2Usuario3TextBox.Text) +
                Convert.ToDecimal(Round3Usuario3TextBox.Text) +
                Convert.ToDecimal(Round4Usuario3TextBox.Text);

            Total3Usuario3TextBox.Text = _total.ToString();
        }

        private void Round1Usuario4TextBox_Validating(object sender, CancelEventArgs e)
        {
            Round1Usuario4TextBox.BorderColor = Publicas._bordaSaida;
            Round2Usuario4textBox.BorderColor = Publicas._bordaSaida;
            Round3Usuario4TextBox.BorderColor = Publicas._bordaSaida;
            Round4Usuario4TextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            decimal _total = Convert.ToDecimal(Round1Usuario4TextBox.Text) +
                Convert.ToDecimal(Round2Usuario4textBox.Text) +
                Convert.ToDecimal(Round3Usuario4TextBox.Text) +
                Convert.ToDecimal(Round4Usuario4TextBox.Text);

            Total4Usuario4TextBox.Text = _total.ToString();
        }

        private void SexoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                TorneioComboBox.Focus();
            }
        }

        private void AtivoCheckBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void NovaDataTimePickerAdv1_Validating(object sender, CancelEventArgs e)
        {

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }
    }
}
