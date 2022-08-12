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

namespace Suportte.SAC
{
    public partial class Retornar : Form
    {
        public Retornar()
        {
            InitializeComponent();

            aberturaDateTimePicker.BorderColor = Publicas._bordaSaida;
            retornoDateTimePicker.BorderColor = Publicas._bordaSaida;
            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Empresa _empresa;
        Atendimento _atendimento;
        TipoDeAtendimentoEMTU _tipoEMTU;
        TipoDeAtendimento _tipoAtendimento;
        Usuario _usuarioAbertura;
        
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

        #region KeyDown
        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                retornoDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void retornoDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                finalizaSemRetornoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoEmtuTextBox.Focus();
            }
        }

        private void finalizaSemRetornoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                retornoDateTimePicker.Focus();
            }
        }

        #endregion

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(_empresa.Separador))
                e.Handled = false;
        }

        private void Retornar_Load(object sender, EventArgs e)
        {
            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

            List<TipoDeAtendimento> _lista = new TipoDeAtendimentoBO().Listar();
            tipoAcessoComboBox.DataSource = _lista;
            tipoAcessoComboBox.DisplayMember = "Descricao";
            tipoAcessoComboBox.SelectedIndex = 0;

            statusComboBox.Items.AddRange(new object[] { "Ativo", "Cancelado", "Respondido", "Finalizado" });
            statusComboBox.SelectedIndex = 0;

            opcaoRetornoComboBox.Items.AddRange(new object[] { "Telefone", "Fax", "E-mail", "Nenhum" });
            opcaoRetornoComboBox.SelectedIndex = 0;

            ufComboBox.Items.AddRange(new object[] { "-", "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" });
            ufComboBox.SelectedIndex = 0;

            if (codigoTextBox.Text != "")
            {
                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (codigoTextBox.Text.Trim() != "" && (!codigoTextBox.Text.Contains(_empresa.Separador)))
                codigoTextBox.Text = (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.Ano ? DateTime.Now.Year.ToString("0000") :
                      (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.AnoMes ? DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") :
                       (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAno ? Publicas._usuario.IdEmpresa.ToString("000") + DateTime.Now.Year.ToString("0000") :
                        (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes ? Publicas._usuario.IdEmpresa.ToString("000") + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") : "")))) +
                        _empresa.Separador + codigoTextBox.Text;

            Publicas._telaQueChamouPesquisaDeAtendimento = Publicas.TelaPesquisaSAC.Retorno;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Atendimentos().ShowDialog();

                codigoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (codigoTextBox.Text.Trim() == "")
                {
                    codigoTextBox.Text = string.Empty;
                    Publicas._escTeclado = true;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _atendimento = new AtendimentoBO().Consultar(codigoTextBox.Text);

            if (_atendimento == null || !_atendimento.Existe)
            {
                new Notificacoes.Mensagem("Atendimento não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }
            else
            {
                if (_atendimento.Status != Publicas.StatusAtendimento.Finalizado)
                {
                    new Notificacoes.Mensagem("Atendimento não foi finalizado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    codigoTextBox.Focus();
                    return;
                }

                if (_atendimento.Status == Publicas.StatusAtendimento.Cancelado)
                {
                    new Notificacoes.Mensagem("Atendimento cancelado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    codigoTextBox.Focus();
                    return;
                }

                if (_atendimento.Situacao != Publicas.SituacaoAtendimento.AguardandoRetornoAoCliente &&
                    _atendimento.Situacao != Publicas.SituacaoAtendimento.EnviadoAoFinalizador)
                {
                    new Notificacoes.Mensagem("Atendimento não disponível para ser retornado ao cliente.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    codigoTextBox.Focus();
                    return;
                }

                if (_atendimento.Retornou)
                {
                    new Notificacoes.Mensagem("Atendimento já foi retornado ao cliente.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    codigoTextBox.Focus();
                    return;
                }

                if (_atendimento.OpcoesDeRetorno != Publicas.OpcaoDeRetornoAtendimento.Telefone &&
                    _atendimento.OpcoesDeRetorno != Publicas.OpcaoDeRetornoAtendimento.Fax) 
                {
                    new Notificacoes.Mensagem("Opção de retorno diferente de telefone ou fax.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    codigoTextBox.Focus();
                    return;
                }
                _usuarioAbertura = new UsuarioBO().ConsultarPorId(_atendimento.IdUsuario);
                retornoDateTimePicker.Value = DateTime.Now;
                usuarioAberturaLabel.Text = "aberto por " + _usuarioAbertura.Nome;

                aberturaDateTimePicker.Value = _atendimento.DataAbertura;
                retornoDateTimePicker.Value = DateTime.Now.Date;

                statusComboBox.SelectedIndex = (_atendimento.Status == Publicas.StatusAtendimento.Ativo ? 0 :
                                                (_atendimento.Status == Publicas.StatusAtendimento.Cancelado ? 1 :
                                                 (_atendimento.Status == Publicas.StatusAtendimento.Respondido ? 2 : 3)));

                opcaoRetornoComboBox.SelectedIndex = (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Telefone ? 0 :
                                                      (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Fax ? 1 :
                                                       (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Email ? 2 : 3)));

                statusComboBox.Enabled = false;
                opcaoRetornoComboBox.Enabled = false;

                nomeTextBox.Text = _atendimento.NomeCliente;
                anonimoCheckBox.Checked = _atendimento.ClienteAnonimo;
                enderecoTextBox.Text = _atendimento.EnderecoCliente;
                cidadeTextBox.Text = _atendimento.CidadeCliente;
                rgTextBox.Text = _atendimento.RGCliente;

                if (_atendimento.CPFCliente != 0)
                    cpfMaskedEditBox.Text = _atendimento.CPFCliente.ToString();

                if (_atendimento.TelefoneCliente != 0)
                    telefoneMaskedEditBox.Text = _atendimento.TelefoneCliente.ToString();

                if (_atendimento.Celular != 0)
                    celularMaskedEditBox.Text = _atendimento.Celular.ToString();

                emailTextBox.Text = _atendimento.EmailCliente;

                if (string.IsNullOrEmpty(_atendimento.UFCliente))
                    ufComboBox.SelectedIndex = 0;
                else
                {
                    for (int i = 0; i < ufComboBox.Items.Count; i++)
                    {
                        ufComboBox.SelectedIndex = i;
                        if (ufComboBox.Text == _atendimento.UFCliente)
                            break;
                    }
                }

                codigoEmtuTextBox.Text = _atendimento.CodigoEmtu;
                _tipoEMTU = new TipoDeAtendimentoEMTUBO().Consultar(codigoEmtuTextBox.Text);

                descricaoEMTUTextBox.Text = _tipoEMTU.Descricao;

                _tipoAtendimento = new TipoDeAtendimentoBO().Consultar(_tipoEMTU.IdTipoAtendimento);

                for (int i = 0; i < tipoAcessoComboBox.Items.Count; i++)
                {
                    tipoAcessoComboBox.SelectedIndex = i;
                    if (tipoAcessoComboBox.Text == _tipoAtendimento.Descricao)
                        break;
                }

                descricaoAtendimentoTextBox.Text = _atendimento.TextoAtendimento;
                respostaTextBox.Text = _atendimento.TextoRetornoAoCliente;
            }

            gravarButton.Enabled = true;

            try
            {
                ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
            }
            catch { }

            if (Publicas._codigoRetornoPesquisa != "")
                retornoDateTimePicker.Focus();
        }

        private void retornoDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void retornoDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            if (retornoDateTimePicker.Value.Date < DateTime.Now.Date)
            {
                new Notificacoes.Mensagem("Retorno do atendimento não pode ser com data retroativa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                retornoDateTimePicker.Focus();
                return;
            }
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            _atendimento.IdUsuarioRetornoAoCliente = Publicas._idUsuario;
            _atendimento.TextoResposta = respostaTextBox.Text;
            _atendimento.AguardaSatisfacaoCliente = !finalizaSemRetornoCheckBox.Checked; // se checado não aguarda
            _atendimento.Situacao = _atendimento.AguardaSatisfacaoCliente ? Publicas.SituacaoAtendimento.AguardandoSatisfacao : Publicas.SituacaoAtendimento.Finalizado;
            _atendimento.Retornou = true;


            if (!new AtendimentoBO().Gravar(_atendimento, _atendimento))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            codigoEmtuTextBox.Text = string.Empty;
            descricaoEMTUTextBox.Text = string.Empty;
            descricaoAtendimentoTextBox.Text = string.Empty;
            usuarioAberturaLabel.Text = string.Empty;
            respostaTextBox.Text = string.Empty;
            aberturaDateTimePicker.Value = DateTime.Now;
            retornoDateTimePicker.Value = DateTime.Now;
            statusComboBox.SelectedIndex = -1;
            opcaoRetornoComboBox.SelectedIndex = -1;
            tipoAcessoComboBox.SelectedIndex = -1;
            gravarButton.Enabled = false;

            statusComboBox.Enabled = false;
            opcaoRetornoComboBox.Enabled = false;

            codigoTextBox.Focus();
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

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                respostaTextBox.Focus();
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

        private void descricaoAtendimentoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }
    }
}
