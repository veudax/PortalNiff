create or replace view pbi_periodo as
select count(a.data) qtd_dia, a.tipo
from
    (select
           p.data,
           decode((to_char(p.data,'d')),'1','Domingo','7','Sabado','Dia util') tipo
     from
           dim_ctr_periodo p
     where p.data between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, 0)) and (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, +1)))a
where a.tipo = 'Dia util'
group by a.tipo

