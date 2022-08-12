using Classes;
using Negocio;
using Suportte.Notificacoes;
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

namespace Suportte.Juridico
{
    public partial class Comunicado : Form
    {
        public Comunicado()
        {
            InitializeComponent();

            dataAberturaDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataParcelaDateTimePickerAdv.BorderColor = Publicas._bordaSaida;
            primeiraParcelaDateTimePicker.BorderColor = Publicas._bordaSaida;

            dataAberturaDateTimePicker.BackColor = nomeResponsavelTextBox.BackColor;
            dataParcelaDateTimePickerAdv.BackColor = nomeResponsavelTextBox.BackColor;
            primeiraParcelaDateTimePicker.BackColor = nomeResponsavelTextBox.BackColor;

            valorReembolsoCurrencyTextBox.BackGroundColor = nomeResponsavelTextBox.BackColor;
            descontoCurrencyTextBox.BackGroundColor = nomeResponsavelTextBox.BackColor;
            totalCurrencyTextBox.BackGroundColor = nomeResponsavelTextBox.BackColor;
            valorParcelaCurrencyTextBox.BackGroundColor = nomeResponsavelTextBox.BackColor;
            parcelaGridGroupingControl.BackColor = nomeResponsavelTextBox.BackColor;
            diasVencimentoCurrencyTextBox.BackColor = nomeResponsavelTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                this.Cursor = Cursors.WaitCursor;
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                {
                    parcelaGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    parcelaGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    parcelaGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    parcelaGridGroupingControl.BackColor = Publicas._panelTitulo;
                    juridicoFavorecidoCheckBoxAdv.ForeColor = Publicas._fonte;
                    primeiraParcelaDateTimePicker.Style = VisualStyle.Office2016Black;
                    dataAberturaDateTimePicker.Style = VisualStyle.Office2016Black;
                    dataParcelaDateTimePickerAdv.Style = VisualStyle.Office2016Black;
                }
            }
            Publicas._mensagemSistema = string.Empty;
            this.Cursor = Cursors.Default;
        }

        List<Empresa> _listaEmpresas;
        List<EmailEnvioComunicado> _listaEmail;
        Empresa _empresa;
        Classes.Comunicado _comunidado;
        Classes.Comunicado _comunidadoLog;
        List<Classes.Comunicado> _listaComunicados;
        List<ParcelasDoComunicado> _listaParcelas = new List<ParcelasDoComunicado>();
        TipoDePagamento _tipo;
        Classes.Vara _vara;
        CentroDeCustoContabil _centroCustoContabil;

        string _emailDestinoJuridico;
        string _emailDestinoDiretoria;
        string[] _dadosEmail;
        bool _copiarDados;
        int _rowIndexComunicado;
        int _idOriginalComunicadoCopiado;

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

        private void Comunicado_Shown(object sender, EventArgs e)
        {
            dataAberturaDateTimePicker.Value = DateTime.Now.Date;
            dataParcelaDateTimePickerAdv.Value = DateTime.Now.Date;

            statusComboBoxAdv.Items.AddRange(new object[] { "Novo", "Aprovado", "Recusado", "Alterado", "Cancelado", "Finalizado"});
            statusComboBoxAdv.SelectedIndex = 0;

            nomeResponsavelTextBox.Text = Publicas._usuario.Nome;

            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoEmpresaGlobus).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            pessoaJuridicaCheckBoxAdv.Checked = false;

            reembolsoCheckBox.Checked = false;
            seguroCheckBoxAdv.Checked = false;
            notaFiscalCheckBoxAdv.Checked = false;

            if (Publicas._chamadoPeloMenuDeComunicado == Publicas.StatusComunicado.Alterado)
                gravarButton.Text = "&Alterar";

            #region grid parcelas
            GridDynamicFilter filter = new GridDynamicFilter();
            GridMetroColors metroColor = new GridMetroColors();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.parcelaGridGroupingControl);

            parcelaGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            parcelaGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            parcelaGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            parcelaGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            parcelaGridGroupingControl.RecordNavigationBar.Label = "Parcelas";
            parcelaGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < parcelaGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                parcelaGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                parcelaGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                parcelaGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                parcelaGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                parcelaGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                parcelaGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

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
                this.parcelaGridGroupingControl.SetMetroStyle(metroColor);
                this.parcelaGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.parcelaGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.parcelaGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.parcelaGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            #endregion

            if (Publicas._idComunicado != 0)
            {
                _comunidado = new ComunicadoBO().Consultar(Publicas._idComunicado);

                PopularTela();
             // popular os campos.   
            }
        }

        #region Changed
        private void totalCurrencyTextBox_DecimalValueChanged(object sender, EventArgs e)
        {
            calcularPictureBox.Visible = totalCurrencyTextBox.DecimalValue > 0;
        }

        private void anteciparCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            postegarCheckBox.Enabled = !anteciparCheckBox.Checked;
            if (anteciparCheckBox.Checked)
                postegarCheckBox.Checked = false;
        }

        private void postegarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            anteciparCheckBox.Enabled = !postegarCheckBox.Checked;
            if (postegarCheckBox.Checked)
                anteciparCheckBox.Checked = false;
        }

        private void checkBoxAdv3_CheckedChanged(object sender, EventArgs e)
        {
            if (pessoaJuridicaCheckBoxAdv.Checked)
            {
                cpfAutorMaskedEditBox.Mask = "99.999.999/9999-99";
                AutorLabel.Text = "Razão Social";
                documentoLabel.Text = "CNPJ";
            }
            else
            {
                cpfAutorMaskedEditBox.Mask = "999.999.999-99";
                AutorLabel.Text = "Nome";
                documentoLabel.Text = "CPF";
            }
        }

        private void juridicoFavorecidoCheckBoxAdv_CheckedChanged(object sender, EventArgs e)
        {
            if (juridicoFavorecidoCheckBoxAdv.Checked)
            {
                cpfFavorecidoMaskedEditBox.Mask = "99.999.999/9999-99";
                documentoFavorecidoLabel.Text = "CNPJ";
            }
            else
            {
                cpfFavorecidoMaskedEditBox.Mask = "999.999.999-99";
                documentoFavorecidoLabel.Text = "CPF";
            }
        }

        private void reembolsoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            valorReembolsoCurrencyTextBox.Enabled = reembolsoCheckBox.Checked;
        }

        private void notaFiscalCheckBoxAdv_CheckedChanged(object sender, EventArgs e)
        {
            descontoCurrencyTextBox.Enabled = notaFiscalCheckBoxAdv.Checked;
        }

        private void descricaoTipoPagamentoTextBoxExt_TextChanged(object sender, EventArgs e)
        {
            motivoOutrosTextBoxExt.Enabled = false;
            outrosLabel.Font = new Font(outrosLabel.Font, outrosLabel.Font.Style & ~FontStyle.Bold);
            try
            {
                motivoOutrosTextBoxExt.Enabled = _tipo.Descricao.ToUpper().Contains("OUTROS");
            }
            catch { }
            if (motivoOutrosTextBoxExt.Enabled)
                outrosLabel.Font = new Font(outrosLabel.Font, FontStyle.Bold);
        }
        #endregion

        #region KeyDown

        private void anteciparCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (postegarCheckBox.Enabled)
                    postegarCheckBox.Focus();
                else
                    quantidadeParcelasCurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                parcelasPanel.Size = new Size(410, 245);
                parcelasPanel.Location = new Point(405, 477);
                calcularPanel.Visible = false;
                panel11.Visible = true;
                totalCurrencyTextBox.Focus();
            }
        }

        private void postegarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                quantidadeParcelasCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (anteciparCheckBox.Enabled)
                    anteciparCheckBox.Focus();
                else
                {
                    parcelasPanel.Size = new Size(410, 245);
                    parcelasPanel.Location = new Point(405, 477);
                    calcularPanel.Visible = false;
                    panel11.Visible = true;
                    totalCurrencyTextBox.Focus();
                }

            }
        }

        private void quantidadeParcelasCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                primeiraParcelaDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (postegarCheckBox.Enabled)
                    postegarCheckBox.Focus();
                else
                    anteciparCheckBox.Focus();
                
            }
        }

        private void primeiraParcelaDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                diasVencimentoCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                quantidadeParcelasCurrencyTextBox.Focus();
            }
        }

        private void diasVencimentoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                breveResumoTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                primeiraParcelaDateTimePicker.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                contaTextBoxExt.Focus();
            }
        }

        private void limparButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (gravarButton.Visible)
                    gravarButton.Focus();
                else
                    statusButton.Focus();
            }
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                numeroProcessoTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void numeroProcessoTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                referenciaMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void numeroProcessoNovoTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                codigoVaraTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void codigoVaraTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                pessoaJuridicaCheckBoxAdv.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                numeroProcessoNovoTextBoxExt.Focus();
            }
        }

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                numeroProcessoNovoTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                numeroProcessoTextBoxExt.Focus();
            }
        }

        private void pessoaJuridicaCheckBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                autorTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoVaraTextBox.Focus();
            }
        }

        private void autorTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cpfAutorMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pessoaJuridicaCheckBoxAdv.Focus();
            }
        }

        private void cpfAutorMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                pisAutorTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                autorTextBoxExt.Focus();
            }
        }

        private void pisAutorTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                centroCustoTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cpfAutorMaskedEditBox.Focus();
            }
        }

        private void centroCustoTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoPagamentoTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pisAutorTextBoxExt.Focus();
            }
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                tipoPagamentoTextBoxExt.Focus();
            }
        }

        private void tipoPagamentoTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (descricaoTipoPagamentoTextBoxExt.Text.ToUpper().Contains("OUTROS"))
                    motivoOutrosTextBoxExt.Focus();
                else
                    reembolsoCheckBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                centroCustoTextBoxExt.Focus();
            }
        }

        private void motivoOutrosTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                reembolsoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoPagamentoTextBoxExt.Focus();
            }
        }

        private void reembolsoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                seguroCheckBoxAdv.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (descricaoTipoPagamentoTextBoxExt.Text.ToUpper().Contains("OUTROS"))
                    motivoOutrosTextBoxExt.Focus();
                else
                    tipoPagamentoTextBoxExt.Focus();
            }
        }

        private void seguroCheckBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (valorReembolsoCurrencyTextBox.Enabled)
                    valorReembolsoCurrencyTextBox.Focus();
                else
                    breveResumoTextBoxExt.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                reembolsoCheckBox.Focus();
            }
        }

        private void valorReembolsoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                breveResumoTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                seguroCheckBoxAdv.Focus();
            }
        }

        private void breveResumoTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                notaFiscalCheckBoxAdv.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (valorReembolsoCurrencyTextBox.Enabled)
                    valorReembolsoCurrencyTextBox.Focus();
                else
                    seguroCheckBoxAdv.Focus();
            }
        }

        private void notaFiscalCheckBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (descontoCurrencyTextBox.Enabled)
                    descontoCurrencyTextBox.Focus();
                else
                    totalCurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                breveResumoTextBoxExt.Focus();
            }
        }

        private void descontoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                totalCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                notaFiscalCheckBoxAdv.Focus();
            }
        }

        private void totalCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                observacaoTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                descontoCurrencyTextBox.Focus();
            }
        }

        private void observacaoTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                favorecidoTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (totalCurrencyTextBox.Enabled)
                    totalCurrencyTextBox.Focus();
                else
                    notaFiscalCheckBoxAdv.Focus();
            }
        }

        private void favorecidoTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                juridicoFavorecidoCheckBoxAdv.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                observacaoTextBoxExt.Focus();
            }
        }
        
        private void juridicoFavorecidoCheckBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cpfFavorecidoMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                favorecidoTextBoxExt.Focus();
            }
        }

        private void cpfFavorecidoMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                bancoTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                juridicoFavorecidoCheckBoxAdv.Focus();
            }
        }

        private void bancoTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                agenciaTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cpfFavorecidoMaskedEditBox.Focus();
            }
        }

        private void agenciaTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                contaTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                bancoTextBoxExt.Focus();
            }
        }

        private void contaTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataParcelaDateTimePickerAdv.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                agenciaTextBoxExt.Focus();
            }
        }

        private void dataParcelaDateTimePickerAdv_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                valorParcelaCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                contaTextBoxExt.Focus();
            }
        }

        private void valorParcelaCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                incluirParcelaButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataParcelaDateTimePickerAdv.Focus();
            }
        }
        #endregion

        #region Enter
        private void gravarButton_Enter(object sender, EventArgs e)
        {
            ((ButtonAdv)sender).BackColor = Publicas._botaoFocado;
            ((ButtonAdv)sender).ForeColor = Publicas._fonteBotaoFocado;
        }

        private void limparButton_Enter(object sender, EventArgs e)
        {
            limparButton.BackColor = Publicas._botaoFocado;
            limparButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void numeroProcessoTextBoxExt_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void dataParcelaDateTimePickerAdv_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void valorParcelaCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }
        
        #endregion

        #region Validation

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            limparButton.BackColor = Publicas._botao;
            limparButton.ForeColor = Publicas._fonteBotao;
        }

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            ((ButtonAdv)sender).BackColor = Publicas._botao;
            ((ButtonAdv)sender).ForeColor = Publicas._fonteBotao;
        }

        private void cpfAutorMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;
            
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (!string.IsNullOrEmpty(cpfAutorMaskedEditBox.ClipText.Trim()))
            {
                if (pessoaJuridicaCheckBoxAdv.Checked)
                {
                    cpfAutorMaskedEditBox.Text = cpfAutorMaskedEditBox.ClipText.Trim().PadLeft(14,'0');
                    if (!Publicas.ValidaCNPJ(cpfAutorMaskedEditBox.Text))
                    {
                        new Notificacoes.Mensagem("CNPJ Inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                        cpfAutorMaskedEditBox.Focus();
                        return;
                    }
                }
                else
                {
                    cpfAutorMaskedEditBox.Text = cpfAutorMaskedEditBox.ClipText.Trim().PadLeft(11, '0');
                    if (!Publicas.ValidaCPF(cpfAutorMaskedEditBox.Text))
                    {
                        new Notificacoes.Mensagem("CPF Inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                        cpfAutorMaskedEditBox.Focus();
                        return;
                    }
                }                                
            }
        }

        private void pisAutorTextBoxExt_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (!string.IsNullOrEmpty(pisAutorTextBoxExt.Text.Trim()))
            {
                if (!Publicas.ValidaPIS(pisAutorTextBoxExt.Text))
                {
                    new Notificacoes.Mensagem("PIS Inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    pisAutorTextBoxExt.Focus();
                    return;
                }
            }
            }

        private void cpfFavorecidoMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (!string.IsNullOrEmpty(cpfFavorecidoMaskedEditBox.ClipText.Trim()))
            {
                if (juridicoFavorecidoCheckBoxAdv.Checked)
                {
                    cpfFavorecidoMaskedEditBox.Text = cpfFavorecidoMaskedEditBox.ClipText.Trim().PadLeft(14, '0');
                    if (!Publicas.ValidaCNPJ(cpfFavorecidoMaskedEditBox.Text))
                    {
                        new Notificacoes.Mensagem("CNPJ Inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                        cpfFavorecidoMaskedEditBox.Focus();
                        return;
                    }
                }
                else
                {
                    cpfFavorecidoMaskedEditBox.Text = cpfFavorecidoMaskedEditBox.ClipText.Trim().PadLeft(11, '0');
                    if (!Publicas.ValidaCPF(cpfFavorecidoMaskedEditBox.Text))
                    {
                        new Notificacoes.Mensagem("CPF Inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                        cpfFavorecidoMaskedEditBox.Focus();
                        return;
                    }
                }
            }
        }

        private void autorTextBoxExt_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(autorTextBoxExt.Text.Trim()))
            {
                new Notificacoes.Mensagem(AutorLabel.Text + " deve ser informada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                autorTextBoxExt.Focus();
                return;
            }
        }

        private void breveResumoTextBoxExt_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(breveResumoTextBoxExt.Text.Trim()))
            {
                new Notificacoes.Mensagem("Breve resumo deve ser informado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                breveResumoTextBoxExt.Focus();
                return;
            }
        }

        private void numeroProcessoTextBoxExt_Validating(object sender, CancelEventArgs e)
        {
            numeroProcessoTextBoxExt.BorderColor = Publicas._bordaSaida;
            _copiarDados = false;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (string.IsNullOrEmpty(numeroProcessoTextBoxExt.Text.Trim()))
            {
                Publicas._idRetornoPesquisa = _empresa.IdEmpresa;
                new Pesquisas.Comunicado().ShowDialog();

                try
                {
                    numeroProcessoTextBoxExt.Text = Publicas._codigoRetornoPesquisa.ToString();
                }
                catch { }

                if (numeroProcessoTextBoxExt.Text.Trim() == "" || numeroProcessoTextBoxExt.Text.Trim() == "0")
                {
                    numeroProcessoTextBoxExt.Text = string.Empty;
                    numeroProcessoTextBoxExt.Focus();
                    return;
                }
            }

            _idOriginalComunicadoCopiado = 0;

            if (Publicas._idComunicado == 0)
            {
                _comunidado = null;

                if (Publicas._idRetornoPesquisa != 0)
                    _comunidado = new ComunicadoBO().Consultar(Publicas._idRetornoPesquisa);
                else
                {
                    _listaComunicados = new ComunicadoBO().Listar(_empresa.IdEmpresa, DateTime.Now.Year, Publicas.StatusComunicado.Todos, numeroProcessoTextBoxExt.Text.Trim());

                    if (_listaComunicados.Count() != 0)
                    {
                        #region Tem mais de um comunicado com o mesmo Processo

                        Publicas._processoComunicado = numeroProcessoTextBoxExt.Text.Trim();
                        Publicas._idRetornoPesquisa = _empresa.IdEmpresa;
                        Publicas._anoSelecionadoComunicado = DateTime.Now.Year;

                        if (Publicas._chamadoPeloMenuDeComunicado == Publicas.StatusComunicado.Novo)
                        {
                            if (new Notificacoes.Mensagem("Comunicado já cadastrado com esse número de processo." +
                            Environment.NewLine +
                            "Deseja copiar os dados ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                            {
                                _copiarDados = true;
                            }
                        }

                        if (Publicas._chamadoPeloMenuDeComunicado != Publicas.StatusComunicado.Novo || _copiarDados)
                        {
                            new Pesquisas.Comunicado().ShowDialog();

                            _comunidado = new Classes.Comunicado();

                            try
                            {
                                if (Publicas._codigoRetornoPesquisa.ToString() != "")
                                {
                                    numeroProcessoTextBoxExt.Text = Publicas._codigoRetornoPesquisa.ToString();
                                    _comunidado = new ComunicadoBO().Consultar(Publicas._idRetornoPesquisa);

                                    if (_copiarDados)
                                    {
                                        _idOriginalComunicadoCopiado = _comunidado.Id;
                                        _comunidado.Status = Publicas.StatusComunicado.Novo;
                                    }
                                }
                            }
                            catch { }
                        }

                        if (numeroProcessoTextBoxExt.Text.Trim() == "" || numeroProcessoTextBoxExt.Text.Trim() == "0")
                        {
                            numeroProcessoTextBoxExt.Text = string.Empty;
                            numeroProcessoTextBoxExt.Focus();
                            return;
                        }

                        #endregion
                    }
                    else
                    if (Publicas._chamadoPeloMenuDeComunicado != Publicas.StatusComunicado.Novo)
                        _comunidado = new ComunicadoBO().Consultar(0, _empresa.IdEmpresa, numeroProcessoTextBoxExt.Text.Trim());
                }
            }

            quantidadeParcelasCurrencyTextBox.DecimalValue = 1;
            if (Publicas._chamadoPeloMenuDeComunicado == Publicas.StatusComunicado.Alterado)
                gravarButton.Text = "&Alterar";

            // processos com status aprovado, reprovado, finalizado e cancelado não podem ser alterados.
            if (_comunidado != null && _comunidado.Existe)
            {
                _comunidadoLog = new ComunicadoBO().Consultar(_comunidado.Id);

                if (Publicas._chamadoPeloMenuDeComunicado != Publicas.StatusComunicado.Novo)
                {

                    PopularTela();

                    if (Publicas._codigoRetornoPesquisa != "")
                        referenciaMaskedEditBox.Focus();
                }
                else
                {
                    gravarButton.Text = "&Gravar";

                    if (_copiarDados)
                    {
                        PopularTela();
                        tipoPagamentoTextBoxExt.Text = string.Empty;
                        descricaoTipoPagamentoTextBoxExt.Text = string.Empty;
                        reembolsoCheckBox.Checked = false;
                        seguroCheckBoxAdv.Checked = false;
                        valorReembolsoCurrencyTextBox.DecimalValue = 0;
                        notaFiscalCheckBoxAdv.Checked = false;
                        descontoCurrencyTextBox.DecimalValue = 0;
                        totalCurrencyTextBox.DecimalValue = 0;
                        valorParcelaCurrencyTextBox.DecimalValue = 0;
                        quantidadeParcelasCurrencyTextBox.DecimalValue = 1;
                        dataAberturaDateTimePicker.Value = DateTime.Now;
                        referenciaMaskedEditBox.Text = DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString("0000");

                        _listaParcelas.Clear();
                        parcelaGridGroupingControl.DataSource = new List<ParcelasDoComunicado>();

                        _comunidado.Existe = false;

                        if (!string.IsNullOrEmpty(codigoVaraTextBox.Text.Trim()))
                            tipoPagamentoTextBoxExt.Focus();
                        else
                        {
                            codigoVaraTextBox.Enabled = true;
                            codigoVaraTextBox.Focus();
                        }

                    }
                    else
                        gravarButton.Text = "&Alterar";                    
                }
                   
            }

        }

        private void PopularTela()
        {
            nomeResponsavelTextBox.Text = _comunidado.Solicitante;
            dataAberturaDateTimePicker.Value = _comunidado.Abertura;

            statusComboBoxAdv.SelectedIndex = (_comunidado.Status == Publicas.StatusComunicado.Novo ? 0 :
                                              (_comunidado.Status == Publicas.StatusComunicado.Aprovado ? 1 :
                                              (_comunidado.Status == Publicas.StatusComunicado.Reprovado ? 2 :
                                              (_comunidado.Status == Publicas.StatusComunicado.Alterado ? 3 :
                                              (_comunidado.Status == Publicas.StatusComunicado.Cancelado ? 4 : 5)))));

            if (_empresa == null)
            {
                _empresa = new EmpresaBO().Consultar(_comunidado.IdEmpresa);

                for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
                {
                    empresaComboBoxAdv.SelectedIndex = i;
                    if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                        break;
                }

                _listaEmail = new EmailEnvioComunicadoBO().Listar(_empresa.IdEmpresa, true);
            }
            referenciaMaskedEditBox.Text = _comunidado.Referencia.ToString("000000");
            numeroProcessoTextBoxExt.Text = _comunidado.Processo;
            numeroProcessoNovoTextBoxExt.Text = _comunidado.NovoProcesso;

            if (_comunidado.IdVara != 0)
                codigoVaraTextBox.Text = _comunidado.IdVara.ToString();

            descricaoVaraTextBoxExt.Text = _comunidado.Vara;
            autorTextBoxExt.Text = _comunidado.Autor;
            pessoaJuridicaCheckBoxAdv.Checked = _comunidado.TipoAutor == Publicas.TipoPessoa.Juridica;
            descricaoCentroCustoTextBoxExt.Text = _comunidado.Custo;

            if (_comunidado.CPFDoAutor != 0)
            {
                if (_comunidado.TipoAutor == Publicas.TipoPessoa.Fisica)
                    cpfAutorMaskedEditBox.Text = _comunidado.CPFDoAutor.ToString().PadLeft(11,'0');
                else
                    cpfAutorMaskedEditBox.Text = _comunidado.CPFDoAutor.ToString().PadLeft(14, '0');
            }

            pisAutorTextBoxExt.Text = _comunidado.PisDoAutor;

            if (_comunidado.CentroDeCustos != 0)
                centroCustoTextBoxExt.Text = _comunidado.CentroDeCustos.ToString();

            if (_comunidado.IdTipo != 0)
                tipoPagamentoTextBoxExt.Text = _comunidado.IdTipo.ToString();

            descricaoTipoPagamentoTextBoxExt.Text = _comunidado.Tipo;
            motivoOutrosTextBoxExt.Text = _comunidado.MotivoTipoOutros;

            reembolsoCheckBox.Checked = _comunidado.Reembolso;
            seguroCheckBoxAdv.Checked = _comunidado.Seguro;
            valorReembolsoCurrencyTextBox.DecimalValue = _comunidado.ValorDoReembolso;

            notaFiscalCheckBoxAdv.Checked = _comunidado.NotaFiscal;
            descontoCurrencyTextBox.DecimalValue = _comunidado.ValorDescontoNotaFiscal;
            totalCurrencyTextBox.DecimalValue = _comunidado.Total;

            breveResumoTextBoxExt.Text = _comunidado.Resumo;
            observacaoTextBoxExt.Text = _comunidado.Observacoes;
            motivoCancelamentoTextBox.Text = _comunidado.MotivoCancelamento;

            favorecidoTextBoxExt.Text = _comunidado.Favorecido;
            juridicoFavorecidoCheckBoxAdv.Checked = _comunidado.TipoFavorecido == Publicas.TipoPessoa.Juridica;

            if (_comunidado.CPFFavorecido != 0)
            {
                if (_comunidado.TipoFavorecido == Publicas.TipoPessoa.Fisica)
                    cpfFavorecidoMaskedEditBox.Text = _comunidado.CPFFavorecido.ToString().PadLeft(11, '0');
                else
                    cpfFavorecidoMaskedEditBox.Text = _comunidado.CPFFavorecido.ToString().PadLeft(14, '0');
            }
            bancoTextBoxExt.Text = _comunidado.Banco;
            agenciaTextBoxExt.Text = _comunidado.Agencia;
            contaTextBoxExt.Text = _comunidado.Conta;

            _listaParcelas = new ComunicadoBO().ListarParcelas(_comunidado.Id);

            parcelaGridGroupingControl.DataSource = _listaParcelas.OrderBy(o => o.Parcela).ToList();

            quantidadeParcelasCurrencyTextBox.DecimalValue = _listaParcelas.Count();

            aprovadoDateTimePicker.Value = _comunidado.Confirmacao;
            usuarioAprovadoTextBox.Text = _comunidado.UsuarioAprovador;
            reprovadoDateTimePicker.Value = _comunidado.Reprovacao;
            usuarioReprovadoTextBox.Text = _comunidado.UsuarioReprovador;
            canceladoDateTimePicker.Value = _comunidado.Cancelamento;
            usuarioCanceladoTextBox.Text = _comunidado.UsuarioCancelador;
            finalizadoDateTimePicker.Value = _comunidado.Finalizado;
            usuarioFinalizadoTextBox.Text = _comunidado.UsuarioFinaliza;
            alteradoDateTimePicker.Value = _comunidado.Alteracao;
            usuarioAlteradoTextBox.Text = _comunidado.UsuarioAlterador;
            informacaoPictureBox.Visible = _comunidado.Existe;
            statusTextBox.Text = (_comunidado.EmailEnviado == "NN" ? "E-mail não enviado ao abrir o comunicado." :
                                 (_comunidado.EmailEnviado == "NE" ? "E-mail enviado ao abrir o comunicado." :
                                 (_comunidado.EmailEnviado == "LN" ? "E-mail não enviado ao alterar o comunicado." :
                                 (_comunidado.EmailEnviado == "LE" ? "E-mail enviado ao alterar o comunicado." :
                                 (_comunidado.EmailEnviado == "AN" ? "E-mail não enviado ao aprovar o comunicado." :
                                 (_comunidado.EmailEnviado == "AE" ? "E-mail enviado ao aprovar o comunicado." :
                                 (_comunidado.EmailEnviado == "RN" ? "E-mail não enviado ao recusar o comunicado." :
                                 (_comunidado.EmailEnviado == "RE" ? "E-mail enviado ao recusar o comunicado." :
                                 (_comunidado.EmailEnviado == "CN" ? "E-mail não enviado ao cancelar o comunicado." : "E-mail enviado ao cancelar o comunicado.")))))))));

            if (_comunidado.Status != Publicas.StatusComunicado.Novo && _comunidado.Status != Publicas.StatusComunicado.Alterado)
            {
                numeroProcessoNovoTextBoxExt.Enabled = false;
                numeroProcessoNovoTextBoxExt.BorderColor = Publicas._bordaSaida;
                referenciaMaskedEditBox.Enabled = false;
                referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;
                pessoaJuridicaCheckBoxAdv.Enabled = true;
                autorTextBoxExt.Enabled = false;
                autorTextBoxExt.BorderColor = Publicas._bordaSaida;
                cpfAutorMaskedEditBox.Enabled = false;
                cpfAutorMaskedEditBox.BorderColor = Publicas._bordaSaida;
                pisAutorTextBoxExt.Enabled = false;
                pisAutorTextBoxExt.BorderColor = Publicas._bordaSaida;
                codigoVaraTextBox.Enabled = false;
                codigoVaraTextBox.BorderColor = Publicas._bordaSaida;
                centroCustoTextBoxExt.Enabled = false;
                centroCustoTextBoxExt.BorderColor = Publicas._bordaSaida;
                tipoPagamentoTextBoxExt.Enabled = false;
                tipoPagamentoTextBoxExt.BorderColor = Publicas._bordaSaida;
                motivoOutrosTextBoxExt.Enabled = false;
                motivoOutrosTextBoxExt.BorderColor = Publicas._bordaSaida;
                reembolsoCheckBox.Enabled = false;
                seguroCheckBoxAdv.Enabled = false;
                valorReembolsoCurrencyTextBox.Enabled = false;
                valorReembolsoCurrencyTextBox.BorderColor = Publicas._bordaSaida;
                notaFiscalCheckBoxAdv.Enabled = false;
                descontoCurrencyTextBox.Enabled = false;
                descontoCurrencyTextBox.BorderColor = Publicas._bordaSaida;
                totalCurrencyTextBox.Enabled = false;
                totalCurrencyTextBox.BorderColor = Publicas._bordaSaida;
                breveResumoTextBoxExt.Enabled = false;
                breveResumoTextBoxExt.BorderColor = Publicas._bordaSaida;
                observacaoTextBoxExt.Enabled = false;
                observacaoTextBoxExt.BorderColor = Publicas._bordaSaida;
                favorecidoTextBoxExt.Enabled = false;
                favorecidoTextBoxExt.BorderColor = Publicas._bordaSaida;
                cpfFavorecidoMaskedEditBox.Enabled = false;
                cpfFavorecidoMaskedEditBox.BorderColor = Publicas._bordaSaida;
                bancoTextBoxExt.Enabled = false;
                bancoTextBoxExt.BorderColor = Publicas._bordaSaida;
                agenciaTextBoxExt.Enabled = false;
                agenciaTextBoxExt.BorderColor = Publicas._bordaSaida;
                contaTextBoxExt.Enabled = false;
                contaTextBoxExt.BorderColor = Publicas._bordaSaida;
                juridicoFavorecidoCheckBoxAdv.Enabled = false;
                dataParcelaDateTimePickerAdv.Enabled = false;
                dataParcelaDateTimePickerAdv.BorderColor = Publicas._bordaSaida;
                valorParcelaCurrencyTextBox.Enabled = false;
                valorParcelaCurrencyTextBox.BorderColor = Publicas._bordaSaida;

                gravarButton.Enabled = false;

                statusButton.Enabled = (_comunidado.Status == Publicas.StatusComunicado.Aprovado && 
                                        (statusButton.Text.Contains("Finalizar") || statusButton.Text.Contains("Cancelar"))) ||
                                       statusButton.Text.Contains("Enviar");
                calcularPictureBox.Visible = false;
            }

            referenciaMaskedEditBox_TextChanged(this, new EventArgs());
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                new Notificacoes.Mensagem("Referência deve ser informada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            int pos = referenciaMaskedEditBox.Text.IndexOf("/");

            if (Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(0, pos)) < 1 ||
                Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(0, pos)) > 12) 
            {
                new Notificacoes.Mensagem("Mês da referência inválida.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            if (statusComboBoxAdv.SelectedIndex == 0)
            {
                if (Convert.ToInt32(referenciaMaskedEditBox.Text.Substring( pos+1,4)) < DateTime.Now.Date.Year)
                {
                    new Notificacoes.Mensagem("Ano da referência deve ser igual ou superior ao ano atual.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }
            }
        }

        private void tipoPagamentoTextBoxExt_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(tipoPagamentoTextBoxExt.Text.Trim()))
            {
                new Pesquisas.TiposDePagamento().ShowDialog();

                tipoPagamentoTextBoxExt.Text = Publicas._idRetornoPesquisa.ToString();

                if (tipoPagamentoTextBoxExt.Text.Trim() == "" || tipoPagamentoTextBoxExt.Text.Trim() == "0")
                {
                    tipoPagamentoTextBoxExt.Text = string.Empty;
                    tipoPagamentoTextBoxExt.Focus();
                    return;
                }
            }

            _tipo = new TipoDePagamentoBO().Consultar(Convert.ToInt32(tipoPagamentoTextBoxExt.Text));

            if (!_tipo.Existe)
            {
                new Notificacoes.Mensagem("Tipo de pagamento não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tipoPagamentoTextBoxExt.Focus();
                return;
            }

            if (!_tipo.Ativo)
            {
                new Notificacoes.Mensagem("Tipo de pagamento inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tipoPagamentoTextBoxExt.Focus();
                return;
            }

            descricaoTipoPagamentoTextBoxExt.Text = _tipo.Descricao;

            if (motivoOutrosTextBoxExt.Enabled)
                motivoOutrosTextBoxExt.Focus();
            else
            {
                reembolsoCheckBox.Focus();
            }
        }

        private void codigoVaraTextBox_Validating(object sender, CancelEventArgs e)
        {
            codigoVaraTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(codigoVaraTextBox.Text.Trim()))
            {
                new Pesquisas.Vara().ShowDialog();

                codigoVaraTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoVaraTextBox.Text.Trim() == "" || codigoVaraTextBox.Text.Trim() == "0")
                {
                    codigoVaraTextBox.Text = string.Empty;
                    codigoVaraTextBox.Focus();
                    return;
                }
            }

            _vara = new VaraBO().Consultar(Convert.ToInt32(codigoVaraTextBox.Text));

            if (!_vara.Existe)
            {
                new Notificacoes.Mensagem("Vara não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoVaraTextBox.Focus();
                return;
            }

            if (!_vara.Ativo)
            {
                new Notificacoes.Mensagem("Vara inativa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoVaraTextBox.Focus();
                return;
            }

            descricaoVaraTextBoxExt.Text = _vara.Descricao;
            if (Publicas._idRetornoPesquisa != 0)
                pessoaJuridicaCheckBoxAdv.Focus();
        }

        private void motivoOutrosTextBoxExt_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(motivoOutrosTextBoxExt.Text.Trim()))
            {
                new Notificacoes.Mensagem("Outros deve informado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                motivoOutrosTextBoxExt.Focus();
                return;
            }
        }

        private void dataParcelaDateTimePickerAdv_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (statusComboBoxAdv.SelectedIndex == 0)
            {
                if (dataParcelaDateTimePickerAdv.Value < DateTime.Now.Date)
                {
                    new Notificacoes.Mensagem("Data deve ser igual ou superior a data atual.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    dataParcelaDateTimePickerAdv.Focus();
                    return;
                }
            }

            // verificar o feriado

            if (dataParcelaDateTimePickerAdv.Value.DayOfWeek == DayOfWeek.Saturday ||
                                dataParcelaDateTimePickerAdv.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                if (new Notificacoes.Mensagem("Data da parcela não é dia útil." + Environment.NewLine +
                    "Confirma ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                {
                    dataParcelaDateTimePickerAdv.Focus();
                    return;
                }
            }

            foreach (var item in _listaParcelas.Where(w => w.Data.Date == dataParcelaDateTimePickerAdv.Value.Date))
            {
                valorParcelaCurrencyTextBox.DecimalValue = item.Valor;
            }
        }

        private void valorParcelaCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (valorParcelaCurrencyTextBox.DecimalValue <= 0)
            {
                new Notificacoes.Mensagem("Valor da parcela dever ser maior que zero.", Publicas.TipoMensagem.Alerta).ShowDialog();
                valorParcelaCurrencyTextBox.Focus();
                return;
            }
        }

        private void valorReembolsoCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;
        }

        private void observacaoTextBoxExt_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }
        
        private void centroCustoTextBoxExt_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = true;
                return;
            }

            if (string.IsNullOrEmpty(centroCustoTextBoxExt.Text.Trim()))
            {
                Publicas._codigoRetornoPesquisa = _empresa.CodigoEmpresaGlobus;
                new Pesquisas.CentroDeCustoContabil().ShowDialog();

                centroCustoTextBoxExt.Text = Publicas._idRetornoPesquisa.ToString();

                if (centroCustoTextBoxExt.Text.Trim() == "" || centroCustoTextBoxExt.Text.Trim() == "0")
                {
                    centroCustoTextBoxExt.Text = string.Empty;
                    centroCustoTextBoxExt.Focus();
                    return;
                }
            }

            _centroCustoContabil = new CentroDeCustoContabilBO().Consultar(Convert.ToInt32(centroCustoTextBoxExt.Text), _empresa.CodigoEmpresaGlobus);

            if (!_centroCustoContabil.Existe)
            {
                new Notificacoes.Mensagem("Centro de custo não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                centroCustoTextBoxExt.Focus();
                return;
            }

            if (!_centroCustoContabil.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Centro de custo não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                centroCustoTextBoxExt.Focus();
                return;
            }

            descricaoCentroCustoTextBoxExt.Text = _centroCustoContabil.Descricao;
            if (Publicas._idRetornoPesquisa != 0)
                tipoPagamentoTextBoxExt.Focus();
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(empresaComboBoxAdv.Text.Trim()))
            {
                new Notificacoes.Mensagem("Empresa deve ser informada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text.Trim()))
            {
                _empresa = item;
            }

            _listaEmail = new EmailEnvioComunicadoBO().Listar(_empresa.IdEmpresa, true);
            if (_listaEmail.Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhum e-mail de destino cadastrado para essa empresa." + 
                    Environment.NewLine + "Solicite via chamado para o TI, passando os e-mail que devem ser cadastrados e para qual empresa. ", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }
        }

        private void primeiraParcelaDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (statusComboBoxAdv.SelectedIndex == 0)
            {
                if (primeiraParcelaDateTimePicker.Value < DateTime.Now.Date)
                {
                    new Notificacoes.Mensagem("Data deve ser igual ou superior a data atual.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    primeiraParcelaDateTimePicker.Focus();
                    return;
                }
            }

            // verificar o feriado

            if (primeiraParcelaDateTimePicker.Value.DayOfWeek == DayOfWeek.Saturday ||
               primeiraParcelaDateTimePicker.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                if (new Notificacoes.Mensagem("Data da parcela não é dia útil." + Environment.NewLine +
                    "Confirma ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                {
                    primeiraParcelaDateTimePicker.Focus();
                    return;
                }
            }
        }

        private void diasVencimentoCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            int i = 0;
            DateTime _data = primeiraParcelaDateTimePicker.Value;
            decimal _valor = Math.Round(totalCurrencyTextBox.DecimalValue / quantidadeParcelasCurrencyTextBox.DecimalValue, 2);
            decimal total = 0;
            ParcelasDoComunicado _parcela = null;
            _listaParcelas.Clear();
            parcelaGridGroupingControl.DataSource = new List<ParcelasDoComunicado>();

            while (i != (int)quantidadeParcelasCurrencyTextBox.DecimalValue)
            {
                _parcela = new ParcelasDoComunicado();
                if (i != 0)
                    _data = _data.AddDays(Convert.ToInt32(diasVencimentoCurrencyTextBox.DecimalValue));

                _parcela.Tipo = Publicas.TipoVencimento.Original;
                total = total + _valor;

                if (_data.DayOfWeek == DayOfWeek.Saturday || _data.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (_data.DayOfWeek == DayOfWeek.Saturday && anteciparCheckBox.Checked)
                    {
                        _data = _data.AddDays(-1);
                        _parcela.Tipo = Publicas.TipoVencimento.Antecipada;
                    }
                    else
                    {
                        if (_data.DayOfWeek == DayOfWeek.Saturday && postegarCheckBox.Checked)
                        {
                            _data = _data.AddDays(2);
                            _parcela.Tipo = Publicas.TipoVencimento.Postergada;
                        }
                        else
                        {
                            if (_data.DayOfWeek == DayOfWeek.Sunday && anteciparCheckBox.Checked)
                            {
                                _data = _data.AddDays(-2);
                                _parcela.Tipo = Publicas.TipoVencimento.Antecipada;
                            }
                            else
                            {
                                if (_data.DayOfWeek == DayOfWeek.Sunday && postegarCheckBox.Checked)
                                {
                                    _data = _data.AddDays(1);
                                    _parcela.Tipo = Publicas.TipoVencimento.Postergada;
                                }
                            }
                        }
                    }
                }

                i++;
                _parcela.Parcela = i;
                _parcela.Data = _data;
                _parcela.Valor = _valor;

                _listaParcelas.Add(_parcela);

            }

            if (total != totalCurrencyTextBox.DecimalValue)
            {
                decimal diferenca = total - totalCurrencyTextBox.DecimalValue;

                if (diferenca < 0)
                    _parcela.Valor = _parcela.Valor + Math.Abs(diferenca);
                else
                    _parcela.Valor = _parcela.Valor - Math.Abs(diferenca);
            }

            parcelaGridGroupingControl.DataSource = _listaParcelas;

            parcelasPanel.Size = new Size(410, 221);
            parcelasPanel.Location = new Point(405, 405);
            calcularPanel.Visible = false;
            panel11.Visible = true;
            observacaoTextBoxExt.Focus();
            referenciaMaskedEditBox_TextChanged(sender, e);
        }

        #endregion
            

        private void incluirParcelaButton_Click(object sender, EventArgs e)
        {
            ParcelasDoComunicado _parcela = new ParcelasDoComunicado();

            bool existe = false;
            foreach (var item in _listaParcelas.Where(w => w.Data.Date == dataParcelaDateTimePickerAdv.Value.Date))
            {
                item.Valor = valorParcelaCurrencyTextBox.DecimalValue;
                existe = true;
            }

            if (!existe)
            {
                _parcela.Data = dataParcelaDateTimePickerAdv.Value.Date;
                _parcela.Valor = valorParcelaCurrencyTextBox.DecimalValue;
                _parcela.Tipo = Publicas.TipoVencimento.Original;
                _parcela.Parcela = _listaParcelas.Count() + 1;
                _listaParcelas.Add(_parcela);
            }

            parcelaGridGroupingControl.DataSource = new List<ParcelasDoComunicado>();
            parcelaGridGroupingControl.DataSource = _listaParcelas.OrderBy(o => o.Data).ToList();

            valorParcelaCurrencyTextBox.DecimalValue = 0;
            dataParcelaDateTimePickerAdv.Focus();

            gravarButton.Enabled = gravarButton.Visible &&
                                   (!string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim())) &&
                                   (!string.IsNullOrEmpty(numeroProcessoTextBoxExt.Text.Trim())) &&
                                   (!string.IsNullOrEmpty(autorTextBoxExt.Text.Trim())) &&
                                   (!string.IsNullOrEmpty(codigoVaraTextBox.Text.Trim())) &&
                                   (!string.IsNullOrEmpty(tipoPagamentoTextBoxExt.Text.Trim())) &&
                                   (!string.IsNullOrEmpty(breveResumoTextBoxExt.Text.Trim())) &&
                                   (totalCurrencyTextBox.DecimalValue > 0) &&
                                   (_listaParcelas.Count() > 0) && !tituloLabel.Text.Contains("Consultar");
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            Publicas._idComunicado = 0;


            parcelaGridGroupingControl.DataSource = new List<ParcelasDoComunicado>();
            _listaParcelas.Clear();

            informacaoPanel.Visible = false;
            parcelasPanel.Size = new Size(410, 221); 
            parcelasPanel.Location = new Point(405, 405);
            calcularPanel.Visible = false;
            panel11.Visible = true;

            numeroProcessoNovoTextBoxExt.Text = string.Empty;
            numeroProcessoTextBoxExt.Text = string.Empty;
            referenciaMaskedEditBox.Text = string.Empty;
            codigoVaraTextBox.Text = string.Empty;
            descricaoVaraTextBoxExt.Text = string.Empty;
            autorTextBoxExt.Text = string.Empty;
            cpfAutorMaskedEditBox.Text = string.Empty;
            pisAutorTextBoxExt.Text = string.Empty;
            centroCustoTextBoxExt.Text = string.Empty;
            descricaoCentroCustoTextBoxExt.Text = string.Empty;
            tipoPagamentoTextBoxExt.Text = string.Empty;
            descricaoTipoPagamentoTextBoxExt.Text = string.Empty;
            motivoOutrosTextBoxExt.Text = string.Empty;
            breveResumoTextBoxExt.Text = string.Empty;
            observacaoTextBoxExt.Text = string.Empty;
            favorecidoTextBoxExt.Text = string.Empty;
            bancoTextBoxExt.Text = string.Empty;
            agenciaTextBoxExt.Text = string.Empty;
            contaTextBoxExt.Text = string.Empty;
            cpfFavorecidoMaskedEditBox.Text = string.Empty;

            valorReembolsoCurrencyTextBox.DecimalValue = 0;
            descontoCurrencyTextBox.DecimalValue = 0;
            totalCurrencyTextBox.DecimalValue = 0;

            dataParcelaDateTimePickerAdv.Value = DateTime.Now.Date;
            statusComboBoxAdv.SelectedIndex = 0;
            pessoaJuridicaCheckBoxAdv.Checked = false;
            juridicoFavorecidoCheckBoxAdv.Checked = false;
            reembolsoCheckBox.Checked = false;
            seguroCheckBoxAdv.Checked = false;
            notaFiscalCheckBoxAdv.Checked = false;

            gravarButton.Enabled = false;
            statusButton.Enabled = false;

            numeroProcessoNovoTextBoxExt.Enabled = true;
            numeroProcessoNovoTextBoxExt.BorderColor = Publicas._bordaSaida;
            referenciaMaskedEditBox.Enabled = true;
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;
            pessoaJuridicaCheckBoxAdv.Enabled = true;
            autorTextBoxExt.Enabled = true;
            autorTextBoxExt.BorderColor = Publicas._bordaSaida;
            cpfAutorMaskedEditBox.Enabled = true;
            cpfAutorMaskedEditBox.BorderColor = Publicas._bordaSaida;
            pisAutorTextBoxExt.Enabled = true;
            pisAutorTextBoxExt.BorderColor = Publicas._bordaSaida;
            codigoVaraTextBox.Enabled = true;
            codigoVaraTextBox.BorderColor = Publicas._bordaSaida;
            centroCustoTextBoxExt.Enabled = true;
            centroCustoTextBoxExt.BorderColor = Publicas._bordaSaida;
            tipoPagamentoTextBoxExt.Enabled = true;
            tipoPagamentoTextBoxExt.BorderColor = Publicas._bordaSaida;
            motivoOutrosTextBoxExt.Enabled = true;
            motivoOutrosTextBoxExt.BorderColor = Publicas._bordaSaida;
            reembolsoCheckBox.Enabled = true;
            seguroCheckBoxAdv.Enabled = true;
            valorReembolsoCurrencyTextBox.Enabled = false;
            valorReembolsoCurrencyTextBox.BorderColor = Publicas._bordaSaida;
            notaFiscalCheckBoxAdv.Enabled = true;
            descontoCurrencyTextBox.Enabled = false;
            descontoCurrencyTextBox.BorderColor = Publicas._bordaSaida;
            totalCurrencyTextBox.Enabled = true;
            totalCurrencyTextBox.BorderColor = Publicas._bordaSaida;
            breveResumoTextBoxExt.Enabled = true;
            breveResumoTextBoxExt.BorderColor = Publicas._bordaSaida;
            observacaoTextBoxExt.Enabled = true;
            observacaoTextBoxExt.BorderColor = Publicas._bordaSaida;
            favorecidoTextBoxExt.Enabled = true;
            favorecidoTextBoxExt.BorderColor = Publicas._bordaSaida;
            cpfFavorecidoMaskedEditBox.Enabled = true;
            cpfFavorecidoMaskedEditBox.BorderColor = Publicas._bordaSaida;
            bancoTextBoxExt.Enabled = true;
            bancoTextBoxExt.BorderColor = Publicas._bordaSaida;
            agenciaTextBoxExt.Enabled = true;
            agenciaTextBoxExt.BorderColor = Publicas._bordaSaida;
            contaTextBoxExt.Enabled = true;
            contaTextBoxExt.BorderColor = Publicas._bordaSaida;
            juridicoFavorecidoCheckBoxAdv.Enabled = true;
            dataParcelaDateTimePickerAdv.Enabled = true;
            dataParcelaDateTimePickerAdv.BorderColor = Publicas._bordaSaida;
            valorParcelaCurrencyTextBox.Enabled = true;
            valorParcelaCurrencyTextBox.BorderColor = Publicas._bordaSaida;
            quantidadeParcelasCurrencyTextBox.DecimalValue = 1;

            aprovadoDateTimePicker.Value = DateTime.MinValue;
            usuarioAprovadoTextBox.Text = string.Empty;
            reprovadoDateTimePicker.Value = DateTime.MinValue;
            usuarioReprovadoTextBox.Text = string.Empty;
            canceladoDateTimePicker.Value = DateTime.MinValue;
            usuarioCanceladoTextBox.Text = string.Empty;
            finalizadoDateTimePicker.Value = DateTime.MinValue;
            usuarioFinalizadoTextBox.Text = string.Empty;
            alteradoDateTimePicker.Value = DateTime.MinValue;
            usuarioAlteradoTextBox.Text = string.Empty;
            informacaoPictureBox.Visible = false;
            calcularPictureBox.Visible = false;

            _copiarDados = false;
            empresaComboBoxAdv.Focus();
        }

        private void informacaoPictureBox_MouseEnter(object sender, EventArgs e)
        {
            informacaoPanel.BringToFront();
            informacaoPanel.Location = new Point(405, 92);
            informacaoPanel.Visible = true;
            informacoesTimer.Start();
        }

        private void informacaoPictureBox_MouseLeave(object sender, EventArgs e)
        {
            informacaoPictureBox.Cursor = Cursors.Default;
        }

        private void calcularPictureBox_Click(object sender, EventArgs e)
        {
            parcelasPanel.Size = new Size(410, 311);
            parcelasPanel.Location = new Point(405, 315);
            calcularPanel.Visible = true;
            primeiraParcelaDateTimePicker.Value = DateTime.Now.Date;
            panel11.Visible = false;
            if (anteciparCheckBox.Enabled)
                anteciparCheckBox.Focus();
            else
                postegarCheckBox.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            string[] _dadosEmail = new string[50];
            string _logAlteracao = "";

            if (_comunidado == null)
                _comunidado = new Classes.Comunicado();

            _emailDestinoDiretoria = "";
            _emailDestinoJuridico = "";

            if (!string.IsNullOrEmpty(codigoVaraTextBox.Text.Trim()))
                codigoVaraTextBox_Validating(sender, new CancelEventArgs());

            foreach (var item in _listaEmail.Where(w => w.TipoEmail == Publicas.TipoEmailComunicado.Diretoria))
            {
                _emailDestinoDiretoria = _emailDestinoDiretoria + item.Email + "; ";
            }

            foreach (var item in _listaEmail.Where(w => w.TipoEmail != Publicas.TipoEmailComunicado.Diretoria))
            {
                _emailDestinoJuridico = _emailDestinoJuridico + item.Email + "; ";
            }

            _emailDestinoJuridico = _emailDestinoJuridico + _emailDestinoDiretoria;
  
            try
            {
                if (_copiarDados)
                {
                    _comunidado.Status = Publicas.StatusComunicado.Novo;
                    _comunidado.Existe = false;
                    _comunidado.Id = 0;
                }
                else
                {
                    if (Publicas._chamadoPeloMenuDeComunicado == Publicas.StatusComunicado.Alterado)
                        _comunidado.Status = Publicas.StatusComunicado.Alterado;
                    else
                        _comunidado.Status = (statusComboBoxAdv.SelectedIndex == 0 ? Publicas.StatusComunicado.Novo :
                                             (statusComboBoxAdv.SelectedIndex == 1 ? Publicas.StatusComunicado.Aprovado :
                                             (statusComboBoxAdv.SelectedIndex == 2 ? Publicas.StatusComunicado.Reprovado :
                                             (statusComboBoxAdv.SelectedIndex == 3 ? Publicas.StatusComunicado.Alterado :
                                             (statusComboBoxAdv.SelectedIndex == 4 ? Publicas.StatusComunicado.Cancelado : Publicas.StatusComunicado.Finalizado)))));
                }
                _dadosEmail[0] = (_comunidado.Existe ? "Alteração do Comunicado" : "Comunicado ao Departamento Financeiro Grupo NIFF");
                _dadosEmail[2] = Publicas._usuario.Nome;

                _comunidado.Abertura = DateTime.Now;
                _dadosEmail[1] = _comunidado.Abertura.ToShortDateString() + " " + _comunidado.Abertura.ToShortTimeString();
                _comunidado.IdEmpresa = _empresa.IdEmpresa;
                _dadosEmail[3] = _empresa.NomeAbreviado;

                if (!_comunidado.Existe)
                {
                    _comunidado.IdUsuario = Publicas._idUsuario;
                    _comunidado.Solicitante = Publicas._usuario.Nome;
                }
                else
                    _comunidado.IdUsuarioAltera = Publicas._idUsuario;

                _comunidado.Processo = numeroProcessoTextBoxExt.Text;
                _dadosEmail[4] = _comunidado.Processo;

                
                _comunidado.Referencia = Convert.ToInt32(referenciaMaskedEditBox.ClipText);
                _dadosEmail[14] = referenciaMaskedEditBox.Text;
                _comunidado.NovoProcesso = numeroProcessoNovoTextBoxExt.Text;
                _dadosEmail[5] = _comunidado.NovoProcesso;

                if (_vara != null)
                    _comunidado.IdVara = _vara.Id;

                _dadosEmail[6] = descricaoVaraTextBoxExt.Text;

                _comunidado.TipoAutor = (pessoaJuridicaCheckBoxAdv.Checked ? Publicas.TipoPessoa.Juridica : Publicas.TipoPessoa.Fisica);
                _comunidado.Autor = autorTextBoxExt.Text.Trim();
                _dadosEmail[7] = _comunidado.Autor;

                if (!String.IsNullOrEmpty(cpfAutorMaskedEditBox.ClipText.Trim()))
                {
                    _comunidado.CPFDoAutor = Convert.ToDecimal(cpfAutorMaskedEditBox.ClipText);
                    _dadosEmail[8] = cpfAutorMaskedEditBox.Text;
                }
                _comunidado.PisDoAutor = pisAutorTextBoxExt.Text;
                _dadosEmail[9] = pisAutorTextBoxExt.Text;

                if (!String.IsNullOrEmpty(centroCustoTextBoxExt.Text.Trim()))
                {
                    _comunidado.CentroDeCustos = Convert.ToInt32(centroCustoTextBoxExt.Text);
                    _dadosEmail[10] = centroCustoTextBoxExt.Text;
                }

                if (_tipo != null)
                    _comunidado.IdTipo = _tipo.Id;

                _dadosEmail[13] = descricaoTipoPagamentoTextBoxExt.Text + (!string.IsNullOrEmpty(motivoOutrosTextBoxExt.Text.Trim()) ? " - " + motivoOutrosTextBoxExt.Text : "");

                _comunidado.MotivoTipoOutros = motivoOutrosTextBoxExt.Text;
                _comunidado.Reembolso = reembolsoCheckBox.Checked;
                _dadosEmail[11] = (reembolsoCheckBox.Checked ? "SIM" : "NÃO") ;

                _dadosEmail[28] = "";
                if (reembolsoCheckBox.Checked)
                    _dadosEmail[28] = "<tr> <td>&nbsp;Valor Reemboso </td> <td>" + valorReembolsoCurrencyTextBox.Text + " </b></td ></tr>";

                _comunidado.Seguro = seguroCheckBoxAdv.Checked;
                _dadosEmail[12] = (seguroCheckBoxAdv.Checked ? "SIM" : "NÃO");

                _comunidado.ValorDoReembolso = valorReembolsoCurrencyTextBox.DecimalValue;

                _dadosEmail[18] = "<tr> <td>&nbsp;Nota Fiscal</td> " +
                        "<td> &nbsp;<b>" + (notaFiscalCheckBoxAdv.Checked ? "SIM " : "NÃO") + " </b></td ></tr>" +
                        (notaFiscalCheckBoxAdv.Checked ? "<tr> <td>&nbsp; Desconto NF </td> <td> &nbsp; " + descontoCurrencyTextBox.Text + " </b></td ></tr>" : "");

                _comunidado.NotaFiscal = notaFiscalCheckBoxAdv.Checked;
                _comunidado.ValorDescontoNotaFiscal = descontoCurrencyTextBox.DecimalValue;
                _comunidado.Total = totalCurrencyTextBox.DecimalValue;
                _dadosEmail[16] = totalCurrencyTextBox.Text;

                _comunidado.Resumo = breveResumoTextBoxExt.Text.Trim();
                _dadosEmail[15] = _comunidado.Resumo;

                _comunidado.Observacoes = observacaoTextBoxExt.Text.Trim();
                _dadosEmail[20] = _comunidado.Observacoes;
                _comunidado.Favorecido = favorecidoTextBoxExt.Text;
                _dadosEmail[21] = _comunidado.Favorecido;
                _comunidado.TipoFavorecido = (juridicoFavorecidoCheckBoxAdv.Checked ? Publicas.TipoPessoa.Juridica : Publicas.TipoPessoa.Fisica);

                if (!String.IsNullOrEmpty(cpfFavorecidoMaskedEditBox.ClipText.Trim()))
                {
                    _comunidado.CPFFavorecido = Convert.ToDecimal(cpfFavorecidoMaskedEditBox.ClipText);
                    _dadosEmail[22] = cpfFavorecidoMaskedEditBox.Text;
                }
                _comunidado.Banco = bancoTextBoxExt.Text;
                _dadosEmail[23] = _comunidado.Banco;
                _comunidado.Agencia = agenciaTextBoxExt.Text;
                _dadosEmail[24] = _comunidado.Agencia;
                _comunidado.Conta = contaTextBoxExt.Text;
                _dadosEmail[25] = _comunidado.Conta;
                _comunidado.QuantidadeDeParcelas = _listaParcelas.Count();
                _dadosEmail[17] = _listaParcelas.Count().ToString();

                _dadosEmail[26] = (pessoaJuridicaCheckBoxAdv.Checked ? "CNPJ" : "CPF");
                _dadosEmail[27] = (juridicoFavorecidoCheckBoxAdv.Checked ? "CNPJ" : "CPF");

                foreach (var item in _listaParcelas)
                {
                    _dadosEmail[19] = _dadosEmail[19] + "<tr> <td align='Right'>" + item.Parcela.ToString() + "&nbsp;</td> " +
                                                        "<td align='Center'> " + item.Data.ToShortDateString() + "</td>" +
                                                        "<td align='Right'> " + string.Format("{0:0.00}", item.Valor) + "&nbsp;</td> </tr>";
                }

                if (!new ComunicadoBO().Gravar(_comunidado, _listaParcelas))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                Publicas._idComunicado = 0;

                if (_comunidadoLog != null)
                {
                    _logAlteracao = "[ " + (_comunidado.Status == _comunidadoLog.Status ? "" : "[Status] de " + _comunidadoLog.Status + " para " + _comunidado.Status + " ") +
                        (_comunidado.IdVara == _comunidadoLog.IdVara ? "" : "[IdVara] de " + _comunidadoLog.IdVara + " para " + _comunidado.IdVara + " ") +
                        (_comunidado.NovoProcesso == _comunidadoLog.NovoProcesso ? "" : "[NovoProcesso] de " + _comunidadoLog.NovoProcesso + " para " + _comunidado.NovoProcesso + " ") +
                        (_comunidado.Autor == _comunidadoLog.Autor ? "" : "[Autor] de " + _comunidadoLog.Autor + " para " + _comunidado.Autor + " ") +
                        (_comunidado.CPFDoAutor == _comunidadoLog.CPFDoAutor ? "" : "[CPFDoAutor] de " + _comunidadoLog.CPFDoAutor + " para " + _comunidado.CPFDoAutor + " ") +
                        (_comunidado.PisDoAutor == _comunidadoLog.PisDoAutor ? "" : "[PisDoAutor] de " + _comunidadoLog.PisDoAutor + " para " + _comunidado.PisDoAutor + " ") +
                        (_comunidado.CentroDeCustos == _comunidadoLog.CentroDeCustos ? "" : "[CentroDeCustos] de " + _comunidadoLog.CentroDeCustos + " para " + _comunidado.CentroDeCustos + " ") +
                        (_comunidado.IdTipo == _comunidadoLog.IdTipo ? "" : "[IdTipo] de " + _comunidadoLog.IdTipo + " para " + _comunidado.IdTipo + " ") +
                        (_comunidado.MotivoTipoOutros == _comunidadoLog.MotivoTipoOutros ? "" : "[MotivoTipoOutros] de " + _comunidadoLog.MotivoTipoOutros + " para " + _comunidado.MotivoTipoOutros + " ") +
                        (_comunidado.Reembolso == _comunidadoLog.Reembolso ? "" : "[Reembolso] de " + _comunidadoLog.Reembolso + " para " + _comunidado.Reembolso + " ") +
                        (_comunidado.Seguro == _comunidadoLog.Seguro ? "" : "[Seguro] de " + _comunidadoLog.Seguro + " para " + _comunidado.Seguro + " ") +
                        (_comunidado.ValorDoReembolso == _comunidadoLog.ValorDoReembolso ? "" : "[ValorDoReembolso] de " + _comunidadoLog.ValorDoReembolso + " para " + _comunidado.ValorDoReembolso + " ") +
                        (_comunidado.NotaFiscal == _comunidadoLog.NotaFiscal ? "" : "[NotaFiscal] de " + _comunidadoLog.NotaFiscal + " para " + _comunidado.NotaFiscal + " ") +
                        (_comunidado.ValorDescontoNotaFiscal == _comunidadoLog.ValorDescontoNotaFiscal ? "" : "[ValorDescontoNotaFiscal] de " + _comunidadoLog.ValorDescontoNotaFiscal + " para " + _comunidado.ValorDescontoNotaFiscal + " ") +
                        (_comunidado.Total == _comunidadoLog.Total ? "" : "[Total] de " + _comunidadoLog.Total + " para " + _comunidado.Total + " ") +
                        (_comunidado.Resumo == _comunidadoLog.Resumo ? "" : "[Resumo] de " + _comunidadoLog.Resumo + " para " + _comunidado.Resumo + " ") +
                        (_comunidado.Observacoes == _comunidadoLog.Observacoes ? "" : "[Observacoes] de " + _comunidadoLog.Observacoes + " para " + _comunidado.Observacoes + " ") +
                        (_comunidado.Favorecido == _comunidadoLog.Favorecido ? "" : "[Favorecido] de " + _comunidadoLog.Favorecido + " para " + _comunidado.Favorecido + " ") +
                        (_comunidado.CPFFavorecido == _comunidadoLog.CPFFavorecido ? "" : "[CPFFavorecido] de " + _comunidadoLog.CPFFavorecido + " para " + _comunidado.CPFFavorecido + " ") +
                        (_comunidado.Banco == _comunidadoLog.Banco ? "" : "[Banco] de " + _comunidadoLog.Banco + " para " + _comunidado.Banco + " ") +
                        (_comunidado.Agencia == _comunidadoLog.Agencia ? "" : "[Agencia] de " + _comunidadoLog.Agencia + " para " + _comunidado.Agencia + " ") +
                        (_comunidado.Conta == _comunidadoLog.Conta ? "" : "[Conta] de " + _comunidadoLog.Conta + " para " + _comunidado.Conta + " ") + " ]";
                }                   


                Log _log = new Log();
                _log.IdUsuario = Publicas._usuario.Id;
                _log.Descricao = (_copiarDados && _idOriginalComunicadoCopiado != 0 ? "Copiou os dados do comunidado de id " + _idOriginalComunicadoCopiado.ToString() :
                    (_comunidado.Existe ? "Alterou " : "Incluiu ") ) +
                     "o Comunicado de número " + _comunidado.Processo +
                     " na data " + _comunidado.Abertura +
                     " vara " + codigoVaraTextBox.Text + " - " + descricaoVaraTextBoxExt.Text +
                     " tipo pagto " + tipoPagamentoTextBoxExt.Text + " - " + descricaoTipoPagamentoTextBoxExt.Text + " " + motivoOutrosTextBoxExt.Text +
                     " no valor de " + _comunidado.Total.ToString() + 
                     " parcelado em " + _dadosEmail[17] +
                     (_comunidado.Existe ? " Id do comunicado " + _comunidado.Id.ToString() : "") + " " +
                     _logAlteracao +
                     " email para [" + _emailDestinoJuridico + "] de [" + Publicas._usuario.Email + "]";

                _log.Tela = "Comunicado";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }
            }
            catch (Exception ex)
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!string.IsNullOrEmpty(_emailDestinoJuridico))
            {
                if (!Publicas.EnviarEmailComunicado(_dadosEmail, false, Publicas._usuario.Email, _emailDestinoJuridico,
                                                "Comunicado em Andamento" + (_comunidado.Existe ? " (ERRATA)" : "") + " - " + _comunidado.Processo + " - " + descricaoVaraTextBoxExt.Text + " - " + _comunidado.Abertura.ToShortDateString(),
                                                "", 587, true, false))
                {
                    new ComunicadoBO().AplicarStatusEmail(Publicas._idComunicado, (_comunidado.Existe ? "LN" : "NN"));
                    new Notificacoes.Mensagem("Problemas durante o envio do e-mail ao departamento financeiro." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }

            if (!string.IsNullOrEmpty(_emailDestinoJuridico))
                new ComunicadoBO().AplicarStatusEmail(Publicas._idComunicado, (_comunidado.Existe ? "LE" : "NE"));

            new Notificacoes.Mensagem("E-mail enviado com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();
            limparButton_Click(sender, e);
        }

        private void totalCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (totalCurrencyTextBox.DecimalValue <= 0)
            {
                new Notificacoes.Mensagem("Total deve ser informado." , Publicas.TipoMensagem. Alerta).ShowDialog();
                totalCurrencyTextBox.Focus();
                return;
            }
        }

        private void parcelaGridGroupingControl_TableControlCellDoubleClick(object sender, GridTableControlCellClickEventArgs e)
        {
            GridRecordRow rec = this.parcelaGridGroupingControl.Table.DisplayElements[e.Inner.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    dataParcelaDateTimePickerAdv.Value = (DateTime)dr["Data"];
                    valorParcelaCurrencyTextBox.DecimalValue = (decimal)dr["Valor"];
                }
            }
        }

        private void referenciaMaskedEditBox_TextChanged(object sender, EventArgs e)
        {
            gravarButton.Enabled = gravarButton.Visible &&
                                   (!string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim())) &&
                                   (!string.IsNullOrEmpty(numeroProcessoTextBoxExt.Text.Trim())) &&
                                   (!string.IsNullOrEmpty(autorTextBoxExt.Text.Trim())) &&
                                   (!string.IsNullOrEmpty(codigoVaraTextBox.Text.Trim())) &&
                                   (!string.IsNullOrEmpty(tipoPagamentoTextBoxExt.Text.Trim())) &&
                                   (!string.IsNullOrEmpty(breveResumoTextBoxExt.Text.Trim())) &&
                                   (totalCurrencyTextBox.DecimalValue > 0) &&
                                   (_listaParcelas.Count() > 0) && !tituloLabel.Text.Contains("Consultar");
        }

        private void popularDadosEmail()
        {
            _dadosEmail = new string[50];

            _dadosEmail[2] = Publicas._usuario.Nome;
            _dadosEmail[1] = _comunidado.Abertura.ToShortDateString() + " " + _comunidado.Abertura.ToShortTimeString();
            _dadosEmail[3] = _empresa.NomeAbreviado;
            _dadosEmail[4] = _comunidado.Processo;
            _dadosEmail[5] = _comunidado.NovoProcesso;
            _dadosEmail[6] = descricaoVaraTextBoxExt.Text;
            _dadosEmail[7] = _comunidado.Autor;
            _dadosEmail[8] = cpfAutorMaskedEditBox.Text;
            _dadosEmail[9] = pisAutorTextBoxExt.Text;
            _dadosEmail[10] = centroCustoTextBoxExt.Text;
            _dadosEmail[11] = (reembolsoCheckBox.Checked ? "SIM" : "NÃO");
            _dadosEmail[12] = (seguroCheckBoxAdv.Checked ? "SIM" : "NÃO");
            _dadosEmail[13] = descricaoTipoPagamentoTextBoxExt.Text + (!string.IsNullOrEmpty(motivoOutrosTextBoxExt.Text.Trim()) ? " - " + motivoOutrosTextBoxExt.Text : ""); 
            _dadosEmail[14] = referenciaMaskedEditBox.Text;
            _dadosEmail[15] = _comunidado.Resumo;
            _dadosEmail[16] = totalCurrencyTextBox.Text;
            _dadosEmail[17] = _comunidado.QuantidadeDeParcelas.ToString();
            _dadosEmail[18] = "<tr> <td>&nbsp;Nota Fiscal</td> " +
                    "<td> &nbsp;<b>" + (notaFiscalCheckBoxAdv.Checked ? "SIM " : "NÃO") + " </b></td ></tr>" +
                    (notaFiscalCheckBoxAdv.Checked ? "<tr> <td>&nbsp; Desconto NF </td> <td> &nbsp; " + descontoCurrencyTextBox.Text + " </b></td ></tr>" : "");

            foreach (var item in _listaParcelas)
            {
                _dadosEmail[19] = _dadosEmail[19] + "<tr> <td align='Right'>" + item.Parcela.ToString() + "&nbsp;</td> " +
                                                    "<td  align='Center'> " + item.Data.ToShortDateString() + "</td>" +
                                                    "<td align='Right'> " + item.Valor.ToString("f") + "&nbsp;</td> </tr>";
            }

            _dadosEmail[20] = _comunidado.Observacoes;
            _dadosEmail[21] = _comunidado.Favorecido;
            _dadosEmail[22] = cpfFavorecidoMaskedEditBox.Text;
            _dadosEmail[23] = _comunidado.Banco;
            _dadosEmail[24] = _comunidado.Agencia;
            _dadosEmail[25] = _comunidado.Conta;
            _dadosEmail[26] = (pessoaJuridicaCheckBoxAdv.Checked ? "CNPJ" : "CPF");
            _dadosEmail[27] = (juridicoFavorecidoCheckBoxAdv.Checked ? "CNPJ" : "CPF");

            _dadosEmail[28] = "";
            if (reembolsoCheckBox.Checked)
                _dadosEmail[28] = "<tr> <td>&nbsp;Valor Reemboso </td> <td>" + valorReembolsoCurrencyTextBox.Text + " </b></td ></tr>";

            _emailDestinoDiretoria = "";
            _emailDestinoJuridico = "";

            foreach (var item in _listaEmail.Where(w => w.TipoEmail == Publicas.TipoEmailComunicado.Diretoria))
            {
                _emailDestinoDiretoria = _emailDestinoDiretoria + item.Email + "; ";
            }

            foreach (var item in _listaEmail.Where(w => w.TipoEmail != Publicas.TipoEmailComunicado.Diretoria))
            {
                _emailDestinoJuridico = _emailDestinoJuridico + item.Email + "; ";
            }

            _emailDestinoJuridico = _emailDestinoJuridico + _emailDestinoDiretoria;
                                    
        }

        private void statusButton_Click(object sender, EventArgs e)
        {
            _comunidado.IdEmpresa = _empresa.IdEmpresa;
            _comunidado.IdUsuario = Publicas._idUsuario;
            _comunidado.Processo = numeroProcessoTextBoxExt.Text;

            if (statusButton.Text.Contains("Aprovar"))
                _comunidado.Status = Publicas.StatusComunicado.Aprovado;

            _comunidado.Referencia = Convert.ToInt32(referenciaMaskedEditBox.ClipText);
            _comunidado.NovoProcesso = numeroProcessoNovoTextBoxExt.Text;

            if (_vara != null)
                _comunidado.IdVara = _vara.Id;

            _comunidado.TipoAutor = (pessoaJuridicaCheckBoxAdv.Checked ? Publicas.TipoPessoa.Juridica : Publicas.TipoPessoa.Fisica);
            _comunidado.Autor = autorTextBoxExt.Text.Trim();

            if (!String.IsNullOrEmpty(cpfAutorMaskedEditBox.ClipText.Trim()))
                _comunidado.CPFDoAutor = Convert.ToDecimal(cpfAutorMaskedEditBox.ClipText);

            _comunidado.PisDoAutor = pisAutorTextBoxExt.Text;

            if (!String.IsNullOrEmpty(centroCustoTextBoxExt.Text.Trim()))
                _comunidado.CentroDeCustos = Convert.ToInt32(centroCustoTextBoxExt.Text);

            if (_tipo != null)
                _comunidado.IdTipo = _tipo.Id;

            _comunidado.MotivoTipoOutros = motivoOutrosTextBoxExt.Text;
            _comunidado.Reembolso = reembolsoCheckBox.Checked;

            _comunidado.Seguro = seguroCheckBoxAdv.Checked;

            _comunidado.ValorDoReembolso = valorReembolsoCurrencyTextBox.DecimalValue;

            _comunidado.NotaFiscal = notaFiscalCheckBoxAdv.Checked;
            _comunidado.ValorDescontoNotaFiscal = descontoCurrencyTextBox.DecimalValue;
            _comunidado.Total = totalCurrencyTextBox.DecimalValue;

            _comunidado.Resumo = breveResumoTextBoxExt.Text.Trim();

            _comunidado.Observacoes = observacaoTextBoxExt.Text.Trim();
            _comunidado.Favorecido = favorecidoTextBoxExt.Text;
            _comunidado.TipoFavorecido = (juridicoFavorecidoCheckBoxAdv.Checked ? Publicas.TipoPessoa.Juridica : Publicas.TipoPessoa.Fisica);

            if (!String.IsNullOrEmpty(cpfFavorecidoMaskedEditBox.ClipText.Trim()))
                _comunidado.CPFFavorecido = Convert.ToDecimal(cpfFavorecidoMaskedEditBox.ClipText);

            _comunidado.Banco = bancoTextBoxExt.Text;
            _comunidado.Agencia = agenciaTextBoxExt.Text;
            _comunidado.Conta = contaTextBoxExt.Text;
            _comunidado.QuantidadeDeParcelas = _listaParcelas.Count();

            popularDadosEmail();
            string _descricao = "";

            if (statusButton.Text.Contains("Enviar"))
            {
                enviarEmailPictureBox_Click(sender, e);
                _descricao = "Enviou e-mail do";
            }

            if (statusButton.Text.Contains("Aprovar"))
            {                

                if (!new ComunicadoBO().Aprovar(_comunidado, _listaParcelas))
                {
                    new Notificacoes.Mensagem("Problemas durante a aprovação do comunicado." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                _descricao = "Aprovou o";
                _dadosEmail[0] = "Comunicado Aprovado ao Departamento Financeiro Grupo NIFF";

                if (!string.IsNullOrEmpty(_emailDestinoJuridico))
                {
                    if (!Publicas.EnviarEmailComunicado(_dadosEmail, false, Publicas._usuario.Email, _emailDestinoJuridico,
                                                    "Comunicado Aprovado - " + _comunidado.Processo + " - " + descricaoVaraTextBoxExt.Text + " - " + _comunidado.Abertura.ToShortDateString(),
                                                    "", 587, true, false))
                    {
                        new ComunicadoBO().AplicarStatusEmail(Publicas._idComunicado, "AN");
                        new Notificacoes.Mensagem("Problemas durante o envio do e-mail ao departamento financeiro e jurídico." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }

                }

                if (!string.IsNullOrEmpty(_emailDestinoJuridico))
                {
                    new Notificacoes.Mensagem("E-mail enviado com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();
                    new ComunicadoBO().AplicarStatusEmail(Publicas._idComunicado, "AE");
                }
            }

            if (statusButton.Text.Contains("Reprovar"))
            {
                _comunidado.Status = Publicas.StatusComunicado.Reprovado;

                if (!new ComunicadoBO().Reprovar(_comunidado.Id))
                {
                    new Notificacoes.Mensagem("Problemas durante a reprovação do comunicado." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                _descricao = "Reprovar o";
                _dadosEmail[0] = "Comunicado Recusado ao Departamento Financeiro Grupo NIFF";

                if (!string.IsNullOrEmpty(_emailDestinoJuridico))
                {
                    if (!Publicas.EnviarEmailComunicado(_dadosEmail, false, Publicas._usuario.Email, _emailDestinoJuridico,
                                                "Comunicado Recusado - " + _comunidado.Processo + " - " + descricaoVaraTextBoxExt.Text + " - " + _comunidado.Abertura.ToShortDateString(),
                                                "", 587, true, false))
                    {
                        new ComunicadoBO().AplicarStatusEmail(Publicas._idComunicado, "RN");
                        new Notificacoes.Mensagem("Problemas durante o envio do e-mail ao departamento financeiro e jurídico." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }

                }

                if (!string.IsNullOrEmpty(_emailDestinoJuridico))
                {
                    new Notificacoes.Mensagem("E-mail enviado com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();
                    new ComunicadoBO().AplicarStatusEmail(Publicas._idComunicado, "RE");
                }
            }

            if (statusButton.Text.Contains("Cancelar"))
            {
                new SAC.Motivo().ShowDialog();

                if (string.IsNullOrEmpty(Publicas._motivoCancelamentoDevolucao.Trim()))
                {
                    new Notificacoes.Mensagem("Cancelamento nao efetuado por falto do motivo." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                _comunidado.Status = Publicas.StatusComunicado.Cancelado;
                _comunidado.MotivoCancelamento = Publicas._motivoCancelamentoDevolucao;

                if (!new ComunicadoBO().Cancelar(_comunidado.Id, _comunidado.MotivoCancelamento))
                {
                    new Notificacoes.Mensagem("Problemas durante o cancelamento do comunicado." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                _descricao = "Cancelar o";
                _dadosEmail[0] = "Comunicado Cancelado ao Departamento Financeiro Grupo NIFF";
                _dadosEmail[29] = "<tr> <td bgcolor = \"#ffffff\">" + 
                    "<table border = \"1\" cellpadding = \"1\" cellspacing = \"0\" width = \"100%\" >" +
                    "<tr> <td style = \"padding: 20px 10px 20px 10px;\" >" +
                    "<p style = \"width: 100%;\" \"font-size:10pt\" align = \"Center\" ><font color = \"00077F\" ><b> Motivo do Cancelamento </font ></b ></p >" 
                           + _comunidado.MotivoCancelamento + "</td> </tr>" + 
                    "</table> </td> </tr>";

                if (!string.IsNullOrEmpty(_emailDestinoJuridico))
                {
                    if (!Publicas.EnviarEmailComunicado(_dadosEmail, false, Publicas._usuario.Email, _emailDestinoJuridico,
                                                "Comunicado Cancelado - " + _comunidado.Processo + " - " + descricaoVaraTextBoxExt.Text + " - " + _comunidado.Abertura.ToShortDateString(),
                                                "", 587, true, false))
                    {
                        new ComunicadoBO().AplicarStatusEmail(Publicas._idComunicado, "CN");
                        new Notificacoes.Mensagem("Problemas durante o envio do e-mail ao departamento financeiro." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }

                    new Notificacoes.Mensagem("E-mail enviado com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();
                    new ComunicadoBO().AplicarStatusEmail(Publicas._idComunicado, "CE");
                }

            }

            if (statusButton.Text.Contains("Finalizar"))
            {
                _comunidado.Status = Publicas.StatusComunicado.Finalizado;

                if (!new ComunicadoBO().Finalizar(_comunidado.Id))
                {
                    new Notificacoes.Mensagem("Problemas durante a finalização do comunicado." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                _descricao = "Finalizar o";
            }

            Publicas._idComunicado = 0;

            string _logAlteracao = "";

            if (_comunidadoLog != null)
            {
                _logAlteracao = (_comunidado.Status == _comunidadoLog.Status ? "" : "[Status] de " + _comunidadoLog.Status + " para " + _comunidado.Status + " ") +
                        (_comunidado.IdVara == _comunidadoLog.IdVara ? "" : "[IdVara] de " + _comunidadoLog.IdVara + " para " + _comunidado.IdVara + " ") +
                        (_comunidado.NovoProcesso == _comunidadoLog.NovoProcesso ? "" : "[NovoProcesso] de " + _comunidadoLog.NovoProcesso + " para " + _comunidado.NovoProcesso + " ") +
                        (_comunidado.Autor == _comunidadoLog.Autor ? "" : "[Autor] de " + _comunidadoLog.Autor + " para " + _comunidado.Autor + " ") +
                        (_comunidado.CPFDoAutor == _comunidadoLog.CPFDoAutor ? "" : "[CPFDoAutor] de " + _comunidadoLog.CPFDoAutor + " para " + _comunidado.CPFDoAutor + " ") +
                        (_comunidado.PisDoAutor == _comunidadoLog.PisDoAutor ? "" : "[PisDoAutor] de " + _comunidadoLog.PisDoAutor + " para " + _comunidado.PisDoAutor + " ") +
                        (_comunidado.CentroDeCustos == _comunidadoLog.CentroDeCustos ? "" : "[CentroDeCustos] de " + _comunidadoLog.CentroDeCustos + " para " + _comunidado.CentroDeCustos + " ") +
                        (_comunidado.IdTipo == _comunidadoLog.IdTipo ? "" : "[IdTipo] de " + _comunidadoLog.IdTipo + " para " + _comunidado.IdTipo + " ") +
                        (_comunidado.MotivoTipoOutros == _comunidadoLog.MotivoTipoOutros ? "" : "[MotivoTipoOutros] de " + _comunidadoLog.MotivoTipoOutros + " para " + _comunidado.MotivoTipoOutros + " ") +
                        (_comunidado.Reembolso == _comunidadoLog.Reembolso ? "" : "[Reembolso] de " + _comunidadoLog.Reembolso + " para " + _comunidado.Reembolso + " ") +
                        (_comunidado.Seguro == _comunidadoLog.Seguro ? "" : "[Seguro] de " + _comunidadoLog.Seguro + " para " + _comunidado.Seguro + " ") +
                        (_comunidado.ValorDoReembolso == _comunidadoLog.ValorDoReembolso ? "" : "[ValorDoReembolso] de " + _comunidadoLog.ValorDoReembolso + " para " + _comunidado.ValorDoReembolso + " ") +
                        (_comunidado.NotaFiscal == _comunidadoLog.NotaFiscal ? "" : "[NotaFiscal] de " + _comunidadoLog.NotaFiscal + " para " + _comunidado.NotaFiscal + " ") +
                        (_comunidado.ValorDescontoNotaFiscal == _comunidadoLog.ValorDescontoNotaFiscal ? "" : "[ValorDescontoNotaFiscal] de " + _comunidadoLog.ValorDescontoNotaFiscal + " para " + _comunidado.ValorDescontoNotaFiscal + " ") +
                        (_comunidado.Total == _comunidadoLog.Total ? "" : "[Total] de " + _comunidadoLog.Total + " para " + _comunidado.Total + " ") +
                        (_comunidado.Resumo == _comunidadoLog.Resumo ? "" : "[Resumo] de " + _comunidadoLog.Resumo + " para " + _comunidado.Resumo + " ") +
                        (_comunidado.Observacoes == _comunidadoLog.Observacoes ? "" : "[Observacoes] de " + _comunidadoLog.Observacoes + " para " + _comunidado.Observacoes + " ") +
                        (_comunidado.Favorecido == _comunidadoLog.Favorecido ? "" : "[Favorecido] de " + _comunidadoLog.Favorecido + " para " + _comunidado.Favorecido + " ") +
                        (_comunidado.CPFFavorecido == _comunidadoLog.CPFFavorecido ? "" : "[CPFFavorecido] de " + _comunidadoLog.CPFFavorecido + " para " + _comunidado.CPFFavorecido + " ") +
                        (_comunidado.Banco == _comunidadoLog.Banco ? "" : "[Banco] de " + _comunidadoLog.Banco + " para " + _comunidado.Banco + " ") +
                        (_comunidado.Agencia == _comunidadoLog.Agencia ? "" : "[Agencia] de " + _comunidadoLog.Agencia + " para " + _comunidado.Agencia + " ") +
                        (_comunidado.Conta == _comunidadoLog.Conta ? "" : "[Conta] de " + _comunidadoLog.Conta + " para " + _comunidado.Conta + " ") +
                        (_comunidado.MotivoCancelamento == _comunidadoLog.MotivoCancelamento ? "" : "[MotivoCancelamento] de " + _comunidadoLog.MotivoCancelamento + " para " + _comunidado.MotivoCancelamento + " ")
                    ;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = _descricao +
                 " Comunicado " + _comunidado.Processo +
                 " na data " + _comunidado.Abertura +
                 " no valor de " + _comunidado.Total.ToString()
                  + " " +
                 _logAlteracao +
                 (statusButton.Text.Contains("Finalizar") ? "" :  " email para [" + _emailDestinoJuridico + "] de [" + Publicas._usuario.Email + "]");

            _log.Tela = "Comunicado";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            Close();
        }

        private void parcelaGridGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            try
            {
                _rowIndexComunicado = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();
            }
            catch { }
        }

        private void parcelaGridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                _rowIndexComunicado = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();
            }
            catch { }
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.parcelaGridGroupingControl.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    dataParcelaDateTimePickerAdv.Value = (DateTime)dr["Data"];
                    dataParcelaDateTimePickerAdv.Enabled = false;
                    valorParcelaCurrencyTextBox.DecimalValue = (decimal)dr["Valor"];
                    valorParcelaCurrencyTextBox.Focus();
                }
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.parcelaGridGroupingControl.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    ParcelasDoComunicado parcela = new ParcelasDoComunicado();

                    foreach (var item in _listaParcelas.Where(w => w.Data == (DateTime)dr["Data"]))
                    {
                        parcela = item;
                    }

                    _listaParcelas.Remove(parcela);
                }
                parcelaGridGroupingControl.DataSource = _listaParcelas;
                parcelaGridGroupingControl.Refresh();
            }
        }

        private void enviarEmailPictureBox_Click(object sender, EventArgs e)
        {
            popularDadosEmail();

            string _status = (_comunidado.Status == Publicas.StatusComunicado.Novo ? "N" :
                         (_comunidado.Status == Publicas.StatusComunicado.Alterado ? "L" :
                         (_comunidado.Status == Publicas.StatusComunicado.Aprovado ? "A" :
                         (_comunidado.Status == Publicas.StatusComunicado.Reprovado ? "R" : "C"))));

            string _texto = (_comunidado.Status == Publicas.StatusComunicado.Novo || _comunidado.Status == Publicas.StatusComunicado.Alterado ? " em Andamento" + (_comunidado.Status == Publicas.StatusComunicado.Alterado ? " (ERRATA)" : "") :
                     (_comunidado.Status == Publicas.StatusComunicado.Aprovado ? "Aprovado" :
                     (_comunidado.Status == Publicas.StatusComunicado.Reprovado ? "Recusado" : "Cancelado")));

            string _assunto = "[REENVIO] " + "Comunicado " + _texto +
                              " - " + _comunidado.Processo +
                              " - " + descricaoVaraTextBoxExt.Text +
                              " - " + _comunidado.Abertura.ToShortDateString();

            _dadosEmail[0] = "Comunicado " + _texto + " ao Departamento Financeiro Grupo NIFF";

            if (_comunidado.MotivoCancelamento != "")
            _dadosEmail[29] = "<tr> <td bgcolor = \"#ffffff\">" +
                    "<table border = \"1\" cellpadding = \"1\" cellspacing = \"0\" width = \"100%\" >" +
                    "<tr> <td style = \"padding: 20px 10px 20px 10px;\" >" +
                    "<p style = \"width: 100%;\" \"font-size:10pt\" align = \"Center\" ><font color = \"00077F\" ><b> Motivo do Cancelamento </font ></b ></p >"
                           + _comunidado.MotivoCancelamento + "</td> </tr>" +
                    "</table> </td> </tr>";

            if (!Publicas.EnviarEmailComunicado(_dadosEmail, false, Publicas._usuario.Email, _emailDestinoJuridico,
                                            _assunto, "", 587, true, false))
            {
                new ComunicadoBO().AplicarStatusEmail(Publicas._idComunicado, _status + "N");
                new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }
            
            new ComunicadoBO().AplicarStatusEmail(Publicas._idComunicado, _status + "E");

            new Notificacoes.Mensagem("E-mail enviado com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();
        }

        private void informacoesTimer_Tick(object sender, EventArgs e)
        {
            if (informacaoPanel.Visible)
            {
                informacaoPanel.Visible = false;
                informacoesTimer.Stop();
            }
        }

        private void enviarEmailPictureBox_MouseHover(object sender, EventArgs e)
        {
            enviarEmailPictureBox.Cursor = Cursors.Hand;
        }

        private void enviarEmailPictureBox_MouseLeave(object sender, EventArgs e)
        {
            enviarEmailPictureBox.Cursor = Cursors.Default;
        }

        private void informacaoPictureBox_MouseHover(object sender, EventArgs e)
        {
            informacaoPictureBox.Cursor = Cursors.Hand;
        }

        private void camposPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
