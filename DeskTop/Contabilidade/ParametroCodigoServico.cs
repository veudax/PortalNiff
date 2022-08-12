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

namespace Suportte.Contabilidade
{
    public partial class ParametroCodigoServico : Form
    {
        public ParametroCodigoServico()
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

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;
        List<Classes.ParametrosCodigoServico> _listaParametro;
        List<Classes.ParametrosCodigoServico> _listaParametroLog;

        Classes.Empresa _empresa;
        Classes.ParametrosCodigoServico _parametro;

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

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                //PatText.Focus();
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

        private void ParametroCodigoServico_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            _empresaDoColaborador = new ColaboradoresBO().Listar(Publicas._idColaborador);

            if (Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.IdEmpresa == 19)
                empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            else
            {
                _listaEmpresasAutorizadas = new List<Empresa>();
                foreach (var item in _empresaDoColaborador.Where(w => w.Inicio != DateTime.MinValue.Date &&
                                                                      (w.Fim == DateTime.MinValue.Date || w.Fim <= DateTime.Now.Date)))
                {
                    _listaEmpresasAutorizadas.AddRange(_listaEmpresas.Where(w => w.IdEmpresa == item.IdEmpresa));
                }

                empresaComboBoxAdv.DataSource = _listaEmpresasAutorizadas.OrderBy(o => o.CodigoeNome).ToList();
            }

            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            #region grid 1
            GridMetroColors metroColor = new GridMetroColors();
            GridDynamicFilter filter = new GridDynamicFilter();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(gridGroupingControl1);

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

            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl1.TableControl.CellToolTip.Active = true;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl1.RecordNavigationBar.Label = "";

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl1.TableDescriptor.Columns[i].AllowSort = true;
                gridGroupingControl1.TableDescriptor.Columns[i].Appearance.FilterBarCell.BackColor = Publicas._fundo;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.DisplayText;
            }

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            #endregion

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

            _empresa = null;

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }

            if (_empresa == null)
            {
                new Notificacoes.Mensagem("Selecione a empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            _listaParametro = new NotaFiscalServicoBO().Listar(_empresa.IdEmpresa, false);
            _listaParametroLog = new NotaFiscalServicoBO().Listar(_empresa.IdEmpresa, false);

            gridGroupingControl1.DataSource = _listaParametro;

            gravarButton.Enabled = _listaParametro.Count() > 0;
            excluirButton.Enabled = _listaParametro.Count() > 0;
            ImportarButton.Enabled = true;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                XmlTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void XmlTextBox_KeyDown(object sender, KeyEventArgs e)
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
                gridGroupingControl1.Focus();
            }
        }

        private void XmlTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void XmlTextBox_Validating(object sender, CancelEventArgs e)
        {
            XmlTextBox.BorderColor = Publicas._bordaSaida;

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

            if (XmlTextBox.Text.Trim() == "")
            {
                XmlTextBox.Focus();
                return;
            }

            gridGroupingControl1.DataSource = new List<Classes.ParametrosCodigoServico>();
            gridGroupingControl1.DataSource = _listaParametro.Where(w => w.CodigoServicoXML == XmlTextBox.Text).ToList();

            excluirButton.Enabled = false;
        }

        private void GlobusTextBox_Validating(object sender, CancelEventArgs e)
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
            
            if (GlobusTextBox.Text.Trim() == "")
            {
                GlobusTextBox.Focus();
                return;
            }

        }

        private void IncluirButton_Click(object sender, EventArgs e)
        {

            gridGroupingControl1.DataSource = new List<Classes.ParametrosCodigoServico>();

            if (_listaParametro.Where(w => w.CodigoServicoXML == XmlTextBox.Text && w.CodigoServicoGlobus == GlobusTextBox.Text).Count() == 0)
            {
                _parametro = new ParametrosCodigoServico();
                _parametro.IdEmpresa = _empresa.IdEmpresa;
                _parametro.CodigoServicoXML = XmlTextBox.Text;
                _parametro.CodigoServicoGlobus = GlobusTextBox.Text;
                _listaParametro.Add(_parametro);

            }

            gridGroupingControl1.DataSource = _listaParametro.Where(w => w.CodigoServicoXML == XmlTextBox.Text).ToList();

            GlobusTextBox.Text = string.Empty;
            GlobusTextBox.Focus();

            gravarButton.Enabled = _listaParametro.Count() > 0;
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            GlobusTextBox.Text = string.Empty;
            XmlTextBox.Text = string.Empty;

            _listaParametro.Clear();

            gridGroupingControl1.DataSource = new List<Classes.ParametrosCodigoServico>();
            empresaComboBoxAdv.Focus();
            excluirButton.Enabled = false;
            gravarButton.Enabled = false;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (!new NotaFiscalServicoBO().Gravar(_listaParametro))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            string descricao = "";
            foreach (var item in _listaParametro)
            {
                if (_listaParametroLog.Where(w => w.CodigoServicoXML == item.CodigoServicoXML && w.CodigoServicoGlobus == item.CodigoServicoGlobus).Count() == 0)
                    descricao = descricao + "XML " + item.CodigoServicoXML + ", Globus " + item.CodigoServicoGlobus + "; ";
            }

            try
            {
                descricao = descricao.Substring(0, descricao.Length - 2);
            }
            catch { }


            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou os codigos de serviços " + descricao +
                " da empresa " + empresaComboBoxAdv.Text;
            _log.Tela = "Escrituracao - Nota Fiscal - Parametros";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão de todos códigos de serviços da empresa ?"
                , Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new NotaFiscalServicoBO().ExcluirTudo(_empresa.IdEmpresa))
            {
                new Notificacoes.Mensagem("Problemas durante a Exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            string descricao = "";
            foreach (var item in _listaParametro)
            {
                descricao = descricao + "XML " + item.CodigoServicoXML + ", Globus " + item.CodigoServicoGlobus + "; ";
            }

            try
            {
                descricao = descricao.Substring(0, descricao.Length - 2);
            }
            catch { }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu os codigos de serviços " + descricao +
                " da empresa " + empresaComboBoxAdv.Text;
            _log.Tela = "Escrituracao - Nota Fiscal - Parametros";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void excluirItemDoAtivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;
            string codXML = "";
            string codigo = "";

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (new Notificacoes.Mensagem("Confirma a exclusão ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.ParametrosCodigoServico _excluirTipos = new Classes.ParametrosCodigoServico();

                    gridGroupingControl1.DataSource = new List<Classes.ParametrosCodigoServico>();

                    
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("CodigoServicoXML =") + 18;
                            posFimId = dr.Info.IndexOf(", CodigoServicoGlobus");
                            codXML = dr.Info.Substring(posIniId, posFimId - posIniId).Trim();
                        }
                        catch { }

                        posIniId = 0;
                        posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("CodigoServicoGlobus =") + 21;
                            posFimId = dr.Info.IndexOf(", Existe");
                            codigo = dr.Info.Substring(posIniId, posFimId - posIniId).Trim();
                        }
                        catch { }
                    

                    foreach (var item in _listaParametro.Where(w => w.CodigoServicoXML == codXML && w.CodigoServicoGlobus == codigo))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new NotaFiscalServicoBO().ExcluirItem(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _listaParametro.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu os codigos de serviços XML " + codXML + ", Globus" + codigo +  
                        " da empresa " + empresaComboBoxAdv.Text;
                    _log.Tela = "Escrituracao - Nota Fiscal - Parametros";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                if (XmlTextBox.Text == "")
                    gridGroupingControl1.DataSource = _listaParametro;
                else
                    gridGroupingControl1.DataSource = _listaParametro.Where(w => w.CodigoServicoXML == XmlTextBox.Text).ToList();
            }
        }

        private void ImportarButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                new Notificacoes.Mensagem("Nenhum arquivo selecionado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            ImportarButton.Enabled = false;

            mensagemSistemaLabel.Text = "Importando arquivo, aguarde...";
            Refresh();

            string[] arquivos = openFileDialog1.FileNames;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            Excel.Range range = null;

            string str = "";
            int rCnt;
            int cCnt;
            int rw = 0;
            int cl = 0;
            int qtdLinhas = 0;
            string descricao = "";

            List<Classes.ParametrosCodigoServico> _listaImporta = new List<Classes.ParametrosCodigoServico>();
            Classes.ParametrosCodigoServico _incluirTipos = new Classes.ParametrosCodigoServico();

            foreach (var itemA in arquivos)
            {
                xlApp = new Excel.Application();

                try
                {
                    xlApp.DisplayAlerts = false;
                    xlWorkBook = xlApp.Workbooks.Open(itemA, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 1);

                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    range = xlWorkSheet.UsedRange;
                    rw = range.Rows.Count;
                    cl = range.Columns.Count;
                    // linha um é o cabeçalho
                    str = (Convert.ToString((range.Cells[1, 1] as Excel.Range).Value2));
                }
                catch
                {
                    return;
                }

                
                for (rCnt = 2; rCnt <= rw; rCnt++)
                {
                    _incluirTipos = new ParametrosCodigoServico();

                    qtdLinhas++;

                    if (Convert.ToString((range.Cells[rCnt, 1] as Excel.Range).Value2) == null)
                    {
                        // coomo esta em branco subtrai
                        qtdLinhas--;
                        break;
                    }

                    for (cCnt = 1; cCnt <= cl; cCnt++)
                    {
                        try
                        {
                            str = Convert.ToString((range.Cells[rCnt, cCnt] as Excel.Range).Value2);

                            _incluirTipos.IdEmpresa = _empresa.IdEmpresa;

                            switch (cCnt)
                            {
                                case 1: // xml
                                    _incluirTipos.CodigoServicoXML = str;
                                    break;
                                case 2: // Fornecedor
                                    _incluirTipos.CodigoServicoGlobus = str;
                                    break;
                            }

                        }
                        catch (Exception)
                        {
                            break;
                            //new Notificacoes.Mensagem(ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
                        }
                    }

                    if (_listaParametro.Where(w => w.CodigoServicoXML == _incluirTipos.CodigoServicoXML && w.CodigoServicoGlobus == _incluirTipos.CodigoServicoGlobus).Count() == 0 &&
                        _listaImporta.Where(w => w.CodigoServicoXML == _incluirTipos.CodigoServicoXML && w.CodigoServicoGlobus == _incluirTipos.CodigoServicoGlobus).Count() == 0)
                    {
                        _listaImporta.Add(_incluirTipos);
                        descricao = descricao + "XML " + _incluirTipos.CodigoServicoXML + ", Globus " + _incluirTipos.CodigoServicoGlobus + "; ";
                    }
                    
                }
            }

            try
            {
                descricao = descricao.Substring(0, descricao.Length - 2);
            }
            catch { }


            if (!new NotaFiscalServicoBO().Gravar(_listaImporta))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou os codigos de serviços " + descricao +
                " da empresa " + empresaComboBoxAdv.Text + " por importação";
            _log.Tela = "Escrituracao - Nota Fiscal - Parametros";
            
            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            mensagemSistemaLabel.Text = "";
            Refresh();

            gridGroupingControl1.DataSource = new List<Classes.ParametrosCodigoServico>();

            new Notificacoes.Mensagem("Importação concluída.", Publicas.TipoMensagem.Sucesso).ShowDialog();
            empresaComboBoxAdv.Focus();
        }
    }
}
