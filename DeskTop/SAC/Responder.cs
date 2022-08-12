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
    public partial class Responder : Form
    {
        public Responder()
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

        Empresa _empresa;
        Atendimento _atendimento;
        Atendimento _atendimentoLog;
        TipoDeAtendimentoEMTU _tipoEMTU;
        TipoDeAtendimento _tipoAtendimento;
        Usuario _usuarioAbertura;
        FuncionariosGlobus _funcionariosGlobus;
        List<Classes.Atendimento.Anexos> _anexos;
        List<Linha> _linhas;
        List<SecaoDaLinha> _secaoDaLinha;


        string[] arquivo;
        int _topArquivos = 8;
        int _qtd = 0;
        bool _alterar = false;

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

        #region KeyDown
        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                funcionarioTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void funcionarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                respostaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
            Publicas._setaParaBaixo = false;
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                respostaTextBox.Focus();
            }
        }

        private void respostaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                funcionarioTextBox.Focus();
            }
        }
        #endregion

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }        

        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(_empresa.Separador))
                e.Handled = false;            
        }

        private void Responder_Load(object sender, EventArgs e)
        {

            // Empresa do usuario logado
            //if (Publicas._usuario.Administrador)
            //    _empresa = new EmpresaBO().Consultar(4);
            //else
                _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);

            try
            {
                if (Publicas._usuario.Administrador)
                    _linhas = new LinhaBO().Listar("003/001");
                else
                    _linhas = new LinhaBO().Listar(_empresa.CodigoEmpresaGlobus);
            }
            catch { }

            List<TipoDeAtendimento> _lista = new TipoDeAtendimentoBO().Listar();
            tipoAcessoComboBox.DataSource = _lista;
            tipoAcessoComboBox.DisplayMember = "Descricao";
            tipoAcessoComboBox.SelectedIndex = 0;

            statusComboBox.Items.AddRange(new object[] { "Ativo", "Cancelado", "Respondido", "Finalizado" });
            statusComboBox.SelectedIndex = 0;

            opcaoRetornoComboBox.Items.AddRange(new object[] { "Telefone", "Fax", "E-mail", "Nenhum" });
            opcaoRetornoComboBox.SelectedIndex = 0;

            if (codigoTextBox.Text != "")
            {
                codigoTextBox_Validating(sender, new CancelEventArgs());

            }
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

            if (codigoTextBox.Text.Trim() != "" && (!codigoTextBox.Text.Contains(_empresa.Separador)))
                codigoTextBox.Text = (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.Ano ? DateTime.Now.Year.ToString("0000") :
                      (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.AnoMes ? DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") :
                       (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAno ? Publicas._usuario.IdEmpresa.ToString("000") + DateTime.Now.Year.ToString("0000") :
                        (_empresa.FormatoCodigo == Publicas.TipoCalculoCodigoSAC.EmpresaAnoMes ? Publicas._usuario.IdEmpresa.ToString("000") + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") : "")))) +
                        _empresa.Separador + codigoTextBox.Text;

            Publicas._telaQueChamouPesquisaDeAtendimento = Publicas.TelaPesquisaSAC.Responde;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

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
            }

            _atendimento = new AtendimentoBO().Consultar(codigoTextBox.Text, _empresa.IdEmpresa);
            _atendimentoLog = new AtendimentoBO().Consultar(codigoTextBox.Text, _empresa.IdEmpresa);
            _alterar = false;

            if (_atendimento == null || !_atendimento.Existe)
            {
                new Notificacoes.Mensagem("Atendimento não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }
            else
            {
                _alterar = (_atendimento.Status == Publicas.StatusAtendimento.Finalizado || _atendimento.IdUsuarioRetorno == Publicas._usuario.Id);

                if (_atendimento.Status == Publicas.StatusAtendimento.Finalizado && _atendimento.IdUsuarioRetorno != Publicas._usuario.Id)
                {
                    new Notificacoes.Mensagem("Atendimento já foi finalizado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    codigoTextBox.Focus();
                    return;
                }
                
                if (_atendimento.Status == Publicas.StatusAtendimento.Cancelado)
                {
                    new Notificacoes.Mensagem("Atendimento cancelado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    codigoTextBox.Focus();
                    return;
                }

                if (_atendimento.Situacao != Publicas.SituacaoAtendimento.EnviadoAoColaborador && _atendimento.IdUsuarioRetorno != Publicas._usuario.Id)
                {
                    new Notificacoes.Mensagem("Atendimento não disponível para ser respondido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    codigoTextBox.Focus();
                    return;
                }

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

                statusComboBox.SelectedIndex = (_atendimento.Status == Publicas.StatusAtendimento.Ativo ? 0 :
                                                (_atendimento.Status == Publicas.StatusAtendimento.Cancelado ? 1 :
                                                 (_atendimento.Status == Publicas.StatusAtendimento.Respondido ? 2 : 3)));

                opcaoRetornoComboBox.SelectedIndex = (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Telefone ? 0 :
                                                      (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Fax ? 1 :
                                                       (_atendimento.OpcoesDeRetorno == Publicas.OpcaoDeRetornoAtendimento.Email ? 2 : 3)));

                SimRadioButton.Checked = _atendimento.ReclamacaoProcede == "S";
                NaoRadioButton.Checked = _atendimento.ReclamacaoProcede == "N";

                statusComboBox.Enabled = false;
                opcaoRetornoComboBox.Enabled = false;

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

                if (_atendimento.CodIntFunc != 0)
                {
                    _funcionariosGlobus = new FuncionariosGlobusBO().ConsultaFuncionarioGlobus(_atendimento.CodIntFunc);
                    if (_funcionariosGlobus.Existe)
                    {
                        funcionarioTextBox.Text = _funcionariosGlobus.Codigo;
                        funcionarioTextBox_Validating(sender, new CancelEventArgs());
                    }
                }

                if (_atendimento.CodigoLinha != "0")
                {
                    foreach (var item in _linhas.Where(w => w.Id == Convert.ToInt32(_atendimento.CodigoLinha)))
                    {
                        LinhaSecaoLabel.Text = "Linha: " + item.Nome;

                        try
                        {
                            _secaoDaLinha = new LinhaBO().Listar(item.Id);

                            if (_atendimento.CodSeqSecao != 0)
                            {
                                foreach (var itemS in _secaoDaLinha.Where(w => w.Id == _atendimento.CodSeqSecao))
                                {
                                    LinhaSecaoLabel.Text = LinhaSecaoLabel.Text + " - Seção: " + itemS.Nome;
                                    break;
                                }
                            }
                        }
                        catch { }
                        break;
                    }
                }
            }


            gravarButton.Enabled = statusComboBox.SelectedIndex == 0 || _alterar;
                        
            if (Publicas._codigoRetornoPesquisa != "")
                funcionarioTextBox.Focus();

        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            LinhaSecaoLabel.Text = string.Empty;
            codigoTextBox.Text = string.Empty;
            codigoEmtuTextBox.Text = string.Empty;
            descricaoEMTUTextBox.Text = string.Empty;
            descricaoAtendimentoTextBox.Text = string.Empty;
            respostaTextBox.Text = string.Empty;
            tituloUsuarioAberturaLabel.Text = string.Empty;
            aberturaDateTimePicker.Value = DateTime.Now;
            fechamentoDateTimePicker.Value = DateTime.Now;
            statusComboBox.SelectedIndex = -1;
            opcaoRetornoComboBox.SelectedIndex = -1;
            tipoAcessoComboBox.SelectedIndex = -1;
            gravarButton.Enabled = false;
            
            statusComboBox.Enabled = true;
            opcaoRetornoComboBox.Enabled = true;

            anexoLabel.Text = "0";
            LimpaImagens(anexoImagensPanel);
            codigoTextBox.Focus();
        }

        private void funcionarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            funcionarioTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
                return;
            }

            if (funcionarioTextBox.Text.Trim() == "")
            {
                if (Publicas._setaParaBaixo)
                {
                    Publicas._setaParaBaixo = false;
                    ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

                    return;
                }

                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._participaAvaliacao = false;
                new Pesquisas.Funcionarios().ShowDialog();

                funcionarioTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (funcionarioTextBox.Text.Trim() == "")
                {
                    funcionarioTextBox.Text = string.Empty;
                    Publicas._escTeclado = true;
                    funcionarioTextBox.Focus();
                    return;
                }
            }

            funcionarioTextBox.Text = funcionarioTextBox.Text.PadLeft(6,'0');
            _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(funcionarioTextBox.Text, _empresa.CodigoEmpresaGlobus);

            if (!_funcionariosGlobus.Existe)
            {
                new Notificacoes.Mensagem("Funcionário não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                funcionarioTextBox.Focus();
                return;
            }

            if (!_funcionariosGlobus.Ativo)
            {
                new Notificacoes.Mensagem("Funcionário não está ativo.", Publicas.TipoMensagem.Alerta).ShowDialog();
                funcionarioTextBox.Focus();
                return;
            }

            nomeFuncionarioTextBox.Text = _funcionariosGlobus.Nome;
            areaTextBox.Text = _funcionariosGlobus.Area;

            if (Publicas._codigoRetornoPesquisa != "")
                respostaTextBox.Focus();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(descricaoAtendimentoTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe a descrição do atendimento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                descricaoAtendimentoTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(respostaTextBox.Text.Trim()))
            {
                new Notificacoes.Mensagem("Informe a resposta.", Publicas.TipoMensagem.Alerta).ShowDialog();
                respostaTextBox.Focus();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Alterou atendimento " + _atendimento.Codigo + " status " + statusComboBox.Text +
                (_alterar ? " [TextoResposta] de " + _atendimento.TextoResposta + " para " + respostaTextBox.Text : "");
            _log.Tela = "SAC - Responder Atendimento";

            _atendimento.IdUsuarioRetorno = Publicas._idUsuario;
            _atendimento.TextoResposta = respostaTextBox.Text;
            _atendimento.Situacao = Publicas.SituacaoAtendimento.ManterComAtendente;
            _atendimento.Status = Publicas.StatusAtendimento.Respondido;

            _atendimento.ReclamacaoProcede = (SimRadioButton.Checked ? "S" : (NaoRadioButton.Checked ? "N" : ""));

            if (funcionarioTextBox.Text.Trim() != "")
                _atendimento.CodIntFunc = _funcionariosGlobus.Id;

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
                new Notificacoes.Mensagem("Problemas durante a gravação." + 
                    Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            if (!_alterar)
            {
                List<Usuario> _listaUsuarios = new List<Usuario>();

                Publicas.EnviarEmail("SAC - Atendimento ao Cliente", _empresa.Nome,
                        _usuarioAbertura.Nome, codigoTextBox.Text, DateTime.Now.ToShortDateString(),
                        _empresa.Telefone, _empresa.Email,
                        /* Destinatario*/ Publicas._usuario.Email + ";" + Publicas._usuario.EmailDepartamento + ";" + _usuarioAbertura.Email,
                        _empresa.Senha, _empresa.Nome + " - SAC nº " + codigoTextBox.Text, _empresa.Smtp,
                        _empresa.PortaSmtp, _empresa.Autentica, _empresa.AutenticaSLL, "", Publicas._usuario.Nome, "Respondido", respostaTextBox.Text);

                if (Publicas.mensagemDeErro != "")
                {
                    new Notificacoes.Mensagem("Problemas durante o envio do e-mail ao departamento." +
                        Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                    return;
                }
            }

            limparButton_Click(sender, e);
        }

        private void funcionarioTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;

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
                respostaTextBox.Focus();
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

        private void respostaTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text);
        }

        private void codigoEmtuTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaTipoButton.Enabled = string.IsNullOrEmpty(codigoEmtuTextBox.Text);
        }

        private void funcionarioTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaFuncionarioButto.Enabled = string.IsNullOrEmpty(funcionarioTextBox.Text);
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

        private void PesquisaFuncionarioButto_Click(object sender, EventArgs e)
        {
            if (funcionarioTextBox.Text.Trim() == "")
            {
                if (Publicas._setaParaBaixo)
                {
                    Publicas._setaParaBaixo = false;
                    funcionarioTextBox.BorderColor = Publicas._bordaSaida;

                    return;
                }

                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._participaAvaliacao = false;
                new Pesquisas.Funcionarios().ShowDialog();

                funcionarioTextBox.Text = Publicas._codigoRetornoPesquisa;

                if (funcionarioTextBox.Text.Trim() == "")
                {
                    funcionarioTextBox.Text = string.Empty;
                    Publicas._escTeclado = true;
                    funcionarioTextBox.Focus();
                    return;
                }

                funcionarioTextBox_Validating(sender, new CancelEventArgs());
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
                arquivoPanel_.Location = new Point(arquivoPanel_.Left, _topArquivos = 8);
                nomeArquivoLabel.Text = Path.GetFileNameWithoutExtension(arquivo);
                removerLabel.Tag = nomeArquivoLabel.Text;
                imagemArquivoPictureBox.ImageLocation = tipo;
                imagemArquivoPictureBox.Tag = nomeArquivoLabel.Text;
                arquivoPanel_.Visible = true;
            }
            else
            {
                Panel panel = new Panel();
                panel.Name = "arquivoPanel" + _qtd.ToString();

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

                panel.Size = arquivoPanel_.Size;
                panel.Location = new Point(arquivoPanel_.Left, _topArquivos);

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

            _topArquivos = _topArquivos + (arquivoPanel_.Height + 5);

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
                catch { }

                Log _log = new Log();
                _log.Descricao = (!_atendimento.Existe ? "Registrou " : "Alterou ");

                _log.Descricao = _log.Descricao + "atendimento " + _atendimento.Codigo + 
                    " foi excluido o anexo " + arquivo;
                _log.Tela = "SAC - Responder Atendimento";

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
            anexoImagensPanel.Top = 50;
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

            foreach (var item in _anexos.Where(w => w.NomeArquivo.Contains(((PictureBox)sender).Tag.ToString())))
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
    }
}
