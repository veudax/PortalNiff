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

namespace Suportte.Operacional
{
    public partial class SetorDaLinha : Form
    {
        public SetorDaLinha()
        {
            InitializeComponent();

            VigenciaDateTimePicker.BackColor = codigoTextBox.BackColor;

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

                    VigenciaDateTimePicker.Value = DateTime.Now;
                    VigenciaDateTimePicker.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Black;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Operacional.Setor _setor;
        Classes.Operacional.Setor _setorlog;
        Classes.Linha _linha;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Operacional.Vigencia> _linhasDaVigencia;
        List<Classes.Operacional.Vigencia> _linhasDaVigenciaLog;

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

        private void SetorDaLinha_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

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

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                codigoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }

        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                VigenciaDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void LinhaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                VigenciaDateTimePicker.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gridGroupingControl1.Focus();
            }
        }

        private void VigenciaDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                LinhaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
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

        private void excluirButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                limparButton.Focus();
            }
        }

        private void buttonAdv1_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                excluirButton.Focus();
            }
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void VigenciaDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
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

        private void ExcluirSetorButton_Enter(object sender, EventArgs e)
        {
            ExcluirSetorButton.BackColor = Publicas._botaoFocado;
            ExcluirSetorButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void ExcluirSetorButton_Validating(object sender, CancelEventArgs e)
        {
            ExcluirSetorButton.BackColor = Publicas._botao;
            ExcluirSetorButton.ForeColor = Publicas._fonteBotao;
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

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            codigoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(codigoTextBox.Text))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;

                new Pesquisas.SetorDasLinhas().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _setor = new OperacionalBO().ConsultaSetor(Convert.ToInt32(codigoTextBox.Text), _empresa.IdEmpresa);
            _setorlog = new OperacionalBO().ConsultaSetor(Convert.ToInt32(codigoTextBox.Text), _empresa.IdEmpresa);

            if (_setor.Existe)
            {
                ExcluirSetorButton.Enabled = true;
                excluirButton.Enabled = false;
                nomeTextBox.Text = _setor.Descricao;
                ativoCheckBox.Checked = _setor.Ativo;
                VigenciaDateTimePicker.Focus();
            }
        }

        private void VigenciaDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            VigenciaDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                PesquisaVigenciaButton.Enabled = false;
                return;
            }

            if (VigenciaDateTimePicker.Value == DateTime.MinValue)
            {
                new Pesquisas.SetorDasLinhas().ShowDialog();

                if (Publicas._codigoRetornoPesquisa.ToString() == "")
                {
                    VigenciaDateTimePicker.Focus();
                    return;
                }
                VigenciaDateTimePicker.Value = Convert.ToDateTime(Publicas._codigoRetornoPesquisa.ToString());
            }

            _linhasDaVigencia = new List<Classes.Operacional.Vigencia>();

            if (_setor.Existe)
            {
                _linhasDaVigencia = new OperacionalBO().ListarLinhasVigencias(_setor.Id, VigenciaDateTimePicker.Value);
                _linhasDaVigenciaLog = new OperacionalBO().ListarLinhasVigencias(_setor.Id, VigenciaDateTimePicker.Value);
            }
            
            gridGroupingControl1.DataSource = _linhasDaVigencia;
            gravarButton.Enabled = true;

            excluirButton.Enabled = _linhasDaVigencia.Count() != 0;
        }

        private void LinhaTextBox_Validating(object sender, CancelEventArgs e)
        {
            LinhaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                PesquisaLinhaButton.Enabled = false;
                return;
            }
            
            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                return;
            }

            Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (string.IsNullOrEmpty(LinhaTextBox.Text))
            {
                new Pesquisas.Linhas().ShowDialog();

                LinhaTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (Publicas._codigoRetornoPesquisa.ToString() == "")
                {
                    LinhaTextBox.Focus();
                    return;
                }
            }

            _linha = new LinhaBO().Consultar(_empresa.CodigoEmpresaGlobus, LinhaTextBox.Text);

            if (!_linha.Existe)
            {
                new Notificacoes.Mensagem("Linha não está cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                LinhaTextBox.Focus();
                return;
            }

            if (!_linha.Ativo)
            {
                new Notificacoes.Mensagem("Linha não está Ativa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                LinhaTextBox.Focus();
                return;
            }

            NomeLinhaTextBox.Text = _linha.Nome;

            
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            empresaComboBoxAdv.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new OperacionalBO().ProximoCodigoSetorEmpresa(_empresa.IdEmpresa).ToString();
            ativoCheckBox.Focus();
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoTextBox.Text))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;

                new Pesquisas.SetorDasLinhas().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }

                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void PesquisaLinhaButton_Click(object sender, EventArgs e)
        {
            Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (string.IsNullOrEmpty(LinhaTextBox.Text))
            {
                new Pesquisas.Linhas().ShowDialog();

                LinhaTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (Publicas._codigoRetornoPesquisa.ToString() == "")
                {
                    LinhaTextBox.Focus();
                    return;
                }

                LinhaTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void PesquisaVigenciaButton_Click(object sender, EventArgs e)
        {
            Publicas._idRetornoPesquisa = _setor.Id;

            new Pesquisas.VigenciasDoSetor().ShowDialog();

            if (Publicas._codigoRetornoPesquisa.ToString() == "")
            {
                VigenciaDateTimePicker.Focus();
                return;
            }

            VigenciaDateTimePicker.Value = Convert.ToDateTime(Publicas._codigoRetornoPesquisa.ToString());
            VigenciaDateTimePicker_Validating(sender, new CancelEventArgs());
            LinhaTextBox.Focus();
            
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            gridGroupingControl1.DataSource = new List<Classes.Operacional.Vigencia>();
            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            LinhaTextBox.Text = string.Empty;
            NomeLinhaTextBox.Text = string.Empty;
            codigoTextBox.Focus();
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o nome da descrição.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            if (_linhasDaVigencia == null || _linhasDaVigencia.Count() == 0)
            {
                new Notificacoes.Mensagem("Informe as linhas para a vigência.", Publicas.TipoMensagem.Alerta).ShowDialog();
                VigenciaDateTimePicker.Focus();
                return;
            }

            _setor.Ativo = ativoCheckBox.Checked;
            _setor.Codigo = Convert.ToInt32(codigoTextBox.Text);
            _setor.Descricao = nomeTextBox.Text;
            _setor.IdEmpresa = _empresa.IdEmpresa;

            if (!new OperacionalBO().Grava(_setor, _linhasDaVigencia ))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Tela = "Cadastro de Setor da linha";

            _log.Descricao = (_setor.Existe ? "Alterou " : "Incluiu ") + " o setor " + codigoTextBox.Text + " da empresa "
                + empresaComboBoxAdv.Text + " ";

            if (_setor.Existe)
            {
                _log.Descricao = _log.Descricao +
                (_setor.Descricao == _setorlog.Descricao ? "" : " [Descricao] de " + _setorlog.Descricao + " para " + _setor.Descricao + "") +
                (_setor.Ativo == _setorlog.Ativo ? "" : " [Ativo] de " + _setorlog.Ativo.ToString() + " para " + _setor.Ativo.ToString() + "") + " ";
            }

            _log.Descricao = _log.Descricao + "Vigência " + VigenciaDateTimePicker.Value.ToShortDateString();

            foreach (var item in _linhasDaVigencia)
            {
                if (_linhasDaVigenciaLog.Where(w => w.CodigoLinha == item.CodigoLinha).Count() == 0)
                {
                    _log.Descricao = _log.Descricao + " incluiu a linha " + item.CodigoLinha + " tem cobrador " + (TemCobradorCheckBox.Checked ? "SIM" : "NÃO");
                }
            }

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão da vigência?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Tela = "Cadastro de Setor da linha";
            _log.Descricao = "Excluiu a vigência " + VigenciaDateTimePicker.Value.ToShortDateString() + 
                " do setor " + codigoTextBox.Text + " da empresa " + empresaComboBoxAdv.Text + " com as Linhas: [ ";

            foreach (var linha in _linhasDaVigenciaLog.Where(g => g.Data == VigenciaDateTimePicker.Value))
            {
                _log.Descricao = _log.Descricao + linha.CodigoLinha + "], ";
            }

            _log.Descricao = _log.Descricao.Substring(0, _log.Descricao.Length - 2);

            if (!new OperacionalBO().ExcluirVigencia(VigenciaDateTimePicker.Value))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void LinhaTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaLinhaButton.Enabled = string.IsNullOrEmpty(LinhaTextBox.Text.Trim());
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

        private void ExcluirSetorButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão do setor?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;
            
            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu o setor " + codigoTextBox.Text + " da empresa " + empresaComboBoxAdv.Text + " com as vigências: " ;

            foreach (var item in _linhasDaVigenciaLog.GroupBy(g => g.Data))
            {
                _log.Descricao = _log.Descricao + item.Key.ToShortDateString() + " com as linhas: [";

                foreach (var linha in _linhasDaVigenciaLog.Where(g => g.Data == item.Key))
                {
                    _log.Descricao = _log.Descricao + linha.CodigoLinha + "], ";
                }
            }

            _log.Descricao = _log.Descricao.Substring(0, _log.Descricao.Length - 2);

            _log.Tela = "Cadastro de Setor da linha";

            if (!new OperacionalBO().ExcluirSetor(_setor.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void excluirLinhaDaVigênciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão da linha?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            try
            {
                Classes.Operacional.Vigencia _linha = new Classes.Operacional.Vigencia();
                int i = gridGroupingControl1.Table.CurrentRecord.GetRecord().GetRowIndex();
                GridRecordRow _registro = gridGroupingControl1.Table.DisplayElements[i] as GridRecordRow;

                if (_registro != null)
                {
                    Record dr = _registro.GetRecord() as Record;

                    if (!new OperacionalBO().ExcluirLinhaDaVigencia(Convert.ToInt32(dr["Id"].ToString())))
                    {
                        new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }

                    gridGroupingControl1.DataSource = new List<Classes.Operacional.Vigencia>();

                    foreach (var item in _linhasDaVigencia.Where(w => w.Id == Convert.ToInt32(dr["Id"].ToString())))
                    {
                        _linha = item;
                    }

                    _linhasDaVigencia.Remove(_linha);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu a linha " + _linha.CodigoLinha + " da vigência " + VigenciaDateTimePicker.Value.ToShortDateString() + 
                        " do setor " + codigoTextBox.Text + " da empresa " + empresaComboBoxAdv.Text;
                    _log.Tela = "Cadastro de Setor da linha";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _linhasDaVigencia;
            }
            catch { }

        }

        private void TemCobradorCheckBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            gridGroupingControl1.DataSource = new List<Classes.Operacional.Vigencia>();

            if (_linhasDaVigencia.Where(w => w.CodigoInternoLinha == _linha.Id).Count() == 0)
            {
                _linhasDaVigencia.Add(new Classes.Operacional.Vigencia()
                {
                    Data = VigenciaDateTimePicker.Value,
                    IdSetor = (_setor.Existe ? _setor.Id : 0),
                    CodigoLinha = _linha.Codigo,
                    NomeLinha = _linha.Nome,
                    CodigoInternoLinha = _linha.Id,
                    TemCobrador = TemCobradorCheckBox.Checked
                });
            }
            else
            {
                foreach (var item in _linhasDaVigencia.Where(w => w.CodigoInternoLinha == _linha.Id))
                {
                    item.TemCobrador = TemCobradorCheckBox.Checked;
                }
            }

            NomeLinhaTextBox.Text = string.Empty;
            LinhaTextBox.Text = string.Empty;
            LinhaTextBox.Focus();

            gridGroupingControl1.DataSource = _linhasDaVigencia;
        }

        private void TemCobradorCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                LinhaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                LinhaTextBox.Focus();
            }
        }
    }
}
