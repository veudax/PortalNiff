create or replace view pbi_tipofrota as
Select k.descricaomodchassi, f.codigotpfrota, f.descricaotpfrota, v.prefixoveic, 1 quantidade
     , Lpad(v.CODIGOEMPRESA, 3,'0') || '/' || Lpad(v.CODIGOFL, 3,'0') EmpFil
     , To_Char(Last_day(A.DATAABASTCARRO),'mm/yyyy') Mesano
     , To_Char(Last_day(A.DATAABASTCARRO),'yyyymm') AnoMes
     , To_Char(Last_day(A.DATAABASTCARRO),'yyyy') Ano

  From Frt_Cadveiculos v, Frt_Modchassi k, ABA_ITEMABASTCARRO a, frt_tipodefrota f
 Where v.Codigomodchassi = k.Codigomodchassi(+)
   And v.CodigoEmpresa || v.CodigoFl In (11,12,21,31,41,51,61,91,131,261,263)
   And v.condicaoveic = 'A'
   And a.codigoveic = v.codigoveic
   And v.codigotpfrota = f.codigotpfrota
   And a.dataabastcarro Between ADD_MONTHS(trunc(Sysdate,'rr'),-12) And ADD_MONTHS(LAST_DAY(trunc(Sysdate)), -1)
   And ((v.CodigoEmpresa || v.CodigoFl = 11 And K.CODIGOMODCHASSI In (10,23,56,117,120,127,146)) Or
        (v.CodigoEmpresa || v.CodigoFl = 12 And K.CODIGOMODCHASSI In (10,56,117,120,151)) Or
        (v.CodigoEmpresa || v.CodigoFl = 21 And K.CODIGOMODCHASSI In (117,120,150)) Or
        (v.CodigoEmpresa || v.CodigoFl = 31 And K.CODIGOMODCHASSI In (10,16,21,117,120,127,146,150,155) And f.codigotpfrota In (1,5,6,8)) Or
        (v.CodigoEmpresa || v.CodigoFl = 41 And K.CODIGOMODCHASSI In (10,21,120) And f.codigotpfrota In (1,50)) Or
        (v.CodigoEmpresa || v.CodigoFl = 61 And K.CODIGOMODCHASSI In (4,120,134,150) And f.codigotpfrota In (2,8,50,51)) Or
        (v.CodigoEmpresa || v.CodigoFl = 91 And K.CODIGOMODCHASSI In (10,23,117,120)) Or
        (v.CodigoEmpresa || v.CodigoFl = 131 And K.CODIGOMODCHASSI In (10,21,117) And f.codigotpfrota In (6,8)) Or
        (v.CodigoEmpresa || v.CodigoFl = 261 And K.CODIGOMODCHASSI In (10,117,120)) Or
        (v.CodigoEmpresa || v.CodigoFl = 263 And K.CODIGOMODCHASSI In (10,117,120)))

