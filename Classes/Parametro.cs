using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    /* tabela NIFF_CHM_Parametros*/
    public class Parametro
    {
        public int IdParam { get; set; }
        public int PrazoReabertura { get; set; } // PRAZOREABERTURA
        public int PrazoRetorno { get; set; } // QTDDIASRETORNOCHAMADO
        public int CancelarVisivelPor { get; set; } //EXIBIRCANCELADOS

        public bool UsuarioComMesmoDepartamentoPodeVerChamados { get; set; } // USUARIOMESMODEPTO
        public bool ExigeAvaliacao { get; set; } // EXIGEAVALIACAO
        public bool AtentendePodeAbrirChamado { get; set; } // ATENDENTEPODECHAMADO
        public bool AtentendePodeConcluirChamado { get; set; } // ATENDENTECONCLUICHAMADO

        public string Email { get; set; } // EMAILCHAMADO
        public string Smtp { get; set; } // SMTP
        public bool Autentica { get; set; } // AUTENTICA
        public bool AutenticaSLL { get; set; } // AUTENTICASMTP
        public int PortaSmtp { get; set; } // PORTA
        public string Senha { get; set; } // SENHAEMAIL

        public Publicas.TipoCalculoChamado FormatoChamado { get; set; } // FORMATOCHAMADO
        public Publicas.TipoUsuarioCancela UsuarioQuePodeCancelarChamado { get; set; }  //USUAUTOCANCELAR
        public DateTime HoraInicioAgenda { get; set; } // HORAINICIOAGENDA
        public DateTime HoraFimAgenda { get; set; } // HORAFIMAGENDA
        public string Separador { get; set; }
        public int MesesConsultaDashboardChamados { get; set; }
        public bool Existe { get; set; }
    }
}
