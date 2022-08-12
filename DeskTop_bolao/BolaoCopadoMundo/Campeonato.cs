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

            dataDateTimePicker.BackColor = ColaboradorTextBox.BackColor;
            NovaDataTimePickerAdv1.BackColor = ColaboradorTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Torneio _torneio;
        Classes.PartidasDoTorneio _partidas;
        Classes.Colaboradores _colaborador;
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
                dataDateTimePicker.Focus();
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
            if (_partidas == null)
                _partidas = new PartidasDoTorneio();

            _partidas.Data = dataDateTimePicker.Value;
            _partidas.IdColaborador = _colaborador.Id;
            _partidas.IdEmpresa = _empresa.IdEmpresa;
            _partidas.IdTorneio = _torneio.Id;
            _partidas.NomePartida = NomePartidaTextBox.Text;
            _partidas.Round1 = Convert.ToDecimal(Round1TextBox.Text);
            _partidas.Round2 = Convert.ToDecimal(Round2TextBox.Text);
            _partidas.Round3 = Convert.ToDecimal(Round3TextBox.Text);
            _partidas.Round4 = Convert.ToDecimal(Round4TextBox.Text);
            _partidas.Total = Convert.ToDecimal(TotalTextBox.Text);

            if (!AtivoCheckBox.Checked)
            {
                if (!new TorneioBO().GravarPartidas(_partidas))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }
            else
            {
                _partidas.Data = NovaDataTimePickerAdv1.Value;

                if (!new TorneioBO().AlterarDataDaPartida(_partidas))
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
        }

        private void PesquisaColaboradorButton_Click(object sender, EventArgs e)
        {
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

            _listaPartidas = new TorneioBO().ListarPartidas(_torneio.Id, dataDateTimePicker.Value);
            _partidas = new PartidasDoTorneio();

            
            if (_listaPartidas.Count() != 0)
            {
                if (new Notificacoes.Mensagem("Existe partida para essa data." + Environment.NewLine + "Deseja consultá-las ?"
                    , Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                {
                    Publicas._idTorneio = _torneio.Id;
                    Publicas._dataPesquisa = dataDateTimePicker.Value;

                    new Pesquisas.ParticipantesTorneio().ShowDialog();

                    ColaboradorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                    if (Publicas._idRetornoPesquisa == 0)
                    {
                        dataDateTimePicker.Focus();
                        return;
                    }

                    _partidas = new TorneioBO().ConsultarPartida(Publicas._idRetornoPesquisa);
                }
            }

            if (_partidas != null)
            {
                NomePartidaTextBox.Text = _partidas.NomePartida;
                ColaboradorTextBox.Text = _partidas.IdColaborador.ToString();
                Round1TextBox.Text = _partidas.Round1.ToString();
                Round2TextBox.Text = _partidas.Round2.ToString();
                Round3TextBox.Text = _partidas.Round3.ToString();
                Round4TextBox.Text = _partidas.Round4.ToString();
                TotalTextBox.Text = _partidas.Total.ToString();

                if (ColaboradorTextBox.Text != "0")
                    ColaboradorTextBox_Validating(sender, e);

                NomePartidaTextBox.Focus();
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _partidas.Existe;
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
    }
}
