using Classes;
using Negocio;
using Suportte.Notificacoes;
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
    public partial class Feedback : Form
    {
        public Feedback()
        {
            InitializeComponent();

            dataLimiteDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataLimiteDateTimePicker.BackColor = usuarioTextBox.BackColor;
            dataLimiteDateTimePicker.Value = DateTime.Now;

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
        Classes.Empresa _empresa;
        Classes.Colaboradores _colaboradores;
        Classes.Colaboradores _colaboradoresSuperior;
        Classes.Prazos _prazo;
        Classes.AutoAvaliacao _autoAvaliacao;
        Classes.FuncionariosGlobus _funcionariosGlobus;
        Classes.Cargos _cargos;

        List<Classes.Empresa> _listaEmpresas;
        public Publicas.TipoPrazos tipoAvaliacao;
        string tipo;

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

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Feedback_Shown(object sender, EventArgs e)
        {
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

            tipo = (tipoAvaliacao == Publicas.TipoPrazos.AutoAvaliacao ? "AA" :
                         (tipoAvaliacao == Publicas.TipoPrazos.FeedbackGestor ? "FG" :
                         (tipoAvaliacao == Publicas.TipoPrazos.MetasNumericas ? "MN" :
                         (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoDoGestor ? "AG" :
                         (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoRH ? "AR" : "FA")))));

            _colaboradoresSuperior = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas._usuario.RegistroFuncionario, false);

            label6.Visible = tipoAvaliacao == Publicas.TipoPrazos.FeedbackAvaliado;
            comentarioTextBox.Visible = tipoAvaliacao == Publicas.TipoPrazos.FeedbackAvaliado || tipoAvaliacao == Publicas.TipoPrazos.RHConsultaFeedback;
             

            if (usuarioTextBox.Enabled)
            {
                gravarButton.Location = new Point(239, 14);
                limparButton.Location = new Point(376, 14);
            }
            else// Carrega quando auto avaliação
            {
                gravarButton.Location = new Point(308, 14);
                limparButton.Visible = false;
                
                usuarioTextBox.Text = Publicas._usuario.RegistroFuncionario;

                _colaboradores = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, usuarioTextBox.Text, false);
                nomeTextBox.Text = _colaboradores.Nome;

                _cargos = new CargosBO().Consultar(_colaboradores.IdCargo);
                descricaoCargoTextBox.Text = _cargos.Descricao;                
            }
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

        private void usuarioTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void dataDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
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
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                usuarioTextBox.Text = string.Empty;
                pesquisaUsuarioButton.Enabled = false;
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

            _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(usuarioTextBox.Text, _empresa.CodigoEmpresaGlobus);

            _colaboradores = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, usuarioTextBox.Text, false);

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

            if (_colaboradores.IdSupervisor != _colaboradoresSuperior.Id && tipoAvaliacao == Publicas.TipoPrazos.FeedbackGestor && Publicas._usuario.UsuarioAcesso != "VDALMEIDA")
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado na sua equipe.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _funcionariosGlobus.Nome;

            _cargos = new CargosBO().Consultar(_colaboradores.IdCargo);
            descricaoCargoTextBox.Text = _cargos.Descricao;
        }
        
        private void FeedbackGestorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (comentarioTextBox.Visible)
                    comentarioTextBox.Focus();
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void comentarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FeedbackGestorTextBox.Focus();
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (tipoAvaliacao == Publicas.TipoPrazos.FeedbackGestor && string.IsNullOrEmpty(FeedbackGestorTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe o seu feedback.", Publicas.TipoMensagem.Alerta).ShowDialog();
                FeedbackGestorTextBox.Focus();
                return;
            }
            
            if (tipoAvaliacao == Publicas.TipoPrazos.FeedbackAvaliado && string.IsNullOrEmpty(comentarioTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe o seu comentário.", Publicas.TipoMensagem.Alerta).ShowDialog();
                comentarioTextBox.Focus();
                return;
            }

            if (tipoAvaliacao == Publicas.TipoPrazos.FeedbackGestor)
            {
                if (!new AutoAvaliacaoBO().FeedBackGestor(_autoAvaliacao.Id, FeedbackGestorTextBox.Text))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }
            else
            {
                if (!new AutoAvaliacaoBO().FeedBackColaborador(_autoAvaliacao.Id, comentarioTextBox.Text))
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }

            if (!empresaComboBoxAdv.Enabled)
                Close();
            else
                limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            dataLimiteDateTimePicker.Value = DateTime.Now;
            referenciaMaskedEditBox.Text = string.Empty;
            descricaoCargoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            usuarioTextBox.Text = string.Empty;
            descricaoCargoTextBox.Text = string.Empty;
            FeedbackGestorTextBox.Text = string.Empty;
            comentarioTextBox.Text = string.Empty;
            pesquisaReferenciaButton.Enabled = false;
            pesquisaUsuarioButton.Enabled = false;

            usuarioTextBox.Focus();
        }

        private void comentarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
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

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                FeedbackGestorTextBox.Focus();
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

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {

            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaReferenciaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                Publicas._idSuperior = _colaboradores.Id;

                new Pesquisas.Feedback("AA").ShowDialog();

                referenciaMaskedEditBox.Text = Publicas._idRetornoPesquisa.ToString("000000");

                if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()) || referenciaMaskedEditBox.ClipText == "000000")
                {
                    referenciaMaskedEditBox.Text = string.Empty;
                    referenciaMaskedEditBox.Focus();
                    return;
                }
            }

            // O feedback é apenas para a auto avaliação 
            _autoAvaliacao = new AutoAvaliacaoBO().Consultar(_colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), "AA", _empresa.IdEmpresa);

            if (_autoAvaliacao.Existe)
            {
                _prazo = new PrazosBO().Consultar(_autoAvaliacao.DataInicio, tipo);
                FeedbackGestorTextBox.Text = _autoAvaliacao.FeedbackGestor;
                comentarioTextBox.Text = _autoAvaliacao.Comentario;
            }
            else
            {
                new Notificacoes.Mensagem("Nenhuma " + Publicas.GetDescription(Publicas.TipoPrazos.AutoAvaliacao, "") + " efetuada para esse colaborador.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            _prazo = new PrazosBO().Consultar(DateTime.Now.Date, tipo, referenciaMaskedEditBox.ClipText.Trim());

            if (!_prazo.Existe)
            {
                new Notificacoes.Mensagem("Feedback não cadastrado para essa referência.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            if (_prazo.Fim.Date < DateTime.Now.Date && tipoAvaliacao != Publicas.TipoPrazos.RHConsultaFeedback)
            {
                new Notificacoes.Mensagem("Prazo para o " + Publicas.GetDescription(tipoAvaliacao, "") + " finalizado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            if (tipoAvaliacao == Publicas.TipoPrazos.FeedbackAvaliado && string.IsNullOrEmpty(_autoAvaliacao.FeedbackGestor))
            {
                new Notificacoes.Mensagem("Seu gestor ainda não escreveu seu Feedback.", Publicas.TipoMensagem.Alerta).ShowDialog();
                referenciaMaskedEditBox.Focus();
                return;
            }

            gravarButton.Enabled = Publicas.TipoPrazos.RHConsultaFeedback != tipoAvaliacao;
            FeedbackGestorTextBox.ReadOnly = !gravarButton.Enabled;
            comentarioTextBox.ReadOnly = !gravarButton.Enabled;

        }

        private void pesquisaReferenciaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                Publicas._idSuperior = _colaboradores.Id;

                new Pesquisas.Feedback("AA").ShowDialog();

                referenciaMaskedEditBox.Text = Publicas._idRetornoPesquisa.ToString("000000");

                if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()) || referenciaMaskedEditBox.ClipText == "000000")
                {
                    referenciaMaskedEditBox.Text = string.Empty;
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                referenciaMaskedEditBox_Validating(sender, new CancelEventArgs());
            }
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
