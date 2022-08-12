create or replace view pbi_he_operacao_outros as
(select '001/001 - EOVG DUTRA' Empresa
         ,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2, flp_variaveis_frq fr2
            where ac2.codevento in (1,5)
            and   fr2.codintfunc = ac2.codintfunc
            and   fr2.competacumu = ac2.competacumu
            and ac2.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 1
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
union all
select '001/001 - EOVG DUTRA' Empresa
         ,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2
            where ac2.codevento in (1,5)
            and ac2.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 1
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1)))
--------------------------------------------------------------------
union all
--------------------------------------------------------------------
(select '001/002 - EOVG LAVRAS' Empresa
         ,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2, flp_variaveis_frq fr2
            where ac2.codevento in (1,5)
            and   fr2.codintfunc = ac2.codintfunc
            and   fr2.competacumu = ac2.competacumu
            and ac2.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 1
and f.CODIGOFL = 2
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
union all
select '001/002 - EOVG LAVRAS' Empresa
         ,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2
            where ac2.codevento in (1,5)
            and ac2.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 1
and f.CODIGOFL = 2
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1)))
-------------------------------------------------------------
UNION ALL
(select '002/001 - ABC TRANSPORTES' Empresa
         ,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2, flp_variaveis_frq fr2
            where ac2.codevento in (1,5)
            and   fr2.codintfunc = ac2.codintfunc
            and   fr2.competacumu = ac2.competacumu
            and ac2.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 2
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
union all
select '002/001 - ABC TRANSPORTES' Empresa
         ,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2
            where ac2.codevento in (1,5)
            and ac2.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 2
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1)))
--------------------------------------------
UNION ALL
(select '003/001 RAPIDO D OESTE'
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2, flp_variaveis_frq fr2
            where ac2.codevento in (1,5)
            and   fr2.codintfunc = ac2.codintfunc
            and   fr2.competacumu = ac2.competacumu
            and ac2.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 3
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
union all
select '003/001 RAPIDO D OESTE'
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2
            where ac2.codevento in (1,5)
            and ac2.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 3
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1)))
--------------------------------------------------------------
UNION ALL
((select '004/001 - CISNE BRANCO' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2, flp_variaveis_frq fr2
            where ac2.codevento in (1,5)
            and   fr2.codintfunc = ac2.codintfunc
            and   fr2.competacumu = ac2.competacumu
            and ac2.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 4
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
union all
select '004/001 - CISNE BRANCO' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2
            where ac2.codevento in (1,5)
            and ac2.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 4
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1)))
union all
(select '004/001 - CISNE BRANCO' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumuladosuplementar ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2, flp_variaveis_frq fr2
            where ac2.codevento in (1,5)
            and   fr2.codintfunc = ac2.codintfunc
            and   fr2.competacumu = ac2.competacumu
            and ac2.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 4
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
union all
select '004/001 - CISNE BRANCO' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumuladosuplementar ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2
            where ac2.codevento in (1,5)
            and ac2.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 4
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1))))
------------------------------------------------------
UNION ALL
(select '006/001 - VIAÇÃO ARUJA' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2, flp_variaveis_frq fr2
            where ac2.codevento in (1,5)
            and   fr2.codintfunc = ac2.codintfunc
            and   fr2.competacumu = ac2.competacumu
            and ac2.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 6
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
union all
select '006/001 - VIAÇÃO ARUJA' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2
            where ac2.codevento in (1,5)
            and ac2.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 6
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1)))
------------------------------------------------------------------------
UNION ALL
(select '009/001 - CAMPIBUS LTDA' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2, flp_variaveis_frq fr2
            where ac2.codevento in (1,5)
            and   fr2.codintfunc = ac2.codintfunc
            and   fr2.competacumu = ac2.competacumu
            and ac2.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 9
and f.CODIGOFL = 2
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
union all
select '009/001 - CAMPIBUS LTDA' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2
            where ac2.codevento in (1,5)
            and ac2.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+25, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 9
and f.CODIGOFL = 2
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+25, -1)))
------------------------------------------------------------------------------
UNION ALL
(select '013/001 - RIBE TRANSPORTES' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2, flp_variaveis_frq fr2
            where ac2.codevento in (1,5)
            and   fr2.codintfunc = ac2.codintfunc
            and   fr2.competacumu = ac2.competacumu
            and ac2.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 13
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
union all
select '013/001 - RIBE TRANSPORTES' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2
            where ac2.codevento in (1,5)
            and ac2.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 13
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1)))
------------------------------------------------------------------
UNION ALL
(select '026/001 - VUG DUTRA' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2, flp_variaveis_frq fr2
            where ac2.codevento in (1,5)
            and   fr2.codintfunc = ac2.codintfunc
            and   fr2.competacumu = ac2.competacumu
            and ac2.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 26
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
union all
select '026/001 - VUG DUTRA' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2
            where ac2.codevento in (1,5)
            and ac2.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 26
and f.CODIGOFL = 1
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1)))
------------------------------------------------------------------
UNION ALL
(select '026/003 - VUG BEBEDOURO' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2, flp_variaveis_frq fr2
            where ac2.codevento in (1,5)
            and   fr2.codintfunc = ac2.codintfunc
            and   fr2.competacumu = ac2.competacumu
            and ac2.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 26
and f.CODIGOFL = 3
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between '01-jan-2016' and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
union all
select '026/003 - VUG BEBEDOURO' Empresa
,f.CODAREA
         ,a.descarea Area
         ,f.CODFUNCAO CodFuncao
         ,fu.descfuncao Funcao
         ,f.CODFUNC registro
         ,f.NomeFunc funcionario
         ,ac.codevento
         ,e.desceven evento
         ,ac.competacumu competacumu
         ,ac.referacumu
         ,trunc((( (trunc(ac.referacumu) * 60 ) + ( (ac.referacumu - trunc(ac.referacumu) )*100) ))/60,2) minutos
from frq_acumulado ac
         ,( select ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy') mesano
                     ,max( ac2.competacumu ) competacumu
            from frq_acumulado ac2
            where ac2.codevento in (1,5)
            and ac2.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1))
            group by ac2.codintfunc
                     ,to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
         ,vw_funcionarios f
         ,flp_area a
         ,flp_depto d
         ,flp_eventos e
         ,flp_funcao fu
         ,flp_setor s
where ac2.codintfunc = ac.codintfunc
and ac2.competacumu = ac.competacumu
and f.CODINTFUNC = ac.codintfunc
and to_char(ac2.competacumu,'mm/yyyy') = mesano
and s.codsetor = f.CODSETOR
and fu.codfuncao = f.CODFUNCAO
and e.codevento = ac.codevento
and d.coddepto = f.CODDEPTO
and a.codarea = f.CODAREA
and f.CODAREA = 40
and f.CODIGOEMPRESA = 26
and f.CODIGOFL = 3
and fu.codfuncao not in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and ac.codevento in (3,375,3,202,281,222,201,202,277,598,443,202,652,637,283)
and ac.competacumu between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+20, -1)))

