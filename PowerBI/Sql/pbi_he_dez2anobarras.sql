create or replace view pbi_he_dez2anobarras as
select lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , f.CODAREA
     , F.descarea Area
     , f.CODFUNCAO CodFuncao
     , f.descfuncao Funcao
     , f.CODFUNC registro
     , f.NomeFunc funcionario
     , ac.codevento
     , e.desceven evento
     , To_Date('15/12/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '12/2019' Periodo
     , '201912' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu = ac2.competacumu
                    and ac2.competacumu between '21-nov-2019' And '15-dec-2019'
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
   And f.codigoempresa || f.codigofl In (11,12,41,61,92,261)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-nov-2019' And '15-dec-2019'
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
     , To_Date('20/12/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '12/2019' Periodo
     , '201912' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu = ac2.competacumu
                    and ac2.competacumu between '21-nov-2019' And '20-dec-2019'
                  group by ac2.codintfunc
                   ) ac2       ) ac2

     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (21,31,131,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-nov-2019' And '20-dec-2019'

-- Suplementar
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
     , To_Date('15/12/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '12/2019' Periodo
     , '201912' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu = ac2.competacumu
                    and ac2.competacumu between '21-nov-2019' And '15-dec-2019'
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
   And f.codigoempresa || f.codigofl In (11,12,41,61,92,261)
   and ac.codevento in (283)
   and ac.competacumu between '21-nov-2019' And '15-dec-2019'
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
     , To_Date('20/12/2019','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '12/2019' Periodo
     , '201912' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu = ac2.competacumu
                    and ac2.competacumu between '21-nov-2019' And '20-dec-2019'
                  group by ac2.codintfunc
                   ) ac2       ) ac2

     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (21,31,131,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-nov-2019' And '20-dec-2019'


/*select lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , f.CODAREA
     , F.descarea Area
     , f.CODFUNCAO CodFuncao
     , f.descfuncao Funcao
     , f.CODFUNC registro
     , f.NomeFunc funcionario
     , ac.codevento
     , e.desceven evento
     , To_Date('15/12/2018','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '12/2018' Periodo
     , '201812' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu = ac2.competacumu
                    and ac2.competacumu between '26-nov-2018' And '20-dec-2018'
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
   And f.codigoempresa || f.codigofl In (92)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '26-nov-2018' And '20-dec-2018'
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
     , To_Date('15/12/2018','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '12/2018' Periodo
     , '201812' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu = ac2.competacumu
                    and ac2.competacumu between '21-nov-2018' And '15-dec-2018'
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
   And f.codigoempresa || f.codigofl In (11,12,41,61,261)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-nov-2018' And '15-dec-2018'
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
     , To_Date('20/12/2018','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '12/2018' Periodo
     , '201812' PeriodoOrdem
  from frq_acumulado ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (1,5)
                    and fr2.codintfunc = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu = ac2.competacumu
                    and ac2.competacumu between '21-nov-2018' And '20-dec-2018'
                  group by ac2.codintfunc
                   ) ac2       ) ac2

     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (21,31,131,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-nov-2018' And '20-dec-2018'

-- Suplementar

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
     , To_Date('15/12/2018','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '12/2018' Periodo
     , '201812' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu = ac2.competacumu
                    and ac2.competacumu between '26-nov-2018' And '20-dec-2018'
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
   And f.codigoempresa || f.codigofl In (92)
   and ac.codevento in (283)
   and ac.competacumu between '26-nov-2018' And '20-dec-2018'
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
     , To_Date('15/12/2018','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '12/2018' Periodo
     , '201812' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu = ac2.competacumu
                    and ac2.competacumu between '21-nov-2018' And '15-dec-2018'
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
   And f.codigoempresa || f.codigofl In (11,12,41,61,261)
   and ac.codevento in (283)
   and ac.competacumu between '21-nov-2018' And '15-dec-2018'
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
     , To_Date('20/12/2018','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '12/2018' Periodo
     , '201812' PeriodoOrdem
  from Frq_Acumuladosuplementar ac
     , ( Select codintfunc, to_char(competacumu,'mm/yyyy') mesAno, competacumu
           From ( Select ac2.codintfunc
                       , max( ac2.competacumu ) competacumu
                    from frq_acumulado ac2
                      , flp_variaveis_frq fr2
                      , PBI_vwFuncionarios f
                  where ac2.codevento in (283)
                    and fr2.codintfunc = ac2.codintfunc
                    And ac2.codintfunc = f.codintfunc
                    and fr2.competacumu = ac2.competacumu
                    and ac2.competacumu between '21-nov-2018' And '20-dec-2018'
                  group by ac2.codintfunc
                   ) ac2       ) ac2

     , PBI_vwFuncionarios f
     , flp_eventos e
 where ac2.codintfunc = ac.codintfunc
   and ac2.competacumu = ac.competacumu
   and f.CODINTFUNC = ac.codintfunc
   and to_char(ac2.competacumu,'mm/yyyy') = mesano
   and e.codevento = ac.codevento
   and f.CODAREA In (20, 30, 40, 80)
   And f.codigoempresa || f.codigofl In (21,31,131,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-nov-2018' And '20-dec-2018'

*/

