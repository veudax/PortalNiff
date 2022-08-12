CREATE OR REPLACE VIEW PBI_CONSULTAABASTECIMENTOS AS
Select EmpFil,
       PrefixoVeic,
       DESCRICAOMODCHASSI,
       Mesano,
       AnoMes,
       Ano,
       DESCRICAOTPFROTA,
       CODIGOMODCHASSI,
       codigotpfrota,
       Sum(QTDE) Qtde ,
       Sum(VALOR) Valor ,
       Sum(Km)Km,
       1 Quantidade
 From (
SELECT Lpad(L.CODIGOEMPRESA, 3,'0') || '/' || Lpad(L.CODIGOFL, 3,'0') EmpFil,
       B.PREFIXOVEIC,
       K.DESCRICAOMODCHASSI,
       l.codigoEmpresa,
       l.codigoFl,
       To_Char(Last_day(A.DATAABASTCARRO),'mm/yyyy') Mesano,
       To_Char(Last_day(A.DATAABASTCARRO),'yyyymm') AnoMes,
       To_Char(Last_day(A.DATAABASTCARRO),'yyyy') Ano,
       Sum(A.QTDEITEMABASTCARRO - NVL(C.QTDEDEVOLVIDA,0)) Qtde,
       Sum(A.VALORITEMABASTCARRO - NVL(C.VLRDEVOLVIDO,0)) valor,
       0 Km,
       J.DESCRICAOTPFROTA,
       K.CODIGOMODCHASSI,
       j.codigotpfrota
  FROM ABA_ITEMABASTCARRO     A,
       FRT_CADVEICULOS        B,
       ABA_DEVOLUCAOABAST     C,
       ABA_CADTIPOOLEOCOMBO   D,
       BGM_PARVELOCCATRA      G,
       FRT_TIPODEFROTA        J,
       FRT_MODCHASSI          K,
       ABA_ABASTECIMENTOCARRO L,
       EST_OUTRASENTRADAS_    O

 WHERE A.CODIGOVEIC              = C.CODIGOVEIC(+)           AND
       A.DATAABASTCARRO          = C.DATAABASTCARRO(+)       AND
       A.TIPOABASTCARRO          = C.TIPOABASTCARRO(+)       AND
       A.SEQUENCIAABASTCARRO     = C.SEQUENCIAABASTCARRO(+)  AND
       A.SEQITEMABASTCARRO       = C.SEQITEMABASTCARRO(+)    AND
       A.CODIGOVEIC              = L.CODIGOVEIC              AND
       A.DATAABASTCARRO          = L.DATAABASTCARRO          AND
       A.TIPOABASTCARRO          = L.TIPOABASTCARRO          AND
       A.SEQUENCIAABASTCARRO     = L.SEQUENCIAABASTCARRO     AND
       A.CODIGOVEIC              = B.CODIGOVEIC              AND
       A.CODIGOTPOLEO            = D.CODIGOTPOLEO(+)         AND
       B.APRESENTARELATORIOVEIC  = 'S'                     AND
       (A.QTDEITEMABASTCARRO > 0 OR A.QTDETROLEOABASTCARRO > 0 OR A.QUANTIDADE_ARLA32 > 0 ) AND
       A.SEQUENCIAABASTCARRO     = G.SEQUENCIAPARAM(+)       AND
       A.NROUTRENT               = O.NROUTRENT(+)            AND
       B.CODIGOTPFROTA           = J.CODIGOTPFROTA(+)        AND
       B.CODIGOMODCHASSI         = K.CODIGOMODCHASSI(+)      AND
       B.CodigoEmpresa || B.CodigoFl In (11,12,21,31,41,51,61,91,131,261,263) And
       A.DATAABASTCARRO BETWEEN ADD_MONTHS(trunc(Sysdate,'rr'),-12) And ADD_MONTHS(LAST_DAY(trunc(Sysdate)), 0) And
       B.UTILIZAHORIMETROVEIC = 'N' And
       ((B.CodigoEmpresa || B.CodigoFl = 11 And K.CODIGOMODCHASSI In (10,23,56,117,120,127,146,150) And j.codigotpfrota <> 52) Or
        (B.CodigoEmpresa || B.CodigoFl = 12 And K.CODIGOMODCHASSI In (10,56,117,120,150,151) And j.codigotpfrota <> 52) Or
        (B.CodigoEmpresa || B.CodigoFl = 21 And K.CODIGOMODCHASSI In (117,120,150) And j.codigotpfrota <> 52) Or
        (B.CodigoEmpresa || B.CodigoFl = 31 And K.CODIGOMODCHASSI In (4,10,16,21,117,120,127,145,146,150,155) And j.codigotpfrota In (1,5,6,8)) Or
        (B.CodigoEmpresa || B.CodigoFl = 41 And K.CODIGOMODCHASSI In (10,21,120,150) And j.codigotpfrota In (1,50)) Or
        (B.CodigoEmpresa || B.CodigoFl = 61 And K.CODIGOMODCHASSI In (4,120,134,150) And j.codigotpfrota In (2,8,50,51)) Or
        (B.CodigoEmpresa || B.CodigoFl = 91 And K.CODIGOMODCHASSI In (10,23,117,120,150) And j.codigotpfrota <> 52) Or
        (B.CodigoEmpresa || B.CodigoFl = 131 And K.CODIGOMODCHASSI In (10,21,117,150) And j.codigotpfrota In (6,8)) Or
        (B.CodigoEmpresa || B.CodigoFl = 261 And K.CODIGOMODCHASSI In (10,117,120,150) And j.codigotpfrota <> 52) Or
        (B.CodigoEmpresa || B.CodigoFl = 263 And K.CODIGOMODCHASSI In (10,117,120,150) And j.codigotpfrota <> 52 ))
     And D.TIPOTPOLEO = 'CO'
Group By Lpad(L.CODIGOEMPRESA, 3,'0') || '/' || Lpad(L.CODIGOFL, 3,'0') ,
       B.PREFIXOVEIC,
       Last_day(A.DATAABASTCARRO) ,
       J.DESCRICAOTPFROTA,
       K.CODIGOMODCHASSI,
       K.DESCRICAOMODCHASSI, l.codigoempresa, l.codigofl,
       j.codigotpfrota
Union All

SELECT Lpad(b.CODIGOEMPRESA, 3,'0') || '/' || Lpad(b.CODIGOFL, 3,'0') EmpFil,
       B.PREFIXOVEIC,
       K.DESCRICAOMODCHASSI,
       b.codigoEmpresa,
       b.codigoFl,
       To_Char(Last_day(v.dataveloc),'mm/yyyy') Mesano,
       To_Char(Last_day(v.dataveloc),'yyyymm') AnoMes,
       To_Char(Last_day(v.dataveloc),'yyyy') Ano,
       0 QTDE ,
       0 VALOR ,
       Sum(v.kmpercorridoveloc)  Km,
       J.DESCRICAOTPFROTA,
       K.CODIGOMODCHASSI,
       j.codigotpfrota
  FROM FRT_CADVEICULOS        B,
       FRT_TIPODEFROTA        J,
       FRT_MODCHASSI          K,
       Bgm_Velocimetro        V
 WHERE B.APRESENTARELATORIOVEIC  = 'S'                     AND
       B.CODIGOTPFROTA           = J.CODIGOTPFROTA(+)        AND
       B.CODIGOMODCHASSI         = K.CODIGOMODCHASSI(+)      AND
       B.CodigoEmpresa || B.CodigoFl In (11,12,21,31,41,51,61,91,131,261,263) And
  -- and v.dataveloc BETWEEN '01-apr-2019' And '30-apr-2019' --
v.dataveloc BETWEEN ADD_MONTHS(trunc(Sysdate,'rr'),-12) And ADD_MONTHS(LAST_DAY(trunc(Sysdate)), 0)
And
       B.UTILIZAHORIMETROVEIC = 'N' And
       v.codigoveic = b.codigoveic And
       ((B.CodigoEmpresa || B.CodigoFl = 11 And K.CODIGOMODCHASSI In (10,23,56,117,120,127,146,150) And j.codigotpfrota <> 52) Or
        (B.CodigoEmpresa || B.CodigoFl = 12 And K.CODIGOMODCHASSI In (10,56,117,120,150,151) And j.codigotpfrota <> 52) Or
        (B.CodigoEmpresa || B.CodigoFl = 21 And K.CODIGOMODCHASSI In (117,120,150) And j.codigotpfrota <> 52) Or
        (B.CodigoEmpresa || B.CodigoFl = 31 And K.CODIGOMODCHASSI In (4,10,16,21,117,120,127,145,146,150,155) And j.codigotpfrota In (1,5,6,8)) Or
        (B.CodigoEmpresa || B.CodigoFl = 41 And K.CODIGOMODCHASSI In (10,21,120,150) And j.codigotpfrota In (1,50)) Or
        (B.CodigoEmpresa || B.CodigoFl = 61 And K.CODIGOMODCHASSI In (4,120,134,150) And j.codigotpfrota In (2,8,50,51)) Or
        (B.CodigoEmpresa || B.CodigoFl = 91 And K.CODIGOMODCHASSI In (10,23,117,120,150) And j.codigotpfrota <> 52) Or
        (B.CodigoEmpresa || B.CodigoFl = 131 And K.CODIGOMODCHASSI In (10,21,117,150) And j.codigotpfrota In (6,8)) Or
        (B.CodigoEmpresa || B.CodigoFl = 261 And K.CODIGOMODCHASSI In (10,117,120,150) And j.codigotpfrota <> 52) Or
        (B.CodigoEmpresa || B.CodigoFl = 263 And K.CODIGOMODCHASSI In (10,117,120,150) And j.codigotpfrota <> 52 ))
--And  b.Codigoveic = 1195
Group By Lpad(b.CODIGOEMPRESA, 3,'0') || '/' || Lpad(b.CODIGOFL, 3,'0') ,
       B.PREFIXOVEIC,
       b.codigoempresa, b.codigofl,
       To_Char(Last_day(v.dataveloc),'mm/yyyy'),
       To_Char(Last_day(v.dataveloc),'yyyymm'),
       To_Char(Last_day(v.dataveloc),'yyyy'),
      K.CODIGOMODCHASSI,
       K.DESCRICAOMODCHASSI,
       J.DESCRICAOTPFROTA,
       j.codigotpfrota
) Group By
EmpFil,
       PrefixoVeic,
       DESCRICAOMODCHASSI,
       Mesano,
       AnoMes,
       Ano,
       DESCRICAOTPFROTA,
       CODIGOMODCHASSI,
       codigotpfrota

