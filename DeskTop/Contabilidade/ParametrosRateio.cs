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
    public partial class ParametrosRateio : Form
    {
        public ParametrosRateio()
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

        Classes.Empresa _empresa;
        Classes.RateioBeneficios.PlanoContabil _plano;
        Classes.CentroDeCustoContabil _custos;
        Classes.RateioBeneficios.Parametros _param;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasAutorizadas;
        List<Classes.RateioBeneficios.Custos> _listaCustosParametro;
        List<Classes.RateioBeneficios.Custos> _listaCustosParametroLog;

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

        private void ParametrosRateio_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);
            _listaEmpresasAutorizadas = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._idUsuario);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

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
            this.gridGroupingControl1.Table.DefaultRecordRowHeight = 22;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PlanoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void PlanoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                LoteTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PlanoTextBox.Focus();
            }
        }

        private void LoteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CustosTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void CustosTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                proximoButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                LoteTextBox.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gridGroupingControl1.Focus();
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
                LimparButton.Focus();
            }
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void PlanoTextBox_Enter(object sender, EventArgs e)
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
            LimparButton.BackColor = Publicas._botaoFocado;
            LimparButton.ForeColor = Publicas._fonteBotaoFocado;
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
            LimparButton.BackColor = Publicas._botao;
            LimparButton.ForeColor = Publicas._fonteBotao;
        }

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
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

            if (_listaEmpresasAutorizadas.Where(w => w.IdEmpresa == _empresa.IdEmpresa && w.EmpresaAutoriza).Count() == 0)
            {
                new Notificacoes.Mensagem("Usuário não autorizado para está empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }
        }

        private void PlanoTextBox_Validating(object sender, CancelEventArgs e)
        {
            PlanoTextBox.BorderColor = Publicas._bordaSaida;
            
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
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

            _param = new RateioBeneficioBO().ConsultarParametro(_empresa.IdEmpresa, _plano.NumeroPlano);

            List<RateioBeneficios.Parametros> _listaParametrosDaEmpresa = new RateioBeneficioBO().Listar(_empresa.IdEmpresa, true);


            if (_param.Existe)
            {
                LoteTextBox.Text = _param.Lote;
                ativoCheckBox.Checked = _param.Ativo;
                IgnorarFuncoesCheckBox.Checked = _param.IgnorarFuncoes;
                CodigosAprendizesTextBox.Text = _param.CodigoFuncoes;

                RegraVTCheckBox.Checked = _param.RegraEspecificaVT;
                ContasCestaBasicaTextBox.Text = _param.CodigoContaCestaBasica;
                ContasConvenioMedicoTextBox.Text = _param.CodigoContaConvenioMedico;
                ContasConvenioOdotologicoTextBox.Text = _param.CodigoContaConvenioOdontologio;
                ContasValeReceicaoTextBox.Text = _param.CodigoContaValeRefeicao;
                ContasValeTransporteTextBox.Text = _param.CodigoContaValeTransporte;
                EventosValeTransporteTextBoxExt.Text = _param.CodigoEventosValeTransporte;

                RegraConveniosCheckBox.Checked = _param.RegraEspecificaConvenios;
                ConsiderarFuncComConvenioMedicoCheckBox.Checked = _param.IgnorarFuncionarioSemConvenioMedico;
                ConsiderarFuncComConvenioOdontologicoCheckBox.Checked = _param.IgnorarFuncionarioSemConvenioOdontologico;

                HistoricoTextBox.Text = _param.HistoricoPadrao;

                _listaCustosParametro = new RateioBeneficioBO().ListarCustosDoParametro(_param.Id);

                gridGroupingControl1.DataSource = _listaCustosParametro;
            }
            else
            if (_listaParametrosDaEmpresa.Count() != 0)
            {
                new Notificacoes.Mensagem("Existe um plano contábil ativo para esta empresa." + Environment.NewLine +
                    "Só é permitido um plano por empresa.", 
                    Publicas.TipoMensagem.Alerta).ShowDialog();
                PlanoTextBox.Focus();
                return;
            }

            if (HistoricoTextBox.Text == "")
                HistoricoTextBox.Text = "Rateio por Centro de Custo";

            gravarButton.Enabled = true;
            excluirButton.Enabled = _param.Existe;
        }

        private void CustosTextBox_Validating(object sender, CancelEventArgs e)
        {
            CustosTextBox.BorderColor = Publicas._bordaSaida;

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

            if (CustosTextBox.Text.Trim() == "")
            {
                Publicas._codigoRetornoPesquisa = _empresa.CodigoEmpresaGlobus;
                new Pesquisas.CentroDeCustoContabil().ShowDialog();

                CustosTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CustosTextBox.Text.Trim() == "" || CustosTextBox.Text == "0")
                {
                    CustosTextBox.Text = string.Empty;
                    CustosTextBox.Focus();
                    return;
                }
            }

            _custos = new CentroDeCustoContabilBO().Consultar(Convert.ToInt32(CustosTextBox.Text), _plano.NumeroPlano);

            if (!_custos.Existe)
            {
                new Notificacoes.Mensagem("Centro de custo não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CustosTextBox.Focus();
                return;
            }

            if (!_custos.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Centro de custo não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CustosTextBox.Focus();
                return;
            }

            NomeCustoTextBox.Text = _custos.Classificador + " " +  _custos.Descricao;

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

        private void proximoButton_Click(object sender, EventArgs e)
        {
            if (_listaCustosParametro == null)
                _listaCustosParametro = new List<RateioBeneficios.Custos>();
            
            if (_listaCustosParametro.Where(w => w.CodigoCusto == _custos.Codigo).Count() == 0)
            {
                _listaCustosParametro.Add(new RateioBeneficios.Custos() { CodigoCusto = _custos.Codigo, Id = _param.Id, Nome = _custos.Classificador + " " + _custos.Descricao });
            }

            gridGroupingControl1.DataSource = new List<RateioBeneficios.Custos>();
            gridGroupingControl1.DataSource = _listaCustosParametro;

            CustosTextBox.Text = string.Empty;
            NomeCustoTextBox.Text = string.Empty;
            CustosTextBox.Focus();
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlanoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o plano.", Publicas.TipoMensagem.Alerta).ShowDialog();
                PlanoTextBox.Focus();
                return;
            }

            if (_listaCustosParametro.Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhum centro de custo associado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }
            if (_param == null)
                _param = new RateioBeneficios.Parametros();


            _param.Ativo = ativoCheckBox.Checked;
            _param.IdEmpresa = _empresa.IdEmpresa;
            _param.NumeroPlano = Convert.ToInt32(PlanoTextBox.Text);
            _param.Lote = LoteTextBox.Text;
            _param.IgnorarFuncoes = IgnorarFuncoesCheckBox.Checked;
            _param.CodigoFuncoes = CodigosAprendizesTextBox.Text;

            _param.CodigoContaCestaBasica = ContasCestaBasicaTextBox.Text ;
            _param.CodigoContaConvenioMedico = ContasConvenioMedicoTextBox.Text ;
            _param.CodigoContaConvenioOdontologio = ContasConvenioOdotologicoTextBox.Text ;
            _param.CodigoContaValeRefeicao = ContasValeReceicaoTextBox.Text ;
            _param.CodigoContaValeTransporte = ContasValeTransporteTextBox.Text ;
            _param.CodigoEventosValeTransporte = EventosValeTransporteTextBoxExt.Text;
            _param.RegraEspecificaVT = RegraVTCheckBox.Checked;
            _param.RegraEspecificaConvenios = RegraConveniosCheckBox.Checked;
            _param.IgnorarFuncionarioSemConvenioMedico = ConsiderarFuncComConvenioMedicoCheckBox.Checked;
            _param.IgnorarFuncionarioSemConvenioOdontologico = ConsiderarFuncComConvenioOdontologicoCheckBox.Checked;
            _param.HistoricoPadrao = HistoricoTextBox.Text;

            if (!new RateioBeneficioBO().Gravar(_param, _listaCustosParametro))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (_param.Existe ? "Alterou" : "Incluiu") + " o parametro da empresa " + empresaComboBoxAdv.Text +
                  (_param.NumeroPlano.ToString() == PlanoTextBox.Text ? "" : " [NumeroPlano] de " + _param.NumeroPlano + " para " + PlanoTextBox.Text) +
                  (_param.Ativo == ativoCheckBox.Checked ? "" : " [Ativo] de " + _param.Ativo + " para " + ativoCheckBox.Checked) +
                  (_param.Lote == LoteTextBox.Text ? "" : " [Lote] de " + _param.Lote + " para " + LoteTextBox.Text) +
                  (_param.IgnorarFuncoes == IgnorarFuncoesCheckBox.Checked ? "" : " [Ignorar Funções] de " + _param.IgnorarFuncoes + " para " + IgnorarFuncoesCheckBox.Checked) +
                  (_param.CodigoFuncoes == CodigosAprendizesTextBox.Text ? "" : " [Codigos das Funções] de " + _param.CodigoFuncoes + " para " + CodigosAprendizesTextBox.Text) +
                  (_param.HistoricoPadrao == HistoricoTextBox.Text ? "" : " [Historico] de " + _param.HistoricoPadrao + " para " + HistoricoTextBox.Text) +
                  (_param.RegraEspecificaVT == RegraVTCheckBox.Checked ? "" : " [Regras VT] de " + _param.RegraEspecificaVT + " para " + RegraVTCheckBox.Checked) +
                  (_param.CodigoContaValeTransporte == ContasValeTransporteTextBox.Text ? "" : " [Contas CTB VT] de " + _param.CodigoContaValeTransporte + " para " + ContasValeTransporteTextBox.Text) +
                  (_param.CodigoEventosValeTransporte == EventosValeTransporteTextBoxExt.Text ? "" : " [Eventos VT] de " + _param.CodigoEventosValeTransporte + " para " + EventosValeTransporteTextBoxExt.Text) +

                  (_param.RegraEspecificaConvenios == RegraConveniosCheckBox.Checked ? "" : " [Regras Convenio] de " + _param.RegraEspecificaConvenios + " para " + RegraConveniosCheckBox.Checked) +
                  (_param.CodigoContaConvenioMedico == ContasConvenioMedicoTextBox.Text ? "" : " [Contas Convenio Medico] de " + _param.CodigoContaConvenioMedico + " para " + ContasConvenioMedicoTextBox.Text) +
                  (_param.IgnorarFuncionarioSemConvenioMedico == ConsiderarFuncComConvenioMedicoCheckBox.Checked ? "" : " [Ignorar Convenio Medico] de " + _param.IgnorarFuncionarioSemConvenioMedico + " para " + ConsiderarFuncComConvenioMedicoCheckBox.Checked) +

                  (_param.CodigoContaConvenioOdontologio == ContasConvenioOdotologicoTextBox.Text ? "" : " [Contas Convenio Odontologico] de " + _param.CodigoContaConvenioOdontologio + " para " + ContasConvenioOdotologicoTextBox.Text) +
                  (_param.IgnorarFuncionarioSemConvenioOdontologico == ConsiderarFuncComConvenioMedicoCheckBox.Checked ? "" : " [Ignorar Convenio Odontologico] de " + _param.IgnorarFuncionarioSemConvenioOdontologico + " para " + ConsiderarFuncComConvenioMedicoCheckBox.Checked) 
                  
                  ;

            string _descricao = "";

            foreach (var item in _listaCustosParametro)
            {
                try
                {
                    if (_listaCustosParametroLog.Where(w => w.CodigoCusto == item.CodigoCusto).Count() == 0)
                    {
                        _descricao = _descricao + " Incluido o centro de custo " +
                            item.CodigoCusto + " " + item.Nome;
                    }
                }
                catch
                {
                    _descricao = _descricao + " Incluido o centro de custo " +
                            item.CodigoCusto + " " + item.Nome;
                }
            }

            _log.Descricao = _log.Descricao + _descricao;
            _log.Tela = "Contabilidade - Cadastros - Parametros";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            LimparButton_Click(sender, e);
        }

        private void LimparButton_Click(object sender, EventArgs e)
        {
            gridGroupingControl1.DataSource = new List<RateioBeneficios.Custos>();
            PlanoTextBox.Text = string.Empty;
            NomePlanoTextBox.Text = string.Empty;
            LoteTextBox.Text = string.Empty;
            ativoCheckBox.Checked = false;
            CustosTextBox.Text = string.Empty;
            NomeCustoTextBox.Text = string.Empty;
            IgnorarFuncoesCheckBox.Checked = false;
            CodigosAprendizesTextBox.Text = string.Empty;

            RegraVTCheckBox.Checked = false;
            ContasCestaBasicaTextBox.Text = string.Empty;
            ContasConvenioMedicoTextBox.Text = string.Empty;
            ContasConvenioOdotologicoTextBox.Text = string.Empty;
            ContasValeReceicaoTextBox.Text = string.Empty;
            ContasValeTransporteTextBox.Text = string.Empty;
            EventosValeTransporteTextBoxExt.Text = string.Empty;
            HistoricoTextBox.Text = string.Empty;

            RegraConveniosCheckBox.Checked = false;
            ConsiderarFuncComConvenioOdontologicoCheckBox.Checked = false;
            ConsiderarFuncComConvenioMedicoCheckBox.Checked = false;

            PlanoTextBox.Focus();

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new RateioBeneficioBO().ExcluirParametros(_param.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu o parametro da empresa " + empresaComboBoxAdv.Text + " plano " + PlanoTextBox.Text;

            string _descricao = "";

            foreach (var item in _listaCustosParametroLog)
            {
                _descricao = _descricao + " Excluido o centro de custo " + item.CodigoCusto + " " + item.Nome;
            }

            _log.Tela = "Contabilidade - Cadastros - Parametros";
            _log.Descricao = _log.Descricao + _descricao;

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            LimparButton_Click(sender, e);
        }

        private void ParametrosRateio_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }

        private void PesquisaPlanoButton_Click(object sender, EventArgs e)
        {
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

                PlanoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void PesquisaCustoButton_Click(object sender, EventArgs e)
        {
            if (CustosTextBox.Text.Trim() == "")
            {
                Publicas._codigoRetornoPesquisa = _empresa.CodigoEmpresaGlobus;
                new Pesquisas.CentroDeCustoContabil().ShowDialog();

                CustosTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CustosTextBox.Text.Trim() == "" || CustosTextBox.Text == "0")
                {
                    CustosTextBox.Text = string.Empty;
                    CustosTextBox.Focus();
                    return;
                }

                CustosTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;
            int codcusto = 0;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (new Notificacoes.Mensagem("Confirma a exclusão ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.RateioBeneficios.Custos _excluirTipos = new Classes.RateioBeneficios.Custos();

                    gridGroupingControl1.DataSource = new List<Classes.RateioBeneficios.Custos>();

                    try
                    {
                        codcusto = (int)dr["CodigoCusto"];
                    }
                    catch
                    {
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("CodigoCusto =") + 13;
                            posFimId = dr.Info.IndexOf(", Nome");
                            codcusto = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        }
                        catch { }
                    }

                    foreach (var item in _listaCustosParametro.Where(w => w.CodigoCusto == codcusto))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new RateioBeneficioBO().ExcluirCustosDoParametro(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _listaCustosParametro.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu a associacao do custo " + _excluirTipos.CodigoCusto + " " + _excluirTipos.Nome +
                        " da empresa " + empresaComboBoxAdv.Text + " plano " + PlanoTextBox.Text;
                    _log.Tela = "Contabilidade - Cadastros - Parametros";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _listaCustosParametro;
            }
        }

        private void IgnorarFuncoesCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (CodigosAprendizesTextBox.Enabled)
                    CodigosAprendizesTextBox.Focus();
                else
                    HistoricoTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void CodigosAprendizesTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                HistoricoTextBox.Focus();

            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void HistoricoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                RegraVTCheckBox.Focus();

            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (CodigosAprendizesTextBox.Enabled)
                    CodigosAprendizesTextBox.Focus();
                else
                    IgnorarFuncoesCheckBox.Focus();
            }
        }

        private void RegraVTCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (RegraVTCheckBox.Checked)
                    ContasValeTransporteTextBox.Focus();
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                HistoricoTextBox.Focus();
            }
        }

        private void ContasValeTransporteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EventosValeTransporteTextBoxExt.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                RegraVTCheckBox.Focus();
            }
        }

        private void EventosValeTransporteTextBoxExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ContasValeTransporteTextBox.Focus();
            }
        }

        private void IgnorarFuncoesCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            CodigosAprendizesTextBox.Enabled = IgnorarFuncoesCheckBox.Checked;
        }

        private void RegraVTCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            ContasValeTransporteTextBox.Enabled = RegraVTCheckBox.Checked;
            EventosValeTransporteTextBoxExt.Enabled = RegraVTCheckBox.Checked;
        }
    }
}
