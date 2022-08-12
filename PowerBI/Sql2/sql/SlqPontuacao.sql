Select Nvl(Sum(p.Pontos), 0) + Nvl(Sum(s.Pontuacao), 0) + Nvl(Sum(e.Pontos), 0) Pontos,
       c.Nome Nomecolaborador
  From Niff_Bib_Resenha s,
       Niff_Bib_Livros l,
       Niff_Ads_Colaboradores c,
       (Select Sum(p.Pergunta) Pontos, p.Idresenha
          From Niff_Bib_Respostas r, Niff_Bib_Perguntas p
         Where r.Idpergunta = p.Idpergunta
         Group By p.Idresenha) p,
       (Select Sum(e.pontuacao) Pontos, e.IdColaborador
          From Niff_Bib_Emprestimo e
         Where e.devolvido = 'S'
         Group By e.IdColaborador) e         
 Where s.Idresenha = p.Idresenha(+)
   And e.Idcolaborador = c.Idcolaborador
   And s.Idlivros = l.Idlivros
   And s.Idcolaborador = c.Idcolaborador
 Group By c.Nome