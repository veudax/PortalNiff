Select Sum(TotalIniciado) TotalIniciado, 
       Sum(totalConcluido) totalConcluido,
       Sum(TotalNaoIniciado) TotalNaoIniciado
  From (
Select Count(*) totalIniciado, 0 totalConcluido, 0 TotalNaoIniciado
  From Niff_Ads_Colaboradores c
     , niff_Ads_Avaliacao a
 Where c.idcolaborador = a.Idcolaborador
   And a.datafim Is Null
Union All 
Select 0 totalIniciado, Count(*) totalConcluido, 0 TotalNaoIniciado
  From Niff_Ads_Colaboradores c
     , niff_Ads_Avaliacao a
 Where c.idcolaborador = a.Idcolaborador
   And a.datafim Is Not Null   
Union All 
Select 0 totalIniciado, 0 totalConcluido, Count(*) TotalNaoIniciado
  From Niff_Ads_Colaboradores c
     , niff_Ads_Avaliacao a
 Where c.idcolaborador = a.Idcolaborador(+) )

   