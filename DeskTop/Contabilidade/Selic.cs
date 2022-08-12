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
    public partial class Selic : Form
    {
        public Selic()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                this.BackColor = Publicas._fundo;

                PrevistoCurrency.DecimalValue = 0;
                PrevistoCurrency.Tag = null;
                PrevistoCurrency.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : referenciaMaskedEditBox.ForeColor);
                PrevistoCurrency.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : referenciaMaskedEditBox.ForeColor);
                PrevistoCurrency.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                PrevistoCurrency.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : referenciaMaskedEditBox.ForeColor);
                PrevistoCurrency.BackGroundColor = Publicas._fundo;

                UFGCurrencyTextBox.DecimalValue = 0;
                UFGCurrencyTextBox.Tag = null;
                UFGCurrencyTextBox.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : referenciaMaskedEditBox.ForeColor);
                UFGCurrencyTextBox.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : referenciaMaskedEditBox.ForeColor);
                UFGCurrencyTextBox.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                UFGCurrencyTextBox.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : referenciaMaskedEditBox.ForeColor);
                UFGCurrencyTextBox.BackGroundColor = Publicas._fundo;
                
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

        Classes.Endividamento.Selic _selic;
        List<Classes.Endividamento.Selic> _listaSelic;
        List<Classes.Endividamento.Selic> _listaSelicLog;
        
        DateTime _dataInicio;
        bool encontrou;
        bool temAlteracao = false;

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

        private void Selic_Shown(object sender, EventArgs e)
        {
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

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Parametros>();

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
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Selic>();

            _listaSelic = new EndividamentoBO().Listar();
            _listaSelicLog = new EndividamentoBO().Listar();

            gridGroupingControl1.DataSource = _listaSelic;
            gravarButton.Enabled = true;
        }

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PrevistoCurrency.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void PrevistoCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                UFGCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                limparButton.Focus();
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

        private void ImportarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                limparButton.Focus();
            }
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void PrevistoCurrency_Enter(object sender, EventArgs e)
        {
            PrevistoCurrency.BorderColor = Publicas._bordaEntrada;
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

        private void ImportarButton_Enter(object sender, EventArgs e)
        {
            ImportarButton.BackColor = Publicas._botaoFocado;
            ImportarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void ImportarButton_Validating(object sender, CancelEventArgs e)
        {
            ImportarButton.BackColor = Publicas._botao;
            ImportarButton.ForeColor = Publicas._fonteBotao;
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

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;
            referenciaMaskedEditBox.ThemeStyle.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                return;
            }

            try
            {
                if (referenciaMaskedEditBox.ClipText.Trim().Length != 6)
                {
                    new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }
                _dataInicio = new DateTime(Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(2, 4)), Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(0, 2)), 1);
            }
            catch
            {
                new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            foreach (var item in _listaSelic.Where(w => w.MesAno == referenciaMaskedEditBox.Text))
            {
                PrevistoCurrency.DecimalValue = item.Valor;
                UFGCurrencyTextBox.DecimalValue = item.ValorUFG ;
            }

        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            temAlteracao = false;
            referenciaMaskedEditBox.Text = string.Empty;
            PrevistoCurrency.DecimalValue = 0;
            referenciaMaskedEditBox.Focus();

            ImportarButton.Enabled = true;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaSelic.Count == 0)
            {
                new Notificacoes.Mensagem("Nenhuma Selic informada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            if (!new EndividamentoBO().Gravar(_listaSelic))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou os valores da Selic/UFG ";

            string _descricao = "";

            bool encontrou = false;
            foreach (var item in _listaSelic)
            {

                foreach (var itemL in _listaSelicLog.Where(w => w.Id == item.Id))
                {
                    encontrou = true;

                    if (itemL.Valor != item.Valor || itemL.ValorUFG != item.ValorUFG)
                    {
                        _descricao = _descricao +
                            " Alterado do Mes/Ano " + item.MesAno +
                            (itemL.Valor != item.Valor ? " Valor SELIC de " + itemL.Valor.ToString() + " para " + item.Valor.ToString() : "") +
                            (itemL.ValorUFG != item.ValorUFG ? " Valor UFG de " + itemL.ValorUFG.ToString() + " para " + item.ValorUFG.ToString() : "");

                    }
                }

                if (!encontrou)
                    _descricao = _descricao + " Incluido o Mes/Ano " + item.MesAno +
                                    " Valor SELIC " + item.Valor.ToString() +
                                    " Valor UFG " + item.ValorUFG.ToString() ;
            }

            _log.Tela = "Contabilidade - Parcelamento - Selic";
            _log.Descricao = _log.Descricao + _descricao;

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            _listaSelic = new EndividamentoBO().Listar();
            _listaSelicLog = new EndividamentoBO().Listar();

            limparButton_Click(sender, e);
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            int id = 0;
            
            _selic = new Classes.Endividamento.Selic();
            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {

                    try
                    {
                        id = (int)dr["Id"];
                    }
                    catch
                    {
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("Id =") + 4;
                            posFimId = dr.Info.IndexOf(", Referencia");
                            id = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        }
                        catch { }
                    }

                    if (new Notificacoes.Mensagem("Confirma a exclusão da Selic selecionada ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    gridGroupingControl1.DataSource = new List<Classes.Endividamento.Selic>();

                    foreach (var item in _listaSelic.Where(w => w.Id == id))
                    {
                        _selic = item;

                        if (item.Id != 0)
                        {
                            if (!new EndividamentoBO().ExcluirSelic(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                    }
                }
                               

                Log _log = new Log();
                _log.IdUsuario = Publicas._usuario.Id;
                _log.Descricao = "Excluiu a Selic do Mes/Ano " + _selic.MesAno +
                    " no valor de " + _selic.Valor.ToString();

                _log.Tela = "Contabilidade - Parcelamento - Selic";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }
            }

            _listaSelic = new EndividamentoBO().Listar();
            _listaSelicLog = new EndividamentoBO().Listar();

            gridGroupingControl1.DataSource = _listaSelic;
            gravarButton.Enabled = _listaSelic.Count() != 0;
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

        private void PrevistoCurrency_Validating(object sender, CancelEventArgs e)
        {
            PrevistoCurrency.BorderColor = Publicas._bordaSaida;
            
            if (Publicas._escTeclado)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }
            
            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                new Notificacoes.Mensagem("Informe o Mês/Ano.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Text = string.Empty;
                referenciaMaskedEditBox.Focus();
                return;
            }

        }

        private void ImportarButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                new Notificacoes.Mensagem("Nenhum arquivo selecionado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

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
            
            List<Classes.Endividamento.Selic> _listaImporta = new List<Classes.Endividamento.Selic>();

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

                mensagemSistemaLabel.Text = "Importando arquivo, aguarde...";
                Refresh();
                                

                for (rCnt = 2; rCnt <= rw; rCnt++)
                {
                    _selic = new Classes.Endividamento.Selic();

                    encontrou = false;

                    for (cCnt = 1; cCnt <= cl; cCnt++)
                    {

                        try
                        {
                            str = Convert.ToString((range.Cells[rCnt, cCnt] as Excel.Range).Value2);

                            switch (cCnt)
                            {
                                case 1: // Mes/Ano
                                    _selic.MesAno = str;

                                    foreach (var item in _listaSelic.Where(w => w.MesAno == _selic.MesAno))
                                    {
                                        encontrou = true;
                                        _selic.Existe = true;
                                        _selic.Ano = item.Ano;
                                        _selic.Referencia = item.Referencia;
                                        _selic.Id = item.Id;
                                        break;
                                    }                                        

                                    if (!encontrou)
                                    {
                                        _selic.Existe = false;
                                        _selic.Ano = Convert.ToInt32( str.Substring(3,4) );
                                        _selic.Referencia = Convert.ToInt32(str.Substring(3, 4) + str.Substring(0, 2));
                                    }

                                    break;
                                case 2: // Valor
                                    _selic.Valor = Convert.ToDecimal(str.Trim().Replace(".", "").Replace(" ", ""));
                                    break;                                
                            }

                        }
                        catch (Exception)
                        {
                            break;
                            //new Notificacoes.Mensagem(ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
                        }
                    }

                    _listaImporta.Add(_selic);
                }
            }
            
            if (!new EndividamentoBO().Gravar(_listaImporta))
            {
                new Notificacoes.Mensagem("Problemas durante a importação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            mensagemSistemaLabel.Text = "";
            Refresh();

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou os valores da Selic por importação ";

            string _descricao = "";

            foreach (var item in _listaImporta)
            {

                foreach (var itemL in _listaSelicLog.Where(w => w.Id == item.Id))
                {
                    encontrou = true;

                    if (itemL.Valor != item.Valor)
                    {
                        _descricao = _descricao +
                            " Alterado do Mes/Ano " + item.MesAno +
                            (itemL.Valor != item.Valor ? " Valor de " + itemL.Valor.ToString() + " para " + item.Valor.ToString() : "");

                    }
                }

                if (!encontrou)
                    _descricao = _descricao + " Incluido o Mes/Ano " + item.MesAno +
                                    " Valor " + item.Valor.ToString();
            }

            _log.Tela = "Contabilidade - Parcelamento - Selic";
            _log.Descricao = _log.Descricao + _descricao;

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Selic>();

            _listaSelic = new EndividamentoBO().Listar();
            _listaSelicLog = new EndividamentoBO().Listar();

            gridGroupingControl1.DataSource = _listaSelic;

            limparButton_Click(sender, e);
            new Notificacoes.Mensagem("Importação concluída.", Publicas.TipoMensagem.Sucesso).ShowDialog();

        }

        private void UFGCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            UFGCurrencyTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                new Notificacoes.Mensagem("Informe o Mês/Ano.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Text = string.Empty;
                referenciaMaskedEditBox.Focus();
                return;
            }

            bool encontrou = false;

            foreach (var item in _listaSelic.Where(w => w.MesAno == referenciaMaskedEditBox.Text))
            {
                encontrou = true;
                item.Valor = PrevistoCurrency.DecimalValue;
                item.ValorUFG = UFGCurrencyTextBox.DecimalValue;
            }

            if (!encontrou)
            {
                Classes.Endividamento.Selic _selic = new Classes.Endividamento.Selic();
                _selic.MesAno = referenciaMaskedEditBox.Text;
                _selic.Valor = PrevistoCurrency.DecimalValue;
                _selic.ValorUFG = UFGCurrencyTextBox.DecimalValue;
                _selic.Ano = Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(3, 4));
                _selic.Referencia = Convert.ToInt32(referenciaMaskedEditBox.ClipText.Substring(2, 4) + referenciaMaskedEditBox.ClipText.Substring(0, 2));

                _listaSelic.Add(_selic);
            }

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Selic>();
            gridGroupingControl1.DataSource = _listaSelic;

            temAlteracao = true;
            PrevistoCurrency.DecimalValue = 0;
            UFGCurrencyTextBox.DecimalValue = 0;
            referenciaMaskedEditBox.Text = string.Empty;
            referenciaMaskedEditBox.Focus();
        }

        private void UFGCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PrevistoCurrency.Focus();
            }
        }
    }
}
