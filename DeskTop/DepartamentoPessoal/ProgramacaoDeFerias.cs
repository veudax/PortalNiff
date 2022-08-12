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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.DepartamentoPessoal
{
    public partial class ProgramacaoDeFerias : Form
    {
        public ProgramacaoDeFerias()
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
                    RecolherCheckBox.ForeColor = Publicas._fonte;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.ProgramacaoFerias> _listaProgramacao;
        List<ProgramacaoFeriasGlobus> _listaGlobus;
        Classes.Empresa _empresa;
        List<Classes.Empresa> _listaEmpresas;
        DateTime _dataInicio = DateTime.MaxValue;
        DateTime _data = DateTime.MaxValue;

        System.Xml.XmlTextWriter gravaColunasGridCabecalho;
        System.Xml.XmlTextWriter gravaCoresGridCabecalho;
        System.Xml.XmlReader leColunasGridCabecalho;
        System.Xml.XmlReader leCorGridCabecalho;
        string _diretorioCor = "";
        string _diretorioCab = "";

        GridCurrentCell _colunaCorrente;

        class Programacao
        {
            public int CodIntFunc { get; set; }
            public string Funcionario { get; set; }
            public string Periodo { get; set; }
            public string Departamento { get; set; }
            public int IdDepartamento { get; set; }
            public string MotivoReprovacao { get; set; }

            #region janeiro
            public string JanDia1 { get; set; }
            public string JanDia2 { get; set; }
            public string JanDia3 { get; set; }
            public string JanDia4 { get; set; }
            public string JanDia5 { get; set; }   
            public string JanDia6 { get; set; }
            public string JanDia7 { get; set; }
            public string JanDia8 { get; set; }
            public string JanDia9 { get; set; }
            public string JanDia10 { get; set; }
            public string JanDia11 { get; set; }
            public string JanDia12 { get; set; }
            public string JanDia13 { get; set; }
            public string JanDia14 { get; set; }
            public string JanDia15 { get; set; }
            public string JanDia16 { get; set; }
            public string JanDia17 { get; set; }
            public string JanDia18 { get; set; }
            public string JanDia19 { get; set; }
            public string JanDia20 { get; set; }  
            public string JanDia21 { get; set; }
            public string JanDia22 { get; set; }
            public string JanDia23 { get; set; }
            public string JanDia24 { get; set; }
            public string JanDia25 { get; set; }
            public string JanDia26 { get; set; }
            public string JanDia27 { get; set; }
            public string JanDia28 { get; set; }
            public string JanDia29 { get; set; }
            public string JanDia30 { get; set; }
            public string JanDia31 { get; set; }
            #endregion

            #region Fevereiro
            public string FevDia1 { get; set; }
            public string FevDia2 { get; set; }
            public string FevDia3 { get; set; }
            public string FevDia4 { get; set; }
            public string FevDia5 { get; set; }
            public string FevDia6 { get; set; }
            public string FevDia7 { get; set; }
            public string FevDia8 { get; set; }
            public string FevDia9 { get; set; }
            public string FevDia10 { get; set; }
            public string FevDia11 { get; set; }
            public string FevDia12 { get; set; }
            public string FevDia13 { get; set; }
            public string FevDia14 { get; set; }
            public string FevDia15 { get; set; }
            public string FevDia16 { get; set; }
            public string FevDia17 { get; set; }
            public string FevDia18 { get; set; }
            public string FevDia19 { get; set; }
            public string FevDia20 { get; set; }
            public string FevDia21 { get; set; }
            public string FevDia22 { get; set; }
            public string FevDia23 { get; set; }
            public string FevDia24 { get; set; }
            public string FevDia25 { get; set; }
            public string FevDia26 { get; set; }
            public string FevDia27 { get; set; }
            public string FevDia28 { get; set; }
            public string FevDia29 { get; set; }
            #endregion

            #region Marco
            public string MarDia1 { get; set; }
            public string MarDia2 { get; set; }
            public string MarDia3 { get; set; }
            public string MarDia4 { get; set; }
            public string MarDia5 { get; set; }
            public string MarDia6 { get; set; }
            public string MarDia7 { get; set; }
            public string MarDia8 { get; set; }
            public string MarDia9 { get; set; }
            public string MarDia10 { get; set; }
            public string MarDia11 { get; set; }
            public string MarDia12 { get; set; }
            public string MarDia13 { get; set; }
            public string MarDia14 { get; set; }
            public string MarDia15 { get; set; }
            public string MarDia16 { get; set; }
            public string MarDia17 { get; set; }
            public string MarDia18 { get; set; }
            public string MarDia19 { get; set; }
            public string MarDia20 { get; set; }
            public string MarDia21 { get; set; }
            public string MarDia22 { get; set; }
            public string MarDia23 { get; set; }
            public string MarDia24 { get; set; }
            public string MarDia25 { get; set; }
            public string MarDia26 { get; set; }
            public string MarDia27 { get; set; }
            public string MarDia28 { get; set; }
            public string MarDia29 { get; set; }
            public string MarDia30 { get; set; }
            public string MarDia31 { get; set; }
            #endregion

            #region Abril
            public string AbrDia1 { get; set; }
            public string AbrDia2 { get; set; }
            public string AbrDia3 { get; set; }
            public string AbrDia4 { get; set; }
            public string AbrDia5 { get; set; }
            public string AbrDia6 { get; set; }
            public string AbrDia7 { get; set; }
            public string AbrDia8 { get; set; }
            public string AbrDia9 { get; set; }
            public string AbrDia10 { get; set; }
            public string AbrDia11 { get; set; }
            public string AbrDia12 { get; set; }
            public string AbrDia13 { get; set; }
            public string AbrDia14 { get; set; }
            public string AbrDia15 { get; set; }
            public string AbrDia16 { get; set; }
            public string AbrDia17 { get; set; }
            public string AbrDia18 { get; set; }
            public string AbrDia19 { get; set; }
            public string AbrDia20 { get; set; }
            public string AbrDia21 { get; set; }
            public string AbrDia22 { get; set; }
            public string AbrDia23 { get; set; }
            public string AbrDia24 { get; set; }
            public string AbrDia25 { get; set; }
            public string AbrDia26 { get; set; }
            public string AbrDia27 { get; set; }
            public string AbrDia28 { get; set; }
            public string AbrDia29 { get; set; }
            public string AbrDia30 { get; set; }
            #endregion

            #region Maio
            public string MaiDia1 { get; set; }
            public string MaiDia2 { get; set; }
            public string MaiDia3 { get; set; }
            public string MaiDia4 { get; set; }
            public string MaiDia5 { get; set; }
            public string MaiDia6 { get; set; }
            public string MaiDia7 { get; set; }
            public string MaiDia8 { get; set; }
            public string MaiDia9 { get; set; }
            public string MaiDia10 { get; set; }
            public string MaiDia11 { get; set; }
            public string MaiDia12 { get; set; }
            public string MaiDia13 { get; set; }
            public string MaiDia14 { get; set; }
            public string MaiDia15 { get; set; }
            public string MaiDia16 { get; set; }
            public string MaiDia17 { get; set; }
            public string MaiDia18 { get; set; }
            public string MaiDia19 { get; set; }
            public string MaiDia20 { get; set; }
            public string MaiDia21 { get; set; }
            public string MaiDia22 { get; set; }
            public string MaiDia23 { get; set; }
            public string MaiDia24 { get; set; }
            public string MaiDia25 { get; set; }
            public string MaiDia26 { get; set; }
            public string MaiDia27 { get; set; }
            public string MaiDia28 { get; set; }
            public string MaiDia29 { get; set; }
            public string MaiDia30 { get; set; }
            public string MaiDia31 { get; set; }
            #endregion

            #region Junho
            public string JunDia1 { get; set; }
            public string JunDia2 { get; set; }
            public string JunDia3 { get; set; }
            public string JunDia4 { get; set; }
            public string JunDia5 { get; set; }
            public string JunDia6 { get; set; }
            public string JunDia7 { get; set; }
            public string JunDia8 { get; set; }
            public string JunDia9 { get; set; }
            public string JunDia10 { get; set; }
            public string JunDia11 { get; set; }
            public string JunDia12 { get; set; }
            public string JunDia13 { get; set; }
            public string JunDia14 { get; set; }
            public string JunDia15 { get; set; }
            public string JunDia16 { get; set; }
            public string JunDia17 { get; set; }
            public string JunDia18 { get; set; }
            public string JunDia19 { get; set; }
            public string JunDia20 { get; set; }
            public string JunDia21 { get; set; }
            public string JunDia22 { get; set; }
            public string JunDia23 { get; set; }
            public string JunDia24 { get; set; }
            public string JunDia25 { get; set; }
            public string JunDia26 { get; set; }
            public string JunDia27 { get; set; }
            public string JunDia28 { get; set; }
            public string JunDia29 { get; set; }
            public string JunDia30 { get; set; }
            #endregion

            #region Julio
            public string JulDia1 { get; set; }
            public string JulDia2 { get; set; }
            public string JulDia3 { get; set; }
            public string JulDia4 { get; set; }
            public string JulDia5 { get; set; }
            public string JulDia6 { get; set; }
            public string JulDia7 { get; set; }
            public string JulDia8 { get; set; }
            public string JulDia9 { get; set; }
            public string JulDia10 { get; set; }
            public string JulDia11 { get; set; }
            public string JulDia12 { get; set; }
            public string JulDia13 { get; set; }
            public string JulDia14 { get; set; }
            public string JulDia15 { get; set; }
            public string JulDia16 { get; set; }
            public string JulDia17 { get; set; }
            public string JulDia18 { get; set; }
            public string JulDia19 { get; set; }
            public string JulDia20 { get; set; }
            public string JulDia21 { get; set; }
            public string JulDia22 { get; set; }
            public string JulDia23 { get; set; }
            public string JulDia24 { get; set; }
            public string JulDia25 { get; set; }
            public string JulDia26 { get; set; }
            public string JulDia27 { get; set; }
            public string JulDia28 { get; set; }
            public string JulDia29 { get; set; }
            public string JulDia30 { get; set; }
            public string JulDia31 { get; set; }
            #endregion

            #region Agosto
            public string AgoDia1 { get; set; }
            public string AgoDia2 { get; set; }
            public string AgoDia3 { get; set; }
            public string AgoDia4 { get; set; }
            public string AgoDia5 { get; set; }
            public string AgoDia6 { get; set; }
            public string AgoDia7 { get; set; }
            public string AgoDia8 { get; set; }
            public string AgoDia9 { get; set; }
            public string AgoDia10 { get; set; }
            public string AgoDia11 { get; set; }
            public string AgoDia12 { get; set; }
            public string AgoDia13 { get; set; }
            public string AgoDia14 { get; set; }
            public string AgoDia15 { get; set; }
            public string AgoDia16 { get; set; }
            public string AgoDia17 { get; set; }
            public string AgoDia18 { get; set; }
            public string AgoDia19 { get; set; }
            public string AgoDia20 { get; set; }
            public string AgoDia21 { get; set; }
            public string AgoDia22 { get; set; }
            public string AgoDia23 { get; set; }
            public string AgoDia24 { get; set; }
            public string AgoDia25 { get; set; }
            public string AgoDia26 { get; set; }
            public string AgoDia27 { get; set; }
            public string AgoDia28 { get; set; }
            public string AgoDia29 { get; set; }
            public string AgoDia30 { get; set; }
            public string AgoDia31 { get; set; }
            #endregion

            #region Setembro
            public string SetDia1 { get; set; }
            public string SetDia2 { get; set; }
            public string SetDia3 { get; set; }
            public string SetDia4 { get; set; }
            public string SetDia5 { get; set; }
            public string SetDia6 { get; set; }
            public string SetDia7 { get; set; }
            public string SetDia8 { get; set; }
            public string SetDia9 { get; set; }
            public string SetDia10 { get; set; }
            public string SetDia11 { get; set; }
            public string SetDia12 { get; set; }
            public string SetDia13 { get; set; }
            public string SetDia14 { get; set; }
            public string SetDia15 { get; set; }
            public string SetDia16 { get; set; }
            public string SetDia17 { get; set; }
            public string SetDia18 { get; set; }
            public string SetDia19 { get; set; }
            public string SetDia20 { get; set; }
            public string SetDia21 { get; set; }
            public string SetDia22 { get; set; }
            public string SetDia23 { get; set; }
            public string SetDia24 { get; set; }
            public string SetDia25 { get; set; }
            public string SetDia26 { get; set; }
            public string SetDia27 { get; set; }
            public string SetDia28 { get; set; }
            public string SetDia29 { get; set; }
            public string SetDia30 { get; set; }
            #endregion

            #region Outubro
            public string OutDia1 { get; set; }
            public string OutDia2 { get; set; }
            public string OutDia3 { get; set; }
            public string OutDia4 { get; set; }
            public string OutDia5 { get; set; }
            public string OutDia6 { get; set; }
            public string OutDia7 { get; set; }
            public string OutDia8 { get; set; }
            public string OutDia9 { get; set; }
            public string OutDia10 { get; set; }
            public string OutDia11 { get; set; }
            public string OutDia12 { get; set; }
            public string OutDia13 { get; set; }
            public string OutDia14 { get; set; }
            public string OutDia15 { get; set; }
            public string OutDia16 { get; set; }
            public string OutDia17 { get; set; }
            public string OutDia18 { get; set; }
            public string OutDia19 { get; set; }
            public string OutDia20 { get; set; }
            public string OutDia21 { get; set; }
            public string OutDia22 { get; set; }
            public string OutDia23 { get; set; }
            public string OutDia24 { get; set; }
            public string OutDia25 { get; set; }
            public string OutDia26 { get; set; }
            public string OutDia27 { get; set; }
            public string OutDia28 { get; set; }
            public string OutDia29 { get; set; }
            public string OutDia30 { get; set; }
            public string OutDia31 { get; set; }
            #endregion

            #region Novembro
            public string NovDia1 { get; set; }
            public string NovDia2 { get; set; }
            public string NovDia3 { get; set; }
            public string NovDia4 { get; set; }
            public string NovDia5 { get; set; }
            public string NovDia6 { get; set; }
            public string NovDia7 { get; set; }
            public string NovDia8 { get; set; }
            public string NovDia9 { get; set; }
            public string NovDia10 { get; set; }
            public string NovDia11 { get; set; }
            public string NovDia12 { get; set; }
            public string NovDia13 { get; set; }
            public string NovDia14 { get; set; }
            public string NovDia15 { get; set; }
            public string NovDia16 { get; set; }
            public string NovDia17 { get; set; }
            public string NovDia18 { get; set; }
            public string NovDia19 { get; set; }
            public string NovDia20 { get; set; }
            public string NovDia21 { get; set; }
            public string NovDia22 { get; set; }
            public string NovDia23 { get; set; }
            public string NovDia24 { get; set; }
            public string NovDia25 { get; set; }
            public string NovDia26 { get; set; }
            public string NovDia27 { get; set; }
            public string NovDia28 { get; set; }
            public string NovDia29 { get; set; }
            public string NovDia30 { get; set; }
            #endregion

            #region Dezembro
            public string DezDia1 { get; set; }
            public string DezDia2 { get; set; }
            public string DezDia3 { get; set; }
            public string DezDia4 { get; set; }
            public string DezDia5 { get; set; }
            public string DezDia6 { get; set; }
            public string DezDia7 { get; set; }
            public string DezDia8 { get; set; }
            public string DezDia9 { get; set; }
            public string DezDia10 { get; set; }
            public string DezDia11 { get; set; }
            public string DezDia12 { get; set; }
            public string DezDia13 { get; set; }
            public string DezDia14 { get; set; }
            public string DezDia15 { get; set; }
            public string DezDia16 { get; set; }
            public string DezDia17 { get; set; }
            public string DezDia18 { get; set; }
            public string DezDia19 { get; set; }
            public string DezDia20 { get; set; }
            public string DezDia21 { get; set; }
            public string DezDia22 { get; set; }
            public string DezDia23 { get; set; }
            public string DezDia24 { get; set; }
            public string DezDia25 { get; set; }
            public string DezDia26 { get; set; }
            public string DezDia27 { get; set; }
            public string DezDia28 { get; set; }
            public string DezDia29 { get; set; }
            public string DezDia30 { get; set; }
            public string DezDia31 { get; set; }
            #endregion
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

        private void ProgramacaoDeFerias_Shown(object sender, EventArgs e)
        {
            this.Top = 60;

            aprovarReprovarToolStripMenuItem.Enabled = Publicas._usuario.Diretor || Publicas._usuario.Gerente || 
                                                       Publicas._usuario.Coordenador || Publicas._usuario.Desenvolvedor;

            if (Environment.MachineName.ToUpper().Contains("CORPTS") || Environment.MachineName.ToUpper().Contains("CORPRDP"))
            {
                _diretorioCab = Path.GetDirectoryName(Application.ExecutablePath) + @"\xml\GridCabecalhoProgramacaoFerias" + Publicas._usuario.Id + ".xml";
                _diretorioCor = Path.GetDirectoryName(Application.ExecutablePath) + @"\xml\CorGridCabecalhoProgramacaoFerias" + Publicas._usuario.Id + ".xml";
            }
            else
            {
                _diretorioCab = Publicas._caminhoPortal + "GridCabecalhoProgramacaoFerias.xml";
                _diretorioCor = Publicas._caminhoPortal + "CorGridCabecalhoProgramacaoFerias.xml";
            }

            gravaColunasGridCabecalho = new System.Xml.XmlTextWriter(_diretorioCab, System.Text.Encoding.UTF8);
            gravaCoresGridCabecalho = new System.Xml.XmlTextWriter(_diretorioCor, System.Text.Encoding.UTF8);

            gravaColunasGridCabecalho.Formatting = System.Xml.Formatting.Indented;
            gravaCoresGridCabecalho.Formatting = System.Xml.Formatting.Indented;

            gridGroupingControl1.WriteXmlSchema(gravaColunasGridCabecalho);
            gridGroupingControl1.WriteXmlLookAndFeel(gravaCoresGridCabecalho);
            gravaColunasGridCabecalho.Close();
            gravaCoresGridCabecalho.Close();


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

            gridGroupingControl1.TableControl.CellToolTip.InitialDelay = 100;
            gridGroupingControl1.TableControl.CellToolTip.AutoPopDelay = 5000;

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            
            IntegrarButton.Visible = Publicas._usuario.PodeIntegrarProgramacaoFerias;
            IntegrarButton.Enabled = false;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GozadasCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GozadasCheckBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void limparButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
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

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            leColunasGridCabecalho = new System.Xml.XmlTextReader(_diretorioCab);
            gridGroupingControl1.ApplyXmlSchema(leColunasGridCabecalho);

            leCorGridCabecalho = new System.Xml.XmlTextReader(_diretorioCor);
            gridGroupingControl1.ApplyXmlLookAndFeel(leCorGridCabecalho);

            leColunasGridCabecalho.Close();
            leCorGridCabecalho.Close();

            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            // Para pegar o último dia do mês selecionado.
            try
            {
                _dataInicio = Convert.ToDateTime("01/01/" + referenciaMaskedEditBox.Text);
                _data = _dataInicio.AddMonths(12).AddDays(-1);

                // Se mostrar só o periodo atual, busca no banco também o periodo atual até o final do ano
                if (!GozadasCheckBox.Checked && referenciaMaskedEditBox.Text == DateTime.Now.Date.Year.ToString())
                    _dataInicio = Convert.ToDateTime("01/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year);
                else
                {
                    if (!GozadasCheckBox.Checked && referenciaMaskedEditBox.Text != DateTime.Now.Date.Year.ToString())
                        GozadasCheckBox.Checked = true;
                }
            }
            catch
            {
                new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            mensagemSistemaLabel.Text = "Pesquisando, Aguarde ...";
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();
            _listaProgramacao = new ProgramacaoFeriasBO().Listar(_empresa.IdEmpresa, _dataInicio, _data, false, MostrarGozadasCheckBox.Checked);
            bool _encontrou = false;
            
            List<Programacao> _lista = new List<Programacao>();

            foreach (var itemG in _listaProgramacao.GroupBy(g => new { g.CodIntFunc, g.DataInicio, g.DataFim, g.Funcionario,
                                                    g.Status, g.Departamento , g.IdDepartamento, g.MotivoReprovacao,
                                                    g.QuantidadeDias})
                                                   .OrderBy(o => o.Key.DataInicio))
            {
                Programacao _prog = new Programacao();
                _prog.CodIntFunc = itemG.Key.CodIntFunc;
                _prog.Periodo = itemG.Key.DataInicio.ToShortDateString() + " até " + itemG.Key.DataFim.ToShortDateString() 
                    + " ( "  +itemG.Key.QuantidadeDias + " dias " + (itemG.Key.Status == "R" ? "Rep " : "") + ")";
                _prog.Funcionario = itemG.Key.Funcionario;
                _prog.Departamento = itemG.Key.Departamento;
                _prog.IdDepartamento = itemG.Key.IdDepartamento;

                _prog.MotivoReprovacao = _prog.MotivoReprovacao + 
                    (itemG.Key.MotivoReprovacao != "" ? "Motivo do Periodo " + 
                    itemG.Key.DataInicio.ToShortDateString() + " até " + itemG.Key.DataFim.ToShortDateString() +
                    " [ " + itemG.Key.MotivoReprovacao + " ]": "");

                _encontrou = false;
                foreach (var item in _lista.Where(w => w.CodIntFunc == itemG.Key.CodIntFunc))
                {
                 // para o segundo período
                    _prog = item;
                    _encontrou = true;
                    _prog.Periodo = _prog.Periodo + " - " + Environment.NewLine + 
                        itemG.Key.DataInicio.ToShortDateString() + " até " + itemG.Key.DataFim.ToShortDateString() + 
                        " ( " + itemG.Key.QuantidadeDias + " dias " + (itemG.Key.Status == "R" ? "Rep " : "") + ")";  

                    _prog.MotivoReprovacao = _prog.MotivoReprovacao +
                        (itemG.Key.MotivoReprovacao != "" ? "Motivo do Periodo " +
                        itemG.Key.DataInicio.ToShortDateString() + " até " + itemG.Key.DataFim.ToShortDateString() +
                        " [ " + itemG.Key.MotivoReprovacao + " ]" : "");

                }

                _data = itemG.Key.DataInicio;

                while (_data <= itemG.Key.DataFim)
                {
                    if (_data.Year.ToString() != referenciaMaskedEditBox.Text)
                    {
                        _data = _data.AddDays(1);
                        continue;
                    }
                    if ((!GozadasCheckBox.Checked && _data.Date.Month >= DateTime.Now.Date.Month) ||
                        (GozadasCheckBox.Checked))
                    {
                        switch (_data.Month)
                        {
                            case 1:
                                switch (_data.Day)
                                {
                                    case 1:
                                        _prog.JanDia1 = itemG.Key.Status;                                        
                                        break;
                                    case 2:
                                        _prog.JanDia2 = itemG.Key.Status;
                                        break;
                                    case 3:
                                        _prog.JanDia3 = itemG.Key.Status;
                                        break;
                                    case 4:
                                        _prog.JanDia4 = itemG.Key.Status;
                                        break;
                                    case 5:
                                        _prog.JanDia5 = itemG.Key.Status;
                                        break;
                                    case 6:
                                        _prog.JanDia6 = itemG.Key.Status;
                                        break;
                                    case 7:
                                        _prog.JanDia7 = itemG.Key.Status;
                                        break;
                                    case 8:
                                        _prog.JanDia8 = itemG.Key.Status;
                                        break;
                                    case 9:
                                        _prog.JanDia9 = itemG.Key.Status;
                                        break;
                                    case 10:
                                        _prog.JanDia10 = itemG.Key.Status;
                                        break;
                                    case 11:
                                        _prog.JanDia11 = itemG.Key.Status;
                                        break;
                                    case 12:
                                        _prog.JanDia12 = itemG.Key.Status;
                                        break;
                                    case 13:
                                        _prog.JanDia13 = itemG.Key.Status;
                                        break;
                                    case 14:
                                        _prog.JanDia14 = itemG.Key.Status;
                                        break;
                                    case 15:
                                        _prog.JanDia15 = itemG.Key.Status;
                                        break;
                                    case 16:
                                        _prog.JanDia16 = itemG.Key.Status;
                                        break;
                                    case 17:
                                        _prog.JanDia17 = itemG.Key.Status;
                                        break;
                                    case 18:
                                        _prog.JanDia18 = itemG.Key.Status;
                                        break;
                                    case 19:
                                        _prog.JanDia19 = itemG.Key.Status;
                                        break;
                                    case 20:
                                        _prog.JanDia20 = itemG.Key.Status;
                                        break;
                                    case 21:
                                        _prog.JanDia21 = itemG.Key.Status;
                                        break;
                                    case 22:
                                        _prog.JanDia22 = itemG.Key.Status;
                                        break;
                                    case 23:
                                        _prog.JanDia23 = itemG.Key.Status;
                                        break;
                                    case 24:
                                        _prog.JanDia24 = itemG.Key.Status;
                                        break;
                                    case 25:
                                        _prog.JanDia25 = itemG.Key.Status;
                                        break;
                                    case 26:
                                        _prog.JanDia26 = itemG.Key.Status;
                                        break;
                                    case 27:
                                        _prog.JanDia27 = itemG.Key.Status;
                                        break;
                                    case 28:
                                        _prog.JanDia28 = itemG.Key.Status;
                                        break;
                                    case 29:
                                        _prog.JanDia29 = itemG.Key.Status;
                                        break;
                                    case 30:
                                        _prog.JanDia30 = itemG.Key.Status;
                                        break;
                                    case 31:
                                        _prog.JanDia31 = itemG.Key.Status;
                                        break;
                                }
                                break;
                            case 2:
                                switch (_data.Day)
                                {
                                    case 1:
                                        _prog.FevDia1 = itemG.Key.Status;
                                        break;
                                    case 2:
                                        _prog.FevDia2 = itemG.Key.Status;
                                        break;
                                    case 3:
                                        _prog.FevDia3 = itemG.Key.Status;
                                        break;
                                    case 4:
                                        _prog.FevDia4 = itemG.Key.Status;
                                        break;
                                    case 5:
                                        _prog.FevDia5 = itemG.Key.Status;
                                        break;
                                    case 6:
                                        _prog.FevDia6 = itemG.Key.Status;
                                        break;
                                    case 7:
                                        _prog.FevDia7 = itemG.Key.Status;
                                        break;
                                    case 8:
                                        _prog.FevDia8 = itemG.Key.Status;
                                        break;
                                    case 9:
                                        _prog.FevDia9 = itemG.Key.Status;
                                        break;
                                    case 10:
                                        _prog.FevDia10 = itemG.Key.Status;
                                        break;
                                    case 11:
                                        _prog.FevDia11 = itemG.Key.Status;
                                        break;
                                    case 12:
                                        _prog.FevDia12 = itemG.Key.Status;
                                        break;
                                    case 13:
                                        _prog.FevDia13 = itemG.Key.Status;
                                        break;
                                    case 14:
                                        _prog.FevDia14 = itemG.Key.Status;
                                        break;
                                    case 15:
                                        _prog.FevDia15 = itemG.Key.Status;
                                        break;
                                    case 16:
                                        _prog.FevDia16 = itemG.Key.Status;
                                        break;
                                    case 17:
                                        _prog.FevDia17 = itemG.Key.Status;
                                        break;
                                    case 18:
                                        _prog.FevDia18 = itemG.Key.Status;
                                        break;
                                    case 19:
                                        _prog.FevDia19 = itemG.Key.Status;
                                        break;
                                    case 20:
                                        _prog.FevDia20 = itemG.Key.Status;
                                        break;
                                    case 21:
                                        _prog.FevDia21 = itemG.Key.Status;
                                        break;
                                    case 22:
                                        _prog.FevDia22 = itemG.Key.Status;
                                        break;
                                    case 23:
                                        _prog.FevDia23 = itemG.Key.Status;
                                        break;
                                    case 24:
                                        _prog.FevDia24 = itemG.Key.Status;
                                        break;
                                    case 25:
                                        _prog.FevDia25 = itemG.Key.Status;
                                        break;
                                    case 26:
                                        _prog.FevDia26 = itemG.Key.Status;
                                        break;
                                    case 27:
                                        _prog.FevDia27 = itemG.Key.Status;
                                        break;
                                    case 28:
                                        _prog.FevDia28 = itemG.Key.Status;
                                        break;
                                    case 29:
                                        _prog.FevDia29 = itemG.Key.Status;
                                        break;
                                }
                                break;
                            case 3:
                                switch (_data.Day)
                                {
                                    case 1:
                                        _prog.MarDia1 = itemG.Key.Status;
                                        break;
                                    case 2:
                                        _prog.MarDia2 = itemG.Key.Status;
                                        break;
                                    case 3:
                                        _prog.MarDia3 = itemG.Key.Status;
                                        break;
                                    case 4:
                                        _prog.MarDia4 = itemG.Key.Status;
                                        break;
                                    case 5:
                                        _prog.MarDia5 = itemG.Key.Status;
                                        break;
                                    case 6:
                                        _prog.MarDia6 = itemG.Key.Status;
                                        break;
                                    case 7:
                                        _prog.MarDia7 = itemG.Key.Status;
                                        break;
                                    case 8:
                                        _prog.MarDia8 = itemG.Key.Status;
                                        break;
                                    case 9:
                                        _prog.MarDia9 = itemG.Key.Status;
                                        break;
                                    case 10:
                                        _prog.MarDia10 = itemG.Key.Status;
                                        break;
                                    case 11:
                                        _prog.MarDia11 = itemG.Key.Status;
                                        break;
                                    case 12:
                                        _prog.MarDia12 = itemG.Key.Status;
                                        break;
                                    case 13:
                                        _prog.MarDia13 = itemG.Key.Status;
                                        break;
                                    case 14:
                                        _prog.MarDia14 = itemG.Key.Status;
                                        break;
                                    case 15:
                                        _prog.MarDia15 = itemG.Key.Status;
                                        break;
                                    case 16:
                                        _prog.MarDia16 = itemG.Key.Status;
                                        break;
                                    case 17:
                                        _prog.MarDia17 = itemG.Key.Status;
                                        break;
                                    case 18:
                                        _prog.MarDia18 = itemG.Key.Status;
                                        break;
                                    case 19:
                                        _prog.MarDia19 = itemG.Key.Status;
                                        break;
                                    case 20:
                                        _prog.MarDia20 = itemG.Key.Status;
                                        break;
                                    case 21:
                                        _prog.MarDia21 = itemG.Key.Status;
                                        break;
                                    case 22:
                                        _prog.MarDia22 = itemG.Key.Status;
                                        break;
                                    case 23:
                                        _prog.MarDia23 = itemG.Key.Status;
                                        break;
                                    case 24:
                                        _prog.MarDia24 = itemG.Key.Status;
                                        break;
                                    case 25:
                                        _prog.MarDia25 = itemG.Key.Status;
                                        break;
                                    case 26:
                                        _prog.MarDia26 = itemG.Key.Status;
                                        break;
                                    case 27:
                                        _prog.MarDia27 = itemG.Key.Status;
                                        break;
                                    case 28:
                                        _prog.MarDia28 = itemG.Key.Status;
                                        break;
                                    case 29:
                                        _prog.MarDia29 = itemG.Key.Status;
                                        break;
                                    case 30:
                                        _prog.MarDia30 = itemG.Key.Status;
                                        break;
                                    case 31:
                                        _prog.MarDia31 = itemG.Key.Status;
                                        break;
                                }
                                break;
                            case 4:
                                switch (_data.Day)
                                {
                                    case 1:
                                        _prog.AbrDia1 = itemG.Key.Status;
                                        break;
                                    case 2:
                                        _prog.AbrDia2 = itemG.Key.Status;
                                        break;
                                    case 3:
                                        _prog.AbrDia3 = itemG.Key.Status;
                                        break;
                                    case 4:
                                        _prog.AbrDia4 = itemG.Key.Status;
                                        break;
                                    case 5:
                                        _prog.AbrDia5 = itemG.Key.Status;
                                        break;
                                    case 6:
                                        _prog.AbrDia6 = itemG.Key.Status;
                                        break;
                                    case 7:
                                        _prog.AbrDia7 = itemG.Key.Status;
                                        break;
                                    case 8:
                                        _prog.AbrDia8 = itemG.Key.Status;
                                        break;
                                    case 9:
                                        _prog.AbrDia9 = itemG.Key.Status;
                                        break;
                                    case 10:
                                        _prog.AbrDia10 = itemG.Key.Status;
                                        break;
                                    case 11:
                                        _prog.AbrDia11 = itemG.Key.Status;
                                        break;
                                    case 12:
                                        _prog.AbrDia12 = itemG.Key.Status;
                                        break;
                                    case 13:
                                        _prog.AbrDia13 = itemG.Key.Status;
                                        break;
                                    case 14:
                                        _prog.AbrDia14 = itemG.Key.Status;
                                        break;
                                    case 15:
                                        _prog.AbrDia15 = itemG.Key.Status;
                                        break;
                                    case 16:
                                        _prog.AbrDia16 = itemG.Key.Status;
                                        break;
                                    case 17:
                                        _prog.AbrDia17 = itemG.Key.Status;
                                        break;
                                    case 18:
                                        _prog.AbrDia18 = itemG.Key.Status;
                                        break;
                                    case 19:
                                        _prog.AbrDia19 = itemG.Key.Status;
                                        break;
                                    case 20:
                                        _prog.AbrDia20 = itemG.Key.Status;
                                        break;
                                    case 21:
                                        _prog.AbrDia21 = itemG.Key.Status;
                                        break;
                                    case 22:
                                        _prog.AbrDia22 = itemG.Key.Status;
                                        break;
                                    case 23:
                                        _prog.AbrDia23 = itemG.Key.Status;
                                        break;
                                    case 24:
                                        _prog.AbrDia24 = itemG.Key.Status;
                                        break;
                                    case 25:
                                        _prog.AbrDia25 = itemG.Key.Status;
                                        break;
                                    case 26:
                                        _prog.AbrDia26 = itemG.Key.Status;
                                        break;
                                    case 27:
                                        _prog.AbrDia27 = itemG.Key.Status;
                                        break;
                                    case 28:
                                        _prog.AbrDia28 = itemG.Key.Status;
                                        break;
                                    case 29:
                                        _prog.AbrDia29 = itemG.Key.Status;
                                        break;
                                    case 30:
                                        _prog.AbrDia30 = itemG.Key.Status;
                                        break;
                                }
                                break;
                            case 5:
                                switch (_data.Day)
                                {
                                    case 1:
                                        _prog.MaiDia1 = itemG.Key.Status;
                                        break;
                                    case 2:
                                        _prog.MaiDia2 = itemG.Key.Status;
                                        break;
                                    case 3:
                                        _prog.MaiDia3 = itemG.Key.Status;
                                        break;
                                    case 4:
                                        _prog.MaiDia4 = itemG.Key.Status;
                                        break;
                                    case 5:
                                        _prog.MaiDia5 = itemG.Key.Status;
                                        break;
                                    case 6:
                                        _prog.MaiDia6 = itemG.Key.Status;
                                        break;
                                    case 7:
                                        _prog.MaiDia7 = itemG.Key.Status;
                                        break;
                                    case 8:
                                        _prog.MaiDia8 = itemG.Key.Status;
                                        break;
                                    case 9:
                                        _prog.MaiDia9 = itemG.Key.Status;
                                        break;
                                    case 10:
                                        _prog.MaiDia10 = itemG.Key.Status;
                                        break;
                                    case 11:
                                        _prog.MaiDia11 = itemG.Key.Status;
                                        break;
                                    case 12:
                                        _prog.MaiDia12 = itemG.Key.Status;
                                        break;
                                    case 13:
                                        _prog.MaiDia13 = itemG.Key.Status;
                                        break;
                                    case 14:
                                        _prog.MaiDia14 = itemG.Key.Status;
                                        break;
                                    case 15:
                                        _prog.MaiDia15 = itemG.Key.Status;
                                        break;
                                    case 16:
                                        _prog.MaiDia16 = itemG.Key.Status;
                                        break;
                                    case 17:
                                        _prog.MaiDia17 = itemG.Key.Status;
                                        break;
                                    case 18:
                                        _prog.MaiDia18 = itemG.Key.Status;
                                        break;
                                    case 19:
                                        _prog.MaiDia19 = itemG.Key.Status;
                                        break;
                                    case 20:
                                        _prog.MaiDia20 = itemG.Key.Status;
                                        break;
                                    case 21:
                                        _prog.MaiDia21 = itemG.Key.Status;
                                        break;
                                    case 22:
                                        _prog.MaiDia22 = itemG.Key.Status;
                                        break;
                                    case 23:
                                        _prog.MaiDia23 = itemG.Key.Status;
                                        break;
                                    case 24:
                                        _prog.MaiDia24 = itemG.Key.Status;
                                        break;
                                    case 25:
                                        _prog.MaiDia25 = itemG.Key.Status;
                                        break;
                                    case 26:
                                        _prog.MaiDia26 = itemG.Key.Status;
                                        break;
                                    case 27:
                                        _prog.MaiDia27 = itemG.Key.Status;
                                        break;
                                    case 28:
                                        _prog.MaiDia28 = itemG.Key.Status;
                                        break;
                                    case 29:
                                        _prog.MaiDia29 = itemG.Key.Status;
                                        break;
                                    case 30:
                                        _prog.MaiDia30 = itemG.Key.Status;
                                        break;
                                    case 31:
                                        _prog.MaiDia31 = itemG.Key.Status;
                                        break;
                                }
                                break;
                            case 6:
                                switch (_data.Day)
                                {
                                    case 1:
                                        _prog.JunDia1 = itemG.Key.Status;
                                        break;
                                    case 2:
                                        _prog.JunDia2 = itemG.Key.Status;
                                        break;
                                    case 3:
                                        _prog.JunDia3 = itemG.Key.Status;
                                        break;
                                    case 4:
                                        _prog.JunDia4 = itemG.Key.Status;
                                        break;
                                    case 5:
                                        _prog.JunDia5 = itemG.Key.Status;
                                        break;
                                    case 6:
                                        _prog.JunDia6 = itemG.Key.Status;
                                        break;
                                    case 7:
                                        _prog.JunDia7 = itemG.Key.Status;
                                        break;
                                    case 8:
                                        _prog.JunDia8 = itemG.Key.Status;
                                        break;
                                    case 9:
                                        _prog.JunDia9 = itemG.Key.Status;
                                        break;
                                    case 10:
                                        _prog.JunDia10 = itemG.Key.Status;
                                        break;
                                    case 11:
                                        _prog.JunDia11 = itemG.Key.Status;
                                        break;
                                    case 12:
                                        _prog.JunDia12 = itemG.Key.Status;
                                        break;
                                    case 13:
                                        _prog.JunDia13 = itemG.Key.Status;
                                        break;
                                    case 14:
                                        _prog.JunDia14 = itemG.Key.Status;
                                        break;
                                    case 15:
                                        _prog.JunDia15 = itemG.Key.Status;
                                        break;
                                    case 16:
                                        _prog.JunDia16 = itemG.Key.Status;
                                        break;
                                    case 17:
                                        _prog.JunDia17 = itemG.Key.Status;
                                        break;
                                    case 18:
                                        _prog.JunDia18 = itemG.Key.Status;
                                        break;
                                    case 19:
                                        _prog.JunDia19 = itemG.Key.Status;
                                        break;
                                    case 20:
                                        _prog.JunDia20 = itemG.Key.Status;
                                        break;
                                    case 21:
                                        _prog.JunDia21 = itemG.Key.Status;
                                        break;
                                    case 22:
                                        _prog.JunDia22 = itemG.Key.Status;
                                        break;
                                    case 23:
                                        _prog.JunDia23 = itemG.Key.Status;
                                        break;
                                    case 24:
                                        _prog.JunDia24 = itemG.Key.Status;
                                        break;
                                    case 25:
                                        _prog.JunDia25 = itemG.Key.Status;
                                        break;
                                    case 26:
                                        _prog.JunDia26 = itemG.Key.Status;
                                        break;
                                    case 27:
                                        _prog.JunDia27 = itemG.Key.Status;
                                        break;
                                    case 28:
                                        _prog.JunDia28 = itemG.Key.Status;
                                        break;
                                    case 29:
                                        _prog.JunDia29 = itemG.Key.Status;
                                        break;
                                    case 30:
                                        _prog.JunDia30 = itemG.Key.Status;
                                        break;
                                }
                                break;
                            case 7:
                                switch (_data.Day)
                                {
                                    case 1:
                                        _prog.JulDia1 = itemG.Key.Status;
                                        break;
                                    case 2:
                                        _prog.JulDia2 = itemG.Key.Status;
                                        break;
                                    case 3:
                                        _prog.JulDia3 = itemG.Key.Status;
                                        break;
                                    case 4:
                                        _prog.JulDia4 = itemG.Key.Status;
                                        break;
                                    case 5:
                                        _prog.JulDia5 = itemG.Key.Status;
                                        break;
                                    case 6:
                                        _prog.JulDia6 = itemG.Key.Status;
                                        break;
                                    case 7:
                                        _prog.JulDia7 = itemG.Key.Status;
                                        break;
                                    case 8:
                                        _prog.JulDia8 = itemG.Key.Status;
                                        break;
                                    case 9:
                                        _prog.JulDia9 = itemG.Key.Status;
                                        break;
                                    case 10:
                                        _prog.JulDia10 = itemG.Key.Status;
                                        break;
                                    case 11:
                                        _prog.JulDia11 = itemG.Key.Status;
                                        break;
                                    case 12:
                                        _prog.JulDia12 = itemG.Key.Status;
                                        break;
                                    case 13:
                                        _prog.JulDia13 = itemG.Key.Status;
                                        break;
                                    case 14:
                                        _prog.JulDia14 = itemG.Key.Status;
                                        break;
                                    case 15:
                                        _prog.JulDia15 = itemG.Key.Status;
                                        break;
                                    case 16:
                                        _prog.JulDia16 = itemG.Key.Status;
                                        break;
                                    case 17:
                                        _prog.JulDia17 = itemG.Key.Status;
                                        break;
                                    case 18:
                                        _prog.JulDia18 = itemG.Key.Status;
                                        break;
                                    case 19:
                                        _prog.JulDia19 = itemG.Key.Status;
                                        break;
                                    case 20:
                                        _prog.JulDia20 = itemG.Key.Status;
                                        break;
                                    case 21:
                                        _prog.JulDia21 = itemG.Key.Status;
                                        break;
                                    case 22:
                                        _prog.JulDia22 = itemG.Key.Status;
                                        break;
                                    case 23:
                                        _prog.JulDia23 = itemG.Key.Status;
                                        break;
                                    case 24:
                                        _prog.JulDia24 = itemG.Key.Status;
                                        break;
                                    case 25:
                                        _prog.JulDia25 = itemG.Key.Status;
                                        break;
                                    case 26:
                                        _prog.JulDia26 = itemG.Key.Status;
                                        break;
                                    case 27:
                                        _prog.JulDia27 = itemG.Key.Status;
                                        break;
                                    case 28:
                                        _prog.JulDia28 = itemG.Key.Status;
                                        break;
                                    case 29:
                                        _prog.JulDia29 = itemG.Key.Status;
                                        break;
                                    case 30:
                                        _prog.JulDia30 = itemG.Key.Status;
                                        break;
                                    case 31:
                                        _prog.JulDia31 = itemG.Key.Status;
                                        break;
                                }
                                break;
                            case 8:
                                switch (_data.Day)
                                {
                                    case 1:
                                        _prog.AgoDia1 = itemG.Key.Status;
                                        break;
                                    case 2:
                                        _prog.AgoDia2 = itemG.Key.Status;
                                        break;
                                    case 3:
                                        _prog.AgoDia3 = itemG.Key.Status;
                                        break;
                                    case 4:
                                        _prog.AgoDia4 = itemG.Key.Status;
                                        break;
                                    case 5:
                                        _prog.AgoDia5 = itemG.Key.Status;
                                        break;
                                    case 6:
                                        _prog.AgoDia6 = itemG.Key.Status;
                                        break;
                                    case 7:
                                        _prog.AgoDia7 = itemG.Key.Status;
                                        break;
                                    case 8:
                                        _prog.AgoDia8 = itemG.Key.Status;
                                        break;
                                    case 9:
                                        _prog.AgoDia9 = itemG.Key.Status;
                                        break;
                                    case 10:
                                        _prog.AgoDia10 = itemG.Key.Status;
                                        break;
                                    case 11:
                                        _prog.AgoDia11 = itemG.Key.Status;
                                        break;
                                    case 12:
                                        _prog.AgoDia12 = itemG.Key.Status;
                                        break;
                                    case 13:
                                        _prog.AgoDia13 = itemG.Key.Status;
                                        break;
                                    case 14:
                                        _prog.AgoDia14 = itemG.Key.Status;
                                        break;
                                    case 15:
                                        _prog.AgoDia15 = itemG.Key.Status;
                                        break;
                                    case 16:
                                        _prog.AgoDia16 = itemG.Key.Status;
                                        break;
                                    case 17:
                                        _prog.AgoDia17 = itemG.Key.Status;
                                        break;
                                    case 18:
                                        _prog.AgoDia18 = itemG.Key.Status;
                                        break;
                                    case 19:
                                        _prog.AgoDia19 = itemG.Key.Status;
                                        break;
                                    case 20:
                                        _prog.AgoDia20 = itemG.Key.Status;
                                        break;
                                    case 21:
                                        _prog.AgoDia21 = itemG.Key.Status;
                                        break;
                                    case 22:
                                        _prog.AgoDia22 = itemG.Key.Status;
                                        break;
                                    case 23:
                                        _prog.AgoDia23 = itemG.Key.Status;
                                        break;
                                    case 24:
                                        _prog.AgoDia24 = itemG.Key.Status;
                                        break;
                                    case 25:
                                        _prog.AgoDia25 = itemG.Key.Status;
                                        break;
                                    case 26:
                                        _prog.AgoDia26 = itemG.Key.Status;
                                        break;
                                    case 27:
                                        _prog.AgoDia27 = itemG.Key.Status;
                                        break;
                                    case 28:
                                        _prog.AgoDia28 = itemG.Key.Status;
                                        break;
                                    case 29:
                                        _prog.AgoDia29 = itemG.Key.Status;
                                        break;
                                    case 30:
                                        _prog.AgoDia30 = itemG.Key.Status;
                                        break;
                                    case 31:
                                        _prog.AgoDia31 = itemG.Key.Status;
                                        break;
                                }
                                break;
                            case 9:
                                switch (_data.Day)
                                {
                                    case 1:
                                        _prog.SetDia1 = itemG.Key.Status;
                                        break;
                                    case 2:
                                        _prog.SetDia2 = itemG.Key.Status;
                                        break;
                                    case 3:
                                        _prog.SetDia3 = itemG.Key.Status;
                                        break;
                                    case 4:
                                        _prog.SetDia4 = itemG.Key.Status;
                                        break;
                                    case 5:
                                        _prog.SetDia5 = itemG.Key.Status;
                                        break;
                                    case 6:
                                        _prog.SetDia6 = itemG.Key.Status;
                                        break;
                                    case 7:
                                        _prog.SetDia7 = itemG.Key.Status;
                                        break;
                                    case 8:
                                        _prog.SetDia8 = itemG.Key.Status;
                                        break;
                                    case 9:
                                        _prog.SetDia9 = itemG.Key.Status;
                                        break;
                                    case 10:
                                        _prog.SetDia10 = itemG.Key.Status;
                                        break;
                                    case 11:
                                        _prog.SetDia11 = itemG.Key.Status;
                                        break;
                                    case 12:
                                        _prog.SetDia12 = itemG.Key.Status;
                                        break;
                                    case 13:
                                        _prog.SetDia13 = itemG.Key.Status;
                                        break;
                                    case 14:
                                        _prog.SetDia14 = itemG.Key.Status;
                                        break;
                                    case 15:
                                        _prog.SetDia15 = itemG.Key.Status;
                                        break;
                                    case 16:
                                        _prog.SetDia16 = itemG.Key.Status;
                                        break;
                                    case 17:
                                        _prog.SetDia17 = itemG.Key.Status;
                                        break;
                                    case 18:
                                        _prog.SetDia18 = itemG.Key.Status;
                                        break;
                                    case 19:
                                        _prog.SetDia19 = itemG.Key.Status;
                                        break;
                                    case 20:
                                        _prog.SetDia20 = itemG.Key.Status;
                                        break;
                                    case 21:
                                        _prog.SetDia21 = itemG.Key.Status;
                                        break;
                                    case 22:
                                        _prog.SetDia22 = itemG.Key.Status;
                                        break;
                                    case 23:
                                        _prog.SetDia23 = itemG.Key.Status;
                                        break;
                                    case 24:
                                        _prog.SetDia24 = itemG.Key.Status;
                                        break;
                                    case 25:
                                        _prog.SetDia25 = itemG.Key.Status;
                                        break;
                                    case 26:
                                        _prog.SetDia26 = itemG.Key.Status;
                                        break;
                                    case 27:
                                        _prog.SetDia27 = itemG.Key.Status;
                                        break;
                                    case 28:
                                        _prog.SetDia28 = itemG.Key.Status;
                                        break;
                                    case 29:
                                        _prog.SetDia29 = itemG.Key.Status;
                                        break;
                                    case 30:
                                        _prog.SetDia30 = itemG.Key.Status;
                                        break;

                                }
                                break;
                            case 10:
                                switch (_data.Day)
                                {
                                    case 1:
                                        _prog.OutDia1 = itemG.Key.Status;
                                        break;
                                    case 2:
                                        _prog.OutDia2 = itemG.Key.Status;
                                        break;
                                    case 3:
                                        _prog.OutDia3 = itemG.Key.Status;
                                        break;
                                    case 4:
                                        _prog.OutDia4 = itemG.Key.Status;
                                        break;
                                    case 5:
                                        _prog.OutDia5 = itemG.Key.Status;
                                        break;
                                    case 6:
                                        _prog.OutDia6 = itemG.Key.Status;
                                        break;
                                    case 7:
                                        _prog.OutDia7 = itemG.Key.Status;
                                        break;
                                    case 8:
                                        _prog.OutDia8 = itemG.Key.Status;
                                        break;
                                    case 9:
                                        _prog.OutDia9 = itemG.Key.Status;
                                        break;
                                    case 10:
                                        _prog.OutDia10 = itemG.Key.Status;
                                        break;
                                    case 11:
                                        _prog.OutDia11 = itemG.Key.Status;
                                        break;
                                    case 12:
                                        _prog.OutDia12 = itemG.Key.Status;
                                        break;
                                    case 13:
                                        _prog.OutDia13 = itemG.Key.Status;
                                        break;
                                    case 14:
                                        _prog.OutDia14 = itemG.Key.Status;
                                        break;
                                    case 15:
                                        _prog.OutDia15 = itemG.Key.Status;
                                        break;
                                    case 16:
                                        _prog.OutDia16 = itemG.Key.Status;
                                        break;
                                    case 17:
                                        _prog.OutDia17 = itemG.Key.Status;
                                        break;
                                    case 18:
                                        _prog.OutDia18 = itemG.Key.Status;
                                        break;
                                    case 19:
                                        _prog.OutDia19 = itemG.Key.Status;
                                        break;
                                    case 20:
                                        _prog.OutDia20 = itemG.Key.Status;
                                        break;
                                    case 21:
                                        _prog.OutDia21 = itemG.Key.Status;
                                        break;
                                    case 22:
                                        _prog.OutDia22 = itemG.Key.Status;
                                        break;
                                    case 23:
                                        _prog.OutDia23 = itemG.Key.Status;
                                        break;
                                    case 24:
                                        _prog.OutDia24 = itemG.Key.Status;
                                        break;
                                    case 25:
                                        _prog.OutDia25 = itemG.Key.Status;
                                        break;
                                    case 26:
                                        _prog.OutDia26 = itemG.Key.Status;
                                        break;
                                    case 27:
                                        _prog.OutDia27 = itemG.Key.Status;
                                        break;
                                    case 28:
                                        _prog.OutDia28 = itemG.Key.Status;
                                        break;
                                    case 29:
                                        _prog.OutDia29 = itemG.Key.Status;
                                        break;
                                    case 30:
                                        _prog.OutDia30 = itemG.Key.Status;
                                        break;
                                    case 31:
                                        _prog.OutDia31 = itemG.Key.Status;
                                        break;
                                }
                                break;
                            case 11:
                                switch (_data.Day)
                                {
                                    case 1:
                                        _prog.NovDia1 = itemG.Key.Status;
                                        break;
                                    case 2:
                                        _prog.NovDia2 = itemG.Key.Status;
                                        break;
                                    case 3:
                                        _prog.NovDia3 = itemG.Key.Status;
                                        break;
                                    case 4:
                                        _prog.NovDia4 = itemG.Key.Status;
                                        break;
                                    case 5:
                                        _prog.NovDia5 = itemG.Key.Status;
                                        break;
                                    case 6:
                                        _prog.NovDia6 = itemG.Key.Status;
                                        break;
                                    case 7:
                                        _prog.NovDia7 = itemG.Key.Status;
                                        break;
                                    case 8:
                                        _prog.NovDia8 = itemG.Key.Status;
                                        break;
                                    case 9:
                                        _prog.NovDia9 = itemG.Key.Status;
                                        break;
                                    case 10:
                                        _prog.NovDia10 = itemG.Key.Status;
                                        break;
                                    case 11:
                                        _prog.NovDia11 = itemG.Key.Status;
                                        break;
                                    case 12:
                                        _prog.NovDia12 = itemG.Key.Status;
                                        break;
                                    case 13:
                                        _prog.NovDia13 = itemG.Key.Status;
                                        break;
                                    case 14:
                                        _prog.NovDia14 = itemG.Key.Status;
                                        break;
                                    case 15:
                                        _prog.NovDia15 = itemG.Key.Status;
                                        break;
                                    case 16:
                                        _prog.NovDia16 = itemG.Key.Status;
                                        break;
                                    case 17:
                                        _prog.NovDia17 = itemG.Key.Status;
                                        break;
                                    case 18:
                                        _prog.NovDia18 = itemG.Key.Status;
                                        break;
                                    case 19:
                                        _prog.NovDia19 = itemG.Key.Status;
                                        break;
                                    case 20:
                                        _prog.NovDia20 = itemG.Key.Status;
                                        break;
                                    case 21:
                                        _prog.NovDia21 = itemG.Key.Status;
                                        break;
                                    case 22:
                                        _prog.NovDia22 = itemG.Key.Status;
                                        break;
                                    case 23:
                                        _prog.NovDia23 = itemG.Key.Status;
                                        break;
                                    case 24:
                                        _prog.NovDia24 = itemG.Key.Status;
                                        break;
                                    case 25:
                                        _prog.NovDia25 = itemG.Key.Status;
                                        break;
                                    case 26:
                                        _prog.NovDia26 = itemG.Key.Status;
                                        break;
                                    case 27:
                                        _prog.NovDia27 = itemG.Key.Status;
                                        break;
                                    case 28:
                                        _prog.NovDia28 = itemG.Key.Status;
                                        break;
                                    case 29:
                                        _prog.NovDia29 = itemG.Key.Status;
                                        break;
                                    case 30:
                                        _prog.NovDia30 = itemG.Key.Status;
                                        break;

                                }
                                break;
                            case 12:
                                switch (_data.Day)
                                {
                                    case 1:
                                        _prog.DezDia1 = itemG.Key.Status;
                                        break;
                                    case 2:
                                        _prog.DezDia2 = itemG.Key.Status;
                                        break;
                                    case 3:
                                        _prog.DezDia3 = itemG.Key.Status;
                                        break;
                                    case 4:
                                        _prog.DezDia4 = itemG.Key.Status;
                                        break;
                                    case 5:
                                        _prog.DezDia5 = itemG.Key.Status;
                                        break;
                                    case 6:
                                        _prog.DezDia6 = itemG.Key.Status;
                                        break;
                                    case 7:
                                        _prog.DezDia7 = itemG.Key.Status;
                                        break;
                                    case 8:
                                        _prog.DezDia8 = itemG.Key.Status;
                                        break;
                                    case 9:
                                        _prog.DezDia9 = itemG.Key.Status;
                                        break;
                                    case 10:
                                        _prog.DezDia10 = itemG.Key.Status;
                                        break;
                                    case 11:
                                        _prog.DezDia11 = itemG.Key.Status;
                                        break;
                                    case 12:
                                        _prog.DezDia12 = itemG.Key.Status;
                                        break;
                                    case 13:
                                        _prog.DezDia13 = itemG.Key.Status;
                                        break;
                                    case 14:
                                        _prog.DezDia14 = itemG.Key.Status;
                                        break;
                                    case 15:
                                        _prog.DezDia15 = itemG.Key.Status;
                                        break;
                                    case 16:
                                        _prog.DezDia16 = itemG.Key.Status;
                                        break;
                                    case 17:
                                        _prog.DezDia17 = itemG.Key.Status;
                                        break;
                                    case 18:
                                        _prog.DezDia18 = itemG.Key.Status;
                                        break;
                                    case 19:
                                        _prog.DezDia19 = itemG.Key.Status;
                                        break;
                                    case 20:
                                        _prog.DezDia20 = itemG.Key.Status;
                                        break;
                                    case 21:
                                        _prog.DezDia21 = itemG.Key.Status;
                                        break;
                                    case 22:
                                        _prog.DezDia22 = itemG.Key.Status;
                                        break;
                                    case 23:
                                        _prog.DezDia23 = itemG.Key.Status;
                                        break;
                                    case 24:
                                        _prog.DezDia24 = itemG.Key.Status;
                                        break;
                                    case 25:
                                        _prog.DezDia25 = itemG.Key.Status;
                                        break;
                                    case 26:
                                        _prog.DezDia26 = itemG.Key.Status;
                                        break;
                                    case 27:
                                        _prog.DezDia27 = itemG.Key.Status;
                                        break;
                                    case 28:
                                        _prog.DezDia28 = itemG.Key.Status;
                                        break;
                                    case 29:
                                        _prog.DezDia29 = itemG.Key.Status;
                                        break;
                                    case 30:
                                        _prog.DezDia30 = itemG.Key.Status;
                                        break;
                                    case 31:
                                        _prog.DezDia31 = itemG.Key.Status;
                                        break;
                                }
                                break;
                        }
                    }
                    _data = _data.AddDays(1);
                }

                if (!_encontrou)
                    _lista.Add(_prog);
            }

            #region Deixa colunas marcadas
            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                if (gridGroupingControl1.TableDescriptor.Columns[i].MappingName.Contains("Dia"))
                {
                    _encontrou = false;
                    switch (gridGroupingControl1.TableDescriptor.Columns[i].MappingName)
                    {
                        #region janeiro
                        case "JanDia1":
                            _encontrou = _lista.Where(w => w.JanDia1 != null).Count() != 0;
                            break;
                        case "JanDia2":
                            _encontrou = _lista.Where(w => w.JanDia2 != null).Count() != 0;
                            break;
                        case "JanDia3":
                            _encontrou = _lista.Where(w => w.JanDia3 != null).Count() != 0;
                            break;
                        case "JanDia4":
                            _encontrou = _lista.Where(w => w.JanDia4 != null).Count() != 0;
                            break;
                        case "JanDia5":
                            _encontrou = _lista.Where(w => w.JanDia5 != null).Count() != 0;
                            break;
                        case "JanDia6":
                            _encontrou = _lista.Where(w => w.JanDia6 != null).Count() != 0;
                            break;
                        case "JanDia7":
                            _encontrou = _lista.Where(w => w.JanDia7 != null).Count() != 0;
                            break;
                        case "JanDia8":
                            _encontrou = _lista.Where(w => w.JanDia8 != null).Count() != 0;
                            break;
                        case "JanDia9":
                            _encontrou = _lista.Where(w => w.JanDia9 != null).Count() != 0;
                            break;
                        case "JanDia10":
                            _encontrou = _lista.Where(w => w.JanDia10 != null).Count() != 0;
                            break;
                        case "JanDia11":
                            _encontrou = _lista.Where(w => w.JanDia11 != null).Count() != 0;
                            break;
                        case "JanDia12":
                            _encontrou = _lista.Where(w => w.JanDia12 != null).Count() != 0;
                            break;
                        case "JanDia13":
                            _encontrou = _lista.Where(w => w.JanDia13 != null).Count() != 0;
                            break;
                        case "JanDia14":
                            _encontrou = _lista.Where(w => w.JanDia14 != null).Count() != 0;
                            break;
                        case "JanDia15":
                            _encontrou = _lista.Where(w => w.JanDia15 != null).Count() != 0;
                            break;
                        case "JanDia16":
                            _encontrou = _lista.Where(w => w.JanDia16 != null).Count() != 0;
                            break;
                        case "JanDia17":
                            _encontrou = _lista.Where(w => w.JanDia17 != null).Count() != 0;
                            break;
                        case "JanDia18":
                            _encontrou = _lista.Where(w => w.JanDia18 != null).Count() != 0;
                            break;
                        case "JanDia19":
                            _encontrou = _lista.Where(w => w.JanDia19 != null).Count() != 0;
                            break;
                        case "JanDia20":
                            _encontrou = _lista.Where(w => w.JanDia20 != null).Count() != 0;
                            break;
                        case "JanDia21":
                            _encontrou = _lista.Where(w => w.JanDia21 != null).Count() != 0;
                            break;
                        case "JanDia22":
                            _encontrou = _lista.Where(w => w.JanDia22 != null).Count() != 0;
                            break;
                        case "JanDia23":
                            _encontrou = _lista.Where(w => w.JanDia23 != null).Count() != 0;
                            break;
                        case "JanDia24":
                            _encontrou = _lista.Where(w => w.JanDia24 != null).Count() != 0;
                            break;
                        case "JanDia25":
                            _encontrou = _lista.Where(w => w.JanDia25 != null).Count() != 0;
                            break;
                        case "JanDia26":
                            _encontrou = _lista.Where(w => w.JanDia26 != null).Count() != 0;
                            break;
                        case "JanDia27":
                            _encontrou = _lista.Where(w => w.JanDia27 != null).Count() != 0;
                            break;
                        case "JanDia28":
                            _encontrou = _lista.Where(w => w.JanDia28 != null).Count() != 0;
                            break;
                        case "JanDia29":
                            _encontrou = _lista.Where(w => w.JanDia29 != null).Count() != 0;
                            break;
                        case "JanDia30":
                            _encontrou = _lista.Where(w => w.JanDia30 != null).Count() != 0;
                            break;
                        case "JanDia31":
                            _encontrou = _lista.Where(w => w.JanDia31 != null).Count() != 0;
                            break;
                        #endregion

                        #region Feveiro
                        case "FevDia1":
                            _encontrou = _lista.Where(w => w.FevDia1 != null).Count() != 0;
                            break;
                        case "FevDia2":
                            _encontrou = _lista.Where(w => w.FevDia2 != null).Count() != 0;
                            break;
                        case "FevDia3":
                            _encontrou = _lista.Where(w => w.FevDia3 != null).Count() != 0;
                            break;
                        case "FevDia4":
                            _encontrou = _lista.Where(w => w.FevDia4 != null).Count() != 0;
                            break;
                        case "FevDia5":
                            _encontrou = _lista.Where(w => w.FevDia5 != null).Count() != 0;
                            break;
                        case "FevDia6":
                            _encontrou = _lista.Where(w => w.FevDia6 != null).Count() != 0;
                            break;
                        case "FevDia7":
                            _encontrou = _lista.Where(w => w.FevDia7 != null).Count() != 0;
                            break;
                        case "FevDia8":
                            _encontrou = _lista.Where(w => w.FevDia8 != null).Count() != 0;
                            break;
                        case "FevDia9":
                            _encontrou = _lista.Where(w => w.FevDia9 != null).Count() != 0;
                            break;
                        case "FevDia10":
                            _encontrou = _lista.Where(w => w.FevDia10 != null).Count() != 0;
                            break;
                        case "FevDia11":
                            _encontrou = _lista.Where(w => w.FevDia11 != null).Count() != 0;
                            break;
                        case "FevDia12":
                            _encontrou = _lista.Where(w => w.FevDia12 != null).Count() != 0;
                            break;
                        case "FevDia13":
                            _encontrou = _lista.Where(w => w.FevDia13 != null).Count() != 0;
                            break;
                        case "FevDia14":
                            _encontrou = _lista.Where(w => w.FevDia14 != null).Count() != 0;
                            break;
                        case "FevDia15":
                            _encontrou = _lista.Where(w => w.FevDia15 != null).Count() != 0;
                            break;
                        case "FevDia16":
                            _encontrou = _lista.Where(w => w.FevDia16 != null).Count() != 0;
                            break;
                        case "FevDia17":
                            _encontrou = _lista.Where(w => w.FevDia17 != null).Count() != 0;
                            break;
                        case "FevDia18":
                            _encontrou = _lista.Where(w => w.FevDia18 != null).Count() != 0;
                            break;
                        case "FevDia19":
                            _encontrou = _lista.Where(w => w.FevDia19 != null).Count() != 0;
                            break;
                        case "FevDia20":
                            _encontrou = _lista.Where(w => w.FevDia20 != null).Count() != 0;
                            break;
                        case "FevDia21":
                            _encontrou = _lista.Where(w => w.FevDia21 != null).Count() != 0;
                            break;
                        case "FevDia22":
                            _encontrou = _lista.Where(w => w.FevDia22 != null).Count() != 0;
                            break;
                        case "FevDia23":
                            _encontrou = _lista.Where(w => w.FevDia23 != null).Count() != 0;
                            break;
                        case "FevDia24":
                            _encontrou = _lista.Where(w => w.FevDia24 != null).Count() != 0;
                            break;
                        case "FevDia25":
                            _encontrou = _lista.Where(w => w.FevDia25 != null).Count() != 0;
                            break;
                        case "FevDia26":
                            _encontrou = _lista.Where(w => w.FevDia26 != null).Count() != 0;
                            break;
                        case "FevDia27":
                            _encontrou = _lista.Where(w => w.FevDia27 != null).Count() != 0;
                            break;
                        case "FevDia28":
                            _encontrou = _lista.Where(w => w.FevDia28 != null).Count() != 0;
                            break;
                        case "FevDia29":
                            _encontrou = _lista.Where(w => w.FevDia29 != null).Count() != 0;
                            break;
                        #endregion

                        #region Março
                        case "MarDia1":
                            _encontrou = _lista.Where(w => w.MarDia1 != null).Count() != 0;
                            break;
                        case "MarDia2":
                            _encontrou = _lista.Where(w => w.MarDia2 != null).Count() != 0;
                            break;
                        case "MarDia3":
                            _encontrou = _lista.Where(w => w.MarDia3 != null).Count() != 0;
                            break;
                        case "MarDia4":
                            _encontrou = _lista.Where(w => w.MarDia4 != null).Count() != 0;
                            break;
                        case "MarDia5":
                            _encontrou = _lista.Where(w => w.MarDia5 != null).Count() != 0;
                            break;
                        case "MarDia6":
                            _encontrou = _lista.Where(w => w.MarDia6 != null).Count() != 0;
                            break;
                        case "MarDia7":
                            _encontrou = _lista.Where(w => w.MarDia7 != null).Count() != 0;
                            break;
                        case "MarDia8":
                            _encontrou = _lista.Where(w => w.MarDia8 != null).Count() != 0;
                            break;
                        case "MarDia9":
                            _encontrou = _lista.Where(w => w.MarDia9 != null).Count() != 0;
                            break;
                        case "MarDia10":
                            _encontrou = _lista.Where(w => w.MarDia10 != null).Count() != 0;
                            break;
                        case "MarDia11":
                            _encontrou = _lista.Where(w => w.MarDia11 != null).Count() != 0;
                            break;
                        case "MarDia12":
                            _encontrou = _lista.Where(w => w.MarDia12 != null).Count() != 0;
                            break;
                        case "MarDia13":
                            _encontrou = _lista.Where(w => w.MarDia13 != null).Count() != 0;
                            break;
                        case "MarDia14":
                            _encontrou = _lista.Where(w => w.MarDia14 != null).Count() != 0;
                            break;
                        case "MarDia15":
                            _encontrou = _lista.Where(w => w.MarDia15 != null).Count() != 0;
                            break;
                        case "MarDia16":
                            _encontrou = _lista.Where(w => w.MarDia16 != null).Count() != 0;
                            break;
                        case "MarDia17":
                            _encontrou = _lista.Where(w => w.MarDia17 != null).Count() != 0;
                            break;
                        case "MarDia18":
                            _encontrou = _lista.Where(w => w.MarDia18 != null).Count() != 0;
                            break;
                        case "MarDia19":
                            _encontrou = _lista.Where(w => w.MarDia19 != null).Count() != 0;
                            break;
                        case "MarDia20":
                            _encontrou = _lista.Where(w => w.MarDia20 != null).Count() != 0;
                            break;
                        case "MarDia21":
                            _encontrou = _lista.Where(w => w.MarDia21 != null).Count() != 0;
                            break;
                        case "MarDia22":
                            _encontrou = _lista.Where(w => w.MarDia22 != null).Count() != 0;
                            break;
                        case "MarDia23":
                            _encontrou = _lista.Where(w => w.MarDia23 != null).Count() != 0;
                            break;
                        case "MarDia24":
                            _encontrou = _lista.Where(w => w.MarDia24 != null).Count() != 0;
                            break;
                        case "MarDia25":
                            _encontrou = _lista.Where(w => w.MarDia25 != null).Count() != 0;
                            break;
                        case "MarDia26":
                            _encontrou = _lista.Where(w => w.MarDia26 != null).Count() != 0;
                            break;
                        case "MarDia27":
                            _encontrou = _lista.Where(w => w.MarDia27 != null).Count() != 0;
                            break;
                        case "MarDia28":
                            _encontrou = _lista.Where(w => w.MarDia28 != null).Count() != 0;
                            break;
                        case "MarDia29":
                            _encontrou = _lista.Where(w => w.MarDia29 != null).Count() != 0;
                            break;
                        case "MarDia30":
                            _encontrou = _lista.Where(w => w.MarDia30 != null).Count() != 0;
                            break;
                        case "MarDia31":
                            _encontrou = _lista.Where(w => w.MarDia31 != null).Count() != 0;
                            break;
                        #endregion

                        #region Abril
                        case "AbrDia1":
                            _encontrou = _lista.Where(w => w.AbrDia1 != null).Count() != 0;
                            break;
                        case "AbrDia2":
                            _encontrou = _lista.Where(w => w.AbrDia2 != null).Count() != 0;
                            break;
                        case "AbrDia3":
                            _encontrou = _lista.Where(w => w.AbrDia3 != null).Count() != 0;
                            break;
                        case "AbrDia4":
                            _encontrou = _lista.Where(w => w.AbrDia4 != null).Count() != 0;
                            break;
                        case "AbrDia5":
                            _encontrou = _lista.Where(w => w.AbrDia5 != null).Count() != 0;
                            break;
                        case "AbrDia6":
                            _encontrou = _lista.Where(w => w.AbrDia6 != null).Count() != 0;
                            break;
                        case "AbrDia7":
                            _encontrou = _lista.Where(w => w.AbrDia7 != null).Count() != 0;
                            break;
                        case "AbrDia8":
                            _encontrou = _lista.Where(w => w.AbrDia8 != null).Count() != 0;
                            break;
                        case "AbrDia9":
                            _encontrou = _lista.Where(w => w.AbrDia9 != null).Count() != 0;
                            break;
                        case "AbrDia10":
                            _encontrou = _lista.Where(w => w.AbrDia10 != null).Count() != 0;
                            break;
                        case "AbrDia11":
                            _encontrou = _lista.Where(w => w.AbrDia11 != null).Count() != 0;
                            break;
                        case "AbrDia12":
                            _encontrou = _lista.Where(w => w.AbrDia12 != null).Count() != 0;
                            break;
                        case "AbrDia13":
                            _encontrou = _lista.Where(w => w.AbrDia13 != null).Count() != 0;
                            break;
                        case "AbrDia14":
                            _encontrou = _lista.Where(w => w.AbrDia14 != null).Count() != 0;
                            break;
                        case "AbrDia15":
                            _encontrou = _lista.Where(w => w.AbrDia15 != null).Count() != 0;
                            break;
                        case "AbrDia16":
                            _encontrou = _lista.Where(w => w.AbrDia16 != null).Count() != 0;
                            break;
                        case "AbrDia17":
                            _encontrou = _lista.Where(w => w.AbrDia17 != null).Count() != 0;
                            break;
                        case "AbrDia18":
                            _encontrou = _lista.Where(w => w.AbrDia18 != null).Count() != 0;
                            break;
                        case "AbrDia19":
                            _encontrou = _lista.Where(w => w.AbrDia19 != null).Count() != 0;
                            break;
                        case "AbrDia20":
                            _encontrou = _lista.Where(w => w.AbrDia20 != null).Count() != 0;
                            break;
                        case "AbrDia21":
                            _encontrou = _lista.Where(w => w.AbrDia21 != null).Count() != 0;
                            break;
                        case "AbrDia22":
                            _encontrou = _lista.Where(w => w.AbrDia22 != null).Count() != 0;
                            break;
                        case "AbrDia23":
                            _encontrou = _lista.Where(w => w.AbrDia23 != null).Count() != 0;
                            break;
                        case "AbrDia24":
                            _encontrou = _lista.Where(w => w.AbrDia24 != null).Count() != 0;
                            break;
                        case "AbrDia25":
                            _encontrou = _lista.Where(w => w.AbrDia25 != null).Count() != 0;
                            break;
                        case "AbrDia26":
                            _encontrou = _lista.Where(w => w.AbrDia26 != null).Count() != 0;
                            break;
                        case "AbrDia27":
                            _encontrou = _lista.Where(w => w.AbrDia27 != null).Count() != 0;
                            break;
                        case "AbrDia28":
                            _encontrou = _lista.Where(w => w.AbrDia28 != null).Count() != 0;
                            break;
                        case "AbrDia29":
                            _encontrou = _lista.Where(w => w.AbrDia29 != null).Count() != 0;
                            break;
                        case "AbrDia30":
                            _encontrou = _lista.Where(w => w.AbrDia30 != null).Count() != 0;
                            break;
                        #endregion

                        #region maio
                        case "MaiDia1":
                            _encontrou = _lista.Where(w => w.MaiDia1 != null).Count() != 0;
                            break;
                        case "MaiDia2":
                            _encontrou = _lista.Where(w => w.MaiDia2 != null).Count() != 0;
                            break;
                        case "MaiDia3":
                            _encontrou = _lista.Where(w => w.MaiDia3 != null).Count() != 0;
                            break;
                        case "MaiDia4":
                            _encontrou = _lista.Where(w => w.MaiDia4 != null).Count() != 0;
                            break;
                        case "MaiDia5":
                            _encontrou = _lista.Where(w => w.MaiDia5 != null).Count() != 0;
                            break;
                        case "MaiDia6":
                            _encontrou = _lista.Where(w => w.MaiDia6 != null).Count() != 0;
                            break;
                        case "MaiDia7":
                            _encontrou = _lista.Where(w => w.MaiDia7 != null).Count() != 0;
                            break;
                        case "MaiDia8":
                            _encontrou = _lista.Where(w => w.MaiDia8 != null).Count() != 0;
                            break;
                        case "MaiDia9":
                            _encontrou = _lista.Where(w => w.MaiDia9 != null).Count() != 0;
                            break;
                        case "MaiDia10":
                            _encontrou = _lista.Where(w => w.MaiDia10 != null).Count() != 0;
                            break;
                        case "MaiDia11":
                            _encontrou = _lista.Where(w => w.MaiDia11 != null).Count() != 0;
                            break;
                        case "MaiDia12":
                            _encontrou = _lista.Where(w => w.MaiDia12 != null).Count() != 0;
                            break;
                        case "MaiDia13":
                            _encontrou = _lista.Where(w => w.MaiDia13 != null).Count() != 0;
                            break;
                        case "MaiDia14":
                            _encontrou = _lista.Where(w => w.MaiDia14 != null).Count() != 0;
                            break;
                        case "MaiDia15":
                            _encontrou = _lista.Where(w => w.MaiDia15 != null).Count() != 0;
                            break;
                        case "MaiDia16":
                            _encontrou = _lista.Where(w => w.MaiDia16 != null).Count() != 0;
                            break;
                        case "MaiDia17":
                            _encontrou = _lista.Where(w => w.MaiDia17 != null).Count() != 0;
                            break;
                        case "MaiDia18":
                            _encontrou = _lista.Where(w => w.MaiDia18 != null).Count() != 0;
                            break;
                        case "MaiDia19":
                            _encontrou = _lista.Where(w => w.MaiDia19 != null).Count() != 0;
                            break;
                        case "MaiDia20":
                            _encontrou = _lista.Where(w => w.MaiDia20 != null).Count() != 0;
                            break;
                        case "MaiDia21":
                            _encontrou = _lista.Where(w => w.MaiDia21 != null).Count() != 0;
                            break;
                        case "MaiDia22":
                            _encontrou = _lista.Where(w => w.MaiDia22 != null).Count() != 0;
                            break;
                        case "MaiDia23":
                            _encontrou = _lista.Where(w => w.MaiDia23 != null).Count() != 0;
                            break;
                        case "MaiDia24":
                            _encontrou = _lista.Where(w => w.MaiDia24 != null).Count() != 0;
                            break;
                        case "MaiDia25":
                            _encontrou = _lista.Where(w => w.MaiDia25 != null).Count() != 0;
                            break;
                        case "MaiDia26":
                            _encontrou = _lista.Where(w => w.MaiDia26 != null).Count() != 0;
                            break;
                        case "MaiDia27":
                            _encontrou = _lista.Where(w => w.MaiDia27 != null).Count() != 0;
                            break;
                        case "MaiDia28":
                            _encontrou = _lista.Where(w => w.MaiDia28 != null).Count() != 0;
                            break;
                        case "MaiDia29":
                            _encontrou = _lista.Where(w => w.MaiDia29 != null).Count() != 0;
                            break;
                        case "MaiDia30":
                            _encontrou = _lista.Where(w => w.MaiDia30 != null).Count() != 0;
                            break;
                        case "MaiDia31":
                            _encontrou = _lista.Where(w => w.MaiDia31 != null).Count() != 0;
                            break;
                        #endregion

                        #region Junho
                        case "JunDia1":
                            _encontrou = _lista.Where(w => w.JunDia1 != null).Count() != 0;
                            break;
                        case "JunDia2":
                            _encontrou = _lista.Where(w => w.JunDia2 != null).Count() != 0;
                            break;
                        case "JunDia3":
                            _encontrou = _lista.Where(w => w.JunDia3 != null).Count() != 0;
                            break;
                        case "JunDia4":
                            _encontrou = _lista.Where(w => w.JunDia4 != null).Count() != 0;
                            break;
                        case "JunDia5":
                            _encontrou = _lista.Where(w => w.JunDia5 != null).Count() != 0;
                            break;
                        case "JunDia6":
                            _encontrou = _lista.Where(w => w.JunDia6 != null).Count() != 0;
                            break;
                        case "JunDia7":
                            _encontrou = _lista.Where(w => w.JunDia7 != null).Count() != 0;
                            break;
                        case "JunDia8":
                            _encontrou = _lista.Where(w => w.JunDia8 != null).Count() != 0;
                            break;
                        case "JunDia9":
                            _encontrou = _lista.Where(w => w.JunDia9 != null).Count() != 0;
                            break;
                        case "JunDia10":
                            _encontrou = _lista.Where(w => w.JunDia10 != null).Count() != 0;
                            break;
                        case "JunDia11":
                            _encontrou = _lista.Where(w => w.JunDia11 != null).Count() != 0;
                            break;
                        case "JunDia12":
                            _encontrou = _lista.Where(w => w.JunDia12 != null).Count() != 0;
                            break;
                        case "JunDia13":
                            _encontrou = _lista.Where(w => w.JunDia13 != null).Count() != 0;
                            break;
                        case "JunDia14":
                            _encontrou = _lista.Where(w => w.JunDia14 != null).Count() != 0;
                            break;
                        case "JunDia15":
                            _encontrou = _lista.Where(w => w.JunDia15 != null).Count() != 0;
                            break;
                        case "JunDia16":
                            _encontrou = _lista.Where(w => w.JunDia16 != null).Count() != 0;
                            break;
                        case "JunDia17":
                            _encontrou = _lista.Where(w => w.JunDia17 != null).Count() != 0;
                            break;
                        case "JunDia18":
                            _encontrou = _lista.Where(w => w.JunDia18 != null).Count() != 0;
                            break;
                        case "JunDia19":
                            _encontrou = _lista.Where(w => w.JunDia19 != null).Count() != 0;
                            break;
                        case "JunDia20":
                            _encontrou = _lista.Where(w => w.JunDia20 != null).Count() != 0;
                            break;
                        case "JunDia21":
                            _encontrou = _lista.Where(w => w.JunDia21 != null).Count() != 0;
                            break;
                        case "JunDia22":
                            _encontrou = _lista.Where(w => w.JunDia22 != null).Count() != 0;
                            break;
                        case "JunDia23":
                            _encontrou = _lista.Where(w => w.JunDia23 != null).Count() != 0;
                            break;
                        case "JunDia24":
                            _encontrou = _lista.Where(w => w.JunDia24 != null).Count() != 0;
                            break;
                        case "JunDia25":
                            _encontrou = _lista.Where(w => w.JunDia25 != null).Count() != 0;
                            break;
                        case "JunDia26":
                            _encontrou = _lista.Where(w => w.JunDia26 != null).Count() != 0;
                            break;
                        case "JunDia27":
                            _encontrou = _lista.Where(w => w.JunDia27 != null).Count() != 0;
                            break;
                        case "JunDia28":
                            _encontrou = _lista.Where(w => w.JunDia28 != null).Count() != 0;
                            break;
                        case "JunDia29":
                            _encontrou = _lista.Where(w => w.JunDia29 != null).Count() != 0;
                            break;
                        case "JunDia30":
                            _encontrou = _lista.Where(w => w.JunDia30 != null).Count() != 0;
                            break;
                        #endregion

                        #region Julho
                        case "JulDia1":
                            _encontrou = _lista.Where(w => w.JulDia1 != null).Count() != 0;
                            break;
                        case "JulDia2":
                            _encontrou = _lista.Where(w => w.JulDia2 != null).Count() != 0;
                            break;
                        case "JulDia3":
                            _encontrou = _lista.Where(w => w.JulDia3 != null).Count() != 0;
                            break;
                        case "JulDia4":
                            _encontrou = _lista.Where(w => w.JulDia4 != null).Count() != 0;
                            break;
                        case "JulDia5":
                            _encontrou = _lista.Where(w => w.JulDia5 != null).Count() != 0;
                            break;
                        case "JulDia6":
                            _encontrou = _lista.Where(w => w.JulDia6 != null).Count() != 0;
                            break;
                        case "JulDia7":
                            _encontrou = _lista.Where(w => w.JulDia7 != null).Count() != 0;
                            break;
                        case "JulDia8":
                            _encontrou = _lista.Where(w => w.JulDia8 != null).Count() != 0;
                            break;
                        case "JulDia9":
                            _encontrou = _lista.Where(w => w.JulDia9 != null).Count() != 0;
                            break;
                        case "JulDia10":
                            _encontrou = _lista.Where(w => w.JulDia10 != null).Count() != 0;
                            break;
                        case "JulDia11":
                            _encontrou = _lista.Where(w => w.JulDia11 != null).Count() != 0;
                            break;
                        case "JulDia12":
                            _encontrou = _lista.Where(w => w.JulDia12 != null).Count() != 0;
                            break;
                        case "JulDia13":
                            _encontrou = _lista.Where(w => w.JulDia13 != null).Count() != 0;
                            break;
                        case "JulDia14":
                            _encontrou = _lista.Where(w => w.JulDia14 != null).Count() != 0;
                            break;
                        case "JulDia15":
                            _encontrou = _lista.Where(w => w.JulDia15 != null).Count() != 0;
                            break;
                        case "JulDia16":
                            _encontrou = _lista.Where(w => w.JulDia16 != null).Count() != 0;
                            break;
                        case "JulDia17":
                            _encontrou = _lista.Where(w => w.JulDia17 != null).Count() != 0;
                            break;
                        case "JulDia18":
                            _encontrou = _lista.Where(w => w.JulDia18 != null).Count() != 0;
                            break;
                        case "JulDia19":
                            _encontrou = _lista.Where(w => w.JulDia19 != null).Count() != 0;
                            break;
                        case "JulDia20":
                            _encontrou = _lista.Where(w => w.JulDia20 != null).Count() != 0;
                            break;
                        case "JulDia21":
                            _encontrou = _lista.Where(w => w.JulDia21 != null).Count() != 0;
                            break;
                        case "JulDia22":
                            _encontrou = _lista.Where(w => w.JulDia22 != null).Count() != 0;
                            break;
                        case "JulDia23":
                            _encontrou = _lista.Where(w => w.JulDia23 != null).Count() != 0;
                            break;
                        case "JulDia24":
                            _encontrou = _lista.Where(w => w.JulDia24 != null).Count() != 0;
                            break;
                        case "JulDia25":
                            _encontrou = _lista.Where(w => w.JulDia25 != null).Count() != 0;
                            break;
                        case "JulDia26":
                            _encontrou = _lista.Where(w => w.JulDia26 != null).Count() != 0;
                            break;
                        case "JulDia27":
                            _encontrou = _lista.Where(w => w.JulDia27 != null).Count() != 0;
                            break;
                        case "JulDia28":
                            _encontrou = _lista.Where(w => w.JulDia28 != null).Count() != 0;
                            break;
                        case "JulDia29":
                            _encontrou = _lista.Where(w => w.JulDia29 != null).Count() != 0;
                            break;
                        case "JulDia30":
                            _encontrou = _lista.Where(w => w.JulDia30 != null).Count() != 0;
                            break;
                        case "JulDia31":
                            _encontrou = _lista.Where(w => w.JulDia31 != null).Count() != 0;
                            break;
                        #endregion

                        #region Agosto
                        case "AgoDia1":
                            _encontrou = _lista.Where(w => w.AgoDia1 != null).Count() != 0;
                            break;
                        case "AgoDia2":
                            _encontrou = _lista.Where(w => w.AgoDia2 != null).Count() != 0;
                            break;
                        case "AgoDia3":
                            _encontrou = _lista.Where(w => w.AgoDia3 != null).Count() != 0;
                            break;
                        case "AgoDia4":
                            _encontrou = _lista.Where(w => w.AgoDia4 != null).Count() != 0;
                            break;
                        case "AgoDia5":
                            _encontrou = _lista.Where(w => w.AgoDia5 != null).Count() != 0;
                            break;
                        case "AgoDia6":
                            _encontrou = _lista.Where(w => w.AgoDia6 != null).Count() != 0;
                            break;
                        case "AgoDia7":
                            _encontrou = _lista.Where(w => w.AgoDia7 != null).Count() != 0;
                            break;
                        case "AgoDia8":
                            _encontrou = _lista.Where(w => w.AgoDia8 != null).Count() != 0;
                            break;
                        case "AgoDia9":
                            _encontrou = _lista.Where(w => w.AgoDia9 != null).Count() != 0;
                            break;
                        case "AgoDia10":
                            _encontrou = _lista.Where(w => w.AgoDia10 != null).Count() != 0;
                            break;
                        case "AgoDia11":
                            _encontrou = _lista.Where(w => w.AgoDia11 != null).Count() != 0;
                            break;
                        case "AgoDia12":
                            _encontrou = _lista.Where(w => w.AgoDia12 != null).Count() != 0;
                            break;
                        case "AgoDia13":
                            _encontrou = _lista.Where(w => w.AgoDia13 != null).Count() != 0;
                            break;
                        case "AgoDia14":
                            _encontrou = _lista.Where(w => w.AgoDia14 != null).Count() != 0;
                            break;
                        case "AgoDia15":
                            _encontrou = _lista.Where(w => w.AgoDia15 != null).Count() != 0;
                            break;
                        case "AgoDia16":
                            _encontrou = _lista.Where(w => w.AgoDia16 != null).Count() != 0;
                            break;
                        case "AgoDia17":
                            _encontrou = _lista.Where(w => w.AgoDia17 != null).Count() != 0;
                            break;
                        case "AgoDia18":
                            _encontrou = _lista.Where(w => w.AgoDia18 != null).Count() != 0;
                            break;
                        case "AgoDia19":
                            _encontrou = _lista.Where(w => w.AgoDia19 != null).Count() != 0;
                            break;
                        case "AgoDia20":
                            _encontrou = _lista.Where(w => w.AgoDia20 != null).Count() != 0;
                            break;
                        case "AgoDia21":
                            _encontrou = _lista.Where(w => w.AgoDia21 != null).Count() != 0;
                            break;
                        case "AgoDia22":
                            _encontrou = _lista.Where(w => w.AgoDia22 != null).Count() != 0;
                            break;
                        case "AgoDia23":
                            _encontrou = _lista.Where(w => w.AgoDia23 != null).Count() != 0;
                            break;
                        case "AgoDia24":
                            _encontrou = _lista.Where(w => w.AgoDia24 != null).Count() != 0;
                            break;
                        case "AgoDia25":
                            _encontrou = _lista.Where(w => w.AgoDia25 != null).Count() != 0;
                            break;
                        case "AgoDia26":
                            _encontrou = _lista.Where(w => w.AgoDia26 != null).Count() != 0;
                            break;
                        case "AgoDia27":
                            _encontrou = _lista.Where(w => w.AgoDia27 != null).Count() != 0;
                            break;
                        case "AgoDia28":
                            _encontrou = _lista.Where(w => w.AgoDia28 != null).Count() != 0;
                            break;
                        case "AgoDia29":
                            _encontrou = _lista.Where(w => w.AgoDia29 != null).Count() != 0;
                            break;
                        case "AgoDia30":
                            _encontrou = _lista.Where(w => w.AgoDia30 != null).Count() != 0;
                            break;
                        case "AgoDia31":
                            _encontrou = _lista.Where(w => w.AgoDia31 != null).Count() != 0;
                            break;
                        #endregion

                        #region Setembro
                        case "SetDia1":
                            _encontrou = _lista.Where(w => w.SetDia1 != null).Count() != 0;
                            break;
                        case "SetDia2":
                            _encontrou = _lista.Where(w => w.SetDia2 != null).Count() != 0;
                            break;
                        case "SetDia3":
                            _encontrou = _lista.Where(w => w.SetDia3 != null).Count() != 0;
                            break;
                        case "SetDia4":
                            _encontrou = _lista.Where(w => w.SetDia4 != null).Count() != 0;
                            break;
                        case "SetDia5":
                            _encontrou = _lista.Where(w => w.SetDia5 != null).Count() != 0;
                            break;
                        case "SetDia6":
                            _encontrou = _lista.Where(w => w.SetDia6 != null).Count() != 0;
                            break;
                        case "SetDia7":
                            _encontrou = _lista.Where(w => w.SetDia7 != null).Count() != 0;
                            break;
                        case "SetDia8":
                            _encontrou = _lista.Where(w => w.SetDia8 != null).Count() != 0;
                            break;
                        case "SetDia9":
                            _encontrou = _lista.Where(w => w.SetDia9 != null).Count() != 0;
                            break;
                        case "SetDia10":
                            _encontrou = _lista.Where(w => w.SetDia10 != null).Count() != 0;
                            break;
                        case "SetDia11":
                            _encontrou = _lista.Where(w => w.SetDia11 != null).Count() != 0;
                            break;
                        case "SetDia12":
                            _encontrou = _lista.Where(w => w.SetDia12 != null).Count() != 0;
                            break;
                        case "SetDia13":
                            _encontrou = _lista.Where(w => w.SetDia13 != null).Count() != 0;
                            break;
                        case "SetDia14":
                            _encontrou = _lista.Where(w => w.SetDia14 != null).Count() != 0;
                            break;
                        case "SetDia15":
                            _encontrou = _lista.Where(w => w.SetDia15 != null).Count() != 0;
                            break;
                        case "SetDia16":
                            _encontrou = _lista.Where(w => w.SetDia16 != null).Count() != 0;
                            break;
                        case "SetDia17":
                            _encontrou = _lista.Where(w => w.SetDia17 != null).Count() != 0;
                            break;
                        case "SetDia18":
                            _encontrou = _lista.Where(w => w.SetDia18 != null).Count() != 0;
                            break;
                        case "SetDia19":
                            _encontrou = _lista.Where(w => w.SetDia19 != null).Count() != 0;
                            break;
                        case "SetDia20":
                            _encontrou = _lista.Where(w => w.SetDia20 != null).Count() != 0;
                            break;
                        case "SetDia21":
                            _encontrou = _lista.Where(w => w.SetDia21 != null).Count() != 0;
                            break;
                        case "SetDia22":
                            _encontrou = _lista.Where(w => w.SetDia22 != null).Count() != 0;
                            break;
                        case "SetDia23":
                            _encontrou = _lista.Where(w => w.SetDia23 != null).Count() != 0;
                            break;
                        case "SetDia24":
                            _encontrou = _lista.Where(w => w.SetDia24 != null).Count() != 0;
                            break;
                        case "SetDia25":
                            _encontrou = _lista.Where(w => w.SetDia25 != null).Count() != 0;
                            break;
                        case "SetDia26":
                            _encontrou = _lista.Where(w => w.SetDia26 != null).Count() != 0;
                            break;
                        case "SetDia27":
                            _encontrou = _lista.Where(w => w.SetDia27 != null).Count() != 0;
                            break;
                        case "SetDia28":
                            _encontrou = _lista.Where(w => w.SetDia28 != null).Count() != 0;
                            break;
                        case "SetDia29":
                            _encontrou = _lista.Where(w => w.SetDia29 != null).Count() != 0;
                            break;
                        case "SetDia30":
                            _encontrou = _lista.Where(w => w.SetDia30 != null).Count() != 0;
                            break;
                        #endregion

                        #region Outubro
                        case "OutDia1":
                            _encontrou = _lista.Where(w => w.OutDia1 != null).Count() != 0;
                            break;
                        case "OutDia2":
                            _encontrou = _lista.Where(w => w.OutDia2 != null).Count() != 0;
                            break;
                        case "OutDia3":
                            _encontrou = _lista.Where(w => w.OutDia3 != null).Count() != 0;
                            break;
                        case "OutDia4":
                            _encontrou = _lista.Where(w => w.OutDia4 != null).Count() != 0;
                            break;
                        case "OutDia5":
                            _encontrou = _lista.Where(w => w.OutDia5 != null).Count() != 0;
                            break;
                        case "OutDia6":
                            _encontrou = _lista.Where(w => w.OutDia6 != null).Count() != 0;
                            break;
                        case "OutDia7":
                            _encontrou = _lista.Where(w => w.OutDia7 != null).Count() != 0;
                            break;
                        case "OutDia8":
                            _encontrou = _lista.Where(w => w.OutDia8 != null).Count() != 0;
                            break;
                        case "OutDia9":
                            _encontrou = _lista.Where(w => w.OutDia9 != null).Count() != 0;
                            break;
                        case "OutDia10":
                            _encontrou = _lista.Where(w => w.OutDia10 != null).Count() != 0;
                            break;
                        case "OutDia11":
                            _encontrou = _lista.Where(w => w.OutDia11 != null).Count() != 0;
                            break;
                        case "OutDia12":
                            _encontrou = _lista.Where(w => w.OutDia12 != null).Count() != 0;
                            break;
                        case "OutDia13":
                            _encontrou = _lista.Where(w => w.OutDia13 != null).Count() != 0;
                            break;
                        case "OutDia14":
                            _encontrou = _lista.Where(w => w.OutDia14 != null).Count() != 0;
                            break;
                        case "OutDia15":
                            _encontrou = _lista.Where(w => w.OutDia15 != null).Count() != 0;
                            break;
                        case "OutDia16":
                            _encontrou = _lista.Where(w => w.OutDia16 != null).Count() != 0;
                            break;
                        case "OutDia17":
                            _encontrou = _lista.Where(w => w.OutDia17 != null).Count() != 0;
                            break;
                        case "OutDia18":
                            _encontrou = _lista.Where(w => w.OutDia18 != null).Count() != 0;
                            break;
                        case "OutDia19":
                            _encontrou = _lista.Where(w => w.OutDia19 != null).Count() != 0;
                            break;
                        case "OutDia20":
                            _encontrou = _lista.Where(w => w.OutDia20 != null).Count() != 0;
                            break;
                        case "OutDia21":
                            _encontrou = _lista.Where(w => w.OutDia21 != null).Count() != 0;
                            break;
                        case "OutDia22":
                            _encontrou = _lista.Where(w => w.OutDia22 != null).Count() != 0;
                            break;
                        case "OutDia23":
                            _encontrou = _lista.Where(w => w.OutDia23 != null).Count() != 0;
                            break;
                        case "OutDia24":
                            _encontrou = _lista.Where(w => w.OutDia24 != null).Count() != 0;
                            break;
                        case "OutDia25":
                            _encontrou = _lista.Where(w => w.OutDia25 != null).Count() != 0;
                            break;
                        case "OutDia26":
                            _encontrou = _lista.Where(w => w.OutDia26 != null).Count() != 0;
                            break;
                        case "OutDia27":
                            _encontrou = _lista.Where(w => w.OutDia27 != null).Count() != 0;
                            break;
                        case "OutDia28":
                            _encontrou = _lista.Where(w => w.OutDia28 != null).Count() != 0;
                            break;
                        case "OutDia29":
                            _encontrou = _lista.Where(w => w.OutDia29 != null).Count() != 0;
                            break;
                        case "OutDia30":
                            _encontrou = _lista.Where(w => w.OutDia30 != null).Count() != 0;
                            break;
                        case "OutDia31":
                            _encontrou = _lista.Where(w => w.OutDia31 != null).Count() != 0;
                            break;
                        #endregion

                        #region Novembro
                        case "NovDia1":
                            _encontrou = _lista.Where(w => w.NovDia1 != null).Count() != 0;
                            break;
                        case "NovDia2":
                            _encontrou = _lista.Where(w => w.NovDia2 != null).Count() != 0;
                            break;
                        case "NovDia3":
                            _encontrou = _lista.Where(w => w.NovDia3 != null).Count() != 0;
                            break;
                        case "NovDia4":
                            _encontrou = _lista.Where(w => w.NovDia4 != null).Count() != 0;
                            break;
                        case "NovDia5":
                            _encontrou = _lista.Where(w => w.NovDia5 != null).Count() != 0;
                            break;
                        case "NovDia6":
                            _encontrou = _lista.Where(w => w.NovDia6 != null).Count() != 0;
                            break;
                        case "NovDia7":
                            _encontrou = _lista.Where(w => w.NovDia7 != null).Count() != 0;
                            break;
                        case "NovDia8":
                            _encontrou = _lista.Where(w => w.NovDia8 != null).Count() != 0;
                            break;
                        case "NovDia9":
                            _encontrou = _lista.Where(w => w.NovDia9 != null).Count() != 0;
                            break;
                        case "NovDia10":
                            _encontrou = _lista.Where(w => w.NovDia10 != null).Count() != 0;
                            break;
                        case "NovDia11":
                            _encontrou = _lista.Where(w => w.NovDia11 != null).Count() != 0;
                            break;
                        case "NovDia12":
                            _encontrou = _lista.Where(w => w.NovDia12 != null).Count() != 0;
                            break;
                        case "NovDia13":
                            _encontrou = _lista.Where(w => w.NovDia13 != null).Count() != 0;
                            break;
                        case "NovDia14":
                            _encontrou = _lista.Where(w => w.NovDia14 != null).Count() != 0;
                            break;
                        case "NovDia15":
                            _encontrou = _lista.Where(w => w.NovDia15 != null).Count() != 0;
                            break;
                        case "NovDia16":
                            _encontrou = _lista.Where(w => w.NovDia16 != null).Count() != 0;
                            break;
                        case "NovDia17":
                            _encontrou = _lista.Where(w => w.NovDia17 != null).Count() != 0;
                            break;
                        case "NovDia18":
                            _encontrou = _lista.Where(w => w.NovDia18 != null).Count() != 0;
                            break;
                        case "NovDia19":
                            _encontrou = _lista.Where(w => w.NovDia19 != null).Count() != 0;
                            break;
                        case "NovDia20":
                            _encontrou = _lista.Where(w => w.NovDia20 != null).Count() != 0;
                            break;
                        case "NovDia21":
                            _encontrou = _lista.Where(w => w.NovDia21 != null).Count() != 0;
                            break;
                        case "NovDia22":
                            _encontrou = _lista.Where(w => w.NovDia22 != null).Count() != 0;
                            break;
                        case "NovDia23":
                            _encontrou = _lista.Where(w => w.NovDia23 != null).Count() != 0;
                            break;
                        case "NovDia24":
                            _encontrou = _lista.Where(w => w.NovDia24 != null).Count() != 0;
                            break;
                        case "NovDia25":
                            _encontrou = _lista.Where(w => w.NovDia25 != null).Count() != 0;
                            break;
                        case "NovDia26":
                            _encontrou = _lista.Where(w => w.NovDia26 != null).Count() != 0;
                            break;
                        case "NovDia27":
                            _encontrou = _lista.Where(w => w.NovDia27 != null).Count() != 0;
                            break;
                        case "NovDia28":
                            _encontrou = _lista.Where(w => w.NovDia28 != null).Count() != 0;
                            break;
                        case "NovDia29":
                            _encontrou = _lista.Where(w => w.NovDia29 != null).Count() != 0;
                            break;
                        case "NovDia30":
                            _encontrou = _lista.Where(w => w.NovDia30 != null).Count() != 0;
                            break;
                        #endregion

                        #region Dezembro
                        case "DezDia1":
                            _encontrou = _lista.Where(w => w.DezDia1 != null).Count() != 0;
                            break;
                        case "DezDia2":
                            _encontrou = _lista.Where(w => w.DezDia2 != null).Count() != 0;
                            break;
                        case "DezDia3":
                            _encontrou = _lista.Where(w => w.DezDia3 != null).Count() != 0;
                            break;
                        case "DezDia4":
                            _encontrou = _lista.Where(w => w.DezDia4 != null).Count() != 0;
                            break;
                        case "DezDia5":
                            _encontrou = _lista.Where(w => w.DezDia5 != null).Count() != 0;
                            break;
                        case "DezDia6":
                            _encontrou = _lista.Where(w => w.DezDia6 != null).Count() != 0;
                            break;
                        case "DezDia7":
                            _encontrou = _lista.Where(w => w.DezDia7 != null).Count() != 0;
                            break;
                        case "DezDia8":
                            _encontrou = _lista.Where(w => w.DezDia8 != null).Count() != 0;
                            break;
                        case "DezDia9":
                            _encontrou = _lista.Where(w => w.DezDia9 != null).Count() != 0;
                            break;
                        case "DezDia10":
                            _encontrou = _lista.Where(w => w.DezDia10 != null).Count() != 0;
                            break;
                        case "DezDia11":
                            _encontrou = _lista.Where(w => w.DezDia11 != null).Count() != 0;
                            break;
                        case "DezDia12":
                            _encontrou = _lista.Where(w => w.DezDia12 != null).Count() != 0;
                            break;
                        case "DezDia13":
                            _encontrou = _lista.Where(w => w.DezDia13 != null).Count() != 0;
                            break;
                        case "DezDia14":
                            _encontrou = _lista.Where(w => w.DezDia14 != null).Count() != 0;
                            break;
                        case "DezDia15":
                            _encontrou = _lista.Where(w => w.DezDia15 != null).Count() != 0;
                            break;
                        case "DezDia16":
                            _encontrou = _lista.Where(w => w.DezDia16 != null).Count() != 0;
                            break;
                        case "DezDia17":
                            _encontrou = _lista.Where(w => w.DezDia17 != null).Count() != 0;
                            break;
                        case "DezDia18":
                            _encontrou = _lista.Where(w => w.DezDia18 != null).Count() != 0;
                            break;
                        case "DezDia19":
                            _encontrou = _lista.Where(w => w.DezDia19 != null).Count() != 0;
                            break;
                        case "DezDia20":
                            _encontrou = _lista.Where(w => w.DezDia20 != null).Count() != 0;
                            break;
                        case "DezDia21":
                            _encontrou = _lista.Where(w => w.DezDia21 != null).Count() != 0;
                            break;
                        case "DezDia22":
                            _encontrou = _lista.Where(w => w.DezDia22 != null).Count() != 0;
                            break;
                        case "DezDia23":
                            _encontrou = _lista.Where(w => w.DezDia23 != null).Count() != 0;
                            break;
                        case "DezDia24":
                            _encontrou = _lista.Where(w => w.DezDia24 != null).Count() != 0;
                            break;
                        case "DezDia25":
                            _encontrou = _lista.Where(w => w.DezDia25 != null).Count() != 0;
                            break;
                        case "DezDia26":
                            _encontrou = _lista.Where(w => w.DezDia26 != null).Count() != 0;
                            break;
                        case "DezDia27":
                            _encontrou = _lista.Where(w => w.DezDia27 != null).Count() != 0;
                            break;
                        case "DezDia28":
                            _encontrou = _lista.Where(w => w.DezDia28 != null).Count() != 0;
                            break;
                        case "DezDia29":
                            _encontrou = _lista.Where(w => w.DezDia29 != null).Count() != 0;
                            break;
                        case "DezDia30":
                            _encontrou = _lista.Where(w => w.DezDia30 != null).Count() != 0;
                            break;
                        case "DezDia31":
                            _encontrou = _lista.Where(w => w.DezDia31 != null).Count() != 0;
                            break;
                            #endregion
                    }

                    if (!_encontrou)
                        gridGroupingControl1.TableDescriptor.VisibleColumns.Remove(gridGroupingControl1.TableDescriptor.Columns[i].MappingName);
                }
            }
            #endregion

            gridGroupingControl1.DataSource = _lista;

            IntegrarButton.Enabled = _listaProgramacao.Where(w => w.Status == "A" &&
                                                         w.IniPeriodoAquisitivo != DateTime.MinValue &&
                                                         w.DataInicio < DateTime.Now.AddMonths(2)).Count() != 0;

            mensagemSistemaLabel.Text = "";
            this.Cursor = Cursors.Default;
            this.Refresh();
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridGroupingControl1_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            if (e.Style.TableCellIdentity.DisplayElement.Kind != Syncfusion.Grouping.DisplayElementKind.Record)
                return;

            try
            {
                Record dr;
                GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;
                dr = rec.GetRecord() as Record;

                if (e.TableCellIdentity.Column.MappingName.Contains("Dia"))
                {
                    switch (e.Style.Text)
                    {
                        case "A":
                            e.Style.BackColor = Color.Green;
                            e.Style.CellTipText = "Aprovado";
                            e.Style.Text = "";
                            break;
                        case "G":
                            e.Style.BackColor = Color.Khaki;
                            e.Style.CellTipText = "Aguardando Aprovação";
                            e.Style.Text = "";
                            break;
                        case "R":
                            e.Style.BackColor = Color.Red;
                            e.Style.CellTipText = "Reprovado" + Environment.NewLine + (string)dr["MotivoReprovacao"];
                            e.Style.Text = "";
                            break;
                        case "Z":
                            e.Style.BackColor = Color.Teal;
                            e.Style.CellTipText = "Gozadas";
                            e.Style.Text = "";
                            break;
                        case "I":
                            e.Style.BackColor = Color.Blue;
                            e.Style.CellTipText = "Integradas para o Globus";
                            e.Style.Text = "";
                            break;
                    }
                   
                }
                else
                {
                    if (Publicas._usuario.PodeIntegrarProgramacaoFerias)
                    {
                        try
                        {

                            if (rec != null)
                            {
                                dr = rec.GetRecord() as Record;

                                if (dr != null && (int)dr["IdDepartamento"] == Publicas._usuario.IdDepartamento)
                                    e.Style.TextColor = Color.DarkOrange;

                            }
                        }
                        catch { }

                    }
                }
            }
            catch { }
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            referenciaMaskedEditBox.Text = string.Empty;
            mensagemSistemaLabel.Focus();

            gridGroupingControl1.DataSource = new List<Programacao>();

            leColunasGridCabecalho = new System.Xml.XmlTextReader("GridCabecalhoProgramacaoFerias.xml");
            gridGroupingControl1.ApplyXmlSchema(leColunasGridCabecalho);

            leCorGridCabecalho = new System.Xml.XmlTextReader("CorGridCabecalhoProgramacaoFerias.xml");
            gridGroupingControl1.ApplyXmlLookAndFeel(leCorGridCabecalho);

            leColunasGridCabecalho.Close();
            leCorGridCabecalho.Close();
        }

        private void GozadasCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (MostrarGozadasCheckBox.Enabled)
                    MostrarGozadasCheckBox.Focus();
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

        private void aprovarReprovarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                mensagemSistemaLabel.Text = "Abrindo tela, Aguarde ...";
                this.Cursor = Cursors.WaitCursor;
                this.Refresh();

                Classes.Usuario _usuario = new Classes.Usuario();
                Classes.ProgramacaoFerias _prog = new ProgramacaoFerias();
                DateTime _data = DateTime.MinValue;

                GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

                GridTableCellStyleInfoIdentity style = this.gridGroupingControl1.TableModel[_colunaCorrente.RowIndex, _colunaCorrente.ColIndex].TableCellIdentity;

                string nomeColuna = "";

                if (style.TableCellType == GridTableCellType.RecordFieldCell || style.TableCellType == GridTableCellType.AlternateRecordFieldCell)
                    nomeColuna = style.Column.Name;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;

                    if (dr != null)
                    {                        
                        _usuario = new UsuarioBO().ConsultaUsuarioPorCodigoFuncionarioGlobus((int)dr["CodIntFunc"]);

                        if (_usuario.CodigoInternoFuncionarioGlobus == Publicas._usuario.CodigoInternoFuncionarioGlobus)
                        {
                            mensagemSistemaLabel.Text = "";
                            this.Cursor = Cursors.Default;
                            this.Refresh();
                            new Notificacoes.Mensagem("Você não pode aprovar/reprovar sua Programação de Férias."
                                + Environment.NewLine
                                + "Solicite ao seu gestor."
                                , Publicas.TipoMensagem.Alerta).ShowDialog();
                            return;
                        }

                        switch (nomeColuna)
                        {
                            #region janeiro
                            case "JanDia1":
                                _data = Convert.ToDateTime("01/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia2":
                                _data = Convert.ToDateTime("02/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia3":
                                _data = Convert.ToDateTime("03/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia4":
                                _data = Convert.ToDateTime("04/01/" + referenciaMaskedEditBox.Text);;
                                break;
                            case "JanDia5":
                                _data = Convert.ToDateTime("05/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia6":
                                _data = Convert.ToDateTime("06/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia7":
                                _data = Convert.ToDateTime("07/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia8":
                                _data = Convert.ToDateTime("08/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia9":
                                _data = Convert.ToDateTime("09/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia10":
                                _data = Convert.ToDateTime("10/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia11":
                                _data = Convert.ToDateTime("11/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia12":
                                _data = Convert.ToDateTime("12/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia13":
                                _data = Convert.ToDateTime("13/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia14":
                                _data = Convert.ToDateTime("14/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia15":
                                _data = Convert.ToDateTime("15/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia16":
                                _data = Convert.ToDateTime("16/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia17":
                                _data = Convert.ToDateTime("17/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia18":
                                _data = Convert.ToDateTime("18/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia19":
                                _data = Convert.ToDateTime("19/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia20":
                                _data = Convert.ToDateTime("20/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia21":
                                _data = Convert.ToDateTime("21/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia22":
                                _data = Convert.ToDateTime("22/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia23":
                                _data = Convert.ToDateTime("23/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia24":
                                _data = Convert.ToDateTime("24/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia25":
                                _data = Convert.ToDateTime("25/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia26":
                                _data = Convert.ToDateTime("26/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia27":
                                _data = Convert.ToDateTime("27/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia28":
                                _data = Convert.ToDateTime("28/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia29":
                                _data = Convert.ToDateTime("29/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia30":
                                _data = Convert.ToDateTime("30/01/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JanDia31":
                                _data = Convert.ToDateTime("31/01/" + referenciaMaskedEditBox.Text);
                                break;
                            #endregion

                            #region Feveiro
                            case "FevDia1":
                                _data = Convert.ToDateTime("01/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia2":
                                _data = Convert.ToDateTime("02/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia3":
                                _data = Convert.ToDateTime("03/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia4":
                                _data = Convert.ToDateTime("04/02/" + referenciaMaskedEditBox.Text); ;
                                break;
                            case "FevDia5":
                                _data = Convert.ToDateTime("05/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia6":
                                _data = Convert.ToDateTime("06/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia7":
                                _data = Convert.ToDateTime("07/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia8":
                                _data = Convert.ToDateTime("08/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia9":
                                _data = Convert.ToDateTime("09/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia10":
                                _data = Convert.ToDateTime("10/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia11":
                                _data = Convert.ToDateTime("11/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia12":
                                _data = Convert.ToDateTime("12/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia13":
                                _data = Convert.ToDateTime("13/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia14":
                                _data = Convert.ToDateTime("14/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia15":
                                _data = Convert.ToDateTime("15/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia16":
                                _data = Convert.ToDateTime("16/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia17":
                                _data = Convert.ToDateTime("17/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia18":
                                _data = Convert.ToDateTime("18/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia19":
                                _data = Convert.ToDateTime("19/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia20":
                                _data = Convert.ToDateTime("20/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia21":
                                _data = Convert.ToDateTime("21/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia22":
                                _data = Convert.ToDateTime("22/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia23":
                                _data = Convert.ToDateTime("23/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia24":
                                _data = Convert.ToDateTime("24/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia25":
                                _data = Convert.ToDateTime("25/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia26":
                                _data = Convert.ToDateTime("26/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia27":
                                _data = Convert.ToDateTime("27/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia28":
                                _data = Convert.ToDateTime("28/02/" + referenciaMaskedEditBox.Text);
                                break;
                            case "FevDia29":
                                _data = Convert.ToDateTime("29/02/" + referenciaMaskedEditBox.Text);
                                break;
                            #endregion

                            #region Março
                            case "MarDia1":
                                _data = Convert.ToDateTime("01/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia2":
                                _data = Convert.ToDateTime("02/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia3":
                                _data = Convert.ToDateTime("03/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia4":
                                _data = Convert.ToDateTime("04/03/" + referenciaMaskedEditBox.Text); ;
                                break;
                            case "MarDia5":
                                _data = Convert.ToDateTime("05/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia6":
                                _data = Convert.ToDateTime("06/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia7":
                                _data = Convert.ToDateTime("07/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia8":
                                _data = Convert.ToDateTime("08/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia9":
                                _data = Convert.ToDateTime("09/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia10":
                                _data = Convert.ToDateTime("10/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia11":
                                _data = Convert.ToDateTime("11/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia12":
                                _data = Convert.ToDateTime("12/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia13":
                                _data = Convert.ToDateTime("13/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia14":
                                _data = Convert.ToDateTime("14/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia15":
                                _data = Convert.ToDateTime("15/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia16":
                                _data = Convert.ToDateTime("16/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia17":
                                _data = Convert.ToDateTime("17/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia18":
                                _data = Convert.ToDateTime("18/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia19":
                                _data = Convert.ToDateTime("19/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia20":
                                _data = Convert.ToDateTime("20/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia21":
                                _data = Convert.ToDateTime("21/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia22":
                                _data = Convert.ToDateTime("22/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia23":
                                _data = Convert.ToDateTime("23/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia24":
                                _data = Convert.ToDateTime("24/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia25":
                                _data = Convert.ToDateTime("25/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia26":
                                _data = Convert.ToDateTime("26/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia27":
                                _data = Convert.ToDateTime("27/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia28":
                                _data = Convert.ToDateTime("28/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia29":
                                _data = Convert.ToDateTime("29/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia30":
                                _data = Convert.ToDateTime("30/03/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MarDia31":
                                _data = Convert.ToDateTime("31/03/" + referenciaMaskedEditBox.Text);
                                break;
                            #endregion

                            #region Abril
                            case "AbrDia1":
                                _data = Convert.ToDateTime("01/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia2":
                                _data = Convert.ToDateTime("02/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia3":
                                _data = Convert.ToDateTime("03/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia4":
                                _data = Convert.ToDateTime("04/04/" + referenciaMaskedEditBox.Text); ;
                                break;
                            case "AbrDia5":
                                _data = Convert.ToDateTime("05/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia6":
                                _data = Convert.ToDateTime("06/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia7":
                                _data = Convert.ToDateTime("07/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia8":
                                _data = Convert.ToDateTime("08/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia9":
                                _data = Convert.ToDateTime("09/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia10":
                                _data = Convert.ToDateTime("10/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia11":
                                _data = Convert.ToDateTime("11/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia12":
                                _data = Convert.ToDateTime("12/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia13":
                                _data = Convert.ToDateTime("13/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia14":
                                _data = Convert.ToDateTime("14/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia15":
                                _data = Convert.ToDateTime("15/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia16":
                                _data = Convert.ToDateTime("16/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia17":
                                _data = Convert.ToDateTime("17/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia18":
                                _data = Convert.ToDateTime("18/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia19":
                                _data = Convert.ToDateTime("19/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia20":
                                _data = Convert.ToDateTime("20/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia21":
                                _data = Convert.ToDateTime("21/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia22":
                                _data = Convert.ToDateTime("22/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia23":
                                _data = Convert.ToDateTime("23/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia24":
                                _data = Convert.ToDateTime("24/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia25":
                                _data = Convert.ToDateTime("25/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia26":
                                _data = Convert.ToDateTime("26/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia27":
                                _data = Convert.ToDateTime("27/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia28":
                                _data = Convert.ToDateTime("28/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia29":
                                _data = Convert.ToDateTime("29/04/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AbrDia30":
                                _data = Convert.ToDateTime("30/04/" + referenciaMaskedEditBox.Text);
                                break;
                            #endregion

                            #region maio
                            case "MaiDia1":
                                _data = Convert.ToDateTime("01/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia2":
                                _data = Convert.ToDateTime("02/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia3":
                                _data = Convert.ToDateTime("03/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia4":
                                _data = Convert.ToDateTime("04/05/" + referenciaMaskedEditBox.Text); ;
                                break;
                            case "MaiDia5":
                                _data = Convert.ToDateTime("05/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia6":
                                _data = Convert.ToDateTime("06/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia7":
                                _data = Convert.ToDateTime("07/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia8":
                                _data = Convert.ToDateTime("08/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia9":
                                _data = Convert.ToDateTime("09/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia10":
                                _data = Convert.ToDateTime("10/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia11":
                                _data = Convert.ToDateTime("11/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia12":
                                _data = Convert.ToDateTime("12/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia13":
                                _data = Convert.ToDateTime("13/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia14":
                                _data = Convert.ToDateTime("14/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia15":
                                _data = Convert.ToDateTime("15/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia16":
                                _data = Convert.ToDateTime("16/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia17":
                                _data = Convert.ToDateTime("17/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia18":
                                _data = Convert.ToDateTime("18/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia19":
                                _data = Convert.ToDateTime("19/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia20":
                                _data = Convert.ToDateTime("20/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia21":
                                _data = Convert.ToDateTime("21/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia22":
                                _data = Convert.ToDateTime("22/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia23":
                                _data = Convert.ToDateTime("23/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia24":
                                _data = Convert.ToDateTime("24/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia25":
                                _data = Convert.ToDateTime("25/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia26":
                                _data = Convert.ToDateTime("26/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia27":
                                _data = Convert.ToDateTime("27/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia28":
                                _data = Convert.ToDateTime("28/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia29":
                                _data = Convert.ToDateTime("29/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia30":
                                _data = Convert.ToDateTime("30/05/" + referenciaMaskedEditBox.Text);
                                break;
                            case "MaiDia31":
                                _data = Convert.ToDateTime("31/05/" + referenciaMaskedEditBox.Text);
                                break;
                            #endregion

                            #region Junho
                            case "JunDia1":
                                _data = Convert.ToDateTime("01/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia2":
                                _data = Convert.ToDateTime("02/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia3":
                                _data = Convert.ToDateTime("03/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia4":
                                _data = Convert.ToDateTime("04/06/" + referenciaMaskedEditBox.Text); ;
                                break;
                            case "JunDia5":
                                _data = Convert.ToDateTime("05/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia6":
                                _data = Convert.ToDateTime("06/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia7":
                                _data = Convert.ToDateTime("07/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia8":
                                _data = Convert.ToDateTime("08/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia9":
                                _data = Convert.ToDateTime("09/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia10":
                                _data = Convert.ToDateTime("10/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia11":
                                _data = Convert.ToDateTime("11/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia12":
                                _data = Convert.ToDateTime("12/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia13":
                                _data = Convert.ToDateTime("13/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia14":
                                _data = Convert.ToDateTime("14/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia15":
                                _data = Convert.ToDateTime("15/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia16":
                                _data = Convert.ToDateTime("16/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia17":
                                _data = Convert.ToDateTime("17/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia18":
                                _data = Convert.ToDateTime("18/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia19":
                                _data = Convert.ToDateTime("19/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia20":
                                _data = Convert.ToDateTime("20/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia21":
                                _data = Convert.ToDateTime("21/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia22":
                                _data = Convert.ToDateTime("22/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia23":
                                _data = Convert.ToDateTime("23/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia24":
                                _data = Convert.ToDateTime("24/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia25":
                                _data = Convert.ToDateTime("25/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia26":
                                _data = Convert.ToDateTime("26/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia27":
                                _data = Convert.ToDateTime("27/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia28":
                                _data = Convert.ToDateTime("28/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia29":
                                _data = Convert.ToDateTime("29/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia30":
                                _data = Convert.ToDateTime("30/06/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JunDia31":
                                _data = Convert.ToDateTime("31/06/" + referenciaMaskedEditBox.Text);
                                break;
                            #endregion

                            #region Julho
                            case "JulDia1":
                                _data = Convert.ToDateTime("01/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia2":
                                _data = Convert.ToDateTime("02/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia3":
                                _data = Convert.ToDateTime("03/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia4":
                                _data = Convert.ToDateTime("04/07/" + referenciaMaskedEditBox.Text); ;
                                break;
                            case "JulDia5":
                                _data = Convert.ToDateTime("05/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia6":
                                _data = Convert.ToDateTime("06/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia7":
                                _data = Convert.ToDateTime("07/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia8":
                                _data = Convert.ToDateTime("08/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia9":
                                _data = Convert.ToDateTime("09/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia10":
                                _data = Convert.ToDateTime("10/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia11":
                                _data = Convert.ToDateTime("11/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia12":
                                _data = Convert.ToDateTime("12/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia13":
                                _data = Convert.ToDateTime("13/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia14":
                                _data = Convert.ToDateTime("14/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia15":
                                _data = Convert.ToDateTime("15/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia16":
                                _data = Convert.ToDateTime("16/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia17":
                                _data = Convert.ToDateTime("17/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia18":
                                _data = Convert.ToDateTime("18/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia19":
                                _data = Convert.ToDateTime("19/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia20":
                                _data = Convert.ToDateTime("20/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia21":
                                _data = Convert.ToDateTime("21/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia22":
                                _data = Convert.ToDateTime("22/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia23":
                                _data = Convert.ToDateTime("23/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia24":
                                _data = Convert.ToDateTime("24/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia25":
                                _data = Convert.ToDateTime("25/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia26":
                                _data = Convert.ToDateTime("26/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia27":
                                _data = Convert.ToDateTime("27/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia28":
                                _data = Convert.ToDateTime("28/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia29":
                                _data = Convert.ToDateTime("29/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia30":
                                _data = Convert.ToDateTime("30/07/" + referenciaMaskedEditBox.Text);
                                break;
                            case "JulDia31":
                                _data = Convert.ToDateTime("31/07/" + referenciaMaskedEditBox.Text);
                                break;
                            #endregion

                            #region Agosto
                            case "AgoDia1":
                                _data = Convert.ToDateTime("01/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia2":
                                _data = Convert.ToDateTime("02/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia3":
                                _data = Convert.ToDateTime("03/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia4":
                                _data = Convert.ToDateTime("04/08/" + referenciaMaskedEditBox.Text); ;
                                break;
                            case "AgoDia5":
                                _data = Convert.ToDateTime("05/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia6":
                                _data = Convert.ToDateTime("06/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia7":
                                _data = Convert.ToDateTime("07/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia8":
                                _data = Convert.ToDateTime("08/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia9":
                                _data = Convert.ToDateTime("09/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia10":
                                _data = Convert.ToDateTime("10/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia11":
                                _data = Convert.ToDateTime("11/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia12":
                                _data = Convert.ToDateTime("12/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia13":
                                _data = Convert.ToDateTime("13/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia14":
                                _data = Convert.ToDateTime("14/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia15":
                                _data = Convert.ToDateTime("15/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia16":
                                _data = Convert.ToDateTime("16/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia17":
                                _data = Convert.ToDateTime("17/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia18":
                                _data = Convert.ToDateTime("18/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia19":
                                _data = Convert.ToDateTime("19/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia20":
                                _data = Convert.ToDateTime("20/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia21":
                                _data = Convert.ToDateTime("21/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia22":
                                _data = Convert.ToDateTime("22/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia23":
                                _data = Convert.ToDateTime("23/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia24":
                                _data = Convert.ToDateTime("24/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia25":
                                _data = Convert.ToDateTime("25/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia26":
                                _data = Convert.ToDateTime("26/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia27":
                                _data = Convert.ToDateTime("27/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia28":
                                _data = Convert.ToDateTime("28/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia29":
                                _data = Convert.ToDateTime("29/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia30":
                                _data = Convert.ToDateTime("30/08/" + referenciaMaskedEditBox.Text);
                                break;
                            case "AgoDia31":
                                _data = Convert.ToDateTime("31/08/" + referenciaMaskedEditBox.Text);
                                break;
                            #endregion

                            #region Setembro
                            case "SetDia1":
                                _data = Convert.ToDateTime("01/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia2":
                                _data = Convert.ToDateTime("02/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia3":
                                _data = Convert.ToDateTime("03/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia4":
                                _data = Convert.ToDateTime("04/09/" + referenciaMaskedEditBox.Text); ;
                                break;
                            case "SetDia5":
                                _data = Convert.ToDateTime("05/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia6":
                                _data = Convert.ToDateTime("06/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia7":
                                _data = Convert.ToDateTime("07/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia8":
                                _data = Convert.ToDateTime("08/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia9":
                                _data = Convert.ToDateTime("09/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia10":
                                _data = Convert.ToDateTime("10/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia11":
                                _data = Convert.ToDateTime("11/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia12":
                                _data = Convert.ToDateTime("12/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia13":
                                _data = Convert.ToDateTime("13/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia14":
                                _data = Convert.ToDateTime("14/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia15":
                                _data = Convert.ToDateTime("15/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia16":
                                _data = Convert.ToDateTime("16/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia17":
                                _data = Convert.ToDateTime("17/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia18":
                                _data = Convert.ToDateTime("18/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia19":
                                _data = Convert.ToDateTime("19/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia20":
                                _data = Convert.ToDateTime("20/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia21":
                                _data = Convert.ToDateTime("21/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia22":
                                _data = Convert.ToDateTime("22/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia23":
                                _data = Convert.ToDateTime("23/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia24":
                                _data = Convert.ToDateTime("24/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia25":
                                _data = Convert.ToDateTime("25/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia26":
                                _data = Convert.ToDateTime("26/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia27":
                                _data = Convert.ToDateTime("27/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia28":
                                _data = Convert.ToDateTime("28/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia29":
                                _data = Convert.ToDateTime("29/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia30":
                                _data = Convert.ToDateTime("30/09/" + referenciaMaskedEditBox.Text);
                                break;
                            case "SetDia31":
                                _data = Convert.ToDateTime("31/09/" + referenciaMaskedEditBox.Text);
                                break;
                            #endregion

                            #region Outubro
                            case "OutDia1":
                                _data = Convert.ToDateTime("01/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia2":
                                _data = Convert.ToDateTime("02/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia3":
                                _data = Convert.ToDateTime("03/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia4":
                                _data = Convert.ToDateTime("04/10/" + referenciaMaskedEditBox.Text); ;
                                break;
                            case "OutDia5":
                                _data = Convert.ToDateTime("05/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia6":
                                _data = Convert.ToDateTime("06/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia7":
                                _data = Convert.ToDateTime("07/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia8":
                                _data = Convert.ToDateTime("08/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia9":
                                _data = Convert.ToDateTime("09/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia10":
                                _data = Convert.ToDateTime("10/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia11":
                                _data = Convert.ToDateTime("11/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia12":
                                _data = Convert.ToDateTime("12/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia13":
                                _data = Convert.ToDateTime("13/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia14":
                                _data = Convert.ToDateTime("14/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia15":
                                _data = Convert.ToDateTime("15/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia16":
                                _data = Convert.ToDateTime("16/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia17":
                                _data = Convert.ToDateTime("17/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia18":
                                _data = Convert.ToDateTime("18/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia19":
                                _data = Convert.ToDateTime("19/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia20":
                                _data = Convert.ToDateTime("20/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia21":
                                _data = Convert.ToDateTime("21/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia22":
                                _data = Convert.ToDateTime("22/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia23":
                                _data = Convert.ToDateTime("23/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia24":
                                _data = Convert.ToDateTime("24/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia25":
                                _data = Convert.ToDateTime("25/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia26":
                                _data = Convert.ToDateTime("26/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia27":
                                _data = Convert.ToDateTime("27/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia28":
                                _data = Convert.ToDateTime("28/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia29":
                                _data = Convert.ToDateTime("29/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia30":
                                _data = Convert.ToDateTime("30/10/" + referenciaMaskedEditBox.Text);
                                break;
                            case "OutDia31":
                                _data = Convert.ToDateTime("31/10/" + referenciaMaskedEditBox.Text);
                                break;
                            #endregion

                            #region Novembro
                            case "NovDia1":
                                _data = Convert.ToDateTime("01/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia2":
                                _data = Convert.ToDateTime("02/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia3":
                                _data = Convert.ToDateTime("03/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia4":
                                _data = Convert.ToDateTime("04/11/" + referenciaMaskedEditBox.Text); ;
                                break;
                            case "NovDia5":
                                _data = Convert.ToDateTime("05/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia6":
                                _data = Convert.ToDateTime("06/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia7":
                                _data = Convert.ToDateTime("07/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia8":
                                _data = Convert.ToDateTime("08/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia9":
                                _data = Convert.ToDateTime("09/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia10":
                                _data = Convert.ToDateTime("10/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia11":
                                _data = Convert.ToDateTime("11/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia12":
                                _data = Convert.ToDateTime("12/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia13":
                                _data = Convert.ToDateTime("13/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia14":
                                _data = Convert.ToDateTime("14/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia15":
                                _data = Convert.ToDateTime("15/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia16":
                                _data = Convert.ToDateTime("16/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia17":
                                _data = Convert.ToDateTime("17/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia18":
                                _data = Convert.ToDateTime("18/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia19":
                                _data = Convert.ToDateTime("19/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia20":
                                _data = Convert.ToDateTime("20/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia21":
                                _data = Convert.ToDateTime("21/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia22":
                                _data = Convert.ToDateTime("22/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia23":
                                _data = Convert.ToDateTime("23/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia24":
                                _data = Convert.ToDateTime("24/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia25":
                                _data = Convert.ToDateTime("25/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia26":
                                _data = Convert.ToDateTime("26/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia27":
                                _data = Convert.ToDateTime("27/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia28":
                                _data = Convert.ToDateTime("28/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia29":
                                _data = Convert.ToDateTime("29/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia30":
                                _data = Convert.ToDateTime("30/11/" + referenciaMaskedEditBox.Text);
                                break;
                            case "NovDia31":
                                _data = Convert.ToDateTime("31/11/" + referenciaMaskedEditBox.Text);
                                break;
                            #endregion

                            #region Dezembro
                            case "DezDia1":
                                _data = Convert.ToDateTime("01/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia2":
                                _data = Convert.ToDateTime("02/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia3":
                                _data = Convert.ToDateTime("03/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia4":
                                _data = Convert.ToDateTime("04/12/" + referenciaMaskedEditBox.Text); ;
                                break;
                            case "DezDia5":
                                _data = Convert.ToDateTime("05/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia6":
                                _data = Convert.ToDateTime("06/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia7":
                                _data = Convert.ToDateTime("07/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia8":
                                _data = Convert.ToDateTime("08/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia9":
                                _data = Convert.ToDateTime("09/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia10":
                                _data = Convert.ToDateTime("10/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia11":
                                _data = Convert.ToDateTime("11/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia12":
                                _data = Convert.ToDateTime("12/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia13":
                                _data = Convert.ToDateTime("13/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia14":
                                _data = Convert.ToDateTime("14/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia15":
                                _data = Convert.ToDateTime("15/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia16":
                                _data = Convert.ToDateTime("16/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia17":
                                _data = Convert.ToDateTime("17/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia18":
                                _data = Convert.ToDateTime("18/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia19":
                                _data = Convert.ToDateTime("19/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia20":
                                _data = Convert.ToDateTime("20/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia21":
                                _data = Convert.ToDateTime("21/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia22":
                                _data = Convert.ToDateTime("22/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia23":
                                _data = Convert.ToDateTime("23/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia24":
                                _data = Convert.ToDateTime("24/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia25":
                                _data = Convert.ToDateTime("25/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia26":
                                _data = Convert.ToDateTime("26/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia27":
                                _data = Convert.ToDateTime("27/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia28":
                                _data = Convert.ToDateTime("28/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia29":
                                _data = Convert.ToDateTime("29/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia30":
                                _data = Convert.ToDateTime("30/12/" + referenciaMaskedEditBox.Text);
                                break;
                            case "DezDia31":
                                _data = Convert.ToDateTime("31/12/" + referenciaMaskedEditBox.Text);
                                break;
                                #endregion
                        }

                        _prog = new ProgramacaoFeriasBO().ConsultarInicioFerias(_usuario.CodigoInternoFuncionarioGlobus, _data);

                        DepartamentoPessoal.AutorizacaoProgramacaoDeFerias _tela = new AutorizacaoProgramacaoDeFerias();

                        _tela.empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
                        _tela.empresaComboBoxAdv.DisplayMember = "CodigoeNome";
                        _tela.empresaComboBoxAdv.Focus();

                        _empresa = new EmpresaBO().Consultar(_usuario.IdEmpresa);

                        for (int i = 0; i < _tela.empresaComboBoxAdv.Items.Count; i++)
                        {
                            _tela.empresaComboBoxAdv.SelectedIndex = i;
                            if (_tela.empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                            {
                                break;
                            }
                        }

                        _tela.usuarioTextBox.Text = _usuario.RegistroFuncionario;
                        _tela.inicialDateTimePicker.Value = _prog.DataInicio;

                        _tela.ShowDialog();
                    }


                }
            }
            catch { }

            mensagemSistemaLabel.Text = "";
            this.Cursor = Cursors.Default;
            this.Refresh();

            referenciaMaskedEditBox_Validating(sender, new CancelEventArgs());
        }

        private void gridGroupingControl1_TableControlMouseDown(object sender, GridTableControlMouseEventArgs e)
        {
            _colunaCorrente = gridGroupingControl1.TableControl.CurrentCell;
        }

        private void gridGroupingControl1_TableControlCurrentCellActivated(object sender, GridTableControlEventArgs e)
        {
        }

        private void IntegrarButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a integração com o Globus das Programações Aprovadas?"
                       , Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
            {
                return;
            }

            _listaGlobus = new List<ProgramacaoFeriasGlobus>();

            string nomes = "";

            // integra para o Globus as programações aprovadas, com periodo aquisitivo e dentro de 2 meses
            foreach (var item in _listaProgramacao.Where(w => w.Status == "A" && 
                                                         w.IniPeriodoAquisitivo != DateTime.MinValue && 
                                                         w.DataInicio < DateTime.Now.AddMonths(2)))
            {
                nomes = nomes + "[ " + item.Funcionario + " de " + item.DataInicio.ToShortDateString() + " até " + item.DataFim.ToShortDateString() + " ], ";

                _listaGlobus.Add(new ProgramacaoFeriasGlobus()
                {
                    CodIntFunc = item.CodIntFunc,
                    AquisitivoInicial = item.IniPeriodoAquisitivo,
                    AquisitivoFinal = item.FimPeriodoAquisitivo,
                    GozoInicial = item.DataInicio,
                    DiasFerias = item.QuantidadeDias,
                    IdProgramacao = item.Id
                });
            } 

            if (_listaGlobus.Count() != 0)
            {
                if (!new ProgramacaoFeriasBO().Gravar(_listaGlobus))
                {
                    new Notificacoes.Mensagem("Problemas durante a integração." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Integrou Programação de Férias da empresa " + empresaComboBoxAdv.Text + " " +  nomes.Substring(0, nomes.Length -2);

            _log.Tela = "Recursos Humanos - Programação Férias";
            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            referenciaMaskedEditBox_Validating(sender, new CancelEventArgs());
        }

        private void RecolherCheckBox_Click(object sender, EventArgs e)
        {
            if (RecolherCheckBox.Checked)
                gridGroupingControl1.Table.CollapseAllGroups();
            else
                gridGroupingControl1.Table.ExpandAllGroups();
            
        }

        private void GozadasCheckBox_Click(object sender, EventArgs e)
        {
        }

        private void MostrarGozadasCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                referenciaMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GozadasCheckBox.Focus();
            }
        }
    }
}
