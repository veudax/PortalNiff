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
    public partial class ValoresDosIndicadores : Form
    {
        public ValoresDosIndicadores()
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
                    ProgramadoCurrencyTextBox.PositiveColor = Publicas._fonte;
                    RealizadoCurrencyTextBox.PositiveColor = Publicas._fonte;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Linha _linha;
        Classes.Operacional.Indicadores _indicadores;
        Classes.Operacional.Valores _valores;
        Classes.Operacional.Valores _valoresLog;
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

        private void ValoresDosIndicadores_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                VigenciaDateTimePicker.Focus();
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
                IndicadorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void IndicadorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PeriodoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                VigenciaDateTimePicker.Focus();
            }
        }

        private void PeriodoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                LinhaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                IndicadorTextBox.Focus();
            }
        }

        private void LinhaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (ProgramadoCurrencyTextBox.Visible)
                    ProgramadoCurrencyTextBox.Focus();
                else
                    RealizadoCurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PeriodoComboBox.Focus();
            }
        }

        private void ProgramadoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (RealizadoCurrencyTextBox.Visible)
                    RealizadoCurrencyTextBox.Focus();
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                LinhaTextBox.Focus();
            }
        }

        private void RealizadoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (ProgramadoCurrencyTextBox.Visible)
                    ProgramadoCurrencyTextBox.Focus();
                else
                    LinhaTextBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (RealizadoCurrencyTextBox.Visible)
                    RealizadoCurrencyTextBox.Focus();
                else
                    ProgramadoCurrencyTextBox.Focus();
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
            PesquisaVigenciaButton.Enabled = true;
        }

        private void IndicadorTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void ProgramadoCurrencyTextBox_Enter(object sender, EventArgs e)
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
            ProgramadoCurrencyTextBox.Visible = _indicadores.TipoDeValores != "R";
            ProgramadoLabel.Visible = _indicadores.TipoDeValores != "R";
            RealizadoCurrencyTextBox.Visible = _indicadores.TipoDeValores != "P";
            RealizadoLabel.Visible = _indicadores.TipoDeValores != "P";
        }

        private void PeriodoComboBox_Validating(object sender, CancelEventArgs e)
        {
            PeriodoComboBox.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void LinhaTextBox_Validating(object sender, CancelEventArgs e)
        {
            LinhaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                PesquisaLinhaButton.Enabled = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                return;
            }

            Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;

            if (string.IsNullOrEmpty(LinhaTextBox.Text))
            {
                new Pesquisas.Linhas().ShowDialog();

                LinhaTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (Publicas._codigoRetornoPesquisa.ToString() == "")
                {
                    LinhaTextBox.Focus();
                    return;
                }
            }

            _linha = new LinhaBO().Consultar(_empresa.CodigoEmpresaGlobus, LinhaTextBox.Text);

            if (!_linha.Existe)
            {
                new Notificacoes.Mensagem("Linha não está cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                LinhaTextBox.Focus();
                return;
            }

            if (!_linha.Ativo)
            {
                new Notificacoes.Mensagem("Linha não está Ativa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                LinhaTextBox.Focus();
                return;
            }

            NomeLinhaTextBox.Text = _linha.Nome;

            _valores = new OperacionalBO().Consultar(_empresa.IdEmpresa, VigenciaDateTimePicker.Value, _indicadores.Id, PeriodoComboBox.Text.Substring(0, 1), _linha.Id);
            _valoresLog = new OperacionalBO().Consultar(_empresa.IdEmpresa, VigenciaDateTimePicker.Value, _indicadores.Id, PeriodoComboBox.Text.Substring(0, 1), _linha.Id);

            if (_valores.Existe)
            {
                ProgramadoCurrencyTextBox.DecimalValue = _valores.Programado;
                RealizadoCurrencyTextBox.DecimalValue = _valores.Realizado;
                excluirButton.Enabled = true;
            }
            else
            {
                if (DescricaoIndicadorTextBox.Text.ToUpper().Contains("QUADRO"))
                    ProgramadoCurrencyTextBox.DecimalValue = new OperacionalBO().QuantidadeFuncionariosEscalados(_empresa.IdEmpresa, VigenciaDateTimePicker.Value, _linha.Id, PeriodoComboBox.Text.Substring(0, 1));
                if (DescricaoIndicadorTextBox.Text.ToUpper().Contains("FROTA") || DescricaoIndicadorTextBox.Text.ToUpper().Contains("F R O T A"))
                    ProgramadoCurrencyTextBox.DecimalValue = new OperacionalBO().QuantidadeVeiculosEscalados(_empresa.IdEmpresa, VigenciaDateTimePicker.Value, _linha.Id, PeriodoComboBox.Text.Substring(0, 1));
                if (DescricaoIndicadorTextBox.Text.ToUpper().Contains("F.C.V") || DescricaoIndicadorTextBox.Text.ToUpper().Contains("FCV"))
                    ProgramadoCurrencyTextBox.DecimalValue = new OperacionalBO().FCV(_empresa.IdEmpresa, VigenciaDateTimePicker.Value, _linha.Id, PeriodoComboBox.Text.Substring(0, 1));

                if (DescricaoIndicadorTextBox.Text.ToUpper().Contains("SOCORRO") || DescricaoIndicadorTextBox.Text.ToUpper().Contains("SOS"))
                {
                    RealizadoCurrencyTextBox.DecimalValue = new OperacionalBO().SOS(_empresa.IdEmpresa, VigenciaDateTimePicker.Value, _linha.Id) ?? 0;
                }
                if (DescricaoIndicadorTextBox.Text.ToUpper().Contains("RECOLHIDA") || DescricaoIndicadorTextBox.Text.ToUpper().Contains("RA"))
                    RealizadoCurrencyTextBox.DecimalValue = new OperacionalBO().SOS(_empresa.IdEmpresa, VigenciaDateTimePicker.Value, _linha.Id) ?? 0;
                if (DescricaoIndicadorTextBox.Text.ToUpper().Contains("QUANTIDADE DE PASS") || DescricaoIndicadorTextBox.Text.ToUpper().Contains("PAX"))
                    RealizadoCurrencyTextBox.DecimalValue = new OperacionalBO().PAX(_empresa.IdEmpresa, VigenciaDateTimePicker.Value, _linha.Id);
                if (DescricaoIndicadorTextBox.Text.ToUpper().Contains("KM") )
                    RealizadoCurrencyTextBox.DecimalValue = new OperacionalBO().KM(_empresa.IdEmpresa, VigenciaDateTimePicker.Value, _linha.Id);
                if (DescricaoIndicadorTextBox.Text.ToUpper().Contains("CONSUMO"))
                    RealizadoCurrencyTextBox.DecimalValue = new OperacionalBO().Consumo(_empresa.IdEmpresa, VigenciaDateTimePicker.Value, _linha.Id);
            }

            gravarButton.Enabled = true;
        }
        
        private void ProgramadoCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ProgramadoCurrencyTextBox.BorderColor = Publicas._bordaSaida;
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void RealizadoCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            RealizadoCurrencyTextBox.BorderColor = Publicas._bordaSaida;
            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void IndicadorTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaIndicadorButton.Enabled = string.IsNullOrEmpty(IndicadorTextBox.Text.Trim());
        }

        private void LinhaTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaLinhaButton.Enabled = string.IsNullOrEmpty(LinhaTextBox.Text.Trim());
        }

        private void PesquisaVigenciaButton_Click(object sender, EventArgs e)
        {
            Publicas._idEmpresa = _empresa.IdEmpresa;

            new Pesquisas.ValoresOperacional().ShowDialog();

            if (Publicas._idRetornoPesquisa.ToString() == "0")
            {
                VigenciaDateTimePicker.Focus();
                return;
            }

            _valores = new OperacionalBO().Consultar(Publicas._idRetornoPesquisa);
            _valoresLog = new OperacionalBO().Consultar(Publicas._idRetornoPesquisa);

            if (_valores.Existe)
            {
                VigenciaDateTimePicker.Value = _valores.Data;

                _indicadores = new OperacionalBO().ConsultarIndicadores(_valores.IdIndicador);
                IndicadorTextBox.Text = _valores.IdIndicador.ToString();
                DescricaoIndicadorTextBox.Text = _indicadores.Descricao;
                PeriodoComboBox.SelectedIndex = (_valores.Periodo == "U" ? 0 : (_valores.Periodo == "1" ? 1 : 2));
                _linha = new LinhaBO().Consultar(_empresa.CodigoEmpresaGlobus, LinhaTextBox.Text);
                LinhaTextBox.Text = _valores.CodigoLinha;
                NomeLinhaTextBox.Text = _linha.Nome;
                ProgramadoCurrencyTextBox.DecimalValue = _valores.Programado;
                RealizadoCurrencyTextBox.DecimalValue = _valores.Realizado;

                DescricaoIndicadorTextBox.Text = _indicadores.Descricao;
                ProgramadoCurrencyTextBox.Visible = _indicadores.TipoDeValores != "R";
                ProgramadoLabel.Visible = _indicadores.TipoDeValores != "R";
                RealizadoCurrencyTextBox.Visible = _indicadores.TipoDeValores != "P";
                RealizadoLabel.Visible = _indicadores.TipoDeValores != "P";

                if (ProgramadoCurrencyTextBox.Visible)
                    ProgramadoCurrencyTextBox.Focus();
                else
                    RealizadoCurrencyTextBox.Focus();

                excluirButton.Enabled = true;
                gravarButton.Enabled = true;
            }

        }

        private void PesquisaLinhaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LinhaTextBox.Text))
            {
                new Pesquisas.Linhas().ShowDialog();

                LinhaTextBox.Text = Publicas._codigoRetornoPesquisa.ToString();

                if (Publicas._codigoRetornoPesquisa.ToString() == "")
                {
                    LinhaTextBox.Focus();
                    return;
                }

                LinhaTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void PesquisaIndicadorButton_Click(object sender, EventArgs e)
        {
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

                IndicadorTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IndicadorTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe o indicador.", Publicas.TipoMensagem.Alerta).ShowDialog();
                IndicadorTextBox.Focus();
                return;
            }

            _valores.IdEmpresa = _empresa.IdEmpresa;
            _valores.Data = VigenciaDateTimePicker.Value;
            _valores.CodigoInternoLinha = _linha.Id;
            _valores.IdIndicador = _indicadores.Id;
            _valores.Periodo = PeriodoComboBox.Text.Substring(0, 1);
            _valores.Programado = ProgramadoCurrencyTextBox.DecimalValue;
            _valores.Realizado = RealizadoCurrencyTextBox.DecimalValue;

            if (!new OperacionalBO().Gravar(_valores))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Tela = "Cadastro de valores do indicador";

            _log.Descricao = (_valores.Existe ? "Alterou " : "Incluiu ") + "o valor do indicador" + " da empresa "
                + empresaComboBoxAdv.Text + " da data " + VigenciaDateTimePicker.Value.ToShortDateString() + " periodo " + PeriodoComboBox.Text +
                " indicador " + IndicadorTextBox.Text + " linha " + LinhaTextBox.Text;

            if (_valores.Existe)
            {
                _log.Descricao = _log.Descricao +
                (_valores.Programado == _valoresLog.Programado ? "" : " [Programado] de " + _valoresLog.Programado.ToString() + " para " + _valores.Programado.ToString() + "") +
                (_valores.Realizado == _valoresLog.Realizado ? "" : " [Realizado] de " + _valoresLog.Realizado.ToString() + " para " + _valores.Realizado.ToString() + "") + " ";
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
            LinhaTextBox.Text = string.Empty;
            NomeLinhaTextBox.Text = string.Empty;
            ProgramadoCurrencyTextBox.DecimalValue = 0;
            RealizadoCurrencyTextBox.DecimalValue = 0;
            LinhaTextBox.Focus();
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão do valor ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu o valor do indicador" + " da empresa " + empresaComboBoxAdv.Text +
                " da data " + VigenciaDateTimePicker.Value.ToShortDateString() + " periodo " + PeriodoComboBox.Text +
                " indicador " + IndicadorTextBox.Text + " linha " + LinhaTextBox.Text +
                (ProgramadoCurrencyTextBox.Visible ? " programado " + ProgramadoCurrencyTextBox.DecimalValue.ToString() :
                " realizado " + RealizadoCurrencyTextBox.DecimalValue.ToString());

            _log.Tela = "Cadastro de valores do indicador";

            if (!new OperacionalBO().ExcluirValores(_valores.Id))
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

        private void IndicadorLabel_Click(object sender, EventArgs e)
        {
            new Operacional.Indicadores().ShowDialog();
        }

        private void IndicadorLabel_MouseHover(object sender, EventArgs e)
        {
            IndicadorLabel.Cursor = Cursors.Hand;
        }

        private void IndicadorLabel_MouseLeave(object sender, EventArgs e)
        {
            IndicadorLabel.Cursor = Cursors.Hand;
        }
    }
}
