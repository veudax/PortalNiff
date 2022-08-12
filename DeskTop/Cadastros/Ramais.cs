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

namespace Suportte.Cadastros
{
    public partial class Ramais : Form
    {
        public Ramais()
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
                    ColaboradoresGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    ColaboradoresGrid.ColorStyles = ColorStyles.Office2010Black;
                    ColaboradoresGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    ColaboradoresGrid.BackColor = Publicas._panelTitulo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Ramais _ramais;
        Classes.Telefone _telefone;
        Classes.Empresa _empresa;
        Classes.Empresa _empresaRamal;
        Classes.Colaboradores _colaborador;
        List<Classes.Empresa> _listaEmpresas;
        List<EmpresaDoUsuario> _listaEmpresasFuncionario;
        List<Classes.RamaisAssociadosAoColaborador> _listaColaboradores;

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

        private void telefoneTextBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void ramalTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
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

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ColaboradorTextBox.Focus();
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

        private void telefoneTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ramalTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EmpresaRamalComboBox.Focus();
            }
        }

        private void ramalTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                LocalText.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                telefoneTextBox.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (empresaComboBoxAdv.Enabled)
                    empresaComboBoxAdv.Focus();
                else
                    ColaboradoresGrid.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                LocalText.Focus();
            }
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ColaboradorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                LocalText.Focus();
            }
        }

        private void ColaboradorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                NomeColaboradorTextBox.Focus();
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;

            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                NomeColaboradorTextBox.Focus();
            }
        }

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            limparButton.BackColor = Publicas._botao;
            limparButton.ForeColor = Publicas._fonteBotao;
        }

        private void excluirButton_Validating(object sender, CancelEventArgs e)
        {
            excluirButton.BackColor = Publicas._botao;
            excluirButton.ForeColor = Publicas._fonteBotao;
        }

        private void telefoneTextBox_Validating(object sender, CancelEventArgs e)
        {
            telefoneTextBox.BorderColor = Publicas._bordaSaida;
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._telefone = 0;

            if (telefoneTextBox.ClipText.Trim() == "")
            {
                Publicas._idEmpresa = _empresaRamal.IdEmpresa;
                new Pesquisas.Telefone().ShowDialog();

                if (Publicas._idRetornoPesquisa == 0)
                {
                    telefoneTextBox.Text = string.Empty;
                    telefoneTextBox.Focus();
                    return;
                }
                _telefone = new RamaisBO().Consultar(Publicas._idRetornoPesquisa);

                if (_telefone.Existe)
                {
                    telefoneTextBox.Text = _telefone.Numero.ToString();
                }
                Publicas._idEmpresa = 0;
                return;
            }

            _telefone = new RamaisBO().Consultar(_empresaRamal.IdEmpresa, Convert.ToDecimal(telefoneTextBox.ClipText.Trim()));
            
        }

        private void ramalTextBox_Validating(object sender, CancelEventArgs e)
        {
            ramalTextBox.BorderColor = Publicas._bordaSaida;
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            Publicas._telefone = _telefone.Id;

            if (ramalTextBox.Text.Trim() == "")
            {
                new Pesquisas.Ramais().ShowDialog();

                if (Publicas._idRetornoPesquisa == 0)
                {
                    ramalTextBox.Text = string.Empty;
                    ramalTextBox.Focus();
                    return;
                }

                ramalTextBox.Text = Publicas._idRetornoPesquisa.ToString();
            }
            Publicas._telefone = 0;

            _ramais = new RamaisBO().ConsultarRamal(_telefone.Id, Convert.ToInt32(ramalTextBox.Text.Trim()));

            if (_ramais.Existe)
            {
                LocalText.Text = _ramais.Grupo;
                // buscar os colaboradores associados
                _listaColaboradores = new RamaisBO().ListarColaboradoresAssociados(_ramais.Id);

                ColaboradoresGrid.DataSource = _listaColaboradores;
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _ramais.Existe;

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

        private void ColaboradorTextBox_Validating(object sender, CancelEventArgs e)
        {
            ColaboradorTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado || Publicas._setaParaBaixo)
            {
                ColaboradorTextBox.Text = string.Empty;
                PesquisaColaboradorButton.Enabled = false;
                Publicas._escTeclado = false;
                Publicas._setaParaBaixo = false;
                return;
            }

            if (string.IsNullOrEmpty(ColaboradorTextBox.Text.Trim()))
            {
                return;
            }

            Publicas._idEmpresa = 0;
            _colaborador = new ColaboradoresBO().ConsultaColaborador(Convert.ToInt32(ColaboradorTextBox.Text.Trim()));

            if (!_colaborador.Existe)
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ColaboradorTextBox.Focus();
                return;
            }

            NomeColaboradorTextBox.Text = _colaborador.Nome;            
        }
        
        private void Ramais_Shown(object sender, EventArgs e)
        {
            if (Publicas._usuario.IdEmpresa != 1) // NIFF
            {
                _listaEmpresasFuncionario = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._usuario.Id);
                _listaEmpresas = new List<Empresa>();

                foreach (var item in _listaEmpresasFuncionario.Where(w => w.EmpresaAutoriza))
                {
                    _listaEmpresas.Add(new Empresa() { IdEmpresa = item.IdEmpresa, CodigoEmpresaGlobus = item.CodigoEmpresaGlobus, CodigoeNome = item.CodigoeNome, Nome = item.Nome, Ativo = item.Ativo, NomeAbreviado = item.NomeAbreviado });
                }               

            }
            else
                _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";

            EmpresaRamalComboBox.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            EmpresaRamalComboBox.DisplayMember = "CodigoeNome";

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

            for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
            {
                empresaComboBoxAdv.SelectedIndex = i;
                if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                {
                    break;
                }
            }

            GridMetroColors metroColor = new GridMetroColors();
            GridDynamicFilter filter = new GridDynamicFilter();

            filter.WireGrid(ColaboradoresGrid);
            ColaboradoresGrid.DataSource = new List<RamaisAssociadosAoColaborador>();
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

            ColaboradoresGrid.ActivateCurrentCellBehavior = Syncfusion.Windows.Forms.Grid.GridCellActivateAction.SetCurrent;
            ColaboradoresGrid.DefaultGridBorderStyle = Syncfusion.Windows.Forms.Grid.GridBorderStyle.Solid;
            ColaboradoresGrid.ShowNavigationBar = true;
            ColaboradoresGrid.ShowRowHeaders = false;
            ColaboradoresGrid.TopLevelGroupOptions.ShowCaption = false;
            ColaboradoresGrid.TableDescriptor.AllowEdit = false;
            ColaboradoresGrid.TableDescriptor.AllowNew = false;
            ColaboradoresGrid.TableDescriptor.AllowRemove = false;

            ColaboradoresGrid.SortIconPlacement = SortIconPlacement.Left;
            ColaboradoresGrid.TopLevelGroupOptions.ShowFilterBar = true;
            ColaboradoresGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            ColaboradoresGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            ColaboradoresGrid.RecordNavigationBar.Label = "Colaboradores";
            ColaboradoresGrid.TableControl.CellToolTip.Active = true;

            if (!Publicas._TemaBlack)
            {
                ColaboradoresGrid.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Metro;
                ColaboradoresGrid.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
                this.ColaboradoresGrid.SetMetroStyle(metroColor);
                this.ColaboradoresGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.ColaboradoresGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            ColaboradoresGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            ColaboradoresGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;

        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_telefone == null)
                _telefone = new Classes.Telefone();

            _telefone.IdEmpresa = _empresaRamal.IdEmpresa;
            _telefone.Numero = Convert.ToDecimal(telefoneTextBox.ClipText.Trim());

            if (_ramais == null)
                _ramais = new Classes.Ramais();

            _ramais.Grupo = LocalText.Text;
            _ramais.IdTelefone = _telefone.Id;
            _ramais.Numero = Convert.ToInt32(ramalTextBox.Text.Trim());

            if (!new RamaisBO().Gravar(_telefone, _ramais, _listaColaboradores))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            ColaboradoresGrid.DataSource = new List<RamaisAssociadosAoColaborador>();
            ramalTextBox.Text = string.Empty;
            empresaComboBoxAdv_Validating(sender, new CancelEventArgs());
            telefoneTextBox_Validating(sender, new CancelEventArgs());
            LocalText.Text = string.Empty;
            ColaboradorTextBox.Text = string.Empty;
            NomeColaboradorTextBox.Text = string.Empty;
            textBoxExt1.Text = string.Empty;
            _listaColaboradores.Clear();

            ramalTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão de todos os colaboradores desse ramal ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new RamaisBO().Excluir(_ramais.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void LocalText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                NomeColaboradorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ramalTextBox.Focus();
            }
        }

        private void LocalText_Validating(object sender, CancelEventArgs e)
        {
            LocalText.BorderColor = Publicas._bordaSaida;
            textBoxExt1.BorderColor = Publicas._bordaSaida;
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

        }

        private void LocalText_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void telefoneTextBox_TextChanged(object sender, EventArgs e)
        {
            TelefonePesquisaButton.Enabled = string.IsNullOrEmpty(telefoneTextBox.ClipText.Trim());
        }

        private void ramalTextBox_TextChanged(object sender, EventArgs e)
        {
            RamaisPesquisaButton.Enabled = string.IsNullOrEmpty(ramalTextBox.Text.Trim());
        }

        private void textBoxExt1_Validating(object sender, CancelEventArgs e)
        {
            textBoxExt1.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado || Publicas._setaParaBaixo)
            {
                textBoxExt1.Text = string.Empty;
                PesquisaColaboradorButton.Enabled = false;
                Publicas._escTeclado = false;
                Publicas._setaParaBaixo = false;
                return;
            }


            if (string.IsNullOrEmpty(NomeColaboradorTextBox.Text.Trim()))
            {
                NomeColaboradorTextBox.Focus();
                return;
            }

            ColaboradoresGrid.DataSource = new List<RamaisAssociadosAoColaborador>();
            if (_listaColaboradores == null)
                _listaColaboradores = new List<RamaisAssociadosAoColaborador>();

            try
            {
                if (!string.IsNullOrEmpty(NomeColaboradorTextBox.Text.Trim()))
                {
                    if (_listaColaboradores.Where(w => w.IdColaborador == _colaborador.Id).Count() == 0)
                    {
                        RamaisAssociadosAoColaborador _ram = new RamaisAssociadosAoColaborador();
                        if (_colaborador != null)
                            _ram.IdColaborador = _colaborador.Id;

                        _ram.NomeColaborador = NomeColaboradorTextBox.Text;
                        _ram.Complemento = textBoxExt1.Text;

                        _listaColaboradores.Add(_ram);
                    }
                    else
                    {
                        foreach (var item in _listaColaboradores.Where(w => w.IdColaborador == _colaborador.Id))
                        {
                            item.NomeColaborador = NomeColaboradorTextBox.Text;
                            item.Complemento = textBoxExt1.Text;
                        }
                    }
                }
            }
            catch
            {
                if (_listaColaboradores.Where(w => w.NomeColaborador == NomeColaboradorTextBox.Text).Count() == 0)
                {
                    RamaisAssociadosAoColaborador _ram = new RamaisAssociadosAoColaborador();
                    if (_colaborador != null)
                        _ram.IdColaborador = _colaborador.Id;

                    _ram.NomeColaborador = NomeColaboradorTextBox.Text;
                    _ram.Complemento = textBoxExt1.Text;

                    _listaColaboradores.Add(_ram);
                }
                else
                {
                    foreach (var item in _listaColaboradores.Where(w => w.NomeColaborador == NomeColaboradorTextBox.Text))
                    {
                        item.NomeColaborador = NomeColaboradorTextBox.Text;
                        item.Complemento = textBoxExt1.Text;
                    }
                }
            }
            ColaboradoresGrid.DataSource = _listaColaboradores;

            ColaboradorTextBox.Text = string.Empty;
            NomeColaboradorTextBox.Text = string.Empty;
            textBoxExt1.Text = string.Empty;
            ColaboradorTextBox.Focus();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int i = ColaboradoresGrid.Table.CurrentRecord.GetRecord().GetRowIndex();
                GridRecordRow _registro = ColaboradoresGrid.Table.DisplayElements[i] as GridRecordRow;

                int Id = 0;
                string nome = "";
                if (_registro != null)
                {
                    ColaboradoresGrid.DataSource = new List<RamaisAssociadosAoColaborador>();
                    Record dr = _registro.GetRecord() as Record;

                    if (dr != null)
                    {
                        try
                        {
                            Id = Convert.ToInt32(dr["IdColaborador"].ToString());
                        }
                        catch
                        {
                            int posIniId = dr.Info.IndexOf("IdColaborador =") + 15;
                            int posFimId = dr.Info.IndexOf(", NomeColaborador");
                            Id = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        }

                        if (Id != 0)
                        {
                            foreach (var item in _listaColaboradores.Where(w => w.IdColaborador == Id))
                            {
                                item.Excluir = true;
                            }
                        }
                        else
                        {
                            try
                            {
                                nome = dr["NomeColaborador"].ToString();
                            }
                            catch
                            {
                                int posIniId = dr.Info.IndexOf("NomeColaborador =") + 17;
                                int posFimId = dr.Info.IndexOf(", Complemento");
                                nome = dr.Info.Substring(posIniId, posFimId - posIniId).Trim();
                            }
                            foreach (var item in _listaColaboradores.Where(w => w.NomeColaborador == nome))
                            {
                                item.Excluir = true;
                            }
                        }
                    }
                    ColaboradoresGrid.DataSource = _listaColaboradores;
                }
            }
            catch { }
        }

        private void ColaboradoresGrid_QueryCellStyleInfo(object sender, GridTableCellStyleInfoEventArgs e)
        {
            Record dr;
            try
            { 
                if (e.TableCellIdentity.RowIndex != -1)
                {
                    GridRecordRow rec = this.ColaboradoresGrid.Table.DisplayElements[e.TableCellIdentity.RowIndex] as GridRecordRow;

                    dr = rec.GetRecord() as Record;
                    if (dr != null && (bool)dr["Excluir"])
                    {
                        e.Style.TextColor = Color.Red;
                        e.Style.CellTipText = "Será excluído ao gravar";
                    }                    
                }
            }
            catch { }
        }

        private void textBoxExt1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ColaboradoresGrid.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ColaboradorTextBox.Focus();
            }
        }

        private void PesquisaColaboradorButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ColaboradorTextBox.Text.Trim()))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;

                new Pesquisas.Colaborador().ShowDialog();

                ColaboradorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(ColaboradorTextBox.Text) || ColaboradorTextBox.Text == "0")
                {
                    ColaboradorTextBox.Text = string.Empty;
                    ColaboradorTextBox.Focus();
                    return;
                }
                Publicas._idEmpresa = 0;
                ColaboradorTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void EmpresaRamalComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                telefoneTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EmpresaRamalComboBox.Focus();
            }
        }

        private void EmpresaRamalComboBox_Validating(object sender, CancelEventArgs e)
        {
            EmpresaRamalComboBox.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == EmpresaRamalComboBox.Text))
            {
                _empresaRamal = item;
            }
        }

        private void TelefonePesquisaButton_Click(object sender, EventArgs e)
        {
            Publicas._telefone = 0;
            if (ramalTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = _empresaRamal.IdEmpresa;
                new Pesquisas.Ramais().ShowDialog();

                if (Publicas._idRetornoPesquisa == 0)
                {
                    ramalTextBox.Text = string.Empty;
                    ramalTextBox.Focus();
                    return;
                }
                Publicas._idEmpresa = 0;
                telefoneTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void RamaisPesquisaButton_Click(object sender, EventArgs e)
        {
            Publicas._telefone = Convert.ToInt32(telefoneTextBox.ClipText.Trim());

            if (ramalTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = _empresaRamal.IdEmpresa;
                new Pesquisas.Ramais().ShowDialog();

                if (Publicas._idRetornoPesquisa == 0)
                {
                    ramalTextBox.Text = string.Empty;
                    ramalTextBox.Focus();
                    return;
                }
                Publicas._idEmpresa = 0;
                ramalTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void ColaboradorLabel_MouseHover(object sender, EventArgs e)
        {
            ColaboradorLabel.Cursor = Cursors.Hand;
        }

        private void ColaboradorLabel_MouseLeave(object sender, EventArgs e)
        {
            ColaboradorLabel.Cursor = Cursors.Default;
        }

        private void ColaboradorLabel_Click(object sender, EventArgs e)
        {
            new Cadastros.Pessoas().ShowDialog();
        }

        private void NomeColaboradorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                textBoxExt1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                LocalText.Focus();
            }
        }
    }
}
