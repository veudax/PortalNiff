using Classes;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.Chamados
{
    public partial class Editar : Form
    {
        public Editar()
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

        Classes.HistoricoDoChamado _historico;
        Classes.Chamado _chamados;
        public int _idHistorico;
        public int _idChamado;

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Editar_Shown(object sender, EventArgs e)
        {
            _historico = new ChamadoBO().Consultar(_idHistorico);

            descricaoTextBox.Text = _historico.Descricao;
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            Log _log = new Log();
            _log.IdUsuario = Publicas._usuario.Id;
            _log.Descricao =
                "Alterou o tramite do chamado " + numeroTextBoxExt.Text +
                " de [" + _historico.Descricao + "] para [" + descricaoTextBox.Text + "]" +
                (_historico.Adequacao == numeroAdequacaoTextBox.Text ? "" : " Número Adequação de [" + _historico.Adequacao + "] para [" + numeroAdequacaoTextBox.Text + "]") +
                (_historico.Prazo.ToString() == PrazoEntregaTextBox.Text ? "" : " Prazo de [" + _historico.Prazo + "] para [" + PrazoEntregaTextBox.Text + "]");

            _log.Tela = "Tramites - Chamado";

            if (numeroAdequacaoTextBox.Visible && _historico.Adequacao != numeroAdequacaoTextBox.Text && !string.IsNullOrEmpty(numeroAdequacaoTextBox.Text.Trim()))
            {
                _chamados = new ChamadoBO().Consulta(_idChamado);
                _chamados.Adequacao = tipoAdequacaoComboBox.Text + " " + numeroAdequacaoTextBox.Text;
                try
                {
                    _chamados.PrazoDesenvolvimento = Convert.ToInt32(PrazoEntregaTextBox.Text);
                    _historico.Prazo = Convert.ToInt32(PrazoEntregaTextBox.Text);
                }
                catch { }
                new ChamadoBO().Gravar(_chamados);

                _historico.Adequacao = tipoAdequacaoComboBox.Text + " " + numeroAdequacaoTextBox.Text;
            }

            _historico.Descricao = descricaoTextBox.Text;

            new ChamadoBO().Alterar(_historico);

            try
            {
                new LogBO().Gravar(_log);
            }
            catch { }

            Close();
        }
    }
}
