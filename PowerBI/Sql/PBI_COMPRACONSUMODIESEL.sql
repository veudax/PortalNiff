CREATE OR REPLACE VIEW PBI_COMPRACONSUMODIESEL AS
SELECT
  Lpad(E.CODIGOEMPRESA,3, '0') || '/' || Lpad(E.CODIGOFL,3, '0') EmpFil,
  Last_day(E.DATAMOVTO) Data,
  To_Char(Last_day(E.DATAMOVTO),'mm/yyyy') Mesano,
  To_Char(Last_day(E.DATAMOVTO),'yyyymm') AnoMes,
  To_Char(Last_day(E.DATAMOVTO),'yyyy') Ano,
  D.DESCRICAOMAT,
  B.TIPOHISMOV ,
  decode(b.tipohismov, 'EN', Sum(A.QTDEITENSMOVTO), 0) QuantidadeEntrada,
  decode(b.tipohismov, 'SA', Sum(A.QTDEITENSMOVTO), 0) QuantidadeSaida,
  decode(b.tipohismov, 'EN', Sum(A.VALORITENSMOVTO), 0) ValorEntrada,
  decode(b.tipohismov, 'SA', Sum(A.VALORITENSMOVTO), 0) ValorSaida,
  decode(To_Char(Last_day(E.DATAMOVTO),'yyyy'), To_Char(Last_day(Sysdate),'yyyy'),0, decode(b.tipohismov, 'EN', Sum(A.VALORITENSMOVTO), 0)) ValorAnoAnteriorEntrada,
  decode(To_Char(Last_day(E.DATAMOVTO),'yyyy'), To_Char(Last_day(Sysdate),'yyyy'),0, decode(b.tipohismov, 'SA', Sum(A.VALORITENSMOVTO), 0)) ValorAnoAnteriorSaida,
  decode(To_Char(Last_day(E.DATAMOVTO),'yyyy'), To_Char(Last_day(Sysdate),'yyyy'), decode(b.tipohismov, 'EN', Sum(A.VALORITENSMOVTO), 0),0) ValorAnoAtualEntrada,
  decode(To_Char(Last_day(E.DATAMOVTO),'yyyy'), To_Char(Last_day(Sysdate),'yyyy'), decode(b.tipohismov, 'SA', Sum(A.VALORITENSMOVTO), 0),0) ValorAnoAtualSaida
FROM
  EST_ITENSMOVTO     A,
  EST_HISTORICOMOVTO B,
  CTR_CADLOCAL       C,
  EST_CADMATERIAL    D,
  EST_MOVTO          E,
  BGM_NOTAFISCAL     F,
  EST_REQUISICAO     G,
  FRT_CADVEICULOS    H,
  BGM_FORNECEDOR     I,
  CTR_CADEMP         J,
  CTR_EMPAUTORIZADAS K,
  CTR_FILIAL         L,
  CTR_EMPAUTORIZADAS M
WHERE
  J.CODINTEMPAUT       = K.CODINTEMPAUT    AND
  L.CODINTEMPAUT       = M.CODINTEMPAUT    AND
  J.CODIGOEMPRESA      = L.CODIGOEMPRESA   AND
  L.CODIGOFL           = E.CODIGOFL        AND
  J.CODIGOEMPRESA      = E.CODIGOEMPRESA   AND
  F.CODIGOFORN         = I.CODIGOFORN  (+) AND
  E.CODIGOHISMOV       = B.CODIGOHISMOV(+) AND
  E.CODINTNF           = F.CODINTNF    (+) AND
  G.CODIGOVEIC         = H.CODIGOVEIC  (+) AND
  E.NUMERORQ           = G.NUMERORQ    (+) AND
  A.CODIGOLOCAL        = C.CODIGOLOCAL     AND
  A.CODIGOMATINT       = D.CODIGOMATINT    AND
  A.SEQMOVTO           = E.SEQMOVTO        AND
  A.DATAMOVTO          = E.DATAMOVTO       AND
  D.EXIBEEMRELATORIOS  = 'S'               AND
  D.CODIGOGRD          = 520               AND
  A.STATUSITENSMOVTO   = 'F'               AND
  D.CODIGOINTERNOMATERIAL BETWEEN '       !' AND 'ZZZZZZZZ' AND
  A.DATAMOVTO             BETWEEN ADD_MONTHS(trunc(Sysdate,'rr'),-12) And ADD_MONTHS(LAST_DAY(trunc(Sysdate)), 0) And
  e.CodigoEmpresa || e.CodigoFl In (11,12,21,31,41,51,61,91,131,261,263) And
  E.CODIGOHISMOV  In ('14', '15')
Group By   Lpad(E.CODIGOEMPRESA,3, '0') || '/' || Lpad(E.CODIGOFL,3, '0') ,
  Last_day(E.DATAMOVTO),
  D.DESCRICAOMAT,
  B.TIPOHISMOV

