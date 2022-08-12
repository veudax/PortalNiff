select  EmpFil, Last_Day(data), Trunc(Sum(Consumo),2) Consumo
 From ( select sum(i.valortotalitensmovto) *-1 consumo, e.datamovto data,
          LPad( e.codigoempresa,3,'0') || '/' || lpad(e.codigofl, 3,'0') EmpFil
          from est_movto e, est_itensmovto i, est_cadmaterial c, Est_Requisicao r
         where e.seqmovto = i.seqmovto and
               i.codigomatint = c.codigomatint and
               e.datamovto = i.datamovto And --R$ 445.916,94
               c.codigogrd in (500,510) and
               e.codigohismov in (4,7,9,11,15,21,23,25) and
               i.statusitensmovto = 'F' and
               e.codigoempresa In (1,26) and e.codigofl In (1,2) and
               e.datamovto between '01-jun-2017' and '30-jun-2017'--(TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
           And r.numerorq = e.numerorq
           And r.codcusto = 3021
         group by e.datamovto, LPad( e.codigoempresa,3,'0') || '/' || lpad(e.codigofl, 3,'0')
         Union All 
        select sum(i.valortotalitensmovto) consumo, e.datamovto Data,
              LPad( e.codigoempresa,3,'0') || '/' || lpad(e.codigofl, 3,'0') EmpFil
          from est_movto e, est_itensmovto i, est_cadmaterial c
         where e.seqmovto = i.seqmovto and
               i.codigomatint = c.codigomatint and
               e.datamovto = i.datamovto And --R$ 445.916,94
               c.codigogrd in (500,510) and
               e.codigohismov in (4,7,9,11,15,21,23,25) and
               i.statusitensmovto = 'F' and
               e.codigoempresa In (1,26) and e.codigofl In (1,2) and
               e.datamovto between '01-jan-2017' and '30-jun-2017'--(TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
         group by e.datamovto, LPad( e.codigoempresa,3,'0') || '/' || lpad(e.codigofl, 3,'0') )
 Group By EmpFil,Last_Day(data)