-- Indicador NF - original
((Select distinct '001 - EOVG DUTRA' EMPRESA, '01 - Pedidos' CLASS, 'Total de Pedidos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.NUMEROPEDIDO) TOTAL
from          
          (select * from cpr_pedidocompras
          where datapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   codigoempresa = 1 and codigofl = 1
          and   statuspedido = 'F')NF
)
UNION ALL
select distinct '001 - EOVG DUTRA' EMPRESA, '02 - Pedidos Fechados - LanCamento de Notas' CLASS, 'Resultado' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, ((nf.TOTAL + nfo.TOTAL) - nfa.Total)Total
from
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, i.dataentregapedido from cpr_pedidocompras c, cpr_itensdepedido i
          where i.dataentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   i.numeropedido = c.numeropedido
          and   c.codigoempresa = 1 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NF,
          
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, o.dataprevistaentregapedido from cpr_pedidocompras c, cpr_itensoutrospedido o
          where o.dataprevistaentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   o.numeropedido = c.numeropedido
          and   c.codigoempresa = 1 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NFO,          

(         select count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 1 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF)NFA
UNION ALL        
(Select distinct '001 - EOVG DUTRA' EMPRESA, '03 - Entrada' CLASS, 'Total LanCamentos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 1 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
)
------
UNION ALL
(Select distinct '001 - EOVG DUTRA' EMPRESA, '04 - Atraso LanCamento' CLASS, '(Entrada NF - Emissao NF) > 3' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select (b.entradasaidanf - b.dataemissaonf) qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.entradasaidanf > b.dataemissaonf
        and   b.codintnf = m.codintnf
        and   b.codigoempresa = 1 and b.codigofl = 1
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where NF.qtd_dia > 3)
-----
UNION ALL
-----
(Select distinct '001 - EOVG DUTRA' EMPRESA, '05 - Atraso IntegraCao (CPG)' CLASS, '(Integracao NF - Entrada NF) > 3' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 1 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   (NF.qtd_dia > 3.99))
UNION ALL
-----------
(Select distinct '001 - EOVG DUTRA' EMPRESA, '06 - Nao Integradas' CLASS, 'Legado' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 1 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   NF.lanctointegradocpg = 'N' or NF.lanctointegradoctb = 'N' or NF.lanctointegradoesf = 'N')
--------
UNION ALL
----------
(Select distinct '001 - EOVG DUTRA' EMPRESA, '07 - Atraso Conferencia' CLASS, '(Conferencia - Vencimento) <= 1' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(distinct nf.numeronf) total
--        NF.logaltdados Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, NF.docconciliado Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') logaltdados, b.codtpdoc, c.vencprorrogcpg,
       c.vencprorrogcpg - to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') QTD_DIA, c.vencprorrogcpg - sysdate QTD_N, e.docconciliado, m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from esfentra e, bgm_notafiscal b , est_movto m, cpgdocto c
       where e.codigoempresa = 1 and e.codigofl = 1
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   b.coddoctoesf = e.coddoctoesf
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  ((NF.QTD_DIA <= 1) or (NF.DOCCONCILIADO = 'N' and QTD_N <= 1)))
UNION ALL
(Select distinct '001 - EOVG DUTRA' EMPRESA, '08 - Atraso Vencimento x IntegraCao' CLASS, 'Integracao >= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 1 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg >= NF.vencprorrogcpg ))
UNION ALL
(Select distinct '001 - EOVG DUTRA' EMPRESA, '09 - IntegraCao Vencimento' CLASS, 'Notas integradas até o vencimento' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 1 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg < NF.vencprorrogcpg ))
----------
UNION ALL
----------
(Select distinct '001 - EOVG DUTRA' EMPRESA, '10 - Atraso Pagamento' CLASS, 'Pagamento > Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        TO_CHAR(NF.dataemissaonf) Data_Emissao, TO_CHAR(NF.entradasaidanf) DATA_ENTRADA,
--        TO_CHAR(NF.vencprorrogcpg) Data_Vencimento, TO_CHAR(NF.vencprorrogcpg) Vencimento_Original, TO_CHAR(NF.pagamentocpg) Data_Pagamento, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, c.vencprorrogcpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 1 and c.codigofl = 1
       and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) AND (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       and   c.codmovtobco is not null
       and   c.codtpdoc <> 'AD'
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.QTD_DIA < -2))
-------------
UNION ALL
-------------
(Select distinct '001 - EOVG DUTRA' EMPRESA, '11 - Pagamento Correto' CLASS, 'Pagamento <= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 1 and c.codigofl = 1
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   c.codmovtobco is not null
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.pagamentocpg <= NF.vencprorrogcpg)))
union all
-----------------------eovg lavras------------------
((Select distinct '001 - EOVG LAVRAS' EMPRESA, '01 - Pedidos' CLASS, 'Total de Pedidos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.NUMEROPEDIDO) TOTAL
from          
          (select * from cpr_pedidocompras
          where datapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   codigoempresa = 1 and codigofl = 2
          and   statuspedido = 'F')NF
)
UNION ALL
select distinct '001 - EOVG LAVRAS' EMPRESA, '02 - Pedidos Fechados - LanCamento de Notas' CLASS, 'Resultado' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, nf.TOTAL - nfa.Total
from
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, i.dataentregapedido from cpr_pedidocompras c, cpr_itensdepedido i
          where i.dataentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   i.numeropedido = c.numeropedido
          and   c.codigoempresa = 1 and c.codigofl = 2
          and   c.statuspedido = 'F')NF)NF,
          
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, o.dataprevistaentregapedido from cpr_pedidocompras c, cpr_itensoutrospedido o
          where o.dataprevistaentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   o.numeropedido = c.numeropedido
          and   c.codigoempresa = 1 and c.codigofl = 2
          and   c.statuspedido = 'F')NF)NFO,          

(         select count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 1 and b.codigofl = 2
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF)NFA
UNION ALL        
(Select distinct '001 - EOVG LAVRAS' EMPRESA, '03 - Entrada' CLASS, 'Total LanCamentos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 1 and b.codigofl = 2
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
)
------
UNION ALL
(Select distinct '001 - EOVG LAVRAS' EMPRESA, '04 - Atraso LanCamento' CLASS, '(Entrada NF - Emissao NF) > 3' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select (b.entradasaidanf - b.dataemissaonf) qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.entradasaidanf > b.dataemissaonf
        and   b.codintnf = m.codintnf
        and   b.codigoempresa = 1 and b.codigofl = 2
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where NF.qtd_dia > 3)
-----
UNION ALL
-----
(Select distinct '001 - EOVG LAVRAS' EMPRESA, '05 - Atraso IntegraCao (CPG)' CLASS, '(Integracao NF - Entrada NF) > 3' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 1 and m.codigofl = 2
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   (NF.qtd_dia > 3.99))
UNION ALL
-----------
(Select distinct '001 - EOVG LAVRAS' EMPRESA, '06 - Nao Integradas' CLASS, 'Legado' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 1 and m.codigofl = 2
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   NF.lanctointegradocpg = 'N' or NF.lanctointegradoctb = 'N' or NF.lanctointegradoesf = 'N')
--------
UNION ALL
----------
(Select distinct '001 - EOVG LAVRAS' EMPRESA, '07 - Atraso Conferencia' CLASS, '(Conferencia - Vencimento) <= 1' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(distinct nf.numeronf) total
--        NF.logaltdados Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, NF.docconciliado Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') logaltdados, b.codtpdoc, c.vencprorrogcpg,
       c.vencprorrogcpg - to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') QTD_DIA, c.vencprorrogcpg - sysdate QTD_N, e.docconciliado, m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from esfentra e, bgm_notafiscal b , est_movto m, cpgdocto c
       where e.codigoempresa = 1 and e.codigofl = 2
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   b.coddoctoesf = e.coddoctoesf
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  ((NF.QTD_DIA <= 1) or (NF.DOCCONCILIADO = 'N' and QTD_N <= 1)))
UNION ALL
(Select distinct '001 - EOVG LAVRAS' EMPRESA, '08 - Atraso Vencimento x IntegraCao' CLASS, 'Integracao >= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 1 and c.codigofl = 2
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg >= NF.vencprorrogcpg ))
UNION ALL
(Select distinct '001 - EOVG LAVRAS' EMPRESA, '09 - IntegraCao Vencimento' CLASS, 'Notas integradas até o vencimento' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 1 and c.codigofl = 2
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg < NF.vencprorrogcpg ))
----------
UNION ALL
----------
(Select distinct '001 - EOVG LAVRAS' EMPRESA, '10 - Atraso Pagamento' CLASS, 'Pagamento > Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 1 and c.codigofl = 2
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       and   c.codmovtobco is not null       
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.QTD_DIA < -2))
-------------
UNION ALL
-------------
(Select distinct '001 - EOVG LAVRAS' EMPRESA, '11 - Pagamento Correto' CLASS, 'Pagamento <= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 1 and c.codigofl = 2
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   c.codmovtobco is not null       
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.pagamentocpg <= NF.vencprorrogcpg)))
union all
-----------------------abct-------------------------
((Select distinct '002 - ABC TRANSP' EMPRESA, '01 - Pedidos' CLASS, 'Total de Pedidos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.NUMEROPEDIDO) TOTAL
from          
          (select * from cpr_pedidocompras
          where datapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   codigoempresa = 2 and codigofl = 1
          and   statuspedido = 'F')NF
)
UNION ALL
select distinct '002 - ABC TRANSP' EMPRESA, '02 - Pedidos Fechados - LanCamento de Notas' CLASS, 'Resultado' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, nf.TOTAL - nfa.Total
from
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, i.dataentregapedido from cpr_pedidocompras c, cpr_itensdepedido i
          where i.dataentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   i.numeropedido = c.numeropedido
          and   c.codigoempresa = 2 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NF,
          
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, o.dataprevistaentregapedido from cpr_pedidocompras c, cpr_itensoutrospedido o
          where o.dataprevistaentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   o.numeropedido = c.numeropedido
          and   c.codigoempresa = 2 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NFO,          

(         select count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 2 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF)NFA
UNION ALL        
(Select distinct '002 - ABC TRANSP' EMPRESA, '03 - Entrada' CLASS, 'Total LanCamentos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 2 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
)
------
UNION ALL
(Select distinct '002 - ABC TRANSP' EMPRESA, '04 - Atraso LanCamento' CLASS, '(Entrada NF - Emissao NF) > 3' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select (b.entradasaidanf - b.dataemissaonf) qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.entradasaidanf > b.dataemissaonf
        and   b.codintnf = m.codintnf
        and   b.codigoempresa = 2 and b.codigofl = 1
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where NF.qtd_dia > 3)
-----
UNION ALL
-----
(Select distinct '002 - ABC TRANSP' EMPRESA, '05 - Atraso IntegraCao (CPG)' CLASS, '(Integracao NF - Entrada NF) > 3' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 2 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   (NF.qtd_dia > 3.99))
UNION ALL
-----------
(Select distinct '002 - ABC TRANSP' EMPRESA, '06 - Nao Integradas' CLASS, 'Legado' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 2 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   NF.lanctointegradocpg = 'N' or NF.lanctointegradoctb = 'N' or NF.lanctointegradoesf = 'N')
--------
UNION ALL
----------
(Select distinct '002 - ABC TRANSP' EMPRESA, '07 - Atraso Conferencia' CLASS, '(Conferencia - Vencimento) <= 1' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(distinct nf.numeronf) total
--        NF.logaltdados Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, NF.docconciliado Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') logaltdados, b.codtpdoc, c.vencprorrogcpg,
       c.vencprorrogcpg - to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') QTD_DIA, c.vencprorrogcpg - sysdate QTD_N, e.docconciliado, m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from esfentra e, bgm_notafiscal b , est_movto m, cpgdocto c
       where e.codigoempresa = 2 and e.codigofl = 1
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   b.coddoctoesf = e.coddoctoesf
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  ((NF.QTD_DIA <= 1) or (NF.DOCCONCILIADO = 'N' and QTD_N <= 1)))
UNION ALL
(Select distinct '002 - ABC TRANSP' EMPRESA, '08 - Atraso Vencimento x IntegraCao' CLASS, 'Integracao >= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 2 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg >= NF.vencprorrogcpg ))
UNION ALL
(Select distinct '002 - ABC TRANSP' EMPRESA, '09 - IntegraCao Vencimento' CLASS, 'Notas integradas até o vencimento' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 2 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg < NF.vencprorrogcpg ))
----------
UNION ALL
----------
(Select distinct '002 - ABC TRANSP' EMPRESA, '10 - Atraso Pagamento' CLASS, 'Pagamento > Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 2 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       and   c.codmovtobco is not null       
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.QTD_DIA < -2))
-------------
UNION ALL
-------------
(Select distinct '002 - ABC TRANSP' EMPRESA, '11 - Pagamento Correto' CLASS, 'Pagamento <= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 2 and c.codigofl = 1
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   c.codmovtobco is not null       
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.pagamentocpg <= NF.vencprorrogcpg)))
UNION ALL
----------------------Rapido------------------------
((Select distinct '003 - RAPIDO OESTE' EMPRESA, '01 - Pedidos' CLASS, 'Total de Pedidos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.NUMEROPEDIDO) TOTAL
from          
          (select * from cpr_pedidocompras
          where datapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   codigoempresa = 3 and codigofl = 1
          and   statuspedido = 'F')NF
)
UNION ALL
select distinct '003 - RAPIDO OESTE' EMPRESA, '02 - Pedidos Fechados - LanCamento de Notas' CLASS, 'Resultado' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, nf.TOTAL - nfa.Total
from
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, i.dataentregapedido from cpr_pedidocompras c, cpr_itensdepedido i
          where i.dataentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   i.numeropedido = c.numeropedido
          and   c.codigoempresa = 3 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NF,
          
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, o.dataprevistaentregapedido from cpr_pedidocompras c, cpr_itensoutrospedido o
          where o.dataprevistaentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   o.numeropedido = c.numeropedido
          and   c.codigoempresa = 3 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NFO,          

(         select count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 3 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF)NFA
UNION ALL        
(Select distinct '003 - RAPIDO OESTE' EMPRESA, '03 - Entrada' CLASS, 'Total LanCamentos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 3 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
)
------
UNION ALL
(Select distinct '003 - RAPIDO OESTE' EMPRESA, '04 - Atraso LanCamento' CLASS, '(Entrada NF - Emissao NF) > 3' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select (b.entradasaidanf - b.dataemissaonf) qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.entradasaidanf > b.dataemissaonf
        and   b.codintnf = m.codintnf
        and   b.codigoempresa = 3 and b.codigofl = 1
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where NF.qtd_dia > 3)
-----
UNION ALL
-----
(Select distinct '003 - RAPIDO OESTE' EMPRESA, '05 - Atraso IntegraCao (CPG)' CLASS, '(Integracao NF - Entrada NF) > 3' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 3 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   (NF.qtd_dia > 3.99))
UNION ALL
-----------
(Select distinct '003 - RAPIDO OESTE' EMPRESA, '06 - Nao Integradas' CLASS, 'Legado' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 3 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   NF.lanctointegradocpg = 'N' or NF.lanctointegradoctb = 'N' or NF.lanctointegradoesf = 'N')
--------
UNION ALL
----------
(Select distinct '003 - RAPIDO OESTE' EMPRESA, '07 - Atraso Conferencia' CLASS, '(Conferencia - Vencimento) <= 1' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(distinct nf.numeronf) total
--        NF.logaltdados Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, NF.docconciliado Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') logaltdados, b.codtpdoc, c.vencprorrogcpg,
       c.vencprorrogcpg - to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') QTD_DIA, c.vencprorrogcpg - sysdate QTD_N, e.docconciliado, m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from esfentra e, bgm_notafiscal b , est_movto m, cpgdocto c
       where e.codigoempresa = 3 and e.codigofl = 1
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   b.coddoctoesf = e.coddoctoesf
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  ((NF.QTD_DIA <= 1) or (NF.DOCCONCILIADO = 'N' and QTD_N <= 1)))
UNION ALL
(Select distinct '003 - RAPIDO OESTE' EMPRESA, '08 - Atraso Vencimento x IntegraCao' CLASS, 'Integracao >= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 3 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg >= NF.vencprorrogcpg ))
UNION ALL
(Select distinct '003 - RAPIDO OESTE' EMPRESA, '09 - IntegraCao Vencimento' CLASS, 'Notas integradas até o vencimento' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 3 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg < NF.vencprorrogcpg ))
----------
UNION ALL
----------
(Select distinct '003 - RAPIDO OESTE' EMPRESA, '10 - Atraso Pagamento' CLASS, 'Pagamento > Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 3 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       and   c.codmovtobco is not null       
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.QTD_DIA < -2))
-------------
UNION ALL
-------------
(Select distinct '003 - RAPIDO OESTE' EMPRESA, '11 - Pagamento Correto' CLASS, 'Pagamento <= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 3 and c.codigofl = 1
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   c.codmovtobco is not null
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.pagamentocpg <= NF.vencprorrogcpg)))
union all
-----------------------cisne branco-----------------
((Select distinct '004 - CISNE BRANCO' EMPRESA, '01 - Pedidos' CLASS, 'Total de Pedidos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.NUMEROPEDIDO) TOTAL
from          
          (select * from cpr_pedidocompras
          where datapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   codigoempresa = 4 and codigofl = 1
          and   statuspedido = 'F')NF
)
UNION ALL
select distinct '004 - CISNE BRANCO' EMPRESA, '02 - Pedidos Fechados - LanCamento de Notas' CLASS, 'Resultado' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, nf.TOTAL - nfa.Total
from
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, i.dataentregapedido from cpr_pedidocompras c, cpr_itensdepedido i
          where i.dataentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   i.numeropedido = c.numeropedido
          and   c.codigoempresa = 4 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NF,
          
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, o.dataprevistaentregapedido from cpr_pedidocompras c, cpr_itensoutrospedido o
          where o.dataprevistaentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   o.numeropedido = c.numeropedido
          and   c.codigoempresa = 4 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NFO,          

(         select count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 4 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF)NFA
UNION ALL        
(Select distinct '004 - CISNE BRANCO' EMPRESA, '03 - Entrada' CLASS, 'Total LanCamentos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 4 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
)
------
UNION ALL
(Select distinct '004 - CISNE BRANCO' EMPRESA, '04 - Atraso LanCamento' CLASS, '(Entrada NF - Emissao NF) > 3' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select (b.entradasaidanf - b.dataemissaonf) qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.entradasaidanf > b.dataemissaonf
        and   b.codintnf = m.codintnf
        and   b.codigoempresa = 4 and b.codigofl = 1
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where NF.qtd_dia > 3)
-----
UNION ALL
-----
(Select distinct '004 - CISNE BRANCO' EMPRESA, '05 - Atraso IntegraCao (CPG)' CLASS, '(Integracao NF - Entrada NF) > 3' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 4 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   (NF.qtd_dia > 3.99))
UNION ALL
-----------
(Select distinct '004 - CISNE BRANCO' EMPRESA, '06 - Nao Integradas' CLASS, 'Legado' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 4 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   NF.lanctointegradocpg = 'N' or NF.lanctointegradoctb = 'N' or NF.lanctointegradoesf = 'N')
--------
UNION ALL
----------
(Select distinct '004 - CISNE BRANCO' EMPRESA, '07 - Atraso Conferencia' CLASS, '(Conferencia - Vencimento) <= 1' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(distinct nf.numeronf) total
--        NF.logaltdados Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, NF.docconciliado Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') logaltdados, b.codtpdoc, c.vencprorrogcpg,
       c.vencprorrogcpg - to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') QTD_DIA, c.vencprorrogcpg - sysdate QTD_N, e.docconciliado, m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from esfentra e, bgm_notafiscal b , est_movto m, cpgdocto c
       where e.codigoempresa = 4 and e.codigofl = 1
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   b.coddoctoesf = e.coddoctoesf
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  ((NF.QTD_DIA <= 1) or (NF.DOCCONCILIADO = 'N' and QTD_N <= 1)))
UNION ALL
(Select distinct '004 - CISNE BRANCO' EMPRESA, '08 - Atraso Vencimento x IntegraCao' CLASS, 'Integracao >= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 4 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg >= NF.vencprorrogcpg ))
UNION ALL
(Select distinct '004 - CISNE BRANCO' EMPRESA, '09 - IntegraCao Vencimento' CLASS, 'Notas integradas até o vencimento' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 4 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg < NF.vencprorrogcpg ))
----------
UNION ALL
----------
(Select distinct '004 - CISNE BRANCO' EMPRESA, '10 - Atraso Pagamento' CLASS, 'Pagamento > Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 4 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       and   c.codmovtobco is not null       
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.QTD_DIA < -2))
-------------
UNION ALL
-------------
(Select distinct '004 - CISNE BRANCO' EMPRESA, '11 - Pagamento Correto' CLASS, 'Pagamento <= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 4 and c.codigofl = 1
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   c.codmovtobco is not null       
       and   m.codigohismov = 14
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.pagamentocpg <= NF.vencprorrogcpg)))
union all
-----------------------niff-------------------------
((Select distinct '005 - NIFF' EMPRESA, '01 - Pedidos' CLASS, 'Total de Pedidos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.NUMEROPEDIDO) TOTAL
from          
          (select * from cpr_pedidocompras
          where datapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   codigoempresa = 5 and codigofl = 1
          and   statuspedido = 'F')NF
)
UNION ALL
select distinct '005 - NIFF' EMPRESA, '02 - Pedidos Fechados - LanCamento de Notas' CLASS, 'Resultado' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, nf.TOTAL - nfa.Total
from
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, i.dataentregapedido from cpr_pedidocompras c, cpr_itensdepedido i
          where i.dataentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   i.numeropedido = c.numeropedido
          and   c.codigoempresa = 5 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NF,
          
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, o.dataprevistaentregapedido from cpr_pedidocompras c, cpr_itensoutrospedido o
          where o.dataprevistaentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   o.numeropedido = c.numeropedido
          and   c.codigoempresa = 5 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NFO,          

(         select count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 5 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF)NFA
UNION ALL        
(Select distinct '005 - NIFF' EMPRESA, '03 - Entrada' CLASS, 'Total LanCamentos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 5 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
)
------
UNION ALL
(Select distinct '005 - NIFF' EMPRESA, '04 - Atraso LanCamento' CLASS, '(Entrada NF - Emissao NF) > 3' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select (b.entradasaidanf - b.dataemissaonf) qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.entradasaidanf > b.dataemissaonf
        and   b.codintnf = m.codintnf
        and   b.codigoempresa = 5 and b.codigofl = 1
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where NF.qtd_dia > 3)
-----
UNION ALL
-----
(Select distinct '005 - NIFF' EMPRESA, '05 - Atraso IntegraCao (CPG)' CLASS, '(Integracao NF - Entrada NF) > 3' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 5 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   (NF.qtd_dia > 3.99))
UNION ALL
-----------
(Select distinct '005 - NIFF' EMPRESA, '06 - Nao Integradas' CLASS, 'Legado' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 5 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   NF.lanctointegradocpg = 'N' or NF.lanctointegradoctb = 'N' or NF.lanctointegradoesf = 'N')
--------
UNION ALL
----------
(Select distinct '005 - NIFF' EMPRESA, '07 - Atraso Conferencia' CLASS, '(Conferencia - Vencimento) <= 1' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(distinct nf.numeronf) total
--        NF.logaltdados Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, NF.docconciliado Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') logaltdados, b.codtpdoc, c.vencprorrogcpg,
       c.vencprorrogcpg - to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') QTD_DIA, c.vencprorrogcpg - sysdate QTD_N, e.docconciliado, m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from esfentra e, bgm_notafiscal b , est_movto m, cpgdocto c
       where e.codigoempresa = 5 and e.codigofl = 1
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   b.coddoctoesf = e.coddoctoesf
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  ((NF.QTD_DIA <= 1) or (NF.DOCCONCILIADO = 'N' and QTD_N <= 1)))
UNION ALL
(Select distinct '005 - NIFF' EMPRESA, '08 - Atraso Vencimento x IntegraCao' CLASS, 'Integracao >= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 5 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg >= NF.vencprorrogcpg ))
UNION ALL
(Select distinct '005 - NIFF' EMPRESA, '09 - IntegraCao Vencimento' CLASS, 'Notas integradas até o vencimento' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 5 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg < NF.vencprorrogcpg ))
----------
UNION ALL
----------
(Select distinct '005 - NIFF' EMPRESA, '10 - Atraso Pagamento' CLASS, 'Pagamento > Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 5 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       and   c.codmovtobco is not null       
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.QTD_DIA < -2))
-------------
UNION ALL
-------------
(Select distinct '005 - NIFF' EMPRESA, '11 - Pagamento Correto' CLASS, 'Pagamento <= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 5 and c.codigofl = 1
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   c.codmovtobco is not null       
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.pagamentocpg <= NF.vencprorrogcpg)))
UNION ALL
-----------------------ARUJA------------------------
((Select distinct '006 - VIACAO ARUJA' EMPRESA, '01 - Pedidos' CLASS, 'Total de Pedidos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.NUMEROPEDIDO) TOTAL
from          
          (select * from cpr_pedidocompras
          where datapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   codigoempresa = 6 and codigofl = 1
          and   statuspedido = 'F')NF
)
UNION ALL
select distinct '006 - VIACAO ARUJA' EMPRESA, '02 - Pedidos Fechados - LanCamento de Notas' CLASS, 'Resultado' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, nf.TOTAL - nfa.Total
from
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, i.dataentregapedido from cpr_pedidocompras c, cpr_itensdepedido i
          where i.dataentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   i.numeropedido = c.numeropedido
          and   c.codigoempresa = 6 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NF,
          
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, o.dataprevistaentregapedido from cpr_pedidocompras c, cpr_itensoutrospedido o
          where o.dataprevistaentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   o.numeropedido = c.numeropedido
          and   c.codigoempresa = 6 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NFO,          

(         select count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 6 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF)NFA
UNION ALL        
(Select distinct '006 - VIACAO ARUJA' EMPRESA, '03 - Entrada' CLASS, 'Total LanCamentos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 6 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
)
------
UNION ALL
(Select distinct '006 - VIACAO ARUJA' EMPRESA, '04 - Atraso LanCamento' CLASS, '(Entrada NF - Emissao NF) > 3' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select (b.entradasaidanf - b.dataemissaonf) qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.entradasaidanf > b.dataemissaonf
        and   b.codintnf = m.codintnf
        and   b.codigoempresa = 6 and b.codigofl = 1
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where NF.qtd_dia > 3)
-----
UNION ALL
-----
(Select distinct '006 - VIACAO ARUJA' EMPRESA, '05 - Atraso IntegraCao (CPG)' CLASS, '(Integracao NF - Entrada NF) > 3' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 6 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   (NF.qtd_dia > 3.99))
UNION ALL
-----------
(Select distinct '006 - VIACAO ARUJA' EMPRESA, '06 - Nao Integradas' CLASS, 'Legado' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 6 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   NF.lanctointegradocpg = 'N' or NF.lanctointegradoctb = 'N' or NF.lanctointegradoesf = 'N')
--------
UNION ALL
----------
(Select distinct '006 - VIACAO ARUJA' EMPRESA, '07 - Atraso Conferencia' CLASS, '(Conferencia - Vencimento) <= 1' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(distinct nf.numeronf) total
--        NF.logaltdados Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, NF.docconciliado Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') logaltdados, b.codtpdoc, c.vencprorrogcpg,
       c.vencprorrogcpg - to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') QTD_DIA, c.vencprorrogcpg - sysdate QTD_N, e.docconciliado, m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from esfentra e, bgm_notafiscal b , est_movto m, cpgdocto c
       where e.codigoempresa = 6 and e.codigofl = 1
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   b.coddoctoesf = e.coddoctoesf
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  ((NF.QTD_DIA <= 1) or (NF.DOCCONCILIADO = 'N' and QTD_N <= 1)))
UNION ALL
(Select distinct '006 - VIACAO ARUJA' EMPRESA, '08 - Atraso Vencimento x IntegraCao' CLASS, 'Integracao >= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 6 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg >= NF.vencprorrogcpg ))
UNION ALL
(Select distinct '006 - VIACAO ARUJA' EMPRESA, '09 - IntegraCao Vencimento' CLASS, 'Notas integradas até o vencimento' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 6 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg < NF.vencprorrogcpg ))
----------
UNION ALL
----------
(Select distinct '006 - VIACAO ARUJA' EMPRESA, '10 - Atraso Pagamento' CLASS, 'Pagamento > Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 6 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   c.codmovtobco is not null       
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.QTD_DIA < -2))
-------------
UNION ALL
-------------
(Select distinct '006 - VIACAO ARUJA' EMPRESA, '11 - Pagamento Correto' CLASS, 'Pagamento <= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 6 and c.codigofl = 1
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   c.codmovtobco is not null       
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.pagamentocpg <= NF.vencprorrogcpg)))
union all
-----------------------ribe-------------------------
((Select distinct '013 - RIBE' EMPRESA, '01 - Pedidos' CLASS, 'Total de Pedidos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.NUMEROPEDIDO) TOTAL
from          
          (select * from cpr_pedidocompras
          where datapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   codigoempresa = 13 and codigofl = 1
          and   statuspedido = 'F')NF
)
UNION ALL
select distinct '013 - RIBE' EMPRESA, '02 - Pedidos Fechados - LanCamento de Notas' CLASS, 'Resultado' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, nf.TOTAL - nfa.Total
from
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, i.dataentregapedido from cpr_pedidocompras c, cpr_itensdepedido i
          where i.dataentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   i.numeropedido = c.numeropedido
          and   c.codigoempresa = 13 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NF,
          
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, o.dataprevistaentregapedido from cpr_pedidocompras c, cpr_itensoutrospedido o
          where o.dataprevistaentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   o.numeropedido = c.numeropedido
          and   c.codigoempresa = 13 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NFO,          

(         select count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 13 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF)NFA
UNION ALL        
(Select distinct '013 - RIBE' EMPRESA, '03 - Entrada' CLASS, 'Total LanCamentos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 13 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
)
------
UNION ALL
(Select distinct '013 - RIBE' EMPRESA, '04 - Atraso LanCamento' CLASS, '(Entrada NF - Emissao NF) > 3' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select (b.entradasaidanf - b.dataemissaonf) qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.entradasaidanf > b.dataemissaonf
        and   b.codintnf = m.codintnf
        and   b.codigoempresa = 13 and b.codigofl = 1
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where NF.qtd_dia > 3)
-----
UNION ALL
-----
(Select distinct '013 - RIBE' EMPRESA, '05 - Atraso IntegraCao (CPG)' CLASS, '(Integracao NF - Entrada NF) > 3' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 13 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   (NF.qtd_dia > 3.99))
UNION ALL
-----------
(Select distinct '013 - RIBE' EMPRESA, '06 - Nao Integradas' CLASS, 'Legado' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 13 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   NF.lanctointegradocpg = 'N' or NF.lanctointegradoctb = 'N' or NF.lanctointegradoesf = 'N')
--------
UNION ALL
----------
(Select distinct '013 - RIBE' EMPRESA, '07 - Atraso Conferencia' CLASS, '(Conferencia - Vencimento) <= 1' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(distinct nf.numeronf) total
--        NF.logaltdados Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, NF.docconciliado Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') logaltdados, b.codtpdoc, c.vencprorrogcpg,
       c.vencprorrogcpg - to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') QTD_DIA, c.vencprorrogcpg - sysdate QTD_N, e.docconciliado, m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from esfentra e, bgm_notafiscal b , est_movto m, cpgdocto c
       where e.codigoempresa = 13 and e.codigofl = 1
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   b.coddoctoesf = e.coddoctoesf
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  ((NF.QTD_DIA <= 1) or (NF.DOCCONCILIADO = 'N' and QTD_N <= 1)))
UNION ALL
(Select distinct '013 - RIBE' EMPRESA, '08 - Atraso Vencimento x IntegraCao' CLASS, 'Integracao >= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 13 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg >= NF.vencprorrogcpg ))
UNION ALL
(Select distinct '013 - RIBE' EMPRESA, '09 - IntegraCao Vencimento' CLASS, 'Notas integradas até o vencimento' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 13 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg < NF.vencprorrogcpg ))
----------
UNION ALL
----------
(Select distinct '013 - RIBE' EMPRESA, '10 - Atraso Pagamento' CLASS, 'Pagamento > Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 13 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       and   c.codmovtobco is not null       
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.QTD_DIA < -2))
-------------
UNION ALL
-------------
(Select distinct '013 - RIBE' EMPRESA, '11 - Pagamento Correto' CLASS, 'Pagamento <= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 13 and c.codigofl = 1
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   c.codmovtobco is not null       
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.pagamentocpg <= NF.vencprorrogcpg)))
union all
-----------------------vug dutra--------------------
((Select distinct '026 - VUG DUTRA' EMPRESA, '01 - Pedidos' CLASS, 'Total de Pedidos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.NUMEROPEDIDO) TOTAL
from          
          (select * from cpr_pedidocompras
          where datapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   codigoempresa = 26 and codigofl = 1
          and   statuspedido = 'F')NF
)
UNION ALL
select distinct '026 - VUG DUTRA' EMPRESA, '02 - Pedidos Fechados - LanCamento de Notas' CLASS, 'Resultado' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, nf.TOTAL - nfa.Total
from
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, i.dataentregapedido from cpr_pedidocompras c, cpr_itensdepedido i
          where i.dataentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   i.numeropedido = c.numeropedido
          and   c.codigoempresa = 26 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NF,
          
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, o.dataprevistaentregapedido from cpr_pedidocompras c, cpr_itensoutrospedido o
          where o.dataprevistaentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   o.numeropedido = c.numeropedido
          and   c.codigoempresa = 26 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NFO,          

(         select count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 26 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF)NFA
UNION ALL        
(Select distinct '026 - VUG DUTRA' EMPRESA, '03 - Entrada' CLASS, 'Total LanCamentos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 26 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
)
------
UNION ALL
(Select distinct '026 - VUG DUTRA' EMPRESA, '04 - Atraso LanCamento' CLASS, '(Entrada NF - Emissao NF) > 3' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select (b.entradasaidanf - b.dataemissaonf) qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.entradasaidanf > b.dataemissaonf
        and   b.codintnf = m.codintnf
        and   b.codigoempresa = 26 and b.codigofl = 1
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where NF.qtd_dia > 3)
-----
UNION ALL
-----
(Select distinct '026 - VUG DUTRA' EMPRESA, '05 - Atraso IntegraCao (CPG)' CLASS, '(Integracao NF - Entrada NF) > 3' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 26 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   (NF.qtd_dia > 3.99))
UNION ALL
-----------
(Select distinct '026 - VUG DUTRA' EMPRESA, '06 - Nao Integradas' CLASS, 'Legado' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 26 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   NF.lanctointegradocpg = 'N' or NF.lanctointegradoctb = 'N' or NF.lanctointegradoesf = 'N')
--------
UNION ALL
----------
(Select distinct '026 - VUG DUTRA' EMPRESA, '07 - Atraso Conferencia' CLASS, '(Conferencia - Vencimento) <= 1' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(distinct nf.numeronf) total
--        NF.logaltdados Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, NF.docconciliado Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') logaltdados, b.codtpdoc, c.vencprorrogcpg,
       c.vencprorrogcpg - to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') QTD_DIA, c.vencprorrogcpg - sysdate QTD_N, e.docconciliado, m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from esfentra e, bgm_notafiscal b , est_movto m, cpgdocto c
       where e.codigoempresa = 26 and e.codigofl = 1
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   b.coddoctoesf = e.coddoctoesf
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  ((NF.QTD_DIA <= 1) or (NF.DOCCONCILIADO = 'N' and QTD_N <= 1)))
UNION ALL
(Select distinct '026 - VUG DUTRA' EMPRESA, '08 - Atraso Vencimento x IntegraCao' CLASS, 'Integracao >= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 26 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg >= NF.vencprorrogcpg ))
UNION ALL
(Select distinct '026 - VUG DUTRA' EMPRESA, '09 - IntegraCao Vencimento' CLASS, 'Notas integradas até o vencimento' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 26 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg < NF.vencprorrogcpg ))
----------
UNION ALL
----------
(Select distinct '026 - VUG DUTRA' EMPRESA, '10 - Atraso Pagamento' CLASS, 'Pagamento > Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 26 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       and   c.codmovtobco is not null
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.QTD_DIA < -2))
-------------
UNION ALL
-------------
(Select distinct '026 - VUG DUTRA' EMPRESA, '11 - Pagamento Correto' CLASS, 'Pagamento <= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 26 and c.codigofl = 1
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   c.codmovtobco is not null       
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.pagamentocpg <= NF.vencprorrogcpg)))
-----------------------vug bebedouro----------------
union all
((Select distinct '026 - VUG BEBEDOURO' EMPRESA, '01 - Pedidos' CLASS, 'Total de Pedidos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.NUMEROPEDIDO) TOTAL
from          
          (select * from cpr_pedidocompras
          where datapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   codigoempresa = 26 and codigofl = 3
          and   statuspedido = 'F')NF
)
UNION ALL
select distinct '026 - VUG BEBEDOURO' EMPRESA, '02 - Pedidos Fechados - LanCamento de Notas' CLASS, 'Resultado' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, nf.TOTAL - nfa.Total
from
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, i.dataentregapedido from cpr_pedidocompras c, cpr_itensdepedido i
          where i.dataentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   i.numeropedido = c.numeropedido
          and   c.codigoempresa = 26 and c.codigofl = 3
          and   c.statuspedido = 'F')NF)NF,
          
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, o.dataprevistaentregapedido from cpr_pedidocompras c, cpr_itensoutrospedido o
          where o.dataprevistaentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   o.numeropedido = c.numeropedido
          and   c.codigoempresa = 26 and c.codigofl = 3
          and   c.statuspedido = 'F')NF)NFO,          

(         select count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 26 and b.codigofl = 3
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF)NFA
UNION ALL        
(Select distinct '026 - VUG BEBEDOURO' EMPRESA, '03 - Entrada' CLASS, 'Total LanCamentos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 26 and b.codigofl = 3
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
)
------
UNION ALL
(Select distinct '026 - VUG BEBEDOURO' EMPRESA, '04 - Atraso LanCamento' CLASS, '(Entrada NF - Emissao NF) > 3' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select (b.entradasaidanf - b.dataemissaonf) qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.entradasaidanf > b.dataemissaonf
        and   b.codintnf = m.codintnf
        and   b.codigoempresa = 26 and b.codigofl = 3
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where NF.qtd_dia > 3)
-----
UNION ALL
-----
(Select distinct '026 - VUG BEBEDOURO' EMPRESA, '05 - Atraso IntegraCao (CPG)' CLASS, '(Integracao NF - Entrada NF) > 3' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 26 and m.codigofl = 3
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   (NF.qtd_dia > 3.99))
UNION ALL
-----------
(Select distinct '026 - VUG BEBEDOURO' EMPRESA, '06 - Nao Integradas' CLASS, 'Legado' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 26 and m.codigofl = 3
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   NF.lanctointegradocpg = 'N' or NF.lanctointegradoctb = 'N' or NF.lanctointegradoesf = 'N')
--------
UNION ALL
----------
(Select distinct '026 - VUG BEBEDOURO' EMPRESA, '07 - Atraso Conferencia' CLASS, '(Conferencia - Vencimento) <= 1' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(distinct nf.numeronf) total
--        NF.logaltdados Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, NF.docconciliado Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') logaltdados, b.codtpdoc, c.vencprorrogcpg,
       c.vencprorrogcpg - to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') QTD_DIA, c.vencprorrogcpg - sysdate QTD_N, e.docconciliado, m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from esfentra e, bgm_notafiscal b , est_movto m, cpgdocto c
       where e.codigoempresa = 26 and e.codigofl = 3
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   b.coddoctoesf = e.coddoctoesf
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  ((NF.QTD_DIA <= 1) or (NF.DOCCONCILIADO = 'N' and QTD_N <= 1)))
UNION ALL
(Select distinct '026 - VUG BEBEDOURO' EMPRESA, '08 - Atraso Vencimento x IntegraCao' CLASS, 'Integracao >= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 26 and c.codigofl = 3
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg >= NF.vencprorrogcpg ))
UNION ALL
(Select distinct '026 - VUG BEBEDOURO' EMPRESA, '09 - IntegraCao Vencimento' CLASS, 'Notas integradas até o vencimento' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 26 and c.codigofl = 3
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg < NF.vencprorrogcpg ))
----------
UNION ALL
----------
(Select distinct '026 - VUG BEBEDOURO' EMPRESA, '10 - Atraso Pagamento' CLASS, 'Pagamento > Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 26 and c.codigofl = 3
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   c.codmovtobco is not null       
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.QTD_DIA < -2))
-------------
UNION ALL
-------------
(Select distinct '026 - VUG BEBEDOURO' EMPRESA, '11 - Pagamento Correto' CLASS, 'Pagamento <= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 26 and c.codigofl = 3
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   c.codmovtobco is not null       
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.pagamentocpg <= NF.vencprorrogcpg)))
union all
-----------------------campibus---------------------
((Select distinct '009 - CAMPIBUS' EMPRESA, '01 - Pedidos' CLASS, 'Total de Pedidos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.NUMEROPEDIDO) TOTAL
from          
          (select * from cpr_pedidocompras
          where datapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   codigoempresa = 9 and codigofl = 1
          and   statuspedido = 'F')NF
)
UNION ALL
select distinct '009 - CAMPIBUS' EMPRESA, '02 - Pedidos Fechados - Lancamento de Notas' CLASS, 'Resultado' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, nf.TOTAL - nfa.Total
from
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, i.dataentregapedido from cpr_pedidocompras c, cpr_itensdepedido i
          where i.dataentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   i.numeropedido = c.numeropedido
          and   c.codigoempresa = 9 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NF,
          
(         select count(nf.NUMEROPEDIDO) TOTAL
          from          
          (select distinct c.statuspedido, c.codigoempresa, c.codigofl, c.numeropedido, o.dataprevistaentregapedido from cpr_pedidocompras c, cpr_itensoutrospedido o
          where o.dataprevistaentregapedido BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
          and   o.numeropedido = c.numeropedido
          and   c.codigoempresa = 9 and c.codigofl = 1
          and   c.statuspedido = 'F')NF)NFO,          

(         select count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 9 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF)NFA
UNION ALL        
(Select distinct '009 - CAMPIBUS' EMPRESA, '03 - Entrada' CLASS, 'Total Lancamentos' FORMULA, 
          (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) TOTAL
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select b.entradasaidanf - b.dataemissaonf qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.codigoempresa = 9 and b.codigofl = 1
        and   b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
)
------
UNION ALL
(Select distinct '009 - CAMPIBUS' EMPRESA, '04 - Atraso Lancamento' CLASS, '(Entrada NF - Emissao NF) > 3' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.dataemissaonf Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo,
--        '' Integra_CPG, '' Integra_ESF, '' Integra_CTB, '' Confere_ESF
From
(select (b.entradasaidanf - b.dataemissaonf) qtd_dia , b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf 
        from  bgm_notafiscal b, est_movto m
        where b.entradasaidanf > b.dataemissaonf
        and   b.codintnf = m.codintnf
        and   b.codigoempresa = 9 and b.codigofl = 1
        and   m.codigohismov = 14
        and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where NF.qtd_dia > 3)
-----
UNION ALL
-----
(Select distinct '009 - CAMPIBUS' EMPRESA, '05 - Atraso Integracao (CPG)' CLASS, '(Integracao NF - Entrada NF) > 3' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 9 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   (NF.qtd_dia > 3.99))
UNION ALL
-----------
(Select distinct '009 - CAMPIBUS' EMPRESA, '06 - Nao Integradas' CLASS, 'Legado' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.datahoraintegracaocpg Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.statusnf, b.numeronf, b.codtpdoc, b.dataemissaonf, b.entradasaidanf, b.sistemanf ,
        b.lanctointegradocpg, b.lanctointegradoesf, b.lanctointegradoctb, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
        (m.datahoraintegracaocpg - b.entradasaidanf) qtd_dia
        from est_movto m, bgm_notafiscal b
        where b.codintnf = m.codintnf
        and   m.codigohismov = 14
        and   m.codigoempresa = 9 and m.codigofl = 1
        and   B.Entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
        AND   B.STATUSNF <> 'C')NF
Where   NF.lanctointegradocpg = 'N' or NF.lanctointegradoctb = 'N' or NF.lanctointegradoesf = 'N')
--------
UNION ALL
----------
(Select distinct '009 - CAMPIBUS' EMPRESA, '07 - Atraso Conferencia' CLASS, '(Conferencia - Vencimento) <= 1' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(distinct nf.numeronf) total
--        NF.logaltdados Data_Base, NF.entradasaidanf Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, NF.docconciliado Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') logaltdados, b.codtpdoc, c.vencprorrogcpg,
       c.vencprorrogcpg - to_date(SUBSTR(e.logaltdados, 0 , 10),'DD/MM/YYYY') QTD_DIA, c.vencprorrogcpg - sysdate QTD_N, e.docconciliado, m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from esfentra e, bgm_notafiscal b , est_movto m, cpgdocto c
       where e.codigoempresa = 9 and e.codigofl = 1
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   b.coddoctoesf = e.coddoctoesf
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  ((NF.QTD_DIA <= 1) or (NF.DOCCONCILIADO = 'N' and QTD_N <= 1)))
UNION ALL
(Select distinct '009 - CAMPIBUS' EMPRESA, '08 - Atraso Vencimento x Integracao' CLASS, 'Integracao >= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 9 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg >= NF.vencprorrogcpg ))
UNION ALL
(Select distinct '009 - CAMPIBUS' EMPRESA, '09 - Integracao Vencimento' CLASS, 'Notas integradas ate o vencimento' FORMULA, 
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.datahoraintegracaocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg,to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 9 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.datahoraintegracaocpg < NF.vencprorrogcpg ))
----------
UNION ALL
----------
(Select distinct '009 - CAMPIBUS' EMPRESA, '10 - Atraso Pagamento' CLASS, 'Pagamento > Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from  bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 9 and c.codigofl = 1
        and   m.codigohismov = 14
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   c.codmovtobco is not null       
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.QTD_DIA < -2))
-------------
UNION ALL
-------------
(Select distinct '009 - CAMPIBUS' EMPRESA, '11 - Pagamento Correto' CLASS, 'Pagamento <= Vencimento' FORMULA,
        (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2))ref, count(nf.numeronf) total
--        NF.vencprorrogcpg Data_Base, NF.pagamentocpg Data_Entrada, NF.qtd_dia Dif_dia, NF.numeronf NF, NF.codtpdoc Tipo, 
--        NF.lanctointegradocpg Integra_CPG, NF.lanctointegradoesf Integra_ESF, NF.lanctointegradoctb Integra_CTB, '' Confere_ESF
From
(select b.numeronf, b.dataemissaonf, b.entradasaidanf, b.codtpdoc, to_date(SUBSTR(m.datahoraintegracaocpg, 0 , 10),'DD/MM/YYYY') datahoraintegracaocpg,
       c.vencprorrogcpg, c.pagamentocpg, to_date(SUBSTR(c.vencprorrogcpg, 0 , 10),'DD/MM/YYYY') - to_date(SUBSTR(c.pagamentocpg, 0 , 10),'DD/MM/YYYY') QTD_DIA, 
       m.lanctointegradoctb, m.lanctointegradocpg, m.lanctointegradoesf
       from bgm_notafiscal b , est_movto m, cpgdocto c
       where c.codigoempresa = 9 and c.codigofl = 1
       and   b.entradasaidanf BETWEEN (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -2)) and  (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+0, -1))
       and   c.coddoctocpg = b.coddoctocpg
       and   c.codmovtobco is not null       
       and   m.codigohismov = 14
       and   m.codintnf = b.codintnf
       AND   B.STATUSNF <> 'C')NF
WHERE  (NF.pagamentocpg <= NF.vencprorrogcpg))) 
 
 
 
