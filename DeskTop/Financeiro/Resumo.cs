using Classes;
using DynamicFilter;
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

namespace Suportte.Financeiro
{
    public partial class Resumo : Form
    {
        public Resumo()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                if (Publicas._TemaBlack)
                {
                    gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl1.BackColor = Publicas._panelTitulo;

                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        #region Atributos

        //Classes.Feriado _feriado;
        List<Classes.Financeiro.Resumo> _listaResumo;
        List<Classes.Financeiro.Resumo> _listaResumoFinal;

        System.Xml.XmlTextWriter gravaColunasGridResumoFinanceiro;
        System.Xml.XmlTextWriter gravaCoresGridResumoFinanceiro;

        System.Xml.XmlReader leColunasGridResumoFinanceiro;
        System.Xml.XmlReader leCorGridResumoFinanceiro;

        DateTime _dataInicio;
        DateTime _dataFim;
        string _referencia;

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

        private void Resumo_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);

            #region coloca os botões centralizados
            List<ButtonAdv> _botoes = new List<ButtonAdv>() { limparButton, GerarExecelButton};
            _botoes = Publicas.CentralizarBotoes(_botoes, this.Width, GerarExecelButton.Left - (limparButton.Left + limparButton.Width));

            for (int i = 0; i < _botoes.Count(); i++)
            {
                if (i == 0)
                    limparButton.Left = _botoes[i].Left;
                if (i == 1)
                    GerarExecelButton.Left = _botoes[i].Left;
            }
            #endregion
            #region Grids

            gravaColunasGridResumoFinanceiro = new System.Xml.XmlTextWriter("GridResumoFinanceiro.xml", System.Text.Encoding.UTF8);
            gravaCoresGridResumoFinanceiro = new System.Xml.XmlTextWriter("CorGridResumoFinanceiro.xml", System.Text.Encoding.UTF8);

            gravaColunasGridResumoFinanceiro.Formatting = System.Xml.Formatting.Indented;
            gravaCoresGridResumoFinanceiro.Formatting = System.Xml.Formatting.Indented;

            gridGroupingControl1.WriteXmlSchema(gravaColunasGridResumoFinanceiro);
            gridGroupingControl1.WriteXmlLookAndFeel(gravaCoresGridResumoFinanceiro);
            gravaColunasGridResumoFinanceiro.Close();
            gravaCoresGridResumoFinanceiro.Close();

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

            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].AllowFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl1.TableDescriptor.Columns[i].AllowSort = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                gridGroupingControl1.SetMetroStyle(metroColor);
                gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            
            #endregion
        }

        private void Resumo_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }

        private void PeriodoEncerradoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReferenciaMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                MostrarFinaisSemanaCheckBox.Focus();
            }
        }

        private void ReferenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                MostrarFinaisSemanaCheckBox.Focus();
            }
        }
        
        private void limparButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gridGroupingControl1.Focus();
            }
        }

        private void ReferenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void ReferenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ReferenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;

            ReferenciaMaskedEditBox.ThemeStyle.BorderColor = Publicas._bordaSaida;
            if (Publicas._escTeclado)
            {
                ReferenciaMaskedEditBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(ReferenciaMaskedEditBox.ClipText.Trim()))
            {
                ReferenciaMaskedEditBox.Text = string.Empty;
                ReferenciaMaskedEditBox.Focus();
                return;
            }

            _referencia = ReferenciaMaskedEditBox.Text.Substring(3, 4) + ReferenciaMaskedEditBox.Text.Substring(0, 2);

            try
            {
                if (ReferenciaMaskedEditBox.ClipText.Trim().Length != 6)
                {
                    new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    ReferenciaMaskedEditBox.Focus();
                    return;
                }
                _dataInicio = new DateTime(Convert.ToInt32(ReferenciaMaskedEditBox.ClipText.Trim().Substring(2, 4)), Convert.ToInt32(ReferenciaMaskedEditBox.ClipText.Trim().Substring(0, 2)), 1);
                _dataFim = _dataInicio.AddMonths(1).AddDays(-1);
            }
            catch
            {
                new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ReferenciaMaskedEditBox.Focus();
                return;
            }

            leColunasGridResumoFinanceiro = new System.Xml.XmlTextReader("GridResumoFinanceiro.xml");
            gridGroupingControl1.ApplyXmlSchema(leColunasGridResumoFinanceiro);

            leCorGridResumoFinanceiro = new System.Xml.XmlTextReader("CorGridResumoFinanceiro.xml");
            gridGroupingControl1.ApplyXmlLookAndFeel(leCorGridResumoFinanceiro);

            leColunasGridResumoFinanceiro.Close();
            leCorGridResumoFinanceiro.Close();

            ReferenciaMaskedEditBox.Cursor = Cursors.WaitCursor;
            mensagemSistemaLabel.Text = "Aguarde, Pesquisando...";
            this.Refresh();

            #region Tira colunas
            if (!MostrarFinaisSemanaCheckBox.Checked)
            {
                DateTime _data = _dataInicio;

                while (_data <= _dataFim)
                {
                    //_feriado = new FeriadoBO().Consultar(_data);

                    if (_data.DayOfWeek == DayOfWeek.Saturday || _data.DayOfWeek == DayOfWeek.Sunday) // || _feriado.Existe)
                    {
                        switch (_data.Day)
                        {
                            case 1:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor01");
                                break;
                            case 2:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor02");
                                break;
                            case 3:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor03");
                                break;
                            case 4:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor04");
                                break;
                            case 5:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor05");
                                break;
                            case 6:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor06");
                                break;
                            case 7:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor07");
                                break;
                            case 8:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor08");
                                break;
                            case 9:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor09");
                                break;
                            case 10:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor10");
                                break;
                            case 11:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor11");
                                break;
                            case 12:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor12");
                                break;
                            case 13:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor13");
                                break;
                            case 14:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor14");
                                break;
                            case 15:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor15");
                                break;
                            case 16:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor16");
                                break;
                            case 17:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor17");
                                break;
                            case 18:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor18");
                                break;
                            case 19:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor19");
                                break;
                            case 20:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor20");
                                break;
                            case 21:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor21");
                                break;
                            case 22:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor22");
                                break;
                            case 23:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor23");
                                break;
                            case 24:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor24");
                                break;
                            case 25:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor25");
                                break;
                            case 26:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor26");
                                break;
                            case 27:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor27");
                                break;
                            case 28:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor28");
                                break;
                            case 29:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor29");
                                break;
                            case 30:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor30");
                                break;
                            case 31:
                                gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor31");
                                break;
                        }
                    }

                    _data = _data.AddDays(1);
                }
            }

            if (_dataFim.Day < 31)
            {
                int dia = _dataFim.Day + 1;

                while (dia <= 31)
                {
                    switch (dia)
                    {
                        case 29:
                            gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor29");
                            break;
                        case 30:
                            gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor30");
                            break;
                        case 31:
                            gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("Valor31");
                            break;
                    }
                    dia++;
                }
            }

            #endregion

            if (_listaResumo == null)
                _listaResumo = new List<Classes.Financeiro.Resumo>();

            if (_listaResumoFinal == null)
                _listaResumoFinal = new List<Classes.Financeiro.Resumo>();

            _listaResumo = new FinanceiroBO().ListarResumo(_referencia);

            Classes.Financeiro.Resumo _resumo = new Classes.Financeiro.Resumo();
            decimal _valor = 0;

            // colocado o z antes do nome para ficar ordenado por ultimo. A ordem do grid é o nome da Empresa e o campo Ordem
            _resumo.Empresa = "ZConsolidado";
            _resumo.Descricao = " ";
            _resumo.Ordem = 10000;
            _listaResumo.Add(_resumo);

            _resumo = new Classes.Financeiro.Resumo();
            decimal _acumulado = 0;

            for (int i = 1; i < 32; i++)
            {
                _valor = 0;

                foreach (var item in _listaResumo.Where(w => w.IdEmpresa == 99999 && w.Ordem == 0))
                {
                    switch (i)
                    {
                        case 1:
                            _valor = _valor + item.Valor01;
                            break;
                        case 2:
                            _valor = _valor + item.Valor02;
                            break;
                        case 3:
                            _valor = _valor + item.Valor03;
                            break;
                        case 4:
                            _valor = _valor + item.Valor04;
                            break;
                        case 5:
                            _valor = _valor + item.Valor05;
                            break;
                        case 6:
                            _valor = _valor + item.Valor06;
                            break;
                        case 7:
                            _valor = _valor + item.Valor07;
                            break;
                        case 8:
                            _valor = _valor + item.Valor08;
                            break;
                        case 9:
                            _valor = _valor + item.Valor09;
                            break;
                        case 10:
                            _valor = _valor + item.Valor10;
                            break;
                        case 11:
                            _valor = _valor + item.Valor11;
                            break;
                        case 12:
                            _valor = _valor + item.Valor12;
                            break;
                        case 13:
                            _valor = _valor + item.Valor13;
                            break;
                        case 14:
                            _valor = _valor + item.Valor14;
                            break;
                        case 15:
                            _valor = _valor + item.Valor15;
                            break;
                        case 16:
                            _valor = _valor + item.Valor16;
                            break;
                        case 17:
                            _valor = _valor + item.Valor17;
                            break;
                        case 18:
                            _valor = _valor + item.Valor18;
                            break;
                        case 19:
                            _valor = _valor + item.Valor19;
                            break;
                        case 20:
                            _valor = _valor + item.Valor20;
                            break;
                        case 21:
                            _valor = _valor + item.Valor21;
                            break;
                        case 22:
                            _valor = _valor + item.Valor22;
                            break;
                        case 23:
                            _valor = _valor + item.Valor23;
                            break;
                        case 24:
                            _valor = _valor + item.Valor24;
                            break;
                        case 25:
                            _valor = _valor + item.Valor25;
                            break;
                        case 26:
                            _valor = _valor + item.Valor26;
                            break;
                        case 27:
                            _valor = _valor + item.Valor27;
                            break;
                        case 28:
                            _valor = _valor + item.Valor28;
                            break;
                        case 29:
                            _valor = _valor + item.Valor29;
                            break;
                        case 30:
                            _valor = _valor + item.Valor30;
                            break;
                        case 31:
                            _valor = _valor + item.Valor31;
                            break;
                    }
                }
                _resumo.Empresa = "ZConsolidado";
                _resumo.Descricao = "Sub Total";
                _resumo.Ordem = 10001;
                _acumulado = _acumulado + _valor;

                switch (i)
                {
                    case 1:
                        _resumo.Valor01 = _valor;
                        break;
                    case 2:
                        _resumo.Valor02 = _valor;
                        break;
                    case 3:
                        _resumo.Valor03 = _valor;
                        break;
                    case 4:
                        _resumo.Valor04 = _valor;
                        break;
                    case 5:
                        _resumo.Valor05 = _valor;
                        break;
                    case 6:
                        _resumo.Valor06 = _valor;
                        break;
                    case 7:
                        _resumo.Valor07 = _valor;
                        break;
                    case 8:
                        _resumo.Valor08 = _valor;
                        break;
                    case 9:
                        _resumo.Valor09 = _valor;
                        break;
                    case 10:
                        _resumo.Valor10 = _valor;
                        break;
                    case 11:
                        _resumo.Valor11 = _valor;
                        break;
                    case 12:
                        _resumo.Valor12 = _valor;
                        break;
                    case 13:
                        _resumo.Valor13 = _valor;
                        break;
                    case 14:
                        _resumo.Valor14 = _valor;
                        break;
                    case 15:
                        _resumo.Valor15 = _valor;
                        break;
                    case 16:
                        _resumo.Valor16 = _valor;
                        break;
                    case 17:
                        _resumo.Valor17 = _valor;
                        break;
                    case 18:
                        _resumo.Valor18 = _valor;
                        break;
                    case 19:
                        _resumo.Valor19 = _valor;
                        break;
                    case 20:
                        _resumo.Valor20 = _valor;
                        break;
                    case 21:
                        _resumo.Valor21 = _valor;
                        break;
                    case 22:
                        _resumo.Valor22 = _valor;
                        break;
                    case 23:
                        _resumo.Valor23 = _valor;
                        break;
                    case 24:
                        _resumo.Valor24 = _valor;
                        break;
                    case 25:
                        _resumo.Valor25 = _valor;
                        break;
                    case 26:
                        _resumo.Valor26 = _valor;
                        break;
                    case 27:
                        _resumo.Valor27 = _valor;
                        break;
                    case 28:
                        _resumo.Valor28 = _valor;
                        break;
                    case 29:
                        _resumo.Valor29 = _valor;
                        break;
                    case 30:
                        _resumo.Valor30 = _valor;
                        break;
                    case 31:
                        _resumo.Valor31 = _valor;
                        break;
                }
            }
            _resumo.AcumuladoMes = _acumulado;

            _listaResumo.Add(_resumo);

            _resumo = new Classes.Financeiro.Resumo();
            decimal _saldoInicial = 0;
            decimal _saldoAnterior = 0;
            decimal _saldoAtual = 0;

            foreach (var item in _listaResumo.Where(w => w.IdEmpresa == 99999 && w.Ordem == 0))
            {
                _saldoInicial = item.SaldoInicial;
            }

            _saldoAnterior = _saldoInicial;

            _resumo.Empresa = "ZConsolidado";
            _resumo.Descricao = "Saldo dia Anterior";
            _resumo.Ordem = 10002;
            _resumo.SaldoInicial = _saldoAnterior;
            _resumo.AcumuladoMes = _saldoAnterior;
            _saldoAtual = _saldoAnterior;

            for (int i = 1; i < 32; i++)
            {
                _valor = 0;
                foreach (var item in _listaResumo.Where(w => w.Ordem == 10001))
                {
                    _saldoAnterior = _saldoAtual;

                    switch (i)
                    {
                        case 1:
                            _valor = _valor + item.Valor01;
                            break;
                        case 2:
                            _valor = _valor + item.Valor02;
                            break;
                        case 3:
                            _valor = _valor + item.Valor03;
                            break;
                        case 4:
                            _valor = _valor + item.Valor04;
                            break;
                        case 5:
                            _valor = _valor + item.Valor05;
                            break;
                        case 6:
                            _valor = _valor + item.Valor06;
                            break;
                        case 7:
                            _valor = _valor + item.Valor07;
                            break;
                        case 8:
                            _valor = _valor + item.Valor08;
                            break;
                        case 9:
                            _valor = _valor + item.Valor09;
                            break;
                        case 10:
                            _valor = _valor + item.Valor10;
                            break;
                        case 11:
                            _valor = _valor + item.Valor11;
                            break;
                        case 12:
                            _valor = _valor + item.Valor12;
                            break;
                        case 13:
                            _valor = _valor + item.Valor13;
                            break;
                        case 14:
                            _valor = _valor + item.Valor14;
                            break;
                        case 15:
                            _valor = _valor + item.Valor15;
                            break;
                        case 16:
                            _valor = _valor + item.Valor16;
                            break;
                        case 17:
                            _valor = _valor + item.Valor17;
                            break;
                        case 18:
                            _valor = _valor + item.Valor18;
                            break;
                        case 19:
                            _valor = _valor + item.Valor19;
                            break;
                        case 20:
                            _valor = _valor + item.Valor20;
                            break;
                        case 21:
                            _valor = _valor + item.Valor21;
                            break;
                        case 22:
                            _valor = _valor + item.Valor22;
                            break;
                        case 23:
                            _valor = _valor + item.Valor23;
                            break;
                        case 24:
                            _valor = _valor + item.Valor24;
                            break;
                        case 25:
                            _valor = _valor + item.Valor25;
                            break;
                        case 26:
                            _valor = _valor + item.Valor26;
                            break;
                        case 27:
                            _valor = _valor + item.Valor27;
                            break;
                        case 28:
                            _valor = _valor + item.Valor28;
                            break;
                        case 29:
                            _valor = _valor + item.Valor29;
                            break;
                        case 30:
                            _valor = _valor + item.Valor30;
                            break;
                        case 31:
                            _valor = _valor + item.Valor31;
                            break;
                    }

                    _saldoAtual = _saldoAtual + _valor;
                }
                
                switch (i)
                {
                    case 1:
                        _resumo.Valor01 = _saldoAnterior;
                        break;
                    case 2:
                        _resumo.Valor02 = _saldoAnterior;
                        break;
                    case 3:
                        _resumo.Valor03 = _saldoAnterior;
                        break;
                    case 4:
                        _resumo.Valor04 = _saldoAnterior;
                        break;
                    case 5:
                        _resumo.Valor05 = _saldoAnterior;
                        break;
                    case 6:
                        _resumo.Valor06 = _saldoAnterior;
                        break;
                    case 7:
                        _resumo.Valor07 = _saldoAnterior;
                        break;
                    case 8:
                        _resumo.Valor08 = _saldoAnterior;
                        break;
                    case 9:
                        _resumo.Valor09 = _saldoAnterior;
                        break;
                    case 10:
                        _resumo.Valor10 = _saldoAnterior;
                        break;
                    case 11:
                        _resumo.Valor11 = _saldoAnterior;
                        break;
                    case 12:
                        _resumo.Valor12 = _saldoAnterior;
                        break;
                    case 13:
                        _resumo.Valor13 = _saldoAnterior;
                        break;
                    case 14:
                        _resumo.Valor14 = _saldoAnterior;
                        break;
                    case 15:
                        _resumo.Valor15 = _saldoAnterior;
                        break;
                    case 16:
                        _resumo.Valor16 = _saldoAnterior;
                        break;
                    case 17:
                        _resumo.Valor17 = _saldoAnterior;
                        break;
                    case 18:
                        _resumo.Valor18 = _saldoAnterior;
                        break;
                    case 19:
                        _resumo.Valor19 = _saldoAnterior;
                        break;
                    case 20:
                        _resumo.Valor20 = _saldoAnterior;
                        break;
                    case 21:
                        _resumo.Valor21 = _saldoAnterior;
                        break;
                    case 22:
                        _resumo.Valor22 = _saldoAnterior;
                        break;
                    case 23:
                        _resumo.Valor23 = _saldoAnterior;
                        break;
                    case 24:
                        _resumo.Valor24 = _saldoAnterior;
                        break;
                    case 25:
                        _resumo.Valor25 = _saldoAnterior;
                        break;
                    case 26:
                        _resumo.Valor26 = _saldoAnterior;
                        break;
                    case 27:
                        _resumo.Valor27 = _saldoAnterior;
                        break;
                    case 28:
                        _resumo.Valor28 = _saldoAnterior;
                        break;
                    case 29:
                        _resumo.Valor29 = _saldoAnterior;
                        break;
                    case 30:
                        _resumo.Valor30 = _saldoAnterior;
                        break;
                    case 31:
                        _resumo.Valor31 = _saldoAnterior;
                        break;
                }
            }

            _listaResumo.Add(_resumo);

            _resumo = new Classes.Financeiro.Resumo();
            _resumo.Empresa = "ZConsolidado";
            _resumo.Descricao = "Total Geral";
            _resumo.Ordem = 10003;
            _resumo.SaldoInicial = _saldoInicial;

            for (int i = 1; i < 32; i++)
            {
                _valor = 0;
                foreach (var item in _listaResumo.Where(w => w.Ordem >= 10001))
                {

                    switch (i)
                    {
                        case 1:
                            _valor = _valor + item.Valor01;
                            break;
                        case 2:
                            _valor = _valor + item.Valor02;
                            break;
                        case 3:
                            _valor = _valor + item.Valor03;
                            break;
                        case 4:
                            _valor = _valor + item.Valor04;
                            break;
                        case 5:
                            _valor = _valor + item.Valor05;
                            break;
                        case 6:
                            _valor = _valor + item.Valor06;
                            break;
                        case 7:
                            _valor = _valor + item.Valor07;
                            break;
                        case 8:
                            _valor = _valor + item.Valor08;
                            break;
                        case 9:
                            _valor = _valor + item.Valor09;
                            break;
                        case 10:
                            _valor = _valor + item.Valor10;
                            break;
                        case 11:
                            _valor = _valor + item.Valor11;
                            break;
                        case 12:
                            _valor = _valor + item.Valor12;
                            break;
                        case 13:
                            _valor = _valor + item.Valor13;
                            break;
                        case 14:
                            _valor = _valor + item.Valor14;
                            break;
                        case 15:
                            _valor = _valor + item.Valor15;
                            break;
                        case 16:
                            _valor = _valor + item.Valor16;
                            break;
                        case 17:
                            _valor = _valor + item.Valor17;
                            break;
                        case 18:
                            _valor = _valor + item.Valor18;
                            break;
                        case 19:
                            _valor = _valor + item.Valor19;
                            break;
                        case 20:
                            _valor = _valor + item.Valor20;
                            break;
                        case 21:
                            _valor = _valor + item.Valor21;
                            break;
                        case 22:
                            _valor = _valor + item.Valor22;
                            break;
                        case 23:
                            _valor = _valor + item.Valor23;
                            break;
                        case 24:
                            _valor = _valor + item.Valor24;
                            break;
                        case 25:
                            _valor = _valor + item.Valor25;
                            break;
                        case 26:
                            _valor = _valor + item.Valor26;
                            break;
                        case 27:
                            _valor = _valor + item.Valor27;
                            break;
                        case 28:
                            _valor = _valor + item.Valor28;
                            break;
                        case 29:
                            _valor = _valor + item.Valor29;
                            break;
                        case 30:
                            _valor = _valor + item.Valor30;
                            break;
                        case 31:
                            _valor = _valor + item.Valor31;
                            break;
                    }
                }

                switch (i)
                {
                    case 1:
                        _resumo.Valor01 = _valor;
                        break;
                    case 2:
                        _resumo.Valor02 = _valor;
                        break;
                    case 3:
                        _resumo.Valor03 = _valor;
                        break;
                    case 4:
                        _resumo.Valor04 = _valor;
                        break;
                    case 5:
                        _resumo.Valor05 = _valor;
                        break;
                    case 6:
                        _resumo.Valor06 = _valor;
                        break;
                    case 7:
                        _resumo.Valor07 = _valor;
                        break;
                    case 8:
                        _resumo.Valor08 = _valor;
                        break;
                    case 9:
                        _resumo.Valor09 = _valor;
                        break;
                    case 10:
                        _resumo.Valor10 = _valor;
                        break;
                    case 11:
                        _resumo.Valor11 = _valor;
                        break;
                    case 12:
                        _resumo.Valor12 = _valor;
                        break;
                    case 13:
                        _resumo.Valor13 = _valor;
                        break;
                    case 14:
                        _resumo.Valor14 = _valor;
                        break;
                    case 15:
                        _resumo.Valor15 = _valor;
                        break;
                    case 16:
                        _resumo.Valor16 = _valor;
                        break;
                    case 17:
                        _resumo.Valor17 = _valor;
                        break;
                    case 18:
                        _resumo.Valor18 = _valor;
                        break;
                    case 19:
                        _resumo.Valor19 = _valor;
                        break;
                    case 20:
                        _resumo.Valor20 = _valor;
                        break;
                    case 21:
                        _resumo.Valor21 = _valor;
                        break;
                    case 22:
                        _resumo.Valor22 = _valor;
                        break;
                    case 23:
                        _resumo.Valor23 = _valor;
                        break;
                    case 24:
                        _resumo.Valor24 = _valor;
                        break;
                    case 25:
                        _resumo.Valor25 = _valor;
                        break;
                    case 26:
                        _resumo.Valor26 = _valor;
                        break;
                    case 27:
                        _resumo.Valor27 = _valor;
                        break;
                    case 28:
                        _resumo.Valor28 = _valor;
                        break;
                    case 29:
                        _resumo.Valor29 = _valor;
                        break;
                    case 30:
                        _resumo.Valor30 = _valor;
                        break;
                    case 31:
                        _resumo.Valor31 = _valor;
                        break;
                }
            }
            _resumo.AcumuladoMes = _valor;
            _listaResumo.Add(_resumo);

            gridGroupingControl1.DataSource = _listaResumo; //.Where(w => w.IdEmpresa != 99999).ToList();


            ReferenciaMaskedEditBox.Cursor = Cursors.Default;
            mensagemSistemaLabel.Text = "";
            this.Refresh();

            GerarExecelButton.Enabled = true;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            {
                GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;
                Record dr = null;

                if (rec != null)
                {
                    dr = rec.GetRecord() as Record;

                    if ((string)dr["Empresa"] == (string)dr["Descricao"] || ((string)dr["Descricao"]).Contains("Total") || ((string)dr["Descricao"]).Contains("Consolidado"))
                        e.Style.Font.Bold = true;
                }

                {
                    if (e.TableCellIdentity.Column.MappingName.Contains("Valor") || e.TableCellIdentity.Column.MappingName.Contains("Saldo") || e.TableCellIdentity.Column.MappingName.Contains("Acumulado"))
                    {
                        if (Convert.ToDecimal(e.Style.Text) < 0)
                            e.Style.TextColor = (Publicas._TemaBlack ? Color.DarkOrange : Color.DarkRed);
                    }
                }
            }
            catch { }
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            gridGroupingControl1.DataSource = new List<Classes.Financeiro.Resumo>();
            ReferenciaMaskedEditBox.Text = string.Empty;
            GerarExecelButton.Enabled = false;
            ReferenciaMaskedEditBox.Focus();
        }

        private void GerarExecelButton_Click(object sender, EventArgs e)
        {
            mensagemSistemaLabel.Text = "Exportando dados para o Excel, aguarde...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            if (!System.IO.Directory.Exists(Publicas._caminhoAnexosFinanceiro))
                System.IO.Directory.CreateDirectory(Publicas._caminhoAnexosFinanceiro);

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;

            string nomeArquivo = "Resumo_Financeiro_" + _referencia.ToString();

            xlApp = new Excel.Application();

            try
            {

                xlApp.DisplayAlerts = false;

                xlWorkBook = xlApp.Workbooks.Add(misValue);

                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                int linha = 1;
                int col = 1;


                #region titulo colunas
                xlWorkSheet.Cells[linha, col] = "Conciliação Fluxo de Caixa Dário - " + ReferenciaMaskedEditBox.Text;
                col++;
                linha++;
                linha++;

                col = 1;

                xlWorkSheet.Cells[linha, col] = " ";
                col++;
                xlWorkSheet.Cells[linha, col] = "Saldo Anterior";
                col++;

                // Dias
                
                DateTime _data = _dataInicio;

                while (_data <= _dataFim)
                {
                    xlWorkSheet.Cells[linha, col] = _data.Day.ToString();
                    col++;
                    _data = _data.AddDays(1);
                }

                xlWorkSheet.Cells[linha, col] = "Acumulado do Mês";
                col++;
                xlWorkSheet.Cells[linha, col] = "Saldo Final";

                #endregion

                string EmpresaAntes = "";
                foreach (var itemC in _listaResumo.OrderBy(o => o.Ordem)
                                                  .OrderBy(o => o.Empresa))
                {
                    col = 1;
                    linha++;

                    if (EmpresaAntes != itemC.Empresa)
                    {
                        linha++;
                        EmpresaAntes = itemC.Empresa;
                    }

                    if (itemC.Ordem == 0)
                        xlWorkSheet.Cells[linha, col] = itemC.Empresa;
                    else
                        xlWorkSheet.Cells[linha, col] = itemC.Descricao;

                    col++;
                    if (itemC.Ordem == 0)
                        xlWorkSheet.Cells[linha, col] = itemC.SaldoInicial.ToString();
                    else
                        xlWorkSheet.Cells[linha, col] = "";

                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor01.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor02.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor03.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor04.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor05.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor06.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor07.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor08.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor09.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor10.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor11.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor12.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor13.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor14.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor15.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor16.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor17.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor18.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor19.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor20.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor21.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor22.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor23.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor24.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor25.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor26.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor27.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor28.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor29.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor30.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.Valor31.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.AcumuladoMes.ToString();
                    col++;
                    xlWorkSheet.Cells[linha, col] = itemC.SaldoFinal.ToString();
                    col++;
                }

                xlWorkSheet.Columns.AutoFit();
                xlWorkBook.SaveAs(Publicas._caminhoAnexosFinanceiro + nomeArquivo + ".xlsx", Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
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
                mensagemSistemaLabel.Text = "";
                new Notificacoes.Mensagem("Arquivo gerado com sucesso." + Environment.NewLine +
                    "Salvo na pasta " + Publicas._caminhoAnexosFinanceiro, Publicas.TipoMensagem.Sucesso).ShowDialog();
                this.Refresh();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                mensagemSistemaLabel.Text = "";
                new Notificacoes.Mensagem("Não foi possível gerar o arquivo." + Environment.NewLine +
                    "Tente em outra maquina." + Environment.NewLine + ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
            }
        }
    }
}
