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

namespace Suportte.Avaliacao_de_desempenho
{
    public partial class EixoYNineBox : Form
    {
        public EixoYNineBox()
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

        #region Atributos
        Classes.Empresa _empresa;
        Classes.Colaboradores _colaboradores;
        Classes.Cargos _cargos;
        Classes.Pontuacao9Box _pontuacao9Box;
        Classes.FuncionariosGlobus _funcionariosGlobus;

        List<Classes.Empresa> _listaEmpresas;
        #endregion

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EixoYNineBox_Shown(object sender, EventArgs e)
        {
            if (cargosPanel.Visible)
                cargosTextBox.Focus();
            else
            {
                _listaEmpresas = new EmpresaBO().Listar(false);

                empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
                empresaComboBoxAdv.DisplayMember = "CodigoeNome";
                empresaComboBoxAdv.Focus();

                _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

                for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
                {
                    empresaComboBoxAdv.SelectedIndex = i;
                    if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                    {
                        break;
                    }
                }
                empresaComboBoxAdv.Focus();

                descontarLabel.Font = new Font(descontarLabel.Font, descontarLabel.Font.Style & ~FontStyle.Bold);
                toleranciaExtroversaoLabel.Font = new Font(toleranciaExtroversaoLabel.Font, toleranciaExtroversaoLabel.Font.Style & ~FontStyle.Bold);
                toleranciaDominanciaLabel.Font = new Font(toleranciaDominanciaLabel.Font, toleranciaDominanciaLabel.Font.Style & ~FontStyle.Bold);
                toleranciaFormalidadeLabel.Font = new Font(toleranciaFormalidadeLabel.Font, toleranciaFormalidadeLabel.Font.Style & ~FontStyle.Bold);
                toleranciaPacienciaLabel.Font = new Font(toleranciaPacienciaLabel.Font, toleranciaPacienciaLabel.Font.Style & ~FontStyle.Bold);
                toleranciaDominanciaCurrencyTextBox.Enabled = false;
                toleranciaExtroversaoCurrencyTextBox.Enabled = false;
                toleranciaFormalidadeCurrencyTextBox.Enabled = false;
                toleranciaPacienciaCurrencyTextBox.Enabled = false;
                descontarCurrencyText.Enabled = false;
            }
            
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                usuarioTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void usuarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                referenciaMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void cargosTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                referenciaMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cargosTextBox.Focus();
            }
        }

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (descontarCurrencyText.Enabled)
                    descontarCurrencyText.Focus();
                else
                    ativoCheckBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (cargosPanel.Visible)
                    cargosTextBox.Focus();
                else
                    usuarioTextBox.Focus();
            }
        }

        private void descontarCurrencyText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ativoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                referenciaMaskedEditBox.Focus();
            }
        }

        private void ativoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                pesoDominanciaCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;

                if (descontarCurrencyText.Enabled)
                    descontarCurrencyText.Focus();
                else
                    referenciaMaskedEditBox.Focus();
            }
        }

        private void pesoDominanciaCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (toleranciaDominanciaCurrencyTextBox.Enabled)
                    toleranciaDominanciaCurrencyTextBox.Focus();
                else
                    pesoExtroversaoCurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ativoCheckBox.Focus();
            }
        }

        private void toleranciaDominanciaCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                pesoExtroversaoCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pesoDominanciaCurrencyTextBox.Focus();
            }
        }

        private void pesoExtroversaoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (toleranciaExtroversaoCurrencyTextBox.Enabled)
                    toleranciaExtroversaoCurrencyTextBox.Focus();
                else
                    pesoPacienciaCurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;

                if (toleranciaDominanciaCurrencyTextBox.Enabled)
                    toleranciaDominanciaCurrencyTextBox.Focus();
                else
                    pesoDominanciaCurrencyTextBox.Focus();
            }
        }

        private void toleranciaExtroversaoCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                pesoPacienciaCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pesoExtroversaoCurrencyTextBox.Focus();
            }
        }

        private void pesoPacienciaCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (toleranciaPacienciaCurrencyTextBox.Enabled)
                    toleranciaPacienciaCurrencyTextBox.Focus();
                else
                    pesoFormalidadeCurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;

                if (toleranciaExtroversaoCurrencyTextBox.Enabled)
                    toleranciaExtroversaoCurrencyTextBox.Focus();
                else
                    pesoExtroversaoCurrencyTextBox.Focus();
            }
        }

        private void toleranciaPacienciaCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                pesoFormalidadeCurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pesoPacienciaCurrencyTextBox.Focus();
            }
        }

        private void pesoFormalidadeCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (toleranciaFormalidadeCurrencyTextBox.Enabled)
                    toleranciaFormalidadeCurrencyTextBox.Focus();
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;

                if (toleranciaPacienciaCurrencyTextBox.Enabled)
                    toleranciaPacienciaCurrencyTextBox.Focus();
                else
                    pesoPacienciaCurrencyTextBox.Focus();
            }
        }

        private void toleranciaFormalidadeCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                pesoFormalidadeCurrencyTextBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;

                if (toleranciaFormalidadeCurrencyTextBox.Enabled)
                    toleranciaFormalidadeCurrencyTextBox.Focus();
                else
                    pesoFormalidadeCurrencyTextBox.Focus();
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

        private void usuarioTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaCargoButton.Enabled = string.IsNullOrEmpty(cargosTextBox.Text.Trim());
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void descontarCurrencyText_Enter(object sender, EventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
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

        private void pesoDominanciaCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;
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
        
        private void EixoYNineBox_Load(object sender, EventArgs e)
        {
            if (cargosPanel.Visible)
                this.Size = new Size(this.Width, this.Height - colaboradorPanel.Height);
            else
                this.Size = new Size(this.Width, this.Height - cargosPanel.Height);
        }

        private void cargosTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCargoButton.Enabled = string.IsNullOrEmpty(cargosTextBox.Text.Trim());
        }

        private void referenciaMaskedEditBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }

        private void usuarioTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void pesquisaCargoButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cargosTextBox.Text))
            {
                new Pesquisas.Cargos().ShowDialog();

                cargosTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (cargosTextBox.Text == "" || cargosTextBox.Text == "0")
                {
                    cargosTextBox.Text = string.Empty;
                    cargosTextBox.Focus();
                    return;
                }

                cargosTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaUsuarioButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._idSuperior = 0;

                new Pesquisas.Funcionarios().ShowDialog();

                usuarioTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(usuarioTextBox.Text) || usuarioTextBox.Text == "0")
                {
                    usuarioTextBox.Text = string.Empty;
                    usuarioTextBox.Focus();
                    return;
                }

                usuarioTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void pesquisaReferenciaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                Publicas._idRetornoPesquisa = 0;
                Publicas._porCargo = cargosPanel.Visible;
                Publicas._idComunicado = (cargosPanel.Visible ? _cargos.Id : _colaboradores.Id);

                new Pesquisas.Pontuacao9Box().ShowDialog();

                if (Publicas._idRetornoPesquisa == 0)
                {
                    referenciaMaskedEditBox.Text = string.Empty;
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                _pontuacao9Box = new Pontuacao9BoxBO().Consultar(Publicas._idRetornoPesquisa);

                PopulaCampo();
                descontarCurrencyText.Focus();
            }
        }

        private void PopulaCampo()
        {
            gravarButton.Enabled = true;
            if (_pontuacao9Box.Existe)
            {
                ativoCheckBox.Checked = _pontuacao9Box.Ativo;
                referenciaMaskedEditBox.Text = _pontuacao9Box.Referencia.ToString("000000");

                descontarCurrencyText.DecimalValue = 0;
                toleranciaDominanciaCurrencyTextBox.DecimalValue = 0;
                toleranciaExtroversaoCurrencyTextBox.DecimalValue = 0;
                toleranciaFormalidadeCurrencyTextBox.DecimalValue = 0;
                toleranciaPacienciaCurrencyTextBox.DecimalValue = 0;

                if (cargosPanel.Visible)
                {
                    descontarCurrencyText.DecimalValue = _pontuacao9Box.Descontar;

                    toleranciaDominanciaCurrencyTextBox.DecimalValue = _pontuacao9Box.ToleranciaDominancia;
                    toleranciaExtroversaoCurrencyTextBox.DecimalValue = _pontuacao9Box.ToleranciaExtroversao;
                    toleranciaFormalidadeCurrencyTextBox.DecimalValue = _pontuacao9Box.ToleranciaFormalidade;
                    toleranciaPacienciaCurrencyTextBox.DecimalValue = _pontuacao9Box.ToleranciaPaciencia;
                }

                pesoDominanciaCurrencyTextBox.DecimalValue = _pontuacao9Box.PontoDominancia;
                pesoExtroversaoCurrencyTextBox.DecimalValue = _pontuacao9Box.PontoExtroversao;
                pesoFormalidadeCurrencyTextBox.DecimalValue = _pontuacao9Box.PontoFormalidade;
                pesoPacienciaCurrencyTextBox.DecimalValue = _pontuacao9Box.PontoPaciencia;
                
                excluirButton.Enabled = _pontuacao9Box.Existe;
            }
        }

        private void cargosTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                cargosTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(cargosTextBox.Text))
            {
                new Pesquisas.Cargos().ShowDialog();

                cargosTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(cargosTextBox.Text) || cargosTextBox.Text == "0")
                {
                    cargosTextBox.Text = string.Empty;
                    cargosTextBox.Focus();
                    return;
                }
            }

            _cargos = new CargosBO().Consultar(Convert.ToInt32(cargosTextBox.Text));

            if (!_cargos.Existe)
            {
                new Notificacoes.Mensagem("Cargo não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                cargosTextBox.Focus();
                return;
            }

            descricaoCargoTextBox.Text = _cargos.Descricao;
            referenciaMaskedEditBox.Focus();
        }


        private void usuarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            usuarioTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                usuarioTextBox.Text = string.Empty;
                nomeTextBox.Text = string.Empty;
                pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
            {
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._idSuperior = 0;

                new Pesquisas.Funcionarios().ShowDialog();

                usuarioTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(usuarioTextBox.Text) || usuarioTextBox.Text == "0")
                {
                    usuarioTextBox.Text = string.Empty;
                    usuarioTextBox.Focus();
                    return;
                }
            }

            usuarioTextBox.Text = usuarioTextBox.Text.PadLeft(6, '0');
            if (usuarioTextBox.Text == Publicas._usuario.RegistroFuncionario)
            {
                new Notificacoes.Mensagem("Registro do funcionário deve ser diferente do seu registro.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(usuarioTextBox.Text, _empresa.CodigoEmpresaGlobus);

            _colaboradores = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, usuarioTextBox.Text, false);

            if (!_funcionariosGlobus.Existe)
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            if (_funcionariosGlobus.DataDesligamento != DateTime.MinValue && !_funcionariosGlobus.Ativo)
            {
                new Notificacoes.Mensagem("Colaborador desligado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _funcionariosGlobus.Nome;
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            referenciaMaskedEditBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                referenciaMaskedEditBox.Text = string.Empty;
                pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                Publicas._idRetornoPesquisa = 0;
                Publicas._porCargo = cargosPanel.Visible;
                Publicas._idComunicado = (cargosPanel.Visible ? _cargos.Id : _colaboradores.Id);

                new Pesquisas.Pontuacao9Box().ShowDialog();

                if (Publicas._idRetornoPesquisa == 0)
                {
                    referenciaMaskedEditBox.Text = string.Empty;
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                _pontuacao9Box = new Pontuacao9BoxBO().Consultar(Publicas._idRetornoPesquisa);

                PopulaCampo();
                descontarCurrencyText.Focus();
                return;
            }

            if (cargosPanel.Visible)
                _pontuacao9Box = new Pontuacao9BoxBO().Consultar(referenciaMaskedEditBox.ClipText.Trim(), _cargos.Id, 0);
            else
                _pontuacao9Box = new Pontuacao9BoxBO().Consultar(referenciaMaskedEditBox.ClipText.Trim(), 0, _colaboradores.Id);
            
            PopulaCampo();
            descontarCurrencyText.Focus();
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            cargosTextBox.Text = string.Empty;
            descricaoCargoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            usuarioTextBox.Text = string.Empty;
            referenciaMaskedEditBox.Text = string.Empty;
            descontarCurrencyText.DecimalValue = 0;
            pesoDominanciaCurrencyTextBox.DecimalValue = 0;
            pesoExtroversaoCurrencyTextBox.DecimalValue = 0;
            pesoFormalidadeCurrencyTextBox.DecimalValue = 0;
            pesoPacienciaCurrencyTextBox.DecimalValue = 0;
            toleranciaDominanciaCurrencyTextBox.DecimalValue = 0;
            toleranciaExtroversaoCurrencyTextBox.DecimalValue = 0;
            toleranciaFormalidadeCurrencyTextBox.DecimalValue = 0;
            toleranciaPacienciaCurrencyTextBox.DecimalValue = 0;
            ativoCheckBox.Checked = false;

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;

            if (cargosPanel.Visible)
                cargosTextBox.Focus();
            else
                usuarioTextBox.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_pontuacao9Box == null)
                _pontuacao9Box = new Pontuacao9Box();

            _pontuacao9Box.Ativo = ativoCheckBox.Checked;
            _pontuacao9Box.Descontar = descontarCurrencyText.DecimalValue;
            _pontuacao9Box.Referencia = Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim());

            if (!string.IsNullOrEmpty(cargosTextBox.Text.Trim()))
                _pontuacao9Box.IdCargo = _cargos.Id;

            if (!string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
                _pontuacao9Box.IdColaborador = _colaboradores.Id;

            _pontuacao9Box.PontoDominancia = pesoDominanciaCurrencyTextBox.DecimalValue;
            _pontuacao9Box.PontoExtroversao = pesoExtroversaoCurrencyTextBox.DecimalValue;
            _pontuacao9Box.PontoFormalidade = pesoFormalidadeCurrencyTextBox.DecimalValue;
            _pontuacao9Box.PontoPaciencia = pesoPacienciaCurrencyTextBox.DecimalValue;

            _pontuacao9Box.ToleranciaDominancia = toleranciaDominanciaCurrencyTextBox.DecimalValue;
            _pontuacao9Box.ToleranciaExtroversao = toleranciaExtroversaoCurrencyTextBox.DecimalValue;
            _pontuacao9Box.ToleranciaFormalidade = toleranciaFormalidadeCurrencyTextBox.DecimalValue;
            _pontuacao9Box.ToleranciaPaciencia = toleranciaPacienciaCurrencyTextBox.DecimalValue;

            if (!new Pontuacao9BoxBO().Gravar(_pontuacao9Box))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new Pontuacao9BoxBO().Excluir(_pontuacao9Box.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
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
    }
}
