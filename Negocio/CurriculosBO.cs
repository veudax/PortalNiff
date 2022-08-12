using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CurriculosBO
    {
        #region Candidatos
        public List<HistoricoDoCandidato> ListaHistorico(int idCandidato, int idVaga = 0)
        {
            return new CurriculosDAO().ListaHistorico(idCandidato, idVaga);
        }

        public List<ArquivosDoCandidato> ListarArquivosDoCandidatos(int IdCandidatos)
        {
            return new CurriculosDAO().ListarArquivosDoCandidatos(IdCandidatos);
        }

        public List<Candidatos> ListarCandidatos(bool apenasAtivos)
        {
            return new CurriculosDAO().ListarCandidatos(apenasAtivos);
        }

        public Candidatos ConsultarCandidato(int codigo)
        {
            return new CurriculosDAO().ConsultarCandidato(codigo);
        }

        public CandidatosDaVaga ConsultarVagasDaCandidato(int IdCandidato)
        {
            return new CurriculosDAO().ConsultarVagasDaCandidato(IdCandidato);
        }

        public List<Curriculos> ConsultarCandidatosDaVaga(int IdVaga)
        {
            List<CandidatosDaVaga> _listaVagas = new CurriculosDAO().ConsultarCandidatosDaVaga(IdVaga);
            List<Curriculos> _lista = new List<Curriculos>();

            foreach (var item in _listaVagas)
            {
                Candidatos _candidato = new CurriculosDAO().ConsultarCandidato(item.IdCandidato);
                Vagas _vaga = new CurriculosDAO().ConsultarVaga(item.IdVaga);

                Curriculos _cv = new Curriculos();
                _cv.Historico = new CurriculosDAO().ListaHistorico(item.IdCandidato, item.IdVaga);
                _cv.IdCandidato = item.IdCandidato;
                _cv.IdVagas = item.IdVaga;
                _cv.NomeCandidato = _candidato.Nome;
                _cv.Telefone = _candidato.TelefoneFormatado;
                _cv.Celular = _candidato.CelularFormatado;
                _cv.Email = _candidato.Email;
                _cv.NomeVaga = _vaga.Descricao;

                _cv.PreSelecionado = _cv.Historico.Where(w => w.Status == "2").Count() != 0;
                _cv.AprovadoGestor = _cv.Historico.Where(w => w.Status == "5").Count() != 0;
                _cv.Aprovado = _cv.Historico.Where(w => w.Status == "15").Count() != 0;
                _cv.Reprovado = _cv.Historico.Where(w => w.Status == "16").Count() != 0;
                _cv.ReprovadoGestor = _cv.Historico.Where(w => w.Status == "10").Count() != 0;
                _cv.Contato = _cv.Historico.Where(w => w.Status == "3").Count() != 0;
                _cv.SemContato = _cv.Historico.Where(w => w.Status == "4").Count() != 0;
                _cv.CVArquivo = item.CVArquivo;

                foreach (var itemH in _cv.Historico.Where(w => w.Status == "6" || w.Status == "8").OrderByDescending(o => o.Data))
                {
                    _cv.DataPrimeiraEntrevista = itemH.DataEntrevista;

                    if (_cv.Historico.Where(w => w.Status == "12" && w.Data > itemH.Data).Count() == 0)
                        _cv.Data1Entrevista = _cv.DataPrimeiraEntrevista.ToShortDateString() + " " + _cv.DataPrimeiraEntrevista.ToShortTimeString();
                    break;
                }

                foreach (var itemH in _cv.Historico.Where(w => w.Status == "7" || w.Status == "9").OrderByDescending(o => o.Data))
                {
                    _cv.DataSegundaEntrevista = itemH.DataEntrevista;

                    if (_cv.Historico.Where(w => w.Status == "13" && w.Data > itemH.Data).Count() == 0)
                        _cv.Data2Entrevista = _cv.DataSegundaEntrevista.ToShortDateString() + " " + _cv.DataSegundaEntrevista.ToShortTimeString();
                    break;
                }

                foreach (var itemH in _cv.Historico.OrderByDescending(o => o.Data))
                { // mostra o último status
                    _cv.Status = itemH.DescricaoStatus;
                    break;
                }

                _lista.Add(_cv);
            }

            return _lista;
        }

        public int ProximoCandidato()
        {
            return new CurriculosDAO().ProximoCandidato();
        }

        public bool GravarCandidato(Candidatos tipo, List<ArquivosDoCandidato> _arquivos, CandidatosDaVaga _vagas, List<HistoricoDoCandidato> _historico)
        {
            return new CurriculosDAO().GravarCandidato(tipo, _arquivos, _vagas, _historico);
        }

        public bool GravarHistorico(List<HistoricoDoCandidato> _historicos)
        {
            return new CurriculosDAO().GravarHistorico(_historicos);
        }

        public bool ExcluirCandidato(int id)
        {
            return new CurriculosDAO().ExcluirCandidato(id);
        }

        #endregion

        #region Vagas
        public List<Vagas> ListarVagas(bool apenasAtivos, int idEmpresa = 0)
        {
            return new CurriculosDAO().ListarVagas(apenasAtivos, idEmpresa);
        }

        public Vagas ConsultarVaga(int codigo)
        {
            return new CurriculosDAO().ConsultarVaga(codigo);
        }

        public int ProximaVaga()
        {
            return new CurriculosDAO().ProximaVaga();
        }

        public bool GravarVaga(Vagas tipo)
        {
            return new CurriculosDAO().GravarVaga(tipo);
        }

        public bool ExcluirVaga(int id)
        {
            return new CurriculosDAO().ExcluirVaga(id);
        }

        #endregion
    }
}
