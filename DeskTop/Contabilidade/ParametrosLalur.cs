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

namespace Suportte.Contabilidade
{
    public partial class ParametrosLalur : Form
    {
        public ParametrosLalur()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                LimiteIRPJCurrencyTextBox.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                LimiteIRPJCurrencyTextBox.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                LimiteIRPJCurrencyTextBox.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                LimiteIRPJCurrencyTextBox.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                LimiteIRPJCurrencyTextBox.BackGroundColor = Publicas._fundo;

                LimiteCSLLCurrencyTextBox.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                LimiteCSLLCurrencyTextBox.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                LimiteCSLLCurrencyTextBox.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                LimiteCSLLCurrencyTextBox.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                LimiteCSLLCurrencyTextBox.BackGroundColor = Publicas._fundo;

                CompensacaoText.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                CompensacaoText.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                CompensacaoText.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                CompensacaoText.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                CompensacaoText.BackGroundColor = Publicas._fundo;

                CSLLText.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                CSLLText.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                CSLLText.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                CSLLText.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                CSLLText.BackGroundColor = Publicas._fundo;

                IRPJText.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                IRPJText.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                IRPJText.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                IRPJText.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                IRPJText.BackGroundColor = Publicas._fundo;

                ValorParcelaIsentaText.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                ValorParcelaIsentaText.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                ValorParcelaIsentaText.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                ValorParcelaIsentaText.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                ValorParcelaIsentaText.BackGroundColor = Publicas._fundo;

                AdicionalAPagarText.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                AdicionalAPagarText.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                AdicionalAPagarText.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                AdicionalAPagarText.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                AdicionalAPagarText.BackGroundColor = Publicas._fundo;

                PatText.PositiveColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                PatText.ForeColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                PatText.NegativeColor = (Publicas._TemaBlack ? Publicas._fonte : Color.DarkRed);
                PatText.ZeroColor = (Publicas._TemaBlack ? Publicas._fonte : empresaComboBoxAdv.ForeColor);
                PatText.BackGroundColor = Publicas._fundo;
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Classes.Empresa> _listaEmpresas;
        Classes.Empresa _empresa;
        Classes.Lalur.Parametros _pametros;
        Classes.Lalur.Parametros _pametrosLog;

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

        private void ParametrosLalur_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

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

        private void CompensacaoText_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CompensacaoText.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void CompensacaoText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SelectNextControl(ActiveControl, true, true, true, true);
            Publicas._escTeclado = false;
            Publicas._setaParaBaixo = false;
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
                PatText.Focus();
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

            _pametros = new LalurBO().ConsultarParametros(_empresa.IdEmpresa);
            _pametrosLog = new LalurBO().ConsultarParametros(_empresa.IdEmpresa);

            if (_pametros.Existe)
            {
                PatText.DecimalValue = _pametros.PercentualPat;
                IRPJText.DecimalValue = _pametros.PercentualIRPJ;
                CSLLText.DecimalValue = _pametros.PercentualCSLL;
                CompensacaoText.DecimalValue = _pametros.PercentualCompensacaoNegativa;
                AdicionalAPagarText.DecimalValue = _pametros.PercentualAdicionalPagar;
                ValorParcelaIsentaText.DecimalValue = _pametros.ValorParcelaIsenta;
                LimiteCSLLCurrencyTextBox.DecimalValue = _pametros.LimiteCSLL;
                LimiteIRPJCurrencyTextBox.DecimalValue = _pametros.LimiteIRPJ;
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _pametros.Existe;
        }

        private void CompensacaoText_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void CSLLText_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

        }

        private void IRPJText_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

        }

        private void ValorParcelaIsentaText_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

        }

        private void AdicionalAPagarText_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

        }

        private void PatText_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            _pametros.IdEmpresa = _empresa.IdEmpresa;
            _pametros.PercentualPat = PatText.DecimalValue;
            _pametros.PercentualIRPJ = IRPJText.DecimalValue;
            _pametros.PercentualCSLL = CSLLText.DecimalValue;
            _pametros.PercentualCompensacaoNegativa = CompensacaoText.DecimalValue;
            _pametros.PercentualAdicionalPagar = AdicionalAPagarText.DecimalValue;
            _pametros.ValorParcelaIsenta = ValorParcelaIsentaText.DecimalValue;
            _pametros.LimiteCSLL = LimiteCSLLCurrencyTextBox.DecimalValue;
            _pametros.LimiteIRPJ = LimiteIRPJCurrencyTextBox.DecimalValue;

            if (!new LalurBO().Gravar(_pametros))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            string _descricao = "";

            if (_pametros.Existe)
            {
                _descricao = _descricao +
                    (_pametros.PercentualAdicionalPagar == _pametrosLog.PercentualAdicionalPagar ? "" : " [PercentualAdicionalPagar] de " + _pametrosLog.PercentualAdicionalPagar.ToString() + " para " + _pametros.PercentualAdicionalPagar.ToString() + "") +
                    (_pametros.PercentualCompensacaoNegativa == _pametrosLog.PercentualCompensacaoNegativa ? "" : " [PercentualCompensacaoNegativa] de " + _pametrosLog.PercentualCompensacaoNegativa.ToString() + " para " + _pametros.PercentualCompensacaoNegativa.ToString() + "") +
                    (_pametros.PercentualCSLL == _pametrosLog.PercentualCSLL ? "" : " [PercentualCSLL] de " + _pametrosLog.PercentualCSLL.ToString() + " para " + _pametros.PercentualCSLL.ToString() + "") +
                    (_pametros.PercentualIRPJ == _pametrosLog.PercentualIRPJ ? "" : " [PercentualIRPJ] de " + _pametrosLog.PercentualIRPJ.ToString() + " para " + _pametros.PercentualIRPJ.ToString() + "") +
                    (_pametros.PercentualPat == _pametrosLog.PercentualPat ? "" : " [PercentualPat] de " + _pametrosLog.PercentualPat.ToString() + " para " + _pametros.PercentualPat.ToString() + "") +
                    (_pametros.ValorParcelaIsenta == _pametrosLog.ValorParcelaIsenta ? "" : " [ValorParcelaIsenta] de " + _pametrosLog.ValorParcelaIsenta.ToString() + " para " + _pametros.ValorParcelaIsenta.ToString() + "");
            }
            
            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou Parâmetro Lalur para a empresa " + empresaComboBoxAdv.Text;
            _log.Tela = "Contabilidade - Lalur - Parâmetros";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            CompensacaoText.DecimalValue = 0;
            PatText.DecimalValue = 0;
            IRPJText.DecimalValue = 0;
            CSLLText.DecimalValue = 0;
            ValorParcelaIsentaText.DecimalValue = 0;
            AdicionalAPagarText.DecimalValue = 0;
            LimiteCSLLCurrencyTextBox.DecimalValue = 0;
            LimiteIRPJCurrencyTextBox.DecimalValue = 0;

            empresaComboBoxAdv.Focus();
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new LalurBO().ExcluirParametros(_pametros.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu Parâmetro Lalur para a empresa " + empresaComboBoxAdv.Text;
            _log.Tela = "Contabilidade - Lalur - Parâmetros";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            limparButton_Click(sender, e);
        }

        private void LimiteCSLLCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void LimiteIRPJCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }
    }
}
