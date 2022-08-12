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
    public partial class Finalizar : Form
    {
        public Finalizar()
        {
            InitializeComponent();

            aberturaDateTimePicker.BorderColor = Publicas._bordaSaida;
            fechamentoDateTimePicker.BorderColor = Publicas._bordaSaida;
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
        Atendimento _atendimentoLog;
        TipoDeAtendimentoEMTU _tipoEMTU;
        TipoDeAtendimento _tipoAtendimento;
        Usuario _usuarioAbertura;
        FuncionariosGlobus _funcionariosGlobus;

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
                situacaoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void funcionarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                respostaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                respostaSACTextBox.Focus();
            }
        }

        private void respostaSACTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                funcionarioTextBox.Focus();
            }
        }
        #endregion

        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(_empresa.Separador))
                e.Handled = false;
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void Finalizar_Load(object sender, EventArgs e)
        {
            // Empresa do usuario logado
            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

            List<TipoDeAtendimento> _lista = new TipoDeAtendimentoBO().Listar();
            tipoAcessoComboBox.DataSource = _lista;
            tipoAcessoComboBox.DisplayMember = "Descricao";
            tipoAcessoComboBox.SelectedIndex = 0;

            statusComboBox.Items.AddRange(new object[] { "Ativo", "Cancelado", "Respondido", "Finalizado" });
            statusComboBox.SelectedIndex = 0;

            opcaoRetornoComboBox.Items.AddRange(new object[] { "Telefone", "Fax", "E-mail", "Nenhum" });
            opcaoRetornoComboBox.SelectedIndex = 0;

            situacaoComboBox.Items.AddRange(new object[] { "Devolver para o Atendente", "Enviado ao Finalizador", "Cancelado", "Finalizado" });
            situacaoComboBox.SelectedIndex = 0;

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

            Publicas._telaQueChamouPesquisaDeAtendimento = Publicas.TelaPesquisaSAC.Finaliza;
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
            _atendimentoLog = new AtendimentoBO().Consultar(codigoTextBox.Text);

            if (_atendimento == null || !_atendimento.Existe)
            {
                new Notificacoes.Mensagem("Atendimento não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }
            else
            {
                if (_atendimento.Status == Publicas.StatusAtendimento.Finalizado)
                {
                    new Notificacoes.Mensagem("Atendimento já foi finalizado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    codigoTextBox.Focus();
                    return;
                }

                if (_atendimento.Status == Publicas.StatusAtendimento.Cancelado)
                {
                    new Notificacoes.Mensagem("Atendimento cancelado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    codigoTextBox.Focus();
                    return;
                }

                if (_atendimento.Situacao != Publicas.SituacaoAtendimento.EnviadoAoFinalizador)
                {
                    new Notificacoes.Mensagem("Atendimento não disponível para ser finalizado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    codigoTextBox.Focus();
                    return;
                }

                _usuarioAbertura = new UsuarioBO().ConsultarPorId(_atendimento.IdUsuario);

                usuarioAberturaLabel.Text = "aberto por " + _usuarioAbertura.Nome;

                aberturaDateTimePicker.Value = _atendimento.DataAbertura;

                statusComboBox.SelectedIndex = (_atendimento.Status == Publicas.StatusAtendimento.Ativo ? 0 :
                                                (_atendimento.Status == Publicas.StatusAtendimento.Cancelado ? 1 :
                                                 (_atendimento.Status == Publicas.StatusAtendimento.Respondido ? 2 : 3)));

                opcaoRetornoComboBox.SelectedIndex = (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Telefone ? 0 :
                                                      (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Fax ? 1 :
                                                       (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Email ? 2 : 3)));

                situacaoComboBox.SelectedIndex = (_atendimento.Situacao == Publicas.SituacaoAtendimento.ManterComAtendente ? 0 :
                                                      (_atendimento.Situacao == Publicas.SituacaoAtendimento.EnviadoAoFinalizador ? 1 :
                                                       (_atendimento.Situacao == Publicas.SituacaoAtendimento.Cancelado ? 2 : 3)));

                statusComboBox.Enabled = false;
                opcaoRetornoComboBox.Enabled = false;

                if (!String.IsNullOrEmpty(_atendimento.TextoResposta))
                    fechamentoDateTimePicker.Value = _atendimento.DataResposta;

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
                respostaTextBox.Text = _atendimento.TextoResposta;
            }

            gravarButton.Enabled = statusComboBox.SelectedIndex == 0;

            try
            {
                ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
            }
            catch { }

            if (Publicas._codigoRetornoPesquisa != "")
                situacaoComboBox.Focus();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            codigoEmtuTextBox.Text = string.Empty;
            descricaoEMTUTextBox.Text = string.Empty;
            descricaoAtendimentoTextBox.Text = string.Empty;
            respostaTextBox.Text = string.Empty;
            respostaSACTextBox.Text = string.Empty;
            usuarioAberturaLabel.Text = string.Empty;
            aberturaDateTimePicker.Value = DateTime.Now;
            fechamentoDateTimePicker.Value = DateTime.Now;
            statusComboBox.SelectedIndex = -1;
            opcaoRetornoComboBox.SelectedIndex = -1;
            tipoAcessoComboBox.SelectedIndex = -1;
            gravarButton.Enabled = false;

            statusComboBox.Enabled = true;
            opcaoRetornoComboBox.Enabled = true;

            codigoTextBox.Focus();
        }

        private void funcionarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
                return;
            }

            if (funcionarioTextBox.Text.Trim() == "")
            {
                if (Publicas._setaParaBaixo)
                {
                    Publicas._setaParaBaixo = false;
                    ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

                    return;
                }

                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

                new Pesquisas.Funcionarios().ShowDialog();

                funcionarioTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (funcionarioTextBox.Text.Trim() == "")
                {
                    funcionarioTextBox.Text = string.Empty;
                    Publicas._escTeclado = true;
                    funcionarioTextBox.Focus();
                    return;
                }
            }

            funcionarioTextBox.Text = funcionarioTextBox.Text.PadLeft(6, '0');
            _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(funcionarioTextBox.Text, _empresa.CodigoEmpresaGlobus);

            if (!_funcionariosGlobus.Existe)
            {
                new Notificacoes.Mensagem("Funcionário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                funcionarioTextBox.Focus();
                return;
            }

            if (!_funcionariosGlobus.Ativo)
            {
                new Notificacoes.Mensagem("Funcionário inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                funcionarioTextBox.Focus();
                return;
            }

            nomeFuncionarioTextBox.Text = _funcionariosGlobus.Nome;
            areaTextBox.Text = _funcionariosGlobus.Area;

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._codigoRetornoPesquisa != "")
                respostaTextBox.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(descricaoAtendimentoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe a descrição do atendimento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                descricaoAtendimentoTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(respostaSACTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe a resposta.", Publicas.TipoMensagem.Alerta).ShowDialog();
                respostaSACTextBox.Focus();
                return;
            }

            if (situacaoComboBox.SelectedIndex == 1)
            {
                new Notificacoes.Mensagem("Situação deve ser diferente de 'Enviado ao finalizador'.", Publicas.TipoMensagem.Alerta).ShowDialog();
                respostaSACTextBox.Focus();
                return;
            }

            if (situacaoComboBox.SelectedIndex == 0) //devolver ao atendente
            {
                if (new Notificacoes.Mensagem("Tem certeza que deseja retornar o chamado para o atendente?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    return;

                new SAC.Motivo().ShowDialog();

                if (Publicas._cancelouMotivo)
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + "Motivo não foi informado.", Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
                _atendimento.MotivoRetorno = Publicas._motivoCancelamentoDevolucao;
                
            }

            if (situacaoComboBox.SelectedIndex == 2) //Cancelar
            {
                if (new Notificacoes.Mensagem("Tem certeza que deseja Cancelar este atendimento?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    return;

                new SAC.Motivo().ShowDialog();
                if (Publicas._cancelouMotivo)
                {
                    new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + "Motivo não foi informado.", Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
                _atendimento.MotivoCancelamento = Publicas._motivoCancelamentoDevolucao;
            }

            _atendimento.IdUsuarioRetorno = Publicas._idUsuario;
            _atendimento.TextoResposta = respostaTextBox.Text.Trim();
            _atendimento.Situacao = (situacaoComboBox.SelectedIndex == 0 ? Publicas.SituacaoAtendimento.ManterComAtendente :
                                    (situacaoComboBox.SelectedIndex == 2 ? Publicas.SituacaoAtendimento.Cancelado : Publicas.SituacaoAtendimento.AguardandoRetornoAoCliente));

            _atendimento.Status = (situacaoComboBox.SelectedIndex == 0 ? Publicas.StatusAtendimento.Ativo :
                                    (situacaoComboBox.SelectedIndex == 2 ? Publicas.StatusAtendimento.Cancelado : Publicas.StatusAtendimento.Finalizado));
            _atendimento.TextoRetornoAoCliente = respostaSACTextBox.Text.Trim();

            if (!new AtendimentoBO().Gravar(_atendimento, _atendimentoLog))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (situacaoComboBox.SelectedIndex == 3) // Finalizou
            {
                Publicas.EnviarEmail("SAC - Atendimento ao Cliente", _empresa.Nome,
                    "", codigoTextBox.Text, DateTime.Now.ToShortDateString(),
                    _empresa.Telefone, _empresa.Email, Publicas._usuario.Email + ";" + Publicas._usuario.EmailDepartamento,
                    _empresa.Senha, _empresa.Nome + " - S.A.C nº " + codigoTextBox.Text, _empresa.Smtp,
                    _empresa.PortaSmtp, _empresa.Autentica, _empresa.AutenticaSLL, "", Publicas._usuario.Nome, "finalizado");

                if (Publicas.mensagemDeErro != "")
                {
                    new Notificacoes.Mensagem("Problemas durante o envio do e-mail ao departamento." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }

                Publicas._telaQueChamouPesquisaDeAtendimento = Publicas.TelaPesquisaSAC.Atendimento;

                if (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Email && _atendimento.EmailCliente != "")
                {
                    Publicas.EnviarEmail("SAC - Atendimento ao Cliente", _empresa.Nome,
                            _usuarioAbertura.Nome, codigoTextBox.Text, DateTime.Now.ToShortDateString(),
                            _empresa.Telefone, _empresa.Email, 
                            _atendimento.EmailCliente,
                            _empresa.Senha, _empresa.Nome + " - S.A.C nº " + codigoTextBox.Text, _empresa.Smtp,
                            _empresa.PortaSmtp, _empresa.Autentica, _empresa.AutenticaSLL, "", Publicas._usuario.Nome);

                    if (Publicas.mensagemDeErro != "")
                    {
                        new Notificacoes.Mensagem("Problemas durante o envio do e-mail ao cliente." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }
                }                
            }
            else
            {
                if (situacaoComboBox.SelectedIndex == 0) // Devolveu
                {
                    Publicas._devolveuAtendimento = true;
                    Publicas.EnviarEmail("SAC - Atendimento ao Cliente", _empresa.Nome,
                              _usuarioAbertura.Nome, codigoTextBox.Text, DateTime.Now.ToShortDateString(),
                              _empresa.Telefone, _empresa.Email, 
                              Publicas._usuario.Email + ";" + Publicas._usuario.EmailDepartamento + ";" + _usuarioAbertura.Email,
                              _empresa.Senha, _empresa.Nome + " - S.A.C nº " + codigoTextBox.Text, _empresa.Smtp,
                              _empresa.PortaSmtp, _empresa.Autentica, _empresa.AutenticaSLL, "devolveu", 
                              Publicas._usuario.Nome, "", Publicas._motivoCancelamentoDevolucao);

                    if (Publicas.mensagemDeErro != "")
                    {
                        new Notificacoes.Mensagem("Problemas durante o envio do e-mail ao atendente." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        return;
                    }
                }
            }
            
            limparButton_Click(sender, e);
        }

        private void funcionarioTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void respostaSACTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void situacaoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void situacaoComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void situacaoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                funcionarioTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoEmtuTextBox.Focus();
            }
        }

        private void respostaSACTextBox_TextChanged(object sender, EventArgs e)
        {
            gravarButton.Enabled = !string.IsNullOrEmpty(respostaSACTextBox.Text.Trim()) &&
                !string.IsNullOrEmpty(respostaTextBox.Text.Trim());
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
