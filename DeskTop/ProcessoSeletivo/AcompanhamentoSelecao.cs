using Classes;
using Negocio;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.ProcessoSeletivo
{
    public partial class AcompanhamentoSelecao : Form
    {
        public AcompanhamentoSelecao()
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
                    CandidatosGrid.GridOfficeScrollBars = OfficeScrollBars.Office2010;
                    CandidatosGrid.ColorStyles = ColorStyles.Office2010Black;
                    CandidatosGrid.GridVisualStyles = GridVisualStyles.Office2016Black;
                    CandidatosGrid.BackColor = Publicas._panelTitulo;
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.Empresa _empresa;
        Classes.Vagas _vagas;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Vagas> _listaVagas;
        List<Curriculos> _lista;
        List<Curriculos> _listaOriginal;
        GridCurrentCell _colunaCorrente;
        string nomeCampo = "";
        int idCandidato = 0;

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

        private void AcompanhamentoSelecao_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Left, 60);

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

            GridDynamicFilter filter = new GridDynamicFilter();
            filter.ApplyFilterOnlyOnCellLostFocus = true;
            filter.WireGrid(this.CandidatosGrid);

            CandidatosGrid.SortIconPlacement = SortIconPlacement.Left;
            CandidatosGrid.TopLevelGroupOptions.ShowFilterBar = true;
            CandidatosGrid.TableOptions.ListBoxSelectionMode = SelectionMode.One;
            CandidatosGrid.TopLevelGroupOptions.ShowAddNewRecordBeforeDetails = false;

            for (int i = 0; i < CandidatosGrid.TableDescriptor.Columns.Count; i++)
            {
                CandidatosGrid.TableDescriptor.Columns[i].AllowFilter = true;
                CandidatosGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowCustomFilter = false;
                CandidatosGrid.TableDescriptor.Columns[i].FilterRowOptions.AllowEmptyFilter = false;
                CandidatosGrid.TableDescriptor.Columns[i].FilterRowOptions.FilterMode = Syncfusion.Windows.Forms.Grid.Grouping.FilterMode.Value;
            }
            
            if (!Publicas._TemaBlack)
            {
                this.CandidatosGrid.SetMetroStyle(metroColor);
                this.CandidatosGrid.TableOptions.SelectionBackColor = Publicas._bordaEntrada;
                this.CandidatosGrid.TableOptions.SelectionTextColor = Color.WhiteSmoke;
            }

            CandidatosGrid.TableOptions.ListBoxSelectionCurrentCellOptions = GridListBoxSelectionCurrentCellOptions.None;

            CandidatosGrid.TableOptions.ListBoxSelectionColorOptions = GridListBoxSelectionColorOptions.ApplySelectionColor;
            CandidatosGrid.Table.DefaultRecordRowHeight = 38;
            CandidatosGrid.TableDescriptor.TableOptions.CaptionRowHeight = 38;
            CandidatosGrid.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 38;

            #region coloca os botões centralizados
            int espacoEntreBotoes = limparButton.Left - (gravarButton.Left + gravarButton.Width);

            gravarButton.Left = (botoesPanel.Width - (espacoEntreBotoes + gravarButton.Width + limparButton.Width)) / 2;
            limparButton.Left = gravarButton.Left + limparButton.Width + espacoEntreBotoes;
            #endregion
        }

        private void empresaComboBoxAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                VagasComboBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                ((Control)sender).Focus();
            }
        }

        private void VagasComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                PesquisarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                empresaComboBoxAdv.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                CandidatosGrid.Focus();
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

            _listaVagas = new CurriculosBO().ListarVagas(true, _empresa.IdEmpresa);

            VagasComboBox.DataSource = _listaVagas.OrderBy(o => o.Descricao).ToList();
            VagasComboBox.DisplayMember = "Descricao";
            VagasComboBox.Focus();
        }

        private void VagasComboBox_Validating(object sender, CancelEventArgs e)
        {
            VagasComboBox.FlatBorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            foreach (var item in _listaVagas.Where(w => w.Descricao == VagasComboBox.Text))
            {
                _vagas = item;
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BuscarPrevistoButton_Click(object sender, EventArgs e)
        {
            _lista = new CurriculosBO().ConsultarCandidatosDaVaga(_vagas.Id);
            _listaOriginal = new CurriculosBO().ConsultarCandidatosDaVaga(_vagas.Id);

            CandidatosGrid.DataSource = _lista;

            for (int i = 0; i < CandidatosGrid.TableDescriptor.Relations.Count; i++)
            {
                CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.TableOptions.IndentWidth = 18;
                CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.TableOptions.RowHeaderWidth = 40;

                CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.AllowEdit = false;
                CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.AllowNew = false;
                CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.AllowRemove = false;

                try
                {
                    CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.SortedColumns.Add("Data", ListSortDirection.Descending);
                }
                catch { }

                for (int j = 0; j < CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns.Count; j++)
                {
                    
                    CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].AllowFilter = false;
                    CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowCustomFilter = false;
                    CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].FilterRowOptions.AllowEmptyFilter = false;
                    
                    if ((CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName != "Entrevista" &&
                         CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName != "Data" &&
                         CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName != "DescricaoStatus" &&
                        (!CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName.StartsWith("Motivo"))))
                        CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.VisibleColumns.Remove(CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName);
                    else
                    {
                        CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalAlignment = GridVerticalAlignment.Middle;
                        CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Left;

                        if (CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName == "Data" ||
                            CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName == "Entrevista")
                        {
                            CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Width = 120;
                            CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.ColumnHeaderCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                            CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.HorizontalAlignment = GridHorizontalAlignment.Center;
                            CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.ShowButtons = GridShowButtons.Hide;
                        }
                        if (CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName == "DescricaoStatus" )
                            CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].HeaderText = "Status";
                        if (CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].MappingName == "Motivo")
                        {
                            CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Width = 650;
                            CandidatosGrid.TableDescriptor.Relations[i].ChildTableDescriptor.Columns[j].Appearance.AnyRecordFieldCell.VerticalScrollbar = true;
                        }
                    }
                }
            }

            gravarButton.Enabled = _lista.Count() > 0;
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            CandidatosGrid.DataSource = new List<Curriculos>();
            
            VagasComboBox.Focus();
        }

        private void CandidatosGrid_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            int colIndex = _colunaCorrente.ColIndex - 1;

            if (colIndex >= CandidatosGrid.TableDescriptor.Columns.Count)
                colIndex = CandidatosGrid.TableDescriptor.Columns.Count - 1;

            if (CandidatosGrid.TableDescriptor.Columns[colIndex].MappingName == "Column1")
            {
                if (!System.IO.Directory.Exists("C:\\Temp\\"))
                    System.IO.Directory.CreateDirectory("C:\\Temp\\");

                GridRecordRow rec = CandidatosGrid.Table.DisplayElements[e.TableControl.CurrentCell.RowIndex] as GridRecordRow;

                string _nomeArquivo = "";

                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        _nomeArquivo = (string)dr["NomeCandidato"] + ".pdf";

                        foreach (var item in _lista.Where(w => w.IdCandidato == (int)dr["IdCandidato"]))
                        {
                            if (item.CVArquivo == null)
                                _nomeArquivo = "";
                            else
                            {
                                using (FileStream fs = new FileStream
                                   ("C:\\Temp\\" + _nomeArquivo, FileMode.Create, FileAccess.Write))
                                {
                                    fs.Write(item.CVArquivo, 0, item.CVArquivo.Length);
                                }

                                _nomeArquivo = "C:\\Temp\\" + _nomeArquivo;
                            }
                        }
                    }
                }

                if (_nomeArquivo != "")
                    System.Diagnostics.Process.Start(_nomeArquivo);
                else
                {
                    new Notificacoes.Mensagem("Candidato sem currículo associado.", Publicas.TipoMensagem.Alerta).ShowDialog();
                }
            }
        }

        private void CandidatosGrid_TableControlCurrentCellChanged(object sender, GridTableControlEventArgs e)
        {
            try
            {
                int _rowIndex = e.TableControl.Table.CurrentRecord.GetRecord().GetRowIndex();

                GridRecordRow rec = CandidatosGrid.Table.DisplayElements[_rowIndex] as GridRecordRow;

                string nomeColuna = CandidatosGrid.TableDescriptor.Columns[_colunaCorrente.ColIndex - 2].MappingName;
                bool marcado = false;
                bool porSkype = false;
                bool enviarEmail = false;
                
                if (rec != null)
                {
                    Record dr = rec.GetRecord() as Record;
                    if (dr != null)
                    {
                        idCandidato = (int)dr["IdCandidato"];

                        if (nomeColuna != "Data1Entrevista" && nomeColuna != "Data2Entrevista")
                        {
                            try
                            {
                                marcado = (bool)dr[nomeColuna];
                            }
                            catch
                            {
                            }

                            foreach (var item in _lista.Where(w => w.IdCandidato == idCandidato))
                            {
                                idCandidato = item.IdCandidato;

                                if (!marcado)
                                {
                                    if (nomeColuna == "Contato")
                                        item.Contato = false;
                                    if (nomeColuna == "Aprovado")
                                        item.Aprovado = false;
                                    if (nomeColuna == "AprovadoGestor")
                                        item.AprovadoGestor = false;
                                    if (nomeColuna == "Reprovado")
                                        item.Reprovado = false;
                                    if (nomeColuna == "ReprovadoGestor")
                                        item.ReprovadoGestor = false;
                                    if (nomeColuna == "Cancelar")
                                        item.Cancelar = false;
                                    if (nomeColuna == "SemContato")
                                        item.SemContato = false;
                                    if (nomeColuna == "PorSkype")
                                        item.PorSkype = false;
                                    if (nomeColuna == "EnviarEmail")
                                        item.EnviarEmail = false;
                                }
                                else
                                {
                                    if (nomeColuna == "Contato")
                                        item.Contato = true;
                                    if (nomeColuna == "Aprovado")
                                        item.Aprovado = true;
                                    if (nomeColuna == "AprovadoGestor")
                                        item.AprovadoGestor = true;
                                    if (nomeColuna == "Reprovado")
                                        item.Reprovado = true;
                                    if (nomeColuna == "ReprovadoGestor")
                                        item.ReprovadoGestor = true;
                                    if (nomeColuna == "SemContato")
                                        item.SemContato = true;
                                    if (nomeColuna == "PorSkype")
                                    {
                                        item.PorSkype = true;
                                        porSkype = true;
                                    }
                                    if (nomeColuna == "EnviarEmail")
                                    {
                                        item.EnviarEmail = true;
                                        enviarEmail = true;
                                    }
                                    if (nomeColuna == "Cancelar")
                                    {
                                        // só deixa cancelar se uma das datas estiver preenchida
                                        if (item.DataPrimeiraEntrevista == DateTime.MinValue && item.DataSegundaEntrevista == DateTime.MinValue)
                                        {
                                            item.Cancelar = false;
                                            marcado = false;
                                        }
                                        else
                                            item.Cancelar = true;
                                    }
                                }
                            }

                            CandidatosGrid.DataSource = new List<Curriculos>();

                            CandidatosGrid.DataSource = _lista;
                            CandidatosGrid.Refresh();

                        }
                        
                    }

                    nomeCampo = nomeColuna;

                    if (marcado && !porSkype && !enviarEmail &&
                        new Notificacoes.Mensagem("Deseja informar o motivo ?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                    {
                        MotivoPanel.Left = (this.Width - MotivoPanel.Width) / 2;
                        MotivoPanel.Top = (this.Height - MotivoPanel.Height) / 2;
                        MotivoPanel.Visible = true;
                        MotivoTextBox.Focus();
                    }
                    if (porSkype)
                    {
                        SkypePanel.Left = (this.Width - SkypePanel.Width) / 2;
                        SkypePanel.Top = (this.Height - SkypePanel.Height) / 2;
                        SkypePanel.Visible = true;
                        SkypeTextBox.Focus();
                    }
                }
            }
            catch { }

        }

        private void CandidatosGrid_TableControlMouseDown(object sender, GridTableControlMouseEventArgs e)
        {
            _colunaCorrente = CandidatosGrid.TableControl.CurrentCell;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            List<Classes.HistoricoDoCandidato> _listaHistoricos = new List<Classes.HistoricoDoCandidato>();
            string[] _dadosEmail = new string[50];

            _dadosEmail[0] = _vagas.Descricao;

            _dadosEmail[7] = Publicas._usuario.Nome;
            _dadosEmail[8] = (_vagas.Confidencial ? "Lembrando que a vaga é confidencial." : "");
            _dadosEmail[9] = _vagas.InformacoesGerais + "</br>";

            foreach (var item in _lista)
            {
                _dadosEmail[5] = item.NomeCandidato.Split(' ')[0];

                _dadosEmail[4] = (DateTime.Now.TimeOfDay < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0).TimeOfDay ? "Bom dia" :
                    (DateTime.Now.TimeOfDay < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0).TimeOfDay ? "Boa Tarde" : "Boa Noite"));

                if (!item.PorSkype)
                {
                    _dadosEmail[3] = _empresa.Nome;
                    _dadosEmail[10] = "no seguinte endereço:";
                    _dadosEmail[6] = _vagas.EnderecoEntevista;
                }
                else
                {
                    _dadosEmail[6] = "Nossa entrevista será efetuada através do skype </br>" + item.Skype;
                    _dadosEmail[10] = ".";
                }

                if (item.Contato)
                {
                    foreach (var itemO in _listaOriginal.Where(w => w.IdCandidato == item.IdCandidato && !w.Contato))
                    {
                        Classes.HistoricoDoCandidato _histo = new Classes.HistoricoDoCandidato();

                        _histo.Status = "3";
                        _histo.IdVaga = item.IdVagas;
                        _histo.IdCandidato = item.IdCandidato;
                        _histo.Motivo = item.MotivoContato;
                        _listaHistoricos.Add(_histo);
                    }
                }
                if (item.SemContato)
                {
                    foreach (var itemO in _listaOriginal.Where(w => w.IdCandidato == item.IdCandidato && !w.SemContato))
                    {
                        Classes.HistoricoDoCandidato _histo = new Classes.HistoricoDoCandidato();

                        _histo.Status = "4";
                        _histo.IdVaga = item.IdVagas;
                        _histo.IdCandidato = item.IdCandidato;
                        _histo.Motivo = item.MotivoContato;
                        _listaHistoricos.Add(_histo);
                    }
                }

                if (item.AprovadoGestor)
                {
                    foreach (var itemO in _listaOriginal.Where(w => w.IdCandidato == item.IdCandidato && !w.AprovadoGestor))
                    {
                        Classes.HistoricoDoCandidato _histo = new Classes.HistoricoDoCandidato();

                        _histo.Status = "5";
                        _histo.IdVaga = item.IdVagas;
                        _histo.IdCandidato = item.IdCandidato;
                        _histo.Motivo = item.MotivoAprovadoGestor;
                        _listaHistoricos.Add(_histo);
                    }
                }

                if (item.ReprovadoGestor)
                {
                    foreach (var itemO in _listaOriginal.Where(w => w.IdCandidato == item.IdCandidato && !w.ReprovadoGestor))
                    {
                        Classes.HistoricoDoCandidato _histo = new Classes.HistoricoDoCandidato();

                        _histo.Status = "10";
                        _histo.IdVaga = item.IdVagas;
                        _histo.IdCandidato = item.IdCandidato;
                        _histo.Motivo = item.MotivoReprovado;
                        _listaHistoricos.Add(_histo);
                    }
                }

                if (item.Aprovado)
                {
                    foreach (var itemO in _listaOriginal.Where(w => w.IdCandidato == item.IdCandidato && !w.Aprovado))
                    {
                        Classes.HistoricoDoCandidato _histo = new Classes.HistoricoDoCandidato();

                        _histo.Status = "15";
                        _histo.IdVaga = item.IdVagas;
                        _histo.IdCandidato = item.IdCandidato;
                        _histo.Motivo = item.MotivoAprovado;
                        _listaHistoricos.Add(_histo);
                    }
                }

                if (item.Reprovado)
                {
                    foreach (var itemO in _listaOriginal.Where(w => w.IdCandidato == item.IdCandidato && !w.Reprovado))
                    {
                        Classes.HistoricoDoCandidato _histo = new Classes.HistoricoDoCandidato();

                        _histo.Status = "16";
                        _histo.IdVaga = item.IdVagas;
                        _histo.IdCandidato = item.IdCandidato;
                        _histo.Motivo = item.MotivoReprovado;
                        _listaHistoricos.Add(_histo);
                    }
                }

                if (item.Cancelar)
                {
                    Classes.HistoricoDoCandidato _histo = new Classes.HistoricoDoCandidato();

                    _histo.Status = (item.DataSegundaEntrevista == DateTime.MinValue ? "12" : "13");
                    _histo.IdVaga = item.IdVagas;
                    _histo.IdCandidato = item.IdCandidato;
                    _histo.Motivo = item.MotivoCancelar;
                    _listaHistoricos.Add(_histo);
                }

                if (item.Data1Entrevista != null && item.DataPrimeiraEntrevista == DateTime.MinValue)
                {
                    Classes.HistoricoDoCandidato _histo = new Classes.HistoricoDoCandidato();

                    _histo.Status = "6";
                    _histo.IdVaga = item.IdVagas;
                    _histo.IdCandidato = item.IdCandidato;
                    _histo.DataEntrevista = Convert.ToDateTime(item.Data1Entrevista);
                    _listaHistoricos.Add(_histo);
                    _dadosEmail[1] = _histo.DataEntrevista.ToShortDateString();
                    _dadosEmail[2] = _histo.DataEntrevista.ToShortTimeString();

                }
                else
                {
                    if (item.Data1Entrevista != null && item.DataPrimeiraEntrevista != DateTime.MinValue &&
                        Convert.ToDateTime(item.Data1Entrevista) != item.DataPrimeiraEntrevista)
                    {
                        Classes.HistoricoDoCandidato _histo = new Classes.HistoricoDoCandidato();

                        _histo.Status = "8";
                        _histo.IdVaga = item.IdVagas;
                        _histo.IdCandidato = item.IdCandidato;
                        _histo.DataEntrevista = Convert.ToDateTime(item.Data1Entrevista);
                        _listaHistoricos.Add(_histo);
                        _dadosEmail[1] = _histo.DataEntrevista.ToShortDateString();
                        _dadosEmail[2] = _histo.DataEntrevista.ToShortTimeString();

                    }
                }

                if (item.Data2Entrevista != null && item.DataSegundaEntrevista == DateTime.MinValue)
                {
                    Classes.HistoricoDoCandidato _histo = new Classes.HistoricoDoCandidato();

                    _histo.Status = "7";
                    _histo.IdVaga = item.IdVagas;
                    _histo.IdCandidato = item.IdCandidato;
                    _histo.DataEntrevista = Convert.ToDateTime(item.Data2Entrevista);
                    _listaHistoricos.Add(_histo);
                    _dadosEmail[1] = _histo.DataEntrevista.ToShortDateString();
                    _dadosEmail[2] = _histo.DataEntrevista.ToShortTimeString();

                }
                else
                {
                    if (item.Data2Entrevista != null && item.DataSegundaEntrevista != DateTime.MinValue &&
                        Convert.ToDateTime(item.Data2Entrevista) != item.DataSegundaEntrevista)
                    {
                        Classes.HistoricoDoCandidato _histo = new Classes.HistoricoDoCandidato();

                        _histo.Status = "9";
                        _histo.IdVaga = item.IdVagas;
                        _histo.IdCandidato = item.IdCandidato;
                        _histo.DataEntrevista = Convert.ToDateTime(item.Data2Entrevista);
                        _listaHistoricos.Add(_histo);
                        _dadosEmail[1] = _histo.DataEntrevista.ToShortDateString();
                        _dadosEmail[2] = _histo.DataEntrevista.ToShortTimeString();
                    }
                }

                if (item.EnviarEmail && item.Email != "" && Publicas._usuario.Email != "")
                    Publicas.EnviarEmailProcessoSeletivo(_dadosEmail, Publicas._usuario.Email, item.Email, "", true);
            }

            if (!new CurriculosBO().GravarHistorico(_listaHistoricos))
            {
                new Notificacoes.Mensagem("Problemas durante a gravação.", Publicas.TipoMensagem.Alerta).ShowDialog();
                return;
            }
            // enviar email
        }

        private void buttonAdv1_Click(object sender, EventArgs e)
        {
            foreach (var item in _lista.Where(w => w.IdCandidato == idCandidato))
            {
                if (nomeCampo == "Contato")
                    item.MotivoContato = MotivoTextBox.Text;
                if (nomeCampo == "Aprovado")
                    item.MotivoAprovado = MotivoTextBox.Text;
                if (nomeCampo == "AprovadoGestor")
                    item.MotivoAprovadoGestor = MotivoTextBox.Text;
                if (nomeCampo == "Reprovado")
                    item.MotivoReprovado = MotivoTextBox.Text;
                if (nomeCampo == "ReprovadoGestor")
                    item.MotivoReprovadoGestor = MotivoTextBox.Text;
                if (nomeCampo == "Cancelar")
                    item.MotivoCancelar = MotivoTextBox.Text;
                if (nomeCampo == "SemContato")
                    item.MotivoSemContato = MotivoTextBox.Text;
            }

            MotivoPanel.Visible = false;
        }

        private void SkypeConfirmarButton_Click(object sender, EventArgs e)
        {
            foreach (var item in _lista.Where(w => w.IdCandidato == idCandidato))
            {
                if (nomeCampo == "PorSkype")
                    item.Skype = SkypeTextBox.Text;
            }

            SkypePanel.Visible = false;
        }

        private void MotivoPanel_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = ((Panel)sender).ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(Pens.DarkOliveGreen, rect);
        }

        private void SkypeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SkypeConfirmarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SkypePanel.Visible = false;
            }
        }

        private void MotivoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                buttonAdv1.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                MotivoPanel.Visible = false;
            }
        }

        private void MotivoTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void MotivoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }
        }

        private void CandidatosGrid_TableControlCurrentCellValidating(object sender, GridTableControlCancelEventArgs e)
        {
            
        }

        private void CandidatosGrid_TableControlCurrentCellKeyUp(object sender, GridTableControlKeyEventArgs e)
        {
            _colunaCorrente = CandidatosGrid.TableControl.CurrentCell;

            string data = "";

            nomeCampo = CandidatosGrid.TableDescriptor.Columns[_colunaCorrente.ColIndex - 2].MappingName;

            if (nomeCampo == "Data1Entrevista" || nomeCampo == "Data2Entrevista" && e.Inner.KeyCode == Keys.Enter || e.Inner.KeyCode == Keys.Return)
            {
                foreach (var item in _lista.Where(w => w.IdCandidato == idCandidato))
                {
                    try
                    {
                        if (nomeCampo == "Data1Entrevista")
                            data = item.Data1Entrevista;
                        else
                            data = item.Data2Entrevista;
                    }
                    catch { }

                    if (Convert.ToDateTime(data) < DateTime.Now && data != null && 
                        ((nomeCampo == "Data1Entrevista" && item.DataPrimeiraEntrevista != Convert.ToDateTime(data)) ||
                         (nomeCampo == "Data2Entrevista" && item.DataSegundaEntrevista != Convert.ToDateTime(data))))
                    {
                        new Notificacoes.Mensagem("Data da entrevista do candidado " + item.NomeCandidato + " menor que a data atual.", Publicas.TipoMensagem.Alerta).ShowDialog();
                        return;
                    }
                }
            }
        }
    }
}
