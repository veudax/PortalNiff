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

namespace Suportte.Diversos
{
    public partial class RadarBI : Form
    {
        public RadarBI()
        {
            InitializeComponent();

            dataDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
            valorCurrencyTextBox.BackGroundColor = dataDateTimePicker.BackColor;
            percentualCurrencyTextBox.BackGroundColor = dataDateTimePicker.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Empresa> _empresas;
        Classes.RadarBI _radar;
        int tipoSelecionado = -1;

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

        private void RadarBI_Load(object sender, EventArgs e)
        {
            _empresas = new EmpresaBO().Listar(false)
                                       .OrderBy(o => o.CodigoeNome).ToList();

            empresaComboBoxAdv.DataSource = _empresas;
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";

            rubricaComboBoxAdv.Items.Add(Publicas.GetDescription(Publicas.RadarBI.FrotaPatrimonial, ""));
            rubricaComboBoxAdv.Items.Add(Publicas.GetDescription(Publicas.RadarBI.FrotaOperacional, ""));
            rubricaComboBoxAdv.Items.Add(Publicas.GetDescription(Publicas.RadarBI.SubsidioDebito, ""));
            rubricaComboBoxAdv.Items.Add(Publicas.GetDescription(Publicas.RadarBI.SubsidioCredito, ""));
            rubricaComboBoxAdv.Items.Add(Publicas.GetDescription(Publicas.RadarBI.LimpezaDeFrota, ""));
            rubricaComboBoxAdv.Items.Add(Publicas.GetDescription(Publicas.RadarBI.ComprasEmergenciais, ""));
            rubricaComboBoxAdv.Items.Add(Publicas.GetDescription(Publicas.RadarBI.MultaOrgaoGestor, ""));
            rubricaComboBoxAdv.Items.Add(Publicas.GetDescription(Publicas.RadarBI.ReclamaCaoPassageiro, ""));
            rubricaComboBoxAdv.Items.Add(Publicas.GetDescription(Publicas.RadarBI.DiasUteis, ""));
            rubricaComboBoxAdv.Items.Add(Publicas.GetDescription(Publicas.RadarBI.PontesFeriados, "")); // BI Considera pontes para Sábado
            empresaComboBoxAdv.Focus();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            valorCurrencyTextBox.DecimalValue = 0;
            percentualCurrencyTextBox.DecimalValue = 0;
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            rubricaComboBoxAdv.Focus();
        }

        private void rubricaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                dataDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void rubricaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void valorCurrencyTextBox_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada; 
        }

        private void dataDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void valorCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;
        }

        private void dataDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            // Validar se ja existe
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _radar = new RadarBIBO().Consultar(empresaComboBoxAdv.Text.Substring(0, 7), rubricaComboBoxAdv.Text, dataDateTimePicker.Value);

            valorCurrencyTextBox.DecimalValue = _radar.Valor;
            percentualCurrencyTextBox.DecimalValue = _radar.Percentual;
            gravarButton.Enabled = true;
            excluirButton.Enabled = _radar.Existe;

            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;
        }

        private void rubricaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                rubricaComboBoxAdv.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void dataDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                valorCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                rubricaComboBoxAdv.Focus();
            }
        }

        private void valorCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                percentualCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                dataDateTimePicker.Focus();
            }
        }

        private void percentualCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                valorCurrencyTextBox.Focus();
            }
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new RadarBIBO().Excluir(empresaComboBoxAdv.Text.Substring(0,7), rubricaComboBoxAdv.Text, dataDateTimePicker.Value))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            string tipo = "";
            string ordem = "0";

            if (tipoSelecionado == -1 && rubricaComboBoxAdv.SelectedIndex != -1)
                tipoSelecionado = rubricaComboBoxAdv.SelectedIndex;
            else
            {
                if (tipoSelecionado != -1 & rubricaComboBoxAdv.SelectedIndex == -1)
                    rubricaComboBoxAdv.SelectedIndex = tipoSelecionado;
            }

            switch (rubricaComboBoxAdv.SelectedIndex)
            {
                case 0:
                    tipo = Publicas.GetDescription(Publicas.TipoRadarBI.FrotaPatrimonial,"");
                    ordem = Publicas.GetDescription(Publicas.OrdemRadarBI.FrotaPatrimonial,"");
                    break;
                case 1:
                    tipo = Publicas.GetDescription(Publicas.TipoRadarBI.FrotaOperacional, "");
                    ordem = Publicas.GetDescription(Publicas.OrdemRadarBI.FrotaOperacional, "");
                    break;
                case 2:
                    tipo = Publicas.GetDescription(Publicas.TipoRadarBI.SubsidioDebito, "");
                    ordem = Publicas.GetDescription(Publicas.OrdemRadarBI.SubsidioDebito, "");
                    break;
                case 3:
                    tipo = Publicas.GetDescription(Publicas.TipoRadarBI.SubsidioCredito, "");
                    ordem = Publicas.GetDescription(Publicas.OrdemRadarBI.SubsidioCredito, "");
                    break;
                case 4:
                    tipo = Publicas.GetDescription(Publicas.TipoRadarBI.LimpezaDeFrota, "");
                    ordem = Publicas.GetDescription(Publicas.OrdemRadarBI.LimpezaDeFrota, "");
                    break;
                case 5:
                    tipo = Publicas.GetDescription(Publicas.TipoRadarBI.ComprasEmergenciais, "");
                    ordem = Publicas.GetDescription(Publicas.OrdemRadarBI.ComprasEmergenciais, "");
                    break;
                case 6:
                    tipo = Publicas.GetDescription(Publicas.TipoRadarBI.MultaOrgaoGestor, "");
                    ordem = Publicas.GetDescription(Publicas.OrdemRadarBI.MultaOrgaoGestor, "");
                    break;
                case 7:
                    tipo = Publicas.GetDescription(Publicas.TipoRadarBI.ReclamaCaoPassageiro, "");
                    ordem = Publicas.GetDescription(Publicas.OrdemRadarBI.ReclamaCaoPassageiro, "");
                    break;
                case 8:
                    tipo = Publicas.GetDescription(Publicas.TipoRadarBI.DiasUteis, "");
                    ordem = Publicas.GetDescription(Publicas.OrdemRadarBI.DiasUteis, "");
                    break;
                case 9:
                    tipo = Publicas.GetDescription(Publicas.TipoRadarBI.PontesFeriados, "");
                    ordem = Publicas.GetDescription(Publicas.OrdemRadarBI.PontesFeriados, "");
                    break;
            }

            if (_radar.Existe)
            {
                _radar.PercentualAnterior = _radar.Percentual;
                _radar.ValorAnterior = _radar.Valor;
            }

            _radar.EmpresaFilial = empresaComboBoxAdv.Text.Substring(0, 7);
            _radar.Grupo = rubricaComboBoxAdv.Text;
            _radar.Data = dataDateTimePicker.Value;
            _radar.Tipo = tipo;
            _radar.Ordem = Convert.ToInt32(ordem);

            _radar.Valor = valorCurrencyTextBox.DecimalValue;
            _radar.Percentual = percentualCurrencyTextBox.DecimalValue;

            if (!new RadarBIBO().Gravar(_radar))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

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

        private void excluirButton_Enter(object sender, EventArgs e)
        {
            excluirButton.BackColor = Publicas._botaoFocado;
            excluirButton.ForeColor = Publicas._fonteBotaoFocado;
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

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                percentualCurrencyTextBox.Focus();
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
    }
}
