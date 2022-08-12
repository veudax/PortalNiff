using Classes;
using DynamicFilter;
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
    public partial class ParametrosArquivei : Form
    {
        public ParametrosArquivei()
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
                    CampoNotasGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    CampoNotasGrid.ColorStyles = ColorStyles.Office2010Black;
                    CampoNotasGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    CampoNotasGrid.BackColor = Publicas._panelTitulo;
                    
                    ItensNotasGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    ItensNotasGrid.ColorStyles = ColorStyles.Office2010Black;
                    ItensNotasGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    ItensNotasGrid.BackColor = Publicas._panelTitulo;
                    this.BackColor = Publicas._fundo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.Empresa> _listaEmpresas;
        Classes.Empresa _empresa;
        Classes.ParametrosArquivei _parametros;
        List<Classes.ItensParametrosArquivei> _itens;
        List<Classes.ItensParametrosArquivei> _itensLog = new List<ItensParametrosArquivei>();

        GridCurrentCell _colunaCorrente;

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

        private void ParametrosArquivei_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            TipoComboBox.Items.AddRange(new object[] { "Excluir", "Renomear" });

            #region Define layout Grid
            GridMetroColors metroColor = new GridMetroColors();
            GridDynamicFilter filter = new GridDynamicFilter();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(CampoNotasGrid);
            filter.WireGrid(ItensNotasGrid);

            #region Exibir e Validar
            #region Campos
            CampoNotasGrid.DataSource = new List<NotasFiscaisServico>();

            CampoNotasGrid.SortIconPlacement = SortIconPlacement.Left;
            CampoNotasGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            CampoNotasGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            CampoNotasGrid.TableControl.CellToolTip.Active = true;
            CampoNotasGrid.TopLevelGroupOptions.ShowFilterBar = true;
            CampoNotasGrid.RecordNavigationBar.Label = "Notas";

            for (int i = 0; i < CampoNotasGrid.TableDescriptor.Columns.Count; i++)
            {
                if (i == 0)
                    CampoNotasGrid.TableDescriptor.Columns[i].ReadOnly = true;
                else
                    CampoNotasGrid.TableDescriptor.Columns[i].ReadOnly = false;

                CampoNotasGrid.TableDescriptor.Columns[i].AllowFilter = true;
                CampoNotasGrid.TableDescriptor.Columns[i].AllowSort = true;
                CampoNotasGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                CampoNotasGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                CampoNotasGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

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

            if (!Publicas._TemaBlack)
            {
                this.CampoNotasGrid.SetMetroStyle(metroColor);
                this.CampoNotasGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.CampoNotasGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            // para permitir editar dados.
            this.CampoNotasGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.CampoNotasGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            CampoNotasGrid.Refresh();

            #endregion

            #region Itens
            ItensNotasGrid.DataSource = new List<ItensNotasFiscaisServico>();

            ItensNotasGrid.SortIconPlacement = SortIconPlacement.Left;
            ItensNotasGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            ItensNotasGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            ItensNotasGrid.TableControl.CellToolTip.Active = true;
            ItensNotasGrid.TopLevelGroupOptions.ShowFilterBar = true;
            ItensNotasGrid.RecordNavigationBar.Label = "Itens";

            for (int i = 0; i < ItensNotasGrid.TableDescriptor.Columns.Count; i++)
            {
                if (i == 0)
                    ItensNotasGrid.TableDescriptor.Columns[i].ReadOnly = true;
                else
                    ItensNotasGrid.TableDescriptor.Columns[i].ReadOnly = false;

                ItensNotasGrid.TableDescriptor.Columns[i].AllowFilter = true;
                ItensNotasGrid.TableDescriptor.Columns[i].AllowSort = true;
                ItensNotasGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                ItensNotasGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                ItensNotasGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.ItensNotasGrid.SetMetroStyle(metroColor);
                this.ItensNotasGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.ItensNotasGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            // para permitir editar dados.
            this.ItensNotasGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.ItensNotasGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            ItensNotasGrid.Refresh();

            #endregion
            #endregion

            
            #endregion
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void DiretorioTextBox_Enter(object sender, EventArgs e)
        {
            DiretorioTextBox.BorderColor = Publicas._bordaEntrada;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                DiretorioTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void DiretorioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ExportarTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }

            _parametros = new ParametrosArquiveiBO().Consultar(_empresa.IdEmpresa);
            TipoComboBox.SelectedIndex = 0;

            if (_parametros.Existe)
            {
                _itensLog = new ItensParametrosArquiveiBO().Listar(_parametros.Id);
                TipoComboBox.SelectedIndex = (_parametros.AcaoComArquivo == "E" ? 0 : 1);
                DiretorioTextBox.Text = _parametros.Diretorio;
                ExportarTextBox.Text = _parametros.DiretorioExportacao;
                DacteLocalArquivoTextBox.Text = _parametros.DiretorioDacte;
                NFSeLocalArquivoTextBox.Text = _parametros.DiretorioNFSe;
            }

            _itens = new List<ItensParametrosArquivei>() { new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.BairroDestinatario, ""), Tipo = "C" },
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.BaseICMS, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.CEPDestinatario, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ChaveDeAcesso, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.CNPJDestinatario, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.CNPJEmitente, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.DadosAdicionais, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.DataEmissao, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.EnderecoDestinatario, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.IEDestinatario, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.IEEmitente, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ModeloNF, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.NaturezaOperacao, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.NumeroNF, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.RazaoSocialDestinatario, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.RazaoSocialEmitente, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.Serie, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ValorProduto, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.ValorTotalNF, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.IsentasOutrasIgualContabil, "" ), Tipo = "C" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.MunicipioOrigem, "" ), Tipo = "C" },
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposCabecalhoValidarArquivei.MunicipioDestino, "" ), Tipo = "C" },
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.AliquotaICMS, "" ), Tipo = "I" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.CCe, "" ), Tipo = "I" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.CFOP, "" ), Tipo = "I" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.CST, "" ), Tipo = "I" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.CSTICMS, "" ), Tipo = "I" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.Desconto, "" ), Tipo = "I" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.OutrasDespesas, "" ), Tipo = "I" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.Seguro, "" ), Tipo = "I" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorFrete, "" ), Tipo = "I" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorICMS, "" ), Tipo = "I" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorICMSST, "" ), Tipo = "I" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorIPI, "" ), Tipo = "I" } ,
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.ValorTotal, "" ), Tipo = "I" },
                    new ItensParametrosArquivei() { NomeCampo = Publicas.GetDescription(Publicas.CamposItensValidarArquivei.OperacaoFiscal, "" ), Tipo = "I" }

            };

            foreach (var itemL in _itensLog)
            {
                foreach (var item in _itens.Where(w => w.NomeCampo == itemL.NomeCampo))
                {
                    item.Id = itemL.Id;
                    item.IdParametro = itemL.IdParametro;
                    item.Existe = true;
                    item.ExibirCampo = itemL.ExibirCampo;
                    item.Tipo = itemL.Tipo;
                    item.ValidarCampo = itemL.ValidarCampo;
                }
            }

            CampoNotasGrid.DataSource = _itens.Where(w => w.Tipo == "C").ToList();
            ItensNotasGrid.DataSource = _itens.Where(w => w.Tipo == "I").ToList();

            gravarButton.Enabled = true;
            excluirButton.Enabled = _parametros.Existe;            
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            DiretorioTextBox.Text = string.Empty;
            ExportarTextBox.Text = string.Empty;
            DacteLocalArquivoTextBox.Text = string.Empty;
            NFSeLocalArquivoTextBox.Text = string.Empty;
            TipoComboBox.SelectedIndex = 0;
            CampoNotasGrid.DataSource = new List<ItensParametrosArquivei>();
            ItensNotasGrid.DataSource = new List<ItensParametrosArquivei>();

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            empresaComboBoxAdv.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {

            if (DacteLocalArquivoTextBox.Text == DiretorioTextBox.Text)
            {
                new Notificacoes.Mensagem("Local dos arquivos Dacte tem que ser diferente do local dos arquivos de Nfe.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tabControlAdv1.SelectedTab = tabPageAdv1;
                DacteLocalArquivoTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(DiretorioTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o diretório onde se encontrará os arquivos de Nfe.", Publicas.TipoMensagem.Alerta).ShowDialog();
                DiretorioTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(DacteLocalArquivoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o diretório onde se encontrará os arquivos de Dacte.", Publicas.TipoMensagem.Alerta).ShowDialog();
                DiretorioTextBox.Focus();
                return;
            }

            if (_parametros == null)
                _parametros = new Classes.ParametrosArquivei();

            _parametros.IdEmpresa = _empresa.IdEmpresa;
            _parametros.Diretorio = DiretorioTextBox.Text;
            _parametros.AcaoComArquivo = "R"; // TipoComboBox.Text.Substring(0, 1); campo não é mais preciso
            _parametros.DiretorioExportacao = ExportarTextBox.Text;
            _parametros.DiretorioDacte = DacteLocalArquivoTextBox.Text;
            _parametros.DiretorioNFSe = NFSeLocalArquivoTextBox.Text;

            if (!new ParametrosArquiveiBO().Gravar(_parametros, _itens))
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

            if (!new ParametrosArquiveiBO().Excluir(_parametros.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void ValidarMarcarTodosToolStripMenu_Click(object sender, EventArgs e)
        {
            CampoNotasGrid.DataSource = new List<ItensParametrosArquivei>();
            foreach (var item in _itens.Where(w => w.Tipo == "C"))
            {
                item.ValidarCampo = true;
            }
            CampoNotasGrid.DataSource = _itens.Where(w => w.Tipo == "C").ToList();

        }

        private void ValidarDesmarcarTodosToolStripMenu_Click(object sender, EventArgs e)
        {
            CampoNotasGrid.DataSource = new List<ItensParametrosArquivei>();
            foreach (var item in _itens.Where(w => w.Tipo == "C"))
            {
                item.ValidarCampo = false;
            }
            CampoNotasGrid.DataSource = _itens.Where(w => w.Tipo == "C").ToList();
        }

        private void ExibirMarcarTodosToolStripMenu_Click(object sender, EventArgs e)
        {
            CampoNotasGrid.DataSource = new List<ItensParametrosArquivei>();
            foreach (var item in _itens.Where(w => w.Tipo == "C"))
            {
                item.ExibirCampo = true;
            }
            CampoNotasGrid.DataSource = _itens.Where(w => w.Tipo == "C").ToList();
        }

        private void ExibirDesmarcarTodosToolStripMenu_Click(object sender, EventArgs e)
        {
            CampoNotasGrid.DataSource = new List<ItensParametrosArquivei>();
            foreach (var item in _itens.Where(w => w.Tipo == "C"))
            {
                item.ExibirCampo = true;
            }
            CampoNotasGrid.DataSource = _itens.Where(w => w.Tipo == "C").ToList();
        }

        private void ItensValidarMarcarToolStripMenu_Click(object sender, EventArgs e)
        {
            ItensNotasGrid.DataSource = new List<ItensParametrosArquivei>();
            foreach (var item in _itens.Where(w => w.Tipo == "I"))
            {
                item.ValidarCampo = true;
            }
            ItensNotasGrid.DataSource = _itens.Where(w => w.Tipo == "I").ToList();
        }

        private void ItensValidarDesmarcarToolStripMenu_Click(object sender, EventArgs e)
        {
            ItensNotasGrid.DataSource = new List<ItensParametrosArquivei>();
            foreach (var item in _itens.Where(w => w.Tipo == "I"))
            {
                item.ValidarCampo = false;
            }
            ItensNotasGrid.DataSource = _itens.Where(w => w.Tipo == "I").ToList();
        }

        private void ItensExibirMarcarToolStripMenu_Click(object sender, EventArgs e)
        {
            ItensNotasGrid.DataSource = new List<ItensParametrosArquivei>();
            foreach (var item in _itens.Where(w => w.Tipo == "I"))
            {
                item.ExibirCampo = true;
            }
            ItensNotasGrid.DataSource = _itens.Where(w => w.Tipo == "I").ToList();
        }

        private void ItensExibirDesmarcarToolStripMenu_Click(object sender, EventArgs e)
        {
            ItensNotasGrid.DataSource = new List<ItensParametrosArquivei>();
            foreach (var item in _itens.Where(w => w.Tipo == "I"))
            {
                item.ExibirCampo = true;
            }
            ItensNotasGrid.DataSource = _itens.Where(w => w.Tipo == "I").ToList();
        }

        private void CampoNotasGrid_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            int _rowIndex = 0;

            try
            {
                _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.CampoNotasGrid.Table.DisplayElements[_rowIndex] as GridRecordRow;

                string nomeColuna = CampoNotasGrid.TableDescriptor.Columns[_colunaCorrente.ColIndex - 1].MappingName;
                bool marcado = false;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        marcado = (bool)dr[nomeColuna];

                        foreach (var item in _itens.Where(w => w.Id == (int)dr["Id"]))
                        {
                            if (nomeColuna == "ValidarCampo")
                            {
                                item.ValidarCampo = marcado;

                                if (marcado && !item.ExibirCampo)
                                    item.ExibirCampo = true;
                            }
                            if (nomeColuna == "ExibirCampo")
                            {
                                item.ExibirCampo = marcado;
                                if (!marcado && item.ValidarCampo)
                                    item.ValidarCampo = false;
                            }
                        }
                    }
                }
            }
            catch { }

            CampoNotasGrid.DataSource = new List<ItensParametrosArquivei>();

            CampoNotasGrid.DataSource = _itens.Where(w => w.Tipo == "C").ToList();
            CampoNotasGrid.Refresh();
        }

        private void CampoNotasGrid_TableControlMouseDown(object sender, GridTableControlMouseEventArgs e)
        {
            _colunaCorrente = CampoNotasGrid.TableControl.CurrentCell;
        }

        private void ItensNotasGrid_TableControlCellMouseDown(object sender, GridTableControlCellMouseEventArgs e)
        {
            _colunaCorrente = ItensNotasGrid.TableControl.CurrentCell;
        }

        private void ItensNotasGrid_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            int _rowIndex = 0;

            try
            {
                _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.ItensNotasGrid.Table.DisplayElements[_rowIndex] as GridRecordRow;

                string nomeColuna = ItensNotasGrid.TableDescriptor.Columns[_colunaCorrente.ColIndex - 1].MappingName;
                bool marcado = false;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        marcado = (bool)dr[nomeColuna];

                        foreach (var item in _itens.Where(w => w.Id == (int)dr["Id"]))
                        {
                            if (nomeColuna == "ValidarCampo")
                            {
                                item.ValidarCampo = marcado;

                                if (marcado && !item.ExibirCampo)
                                    item.ExibirCampo = true;
                            }
                            if (nomeColuna == "ExibirCampo")
                            {
                                item.ExibirCampo = marcado;
                                if (!marcado && item.ValidarCampo)
                                    item.ValidarCampo = false;
                            }                            
                        }
                    }
                }
            }
            catch { }

            ItensNotasGrid.DataSource = new List<ItensParametrosArquivei>();

            ItensNotasGrid.DataSource = _itens.Where(w => w.Tipo == "I").ToList();
            ItensNotasGrid.Refresh();
        }

        private void TipoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ExportarTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DiretorioTextBox.Focus();
            }
        }

        private void DiretorioTextBox_Validating(object sender, CancelEventArgs e)
        {
            DiretorioTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
            
            if (String.IsNullOrEmpty(DiretorioTextBox.Text.Trim()))
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    DiretorioTextBox.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void TipoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void TipoComboBox_Validating(object sender, CancelEventArgs e)
        {
            TipoComboBox.FlatBorderColor = Publicas._bordaSaida;
        }

        private void ExportarTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                DacteLocalArquivoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                TipoComboBox.Focus();
            }
        }

        private void ParametrosArquivei_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }

        private void DacteLocalArquivoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                NFSeLocalArquivoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ExportarTextBox.Focus();
            }
        }

        private void DacteLocalArquivoTextBox_Validating(object sender, CancelEventArgs e)
        {
            DacteLocalArquivoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (String.IsNullOrEmpty( DacteLocalArquivoTextBox.Text.Trim()))
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    DacteLocalArquivoTextBox.Text = folderBrowserDialog.SelectedPath;
                }
            }

        }

        private void ExportarTextBox_Validating(object sender, CancelEventArgs e)
        {
            ExportarTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (String.IsNullOrEmpty(ExportarTextBox.Text.Trim()))
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportarTextBox.Text = folderBrowserDialog.SelectedPath;
                }
            }

        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
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

        private void NFSeLocalArquivoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                tabControlAdv1.SelectedTab = tabPageAdv2;
                CampoNotasGrid.Focus();
                gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DacteLocalArquivoTextBox.Focus();
            }
        }
    }
}
