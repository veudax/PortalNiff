using Classes;
using Negocio;
using Syncfusion.Windows.Forms;
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

namespace Suportte.DepartamentoPessoal
{
    public partial class CadastroProgramacaoDeFerias : Form
    {
        public CadastroProgramacaoDeFerias()
        {
            InitializeComponent();
            
            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }

                inicialDateTimePicker.Style = VisualStyle.Office2016Black;
                FimDateTimePicker.Style = VisualStyle.Office2016Black;
            }
            inicialDateTimePicker.Value = DateTime.Now;
            FimDateTimePicker.Value = DateTime.Now;
            QuantidadeCurrencyTextBox.BackGroundColor = empresaComboBoxAdv.BackColor;
            inicialDateTimePicker.BorderColor = Publicas._bordaSaida;
            FimDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._TemaBlack)
                QuantidadeCurrencyTextBox.PositiveColor = Publicas._fonte;

            Publicas._mensagemSistema = string.Empty;
        }

        Classes.ProgramacaoFerias _programacao;
        List<Classes.ProgramacaoFerias> _listaProgramacaoMesmoPeriodo;
        List<Classes.ProgramacaoFerias> _listaProgramacaoFuncionario;
        List<Classes.PeriodoAquisitivo> _listaPeriodos;

        Classes.Empresa _empresa;
        Classes.Usuario _usuario;
        Classes.PeriodoAquisitivo _aquisitivo;
        List <Classes.Empresa> _listaEmpresas;
        Classes.FuncionariosGlobus _funcionariosGlobus;

        DateTime _inicioAquisitivoG;
        DateTime _fimAquisitivoG;
        DateTime _limiteG;

        bool focarNoPeriodo = false;
        int qtdDias;

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

        private void CadastroProgramacaoDeFerias_Shown(object sender, EventArgs e)
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

            if (!usuarioTextBox.Enabled)
            {
                usuarioTextBox.Text = Publicas._usuario.RegistroFuncionario;
                _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(usuarioTextBox.Text, _empresa.CodigoEmpresaGlobus);

                //_usuario = new UsuarioBO().ConsultaUsuarioPorCodigoFuncionarioGlobus(_funcionariosGlobus.Id);

                //nomeTextBox.Text = _usuario.Nome;
                usuarioTextBox_Validating(sender, new CancelEventArgs());

                inicialDateTimePicker.Focus();
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
                ((Control)sender).Focus();
            }
        }

        private void usuarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PeriodoComboBox.Focus();
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
            {
                if (focarNoPeriodo)
                    PeriodoComboBox.Focus();
                else
                    QuantidadeCurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PeriodoComboBox.Focus();
            }
        }

        private void QuantidadeCurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (focarNoPeriodo)
                    PeriodoComboBox.Focus();
                else
                    GozadasCheckBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                inicialDateTimePicker.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GozadasCheckBox.Focus();
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

        private void usuarioTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void inicialDateTimePicker_Enter(object sender, EventArgs e)
        {
            inicialDateTimePicker.BorderColor = Publicas._bordaEntrada;
        }

        private void QuantidadeCurrencyTextBox_Enter(object sender, EventArgs e)
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
                Publicas._idEmpresa = _empresa.IdEmpresa;
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._participaAvaliacao = false;

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

            _funcionariosGlobus = new FuncionariosGlobusBO().ConsultarFuncionarioGlobus(usuarioTextBox.Text, _empresa.CodigoEmpresaGlobus);

            if (!_funcionariosGlobus.Existe)
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            _usuario = new UsuarioBO().ConsultaUsuarioPorCodigoFuncionarioGlobus(_funcionariosGlobus.Id);
            _listaPeriodos = new ProgramacaoFeriasBO().Listar(_empresa.IdEmpresa, _funcionariosGlobus.Id, DateTime.Now.Date);

            if (_listaPeriodos.Count() == 0)
            {
                new Notificacoes.Mensagem("Colaborador sem periodo aquisitivo cadastrado." + Environment.NewLine + 
                    "Solicite ao departamento pessoal para cadastrar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            PeriodoComboBox.DataSource = _listaPeriodos;
            PeriodoComboBox.DisplayMember = "Periodo";
            PeriodoComboBox.Focus();            

            DepartamentosGerenciadosPeloColaborador _deptpGerenciado = new DepartamentoBO().ListarIdUsuario(Publicas._usuario.Id, _usuario.IdDepartamento);
            Departamento _depto = new DepartamentoBO().Consultar(_usuario.IdDepartamento);

            if (_usuario.IdDepartamento != Publicas._usuario.IdDepartamento && !_deptpGerenciado.Existe && 
                (Publicas._usuario.Gerente || Publicas._usuario.Coordenador) && !Publicas._usuario.Desenvolvedor && 
                !Publicas._usuario.PodeIntegrarProgramacaoFerias)
            {
                new Notificacoes.Mensagem("Você não é o responsável por esse departamento." + Environment.NewLine + 
                    _depto.Descricao , Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            if (_usuario.DataAdmissao == DateTime.MinValue)
            {
                new Notificacoes.Mensagem("Colaborador sem data de admissão." + Environment.NewLine +
                    "Informe ao TI para corrigir o cadastro no Sistema Interno.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            PeriodoAquisitivoLabel.Text = "Admissão: " + _usuario.DataAdmissao.ToShortDateString();

            /*
            _inicioAquisitivo = _usuario.DataAdmissao.AddYears(1);
            _inicioAquisitivo1 = new DateTime(DateTime.Now.Year, _usuario.DataAdmissao.Month, _usuario.DataAdmissao.Day);

            if (DateTime.Now.Year - _inicioAquisitivo.Year >= 2)
                _inicioAquisitivo = new DateTime(DateTime.Now.AddYears(-1).Year, _usuario.DataAdmissao.Month, _usuario.DataAdmissao.Day);
            else
            {
                if (DateTime.Now.Date.Subtract(_usuario.DataAdmissao.Date).TotalDays < 365)
                {
                    _inicioAquisitivo = _usuario.DataAdmissao;
                    _inicioAquisitivo1 = new DateTime(DateTime.Now.AddYears(1).Year, _usuario.DataAdmissao.Month, _usuario.DataAdmissao.Day);
                }
            }

            _fimAquisitivo = _inicioAquisitivo.AddYears(1).AddDays(-1);
            _fimAquisitivo1 = _inicioAquisitivo1.AddYears(1).AddDays(-1);
            _limite = _fimAquisitivo.AddYears(1).AddMonths(-1).AddDays(1);
            _limite1 = _fimAquisitivo1.AddYears(1).AddMonths(-1).AddDays(1);

            PeriodoAquisitivoLabel.Text = "Admissão: " + _usuario.DataAdmissao.ToShortDateString() +
                " - Período Aquisitivo: " + _inicioAquisitivo.ToShortDateString() + " - " + _fimAquisitivo.ToShortDateString() +
                " - Limite: " + _limite.ToShortDateString();
                */
            nomeTextBox.Text = _usuario.Nome;
            
        }

        private void inicialDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            
            inicialDateTimePicker.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (inicialDateTimePicker.Value == DateTime.MinValue)
            {
                new Notificacoes.Mensagem("Inicio solicitado inválido", Publicas.TipoMensagem.Alerta).ShowDialog();
                inicialDateTimePicker.Focus();
                return;
            }

            if (inicialDateTimePicker.Value < DateTime.Now.Date)
            {
                new Notificacoes.Mensagem("Inicio menor que data atual", Publicas.TipoMensagem.Alerta).ShowDialog();
                inicialDateTimePicker.Focus();
                return;
            }
            
            if (_listaPeriodos.Count() == 0)
            {
                new Notificacoes.Mensagem("Colaborador sem periodo aquisitivo cadastrado." + Environment.NewLine +
                    "Solicite ao departamento pessoal para cadastrar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            _programacao = new ProgramacaoFeriasBO().Consultar(_funcionariosGlobus.Id, inicialDateTimePicker.Value);

            excluirButton.Enabled = _programacao.Existe;
            StatusLabel.Text = "Aguardando Aprovação";

            if (_programacao.Existe)
            {
                QuantidadeCurrencyTextBox.DecimalValue = _programacao.QuantidadeDias;
                FimDateTimePicker.Value = _programacao.DataFim;
                GozadasCheckBox.Checked = _programacao.Gozadas;
                StatusLabel.Text = (_programacao.Status == "G" ? "Aguardando Aprovação" : (_programacao.Status == "A" ? "Aprovado" : "Reprovado"));

                if (_programacao.IniPeriodoAquisitivo != DateTime.MinValue)
                {
                    PeriodoAquisitivoLabel.Text = "Admissão: " + _usuario.DataAdmissao.ToShortDateString();// +
                    //" - Período Aquisitivo: " + _programacao.IniPeriodoAquisitivo.ToShortDateString() + " - " + _programacao.FimPeriodoAquisitivo.ToShortDateString() +
                    //" - Limite: " + _programacao.Limite.ToShortDateString();

                    for (int i = 0; i < PeriodoComboBox.Items.Count-1; i++)
                    {
                        PeriodoComboBox.SelectedIndex = i;
                        if (PeriodoComboBox.Text == "Inicio: " + _programacao.IniPeriodoAquisitivo.ToShortDateString() +
                            " Fim: " + _programacao.FimPeriodoAquisitivo.ToShortDateString() +
                            " Limite: " + _programacao.Limite.ToShortDateString())
                        {
                            PeriodoComboBox_Validating(sender, new CancelEventArgs());
                            break;
                        }
                    }
                }
            }
            else
            {
                if (inicialDateTimePicker.Value.DayOfWeek != DayOfWeek.Monday)
                {
                    int dias = 0;

                    switch (inicialDateTimePicker.Value.DayOfWeek)
                    {
                        case DayOfWeek.Tuesday:
                            dias = -1;
                            break;
                        case DayOfWeek.Wednesday:
                            dias = -2;
                            break;
                        case DayOfWeek.Thursday:
                            dias = -3;
                            break;
                        case DayOfWeek.Friday:
                            dias = -4;
                            break;
                    }
                    FeriadoEmenda _feriado = new FeriadoBO().Consultar(inicialDateTimePicker.Value.AddDays(dias), _empresa.IdEmpresa);
                    if (!_feriado.Existe)
                    {
                        new Notificacoes.Mensagem("Inicio das Férias deve iniciar na Segunda-Feira.", Publicas.TipoMensagem.Alerta).ShowDialog();
                        inicialDateTimePicker.Focus();
                        return;
                    }
                }
            }

            if (_usuario.DataAdmissao != DateTime.MinValue)
            {
                if (inicialDateTimePicker.Value.Date <= _aquisitivo.Inicio && inicialDateTimePicker.Value.Date <= _aquisitivo.Fim)
                {
                    new Notificacoes.Mensagem("Inicio solicitado fora do período Aquisitivo", Publicas.TipoMensagem.Alerta).ShowDialog();
                    inicialDateTimePicker.Focus();
                    return;
                }

                _inicioAquisitivoG = _aquisitivo.Inicio;
                _fimAquisitivoG = _aquisitivo.Fim;
                _limiteG = _aquisitivo.Limite;

                if (inicialDateTimePicker.Value.Date > _aquisitivo.Limite)
                {
                    new Notificacoes.Mensagem("Inicio solicitado fora do período limite permitido " +
                        _aquisitivo.Inicio.ToShortDateString() + " até " +
                        _aquisitivo.Fim.ToShortDateString() + " - Limite " + _aquisitivo.Limite.ToShortDateString() +
                        Environment.NewLine + Environment.NewLine +
                        "Selecione outro período aquisitivo."
                        , Publicas.TipoMensagem.Alerta);

                    focarNoPeriodo = true;
                    PeriodoComboBox.Focus();
                    return;
                }
                
            }
            _listaProgramacaoFuncionario = new ProgramacaoFeriasBO().Listar(_funcionariosGlobus.Id);
            qtdDias = 0;

            foreach (var item in _listaProgramacaoFuncionario.Where(w => w.Status != "R"))
            {
                if (item.IniPeriodoAquisitivo == _inicioAquisitivoG)
                    qtdDias = qtdDias + item.QuantidadeDias;
            }

            if (30 - qtdDias <= 0)
                QuantidadeRestanteLabel.Text = "Total de dias completos";
            else
                QuantidadeRestanteLabel.Text = (30 - qtdDias).ToString() + " restantes para completar o período";
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

        private void QuantidadeCurrencyTextBox_DecimalValueChanged(object sender, EventArgs e)
        {
            FimDateTimePicker.Value = inicialDateTimePicker.Value.AddDays(Convert.ToInt32(QuantidadeCurrencyTextBox.Text)-1);
        }

        private void QuantidadeCurrencyTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((CurrencyTextBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            FimDateTimePicker.Value = inicialDateTimePicker.Value.AddDays(Convert.ToInt32(QuantidadeCurrencyTextBox.Text) - 1);

            if (!_programacao.Existe)
            {
                _listaProgramacaoMesmoPeriodo = new ProgramacaoFeriasBO().ListarPeriodoIguaisDeFerias(_funcionariosGlobus.Id, inicialDateTimePicker.Value, FimDateTimePicker.Value, _usuario.IdEmpresa, _usuario.IdDepartamento);
                if (_listaProgramacaoMesmoPeriodo.Where(w => w.IdDepartamento == _usuario.IdDepartamento).Count() != 0)
                {
                    new Notificacoes.Mensagem("Existe colaborador no seu departamento com Férias para este período.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    QuantidadeCurrencyTextBox.Focus();
                    return;
                }

                if (qtdDias + QuantidadeCurrencyTextBox.DecimalValue > 30)
                {
                    new Notificacoes.Mensagem("A soma das quantidade de dias solicitados superior a 30 dias." + 
                        Environment.NewLine + 
                        "Verifique na tela de 'Consulta de Programação de Férias' as datas cadastradas." +
                        Environment.NewLine +
                        "Caso queria trocar os períodos exclua o período antes de cadastrar a nova solicitação.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    inicialDateTimePicker.Focus();
                    return;
                }

            }

            gravarButton.Enabled = true;

        }

        private void usuarioTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {

            if (_listaPeriodos.Count() == 0)
            {
                new Notificacoes.Mensagem("Colaborador sem periodo aquisitivo cadastrado." + Environment.NewLine +
                    "Solicite ao departamento pessoal para cadastrar.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            if (inicialDateTimePicker.Value == DateTime.MinValue)
            {
                new Notificacoes.Mensagem("Inicio solicitado inválido", Publicas.TipoMensagem.Alerta).ShowDialog();
                inicialDateTimePicker.Focus();
                return;
            }

            if (QuantidadeCurrencyTextBox.DecimalValue == 0)
            {
                new Notificacoes.Mensagem("Informe a quantidade de dias.", Publicas.TipoMensagem.Alerta).ShowDialog();
                QuantidadeCurrencyTextBox.Focus();
                return;
            }

            if (!_programacao.Existe)
            {
                if (_listaProgramacaoMesmoPeriodo.Where(w => w.IdDepartamento == _usuario.IdDepartamento).Count() != 0)
                {
                    new Notificacoes.Mensagem("Existe colaborador no seu departamento com Férias para este período.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    QuantidadeCurrencyTextBox.Focus();
                    return;
                }
            }

            _programacao.QuantidadeDias = (int)QuantidadeCurrencyTextBox.DecimalValue;
            _programacao.DataInicio = inicialDateTimePicker.Value;
            _programacao.DataFim = FimDateTimePicker.Value;
            _programacao.CodIntFunc = _funcionariosGlobus.Id;
            _programacao.IdEmpresa = _empresa.IdEmpresa;
            _programacao.Gozadas = GozadasCheckBox.Checked;
            _programacao.IniPeriodoAquisitivo = _inicioAquisitivoG;
            _programacao.FimPeriodoAquisitivo = _fimAquisitivoG;
            _programacao.Limite = _limiteG;

            if (inicialDateTimePicker.Value > DateTime.Now.Date)
                _programacao.Gozadas = false;

            if (!_programacao.Existe)
                _programacao.Status = "G";

            if (!new ProgramacaoFeriasBO().Gravar(_programacao))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Gravou Programação de Férias de " + nomeTextBox.Text +
                " periodo de " + inicialDateTimePicker.Value.ToShortDateString() + " ate " + FimDateTimePicker.Value.ToShortDateString() +
                " da empresa " + empresaComboBoxAdv.Text;

            _log.Tela = "Recursos Humanos - Programação Férias";
            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }


            limparButton_Click(sender, e);

        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            QuantidadeCurrencyTextBox.DecimalValue = 0;
            GozadasCheckBox.Checked = false;
            QuantidadeRestanteLabel.Text = string.Empty;

            if (usuarioTextBox.Enabled)
            {
                usuarioTextBox.Text = string.Empty;
                nomeTextBox.Text = string.Empty;
                PeriodoAquisitivoLabel.Text = string.Empty;

                usuarioTextBox.Focus();
            }
            else
                inicialDateTimePicker.Focus();

        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new ProgramacaoFeriasBO().Excluir(_programacao.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu Programação de Férias de " + nomeTextBox.Text +
                " periodo de " + inicialDateTimePicker.Value.ToShortDateString() + " ate " + FimDateTimePicker.Value.ToShortDateString() +
                " da empresa " + empresaComboBoxAdv.Text;

            _log.Tela = "Recursos Humanos - Programação Férias";
            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            limparButton_Click(sender, e);
        }

        private void GozadasCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                QuantidadeCurrencyTextBox.Focus();
            }
        }

        private void PeriodoComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                inicialDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                usuarioTextBox.Focus();
            }
        }

        private void PeriodoComboBox_Validating(object sender, CancelEventArgs e)
        {
            PeriodoComboBox.FlatBorderColor = Publicas._bordaSaida;

            focarNoPeriodo = false;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaPeriodos.Where(w => w.Periodo == PeriodoComboBox.Text))
            {
                _aquisitivo = item;
            }

            _inicioAquisitivoG = _aquisitivo.Inicio;

            _listaProgramacaoFuncionario = new ProgramacaoFeriasBO().Listar(_funcionariosGlobus.Id);
            qtdDias = 0;

            foreach (var item in _listaProgramacaoFuncionario.Where(w => w.Status != "R"))
            {
                if (item.IniPeriodoAquisitivo == _inicioAquisitivoG)
                    qtdDias = qtdDias + item.QuantidadeDias;
            }

            if (30 - qtdDias <= 0)
                QuantidadeRestanteLabel.Text = "Total de dias completos";
            else
                QuantidadeRestanteLabel.Text = (30 - qtdDias).ToString() + " dias restantes para completar o período";
        }
    }
}
