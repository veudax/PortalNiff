Select Excesso,  Add_months(trunc(dtdigit),-1) antes, trunc(dtdigit) atual,
       dtdigit,
       NOMEFUNC,
       CODFUNC,
       CODFUNCAO,
       CODAREA,
       DESCFUNCAO,
       DESCAREA,
       EmpFil
/* ,      (Case When EmpFil <> '009/001' And 
                  To_Char(dtdigit,'dd') >= 01 And To_Char(dtdigit,'dd') <= 20 Then To_Char(trunc(dtdigit),'mm/yyyy') 
             When  EmpFil <> '009/001' And 
                  To_Char(dtdigit,'dd') >= 21 And To_Char(dtdigit,'dd') <= 31 Then To_Char(Add_months(trunc(dtdigit),1),'mm/yyyy') 
             When EmpFil = '009/001' And 
                  To_Char(dtdigit,'dd') >= 01 And To_Char(dtdigit,'dd') <= 25 Then To_Char(trunc(dtdigit),'mm/yyyy') 
             When EmpFil = '009/001' And 
                  To_Char(dtdigit,'dd') >= 26 And To_Char(dtdigit,'dd') <= 31 Then To_Char(Add_months(trunc(dtdigit),1),'mm/yyyy') End) mesAno      
*/  From (
Select Sum(m.normaldm) - f.JORNADAFUNC-2 excesso,
         m.dtdigit,
         f.NOMEFUNC,
         f.CODFUNC,
         f.CODFUNCAO,
         f.CODAREA,
         f.DESCFUNCAO,
         f.DESCAREA,
         lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil,
         to_char(m.dtdigit, 'yyyy') Ano,
         to_char(m.dtdigit, 'yyyymm') AnoMes,
         to_char(m.dtdigit, 'mm/yyyy') MesAno
    From frq_digitacaomovimento m, vw_funcionarios f
   Where f.CODAREA In (20, 30, 40, 80)
     And m.dtdigit Between Trunc(Sysdate,'rr') and Last_Day(Trunc(Sysdate))
     And f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
     And m.tipodigit = 'F'
     And f.CODINTFUNC = m.codintfunc
     And m.statusdigit = 'N'
Group By 
       m.dtdigit,
         f.NOMEFUNC,
         f.CODFUNC,
         f.CODFUNCAO,
         f.CODAREA,
         f.DESCFUNCAO,
         f.DESCAREA,
         lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0'),
         to_char(m.dtdigit, 'yyyy'),
         to_char(m.dtdigit, 'yyyymm'),
         to_char(m.dtdigit, 'mm/yyyy'), f.JORNADAFUNC
)