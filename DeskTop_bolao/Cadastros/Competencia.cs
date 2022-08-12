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
{
    public partial class Competencia : Form
    {
        public Competencia()
        {
            InitializeComponent();

            pontuacaoCurrencyTextBox.BackGroundColor = codigoTextBox.BackColor;
            gridGroupingControl.BackColor = codigoTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Competencias _competencias = new Classes.Competencias();
        List<SubCompetencias> _listasSub = new List<SubCompetencias>();
        Classes.Prazos _cicloAvaliacao;
        bool _pesquisa = false;
        int _rowIndexComunicado;

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

        private void Competencia_Shown(object sender, EventArgs e)
        {
            tipoComboBox.Items.AddRange(new object[] { "Técnica", "Comportamental" });

            //VisualStyles and themes
            GridMetroColors metroColor = new GridMetroColors();
            GridDynamicFilter filter = new GridDynamicFilter();
           
            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(gridGroupingControl);

            gridGroupingControl.DataSource = new List<SubCompetencias>();

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.TableControl.CellToolTip.Active = true;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl.RecordNavigationBar.Label = "Questões";

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                gridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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

            gridGroupingControl.SetMetroStyle(metroColor);

            // para permitir editar dados.
            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl.Table.DefaultRecordRowHeight = 40;
            gridGroupingControl.Refresh();
        }

        private void gridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                _rowIndexComunicado = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();
            }
            catch { }
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    subDescricaoTextBox.Text = (string)dr["Descricao"];
                    subDescricaoTextBox.Tag = (int)dr["Id"];
                    pontuacaoCurrencyTextBox.DecimalValue = (decimal)dr["Pontuacao"];
                    subDescricaoTextBox.Focus();
                    subAtivoCheckBox.Checked = (bool)dr["Ativo"];
                    exibirNaAutoAvaliacaoCheckBox.Checked = (bool)dr["ExibeNaAutoAvaliacao"];
                    exibirNaAvaliacaoGestoCheckBox.Checked = (bool)dr["ExibeNaAvaliacaoGestor"];
                    exibirNaAvaliacaoRHCheckBox.Checked = (bool)dr["ExibeNaAvaliacaoRH"];
                }
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[_rowIndexComunicado] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    
                    // não deixar exclui se já estiver em uso
                    if (new AutoAvaliacaoBO().SubCompetenciaEmUso((int)dr["Id"]))
                    {
                        new Notificacoes.Mensagem("Não é possível excluir uma pergunta já utilizada em avaliações.", Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }

                    gridGroupingControl.DataSource = new List<SubCompetencias>();
                    SubCompetencias parcela = new SubCompetencias();

                    foreach (var item in _listasSub.Where(w => w.Id == (int)dr["Id"]))
                    {
                        parcela = item;
                    }

                    _listasSub.Remove(parcela);
                }
                gridGroupingControl.DataSource = _listasSub;
                gridGroupingControl.Refresh();
            }
        }

        private void subCompetenciaButtonAdv_Click(object sender, EventArgs e)
        {
            bool subExiste = false;
            gridGroupingControl.DataSource = new List<SubCompetencias>();

            if (Convert.ToInt32(subDescricaoTextBox.Tag) != 0)
            {
                //foreach (var item in _listasSub.Where(w => w.Descricao == subDescricaoTextBox.Text))
                foreach (var item in _listasSub.Where(w => w.Id == Convert.ToInt32(subDescricaoTextBox.Tag)))
                {
                    subExiste = true;
                    item.Descricao = subDescricaoTextBox.Text;
                    item.Ativo = subAtivoCheckBox.Checked;
                    item.Pontuacao = pontuacaoCurrencyTextBox.DecimalValue;
                    item.ExibeNaAutoAvaliacao = exibirNaAutoAvaliacaoCheckBox.Checked;
                    item.ExibeNaAvaliacaoGestor = exibirNaAvaliacaoGestoCheckBox.Checked;
                    item.ExibeNaAvaliacaoGestor = exibirNaAvaliacaoRHCheckBox.Checked;
                }
            }
            
            if (!subExiste)
            {
                _listasSub.Add(new SubCompetencias()
                {
                    IdCompetencia = Convert.ToInt32(codigoTextBox.Text),
                    Descricao = subDescricaoTextBox.Text,
                    Ativo = subAtivoCheckBox.Checked,
                    Pontuacao = pontuacaoCurrencyTextBox.DecimalValue,
                    ExibeNaAutoAvaliacao = exibirNaAutoAvaliacaoCheckBox.Checked,
                    ExibeNaAvaliacaoGestor = exibirNaAvaliacaoGestoCheckBox.Checked,
                    ExibeNaAvaliacaoRH = exibirNaAvaliacaoRHCheckBox.Checked,
                    Id = _listasSub.Count() + 1
                });
            }

            gridGroupingControl.DataSource = _listasSub;

            subDescricaoTextBox.Focus();
            subDescricaoTextBox.Text = string.Empty;
            pontuacaoCurrencyTextBox.DecimalValue = 1;
            subAtivoCheckBox.Checked = false;
            exibirNaAutoAvaliacaoCheckBox.Checked = true;
            exibirNaAvaliacaoGestoCheckBox.Checked = false;
            exibirNaAvaliacaoRHCheckBox.Checked = false;
            subDescricaoTextBox.Tag = 0;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            ativoCheckBox.Checked = false;
            subDescricaoTextBox.Text = string.Empty;
            pontuacaoCurrencyTextBox.DecimalValue = 1;
            subAtivoCheckBox.Checked = false;
            exibirNaAutoAvaliacaoCheckBox.Checked = false;
            exibirNaAvaliacaoGestoCheckBox.Checked = false;
            exibirNaAvaliacaoRHCheckBox.Checked = false;
            textoAutoAvaliacaoTextBox.Text = string.Empty;
            subDescricaoTextBox.Tag = 0;

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            gridGroupingControl.DataSource = new List<SubCompetencias>();

            codigoTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new CompetenciasBO().Excluir(_competencias))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a descrição.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            _competencias.Id = Convert.ToInt32(codigoTextBox.Text);
            _competencias.Ativo = ativoCheckBox.Checked;
            _competencias.Descricao = nomeTextBox.Text;
            _competencias.Tipo = (tipoComboBox.Text == "Técnica" ? Publicas.TipoCompetencias.Tecnica : Publicas.TipoCompetencias.Comportamental);
            _competencias.TextoExplicativo = textoAutoAvaliacaoTextBox.Text;

            if (_listasSub == null)
                _listasSub = new List<SubCompetencias>();

            if (!new CompetenciasBO().Gravar(_competencias, _listasSub))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new CompetenciasBO().Proximo().ToString();
            tipoComboBox.Focus();
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            codigoTextBox.BorderColor = Publicas._bordaEntrada;
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
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

        private void tipoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;

            if (codigoTextBox.Text == "")
                ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void pontuacaoCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
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

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoComboBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                textoAutoAvaliacaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
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

        private void subDescricaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                textoAutoAvaliacaoTextBox.Focus();
            }
        }

        private void pontuacaoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                subAtivoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                subDescricaoTextBox.Focus();
            }
        }

        private void subAtivoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                exibirNaAutoAvaliacaoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                subDescricaoTextBox.Focus();
            }
        }
        
        private void gridGroupingControl_TableControlCurrentCellKeyDown(object sender, GridTableControlKeyEventArgs e)
        {
            if (e.Inner.KeyCode == Keys.Enter || e.Inner.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.Inner.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                subDescricaoTextBox.Focus();
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
                if (!_pesquisa)
                {
                    new Pesquisas.Competencias().ShowDialog();

                    codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();
                }

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                { 
                    _pesquisa = !_pesquisa;
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _pesquisa = false;
            
            _cicloAvaliacao = new PrazosBO().ConsultarCicloAvaliacao(Convert.ToInt32(DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString()));
            
            _competencias = new CompetenciasBO().Consultar(Convert.ToInt32(codigoTextBox.Text));

            if (_competencias.Existe)
            {
                ativoCheckBox.Checked = _competencias.Ativo;
                nomeTextBox.Text = _competencias.Descricao;
                tipoComboBox.SelectedIndex = (_competencias.Tipo == Publicas.TipoCompetencias.Tecnica ? 0 : 1);
                textoAutoAvaliacaoTextBox.Text = _competencias.TextoExplicativo;

                _listasSub = new CompetenciasBO().ListarSubCompetencias(false, _competencias.Id);
                gridGroupingControl.DataSource = _listasSub;
            }

            if (_competencias.Tipo == Publicas.TipoCompetencias.Comportamental)
            {
                excluirButton.Enabled = !(_cicloAvaliacao.Inicio.Date >= DateTime.Now.Date && _cicloAvaliacao.Inicio.Date <= DateTime.Now.Date);
                gravarButton.Enabled = !(_cicloAvaliacao.Inicio.Date >= DateTime.Now.Date && _cicloAvaliacao.Inicio.Date <= DateTime.Now.Date);
            }
            else
            {
                excluirButton.Enabled = _competencias.Existe;
                gravarButton.Enabled = true;
            }

            if (Publicas._idRetornoPesquisa != 0)
                tipoComboBox.Focus();
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void tipoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
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

        private void excluirButton_Validating(object sender, CancelEventArgs e)
        {
            excluirButton.BackColor = Publicas._botao;
            excluirButton.ForeColor = Publicas._fonteBotao;
        }

        private void ativoCheckBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void pontuacaoCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;
        }

        private void subDescricaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(subDescricaoTextBox.Text))
                gridGroupingControl.Focus();
            else
            {
                foreach (var item in _listasSub.Where(w => w.Descricao == subDescricaoTextBox.Text))
                {
                    subAtivoCheckBox.Checked = item.Ativo;
                    pontuacaoCurrencyTextBox.DecimalValue = item.Pontuacao;
                }
            }
        }

        private void gridGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            try
            {
                _rowIndexComunicado = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();
            }
            catch { }
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (codigoTextBox.Text.Trim() == "")
            {
                if (!_pesquisa)
                {
                    new Pesquisas.Competencias().ShowDialog();

                    codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();
                }

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    _pesquisa = !_pesquisa;
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }

                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void textoAutoAvaliacaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                subDescricaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void exibirNaAutoAvaliacaoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                exibirNaAvaliacaoGestoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                subAtivoCheckBox.Focus();
            }
        }

        private void exibirNaAvaliacaoGestoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                exibirNaAvaliacaoRHCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                exibirNaAutoAvaliacaoCheckBox.Focus();
            }
        }

        private void exibirNaAvaliacaoRHCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                subCompetenciaButtonAdv.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                exibirNaAvaliacaoGestoCheckBox.Focus();
            }
        }

        private void subCompetenciaButtonAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                exibirNaAvaliacaoRHCheckBox.Focus();
            }
        }

        private void subCompetenciaButtonAdv_Enter(object sender, EventArgs e)
        {
            subCompetenciaButtonAdv.BackColor = Publicas._botaoFocado;
            subCompetenciaButtonAdv.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void subCompetenciaButtonAdv_Validating(object sender, CancelEventArgs e)
        {
            subCompetenciaButtonAdv.BackColor = codigoTextBox.BackColor;
            subCompetenciaButtonAdv.ForeColor = Publicas._fonteBotao;

        }

        private void nomeTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }
    }
}
