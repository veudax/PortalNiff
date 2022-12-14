using Classes;
using Negocio;
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
    public partial class Satisfacao : Form
    {
        public Satisfacao()
        {
            InitializeComponent();

            aberturaDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                {
                    this.BackColor = Publicas._fundo;
                    //avaliacaoRatingControl.MetroColor = Publicas._fundo;
                    avaliacaoRatingControl.BackColor = Publicas._fundo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        //int pontuacao = 0;
        Empresa _empresa;
        Atendimento _atendimento;
        Atendimento _atendimentoLog;
        TipoDeAtendimentoEMTU _tipoEMTU;
        TipoDeAtendimento _tipoAtendimento;
        Usuario _usuarioResponsavel;
        Usuario _usuarioRespostaCliente;
        Usuario _usuarioAbertura;
        Usuario _usuarioRetorno;


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

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                motivoTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
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

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
            }
            catch { }

            if (codigoTextBox.Text.Trim() != "" && (!codigoTextBox.Text.Contains(_empresa.Separador)))
                codigoTextBox.Text = (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.Ano ? DateTime.Now.Year.ToString("0000") :
                      (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.AnoMes ? DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") :
                       (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAno ? Publicas._usuario.IdEmpresa.ToString("000") + DateTime.Now.Year.ToString("0000") :
                        (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes ? Publicas._usuario.IdEmpresa.ToString("000") + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") : "")))) +
                        _empresa.Separador + codigoTextBox.Text;

            Publicas._telaQueChamouPesquisaDeAtendimento = Publicas.TelaPesquisaSAC.Satisfacao;
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

            _atendimento = new AtendimentoBO().Consultar(codigoTextBox.Text, _empresa.IdEmpresa);
            _atendimentoLog = new AtendimentoBO().Consultar(codigoTextBox.Text, _empresa.IdEmpresa);

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
                
                if (!_atendimento.AguardaSatisfacaoCliente || !_atendimento.Retornou)
                {
                    new Notificacoes.Mensagem("Atendimento não disponível para avaliação.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    codigoTextBox.Focus();
                    return;
                }

                _usuarioAbertura = new UsuarioBO().ConsultarPorId(_atendimento.IdUsuario);

                tituloUsuarioAberturaLabel.Text = "aberto por " + _usuarioAbertura.Nome;

                aberturaDateTimePicker.Value = _atendimento.DataAbertura;

                codigoEmtuTextBox.Text = _atendimento.CodigoEmtu;
                _tipoEMTU = new TipoDeAtendimentoEMTUBO().Consultar(codigoEmtuTextBox.Text);

                descricaoEMTUTextBox.Text = _tipoEMTU.Descricao;

                _tipoAtendimento = new TipoDeAtendimentoBO().Consultar(_tipoEMTU.IdTipoAtendimento);

                descricaoAtendimentoTextBox.Text = _atendimento.TextoAtendimento;
                respostaTextBox.Text = _atendimento.TextoResposta;
                RespostaAoClienteTextBox.Text = _atendimento.TextoRetornoAoCliente;
                RespostaAoClienteTextBox.Enabled = _atendimento.DataFinalizado != DateTime.MinValue;
                
                if (_atendimento.IdUsuarioRetornoAoCliente != 0)
                {
                    _usuarioRespostaCliente = new UsuarioBO().ConsultarPorId(_atendimento.IdUsuarioRetornoAoCliente);
                    respostaAoClienteLabel.Text = "Respondido ao cliente por " + _usuarioRespostaCliente.Nome + " na data " +
                        _atendimento.DataRetornoAoCliente.ToShortDateString();
                }

                if (_atendimento.IdUsuarioResponsavel != 0)
                    _usuarioResponsavel = new UsuarioBO().ConsultarPorId(_atendimento.IdUsuarioResponsavel);

                RespostaAoClienteTextBox.Enabled = (_atendimento.Status == Publicas.StatusAtendimento.Ativo || _atendimento.Status == Publicas.StatusAtendimento.Respondido);

                if (_atendimento.IdUsuarioRetorno != 0)
                {
                    _usuarioRetorno = new UsuarioBO().ConsultarPorId(_atendimento.IdUsuarioRetorno);

                    respostaColaboradorLabel.Text = "Retornado pelo Colaborador " + _usuarioRetorno.Nome + " na data " + _atendimento.DataResposta.ToShortDateString();

                }
            }

            if (_atendimento.IdUsuarioResponsavel != 0)
                _usuarioResponsavel = new UsuarioBO().ConsultarPorId(_atendimento.IdUsuarioResponsavel);

            avaliacaoRatingControl.Value = (_atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.Ruim ? 1 :
                (_atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.Bom ? 2 :
                (_atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.Regular ? 3 :
                (_atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.MuitoBom ? 4 :
                (_atendimento.Satisfacao == Publicas.TipoDeSatisfacaoAtendimento.Excelente ? 5 : 0)))));

            motivoTextBox.Text = _atendimento.MotivoSatisfacao;

            gravarButton.Enabled = false;

            if (Publicas._codigoRetornoPesquisa != "")
                motivoTextBox.Focus();
        }

        private void motivoTextBox_TextChanged(object sender, EventArgs e)
        {
            gravarButton.Enabled = (!string.IsNullOrEmpty(motivoTextBox.Text.Trim()) && avaliacaoRatingControl.Value > 0);
        }

        private void motivoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void motivoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void motivoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void Satisfacao_Load(object sender, EventArgs e)
        {
            // Empresa do usuario logado
            //if (Publicas._usuario.Administrador)
            //    _empresa = new EmpresaBO().Consultar(4);
            //else
                _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            
            codigoTextBox.Text = string.Empty;
            codigoEmtuTextBox.Text = string.Empty;
            descricaoEMTUTextBox.Text = string.Empty;
            descricaoAtendimentoTextBox.Text = string.Empty;
            motivoTextBox.Text = string.Empty;
            respostaAoClienteLabel.Text = string.Empty;
            respostaTextBox.Text = string.Empty;
            aberturaDateTimePicker.Value = DateTime.Now;
            gravarButton.Enabled = false;
            tituloUsuarioAberturaLabel.Text = string.Empty;
            respostaColaboradorLabel.Text = string.Empty;
            
            codigoTextBox.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            _atendimento.Situacao = Publicas.SituacaoAtendimento.Finalizado;
            _atendimento.AguardaSatisfacaoCliente = false;
            _atendimento.Satisfacao = (avaliacaoRatingControl.Value == 1 ? Publicas.TipoDeSatisfacaoAtendimento.Ruim :
                                      (avaliacaoRatingControl.Value == 2 ? Publicas.TipoDeSatisfacaoAtendimento.Bom :
                                      (avaliacaoRatingControl.Value == 3 ? Publicas.TipoDeSatisfacaoAtendimento.Regular :
                                      (avaliacaoRatingControl.Value == 4 ? Publicas.TipoDeSatisfacaoAtendimento.MuitoBom : Publicas.TipoDeSatisfacaoAtendimento.Excelente))));

            _atendimento.MotivoSatisfacao = motivoTextBox.Text;

            if (!new AtendimentoBO().Gravar(_atendimento, _atendimentoLog, null))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Alterou atendimento " + _atendimento.Codigo + " informou a satisfação do cliente";
            _log.Tela = "SAC - Satisfação";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
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
