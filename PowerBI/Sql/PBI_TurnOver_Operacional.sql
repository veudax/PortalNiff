create or replace view pbi_funcionarioefet_op as
Select e.empresa
    , e.EmpFil
    , Max(e.Total_func) Total_func
    , Nvl(Sum(a.total_Admitidos),0) Admitidos
    , Nvl(Sum(a.total_Demitidos),0) Demidos
    , e.Data
    , e.codarea
  From (Select Empresa, Empfil, Max(Total_Func) Total_Func, Data, CodArea
          From (Select 'X' empresa
                     , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                     , f.codarea
                     , count(*) total_func
                     , Last_day((Case When trunc(ff.competficha) > Trunc(Sysdate) then Trunc(Sysdate) Else trunc(ff.competficha) End)) data
                  from PBI_vwFuncionarios f, flp_fichafinanceira ff
                 where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
                   and ff.competficha between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and last_day(sysdate) -- ADD_MONTHS(Trunc(Sysdate,'rr'), -96) and sysdate
                   and ff.codintfunc = f.codintfunc
                   and ff.situacaoffinan = 'A'
                   and (ff.tipofolha = 1)
                   And f.CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                           571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                           692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913)
                 group by ff.competficha, f.codarea, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
                 Union All -- para trazer a quantidade do mês anterior quando não tiver calculo no mês atual
                Select Empresa, EmpFil, CodArea, Sum(Total_func) Total_func, Data
                  From (Select Empresa, EmpFil, CodArea, Sum(total_Admitidos) - Sum(total_Demitidos) total_func, Data
                          From pbi_FuncionariosAtivosDemitido
                         Group By Empresa, EmpFil, Data, CodArea
                        Union All
                        select 'X' empresa
                             , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                             , f.codarea
                             , count(*) total_func
                             , Last_Day(trunc(Sysdate)) Data
                          from PBI_vwFuncionarios f, flp_fichafinanceira ff
                         where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
                           and ff.competficha = Last_day(ADD_MONTHS(Trunc(sysdate),-1))
                           and ff.codintfunc = f.codintfunc
                           and ff.situacaoffinan = 'A'
                           and (ff.tipofolha = 1)
                           And f.CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                                   571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                                   692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913)
                           And f.codintfunc Not In (select f.CodintFunc
                                                      from PBI_vwFuncionarios f, flp_fichafinanceira ff
                                                     where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
                                                       and ff.competficha between Last_day(ADD_MONTHS(Trunc(sysdate),-1))+1 and last_day(sysdate)
                                                       and ff.codintfunc = f.codintfunc
                                                       and ff.situacaoffinan = 'A'
                                                       and (ff.tipofolha = 1)
                                                       And f.CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                                   571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                                   692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913)
                                                     group by f.CodintFunc)
                        group by ff.competficha, f.codarea, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0'))
                  Group By Empresa, Empfil, CodArea, Data)
          Group By  Empresa, Empfil, CodArea, Data) e
    , (Select Empresa, EmpFil, CodArea, Sum(total_Admitidos) total_Admitidos, Sum(total_Demitidos) total_Demitidos, Data
          From pbi_FuncionariosAtivosDemitido
         Group By Empresa, EmpFil, Data, CodArea) A
 Where a.EmpFil(+) = e.EmpFil
   And a.Data(+) = e.Data
   And a.CodArea(+) = e.CodArea
 Group By e.empresa, e.EmpFil, e.Data, e.codarea

