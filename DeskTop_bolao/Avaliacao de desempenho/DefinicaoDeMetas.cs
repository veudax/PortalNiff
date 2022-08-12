using Classes;
using Negocio;
using Suportte.Notificacoes;
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
    public partial class DefinicaoDeMetas : Form
    {
        public DefinicaoDeMetas()
        {
            InitializeComponent();

            dataLimiteDateTimePicker.Value = DateTime.Now;
            dataLimiteDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataLimiteDateTimePicker.BackColor = usuarioTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        #region Atributos

        #region Criando Classes
        class ItensAvaliacaoMetasPeriodo : Classes.ItensAvaliacaoMetas
        {
            public decimal Peso1 { get; set; }
            public decimal ValorEsperado1 { get; set; }
            public decimal Realizado1 { get; set; }
            public string Referencia1 { get; set; }
            public decimal Eficiencia1 { get; set; }
            public decimal Resultado1 { get; set; }
            public decimal EficienciaPonderada1 { get; set; }
            public decimal ResultadoPonderado1 { get; set; }
            public decimal Peso2 { get; set; }
            public decimal ValorEsperado2 { get; set; }
            public decimal Realizado2 { get; set; }
            public string Referencia2 { get; set; }
            public decimal Eficiencia2 { get; set; }
            public decimal Resultado2 { get; set; }
            public decimal EficienciaPonderada2 { get; set; }
            public decimal ResultadoPonderado2 { get; set; }
            public decimal Peso3 { get; set; }
            public decimal ValorEsperado3 { get; set; }
            public decimal Realizado3 { get; set; }
            public string Referencia3 { get; set; }
            public decimal Eficiencia3 { get; set; }
            public decimal Resultado3 { get; set; }
            public decimal EficienciaPonderada3 { get; set; }
            public decimal ResultadoPonderado3 { get; set; }
            public decimal Peso4 { get; set; }
            public decimal ValorEsperado4 { get; set; }
            public decimal Realizado4 { get; set; }
            public string Referencia4 { get; set; }
            public decimal Eficiencia4 { get; set; }
            public decimal Resultado4 { get; set; }
            public decimal EficienciaPonderada4 { get; set; }
            public decimal ResultadoPonderado4 { get; set; }
            public decimal Peso5 { get; set; }
            public decimal ValorEsperado5 { get; set; }
            public decimal Realizado5 { get; set; }
            public string Referencia5 { get; set; }
            public decimal Eficiencia5 { get; set; }
            public decimal Resultado5 { get; set; }
            public decimal EficienciaPonderada5 { get; set; }
            public decimal ResultadoPonderado5 { get; set; }
            public decimal Peso6 { get; set; }
            public decimal ValorEsperado6 { get; set; }
            public decimal Realizado6 { get; set; }
            public string Referencia6 { get; set; }
            public decimal Eficiencia6 { get; set; }
            public decimal Resultado6 { get; set; }
            public decimal EficienciaPonderada6 { get; set; }
            public decimal ResultadoPonderado6 { get; set; }
            public decimal Peso7 { get; set; }
            public decimal ValorEsperado7 { get; set; }
            public decimal Realizado7 { get; set; }
            public string Referencia7 { get; set; }
            public decimal Eficiencia7 { get; set; }
            public decimal Resultado7 { get; set; }
            public decimal EficienciaPonderada7 { get; set; }
            public decimal ResultadoPonderado7 { get; set; }
            public decimal Peso8 { get; set; }
            public decimal ValorEsperado8 { get; set; }
            public decimal Realizado8 { get; set; }
            public string Referencia8 { get; set; }
            public decimal Eficiencia8 { get; set; }
            public decimal Resultado8 { get; set; }
            public decimal EficienciaPonderada8 { get; set; }
            public decimal ResultadoPonderado8 { get; set; }
            public decimal Peso9 { get; set; }
            public decimal ValorEsperado9 { get; set; }
            public decimal Realizado9 { get; set; }
            public string Referencia9 { get; set; }
            public decimal Eficiencia9 { get; set; }
            public decimal Resultado9 { get; set; }
            public decimal EficienciaPonderada9 { get; set; }
            public decimal ResultadoPonderado9 { get; set; }
            public decimal Peso10 { get; set; }
            public decimal ValorEsperado10 { get; set; }
            public decimal Realizado10 { get; set; }
            public string Referencia10 { get; set; }
            public decimal Eficiencia10 { get; set; }
            public decimal Resultado10 { get; set; }
            public decimal EficienciaPonderada10 { get; set; }
            public decimal ResultadoPonderado10 { get; set; }
            public decimal Peso11 { get; set; }
            public decimal ValorEsperado11 { get; set; }
            public decimal Realizado11 { get; set; }
            public string Referencia11 { get; set; }
            public decimal Eficiencia11 { get; set; }
            public decimal Resultado11 { get; set; }
            public decimal EficienciaPonderada11 { get; set; }
            public decimal ResultadoPonderado11 { get; set; }
            public decimal Peso12 { get; set; }
            public decimal ValorEsperado12 { get; set; }
            public decimal Realizado12 { get; set; }
            public string Referencia12 { get; set; }
            public decimal Eficiencia12 { get; set; }
            public decimal Resultado12 { get; set; }
            public decimal EficienciaPonderada12 { get; set; }
            public decimal ResultadoPonderado12 { get; set; }
        }

        #endregion

        Classes.Empresa _empresa;
        Classes.Colaboradores _colaboradores;
        Classes.Colaboradores _colaboradoresSuperior;
        Classes.Prazos _prazo;
        Classes.AutoAvaliacao _autoAvaliacao;
        Classes.FuncionariosGlobus _funcionariosGlobus;
        Classes.Cargos _cargos;
        Classes.Pontuacao _pontuacao;
        Classes.PontuacaoFatorEmpresa _fatores;
        Classes.EmpresaQueOColaboradorEhAvaliado _empresaDoColaborador;

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.ItensAvaliacaoMetas> _listaItensAvaliacao;
        List<ItensAvaliacaoMetasPeriodo> _listaItensPorPeriodo;
        //List<Classes.ItensAvaliacaoMetas> _listaItens;

        GridCurrentCell _colunaCorrente;
        DateTime referencia;
        DateTime referenciaFim;

        int colunaPeso = 2;
        int colunaEsperado = 7;
        int colunaRealizado = 4;
        int colunaEficiencia = 5;
        int colunaEficienciaPonderada = 6;
        int colunaResultado = 7;
        int colunaResultadoPonderada = 8;

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

        private void DefinicaoDeMetas_Shown(object sender, EventArgs e)
        {
            copiarbutton.Visible = !ativoCheckBox.Checked && tituloLabel.Text.Contains("Definição");

            GridMetroColors metroColor = new GridMetroColors();

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.TableControl.CellToolTip.Active = true;
            gridGroupingControl.TableDescriptor.AllowEdit = tituloLabel.Text.Contains("Definição");

            if (!tituloLabel.Text.Contains("Definição") && !tituloLabel.Text.Contains("RH") && !tituloLabel.Text.Contains("Gestor"))
            {
                gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Eficiencia");
                gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Resultado");
            }

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                if (i == 0 || i == 5 || i == 6)
                    gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                else
                    gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;

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

            this.gridGroupingControl.SetMetroStyle(metroColor);

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = (tituloLabel.Text.Contains("Definição") ? GridListBoxSelectionCurrentCellOptions.None : GridListBoxSelectionCurrentCellOptions.HideCurrentCell);

            this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl.Table.DefaultRecordRowHeight = 25;

            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

            for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
            {
                empresaComboBoxAdv.SelectedIndex = i;
                if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                {
                    break;
                }
            }

            _colaboradoresSuperior = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas._usuario.RegistroFuncionario, false);

            if (!usuarioTextBox.Enabled)
            {
                usuarioTextBox.Text = Publicas._usuario.RegistroFuncionario;

                _colaboradores = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, usuarioTextBox.Text, false);
                nomeTextBox.Text = _colaboradores.Nome;

                _cargos = new CargosBO().Consultar(_colaboradores.IdCargo);
                descricaoCargoTextBox.Text = _cargos.Descricao;
            }

            if (tituloLabel.Text.Contains("Definição"))
            {
                gravarButton.Location = new Point(303, 14);
                limparButton.Location = new Point(438, 14);
                excluirButton.Location = new Point(573, 14);
            }
            else
            {
                excluirButton.Visible = false;
                gravarButton.Location = new Point(370, 14);
                limparButton.Location = new Point(505, 14);
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
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

        private void dataDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void dataDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void usuarioTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
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

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

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
                usuarioTextBox.Text = string.Empty;
                nomeTextBox.Text = string.Empty;
                descricaoCargoTextBox.Text = string.Empty;
                pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._idSuperior = (!tituloLabel.Text.Contains("Gestor") ? 0 : _colaboradoresSuperior.Id);

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

            _colaboradores = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, usuarioTextBox.Text, false);

            _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(usuarioTextBox.Text, _empresa.CodigoEmpresaGlobus);

            _empresaDoColaborador = new ColaboradoresBO().Consultar(_colaboradores.Id, _empresa.IdEmpresa);

            if (!_empresaDoColaborador.Existe)
            {
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
            }

            if (_colaboradores.IdSupervisor != _colaboradoresSuperior.Id && tituloLabel.Text.Contains("Gestor"))
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado na sua equipe.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _colaboradores.Nome;

            _cargos = new CargosBO().Consultar(_colaboradores.IdCargo);
            descricaoCargoTextBox.Text = _cargos.Descricao;
        }

        private void gridGroupingControl_RecordValueChanged(object sender, RecordValueChangedEventArgs e)
        {
            string nomeColuna = gridGroupingControl.TableDescriptor.Columns[_colunaCorrente.ColIndex - 1].MappingName;
            string posicao;
            string _formula = "";

            if (!ativoCheckBox.Checked)
            {

                try
                {
                    int _rowIndex = gridGroupingControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                    GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                    if (rec != null)
                    {
                        Record dr = rec.GetRecord() as Record;
                        if (dr != null)
                        {
                            decimal valor = (decimal)dr[nomeColuna];

                            foreach (var item in _listaItensAvaliacao.Where(w => w.IdMetas == (int)dr["IdMetas"]))
                            {
                                _formula = item.Formula;

                                if (nomeColuna.Contains("Peso"))
                                {
                                    item.Peso = valor;
                                }

                                if (nomeColuna.Contains("ValorEsperado"))
                                {
                                    item.ValorEsperado = valor;
                                    if (valor != 0)
                                    {
                                        item.Eficiencia = Math.Round( new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }
                                    else
                                    {
                                        if (_formula.Contains("/ Esperado") && _formula.Contains("200"))
                                            item.Eficiencia = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", 1.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }

                                }

                                if (nomeColuna.Contains("Realizado"))
                                {
                                    item.Realizado = valor;

                                    if ((item.ValorEsperado != 0 && _formula.Contains("/ Esperado")) )
                                    {
                                        
                                        item.Eficiencia = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }
                                    else
                                    {
                                        if (item.ValorEsperado == 0 && _formula.Contains("/ Esperado") && _formula.Contains("200"))
                                        {
                                            item.Eficiencia = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", 1.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                        }
                                        else
                                        {
                                            if (item.ValorEsperado == 0 && !_formula.Contains("/ Esperado"))
                                                item.Eficiencia = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                        }
                                    }

                                }

                                if (item.ValorEsperado == 0 && item.Realizado == 0)
                                {
                                    item.Eficiencia = 100;
                                }

                                item.Resultado = Math.Abs(Math.Round(item.Eficiencia != 0 ? ((item.Peso * item.Eficiencia) / 100) : 0, 1));
                                item.EficienciaPonderada = (item.Eficiencia > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia));
                                item.ResultadoPonderado = Math.Abs(Math.Round(item.EficienciaPonderada != 0 ? ((item.Peso * item.EficienciaPonderada) / 100) : 0, 1));

                            }
                        }
                    }
                }
                catch { }
            }
            else
            {

                try
                {
                    int _rowIndex = gridGroupingControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                    GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                    if (rec != null)
                    {
                        Record dr = rec.GetRecord() as Record;
                        if (dr != null)
                        {
                            decimal valor = (decimal)dr[nomeColuna];

                            foreach (var item in _listaItensPorPeriodo.Where(w => w.IdMetas == (int)dr["IdMetas"]))
                            {
                                _formula = item.Formula;
                                if (nomeColuna.Contains("ValorEsperado"))
                                {
                                    posicao = nomeColuna.Replace("ValorEsperado", "");
                                    if (valor != 0)
                                    {
                                        switch (posicao)
                                        {
                                            case "1":
                                                item.ValorEsperado1 = valor;

                                                item.Eficiencia1 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado1.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                        
                                                        item.Resultado1 = Math.Abs(Math.Round(item.Eficiencia1 != 0 ? ((item.Peso1 * item.Eficiencia1) / 100 ): 0, 1));

                                                item.EficienciaPonderada1 = (item.Eficiencia1 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia1 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia1));
                                                item.ResultadoPonderado1 = Math.Abs(Math.Round(item.EficienciaPonderada1 != 0 ? ((item.Peso1 * item.EficienciaPonderada1) / 100) : 0, 1));

                                                break;
                                            case "2":
                                                item.ValorEsperado2 = valor;
                                                item.Eficiencia2 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado2.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);

                                                item.Resultado2 = Math.Abs(Math.Round(item.Eficiencia2 != 0 ? ((item.Peso2 * item.Eficiencia2) / 100) : 0, 1));
                                                item.EficienciaPonderada2 = (item.Eficiencia2 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia2 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia2));
                                                item.ResultadoPonderado2 = Math.Abs(Math.Round(item.EficienciaPonderada2 != 0 ? ((item.Peso2 * item.EficienciaPonderada2) / 100) : 0, 1));
                                                break;
                                            case "3":
                                                item.ValorEsperado3 = valor;
                                                
                                                item.Eficiencia3 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado3.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);

                                                item.Resultado3 = Math.Abs(Math.Round(item.Eficiencia3 != 0 ? ((item.Peso3 * item.Eficiencia3) / 100) : 0, 1));
                                                item.EficienciaPonderada3 = (item.Eficiencia3 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia3 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia3));
                                                item.ResultadoPonderado3 = Math.Abs(Math.Round(item.EficienciaPonderada3 != 0 ? ((item.Peso3 * item.EficienciaPonderada3) / 100) : 0, 1));

                                                break;
                                            case "4":
                                                item.ValorEsperado4 = valor;
                                                item.Eficiencia4 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado4.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);

                                                item.Resultado4 = Math.Abs(Math.Round(item.Eficiencia4 != 0 ? ((item.Peso4 * item.Eficiencia4) / 100) : 0, 1));

                                                item.EficienciaPonderada4 = (item.Eficiencia4 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia4 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia4));
                                                item.ResultadoPonderado4 = Math.Abs(Math.Round(item.EficienciaPonderada4 != 0 ? ((item.Peso4 * item.EficienciaPonderada4) / 100) : 0, 1));
                                                break;
                                            case "5":
                                                item.ValorEsperado5 = valor;
                                                item.Eficiencia5 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado5.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);

                                                item.Resultado5 = Math.Abs(Math.Round(item.Eficiencia5 != 0 ? ((item.Peso5 * item.Eficiencia5) / 100) : 0, 1));
                                                item.EficienciaPonderada5 = (item.Eficiencia5 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia5 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia5));
                                                item.ResultadoPonderado5 = Math.Abs(Math.Round(item.EficienciaPonderada5 != 0 ? ((item.Peso5 * item.EficienciaPonderada5) / 100) : 0, 1));

                                                break;
                                            case "6":
                                                item.ValorEsperado6 = valor;
                                                item.Eficiencia6 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado6.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);

                                                item.Resultado6 = Math.Abs(Math.Round(item.Eficiencia6 != 0 ? ((item.Peso6 * item.Eficiencia6) / 100) : 0, 1));
                                                item.EficienciaPonderada6 = (item.Eficiencia6 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia6 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia6));
                                                item.ResultadoPonderado6 = Math.Abs(Math.Round(item.EficienciaPonderada6 != 0 ? ((item.Peso6 * item.EficienciaPonderada6) / 100) : 0, 1));

                                                break;
                                            case "7":
                                                item.ValorEsperado7 = valor;
                                                item.Eficiencia7 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado7.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);

                                                item.Resultado7 = Math.Abs(Math.Round(item.Eficiencia7 != 0 ? ((item.Peso7 * item.Eficiencia7) / 100) : 0, 1));
                                                item.EficienciaPonderada7 = (item.Eficiencia7 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia7 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia7));
                                                item.ResultadoPonderado7 = Math.Abs(Math.Round(item.EficienciaPonderada7 != 0 ? ((item.Peso7 * item.EficienciaPonderada7) / 100) : 0, 1));

                                                break;
                                            case "8":
                                                item.ValorEsperado8 = valor;
                                                item.Eficiencia8 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado8.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);

                                                item.Resultado8 = Math.Abs(Math.Round(item.Eficiencia8 != 0 ? ((item.Peso8 * item.Eficiencia8) / 100) : 0, 1));

                                                item.EficienciaPonderada8 = (item.Eficiencia8 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia8 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia8));
                                                item.ResultadoPonderado8 = Math.Abs(Math.Round(item.EficienciaPonderada8 != 0 ? ((item.Peso8 * item.EficienciaPonderada8) / 100) : 0, 1));

                                                break;
                                            case "9":
                                                item.ValorEsperado9 = valor;
                                                item.Eficiencia9 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado9.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);

                                                item.Resultado9 = Math.Abs(Math.Round(item.Eficiencia9 != 0 ? ((item.Peso9 * item.Eficiencia9) / 100) : 0, 1));

                                                item.EficienciaPonderada9 = (item.Eficiencia9 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia9 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia9));
                                                item.ResultadoPonderado9 = Math.Abs(Math.Round(item.EficienciaPonderada9 != 0 ? ((item.Peso9 * item.EficienciaPonderada9) / 100) : 0, 1));

                                                break;
                                            case "10":
                                                item.ValorEsperado10 = valor;
                                                item.Eficiencia10 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado10.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);

                                                item.Resultado10 = Math.Abs(Math.Round(item.Eficiencia10 != 0 ? ((item.Peso10 * item.Eficiencia10) / 100) : 0, 1));

                                                item.EficienciaPonderada10 = (item.Eficiencia10 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia10 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia10));
                                                item.ResultadoPonderado10 = Math.Abs(Math.Round(item.EficienciaPonderada10 != 0 ? ((item.Peso10 * item.EficienciaPonderada10) / 100) : 0, 1));

                                                break;
                                            case "11":
                                                item.ValorEsperado11 = valor;
                                                item.Eficiencia11 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado11.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);

                                                item.Resultado11 = Math.Abs(Math.Round(item.Eficiencia11 != 0 ? ((item.Peso11 * item.Eficiencia11) / 100) : 0, 1));

                                                item.EficienciaPonderada11 = (item.Eficiencia11 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia11 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia11));
                                                item.ResultadoPonderado11 = Math.Abs(Math.Round(item.EficienciaPonderada11 != 0 ? ((item.Peso11 * item.EficienciaPonderada11) / 100) : 0, 1));

                                                break;
                                            case "12":
                                                item.ValorEsperado12 = valor;
                                                item.Eficiencia12 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado12.ToString().Replace(".", "").Replace(",", "."))
                                                                 .Replace("Esperado", valor.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);

                                                item.Resultado12 = Math.Abs(Math.Round(item.Eficiencia12 != 0 ? ((item.Peso12 * item.Eficiencia12) / 100) : 0, 1));

                                                item.EficienciaPonderada12 = (item.Eficiencia12 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia12 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia12));
                                                item.ResultadoPonderado12 = Math.Abs(Math.Round(item.EficienciaPonderada12 != 0 ? ((item.Peso12 * item.EficienciaPonderada12) / 100) : 0, 1));

                                                break;
                                        }
                                    }
                                }

                                if (nomeColuna.Contains("Realizado"))
                                {
                                    posicao = nomeColuna.Replace("Realizado", "");
                                    switch (posicao)
                                    {

                                        case "1":
                                            if (item.ValorEsperado1 != 0)
                                            {
                                                item.Realizado1 = valor;
                                                item.Eficiencia1 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado1.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                                item.Resultado1 = Math.Abs(Math.Round(item.Eficiencia1 != 0 ? ((item.Peso1 * item.Eficiencia1) / 100) : 0, 1));

                                                item.EficienciaPonderada1 = (item.Eficiencia1 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia1 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia1));
                                                item.ResultadoPonderado1 = Math.Abs(Math.Round(item.EficienciaPonderada1 != 0 ? ((item.Peso1 * item.EficienciaPonderada1) / 100) : 0, 1));

                                            }
                                            break;
                                        case "2":
                                            if (item.ValorEsperado2 != 0)
                                            {
                                                item.Realizado2 = valor;
                                                item.Eficiencia2 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado2.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                                item.Resultado2 = Math.Abs(Math.Round(item.Eficiencia2 != 0 ? ((item.Peso2 * item.Eficiencia2) / 100) : 0, 1));

                                                item.EficienciaPonderada2 = (item.Eficiencia2 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia2 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia2));
                                                item.ResultadoPonderado2 = Math.Abs(Math.Round(item.EficienciaPonderada2 != 0 ? ((item.Peso2 * item.EficienciaPonderada2) / 100) : 0, 1));

                                            }
                                            break;
                                        case "3":
                                            if (item.ValorEsperado3 != 0)
                                            {
                                                item.Realizado3 = valor;
                                                item.Eficiencia3 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado3.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                                item.Resultado3 = Math.Abs(Math.Round(item.Eficiencia3 != 0 ? ((item.Peso3 * item.Eficiencia3) / 100) : 0, 1));

                                                item.EficienciaPonderada3 = (item.Eficiencia3 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia3 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia3));
                                                item.ResultadoPonderado3 = Math.Abs(Math.Round(item.EficienciaPonderada3 != 0 ? ((item.Peso3 * item.EficienciaPonderada3) / 100) : 0, 1));

                                            }
                                            break;
                                        case "4":
                                            if (item.ValorEsperado4 != 0)
                                            {
                                                item.Realizado4 = valor;
                                                item.Eficiencia4 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado4.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                                item.Resultado4 = Math.Abs(Math.Round(item.Eficiencia4 != 0 ? ((item.Peso4 * item.Eficiencia4) / 100) : 0, 1));

                                                item.EficienciaPonderada4 = (item.Eficiencia4 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia4 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia4));
                                                item.ResultadoPonderado4 = Math.Abs(Math.Round(item.EficienciaPonderada4 != 0 ? ((item.Peso4 * item.EficienciaPonderada4) / 100) : 0, 1));

                                            }
                                            break;
                                        case "5":
                                            if (item.ValorEsperado5 != 0)
                                            {
                                                item.Realizado5 = valor;
                                                item.Eficiencia = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado5.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                                item.Resultado5 = Math.Abs(Math.Round(item.Eficiencia5 != 0 ? ((item.Peso5 * item.Eficiencia5) / 100) : 0, 1));

                                                item.EficienciaPonderada5 = (item.Eficiencia5 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia5 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia5));
                                                item.ResultadoPonderado5 = Math.Abs(Math.Round(item.EficienciaPonderada5 != 0 ? ((item.Peso5 * item.EficienciaPonderada5) / 100) : 0, 1));

                                            }
                                            break;
                                        case "6":
                                            if (item.ValorEsperado6 != 0)
                                            {
                                                item.Realizado6 = valor;
                                                item.Eficiencia6 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado6.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                                item.Resultado6 = Math.Abs(Math.Round(item.Eficiencia6 != 0 ? ((item.Peso6 * item.Eficiencia6) / 100) : 0, 1));

                                                item.EficienciaPonderada6 = (item.Eficiencia6 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia6 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia6));
                                                item.ResultadoPonderado6 = Math.Abs(Math.Round(item.EficienciaPonderada6 != 0 ? ((item.Peso6 * item.EficienciaPonderada6) / 100) : 0, 1));

                                            }
                                            break;
                                        case "7":
                                            if (item.ValorEsperado7 != 0)
                                            {
                                                item.Realizado7 = valor;
                                                item.Eficiencia7 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado7.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                                item.Resultado7 = Math.Abs(Math.Round(item.Eficiencia7 != 0 ? ((item.Peso7 * item.Eficiencia7) / 100) : 0, 1));

                                                item.EficienciaPonderada7 = (item.Eficiencia7 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia7 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia7));
                                                item.ResultadoPonderado7 = Math.Abs(Math.Round(item.EficienciaPonderada7 != 0 ? ((item.Peso7 * item.EficienciaPonderada7) / 100) : 0, 1));

                                            }
                                            break;
                                        case "8":
                                            if (item.ValorEsperado8 != 0)
                                            {
                                                item.Realizado8 = valor;
                                                item.Eficiencia8 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado8.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                                item.Resultado8 = Math.Abs(Math.Round(item.Eficiencia8 != 0 ? ((item.Peso8 * item.Eficiencia8) / 100) : 0, 1));

                                                item.EficienciaPonderada8 = (item.Eficiencia8 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia8 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia8));
                                                item.ResultadoPonderado8 = Math.Abs(Math.Round(item.EficienciaPonderada8 != 0 ? ((item.Peso8 * item.EficienciaPonderada8) / 100) : 0, 1));

                                            }
                                            break;
                                        case "9":
                                            if (item.ValorEsperado9 != 0)
                                            {
                                                item.Realizado9 = valor;
                                                item.Eficiencia9 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado9.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                                item.Resultado9 = Math.Abs(Math.Round(item.Eficiencia9 != 0 ? ((item.Peso9 * item.Eficiencia9) / 100) : 0, 1));

                                                item.EficienciaPonderada9 = (item.Eficiencia9 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia9 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia9));
                                                item.ResultadoPonderado9 = Math.Abs(Math.Round(item.EficienciaPonderada9 != 0 ? ((item.Peso9 * item.EficienciaPonderada9) / 100) : 0, 1));

                                            }
                                            break;
                                        case "10":
                                            if (item.ValorEsperado10 != 0)
                                            {
                                                item.Realizado10 = valor;
                                                item.Eficiencia1 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado10.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                                item.Resultado10 = Math.Abs(Math.Round(item.Eficiencia10 != 0 ? ((item.Peso10 * item.Eficiencia10) / 100) : 0, 1));

                                                item.EficienciaPonderada10 = (item.Eficiencia10 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia10 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia10));
                                                item.ResultadoPonderado10 = Math.Abs(Math.Round(item.EficienciaPonderada10 != 0 ? ((item.Peso10 * item.EficienciaPonderada10) / 100) : 0, 1));

                                            }
                                            break;
                                        case "11":
                                            if (item.ValorEsperado11 != 0)
                                            {
                                                item.Realizado11 = valor;
                                                item.Eficiencia11 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado11.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                                item.Resultado11 = Math.Abs(Math.Round(item.Eficiencia11 != 0 ? ((item.Peso11 * item.Eficiencia11) / 100) : 0, 1));

                                                item.EficienciaPonderada11 = (item.Eficiencia11 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia11 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia11));
                                                item.ResultadoPonderado11 = Math.Abs(Math.Round(item.EficienciaPonderada11 != 0 ? ((item.Peso11 * item.EficienciaPonderada11) / 100) : 0, 1));

                                            }
                                            break;
                                        case "12":
                                            if (item.ValorEsperado12 != 0)
                                            {
                                                item.Realizado12 = valor;
                                                item.Eficiencia12 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", valor.ToString().Replace(".", "").Replace(",", "."))
                                                                                .Replace("Esperado", item.ValorEsperado12.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                                item.Resultado12 = Math.Abs(Math.Round(item.Eficiencia12 != 0 ? ((item.Peso12 * item.Eficiencia12) / 100) : 0, 1));

                                                item.EficienciaPonderada12 = (item.Eficiencia12 > _pontuacao.Base100 ? _pontuacao.Base100 : (item.Eficiencia12 < _pontuacao.Piso ? _pontuacao.Piso : item.Eficiencia12));
                                                item.ResultadoPonderado12 = Math.Abs(Math.Round(item.EficienciaPonderada12 != 0 ? ((item.Peso12 * item.EficienciaPonderada12) / 100) : 0, 1));

                                            }
                                            break;
                                    }                                    
                                }                                
                            }                           
                        }
                    }

                    resultadoAvaliacaoLabel.Text = "Resultado da Avaliação " + Math.Round(_listaItensAvaliacao.Sum(s => s.ResultadoPonderado) * _fatores.Fator, 2).ToString();

                }
                catch { }
            }
        }

        private void ReestruturaGrid()
        {
            if (_listaItensAvaliacao != null)
                _listaItensAvaliacao.Clear();

            referencia = new DateTime(Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(2, 4)), Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(0, 2)), 1);

            gridGroupingControl.DataSource = new List<ItensAvaliacaoMetasPeriodo>();

            int i = colunaPeso - 1;

            while (i >= 9)
            {
                gridGroupingControl.TableDescriptor.Columns.RemoveAt(i);
                i--;
            }

            gridGroupingControl.TableDescriptor.StackedHeaderRows.Clear();
            gridGroupingControl.Enabled = true;

        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            ReestruturaGrid();

            resultadoAvaliacaoLabel.Text = string.Empty;
            descricaoCargoTextBox.Text = string.Empty;
            referenciaMaskedEditBox.Text = string.Empty;
            referenciaFinalMaskedEditBox.Text = string.Empty;
            dataLimiteDateTimePicker.Value = DateTime.Now;
            nomeTextBox.Text = string.Empty;
            usuarioTextBox.Text = string.Empty;
            descricaoCargoTextBox.Text = string.Empty;
            ativoCheckBox.Enabled = true;
            ativoCheckBox.Checked = false;
            usuarioTextBox.Focus();
            pesquisaReferenciaButton.Enabled = false;
            pesquisaUsuarioButton.Enabled = false;
            gravarButton.Enabled = false;
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
                Publicas._escTeclado = false;
                return;
            }

            try
            {
                referenciaFinalMaskedEditBox.Text = string.Empty;
                ReestruturaGrid();
            }
            catch { };

            if (!ativoCheckBox.Checked) // se não estiver marcador para comparar referencias
            {
                if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
                {
                    Publicas._idSuperior = _colaboradores.Id;

                    Pesquisas.Feedback _pesquisa = new Pesquisas.Feedback("MN");
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

                _autoAvaliacao = new AutoAvaliacaoBO().Consultar(_colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), "MN", _empresa.IdEmpresa);

                _pontuacao = new PontuacaoBO().ConsultarMaiorReferencia(Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim()));

                if (_empresaDoColaborador.Existe && 
                    ((_empresaDoColaborador.Fim != DateTime.MinValue && _empresaDoColaborador.Fim <= Convert.ToDateTime("01/" + referenciaMaskedEditBox.Text).AddMonths(1).AddDays(-1)) || // Ultimo dia do mês
                     (_empresaDoColaborador.Inicio != DateTime.MinValue && _empresaDoColaborador.Inicio > Convert.ToDateTime("01/" + referenciaMaskedEditBox.Text))))
                {
                    new Notificacoes.Mensagem("Colaborador não habilitado para receber avaliação neste período para essa empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                if (!_pontuacao.Existe)
                {
                    new Notificacoes.Mensagem("Pontuação não cadastrada para essa referência.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                _fatores = new PontuacaoBO().Consultar(_pontuacao.Id, _empresa.IdEmpresa);
                
                _prazo = new PrazosBO().Consultar(DateTime.Now.Date, "MN", referenciaMaskedEditBox.ClipText.Trim());

                if (!_prazo.Existe)
                {
                    new Notificacoes.Mensagem("Prazo para " + Publicas.GetDescription(Publicas.TipoPrazos.MetasNumericas, "") + " não cadastrado para essa referência.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                dataLimiteDateTimePicker.Value = _prazo.Fim;

                if (_prazo.Fim.Date < DateTime.Now.Date)
                {
                    if (new Notificacoes.Mensagem("Prazo para " + Publicas.GetDescription(Publicas.TipoPrazos.MetasNumericas, "") + " finalizado em " + _prazo.Fim.Date.ToShortDateString() + "." +
                        Environment.NewLine + "Deseja consultar?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    {
                        referenciaMaskedEditBox.Focus();
                        return;
                    }
                }

                _listaItensAvaliacao = new AutoAvaliacaoBO().Listar(_colaboradores.IdCargo, _colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), _empresa.IdEmpresa, "");

                if (_listaItensAvaliacao.Count == 0)
                    _listaItensAvaliacao = new AutoAvaliacaoBO().Listar(_colaboradores.IdCargo, _colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), _empresa.IdEmpresa, "", true);

                gridGroupingControl.DataSource = _listaItensAvaliacao;

                gravarButton.Enabled = _prazo.Fim.Date >= DateTime.Now.Date && tituloLabel.Text.Contains("Definição") ;
                excluirButton.Enabled = _autoAvaliacao.Existe && _prazo.Fim.Date >= DateTime.Now.Date && tituloLabel.Text.Contains("Definição");
                //gridGroupingControl.Enabled = gravarButton.Enabled;

                resultadoAvaliacaoLabel.Visible = (_autoAvaliacao.DataFim != DateTime.MinValue && _autoAvaliacao.Existe);

                resultadoAvaliacaoLabel.Text = "Resultado da Avaliação " + Math.Round(_listaItensAvaliacao.Sum(s => s.ResultadoPonderado) * _fatores.Fator, 2) .ToString();

            }
        }

        private void pesquisaReferenciaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                Publicas._idSuperior = _colaboradores.Id;

                Pesquisas.Feedback _pesquisa = new Pesquisas.Feedback("MN");
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

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (referenciaFinalMaskedEditBox.Visible)
                    referenciaFinalMaskedEditBox.Focus();
                else
                    gridGroupingControl.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (usuarioTextBox.Enabled)
                    usuarioTextBox.Focus();
                else
                    ativoCheckBox.Focus();
            }
        }

        private void ativoCheckBox_Click(object sender, EventArgs e)
        {
            
        }

        private void ativoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            referenciaFinalMaskedEditBox.Visible = ativoCheckBox.Checked;
            referenciaFinalLabel.Visible = ativoCheckBox.Checked;
            excluirButton.Visible = !ativoCheckBox.Checked;
            copiarbutton.Visible = !ativoCheckBox.Checked && tituloLabel.Text.Contains("Definição");

            if (excluirButton.Visible)
            {
                gravarButton.Location = new Point(303, 14);
                limparButton.Location = new Point(438, 14);
                excluirButton.Location = new Point(573, 14);
            }
            else
            {
                gravarButton.Location = new Point(370, 14);
                limparButton.Location = new Point(505, 14);
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (usuarioTextBox.Enabled)
                    usuarioTextBox.Focus();
                else
                    referenciaMaskedEditBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void referenciaFinalMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void referenciaFinalMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            try
            {
                ReestruturaGrid();
            }
            catch { };

            try
            {
                _autoAvaliacao = new AutoAvaliacaoBO().Consultar(_colaboradores.Id, referenciaFinalMaskedEditBox.ClipText.Trim(), "MN", _empresa.IdEmpresa);
                _pontuacao = new PontuacaoBO().Consultar(Convert.ToInt32(referenciaFinalMaskedEditBox.ClipText.Trim()));

                if (!_pontuacao.Existe)
                {
                    new Notificacoes.Mensagem("Pontuação não cadastrado para essa referência.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaFinalMaskedEditBox.Focus();
                    return;
                }
                
                _prazo = new PrazosBO().Consultar(DateTime.Now.Date, "MN", referenciaFinalMaskedEditBox.ClipText.Trim());

                if (!_prazo.Existe)
                {
                    new Notificacoes.Mensagem(Publicas.GetDescription(Publicas.TipoPrazos.MetasNumericas, "") + " não cadastrado para essa referência.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaFinalMaskedEditBox.Focus();
                    return;
                }

                dataLimiteDateTimePicker.Value = _prazo.Fim;

                if (_prazo.Fim.Date < DateTime.Now.Date)
                {
                    if (new Notificacoes.Mensagem("Prazo para " + Publicas.GetDescription(Publicas.TipoPrazos.MetasNumericas, "") + " finalizado em " + _prazo.Fim.Date.ToShortDateString() + "." +
                        Environment.NewLine + "Deseja consultar?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    {
                        referenciaFinalMaskedEditBox.Focus();
                        return;
                    }
                }

                _listaItensAvaliacao = new AutoAvaliacaoBO().Listar(_colaboradores.IdCargo, _colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), _empresa.IdEmpresa, referenciaFinalMaskedEditBox.ClipText.Trim());

                ativoCheckBox.Enabled = false;
                gravarButton.Enabled = _prazo.Fim.Date >= DateTime.Now.Date && tituloLabel.Text.Contains("Definição") ;
                
                int idMeta = 0;
                int i = 0;
                string _formula = "";

                ItensAvaliacaoMetasPeriodo _mes = new ItensAvaliacaoMetasPeriodo();
                _listaItensPorPeriodo = new List<ItensAvaliacaoMetasPeriodo>();

                referenciaFim = new DateTime(Convert.ToInt32(referenciaFinalMaskedEditBox.ClipText.Trim().Substring(2, 4)), Convert.ToInt32(referenciaFinalMaskedEditBox.ClipText.Trim().Substring(0, 2)), 1);
                referencia = new DateTime(Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(2, 4)), Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(0, 2)), 1);

                foreach (var item in _listaItensAvaliacao.OrderBy(o => o.Referencia)
                                                         .OrderBy(o => o.IdMetas))
                {
                    referencia = new DateTime(Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(2, 4)), Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(0, 2)), 1);
                    i = 0;

                    if (idMeta == 0)
                    {
                        idMeta = item.IdMetas;
                    }

                    if (idMeta != item.IdMetas)
                    {
                        // Só adicionar quando mudar a descrição/idMeta
                        _listaItensPorPeriodo.Add(_mes);
                        idMeta = item.IdMetas;
                        _mes = new ItensAvaliacaoMetasPeriodo();
                        i = 0;
                    }

                    _mes.Descricao = item.Descricao;
                    _mes.IdMetas = item.IdMetas;
                    _mes.Perspectiva = item.Perspectiva;
                    _mes.DescricaoPerspectiva = item.DescricaoPerspectiva;

                    while (referencia <= referenciaFim)
                    {
                        _formula = _mes.Formula;
                        if (referencia.Month.ToString() + referencia.Year.ToString() == item.Referencia)
                        {
                            switch (i)
                            {
                                case 0:
                                    _mes.Referencia = item.Referencia;
                                    _mes.Peso = item.Peso;
                                    _mes.ValorEsperado = item.ValorEsperado;
                                    _mes.Realizado = item.Realizado;

                                    _mes.Eficiencia = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }

                                    _mes.Resultado = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia != 0 ? ((item.Peso / _mes.Eficiencia)-1) * 100 : item.Resultado,1));

                                    _mes.EficienciaPonderada = (_mes.Eficiencia > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia));
                                    _mes.ResultadoPonderado = Math.Abs(Math.Round(_mes.EficienciaPonderada != 0 ? ((_mes.Peso * _mes.EficienciaPonderada) / 100) : 0, 1));

                                    break;
                                case 1:
                                    _mes.Referencia1 = item.Referencia;
                                    _mes.Peso1 = item.Peso;
                                    _mes.Realizado1 = item.Realizado;
                                    _mes.ValorEsperado1 = item.ValorEsperado;
                                    _mes.Eficiencia1 = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia1 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }
                                    
                                    _mes.Resultado1 = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia1 != 0 ? ((item.Peso / _mes.Eficiencia1) - 1) * 100 : item.Resultado, 1));

                                    _mes.EficienciaPonderada1 = (_mes.Eficiencia1 > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia1 < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia1));
                                    _mes.ResultadoPonderado1 = Math.Abs(Math.Round(_mes.EficienciaPonderada1 != 0 ? ((_mes.Peso1 * _mes.EficienciaPonderada1) / 100) : 0, 1));

                                    break;
                                case 2:
                                    _mes.Referencia2 = item.Referencia;
                                    _mes.Peso2 = item.Peso;
                                    _mes.ValorEsperado2 = item.ValorEsperado;
                                    _mes.Realizado2 = item.Realizado;

                                    _mes.Eficiencia2 = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia2 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }
                                    
                                    _mes.Resultado2 = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia2 != 0 ? ((item.Peso / _mes.Eficiencia2) - 1) * 100 : item.Resultado, 1));

                                    _mes.EficienciaPonderada2 = (_mes.Eficiencia2 > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia2 < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia2));
                                    _mes.ResultadoPonderado2 = Math.Abs(Math.Round(_mes.EficienciaPonderada2 != 0 ? ((_mes.Peso2 * _mes.EficienciaPonderada2) / 100) : 0, 1));

                                    break;
                                case 3:
                                    _mes.Referencia3 = item.Referencia;
                                    _mes.Peso3 = item.Peso;
                                    _mes.ValorEsperado3 = item.ValorEsperado;
                                    _mes.Realizado3 = item.Realizado;

                                    _mes.Eficiencia3 = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia3 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }

                                    _mes.Resultado3 = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia3 != 0 ? ((item.Peso / _mes.Eficiencia3) - 1) * 100 : item.Resultado, 1));

                                    _mes.EficienciaPonderada3 = (_mes.Eficiencia3 > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia3 < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia3));
                                    _mes.ResultadoPonderado3 = Math.Abs(Math.Round(_mes.EficienciaPonderada3 != 0 ? ((_mes.Peso3 * _mes.EficienciaPonderada3) / 100) : 0, 1));

                                    break;
                                case 4:
                                    _mes.Referencia4 = item.Referencia;
                                    _mes.Peso4 = item.Peso;
                                    _mes.ValorEsperado4 = item.ValorEsperado;
                                    _mes.Realizado4 = item.Realizado;


                                    _mes.Eficiencia4 = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia4 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }
                                    _mes.Resultado4 = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia4 != 0 ? ((item.Peso / _mes.Eficiencia4) - 1) * 100 : item.Resultado, 1));

                                    _mes.EficienciaPonderada4 = (_mes.Eficiencia4 > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia4 < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia4));
                                    _mes.ResultadoPonderado4 = Math.Abs(Math.Round(_mes.EficienciaPonderada4 != 0 ? ((_mes.Peso4 * _mes.EficienciaPonderada4) / 100) : 0, 1));

                                    break;
                                case 5:
                                    _mes.Referencia5 = item.Referencia;
                                    _mes.Peso5 = item.Peso;
                                    _mes.ValorEsperado5 = item.ValorEsperado;
                                    _mes.Realizado5 = item.Realizado;
                                    
                                    _mes.Eficiencia5 = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia5 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }

                                    _mes.Resultado5 = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia5 != 0 ? ((item.Peso / _mes.Eficiencia5) - 1) * 100 : item.Resultado, 1));

                                    _mes.EficienciaPonderada5 = (_mes.Eficiencia5 > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia5 < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia5));
                                    _mes.ResultadoPonderado5 = Math.Abs(Math.Round(_mes.EficienciaPonderada5 != 0 ? ((_mes.Peso5 * _mes.EficienciaPonderada5) / 100) : 0, 1));

                                    break;
                                case 6:
                                    _mes.Referencia6 = item.Referencia;
                                    _mes.Peso6 = item.Peso;
                                    _mes.ValorEsperado6 = item.ValorEsperado;
                                    _mes.Realizado6 = item.Realizado;

                                    _mes.Eficiencia6 = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia6 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }

                                    _mes.Resultado6 = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia6 != 0 ? ((item.Peso / _mes.Eficiencia6) - 1) * 100 : item.Resultado, 1));

                                    _mes.EficienciaPonderada6 = (_mes.Eficiencia6 > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia6 < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia6));
                                    _mes.ResultadoPonderado6 = Math.Abs(Math.Round(_mes.EficienciaPonderada6 != 0 ? ((_mes.Peso6 * _mes.EficienciaPonderada6) / 100) : 0, 1));

                                    break;
                                case 7:
                                    _mes.Referencia7 = item.Referencia;
                                    _mes.Peso7 = item.Peso;
                                    _mes.Realizado7 = item.Realizado;
                                    _mes.ValorEsperado7 = item.ValorEsperado;

                                    _mes.Eficiencia7 = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia7 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }

                                    _mes.Resultado7 = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia7 != 0 ? ((item.Peso / _mes.Eficiencia7) - 1) * 100 : item.Resultado, 1));

                                    _mes.EficienciaPonderada7 = (_mes.Eficiencia7 > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia7 < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia7));
                                    _mes.ResultadoPonderado7 = Math.Abs(Math.Round(_mes.EficienciaPonderada7 != 0 ? ((_mes.Peso7 * _mes.EficienciaPonderada7) / 100) : 0, 1));

                                    break;
                                case 8:
                                    _mes.Referencia8 = item.Referencia;
                                    _mes.Peso8 = item.Peso;
                                    _mes.ValorEsperado8 = item.ValorEsperado;
                                    _mes.Realizado8 = item.Realizado;

                                    _mes.Eficiencia8 = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia8 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }

                                    _mes.Resultado8 = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia8 != 0 ? ((item.Peso / _mes.Eficiencia8) - 1) * 100 : item.Resultado, 1));

                                    _mes.EficienciaPonderada8 = (_mes.Eficiencia8 > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia8 < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia8));
                                    _mes.ResultadoPonderado8 = Math.Abs(Math.Round(_mes.EficienciaPonderada8 != 0 ? ((_mes.Peso8 * _mes.EficienciaPonderada8) / 100) : 0, 1));

                                    break;
                                case 9:
                                    _mes.Referencia9 = item.Referencia;
                                    _mes.Peso9 = item.Peso;
                                    _mes.ValorEsperado9 = item.ValorEsperado;
                                    _mes.Realizado9 = item.Realizado;

                                    _mes.Eficiencia9 = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia9 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }

                                    _mes.Resultado9 = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia9 != 0 ? ((item.Peso / _mes.Eficiencia9) - 1) * 100 : item.Resultado, 1));

                                    _mes.EficienciaPonderada9 = (_mes.Eficiencia9 > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia9 < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia9));
                                    _mes.ResultadoPonderado9 = Math.Abs(Math.Round(_mes.EficienciaPonderada9 != 0 ? ((_mes.Peso9 * _mes.EficienciaPonderada9) / 100) : 0, 1));

                                    break;
                                case 10:
                                    _mes.Referencia10 = item.Referencia;
                                    _mes.Peso10 = item.Peso;
                                    _mes.ValorEsperado10 = item.ValorEsperado;
                                    _mes.Realizado10 = item.Realizado;

                                    _mes.Eficiencia10 = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia10 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }

                                    _mes.Resultado10 = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia10 != 0 ? ((item.Peso / _mes.Eficiencia10) - 1) * 100 : item.Resultado, 1));

                                    _mes.EficienciaPonderada10 = (_mes.Eficiencia10 > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia10 < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia10));
                                    _mes.ResultadoPonderado10 = Math.Abs(Math.Round(_mes.EficienciaPonderada10 != 0 ? ((_mes.Peso10 * _mes.EficienciaPonderada10) / 100) : 0, 1));

                                    break;
                                case 11:
                                    _mes.Referencia11 = item.Referencia;
                                    _mes.Peso11 = item.Peso;
                                    _mes.ValorEsperado11 = item.ValorEsperado;
                                    _mes.Realizado11 = item.Realizado;

                                    _mes.Eficiencia10 = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia10 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }

                                    _mes.Resultado11 = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia11 != 0 ? ((item.Peso / _mes.Eficiencia11) - 1) * 100 : item.Resultado, 1));

                                    _mes.EficienciaPonderada11 = (_mes.Eficiencia11 > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia11 < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia11));
                                    _mes.ResultadoPonderado11 = Math.Abs(Math.Round(_mes.EficienciaPonderada11 != 0 ? ((_mes.Peso11 * _mes.EficienciaPonderada11) / 100) : 0, 1));

                                    break;
                                case 12:
                                    _mes.Referencia12 = item.Referencia;
                                    _mes.Peso12 = item.Peso;
                                    _mes.Realizado12 = item.Realizado;
                                    _mes.ValorEsperado12 = item.ValorEsperado;

                                    _mes.Eficiencia12 = item.Eficiencia;

                                    if (item.Eficiencia == 0 && item.ValorEsperado != 0)
                                    {
                                        _mes.Eficiencia12 = Math.Round(new ItensAvaliacaoMetaBO().CalculoFormula(_formula.Replace("Realizado", item.Realizado.ToString().Replace(".", "").Replace(",", "."))
                                                                                    .Replace("Esperado", item.ValorEsperado.ToString().Replace(".", "").Replace(",", "."))) * 100, 1);
                                    }

                                    _mes.Resultado12 = Math.Abs(Math.Round(item.Resultado == 0 && _mes.Eficiencia12 != 0 ? ((item.Peso / _mes.Eficiencia12) - 1) * 100 : item.Resultado, 1));

                                    _mes.EficienciaPonderada12 = (_mes.Eficiencia12 > _pontuacao.Base100 ? _pontuacao.Base100 : (_mes.Eficiencia12 < _pontuacao.Piso ? _pontuacao.Piso : _mes.Eficiencia12));
                                    _mes.ResultadoPonderado12 = Math.Abs(Math.Round(_mes.EficienciaPonderada12 != 0 ? ((_mes.Peso12 * _mes.EficienciaPonderada12) / 100) : 0, 1));

                                    break;
                            }
                        }
                        i++;
                        referencia = referencia.AddMonths(1);
                    }

                }

                if (_listaItensAvaliacao.Count > 0)
                    _listaItensPorPeriodo.Add(_mes);

                // prepara o grid
                string texto = "";

                referencia = new DateTime(Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(2, 4)), Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim().Substring(0, 2)), 1);
                int QtdColunas = 0;
                int ano = referencia.Year;

                if (ano != referenciaFim.Year)
                {
                    while (ano < referenciaFim.Year)
                    {
                        QtdColunas = QtdColunas + Convert.ToInt32(referencia.Year.ToString() + "12") - Convert.ToInt32(referencia.Year.ToString() + referencia.Month.ToString("00")) + 1;
                        ano++;
                    }
                    QtdColunas = QtdColunas + (Convert.ToInt32(referenciaFim.Year.ToString() + referenciaFim.Month.ToString("00")) - Convert.ToInt32(referenciaFim.Year.ToString() + "01")) + 1;
                }
                else
                {
                    QtdColunas = QtdColunas + (Convert.ToInt32(referenciaFim.Year.ToString() + referenciaFim.Month.ToString("00")) - Convert.ToInt32(referencia.Year.ToString() + referencia.Month.ToString("00"))) + 1;
                }

                if (QtdColunas > 17)
                {
                    new Notificacoes.Mensagem("Quantidade superior a 12 meses.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaFinalMaskedEditBox.Focus();
                    return;
                }

                i = 0;
                colunaPeso = 2;
                colunaEsperado = 7;
                colunaRealizado = 4;
                colunaEficiencia = 5;
                colunaEficienciaPonderada = 6;
                colunaResultado = 7;
                colunaResultadoPonderada = 8;

                gridGroupingControl.TableDescriptor.GroupedColumns.Clear();
                gridGroupingControl.TableDescriptor.VisibleColumns.Add("DescricaoPerspectiva");
                gridGroupingControl.TableDescriptor.VisibleColumns.Move(8, 1);

                GridStackedHeaderRowDescriptor stackedHeaderRowDescriptor;
                List<GridStackedHeaderDescriptor> _listaHeader = new List<GridStackedHeaderDescriptor>();

                Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderDescriptor gridStackedHeaderDescriptor1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderDescriptor();
                gridStackedHeaderDescriptor1.HeaderText = "Metas acordadas";
                gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor[] { new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("Descricao")
                                                                                                                                                          , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("DescricaoPerspectiva") });

                _listaHeader.Add(gridStackedHeaderDescriptor1);

                while (referencia <= referenciaFim)
                {
                    texto = referencia.Month.ToString("00") + "/" + referencia.Year.ToString("0000");

                    gridStackedHeaderDescriptor1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderDescriptor();
                    gridStackedHeaderDescriptor1.HeaderText = texto;

                    if (i == 0)
                    {
                        gridGroupingControl.TableDescriptor.Columns[0].ReadOnly = true; // descrição
                        gridGroupingControl.TableDescriptor.Columns[1].ReadOnly = true; // perpectiva
                        gridGroupingControl.TableDescriptor.Columns[colunaPeso].ReadOnly = true; // Peso
                        gridGroupingControl.TableDescriptor.Columns[colunaEsperado].ReadOnly = true; // Esperado
                        gridGroupingControl.TableDescriptor.Columns[colunaRealizado].ReadOnly = true; // Realizado
                        gridGroupingControl.TableDescriptor.Columns[colunaEficiencia].ReadOnly = true; // Eficiência
                        gridGroupingControl.TableDescriptor.Columns[colunaEficienciaPonderada].ReadOnly = true;
                        gridGroupingControl.TableDescriptor.Columns[colunaResultado].ReadOnly = true; // Resultado
                        gridGroupingControl.TableDescriptor.Columns[colunaResultadoPonderada].ReadOnly = true;

                        gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor[] { new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("Peso")
                                                                                                                                                                   , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("ValorEsperado")
                                                                                                                                                                   , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("Realizado")
                                                                                                                                                                   , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("Eficiencia")
                                                                                                                                                                   , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("EficienciaPonderada")
                                                                                                                                                                   , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("Resultado")
                                                                                                                                                                   , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("ResultadoPonderado")});
                    }
                    else
                    {
                        gridGroupingControl.TableDescriptor.Columns.Add("Peso" + i.ToString(), "Peso" + i.ToString());
                        gridGroupingControl.TableDescriptor.Columns[colunaPeso].HeaderText = "Peso";
                        gridGroupingControl.TableDescriptor.Columns[colunaPeso].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaPeso].Appearance.AnyRecordFieldCell.MaxLength = 7;
                        gridGroupingControl.TableDescriptor.Columns[colunaPeso].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns[colunaPeso].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaPeso].Width = 64;
                        gridGroupingControl.TableDescriptor.Columns[colunaPeso].ReadOnly = referencia != referenciaFim;

                        gridGroupingControl.TableDescriptor.Columns.Add("Esperado" + i.ToString(), "ValorEsperado" + i.ToString());
                        gridGroupingControl.TableDescriptor.Columns[colunaEsperado].HeaderText = "Esperado";
                        gridGroupingControl.TableDescriptor.Columns[colunaEsperado].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaEsperado].Appearance.AnyRecordFieldCell.MaxLength = 7;
                        gridGroupingControl.TableDescriptor.Columns[colunaEsperado].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns[colunaEsperado].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaEsperado].Width = 75;
                        gridGroupingControl.TableDescriptor.Columns[colunaEsperado].ReadOnly = referencia != referenciaFim;

                        gridGroupingControl.TableDescriptor.Columns.Add("Realizado" + i.ToString(), "Realizado" + i.ToString());
                        gridGroupingControl.TableDescriptor.Columns[colunaRealizado].HeaderText = "Realizado";
                        gridGroupingControl.TableDescriptor.Columns[colunaRealizado].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaRealizado].Appearance.AnyRecordFieldCell.MaxLength = 7;
                        gridGroupingControl.TableDescriptor.Columns[colunaRealizado].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns[colunaRealizado].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaRealizado].Width = 75;
                        gridGroupingControl.TableDescriptor.Columns[colunaRealizado].ReadOnly = referencia != referenciaFim;

                        gridGroupingControl.TableDescriptor.Columns.Add("Eficiencia" + i.ToString(), "Eficiencia" + i.ToString());
                        gridGroupingControl.TableDescriptor.Columns[colunaEficiencia].HeaderText = "% Eficiência";
                        gridGroupingControl.TableDescriptor.Columns[colunaEficiencia].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaEficiencia].Appearance.AnyRecordFieldCell.MaxLength = 7;
                        gridGroupingControl.TableDescriptor.Columns[colunaEficiencia].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns[colunaEficiencia].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaEficiencia].Width = 75;
                        gridGroupingControl.TableDescriptor.Columns[colunaEficiencia].ReadOnly = true;

                        gridGroupingControl.TableDescriptor.Columns.Add("EficienciaPonderada" + i.ToString(), "EficienciaPonderada" + i.ToString());
                        gridGroupingControl.TableDescriptor.Columns[colunaEficienciaPonderada].HeaderText = "% Eficiência Final";
                        gridGroupingControl.TableDescriptor.Columns[colunaEficienciaPonderada].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaEficienciaPonderada].Appearance.AnyRecordFieldCell.MaxLength = 7;
                        gridGroupingControl.TableDescriptor.Columns[colunaEficienciaPonderada].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns[colunaEficienciaPonderada].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaEficienciaPonderada].Width = 75;
                        gridGroupingControl.TableDescriptor.Columns[colunaEficienciaPonderada].ReadOnly = true;

                        gridGroupingControl.TableDescriptor.Columns.Add("Resultado" + i.ToString(), "Resultado" + i.ToString());
                        gridGroupingControl.TableDescriptor.Columns[colunaResultado].HeaderText = "Resultado";
                        gridGroupingControl.TableDescriptor.Columns[colunaResultado].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaResultado].Appearance.AnyRecordFieldCell.MaxLength = 7;
                        gridGroupingControl.TableDescriptor.Columns[colunaResultado].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns[colunaResultado].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaResultado].Width = 75;
                        gridGroupingControl.TableDescriptor.Columns[colunaResultado].ReadOnly = true;

                        gridGroupingControl.TableDescriptor.Columns.Add("ResultadoPonderado" + i.ToString(), "ResultadoPonderado" + i.ToString());
                        gridGroupingControl.TableDescriptor.Columns[colunaResultadoPonderada].HeaderText = "Resultado Final";
                        gridGroupingControl.TableDescriptor.Columns[colunaResultadoPonderada].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaResultadoPonderada].Appearance.AnyRecordFieldCell.MaxLength = 7;
                        gridGroupingControl.TableDescriptor.Columns[colunaResultadoPonderada].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns[colunaResultadoPonderada].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Columns[colunaResultadoPonderada].Width = 75;
                        gridGroupingControl.TableDescriptor.Columns[colunaResultadoPonderada].ReadOnly = true;


                        gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor[] { new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("Peso"+ i.ToString())
                                                                                                                                                                   , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("Esperado"+ i.ToString())
                                                                                                                                                                   , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("Realizado"+ i.ToString())
                                                                                                                                                                   , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("Eficiencia"+ i.ToString())
                                                                                                                                                                   , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("EficienciaPonderada"+ i.ToString())
                                                                                                                                                                   , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("Resultado"+ i.ToString())
                                                                                                                                                                   , new Syncfusion.Windows.Forms.Grid.Grouping.GridStackedHeaderVisibleColumnDescriptor("ResultadoPonderado"+ i.ToString()) });

                    }

                    _listaHeader.Add(gridStackedHeaderDescriptor1);

                    colunaPeso = colunaResultadoPonderada + 1;
                    colunaEsperado = colunaPeso + 1; 
                    colunaRealizado = colunaEsperado + 1;
                    colunaEficiencia = colunaRealizado + 1;
                    colunaEficienciaPonderada = colunaEficiencia + 1;
                    colunaResultado = colunaEficienciaPonderada + 1;
                    colunaResultadoPonderada = colunaResultado + 1;
                                        
                    i++;
                    referencia = referencia.AddMonths(1);
                }

                stackedHeaderRowDescriptor = new GridStackedHeaderRowDescriptor("Row1", _listaHeader.ToArray());

                this.gridGroupingControl.TableDescriptor.StackedHeaderRows.Add(stackedHeaderRowDescriptor);
                                
                try
                {
                    gridGroupingControl.DataSource = _listaItensPorPeriodo;
                }
                catch { }
            }
            catch { }
        }

        private void gridGroupingControl_TableControlCellMouseDown(object sender, GridTableControlCellMouseEventArgs e)
        {
            _colunaCorrente = gridGroupingControl.TableControl.CurrentCell;
        }

        private void gridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            _colunaCorrente = gridGroupingControl.TableControl.CurrentCell;
        }

        private void gridGroupingControl_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            _colunaCorrente = gridGroupingControl.TableControl.CurrentCell;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new AutoAvaliacaoBO().Excluir(_autoAvaliacao.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            int qtdColunas = 0;
            string nomeCampo = "";
            decimal total = 0;

            if (_autoAvaliacao == null)
                _autoAvaliacao = new AutoAvaliacao();

            _autoAvaliacao.IdColaborador = _colaboradores.Id;

            _autoAvaliacao.IdUsuario = Publicas._idUsuario;
            _autoAvaliacao.MesReferencia = Convert.ToInt32(ativoCheckBox.Checked ? referenciaFinalMaskedEditBox.ClipText.Trim() : referenciaMaskedEditBox.ClipText.Trim());
            _autoAvaliacao.Tipo = Publicas.TipoPrazos.MetasNumericas;
            _autoAvaliacao.TotalAvaliacao = Math.Round( _listaItensAvaliacao.Sum(s => s.ResultadoPonderado) * _fatores.Fator,2);
            _autoAvaliacao.IdEmpresa = _empresa.IdEmpresa;

            if (_listaItensAvaliacao.Where(w => w.Eficiencia == 0).Count() == 0)
                _autoAvaliacao.DataFim = DateTime.Now;

            if (!ativoCheckBox.Checked)
            {
                total = _listaItensAvaliacao.Sum(s => s.Peso);

                if (total > 100)
                {
                    new Notificacoes.Mensagem("Total do peso da referência " + referenciaMaskedEditBox.Text.Trim() + " maior que 100%.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }
            }
            else    // Opção comparar marcada
            {
                #region Buscar total da ultima referência para gravar, só grava se estiver 100%

                qtdColunas = gridGroupingControl.TableDescriptor.Columns.Count-5;
                nomeCampo = gridGroupingControl.TableDescriptor.Columns[qtdColunas].MappingName;

                switch (nomeCampo)
                {
                    case "Peso":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso);
                        break;
                    case "Peso1":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso1);
                        break;                                 
                    case "Peso2":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso2);
                        break;                                 
                    case "Peso3":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso3);
                        break;                                 
                    case "Peso4":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso4);
                        break;                                 
                    case "Peso5":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso5);
                        break;                                 
                    case "Peso6":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso6);
                        break;                                 
                    case "Peso7":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso7);
                        break;                                
                    case "Peso8":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso8);
                        break;
                    case "Peso9":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso9);
                        break;
                    case "Peso10":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso10);
                        break;
                    case "Peso11":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso11);
                        break;
                    case "Peso12":
                        total = _listaItensPorPeriodo.Sum(s => s.Peso12);
                        break;
                }

                if (total != 100)
                {
                    new Notificacoes.Mensagem("Total do peso da referência " + referenciaFinalMaskedEditBox.Text.Trim() + " diferente de 100%.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }

                _listaItensAvaliacao.Clear();

                foreach (var item in _listaItensPorPeriodo)
                {
                    ItensAvaliacaoMetas _metas = new ItensAvaliacaoMetas();

                    _metas.IdMetas = item.IdMetas;
                    _metas.IdAvaliacao = _autoAvaliacao.Id;

                    switch (nomeCampo)
                    {
                        case "Peso":
                            _metas.Peso = item.Peso;
                            _metas.ValorEsperado = item.ValorEsperado;
                            _metas.Realizado = item.Realizado;
                            _metas.Resultado = item.Resultado;
                            _metas.Eficiencia = item.Eficiencia;
                            break;
                        case "Peso1":
                            _metas.Peso = item.Peso1 ;
                            _metas.ValorEsperado = item.ValorEsperado1;
                            _metas.Realizado = item.Realizado1;
                            _metas.Resultado = item.Resultado1;
                            _metas.Eficiencia = item.Eficiencia1;
                            break;
                        case "Peso2":
                            _metas.Peso = item.Peso2 ;
                            _metas.ValorEsperado = item.ValorEsperado2;
                            _metas.Realizado = item.Realizado2;
                            _metas.Resultado = item.Resultado2;
                            _metas.Eficiencia = item.Eficiencia2;
                            break;
                        case "Peso3":
                            _metas.Peso = item.Peso3 ;
                            _metas.ValorEsperado = item.ValorEsperado3;
                            _metas.Realizado = item.Realizado3;
                            _metas.Resultado = item.Resultado3;
                            _metas.Eficiencia = item.Eficiencia3;
                            break;
                        case "Peso4":
                            _metas.Peso = item.Peso4 ;
                            _metas.ValorEsperado = item.ValorEsperado4;
                            _metas.Realizado = item.Realizado4;
                            _metas.Resultado = item.Resultado4;
                            _metas.Eficiencia = item.Eficiencia4;
                            break;
                        case "Peso5":
                            _metas.Peso = item.Peso5 ;
                            _metas.ValorEsperado = item.ValorEsperado5;
                            _metas.Realizado = item.Realizado5;
                            _metas.Resultado = item.Resultado5;
                            _metas.Eficiencia = item.Eficiencia5;
                            break;
                        case "Peso6":
                            _metas.Peso = item.Peso6 ;
                            _metas.ValorEsperado = item.ValorEsperado6;
                            _metas.Realizado = item.Realizado6;
                            _metas.Resultado = item.Resultado6;
                            _metas.Eficiencia = item.Eficiencia6;
                            break;
                        case "Peso7":
                            _metas.Peso = item.Peso7 ;
                            _metas.ValorEsperado = item.ValorEsperado7;
                            _metas.Realizado = item.Realizado7;
                            _metas.Resultado = item.Resultado7;
                            _metas.Eficiencia = item.Eficiencia7;
                            break;
                        case "Peso8":
                            _metas.Peso = item.Peso8 ;
                            _metas.ValorEsperado = item.ValorEsperado8;
                            _metas.Realizado = item.Realizado8;
                            _metas.Resultado = item.Resultado8;
                            _metas.Eficiencia = item.Eficiencia8;
                            break;
                        case "Peso9":
                            _metas.Peso = item.Peso9 ;
                            _metas.ValorEsperado = item.ValorEsperado9;
                            _metas.Realizado = item.Realizado9;
                            _metas.Resultado = item.Resultado9;
                            _metas.Eficiencia = item.Eficiencia9;
                            break;
                        case "Peso10":
                            _metas.Peso = item.Peso10;
                            _metas.ValorEsperado = item.ValorEsperado10;
                            _metas.Realizado = item.Realizado10;
                            _metas.Resultado = item.Resultado10;
                            _metas.Eficiencia = item.Eficiencia10;
                            break;
                        case "Peso11":
                            _metas.Peso = item.Peso11;
                            _metas.ValorEsperado = item.ValorEsperado11;
                            _metas.Realizado = item.Realizado11;
                            _metas.Resultado = item.Resultado11;
                            _metas.Eficiencia = item.Eficiencia11;
                            break;
                        case "Peso12":
                            _metas.Peso = item.Peso12;
                            _metas.ValorEsperado = item.ValorEsperado12;
                            _metas.Realizado = item.Realizado12;
                            _metas.Resultado = item.Resultado12;
                            _metas.Eficiencia = item.Eficiencia12;
                            break;
                    }

                    _listaItensAvaliacao.Add(_metas);
                }
                #endregion
            }

            if (!new AutoAvaliacaoBO().Gravar(_autoAvaliacao, null, _listaItensAvaliacao))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void copiarbutton_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void copiarbutton_Enter(object sender, EventArgs e)
        {
            copiarbutton.BackColor = Publicas._botaoFocado;
            copiarbutton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void copiarbutton_Validating(object sender, CancelEventArgs e)
        {
            copiarbutton.BackColor = usuarioTextBox.BackColor;
            copiarbutton.ForeColor = Publicas._fonteBotao;
        }

        private void copiarbutton_Click(object sender, EventArgs e)
        {
            Avaliacao_de_desempenho.CopiaDefinicaoDeMetas _tela = new Avaliacao_de_desempenho.CopiaDefinicaoDeMetas();
            _tela.empresaComboBoxAdv.Text = this.empresaComboBoxAdv.Text;
            _tela.usuarioTextBox.Text = this.usuarioTextBox.Text;
            _tela.nomeTextBox.Text = this.nomeTextBox.Text;
            _tela.descricaoCargoTextBox.Text = this.descricaoCargoTextBox.Text;
            _tela.referenciaMaskedEditBox.Text = this.referenciaMaskedEditBox.Text;
            _tela._empresa = this._empresa;
            _tela._listaItensAvaliacao = this._listaItensAvaliacao;
            _tela._cargos = this._cargos;
            _tela._colaborador = this._colaboradores;
            _tela.ShowDialog();
        }

        private void pesquisaUsuarioButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._idSuperior = (!tituloLabel.Text.Contains("Gestor") ? 0 : _colaboradoresSuperior.Id);

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

        private void usuarioTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void referenciaMaskedEditBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }
    }
}
