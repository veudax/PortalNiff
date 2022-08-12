using Classes;
using Negocio;
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

namespace Suportte.ProcessoSeletivo
{
    public partial class Candidatos : Form
    {
        public Candidatos()
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
                    HistoricosGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    HistoricosGrid.ColorStyles = ColorStyles.Office2010Black;
                    HistoricosGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    HistoricosGrid.BackColor = Publicas._panelTitulo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        /* Status Histórico Candidato
        1 - Cadastrado
        2 - Pré-Selecionado
        3 - Contato
        4 - Sem Contato
        5 - Aprovado Gestor
        6 - Agendada 1ª Entrevista
        7 - Agendada 2ª Entrevista 
        8 - Reagendamento da 1ª Entrevista
        9 - Reagendamento da 2ª Entrevista
        10 - Reprovado Gestor
        11 - Aprovado Gestor
        12 - Cancelamento 1ª Entrevista
        13 - Cancelamento 2ª Entrevista
        14 - Processo Seletivo Encerrada
        15 - Aprovado
        16 - Reprovado
        */

        Classes.Candidatos _candidatos;
        Classes.Empresa _empresa;
        Classes.Vagas _vagas;
        Classes.CandidatosDaVaga _candidatosDaVaga;
        List<Classes.Empresa> _listaEmpresas;

        List<Classes.HistoricoDoCandidato> _historico;
        List<Classes.ArquivosDoCandidato> _arquivos;

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

        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '+')
            {
                codigoTextBox.Text = string.Empty;
                proximoButton_Click(sender, e);
            }
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                telefoneTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void telefoneTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CelularTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void CelularTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EmailTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                telefoneTextBox.Focus();
            }
        }

        private void EmailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CathoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CelularTextBox.Focus();
            }
        }

        private void CathoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                InfoJobsCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EmailTextBox.Focus();
            }
        }

        private void InfoJobsCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                LinkedinCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CathoCheckBox.Focus();
            }
        }

        private void LinkedinCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OutrosCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                InfoJobsCheckBox.Focus();
            }
        }

        private void OutrosCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (IndicadoCheckBox.Enabled)
                    IndicadoCheckBox.Focus();
                else
                    ContratadoCheckBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                LinkedinCheckBox.Focus();
            }
        }

        private void IndicadoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ContratadoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OutrosCheckBox.Focus();
            }
        }

        private void ContratadoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (DescricaoOutrosTextBox.Enabled)
                    DescricaoOutrosTextBox.Focus();
                else
                {
                    if (CVTextBox.Enabled)
                        CVTextBox.Focus();
                    else
                    {
                        if (OutrosArqTextBox.Enabled)
                            OutrosArqTextBox.Focus();
                        else
                        {
                            if (Teste1TextBox.Enabled)
                                Teste1TextBox.Focus();
                            else
                            {
                                if (Teste2TextBox.Enabled)
                                    Teste2TextBox.Focus();
                                else
                                    if (PITextBox.Enabled)
                                    PITextBox.Focus();
                                else
                                    gravarButton.Focus();
                            }
                        }
                    }
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndicadoCheckBox.Focus();
            }
        }

        private void DescricaoOutrosTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (CVTextBox.Enabled)
                    CVTextBox.Focus();
                else
                {
                    if (OutrosArqTextBox.Enabled)
                        OutrosArqTextBox.Focus();
                    else
                    {
                        if (Teste1TextBox.Enabled)
                            Teste1TextBox.Focus();
                        else
                        {
                            if (Teste2TextBox.Enabled)
                                Teste2TextBox.Focus();
                            else
                                if (PITextBox.Enabled)
                                PITextBox.Focus();
                            else
                                gravarButton.Focus();
                        }
                    }
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ContratadoCheckBox.Focus();
            }
        }

        private void CVTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Down)
            {
                if (e.KeyCode == Keys.Down)
                    Publicas._setaParaBaixo = true;

                if (OutrosArqTextBox.Enabled)
                    OutrosArqTextBox.Focus();
                else
                {
                    if (Teste1TextBox.Enabled)
                        Teste1TextBox.Focus();
                    else
                    {
                        if (Teste2TextBox.Enabled)
                            Teste2TextBox.Focus();
                        else
                            if (PITextBox.Enabled)
                            PITextBox.Focus();
                        else
                            gravarButton.Focus();
                    }
                }                
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (DescricaoOutrosTextBox.Enabled)
                    DescricaoOutrosTextBox.Focus();
                else
                    ContratadoCheckBox.Focus();
            }            
        }

        private void OutrosArqTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Down)
            {
                if (e.KeyCode == Keys.Down)
                    Publicas._setaParaBaixo = true;
                if (Teste1TextBox.Enabled)
                    Teste1TextBox.Focus();
                else
                {
                    if (Teste2TextBox.Enabled)
                        Teste2TextBox.Focus();
                    else
                        if (PITextBox.Enabled)
                        PITextBox.Focus();
                    else
                        gravarButton.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (CVTextBox.Enabled)
                    CVTextBox.Focus();
                else
                {
                    if (DescricaoOutrosTextBox.Enabled)
                        DescricaoOutrosTextBox.Focus();
                    else
                        ContratadoCheckBox.Focus();
                }
            }
        }

        private void Teste1TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Down)
            {
                if (e.KeyCode == Keys.Down)
                    Publicas._setaParaBaixo = true;
                if (Teste2TextBox.Enabled)
                    Teste2TextBox.Focus();
                else
                {
                    if (PITextBox.Enabled)
                        PITextBox.Focus();
                    else
                        gravarButton.Focus();
                }
                
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (OutrosArqTextBox.Enabled)
                    OutrosArqTextBox.Focus();
                else
                {
                    if (CVTextBox.Enabled)
                        CVTextBox.Focus();
                    else
                    {
                        if (DescricaoOutrosTextBox.Enabled)
                            DescricaoOutrosTextBox.Focus();
                        else
                            ContratadoCheckBox.Focus();
                    }
                }
            }
        }

        private void Teste2TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Down)
            {
                if (e.KeyCode == Keys.Down)
                    Publicas._setaParaBaixo = true;
                if (PITextBox.Enabled)
                    PITextBox.Focus();
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (Teste1TextBox.Enabled)
                    Teste1TextBox.Focus();
                else
                {
                    if (OutrosArqTextBox.Enabled)
                        OutrosArqTextBox.Focus();
                    else
                    {
                        if (CVTextBox.Enabled)
                            CVTextBox.Focus();
                        else
                        {
                            if (DescricaoOutrosTextBox.Enabled)
                                DescricaoOutrosTextBox.Focus();
                            else
                                ContratadoCheckBox.Focus();
                        }
                    }
                }
            }
        }

        private void PITextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (Teste2TextBox.Enabled)
                    Teste2TextBox.Focus();
                else
                {
                    if (Teste1TextBox.Enabled)
                        Teste1TextBox.Focus();
                    else
                    {
                        if (OutrosArqTextBox.Enabled)
                            OutrosArqTextBox.Focus();
                        else
                        {
                            if (CVTextBox.Enabled)
                                CVTextBox.Focus();
                            else
                            {
                                if (DescricaoOutrosTextBox.Enabled)
                                    DescricaoOutrosTextBox.Focus();
                                else
                                    ContratadoCheckBox.Focus();
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gravarButton.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (ContratadoCheckBox.Enabled)
                    ContratadoCheckBox.Focus();
                else
                {
                    if (DescricaoOutrosTextBox.Enabled)
                        DescricaoOutrosTextBox.Focus();
                    else
                        OutrosCheckBox.Focus();
                }
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

        private void nomeTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
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

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            codigoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                codigoTextBox.Text = string.Empty;
                pesquisaCategoriaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()))
            {
                new Pesquisas.Candidato().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _candidatos = new CurriculosBO().ConsultarCandidato(Convert.ToInt32(codigoTextBox.Text.Trim()));

            if (_candidatos.Existe)
            {
                nomeTextBox.Text = _candidatos.Nome;

                if (_candidatos.Telefone!= 0)
                    telefoneTextBox.Text = _candidatos.Telefone.ToString();

                if (_candidatos.Celular != 0)
                    CelularTextBox.Text = _candidatos.Celular.ToString();

                EmailTextBox.Text = _candidatos.Email;
                CathoCheckBox.Checked = _candidatos.Catho;
                InfoJobsCheckBox.Checked = _candidatos.Infojobs;
                LinkedinCheckBox.Checked = _candidatos.LinkedIn;
                OutrosCheckBox.Checked = _candidatos.Outros;
                IndicadoCheckBox.Checked = _candidatos.Indicado;
                ContratadoCheckBox.Checked = _candidatos.Contratado;

                DescricaoOutrosTextBox.Text = _candidatos.DescricaoOutros;
                _arquivos = new CurriculosBO().ListarArquivosDoCandidatos(_candidatos.Id);
                _historico = new CurriculosBO().ListaHistorico(_candidatos.Id);
                _candidatosDaVaga = new CurriculosBO().ConsultarVagasDaCandidato(_candidatos.Id);

                if (_candidatosDaVaga.Existe)
                {
                    _empresa = new EmpresaBO().Consultar(_candidatosDaVaga.IdEmpresa);

                    for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
                    {
                        empresaComboBoxAdv.SelectedIndex = i;
                        if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                        {
                            break;
                        }
                    }
                    VagasTextBox.Text = _candidatosDaVaga.IdVaga.ToString();

                    VagasTextBox_Validating(sender, new CancelEventArgs());
                }

                foreach (var item in _arquivos)
                {
                    if (item.Tipo == "CV")
                    {
                        VerCVPictureBox.Visible = true;
                        CVTextBox.Enabled = false;
                    }
                    if (item.Tipo == "T1")
                    {
                        Teste1PictureBox.Visible = true;
                        Teste1TextBox.Enabled = false;
                    }
                    if (item.Tipo == "T2")
                    {
                        Teste2PictureBox.Visible = true;
                        Teste2TextBox.Enabled = false;
                    }
                    if (item.Tipo == "PI")
                    {
                        PIPictureBox.Visible = true;
                        PITextBox.Enabled = false;
                    }
                    if (item.Tipo == "OU")
                    {
                        OutrosPictureBox.Visible = true;
                        OutrosArqTextBox.Enabled = false;
                    }
                }

                HistoricosGrid.DataSource = _historico;
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _candidatos.Existe;
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            nomeTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void EmailTextBox_Validating(object sender, CancelEventArgs e)
        {
            EmailTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void DescricaoOutrosTextBox_Validating(object sender, CancelEventArgs e)
        {
            DescricaoOutrosTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void OutrosArqTextBox_Validating(object sender, CancelEventArgs e)
        {
            OutrosArqTextBox.BorderColor = Publicas._bordaSaida;

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

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                new Notificacoes.Mensagem("Seleção cancelada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            OutrosArqTextBox.Text = openFileDialog1.FileName;
        }

        private void CVTextBox_Validating(object sender, CancelEventArgs e)
        {
            CVTextBox.BorderColor = Publicas._bordaSaida;

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

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                new Notificacoes.Mensagem("Seleção cancelada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            CVTextBox.Text = openFileDialog1.FileName;
        }

        private void Teste1TextBox_Validating(object sender, CancelEventArgs e)
        {
            Teste1TextBox.BorderColor = Publicas._bordaSaida;

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

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                new Notificacoes.Mensagem("Seleção cancelada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            Teste1TextBox.Text = openFileDialog1.FileName;
        }

        private void Teste2TextBox_Validating(object sender, CancelEventArgs e)
        {
            Teste2TextBox.BorderColor = Publicas._bordaSaida;

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

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                new Notificacoes.Mensagem("Seleção cancelada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            Teste2TextBox.Text = openFileDialog1.FileName;
        }

        private void PITextBox_Validating(object sender, CancelEventArgs e)
        {
            PITextBox.BorderColor = Publicas._bordaSaida;

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

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                new Notificacoes.Mensagem("Seleção cancelada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            PITextBox.Text = openFileDialog1.FileName;
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            HistoricosGrid.DataSource = new List<HistoricoDoCandidato>();

            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            telefoneTextBox.Text = string.Empty;
            CelularTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            DescricaoOutrosTextBox.Text = string.Empty;
            CVTextBox.Text = string.Empty;
            OutrosArqTextBox.Text = string.Empty;
            Teste1TextBox.Text = string.Empty;
            Teste2TextBox.Text = string.Empty;
            PITextBox.Text = string.Empty;
            CathoCheckBox.Checked = false;
            InfoJobsCheckBox.Checked = false;
            LinkedinCheckBox.Checked = false;
            OutrosCheckBox.Checked = false;
            IndicadoCheckBox.Checked = false;
            ContratadoCheckBox.Checked = false;
            codigoTextBox.Focus();

            VerCVPictureBox.Visible = false;
            Teste1PictureBox.Visible = false;
            Teste2PictureBox.Visible = false;
            PIPictureBox.Visible = false;
            OutrosPictureBox.Visible = false;
            CVTextBox.Enabled = true;
            OutrosArqTextBox.Enabled = true;
            PITextBox.Enabled = true;
            Teste1TextBox.Enabled = true;
            Teste2TextBox.Enabled = true;

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a código.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a descrição.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }
            
            _candidatos.Id = Convert.ToInt32(codigoTextBox.Text);
            _candidatos.Nome = nomeTextBox.Text;
            _candidatos.Telefone = Convert.ToDecimal(telefoneTextBox.ClipText.Trim());
            _candidatos.Celular = Convert.ToDecimal(CelularTextBox.ClipText.Trim());

            _candidatos.Email = EmailTextBox.Text;
            _candidatos.DescricaoOutros = DescricaoOutrosTextBox.Text;
            _candidatos.Catho = CathoCheckBox.Checked;
            _candidatos.Infojobs = InfoJobsCheckBox.Checked;
            _candidatos.LinkedIn = LinkedinCheckBox.Checked;
            _candidatos.Outros = OutrosCheckBox.Checked;
            _candidatos.Indicado = IndicadoCheckBox.Checked;
            _candidatos.Contratado = ContratadoCheckBox.Checked;

            int i = 1;
            string _arquivo;
            string _tipo;
            bool _encontrou;

            while (i <= 5)
            {
                switch (i)
                {
                    case 1: _arquivo = CVTextBox.Text;
                        _tipo = "CV";
                        break;
                    case 2:
                        _arquivo = OutrosArqTextBox.Text;
                        _tipo = "OU";
                        break;
                    case 3:
                        _arquivo = Teste1TextBox.Text;
                        _tipo = "T1";
                        break;
                    case 4:
                        _arquivo = Teste2TextBox.Text;
                        _tipo = "T2";
                        break;
                    case 5:
                        _arquivo = PITextBox.Text;
                        _tipo = "PI";
                        break;
                    default: _arquivo = "";
                        _tipo = "";
                        break;
                }
                
                if (_arquivo.Trim() != "")
                {
                    try
                    {
                        FileStream fs = new FileStream(_arquivo.Trim(), FileMode.Open, FileAccess.Read);

                        // Create a byte array of file stream length
                        byte[] ImageData = new byte[fs.Length];

                        //Read block of bytes from stream into the byte array
                        fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));

                        _encontrou = false;
                        foreach (var item in _arquivos.Where(w => w.Tipo == _tipo))
                        {
                            _encontrou = true;
                            item.Arquivo = ImageData;
                            item.Extensao = Path.GetExtension(_arquivo);
                        }

                        if (!_encontrou)
                        {
                            ArquivosDoCandidato _arqCandidato = new ArquivosDoCandidato();
                            _arqCandidato.Arquivo = ImageData;
                            _arqCandidato.Tipo = _tipo;
                            _arquivos.Add(_arqCandidato);
                            _arqCandidato.IdCandidato = _candidatos.Id;
                            _arqCandidato.Extensao = Path.GetExtension(_arquivo);
                        }

                        //Close the File Stream
                        fs.Dispose();
                        fs.Close();
                    }
                    catch
                    {

                    }
                }
                i++;
            }

            if (!_candidatos.Existe)
            {
                Classes.HistoricoDoCandidato _histo = new HistoricoDoCandidato();
                _histo.Status = "1";
                _histo.IdVaga = 0;
                _histo.IdCandidato = _candidatos.Id;

                if (_historico == null)
                    _historico = new List<HistoricoDoCandidato>();

                _historico.Add(_histo);
            }

            if (VagasTextBox.Text.Trim() != "" && _vagas != null && (_candidatosDaVaga == null || _candidatosDaVaga.IdVaga != _vagas.Id))
            {                
                _candidatosDaVaga = new CandidatosDaVaga();

                _candidatosDaVaga.IdCandidato = _candidatos.Id;
                _candidatosDaVaga.IdVaga = _vagas.Id;

                Classes.HistoricoDoCandidato _histo = new HistoricoDoCandidato();
                _histo.Status = "2";
                _histo.IdVaga = _vagas.Id;
                _histo.IdCandidato = _candidatos.Id;

                if (_historico == null)
                    _historico = new List<HistoricoDoCandidato>();

                _historico.Add(_histo);
            }

            if (!new CurriculosBO().GravarCandidato(_candidatos, _arquivos, _candidatosDaVaga, _historico))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new CurriculosBO().ExcluirCandidato(_candidatos.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new CurriculosBO().ProximoCandidato().ToString();
            nomeTextBox.Focus();
        }

        private void OutrosCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IndicadoCheckBox.Enabled = OutrosCheckBox.Checked;
            DescricaoOutrosTextBox.Enabled = OutrosCheckBox.Checked;
        }

        private string CriarArquivo(string tipo)
        {
            string _nomeArquivo = "";
            if (!System.IO.Directory.Exists("C:\\Temp\\"))
                System.IO.Directory.CreateDirectory("C:\\Temp\\");

            foreach (var item in _arquivos.Where(w => w.Tipo == tipo))
            {
                using (FileStream fs = new FileStream
                                ("C:\\Temp\\" + nomeTextBox.Text.Split(' ')[0] + "." + item.Extensao, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(item.Arquivo, 0, item.Arquivo.Length);

                    _nomeArquivo = "C:\\Temp\\" + nomeTextBox.Text.Split(' ')[0] + "." + item.Extensao;
                }
            }
            return _nomeArquivo;
        }

        private void VerCVPictureBox_Click(object sender, EventArgs e)
        {
            string _nomeArquivo = "";
            
            if (CVTextBox.Text.Trim() != "")
                _nomeArquivo = CVTextBox.Text;
            else
            {
                try
                {

                    _nomeArquivo = CriarArquivo("CV");
                }
                catch { }

            }
            
            System.Diagnostics.Process.Start(_nomeArquivo);
        }

        private void OutrosPictureBox_Click(object sender, EventArgs e)
        {
            string _nomeArquivo = "";

            if (OutrosArqTextBox.Text.Trim() != "")
                _nomeArquivo = OutrosArqTextBox.Text;
            else
            {
                try
                {
                    _nomeArquivo = CriarArquivo("OU");
                }
                catch { }
            }

            System.Diagnostics.Process.Start(_nomeArquivo);
        }

        private void Teste1PictureBox_Click(object sender, EventArgs e)
        {
            string _nomeArquivo = "";
            
            if (Teste1TextBox.Text.Trim() != "")
                _nomeArquivo = Teste1TextBox.Text;
            else
            {
                try
                {
                    _nomeArquivo = CriarArquivo("T1");
                }
                catch { }

            }

            System.Diagnostics.Process.Start(_nomeArquivo);
        }

        private void Teste2PictureBox_Click(object sender, EventArgs e)
        {
            string _nomeArquivo = "";
            
            if (Teste2TextBox.Text.Trim() != "")
                _nomeArquivo = Teste2TextBox.Text;
            else
            {
                try
                {
                    _nomeArquivo = CriarArquivo("T2");
                }
                catch { }
            }

            System.Diagnostics.Process.Start(_nomeArquivo);
        }

        private void PIPictureBox_Click(object sender, EventArgs e)
        {
            string _nomeArquivo = "";

            if (PITextBox.Text.Trim() != "")
                _nomeArquivo = PITextBox.Text;
            else
            {
                try
                {
                    _nomeArquivo = CriarArquivo("PI");
                }
                catch { }                
            }

            System.Diagnostics.Process.Start(_nomeArquivo);
        }

        private void PIPictureBox_MouseHover(object sender, EventArgs e)
        {
            ((PictureBox)sender).Cursor = Cursors.Hand;
            ((PictureBox)sender).BackColor = Color.Silver; 
        }

        private void PIPictureBox_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).Cursor = Cursors.Default;
            ((PictureBox)sender).BackColor = CVTextBox.BackColor;
        }

        private void Candidatos_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

            for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
            {
                empresaComboBoxAdv.SelectedIndex = i;
                if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                {
                    break;
                }
            }

            HistoricosGrid.SortIconPlacement = SortIconPlacement.Left;
            HistoricosGrid.TopLevelGroupOptions.ShowFilterBar = false;
            HistoricosGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            HistoricosGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            HistoricosGrid.TableControl.CellToolTip.Active = true;
            HistoricosGrid.RecordNavigationBar.Label = "Histórico";

            for (int i = 0; i < HistoricosGrid.TableDescriptor.Columns.Count; i++)
            {
                HistoricosGrid.TableDescriptor.Columns[i].AllowSort = true;
            }

            if (!Publicas._TemaBlack)
            {
                this.HistoricosGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.HistoricosGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            HistoricosGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            HistoricosGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            HistoricosGrid.Table.DefaultRecordRowHeight = 30;
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

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                VagasTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CathoCheckBox.Focus();
            }
        }

        private void VagasTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void VagasButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(VagasTextBox.Text.Trim()))
            {
                new Pesquisas.Vaga().ShowDialog();

                VagasTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(VagasTextBox.Text) || VagasTextBox.Text == "0")
                {
                    VagasTextBox.Text = string.Empty;
                    VagasTextBox.Focus();
                    return;
                }
            }

            codigoTextBox_Validating(sender, new CancelEventArgs());
        }

        private void VagasTextBox_Validating(object sender, CancelEventArgs e)
        {
            VagasTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                VagasTextBox.Text = string.Empty;
                PesquisaVagasButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }
            if (Publicas._setaParaBaixo)
            {
                VagasTextBox.Text = string.Empty;
                PesquisaVagasButton.Enabled = false;
                Publicas._setaParaBaixo = false;
                return;
            }

            if (string.IsNullOrEmpty(VagasTextBox.Text.Trim()))
            {
                new Pesquisas.Vaga().ShowDialog();

                VagasTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(VagasTextBox.Text) || VagasTextBox.Text == "0")
                {
                    VagasTextBox.Text = string.Empty;
                    VagasTextBox.Focus();
                    return;
                }
            }

            _vagas = new CurriculosBO().ConsultarVaga(Convert.ToInt32(VagasTextBox.Text.Trim()));

            if (!_vagas.Existe)
            {
                new Notificacoes.Mensagem("Vaga não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                VagasTextBox.Focus();
                return;
            }

            DescricaoVagaTextBox.Text = _vagas.Descricao;

            if (_vagas.Status != "A")
            {
                if (((TextBoxExt)sender).Name == "VagasTextBox")
                {
                    new Notificacoes.Mensagem("Vaga encerrada ou congelada.", Publicas.TipoMensagem.Alerta).ShowDialog();

                    VagasTextBox.Focus();
                    return;
                }
                else
                {
                    DescricaoVagaTextBox.Text = "";
                    VagasTextBox.Text = "";
                }
            }            
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void VagasTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Down)
            {
                gravarButton.Focus();
                Publicas._setaParaBaixo = true;
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void VagasTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaVagasButton.Enabled = string.IsNullOrEmpty(VagasTextBox.Text.Trim());
        }

        private void semContatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_candidatos.Existe)
            {
                Classes.HistoricoDoCandidato _histo = new HistoricoDoCandidato();
                _histo.Status = "4";
                _histo.IdVaga = _vagas.Id; 
                _histo.IdCandidato = _candidatos.Id;
                
                HistoricosGrid.DataSource = new List<HistoricoDoCandidato>(); 

                List<HistoricoDoCandidato> _lista = new List<HistoricoDoCandidato>();

                _lista.Add(_histo);

                new CurriculosBO().GravarHistorico(_lista);

                _historico = new CurriculosBO().ListaHistorico(_candidatos.Id);
                HistoricosGrid.DataSource = _historico;
            }
        }

        private void reprovadoPeloGestorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_candidatos.Existe)
            {
                Classes.HistoricoDoCandidato _histo = new HistoricoDoCandidato();
                _histo.Status = "10";
                _histo.IdVaga = _vagas.Id; 
                _histo.IdCandidato = _candidatos.Id;

                HistoricosGrid.DataSource = new List<HistoricoDoCandidato>();

                List<HistoricoDoCandidato> _lista = new List<HistoricoDoCandidato>();

                _lista.Add(_histo);

                new CurriculosBO().GravarHistorico(_lista);

                _historico = new CurriculosBO().ListaHistorico(_candidatos.Id);
                HistoricosGrid.DataSource = _historico;
            }
        }

        private void aprovadoPeloGestorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_candidatos.Existe)
            {
                Classes.HistoricoDoCandidato _histo = new HistoricoDoCandidato();
                _histo.Status = "5";  
                _histo.IdVaga = _vagas.Id; 
                _histo.IdCandidato = _candidatos.Id;

                HistoricosGrid.DataSource = new List<HistoricoDoCandidato>();

                List<HistoricoDoCandidato> _lista = new List<HistoricoDoCandidato>();

                _lista.Add(_histo);

                new CurriculosBO().GravarHistorico(_lista);

                _historico = new CurriculosBO().ListaHistorico(_candidatos.Id);
                HistoricosGrid.DataSource = _historico;
            }
        }

        private void agendarEntrevistaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessoSeletivo.Agendamento _tela = new Agendamento();
            _tela.candidatoTextBox.Text = this.codigoTextBox.Text;
            _tela.nomeTextBox.Text = this.nomeTextBox.Text;
            _tela.EmailTextBox.Text = this.EmailTextBox.Text;
            _tela.VagasTextBox.Text = this.VagasTextBox.Text;
            _tela.DescricaoVagaTextBox.Text = this.DescricaoVagaTextBox.Text;
            _tela.ReagendamentoCheckBox.Visible = true;
            _tela.CancelamentoCheckBox.Visible = false;
            _tela.ShowDialog();

            HistoricosGrid.DataSource = new List<HistoricoDoCandidato>();
            _historico = new CurriculosBO().ListaHistorico(_candidatos.Id);
            HistoricosGrid.DataSource = _historico;
        }

        private void cancelarEntrevistaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessoSeletivo.Agendamento _tela = new Agendamento();
            _tela.candidatoTextBox.Text = this.codigoTextBox.Text;
            _tela.nomeTextBox.Text = this.nomeTextBox.Text;
            _tela.EmailTextBox.Text = this.EmailTextBox.Text;
            _tela.VagasTextBox.Text = this.VagasTextBox.Text;
            _tela.DescricaoVagaTextBox.Text = this.DescricaoVagaTextBox.Text;
            _tela.CancelamentoCheckBox.Location = new Point(_tela.ReagendamentoCheckBox.Left, _tela.ReagendamentoCheckBox.Top);
            _tela.ReagendamentoCheckBox.Visible = false;
            _tela.CancelamentoCheckBox.Visible = true;
            _tela.CancelamentoCheckBox.Checked = true;
            _tela.CancelamentoCheckBox.Enabled = false;
            _tela.DataTextBox.Enabled = false;
            _tela.ShowDialog();

            HistoricosGrid.DataSource = new List<HistoricoDoCandidato>();
            _historico = new CurriculosBO().ListaHistorico(_candidatos.Id);
            HistoricosGrid.DataSource = _historico;
        }

        private void telefoneTextBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void telefoneTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void reprovadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_candidatos.Existe)
            {
                Classes.HistoricoDoCandidato _histo = new HistoricoDoCandidato();
                _histo.Status = "16";
                _histo.IdVaga = _vagas.Id;
                _histo.IdCandidato = _candidatos.Id;

                HistoricosGrid.DataSource = new List<HistoricoDoCandidato>();

                List<HistoricoDoCandidato> _lista = new List<HistoricoDoCandidato>();

                _lista.Add(_histo);

                new CurriculosBO().GravarHistorico(_lista);

                _historico = new CurriculosBO().ListaHistorico(_candidatos.Id);
                HistoricosGrid.DataSource = _historico;
            }
        }

        private void aprovadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_candidatos.Existe)
            {
                Classes.HistoricoDoCandidato _histo = new HistoricoDoCandidato();
                _histo.Status = "15";
                _histo.IdVaga = _vagas.Id;
                _histo.IdCandidato = _candidatos.Id;

                HistoricosGrid.DataSource = new List<HistoricoDoCandidato>();

                List<HistoricoDoCandidato> _lista = new List<HistoricoDoCandidato>();

                _lista.Add(_histo);

                new CurriculosBO().GravarHistorico(_lista);

                _historico = new CurriculosBO().ListaHistorico(_candidatos.Id);
                HistoricosGrid.DataSource = _historico;
            }
        }
    }
}
