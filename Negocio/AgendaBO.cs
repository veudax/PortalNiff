using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AgendaBO
    {
        public List<Agenda> Listar(int usuario, Publicas.TipoAgenda tipoAgenda)
        {
            return new AgendaDAO().Listar(usuario, tipoAgenda);
        }

        public List<Agenda> Consultar(DateTime dataInicio, DateTime dataFim, Publicas.TipoAgenda tipoAgenda, bool diaTodo)
        {
            return new AgendaDAO().Consultar(dataInicio, dataFim, tipoAgenda, diaTodo);
        }

        public bool Gravar(Agenda agenda, List<ParticipanteDaAgenda> participantes)
        {
            return new AgendaDAO().Gravar(agenda, participantes);
        }

        public List<ParticipanteDaAgenda> Listar(int idAgenda)
        {
            return new AgendaDAO().Listar(idAgenda);
        }

        public bool Excluir(int idAgenda)
        {
            return new AgendaDAO().Excluir(idAgenda);
        }
    }
}
