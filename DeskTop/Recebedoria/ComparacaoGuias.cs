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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Suportte.Recebedoria
{
    public partial class ComparacaoGuias : Form
    {
        public ComparacaoGuias()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                VigenciaDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
                FinaldateTimePickerAdv.BackColor = empresaComboBoxAdv.BackColor;

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
                    VigenciaDateTimePicker.Style = VisualStyle.Office2016Black;
                    FinaldateTimePickerAdv.Style = VisualStyle.Office2016Black;
                    RecolherCheckBox.ForeColor = Publicas._fonte;
                }
            }
            Publicas._mensagemSistema = string.Empty;

        }

        Classes.Empresa _empresa;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasAutorizadas;
        List<SIGOM> _lista;
        int _leftAux = 0;

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

        private void ComparacaoGuias_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);
            VigenciaDateTimePicker.Value = DateTime.Now.Date;

            ExportaExcelPictureBox.Visible = Publicas._usuario.PodeExportarSigomExcel;

            _listaEmpresas = new EmpresaBO().Listar(false);
            _listaEmpresasAutorizadas = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._idUsuario);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

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

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl1.Table.DefaultRecordRowHeight = 22;

            GridSummaryRowDescriptor _soma;
            GridSummaryColumnDescriptor summaryColumnDescriptor5 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor5.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor5.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor5.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor5.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor5.DataMember = "CodigoLinha";
            summaryColumnDescriptor5.Format = "{Count}";
            summaryColumnDescriptor5.Name = "CodigoLinha";
            summaryColumnDescriptor5.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor.DataMember = "QuantidadeGirosSigom";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "QuantidadeGirosSigom";
            summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor2 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor2.Appearance.AnySummaryCell.Format = "n0";
            summaryColumnDescriptor2.Appearance.GroupCaptionSummaryCell.Format = "n0";
            summaryColumnDescriptor2.DataMember = "QuantidadeGirosGlobus";
            summaryColumnDescriptor2.Format = "{Sum}";
            summaryColumnDescriptor2.Name = "QuantidadeGirosGlobus";
            summaryColumnDescriptor2.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor3 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor3.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor3.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor3.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor3.DataMember = "ValorSigom";
            summaryColumnDescriptor3.Format = "{Sum}";
            summaryColumnDescriptor3.Name = "ValorSigom";
            summaryColumnDescriptor3.SummaryType = SummaryType.DoubleAggregate;

            GridSummaryColumnDescriptor summaryColumnDescriptor4 = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor4.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            summaryColumnDescriptor4.Appearance.AnySummaryCell.Format = "n2";
            summaryColumnDescriptor4.Appearance.GroupCaptionSummaryCell.Format = "n2";
            summaryColumnDescriptor4.DataMember = "ValorGlobus";
            summaryColumnDescriptor4.Format = "{Sum}";
            summaryColumnDescriptor4.Name = "ValorGlobus";
            summaryColumnDescriptor4.SummaryType = SummaryType.DoubleAggregate;
            

            _soma = new GridSummaryRowDescriptor("Sum", "Total geral",
                new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor2, summaryColumnDescriptor3,
                                                    summaryColumnDescriptor4, summaryColumnDescriptor5
                });

            _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

            gridGroupingControl1.TableDescriptor.SummaryRows.Add(_soma);

            this.gridGroupingControl1.ChildGroupOptions.ShowCaptionSummaryCells = true;
            this.gridGroupingControl1.ChildGroupOptions.ShowSummaries = false;
            this.gridGroupingControl1.ChildGroupOptions.CaptionSummaryRow = "Sum";
            this.gridGroupingControl1.Appearance.GroupCaptionCell.BackColor = this.gridGroupingControl1.Appearance.RecordFieldCell.BackColor;
            this.gridGroupingControl1.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            this.gridGroupingControl1.Appearance.GroupCaptionCell.CellType = "Static";

            ResumoPanel.Width = 20;
            ResumoPanel.Height = ComparaPanel.Height;
            _leftAux = ResumoPanel.Left;
            //ResumoPanel.Left = ComparaPanel.Width + 5;
            ResumoPanel.Visible = false;
            ComparaPanel.Width = ComparaPanel.Width + 25;

        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                VigenciaDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void VigenciaDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                FinaldateTimePickerAdv.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void FinaldateTimePickerAdv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                VigenciaDateTimePicker.Focus();
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

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void VigenciaDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
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

            if (_listaEmpresasAutorizadas.Where(w => w.IdEmpresa == _empresa.IdEmpresa && w.EmpresaAutoriza).Count() == 0)
            {
                new Notificacoes.Mensagem("Usuário não autorizado para está empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }
        }
        
        private void VigenciaDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            VigenciaDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            FinaldateTimePickerAdv.Value = VigenciaDateTimePicker.Value;
        }

        private void FinaldateTimePickerAdv_Validating(object sender, CancelEventArgs e)
        {
            FinaldateTimePickerAdv.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (VigenciaDateTimePicker.Value.Date > FinaldateTimePickerAdv.Value.Date)
            {
                new Notificacoes.Mensagem("Data final deve ser maior que a data inicial.", Publicas.TipoMensagem.Alerta).ShowDialog();
                FinaldateTimePickerAdv.Focus();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Comparou Globus x Sigom da empresa " + empresaComboBoxAdv.Text + 
                " periodo de " + VigenciaDateTimePicker.Value.Date.ToShortDateString() + " até " + 
                FinaldateTimePickerAdv.Value.Date.ToShortDateString();
            _log.Tela = "Comparar Globus x Sigom";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            int empresa = (_empresa.IdEmpresa == 2 || _empresa.IdEmpresa == 3 ? 3 :
                        (_empresa.IdEmpresa == 10 ? 5 :
                        (_empresa.IdEmpresa == 7 ? 26 : 0)));

            mensagemSistemaLabel.Text = "Aguarde," + Environment.NewLine + "Pesquisando guias Globus";
            this.Refresh();
            List<SIGOM> _listaGlobus = new SIGOMBO().ListarGlobus(_empresa.CodigoEmpresaGlobus, VigenciaDateTimePicker.Value.Date, FinaldateTimePickerAdv.Value.Date);

            mensagemSistemaLabel.Text = "Aguarde," + Environment.NewLine + "Pesquisando guias Sigom";
            this.Refresh();
            List<SIGOM> _listaSigom = new SIGOMBO().ListarSigon(empresa, VigenciaDateTimePicker.Value.Date, FinaldateTimePickerAdv.Value.Date);

            _lista = new List<SIGOM>();

            mensagemSistemaLabel.Text = "Aguarde," + Environment.NewLine + "Comparando";
            this.Refresh();

            foreach (var itemGlobus in _listaGlobus.Where(w => !w.Lido).OrderBy(o => o.CodigoImportacaoGlobus))
            {
                if (itemGlobus.TipoPagtoGlobus.ToUpper().StartsWith("INT"))
                {
                    foreach (var itemSigom in _listaSigom.Where(w => !w.Lido &&
                                                                w.CodigoLinha == itemGlobus.CodigoLinha &&
                                                                w.Prefixo == itemGlobus.Prefixo &&
                                                                w.TipoPagtoSigom.ToUpper() == "INTEGRACAO" &&
                                                                w.IdTipoPagtoSigom == itemGlobus.CodigoImportacaoGlobus &&
                                                                (w.InicioJornada == itemGlobus.InicioJornada || w.InicioJornadaSigom == itemGlobus.InicioJornada ||
                                                                (w.InicioJornada >= itemGlobus.InicioJornada && w.InicioJornada <= itemGlobus.InicioJornada.AddMinutes(5)) ||
                                                                (w.InicioJornadaSigom >= itemGlobus.InicioJornada && w.InicioJornadaSigom <= itemGlobus.InicioJornada.AddMinutes(5)) ||
                                                                (itemGlobus.InicioJornada >= w.InicioJornada && itemGlobus.InicioJornada <= w.InicioJornada.AddMinutes(5)) ||
                                                                (itemGlobus.InicioJornada >= w.InicioJornadaSigom && itemGlobus.InicioJornada <= w.InicioJornadaSigom.AddMinutes(5))
                                                                ))
                                                         .OrderBy(o => o.IdTipoPagtoGlobus))
                    {
                        _lista.Add(new SIGOM()
                        {
                            CodigoLinha = itemGlobus.CodigoLinha,
                            Prefixo = itemGlobus.Prefixo,
                            InicioJornada = itemGlobus.InicioJornada,
                            InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                            FimJornadaSigom = itemSigom.FimJornadaSigom,
                            FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                            MotoristaSigom = itemSigom.MotoristaSigom,
                            MotoristaGlobus = itemGlobus.MotoristaGlobus,
                            IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                            IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                            TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                            TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                            QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                            QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                            ValorSigom = itemSigom.ValorSigom,
                            ValorGlobus = itemGlobus.ValorGlobus,
                            GuiaGlobus = itemGlobus.GuiaGlobus,
                            CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                            DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemSigom.QuantidadeGirosSigom,
                            DiferencaValor = itemGlobus.ValorGlobus != itemSigom.ValorSigom
                        });

                        itemGlobus.Lido = true;
                        itemSigom.Lido = true;

                    }
                }
                else
                {
                    foreach (var itemSigom in _listaSigom.Where(w => !w.Lido &&
                                                                w.CodigoLinha == itemGlobus.CodigoLinha &&
                                                                w.Prefixo == itemGlobus.Prefixo &&
                                                                w.IdTipoPagtoSigom == itemGlobus.CodigoImportacaoGlobus &&
                                                                w.TipoPagtoSigom.ToUpper() != "INTEGRACAO" &&
                                                                (w.InicioJornada == itemGlobus.InicioJornada || w.InicioJornadaSigom == itemGlobus.InicioJornada ||
                                                                (w.InicioJornada >= itemGlobus.InicioJornada && w.InicioJornada <= itemGlobus.InicioJornada.AddMinutes(5)) ||
                                                                (w.InicioJornadaSigom >= itemGlobus.InicioJornada && w.InicioJornadaSigom <= itemGlobus.InicioJornada.AddMinutes(5)) ||
                                                                (itemGlobus.InicioJornada >= w.InicioJornada && itemGlobus.InicioJornada <= w.InicioJornada.AddMinutes(5)) ||
                                                                (itemGlobus.InicioJornada >= w.InicioJornadaSigom && itemGlobus.InicioJornada <= w.InicioJornadaSigom.AddMinutes(5))
                                                                ))
                                                         .OrderBy(o => o.IdTipoPagtoGlobus))
                    {
                        _lista.Add(new SIGOM()
                        {
                            CodigoLinha = itemGlobus.CodigoLinha,
                            Prefixo = itemGlobus.Prefixo,
                            InicioJornada = itemGlobus.InicioJornada,
                            InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                            FimJornadaSigom = itemSigom.FimJornadaSigom,
                            FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                            MotoristaSigom = itemSigom.MotoristaSigom,
                            MotoristaGlobus = itemGlobus.MotoristaGlobus,
                            IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                            IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                            TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                            TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                            QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                            QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                            ValorSigom = itemSigom.ValorSigom,
                            ValorGlobus = itemGlobus.ValorGlobus,
                            GuiaGlobus = itemGlobus.GuiaGlobus,
                            CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                            DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemSigom.QuantidadeGirosSigom,
                            DiferencaValor = itemGlobus.ValorGlobus != itemSigom.ValorSigom
                        });

                        itemGlobus.Lido = true;
                        itemSigom.Lido = true;
                    }
                }

                if (!itemGlobus.Lido)
                {
                    _lista.Add(new SIGOM()
                    {
                        CodigoLinha = itemGlobus.CodigoLinha,
                        Prefixo = itemGlobus.Prefixo,
                        InicioJornada = itemGlobus.InicioJornada,
                        FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                        MotoristaGlobus = itemGlobus.MotoristaGlobus,
                        IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                        TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                        QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                        ValorGlobus = itemGlobus.ValorGlobus,
                        GuiaGlobus = itemGlobus.GuiaGlobus,
                        CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                        DiferencaValor = true,
                        DiferencaQuantidade = true
                    });
                }
            }

            foreach (var itemSigom in _listaSigom.Where(w => !w.Lido))
            {
                Linha _linha = new LinhaBO().Consultar(itemSigom.CodigoLinha,false);

                if (_linha.Empresa == _empresa.CodigoEmpresaGlobus)
                {
                    _lista.Add(new SIGOM()
                    {
                        CodigoLinha = itemSigom.CodigoLinha,
                        Prefixo = itemSigom.Prefixo,
                        InicioJornada = itemSigom.InicioJornada,
                        InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                        FimJornadaSigom = itemSigom.FimJornadaSigom,
                        MotoristaSigom = itemSigom.MotoristaSigom,
                        IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                        TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                        QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                        ValorSigom = itemSigom.ValorSigom,
                        DiferencaValor = true,
                        DiferencaQuantidade = true
                    });
                }
            }

            QuantidadeLinhasLabel.Text = _lista.Where(w => w.CodigoLinha != null)
                                               .GroupBy(g => g.CodigoLinha).Count().ToString();
            QuantidadeViagemLabel.Text = _lista.Where(w => w.InicioJornada.Date != DateTime.MinValue.Date)
                                               .GroupBy(g => g.InicioJornada)
                                               .Count().ToString();

            TotalViagensSigomLabel.Text = _lista.Where(w => w.InicioJornadaSigom.Date != DateTime.MinValue.Date)
                                   .GroupBy(g => g.InicioJornadaSigom)
                                   .Count().ToString();

            TotalPagantesGlobusLabel.Text = string.Format("{0,12:N2}", 
                _lista
                .Where(w => w.TipoPagtoGlobus.Contains("PAGA"))
                .Sum(s => s.ValorGlobus));

            TotalPagantesSigomLabel.Text = string.Format("{0,12:N2}", _lista
                .Where(w => w.IdTipoPagtoSigom == 0)
                .Sum(s => s.ValorSigom));


            gridGroupingControl1.DataSource = _lista.Where(w => w.CodigoLinha != null).ToList();
            mensagemSistemaLabel.Text = "";
            ComparaPanel.Width = ComparaPanel.Width - 25;
            ResumoPanel.Visible = true;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExportaExcelPictureBox_Click(object sender, EventArgs e)
        {
            string caminho =  (Environment.MachineName.ToUpper().Contains("CORPTS") || Environment.MachineName.ToUpper().Contains("CORPRDP") ? @"d:\portalNiff\Excel\" : @"c:\portalNiff\Excel\") ;

            if (!System.IO.Directory.Exists(caminho))
            {
                System.IO.Directory.CreateDirectory(caminho);
            }

            mensagemSistemaLabel.Text = "Aguarde, Exportando dados para o Excel";
            this.Cursor = Cursors.WaitCursor;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            object misValue = System.Reflection.Missing.Value;
            string nomeArquivo = "ComparacaoSigom" + _empresa.CodigoEmpresaGlobus.Replace("/", "_")
                               + "_" + VigenciaDateTimePicker.Value.ToShortDateString().Replace("/", "_")
                               + "_" + FinaldateTimePickerAdv.Value.ToShortDateString().Replace("/", "_");

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);


            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int linha = 1;
            int col = 1;


            #region titulo colunas
            xlWorkSheet.Cells[linha, col] = "Linha ";
            col++;

            xlWorkSheet.Cells[linha, col] = "Carro ";
            col++;
            xlWorkSheet.Cells[linha, col] = "Guia no Globus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Inicio Jornada";
            col++;
            xlWorkSheet.Cells[linha, col] = "Final Jornada";
            col++;
            xlWorkSheet.Cells[linha, col] = "Tipo de Pagamento Sigom";
            col++;
            xlWorkSheet.Cells[linha, col] = "Tipo de Pagamento Globus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Quantidade Sigom";
            col++;
            xlWorkSheet.Cells[linha, col] = "Quantidade Globus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor Sigom";
            col++;
            xlWorkSheet.Cells[linha, col] = "Valor Globus";
            col++;
            xlWorkSheet.Cells[linha, col] = "Responsável Sigom";
            col++;
            xlWorkSheet.Cells[linha, col] = "Responsável Globus";
            col++;
            
            #endregion

            foreach (var itemC in _lista)
            {
                col = 1;
                linha++;

                #region Cabeçalho da Nota
                xlWorkSheet.Cells[linha, col] = itemC.CodigoLinha;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.Prefixo;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.GuiaGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = " " + itemC.InicioJornada.ToShortDateString() + " " + itemC.InicioJornada.ToShortTimeString() + " " ;
                col++;
                xlWorkSheet.Cells[linha, col] = " " + itemC.FimJornadaGlobus.ToShortDateString() + " " + itemC.FimJornadaGlobus.ToShortTimeString() + " ";
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.TipoPagtoSigom;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.TipoPagtoGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.QuantidadeGirosSigom;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.QuantidadeGirosGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorSigom;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.ValorGlobus;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.MotoristaSigom;
                col++;
                xlWorkSheet.Cells[linha, col] = itemC.MotoristaGlobus;
                #endregion

            }

            xlWorkSheet.Columns.AutoFit();
            xlWorkBook.SaveAs(caminho + nomeArquivo + ".xlsx",
                            Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
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

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Exportou Globus x Sigom da empresa " + empresaComboBoxAdv.Text +
                " periodo de " + VigenciaDateTimePicker.Value.Date.ToShortDateString() + " até " +
                FinaldateTimePickerAdv.Value.Date.ToShortDateString() + " para excel";
            _log.Tela = "Comparar Globus x Sigom";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            new Notificacoes.Mensagem("Arquivo " + nomeArquivo + " gerado com sucesso." + Environment.NewLine +
                " Salvo em: " + caminho, Publicas.TipoMensagem.Alerta).ShowDialog();
        }

        private void QtdeLinhasPanel_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = ((Panel)sender).ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle((Publicas._TemaBlack ? Pens.Gray : Pens.DarkGray), rect);
        }

        private void BotaoDashboardLabel_Click(object sender, EventArgs e)
        {
            if (ResumoPanel.Width == 20)
            {
                ResumoPanel.Width = 201;
                ResumoPanel.Left = ResumoPanel.Left - (ResumoPanel.Width - 25);
                BotaoDashboardLabel.Text = "8";
            }
            else
            {
                ResumoPanel.Width = 20;
                ResumoPanel.Left = _leftAux;// ComparaPanel.Width + 5;
                BotaoDashboardLabel.Text = "7";
            }
        }

        private void BotaoDashboardLabel_MouseHover(object sender, EventArgs e)
        {
            if (ResumoPanel.Width == 20)
            {
                ResumoPanel.Width = 201;
                ResumoPanel.Left = ResumoPanel.Left - (ResumoPanel.Width - 25);
                BotaoDashboardLabel.Text = "8";
            }
        }

        private void BotaoDashboardLabel_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void RecolherCheckBox_Click(object sender, EventArgs e)
        {
            if (RecolherCheckBox.Checked)
                gridGroupingControl1.Table.CollapseAllGroups();
            else
                gridGroupingControl1.Table.ExpandAllGroups();
        }
    }
}
