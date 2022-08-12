-- Ficha F
Select * From (
Select Distinct f.Codfunc, ff.competficha, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC,
                f.Codintfunc,
                FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, Null,Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit,
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
   And Ff.Competficha Between '01-jan-2018'    and '31-dec-2018'
   And Ff.Codintfunc = f.Codintfunc
   And (Ff.Situacaoffinan = 'F')
   And (Ff.Tipofolha = 1)
   And (f.DTTRANSFFUNC Is Null Or f.DTTRANSFFUNC <= ff.competficha)
   And Ff.Codintfunc In
       (Select Codintfunc
          From Flp_Afastados a
         Where Dtretafast  = (Select Max(Dtretafast) 
                                From Flp_Afastados 
                               Where ((Dtretafast >= (ADD_MONTHS(last_day(ff.competficha)+1,-1)) And Dtretafast < ff.competficha)
                                  Or (Dtafast < ff.competficha And Dtretafast Is Null))
                                 And codintfunc = f.CODINTFUNC  )
           And codintfunc = f.CODINTFUNC   )
                )
Where codarea = 40
Order By codfunc; 
/*
Select * 
          From Flp_Afastados a
         Where Dtretafast  = (Select Max(Dtretafast) 
                                From Flp_Afastados 
                               Where (Dtretafast Between '01-feb-2018' And  '28-feb-2020' 
                                  Or (Dtafast < '28-feb-2020' And Dtretafast Is Null))
                                 And codintfunc = 5197   )
           And codintfunc = 5197    */