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

namespace Suportte.Biblioteca
{
    public partial class Leitor : Form
    {
        public Leitor()
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

        public string _arquivo;
        public int _width = 0;
        public int _heigth = 0;
        public int _top = 0;
        public int _left = 0;
        Classes.Leitura _leitura;
        Classes.Livros _livros;
        Classes.EmprestimoLivros _emprestimo;

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
            if (!Publicas._chamouPelaTeladeLivros)
            {
                if (_leitura == null)
                    _leitura = new Leitura();

                _leitura.IdColaborador = Publicas._idColaborador;
                _leitura.IdLivros = Publicas._idLivro;
                _leitura.Pagina = pdfViewerControl.CurrentPageIndex;
                _leitura.UltimoAcesso = DateTime.Now;
                _leitura.TotalPagina = pdfViewerControl.PageCount;

                new LivrosBO().GravarLeitura(_leitura);

                string[] _dadosEmail = new string[50];

                _dadosEmail[0] = _livros.Nome;
                _dadosEmail[1] = "Você parou sua leitura na página " + _leitura.Pagina + ".";
                _dadosEmail[2] = "A data para finalizar a leitura é <b>" + _emprestimo.DataAcompanhamento.ToShortDateString() + "</b>. Se precisar de mais tempo informe o RH.";
                _dadosEmail[4] = new LivrosBO().QuantidadeLivros(false).ToString();
                _dadosEmail[5] = new LivrosBO().Sugestoes(false);
                _dadosEmail[6] = new LivrosBO().QuantidadeLivros(true).ToString();
                _dadosEmail[7] = new LivrosBO().Sugestoes(true);

                List<ResenhaLivros> _pontos = new ResenhaLivrosBO().ListarPontuacao();

                int classificacao = 0;
                int _pontuacao = 0;
                Colaboradores _colaborador = new ColaboradoresBO().ConsultaColaborador(Publicas._idColaborador);

                foreach (var item in _pontos.OrderBy(o => o.Classificacao))
                {
                    if (item.NomeColaborador == _colaborador.Nome)
                    {
                        classificacao = item.Classificacao;
                        _pontuacao = item.Pontuacao;
                        break;
                    }
                }

                if (classificacao != 0)
                    _dadosEmail[8] = "Você está em <b>" + classificacao.ToString() + "º</b> lugar com <b>" + _pontuacao.ToString() + "</b> pontos.";
                else
                    _dadosEmail[8] = "Você ainda não possui pontuação.";

                Publicas.EnviarEmailBiblioteca(_dadosEmail, new ColaboradoresBO().EmailDoColaborado(_colaborador.Id), "");
            }
            pdfViewerControl.Unload();
            Close();
        }

        private void Leitor_Shown(object sender, EventArgs e)
        {
            _width = this.Width;
            _heigth = this.Height;
            _top = this.Top;
            _left = this.Left;

            if (!Publicas._chamouPelaTeladeLivros)
            {
                _leitura = new LivrosBO().ConsultarLeitura(Publicas._idLivro);
                _livros = new LivrosBO().Consultar(_leitura.IdLivros);
                _emprestimo = new EmprestimoLivrosBO().Consultar(_leitura.IdLivros, Publicas._idColaborador);

                try
                {
                    if (_leitura.Pagina > 0)
                        pdfViewerControl.GoToPageAtIndex(_leitura.Pagina);
                }
                catch { }
            }
        }

        private void tituloMaximizarLabel_Click(object sender, EventArgs e)
        {
            ToolTipInfo _tool = superToolTip1.GetToolTip(tituloMaximizarLabel);

            if (_width == this.Width)
            {
                this.Size = new Size(Publicas._widthTelaInicial - 5, Publicas._heigthTelaInicial - (Publicas._heigthTituloTelaInicial + Publicas._heigthBarraTelaInicial + 2));
                this.Left = 0;
                this.Location = new Point(this.Left, 60);
                tituloMaximizarLabel.Text = "1";

                _tool.Footer.Text = "Restaura tamanho Original";
            }
            else
            {
                this.Size = new Size(_width, _heigth);
                this.Location = new Point(_left, _top);
                tituloMaximizarLabel.Text = "2";
                _tool.Footer.Text = "Maximiza";
            }

            superToolTip1.SetToolTip(tituloMaximizarLabel, _tool);
        }

        private void tituloMinimizarLabel_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Leitor_Load(object sender, EventArgs e)
        {
            pdfViewerControl.Load(_arquivo);
        }
    }
}
