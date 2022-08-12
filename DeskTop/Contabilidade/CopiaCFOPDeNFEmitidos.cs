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
    public partial class CopiaCFOPDeNFEmitimos : Form
    {
        public CopiaCFOPDeNFEmitimos()
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
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        public Classes.Empresa _empresa;
        Classes.Empresa _empresaDestino;
        Classes.RateioBeneficios.Parametros _param;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasAutorizadas;
        GridCurrentCell _colunaCorrente;
        List<Classes.CFOPEmitidas> _listaCFOPsOrigem;
        List<Classes.CFOPEmitidas> _listaCFOPsDestino;

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

        private void CopiaCFOPDeNFEmitimos_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);
            _listaEmpresasAutorizadas = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._idUsuario);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            EmpresaDestinoComboBox.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            EmpresaDestinoComboBox.DisplayMember = "CodigoeNome";
            EmpresaDestinoComboBox.Focus();

            for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
            {
                empresaComboBoxAdv.SelectedIndex = i;
                if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                    break;
            }

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

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            gridGroupingControl1.Table.DefaultRecordRowHeight = 45;

        }

        private void EmpresaDestinoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gridGroupingControl1.Focus();
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

        private void EmpresaDestinoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void gravarButton_Enter(object sender, EventArgs e)
        {
            gravarButton.BackColor = Publicas._botaoFocado;
            gravarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void EmpresaDestinoComboBox_Validating(object sender, CancelEventArgs e)
        {
            EmpresaDestinoComboBox.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == EmpresaDestinoComboBox.Text))
            {
                _empresaDestino = item;
            }

            if (_listaEmpresasAutorizadas.Where(w => w.IdEmpresa == _empresaDestino.IdEmpresa && w.EmpresaAutoriza).Count() == 0)
            {
                new Notificacoes.Mensagem("Usuário não autorizado para está empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                EmpresaDestinoComboBox.Focus();
                return;
            }

            _param = new RateioBeneficioBO().ConsultarParametro(_empresaDestino.IdEmpresa);

            if (!_param.Existe)
            {
                new Notificacoes.Mensagem("Parâmetros de rateio não cadastrado para a empresa destino.", Publicas.TipoMensagem.Alerta).ShowDialog();
                EmpresaDestinoComboBox.Focus();
                return;
            }

            _listaCFOPsOrigem = new ArquiveiBO().ListarCFOPEmitidas(_empresa.IdEmpresa);
            _listaCFOPsDestino = new ArquiveiBO().ListarCFOPEmitidas(_empresaDestino.IdEmpresa);

            gridGroupingControl1.DataSource = new List<Classes.CFOPEmitidas>();

            foreach (var item in _listaCFOPsDestino)
            {
                foreach (var itemO in _listaCFOPsOrigem.Where(w => w.CFOPCodigo == item.CFOPCodigo &&
                                                                   w.Natureza == item.Natureza &&
                                                                   w.Lei == item.Lei &&
                                                                   w.CST == item.CST &&
                                                                   w.Operacao == item.Operacao))
                {
                    itemO.Copiar = true;
                    itemO.CopiarAnterior = true;
                }
            }

            gridGroupingControl1.DataSource = new List<Classes.CFOPEmitidas>();
            gridGroupingControl1.DataSource = _listaCFOPsOrigem;
            gravarButton.Enabled = true;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaCFOPsOrigem.Where(w => w.Copiar).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma CFOP selecionada para copiar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            _listaCFOPsOrigem.ForEach(u => { u.Existe = false; u.IdEmpresa = _empresaDestino.IdEmpresa; });

            foreach (var item in _listaCFOPsOrigem.Where(w => w.Copiar))
            {
                if (!new ArquiveiBO().GravarCFOPEmitidas(item))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;

            _log.Descricao = "Copiou o CFOPxCST - NF Emitidas da empresa " + empresaComboBoxAdv.Text + " para a empresa " + EmpresaDestinoComboBox.Text;

            string _descricao = "";

            foreach (var item in _listaCFOPsOrigem.Where(w => w.Copiar && !w.CopiarAnterior))
            {
                _descricao = _descricao + " Incluido o CFOP " + item.CFOPCodigo + " natureza " + item.Natureza + " Lei " + item.Lei + 
                    " CST " + item.CST + " Operação " + item.Operacao;
            }

            _log.Descricao = _log.Descricao + _descricao;
            _log.Tela = "Contabilidade - Cadastros - Copia de CFOP x CST ";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            new Notificacoes.Mensagem("Processo finalizado com sucesso." + Environment.NewLine + "A tela será fechada!", Publicas.TipoMensagem.Sucesso).ShowDialog();
            Close();
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            { // buscar da empresa do usuario
                GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;
                Record dr = null;

                if (rec != null)
                {
                    dr = rec.GetRecord() as Record;

                    if ((bool)dr["CopiarAnterior"])
                        e.Style.TextColor = Color.Gray;
                }
            }
            catch { }
        }

        private void CopiaCFOPDeNFEmitimos_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }

        private void gridGroupingControl1_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            try
            {

                GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex()] as GridRecordRow;

                bool marcado = false;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        marcado = (bool)dr["Copiar"];

                        foreach (var item in _listaCFOPsOrigem.Where(w => w.Id == (int)dr["Id"]))
                        {
                            if (!marcado)
                                item.Copiar = false;
                            else
                                item.Copiar = marcado;
                        }
                    }
                }
            }
            catch { }

            gridGroupingControl1.DataSource = new List<Classes.CFOPEmitidas>();
            gridGroupingControl1.DataSource = _listaCFOPsOrigem;
        }

        private void gridGroupingControl1_TableControlMouseDown(object sender, GridTableControlMouseEventArgs e)
        {
            _colunaCorrente = gridGroupingControl1.TableControl.CurrentCell;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
