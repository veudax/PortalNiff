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

namespace Suportte.Cadastros
{
    public partial class EMTU : Form
    {
        public EMTU()
        {
            InitializeComponent();

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        TipoDeAtendimentoEMTU _tipo;
        Departamento _departamento;
        TipoDeAtendimento _tipoAtendimento;

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

        private void departamentoLabel_Click(object sender, EventArgs e)
        {
            new Departamentos().ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            new TiposDeAtendimento().Show();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            tituloTextBox.Text = string.Empty;
            descricaoTextBox.Text = string.Empty;
            descricaoTipoAtendimentoTextBox.Text = string.Empty;
            descricaoDeptoTextBox.Text = string.Empty;
            setorTextBox.Text = string.Empty;
            tipoAtendimentoTextBox.Text = string.Empty;

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            codigoTextBox.Focus();
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void tituloTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tituloTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void tituloTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void descricaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                setorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void tipoAtendimentoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                setorTextBox.Focus();
            }
        }

        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') 
                e.Handled = true;
           
            if (e.KeyChar == '+')
            {
                codigoTextBox.Text = string.Empty;
                proximoButton_Click(sender, e);
            }
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new TipoDeAtendimentoEMTUBO().Proximo(codigoTextBox.Text).ToString();
            tituloTextBox.Focus();
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
            
            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.EMTU().ShowDialog();

                codigoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (codigoTextBox.Text.Trim() == "")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            if (codigoTextBox.Text.Trim().Length > 5)
            {
                new Notificacoes.Mensagem("Tamanho do código maior que o permitido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            _tipo = new TipoDeAtendimentoEMTUBO().Consultar(codigoTextBox.Text);

            if (_tipo != null)
            {
                tituloTextBox.Text = _tipo.Descricao;
                ativoCheckBox.Checked = _tipo.Ativo;
                descricaoTextBox.Text = _tipo.Descricao;

                if (_tipo.IdDepartamento != 0)
                    setorTextBox.Text = _tipo.IdDepartamento.ToString();

                descricaoDeptoTextBox.Text = _tipo.Departamento;
                descricaoTipoAtendimentoTextBox.Text = _tipo.TipoAtendimento;

                if (_tipo.IdTipoAtendimento != 0)
                    tipoAtendimentoTextBox.Text = _tipo.IdTipoAtendimento.ToString();

            }

            excluirButton.Enabled = _tipo.Existe;
            gravarButton.Enabled = true;

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._codigoRetornoPesquisa != "")
                tituloTextBox.Focus();
        }

        private void setorTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (String.IsNullOrEmpty(setorTextBox.Text))
            {
                new Pesquisas.Departamentos().ShowDialog();

                setorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (setorTextBox.Text == "" || setorTextBox.Text == "0")
                {
                    setorTextBox.Text = string.Empty;
                    setorTextBox.Focus();
                    return;
                }
            }
                _departamento = new DepartamentoBO().Consultar(Convert.ToInt32(setorTextBox.Text));

                if (_departamento == null)
                {
                    new Notificacoes.Mensagem("Departamento não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    setorTextBox.Focus();
                    return;
                }

                if (!_departamento.Ativo)
                {
                    new Notificacoes.Mensagem("Departamento inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    setorTextBox.Focus();
                    return;
                }

                setorTextBox.Text = _departamento.Id.ToString();
                descricaoDeptoTextBox.Text = _departamento.Descricao;


            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._idRetornoPesquisa != 0)
                tipoAtendimentoTextBox.Focus();
        }

        private void tipoAtendimentoTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (String.IsNullOrEmpty(tipoAtendimentoTextBox.Text))
            {
                new Pesquisas.TiposDeAtendimento().ShowDialog();

                tipoAtendimentoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (tipoAtendimentoTextBox.Text == "" || tipoAtendimentoTextBox.Text == "0")
                {
                    tipoAtendimentoTextBox.Text = string.Empty;
                    tipoAtendimentoTextBox.Focus();
                    return;
                }
            }

                _tipoAtendimento = new TipoDeAtendimentoBO().Consultar(Convert.ToInt32(tipoAtendimentoTextBox.Text));

                if (_tipoAtendimento == null)
                {
                    new Notificacoes.Mensagem("Tipo de atendimento não cadastrado", Publicas.TipoMensagem.Alerta).ShowDialog();
                    tipoAtendimentoTextBox.Focus();
                    return;
                }

                if (!_tipoAtendimento.Ativo)
                {
                    new Notificacoes.Mensagem("Tipo de atendimento inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    tipoAtendimentoTextBox.Focus();
                    return;
                }

                tipoAtendimentoTextBox.Text = _tipoAtendimento.IdTipoAtendimento.ToString();
                descricaoTipoAtendimentoTextBox.Text = _tipoAtendimento.Descricao;
            

            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._idRetornoPesquisa != 0)
                gravarButton.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new TipoDeAtendimentoEMTUBO().Excluir(_tipo))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tituloTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a titulo!", Publicas.TipoMensagem.Alerta).ShowDialog();
                tituloTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(descricaoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a descrição!", Publicas.TipoMensagem.Alerta).ShowDialog();
                descricaoTextBox.Focus();
                return;
            }

            _tipo.Codigo = codigoTextBox.Text;
            _tipo.Descricao = descricaoTextBox.Text;
            _tipo.Ativo = ativoCheckBox.Checked;
            _tipo.Titulo = tituloTextBox.Text;
            _tipo.IdDepartamento = _departamento.Id;
            _tipo.IdTipoAtendimento = _tipoAtendimento.IdTipoAtendimento;

            if (!new TipoDeAtendimentoEMTUBO().Gravar(_tipo))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void setorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                tipoAtendimentoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                descricaoTextBox.Focus();
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
                tipoAtendimentoTextBox.Focus();
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

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                descricaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tituloTextBox.Focus();
            }
        }

        private void departamentoLabel_MouseHover(object sender, EventArgs e)
        {
            departamentoLabel.Cursor = Cursors.Hand;
        }

        private void departamentoLabel_MouseLeave(object sender, EventArgs e)
        {
            departamentoLabel.Cursor = Cursors.Default;
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            label4.Cursor = Cursors.Hand;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.Cursor = Cursors.Default;
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaEmpresaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void setorTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaDepartamentoButton.Enabled = string.IsNullOrEmpty(setorTextBox.Text.Trim());
        }

        private void tipoAtendimentoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaTipoButton.Enabled = string.IsNullOrEmpty(tipoAtendimentoTextBox.Text.Trim());
        }

        private void pesquisaEmpresaButton_Click(object sender, EventArgs e)
        {
            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.EMTU().ShowDialog();

                codigoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (codigoTextBox.Text.Trim() == "")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }

                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void setorTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tipoAtendimentoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void pesquisaDepartamentoButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(setorTextBox.Text))
            {
                new Pesquisas.Departamentos().ShowDialog();

                setorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (setorTextBox.Text == "" || setorTextBox.Text == "0")
                {
                    setorTextBox.Text = string.Empty;
                    setorTextBox.Focus();
                    return;
                }

                setorTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaTipoButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tipoAtendimentoTextBox.Text))
            {
                new Pesquisas.TiposDeAtendimento().ShowDialog();

                tipoAtendimentoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (tipoAtendimentoTextBox.Text == "" || tipoAtendimentoTextBox.Text == "0")
                {
                    tipoAtendimentoTextBox.Text = string.Empty;
                    tipoAtendimentoTextBox.Focus();
                    return;
                }

                tipoAtendimentoTextBox_Validating(sender, new CancelEventArgs());
            }
        }
    }
}
