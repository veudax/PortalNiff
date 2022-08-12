using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class RamaisBO
    {
        public List<Telefone> ListarTelefones(int idEmpresa)
        {
            return new RamaisDAO().ListarTelefones(idEmpresa);
        }

        public List<Ramais> ListarRamais(int idTelefone)
        {
            return new RamaisDAO().ListarRamais(idTelefone);
        }

        public List<RamaisAssociadosAoColaborador> ListarColaboradoresAssociados(int idRamal)
        {
            return new RamaisDAO().ListarColaboradoresAssociados(idRamal);
        }

        public List<LocalizaRamais> ListarRamais()
        {
            return new RamaisDAO().ListarRamais();
        }

        public Telefone Consultar(int id)
        {
            return new RamaisDAO().Consultar(id);
        }

        public Telefone Consultar(int idEmpresa, decimal Telefone)
        {
            return new RamaisDAO().Consultar(idEmpresa, Telefone);
        }

        public Ramais ConsultarRamal(int idTelefone, int numero)
        {
            return new RamaisDAO().ConsultarRamal(idTelefone, numero);
        }

        public bool Gravar(Telefone tel, Ramais item, List<RamaisAssociadosAoColaborador> colaboradores)
        {
            return new RamaisDAO().Gravar(tel, item, colaboradores);
        }

        public bool GravarAssociacao(List<RamaisAssociadosAoColaborador> colaboradores)
        {
            return new RamaisDAO().GravarAssociacao(colaboradores);
        }

        public bool Excluir(int Id)
        {
            return new RamaisDAO().Excluir(Id);
        }

        public bool ExcluirAssociacao(int IdColaborador)
        {
            return new RamaisDAO().ExcluirAssociacao(IdColaborador);
        }

        public RamaisAssociadosAoColaborador ColaboradoresAssociados(int IdColaborador)
        {
            return new RamaisDAO().ColaboradoresAssociados(IdColaborador);
        }
    }
}
