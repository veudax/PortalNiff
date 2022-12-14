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
           and f.codocorr In (552,553))   )
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20,30,40,80)
         group by Last_day(trunc(t.dthist))
             , fu.situacaofunc
             , fu.CODAREA
             , Lpad(fu.Codigoempresa, 3, '0') || '/' || Lpad(Decode(fu.Codigoempresa, 9, Decode(fu.Codigofl, 2, 1, fu.Codigofl), fu.Codigofl), 3, '0')) a,
       (Select max(Total_func) Total_func, Data, CodArea, EmpFil
          From (Select Empresa, Empfil, Max(Total_Func) Total_Func, Data, CodArea
                  From (Select Empresa, EmpfIl, Count(*) Total_Func, Data, To_number(CodArea) CodArea
                          From (Select 'X' Empresa,
                                       Lpad(f.Codigoempresa, 3, '0') || '/' ||
                                       Lpad(Decode(f.Codigoempresa,
                                                   9,
                                                   Decode(f.Codigofl, 2, 1, f.Codigofl),
                                                   f.Codigofl),
                                            3,
                                            '0') Empfil,
                                       Last_Day((Case
                                                  When Trunc(Ff.Competficha) > Trunc(Sysdate) Then
                                                   Trunc(Sysdate)
                                                  Else
                                                   Trunc(Ff.Competficha)
                                                End)) Data,
                                       f.Codfunc,
                                       f.Codintfunc,
                                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit,
                                       Fc_Niif_Retornaultimaarea(f.Codintfunc,
                                                                 Last_Day((Case
                                                                            When Trunc(Ff.Competficha) > Trunc(Sysdate) Then
                                                                             Trunc(Sysdate)
                                                                            Else
                                                                             Trunc(Ff.Competficha)
                                                                          End)), f.codArea) Codarea

                                  From Pbi_Vwfuncionarios f, Flp_Fichafinanceira Ff

                                 Where f.Codigoempresa || f.Codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                                   And Ff.Competficha Between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and last_day(sysdate) -- ADD_MONTHS(Trunc(Sysdate,'rr'), -96) and sysdate
                                   And Ff.Codintfunc = f.Codintfunc
                                   And (Ff.Tipofolha = 1)
                                )
                         Where codArea In (20,30,40)
                           And Sit = 'A'
                         Group By Empresa, Empfil, Data, Codarea

                  Union All -- para trazer a quantidade do m?s anterior quando n?o tiver calculo no m?s atual


                 Select Empresa, EmpfIl, Sum(Total_func) Total_func, Data, CodArea
                   From (Select Empresa, EmpfIl, Count(*) Total_Func, Last_day(trunc(Sysdate)) Data, To_number(CodArea) CodArea
                           From (Select f.Codfunc,
                                        Fc_Niif_Retornaultimaarea(f.Codintfunc,
                                                                  Last_Day(trunc(Sysdate)),
                                                                  f.Codarea) Codarea,
                                        Func_Trazsituacaofunc(f.Codintfunc,
                                                              Last_Day(trunc(Sysdate)),
                                                              'F',
                                                              Null,
                                                              f.Dtadmfunc,
                                                              f.Situacaofunc,
                                                              f.Dttransffunc) Sit,
                                        'X' Empresa,
                                        Lpad(f.Codigoempresa, 3, '0') || '/' ||
                                        Lpad(Decode(f.Codigoempresa,
                                                    9,
                                                    Decode(f.Codigofl, 2, 1, f.Codigofl),
                                                    f.Codigofl),
                                             3,
                                             '0') Empfil
                                   From Pbi_Vwfuncionarios f
                                  Where f.Codigoempresa || f.Codigofl In (11,12,21,31,41,51,61,92,131,261,263,291))
                          Where Sit = 'A'
                          Group By Codarea, Empresa, EmpFil

                   )
                    Group By Empresa, Empfil, Data, codArea )
          Group By  Empresa, Empfil, Data, CodArea  )
          Group By  Empresa, Empfil, Data, CodArea                  ) B
 Where To_char(b.Data,'mm/yyyy') = To_char(a.dthist(+),'mm/yyyy')
   And b.CodArea = a.CodArea(+)
   And b.EmpFil = a.EmpFil(+)
 Group By decode(a.EmpFil, Null, b.EmpFil, a.EmpFil)
     , Decode(a.dthist, Null, b.Data, a.dthist)
     , decode(a.CODAREA, Null, b.CODAREA, a.CODAREA)

