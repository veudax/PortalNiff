using Classes;
using DynamicFilter;
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
    public partial class ResultadoDasCorridas : Form
    {
        public ResultadoDasCorridas()
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

                    valorCurrencyTextBox.PositiveColor = Publicas._fonte;
                    classificacaoGeralCurrencyText.PositiveColor = Publicas._fonte;
                    numeroPeitoCurrencyTextBox.PositiveColor = Publicas._fonte;
                }
            }
            Publicas._mensagemSistema = string.Empty;
            valorCurrencyTextBox.BackGroundColor = codigoTextBox.BackColor;
            classificacaoGeralCurrencyText.BackGroundColor = codigoTextBox.BackColor;
            numeroPeitoCurrencyTextBox.BackGroundColor = codigoTextBox.BackColor;
            gridGroupingControl.DataSource = new List<ParticipanteCorrida>();
        }

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

        Classes.Corridas _corridas;
        List<Classes.ParticipanteCorrida> _listaParticipante;
        int _rowIndex = 0;

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
                gridGroupingControl.Focus();
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

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            codigoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Corridas().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (codigoTextBox.Text.Trim() == "" || codigoTextBox.Text.Trim() == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _corridas = new CorridasBO().Consultar(Convert.ToInt32(codigoTextBox.Text));

            if (!_corridas.Existe)
            {
                new Notificacoes.Mensagem("Corrida não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            if (!_corridas.Ativo)
            {
                new Notificacoes.Mensagem("Corrida está inativa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            descricaoCorridaTextBox.Text = _corridas.Nome;
            dataLabel.Text = _corridas.Data.ToShortDateString();
            valorCurrencyTextBox.DecimalValue = _corridas.Valor;

            try
            {
                _listaParticipante = new ParticipanteCorridaBO().Listar(_corridas.Id);
                    //.Where(w => w.InscricaoPaga).ToList();

                gridGroupingControl.DataSource = _listaParticipante.OrderBy(o => o.TempoLiquido)
                                                                   .OrderBy(o => o.KM)
                                                                   .OrderBy(o => o.Sexo)
                                                                   .ToList();
            }
            catch { }
            gridGroupingControl.Focus();
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridGroupingControl_TableControlCellDoubleClick(object sender, GridTableControlCellClickEventArgs e)
        {
            // abrir a tela de tramites/historicos do chamado

            resultadoPanel.Visible = true;
            numeroPeitoCurrencyTextBox.Focus();
            try
            {
                GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        participanteTextBox.Text = (string)dr["Nome"];
                                                
                        if ((int)dr["ClassificacaoGeral"] != 0)
                            classificacaoGeralCurrencyText.DecimalValue = (int)dr["ClassificacaoGeral"];

                        if ((decimal)dr["TempoBruto"] != 0)
                            tempoBrutoMaskedEditBox.Text = ((decimal)dr["TempoBruto"]).ToString("000000");

                        if ((decimal)dr["TempoLiquido"] != 0)
                            tempoLiquidoMaskedEditBox.Text = ((decimal)dr["TempoLiquido"]).ToString("000000");

                        if ((int)dr["Ritmo"] != 0)
                            ritmoMaskedEditBox.Text = ((int)dr["Ritmo"]).ToString("0000");

                        if ((int)dr["NumeroDePeito"] != 0)
                            numeroPeitoCurrencyTextBox.DecimalValue = (int)dr["NumeroDePeito"];
                    }
                }
            }
            catch { }
            gravarButton.Enabled = true;
        }

        private void ritmoMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Convert.ToInt32(ritmoMaskedEditBox.Text.Substring(3,2)) > 59)
            {
                new Notificacoes.Mensagem("Segundos inválidos.", Publicas.TipoMensagem.Erro).ShowDialog();
                ritmoMaskedEditBox.Focus();
                return;
            }
            if (Convert.ToInt32(ritmoMaskedEditBox.Text.Substring(0, 2)) > 59)
            {
                new Notificacoes.Mensagem("Minutos inválidos.", Publicas.TipoMensagem.Erro).ShowDialog();
                ritmoMaskedEditBox.Focus();
                return;
            }

            gridGroupingControl.DataSource = new List<ParticipanteCorrida>();

            foreach (var item in _listaParticipante.Where(w => w.Nome == participanteTextBox.Text))
            {
                item.ClassificacaoGeral = Convert.ToInt32(classificacaoGeralCurrencyText.DecimalValue);
                item.NumeroDePeito = Convert.ToInt32(numeroPeitoCurrencyTextBox.DecimalValue);

                try
                {
                    item.TempoBruto = Convert.ToDecimal(tempoBrutoMaskedEditBox.ClipText.Trim());
                    item.TempoLiquido = Convert.ToDecimal(tempoLiquidoMaskedEditBox.ClipText.Trim());
                    item.Ritmo = Convert.ToInt32(ritmoMaskedEditBox.ClipText.Trim());
                }
                catch { }
                item.RitmoFormatado = item.Ritmo.ToString("00:00");
                item.TempoLiquidoFormatado = item.TempoLiquido.ToString("00:00:00");
                item.TempoBrutoFormatado = item.TempoBruto.ToString("00:00:00");

            }

            classificacaoGeralCurrencyText.DecimalValue = 0;
            classificacaoGeralCurrencyText.DecimalValue = 0;
            ritmoMaskedEditBox.Text = string.Empty;
            tempoBrutoMaskedEditBox.Text = string.Empty;
            tempoLiquidoMaskedEditBox.Text = string.Empty;

            gridGroupingControl.DataSource = _listaParticipante.OrderBy(o => o.TempoLiquido)
                                                               .OrderBy(o => o.KM)
                                                               .OrderBy(o => o.Sexo)
                                                               .ToList(); 
            gridGroupingControl.Refresh();
            resultadoPanel.Visible = false;
            gridGroupingControl.Focus();
        }

        private void gridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();
            }
            catch { }
        }

        private void gridGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            try
            {
                _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();
            }
            catch { }
        }

        private void ResultadoDasCorridas_Shown(object sender, EventArgs e)
        {
            GridDynamicFilter filter = new GridDynamicFilter();
            GridMetroColors metroColor = new GridMetroColors();

            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.gridGroupingControl);

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.RecordNavigationBar.Label = "Participantes";
            gridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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
        }

        private void classificacaoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                classificacaoGeralCurrencyText.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                classificacaoGeralCurrencyText.Focus();
            }
        }

        private void classificacaoGeralCurrencyText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tempoBrutoMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                numeroPeitoCurrencyTextBox.Focus();
            }
        }

        private void tempoBrutoMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tempoLiquidoMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tempoBrutoMaskedEditBox.Focus();
            }
        }

        private void tempoLiquidoMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ritmoMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tempoBrutoMaskedEditBox.Focus();
            }
        }

        private void ritmoMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tempoLiquidoMaskedEditBox.Focus();
            }
        }

        private void resultadoPanel_VisibleChanged(object sender, EventArgs e)
        {
           
        }

        private void participanteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                classificacaoGeralCurrencyText.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tempoLiquidoMaskedEditBox.Focus();
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
                if (resultadoPanel.Visible)
                    classificacaoGeralCurrencyText.Focus();
                else
                    codigoTextBox.Focus();
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            int i = 1;
            foreach (var item in _listaParticipante.Where(w => w.ClassificacaoGeral !=0).OrderBy(o => o.TempoLiquido))
            {
                item.Classificacao = i;
                if (!new ParticipanteCorridaBO().Gravar(item))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
                i++;
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            _listaParticipante.Clear();
            gridGroupingControl.DataSource = new List<ParticipanteCorrida>();
            codigoTextBox.Text = string.Empty;
            descricaoCorridaTextBox.Text = string.Empty;
            valorCurrencyTextBox.DecimalValue = 0;
            codigoTextBox.Focus();
        }

        private void classificacaoCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void tempoBrutoMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void tempoBrutoMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Convert.ToInt32(tempoBrutoMaskedEditBox.Text.Substring(6, 2)) > 59)
            {
                new Notificacoes.Mensagem("Segundos inválidos.", Publicas.TipoMensagem.Erro).ShowDialog();
                tempoBrutoMaskedEditBox.Focus();
                return;
            }
            if (Convert.ToInt32(tempoBrutoMaskedEditBox.Text.Substring(3, 2)) > 59)
            {
                new Notificacoes.Mensagem("Minutos inválidos.", Publicas.TipoMensagem.Erro).ShowDialog();
                tempoBrutoMaskedEditBox.Focus();
                return;
            }
        }

        private void tempoLiquidoMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Convert.ToInt32(tempoLiquidoMaskedEditBox.Text.Substring(6, 2)) > 59)
            {
                new Notificacoes.Mensagem("Segundos inválidos.", Publicas.TipoMensagem.Erro).ShowDialog();
                tempoLiquidoMaskedEditBox.Focus();
                return;
            }
            if (Convert.ToInt32(tempoLiquidoMaskedEditBox.Text.Substring(3, 2)) > 59)
            {
                new Notificacoes.Mensagem("Minutos inválidos.", Publicas.TipoMensagem.Erro).ShowDialog();
                tempoLiquidoMaskedEditBox.Focus();
                return;
            }
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());            
        }

        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Corridas().ShowDialog();

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

        private void numeroPeitoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                classificacaoGeralCurrencyText.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                numeroPeitoCurrencyTextBox.Focus();
            }
        }

        private void ResultadoDasCorridas_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }
    }
}
