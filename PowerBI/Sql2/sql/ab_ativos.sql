-- Ativos
Select * From (
Select Distinct f.Codfunc,
                f.Codintfunc,
                Fc_Niif_Retornaultimaarea(f.Codintfunc,
                                          Last_Day((Case
                                                     When Trunc(Ff.Competficha) > Trunc(Sysdate) Then
                                                      Trunc(Sysdate)
                                                     Else
                                                      Trunc(Ff.Competficha)
                                                   End)),
                                          f.Codarea) Codarea

  From Pbi_Vwfuncionarios f, Flp_Fichafinanceira Ff

 Where f.Codigoempresa || f.Codigofl In 21
   And Ff.Competficha Between '01-jun-2018'    and '30-jun-2018'
   And Ff.Codintfunc = f.Codintfunc
   And (Ff.Situacaoffinan = 'A')
   And (Ff.Tipofolha = 1)
   And (f.DTTRANSFFUNC Is Null Or f.DTTRANSFFUNC <= ff.competficha)
   And Ff.Codintfunc Not In
       (Select Codintfunc
          From Flp_Afastados a
         Where (Ff.Competficha Between Dtafast And Dtretafast Or
               (Dtafast < Ff.Competficha And Dtretafast Is Null))) )
Where codarea = 40
Order By codfunc