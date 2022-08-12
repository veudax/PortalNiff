create or replace view pbi_dashturnover as
Select EmpFil
     , Sum(Total_func) Total_func
     , Sum(qtd) qtd
     , Sum(previsto) previsto
     , CodArea
     , Sum(MaxRealizado) MaxRealizado
     , Sum(MinRealizado) MinRealizado
     , Sum(Admitidos) Admitidos
     , Sum(Demidos) Demidos
From (Select 0 qtd
           , 0 CodArea --decode( idMetas, 30, 20, 31, 30, 28, 40, 00) CodArea
           , e.codigoglobus empfil
           , 0 Total_func
           , Nvl(previsto,0) Previsto
           , 0 MaxRealizado
           , 0 MinRealizado
           , 0 Admitidos
           , 0 Demidos
        From niff_ads_valoresmetas v, niff_chm_empresas e
       Where referencia = to_char(Sysdate,'yyyymm')
         And idmetas In (13)--, 28, 30, 31)
         And v.idempresa = e.Idempresa

         Union All
        Select 0 qtd_ocorr
             , 0 CodArea --decode( idMetas, 30, 20, 31, 30, 28, 40, 00) CodArea
             , e.codigoglobus empfil
             , 0 Previsto
             , 0 Total_func
             , Max(Realizado) MaxRealizado
             , 0 MinRealizado
             , 0 Admitidos
             , 0 Demidos
          From niff_ads_valoresmetas v, niff_chm_empresas e
         Where referencia Between To_Char(Add_Months(Sysdate, -12), 'yyyymm') And  To_Char(Add_Months(Sysdate,0), 'yyyymm')
           And idmetas In (13) --, 26, 27, 29)
           And v.idempresa = e.Idempresa
         Group By e.codigoglobus --  decode( idMetas, 30, 20, 31, 30, 28, 40, 00), e.codigoglobus

         Union All
        Select 0 qtd_ocorr
             , 0 CodArea --decode( idMetas, 30, 20, 31, 30, 28, 40, 00) CodArea
             , e.codigoglobus empfil
             , 0 Previsto
             , 0 Total_func
             , 0 MaxRealizado
             , Max(Realizado) MinRealizado
             , 0 Admitidos
             , 0 Demidos
          From niff_ads_valoresmetas v, niff_chm_empresas e
         Where referencia Between To_Char(Add_Months(Sysdate, -12), 'yyyymm') And  To_Char(Add_Months(Sysdate,0), 'yyyymm')
           And idmetas In (13) --, 26, 27, 29)
           And v.idempresa = e.Idempresa
           And v.Realizado > 0
         Group By e.codigoglobus --decode( idMetas, 30, 20, 31, 30, 28, 40, 00), e.codigoglobus

       Union All
      Select Nvl(Sum(a.total_Admitidos),0) + Nvl(Sum(a.total_Demitidos),0) qtd
           , decode(a.CODAREA, Null, e.CODAREA, a.CODAREA) codarea
           , e.EmpFil
           , Max(e.Total_func) Total_func
           , 0 Previsto
           , 0 MaxRealizado
           , 0 MinRealizado
           , Nvl(Sum(a.total_Admitidos),0) Admitidos
           , Nvl(Sum(a.total_Demitidos),0) Demidos

  From ( Select Empresa, Empfil, Max(Total_Func) Total_Func, Data, decode(CodArea, 80 ,20, CodArea) CodArea
           From (select 'X' empresa
                      , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                      , count(*) total_func
                      , Last_day((Case When trunc(ff.competficha) > Trunc(Sysdate) then Trunc(Sysdate) Else trunc(ff.competficha) End)) Data
                      , f.codarea
                   from PBI_vwFuncionarios f, flp_fichafinanceira ff
                  where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                    and ff.competficha Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and last_day(sysdate)
                    and ff.codintfunc = f.codintfunc
                    and ff.situacaoffinan = 'A'
                    and (ff.tipofolha = 1)
                    And f.CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                            571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                            692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913)
                  group by ff.competficha, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0'), CodArea
                  Union All -- para trazer a quantidade do mês anterior quando não tiver calculo no mês atual
                 Select Empresa, EmpfIl, Sum(Total_func) Total_func, Data, CodArea
                   From (Select Empresa, EmpfIl, Sum(total_Admitidos) - Sum(total_Demitidos) Total_func, Data, CodArea
                           From pbi_dashTurnoverAux
                          Group By Empresa, EmpFil, Data, CodArea
                          Union All
                         select 'X' empresa
                              , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                              , count(*) total_func
                              , Last_Day(trunc(Sysdate)) Data
                              , f.codarea
                           from PBI_vwFuncionarios f, flp_fichafinanceira ff
                          where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                            and ff.competficha = Last_day(ADD_MONTHS(Trunc(sysdate),-1))
                            and ff.codintfunc = f.codintfunc
                            and ff.situacaoffinan = 'A'
                            and (ff.tipofolha = 1)
                            And f.CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                                    571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                                    692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913)
                            And f.codintfunc Not In (select f.CodintFunc
                                                       from PBI_vwFuncionarios f, flp_fichafinanceira ff
                                                      where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                                                        and ff.competficha between Last_day(ADD_MONTHS(Trunc(sysdate),-1))+1 and last_day(sysdate)
                                                        and ff.codintfunc = f.codintfunc
                                                        and ff.situacaoffinan = 'A'
                                                        and (ff.tipofolha = 1)
                                                        And f.CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                                    571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                                    692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913)
                                                      group by f.CodintFunc)
                         group by ff.competficha, CodArea,
                                  lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0'))
                    Group By Empresa, Empfil, Data, CodArea )
          Group By  Empresa, Empfil, Data, CodArea) e
     , (Select Empresa, EmpFil, Sum(total_Admitidos) total_Admitidos, Sum(total_Demitidos) total_Demitidos, Data, CodArea
          From pbi_dashTurnoverAux
         Group By Empresa, EmpFil, Data, CodArea ) A
 Where a.EmpFil(+) = e.EmpFil
   And a.Data(+) = e.Data
   And a.CodArea(+) = e.CodArea
 Group By e.empresa, e.EmpFil, e.Data, decode(a.CODAREA, Null, e.CODAREA, a.CODAREA)
)
Group By EmpFil, CodArea

