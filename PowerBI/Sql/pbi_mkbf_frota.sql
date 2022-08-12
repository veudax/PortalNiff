create or replace view pbi_mkbf_frota as
(select x.empresa, x.frota, x.data, sum(x.totalra)totalra, sum(x.totalsos)totalsos, sum(x.totalkm)totalkm, sum(x.consumo)consumo
from

(select '003/001 - RAPIDO D OESTE' empresa, tv.descricaotpfrota frota, m.dataaberturaos data, count(distinct m.numeroos) totalra, 0 totalsos, 0 totalkm, 0 consumo
from man_os m, frt_tipodefrota tv, frt_cadveiculos f
where m.dataaberturaos between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
and   m.codigoveic = f.codigoveic
and   f.codigotpfrota = tv.codigotpfrota
and   tv.codigotpfrota in (1,5,6,8,10,50,52)
and   m.codorigos in (2) and m.codigoempresa = 3 and m.codigofl = 1
group by m.dataaberturaos, tv.descricaotpfrota

union all

select '003/001 - RAPIDO D OESTE' empresa, tv.descricaotpfrota frota, m.dataaberturaos data, 0 totalra, count(distinct m.numeroos) totalsos, 0 totalkm, 0 consumo
from man_os m, frt_tipodefrota tv, frt_cadveiculos f
where m.dataaberturaos between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
and   m.codigoveic = f.codigoveic
and   f.codigotpfrota = tv.codigotpfrota
and   tv.codigotpfrota in (1,5,6,8,10,50,52)
and   m.codorigos in (3) and m.codigoempresa = 3 and m.codigofl = 1
group by m.dataaberturaos, tv.descricaotpfrota

union all

select '003/001 - RAPIDO D OESTE' empresa, tv.descricaotpfrota frota, b.dataveloc data, 0 totalra, 0 totalsos, sum(b.kmpercorridoveloc) totalkm, 0 consumo
from bgm_velocimetro b, frt_tipodefrota tv, frt_cadveiculos f
where b.dataveloc between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
and   b.codigoveic = f.codigoveic
and   tv.codigotpfrota in (1,5,6,8,10,50,52)
and   f.codigotpfrota = tv.codigotpfrota
and   b.codigoempresa = 3 and b.codigofl = 1
group by b.dataveloc, tv.descricaotpfrota

union all

select  '003/001 - RAPIDO D OESTE' empresa, tv.descricaotpfrota frota, e.datamovto data, 0 totalra, 0 totalsos, 0 totalkm, sum(i.valortotalitensmovto) consumo
from    est_movto e, est_itensmovto i, est_requisicao r, est_cadmaterial c, frt_cadveiculos f, frt_tipodefrota tv
where   e.seqmovto = i.seqmovto and
        f.codigotpfrota = tv.codigotpfrota and
        r.numerorq = e.numerorq and
        r.codigoveic = f.codigoveic and
        i.codigomatint = c.codigomatint and
        e.datamovto = i.datamovto and
        tv.codigotpfrota in (1,5,6,8,10,50,52) and
        c.codigogrd in (500,510) and
        e.codigohismov in (4,5,7,9,11,13,15,16,18,19,20,21,23) and
        i.statusitensmovto = 'F' and
        e.codigoempresa = 3 and e.codigofl = 1 and
        e.datamovto between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
group by e.datamovto, tv.descricaotpfrota)x
group by x.empresa, x.frota, x.data)

