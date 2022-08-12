using Classes;
using Negocio;
using Suportte.Notificacoes;
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

namespace Suportte.Avaliacao_de_desempenho
{
    public partial class StatusPrazos : Form
    {
        public StatusPrazos()
        {
            InitializeComponent();

            dataInicioDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataInicioDateTimePicker.BackColor = nomeTextBox.BackColor;
            dataFimDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataFimDateTimePicker.BackColor = nomeTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.AutoAvaliacao> _listaAvaliacoes;

        public int mesReferencia;
        public string tipoAvaliacao;
        public string[] _dados = new string[5];

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

        private void StatusPrazos_Shown(object sender, EventArgs e)
        {
            string tipo = (direcaoCheckBox.Checked ? "'D'," : "") +
                 (gerenciaCheckBox.Checked ? "'G'," : "") +
                  (coordenacaoCheckBox.Checked ? "'C'," : "") +
                   (outrosCheckBox.Checked ? "'O'," : "");

            decimal finalizados = 0;
            decimal iniciados = 0;
            decimal naoIniciados = 0;
            decimal todos = 0;
            decimal resultado;
            decimal totalPercentual = 0;

            try
            {
                tipo = tipo.Substring(0, tipo.Length - 1);
            }
            catch { }

            _listaAvaliacoes = new AutoAvaliacaoBO().Listar(mesReferencia, tipo, tipoAvaliacao);

            finalizadosGridGroupingControl.DataSource = _listaAvaliacoes.Where(w => w.Status == "F").ToList();
            emAndamentoGridGroupingControl.DataSource = _listaAvaliacoes.Where(w => w.Status == "I").ToList();
            naoIniciadoGridGroupingControl.DataSource = _listaAvaliacoes.Where(w => w.Status == "N").ToList();

            todos = _listaAvaliacoes.Count();
            finalizados = _listaAvaliacoes.Where(w => w.Status == "F").Count();
            iniciados = _listaAvaliacoes.Where(w => w.Status == "I").Count();
            naoIniciados = _listaAvaliacoes.Where(w => w.Status == "N").Count();

            try
            {
                resultado = Math.Round((finalizados / todos) * 100,2);
                
            }
            catch
            {
                resultado = 0;
            }

            totalPercentual = totalPercentual + resultado;
            percentualFinalizadoLabel.Text = resultado.ToString() + "%";

            try
            {
                resultado = Math.Round((iniciados / todos) * 100,2);

            }
            catch
            {
                resultado = 0;
            }

            totalPercentual = totalPercentual + resultado;

            percentualEmAndamentoLabel.Text = resultado.ToString() + "%";

            try
            {
                resultado = Math.Round((naoIniciados / todos) * 100,2);
            }
            catch
            {
                resultado = 0;
            }

            totalPercentual = totalPercentual + resultado;

            if (totalPercentual > 100)
                resultado = resultado - (totalPercentual - 100);
            else
                resultado = resultado + (100 - totalPercentual);

            percentualNaoIniciadoLabel.Text = resultado.ToString() + "%";
            
            GridMetroColors metroColor = new GridMetroColors();

            finalizadosGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            finalizadosGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            finalizadosGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            finalizadosGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            finalizadosGridGroupingControl.RecordNavigationBar.Label = "Colaboradores";
            finalizadosGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < finalizadosGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                finalizadosGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                finalizadosGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
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

            this.finalizadosGridGroupingControl.SetMetroStyle(metroColor);
            this.finalizadosGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.finalizadosGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.finalizadosGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.finalizadosGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            emAndamentoGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            emAndamentoGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            emAndamentoGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            emAndamentoGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            emAndamentoGridGroupingControl.RecordNavigationBar.Label = "Colaboradores";
            emAndamentoGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < emAndamentoGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                emAndamentoGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                emAndamentoGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
            }
            
            this.emAndamentoGridGroupingControl.SetMetroStyle(metroColor);
            this.emAndamentoGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.emAndamentoGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.emAndamentoGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.emAndamentoGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

            naoIniciadoGridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            naoIniciadoGridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            naoIniciadoGridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            naoIniciadoGridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            naoIniciadoGridGroupingControl.RecordNavigationBar.Label = "Colaboradores";
            naoIniciadoGridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < naoIniciadoGridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                naoIniciadoGridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                naoIniciadoGridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
            }

            this.naoIniciadoGridGroupingControl.SetMetroStyle(metroColor);
            this.naoIniciadoGridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;

            this.naoIniciadoGridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
            this.naoIniciadoGridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            this.naoIniciadoGridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void enviarEmailButton_Click(object sender, EventArgs e)
        {
            mensagemSistemaLabel.Text = "Aguarde. Enviando e-mail ...";
            
            this.Refresh();
            foreach (var item in _listaAvaliacoes.Where(w => !string.IsNullOrEmpty(w.Email.Trim()) && w.Status == "N"))
            {
                _dados[3] = item.Colaborador;

                if (!Publicas.EnviarEmailAvaliacao(_dados, item.Email))
                {
                    new Notificacoes.Mensagem("Problemas durante o envio do e-mail." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    mensagemSistemaLabel.Text = "";
                    return;
                }
            }
            mensagemSistemaLabel.Text = "";
        }
    }
}
