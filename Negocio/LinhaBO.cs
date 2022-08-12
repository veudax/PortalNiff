using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LinhaBO
    {
        public List<Linha> Listar(string empresaGlobus)
        {
            return new LinhaDAO().Listar(empresaGlobus);
        }

        public List<Linha> ListarPesquisa(string empresaGlobus)
        {
            return new LinhaDAO().ListarPesquisa(empresaGlobus);
        }

        public Linha Consultar(int codigo)
        {
            return new LinhaDAO().Consultar(codigo);
        }

        public Linha Consultar(string empresaGlobus, string linha)
        {
            return new LinhaDAO().Consultar(empresaGlobus, linha);
        }

        public Linha Consultar(string codigo, bool ativas)
        {
            return new LinhaDAO().Consultar(codigo, ativas);
        }
        
        public List<SecaoDaLinha> Listar(int IdLinha)
        {
            return new LinhaDAO().Listar(IdLinha);
        }
    }
}
