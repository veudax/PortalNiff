using Classes;
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

namespace Suportte.Operacional
{
    public partial class Feriados : Form
    {
        public Feriados()
        {
            InitializeComponent();
            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                inicialDateTimePicker.BorderColor = Publicas._bordaSaida;
                inicialDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
                inicialDateTimePicker.Value = DateTime.Now;

                if (Publicas._TemaBlack)
                {
                    inicialDateTimePicker.Style = VisualStyle.Office2016Black;
                    gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl1.BackColor = Publicas._panelTitulo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Feriado _feriado;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.FeriadoEmenda> _listaFeriados;
        List<Classes.FeriadoEmenda> _listaFeriadosLog;
        List<Classes.Empresa> _listaEmpresasAutorizadas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasUsuario;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;

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

        private void Feriados_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            _empresaDoColaborador = new ColaboradoresBO().Listar(Publicas._idColaborador);

            if (Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.IdEmpresa == 19)
            {
                empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
                empresaComboBoxAdv.DisplayMember = "CodigoeNome";
                empresaComboBoxAdv.Focus();
            }
            else
            {
                if (_empresaDoColaborador.Count == 0)
                {
                    _listaEmpresasUsuario = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._usuario.Id);

                    foreach (var item in _listaEmpresasUsuario.Where(w => w.EmpresaAutoriza))
                    {
                        _listaEmpresasAutorizadas.AddRange(_listaEmpresas.Where(w => w.IdEmpresa == item.IdEmpresa));
                    }
                }
                else
                {
                    _listaEmpresasAutorizadas = new List<Empresa>();
                    foreach (var item in _empresaDoColaborador.Where(w => w.Inicio != DateTime.MinValue.Date &&
                                                                          (w.Fim == DateTime.MinValue.Date || w.Fim <= DateTime.Now.Date)))
                    {
                        _listaEmpresasAutorizadas.AddRange(_listaEmpresas.Where(w => w.IdEmpresa == item.IdEmpresa));
                    }
                }

                empresaComboBoxAdv.DataSource = _listaEmpresasAutorizadas.OrderBy(o => o.CodigoeNome).ToList();
                empresaComboBoxAdv.DisplayMember = "CodigoeNome";
                empresaComboBoxAdv.Focus();
            }
            

            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].AllowFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
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
                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl1.Table.DefaultCaptionRowHeight = 23;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                inicialDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void inicialDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                inicialDateTimePicker.Focus();
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

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void inicialDateTimePicker_Enter(object sender, EventArgs e)
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

            _listaFeriados = new FeriadoBO().Listar(_empresa.IdEmpresa);
            _listaFeriadosLog = new FeriadoBO().Listar(_empresa.IdEmpresa);
            gridGroupingControl1.DataSource = _listaFeriados;

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Consultou feriados da empresa " + empresaComboBoxAdv.Text;
            _log.Tela = "Operacional - Cadastros - Feriados/Emendas";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            gravarButton.Enabled = true;
            excluirButton.Enabled = _listaFeriados.Count() != 0;
        }

        private void inicialDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
            
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _feriado = new FeriadoBO().Consultar(inicialDateTimePicker.Value);

            if (_feriado.Existe)
                nomeTextBox.Text = _feriado.Descricao;

        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            if (!FeriadoRadioButton.Checked && !EmendaRadioButton.Checked)
            {
                new Notificacoes.Mensagem("Selecione o tipo da data.", Publicas.TipoMensagem.Alerta).ShowDialog();
                FeriadoRadioButton.Focus();
                return;
            }

            if (_listaFeriados == null)
                _listaFeriados = new List<FeriadoEmenda>();

            if (_listaFeriados.Where(w => w.Data.Date == inicialDateTimePicker.Value.Date).Count() == 0)
            {
                _listaFeriados.Add(new FeriadoEmenda()
                {
                    IdEmpresa = _empresa.IdEmpresa,
                    Data = inicialDateTimePicker.Value,
                    Tipo = (FeriadoRadioButton.Checked ? "F" : "E"),
                    TipoDescricao = (FeriadoRadioButton.Checked ? "Feriado" : "Emenda"),
                    Ano = inicialDateTimePicker.Value.Year,
                    Nome = nomeTextBox.Text
                });
            }
            else
            {
                foreach (var item in _listaFeriados.Where(w => w.Data.Date == inicialDateTimePicker.Value.Date))
                {
                    item.Tipo = (FeriadoRadioButton.Checked ? "F" : "E");
                    item.Nome = nomeTextBox.Text;
                    item.TipoDescricao = (FeriadoRadioButton.Checked ? "Feriado" : "Emenda");
                }
            }

            gridGroupingControl1.DataSource = new List<FeriadoEmenda>();
            gridGroupingControl1.DataSource = _listaFeriados;

            inicialDateTimePicker.Focus();
            nomeTextBox.Text = string.Empty;
            FeriadoRadioButton.Checked = false;
            EmendaRadioButton.Checked = false;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            empresaComboBoxAdv.Focus();
            _listaFeriados.Clear();
            inicialDateTimePicker.Value = DateTime.Now;
            FeriadoRadioButton.Checked = false;
            EmendaRadioButton.Checked = false;

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

            gridGroupingControl1.DataSource = new List<FeriadoEmenda>();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {

            if (_listaFeriados.Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhum data informada." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                inicialDateTimePicker.Focus();
                return;
            }

            if (!new FeriadoBO().Gravar(_listaFeriados))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            string _descricao = "";
            foreach (var itemL in _listaFeriadosLog)
            {
                foreach (var item in _listaFeriados.Where(w => w.Data == itemL.Data))
                {
                    if (item.Tipo != itemL.Tipo)
                        _descricao = _descricao + "Data " + item.Data.ToShortDateString() + " [tipo] de " + itemL.Tipo + " para " + item.Tipo + ", ";
                }
            }

            foreach (var item in _listaFeriados)
            {
                if (_listaFeriadosLog.Where(w => w.Data == item.Data).Count() == 0)
                {
                    _descricao = _descricao + "incluiu Data " + item.Data.ToShortDateString() + " tipo " + item.Tipo + ", ";
                }
            }

            if (!string.IsNullOrEmpty(_descricao))
                _descricao = _descricao.Substring(0, _descricao.Length - 2);

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou as data da empresa " + empresaComboBoxAdv.Text + " [ " + _descricao + " ]";

            _log.Tela = "Operacional - Cadastros - Feriados/Emendas";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new FeriadoBO().Excluir(_empresa.IdEmpresa, true))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }
            
            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu todas as data da empresa " + empresaComboBoxAdv.Text;
            _log.Tela = "Operacional - Cadastros - Feriados/Emendas";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void excluirDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão da data?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    Classes.FeriadoEmenda _excluirTipos = new Classes.FeriadoEmenda();

                    gridGroupingControl1.DataSource = new List<Classes.FeriadoEmenda>();

                    int posIniId = dr.Info.IndexOf("Data =") + 6;
                    int posFimId = dr.Info.IndexOf(", Tipo");
                    DateTime _data = Convert.ToDateTime(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                                        
                    foreach (var item in _listaFeriados.Where(w => w.Data.Date == _data.Date))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new FeriadoBO().Excluir(item.Id, false))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _listaFeriados.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu a data " + _excluirTipos.Data +
                        " " + _excluirTipos.TipoDescricao +
                        " da empresa " + empresaComboBoxAdv.Text;
                    _log.Tela = "Operacional - Cadastros - Feriados/Emendas";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _listaFeriados;
            }
            
        }

        private void FeriadoRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void nomeTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
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
    }
}
