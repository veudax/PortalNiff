using Classes;
using Negocio;
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

namespace Suportte.BolaoCopadoMundo
{
    public partial class ResultadosJogos : Form
    {
        public ResultadosJogos()
        {
            InitializeComponent();

            DataJogoMaskedEditBox.BorderColor = Publicas._bordaSaida;
            DataJogoMaskedEditBox.BackColor = GrupoBJ1DescricaoSelecao1TextBox.BackColor;

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        Classes.BolaoJogos _jogos;
        Classes.BolaoTimes _selecao1;
        Classes.BolaoTimes _selecao2;
        Classes.BolaoPalpiteFinalDoColaborador _palpitesFinal;
        List<Classes.BolaoPalpitesDosColaboradores> _palpites;
        List<Classes.BolaoPontuacao> _listaDePontuacao;

        int _exatos;
        int _ganhador;
        int _ganhador1Placar;
        int _1placar;
        int _campeao;
        int _vice;
        int _3Lugar;

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DataJogoMaskedEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                GrupoBJ1Placar1CurrencyTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                DataJogoMaskedEditBox.Focus();
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
                DataJogoMaskedEditBox.Focus();
            }
        }

        private void GrupoBJ1Placar2CurrencyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ1Placar1CurrencyTextBox.Focus();
            }
        }

        private void gravarButton_KeyDown(object sender, KeyEventArgs e)
        {
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                GrupoBJ1Placar2CurrencyTextBox.Focus();
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

        private void DataJogoMaskedEditBox_Enter(object sender, EventArgs e)
        {
            ((MaskedEditBox)sender).BorderColor = Publicas._bordaEntrada;
            pesquisaReferenciaButton.Enabled = string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim());
        }

        private void GrupoBJ1Placar1CurrencyTextBox_Enter(object sender, EventArgs e)
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


        private void pesquisaReferenciaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim()))
            {
                Publicas._jogosNaoFinalizados = true;
                new Pesquisas.Jogos().ShowDialog();

                DataJogoMaskedEditBox.Text = Publicas._codigoRetornoPesquisa;

                if (string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim()) || DataJogoMaskedEditBox.ClipText.Trim() == "0")
                {
                    DataJogoMaskedEditBox.Text = string.Empty;
                    DataJogoMaskedEditBox.Focus();
                    return;
                }

                DataJogoMaskedEditBox_Validating(sender, new CancelEventArgs());
            }
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            _jogos.Placar1 = (int)GrupoBJ1Placar1CurrencyTextBox.DecimalValue;
            _jogos.Placar2 = (int)GrupoBJ1Placar2CurrencyTextBox.DecimalValue;
            _jogos.Penalti1 = (int)Penalti1CurrencyText.DecimalValue;
            _jogos.Penalti2 = (int)Penalt2CurrencyText.DecimalValue;

            _jogos.Encerrado = true;

            if (!new BolaoJogosBO().Gravar(_jogos))
            {
                new Notificacoes.Mensagem("Problemas durante a gravar." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            // efetuar comparação dos resultados
            _listaDePontuacao = new BolaoPontuacaoBO().Listar(DateTime.Now.Year);

            foreach (var item in _listaDePontuacao)
            {
                if (item.Nome == "Exato")
                    _exatos = item.Ponto;
                if (item.Nome == "Ganhador")
                    _ganhador = item.Ponto;
                if (item.Nome == "Ganhador + 1 Placar")
                    _ganhador1Placar = item.Ponto;
                if (item.Nome == "1 Placar")
                    _1placar = item.Ponto;
                if (item.Nome == "Campeão")
                    _campeao = item.Ponto;
                if (item.Nome == "Vice")
                    _vice = item.Ponto;
                if (item.Nome == "3º Lugar")
                    _3Lugar = item.Ponto;
            }
            _palpites = new BolaoPalpitesDosColaboradoresBO().Listar(_jogos.Id);

            foreach (var item in _palpites)
            {
                //Acertou o Placar 
                if (_jogos.Placar1 == item.Placar1 && _jogos.Placar2 == item.Placar2)
                    item.Pontuacao = _exatos;
                else
                {
                    // Acertou que era Empates porém errou o placar
                    if (_jogos.Placar1 == _jogos.Placar2 && item.Placar1 == item.Placar2)
                        item.Pontuacao = _ganhador;
                    else
                    {
                        if (_jogos.Placar1 > _jogos.Placar2)
                        {
                            // Acertou o Ganhar e 1 placar certo
                            if (item.Placar1 > item.Placar2 && (_jogos.Placar1 == item.Placar1 || _jogos.Placar2 == item.Placar2))
                                item.Pontuacao = _ganhador1Placar;
                            else
                            {
                                // Acertou apenas o Ganhador
                                if ((item.Placar1 > item.Placar2))
                                    item.Pontuacao = _ganhador;
                                else
                                {
                                    if (_jogos.Placar1 == item.Placar1 || _jogos.Placar2 == item.Placar2)
                                        item.Pontuacao = _1placar;
                                    else
                                        item.Pontuacao = 0;
                                }
                            }
                        }
                        else
                        if (_jogos.Placar2 > _jogos.Placar1)
                        {
                            // Acertou o Ganhar e 1 placar certo
                            if (item.Placar2 > item.Placar1 && (_jogos.Placar1 == item.Placar1 || _jogos.Placar2 == item.Placar2))
                                item.Pontuacao = _ganhador1Placar;
                            else
                            {
                                // Acertou apenas o Ganhador
                                if ((item.Placar2 > item.Placar1))
                                    item.Pontuacao = _ganhador;
                                else
                                {
                                    if (_jogos.Placar1 == item.Placar1 || _jogos.Placar2 == item.Placar2)
                                        item.Pontuacao = _1placar;
                                    else
                                        item.Pontuacao = 0;
                                }
                            }
                        }
                        else
                        {
                            // apenas um resultado certo
                            if (_jogos.Placar1 == item.Placar1 || _jogos.Placar2 == item.Placar2)
                                item.Pontuacao = _1placar;
                            else
                            {
                                item.Pontuacao = 0;
                            }
                        }
                    }
                }

                if (_jogos.Fase == "3L" || _jogos.Fase == "FN")
                {
                    _palpitesFinal = new BolaoPalpiteFinalDoColaboradorBO().Consultar(item.IdColaborador, DateTime.Now.Year);

                    if (_jogos.Fase == "3L")
                    {

                        if (((_jogos.Placar1 > _jogos.Placar2 || _jogos.Penalti1 > _jogos.Penalti2) &&
                            (_palpitesFinal.IdTime3Lugar == _jogos.IdTime1)) ||
                            ((_jogos.Placar2 > _jogos.Placar1 || _jogos.Penalti2 > _jogos.Penalti1) &&
                             (_palpitesFinal.IdTime3Lugar == _jogos.IdTime2)))
                        {
                            _palpitesFinal.Acertou3Lugar = true;
                            _palpitesFinal.Pontuacao = _palpitesFinal.Pontuacao + _3Lugar;
                        }
                    }

                    if (_jogos.Fase == "FN")
                    {
                        if (((_jogos.Placar1 > _jogos.Placar2 || _jogos.Penalti1 > _jogos.Penalti2) &&
                            (_palpitesFinal.IdTimeCampeao == _jogos.IdTime1)) ||
                            ((_jogos.Placar2 > _jogos.Placar1 || _jogos.Penalti2 > _jogos.Penalti1) &&
                             (_palpitesFinal.IdTimeCampeao == _jogos.IdTime2)))
                        {
                            _palpitesFinal.AcertouCampeao = true;
                            _palpitesFinal.Pontuacao = _palpitesFinal.Pontuacao + _campeao;
                        }
                        else
                        {
                            if (((_jogos.Placar1 < _jogos.Placar2 || _jogos.Penalti1 < _jogos.Penalti2) &&
                            (_palpitesFinal.IdTimeVice == _jogos.IdTime1)) ||
                            ((_jogos.Placar2 < _jogos.Placar1 || _jogos.Penalti2 < _jogos.Penalti1) &&
                             (_palpitesFinal.IdTimeVice == _jogos.IdTime2)))
                            {
                                _palpitesFinal.AcertouViceCampeao = true;
                                _palpitesFinal.Pontuacao = _palpitesFinal.Pontuacao + _vice;
                            }
                        }
                    }

                    if (_palpitesFinal != null)
                    {
                        if (!new BolaoPalpiteFinalDoColaboradorBO().GravarPontuacao(_palpitesFinal))
                        {
                            new Notificacoes.Mensagem("Problemas durante a gravar a pontuação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                            return;
                        }
                    }
                }
            }

            if (!new BolaoPalpitesDosColaboradoresBO().GravarPontuacao(_palpites))
            {
                new Notificacoes.Mensagem("Problemas durante a gravar a pontuação." + Environment.NewLine + Publicas.mensagemDeErro, Publicas.TipoMensagem.Erro).ShowDialog();
                return;
            }

            


            limparButton_Click(sender, e);
        }

        private void limparButton_Click(object sender, EventArgs e)
        {
            DataJogoMaskedEditBox.Text = string.Empty;
            GrupoBJ1DescricaoSelecao1TextBox.Text = string.Empty;
            GrupoBJ1DescricaoSelecao2TextBoxExt.Text = string.Empty;
            GrupoBJ1Bandeira1PictureBox.Image = null;
            GrupoBJ1Bandeira2PictureBox.Image = null;
            GrupoBJ1Placar1CurrencyTextBox.DecimalValue = 0;
            GrupoBJ1Placar2CurrencyTextBox.DecimalValue = 0;
            DataJogoMaskedEditBox.Focus();

            gravarButton.Enabled = false;
        }
        
        private void DataJogoMaskedEditBox_Validating(object sender, CancelEventArgs e)
        {
            DataJogoMaskedEditBox.BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                DataJogoMaskedEditBox.Text = string.Empty;
                pesquisaReferenciaButton.Enabled = false;
                Publicas._escTeclado = false;
                return;
            }

            Publicas._codigoRetornoPesquisa = "";

            if (string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim()))
            {
                Publicas._jogosNaoFinalizados = true;
                new Pesquisas.Jogos().ShowDialog();

                Publicas._dataPesquisa = DateTime.MinValue;
                DataJogoMaskedEditBox.Text = Publicas._codigoRetornoPesquisa;
                
                if (string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim()) || DataJogoMaskedEditBox.ClipText.Trim() == "0")
                {
                    DataJogoMaskedEditBox.Text = string.Empty;
                    DataJogoMaskedEditBox.Focus();
                    return;
                }
            }

            Publicas._jogosNaoFinalizados = false;
            List<BolaoJogos> _lista = new BolaoJogosBO().Listar(Convert.ToDateTime(DataJogoMaskedEditBox.Text.Trim()));

            if (_lista.Count() > 1)
            {
                Publicas._dataPesquisa = Convert.ToDateTime(DataJogoMaskedEditBox.Text.Trim());

                new Pesquisas.Jogos().ShowDialog();

                if (Publicas._idRetornoPesquisa == 0)
                {
                    DataJogoMaskedEditBox.Text = string.Empty;
                    DataJogoMaskedEditBox.Focus();
                    return;
                }

                _jogos = new BolaoJogosBO().Consultar(Publicas._idRetornoPesquisa);

                DataJogoMaskedEditBox.Text = _jogos.Data.ToString();

                if (string.IsNullOrEmpty(DataJogoMaskedEditBox.ClipText.Trim()) || DataJogoMaskedEditBox.ClipText.Trim() == "0")
                {
                    DataJogoMaskedEditBox.Text = string.Empty;
                    DataJogoMaskedEditBox.Focus();
                    return;
                }

                PopulaCampos();
                return;
            }

            _jogos = new BolaoJogosBO().Consultar(Convert.ToDateTime(DataJogoMaskedEditBox.Text.Trim()));

            PopulaCampos();

            Publicas._dataPesquisa = DateTime.MinValue;

        }

        private void PopulaCampos()
        {
            if (!_jogos.Existe)
            {
                new Notificacoes.Mensagem("Jogo não cadastrado.", Publicas.TipoMensagem.Confirmacao).ShowDialog();
                DataJogoMaskedEditBox.Focus();
                return;
            }


            _selecao1 = new BolaoTimesBO().Consultar(_jogos.IdTime1);
            GrupoBJ1DescricaoSelecao1TextBox.Text = _selecao1.Nome;
            GrupoBJ1Placar1CurrencyTextBox.DecimalValue = _jogos.Placar1;
            GrupoBJ1Placar2CurrencyTextBox.DecimalValue = _jogos.Placar2;
            Penalti1CurrencyText.DecimalValue = _jogos.Penalti1;
            Penalt2CurrencyText.DecimalValue = _jogos.Penalti2;

            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    mStream.Write(_selecao1.Bandeira, 0, _selecao1.Bandeira.Length);
                    mStream.Seek(0, SeekOrigin.Begin);

                    GrupoBJ1Bandeira1PictureBox.Image = new Bitmap(mStream);
                }

                GrupoBJ1Bandeira1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                GrupoBJ1Bandeira1PictureBox.Refresh();
            }
            catch { }

            _selecao2 = new BolaoTimesBO().Consultar(_jogos.IdTime2);
            GrupoBJ1DescricaoSelecao2TextBoxExt.Text = _selecao2.Nome;

            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    mStream.Write(_selecao2.Bandeira, 0, _selecao2.Bandeira.Length);
                    mStream.Seek(0, SeekOrigin.Begin);

                    GrupoBJ1Bandeira2PictureBox.Image = new Bitmap(mStream);
                }

                GrupoBJ1Bandeira2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                GrupoBJ1Bandeira2PictureBox.Refresh();
            }
            catch { }

            gravarButton.Enabled = true;
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
    }
}
