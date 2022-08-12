create or replace view pbi_dashhorasextras as
Select EmpFil,
       CodArea,
       Round(Sum(minutos)/60,0) minutos,
       Round(Sum(previsto)/60,0) previsto,
       Round(Sum(MaxRealizado)/60,0) MaxRealizado,
       Round(Sum(MinRealizado)/60,0) MinRealizado
  From (-- Mês atual
      Select e.codigoglobus empfil
           , decode( idMetas, 177, 20, 176, 30, 175, 40, 00) CodArea
           , 0 minutos
           , Nvl(previsto,0) Previsto
           , 0 MaxRealizado
           , 0 MinRealizado
        From niff_ads_valoresmetas v, niff_chm_empresas e
       Where referencia = to_char(Sysdate,'yyyymm')
         And idmetas In (175,176,177)
         And v.idempresa = e.Idempresa

       Union All
      Select e.codigoglobus empfil
           , decode( idMetas, 177, 20, 176, 30, 175, 40, 00) CodArea
           , 0 minutos
           , 0 previsto
           , Max(Realizado) MaxRealizado
           , 0 MinRealizado

        From niff_ads_valoresmetas v, niff_chm_empresas e
       Where referencia Between To_Char(Add_Months(Sysdate, -12), 'yyyymm') And  To_Char(Add_Months(Sysdate,0), 'yyyymm')
         And idmetas In (175,176,177)
         And v.idempresa = e.Idempresa
       Group By e.codigoglobus, decode( idMetas, 177, 20, 176, 30, 175, 40, 00)

       Union All
      Select e.codigoglobus empfil
           , decode( idMetas, 177, 20, 176, 30, 175, 40, 00) CodArea
           , 0 minutos
           , 0 previsto
           , 0 MaxRealizado
           , Min(Realizado) MinRealizado
        From niff_ads_valoresmetas v, niff_chm_empresas e
       Where referencia Between To_Char(Add_Months(Sysdate, -12), 'yyyymm') And  To_Char(Add_Months(Sysdate,-1), 'yyyymm')
         And idmetas In (175,176,177)
         And v.idempresa = e.Idempresa
       Group By e.codigoglobus, decode( idMetas, 177, 20, 176, 30, 175, 40, 00)

      Union All

      -- para janeiro de qualquer ano
     select EmpFil
          , CODAREA
          , Sum(minutos) Minutos
          , 0 Previsto
          , 0 MaxRealizado
          , 0 MinRealizado
       from pbi_he_jan3anobarras t
      Where to_char(competacumu,'yyyymm') = to_Char(Sysdate,'yyyymm')
      Group By EmpFil
          , CODAREA

     Union All
      -- para fev a nov de qualquer ano
     select EmpFil
          , CODAREA
          , Sum(minutos) Minutos
          , 0 Previsto
          , 0 MaxRealizado
          , 0 MinRealizado
       from pbi_he_demaismeses3anobarras t
      Where to_char(competacumu,'yyyymm') = to_Char(Sysdate,'yyyymm')
      Group By EmpFil
          , CODAREA

     Union All
      -- para dezembro de qualquer ano
     select EmpFil
          , CODAREA
          , Sum(minutos) Minutos
          , 0 Previsto
          , 0 MaxRealizado
          , 0 MinRealizado
       from pbi_he_dez3anobarras t
      Where to_char(competacumu,'yyyymm') = to_Char(Sysdate,'yyyymm')
      Group By EmpFil
          , CODAREA

)
  Group By EmpFil, CodArea

