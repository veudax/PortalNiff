Create Or Replace View pbi_he_DemaisMeses1AnoBarras As
-- 2017 Campibus
select lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , f.CODAREA
     , F.descarea Area
     , f.CODFUNCAO CodFuncao
     , f.descfuncao Funcao
     , f.CODFUNC registro
     , f.NomeFunc funcionario
     , ac.codevento
     , e.desceven evento
     , To_Date('25/02/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '02/2017' Periodo
     , '201702' PeriodoOrdem
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
                    and ac2.competacumu between '26-jan-2017' And '25-feb-2017'
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
   and ac.competacumu between '01-jan-2017' And '25-feb-2017'
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
     , To_Date('25/03/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '03/2017' Periodo
     , '201703' PeriodoOrdem
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
                    and ac2.competacumu between '26-feb-2017' And '25-mar-2017'
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
   and ac.competacumu between '01-feb-2017' And '25-mar-2017' 
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
     , To_Date('25/04/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '04/2017' Periodo
     , '201704' PeriodoOrdem
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
                    and ac2.competacumu between '26-mar-2017' And '25-apr-2017'
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
   and ac.competacumu between '01-mar-2017' And '25-apr-2017'   
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
     , To_Date('25/05/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '05/2017' Periodo
     , '201705' PeriodoOrdem
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
                    and ac2.competacumu between '26-apr-2017' And '25-may-2017'
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
   and ac.competacumu between '01-apr-2017' And '25-may-2017'      
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
     , To_Date('25/06/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '06/2017' Periodo
     , '201706' PeriodoOrdem
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
                    and ac2.competacumu between '26-may-2017' And '25-jun-2017'
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
   and ac.competacumu between '01-may-2017' And '25-jun-2017'      
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
     , To_Date('25/07/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '07/2017' Periodo
     , '201707' PeriodoOrdem
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
                    and ac2.competacumu between '26-jun-2017' And '25-jul-2017'
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
   and ac.competacumu between '01-jun-2017' And '25-jul-2017'      
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
     , To_Date('25/08/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '08/2017' Periodo
     , '201708' PeriodoOrdem
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
                    and ac2.competacumu between '26-jul-2017' And '25-aug-2017'
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
   and ac.competacumu between '01-jul-2017' And '25-aug-2017'      
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
     , To_Date('25/09/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '09/2017' Periodo
     , '201709' PeriodoOrdem
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
                    and ac2.competacumu between '26-aug-2017' And '25-sep-2017'
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
   and ac.competacumu between '01-aug-2017' And '25-sep-2017'      
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
     , To_Date('25/10/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '10/2017' Periodo
     , '201710' PeriodoOrdem
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
                    and ac2.competacumu between '26-sep-2017' And '25-oct-2017'
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
   and ac.competacumu between '01-sep-2017' And '25-oct-2017'   
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
     , To_Date('25/11/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '11/2017' Periodo
     , '201711' PeriodoOrdem
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
                    and ac2.competacumu between '26-oct-2017' And '25-nov-2017'
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
   and ac.competacumu between '01-oct-2017' And '25-nov-2017'      

-- 2017 -- demais empresas 
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
     , To_Date('20/02/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '02/2017' Periodo
     , '201702' PeriodoOrdem
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
                    and ac2.competacumu between '21-jan-2017' And '20-feb-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-jan-2017' And '20-feb-2017'
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
     , To_Date('20/03/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '03/2017' Periodo
     , '201703' PeriodoOrdem
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
                    and ac2.competacumu between '21-feb-2017' And '20-mar-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-feb-2017' And '20-mar-2017'   
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
     , To_Date('20/04/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '04/2017' Periodo
     , '201704' PeriodoOrdem
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
                    and ac2.competacumu between '21-mar-2017' And '20-apr-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-mar-2017' And '20-apr-2017'   
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
     , To_Date('20/05/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '05/2017' Periodo
     , '201705' PeriodoOrdem
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
                    and ac2.competacumu between '21-apr-2017' And '20-may-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-apr-2017' And '20-may-2017'   
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
     , To_Date('20/06/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '06/2017' Periodo
     , '201706' PeriodoOrdem
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
                    and ac2.competacumu between '21-may-2017' And '20-jun-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-may-2017' And '20-jun-2017'   
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
     , To_Date('20/07/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '07/2017' Periodo
     , '201707' PeriodoOrdem
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
                    and ac2.competacumu between '21-jun-2017' And '20-jul-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-jun-2017' And '20-jul-2017'   
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
     , To_Date('20/08/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '08/2017' Periodo
     , '201708' PeriodoOrdem
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
                    and ac2.competacumu between '21-jul-2017' And '20-aug-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-jul-2017' And '20-aug-2017'   
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
     , To_Date('20/09/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '09/2017' Periodo
     , '201709' PeriodoOrdem
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
                    and ac2.competacumu between '21-aug-2017' And '20-sep-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-aug-2017' And '20-sep-2017'   
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
     , To_Date('20/10/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '10/2017' Periodo
     , '201710' PeriodoOrdem
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
                    and ac2.competacumu between '21-sep-2017' And '20-oct-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-sep-2017' And '20-oct-2017'   
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
     , To_Date('20/11/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '11/2017' Periodo
     , '201711' PeriodoOrdem
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
                    and ac2.competacumu between '21-oct-2017' And '20-nov-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
   and ac.competacumu between '21-oct-2017' And '20-nov-2017'   
   
Union All   
-- Suplementar
-- 2017 Campibus
select lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , f.CODAREA
     , F.descarea Area
     , f.CODFUNCAO CodFuncao
     , f.descfuncao Funcao
     , f.CODFUNC registro
     , f.NomeFunc funcionario
     , ac.codevento
     , e.desceven evento
     , To_Date('25/02/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '02/2017' Periodo
     , '201702' PeriodoOrdem
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
                    and ac2.competacumu between '26-jan-2017' And '25-feb-2017'
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
   and ac.competacumu between '01-jan-2017' And '25-feb-2017'
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
     , To_Date('25/03/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '03/2017' Periodo
     , '201703' PeriodoOrdem
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
                    and ac2.competacumu between '26-feb-2017' And '25-mar-2017'
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
   and ac.competacumu between '01-feb-2017' And '25-mar-2017' 
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
     , To_Date('25/04/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '04/2017' Periodo
     , '201704' PeriodoOrdem
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
                    and ac2.competacumu between '26-mar-2017' And '25-apr-2017'
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
   and ac.competacumu between '01-mar-2017' And '25-apr-2017'   
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
     , To_Date('25/05/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '05/2017' Periodo
     , '201705' PeriodoOrdem
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
                    and ac2.competacumu between '26-apr-2017' And '25-may-2017'
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
   and ac.competacumu between '01-apr-2017' And '25-may-2017'      
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
     , To_Date('25/06/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '06/2017' Periodo
     , '201706' PeriodoOrdem
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
                    and ac2.competacumu between '26-may-2017' And '25-jun-2017'
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
   and ac.competacumu between '01-may-2017' And '25-jun-2017'      
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
     , To_Date('25/07/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '07/2017' Periodo
     , '201707' PeriodoOrdem
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
                    and ac2.competacumu between '26-jun-2017' And '25-jul-2017'
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
   and ac.competacumu between '01-jun-2017' And '25-jul-2017'      
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
     , To_Date('25/08/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '08/2017' Periodo
     , '201708' PeriodoOrdem
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
                    and ac2.competacumu between '26-jul-2017' And '25-aug-2017'
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
   and ac.competacumu between '01-jul-2017' And '25-aug-2017'      
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
     , To_Date('25/09/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '09/2017' Periodo
     , '201709' PeriodoOrdem
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
                    and ac2.competacumu between '26-aug-2017' And '25-sep-2017'
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
   and ac.competacumu between '01-aug-2017' And '25-sep-2017'      
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
     , To_Date('25/10/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '10/2017' Periodo
     , '201710' PeriodoOrdem
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
                    and ac2.competacumu between '26-sep-2017' And '25-oct-2017'
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
   and ac.competacumu between '01-sep-2017' And '25-oct-2017'   
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
     , To_Date('25/11/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '11/2017' Periodo
     , '201711' PeriodoOrdem
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
                    and ac2.competacumu between '26-oct-2017' And '25-nov-2017'
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
   and ac.competacumu between '01-oct-2017' And '25-nov-2017'      

-- 2017 -- demais empresas 
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
     , To_Date('20/02/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '02/2017' Periodo
     , '201702' PeriodoOrdem
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
                    and ac2.competacumu between '21-jan-2017' And '20-feb-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-jan-2017' And '20-feb-2017'
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
     , To_Date('20/03/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '03/2017' Periodo
     , '201703' PeriodoOrdem
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
                    and ac2.competacumu between '21-feb-2017' And '20-mar-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-feb-2017' And '20-mar-2017'   
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
     , To_Date('20/04/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '04/2017' Periodo
     , '201704' PeriodoOrdem
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
                    and ac2.competacumu between '21-mar-2017' And '20-apr-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-mar-2017' And '20-apr-2017'   
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
     , To_Date('20/05/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '05/2017' Periodo
     , '201705' PeriodoOrdem
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
                    and ac2.competacumu between '21-apr-2017' And '20-may-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-apr-2017' And '20-may-2017'   
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
     , To_Date('20/06/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '06/2017' Periodo
     , '201706' PeriodoOrdem
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
                    and ac2.competacumu between '21-may-2017' And '20-jun-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-may-2017' And '20-jun-2017'   
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
     , To_Date('20/07/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '07/2017' Periodo
     , '201707' PeriodoOrdem
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
                    and ac2.competacumu between '21-jun-2017' And '20-jul-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-jun-2017' And '20-jul-2017'   
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
     , To_Date('20/08/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '08/2017' Periodo
     , '201708' PeriodoOrdem
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
                    and ac2.competacumu between '21-jul-2017' And '20-aug-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-jul-2017' And '20-aug-2017'   
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
     , To_Date('20/09/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '09/2017' Periodo
     , '201709' PeriodoOrdem
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
                    and ac2.competacumu between '21-aug-2017' And '20-sep-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-aug-2017' And '20-sep-2017'   
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
     , To_Date('20/10/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '10/2017' Periodo
     , '201710' PeriodoOrdem
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
                    and ac2.competacumu between '21-sep-2017' And '20-oct-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-sep-2017' And '20-oct-2017'   
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
     , To_Date('20/11/2017','dd/mm/yyyy') competacumu
     , ac.referacumu
     , trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
     , '11/2017' Periodo
     , '201711' PeriodoOrdem
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
                    and ac2.competacumu between '21-oct-2017' And '20-nov-2017'
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
   And f.codigoempresa || f.codigofl In (11,12,21,31,41,61,131,261,263)
   and ac.codevento in (283)
   and ac.competacumu between '21-oct-2017' And '20-nov-2017'   
      