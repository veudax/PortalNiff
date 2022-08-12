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

namespace Suportte.Cigam
{
    public partial class LimparTabela : Form
    {
        public LimparTabela()
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
                    //gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    //gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                    //gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                    //gridGroupingControl1.BackColor = Publicas._panelTitulo;

                    //inicialDateTimePicker.Style = VisualStyle.Office2016Black;
                    //finalDateTimePicker.Style = VisualStyle.Office2016Black;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
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

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void limparButton_Enter(object sender, EventArgs e)
        {
            limparButton.BackColor = Publicas._botaoFocado;
            limparButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void limparButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            limparButton.BackColor = Publicas._botao;
            limparButton.ForeColor = Publicas._fonteBotao;
        }
        
        private void ImportacaoManual_Shown(object sender, EventArgs e)
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

        private void inicialDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ProducaoRadioButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void inicialDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                finalDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                HomologacaoRadioButton.Focus();
            }
        }

        private void finalDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                limparButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                inicialDateTimePicker.Focus();
            }
        }

        private void ProducaoRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                inicialDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void HomologacaoRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                inicialDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            _empresa = null;

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == empresaComboBoxAdv.Text))
            {
                _empresa = item;
            }

            if (_empresa == null)
            {
                new Notificacoes.Mensagem("Selecione a empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            limparButton.Enabled = true;
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            if (!ProducaoRadioButton.Checked && !HomologacaoRadioButton.Checked)
            {
                new Notificacoes.Mensagem("Selecione uma das opções 'Produção' ou 'Homologaçao'.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ProducaoRadioButton.Focus();
                return;
            }

            if (inicialDateTimePicker.Value.Date > finalDateTimePicker.Value.Date)
            {
                new Notificacoes.Mensagem("Data inicial deve ser menor que a final.", Publicas.TipoMensagem.Alerta).ShowDialog();
                inicialDateTimePicker.Focus();
                return;
            }

            limparButton.Enabled = false;
            
            if (ProducaoRadioButton.Checked)
                Publicas.stringConexaoCigam = Publicas._conexaoStringCigamProducao;
            else
                Publicas.stringConexaoCigam = Publicas._conexaoStringCigamHomologacao;


            mensagemSistemaLabel.Text = "Limpando os dados, aguarde...";
            Refresh();

            if (!new CigamBO().LimparBase(_empresa.CodigoEmpresaGlobus, inicialDateTimePicker.Value.Date, finalDateTimePicker.Value.Date))
            {
                new Notificacoes.Mensagem("Problemas durante a limpeza dos dados." + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                empresaComboBoxAdv.Focus();

                mensagemSistemaLabel.Text = "";
                Refresh();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Limpou os dados da empresa " + empresaComboBoxAdv.Text + " na base "
                + (ProducaoRadioButton.Checked ? "produção" : "homologação")
                + " para o período de " + inicialDateTimePicker.Value.ToShortDateString() + " a " + finalDateTimePicker.Value.ToShortDateString();
            _log.Tela = "Contabilidade - Cigam - Limpeza de dados";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }
            new Notificacoes.Mensagem("Limpeza dos dados finalizado com Sucesso.", Publicas.TipoMensagem.Alerta).ShowDialog();

            mensagemSistemaLabel.Text = "";
            Refresh();
            empresaComboBoxAdv.Focus();
        }
    }
}
