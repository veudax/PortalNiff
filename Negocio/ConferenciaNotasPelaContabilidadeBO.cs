using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ConferenciaNotasPelaContabilidadeBO
    {
        public List<ConferenciaNotasPelaContabilidade.GrupoDespesas> ListarGrupo()
        {
            return new ConferenciaNotasPelaContabilidadeDAO().ListarGrupo();
        }

        public ConferenciaNotasPelaContabilidade.GrupoDespesas ConsultarGrupo(int codigo)
        {
            return new ConferenciaNotasPelaContabilidadeDAO().ConsultarGrupo(codigo);
        }

        public List<ConferenciaNotasPelaContabilidade.Parametros> Listar()
        {
            return new ConferenciaNotasPelaContabilidadeDAO().Listar();
        }

        public bool Gravar(List<ConferenciaNotasPelaContabilidade.Parametros> _lista)
        {
            return new ConferenciaNotasPelaContabilidadeDAO().Gravar(_lista);
        }

        public bool ExcluirTipo(int id)
        {
            return new ConferenciaNotasPelaContabilidadeDAO().ExcluirTipo(id);
        }

        public bool ExcluirGrupo(int grupo)
        {
            return new ConferenciaNotasPelaContabilidadeDAO().ExcluirGrupo(grupo);
        }

        public List<ConferenciaNotasPelaContabilidade.Conferencia> Listar(int idEmpresa, string referencia)
        {
            return new ConferenciaNotasPelaContabilidadeDAO().Listar(idEmpresa, referencia);
        }

        public List<ConferenciaNotasPelaContabilidade.ItensConferencia> ListarItens(int idEmpresa, string referencia, int plano, bool apenasNaoConferidas)
        {
            return new ConferenciaNotasPelaContabilidadeDAO().ListarItens(idEmpresa, referencia, plano, apenasNaoConferidas);
        }

        public bool Gravar(List<ConferenciaNotasPelaContabilidade.ItensConferencia> _lista, List<ConferenciaNotasPelaContabilidade.Conferencia> _notas)
        {
            return new ConferenciaNotasPelaContabilidadeDAO().Gravar(_lista, _notas);
        }
    }
}
