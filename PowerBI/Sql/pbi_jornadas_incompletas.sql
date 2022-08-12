create or replace view pbi_jornadas_incompletas as
(Select '001/001 - EOVG DUTRA' empresa,
        '001/001' EmpFil,
nomefunc, dtdigit, jornadafunc, normaldigit,
        m_jornada, m_real, (m_jornada - m_real) dif_min_tot
from
(
select f.nomefunc, f.codfunc,
       To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy') dtdigit,
       max(f.jornadafunc) jornadafunc,
       Sum(trunc((( (trunc(f.jornadafunc) * 60 ) + ( ((f.jornadafunc) - trunc(f.jornadafunc) )*100) )),2)) m_jornada,
       Max(m.normaldm) normaldigit,
       Sum(trunc((( (trunc(m.normaldm) * 60 ) + ( ((m.normaldm) - trunc(m.normaldm) )*100) )),2)) m_real
from PBI_vwFuncionarios f, frq_digitacaomovimento m
where f.codintfunc = m.codintfunc
and   m.iddigit = 1
And m.statusdigit = 'N'
and   m.tipodigit = 'F'
and   m.codocorr in (96,99,98)
and   f.CODFUNCAO in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and   m.iddigit = 1
and   m.dtdigit between trunc(add_months(last_day(Sysdate)+1,-12)) and trunc(sysdate)
and   f.codigoempresa = 1 and f.codigofl = 1
and   m.normaldm < f.jornadafunc and m.normaldm > 0
Group By f.nomefunc, f.codfunc,
        To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy')
order by f.codfunc, f.nomefunc))

union all

(Select '001/002 - EOVG LAVRAS' empresa,
        '001/002' EmpFil, nomefunc, dtdigit, jornadafunc, normaldigit,
        m_jornada, m_real, (m_jornada - m_real) dif_min_tot
from
(select f.nomefunc, f.codfunc,
       To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy') dtdigit,
       max(f.jornadafunc) jornadafunc,
       Sum(trunc((( (trunc(f.jornadafunc) * 60 ) + ( ((f.jornadafunc) - trunc(f.jornadafunc) )*100) )),2)) m_jornada,
       Max(m.normaldm) normaldigit,
       Sum(trunc((( (trunc(m.normaldm) * 60 ) + ( ((m.normaldm) - trunc(m.normaldm) )*100) )),2)) m_real
from PBI_vwFuncionarios f, frq_digitacaomovimento m
where f.codintfunc = m.codintfunc
and   m.iddigit = 1
And m.statusdigit = 'N'
and   m.tipodigit = 'F'
and   m.codocorr in (96,99,98)
and   f.CODFUNCAO in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and   m.iddigit = 1
and   m.dtdigit between trunc(add_months(last_day(Sysdate)+1,-12)) and trunc(sysdate)
and   f.codigoempresa = 1 and f.codigofl = 2
and   m.normaldm < f.jornadafunc and m.normaldm > 0
Group By f.nomefunc, f.codfunc,
        To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy')
order by f.codfunc, f.nomefunc))

union all

(Select '002/001 - ABC TRANSPORTES' empresa,
        '002/001' EmpFil, nomefunc, dtdigit, jornadafunc, normaldigit,
        m_jornada, m_real, (m_jornada - m_real) dif_min_tot
from
(select f.nomefunc, f.codfunc,
       To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy') dtdigit,
       max(f.jornadafunc) jornadafunc,
       Sum(trunc((( (trunc(f.jornadafunc) * 60 ) + ( ((f.jornadafunc) - trunc(f.jornadafunc) )*100) )),2)) m_jornada,
       Max(m.normaldm) normaldigit,
       Sum(trunc((( (trunc(m.normaldm) * 60 ) + ( ((m.normaldm) - trunc(m.normaldm) )*100) )),2)) m_real
from PBI_vwFuncionarios f, frq_digitacaomovimento m
where f.codintfunc = m.codintfunc
and   m.iddigit = 1
And m.statusdigit = 'N'
and   m.tipodigit = 'F'
and   m.codocorr in (96,99,98)
and   f.CODFUNCAO in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and   m.iddigit = 1
and   m.dtdigit between trunc(add_months(last_day(Sysdate)+1,-12)) and trunc(sysdate)
and   f.codigoempresa = 2 and f.codigofl = 1
and   m.normaldm < f.jornadafunc and m.normaldm > 0
Group By f.nomefunc, f.codfunc,
        To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy')
order by f.codfunc, f.nomefunc))

union all

(Select '003/001 - RAPIDO D OESTE' empresa,
        '003/001' EmpFil, nomefunc, dtdigit, jornadafunc, normaldigit,
        m_jornada, m_real, (m_jornada - m_real) dif_min_tot
from
(select f.nomefunc, f.codfunc,
       To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy') dtdigit,
       max(f.jornadafunc) jornadafunc,
       Sum(trunc((( (trunc(f.jornadafunc) * 60 ) + ( ((f.jornadafunc) - trunc(f.jornadafunc) )*100) )),2)) m_jornada,
       Max(m.normaldm) normaldigit,
       Sum(trunc((( (trunc(m.normaldm) * 60 ) + ( ((m.normaldm) - trunc(m.normaldm) )*100) )),2)) m_real
from PBI_vwFuncionarios f, frq_digitacaomovimento m
where f.codintfunc = m.codintfunc
and   m.iddigit = 1
And m.statusdigit = 'N'
and   m.tipodigit = 'F'
and   m.codocorr in (96,99,98)
and   f.CODFUNCAO in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and   m.iddigit = 1
and   m.dtdigit between trunc(add_months(last_day(Sysdate)+1,-12)) and trunc(sysdate)
and   f.codigoempresa = 3 and f.codigofl = 1
and   m.normaldm < f.jornadafunc and m.normaldm > 0
Group By f.nomefunc, f.codfunc,
        To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy')
order by f.codfunc, f.nomefunc))

union all

(Select '004/001 - CISNE BRANCO' empresa,
        '004/001' EmpFil, nomefunc, dtdigit, jornadafunc, normaldigit,
        m_jornada, m_real, (m_jornada - m_real) dif_min_tot
from
(select f.nomefunc, f.codfunc,
       To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy') dtdigit,
       max(f.jornadafunc) jornadafunc,
       Sum(trunc((( (trunc(f.jornadafunc) * 60 ) + ( ((f.jornadafunc) - trunc(f.jornadafunc) )*100) )),2)) m_jornada,
       Max(m.normaldm) normaldigit,
       Sum(trunc((( (trunc(m.normaldm) * 60 ) + ( ((m.normaldm) - trunc(m.normaldm) )*100) )),2)) m_real
from PBI_vwFuncionarios f, frq_digitacaomovimento m
where f.codintfunc = m.codintfunc
and   m.iddigit = 1
And m.statusdigit = 'N'
and   m.tipodigit = 'F'
and   m.codocorr in (96,99,98)
and   f.CODFUNCAO in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and   m.iddigit = 1
and   m.dtdigit between trunc(add_months(last_day(Sysdate)+1,-12)) and trunc(sysdate)
and   f.codigoempresa = 4 and f.codigofl = 1
and   m.normaldm < f.jornadafunc and m.normaldm > 0
Group By f.nomefunc, f.codfunc,
        To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy')
order by f.codfunc, f.nomefunc))

union all

(Select '006/001 - VIAÇÃO ARUJÁ' empresa,
        '006/001' EmpFil, nomefunc, dtdigit, jornadafunc, normaldigit,
        m_jornada, m_real, (m_jornada - m_real) dif_min_tot
from
(select f.nomefunc, f.codfunc,
       To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy') dtdigit,
       max(f.jornadafunc) jornadafunc,
       Sum(trunc((( (trunc(f.jornadafunc) * 60 ) + ( ((f.jornadafunc) - trunc(f.jornadafunc) )*100) )),2)) m_jornada,
       Max(m.normaldm) normaldigit,
       Sum(trunc((( (trunc(m.normaldm) * 60 ) + ( ((m.normaldm) - trunc(m.normaldm) )*100) )),2)) m_real
from PBI_vwFuncionarios f, frq_digitacaomovimento m
where f.codintfunc = m.codintfunc
and   m.iddigit = 1
And m.statusdigit = 'N'
and   m.tipodigit = 'F'
and   m.codocorr in (96,99,98)
and   f.CODFUNCAO in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and   m.iddigit = 1
and   m.dtdigit between trunc(add_months(last_day(Sysdate)+1,-12)) and trunc(sysdate)
and   f.codigoempresa = 6 and f.codigofl = 1
and   m.normaldm < f.jornadafunc and m.normaldm > 0
Group By f.nomefunc, f.codfunc,
        To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy')
order by f.codfunc, f.nomefunc))

UNION ALL

(Select '009/001 - CAMPIBUS LTDA' empresa,
        '009/001' EmpFil,  nomefunc, dtdigit, jornadafunc, normaldigit,
        m_jornada, m_real, (m_jornada - m_real) dif_min_tot
from
(select f.nomefunc, f.codfunc,
       To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy') dtdigit,
       max(f.jornadafunc) jornadafunc,
       Sum(trunc((( (trunc(f.jornadafunc) * 60 ) + ( ((f.jornadafunc) - trunc(f.jornadafunc) )*100) )),2)) m_jornada,
       Max(m.normaldm) normaldigit,
       Sum(trunc((( (trunc(m.normaldm) * 60 ) + ( ((m.normaldm) - trunc(m.normaldm) )*100) )),2)) m_real
from PBI_vwFuncionarios f, frq_digitacaomovimento m
where f.codintfunc = m.codintfunc
and   m.iddigit = 1
And m.statusdigit = 'N'
and   m.tipodigit = 'F'
and   m.codocorr in (96,99,98)
and   f.CODFUNCAO in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and   m.iddigit = 1
and   m.dtdigit between trunc(add_months(last_day(Sysdate)+1,-12)) and trunc(sysdate)
and   f.codigoempresa = 9 and f.codigofl = 2
and   m.normaldm < f.jornadafunc and m.normaldm > 0
Group By f.nomefunc, f.codfunc,
        To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy')
order by f.codfunc, f.nomefunc))

union all

(Select '013/001 - RIBE TRANSPORTES' empresa,
        '013/001' EmpFil, nomefunc, dtdigit, jornadafunc, normaldigit,
        m_jornada, m_real, (m_jornada - m_real) dif_min_tot
from
(select f.nomefunc, f.codfunc,
       To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy') dtdigit,
       max(f.jornadafunc) jornadafunc,
       Sum(trunc((( (trunc(f.jornadafunc) * 60 ) + ( ((f.jornadafunc) - trunc(f.jornadafunc) )*100) )),2)) m_jornada,
       Max(m.normaldm) normaldigit,
       Sum(trunc((( (trunc(m.normaldm) * 60 ) + ( ((m.normaldm) - trunc(m.normaldm) )*100) )),2)) m_real
from PBI_vwFuncionarios f, frq_digitacaomovimento m
where f.codintfunc = m.codintfunc
and   m.iddigit = 1
And m.statusdigit = 'N'
and   m.tipodigit = 'F'
and   m.codocorr in (96,99,98)
and   f.CODFUNCAO in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and   m.iddigit = 1
and   m.dtdigit between trunc(add_months(last_day(Sysdate)+1,-12)) and trunc(sysdate)
and   f.codigoempresa = 13 and f.codigofl = 1
and   m.normaldm < f.jornadafunc and m.normaldm > 0
Group By f.nomefunc, f.codfunc,
        To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy')
order by f.codfunc, f.nomefunc))

union all

(Select '026/001 - VUG DUTRA' empresa,
        '026/001' EmpFil,  nomefunc, dtdigit, jornadafunc, normaldigit,
        m_jornada, m_real, (m_jornada - m_real) dif_min_tot
from
(select f.nomefunc, f.codfunc,
       To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy') dtdigit,
       max(f.jornadafunc) jornadafunc,
       Sum(trunc((( (trunc(f.jornadafunc) * 60 ) + ( ((f.jornadafunc) - trunc(f.jornadafunc) )*100) )),2)) m_jornada,
       Max(m.normaldm) normaldigit,
       Sum(trunc((( (trunc(m.normaldm) * 60 ) + ( ((m.normaldm) - trunc(m.normaldm) )*100) )),2)) m_real
from PBI_vwFuncionarios f, frq_digitacaomovimento m
where f.codintfunc = m.codintfunc
and   m.iddigit = 1
And m.statusdigit = 'N'
and   m.tipodigit = 'F'
and   m.codocorr in (96,99,98)
and   f.CODFUNCAO in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and   m.iddigit = 1
and   m.dtdigit between trunc(add_months(last_day(Sysdate)+1,-12)) and trunc(sysdate)
and   f.codigoempresa = 26 and f.codigofl = 1
and   m.normaldm < f.jornadafunc and m.normaldm > 0
Group By f.nomefunc, f.codfunc,
        To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy')
order by f.codfunc, f.nomefunc))

union all

(Select '026/003 - VUG BEBEDOURO' empresa,
        '026/003' EmpFil, nomefunc, dtdigit, jornadafunc, normaldigit,
        m_jornada, m_real, (m_jornada - m_real) dif_min_tot
from
(select f.nomefunc, f.codfunc,
       To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy') dtdigit,
       max(f.jornadafunc) jornadafunc,
       Sum(trunc((( (trunc(f.jornadafunc) * 60 ) + ( ((f.jornadafunc) - trunc(f.jornadafunc) )*100) )),2)) m_jornada,
       Max(m.normaldm) normaldigit,
       Sum(trunc((( (trunc(m.normaldm) * 60 ) + ( ((m.normaldm) - trunc(m.normaldm) )*100) )),2)) m_real
from PBI_vwFuncionarios f, frq_digitacaomovimento m
where f.codintfunc = m.codintfunc
and   m.iddigit = 1
And m.statusdigit = 'N'
and   m.tipodigit = 'F'
and   m.codocorr in (96,99,98)
and   f.CODFUNCAO in (1,170,241,242,327,393,413,502,526,527,65,671,674,699,76,773,99,2,226,228,432,700,79)
and   m.iddigit = 1
and   m.dtdigit between trunc(add_months(last_day(Sysdate)+1,-12)) and trunc(sysdate)
and   f.codigoempresa = 26 and f.codigofl = 3
and   m.normaldm < f.jornadafunc and m.normaldm > 0
Group By f.nomefunc, f.codfunc,
        To_Date( '01/' ||To_Char( m.dtdigit, 'mm/yyyy'), 'dd/mm/yyyy')
order by f.codfunc, f.nomefunc))

