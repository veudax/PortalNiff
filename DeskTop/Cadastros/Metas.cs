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
{// danificado
    public partial class Metas : Form
    {
        public Metas()
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
                    RubricasComboBox.Style = MultiSelectionComboBoxStyle.Office2016Black;
                    OcorrenciasComboBox.Style = MultiSelectionComboBoxStyle.Office2016Black;
                    AreasComboBox.Style = MultiSelectionComboBoxStyle.Office2016Black;
                    FuncoesComboBox.Style = MultiSelectionComboBoxStyle.Office2016Black;
                    RubricasComboBox.FlatBorderColor = Publicas.padraoAnoNovo_BordaEntrada;
                    OcorrenciasComboBox.FlatBorderColor = Publicas.padraoAnoNovo_BordaEntrada;
                    AreasComboBox.FlatBorderColor = Publicas.padraoAnoNovo_BordaEntrada;
                    FuncoesComboBox.FlatBorderColor = Publicas.padraoAnoNovo_BordaEntrada;
                    RubricasComboBox.MetroColor = Publicas._panelTitulo;
                    OcorrenciasComboBox.MetroColor = Publicas._panelTitulo; 
                    AreasComboBox.MetroColor = Publicas._panelTitulo; 
                    FuncoesComboBox.MetroColor = Publicas._panelTitulo;

                    gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl1.BackColor = Publicas._panelTitulo;

                    DecimaisCurrencyTextBox.PositiveColor = Publicas._fonte;
                    NivelTotalizadorCurrencyText.PositiveColor = Publicas._fonte;
                    DecimaisCurrencyTextBox.ZeroColor = Publicas._fonte;
                    NivelTotalizadorCurrencyText.ZeroColor = Publicas._fonte;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Metas _metas;
        VisoesBI _visoesBI;
        List<VisoesBI> _listaBI;
        List<DetalheVisoes> _listaDetalhes;
        List<OcorrenciasGlobus> _listaOcorrencias;
        List<AreaGlobus> _listaAreas;
        List<FuncoesGlobus> _listaFuncoes;
        List<MetasBIItens> _listaBIItens;
        Publicas.RegraFormulaMetas _regraFormula;
        Classes.Empresa _empresa;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasAutorizadas;
        Classes.RateioBeneficios.PlanoContabil _plano;
        Classes.RateioBeneficios.ContasContabeis _contas;
        List<Classes.MetasContasContabeis> _listaMetas;

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

        private void Metas_Shown(object sender, EventArgs e)
        {
           
            int _h = Publicas._heigthTelaInicial - Publicas._heigthBarraTelaInicial - Publicas._heigthTituloTelaInicial;

            if (_h <= this.Height)
                this.Location = new Point(this.Left, 60);

            _listaBI = new VisoesBIBO().Listar(true);

            _listaEmpresas = new EmpresaBO().Listar(false);
            _listaEmpresasAutorizadas = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._idUsuario);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            
            VisoesBIComboBox.DataSource = _listaBI.OrderBy(o => o.Descricao).ToList();
            VisoesBIComboBox.DisplayMember = "Descricao";
            VisoesBIComboBox.SelectedIndex = -1;

            perspectivaComboBox.Items.AddRange(new object[] { "Cliente", "Processos", "Financeiro", "Aprendizagem e crescimento" });

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
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                perspectivaComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void tipoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                perspectivaComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void perspectivaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                DecimaisCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                UsaAvaliacaoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DecimaisCurrencyTextBox.Focus();
            }
        }

        private void cargoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
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

        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '+')
            {
                codigoTextBox.Text = string.Empty;
                proximoButton_Click(sender, e);
            }
        }

        private void cargoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }            
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new MetasBO().Proximo().ToString();
            ativoCheckBox.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaMetas == null)
                _listaMetas = new List<MetasContasContabeis>();

            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a descrição.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            if (_metas == null)
                _metas = new Classes.Metas();

            _metas.Id = Convert.ToInt32(codigoTextBox.Text);
            _metas.Descricao = nomeTextBox.Text;
            _metas.Ativo = ativoCheckBox.Checked;
            _metas.TextoBI = TextoBITextBox.Text;

            _metas.Perspectiva = (perspectivaComboBox.SelectedIndex == 0 ? Publicas.Perspectivas.Cliente :
                (perspectivaComboBox.SelectedIndex == 1 ? Publicas.Perspectivas.Processos :
                (perspectivaComboBox.SelectedIndex == 2 ? Publicas.Perspectivas.Financeira : Publicas.Perspectivas.Aprendizagem)));
            _metas.Tipo = Publicas.TipoDeMetas.Resultado;
            _metas.Regra = _regraFormula;
            _metas.ExibeNoDRE = ExibirNoDRECheckBox.Checked;
            _metas.GrupoTotalizador = TotalizadorCheckBox.Checked;

            _metas.FormulaTotalizador = FormulaTotalizadorTextBox.Text;
            _metas.NivelCalculo = (int)NivelTotalizadorCurrencyText.DecimalValue;

            _metas.UsaNaAvaliacao = UsaAvaliacaoCheckBox.Checked;
            _metas.UsaNaGestao = UsarGestaoCheckBox.Checked;
            _metas.QuantidadeDecimais = (int)DecimaisCurrencyTextBox.DecimalValue;
            _metas.UsaKMRodado = UsarKmRodadoCheckBox.Checked;
            _metas.PrevistoPermiteAlterar = true;
            _metas.RealizadoPermiteAlterar = true;

            if (_visoesBI != null)
                _metas.IdBI = _visoesBI.Id;

            if (formula1RadioButton.Checked)
                _metas.Formula = formula1RadioButton.Text;
            else
            {
                if (formula2RadioButton.Checked)
                    _metas.Formula = formula2RadioButton.Text;
                else
                    _metas.Formula = formula3RadioButton.Text;
            }

            if (_listaBIItens == null)
                _listaBIItens = new List<MetasBIItens>();
            else
                _listaBIItens.Clear();

            foreach (var item in RubricasComboBox.SelectedItems)
            {
                MetasBIItens _metasB = new MetasBIItens();
                _metasB.IdMetas = _metas.Id;
                _metasB.Tipo = "R";
                _metasB.Descricao = item.ToString();

                _listaBIItens.Add(_metasB);
            }

            foreach (var item in OcorrenciasComboBox.SelectedItems)
            {
                MetasBIItens _metasB = new MetasBIItens();
                _metasB.IdMetas = _metas.Id;
                _metasB.Tipo = "O";
                _metasB.Descricao = item.ToString();
                _metasB.Codigo = Convert.ToInt32(_metasB.Descricao.Substring(0, 3));

                _listaBIItens.Add(_metasB);
            }

            foreach (var item in FuncoesComboBox.SelectedItems)
            {
                MetasBIItens _metasB = new MetasBIItens();
                _metasB.IdMetas = _metas.Id;
                _metasB.Tipo = "F";
                _metasB.Descricao = item.ToString();
                _metasB.Codigo = Convert.ToInt32(_metasB.Descricao.Substring(0, 3));

                _listaBIItens.Add(_metasB);
            }

            foreach (var item in AreasComboBox.SelectedItems)
            {
                MetasBIItens _metasB = new MetasBIItens();
                _metasB.IdMetas = _metas.Id;
                _metasB.Tipo = "A";
                _metasB.Descricao = item.ToString();
                _metasB.Codigo = Convert.ToInt32(_metasB.Descricao.Substring(0, 3));

                _listaBIItens.Add(_metasB);
            }

            //if (!new MetasBO().Gravar(_metas, _listaBIItens, _listaMetas)) 
            //{
            //    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
            //    return;
            //}

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            ativoCheckBox.Checked = false;
            perspectivaComboBox.SelectedIndex = -1;
            perspectivaComboBox.Text = string.Empty;
            TextoBITextBox.Text = string.Empty;
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

            DecimaisCurrencyTextBox.DecimalValue = 2;
            UsaAvaliacaoCheckBox.Checked = false;
            UsarGestaoCheckBox.Checked = false;
            UsarKmRodadoCheckBox.Checked = false;
            VisoesBIComboBox.SelectedIndex = -1;

            VisoesBIComboBox.SelectedIndex = -1;
            RubricasComboBox.DataSource = new List<DetalheVisoes>();
            OcorrenciasComboBox.DataSource = new List<OcorrenciasGlobus>();
            AreasComboBox.DataSource = new List<AreaGlobus>();
            FuncoesComboBox.DataSource = new List<FuncionariosGlobus>();
            gridGroupingControl1.DataSource = new List<MetasContasContabeis>();

            ExibirNoDRECheckBox.Checked = false;
            TotalizadorCheckBox.Checked = false;
            FormulaTotalizadorTextBox.Text = string.Empty;
            FormulaTextBox.Text = string.Empty;
            NivelTotalizadorCurrencyText.DecimalValue = 0;

            try
            {
                _listaMetas.Clear();
            }
            catch { }

            try
            {
                FuncoesComboBox.SelectedItems.Clear();
            }
            catch { }
            try
            {
                AreasComboBox.SelectedItems.Clear();
            }
            catch { }
            try
            {
                OcorrenciasComboBox.SelectedItems.Clear();
            }
            catch { }
            try
            {
                RubricasComboBox.SelectedItems.Clear();
            }
            catch { }


            codigoTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new MetasBO().Excluir(_metas))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
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

                codigoTextBox_Validating(sender, new CancelEventArgs());
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

        private void excluirButton_Enter(object sender, EventArgs e)
        {
            excluirButton.BackColor = Publicas._botaoFocado;
            excluirButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void tipoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
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

            if (_metas.Existe)
            {
                ativoCheckBox.Checked = _metas.Ativo;
                nomeTextBox.Text = _metas.Descricao;
                TextoBITextBox.Text = _metas.TextoBI;

                perspectivaComboBox.SelectedIndex = (_metas.Perspectiva == Publicas.Perspectivas.Cliente ? 0 :
                    (_metas.Perspectiva == Publicas.Perspectivas.Processos ? 1 :
                    (_metas.Perspectiva == Publicas.Perspectivas.Financeira ? 2 : 3)));

                _listaMetas = new MetasBO().ListarContasMetas(_metas.Id);

                gridGroupingControl1.DataSource = new List<MetasContasContabeis>();
                gridGroupingControl1.DataSource = _listaMetas;

                switch (_metas.Regra)
                {
                    case Publicas.RegraFormulaMetas.MaiorMelhor:
                        maiorMelhorButton_Click(sender, e);
                        break;
                    case Publicas.RegraFormulaMetas.MenorMelhor:
                        menorMelhorButton_Click(sender, e);
                        break;
                    case Publicas.RegraFormulaMetas.Igual:
                        igualMelhorButton_Click(sender, e);
                        break;
                }

                formula1RadioButton.Checked = formula1RadioButton.Text == _metas.Formula;
                formula2RadioButton.Checked = formula2RadioButton.Text == _metas.Formula;
                formula3RadioButton.Checked = formula3RadioButton.Text == _metas.Formula;

                UsaAvaliacaoCheckBox.Checked = _metas.UsaNaAvaliacao;
                UsarGestaoCheckBox.Checked = _metas.UsaNaGestao;
                DecimaisCurrencyTextBox.DecimalValue = _metas.QuantidadeDecimais;

                ExibirNoDRECheckBox.Checked = _metas.ExibeNoDRE;
                TotalizadorCheckBox.Checked = _metas.GrupoTotalizador;
                FormulaTotalizadorTextBox.Text = _metas.FormulaTotalizador;
                NivelTotalizadorCurrencyText.DecimalValue = _metas.NivelCalculo;


                if (_metas.IdBI != 0)
                {
                    foreach (var item in _listaBI.Where(w => w.Id == _metas.IdBI))
                    {
                        for (int i = 0; i < VisoesBIComboBox.Items.Count; i++)
                        {
                            VisoesBIComboBox.SelectedIndex = i;
                            if (VisoesBIComboBox.Text == item.Descricao)
                            {
                                VisoesBIComboBox_Validating(sender, new CancelEventArgs());
                                break;
                            }
                        }
                    }

                    _listaBIItens = new MetasBO().Listar(_metas.Id);
                    string teste = "";
                    foreach (var item in _listaBIItens)
                    {
                        if (item.Tipo == "R") // Rubrica
                        {
                            for (int i = 0; i < RubricasComboBox.Items.Count; i++)
                            {
                                teste = RubricasComboBox.GetItemText(RubricasComboBox.Items[i]);
                                if (RubricasComboBox.GetItemText(RubricasComboBox.Items[i]) == item.Descricao)
                                {
                                    RubricasComboBox.SelectedItems.Add(teste);
                                    RubricasComboBox.TextBox.Text =  teste + ";" + RubricasComboBox.TextBox.Text;
                                    RubricasComboBox_Validating(sender, new CancelEventArgs());
                                }
                            }
                        }

                        if (item.Tipo == "O") // Ocorrência
                        {
                            for (int i = 0; i < OcorrenciasComboBox.Items.Count; i++)
                            {
                                teste = OcorrenciasComboBox.GetItemText(OcorrenciasComboBox.Items[i]);
                                if (teste.Contains(item.Codigo.ToString()))
                                {
                                    OcorrenciasComboBox.SelectedItems.Add(teste);
                                    OcorrenciasComboBox.TextBox.Text = teste + ";" + OcorrenciasComboBox.TextBox.Text;
                                }
                            }
                        }

                        if (item.Tipo == "F") // Funçao
                        {
                            for (int i = 0; i < FuncoesComboBox.Items.Count; i++)
                            {
                                teste = FuncoesComboBox.GetItemText(FuncoesComboBox.Items[i]);
                                if (teste.Contains(item.Codigo.ToString()))
                                {
                                    FuncoesComboBox.SelectedItems.Add(teste);
                                    FuncoesComboBox.TextBox.Text = teste + ";" + FuncoesComboBox.TextBox.Text;
                                }
                            }
                        }

                        if (item.Tipo == "A") // Area
                        {
                            for (int i = 0; i < AreasComboBox.Items.Count; i++)
                            {
                                teste = AreasComboBox.GetItemText(AreasComboBox.Items[i]);
                                if (teste.Contains(item.Codigo.ToString()))
                                {
                                    AreasComboBox.SelectedItems.Add(teste);
                                    AreasComboBox.TextBox.Text = teste + ";" + AreasComboBox.TextBox.Text;
                                }
                            }
                        }
                    }
                    RubricasComboBox.Refresh();

                }
            }

            excluirButton.Enabled = _metas.Existe;
            gravarButton.Enabled = true;

            if (Publicas._idRetornoPesquisa != 0)
                ativoCheckBox.Focus();
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void tipoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void TextoBITextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void maiorMelhorButton_Click(object sender, EventArgs e)
        {
            _regraFormula = Publicas.RegraFormulaMetas.MaiorMelhor;
            maiorMelhorButton.BackColor = codigoTextBox.BackColor;
            menorMelhorButton.BackColor = Publicas._botao;
            igualMelhorButton.BackColor = Publicas._botao;
            formula2RadioButton.Checked = true; ;
        }

        private void menorMelhorButton_Click(object sender, EventArgs e)
        {
            _regraFormula = Publicas.RegraFormulaMetas.MenorMelhor;
            maiorMelhorButton.BackColor = Publicas._botao;
            menorMelhorButton.BackColor = codigoTextBox.BackColor; 
            igualMelhorButton.BackColor = Publicas._botao;
            formula1RadioButton.Checked = true;
        }

        private void igualMelhorButton_Click(object sender, EventArgs e)
        {
            _regraFormula = Publicas.RegraFormulaMetas.Igual;
            maiorMelhorButton.BackColor = Publicas._botao;
            menorMelhorButton.BackColor = Publicas._botao;
            igualMelhorButton.BackColor = codigoTextBox.BackColor;
            formula3RadioButton.Checked = true;
        }

        private void VisoesBIComboBox_Validating(object sender, CancelEventArgs e)
        {
            VisoesBIComboBox.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaBI.Where(w => w.Descricao == VisoesBIComboBox.Text))
            {
                _visoesBI = item;
            }

            _listaDetalhes = new VisoesBIBO().Listar(_visoesBI.Id);

            RubricasComboBox.DataSource = _listaDetalhes.OrderBy(o => o.Descricao).ToList();
            RubricasComboBox.DisplayMember = "Descricao";
            RubricasComboBox.SelectedIndex = -1;

            _listaOcorrencias = new OcorrenciasGlobusBO().Listar();
            _listaAreas = new OcorrenciasGlobusBO().ListarArea();
            _listaFuncoes = new OcorrenciasGlobusBO().ListarFuncoes();

            OcorrenciasComboBox.DataSource = new List<OcorrenciasGlobus>();
            AreasComboBox.DataSource = new List<AreaGlobus>();
            FuncoesComboBox.DataSource = new List<FuncionariosGlobus>();

            if (VisoesBIComboBox.Text.Contains("Avarias"))
            {
                OcorrenciasComboBox.DataSource = _listaOcorrencias.Where(w => !w.Descricao.Contains("ZZ.") && w.Descricao.ToUpper().Contains("AVARIA")).OrderBy(o => o.CodigoENome).ToList();
                OcorrenciasComboBox.DisplayMember = "CodigoENome";
                OcorrenciasComboBox.SelectedIndex = -1;
            }

            if (VisoesBIComboBox.Text.Contains("Acidentes"))
            {
                OcorrenciasComboBox.DataSource = _listaOcorrencias.Where(w => !w.Descricao.Contains("ZZ.") &&
                                                                              (w.Descricao.ToUpper().Contains("ACIDENTE") ||
                                                                              w.Descricao.ToUpper().Contains("ATROPEL") ||
                                                                              w.Descricao.ToUpper().Contains("AC.") ||
                                                                              w.Descricao.ToUpper().Contains("AC ")) ).OrderBy(o => o.CodigoENome).ToList();
                OcorrenciasComboBox.DisplayMember = "CodigoENome";
                OcorrenciasComboBox.SelectedIndex = -1;
            }

            if (VisoesBIComboBox.Text.Contains("Absenteísmo") || VisoesBIComboBox.Text.Contains("Turn"))
            {
                if (VisoesBIComboBox.Text.Contains("Absenteísmo"))
                {
                    OcorrenciasComboBox.DataSource = _listaOcorrencias.Where(w => !w.Descricao.Contains("ZZ.") &&
                                                                                  (w.Descricao.ToUpper().Contains("ATEST") ||
                                                                                  w.Descricao.ToUpper().Contains("DECL") ||
                                                                                  w.Descricao.ToUpper().Contains("DOAR") ||
                                                                                  w.Descricao.ToUpper().Contains("FALTA") ||
                                                                                  w.Descricao.ToUpper().Contains("SUSPENSAO") ||
                                                                                  w.Descricao.ToUpper().Contains("LIC"))).OrderBy(o => o.CodigoENome).ToList();
                    OcorrenciasComboBox.DisplayMember = "CodigoENome";
                    OcorrenciasComboBox.SelectedIndex = -1;
                }
                else
                {
                    FuncoesComboBox.DataSource = _listaFuncoes.Where(w => !w.Descricao.Contains("ZZ.") && (w.Descricao.Contains("LEGAL") || w.Descricao.Contains("APRENDIZ") || w.Descricao.Contains("JOVEM")))
                                                                .OrderBy(o => o.CodigoENome).ToList();
                    FuncoesComboBox.DisplayMember = "CodigoENome";
                    FuncoesComboBox.SelectedIndex = -1;
                }

                AreasComboBox.DataSource = _listaAreas.Where(w => !w.Descricao.Contains("ZZ."))
                                                            .OrderBy(o => o.CodigoENome).ToList();
                AreasComboBox.DisplayMember = "CodigoENome";
                AreasComboBox.SelectedIndex = -1;
            }

        }

        private void RubricasComboBox_Validating(object sender, CancelEventArgs e)
        {
            RubricasComboBox.MetroColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in RubricasComboBox.SelectedItems)
            {
                if (item.ToString().Contains("CNH"))
                {
                    FuncoesComboBox.DataSource = _listaFuncoes.Where(w => !w.Descricao.Contains("ZZ.") && w.Descricao.StartsWith("MOT"))
                                                                .OrderBy(o => o.CodigoENome).ToList();
                    FuncoesComboBox.DisplayMember = "CodigoENome";
                    FuncoesComboBox.SelectedIndex = -1;
                }

                if (item.ToString().Contains("Folha"))
                {
                    AreasComboBox.DataSource = _listaAreas.Where(w => !w.Descricao.Contains("ZZ."))
                                                                .OrderBy(o => o.CodigoENome).ToList();
                    AreasComboBox.DisplayMember = "CodigoENome";
                    AreasComboBox.SelectedIndex = -1;
                }
            }            
        }

        private void UsaAvaliacaoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                UsarGestaoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void UsarGestaoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ExibirNoDRECheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                UsaAvaliacaoCheckBox.Focus();
            }
        }

        private void maiorMelhorButton_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void menorMelhorButton_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void igualMelhorButton_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void formula1RadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void formula2RadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void formula3RadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
                
        private void VisoesBIComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                UsarKmRodadoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                formula3RadioButton.Focus();
            }
        }

        private void UsarKmRodadoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                RubricasComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                VisoesBIComboBox.Focus();
            }
        }

        private void RubricasComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OcorrenciasComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                UsarKmRodadoCheckBox.Focus();
            }
        }

        private void OcorrenciasComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                FuncoesComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                RubricasComboBox.Focus();
            }
        }

        private void FuncoesComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                AreasComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OcorrenciasComboBox.Focus();
            }
        }

        private void AreasComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FuncoesComboBox.Focus();
            }
        }

        private void quantidadeMesesCurrencyTextBox_TextChanged(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).FocusBorderColor = Publicas._bordaEntrada;
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void RubricasComboBox_Enter(object sender, EventArgs e)
        {
            ((MultiSelectionComboBox)sender).MetroColor = Publicas._bordaEntrada;
        }

        private void AreasComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((MultiSelectionComboBox)sender).MetroColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void RubricasComboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OcorrenciasComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                UsarKmRodadoCheckBox.Focus();
            }
        }

        private void RubricasComboBox_SelectedItemCollectionChanged(object sender, SelectedItemCollectionChangedArgs e)
        {
            
        }

        private void DecimaisCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                perspectivaComboBox.Focus();
            }
        }

        private void DecimaisCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                ((CurrencyTextBox)sender).FocusBorderColor = Publicas._bordaEntrada;
                ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;

            }
            catch { }
        }

        private void DecimaisCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ((CurrencyTextBox)sender).FocusBorderColor = Publicas._bordaSaida;
                ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            }
            catch { }

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PlanoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                if (TotalizadorCheckBox.Checked)
                    FormulaTotalizadorTextBox.Focus();
                else
                    TotalizadorCheckBox.Focus();
            }
        }

        private void PlanoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ContasTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void ContasTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SomarRadioButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PlanoTextBox.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                IncluirButton.Focus();
            }
        }

        private void SomarRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                IncluirButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ContasTextBox.Focus();
            }
        }

        private void SubtrairRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                IncluirButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SomarRadioButton.Focus();
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

            if (_listaEmpresasAutorizadas.Where(w => w.IdEmpresa == _empresa.IdEmpresa && w.EmpresaAutoriza).Count() == 0)
            {
                new Notificacoes.Mensagem("Usuário não autorizado para está empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }
        }

        private void PlanoTextBox_Validating(object sender, CancelEventArgs e)
        {
            PlanoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (PlanoTextBox.Text.Trim() == "") 
            {
                new Pesquisas.PlanoContabil().ShowDialog();

                PlanoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (PlanoTextBox.Text.Trim() == "" || PlanoTextBox.Text == "0")
                {
                    PlanoTextBox.Text = string.Empty;
                    PlanoTextBox.Focus();
                    return;
                }
            }

            _plano = new RateioBeneficioBO().Consultar(Convert.ToInt32(PlanoTextBox.Text));

            if (!_plano.Existe)
            {
                new Notificacoes.Mensagem("Plano contábil não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                PlanoTextBox.Focus();
                return;
            }

            NomePlanoTextBox.Text = _plano.Nome;
        }

        private void ContasTextBox_Validating(object sender, CancelEventArgs e)
        {
            ContasTextBox.BorderColor = Publicas._bordaSaida;

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

            Publicas._idRetornoPesquisa = 0;

            if (ContasTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _plano.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                ContasTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (ContasTextBox.Text.Trim() == "" || ContasTextBox.Text == "0")
                {
                    ContasTextBox.Text = string.Empty;
                    ContasTextBox.Focus();
                    return;
                }
            }

            _contas = new RateioBeneficioBO().Consultar(_plano.NumeroPlano, Convert.ToInt32(ContasTextBox.Text));

            if (!_contas.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContasTextBox.Focus();
                return;
            }

            //if (!_contas.AceitaLancamento)
            //{
            //    new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
            //    ContasTextBox.Focus();
            //    return;
            //}

            NomeContaTextBox.Text = _contas.Classificador + " " + _contas.Nome;
        }

        private void IncluirButton_Click(object sender, EventArgs e)
        {
            if (_listaMetas == null)
                _listaMetas = new List<MetasContasContabeis>();

            if (string.IsNullOrEmpty(ContasTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Conta Contábil não informada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContasTextBox.Focus();
                return;
            }

            if (_listaMetas.Where(w => w.Conta == _contas.Codigo && w.IdEmpresa == _empresa.IdEmpresa && w.Plano == _plano.NumeroPlano).Count() == 0)
            {
                _listaMetas.Add(new MetasContasContabeis()
                {
                    IdMetas = _metas.Id,
                    IdEmpresa = _empresa.IdEmpresa,
                    Nome = _contas.Classificador + " " + _contas.Nome,
                    Conta = _contas.Codigo,
                    Plano = _plano.NumeroPlano,
                    Tipo = SomarRadioButton.Checked ? "+" : "-",
                    Empresa = _empresa.CodigoEmpresaGlobus + " - " + _empresa.NomeAbreviado,
                    Formula = FormulaTextBox.Text.Trim()
                });
            }

            gridGroupingControl1.DataSource = new List<MetasContasContabeis>();
            gridGroupingControl1.DataSource = _listaMetas;

            ContasTextBox.Text = string.Empty;
            NomeContaTextBox.Text = string.Empty;
            ContasTextBox.Focus();
        }

        private void CopiarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlanoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Selecione a empresa e o Plano que deseja copiar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                PlanoTextBox.Focus();
                return;
            }

            GestaoEstrategica.CopiaContasContabeisDaMeta _tela = new GestaoEstrategica.CopiaContasContabeisDaMeta();
            _tela._metas = _metas;

            _tela.ShowDialog();

            if (_tela._listaMetas != null)
            {
                _tela._listaMetas.ForEach(u =>
                {
                    u.Existe = false;
                    u.IdMetas = _metas.Id;
                    u.IdEmpresa = _empresa.IdEmpresa;
                    u.Empresa = _empresa.CodigoEmpresaGlobus + " - " + _empresa.NomeAbreviado;
                });

                foreach (var item in _tela._listaMetas.Where(w => w.Marcado))
                {
                    if (_listaMetas.Where(w => w.Conta == item.Conta &&
                                               w.Plano == item.Plano &&
                                               w.IdEmpresa == item.IdEmpresa).Count() == 0)
                        _listaMetas.Add(item);
                }

                gridGroupingControl1.DataSource = new List<MetasContasContabeis>();
                gridGroupingControl1.DataSource = _listaMetas;
            }
        }

        private void excluirContaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (new Notificacoes.Mensagem("Confirma a exclusão ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.MetasContasContabeis _excluirTipos = new Classes.MetasContasContabeis();

                    gridGroupingControl1.DataSource = new List<Classes.MetasContasContabeis>();

                    int conta = 0;
                    int plano = 0;
                    int idempresa = 0;
                    int posIniId = 0;
                    int posFimId = 0;
                    
                    try
                    {
                        conta = (int)dr["Conta"];
                    }
                    catch
                    {
                        posIniId = dr.Info.IndexOf("Conta =") + 7;
                        posFimId = dr.Info.IndexOf(", Nome");
                        conta = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                    }


                    try
                    {
                        plano = (int)dr["Plano"];
                    }
                    catch
                    {
                        posIniId = dr.Info.IndexOf("Plano =") + 7;
                        posFimId = dr.Info.IndexOf(", Tipo");
                        plano = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                    }

                    try
                    {
                        idempresa = (int)dr["IdEmpresa"];
                    }
                    catch
                    {
                        posIniId = dr.Info.IndexOf("IdEmpresa =") + 11;
                        posFimId = dr.Info.IndexOf(", Empresa");
                        idempresa = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                    }
                    
                    foreach (var item in _listaMetas.Where(w => w.Conta == conta &&
                                                                w.Plano == plano &&
                                                                w.IdEmpresa == idempresa))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new MetasBO().ExcluiConta(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _listaMetas.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu a conta contábil " + _excluirTipos.Conta +
                        " do plano " + _excluirTipos.Plano +
                        " da empresa " + _excluirTipos.Empresa;
                    _log.Tela = "Recursos Humanos - Cadastros - Metas";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _listaMetas;
            }
        }

        private void TotalizadorCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            PesquisaMetasFormulaButton.Enabled = TotalizadorCheckBox.Checked;
            FormulaTotalizadorTextBox.Enabled = TotalizadorCheckBox.Checked;
            NivelTotalizadorCurrencyText.Enabled = TotalizadorCheckBox.Checked;
            label4.Enabled = TotalizadorCheckBox.Checked;
            ContasPanel.Enabled = !TotalizadorCheckBox.Checked;
        }

        private void DesoneracaoCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            FormulaTextBox.Enabled = DesoneracaoCheckBox.Checked;
            label8.Enabled = DesoneracaoCheckBox.Checked;
        }

        private void PesquisaMetasFormulaButton_Click(object sender, EventArgs e)
        {
            new Pesquisas.Metas().ShowDialog();


            if (Publicas._idRetornoPesquisa == 0)
            {
                FormulaTotalizadorTextBox.Focus();
                return;
            }

            FormulaTotalizadorTextBox.Text = FormulaTotalizadorTextBox.Text + " [meta " + Publicas._idRetornoPesquisa + "] ";
        }

        private void ExibirNoDRECheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                TotalizadorCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                UsarGestaoCheckBox.Focus();
            }
        }

        private void TotalizadorCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (TotalizadorCheckBox.Checked)
                    NivelTotalizadorCurrencyText.Focus();
                else
                    empresaComboBoxAdv.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ExibirNoDRECheckBox.Focus();
            }
        }

        private void DesoneracaoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (DesoneracaoCheckBox.Checked)
                    FormulaTextBox.Focus();
                else
                    IncluirButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ContasTextBox.Focus();
            }
        }

        private void NivelTotalizadorCurrencyText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                FormulaTotalizadorTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                TotalizadorCheckBox.Focus();
            }
        }

        private void FormulaTotalizadorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                NivelTotalizadorCurrencyText.Focus();
            }
        }

        private void NivelTotalizadorCurrencyText_Validating(object sender, CancelEventArgs e)
        {
            NivelTotalizadorCurrencyText.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void FormulaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                IncluirButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DesoneracaoCheckBox.Focus();
            }
        }

        private void FormulaTextBox_Validating(object sender, CancelEventArgs e)
        {
            FormulaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void FormulaTotalizadorTextBox_Validating(object sender, CancelEventArgs e)
        {
            FormulaTotalizadorTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void PesquisaPlanoButton_Click(object sender, EventArgs e)
        {
            new Pesquisas.PlanoContabil().ShowDialog();

            PlanoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

            if (PlanoTextBox.Text.Trim() == "" || PlanoTextBox.Text == "0")
            {
                PlanoTextBox.Text = string.Empty;
                PlanoTextBox.Focus();
                return;
            }
        }

        private void PesquisaCustoButton_Click(object sender, EventArgs e)
        {
            Publicas._idRetornoPesquisa = _plano.NumeroPlano;
            new Pesquisas.ContaContabil().ShowDialog();

            ContasTextBox.Text = Publicas._idRetornoPesquisa.ToString();

            if (ContasTextBox.Text.Trim() == "" || ContasTextBox.Text == "0")
            {
                ContasTextBox.Text = string.Empty;
                ContasTextBox.Focus();
                return;
            }
        }
    }
}
