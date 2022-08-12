Select        EmpFil,
       CODFUNC,
       NOMEFUNC,
       dtdigit,
       ExcessoDm,
       DESCFUNCAO,
       DESCAREA,
       to_char(DataAux, 'yyyy') Ano,
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
              (Case When EmpFil <> '009/001' And
                         To_Char(dtdigit,'dd') >= 01 And To_Char(dtdigit,'dd') <= 20 Then To_Date('20/' || To_Char(trunc(dtdigit),'mm/yyyy'),'dd/mm/yyyy')
                    When  EmpFil <> '009/001' And
                         To_Char(dtdigit,'dd') >= 21 And To_Char(dtdigit,'dd') <= 31 Then To_Date('21/' || To_Char(Add_months(trunc(dtdigit),1),'mm/yyyy'),'dd/mm/yyyy')
                    When EmpFil = '009/001' And
                         To_Char(dtdigit,'dd') >= 01 And To_Char(dtdigit,'dd') <= 25 Then To_Date('25/' || To_Char(trunc(dtdigit),'mm/yyyy'),'dd/mm/yyyy')
                    When EmpFil = '009/001' And
                         To_Char(dtdigit,'dd') >= 26 And To_Char(dtdigit,'dd') <= 31 Then To_Date('26/' || To_Char(Add_months(trunc(dtdigit),1),'mm/yyyy'),'dd/mm/yyyy') End) DataAux
          From (Select Sum(m.extradigit) + Sum(m.excessodigit) + Sum(m.extranotdigit) + Sum(m.extralinhadigit) - 2 excesso,
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
                    From frq_digitacao m, vw_funcionarios f
                   Where f.CODAREA In (20, 30, 40, 80)
                     And m.dtdigit Between '21-jun-2018' and '20-jul-2018'
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
                Having Sum(m.extradigit) + Sum(m.excessodigit) + Sum(m.extranotdigit) + Sum(m.extralinhadigit) - 2 > 0                         
                 Union All
                Select Sum(m.extradigit) + Sum(m.excessodigit) + Sum(m.extranotdigit) + Sum(m.extralinhadigit) - 4 excesso,
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
                    From frq_digitacao m, vw_funcionarios f
                   Where f.CODAREA In (20, 30, 40, 80)
--                     And m.dtdigit Between Add_Months(Last_Day(Trunc(Sysdate)),-4)+1 and Last_Day(Trunc(Sysdate))
                     And m.dtdigit Between '21-jun-2018' and '20-jul-2018'
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
                Having Sum(m.extradigit) + Sum(m.excessodigit) + Sum(m.extranotdigit) + Sum(m.extralinhadigit) - 4 > 0
                Union All
                Select Sum(m.extradigit) + Sum(m.excessodigit) + Sum(m.extranotdigit) + Sum(m.extralinhadigit) - 2 excesso,
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
                    From frq_digitacao m, vw_funcionarios f
                   Where f.CODAREA In (20, 30, 40, 80)
                     And m.dtdigit Between '26-jun-2018' and '25-jul-2018'
                     And f.codigoempresa || f.codigofl In (92)-- (11,12,31,41,51,61,131,261,263)
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
                Having Sum(m.extradigit) + Sum(m.excessodigit) + Sum(m.extranotdigit) + Sum(m.extralinhadigit) - 2 > 0                   
                  ))

