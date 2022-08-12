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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.BI
{
    public partial class AcessoUsuariosPorEmail : Form
    {
        public AcessoUsuariosPorEmail()
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

        Classes.Usuario _usuarios;
        
        Classes.PowerBI.EmailDeAcesso _emails;
        List<Classes.Empresa>_listaEmpresa;
        List<Classes.PowerBI.UsuariosAutorizados> _listaUsuariosAutorizados;
        List<Classes.PowerBI.UsuariosAutorizados> _listaUsuariosAutorizadosLog;
        List<Classes.PowerBI.EmpresasAutorizadas> _listaEmpresasAutorizadas;

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

        private void AcessoUsuariosPorEmail_Shown(object sender, EventArgs e)
        {
            _listaEmpresa = new EmpresaBO().Listar(false);

            GridMetroColors metroColor = new GridMetroColors();

            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl1.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].AllowSort = true;
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

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
        }

        private void EmailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                UsuarioTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void UsuarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                IncluirButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EmailTextBox.Focus();
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

        private void EmailTextBox_Enter(object sender, EventArgs e)
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
            limparButton.BackColor = Publicas._botaoFocado;
            limparButton.ForeColor = Publicas._fonteBotaoFocado;
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
                
        private void EmailTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (EmailTextBox.Text.Trim() == "")
            {
                new Pesquisas.EmailBI().ShowDialog();

                EmailTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (EmailTextBox.Text.Trim() == "" || EmailTextBox.Text.Trim() == "0")
                {
                    EmailTextBox.Text = string.Empty;
                    EmailTextBox.Focus();
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!reg.IsMatch(EmailTextBox.Text))
                {
                    new Notificacoes.Mensagem("E-mail inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    EmailTextBox.Focus();
                    return;
                }
            }

            _emails = new PowerBIBO().Consultar(EmailTextBox.Text);

            if (!_emails.Existe)
            {
                new Notificacoes.Mensagem("E-mail não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                EmailTextBox.Focus();
                return;
            }

            if (!_emails.Ativo)
            {
                new Notificacoes.Mensagem("E-mail não está ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                EmailTextBox.Focus();
                return;
            }

            _listaEmpresasAutorizadas = new PowerBIBO().Listar(_emails.Id);
            _listaUsuariosAutorizados = new PowerBIBO().ListarUsuarios(_emails.Id);
            _listaUsuariosAutorizadosLog = new PowerBIBO().ListarUsuarios(_emails.Id);

            gravarButton.Enabled = true;
            gridGroupingControl1.DataSource = _listaUsuariosAutorizados.OrderBy(o => o.Nome).ToList();

        }

        private void UsuarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            UsuarioTextBox.BorderColor = Publicas._bordaSaida;

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

            if (UsuarioTextBox.Text.Trim() == "")
            {
                new Pesquisas.Usuarios().ShowDialog();

                UsuarioTextBox.Text = Publicas._usuarioAcesso;

                if (UsuarioTextBox.Text.Trim() == "")
                {
                    UsuarioTextBox.Text = string.Empty;
                    UsuarioTextBox.Focus();
                    return;
                }
            }

            _usuarios = new UsuarioBO().Consultar(UsuarioTextBox.Text);

            if (_usuarios == null || !_usuarios.Existe)
            {
                new Notificacoes.Mensagem("Usuário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                UsuarioTextBox.Focus();
                return;
            }

            //bool encontrou = false;
            //foreach (var item in _listaEmpresa.Where(w => w.IdEmpresa == _usuarios.IdEmpresa))
            //{
            //    foreach (var itemE in _listaEmpresasAutorizadas.Where(w => w.Empresa == item.CodigoEmpresaGlobus))
            //    {
            //        encontrou = true;
            //        break;
            //    }
            //}

            //if (!encontrou)
            //{
            //    new Notificacoes.Mensagem("Usuário não cadastrado na empresa associada a este e-mail.", Publicas.TipoMensagem.Alerta).ShowDialog();
            //    UsuarioTextBox.Focus();
            //    return;
            //}

            nomeResponsavelTextBox.Text = _usuarios.Nome;
        }

        private void PesquisaResponsavelButton_Click(object sender, EventArgs e)
        {
            if (UsuarioTextBox.Text.Trim() == "")
            {
                new Pesquisas.Usuarios().ShowDialog();

                UsuarioTextBox.Text = Publicas._usuarioAcesso;

                if (UsuarioTextBox.Text.Trim() == "")
                {
                    UsuarioTextBox.Text = string.Empty;
                    UsuarioTextBox.Focus();
                    return;
                }

                UsuarioTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {
            if (EmailTextBox.Text.Trim() == "")
            {
                new Pesquisas.EmailBI().ShowDialog();

                EmailTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (EmailTextBox.Text.Trim() == "" || EmailTextBox.Text.Trim() == "0")
                {
                    EmailTextBox.Text = string.Empty;
                    EmailTextBox.Focus();
                    return;
                }

                EmailTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaUsuariosAutorizados.Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhum usuário selecionado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                gridGroupingControl1.Focus();
                return;
            }

            _listaUsuariosAutorizados.ForEach(u => u.IdEmail = _emails.Id);

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            string _descricao = "";

            foreach (var item in _listaUsuariosAutorizados)
            {
                if (_listaUsuariosAutorizadosLog.Where(w => w.IdUsuario == item.IdUsuario).Count() == 0)
                    _descricao = _descricao + "Usuário " + item.Nome + " ";
            }

            if (_descricao != "")
                _log.Descricao = "Incluiu " + _descricao + "para o e-mail " + EmailTextBox.Text;

            if (!new PowerBIBO().Gravar(_listaUsuariosAutorizados))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            _log.Tela = "TI - PowerBI - Associa Usuários";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            limparButton_Click(sender, new EventArgs());
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            EmailTextBox.Text = string.Empty;
            UsuarioTextBox.Text = string.Empty;
            nomeResponsavelTextBox.Text = string.Empty;
            EmailTextBox.Focus();

            gridGroupingControl1.DataSource = new List<PowerBI.UsuariosAutorizados>();
            gravarButton.Enabled = false;
        }

        private void IncluirButton_Click(object sender, EventArgs e)
        {
            if (_listaUsuariosAutorizados == null)
                _listaUsuariosAutorizados = new List<PowerBI.UsuariosAutorizados>();

            if (_listaUsuariosAutorizados.Where(w => w.IdUsuario == _usuarios.Id).Count() == 0)
                _listaUsuariosAutorizados.Add(new PowerBI.UsuariosAutorizados() { IdEmail = _emails.Id, IdUsuario = _usuarios.Id, Nome = _usuarios.Nome });

            gridGroupingControl1.DataSource = new List<PowerBI.UsuariosAutorizados>();
            gridGroupingControl1.DataSource = _listaUsuariosAutorizados;
            UsuarioTextBox.Text = string.Empty;
            nomeResponsavelTextBox.Text = string.Empty;
            UsuarioTextBox.Focus();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            int Id = 0;

            if (rec != null)
            {

                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    
                    if (new Notificacoes.Mensagem("Confirma a exclusão do usuário ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.PowerBI.UsuariosAutorizados _excluirTipos = new Classes.PowerBI.UsuariosAutorizados();
                    gridGroupingControl1.DataSource = new List<Classes.PowerBI.UsuariosAutorizados>();


                    try
                    {
                        Id = (int)dr["IdUsuario"];
                    }
                    catch
                    {
                        int posIniId = dr.Info.IndexOf("IdUsuario =") + 11;
                        int posFimId = dr.Info.IndexOf(", Nome");
                        Id = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                    }

                    foreach (var item in _listaUsuariosAutorizados.Where(w => w.IdUsuario == Id))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new PowerBIBO().ExcluirUsuario(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _listaUsuariosAutorizados.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu o usuário " + _excluirTipos.Nome + " que estava associada ao e-mail " + EmailTextBox.Text;
                    _log.Tela = "TI - PowerBI - Associa Usuários";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _listaUsuariosAutorizados;
            }
        }

        private void UsuarioTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaResponsavelButton.Enabled = string.IsNullOrEmpty(UsuarioTextBox.Text.Trim());
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(EmailTextBox.Text.Trim());
        }
    }
}
