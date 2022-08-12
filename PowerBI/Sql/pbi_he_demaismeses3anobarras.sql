create or replace view pbi_he_demaismeses3anobarras as
select lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , f.CODAREA
     , F.descarea Area
     , f.CODFUNCAO CodFuncao
     , f.descfuncao Funcao
     , f.CODFUNC registro
     , f.NomeFunc funcionario
     , ac.codevento
     , e.desceven evento
     , To_Date('20/02/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '02/2020' Periodo
     , '202002' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-jan-2020' And '20-feb-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-jan-2020' And '20-feb-2020'
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
     , To_Date('20/03/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '03/2020' Periodo
     , '202003' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-feb-2020' And '20-mar-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-feb-2020' And '20-mar-2020'
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
     , To_Date('20/04/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '04/2020' Periodo
     , '202004' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-mar-2020' And '20-apr-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-mar-2020' And '20-apr-2020'
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
     , To_Date('20/05/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '05/2020' Periodo
     , '202005' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-apr-2020' And '20-may-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-apr-2020' And '20-may-2020'
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
     , To_Date('20/06/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '06/2020' Periodo
     , '202006' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-may-2020' And '20-jun-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-may-2020' And '20-jun-2020'
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
     , To_Date('20/07/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '07/2020' Periodo
     , '202007' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-jun-2020' And '20-jul-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-jun-2020' And '20-jul-2020'
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
     , To_Date('20/08/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '08/2020' Periodo
     , '202008' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-jul-2020' And '20-aug-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-jul-2020' And '20-aug-2020'
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
     , To_Date('20/09/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '09/2020' Periodo
     , '202009' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-aug-2020' And '20-sep-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-aug-2020' And '20-sep-2020'
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
     , To_Date('20/10/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '10/2020' Periodo
     , '202010' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-sep-2020' And '20-oct-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-sep-2020' And '20-oct-2020'
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
     , To_Date('20/11/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '11/2020' Periodo
     , '202011' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-oct-2020' And '20-nov-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-oct-2020' And '20-nov-2020'

Union All
-- Suplementar

select lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , f.CODAREA
     , F.descarea Area
     , f.CODFUNCAO CodFuncao
     , f.descfuncao Funcao
     , f.CODFUNC registro
     , f.NomeFunc funcionario
     , ac.codevento
     , e.desceven evento
     , To_Date('20/02/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '02/2020' Periodo
     , '202002' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-jan-2020' And '20-feb-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-jan-2020' And '20-feb-2020'
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
     , To_Date('20/03/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '03/2020' Periodo
     , '202003' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-feb-2020' And '20-mar-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-feb-2020' And '20-mar-2020'
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
     , To_Date('20/04/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '04/2020' Periodo
     , '202004' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-mar-2020' And '20-apr-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-mar-2020' And '20-apr-2020'
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
     , To_Date('20/05/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '05/2020' Periodo
     , '202005' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-apr-2020' And '20-may-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-apr-2020' And '20-may-2020'
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
     , To_Date('20/06/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '06/2020' Periodo
     , '202006' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-may-2020' And '20-jun-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-may-2020' And '20-jun-2020'
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
     , To_Date('20/07/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '07/2020' Periodo
     , '202007' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-jun-2020' And '20-jul-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-jun-2020' And '20-jul-2020'
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
     , To_Date('20/08/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '08/2020' Periodo
     , '202008' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-jul-2020' And '20-aug-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-jul-2020' And '20-aug-2020'
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
     , To_Date('20/09/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '09/2020' Periodo
     , '202009' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-aug-2020' And '20-sep-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-aug-2020' And '20-sep-2020'
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
     , To_Date('20/10/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '10/2020' Periodo
     , '202010' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-sep-2020' And '20-oct-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-sep-2020' And '20-oct-2020'
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
     , To_Date('20/11/2020','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '11/2020' Periodo
     , '202011' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-oct-2020' And '20-nov-2020'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-oct-2020' And '20-nov-2020'

/*select lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , f.CODAREA
     , F.descarea Area
     , f.CODFUNCAO CodFuncao
     , f.descfuncao Funcao
     , f.CODFUNC registro
     , f.NomeFunc funcionario
     , ac.codevento
     , e.desceven evento
     , To_Date('20/02/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '02/2019' Periodo
     , '201902' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-jan-2019' And '20-feb-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-jan-2019' And '20-feb-2019'
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
     , To_Date('20/03/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '03/2019' Periodo
     , '201903' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-feb-2019' And '20-mar-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-feb-2019' And '20-mar-2019'
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
     , To_Date('20/04/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '04/2019' Periodo
     , '201904' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-mar-2019' And '20-apr-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-mar-2019' And '20-apr-2019'
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
     , To_Date('20/05/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '05/2019' Periodo
     , '201905' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-apr-2019' And '20-may-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-apr-2019' And '20-may-2019'
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
     , To_Date('20/06/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '06/2019' Periodo
     , '201906' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-may-2019' And '20-jun-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-may-2019' And '20-jun-2019'
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
     , To_Date('20/07/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '07/2019' Periodo
     , '201907' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-jun-2019' And '20-jul-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-jun-2019' And '20-jul-2019'
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
     , To_Date('20/08/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '08/2019' Periodo
     , '201908' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-jul-2019' And '20-aug-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-jul-2019' And '20-aug-2019'
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
     , To_Date('20/09/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '09/2019' Periodo
     , '201909' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-aug-2019' And '20-sep-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-aug-2019' And '20-sep-2019'
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
     , To_Date('20/10/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '10/2019' Periodo
     , '201910' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-sep-2019' And '20-oct-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-sep-2019' And '20-oct-2019'
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
     , To_Date('20/11/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '11/2019' Periodo
     , '201911' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-oct-2019' And '20-nov-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-oct-2019' And '20-nov-2019'

Union All
-- Suplementar

select lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , f.CODAREA
     , F.descarea Area
     , f.CODFUNCAO CodFuncao
     , f.descfuncao Funcao
     , f.CODFUNC registro
     , f.NomeFunc funcionario
     , ac.codevento
     , e.desceven evento
     , To_Date('20/02/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '02/2019' Periodo
     , '201902' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-jan-2019' And '20-feb-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-jan-2019' And '20-feb-2019'
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
     , To_Date('20/03/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '03/2019' Periodo
     , '201903' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-feb-2019' And '20-mar-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-feb-2019' And '20-mar-2019'
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
     , To_Date('20/04/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '04/2019' Periodo
     , '201904' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-mar-2019' And '20-apr-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-mar-2019' And '20-apr-2019'
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
     , To_Date('20/05/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '05/2019' Periodo
     , '201905' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-apr-2019' And '20-may-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-apr-2019' And '20-may-2019'
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
     , To_Date('20/06/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '06/2019' Periodo
     , '201906' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-may-2019' And '20-jun-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-may-2019' And '20-jun-2019'
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
     , To_Date('20/07/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '07/2019' Periodo
     , '201907' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-jun-2019' And '20-jul-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-jun-2019' And '20-jul-2019'
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
     , To_Date('20/08/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '08/2019' Periodo
     , '201908' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-jul-2019' And '20-aug-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-jul-2019' And '20-aug-2019'
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
     , To_Date('20/09/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '09/2019' Periodo
     , '201909' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-aug-2019' And '20-sep-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-aug-2019' And '20-sep-2019'
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
     , To_Date('20/10/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '10/2019' Periodo
     , '201910' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-sep-2019' And '20-oct-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-sep-2019' And '20-oct-2019'
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
     , To_Date('20/11/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '11/2019' Periodo
     , '201911' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc(+) = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu(+) = ac2.competacumu
                    and ac2.competacumu between '21-oct-2019' And '20-nov-2019'
                  group by ac2.codintfunc
                   ) ac2
       ) ac2
     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,92,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-oct-2019' And '20-nov-2019'

*/

