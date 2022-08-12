using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EmailEnvioComunicadoBO
    {
        public List<EmailEnvioComunicado> Listar(int idEmpresa = 0, bool apenasAtivos = false)
        {
            return new EmailEnvioComunicadoDAO().Listar(idEmpresa, apenasAtivos);
        }

        public EmailEnvioComunicado Consultar(int idEmpresa, string email)
        {
            return new EmailEnvioComunicadoDAO().Consulta(idEmpresa, email);
        }

        public bool Gravar(List<EmailEnvioComunicado> email)
        {
            return new EmailEnvioComunicadoDAO().Gravar(email);
        }

        public bool Excluir(int IdEmpresa)
        {
            return new EmailEnvioComunicadoDAO().Excluir(IdEmpresa);
        }
    }
}
