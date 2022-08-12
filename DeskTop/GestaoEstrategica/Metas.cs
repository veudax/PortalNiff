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

namespace Suportte.GestaoEstrategica
{
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

                    gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl1.BackColor = Publicas._panelTitulo;

                    RubricaGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    RubricaGrid.ColorStyles = ColorStyles.Office2010Black;
                    RubricaGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    RubricaGrid.BackColor = Publicas._panelTitulo;

                    FuncoesGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    FuncoesGrid.ColorStyles = ColorStyles.Office2010Black;
                    FuncoesGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    FuncoesGrid.BackColor = Publicas._panelTitulo;

                    AreasGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    AreasGrid.ColorStyles = ColorStyles.Office2010Black;
                    AreasGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    AreasGrid.BackColor = Publicas._panelTitulo;

                    OcorrenciaGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    OcorrenciaGrid.ColorStyles = ColorStyles.Office2010Black;
                    OcorrenciaGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    OcorrenciaGrid.BackColor = Publicas._panelTitulo;
                    
                    DecimaisCurrencyTextBox.PositiveColor = Publicas._fonte;
                    NivelTotalizadorCurrencyText.PositiveColor = Publicas._fonte;
                    DecimaisCurrencyTextBox.ZeroColor = Publicas._fonte;
                    NivelTotalizadorCurrencyText.ZeroColor = Publicas._fonte;
                }

                VisoesBIComboBox.BackColor = empresaComboBoxAdv.BackColor;
                VisoesBIComboBox.ForeColor = empresaComboBoxAdv.ForeColor;

                TituloBIPanel.BackColor = tituloPanel.BackColor;
                TituloBILabel.ForeColor = tituloPanel.ForeColor;
                TituloContasPanel.BackColor = tituloPanel.BackColor;
                TituloContasPanel.ForeColor = tituloPanel.ForeColor;
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Metas _metas;
        Classes.VisoesBI _visoesBI;
        List<Classes.VisoesBI> _listaBI;
        List<Classes.DetalheVisoes> _listaDetalhes = new List<DetalheVisoes>();
        List<Classes.OcorrenciasGlobus> _listaOcorrencias = new List<OcorrenciasGlobus>();
        List<Classes.AreaGlobus> _listaAreas = new List<AreaGlobus>();
        List<Classes.FuncoesGlobus> _listaFuncoes = new List<FuncoesGlobus>();
        List<Classes.OcorrenciasGlobus> _listaOcorrenciasFiltradas = new List<OcorrenciasGlobus>();
        List<Classes.AreaGlobus> _listaAreasFiltradas = new List<AreaGlobus>();
        List<Classes.FuncoesGlobus> _listaFuncoesFiltradas = new List<FuncoesGlobus>();

        List<Classes.MetasBIItens> _listaBIItens;
        List<Classes.MetasBIItens> _listaBIItensDesmarcados;
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

            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(gridGroupingControl1);
            filter.WireGrid(RubricaGrid);
            filter.WireGrid(OcorrenciaGrid);
            filter.WireGrid(FuncoesGrid);
            filter.WireGrid(AreasGrid);

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

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl1.Table.DefaultRecordRowHeight = 22;

            AreasGrid.SortIconPlacement = SortIconPlacement.Left;
            AreasGrid.TopLevelGroupOptions.ShowFilterBar = true;
            AreasGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            AreasGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < AreasGrid.TableDescriptor.Columns.Count; i++)
            {
                AreasGrid.TableDescriptor.Columns[i].AllowFilter = true;
                AreasGrid.TableDescriptor.Columns[i].ReadOnly = false;
                AreasGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                AreasGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                AreasGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.AreasGrid.SetMetroStyle(metroColor);
                this.AreasGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.AreasGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.AreasGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.AreasGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.AreasGrid.Table.DefaultRecordRowHeight = 22;

            FuncoesGrid.SortIconPlacement = SortIconPlacement.Left;
            FuncoesGrid.TopLevelGroupOptions.ShowFilterBar = true;
            FuncoesGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            FuncoesGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < FuncoesGrid.TableDescriptor.Columns.Count; i++)
            {
                FuncoesGrid.TableDescriptor.Columns[i].AllowFilter = true;
                FuncoesGrid.TableDescriptor.Columns[i].ReadOnly = false;
                FuncoesGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                FuncoesGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                FuncoesGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.FuncoesGrid.SetMetroStyle(metroColor);
                this.FuncoesGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.FuncoesGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.FuncoesGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.FuncoesGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.FuncoesGrid.Table.DefaultRecordRowHeight = 22;

            OcorrenciaGrid.SortIconPlacement = SortIconPlacement.Left;
            OcorrenciaGrid.TopLevelGroupOptions.ShowFilterBar = true;
            OcorrenciaGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            OcorrenciaGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < OcorrenciaGrid.TableDescriptor.Columns.Count; i++)
            {
                OcorrenciaGrid.TableDescriptor.Columns[i].AllowFilter = true;
                OcorrenciaGrid.TableDescriptor.Columns[i].ReadOnly = false;
                OcorrenciaGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                OcorrenciaGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                OcorrenciaGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.OcorrenciaGrid.SetMetroStyle(metroColor);
                this.OcorrenciaGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.OcorrenciaGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.OcorrenciaGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.OcorrenciaGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.OcorrenciaGrid.Table.DefaultRecordRowHeight = 22;

            RubricaGrid.SortIconPlacement = SortIconPlacement.Left;
            RubricaGrid.TopLevelGroupOptions.ShowFilterBar = true;
            RubricaGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            RubricaGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < RubricaGrid.TableDescriptor.Columns.Count; i++)
            {
                RubricaGrid.TableDescriptor.Columns[i].AllowFilter = true;
                RubricaGrid.TableDescriptor.Columns[i].ReadOnly = false;
                RubricaGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                RubricaGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                RubricaGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.RubricaGrid.SetMetroStyle(metroColor);
                this.RubricaGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.RubricaGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.RubricaGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.RubricaGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.RubricaGrid.Table.DefaultRecordRowHeight = 22;
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
            //_metas.TextoBI = TextoBITextBox.Text;

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
            //_metas.UsaKMRodado = UsarKmRodadoCheckBox.Checked;
            _metas.PrevistoPermiteAlterar = true;
            _metas.RealizadoPermiteAlterar = true;

            if (TipoDeFrotaTextBox.Text != "" && TipoDeFrotaTextBox.Text.Contains(", "))
                _metas.TipoDeFrota = TipoDeFrotaTextBox.Text.Substring(0, TipoDeFrotaTextBox.TextLength -2);

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

            _listaBIItensDesmarcados = new List<MetasBIItens>();

            foreach (var item in _listaDetalhes.Where(w => !w.Selecionado && w.Existe))
            {
                MetasBIItens _metasB = new MetasBIItens();
                _metasB.IdMetas = _metas.Id;
                _metasB.Tipo = "R";
                _metasB.Descricao = item.ToString();
                _metasB.Existe = item.Existe;
                _metasB.Id = item.Id;
                _listaBIItensDesmarcados.Add(_metasB);
            }

            foreach (var item in _listaDetalhes.Where(w => w.Selecionado))
            {
                MetasBIItens _metasB = new MetasBIItens();
                _metasB.IdMetas = _metas.Id;
                _metasB.Tipo = "R";
                _metasB.Descricao = item.ToString();
                _metasB.Existe = item.Existe;

                _listaBIItens.Add(_metasB);
            }

            foreach (var item in _listaOcorrenciasFiltradas.Where(w => w.Selecionado))
            {
                MetasBIItens _metasB = new MetasBIItens();
                _metasB.IdMetas = _metas.Id;
                _metasB.Tipo = "O";
                _metasB.Descricao = item.ToString();
                _metasB.Codigo = item.Codigo;
                _metasB.Existe = item.Existe;

                _listaBIItens.Add(_metasB);
            }

            foreach (var item in _listaOcorrenciasFiltradas.Where(w => !w.Selecionado && w.Existe))
            {
                MetasBIItens _metasB = new MetasBIItens();
                _metasB.IdMetas = _metas.Id;
                _metasB.Tipo = "O";
                _metasB.Descricao = item.ToString();
                _metasB.Codigo = item.Codigo;
                _metasB.Existe = item.Existe;
                _metasB.Id = item.Id;

                _listaBIItensDesmarcados.Add(_metasB);
            }
            
            foreach (var item in _listaFuncoesFiltradas.Where(w => w.Selecionado))
            {
                MetasBIItens _metasB = new MetasBIItens();
                _metasB.IdMetas = _metas.Id;
                _metasB.Tipo = "F";
                _metasB.Descricao = item.ToString();
                _metasB.Codigo = item.Codigo;
                _metasB.Existe = item.Existe;

                _listaBIItens.Add(_metasB);
            }

            foreach (var item in _listaFuncoesFiltradas.Where(w => !w.Selecionado && w.Existe))
            {
                MetasBIItens _metasB = new MetasBIItens();
                _metasB.IdMetas = _metas.Id;
                _metasB.Tipo = "F";
                _metasB.Descricao = item.ToString();
                _metasB.Codigo = item.Codigo;
                _metasB.Existe = item.Existe;
                _metasB.Id = item.Id;

                _listaBIItensDesmarcados.Add(_metasB);
            }

            foreach (var item in _listaAreasFiltradas.Where(w => w.Selecionado))
            {
                MetasBIItens _metasB = new MetasBIItens();
                _metasB.IdMetas = _metas.Id;
                _metasB.Tipo = "A";
                _metasB.Descricao = item.ToString();
                _metasB.Codigo = item.Codigo;
                _metasB.Existe = item.Existe;

                _listaBIItens.Add(_metasB);
            }

            foreach (var item in _listaAreasFiltradas.Where(w => !w.Selecionado && w.Existe))
            {
                MetasBIItens _metasB = new MetasBIItens();
                _metasB.IdMetas = _metas.Id;
                _metasB.Tipo = "A";
                _metasB.Descricao = item.ToString();
                _metasB.Codigo = item.Codigo;
                _metasB.Existe = item.Existe;
                _metasB.Id = item.Id;

                _listaBIItensDesmarcados.Add(_metasB);
            }

            if (!new MetasBO().Gravar(_metas, _listaBIItens, _listaMetas, _listaBIItensDesmarcados))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            ativoCheckBox.Checked = false;
            perspectivaComboBox.SelectedIndex = -1;
            perspectivaComboBox.Text = string.Empty;
            //TextoBITextBox.Text = string.Empty;
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

            DecimaisCurrencyTextBox.DecimalValue = 2;
            UsaAvaliacaoCheckBox.Checked = false;
            UsarGestaoCheckBox.Checked = false;
            //UsarKmRodadoCheckBox.Checked = false;
            VisoesBIComboBox.SelectedIndex = -1;

            VisoesBIComboBox.SelectedIndex = -1;
            RubricaGrid.DataSource = new List<DetalheVisoes>();
            OcorrenciaGrid.DataSource = new List<OcorrenciasGlobus>();
            AreasGrid.DataSource = new List<AreaGlobus>();
            FuncoesGrid.DataSource = new List<FuncoesGlobus>();
            gridGroupingControl1.DataSource = new List<MetasContasContabeis>();

            ExibirNoDRECheckBox.Checked = false;
            TotalizadorCheckBox.Checked = false;
            FormulaTotalizadorTextBox.Text = string.Empty;
            FormulaTextBox.Text = string.Empty;
            NivelTotalizadorCurrencyText.DecimalValue = 0;
            TipoDeFrotaTextBox.Text = string.Empty;

            try
            {
                _listaMetas.Clear();
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
        
        private void perspectivaComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
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

                //formula1RadioButton.Checked = formula1RadioButton.Text == _metas.Formula;
                //formula2RadioButton.Checked = formula2RadioButton.Text == _metas.Formula;
                //formula3RadioButton.Checked = formula3RadioButton.Text == _metas.Formula;

                UsaAvaliacaoCheckBox.Checked = _metas.UsaNaAvaliacao;
                UsarGestaoCheckBox.Checked = _metas.UsaNaGestao;
                DecimaisCurrencyTextBox.DecimalValue = _metas.QuantidadeDecimais;

                ExibirNoDRECheckBox.Checked = _metas.ExibeNoDRE;
                TotalizadorCheckBox.Checked = _metas.GrupoTotalizador;
                FormulaTotalizadorTextBox.Text = _metas.FormulaTotalizador;
                NivelTotalizadorCurrencyText.DecimalValue = _metas.NivelCalculo;
                TipoDeFrotaTextBox.Text = _metas.TipoDeFrota;

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
                    
                    foreach (var item in _listaBIItens)
                    {
                        if (item.Tipo == "R") // Rubrica
                        {
                            foreach (var itemD in _listaDetalhes)
                            {
                                if (itemD.Descricao.Contains(item.Descricao))
                                {
                                    itemD.Selecionado = true;
                                    itemD.Existe = true;
                                    itemD.Id = item.Id;
                                }
                            }
                        }

                        if (item.Tipo == "O") // Ocorrência
                        {
                            foreach (var itemD in _listaOcorrenciasFiltradas)
                            {
                                if (itemD.Codigo == item.Codigo)
                                {
                                    itemD.Selecionado = true;
                                    itemD.Existe = true;
                                    itemD.Id = item.Id;
                                }
                            }

                            OcorrenciaGrid.DataSource = new List<OcorrenciasGlobus>();
                            OcorrenciaGrid.DataSource = _listaOcorrenciasFiltradas;

                        }

                        if (item.Tipo == "F") // Funçao
                        {
                            foreach (var itemD in _listaFuncoesFiltradas)
                            {
                                if (itemD.Codigo == item.Codigo)
                                {
                                    itemD.Selecionado = true;
                                    itemD.Existe = true;
                                    itemD.Id = item.Id;
                                }
                            }

                            FuncoesGrid.DataSource = new List<FuncoesGlobus>();
                            FuncoesGrid.DataSource = _listaFuncoesFiltradas;

                        }

                        if (item.Tipo == "A") // Area
                        {
                            foreach (var itemD in _listaAreasFiltradas)
                            {
                                if (itemD.Codigo == item.Codigo)
                                {
                                    itemD.Selecionado = true;
                                    itemD.Existe = true;
                                    itemD.Id = item.Id;
                                }
                            }

                            AreasGrid.DataSource = new List<AreaGlobus>();
                            AreasGrid.DataSource = _listaAreasFiltradas;
                            
                        }
                    }
                    
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
            maiorMelhorButton.ForeColor = codigoTextBox.ForeColor;
            menorMelhorButton.BackColor = Publicas._botao;
            igualMelhorButton.BackColor = Publicas._botao;
            formula2RadioButton.Checked = true; 
        }

        private void menorMelhorButton_Click(object sender, EventArgs e)
        {
            _regraFormula = Publicas.RegraFormulaMetas.MenorMelhor;
            maiorMelhorButton.BackColor = Publicas._botao;
            menorMelhorButton.BackColor = codigoTextBox.BackColor;
            menorMelhorButton.ForeColor = codigoTextBox.ForeColor;
            igualMelhorButton.BackColor = Publicas._botao;
            formula1RadioButton.Checked = true;
        }

        private void igualMelhorButton_Click(object sender, EventArgs e)
        {
            _regraFormula = Publicas.RegraFormulaMetas.Igual;
            maiorMelhorButton.BackColor = Publicas._botao;
            menorMelhorButton.BackColor = Publicas._botao;
            igualMelhorButton.BackColor = codigoTextBox.BackColor;
            igualMelhorButton.ForeColor = codigoTextBox.ForeColor;
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

            RubricaGrid.DataSource = _listaDetalhes.OrderBy(o => o.Descricao).ToList();
            //RubricasComboBox.DisplayMember = "Descricao";
            //RubricasComboBox.SelectedIndex = -1;

            _listaOcorrencias = new OcorrenciasGlobusBO().Listar();
            _listaAreas = new OcorrenciasGlobusBO().ListarArea();
            _listaFuncoes = new OcorrenciasGlobusBO().ListarFuncoes();

            _listaOcorrenciasFiltradas = new List<OcorrenciasGlobus>();
            _listaAreasFiltradas = new List<AreaGlobus>();
            _listaFuncoesFiltradas = new List<FuncoesGlobus>();

            OcorrenciaGrid.DataSource = new List<OcorrenciasGlobus>();
            AreasGrid.DataSource = new List<AreaGlobus>();
            FuncoesGrid.DataSource = new List<FuncoesGlobus>();

            if (VisoesBIComboBox.Text.Contains("Avarias"))
            {
                _listaOcorrenciasFiltradas = _listaOcorrencias.Where(w => !w.Descricao.Contains("ZZ.") && w.Descricao.ToUpper().Contains("AVARIA")).OrderBy(o => o.CodigoENome).ToList();
                OcorrenciaGrid.DataSource = _listaOcorrenciasFiltradas;

            }

            if (VisoesBIComboBox.Text.Contains("Acidentes"))
            {

                _listaOcorrenciasFiltradas = _listaOcorrencias.Where(w => !w.Descricao.Contains("ZZ.") &&
                                                                              (w.Descricao.ToUpper().Contains("ACIDENTE") ||
                                                                              w.Descricao.ToUpper().Contains("ATROPEL") ||
                                                                              w.Descricao.ToUpper().Contains("AC.") ||
                                                                              w.Descricao.ToUpper().Contains("AC "))).OrderBy(o => o.CodigoENome).ToList();

                OcorrenciaGrid.DataSource = _listaOcorrenciasFiltradas;
            }

            if (VisoesBIComboBox.Text.Contains("Absenteísmo") || VisoesBIComboBox.Text.Contains("Turn"))
            {
                if (VisoesBIComboBox.Text.Contains("Absenteísmo"))
                {
                    _listaOcorrenciasFiltradas = _listaOcorrencias.Where(w => !w.Descricao.Contains("ZZ.") &&
                                                                                  (w.Descricao.ToUpper().Contains("ATEST") ||
                                                                                  w.Descricao.ToUpper().Contains("DECL") ||
                                                                                  w.Descricao.ToUpper().Contains("DOAR") ||
                                                                                  w.Descricao.ToUpper().Contains("FALTA") ||
                                                                                  w.Descricao.ToUpper().Contains("SUSPENSAO") ||
                                                                                  w.Descricao.ToUpper().Contains("AMAMENTA") ||
                                                                                  w.Descricao.ToUpper().Contains("LIC"))).OrderBy(o => o.CodigoENome).ToList();

                    OcorrenciaGrid.DataSource = _listaOcorrenciasFiltradas;
                    
                }
                else
                {
                    _listaFuncoesFiltradas = _listaFuncoes.Where(w => !w.Descricao.Contains("ZZ.") && (w.Descricao.Contains("LEGAL") || w.Descricao.Contains("APRENDIZ") || w.Descricao.Contains("JOVEM")))
                                                               .OrderBy(o => o.CodigoENome).ToList();

                    FuncoesGrid.DataSource = _listaFuncoesFiltradas;


                }

                _listaAreasFiltradas = _listaAreas.Where(w => !w.Descricao.Contains("ZZ."))
                                                            .OrderBy(o => o.CodigoENome).ToList();

                AreasGrid.DataSource = _listaAreasFiltradas;
            }

        }

        private void RubricasComboBox_Validating(object sender, CancelEventArgs e)
        {
            ////RubricasComboBox.MetroColor = Publicas._bordaSaida;

            //if (Publicas._escTeclado)
            //{
            //    Publicas._escTeclado = false;
            //    return;
            //}

            //foreach (var item in RubricasComboBox.SelectedItems)
            //{
            //    if (item.ToString().Contains("CNH"))
            //    {
            //        FuncoesComboBox.DataSource = _listaFuncoes.Where(w => !w.Descricao.Contains("ZZ.") && w.Descricao.StartsWith("MOT"))
            //                                                    .OrderBy(o => o.CodigoENome).ToList();
            //        FuncoesComboBox.DisplayMember = "CodigoENome";
            //        FuncoesComboBox.SelectedIndex = -1;
            //    }

            //    if (item.ToString().Contains("Folha"))
            //    {
            //        AreasComboBox.DataSource = _listaAreas.Where(w => !w.Descricao.Contains("ZZ."))
            //                                                    .OrderBy(o => o.CodigoENome).ToList();
            //        AreasComboBox.DisplayMember = "CodigoENome";
            //        AreasComboBox.SelectedIndex = -1;
            //    }
            //}
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

            empresaComboBoxAdv_Validating(sender, new CancelEventArgs());

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
                    Formula = FormulaTextBox.Text
                });
            }

            gridGroupingControl1.DataSource = new List<MetasContasContabeis>();
            gridGroupingControl1.DataSource = _listaMetas;

            ContasTextBox.Text = string.Empty;
            NomeContaTextBox.Text = string.Empty;
            FormulaTextBox.Text = string.Empty;
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
                        " da empresa " + _excluirTipos.Empresa +
                        " meta " + codigoTextBox.Text;
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
            IncluirButton.Enabled = !TotalizadorCheckBox.Checked;
            CopiarButton.Enabled = !TotalizadorCheckBox.Checked;
        }

        private void DesoneracaoCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            FormulaTextBox.Enabled = DesoneracaoCheckBox.Checked;
            label11.Enabled = DesoneracaoCheckBox.Checked;
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

       
        private void NivelTotalizadorCurrencyText_Validating(object sender, CancelEventArgs e)
        {
            NivelTotalizadorCurrencyText.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
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

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
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
        }

        private void PesquisaTipoFrotaButton_Click(object sender, EventArgs e)
        {
            new Pesquisas.TipoDeFrota().ShowDialog();


            if (Publicas._idRetornoPesquisa == 0)
            {
                TipoDeFrotaTextBox.Focus();
                return;
            }

            TipoDeFrotaTextBox.Text = TipoDeFrotaTextBox.Text + Publicas._idRetornoPesquisa + ", ";
        }
    }
}
