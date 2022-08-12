select lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , f.CODAREA
     , f.descarea Area
     , f.CODFUNCAO CodFuncao
     , f.descfuncao Funcao
     , f.CODFUNC registro
     , f.NomeFunc funcionario
     , ac.codevento
     , e.desceven evento
     , ac.competacumu competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , AC2.MESaNO Periodo
--     , To_Char(ff.datafim,'mm/yyyy') Periodo
--     , To_Char(ff.datafim,'yyyymm') PeriodoOrdem
--     , ff.datafim
  from frq_acumulado ac
--     , Pbi_PeriodoFrequencia ff
     , ( select ac2.codintfunc
              , to_char(g.datafim,'mm/yyyy') mesano
              , max( g.datafim ) competacumu
           from frq_acumulado ac2
              , flp_variaveis_frq fr2
              , flp_funcionarios f     
              , Pbi_PeriodoFrequencia g
          where ac2.codevento in (1,5)
            and fr2.codintfunc = ac2.codintfunc
            and fr2.competacumu = ac2.competacumu
            and g.datafim between '21-Dec-2017' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
            And g.codigoempresa = f.codigoempresa
            And ac2.competacumu Between g.datainicio And g.datafim   
            And f.codintfunc = ac2.codintfunc      
            And f.codigoempresa || f.codigofl In 41--(11,12,21,31,41,51,61,92,131,261,263)                  
          group by ac2.codintfunc
              , to_char(g.datafim,'mm/yyyy') ) ac2
     , vw_funcionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
--   and to_char(ff.datafim,'mm/yyyy') = mesano
--   And FF.DATAFIM = AC2.competacumu
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In 41--(11,12,21,31,41,51,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-dec-2017' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
--   And ff.codigoempresa = f.codigoempresa
--   And ac.competacumu Between ff.datainicio And ff.datafim
