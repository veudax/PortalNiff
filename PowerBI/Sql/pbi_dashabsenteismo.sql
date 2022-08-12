create or replace view pbi_dashabsenteismo as
select Empfil
     , Nvl(Sum(qtd_ocorr),0) qtd_ocorr
     , CODAREA
     , Max(Total_func) total_func
     , Sum(Nvl(previsto,0)) previsto
     , Sum(MaxRealizado) MaxRealizado
     , Sum(MinRealizado) MinRealizado
  From (Select 0 qtd_ocorr
             , 0 CodArea --decode( idMetas, 29, 20, 27, 30, 26, 40) CodArea
             , e.codigoglobus empfil
             , Nvl(previsto,0) Previsto
             , 0 Total_func
             , 0 MaxRealizado
             , 0 MinRealizado
          From niff_ads_valoresmetas v, niff_chm_empresas e
         Where referencia = to_char(Sysdate,'yyyymm')
           And idmetas In (12) --, 26, 27, 29)
           And v.idempresa = e.Idempresa

         Union All
        Select 0 qtd_ocorr
             , 0 CodArea --decode( idMetas, 29, 20, 27, 30, 26, 40) CodArea
             , e.codigoglobus empfil
             , 0 Previsto
             , 0 Total_func
             , Max(Realizado) MaxRealizado
             , 0 MinRealizado
          From niff_ads_valoresmetas v, niff_chm_empresas e
         Where referencia Between To_Char(Add_Months(Sysdate, -12), 'yyyymm') And  To_Char(Add_Months(Sysdate,0), 'yyyymm')
           And idmetas In (12) --, 26, 27, 29)
           And v.idempresa = e.Idempresa
         Group By e.codigoglobus --  decode( idMetas, 29, 20, 27, 30, 26, 40), e.codigoglobus

         Union All
        Select 0 qtd_ocorr
             , 0 CodArea --decode( idMetas, 29, 20, 27, 30, 26, 40) CodArea
             , e.codigoglobus empfil
             , 0 Previsto
             , 0 Total_func
             , 0 MaxRealizado
             , Max(Realizado) MinRealizado
          From niff_ads_valoresmetas v, niff_chm_empresas e
         Where referencia Between To_Char(Add_Months(Sysdate, -12), 'yyyymm') And  To_Char(Add_Months(Sysdate,0), 'yyyymm')
           And idmetas In (12) --, 26, 27, 29)
           And v.idempresa = e.Idempresa
           And v.realizado > 0
         Group By e.codigoglobus --decode( idMetas, 29, 20, 27, 30, 26, 40), e.codigoglobus

         Union All
       (Select  Nvl(Sum(a.qtd_ocorr),0)
             , decode(a.CODAREA, Null, b.CODAREA, a.CODAREA) CODAREA
             , decode(a.EmpFil, Null, b.EmpFil, a.EmpFil) EmpFil
             , Sum(Previsto) Previsto
             , max(Total_func) Total_func
             , 0 MaxRealizado
             , 0 MinRealizado
          From (select count(fu.nomefunc) qtd_ocorr
                     , fu.CODAREA
                     , Lpad(fu.Codigoempresa, 3, '0') || '/' || Lpad(Decode(fu.Codigoempresa, 9, Decode(fu.Codigofl, 2, 1, fu.Codigofl), fu.Codigofl), 3, '0') EmpFil
                     , 0 Previsto
                  from flp_historico t, frq_ocorrencia f, PBI_vwFuncionarios fu
                 where f.codocorr = t.codocorr
                   and fu.Codigoempresa || fu.Codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                   And ((Trunc(t.dthist) Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and Sysdate
                   and f.codocorr In (3,238,505,504,506,507,521,526,534,535,541,542,579,582,560,584,580,581,583,592,593,527,601,602,608,609))
                    Or (Trunc(t.dthist) Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and Sysdate
                   And fu.CODAREA = 40
                   and f.codocorr In (552,553)) )
                   and t.codintfunc = fu.codintfunc
                   and fu.CODAREA in (20,30,40,80)
                 group by Last_day(trunc(t.dthist))
                     , fu.situacaofunc
                     , fu.CODAREA
                     , Lpad(fu.Codigoempresa, 3, '0') || '/' || Lpad(Decode(fu.Codigoempresa, 9, Decode(fu.Codigofl, 2, 1, fu.Codigofl), fu.Codigofl), 3, '0')) a,
               (Select max(Total_func) Total_func, CodArea, EmpFil
                  From (Select Empresa, Empfil, Max(Total_Func) Total_Func, Data, CodArea
                          From (select 'X' empresa
                                     , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                                     , count(*) total_func
                                     , Last_day((Case When trunc(ff.competficha) > Trunc(Sysdate) then Trunc(Sysdate) Else trunc(ff.competficha) End)) Data
                                     , f.CodArea
                                  from PBI_vwFuncionarios f, flp_fichafinanceira ff
                                 where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                                   and ff.competficha Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and last_day(sysdate)
                                   and ff.codintfunc = f.codintfunc
                                   and ff.situacaoffinan = 'A'
                                   and (ff.tipofolha = 1)
                                 group by ff.competficha, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0'), CodArea
                                 Union All -- para trazer a quantidade do mês anterior quando não tiver calculo no mês atual
                                Select Empresa, EmpfIl, Sum(Total_func) Total_func, Data, CodArea
                                  From (Select Empresa, EmpfIl, Sum(total_Admitidos) - Sum(total_Demitidos) Total_func, Data, CodArea
                                          From pbi_DashAbsenteismoAux
                                         Group By Empresa, EmpFil, Data, CodArea
                                         Union All
                                        Select 'X' empresa
                                             , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                                             , count(*) total_func
                                             , Last_Day(trunc(Sysdate)) Data
                                             , f.CODAREA
                                          from PBI_vwFuncionarios f, flp_fichafinanceira ff
                                         where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                                           and ff.competficha = Last_day(ADD_MONTHS(Trunc(sysdate),-1))
                                           and ff.codintfunc = f.codintfunc
                                           and ff.situacaoffinan = 'A'
                                           and (ff.tipofolha = 1)
                                           And f.codintfunc Not In (select f.CodintFunc
                                                                      from flp_funcionarios f, flp_fichafinanceira ff
                                                                     where f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
                                                                       and ff.competficha between Last_day(ADD_MONTHS(Trunc(sysdate),-1))+1 and last_day(sysdate)
                                                                       and ff.codintfunc = f.codintfunc
                                                                       and ff.situacaoffinan = 'A'
                                                                       and (ff.tipofolha = 1)
                                                                     group by f.CodintFunc)
                                        group by ff.competficha,
                                                  lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0'), CodArea)
                                 Group By Empresa, Empfil, Data, codArea )
                         Group By  Empresa, Empfil, Data, CodArea  )
                 Group By  Empresa, Empfil, Data, CodArea ) B
         Where b.CodArea = a.CodArea(+)
           And b.EmpFil = a.EmpFil(+)
         Group By decode(a.EmpFil, Null, b.EmpFil, a.EmpFil)
             , decode(a.CODAREA, Null, b.CODAREA, a.CODAREA)))
 Group By Empfil, CODAREA

