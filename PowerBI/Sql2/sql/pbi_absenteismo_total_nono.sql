create or replace view pbi_absenteismo_total as
select decode(a.EmpFil, Null, b.EmpFil, a.EmpFil) EmpFil, 'X' Empresa
     , Nvl(Sum(a.qtd_ocorr),0) qtd_ocorr
     , Decode(a.dthist, Null, b.Data, a.dthist) dthist
     , decode(a.CODAREA, Null, b.CODAREA, a.CODAREA) CODAREA
     , Max(total_func) total_func
  From (select count(fu.nomefunc) qtd_ocorr
             , Last_day(trunc(t.dthist)) dthist
             , fu.situacaofunc
             , fu.CODAREA
             , Lpad(fu.Codigoempresa, 3, '0') || '/' || Lpad(Decode(fu.Codigoempresa, 9, Decode(fu.Codigofl, 2, 1, fu.Codigofl), fu.Codigofl), 3, '0') EmpFil
          from flp_historico t, frq_ocorrencia f, PBI_vwFuncionarios fu
         where f.codocorr = t.codocorr
           and fu.Codigoempresa || fu.Codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
           And ((Trunc(t.dthist) BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr In (3,238,505,504,506,507,521,526,534,535,541,542,579,582,560,584,580,581,583,592,593,527,601,602,608,609))
            Or (Trunc(t.dthist) BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and Sysdate
           And fu.CODAREA = 40
           and f.codocorr In (552,553))
--            Or (Trunc(t.dthist) BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
--           And fu.CODAREA = 40
--           And f.codocorr In (5, 235, 303))
--            Or (Trunc(t.dthist) BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
--           And f.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,902,903))
            )
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20,30,40,80)
         group by Last_day(trunc(t.dthist))
             , fu.situacaofunc
             , fu.CODAREA
             , Lpad(fu.Codigoempresa, 3, '0') || '/' || Lpad(Decode(fu.Codigoempresa, 9, Decode(fu.Codigofl, 2, 1, fu.Codigofl), fu.Codigofl), 3, '0')) a,
       (Select max(Total_func) Total_func, Data, CodArea, EmpFil
          From (Select Empresa, Empfil, Max(Total_Func) Total_Func, Data, CodArea
           From (select 'X' empresa
                      , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                      , count(*) total_func
                      , Last_day((Case When trunc(ff.competficha) > Trunc(Sysdate) then Trunc(Sysdate) Else trunc(ff.competficha) End)) Data
                      , f.CodArea
                   from PBI_vwFuncionarios f, flp_fichafinanceira ff
                  where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                    and ff.competficha between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and last_day(sysdate) -- ADD_MONTHS(Trunc(Sysdate,'rr'), -96) and sysdate
                    and ff.codintfunc = f.codintfunc
                    and (ff.situacaoffinan = 'A' Or (ff.situacaoffinan = 'F' And ff.codcondi In (36,37,38)))
                    and (ff.tipofolha = 1)
                  group by ff.competficha, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0'), CodArea
                  Union All -- para trazer a quantidade do mês anterior quando não tiver calculo no mês atual
                 Select Empresa, EmpfIl, Sum(Total_func) Total_func, Data, CodArea
                   From (Select Empresa, EmpfIl, Sum(total_Admitidos) - Sum(total_Demitidos) Total_func, Data
                              , CodArea
                           From pbi_funcAtivosdemitidoAb
                          Group By Empresa, EmpFil, Data, CodArea
                          Union All
                         select 'X' empresa
                              , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                              , count(*) total_func
                              , Last_Day(trunc(Sysdate)) Data
                              , f.CODAREA
                           from PBI_vwFuncionarios f, flp_fichafinanceira ff
                          where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                            and ff.competficha = Last_day(ADD_MONTHS(Trunc(sysdate),-1))
                            and ff.codintfunc = f.codintfunc
                            And (ff.situacaoffinan = 'A' Or (ff.situacaoffinan = 'F' And ff.codcondi In (36,37,38)))
                            and (ff.tipofolha = 1)
                            And f.codintfunc Not In (select f.CodintFunc
                                                       from flp_funcionarios f, flp_fichafinanceira ff
                                                      where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                                                        and ff.competficha between Last_day(ADD_MONTHS(Trunc(sysdate),-1))+1 and last_day(sysdate)
                                                        and ff.codintfunc = f.codintfunc
                                                        --and ff.situacaoffinan = 'A'
                                                        And (ff.situacaoffinan = 'A' Or (ff.situacaoffinan = 'F' And ff.codcondi In (36,37,38)))
                                                        and (ff.tipofolha = 1)
                                                      group by f.CodintFunc)
                         group by ff.competficha,
                                  lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0'), CodArea)
                    Group By Empresa, Empfil, Data, codArea )
          Group By  Empresa, Empfil, Data, CodArea  )
          Group By  Empresa, Empfil, Data, CodArea                  ) B
 Where To_char(b.Data,'mm/yyyy') = To_char(a.dthist(+),'mm/yyyy')
   And b.CodArea = a.CodArea(+)
   And b.EmpFil = a.EmpFil(+)
 Group By decode(a.EmpFil, Null, b.EmpFil, a.EmpFil)
     , Decode(a.dthist, Null, b.Data, a.dthist)
     , decode(a.CODAREA, Null, b.CODAREA, a.CODAREA)

