Select c.Nome
 From Niff_Ads_Colaboradores c, Niff_Chm_Usuarios u
Where c.Idcolaborador Not In (
Select c.Idcolaborador
--c.nome--, t1.Nome, t2.Nome, t3.nome
  From Niff_Bol_Palpitefinal p, Niff_Ads_Colaboradores c
     , niff_bol_times t1
     , niff_bol_times t2
     , niff_bol_times t3
 Where p.Idcolaborador = c.Idcolaborador
   And p.idtimevencedor = t1.Id
   And p.idtimevice = t2.Id
   And p.idtime3lugar = t3.Id)
And c.idempresa = 1
And u.Idempresa = c.Idempresa
And u.codfunc = c.codintfunc
And u.participabolaocopa ='S'