using Classes;
using Negocio;
using Syncfusion.Grouping;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace Suportte.Cadastros
{
    public partial class ControleDeHoras : Form
    {
        public ControleDeHoras()
        {
            InitializeComponent();

            dataDateTimePicker.Value = DateTime.Now;
            dataDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
            AtestadoCheckBox.ForeColor = dataDateTimePicker.ForeColor;
            DeclaracaoCheckBox.ForeColor = dataDateTimePicker.ForeColor;
            AusenciaCheckBox.ForeColor = dataDateTimePicker.ForeColor;
            ReducaoCheckBox.ForeColor = dataDateTimePicker.ForeColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                {
                    gridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl.BackColor = Publicas._panelTitulo;
                    dataDateTimePicker.Style = VisualStyle.Office2016Black;
                    AtestadoCheckBox.ForeColor = Publicas._fonte;
                    DeclaracaoCheckBox.ForeColor = Publicas._fonte;
                    CompensacaoCheckBox.ForeColor = Publicas._fonte;
                    AusenciaCheckBox.ForeColor = Publicas._fonte;
                    ReducaoCheckBox.ForeColor = Publicas._fonte;
                }
            }
            Publicas._mensagemSistema = string.Empty;

        }

        #region Atributos
        Classes.Empresa _empresa;
        Classes.Colaboradores _colaborador;
        Classes.PeriodoBancoHorasColaborador _periodo;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.PeriodoBancoHorasColaborador> _listaPeriodo;
        List<Classes.ControleDeHoras> _listaHorarios;
        bool temAlteracao = false;
        #endregion

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

        private void ControleDeHoras_Shown(object sender, EventArgs e)
        {
            gridGroupingControl.DataSource = new List<Classes.ControleDeHoras>();
            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.TableControl.CellToolTip.Active = true;

            if (!Publicas._TemaBlack)
            {
                GridMetroColors metroColor = new GridMetroColors();

                metroColor.HeaderBottomBorderColor = Publicas._bordaEntrada;
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

                this.gridGroupingControl.SetMetroStyle(metroColor);
                this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
                this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            }

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl.Table.DefaultRecordRowHeight = 20;

            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);
            _listaPeriodo = new PeriodoBancoHorasColaboradorBO().Listar(Publicas._idColaborador, true);

            for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
            {
                empresaComboBoxAdv.SelectedIndex = i;
                if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                {
                    break;
                }
            }

            if (DateTime.Now.Date <= Convert.ToDateTime("22/01/" + DateTime.Now.Year))
            {
                PeriodoBancoHorasColaborador _per = new PeriodoBancoHorasColaborador();
                _per.Inicio = Convert.ToDateTime("21/11/" + (DateTime.Now.Year - 1).ToString());
                _per.Fim = Convert.ToDateTime("20/01/" + DateTime.Now.Year.ToString());
                _listaPeriodo.Add(_per);
            }

            foreach (var item in _listaPeriodo)
            {
                PeriodoComboBox.Items.Add(item.Inicio.ToShortDateString() + " até " + item.Fim.ToShortDateString());
            }

            
            ColaboradorTextBox.Text = Publicas._idColaborador.ToString();
            //_colaborador = new ColaboradoresBO().ConsultaColaborador(39);
            _colaborador = new ColaboradoresBO().ConsultaColaborador(Publicas._idColaborador);
            NomeColaboradorTextBox.Text = _colaborador.Nome;
            PeriodoComboBox.Focus();

            if (Publicas._usuario.UsuarioAcesso == "FFLOPES" || Publicas._usuario.Administrador)
            {
                ColaboradorTextBox.Enabled = true;
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            if (temAlteracao)
            {
                if (new Notificacoes.Mensagem("Deseja realmente fechar a tela?" + Environment.NewLine +
                    "Existem alterações não gravadas ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                    Close();
            }
            else
                Close();
        }

        private void PeriodoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (ColaboradorTextBox.Enabled)
                    ColaboradorTextBox.Focus();
                else
                    PeriodoComboBox.Focus();
            }
        }

        private void dataDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void EntradaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void SaidaAlmocoMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            EntradaMaskedEditBox_KeyDown(sender, e);
        }

        private void VoltaAlmocoMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            EntradaMaskedEditBox_KeyDown(sender, e);
        }

        private void SaidaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            EntradaMaskedEditBox_KeyDown(sender, e);
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            if (_listaHorarios == null)
                _listaHorarios = new List<Classes.ControleDeHoras>();

            bool temDia = false;
            DateTime entrada = dataDateTimePicker.Value.Date;
            DateTime saida = dataDateTimePicker.Value.Date;
            DateTime almoco = dataDateTimePicker.Value.Date;
            DateTime volta = dataDateTimePicker.Value.Date;
            DateTime saidaMaxima;
            DateTime saidaMinima;

            DateTime entradaExata ;
            DateTime entradaMinima;
            DateTime entradaMaxima;
            DateTime inicioAlmoco;
            DateTime fimAlmoco;

            if (!ReducaoCheckBox.Checked || PeriodoReducaoComboBox.SelectedIndex == -1 || 
                PeriodoReducaoComboBox.SelectedIndex == 0 || PeriodoReducaoComboBox.SelectedIndex == 2)
            {
                entradaExata = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 8, 00, 0);
                entradaMinima = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 7, 55, 0);
                entradaMaxima = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 8, 5, 0);
                inicioAlmoco = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 12, 0, 0);
                fimAlmoco = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 13, 0, 0);
            }
            else
            {
                entradaExata = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 10, 00, 0);
                entradaMinima = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 9, 55, 0);
                entradaMaxima = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 10, 5, 0);
                inicioAlmoco = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 13, 0, 0);
                fimAlmoco = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 14, 0, 0);
            }


            if (!ReducaoCheckBox.Checked || PeriodoReducaoComboBox.SelectedIndex == -1 ||
                 PeriodoReducaoComboBox.SelectedIndex == 1 || PeriodoReducaoComboBox.SelectedIndex == 3)
            {
                if (dataDateTimePicker.Value.DayOfWeek != DayOfWeek.Friday)
                {
                    saidaMaxima = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 18, 5, 0);
                    saidaMinima = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 18, 0, 0);
                }
                else
                {
                    saidaMaxima = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 17, 5, 0);
                    saidaMinima = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 17, 0, 0);
                }
            }
            else
            {
                if (dataDateTimePicker.Value.DayOfWeek != DayOfWeek.Friday)
                {
                    saidaMaxima = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 15, 50, 0);
                    saidaMinima = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 15, 45, 0);
                }
                else
                {
                    saidaMaxima = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 15, 05, 0);
                    saidaMinima = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day, 15, 0, 0);
                }
            }

            double extraEntrada = 0;
            double extraAlmoco = 0;
            double extraVolta = 0;
            double extraSaida = 0;
            double incEntrada = 0;
            double incAlmoco = 0;
            double incVolta = 0;
            double incSaida = 0;
            double _calExtra = 0;
            double _calIncompletas = 0;
            double _totalExtra = 0;
            double _totalIncompleta = 0;

            gridGroupingControl.DataSource = new List<Classes.ControleDeHoras>();

            Classes.ControleDeHoras _horas = new Classes.ControleDeHoras();

            if (_listaHorarios.Where(w => w.Data == dataDateTimePicker.Value.Date).Count() > 0)
            {
                if (new Notificacoes.Mensagem("Existe horário para esta data." + Environment.NewLine +
                    "Deseja alterar ? ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                {
                    foreach (var item in _listaHorarios.Where(w => w.Data == dataDateTimePicker.Value.Date))
                    {
                        temDia = true;
                        _horas = item;
                    }
                }
            }

            _horas.Entrada = EntradaMaskedEditBox.Text;
            _horas.SaidaAlmoco = SaidaAlmocoMaskedEditBox.Text;
            _horas.VoltaAlmoco = VoltaAlmocoMaskedEditBox.Text;
            _horas.Saida = SaidaMaskedEditBox.Text;
            _horas.Atestado = AtestadoCheckBox.Checked;
            _horas.Declaracao = DeclaracaoCheckBox.Checked;
            _horas.Compensacao = CompensacaoCheckBox.Checked;
            _horas.Ausencia = AusenciaCheckBox.Checked;
            try
            {
                _horas.Motivo = MotivoTextBox.Text + (_horas.Motivo.Contains("redução") ? "" :
                    (ReducaoCheckBox.Checked ? " redução -> [ " + PeriodoReducaoComboBox.Text + " ]" : ""));
            }
            catch
            {
                _horas.Motivo = MotivoTextBox.Text;
            }

            _horas.Data = dataDateTimePicker.Value.Date;
            _horas.DataExtensa = Publicas.DiaDaSemana(dataDateTimePicker.Value.DayOfWeek) + ", " +
                dataDateTimePicker.Value.ToShortDateString();

            if (AusenciaCheckBox.Checked)
            {
                if (dataDateTimePicker.Value.DayOfWeek == DayOfWeek.Friday)
                {
                    if (ReducaoCheckBox.Checked)
                    {
                        _horas.IncompletaFormatada = "06:00";
                        _horas.Incompletas = 360;
                    }
                    else
                    {
                        _horas.IncompletaFormatada = "08:00";
                        _horas.Incompletas = 480;
                    }
                }
                else
                {
                    if (ReducaoCheckBox.Checked)
                    {
                        _horas.IncompletaFormatada = "06:45";
                        _horas.Incompletas = 405;
                    }
                    else
                    {
                        _horas.IncompletaFormatada = "09:00";
                        _horas.Incompletas = 540;
                    }
                }
                _listaHorarios.Add(_horas);
            }
            else
            {
                FeriadoEmenda _feriado = new FeriadoBO().Consultar(dataDateTimePicker.Value.Date, _colaborador.IdEmpresa);

                if (!temDia)
                {
                    _horas.Data = dataDateTimePicker.Value.Date;
                    _horas.DataExtensa = (_feriado.Existe && _feriado.Tipo == "F" ? "Feriado, " : Publicas.DiaDaSemana(dataDateTimePicker.Value.DayOfWeek) + ", ") +
                        dataDateTimePicker.Value.ToShortDateString();
                }

                if (!temDia)
                    _listaHorarios.Add(_horas);

                entrada = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day,
                    Convert.ToInt32(_horas.Entrada.Substring(0, 2)), Convert.ToInt32(_horas.Entrada.Substring(3, 2)), 0);

                if (_horas.Saida.Trim() != ":")
                {
                    saida = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day,
                        Convert.ToInt32(_horas.Saida.Substring(0, 2)), Convert.ToInt32(_horas.Saida.Substring(3, 2)), 0);

                    if (saida < entrada)
                        saida = saida.AddDays(1);
                }

                if (_horas.SaidaAlmoco.Trim() != ":")
                    almoco = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day,
                        Convert.ToInt32(_horas.SaidaAlmoco.Substring(0, 2)), Convert.ToInt32(_horas.SaidaAlmoco.Substring(3, 2)), 0);

                if (_horas.VoltaAlmoco.Trim() != ":")
                    volta = new DateTime(dataDateTimePicker.Value.Year, dataDateTimePicker.Value.Month, dataDateTimePicker.Value.Day,
                        Convert.ToInt32(_horas.VoltaAlmoco.Substring(0, 2)), Convert.ToInt32(_horas.VoltaAlmoco.Substring(3, 2)), 0);

                if ((dataDateTimePicker.Value.DayOfWeek == DayOfWeek.Saturday ||
                    dataDateTimePicker.Value.DayOfWeek == DayOfWeek.Sunday ||
                    (_feriado.Existe && _feriado.Tipo == "F") || entrada > saidaMaxima) && !CompensacaoCheckBox.Checked)
                {
                    extraEntrada = saida.Subtract(entrada).TotalMinutes;
                }
                else
                {
                    if (entrada < entradaMinima)
                        extraEntrada = entradaExata.Subtract(entrada).TotalMinutes;

                    if (almoco > inicioAlmoco && _horas.SaidaAlmoco.Trim() != ":")
                        extraAlmoco = almoco.Subtract(inicioAlmoco).TotalMinutes;

                    if (volta < fimAlmoco && _horas.VoltaAlmoco.Trim() != ":")
                        extraVolta = fimAlmoco.Subtract(volta).TotalMinutes;

                    if (saida > saidaMaxima)
                        extraSaida = saida.Subtract(saidaMinima).TotalMinutes;

                    if (entrada > entradaMaxima)
                        incEntrada = entrada.Subtract(entradaExata).TotalMinutes;

                    if (almoco < inicioAlmoco && _horas.SaidaAlmoco.Trim() != ":")
                        incAlmoco = inicioAlmoco.Subtract(almoco).TotalMinutes;

                    if (volta > fimAlmoco && _horas.VoltaAlmoco.Trim() != ":")
                        incVolta = volta.Subtract(fimAlmoco).TotalMinutes;

                    if (saida < saidaMinima && _horas.Saida.Trim() != ":")
                        incSaida = saidaMinima.Subtract(saida).TotalMinutes;

                    if (_horas.SaidaAlmoco.Trim() == ":" && entrada > fimAlmoco)
                        extraVolta = 60;
                }

                _calExtra = extraEntrada + extraAlmoco + extraVolta + extraSaida;
                _calIncompletas = incEntrada + incAlmoco + incVolta + incSaida;

                if (!AtestadoCheckBox.Checked)
                {
                    foreach (var item in _listaHorarios.Where(w => w.Data.Date == _horas.Data.Date && w.Atestado))
                    {
                        if (item.Saida == _horas.Entrada)
                        {
                            _calIncompletas = _calIncompletas - Convert.ToDateTime(item.Saida).Subtract(Convert.ToDateTime(item.Entrada)).TotalMinutes;
                        }
                    }

                    if (_calExtra >= _calIncompletas)
                    {
                        _horas.Extra = (_calExtra - _calIncompletas);
                        _horas.ExtraFormatada = DateTime.MinValue.AddMinutes(_horas.Extra).ToShortTimeString();
                        _horas.IncompletaFormatada = "";
                        _horas.Incompletas = 0;
                    }
                    else
                    {
                        _horas.Extra = 0;
                        _horas.Incompletas = (_calIncompletas - _calExtra);
                        _horas.IncompletaFormatada = DateTime.MinValue.AddMinutes(_horas.Incompletas).ToShortTimeString();
                        _horas.ExtraFormatada = "";
                    }
                }

            }
            gridGroupingControl.DataSource = _listaHorarios;

            _totalExtra = _listaHorarios.Sum(s => s.Extra);
            _totalIncompleta = _listaHorarios.Sum(s => s.Incompletas);

            if (_totalExtra >= _totalIncompleta)
                {
                    TotalLabel.Text = "Extras";
                    if (DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta).Day == 1)
                        TotalMaskedEditBox.Text = DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta).ToShortTimeString();
                    else
                    {
                        TotalMaskedEditBox.Text = ((24 * ((DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta)).Day - 1)) + 
                        DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta).Hour).ToString() + ":" + 
                        DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta).Minute.ToString("00");
                    }
                }
            else
                {
                    TotalLabel.Text = "Incompletas";
                    if (DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra).Day == 1)
                        TotalMaskedEditBox.Text = DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra).ToShortTimeString();
                    else
                        TotalMaskedEditBox.Text = ((24 * ((DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra)).Day - 1)) +
                        DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra).Hour).ToString() + ":" + 
                        DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra).Minute.ToString("00");
                }

            temAlteracao = true;
            dataDateTimePicker.Value = dataDateTimePicker.Value.AddDays(1);
            dataDateTimePicker.Focus();
            EntradaMaskedEditBox.Text = String.Empty;
            SaidaAlmocoMaskedEditBox.Text = String.Empty;
            SaidaMaskedEditBox.Text = String.Empty;
            VoltaAlmocoMaskedEditBox.Text = String.Empty;
            AtestadoCheckBox.Checked = false;
            DeclaracaoCheckBox.Checked = false;
            AusenciaCheckBox.Checked = false;

            MotivoPanel.Visible = false;
            MotivoTextBox.Text = String.Empty;
        }

        private void PeriodoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void dataDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void EntradaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void PeriodoComboBox_Validating(object sender, CancelEventArgs e)
        {
            PeriodoComboBox.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (PeriodoComboBox.Text == "")
            {
                new Notificacoes.Mensagem("Selecione o Período do bando de horas", Publicas.TipoMensagem.Alerta).ShowDialog();
                PeriodoComboBox.Focus();
                return;
            }
            
            foreach (var item in _listaPeriodo)
            {
                if (item.Inicio.ToShortDateString() + " até " + item.Fim.ToShortDateString() == PeriodoComboBox.Text)
                {
                    _periodo = item;
                    break;
                }
            }

            _listaHorarios = new ControleDeHorasBO().Listar(_colaborador.Id, _periodo.Inicio, _periodo.Fim, _colaborador.IdEmpresa);

            gridGroupingControl.DataSource = _listaHorarios;

            double _totalExtra = 0;
            double _totalIncompleta = 0;

            _totalExtra = _listaHorarios.Sum(s => s.Extra);
            _totalIncompleta = _listaHorarios.Sum(s => s.Incompletas);
            if (_totalExtra >= _totalIncompleta)
            {
                TotalLabel.Text = "Extras";
                if (DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta).Day == 1)
                    TotalMaskedEditBox.Text = DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta).ToShortTimeString();
                else
                {
                    TotalMaskedEditBox.Text = ((24 * ((DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta)).Day - 1)) + DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta).Hour).ToString() + ":" + DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta).Minute.ToString("00");
                }

            }
            else
            {
                TotalLabel.Text = "Incompletas";
                if (DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra).Day == 1)
                    TotalMaskedEditBox.Text = DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra).ToShortTimeString();
                else
                    TotalMaskedEditBox.Text = ((24 * ((DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra)).Day - 1)) + DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra).Hour).ToString() + ":" + DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra).Minute.ToString("00");
            }

            gravarButton.Enabled = true;
            buttonAdv1.Enabled = _periodo.Fim.Date <= DateTime.Now.Date;
        }

        private void EntradaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void SaidaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

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

            if (dataDateTimePicker.Value.Date < _periodo.Inicio || dataDateTimePicker.Value.Date > _periodo.Fim)
            {
                new Notificacoes.Mensagem("Data fora do período do banco.", Publicas.TipoMensagem.Alerta).ShowDialog();
                dataDateTimePicker.Focus();
                return;
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            new ControleDeHorasBO().Gravar(_listaHorarios);

            PeriodoComboBox_Validating(sender, new CancelEventArgs());
            temAlteracao = false;
        }

        private void gridGroupingControl_TableControlCellDoubleClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    dataDateTimePicker.Value = (DateTime)dr["Data"];
                    EntradaMaskedEditBox.Text = (string)dr["Entrada"];
                    SaidaAlmocoMaskedEditBox.Text = (string)dr["SaidaAlmoco"];
                    VoltaAlmocoMaskedEditBox.Text = (string)dr["VoltaAlmoco"];
                    SaidaMaskedEditBox.Text = (string)dr["Saida"];
                    MotivoTextBox.Text = (string)dr["Motivo"];

                    AtestadoCheckBox.Checked = (bool)dr["Atestado"];
                    DeclaracaoCheckBox.Checked = (bool)dr["Declaracao"];
                    CompensacaoCheckBox.Checked = (bool)dr["Compensacao"];
                    AusenciaCheckBox.Checked = (bool)dr["Ausencia"];

                    EntradaMaskedEditBox.Focus();
                }
            }
            EntradaMaskedEditBox.Focus();
        }

        private void AtestadoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            EntradaMaskedEditBox_KeyDown(sender, e);
        }

        private void DeclaracaoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            EntradaMaskedEditBox_KeyDown(sender, e);
        }

        private void CompensacaoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            EntradaMaskedEditBox_KeyDown(sender, e);
        }

        private void ControleDeHoras_FormClosing(object sender, FormClosingEventArgs e)
        {

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

            _colaborador = new ColaboradoresBO().ConsultaColaborador(Convert.ToInt32(ColaboradorTextBox.Text));
            NomeColaboradorTextBox.Text = _colaborador.Nome;
            PeriodoComboBox.Focus();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[gridGroupingControl.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            if (rec != null)
            {

                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (new Notificacoes.Mensagem("Confirma a exclusão? ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    List<Classes.ControleDeHoras> _excluirTipos = new List<Classes.ControleDeHoras>();
                    gridGroupingControl.DataSource = new List<Classes.ControleDeHoras>();

                    int _id = 0;

                    try
                    {
                        _id = (int)dr["Id"];
                    }
                    catch
                    {
                        int posIniId = dr.Info.IndexOf("Id =") + 4;
                        int posFimId = dr.Info.IndexOf(", IdUsuario");
                        _id = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                    }
                    foreach (var item in _listaHorarios.Where(w => w.Id == _id))
                    {
                        _excluirTipos.Add(item);

                        if (item.Id != 0)
                        {
                            if (!new ControleDeHorasBO().Excluir(item))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    foreach (var item in _excluirTipos)
                    {
                        _listaHorarios.Remove(item);
                    }

                }

                gridGroupingControl.DataSource = _listaHorarios;

                double _totalExtra = 0;
                double _totalIncompleta = 0;

                _totalExtra = _listaHorarios.Sum(s => s.Extra);
                _totalIncompleta = _listaHorarios.Sum(s => s.Incompletas);

                if (_totalExtra >= _totalIncompleta)
                {
                    TotalLabel.Text = "Extras";
                    if (DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta).Day == 1)
                        TotalMaskedEditBox.Text = DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta).ToShortTimeString();
                    else
                    {
                        TotalMaskedEditBox.Text = ((24 * ((DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta)).Day - 1)) + DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta).Hour).ToString() + ":" + DateTime.MinValue.AddMinutes(_totalExtra - _totalIncompleta).Minute.ToString("00");
                    }

                }
                else
                {
                    TotalLabel.Text = "Incompletas";
                    if (DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra).Day == 1)
                        TotalMaskedEditBox.Text = DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra).ToShortTimeString();
                    else
                        TotalMaskedEditBox.Text = ((24 * ((DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra)).Day - 1)) + DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra).Hour).ToString() + ":" + DateTime.MinValue.AddMinutes(_totalIncompleta - _totalExtra).Minute.ToString("00");
                }

            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Publicas._caminhoPortal))
            {
                new Notificacoes.Mensagem("Diretório '" + Publicas._caminhoPortal + "' não existe.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;
            string nomeArquivo = "ControleDeHoras_" + PeriodoComboBox.Text.Replace("/","").Replace(" até ", "_");
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            _listaHorarios.OrderBy(w => w.Data).ToList();
                        
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int linha = 1;
            int col = 1;

            this.Cursor = Cursors.WaitCursor;

            #region titulo colunas
            xlWorkSheet.Cells[linha, col] = NomeColaboradorTextBox.Text;
            linha++;
            xlWorkSheet.Cells[linha, col] = "Total de " +  TotalLabel.Text + " " + TotalMaskedEditBox.Text;

            linha++;
            linha++;

            col = 1;
            xlWorkSheet.Cells[linha, col] = "Data ";
            col++;

            xlWorkSheet.Cells[linha, col] = "Entrada";
            col++;
            xlWorkSheet.Cells[linha, col] = "Saída Almoço";
            col++;
            xlWorkSheet.Cells[linha, col] = "Volta Almoço";
            col++;
            xlWorkSheet.Cells[linha, col] = "Saída";
            col++;
            xlWorkSheet.Cells[linha, col] = "Extras";
            col++;
            xlWorkSheet.Cells[linha, col] = "Incompletas";
            col++;
            xlWorkSheet.Cells[linha, col] = "Atestado";
            col++;
            xlWorkSheet.Cells[linha, col] = "Declaração";
            col++;
            xlWorkSheet.Cells[linha, col] = "Compensação";
            col++;
            xlWorkSheet.Cells[linha, col] = "Ausência";
            col++;
            xlWorkSheet.Cells[linha, col] = "Motivo";

            #endregion

            foreach (var itemC in _listaHorarios.OrderBy(o => o.Data))
            {
                col = 1;
                linha++;

                #region Dados
                xlWorkSheet.Cells[linha, col] = itemC.DataExtensa;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Entrada;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.SaidaAlmoco;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.VoltaAlmoco;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Saida;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ExtraFormatada;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.IncompletaFormatada;
                col++;
                xlWorkSheet.Cells[linha, col] = (itemC.Atestado ? "SIM" : "NÃO");
                col++;
                xlWorkSheet.Cells[linha, col] = (itemC.Declaracao ? "SIM" : "NÃO");
                col++;
                xlWorkSheet.Cells[linha, col] = (itemC.Compensacao ? "SIM" : "NÃO");
                col++;
                xlWorkSheet.Cells[linha, col] = (itemC.Ausencia ? "SIM" : "NÃO");
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Motivo ;
                col++;
                #endregion
            }

            xlWorkSheet.Columns.AutoFit();
            xlWorkBook.SaveAs(Publicas._caminhoPortal + nomeArquivo + ".xlsx", Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
                            Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            try
            {
                xlWorkBook.Close(true, misValue, misValue);
            }
            catch
            {
            }

            try
            {
                xlApp.Quit();
            }
            catch { }

            this.Cursor = Cursors.Default;
            new Notificacoes.Mensagem("Arquivo gerado com sucesso." + Environment.NewLine + "Salvo em: " + Publicas._caminhoPortal, Publicas.TipoMensagem.Alerta).ShowDialog();
        }

        private void ColaboradorTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void ColaboradorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PeriodoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ColaboradorTextBox.Focus();
            }
        }

        private void CompensacaoCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if ((DeclaracaoCheckBox.Checked || CompensacaoCheckBox.Checked || AusenciaCheckBox.Checked) && string.IsNullOrEmpty(MotivoTextBox.Text))
            {
                MotivoPanel.Visible = true;
                MotivoTextBox.Focus();
            }

            AusenciaCheckBox.Enabled = !CompensacaoCheckBox.Checked;
        }

        private void AusenciaCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if ((DeclaracaoCheckBox.Checked || CompensacaoCheckBox.Checked || AusenciaCheckBox.Checked) && string.IsNullOrEmpty(MotivoTextBox.Text))
            {
                MotivoPanel.Visible = true;
                MotivoTextBox.Focus();
            }

            CompensacaoCheckBox.Enabled = !AusenciaCheckBox.Checked;
        }

        private void buttonAdv2_Click(object sender, EventArgs e)
        {
            proximoButton.Focus();
            MotivoPanel.Visible = false;
        }

        private void DeclaracaoCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if ((DeclaracaoCheckBox.Checked || CompensacaoCheckBox.Checked || AusenciaCheckBox.Checked) && string.IsNullOrEmpty(MotivoTextBox.Text))
            {
                MotivoPanel.Visible = true;
                MotivoTextBox.Focus();
            }
        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        {
            // transferir saldo ara o próximo periodo
            Classes.ControleDeHoras _horas = new Classes.ControleDeHoras();

            _horas.Atestado = false;
            _horas.Declaracao = false;
            _horas.Compensacao = false;
            _horas.Ausencia = false;
            _horas.Motivo = "Transferência de Saldo para o próximo periodo";
            _horas.IdColaborador = _colaborador.Id;

            _horas.Data = _periodo.Fim.AddDays(1);

            double _totalExtra = _listaHorarios.Sum(s => s.Extra);
            double _totalIncompleta = _listaHorarios.Sum(s => s.Incompletas);

            if (TotalLabel.Text.Equals("Extras"))
                _horas.Extra = _totalExtra - _totalIncompleta;
            else
                _horas.Incompletas = _totalIncompleta - _totalExtra;

            List<Classes.ControleDeHoras> _listaTrans = new List<Classes.ControleDeHoras>();
            _listaTrans.Add(_horas);

            new ControleDeHorasBO().Gravar(_listaTrans);
            new Notificacoes.Mensagem("Transferido com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();
        }
    }
}
