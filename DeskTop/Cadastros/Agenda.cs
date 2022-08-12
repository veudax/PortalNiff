using Classes;
using Negocio;
using Suportte.Notificacoes;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.Cadastros
{
    public partial class Agenda : Form
    {

        Classes.Empresa _empresa;
        Classes.SalaDeReuniao _sala;
        public Classes.Agenda _agenda = new Classes.Agenda();
        Classes.VeiculosGlobus _veiculos;
        Publicas.TipoAgenda _tipoAgenda;
        DateTime _dataInicio;
        DateTime _dataFim;

        List<Classes.Agenda> _listaAgenda;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasUsuario;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;
        List<Classes.ParticipanteDaAgenda> _listaParticipantes;
        List<Classes.Usuario> _listaUsuarios;


        public Agenda()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                dataDateTimePicker.BorderColor = Publicas._bordaSaida;
                dataDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
                
                dataFimDateTimePicker.BorderColor = Publicas._bordaSaida;
                dataFimDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
                
                horaInicioDateTimePicker.BorderColor = Publicas._bordaSaida;
                horaInicioDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
                
                horaFimDateTimePicker.BorderColor = Publicas._bordaSaida;
                horaFimDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
                

                if (Publicas._TemaBlack)
                {
                    dataDateTimePicker.Style = VisualStyle.Office2016Black;
                    dataFimDateTimePicker.Style = VisualStyle.Office2016Black;
                    horaInicioDateTimePicker.Style = VisualStyle.Office2016Black;
                    horaFimDateTimePicker.Style = VisualStyle.Office2016Black;

                    GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    participantesGridDataBoundGrid.ColorStyles = ColorStyles.Office2010Black;
                    participantesGridDataBoundGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    participantesGridDataBoundGrid.BackColor = Publicas._panelTitulo;
                }
            }

            _listaUsuarios = new UsuarioBO().ListarUsuarios(true);

            Publicas._mensagemSistema = string.Empty;
        }

        #region Move tela
        bool clicouNoPanel;
        int posIniX;
        int posIniY;

        public OfficeScrollBars GridOfficeScrollBars { get; }

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
            tipoAgendaComboBox.Items.Add(Publicas.GetDescription(Publicas.TipoAgenda.Ferias, "")); //6
            tipoAgendaComboBox.Items.Add(Publicas.GetDescription(Publicas.TipoAgenda.AtestadoMedico, "")); //7

            statusComboBox.Items.Add(Publicas.GetDescription(Publicas.StatusAgenda.Ativo, ""));
            statusComboBox.Items.Add(Publicas.GetDescription(Publicas.StatusAgenda.Cancelado, ""));
            statusComboBox.Items.Add(Publicas.GetDescription(Publicas.StatusAgenda.Reservado, ""));
            statusComboBox.Items.Add(Publicas.GetDescription(Publicas.StatusAgenda.SolicitacaoCarro, ""));
            statusComboBox.Items.Add(Publicas.GetDescription(Publicas.StatusAgenda.Finalizado, ""));

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                    TempoComboBox.Items.Add(i.ToString() + " minutos");
                else
                    TempoComboBox.Items.Add((i * 5).ToString() + " minutos");
            }
            TempoComboBox.Items.Add("30 minutos");

            for (int i = 1; i < 13; i++)
               TempoComboBox.Items.Add(i.ToString() + " horas");

            TempoComboBox.Items.Add("18 horas");

            TempoComboBox.SelectedIndex = 0;
            tipoAgendaComboBox.SelectedIndex = -1;

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
            TempoComboBox.SelectedIndex = _agenda.Lembrar;
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
                foreach (var item in _listaEmpresas.Where(w => w.IdEmpresa == _agenda.IdEmpresa))
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
                case Publicas.StatusAgenda.Reservado:
                    statusComboBox.SelectedIndex = 2;
                    break;
                case Publicas.StatusAgenda.SolicitacaoCarro:
                    statusComboBox.SelectedIndex = 3;
                    break;
                case Publicas.StatusAgenda.Finalizado:
                    statusComboBox.SelectedIndex = 4;
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
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void dataDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void dataFimDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void diaTodoCheckBox_KeyDown(object sender, KeyEventArgs e)
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

        private void lembrarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void horaInicioDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void horaFimDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void statusComboBox_KeyDown(object sender, KeyEventArgs e)
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

        private void minutosLembreteCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void informacaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void salaReuniaoTextBox_KeyDown(object sender, KeyEventArgs e)
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
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void veiculoTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void localTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void participantesGridDataBoundGrid_TableControlCurrentCellKeyDown(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlKeyEventArgs e)
        {
            
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

            try
            {
                if (tipoAgendaComboBox.SelectedIndex == 0 || tipoAgendaComboBox.SelectedIndex == 2 || tipoAgendaComboBox.SelectedIndex == 4)
                {
                    empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
                    empresaComboBoxAdv.DisplayMember = "CodigoeNome";
                }
                else
                {
                    if (_empresaDoColaborador.Count != 0)
                    {
                        _listaEmpresasAutorizadas = new List<Empresa>();
                        foreach (var item in _empresaDoColaborador.Where(w => w.Inicio != DateTime.MinValue.Date &&
                                                                              (w.Fim == DateTime.MinValue.Date || w.Fim <= DateTime.Now.Date)))
                        {
                            _listaEmpresasAutorizadas.AddRange(_listaEmpresas.Where(w => w.IdEmpresa == item.IdEmpresa));
                        }                        
                    }
                    
                    if (_listaEmpresasAutorizadas.Count == 0)
                    {
                        _listaEmpresasUsuario = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._usuario.Id);

                        foreach (var item in _listaEmpresasUsuario.Where(w => w.EmpresaAutoriza))
                        {
                            _listaEmpresasAutorizadas.AddRange(_listaEmpresas.Where(w => w.IdEmpresa == item.IdEmpresa));
                        }
                    }

                    empresaComboBoxAdv.DataSource = _listaEmpresasAutorizadas.OrderBy(o => o.CodigoeNome).ToList();
                    empresaComboBoxAdv.DisplayMember = "CodigoeNome";
                }

                foreach (var item in _listaEmpresas.Where(w => w.IdEmpresa == Publicas._usuario.IdEmpresa))
                {
                    for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
                    {
                        empresaComboBoxAdv.SelectedIndex = i;
                        if (empresaComboBoxAdv.Text == item.CodigoeNome)
                            break;
                    }
                }
            }
            catch { }
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

            if (dataFimDateTimePicker.Value.DayOfWeek == DayOfWeek.Friday)
                horaFimDateTimePicker.Value = dataFimDateTimePicker.Value.Date.AddHours(17);
            else
                horaFimDateTimePicker.Value = dataFimDateTimePicker.Value.Date.AddHours(18);

            horaInicioDateTimePicker.Value = dataDateTimePicker.Value.Date.AddHours(8);
        }

        private void lembrarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TempoComboBox.ReadOnly = !lembrarCheckBox.Checked;

            if (!lembrarCheckBox.Checked)
                TempoComboBox.SelectedIndex = 0;
        }
        #endregion

        private void tipoAgendaComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;


            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            switch (tipoAgendaComboBox.SelectedIndex)
            {
                case 0:
                    _tipoAgenda = Publicas.TipoAgenda.SalaDeReuniao;
                    break;
                case 1:
                    _tipoAgenda = Publicas.TipoAgenda.Carro;
                    break;
                case 2:
                    _tipoAgenda = Publicas.TipoAgenda.Visita;
                    break;
                case 3:
                    _tipoAgenda = Publicas.TipoAgenda.TreinamentoExterno;
                    break;
                case 4:
                    _tipoAgenda = Publicas.TipoAgenda.TreinamentoInterno;
                    break;
                case 5:
                    _tipoAgenda = Publicas.TipoAgenda.Particular;
                    break;
                case 6:
                    _tipoAgenda = Publicas.TipoAgenda.Ferias;
                    break;
                case 7:
                    _tipoAgenda = Publicas.TipoAgenda.AtestadoMedico;
                    break;
                default:
                    _tipoAgenda = Publicas.TipoAgenda.Todos;
                    break;
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

        private void informacaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;


            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void statusComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;


            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
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
                _listaAgenda = new AgendaBO().Consultar(dataDateTimePicker.Value.Date.AddHours(horaInicioDateTimePicker.Value.Hour).AddMinutes(horaInicioDateTimePicker.Value.Minute),
                                                    dataFimDateTimePicker.Value.Date.AddHours(horaFimDateTimePicker.Value.Hour).AddMinutes(horaFimDateTimePicker.Value.Minute),
                                                        _tipoAgenda,
                                                        diaTodoCheckBox.Checked);

                gravarButton.Enabled = true;

                foreach (Classes.Agenda item in _listaAgenda.Where(w => w.IdUsuario == Publicas._idUsuario))
                {
                    _agenda = item;
                    populaAgenda(item);
                }
            }
        }

        private void horaFimDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
                Publicas._escTeclado = false;
                return;
            }

            _dataInicio = dataDateTimePicker.Value.Date.AddHours(horaInicioDateTimePicker.Value.Hour).AddMinutes(horaInicioDateTimePicker.Value.Minute);
            _dataFim = dataFimDateTimePicker.Value.Date.AddHours(horaFimDateTimePicker.Value.Hour).AddMinutes(horaFimDateTimePicker.Value.Minute);

            _listaAgenda = new AgendaBO().Consultar(_dataInicio,
                                                    _dataFim,
                                                    _tipoAgenda,
                                                    diaTodoCheckBox.Checked);

            gravarButton.Enabled = true;

            foreach (Classes.Agenda item in _listaAgenda.Where(w => w.IdUsuario == Publicas._idUsuario))
            {
                _agenda = item;
                populaAgenda(item);
            }
            
        }

        private void salaReuniaoTextBox_Validating(object sender, CancelEventArgs e)
        {

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                return;
            }
            
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

            _listaAgenda = new AgendaBO().Consultar(_dataInicio,
                                                    _dataFim,
                                                    Publicas.TipoAgenda.SalaDeReuniao,
                                                    true);

            foreach (var item in _listaAgenda.Where(w => w.CodigoVeiculo == _veiculos.Id &&
                                                        ((_dataInicio >= w.Data && _dataInicio <= w.DataFimReal) ||
                                                         (_dataFim >= w.Data && _dataFim <= w.DataFimReal))))
            {
                new Notificacoes.Mensagem("Sala de reunião se encontra reservada para:" +
                    Environment.NewLine + item.Nome + " no horário " + item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() +
                    " até " + item.DataFim.ToShortDateString() + " " + item.DataFim.ToShortTimeString()
                    , Publicas.TipoMensagem.Alerta).ShowDialog();

                salaReuniaoTextBox.Text = string.Empty;
                salaReuniaoTextBox.Focus();
                return;
            }

            nomeSalaReuniaoTextBox.Text = _sala.Descricao;

        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_agenda == null)
                _agenda = new Classes.Agenda();

            switch (tipoAgendaComboBox.SelectedIndex)
            {
                case 0:
                    _tipoAgenda = Publicas.TipoAgenda.SalaDeReuniao;
                    break;
                case 1:
                    _tipoAgenda = Publicas.TipoAgenda.Carro;
                    break;
                case 2:
                    _tipoAgenda = Publicas.TipoAgenda.Visita;
                    break;
                case 3:
                    _tipoAgenda = Publicas.TipoAgenda.TreinamentoExterno;
                    break;
                case 4:
                    _tipoAgenda = Publicas.TipoAgenda.TreinamentoInterno;
                    break;
                case 5:
                    _tipoAgenda = Publicas.TipoAgenda.Particular;
                    break;
                case 6:
                    _tipoAgenda = Publicas.TipoAgenda.Ferias;
                    break;
                case 7:
                    _tipoAgenda = Publicas.TipoAgenda.AtestadoMedico;
                    break;
                default:
                    _tipoAgenda = Publicas.TipoAgenda.Todos;
                    break;
            }

            if (string.IsNullOrEmpty(informacaoTextBox.Text) && _tipoAgenda != Publicas.TipoAgenda.Ferias && _tipoAgenda != Publicas.TipoAgenda.AtestadoMedico)
            {
                new Notificacoes.Mensagem("Campo informações deve ser preenchido", Publicas.TipoMensagem.Alerta).ShowDialog();
                informacaoTextBox.Focus();
                return;
            }

            int loop = 1;

            DateTime _data = dataDateTimePicker.Value.Date;

            if ((_tipoAgenda == Publicas.TipoAgenda.Visita || _tipoAgenda == Publicas.TipoAgenda.TreinamentoExterno) &&
                _veiculos != null)
            {
                loop = 2;
            }

            for (int i = 0; i < loop; i++)
            {
                _data = dataDateTimePicker.Value.Date;

                while (_data <= dataFimDateTimePicker.Value.Date)
                {
                    if (_data != dataDateTimePicker.Value.Date)
                    {
                        _agenda = new Classes.Agenda();
                        _agenda.Data = _data.AddHours(8);
                        _agenda.DataFimReal = dataFimDateTimePicker.Value.Date.AddHours(horaFimDateTimePicker.Value.Hour).AddMinutes(horaFimDateTimePicker.Value.Minute);

                        if (_data == dataFimDateTimePicker.Value.Date)
                            _agenda.DataFim = _data.AddHours(horaFimDateTimePicker.Value.Hour).AddMinutes(horaFimDateTimePicker.Value.Minute);
                        else
                        {
                            if (_data.DayOfWeek == DayOfWeek.Friday)
                                _agenda.DataFim = _data.AddHours(17);
                            else
                                _agenda.DataFim = _data.AddHours(18);
                        }
                    }
                    else
                    {
                        _agenda.Data = _data.AddHours(horaInicioDateTimePicker.Value.Hour).AddMinutes(horaInicioDateTimePicker.Value.Minute);
                        _agenda.DataFimReal = dataFimDateTimePicker.Value.Date.AddHours(horaFimDateTimePicker.Value.Hour).AddMinutes(horaFimDateTimePicker.Value.Minute);

                        if (_data == dataFimDateTimePicker.Value.Date)
                            _agenda.DataFim = _data.AddHours(horaFimDateTimePicker.Value.Hour).AddMinutes(horaFimDateTimePicker.Value.Minute);
                        else
                        {
                            if (_data.DayOfWeek == DayOfWeek.Friday)
                                _agenda.DataFim = _data.AddHours(17);
                            else
                                _agenda.DataFim = _data.AddHours(18);
                        }
                    }

                    //_agenda.Data = dataDateTimePicker.Value.Date.AddHours(horaInicioDateTimePicker.Value.Hour).AddMinutes(horaInicioDateTimePicker.Value.Minute).AddSeconds(horaInicioDateTimePicker.Value.Second);
                    //_agenda.DataFim = dataFimDateTimePicker.Value.Date.AddHours(horaFimDateTimePicker.Value.Hour).AddMinutes(horaFimDateTimePicker.Value.Minute).AddSeconds(horaFimDateTimePicker.Value.Second);

                    if (!string.IsNullOrEmpty(salaReuniaoTextBox.Text.Trim()))
                    {
                        _agenda.IdSala = Convert.ToInt32(salaReuniaoTextBox.Text);
                    }

                    if (_tipoAgenda == Publicas.TipoAgenda.Carro || i == 1)
                    {
                        if (!string.IsNullOrEmpty(veiculoTextBoxExt.Text.Trim()))
                        {
                            _agenda.CodigoVeiculo = _veiculos.Id;
                        }
                    }

                    _agenda.Texto = informacaoTextBox.Text;
                    _agenda.DiaTodo = diaTodoCheckBox.Checked;
                    _agenda.Local = localTextBoxExt.Text;

                    _agenda.TipoAgenda = (i == 0 ? _tipoAgenda : Publicas.TipoAgenda.Carro);

                    _agenda.IdUsuario = Publicas._idUsuario;

                    if (i == 1)
                        _agenda.Status = Publicas.StatusAgenda.SolicitacaoCarro;
                    else
                        _agenda.Status = (statusComboBox.SelectedIndex == 0 ? Publicas.StatusAgenda.Ativo :
                                          (statusComboBox.SelectedIndex == 1 ? Publicas.StatusAgenda.Cancelado :
                                          (statusComboBox.SelectedIndex == 2 ? Publicas.StatusAgenda.Reservado :
                                          (statusComboBox.SelectedIndex == 3 ? Publicas.StatusAgenda.SolicitacaoCarro :
                                      Publicas.StatusAgenda.Finalizado))));

                    try
                    {
                        _agenda.Lembrar = TempoComboBox.SelectedIndex;
                    }
                    catch
                    {
                        _agenda.Lembrar = 0;
                    }

                    if (_agenda.Lembrar != 0)
                    {
                        if (TempoComboBox.SelectedIndex <= 4)
                            _agenda.DataLembrete = _agenda.Data.AddMinutes(-Convert.ToDouble(Publicas.OnlyNumbers(TempoComboBox.Text)));
                        else
                            _agenda.DataLembrete = _agenda.Data.AddHours(-Convert.ToDouble(Publicas.OnlyNumbers(TempoComboBox.Text)));
                    }

                    try
                    {
                        _agenda.IdEmpresa = _empresa.IdEmpresa;
                    }
                    catch { }

                    if (!new AgendaBO().Gravar(_agenda, _listaParticipantes.Where(w => w.Avisado || w.Marcado).ToList()))
                    {
                        new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }
                    _data = _data.AddDays(1);
                }
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
            TempoComboBox.SelectedIndex = 0;
            informacaoTextBox.Text = string.Empty;
            salaReuniaoTextBox.Text = string.Empty;
            nomeSalaReuniaoTextBox.Text = string.Empty;
            veiculoTextBoxExt.Text = string.Empty;
            placaTextBoxExt.Text = string.Empty;
            localTextBoxExt.Text = string.Empty;
            empresaComboBoxAdv.SelectedIndex = -1;
            _agenda = null;
            _veiculos = null;
            _sala = null;

            // participantes
            _listaParticipantes.ForEach(w => { w.Marcado = false; w.Avisado = false; });

            tipoAgendaComboBox.Focus();
        }

        private void veiculoTextBoxExt_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

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

            if (veiculoTextBoxExt.Text.Trim() == "")
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                new Pesquisas.Veiculos().ShowDialog();

                veiculoTextBoxExt.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (veiculoTextBoxExt.Text.Trim() == "" || veiculoTextBoxExt.Text == "0")
                {
                    veiculoTextBoxExt.Text = string.Empty;
                    veiculoTextBoxExt.Focus();
                    return;
                }
            }

            _veiculos = new VeiculosGlobusBO().Consultar(_empresa.CodigoEmpresaGlobus, veiculoTextBoxExt.Text);

            if (!_veiculos.Existe)
            {
                new Notificacoes.Mensagem("Veículo não cadastrado.", Publicas.TipoMensagem.Alerta);
                veiculoTextBoxExt.Text = string.Empty;
                veiculoTextBoxExt.Focus();
                return;
            }

            if (!_veiculos.Ativo)
            {
                new Notificacoes.Mensagem("Veículo não está ativo.", Publicas.TipoMensagem.Alerta);
                veiculoTextBoxExt.Text = string.Empty;
                veiculoTextBoxExt.Focus();
                return;
            }

            _listaAgenda = new AgendaBO().Consultar(_dataInicio,
                                                    _dataFim,
                                                    Publicas.TipoAgenda.Carro,
                                                    true); // para buscar o dia 

            foreach (var item in _listaAgenda.Where(w => w.CodigoVeiculo == _veiculos.Id &&
                                                        ((_dataInicio >= w.Data && _dataInicio <= w.DataFimReal) ||
                                                         (_dataFim >= w.Data && _dataFim <= w.DataFimReal))))
            {
                new Notificacoes.Mensagem("Veículo se encontra reservado para: " +
                    Environment.NewLine + item.Nome + " no horário " + item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + 
                    " até " + item.DataFimReal.ToShortDateString() + " " + item.DataFimReal.ToShortTimeString()
                    , Publicas.TipoMensagem.Alerta).ShowDialog();
                veiculoTextBoxExt.Text = string.Empty;
                veiculoTextBoxExt.Focus();
                return;
            }

            placaTextBoxExt.Text = _veiculos.Placa;  
        }

        private void Agenda_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            _empresaDoColaborador = new ColaboradoresBO().Listar(Publicas._idColaborador);

            participantesGridDataBoundGrid.SortIconPlacement = SortIconPlacement.Left;
            participantesGridDataBoundGrid.TopLevelGroupOptions.ShowFilterBar = false;
            participantesGridDataBoundGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            participantesGridDataBoundGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < participantesGridDataBoundGrid.TableDescriptor.Columns.Count; i++)
            {
                participantesGridDataBoundGrid.TableDescriptor.Columns[i].AllowFilter = false;
                participantesGridDataBoundGrid.TableDescriptor.Columns[i].ReadOnly = false;
                participantesGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                participantesGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                participantesGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            GridMetroColors metroColor = new GridMetroColors();
            metroColor.HeaderBottomBorderColor = Publicas._bordaEntrada;
            metroColor.HeaderBottomBorderWeight = GridBottomBorderWeight.ExtraThin;

            metroColor.HeaderColor.HoverColor = Publicas._bordaEntrada;
            metroColor.HeaderColor.PressedColor = Publicas._bordaEntrada;

            metroColor.CheckBoxColor.BorderColor = Publicas._bordaEntrada;
            metroColor.PushButtonColor.PushedBackColor = Publicas._bordaEntrada;
            metroColor.PushButtonColor.HoverBackColor = Publicas._bordaEntrada;
            metroColor.PushButtonColor.NormalBackColor = Color.WhiteSmoke;
            metroColor.ComboboxColor.NormalBorderColor = Publicas._bordaEntrada;
            metroColor.ComboboxColor.HoverBorderColor = Publicas._bordaEntrada;
            metroColor.ComboboxColor.HoverBackColor = Publicas._bordaEntrada;
            metroColor.ComboboxColor.PressedBackColor = Publicas._bordaEntrada;
            metroColor.ComboboxColor.NormalBackColor = Color.WhiteSmoke;

            if (!Publicas._TemaBlack)
            {
                this.participantesGridDataBoundGrid.SetMetroStyle(metroColor);
                this.participantesGridDataBoundGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.participantesGridDataBoundGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.participantesGridDataBoundGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.participantesGridDataBoundGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.participantesGridDataBoundGrid.Table.DefaultCaptionRowHeight = 23;

            _listaParticipantes = new List<ParticipanteDaAgenda>();

            foreach (var item in _listaUsuarios)
            {
                _listaParticipantes.Add(new ParticipanteDaAgenda()
                {
                    IdUsuario = item.Id,
                    Nome = item.Nome,
                    Empresa = item.Empresa
                });
            }

            if (_agenda != null && _agenda.Existe)
            {
                tipoAgendaComboBox.SelectedIndex = (_agenda.TipoAgenda == Publicas.TipoAgenda.SalaDeReuniao ? 0 :
                    (_agenda.TipoAgenda == Publicas.TipoAgenda.Carro ? 1 :
                    (_agenda.TipoAgenda == Publicas.TipoAgenda.Visita ? 2 :
                    (_agenda.TipoAgenda == Publicas.TipoAgenda.TreinamentoExterno ? 3 :
                    (_agenda.TipoAgenda == Publicas.TipoAgenda.TreinamentoInterno ? 4 :
                    (_agenda.TipoAgenda == Publicas.TipoAgenda.Particular ? 5 :
                    (_agenda.TipoAgenda == Publicas.TipoAgenda.Ferias ? 6 : 7
                    )))))));

                horaFimDateTimePicker.Value = _agenda.HoraFim;
                horaInicioDateTimePicker.Value = _agenda.HoraInicio;
                populaAgenda(_agenda);
            }
            else
            {
                tipoAgendaComboBox.SelectedIndex = -1;
                tipoAgendaComboBox.Focus();
            }
        }

        private void tipoAgendaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            salaReuniaoTextBox.Enabled = tipoAgendaComboBox.SelectedIndex == 0 || tipoAgendaComboBox.SelectedIndex == 4;
            veiculoTextBoxExt.Enabled = tipoAgendaComboBox.SelectedIndex == 1 || tipoAgendaComboBox.SelectedIndex == 2 || tipoAgendaComboBox.SelectedIndex == 3;
            empresaComboBoxAdv.TabStop = tipoAgendaComboBox.SelectedIndex < 6;
            participantesGridDataBoundGrid.Enabled = tipoAgendaComboBox.SelectedIndex == 0 || tipoAgendaComboBox.SelectedIndex == 1 || tipoAgendaComboBox.SelectedIndex == 4 || tipoAgendaComboBox.SelectedIndex == 5;
            localTextBoxExt.Enabled = tipoAgendaComboBox.SelectedIndex == 1 || tipoAgendaComboBox.SelectedIndex == 3 || tipoAgendaComboBox.SelectedIndex == 3;

            if (participantesGridDataBoundGrid.Enabled)
                participantesGridDataBoundGrid.DataSource = _listaParticipantes;

            statusComboBox.ReadOnly = false;

            if (tipoAgendaComboBox.SelectedIndex == 1)
            {
                statusComboBox.SelectedIndex = 3;
                statusComboBox.ReadOnly = true;
            }
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
            
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            try
            {
                foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
                {
                    _empresa = item;
                }
            }
            catch { }
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new AgendaBO().Excluir(_agenda.IdAgenda))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu a agenda " + _agenda.DescricaoTipoAgenda + " de " +
                _agenda.Data.ToString() + " até " + _agenda.DataFim.ToString() + " " +
                _agenda.Texto;

            _log.Tela = "TI - Cadastros - Agendas";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }
    }
}
