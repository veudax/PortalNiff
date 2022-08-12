create or replace view pbi_totalkm as
Select EmpFil,dthist, totalkm,
         To_char(dthist,'mm/yyyy') MesAno,
         o.codocorr || ' - ' || o.desccomplhist ocorrencia
  From frq_ocorrencia o,
     ( Select lpad(b.codigoempresa,3,'0') || '/' || Lpad(b.codigofl,3,'0') EmpFil,
              To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy') dthist, sum(b.kmpercorridoveloc) totalkm
         from bgm_velocimetro b
        where b.dataveloc between '01-jan-2016' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
          and b.codigoempresa || b.codigofl In (11,12,21,31,41,51,61,91,131,261,263)
        group by To_Date('01/' ||To_char(b.dataveloc, 'mm/yyyy'),'dd/mm/yyyy'),
                 lpad(b.codigoempresa,3,'0') || '/' || Lpad(b.codigofl,3,'0')
     ) a

