Select u.nome, to_date(to_char(u.dtnascimento,'dd/mm') || '/2019', 'dd/mm/yyyy') niver From niff_chm_usuarios u
Where to_char(u.dtnascimento,'mmdd') Between '1201' And '1231'
And ativo = 'S'
And idempresa = 19