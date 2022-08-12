using Classes;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Input;
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
    public partial class Lembrete : Form
    {
        public Lembrete()
        {
            InitializeComponent();
            

            dataAberturaDateTimePicker.BorderColor = Publicas._bordaSaida;
            dataAberturaDateTimePicker.BackColor = PrazoEntregaTextBox.BackColor;
            

            if (Publicas._alterouSkin)
            {
                foreach (Control componenteDaTela in this.Controls)
                {
                    Publicas.AplicarSkin(componenteDaTela);
                }
                if (Publicas._TemaBlack)
                {
                    skinManager1.VisualTheme = VisualTheme.Office2016Black;
                    dataAberturaDateTimePicker.Style = VisualStyle.Office2016Black;
                    sfCalendar1.ThemeName = "Office2016Black";
                    sfCalendar1.Refresh();
                }
            }
            Publicas._mensagemSistema = string.Empty;
        }

        

        private void PrazoEntregaTextBox_Enter(object sender, EventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaEntrada;
        }

        private void PrazoEntregaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                descricaoTextBox.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PrazoEntregaTextBox.Focus();
            }
        }

        private void descricaoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                gravarButton.Focus();
            Publicas._escTeclado = false;
            if (e.KeyCode == Keys.Escape)
            {
                Publicas._escTeclado = true;
                PrazoEntregaTextBox.Focus();
            }
        }

        private void PrazoEntregaTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(PrazoEntregaTextBox.Text) || PrazoEntregaTextBox.Text.Trim() == "0")
            {
                PrazoEntregaTextBox.Focus();
                return;
            }

            dataAberturaDateTimePicker.Value = DateTime.Now.Date.AddDays(Convert.ToInt32(PrazoEntregaTextBox.Text));

            FeriadoEmenda _feriado = new Dados.FeriadoDAO().Consulta(dataAberturaDateTimePicker.Value.Date, Publicas._usuario.IdEmpresa);

            if (_feriado.Existe)
                dataAberturaDateTimePicker.Value = dataAberturaDateTimePicker.Value.AddDays(1);

            // se cair o dia maximo de retorno em uma sabado ou domingo muda para o próximo dia util.
            if (dataAberturaDateTimePicker.Value.DayOfWeek == DayOfWeek.Saturday || dataAberturaDateTimePicker.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                if (dataAberturaDateTimePicker.Value.DayOfWeek == DayOfWeek.Saturday)
                    dataAberturaDateTimePicker.Value = dataAberturaDateTimePicker.Value.AddDays(2);
                else
                    dataAberturaDateTimePicker.Value = dataAberturaDateTimePicker.Value.AddDays(1);
            }
        }

        private void descricaoTextBox_Validating(object sender, CancelEventArgs e)
        {
            ((TextBoxExt)sender).BorderColor = Publicas._bordaSaida;

            if (Publicas._escTeclado)
            {
                Publicas._escTeclado = false;
                return;
            }

            if (string.IsNullOrEmpty(descricaoTextBox.Text))
            {
                descricaoTextBox.Focus();
                return;
            }
        }

        private void powerPictureBox_Click(object sender, EventArgs e)
        {
            Publicas._temLembrete = false;
            Close();
        }

        private void gravarButton_Click(object sender, EventArgs e)
        {
            Publicas._temLembrete = true;
            Publicas._prazoLembrete = 0;
            try
            {
                Publicas._prazoLembrete = Convert.ToInt32(PrazoEntregaTextBox.Text);
            }
            catch { }
            Publicas._motivoLembrete = descricaoTextBox.Text;
            Publicas._datasLembrete = sfCalendar1.SpecialDates.Select(s => s.Value).ToArray();
            Close();
        }

        private void SelecionarDatasCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelecionarDatasCheckBox.Checked)
            {
                descricaoTextBox.Size = new Size(434, 139);
                sfCalendar1.Visible = true;
            }
            else
            {
                descricaoTextBox.Size = new Size(636, 139);
                sfCalendar1.Visible = false;
            }
        }

        private void sfCalendar1_SelectionChanged(SfCalendar sender, Syncfusion.WinForms.Input.Events.SelectionChangedEventArgs e)
        {
            if (sfCalendar1.SpecialDates.Where(w => w.Value == sfCalendar1.SelectedDate).Count() == 0)
            {
                if (sfCalendar1.SpecialDates.Count() > 3)
                {
                    new Notificacoes.Mensagem("Permitido até 4 datas.", Publicas.TipoMensagem.Alerta).ShowDialog();
                    return;
                }

                if (new Notificacoes.Mensagem("Confirma a inclusão da data " + Convert.ToDateTime(sfCalendar1.SelectedDate).ToShortDateString() + " no lembrete? ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
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
                        if (new Notificacoes.Mensagem("Confirma a exclusão da data " + Convert.ToDateTime(sfCalendar1.SelectedDate).ToShortDateString() + " do lembrete? ", Publicas.TipoMensagem.Confirmacao).ShowDialog() == DialogResult.Yes)
                        {
                            sfCalendar1.SpecialDates.Remove(item);
                            break;
                        }
                    }
                }
            }

            sfCalendar1.Refresh();
        }
    }
}
