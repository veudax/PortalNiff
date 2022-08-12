using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TorneioBO
    {
        public List<Torneio> ListarTorneio(bool apenasAtivos)
        {
            return new TorneioDAO().Listar(apenasAtivos);
        }

        public Torneio ConsultarTorneio(int codigo)
        {
            return new TorneioDAO().Consulta(codigo);
        }

        public int Proximo()
        {
            return new TorneioDAO().Proximo();
        }

        public bool Gravar(Torneio tipo)
        {
            return new TorneioDAO().Gravar(tipo);
        }

        public bool Excluir(int id)
        {
            return new TorneioDAO().Excluir(id);
        }

        public PartidasDoTorneio ConsultarPartida(int id)
        {
            return new TorneioDAO().ConsultarPartida(id);
        }

        public List<PartidasDoTorneio> ListarPartidas(int idColaborador)
        {
            return new TorneioDAO().ListarPartidas(idColaborador);
        }

        public List<PartidasDoTorneio> ListarPartidas(int IdTorneio, DateTime data, string sexo)
        {
            return new TorneioDAO().ListarPartidas(IdTorneio, data, sexo);
        }

        public List<PartidasDoTorneio> ListarPartidas(int IdTorneio, DateTime data)
        {
            return new TorneioDAO().ListarPartidas(IdTorneio, data);
        }

        public List<AgendaTorneio> ListarPartidas()
        {
            return new TorneioDAO().ListarPartidas();
        }

        public List<Participantes> ListarClassificacao()
        {
            return new TorneioDAO().ListarClassificacao();
        }

        public bool GravarPartidas(List<PartidasDoTorneio> tipo)
        {
            return new TorneioDAO().GravarPartidas(tipo);
        }

        public bool AlterarDataDaPartida(List<PartidasDoTorneio> tipo)
        {
            return new TorneioDAO().AlterarDataDaPartida(tipo);
        }

        public bool ExcluirPartidas(int id)
        {
            return new TorneioDAO().ExcluirPartidas(id);
        }

        //public List<ParticipantesDoTorneio> ListarParticipantes(int IdTorneio)
        //{
        //    return new TorneioDAO().ListarParticipantes(IdTorneio);
        //}

        //public bool GravarParticipantes(List<ParticipantesDoTorneio> _lista)
        //{
        //    return new TorneioDAO().GravarParticipantes(_lista);
        //}

        //public bool GravarPlacares(List<ParticipantesDoTorneio> _lista)
        //{
        //    return new TorneioDAO().GravarPlacares(_lista);
        //}
    }
}
