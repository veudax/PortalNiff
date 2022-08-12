using Classes;
using Negocio;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Chart;
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

namespace Suportte.GestaoEstrategica
{
    public partial class Graficos : Form
    {
        public Graficos()
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
                    foreach (Control item in detalhePanel.Controls)
                    {
                        if (item is CurrencyTextBox)
                        {
                            ((CurrencyTextBox)item).DecimalValue = 0;
                            ((CurrencyTextBox)item).Tag = null;
                            ((CurrencyTextBox)item).PositiveColor = Publicas._fonte;
                            ((CurrencyTextBox)item).NegativeColor = Publicas._fonte;
                        }
                    }
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        public Classes.Empresa _empresaGrafico;
        public int _indicador;
        public int _ano;
        decimal _maiorDesempenho = 175;

        string _arquivoAmarelo = @"Imagens\Amarelo_Solido.png";
        string _arquivoVerde = @"Imagens\Verde_Solido.png";
        string _arquivoVermelho = @"Imagens\Vermelho_Solido.png";

        string _arquivoMelhorIgual = @"Imagens\melhor_igual.png";
        string _arquivoMelhorBaixo = @"Imagens\melhor_baixo.png";
        string _arquivoMelhorAcima = @"Imagens\melhor_cima.png";

        Classes.Metas _metas;
        List<Classes.ValoresPorMes> _valores;
        ChartAxis secYAxis = new ChartAxis();

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

        private void Graficos_Shown(object sender, EventArgs e)
        {
            _metas = new MetasBO().Consultar(_indicador);

            if (_metas.Regra == Publicas.RegraFormulaMetas.Igual)
                RegraPictureBox.ImageLocation = _arquivoMelhorIgual;
            else
            {
                if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                    RegraPictureBox.ImageLocation = _arquivoMelhorAcima;
                else
                    RegraPictureBox.ImageLocation = _arquivoMelhorBaixo;
            }

            this.Location = new Point(this.Left, 60);
            tituloLabel.Text = "Gráfico - " + _metas.Descricao + " - " + _empresaGrafico.NomeAbreviado;

            _valores = new List<ValoresPorMes>();
            _valores.Add(new ValoresPorMes() { Tipo = "Previsto" });
            _valores.Add(new ValoresPorMes() { Tipo = "Realizado" });
            _valores.Add(new ValoresPorMes() { Tipo = "Desvio" });
            _valores.Add(new ValoresPorMes() { Tipo = "Farol" });
            _valores.Add(new ValoresPorMes() { Tipo = "Desempenho" });


            int _mes = 1;
            while (_mes <= 12)
            {
                List<Classes.ValoresDasMetas> _valor = new MetasBO().Listar(false, _empresaGrafico.IdEmpresa, 
                                                                            _ano.ToString() + _mes.ToString("00"), _ano.ToString() + _mes.ToString("00"), _indicador);
                foreach (var itemV in _valor)
                {
                    switch (_mes)
                    {
                        case 1:
                            JanPrevTextLabel.DecimalValue = itemV.Previsto;
                            JanRealTextLabel.DecimalValue = itemV.Realizado;
                            JanDesviolTextLabel.DecimalValue = itemV.Previsto - itemV.Realizado;
                            
                            try
                            {
                                if (itemV.Previsto < 0 && itemV.Realizado < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        JanDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            JanDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                JanDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                JanDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        JanDesempenholTextLabel.DecimalValue = (itemV.Realizado / itemV.Previsto) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            JanDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                JanDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                JanDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (_maiorDesempenho < JanDesempenholTextLabel.DecimalValue)
                                _maiorDesempenho = JanDesempenholTextLabel.DecimalValue;

                            if (JanDesempenholTextLabel.DecimalValue >= 100 || (itemV.Realizado == 0 && itemV.Previsto > 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                                JanFarolPictureBox.ImageLocation = _arquivoVerde;
                            else
                            {
                                if (JanDesempenholTextLabel.DecimalValue >= 98 && JanDesempenholTextLabel.DecimalValue < 100)
                                    JanFarolPictureBox.ImageLocation = _arquivoAmarelo;
                                else
                                    JanFarolPictureBox.ImageLocation = _arquivoVermelho;
                            }
                            JanPrevistoAcumuladoTextBox.DecimalValue = JanPrevTextLabel.DecimalValue;
                            JanRealizadoAcumuladoTextBox.DecimalValue = JanRealTextLabel.DecimalValue;
                            JanDesvioAcumuladoTextBox.DecimalValue = JanDesviolTextLabel.DecimalValue;
                            JanDesempenhoAcumuladoTextBox.DecimalValue = JanDesempenholTextLabel.DecimalValue;
                            JanFarolAcumuladoPictureBox.ImageLocation = JanFarolPictureBox.ImageLocation;
                            break;
                        case 2:
                            FevPrevTextLabel.DecimalValue = itemV.Previsto;
                            FevRealTextLabel.DecimalValue = itemV.Realizado;
                            FevDesviolTextLabel.DecimalValue = itemV.Previsto - itemV.Realizado;
                            try
                            {
                                if (itemV.Previsto < 0 && itemV.Realizado < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        FevDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            FevDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                FevDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                FevDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        FevDesempenholTextLabel.DecimalValue = (itemV.Realizado / itemV.Previsto) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            FevDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                FevDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                FevDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                            }
                            catch { }
                            if (_maiorDesempenho < FevDesempenholTextLabel.DecimalValue)
                                _maiorDesempenho = FevDesempenholTextLabel.DecimalValue;

                            if (FevDesempenholTextLabel.DecimalValue >= 100 || (itemV.Realizado == 0 && itemV.Previsto > 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                                FevFarolPictureBox.ImageLocation = _arquivoVerde; 
                            else
                            if (FevDesempenholTextLabel.DecimalValue >= 98 && FevDesempenholTextLabel.DecimalValue < 100)
                                FevFarolPictureBox.ImageLocation = _arquivoAmarelo;
                            else
                                FevFarolPictureBox.ImageLocation = _arquivoVermelho;

                            FevPrevistoAcumuladoTextBox.DecimalValue = JanPrevTextLabel.DecimalValue + FevPrevTextLabel.DecimalValue;
                            FevRealizadoAcumuladoTextBox.DecimalValue = JanRealTextLabel.DecimalValue + FevRealTextLabel.DecimalValue;
                            FevDesvioAcumuladoTextBox.DecimalValue = FevPrevistoAcumuladoTextBox.DecimalValue - FevRealizadoAcumuladoTextBox.DecimalValue;
                            try
                            {
                                if (FevRealizadoAcumuladoTextBox.DecimalValue < 0 && FevPrevistoAcumuladoTextBox.DecimalValue < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        FevDesempenhoAcumuladoTextBox.DecimalValue = (FevPrevistoAcumuladoTextBox.DecimalValue / FevRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            FevDesempenhoAcumuladoTextBox.DecimalValue = (FevPrevistoAcumuladoTextBox.DecimalValue / FevRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (FevPrevistoAcumuladoTextBox.DecimalValue == FevRealizadoAcumuladoTextBox.DecimalValue)
                                                FevDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                FevDesempenhoAcumuladoTextBox.DecimalValue = 100 - FevRealizadoAcumuladoTextBox.DecimalValue;
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        FevDesempenhoAcumuladoTextBox.DecimalValue = (FevRealizadoAcumuladoTextBox.DecimalValue / FevPrevistoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            FevDesempenhoAcumuladoTextBox.DecimalValue = (FevPrevistoAcumuladoTextBox.DecimalValue / FevRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (FevPrevistoAcumuladoTextBox.DecimalValue == FevRealizadoAcumuladoTextBox.DecimalValue)
                                                FevDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                FevDesempenhoAcumuladoTextBox.DecimalValue = 100 - FevRealizadoAcumuladoTextBox.DecimalValue;
                                        }
                                    }
                                }
                            }
                            catch { }
                            if (FevDesempenhoAcumuladoTextBox.DecimalValue >= 100)
                                FevFarolAcumuladoPictureBox.ImageLocation = _arquivoVerde;
                            else
                            {
                                if (FevDesempenhoAcumuladoTextBox.DecimalValue >= 98 && FevDesempenhoAcumuladoTextBox.DecimalValue < 100)
                                    FevFarolAcumuladoPictureBox.ImageLocation = _arquivoAmarelo;
                                else
                                    FevFarolAcumuladoPictureBox.ImageLocation = _arquivoVermelho;
                            }
                            break;
                        case 3:
                            MarPrevTextLabel.DecimalValue = itemV.Previsto;
                            MarRealTextLabel.DecimalValue = itemV.Realizado;
                            MarDesviolTextLabel.DecimalValue = itemV.Previsto - itemV.Realizado;
                            try
                            {
                                if (itemV.Previsto < 0 && itemV.Realizado < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        MarDesempenholTextLabel.DecimalValue = (itemV.Previsto/ itemV.Realizado) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            MarDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                MarDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                MarDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        MarDesempenholTextLabel.DecimalValue = (itemV.Realizado / itemV.Previsto) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            MarDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                MarDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                MarDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (_maiorDesempenho < MarDesempenholTextLabel.DecimalValue)
                                _maiorDesempenho = MarDesempenholTextLabel.DecimalValue;

                            if (MarDesempenholTextLabel.DecimalValue >= 100 || (itemV.Realizado == 0 && itemV.Previsto > 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                                MarFarolPictureBox.ImageLocation = _arquivoVerde;
                            else
                            if (MarDesempenholTextLabel.DecimalValue >= 98 && MarDesempenholTextLabel.DecimalValue < 100)
                                MarFarolPictureBox.ImageLocation = _arquivoAmarelo;
                            else
                                MarFarolPictureBox.ImageLocation = _arquivoVermelho;

                            MarPrevistoAcumuladoTextBox.DecimalValue = JanPrevTextLabel.DecimalValue + FevPrevTextLabel.DecimalValue + MarPrevTextLabel.DecimalValue;
                            MarRealizadoAcumuladoTextBox.DecimalValue = JanRealTextLabel.DecimalValue + FevRealTextLabel.DecimalValue + MarRealTextLabel.DecimalValue;
                            MarDesvioAcumuladoTextBox.DecimalValue = MarPrevistoAcumuladoTextBox.DecimalValue - MarRealizadoAcumuladoTextBox.DecimalValue;

                            try
                            {
                                if (MarPrevistoAcumuladoTextBox.DecimalValue < 0 && MarRealizadoAcumuladoTextBox.DecimalValue < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        MarDesempenhoAcumuladoTextBox.DecimalValue = (MarPrevistoAcumuladoTextBox.DecimalValue / MarRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            MarDesempenhoAcumuladoTextBox.DecimalValue = (MarPrevistoAcumuladoTextBox.DecimalValue / MarRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (MarPrevistoAcumuladoTextBox.DecimalValue == MarRealizadoAcumuladoTextBox.DecimalValue)
                                                MarDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                MarDesempenhoAcumuladoTextBox.DecimalValue = (100 - MarRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        MarDesempenhoAcumuladoTextBox.DecimalValue = (MarRealizadoAcumuladoTextBox.DecimalValue / MarPrevistoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            MarDesempenhoAcumuladoTextBox.DecimalValue = (MarPrevistoAcumuladoTextBox.DecimalValue / MarRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (MarPrevistoAcumuladoTextBox.DecimalValue == MarRealizadoAcumuladoTextBox.DecimalValue)
                                                MarDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                MarDesempenhoAcumuladoTextBox.DecimalValue = (100 - MarRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (MarDesempenhoAcumuladoTextBox.DecimalValue >= 100)
                                MarFarolAcumuladoPictureBox.ImageLocation = _arquivoVerde;
                            else
                            {
                                if (MarDesempenhoAcumuladoTextBox.DecimalValue >= 98 && MarDesempenhoAcumuladoTextBox.DecimalValue < 100)
                                    MarFarolAcumuladoPictureBox.ImageLocation = _arquivoAmarelo;
                                else
                                    MarFarolAcumuladoPictureBox.ImageLocation = _arquivoVermelho;
                            }
                            break; 
                        case 4:
                            AbrPrevTextLabel.DecimalValue = itemV.Previsto;
                            AbrRealTextLabel.DecimalValue = itemV.Realizado;
                            AbrDesviolTextLabel.DecimalValue = itemV.Previsto - itemV.Realizado;

                            try
                            {
                                if (itemV.Previsto < 0 && itemV.Realizado < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        AbrDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            AbrDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                AbrDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                AbrDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        AbrDesempenholTextLabel.DecimalValue = (itemV.Realizado / itemV.Previsto) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            AbrDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                AbrDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                AbrDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (_maiorDesempenho < AbrDesempenholTextLabel.DecimalValue)
                                _maiorDesempenho = AbrDesempenholTextLabel.DecimalValue;

                            if (AbrDesempenholTextLabel.DecimalValue >= 100 || (itemV.Realizado == 0 && itemV.Previsto > 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                                AbrFarolPictureBox.ImageLocation = _arquivoVerde;
                            else
                            if (AbrDesempenholTextLabel.DecimalValue >= 98 && AbrDesempenholTextLabel.DecimalValue < 100)
                                AbrFarolPictureBox.ImageLocation = _arquivoAmarelo;
                            else
                                AbrFarolPictureBox.ImageLocation = _arquivoVermelho;

                            AbrPrevistoAcumuladoTextBox.DecimalValue = JanPrevTextLabel.DecimalValue + FevPrevTextLabel.DecimalValue + MarPrevTextLabel.DecimalValue +
                                AbrPrevTextLabel.DecimalValue;
                            AbrRealizadoAcumuladoTextBox.DecimalValue = JanRealTextLabel.DecimalValue + FevRealTextLabel.DecimalValue + MarRealTextLabel.DecimalValue +
                                AbrRealTextLabel.DecimalValue;
                            AbrDesvioAcumuladoTextBox.DecimalValue = AbrPrevistoAcumuladoTextBox.DecimalValue - AbrRealizadoAcumuladoTextBox.DecimalValue;

                            try
                            {
                                if (AbrPrevistoAcumuladoTextBox.DecimalValue < 0 && AbrRealizadoAcumuladoTextBox.DecimalValue < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        AbrDesempenhoAcumuladoTextBox.DecimalValue = (AbrPrevistoAcumuladoTextBox.DecimalValue / AbrRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            AbrDesempenhoAcumuladoTextBox.DecimalValue = (AbrPrevistoAcumuladoTextBox.DecimalValue / AbrRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (AbrPrevistoAcumuladoTextBox.DecimalValue == AbrRealizadoAcumuladoTextBox.DecimalValue)
                                                AbrDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                AbrDesempenhoAcumuladoTextBox.DecimalValue = 100 - AbrRealizadoAcumuladoTextBox.DecimalValue;
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        AbrDesempenhoAcumuladoTextBox.DecimalValue = (AbrRealizadoAcumuladoTextBox.DecimalValue / AbrPrevistoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            AbrDesempenhoAcumuladoTextBox.DecimalValue = (AbrPrevistoAcumuladoTextBox.DecimalValue / AbrRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (AbrPrevistoAcumuladoTextBox.DecimalValue == AbrRealizadoAcumuladoTextBox.DecimalValue)
                                                AbrDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                AbrDesempenhoAcumuladoTextBox.DecimalValue = 100 - AbrRealizadoAcumuladoTextBox.DecimalValue;
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (AbrDesempenhoAcumuladoTextBox.DecimalValue >= 100)
                                AbrFarolAcumuladoPictureBox.ImageLocation = _arquivoVerde;
                            else
                            {
                                if (AbrDesempenhoAcumuladoTextBox.DecimalValue >= 98 && AbrDesempenhoAcumuladoTextBox.DecimalValue < 100)
                                    AbrFarolAcumuladoPictureBox.ImageLocation = _arquivoAmarelo;
                                else
                                    AbrFarolAcumuladoPictureBox.ImageLocation = _arquivoVermelho;
                            }
                            break;
                        case 5:
                            MaiPrevTextLabel.DecimalValue = itemV.Previsto;
                            MaiRealTextLabel.DecimalValue = itemV.Realizado;
                            MaiDesviolTextLabel.DecimalValue = itemV.Previsto - itemV.Realizado;
                            try
                            {
                                if (itemV.Previsto < 0 && itemV.Realizado < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        MaiDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            MaiDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                MaiDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                MaiDesempenholTextLabel.DecimalValue = 100 - itemV.Realizado;
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        MaiDesempenholTextLabel.DecimalValue = (itemV.Realizado / itemV.Previsto) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            MaiDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                MaiDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                MaiDesempenholTextLabel.DecimalValue = 100 - itemV.Realizado;
                                        }
                                    }
                                }
                            }
                            catch { }
                            if (_maiorDesempenho < MaiDesempenholTextLabel.DecimalValue)
                                _maiorDesempenho = MaiDesempenholTextLabel.DecimalValue;

                            if (MaiDesempenholTextLabel.DecimalValue >= 100 || (itemV.Realizado == 0 && itemV.Previsto > 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                                MaiFarolPictureBox.ImageLocation = _arquivoVerde;
                            else
                            if (MaiDesempenholTextLabel.DecimalValue >= 98 && MaiDesempenholTextLabel.DecimalValue < 100)
                                MaiFarolPictureBox.ImageLocation = _arquivoAmarelo;
                            else
                                MaiFarolPictureBox.ImageLocation = _arquivoVermelho;

                            MaiPrevistoAcumuladoTextBox.DecimalValue = JanPrevTextLabel.DecimalValue + FevPrevTextLabel.DecimalValue + MarPrevTextLabel.DecimalValue +
                                AbrPrevTextLabel.DecimalValue + MaiPrevTextLabel.DecimalValue; 
                            MaiRealizadoAcumuladoTextBox.DecimalValue = JanRealTextLabel.DecimalValue + FevRealTextLabel.DecimalValue + MarRealTextLabel.DecimalValue +
                                AbrRealTextLabel.DecimalValue + MaiRealTextLabel.DecimalValue;
                            MaiDesvioAcumuladoTextBox.DecimalValue = MaiPrevistoAcumuladoTextBox.DecimalValue - MaiRealizadoAcumuladoTextBox.DecimalValue;
                            try
                            {
                                if (MaiPrevistoAcumuladoTextBox.DecimalValue < 0 && MaiRealizadoAcumuladoTextBox.DecimalValue < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        MaiDesempenhoAcumuladoTextBox.DecimalValue = (MaiPrevistoAcumuladoTextBox.DecimalValue / MaiRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            MaiDesempenhoAcumuladoTextBox.DecimalValue = (MaiPrevistoAcumuladoTextBox.DecimalValue / MaiRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (MaiPrevistoAcumuladoTextBox.DecimalValue == MaiRealizadoAcumuladoTextBox.DecimalValue)
                                                MaiDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                MaiDesempenhoAcumuladoTextBox.DecimalValue = 100 - MaiRealizadoAcumuladoTextBox.DecimalValue;
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        MaiDesempenhoAcumuladoTextBox.DecimalValue = (MaiRealizadoAcumuladoTextBox.DecimalValue / MaiPrevistoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            MaiDesempenhoAcumuladoTextBox.DecimalValue = (MaiPrevistoAcumuladoTextBox.DecimalValue / MaiRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (MaiPrevistoAcumuladoTextBox.DecimalValue == MaiRealizadoAcumuladoTextBox.DecimalValue)
                                                MaiDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                MaiDesempenhoAcumuladoTextBox.DecimalValue = 100 - MaiRealizadoAcumuladoTextBox.DecimalValue;
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (MaiDesempenhoAcumuladoTextBox.DecimalValue >= 100)
                                MaiFarolAcumuladoPictureBox.ImageLocation = _arquivoVerde;
                            else
                            {
                                if (MaiDesempenhoAcumuladoTextBox.DecimalValue >= 98 && MaiDesempenhoAcumuladoTextBox.DecimalValue < 100)
                                    MaiFarolAcumuladoPictureBox.ImageLocation = _arquivoAmarelo;
                                else
                                    MaiFarolAcumuladoPictureBox.ImageLocation = _arquivoVermelho;
                            }
                            break;
                        case 6:
                            JunPrevTextLabel.DecimalValue = itemV.Previsto;
                            JunRealTextLabel.DecimalValue = itemV.Realizado;
                            JunDesviolTextLabel.DecimalValue = itemV.Previsto - itemV.Realizado;
                            try
                            {
                                if (itemV.Previsto < 0 && itemV.Realizado < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        JunDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            JunDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                JunDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                JunDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        JunDesempenholTextLabel.DecimalValue = (itemV.Realizado / itemV.Previsto) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            JunDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                JunDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                JunDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                            }
                            catch { }
                            if (_maiorDesempenho < JunDesempenholTextLabel.DecimalValue)
                                _maiorDesempenho = JunDesempenholTextLabel.DecimalValue;

                            if (JunDesempenholTextLabel.DecimalValue >= 100 || (itemV.Realizado == 0 && itemV.Previsto > 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                                JunFarolPictureBox.ImageLocation = _arquivoVerde; 
                            else
                            if (JunDesempenholTextLabel.DecimalValue >= 98 && JunDesempenholTextLabel.DecimalValue < 100)
                                JunFarolPictureBox.ImageLocation = _arquivoAmarelo;
                            else
                                JunFarolPictureBox.ImageLocation = _arquivoVermelho;

                            JunPrevistoAcumuladoTextBox.DecimalValue = JanPrevTextLabel.DecimalValue + FevPrevTextLabel.DecimalValue + MarPrevTextLabel.DecimalValue +
                                AbrPrevTextLabel.DecimalValue + MaiPrevTextLabel.DecimalValue + JunPrevTextLabel.DecimalValue;
                            JunRealizadoAcumuladoTextBox.DecimalValue = JanRealTextLabel.DecimalValue + FevRealTextLabel.DecimalValue + MarRealTextLabel.DecimalValue +
                                AbrRealTextLabel.DecimalValue + MaiRealTextLabel.DecimalValue + JunRealTextLabel.DecimalValue;
                            JunDesvioAcumuladoTextBox.DecimalValue = JunPrevistoAcumuladoTextBox.DecimalValue - JunRealizadoAcumuladoTextBox.DecimalValue;
                            try
                            {
                                if (JunPrevistoAcumuladoTextBox.DecimalValue < 0 && JunRealizadoAcumuladoTextBox.DecimalValue < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        JunDesempenhoAcumuladoTextBox.DecimalValue = (JunPrevistoAcumuladoTextBox.DecimalValue / JunRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            JunDesempenhoAcumuladoTextBox.DecimalValue = (JunPrevistoAcumuladoTextBox.DecimalValue / JunRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (JunPrevistoAcumuladoTextBox.DecimalValue == JunRealizadoAcumuladoTextBox.DecimalValue)
                                                JunDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                JunDesempenhoAcumuladoTextBox.DecimalValue = (100 - JunRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        JunDesempenhoAcumuladoTextBox.DecimalValue = (JunRealizadoAcumuladoTextBox.DecimalValue / JunPrevistoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            JunDesempenhoAcumuladoTextBox.DecimalValue = (JunPrevistoAcumuladoTextBox.DecimalValue / JunRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (JunPrevistoAcumuladoTextBox.DecimalValue == JunRealizadoAcumuladoTextBox.DecimalValue)
                                                JunDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                JunDesempenhoAcumuladoTextBox.DecimalValue = (100 - JunRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                            }
                            catch { }
                            if (JunDesempenhoAcumuladoTextBox.DecimalValue >= 100)
                                JunFarolAcumuladoPictureBox.ImageLocation = _arquivoVerde;
                            else
                            {
                                if (JunDesempenhoAcumuladoTextBox.DecimalValue >= 98 && JunDesempenhoAcumuladoTextBox.DecimalValue < 100)
                                    JunFarolAcumuladoPictureBox.ImageLocation = _arquivoAmarelo;
                                else
                                    JunFarolAcumuladoPictureBox.ImageLocation = _arquivoVermelho;
                            }
                            break;
                        case 7:
                            JulPrevTextLabel.DecimalValue = itemV.Previsto;
                            JulRealTextLabel.DecimalValue = itemV.Realizado;
                            JulDesviolTextLabel.DecimalValue = itemV.Previsto - itemV.Realizado;
                            try
                            {
                                if (itemV.Previsto < 0 && itemV.Realizado < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        JulDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            JulDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                JulDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                JulDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        JulDesempenholTextLabel.DecimalValue = (itemV.Realizado / itemV.Previsto) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            JulDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                JulDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                JulDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                            }
                            catch { }
                            if (JulDesempenholTextLabel.DecimalValue >= 100 || (itemV.Realizado == 0 && itemV.Previsto > 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                                JulFarolPictureBox.ImageLocation = _arquivoVerde; 
                            else
                            if (JulDesempenholTextLabel.DecimalValue >= 98 && JulDesempenholTextLabel.DecimalValue < 100)
                                JulFarolPictureBox.ImageLocation = _arquivoAmarelo;
                            else
                                JulFarolPictureBox.ImageLocation = _arquivoVermelho;

                            if (_maiorDesempenho < JulDesempenholTextLabel.DecimalValue)
                                _maiorDesempenho = JulDesempenholTextLabel.DecimalValue;

                            JulPrevistoAcumuladoTextBox.DecimalValue = JanPrevTextLabel.DecimalValue + FevPrevTextLabel.DecimalValue + MarPrevTextLabel.DecimalValue +
                                AbrPrevTextLabel.DecimalValue + MaiPrevTextLabel.DecimalValue + JunPrevTextLabel.DecimalValue + 
                                JulPrevTextLabel.DecimalValue;
                            JulRealizadoAcumuladoTextBox.DecimalValue = JanRealTextLabel.DecimalValue + FevRealTextLabel.DecimalValue + MarRealTextLabel.DecimalValue +
                                AbrRealTextLabel.DecimalValue + MaiRealTextLabel.DecimalValue + JunRealTextLabel.DecimalValue +
                                JulRealTextLabel.DecimalValue;
                            JulDesvioAcumuladoTextBox.DecimalValue = JulPrevistoAcumuladoTextBox.DecimalValue - JulRealizadoAcumuladoTextBox.DecimalValue;

                            try
                            {
                                if (JulPrevistoAcumuladoTextBox.DecimalValue < 0 && JulRealizadoAcumuladoTextBox.DecimalValue < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        JulDesempenhoAcumuladoTextBox.DecimalValue = (JulPrevistoAcumuladoTextBox.DecimalValue / JulRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            JulDesempenhoAcumuladoTextBox.DecimalValue = (JulPrevistoAcumuladoTextBox.DecimalValue / JulRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (JulPrevistoAcumuladoTextBox.DecimalValue == JulRealizadoAcumuladoTextBox.DecimalValue)
                                                JulDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                JulDesempenhoAcumuladoTextBox.DecimalValue = (100 - JulRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        JulDesempenhoAcumuladoTextBox.DecimalValue = (JulRealizadoAcumuladoTextBox.DecimalValue / JulPrevistoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            JulDesempenhoAcumuladoTextBox.DecimalValue = (JulPrevistoAcumuladoTextBox.DecimalValue / JulRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (JulPrevistoAcumuladoTextBox.DecimalValue == JulRealizadoAcumuladoTextBox.DecimalValue)
                                                JulDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                JulDesempenhoAcumuladoTextBox.DecimalValue = (100 - JulRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                            }
                            catch { }
                            if (JulDesempenhoAcumuladoTextBox.DecimalValue >= 100)
                                JulFarolAcumuladoPictureBox.ImageLocation = _arquivoVerde;
                            else
                            {
                                if (JulDesempenhoAcumuladoTextBox.DecimalValue >= 98 && JulDesempenhoAcumuladoTextBox.DecimalValue < 100)
                                    JulFarolAcumuladoPictureBox.ImageLocation = _arquivoAmarelo;
                                else
                                    JulFarolAcumuladoPictureBox.ImageLocation = _arquivoVermelho;
                            }
                            break;
                        case 8:
                            AgoPrevTextLabel.DecimalValue = itemV.Previsto;
                            AgoRealTextLabel.DecimalValue = itemV.Realizado;
                            AgoDesviolTextLabel.DecimalValue = itemV.Previsto - itemV.Realizado;
                            try
                            {
                                if (itemV.Previsto < 0 && itemV.Realizado < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        AgoDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            AgoDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                AgoDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                AgoDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        AgoDesempenholTextLabel.DecimalValue = (itemV.Realizado / itemV.Previsto) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            AgoDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                AgoDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                AgoDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (_maiorDesempenho < AgoDesempenholTextLabel.DecimalValue)
                                _maiorDesempenho = AgoDesempenholTextLabel.DecimalValue;

                            if (AgoDesempenholTextLabel.DecimalValue >= 100 || (itemV.Realizado == 0 && itemV.Previsto > 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                                AgoFarolPictureBox.ImageLocation = _arquivoVerde;
                            else
                            if (AgoDesempenholTextLabel.DecimalValue >= 98 && AgoDesempenholTextLabel.DecimalValue < 100)
                                AgoFarolPictureBox.ImageLocation = _arquivoAmarelo;
                            else
                                AgoFarolPictureBox.ImageLocation = _arquivoVermelho;

                            AgoPrevistoAcumuladoTextBox.DecimalValue = JanPrevTextLabel.DecimalValue + FevPrevTextLabel.DecimalValue + MarPrevTextLabel.DecimalValue +
                                AbrPrevTextLabel.DecimalValue + MaiPrevTextLabel.DecimalValue + JunPrevTextLabel.DecimalValue +
                                JulPrevTextLabel.DecimalValue + AgoPrevTextLabel.DecimalValue;
                            AgoRealizadoAcumuladoTextBox.DecimalValue = JanRealTextLabel.DecimalValue + FevRealTextLabel.DecimalValue + MarRealTextLabel.DecimalValue +
                                AbrRealTextLabel.DecimalValue + MaiRealTextLabel.DecimalValue + JunRealTextLabel.DecimalValue +
                                JulRealTextLabel.DecimalValue + AgoRealTextLabel.DecimalValue;
                            AgoDesvioAcumuladoTextBox.DecimalValue = AgoPrevistoAcumuladoTextBox.DecimalValue - AgoRealizadoAcumuladoTextBox.DecimalValue;

                            try
                            {
                                if (AgoPrevistoAcumuladoTextBox.DecimalValue < 0 && AgoRealizadoAcumuladoTextBox.DecimalValue < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        AgoDesempenhoAcumuladoTextBox.DecimalValue = (AgoPrevistoAcumuladoTextBox.DecimalValue / AgoRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            AgoDesempenhoAcumuladoTextBox.DecimalValue = (AgoPrevistoAcumuladoTextBox.DecimalValue / AgoRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (AgoPrevistoAcumuladoTextBox.DecimalValue == AgoRealizadoAcumuladoTextBox.DecimalValue)
                                                AgoDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                AgoDesempenhoAcumuladoTextBox.DecimalValue = (100 - AgoRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        AgoDesempenhoAcumuladoTextBox.DecimalValue = (AgoRealizadoAcumuladoTextBox.DecimalValue / AgoPrevistoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            AgoDesempenhoAcumuladoTextBox.DecimalValue = (AgoPrevistoAcumuladoTextBox.DecimalValue / AgoRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (AgoPrevistoAcumuladoTextBox.DecimalValue == AgoRealizadoAcumuladoTextBox.DecimalValue)
                                                AgoDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                AgoDesempenhoAcumuladoTextBox.DecimalValue = (100 - AgoRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (AgoDesempenhoAcumuladoTextBox.DecimalValue >= 100)
                                AgoFarolAcumuladoPictureBox.ImageLocation = _arquivoVerde;
                            else
                            {
                                if (AgoDesempenhoAcumuladoTextBox.DecimalValue >= 98 && AgoDesempenhoAcumuladoTextBox.DecimalValue < 100)
                                    AgoFarolAcumuladoPictureBox.ImageLocation = _arquivoAmarelo;
                                else
                                    AgoFarolAcumuladoPictureBox.ImageLocation = _arquivoVermelho;
                            }
                            break;
                        case 9:
                            SetPrevTextLabel.DecimalValue = itemV.Previsto;
                            SetRealTextLabel.DecimalValue = itemV.Realizado;
                            SetDesviolTextLabel.DecimalValue = itemV.Previsto - itemV.Realizado;

                            try
                            {
                                if (itemV.Previsto < 0 && itemV.Realizado < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        SetDesempenholTextLabel.DecimalValue = ( itemV.Previsto / itemV.Realizado) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            SetDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                SetDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                SetDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        SetDesempenholTextLabel.DecimalValue = (itemV.Realizado / itemV.Previsto) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            SetDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                SetDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                SetDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (_maiorDesempenho < SetDesempenholTextLabel.DecimalValue)
                                _maiorDesempenho = SetDesempenholTextLabel.DecimalValue;

                            if (SetDesempenholTextLabel.DecimalValue >= 100 || (itemV.Realizado == 0 && itemV.Previsto > 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                                SetFarolPictureBox.ImageLocation = _arquivoVerde;
                            else
                            if (SetDesempenholTextLabel.DecimalValue >= 98 && SetDesempenholTextLabel.DecimalValue < 100)
                                SetFarolPictureBox.ImageLocation = _arquivoAmarelo;
                            else
                                SetFarolPictureBox.ImageLocation = _arquivoVermelho;

                            SetPrevistoAcumuladoTextBox.DecimalValue = JanPrevTextLabel.DecimalValue + FevPrevTextLabel.DecimalValue + MarPrevTextLabel.DecimalValue +
                                AbrPrevTextLabel.DecimalValue + MaiPrevTextLabel.DecimalValue + JunPrevTextLabel.DecimalValue +
                                JulPrevTextLabel.DecimalValue + AgoPrevTextLabel.DecimalValue + SetPrevTextLabel.DecimalValue;
                            SetRealizadoAcumuladoTextBox.DecimalValue = JanRealTextLabel.DecimalValue + FevRealTextLabel.DecimalValue + MarRealTextLabel.DecimalValue +
                                AbrRealTextLabel.DecimalValue + MaiRealTextLabel.DecimalValue + JunRealTextLabel.DecimalValue +
                                JulRealTextLabel.DecimalValue + AgoRealTextLabel.DecimalValue + SetRealTextLabel.DecimalValue;
                            SetDesvioAcumuladoTextBox.DecimalValue = SetPrevistoAcumuladoTextBox.DecimalValue - SetRealizadoAcumuladoTextBox.DecimalValue;

                            try
                            {
                                if (SetPrevistoAcumuladoTextBox.DecimalValue < 0 && SetRealizadoAcumuladoTextBox.DecimalValue < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        SetDesempenhoAcumuladoTextBox.DecimalValue = (SetPrevistoAcumuladoTextBox.DecimalValue / SetRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            SetDesempenhoAcumuladoTextBox.DecimalValue = (SetPrevistoAcumuladoTextBox.DecimalValue / SetRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (SetPrevistoAcumuladoTextBox.DecimalValue == SetRealizadoAcumuladoTextBox.DecimalValue)
                                                SetDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                SetDesempenhoAcumuladoTextBox.DecimalValue = (100 - SetRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        SetDesempenhoAcumuladoTextBox.DecimalValue = (SetRealizadoAcumuladoTextBox.DecimalValue / SetPrevistoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            SetDesempenhoAcumuladoTextBox.DecimalValue = (SetPrevistoAcumuladoTextBox.DecimalValue / SetRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (SetPrevistoAcumuladoTextBox.DecimalValue == SetRealizadoAcumuladoTextBox.DecimalValue)
                                                SetDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                SetDesempenhoAcumuladoTextBox.DecimalValue = (100 - SetRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (SetDesempenhoAcumuladoTextBox.DecimalValue >= 100)
                                SetFarolAcumuladoPictureBox.ImageLocation = _arquivoVerde;
                            else
                            {
                                if (SetDesempenhoAcumuladoTextBox.DecimalValue >= 98 && SetDesempenhoAcumuladoTextBox.DecimalValue < 100)
                                    SetFarolAcumuladoPictureBox.ImageLocation = _arquivoAmarelo;
                                else
                                    SetFarolAcumuladoPictureBox.ImageLocation = _arquivoVermelho;
                            }
                            break;
                        case 10:
                            OutPrevTextLabel.DecimalValue = itemV.Previsto;
                            OutRealTextLabel.DecimalValue = itemV.Realizado;
                            OutDesviolTextLabel.DecimalValue = itemV.Previsto - itemV.Realizado;
                            try
                            {
                                if (itemV.Previsto < 0 && itemV.Realizado < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        OutDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            OutDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                OutDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                OutDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        OutDesempenholTextLabel.DecimalValue = (itemV.Realizado / itemV.Previsto) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            OutDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                OutDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                OutDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                            }
                            catch { }
                            if (_maiorDesempenho < OutDesempenholTextLabel.DecimalValue)
                                _maiorDesempenho = OutDesempenholTextLabel.DecimalValue;

                            if (OutDesempenholTextLabel.DecimalValue >= 100 || (itemV.Realizado == 0 && itemV.Previsto > 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                                OutFarolPictureBox.ImageLocation = _arquivoVerde;
                            else
                            if (OutDesempenholTextLabel.DecimalValue >= 98 && OutDesempenholTextLabel.DecimalValue < 100)
                                OutFarolPictureBox.ImageLocation = _arquivoAmarelo;
                            else
                                OutFarolPictureBox.ImageLocation = _arquivoVermelho;

                            OutPrevistoAcumuladoTextBox.DecimalValue = JanPrevTextLabel.DecimalValue + FevPrevTextLabel.DecimalValue + MarPrevTextLabel.DecimalValue +
                                AbrPrevTextLabel.DecimalValue + MaiPrevTextLabel.DecimalValue + JunPrevTextLabel.DecimalValue +
                                JulPrevTextLabel.DecimalValue + AgoPrevTextLabel.DecimalValue + SetPrevTextLabel.DecimalValue +
                                OutPrevTextLabel.DecimalValue;
                            OutRealizadoAcumuladoTextBox.DecimalValue = JanRealTextLabel.DecimalValue + FevRealTextLabel.DecimalValue + MarRealTextLabel.DecimalValue +
                                AbrRealTextLabel.DecimalValue + MaiRealTextLabel.DecimalValue + JunRealTextLabel.DecimalValue +
                                JulRealTextLabel.DecimalValue + AgoRealTextLabel.DecimalValue + SetRealTextLabel.DecimalValue +
                                OutRealTextLabel.DecimalValue;
                            OutDesvioAcumuladoTextBox.DecimalValue = OutPrevistoAcumuladoTextBox.DecimalValue - OutRealizadoAcumuladoTextBox.DecimalValue;

                            try
                            {
                                if (OutPrevistoAcumuladoTextBox.DecimalValue < 0 && OutRealizadoAcumuladoTextBox.DecimalValue < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        OutDesempenhoAcumuladoTextBox.DecimalValue = (OutPrevistoAcumuladoTextBox.DecimalValue / OutRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            OutDesempenhoAcumuladoTextBox.DecimalValue = (OutPrevistoAcumuladoTextBox.DecimalValue / OutRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (OutPrevistoAcumuladoTextBox.DecimalValue == OutRealizadoAcumuladoTextBox.DecimalValue)
                                                OutDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                OutDesempenhoAcumuladoTextBox.DecimalValue = (100 - OutRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        OutDesempenhoAcumuladoTextBox.DecimalValue = (OutRealizadoAcumuladoTextBox.DecimalValue / OutPrevistoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            OutDesempenhoAcumuladoTextBox.DecimalValue = (OutPrevistoAcumuladoTextBox.DecimalValue / OutRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (OutPrevistoAcumuladoTextBox.DecimalValue == OutRealizadoAcumuladoTextBox.DecimalValue)
                                                OutDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                OutDesempenhoAcumuladoTextBox.DecimalValue = (100 - OutRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                            }
                            catch { }
                            if (OutDesempenhoAcumuladoTextBox.DecimalValue >= 100)
                                OutFarolAcumuladoPictureBox.ImageLocation = _arquivoVerde;
                            else
                            {
                                if (OutDesempenhoAcumuladoTextBox.DecimalValue >= 98 && OutDesempenhoAcumuladoTextBox.DecimalValue < 100)
                                    OutFarolAcumuladoPictureBox.ImageLocation = _arquivoAmarelo;
                                else
                                    OutFarolAcumuladoPictureBox.ImageLocation = _arquivoVermelho;
                            }
                            break;
                        case 11:
                            NovPrevTextLabel.DecimalValue = itemV.Previsto;
                            NovRealTextLabel.DecimalValue = itemV.Realizado;
                            NovDesviolTextLabel.DecimalValue = itemV.Previsto - itemV.Realizado;

                            try
                            {
                                if (itemV.Previsto < 0 && itemV.Realizado < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        NovDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            NovDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                NovDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                NovDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        NovDesempenholTextLabel.DecimalValue = (itemV.Realizado / itemV.Previsto) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            NovDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                NovDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                NovDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                            }
                            catch { }
                            if (_maiorDesempenho < NovDesempenholTextLabel.DecimalValue)
                                _maiorDesempenho = NovDesempenholTextLabel.DecimalValue;

                            if (NovDesempenholTextLabel.DecimalValue>= 100 || (itemV.Realizado == 0 && itemV.Previsto > 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                                NovFarolPictureBox.ImageLocation = _arquivoVerde;
                            else
                            if (NovDesempenholTextLabel.DecimalValue >= 98 && NovDesempenholTextLabel.DecimalValue < 100)
                                NovFarolPictureBox.ImageLocation = _arquivoAmarelo;
                            else
                                NovFarolPictureBox.ImageLocation = _arquivoVermelho;

                            NovPrevistoAcumuladoTextBox.DecimalValue = JanPrevTextLabel.DecimalValue + FevPrevTextLabel.DecimalValue + MarPrevTextLabel.DecimalValue +
                                AbrPrevTextLabel.DecimalValue + MaiPrevTextLabel.DecimalValue + JunPrevTextLabel.DecimalValue +
                                JulPrevTextLabel.DecimalValue + AgoPrevTextLabel.DecimalValue + SetPrevTextLabel.DecimalValue +
                                OutPrevTextLabel.DecimalValue + NovPrevTextLabel.DecimalValue;
                            NovRealizadoAcumuladoTextBox.DecimalValue = JanRealTextLabel.DecimalValue + FevRealTextLabel.DecimalValue + MarRealTextLabel.DecimalValue +
                                AbrRealTextLabel.DecimalValue + MaiRealTextLabel.DecimalValue + JunRealTextLabel.DecimalValue +
                                JulRealTextLabel.DecimalValue + AgoRealTextLabel.DecimalValue + SetRealTextLabel.DecimalValue +
                                OutRealTextLabel.DecimalValue + NovRealTextLabel.DecimalValue;
                            NovDesvioAcumuladoTextBox.DecimalValue = NovPrevistoAcumuladoTextBox.DecimalValue - NovRealizadoAcumuladoTextBox.DecimalValue;

                            try
                            {
                                if (NovPrevistoAcumuladoTextBox.DecimalValue < 0 && NovRealizadoAcumuladoTextBox.DecimalValue < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        NovDesempenhoAcumuladoTextBox.DecimalValue = (NovPrevistoAcumuladoTextBox.DecimalValue / NovRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            NovDesempenhoAcumuladoTextBox.DecimalValue = (NovPrevistoAcumuladoTextBox.DecimalValue / NovRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (NovPrevistoAcumuladoTextBox.DecimalValue == NovRealizadoAcumuladoTextBox.DecimalValue)
                                                NovDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                NovDesempenhoAcumuladoTextBox.DecimalValue = (100 - NovRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        NovDesempenhoAcumuladoTextBox.DecimalValue = (NovRealizadoAcumuladoTextBox.DecimalValue / NovPrevistoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            NovDesempenhoAcumuladoTextBox.DecimalValue = (NovPrevistoAcumuladoTextBox.DecimalValue / NovRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (NovPrevistoAcumuladoTextBox.DecimalValue == NovRealizadoAcumuladoTextBox.DecimalValue)
                                                NovDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                NovDesempenhoAcumuladoTextBox.DecimalValue = (100 - NovRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (NovDesempenhoAcumuladoTextBox.DecimalValue >= 100)
                                NovFarolAcumuladoPictureBox.ImageLocation = _arquivoVerde;
                            else
                            {
                                if (NovDesempenhoAcumuladoTextBox.DecimalValue >= 98 && NovDesempenhoAcumuladoTextBox.DecimalValue < 100)
                                    NovFarolAcumuladoPictureBox.ImageLocation = _arquivoAmarelo;
                                else
                                    NovFarolAcumuladoPictureBox.ImageLocation = _arquivoVermelho;
                            }
                            break;
                        case 12:
                            DezPrevTextLabel.DecimalValue = itemV.Previsto;
                            DezRealTextLabel.DecimalValue = itemV.Realizado;
                            DezDesviolTextLabel.DecimalValue = itemV.Previsto - itemV.Realizado;

                            try
                            {
                                if (itemV.Previsto < 0 && itemV.Realizado < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        DezDesempenholTextLabel.DecimalValue = (itemV.Previsto/ itemV.Realizado) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            DezDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                DezDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                DezDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        DezDesempenholTextLabel.DecimalValue = (itemV.Realizado / itemV.Previsto) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            DezDesempenholTextLabel.DecimalValue = (itemV.Previsto / itemV.Realizado) * 100;
                                        else
                                        {
                                            if (itemV.Previsto == itemV.Realizado)
                                                DezDesempenholTextLabel.DecimalValue = 100;
                                            else
                                                DezDesempenholTextLabel.DecimalValue = (100 - itemV.Realizado);
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (_maiorDesempenho < DezDesempenholTextLabel.DecimalValue)
                                _maiorDesempenho = DezDesempenholTextLabel.DecimalValue;

                            if (DezDesempenholTextLabel.DecimalValue >= 100 || (itemV.Realizado == 0 && itemV.Previsto > 0 && _metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor))
                                DezFarolPictureBox.ImageLocation = _arquivoVerde; 
                            else
                            if (DezDesempenholTextLabel.DecimalValue >= 98 && DezDesempenholTextLabel.DecimalValue < 100)
                                DezFarolPictureBox.ImageLocation = _arquivoAmarelo;
                            else
                                DezFarolPictureBox.ImageLocation = _arquivoVermelho;

                            DezPrevistoAcumuladoTextBox.DecimalValue = JanPrevTextLabel.DecimalValue + FevPrevTextLabel.DecimalValue + MarPrevTextLabel.DecimalValue +
                                AbrPrevTextLabel.DecimalValue + MaiPrevTextLabel.DecimalValue + JunPrevTextLabel.DecimalValue +
                                JulPrevTextLabel.DecimalValue + AgoPrevTextLabel.DecimalValue + SetPrevTextLabel.DecimalValue +
                                OutPrevTextLabel.DecimalValue + NovPrevTextLabel.DecimalValue + DezPrevTextLabel.DecimalValue;
                            DezRealizadoAcumuladoTextBox.DecimalValue = JanRealTextLabel.DecimalValue + FevRealTextLabel.DecimalValue + MarRealTextLabel.DecimalValue +
                                AbrRealTextLabel.DecimalValue + MaiRealTextLabel.DecimalValue + JunRealTextLabel.DecimalValue +
                                JulRealTextLabel.DecimalValue + AgoRealTextLabel.DecimalValue + SetRealTextLabel.DecimalValue +
                                OutRealTextLabel.DecimalValue + NovRealTextLabel.DecimalValue + DezRealTextLabel.DecimalValue;
                            DezDesvioAcumuladoTextBox.DecimalValue = DezPrevistoAcumuladoTextBox.DecimalValue - DezRealizadoAcumuladoTextBox.DecimalValue;

                            try
                            {
                                if (DezPrevistoAcumuladoTextBox.DecimalValue < 0 && DezRealizadoAcumuladoTextBox.DecimalValue < 0)
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        DezDesempenhoAcumuladoTextBox.DecimalValue = ( DezPrevistoAcumuladoTextBox.DecimalValue / DezRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            DezDesempenhoAcumuladoTextBox.DecimalValue = (DezPrevistoAcumuladoTextBox.DecimalValue / DezRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (DezPrevistoAcumuladoTextBox.DecimalValue == DezRealizadoAcumuladoTextBox.DecimalValue)
                                                DezDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                DezDesempenhoAcumuladoTextBox.DecimalValue = (100 - DezRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                                else
                                {
                                    if (_metas.Regra == Publicas.RegraFormulaMetas.MaiorMelhor)
                                        DezDesempenhoAcumuladoTextBox.DecimalValue = (DezRealizadoAcumuladoTextBox.DecimalValue / DezPrevistoAcumuladoTextBox.DecimalValue) * 100;
                                    else
                                    {
                                        if (_metas.Regra == Publicas.RegraFormulaMetas.MenorMelhor)
                                            DezDesempenhoAcumuladoTextBox.DecimalValue = (DezPrevistoAcumuladoTextBox.DecimalValue / DezRealizadoAcumuladoTextBox.DecimalValue) * 100;
                                        else
                                        {
                                            if (DezPrevistoAcumuladoTextBox.DecimalValue == DezRealizadoAcumuladoTextBox.DecimalValue)
                                                DezDesempenhoAcumuladoTextBox.DecimalValue = 100;
                                            else
                                                DezDesempenhoAcumuladoTextBox.DecimalValue = (100 - DezRealizadoAcumuladoTextBox.DecimalValue);
                                        }
                                    }
                                }
                            }
                            catch { }

                            if (DezDesempenhoAcumuladoTextBox.DecimalValue >= 100)
                                DezFarolAcumuladoPictureBox.ImageLocation = _arquivoVerde;
                            else
                            {
                                if (DezDesempenhoAcumuladoTextBox.DecimalValue >= 98 && DezDesempenhoAcumuladoTextBox.DecimalValue < 100)
                                    DezFarolAcumuladoPictureBox.ImageLocation = _arquivoAmarelo;
                                else
                                    DezFarolAcumuladoPictureBox.ImageLocation = _arquivoVermelho;
                            }
                            break;
                    }
                }
                _mes++;
            }

            AcumuladoRadioButton_CheckChanged(sender, e);
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected void ChartControlSeries_PrepareStyle(object sender, ChartPrepareStyleInfoEventArgs args)
        {
            ChartSeries series = sender as ChartSeries;
            Random rand = new Random();

            try
            {
                if (args.Index == 0)
                {
                    if (JanFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (JanFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 1)
                {
                    if (FevFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (FevFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 2)
                {
                    if (MarFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (MarFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 3)
                {
                    if (AbrFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (AbrFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 4)
                {
                    if (MaiFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (MaiFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 5)
                {
                    if (JunFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (JunFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 6)
                {
                    if (JulFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (JulFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 7)
                {
                    if (AgoFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (AgoFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 8)
                {
                    if (SetFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (SetFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 9)
                {
                    if (OutFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (OutFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 10)
                {
                    if (NovFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (NovFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 11)
                {
                    if (DezFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (DezFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
            }
            catch { }
        }

        protected void ChartControlSeries_AcuPrepareStyle(object sender, ChartPrepareStyleInfoEventArgs args)
        {
            ChartSeries series = sender as ChartSeries;
            Random rand = new Random();

            try
            {
                if (args.Index == 0)
                {
                    if (JanFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (JanFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 1)
                {
                    if (FevFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (FevFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 2)
                {
                    if (MarFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (MarFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 3)
                {
                    if (AbrFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (AbrFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 4)
                {
                    if (MaiFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (MaiFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 5)
                {
                    if (JunFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (JunFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 6)
                {
                    if (JulFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (JulFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 7)
                {
                    if (AgoFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (AgoFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 8)
                {
                    if (SetFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (SetFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 9)
                {
                    if (OutFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (OutFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 10)
                {
                    if (NovFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (NovFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
                if (args.Index == 11)
                {
                    if (DezFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Symbol.Color = Color.Green;
                    else
                    {
                        if (DezFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Symbol.Color = Color.Red;
                        else
                            args.Style.Symbol.Color = Color.Yellow;
                    }
                }
            }
            catch { }
        }

        protected void ChartControlSeries_BarrasPrepareStyle(object sender, ChartPrepareStyleInfoEventArgs args)
        {
            ChartSeries series = sender as ChartSeries;
            Random rand = new Random();

            try
            {
                if (args.Index == 0)
                {
                    if (JanFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (JanFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 1)
                {
                    if (FevFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (FevFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 2)
                {
                    if (MarFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (MarFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 3)
                {
                    if (AbrFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (AbrFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 4)
                {
                    if (MaiFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (MaiFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 5)
                {
                    if (JunFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (JunFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 6)
                {
                    if (JulFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (JulFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 7)
                {
                    if (AgoFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (AgoFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 8)
                {
                    if (SetFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (SetFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 9)
                {
                    if (OutFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (OutFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 10)
                {
                    if (NovFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (NovFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 11)
                {
                    if (DezFarolPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (DezFarolPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
            }
            catch { }
        }

        protected void ChartControlSeries_AcuBarrasPrepareStyle(object sender, ChartPrepareStyleInfoEventArgs args)
        {
            ChartSeries series = sender as ChartSeries;
            Random rand = new Random();

            try
            {
                if (args.Index == 0)
                {
                    if (JanFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (JanFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 1)
                {
                    if (FevFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (FevFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 2)
                {
                    if (MarFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (MarFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 3)
                {
                    if (AbrFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (AbrFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 4)
                {
                    if (MaiFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (MaiFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 5)
                {
                    if (JunFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (JunFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 6)
                {
                    if (JulFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (JulFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 7)
                {
                    if (AgoFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (AgoFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 8)
                {
                    if (SetFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (SetFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 9)
                {
                    if (OutFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (OutFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 10)
                {
                    if (NovFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (NovFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
                if (args.Index == 11)
                {
                    if (DezFarolAcumuladoPictureBox.ImageLocation.Contains("Verde"))
                        args.Style.Interior = new BrushInfo(Color.Green);
                    else
                    {
                        if (DezFarolAcumuladoPictureBox.ImageLocation.Contains("Vermelho"))
                            args.Style.Interior = new BrushInfo(Color.Red);
                        else
                            args.Style.Interior = new BrushInfo(Color.Yellow);
                    }
                }
            }
            catch { }
        }

        private void AcumuladoRadioButton_CheckChanged(object sender, EventArgs e)
        {
            GraficoChartControl.BackInterior = new Syncfusion.Drawing.BrushInfo(GraficoPanel.BackColor);
            GraficoChartControl.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(GraficoPanel.BackColor);
            GraficoChartControl.PrimaryXAxis.ValueType = ChartValueType.Category;

            GraficoChartControl.Skins = Syncfusion.Windows.Forms.Chart.Skins.Metro;
            GraficoChartControl.Palette = ChartColorPalette.Custom;
            GraficoChartControl.Series3D = false;
            GraficoChartControl.ShowLegend = true;
            GraficoChartControl.Legend.Position = ChartDock.Bottom;
            GraficoChartControl.LegendsPlacement = ChartPlacement.Outside;
            GraficoChartControl.Indexed = false;
            GraficoChartControl.PrimaryYAxis.Margin = true;

            GraficoChartControl.ChartArea.YAxesLayoutMode = ChartAxesLayoutMode.Stacking;

            ChartSeries _previstoSerie;
            _previstoSerie = new ChartSeries("Previsto", ChartSeriesType.Line);
            _previstoSerie.Style.Symbol.Shape = ChartSymbolShape.Circle;
            _previstoSerie.Style.Symbol.Color = GraficoChartControl.CustomPalette[0];
            _previstoSerie.SortPoints = false;
            _previstoSerie.Style.Border.Width = 3;
            if (NormalRadioButton.Checked)
            {
                _previstoSerie.Points.Add("Jan", (double)JanPrevTextLabel.DecimalValue);
                _previstoSerie.Points.Add("Fev", (double)FevPrevTextLabel.DecimalValue);
                _previstoSerie.Points.Add("Mar", (double)MarPrevTextLabel.DecimalValue);
                _previstoSerie.Points.Add("Abr", (double)AbrPrevTextLabel.DecimalValue);
                _previstoSerie.Points.Add("Mai", (double)MaiPrevTextLabel.DecimalValue);
                _previstoSerie.Points.Add("Jun", (double)JunPrevTextLabel.DecimalValue);
                _previstoSerie.Points.Add("Jul", (double)JulPrevTextLabel.DecimalValue);
                _previstoSerie.Points.Add("Ago", (double)AgoPrevTextLabel.DecimalValue);
                _previstoSerie.Points.Add("Set", (double)SetPrevTextLabel.DecimalValue);
                _previstoSerie.Points.Add("Out", (double)OutPrevTextLabel.DecimalValue);
                _previstoSerie.Points.Add("Nov", (double)NovPrevTextLabel.DecimalValue);
                _previstoSerie.Points.Add("Dez", (double)DezPrevTextLabel.DecimalValue);
            }
            else
            {
                _previstoSerie.Points.Add("Jan", (double)JanPrevistoAcumuladoTextBox.DecimalValue);
                _previstoSerie.Points.Add("Fev", (double)FevPrevistoAcumuladoTextBox.DecimalValue);
                _previstoSerie.Points.Add("Mar", (double)MarPrevistoAcumuladoTextBox.DecimalValue);
                _previstoSerie.Points.Add("Abr", (double)AbrPrevistoAcumuladoTextBox.DecimalValue);
                _previstoSerie.Points.Add("Mai", (double)MaiPrevistoAcumuladoTextBox.DecimalValue);
                _previstoSerie.Points.Add("Jun", (double)JunPrevistoAcumuladoTextBox.DecimalValue);
                _previstoSerie.Points.Add("Jul", (double)JulPrevistoAcumuladoTextBox.DecimalValue);
                _previstoSerie.Points.Add("Ago", (double)AgoPrevistoAcumuladoTextBox.DecimalValue);
                _previstoSerie.Points.Add("Set", (double)SetPrevistoAcumuladoTextBox.DecimalValue);
                _previstoSerie.Points.Add("Out", (double)OutPrevistoAcumuladoTextBox.DecimalValue);
                _previstoSerie.Points.Add("Nov", (double)NovPrevistoAcumuladoTextBox.DecimalValue);
                _previstoSerie.Points.Add("Dez", (double)DezPrevistoAcumuladoTextBox.DecimalValue);
            }

            ChartSeries _desempenhoSerie;
            _desempenhoSerie = new ChartSeries("Desempenho", ChartSeriesType.Line);
            _desempenhoSerie.Style.Symbol.Shape = ChartSymbolShape.Hexagon;
            _desempenhoSerie.Style.Symbol.Color = GraficoChartControl.CustomPalette[1];
            _desempenhoSerie.Style.Border.Width = 3;
            _desempenhoSerie.SortPoints = false;
            if (NormalRadioButton.Checked)
            {
                _desempenhoSerie.PrepareStyle += new ChartPrepareStyleInfoHandler(ChartControlSeries_PrepareStyle);
                _desempenhoSerie.Points.Add("Jan", (double)JanDesempenholTextLabel.DecimalValue);
                _desempenhoSerie.Points.Add("Fev", (double)FevDesempenholTextLabel.DecimalValue);
                _desempenhoSerie.Points.Add("Mar", (double)MarDesempenholTextLabel.DecimalValue);
                _desempenhoSerie.Points.Add("Abr", (double)AbrDesempenholTextLabel.DecimalValue);
                _desempenhoSerie.Points.Add("Mai", (double)MaiDesempenholTextLabel.DecimalValue);
                _desempenhoSerie.Points.Add("Jun", (double)JunDesempenholTextLabel.DecimalValue);
                _desempenhoSerie.Points.Add("Jul", (double)JulDesempenholTextLabel.DecimalValue);
                _desempenhoSerie.Points.Add("Ago", (double)AgoDesempenholTextLabel.DecimalValue);
                _desempenhoSerie.Points.Add("Set", (double)SetDesempenholTextLabel.DecimalValue);
                _desempenhoSerie.Points.Add("Out", (double)OutDesempenholTextLabel.DecimalValue);
                _desempenhoSerie.Points.Add("Nov", (double)NovDesempenholTextLabel.DecimalValue);
                _desempenhoSerie.Points.Add("Dez", (double)DezDesempenholTextLabel.DecimalValue);
            }
            else
            {
                _desempenhoSerie.PrepareStyle += new ChartPrepareStyleInfoHandler(ChartControlSeries_AcuPrepareStyle);
                _desempenhoSerie.Points.Add("Jan", (double)JanDesempenhoAcumuladoTextBox.DecimalValue);
                _desempenhoSerie.Points.Add("Fev", (double)FevDesempenhoAcumuladoTextBox.DecimalValue);
                _desempenhoSerie.Points.Add("Mar", (double)MarDesempenhoAcumuladoTextBox.DecimalValue);
                _desempenhoSerie.Points.Add("Abr", (double)AbrDesempenhoAcumuladoTextBox.DecimalValue);
                _desempenhoSerie.Points.Add("Mai", (double)MaiDesempenhoAcumuladoTextBox.DecimalValue);
                _desempenhoSerie.Points.Add("Jun", (double)JunDesempenhoAcumuladoTextBox.DecimalValue);
                _desempenhoSerie.Points.Add("Jul", (double)JulDesempenhoAcumuladoTextBox.DecimalValue);
                _desempenhoSerie.Points.Add("Ago", (double)AgoDesempenhoAcumuladoTextBox.DecimalValue);
                _desempenhoSerie.Points.Add("Set", (double)SetDesempenhoAcumuladoTextBox.DecimalValue);
                _desempenhoSerie.Points.Add("Out", (double)OutDesempenhoAcumuladoTextBox.DecimalValue);
                _desempenhoSerie.Points.Add("Nov", (double)NovDesempenhoAcumuladoTextBox.DecimalValue);
                _desempenhoSerie.Points.Add("Dez", (double)DezDesempenhoAcumuladoTextBox.DecimalValue);
            }

            ChartSeries _realizadoSerie;
            _realizadoSerie = new ChartSeries("Realizado", ChartSeriesType.Column);
            _realizadoSerie.SortPoints = false;
            if (NormalRadioButton.Checked)
            {
                _realizadoSerie.PrepareStyle += new ChartPrepareStyleInfoHandler(ChartControlSeries_BarrasPrepareStyle);
                _realizadoSerie.Points.Add("Jan", (double)JanRealTextLabel.DecimalValue);
                _realizadoSerie.Points.Add("Fev", (double)FevRealTextLabel.DecimalValue);
                _realizadoSerie.Points.Add("Mar", (double)MarRealTextLabel.DecimalValue);
                _realizadoSerie.Points.Add("Abr", (double)AbrRealTextLabel.DecimalValue);
                _realizadoSerie.Points.Add("Mai", (double)MaiRealTextLabel.DecimalValue);
                _realizadoSerie.Points.Add("Jun", (double)JunRealTextLabel.DecimalValue);
                _realizadoSerie.Points.Add("Jul", (double)JulRealTextLabel.DecimalValue);
                _realizadoSerie.Points.Add("Ago", (double)AgoRealTextLabel.DecimalValue);
                _realizadoSerie.Points.Add("Set", (double)SetRealTextLabel.DecimalValue);
                _realizadoSerie.Points.Add("Out", (double)OutRealTextLabel.DecimalValue);
                _realizadoSerie.Points.Add("Nov", (double)NovRealTextLabel.DecimalValue);
                _realizadoSerie.Points.Add("Dez", (double)DezRealTextLabel.DecimalValue);
            }
            else
            {
                _realizadoSerie.PrepareStyle += new ChartPrepareStyleInfoHandler(ChartControlSeries_AcuBarrasPrepareStyle);
                _realizadoSerie.Points.Add("Jan", (double)JanRealizadoAcumuladoTextBox.DecimalValue);
                _realizadoSerie.Points.Add("Fev", (double)FevRealizadoAcumuladoTextBox.DecimalValue);
                _realizadoSerie.Points.Add("Mar", (double)MarRealizadoAcumuladoTextBox.DecimalValue);
                _realizadoSerie.Points.Add("Abr", (double)AbrRealizadoAcumuladoTextBox.DecimalValue);
                _realizadoSerie.Points.Add("Mai", (double)MaiRealizadoAcumuladoTextBox.DecimalValue);
                _realizadoSerie.Points.Add("Jun", (double)JunRealizadoAcumuladoTextBox.DecimalValue);
                _realizadoSerie.Points.Add("Jul", (double)JulRealizadoAcumuladoTextBox.DecimalValue);
                _realizadoSerie.Points.Add("Ago", (double)AgoRealizadoAcumuladoTextBox.DecimalValue);
                _realizadoSerie.Points.Add("Set", (double)SetRealizadoAcumuladoTextBox.DecimalValue);
                _realizadoSerie.Points.Add("Out", (double)OutRealizadoAcumuladoTextBox.DecimalValue);
                _realizadoSerie.Points.Add("Nov", (double)NovRealizadoAcumuladoTextBox.DecimalValue);
                _realizadoSerie.Points.Add("Dez", (double)DezRealizadoAcumuladoTextBox.DecimalValue);
            }

            GraficoChartControl.Series.Clear();
            GraficoChartControl.Series.Add(_previstoSerie);
            GraficoChartControl.Series.Add(_desempenhoSerie);
            GraficoChartControl.Series.Add(_realizadoSerie);

            try
            {
                GraficoChartControl.Axes.Remove(secYAxis);
            }
            catch { }

            
            secYAxis.DrawGrid = false;
            secYAxis.Range = new MinMaxInfo(0, (int)_maiorDesempenho + 25, 25);
            secYAxis.HidePartialLabels = true;
            secYAxis.OpposedPosition = true;
            secYAxis.ValueType = ChartValueType.Double;
            secYAxis.LabelIntersectAction = ChartLabelIntersectAction.Rotate;
            secYAxis.Orientation = ChartOrientation.Vertical;
            secYAxis.GridLineType.ForeColor = Color.Transparent;
            secYAxis.GridLineType.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;

            try
            {
                GraficoChartControl.Axes.Remove(secYAxis);
            }
            catch { }

            GraficoChartControl.Axes.Add(secYAxis);
            _desempenhoSerie.YAxis = secYAxis;
            GraficoChartControl.ChartArea.YAxesLayoutMode = ChartAxesLayoutMode.Stacking;

            GraficoChartControl.Model.ColorModel.AllowGradient = true;
        }
    }
}
