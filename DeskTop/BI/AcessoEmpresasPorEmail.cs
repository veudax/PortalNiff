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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.BI
{
    public partial class AcessoEmpresasPorEmail : Form
    {
        public AcessoEmpresasPorEmail()
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

        Classes.PowerBI.EmailDeAcesso _emails;
        List<Classes.PowerBI.EmpresasAutorizadas> _listaEmpresasAutorizadas;

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

        private void EmailTextBox_KeyDown(object sender, KeyEventArgs e)
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

        private void EmailTextBox_Enter(object sender, EventArgs e)
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

        private void EmailTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (EmailTextBox.Text.Trim() == "")
            {
                new Pesquisas.EmailBI().ShowDialog();

                EmailTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (EmailTextBox.Text.Trim() == "" || EmailTextBox.Text.Trim() == "0")
                {
                    EmailTextBox.Text = string.Empty;
                    EmailTextBox.Focus();
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!reg.IsMatch(EmailTextBox.Text))
                {
                    new Notificacoes.Mensagem("E-mail inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    EmailTextBox.Focus();
                    return;
                }
            }

            _emails = new PowerBIBO().Consultar(EmailTextBox.Text);

            if (!_emails.Existe)
            {
                new Notificacoes.Mensagem("E-mail não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                EmailTextBox.Focus();
                return;
            }

            if (!_emails.Ativo)
            {
                new Notificacoes.Mensagem("E-mail não está ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                EmailTextBox.Focus();
                return;
            }

            _listaEmpresasAutorizadas = new PowerBIBO().Listar(_emails.Id);

            gravarButton.Enabled = true;
            gridGroupingControl1.DataSource = _listaEmpresasAutorizadas.OrderBy(o => o.CodigoEmpresa).ToList();

        }

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            limparButton.BackColor = Publicas._botao;
            limparButton.ForeColor = Publicas._fonteBotao;
        }
                
        private void AcessoEmpresasPorEmail_Shown(object sender, EventArgs e)
        {
            GridMetroColors metroColor = new GridMetroColors();

            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl1.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].AllowSort = true;
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
                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaEmpresasAutorizadas.Where(w => w.Selecionado).Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhuma empresa selecionada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                gridGroupingControl1.Focus();
                return;
            }

            _listaEmpresasAutorizadas.ForEach(u => u.IdEmail = _emails.Id);

            if (!new PowerBIBO().Gravar(_listaEmpresasAutorizadas))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, new EventArgs());
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            EmailTextBox.Text = string.Empty;
            EmailTextBox.Focus();

            gridGroupingControl1.DataSource = new List<PowerBI.EmpresasAutorizadas>();
            gravarButton.Enabled = false;
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(EmailTextBox.Text.Trim());
        }

        private void gridGroupingControl1_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex()] as GridRecordRow;

            bool marcado = false;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    marcado = (bool)dr["Selecionado"];

                    foreach (var item in _listaEmpresasAutorizadas.Where(w => w.CodigoEmpresa == (string)dr["CodigoEmpresa"]))
                    {
                        if (!marcado)
                            item.Selecionado = false;
                        else
                            item.Selecionado = true;
                    }
                }
            }
        }
    }
}
