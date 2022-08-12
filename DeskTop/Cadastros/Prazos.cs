using Classes;
using Negocio;
using Suportte.Notificacoes;
using Syncfusion.Windows.Forms;
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
    public partial class Prazos : Form
    {
        public Prazos()
        {
            InitializeComponent();

            dataInicioDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataInicioDateTimePicker.BackColor = referenciaMaskedEditBox.BackColor;
            dataFimDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataFimDateTimePicker.BackColor = referenciaMaskedEditBox.BackColor;
            
            dataInicioDateTimePicker.Value = DateTime.Now;
            dataFimDateTimePicker.Value = DateTime.Now;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                {
                    dataInicioDateTimePicker.Style = VisualStyle.Office2016Black;
                    dataFimDateTimePicker.Style = VisualStyle.Office2016Black;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Prazos _prazos = new Classes.Prazos();
        bool _pesquisa = false;
        string[] _dados = new string[5];

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

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            referenciaMaskedEditBox.BorderColor = Publicas._bordaEntrada;
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }

        private void tipoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;

            if (referenciaMaskedEditBox.ClipText.Trim() == "")
                ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void dataInicioDateTimePicker_Enter(object sender, EventArgs e)
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

        private void tipoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataInicioDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                direcaoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataFimDateTimePicker.Focus();
            }
        }

        private void dataInicioDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataFimDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tipoComboBox.Focus();
            }
        }

        private void dataFimDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataInicioDateTimePicker.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                outrosCheckBox.Focus();
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

        private void Prazos_Shown(object sender, EventArgs e)
        {
            tipoComboBox.Items.AddRange(new object[] { "Auto Avaliação", "Feedback do Gestor", "Metas numéricas", "Avaliação do Gestor", "Avaliação do RH", "Feedback do Avaliado", "Plano de Desenvolvimento individual" });
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

            if (referenciaMaskedEditBox.ClipText.Trim() != "")
                tipoComboBox.Text = string.Empty;
            else
            {
                if (!_pesquisa)
                    new Pesquisas.Prazos().ShowDialog();

                if (Publicas._idRetornoPesquisa == 0)
                {
                    _pesquisa = !_pesquisa;
                    referenciaMaskedEditBox.Text = string.Empty;
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                _prazos = new PrazosBO().Consultar(0, Publicas.TipoPrazos.SemSelecao, Publicas._idRetornoPesquisa);

                referenciaMaskedEditBox.Text = _prazos.Referencia;
                tipoComboBox.SelectedIndex = (_prazos.Tipo == Publicas.TipoPrazos.AutoAvaliacao ? 0 :
                    (_prazos.Tipo == Publicas.TipoPrazos.FeedbackGestor ? 1 :
                    (_prazos.Tipo == Publicas.TipoPrazos.MetasNumericas ? 2 :
                    (_prazos.Tipo == Publicas.TipoPrazos.AvaliacaoDoGestor ? 3 :
                    (_prazos.Tipo == Publicas.TipoPrazos.AvaliacaoRH ? 4 :
                    (_prazos.Tipo == Publicas.TipoPrazos.FeedbackAvaliado ? 5 : 6))))));

                ativoCheckBox.Checked = _prazos.Ativo;
                dataInicioDateTimePicker.Value = _prazos.Inicio;
                dataFimDateTimePicker.Value = _prazos.Fim;
                dataInicioDateTimePicker.Focus();
                excluirButton.Enabled = _prazos.Existe;
                gravarButton.Enabled = true;
            }
            _pesquisa = false;

        }

        private void tipoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                tipoComboBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }
            
            if (tipoComboBox.SelectedIndex == -1)
            {
                new Notificacoes.Mensagem("Tipo deve ser informado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tipoComboBox.Focus();
                return;
            }

            Publicas.TipoPrazos _tipo = (tipoComboBox.SelectedIndex == 0 ? Publicas.TipoPrazos.AutoAvaliacao :
                                     (tipoComboBox.SelectedIndex == 1 ? Publicas.TipoPrazos.FeedbackGestor :
                                     (tipoComboBox.SelectedIndex == 2 ? Publicas.TipoPrazos.MetasNumericas :
                                     (tipoComboBox.SelectedIndex == 3 ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                     (tipoComboBox.SelectedIndex == 4 ? Publicas.TipoPrazos.AvaliacaoRH :
                                     (tipoComboBox.SelectedIndex == 5 ? Publicas.TipoPrazos.FeedbackAvaliado : 
                                     Publicas.TipoPrazos.PlanoDeDesenvolvimento))))));

            _prazos = new PrazosBO().Consultar(Convert.ToInt32(referenciaMaskedEditBox.ClipText), _tipo, 0);

            novoLabel.Visible = false;
            if (_prazos.Existe)
            {
                ativoCheckBox.Checked = _prazos.Ativo;
                dataInicioDateTimePicker.Value = _prazos.Inicio;
                dataFimDateTimePicker.Value = _prazos.Fim;

                direcaoCheckBox.Checked = _prazos.EnvioEmail.Contains("D,");
                gerenciaCheckBox.Checked = _prazos.EnvioEmail.Contains("G,");
                coordenacaoCheckBox.Checked = _prazos.EnvioEmail.Contains("C,");
                outrosCheckBox.Checked = _prazos.EnvioEmail.Contains("O,");
            }

            novoLabel.Visible = !_prazos.Existe;
            excluirButton.Enabled = _prazos.Existe;
            gravarButton.Enabled = true;
            dataInicioDateTimePicker.Focus();
        }

        private void dataInicioDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (dataInicioDateTimePicker.Value > dataFimDateTimePicker.Value)
            {
                new Notificacoes.Mensagem("Data inicio não pode ser superior a data final." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                dataInicioDateTimePicker.Focus();
                return;
            }

            if (dataFimDateTimePicker.Value.Year < DateTime.Now.Year)
            {
                new Notificacoes.Mensagem("Ano deve ser igual ou superior ao ano atual." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                dataFimDateTimePicker.Focus();
                return;
            }

            if (dataInicioDateTimePicker.Value.Year < DateTime.Now.Year)
            {
                new Notificacoes.Mensagem("Ano deve ser igual ou superior ao ano atual." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                dataInicioDateTimePicker.Focus();
                return;
            }            
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            referenciaMaskedEditBox.Text = string.Empty;
            tipoComboBox.Text = string.Empty;
            tipoComboBox.SelectedIndex = -1;
            ativoCheckBox.Checked = false;
            dataInicioDateTimePicker.Value = DateTime.Now;
            dataFimDateTimePicker.Value = DateTime.Now;

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            novoLabel.Visible = false;

            referenciaMaskedEditBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new PrazosBO().Excluir(_prazos))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tipoComboBox.Text))
            {
                new Notificacoes.Mensagem("Informe o tipo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                tipoComboBox.Focus();
                return;
            }

            _prazos.Referencia = referenciaMaskedEditBox.ClipText;
            _prazos.Ativo = ativoCheckBox.Checked;
            _prazos.Tipo = (tipoComboBox.SelectedIndex == 0 ? Publicas.TipoPrazos.AutoAvaliacao :
                                     (tipoComboBox.SelectedIndex == 1 ? Publicas.TipoPrazos.FeedbackGestor :
                                     (tipoComboBox.SelectedIndex == 2 ? Publicas.TipoPrazos.MetasNumericas :
                                     (tipoComboBox.SelectedIndex == 3 ? Publicas.TipoPrazos.AvaliacaoDoGestor :
                                     (tipoComboBox.SelectedIndex == 4 ? Publicas.TipoPrazos.AvaliacaoRH :
                                     (tipoComboBox.SelectedIndex == 5 ? Publicas.TipoPrazos.FeedbackAvaliado : 
                                     Publicas.TipoPrazos.PlanoDeDesenvolvimento))))));

            _prazos.Inicio = dataInicioDateTimePicker.Value;
            _prazos.Fim = dataFimDateTimePicker.Value;
            _prazos.EnvioEmail = (direcaoCheckBox.Checked ? "D," : "") +
                 (gerenciaCheckBox.Checked ? "G," : "") +
                  (coordenacaoCheckBox.Checked ? "C," : "") +
                   (outrosCheckBox.Checked ? "O," : "");

            if (!new PrazosBO().Gravar(_prazos))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            // disparar e-mail se data atual estiver dentro inicio e fim 
            if (DateTime.Now.Date >= _prazos.Inicio && DateTime.Now.Date <= _prazos.Fim && _prazos.Ativo &&
                (tipoComboBox.SelectedIndex == 0 || tipoComboBox.SelectedIndex == 1 || tipoComboBox.SelectedIndex == 3 || tipoComboBox.SelectedIndex == 5))
            {
                string tipo = _prazos.EnvioEmail = (direcaoCheckBox.Checked ? "'D'," : "") +
                 (gerenciaCheckBox.Checked ? "'G'," : "") +
                  (coordenacaoCheckBox.Checked ? "'C'," : "") +
                   (outrosCheckBox.Checked ? "'O'," : "");

                if (tipo != "")
                {
                    tipo = tipo.Substring(0, tipo.Length - 1);

                    List<Colaboradores> _lista = new ColaboradoresBO().Listar(tipo);


                    mensagemSistemaLabel.Text = "Aguarde. Enviando e-mail ...";
                    gravarButton.Enabled = false;
                    excluirButton.Enabled = false;
                    this.Refresh();
                    foreach (var item in _lista.Where(w => !string.IsNullOrEmpty(w.Email.Trim()) && w.ParticipaDaAvaliacao))
                    {
                        _dados[0] = tipoComboBox.Text;
                        _dados[1] = (tipoComboBox.SelectedIndex == 0 || tipoComboBox.SelectedIndex == 3 ? "da " : "do ") +
                            tipoComboBox.Text;
                        _dados[2] = _prazos.Fim.ToShortDateString();
                        _dados[3] = item.Nome;
                        _dados[4] = (tipoComboBox.SelectedIndex == 0 ? "leia as orientações e faça sua auto avaliação" :
                            (tipoComboBox.SelectedIndex == 1 ? "de seu feedback ao seus colaboradores" :
                            (tipoComboBox.SelectedIndex == 3 ? "leia as orientações e faça a avaliação das lideranças" :
                            (tipoComboBox.SelectedIndex == 5 ? "de seu feedback ao seu gestor" : ""))));


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

            limparButton_Click(sender, e);
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tipoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataInicioDateTimePicker.Enabled = tipoComboBox.SelectedIndex != -1;
            dataFimDateTimePicker.Enabled = tipoComboBox.SelectedIndex != -1;
            ativoCheckBox.Enabled = tipoComboBox.SelectedIndex != -1;
        }

        private void referenciaMaskedEditBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }

        private void pesquisaUsuarioButton_Click(object sender, EventArgs e)
        {
            if (!_pesquisa)
                new Pesquisas.Prazos().ShowDialog();

            if (Publicas._idRetornoPesquisa == 0)
            {
                _pesquisa = !_pesquisa;
                referenciaMaskedEditBox.Text = string.Empty;
                referenciaMaskedEditBox.Focus();
                return;
            }

            _prazos = new PrazosBO().Consultar(0, Publicas.TipoPrazos.SemSelecao, Publicas._idRetornoPesquisa);

            referenciaMaskedEditBox.Text = _prazos.Referencia;
            tipoComboBox.SelectedIndex = (_prazos.Tipo == Publicas.TipoPrazos.AutoAvaliacao ? 0 :
                (_prazos.Tipo == Publicas.TipoPrazos.FeedbackGestor ? 1 :
                (_prazos.Tipo == Publicas.TipoPrazos.MetasNumericas ? 2 :
                (_prazos.Tipo == Publicas.TipoPrazos.AvaliacaoDoGestor ? 3 :
                (_prazos.Tipo == Publicas.TipoPrazos.AvaliacaoRH ? 4 :
                (_prazos.Tipo == Publicas.TipoPrazos.FeedbackAvaliado ? 5 : 6))))));

            ativoCheckBox.Checked = _prazos.Ativo;
            dataInicioDateTimePicker.Value = _prazos.Inicio;
            dataFimDateTimePicker.Value = _prazos.Fim;
            dataInicioDateTimePicker.Focus();
            excluirButton.Enabled = _prazos.Existe;
            gravarButton.Enabled = true;
            tipoComboBox.Focus();
        }

        private void direcaoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gerenciaCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void gerenciaCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                coordenacaoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                direcaoCheckBox.Focus();
            }
        }

        private void coordenacaoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                outrosCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gerenciaCheckBox.Focus();
            }
        }

        private void outrosCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                coordenacaoCheckBox.Focus();
            }
        }

        private void statusButton_Click(object sender, EventArgs e)
        {
            Avaliacao_de_desempenho.StatusPrazos _tela = new Avaliacao_de_desempenho.StatusPrazos();
            _tela.tituloLabel.Text = "Andamento " + (tipoComboBox.SelectedIndex == 0 || tipoComboBox.SelectedIndex == 3 ? "da " : "do ") + tipoComboBox.Text;
            _tela.nomeTextBox.Text = tipoComboBox.Text;

            _tela.direcaoCheckBox.Checked = direcaoCheckBox.Checked;
            _tela.gerenciaCheckBox.Checked = gerenciaCheckBox.Checked;
            _tela.coordenacaoCheckBox.Checked = coordenacaoCheckBox.Checked;
            _tela.outrosCheckBox.Checked = outrosCheckBox.Checked;

            _tela.dataInicioDateTimePicker.Value = dataInicioDateTimePicker.Value;
            _tela.dataFimDateTimePicker.Value = dataFimDateTimePicker.Value;

            _tela.mesReferencia = Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim());

            _tela.tipoAvaliacao = (tipoComboBox.SelectedIndex == 0 ? "AA" : // AutoAvaliacao
                                  (tipoComboBox.SelectedIndex == 1 ? "FG" : // Feedback Gestor
                                  (tipoComboBox.SelectedIndex == 2 ? "MN" : // Metas Numericas
                                  (tipoComboBox.SelectedIndex == 3 ? "AG" : // Avaliação Gestos
                                  (tipoComboBox.SelectedIndex == 4 ? "AR" : // Avaliação RH
                                  (tipoComboBox.SelectedIndex == 5 ? "FA" : // FeedBack Avaliado
                                  "PD" // Plano de desenvolvimento
                                  ))))));

            _dados[0] = tipoComboBox.Text;
            _dados[1] = (tipoComboBox.SelectedIndex == 0 || tipoComboBox.SelectedIndex == 3 ? "da " : "do ") +
                tipoComboBox.Text;
            _dados[2] = _prazos.Fim.ToShortDateString();
            _dados[4] = (tipoComboBox.SelectedIndex == 0 ? "leia as orientações e faça sua auto avaliação" :
                (tipoComboBox.SelectedIndex == 1 ? "de seu feedback ao seus colaboradores" :
                (tipoComboBox.SelectedIndex == 3 ? "leia as orientações e faça a avaliação das lideranças" :
                (tipoComboBox.SelectedIndex == 5 ? "de seu feedback ao seu gestor" : ""))));

            _tela._dados = _dados;

            _tela.ShowDialog();
        }
    }
}
