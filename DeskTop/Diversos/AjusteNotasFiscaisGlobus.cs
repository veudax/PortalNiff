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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.Diversos
{
    public partial class AjusteNotasFiscaisGlobus : Form
    {
        public AjusteNotasFiscaisGlobus()
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

        //List<Empresa> _empresas;
        List<NotasFiscaisEscrituracaoGlobus> _listaNotasESF;

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

        private void AjusteNotasFiscaisGlobus_Load(object sender, EventArgs e)
        {
            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;

            #region itens NF ESF
            itensESFGridGroupingControl.AllowProportionalColumnSizing = false;
            itensESFGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            itensESFGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            itensESFGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            itensESFGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            itensESFGridGroupingControl.RecordNavigationBar.Label = "Itens";

            filter.WireGrid(this.itensESFGridGroupingControl);

            for (int i = 0; i < itensESFGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                itensESFGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                itensESFGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                itensESFGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                itensESFGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                itensESFGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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
            this.itensESFGridGroupingControl.SetMetroStyle(metroColor);
            // para permitir editar dados.
            this.itensESFGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            this.itensESFGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.itensESFGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.itensESFGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            #endregion
        }

        #region KeyDown

        private void TipoDocumentoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                numeroNFTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void numeroNFTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tipoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EmissaoDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                numeroNFTextBox.Focus();
            }
        }

        private void EmissaoDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoComboBox.Focus();
            }
        }

        private void dataDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cfopTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EmissaoDateTimePicker.Focus();
            }
        }

        private void cfopTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                operacaoFiscalTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataDateTimePicker.Focus();
            }
        }

        private void operacaoFiscalTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                situaçãoFiscalTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cfopTextBox.Focus();
            }
        }

        private void situaçãoFiscalTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                aplicarCFOPCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                operacaoFiscalTextBox.Focus();
            }
        }

        private void aplicarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                aplicarOPCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                situaçãoFiscalTextBox.Focus();
            }
        }

        private void baseICMSCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ICMSCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                baseICMSCurrencyTextBox.Focus();
            }
        }

        private void ICMSCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                aliquotaICMSCurrencyTextBo.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ICMSCurrencyTextBox.Focus();
            }
        }

        private void aliquotaICMSCurrencyTextBo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                isentasICMSCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                aliquotaICMSCurrencyTextBo.Focus();
            }
        }

        private void isentasICMSCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                outrasICMSCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                aliquotaICMSCurrencyTextBo.Focus();
            }
        }

        private void outrasICMSCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                baseIPICurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                isentasICMSCurrencyTextBox.Focus();
            }
        }

        private void baseIPICurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                IPICurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                outrasICMSCurrencyTextBox.Focus();
            }
        }

        private void IPICurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                aliquotaIPICurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                baseIPICurrencyTextBox.Focus();
            }
        }

        private void aliquotaIPICurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                isentasIPICurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IPICurrencyTextBox.Focus();
            }
        }

        private void isentasIPICurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                outrasIPICurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                aliquotaIPICurrencyTextBox.Focus();
            }
        }

        private void outrasIPICurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dadosAdicionaisTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                isentasIPICurrencyTextBox.Focus();
            }
        }

        private void dadosAdicionaisTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                alterarNFButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                outrasIPICurrencyTextBox.Focus();
            }
        }

        private void aplicarOPCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                aplicarCSTCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                aplicarCFOPCheckBox.Focus();
            }
        }

        private void aplicarCSTCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                baseICMSCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                aplicarOPCheckBox.Focus();
            }
        }
        #endregion



        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void TipoDocumentoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void EmissaoDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void valorJustificadasCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void TipoDocumentoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void tipoComboBox_Validating(object sender, CancelEventArgs e)
        {
            // pesquisa NF
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
                return;
            }

            if (string.IsNullOrEmpty(TipoDocumentoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe o tipo de documento!", Publicas.TipoMensagem.Alerta).ShowDialog();
                ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
                TipoDocumentoTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(numeroNFTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe o número da nota fiscal!", Publicas.TipoMensagem.Alerta).ShowDialog();
                ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
                numeroNFTextBox.Focus();
                return;
            }

            _listaNotasESF = new NotaFiscalEscrituracaoGlobusBO().Consultar(numeroNFTextBox.Text,
                                                                       TipoDocumentoTextBox.Text,
                                                                       empresaComboBoxAdv.Text.Substring(0, 7),
                                                                       tipoComboBox.Text.Substring(0, 1));

            if (_listaNotasESF.Count() == 0)
            {
                _listaNotasESF = new NotaFiscalEscrituracaoGlobusBO().ConsultarEstoque(numeroNFTextBox.Text,
                                                                      TipoDocumentoTextBox.Text,
                                                                      empresaComboBoxAdv.Text.Substring(0, 7),
                                                                      tipoComboBox.Text.Substring(0, 1));

                if (_listaNotasESF.Count() == 0)
                {
                    new Notificacoes.Mensagem("Nota fiscal não encontrada!", Publicas.TipoMensagem.Alerta).ShowDialog();
                    tipoComboBox.Focus();
                    return;
                }
            }



            //ESFtabPageAdv.Text = (_listaNotasESF.IdCodDoctoESF == 0 ? "Estoque" : "Escrituração");

            //_itensESF = new NotaFiscalEscrituracaoGlobusBO().ListarItens(_listaNotasESF.Id);

            //if (_itensESF.Count() != 0)
            //    itensESFGridGroupingControl.DataSource = _itensESF.OrderBy(o => o.Item).ToList();

            //statusTextBox.Text = _listaNotasESF.StatusNF;
            //ClienteFornecedorTextBox.Text = _listaNotasESF.ClienteFornecedor;
            //cfopTextBox.Text = _listaNotasESF.CFOP.ToString();
            //operacaoFiscalTextBox.Text = _listaNotasESF.OperacaoFiscal == 0 ? string.Empty : _listaNotasESF.OperacaoFiscal.ToString();
            //situaçãoFiscalTextBox.Text = _listaNotasESF.SituacaoTributaria;
            //EmissaoDateTimePicker.Value = _listaNotasESF.Emissao;
            //dataDateTimePicker.Value = _listaNotasESF.Emissao;
            //dadosAdicionaisTextBoxExt.Text = _listaNotasESF.DadosAdicionais;
            //chaveAcessoTextBox.Text = _listaNotasESF.ChaveDeAcesso;
            //protocoloSefazTextBox.Text = _listaNotasESF.ProtocoloSefaz;
            //mensagemSefazTextBox.Text = _listaNotasESF.UltimaMensagemSefaz;
            //reciboTextBox.Text = _listaNotasESF.ReciboSefaz;
            //dataProtocoloTextBox.Text = _listaNotasESF.DataSefaz;
            //statusSefazTextBox.Text = _listaNotasESF.StatusSefaz;
            //documentoTextBox.Text = _listaNotasESF.Documento;
            //itemTextBox.Text = _listaNotasESF.Item;

            //valorContabilCurrencyTextBox.DecimalValue = _listaNotasESF.TotalNF;
            //baseICMSCurrencyTextBox.DecimalValue = _listaNotasESF.BaseICMS;
            //ICMSCurrencyTextBox.DecimalValue = _listaNotasESF.ValorICMS;
            //aliquotaICMSCurrencyTextBo.DecimalValue = _listaNotasESF.AliquotaICMS;
            //isentasICMSCurrencyTextBox.DecimalValue = _listaNotasESF.IsentasICMS;
            //outrasICMSCurrencyTextBox.DecimalValue = _listaNotasESF.OutrasICMS;

            //baseIPICurrencyTextBox.DecimalValue = _listaNotasESF.BaseIPI;
            //IPICurrencyTextBox.DecimalValue = _listaNotasESF.ValorIPI;
            //aliquotaIPICurrencyTextBox.DecimalValue = _listaNotasESF.AliquotaIPI;
            //isentasIPICurrencyTextBox.DecimalValue = _listaNotasESF.IsentaIPI;
            //outrasIPICurrencyTextBox.DecimalValue = _listaNotasESF.OutrasIPI;

            //baseICMSSubstituicaoCurrencyTextBox.DecimalValue = _listaNotasESF.BaseICMSSubstituicao;
            //valorICMSSubstituicaoCurrencyTextBox.DecimalValue = _listaNotasESF.ValorICMSSubstituicao;

            //basePisCurrencyTextBox.DecimalValue = _listaNotasESF.BasePIS;
            //valorPisCurrencyTextBox.DecimalValue = _listaNotasESF.PIS;

            //baseCofinsCurrencyTextBox.DecimalValue = _listaNotasESF.BaseCofins;
            //valorCofinsCurrencyTextBox.DecimalValue = _listaNotasESF.Cofins;

            //freteCurrencyTextBox.DecimalValue = _listaNotasESF.Frete;
            //seguroCurrencyTextBox.DecimalValue = _listaNotasESF.Seguro;
            //descontoCurrencyTextBox.DecimalValue = _listaNotasESF.Desconto;
            //outrasCurrencyTextBox.DecimalValue = _listaNotasESF.Outros;

            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void EmissaoDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
        }

        private void valorJustificadasCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;
        }

        private void numeroNFTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //private void aplicarCFOPCheckBox_MouseEnter(object sender, EventArgs e)
        //{
        //    Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfo1 = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();

        //    toolTipInfo1.Header.Text = "Informação";
        //    toolTipInfo1.Body.Text = "Será aplicado ao clicar no Alterar";

        //    superToolTip.SetToolTip(aplicarCFOPCheckBox, toolTipInfo1);
        //}

        //private void aplicarOPCheckBox_MouseEnter(object sender, EventArgs e)
        //{
        //    Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfo1 = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();

        //    toolTipInfo1.Header.Text = "Informação";
        //    toolTipInfo1.Body.Text = "Será aplicado ao clicar no Alterar";

        //    superToolTip.SetToolTip(aplicarOPCheckBox, toolTipInfo1);
        //}

        //private void aplicarCSTCheckBox_MouseEnter(object sender, EventArgs e)
        //{
        //    Syncfusion.Windows.Forms.Tools.ToolTipInfo toolTipInfo1 = new Syncfusion.Windows.Forms.Tools.ToolTipInfo();

        //    toolTipInfo1.Header.Text = "Informação";
        //    toolTipInfo1.Body.Text = "Será aplicado ao clicar no Alterar";

        //    superToolTip.SetToolTip(aplicarCSTCheckBox, toolTipInfo1);
        //}

        private void alterarNFButton_Click(object sender, EventArgs e)
        {
            //_listaNotasESF.CFOP = Convert.ToInt32(cfopTextBox.Text);

            //if (string.IsNullOrEmpty(operacaoFiscalTextBox.Text.Trim()))
            //  _listaNotasESF.OperacaoFiscal = Convert.ToInt32(operacaoFiscalTextBox.Text);

            //_listaNotasESF.SituacaoTributaria = situaçãoFiscalTextBox.Text;
            //_listaNotasESF.Emissao = EmissaoDateTimePicker.Value;
            //_listaNotasESF.Emissao = dataDateTimePicker.Value;
            //_listaNotasESF.DadosAdicionais = dadosAdicionaisTextBoxExt.Text.Trim();

            //_listaNotasESF.BaseICMS = baseICMSCurrencyTextBox.DecimalValue;
            //_listaNotasESF.ValorICMS = ICMSCurrencyTextBox.DecimalValue;
            //_listaNotasESF.AliquotaICMS = aliquotaICMSCurrencyTextBo.DecimalValue;
            //_listaNotasESF.IsentasICMS = isentasICMSCurrencyTextBox.DecimalValue;
            //_listaNotasESF.OutrasICMS = outrasICMSCurrencyTextBox.DecimalValue;

            //_listaNotasESF.BaseIPI = baseIPICurrencyTextBox.DecimalValue;
            //_listaNotasESF.ValorIPI = IPICurrencyTextBox.DecimalValue;
            //_listaNotasESF.AliquotaIPI = aliquotaIPICurrencyTextBox.DecimalValue;
            //_listaNotasESF.IsentaIPI = isentasIPICurrencyTextBox.DecimalValue;
            //_listaNotasESF.OutrasIPI = outrasIPICurrencyTextBox.DecimalValue;

            //if (new NotaFiscalEscrituracaoGlobusBO().Gravar(tipoComboBox.Text, _listaNotasESF, _itensESF))
            //{
            //    MessageBox.Show("Problemas durante a gravação." +
            //                       Environment.NewLine + Publicas.mensagemDeErro);
            //    return;
            //}

            limparNFButton_Click(sender, e);
        }

        private void limparNFButton_Click(object sender, EventArgs e)
        {
            dataDateTimePicker.Value = DateTime.Now;
            EmissaoDateTimePicker.Value = DateTime.Now;

            TipoDocumentoTextBox.Text = string.Empty;
            numeroNFTextBox.Text = string.Empty;
            cfopTextBox.Text = string.Empty;
            operacaoFiscalTextBox.Text = string.Empty;
            situaçãoFiscalTextBox.Text = string.Empty;
            ClienteFornecedorTextBox.Text = string.Empty;
            dadosAdicionaisTextBoxExt.Text = string.Empty;
            chaveAcessoTextBox.Text = string.Empty;
            protocoloSefazTextBox.Text = string.Empty;
            dataProtocoloTextBox.Text = string.Empty;
            reciboTextBox.Text = string.Empty;
            statusSefazTextBox.Text = string.Empty;
            statusTextBox.Text = string.Empty;
            mensagemSefazTextBox.Text = string.Empty;
            documentoTextBox.Text = string.Empty;
            itemTextBox.Text = string.Empty;

            aplicarCFOPCheckBox.Checked = false;
            aplicarCSTCheckBox.Checked = false;
            aplicarOPCheckBox.Checked = false;

            valorCofinsCurrencyTextBox.DecimalValue = 0;
            valorContabilCurrencyTextBox.DecimalValue = 0;
            valorICMSSubstituicaoCurrencyTextBox.DecimalValue = 0;
            valorPisCurrencyTextBox.DecimalValue = 0;
            ICMSCurrencyTextBox.DecimalValue = 0;
            valorServicoCurrencyTextBox.DecimalValue = 0;
            IPICurrencyTextBox.DecimalValue = 0;

            baseICMSCurrencyTextBox.DecimalValue = 0;
            baseICMSSubstituicaoCurrencyTextBox.DecimalValue = 0;
            baseCofinsCurrencyTextBox.DecimalValue = 0;
            baseIPICurrencyTextBox.DecimalValue = 0;
            basePisCurrencyTextBox.DecimalValue = 0;
            aliquotaICMSCurrencyTextBo.DecimalValue = 0;
            aliquotaIPICurrencyTextBox.DecimalValue = 0;
            isentasICMSCurrencyTextBox.DecimalValue = 0;
            isentasIPICurrencyTextBox.DecimalValue = 0;
            outrasCurrencyTextBox.DecimalValue = 0;
            outrasIPICurrencyTextBox.DecimalValue = 0;
            outrasCurrencyTextBox.DecimalValue = 0;
            freteCurrencyTextBox.DecimalValue = 0;
            seguroCurrencyTextBox.DecimalValue = 0;
            descontoCurrencyTextBox.DecimalValue = 0;

            itensESFGridGroupingControl.DataSource = new List<ItensNotasFiscaisEscrituracaoGlobus>();

            empresaComboBoxAdv.Focus();
        }

        private void cancelarNFButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma o cancelamento desta Nota fiscal?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            //if (new NotaFiscalEscrituracaoGlobusBO().Cancelar(tipoComboBox.Text, _listaNotasESF))
            //{
            //    MessageBox.Show("Problemas durante o cancelamento." +
            //                       Environment.NewLine + Publicas.mensagemDeErro);
            //    return;
            //}

            limparNFButton_Click(sender, e);
        }

        private void excluirButton_Enter(object sender, EventArgs e)
        {
            cancelarNFButton.BackColor = Publicas._botaoFocado;
            cancelarNFButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void excluirButton_Validating(object sender, CancelEventArgs e)
        {
            cancelarNFButton.BackColor = Publicas._botao;
            cancelarNFButton.ForeColor = Publicas._fonteBotao;
        }

        private void excluirButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                limparNFButton.Focus();
            }
        }


        

        private void limparNFButton_Enter(object sender, EventArgs e)
        {
            limparNFButton.BackColor = Publicas._botaoFocado;
            limparNFButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void limparNFButton_Validating(object sender, CancelEventArgs e)
        {
            limparNFButton.BackColor = Publicas._botao;
            limparNFButton.ForeColor = Publicas._fonteBotao;
        }

        private void limparNFButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                alterarNFButton.Focus();
            }
        }

    }
}
