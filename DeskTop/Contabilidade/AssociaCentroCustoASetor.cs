using Negocio;
using Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Grouping;
using DynamicFilter;

namespace Suportte.Contabilidade
{
    public partial class AssociaCentroCustoASetor : Form
    {
        public AssociaCentroCustoASetor()
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
                    gridGroupingControl1.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl1.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl1.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl1.BackColor = Publicas._panelTitulo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }


        Classes.Empresa _empresa;
        Classes.RateioBeneficios.ContasContabeis _contas;
        Classes.RateioBeneficios.ContasContabeis _contaDestino;
        Classes.RateioBeneficios.Setor _setor;
        Classes.CentroDeCustoContabil _custos;
        Classes.RateioBeneficios.Parametros _param;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.EmpresaDoUsuario> _listaEmpresasAutorizadas;
        List<Classes.RateioBeneficios.Associacao> _listaAssociacoes;
        List<Classes.RateioBeneficios.Associacao> _listaAssociacoesLog;

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

        private void AssociaCentroCustoASetor_Shown(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);
            _listaEmpresasAutorizadas = new UsuarioBO().ConsultaEmpresasAutorizadasDoUsuario(Publicas._idUsuario);

            empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            empresaComboBoxAdv.DisplayMember = "CodigoeNome";
            empresaComboBoxAdv.Focus();

            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.gridGroupingControl1);

            gridGroupingControl1.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl1.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < gridGroupingControl1.TableDescriptor.Columns.Count; i++)
            {
                gridGroupingControl1.TableDescriptor.Columns[i].AllowFilter = true;
                gridGroupingControl1.TableDescriptor.Columns[i].ReadOnly = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                gridGroupingControl1.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }

            GridMetroColors metroColor = new GridMetroColors();
            metroColor.HeaderBottomBorderColor = Publicas._bordaEntrada;
            metroColor.HeaderColor.HoverColor = Publicas._bordaEntrada;
            metroColor.HeaderColor.PressedColor = Publicas._bordaEntrada;

            metroColor.CheckBoxColor.BorderColor = Publicas._bordaEntrada;
            metroColor.PushButtonColor.PushedBackColor = Publicas._bordaEntrada;
            metroColor.PushButtonColor.HoverBackColor = Publicas._bordaEntrada;
            metroColor.PushButtonColor.NormalBackColor = Color.WhiteSmoke;
            metroColor.ComboboxColor.NormalBorderColor = Publicas._bordaEntrada;
            metroColor.ComboboxColor.HoverBorderColor = Publicas._bordaEntrada;
            metroColor.ComboboxColor.HoverBackColor = Publicas._bordaEntrada;
            metroColor.ComboboxColor.PressedBackColor = Publicas._bordaEntrada;
            metroColor.ComboboxColor.NormalBackColor = Color.WhiteSmoke;

            if (!Publicas._TemaBlack)
            {
                this.gridGroupingControl1.SetMetroStyle(metroColor);
                this.gridGroupingControl1.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl1.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl1.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.HideCurrentCell;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl1.Table.DefaultRecordRowHeight = 22;
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void CustoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ContaTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gridGroupingControl1.Focus();
            }
        }

        private void ContaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                ContaDestinoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CustoTextBox.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gridGroupingControl1.Focus();
            }
        }

        private void ContaDestinoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SetorTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ContaTextBox.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gridGroupingControl1.Focus();
            }
        }

        private void SetorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                proximoButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ContaTextBox.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                Publicas._setaParaBaixo = true;
                gridGroupingControl1.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                gridGroupingControl1.Focus();
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
                LimparButton.Focus();
            }
        }
        
        private void CopiarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                excluirButton.Focus();
            }
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
        }

        private void CustoTextBox_Enter(object sender, EventArgs e)
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
            LimparButton.BackColor = Publicas._botaoFocado;
            LimparButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void excluirButton_Enter(object sender, EventArgs e)
        {
            excluirButton.BackColor = Publicas._botaoFocado;
            excluirButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void CopiarButton_Enter(object sender, EventArgs e)
        {
            CopiarButton.BackColor = Publicas._botaoFocado;
            CopiarButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void CopiarButton_Validating(object sender, CancelEventArgs e)
        {
            CopiarButton.BackColor = Publicas._botao;
            CopiarButton.ForeColor = Publicas._fonteBotao;
        }

        private void excluirButton_Validating(object sender, CancelEventArgs e)
        {
            excluirButton.BackColor = Publicas._botao;
            excluirButton.ForeColor = Publicas._fonteBotao;
        }

        private void limparButton_Validating(object sender, CancelEventArgs e)
        {
            LimparButton.BackColor = Publicas._botao;
            LimparButton.ForeColor = Publicas._fonteBotao;
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

            if (_listaEmpresasAutorizadas.Where(w => w.IdEmpresa == _empresa.IdEmpresa && w.EmpresaAutoriza).Count() == 0)
            {
                new Notificacoes.Mensagem("Usuário não autorizado para está empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            _param = new RateioBeneficioBO().ConsultarParametro(_empresa.IdEmpresa);

            if (!_param.Existe)
            {
                new Notificacoes.Mensagem("Parâmetros de rateio não cadastrado para essa empresa.", Publicas.TipoMensagem.Alerta).ShowDialog();
                empresaComboBoxAdv.Focus();
                return;
            }

            _listaAssociacoes = new RateioBeneficioBO().ListarAssociacoes(_empresa.IdEmpresa, _param.Id);
            _listaAssociacoesLog = new RateioBeneficioBO().ListarAssociacoes(_empresa.IdEmpresa, _param.Id);

            gridGroupingControl1.DataSource = _listaAssociacoes;

            gravarButton.Enabled = _listaAssociacoes.Count() != 0;
            excluirButton.Enabled = _listaAssociacoes.Count() != 0;
            CopiarButton.Enabled = _listaAssociacoes.Count() != 0;
        }

        private void CustoTextBox_Validating(object sender, CancelEventArgs e)
        {
            CustoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (CustoTextBox.Text.Trim() == "")
            {
                Publicas._codigoRetornoPesquisa = _empresa.CodigoEmpresaGlobus;
                new Pesquisas.CentroDeCustoContabil().ShowDialog();

                CustoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CustoTextBox.Text.Trim() == "" || CustoTextBox.Text == "0")
                {
                    CustoTextBox.Text = string.Empty;
                    CustoTextBox.Focus();
                    return;
                }
            }

            _custos = new CentroDeCustoContabilBO().Consultar(Convert.ToInt32(CustoTextBox.Text), _param.NumeroPlano);

            if (!_custos.Existe)
            {
                new Notificacoes.Mensagem("Centro de custo não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CustoTextBox.Focus();
                return;
            }

            if (!_custos.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Centro de custo não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                CustoTextBox.Focus();
                return;
            }

            NomeCustoTextBox.Text = _custos.Classificador + " " + _custos.Descricao;
        }

        private void ContaTextBox_Validating(object sender, CancelEventArgs e)
        {
            ContaTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }


            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (ContaTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _param.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                ContaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (ContaTextBox.Text.Trim() == "" || ContaTextBox.Text == "0")
                {
                    ContaTextBox.Text = string.Empty;
                    ContaTextBox.Focus();
                    return;
                }
            }

            _contas = new RateioBeneficioBO().Consultar(_param.NumeroPlano, Convert.ToInt32(ContaTextBox.Text));

            if (!_contas.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContaTextBox.Focus();
                return;
            }

            if (!_contas.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContaTextBox.Focus();
                return;
            }

            NomeContaTextBox.Text = _contas.Classificador + " " + _contas.Nome;
        }
        
        private void ContaDestinoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ContaDestinoTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (ContaDestinoTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _param.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                ContaDestinoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (ContaDestinoTextBox.Text.Trim() == "" || ContaDestinoTextBox.Text == "0")
                {
                    ContaDestinoTextBox.Text = string.Empty;
                    ContaDestinoTextBox.Focus();
                    return;
                }
            }

            _contaDestino = new RateioBeneficioBO().Consultar(_param.NumeroPlano, Convert.ToInt32(ContaDestinoTextBox.Text));

            if (!_contaDestino.Existe)
            {
                new Notificacoes.Mensagem("Conta Contábil não cadastrada.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContaDestinoTextBox.Focus();
                return;
            }

            if (!_contaDestino.AceitaLancamento)
            {
                new Notificacoes.Mensagem("Conta Contábil não aceita lançamento.", Publicas.TipoMensagem.Alerta).ShowDialog();
                ContaDestinoTextBox.Focus();
                return;
            }

            NomeContaDestinoTextBox.Text = _contaDestino.Classificador + " " + _contaDestino.Nome;
        }

        private void SetorTextBox_Validating(object sender, CancelEventArgs e)
        {
            SetorTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (Publicas._setaParaBaixo)
            {
                Publicas._setaParaBaixo = false;
                return;
            }

            Publicas._idRetornoPesquisa = 0;

            if (SetorTextBox.Text.Trim() == "")
            {                
                new Pesquisas.SetorFolha().ShowDialog();

                SetorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (SetorTextBox.Text.Trim() == "" || SetorTextBox.Text == "0")
                {
                    SetorTextBox.Text = string.Empty;
                    SetorTextBox.Focus();
                    return;
                }
            }

            _setor = new RateioBeneficioBO().ConsultarSetor(Convert.ToInt32(SetorTextBox.Text));

            if (!_setor.Existe)
            {
                new Notificacoes.Mensagem("Setor não cadastrado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                SetorTextBox.Focus();
                return;
            }
            
            NomeSetorTextBox.Text = _setor.Nome;
        }

        private void PesquisaSetorButton_Click(object sender, EventArgs e)
        {
            if (SetorTextBox.Text.Trim() == "")
            {
                new Pesquisas.SetorFolha().ShowDialog();

                SetorTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (SetorTextBox.Text.Trim() == "" || SetorTextBox.Text == "0")
                {
                    SetorTextBox.Text = string.Empty;
                    SetorTextBox.Focus();
                    return;
                }

                SetorTextBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void PesquisaContaButton_Click(object sender, EventArgs e)
        {
            if (ContaTextBox.Text.Trim() == "")
            {
                Publicas._idRetornoPesquisa = _param.NumeroPlano;
                new Pesquisas.ContaContabil().ShowDialog();

                ContaTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (ContaTextBox.Text.Trim() == "" || ContaTextBox.Text == "0")
                {
                    ContaTextBox.Text = string.Empty;
                    ContaTextBox.Focus();
                    return;
                }

                ContaTextBox_Validating(sender, new CancelEventArgs());
            }

        }

        private void PesquisaCustoButton_Click(object sender, EventArgs e)
        {
            if (CustoTextBox.Text.Trim() == "")
            {
                Publicas._codigoRetornoPesquisa = _empresa.CodigoEmpresaGlobus;
                new Pesquisas.CentroDeCustoContabil().ShowDialog();

                CustoTextBox.Text = Publicas._idRetornoPesquisa.ToString();

                if (CustoTextBox.Text.Trim() == "" || CustoTextBox.Text == "0")
                {
                    CustoTextBox.Text = string.Empty;
                    CustoTextBox.Focus();
                    return;
                }

                CustoTextBox_Validating(sender, new CancelEventArgs());
            }

        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            if (_listaAssociacoes.Where(w => w.CodigoCusto == _custos.Codigo && w.CodConta == _contas.Codigo && w.CodigoSetor == _setor.Codigo).Count() == 0)
            {
                _listaAssociacoes.Add(new RateioBeneficios.Associacao()
                {
                    IdEmpresa = _empresa.IdEmpresa,
                    IdParam = _param.Id,
                    CodigoCusto = _custos.Codigo,
                    Nome = _custos.Classificador + " " + _custos.Descricao,
                    CodigoSetor = _setor.Codigo,
                    NomeSetor = _setor.Nome,
                    CodConta = _contas.Codigo,
                    NomeConta = _contas.Classificador + " " + _contas.Nome,
                    CodContaDestino = _contaDestino.Codigo,
                    NomeContaDestino = _contaDestino.Classificador + " " + _contaDestino.Nome
                });
            }

            gridGroupingControl1.DataSource = new List<RateioBeneficios.Associacao>();
            gridGroupingControl1.DataSource = _listaAssociacoes;

            ContaTextBox.Text = string.Empty;
            ContaDestinoTextBox.Text = string.Empty;
            NomeContaDestinoTextBox.Text = string.Empty;
            NomeContaTextBox.Text = string.Empty;
            SetorTextBox.Text = string.Empty;
            NomeSetorTextBox.Text = string.Empty;
            ContaTextBox.Focus();
            gravarButton.Enabled = _listaAssociacoes.Count() != 0;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (_listaAssociacoes.Count() == 0)
            {
                new Notificacoes.Mensagem("Nenhum centro de custo associado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }

            if (!new RateioBeneficioBO().Gravar(_listaAssociacoes))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;

            _log.Descricao = (_listaAssociacoesLog.Count() == 0 ? "Incluiu" : "Alterou") + " associação da empresa " + empresaComboBoxAdv.Text;

            string _descricao = "";

            foreach (var item in _listaAssociacoes)
            {
                if (_listaAssociacoesLog.Where(w => w.CodigoCusto == item.CodigoCusto).Count() == 0)
                {
                    _descricao = _descricao + " Incluido o centro de custo " + item.CodigoCusto + " conta "  + item.CodConta +
                        " conta destino " + item.CodContaDestino + " setor " + item.CodigoSetor;
                }
            }

            _log.Descricao = _log.Descricao + _descricao;
            _log.Tela = "Contabilidade - Cadastros - Associacoes";

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            LimparButton_Click(sender, e);
        }

        private void LimparButton_Click(object sender, EventArgs e)
        {
            gridGroupingControl1.DataSource = new List<RateioBeneficios.Associacao>();
            ContaTextBox.Text = string.Empty;
            NomeContaTextBox.Text = string.Empty;
            SetorTextBox.Text = string.Empty;
            NomeSetorTextBox.Text = string.Empty;
            CustoTextBox.Text = string.Empty;
            NomeCustoTextBox.Text = string.Empty;
            NomeContaDestinoTextBox.Text = string.Empty;
            ContaDestinoTextBox.Text = string.Empty;
            empresaComboBoxAdv.Focus();

            gravarButton.Enabled = false;
            excluirButton.Enabled = false;
        }

        private void excluirButton_Click(object sender, EventArgs e)
        {
            if (new Notificacoes.Mensagem("Confirma a exclusão?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                return;

            if (!new RateioBeneficioBO().ExcluiTodasAssociacoes(_param.Id))
            {
                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao = "Excluiu as associações da empresa " + empresaComboBoxAdv.Text;

            string _descricao = "";

            foreach (var item in _listaAssociacoesLog)
            {
                _descricao = _descricao + " Excluido o centro de custo " + item.CodigoCusto +
                    " conta " + item.CodConta +
                    " conta destino " + item.CodContaDestino + " setor " + item.CodigoSetor;
            }

            _log.Tela = "Contabilidade - Cadastros - Associacoes";
            _log.Descricao = _log.Descricao + _descricao;

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            LimparButton_Click(sender, e);
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridRecordRow rec = this.gridGroupingControl1.Table.DisplayElements[gridGroupingControl1.TableControl.CurrentCell.RowIndex] as GridRecordRow;

            if (rec != null)
            {
                Record dr = rec.GetRecord() as Record;
                if (dr != null)
                {
                    if (new Notificacoes.Mensagem("Confirma a exclusão ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                        return;

                    Classes.RateioBeneficios.Associacao _excluirTipos = new Classes.RateioBeneficios.Associacao();

                    gridGroupingControl1.DataSource = new List<Classes.RateioBeneficios.Associacao>();

                    int custo= 0;
                    int conta = 0;
                    int setor = 0;
                    try
                    {
                        custo = (int)dr["CodigoCusto"];
                        conta = (int)dr["CodConta"];
                        setor = (int)dr["CodigoSetor"];
                    }
                    catch
                    {
                        int posIniId = 0;
                        int posFimId = 0;

                        try
                        {
                            posIniId = dr.Info.IndexOf("CodigoCusto =") + 13;
                            posFimId = dr.Info.IndexOf(", Nome");
                            custo = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());

                            posIniId = dr.Info.IndexOf("CodConta =") + 10;
                            posFimId = dr.Info.IndexOf(", NomeConta");
                            conta = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());

                            posIniId = dr.Info.IndexOf("CodigoSetor =") + 13;
                            posFimId = dr.Info.IndexOf(", NomeSetor");
                            setor = Convert.ToInt32(dr.Info.Substring(posIniId, posFimId - posIniId).Trim());
                        }
                        catch { }
                    }

                    foreach (var item in _listaAssociacoes.Where(w => w.CodigoCusto == custo && 
                                                                      w.CodConta == conta &&
                                                                      w.CodigoSetor == setor))
                    {
                        _excluirTipos = item;

                        if (item.Id != 0)
                        {
                            if (!new RateioBeneficioBO().ExcluiAssociacoes(item.Id))
                            {
                                new Notificacoes.Mensagem("Problemas durante a exclusão." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                                return;
                            }
                        }
                        break;
                    }

                    _listaAssociacoes.Remove(_excluirTipos);

                    Log _log = new Log();
                    _log.IdUsuario = Publicas._usuario.Id;
                    _log.Descricao = "Excluiu a associacao do custo " + _excluirTipos.CodigoCusto + 
                        " conta " + _excluirTipos.CodConta +
                        " conta destino " + _excluirTipos.CodContaDestino +
                        " setor " + _excluirTipos.CodigoSetor +
                        " da empresa " + empresaComboBoxAdv.Text;
                    _log.Tela = "Contabilidade - Cadastros - Associacoes";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                gridGroupingControl1.DataSource = _listaAssociacoes;
                gravarButton.Enabled = _listaAssociacoes.Count() != 0;
            }
        }

        private void AssociaCentroCustoASetor_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }

        private void CustoTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaCustoButton.Enabled = string.IsNullOrEmpty(CustoTextBox.Text.Trim());
        }

        private void ContaTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaContaButton.Enabled = string.IsNullOrEmpty(ContaTextBox.Text.Trim());
        }

        private void SetorTextBox_TextChanged(object sender, EventArgs e)
        {
            PesquisaSetorButton.Enabled = string.IsNullOrEmpty(SetorTextBox.Text.Trim());
        }

        private void CopiarButton_Click(object sender, EventArgs e)
        {
            // chamar tela de copiar
            Contabilidade.CopiaAssociacaoCentroCusto _tela = new CopiaAssociacaoCentroCusto();
            _tela._empresa = this._empresa;
            _tela._listaAssociacoesOrigem = this._listaAssociacoes;
            _tela.ShowDialog();
        }
    }
}
