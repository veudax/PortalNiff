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

namespace Suportte.GestaoEstrategica
{
    public partial class CalculoMetas : Form
    {
        public CalculoMetas()
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
                    MesesGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    MesesGrid.ColorStyles = ColorStyles.Office2010Black;
                    MesesGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    MesesGrid.BackColor = Publicas._panelTitulo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Metas _metas;
        Classes.CalculoMetas _calculoMetas;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.MesesUsadoNoCalculo> _listaMetasUsadas;
        List<Classes.ValoresDasMetas> _listaValores;
        List<Classes.ValoresDasMetas> _listaValoresMetaSelecionada;

        GridCurrentCell _colunaCorrente;

        string _referenciaTela;
        string _referencia;
        string _referenciaInicio;
        int _diasCorridos;
        //int _diasUteis;
        //int _folgasComplementares;
        //int _diasFeriados;
        int _mes;
        int _mesAux;
        int _ano;
        int _rowIndex = 0;
        decimal _media = 0;

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

        private void CalculoMetas_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);

            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

            #region Grid
            GridMetroColors metroColor = new GridMetroColors();
            metroColor.GroupDropAreaColor.BackColor = Publicas._bordaEntrada;

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

            MesesGrid.SortIconPlacement = SortIconPlacement.Left;
            MesesGrid.TopLevelGroupOptions.ShowFilterBar = false;
            MesesGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            MesesGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            MesesGrid.RecordNavigationBar.Label = "Mês Anteriores";
            MesesGrid.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < MesesGrid.TableDescriptor.Columns.Count; i++)
            {
                MesesGrid.TableDescriptor.Columns[i].AllowFilter = true;
                MesesGrid.TableDescriptor.Columns[i].AllowSort = true;
                MesesGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                MesesGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                MesesGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.MesesGrid.SetMetroStyle(metroColor);
                this.MesesGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.MesesGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.MesesGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            this.MesesGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.MesesGrid.TableDescriptor.TableOptions.CaptionRowHeight = 35;
            this.MesesGrid.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 35;
            this.MesesGrid.GridOfficeScrollBars = OfficeScrollBars.Metro;

            #endregion

            #region coloca os botões centralizados
            int espacoEntreBotoes = limparButton.Left - (gravarButton.Left + gravarButton.Width);

            gravarButton.Left = botoesPanel.Width / 3;
            limparButton.Left = gravarButton.Left + limparButton.Width + espacoEntreBotoes;
            excluirButton.Left = limparButton.Left + limparButton.Width + espacoEntreBotoes;
            #endregion
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

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void DiasUteisCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
            }
            catch { }
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                codigoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                DiasUteisRadioButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void DiasUteisCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                DiasCorridosCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                RealizadoRadioButton.Focus();
            }
        }

        private void DiasCorridosCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                 FeriasCurrency.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DiasUteisCurrencyTextBox.Focus();
            }
        }

        private void PermiteAlterarCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                FeriasCurrency.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FeriadosText.Focus();
            }
        }

        private void ValorCalculadoCurrencyText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                AumentarRadioButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DissidioCurrency.Focus();
            }
        }

        private void AumentarRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PercentualCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ValorCalculadoCurrencyText.Focus();
            }
        }

        private void PercentualCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ResultadoFinalCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReduzirRadioButton.Focus();
            }
        }

        private void ResultadoFinalCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PercentualCurrencyTextBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
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

        private void DiasUteisCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;
            }
            catch { }

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
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
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Metas().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _metas = new MetasBO().Consultar(Convert.ToInt32(codigoTextBox.Text));

            if (!_metas.Existe)
            {
                new Notificacoes.Mensagem("Meta não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            if (!_metas.Ativo)
            {
                new Notificacoes.Mensagem("Meta não está ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _metas.Descricao;

            PerspectivaLabel.Text = Publicas.GetDescription(_metas.Perspectiva, "");
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }
            
            if (referenciaMaskedEditBox.ClipText.Trim() == "")
            {
                new Notificacoes.Mensagem("Informe o mês/ano desejado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            try
            {
                int validacao = Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(3, 4));
                validacao = Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(0, 2));

                if (validacao > 12)
                {
                    new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                referenciaMaskedEditBox.Text = validacao.ToString("00") + Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(3, 4));
            }
            catch
            {
                new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            referenciaMaskedEditBox.Cursor = Cursors.WaitCursor;
            Publicas._mensagemSistema = "Aguarde, Pesquisando...";

            _referenciaTela = referenciaMaskedEditBox.Text.Substring(3, 4) + referenciaMaskedEditBox.Text.Substring(0, 2).PadRight(2, '0');

            _mes = Convert.ToInt32(_referenciaTela.Substring(4, 2));
            _ano = Convert.ToInt32(_referenciaTela.Substring(0, 4));

            if (_mes == 12)
            {
                _diasCorridos = Convert.ToInt32(Convert.ToDateTime("01/01/" + (_ano + 1).ToString()).Subtract(Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString())).TotalDays);
               //_diasUteis = new FeriadoBO().DiasUteis(_empresa.IdEmpresa, Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString()), Convert.ToDateTime("01/01/" + (_ano + 1).ToString()).AddDays(-1));

                #region Folgas Complementares
                if (_empresa.CodigoEmpresaGlobus == "009/001")
                {
                    //_folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa, Convert.ToDateTime("26/11/" + _ano.ToString()), Convert.ToDateTime("25/" + _mes.ToString("00") + "/" + _ano.ToString()));
                    //_diasFeriados = new FeriadoBO().QuantidadeDeFeriados(_empresa.IdEmpresa, Convert.ToDateTime("26/11/" + _ano.ToString()), Convert.ToDateTime("25/" + _mes.ToString("00") + "/" + _ano.ToString()));
                }
                else
                {
                    if (_empresa.CodigoEmpresaGlobus == "001/001" || _empresa.CodigoEmpresaGlobus == "001/002" || _empresa.CodigoEmpresaGlobus == "026/001" || _empresa.CodigoEmpresaGlobus == "006/001")
                    {
                       // _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa, Convert.ToDateTime("21/11/" + _ano.ToString()), Convert.ToDateTime("15/" + _mes.ToString("00") + "/" + _ano.ToString()));
//_diasFeriados = new FeriadoBO().QuantidadeDeFeriados(_empresa.IdEmpresa, Convert.ToDateTime("21/11/" + _ano.ToString()), Convert.ToDateTime("15/" + _mes.ToString("00") + "/" + _ano.ToString()));
                    }
                    else
                    {
                      //  _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa, Convert.ToDateTime("21/11/" + _ano.ToString()), Convert.ToDateTime("20/" + _mes.ToString("00") + "/" + _ano.ToString()));
                      //  _diasFeriados = new FeriadoBO().QuantidadeDeFeriados(_empresa.IdEmpresa, Convert.ToDateTime("21/11/" + _ano.ToString()), Convert.ToDateTime("20/" + _mes.ToString("00") + "/" + _ano.ToString()));
                    }
                }
                #endregion
            }
            else
            {
                _diasCorridos = Convert.ToInt32(Convert.ToDateTime("01/" + (_mes + 1).ToString("00") + "/" + _ano.ToString()).Subtract(Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString())).TotalDays);
                //_diasUteis = new FeriadoBO().DiasUteis(_empresa.IdEmpresa, Convert.ToDateTime("01/" + _mes.ToString("00") + "/" + _ano.ToString()), Convert.ToDateTime("01/" + (_mes + 1).ToString("00") + "/" + _ano.ToString()).AddDays(-1));

                #region Folgas Complementares
                if (_mes == 01)
                {
                    if (_empresa.CodigoEmpresaGlobus == "009/001")
                    {
                        //_folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa, Convert.ToDateTime("26/12/" + (_ano - 1).ToString()), Convert.ToDateTime("25/" + _mes.ToString("00") + "/" + _ano.ToString()));
                        //_diasFeriados = new FeriadoBO().QuantidadeDeFeriados(_empresa.IdEmpresa, Convert.ToDateTime("26/12/" + (_ano - 1).ToString()), Convert.ToDateTime("25/" + _mes.ToString("00") + "/" + _ano.ToString()));
                    }
                    else
                    {
                        if (_empresa.CodigoEmpresaGlobus == "001/001" || _empresa.CodigoEmpresaGlobus == "001/002" || _empresa.CodigoEmpresaGlobus == "026/001" || _empresa.CodigoEmpresaGlobus == "006/001")
                        {
                         //   _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa, Convert.ToDateTime("16/12/" + (_ano - 1).ToString()), Convert.ToDateTime("20/" + _mes.ToString("00") + "/" + _ano.ToString()));
                         //   _diasFeriados = new FeriadoBO().QuantidadeDeFeriados(_empresa.IdEmpresa, Convert.ToDateTime("16/12/" + (_ano - 1).ToString()), Convert.ToDateTime("20/" + _mes.ToString("00") + "/" + _ano.ToString()));
                        }
                        else
                        {
                         //   _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa, Convert.ToDateTime("21/12/" + (_ano - 1).ToString()), Convert.ToDateTime("20/" + _mes.ToString("00") + "/" + _ano.ToString()));
                          //  _diasFeriados = new FeriadoBO().QuantidadeDeFeriados(_empresa.IdEmpresa, Convert.ToDateTime("21/12/" + (_ano - 1).ToString()), Convert.ToDateTime("20/" + _mes.ToString("00") + "/" + _ano.ToString()));
                        }
                    }
                }
                else
                {
                    if (_empresa.CodigoEmpresaGlobus == "009/001")
                    {
                      //  _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa, Convert.ToDateTime("26/" + (_mes - 1).ToString() + "/" + _ano.ToString()), Convert.ToDateTime("25/" + _mes.ToString("00") + "/" + _ano.ToString()));
//_diasFeriados = new FeriadoBO().QuantidadeDeFeriados(_empresa.IdEmpresa, Convert.ToDateTime("26/" + (_mes - 1).ToString() + "/" + _ano.ToString()), Convert.ToDateTime("25/" + _mes.ToString("00") + "/" + _ano.ToString()));
                    }
                    else
                    {
                      //  _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa, Convert.ToDateTime("21/" + (_mes - 1).ToString() + "/" + _ano.ToString()), Convert.ToDateTime("20/" + _mes.ToString("00") + "/" + _ano.ToString()));
//_diasFeriados = new FeriadoBO().QuantidadeDeFeriados(_empresa.IdEmpresa, Convert.ToDateTime("21/" + (_mes - 1).ToString() + "/" + _ano.ToString()), Convert.ToDateTime("20/" + _mes.ToString("00") + "/" + _ano.ToString()));
                    }
                }
                #endregion
            }

            DiasCorridosCurrencyTextBox.DecimalValue = _diasCorridos;
            //DiasUteisCurrencyTextBox.DecimalValue = _diasUteis;
            //FeriadosText.DecimalValue = _diasFeriados;

            _mesAux = _mes - 7;            

            if (_mesAux < 1 || _mesAux > 12)
            {
                _mesAux = 12 + _mesAux;
                _referenciaInicio = (_ano - 1).ToString() + _mesAux.ToString("00");
            }
            else
            {
                _referenciaInicio = _ano.ToString() + _mesAux.ToString("00");
            }

            _mesAux = _mes - 1;

            // um ano retroativo
            _referenciaInicio = (_ano - 1).ToString() + _mes.ToString();

            if (_mesAux < 1 || _mesAux > 12)
            {
                _mesAux = 12 + _mesAux;
                _referencia = (_ano - 1).ToString() + _mesAux.ToString("00");
            }
            else
            {
                _referencia = _ano.ToString() + _mesAux.ToString("00");
            }

            _calculoMetas = new MetasBO().Consultar(true, _empresa.IdEmpresa, _referenciaTela, _metas.Id);

            _listaValoresMetaSelecionada = new MetasBO().Listar(true, _empresa.IdEmpresa, _referenciaTela, _referenciaTela, _metas.Id);
            _listaValores = new MetasBO().Listar(true, _empresa.IdEmpresa, _referencia, _referenciaInicio, _metas.Id);

            foreach (var itemV in _listaValores)
            {
                try
                {
                    itemV.MediaUPrevisto = itemV.Previsto / itemV.DiasUteis;
                    itemV.MediaURealizado = itemV.Realizado / itemV.DiasUteis;
                    itemV.MediaCPrevisto = itemV.Previsto / itemV.DiasCorridos;
                    itemV.MediaCRealizado = itemV.Realizado / itemV.DiasCorridos;

                    itemV.FolgaComplementarRealizada = new ValoresDasMetasNoGlobusBO().ValorPorEvento(_empresa.CodigoEmpresaGlobus, Convert.ToDateTime("01/" + itemV.Mes), nomeTextBox.Text, "375");
                    itemV.PLRRealizado = new ValoresDasMetasNoGlobusBO().ValorPorEvento(_empresa.CodigoEmpresaGlobus, Convert.ToDateTime("01/" + itemV.Mes), nomeTextBox.Text, "198, 731 ,1008");

                    if (itemV.Mes.Contains("01/"))
                    {
                        itemV.FeriasBase = FeriasCurrency.DecimalValue * (decimal)(10 / 100) * (decimal)1.33;

                        //if (_empresa.CodigoEmpresaGlobus == "009/001")
                        //    //_folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa,
                        //    //                                                          Convert.ToDateTime("26/12/" + (_ano - 1).ToString()),
                        //    //                                                          Convert.ToDateTime("25/" + itemV.Mes.Substring(0, 2) + "/" + itemV.Mes.Substring(3, 4)));
                        //else
                        //{
                        //    //if (_empresa.CodigoEmpresaGlobus == "001/001" || _empresa.CodigoEmpresaGlobus == "001/002" || _empresa.CodigoEmpresaGlobus == "026/001" || _empresa.CodigoEmpresaGlobus == "006/001")
                        //    //    _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa,
                        //    //                                                              Convert.ToDateTime("16/12/" + (_ano - 1).ToString()),
                        //    //                                                              Convert.ToDateTime("20/" + itemV.Mes.Substring(0, 2) + "/" + itemV.Mes.Substring(3, 4)));
                        //    //else
                        //    //    _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa,
                        //    //                                                              Convert.ToDateTime("21/12/" + (_ano - 1).ToString()),
                        //    //                                                              Convert.ToDateTime("20/" + itemV.Mes.Substring(0, 2) + "/" + itemV.Mes.Substring(3, 4)));
                        //}
                    }
                    else
                    {
                        if (itemV.Mes.Contains("12/"))
                        {
                            itemV.FeriasBase = FeriasCurrency.DecimalValue * (decimal)(8 / 100) * (decimal)1.33;

                            //if (_empresa.CodigoEmpresaGlobus == "009/001")
                            //    _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa,
                            //                                                               Convert.ToDateTime("26/11/" + (Convert.ToInt32(itemV.Mes.Substring(3, 4)) - 1).ToString()),
                            //                                                               Convert.ToDateTime("25/" + itemV.Mes.Substring(0, 2) + "/" + itemV.Mes.Substring(3, 4)));
                            //else
                            //{
                            //    if (_empresa.CodigoEmpresaGlobus == "001/001" || _empresa.CodigoEmpresaGlobus == "001/002" || _empresa.CodigoEmpresaGlobus == "026/001" || _empresa.CodigoEmpresaGlobus == "006/001")
                            //        _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa,
                            //                                                                   Convert.ToDateTime("21/11/" + (Convert.ToInt32(itemV.Mes.Substring(3, 4)) - 1).ToString()),
                            //                                                                   Convert.ToDateTime("15/" + itemV.Mes.Substring(0, 2) + "/" + itemV.Mes.Substring(3, 4)));
                            //    else
                            //        _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa,
                            //                                                                   Convert.ToDateTime("21/11/" + (Convert.ToInt32(itemV.Mes.Substring(3, 4)) - 1).ToString()),
                            //                                                                   Convert.ToDateTime("20/" + itemV.Mes.Substring(0, 2) + "/" + itemV.Mes.Substring(3, 4)));
                            //}
                        }
                        else
                        {
                            //if (itemV.Mes.Contains("07/"))
                            //    itemV.FeriasBase = FeriasCurrency.DecimalValue * (decimal)(10 / 100) * (decimal)1.33;
                            //else
                            //    itemV.FeriasBase = FeriasCurrency.DecimalValue * (decimal)(8 / 100) * (decimal)1.33;

                            //if (_empresa.CodigoEmpresaGlobus == "009/001")
                            //    _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa, 
                            //                                                              Convert.ToDateTime("26/" + (Convert.ToInt32(itemV.Mes.Substring(0, 2)) - 1).ToString() + "/" + itemV.Mes.Substring(3, 4)), 
                            //                                                              Convert.ToDateTime("25/" + itemV.Mes.Substring(0, 2) + "/" + itemV.Mes.Substring(3, 4)));
                            //else
                            //    _folgasComplementares = new FeriadoBO().FolgaComplementar(_empresa.IdEmpresa, 
                            //                                                              Convert.ToDateTime("21/" + (Convert.ToInt32(itemV.Mes.Substring(0, 2)) - 1).ToString() + "/" + itemV.Mes.Substring(3, 4)),
                            //                                                              Convert.ToDateTime("20/" + itemV.Mes.Substring(0, 2) + "/" + itemV.Mes.Substring(3, 4)));
                        }
                    }

                    //itemV.FolgaComplementares = _folgasComplementares;
                    // carregar o valor folga complementar
                }
                catch { }
            }
                         
            if (_calculoMetas.Existe)
            {
                _listaMetasUsadas = new MetasBO().ListarMesesUtilizados(_calculoMetas.Id);

                foreach (var item in _listaMetasUsadas)
                {
                    foreach (var itemV in _listaValores.Where(w => w.Referencia == item.Referencia))
                    {
                        itemV.Marcado = true;
                        break;
                    }
                }

                DiasUteisRadioButton.Checked = _calculoMetas.UsouDiasUteis;
                DiasCorridoRadioButton.Checked = _calculoMetas.UsouDiasCorridos;
                PrevistoRadioButton.Checked = _calculoMetas.UsouPrevisto;
                RealizadoRadioButton.Checked = _calculoMetas.UsouRealizado;
                PermiteAlterarCheckBox.Checked = _calculoMetas.PermitiuAlterar;
                AumentarRadioButton.Checked = _calculoMetas.Aumentou;
                ReduzirRadioButton.Checked = !_calculoMetas.Aumentou;
                DiasUteisCurrencyTextBox.DecimalValue = _calculoMetas.DiasUteis;
                DiasCorridosCurrencyTextBox.DecimalValue = _calculoMetas.DiasCorridos;
                PercentualCurrencyTextBox.DecimalValue = _calculoMetas.Percentual;
                FeriadosText.DecimalValue = _calculoMetas.DiasFeriados;
                PLRCurrency.DecimalValue = _calculoMetas.PLRPrevisto;
                DissidioCurrency.DecimalValue = _calculoMetas.Dissidio;
                FeriasCurrency.DecimalValue = _calculoMetas.FeriasBase;

                ValorCalculadoCurrencyText.DecimalValue = _calculoMetas.ValorCalculado;
                ResultadoFinalCurrencyTextBox.DecimalValue = _calculoMetas.ValorResultado;

                ValorCalculadoCurrencyText.CurrencyDecimalDigits = _metas.QuantidadeDecimais;
                ResultadoFinalCurrencyTextBox.CurrencyDecimalDigits = _metas.QuantidadeDecimais;
            }

            FeriasCurrency.DecimalValue = new MetasBO().FeriasBase(_empresa.IdEmpresa, referenciaMaskedEditBox.Text.Substring(3, 4), _metas.Id);

            MesesGrid.DataSource = _listaValores;

            for (int i = 0; i < MesesGrid.TableDescriptor.Columns.Count; i++)
            {
                if (MesesGrid.TableDescriptor.Columns[i].MappingName.Contains("Previsto") ||
                    MesesGrid.TableDescriptor.Columns[i].MappingName.Contains("Realizado") ||
                    MesesGrid.TableDescriptor.Columns[i].MappingName.Contains("FolgaComplementarRealizada") ||
                    MesesGrid.TableDescriptor.Columns[i].MappingName.Contains("PLR") ||
                    MesesGrid.TableDescriptor.Columns[i].MappingName.Contains("Ferias")) 
                {
                    MesesGrid.TableDescriptor.Columns[i].Appearance.AnyRecordFieldCell.CellValueType = typeof(decimal);
                    MesesGrid.TableDescriptor.Columns[i].Appearance.AnyRecordFieldCell.Format = "n" + _metas.QuantidadeDecimais;
                }

                if (!nomeTextBox.Text.ToUpper().Contains("FOLHA") && (MesesGrid.TableDescriptor.Columns[i].MappingName.Contains("Folga") ||
                                                                      MesesGrid.TableDescriptor.Columns[i].MappingName.Contains("PLR") ||
                                                                      MesesGrid.TableDescriptor.Columns[i].MappingName.Contains("Ferias")))
                    MesesGrid.TableDescriptor.VisibleColumns.Remove(MesesGrid.TableDescriptor.Columns[i].MappingName);
                else
                if (nomeTextBox.Text.ToUpper().Contains("FOLHA") && nomeTextBox.Text.ToUpper().Contains("ADM") && 
                    (MesesGrid.TableDescriptor.Columns[i].MappingName.Contains("Folga") ||
                     MesesGrid.TableDescriptor.Columns[i].MappingName.Contains("PLR") ||
                     MesesGrid.TableDescriptor.Columns[i].MappingName.Contains("Ferias")))
                    MesesGrid.TableDescriptor.VisibleColumns.Remove(MesesGrid.TableDescriptor.Columns[i].MappingName);

            }
            referenciaMaskedEditBox.Cursor = Cursors.Default;
            Publicas._mensagemSistema = "";

            gravarButton.Enabled = true;
            excluirButton.Enabled = _calculoMetas.Existe;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DiasUteisRadioButton_CheckChanged(object sender, EventArgs e)
        {
            decimal _valor = 0;
            int _qtd = 0;

            try
            {
                foreach (var item in _listaValores.Where(w => w.Marcado))
                {
                    if (ValorFixoRadioButton.Checked)
                    {
                        if (PrevistoRadioButton.Checked)
                            _valor = item.Previsto;
                        else
                            _valor = item.Realizado;
                    }
                    else
                    {
                        if (PrevistoRadioButton.Checked)
                        {
                            _valor = _valor + (DiasCorridoRadioButton.Checked ? item.MediaCPrevisto : item.MediaUPrevisto);
                            _qtd++;
                        }
                        else
                        {
                            _valor = _valor + (DiasCorridoRadioButton.Checked ? item.MediaCRealizado : item.MediaURealizado);
                            _qtd++;
                        }
                    }
                }

                if (ValorFixoRadioButton.Checked) // pega o último item selecionado
                    _media = Math.Round(_valor, _metas.QuantidadeDecimais);
                else
                if (DiasCorridoRadioButton.Checked)
                {
                    if (DiasCorridosCurrencyTextBox.DecimalValue != 0 && _qtd != 0)
                        _media = Math.Round((_valor / _qtd) * DiasCorridosCurrencyTextBox.DecimalValue, _metas.QuantidadeDecimais);
                }
                else
                {
                    if (DiasUteisCurrencyTextBox.DecimalValue != 0 && _qtd != 0)
                        _media = Math.Round((_valor / _qtd) * DiasUteisCurrencyTextBox.DecimalValue, _metas.QuantidadeDecimais);
                }
            }
            catch { }

            ValorCalculadoCurrencyText.Text = _media.ToString();

            if (AumentarRadioButton.Checked)
                ResultadoFinalCurrencyTextBox.DecimalValue = _media + (_media* (PercentualCurrencyTextBox.DecimalValue / 100));
            else
                ResultadoFinalCurrencyTextBox.DecimalValue = _media - (_media* (PercentualCurrencyTextBox.DecimalValue / 100));

            ValorCalculadoCurrencyText.CurrencyDecimalDigits = _metas.QuantidadeDecimais;
            ResultadoFinalCurrencyTextBox.CurrencyDecimalDigits = _metas.QuantidadeDecimais;

        }

        private void MesesGrid_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            decimal _media = 0;

            try
            {
                _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.MesesGrid.Table.DisplayElements[_rowIndex] as GridRecordRow;

                string nomeColuna = MesesGrid.TableDescriptor.Columns[_colunaCorrente.ColIndex - 1].MappingName;
                bool marcado = false;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        marcado = (bool)dr[nomeColuna];

                        if (ValorFixoRadioButton.Checked)
                            _listaValores.ForEach(u => u.Marcado = false);

                        foreach (var item in _listaValores.Where(w => w.Referencia == (string)dr["Referencia"]))
                        {
                            if (!marcado)
                                item.Marcado = false;
                            else
                                item.Marcado = true;
                        }
                    }
                }

                decimal _valor = 0;
                int _qtd = 0;

                foreach (var item in _listaValores.Where(w => w.Marcado))
                {
                    if (ValorFixoRadioButton.Checked)
                    {
                        if (PrevistoRadioButton.Checked)
                            _valor = item.Previsto;
                        else
                            _valor = item.Realizado;
                    }
                    else
                    {
                        if (PrevistoRadioButton.Checked)
                        {
                            _valor = _valor + (DiasCorridoRadioButton.Checked ?  item.MediaCPrevisto : item.MediaUPrevisto);
                            _qtd++;
                        }
                        else
                        {
                            _valor = _valor + (DiasCorridoRadioButton.Checked ? item.MediaCRealizado : item.MediaURealizado);
                            _qtd++;
                        }
                    }
                }

                if (ValorFixoRadioButton.Checked) // pega o último item selecionado
                    _media = Math.Round(_valor, _metas.QuantidadeDecimais);
                else
                if (DiasCorridoRadioButton.Checked)
                {
                    if (DiasCorridosCurrencyTextBox.DecimalValue != 0)
                        _media = Math.Round((_valor / _qtd) * DiasCorridosCurrencyTextBox.DecimalValue, _metas.QuantidadeDecimais);
                }
                else
                {
                    if (DiasUteisCurrencyTextBox.DecimalValue != 0)
                        _media = Math.Round((_valor / _qtd) * DiasUteisCurrencyTextBox.DecimalValue, _metas.QuantidadeDecimais);
                }
            }
            catch { }

            ValorCalculadoCurrencyText.Text = _media.ToString();

            if (AumentarRadioButton.Checked)
                ResultadoFinalCurrencyTextBox.DecimalValue = _media + (_media * (PercentualCurrencyTextBox.DecimalValue / 100));
            else
                ResultadoFinalCurrencyTextBox.DecimalValue = _media - (_media * (PercentualCurrencyTextBox.DecimalValue / 100));

            ValorCalculadoCurrencyText.CurrencyDecimalDigits = _metas.QuantidadeDecimais;
            ResultadoFinalCurrencyTextBox.CurrencyDecimalDigits = _metas.QuantidadeDecimais;

            MesesGrid.DataSource = new List<ItensDaAutoAvaliacao>();

            MesesGrid.DataSource = _listaValores;
            MesesGrid.Refresh();
        }

        private void MesesGrid_TableControlMouseDown(object sender, GridTableControlMouseEventArgs e)
        {
            _colunaCorrente = MesesGrid.TableControl.CurrentCell;
        }

        private void MesesGrid_DataSourceChanged(object sender, EventArgs e)
        {
            
        }

        private void PercentualCurrencyTextBox_DecimalValueChanged(object sender, EventArgs e)
        {
            decimal _valor = 0;

            _valor = ValorCalculadoCurrencyText.DecimalValue;

            if (AumentarRadioButton.Checked)
                ResultadoFinalCurrencyTextBox.DecimalValue = _valor + (_valor * (PercentualCurrencyTextBox.DecimalValue / 100));
            else
                ResultadoFinalCurrencyTextBox.DecimalValue = _valor - (_valor * (PercentualCurrencyTextBox.DecimalValue / 100));
        }

        private void PermiteAlterarCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            ValorCalculadoCurrencyText.Enabled = PermiteAlterarCheckBox.Checked;
            ResultadoFinalCurrencyTextBox.Enabled = PermiteAlterarCheckBox.Checked;
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            _media = 0;

            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            PerspectivaLabel.Text = string.Empty;

            ValorCalculadoCurrencyText.DecimalValue = 0;
            ResultadoFinalCurrencyTextBox.DecimalValue = 0;
            PercentualCurrencyTextBox.DecimalValue = 0;
            FeriasCurrency.DecimalValue = 0;
            PLRCurrency.DecimalValue = 0;
            FeriasCurrency.DecimalValue = 0;

            if (_listaMetasUsadas != null)
                _listaMetasUsadas.Clear();

            if (_listaValores != null)
                _listaValores.Clear();

            MesesGrid.DataSource = new List<ValoresDasMetas>();

            codigoTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new MetasBO().ExcluirCalculoMetas(_calculoMetas.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            gravarButton.Cursor = Cursors.WaitCursor;
            Publicas._mensagemSistema = "Aguarde, Gravando...";
            
            if (_listaValoresMetaSelecionada == null)
                _listaValoresMetaSelecionada = new List<ValoresDasMetas>();

            if (_listaValoresMetaSelecionada.Count == 0)
            {                
                ValoresDasMetas _val = new ValoresDasMetas();

                _val.IdEmpresa = _empresa.IdEmpresa;
                _val.IdMetas = _metas.Id;
                _val.DiasCorridos = (int)DiasCorridosCurrencyTextBox.DecimalValue;
                _val.DiasUteis = (int)DiasUteisCurrencyTextBox.DecimalValue;
                _val.DiasFeriados = (int)FeriadosText.DecimalValue;
                _val.Referencia = _referenciaTela;
                _val.Previsto = ResultadoFinalCurrencyTextBox.DecimalValue;
                _val.Descricao = nomeTextBox.Text;
                _val.PLRPrevisto = PLRCurrency.DecimalValue;
                _val.Dissidio = DissidioCurrency.DecimalValue;
                _val.FeriasBase = FeriasCurrency.DecimalValue;
                _listaValoresMetaSelecionada.Add(_val);
            }

            foreach (var item in _listaValoresMetaSelecionada)
            {
                item.DiasCorridos = (int)DiasCorridosCurrencyTextBox.DecimalValue;
                item.DiasUteis = (int)DiasUteisCurrencyTextBox.DecimalValue;
                item.DiasFeriados = (int)FeriadosText.DecimalValue;
                item.Referencia = _referenciaTela;
                item.Previsto = ResultadoFinalCurrencyTextBox.DecimalValue;
                item.PLRPrevisto = PLRCurrency.DecimalValue;
                item.Dissidio = DissidioCurrency.DecimalValue;
                item.FeriasBase = FeriasCurrency.DecimalValue;
            }

            if (_calculoMetas == null)
                _calculoMetas = new Classes.CalculoMetas();

            _calculoMetas.IdEmpresa = _empresa.IdEmpresa;
            _calculoMetas.IdMetas = _metas.Id;
            _calculoMetas.Referencia = _referenciaTela;
            _calculoMetas.DiasCorridos = (int)DiasCorridosCurrencyTextBox.DecimalValue;
            _calculoMetas.DiasUteis = (int)DiasUteisCurrencyTextBox.DecimalValue;
            _calculoMetas.Percentual = PercentualCurrencyTextBox.DecimalValue;
            _calculoMetas.ValorCalculado = ValorCalculadoCurrencyText.DecimalValue;
            _calculoMetas.ValorResultadoOriginal = _calculoMetas.ValorResultado;
            _calculoMetas.ValorResultado = ResultadoFinalCurrencyTextBox.DecimalValue;
            _calculoMetas.UsouDiasCorridos = DiasCorridoRadioButton.Checked;
            _calculoMetas.UsouDiasUteis = DiasUteisRadioButton.Checked;
            _calculoMetas.UsouPrevisto = PrevistoRadioButton.Checked;
            _calculoMetas.UsouRealizado = RealizadoRadioButton.Checked;
            _calculoMetas.PermitiuAlterar = PermiteAlterarCheckBox.Checked;
            _calculoMetas.Aumentou = AumentarRadioButton.Checked;
            _calculoMetas.DiasFeriados = (int)FeriadosText.DecimalValue;
            _calculoMetas.PLRPrevisto = PLRCurrency.DecimalValue;
            _calculoMetas.Dissidio = DissidioCurrency.DecimalValue;
            _calculoMetas.FeriasBase = FeriasCurrency.DecimalValue;

            List<Classes.MesesUsadoNoCalculo> _mesesSelecionados = new List<MesesUsadoNoCalculo>();

            foreach (var item in _listaValores.Where(w => w.Marcado))
            {
                Classes.MesesUsadoNoCalculo _selecionado = new MesesUsadoNoCalculo();

                _selecionado.DiasCorridos = item.DiasCorridos;
                _selecionado.DiasUteis = item.DiasUteis;
                _selecionado.IdValorMetas = item.Id;
                _selecionado.Previsto = item.Previsto;
                _selecionado.Realizado = item.Realizado;
                _selecionado.IdCalculo = _calculoMetas.Id;

                _mesesSelecionados.Add(_selecionado);
            }

            if (!new MetasBO().Gravar(_calculoMetas, _mesesSelecionados))
            {
                gravarButton.Cursor = Cursors.Default;
                Publicas._mensagemSistema = "";
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!new ValoresDasMetasNoGlobusBO().Gravar(_listaValoresMetaSelecionada, new List<ItensAvaliacaoMetas>(), referenciaMaskedEditBox.ClipText))
            {
                gravarButton.Cursor = Cursors.Default;
                Publicas._mensagemSistema = "";
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }
            gravarButton.Cursor = Cursors.Default;
            Publicas._mensagemSistema = "";
            limparButton_Click(sender, e);
        }

        private void DiasUteisRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PrevistoRadioButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void DiasCorridoRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PrevistoRadioButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DiasUteisRadioButton.Focus();
            }
        }

        private void ValorFixoRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PrevistoRadioButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DiasCorridoRadioButton.Focus();
            }
        }

        private void PrevistoRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                DiasUteisCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ValorFixoRadioButton.Focus();
            }
        }

        private void RealizadoRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                DiasUteisCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PrevistoRadioButton.Focus();
            }
        }

        private void FeriasCurrency_Validating(object sender, CancelEventArgs e)
        {
            FeriasCurrency.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }

            MesesGrid.DataSource = new List<ValoresDasMetas>();
            decimal _mediaU = 0;
            decimal _mediaC = 0;
            decimal _valor = FeriasCurrency.DecimalValue;
            decimal _percentual10 = (decimal)0.10;
            decimal _percentual8 = (decimal)0.08;
            decimal _abono = (decimal)1.33;
            int _quantidade = 0;

            foreach (var itemV in _listaValores)
            {
                try
                {
                    itemV.Marcado = true;

                    if (itemV.Mes.Contains("01/") || itemV.Mes.Contains("07/"))
                        itemV.FeriasBase = _valor * _percentual10 * _abono;
                    else
                        itemV.FeriasBase = _valor * _percentual8 * _abono;

                    itemV.RealizadoPuro = itemV.Realizado - itemV.FeriasBase - itemV.FolgaComplementarRealizada - itemV.PLRRealizado;
                    itemV.MediaURealizado = itemV.RealizadoPuro / itemV.DiasUteis;
                    itemV.MediaCRealizado = itemV.RealizadoPuro / itemV.DiasCorridos;

                    if (itemV.MediaURealizado < 0)
                        itemV.MediaURealizado = 0;
                    if (itemV.MediaCRealizado < 0)
                        itemV.MediaCRealizado = 0;

                    _mediaU = _mediaU + itemV.MediaURealizado;
                    _mediaC = _mediaC + itemV.MediaCRealizado;

                    if (itemV.Realizado > 0)
                        _quantidade++;
                }
                catch { }
            }

            MesesGrid.DataSource = _listaValores;

            _mediaC = _mediaC / _quantidade;
            _mediaU = _mediaU / _quantidade;

            try
            {
                string _mes = _listaValores.Where(w => w.FolgaComplementarRealizada > 0 &&
                                                                  Convert.ToInt32(w.Referencia) < Convert.ToInt32(referenciaMaskedEditBox.Text.Substring(3, 4) + referenciaMaskedEditBox.Text.Substring(0, 2)))
                                                         .Max(m => m.Mes);

                decimal _folgas = 0;

                foreach (var item in _listaValores.Where(w => w.Mes == _mes))
                {
                    _folgas = item.FolgaComplementarRealizada;
                }
                
                decimal _ValorFeriado = (FeriadosText.DecimalValue * _folgas);

                if (referenciaMaskedEditBox.Text.Contains("01/") || referenciaMaskedEditBox.Text.Contains("07/"))
                    ValorCalculadoCurrencyText.DecimalValue = (_valor * _percentual10 * _abono);
                else
                    ValorCalculadoCurrencyText.DecimalValue = (_valor * _percentual8 * _abono);

                ValorCalculadoCurrencyText.DecimalValue = (ValorCalculadoCurrencyText.DecimalValue +
                    (_mediaU * DiasUteisCurrencyTextBox.DecimalValue) + PLRCurrency.DecimalValue + _ValorFeriado) *
                    (DissidioCurrency.DecimalValue == 0 ? 1 : DissidioCurrency.DecimalValue);

                ResultadoFinalCurrencyTextBox.DecimalValue = ValorCalculadoCurrencyText.DecimalValue;
            }
            catch { }
        }

        private void FeriasCurrency_Validating(object sender, EventArgs e)
        {
            FeriasCurrency_Validating(sender, new CancelEventArgs());
        }

        private void FeriadosText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PermiteAlterarCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DiasCorridosCurrencyTextBox.Focus();
            }
        }

        private void FeriasCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PLRCurrency.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PermiteAlterarCheckBox.Focus();
            }
        }

        private void PLRCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                DissidioCurrency.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FeriasCurrency.Focus();
            }
        }

        private void DissidioCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ValorCalculadoCurrencyText.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PLRCurrency.Focus();
            }
        }
    }
}
