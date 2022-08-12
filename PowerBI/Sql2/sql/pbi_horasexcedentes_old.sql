create or replace view pbi_horasexcedentes as
Select m.dtdigit,
         extraDm + ExtraNotDm horasExtra,
         ExcessoDm,
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
   Where m.dtdigit Between Trunc(Sysdate,'rr') and Last_Day(Trunc(Sysdate))
     and f.CODAREA In (20, 30, 40, 80)
     And f.codigoempresa || f.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
     And excessoDm > 2
     And f.CODINTFUNC = m.codintfunc
     And m.statusdigit = 'N'

