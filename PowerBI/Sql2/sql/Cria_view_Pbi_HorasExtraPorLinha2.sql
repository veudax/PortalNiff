create or replace view pbi_horasextrasporlinha as
select  f.CODFUNC registro
      , f.CODINTFUNC
      , f.CODFUNC || '-' || f.NomeFunc funcionario
      , ac2.competacumu
      , dm.codintlinha
      , Trunc(Sum(QtdHrsMinToMin(dm.extradm) + QtdHrsMinToMin(dm.excessodm) + QtdHrsMinToMin(dm.extranotdm))/60,2) minutos
   from
      ( select ac2.codintfunc
              , ac2.codevento
              , to_char(ac2.competacumu,'mm/yyyy') mesano
              , max( ac2.competacumu ) competacumu
              , Max(ac2.dtiniacumu) dtInicioComp
           from frq_acumulado ac2
          where ac2.codevento in (1,5)
            and ac2.competacumu between Add_months(trunc(Sysdate, 'rr'),-12)
            and (ADD_MONTHS(LAST_DAY(Sysdate)+0, 0))
          group by ac2.codintfunc
                 , ac2.codevento
                 , to_char(ac2.competacumu,'mm/yyyy')
          ) ac2
      , Frq_Digitacaomovimento dm
      , vw_funcionarios f
  where ac2.codintfunc = f.codintfunc
    and f.CODINTFUNC = dm.codintfunc
    and f.CODAREA = 40
    and f.codfuncao in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
    And dm.Tipodigit = 'F'
    And Dm.Codocorr In (96, 99, 98)
    And dm.dtdigit Between ac2.dtInicioComp And ac2.competacumu
    And dm.statusdigit = 'N'
    and ac2.competacumu between Add_months(trunc(Sysdate, 'rr'),-12)
    and (ADD_MONTHS(LAST_DAY(Sysdate)+0, 0))
Group By f.CODFUNC
      , f.CODINTFUNC
      , f.CODFUNC || '-' || f.NomeFunc 
      , ac2.competacumu
      , dm.codintlinha
Having Sum(QtdHrsMinToMin(dm.extradm) + QtdHrsMinToMin(dm.excessodm) + QtdHrsMinToMin(dm.extranotdm))/60 > 0

