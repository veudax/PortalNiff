Select c.Nome, r.opcao, sexo 
From niff_bol_enqueteresp r, niff_ads_colaboradores c, niff_bol_enquete p
Where r.Idpergunta =7
  And r.Idcolaborador = c.Idcolaborador
  And p.Id = r.Idpergunta
  And p.idempresa = 1
Order By opcao, sexo, nome