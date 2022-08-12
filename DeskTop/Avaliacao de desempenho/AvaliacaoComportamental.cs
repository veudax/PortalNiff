using Classes;
using DynamicFilter;
using Negocio;
using Suportte.Notificacoes;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
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
    public partial class AvaliacaoComportamental : Form
    {
        public AvaliacaoComportamental()
        {
            InitializeComponent();

            dataLimiteDateTimePicker.Value = DateTime.Now;
            dataLimiteDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataLimiteDateTimePicker.BackColor = usuarioTextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                {
                    gridGroupingControl.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    gridGroupingControl.ColorStyles = ColorStyles.Office2010Black;
                    gridGroupingControl.GridVisualStyles = GridVisualStyles.Office2016Black;
                    gridGroupingControl.BackColor = Publicas._panelTitulo;
                    dataLimiteDateTimePicker.Style = VisualStyle.Office2016Black;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Colaboradores _colaboradores;
        Classes.Colaboradores _colaboradoresSuperior;
        Classes.Prazos _prazo;
        Classes.AutoAvaliacao _autoAvaliacao;
        Classes.FuncionariosGlobus _funcionariosGlobus;
        Classes.Cargos _cargos;
        Classes.Pontuacao _pontuacao;

        List<Classes.Empresa> _listaEmpresas;
        List<Classes.CompetenciasDoCargo> _listaCompetencias;
        List<Classes.ItensDaAutoAvaliacao> _listaItensAvaliacao;
        List<Classes.ItensDaAutoAvaliacao> _listaItens;
        List<Classes.EmpresaQueOColaboradorEhAvaliado> _empresaDoColaborador;
        List<Classes.Empresa> _listaEmpresasAutorizadas;

        GridCurrentCell _colunaCorrente;
        
        int _rowIndex = 0;

        int[] posicaoCompetencias;
        int posicao;
        string tipo;
        decimal regraBase;

        public Publicas.TipoPrazos tipoAvaliacao;

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

        private void AutoAvaliacao_Shown(object sender, EventArgs e)
        {
            GridMetroColors metroColor = new GridMetroColors();

            gridGroupingControl.SortIconPlacement = SortIconPlacement.Left;
            gridGroupingControl.TopLevelGroupOptions.ShowFilterBar = false;
            gridGroupingControl.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            gridGroupingControl.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;
            gridGroupingControl.TableControl.CellToolTip.Active = true;

            for (int i = 0; i < gridGroupingControl.TableDescriptor.Columns.Count; i++)
            {
                if (i == 0)
                    gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = true;
                else
                    gridGroupingControl.TableDescriptor.Columns[i].ReadOnly = false;

                gridGroupingControl.TableDescriptor.Columns[i].AllowSort = true;
            }

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
                this.gridGroupingControl.SetMetroStyle(metroColor);
                this.gridGroupingControl.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.gridGroupingControl.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            this.gridGroupingControl.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;
            this.gridGroupingControl.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            this.gridGroupingControl.Table.DefaultRecordRowHeight = 50;

            _listaEmpresas = new EmpresaBO().Listar(false);
            _empresaDoColaborador = new ColaboradoresBO().Listar(Publicas._idColaborador);
            competenciaLabel.Text = "";
            textoCompetenciaLabel.Text = "";

            if (Publicas._usuario.PermiteAlterarBSC || Publicas._telaRadarChamadaPeloMenu == "RH" || Publicas._usuario.IdEmpresa == 1 || Publicas._usuario.IdEmpresa == 19)
                empresaComboBoxAdv.DataSource = _listaEmpresas.OrderBy(o => o.CodigoeNome).ToList();
            else
            {
                _listaEmpresasAutorizadas = new List<Empresa>();
                foreach (var item in _empresaDoColaborador.Where(w => w.Inicio != DateTime.MinValue.Date &&
                                                                      (w.Fim == DateTime.MinValue.Date || w.Fim <= DateTime.Now.Date)))
                {
                    _listaEmpresasAutorizadas.AddRange(_listaEmpresas.Where(w => w.IdEmpresa == item.IdEmpresa));
                }

                empresaComboBoxAdv.DataSource = _listaEmpresasAutorizadas.OrderBy(o => o.CodigoeNome).ToList();
            }
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

             tipo = (tipoAvaliacao == Publicas.TipoPrazos.AutoAvaliacao ? "AA" :
                          (tipoAvaliacao == Publicas.TipoPrazos.FeedbackGestor ? "FG" :
                          (tipoAvaliacao == Publicas.TipoPrazos.MetasNumericas ? "MN" :
                          (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoDoGestor ? "AG" :
                          (tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoRH ? "AR" :
                          (tipoAvaliacao == Publicas.TipoPrazos.FeedbackAvaliado ? "AR" : "PD"))))));

            _colaboradoresSuperior = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, Publicas._usuario.RegistroFuncionario, false);

            if (usuarioTextBox.Enabled)
            {
                gravarButton.Location = new Point(387, 14);
                limparButton.Location = new Point(524, 14);
                empresaComboBoxAdv.Focus();
            }
            else// Carrega quando auto avaliação
            {
                gravarButton.Location = new Point(456, 14);
                limparButton.Visible = false;

                usuarioTextBox.Text = Publicas._usuario.RegistroFuncionario;

                _colaboradores = new ColaboradoresBO().Consultar(_empresa.CodigoEmpresaGlobus, usuarioTextBox.Text, false);
                nomeTextBox.Text = _colaboradores.Nome;

                _cargos = new CargosBO().Consultar(_colaboradores.IdCargo);
                descricaoCargoTextBox.Text = _cargos.Descricao;

                referenciaMaskedEditBox.Focus();
            }
            empresaComboBoxAdv.Enabled = true;
        }

        private void PreparaGrid()
        {
            posicaoCompetencias = new int[_listaCompetencias.Count()];

            int j = 0;
            foreach (var item in _listaCompetencias.Where(w => w.Marcado).OrderBy(o => o.Id))
            {
                posicaoCompetencias[j] = item.Id;
                j++;
            }

            posicao = 0;
            foreach (var item in _listaCompetencias.Where(w => w.Marcado && w.Id == posicaoCompetencias[posicao]))
            {
                competenciaLabel.Text = item.Descricao;
                textoCompetenciaLabel.Text = item.TextoExplicativo;
            }
            comentarioTextBox.Text = string.Empty;

            #region calculo
            decimal _total = 0;

            foreach (var comp in _listaCompetencias.Where(w => w.Marcado))
            {
                try
                {
                    foreach (var item in _listaItensAvaliacao.Where(w => w.IdCompetencia == comp.Id).ToList())
                    {
                        item.TotalSubCompetencia = (item.Pontuacao / _listaItensAvaliacao.Where(w => w.IdCompetencia == item.IdCompetencia).Count());
                    }
                }
                catch { }

                comp.Total = _listaItensAvaliacao.Where(w => w.IdCompetencia == comp.Id)
                                              .Sum(s => s.TotalSubCompetencia);
                if (_pontuacao.Supera != 0)
                    comp.TotalBase100 = Math.Round(Math.Round(((decimal)_pontuacao.Base100 / (decimal)_pontuacao.Supera) / _listaCompetencias.Where(w => w.Marcado).Count(), 4) * comp.Total,2);
            }
            #endregion

            foreach (var item in _listaItensAvaliacao.Where(w => w.IdCompetencia == posicaoCompetencias[posicao]).ToList())
            {
                if (string.IsNullOrEmpty(comentarioTextBox.Text))
                    comentarioTextBox.Text = item.Comentario;
            }

            _total = _listaItensAvaliacao.Where(w => w.IdCompetencia == posicaoCompetencias[posicao])
                                                 .Sum(s => s.TotalSubCompetencia);

            totalLabel.Visible = (_autoAvaliacao.DataFim != DateTime.MinValue || tipoAvaliacao != Publicas.TipoPrazos.AutoAvaliacao) && _autoAvaliacao.Existe;
            resultadoAvaliacaoLabel.Visible = (_autoAvaliacao.DataFim != DateTime.MinValue || tipoAvaliacao != Publicas.TipoPrazos.AutoAvaliacao) && _autoAvaliacao.Existe;

            totalLabel.Text = "";

            _total = _listaCompetencias.Sum(s => s.TotalBase100);

            //if (_total > _pontuacao.Base100)
            //resultadoAvaliacaoLabel.Text = "Resultado da Avaliação " + (_total > _pontuacao.Base100 ? _pontuacao.Base100.ToString() :
            //                                                               (_total < _pontuacao.Piso ? _pontuacao.Piso.ToString() : Math.Round(_total, 2).ToString()));

            resultadoAvaliacaoLabel.Text = "Resultado da Avaliação " + (_total > _pontuacao.Base100 ? _pontuacao.Base100.ToString() : Math.Round(_total, 2).ToString());

            dataLimiteDateTimePicker.Value = _prazo.Fim;
            
            _listaItens = _listaItensAvaliacao.Where(w => w.IdCompetencia == posicaoCompetencias[posicao]).ToList();
            gridGroupingControl.DataSource = _listaItens;
            posicaoTextBox.Text = "1/" + _listaCompetencias.Where(w => w.Marcado).Count().ToString();
            anteriorButton.Enabled = false;

            gridGroupingControl.Refresh();
            proximoButton.Enabled = posicao < _listaCompetencias.Where(w => w.Marcado).Count() - 1;

            gravarButton.Enabled = _autoAvaliacao.DataFim == DateTime.MinValue && (_prazo.Fim.Date >= DateTime.Now.Date);
            gridGroupingControl.Enabled = gravarButton.Enabled;
            comentarioTextBox.Enabled = gravarButton.Enabled;

            if (_autoAvaliacao.DataFim != DateTime.MinValue)
            {
                MensagemLabel.Text = Publicas.GetDescription(tipoAvaliacao, "") + " finalizada em " + _autoAvaliacao.DataFim.ToShortDateString();
                textBox1.Focus();
            }            
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void gridGroupingControl_TableControlMouseDown(object sender, GridTableControlMouseEventArgs e)
        {
            _colunaCorrente = gridGroupingControl.TableControl.CurrentCell;
        }        

        private void gridGroupingControl_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            try
            {
                _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = this.gridGroupingControl.Table.DisplayElements[_rowIndex] as GridRecordRow;

                string nomeColuna = gridGroupingControl.TableDescriptor.Columns[_colunaCorrente.ColIndex - 1].MappingName;
                bool marcado = false;

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null) 
                    {
                        marcado = (bool)dr[nomeColuna];

                        foreach (var item in _listaItens.Where(w => w.IdSubCompetencia == (int)dr["IdSubCompetencia"]))
                        {
                            if (!marcado)
                            {
                                item.Supera = false;
                                item.NaoAtende = false;
                                item.AtendeParcialmente = false;
                                item.AtendePlenamente = false;
                                item.Pontuacao = 0;
                            }
                            else
                            {
                                if (nomeColuna == "NaoAtende")
                                {
                                    item.NaoAtende = marcado;
                                    item.AtendeParcialmente = false;
                                    item.AtendePlenamente = false;
                                    item.Supera = false;
                                    item.Pontuacao = _pontuacao.NaoAtende;
                                }
                                if (nomeColuna == "AtendeParcialmente")
                                {
                                    item.AtendeParcialmente = marcado;
                                    item.NaoAtende = false;
                                    item.AtendePlenamente = false;
                                    item.Supera = false;
                                    item.Pontuacao = _pontuacao.AtendeParcialmente;
                                }
                                if (nomeColuna == "AtendePlenamente")
                                {
                                    item.AtendePlenamente = marcado;
                                    item.NaoAtende = false;
                                    item.AtendeParcialmente = false;
                                    item.Supera = false;
                                    item.Pontuacao = _pontuacao.AtendePlenamente;
                                }
                                if (nomeColuna == "Supera")
                                {
                                    item.Supera = marcado;
                                    item.NaoAtende = false;
                                    item.AtendeParcialmente = false;
                                    item.AtendePlenamente = false;
                                    item.Pontuacao = _pontuacao.Supera;
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            gridGroupingControl.DataSource = new List<ItensDaAutoAvaliacao>();

            gridGroupingControl.DataSource = _listaItens;
            gridGroupingControl.Refresh();
        }

        private void proximoButton_Click(object sender, EventArgs e)
        {
            foreach (var item in _listaItens)
            {
                foreach (var comp in _listaItensAvaliacao.Where(w => w.IdSubCompetencia == item.IdSubCompetencia))
                {
                    comp.NaoAtende = item.NaoAtende;
                    comp.Supera = item.Supera;
                    comp.AtendeParcialmente = item.AtendeParcialmente;
                    comp.AtendePlenamente = item.AtendePlenamente;

                    comp.Avaliacao = (comp.NaoAtende ? 1 :
                        (comp.AtendeParcialmente ? 2 :
                        (comp.AtendePlenamente ? 3 :
                        (comp.Supera ? 4 : 0))));

                    comp.Comentario = comentarioTextBox.Text;
                    comp.Pontuacao = (comp.NaoAtende ? _pontuacao.NaoAtende :
                        (comp.AtendeParcialmente ? _pontuacao.AtendeParcialmente :
                        (comp.AtendePlenamente ? _pontuacao.AtendePlenamente :
                        (comp.Supera ? _pontuacao.Supera : 0))));
                    comp.TotalSubCompetencia = (comp.Pontuacao / _listaItensAvaliacao.Where(w => w.IdCompetencia == item.IdCompetencia).Count());
                }
            }

            try
            {
                decimal _total = _listaItensAvaliacao.Where(w => w.IdCompetencia == posicaoCompetencias[posicao])
                                                 .Sum(s => s.TotalSubCompetencia);

                foreach (var comp in _listaCompetencias.Where(w => w.Id == posicaoCompetencias[posicao]))
                {
                    comp.Total = _total;
                }

                totalLabel.Text = Math.Round(_total, 2).ToString();
                posicao = posicao + 1;

                _listaItens = _listaItensAvaliacao.Where(w => w.IdCompetencia == posicaoCompetencias[posicao]).ToList();

                if (_listaItens.Count() == 0)
                {
                    proximoButton_Click(sender, e);
                    return;
                }

                foreach (var item in _listaCompetencias.Where(w => w.Marcado && w.Id == posicaoCompetencias[posicao]))
                {
                    competenciaLabel.Text = item.Descricao;
                    textoCompetenciaLabel.Text = item.TextoExplicativo;
                }

                comentarioTextBox.Text = string.Empty;

                try
                {
                    foreach (var item in _listaItensAvaliacao.Where(w => w.IdCompetencia == posicaoCompetencias[posicao]).ToList())
                    {
                        comentarioTextBox.Text = item.Comentario;
                        item.TotalSubCompetencia = (item.Pontuacao / _listaItensAvaliacao.Where(w => w.IdCompetencia == item.IdCompetencia).Count());
                    }
                }
                catch { }

                _total = _listaItensAvaliacao.Where(w => w.IdCompetencia == posicaoCompetencias[posicao])
                                                     .Sum(s => s.TotalSubCompetencia);

                foreach (var comp in _listaCompetencias.Where(w => w.Id == posicaoCompetencias[posicao]))
                {
                    comp.Total = _total;
                }

                gridGroupingControl.DataSource = _listaItens;
                posicaoTextBox.Text = (posicao + 1).ToString() + "/" + _listaCompetencias.Where(w => w.Marcado).Count().ToString();
                gridGroupingControl.Refresh();
                anteriorButton.Enabled = posicao > 0;
                proximoButton.Enabled = posicao < _listaCompetencias.Where(w => w.Marcado).Count() - 1;

                totalLabel.Text = Math.Round(_total, 2).ToString();
            }
            catch { }

            if (proximoButton.Enabled)
                proximoButton.Focus();
            else
                anteriorButton.Focus();
        }

        private void anteriorButton_Click(object sender, EventArgs e)
        {
            foreach (var item in _listaItens)
            {
                foreach (var comp in _listaItensAvaliacao.Where(w => w.IdSubCompetencia == item.IdSubCompetencia))
                {
                    comp.NaoAtende = item.NaoAtende;
                    comp.Supera = item.Supera;
                    comp.AtendeParcialmente = item.AtendeParcialmente;
                    comp.AtendePlenamente = item.AtendePlenamente;

                    comp.Avaliacao = (comp.NaoAtende ? 1 :
                        (comp.AtendeParcialmente ? 2 :
                        (comp.AtendePlenamente ? 3 :
                        (comp.Supera ? 4 : 0))));

                    comp.Comentario = comentarioTextBox.Text;
                    comp.Pontuacao = (comp.NaoAtende ? _pontuacao.NaoAtende :
                        (comp.AtendeParcialmente ? _pontuacao.AtendeParcialmente :
                        (comp.AtendePlenamente ? _pontuacao.AtendePlenamente :
                        (comp.Supera ? _pontuacao.Supera : 0))));
                    comp.TotalSubCompetencia = (comp.Pontuacao / _listaItensAvaliacao.Where(w => w.IdCompetencia == item.IdCompetencia).Count());
                }
            }

            try
            {

                decimal _total = _listaItensAvaliacao.Where(w => w.IdCompetencia == posicaoCompetencias[posicao])
                                                 .Sum(s => s.TotalSubCompetencia);


                foreach (var comp in _listaCompetencias.Where(w => w.Id == posicaoCompetencias[posicao]))
                {
                    comp.Total = _total;
                }

                posicao = posicao - 1;

                _listaItens = _listaItensAvaliacao.Where(w => w.IdCompetencia == posicaoCompetencias[posicao]).ToList();

                if (_listaItens.Count() == 0)
                {
                    anteriorButton_Click(sender, e);
                    return;
                }

                foreach (var item in _listaCompetencias.Where(w => w.Marcado && w.Id == posicaoCompetencias[posicao]))
                {
                    competenciaLabel.Text = item.Descricao;
                    textoCompetenciaLabel.Text = item.TextoExplicativo;
                }

                comentarioTextBox.Text = string.Empty;
                try
                {
                    foreach (var item in _listaItensAvaliacao.Where(w => w.IdCompetencia == posicaoCompetencias[posicao]).ToList())
                    {
                        comentarioTextBox.Text = item.Comentario;
                        item.TotalSubCompetencia = (item.Pontuacao / _listaItensAvaliacao.Where(w => w.IdCompetencia == item.IdCompetencia).Count());
                    }
                }
                catch { }

                _total = _listaItensAvaliacao.Where(w => w.IdCompetencia == posicaoCompetencias[posicao])
                                                     .Sum(s => s.TotalSubCompetencia);


                foreach (var comp in _listaCompetencias.Where(w => w.Id == posicaoCompetencias[posicao]))
                {
                    comp.Total = _total;
                }

                gridGroupingControl.DataSource = _listaItens;
                posicaoTextBox.Text = (posicao + 1).ToString() + "/" + _listaCompetencias.Where(w => w.Marcado).Count().ToString();
                gridGroupingControl.Refresh();
                anteriorButton.Enabled = posicao > 0;
                proximoButton.Enabled = posicao < _listaCompetencias.Where(w => w.Marcado).Count() - 1;
                totalLabel.Text = Math.Round(_total, 2).ToString();
            }
            catch { }

            if (anteriorButton.Enabled)
                anteriorButton.Focus();
            else
                proximoButton.Focus();

        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            foreach (var item in _listaItens)
            {
                foreach (var comp in _listaItensAvaliacao.Where(w => w.IdSubCompetencia == item.IdSubCompetencia))
                {
                    comp.NaoAtende = item.NaoAtende;
                    comp.Supera = item.Supera;
                    comp.AtendeParcialmente = item.AtendeParcialmente;
                    comp.AtendePlenamente = item.AtendePlenamente;

                    comp.Avaliacao = (comp.NaoAtende ? 1 :
                        (comp.AtendeParcialmente ? 2 :
                        (comp.AtendePlenamente ? 3 :
                        (comp.Supera ? 4 : 0))));

                    comp.Comentario = comentarioTextBox.Text;
                    comp.Pontuacao = (comp.NaoAtende ? _pontuacao.NaoAtende :
                        (comp.AtendeParcialmente ? _pontuacao.AtendeParcialmente :
                        (comp.AtendePlenamente ? _pontuacao.AtendePlenamente :
                        (comp.Supera ? _pontuacao.Supera : 0))));
                    comp.TotalSubCompetencia = (comp.Pontuacao / _listaItensAvaliacao.Where(w => w.IdCompetencia == item.IdCompetencia).Count());
                }
            }

            #region calculo
            foreach (var comp in _listaCompetencias.Where(w => w.Marcado))
            {
                try
                {
                    foreach (var item in _listaItensAvaliacao.Where(w => w.IdCompetencia == comp.Id).ToList())
                    {
                        item.TotalSubCompetencia = (item.Pontuacao / _listaItensAvaliacao.Where(w => w.IdCompetencia == item.IdCompetencia).Count());
                    }
                }
                catch { }

                comp.Total = _listaItensAvaliacao.Where(w => w.IdCompetencia == comp.Id)
                                              .Sum(s => s.TotalSubCompetencia);

                if (_pontuacao.Supera != 0)
                    comp.TotalBase100 = Math.Round(Math.Round(((decimal)_pontuacao.Base100 / (decimal)_pontuacao.Supera) / _listaCompetencias.Where(w => w.Marcado).Count(), 4) * comp.Total, 2);
            }
            #endregion

            if (_autoAvaliacao == null)
                _autoAvaliacao = new Classes.AutoAvaliacao();

            _autoAvaliacao.IdColaborador = _colaboradores.Id;
            _autoAvaliacao.MesReferencia = Convert.ToInt32(_prazo.Referencia);
            _autoAvaliacao.DataInicio = DateTime.Now;
            _autoAvaliacao.Tipo = tipoAvaliacao;
            _autoAvaliacao.IdEmpresa = _empresa.IdEmpresa;

            int totalMarcados = 0;
            foreach (var item in _listaItensAvaliacao)
            {
                if (item.AtendeParcialmente || item.AtendePlenamente || item.NaoAtende || item.Supera)
                    totalMarcados++;
            }

            if (totalMarcados == _listaItensAvaliacao.Count())
            {
                _autoAvaliacao.DataFim = DateTime.Now;
                _autoAvaliacao.TotalAvaliacao = _listaCompetencias.Sum(s => s.TotalBase100);
            }
            else
            {
                new Notificacoes.Mensagem("Existem itens não avaliados." + Environment.NewLine +
                    "Você poderá continuar a avaliação futuramente." + Environment.NewLine +
                    "Não se esqueça do prazo final " + _prazo.Fim.ToShortDateString()
                    , Publicas.TipoMensagem.Alerta).ShowDialog();
            }

            if (!new AutoAvaliacaoBO().Gravar(_autoAvaliacao, _listaItensAvaliacao))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            if (!empresaComboBoxAdv.Enabled)
                Close();
            else
                limparButton_Click(sender, e);
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (usuarioTextBox.Enabled)
                    usuarioTextBox.Focus();
                else
                    referenciaMaskedEditBox.Focus();
            }
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

        private void usuarioTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void empresaComboBoxAdv_Enter(object sender, EventArgs e)
        {
            ((ComboBoxAdv)sender).FlatBorderColor = Publicas._bordaEntrada;
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

        private void usuarioTextBox_Validating(object sender, CancelEventArgs e)
        {
            usuarioTextBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaUsuarioButton.Enabled = false;
                usuarioTextBox.Text = string.Empty;
                nomeTextBox.Text = string.Empty;
                descricaoCargoTextBox.Text = string.Empty;
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._idSuperior = (!tituloLabel.Text.Contains("Gestor") ? 0 : _colaboradoresSuperior.Id);
                Publicas._participaAvaliacao = true;

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

            //if (!_funcionariosGlobus.Existe)
            //{
            //    new Notificacoes.Mensagem("Colaborador não cadastrado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
            //    usuarioTextBox.Focus();
            //    return;
            //}

            //if (_funcionariosGlobus.DataDesligamento != DateTime.MinValue && !_funcionariosGlobus.Ativo)
            //{
            //    new Notificacoes.Mensagem("Colaborador desligado na Folha de pagamento do Globus.", Publicas.TipoMensagem.Alerta).ShowDialog();
            //    usuarioTextBox.Focus();
            //    return;
            //}

            if (_colaboradores.IdSupervisor != _colaboradoresSuperior.Id && tipoAvaliacao == Publicas.TipoPrazos.AvaliacaoDoGestor &&
                (Publicas._usuario.UsuarioAcesso != "JFERNANDES" && Publicas._usuario.UsuarioAcesso != "MDMUNOZ" && Publicas._usuario.UsuarioAcesso != "TIFELICIO"))
            {
                new Notificacoes.Mensagem("Colaborador não cadastrado na sua equipe.", Publicas.TipoMensagem.Alerta).ShowDialog();
                usuarioTextBox.Focus();
                return;
            }

            nomeTextBox.Text = _funcionariosGlobus.Nome;

            _cargos = new CargosBO().Consultar(_colaboradores.IdCargo);
            descricaoCargoTextBox.Text = _cargos.Descricao;
                       
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            _listaCompetencias.Clear();
            _listaItensAvaliacao.Clear();

            competenciaLabel.Text = "";
            textoCompetenciaLabel.Text = "";
            dataLimiteDateTimePicker.Value = DateTime.Now;
            referenciaMaskedEditBox.Text = string.Empty;
            gridGroupingControl.DataSource = new List<AutoAvaliacao>();
            gridGroupingControl.Enabled = true;
            descricaoCargoTextBox.Text = string.Empty;
            totalLabel.Text = string.Empty;
            resultadoAvaliacaoLabel.Text = string.Empty;
            totalLabel.Visible = false;
            resultadoAvaliacaoLabel.Visible = false;
            nomeTextBox.Text = string.Empty;
            posicaoTextBox.Text = string.Empty;
            usuarioTextBox.Text = string.Empty;
            descricaoCargoTextBox.Text = string.Empty;
            comentarioTextBox.Text = string.Empty;
            MensagemLabel.Text = string.Empty;
            proximoButton.Enabled = false;
            anteriorButton.Enabled = false;
            pesquisaReferenciaButton.Enabled = false;
            pesquisaUsuarioButton.Enabled = false;
            usuarioTextBox.Focus();
        }

        private void dataDateTimePicker_Validating(object sender, CancelEventArgs e)
        {

        }

        private void dataDateTimePicker_Enter(object sender, EventArgs e)
        {
            ((DateTimePickerAdv)sender).BorderColor = Publicas._bordaEntrada;
        }
        
        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                nomeTextBox.Focus();
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

        private void referenciaMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gridGroupingControl.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                if (usuarioTextBox.Enabled)
                    usuarioTextBox.Focus();
                else
                    empresaComboBoxAdv.Focus();
            }
        }

        private void referenciaMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }

        private void referenciaMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {

            ((MaskedEditBox)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                pesquisaReferenciaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }
            
            if (_listaCompetencias == null || _listaCompetencias.Count() == 0)
            {
                if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
                {
                    Publicas._idSuperior = _colaboradores.Id;

                    Pesquisas.Feedback _pesquisa = new Pesquisas.Feedback("AA");
                    _pesquisa.tituloLabel.Text = "Pesquisa de " + tituloLabel.Text;
                    _pesquisa.ShowDialog();

                    referenciaMaskedEditBox.Text = Publicas._idRetornoPesquisa.ToString("000000");

                    if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()) || referenciaMaskedEditBox.ClipText == "000000")
                    {
                        referenciaMaskedEditBox.Text = string.Empty;
                        referenciaMaskedEditBox.Focus();
                        return;
                    }
                }

                try
                {
                    _autoAvaliacao = new AutoAvaliacaoBO().Consultar(_colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), tipo, _empresa.IdEmpresa);
                    _pontuacao = new PontuacaoBO().ConsultarMaiorReferencia(Convert.ToInt32(referenciaMaskedEditBox.ClipText.Trim()));
                    _prazo = new PrazosBO().Consultar(DateTime.Now.Date, tipo, referenciaMaskedEditBox.ClipText.Trim());
                }
                catch {
                    new Notificacoes.Mensagem("Mês/Ano inválido.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                if (!_prazo.Existe)
                {
                    new Notificacoes.Mensagem(Publicas.GetDescription(tipoAvaliacao, "") + " não cadastrado para essa referência.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    referenciaMaskedEditBox.Focus();
                    return;
                }

                if (_prazo.Fim.Date < DateTime.Now.Date)
                {
                    if (new Notificacoes.Mensagem("Prazo para " + Publicas.GetDescription(tipoAvaliacao, "") + " finalizado em " + _prazo.Fim.Date.ToShortDateString() + "." +
                        Environment.NewLine + "Deseja consultar?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    {
                        referenciaMaskedEditBox.Focus();
                        return;
                    }
                }

                _listaCompetencias = new CompetenciasDoCargoBO().Listar(_colaboradores.IdCargo, "C", _autoAvaliacao.Existe, _colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), tipoAvaliacao);
                _listaItensAvaliacao = new AutoAvaliacaoBO().Listar(_colaboradores.IdCargo, _colaboradores.Id, referenciaMaskedEditBox.ClipText.Trim(), tipoAvaliacao);

                totalLabel.Visible = false;
                totalLabel.Text = "0";

                regraBase = (_listaCompetencias.Where(w => w.Marcado).Count() * _pontuacao.Supera);

                PreparaGrid();
            }
        }

        private void pesquisaReferenciaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()))
            {
                Publicas._idSuperior = _colaboradores.Id;

                Pesquisas.Feedback _pesquisa = new Pesquisas.Feedback("AA");
                _pesquisa.tituloLabel.Text = "Pesquisa de " + tituloLabel.Text;
                _pesquisa.ShowDialog();
                
                referenciaMaskedEditBox.Text = Publicas._idRetornoPesquisa.ToString("000000");

                if (string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim()) || referenciaMaskedEditBox.ClipText == "000000")
                {
                    referenciaMaskedEditBox.Text = string.Empty;
                    referenciaMaskedEditBox.Focus();
                    return;
                }
            }
        }

        private void pesquisaUsuarioButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuarioTextBox.Text.Trim()))
            {
                Publicas._idEmpresa = _empresa.IdEmpresa;
                Publicas._codigoEmpresaGlobus = _empresa.CodigoEmpresaGlobus;
                Publicas._idSuperior = (!tituloLabel.Text.Contains("Gestor") ? 0 : _colaboradoresSuperior.Id);
                Publicas._participaAvaliacao = true;

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

        private void usuarioTextBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaUsuarioButton.Enabled = string.IsNullOrEmpty(usuarioTextBox.Text.Trim());
        }

        private void referenciaMaskedEditBox_TextChanged(object sender, EventArgs e)
        {
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(referenciaMaskedEditBox.ClipText.Trim());
        }

        private void AvaliacaoComportamental_Load(object sender, EventArgs e)
        {
            LocalizationProvider.Provider = new Localizer();

            Localizer loc = new Localizer();
            loc.getstring("True");
            LocalizationProvider.Provider = loc;
        }
    }
}
