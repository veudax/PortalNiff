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

namespace Suportte.Cadastros
{
    public partial class Cargos : Form
    {
        public Cargos()
        {
            InitializeComponent();

            maiorSalarioCurrencyTextBox.BackGroundColor = codigoTextBox.BackColor;
            menorSalarioCurrencyTextBox.BackGroundColor = codigoTextBox.BackColor;
            competenciasGridGroupingControl.BackColor = codigoTextBox.BackColor;
            tecnicasGridGroupingControl.BackColor = codigoTextBox.BackColor;

            tipoComboBox.Items.AddRange(new object[] { "Direção", "Gerência", "Coordenação", "Outros" });

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                if (Publicas._TemaBlack)
                {
                    maiorSalarioCurrencyTextBox.PositiveColor = Publicas._fonte;
                    menorSalarioCurrencyTextBox.PositiveColor = Publicas._fonte;

                    metasCrescimentoGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    metasCrescimentoGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    metasCrescimentoGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    metasCrescimentoGridGroupingControl.BackColor = Publicas._panelTitulo;

                    metasResultadoGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    metasResultadoGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    metasResultadoGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    metasResultadoGridGroupingControl.BackColor = Publicas._panelTitulo;

                    tecnicasGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    tecnicasGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    tecnicasGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    tecnicasGridGroupingControl.BackColor = Publicas._panelTitulo;

                    competenciasGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    competenciasGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    competenciasGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    competenciasGridGroupingControl.BackColor = Publicas._panelTitulo;

                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Cargos _cargos;
        Classes.Escolaridade _escolaridade;
        List<Classes.CompetenciasDoCargo> _listaCompetencias;
        List<Classes.CompetenciasDoCargo> _listaTecnicas;

        List<Classes.MetasDoCargo> _listaMetasResultado;
        List<Classes.MetasDoCargo> _listaMetasCrescimento;

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

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o nome da empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(escolaridadeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a escolaridade minima.", Publicas.TipoMensagem.Alerta).ShowDialog();
                escolaridadeTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tipoComboBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Selecione o tipo do cargo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tipoComboBox.Focus();
                return;
            }

            _cargos.Id = Convert.ToInt32(codigoTextBox.Text);
            _cargos.Descricao = nomeTextBox.Text;
            _cargos.Ativo = ativoCheckBox.Checked;
            _cargos.RequerExperiencia = requerExperienciasCheckBox.Checked;
            _cargos.IdEscolaridade = Convert.ToInt32(escolaridadeTextBox.Text);

            _cargos.SalarioMaximo = maiorSalarioCurrencyTextBox.DecimalValue;
            _cargos.SalarioMinimo = menorSalarioCurrencyTextBox.DecimalValue;
            _cargos.TipoDoCargo = tipoComboBox.Text.Substring(0, 1);

            _listaCompetencias.ForEach(u => u.IdCargo = _cargos.Id);
            _listaTecnicas.ForEach(u => u.IdCargo = _cargos.Id);
            _listaMetasResultado.ForEach(u => u.IdCargo = _cargos.Id);
            _listaMetasCrescimento.ForEach(u => u.IdCargo = _cargos.Id);

            List<CompetenciasDoCargo> _competencias = new List<CompetenciasDoCargo>();
            List<MetasDoCargo> _metas = new List<MetasDoCargo>();

            if (_listaCompetencias.Where(w => w.Marcado).Count() != 0)
                _competencias.AddRange(_listaCompetencias.Where(w => w.Marcado).ToList());

            if (_listaTecnicas.Where(w => w.Marcado).Count() != 0)
                _competencias.AddRange(_listaTecnicas.Where(w => w.Marcado).ToList());

            if (_listaMetasCrescimento.Where(w => w.Marcado).Count() != 0)
            {
                _metas.AddRange(_listaMetasCrescimento.Where(w => w.Marcado).ToList());
            }

            if (_listaMetasResultado.Where(w => w.Marcado).Count() != 0)
            {
                if (_listaMetasResultado.Where(w => w.Marcado).Sum(s => s.Peso) > 100)
                {
                    new Notificacoes.Mensagem("Total do peso superior a 100%." + Environment.NewLine, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
                else
                {
                    if (_listaMetasResultado.Where(w => w.Marcado).Sum(s => s.Peso) < 100)
                        new Notificacoes.Mensagem("Total do peso inferior a 100%." + Environment.NewLine, Publicas.TipoMensagem.Alerta).ShowDialog();
                }
                
                _metas.AddRange(_listaMetasResultado.Where(w => w.Marcado).ToList());
            }

            if (!new CargosBO().Gravar(_cargos, _competencias, _metas))
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

            if (!new CargosBO().Excluir(_cargos))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            escolaridadeTextBox.Text = string.Empty;
            descricaoEscolaridadeTextBox.Text = string.Empty;
            menorSalarioCurrencyTextBox.DecimalValue = 0;
            maiorSalarioCurrencyTextBox.DecimalValue = 0;

            competenciasGridGroupingControl.DataSource = new List<CompetenciasDoCargo>();
            tecnicasGridGroupingControl.DataSource = new List<CompetenciasDoCargo>();

            metasCrescimentoGridGroupingControl.DataSource = new List<MetasDoCargo>();
            metasResultadoGridGroupingControl.DataSource = new List<MetasDoCargo>();

            _listaCompetencias.Clear();
            _listaMetasCrescimento.Clear();
            _listaMetasResultado.Clear();
            _listaTecnicas.Clear();

            codigoTextBox.Focus();
            pesquisaEscolaridadeButton.Enabled = false;
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new CargosBO().Proximo().ToString();
            tipoComboBox.Focus();
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
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

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoComboBox.Focus();
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
                escolaridadeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                requerExperienciasCheckBox.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                requerExperienciasCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoComboBox.Focus();
            }
        }

        private void requerExperienciasCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void escolaridadeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                menorSalarioCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void competenciasGridGroupingControl_TableControlCurrentCellKeyDown(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlKeyEventArgs e)
        {
            if (e.Inner.KeyCode == Keys.Enter || e.Inner.KeyCode == Keys.Return)
                tecnicasGridGroupingControl.Focus();
            Publicas._escTeclado = false;
            if (e.Inner.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                maiorSalarioCurrencyTextBox.Focus();
            }
        }

        private void tecnicasGridGroupingControl_TableControlCurrentCellKeyDown(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlKeyEventArgs e)
        {
            if (e.Inner.KeyCode == Keys.Enter || e.Inner.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.Inner.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                competenciasGridGroupingControl.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tecnicasGridGroupingControl.Focus();
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

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                codigoTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(codigoTextBox.Text))
            {
                new Pesquisas.Cargos().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _cargos = new CargosBO().Consultar(Convert.ToInt32(codigoTextBox.Text));

            if (_cargos.Existe)
            {
                nomeTextBox.Text = _cargos.Descricao;
                ativoCheckBox.Checked = _cargos.Ativo;
                requerExperienciasCheckBox.Checked = _cargos.RequerExperiencia;
                escolaridadeTextBox.Text = _cargos.IdEscolaridade.ToString();
                menorSalarioCurrencyTextBox.DecimalValue = _cargos.SalarioMinimo;
                maiorSalarioCurrencyTextBox.DecimalValue = _cargos.SalarioMaximo;

                tipoComboBox.SelectedIndex = (_cargos.TipoDoCargo == "D" ? 0 :
                    (_cargos.TipoDoCargo == "G" ? 1 :
                    (_cargos.TipoDoCargo == "C" ? 2 : 3)));

                if (_cargos.IdEscolaridade != 0)
                    escolaridadeTextBox_Validating(sender, e);
            }

            _listaCompetencias = new CompetenciasDoCargoBO().Listar(_cargos.Id, "C", false, 0, "", Publicas.TipoPrazos.SemSelecao);
            _listaTecnicas = new CompetenciasDoCargoBO().Listar(_cargos.Id, "T");

            _listaMetasCrescimento = new MetasDoCargoBO().Listar(_cargos.Id, "C");
            _listaMetasResultado = new MetasDoCargoBO().Listar(_cargos.Id, "R");

            #region grid Competencias
            GridDynamicFilter filter = new GridDynamicFilter();
            GridMetroColors metroColor = new GridMetroColors();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.competenciasGridGroupingControl);
            filter.WireGrid(this.tecnicasGridGroupingControl);

            competenciasGridGroupingControl.DataSource = _listaCompetencias;
            competenciasGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            competenciasGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            competenciasGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            competenciasGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            competenciasGridGroupingControl.RecordNavigationBar.Label = "Comportamentais";
            competenciasGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < competenciasGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                competenciasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;

                if (i != 0)
                    competenciasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;

                competenciasGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                competenciasGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                competenciasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                competenciasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                competenciasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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
                competenciasGridGroupingControl.SetMetroStyle(metroColor);
                competenciasGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                competenciasGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            competenciasGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            competenciasGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            tecnicasGridGroupingControl.DataSource = _listaTecnicas;
            tecnicasGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            tecnicasGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            tecnicasGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            tecnicasGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            tecnicasGridGroupingControl.RecordNavigationBar.Label = "Técnicas";
            tecnicasGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < tecnicasGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                tecnicasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                if (i != 0)
                    tecnicasGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;

                tecnicasGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                tecnicasGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                tecnicasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                tecnicasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                tecnicasGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                tecnicasGridGroupingControl.SetMetroStyle(metroColor);
                tecnicasGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                tecnicasGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.tecnicasGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.tecnicasGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            #endregion

            #region grid metas

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.metasCrescimentoGridGroupingControl);
            filter.WireGrid(this.metasResultadoGridGroupingControl);

            metasResultadoGridGroupingControl.DataSource = _listaMetasResultado;
            metasResultadoGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            metasResultadoGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            metasResultadoGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            metasResultadoGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            metasResultadoGridGroupingControl.RecordNavigationBar.Label = "Resultados";
            metasResultadoGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < metasResultadoGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                metasResultadoGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;

                if (i == 1)
                    metasResultadoGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;

                metasResultadoGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                metasResultadoGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                metasResultadoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                metasResultadoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                metasResultadoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                metasResultadoGridGroupingControl.SetMetroStyle(metroColor);
                metasResultadoGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                metasResultadoGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.metasResultadoGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.metasResultadoGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            metasCrescimentoGridGroupingControl.DataSource = _listaMetasCrescimento;
            metasCrescimentoGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            metasCrescimentoGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            metasCrescimentoGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            metasCrescimentoGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            metasCrescimentoGridGroupingControl.RecordNavigationBar.Label = "Crescimento";
            metasCrescimentoGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < metasCrescimentoGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                metasCrescimentoGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                if (i != 0)
                    metasCrescimentoGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;

                metasCrescimentoGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                metasCrescimentoGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                metasCrescimentoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                metasCrescimentoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                metasCrescimentoGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                metasCrescimentoGridGroupingControl.SetMetroStyle(metroColor);
                metasCrescimentoGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                metasCrescimentoGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.metasCrescimentoGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.metasCrescimentoGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            #endregion

            excluirButton.Enabled = _cargos.Existe;
            gravarButton.Enabled = true;
                        
            if (Publicas._idRetornoPesquisa != 0)
                nomeTextBox.Focus();
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void escolaridadeTextBox_Validating(object sender, CancelEventArgs e)
        {
            escolaridadeTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaEscolaridadeButton.Enabled = false;
                return;
            }

            if (string.IsNullOrEmpty(escolaridadeTextBox.Text))
            {
                new Pesquisas.Escolaridade().ShowDialog();

                escolaridadeTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(escolaridadeTextBox.Text) || escolaridadeTextBox.Text == "0")
                {
                    escolaridadeTextBox.Text = string.Empty;
                    escolaridadeTextBox.Focus();
                    return;
                }
            }

            _escolaridade = new EscolaridadeBO().Consultar(Convert.ToInt32(escolaridadeTextBox.Text));

            if (!_escolaridade.Existe)
            {
                new Notificacoes.Mensagem("Escolaridade não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                escolaridadeTextBox.Focus();
                return;
            }

            if (!_escolaridade.Ativo)
            {
                new Notificacoes.Mensagem("Escolaridade está inativa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                escolaridadeTextBox.Focus();
                return;
            }

            descricaoEscolaridadeTextBox.Text = _escolaridade.Descricao;
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

        private void menorSalarioCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                maiorSalarioCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                escolaridadeTextBox.Focus();
            }
        }

        private void maiorSalarioCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                competenciasGridGroupingControl.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                menorSalarioCurrencyTextBox.Focus();
            }
        }

        private void escolaridadeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void escolaridadeTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaEscolaridadeButton.Enabled = string.IsNullOrEmpty(escolaridadeTextBox.Text.Trim());
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(codigoTextBox.Text))
            {
                new Pesquisas.Cargos().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text == "" || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }

                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaEscolaridadeButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(escolaridadeTextBox.Text))
            {
                new Pesquisas.Escolaridade().ShowDialog();

                escolaridadeTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (escolaridadeTextBox.Text == "" || escolaridadeTextBox.Text == "0")
                {
                    escolaridadeTextBox.Text = string.Empty;
                    escolaridadeTextBox.Focus();
                    return;
                }

                escolaridadeTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void tipoComboBox_KeyDown(object sender, KeyEventArgs e)
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

        private void tipoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void tipoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void escolaridadeTextBox_Enter(object sender, EventArgs e)
        {
            escolaridadeTextBox.BorderColor = Publicas._bordaEntrada;
            pesquisaEscolaridadeButton.Enabled = string.IsNullOrEmpty(escolaridadeTextBox.Text.Trim());
        }

        private void Cargos_Shown(object sender, EventArgs e)
        {
            int _h = Publicas._heigthTelaInicial - Publicas._heigthBarraTelaInicial - Publicas._heigthTituloTelaInicial;

            if (_h <= this.Height)
                this.Location = new Point(this.Left, 60);
        }
    }
}
