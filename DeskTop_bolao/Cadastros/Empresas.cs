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

namespace Suportte.Cadastros
{
    public partial class Empresas : Form
    {
        public Empresas()
        {
            InitializeComponent();

            responderCurrencyTextBox.BackGroundColor = emailTextBox.BackColor;
            cancelarCurrencyTextBox.BackGroundColor = emailTextBox.BackColor;
            semRetornoCurrencyTextBox.BackGroundColor = emailTextBox.BackColor;
            valorDescontarInjustificadasCurrencyTextBox.BackGroundColor = emailTextBox.BackColor;
            valorJustificadasCurrencyTextBox.BackGroundColor = emailTextBox.BackColor;
            quantidadeAplicarInjustificadasCurrencyTextBox.BackGroundColor = emailTextBox.BackColor;
            quantidadeFaltasAplicarCurrencyTextBox.BackGroundColor = emailTextBox.BackColor;

            emailGridGroupingControl.BackColor = emailTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Empresa _empresa;
        List<EmailEnvioComunicado> _listaEmail;
        int _rowIndex;
        bool _emailInvalido;

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

        private void empresaTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void empresaTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                empresaTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(empresaTextBox.Text))
            {
                new Pesquisas.Empresas().ShowDialog();

                empresaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(empresaTextBox.Text) || empresaTextBox.Text == "0")
                {
                    empresaTextBox.Text = string.Empty;
                    empresaTextBox.Focus();
                    return;
                }
            }

            _empresa = new EmpresaBO().Consultar(Convert.ToInt32(empresaTextBox.Text));

            if (_empresa != null)
            {
                nomeTextBox.Text = _empresa.Nome;
                ativoCheckBox.Checked = _empresa.Ativo;
                codigoGlobusTextBox.Text = _empresa.CodigoEmpresaGlobus;
                textoPadraoTextBox.Text = _empresa.TextoPadraoSAC;
                autenticaSSLCheckBox.Checked = _empresa.AutenticaSLL;
                autenticaCheckBox.Checked = _empresa.Autentica;
                nomeAbreviadoTextBox.Text = _empresa.NomeAbreviado;
                emailTextBox.Text = _empresa.Email;
                smtpTextBox.Text = _empresa.Smtp;
                portaTextBox.Text = _empresa.PortaSmtp.ToString();
                senhaTextBox.Text = _empresa.Senha;
                avaliaColaboradoresCheckBox.Checked = _empresa.AvaliaColaboradores;

                valorDescontarInjustificadasCurrencyTextBox.DecimalValue = _empresa.ValorDescontoSobreFaltaInjustificada;
                valorJustificadasCurrencyTextBox.DecimalValue = _empresa.ValorDescontoSobreFaltaJustificada;
                quantidadeAplicarInjustificadasCurrencyTextBox.DecimalValue = _empresa.QuantidadeFaltasInjustificadasSuperior;
                quantidadeFaltasAplicarCurrencyTextBox.DecimalValue = _empresa.QuantidadeFaltasJustificadasSuperior;
                cancelarCurrencyTextBox.DecimalValue = _empresa.QuantidadeDiasCanceladoNoGrid;
                semRetornoCurrencyTextBox.DecimalValue = _empresa.QuantidadeDiasSemRetornoNoGrid;
                responderCurrencyTextBox.DecimalValue = _empresa.QuantidadeDiasParaResponder;

                telefoneMaskedEditBox.Text = _empresa.Telefone;

                separadorComboBox.SelectedIndex = (_empresa.Separador == "" ? 0 :
                                                   (_empresa.Separador == "-" ? 1 :
                                                    (_empresa.Separador == "/" ? 2 : 3)));

                tipoFormatoComboBox.SelectedIndex = (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.Ano ? 0 :
                                                     (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.AnoMes ? 1 :
                                                      (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAno ? 2 :
                                                       (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes ? 3 : 4))));

                _listaEmail = new EmailEnvioComunicadoBO().Listar(_empresa.IdEmpresa);

                #region grid E-mail juridico
                GridDynamicFilter filter = new GridDynamicFilter();
                GridMetroColors metroColor = new GridMetroColors();

                filter.ApplyFilterOnlyOnCellLostFocus = true;
                filter.WireGrid(this.emailGridGroupingControl);

                emailGridGroupingControl.DataSource = _listaEmail;
                emailGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
                emailGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
                emailGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
                emailGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
                emailGridGroupingControl.RecordNavigationBar.Label = "E-mails";
                emailGridGroupingControl.TableControl.CellToolTip.Active = true;

                for (int i = 0; i < emailGridGroupingControl.TableDescriptor.Columns.Count; i++)
                {
                    emailGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                    emailGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                    emailGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                    emailGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                    emailGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                    emailGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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

                this.emailGridGroupingControl.SetMetroStyle(metroColor);

                this.emailGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

                this.emailGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.emailGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
                this.emailGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
                #endregion
            }

            excluirButton.Enabled = _empresa.Existe;
            gravarButton.Enabled = true;

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._idRetornoPesquisa != 0)
                nomeAbreviadoTextBox.Focus();
        }

        private void empresaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                tabControlAdv1.SelectedTab = EmailtabPageAdv;
                emailTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoGlobusTextBox.Focus();
            }
        }

        private void codigoGlobusTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                tabControlAdv1.SelectedTab = EmailtabPageAdv;
                emailTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                avaliaColaboradoresCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaTextBox.Focus();
            }
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            excluirButton.Enabled = false;
            gravarButton.Enabled = false;

            empresaTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            codigoGlobusTextBox.Text = string.Empty;
            textoPadraoTextBox.Text = string.Empty;
            nomeAbreviadoTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            smtpTextBox.Text = string.Empty;
            portaTextBox.Text = string.Empty;
            telefoneMaskedEditBox.Text = string.Empty;
            senhaTextBox.Text = string.Empty;
            ativoCheckBox.Checked = true;
            autenticaCheckBox.Checked = true;
            autenticaSSLCheckBox.Checked = true;
            avaliaColaboradoresCheckBox.Checked = false;
            tipoFormatoComboBox.SelectedIndex = -1;
            separadorComboBox.SelectedIndex = -1;
            valorDescontarInjustificadasCurrencyTextBox.DecimalValue = 0;
            valorJustificadasCurrencyTextBox.DecimalValue = 0;
            quantidadeFaltasAplicarCurrencyTextBox.DecimalValue = 0;
            quantidadeAplicarInjustificadasCurrencyTextBox.DecimalValue = 0;
            responderCurrencyTextBox.DecimalValue = 1;
            cancelarCurrencyTextBox.DecimalValue = 1;
            semRetornoCurrencyTextBox.DecimalValue = 1;
            emailGridGroupingControl.DataSource = new List<EmailEnvioComunicado>();
            grupoEmailComboBoxAdv.SelectedIndex = -1;
            emailAtivoCheckBoxAdv.Checked = false;
            emailTextBoxExt.Text = string.Empty;
            tabControlAdv1.SelectedTab = EmailtabPageAdv;
            empresaTextBox.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o nome da empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            _empresa.IdEmpresa = Convert.ToInt32(empresaTextBox.Text);
            _empresa.Nome = nomeTextBox.Text;
            _empresa.Ativo = ativoCheckBox.Checked;
            _empresa.CodigoEmpresaGlobus = codigoGlobusTextBox.Text;
            _empresa.TextoPadraoSAC = textoPadraoTextBox.Text;
            _empresa.Separador = separadorComboBox.Text;
            _empresa.NomeAbreviado = nomeAbreviadoTextBox.Text;
            _empresa.Email = emailTextBox.Text;
            _empresa.Smtp = smtpTextBox.Text;
            _empresa.PortaSmtp = Convert.ToInt32(portaTextBox.Text);
            _empresa.Autentica = autenticaCheckBox.Checked;
            _empresa.AutenticaSLL = autenticaSSLCheckBox.Checked;
            _empresa.Senha = senhaTextBox.Text;
            _empresa.Telefone = telefoneMaskedEditBox.ClipText.Trim();
            _empresa.ValorDescontoSobreFaltaInjustificada = valorDescontarInjustificadasCurrencyTextBox.DecimalValue;
            _empresa.ValorDescontoSobreFaltaJustificada = valorJustificadasCurrencyTextBox.DecimalValue;
            _empresa.QuantidadeFaltasInjustificadasSuperior = Convert.ToInt32(quantidadeAplicarInjustificadasCurrencyTextBox.DecimalValue);
            _empresa.QuantidadeFaltasJustificadasSuperior = Convert.ToInt32(quantidadeFaltasAplicarCurrencyTextBox.DecimalValue);
            _empresa.QuantidadeDiasCanceladoNoGrid = Convert.ToInt32(cancelarCurrencyTextBox.DecimalValue);
            _empresa.QuantidadeDiasSemRetornoNoGrid= Convert.ToInt32(semRetornoCurrencyTextBox.DecimalValue);
            _empresa.QuantidadeDiasParaResponder = Convert.ToInt32(responderCurrencyTextBox.DecimalValue);
            _empresa.AvaliaColaboradores = avaliaColaboradoresCheckBox.Checked;

            switch (tipoFormatoComboBox.SelectedIndex)
            {
                case 0:
                    _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.Ano;
                    break;
                case 1:
                    _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.AnoMes;
                    break;
                case 2:
                    _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.EmpresaAno;
                    break;
                case 3:
                    _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes;
                    break;
                case 4:
                    _empresa.FormatoCodigo = Publicas.TipoCalculoCodigoSAC.Sequencial;
                    break;
            }
            

            if (! new EmpresaBO().Gravar(_empresa, _listaEmail))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            

            limparButton_Click(sender, e);
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new EmpresaBO().Excluir(_empresa))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textoPadraoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                responderCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                separadorComboBox.Focus();
            }
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            empresaTextBox.Text = new EmpresaBO().Proximo().ToString();
            nomeAbreviadoTextBox.Focus();
        }

        private void empresaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '+')
            {
                empresaTextBox.Text = string.Empty;
                proximoButton_Click(sender, e);
            }
        }

        private void Empresas_Load(object sender, EventArgs e)
        {
            tipoFormatoComboBox.Items.AddRange(new object[] { "Ano", "Ano e mês", "Empresa e Ano", "Empresa, Ano e mês", "Sequencial"});
            separadorComboBox.Items.AddRange(new object[] {"", "-", "/", "."});
            grupoEmailComboBoxAdv.Items.AddRange(new object[] { "Jurídico", "Financeiro", "Diretoria" });

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
        }

        private void tipoFormatoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                separadorComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                telefoneMaskedEditBox.Focus();
            }
        }

        private void separadorComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                textoPadraoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoFormatoComboBox.Focus();
            }
        }

        private void tipoFormatoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void tipoFormatoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void nomeAbreviadoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                codigoGlobusTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                avaliaColaboradoresCheckBox.Focus();
            }
        }

        private void emailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                senhaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void smtpTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                portaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                senhaTextBox.Focus();
            }
        }

        private void portaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                autenticaCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                smtpTextBox.Focus();
            }
        }

        private void autenticaCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                autenticaSSLCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                portaTextBox.Focus();
            }
        }

        private void autenticaSSLCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                tabControlAdv1.SelectedTab = SACtabPageAdv;
                
                telefoneMaskedEditBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                autenticaCheckBox.Focus();
            }
        }

        private void portaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) )
            {
                e.Handled = true;
            }
            
        }

        private void telefoneMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoFormatoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tabControlAdv1.SelectedTab = EmailtabPageAdv;
                autenticaSSLCheckBox.Focus();
            }
        }

        private void telefoneMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void telefoneMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;
        }

        private void valorJustificadasCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void quantidadeFaltasAplicarCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void valorDescontarInjustificadasCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void quantidadeAplicarInjustificadasCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void valorJustificadasCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;
        }

        private void valorJustificadasCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                quantidadeFaltasAplicarCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tabControlAdv1.SelectedTab = SACtabPageAdv;
                semRetornoCurrencyTextBox.Focus();
            }
        }

        private void quantidadeFaltasAplicarCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                valorDescontarInjustificadasCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                valorJustificadasCurrencyTextBox.Focus();
            }
        }

        private void valorDescontarInjustificadasCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                quantidadeAplicarInjustificadasCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                quantidadeFaltasAplicarCurrencyTextBox.Focus();
            }
        }

        private void quantidadeAplicarInjustificadasCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                tabControlAdv1.SelectedTab = JuridicotabPageAdv;
                emailTextBoxExt.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                valorDescontarInjustificadasCurrencyTextBox.Focus();
            }
        }

        private void senhaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                smtpTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                emailTextBox.Focus();
            }
        }

        private void textoPadraoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }
        
        private void semRetornoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                tabControlAdv1.SelectedTab = BeneficiotabPageAdv;
                valorJustificadasCurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cancelarCurrencyTextBox.Focus();
            }
        }

        private void cancelarCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                semRetornoCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                responderCurrencyTextBox.Focus();
            }
        }

        private void responderCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cancelarCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                textoPadraoTextBox.Focus();
            }
        }

        private void responderCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            responderCurrencyTextBox.BorderColor = Publicas._bordaEntrada;
        }

        private void semRetornoCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            semRetornoCurrencyTextBox.BorderColor = Publicas._bordaEntrada;
        }

        private void cancelarCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            cancelarCurrencyTextBox.BorderColor = Publicas._bordaEntrada;
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

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                semRetornoCurrencyTextBox.Focus();
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

        private void emailTextBoxExt_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            _emailInvalido = false;
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(emailTextBoxExt.Text.Trim()))
            {
                emailGridGroupingControl.Focus();
                return;
            }

            if (!Publicas.ValidarEmail(emailTextBoxExt.Text.Trim()))
            {
                _emailInvalido = true;
                new Notificacoes.Mensagem("E-mail inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                emailTextBoxExt.Focus();
                return;
            }

            foreach (var item in _listaEmail.Where(w => w.Email.ToLower() == emailTextBoxExt.Text.ToLower().Trim()))
            {
                emailAtivoCheckBoxAdv.Checked = item.Ativo;
                grupoEmailComboBoxAdv.SelectedIndex = (item.TipoEmail == Publicas.TipoEmailComunicado.Juridico ? 0 :
                                                      (item.TipoEmail == Publicas.TipoEmailComunicado.Financeiro ? 1 : 2));
            }

        }

        private void emailTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                emailAtivoCheckBoxAdv.Focus();

            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                grupoEmailComboBoxAdv.Focus();
            }
        }

        private void emailAtivoCheckBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();

            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                emailTextBoxExt.Focus();
            }
        }

        private void emailAtivoCheckBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
                return;

            bool encontrou = false;

            if (!string.IsNullOrEmpty(emailTextBoxExt.Text.Trim()))
            {
                foreach (var item in _listaEmail.Where(w => w.Email.ToLower() == emailTextBoxExt.Text.ToLower().Trim()))
                {
                    encontrou = true;
                    item.Ativo = emailAtivoCheckBoxAdv.Checked;
                }

                if (!encontrou)
                {
                    EmailEnvioComunicado _email = new EmailEnvioComunicado();
                    _email.Email = emailTextBoxExt.Text.ToLower();
                    _email.Ativo = emailAtivoCheckBoxAdv.Checked;
                    _email.TipoEmail = (grupoEmailComboBoxAdv.SelectedIndex == 0 ? Publicas.TipoEmailComunicado.Juridico :
                        (grupoEmailComboBoxAdv.SelectedIndex == 1 ? Publicas.TipoEmailComunicado.Financeiro : Publicas.TipoEmailComunicado.Diretoria));
                    _email.DescricaoTipoEmail = Publicas.GetDescription(_email.TipoEmail, "");
                    _listaEmail.Add(_email);
                    _email.Id = _listaEmail.Count() + 1;
                }

                emailGridGroupingControl.DataSource = new List<EmailEnvioComunicado>();
                emailGridGroupingControl.DataSource = _listaEmail;

                emailTextBoxExt.Text = string.Empty;
                emailTextBoxExt.Focus();
            }
        }

        private void grupoEmailComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                emailTextBoxExt.Focus();

            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tabControlAdv1.SelectedTab = BeneficiotabPageAdv;
                quantidadeAplicarInjustificadasCurrencyTextBox.Focus();
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.emailGridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;
            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                
                if (dr != null)
                {
                    foreach (var item in _listaEmail.Where(w => w.Id == (int)dr["Id"]))
                    {
                        item.Excluido = true;
                    }

                    emailGridGroupingControl.DataSource = new List<EmailEnvioComunicado>();
                    emailGridGroupingControl.DataSource = _listaEmail;
                }
            }
        }

        private void emailGridGroupingControl_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            GridRecordRow rec = this.emailGridGroupingControl.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;
            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;

                if (dr != null && (bool)dr["Excluido"])
                {
                    e.Style.TextColor = Color.Red;
                    e.Style.CellTipText = "Será excluído ao gravar.";
                }
            }

        }

        private void emailGridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            _rowIndex = e.Inner.RowIndex;
        }

        private void emailTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
                return;

            if (string.IsNullOrEmpty(emailTextBox.Text.Trim()))
                return;

            if (!Publicas.ValidarEmail(emailTextBox.Text.Trim()))
            {
                //_emailInvalido = true;
                new Notificacoes.Mensagem("E-mail inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                emailTextBox.Focus();
                return;
            }
        }

        private void grupoEmailComboBoxAdv_Enter(object sender, EventArgs e)
        {
            if (_emailInvalido)
            {
                ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
                emailTextBoxExt.Focus();
                return;
            }
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.emailGridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;
            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;

                if (dr != null)
                {
                    emailTextBoxExt.Text = (string)dr["Email"];
                    emailAtivoCheckBoxAdv.Checked = (bool)dr["Ativo"];
                    grupoEmailComboBoxAdv.SelectedIndex = ((Publicas.TipoEmailComunicado)dr["TipoEmail"] == Publicas.TipoEmailComunicado.Juridico ? 0 :
                                                          ((Publicas.TipoEmailComunicado)dr["TipoEmail"] == Publicas.TipoEmailComunicado.Financeiro ? 1 : 2));
                }
            }
        }

        private void emailGridGroupingControl_TableControlCellDoubleClick(object sender, GridTableControlCellClickEventArgs e)
        {
            alterarToolStripMenuItem_Click(sender, e);
        }

        private void empresaTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(empresaTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(empresaTextBox.Text.Trim());
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(empresaTextBox.Text))
            {
                new Pesquisas.Empresas().ShowDialog();

                empresaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(empresaTextBox.Text) || empresaTextBox.Text == "0")
                {
                    empresaTextBox.Text = string.Empty;
                    empresaTextBox.Focus();
                    return;
                }

                empresaTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void avaliaColaboradoresCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeAbreviadoTextBox.Focus();

            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }
    }
}
