using Classes;
using DynamicFilter;
using Negocio;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Chart;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.Avaliacao_de_desempenho
{
    public partial class Notas : Form
    {
        public Notas()
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

                    NumericaGridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    NumericaGridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    NumericaGridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    NumericaGridGroupingControl.BackColor = Publicas._panelTitulo;

                    listBox1.BackColor = Publicas._fundo;
                    listBox1.ForeColor = Publicas._fonte;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        System.Xml.XmlTextWriter gravaColunasGridCabecalho;
        System.Xml.XmlTextWriter gravaCoresGridCabecalho;
        System.Xml.XmlReader leColunasGridCabecalho;
        System.Xml.XmlReader leCorGridCabecalho;
        string _diretorioCor = "";
        string _diretorioCab = "";


        #region Cria Classe
        private class Resultados
        {
            public int IdEmpresa;
            public int IdColaborador;
            public string Empresa;
            public string Cargos;
            public string Colaborador;
            public string AvaliacaoReferencia1;
            public string AvaliacaoReferencia2;
            public string AvaliacaoReferencia3;
            public string AvaliacaoReferencia4;
            public string AvaliacaoReferencia5;
            public string AvaliacaoReferencia6;
            public decimal Avaliacao1;
            public decimal Avaliacao2;
            public decimal Avaliacao3;
            public decimal Avaliacao4;
            public decimal Avaliacao5;
            public decimal Avaliacao6;
            public decimal MediaAvaliacao1;
            public decimal MediaAvaliacao2;
            public string NotaReferencia1;
            public string NotaReferencia2;
            public string NotaReferencia3;
            public string NotaReferencia4;
            public string NotaReferencia5;
            public string NotaReferencia6;
            public string NotaReferencia7;
            public string NotaReferencia8;
            public string NotaReferencia9;
            public string NotaReferencia10;
            public string NotaReferencia11;
            public string NotaReferencia12;
            public decimal MediaSemestre1;
            public decimal MediaSemestre2;
            public decimal Nota1;
            public decimal Nota2;
            public decimal Nota3;
            public decimal Nota4;
            public decimal Nota5;
            public decimal Nota6;
            public decimal Nota7;
            public decimal Nota8;
            public decimal Nota9;
            public decimal Nota10;
            public decimal Nota11;
            public decimal Nota12;
            public decimal NotaFinal1;
            public decimal NotaFinal2;

            public List<Detalhe> Detalhamento;
        }

        private class Detalhe
        {
            public int IdEmpresa;
            public int IdColaborador;
            public string Empresa;
            public string Cargos;
            public string Colaborador;
            public string AvaliacaoReferencia1;
            public string AvaliacaoReferencia2;
            public string AvaliacaoReferencia3;
            public string AvaliacaoReferencia4;
            public string AvaliacaoReferencia5;
            public string AvaliacaoReferencia6;
            public decimal Avaliacao1;
            public decimal Avaliacao2;
            public decimal Avaliacao3;
            public decimal Avaliacao4;
            public decimal Avaliacao5;
            public decimal Avaliacao6;
            public decimal MediaAvaliacao1;
            public decimal MediaAvaliacao2;
            public string NotaReferencia1;
            public string NotaReferencia2;
            public string NotaReferencia3;
            public string NotaReferencia4;
            public string NotaReferencia5;
            public string NotaReferencia6;
            public string NotaReferencia7;
            public string NotaReferencia8;
            public string NotaReferencia9;
            public string NotaReferencia10;
            public string NotaReferencia11;
            public string NotaReferencia12;
            public decimal Nota1;
            public decimal Nota2;
            public decimal Nota3;
            public decimal Nota4;
            public decimal Nota5;
            public decimal Nota6;
            public decimal Nota7;
            public decimal Nota8;
            public decimal Nota9;
            public decimal Nota10;
            public decimal Nota11;
            public decimal Nota12;
            public decimal MediaSemestre1;
            public decimal MediaSemestre2;
            public decimal NotaFinal1;
            public decimal NotaFinal2;

        }
        #endregion

        List<Resultados> _listaNotas;
        List<Resultados> _listaNotasDetalhado;

        Classes.AutoAvaliacao _autoAvaliacao;
        List<Classes.AutoAvaliacao> _metasNumericas;
        List<Classes.AutoAvaliacao> _avaliacoes;

        private void Notas_Shown(object sender, EventArgs e)
        {
            List<Classes.Prazos> _prazos = new PrazosBO().Listar(true, false, true);

            this.Location = new Point(this.Left, 60);

            foreach (var item in _prazos)
            {
                AnoComboBox.Items.Add(item.Referencia);
            }

            AnoComboBox.SelectedIndex = 0;
            AnoComboBox.Focus();

            #region grid
            GridDynamicFilter filter = new GridDynamicFilter();
            GridMetroColors metroColor = new GridMetroColors();

            filter.WireGrid(this.gridGroupingControl);
            filter.WireGrid(this.NumericaGridGroupingControl);            

            metroColor.GroupDropAreaColor.BackColor = Publicas._bordaEntrada;
            
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
            
            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.RecordNavigationBar.Label = "Colaboradores";
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

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl.SetMetroStyle(metroColor);
                this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            
            NumericaGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = true;
            NumericaGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            NumericaGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            NumericaGridGroupingControl.RecordNavigationBar.Label = "Colaboradores";
            NumericaGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < NumericaGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                NumericaGridGroupingControl.TableDescriptor.Columns[i].AllowFilter = true;
                NumericaGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
                NumericaGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;
                NumericaGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                NumericaGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                NumericaGridGroupingControl.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            if (!Publicas._TemaBlack)
            {
                this.NumericaGridGroupingControl.SetMetroStyle(metroColor);
                this.NumericaGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.NumericaGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.NumericaGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.NumericaGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;


            if (Environment.MachineName.ToUpper().Contains("CORPTS") || Environment.MachineName.ToUpper().Contains("CORPRDP"))
            {
                _diretorioCab = Path.GetDirectoryName(Application.ExecutablePath) + @"\xml\GridNotasAvaliacao" + Publicas._usuario.Id + ".xml";
                _diretorioCor = Path.GetDirectoryName(Application.ExecutablePath) + @"\xml\CorGridNotasAvaliacao" + Publicas._usuario.Id + ".xml";
            }
            else
            {
                _diretorioCab = Publicas._caminhoPortal + "GridNotasAvaliacao.xml";
                _diretorioCor = Publicas._caminhoPortal + "CorGridNotasAvaliacao.xml";
            }

            //gravaColunasGridCabecalho = new System.Xml.XmlTextWriter("GridNotasAvaliacao.xml", System.Text.Encoding.UTF8);
            //gravaCoresGridCabecalho = new System.Xml.XmlTextWriter("CorGridNotasAvaliacao.xml", System.Text.Encoding.UTF8);
            gravaColunasGridCabecalho = new System.Xml.XmlTextWriter(_diretorioCab, System.Text.Encoding.UTF8);
            gravaCoresGridCabecalho = new System.Xml.XmlTextWriter(_diretorioCor, System.Text.Encoding.UTF8);

            gravaColunasGridCabecalho.Formatting = System.Xml.Formatting.Indented;
            gravaCoresGridCabecalho.Formatting = System.Xml.Formatting.Indented;
            #endregion

            gridGroupingControl.WriteXmlSchema(gravaColunasGridCabecalho);
            gridGroupingControl.WriteXmlLookAndFeel(gravaCoresGridCabecalho);
            gravaColunasGridCabecalho.Close();
            gravaCoresGridCabecalho.Close();
        }

        private void PesquisarButton_Click(object sender, EventArgs e)
        {
            gridGroupingControl.DataSource = new List<Resultados>();

            leColunasGridCabecalho = new System.Xml.XmlTextReader(_diretorioCab);
            gridGroupingControl.ApplyXmlSchema(leColunasGridCabecalho);

            leCorGridCabecalho = new System.Xml.XmlTextReader(_diretorioCor);
            gridGroupingControl.ApplyXmlLookAndFeel(leCorGridCabecalho);

            int inicio;
            int fim;
            ReestruturaGrid(gridGroupingControl);
            ReestruturaGrid(NumericaGridGroupingControl);

            if (string.IsNullOrEmpty( AnoComboBox.Text.Trim() ))
            {
                new Notificacoes.Mensagem("Selecione o Ano.", Publicas.TipoMensagem.Alerta).ShowDialog();
                AnoComboBox.Focus();
                return;
            }

            if (!IncluirQualitativasCheckBox.Checked && !IncluirNumericasCheckBox.Checked)
            {
                new Notificacoes.Mensagem("Selecione as metas que deseja visualizar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                 IncluirQualitativasCheckBox.Focus();
                return;
            }

            inicio = Convert.ToInt32("1" + AnoComboBox.Text);
            fim = Convert.ToInt32("6" + AnoComboBox.Text);

            if (AmbosRadioButton.Checked)
                fim = Convert.ToInt32("12" + AnoComboBox.Text);
            else
            {
                if (Semestre2RadioButton.Checked)
                {
                    inicio = Convert.ToInt32("7" + AnoComboBox.Text);
                    fim = Convert.ToInt32("12" + AnoComboBox.Text);
                }                
            }

            Classes.Pontuacao _pontuacao2 = new Pontuacao();
            Classes.Pontuacao _pontuacao1 = new Pontuacao();
            
            if (!AmbosRadioButton.Checked)
                _pontuacao1 = new PontuacaoBO().ConsultarMaiorReferencia(fim);
            else
            {
                _pontuacao1 = new PontuacaoBO().ConsultarMaiorReferencia(Convert.ToInt32("6" + AnoComboBox.Text));

                if (!_pontuacao1.Existe)
                    _pontuacao1 = new PontuacaoBO().ConsultarMaiorReferencia(fim);

                _pontuacao2 = new PontuacaoBO().ConsultarMaiorReferencia(fim);
            }

            
            List<Classes.AutoAvaliacao> _avaliacoes = new AutoAvaliacaoBO().Listar(inicio, fim, IncluirQualitativasCheckBox.Checked, IncluirNumericasCheckBox.Checked, Publicas._notasAcessadoPeloMenu);
            List<Classes.AutoAvaliacao> _avaliacoesDetalhe = new AutoAvaliacaoBO().ListarDetalhe(inicio, fim, IncluirQualitativasCheckBox.Checked, IncluirNumericasCheckBox.Checked);

            #region Preparação dos Dados

            int contAvaliacao =1;
            int contMetas = 1;
            int contsemestre1 = 1;
            int contsemestre2 = 1;
            decimal _mediaA1 = 0;
            decimal _mediaA2 = 0;
            decimal _mediaQ1 = 0;
            decimal _mediaQ2 = 0;

            _listaNotas = new List<Resultados>();

            foreach (var col in _avaliacoes.GroupBy(g => g.IdColaborador)
                                           .Select(s => s.Key ))
            {
                contAvaliacao = 1;
                contMetas = 1;
                contsemestre1 = 1;
                contsemestre2 = 1;
                _mediaA1 = 0;
                _mediaA2 = 0;
                _mediaQ1 = 0;
                _mediaQ2 = 0;

                Resultados _result = new Resultados();
                List<Detalhe> _listaDetalhe = new List<Detalhe>();


                foreach (var itemEG in _avaliacoesDetalhe.GroupBy(g => new { g.IdColaborador, g.IdEmpresa })
                                 .Select(s => new { IdColaborador = s.Key.IdColaborador, IdEmpresa = s.Key.IdEmpresa })
                                 .Where(w => w.IdColaborador == col))
                {
                    Detalhe _detalhe = new Detalhe();

                    contAvaliacao = 1;
                    contMetas = 1;
                    contsemestre1 = 1;
                    contsemestre2 = 1;

                    _mediaA1 = 0;
                    _mediaA2 = 0;
                    _mediaQ1 = 0;
                    _mediaQ2 = 0;

                    foreach (var itemE in _avaliacoesDetalhe.Where(w => w.IdColaborador == col && w.IdEmpresa == itemEG.IdEmpresa)
                                            .OrderBy(o => o.Ordem)
                                            .OrderBy(o => o.Tipo))
                    {

                        _detalhe.Colaborador = itemE.Colaborador;
                        _detalhe.IdColaborador = itemE.IdColaborador;
                        _detalhe.Cargos = itemE.Cargo;
                        _detalhe.Empresa = itemE.Empresa;
                        _detalhe.IdEmpresa = itemE.IdEmpresa;

                        if (AmbosRadioButton.Checked)
                        {
                            if (itemE.MesReferencia == inicio)
                                contMetas = 1;
                            if (itemE.MesReferencia == inicio + 10000)
                                contMetas = 2;
                            if (itemE.MesReferencia == inicio + 20000)
                                contMetas = 3;
                            if (itemE.MesReferencia == inicio + 30000)
                                contMetas = 4;
                            if (itemE.MesReferencia == inicio + 40000)
                                contMetas = 5;
                            if (itemE.MesReferencia == inicio + 50000)
                                contMetas = 6;
                            if (itemE.MesReferencia == inicio + 60000)
                                contMetas = 7;
                            if (itemE.MesReferencia == inicio + 70000)
                                contMetas = 8;
                            if (itemE.MesReferencia == inicio + 80000)
                                contMetas = 9;
                            if (itemE.MesReferencia == inicio + 90000)
                                contMetas = 10;
                            if (itemE.MesReferencia == inicio + 100000)
                                contMetas = 11;
                            if (itemE.MesReferencia == inicio + 110000)
                                contMetas = 12;
                        }
                        else
                        {
                            if (itemE.MesReferencia == inicio)
                                contMetas = 1;
                            if (itemE.MesReferencia == inicio + 10000)
                                contMetas = 2;
                            if (itemE.MesReferencia == inicio + 20000)
                                contMetas = 3;
                            if (itemE.MesReferencia == inicio + 30000)
                                contMetas = 4;
                            if (itemE.MesReferencia == inicio + 40000)
                                contMetas = 5;
                            if (itemE.MesReferencia == inicio + 50000)
                                contMetas = 6;
                        }

                        if (itemE.Tipo != Publicas.TipoPrazos.MetasNumericas)
                        {
                            switch (contAvaliacao)
                            {
                                case 1:
                                    _detalhe.Avaliacao1 = itemE.TotalAvaliacao;
                                    _detalhe.AvaliacaoReferencia1 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaA1 = _mediaA1 + _detalhe.Avaliacao1;
                                    break;
                                case 2:
                                    _detalhe.Avaliacao2 = itemE.TotalAvaliacao;
                                    _detalhe.AvaliacaoReferencia2 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaA1 = _mediaA1 + _detalhe.Avaliacao2;
                                    break;
                                case 3:
                                    _detalhe.Avaliacao3 = itemE.TotalAvaliacao;
                                    _detalhe.AvaliacaoReferencia3 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaA1 = _mediaA1 + _detalhe.Avaliacao3;
                                    break;
                                case 4:
                                    _detalhe.Avaliacao4 = itemE.TotalAvaliacao;
                                    _detalhe.AvaliacaoReferencia4 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaA2 = _mediaA2 + _detalhe.Avaliacao4;
                                    break;
                                case 5:
                                    _detalhe.Avaliacao5 = itemE.TotalAvaliacao;
                                    _detalhe.AvaliacaoReferencia5 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaA2 = _mediaA2 + _detalhe.Avaliacao5;
                                    break;
                                case 6:
                                    _detalhe.Avaliacao6 = itemE.TotalAvaliacao;
                                    _detalhe.AvaliacaoReferencia6 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaA2 = _mediaA2 + _detalhe.Avaliacao6;
                                    break;
                            }
                            contAvaliacao++;
                        }
                        else
                        {
                            switch (contMetas)
                            {
                                case 1:
                                    _detalhe.Nota1 = itemE.TotalAvaliacao;
                                    _detalhe.NotaReferencia1 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaQ1 = _mediaQ1 + _detalhe.Nota1;
                                    contsemestre1++;
                                    break;
                                case 2:
                                    _detalhe.Nota2 = itemE.TotalAvaliacao;
                                    _detalhe.NotaReferencia2 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaQ1 = _mediaQ1 + _detalhe.Nota2;
                                    contsemestre1++;
                                    break;
                                case 3:
                                    _detalhe.Nota3 = itemE.TotalAvaliacao;
                                    _detalhe.NotaReferencia3 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaQ1 = _mediaQ1 + _detalhe.Nota3;
                                    contsemestre1++;
                                    break;
                                case 4:
                                    _detalhe.Nota4 = itemE.TotalAvaliacao;
                                    _detalhe.NotaReferencia4 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaQ1 = _mediaQ1 + _detalhe.Nota4;
                                    contsemestre1++;
                                    break;
                                case 5:
                                    _detalhe.Nota5 = itemE.TotalAvaliacao;
                                    _detalhe.NotaReferencia5 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaQ1 = _mediaQ1 + _detalhe.Nota5;
                                    contsemestre1++;
                                    break;
                                case 6:
                                    _detalhe.Nota6 = itemE.TotalAvaliacao;
                                    _detalhe.NotaReferencia6 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaQ1 = _mediaQ1 + _detalhe.Nota6;
                                    contsemestre1++;
                                    break;
                                case 7:
                                    _detalhe.Nota7 = itemE.TotalAvaliacao;
                                    _detalhe.NotaReferencia7 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaQ2 = _mediaQ2 + _detalhe.Nota7;
                                    contsemestre2++;
                                    break;
                                case 8:
                                    _detalhe.Nota8 = itemE.TotalAvaliacao;
                                    _detalhe.NotaReferencia8 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaQ2 = _mediaQ2 + _detalhe.Nota8;
                                    contsemestre2++;
                                    break;
                                case 9:
                                    _detalhe.Nota9 = itemE.TotalAvaliacao;
                                    _detalhe.NotaReferencia9 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaQ2 = _mediaQ2 + _detalhe.Nota9;
                                    contsemestre2++;
                                    break;
                                case 10:
                                    _detalhe.Nota10 = itemE.TotalAvaliacao;
                                    _detalhe.NotaReferencia10 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaQ2 = _mediaQ2 + _detalhe.Nota10;
                                    contsemestre2++;
                                    break;
                                case 11:
                                    _detalhe.Nota11 = itemE.TotalAvaliacao;
                                    _detalhe.NotaReferencia11 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaQ2 = _mediaQ2 + _detalhe.Nota11;
                                    contsemestre2++;
                                    break;
                                case 12:
                                    _detalhe.Nota12 = itemE.TotalAvaliacao;
                                    _detalhe.NotaReferencia12 = itemE.MesReferencia.ToString("00/0000");
                                    _mediaQ2 = _mediaQ2 + _detalhe.Nota12;
                                    contsemestre2++;
                                    break;
                            }
                        }
                    }

                    _detalhe.MediaAvaliacao1 = Math.Round(_mediaA1 / 3, 2);
                    _detalhe.MediaAvaliacao2 = Math.Round(_mediaA2 / 3, 2);

                    if (_mediaQ1 > 0)
                        _detalhe.MediaSemestre1 = Math.Round(_mediaQ1 / contsemestre1 - 1, 2);
                    if (_mediaQ2 > 0)
                        _detalhe.MediaSemestre2 = Math.Round(_mediaQ2 / contsemestre2 - 1, 2);

                    _detalhe.NotaFinal1 = Math.Round(_detalhe.MediaAvaliacao1 * (_pontuacao1.PesoQualitativa / 100) +
                                         _detalhe.MediaSemestre1 * (_pontuacao1.PesoNumerica / 100), 2);

                    if (_pontuacao2 != null && _pontuacao2.Existe)
                    {
                        _detalhe.NotaFinal2 = Math.Round(_detalhe.MediaAvaliacao2 * (_pontuacao2.PesoQualitativa * 100) +
                                             _detalhe.MediaSemestre2 * (_pontuacao2.PesoNumerica * 100), 2);
                    }
                    _listaDetalhe.Add(_detalhe);
                }
                
                contAvaliacao = 1;
                contMetas = 1;
                contsemestre1 = 1;
                contsemestre2 = 1;

                _mediaA1 = 0;
                _mediaA2 = 0;
                _mediaQ1 = 0;
                _mediaQ2 = 0;

                foreach (var item in _avaliacoes.Where(w => w.IdColaborador == col)
                                                .OrderBy(o => o.Ordem)
                                                .OrderBy(o => o.Tipo))
                {

                    _result.Empresa = item.Empresa;
                    _result.Colaborador = item.Colaborador;
                    _result.Cargos = item.Cargo;
                    _result.IdEmpresa = item.IdEmpresa;
                    _result.IdColaborador = item.IdColaborador;

                    if (AmbosRadioButton.Checked)
                    {
                        if (item.MesReferencia == inicio)
                            contMetas = 1;
                        if (item.MesReferencia == inicio + 10000)
                            contMetas = 2;
                        if (item.MesReferencia == inicio + 20000)
                            contMetas = 3;
                        if (item.MesReferencia == inicio + 30000)
                            contMetas = 4;
                        if (item.MesReferencia == inicio + 40000)
                            contMetas = 5;
                        if (item.MesReferencia == inicio + 50000)
                            contMetas = 6;
                        if (item.MesReferencia == inicio + 60000)
                            contMetas = 7;
                        if (item.MesReferencia == inicio + 70000)
                            contMetas = 8;
                        if (item.MesReferencia == inicio + 80000)
                            contMetas = 9;
                        if (item.MesReferencia == inicio + 90000)
                            contMetas = 10;
                        if (item.MesReferencia == inicio + 100000)
                            contMetas = 11;
                        if (item.MesReferencia == inicio + 110000)
                            contMetas = 12;
                    }
                    else
                    {
                        if (item.MesReferencia == inicio)
                            contMetas = 1;
                        if (item.MesReferencia == inicio + 10000)
                            contMetas = 2;
                        if (item.MesReferencia == inicio + 20000)
                            contMetas = 3;
                        if (item.MesReferencia == inicio + 30000)
                            contMetas = 4;
                        if (item.MesReferencia == inicio + 40000)
                            contMetas = 5;
                        if (item.MesReferencia == inicio + 50000)
                            contMetas = 6;
                    }

                    if (item.Tipo != Publicas.TipoPrazos.MetasNumericas)
                    {
                        switch (contAvaliacao)
                        {
                            case 1:
                                _result.Avaliacao1 = item.TotalAvaliacao;
                                _result.AvaliacaoReferencia1 = item.MesReferencia.ToString("00/0000");
                                _mediaA1 = _mediaA1 + _result.Avaliacao1;
                                break;
                            case 2:
                                _result.Avaliacao2 = item.TotalAvaliacao;
                                _result.AvaliacaoReferencia2 = item.MesReferencia.ToString("00/0000");
                                _mediaA1 = _mediaA1 + _result.Avaliacao2;
                                break;
                            case 3:
                                _result.Avaliacao3 = item.TotalAvaliacao;
                                _result.AvaliacaoReferencia3 = item.MesReferencia.ToString("00/0000");
                                _mediaA1 = _mediaA1 + _result.Avaliacao3;
                                break;
                            case 4:
                                _result.Avaliacao4 = item.TotalAvaliacao;
                                _result.AvaliacaoReferencia4 = item.MesReferencia.ToString("00/0000");
                                _mediaA2 = _mediaA2 + _result.Avaliacao4;
                                break;
                            case 5:
                                _result.Avaliacao5 = item.TotalAvaliacao;
                                _result.AvaliacaoReferencia5 = item.MesReferencia.ToString("00/0000");
                                _mediaA2 = _mediaA2 + _result.Avaliacao5;
                                break;
                            case 6:
                                _result.Avaliacao6 = item.TotalAvaliacao;
                                _result.AvaliacaoReferencia6 = item.MesReferencia.ToString("00/0000");
                                _mediaA2 = _mediaA2 + _result.Avaliacao6;
                                break;
                        }
                        contAvaliacao++;
                    }
                    else
                    {
                        switch (contMetas)
                        {
                            case 1:
                                _result.Nota1 = item.TotalAvaliacao;
                                _result.NotaReferencia1 = item.MesReferencia.ToString("00/0000");
                                _mediaQ1 = _mediaQ1 + _result.Nota1;
                                contsemestre1++;
                                break;
                            case 2:
                                _result.Nota2 = item.TotalAvaliacao;
                                _result.NotaReferencia2 = item.MesReferencia.ToString("00/0000");
                                _mediaQ1 = _mediaQ1 + _result.Nota2;
                                contsemestre1++;
                                break;
                            case 3:
                                _result.Nota3 = item.TotalAvaliacao;
                                _result.NotaReferencia3 = item.MesReferencia.ToString("00/0000");
                                _mediaQ1 = _mediaQ1 + _result.Nota3;
                                contsemestre1++;
                                break;
                            case 4:
                                _result.Nota4 = item.TotalAvaliacao;
                                _result.NotaReferencia4 = item.MesReferencia.ToString("00/0000");
                                _mediaQ1 = _mediaQ1 + _result.Nota4;
                                contsemestre1++;
                                break;
                            case 5:
                                _result.Nota5 = item.TotalAvaliacao;
                                _result.NotaReferencia5 = item.MesReferencia.ToString("00/0000");
                                _mediaQ1 = _mediaQ1 + _result.Nota5;
                                contsemestre1++;
                                break;
                            case 6:
                                _result.Nota6 = item.TotalAvaliacao;
                                _result.NotaReferencia6 = item.MesReferencia.ToString("00/0000");
                                _mediaQ1 = _mediaQ1 + _result.Nota6;
                                contsemestre1++;
                                break;
                            case 7:
                                _result.Nota7 = item.TotalAvaliacao;
                                _result.NotaReferencia7 = item.MesReferencia.ToString("00/0000");
                                _mediaQ2 = _mediaQ2 + _result.Nota7;
                                contsemestre2++;
                                break;
                            case 8:
                                _result.Nota8 = item.TotalAvaliacao;
                                _result.NotaReferencia8 = item.MesReferencia.ToString("00/0000");
                                _mediaQ2 = _mediaQ2 + _result.Nota8;
                                contsemestre2++;
                                break;
                            case 9:
                                _result.Nota9 = item.TotalAvaliacao;
                                _result.NotaReferencia9 = item.MesReferencia.ToString("00/0000");
                                _mediaQ2 = _mediaQ2 + _result.Nota9;
                                contsemestre2++;
                                break;
                            case 10:
                                _result.Nota10 = item.TotalAvaliacao;
                                _result.NotaReferencia10 = item.MesReferencia.ToString("00/0000");
                                _mediaQ2 = _mediaQ2 + _result.Nota10;
                                contsemestre2++;
                                break;
                            case 11:
                                _result.Nota11 = item.TotalAvaliacao;
                                _result.NotaReferencia11 = item.MesReferencia.ToString("00/0000");
                                _mediaQ2 = _mediaQ2 + _result.Nota11;
                                contsemestre2++;
                                break;
                            case 12:
                                _result.Nota12 = item.TotalAvaliacao;
                                _result.NotaReferencia12 = item.MesReferencia.ToString("00/0000");
                                _mediaQ2 = _mediaQ2 + _result.Nota12;
                                contsemestre2++;
                                break;
                        }
                        contMetas++;
                    }

                    if (_result.Detalhamento == null)
                        _result.Detalhamento = new List<Detalhe>();

                }

                _result.Detalhamento = _listaDetalhe;

                _result.MediaAvaliacao1 = Math.Round(_mediaA1 / 3, 2);
                _result.MediaAvaliacao2 = Math.Round(_mediaA2 / 3, 2);

                if (_mediaQ1 > 0)
                    _result.MediaSemestre1 = Math.Round(_mediaQ1 / contsemestre1 - 1, 2);
                if (_mediaQ2 > 0)
                    _result.MediaSemestre2 = Math.Round(_mediaQ2 / contsemestre2 - 1, 2);

                _result.NotaFinal1 = Math.Round(_result.MediaAvaliacao1 * (_pontuacao1.PesoQualitativa / 100) +
                                     _result.MediaSemestre1 * (_pontuacao1.PesoNumerica / 100),2);

                if (_pontuacao2 != null && _pontuacao2.Existe)
                {
                    _result.NotaFinal2 = Math.Round(_result.MediaAvaliacao2 * (_pontuacao2.PesoQualitativa * 100) +
                                         _result.MediaSemestre2 * (_pontuacao2.PesoNumerica * 100),2);
                }
                _listaNotas.Add(_result);
            }
            #endregion

            #region Preparo do Grid principal
           
            GridColumnDescriptor _col = new GridColumnDescriptor();
            GridStackedHeaderRowDescriptor stackedHeaderRowDescriptor;
            List<GridStackedHeaderDescriptor> _listaHeader = new List<GridStackedHeaderDescriptor>();
            GridStackedHeaderDescriptor gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();

            if (VisualizacaoNovaCheckBox.Checked)
            {
                _col = new GridColumnDescriptor("Cargos", "Cargos", "Cargo");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                gridGroupingControl.TableDescriptor.Columns.Add(_col);

                _col = new GridColumnDescriptor("Colaborador", "Colaborador", "Colaborador");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(AnoComboBox.Font.FontFamily, (float)8, FontStyle.Regular));

                gridGroupingControl.TableDescriptor.FrozenColumn = "Colaborador";
                gridGroupingControl.TableDescriptor.Columns.Add(_col);


                gridStackedHeaderDescriptor1.HeaderText = " ";
                gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Colaborador") });

            }
            else
            {
                _col = new GridColumnDescriptor("Empresa", "Empresa", "Empresa");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                gridGroupingControl.TableDescriptor.Columns.Add(_col);

                _col = new GridColumnDescriptor("Cargos", "Cargos", "Cargo");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                gridGroupingControl.TableDescriptor.Columns.Add(_col);

                _col = new GridColumnDescriptor("Colaborador", "Colaborador", "Colaborador");
                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                _col.Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(AnoComboBox.Font.FontFamily, (float)8, FontStyle.Regular));
                gridGroupingControl.TableDescriptor.FrozenColumn = "Colaborador";
                gridGroupingControl.TableDescriptor.Columns.Add(_col);

                gridStackedHeaderDescriptor1.HeaderText = " ";
                gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Empresa")
                                                                                                                    //, new GridStackedHeaderVisibleColumnDescriptor("Cargos")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Colaborador") });

            }

                _listaHeader.Add(gridStackedHeaderDescriptor1);

                if (IncluirQualitativasCheckBox.Checked)
                {
                    _col = new GridColumnDescriptor("Avaliacao1", "Avaliacao1", "Auto Avaliação");
                    _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    gridGroupingControl.TableDescriptor.Columns.Add(_col);

                    _col = new GridColumnDescriptor("Avaliacao2", "Avaliacao2", "Avaliação Gestor");
                    _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    gridGroupingControl.TableDescriptor.Columns.Add(_col);

                    _col = new GridColumnDescriptor("Avaliacao3", "Avaliacao3", "Avaliação RH");
                    _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    gridGroupingControl.TableDescriptor.Columns.Add(_col);

                    _col = new GridColumnDescriptor("MediaAvaliacao1", "MediaAvaliacao1", "Média");
                    _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                    _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    gridGroupingControl.TableDescriptor.Columns.Add(_col);

                    if (AmbosRadioButton.Checked)
                    {
                        _col = new GridColumnDescriptor("Avaliacao4", "Avaliacao4", "Auto Avaliação");
                        _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns.Add(_col);

                        _col = new GridColumnDescriptor("Avaliacao5", "Avaliacao5", "Avaliação Gestor");
                        _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns.Add(_col);

                        _col = new GridColumnDescriptor("Avaliacao6", "Avaliacao6", "Avaliação RH");
                        _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns.Add(_col);

                        _col = new GridColumnDescriptor("MediaAvaliacao2", "MediaAvaliacao2", "Média");
                        _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns.Add(_col);
                    }


                    gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();
                    gridStackedHeaderDescriptor1.HeaderText = (Semestre1RadioButton.Checked || AmbosRadioButton.Checked ? "06/" : "12/") + AnoComboBox.Text;

                    gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Avaliacao1")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Avaliacao2")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Avaliacao3")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("MediaAvaliacao1") });

                    _listaHeader.Add(gridStackedHeaderDescriptor1);

                    gridGroupingControl.TableDescriptor.VisibleColumns.Add("Avaliacao1");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Add("Avaliacao2");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Add("Avaliacao3");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Add("MediaAvaliacao1");

                    if (AmbosRadioButton.Checked)
                    {
                        gridGroupingControl.TableDescriptor.VisibleColumns.Add("Avaliacao4");
                        gridGroupingControl.TableDescriptor.VisibleColumns.Add("Avaliacao5");
                        gridGroupingControl.TableDescriptor.VisibleColumns.Add("Avaliacao6");
                        gridGroupingControl.TableDescriptor.VisibleColumns.Add("MediaAvaliacao2");

                        gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();
                        gridStackedHeaderDescriptor1.HeaderText = "12/" + AnoComboBox.Text;

                        gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Avaliacao4")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Avaliacao5")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Avaliacao6")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("MediaAvaliacao2") });

                        _listaHeader.Add(gridStackedHeaderDescriptor1);
                    }

                }
                else
                {
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Avaliacao1");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Avaliacao2");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Avaliacao3");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Avaliacao4");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Avaliacao5");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Avaliacao6");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("MediaAvaliacao1");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("MediaAvaliacao2");
                }

                if (IncluirNumericasCheckBox.Checked)
                {
                    string _coluna = "";
                    foreach (var item in _listaNotas)
                    {
                        for (int i = 1; i < (AmbosRadioButton.Checked ? 13 : 7); i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    _coluna = item.NotaReferencia1;
                                    break;
                                case 2:
                                    _coluna = item.NotaReferencia2;
                                    break;
                                case 3:
                                    _coluna = item.NotaReferencia3;
                                    break;
                                case 4:
                                    _coluna = item.NotaReferencia4;
                                    break;
                                case 5:
                                    _coluna = item.NotaReferencia5;
                                    break;
                                case 6:
                                    _coluna = item.NotaReferencia6;
                                    break;
                                case 7:
                                    _coluna = item.NotaReferencia7;
                                    break;
                                case 8:
                                    _coluna = item.NotaReferencia8;
                                    break;
                                case 9:
                                    _coluna = item.NotaReferencia9;
                                    break;
                                case 10:
                                    _coluna = item.NotaReferencia10;
                                    break;
                                case 11:
                                    _coluna = item.NotaReferencia11;
                                    break;
                                case 12:
                                    _coluna = item.NotaReferencia12;
                                    break;
                            }

                            _col = new GridColumnDescriptor("Nota" + i.ToString(), "Nota" + i.ToString(), (_coluna == null ? " " : _coluna));
                            _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                            _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                            gridGroupingControl.TableDescriptor.Columns.Add(_col);

                            if (i == 6)
                            {
                                _col = new GridColumnDescriptor("MediaSemestre1", "MediaSemestre1", "Média");
                                _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                                _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                                _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                                _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                                gridGroupingControl.TableDescriptor.Columns.Add(_col);
                            }
                        }
                        break;
                    }

                    if (AmbosRadioButton.Checked)
                    {
                        _col = new GridColumnDescriptor("MediaSemestre2", "MediaSemestre2", "Média");
                        _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns.Add(_col);
                    }


                    gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();
                    gridStackedHeaderDescriptor1.HeaderText = (Semestre1RadioButton.Checked || AmbosRadioButton.Checked ? "1º Semestre" : "2º Semestre");

                    gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Nota1")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota2")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota3")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota4")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota5")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota6")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("MediaSemestre1")});
                    _listaHeader.Add(gridStackedHeaderDescriptor1);

                    if (AmbosRadioButton.Checked)
                    {
                        gridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota7");
                        gridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota8");
                        gridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota9");
                        gridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota10");
                        gridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota11");
                        gridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota12");
                        gridGroupingControl.TableDescriptor.VisibleColumns.Add("MediaSemestre2");
                        gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();
                        gridStackedHeaderDescriptor1.HeaderText = "2º Semestre";

                        gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Nota7")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota8")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota9")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota10")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota11")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota12")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("MediaSemestre2")});
                    }

                    gridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota1");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota2");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota3");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota4");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota5");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota6");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Add("MediaSemestre1");

                }
                else
                {
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota1");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota2");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota3");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota4");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota5");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota6");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota7");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota8");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota9");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota10");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota11");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota12");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("MediaSemestre1");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("MediaSemestre2");
                }

                if (IncluirNumericasCheckBox.Checked && IncluirQualitativasCheckBox.Checked)
                {
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("NotaFinal1");
                    gridGroupingControl.TableDescriptor.VisibleColumns.Remove("NotaFinal2");

                    if (Semestre1RadioButton.Checked || AmbosRadioButton.Checked)
                    {
                        _col = new GridColumnDescriptor("NotaFinal1", "NotaFinal1", "Nota 1º Semeste");
                        _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns.Add(_col);
                        gridGroupingControl.TableDescriptor.VisibleColumns.Add("NotaFinal1");
                    }
                    if (Semestre2RadioButton.Checked || AmbosRadioButton.Checked)
                    {
                        if (Semestre2RadioButton.Checked)
                            _col = new GridColumnDescriptor("NotaFinal1", "NotaFinal1", "Nota 2º Semeste");
                        else
                            _col = new GridColumnDescriptor("NotaFinal2", "NotaFinal2", "Nota 2º Semeste");
                        _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        gridGroupingControl.TableDescriptor.Columns.Add(_col);

                        if (Semestre2RadioButton.Checked)
                            gridGroupingControl.TableDescriptor.VisibleColumns.Add("NotaFinal1");
                        else
                            gridGroupingControl.TableDescriptor.VisibleColumns.Add("NotaFinal2");
                    }

                    gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();

                    gridStackedHeaderDescriptor1.HeaderText = "Resultado Final";
                    if (AmbosRadioButton.Checked)
                        gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("NotaFinal1")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("NotaFinal2") });
                    else
                        gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("NotaFinal1") });

                    _listaHeader.Add(gridStackedHeaderDescriptor1);

                }

                stackedHeaderRowDescriptor = new GridStackedHeaderRowDescriptor("Row1", _listaHeader.ToArray());

                this.gridGroupingControl.TableDescriptor.StackedHeaderRows.Add(stackedHeaderRowDescriptor);

            if (!VisualizacaoNovaCheckBox.Checked)
            {
                this.gridGroupingControl.TableDescriptor.GroupedColumns.Add("Empresa", ListSortDirection.Ascending);
                //this.gridGroupingControl.TableDescriptor.GroupedColumns.Add("Cargos", ListSortDirection.Ascending);
            }
            else
            {
                this.gridGroupingControl.TableDescriptor.GroupedColumns.Add("Cargos", ListSortDirection.Ascending);
            }
            this.gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Empresa");
            this.gridGroupingControl.TableDescriptor.VisibleColumns.Remove("Cargos");

            #endregion
            
            gridGroupingControl.DataSource = _listaNotas;

            #region Ajusta descrição colunas
            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                if (!gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("MediaAvaliacao") && !gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("Nota") && !gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("Media"))
                {
                    if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("4") ||
                        gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("1"))
                        gridGroupingControl.TableDescriptor.Columns[i].HeaderText = "Auto Avaliação";
                    if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("5") ||
                        gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("2"))
                        gridGroupingControl.TableDescriptor.Columns[i].HeaderText = "Avaliação Gestor";
                    if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("6") ||
                        gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("3"))
                        gridGroupingControl.TableDescriptor.Columns[i].HeaderText = "Avaliação RH";
                }

                if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("Nota"))
                {
                        if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("12"))
                        gridGroupingControl.TableDescriptor.Columns[i].HeaderText = "12/" + AnoComboBox.Text;
                    else
                    {
                        if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("11"))
                            gridGroupingControl.TableDescriptor.Columns[i].HeaderText = "11/" + AnoComboBox.Text;
                        else
                        {
                            if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("10"))
                                gridGroupingControl.TableDescriptor.Columns[i].HeaderText = "10/" + AnoComboBox.Text;
                            else
                            {
                                if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("9"))
                                    gridGroupingControl.TableDescriptor.Columns[i].HeaderText = "09/" + AnoComboBox.Text;
                                if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("8"))
                                    gridGroupingControl.TableDescriptor.Columns[i].HeaderText = "08/" + AnoComboBox.Text;
                                if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("7"))
                                    gridGroupingControl.TableDescriptor.Columns[i].HeaderText = "07/" + AnoComboBox.Text;
                                if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("6"))
                                    gridGroupingControl.TableDescriptor.Columns[i].HeaderText = (Semestre2RadioButton.Checked ? "12/" : "06/") + AnoComboBox.Text;
                                if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("5"))
                                    gridGroupingControl.TableDescriptor.Columns[i].HeaderText = (Semestre2RadioButton.Checked ? "11/" : "05/") + AnoComboBox.Text;
                                if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("4"))
                                    gridGroupingControl.TableDescriptor.Columns[i].HeaderText = (Semestre2RadioButton.Checked ? "10/" : "04/") + AnoComboBox.Text;
                                if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("3"))
                                    gridGroupingControl.TableDescriptor.Columns[i].HeaderText = (Semestre2RadioButton.Checked ? "09/" : "03/") + AnoComboBox.Text;
                                if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("2"))
                                    gridGroupingControl.TableDescriptor.Columns[i].HeaderText = (Semestre2RadioButton.Checked ? "08/" : "02/") + AnoComboBox.Text;
                                if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("1"))
                                    gridGroupingControl.TableDescriptor.Columns[i].HeaderText = (Semestre2RadioButton.Checked ? "07/" : "01/") + AnoComboBox.Text;

                                if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("NotaFinal1"))
                                    gridGroupingControl.TableDescriptor.Columns[i].HeaderText = "Nota Final 1º Semestre";
                                if (gridGroupingControl.TableDescriptor.Columns[i].MappingName.Contains("NotaFinal2"))
                                    gridGroupingControl.TableDescriptor.Columns[i].HeaderText = "Nota Final 2º Semestre";
                            }
                        }
                    }
                }
            }
            #endregion

            #region Preparo do grid interno
            if (!VisualizacaoNovaCheckBox.Checked)
                gridGroupingControl.TableDescriptor.Relations.Clear();
            else
            {
                for (int i = 0; i < gridGroupingControl.TableDescriptor.Relations.Count; i++)
                {

                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.TableOptions.ShowRowHeader = false;
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.AllowEdit = false;
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.AllowNew = false;
                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.AllowRemove = false;

                    //GridSummaryRowDescriptor _soma = new GridSummaryRowDescriptor();

                    //gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.SummaryRows.Add(_soma);
                    //gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.SummaryRows[0].Appearance.AnyCell.BackColor = gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[0].Appearance.AnyRecordFieldCell.BackColor;
                    
                    for (int j = 0; j < gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns.Count; j++)
                    {
                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[i].AllowFilter = false;
                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(AnoComboBox.Font.FontFamily, (float)8, FontStyle.Regular));

                        if ((gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName != "Empresa" &&
                            (!gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("MediaAvaliacao")) &&
                            (!gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.StartsWith("Avaliacao")) &&
                            (!gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.StartsWith("Nota")) &&
                            (!gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.StartsWith("Media"))) ||
                             gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("Final") ||
                            (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("Referencia"))) 
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                        else
                        {
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                            if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName == "Empresa")
                            {
                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Width = 150;
                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.Font = new GridFontInfo(new Font(AnoComboBox.Font.FontFamily, (float)8, FontStyle.Bold));
                            }

                            if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("Avaliacao"))
                            {
                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;

                                if (!IncluirQualitativasCheckBox.Checked)
                                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                                else
                                {
                                    if (!AmbosRadioButton.Checked && 
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("4") ||
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("5") ||
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("6") ||
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("MediaAvaliacao2"))
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                                    else
                                    {
                                        if (!gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("MediaAvaliacao"))
                                        {
                                            if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("4") ||
                                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("1"))
                                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Auto Avaliação";
                                            if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("5") ||
                                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("2"))
                                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Avaliação Gestor";
                                            if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("6") ||
                                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("3"))
                                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Avaliação RH";
                                        }
                                    }
                                }
                            }

                            if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("Nota"))
                            {
                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;

                                if ((!IncluirNumericasCheckBox.Checked || !IncluirQualitativasCheckBox.Checked) &&
                                     gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("Final"))
                                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                                else
                                {
                                    if (!AmbosRadioButton.Checked && gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("Final2"))
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                                }

                                if (!IncluirNumericasCheckBox.Checked)
                                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                                else
                                {
                                    
                                    if (!AmbosRadioButton.Checked &&
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("7") ||
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("8") ||
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("9") ||
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("10") ||
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("11") ||
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("12") )
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                                    else
                                    {
                                        if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("12"))
                                            gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "12/" + AnoComboBox.Text;
                                        else
                                        {
                                            if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("11"))
                                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "11/" + AnoComboBox.Text;
                                            else
                                            {
                                                if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("10"))
                                                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "10/" + AnoComboBox.Text;
                                                else
                                                {
                                                    if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("9"))
                                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "09/" + AnoComboBox.Text;
                                                    if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("8"))
                                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "08/" + AnoComboBox.Text;
                                                    if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("7"))
                                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "07/" + AnoComboBox.Text;
                                                    if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("6"))
                                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = (Semestre2RadioButton.Checked ? "12/" : "06/") + AnoComboBox.Text;
                                                    if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("5"))
                                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = (Semestre2RadioButton.Checked ? "11/" : "05/") + AnoComboBox.Text;
                                                    if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("4"))
                                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = (Semestre2RadioButton.Checked ? "10/" : "04/") + AnoComboBox.Text;
                                                    if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("3"))
                                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = (Semestre2RadioButton.Checked ? "09/" : "03/") + AnoComboBox.Text;
                                                    if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("2"))
                                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = (Semestre2RadioButton.Checked ? "08/" : "02/") + AnoComboBox.Text;
                                                    if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("1"))
                                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = (Semestre2RadioButton.Checked ? "07/" : "01/") + AnoComboBox.Text;

                                                    if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("NotaFinal1"))
                                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Nota Final 1º Semestre";
                                                    if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("NotaFinal2"))
                                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Nota Final 2º Semestre";
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("Media"))
                            {
                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;

                                gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Média";

                                if (!IncluirNumericasCheckBox.Checked)
                                    gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                                else
                                {
                                    if (!AmbosRadioButton.Checked &&
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.Contains("2") )
                                        gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(gridGroupingControl.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                                    else
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AnoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int inicio = Convert.ToInt32("1" + AnoComboBox.Text);
            int fim = Convert.ToInt32("12" + AnoComboBox.Text);

            IncluirNumericasCheckBox.Enabled = false;
            IncluirQualitativasCheckBox.Enabled = false;

            List<AutoAvaliacao> _ref = new AutoAvaliacaoBO().MaiorReferencia(inicio, fim);

            foreach (var item in _ref)
            {
                IncluirNumericasCheckBox.Enabled = item.Tipo == Publicas.TipoPrazos.MetasNumericas || IncluirNumericasCheckBox.Enabled;
                IncluirQualitativasCheckBox.Enabled = (item.Tipo == Publicas.TipoPrazos.AvaliacaoDoGestor ||
                    item.Tipo == Publicas.TipoPrazos.AutoAvaliacao ||
                    item.Tipo == Publicas.TipoPrazos.AvaliacaoRH) ||
                    IncluirQualitativasCheckBox.Enabled;

                if (!IncluirNumericasCheckBox.Enabled)
                    IncluirNumericasCheckBox.Checked = false;

                if (!IncluirQualitativasCheckBox.Enabled)
                    IncluirQualitativasCheckBox.Checked = false;

                if (item.MesReferencia != fim)
                {
                    Semestre1RadioButton.Enabled = item.MesReferencia >= inicio;// && item.MesReferencia <= fim - 60000; // para chegar no mes 6.
                    Semestre2RadioButton.Enabled = item.MesReferencia >= fim - 50000 && item.MesReferencia <= fim; // para chegar no mes 6.
                    AmbosRadioButton.Enabled = false;
                    Semestre1RadioButton.Checked = Semestre1RadioButton.Enabled;
                    Semestre2RadioButton.Checked = Semestre2RadioButton.Enabled;
                }
                else
                {
                    Semestre1RadioButton.Enabled = true;
                    Semestre2RadioButton.Enabled = true;
                    AmbosRadioButton.Enabled = true;
                }
            }

            if (AnoComboBox.Text == "2017")
            {
                Semestre1RadioButton.Checked = AnoComboBox.Text != "2017";
                Semestre1RadioButton.Enabled = AnoComboBox.Text != "2017";

                Semestre2RadioButton.Checked = AnoComboBox.Text == "2017";
                Semestre2RadioButton.Enabled = AnoComboBox.Text != "2017";

                AmbosRadioButton.Checked = AnoComboBox.Text != "2017";
                AmbosRadioButton.Enabled = AnoComboBox.Text != "2017";
            }
        }

        private void ReestruturaGrid( GridGroupingControl _grid)
        {
            if (_listaNotas != null && _grid.Name == "gridGroupingControl")
                _listaNotas.Clear();
            
            //gridGroupingControl.TableDescriptor.Columns.Clear();
            try
            {
                _grid.ShowGroupDropArea = false;

                for (int i = _grid.TableDescriptor.Columns.Count-1; i >= 0; i--)
                {
                    _grid.TableDescriptor.Columns.RemoveAt(i);
                }

                for (int i = _grid.TableDescriptor.StackedHeaderRows.Count-1; i >= 0; i--)
                {
                    _grid.TableDescriptor.StackedHeaderRows.RemoveAt(i);
                }
            }
            catch { }

            _grid.TableDescriptor.StackedHeaderRows.Clear();
            _grid.TableDescriptor.GroupedColumns.Clear();

            _grid.Enabled = true;
            try
            {
                _grid.DataSource = new List<Resultados>();
            }
            catch { }

        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            ReestruturaGrid(gridGroupingControl);
            ReestruturaGrid(NumericaGridGroupingControl);
            Auto1SemestreCurrencyTextBox.DecimalValue = 0;
            Auto2SemestreCurrencyTextBox.DecimalValue = 0;
            RH1SemestreCurrencyTextBox.DecimalValue = 0;
            RH2SemestreCurrencyTextBox.DecimalValue = 0;
            Gestor1SemestreCurrencyTextBox.DecimalValue = 0;
            Gestor2SemestreCurrencyTextBox.DecimalValue = 0;
            FeedbackColaborador1TextBox.Text = string.Empty;
            FeedbackColaborador2TextBox.Text = string.Empty;
            FeedbackGestor1TextBox.Text = string.Empty;
            FeedbackGestor2TextBox.Text = string.Empty;

            tabControlAdv1.SelectedTab = tabPageAdv1;
            AnoComboBox.Focus();
        }

        private void AnoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (Semestre1RadioButton.Enabled)
                    Semestre1RadioButton.Focus();
                else
                {
                    if (Semestre2RadioButton.Enabled)
                        Semestre2RadioButton.Focus();
                    else
                    {
                        if (AmbosRadioButton.Enabled)
                            AmbosRadioButton.Focus();
                        else
                            IncluirQualitativasCheckBox.Focus();
                    }
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AnoComboBox.Focus();
            }
        }

        private void limparButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PesquisarButton.Focus();
            }
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

        private void Semestre1RadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                IncluirQualitativasCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AnoComboBox.Focus();
            }
        }

        private void IncluirQualitativasCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                IncluirNumericasCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (Semestre1RadioButton.Enabled)
                    Semestre1RadioButton.Focus();
                else
                {
                    if (Semestre2RadioButton.Enabled)
                        Semestre2RadioButton.Focus();
                    else
                    {
                        if (AmbosRadioButton.Enabled)
                            AmbosRadioButton.Focus();
                        else
                            IncluirQualitativasCheckBox.Focus();
                    }
                }
            }
        }

        private void IncluirNumericasCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PesquisarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IncluirQualitativasCheckBox.Focus();
            }
        }

        private void PesquisarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IncluirNumericasCheckBox.Focus();
            }
        }

        private void PesquisarButton_Enter(object sender, EventArgs e)
        {
            PesquisarButton.BackColor = Publicas._botaoFocado;
            PesquisarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void PesquisarButton_Validating(object sender, CancelEventArgs e)
        {
            PesquisarButton.BackColor = AnoComboBox.BackColor;
            PesquisarButton.ForeColor = Publicas._fonteBotao;
        }

        private void analisarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetalhamentoTabPage.TabVisible = true;
            GridRecordRow _registro;

            ReestruturaGrid(NumericaGridGroupingControl);
            List<AutoAvaliacao> _listaReferencias1 = new List<AutoAvaliacao>();
            List<AutoAvaliacao> _listaReferencias2 = new List<AutoAvaliacao>();

            Semestre1ChartControl.Visible = IncluirQualitativasCheckBox.Checked;
            Semestre2ChartControl.Visible = IncluirQualitativasCheckBox.Checked;

            try
            {
                int index = gridGroupingControl.Table.CurrentRecord.GetRecord().GetRowIndex();
                _registro = gridGroupingControl.Table.DisplayElements[index] as GridRecordRow;

                if (_registro != null)
                {
                    Record dr = _registro.GetRecord() as Record;

                    if (dr != null)
                    {
                        NomeColaborador.Text = (string)dr["Colaborador"];
                        CargoColaboradorLabel.Text = (string)dr["Cargo"];

                        _avaliacoes = new AutoAvaliacaoBO().Listar((int)dr["IdColaborador"], "Todos", Convert.ToInt32(AnoComboBox.Text));

                        if (_listaReferencias1 == null)
                            _listaReferencias1 = new List<AutoAvaliacao>();
                        else
                            _listaReferencias1.Clear();

                        if (_listaReferencias2 == null)
                            _listaReferencias2 = new List<AutoAvaliacao>();
                        else
                            _listaReferencias2.Clear();

                        foreach (var item in _avaliacoes.OrderBy(o => o.Ano).OrderBy(o => o.MesReferencia))
                        {
                            AutoAvaliacao _ano = new AutoAvaliacao();
                            _ano.Ano = item.Ano;
                            _ano.MesReferencia = item.MesReferencia;
                            _ano.ReferenciaFormatada = item.ReferenciaFormatada;

                            if (Semestre1RadioButton.Checked || AmbosRadioButton.Checked)
                            {
                                if (_listaReferencias1.Where(w => w.MesReferencia.ToString("000000") == "06" + AnoComboBox.Text).Count() == 0 && _ano.MesReferencia.ToString("000000") == "06" + AnoComboBox.Text)
                                    _listaReferencias1.Add(_ano);
                            }

                            if (Semestre1RadioButton.Checked || AmbosRadioButton.Checked)
                            {
                                if (_listaReferencias2.Where(w => w.MesReferencia.ToString("000000") == "12" + AnoComboBox.Text).Count() == 0 && _ano.MesReferencia.ToString("000000") == "12" + AnoComboBox.Text)
                                    _listaReferencias2.Add(_ano);
                            }
                        }

                        if (AmbosRadioButton.Checked)
                            _metasNumericas = new AutoAvaliacaoBO().Listar("MN", (int)dr["IdColaborador"], "01" + AnoComboBox.Text, "12" + AnoComboBox.Text);

                        if (Semestre1RadioButton.Checked || AmbosRadioButton.Checked)
                        {
                            Auto1SemestreCurrencyTextBox.DecimalValue = (decimal)dr["Avaliacao1"];
                            Gestor1SemestreCurrencyTextBox.DecimalValue = (decimal)dr["Avaliacao2"];
                            RH1SemestreCurrencyTextBox.DecimalValue = (decimal)dr["Avaliacao3"];

                            _autoAvaliacao = new AutoAvaliacaoBO().Consultar((int)dr["IdColaborador"], "06"+AnoComboBox.Text, "AA", (int)dr["IdEmpresa"]);

                            if (Semestre1RadioButton.Checked)
                                _metasNumericas = new AutoAvaliacaoBO().Listar("MN", (int)dr["IdColaborador"], "01" + AnoComboBox.Text, "06" + AnoComboBox.Text);

                            FeedbackGestor1TextBox.Text = _autoAvaliacao.FeedbackGestor;
                            FeedbackColaborador1TextBox.Text = _autoAvaliacao.Comentario;

                            if (IncluirQualitativasCheckBox.Checked)
                            {
                                MontaGrafico(Convert.ToInt32("06" + AnoComboBox.Text), Semestre1ChartControl);
                                Semestre1ChartControl.Visible = true;
                            }
                        }

                        if (Semestre2RadioButton.Checked || AmbosRadioButton.Checked)
                        {
                            _autoAvaliacao = new AutoAvaliacaoBO().Consultar((int)dr["IdColaborador"], "12" + AnoComboBox.Text, "AA", (int)dr["IdEmpresa"]);
                            FeedbackGestor2TextBox.Text = _autoAvaliacao.FeedbackGestor;
                            FeedbackColaborador2TextBox.Text = _autoAvaliacao.Comentario;
                            
                            if (Semestre2RadioButton.Checked)
                            {
                                _metasNumericas = new AutoAvaliacaoBO().Listar("MN", (int)dr["IdColaborador"], "07" + AnoComboBox.Text, "12" + AnoComboBox.Text);
                                Auto2SemestreCurrencyTextBox.DecimalValue = (decimal)dr["Avaliacao1"];
                                Gestor2SemestreCurrencyTextBox.DecimalValue = (decimal)dr["Avaliacao2"];
                                RH2SemestreCurrencyTextBox.DecimalValue = (decimal)dr["Avaliacao3"];
                            }
                            else
                            {
                                Auto2SemestreCurrencyTextBox.DecimalValue = (decimal)dr["Avaliacao4"];
                                Gestor2SemestreCurrencyTextBox.DecimalValue = (decimal)dr["Avaliacao6"];
                                RH2SemestreCurrencyTextBox.DecimalValue = (decimal)dr["Avaliacao5"];
                            }

                            if (IncluirQualitativasCheckBox.Checked)
                            {
                                MontaGrafico(Convert.ToInt32("12" + AnoComboBox.Text), Semestre2ChartControl);
                                Semestre2ChartControl.Visible = true;
                            }
                        }
                    }
                }

                if (_metasNumericas != null)
                {
                    int inicio;
                    int fim;

                    inicio = Convert.ToInt32("1" + AnoComboBox.Text);
                    fim = Convert.ToInt32("6" + AnoComboBox.Text);

                    if (AmbosRadioButton.Checked)
                        fim = Convert.ToInt32("12" + AnoComboBox.Text);
                    else
                    {
                        if (Semestre2RadioButton.Checked)
                        {
                            inicio = Convert.ToInt32("7" + AnoComboBox.Text);
                            fim = Convert.ToInt32("12" + AnoComboBox.Text);
                        }
                    }

                    Classes.Pontuacao _pontuacao2 = new Pontuacao();
                    Classes.Pontuacao _pontuacao1 = new Pontuacao();

                    if (!AmbosRadioButton.Checked)
                        _pontuacao1 = new PontuacaoBO().ConsultarMaiorReferencia(fim);
                    else
                    {
                        _pontuacao1 = new PontuacaoBO().ConsultarMaiorReferencia(Convert.ToInt32("6" + AnoComboBox.Text));

                        if (!_pontuacao1.Existe)
                            _pontuacao1 = new PontuacaoBO().ConsultarMaiorReferencia(fim);

                        _pontuacao2 = new PontuacaoBO().ConsultarMaiorReferencia(fim);
                    }


                    #region Preparação dos Dados

                    int contMetas = 1;
                    int contsemestre1 = 1;
                    int contsemestre2 = 1;
                    decimal _mediaA1 = 0;
                    decimal _mediaA2 = 0;
                    decimal _mediaQ1 = 0;
                    decimal _mediaQ2 = 0;

                    _listaNotasDetalhado = new List<Resultados>();

                    foreach (var col in _metasNumericas.GroupBy(g => g.IdEmpresa)
                                                   .Select(s => s.Key))
                    {
                        contMetas = 1;
                        contsemestre1 = 1;
                        contsemestre2 = 1;
                        _mediaA1 = 0;
                        _mediaA2 = 0;
                        _mediaQ1 = 0;
                        _mediaQ2 = 0;

                        Resultados _result = new Resultados();

                       
                        foreach (var item in _metasNumericas.Where(w => w.IdEmpresa == col)
                                                        .OrderBy(o => o.Ordem)
                                                        .OrderBy(o => o.Tipo))
                        {

                            _result.Empresa = item.Empresa;
                            _result.Colaborador = item.Colaborador;
                            _result.Cargos = item.Cargo;
                            _result.IdEmpresa = item.IdEmpresa;
                            _result.IdColaborador = item.IdColaborador;

                            if (AmbosRadioButton.Checked)
                            {
                                if (item.MesReferencia == inicio)
                                    contMetas = 1;
                                if (item.MesReferencia == inicio + 10000)
                                    contMetas = 2;
                                if (item.MesReferencia == inicio + 20000)
                                    contMetas = 3;
                                if (item.MesReferencia == inicio + 30000)
                                    contMetas = 4;
                                if (item.MesReferencia == inicio + 40000)
                                    contMetas = 5;
                                if (item.MesReferencia == inicio + 50000)
                                    contMetas = 6;
                                if (item.MesReferencia == inicio + 60000)
                                    contMetas = 7;
                                if (item.MesReferencia == inicio + 70000)
                                    contMetas = 8;
                                if (item.MesReferencia == inicio + 80000)
                                    contMetas = 9;
                                if (item.MesReferencia == inicio + 90000)
                                    contMetas = 10;
                                if (item.MesReferencia == inicio + 100000)
                                    contMetas = 11;
                                if (item.MesReferencia == inicio + 110000)
                                    contMetas = 12;
                            }
                            else
                            {
                                if (item.MesReferencia == inicio)
                                    contMetas = 1;
                                if (item.MesReferencia == inicio + 10000)
                                    contMetas = 2;
                                if (item.MesReferencia == inicio + 20000)
                                    contMetas = 3;
                                if (item.MesReferencia == inicio + 30000)
                                    contMetas = 4;
                                if (item.MesReferencia == inicio + 40000)
                                    contMetas = 5;
                                if (item.MesReferencia == inicio + 50000)
                                    contMetas = 6;
                            }

                            switch (contMetas)
                            {
                                case 1:
                                    _result.Nota1 = item.TotalAvaliacao;
                                    _result.NotaReferencia1 = item.MesReferencia.ToString("00/0000");
                                    _mediaQ1 = _mediaQ1 + _result.Nota1;
                                    contsemestre1++;
                                    break;
                                case 2:
                                    _result.Nota2 = item.TotalAvaliacao;
                                    _result.NotaReferencia2 = item.MesReferencia.ToString("00/0000");
                                    _mediaQ1 = _mediaQ1 + _result.Nota2;
                                    contsemestre1++;
                                    break;
                                case 3:
                                    _result.Nota3 = item.TotalAvaliacao;
                                    _result.NotaReferencia3 = item.MesReferencia.ToString("00/0000");
                                    _mediaQ1 = _mediaQ1 + _result.Nota3;
                                    contsemestre1++;
                                    break;
                                case 4:
                                    _result.Nota4 = item.TotalAvaliacao;
                                    _result.NotaReferencia4 = item.MesReferencia.ToString("00/0000");
                                    _mediaQ1 = _mediaQ1 + _result.Nota4;
                                    contsemestre1++;
                                    break;
                                case 5:
                                    _result.Nota5 = item.TotalAvaliacao;
                                    _result.NotaReferencia5 = item.MesReferencia.ToString("00/0000");
                                    _mediaQ1 = _mediaQ1 + _result.Nota5;
                                    contsemestre1++;
                                    break;
                                case 6:
                                    _result.Nota6 = item.TotalAvaliacao;
                                    _result.NotaReferencia6 = item.MesReferencia.ToString("00/0000");
                                    _mediaQ1 = _mediaQ1 + _result.Nota6;
                                    contsemestre1++;
                                    break;
                                case 7:
                                    _result.Nota7 = item.TotalAvaliacao;
                                    _result.NotaReferencia7 = item.MesReferencia.ToString("00/0000");
                                    _mediaQ2 = _mediaQ2 + _result.Nota7;
                                    contsemestre2++;
                                    break;
                                case 8:
                                    _result.Nota8 = item.TotalAvaliacao;
                                    _result.NotaReferencia8 = item.MesReferencia.ToString("00/0000");
                                    _mediaQ2 = _mediaQ2 + _result.Nota8;
                                    contsemestre2++;
                                    break;
                                case 9:
                                    _result.Nota9 = item.TotalAvaliacao;
                                    _result.NotaReferencia9 = item.MesReferencia.ToString("00/0000");
                                    _mediaQ2 = _mediaQ2 + _result.Nota9;
                                    contsemestre2++;
                                    break;
                                case 10:
                                    _result.Nota10 = item.TotalAvaliacao;
                                    _result.NotaReferencia10 = item.MesReferencia.ToString("00/0000");
                                    _mediaQ2 = _mediaQ2 + _result.Nota10;
                                    contsemestre2++;
                                    break;
                                case 11:
                                    _result.Nota11 = item.TotalAvaliacao;
                                    _result.NotaReferencia11 = item.MesReferencia.ToString("00/0000");
                                    _mediaQ2 = _mediaQ2 + _result.Nota11;
                                    contsemestre2++;
                                    break;
                                case 12:
                                    _result.Nota12 = item.TotalAvaliacao;
                                    _result.NotaReferencia12 = item.MesReferencia.ToString("00/0000");
                                    _mediaQ2 = _mediaQ2 + _result.Nota12;
                                    contsemestre2++;
                                    break;
                            }
                        }
                        
                        _result.MediaAvaliacao1 = Math.Round(_mediaA1 / 3, 2);
                        _result.MediaAvaliacao2 = Math.Round(_mediaA2 / 3, 2);

                        if (_mediaQ1 > 0)
                            _result.MediaSemestre1 = Math.Round(_mediaQ1 / contsemestre1 - 1, 2);
                        if (_mediaQ2 > 0)
                            _result.MediaSemestre2 = Math.Round(_mediaQ2 / contsemestre2 - 1, 2);


                        _result.NotaFinal1 = Math.Round(_result.MediaAvaliacao1 * (_pontuacao1.PesoQualitativa / 100) +
                                             _result.MediaSemestre1 * (_pontuacao1.PesoNumerica / 100), 2);

                        if (_pontuacao2 != null && _pontuacao2.Existe)
                        {
                            _result.NotaFinal2 = Math.Round(_result.MediaAvaliacao2 * (_pontuacao2.PesoQualitativa * 100) +
                                                 _result.MediaSemestre2 * (_pontuacao2.PesoNumerica * 100), 2);
                        }
                        _listaNotasDetalhado.Add(_result);
                    }
                    #endregion


                    GridColumnDescriptor _col = new GridColumnDescriptor("Empresa", "Empresa", "Empresa");
                    _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                    _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;
                    _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                    NumericaGridGroupingControl.TableDescriptor.Columns.Add(_col);

                    GridStackedHeaderRowDescriptor stackedHeaderRowDescriptor;
                    List<GridStackedHeaderDescriptor> _listaHeader = new List<GridStackedHeaderDescriptor>();
                    GridStackedHeaderDescriptor gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();

                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota1");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota2");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota3");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota4");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota5");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota6");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("MediaSemestre1");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota7");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota8");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota9");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota10");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota11");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("Nota12");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Remove("MediaSemestre2");

                    string _coluna = "";
                    int referencia = Convert.ToInt32((AmbosRadioButton.Checked || Semestre1RadioButton.Checked ? "1"+AnoComboBox.Text : "7" + AnoComboBox.Text));

                    for (int i = 1; i < (AmbosRadioButton.Checked ? 13 : 7); i++)
                    {
                        switch (i)
                        {
                            case 1: 
                                _coluna = referencia.ToString("00/0000");
                                break;
                            case 2:
                                _coluna = referencia.ToString("00/0000"); 
                                break;
                            case 3:
                                _coluna = referencia.ToString("00/0000");
                                break;
                            case 4:
                                _coluna = referencia.ToString("00/0000");
                                break;
                            case 5:
                                _coluna = referencia.ToString("00/0000");
                                break;
                            case 6:
                                _coluna = referencia.ToString("00/0000");
                                break;
                            case 7:
                                _coluna = referencia.ToString("00/0000");
                                break;
                            case 8:
                                _coluna = referencia.ToString("00/0000");
                                break;
                            case 9:
                                _coluna = referencia.ToString("00/0000");
                                break;
                            case 10:
                                _coluna = referencia.ToString("00/0000");
                                break;
                            case 11:
                                _coluna = referencia.ToString("00/0000");
                                break;
                            case 12:
                                _coluna = referencia.ToString("00/0000");
                                break;
                        }

                        referencia = referencia + 10000;


                        _col = new GridColumnDescriptor("Nota" + i.ToString(), "Nota" + i.ToString(), _coluna);
                        _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        NumericaGridGroupingControl.TableDescriptor.Columns.Add(_col);

                        if (i == 6)
                        {
                            _col = new GridColumnDescriptor("MediaSemestre1", "MediaSemestre1", "Média");
                            _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                            _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                            _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                            NumericaGridGroupingControl.TableDescriptor.Columns.Add(_col);
                        }
                    }

                    if (AmbosRadioButton.Checked)
                    {
                        _col = new GridColumnDescriptor("MediaSemestre2", "MediaSemestre2", "Média");
                        _col.Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        _col.Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Right;
                        _col.Appearance.ColumnHeaderCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        NumericaGridGroupingControl.TableDescriptor.Columns.Add(_col);
                    }

                    gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();
                    gridStackedHeaderDescriptor1.HeaderText = (Semestre1RadioButton.Checked || AmbosRadioButton.Checked ? "1º Semestre" : "2º Semestre");

                    gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Nota1")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Nota2")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Nota3")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Nota4")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Nota5")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("Nota6")
                                                                                                                , new GridStackedHeaderVisibleColumnDescriptor("MediaSemestre1")});

                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota1");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota2");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota3");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota4");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota5");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota6");
                    NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("MediaSemestre1");

                    _listaHeader.Add(gridStackedHeaderDescriptor1);

                    if (AmbosRadioButton.Checked)
                    {
                        NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota7");
                        NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota8");
                        NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota9");
                        NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota10");
                        NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota11");
                        NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("Nota12");
                        NumericaGridGroupingControl.TableDescriptor.VisibleColumns.Add("MediaSemestre2");

                        gridStackedHeaderDescriptor1 = new GridStackedHeaderDescriptor();
                        gridStackedHeaderDescriptor1.HeaderText = "2º Semestre";

                        gridStackedHeaderDescriptor1.VisibleColumns.AddRange(new GridStackedHeaderVisibleColumnDescriptor[] { new GridStackedHeaderVisibleColumnDescriptor("Nota7")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota8")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota9")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota10")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota11")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("Nota12")
                                                                                                                    , new GridStackedHeaderVisibleColumnDescriptor("MediaSemestre2")});

                        _listaHeader.Add(gridStackedHeaderDescriptor1);
                    }

                    stackedHeaderRowDescriptor = new GridStackedHeaderRowDescriptor("Row1", _listaHeader.ToArray());

                    NumericaGridGroupingControl.TableDescriptor.StackedHeaderRows.Add(stackedHeaderRowDescriptor);

                    NumericaGridGroupingControl.DataSource = _listaNotasDetalhado;

                    NumericaGridGroupingControl.TableDescriptor.Relations.Clear();
                }

            }
            catch { }

            tabControlAdv1.SelectedTab = DetalhamentoTabPage;
        }

        private void MontaGrafico(int referencia, ChartControl radar)
        {
            string tipo = "";
            string tipoLegenda = "";

            ChartSeries serieRH = null;
            int i = 1;
            bool _legenda = false;
            listBox1.Items.Clear();

            radar.Series.Clear();

            foreach (var item in _avaliacoes.Where(w => w.MesReferencia == referencia)
                                            .OrderBy(o => o.Tipo))
            {

                if (item.Tipo == Publicas.TipoPrazos.AutoAvaliacao)
                    tipoLegenda = "Auto";
                else
                {
                    if (item.Tipo == Publicas.TipoPrazos.AvaliacaoDoGestor)
                        tipoLegenda = "Gestor";
                    else
                        tipoLegenda = "RH";
                }

                if (tipo == "")
                {
                    tipo = Publicas.GetDescription(item.Tipo, "");
                    
                    serieRH = new ChartSeries(tipoLegenda, ChartSeriesType.Radar);
                    _legenda = true;
                }

                if (tipo != Publicas.GetDescription(item.Tipo, ""))
                {
                    radar.Series.Add(serieRH);
                    tipo = Publicas.GetDescription(item.Tipo, "");
                    serieRH = new ChartSeries(tipoLegenda, ChartSeriesType.Radar);
                    _legenda = false;
                }

                if (_legenda)
                {
                    listBox1.Items.Add(i.ToString() + " - " + item.Comentario);
                    serieRH.Points.Add(i.ToString(), (double)item.TotalAvaliacao);
                    i++;
                }
                else
                {
                    for (int j = 0; j < listBox1.Items.Count; j++)
                    {
                        if (listBox1.Items[j].ToString().Contains(item.Comentario))
                        {
                            i = j + 1;
                            serieRH.Points.Add(i.ToString(), (double)item.TotalAvaliacao);
                        }
                    }                    
                }
            }

            if (serieRH != null)
                radar.Series.Add(serieRH);
        }

        private void Notas_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }
    }
}
