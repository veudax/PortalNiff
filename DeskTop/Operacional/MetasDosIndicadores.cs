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
    public partial class MetasDosIndicadores : Form
    {
        public MetasDosIndicadores()
        {
            InitializeComponent();

            VigenciaDateTimePicker.BackColor = empresaComboBoxAdv.BackColor;
            PesoCurrencyTextBox.BackColor = empresaComboBoxAdv.BackColor;

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
                    ValorMetaCurrencyTextBox.PositiveColor = Publicas._fonte;
                    PesoCurrencyTextBox.PositiveColor = Publicas._fonte;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Operacional.Indicadores _indicadores;
        Classes.Operacional.Metas _metas;
        Classes.Operacional.Metas _metasLog;
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

        private void MetasDosIndicadores_Shown(object sender, EventArgs e)
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
                IndicadorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void VigenciaDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndicadorTextBox.Focus();
            }
        }


        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ValorMetaCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                VigenciaDateTimePicker.Focus();
            }
        }

        private void IndicadorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                VigenciaDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void ValorMetaCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PesoCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ValorMetaCurrencyTextBox.Focus();
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

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void VigenciaDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void IndicadorTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void ValorMetaCurrencyTextBox_Enter(object sender, EventArgs e)
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
                Publicas._idEmpresa = _empresa.IdEmpresa;
                Publicas._idRetornoPesquisa = _indicadores.Id;

                new Pesquisas.VigenciasDosIndicadores().ShowDialog();

                if (Publicas._codigoRetornoPesquisa.ToString() == "")
                {
                    VigenciaDateTimePicker.Focus();
                    return;
                }
                VigenciaDateTimePicker.Value = Convert.ToDateTime(Publicas._codigoRetornoPesquisa.ToString());
            }

            _metas = new OperacionalBO().ConsultarMetas(_empresa.IdEmpresa, _indicadores.Id, VigenciaDateTimePicker.Value);
            _metasLog = new OperacionalBO().ConsultarMetas(_empresa.IdEmpresa, _indicadores.Id, VigenciaDateTimePicker.Value);

            if (_metas.Existe)
            {
                ativoCheckBox.Checked = _metas.Ativo;
                ValorMetaCurrencyTextBox.DecimalValue = _metas.Meta;
                PesoCurrencyTextBox.DecimalValue = _metas.Meta;
                excluirButton.Enabled = true;
            }
            gravarButton.Enabled = true;
        }

        private void IndicadorTextBox_Validating(object sender, CancelEventArgs e)
        {
            IndicadorTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                PesquisaIndicadorButton.Enabled = false;
                return;
            }

            if (string.IsNullOrEmpty(IndicadorTextBox.Text))
            {
                new Pesquisas.Indicadores().ShowDialog();

                IndicadorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(IndicadorTextBox.Text) || IndicadorTextBox.Text == "0")
                {
                    IndicadorTextBox.Text = string.Empty;
                    IndicadorTextBox.Focus();
                    return;
                }
            }

            _indicadores = new OperacionalBO().ConsultarIndicadores(Convert.ToInt32(IndicadorTextBox.Text));

            if (!_indicadores.Existe)
            {
                new Notificacoes.Mensagem("Indicador não está cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                IndicadorTextBox.Focus();
                return;
            }

            if (!_indicadores.Ativo)
            {
                new Notificacoes.Mensagem("Indicador não está Ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                IndicadorTextBox.Focus();
                return;
            }

            DescricaoIndicadorTextBox.Text = _indicadores.Descricao;
        }
        
        private void ValorMetaCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ValorMetaCurrencyTextBox.BorderColor = Publicas._bordaSaida;
            PesoCurrencyTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void IndicadorTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaIndicadorButton.Enabled = string.IsNullOrEmpty(IndicadorTextBox.Text.Trim());
            PesquisaVigenciaButton.Enabled = !string.IsNullOrEmpty(IndicadorTextBox.Text.Trim());
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IndicadorTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o indicador.", Publicas.TipoMensagem.Alerta).ShowDialog();
                IndicadorTextBox.Focus();
                return;
            }

            _metas.Ativo = ativoCheckBox.Checked;
            _metas.Data = VigenciaDateTimePicker.Value;
            _metas.Peso = PesoCurrencyTextBox.DecimalValue;
            _metas.Meta = ValorMetaCurrencyTextBox.DecimalValue;
            _metas.IdEmpresa = _empresa.IdEmpresa;
            _metas.IdIndicador = _indicadores.Id;

            if (!new OperacionalBO().Gravar(_metas))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Tela = "Cadastro de metas do indicador";

            _log.Descricao = (_metas.Existe ? "Alterou " : "Incluiu ") + "a vigência " + VigenciaDateTimePicker.Value.ToShortDateString() + " da empresa "
                + empresaComboBoxAdv.Text + " e indicador " + IndicadorTextBox.Text;

            if (_metas.Existe)
            {
                _log.Descricao = _log.Descricao +
                (_metas.Peso == _metasLog.Peso ? "" : " [Peso] de " + _metasLog.Peso+ " para " + _metas.Peso + "") +
                (_metas.Meta == _metasLog.Meta ? "" : " [Meta] de " + _metasLog.Meta + " para " + _metas.Meta + "") +
                (_metas.Ativo == _metasLog.Ativo ? "" : " [Ativo] de " + _metasLog.Ativo.ToString() + " para " + _metas.Ativo.ToString() + "") + " ";
            }

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            IndicadorTextBox.Text = string.Empty;
            DescricaoIndicadorTextBox.Text = string.Empty;
            ativoCheckBox.Checked = false;
            ValorMetaCurrencyTextBox.DecimalValue = 0;
            PesoCurrencyTextBox.DecimalValue = 0;
            IndicadorTextBox.Focus();
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão da vigência?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu a vigência " + VigenciaDateTimePicker.Value.ToShortDateString() + " da empresa " + empresaComboBoxAdv.Text + " e indicador " + IndicadorTextBox.Text + " no valor de " + ValorMetaCurrencyTextBox.Text;

            _log.Descricao = _log.Descricao.Substring(0, _log.Descricao.Length - 2);

            _log.Tela = "Cadastro de metas do indicador";

            if (!new OperacionalBO().ExcluirMetas(_metas.Id))
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

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PesquisaVigenciaButton_Click(object sender, EventArgs e)
        {
            Publicas._idEmpresa = _empresa.IdEmpresa;
            Publicas._idRetornoPesquisa = _indicadores.Id;

            new Pesquisas.VigenciasDosIndicadores().ShowDialog();

            if (Publicas._codigoRetornoPesquisa.ToString() == "")
            {
                VigenciaDateTimePicker.Focus();
                return;
            }

            VigenciaDateTimePicker.Value = Convert.ToDateTime(Publicas._codigoRetornoPesquisa.ToString());

            VigenciaDateTimePicker_Validating(sender, new CancelEventArgs());
            ativoCheckBox.Focus();
        }

        private void PesoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ValorMetaCurrencyTextBox.Focus();
            }
        }
    }
}
