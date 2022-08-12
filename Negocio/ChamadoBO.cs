using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ChamadoBO
    {
        public List<int> Datas()
        {
            return new ChamadoDAO().Datas();
        }

        public List<Chamado> ListarNotificacoes(int departamento = 0)
        {
            return new ChamadoDAO().ListarNotificacoes(departamento);
        }

        public List<Chamado> Listar(int departamento = 0, bool apenasDoUsuario = false, string status = "T", string ano = "")
        {
            return new ChamadoDAO().Listar(departamento, apenasDoUsuario, status, ano);
        }

        public List<Chamado> Pesquisar(string pesquisa)
        {
            return new ChamadoDAO().Pesquisar(pesquisa);
        }

        public Chamado Consulta(int codigo)
        {   
            return new ChamadoDAO().Consulta(codigo);
        }

        public bool Gravar(Chamado chamado)
        {
            return new ChamadoDAO().Grava(chamado);
        }

        public bool GravarAvaliacaoAtendente(Chamado chamado)
        {
            return new ChamadoDAO().GravaAvaliacaoAtendente(chamado);
        }

        public bool GravarAvaliacaoSolicitante(Chamado chamado)
        {
            return new ChamadoDAO().GravaAvaliacaoSolicitante(chamado);
        }

        public bool GravaStatus(Chamado chamado)
        {
            return new ChamadoDAO().GravaStatus(chamado);
        }

        public bool LiberarParaReavaliacao(Chamado chamado)
        {
            return new ChamadoDAO().LiberarParaReavaliacao(chamado);
        }        

        public string ProximoCodigo(Publicas.TipoCalculoChamado tipo, string separador)
        {
            return new ChamadoDAO().ProximoCodigo(tipo, separador);
        }

        public List<HistoricoDoChamado> ListarHistoricos(int idChamado, bool trazerUltimoTramite, bool naoPrivados)
        {
            return new HistoricosChamadoDAO().Listar(idChamado, trazerUltimoTramite, naoPrivados);
        }

        public bool GravarHistorico(HistoricoDoChamado chamado, List<AnexoDoHistorico> anexos, DateTime sla)
        {
            return new HistoricosChamadoDAO().Grava(chamado, anexos, sla);
        }

        public List<AnexoDoHistorico> ListarAnexos(int idChamado)
        {
            return new AnexoDoHistoricoDAO().Listar(idChamado);
        }

        public bool GravarAnexos(List<AnexoDoHistorico> anexos)
        {
            return new AnexoDoHistoricoDAO().Grava(anexos);
        }

        public bool Agrupar (List<Chamado> _chamados)
        {
            return new ChamadoDAO().AgruparChamado(_chamados);
        }

        public int ConsultaQuantidadeSemAvaliacao()
        {
            return new ChamadoDAO().ConsultaQuantidadeSemAvaliacao();
        }

        public bool GravarLembrete(List<Lembrete> _lembretes)
        {
            return new ChamadoDAO().Grava(_lembretes);
        }

        public HistoricoDoChamado Consultar(int IdHistorico)
        {
            return new HistoricosChamadoDAO().Consultar(IdHistorico);
        }

        public bool Alterar(HistoricoDoChamado chamado)
        {
            return new HistoricosChamadoDAO().Altera(chamado);
        }

        public void AvaliacaoAutomatica()
        {
            new ChamadoDAO().AvaliacaoAutomatica();
        }
        
        public bool IniciarTemporizador(int idChamado)
        {
            return new ChamadoDAO().IniciarTemporizador(idChamado);
        }

        public bool PausarTemporizador(int idTemporizador, DateTime inicio)
        {
            return new ChamadoDAO().PausarTemporizador(idTemporizador, inicio);
        }

        public List<TempoExecucao> Temporizador(int idChamado)
        {
            return new ChamadoDAO().Temporizador(idChamado);
        }

        public bool GravarTempoEstimado(Chamado chamado)
        {
            return new ChamadoDAO().GravaTempoEstimado(chamado);
        }
    }
}
