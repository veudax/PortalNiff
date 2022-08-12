using Classes;
using Negocio;
using Syncfusion.GridHelperClasses;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.BI
{
    public partial class ResumoDosAcessos : Form
    {
        public ResumoDosAcessos()
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

        List<Classes.PowerBI.EmailDeAcesso> _listaEmails;
        List<Classes.PowerBI.Resumo> _listaAcessos;
        List<Classes.PowerBI.UsuariosAutorizados> _listaUsuarios;

        System.Xml.XmlTextWriter gravaColunasGridCabecalho;
        System.Xml.XmlTextWriter gravaCoresGridCabecalho;
        System.Xml.XmlReader leColunasGridCabecalho;
        System.Xml.XmlReader leCorGridCabecalho;
        string _diretorioCor = "";
        string _diretorioCab = "";


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

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                limparButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
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

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void limparButton_Enter(object sender, EventArgs e)
        {
            limparButton.BackColor = Publicas._botaoFocado;
            limparButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            limparButton.BackColor = Publicas._botao;
            limparButton.ForeColor = Publicas._fonteBotao;
        }

        private void ResumoDosAcessos_Shown(object sender, EventArgs e)
        {
            this.Top = 60;
            if (Environment.MachineName.ToUpper().Contains("CORPTS") || Environment.MachineName.ToUpper().Contains("CORPRDP"))
            {
                _diretorioCab = Path.GetDirectoryName(Application.ExecutablePath) + @"\xml\GridCabecalhoResumoPowerBI" + Publicas._usuario.Id + ".xml";
                _diretorioCor = Path.GetDirectoryName(Application.ExecutablePath) + @"\xml\CorGridCabecalhoResumoPowerBI" + Publicas._usuario.Id + ".xml";
            }
            else
            {
                _diretorioCab = Publicas._caminhoPortal + "GridCabecalhoResumoPowerBI.xml";
                _diretorioCor = Publicas._caminhoPortal + "CorGridCabecalhoResumoPowerBI.xml";
            }

            //gravaColunasGridCabecalho = new System.Xml.XmlTextWriter("GridCabecalhoResumoPowerBI.xml", System.Text.Encoding.UTF8);
            //gravaCoresGridCabecalho = new System.Xml.XmlTextWriter("CorGridCabecalhoResumoPowerBI.xml", System.Text.Encoding.UTF8);
            gravaColunasGridCabecalho = new System.Xml.XmlTextWriter(_diretorioCab, System.Text.Encoding.UTF8);
            gravaCoresGridCabecalho = new System.Xml.XmlTextWriter(_diretorioCor, System.Text.Encoding.UTF8);

            gravaColunasGridCabecalho.Formatting = System.Xml.Formatting.Indented;
            gravaCoresGridCabecalho.Formatting = System.Xml.Formatting.Indented;

            gridGroupingControl1.WriteXmlSchema(gravaColunasGridCabecalho);
            gridGroupingControl1.WriteXmlLookAndFeel(gravaCoresGridCabecalho);
            gravaColunasGridCabecalho.Close();
            gravaCoresGridCabecalho.Close();

            _listaEmails = new PowerBIBO().Listar(true);
            _listaUsuarios = new PowerBIBO().ListarUsuarios(0); // todos;

            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.gridGroupingControl1);

            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl1.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

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

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            gridGroupingControl1.Table.DefaultRecordRowHeight = 22;
            gridGroupingControl1.TableControl.CellToolTip.AutoPopDelay = 5000;

            referenciaMaskedEditBox.Focus();
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            DateTime _dataInicio = DateTime.MaxValue;
            DateTime _data = DateTime.MaxValue;

            // Para pegar o último dia do mês selecionado.
            try
            {
                _dataInicio = Convert.ToDateTime("01/" + referenciaMaskedEditBox.Text);
                _data = _dataInicio.AddMonths(1).AddDays(-1);
            }
            catch
            {
                new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            _listaAcessos = new PowerBIBO().ResumoAcesso(_dataInicio, _data);

            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            int _coluna = 0;
            int _idEmail = 0;
            bool _encontrou = false;

            leColunasGridCabecalho = new System.Xml.XmlTextReader(_diretorioCab);
            gridGroupingControl1.ApplyXmlSchema(leColunasGridCabecalho);

            leCorGridCabecalho = new System.Xml.XmlTextReader(_diretorioCor);
            gridGroupingControl1.ApplyXmlLookAndFeel(leCorGridCabecalho);

            leColunasGridCabecalho.Close();
            leCorGridCabecalho.Close();

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                if (gridGroupingControl1.TableDescriptor.Columns[i].MappingName.Contains("QuantidadeColuna"))
                {
                    _coluna = Convert.ToInt32(Publicas.OnlyNumbers(gridGroupingControl1.TableDescriptor.Columns[i].MappingName));

                    if (gridGroupingControl1.TableDescriptor.Columns[i].MappingName == gridGroupingControl1.TableDescriptor.Columns[i].HeaderText)
                    {
                        try
                        {
                            _idEmail = 0;
                            switch (_coluna)
                            {
                                case 1:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna1 ?? 0) != 0).Max(m => m.idEmailColuna1);
                                    break;
                                case 2:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna2 ?? 0) != 0).Max(m => m.idEmailColuna2);
                                    break;
                                case 3:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna3 ?? 0) != 0).Max(m => m.idEmailColuna3);
                                    break;
                                case 4:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna4 ?? 0) != 0).Max(m => m.idEmailColuna4);
                                    break;
                                case 5:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna5 ?? 0) != 0).Max(m => m.idEmailColuna5);
                                    break;
                                case 6:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna6 ?? 0) != 0).Max(m => m.idEmailColuna6);
                                    break;
                                case 7:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna7 ?? 0) != 0).Max(m => m.idEmailColuna7);
                                    break;
                                case 8:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna8 ?? 0) != 0).Max(m => m.idEmailColuna8);
                                    break;
                                case 9:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna9 ?? 0) != 0).Max(m => m.idEmailColuna9);
                                    break;
                                case 10:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna10 ?? 0) != 0).Max(m => m.idEmailColuna10);
                                    break;
                                case 11:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna11 ?? 0) != 0).Max(m => m.idEmailColuna11);
                                    break;
                                case 12:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna12 ?? 0) != 0).Max(m => m.idEmailColuna12);
                                    break;
                                case 13:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna13 ?? 0) != 0).Max(m => m.idEmailColuna13);
                                    break;
                                case 14:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna14 ?? 0) != 0).Max(m => m.idEmailColuna14);
                                    break;
                                case 15:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna15 ?? 0) != 0).Max(m => m.idEmailColuna15);
                                    break;
                                case 16:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna16 ?? 0) != 0).Max(m => m.idEmailColuna16);
                                    break;
                                case 17:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna17 ?? 0) != 0).Max(m => m.idEmailColuna17);
                                    break;
                                case 18:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna18 ?? 0) != 0).Max(m => m.idEmailColuna18);
                                    break;
                                case 19:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna19 ?? 0) != 0).Max(m => m.idEmailColuna19);
                                    break;
                                case 20:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna20 ?? 0) != 0).Max(m => m.idEmailColuna20);
                                    break;
                                case 21:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna21 ?? 0) != 0).Max(m => m.idEmailColuna21);
                                    break;
                                case 22:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna22 ?? 0) != 0).Max(m => m.idEmailColuna22);
                                    break;
                                case 23:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna23 ?? 0) != 0).Max(m => m.idEmailColuna23);
                                    break;
                                case 24:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna24 ?? 0) != 0).Max(m => m.idEmailColuna24);
                                    break;
                                case 25:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna25 ?? 0) != 0).Max(m => m.idEmailColuna25);
                                    break;
                                case 26:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna26 ?? 0) != 0).Max(m => m.idEmailColuna26);
                                    break;
                                case 27:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna27 ?? 0) != 0).Max(m => m.idEmailColuna27);
                                    break;
                                case 28:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna28 ?? 0) != 0).Max(m => m.idEmailColuna28);
                                    break;
                                case 29:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna29 ?? 0) != 0).Max(m => m.idEmailColuna29);
                                    break;
                                case 30:
                                    _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna30 ?? 0) != 0).Max(m => m.idEmailColuna30);
                                    break;
                            }

                        }
                        catch { }

                        _encontrou = false;
                        foreach (var item in _listaEmails.Where(w => w.Id == _idEmail))
                        {
                            _encontrou = true;
                            gridGroupingControl1.TableDescriptor.Columns[i].HeaderText = item.Nome.Replace("Diretoria", "").Replace("Gerência", "").Replace("/", "").Trim();
                        }

                        if (!_encontrou)
                            gridGroupingControl1.TableDescriptor.VisibleColumns.Remove(gridGroupingControl1.TableDescriptor.Columns[i].MappingName);
                        
                    }
                    
                }
            }

            #region Totalização do grid
            GridSummaryRowDescriptor _soma;

            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor.DataMember = "QuantidadeColuna1";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "QuantidadeColuna1";
            summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor2.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor2.DataMember = "QuantidadeColuna2";
            summaryColumnDescriptor2.Format = "{Sum}";
            summaryColumnDescriptor2.Name = "QuantidadeColuna2";
            summaryColumnDescriptor2.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor3 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor3.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor3.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor3.DataMember = "QuantidadeColuna3";
            summaryColumnDescriptor3.Format = "{Sum}";
            summaryColumnDescriptor3.Name = "QuantidadeColuna3";
            summaryColumnDescriptor3.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor4 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor4.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor4.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor4.DataMember = "QuantidadeColuna4";
            summaryColumnDescriptor4.Format = "{Sum}";
            summaryColumnDescriptor4.Name = "QuantidadeColuna4";
            summaryColumnDescriptor4.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor5 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor5.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor5.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor5.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor5.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor5.DataMember = "QuantidadeColuna5";
            summaryColumnDescriptor5.Format = "{Sum}";
            summaryColumnDescriptor5.Name = "QuantidadeColuna5";
            summaryColumnDescriptor5.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor6 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor6.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor6.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor6.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor6.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor6.DataMember = "QuantidadeColuna6";
            summaryColumnDescriptor6.Format = "{Sum}";
            summaryColumnDescriptor6.Name = "QuantidadeColuna6";
            summaryColumnDescriptor6.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor7 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor7.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor7.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor7.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor7.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor7.DataMember = "QuantidadeColuna7";
            summaryColumnDescriptor7.Format = "{Sum}";
            summaryColumnDescriptor7.Name = "QuantidadeColuna7";
            summaryColumnDescriptor7.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor8 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor8.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor8.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor8.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor8.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor8.DataMember = "QuantidadeColuna8";
            summaryColumnDescriptor8.Format = "{Sum}";
            summaryColumnDescriptor8.Name = "QuantidadeColuna8";
            summaryColumnDescriptor8.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor9 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor9.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor9.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor9.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor9.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor9.DataMember = "QuantidadeColuna9";
            summaryColumnDescriptor9.Format = "{Sum}";
            summaryColumnDescriptor9.Name = "QuantidadeColuna9";
            summaryColumnDescriptor9.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor10 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor10.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor10.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor10.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor10.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor10.DataMember = "QuantidadeColuna10";
            summaryColumnDescriptor10.Format = "{Sum}";
            summaryColumnDescriptor10.Name = "QuantidadeColuna10";
            summaryColumnDescriptor10.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor11 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor11.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor11.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor11.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor11.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor11.DataMember = "QuantidadeColuna11";
            summaryColumnDescriptor11.Format = "{Sum}";
            summaryColumnDescriptor11.Name = "QuantidadeColuna11";
            summaryColumnDescriptor11.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor12 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor12.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor12.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor12.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor12.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor12.DataMember = "QuantidadeColuna12";
            summaryColumnDescriptor12.Format = "{Sum}";
            summaryColumnDescriptor12.Name = "QuantidadeColuna12";
            summaryColumnDescriptor12.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor13 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor13.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor13.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor13.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor13.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor13.DataMember = "QuantidadeColuna13";
            summaryColumnDescriptor13.Format = "{Sum}";
            summaryColumnDescriptor13.Name = "QuantidadeColuna13";
            summaryColumnDescriptor13.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor14 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor14.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor14.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor14.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor14.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor14.DataMember = "QuantidadeColuna14";
            summaryColumnDescriptor14.Format = "{Sum}";
            summaryColumnDescriptor14.Name = "QuantidadeColuna14";
            summaryColumnDescriptor14.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor15 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor15.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor15.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor15.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor15.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor15.DataMember = "QuantidadeColuna15";
            summaryColumnDescriptor15.Format = "{Sum}";
            summaryColumnDescriptor15.Name = "QuantidadeColuna15";
            summaryColumnDescriptor15.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor16 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor16.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor16.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor16.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor16.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor16.DataMember = "QuantidadeColuna16";
            summaryColumnDescriptor16.Format = "{Sum}";
            summaryColumnDescriptor16.Name = "QuantidadeColuna16";
            summaryColumnDescriptor16.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor17 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor17.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor17.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor17.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor17.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor17.DataMember = "QuantidadeColuna17";
            summaryColumnDescriptor17.Format = "{Sum}";
            summaryColumnDescriptor17.Name = "QuantidadeColuna17";
            summaryColumnDescriptor17.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor18 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor18.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor18.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor18.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor18.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor18.DataMember = "QuantidadeColuna18";
            summaryColumnDescriptor18.Format = "{Sum}";
            summaryColumnDescriptor18.Name = "QuantidadeColuna18";
            summaryColumnDescriptor18.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor19 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor19.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor19.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor19.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor19.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor19.DataMember = "QuantidadeColuna19";
            summaryColumnDescriptor19.Format = "{Sum}";
            summaryColumnDescriptor19.Name = "QuantidadeColuna19";
            summaryColumnDescriptor19.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor20 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor20.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor20.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor20.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor20.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor20.DataMember = "QuantidadeColuna20";
            summaryColumnDescriptor20.Format = "{Sum}";
            summaryColumnDescriptor20.Name = "QuantidadeColuna20";
            summaryColumnDescriptor20.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor21 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor21.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor21.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor21.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor21.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor21.DataMember = "QuantidadeColuna21";
            summaryColumnDescriptor21.Format = "{Sum}";
            summaryColumnDescriptor21.Name = "QuantidadeColuna21";
            summaryColumnDescriptor21.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor22 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor22.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor22.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor22.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor22.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor22.DataMember = "QuantidadeColuna22";
            summaryColumnDescriptor22.Format = "{Sum}";
            summaryColumnDescriptor22.Name = "QuantidadeColuna22";
            summaryColumnDescriptor22.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor23 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor23.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor23.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor23.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor23.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor23.DataMember = "QuantidadeColuna23";
            summaryColumnDescriptor23.Format = "{Sum}";
            summaryColumnDescriptor23.Name = "QuantidadeColuna23";
            summaryColumnDescriptor23.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor24 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor24.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor24.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor24.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor24.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor24.DataMember = "QuantidadeColuna24";
            summaryColumnDescriptor24.Format = "{Sum}";
            summaryColumnDescriptor24.Name = "QuantidadeColuna24";
            summaryColumnDescriptor24.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor25 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor25.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor25.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor25.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor25.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor25.DataMember = "QuantidadeColuna25";
            summaryColumnDescriptor25.Format = "{Sum}";
            summaryColumnDescriptor25.Name = "QuantidadeColuna25";
            summaryColumnDescriptor25.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor26 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor26.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor26.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor26.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor26.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor26.DataMember = "QuantidadeColuna26";
            summaryColumnDescriptor26.Format = "{Sum}";
            summaryColumnDescriptor26.Name = "QuantidadeColuna26";
            summaryColumnDescriptor26.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor27 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor27.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor27.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor27.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor27.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor27.DataMember = "QuantidadeColuna27";
            summaryColumnDescriptor27.Format = "{Sum}";
            summaryColumnDescriptor27.Name = "QuantidadeColuna27";
            summaryColumnDescriptor27.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor28 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor28.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor28.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor28.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor28.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor28.DataMember = "QuantidadeColuna28";
            summaryColumnDescriptor28.Format = "{Sum}";
            summaryColumnDescriptor28.Name = "QuantidadeColuna28";
            summaryColumnDescriptor28.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor29 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor29.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor29.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor29.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor29.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor29.DataMember = "QuantidadeColuna29";
            summaryColumnDescriptor29.Format = "{Sum}";
            summaryColumnDescriptor29.Name = "QuantidadeColuna29";
            summaryColumnDescriptor29.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor30 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor30.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor30.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor30.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor30.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor30.DataMember = "QuantidadeColuna30";
            summaryColumnDescriptor30.Format = "{Sum}";
            summaryColumnDescriptor30.Name = "QuantidadeColuna30";
            summaryColumnDescriptor30.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptorTotal = new GridSummaryColumnDescriptor();
            summaryColumnDescriptorTotal.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptorTotal.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptorTotal.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptorTotal.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptorTotal.DataMember = "Total";
            summaryColumnDescriptorTotal.Format = "{Sum}";
            summaryColumnDescriptorTotal.Name = "Total";
            summaryColumnDescriptorTotal.SummaryType = SummaryType.DoubleAggregate;

            _soma = new GridSummaryRowDescriptor("Sum", "Total",
                    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor2, summaryColumnDescriptor3, summaryColumnDescriptor4, summaryColumnDescriptor5, summaryColumnDescriptor6,
                    summaryColumnDescriptor7, summaryColumnDescriptor8, summaryColumnDescriptor9, summaryColumnDescriptor10, summaryColumnDescriptor11, summaryColumnDescriptor12,
                    summaryColumnDescriptor13, summaryColumnDescriptor14, summaryColumnDescriptor15, summaryColumnDescriptor16,summaryColumnDescriptor17, summaryColumnDescriptor18,
                    summaryColumnDescriptor19, summaryColumnDescriptor20, summaryColumnDescriptor21, summaryColumnDescriptor22, summaryColumnDescriptor23, summaryColumnDescriptor24,
                    summaryColumnDescriptor25, summaryColumnDescriptor26, summaryColumnDescriptor27, summaryColumnDescriptor28, summaryColumnDescriptor29, summaryColumnDescriptor30,
                    summaryColumnDescriptorTotal});

            _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

            try
            {
                gridGroupingControl1.TableDescriptor.SummaryRows.Add(_soma);
            }
            catch { }

            gridGroupingControl1.TableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
            gridGroupingControl1.TableDescriptor.ChildGroupOptions.ShowSummaries = false;
            gridGroupingControl1.TableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
            gridGroupingControl1.TableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl1.TableDescriptor.Appearance.RecordFieldCell.BackColor;
            gridGroupingControl1.TableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            gridGroupingControl1.TableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";

            #endregion

            try
            {
                AtualizadoLabel.Text = "Atualizado com movimentos até a data " + _listaAcessos.Max(m => m.Data).ToShortDateString();
            }
            catch { }

            gridGroupingControl1.DataSource = _listaAcessos;

            this.Cursor = Cursors.Default;
            this.Refresh();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            AtualizadoLabel.Text = string.Empty;
            leColunasGridCabecalho = new System.Xml.XmlTextReader("GridCabecalhoResumoPowerBI.xml");
            gridGroupingControl1.ApplyXmlSchema(leColunasGridCabecalho);

            leCorGridCabecalho = new System.Xml.XmlTextReader("CorGridCabecalhoResumoPowerBI.xml");
            gridGroupingControl1.ApplyXmlLookAndFeel(leCorGridCabecalho);

            leColunasGridCabecalho.Close();
            leCorGridCabecalho.Close();

            gridGroupingControl1.DataSource = new List<PowerBI.Resumo>();
            referenciaMaskedEditBox.Text = string.Empty;
            referenciaMaskedEditBox.Focus();
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            int _idEmail = 0;

            if (e.Style.TableCellIdentity.Column == null)
                return;

            if (e.Style.TableCellIdentity.DisplayElement.Kind != Syncfusion.Grouping.DisplayElementKind.ColumnHeader)
                return;

                try
            {
                int _coluna = Convert.ToInt32(Publicas.OnlyNumbers(e.Style.TableCellIdentity.Column.Name));

                if (e.Style.TableCellIdentity.Column.Name.Contains("QuantidadeColuna"))
                {
                    switch (_coluna)
                    {
                        case 1:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna1 ?? 0) != 0).Max(m => m.idEmailColuna1);
                            break;
                        case 2:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna2 ?? 0) != 0).Max(m => m.idEmailColuna2);
                            break;
                        case 3:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna3 ?? 0) != 0).Max(m => m.idEmailColuna3);
                            break;
                        case 4:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna4 ?? 0) != 0).Max(m => m.idEmailColuna4);
                            break;
                        case 5:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna5 ?? 0) != 0).Max(m => m.idEmailColuna5);
                            break;
                        case 6:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna6 ?? 0) != 0).Max(m => m.idEmailColuna6);
                            break;
                        case 7:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna7 ?? 0) != 0).Max(m => m.idEmailColuna7);
                            break;
                        case 8:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna8 ?? 0) != 0).Max(m => m.idEmailColuna8);
                            break;
                        case 9:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna9 ?? 0) != 0).Max(m => m.idEmailColuna9);
                            break;
                        case 10:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna10 ?? 0) != 0).Max(m => m.idEmailColuna10);
                            break;
                        case 11:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna11 ?? 0) != 0).Max(m => m.idEmailColuna11);
                            break;
                        case 12:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna12 ?? 0) != 0).Max(m => m.idEmailColuna12);
                            break;
                        case 13:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna13 ?? 0) != 0).Max(m => m.idEmailColuna13);
                            break;
                        case 14:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna14 ?? 0) != 0).Max(m => m.idEmailColuna14);
                            break;
                        case 15:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna15 ?? 0) != 0).Max(m => m.idEmailColuna15);
                            break;
                        case 16:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna16 ?? 0) != 0).Max(m => m.idEmailColuna16);
                            break;
                        case 17:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna17 ?? 0) != 0).Max(m => m.idEmailColuna17);
                            break;
                        case 18:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna18 ?? 0) != 0).Max(m => m.idEmailColuna18);
                            break;
                        case 19:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna19 ?? 0) != 0).Max(m => m.idEmailColuna19);
                            break;
                        case 20:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna20 ?? 0) != 0).Max(m => m.idEmailColuna20);
                            break;
                        case 21:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna21 ?? 0) != 0).Max(m => m.idEmailColuna21);
                            break;
                        case 22:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna22 ?? 0) != 0).Max(m => m.idEmailColuna22);
                            break;
                        case 23:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna23 ?? 0) != 0).Max(m => m.idEmailColuna23);
                            break;
                        case 24:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna24 ?? 0) != 0).Max(m => m.idEmailColuna24);
                            break;
                        case 25:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna25 ?? 0) != 0).Max(m => m.idEmailColuna25);
                            break;
                        case 26:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna26 ?? 0) != 0).Max(m => m.idEmailColuna26);
                            break;
                        case 27:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna27 ?? 0) != 0).Max(m => m.idEmailColuna27);
                            break;
                        case 28:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna28 ?? 0) != 0).Max(m => m.idEmailColuna28);
                            break;
                        case 29:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna29 ?? 0) != 0).Max(m => m.idEmailColuna29);
                            break;
                        case 30:
                            _idEmail = _listaAcessos.Where(w => (w.QuantidadeColuna30 ?? 0) != 0).Max(m => m.idEmailColuna30);
                            break;
                    }

                    foreach (var item in _listaUsuarios.Where(w => w.IdEmail == _idEmail))
                    {
                        e.Style.CellTipText = e.Style.CellTipText + item.Nome + Environment.NewLine;
                    }

                }
            }
            catch { }
        }
    }
}
