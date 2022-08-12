create or replace view pbi_horasexcedentes as
Select ExcessoDm,
       horasExtra,
       dtdigit,
       NOMEFUNC,
       CODFUNC,
       CODFUNCAO,
       CODAREA,
       DESCFUNCAO,
       DESCAREA,
       EmpFil,
       Tipo,
       to_char(DataAux, 'yyyy') Ano,
       to_char(DataAux, 'yyyymm') AnoMes,
       to_char(DataAux, 'mm/yyyy') MesAno
  From (Select Excesso As ExcessoDm,
               horasExtra,
               dtdigit,
               NOMEFUNC,
               CODFUNC,
               CODFUNCAO,
               CODAREA,
               DESCFUNCAO,
               DESCAREA,
               EmpFil,
               Tipo,
              (Case When EmpFil <> '009/001' And
                         To_Char(dtdigit,'dd') >= 01 And To_Char(dtdigit,'dd') <= 20 Then To_Date('20/' || To_Char(trunc(dtdigit),'mm/yyyy'),'dd/mm/yyyy')
                    When  EmpFil <> '009/001' And
                         To_Char(dtdigit,'dd') >= 21 And To_Char(dtdigit,'dd') <= 31 Then To_Date('21/' || To_Char(Add_months(trunc(dtdigit),1),'mm/yyyy'),'dd/mm/yyyy')
                    When EmpFil = '009/001' And
                         To_Char(dtdigit,'dd') >= 01 And To_Char(dtdigit,'dd') <= 25 Then To_Date('25/' || To_Char(trunc(dtdigit),'mm/yyyy'),'dd/mm/yyyy')
                    When EmpFil = '009/001' And
                         To_Char(dtdigit,'dd') >= 26 And To_Char(dtdigit,'dd') <= 31 Then To_Date('26/' || To_Char(Add_months(trunc(dtdigit),1),'mm/yyyy'),'dd/mm/yyyy') End) DataAux
          From (Select Case When Sum(m.normaldigit) = 0 Then
                       Case
                         When Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                              Sum(m.Extralinhadigit) -
                              Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                    Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) > 0.59 Then
                          Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) + 1 +
                          (Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                           Sum(m.Extralinhadigit) -
                           Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                 Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) - 0.6)
                         Else
                          Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                          Sum(m.Extralinhadigit)
                       End - Sum(m.jornadadigit) Else
                       Case
                         When Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                              Sum(m.Extralinhadigit) -
                              Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                    Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) > 0.59 Then
                          Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) + 1 +
                          (Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                           Sum(m.Extralinhadigit) -
                           Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                 Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) - 0.6)
                         Else
                          Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                          Sum(m.Extralinhadigit)
                       End End -2 Excesso,
                       '2 horas Excesso ' Tipo,
                       Sum(m.normaldigit) Normal,
                       Sum(m.extradigit) + Sum(m.extranotdigit) + Sum(m.extralinhadigit) horasExtra,
                       Sum(m.excessodigit) ExcessoDm,
                       Sum(m.adnotdigit) AdNoite,
                       m.dtdigit,
                       f.NOMEFUNC,
                       f.CODFUNC,
                       f.CODFUNCAO,
                       f.CODAREA,
                       f.DESCFUNCAO,
                       f.DESCAREA,
                       lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                    From frq_digitacao m, PBI_vwFuncionarios f
                   Where f.CODAREA In (20, 30, 40, 80)
                     And m.dtdigit Between To_Date('21/' || To_char(Add_Months(Last_Day(Trunc(Sysdate)),-4)+1,'mm/yyyy'), 'dd/mm/yyyy') and Last_Day(Trunc(Sysdate))
                     And f.codigoempresa || f.codigofl In (11,12,31,41,51,61,131,261,263)
                     And m.tipodigit = 'F'
                     And f.CODINTFUNC = m.codintfunc
                Group By m.dtdigit,
                         f.NOMEFUNC,
                         f.CODFUNC,
                         f.CODFUNCAO,
                         f.CODAREA,
                         f.DESCFUNCAO,
                         f.DESCAREA,
                         lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0'),
                         f.JORNADAFUNC
--                Having Sum(m.extradigit) + Sum(m.excessodigit) + Sum(m.extranotdigit) + Sum(m.extralinhadigit) - 2 > 0
                Having Case When Sum(m.normaldigit) = 0 Then
                       Case
                         When Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                              Sum(m.Extralinhadigit) -
                              Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                    Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) > 0.59 Then
                          Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) + 1 +
                          (Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                           Sum(m.Extralinhadigit) -
                           Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                 Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) - 0.6)
                         Else
                          Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                          Sum(m.Extralinhadigit)
                       End - Sum(m.jornadadigit) Else
                       Case
                         When Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                              Sum(m.Extralinhadigit) -
                              Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                    Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) > 0.59 Then
                          Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) + 1 +
                          (Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                           Sum(m.Extralinhadigit) -
                           Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                 Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) - 0.6)
                         Else
                          Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                          Sum(m.Extralinhadigit)
                       End End -2 > 0
                 Union All
                Select Case When Sum(m.normaldigit) = 0 Then
                       Case
                         When Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                              Sum(m.Extralinhadigit) -
                              Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                    Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) > 0.59 Then
                          Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) + 1 +
                          (Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                           Sum(m.Extralinhadigit) -
                           Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                 Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) - 0.6)
                         Else
                          Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                          Sum(m.Extralinhadigit)
                       End - Sum(m.jornadadigit) Else
                       Case
                         When Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                              Sum(m.Extralinhadigit) -
                              Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                    Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) > 0.59 Then
                          Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) + 1 +
                          (Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                           Sum(m.Extralinhadigit) -
                           Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                 Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) - 0.6)
                         Else
                          Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                          Sum(m.Extralinhadigit)
                       End End  -4 Excesso,
                       '4 horas Excesso ' Tipo,
                       Sum(m.normaldigit) Normal,
                       Sum(m.extradigit) + Sum(m.extranotdigit) + Sum(m.extralinhadigit) horasExtra,
                       Sum(m.excessodigit) ExcessoDm,
                       Sum(m.adnotdigit) AdNoite,
                         m.dtdigit,
                         f.NOMEFUNC,
                         f.CODFUNC,
                         f.CODFUNCAO,
                         f.CODAREA,
                         f.DESCFUNCAO,
                         f.DESCAREA,
                         lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                    From frq_digitacao m, PBI_vwFuncionarios f
                   Where f.CODAREA In (20, 30, 40, 80)
                     And m.dtdigit Between To_Date('21/' || To_char(Add_Months(Last_Day(Trunc(Sysdate)),-4)+1,'mm/yyyy'), 'dd/mm/yyyy') and Last_Day(Trunc(Sysdate))
                     And f.codigoempresa || f.codigofl In (21) --ABC acordo de até 4 horas extras
                     And m.tipodigit = 'F'
                     And f.CODINTFUNC = m.codintfunc
                Group By
                       m.dtdigit,
                         f.NOMEFUNC,
                         f.CODFUNC,
                         f.CODFUNCAO,
                         f.CODAREA,
                         f.DESCFUNCAO,
                         f.DESCAREA,
                         lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0'),
                         f.JORNADAFUNC
--                Having Sum(m.extradigit) + Sum(m.excessodigit) + Sum(m.extranotdigit) + Sum(m.extralinhadigit) - 4 > 0
                Having Case When Sum(m.normaldigit) = 0 Then
                       Case
                         When Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                              Sum(m.Extralinhadigit) -
                              Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                    Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) > 0.59 Then
                          Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) + 1 +
                          (Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                           Sum(m.Extralinhadigit) -
                           Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                 Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) - 0.6)
                         Else
                          Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                          Sum(m.Extralinhadigit)
                       End - Sum(m.jornadadigit) Else
                       Case
                         When Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                              Sum(m.Extralinhadigit) -
                              Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                    Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) > 0.59 Then
                          Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) + 1 +
                          (Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                           Sum(m.Extralinhadigit) -
                           Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                 Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) - 0.6)
                         Else
                          Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                          Sum(m.Extralinhadigit)
                       End End -4 > 0
                Union All
               Select Case When Sum(m.normaldigit) = 0 Then
                       Case
                         When Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                              Sum(m.Extralinhadigit) -
                              Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                    Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) > 0.59 Then
                          Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) + 1 +
                          (Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                           Sum(m.Extralinhadigit) -
                           Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                 Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) - 0.6)
                         Else
                          Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                          Sum(m.Extralinhadigit)
                       End - Sum(m.jornadadigit) Else
                       Case
                         When Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                              Sum(m.Extralinhadigit) -
                              Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                    Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) > 0.59 Then
                          Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) + 1 +
                          (Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                           Sum(m.Extralinhadigit) -
                           Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                 Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) - 0.6)
                         Else
                          Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                          Sum(m.Extralinhadigit)
                       End End  -2 Excesso,
                       '2 horas Excesso ' Tipo,
                       Sum(m.normaldigit) Normal,
                       Sum(m.extradigit) + Sum(m.extranotdigit) + Sum(m.extralinhadigit) horasExtra,
                       Sum(m.excessodigit) ExcessoDm,
                       Sum(m.adnotdigit) AdNoite,
                       m.dtdigit,
                       f.NOMEFUNC,
                       f.CODFUNC,
                       f.CODFUNCAO,
                       f.CODAREA,
                       f.DESCFUNCAO,
                       f.DESCAREA,
                       lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
                    From frq_digitacao m, PBI_vwFuncionarios f
                   Where f.CODAREA In (20, 30, 40, 80)
                     And m.dtdigit Between To_Date('26/' || To_char(Add_Months(Last_Day(Trunc(Sysdate)),-4)+1,'mm/yyyy'), 'dd/mm/yyyy') and Last_Day(Trunc(Sysdate))
                     And f.codigoempresa || f.codigofl In (92)
                     And m.tipodigit = 'F'
                     And f.CODINTFUNC = m.codintfunc
                Group By m.dtdigit,
                         f.NOMEFUNC,
                         f.CODFUNC,
                         f.CODFUNCAO,
                         f.CODAREA,
                         f.DESCFUNCAO,
                         f.DESCAREA,
                         lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0'),
                         f.JORNADAFUNC
--                Having Sum(m.extradigit) + Sum(m.excessodigit) + Sum(m.extranotdigit) + Sum(m.extralinhadigit) - 2 > 0
                Having Case When Sum(m.normaldigit) = 0 Then
                       Case
                         When Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                              Sum(m.Extralinhadigit) -
                              Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                    Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) > 0.59 Then
                          Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) + 1 +
                          (Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                           Sum(m.Extralinhadigit) -
                           Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                 Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) - 0.6)
                         Else
                          Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                          Sum(m.Extralinhadigit)
                       End - Sum(m.jornadadigit) Else
                       Case
                         When Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                              Sum(m.Extralinhadigit) -
                              Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                    Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) > 0.59 Then
                          Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) + 1 +
                          (Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                           Sum(m.Extralinhadigit) -
                           Trunc(Sum(m.Extradigit) + Sum(m.Excessodigit) +
                                 Sum(m.Extranotdigit) + Sum(m.Extralinhadigit)) - 0.6)
                         Else
                          Sum(m.Extradigit) + Sum(m.Excessodigit) + Sum(m.Extranotdigit) +
                          Sum(m.Extralinhadigit)
                       End End -2 > 0

                  ))

