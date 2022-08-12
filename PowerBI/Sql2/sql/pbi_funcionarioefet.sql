create or replace view pbi_funcionarioefet as
Select empresa
    , EmpFil
    , Sum(Total_func) Total_func
    , Sum(Admitidos) Admitidos
    , Sum(Demidos) Demidos
    , (Case When Data > trunc(Sysdate) Then trunc(Sysdate) Else Data   End) Data
From (

Select e.empresa
    , e.EmpFil
    , Max(e.Total_func) Total_func
    , Nvl(Sum(a.total_Admitidos),0) Admitidos
    , Nvl(Sum(a.total_Demitidos),0) Demidos
    , e.Data
  From ( Select Empresa, Empfil, Max(Total_Func) Total_Func, Data
           From (Select Empresa, EmpFil, Count(*) Total_Func, Data
                   From (
                 select 'X' empresa
                      , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                      , Last_day((Case When trunc(ff.competficha) > Trunc(Sysdate) then Trunc(Sysdate) Else trunc(ff.competficha) End)) Data
                      , FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                      , Fc_Niff_Retornafuncaoanterior(f.Codintfunc, to_number(to_char(Last_day((Case When trunc(ff.competficha) > Trunc(Sysdate) then Trunc(Sysdate) Else trunc(ff.competficha) End)),'yyyymm'))) Codfuncao
                   from PBI_vwFuncionarios f, flp_fichafinanceira ff
                  where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                    and ff.competficha between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and last_day(sysdate) -- ADD_MONTHS(Trunc(Sysdate,'rr'), -96) and sysdate
                    and ff.codintfunc = f.codintfunc
                    and (ff.tipofolha = 1))
                  Where Sit = 'A'                  
                    And CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                            571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                            692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913) 
                  group by empresa, EmpFil, Data
                  Union All -- para trazer a quantidade do m�s anterior quando n�o tiver calculo no m�s atual
                 Select Empresa, EmpfIl, Sum(Total_func) Total_func, Data
                   From (Select Empresa, EmpFil, Count(*) Total_Func, Data
                           From (
                         select 'X' empresa
                              , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                              , Last_Day(trunc(Sysdate)) Data
                              , FUNC_TRAZSITUACAOFUNC(f.codintfunc, trunc(Sysdate), 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                              , Fc_Niff_Retornafuncaoanterior(f.Codintfunc, to_number(to_char(Last_day(Trunc(Sysdate)),'yyyymm'))) Codfuncao
                           from PBI_vwFuncionarios f
                          where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291) )
                         Where sit = 'A'
                            And CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                                    571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                                    692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913)                         
                         group by Empresa, EmpFil, Data)
                    Group By Empresa, Empfil, Data )
          Group By  Empresa, Empfil, Data) e
     , (Select Empresa, EmpFil, Sum(total_Admitidos) total_Admitidos, Sum(total_Demitidos) total_Demitidos, Data
          From pbi_FuncionariosAtivosDemitido
         Group By Empresa, EmpFil, Data ) A
 Where a.EmpFil(+) = e.EmpFil
   And a.Data(+) = e.Data
 Group By e.empresa, e.EmpFil, e.Data
)
Group By empresa
    , EmpFil, Data

