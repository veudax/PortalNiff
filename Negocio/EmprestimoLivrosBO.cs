using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EmprestimoLivrosBO
    {
        public List<EmprestimoLivros> Listar(bool comDevolucaoEm5Dias = false, int colaborador = 0)
        {
            return new EmprestimoLivrosDAO().Listar(comDevolucaoEm5Dias, colaborador);
        }

        public EmprestimoLivros Consultar(int codigo, int colaborador)
        {
            return new EmprestimoLivrosDAO().Consulta(codigo, colaborador);
        }

        public EmprestimoLivros Consultar(int codigo)
        {
            return new EmprestimoLivrosDAO().Consulta(codigo);
        }

        public bool Gravar(EmprestimoLivros livro)
        {
            return new EmprestimoLivrosDAO().Grava(livro);
        }

        public bool Devolucao(EmprestimoLivros _livros)
        {
            return new EmprestimoLivrosDAO().Devolucao(_livros);
        }

        public bool Excluir(int codigo)
        {
            return new EmprestimoLivrosDAO().Exclui(codigo);
        }

        public int Proximo()
        {
            return new EmprestimoLivrosDAO().Proximo();
        }

        public List<EmprestimoLivros> ListarDownload(int colaborador)
        {
            return new EmprestimoLivrosDAO().ListarDownload(colaborador);
        }
    }
}
