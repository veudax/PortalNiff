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

namespace Suportte.Financeiro
{
    public partial class CopiaColunasETipos : Form
    {
        public CopiaColunasETipos()
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
                }
            }

            Publicas._mensagemSistema = string.Empty;
        }

        #region Atributos
        public Classes.Empresa _empresa;
        Classes.Empresa _empresaDestino;
        List<Classes.Empresa> _listaEmpresas;
        public Classes.Financeiro.Bancos _banco;
        Classes.Financeiro.Bancos _bancoDestino;

        public List<Classes.Financeiro.ColunasDoBanco> _lista;
        public List<Classes.Financeiro.ColunasDoBanco> _listaDestino;

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

        private void CopiaColunasETipos_Shown(object sender, EventArgs e)
        {
            _lista.ForEach(u => { u.Selecionado = false; u.Existe = false; u.TipoExiste = false; });

            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";

            EmpresaDestinoComboBox.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            EmpresaDestinoComboBox.DisplayMember = "CodigoeNome";
            EmpresaDestinoComboBox.Focus();

            GridMetroColors metroColor = new GridMetroColors();

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
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

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
            {
                empresaComboBoxAdv.SelectedIndex = i;
                if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                    break;
            }

            codigoTextBox.Text = _banco.Codigo.ToString();
            nomeTextBox.Text = _banco.Nome;

            gridGroupingControl.DataSource = _lista;
            gridGroupingControl.Table.ExpandAllGroups();

        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            CopiaColunasETipos_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                codigoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void CopiaColunasETipos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                Publicas.AbrirFerramentaDeCapitura();
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            CopiaColunasETipos_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EmpresaDestinoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void EmpresaDestinoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            CopiaColunasETipos_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CodigoDestinoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void CodigoDestinoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            CopiaColunasETipos_KeyDown(sender, e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EmpresaDestinoComboBox.Focus();
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

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void gravarButton_Enter(object sender, EventArgs e)
        {
            gravarButton.BackColor = Publicas._botaoFocado;
            gravarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void EmpresaDestinoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == EmpresaDestinoComboBox.Text))
            {
                _empresaDestino = item;
            }
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void CodigoDestinoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (CodigoDestinoTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = _empresaDestino.IdEmpresa;
                new Pesquisas.Bancos().ShowDialog();

                CodigoDestinoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CodigoDestinoTextBox.Text.Trim() == "" || CodigoDestinoTextBox.Text.Trim() == "0")
                {
                    CodigoDestinoTextBox.Text = string.Empty;
                    CodigoDestinoTextBox.Focus();
                    return;
                }
            }

            _bancoDestino = new FinanceiroBO().ConsultarBancos(Convert.ToInt32(CodigoDestinoTextBox.Text), _empresaDestino.IdEmpresa);

            if (!_bancoDestino.Existe)
            {
                new Notificacoes.Mensagem("Banco não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoDestinoTextBox.Focus();
                return;
            }

            if (!_bancoDestino.Ativo)
            {
                new Notificacoes.Mensagem("Banco está inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoDestinoTextBox.Focus();
                return;
            }

            if (_bancoDestino.Codigo == _banco.Codigo && _banco.IdEmpresa == _bancoDestino.IdEmpresa)
            {
                new Notificacoes.Mensagem("Banco destino deve ser diferente do banco origem.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoDestinoTextBox.Focus();
                return;
            }

            _listaDestino = new FinanceiroBO().ListarColunasDoBanco(_bancoDestino.Id, false);

            if (_listaDestino.Count() != 0)
            {
                if (new Notificacoes.Mensagem("Banco destino Já possui tipos associados."
                    + Environment.NewLine + " Deseja continuar com a copia?", Publicas.TipoMensagem.Alerta).ShowDialog() == DialogResult.No)
                {
                    CodigoDestinoTextBox.Focus();
                    return;
                }

                foreach (var item in _lista) // Verifica se já existe
                {
                    foreach (var itemD in _listaDestino.Where(w => w.IdColuna == item.IdColuna))
                    {
                        // só copia o coluna que não está cadastrado.  para não duplicar as colunas
                        item.Id = itemD.Id;
                        item.Existe = true;
                    }

                    foreach (var itemD in _listaDestino.Where(w => w.IdColuna == item.IdColuna && w.TipoCodigo == item.TipoCodigo))
                    {
                        // só copia o tipo que não está cadastrado. para não duplicar os tipos
                        item.IdAssociado = itemD.IdAssociado;
                        item.Selecionado = true;
                        item.TipoExiste = true;
                    }
                }
            }

            if (_lista.Where(w => w.Selecionado).Count() != 0)
            {
                gridGroupingControl.DataSource = new List<Classes.Financeiro.ColunasDoBanco>();
                gridGroupingControl.DataSource = _lista;
                gridGroupingControl.Table.ExpandAllGroups();
            }

            NomeDestinoTextBox.Text = _bancoDestino.Nome;

            gravarButton.Enabled = true;
        }

        private void PesquisaDestinoButton_Click(object sender, EventArgs e)
        {
            if (CodigoDestinoTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = _empresaDestino.IdEmpresa;
                new Pesquisas.Bancos().ShowDialog();

                CodigoDestinoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CodigoDestinoTextBox.Text.Trim() == "" || CodigoDestinoTextBox.Text.Trim() == "0")
                {
                    CodigoDestinoTextBox.Text = string.Empty;
                    CodigoDestinoTextBox.Focus();
                    return;
                }

                CodigoDestinoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Copiou do banco " + codigoTextBox.Text + " " + nomeTextBox.Text + " da empresa " + empresaComboBoxAdv.Text +
                "para o banco " + CodigoDestinoTextBox.Text + " " + NomeDestinoTextBox.Text + " da empresa " + EmpresaDestinoComboBox.Text;

            if (_bancoDestino == null)
            {
                new Notificacoes.Mensagem("Informe o Banco destino", Publicas.TipoMensagem.Alerta).ShowDialog();
                CodigoDestinoTextBox.Focus();
                return;
            }

            _lista.ForEach(u => u.IdBanco = _bancoDestino.Id);

            foreach (var item in _lista.Where(w => w.Selecionado))
            {
                foreach (var itemD in _listaDestino.Where(w => w.IdColuna == item.IdColuna))
                {
                    // só copia o coluna que não está cadastrado.  para não duplicar as colunas
                    item.Id = itemD.Id;
                    item.Existe = true;
                }

                foreach (var itemD in _listaDestino.Where(w => w.IdColuna == item.IdColuna && w.TipoCodigo == item.TipoCodigo))
                {
                    // só copia o tipo que não está cadastrado. para não duplicar os tipos
                    item.IdAssociado = itemD.IdAssociado;
                    item.TipoExiste = true;
                }
            }

            string _descricao = "";
            foreach (var item in _lista.GroupBy(g => new { g.IdColuna, g.Ativo, g.Nome, g.Selecionado })
                                       .Where(w => w.Key.Selecionado))
            {
                _descricao = " Coluna " + item.Key.Nome;

                foreach (var itemG in _lista.GroupBy(g => new { g.TipoCodigo, g.IdColuna, g.TipoNome, g.Selecionado })
                                            .Where(w => w.Key.IdColuna == item.Key.IdColuna && w.Key.Selecionado))
                {
                    if (!_descricao.Contains("Incluido o "))
                        _descricao = _descricao + " Incluido o Tipo ";
                    _descricao = _descricao + "[" + itemG.Key.TipoCodigo + " " + itemG.Key.TipoNome + "] ";
                }
            }

            if (!new FinanceiroBO().GravarBancos(_bancoDestino, _lista.Where(w => w.Selecionado).ToList()))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }
            
            _log.Descricao = _log.Descricao + _descricao;
            _log.Tela = "Financeiro - Cadastros - Bancos";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            if (new Notificacoes.Mensagem("Copia finalizada com sucesso." + Environment.NewLine + 
                " Deseja copiar para outros bancos ?", Publicas.TipoMensagem.Sucesso).ShowDialog() == DialogResult.No)
                Close();
            else
            {
                CodigoDestinoTextBox.Text = string.Empty;
                NomeDestinoTextBox.Text = string.Empty;
                CodigoDestinoTextBox.Focus();
            }
        }

        private void gridGroupingControl_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            { // buscar da empresa do usuario
                GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;
                Record dr = null;

                if (rec != null)
                {
                    dr = rec.GetRecord() as Record;

                    if ((bool)dr["TipoExiste"])
                        e.Style.TextColor = Color.Gray;
                }
            }
            catch { }
        }
    }
}
