using Classes;
using System;
using Syncfusion.WinForms.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suportte.GestaoEstrategica
{
    public partial class DataDeCorteResultadoBSC : Form
    {
        public DataDeCorteResultadoBSC()
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
                    sfCalendar1.ThemeName = "Office2016Black";
                    sfCalendar1.Refresh();
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        private void sfCalendar1_SelectionChanged(Syncfusion.WinForms.Input.SfCalendar sender, Syncfusion.WinForms.Input.Events.SelectionChangedEventArgs e)
        {
            if (sfCalendar1.SpecialDates.Where(w => w.Value == sfCalendar1.SelectedDate).Count() == 0)
            {
                if (sfCalendar1.SpecialDates.Count() > 1)
                {
                    new Notificacoes.Mensagem("Permitido apenas uma Data.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }

                if (new Notificacoes.Mensagem("Confirma a inclusão da data " + Convert.ToDateTime(sfCalendar1.SelectedDate).ToShortDateString() + "? ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                {
                    SpecialDate specialDate = new SpecialDate();
                    specialDate.Value = Convert.ToDateTime(sfCalendar1.SelectedDate);

                    if (Publicas._TemaBlack)
                    {
                        specialDate.ForeColor = Color.SteelBlue;
                        specialDate.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
                    }
                    sfCalendar1.SpecialDates.Add(specialDate);
                }
            }
            else
            {
                foreach (var item in sfCalendar1.SpecialDates)
                {
                    if (item.Value == Convert.ToDateTime(sfCalendar1.SelectedDate))
                    {
                        if (new Notificacoes.Mensagem("Confirma a exclusão da data " + Convert.ToDateTime(sfCalendar1.SelectedDate).ToShortDateString() + "? ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                        {
                            sfCalendar1.SpecialDates.Remove(item);
                            break;
                        }
                    }
                }
            }

            sfCalendar1.Refresh();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            if (sfCalendar1.SpecialDates.Select(s => s.Value).Count() == 0)
            {
                if (new Notificacoes.Mensagem("Confirma data " + DateTime.Now.Date.ToShortDateString() + " como data de corte?", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.No)
                    return;

                SpecialDate specialDate = new SpecialDate();
                specialDate.Value = DateTime.Now.Date;

                if (Publicas._TemaBlack)
                {
                    specialDate.ForeColor = Color.SteelBlue;
                    specialDate.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
                }

                sfCalendar1.SpecialDates.Add(specialDate);
            }

            try
            {
                Publicas._datasLembrete = sfCalendar1.SpecialDates.Select(s => s.Value).ToArray();
            }
            catch
            {
                
            }
            
            Close();
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Publicas._datasLembrete = null;
            Close();
        }
    }
}
