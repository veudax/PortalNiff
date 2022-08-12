using Classes;
using Negocio;
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
    public partial class EditarProcessoParcelamento : Form
    {
        public EditarProcessoParcelamento()
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

        public Classes.Endividamento.Contrato _editarContrato;

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

        private void gravarButton_Enter(object sender, EventArgs e)
        {
            gravarButton.BackColor = Publicas._botaoFocado;
            gravarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                //PatText.Focus();
            }
        }

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            _editarContrato.NumeroContrato = NovoContratoTextBox.Text;


            if (!new EndividamentoBO().EditarNumeroContrato(_editarContrato))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Alterou o número do contrato '" + ContratoTextBox.Text + "' da empresa " + empresaComboBoxAdv.Text +
                " Fornecedor " + FornecedorTextBox.Text + " " + NomeFornecedorTextBox.Text + " Modalidade " + ModalidadeComboBox.Text +
                " Para ['" + NovoContratoTextBox.Text + "']";

            _log.Tela = "Contabilidade - Parcelamento - Valores";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            Publicas._novoContratoParcelamento = NovoContratoTextBox.Text;
            new Notificacoes.Mensagem("Gravado com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();
            Close();
        }

        private void EditarProcessoParcelamento_Shown(object sender, EventArgs e)
        {
            NovoContratoTextBox.Focus();
        }
    }
}
