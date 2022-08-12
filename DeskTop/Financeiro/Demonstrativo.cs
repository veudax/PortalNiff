using Classes;
using Negocio;
using DynamicFilter;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Grouping;
using Syncfusion.Drawing;

namespace Suportte.Financeiro
{
    public partial class Demonstrativo : Form
    {
        public Demonstrativo()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                ValorAnteriorCurrencyTextBox.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : ReferenciaMaskedEditBox.ForeColor);
                ValorAtualCurrencyTextBox.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : ReferenciaMaskedEditBox.ForeColor);

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

        private class Valores
        {
            #region Campos
            public int Id { get; set; }
            public DateTime Data { get; set; }
            public decimal SaldoAnterior { get; set; }
            public decimal SaldoFinalPrevisto { get; set; }
            public decimal SaldoFinalRealizadoBCO { get; set; }
            public decimal SaldoFinalRealizadoGlobus { get; set; }

            public int Coluna1Id { get; set; }
            public string Coluna1Nome { get; set; }
            public decimal Coluna1Previsto { get; set; }
            public decimal Coluna1RealizadoBCO { get; set; }
            public decimal Coluna1RealizadoGlobus { get; set; }
            public string Coluna1MotivoPrevisto { get; set; }
            public string Coluna1MotivoRealizadoBCO { get; set; }
            public string Coluna1MotivoRealizadoGlobus { get; set; }
            public string Coluna1Operacao { get; set; }

            public int Coluna2Id { get; set; }
            public string Coluna2Nome { get; set; }
            public decimal Coluna2Previsto { get; set; }
            public decimal Coluna2RealizadoBCO { get; set; }
            public decimal Coluna2RealizadoGlobus { get; set; }
            public string Coluna2MotivoPrevisto { get; set; }
            public string Coluna2MotivoRealizadoBCO { get; set; }
            public string Coluna2MotivoRealizadoGlobus { get; set; }
            public string Coluna2Operacao { get; set; }

            public int Coluna3Id { get; set; }
            public string Coluna3Nome { get; set; }
            public decimal Coluna3Previsto { get; set; }
            public decimal Coluna3RealizadoBCO { get; set; }
            public decimal Coluna3RealizadoGlobus { get; set; }
            public string Coluna3MotivoPrevisto { get; set; }
            public string Coluna3MotivoRealizadoBCO { get; set; }
            public string Coluna3MotivoRealizadoGlobus { get; set; }
            public string Coluna3Operacao { get; set; }

            public int Coluna4Id { get; set; }
            public string Coluna4Nome { get; set; }
            public decimal Coluna4Previsto { get; set; }
            public decimal Coluna4RealizadoBCO { get; set; }
            public decimal Coluna4RealizadoGlobus { get; set; }
            public string Coluna4MotivoPrevisto { get; set; }
            public string Coluna4MotivoRealizadoBCO { get; set; }
            public string Coluna4MotivoRealizadoGlobus { get; set; }
            public string Coluna4Operacao { get; set; }

            public int Coluna5Id { get; set; }
            public string Coluna5Nome { get; set; }
            public decimal Coluna5Previsto { get; set; }
            public decimal Coluna5RealizadoBCO { get; set; }
            public decimal Coluna5RealizadoGlobus { get; set; }
            public string Coluna5MotivoPrevisto { get; set; }
            public string Coluna5MotivoRealizadoBCO { get; set; }
            public string Coluna5MotivoRealizadoGlobus { get; set; }
            public string Coluna5Operacao { get; set; }

            public int Coluna6Id { get; set; }
            public string Coluna6Nome { get; set; }
            public decimal Coluna6Previsto { get; set; }
            public decimal Coluna6RealizadoBCO { get; set; }
            public decimal Coluna6RealizadoGlobus { get; set; }
            public string Coluna6MotivoPrevisto { get; set; }
            public string Coluna6MotivoRealizadoBCO { get; set; }
            public string Coluna6MotivoRealizadoGlobus { get; set; }
            public string Coluna6Operacao { get; set; }

            public int Coluna7Id { get; set; }
            public string Coluna7Nome { get; set; }
            public decimal Coluna7Previsto { get; set; }
            public decimal Coluna7RealizadoBCO { get; set; }
            public decimal Coluna7RealizadoGlobus { get; set; }
            public string Coluna7MotivoPrevisto { get; set; }
            public string Coluna7MotivoRealizadoBCO { get; set; }
            public string Coluna7MotivoRealizadoGlobus { get; set; }
            public string Coluna7Operacao { get; set; }

            public int Coluna8Id { get; set; }
            public string Coluna8Nome { get; set; }
            public decimal Coluna8Previsto { get; set; }
            public decimal Coluna8RealizadoBCO { get; set; }
            public decimal Coluna8RealizadoGlobus { get; set; }
            public string Coluna8MotivoPrevisto { get; set; }
            public string Coluna8MotivoRealizadoBCO { get; set; }
            public string Coluna8MotivoRealizadoGlobus { get; set; }
            public string Coluna8Operacao { get; set; }

            public int Coluna9Id { get; set; }
            public string Coluna9Nome { get; set; }
            public decimal Coluna9Previsto { get; set; }
            public decimal Coluna9RealizadoBCO { get; set; }
            public decimal Coluna9RealizadoGlobus { get; set; }
            public string Coluna9MotivoPrevisto { get; set; }
            public string Coluna9MotivoRealizadoBCO { get; set; }
            public string Coluna9MotivoRealizadoGlobus { get; set; }
            public string Coluna9Operacao { get; set; }

            public int Coluna10Id { get; set; }
            public string Coluna10Nome { get; set; }
            public decimal Coluna10Previsto { get; set; }
            public decimal Coluna10RealizadoBCO { get; set; }
            public decimal Coluna10RealizadoGlobus { get; set; }
            public string Coluna10MotivoPrevisto { get; set; }
            public string Coluna10MotivoRealizadoBCO { get; set; }
            public string Coluna10MotivoRealizadoGlobus { get; set; }
            public string Coluna10Operacao { get; set; }

            public int Coluna11Id { get; set; }
            public string Coluna11Nome { get; set; }
            public decimal Coluna11Previsto { get; set; }
            public decimal Coluna11RealizadoBCO { get; set; }
            public decimal Coluna11RealizadoGlobus { get; set; }
            public string Coluna11MotivoPrevisto { get; set; }
            public string Coluna11MotivoRealizadoBCO { get; set; }
            public string Coluna11MotivoRealizadoGlobus { get; set; }
            public string Coluna11Operacao { get; set; }

            public int Coluna12Id { get; set; }
            public string Coluna12Nome { get; set; }
            public decimal Coluna12Previsto { get; set; }
            public decimal Coluna12RealizadoBCO { get; set; }
            public decimal Coluna12RealizadoGlobus { get; set; }
            public string Coluna12MotivoPrevisto { get; set; }
            public string Coluna12MotivoRealizadoBCO { get; set; }
            public string Coluna12MotivoRealizadoGlobus { get; set; }
            public string Coluna12Operacao { get; set; }

            public int Coluna13Id { get; set; }
            public string Coluna13Nome { get; set; }
            public decimal Coluna13Previsto { get; set; }
            public decimal Coluna13RealizadoBCO { get; set; }
            public decimal Coluna13RealizadoGlobus { get; set; }
            public string Coluna13MotivoPrevisto { get; set; }
            public string Coluna13MotivoRealizadoBCO { get; set; }
            public string Coluna13MotivoRealizadoGlobus { get; set; }
            public string Coluna13Operacao { get; set; }

            public int Coluna14Id { get; set; }
            public string Coluna14Nome { get; set; }
            public decimal Coluna14Previsto { get; set; }
            public decimal Coluna14RealizadoBCO { get; set; }
            public decimal Coluna14RealizadoGlobus { get; set; }
            public string Coluna14MotivoPrevisto { get; set; }
            public string Coluna14MotivoRealizadoBCO { get; set; }
            public string Coluna14MotivoRealizadoGlobus { get; set; }
            public string Coluna14Operacao { get; set; }

            public int Coluna15Id { get; set; }
            public string Coluna15Nome { get; set; }
            public decimal Coluna15Previsto { get; set; }
            public decimal Coluna15RealizadoBCO { get; set; }
            public decimal Coluna15RealizadoGlobus { get; set; }
            public string Coluna15MotivoPrevisto { get; set; }
            public string Coluna15MotivoRealizadoBCO { get; set; }
            public string Coluna15MotivoRealizadoGlobus { get; set; }
            public string Coluna15Operacao { get; set; }

            #endregion

            //public List<ArrecadacaoPrevisto> ArrecadacaoPrevisto { get; set; }
            //public List<ArrecadacaoRealizado> ArrecadacaoRealizado { get; set; }

            //public List<CRCPrevistoCalculo> MediaPrevistoContasReceber { get; set; }
            //public List<CRCPrevisto> ContasReceberPrevisto { get; set; }
            //public List<CRCRealizado> ContasReceberRealizado { get; set; }

            //public List<CRCPrevistoCalculo> MediaPrevistoContasPagar { get; set; }
            //public List<CRCPrevisto> ContasPagarPrevisto { get; set; }
            //public List<CRCRealizado> ContasPagarRealizado { get; set; }

            //public List<BancoPrevisto> MediaEntradaBancosPrevisto { get; set; }
            //public List<BancoPrevisto> MediaSaidaBancosPrevisto { get; set; }

            //public List<BancoRealizado> EntradaBancosPrevisto { get; set; }
            //public List<BancoRealizado> SaidaBancosPrevisto { get; set; }

            //public List<BancoRealizado> EntradaBancosRealizado { get; set; }
            //public List<BancoRealizado> SaidaBancosRealizado { get; set; }

            //public List<BancoRealizado> Entrada2BancosRealizado { get; set; }
            //public List<BancoRealizado> Saida2BancosRealizado { get; set; }

            public List<Historico> Historico { get; set; }

        }

        private class ArrecadacaoPrevisto
        {
            public DateTime Data { get; set; }
            public decimal Valor { get; set; }
            public decimal ValorPer { get; set; }
            public string Percentual { get; set; }
        }

        private class ArrecadacaoRealizado
        {
            public string Data { get; set; }
            public string Guia { get; set; }
            public decimal Valor { get; set; }
        }

        private class CRCPrevistoCalculo
        {
            public string Data { get; set; }
            public string Tipo { get; set; }
            public decimal Valor { get; set; }
            public decimal ValorPer { get; set; }
            public string Percentual { get; set; }
        }

        private class CRCPrevisto
        {
            public string Data { get; set; }
            public string Tipo { get; set; }
            public decimal Valor { get; set; }
            public string Documento { get; set; }
            public string Serie { get; set; }
            public string Fornecedor { get; set; }
            public string Status { get; set; }
            public decimal IdDocto { get; set; }
            public string VencimentoAnterior { get; set; }
            public int IdColuna { get; set; }
        }

        private class CRCRealizado
        {
            public string Data { get; set; }
            public string Tipo { get; set; }
            public decimal Valor { get; set; }
            public string Documento { get; set; }
            public string Serie { get; set; }
            public string Fornecedor { get; set; }
            public string Status { get; set; }
            public decimal IdDocto { get; set; }

        }

        private class BancoPrevisto
        {
            public DateTime Data { get; set; }
            public decimal Valor { get; set; }
            public decimal ValorPer { get; set; }
            public string Percentual { get; set; }
        }

        private class BancoRealizado
        {
            public DateTime Data { get; set; }
            public decimal Valor { get; set; }
            public string Documento { get; set; }
            public string Historico { get; set; }
        }

        private class Historico
        {
            public string Data { get; set; }
            public string Coluna { get; set; }
            public decimal Previsto { get; set; }
            public decimal Realizado { get; set; }
            public decimal RealizadoBCO { get; set; }
            public string MotivoPrevisto { get; set; }
            public string MotivoRealizadoBCO { get; set; }
            public string Usuario { get; set; }
        }

        Classes.Empresa _empresa;
        Classes.Empresa _empresaConsolidada;
        List<Classes.Empresa> _listaEmpresas;
        Classes.FeriadoEmenda _feriado;
        Classes.Financeiro.Bancos _banco;
        Classes.Financeiro.Colunas _coluna;
        Classes.Financeiro.Variaveis _variaveis;
        Classes.Financeiro.Demonstrativo _demonstrativo;

        List<Classes.Financeiro.ColunasDoBanco> _lista;
        List<Classes.Financeiro.ColunasDemonstrativo> _listaColunas;
        List<Classes.Financeiro.ColunasDemonstrativo> _listaColunasLog;
        List<Classes.Financeiro.HistoricoDemonstrativo> _listaHistorico;

        List<Classes.Financeiro.ColunasDemonstrativo> _listaPrevistoArrDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaRealizadaArrDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaCRCPrevistoDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaCRCPrevistoVenctoDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaCPGPrevistoDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaCPGPrevistoVenctoDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaCRCRealizadaDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaCPGRealizadaDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaEntradaBCOPrevistoDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaSaidaBCOPrevistoDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaEntradaBCORealizadaDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaSaidaBCORealizadaDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();

        List<Classes.Financeiro.ColunasDemonstrativo> _listaEntradaMovtoBCORealizadaDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaSaidaMovtoBCORealizadaDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();

        List<Classes.Financeiro.ColunasDemonstrativo> _listaEntradaBCOPrevistoNaDataDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaSaidaBCOPrevistoNaDataDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaEntradaBCORealizadaNaDataDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();
        List<Classes.Financeiro.ColunasDemonstrativo> _listaSaidaBCORealizadaNaDataDetalhada = new List<Classes.Financeiro.ColunasDemonstrativo>();


        List<CRCPrevisto> _listaCRCVencimentoAlterado = new List<CRCPrevisto>();
        List<CRCPrevisto> _listaCPGVencimentoAlterado = new List<CRCPrevisto>();

        List<Valores> _listaD;
        Valores _valores;
        
        GridCurrentCell _colunaCorrente;
        Element _elementoGridClicado;

        int idBancoParaConsolidar = 0;

        decimal _saldoInicial = 0;
        decimal _saldoFinal = 0;

        decimal _saldPrevisto = 0;
        decimal _saldoRealizadoBco = 0;
        decimal _saldoRealizadoGlobus = 0;
        bool temRealizadoBCO = false;

        string nomeColuna = "";
        string nomeColunaAux = "";
        string colunaSelecionada = "";
        string nomeTabela = "";
        string empresaParaConsolidar = "";

        DateTime _data = DateTime.MinValue;
        DateTime _dataFim = DateTime.MinValue;
        bool temAlteracao = false;

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

        private void Demonstrativo_Shown(object sender, EventArgs e)
        {
            EmpresaConsolidadaLabel.Text = "";
            this.Location = new Point(this.Left, 60);

            #region coloca os botões centralizados
            int espacoEntreBotoes = limparButton.Left - (gravarButton.Left + gravarButton.Width);

            gravarButton.Left = botoesPanel.Width / 3;
            limparButton.Left = gravarButton.Left + limparButton.Width + (espacoEntreBotoes * 2);
            #endregion

            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl.TableDescriptor.Columns[i].AllowFilter = false;
                gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            GridMetroColors metroColor = new GridMetroColors();
            metroColor.HeaderBottomBorderColor = Publicas._bordaEntrada;
            metroColor.HeaderBottomBorderWeight = GridBottomBorderWeight.ExtraThin;

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
            this.gridGroupingControl.Table.DefaultCaptionRowHeight = 27;
            this.gridGroupingControl.Table.DefaultRecordRowHeight = 27;
            this.gridGroupingControl.Table.DefaultColumnHeaderRowHeight = 35;
            this.gridGroupingControl.Table.DefaultRowHeaderWidth = 35;
        }

        private void Demonstrativo_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                BancoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void BancoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReprocessarPrevistoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void ReferenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ReprocessarPrevistoCheckBox.Focus();
            }
        }


        private void limparButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gridGroupingControl.Focus();
            }
        }

        private void BancoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void ReferenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void limparButton_Enter(object sender, EventArgs e)
        {
            limparButton.BackColor = Publicas._botaoFocado;
            limparButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _empresa = null;

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }

            if (_empresa == null)
            {
                new Notificacoes.Mensagem("Selecione a empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }
        }

        private void BancoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (BancoTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                new Pesquisas.Bancos().ShowDialog();

                BancoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (BancoTextBox.Text.Trim() == "" || BancoTextBox.Text.Trim() == "0")
                {
                    BancoTextBox.Text = string.Empty;
                    BancoTextBox.Focus();
                    return;
                }
            }

            _banco = new FinanceiroBO().ConsultarBancos(Convert.ToInt32(BancoTextBox.Text), _empresa.IdEmpresa);

            _lista = new List<Classes.Financeiro.ColunasDoBanco>();

            if (!_banco.Existe)
            {
                new Notificacoes.Mensagem("Banco não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                BancoTextBox.Focus();
                return;
            }

            if (!_banco.Ativo)
            {
                new Notificacoes.Mensagem("Banco inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                BancoTextBox.Focus();
                return;
            }

            NomeBancoTextBox.Text = _banco.Nome;
            _lista = new FinanceiroBO().ListarColunasDoBanco(_banco.Id, true);

            idBancoParaConsolidar = 0;
            empresaParaConsolidar = "";
            EmpresaConsolidadaLabel.Text = "";

            if (_banco.Consolidar)
            {
                // Busca as colunas que foram configuradas na empresa que ira consolidar com a da tela.
                _empresaConsolidada = new EmpresaBO().Consultar(_banco.IdEmpresaConsolidado);
                empresaParaConsolidar = _empresaConsolidada.CodigoEmpresaGlobus;
                idBancoParaConsolidar = _banco.IdBancoConsolidado;

                EmpresaConsolidadaLabel.Text = "Soma valores da empresa " + _empresaConsolidada.CodigoeNome + " junto com a empresa selecionada";
                try
                {
                    _lista.AddRange(new FinanceiroBO().ListarColunasDoBanco(_banco.IdBancoConsolidado, true));
                }
                catch { }
            }

            #region Colunas
            gridGroupingControl.TableDescriptor.Columns.Clear();
            gridGroupingControl.TableDescriptor.StackedHeaderRows.Clear();

            GridColumnDescriptor _col = new GridColumnDescriptor();
            GridStackedHeaderRowDescriptor stackedHeaderRowDescriptor;
            List<GridStackedHeaderDescriptor> _listaHeader = new List<GridStackedHeaderDescriptor>();
            GridStackedHeaderDescriptor gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();

            _col = new GridColumnDescriptor("Data", "Data", "Data");
            _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
            _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _col.Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
            _col.Appearance.AnyRecordFieldCell.Format = "d";
            _col.Width = 80;
            _col.AllowSort = true;
            _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
            _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
            gridGroupingControl.TableDescriptor.Columns.Add(_col);

            _col = new GridColumnDescriptor("SaldoAnterior", "SaldoAnterior", "Saldo Anterior");
            _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
            _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _col.AllowSort = false;

            _col.Width = 100;
            gridGroupingControl.TableDescriptor.Columns.Add(_col);

            gridStackedHeaderDescriptor1.HeaderText = " ";
            gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Data"),
                                                                                                                  new GridStackedHeaderVisibleColumnDescriptor("SaldoAnterior") });

            _listaHeader.Add(gridStackedHeaderDescriptor1);

            int i = 1;
            foreach (var item in _lista.GroupBy(g => new { g.IdColuna, g.Nome, g.Tipo })
                                       .Where(w => w.Key.Tipo == "EN")
                                       .OrderBy(o => o.Key.Nome))
            {
                _col = new GridColumnDescriptor("Coluna" + i.ToString() + "Previsto", "Coluna" + i.ToString() + "Previsto", "Previsto");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Width = 90;
                _col.AllowSort = false;
                gridGroupingControl.TableDescriptor.Columns.Add(_col);

                _col = new GridColumnDescriptor("Coluna" + i.ToString() + "RealizadoBCO", "Coluna" + i.ToString() + "RealizadoBCO", "Realizado Banco");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Width = 90;
                _col.AllowSort = false;
                gridGroupingControl.TableDescriptor.Columns.Add(_col);

                //_col = new GridColumnDescriptor("Coluna" + i.ToString() + "RealizadoGlobus", "Coluna" + i.ToString() + "RealizadoGlobus", "Realizado Globus");
                //_col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //_col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //_col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
                //_col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //_col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //_col.Width = 90;
                //_col.AllowSort = false;
                //gridGroupingControl.TableDescriptor.Columns.Add(_col);

                gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();
                gridStackedHeaderDescriptor1.Appearance.StackedHeaderCell.Font.Bold = true;
                gridStackedHeaderDescriptor1.Appearance.StackedHeaderCell.Font.Italic = true;
                gridStackedHeaderDescriptor1.HeaderText = item.Key.Nome;

                gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Coluna" + i.ToString() + "Previsto")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Coluna" + i.ToString() + "RealizadoBCO")
                                                                                                                //, new GridStackedHeaderVisibleColumnDescriptor("Coluna" + i.ToString() + "RealizadoGlobus")
                });
                _listaHeader.Add(gridStackedHeaderDescriptor1);
                i++;
            }

            foreach (var item in _lista.GroupBy(g => new { g.IdColuna, g.Nome, g.Tipo })
                                       .Where(w => w.Key.Tipo == "TE")
                                       .OrderBy(o => o.Key.Nome))
            {
                _col = new GridColumnDescriptor("Coluna" + i.ToString() + "Previsto", "Coluna" + i.ToString() + "Previsto", "Previsto");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Width = 90;
                _col.AllowSort = false;
                gridGroupingControl.TableDescriptor.Columns.Add(_col);

                _col = new GridColumnDescriptor("Coluna" + i.ToString() + "RealizadoBCO", "Coluna" + i.ToString() + "RealizadoBCO", "Realizado Banco");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Width = 90;
                _col.AllowSort = false;
                gridGroupingControl.TableDescriptor.Columns.Add(_col);

                //_col = new GridColumnDescriptor("Coluna" + i.ToString() + "RealizadoGlobus", "Coluna" + i.ToString() + "RealizadoGlobus", "Realizado Globus");
                //_col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //_col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //_col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
                //_col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //_col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //_col.Width = 90;
                //_col.AllowSort = false;
                //gridGroupingControl.TableDescriptor.Columns.Add(_col);

                gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();
                gridStackedHeaderDescriptor1.Appearance.StackedHeaderCell.Font.Bold = true;
                gridStackedHeaderDescriptor1.Appearance.StackedHeaderCell.Font.Italic = true;
                //FontStyle = FontStyle.Italic;
                gridStackedHeaderDescriptor1.HeaderText = item.Key.Nome;

                gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Coluna" + i.ToString() + "Previsto")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Coluna" + i.ToString() + "RealizadoBCO")
                    //, new GridStackedHeaderVisibleColumnDescriptor("Coluna" + i.ToString() + "RealizadoGlobus") 
                });
                _listaHeader.Add(gridStackedHeaderDescriptor1);
                i++;
            }

            foreach (var item in _lista.GroupBy(g => new { g.IdColuna, g.Nome, g.Tipo })
                           .Where(w => w.Key.Tipo == "SA")
                           .OrderBy(o => o.Key.Nome))
            {
                _col = new GridColumnDescriptor("Coluna" + i.ToString() + "Previsto", "Coluna" + i.ToString() + "Previsto", "Previsto");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Width = 90;
                _col.AllowSort = false;
                gridGroupingControl.TableDescriptor.Columns.Add(_col);

                _col = new GridColumnDescriptor("Coluna" + i.ToString() + "RealizadoBCO", "Coluna" + i.ToString() + "RealizadoBCO", "Realizado Banco");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Width = 90;
                _col.AllowSort = false;
                gridGroupingControl.TableDescriptor.Columns.Add(_col);

                //_col = new GridColumnDescriptor("Coluna" + i.ToString() + "RealizadoGlobus", "Coluna" + i.ToString() + "RealizadoGlobus", "Realizado Globus");
                //_col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //_col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //_col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
                //_col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //_col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //_col.Width = 90;
                //_col.AllowSort = false;
                //gridGroupingControl.TableDescriptor.Columns.Add(_col);

                gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();
                gridStackedHeaderDescriptor1.HeaderText = item.Key.Nome;

                gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Coluna" + i.ToString() + "Previsto")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Coluna" + i.ToString() + "RealizadoBCO")
                    //, new GridStackedHeaderVisibleColumnDescriptor("Coluna" + i.ToString() + "RealizadoGlobus") 
                });
                _listaHeader.Add(gridStackedHeaderDescriptor1);
                i++;
            }

            foreach (var item in _lista.GroupBy(g => new { g.IdColuna, g.Nome, g.Tipo })
                                       .Where(w => w.Key.Tipo == "TS")
                                       .OrderBy(o => o.Key.Nome))
            {
                _col = new GridColumnDescriptor("Coluna" + i.ToString() + "Previsto", "Coluna" + i.ToString() + "Previsto", "Previsto");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Width = 90;
                _col.AllowSort = false;
                gridGroupingControl.TableDescriptor.Columns.Add(_col);

                _col = new GridColumnDescriptor("Coluna" + i.ToString() + "RealizadoBCO", "Coluna" + i.ToString() + "RealizadoBCO", "Realizado Banco");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Width = 90;
                _col.AllowSort = false;
                gridGroupingControl.TableDescriptor.Columns.Add(_col);

                //_col = new GridColumnDescriptor("Coluna" + i.ToString() + "RealizadoGlobus", "Coluna" + i.ToString() + "RealizadoGlobus", "Realizado Globus");
                //_col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //_col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //_col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
                //_col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //_col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //_col.Width = 90;
                //_col.AllowSort = false;
                //gridGroupingControl.TableDescriptor.Columns.Add(_col);

                gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();
                gridStackedHeaderDescriptor1.HeaderText = item.Key.Nome;

                gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Coluna" + i.ToString() + "Previsto")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Coluna" + i.ToString() + "RealizadoBCO")
                    //, new GridStackedHeaderVisibleColumnDescriptor("Coluna" + i.ToString() + "RealizadoGlobus") 
                });
                _listaHeader.Add(gridStackedHeaderDescriptor1);
                i++;
            }

            _col = new GridColumnDescriptor("SaldoFinalPrevisto", "SaldoFinalPrevisto", "Previsto");
            _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
            _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _col.Width = 100;
            _col.AllowSort = false;
            gridGroupingControl.TableDescriptor.Columns.Add(_col);

            _col = new GridColumnDescriptor("SaldoFinalRealizadoBCO", "SaldoFinalRealizadoBCO", "Realizado BCO");
            _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
            _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
            _col.Width = 100;
            _col.AllowSort = false;
            gridGroupingControl.TableDescriptor.Columns.Add(_col);

            //_col = new GridColumnDescriptor("SaldoFinalRealizadoGlobus", "SaldoFinalRealizadoGlobus", "Realizado Globus");
            //_col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            //_col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
            //_col.Appearance.AnyRecordFieldCell.Format = "#,##0.00;-#,##0.00; #.##";//"n2";
            //_col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
            //_col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
            //_col.Width = 100;
            //_col.AllowSort = false;
            //gridGroupingControl.TableDescriptor.Columns.Add(_col);

            gridGroupingControl.TableDescriptor.FrozenColumn = "SaldoAnterior";

            gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();
            gridStackedHeaderDescriptor1.HeaderText = "Saldo Final";

            gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("SaldoFinalPrevisto"),
            new GridStackedHeaderVisibleColumnDescriptor("SaldoFinalRealizadoBCO"),
                //new GridStackedHeaderVisibleColumnDescriptor("SaldoFinalRealizadoGlobus")
            });
            _listaHeader.Add(gridStackedHeaderDescriptor1);

            //_listaHeader.Add(gridStackedHeaderDescriptor1);
            stackedHeaderRowDescriptor = new GridStackedHeaderRowDescriptor("Row1", _listaHeader.ToArray());
            this.gridGroupingControl.TableDescriptor.StackedHeaderRows.Add(stackedHeaderRowDescriptor);
            #endregion
        }

        private void ReferenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _listaD = new List<Valores>();

            _data = DateTime.MinValue;
            _dataFim = DateTime.MinValue;

            try
            {
                if (ReferenciaMaskedEditBox.ClipText.Trim().Length != 6)
                {
                    new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    ReferenciaMaskedEditBox.Focus();
                    return;
                }
                _data = Convert.ToDateTime("01/" + ReferenciaMaskedEditBox.Text);
                _dataFim = _data.AddMonths(1).AddDays(-1);
            }
            catch
            {
                new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ReferenciaMaskedEditBox.Focus();
                return;
            }

            int Mes = Convert.ToInt32(ReferenciaMaskedEditBox.Text.Substring(0, 2));
            int Ano = Convert.ToInt32(ReferenciaMaskedEditBox.Text.Substring(3, 4));

            // Mes Anterior para o Saldo
            if (Mes == 1)
            {
                Mes = 12; 
                Ano = Ano - 1;
            }
            else 
                Mes = Mes - 1;

            _demonstrativo = new FinanceiroBO().ConsultaDemonstrativo(_empresa.IdEmpresa, _banco.Id, Ano.ToString("0000") + Mes.ToString("00"));
            decimal _saldoFinalAnterior = _demonstrativo.SaldoFinal;

            // Mes Atual
            _demonstrativo = new FinanceiroBO().ConsultaDemonstrativo(_empresa.IdEmpresa, _banco.Id, ReferenciaMaskedEditBox.Text.Substring(3, 4) + ReferenciaMaskedEditBox.Text.Substring(0, 2));
            _listaColunas = new FinanceiroBO().ListarColunasDemonstrativo(_demonstrativo.Id);
            _listaColunasLog = new FinanceiroBO().ListarColunasDemonstrativo(_demonstrativo.Id);
            _listaHistorico = new FinanceiroBO().ListarHistoricoDemonstrativo(_demonstrativo.Id);

            _saldoInicial = (_saldoFinalAnterior == 0 ? _banco.SaldoInicial : _saldoFinalAnterior);
            _saldoFinal = _saldoInicial;

            _saldoRealizadoBco = 0;
            _saldPrevisto = 0;

            while (_data <= _dataFim)
            {
                int i = 1;
                _valores = new Valores();
                _valores.Data = _data;
                _valores.SaldoAnterior = _saldoFinal;

                _saldPrevisto = _valores.SaldoAnterior;
                if (_saldoRealizadoBco == 0 && i == 1)
                    _saldoRealizadoBco = _valores.SaldoAnterior;

                //_saldoRealizadoGlobus = _valores.SaldoAnterior;
                decimal _previsto = 0;
                decimal _realizado = 0;
                decimal _realizadoSalvo = 0;
                decimal _realizadoBco = 0;

                // verificar se data é feriado
                _feriado = new FeriadoBO().Consultar(_data, _empresa.IdEmpresa);

                ReferenciaMaskedEditBox.Cursor = Cursors.WaitCursor;
                mensagemSistemaLabel.Text = "Aguarde," + Environment.NewLine + "Pesquisando dia " + _data.ToShortDateString();
                this.Refresh();

                temRealizadoBCO = false;

                foreach (var item in _lista.GroupBy(g => new { g.IdColuna, g.Nome, g.Tipo })
                               .Where(w => w.Key.Tipo == "EN")
                               .OrderBy(o => o.Key.Nome))
                {
                    _previsto = 0;
                    _realizado = 0;
                    _realizadoSalvo = 0;
                    _realizadoBco = 0;
                    _variaveis = new FinanceiroBO().ConsultarVariaveisPelaEmpresaIdColuna(_empresa.IdEmpresa, item.Key.IdColuna);
                    _coluna = new FinanceiroBO().Consultar(item.Key.IdColuna);

                    if (!_demonstrativo.Existe || ReprocessarPrevistoCheckBox.Checked)
                    {
                        if (_variaveis.CalcularFinaisDeSemana ||
                            (!_variaveis.CalcularFinaisDeSemana && (_data.DayOfWeek != DayOfWeek.Saturday && _data.DayOfWeek != DayOfWeek.Sunday)))
                        {

                            if (_coluna.Origem == "ARR")
                                _previsto = new FinanceiroBO().ConsultarReceitaPrevista(_empresa.CodigoEmpresaGlobus
                                                                                       , _data
                                                                                       , _variaveis.Quantidade
                                                                                       , (_feriado.Existe ? !_variaveis.FeriadoPorAno : _variaveis.CalculoPor == "M")
                                                                                       , (_variaveis.Aumentar ? "A" : (_variaveis.Reduzir ? "R" : "N"))
                                                                                       , (_variaveis.Aumentar ? _variaveis.PercentualAumentar : (_variaveis.Reduzir ? _variaveis.PercentualReduzir : 0))
                                                                                       , _coluna.TipoOperacao
                                                                                       , empresaParaConsolidar);

                            if (_coluna.Origem == "BCRC")
                                _previsto = new FinanceiroBO().ConsultarContaReceberBancosPrevista(_empresa.CodigoEmpresaGlobus
                                                                                                  , _data
                                                                                                  , _variaveis.Quantidade
                                                                                                  , (_feriado.Existe ? !_variaveis.FeriadoPorAno : _variaveis.CalculoPor == "M")
                                                                                                  , (_variaveis.Aumentar ? "A" : (_variaveis.Reduzir ? "R" : "N"))
                                                                                                  , (_variaveis.Aumentar ? _variaveis.PercentualAumentar : (_variaveis.Reduzir ? _variaveis.PercentualReduzir : 0))
                                                                                                  , _banco.Id
                                                                                                  , item.Key.IdColuna
                                                                                                  , empresaParaConsolidar
                                                                                                  , idBancoParaConsolidar);

                            if (_coluna.Origem == "CRC")
                                _previsto = new FinanceiroBO().ConsultarContaReceberPrevista(_empresa.CodigoEmpresaGlobus
                                                                                            , _data
                                                                                            , _variaveis.Quantidade
                                                                                            , (_feriado.Existe ? !_variaveis.FeriadoPorAno : _variaveis.CalculoPor == "M")
                                                                                            , (_variaveis.Aumentar ? "A" : (_variaveis.Reduzir ? "R" : "N"))
                                                                                            , (_variaveis.Aumentar ? _variaveis.PercentualAumentar : (_variaveis.Reduzir ? _variaveis.PercentualReduzir : 0))
                                                                                            , _banco.Id
                                                                                            , item.Key.IdColuna
                                                                                            , empresaParaConsolidar
                                                                                            , idBancoParaConsolidar);

                            if (_coluna.Origem == "BCO")
                                _previsto = new FinanceiroBO().ConsultaMovtoBCOPrevisto(_empresa.CodigoEmpresaGlobus
                                                                                       , _data
                                                                                       , _variaveis.Quantidade
                                                                                       , (_feriado.Existe ? !_variaveis.FeriadoPorAno : _variaveis.CalculoPor == "M")
                                                                                       , (_variaveis.Aumentar ? "A" : (_variaveis.Reduzir ? "R" : "N"))
                                                                                       , (_variaveis.Aumentar ? _variaveis.PercentualAumentar : (_variaveis.Reduzir ? _variaveis.PercentualReduzir : 0))
                                                                                       , _banco.CodigoBanco
                                                                                       , _banco.CodigoAgencia
                                                                                       , _banco.CodigoConta
                                                                                       , "EN"
                                                                                       , _banco.Id
                                                                                       , item.Key.IdColuna
                                                                                       , empresaParaConsolidar
                                                                                       , idBancoParaConsolidar
                                    );
                        }
                    }

                    if (_demonstrativo.Existe)
                    {
                        foreach (var itemC in _listaColunas.Where(w => w.IdColuna == item.Key.IdColuna && w.Data.Date == _data.Date))
                        {
                            if (!ReprocessarPrevistoCheckBox.Checked)
                                _previsto = itemC.Previsto;
                            _realizadoBco = itemC.RealizadoBCO;
                            _realizadoSalvo = itemC.Realizado;
                        }

                        // não mostro mais o realizado do globus, entao não busca mais
                        #region Realizado
                        /*
                        if (_coluna.Origem == "CRC")
                        {
                            _listaCRCRealizadaDetalhada = new FinanceiroBO().ConsultarContasReceberRealizadaDetalhada(_empresa.CodigoEmpresaGlobus
                                                                                                                     , _data
                                                                                                                     , _banco.Id
                                                                                                                     , item.Key.IdColuna);
                            _realizado = _listaCRCRealizadaDetalhada.Sum(s => s.Previsto);
                        }
                        else
                        {
                            if (_coluna.Origem == "ARR")
                            {
                                _realizado = new FinanceiroBO().ConsultarReceitaRealizada(_empresa.CodigoEmpresaGlobus
                                                                                         , _data
                                                                                         , _coluna.TipoOperacao);
                                _listaRealizadaArrDetalhada = new FinanceiroBO().ConsultarReceitaRealizadaDetalhada(_empresa.CodigoEmpresaGlobus
                                                                                                                   , _data
                                                                                                                   , _coluna.TipoOperacao);
                            }
                            else
                            {
                                if (_coluna.Origem == "BCO")
                                    _listaEntradaMovtoBCORealizadaDetalhada = new FinanceiroBO().ConsultaMovtoBCORealizadoNaDataDetalhada((_banco.Consolidar ? _banco.CodigoEmpresaGlobus : _empresa.CodigoEmpresaGlobus)
                                                                                                                                         , _data
                                                                                                                                         , _banco.CodigoBanco
                                                                                                                                         , _banco.CodigoAgencia
                                                                                                                                         , _banco.CodigoConta
                                                                                                                                         , "EN"
                                                                                                                                         , _banco.Id
                                                                                                                                         , item.Key.IdColuna);
                                _realizado = _listaEntradaMovtoBCORealizadaDetalhada.Sum(s => s.Previsto);
                            }
                        } */
                        #endregion
                    }

                    //if (_coluna.Origem == "ARR")
                    //{
                    //    _listaPrevistoArrDetalhada = new FinanceiroBO().ConsultarReceitaPrevistaDetalhada(_empresa.CodigoEmpresaGlobus
                    //                                                                                     , _data
                    //                                                                                     , _variaveis.Quantidade
                    //                                                                                     , (_feriado.Existe ? !_variaveis.FeriadoPorAno : _variaveis.CalculoPor == "M")
                    //                                                                                     , (_variaveis.Aumentar ? "A" : (_variaveis.Reduzir ? "R" : "N"))
                    //                                                                                     , (_variaveis.Aumentar ? _variaveis.PercentualAumentar : (_variaveis.Reduzir ? _variaveis.PercentualReduzir : 0))
                    //                                                                                     , _coluna.TipoOperacao
                    //                                                                                     , empresaParaConsolidar);

                    //    if (_previsto < _listaPrevistoArrDetalhada.Sum(s => s.Previsto) && !_demonstrativo.Existe)
                    //    {
                    //        _previsto = _listaCRCPrevistoVenctoDetalhada.Sum(s => s.Previsto);
                    //    }
                    //}

                    //if (_coluna.Origem == "CRC")
                    //{
                    //    _listaCRCPrevistoDetalhada = new FinanceiroBO().ConsultarContaReceberDetalhada(_empresa.CodigoEmpresaGlobus
                    //                                                                                  , _data
                    //                                                                                  , _variaveis.Quantidade
                    //                                                                                  , (_feriado.Existe ? !_variaveis.FeriadoPorAno : _variaveis.CalculoPor == "M")
                    //                                                                                  , (_variaveis.Aumentar ? "A" : (_variaveis.Reduzir ? "R" : "N"))
                    //                                                                                  , (_variaveis.Aumentar ? _variaveis.PercentualAumentar : (_variaveis.Reduzir ? _variaveis.PercentualReduzir : 0))
                    //                                                                                  , _banco.Id
                    //                                                                                  , item.Key.IdColuna
                    //                                                                                  , empresaParaConsolidar
                    //                                                                                  , idBancoParaConsolidar);

                    //    _listaCRCPrevistoVenctoDetalhada = new FinanceiroBO().ConsultarContaReceberDetalhadaPeloVencimento(_empresa.CodigoEmpresaGlobus
                    //                                                                                                      , _data
                    //                                                                                                      , _variaveis.Quantidade
                    //                                                                                                      , (_feriado.Existe ? !_variaveis.FeriadoPorAno : _variaveis.CalculoPor == "M")
                    //                                                                                                      , (_variaveis.Aumentar ? "A" : (_variaveis.Reduzir ? "R" : "N"))
                    //                                                                                                      , (_variaveis.Aumentar ? _variaveis.PercentualAumentar : (_variaveis.Reduzir ? _variaveis.PercentualReduzir : 0))
                    //                                                                                                      , _banco.Id
                    //                                                                                                      , item.Key.IdColuna
                    //                                                                                                      , empresaParaConsolidar
                    //                                                                                                      , idBancoParaConsolidar);

                    //    if (_previsto < _listaCRCPrevistoVenctoDetalhada.Sum(s => s.Previsto) && !_demonstrativo.Existe)
                    //    {
                    //        _previsto = _listaCRCPrevistoVenctoDetalhada.Sum(s => s.Previsto);
                    //    }
                    //}

                    AtualizaValores(i, item.Key.IdColuna, _previsto, _realizadoBco, _realizado, "+");
                    i++;
                }

                foreach (var item in _lista.GroupBy(g => new { g.IdColuna, g.Nome, g.Tipo })
                               .Where(w => w.Key.Tipo == "TE")
                               .OrderBy(o => o.Key.Nome))
                {
                    _previsto = 0;
                    _realizado = 0;
                    _realizadoSalvo = 0;
                    _realizadoBco = 0;
                    _variaveis = new FinanceiroBO().ConsultarVariaveisPelaEmpresaIdColuna(_empresa.IdEmpresa, item.Key.IdColuna);
                    _coluna = new FinanceiroBO().Consultar(item.Key.IdColuna);

                    // para transferência não busca Previsto
                    #region Realizado

                    if (_demonstrativo.Existe)
                    {
                        foreach (var itemC in _listaColunas.Where(w => w.IdColuna == item.Key.IdColuna && w.Data.Date == _data.Date))
                        {
                            _previsto = itemC.Previsto;
                            _realizadoBco = itemC.RealizadoBCO;
                            _realizadoSalvo = itemC.Realizado;
                        }
                    //    if (_coluna.Origem == "BCO")
                    //    {
                    //        _listaEntradaBCORealizadaDetalhada = new FinanceiroBO().ConsultarTransferenciasRealizadaDetalhada((_banco.Consolidar ? _banco.CodigoEmpresaGlobus : _empresa.CodigoEmpresaGlobus)
                    //                                                                                                         , _data
                    //                                                                                                         , _banco.CodigoBanco
                    //                                                                                                         , _banco.CodigoAgencia
                    //                                                                                                         , _banco.CodigoConta
                    //                                                                                                         , "TE"
                    //                                                                                                         , empresaParaConsolidar);
                    //        _realizado = _listaEntradaBCORealizadaDetalhada.Sum(s => s.Previsto);
                    //    }
                    //    else
                    //    {
                    //        if (_coluna.Origem == "CRC")
                    //        {
                    //            _listaCRCRealizadaDetalhada = new FinanceiroBO().ConsultarContasReceberRealizadaDetalhada(_empresa.CodigoEmpresaGlobus
                    //                                                                                                     , _data
                    //                                                                                                     , _banco.Id
                    //                                                                                                     , item.Key.IdColuna);
                    //            _realizado = _listaCRCRealizadaDetalhada.Sum(s => s.Previsto);
                    //        }
                    //    }

                    }
                    #endregion


                    AtualizaValores(i, item.Key.IdColuna, _previsto, _realizadoBco, _realizado, "+");
                    i++;
                }

                foreach (var item in _lista.GroupBy(g => new { g.IdColuna, g.Nome, g.Tipo })
                               .Where(w => w.Key.Tipo == "SA")
                               .OrderBy(o => o.Key.Nome))
                {
                    _variaveis = new FinanceiroBO().ConsultarVariaveisPelaEmpresaIdColuna(_empresa.IdEmpresa, item.Key.IdColuna);
                    _coluna = new FinanceiroBO().Consultar(item.Key.IdColuna);

                    _previsto = 0;
                    _realizado = 0;
                    _realizadoSalvo = 0;
                    _realizadoBco = 0;

                    if (!_demonstrativo.Existe || ReprocessarPrevistoCheckBox.Checked)
                    {
                        if (_variaveis.CalcularFinaisDeSemana ||
                            (!_variaveis.CalcularFinaisDeSemana && (_data.DayOfWeek != DayOfWeek.Saturday && _data.DayOfWeek != DayOfWeek.Sunday)))
                        {

                            if (_coluna.Origem == "BCPG")
                                _previsto = new FinanceiroBO().ConsultarContaPagarBancosPrevista(_empresa.CodigoEmpresaGlobus
                                                                                          , _data
                                                                                          , _variaveis.Quantidade
                                                                                          , (_feriado.Existe ? !_variaveis.FeriadoPorAno : _variaveis.CalculoPor == "M")
                                                                                          , (_variaveis.Aumentar ? "A" : (_variaveis.Reduzir ? "R" : "N"))
                                                                                          , (_variaveis.Aumentar ? _variaveis.PercentualAumentar : (_variaveis.Reduzir ? _variaveis.PercentualReduzir : 0))
                                                                                          , _banco.Id
                                                                                          , item.Key.IdColuna
                                                                                          , empresaParaConsolidar
                                                                                          , idBancoParaConsolidar);

                            if (_coluna.Origem == "CPG")
                                _previsto = new FinanceiroBO().ConsultarContaPagarPrevista(_empresa.CodigoEmpresaGlobus
                                                                                          , _data
                                                                                          , _variaveis.Quantidade
                                                                                          , (_feriado.Existe ? !_variaveis.FeriadoPorAno : _variaveis.CalculoPor == "M")
                                                                                          , (_variaveis.Aumentar ? "A" : (_variaveis.Reduzir ? "R" : "N"))
                                                                                          , (_variaveis.Aumentar ? _variaveis.PercentualAumentar : (_variaveis.Reduzir ? _variaveis.PercentualReduzir : 0))
                                                                                          , _banco.Id
                                                                                          , item.Key.IdColuna
                                                                                          , empresaParaConsolidar
                                                                                          , idBancoParaConsolidar);

                            if (_coluna.Origem == "BCO")
                                _previsto = new FinanceiroBO().ConsultaMovtoBCOPrevisto((_banco.Consolidar ? _banco.CodigoEmpresaGlobus : _empresa.CodigoEmpresaGlobus)
                                                                                       , _data
                                                                                       , _variaveis.Quantidade
                                                                                       , (_feriado.Existe ? !_variaveis.FeriadoPorAno : _variaveis.CalculoPor == "M")
                                                                                       , (_variaveis.Aumentar ? "A" : (_variaveis.Reduzir ? "R" : "N"))
                                                                                       , (_variaveis.Aumentar ? _variaveis.PercentualAumentar : (_variaveis.Reduzir ? _variaveis.PercentualReduzir : 0))
                                                                                       , _banco.CodigoBanco
                                                                                       , _banco.CodigoAgencia
                                                                                       , _banco.CodigoConta
                                                                                       , "SA"
                                                                                       , _banco.Id
                                                                                       , item.Key.IdColuna
                                                                                       , empresaParaConsolidar
                                                                                       , idBancoParaConsolidar);

                        }
                    }

                    if (_demonstrativo.Existe)
                    {
                        foreach (var itemC in _listaColunas.Where(w => w.IdColuna == item.Key.IdColuna && w.Data.Date == _data.Date))
                        {
                            if (!ReprocessarPrevistoCheckBox.Checked)
                                _previsto = itemC.Previsto;

                            _realizadoBco = itemC.RealizadoBCO;
                            _realizadoSalvo = itemC.Realizado;
                        }
                        //if (_coluna.Origem == "CPG")
                        //{
                        //    _listaCPGRealizadaDetalhada = new FinanceiroBO().ConsultarContasPagarRealizadaDetalhada(_empresa.CodigoEmpresaGlobus
                        //                                                                                          , _data
                        //                                                                                          , _banco.Id
                        //                                                                                          , item.Key.IdColuna);
                        //    _realizado = _listaCPGRealizadaDetalhada.Sum(s => s.Previsto);
                        //}
                        //else
                        //{
                        //    if (_coluna.Origem == "BCO")
                        //        _listaSaidaMovtoBCORealizadaDetalhada = new FinanceiroBO().ConsultaMovtoBCORealizadoNaDataDetalhada((_banco.Consolidar ? _banco.CodigoEmpresaGlobus : _empresa.CodigoEmpresaGlobus)
                        //                                                                                                           , _data
                        //                                                                                                           , _banco.CodigoBanco
                        //                                                                                                           , _banco.CodigoAgencia
                        //                                                                                                           , _banco.CodigoConta
                        //                                                                                                           , "SA"
                        //                                                                                                           , _banco.Id
                        //                                                                                                           , item.Key.IdColuna
                        //                                                                                                           , empresaParaConsolidar
                        //                                                                                                           , idBancoParaConsolidar);
                        //    _realizado = _listaSaidaMovtoBCORealizadaDetalhada.Sum(s => s.Previsto);
                        //}
                    }

                    //if (_coluna.Origem == "CPG")
                    //{
                    //    _listaCPGPrevistoDetalhada = new FinanceiroBO().ConsultarContaPagarDetalhada(_empresa.CodigoEmpresaGlobus
                    //                                                                               , _data
                    //                                                                               , _variaveis.Quantidade
                    //                                                                               , (_feriado.Existe ? !_variaveis.FeriadoPorAno : _variaveis.CalculoPor == "M")
                    //                                                                               , (_variaveis.Aumentar ? "A" : (_variaveis.Reduzir ? "R" : "N"))
                    //                                                                               , (_variaveis.Aumentar ? _variaveis.PercentualAumentar : (_variaveis.Reduzir ? _variaveis.PercentualReduzir : 0))
                    //                                                                               , _banco.Id
                    //                                                                               , item.Key.IdColuna
                    //                                                                               , empresaParaConsolidar
                    //                                                                               , idBancoParaConsolidar);

                    //    _listaCPGPrevistoVenctoDetalhada = new FinanceiroBO().ConsultarContaPagarDetalhadaPeloVencimento(_empresa.CodigoEmpresaGlobus
                    //                                                                                                   , _data
                    //                                                                                                   , _variaveis.Quantidade
                    //                                                                                                   , (_feriado.Existe ? !_variaveis.FeriadoPorAno : _variaveis.CalculoPor == "M")
                    //                                                                                                   , (_variaveis.Aumentar ? "A" : (_variaveis.Reduzir ? "R" : "N"))
                    //                                                                                                   , (_variaveis.Aumentar ? _variaveis.PercentualAumentar : (_variaveis.Reduzir ? _variaveis.PercentualReduzir : 0))
                    //                                                                                                   , _banco.Id
                    //                                                                                                   , item.Key.IdColuna
                    //                                                                                                   , empresaParaConsolidar
                    //                                                                                                   , idBancoParaConsolidar);

                    //    if (_previsto < _listaCPGPrevistoVenctoDetalhada.Sum(s => s.Previsto) && !_demonstrativo.Existe)
                    //    {
                    //        _previsto = _listaCPGPrevistoVenctoDetalhada.Sum(s => s.Previsto);
                    //    }
                    //}

                    AtualizaValores(i, item.Key.IdColuna, _previsto, _realizadoBco, _realizado, "-");
                    i++;
                }

                foreach (var item in _lista.GroupBy(g => new { g.IdColuna, g.Nome, g.Tipo })
                               .Where(w => w.Key.Tipo == "TS")
                               .OrderBy(o => o.Key.Nome))
                {
                    _previsto = 0;
                    _realizado = 0;
                    _realizadoSalvo = 0;
                    _realizadoBco = 0;

                    _variaveis = new FinanceiroBO().ConsultarVariaveisPelaEmpresaIdColuna(_empresa.IdEmpresa, item.Key.IdColuna);
                    _coluna = new FinanceiroBO().Consultar(item.Key.IdColuna);

                    #region Realizado

                    if (_demonstrativo.Existe)
                    {
                        foreach (var itemC in _listaColunas.Where(w => w.IdColuna == item.Key.IdColuna && w.Data.Date == _data.Date))
                        {
                            _previsto = itemC.Previsto;
                            _realizadoBco = itemC.RealizadoBCO;
                            _realizadoSalvo = itemC.Realizado;
                        }
                    //    if (_coluna.Origem == "BCO")
                    //    {
                    //        _listaSaidaBCORealizadaDetalhada = new FinanceiroBO().ConsultarTransferenciasRealizadaDetalhada((_banco.Consolidar ? _banco.CodigoEmpresaGlobus : _empresa.CodigoEmpresaGlobus)
                    //                                                                                                       , _data
                    //                                                                                                       , _banco.CodigoBanco
                    //                                                                                                       , _banco.CodigoAgencia
                    //                                                                                                       , _banco.CodigoConta
                    //                                                                                                       , "TS");
                    //        _realizado = _listaSaidaBCORealizadaDetalhada.Sum(s => s.Previsto);
                    //    }
                    //    else
                    //    {
                    //        if (_coluna.Origem == "CPG")
                    //        {
                    //            _listaCPGRealizadaDetalhada = new FinanceiroBO().ConsultarContasPagarRealizadaDetalhada(_empresa.CodigoEmpresaGlobus
                    //                                                                                                  , _data
                    //                                                                                                  , _banco.Id
                    //                                                                                                  , item.Key.IdColuna);
                    //            _realizado = _listaCPGRealizadaDetalhada.Sum(s => s.Previsto);
                    //        }
                    //    }
                    }
                    #endregion


                    AtualizaValores(i, item.Key.IdColuna, _previsto, _realizadoBco, _realizado, "-");
                    i++;
                }

                _valores.SaldoFinalPrevisto = _saldPrevisto;
                _valores.SaldoFinalRealizadoBCO = _saldoRealizadoBco;
                _valores.SaldoFinalRealizadoGlobus = _saldoRealizadoGlobus;

                if (!_demonstrativo.Existe)
                    _saldoFinal = _saldPrevisto;
                else
                {
                    decimal realizadoAuxiliar = _listaColunas.Where(w => w.Data >= _data).Sum(s => s.RealizadoBCO);

                    if (_saldoRealizadoBco != _saldoFinal && temRealizadoBCO)
                        _saldoFinal = _saldoRealizadoBco;
                    else
                    {
                        if (realizadoAuxiliar == 0)
                            _saldoFinal = _saldPrevisto;
                    }
                }

                if (_listaHistorico.Where(w => w.Data.Date == _data.Date).Count() != 0)
                    _valores.Historico = new List<Historico>();

                foreach (var item in _listaHistorico.Where(w => w.Data.Date == _data.Date))
                {
                    foreach (var itemC in _listaColunas.Where(w => w.Id == item.IdColunaDemonstrativo))
                    {
                        _coluna = new FinanceiroBO().Consultar(itemC.IdColuna);
                    }

                    Usuario _usu = new UsuarioBO().ConsultarPorId(item.IdUsuario);
                    _valores.Historico.Add(new Historico() { Data = item.DataAlteracao.ToString(), Previsto = item.Previsto, Realizado = item.Realizado, RealizadoBCO = item.RealizadoBCO, MotivoPrevisto = item.MotivoPrevisto, MotivoRealizadoBCO = item.MotivoRealizado, Usuario = _usu.Nome, Coluna = _coluna.Nome });
                }

                _listaD.Add(_valores);
                _data = _data.AddDays(1);
                this.Refresh();
            }

            gridGroupingControl.DataSource = new List<Valores>();
            gridGroupingControl.DataSource = _listaD;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Relations.Count; i++)
            {

                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.TableOptions.ShowRowHeader = false;
                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.AllowEdit = false;
                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.AllowNew = false;
                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.AllowRemove = false;
            }

            if (gridGroupingControl.TableDescriptor.Relations.Count != 0)
            {

                #region Historicos
                for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns.Count; j++)
                {
                    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].AllowFilter = false;
                    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                    if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                    {
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Width = 120;
                    }
                    if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("Coluna"))
                    {
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Coluna";
                    }

                    if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Previsto"))
                    {
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Previsto";
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Width = 100;
                    }

                    if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Realizado"))
                    {
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName);

                        //gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                        //gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        //gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        //gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                        //gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Realizado";
                        //gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Width = 100;
                    }
                    if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("RealizadoBCO"))
                    {
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Realizado Banco";
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Width = 110;
                    }
                    if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("MotivoP"))
                    {
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Motivo da alteração do valor Previsto";
                    }
                    if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("MotivoR"))
                    {
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Motivo da alteração do valor Realizado Banco";
                    }
                    if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("usuario"))
                    {
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Quem fez a Alteração";
                    }
                }

                gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Históricos das Alterações";

                #endregion

                #region Previsto Arrecadação -- não tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Data Viagem";
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("ValorPer"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Vr. Calculado";
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].MappingName.Contains("Percentual"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Columns[j].HeaderText = "% Aplicado";
                //    }
                //}

                //gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Arrecadação - Média Prevista de Feria";

                //gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.DarkGreen;

                //if (gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor1 = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor1.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor1.DataMember = "ValorPer";
                //    summaryColumnDescriptor1.Format = "{Sum}";
                //    summaryColumnDescriptor1.Name = "ValorPer";
                //    summaryColumnDescriptor1.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor1 });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[0].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion

                #region Realizado Arrecadação -- nao tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].HeaderText = "Data Viagem";
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].MappingName.Contains("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].MappingName.Contains("Guia"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].HeaderText = "Guia";
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Columns[j].Width = 150;
                //    }
                //}

                //try
                //{
                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.GroupedColumns.Add("Data");
                //}
                //catch { }

                //gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Appearance.GroupPreviewCell.Format = "d";

                //gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.VisibleColumns.Remove("Data");
                //gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Arrecadação - Feria Realizada";

                //gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.DarkGreen;

                //if (gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.AutoSize = true;
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;
                //    _soma.Appearance.AnyCell.AutoSize = true;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //    gridGroupingControl.TableDescriptor.Relations[1].ChildTableDescriptor.Appearance.GroupCaptionCell.AutoSize = true;
                //}
                #endregion

                #region Média Previsto Contas a Receber -- nao tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].MappingName.Contains("Tipo"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].HeaderText = "Tipo";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].MappingName.Contains("ValorPer"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].HeaderText = "Vr. Calculado";
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].MappingName.Contains("Percentual"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Columns[j].HeaderText = "% Aplicado";
                //    }
                //}

                //gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Contas a Receber - Média Prevista";

                //gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.DarkOrange;

                //if (gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor1 = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor1.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor1.DataMember = "ValorPer";
                //    summaryColumnDescriptor1.Format = "{Sum}";
                //    summaryColumnDescriptor1.Name = "ValorPer";
                //    summaryColumnDescriptor1.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor1 });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[2].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion

                #region Previsto Contas a Receber -- nao tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].MappingName.Contains("Status") ||
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].MappingName.Contains("IdDocto") ||
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].MappingName.Contains("IdColuna"))
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].MappingName);

                //    if (gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].MappingName.Contains("Vencimento"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].HeaderText = "Vencimento Anterior";
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].MappingName.Contains("Tipo"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].HeaderText = "Tipo";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Width = 100;
                //    }

                //    if (gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].MappingName.Contains("Documento"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].HeaderText = "Documento";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].MappingName.Contains("Serie"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].HeaderText = "Serie";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].MappingName.Contains("Fornecedor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Columns[j].HeaderText = "Cliente";
                //    }

                //}

                //gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Contas a Receber - Previstos já cadastrados";

                //gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.DarkOrange;

                //try
                //{
                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.GroupedColumns.Add("Data");
                //}
                //catch { }

                //if (gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[3].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion

                #region Realizado Contas a Receber -- não tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Contains("Status") ||
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Contains("IdDocto") ||
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Contains("IdColuna"))
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName);

                //    if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Contains("Tipo"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].HeaderText = "Tipo";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Contains("Documento"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].HeaderText = "Documento";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Contains("Serie"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].HeaderText = "Serie";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Contains("Fornecedor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].HeaderText = "Cliente";
                //    }
                //}

                //gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Contas a Receber - Realizados";

                //try
                //{
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.GroupedColumns.Add("Data");
                //}
                //catch { }

                //gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.DarkOrange;

                //if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion

                #region Média Previsto Contas a Pagar -- não tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Contains("Tipo"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].HeaderText = "Tipo";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Contains("ValorPer"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].HeaderText = "Vr. Calculado";
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].MappingName.Contains("Percentual"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Columns[j].HeaderText = "% Aplicado";
                //    }
                //}

                //gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Contas a Pagar - Média Prevista";

                //gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.IndianRed;

                //if (gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor1 = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor1.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor1.DataMember = "ValorPer";
                //    summaryColumnDescriptor1.Format = "{Sum}";
                //    summaryColumnDescriptor1.Name = "ValorPer";
                //    summaryColumnDescriptor1.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor1 });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[4].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion

                #region Previsto Contas a Pagar -- não tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].MappingName.Contains("Status") ||
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].MappingName.Contains("IdDocto") ||
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].MappingName.Contains("IdColuna"))
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].MappingName);

                //    if (gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].MappingName.Contains("Vencimento"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].HeaderText = "Vencimento Anterior";
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].MappingName.Contains("Tipo"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].HeaderText = "Tipo";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //}

                //gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Contas a Pagar - Previstos já cadastrados";

                //try
                //{
                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.GroupedColumns.Add("Data");
                //}
                //catch { }

                //gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.IndianRed;

                //if (gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[5].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion

                #region Realizado Contas a Pagar -- não tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].MappingName.Contains("Status") ||
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].MappingName.Contains("IdDocto") ||
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].MappingName.Contains("IdColuna"))
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].MappingName);

                //    if (gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].MappingName.Contains("Tipo"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].HeaderText = "Tipo";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //}

                //gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Contas a Pagar - Realizados";

                //try
                //{
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.GroupedColumns.Add("Data");
                //}
                //catch { }

                //gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.IndianRed;

                //if (gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion

                #region Média Previsto Entradas Bancos -- nao tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].MappingName.Contains("ValorPer"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].HeaderText = "Vr. Calculado";
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].MappingName.Contains("Percentual"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Columns[j].HeaderText = "% Aplicado";
                //    }
                //}

                //gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Transferência Bancos - Média Prevista de Entrada ";

                //gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.BlueViolet;

                //if (gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor1 = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor1.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor1.DataMember = "ValorPer";
                //    summaryColumnDescriptor1.Format = "{Sum}";
                //    summaryColumnDescriptor1.Name = "ValorPer";
                //    summaryColumnDescriptor1.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor1 });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[6].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion

                #region Média Previsto Saidas Bancos -- nao tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].MappingName.Contains("ValorPer"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].HeaderText = "Vr. Calculado";
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].MappingName.Contains("Percentual"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Columns[j].HeaderText = "% Aplicado";
                //    }
                //}

                //gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Transferência Bancos - Média Prevista de Saída ";

                //gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.BlueViolet;

                //if (gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor1 = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor1.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor1.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor1.DataMember = "ValorPer";
                //    summaryColumnDescriptor1.Format = "{Sum}";
                //    summaryColumnDescriptor1.Name = "ValorPer";
                //    summaryColumnDescriptor1.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor, summaryColumnDescriptor1 });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[7].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion

                #region Previsto Entradas Bancos -- não tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].MappingName.Contains("Documento"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].HeaderText = "Documento";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].MappingName.Contains("Historico"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Columns[j].HeaderText = "Histórico";
                //    }
                //}

                //gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Transferência Bancos - Previsto de Entrada ";

                //gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.BlueViolet;

                //if (gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[8].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion

                #region Previsto Saidas Bancos -- nao tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].MappingName.Contains("Documento"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].HeaderText = "Documento";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].MappingName.Contains("Historico"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Columns[j].HeaderText = "Histórico";
                //    }
                //}

                //gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Transferência Bancos - Previsto de Saída ";

                //gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.BlueViolet;

                //if (gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[9].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion

                #region Realizado Entradas Bancos -- não tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].MappingName.Contains("Documento"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].HeaderText = "Documento";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].MappingName.Contains("Historico"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Columns[j].HeaderText = "Histórico";
                //    }
                //}

                //gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Transferência Bancos - Realizado de Entrada ";

                //gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.BlueViolet;

                //if (gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[12].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion

                #region Realizado Entradas Bancos -- não tem mais
                //for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns.Count; j++)
                //{
                //    gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].AllowFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                //    gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Regular));

                //    gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                //    if (gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].MappingName.Contains("Data"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "d";
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].HeaderText = "Data";
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].MappingName.Equals("Valor"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Format = "n2";
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].HeaderText = "Valor";
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Width = 100;
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].MappingName.Contains("Documento"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].HeaderText = "Documento";
                //    }
                //    if (gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].MappingName.Contains("Historico"))
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(empresaComboBoxAdv.Font.FontFamily, (float)8, FontStyle.Bold));
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Columns[j].HeaderText = "Histórico";
                //    }
                //}

                //gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.TopLevelGroupOptions.CaptionText = "Transferência Bancos - Realizado de Saída ";

                //gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Appearance.GroupCaptionCell.TextColor = Color.BlueViolet;

                //if (gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.SummaryRows.Count == 0)
                //{
                //    GridSummaryRowDescriptor _soma;

                //    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    summaryColumnDescriptor.Appearance.AnySummaryCell.Format = "n2";
                //    summaryColumnDescriptor.Appearance.GroupCaptionSummaryCell.Format = "n2";
                //    summaryColumnDescriptor.DataMember = "Valor";
                //    summaryColumnDescriptor.Format = "{Sum}";
                //    summaryColumnDescriptor.Name = "Valor";
                //    summaryColumnDescriptor.SummaryType = SummaryType.DoubleAggregate;

                //    _soma = new GridSummaryRowDescriptor("Sum", "Total",
                //    new GridSummaryColumnDescriptor[] { summaryColumnDescriptor });

                //    _soma.Appearance.SummaryTitleCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.SummaryTitleCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                //    _soma.Appearance.AnyCell.VerticalAlignment = GridVerticalAlignment.Middle;
                //    _soma.Appearance.AnyCell.Font.FontStyle = FontStyle.Bold;

                //    try
                //    {
                //        gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.SummaryRows.Add(_soma);
                //    }
                //    catch { }

                //    gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.ChildGroupOptions.ShowCaptionSummaryCells = true;
                //    gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.ChildGroupOptions.ShowSummaries = false;
                //    gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.ChildGroupOptions.CaptionSummaryRow = "Sum";
                //    gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Appearance.GroupCaptionCell.BackColor = gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Appearance.RecordFieldCell.BackColor;
                //    gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Appearance.GroupCaptionCell.Borders.Top = new GridBorder(GridBorderStyle.Standard);
                //    gridGroupingControl.TableDescriptor.Relations[13].ChildTableDescriptor.Appearance.GroupCaptionCell.CellType = "Static";
                //}
                #endregion
                            
            }

            ReferenciaMaskedEditBox.Cursor = Cursors.Default;
            mensagemSistemaLabel.Text = "";
            this.Refresh();

        }

        private void AtualizaValores(int i, int IdColuna, decimal previsto, decimal realizadoBco, decimal realizado, string operacao)
        {
            if (operacao.Equals("+"))
            {
                _saldPrevisto = _saldPrevisto + previsto;
                _saldoRealizadoBco = _saldoRealizadoBco + realizadoBco;
                _saldoRealizadoGlobus = _saldoRealizadoGlobus + realizado;
            }
            else
            {
                _saldPrevisto = _saldPrevisto - previsto;
                _saldoRealizadoBco = _saldoRealizadoBco - realizadoBco;
                _saldoRealizadoGlobus = _saldoRealizadoGlobus - realizado;
            }

            temRealizadoBCO = temRealizadoBCO || realizadoBco != 0;

            switch (i)
            {
                case 1:
                    _valores.Coluna1Id = IdColuna;
                    _valores.Coluna1Previsto = previsto;
                    _valores.Coluna1RealizadoBCO = realizadoBco;
                    _valores.Coluna1RealizadoGlobus = realizado;
                    _valores.Coluna1Operacao = operacao;                    
                    break;
                case 2:
                    _valores.Coluna2Id = IdColuna;
                    _valores.Coluna2Previsto = previsto;
                    _valores.Coluna2RealizadoBCO = realizadoBco;
                    _valores.Coluna2RealizadoGlobus = realizado;
                    _valores.Coluna2Operacao = operacao;                    
                    break;
                case 3:
                    _valores.Coluna3Id = IdColuna;
                    _valores.Coluna3Previsto = previsto;
                    _valores.Coluna3RealizadoBCO = realizadoBco;
                    _valores.Coluna3RealizadoGlobus = realizado;
                    _valores.Coluna3Operacao = operacao;
                    break;
                case 4:
                    _valores.Coluna4Id = IdColuna;
                    _valores.Coluna4Previsto = previsto;
                    _valores.Coluna4RealizadoBCO = realizadoBco;
                    _valores.Coluna4RealizadoGlobus = realizado;
                    _valores.Coluna4Operacao = operacao;
                    break;
                case 5:
                    _valores.Coluna5Id = IdColuna;
                    _valores.Coluna5Previsto = previsto;
                    _valores.Coluna5RealizadoBCO = realizadoBco;
                    _valores.Coluna5RealizadoGlobus = realizado;
                    _valores.Coluna5Operacao = operacao;
                    break;
                case 6:
                    _valores.Coluna6Id = IdColuna;
                    _valores.Coluna6Previsto = previsto;
                    _valores.Coluna6RealizadoBCO = realizadoBco;
                    _valores.Coluna6RealizadoGlobus = realizado;
                    _valores.Coluna6Operacao = operacao;
                    break;
                case 7:
                    _valores.Coluna7Id = IdColuna;
                    _valores.Coluna7Previsto = previsto;
                    _valores.Coluna7RealizadoBCO = realizadoBco;
                    _valores.Coluna7RealizadoGlobus = realizado;
                    _valores.Coluna7Operacao = operacao;
                    break;
                case 8:
                    _valores.Coluna8Id = IdColuna;
                    _valores.Coluna8Previsto = previsto;
                    _valores.Coluna8RealizadoBCO = realizadoBco;
                    _valores.Coluna8RealizadoGlobus = realizado;
                    _valores.Coluna8Operacao = operacao;
                    break;
                case 9:
                    _valores.Coluna9Id = IdColuna;
                    _valores.Coluna9Previsto = previsto;
                    _valores.Coluna9RealizadoBCO = realizadoBco;
                    _valores.Coluna9RealizadoGlobus = realizado;
                    _valores.Coluna9Operacao = operacao;
                    break;
                case 10:
                    _valores.Coluna10Id = IdColuna;
                    _valores.Coluna10Previsto = previsto;
                    _valores.Coluna10RealizadoBCO = realizadoBco;
                    _valores.Coluna10RealizadoGlobus = realizado;
                    _valores.Coluna10Operacao = operacao;
                    break;
                case 11:
                    _valores.Coluna11Id = IdColuna;
                    _valores.Coluna11Previsto = previsto;
                    _valores.Coluna11RealizadoBCO = realizadoBco;
                    _valores.Coluna11RealizadoGlobus = realizado;
                    _valores.Coluna11Operacao = operacao;
                    break;
                case 12:
                    _valores.Coluna12Id = IdColuna;
                    _valores.Coluna12Previsto = previsto;
                    _valores.Coluna12RealizadoBCO = realizadoBco;
                    _valores.Coluna12RealizadoGlobus = realizado;
                    _valores.Coluna12Operacao = operacao;
                    break;
                case 13:
                    _valores.Coluna13Id = IdColuna;
                    _valores.Coluna13Previsto = previsto;
                    _valores.Coluna13RealizadoBCO = realizadoBco;
                    _valores.Coluna13RealizadoGlobus = realizado;
                    _valores.Coluna13Operacao = operacao;
                    break;
                case 14:
                    _valores.Coluna14Id = IdColuna;
                    _valores.Coluna14Previsto = previsto;
                    _valores.Coluna14RealizadoBCO = realizadoBco;
                    _valores.Coluna14RealizadoGlobus = realizado;
                    _valores.Coluna14Operacao = operacao;
                    break;
                case 15:
                    _valores.Coluna15Id = IdColuna;
                    _valores.Coluna15Previsto = previsto;
                    _valores.Coluna15RealizadoBCO = realizadoBco;
                    _valores.Coluna15RealizadoGlobus = realizado;
                    _valores.Coluna15Operacao = operacao;
                    break;
            }
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            limparButton.BackColor = Publicas._botao;
            limparButton.ForeColor = Publicas._fonteBotao;
        }

        private void PesquisaBancoButton_Click(object sender, EventArgs e)
        {
            if (BancoTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                new Pesquisas.Bancos().ShowDialog();

                BancoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (BancoTextBox.Text.Trim() == "" || BancoTextBox.Text.Trim() == "0")
                {
                    BancoTextBox.Text = string.Empty;
                    BancoTextBox.Focus();
                    return;
                }

                BancoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            if (temAlteracao)
            {
                if (new Notificacoes.Mensagem("Deseja realmente fechar a tela?" + Environment.NewLine +
                    "Existem alterações não gravadas ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                    Close();
            }
            else
                Close();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            temAlteracao = false;
            EmpresaConsolidadaLabel.Text = "";
            gridGroupingControl.DataSource = new List<Valores>();

            BancoTextBox.Text = string.Empty;
            NomeBancoTextBox.Text = string.Empty;
            ReprocessarPrevistoCheckBox.Checked = false;
            BancoTextBox.Focus();

        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            foreach (var item in _listaD.Where(w => w.Data == Convert.ToDateTime("01/"+ReferenciaMaskedEditBox.Text)))
            {
                _saldoInicial = item.SaldoAnterior;
            }

            foreach (var item in _listaD.Where(w => w.Data == _dataFim))
            {
                _saldoFinal = item.SaldoFinalRealizadoBCO;
            }

            if (_demonstrativo == null)
                _demonstrativo = new Classes.Financeiro.Demonstrativo();

            _demonstrativo.IdBanco = _banco.Id;
            _demonstrativo.IdEmpresa = _empresa.IdEmpresa;
            _demonstrativo.Referencia = ReferenciaMaskedEditBox.Text.Substring(3, 4) + ReferenciaMaskedEditBox.Text.Substring(0, 2);  
            _demonstrativo.SaldoInicial = _saldoInicial;
            _demonstrativo.SaldoFinal = _saldoFinal;

            if (_listaColunas == null)
                _listaColunas = new List<Classes.Financeiro.ColunasDemonstrativo>();

            if (_listaHistorico == null)
                _listaHistorico = new List<Classes.Financeiro.HistoricoDemonstrativo>();

            var id = 1;
            string motivoPrevisto = "";
            string motivoRealizado = "";
            decimal _previstoAntes = 0;
            decimal _realizadoAntes = 0;

            foreach (var item in _listaD.OrderBy(o => o.Data))
            {
                mensagemSistemaLabel.Text = "Aguarde, Preparando para " + Environment.NewLine + "gravar dia " + item.Data.ToShortDateString();
                this.Refresh();

                for (int i = 1; i <= 15; i++)
                {
                    
                    Classes.Financeiro.ColunasDemonstrativo _col = new Classes.Financeiro.ColunasDemonstrativo();
                    _col.Data = item.Data;
                    _col.IdDemonstrativo = _demonstrativo.Id;

                    motivoPrevisto = "";
                    motivoRealizado = "";

                    switch (i)
                    {
                        case 1: _col.IdColuna = item.Coluna1Id;
                            _col.Previsto = item.Coluna1Previsto;
                            _col.Realizado = item.Coluna1RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna1RealizadoBCO;
                            motivoPrevisto = item.Coluna1MotivoPrevisto;
                            motivoRealizado = item.Coluna1MotivoRealizadoBCO;
                            break;
                        case 2:
                            _col.IdColuna = item.Coluna2Id;
                            _col.Previsto = item.Coluna2Previsto;
                            _col.Realizado = item.Coluna2RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna2RealizadoBCO;
                            motivoPrevisto = item.Coluna2MotivoPrevisto;
                            motivoRealizado = item.Coluna2MotivoRealizadoBCO;
                            break;
                        case 3:
                            _col.IdColuna = item.Coluna3Id;
                            _col.Previsto = item.Coluna3Previsto;
                            _col.Realizado = item.Coluna3RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna3RealizadoBCO;
                            motivoPrevisto = item.Coluna3MotivoPrevisto;
                            motivoRealizado = item.Coluna3MotivoRealizadoBCO;

                            break;
                        case 4:
                            _col.IdColuna = item.Coluna4Id;
                            _col.Previsto = item.Coluna4Previsto;
                            _col.Realizado = item.Coluna4RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna4RealizadoBCO;
                            motivoPrevisto = item.Coluna4MotivoPrevisto;
                            motivoRealizado = item.Coluna4MotivoRealizadoBCO;
                            break;
                        case 5:
                            _col.IdColuna = item.Coluna5Id;
                            _col.Previsto = item.Coluna5Previsto;
                            _col.Realizado = item.Coluna5RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna5RealizadoBCO;
                            motivoPrevisto = item.Coluna5MotivoPrevisto;
                            motivoRealizado = item.Coluna5MotivoRealizadoBCO;
                            break;
                        case 6:
                            _col.IdColuna = item.Coluna6Id;
                            _col.Previsto = item.Coluna6Previsto;
                            _col.Realizado = item.Coluna6RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna6RealizadoBCO;
                            motivoPrevisto = item.Coluna6MotivoPrevisto;
                            motivoRealizado = item.Coluna6MotivoRealizadoBCO;
                            break;
                        case 7:
                            _col.IdColuna = item.Coluna7Id;
                            _col.Previsto = item.Coluna7Previsto;
                            _col.Realizado = item.Coluna7RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna7RealizadoBCO;
                            motivoPrevisto = item.Coluna7MotivoPrevisto;
                            motivoRealizado = item.Coluna7MotivoRealizadoBCO;
                            break;
                        case 8:
                            _col.IdColuna = item.Coluna8Id;
                            _col.Previsto = item.Coluna8Previsto;
                            _col.Realizado = item.Coluna8RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna8RealizadoBCO;
                            motivoPrevisto = item.Coluna8MotivoPrevisto;
                            motivoRealizado = item.Coluna8MotivoRealizadoBCO;
                            break;
                        case 9:
                            _col.IdColuna = item.Coluna9Id;
                            _col.Previsto = item.Coluna9Previsto;
                            _col.Realizado = item.Coluna9RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna9RealizadoBCO;
                            motivoPrevisto = item.Coluna9MotivoPrevisto;
                            motivoRealizado = item.Coluna9MotivoRealizadoBCO;
                            break;
                        case 10:
                            _col.IdColuna = item.Coluna10Id;
                            _col.Previsto = item.Coluna10Previsto;
                            _col.Realizado = item.Coluna10RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna10RealizadoBCO;
                            motivoPrevisto = item.Coluna10MotivoPrevisto;
                            motivoRealizado = item.Coluna10MotivoRealizadoBCO;
                            break;
                        case 11:
                            _col.IdColuna = item.Coluna11Id;
                            _col.Previsto = item.Coluna11Previsto;
                            _col.Realizado = item.Coluna11RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna11RealizadoBCO;
                            motivoPrevisto = item.Coluna11MotivoPrevisto;
                            motivoRealizado = item.Coluna11MotivoRealizadoBCO;
                            break;
                        case 12:
                            _col.IdColuna = item.Coluna12Id;
                            _col.Previsto = item.Coluna12Previsto;
                            _col.Realizado = item.Coluna12RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna12RealizadoBCO;
                            motivoPrevisto = item.Coluna12MotivoPrevisto;
                            motivoRealizado = item.Coluna12MotivoRealizadoBCO;
                            break;
                        case 13:
                            _col.IdColuna = item.Coluna13Id;
                            _col.Previsto = item.Coluna13Previsto;
                            _col.Realizado = item.Coluna13RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna13RealizadoBCO;
                            motivoPrevisto = item.Coluna13MotivoPrevisto;
                            motivoRealizado = item.Coluna13MotivoRealizadoBCO;
                            break;
                        case 14:
                            _col.IdColuna = item.Coluna14Id;
                            _col.Previsto = item.Coluna14Previsto;
                            _col.Realizado = item.Coluna14RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna14RealizadoBCO;
                            motivoPrevisto = item.Coluna14MotivoPrevisto;
                            motivoRealizado = item.Coluna14MotivoRealizadoBCO;
                            break;
                        case 15:
                            _col.IdColuna = item.Coluna15Id;
                            _col.Previsto = item.Coluna15Previsto;
                            _col.Realizado = item.Coluna15RealizadoGlobus;
                            _col.RealizadoBCO = item.Coluna15RealizadoBCO;
                            motivoPrevisto = item.Coluna15MotivoPrevisto;
                            motivoRealizado = item.Coluna15MotivoRealizadoBCO;
                            break;

                    }

                    _previstoAntes = 0;
                    _realizadoAntes = 0;

                    foreach (var itemC in _listaColunas.Where(w => w.IdColuna == _col.IdColuna && w.Data == _col.Data))
                    {
                        _previstoAntes = itemC.Previsto;
                        _realizadoAntes = itemC.RealizadoBCO;
                    }

                    if ((_col.Previsto != 0 || _previstoAntes != 0) || (_col.RealizadoBCO != 0 || _realizadoAntes != 0))
                    {
                        id++;
                        _col.Id = id;

                        bool encontrou = false;
                        foreach (var itemC in _listaColunas.Where(w => w.IdColuna == _col.IdColuna && w.Data == _col.Data))
                        {
                            itemC.Previsto = Math.Round(_col.Previsto,2);
                            itemC.Realizado = Math.Round(_col.Realizado,2);
                            itemC.RealizadoBCO = Math.Round(_col.RealizadoBCO,2);
                            encontrou = true;
                        }

                        if (!encontrou)
                        {
                            _listaColunas.Add(_col);

                            Classes.Financeiro.HistoricoDemonstrativo _histo = new Classes.Financeiro.HistoricoDemonstrativo();
                            _histo.IdColunaDemonstrativo = id;
                            _histo.IdUsuario = Publicas._usuario.Id;
                            _histo.Previsto = Math.Round(_col.Previsto, 2);
                            _histo.Realizado = Math.Round(_col.Realizado,2);
                            _histo.RealizadoBCO = Math.Round(_col.RealizadoBCO,2);
                            _histo.MotivoPrevisto = motivoPrevisto;
                            _histo.MotivoRealizado = motivoRealizado;
                            _listaHistorico.Add(_histo);
                        }

                        foreach (var itemH in _listaColunasLog.Where(w => w.IdColuna == _col.IdColuna && w.Data == _col.Data))
                        {
                            if (Math.Round(itemH.Previsto, 2) != Math.Round(_col.Previsto, 2) ||
                                Math.Round(itemH.Realizado, 2) != Math.Round(_col.Realizado, 2) || 
                                Math.Round(itemH.RealizadoBCO, 2) != Math.Round(_col.RealizadoBCO, 2))
                            {
                                Classes.Financeiro.HistoricoDemonstrativo _histo = new Classes.Financeiro.HistoricoDemonstrativo();
                                _histo.IdColunaDemonstrativo = itemH.Id;
                                _histo.IdUsuario = Publicas._usuario.Id;
                                _histo.Previsto = Math.Round(_col.Previsto, 2);
                                _histo.Realizado = Math.Round(_col.Realizado,2);
                                _histo.RealizadoBCO = Math.Round(_col.RealizadoBCO, 2);
                                _histo.MotivoPrevisto = motivoPrevisto;
                                _histo.MotivoRealizado = motivoRealizado;
                                _listaHistorico.Add(_histo);
                            }
                        }
                    }
                }
            }

            string[,] crc = new string[_listaCRCVencimentoAlterado.Count(),2];
            string[,] cpg = new string[_listaCPGVencimentoAlterado.Count(),2];

            int x = 0;
            string logCRC = "";
            string logCPG = "";
            string logValores = "";


            foreach (var item in _listaCRCVencimentoAlterado)
            {
                crc[x, 0] = item.Data.ToString();
                crc[x, 1] = item.IdDocto.ToString();
                x++;

                logCRC = logCRC + " [ " + item.Documento + ", " + item.Serie + ", " + item.Fornecedor + " vencimento de " + item.VencimentoAnterior + " para " + item.Data + " ]";
            }

            if (!string.IsNullOrEmpty(logCRC))
                logCRC = "Alteração do vencimento no Contas Receber [Documento, Serie e Cliente]" + logCRC;


            x = 0;
            foreach (var item in _listaCPGVencimentoAlterado)
            {
                cpg[x, 0] = item.Data.ToString();
                cpg[x, 1] = item.IdDocto.ToString();
                x++;
                logCPG = logCPG + " [ " + item.Documento + ", " + item.Serie + ", " + item.Fornecedor + " vencimento de " + item.VencimentoAnterior + " para " + item.Data + " ]";
            }

            if (!string.IsNullOrEmpty(logCPG))
                logCPG = "Alteração do vencimento no Contas Pagar [Documento, Serie e Fornecedor]" + logCPG;

            foreach (var itemL in _listaColunasLog.OrderBy(o => o.IdColuna).OrderBy(o => o.Data))
            {
                foreach (var item in _listaColunas.Where(w => w.IdColuna == itemL.IdColuna && w.Data == itemL.Data))
                {
                    if (itemL.Previsto != item.Previsto || itemL.RealizadoBCO != item.RealizadoBCO || itemL.Realizado != item.Realizado)
                    {
                        logValores = logValores + " [ Data " + itemL.Data.ToShortDateString() + " Coluna " + itemL.Docto;
                    }

                    if (itemL.Previsto != item.Previsto)
                    {
                        logValores = logValores + " Previsto de " + string.Format("{0:n2}", itemL.Previsto) + " para " + string.Format("{0:n2}", item.Previsto);
                    }

                    if (itemL.RealizadoBCO != item.RealizadoBCO)
                    {
                        logValores = logValores + " RealizadoBCO de " + string.Format("{0:n2}", itemL.RealizadoBCO) + " para " + string.Format("{0:n2}", item.RealizadoBCO);
                    }

                    if ( itemL.Realizado != item.Realizado)
                    {
                        logValores = logValores + " Realizado de " + string.Format("{0:n2}", itemL.Realizado) + " para " + string.Format("{0:n2}", item.Realizado);
                    }
                    
                    if (itemL.Previsto != item.Previsto || itemL.RealizadoBCO != item.RealizadoBCO || itemL.Realizado != item.Realizado)
                    {
                        logValores = logValores + " ] ";
                    }
                }
            }
            
            if (!new FinanceiroBO().Gravar(_demonstrativo, _listaColunas, _listaHistorico, crc, cpg))
            {
                new Notificacoes.Mensagem("Problemas durante a Gravação.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            // gravar log
            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (!_demonstrativo.Existe ? "Incluiu " : "Alterou ") + " demonstrativo para a ";

            _log.Descricao = _log.Descricao + "Referência " + ReferenciaMaskedEditBox.Text + " para a empresa " + empresaComboBoxAdv.Text + " banco " + BancoTextBox.Text + " " + NomeBancoTextBox.Text + 
                logValores + logCRC + logCPG;

            _log.Tela = "Financeiro - Demonstrativo";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            // Recarrega os dados para não duplicar informações
            _demonstrativo = new FinanceiroBO().ConsultaDemonstrativo(_empresa.IdEmpresa, _banco.Id, ReferenciaMaskedEditBox.Text.Substring(3, 4) + ReferenciaMaskedEditBox.Text.Substring(0, 2));
            _listaColunas = new FinanceiroBO().ListarColunasDemonstrativo(_demonstrativo.Id);
            _listaColunasLog = new FinanceiroBO().ListarColunasDemonstrativo(_demonstrativo.Id);
            _listaHistorico = new FinanceiroBO().ListarHistoricoDemonstrativo(_demonstrativo.Id);

            mensagemSistemaLabel.Text = "";
            this.Refresh();
            bool temAlteracao = false;

        }

        private void editarPrevistoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[gridGroupingControl.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            try
            {
                GridTableCellStyleInfoIdentity style = this.gridGroupingControl.TableModel[_colunaCorrente.RowIndex, _colunaCorrente.ColIndex].TableCellIdentity;

                if (style.TableCellType == GridTableCellType.RecordFieldCell || style.TableCellType == GridTableCellType.AlternateRecordFieldCell)
                    nomeColuna = style.Column.Name;

                //nomeColuna = gridGroupingControl.TableDescriptor.Columns[_colunaCorrente.ColIndex - 1].MappingName;
                nomeColunaAux = nomeColuna.Replace("Previsto", "").Replace("RealizadoBCO", "").Replace("RealizadoGlobus", "");

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;

                    if (dr != null)
                    {
                        _coluna = new FinanceiroBO().Consultar((int)dr[nomeColunaAux + "Id"]);
                        ColunaLabel.Text = _coluna.Nome;
                        DataEdicaoLabel.Text = ((DateTime)dr["Data"]).ToShortDateString();
                        ValorAnteriorCurrencyTextBox.DecimalValue = (decimal)dr[nomeColunaAux + "Previsto"];
                        colunaSelecionada = nomeColunaAux + "Previsto";

                        foreach (var item in _listaD.Where(w => w.Data == (DateTime)dr["Data"]))
                        {
                            _valores = item;
                        }

                        EdicaoPanel.Visible = true;
                        ValorAtualCurrencyTextBox.Focus();
                    }
                }
            }
            catch { }

        }

        private void gridGroupingControl_TableControlMouseDown(object sender, GridTableControlMouseEventArgs e)
        {
            _colunaCorrente = gridGroupingControl.TableControl.CurrentCell;
            Control _con = e.TableControl.GetChildAtPoint(e.Inner.Location);
            
            nomeTabela = e.TableControl.Table.TableDescriptor.Name;
        }

        private void buttonAdv2_Click(object sender, EventArgs e)
        {// confirmar ediçao
            //if (string.IsNullOrEmpty(MotivoRealTextBox.Text.Trim()))
            //{
            //    new Notificacoes.Mensagem("Motivo não informado", Publicas.TipoMensagem.Alerta).ShowDialog();
            //    MotivoRealTextBox.Focus();
            //    return;
            //}

            string aux = "";
            int num = 0;

            aux = Publicas.OnlyNumbers(colunaSelecionada);
            num = Convert.ToInt32(aux);
            decimal saldoAnterior = _valores.SaldoFinalPrevisto;

            _listaD.Remove(_valores);

            if (colunaSelecionada.Contains("Previsto"))
            {                
                switch (num)
                {
                    case 1:
                        _valores.Coluna1Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna1MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 2:
                        _valores.Coluna2Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna2MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 3:
                        _valores.Coluna3Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna3MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 4:
                        _valores.Coluna4Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna4MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 5:
                        _valores.Coluna5Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna5MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 6:
                        _valores.Coluna6Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna6MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 7:
                        _valores.Coluna7Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna7MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 8:
                        _valores.Coluna8Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna8MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 9:
                        _valores.Coluna9Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna9MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 10:
                        _valores.Coluna10Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna10MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 11:
                        _valores.Coluna11Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna11MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 12:
                        _valores.Coluna12Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna12MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 13:
                        _valores.Coluna13Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna13MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 14:
                        _valores.Coluna14Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna14MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                    case 15:
                        _valores.Coluna15Previsto = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna15MotivoPrevisto = MotivoRealTextBox.Text;
                        break;
                }
            }

            saldoAnterior = _valores.SaldoAnterior;

            if (colunaSelecionada.Contains("RealizadoBCO"))
            {               

                switch (num)
                {
                    case 1:
                        _valores.Coluna1RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna1MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 2:
                        _valores.Coluna2RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna1MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 3:
                        _valores.Coluna3RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna3MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 4:
                        _valores.Coluna4RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna4MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 5:
                        _valores.Coluna5RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna5MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 6:
                        _valores.Coluna6RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna6MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 7:
                        _valores.Coluna7RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna7MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 8:
                        _valores.Coluna8RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna8MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 9:
                        _valores.Coluna9RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna9MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 10:
                        _valores.Coluna10RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna10MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 11:
                        _valores.Coluna11RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna11MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 12:
                        _valores.Coluna12RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna12MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 13:                        
                        _valores.Coluna13RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna13MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 14:
                        _valores.Coluna14RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna14MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                    case 15:
                        _valores.Coluna15RealizadoBCO = ValorAtualCurrencyTextBox.DecimalValue;
                        _valores.Coluna15MotivoRealizadoBCO = MotivoRealTextBox.Text;
                        break;
                }
            }

            temAlteracao = true;
            _listaD.Add(_valores);

            AtualizaSaldos(saldoAnterior, Convert.ToDateTime(DataEdicaoLabel.Text));

            ColunaLabel.Text = string.Empty;
            DataEdicaoLabel.Text = string.Empty;
            ValorAnteriorCurrencyTextBox.DecimalValue = 0;
            ValorAtualCurrencyTextBox.DecimalValue = 0;
            MotivoRealTextBox.Text = string.Empty;
            EdicaoPanel.Visible = false;

        }
        
        private void AtualizaSaldos(decimal saldoAnterior, DateTime data)
        {
            decimal _saldPrevisto = 0;
            decimal _saldoRealizadoBco = 0;
            decimal _saldoRealizadoGlobus = 0;
           
            foreach (var item in _listaD.OrderBy(o => o.Data))
            {
                if (item.Data.Day == 1)
                {
                    _saldPrevisto = item.SaldoAnterior;
                    _saldoRealizadoBco = item.SaldoAnterior;
                }
                else
                {
                    item.SaldoAnterior = _saldoRealizadoBco;
                }

                for (int i = 1; i <= 15; i++)
                {
                    switch (i)
                    {
                        case 1:
                            if (item.Coluna1Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna1Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna1RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna1Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna1RealizadoBCO;
                            }
                            break;
                        case 2:
                            if (item.Coluna2Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna2Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna2RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna2Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna2RealizadoBCO;
                            }
                            break;
                        case 3:
                            if (item.Coluna3Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna3Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna3RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna3Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna3RealizadoBCO;
                            }
                            break;
                        case 4:
                            if (item.Coluna4Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna4Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna4RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna4Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna4RealizadoBCO;
                            }
                            break;
                        case 5:
                            if (item.Coluna5Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna5Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna5RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna5Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna5RealizadoBCO;
                            }
                            break;
                        case 6:
                            if (item.Coluna6Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna6Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna6RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna6Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna6RealizadoBCO;
                            }
                            break;
                        case 7:
                            if (item.Coluna7Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna7Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna7RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna7Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna7RealizadoBCO;
                            }
                            break;
                        case 8:
                            if (item.Coluna8Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna8Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna8RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna8Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna8RealizadoBCO;
                            }
                            break;
                        case 9:
                            if (item.Coluna9Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna9Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna9RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna9Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna9RealizadoBCO;
                            }
                            break;
                        case 10:
                            if (item.Coluna10Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna10Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna10RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna10Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna10RealizadoBCO;
                            }
                            break;
                        case 11:
                            if (item.Coluna11Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna11Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna11RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna11Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna11RealizadoBCO;
                            }
                            break;
                        case 12:
                            if (item.Coluna12Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna12Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna12RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna12Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna12RealizadoBCO;
                            }
                            break;
                        case 13:
                            if (item.Coluna13Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna13Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna13RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna13Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna13RealizadoBCO;
                            }
                            break;
                        case 14:
                            if (item.Coluna14Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna14Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna14RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna14Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna14RealizadoBCO;
                            }
                            break;
                        case 15:
                            if (item.Coluna15Operacao == "+")
                            {
                                _saldPrevisto = _saldPrevisto + item.Coluna15Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco + item.Coluna15RealizadoBCO;
                            }
                            else
                            {
                                _saldPrevisto = _saldPrevisto - item.Coluna15Previsto;
                                _saldoRealizadoBco = _saldoRealizadoBco - item.Coluna15RealizadoBCO;
                            }
                            break;
                    }
                }

                item.SaldoFinalPrevisto = _saldPrevisto;
                item.SaldoFinalRealizadoBCO = _saldoRealizadoBco;
                _saldoRealizadoGlobus = item.SaldoFinalRealizadoGlobus;

                if (!_demonstrativo.Existe)
                    saldoAnterior = _saldPrevisto;
                else
                {
                    if (_saldoRealizadoBco != saldoAnterior)
                        saldoAnterior = _saldoRealizadoBco;
                    else
                    {
                        if (_saldoRealizadoGlobus != 0 && _saldoRealizadoGlobus != saldoAnterior)
                            saldoAnterior = _saldoRealizadoGlobus;
                        else
                            saldoAnterior = _saldPrevisto;
                    }
                }

                _valores = null;
            }

            gridGroupingControl.DataSource = new List<Valores>();
            gridGroupingControl.DataSource = _listaD.OrderBy(o => o.Data).ToList();

        }

        private void ValorAtualCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                MotivoRealTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void MotivoRealTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                buttonAdv2.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ValorAtualCurrencyTextBox.Focus();
            }
        }

        private void FecharEdicaoLabel_Click(object sender, EventArgs e)
        {
            ColunaLabel.Text = string.Empty;
            DataEdicaoLabel.Text = string.Empty;
            ValorAnteriorCurrencyTextBox.DecimalValue = 0;
            ValorAtualCurrencyTextBox.DecimalValue = 0;
            MotivoRealTextBox.Text = string.Empty;
            EdicaoPanel.Visible = false;
        }

        private void TituloMotivoPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicouNoPanel)
            {
                EdicaoPanel.Location = new Point(MousePosition.X - posIniX, MousePosition.Y - posIniY);
            }
        }

        private void FecharEdicaoLabel_MouseLeave(object sender, EventArgs e)
        {
            FecharEdicaoLabel.Text = "S";
        }

        private void FecharEdicaoLabel_MouseHover(object sender, EventArgs e)
        {
            FecharEdicaoLabel.Text = "T";
        }

        private void editarRealizadoBancárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[gridGroupingControl.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            try
            {
                GridTableCellStyleInfoIdentity style = this.gridGroupingControl.TableModel[_colunaCorrente.RowIndex, _colunaCorrente.ColIndex].TableCellIdentity;

                if (style.TableCellType == GridTableCellType.RecordFieldCell || style.TableCellType == GridTableCellType.AlternateRecordFieldCell)
                    nomeColuna = style.Column.Name;

                //nomeColuna = gridGroupingControl.TableDescriptor.Columns[_colunaCorrente.ColIndex - 1].MappingName;
                string nomeColunaAux = nomeColuna.Replace("Previsto", "").Replace("RealizadoBCO", "").Replace("RealizadoGlobus", "");

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        _coluna = new FinanceiroBO().Consultar((int)dr[nomeColunaAux + "Id"]);
                        ColunaLabel.Text = _coluna.Nome;
                        DataEdicaoLabel.Text = ((DateTime)dr["Data"]).ToShortDateString();
                        ValorAnteriorCurrencyTextBox.DecimalValue = (decimal)dr[nomeColunaAux + "RealizadoBCO"];

                        colunaSelecionada = nomeColunaAux + "RealizadoBCO";
                        foreach (var item in _listaD.Where(w => w.Data == (DateTime)dr["Data"]))
                        {
                            _valores = item;
                        }

                        EdicaoPanel.Visible = true;
                        ValorAtualCurrencyTextBox.Focus();
                    }

                }
            }
            catch { }
        }

        private void gridGroupingControl_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            try
            {
                if (!e.TableCellIdentity.Info.Contains("Historico"))
                {
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna"))
                        e.Style.TextColor = Color.Black;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna1"))
                        e.Style.BackColor = Color.MistyRose;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna2"))
                        e.Style.BackColor = Color.Bisque;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna3"))
                        e.Style.BackColor = Color.LemonChiffon;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna4"))
                        e.Style.BackColor = Color.PaleTurquoise;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna5"))
                        e.Style.BackColor = Color.LightBlue;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna6"))
                        e.Style.BackColor = Color.Lavender;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna7"))
                        e.Style.BackColor = Color.Thistle;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna8"))
                        e.Style.BackColor = Color.LightPink;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna9"))
                        e.Style.BackColor = Color.PowderBlue;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna10"))
                        e.Style.BackColor = Color.LightGray;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna11"))
                        e.Style.BackColor = Color.LightGray;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna12"))
                        e.Style.BackColor = Color.LightGray;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna13"))
                        e.Style.BackColor = Color.LightGray;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna14"))
                        e.Style.BackColor = Color.LightGray;
                    if (e.TableCellIdentity.Column.MappingName.Contains("Coluna15"))
                        e.Style.BackColor = Color.LightGray;
                }
            }
            catch { }

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                //nomeColuna = gridGroupingControl.TableDescriptor.Columns[_colunaCorrente.ColIndex - 1].MappingName;
                GridTableCellStyleInfoIdentity style = this.gridGroupingControl.TableModel[_colunaCorrente.RowIndex, _colunaCorrente.ColIndex].TableCellIdentity;

                if (style.TableCellType == GridTableCellType.RecordFieldCell || style.TableCellType == GridTableCellType.AlternateRecordFieldCell)
                    nomeColuna = style.Column.Name;
                
                editarRealizadoBancárioToolStripMenuItem.Enabled = nomeColuna.Contains("Realizado") || nomeColuna.Contains("Previsto");
                editarPrevistoToolStripMenuItem.Enabled = nomeColuna.Contains("Realizado") || nomeColuna.Contains("Previsto");

                //alterarVencimentoToolStripMenuItem.Enabled = _elementoGridClicado.ParentChildTable.Name == "ContasReceberPrevisto" ||
                  //  _elementoGridClicado.ParentChildTable.Name == "ContasPagarPrevisto";
            }
            catch
            {

            }
        }

        private void gridGroupingControl_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            _elementoGridClicado = this.gridGroupingControl.Table.GetInnerMostCurrentElement();
        }

        private void alterarVencimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*try
            {
                GridRecord rec = _elementoGridClicado as GridRecord;

                _dadosPrevistos = new CRCPrevisto();

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        if ((string)dr["Status"] == "B")
                        {
                            new Notificacoes.Mensagem("Documento já foi pago ou recebido." + Environment.NewLine + "Não é permitido alterar o vencimento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                            return;
                        }

                        if (_elementoGridClicado.ParentChildTable.Name.Contains("Receber"))
                        {
                            foreach (var item in _listaD.Where(w => w.Data == Convert.ToDateTime((string)dr["Data"])))
                            {
                                foreach (var itemR in item.ContasReceberPrevisto.Where(w => w.IdDocto == (decimal)dr["IdDocto"]))
                                {
                                    _dadosPrevistos = itemR;
                                }

                            }
                        }
                        else
                        {
                            foreach (var item in _listaD.Where(w => w.Data == Convert.ToDateTime((string)dr["Data"])))
                            {
                                foreach (var itemR in item.ContasPagarPrevisto.Where(w => w.IdDocto == (decimal)dr["IdDocto"]))
                                {
                                    _dadosPrevistos = itemR;
                                }

                            }                            
                        }
                                               
                        DocumentoLabel.Text = "Documento " + (string)dr["Documento"];
                        VencimentoLabel.Text = "Vencimento " + ((string)dr["Data"]);
                        SerieLabel.Text = "Série " + (string)dr["Serie"];
                        FornecedorLabel.Text = (string)dr["Fornecedor"];
                        
                        EditarVencimentoPanel.Visible = true;
                        NovoVencimentoMaskedEditBox.Focus();
                    }

                }
            }
            catch { }*/

        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        {// confirmar vencimento

            /*if (string.IsNullOrEmpty(MotivoVencimentoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Motivo não informado", Publicas.TipoMensagem.Alerta).ShowDialog();
                MotivoVencimentoTextBox.Focus();
                return;
            }

            DateTime _vencimento = DateTime.MinValue;
            DateTime _vencimentoAnterior = Convert.ToDateTime(VencimentoLabel.Text.Replace("Vencimento ", ""));
            decimal _valor = 0;

            try
            {
                _vencimento = Convert.ToDateTime(NovoVencimentoMaskedEditBox.Text);

                if (_vencimento <= _vencimentoAnterior)
                {
                    new Notificacoes.Mensagem("Vencimento deve ser superior a data atual do vencimento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    NovoVencimentoMaskedEditBox.Focus();
                    return;
                }
            }
            catch { }

            FeriadoEmenda _fer = new FeriadoBO().Consultar(_vencimento, _empresa.IdEmpresa);

            if (_fer.Existe && _fer.Tipo == "F")
            {
                if (new Notificacoes.Mensagem("Vencimento caiu no feriado de " + _fer.Nome + "." +
                    Environment.NewLine +
                    "Deseja manter está data?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                {
                    NovoVencimentoMaskedEditBox.Focus();
                    return;
                }
            }

            if (_vencimento.DayOfWeek == DayOfWeek.Saturday || _vencimento.DayOfWeek == DayOfWeek.Sunday)
            {
                if (new Notificacoes.Mensagem("Vencimento caiu no final de semana." + Environment.NewLine +
                    "Deseja manter está data?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                {
                    NovoVencimentoMaskedEditBox.Focus();
                    return;
                }
            }

            _dadosPrevistos.VencimentoAnterior = _vencimentoAnterior.ToShortDateString();
            _dadosPrevistos.Data = _vencimento.ToShortDateString();

            List<CRCPrevisto> _listaExcluir = new List<CRCPrevisto>();
            List<CRCPrevisto> _listaIncluir = new List<CRCPrevisto>();

            if (_elementoGridClicado.ParentChildTable.Name.Contains("Receber"))
            {
                _listaCRCVencimentoAlterado.Add(_dadosPrevistos);

                foreach (var item in _listaD.Where(w => w.Data.ToShortDateString() == _dadosPrevistos.VencimentoAnterior))
                {
                    foreach (var itemR in item.ContasReceberPrevisto.Where(w => w.IdDocto == _dadosPrevistos.IdDocto))
                    {
                        _valor = _valor + itemR.Valor;
                        _listaExcluir.Add(itemR);
                    }

                    // Exclui vencimento antigo
                    foreach (var itemR in _listaExcluir)
                    {
                        item.ContasReceberPrevisto.Remove(itemR);
                    }

                    for (int i = 1; i <= 10; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                if (_dadosPrevistos.IdColuna == item.Coluna1Id)
                                {
                                    item.Coluna1Previsto = item.Coluna1Previsto - _valor;
                                }
                                break;
                            case 2:
                                if (_dadosPrevistos.IdColuna == item.Coluna2Id)
                                {
                                    item.Coluna2Previsto = item.Coluna2Previsto - _valor;
                                }
                                break;
                            case 3:
                                if (_dadosPrevistos.IdColuna == item.Coluna3Id)
                                {
                                    item.Coluna3Previsto = item.Coluna3Previsto - _valor;
                                }
                                break;
                            case 4:
                                if (_dadosPrevistos.IdColuna == item.Coluna4Id)
                                {
                                    item.Coluna4Previsto = item.Coluna4Previsto - _valor;
                                }
                                break;
                            case 5:
                                if (_dadosPrevistos.IdColuna == item.Coluna5Id)
                                {
                                    item.Coluna5Previsto = item.Coluna5Previsto - _valor;
                                }
                                break;
                            case 6:
                                if (_dadosPrevistos.IdColuna == item.Coluna6Id)
                                {
                                    item.Coluna6Previsto = item.Coluna6Previsto - _valor;
                                }
                                break;
                            case 7:
                                if (_dadosPrevistos.IdColuna == item.Coluna7Id)
                                {
                                    item.Coluna7Previsto = item.Coluna7Previsto - _valor;
                                }
                                break;
                            case 8:
                                if (_dadosPrevistos.IdColuna == item.Coluna8Id)
                                {
                                    item.Coluna8Previsto = item.Coluna8Previsto - _valor;
                                }
                                break;
                            case 9:
                                if (_dadosPrevistos.IdColuna == item.Coluna9Id)
                                {
                                    item.Coluna9Previsto = item.Coluna9Previsto - _valor;
                                }
                                break;
                            case 10:
                                if (_dadosPrevistos.IdColuna == item.Coluna10Id)
                                {
                                    item.Coluna10Previsto = item.Coluna10Previsto - _valor;
                                }
                                break;
                            case 11:
                                if (_dadosPrevistos.IdColuna == item.Coluna11Id)
                                {
                                    item.Coluna11Previsto = item.Coluna11Previsto - _valor;
                                }
                                break;
                            case 12:
                                if (_dadosPrevistos.IdColuna == item.Coluna12Id)
                                {
                                    item.Coluna12Previsto = item.Coluna12Previsto - _valor;
                                }
                                break;
                            case 13:
                                if (_dadosPrevistos.IdColuna == item.Coluna13Id)
                                {
                                    item.Coluna13Previsto = item.Coluna13Previsto - _valor;
                                }
                                break;
                            case 14:
                                if (_dadosPrevistos.IdColuna == item.Coluna14Id)
                                {
                                    item.Coluna14Previsto = item.Coluna14Previsto - _valor;
                                }
                                break;
                            case 15:
                                if (_dadosPrevistos.IdColuna == item.Coluna15Id)
                                {
                                    item.Coluna15Previsto = item.Coluna15Previsto - _valor;
                                }
                                break;

                        }

                    }
                }

                // incluir o novo vencimento
                foreach (var item in _listaD.Where(w => w.Data == _vencimento))
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                if (_dadosPrevistos.IdColuna == item.Coluna1Id)
                                {
                                    item.Coluna1Previsto = item.Coluna1Previsto + _valor;
                                }
                                break;
                            case 2:
                                if (_dadosPrevistos.IdColuna == item.Coluna2Id)
                                {
                                    item.Coluna2Previsto = item.Coluna2Previsto + _valor;
                                }
                                break;
                            case 3:
                                if (_dadosPrevistos.IdColuna == item.Coluna3Id)
                                {
                                    item.Coluna3Previsto = item.Coluna3Previsto + _valor;
                                }
                                break;
                            case 4:
                                if (_dadosPrevistos.IdColuna == item.Coluna4Id)
                                {
                                    item.Coluna4Previsto = item.Coluna4Previsto + _valor;
                                }
                                break;
                            case 5:
                                if (_dadosPrevistos.IdColuna == item.Coluna5Id)
                                {
                                    item.Coluna5Previsto = item.Coluna5Previsto + _valor;
                                }
                                break;
                            case 6:
                                if (_dadosPrevistos.IdColuna == item.Coluna6Id)
                                {
                                    item.Coluna6Previsto = item.Coluna6Previsto + _valor;
                                }
                                break;
                            case 7:
                                if (_dadosPrevistos.IdColuna == item.Coluna7Id)
                                {
                                    item.Coluna7Previsto = item.Coluna7Previsto + _valor;
                                }
                                break;
                            case 8:
                                if (_dadosPrevistos.IdColuna == item.Coluna8Id)
                                {
                                    item.Coluna8Previsto = item.Coluna8Previsto + _valor;
                                }
                                break;
                            case 9:
                                if (_dadosPrevistos.IdColuna == item.Coluna9Id)
                                {
                                    item.Coluna9Previsto = item.Coluna9Previsto + _valor;
                                }
                                break;
                            case 10:
                                if (_dadosPrevistos.IdColuna == item.Coluna10Id)
                                {
                                    item.Coluna10Previsto = item.Coluna10Previsto + _valor;
                                }
                                break;
                            case 11:
                                if (_dadosPrevistos.IdColuna == item.Coluna11Id)
                                {
                                    item.Coluna11Previsto = item.Coluna11Previsto + _valor;
                                }
                                break;
                            case 12:
                                if (_dadosPrevistos.IdColuna == item.Coluna12Id)
                                {
                                    item.Coluna12Previsto = item.Coluna12Previsto + _valor;
                                }
                                break;
                            case 13:
                                if (_dadosPrevistos.IdColuna == item.Coluna13Id)
                                {
                                    item.Coluna13Previsto = item.Coluna13Previsto + _valor;
                                }
                                break;
                            case 14:
                                if (_dadosPrevistos.IdColuna == item.Coluna14Id)
                                {
                                    item.Coluna14Previsto = item.Coluna14Previsto + _valor;
                                }
                                break;
                            case 15:
                                if (_dadosPrevistos.IdColuna == item.Coluna15Id)
                                {
                                    item.Coluna15Previsto = item.Coluna15Previsto + _valor;
                                }
                                break;
                        }
                    }

                    foreach (var itemR in _listaExcluir)
                    {
                        itemR.Data = _vencimento.ToShortDateString();
                        if (item.ContasReceberPrevisto == null)
                            item.ContasReceberPrevisto = new List<CRCPrevisto>();
                        item.ContasReceberPrevisto.Add(itemR);
                    }
                }

            }
            else
            {
                _listaCPGVencimentoAlterado.Add(_dadosPrevistos);

                foreach (var item in _listaD.Where(w => w.Data.ToShortDateString() == _dadosPrevistos.VencimentoAnterior))
                {
                    foreach (var itemR in item.ContasPagarPrevisto.Where(w => w.IdDocto == _dadosPrevistos.IdDocto))
                    {
                        _valor = _valor + itemR.Valor;
                        _listaExcluir.Add(itemR);
                    }

                    // Exclui vencimento antigo
                    foreach (var itemR in _listaExcluir)
                    {
                        item.ContasPagarPrevisto.Remove(itemR);
                    }
                    
                    for (int i = 1; i <= 10; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                if (_dadosPrevistos.IdColuna == item.Coluna1Id)
                                {
                                    item.Coluna1Previsto = item.Coluna1Previsto - _valor;
                                }
                                break;
                            case 2:
                                if (_dadosPrevistos.IdColuna == item.Coluna2Id)
                                {
                                    item.Coluna2Previsto = item.Coluna2Previsto - _valor;
                                }
                                break;
                            case 3:
                                if (_dadosPrevistos.IdColuna == item.Coluna3Id)
                                {
                                    item.Coluna3Previsto = item.Coluna3Previsto - _valor;
                                }
                                break;
                            case 4:
                                if (_dadosPrevistos.IdColuna == item.Coluna4Id)
                                {
                                    item.Coluna4Previsto = item.Coluna4Previsto - _valor;
                                }
                                break;
                            case 5:
                                if (_dadosPrevistos.IdColuna == item.Coluna5Id)
                                {
                                    item.Coluna5Previsto = item.Coluna5Previsto - _valor;
                                }
                                break;
                            case 6:
                                if (_dadosPrevistos.IdColuna == item.Coluna6Id)
                                {
                                    item.Coluna6Previsto = item.Coluna6Previsto - _valor;
                                }
                                break;
                            case 7:
                                if (_dadosPrevistos.IdColuna == item.Coluna7Id)
                                {
                                    item.Coluna7Previsto = item.Coluna7Previsto - _valor;
                                }
                                break;
                            case 8:
                                if (_dadosPrevistos.IdColuna == item.Coluna8Id)
                                {
                                    item.Coluna8Previsto = item.Coluna8Previsto - _valor;
                                }
                                break;
                            case 9:
                                if (_dadosPrevistos.IdColuna == item.Coluna9Id)
                                {
                                    item.Coluna9Previsto = item.Coluna9Previsto - _valor;
                                }
                                break;
                            case 10:
                                if (_dadosPrevistos.IdColuna == item.Coluna10Id)
                                {
                                    item.Coluna10Previsto = item.Coluna10Previsto - _valor;
                                }
                                break;
                            case 11:
                                if (_dadosPrevistos.IdColuna == item.Coluna11Id)
                                {
                                    item.Coluna11Previsto = item.Coluna11Previsto - _valor;
                                }
                                break;
                            case 12:
                                if (_dadosPrevistos.IdColuna == item.Coluna12Id)
                                {
                                    item.Coluna12Previsto = item.Coluna12Previsto - _valor;
                                }
                                break;
                            case 13:
                                if (_dadosPrevistos.IdColuna == item.Coluna13Id)
                                {
                                    item.Coluna13Previsto = item.Coluna13Previsto - _valor;
                                }
                                break;
                            case 14:
                                if (_dadosPrevistos.IdColuna == item.Coluna14Id)
                                {
                                    item.Coluna14Previsto = item.Coluna14Previsto - _valor;
                                }
                                break;
                            case 15:
                                if (_dadosPrevistos.IdColuna == item.Coluna15Id)
                                {
                                    item.Coluna15Previsto = item.Coluna15Previsto - _valor;
                                }
                                break;
                        }

                    }
                    
                }

                // incluir o novo vencimento
                foreach (var item in _listaD.Where(w => w.Data == _vencimento))
                {

                    for (int i = 1; i <= 10; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                if (_dadosPrevistos.IdColuna == item.Coluna1Id)
                                {
                                    item.Coluna1Previsto = item.Coluna1Previsto + _valor;
                                }
                                break;
                            case 2:
                                if (_dadosPrevistos.IdColuna == item.Coluna2Id)
                                {
                                    item.Coluna2Previsto = item.Coluna2Previsto + _valor;
                                }
                                break;
                            case 3:
                                if (_dadosPrevistos.IdColuna == item.Coluna3Id)
                                {
                                    item.Coluna3Previsto = item.Coluna3Previsto + _valor;
                                }
                                break;
                            case 4:
                                if (_dadosPrevistos.IdColuna == item.Coluna4Id)
                                {
                                    item.Coluna4Previsto = item.Coluna4Previsto + _valor;
                                }
                                break;
                            case 5:
                                if (_dadosPrevistos.IdColuna == item.Coluna5Id)
                                {
                                    item.Coluna5Previsto = item.Coluna5Previsto + _valor;
                                }
                                break;
                            case 6:
                                if (_dadosPrevistos.IdColuna == item.Coluna6Id)
                                {
                                    item.Coluna6Previsto = item.Coluna6Previsto + _valor;
                                }
                                break;
                            case 7:
                                if (_dadosPrevistos.IdColuna == item.Coluna7Id)
                                {
                                    item.Coluna7Previsto = item.Coluna7Previsto + _valor;
                                }
                                break;
                            case 8:
                                if (_dadosPrevistos.IdColuna == item.Coluna8Id)
                                {
                                    item.Coluna8Previsto = item.Coluna8Previsto + _valor;
                                }
                                break;
                            case 9:
                                if (_dadosPrevistos.IdColuna == item.Coluna9Id)
                                {
                                    item.Coluna9Previsto = item.Coluna9Previsto + _valor;
                                }
                                break;
                            case 10:
                                if (_dadosPrevistos.IdColuna == item.Coluna10Id)
                                {
                                    item.Coluna10Previsto = item.Coluna10Previsto + _valor;
                                }
                                break;
                            case 11:
                                if (_dadosPrevistos.IdColuna == item.Coluna11Id)
                                {
                                    item.Coluna11Previsto = item.Coluna11Previsto + _valor;
                                }
                                break;
                            case 12:
                                if (_dadosPrevistos.IdColuna == item.Coluna12Id)
                                {
                                    item.Coluna12Previsto = item.Coluna12Previsto + _valor;
                                }
                                break;
                            case 13:
                                if (_dadosPrevistos.IdColuna == item.Coluna13Id)
                                {
                                    item.Coluna13Previsto = item.Coluna13Previsto + _valor;
                                }
                                break;
                            case 14:
                                if (_dadosPrevistos.IdColuna == item.Coluna14Id)
                                {
                                    item.Coluna14Previsto = item.Coluna14Previsto + _valor;
                                }
                                break;
                            case 15:
                                if (_dadosPrevistos.IdColuna == item.Coluna15Id)
                                {
                                    item.Coluna15Previsto = item.Coluna15Previsto + _valor;
                                }
                                break;
                        }

                    }
                    
                    foreach (var itemR in _listaExcluir)
                    {
                        itemR.Data = _vencimento.ToShortDateString();

                        if (item.ContasPagarPrevisto == null)
                            item.ContasPagarPrevisto = new List<CRCPrevisto>();

                        item.ContasPagarPrevisto.Add(itemR);
                    }
                }

            }

            // talvez o documento tenha tipos em colunas diferentes


            // atualizar saldos
            AtualizaSaldos(0, _vencimentoAnterior.AddDays((_vencimento.Day-1)*-1));

            VencimentoLabel.Text = string.Empty;
            DocumentoLabel.Text = string.Empty;
            SerieLabel.Text = string.Empty;
            FornecedorLabel.Text = string.Empty;
            NovoVencimentoMaskedEditBox.Text = string.Empty;
            MotivoVencimentoTextBox.Text = string.Empty;
            EditarVencimentoPanel.Visible = false;

            gridGroupingControl.DataSource = new List<Valores>();
            gridGroupingControl.DataSource = _listaD.OrderBy(o => o.Data).ToList();*/
        }

        private void FecharEdicaoVencimentoLabel_Click(object sender, EventArgs e)
        {
            VencimentoLabel.Text = string.Empty;
            DocumentoLabel.Text = string.Empty;
            SerieLabel.Text = string.Empty;
            FornecedorLabel.Text = string.Empty;
            NovoVencimentoMaskedEditBox.Text = string.Empty;
            MotivoVencimentoTextBox.Text = string.Empty;
            EditarVencimentoPanel.Visible = false;
        }

        private void TituloVencimentoLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicouNoPanel)
            {
                EditarVencimentoPanel.Location = new Point(MousePosition.X - posIniX, MousePosition.Y - posIniY);
            }
        }

        private void NovoVencimentoMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                MotivoVencimentoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void MotivoVencimentoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                buttonAdv1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                NovoVencimentoMaskedEditBox.Focus();
            }
        }

        private void ReprocessarPrevistoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ReferenciaMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                BancoTextBox.Focus();
            }
        }
    }
}
