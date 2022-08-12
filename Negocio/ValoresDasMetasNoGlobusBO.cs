using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{

    public class ValoresDasMetasNoGlobusBO
    {
        #region DRE
        public List<DRE> Listar(int IdEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().Listar(IdEmpresa);
        }

        public DRE ConsultarDRE(int IdEmpresa, string referencia)
        {
            return new ValoresDasMetasNoGlobusDAO().ConsultarDRE(IdEmpresa, referencia);
        }

        public bool Gravar(DRE _dre, List<ValoresDasMetas> _lista)
        {
            return new ValoresDasMetasNoGlobusDAO().Gravar(_dre, _lista);
        }

        public bool Excluir(int id)
        {
            return new ValoresDasMetasNoGlobusDAO().Excluir(id);
        }

        public decimal Receitas(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().Receitas(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal Despesas(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().Despesas(Empresa, referencia, idMeta, idEmpresa);
        }
        #endregion

        public bool Gravar(List<ValoresDasMetas> _lista, List<ItensAvaliacaoMetas> _listaContratoMetas, string referencia)
        {
            return new ValoresDasMetasNoGlobusDAO().Gravar(_lista, _listaContratoMetas, referencia);
        }

        public bool Excluir(List<ValoresDasMetas> _lista)
        {
            return new ValoresDasMetasNoGlobusDAO().Excluir(_lista);
        }

        public decimal ReceitaBruta(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().ReceitaBruta(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal ReceitaSubsidio(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().ReceitaSubsidio(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal DeducoesSobreReceita(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().DeducoesSobreReceita(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal FolhaAdministracao(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().FolhaAdministracao(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal FolhaOperacao(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().FolhaOperacao(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal FolhaManutencao(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().FolhaManutencao(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal ManutencaoFrota(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().ManutencaoFrota(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal OutrosCustosDespesas(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().OutrosCustosDespesas(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal Pecas(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().Pecas(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal Pneus(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().Pneus(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal HorasExtrasOperacao(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().HorasExtrasOperacao(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal HorasExtrasManutencao(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().HorasExtrasManutencao(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal HorasExtrasAdministracao(string Empresa, string referencia, int idMeta, int idEmpresa)
        {
            return new ValoresDasMetasNoGlobusDAO().HorasExtrasAdministracao(Empresa, referencia, idMeta, idEmpresa);
        }

        public decimal KmRodado(string Empresa, DateTime inicio, DateTime fim)
        {
            return new ValoresDasMetasNoGlobusDAO().KmRodado(Empresa, inicio, fim);
        }

        public decimal KmRodadoModal(string Empresa, DateTime inicio, DateTime fim, int IdMetas)
        {
            return new ValoresDasMetasNoGlobusDAO().KmRodadoModal(Empresa, inicio, fim, IdMetas);
        }

        public decimal MKBF(string Empresa, DateTime inicio, DateTime fim)
        {
            return new ValoresDasMetasNoGlobusDAO().MKBF(Empresa, inicio, fim);
        }

        public decimal MKBFModal(string Empresa, DateTime inicio, DateTime fim, int IdMetas)
        {
            return new ValoresDasMetasNoGlobusDAO().MKBFModal(Empresa, inicio, fim, IdMetas);
        }

        public decimal ConsultarRadar(string Empresa, DateTime inicio, DateTime fim, string texto, string tipo = "V")
        {
            return new ValoresDasMetasNoGlobusDAO().ConsultarRadar(Empresa, inicio, fim, texto, tipo);
        }

        public decimal CNHVencidas(string Empresa, DateTime inicio, DateTime fim)
        {
            return new ValoresDasMetasNoGlobusDAO().CNHVencidas(Empresa, inicio, fim);
        }

        public decimal Avarias(string Empresa, DateTime inicio, DateTime fim)
        {
            return new ValoresDasMetasNoGlobusDAO().Avarias(Empresa, inicio, fim);
        }

        public decimal Acidentes(string Empresa, DateTime inicio, DateTime fim)
        {
            return new ValoresDasMetasNoGlobusDAO().Acidentes(Empresa, inicio, fim);
        }

        public decimal FuncionariosAtivos(string Empresa, DateTime inicio, DateTime fim, string texto, bool ignorarAprendiz = false)
        {
            return new ValoresDasMetasNoGlobusDAO().FuncionariosAtivos(Empresa, inicio, fim, texto, ignorarAprendiz);
        }

        public decimal Absenteismo(string Empresa, DateTime inicio, DateTime fim, string texto)
        {
            return new ValoresDasMetasNoGlobusDAO().Absenteismo(Empresa, inicio, fim, texto);
        }

        public decimal FuncionariosAdmitidos(string Empresa, DateTime inicio, DateTime fim, string texto)
        {
            return new ValoresDasMetasNoGlobusDAO().FuncionariosAdmitidos(Empresa, inicio, fim, texto);
        }

        public decimal FuncionariosDemitidos(string Empresa, DateTime inicio, DateTime fim, string texto)
        {
            return new ValoresDasMetasNoGlobusDAO().FuncionariosDemitidos(Empresa, inicio, fim, texto);
        }

        public decimal ValorPorEvento(string Empresa, DateTime competencia, string texto, string evento)
        {
            return new ValoresDasMetasNoGlobusDAO().ValorPorEvento(Empresa, competencia, texto, evento);
        }

        public decimal LitrosConsumidos(string Empresa, DateTime inicio, DateTime fim)
        {
            return new ValoresDasMetasNoGlobusDAO().LitrosConsumidos(Empresa, inicio, fim);
        }

        public decimal Gratuidade(string Empresa, DateTime inicio, DateTime fim)
        {
            return new ValoresDasMetasNoGlobusDAO().Gratuidade(Empresa, inicio, fim);
        }

        public decimal IntegracoesSemValor(string Empresa, DateTime inicio, DateTime fim)
        {
            return new ValoresDasMetasNoGlobusDAO().IntegracoesSemValor(Empresa, inicio, fim);
        }

        public decimal Pagantes(string Empresa, DateTime inicio, DateTime fim)
        {
            return new ValoresDasMetasNoGlobusDAO().Pagantes(Empresa, inicio, fim);
        }
    }
}
