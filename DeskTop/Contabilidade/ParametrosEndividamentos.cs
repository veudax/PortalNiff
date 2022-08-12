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

namespace Suportte.Contabilidade
{
    public partial class ParametrosEndividamentos : Form
    {
        public ParametrosEndividamentos()
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

                    gridGroupingControl2.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl2.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl2.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl2.BackColor = Publicas._panelTitulo;
                    
                    gridGroupingControl3.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl3.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl3.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl3.BackColor = Publicas._panelTitulo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.Empresa> _listaEmpresas;
        Classes.Empresa _empresa;
        Classes.Usuario _usuarioAutorizador;
        Classes.RateioBeneficios.PlanoContabil _plano;
        Classes.RateioBeneficios.ContasContabeis _contas;
        Classes.CentroDeCustoContabil _custos;
        Classes.TipoDocumentoGlobus _tipoDocumento;
        Classes.FornecedoresGlobus _fornecedor;
        Classes.Financeiro.DespesaReceitaGlobus _despesas;
        Classes.Endividamento.Parametros _associaContas;
        List<Classes.Endividamento.Parametros> _listaParametros;
        List<Classes.Endividamento.Parametros> _listaParametrosTipo;
        List<Classes.Endividamento.Parametros> _listaParametrosEmail;
        List<Classes.Endividamento.Parametros> _listaParametrosLog;
        bool saiuDaModalidade = false;
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

        private void ParametrosEndividamentos_Shown(object sender, EventArgs e)
        {
            ModalidadeComboBox.Items.AddRange(new object[] { "Capital de Giro", "Cessão de Títulos", "Financiamento", "Refrota", "Guarupas", "Parcelamento RFB - Previdenciário", "Parcelamento RFB - Demais Débitos", "Parcelamento PGFN - Previdenciário", "Parcelamento PGFN - Demais Débitos", "Parcelamento", "Pert", "PMG" });
            ModalidadeComboBox.SelectedIndex = -1;

            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            GridMetroColors metroColor = new GridMetroColors();
            GridDynamicFilter filter = new GridDynamicFilter();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(gridGroupingControl1);
            filter.WireGrid(gridGroupingControl2);

            //changes the header text color on mouse hovering.
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

            // para permitir editar dados.
            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl1.Table.DefaultRecordRowHeight = 30;

            gridGroupingControl2.DataSource = new List<Classes.Endividamento.Parametros>();

            gridGroupingControl2.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl2.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl2.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false; 
            gridGroupingControl2.TableControl.CellToolTip.Active = true;
            gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl2.RecordNavigationBar.Label = "";

            for (int i = 0; i < gridGroupingControl2.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl2.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl2.TableDescriptor.Columns[i].AllowSort = true;
                gridGroupingControl2.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl2.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl2.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl2.SetMetroStyle(metroColor);
                this.gridGroupingControl2.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl2.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            // para permitir editar dados.
            this.gridGroupingControl2.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl2.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl2.Table.DefaultRecordRowHeight = 30;

            gridGroupingControl3.DataSource = new List<Classes.Endividamento.Parametros>();

            gridGroupingControl3.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl3.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl3.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl3.TableControl.CellToolTip.Active = true;
            gridGroupingControl3.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl3.RecordNavigationBar.Label = "";

            for (int i = 0; i < gridGroupingControl3.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl3.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl3.TableDescriptor.Columns[i].AllowSort = true;
                gridGroupingControl3.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl3.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl3.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl3.SetMetroStyle(metroColor);
                this.gridGroupingControl3.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl3.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            // para permitir editar dados.
            this.gridGroupingControl3.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl3.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl3.Table.DefaultRecordRowHeight = 30;
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void tipoDocumentoTextBox_Enter(object sender, EventArgs e)
        {
            tipoDocumentoTextBox.BorderColor = Publicas._bordaEntrada;
            pesquisaTipoButton.Enabled = string.IsNullOrEmpty(tipoDocumentoTextBox.Text.Trim());
        }

        private void FornecedorTextBox_Enter(object sender, EventArgs e)
        {
            FornecedorTextBox.BorderColor = Publicas._bordaEntrada;
            pesquisaFornecedorButton.Enabled = string.IsNullOrEmpty(FornecedorTextBox.Text.Trim());
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

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoDocumentoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void tipoDocumentoTextBox_KeyDown(object sender, KeyEventArgs e)
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
                ModalidadeComboBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoDocumentoTextBox.Focus();
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

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

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
            mensagemSistemaLabel.Text = "Pesquisando, aguarde...";
            Refresh();

            _listaParametros = new EndividamentoBO().Listar(_empresa.IdEmpresa, "F");
            _listaParametrosLog = new EndividamentoBO().Listar(_empresa.IdEmpresa, ""); // todos
            _listaParametrosTipo = new EndividamentoBO().Listar(_empresa.IdEmpresa, "T");
            _listaParametrosEmail = new EndividamentoBO().Listar(_empresa.IdEmpresa, "U");

            gridGroupingControl1.DataSource = _listaParametros;
            gridGroupingControl2.DataSource = _listaParametrosTipo;
            gridGroupingControl3.DataSource = _listaParametrosEmail;

            gravarButton.Enabled = true;
            excluirButton.Enabled = _listaParametros.Count() != 0;

            mensagemSistemaLabel.Text = "";
            Refresh();

        }

        private void tipoDocumentoTextBox_Validating(object sender, CancelEventArgs e)
        {
            tipoDocumentoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

                new Pesquisas.TipoDocumentoGlobus().ShowDialog();

                tipoDocumentoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text) || tipoDocumentoTextBox.Text == "0")
                {
                    tipoDocumentoTextBox.Text = string.Empty;
                    tipoDocumentoTextBox.Focus();
                    return;
                }
            }

            _tipoDocumento = new TipoDocumentoGlobusBO().Consultar(_empresa.CodigoEmpresaGlobus, tipoDocumentoTextBox.Text);

            if (!_tipoDocumento.Existe)
            {
                new Notificacoes.Mensagem("Tipo do documento não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tipoDocumentoTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _tipoDocumento.Descricao;

            bool encontrou = false;
            foreach (var item in _listaParametrosTipo.Where(w => w.CodigoTipoDocumento == _tipoDocumento.Codigo))
            {
                encontrou = true;
            }

            if (!encontrou)
            {
                _listaParametrosTipo.Add(new Classes.Endividamento.Parametros()
                {
                    IdEmpresa = _empresa.IdEmpresa,
                    CodigoTipoDocumento = _tipoDocumento.Codigo,
                    TipoDocto = _tipoDocumento.Codigo + " - " + _tipoDocumento.Descricao
                });
            }

            gridGroupingControl2.DataSource = new List<Classes.Endividamento.Parametros>();
            gridGroupingControl2.DataSource = _listaParametrosTipo;
            gravarButton.Enabled = _listaParametrosTipo.Count() != 0 || _listaParametros.Count() != 0 || _listaParametrosEmail.Count() != 0;

            tipoDocumentoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;

            tipoDocumentoTextBox.Focus();
        }

        private void pesquisaTipoButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

                new Pesquisas.TipoDocumentoGlobus().ShowDialog();

                tipoDocumentoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(tipoDocumentoTextBox.Text) || tipoDocumentoTextBox.Text == "0")
                {
                    tipoDocumentoTextBox.Text = string.Empty;
                    tipoDocumentoTextBox.Focus();
                    return;
                }

                tipoDocumentoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void FornecedorTextBox_Validating(object sender, CancelEventArgs e)
        {
            FornecedorTextBox.BorderColor = Publicas._bordaSaida;

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

            if (string.IsNullOrEmpty(FornecedorTextBox.Text.Trim()))
            {
                new Pesquisas.Fornecedores().ShowDialog();

                FornecedorTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(FornecedorTextBox.Text) || FornecedorTextBox.Text == "0")
                {
                    FornecedorTextBox.Text = string.Empty;
                    FornecedorTextBox.Focus();
                    return;
                }
            }

            try
            {
                FornecedorTextBox.Text = Convert.ToDecimal(FornecedorTextBox.Text).ToString("000000");
            }
            catch {}

            _fornecedor = new FornecedoresGlobusBO().Consultar(FornecedorTextBox.Text);

            if (!_fornecedor.Existe)
            {
                new Notificacoes.Mensagem("Fornecedor não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                FornecedorTextBox.Focus();
                return;
            }

            if (!_fornecedor.Ativo)
            {
                new Notificacoes.Mensagem("Fornecedor não está ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                FornecedorTextBox.Focus();
                return;
            }

            NomeFornecedorTextBox.Text = _fornecedor.NomeFantasia;

            bool encontrou = false;
            foreach (var item in _listaParametros.Where(w => w.CodigoFornecedor == _fornecedor.CodigoFornecedor &&
                                                             w.Modalidade == ModalidadeComboBox.Text &&
                                                             w.CodigoDespesa == _despesas.Codigo))
            {
                encontrou = true;
                item.Modalidade = ModalidadeComboBox.Text;
            }

            if (!encontrou)
            {
                _listaParametros.Add(new Classes.Endividamento.Parametros()
                {
                    IdEmpresa = _empresa.IdEmpresa,
                    CodigoFornecedor = _fornecedor.CodigoFornecedor,
                    NomeFantasia = _fornecedor.Numero + " - " + _fornecedor.NomeFantasia,
                    Modalidade = ModalidadeComboBox.Text,
                    CodigoDespesa = _despesas.Codigo,
                    TipoDespesa = _despesas.Codigo + " - " + _despesas.Descricao
                });
            }

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Parametros>();
            gridGroupingControl1.DataSource = _listaParametros;
            gravarButton.Enabled = _listaParametros.Count() != 0 || _listaParametrosTipo.Count() != 0 || _listaParametrosEmail.Count() != 0;

            FornecedorTextBox.Text = string.Empty;
            NomeFornecedorTextBox.Text = string.Empty;
            DespesaTextBox.Text = string.Empty;
            NomeDespesaTextBox.Text = string.Empty;

            temAlteracao = true;
            DespesaTextBox.Focus();
        }

        private void pesquisaFornecedorButton_Click(object sender, EventArgs e)
        {
            new Pesquisas.Fornecedores().ShowDialog();

            FornecedorTextBox.Text = Publicas._codigoRetornoPesquisa;

            if (string.IsNullOrEmpty(FornecedorTextBox.Text) || FornecedorTextBox.Text == "0")
            {
                FornecedorTextBox.Text = string.Empty;
                FornecedorTextBox.Focus();
                return;
            }

            FornecedorTextBox_Validating(sender, new CancelEventArgs());
        }

        private void FornecedorTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaParametros.Count() == 0 && _listaParametrosTipo.Count() == 0)
            {
                new Notificacoes.Mensagem("Informe o tipo de documento e fornecedor.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            if (!new EndividamentoBO().Gravar(_listaParametrosTipo))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!new EndividamentoBO().Gravar(_listaParametros))// fornecedores
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!new EndividamentoBO().Gravar(_listaParametrosEmail))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou parâmetro de endividamento para a empresa " + empresaComboBoxAdv.Text;
            _log.Tela = "Contabilidade - Endividamento - Parâmetros";

            foreach (var item in _listaParametrosTipo)
            {
                if (_listaParametrosLog.Where(w => w.CodigoTipoDocumento == item.CodigoTipoDocumento).Count() == 0)
                {
                    _log.Descricao = _log.Descricao + " Incluido o Tipo de Documento " + item.CodigoTipoDocumento;
                }
            }

            foreach (var item in _listaParametros)
            {
                if (_listaParametrosLog.Where(w => w.CodigoFornecedor == item.CodigoFornecedor &&
                                                   w.Modalidade == item.Modalidade &&
                                                   w.CodigoDespesa == item.CodigoDespesa).Count() == 0)
                {
                    _log.Descricao = _log.Descricao + " Incluido o fornecedor " + item.NomeFantasia +
                        " com a modalidade " + item.Modalidade +
                        (item.CodigoDespesa != "" ? " e despesa " + item.CodigoDespesa : "");
                }
            }

            foreach (var item in _listaParametrosEmail)
            {
                if (_listaParametrosLog.Where(w => w.IdUsuario == item.IdUsuario).Count() == 0)
                {
                    _log.Descricao = _log.Descricao + " Incluido o Usuário " + item.NomeUsuario +
                        " com o email " + item.Email;
                }
            }

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            temAlteracao = false;
            tipoDocumentoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            FornecedorTextBox.Text = string.Empty;
            NomeFornecedorTextBox.Text = string.Empty;

            _listaParametros.Clear();
            _listaParametrosLog.Clear();

            gridGroupingControl1.DataSource = new List<Classes.Endividamento.Parametros>();
            gridGroupingControl2.DataSource = new List<Classes.Endividamento.Parametros>();
            gridGroupingControl3.DataSource = new List<Classes.Endividamento.Parametros>();
            empresaComboBoxAdv.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (new Notificacoes.Mensagem("Ira excluir todo o cadastro." + Environment.NewLine
                + Environment.NewLine
                + "Confirma ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new EndividamentoBO().ExcluirParametros(_empresa.IdEmpresa))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu Parâmetro de endividamento para a empresa " + empresaComboBoxAdv.Text;
            _log.Tela = "Contabilidade - Endividamento - Parâmetros";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            limparButton_Click(sender, e);
        }

        private void excluirFornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            decimal codigoFornecedor = 0;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (new Notificacoes.Mensagem("Confirma a exclusão do fornecedor selecionado ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.Endividamento.Parametros _excluirTipos = new Classes.Endividamento.Parametros();

                    gridGroupingControl1.DataSource = new List<Classes.Endividamento.Parametros>();

                    try
                    {
                        codigoFornecedor = (decimal)dr["CodigoFornecedor"];
                    }
                    catch
                    {
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("CodigoFornecedor =") + 18;
                            posFimId = dr.Info.IndexOf(", CodigoTipoDocumento");
                            codigoFornecedor = Convert.ToDecimal(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        }
                        catch { }
                    }

                    foreach (var item in _listaParametros.Where(w => w.CodigoFornecedor == codigoFornecedor))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new EndividamentoBO().ExcluirFornecedor(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _listaParametros.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu o fornecedor " + _excluirTipos.NomeFantasia +
                        " do parametros de endividamento da empresa " + empresaComboBoxAdv.Text;
                    _log.Tela = "Contabilidade - Endividamento - Parâmetros";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _listaParametros;
                gravarButton.Enabled = _listaParametrosTipo.Count() != 0 || _listaParametros.Count() != 0 || _listaParametrosEmail.Count() != 0;
            }

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

        private void ParametrosEndividamentos_FormClosing(object sender, FormClosingEventArgs e)
        { }

        private void DespesaTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (saiuDaModalidade)
            {
                DespesaTextBox.Focus();
                saiuDaModalidade = false;
                return;
            }

            saiuDaModalidade = false;

            DespesaTextBox.BorderColor = Publicas._bordaSaida;

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

            if (string.IsNullOrEmpty(DespesaTextBox.Text.Trim()))
            {
                Pesquisas.Despesas _tela = new Pesquisas.Despesas();
                Publicas._tipoDespesaReceita = "D";
                _tela.ShowDialog();

                DespesaTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(DespesaTextBox.Text) || DespesaTextBox.Text == "0")
                {
                    DespesaTextBox.Text = string.Empty;
                    DespesaTextBox.Focus();
                    return;
                }
            }

            try
            {
                DespesaTextBox.Text = Convert.ToDecimal(DespesaTextBox.Text).ToString("00000");
            }
            catch { }

            _despesas = new FinanceiroBO().Consultar(DespesaTextBox.Text, "D");

            if (!_despesas.Existe)
            {
                new Notificacoes.Mensagem("Tipo de Despesa não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                DespesaTextBox.Focus();
                return;
            }

            if (!_despesas.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Tipo de Despesa não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                DespesaTextBox.Focus();
                return;
            }

            NomeDespesaTextBox.Text = _despesas.Descricao;
        }

        private void DespesaTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void PesquisaDespesaButton_Click(object sender, EventArgs e)
        {
            Pesquisas.Despesas _tela = new Pesquisas.Despesas();
            Publicas._tipoDespesaReceita = "D";
            _tela.ShowDialog();

            DespesaTextBox.Text = Publicas._codigoRetornoPesquisa;

            if (string.IsNullOrEmpty(DespesaTextBox.Text) || DespesaTextBox.Text == "0")
            {
                DespesaTextBox.Text = string.Empty;
                DespesaTextBox.Focus();
                return;
            }

            DespesaTextBox_Validating(sender, new CancelEventArgs());
        }

        private void ModalidadeComboBox_Validating(object sender, CancelEventArgs e)
        {
            ModalidadeComboBox.FlatBorderColor = Publicas._bordaSaida;
            
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            saiuDaModalidade = true;
        }

        private void associarDadosContábeisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Publicas._escTeclado = true;

            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            decimal codigoFornecedor = 0;
            string modalidade = "";
            string TpDespesas = "";

            if (rec == null)
            {
                new Notificacoes.Mensagem("Nenhuma Modalidade/Fornecedor selecionado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                gridGroupingControl1.Focus();
                return;
            }
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    _associaContas = new Classes.Endividamento.Parametros();

                    try
                    {
                        codigoFornecedor = (decimal)dr["CodigoFornecedor"];
                        modalidade = (string)dr["Modalidade"];
                        TpDespesas = (string)dr["CodigoDespesa"];
                    }
                    catch
                    {
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("Modalidade =") + 12;
                            posFimId = dr.Info.IndexOf(", CodigoDespesa");
                            modalidade = dr.Info.Substring(posIniId, posFimId - posIniId).Trim();

                            posIniId = dr.Info.IndexOf("CodigoDespesa =") + 15;
                            posFimId = dr.Info.IndexOf(", TipoDocto");
                            TpDespesas = dr.Info.Substring(posIniId, posFimId - posIniId).Trim();

                            posIniId = dr.Info.IndexOf("CodigoFornecedor =") + 18;
                            posFimId = dr.Info.IndexOf(", CodigoTipoDocumento");
                            codigoFornecedor = Convert.ToDecimal(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());

                        }
                        catch { }
                    }

                    foreach (var item in _listaParametros.Where(w => w.CodigoFornecedor == codigoFornecedor && w.Modalidade == modalidade && w.CodigoDespesa == TpDespesas))
                    {
                        _associaContas = item;
                        break;
                    }

                    FornecedorModalidadeLabel.Text = _associaContas.NomeFantasia + " / " + _associaContas.Modalidade + " / " + _associaContas.TipoDespesa;

                    if (_associaContas.Plano != 0)
                        PlanoTextBox.Text = _associaContas.Plano.ToString();

                    if (_associaContas.CodigoContaJurosDebito != 0)
                    {
                        JurosDebitoTextBox.Text = _associaContas.CodigoContaJurosDebito.ToString();
                        NomeJurosDebitoTextBox.Text = _associaContas.ContaJurosDebito;
                    }

                    if (_associaContas.CodigoContaJurosCredito != 0)
                    {
                        JurosCreditoTextBox.Text = _associaContas.CodigoContaJurosCredito.ToString();
                        NomeJurosCreditoTextBox.Text = _associaContas.ContaJurosCredito;
                    }

                    if (_associaContas.CodigoContaVariacaoDebito != 0)
                    {
                        VariacaoDebitoTextBox.Text = _associaContas.CodigoContaVariacaoDebito.ToString();
                        NomeVariacaoDebitoTextBox.Text = _associaContas.ContaVariacaoDebito;
                    }

                    if (_associaContas.CodigoContaVariacaoCredito != 0)
                    {
                        VariacaoCreditoTextBox.Text = _associaContas.CodigoContaVariacaoCredito.ToString();
                        NomeVariacaoCreditoTextBox.Text = _associaContas.ContaVariacaoCredito;
                    }

                    if (_associaContas.CodigoContaCurtoPrazo != 0)
                    {
                        CurtoPrazoTextBox.Text = _associaContas.CodigoContaCurtoPrazo.ToString();
                        NomeCurtoPrazoTextBox.Text = _associaContas.ContaCurtoPrazo;
                    }

                    if (_associaContas.CodigoContaCurtoPrevisto != 0)
                    {
                        ContaCurtoPrevistoTextBox.Text = _associaContas.CodigoContaCurtoPrevisto.ToString();
                        NomeCurtoPrevistoTextBox.Text = _associaContas.ContaCurtoPrevisto;
                    }

                    if (_associaContas.CodigoContaLongoPrevisto != 0)
                    {
                        ContaLongoPrevistoTextBox.Text = _associaContas.CodigoContaLongoPrevisto.ToString();
                        NomeLongoPrevistoTextBox.Text = _associaContas.ContaLongoPrevisto;
                    }

                    if (_associaContas.CodigoContaLongoPrazo != 0)
                    {
                        LongoPrazoTextBox.Text = _associaContas.CodigoContaLongoPrazo.ToString();
                        NomeLongoPrazoTextBox.Text = _associaContas.ContaLongoPrazo;
                    }
                    
                    if (_associaContas.CustoJuros != 0)
                        CustoJurosTextBox.Text = _associaContas.CustoJuros.ToString();

                    if (_associaContas.CustoJurosConciliacao != 0)
                        CustoJurosConciliacaoTextBox.Text = _associaContas.CustoJurosConciliacao.ToString();

                    if (_associaContas.CustoVariacao != 0)
                        CustoVariacaoTextBox.Text = _associaContas.CustoVariacao.ToString();

                    if (_associaContas.CustoPrevsto != 0)
                        CustoPrevistoTextBox.Text = _associaContas.CustoPrevsto.ToString();

                    LoteTextBox.Text = _associaContas.Lote;
                    HistoricoJurosTextBox.Text = _associaContas.HistoricoJuros;
                    HistoricoVariacaoTextBox.Text = _associaContas.HistoricoVariacao;

                    HistoricoJurosConciliacaoTextBox.Text = _associaContas.HistoricoJurosConciliacao;
                    HistoricoPrevistoTextBox.Text = _associaContas.HistoricoPrevisto;
                }
            }
            
            ContabilTabPage.TabVisible = true;
            tabControlAdv1.SelectedTab = ContabilTabPage;
            PlanoTextBox.Focus();
        }

        private void AssociarButton_Click(object sender, EventArgs e)
        {
            //_associaContas

            _associaContas.Plano = _plano.NumeroPlano;

            if (string.IsNullOrEmpty(LoteTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe o Lote.", Publicas.TipoMensagem.Alerta).ShowDialog();
                LoteTextBox.Focus();
                return;
            }

            _associaContas.CodigoContaJurosDebito = 0;
            _associaContas.ContaJurosDebito = "";
            _associaContas.CodigoContaJurosCredito = 0;
            _associaContas.ContaJurosCredito = "";
            _associaContas.CodigoContaVariacaoDebito = 0;
            _associaContas.ContaVariacaoDebito = "";
            _associaContas.CodigoContaVariacaoCredito = 0;
            _associaContas.ContaVariacaoCredito = "";
            _associaContas.CodigoContaCurtoPrazo = 0;
            _associaContas.ContaCurtoPrazo = "";
            _associaContas.CodigoContaLongoPrazo = 0;
            _associaContas.ContaLongoPrazo = "";
            _associaContas.CodigoContaCurtoPrevisto = 0;
            _associaContas.ContaCurtoPrevisto = "";
            _associaContas.CodigoContaLongoPrevisto = 0;
            _associaContas.ContaLongoPrevisto = "";
            _associaContas.CustoJuros = null;
            _associaContas.CustoVariacao = null;
            _associaContas.CustoJurosConciliacao = null;
            _associaContas.CustoPrevsto = null;

            if (!string.IsNullOrEmpty(JurosDebitoTextBox.Text))
            {
                _associaContas.CodigoContaJurosDebito = Convert.ToInt32(JurosDebitoTextBox.Text);
                _associaContas.ContaJurosDebito = NomeJurosDebitoTextBox.Text;
            }

            if (!string.IsNullOrEmpty(JurosCreditoTextBox.Text))
            {
                _associaContas.CodigoContaJurosCredito = Convert.ToInt32(JurosCreditoTextBox.Text);
                _associaContas.ContaJurosCredito = NomeJurosCreditoTextBox.Text;
            }

            if (!string.IsNullOrEmpty(VariacaoDebitoTextBox.Text))
            {
                _associaContas.CodigoContaVariacaoDebito = Convert.ToInt32(VariacaoDebitoTextBox.Text);
                _associaContas.ContaVariacaoDebito = NomeVariacaoDebitoTextBox.Text;
            }

            if (!string.IsNullOrEmpty(VariacaoCreditoTextBox.Text))
            {
                _associaContas.CodigoContaVariacaoCredito = Convert.ToInt32(VariacaoCreditoTextBox.Text);
                _associaContas.ContaVariacaoCredito = NomeVariacaoCreditoTextBox.Text;
            }

            if (!string.IsNullOrEmpty(CurtoPrazoTextBox.Text))
            {
                _associaContas.CodigoContaCurtoPrazo = Convert.ToInt32(CurtoPrazoTextBox.Text);
                _associaContas.ContaCurtoPrazo = NomeCurtoPrazoTextBox.Text;
            }

            if (!string.IsNullOrEmpty(LongoPrazoTextBox.Text))
            {
                _associaContas.CodigoContaLongoPrazo = Convert.ToInt32(LongoPrazoTextBox.Text);
                _associaContas.ContaLongoPrazo = NomeLongoPrazoTextBox.Text;
            }

            if (!string.IsNullOrEmpty(ContaCurtoPrevistoTextBox.Text))
            {
                _associaContas.CodigoContaCurtoPrevisto = Convert.ToInt32(ContaCurtoPrevistoTextBox.Text);
                _associaContas.ContaCurtoPrevisto = NomeCurtoPrevistoTextBox.Text;
            }

            if (!string.IsNullOrEmpty(ContaLongoPrevistoTextBox.Text))
            {
                _associaContas.CodigoContaLongoPrevisto = Convert.ToInt32(ContaLongoPrevistoTextBox.Text);
                _associaContas.ContaLongoPrevisto = NomeLongoPrevistoTextBox.Text;
            }

            if (!string.IsNullOrEmpty(CustoJurosTextBox.Text))
                _associaContas.CustoJuros = Convert.ToInt32(CustoJurosTextBox.Text);

            if (!string.IsNullOrEmpty(CustoVariacaoTextBox.Text))
                _associaContas.CustoVariacao = Convert.ToInt32(CustoVariacaoTextBox.Text);

            if (!string.IsNullOrEmpty(CustoJurosConciliacaoTextBox.Text))
                _associaContas.CustoJurosConciliacao = Convert.ToInt32(CustoJurosConciliacaoTextBox.Text);

            if (!string.IsNullOrEmpty(CustoPrevistoTextBox.Text))
                _associaContas.CustoPrevsto = Convert.ToInt32(CustoPrevistoTextBox.Text);

            _associaContas.Lote = LoteTextBox.Text;
            _associaContas.HistoricoJuros = HistoricoJurosTextBox.Text;
            _associaContas.HistoricoVariacao = HistoricoVariacaoTextBox.Text;
            _associaContas.HistoricoJurosConciliacao = HistoricoJurosConciliacaoTextBox.Text;
            _associaContas.HistoricoPrevisto = HistoricoPrevistoTextBox.Text;

            temAlteracao = true;
             
            ContabilTabPage.TabVisible = false;
            tabControlAdv1.SelectedTab = AssociacoesTabPage;
            gridGroupingControl2.Focus();
        }

        private void PlanoTextBox_KeyDown(object sender, KeyEventArgs e)
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
                ContabilTabPage.TabVisible = false;
                tabControlAdv1.SelectedTab = AssociacoesTabPage;
                gridGroupingControl2.Focus();
            }
        }

        private void PlanoTextBox_Validating(object sender, CancelEventArgs e)
        {
            PlanoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                ContabilTabPage.TabVisible = false;
                tabControlAdv1.SelectedTab = AssociacoesTabPage;
                gridGroupingControl2.Focus();
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
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

        private void JurosDebitoTextBox_Validating(object sender, CancelEventArgs e)
        {
            JurosDebitoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (JurosDebitoTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _plano.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                JurosDebitoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (JurosDebitoTextBox.Text.Trim() == "" || JurosCreditoTextBox.Text == "0")
                {
                    JurosDebitoTextBox.Text = string.Empty;
                    JurosDebitoTextBox.Focus();
                    return;
                }
            }

            _contas = new RateioBeneficioBO().Consultar(_plano.NumeroPlano, Convert.ToInt32(JurosDebitoTextBox.Text));

            if (!_contas.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                JurosDebitoTextBox.Focus();
                return;
            }

            if (!_contas.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                JurosDebitoTextBox.Focus();
                return;
            }

            NomeJurosDebitoTextBox.Text = _contas.Codigo + " " + _contas.Nome;
        }

        private void JurosCreditoTextBox_Validating(object sender, CancelEventArgs e)
        {
            JurosCreditoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (JurosCreditoTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _plano.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                JurosCreditoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (JurosCreditoTextBox.Text.Trim() == "" || JurosCreditoTextBox.Text == "0")
                {
                    JurosCreditoTextBox.Text = string.Empty;
                    JurosCreditoTextBox.Focus();
                    return;
                }
            }

            _contas = new RateioBeneficioBO().Consultar(_plano.NumeroPlano, Convert.ToInt32(JurosCreditoTextBox.Text));

            if (!_contas.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                JurosCreditoTextBox.Focus();
                return;
            }

            if (!_contas.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                JurosCreditoTextBox.Focus();
                return;
            }

            NomeJurosCreditoTextBox.Text = _contas.Codigo + " " + _contas.Nome;
        }

        private void VariacaoDebitoTextBox_Validating(object sender, CancelEventArgs e)
        {
            VariacaoDebitoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (VariacaoDebitoTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _plano.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                VariacaoDebitoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (VariacaoDebitoTextBox.Text.Trim() == "" || VariacaoDebitoTextBox.Text == "0")
                {
                    VariacaoDebitoTextBox.Text = string.Empty;
                    VariacaoDebitoTextBox.Focus();
                    return;
                }
            }

            _contas = new RateioBeneficioBO().Consultar(_plano.NumeroPlano, Convert.ToInt32(VariacaoDebitoTextBox.Text));

            if (!_contas.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                VariacaoDebitoTextBox.Focus();
                return;
            }

            if (!_contas.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                VariacaoDebitoTextBox.Focus();
                return;
            }

            NomeVariacaoDebitoTextBox.Text = _contas.Codigo + " " + _contas.Nome;
        }

        private void VariacaoCreditoTextBox_Validating(object sender, CancelEventArgs e)
        {
            VariacaoCreditoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (VariacaoCreditoTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _plano.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                VariacaoCreditoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (VariacaoCreditoTextBox.Text.Trim() == "" || VariacaoCreditoTextBox.Text == "0")
                {
                    VariacaoCreditoTextBox.Text = string.Empty;
                    VariacaoCreditoTextBox.Focus();
                    return;
                }
            }

            _contas = new RateioBeneficioBO().Consultar(_plano.NumeroPlano, Convert.ToInt32(VariacaoCreditoTextBox.Text));

            if (!_contas.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                VariacaoCreditoTextBox.Focus();
                return;
            }

            if (!_contas.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                VariacaoCreditoTextBox.Focus();
                return;
            }

            NomeVariacaoCreditoTextBox.Text = _contas.Codigo + " " + _contas.Nome;
        }

        private void CurtoPrazoTextBox_Validating(object sender, CancelEventArgs e)
        {
            CurtoPrazoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (CurtoPrazoTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _plano.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                CurtoPrazoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CurtoPrazoTextBox.Text.Trim() == "" || CurtoPrazoTextBox.Text == "0")
                {
                    CurtoPrazoTextBox.Text = string.Empty;
                    CurtoPrazoTextBox.Focus();
                    return;
                }
            }

            _contas = new RateioBeneficioBO().Consultar(_plano.NumeroPlano, Convert.ToInt32(CurtoPrazoTextBox.Text));

            if (!_contas.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CurtoPrazoTextBox.Focus();
                return;
            }

            if (!_contas.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CurtoPrazoTextBox.Focus();
                return;
            }

            NomeCurtoPrazoTextBox.Text = _contas.Codigo + " " + _contas.Nome;
        }
        
        private void LongoPrazoTextBox_Validating(object sender, CancelEventArgs e)
        {
            LongoPrazoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (LongoPrazoTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _plano.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                LongoPrazoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (LongoPrazoTextBox.Text.Trim() == "" || LongoPrazoTextBox.Text == "0")
                {
                    LongoPrazoTextBox.Text = string.Empty;
                    LongoPrazoTextBox.Focus();
                    return;
                }
            }

            _contas = new RateioBeneficioBO().Consultar(_plano.NumeroPlano, Convert.ToInt32(LongoPrazoTextBox.Text));

            if (!_contas.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                LongoPrazoTextBox.Focus();
                return;
            }

            if (!_contas.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                LongoPrazoTextBox.Focus();
                return;
            }

            NomeLongoPrazoTextBox.Text = _contas.Codigo + " " + _contas.Nome;
        }

        private void JurosDebitoTextBox_KeyDown(object sender, KeyEventArgs e)
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
                VariacaoDebitoTextBox.Focus();
            }
        }

        private void VariacaoDebitoTextBox_KeyDown(object sender, KeyEventArgs e)
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
                CurtoPrazoTextBox.Focus();
            }
        }

        private void CurtoPrazoTextBox_KeyDown(object sender, KeyEventArgs e)
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
                ContaCurtoPrevistoTextBox.Focus();
            }
        }

        private void LongoPrazoTextBox_KeyDown(object sender, KeyEventArgs e)
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
                ContaCurtoPrevistoTextBox.Focus();
            }
        }

        private void HistoricoJurosTextBox_Validating(object sender, CancelEventArgs e)
        {
            HistoricoJurosTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void LoteTextBox_Validating(object sender, CancelEventArgs e)
        {
            LoteTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void HistoricoVariacaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            HistoricoVariacaoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void LoteTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void ContaCurtoPrevistoTextBox_KeyDown(object sender, KeyEventArgs e)
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
                AssociarButton.Focus();
            }
        }

        private void ContaCurtoPrevistoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ContaCurtoPrevistoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (ContaCurtoPrevistoTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _plano.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                ContaCurtoPrevistoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (ContaCurtoPrevistoTextBox.Text.Trim() == "" || ContaCurtoPrevistoTextBox.Text == "0")
                {
                    ContaCurtoPrevistoTextBox.Text = string.Empty;
                    ContaCurtoPrevistoTextBox.Focus();
                    return;
                }
            }

            _contas = new RateioBeneficioBO().Consultar(_plano.NumeroPlano, Convert.ToInt32(ContaCurtoPrevistoTextBox.Text));

            if (!_contas.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContaCurtoPrevistoTextBox.Focus();
                return;
            }

            if (!_contas.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContaCurtoPrevistoTextBox.Focus();
                return;
            }

            NomeCurtoPrevistoTextBox.Text = _contas.Codigo + " " + _contas.Nome;
        }

        private void ContaLongoPrevistoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ContaLongoPrevistoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (ContaLongoPrevistoTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _plano.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                ContaLongoPrevistoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (ContaLongoPrevistoTextBox.Text.Trim() == "" || ContaLongoPrevistoTextBox.Text == "0")
                {
                    ContaLongoPrevistoTextBox.Text = string.Empty;
                    ContaLongoPrevistoTextBox.Focus();
                    return;
                }
            }

            _contas = new RateioBeneficioBO().Consultar(_plano.NumeroPlano, Convert.ToInt32(ContaLongoPrevistoTextBox.Text));

            if (!_contas.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContaLongoPrevistoTextBox.Focus();
                return;
            }

            if (!_contas.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContaLongoPrevistoTextBox.Focus();
                return;
            }

            NomeLongoPrevistoTextBox.Text = _contas.Codigo + " " + _contas.Nome;
        }

        private void HistoricoJurosConciliacaoTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void HistoricoJurosConciliacaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            HistoricoJurosConciliacaoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void HistoricoPrevistoTextBox_Validating(object sender, CancelEventArgs e)
        {
            HistoricoPrevistoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void CustoJurosTextBox_Validating(object sender, CancelEventArgs e)
        {
            CustoJurosTextBox.BorderColor = Publicas._bordaSaida;

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

            if (CustoJurosTextBox.Text.Trim() == "")
            {
                Publicas._codigoRetornoPesquisa = _empresa.CodigoEmpresaGlobus;
                new Pesquisas.CentroDeCustoContabil().ShowDialog();

                CustoJurosTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CustoJurosTextBox.Text.Trim() == "" || CustoJurosTextBox.Text == "0")
                {
                    CustoJurosTextBox.Text = string.Empty;
                    CustoJurosTextBox.Focus();
                    return;
                }
            }

            _custos = new CentroDeCustoContabilBO().Consultar(Convert.ToInt32(CustoJurosTextBox.Text), _plano.NumeroPlano);

            if (!_custos.Existe)
            {
                new Notificacoes.Mensagem("Centro de custo não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CustoJurosTextBox.Focus();
                return;
            }

            if (!_custos.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Centro de custo não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CustoJurosTextBox.Focus();
                return;
            }
        }

        private void CustoVariacaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            CustoVariacaoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (CustoVariacaoTextBox.Text.Trim() == "")
            {
                Publicas._codigoRetornoPesquisa = _empresa.CodigoEmpresaGlobus;
                new Pesquisas.CentroDeCustoContabil().ShowDialog();

                CustoVariacaoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CustoVariacaoTextBox.Text.Trim() == "" || CustoVariacaoTextBox.Text == "0")
                {
                    CustoVariacaoTextBox.Text = string.Empty;
                    CustoVariacaoTextBox.Focus();
                    return;
                }
            }

            _custos = new CentroDeCustoContabilBO().Consultar(Convert.ToInt32(CustoVariacaoTextBox.Text), _plano.NumeroPlano);

            if (!_custos.Existe)
            {
                new Notificacoes.Mensagem("Centro de custo não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CustoVariacaoTextBox.Focus();
                return;
            }

            if (!_custos.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Centro de custo não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CustoVariacaoTextBox.Focus();
                return;
            }
        }

        private void CustoJurosConciliacaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            CustoJurosConciliacaoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (CustoJurosConciliacaoTextBox.Text.Trim() == "")
            {
                Publicas._codigoRetornoPesquisa = _empresa.CodigoEmpresaGlobus;
                new Pesquisas.CentroDeCustoContabil().ShowDialog();

                CustoJurosConciliacaoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CustoJurosConciliacaoTextBox.Text.Trim() == "" || CustoJurosConciliacaoTextBox.Text == "0")
                {
                    CustoJurosConciliacaoTextBox.Text = string.Empty;
                    CustoJurosConciliacaoTextBox.Focus();
                    return;
                }
            }

            _custos = new CentroDeCustoContabilBO().Consultar(Convert.ToInt32(CustoJurosConciliacaoTextBox.Text), _plano.NumeroPlano);

            if (!_custos.Existe)
            {
                new Notificacoes.Mensagem("Centro de custo não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CustoJurosConciliacaoTextBox.Focus();
                return;
            }

            if (!_custos.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Centro de custo não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CustoJurosConciliacaoTextBox.Focus();
                return;
            }
        }

        private void CustoPrevistoTextBox_Validating(object sender, CancelEventArgs e)
        {
            CustoPrevistoTextBox.BorderColor = Publicas._bordaSaida;

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

            if (CustoPrevistoTextBox.Text.Trim() == "")
            {
                Publicas._codigoRetornoPesquisa = _empresa.CodigoEmpresaGlobus;
                new Pesquisas.CentroDeCustoContabil().ShowDialog();

                CustoPrevistoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CustoPrevistoTextBox.Text.Trim() == "" || CustoPrevistoTextBox.Text == "0")
                {
                    CustoPrevistoTextBox.Text = string.Empty;
                    CustoPrevistoTextBox.Focus();
                    return;
                }
            }

            _custos = new CentroDeCustoContabilBO().Consultar(Convert.ToInt32(CustoPrevistoTextBox.Text), _plano.NumeroPlano);

            if (!_custos.Existe)
            {
                new Notificacoes.Mensagem("Centro de custo não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CustoPrevistoTextBox.Focus();
                return;
            }

            if (!_custos.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Centro de custo não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CustoPrevistoTextBox.Focus();
                return;
            }
        }

        private void CustoJurosTextBox_KeyDown(object sender, KeyEventArgs e)
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
                VariacaoDebitoTextBox.Focus();
            }
        }

        private void CustoVariacaoTextBox_KeyDown(object sender, KeyEventArgs e)
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
                CurtoPrazoTextBox.Focus();
            }
        }

        private void CustoJurosConciliacaoTextBox_KeyDown(object sender, KeyEventArgs e)
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
                ContaCurtoPrevistoTextBox.Focus();
            }
        }

        private void CustoPrevistoTextBox_KeyDown(object sender, KeyEventArgs e)
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
                AssociarButton.Focus();
            }
        }

        private void UsuarioAutorizacaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl3.Focus();
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                UsuarioAutorizacaoTextBox.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gridGroupingControl3.Focus();
            }
        }

        private void UsuarioAutorizacaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                UsuarioAutorizacaoTextBox.BorderColor = Publicas._bordaSaida;
            }
            catch { }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                return;
            }

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (UsuarioAutorizacaoTextBox.Text.Trim() == "")
            {
                Publicas._apenasAtivos = true;
                new Pesquisas.Usuarios().ShowDialog();

                UsuarioAutorizacaoTextBox.Text = Publicas._usuarioAcesso;

                if (UsuarioAutorizacaoTextBox.Text.Trim() == "")
                {
                    UsuarioAutorizacaoTextBox.Focus();
                    return;
                }
            }

            _usuarioAutorizador = new UsuarioBO().Consultar(UsuarioAutorizacaoTextBox.Text);

            if (!_usuarioAutorizador.Existe)
            {
                new Notificacoes.Mensagem("Usuário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                UsuarioAutorizacaoTextBox.Focus();
                return;
            }

            if (!_usuarioAutorizador.Ativo)
            {
                new Notificacoes.Mensagem("Usuário inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                UsuarioAutorizacaoTextBox.Focus();
                return;
            }

            bool encontrou = false;
            foreach (var item in _listaParametrosEmail.Where(w => w.IdUsuario == _usuarioAutorizador.Id))
            {
                encontrou = true;
            }

            if (!encontrou)
            {
                _listaParametrosEmail.Add(new Classes.Endividamento.Parametros()
                {
                    IdEmpresa = _empresa.IdEmpresa,
                    IdUsuario = _usuarioAutorizador.Id,
                    NomeUsuario = _usuarioAutorizador.Nome,
                    Email = _usuarioAutorizador.Email
                });
            }

            gridGroupingControl3.DataSource = new List<Classes.Endividamento.Parametros>();
            gridGroupingControl3.DataSource = _listaParametrosEmail;
            gravarButton.Enabled = _listaParametrosTipo.Count() != 0 || _listaParametros.Count() != 0 || _listaParametrosEmail.Count() != 0;
            UsuarioAutorizacaoTextBox.Text = string.Empty;
            UsuarioAutorizacaoTextBox.Focus();
        }

        private void pesquisaUsuarioButton_Click(object sender, EventArgs e)
        {
            Publicas._apenasAtivos = true;
            new Pesquisas.Usuarios().ShowDialog();

            UsuarioAutorizacaoTextBox.Text = Publicas._usuarioAcesso;

            if (UsuarioAutorizacaoTextBox.Text.Trim() == "")
            {
                UsuarioAutorizacaoTextBox.Focus();
                return;
            }

            UsuarioAutorizacaoTextBox_Validating(sender, new CancelEventArgs());
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl3.Table.DisplayElements[gridGroupingControl3.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            Int32 idUsuario = 0;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (new Notificacoes.Mensagem("Confirma a exclusão do usuário selecionado ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.Endividamento.Parametros _excluirTipos = new Classes.Endividamento.Parametros();

                    gridGroupingControl3.DataSource = new List<Classes.Endividamento.Parametros>();

                    try
                    {
                        idUsuario = (int)dr["idUsuario"];
                    }
                    catch
                    {
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("IdUsuario =") + 11;
                            posFimId = dr.Info.IndexOf(", NomeUsuario");
                            idUsuario = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        }
                        catch { }
                    }

                    foreach (var item in _listaParametrosEmail.Where(w => w.IdUsuario == idUsuario))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new EndividamentoBO().ExcluirFornecedor(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _listaParametrosEmail.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu o usuário " + _excluirTipos.NomeUsuario +
                        " do parametros de endividamento da empresa " + empresaComboBoxAdv.Text;
                    _log.Tela = "Contabilidade - Endividamento - Parâmetros";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl3.DataSource = _listaParametrosEmail;
                gravarButton.Enabled = _listaParametrosTipo.Count() != 0 || _listaParametros.Count() != 0 || _listaParametrosEmail.Count() != 0;
            }
        }
    }
}
