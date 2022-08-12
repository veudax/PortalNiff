using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ResenhaLivrosBO
    {
        public List<ResenhaLivros> Listar(bool resenhasLiberadas = false, int colaborador = 0)
        {
            return new ResenhaLivrosDAO().Listar(resenhasLiberadas, colaborador);
        }

        public List<ResenhaLivros> ListarComResenha(bool mostraSinopse)
        {
            return new ResenhaLivrosDAO().ListarComResenha(mostraSinopse);
        }        

        public List<ResenhaLivros> ListarLivroSemResenha(int colaborador = 0)
        {
            return new ResenhaLivrosDAO().ListarLivroSemResenha(colaborador);
        }

        public List<PerguntasLivros> Listar(int IdResenha, int IdLivro = 0)
        {
            return new ResenhaLivrosDAO().ListarPerguntas(IdResenha, IdLivro);
        }

        public List<RespostasLivros> ListarRespostas(int IdColaborador, int idLivro)
        {
            return new ResenhaLivrosDAO().ListarRespostas(IdColaborador, idLivro);
        }

        public List<RespostasLivros> ListarLivrosComRespostas(bool rh)
        {
            return new ResenhaLivrosDAO().ListarLivrosComRespostas(rh);
        }

        public List<ResenhaLivros> ListarPontuacao(int IdColaborador = 0, bool mostraLivros = false)
        {
            return new ResenhaLivrosDAO().ListarPontuacao(IdColaborador, mostraLivros);
        }

        public ResenhaLivros Consultar(int codigo, int idColaborado, bool diferente)
        {
            return new ResenhaLivrosDAO().Consulta(codigo, idColaborado, diferente);
        }

        public ResenhaLivros Consultar(int livro, int colaborador)
        {
            return new ResenhaLivrosDAO().Consulta(livro, colaborador);
        }

        public bool Gravar(ResenhaLivros _livros, List<PerguntasLivros> _perguntas)
        {
            return new ResenhaLivrosDAO().Grava(_livros, _perguntas);
        }

        public bool Gravar(List<RespostasLivros> _respostas)
        {
            return new ResenhaLivrosDAO().GravaRespostas(_respostas);
        }

        public bool Excluir(int codigo)
        {
            return new ResenhaLivrosDAO().Exclui(codigo);
        }
    }
}
