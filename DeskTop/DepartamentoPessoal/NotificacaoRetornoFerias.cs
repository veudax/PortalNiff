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

namespace Suportte.DepartamentoPessoal
{
    public partial class NotificacaoRetornoFerias : Form
    {
        public NotificacaoRetornoFerias()
        {
            InitializeComponent();
        }

        public int idProgramacao;
        
        private void simButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Publicas._clicouSIM = true;
            Close();
        }

        private void naoButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Publicas._clicouSIM = false;
            Close();
        }

        private void simButton_Enter(object sender, EventArgs e)
        {
            simButton.BackColor = Publicas._botaoFocado;
            simButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void simButton_Validating(object sender, CancelEventArgs e)
        {
            simButton.BackColor = Publicas._botao;
            simButton.ForeColor = Publicas._fonteBotao;
        }

        private void naoButton_Enter(object sender, EventArgs e)
        {
            naoButton.BackColor = Publicas._botaoFocado;
            naoButton.ForeColor = Publicas._fonteBotaoFocado;
        }

        private void naoButton_Validating(object sender, CancelEventArgs e)
        {
            naoButton.BackColor = Publicas._botao;
            naoButton.ForeColor = Publicas._fonteBotao;
        }

        private void NotificacaoRetornoFerias_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
