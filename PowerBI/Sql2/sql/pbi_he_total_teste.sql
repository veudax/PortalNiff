create or replace view pbi_he_total_teste as
select lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , f.CODAREA
     , F.descarea Area
     , f.CODFUNCAO CodFuncao
     , f.descfuncao Funcao
     , f.CODFUNC registro
     , f.NomeFunc funcionario
     , ac.codevento
     , e.desceven evento
     , ac.competacumu competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , To_Char(ac.competacumu,'mm/yyyy') Periodo
     , To_Char(ac.competacumu,'yyyymm') PeriodoOrdem
  from frq_acumulado ac
--     , Pbi_PeriodoFrequencia f
     , ( select ac2.codintfunc
              , to_char(ac2.competacumu,'mm/yyyy') mesano
              , max( ac2.competacumu ) competacumu
           from frq_acumulado ac2
              , flp_variaveis_frq fr2
          where ac2.codevento in (1,5)
            and fr2.codintfunc = ac2.codintfunc
            and fr2.competacumu = ac2.competacumu
            and ac2.competacumu between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          group by ac2.codintfunc
              , to_char(ac2.competacumu,'mm/yyyy')
           ) ac2
     , vw_funcionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))

Union All
select lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , f.CODAREA
     , F.descarea Area
     , f.CODFUNCAO CodFuncao
     , f.descfuncao Funcao
     , f.CODFUNC registro
     , f.NomeFunc funcionario
     , ac.codevento
     , e.desceven evento
     , ac.competacumu competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , To_Char(ac.competacumu,'mm/yyyy') Periodo
     , To_Char(ac.competacumu,'yyyymm') PeriodoOrdem
  from frq_Acumulado ac
     , ( select ac2.codintfunc
              , to_char(ac2.competacumu,'mm/yyyy') mesano
              , max( ac2.competacumu ) competacumu
           from frq_acumulado ac2
          where ac2.codevento in (1,5)
            and ac2.competacumu between ADD_MONTHS(Last_day(trunc(Sysdate)),-1)+1 And Last_day(trunc(Sysdate))
          group by ac2.codintfunc
              , to_char(ac2.competacumu,'mm/yyyy')
           ) ac2
     , vw_funcionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC(+) = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between ADD_MONTHS(Last_day(trunc(Sysdate)),-1)+1 And Last_day(trunc(Sysdate))

Union All
select lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , f.CODAREA
     , F.descarea Area
     , f.CODFUNCAO CodFuncao
     , f.descfuncao Funcao
     , f.CODFUNC registro
     , f.NomeFunc funcionario
     , ac.codevento
     , e.desceven evento
     , ac.competacumu competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , To_Char(ac.competacumu,'mm/yyyy') Periodo
     , To_Char(ac.competacumu,'yyyymm') PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( select ac2.codintfunc
              , to_char(ac2.competacumu,'mm/yyyy') mesano
              , max( ac2.competacumu ) competacumu
           from frq_acumulado ac2
              , flp_variaveis_frq fr2
          where ac2.codevento in (1,5)
            and fr2.codintfunc = ac2.codintfunc
            and fr2.competacumu = ac2.competacumu
            and ac2.competacumu Between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          group by ac2.codintfunc
              , to_char(ac2.competacumu,'mm/yyyy')
           ) ac2
     , vw_funcionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC(+) = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))

