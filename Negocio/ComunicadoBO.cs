using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ComunicadoBO
    {
        public List<Comunicado> Listar(int idEmpresa = 0, int ano = 0, Publicas.StatusComunicado _status = Publicas.StatusComunicado.Todos, string processo = "")
        {
            return new ComunicadoDAO().Listar(idEmpresa, ano, _status, processo);
        }

        public Comunicado Consultar(int id, int empresa = 0, string processo = "")
        {
            return new ComunicadoDAO().Consulta(id, empresa, processo);
        }

        public List<int> Datas()
        {
            return new ComunicadoDAO().Datas();
        }

        public bool Aprovar(Comunicado _comunicado, List<ParcelasDoComunicado> _listaParcelas)
        {
            return new ComunicadoDAO().Aprovar(_comunicado, _listaParcelas);
        }

        public bool Reprovar(int _id)
        {
            return new ComunicadoDAO().Reprovar(_id);
        }

        public bool Cancelar(int _id, string motivo)
        {
            return new ComunicadoDAO().Cancelar(_id, motivo);
        }

        public bool Finalizar(int _id)
        {
            return new ComunicadoDAO().Finalizar(_id);
        }

        public bool Gravar(Comunicado _comunicado, List<ParcelasDoComunicado> _listaParcelas)
        {
            return new ComunicadoDAO().Gravar(_comunicado, _listaParcelas);
        }

        public List<ParcelasDoComunicado> ListarParcelas(int id)
        {
            return new ParcelasDoComunicadoDAO().Listar(id);
        }

        public bool AplicarStatusEmail(int id, string status)
        {
            return new ComunicadoDAO().AplicarStatusEmail(id, status);
        }

        public List<NotificacaoComunicado> ListarNotificacoes(int ano)
        {
            return new NotificacaoComunicadoDAO().Listar(ano);
        }

        public bool GravarNotificacao(NotificacaoComunicado notificacao)
        {
            return new NotificacaoComunicadoDAO().Gravar(notificacao);
        }
    }
}
