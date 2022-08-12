using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LivrosBO
    {
        public string Sugestoes(bool ebook)
        {
            List<Livros> _livros = new LivrosDAO().Sugestoes(ebook);

            int[] arr = new int[_livros.Count()];
            int i = 1;
            string sugestoes = "";

            Publicas.RetornaPosicaoAleatoria(_livros.Count(), 1, arr);
            
            while (i <= 5)
            {
                int cont = 1;
                int pos = arr[i];

                foreach (var item in _livros)
                {
                    if (cont != pos)
                    {
                        cont++;
                        continue;
                    }

                    sugestoes = sugestoes + " * " + item.Nome + " </br>" ;
                    break;
                }
                i++;
            }
                 
            return sugestoes;
        }

        public int QuantidadeLivros(bool ebook)
        {
            return new LivrosDAO().QuantidadeLivros(ebook);
        }

        public List<Livros> Listar(bool apenasAtivos = false, bool naoLidos = false)
        {
            return new LivrosDAO().Listar(apenasAtivos, naoLidos);
        }
                
        public Ebook ConsultaEbook(int codigo)
        {
            return new LivrosDAO().ConsultaEbook(codigo);
        }

        public Livros Consultar(int codigo)
        {
            return new LivrosDAO().Consulta(codigo);
        }

        public bool Gravar(Livros livro)
        {
            return new LivrosDAO().Grava(livro);
        }

        public bool Excluir(int codigo)
        {
            return new LivrosDAO().Exclui(codigo);
        }

        public int Proximo()
        {
            return new LivrosDAO().Proximo();
        }

        public bool AtualizaQuantidadeDownload(int idLivro)
        {
            return new LivrosDAO().AtualizaQuantidadeDownload(idLivro);
        }

        public bool GravarLeitura(Leitura _livros)
        {
            return new LivrosDAO().GravaLeitura(_livros);
        }

        public Leitura ConsultarLeitura(int codigo)
        {
            return new LivrosDAO().ConsultaLeitura(codigo);
        }
    }
}
