using Classes;
using Negocio;
using Suportte.Notificacoes;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Suportte.Diversos
{
    public partial class ValoresADescontar : Form
    {
        public ValoresADescontar()
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

        List<EmpresaDoUsuario> _listaEmpresas;
        List<OcorrenciasGlobus> _listaOcorrenciasJustificadas;
        List<OcorrenciasGlobus> _listaOcorrenciasInjustificadas;

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

        private void ValoresADescontar_Load(object sender, EventArgs e)
        {

            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;

            #region Empresa

            _listaEmpresas = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._idUsuario);

            _listaEmpresas.ForEach(u => u.EmpresaAutoriza = false);
            empresasGridDataBoundGrid.DataSource = _listaEmpresas;
            empresasGridDataBoundGrid.AllowProportionalColumnSizing = false;
            empresasGridDataBoundGrid.SortIconPlacement = SortIconPlacement.Left;
            empresasGridDataBoundGrid.TopLevelGroupOptions.ShowFilterBar = true;
            empresasGridDataBoundGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            empresasGridDataBoundGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            empresasGridDataBoundGrid.RecordNavigationBar.Label = "Empresas";
            
            filter.WireGrid(this.empresasGridDataBoundGrid);
            
            for (int i = 0; i < empresasGridDataBoundGrid.TableDescriptor.Columns.Count; i++)
            {
                empresasGridDataBoundGrid.TableDescriptor.Columns[i].ReadOnly = false;
                if (i > 0)
                    empresasGridDataBoundGrid.TableDescriptor.Columns[i].ReadOnly = true;

                empresasGridDataBoundGrid.TableDescriptor.Columns[i].AllowFilter = true;
                empresasGridDataBoundGrid.TableDescriptor.Columns[i].AllowSort = true;
                empresasGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                empresasGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                empresasGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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


            this.empresasGridDataBoundGrid.SetMetroStyle(metroColor);
            // para permitir editar dados.
            this.empresasGridDataBoundGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            this.empresasGridDataBoundGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.empresasGridDataBoundGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.empresasGridDataBoundGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            #endregion

            #region ocorrências
            _listaOcorrenciasJustificadas = new OcorrenciasGlobusBO().Listar();
            _listaOcorrenciasInjustificadas = new OcorrenciasGlobusBO().Listar();

            #region marcar as ocorrências default 

            foreach (var item in _listaOcorrenciasJustificadas.Where(w => ",2,3,10,28,117,120,173,209,222,230,233,322,344,374,506,507,521,526,527,535,560,579,580,584,592,593,601,602,902,903,".Contains( "," + w.Codigo.ToString() + ",")))
            {
                item.Selecionado = true;
            }

            foreach (var item in _listaOcorrenciasInjustificadas.Where(w => ",209,534,583,".Contains( "," + w.Codigo.ToString() + ",")))
            {
                item.Selecionado = true;
            }
            

            #endregion

            ocorrenciasJustificadasGridDataBoundGrid.DataSource = _listaOcorrenciasJustificadas;
            ocorrenciasJustificadasGridDataBoundGrid.AllowProportionalColumnSizing = false;
            ocorrenciasJustificadasGridDataBoundGrid.SortIconPlacement = SortIconPlacement.Left;
            ocorrenciasJustificadasGridDataBoundGrid.TopLevelGroupOptions.ShowFilterBar = true;
            ocorrenciasJustificadasGridDataBoundGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            ocorrenciasJustificadasGridDataBoundGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            ocorrenciasJustificadasGridDataBoundGrid.RecordNavigationBar.Label = "Ocorrências";
            ocorrenciasJustificadasGridDataBoundGrid.TableControl.CellToolTip.Active = true;
            filter.WireGrid(this.ocorrenciasJustificadasGridDataBoundGrid);

            for (int i = 0; i < ocorrenciasJustificadasGridDataBoundGrid.TableDescriptor.Columns.Count; i++)
            {
                ocorrenciasJustificadasGridDataBoundGrid.TableDescriptor.Columns[i].ReadOnly = false;
                if (i > 0)
                    ocorrenciasJustificadasGridDataBoundGrid.TableDescriptor.Columns[i].ReadOnly = true;

                ocorrenciasJustificadasGridDataBoundGrid.TableDescriptor.Columns[i].AllowFilter = true;
                ocorrenciasJustificadasGridDataBoundGrid.TableDescriptor.Columns[i].AllowSort = true;
                ocorrenciasJustificadasGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                ocorrenciasJustificadasGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                ocorrenciasJustificadasGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;

            }

            metroColor = new GridMetroColors();
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

            this.ocorrenciasJustificadasGridDataBoundGrid.SetMetroStyle(metroColor);

            this.ocorrenciasJustificadasGridDataBoundGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            this.ocorrenciasJustificadasGridDataBoundGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.ocorrenciasJustificadasGridDataBoundGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.ocorrenciasJustificadasGridDataBoundGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;


            ocorrenciasInjustificadasGridDataBoundGrid.DataSource = _listaOcorrenciasInjustificadas;
            ocorrenciasInjustificadasGridDataBoundGrid.AllowProportionalColumnSizing = false;
            ocorrenciasInjustificadasGridDataBoundGrid.SortIconPlacement = SortIconPlacement.Left;
            ocorrenciasInjustificadasGridDataBoundGrid.TopLevelGroupOptions.ShowFilterBar = true;
            ocorrenciasInjustificadasGridDataBoundGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            ocorrenciasInjustificadasGridDataBoundGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            ocorrenciasInjustificadasGridDataBoundGrid.RecordNavigationBar.Label = "Ocorrências";
            ocorrenciasInjustificadasGridDataBoundGrid.TableControl.CellToolTip.Active = true;
            filter.WireGrid(this.ocorrenciasInjustificadasGridDataBoundGrid);

            for (int i = 0; i < ocorrenciasInjustificadasGridDataBoundGrid.TableDescriptor.Columns.Count; i++)
            {
                ocorrenciasInjustificadasGridDataBoundGrid.TableDescriptor.Columns[i].ReadOnly = false;
                if (i > 0)
                    ocorrenciasInjustificadasGridDataBoundGrid.TableDescriptor.Columns[i].ReadOnly = true;

                ocorrenciasInjustificadasGridDataBoundGrid.TableDescriptor.Columns[i].AllowFilter = true;
                ocorrenciasInjustificadasGridDataBoundGrid.TableDescriptor.Columns[i].AllowSort = true;
                ocorrenciasInjustificadasGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                ocorrenciasInjustificadasGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                ocorrenciasInjustificadasGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;

            }

            metroColor = new GridMetroColors();
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

            this.ocorrenciasInjustificadasGridDataBoundGrid.SetMetroStyle(metroColor);

            this.ocorrenciasInjustificadasGridDataBoundGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            this.ocorrenciasInjustificadasGridDataBoundGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.ocorrenciasInjustificadasGridDataBoundGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.ocorrenciasInjustificadasGridDataBoundGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            #endregion

            empresasGridDataBoundGrid.Focus();
        }

        private void aberturaDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void aberturaDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
        }

        private void inicioDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                finalDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ocorrenciasInjustificadasGridDataBoundGrid.Focus();
            }
        }

        private void finalDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                calcularCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                inicioDateTimePicker.Focus();
            }
        }

        private void arquivoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                calcularCheckBox.Focus();
            }
        }

        private void ocorrenciasInjustificadasGridDataBoundGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                inicioDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ocorrenciasJustificadasGridDataBoundGrid.Focus();
            }
        }

        private void ocorrenciasJustificadasGridDataBoundGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ocorrenciasInjustificadasGridDataBoundGrid.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresasGridDataBoundGrid.Focus();
            }
        }

        private void empresasGridDataBoundGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ocorrenciasJustificadasGridDataBoundGrid.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresasGridDataBoundGrid.Focus();
            }
        }

        private void calcularCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                arquivoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                finalDateTimePicker.Focus();
            }
        }

        private void arquivoTextBox_Enter(object sender, EventArgs e)
        {
            Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfo1 = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();

            toolTipInfo1.Header.Text = "Informação";
            toolTipInfo1.Body.Text = "Informe o diretório e o nome do arquivo Excel que deseja gravar.";

            superToolTip.SetToolTip(arquivoTextBox, toolTipInfo1);
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void arquivoTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(arquivoTextBox.Text.Trim()))
            {
                saveFileDialog.Filter = "Arquivos de excel (*.xlsx, *.xls) | *.xlsx; *.xls";

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    new Notificacoes.Mensagem("Nenhum arquivo selecionado!", Publicas.TipoMensagem.Alerta).ShowDialog();
                    arquivoTextBox.Focus();
                    return;
                }

                arquivoTextBox.Text = saveFileDialog.FileName;
            }


            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            _listaEmpresas.ForEach(u => u.EmpresaAutoriza = false);
            empresasGridDataBoundGrid.DataSource = _listaEmpresas;

            _listaOcorrenciasInjustificadas.ForEach(u => u.Selecionado = false);
            _listaOcorrenciasJustificadas.ForEach(u => u.Selecionado = false);

            ocorrenciasInjustificadasGridDataBoundGrid.DataSource = _listaOcorrenciasInjustificadas;
            ocorrenciasJustificadasGridDataBoundGrid.DataSource = _listaOcorrenciasJustificadas;

            inicioDateTimePicker.Value = DateTime.Now;
            finalDateTimePicker.Value = DateTime.Now;
            arquivoTextBox.Text = string.Empty;
            calcularCheckBox.Checked = true;

            empresasGridDataBoundGrid.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            List<DescontoBeneficios> _lista;

            saveFileDialog.Filter = "Arquivos de excel (*.xlsx, *.xls) | *.xlsx; *.xls";

            if (string.IsNullOrEmpty(arquivoTextBox.Text.Trim()))
            {
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    new Notificacoes.Mensagem("Nenhum arquivo selecionado!", Publicas.TipoMensagem.Alerta).ShowDialog();
                    arquivoTextBox.Focus();
                    return;
                }

                arquivoTextBox.Text = saveFileDialog.FileName;
            }

            if (!arquivoTextBox.Text.Contains(".xls"))
                arquivoTextBox.Text = arquivoTextBox.Text + ".xls";

            if (_listaEmpresas.Where(w => w.EmpresaAutoriza).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhum empresa selecionado!", Publicas.TipoMensagem.Alerta).ShowDialog();
                arquivoTextBox.Focus();
                return;
            }

            if (_listaOcorrenciasJustificadas.Where(w => w.Selecionado).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma ocorrência de faltas justificadas selecionada!", Publicas.TipoMensagem.Alerta).ShowDialog();
                arquivoTextBox.Focus();
                return;
            }

            if (_listaOcorrenciasInjustificadas.Where(w => w.Selecionado).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma ocorrência de faltas injustificadas selecionada!", Publicas.TipoMensagem.Alerta).ShowDialog();
                arquivoTextBox.Focus();
                return;
            }

            mensagemSistemaLabel.Text = "Pesquisando faltas. Aguarde ...";
            if (calcularCheckBox.Checked)
            {
                _lista = new DescontoBeneficioBO().CalcularDescontoPorFerias(inicioDateTimePicker.Value, finalDateTimePicker.Value,
                                                                             _listaEmpresas.Where(w => w.EmpresaAutoriza).ToList(),
                                                                             _listaOcorrenciasJustificadas.Where(w => w.Selecionado).ToList(),
                                                                             _listaOcorrenciasInjustificadas.Where(w => w.Selecionado).ToList());
            }
            else
            {
                _lista = new DescontoBeneficioBO().CalcularDesconto(inicioDateTimePicker.Value, finalDateTimePicker.Value,
                                                             _listaEmpresas.Where(w => w.EmpresaAutoriza).ToList(),
                                                             _listaOcorrenciasJustificadas.Where(w => w.Selecionado).ToList(),
                                                             _listaOcorrenciasInjustificadas.Where(w => w.Selecionado).ToList());
            }

            if (_lista.Count == 0)
            {
                mensagemSistemaLabel.Text = "";
                new Notificacoes.Mensagem("Nenhuma informação encontrada para os dados informados!", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            int linha = 1;
            Excel.Workbook objBook = null;
            Excel.Worksheet objSheet = null; // Criando objeto Workbook
            Excel.Application ExcelApp = new Excel.Application();
            object misValue = System.Reflection.Missing.Value;

            try
            {
                
                try
                {
                    objBook = ExcelApp.Workbooks.Open(arquivoTextBox.Text);

                    try
                    {
                        objSheet = (Excel.Worksheet)objBook.Worksheets.Add(finalDateTimePicker.Value.Year.ToString() + "_" + finalDateTimePicker.Value.Month.ToString("00"));
                    }
                    catch
                    {
                        objSheet = (Excel.Worksheet)objBook.Worksheets[finalDateTimePicker.Value.Year.ToString() + "_" + finalDateTimePicker.Value.Month.ToString("00")];
                    }
                }
                catch
                {
                    objBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value); // Criando objeto Workbook
                    objSheet = (Excel.Worksheet)objBook.Worksheets.Add();
                    objSheet.Name = finalDateTimePicker.Value.Year.ToString() + "_" + finalDateTimePicker.Value.Month.ToString("00");
                }
                
                progressBar.Visible = true;
                progressBar.TextVisible = true;
                progressBar.Maximum = _lista.Count;
                progressBar.Value = 0;
                progressBar.Step = 1;

                mensagemSistemaLabel.Text = "Preparando planilha Excel. Aguarde ...";
                if (calcularCheckBox.Checked)
                {
                    objSheet.Cells[linha, 1] = "Empresa";
                    objSheet.Cells[linha, 2] = "Registro";
                    objSheet.Cells[linha, 3] = "Nome funcionário";
                    objSheet.Cells[linha, 4] = "Inicio Férias";
                    objSheet.Cells[linha, 5] = "Fim Férias";
                    objSheet.Cells[linha, 6] = "Inicio Aquisição";
                    objSheet.Cells[linha, 7] = "Fim Aquisição";
                    objSheet.Cells[linha, 8] = "Data Nascimento";
                    objSheet.Cells[linha, 9] = "CPF";
                    objSheet.Cells[linha, 10] = "Quantidade Faltas Injustificadas";
                    objSheet.Cells[linha, 11] = "Valor A Descontar Faltas Injustificadas";
                    objSheet.Cells[linha, 12] = "Quantidade Faltas Justificadas";
                    objSheet.Cells[linha, 13] = "Quantidade Faltas Descontadas Justificadas";
                    objSheet.Cells[linha, 14] = "Valor A Descontar Faltas Justificadas";
                    objSheet.Cells[linha, 15] = "Total";
                    objSheet.Cells[linha, 16] = "Período de " + inicioDateTimePicker.Value.ToShortDateString() + " a " + finalDateTimePicker.Value.ToShortDateString();
                    
                    foreach (var item in _lista)
                    {
                        linha = linha + 1;
                        progressBar.Value = progressBar.Value + linha;

                        objSheet.Cells[linha, 1] = item.Empresa;
                        objSheet.Cells[linha, 2] = item.Codigo;
                        objSheet.Cells[linha, 3] = item.Nome;
                        objSheet.Cells[linha, 4] = item.InicioFerias;
                        objSheet.Cells[linha, 5] = item.FimFerias;
                        objSheet.Cells[linha, 6] = item.InicioAquisicao;
                        objSheet.Cells[linha, 7] = item.FimAquisicao;
                        objSheet.Cells[linha, 8] = item.DataNascimento;
                        objSheet.Cells[linha, 9] = item.CPF;
                        objSheet.Cells[linha, 10] = item.QuantidadeFaltasDescontadasInjustificadas;
                        objSheet.Cells[linha, 11] = item.ValorADescontarFaltasInjustificadas;
                        objSheet.Cells[linha, 12] = item.QuantidadeFaltasJustificadas;
                        objSheet.Cells[linha, 13] = item.QuantidadeFaltasDescontadasJustificadas;
                        objSheet.Cells[linha, 14] = item.ValorADescontarFaltasJustificadas;
                        objSheet.Cells[linha, 15] = item.ValorADescontarFaltasJustificadas + item.ValorADescontarFaltasInjustificadas;

                    }
                }
                else
                {
                    objSheet.Cells[linha, 1] = "Empresa";
                    objSheet.Cells[linha, 2] = "Registro";
                    objSheet.Cells[linha, 3] = "Nome funcionário";
                    objSheet.Cells[linha, 4] = "Data Nascimento";
                    objSheet.Cells[linha, 5] = "CPF";
                    objSheet.Cells[linha, 6] = "Quantidade Faltas Injustificadas";
                    objSheet.Cells[linha, 7] = "Valor A Descontar Faltas Injustificadas";
                    objSheet.Cells[linha, 8] = "Quantidade Faltas Justificadas";
                    objSheet.Cells[linha, 9] = "Quantidade Faltas Descontadas Justificadas";
                    objSheet.Cells[linha, 10] = "Valor A Descontar Faltas Justificadas";
                    objSheet.Cells[linha, 11] = "Total";
                    objSheet.Cells[linha, 12] = "Período de " + inicioDateTimePicker.Value.ToShortDateString() + " a " + finalDateTimePicker.Value.ToShortDateString();
                    
                    foreach (var item in _lista)
                    {
                        linha = linha + 1;
                        progressBar.Value = progressBar.Value + linha;

                        objSheet.Cells[linha, 1] = item.Empresa;
                        objSheet.Cells[linha, 2] = item.Codigo;
                        objSheet.Cells[linha, 3] = item.Nome;
                        objSheet.Cells[linha, 4] = item.DataNascimento;
                        objSheet.Cells[linha, 5] = item.CPF;
                        objSheet.Cells[linha, 6] = item.QuantidadeFaltasDescontadasInjustificadas;
                        objSheet.Cells[linha, 7] = item.ValorADescontarFaltasInjustificadas;
                        objSheet.Cells[linha, 8] = item.QuantidadeFaltasJustificadas;
                        objSheet.Cells[linha, 9] = item.QuantidadeFaltasDescontadasJustificadas;
                        objSheet.Cells[linha, 10] = item.ValorADescontarFaltasJustificadas;
                        objSheet.Cells[linha, 11] = item.ValorADescontarFaltasJustificadas + item.ValorADescontarFaltasInjustificadas;

                    }
                }
                //Salvando informações
                mensagemSistemaLabel.Text = "";

                objBook.SaveAs(arquivoTextBox.Text, Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, false, misValue,
                               Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                progressBar.Visible = false;
                new Notificacoes.Mensagem("Arquivo gerado com sucesso", Publicas.TipoMensagem.Sucesso).ShowDialog();
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                new Notificacoes.Mensagem("Erro: " + ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
            }
            finally
            {

                //Eliminando o Excel da memória
                objBook.Close(true, misValue, misValue);

                liberarObjetos(objSheet);
                liberarObjetos(objBook);
                liberarObjetos(ExcelApp);

            }

        }

        private void liberarObjetos(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                new Notificacoes.Mensagem("Ocorreu um erro durante a liberação do excel." + Environment.NewLine + ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
            }
            finally
            {
                GC.Collect();
            }
        }

        private void empresasGridDataBoundGrid_TableControlCurrentCellKeyDown(object sender, GridTableControlKeyEventArgs e)
        {
            if (e.Inner.KeyCode == Keys.Enter || e.Inner.KeyCode == Keys.Return)
                ocorrenciasJustificadasGridDataBoundGrid.Focus();
            Publicas._escTeclado = false;
            if (e.Inner.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresasGridDataBoundGrid.Focus();
            }
        }

        private void ocorrenciasJustificadasGridDataBoundGrid_TableControlCurrentCellKeyDown(object sender, GridTableControlKeyEventArgs e)
        {
            if (e.Inner.KeyCode == Keys.Enter || e.Inner.KeyCode == Keys.Return)
                ocorrenciasInjustificadasGridDataBoundGrid.Focus();
            Publicas._escTeclado = false;
            if (e.Inner.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresasGridDataBoundGrid.Focus();
            }
        }

        private void ocorrenciasInjustificadasGridDataBoundGrid_TableControlCurrentCellKeyDown(object sender, GridTableControlKeyEventArgs e)
        {
            if (e.Inner.KeyCode == Keys.Enter || e.Inner.KeyCode == Keys.Return)
                inicioDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.Inner.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ocorrenciasJustificadasGridDataBoundGrid.Focus();
            }
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

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                 arquivoTextBox.Focus();
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
        
    }
}
