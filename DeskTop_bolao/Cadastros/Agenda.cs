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
    public partial class Agenda : Form
    {

        List<Empresa> _empresas;
        Classes.SalaDeReuniao _sala;
        Classes.Agenda _agenda = new Classes.Agenda();
        List<Classes.Agenda> _listaAgenda;
        Publicas.TipoAgenda _tipoAgenda;

        public Agenda()
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

        private void Agenda_Load(object sender, EventArgs e)
        {
            tipoAgendaComboBox.Items.Add(Publicas.GetDescription(Publicas.TipoAgenda.SalaDeReuniao, "")); //0
            tipoAgendaComboBox.Items.Add(Publicas.GetDescription(Publicas.TipoAgenda.Carro, "")); //1
            tipoAgendaComboBox.Items.Add(Publicas.GetDescription(Publicas.TipoAgenda.Visita, "")); //2
            tipoAgendaComboBox.Items.Add(Publicas.GetDescription(Publicas.TipoAgenda.TreinamentoExterno, "")); //3
            tipoAgendaComboBox.Items.Add(Publicas.GetDescription(Publicas.TipoAgenda.TreinamentoInterno, "")); //4
            tipoAgendaComboBox.Items.Add(Publicas.GetDescription(Publicas.TipoAgenda.Particular, "")); //5

            statusComboBox.Items.Add(Publicas.GetDescription(Publicas.StatusAgenda.Ativo, ""));
            statusComboBox.Items.Add(Publicas.GetDescription(Publicas.StatusAgenda.Cancelado, ""));
            tipoAgendaComboBox.SelectedIndex = 0;

            _empresas = new EmpresaBO().Listar();

            empresaComboBoxAdv.DataSource = _empresas;
            empresaComboBoxAdv.DisplayMember = "Nome";
            empresaComboBoxAdv.SelectedIndex = -1;

            dataDateTimePicker.Value = DateTime.Now;
            dataFimDateTimePicker.Value = DateTime.Now;
            horaFimDateTimePicker.Value = DateTime.Now;
            horaInicioDateTimePicker.Value = DateTime.Now;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void populaAgenda(Classes.Agenda _agenda)
        {
            minutosLembreteCurrencyTextBox.DecimalValue = _agenda.Lembrar;
            informacaoTextBox.Text = _agenda.Texto;
            localTextBoxExt.Text = _agenda.Local;

            if (_agenda.IdSala != 0)
            {
                salaReuniaoTextBox.Text = _agenda.IdSala.ToString();
                _sala = new SalaDeReuniaoBO().Consultar(Convert.ToInt32(salaReuniaoTextBox.Text));
                nomeSalaReuniaoTextBox.Text = _sala.Descricao;
            }

            if (_agenda.CodigoVeiculo != 0)
                veiculoTextBoxExt.Text = _agenda.Prefixo;

            if (_agenda.IdEmpresa != 0)
            {
                foreach (var item in _empresas.Where(w => w.IdEmpresa == _agenda.IdEmpresa))
                {
                    for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
                    {
                        empresaComboBoxAdv.SelectedIndex = i;
                        if (empresaComboBoxAdv.Text == item.Nome)
                            break;
                    }
                }
            }

            switch (_agenda.Status)
            {
                case Publicas.StatusAgenda.Ativo:
                    statusComboBox.SelectedIndex = 0;
                    break;
                case Publicas.StatusAgenda.Cancelado:
                    statusComboBox.SelectedIndex = 1;
                    break;
            }

            excluirButton.Enabled = _agenda.Existe;
            // buscar os participantes

        }

        #region KeyDown
        private void tipoAgendaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void dataDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataFimDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoAgendaComboBox.Focus();
            }
        }

        private void dataFimDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                diaTodoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataDateTimePicker.Focus();
            }
        }

        private void diaTodoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                lembrarCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataFimDateTimePicker.Focus();
            }
        }

        private void lembrarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                horaInicioDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                diaTodoCheckBox.Focus();
            }
        }

        private void horaInicioDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                horaFimDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                lembrarCheckBox.Focus();
            }
        }

        private void horaFimDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                statusComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (horaInicioDateTimePicker.Enabled)
                    horaInicioDateTimePicker.Focus();
                else
                    statusComboBox.Focus();
            }
        }

        private void statusComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (minutosLembreteCurrencyTextBox.Enabled)
                    minutosLembreteCurrencyTextBox.Focus();
                else
                    informacaoTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                horaFimDateTimePicker.Focus();
            }
        }

        private void minutosLembreteCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                informacaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                statusComboBox.Focus();
            }
        }

        private void informacaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            { 
                if (salaReuniaoTextBox.Enabled)
                    salaReuniaoTextBox.Focus();
                else
                {
                    if (veiculoTextBoxExt.Enabled)
                        veiculoTextBoxExt.Focus();
                    else
                        empresaComboBoxAdv.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (minutosLembreteCurrencyTextBox.Enabled)
                    minutosLembreteCurrencyTextBox.Focus();
                else
                    informacaoTextBox.Focus();
            }
        }

        private void salaReuniaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                participantesGridDataBoundGrid.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                informacaoTextBox.Focus();
            }
        }

        private void veiculoTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (empresaComboBoxAdv.Enabled)
                    empresaComboBoxAdv.Focus();
                else
                    localTextBoxExt.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                informacaoTextBox.Focus();
            }
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                localTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (veiculoTextBoxExt.Enabled)
                    veiculoTextBoxExt.Focus();
                else
                    informacaoTextBox.Focus();
            }
        }

        private void localTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (empresaComboBoxAdv.Enabled)
                    empresaComboBoxAdv.Focus();
                else
                {
                    if (veiculoTextBoxExt.Enabled)
                        veiculoTextBoxExt.Focus();
                    else
                        informacaoTextBox.Focus();
                }
            }
        }

        private void participantesGridDataBoundGrid_TableControlCurrentCellKeyDown(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlKeyEventArgs e)
        {
            if (e.Inner.KeyCode == Keys.Enter || e.Inner.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.Inner.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                salaReuniaoTextBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                limparButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (participantesGridDataBoundGrid.Enabled)
                    participantesGridDataBoundGrid.Focus();
                else
                    localTextBoxExt.Focus();
            }
        }

        private void limparButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                excluirButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gravarButton.Focus();
            }
        }

        private void excluirButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                limparButton.Focus();
            }
        }

        #endregion

        #region Enter
        private void tipoAgendaComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void dataDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void informacaoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void minutosLembreteCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        #endregion

        #region Change
        private void diaTodoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            horaFimDateTimePicker.Enabled = !diaTodoCheckBox.Checked;
            horaInicioDateTimePicker.Enabled = !diaTodoCheckBox.Checked;
        }

        private void lembrarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            minutosLembreteCurrencyTextBox.Enabled = lembrarCheckBox.Checked;

            if (!lembrarCheckBox.Checked)
                minutosLembreteCurrencyTextBox.DecimalValue = 0;
        }
        #endregion

        private void tipoAgendaComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (tipoAgendaComboBox.SelectedIndex == 0) // sala de reunião
            {
                _tipoAgenda = Publicas.TipoAgenda.SalaDeReuniao;
                salaReuniaoTextBox.Enabled = true;
                participantesGridDataBoundGrid.Enabled = true;
                veiculoTextBoxExt.Enabled = false;
                placaTextBoxExt.Enabled = false;
                empresaComboBoxAdv.Enabled = false;
                localLabel.Enabled = false;
            }
            if (tipoAgendaComboBox.SelectedIndex == 1) // veículo
            {
                _tipoAgenda = Publicas.TipoAgenda.Carro;
                veiculoTextBoxExt.Enabled = true;
                placaTextBoxExt.Enabled = false;
                salaReuniaoTextBox.Enabled = false;
                participantesGridDataBoundGrid.Enabled = false;
                empresaComboBoxAdv.Enabled = false;
                localLabel.Enabled = true;
            }
            if (tipoAgendaComboBox.SelectedIndex == 2) // visita
            {
                _tipoAgenda = Publicas.TipoAgenda.Visita;
                salaReuniaoTextBox.Enabled = false;
                empresaComboBoxAdv.Enabled = true;
                participantesGridDataBoundGrid.Enabled = false;
                veiculoTextBoxExt.Enabled = true;
                placaTextBoxExt.Enabled = false;
                localLabel.Enabled = true;
            }
            if (tipoAgendaComboBox.SelectedIndex == 4) // treinamento interno
            {
                _tipoAgenda = Publicas.TipoAgenda.TreinamentoInterno;
                salaReuniaoTextBox.Enabled = true;
                participantesGridDataBoundGrid.Enabled = true;
                veiculoTextBoxExt.Enabled = false;
                placaTextBoxExt.Enabled = false;
                localLabel.Enabled = false;
                empresaComboBoxAdv.Enabled = false;
            }
            if (tipoAgendaComboBox.SelectedIndex == 3) // treinamento externo
            {
                _tipoAgenda = Publicas.TipoAgenda.TreinamentoExterno;
                empresaComboBoxAdv.Enabled = false;
                salaReuniaoTextBox.Enabled = false;
                participantesGridDataBoundGrid.Enabled = false;
                veiculoTextBoxExt.Enabled = false;
                placaTextBoxExt.Enabled = false;
                localLabel.Enabled = true;
            }

            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void dataDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
        }

        private void informacaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void statusComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void diaTodoCheckBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (diaTodoCheckBox.Checked)
            {
                _listaAgenda = new AgendaBO().Consultar(dataDateTimePicker.Value.Date.AddHours(horaInicioDateTimePicker.Value.Hour).AddMinutes(horaInicioDateTimePicker.Value.Minute).AddSeconds(horaInicioDateTimePicker.Value.Second),
                                                    dataFimDateTimePicker.Value.Date.AddHours(horaFimDateTimePicker.Value.Hour).AddMinutes(horaFimDateTimePicker.Value.Minute).AddSeconds(horaFimDateTimePicker.Value.Second),
                                                        _tipoAgenda,
                                                        diaTodoCheckBox.Checked);

                gravarButton.Enabled = true;

                foreach (Classes.Agenda item in _listaAgenda.Where(w => w.IdUsuario == Publicas._idUsuario))
                {
                    populaAgenda(item);
                }
            }
        }

        private void horaFimDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
                Publicas._escTeclado = false;
                return;
            }

            _listaAgenda = new AgendaBO().Consultar(dataDateTimePicker.Value.Date.AddHours(horaInicioDateTimePicker.Value.Hour).AddMinutes(horaInicioDateTimePicker.Value.Minute).AddSeconds(horaInicioDateTimePicker.Value.Second),
                                                    dataFimDateTimePicker.Value.Date.AddHours(horaFimDateTimePicker.Value.Hour).AddMinutes(horaFimDateTimePicker.Value.Minute).AddSeconds(horaFimDateTimePicker.Value.Second),
                                                        _tipoAgenda,
                                                        diaTodoCheckBox.Checked);

            gravarButton.Enabled = true;

            foreach (Classes.Agenda item in _listaAgenda.Where(w => w.IdUsuario == Publicas._idUsuario))
            {
                populaAgenda(item);
            }

            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
        }

        private void salaReuniaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
                Publicas._escTeclado = false;
                return;
            }

            if (salaReuniaoTextBox.Text.Trim() == "")
            {
                new Pesquisas.SalaDeReuniao().ShowDialog();

                salaReuniaoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (salaReuniaoTextBox.Text.Trim() == "" || salaReuniaoTextBox.Text == "0")
                {
                    salaReuniaoTextBox.Text = string.Empty;
                    salaReuniaoTextBox.Focus();
                    return;
                }
            }

            _sala = new SalaDeReuniaoBO().Consultar(Convert.ToInt32(salaReuniaoTextBox.Text));

            if (!_sala.Existe)
            {
                new Notificacoes.Mensagem("Sala de reunião não cadastrada.", Publicas.TipoMensagem.Alerta);
                salaReuniaoTextBox.Text = string.Empty;
                salaReuniaoTextBox.Focus();
                return;
            }

            if (!_sala.Ativo)
            {
                new Notificacoes.Mensagem("Sala de reunião inativa.", Publicas.TipoMensagem.Alerta);
                salaReuniaoTextBox.Text = string.Empty;
                salaReuniaoTextBox.Focus();
                return;
            }

            nomeSalaReuniaoTextBox.Text = _sala.Descricao;

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(informacaoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o campo informção!", Publicas.TipoMensagem.Alerta).ShowDialog();
                informacaoTextBox.Focus();
                return;
            }

            _agenda.Data = dataDateTimePicker.Value.AddHours(horaInicioDateTimePicker.Value.Hour).AddMinutes(horaInicioDateTimePicker.Value.Minute).AddSeconds(horaInicioDateTimePicker.Value.Second);
            _agenda.DataFim = dataFimDateTimePicker.Value.AddHours(horaFimDateTimePicker.Value.Hour).AddMinutes(horaFimDateTimePicker.Value.Minute).AddSeconds(horaFimDateTimePicker.Value.Second);

            _agenda.IdSala = Convert.ToInt32(salaReuniaoTextBox.Text);
            _agenda.Texto = informacaoTextBox.Text;
            _agenda.DiaTodo = diaTodoCheckBox.Checked;
            _agenda.Local = localTextBoxExt.Text;
            _agenda.TipoAgenda = _tipoAgenda;
            _agenda.IdUsuario = Publicas._idUsuario;

            _agenda.Status = (statusComboBox.SelectedIndex == 0 ? Publicas.StatusAgenda.Ativo : Publicas.StatusAgenda.Cancelado);

            try
            {
                _agenda.Lembrar = Convert.ToInt32(minutosLembreteCurrencyTextBox.DecimalValue);
            }
            catch
            {
                _agenda.Lembrar = 0;
            }
            
            foreach (var item in _empresas.Where(w => w.Nome == empresaComboBoxAdv.Text))
            {
                _agenda.IdEmpresa = item.IdEmpresa;
            }

            if (!new AgendaBO().Gravar(_agenda, new List<ParticipanteDaAgenda>()))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            tipoAgendaComboBox.SelectedIndex = -1;
            dataDateTimePicker.Value = DateTime.Now;
            dataFimDateTimePicker.Value = DateTime.Now;
            horaFimDateTimePicker.Value = DateTime.Now;
            horaInicioDateTimePicker.Value = DateTime.Now;
            diaTodoCheckBox.Checked = false;
            lembrarCheckBox.Checked = true;
            statusComboBox.SelectedIndex = -1;
            minutosLembreteCurrencyTextBox.DecimalValue = 0;
            informacaoTextBox.Text = string.Empty;
            salaReuniaoTextBox.Text = string.Empty;
            nomeSalaReuniaoTextBox.Text = string.Empty;
            veiculoTextBoxExt.Text = string.Empty;
            placaTextBoxExt.Text = string.Empty;
            localTextBoxExt.Text = string.Empty;
            empresaComboBoxAdv.SelectedIndex = -1;

            // participantes

            tipoAgendaComboBox.Focus();
        }

        private void veiculoTextBoxExt_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
                Publicas._escTeclado = false;
                return;
            }

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }
    }
}
