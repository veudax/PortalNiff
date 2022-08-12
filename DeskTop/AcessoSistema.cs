using DevExpress.XtraBars.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte
{
    public partial class AcessoSistema : Form
    {
        public AcessoSistema()
        {
            InitializeComponent();
        }

        private void AcessoSistema_Load(object sender, EventArgs e)
        {
            JuridicoPanel.Size = new Size(249, 81);
            SACPanel.Size = new Size(249, 81);
            AvaliacaoDesempenhoPanel.Size = new Size(249, 81);

            ItensCadastroJuridicoPanel.Visible = false;
            ItensJuridicoPanel.Visible = false;
            ItensAvaliacaoPanel.Visible = false;
            ItensSACPanel.Visible = false;

            this.Location = new Point(this.Left, 60);

            TileBarDropDownContainer rhConteiner = new TileBarDropDownContainer();
            rhConteiner.BackColor = Color.Transparent;
            rhConteiner.Size = new System.Drawing.Size(595, 185);
            rhConteiner.Controls.Add(RHtileBar);
            RHtileBar.Dock = DockStyle.Fill;
            RHtileBarItem.DropDownControl = rhConteiner;

            TileBarDropDownContainer juridicoConteiner = new TileBarDropDownContainer();
            juridicoConteiner.BackColor = Color.Transparent;
            juridicoConteiner.Size = new System.Drawing.Size(595, 185);
            juridicoConteiner.Controls.Add(JuridicotileBar);
            JuridicotileBar.Dock = DockStyle.Fill;
            JuridicotileBarItem.DropDownControl = juridicoConteiner;

            TileBarDropDownContainer cadadastroJuridicoConteiner = new TileBarDropDownContainer();
            cadadastroJuridicoConteiner.BackColor = Color.Transparent;
            cadadastroJuridicoConteiner.Size = new System.Drawing.Size(595, 185);
            cadadastroJuridicoConteiner.Controls.Add(cadastroJuridicoTileBar);
            cadastroJuridicoTileBar.Dock = DockStyle.Fill;
            cadastroJuridicoTileBarItem.DropDownControl = cadadastroJuridicoConteiner;

            TileBarDropDownContainer contabilidadeConteiner = new TileBarDropDownContainer();
            contabilidadeConteiner.BackColor = Color.Transparent;
            contabilidadeConteiner.Size = new System.Drawing.Size(595, 185);
            contabilidadeConteiner.Controls.Add(ContabilidadeTileBar);
            ContabilidadeTileBar.Dock = DockStyle.Fill;
            //ContabilidadeTileBar.Location = new Point(0, 113);
            ContabilidadeTileBarItem.DropDownControl = contabilidadeConteiner;

            TileBarDropDownContainer atendimentoConteiner = new TileBarDropDownContainer();
            atendimentoConteiner.BackColor = Color.Transparent;
            atendimentoConteiner.Size = new System.Drawing.Size(595, 185);
            atendimentoConteiner.Controls.Add(AtendimentoTileBar);
            AtendimentoTileBar.Dock = DockStyle.Fill;
            AtendimentoTileBarItem.DropDownControl = atendimentoConteiner;

            TileBarDropDownContainer AvaliacaoDesempenhoConteiner = new TileBarDropDownContainer();
            AvaliacaoDesempenhoConteiner.BackColor = Color.Transparent;
            AvaliacaoDesempenhoConteiner.Size = new System.Drawing.Size(595, 185);
            AvaliacaoDesempenhoConteiner.Controls.Add(AvaliacaoDesemenhoTileBar);
            AvaliacaoDesemenhoTileBar.Dock = DockStyle.Fill;
            AvaliacaoDesempenhotileBarItem.DropDownControl = AvaliacaoDesempenhoConteiner;

            TileBarDropDownContainer BibliotecaConteiner = new TileBarDropDownContainer();
            BibliotecaConteiner.BackColor = Color.Transparent;
            BibliotecaConteiner.Size = new System.Drawing.Size(595, 185);
            BibliotecaConteiner.Controls.Add(BibliotecaTileBar);
            BibliotecaTileBar.Dock = DockStyle.Fill;
            BibliotecaTileBarItem.DropDownControl = BibliotecaConteiner;

            TileBarDropDownContainer ControladoriaConteiner = new TileBarDropDownContainer();
            ControladoriaConteiner.BackColor = Color.Transparent;
            ControladoriaConteiner.Size = new System.Drawing.Size(595, 185);
            ControladoriaConteiner.Controls.Add(ConroladoriaTileBar);
            ConroladoriaTileBar.Dock = DockStyle.Fill;
            ControladoriaTileBarItem.DropDownControl = ControladoriaConteiner;


            TileBarDropDownContainer CadBibliotecaConteiner = new TileBarDropDownContainer();
            CadBibliotecaConteiner.BackColor = Color.Transparent;
            CadBibliotecaConteiner.Size = new System.Drawing.Size(595, 185);
            CadBibliotecaConteiner.Controls.Add(cadastroBibliotecaTileBar);
            cadastroBibliotecaTileBar.Dock = DockStyle.Fill;
            CadastroBibliotecaTileBarItem.DropDownControl = CadBibliotecaConteiner;
        }

        private void JuridicoPanel_Click(object sender, EventArgs e)
        {
            JuridicoPanel.Size = new Size(249, ( JuridicoPanel.Height == 81 ? 140 : 81) );
            CadastroJuridicoPanel.Size = AbrirComunicadoPanel.Size;
            ItensJuridicoPanel.Visible = !ItensJuridicoPanel.Visible;
            ItensCadastroJuridicoPanel.Visible = false;
        }

        private void JuridicoPanel_MouseHover(object sender, EventArgs e)
        {
            //JuridicoPanel.Size = new Size(249, 225);
        }

        private void JuridicoPanel_MouseLeave(object sender, EventArgs e)
        {
            //JuridicoPanel.Size = new Size(249, 81);
        }

        private void CadastroJuridicoPanel_Click(object sender, EventArgs e)
        {
            ItensJuridicoPanel.Dock = DockStyle.None;
            ItensJuridicoPanel.Location = new Point(0, 70);
            ItensJuridicoPanel.Size = new Size(249, 239);

            ItensCadastroJuridicoPanel.Visible = !ItensCadastroJuridicoPanel.Visible;

            if (ItensCadastroJuridicoPanel.Visible)
            {
                JuridicoPanel.Size = new Size(249, 205);
                ItensCadastroJuridicoPanel.Size = new Size(249, 65);
                CadastroJuridicoPanel.Size = new Size(249, 100);
            }
            else
            {
                JuridicoPanel.Size = new Size(249, 140);
                ItensCadastroJuridicoPanel.Size = new Size(249, 35);
                CadastroJuridicoPanel.Size = new Size(249, 35);
            }
        }

        private void AvaliacaoDesempenhoPanel_Click(object sender, EventArgs e)
        {
            AvaliacaoDesempenhoPanel.Size = new Size(249, (AvaliacaoDesempenhoPanel.Height == 81 ? 210 : 81));

            ItensAvaliacaoPanel.Dock = DockStyle.None;
            ItensAvaliacaoPanel.Location = new Point(0, 70);
            ItensAvaliacaoPanel.Size = new Size(249, 151);

            ControladoriaPanel.Size = AbrirComunicadoPanel.Size;
            RecursosHumananosPanel.Size = AbrirComunicadoPanel.Size;
            ColaboradorPanel.Size = AbrirComunicadoPanel.Size;
            GestorPanel.Size = AbrirComunicadoPanel.Size;

            ItensAvaliacaoPanel.Visible = !ItensAvaliacaoPanel.Visible;
            ItensControladoriaPanel.Visible = false;
            ItensColaboradorPanel.Visible = false;
            ItensRHPanel.Visible = false;
            ItensCadastroRHPanel.Visible = false;
            ItensGestorPanel.Visible = false;
        }

        private void Controladoria()
        {
            if (ItensControladoriaPanel.Visible)
            {
                AvaliacaoDesempenhoPanel.Size = new Size(249, 275);
                ItensAvaliacaoPanel.Size = new Size(249, 213);
                ControladoriaPanel.Size = new Size(249, 100);
            }
            else
            {
                AvaliacaoDesempenhoPanel.Size = new Size(249, 210);
                ItensAvaliacaoPanel.Size = new Size(249, 151);
                ControladoriaPanel.Size = new Size(249, 35);
            }
        }

        private void ControladoriaPanel_Click(object sender, EventArgs e)
        {
            RecursosHumados();

            ItensAvaliacaoPanel.Dock = DockStyle.None;
            ItensAvaliacaoPanel.Location = new Point(0, 70);

            RecursosHumananosPanel.Size = AbrirComunicadoPanel.Size;
            ColaboradorPanel.Size = AbrirComunicadoPanel.Size;
            GestorPanel.Size = AbrirComunicadoPanel.Size;

            ItensControladoriaPanel.Visible = !ItensControladoriaPanel.Visible;

            Controladoria();

            ItensColaboradorPanel.Visible = false;
            ItensRHPanel.Visible = false;
            ItensCadastroRHPanel.Visible = false;
            ItensGestorPanel.Visible = false;
        }

        private void RecursosHumados()
        {
            if (ItensRHPanel.Visible)
            {
                ItensRHPanel.Location = new Point(0, 34);
                RecursosHumananosPanel.Controls.Add(ItensRHPanel);
                AvaliacaoDesempenhoPanel.Size = new Size(249, 400);
                ItensAvaliacaoPanel.Size = new Size(249, 405);
                RecursosHumananosPanel.Size = new Size(249, 225);
            }
            else
            {
                RecursosHumananosPanel.Controls.Remove(ItensRHPanel);
                AvaliacaoDesempenhoPanel.Size = new Size(249, 210);
                ItensAvaliacaoPanel.Size = new Size(249, 151);
                RecursosHumananosPanel.Size = new Size(249, 35);
            }
        }

        private void RecursosHumananosPanel_Click(object sender, EventArgs e)
        {
            Controladoria();

            ItensAvaliacaoPanel.Dock = DockStyle.None;
            ItensAvaliacaoPanel.Location = new Point(0, 70);

            ItensControladoriaPanel.Visible = false;
            ItensRHPanel.Visible = !ItensRHPanel.Visible;
            ItensCadastroRHPanel.Visible = false;
            ItensPontuacao9BoxPanel.Visible = false;
            CadastroRHPanel.Size = AbrirComunicadoPanel.Size;

            RecursosHumados();

            ControladoriaPanel.Size = AbrirComunicadoPanel.Size;
            ColaboradorPanel.Size = AbrirComunicadoPanel.Size;
            GestorPanel.Size = AbrirComunicadoPanel.Size;

            ItensColaboradorPanel.Visible = false;
            ItensGestorPanel.Visible = false;

        }

        private void CadastroRH()
        {
            if (ItensCadastroRHPanel.Visible)
            {
                CadastroRHPanel.Size = new Size(249, 276);
                ItensCadastroRHPanel.Size = new Size(249, 246);

                AvaliacaoDesempenhoPanel.Size = new Size(249, 642);
                ItensAvaliacaoPanel.Size = new Size(249, 567);
                RecursosHumananosPanel.Size = new Size(249, 467);
            }
            else
            {
                CadastroRHPanel.Size = new Size(249, 35);
                AvaliacaoDesempenhoPanel.Size = new Size(249, 400);
                ItensAvaliacaoPanel.Size = new Size(249, 405);
                RecursosHumananosPanel.Size = new Size(249, 225);
            }
        }

        private void CadastroRHPanel_Click(object sender, EventArgs e)
        {
            ItensCadastroRHPanel.Visible = !ItensCadastroRHPanel.Visible;
            ItensPontuacao9BoxPanel.Visible = false;
            ItensPontuacao9BoxPanel.Size = new Size(249, 35);

            CadastroRH();
        }
        
        private void Pontuacao9BoxPanel_Click(object sender, EventArgs e)
        {
            ItensPontuacao9BoxPanel.Visible = !ItensPontuacao9BoxPanel.Visible;

            if (ItensPontuacao9BoxPanel.Visible)
            {
                CadastroRHPanel.Size = new Size(249, 338);
                ItensCadastroRHPanel.Size = new Size(249, 308);
                Pontuacao9BoxPanel.Size = new Size(249, 95);
                ItensPontuacao9BoxPanel.Size = new Size(249, 65);

                AvaliacaoDesempenhoPanel.Size = new Size(249, 704);
                ItensAvaliacaoPanel.Size = new Size(249, 629);
                RecursosHumananosPanel.Size = new Size(249, 529);
            }
            else
            {
                CadastroRHPanel.Size = new Size(249, 276);
                ItensCadastroRHPanel.Size = new Size(249, 246);
                Pontuacao9BoxPanel.Size = new Size(249, 31);

                AvaliacaoDesempenhoPanel.Size = new Size(249, 642);
                ItensAvaliacaoPanel.Size = new Size(249, 567);
                RecursosHumananosPanel.Size = new Size(249, 467);
            }
        }

        private void ColaboradorPanel_Click(object sender, EventArgs e)
        {
            ItensAvaliacaoPanel.Dock = DockStyle.None;
            ItensAvaliacaoPanel.Location = new Point(0, 70);

            ItensControladoriaPanel.Visible = false;
            ItensColaboradorPanel.Visible = !ItensColaboradorPanel.Visible;
            ItensRHPanel.Visible = false;
            ItensCadastroRHPanel.Visible = false;
            ItensPontuacao9BoxPanel.Visible = false;

            if (ItensColaboradorPanel.Visible)
            {
                ItensColaboradorPanel.Location = new Point(0, 34);
                ColaboradorPanel.Controls.Add(ItensColaboradorPanel);

                AvaliacaoDesempenhoPanel.Size = new Size(249, 335);
                ItensAvaliacaoPanel.Size = new Size(249, 367);
                ColaboradorPanel.Size = new Size(249, 160);
            }
            else
            {
                ColaboradorPanel.Controls.Remove(ItensColaboradorPanel);
                AvaliacaoDesempenhoPanel.Size = new Size(249, 210);
                ItensColaboradorPanel.Size = new Size(249, 151);
                ColaboradorPanel.Size = new Size(249, 35);
            }

            RecursosHumananosPanel.Size = AbrirComunicadoPanel.Size;
            CadastroRHPanel.Size = AbrirComunicadoPanel.Size;            
            ControladoriaPanel.Size = AbrirComunicadoPanel.Size;
            GestorPanel.Size = AbrirComunicadoPanel.Size;
                        
        }

        private void GestorPanel_Click(object sender, EventArgs e)
        {
            ItensAvaliacaoPanel.Dock = DockStyle.None;
            ItensAvaliacaoPanel.Location = new Point(0, 70);

            ItensControladoriaPanel.Visible = false;
            ItensColaboradorPanel.Visible = false;
            ItensRHPanel.Visible = false;
            ItensCadastroRHPanel.Visible = false;
            ItensPontuacao9BoxPanel.Visible = false;
            ItensGestorPanel.Visible = !ItensGestorPanel.Visible;

            if (ItensGestorPanel.Visible)
            {
                ItensGestorPanel.Location = new Point(0, 34);
                GestorPanel.Controls.Add(ItensGestorPanel);

                AvaliacaoDesempenhoPanel.Size = new Size(249, 335);
                ItensAvaliacaoPanel.Size = new Size(249, 367);
                GestorPanel.Size = new Size(249, 160);
            }
            else
            {
                GestorPanel.Controls.Remove(ItensGestorPanel);
                AvaliacaoDesempenhoPanel.Size = new Size(249, 210);
                GestorPanel.Size = new Size(249, 35);
            }

            RecursosHumananosPanel.Size = AbrirComunicadoPanel.Size;
            CadastroRHPanel.Size = AbrirComunicadoPanel.Size;
            ControladoriaPanel.Size = AbrirComunicadoPanel.Size;
            ColaboradorPanel.Size = AbrirComunicadoPanel.Size;
        }

        private void SACPanel_Click(object sender, EventArgs e)
        {
            SACPanel.Size = new Size(249, (SACPanel.Height == 81 ? 246 : 81));
            ItensSACPanel.Location = new Point(0, 70);
            ItensSACPanel.Visible = !ItensSACPanel.Visible;
        }

        private void tileBarItem1_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            
        }
    }
}
