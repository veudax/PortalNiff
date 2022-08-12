select  f.CODFUNC registro
      , f.CODINTFUNC
      , f.NomeFunc funcionario
      , ac2.competacumu Data
      , dm.usuexcldigit, dm.statusdigit
--      , dm.codintlinha
--      , Round(Sum(QtdHrsMinToMin(dm.extradm) + QtdHrsMinToMin(dm.excessodm) + QtdHrsMinToMin(dm.extranotdm))/60,2) minutos
, dm.dtdigit, dm.dtexcldigit
, dm.extradm, dm.extranotdm, dm.excessodm
, Trunc((QtdHrsMinToMin(dm.extradm) + QtdHrsMinToMin(dm.excessodm) + QtdHrsMinToMin(dm.extranotdm))/60,2) minutos
   from 
      ( select ac2.codintfunc
              , ac2.codevento
              , to_char(ac2.competacumu,'mm/yyyy') mesano
              , max( ac2.competacumu ) competacumu
              , Max(ac2.dtiniacumu) dtInicioComp              
           from frq_acumulado ac2, 
                flp_variaveis_frq fr2
          where ac2.codevento in (1,5)
            and fr2.codintfunc = ac2.codintfunc
            and fr2.competacumu = ac2.competacumu
            and ac2.competacumu between Add_months(trunc(Sysdate, 'rr'),-12)
            and (ADD_MONTHS(LAST_DAY(Sysdate)+0, -1))
          group by ac2.codintfunc
                 , ac2.codevento
                 , to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
      , Frq_Digitacaomovimento dm   
      , Bgm_Cadlinhas l
      , vw_funcionarios f
      , flp_area a
      , flp_depto d
      , flp_funcao fu
      , flp_setor s
  where ac2.codintfunc = f.codintfunc
    and f.CODINTFUNC = dm.codintfunc
    and s.codsetor = f.CODSETOR
    and fu.codfuncao = f.CODFUNCAO
    and d.coddepto = f.CODDEPTO
    and a.codarea = f.CODAREA
    and f.CODAREA = 40
    and f.CODIGOEMPRESA = 1
    and f.CODIGOFL = 1
    and fu.codfuncao in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
    And dm.Tipodigit = 'F'
--    And dm.statusdigit = 'N'
    And Dm.Codocorr In (96, 99, 98)
    And dm.dtdigit Between ac2.dtInicioComp And ac2.competacumu
--    And dm.usuexcldigit Is Null
    and ac2.competacumu between Add_months(trunc(Sysdate, 'rr'),-12)
    and (ADD_MONTHS(LAST_DAY(Sysdate)+0, -1))
    And l.codintlinha(+) = dm.codintlinha
    And f.CODFUNC = '004953'
    And ac2.competacumu = '20-may-2017'
/*Group By f.CODFUNC 
      , f.CODINTFUNC
      , f.NomeFunc 
      , ac2.competacumu
--      , dm.codintlinha
Having Sum(QtdHrsMinToMin(dm.extradm) + QtdHrsMinToMin(dm.excessodm))/60 > 0*/
Order By registro, Data