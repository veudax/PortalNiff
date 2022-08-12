using Classes;
using Negocio;
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

namespace Suportte.BolaoCopadoMundo
{
    public partial class PalpiteJogos : Form
    {
        public PalpiteJogos()
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

        List<Classes.BolaoPalpitesDosColaboradores> _listaPalpites;
        Classes.BolaoJogos _ultimaDataFaseGrupo;
        Classes.BolaoJogos _ultimaDataOitavas;
        Classes.BolaoJogos _ultimaDataQuartas;
        Classes.BolaoJogos _ultimaDataSemi;
        Classes.BolaoJogos _ultimaData3Lugar;
        Classes.BolaoJogos _final;
        List<Classes.BolaoTimes> _times;

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

        private void PalpiteJogos_Shown(object sender, EventArgs e)
        {
            string _encrypta = "";
            _listaPalpites = new BolaoPalpitesDosColaboradoresBO().Listar(DateTime.Now.Year, Publicas._idColaborador, false);

            _times = new BolaoTimesBO().Listar(DateTime.Now.Year);

            if (_listaPalpites.Count() == 0)
                _listaPalpites = new BolaoPalpitesDosColaboradoresBO().Listar(DateTime.Now.Year, Publicas._idColaborador, true);

            _ultimaDataFaseGrupo = new BolaoJogosBO().Consultar(DateTime.Now.Year, "GP");
            _ultimaDataOitavas = new BolaoJogosBO().Consultar(DateTime.Now.Year, "8F");
            _ultimaDataQuartas = new BolaoJogosBO().Consultar(DateTime.Now.Year, "4F");
            _ultimaDataSemi = new BolaoJogosBO().Consultar(DateTime.Now.Year, "SF");
            _ultimaData3Lugar = new BolaoJogosBO().Consultar(DateTime.Now.Year, "3L");
            _final = new BolaoJogosBO().Consultar(DateTime.Now.Year, "FN");

            DateTime _data = DateTime.Now.Date;
            //new DateTime(2018, 06, 29);

            GrupoAtabPage.TabVisible = _times.Where(w => w.Grupo == "A").Count() != 0;
            GrupoBTabPage.TabVisible = _times.Where(w => w.Grupo == "B").Count() != 0;
            GrupoCTabPage.TabVisible = _times.Where(w => w.Grupo == "C").Count() != 0;
            GrupoDTabPage.TabVisible = _times.Where(w => w.Grupo == "D").Count() != 0;
            GrupoETabPage.TabVisible = _times.Where(w => w.Grupo == "E").Count() != 0;
            GrupoFTabPage.TabVisible = _times.Where(w => w.Grupo == "F").Count() != 0;
            GrupoGTabPage.TabVisible = _times.Where(w => w.Grupo == "G").Count() != 0;
            GrupoHTabPage.TabVisible = _times.Where(w => w.Grupo == "H").Count() != 0;

            GrupoAtabPage.Enabled = _data <= _ultimaDataFaseGrupo.Data.Date; 
            GrupoBTabPage.Enabled = _data <= _ultimaDataFaseGrupo.Data.Date; 
            GrupoCTabPage.Enabled = _data <= _ultimaDataFaseGrupo.Data.Date; 
            GrupoDTabPage.Enabled = _data <= _ultimaDataFaseGrupo.Data.Date; 
            GrupoETabPage.Enabled = _data <= _ultimaDataFaseGrupo.Data.Date; 
            GrupoFTabPage.Enabled = _data <= _ultimaDataFaseGrupo.Data.Date; 
            GrupoGTabPage.Enabled = _data <= _ultimaDataFaseGrupo.Data.Date; 
            GrupoHTabPage.Enabled = _data <= _ultimaDataFaseGrupo.Data.Date; 

            if (GrupoAtabPage.Enabled)
            {
                FaseGruposTabControl.SelectedTab = GrupoAtabPage;
                GrupoAJ1Placar1CurrencyTextBox.Focus();
            }

            OitavasTabPage.TabVisible = _ultimaDataOitavas.Existe && _ultimaDataOitavas.Data != DateTime.MinValue;
            OitavasTabPage.Enabled = _data <= _ultimaDataOitavas.Data.Date;
            //OitavasTabPage.TabVisible = true; 

            if (OitavasTabPage.Enabled && _data > _ultimaDataFaseGrupo.Data.Date && _data <= _ultimaDataOitavas.Data.Date)
            {
                tabControlAdv1.SelectedTab = OitavasTabPage;
                OitavaJ1Placar1CurrencyTextBox.Focus();
            }

            QuartasTabPage.TabVisible = _ultimaDataQuartas.Existe;
            QuartasTabPage.Enabled = _data <= _ultimaDataQuartas.Data.Date;

            if (QuartasTabPage.Enabled && _data > _ultimaDataOitavas.Data.Date && _data <= _ultimaDataQuartas.Data.Date)
            {
                tabControlAdv1.SelectedTab = QuartasTabPage;
                QuartasJ1Placar1CurrencyTextBox.Focus();
            }

            SemiTabPage.TabVisible = _ultimaDataSemi.Existe;
            SemiTabPage.Enabled = _data >= _ultimaDataQuartas.Data.Date && _data <= _ultimaDataSemi.Data.Date;

            if (SemiTabPage.Enabled && _data > _ultimaDataQuartas.Data.Date && _data <= _ultimaDataSemi.Data.Date)
            {
                tabControlAdv1.SelectedTab = SemiTabPage;
                SemiJ1Placar1CurrencyTextBox.Focus();
            }

            TerceiroTabPage.TabVisible = _ultimaData3Lugar.Existe;
            TerceiroTabPage.Enabled = _data >= _ultimaDataSemi.Data.Date && _data <= _ultimaData3Lugar.Data.Date;
            //TerceiroTabPage.TabVisible = true;// _data > _ultimaDataSemi.Data.Date;

            if (TerceiroTabPage.Enabled && _data > _ultimaDataSemi.Data.Date && _data <= _ultimaData3Lugar.Data.Date)
            {
                tabControlAdv1.SelectedTab = TerceiroTabPage;
                TerceiroJ1Placar1CurrencyTextBox.Focus();
            }

            FinalTabPage.TabVisible = _final.Existe;
            FinalTabPage.Enabled = _data >= _ultimaDataSemi.Data.Date && _data <= _final.Data.Date;
            FinalTabPage.TabVisible =  true;//_data > _ultimaDataSemi.Data.Date;

            if (FinalTabPage.Enabled && _data > _ultimaDataSemi.Data.Date && _data <= _final.Data.Date)
            {
                tabControlAdv1.SelectedTab = FinalTabPage;
                FinalPlacar1CurrencyTextBox.Focus();
            }

            PontuacaoTotalLabel.Visible = _listaPalpites.Where(w => w.Encerrado).Count() != 0;

            int cont = 0;

            PontuacaoTotalLabel.Text = "Pontuação total " + _listaPalpites.Sum(s => s.Pontuacao);

            DateTime _dataSistema = DateTime.Now;
            #region Fase de grupos
            foreach (var item in _listaPalpites.Where(w => w.Grupo1 == "A" && w.Fase == "GP")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
                        item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        GrupoAJ1Panel.Tag = item.IdJogo;
                        GrupoAJ1PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoAJ1PontuacaoLabel.Visible = item.Encerrado;
                        label71.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoAJ1Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;;
                        GrupoAJ1TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoAJ1DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoAJ1DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoAJ1Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoAJ1Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoAJ1PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoAJ1PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoAJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoAJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoAJ1Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoAJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoAJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoAJ1Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 1:
                        GrupoAJ2Panel.Tag = item.IdJogo;
                        GrupoAJ2PontuacaoLabel.Visible = item.Encerrado;
                        GrupoAJ2Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label72.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoAJ2PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoAJ2TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoAJ2DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoAJ2DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoAJ2Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoAJ2Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoAJ2PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoAJ2PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoAJ2Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoAJ2Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoAJ2Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoAJ2Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoAJ2Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoAJ2Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 2:
                        GrupoAJ3Panel.Tag = item.IdJogo;
                        GrupoAJ3PontuacaoLabel.Visible = item.Encerrado;
                        GrupoAJ3Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label78.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoAJ3PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoAJ3TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoAJ3DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoAJ3DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoAJ3Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoAJ3Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoAJ3PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoAJ3PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoAJ3Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoAJ3Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoAJ3Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoAJ3Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoAJ3Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoAJ3Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 3:
                        GrupoAJ4Panel.Tag = item.IdJogo;
                        GrupoAJ4PontuacaoLabel.Visible = item.Encerrado;
                        GrupoAJ4Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label73.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoAJ4PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoAJ4TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoAJ4DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoAJ4DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoAJ4Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoAJ4Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoAJ4PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoAJ4PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoAJ4Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoAJ4Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoAJ4Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoAJ4Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoAJ4Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoAJ4Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 4:
                        GrupoAJ5Panel.Tag = item.IdJogo;
                        GrupoAJ5PontuacaoLabel.Visible = item.Encerrado;
                        GrupoAJ5Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label81.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoAJ5PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoAJ5TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoAJ5DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoAJ5DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoAJ5Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoAJ5Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoAJ5PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoAJ5PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoAJ5Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoAJ5Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoAJ5Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoAJ5Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoAJ5Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoAJ5Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 5:
                        GrupoAJ6Panel.Tag = item.IdJogo;
                        GrupoAJ6PontuacaoLabel.Visible = item.Encerrado;
                        GrupoAJ6Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label82.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoAJ6PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoAJ6TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoAJ6DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoAJ6DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoAJ6Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoAJ6Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoAJ6PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoAJ6PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoAJ6Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoAJ6Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoAJ6Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoAJ6Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoAJ6Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoAJ6Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                }

                cont++;
            }

            cont = 0;
            foreach (var item in _listaPalpites.Where(w => w.Grupo1 == "B" && w.Fase == "GP")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
        item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        GrupoBJ1Panel.Tag = item.IdJogo;
                        GrupoBJ1PontuacaoLabel.Visible = item.Encerrado;
                        GrupoBJ1Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label87.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoBJ1PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoBJ1TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoBJ1DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoBJ1DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoBJ1Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoBJ1Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoBJ1PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoBJ1PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoBJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoBJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoBJ1Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoBJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoBJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoBJ1Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 1:
                        GrupoBJ2Panel.Tag = item.IdJogo;
                        GrupoBJ2PontuacaoLabel.Visible = item.Encerrado;
                        GrupoBJ2Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label88.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoBJ2PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoBJ2TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoBJ2DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoBJ2DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoBJ2Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoBJ2Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoBJ2PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoBJ2PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoBJ2Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoBJ2Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoBJ2Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoBJ2Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoBJ2Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoBJ2Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 2:
                        GrupoBJ3Panel.Tag = item.IdJogo;
                        GrupoBJ3PontuacaoLabel.Visible = item.Encerrado;
                        GrupoBJ3Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label91.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoBJ3PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoBJ3TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoBJ3DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoBJ3DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoBJ3Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoBJ3Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoBJ3PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoBJ3PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoBJ3Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoBJ3Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoBJ3Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoBJ3Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoBJ3Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoBJ3Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 3:
                        GrupoBJ4Panel.Tag = item.IdJogo;
                        GrupoBJ4PontuacaoLabel.Visible = item.Encerrado;
                        GrupoBJ4Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label96.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoBJ4PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoBJ4TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoBJ4DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoBJ4DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoBJ4Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoBJ4Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoBJ4PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoBJ4PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoBJ4Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoBJ4Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoBJ4Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoBJ4Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoBJ4Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoBJ4Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 4:
                        GrupoBJ5Panel.Tag = item.IdJogo;
                        GrupoBJ5PontuacaoLabel.Visible = item.Encerrado;
                        GrupoBJ5Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label97.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoBJ5PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoBJ5TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoBJ5DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoBJ5DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoBJ5Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoBJ5Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoBJ5PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoBJ5PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoBJ5Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoBJ5Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoBJ5Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoBJ5Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoBJ5Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoBJ5Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 5:
                        GrupoBJ6Panel.Tag = item.IdJogo;
                        GrupoBJ6PontuacaoLabel.Visible = item.Encerrado;
                        GrupoBJ6Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label98.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoBJ6PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoBJ6TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoBJ6DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoBJ6DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoBJ6Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoBJ6Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoBJ6PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoBJ6PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoBJ6Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoBJ6Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoBJ6Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoBJ6Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoBJ6Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoBJ6Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                }

                cont++;
            }

            cont = 0;
            foreach (var item in _listaPalpites.Where(w => w.Grupo1 == "C" && w.Fase == "GP")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
        item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        GrupoCJ1Panel.Tag = item.IdJogo;
                        GrupoCJ1PontuacaoLabel.Visible = item.Encerrado;
                        GrupoCJ1Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label101.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoCJ1PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoCJ1TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoCJ1DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoCJ1DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoCJ1Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoCJ1Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoCJ1PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoCJ1PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoCJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoCJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoCJ1Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoCJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoCJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoCJ1Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 1:
                        GrupoCJ2Panel.Tag = item.IdJogo;
                        GrupoCJ2PontuacaoLabel.Visible = item.Encerrado;
                        GrupoCJ2Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label102.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoCJ2PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoCJ2TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoCJ2DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoCJ2DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoCJ2Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoCJ2Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoCJ2PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoCJ2PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoCJ2Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoCJ2Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoCJ2Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoCJ2Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoCJ2Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoCJ2Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 2:
                        GrupoCJ3Panel.Tag = item.IdJogo;
                        GrupoCJ3PontuacaoLabel.Visible = item.Encerrado;
                        GrupoCJ3Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label103.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoCJ3PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoCJ3TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoCJ3DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoCJ3DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoCJ3Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoCJ3Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoCJ3PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoCJ3PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoCJ3Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoCJ3Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoCJ3Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoCJ3Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoCJ3Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoCJ3Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 3:
                        GrupoCJ4Panel.Tag = item.IdJogo;
                        GrupoCJ4PontuacaoLabel.Visible = item.Encerrado;
                        GrupoCJ4Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label106.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoCJ4PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoCJ4TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoCJ4DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoCJ4DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoCJ4Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoCJ4Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoCJ4PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoCJ4PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoCJ4Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoCJ4Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoCJ4Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoCJ4Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoCJ4Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoCJ4Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 4:
                        GrupoCJ5Panel.Tag = item.IdJogo;
                        GrupoCJ5PontuacaoLabel.Visible = item.Encerrado;
                        GrupoCJ5Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label107.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoCJ5PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoCJ5TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoCJ5DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoCJ5DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoCJ5Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoCJ5Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoCJ5PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoCJ5PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoCJ5Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoCJ5Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoCJ5Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoCJ5Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoCJ5Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoCJ5Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 5:
                        GrupoCJ6Panel.Tag = item.IdJogo;
                        GrupoCJ6PontuacaoLabel.Visible = item.Encerrado;
                        GrupoCJ6Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label108.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoCJ6PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoCJ6TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoCJ6DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoCJ6DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoCJ6Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoCJ6Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoCJ6PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoCJ6PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoCJ6Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoCJ6Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoCJ6Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoCJ6Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoCJ6Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoCJ6Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                }

                cont++;
            }

            cont = 0;
            foreach (var item in _listaPalpites.Where(w => w.Grupo1 == "D" && w.Fase == "GP")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
        item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        GrupoDJ1Panel.Tag = item.IdJogo;
                        GrupoDJ1PontuacaoLabel.Visible = item.Encerrado;
                        GrupoDJ1Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label111.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoDJ1PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoDJ1TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoDJ1DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoDJ1DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoDJ1Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoDJ1Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoDJ1PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoDJ1PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoDJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoDJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoDJ1Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoDJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoDJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoDJ1Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 1:
                        GrupoDJ2Panel.Tag = item.IdJogo;
                        GrupoDJ2PontuacaoLabel.Visible = item.Encerrado;
                        GrupoDJ2Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label112.Visible = item.Encerrado &&
                             item.Empritado != _encrypta;
                        GrupoDJ2PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoDJ2TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoDJ2DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoDJ2DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoDJ2Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoDJ2Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoDJ2PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoDJ2PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoDJ2Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoDJ2Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoDJ2Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoDJ2Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoDJ2Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoDJ2Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 2:
                        GrupoDJ3Panel.Tag = item.IdJogo;
                        GrupoDJ3PontuacaoLabel.Visible = item.Encerrado;
                        GrupoDJ3Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label113.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoDJ3PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoDJ3TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoDJ3DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoDJ3DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoDJ3Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoDJ3Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoDJ3PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoDJ3PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoDJ3Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoDJ3Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoDJ3Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoDJ3Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoDJ3Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoDJ3Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 3:
                        GrupoDJ4Panel.Tag = item.IdJogo;
                        GrupoDJ4PontuacaoLabel.Visible = item.Encerrado;
                        GrupoDJ4Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label116.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoDJ4PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoDJ4TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoDJ4DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoDJ4DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoDJ4Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoDJ4Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoDJ4PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoDJ4PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoDJ4Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoDJ4Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoDJ4Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoDJ4Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoDJ4Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoDJ4Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 4:
                        GrupoDJ5Panel.Tag = item.IdJogo;
                        GrupoDJ5PontuacaoLabel.Visible = item.Encerrado;
                        GrupoDJ5Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label117.Visible = item.Encerrado &&
                             item.Empritado != _encrypta;
                        GrupoDJ5PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoDJ5TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoDJ5DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoDJ5DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoDJ5Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoDJ5Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoDJ5PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoDJ5PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoDJ5Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoDJ5Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoDJ5Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoDJ5Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoDJ5Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoDJ5Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 5:
                        GrupoDJ6Panel.Tag = item.IdJogo;
                        GrupoDJ6PontuacaoLabel.Visible = item.Encerrado;
                        GrupoDJ6Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label118.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoDJ6PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoDJ6TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoDJ6DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoDJ6DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoDJ6Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoDJ6Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoDJ6PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoDJ6PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoDJ6Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoDJ6Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoDJ6Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoDJ6Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoDJ6Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoDJ6Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                }

                cont++;
            }

            cont = 0;
            foreach (var item in _listaPalpites.Where(w => w.Grupo1 == "E" && w.Fase == "GP")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
        item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        GrupoEJ1Panel.Tag = item.IdJogo;
                        GrupoEJ1PontuacaoLabel.Visible = item.Encerrado;
                        GrupoEJ1Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label121.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoEJ1PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoEJ1TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoEJ1DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoEJ1DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoEJ1Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoEJ1Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoEJ1PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoEJ1PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoEJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoEJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoEJ1Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoEJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoEJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoEJ1Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 1:
                        GrupoEJ2Panel.Tag = item.IdJogo;
                        GrupoEJ2PontuacaoLabel.Visible = item.Encerrado;
                        GrupoEJ2Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label122.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoEJ2PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoEJ2TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoEJ2DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoEJ2DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoEJ2Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoEJ2Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoEJ2PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoEJ2PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoEJ2Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoEJ2Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoEJ2Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoEJ2Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoEJ2Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoEJ2Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 2:
                        GrupoEJ3Panel.Tag = item.IdJogo;
                        GrupoEJ3PontuacaoLabel.Visible = item.Encerrado;
                        GrupoEJ3Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label123.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoEJ3PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoEJ3TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoEJ3DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoEJ3DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoEJ3Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoEJ3Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoEJ3PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoEJ3PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoEJ3Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoEJ3Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoEJ3Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoEJ3Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoEJ3Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoEJ3Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 3:
                        GrupoEJ4Panel.Tag = item.IdJogo;
                        GrupoEJ4PontuacaoLabel.Visible = item.Encerrado;
                        GrupoEJ4Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label126.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoEJ4PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoEJ4TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoEJ4DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoEJ4DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoEJ4Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoEJ4Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoEJ4PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoEJ4PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoEJ4Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoEJ4Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoEJ4Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoEJ4Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoEJ4Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoEJ4Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 4:
                        GrupoEJ5Panel.Tag = item.IdJogo;
                        GrupoEJ5PontuacaoLabel.Visible = item.Encerrado;
                        GrupoEJ5Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label127.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoEJ5PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoEJ5TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoEJ5DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoEJ5DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoEJ5Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoEJ5Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoEJ5PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoEJ5PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoEJ5Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoEJ5Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoEJ5Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoEJ5Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoEJ5Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoEJ5Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 5:
                        GrupoEJ6Panel.Tag = item.IdJogo;
                        GrupoEJ6PontuacaoLabel.Visible = item.Encerrado;
                        GrupoEJ6Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label128.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoEJ6PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoEJ6TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoEJ6DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoEJ6DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoEJ6Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoEJ6Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoEJ6PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoEJ6PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoEJ6Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoEJ6Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoEJ6Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoEJ6Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoEJ6Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoEJ6Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                }

                cont++;
            }

            cont = 0;
            foreach (var item in _listaPalpites.Where(w => w.Grupo1 == "F" && w.Fase == "GP")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
        item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        GrupoFJ1Panel.Tag = item.IdJogo;
                        GrupoFJ1PontuacaoLabel.Visible = item.Encerrado;
                        GrupoFJ1Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label131.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoFJ1PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoFJ1TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoFJ1DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoFJ1DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoFJ1Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoFJ1Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoFJ1PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoFJ1PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoFJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoFJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoFJ1Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoFJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoFJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoFJ1Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 1:
                        GrupoFJ2Panel.Tag = item.IdJogo;
                        GrupoFJ2PontuacaoLabel.Visible = item.Encerrado;
                        GrupoFJ2Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label132.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoFJ2PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoFJ2TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoFJ2DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoFJ2DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoFJ2Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoFJ2Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoFJ2PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoFJ2PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoFJ2Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoFJ2Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoFJ2Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoFJ2Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoFJ2Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoFJ2Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 2:
                        GrupoFJ3Panel.Tag = item.IdJogo;
                        GrupoFJ3PontuacaoLabel.Visible = item.Encerrado;
                        GrupoFJ3Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label133.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoFJ3PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoFJ3TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoFJ3DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoFJ3DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoFJ3Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoFJ3Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoFJ3PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoFJ3PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoFJ3Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoFJ3Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoFJ3Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoFJ3Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoFJ3Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoFJ3Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 3:
                        GrupoFJ4Panel.Tag = item.IdJogo;
                        GrupoFJ4PontuacaoLabel.Visible = item.Encerrado;
                        GrupoFJ4Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label136.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoFJ4PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoFJ4TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoFJ4DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoFJ4DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoFJ4Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoFJ4Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoFJ4PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoFJ4PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoFJ4Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoFJ4Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoFJ4Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoFJ4Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoFJ4Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoFJ4Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 4:
                        GrupoFJ5Panel.Tag = item.IdJogo;
                        GrupoFJ5PontuacaoLabel.Visible = item.Encerrado;
                        GrupoFJ5Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label137.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoFJ5PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoFJ5TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoFJ5DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoFJ5DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoFJ5Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoFJ5Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoFJ5PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoFJ5PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoFJ5Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoFJ5Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoFJ5Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoFJ5Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoFJ5Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoFJ5Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 5:
                        GrupoFJ6Panel.Tag = item.IdJogo;
                        GrupoFJ6PontuacaoLabel.Visible = item.Encerrado;
                        GrupoFJ6Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label138.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoFJ6PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoFJ6TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoFJ6DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoFJ6DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoFJ6Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoFJ6Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoFJ6PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoFJ6PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoFJ6Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoFJ6Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoFJ6Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoFJ6Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoFJ6Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoFJ6Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                }

                cont++;
            }

            cont = 0;
            foreach (var item in _listaPalpites.Where(w => w.Grupo1 == "G" && w.Fase == "GP")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
        item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        GrupoGJ1Panel.Tag = item.IdJogo;
                        GrupoGJ1PontuacaoLabel.Visible = item.Encerrado;
                        GrupoGJ1Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label141.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoGJ1PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoGJ1TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoGJ1DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoGJ1DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoGJ1Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoGJ1Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoGJ1PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoGJ1PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoGJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoGJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoGJ1Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoGJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoGJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoGJ1Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 1:
                        GrupoGJ2Panel.Tag = item.IdJogo;
                        GrupoGJ2PontuacaoLabel.Visible = item.Encerrado;
                        GrupoGJ2Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label142.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoGJ2PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoGJ2TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoGJ2DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoGJ2DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoGJ2Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoGJ2Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoGJ2PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoGJ2PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoGJ2Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoGJ2Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoGJ2Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoGJ2Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoGJ2Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoGJ2Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 2:
                        GrupoGJ3Panel.Tag = item.IdJogo;
                        GrupoGJ3PontuacaoLabel.Visible = item.Encerrado;
                        GrupoGJ3Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label143.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;
                        GrupoGJ3PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoGJ3TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoGJ3DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoGJ3DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoGJ3Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoGJ3Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoGJ3PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoGJ3PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoGJ3Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoGJ3Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoGJ3Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoGJ3Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoGJ3Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoGJ3Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 3:
                        GrupoGJ4Panel.Tag = item.IdJogo;
                        GrupoGJ4PontuacaoLabel.Visible = item.Encerrado;
                        GrupoGJ4Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label146.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        GrupoGJ4PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoGJ4TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoGJ4DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoGJ4DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoGJ4Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoGJ4Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoGJ4PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoGJ4PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoGJ4Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoGJ4Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoGJ4Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoGJ4Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoGJ4Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoGJ4Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 4:
                        GrupoGJ5Panel.Tag = item.IdJogo;
                        GrupoGJ5PontuacaoLabel.Visible = item.Encerrado;
                        GrupoGJ5Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label147.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        GrupoGJ5PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoGJ5TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoGJ5DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoGJ5DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoGJ5Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoGJ5Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoGJ5PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoGJ5PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoGJ5Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoGJ5Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoGJ5Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoGJ5Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoGJ5Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoGJ5Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 5:
                        GrupoGJ6Panel.Tag = item.IdJogo;
                        GrupoGJ6PontuacaoLabel.Visible = item.Encerrado;
                        GrupoGJ6Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label148.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        GrupoGJ6PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoGJ6TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoGJ6DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoGJ6DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoGJ6Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoGJ6Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoGJ6PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoGJ6PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoGJ6Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoGJ6Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoGJ6Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoGJ6Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoGJ6Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoGJ6Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                }

                cont++;
            }

            cont = 0;
            foreach (var item in _listaPalpites.Where(w => w.Grupo1 == "H" && w.Fase == "GP")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
        item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        GrupoHJ1Panel.Tag = item.IdJogo;
                        GrupoHJ1PontuacaoLabel.Visible = item.Encerrado;
                        GrupoHJ1Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label151.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        GrupoHJ1PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoHJ1TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoHJ1DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoHJ1DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoHJ1Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoHJ1Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoHJ1PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoHJ1PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoHJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoHJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoHJ1Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoHJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoHJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoHJ1Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 1:
                        GrupoHJ2Panel.Tag = item.IdJogo;
                        GrupoHJ2PontuacaoLabel.Visible = item.Encerrado;
                        GrupoHJ2Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label152.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        GrupoHJ2PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoHJ2TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoHJ2DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoHJ2DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoHJ2Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoHJ2Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoHJ2PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoHJ2PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoHJ2Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoHJ2Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoHJ2Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoHJ2Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoHJ2Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoHJ2Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 2:
                        GrupoHJ3Panel.Tag = item.IdJogo;
                        GrupoHJ3PontuacaoLabel.Visible = item.Encerrado;
                        GrupoHJ3Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label153.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        GrupoHJ3PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoHJ3TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoHJ3DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoHJ3DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoHJ3Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoHJ3Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoHJ3PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoHJ3PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoHJ3Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoHJ3Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoHJ3Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoHJ3Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoHJ3Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoHJ3Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 3:
                        GrupoHJ4Panel.Tag = item.IdJogo;
                        GrupoHJ4PontuacaoLabel.Visible = item.Encerrado;
                        GrupoHJ4Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label156.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        GrupoHJ4PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoHJ4TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoHJ4DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoHJ4DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoHJ4Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoHJ4Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoHJ4PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoHJ4PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoHJ4Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoHJ4Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoHJ4Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoHJ4Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoHJ4Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoHJ4Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 4:
                        GrupoHJ5Panel.Tag = item.IdJogo;
                        GrupoHJ5PontuacaoLabel.Visible = item.Encerrado;
                        GrupoHJ5Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label157.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        GrupoHJ5PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoHJ5TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoHJ5DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoHJ5DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoHJ5Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoHJ5Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoHJ5PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoHJ5PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoHJ5Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoHJ5Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoHJ5Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoHJ5Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoHJ5Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoHJ5Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 5:
                        GrupoHJ6Panel.Tag = item.IdJogo;
                        GrupoHJ6PontuacaoLabel.Visible = item.Encerrado;
                        GrupoHJ6Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema;
                        label158.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        GrupoHJ6PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        GrupoHJ6TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        GrupoHJ6DescricaoSelecao1TextBox.Text = item.Nome1;
                        GrupoHJ6DescricaoSelecao2TextBoxExt.Text = item.Nome2;
                        GrupoHJ6Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        GrupoHJ6Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        GrupoHJ6PlacarFinal1Label.Text = item.PlacarOficial1.ToString();
                        GrupoHJ6PlacarFinal2Label.Text = item.PlacarOficial2.ToString();

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoHJ6Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoHJ6Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoHJ6Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                GrupoHJ6Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            GrupoHJ6Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            GrupoHJ6Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                }

                cont++;
            }

#endregion

            cont = 0;
            foreach (var item in _listaPalpites.Where(w => w.Fase == "8F")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
                            item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        OitavaJ1Panel.Tag = item.IdJogo;
                        OitavasJ1PontuacaoLabel.Visible = item.Encerrado;
                        OitavaJ1Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null;
                        label161.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        OitavasJ1PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        OitavasJ1TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        OitavaJ1Time1TextBox.Text = item.Nome1;
                        OitavaJ1Time2TextBox.Text = item.Nome2;
                        OitavaJ1Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        OitavaJ1Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        OitavaJ1Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") +
                            item.PlacarOficial1.ToString();
                        OitavaJ1Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ1Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ1Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 1:
                        OitavaJ2Panel.Tag = item.IdJogo;
                        OitavasJ2PontuacaoLabel.Visible = item.Encerrado;
                        OitavaJ2Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null;
                        label162.Visible = item.Encerrado &&
                             item.Empritado != _encrypta;;
                        OitavasJ2PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        OitavasJ2TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        OitavaJ2Time1TextBox.Text = item.Nome1;
                        OitavaJ2Time2TextBox.Text = item.Nome2;
                        OitavaJ2Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        OitavaJ2Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        OitavaJ2Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") + item.PlacarOficial1.ToString();
                        OitavaJ2Resultado2Label.Text = item.PlacarOficial2.ToString()+  (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ2Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ2Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ2Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ2Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ2Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ2Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 2:
                        OitavaJ3Panel.Tag = item.IdJogo;
                        OitavasJ3PontuacaoLabel.Visible = item.Encerrado;
                        OitavaJ3Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null;
                        label163.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        OitavasJ3PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        OitavasJ3TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        OitavaJ3Time1TextBox.Text = item.Nome1;
                        OitavaJ3Time2TextBox.Text = item.Nome2;
                        OitavaJ3Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        OitavaJ3Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        OitavaJ3Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") + item.PlacarOficial1.ToString();
                        OitavaJ3Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ3Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ3Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ3Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ3Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ3Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ3Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 3:
                        OitavaJ4Panel.Tag = item.IdJogo;
                        OitavasJ4PontuacaoLabel.Visible = item.Encerrado;
                        OitavaJ4Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null;
                        label166.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        OitavasJ4PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        OitavasJ4TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        OitavaJ4Time1TextBox.Text = item.Nome1;
                        OitavaJ4Time2TextBox.Text = item.Nome2;
                        OitavaJ4Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        OitavaJ4Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        OitavaJ4Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") + item.PlacarOficial1.ToString();
                        OitavaJ4Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ4Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ4Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ4Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ4Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ4Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ4Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 4:
                        OitavaJ5Panel.Tag = item.IdJogo;
                        OitavasJ5PontuacaoLabel.Visible = item.Encerrado;
                        OitavaJ5Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null;
                        label167.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        OitavasJ5PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        OitavasJ5TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        OitavaJ5Time1TextBox.Text = item.Nome1;
                        OitavaJ5Time2TextBox.Text = item.Nome2;
                        OitavaJ5Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        OitavaJ5Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        OitavaJ5Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") + item.PlacarOficial1.ToString();
                        OitavaJ5Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "") ;

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ5Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ5Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ5Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ5Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ5Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ5Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 5:
                        OitavaJ6Panel.Tag = item.IdJogo;
                        OitavasJ6PontuacaoLabel.Visible = item.Encerrado;
                        OitavaJ6Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null;
                        label168.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        OitavasJ6PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        OitavasJ6TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        OitavaJ6Time1TextBox.Text = item.Nome1;
                        OitavaJ6Time2TextBox.Text = item.Nome2;
                        OitavaJ6Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        OitavaJ6Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        OitavaJ6Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") + item.PlacarOficial1.ToString();
                        OitavaJ6Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ6Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ6Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ6Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ6Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ6Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ6Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 6:
                        OitavaJ7Panel.Tag = item.IdJogo;
                        OitavasJ7PontuacaoLabel.Visible = item.Encerrado;
                        OitavaJ7Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null;
                        label171.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        OitavasJ7PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        OitavasJ7TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        OitavaJ7Time1TextBox.Text = item.Nome1;
                        OitavaJ7Time2TextBox.Text = item.Nome2;
                        OitavaJ7Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        OitavaJ7Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        OitavaJ7Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") + item.PlacarOficial1.ToString();
                        OitavaJ7Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ7Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ7Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ7Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ7Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ7Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ7Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 7:
                        OitavaJ8Panel.Tag = item.IdJogo;
                        OitavasJ8PontuacaoLabel.Visible = item.Encerrado;
                        OitavaJ8Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null;
                        label172.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        OitavasJ8PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        OitavasJ8TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        OitavaJ8Time1TextBox.Text = item.Nome1;
                        OitavaJ8Time2TextBox.Text = item.Nome2;
                        OitavaJ8Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        OitavaJ8Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        OitavaJ8Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") + item.PlacarOficial1.ToString();
                        OitavaJ8Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ8Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ8Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ8Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                OitavaJ8Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            OitavaJ8Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            OitavaJ8Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                }

                cont++;
            }


            cont = 0;
            foreach (var item in _listaPalpites.Where(w => w.Fase == "4F")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
                            item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        QuartasJ1.Tag = item.IdJogo;
                        QuartasJ1PontuacaoLabel.Visible = item.Encerrado;
                        QuartasJ1.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null; ;
                        label173.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        QuartasJ1PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        QuartasJ1TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        QuartasJ1Time1TextBox.Text = item.Nome1;
                        QuartasJ1Time2TextBox.Text = item.Nome2;
                        QuartasJ1Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        QuartasJ1Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        QuartasJ1Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "")+ item.PlacarOficial1.ToString();
                        QuartasJ1Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                QuartasJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            QuartasJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            QuartasJ1Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                QuartasJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            QuartasJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            QuartasJ1Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 1:
                        QuartasJ2.Tag = item.IdJogo;
                        QuartasJ2PontuacaoLabel.Visible = item.Encerrado;
                        QuartasJ2.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null;
                        label176.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        QuartasJ2PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        QuartasJ2TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        QuartasJ2Time1TextBox.Text = item.Nome1;
                        QuartasJ2Time2TextBox.Text = item.Nome2;
                        QuartasJ2Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        QuartasJ2Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        QuartasJ2Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") + item.PlacarOficial1.ToString();
                        QuartasJ2Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                QuartasJ2Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            QuartasJ2Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            QuartasJ2Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                QuartasJ2Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            QuartasJ2Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            QuartasJ2Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 2:
                        QuartasJ3.Tag = item.IdJogo;
                        QuartasJ3PontuacaoLabel.Visible = item.Encerrado;
                        QuartasJ3.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null;
                        label177.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        QuartasJ3PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        QuartasJ3TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        QuartasJ3Time1TextBox.Text = item.Nome1;
                        QuartasJ3Time2TextBox.Text = item.Nome2;
                        QuartasJ3Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        QuartasJ3Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        QuartasJ3Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") + item.PlacarOficial1.ToString();
                        QuartasJ3Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                QuartasJ3Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            QuartasJ3Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            QuartasJ3Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                QuartasJ3Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            QuartasJ3Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            QuartasJ3Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 3:
                        QuartasJ4.Tag = item.IdJogo;
                        QuartasJ4PontuacaoLabel.Visible = item.Encerrado;
                        QuartasJ4.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null;
                        label178.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        QuartasJ4PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        QuartasJ4TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        QuartasJ4Time1TextBox.Text = item.Nome1;
                        QuartasJ4Time2TextBox.Text = item.Nome2;
                        QuartasJ4Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        QuartasJ4Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        QuartasJ4Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") + item.PlacarOficial1.ToString();
                        QuartasJ4Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                QuartasJ4Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            QuartasJ4Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            QuartasJ4Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                QuartasJ4Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            QuartasJ4Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            QuartasJ4Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;                    
                }

                cont++;
            }

            cont = 0;
            foreach (var item in _listaPalpites.Where(w => w.Fase == "SF")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
                            item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        SemiJ1Panel.Tag = item.IdJogo;
                        SemiJ1PontuacaoLabel.Visible = item.Encerrado;
                        SemiJ1Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null; ;
                        label181.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        SemiJ1PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        SemiJ1TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        SemiJ1Time1TextBox.Text = item.Nome1;
                        SemiJ1Time2TextBox.Text = item.Nome2;
                        SemiJ1Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        SemiJ1Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        SemiJ1Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") + item.PlacarOficial1.ToString();
                        SemiJ1Resultado2Label.Text = item.PlacarOficial2.ToString()+ (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                SemiJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            SemiJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            SemiJ1Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                SemiJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            SemiJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            SemiJ1Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                    case 1:
                        SemiJ2Panel.Tag = item.IdJogo;
                        SemiJ2PontuacaoLabel.Visible = item.Encerrado;
                        SemiJ2Panel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null;
                        label182.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        SemiJ2PontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        SemiJ2TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        SemiJ2Time1TextBox.Text = item.Nome1;
                        SemiJ2Time2TextBox.Text = item.Nome2;
                        SemiJ2Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        SemiJ2Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        SemiJ2Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "")+ item.PlacarOficial1.ToString();
                        SemiJ2Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                SemiJ2Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            SemiJ2Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            SemiJ2Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                SemiJ2Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            SemiJ2Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            SemiJ2Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;                   
                }

                cont++;
            }

            cont = 0;
            foreach (var item in _listaPalpites.Where(w => w.Fase == "3L")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
        item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        TerceiroJ1TituloSubPanel.Tag = item.IdJogo;
                        TerceiroPontuacaoLabel.Visible = item.Encerrado;
                        TerceiroPanel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null; ;
                        label183.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        TerceiroPontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        TerceiroJ1TituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        TerceiroJ1Time1TextBox.Text = item.Nome1;
                        TerceiroJ1Time2TextBox.Text = item.Nome2;
                        TerceiroJ1Placar1CurrencyTextBox.Text = item.Placar1.ToString();
                        TerceiroJ1Placar2CurrencyTextBox.Text = item.Placar2.ToString();
                        TerceiroJ1Resultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "")+ item.PlacarOficial1.ToString();
                        TerceiroJ1Resultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                TerceiroJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            TerceiroJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            TerceiroJ1Bandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                TerceiroJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            TerceiroJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            TerceiroJ1Bandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;                    
                }

                cont++;
            }

            cont = 0;
            foreach (var item in _listaPalpites.Where(w => w.Fase == "FN")
                                               .OrderBy(o => o.Data))
            {
                _encrypta = Publicas.Encrypta(Publicas._idColaborador.ToString() + item.IdJogo.ToString() +
                item.Placar1.ToString() + item.Placar2.ToString() + item.Pontuacao.ToString(), Publicas.CryptProvider.DES).PadRight(100, ' ').Trim();

                switch (cont)
                {
                    case 0:
                        FinalTituloSubPanel.Tag = item.IdJogo;
                        FinalPontuacaoLabel.Visible = item.Encerrado;
                        FinalPanel.Enabled = !item.Encerrado && item.DataLimite >= _dataSistema && item.Bandeira1 != null && item.Bandeira2 != null; ;
                        label186.Visible = item.Encerrado &&
                            item.Empritado != _encrypta;;
                        FinalPontuacaoLabel.Text = "Sua pontuação nesse jogo: " + item.Pontuacao;
                        FinalTituloSubLabel.Text = item.Data.ToShortDateString() + " " + item.Data.ToShortTimeString() + " " +
                            Publicas.DiaDaSemana(item.Data.DayOfWeek) + " - " + item.Localizacao;
                        FinalTime1TextBox.Text = item.Nome1;
                        FinalTime2TextBox.Text = item.Nome2;
                        FinalPlacar1CurrencyTextBox.Text = item.Placar1.ToString();
                        FinalPlacar2CurrencyTextBox.Text = item.Placar2.ToString();
                        FinalResultado1Label.Text = (item.Penalti1 != 0 ? "[" + item.Penalti1.ToString() + "] " : "") + item.PlacarOficial1.ToString();
                        FinalResultado2Label.Text = item.PlacarOficial2.ToString() + (item.Penalti2 != 0 ? " [" + item.Penalti2.ToString() + "] " : "");

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira1, 0, item.Bandeira1.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                FinalBandeira1PictureBox.Image = new Bitmap(mStream);
                            }

                            FinalBandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            FinalBandeira1PictureBox.Refresh();
                        }
                        catch { }

                        try
                        {
                            using (MemoryStream mStream = new MemoryStream())
                            {
                                mStream.Write(item.Bandeira2, 0, item.Bandeira2.Length);
                                mStream.Seek(0, SeekOrigin.Begin);

                                FinalBandeira2PictureBox.Image = new Bitmap(mStream);
                            }

                            FinalBandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            FinalBandeira2PictureBox.Refresh();
                        }
                        catch { }
                        break;
                }

                cont++;
            }

            if (GrupoAJ6Panel.Enabled)
                FaseGruposTabControl.SelectedTab = GrupoAtabPage;
            else
            {
                if (GrupoBJ6Panel.Enabled)
                    FaseGruposTabControl.SelectedTab = GrupoBTabPage;
                else
                {
                    if (GrupoCJ6Panel.Enabled)
                        FaseGruposTabControl.SelectedTab = GrupoCTabPage;
                    else
                    {
                        if (GrupoDJ6Panel.Enabled)
                            FaseGruposTabControl.SelectedTab = GrupoDTabPage;
                        else
                        {
                            if (GrupoEJ6Panel.Enabled)
                                FaseGruposTabControl.SelectedTab = GrupoETabPage;
                            else
                            {
                                if (GrupoFJ6Panel.Enabled)
                                    FaseGruposTabControl.SelectedTab = GrupoFTabPage;
                                else
                                {
                                    if (GrupoGJ6Panel.Enabled)
                                        FaseGruposTabControl.SelectedTab = GrupoGTabPage;
                                    else
                                    {
                                        if (GrupoHJ6Panel.Enabled)
                                            FaseGruposTabControl.SelectedTab = GrupoHTabPage;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            gravarButton.Enabled = true;
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            
            #region Grupo A
            int idJogos = Convert.ToInt32(GrupoAJ1Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoAJ1Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoAJ1Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoAJ2Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoAJ2Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoAJ2Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoAJ3Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoAJ3Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoAJ3Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoAJ4Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoAJ4Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoAJ4Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoAJ5Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoAJ5Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoAJ5Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoAJ6Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoAJ6Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoAJ6Placar2CurrencyTextBox.DecimalValue;
            }

            #endregion

            #region Grupo B
            idJogos = Convert.ToInt32(GrupoBJ1Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoBJ1Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoBJ1Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoBJ2Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoBJ2Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoBJ2Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoBJ3Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoBJ3Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoBJ3Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoBJ4Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoBJ4Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoBJ4Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoBJ5Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoBJ5Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoBJ5Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoBJ6Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoBJ6Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoBJ6Placar2CurrencyTextBox.DecimalValue;
            }
            #endregion

            #region Grupo C
            idJogos = Convert.ToInt32(GrupoCJ1Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoCJ1Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoCJ1Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoCJ2Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoCJ2Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoCJ2Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoCJ3Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoCJ3Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoCJ3Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoCJ4Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoCJ4Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoCJ4Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoCJ5Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoCJ5Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoCJ5Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoCJ6Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoCJ6Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoCJ6Placar2CurrencyTextBox.DecimalValue;
            }
            #endregion

            #region Grupo D
            idJogos = Convert.ToInt32(GrupoDJ1Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoDJ1Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoDJ1Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoDJ2Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoDJ2Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoDJ2Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoDJ3Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoDJ3Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoDJ3Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoDJ4Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoDJ4Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoDJ4Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoDJ5Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoDJ5Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoDJ5Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoDJ6Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoDJ6Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoDJ6Placar2CurrencyTextBox.DecimalValue;
            }
            #endregion

            #region Grupo E
            idJogos = Convert.ToInt32(GrupoEJ1Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoEJ1Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoEJ1Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoEJ2Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoEJ2Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoEJ2Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoEJ3Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoEJ3Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoEJ3Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoEJ4Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoEJ4Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoEJ4Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoEJ5Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoEJ5Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoEJ5Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoEJ6Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoEJ6Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoEJ6Placar2CurrencyTextBox.DecimalValue;
            }
            #endregion

            #region Grupo F
            idJogos = Convert.ToInt32(GrupoFJ1Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoFJ1Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoFJ1Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoFJ2Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoFJ2Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoFJ2Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoFJ3Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoFJ3Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoFJ3Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoFJ4Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoFJ4Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoFJ4Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoFJ5Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoFJ5Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoFJ5Placar2CurrencyTextBox.DecimalValue;
            }

            
            idJogos = Convert.ToInt32(GrupoFJ6Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoFJ6Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoFJ6Placar2CurrencyTextBox.DecimalValue;
            }
            #endregion

            #region Grupo G
            idJogos = Convert.ToInt32(GrupoGJ1Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoGJ1Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoGJ1Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoGJ2Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoGJ2Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoGJ2Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoGJ3Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoGJ3Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoGJ3Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoGJ4Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoGJ4Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoGJ4Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoGJ5Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoGJ5Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoGJ5Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoGJ6Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoGJ6Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoGJ6Placar2CurrencyTextBox.DecimalValue;
            }
            #endregion

            #region Grupo H
            idJogos = Convert.ToInt32(GrupoHJ1Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoHJ1Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoHJ1Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoHJ2Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoHJ2Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoHJ2Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoHJ3Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoHJ3Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoHJ3Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoHJ4Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoHJ4Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoHJ4Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoHJ5Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoHJ5Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoHJ5Placar2CurrencyTextBox.DecimalValue;
            }

            idJogos = Convert.ToInt32(GrupoHJ6Panel.Tag);

            foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
            {
                item.Placar1 = (int)GrupoHJ6Placar1CurrencyTextBox.DecimalValue;
                item.Placar2 = (int)GrupoHJ6Placar2CurrencyTextBox.DecimalValue;
            }
            #endregion

            #region Oitavas
            if (OitavasTabPage.TabVisible)
            {
                idJogos = Convert.ToInt32(OitavaJ1Panel.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)OitavaJ1Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)OitavaJ1Placar2CurrencyTextBox.DecimalValue;
                }

                idJogos = Convert.ToInt32(OitavaJ2Panel.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)OitavaJ2Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)OitavaJ2Placar2CurrencyTextBox.DecimalValue;
                }

                idJogos = Convert.ToInt32(OitavaJ3Panel.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)OitavaJ3Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)OitavaJ3Placar2CurrencyTextBox.DecimalValue;
                }

                idJogos = Convert.ToInt32(OitavaJ4Panel.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)OitavaJ4Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)OitavaJ4Placar2CurrencyTextBox.DecimalValue;
                }

                idJogos = Convert.ToInt32(OitavaJ5Panel.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)OitavaJ5Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)OitavaJ5Placar2CurrencyTextBox.DecimalValue;
                }

                idJogos = Convert.ToInt32(OitavaJ6Panel.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)OitavaJ6Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)OitavaJ6Placar2CurrencyTextBox.DecimalValue;
                }

                idJogos = Convert.ToInt32(OitavaJ7Panel.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)OitavaJ7Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)OitavaJ7Placar2CurrencyTextBox.DecimalValue;
                }

                idJogos = Convert.ToInt32(OitavaJ8Panel.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)OitavaJ8Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)OitavaJ8Placar2CurrencyTextBox.DecimalValue;
                }
            }
            #endregion

            #region Quartas
            if (QuartasTabPage.TabVisible)
            {
                idJogos = Convert.ToInt32(QuartasJ1.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)QuartasJ1Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)QuartasJ1Placar2CurrencyTextBox.DecimalValue;
                }

                idJogos = Convert.ToInt32(QuartasJ2.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)QuartasJ2Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)QuartasJ2Placar2CurrencyTextBox.DecimalValue;
                }

                idJogos = Convert.ToInt32(QuartasJ3.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)QuartasJ3Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)QuartasJ3Placar2CurrencyTextBox.DecimalValue;
                }

                idJogos = Convert.ToInt32(QuartasJ4.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)QuartasJ4Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)QuartasJ4Placar2CurrencyTextBox.DecimalValue;
                }
            }
            #endregion

            #region SemiFinal
            if (SemiTabPage.TabVisible)
            {
                idJogos = Convert.ToInt32(SemiJ1Panel.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)SemiJ1Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)SemiJ1Placar2CurrencyTextBox.DecimalValue;
                }

                idJogos = Convert.ToInt32(SemiJ2Panel.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)SemiJ2Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)SemiJ2Placar2CurrencyTextBox.DecimalValue;
                }
            }
            #endregion

            #region Terceiro lugar
            if (TerceiroTabPage.TabVisible)
            {
                idJogos = Convert.ToInt32(TerceiroJ1TituloSubPanel.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)TerceiroJ1Placar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)TerceiroJ1Placar2CurrencyTextBox.DecimalValue;
                }
            }
            #endregion

            #region Final
            if (FinalTabPage.TabVisible)
            {
                idJogos = Convert.ToInt32(FinalTituloSubPanel.Tag);

                foreach (var item in _listaPalpites.Where(w => w.IdJogo == idJogos))
                {
                    item.Placar1 = (int)FinalPlacar1CurrencyTextBox.DecimalValue;
                    item.Placar2 = (int)FinalPlacar2CurrencyTextBox.DecimalValue;
                }
            }
            #endregion

            _listaPalpites.ForEach(u => u.IdColaborador = Publicas._idColaborador);

            if (!new BolaoPalpitesDosColaboradoresBO().Gravar(_listaPalpites))
            {
                new Notificacoes.Mensagem("Problemas durante a gravar." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            new Notificacoes.Mensagem("Palpites gravados com sucesso.", Publicas.TipoMensagem.Sucesso).ShowDialog();

            PalpiteJogos_Shown(sender, e);
        }

        private void GrupoAJ1Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoAJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoAJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoAJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoAJ2Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoAJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoAJ2Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoAJ2Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoAJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoAJ2Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoAJ3Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoAJ2Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoAJ3Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoAJ3Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoAJ2Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoAJ3Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoAJ4Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoAJ3Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoAJ4Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoAJ4Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoAJ3Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoAJ4Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoAJ5Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoAJ4Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoAJ5Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoAJ5Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoAJ4Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoAJ5Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoAJ6Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoAJ5Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoAJ6Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoAJ6Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoAJ5Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoAJ6Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                FaseGruposTabControl.SelectedTab = GrupoBTabPage;
                GrupoBJ1Placar1CurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoAJ6Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoBJ1Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FaseGruposTabControl.SelectedTab = GrupoAtabPage;
                GrupoAJ6Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoBJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ2Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoBJ2Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ2Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoBJ2Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ3Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ2Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoBJ3Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ3Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ2Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoBJ3Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ4Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ3Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoBJ4Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ4Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ3Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoBJ4Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ5Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ4Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoBJ5Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ5Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ4Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoBJ5Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ6Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ5Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoBJ6Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ6Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ5Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoBJ6Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                FaseGruposTabControl.SelectedTab = GrupoCTabPage;
                GrupoCJ1Placar1CurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ6Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoCJ1Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoCJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FaseGruposTabControl.SelectedTab = GrupoBTabPage;
                GrupoBJ6Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoCJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoCJ2Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoCJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoCJ2Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoCJ2Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoCJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoCJ2Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoCJ3Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoCJ2Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoCJ3Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoCJ3Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoCJ2Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoCJ3Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoCJ4Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoCJ3Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoCJ4Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoCJ4Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoCJ3Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoCJ4Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoCJ5Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoCJ4Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoCJ5Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoCJ5Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoCJ4Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoCJ5Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoCJ6Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoCJ5Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoCJ6Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoCJ6Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoCJ5Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoCJ6Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                FaseGruposTabControl.SelectedTab = GrupoDTabPage;
                GrupoDJ1Placar1CurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoCJ6Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoDJ1Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoDJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FaseGruposTabControl.SelectedTab = GrupoCTabPage;
                GrupoCJ6Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoDJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoDJ2Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoDJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoDJ2Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoDJ2Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoDJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoDJ2Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoDJ3Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoDJ2Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoDJ3Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoDJ3Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoDJ2Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoDJ3Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoDJ4Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoDJ3Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoDJ4Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoDJ4Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoDJ3Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoDJ4Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoDJ5Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoDJ4Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoDJ5Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoDJ5Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoDJ4Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoDJ5Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoDJ6Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoDJ5Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoDJ6Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoDJ6Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoDJ5Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoDJ6Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                FaseGruposTabControl.SelectedTab = GrupoETabPage;
                GrupoEJ1Placar1CurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoDJ6Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoEJ1Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoEJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FaseGruposTabControl.SelectedTab = GrupoDTabPage;
                GrupoDJ6Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoEJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoEJ2Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoEJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoEJ2Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoEJ2Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoEJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoEJ2Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoEJ3Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoEJ2Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoEJ3Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoEJ3Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoEJ2Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoEJ3Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoEJ4Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoEJ3Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoEJ4Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoEJ4Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoEJ3Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoEJ4Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoEJ5Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoEJ4Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoEJ5Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoEJ5Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoEJ4Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoEJ5Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoEJ6Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoEJ5Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoEJ6Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoEJ6Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoEJ5Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoEJ6Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                FaseGruposTabControl.SelectedTab = GrupoFTabPage;
                GrupoFJ1Placar1CurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoEJ6Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoFJ1Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoFJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FaseGruposTabControl.SelectedTab = GrupoETabPage;
                GrupoEJ6Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoFJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoFJ2Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoFJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoFJ2Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoFJ2Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoFJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoFJ2Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoFJ3Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoFJ2Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoFJ3Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoFJ3Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoFJ2Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoFJ3Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoFJ4Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoFJ3Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoFJ4Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoFJ4Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoFJ3Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoFJ4Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoFJ5Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoFJ4Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoFJ5Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoFJ5Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoFJ4Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoFJ5Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoFJ6Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoFJ5Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoFJ6Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoFJ6Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoFJ5Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoFJ6Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                FaseGruposTabControl.SelectedTab = GrupoGTabPage;
                GrupoGJ1Placar1CurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoFJ6Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoGJ1Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoGJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FaseGruposTabControl.SelectedTab = GrupoFTabPage;
                GrupoFJ6Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoGJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoGJ2Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoGJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoGJ2Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoGJ2Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoGJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoGJ2Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoGJ3Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoGJ2Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoGJ3Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoGJ3Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoGJ2Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoGJ3Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoGJ4Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoGJ3Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoGJ4Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoGJ4Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoGJ3Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoGJ4Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoGJ5Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoGJ4Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoGJ5Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoGJ5Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoGJ4Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoGJ5Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoGJ6Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoGJ5Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoGJ6Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoGJ6Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoGJ5Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoGJ6Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                FaseGruposTabControl.SelectedTab = GrupoHTabPage;
                GrupoHJ1Placar1CurrencyTextBox.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoGJ6Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoHJ1Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoHJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FaseGruposTabControl.SelectedTab = GrupoGTabPage;
                GrupoGJ6Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoHJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoHJ2Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoHJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoHJ2Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoHJ2Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoHJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoHJ2Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoHJ3Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoHJ2Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoHJ3Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoHJ3Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoHJ2Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoHJ3Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoHJ4Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoHJ3Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoHJ4Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoHJ4Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoHJ3Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoHJ4Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoHJ5Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoHJ4Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoHJ5Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoHJ5Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoHJ4Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoHJ5Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoHJ6Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoHJ5Placar1CurrencyTextBox.Focus();
            }
        }

        private void GrupoHJ6Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoHJ6Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoHJ5Placar2CurrencyTextBox.Focus();
            }
        }

        private void GrupoHJ6Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (OitavasTabPage.Enabled)
                {
                    tabControlAdv1.SelectedTab = OitavasTabPage;
                    OitavaJ1Placar1CurrencyTextBox.Focus();
                }
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoHJ6Placar1CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ1Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tabControlAdv1.SelectedTab = FaseGruposTabPage;
                FaseGruposTabControl.SelectedTab = OitavasTabPage;
                GrupoHJ6Placar2CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ2Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ2Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ2Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ2Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ3Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ2Placar1CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ3Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ3Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ2Placar2CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ3Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ4Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ2Placar1CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ4Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ4Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ3Placar2CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ4Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ5Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ4Placar1CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ5Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ5Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ4Placar2CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ5Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ6Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ5Placar1CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ6Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ6Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ5Placar2CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ6Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ7Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ6Placar1CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ7Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ7Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ6Placar2CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ7Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ8Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ7Placar1CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ8Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                OitavaJ8Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ7Placar2CurrencyTextBox.Focus();
            }
        }

        private void OitavaJ8Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (QuartasTabPage.TabVisible)
                {
                    tabControlAdv1.SelectedTab = QuartasTabPage;
                    QuartasJ1Placar1CurrencyTextBox.Focus();
                }
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                OitavaJ8Placar1CurrencyTextBox.Focus();
            }
        }

        private void QuartasJ1Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                QuartasJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tabControlAdv1.SelectedTab = OitavasTabPage;
                OitavaJ8Placar1CurrencyTextBox.Focus();
            }
        }

        private void QuartasJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                QuartasJ2Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                QuartasJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void QuartasJ2Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                QuartasJ2Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                QuartasJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void QuartasJ2Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                QuartasJ3Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                QuartasJ2Placar1CurrencyTextBox.Focus();
            }
        }

        private void QuartasJ3Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                QuartasJ3Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                QuartasJ2Placar2CurrencyTextBox.Focus();
            }
        }

        private void QuartasJ3Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                QuartasJ4Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                QuartasJ3Placar1CurrencyTextBox.Focus();
            }
        }

        private void QuartasJ4Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                QuartasJ4Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                QuartasJ3Placar2CurrencyTextBox.Focus();
            }
        }

        private void QuartasJ4Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (SemiTabPage.TabVisible)
                {
                    tabControlAdv1.SelectedTab = SemiTabPage;
                    SemiJ1Placar1CurrencyTextBox.Focus();
                }
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                QuartasJ4Placar1CurrencyTextBox.Focus();
            }
        }

        private void SemiJ1Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SemiJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tabControlAdv1.SelectedTab = QuartasTabPage;
                QuartasJ4Placar2CurrencyTextBox.Focus();
            }
        }

        private void SemiJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SemiJ2Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SemiJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void SemiJ2Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                SemiJ2Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SemiJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void SemiJ2Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (TerceiroTabPage.TabVisible)
                {
                    tabControlAdv1.SelectedTab = TerceiroTabPage;
                    TerceiroJ1Placar1CurrencyTextBox.Focus();
                }
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                SemiJ2Placar1CurrencyTextBox.Focus();
            }
        }

        private void TerceiroJ1Placar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                TerceiroJ1Placar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tabControlAdv1.SelectedTab =  SemiTabPage;
                SemiJ2Placar2CurrencyTextBox.Focus();
            }
        }

        private void TerceiroJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (FinalTabPage.TabVisible)
                {
                    tabControlAdv1.SelectedTab = FinalTabPage;
                    FinalPlacar1CurrencyTextBox.Focus();
                }
                else
                    gravarButton.Focus();
            }
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                TerceiroJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void FinalPlacar1CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                FinalPlacar2CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                tabControlAdv1.SelectedTab = TerceiroTabPage;
                TerceiroJ1Placar2CurrencyTextBox.Focus();
            }
        }

        private void FinalPlacar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                FinalPlacar1CurrencyTextBox.Focus();
            }
        }
    }
}
