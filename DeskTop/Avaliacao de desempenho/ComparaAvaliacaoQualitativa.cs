using Classes;
using DynamicFilter;
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

namespace Suportte.Avaliacao_de_desempenho
{
    public partial class ComparaAvaliacaoQualitativa : Form
    {
        public ComparaAvaliacaoQualitativa()
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

        Classes.Empresa _empresa;
        Classes.Colaboradores _colaboradores;
        Classes.Prazos _prazo;
        Classes.AutoAvaliacao _autoAvaliacao;
        Classes.AutoAvaliacao _autoAvaliacaoRh;
        Classes.AutoAvaliacao _autoAvaliacaoGestor;
        Classes.FuncionariosGlobus _funcionariosGlobus;
        Classes.Cargos _cargos;
        Classes.Pontuacao _pontuacao;

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.CompetenciasDoCargo> _listaCompetenciasRH;
        List<Classes.CompetenciasDoCargo> _listaCompetenciasGestor;
        List<Classes.CompetenciasDoCargo> _listaCompetencias;
        List<Classes.ItensDaAutoAvaliacao> _listaItensAvaliacao;
        List<Classes.ItensDaAutoAvaliacao> _listaItensAvaliacaoRH;
        List<Classes.ItensDaAutoAvaliacao> _listaItensAvaliacaoGestor;
        
        List<ItensCompetencia> _listaGrid;
        List<ACompetencia> _listaACompetencia;

        GridSummaryRowDescriptor _soma;

        public class ACompetencia
        {
            public string Competencia { get; set; }
            public string DetalheCompetencia { get; set; }
            public string Comentario { get; set; }
            public string ComentarioRH { get; set; }
            public string ComentarioGestor { get; set; }
            public List<ItensCompetencia> Itens { get; set; }
        }

        public class ItensCompetencia
        {
            public int Id { get; set; }
            public int IdAutoAvaliacao { get; set; }
            public int IdSubCompetencia { get; set; }
            public int IdCompetencia { get; set; }
            public string Descricao { get; set; }            
            public string AvaliacaoAuto { get; set; }
            public string AvaliacaoRH { get; set; }
            public string AvaliacaoGestor { get; set; }
            public decimal PontuacaoAuto { get; set; }
            public decimal PontuacaoGestor { get; set; }
            public decimal PontuacaoRH { get; set; }
            public decimal TotalSubCompetenciaAuto { get; set; }
            public decimal TotalSubCompetenciaRH { get; set; }
            public decimal TotalSubCompetenciaGestor { get; set; }
            public decimal TotalCompetenciaAuto { get; set; }
            public decimal TotalCompetenciaRH { get; set; }
            public decimal TotalCompetenciaGestor { get; set; }
        }

        decimal regraBase;

        public Publicas.TipoPrazos tipoAvaliacao;

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

        private void ComparaAvaliacaoQualitativa_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);

            GridMetroColors metroColor = new GridMetroColors();

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                //gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
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

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl.SetMetroStyle(metroColor);
                this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl.Table.DefaultRecordRowHeight = 55;

            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

            empresaComboBoxAdv.SelectedIndex = 0;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridGroupingControl_TableControlMouseDown(object sender, GridTableControlMouseEventArgs e)
        {
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                usuarioTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void usuarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                referenciaMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (usuarioTextBox.Enabled)
                    usuarioTextBox.Focus();
                else
                    referenciaMaskedEditBox.Focus();
            }
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void usuarioTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
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

        private void usuarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            usuarioTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaUsuarioButton.Enabled = false;
                usuarioTextBox.Text = string.Empty;
                nomeTextBox.Text = string.Empty;
                descricaoCargoTextBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._participaAvaliacao = true;

                new Pesquisas.Funcionarios().ShowDialog();

                usuarioTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(usuarioTextBox.Text) || usuarioTextBox.Text == "0")
                {
                    usuarioTextBox.Text = string.Empty;
                    usuarioTextBox.Focus();
                    return;
                }
            }

            usuarioTextBox.Text = usuarioTextBox.Text.PadLeft(6, '0');
            if (usuarioTextBox.Text == Publicas._usuario.RegistroFuncionario)
            {
                new Notificacoes.Mensagem("Registro do funcionário deve ser diferente do seu registro.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(usuarioTextBox.Text, _empresa.CodigoEmpresaGlobus);

            _colaboradores = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, usuarioTextBox.Text, false);

            if (!_funcionariosGlobus.Existe)
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            if (_funcionariosGlobus.DataDesligamento != DateTime.MinValue && !_funcionariosGlobus.Ativo)
            {
                new Notificacoes.Mensagem("Colaborador desligado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _funcionariosGlobus.Nome;

            _cargos = new CargosBO().Consultar(_colaboradores.IdCargo);
            descricaoCargoTextBox.Text = _cargos.Descricao;
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaReferenciaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (_listaCompetencias == null || _listaCompetencias.Count() == 0)
            {
                if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
                {
                    Publicas._idSuperior = _colaboradores.Id;

                    Pesquisas.Feedback _pesquisa = new Pesquisas.Feedback("AA");
                    _pesquisa.tituloLabel.Text = "Pesquisa de " + tituloLabel.Text;
                    _pesquisa.ShowDialog();

                    referenciaMaskedEditBox.Text = Publicas._idRetornoPesquisa.ToString("000000");

                    if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()) || referenciaMaskedEditBox.ClipText == "000000")
                    {
                        referenciaMaskedEditBox.Text = string.Empty;
                        referenciaMaskedEditBox.Focus();
                        return;
                    }
                }

                try
                {
                    _autoAvaliacao = new AutoAvaliacaoBO().Consultar(_colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), "AA", _empresa.IdEmpresa);
                    _autoAvaliacaoRh = new AutoAvaliacaoBO().Consultar(_colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), "AR", _empresa.IdEmpresa);
                    _autoAvaliacaoGestor = new AutoAvaliacaoBO().Consultar(_colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), "AG", _empresa.IdEmpresa);
                    _pontuacao = new PontuacaoBO().ConsultarMaiorReferencia(Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim()));
                    _prazo = new PrazosBO().Consultar(DateTime.Now.Date, "AA", referenciaMaskedEditBox.ClipText.Trim());
                }
                catch
                {
                    new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                if (!_prazo.Existe)
                {
                    new Notificacoes.Mensagem(Publicas.GetDescription(tipoAvaliacao, "") + " não cadastrado para essa referência.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                if (_prazo.Fim.Date < DateTime.Now.Date)
                {
                    if (new Notificacoes.Mensagem("Prazo para " + Publicas.GetDescription(tipoAvaliacao, "") + " finalizado em " + _prazo.Fim.Date.ToShortDateString() + "." +
                        Environment.NewLine + "Deseja consultar?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    {
                        referenciaMaskedEditBox.Focus();
                        return;
                    }
                }

                _listaCompetencias = new CompetenciasDoCargoBO().Listar(_colaboradores.IdCargo, "C", _autoAvaliacao.Existe, _colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), Publicas.TipoPrazos.AutoAvaliacao);
                _listaCompetenciasGestor = new CompetenciasDoCargoBO().Listar(_colaboradores.IdCargo, "C", _autoAvaliacao.Existe, _colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), Publicas.TipoPrazos.AvaliacaoDoGestor);
                _listaCompetenciasRH = new CompetenciasDoCargoBO().Listar(_colaboradores.IdCargo, "C", _autoAvaliacao.Existe, _colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), Publicas.TipoPrazos.AvaliacaoRH);

                _listaItensAvaliacao = new AutoAvaliacaoBO().Listar(_colaboradores.IdCargo, _colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), Publicas.TipoPrazos.AutoAvaliacao);
                _listaItensAvaliacaoGestor = new AutoAvaliacaoBO().Listar(_colaboradores.IdCargo, _colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), Publicas.TipoPrazos.AvaliacaoDoGestor);
                _listaItensAvaliacaoRH = new AutoAvaliacaoBO().Listar(_colaboradores.IdCargo, _colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), Publicas.TipoPrazos.AvaliacaoRH);

                regraBase = (_listaCompetenciasGestor.Where(w => w.Marcado).Count() * _pontuacao.Supera);

                resultadoAvaliacaoLabel.Text = "Colaborador " + Environment.NewLine + _autoAvaliacao.TotalAvaliacao;
                resultadoRhLabel.Text = "RH " + Environment.NewLine + _autoAvaliacaoRh.TotalAvaliacao;
                resultadoAvaliacaoGestorLabel.Text = "Gestor " + Environment.NewLine + _autoAvaliacaoGestor.TotalAvaliacao;

                #region Prepara dados

                ItensCompetencia _itens;


                _listaACompetencia = new List<ACompetencia>();

                foreach (var item in _listaCompetenciasGestor)
                {
                    if (_listaGrid == null)
                        _listaGrid = new List<ItensCompetencia>();
                    else
                        _listaGrid.Clear();

                    ACompetencia _aCompetencia = new ACompetencia();

                    _aCompetencia.Competencia = item.Descricao;
                    _aCompetencia.DetalheCompetencia = item.TextoExplicativo;

                    _aCompetencia.Itens = new List<ItensCompetencia>();

                    foreach (var itemA in _listaItensAvaliacao.Where(w => w.IdCompetencia == item.Id))
                    {
                        _itens = new ItensCompetencia();

                        _itens.Descricao = itemA.Descricao;
                        _itens.Id = itemA.Id;
                        _itens.IdCompetencia = itemA.IdCompetencia;
                        _itens.IdSubCompetencia = itemA.IdSubCompetencia;
                        _itens.PontuacaoAuto = itemA.Pontuacao;
                        _aCompetencia.Comentario = itemA.Comentario;
                        
                        _itens.AvaliacaoAuto = (itemA.NaoAtende ? "1" + Environment.NewLine + "Não" + Environment.NewLine + "Atende" :
                            (itemA.AtendeParcialmente ? "2" + Environment.NewLine + "Atende" + Environment.NewLine + "parcialmente" :
                            (itemA.AtendePlenamente ? "3" + Environment.NewLine + "Atende" + Environment.NewLine + "plenamente" :
                            (itemA.Supera ? "4" + Environment.NewLine + "Supera" : ""))));

                        _listaGrid.Add(_itens);
                    }

                    foreach (var itemA in _listaItensAvaliacaoGestor.Where(w => w.IdCompetencia == item.Id))
                    {
                        if (_listaGrid.Where(w => w.IdSubCompetencia == itemA.IdSubCompetencia).Count() == 0)
                        {
                            _itens = new ItensCompetencia();

                            _itens.Descricao = itemA.Descricao;
                            _itens.Id = itemA.Id;
                            _itens.IdCompetencia = itemA.IdCompetencia;
                            _itens.IdSubCompetencia = itemA.IdSubCompetencia;
                            _itens.PontuacaoGestor = itemA.Pontuacao;
                            _aCompetencia.ComentarioGestor = itemA.Comentario;
                            _itens.AvaliacaoGestor = (itemA.NaoAtende ? "1" + Environment.NewLine + "Não" + Environment.NewLine + "Atende" :
                                (itemA.AtendeParcialmente ? "2" + Environment.NewLine + "Atende" + Environment.NewLine + "parcialmente" :
                                (itemA.AtendePlenamente ? "3" + Environment.NewLine + "Atende" + Environment.NewLine + "plenamente" :
                                (itemA.Supera ? "4" + Environment.NewLine + "Supera" : ""))));
                            _listaGrid.Add(_itens);
                        }
                        else
                            foreach (var itemG in _listaGrid.Where(w => w.IdSubCompetencia == itemA.IdSubCompetencia))
                            {
                                itemG.PontuacaoGestor = itemA.Pontuacao;
                                _aCompetencia.ComentarioGestor = itemA.Comentario;
                                itemG.AvaliacaoGestor = (itemA.NaoAtende ? "1" + Environment.NewLine + "Não" + Environment.NewLine + "Atende" :
                                (itemA.AtendeParcialmente ? "2" + Environment.NewLine + "Atende" + Environment.NewLine + "parcialmente" :
                                (itemA.AtendePlenamente ? "3" + Environment.NewLine + "Atende" + Environment.NewLine + "plenamente" :
                                (itemA.Supera ? "4" + Environment.NewLine + "Supera" : ""))));
                            }
                    }

                    foreach (var itemA in _listaItensAvaliacaoRH.Where(w => w.IdCompetencia == item.Id))
                    {
                        if (_listaGrid.Where(w => w.IdSubCompetencia == itemA.IdSubCompetencia).Count() == 0)
                        {
                            _itens = new ItensCompetencia();

                            _itens.Descricao = itemA.Descricao;
                            _itens.Id = itemA.Id;
                            _itens.IdCompetencia = itemA.IdCompetencia;
                            _aCompetencia.ComentarioRH = itemA.Comentario;
                            _itens.IdSubCompetencia = itemA.IdSubCompetencia;
                            _itens.PontuacaoRH = itemA.Pontuacao;
                            _itens.AvaliacaoRH = (itemA.NaoAtende ? "1" + Environment.NewLine + "Não" + Environment.NewLine + "Atende" :
                                (itemA.AtendeParcialmente ? "2" + Environment.NewLine + "Atende" + Environment.NewLine + "parcialmente" :
                                (itemA.AtendePlenamente ? "3" + Environment.NewLine + "Atende" + Environment.NewLine + "plenamente" :
                                (itemA.Supera ? "4" + Environment.NewLine + "Supera" : ""))));

                            _listaGrid.Add(_itens);
                        }
                        else
                            foreach (var itemG in _listaGrid.Where(w => w.IdSubCompetencia == itemA.IdSubCompetencia))
                            {
                                itemG.PontuacaoRH = itemA.Pontuacao;
                                _aCompetencia.ComentarioRH = itemA.Comentario;
                                itemG.AvaliacaoRH = (itemA.NaoAtende ? "1" + Environment.NewLine + "Não" + Environment.NewLine + "Atende" :
                            (itemA.AtendeParcialmente ? "2" + Environment.NewLine + "Atende" + Environment.NewLine + "parcialmente" :
                            (itemA.AtendePlenamente ? "3" + Environment.NewLine + "Atende" + Environment.NewLine + "plenamente" :
                            (itemA.Supera ? "4" + Environment.NewLine + "Supera" : ""))));
                            }
                    }

                    _aCompetencia.Itens.AddRange(_listaGrid);

                    _listaACompetencia.Add(_aCompetencia);
                }

                #endregion

                PreparaGrid();

                for (int i = 0; i < gridGroupingControl.TableDescriptor.Relations.Count; i++)
                {

                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.TableOptions.ShowRowHeader = false;
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.AllowEdit = false;
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.AllowNew = false;
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.AllowRemove = false;

                    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                    summaryColumnDescriptor.DataMember = "PontuacaoAuto";
                    summaryColumnDescriptor.Format = "{Average}";
                    summaryColumnDescriptor.Name = "PontuacaoAuto";
                    summaryColumnDescriptor.SummaryType = SummaryType.Int32Aggregate;

                    GridSummaryColumnDescriptor summaryColumnDescriptor2 = new GridSummaryColumnDescriptor();
                    summaryColumnDescriptor2.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    summaryColumnDescriptor2.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                    summaryColumnDescriptor2.Appearance.AnySummaryCell.Format = "n2";
                    summaryColumnDescriptor2.DataMember = "PontuacaoGestor";
                    summaryColumnDescriptor2.Format = "{Average}";
                    summaryColumnDescriptor2.Name = "PontuacaoGestor";
                    summaryColumnDescriptor2.SummaryType = SummaryType.Int32Aggregate;

                    GridSummaryColumnDescriptor summaryColumnDescriptor3 = new GridSummaryColumnDescriptor();
                    summaryColumnDescriptor3.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    summaryColumnDescriptor3.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                    summaryColumnDescriptor3.Appearance.AnySummaryCell.Format = "n2";
                    summaryColumnDescriptor3.DataMember = "PontuacaoRH";
                    summaryColumnDescriptor3.Format = "{Average}";
                    summaryColumnDescriptor3.Name = "PontuacaoRH";
                    summaryColumnDescriptor3.SummaryType = SummaryType.Int32Aggregate;

                    _soma = new GridSummaryRowDescriptor("Sum", "Pontuação da competência", 
                        new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor3, summaryColumnDescriptor2 });
                    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;

                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.SummaryRows.Add(_soma);
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.SummaryRows[0].Appearance.AnyCell.BackColor = gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[0].Appearance.AnyRecordFieldCell.BackColor;

                    for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns.Count; j++)
                    {
                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[i].AllowFilter = false;
                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(referenciaMaskedEditBox.Font.FontFamily, (float)8, FontStyle.Regular));

                        if ((gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName != "Descricao" &&
                            (!gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.StartsWith("Pontuacao"))))
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                        else
                        {
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                            if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName == "Descricao")
                            {
                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Descrição";
                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Width = 450;
                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(referenciaMaskedEditBox.Font.FontFamily, (float)8, FontStyle.Bold));
                            }

                            if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("Pontuacao"))
                            {
                                if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("Auto"))
                                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Colaborador";
                                if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("Gestor"))
                                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Gestor";
                                if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("RH"))
                                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "RH";

                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                            }                            
                        }
                    }
                }
            }
        }

        private void usuarioTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void referenciaMaskedEditBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }

        private void pesquisaUsuarioButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._participaAvaliacao = true;

                new Pesquisas.Funcionarios().ShowDialog();

                usuarioTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(usuarioTextBox.Text) || usuarioTextBox.Text == "0")
                {
                    usuarioTextBox.Text = string.Empty;
                    usuarioTextBox.Focus();
                    return;
                }

                usuarioTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaReferenciaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                Publicas._idSuperior = _colaboradores.Id;

                Pesquisas.Feedback _pesquisa = new Pesquisas.Feedback("AA");
                _pesquisa.tituloLabel.Text = "Pesquisa de " + tituloLabel.Text;
                _pesquisa.ShowDialog();

                referenciaMaskedEditBox.Text = Publicas._idRetornoPesquisa.ToString("000000");

                if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()) || referenciaMaskedEditBox.ClipText == "000000")
                {
                    referenciaMaskedEditBox.Text = string.Empty;
                    referenciaMaskedEditBox.Focus();
                    return;
                }
            }
        }

        private void PreparaGrid()
        {
            gridGroupingControl.DataSource = _listaACompetencia;
            gridGroupingControl.Refresh();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            _listaCompetencias.Clear();
            _listaItensAvaliacao.Clear();
            _listaACompetencia.Clear();

            gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.SummaryRows.Remove(_soma);

            referenciaMaskedEditBox.Text = string.Empty;
            gridGroupingControl.DataSource = new List<ACompetencia>();
            gridGroupingControl.Enabled = true;
            descricaoCargoTextBox.Text = string.Empty;
            resultadoAvaliacaoLabel.Text = "Colaborador" + Environment.NewLine + "0";
            resultadoAvaliacaoGestorLabel.Text = "Gestor" + Environment.NewLine + "0";
            resultadoRhLabel.Text = "RH" + Environment.NewLine + "0";
            nomeTextBox.Text = string.Empty;
            usuarioTextBox.Text = string.Empty;
            descricaoCargoTextBox.Text = string.Empty;
            MensagemLabel.Text = string.Empty;
            pesquisaReferenciaButton.Enabled = false;
            pesquisaUsuarioButton.Enabled = false;
            usuarioTextBox.Focus();
            _soma = null;
        }
    }
}
