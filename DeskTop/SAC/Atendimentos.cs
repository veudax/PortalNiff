using Classes;
using Negocio;
using Suportte.Notificacoes;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.SAC
{
    public partial class Atendimentos : Form
    {
        public Atendimentos()
        {
            InitializeComponent();

            aberturaDateTimePicker.BorderColor = Publicas._bordaSaida;
            fechamentoDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        List<Linha> _linhas;
        List<SecaoDaLinha> _secaoDaLinha;
        Empresa _empresa;
        Atendimento _atendimento;
        Atendimento _atendimentoLog;
        List<Classes.Atendimento.Anexos> _anexos;
        TipoDeAtendimentoEMTU _tipoEMTU;
        TipoDeAtendimento _tipoAtendimento;
        Usuario _usuarioResponsavel;
        Usuario _usuarioRespostaCliente;
        Usuario _usuarioAbertura;
        Usuario _usuarioRetorno;
        List<Usuario> _listaUsuarios;

        FuncionariosGlobus _funcionariosGlobus;

        string[] arquivo;
        int _topArquivos = 8;
        int _qtd = 0;

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

        private void Atendimentos_Load(object sender, EventArgs e)
        {
            List<TipoDeAtendimento> _lista = new TipoDeAtendimentoBO().Listar();
            tipoAcessoComboBox.DataSource = _lista;
            tipoAcessoComboBox.DisplayMember = "Descricao";
            tipoAcessoComboBox.SelectedIndex = 0;

            statusComboBox.Items.AddRange(new object[] { "Ativo", "Cancelado", "Respondido", "Finalizado" });
            statusComboBox.SelectedIndex = 0;

            situacaoComboBox.Items.AddRange(new object[] { "Manter com o Atendente", "Enviado ao Colaborador", "Enviado ao Finalizador", "Cancelado", "Finalizado" });
            situacaoComboBox.SelectedIndex = 0;

            opcaoRetornoComboBox.Items.AddRange(new object[] { "Telefone", "Fax", "E-mail", "Nenhum" });
            opcaoRetornoComboBox.SelectedIndex = 0;

            origemReclamacaoComboBox.Items.AddRange(new object[] { "Pessoalmente", "Internet", "Telefone", "E-mail", "Orgão Gestor", "Jornal e Rádio" });
            origemReclamacaoComboBox.SelectedIndex = 0;

            ufComboBox.Items.AddRange(new object[] { "-", "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" });
            ufComboBox.SelectedIndex = 0;

            // Empresa do usuario logado
            if (Publicas._usuario.Administrador)
                _empresa = new EmpresaBO().Consultar(5);
           else
                _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

            try
            {
                if (Publicas._usuario.Administrador)
                    _linhas = new LinhaBO().Listar("003/001");
                else
                    _linhas = new LinhaBO().Listar(_empresa.CodigoEmpresaGlobus);
            }
            catch { }
            

            linhaComboBox.DataSource = _linhas.OrderBy(o => o.Nome).ToList();
            linhaComboBox.DisplayMember = "Nome";

            try
            {
                linhaComboBox.SelectedIndex = 0;
            }
            catch { }
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }
               
        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                codigoTextBox.BorderColor = Publicas._bordaSaida;
            }
            catch { }

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (codigoTextBox.Text.Trim() != "" && (!codigoTextBox.Text.Contains(_empresa.Separador)))
                codigoTextBox.Text = (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.Ano ? DateTime.Now.Year.ToString("0000") :
                      (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.AnoMes ? DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") :
                       (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAno ? Publicas._usuario.IdEmpresa.ToString("000") + DateTime.Now.Year.ToString("0000") :
                        (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes ? Publicas._usuario.IdEmpresa.ToString("000") + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") : "")))) +
                        _empresa.Separador + codigoTextBox.Text;

            Publicas._telaQueChamouPesquisaDeAtendimento = Publicas.TelaPesquisaSAC.Atendimento;

            if (codigoTextBox.Text.Trim() == "")
            {
                new Pesquisas.Atendimentos().ShowDialog();

                codigoTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (codigoTextBox.Text.Trim() == "")
                    proximoButton_Click(sender, new EventArgs());
            }

            _atendimento = new AtendimentoBO().Consultar(codigoTextBox.Text, _empresa.IdEmpresa);
            _atendimentoLog = new AtendimentoBO().Consultar(codigoTextBox.Text, _empresa.IdEmpresa);

            if (_atendimento == null || !_atendimento.Existe )
            {
                if (sender != aberturaDateTimePicker)
                {
                    aberturaDateTimePicker.Value = DateTime.Now.Date;
                    fechamentoDateTimePicker.Value = DateTime.Now.Date;
                    origemReclamacaoComboBox.SelectedIndex = 0;
                    statusComboBox.SelectedIndex = 0;
                    situacaoComboBox.SelectedIndex = 0;
                    opcaoRetornoComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                _usuarioAbertura = new UsuarioBO().ConsultarPorId(_atendimento.IdUsuario);
                _anexos = new AtendimentoBO().Listar(_atendimento.Id);

                foreach (var item in _anexos)
                {
                    listBox1.Items.Add(item.NomeArquivo);
                    CriaPanelsArquivo(item.NomeArquivo);
                    Publicas._escTeclado = true;
                }

                anexoLabel.Text = _anexos.Count().ToString();

                tituloUsuarioAberturaLabel.Text = "aberto por " + _usuarioAbertura.Nome;

                aberturaDateTimePicker.Value = _atendimento.DataAbertura;
                
                origemReclamacaoComboBox.SelectedIndex = (_atendimento.Origem == Publicas.OrigemAtendimento.Email ? 3 :
                                                           (_atendimento.Origem == Publicas.OrigemAtendimento.Internet ? 1 :
                                                            (_atendimento.Origem == Publicas.OrigemAtendimento.Pessoalmente ? 0 :
                                                             (_atendimento.Origem == Publicas.OrigemAtendimento.Telefone ? 2 :
                                                              (_atendimento.Origem == Publicas.OrigemAtendimento.OrgaoGestor ? 4 : 5)))));

                statusComboBox.SelectedIndex = (_atendimento.Status == Publicas.StatusAtendimento.Ativo ? 0 :
                                                (_atendimento.Status == Publicas.StatusAtendimento.Cancelado ? 1 :
                                                 (_atendimento.Status == Publicas.StatusAtendimento.Respondido ? 2 : 3)));

                situacaoComboBox.SelectedIndex = (_atendimento.Situacao == Publicas.SituacaoAtendimento.Cancelado ? 3 :
                                                  (_atendimento.Situacao == Publicas.SituacaoAtendimento.Finalizado ? 4 :
                                                   (_atendimento.Situacao == Publicas.SituacaoAtendimento.ManterComAtendente ? 0 :
                                                    (_atendimento.Situacao == Publicas.SituacaoAtendimento.EnviadoAoColaborador ? 1 : 2))));

                opcaoRetornoComboBox.SelectedIndex = (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Telefone ? 0 :
                                                      (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Fax ? 1 :
                                                       (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Email ? 2 : 3)));

                SimRadioButton.Checked = _atendimento.ReclamacaoProcede == "S";
                NaoRadioButton.Checked = _atendimento.ReclamacaoProcede == "N";

                statusComboBox.ReadOnly = true;
                situacaoComboBox.ReadOnly = false; // _atendimento.Situacao != Publicas.SituacaoAtendimento.ManterComAtendente;
                opcaoRetornoComboBox.ReadOnly = true;
                origemReclamacaoComboBox.ReadOnly = true;

                fechamentoDateTimePicker.Visible = !String.IsNullOrEmpty(_atendimento.TextoResposta);
                label7.Visible = !String.IsNullOrEmpty(_atendimento.TextoResposta);

                if (!String.IsNullOrEmpty(_atendimento.TextoResposta))
                    fechamentoDateTimePicker.Value = _atendimento.DataResposta;
                

                codigoEmtuTextBox.Text = _atendimento.CodigoEmtu;
                _tipoEMTU = new TipoDeAtendimentoEMTUBO().Consultar(codigoEmtuTextBox.Text);

                descricaoEMTUTextBox.Text = _tipoEMTU.Descricao;

                _tipoAtendimento = new TipoDeAtendimentoBO().Consultar(_tipoEMTU.IdTipoAtendimento);

                for (int i = 0; i < tipoAcessoComboBox.Items.Count; i++)
                {
                    tipoAcessoComboBox.SelectedIndex = i;
                    if (tipoAcessoComboBox.Text == _tipoAtendimento.Descricao)
                        break;
                }

                descricaoAtendimentoTextBox.Text = _atendimento.TextoAtendimento;
                respostaTextBox.Text = _atendimento.TextoResposta;
                nomeTextBox.Text = _atendimento.NomeCliente;
                anonimoCheckBox.Checked = _atendimento.ClienteAnonimo;
                enderecoTextBox.Text = _atendimento.EnderecoCliente;
                cidadeTextBox.Text = _atendimento.CidadeCliente;
                rgTextBox.Text = _atendimento.RGCliente;
                RespostaAoClienteTextBox.Text = _atendimento.TextoRetornoAoCliente;
                RespostaAoClienteTextBox.Enabled = _atendimento.DataFinalizado != DateTime.MinValue;

                if (_atendimento.IdUsuarioRetornoAoCliente != 0)
                {
                    _usuarioRespostaCliente = new UsuarioBO().ConsultarPorId(_atendimento.IdUsuarioRetornoAoCliente);
                    respostaAoClienteLabel.Text = "Respondido ao cliente por " + _usuarioRespostaCliente.Nome + " na data " +
                        _atendimento.DataRetornoAoCliente.ToShortDateString() ;
                }

                if (_atendimento.CPFCliente != 0)
                    cpfMaskedEditBox.Text = _atendimento.CPFCliente.ToString();

                if (_atendimento.TelefoneCliente != 0)
                    telefoneMaskedEditBox.Text = _atendimento.TelefoneCliente.ToString();

                if (_atendimento.Celular != 0)
                    celularMaskedEditBox.Text = _atendimento.Celular.ToString();

                emailTextBox.Text = _atendimento.EmailCliente;

                if (string.IsNullOrEmpty(_atendimento.UFCliente))
                    ufComboBox.SelectedIndex = 0;
                else
                {
                    for (int i = 0; i < ufComboBox.Items.Count; i++)
                    {
                        ufComboBox.SelectedIndex = i;
                        if (ufComboBox.Text == _atendimento.UFCliente)
                            break;
                    }
                }

                if (_atendimento.CodigoLinha == "")
                    linhaComboBox.SelectedIndex = 0;
                else
                {
                    foreach (var item in _linhas.Where(w => w.Id == Convert.ToInt32(_atendimento.CodigoLinha)))
                    { 

                        for (int i = 0; i < linhaComboBox.Items.Count; i++)
                        {
                            linhaComboBox.SelectedIndex = i;
                            if (linhaComboBox.Text == item.Nome)
                                break;
                        }
                    }
                }

                if (_atendimento.CodSeqSecao == 0)
                    SecaoDaLinhaComboBox.SelectedIndex = 0;
                else
                {
                    foreach (var item in _secaoDaLinha.Where(w => w.Id == _atendimento.CodSeqSecao))
                    {

                        for (int i = 0; i < SecaoDaLinhaComboBox.Items.Count; i++)
                        {
                            SecaoDaLinhaComboBox.SelectedIndex = i;
                            if (SecaoDaLinhaComboBox.Text == item.NomeExibicao)
                                break;
                        }
                    }
                }

                if (_atendimento.IdUsuarioResponsavel != 0)
                {
                    _usuarioResponsavel = new UsuarioBO().ConsultarPorId(_atendimento.IdUsuarioResponsavel);
                    responsavelTextBox.Text = _usuarioResponsavel.UsuarioAcesso;
                    nomeResponsavelTextBox.Text = _usuarioResponsavel.Nome;
                    cpfResponsavelMaskedEditBox.Text = _usuarioResponsavel.CPF.ToString();
                }

                RespostaAoClienteTextBox.Enabled = (_atendimento.Status == Publicas.StatusAtendimento.Ativo || _atendimento.Status == Publicas.StatusAtendimento.Respondido);

                if (_atendimento.IdUsuarioRetorno != 0)
                {
                    _usuarioRetorno = new UsuarioBO().ConsultarPorId(_atendimento.IdUsuarioRetorno);

                    respostaColaboradorLabel.Text = "Retornado pelo Colaborador " + _usuarioRetorno.Nome + " na data " + _atendimento.DataResposta.ToShortDateString();
                    
                }
            }

            if (_atendimento.IdUsuarioResponsavel != 0)
            {
                _usuarioResponsavel = new UsuarioBO().ConsultarPorId(_atendimento.IdUsuarioResponsavel);
                nomeResponsavelTextBox.Text = _usuarioResponsavel.Nome;
                cpfResponsavelMaskedEditBox.Text = _usuarioResponsavel.CPF.ToString();
            }

            if (_atendimento.CodIntFunc != 0)
            {
                _funcionariosGlobus = new FuncionariosGlobusBO().ConsultaFuncionarioGlobus(_atendimento.CodIntFunc);
                if (_funcionariosGlobus.Existe)
                    FuncionarioLabel.Text = "Funcionário: " + _funcionariosGlobus.Codigo + " - " + _funcionariosGlobus.Nome;
            }

            gravarButton.Enabled = true;
            
            if (Publicas._codigoRetornoPesquisa != "")
                origemReclamacaoComboBox.Focus();
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            try
            {
                codigoTextBox.Text = new AtendimentoBO().ProximoCodigo(_empresa.FormatoCodigo, _empresa.Separador, DateTime.Now, Publicas._usuario.IdEmpresa).ToString();
            }
            catch
            {
                codigoTextBox.Text = new AtendimentoBO().ProximoCodigo(Publicas.TipoCalculoCodigoSAC.Sequencial, _empresa.Separador, DateTime.Now, Publicas._usuario.IdEmpresa).ToString();
            }
            codigoTextBox_Validating(sender, new CancelEventArgs());
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            anexoLabel.Text = "0";
            codigoTextBox.Text = string.Empty;
            codigoEmtuTextBox.Text = string.Empty;
            descricaoEMTUTextBox.Text = string.Empty;
            descricaoAtendimentoTextBox.Text = string.Empty;
            responsavelTextBox.Text = string.Empty;
            respostaTextBox.Text = string.Empty;
            nomeResponsavelTextBox.Text = string.Empty;
            cpfMaskedEditBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            rgTextBox.Text = string.Empty;
            enderecoTextBox.Text = string.Empty;
            cidadeTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            telefoneMaskedEditBox.Text = string.Empty;
            celularMaskedEditBox.Text = string.Empty;
            cpfResponsavelMaskedEditBox.Text = string.Empty;
            aberturaDateTimePicker.Value = DateTime.Now;
            fechamentoDateTimePicker.Value = DateTime.Now;
            origemReclamacaoComboBox.SelectedIndex = -1;
            statusComboBox.SelectedIndex = -1;
            situacaoComboBox.SelectedIndex = -1;
            opcaoRetornoComboBox.SelectedIndex = -1;
            tipoAcessoComboBox.SelectedIndex = -1;
            linhaComboBox.SelectedIndex = -1;
            SecaoDaLinhaComboBox.SelectedIndex = -1;
            ufComboBox.SelectedIndex = -1;
            anonimoCheckBox.Checked = false;
            RespostaAoClienteTextBox.Text = string.Empty;
            respostaAoClienteLabel.Text = string.Empty;
            tituloUsuarioAberturaLabel.Text = string.Empty;

            gravarButton.Enabled = false;

            statusComboBox.ReadOnly = false;
            situacaoComboBox.ReadOnly = false;
            opcaoRetornoComboBox.ReadOnly = false;
            origemReclamacaoComboBox.ReadOnly = false;
            LimpaImagens(anexoImagensPanel);
            codigoTextBox.Focus();
        }

        private void origemReclamacaoComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;

            if (codigoTextBox.Text.Trim() == "")
            {
                codigoTextBox.Text = string.Empty;
                codigoTextBox.Focus();
                return;
            }
        }

        private void cpfMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void telefoneMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;
        }

        private void ufComboBox_Validating(object sender, CancelEventArgs e)
        {
            respostaTextBox.Enabled = situacaoComboBox.SelectedIndex == 4;
            respostaTextBox.ReadOnly = situacaoComboBox.SelectedIndex != 4;

            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;
        }

        private void CodigoEmtuTextBox_Validating(object sender, CancelEventArgs e)
        {
            codigoEmtuTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (codigoEmtuTextBox.Text.Trim() == "")
            {
                new Pesquisas.EMTU().ShowDialog();

                codigoEmtuTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (codigoEmtuTextBox.Text.Trim() == "")
                {
                    codigoEmtuTextBox.Text = string.Empty;
                    codigoEmtuTextBox.Focus();
                    return;
                }
            }

            _tipoEMTU = new TipoDeAtendimentoEMTUBO().Consultar(codigoEmtuTextBox.Text);

            if (_tipoEMTU == null || !_tipoEMTU.Existe)
            {
                new Notificacoes.Mensagem("Tipo não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoEmtuTextBox.Focus();
                return;
            }
            if (!_tipoEMTU.Ativo)
            {
                new Notificacoes.Mensagem("Tipo inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoEmtuTextBox.Focus();
                return;
            }
            descricaoEMTUTextBox.Text = _tipoEMTU.Descricao;

            
            _tipoAtendimento = new TipoDeAtendimentoBO().Consultar(_tipoEMTU.IdTipoAtendimento);

            for (int i = 0; i < tipoAcessoComboBox.Items.Count; i++)
            {
                tipoAcessoComboBox.SelectedIndex = i;
                if (tipoAcessoComboBox.Text == _tipoAtendimento.Descricao)
                    break;
            }

            if (Publicas._codigoRetornoPesquisa != "")
            {
                if (situacaoComboBox.Enabled)
                    situacaoComboBox.Focus();
                else
                    linhaComboBox.Focus();
            }
        }

        private void fechamentoDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void aberturaDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            aberturaDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (aberturaDateTimePicker.Value.Month != DateTime.Now.Month || aberturaDateTimePicker.Value.Year != DateTime.Now.Year)
            {
                new Notificacoes.Mensagem("Será recalculado o protocolo para a data de abertura.", Publicas.TipoMensagem.Alerta).ShowDialog();

                try
                {
                    codigoTextBox.Text = new AtendimentoBO().ProximoCodigo(_empresa.FormatoCodigo, _empresa.Separador, aberturaDateTimePicker.Value, Publicas._usuario.IdEmpresa).ToString();
                }
                catch
                {
                    codigoTextBox.Text = new AtendimentoBO().ProximoCodigo(Publicas.TipoCalculoCodigoSAC.Sequencial, _empresa.Separador, aberturaDateTimePicker.Value, Publicas._usuario.IdEmpresa).ToString();
                }
                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void descricaoAtendimentoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

        }

        #region KeyDown
        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (origemReclamacaoComboBox.Enabled)
                    origemReclamacaoComboBox.Focus();
                else
                {
                    if (codigoEmtuTextBox.Enabled)
                        codigoEmtuTextBox.Focus();
                    else
                        situacaoComboBox.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void aberturaDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                origemReclamacaoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoEmtuTextBox.Focus();
            }
        }

        private void origemReclamacaoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                statusComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void statusComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                opcaoRetornoComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                origemReclamacaoComboBox.Focus();
            }
        }

        private void codigoEmtuTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (situacaoComboBox.Enabled)
                    situacaoComboBox.Focus();
                else
                    linhaComboBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                opcaoRetornoComboBox.Focus();
            }
        }

        private void situacaoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                linhaComboBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoEmtuTextBox.Focus();
            }
        }

        private void opcaoRetornoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                codigoEmtuTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                origemReclamacaoComboBox.Focus();
            }
        }

        private void linhaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                SecaoDaLinhaComboBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;

                if (situacaoComboBox.Enabled)
                    situacaoComboBox.Focus();
                else
                    codigoEmtuTextBox.Focus();
                
            }
        }

        private void descricaoAtendimentoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SecaoDaLinhaComboBox.Focus();
            }
        }

        private void respostaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                descricaoAtendimentoTextBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cpfMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;

                atendimentoTabPageAdv.Select();
                descricaoAtendimentoTextBox.Focus();
                //else
                //{
                //    if (respostaTabPageAdv.TabVisible)
                //        respostaTextBox.Focus();
                //    else
                //    {
                //        if (RespostaAoClienteTextBox.Enabled)
                //            RespostaAoClienteTextBox.Focus();
                //        else
                //            linhaComboBox.Focus();
                //    }
                //}
            }
        }

        private void cpfMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                rgTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void rgTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                anonimoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cpfMaskedEditBox.Focus();
            }
        }

        private void anonimoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                enderecoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                rgTextBox.Focus();
            }
        }

        private void enderecoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                cidadeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                anonimoCheckBox.Focus();
            }
        }

        private void cidadeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ufComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                enderecoTextBox.Focus();
            }
        }

        private void ufComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                telefoneMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                cidadeTextBox.Focus();
            }
        }

        private void emailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                responsavelTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                celularMaskedEditBox.Focus();
            }
        }

        private void telefoneMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                celularMaskedEditBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ufComboBox.Focus();
            }
        }

        private void celularMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                emailTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                telefoneMaskedEditBox.Focus();
            }
        }

        private void responsavelTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (gravarButton.Enabled)
                    gravarButton.Focus();
                else
                    limparButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                emailTextBox.Focus();
            }
        }

        #endregion

        #region KeyPress
        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) )
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(_empresa.Separador))
                e.Handled = false;

            if (e.KeyChar == '+')
            {
                codigoTextBox.Text = string.Empty;
                proximoButton_Click(sender, e);
            }
        }

        private void codigoEmtuTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.')
            {
                e.Handled = false;
            }
        }

        #endregion

        private void responsavelTextBox_Validating(object sender, CancelEventArgs e)
        {
            responsavelTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (responsavelTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = Publicas._usuario.IdEmpresa;
                Publicas._apenasAtivos = true;
                new Pesquisas.Usuarios().ShowDialog();

                responsavelTextBox.Text = Publicas._usuarioAcesso;

                if (responsavelTextBox.Text.Trim() == "")
                {
                    responsavelTextBox.Text = string.Empty;
                    responsavelTextBox.Focus();
                    return;
                }
            }

            _usuarioResponsavel = new UsuarioBO().Consultar(responsavelTextBox.Text);

            if (_usuarioResponsavel == null || !_usuarioResponsavel.Existe)
            {
                new Notificacoes.Mensagem("Responsável não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                responsavelTextBox.Focus();
                return;
            }
            if (!_tipoEMTU.Ativo)
            {
                new Notificacoes.Mensagem("Responsável inativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                responsavelTextBox.Focus();
                return;
            }
            nomeResponsavelTextBox.Text = _usuarioResponsavel.Nome;
            cpfResponsavelMaskedEditBox.Text = _usuarioResponsavel.CPF.ToString();

            if (Publicas._usuarioAcesso != "")
                gravarButton.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe o código de atendimento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(codigoEmtuTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe o tipo de atendimento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoEmtuTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(descricaoAtendimentoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe a descrição do atendimento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                descricaoAtendimentoTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(responsavelTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe o responsável.", Publicas.TipoMensagem.Alerta).ShowDialog();
                responsavelTextBox.Focus();
                return;
            }

            if (situacaoComboBox.SelectedIndex == 2 && string.IsNullOrEmpty(respostaTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe a resposta do colaborador.", Publicas.TipoMensagem.Alerta).ShowDialog();
                respostaTabPageAdv.Show();
                respostaTextBox.Focus();
                return;
            }

            if (situacaoComboBox.SelectedIndex == 4 && string.IsNullOrEmpty(respostaTextBox.Text.Trim())) // finalizado
            {
                new Notificacoes.Mensagem("Não é possível finalizar sem a resposta do colaborador.", Publicas.TipoMensagem.Alerta).ShowDialog();
                respostaTabPageAdv.Show();
                respostaTextBox.Focus();
                return;
            }

            gravarButton.Enabled = false;
            if (situacaoComboBox.SelectedIndex == 3) // cancelado
            {
                if (new Notificacoes.Mensagem("Tem certeza que deseja Cancelar este atendimento?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    return;

                new SAC.Motivo().ShowDialog();

                if (Publicas._cancelouMotivo)
                {
                    new Notificacoes.Mensagem("Gravação cancelada." + Environment.NewLine + "Motivo não foi informado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }
                _atendimento.MotivoCancelamento = Publicas._motivoCancelamentoDevolucao;
            }

            anonimoCheckBox.Checked = (string.IsNullOrEmpty(nomeTextBox.Text.Trim()));

            if (celularMaskedEditBox.ClipText.Trim() != "")
                _atendimento.Celular = Convert.ToDecimal(celularMaskedEditBox.ClipText);

            _atendimento.CidadeCliente = cidadeTextBox.Text;
            _atendimento.ClienteAnonimo = anonimoCheckBox.Checked;
            _atendimento.Codigo = codigoTextBox.Text;
            _atendimento.idEmpresa = Publicas._usuario.IdEmpresa;
            _atendimento.CodigoLinha = "-";

            string selecao = origemReclamacaoComboBox.Text;
            for (int i = 0; i < origemReclamacaoComboBox.Items.Count; i++)
            {
                origemReclamacaoComboBox.SelectedIndex = i;
                if (selecao == origemReclamacaoComboBox.Text)
                    break;
            }

            selecao = opcaoRetornoComboBox.Text;
            for (int i = 0; i < opcaoRetornoComboBox.Items.Count; i++)
            {
                opcaoRetornoComboBox.SelectedIndex = i;
                if (selecao == opcaoRetornoComboBox.Text)
                    break;
            }

            selecao = situacaoComboBox.Text;
            for (int i = 0; i < situacaoComboBox.Items.Count; i++)
            {
                situacaoComboBox.SelectedIndex = i;
                if (selecao == situacaoComboBox.Text)
                    break;
            }

            selecao = statusComboBox.Text;
            for (int i = 0; i < statusComboBox.Items.Count; i++)
            {
                statusComboBox.SelectedIndex = i;
                if (selecao == statusComboBox.Text)
                    break;
            }

            if (cpfMaskedEditBox.ClipText.Trim() != "")
                _atendimento.CPFCliente = Convert.ToDecimal(cpfMaskedEditBox.ClipText);

            _atendimento.ReclamacaoProcede = (SimRadioButton.Checked ? "S" : (NaoRadioButton.Checked ? "N" : ""));
            
            _atendimento.DataAbertura = aberturaDateTimePicker.Value;
            _atendimento.EmailCliente = emailTextBox.Text;
            _atendimento.EnderecoCliente = enderecoTextBox.Text;
            _atendimento.IdEmtu = _tipoEMTU.Id;
            _atendimento.IdTipoAtendimento = _tipoEMTU.IdTipoAtendimento;
            _atendimento.IdUsuario = Publicas._idUsuario;
            _atendimento.IdUsuarioResponsavel = _usuarioResponsavel.Id;
            _atendimento.NomeCliente = nomeTextBox.Text;
            _atendimento.OpcoesDeRetorno = (opcaoRetornoComboBox.SelectedIndex == 0 ? Publicas.OpcaoDeRetornoAtendimento.Telefone :
                                            (opcaoRetornoComboBox.SelectedIndex == 1 ? Publicas.OpcaoDeRetornoAtendimento.Fax :
                                             (opcaoRetornoComboBox.SelectedIndex == 2 ? Publicas.OpcaoDeRetornoAtendimento.Email : Publicas.OpcaoDeRetornoAtendimento.Nenhum)));

            _atendimento.Origem = (origemReclamacaoComboBox.SelectedIndex == 0 ? Publicas.OrigemAtendimento.Pessoalmente :
                                   (origemReclamacaoComboBox.SelectedIndex == 1 ? Publicas.OrigemAtendimento.Internet :
                                    (origemReclamacaoComboBox.SelectedIndex == 2 ? Publicas.OrigemAtendimento.Telefone :
                                     (origemReclamacaoComboBox.SelectedIndex == 3 ? Publicas.OrigemAtendimento.Email :
                                      (origemReclamacaoComboBox.SelectedIndex == 4 ? Publicas.OrigemAtendimento.OrgaoGestor : Publicas.OrigemAtendimento.JornalRadio)))));

            _atendimento.Retornou = false;
            _atendimento.RGCliente = rgTextBox.Text;
            _atendimento.Situacao = (situacaoComboBox.SelectedIndex == 0 ? Publicas.SituacaoAtendimento.ManterComAtendente :
                                     (situacaoComboBox.SelectedIndex == 1 ? Publicas.SituacaoAtendimento.EnviadoAoColaborador :
                                      (situacaoComboBox.SelectedIndex == 2 ? Publicas.SituacaoAtendimento.EnviadoAoFinalizador :
                                       (situacaoComboBox.SelectedIndex == 3 ? Publicas.SituacaoAtendimento.Cancelado : Publicas.SituacaoAtendimento.Finalizado))));

            Publicas._telaQueChamouPesquisaDeAtendimento = Publicas.TelaPesquisaSAC.Atendimento;

            if (situacaoComboBox.SelectedIndex == 3) // cancelado
                _atendimento.Status = Publicas.StatusAtendimento.Cancelado;
            else
            {
                if (situacaoComboBox.SelectedIndex == 4) // finalizado
                {
                    _atendimento.Status = Publicas.StatusAtendimento.Finalizado;
                    _atendimento.Situacao = Publicas.SituacaoAtendimento.Finalizado;
                    _atendimento.TextoRetornoAoCliente = RespostaAoClienteTextBox.Text;
                    Publicas._telaQueChamouPesquisaDeAtendimento = Publicas.TelaPesquisaSAC.Finaliza;
                }
                else
                    _atendimento.Status = (statusComboBox.SelectedIndex == 0 ? Publicas.StatusAtendimento.Ativo :
                                       (statusComboBox.SelectedIndex == 1 ? Publicas.StatusAtendimento.Cancelado :
                                        (statusComboBox.SelectedIndex == 2 ? Publicas.StatusAtendimento.Respondido : Publicas.StatusAtendimento.Finalizado)));
            }

            if (telefoneMaskedEditBox.ClipText.Trim() != "")
                _atendimento.TelefoneCliente = Convert.ToDecimal(telefoneMaskedEditBox.ClipText);

            _atendimento.TextoAtendimento = descricaoAtendimentoTextBox.Text;
            _atendimento.TextoResposta = respostaTextBox.Text;
            _atendimento.UFCliente = ufComboBox.Text;

            _atendimento.CodigoLinha = "0";
            if (linhaComboBox.Text != "-")
            {
                foreach (var item in _linhas.Where(w => w.Nome == linhaComboBox.Text))
                {
                    _atendimento.CodigoLinha = item.Id.ToString();
                }
            }
            _atendimento.CodSeqSecao = 0;

            if (SecaoDaLinhaComboBox.Text != "-")
            {
                foreach (var item in _secaoDaLinha.Where(w => w.NomeExibicao == SecaoDaLinhaComboBox.Text))
                {
                    _atendimento.CodSeqSecao = item.Id;
                }
            }

            List<Classes.Atendimento.Anexos> _listaAnexosGravar = new List<Classes.Atendimento.Anexos>();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                Classes.Atendimento.Anexos _arquivos = new Classes.Atendimento.Anexos();

                try
                {
                    StreamReader oStreamReader = new StreamReader(listBox1.Items[i].ToString());

                    byte[] buffer = new byte[oStreamReader.BaseStream.Length];

                    oStreamReader.BaseStream.Read(buffer, 0, buffer.Length);

                    oStreamReader.Close();
                    oStreamReader.Dispose();

                    _arquivos.Anexo = buffer;
                    _arquivos.NomeArquivo = Path.GetFileName(listBox1.Items[i].ToString());
                }
                catch
                {
                    foreach (var item in _anexos.Where(w => listBox1.Items[i].ToString().Contains(w.NomeArquivo)))
                    {
                        _arquivos.Anexo = item.Anexo;
                        _arquivos.Id = item.Id;
                        _arquivos.NomeArquivo = item.NomeArquivo;
                        _arquivos.IdAtendimento = item.IdAtendimento;
                        _arquivos.Existe = item.Existe;
                    }
                }

                _listaAnexosGravar.Add(_arquivos);
            }

            if (!new AtendimentoBO().Gravar(_atendimento, _atendimentoLog, _listaAnexosGravar))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                gravarButton.Enabled = true;
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = (!_atendimento.Existe ? "Registrou " : "Alterou ") ;

            _log.Descricao = _log.Descricao + "atendimento " + _atendimento.Codigo + " status " + statusComboBox.Text + " situação " + situacaoComboBox.Text;
            _log.Tela = "SAC - Registrar Atendimento";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            if (!_atendimento.Existe && _atendimento.EmailCliente != "" &&
                (tipoAcessoComboBox.Text.ToUpper() == "Sugestão".ToUpper() || tipoAcessoComboBox.Text.ToUpper() == "Reclamação".ToUpper()))
            {
                Publicas.EnviarEmail("SAC - Atendimento ao Cliente", _empresa.Nome,
                    nomeTextBox.Text, codigoTextBox.Text, aberturaDateTimePicker.Value.ToShortDateString(),
                    _empresa.Telefone, _empresa.Email, emailTextBox.Text, _empresa.Senha,
                    _empresa.Nome + " - SAC nº " + codigoTextBox.Text, _empresa.Smtp,
                    _empresa.PortaSmtp, _empresa.Autentica, _empresa.AutenticaSLL, "", Publicas._usuario.Nome, "", descricaoAtendimentoTextBox.Text, _empresa.TextoPadraoSAC);

                if (Publicas.mensagemDeErro != "")
                {
                    new Notificacoes.Mensagem("Problemas durante o envio do e-mail ao cliente." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    gravarButton.Enabled = true;
                    return;
                }
            }

            if (!_atendimento.Existe && !string.IsNullOrEmpty(_empresa.Email) && !string.IsNullOrEmpty(Publicas._usuario.Email))
            {
                Publicas.EnviarEmail("SAC - Atendimento ao Cliente", _empresa.Nome,
                    "", codigoTextBox.Text, aberturaDateTimePicker.Value.ToShortDateString(),
                    _empresa.Telefone, _empresa.Email,
                    /*destinatario*/Publicas._usuario.Email + ";" + Publicas._usuario.EmailDepartamento + ";" + _usuarioResponsavel.Email, 
                    _empresa.Senha, _empresa.Nome + " - SAC nº " + codigoTextBox.Text, _empresa.Smtp,
                    _empresa.PortaSmtp, _empresa.Autentica, _empresa.AutenticaSLL, "", Publicas._usuario.Nome, "", descricaoAtendimentoTextBox.Text);
                 
                if (Publicas.mensagemDeErro != "")
                {
                    new Notificacoes.Mensagem("Problemas durante o envio do e-mail ao departamento." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    gravarButton.Enabled = true;
                    return;
                }
            }


            if (!_atendimento.Existe && !string.IsNullOrEmpty(_empresa.Email) && !string.IsNullOrEmpty(Publicas._usuario.Email) )
            {// enviar email ao responsavel pela finalização
                string emailDestino = "";

                if (_atendimento.Situacao == Publicas.SituacaoAtendimento.EnviadoAoFinalizador)
                {
                    _listaUsuarios = new UsuarioBO().ListarUsuarios(true, Publicas._usuario.IdEmpresa, true, "F");
                }
                else
                if (_atendimento.Situacao == Publicas.SituacaoAtendimento.EnviadoAoColaborador)
                {
                    _listaUsuarios = new UsuarioBO().ListarUsuarios(true, Publicas._usuario.IdEmpresa, true, "U");
                }
                foreach (var item in _listaUsuarios)
                {
                    if (item.Email != "")
                        emailDestino = emailDestino + item.Email + "; ";
                }
                if (emailDestino != "")
                {
                    Publicas.EnviarEmail("SAC - Atendimento ao Cliente", _empresa.Nome,
                        "", codigoTextBox.Text, aberturaDateTimePicker.Value.ToShortDateString(),
                        _empresa.Telefone, _empresa.Email,
                        /*destinatario*/Publicas._usuario.Email + ";" + Publicas._usuario.EmailDepartamento + ";" + _usuarioResponsavel.Email + ";" + emailDestino,
                        _empresa.Senha, _empresa.Nome + " - SAC nº " + codigoTextBox.Text, _empresa.Smtp,
                        _empresa.PortaSmtp, _empresa.Autentica, _empresa.AutenticaSLL, "", Publicas._usuario.Nome, "", 
                        situacaoComboBox.Text + Environment.NewLine + descricaoAtendimentoTextBox.Text);

                    if (Publicas.mensagemDeErro != "")
                    {
                        new Notificacoes.Mensagem("Problemas durante o envio do e-mail ao departamento." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                        gravarButton.Enabled = true;
                        return;
                    }
                }
            }
            limparButton_Click(sender, e);
        }        

        private void Atendimentos_Shown(object sender, EventArgs e)
        {

            if (codigoTextBox.Text != "")
            {
                codigoTextBox_Validating(sender, new CancelEventArgs());
                Publicas._escTeclado = true;
                if (origemReclamacaoComboBox.Enabled)
                    origemReclamacaoComboBox.Focus();
                else
                {
                    if (codigoEmtuTextBox.Enabled)
                        codigoEmtuTextBox.Focus();
                    else
                        situacaoComboBox.Focus();
                }
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
                responsavelTextBox.Focus();
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

        private void label4_MouseHover(object sender, EventArgs e)
        {
            label4.Cursor = Cursors.Hand;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.Cursor = Cursors.Default;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            new Cadastros.EMTU().ShowDialog();
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text);
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text);
        }

        private void pesquisaCategoriaButton_Click(object sender, EventArgs e)
        {
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

                codigoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void codigoEmtuTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(codigoEmtuTextBox.Text);
        }

        private void PesquisaTipoButtonAdv_Click(object sender, EventArgs e)
        {
            if (codigoEmtuTextBox.Text.Trim() == "")
            {
                new Pesquisas.EMTU().ShowDialog();

                codigoEmtuTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (codigoEmtuTextBox.Text.Trim() == "")
                {
                    codigoEmtuTextBox.Text = string.Empty;
                    codigoEmtuTextBox.Focus();
                    return;
                }

                CodigoEmtuTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void PesquisaResponsavelButton_Click(object sender, EventArgs e)
        {
            if (responsavelTextBox.Text.Trim() == "")
            {
                Publicas._idEmpresa = Publicas._usuario.IdEmpresa;
                Publicas._apenasAtivos = true;
                new Pesquisas.Usuarios().ShowDialog();

                responsavelTextBox.Text = Publicas._usuarioAcesso;

                if (responsavelTextBox.Text.Trim() == "")
                {
                    responsavelTextBox.Text = string.Empty;
                    responsavelTextBox.Focus();
                    return;
                }

                responsavelTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void responsavelTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaResponsavelButton.Enabled = string.IsNullOrEmpty(responsavelTextBox.Text);
        }

        private void situacaoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_atendimento != null && !_atendimento.Existe && situacaoComboBox.SelectedIndex == 4)
            {
                if (new Notificacoes.Mensagem("Deseja informar a data real do atendimento ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                {
                    aberturaDateTimePicker.Enabled = true;
                    aberturaDateTimePicker.ReadOnly = false;
                    fechamentoDateTimePicker.Enabled = true;
                    fechamentoDateTimePicker.ReadOnly = false;
                    aberturaDateTimePicker.Focus();
                }
            }
        }

        private void fechamentoDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            fechamentoDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (fechamentoDateTimePicker.Value.Date < aberturaDateTimePicker.Value.Date)
            {
                new Notificacoes.Mensagem("Fechamento não pode ter data menor que a abertura.", Publicas.TipoMensagem.Confirmacao).ShowDialog();
                fechamentoDateTimePicker.Focus();
                return;
            }
        }

        private void clipAnexoPictureBox_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Title = "Selecione o arquivo a ser anexado.";
            anexoImagensPanel.Visible = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] arquivos = openFileDialog1.FileNames;

                if (arquivos.Count() != 0)
                {
                    foreach (var item in arquivos)
                    {
                        if (item.ToString().Length > 100)
                        {
                            new Notificacoes.Mensagem("Nome do arquivo muito grande. Renomeio o arquivo!", Publicas.TipoMensagem.Alerta).ShowDialog();
                            return;
                        }

                        if (!item.ToString().ToLower().Contains(".pdf") && !item.ToString().ToLower().Contains(".txt") &&
                            !item.ToString().ToLower().Contains(".jpg") && !item.ToString().ToLower().Contains(".jpeg") &&
                            !item.ToString().ToLower().Contains(".png"))
                        {
                            new Notificacoes.Mensagem("Extensão do arquivo inválido." + Environment.NewLine +
                                item.ToString() +
                                Environment.NewLine + Environment.NewLine +
                                "Extensões permitidas: pdf, png, jpeg e txt"
                                , Publicas.TipoMensagem.Alerta).ShowDialog();
                            return;
                        }
                        listBox1.Items.Add(item);
                        CriaPanelsArquivo(item);
                    }
                    anexoLabel.Text = listBox1.Items.Count.ToString();

                }
            }
        }

        private void CriaPanelsArquivo(string arquivo)
        {
            string caminho = "";// Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            string tipo = "";

            switch (Path.GetExtension(arquivo).ToUpper())
            {
                case ".TXT":
                    tipo = caminho + @"Imagens\txt.png";
                    break;
                case ".RAR":
                    tipo = caminho + @"Imagens\rar.png";
                    break;
                case ".EXE":
                    tipo = caminho + @"Imagens\exe.png";
                    break;
                case ".XLSX":
                    tipo = caminho + @"Imagens\xlsx.png";
                    break;
                case ".PPT":
                    tipo = caminho + @"Imagens\ppt.png";
                    break;
                case ".JPG":
                    tipo = caminho + @"Imagens\jpg.png";
                    break;
                case ".SQL":
                    tipo = caminho + @"Imagens\sql.png";
                    break;
                case ".ZIP":
                    tipo = caminho + @"Imagens\zip.png";
                    break;
                case ".HTML":
                    tipo = caminho + @"Imagens\html.png";
                    break;
                case ".XLS":
                    tipo = caminho + @"Imagens\xls.png";
                    break;
                case ".XML":
                    tipo = caminho + @"Imagens\xml.png";
                    break;
                case ".DOC":
                    tipo = caminho + @"Imagens\doc.png";
                    break;
                case ".PDF":
                    tipo = caminho + @"Imagens\pdf.png";
                    break;
                case ".PNG":
                    tipo = caminho + @"Imagens\png.png";
                    break;
                default:
                    tipo = caminho + @"Imagens\File.png";
                    break;
            }

            if (_qtd == 0)
            {
                arquivoPanel.Location = new Point(arquivoPanel.Left, _topArquivos = 8);
                nomeArquivoLabel.Text = Path.GetFileNameWithoutExtension(arquivo);
                removerLabel.Tag = nomeArquivoLabel.Text;
                imagemArquivoPictureBox.ImageLocation = tipo;
                imagemArquivoPictureBox.Tag = nomeArquivoLabel.Text;
                arquivoPanel.Visible = true;
            }
            else
            {
                Panel panel = new Panel();
                panel.Name = "arquivoPanel_" + _qtd.ToString();

                Label labelNomeArquivo = new Label();
                Label labelRemover = new Label();
                PictureBox imagem = new PictureBox();

                panel.Controls.Add(imagem);
                panel.Controls.Add(labelNomeArquivo);
                panel.Controls.Add(labelRemover);

                imagem.Size = imagemArquivoPictureBox.Size;
                imagem.Location = imagemArquivoPictureBox.Location;
                imagem.Image = imagemArquivoPictureBox.Image;
                imagem.SizeMode = PictureBoxSizeMode.StretchImage;
                imagem.ImageLocation = tipo;
                imagem.Tag = Path.GetFileNameWithoutExtension(arquivo);
                imagem.Click += new System.EventHandler(this.imagemArquivoPictureBox_Click);

                panel.Size = arquivoPanel.Size;
                panel.Location = new Point(arquivoPanel.Left, _topArquivos);

                labelNomeArquivo.ForeColor = nomeArquivoLabel.ForeColor;
                labelNomeArquivo.Font = nomeArquivoLabel.Font;
                labelNomeArquivo.TextAlign = ContentAlignment.MiddleCenter;
                labelNomeArquivo.Text = Path.GetFileNameWithoutExtension(arquivo);
                labelNomeArquivo.AutoSize = false;
                labelNomeArquivo.Site = nomeArquivoLabel.Site;
                labelNomeArquivo.Location = nomeArquivoLabel.Location;

                labelRemover.TextAlign = ContentAlignment.MiddleCenter;
                labelRemover.AutoSize = false;
                labelRemover.Size = removerLabel.Size;
                labelRemover.ForeColor = removerLabel.ForeColor;
                labelRemover.Font = removerLabel.Font;
                labelRemover.Location = removerLabel.Location;
                labelRemover.Text = removerLabel.Text;
                labelRemover.Tag = labelNomeArquivo.Text;

                labelRemover.Click += new System.EventHandler(this.removerLabel_Click);
                labelRemover.MouseLeave += new System.EventHandler(this.removerLabel_MouseLeave);
                labelRemover.MouseHover += new System.EventHandler(this.removerLabel_MouseHover);

                panel.Visible = true;
                anexoImagensPanel.Controls.Add(panel);

                panel.BringToFront();

            }

            _topArquivos = _topArquivos + (arquivoPanel.Height + 5);

            Refresh();
            _qtd++;
        }

        private void removerLabel_Click(object sender, EventArgs e)
        {
            _qtd = 0;
            Control[] controle;
            string nome = "";
            string arquivo = ((Label)sender).Tag.ToString();

            if (_atendimento.Existe)
            {
                if (!new AtendimentoBO().Excluir(arquivo))
                {
                    new Notificacoes.Mensagem("Problema durante a exclusão.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    respostaTextBox.Focus();
                    return;
                }

                LogSAC _logS = new LogSAC();
                _logS.Descricao = "Excluido anexo " + arquivo;
                _logS.IdAtendimento = _atendimento.Id;
                _logS.IdUsuario = Publicas._idUsuario;

                try
                {
                    new LogSACBO().Gravar(_logS);
                }
                catch {}

                Log _log = new Log();
                _log.Descricao = (!_atendimento.Existe ? "Registrou " : "Alterou ");

                _log.Descricao = _log.Descricao + "atendimento " + _atendimento.Codigo + " status " + statusComboBox.Text + " situação " + situacaoComboBox.Text + 
                    " foi excluido o anexo " + arquivo;
                _log.Tela = "SAC - Registrar Atendimento";

                _log.IdUsuario = Publicas._idUsuario;

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }
            }

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                nome = "arquivoPanel" + (i == 0 ? "" : i.ToString());

                controle = this.Controls.Find(nome, true);

                if (i == 0)
                    controle[0].Visible = false; // o primeiro não é criado em tempo de execução por isso fica invisivel
                else
                {
                    try
                    {
                        controle[0].Dispose();
                    }
                    catch { }
                }
            }

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString().Contains(arquivo))
                {
                    nome = listBox1.Items[i].ToString();
                    break;
                }
            }

            listBox1.Items.Remove(nome);

            _topArquivos = 8;
            anexoImagensPanel.Visible = false;

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                CriaPanelsArquivo(listBox1.Items[i].ToString());
            }

            anexoImagensPanel.Visible = listBox1.Items.Count > 0;
            anexoLabel.Text = listBox1.Items.Count.ToString();
        }

        private void removerLabel_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Hand;
        }

        private void removerLabel_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Cursor = Cursors.Default;
        }

        private void anexoPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void anexoPanel_DragDrop(object sender, DragEventArgs e)
        {
            arquivo = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            anexoImagensPanel.Visible = false;
            for (i = 0; i < arquivo.Length; i++)
            {
                if (arquivo[i].ToString().Length > 100)
                {
                    new Notificacoes.Mensagem("Nome do arquivo muito grande. Renomeio o arquivo!", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }
                if (!arquivo[i].ToString().ToLower().Contains(".pdf") && !arquivo[i].ToString().ToLower().Contains(".txt") &&
                            !arquivo[i].ToString().ToLower().Contains(".jpg") && !arquivo[i].ToString().ToLower().Contains(".jpeg") &&
                            !arquivo[i].ToString().ToLower().Contains(".xls") && !arquivo[i].ToString().ToLower().Contains(".rem") &&
                            !arquivo[i].ToString().ToLower().Contains(".ret") && !arquivo[i].ToString().ToLower().Contains(".png") &&
                            !arquivo[i].ToString().ToLower().Contains(".xml"))
                {
                    new Notificacoes.Mensagem("Extensão do arquivo inválido." + Environment.NewLine +
                        arquivo[i].ToString() +
                        Environment.NewLine + Environment.NewLine +
                        "Extensões permitidas: pdf, png, jpeg, txt e xls"
                        , Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }
                listBox1.Items.Add(arquivo[i]);
                CriaPanelsArquivo(arquivo[i]);
            }

            anexoLabel.Text = listBox1.Items.Count.ToString();
        }
        
        private void verPictureBox_Click(object sender, EventArgs e)
        {
            anexoImagensPanel.Left = 1;
            anexoImagensPanel.Top = 216;
            anexoImagensPanel.Visible = !anexoImagensPanel.Visible;
        }

        private void verPictureBox_MouseHover(object sender, EventArgs e)
        {
            verPictureBox.BackColor = Color.Silver;
            verPictureBox.Cursor = Cursors.Hand;
        }

        private void verPictureBox_MouseLeave(object sender, EventArgs e)
        {
            verPictureBox.BackColor = codigoEmtuTextBox.BackColor;
            verPictureBox.Cursor = Cursors.Default;
        }

        private void clipAnexoPictureBox_MouseHover(object sender, EventArgs e)
        {
            clipAnexoPictureBox.BackColor = Color.Silver;
            clipAnexoPictureBox.Cursor = Cursors.Hand;
        }

        private void clipAnexoPictureBox_MouseLeave(object sender, EventArgs e)
        {
            clipAnexoPictureBox.BackColor = codigoEmtuTextBox.BackColor;
            clipAnexoPictureBox.Cursor = Cursors.Default;
        }

        private void imagemArquivoPictureBox_Click(object sender, EventArgs e)
        {
            int id = 0;

            if (!Directory.Exists(Publicas._caminhoAnexosSAC))
            {// cria o diretorio
                Directory.CreateDirectory(Publicas._caminhoAnexosSAC);
            }

            foreach (var item in _anexos.Where(w => w.NomeArquivo.Contains( ((PictureBox)sender).Tag.ToString())) )
            {
                id = listBox1.FindString(item.NomeArquivo);
                if (id != -1)
                {
                    if (!SalvaArquivos(item.Anexo, listBox1.Items[id].ToString()))
                        return;

                    System.Diagnostics.Process.Start(Publicas._caminhoAnexosSAC + codigoTextBox.Text + "_" + listBox1.Items[id].ToString());
                }
            }
        }

        public bool SalvaArquivos(Byte[] anexo, string nomeArquivo)
        {
            try
            {
                FileStream stream = new FileStream(Publicas._caminhoAnexosSAC + codigoTextBox.Text + "_" + nomeArquivo, FileMode.Create);
                stream.Write(anexo, 0, anexo.Length);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                new Notificacoes.Mensagem("Problemas ao salvar o(s) anexo(s)." + Environment.NewLine + ex.Message, Publicas.TipoMensagem.Erro);
                return false;
            }
        }

        private void anexoImagensPanel_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = ((Panel)sender).ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle((Publicas._TemaBlack ? Pens.Black : Pens.DarkOliveGreen), rect);
        }

        private void LimpaImagens(Control controle)
        {
            foreach (Control item in controle.Controls)
            {
                if (item.Name.StartsWith("arquivoPanel_"))
                {
                    item.Dispose();
                    LimpaImagens(controle);
                }
                else
                {
                    if (item.Name == "arquivoPanel")
                        item.Visible = false;
                }
            }

            listBox1.Items.Clear();
            _topArquivos = 8;
        }

        private void SecaoDaLinhaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (atendimentoTabPageAdv.TabVisible)
                    descricaoAtendimentoTextBox.Focus();
                else
                {
                    if (respostaTabPageAdv.TabVisible)
                        respostaTextBox.Focus();
                    else
                        RespostaAoClienteTextBox.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;

                linhaComboBox.Focus();
            }
        }

        private void linhaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idLinha = 0;

            foreach (var item in _linhas.Where(w => w.Nome == linhaComboBox.Text))
            {
                idLinha = item.Id;
            }

            try
            {
                _secaoDaLinha = new LinhaBO().Listar(idLinha);
            }
            catch { }


            SecaoDaLinhaComboBox.DataSource = _secaoDaLinha.OrderBy(o => o.Nome).ToList();
            SecaoDaLinhaComboBox.DisplayMember = "NomeExibicao";

            try
            {
                SecaoDaLinhaComboBox.SelectedIndex = 0;
            }
            catch { }
        }
    }
}
