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

namespace Suportte.ProcessoSeletivo
{
    public partial class Vagas : Form
    {
        public Vagas()
        {
            InitializeComponent();

            AberturaDateTimePicker.BackColor = codigoTextBox.BackColor;
            EncerramentoDateTimePicker.BackColor = codigoTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Empresa _empresaEntrevista;
        Classes.Vagas _vagas;
        Classes.Candidatos _candidatos;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.HistoricoDoCandidato> _historico;
        
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

        private void Vagas_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";

            EmpresaEntrevistaComboBox.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            EmpresaEntrevistaComboBox.DisplayMember = "CodigoeNome";

            _empresa = new EmpresaBO().Consultar(Publicas._usuario.IdEmpresa);
            
            for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
            {
                empresaComboBoxAdv.SelectedIndex = i;
                if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                {
                    break;
                }
            }

            StatusComboBox.Items.AddRange(new object[] { "Aberta", "Encerrada", "Congelada" });
        }
        
        private void codigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '+')
            {
                codigoTextBox.Text = string.Empty;
                proximoButton_Click(sender, e);
            }
        }
        private void codigoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ConfidencialCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void ConfidencialCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                nomeTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void nomeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                StatusComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                codigoTextBox.Focus();
            }
        }

        private void StatusComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                AberturaDateTimePicker.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
            }
        }

        private void AberturaDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (EncerramentoDateTimePicker.Enabled)
                    EncerramentoDateTimePicker.Focus();
                else
                    empresaComboBoxAdv.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                StatusComboBox.Focus();
            }
        }

        private void EncerramentoDateTimePicker_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                empresaComboBoxAdv.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                AberturaDateTimePicker.Focus();
            }
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                CathoCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (EncerramentoDateTimePicker.Enabled)
                    EncerramentoDateTimePicker.Focus();
                else
                    AberturaDateTimePicker.Focus();
            }
        }

        private void CathoCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                InfoJobsCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void InfoJobsCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                LinkedinCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CathoCheckBox.Focus();
            }
        }

        private void LinkedinCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OutrosCheckBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                InfoJobsCheckBox.Focus();
            }
        }

        private void OutrosCheckBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (DescricaoOutrosTextBox.Enabled)
                    DescricaoOutrosTextBox.Focus();
                else
                {
                    if (CandidatoTextBox.Enabled)
                        CandidatoTextBox.Focus();
                    else
                        gravarButton.Focus();
                }
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                LinkedinCheckBox.Focus();
            }
        }


        private void DescricaoOutrosTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (CandidatoTextBox.Enabled)
                    CandidatoTextBox.Focus();
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OutrosCheckBox.Focus();
            }
        }

        private void CandidatoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                MotivoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (DescricaoOutrosTextBox.Enabled)
                    DescricaoOutrosTextBox.Focus();
                else
                    OutrosCheckBox.Focus();
            }
        }

        private void MotivoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                EmpresaEntrevistaComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (CandidatoTextBox.Enabled)
                    CandidatoTextBox.Focus();
                else
                {
                    if (DescricaoOutrosTextBox.Enabled)
                        DescricaoOutrosTextBox.Focus();
                    else
                        OutrosCheckBox.Focus();
                }
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                InformacoesGeraisTextBox.Focus();
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

        private void nomeTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void CandidatoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            PesquisaColaboradorButton.Enabled = string.IsNullOrEmpty(CandidatoTextBox.Text.Trim());
        }

        private void codigoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void StatusComboBox_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void AberturaDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
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

        private void gravarButton_Validating(object sender, CancelEventArgs e)
        {
            gravarButton.BackColor = Publicas._botao;
            gravarButton.ForeColor = Publicas._fonteBotao;
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            limparButton.BackColor = Publicas._botao;
            limparButton.ForeColor = Publicas._fonteBotao;
        }

        private void excluirButton_Validating(object sender, CancelEventArgs e)
        {
            excluirButton.BackColor = Publicas._botao;
            excluirButton.ForeColor = Publicas._fonteBotao;
        }

        private void codigoTextBox_Validating(object sender, CancelEventArgs e)
        {
            codigoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                codigoTextBox.Text = string.Empty;
                pesquisaCategoriaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()))
            {
                new Pesquisas.Vaga().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            _vagas = new CurriculosBO().ConsultarVaga(Convert.ToInt32(codigoTextBox.Text.Trim()));

            if (_vagas.Existe)
            {
                nomeTextBox.Text = _vagas.Descricao;

                if (_vagas.IdCandidato != 0)
                { 
                    CandidatoTextBox.Text = _vagas.IdCandidato.ToString();
                    CandidatoTextBox_Validating(sender, e);
                }

                AberturaDateTimePicker.Value = _vagas.Abertura;
                EncerramentoDateTimePicker.Value = _vagas.Encerramento;

                StatusComboBox.SelectedIndex = (_vagas.Status == "A" ? 0 : (_vagas.Status == "E" ? 1 : 2));

                CathoCheckBox.Checked = _vagas.Catho;
                InfoJobsCheckBox.Checked = _vagas.Infojobs;
                LinkedinCheckBox.Checked = _vagas.LinkedIn;
                OutrosCheckBox.Checked = _vagas.Outros;
                ConfidencialCheckBox.Checked = _vagas.Confidencial;

                DescricaoOutrosTextBox.Text = _vagas.DescricaoOutros;
                MotivoTextBox.Text = _vagas.Detalhamento;
                InformacoesGeraisTextBox.Text = _vagas.InformacoesGerais;
                LocalEntrevistaTextBox.Text = _vagas.EnderecoEntevista;

                _empresa = new EmpresaBO().Consultar(_vagas.IdEmpresa);

                for (int i = 0; i < empresaComboBoxAdv.Items.Count; i++)
                {
                    empresaComboBoxAdv.SelectedIndex = i;
                    if (empresaComboBoxAdv.Text == _empresa.CodigoeNome)
                    {
                        break;
                    }
                }

                _empresaEntrevista = new EmpresaBO().Consultar(_vagas.IdEmpresaEntrevista);

                for (int i = 0; i < EmpresaEntrevistaComboBox.Items.Count; i++)
                {
                    EmpresaEntrevistaComboBox.SelectedIndex = i;
                    if (EmpresaEntrevistaComboBox.Text == _empresaEntrevista.CodigoeNome)
                    {
                        break;
                    }
                }
            }

            gravarButton.Enabled = true;
            excluirButton.Enabled = _vagas.Existe;
        }

        private void CandidatoTextBox_Validating(object sender, CancelEventArgs e)
        {
            CandidatoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                CandidatoTextBox.Text = string.Empty;
                PesquisaColaboradorButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(CandidatoTextBox.Text.Trim()))
            {
                new Pesquisas.Candidato().ShowDialog();

                CandidatoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(CandidatoTextBox.Text) || CandidatoTextBox.Text == "0")
                {
                    CandidatoTextBox.Text = string.Empty;
                    CandidatoTextBox.Focus();
                    return;
                }
            }

            _candidatos = new CurriculosBO().ConsultarCandidato(Convert.ToInt32(CandidatoTextBox.Text.Trim()));

            if (!_candidatos.Existe)
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CandidatoTextBox.Focus();
                return;
            }

            NomeCandidatoTextBox.Text = _candidatos.Nome;
        }

        private void nomeTextBox_Validating(object sender, CancelEventArgs e)
        {
            nomeTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void DescricaoOutrosTextBox_Validating(object sender, CancelEventArgs e)
        {
            DescricaoOutrosTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }
        
        private void MotivoTextBox_Validating(object sender, CancelEventArgs e)
        {
            MotivoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void StatusComboBox_Validating(object sender, CancelEventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void empresaComboBoxAdv_Validating(object sender, CancelEventArgs e)
        {
            empresaComboBoxAdv.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            EmpresaEntrevistaComboBox.Text = empresaComboBoxAdv.Text;
            
            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == EmpresaEntrevistaComboBox.Text))
            {
                _empresaEntrevista = item;
            }
        }

        private void AberturaDateTimePicker_Validating(object sender, CancelEventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void codigoTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaCategoriaButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
            proximoButton.Enabled = string.IsNullOrEmpty(codigoTextBox.Text.Trim());
        }

        private void CandidatoTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaColaboradorButton.Enabled = string.IsNullOrEmpty( CandidatoTextBox.Text.Trim());
        }

        private void StatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EncerramentoDateTimePicker.Enabled = StatusComboBox.SelectedIndex == 1;
            CandidatoTextBox.Enabled = StatusComboBox.SelectedIndex == 1;
        }
        
        private void OutrosCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DescricaoOutrosTextBox.Enabled = OutrosCheckBox.Checked;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = new CurriculosBO().ProximaVaga().ToString();
            nomeTextBox.Focus();
        }

        private void pesquisaCategoriaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoTextBox.Text.Trim()))
            {
                new Pesquisas.Vaga().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(codigoTextBox.Text) || codigoTextBox.Text == "0")
                {
                    codigoTextBox.Text = string.Empty;
                    codigoTextBox.Focus();
                    return;
                }
            }

            codigoTextBox_Validating(sender, new CancelEventArgs());
        }

        private void PesquisaColaboradorButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CandidatoTextBox.Text.Trim()))
            {
                new Pesquisas.Candidato().ShowDialog();

                codigoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (string.IsNullOrEmpty(CandidatoTextBox.Text) || CandidatoTextBox.Text == "0")
                {
                    CandidatoTextBox.Text = string.Empty;
                    CandidatoTextBox.Focus();
                    return;
                }

                CandidatoTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a código.", Publicas.TipoMensagem.Alerta).ShowDialog();
                codigoTextBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(nomeTextBox.Text))
            {
                new Notificacoes.Mensagem("Informe a descrição.", Publicas.TipoMensagem.Alerta).ShowDialog();
                nomeTextBox.Focus();
                return;
            }

            if (_empresa.IdEmpresa == 0)
            {
                new Notificacoes.Mensagem("Informe a empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            _vagas.Id = Convert.ToInt32(codigoTextBox.Text);
            _vagas.Descricao = nomeTextBox.Text;

            if (_candidatos != null)
                _vagas.IdCandidato = _candidatos.Id;
            else
                _vagas.IdCandidato = 0;

            _vagas.Abertura = AberturaDateTimePicker.Value;

            if (EncerramentoDateTimePicker.Enabled)
                _vagas.Encerramento = EncerramentoDateTimePicker.Value;

            _vagas.Status= StatusComboBox.Text.Substring(0, 1);
            
            _vagas.DescricaoOutros = DescricaoOutrosTextBox.Text;

            _vagas.Catho = CathoCheckBox.Checked;
            _vagas.Infojobs = InfoJobsCheckBox.Checked;
            _vagas.LinkedIn= LinkedinCheckBox.Checked;
            _vagas.Outros = OutrosCheckBox.Checked;
            _vagas.IdEmpresa = _empresa.IdEmpresa;
            _vagas.Confidencial = ConfidencialCheckBox.Checked;
            _vagas.Detalhamento = MotivoTextBox.Text;
            _vagas.InformacoesGerais = InformacoesGeraisTextBox.Text;
            _vagas.EnderecoEntevista = LocalEntrevistaTextBox.Text;
            _vagas.IdEmpresaEntrevista = _empresaEntrevista.IdEmpresa;
            
            if (!new CurriculosBO().GravarVaga(_vagas))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (_vagas.Status == "E")
            {
                _historico = new CurriculosBO().ListaHistorico(0, _vagas.Id);

                foreach (var item in _historico.GroupBy(g => g.IdCandidato))
                {
                    Classes.HistoricoDoCandidato _histo = new HistoricoDoCandidato();
                    _histo.Status = "14";
                    _histo.IdVaga = _vagas.Id;
                    _histo.IdCandidato = item.Key;

                    List<HistoricoDoCandidato> _lista = new List<HistoricoDoCandidato>();

                    _lista.Add(_histo);

                    new CurriculosBO().GravarHistorico(_lista);
                    
                }
            }

            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            codigoTextBox.Text = string.Empty;
            nomeTextBox.Text = string.Empty;
            CandidatoTextBox.Text = string.Empty;
            NomeCandidatoTextBox.Text = string.Empty;
            DescricaoOutrosTextBox.Text = string.Empty;
            MotivoTextBox.Text = string.Empty;
            LocalEntrevistaTextBox.Text = string.Empty;
            InformacoesGeraisTextBox.Text = string.Empty;
            StatusComboBox.SelectedIndex = 0;
            AberturaDateTimePicker.Value = DateTime.MinValue;
            EncerramentoDateTimePicker.Value = DateTime.MinValue;
            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
            CathoCheckBox.Checked = false;
            InfoJobsCheckBox.Checked = false;
            LinkedinCheckBox.Checked = false;
            OutrosCheckBox.Checked = false;
            ConfidencialCheckBox.Checked = false;

            codigoTextBox.Focus();
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new CurriculosBO().ExcluirVaga(_vagas.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            limparButton_Click(sender, e);
        }

        private void EmpresaEntrevistaComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                LocalEntrevistaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                MotivoTextBox.Focus();
            }
        }

        private void LocalEntrevistaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                InformacoesGeraisTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                EmpresaEntrevistaComboBox.Focus();
            }
        }

        private void InformacoesGeraisTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                LocalEntrevistaTextBox.Focus();
            }
        }

        private void EmpresaEntrevistaComboBox_Validating(object sender, CancelEventArgs e)
        {
            EmpresaEntrevistaComboBox.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaEmpresas.Where(w => w.CodigoeNome == EmpresaEntrevistaComboBox.Text))
            {
                _empresaEntrevista = item;
            }
        }

        private void LocalEntrevistaTextBox_Validating(object sender, CancelEventArgs e)
        {
            LocalEntrevistaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void InformacoesGeraisTextBox_Validating(object sender, CancelEventArgs e)
        {
            InformacoesGeraisTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }
    }
}
