using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class SuprimentosBO
    {
        public List<Suprimentos.Metas> Listar(int idEmpresa)
        {
            return new SuprimentosDAO().Listar(idEmpresa);
        }

        public List<Suprimentos.Metas> Listar(decimal CodIntFunc)
        {
            return new SuprimentosDAO().Listar(CodIntFunc);
        }

        public bool Gravar(List<Suprimentos.Metas> _lista)
        {
            return new SuprimentosDAO().Gravar(_lista);
        }

        public bool ExcluirReferencia(int id)
        {
            return new SuprimentosDAO().ExcluirReferencia(id);
        }

        public bool ExcluirTodos(decimal CodIntFunc)
        {
            return new SuprimentosDAO().ExcluirTodos(CodIntFunc);
        }

        public List<Suprimentos.Pedidos> Listar(int idEmpresa, string referencia, string Status)
        {
            return new SuprimentosDAO().Listar(idEmpresa, referencia, Status);
        }

        public List<Suprimentos.ItensPedido> ListarItens(int idEmpresa, string referencia, string Status)
        {
            return new SuprimentosDAO().ListarItens(idEmpresa, referencia, Status);
        }

        public bool Gravar(List<Suprimentos.Pedidos> _lista)
        {
            return new SuprimentosDAO().Gravar(_lista);
        }
    }
}
