using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ParticipanteCorridaBO
    {
        public List<ParticipanteCorrida> ListarCPF()
        {
            return new ParticipanteCorridaDAO().ListarCPF();
        }

        public List<ParticipanteCorrida> Listar(int distancia)
        {
            return new ParticipanteCorridaDAO().Listar(distancia);
        }

        public ParticipanteCorrida Consultar(int codigo, int corrida, decimal cpf = 0)
        {
            return new ParticipanteCorridaDAO().Consulta(codigo, corrida, cpf);
        }

        public bool Gravar(ParticipanteCorrida tipo)
        {
            return new ParticipanteCorridaDAO().Gravar(tipo);
        }

        public bool Excluir(ParticipanteCorrida tipo)
        {
            return new ParticipanteCorridaDAO().Excluir(tipo);
        }

        public bool ResultadoVisualizado(int codigo)
        {
            return new ParticipanteCorridaDAO().ResultadoVisualizado(codigo);
        }

        public bool AtualizaValorInscricaoCapitao(int usuario, int corrida, decimal valor)
        {
            return new ParticipanteCorridaDAO().AtualizaValorInscricaoCapitao(usuario, corrida, valor);
        }
    }
}
