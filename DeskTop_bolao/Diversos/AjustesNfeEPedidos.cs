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
    public partial class AjustesNfeEPedidos : Form
    {
        public AjustesNfeEPedidos()
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

        List<PedidoGlobus> _listaPedido;

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

        private void AjustesNfeEPedidos_Load(object sender, EventArgs e)
        {
                        
            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;

            #region Pedidos
            pedidosGridDataBoundGrid.AllowProportionalColumnSizing = false;
            pedidosGridDataBoundGrid.SortIconPlacement = SortIconPlacement.Left;
            pedidosGridDataBoundGrid.TopLevelGroupOptions.ShowFilterBar = true;
            pedidosGridDataBoundGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            pedidosGridDataBoundGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            pedidosGridDataBoundGrid.RecordNavigationBar.Label = "Itens do pedido";

            filter.WireGrid(this.pedidosGridDataBoundGrid);

            for (int i = 0; i < pedidosGridDataBoundGrid.TableDescriptor.Columns.Count; i++)
            {
                pedidosGridDataBoundGrid.TableDescriptor.Columns[i].ReadOnly = false;

                if (i > 0)
                    pedidosGridDataBoundGrid.TableDescriptor.Columns[i].ReadOnly = true;

                pedidosGridDataBoundGrid.TableDescriptor.Columns[i].AllowFilter = true;
                pedidosGridDataBoundGrid.TableDescriptor.Columns[i].AllowSort = true;
                pedidosGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                pedidosGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                pedidosGridDataBoundGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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


            this.pedidosGridDataBoundGrid.SetMetroStyle(metroColor);
            // para permitir editar dados.
            this.pedidosGridDataBoundGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            this.pedidosGridDataBoundGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.pedidosGridDataBoundGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.pedidosGridDataBoundGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            #endregion

        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            // pedidos

            pedidoTextBox.Text = string.Empty;
            pedidosGridDataBoundGrid.DataSource = new List<PedidoGlobus>();
            pedidoTextBox.Focus();

        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            // pedidos
            if (_listaPedido.Where(w => w.Tipo != w.TipoOriginal).Count() != 0)
            {
                string _textoLog = "Alterou Pedido " + pedidoTextBox.Text + " ";

                if (!new PedidoGlobusBO().Gravar(_listaPedido))
                {
                    new Notificacoes.Mensagem("Problemas durante a alteração do pedido." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                foreach (var item in _listaPedido)
                {
                    if (item.Tipo != item.TipoOriginal)
                        _textoLog = _textoLog + "[ Item " + item.Sequencial.ToString() +
                                    " alterado de " + item.TipoOriginal.ToString() +
                                    " para " + item.Tipo.ToString() + "], ";
                }
                _textoLog = _textoLog.Substring(0, _textoLog.Length - 2);

                if (!new LogBO().Gravar(Publicas.MontaLog(_textoLog, tituloLabel.Text)))
                {
                    MessageBox.Show("Problemas durante a alteração do pedido (log)." +
                        Environment.NewLine + Publicas.mensagemDeErro);
                    return;
                }
            }

            limparButton_Click(sender, e);
        }
                
        private void AjustesNfeEPedidos_Shown(object sender, EventArgs e)
        {
            pedidoTextBox.Focus();
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
                pedidoTextBox.Focus();
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

        
        private void pedidosGridDataBoundGrid_TableControlCurrentCellKeyDown(object sender, GridTableControlKeyEventArgs e)
        {
            if (e.Inner.KeyCode == Keys.Enter || e.Inner.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.Inner.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pedidoTextBox.Focus();
            }
        }

        private void pedidoTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(pedidoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe o número do Pedido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                pedidoTextBox.Focus();
                return;
            }

            _listaPedido = new PedidoGlobusBO().Consultar(pedidoTextBox.Text).OrderBy(o => o.Sequencial).ToList();

            pedidosGridDataBoundGrid.DataSource = _listaPedido;

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void pedidoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                pedidosGridDataBoundGrid.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pedidoTextBox.Focus();
            }
        }

        private void pedidoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void pedidoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }
    }
}
