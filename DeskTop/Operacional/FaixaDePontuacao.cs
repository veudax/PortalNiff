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

namespace Suportte.Operacional
{
    public partial class FaixaDePontuacao : Form
    {
        public FaixaDePontuacao()
        {
            InitializeComponent();
            VigenciaDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                if (Publicas._TemaBlack)
                {
                    VigenciaDateTimePicker.Value = DateTime.Now;
                    VigenciaDateTimePicker.Style = Syncfusion.Windows.Forms.VisualStyle.Office2016Black;
                    FaixaInicialCurrencyTextBox.PositiveColor = Publicas._fonte;
                    FaixaFinalCurrencyTextBox.PositiveColor = Publicas._fonte;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Operacional.Pontuacao _pontuacao;
        Classes.Operacional.Pontuacao _pontuacaoLog;
        Classes.Operacional.VigenciaPontuacao _vigencia;
        Classes.Operacional.VigenciaPontuacao _vigenciaLog;
        List<Classes.Operacional.VigenciaPontuacao> _listaVigencias;
        List<Classes.Operacional.VigenciaPontuacao> _listaVigenciasLog;
        List<Classes.Empresa> _listaEmpresas;

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

        private void FaixaDePontuacao_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                codigoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                VigenciaDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void VigenciaDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                FaixaInicialCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void FaixaInicialCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                FaixaFinalCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                VigenciaDateTimePicker.Focus();
            }
        }

        private void FaixaFinalCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FaixaInicialCurrencyTextBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FaixaFinalCurrencyTextBox.Focus();
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

        private void excluirPontuacaoButton_KeyDown(object sender, KeyEventArgs e)
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

        private void VigenciaDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void FaixaInicialCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
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

        private void ExcluirPontuacaoButton_Enter(object sender, EventArgs e)
        {
            ExcluirPontuacaoButton.BackColor = Publicas._botaoFocado;
            ExcluirPontuacaoButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void ExcluirPontuacaoButton_Validating(object sender, CancelEventArgs e)
        {
            ExcluirPontuacaoButton.BackColor = Publicas._botao;
            ExcluirPontuacaoButton.ForeColor = Publicas._fonteBotao;
        }

        private void excluirButton_Validating(object sender, CancelEventArgs e)
        {
            excluirButton.BackColor = Publicas._botao;
            excluirButton.ForeColor = Publicas._fonteBotao;
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

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            codigoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(codigoTextBox.Text))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;

                new Pesquisas.FaixaDePontuacao().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _pontuacao = new OperacionalBO().Consultar(Convert.ToInt32(codigoTextBox.Text), _empresa.IdEmpresa);
            _pontuacaoLog = new OperacionalBO().Consultar(Convert.ToInt32(codigoTextBox.Text), _empresa.IdEmpresa);

            if (_pontuacao.Existe)
            {
                ExcluirPontuacaoButton.Enabled = true;
                excluirButton.Enabled = false;
                nomeTextBox.Text = _pontuacao.Descricao;
                ativoCheckBox.Checked = _pontuacao.Ativo;
                ativoCheckBox.Focus();

                _listaVigencias = new OperacionalBO().ListarVigenciasPontuacao(_pontuacao.Id);
                _listaVigenciasLog = new OperacionalBO().ListarVigenciasPontuacao(_pontuacao.Id);
            }
        }

        private void VigenciaDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            VigenciaDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                PesquisaVigenciaButton.Enabled = false;
                return;
            }

            if (VigenciaDateTimePicker.Value == DateTime.MinValue)
            {
                Publicas._idRetornoPesquisa = _pontuacao.Id;

                new Pesquisas.VigenciasDaPontuacao().ShowDialog();

                if (Publicas._codigoRetornoPesquisa.ToString() == "")
                {
                    VigenciaDateTimePicker.Focus();
                    return;
                }

                VigenciaDateTimePicker.Value = Convert.ToDateTime(Publicas._codigoRetornoPesquisa.ToString());
            }

            _vigencia = new OperacionalBO().Consultar(_pontuacao.Id, VigenciaDateTimePicker.Value);
            _vigenciaLog = new OperacionalBO().Consultar(_pontuacao.Id, VigenciaDateTimePicker.Value);

            if (_vigencia.Existe)
            {
                FaixaInicialCurrencyTextBox.DecimalValue = _vigencia.Inicio;
                FaixaFinalCurrencyTextBox.DecimalValue = _vigencia.Fim;
                excluirButton.Enabled = true;
            }
            gravarButton.Enabled = true;
        }

        private void FaixaInicialCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            FaixaInicialCurrencyTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void FaixaFinalCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            FaixaFinalCurrencyTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

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

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            PesquisaVigenciaButton.Enabled = !string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pesquisaButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(codigoTextBox.Text))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;

                new Pesquisas.FaixaDePontuacao().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }

                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new OperacionalBO().ProximaPontuacao(_empresa.IdEmpresa).ToString();
            ativoCheckBox.Focus();
        }

        private void PesquisaVigenciaButton_Click(object sender, EventArgs e)
        {
            Publicas._idRetornoPesquisa = _pontuacao.Id;

            new Pesquisas.VigenciasDaPontuacao().ShowDialog();

            if (Publicas._codigoRetornoPesquisa.ToString() == "")
            {
                VigenciaDateTimePicker.Focus();
                return;
            }

            VigenciaDateTimePicker.Value = Convert.ToDateTime(Publicas._codigoRetornoPesquisa.ToString());
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o nome da descrição.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            _pontuacao.Ativo = ativoCheckBox.Checked;
            _pontuacao.Codigo = Convert.ToInt32(codigoTextBox.Text);
            _pontuacao.Descricao = nomeTextBox.Text;
            _pontuacao.IdEmpresa = _empresa.IdEmpresa;

            _vigencia.Data = VigenciaDateTimePicker.Value;
            _vigencia.Fim = FaixaFinalCurrencyTextBox.DecimalValue;
            _vigencia.Inicio = FaixaInicialCurrencyTextBox.DecimalValue;

            if (!new OperacionalBO().Gravar(_pontuacao, _vigencia))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Tela = "Cadastro de faixa de pontuação";

            _log.Descricao = (_pontuacao.Existe ? "Alterou " : "Incluiu ") + " a faixa " + codigoTextBox.Text + " da empresa "
                + empresaComboBoxAdv.Text + " ";

            if (_pontuacao.Existe)
            {
                _log.Descricao = _log.Descricao +
                (_pontuacao.Descricao == _pontuacaoLog.Descricao ? "" : " [Descricao] de " + _pontuacaoLog.Descricao + " para " + _pontuacao.Descricao + "") +
                (_pontuacao.Ativo == _pontuacaoLog.Ativo ? "" : " [Ativo] de " + _pontuacaoLog.Ativo.ToString() + " para " + _pontuacao.Ativo.ToString() + "") + " ";
            }

            if (!_vigencia.Existe)
                _log.Descricao = _log.Descricao + " Incluiu a Vigência " + VigenciaDateTimePicker.Value.ToShortDateString();
            else
                _log.Descricao = _log.Descricao + "Alterou a faixa da vigência " + VigenciaDateTimePicker.Value.ToShortDateString() +
                    (_vigencia.Inicio == _vigenciaLog.Inicio ? "" : " [Inicio] de " + _vigenciaLog.Inicio.ToString() + " para " + _vigencia.Inicio.ToString() + "") +
                (_vigencia.Fim == _vigenciaLog.Fim ? "" : " [Fim] de " + _vigenciaLog.Fim.ToString() + " para " + _vigencia.Fim.ToString() + "") + " ";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            FaixaInicialCurrencyTextBox.DecimalValue = 0;
            FaixaFinalCurrencyTextBox.DecimalValue = 0;
            codigoTextBox.Focus();
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            ExcluirPontuacaoButton.Enabled = false;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão da vigência?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Tela = "Cadastro de faixa de pontuação";
            _log.Descricao = "Excluiu a vigência " + VigenciaDateTimePicker.Value.ToShortDateString() +
                " da faixa de pontuação " + codigoTextBox.Text + " da empresa " + empresaComboBoxAdv.Text +
                " inicio " + FaixaInicialCurrencyTextBox.Text + " fim " + FaixaFinalCurrencyTextBox.Text;

            if (!new OperacionalBO().ExcluirVigenciaPontuacao(_vigencia.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void ExcluirPontuacaoButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão da vigência?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Tela = "Cadastro de faixa de pontuação";

            _log.Descricao = "Excluiu a faixa de pontuação " + codigoTextBox.Text + " da empresa " + empresaComboBoxAdv.Text + " com as vigências: ";

            foreach (var item in _listaVigencias.GroupBy(g => g.Data))
            {
                _log.Descricao = _log.Descricao + item.Key.ToShortDateString() + " com os: [";

                foreach (var linha in _listaVigencias.Where(g => g.Data == item.Key))
                {
                    _log.Descricao = _log.Descricao + " inicio " + linha.Inicio + " fim " + linha.Fim + "], ";
                }
            }

            _log.Descricao = _log.Descricao.Substring(0, _log.Descricao.Length - 2);

            if (!new OperacionalBO().ExcluirPontuacao(_pontuacao.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }
    }
}
