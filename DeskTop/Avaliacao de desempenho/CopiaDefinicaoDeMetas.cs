using Classes;
using DynamicFilter;
using Negocio;
using Suportte.Notificacoes;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.Avaliacao_de_desempenho
{
    public partial class CopiaDefinicaoDeMetas : Form
    {
        public CopiaDefinicaoDeMetas()
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
                    gridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl.BackColor = Publicas._panelTitulo;
                    copiarGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    copiarGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    copiarGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    copiarGridGroupingControl.BackColor = Publicas._panelTitulo;
                    empresaComboBoxAdv.BackColor = Publicas._fundo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        #region Atributos
        public Classes.Empresa _empresa;
        public Classes.Cargos _cargos;
        public Classes.Colaboradores _colaborador;
        public List<Classes.ItensAvaliacaoMetas> _listaItensAvaliacao;
        List<Classes.Colaboradores> _listaColaboradores;
        Classes.AutoAvaliacao _avaliacao;
        #endregion

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

        private void CopiaDefinicaoDeMetas_Shown(object sender, EventArgs e)
        {
            #region grid Metas
            GridMetroColors metroColor = new GridMetroColors();

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.TableControl.CellToolTip.Active = true;
            gridGroupingControl.TableDescriptor.AllowEdit = tituloLabel.Text.Contains("Definição");

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                gridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
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
                this.gridGroupingControl.SetMetroStyle(metroColor);
                this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }
            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;            
            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl.Table.DefaultRecordRowHeight = 25;

            gridGroupingControl.DataSource = _listaItensAvaliacao;
            #endregion

            #region Colaboradores
            copiarGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            copiarGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            copiarGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            copiarGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            copiarGridGroupingControl.TableControl.CellToolTip.Active = true;
            copiarGridGroupingControl.TableDescriptor.AllowEdit = tituloLabel.Text.Contains("Definição");

            for (int i = 0; i < copiarGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                if (i == 0)
                    copiarGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                else
                {
                    copiarGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                    copiarGridGroupingControl.TableDescriptor.Columns[i].Width = 250;
                }

                copiarGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
            }

            if (!Publicas._TemaBlack)
            {
                this.copiarGridGroupingControl.SetMetroStyle(metroColor);
                this.copiarGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.copiarGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.copiarGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            this.copiarGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.copiarGridGroupingControl.Table.DefaultRecordRowHeight = 25;
            _listaColaboradores = new AutoAvaliacaoBO().Listar(_colaborador.Id, _cargos.Id, referenciaMaskedEditBox.ClipText.Trim(), _empresa.IdEmpresa);

            copiarGridGroupingControl.DataSource = _listaColaboradores;
            #endregion

        }

        private void copiarGridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {

        }

        private void marcarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copiarGridGroupingControl.DataSource = new List<Colaboradores>();
            _listaColaboradores.ForEach(u => u.Existe = true);
            copiarGridGroupingControl.DataSource = _listaColaboradores;
        }

        private void desmarcarTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copiarGridGroupingControl.DataSource = new List<Colaboradores>();
            _listaColaboradores.ForEach(u => u.Existe = false);
            copiarGridGroupingControl.DataSource = _listaColaboradores;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaColaboradores.Where(w => w.Existe).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhum colaborador foi selecionado para copiar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                copiarGridGroupingControl.Focus();
                return;
            }

            if (_avaliacao == null)
                _avaliacao = new AutoAvaliacao();

            _listaItensAvaliacao.ForEach(u => { u.Realizado = 0; u.Resultado = 0; u.Eficiencia = 0; });
            //_listaItensAvaliacao.ForEach(u => { u.Resultado = 0; u.Eficiencia = 0; });
            foreach (var item in _listaColaboradores.Where(w => w.Existe))
            {
                _avaliacao.IdColaborador = item.Id;
                _avaliacao.IdEmpresa = item.IdEmpresa;

                _avaliacao.IdUsuario = Publicas._idUsuario;
                _avaliacao.MesReferencia = Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim());
                _avaliacao.Tipo = Publicas.TipoPrazos.MetasNumericas;
                
                if (!new AutoAvaliacaoBO().Gravar(_avaliacao, null, _listaItensAvaliacao))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

            }

            new Notificacoes.Mensagem("Cópia finalizada." + Environment.NewLine + "A tela de cópia será fechada.", Publicas.TipoMensagem.Informacao).ShowDialog();
            Close();
        }

        private void CopiaDefinicaoDeMetas_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }
    }
}
