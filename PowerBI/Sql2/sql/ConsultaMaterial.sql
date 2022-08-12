Create Or Replace View pbi_PosicaoFinanceiraEstoque As
Select 
   Abs(Sum(ValorInicial) + Sum(ValorEntrada) - (Sum(ValorSaida) - Sum(ValorSimplesRemessa) + Sum(ValorTransf))) Valor,
   a.CODIGOGRD || '-' || d.descricaogrd GrupoDespesa,
   Lpad(CODIGOEMPRESA,3,'0') || '/' || Lpad(CODIGOFL,3,'0') EmpFil,
   To_Char(datamovto,'mm/yyyy') mesAno,
   Sum(ValorInicial) ValorInicial 
From  est_grupodespesas d, (
Select  
   Sum(ValorEntrada) ValorEntrada,
   Sum(ValorSaida) ValorSaida,
   Sum(ValorTransf) ValorTransf,
   Sum(ValorSimplesRemessa) ValorSimplesRemessa, 
   Sum(ValorInicial) ValorInicial,
   Sum(ValorEntrada) - (Sum(ValorSaida) - Sum(ValorSimplesRemessa) + Sum(ValorTransf)) Valor,
   CODIGOEMPRESA, CODIGOGRD, 
   CODIGOFL,     
   datamovto
 From (
    Select Sum(valorInicial) ValorInicial,
           0 ValorEntrada,
           0 ValorSaida,
           0 ValorTransf,
           0 ValorSimplesRemessa,
           CODIGOEMPRESA,
           CODIGOFL,
           CODIGOGRD,
           Data dataMovto
      From (
    Select 
          NVL(FC_ESTTRAZSALDOITEM(m.CODIGOINTERNOMATERIAL,l.CODIGOLOCAL,1,p.Data-1,'I','V'),0) * 
          NVL(FC_ESTTRAZSALDOITEM(m.CODIGOINTERNOMATERIAL,l.CODIGOLOCAL,1,p.Data-1,'I','Q'),0) ValorInicial,
          L.CODIGOEMPRESA,
          L.CODIGOFL,
          TO_CHAR(M.CODIGOGRD  ,'000') CODIGOGRD,
          p.Data  
        FROM
          EST_ITENSDEESTOQUE I,
          CTR_CADLOCAL       L,
          (Select Distinct im.codigomatint, m.descricaomat, m.codigointernomaterial, M.CODIGOGRD
             From Est_Itensmovto IM,
                  Est_movto MV, 
                  EST_CADMATERIAL M
            Where IM.Seqmovto = MV.Seqmovto And
                  IM.Datamovto = Mv.Datamovto And 
                  m.codigomatint = im.codigomatint And
                  mv.datamovto Between ADD_MONTHS(Trunc(Sysdate,'rr'), -24) And Sysdate ) M,
          (Select Data From Dim_Ctr_Periodo
            Where Data Between ADD_MONTHS(Trunc(Sysdate,'rr'), -8) And Sysdate
              And To_char(Data, 'dd') = '26') P
        WHERE
          L.CODIGOLOCAL           = I.CODIGOLOCAL   AND
          I.CODIGOMATINT          = M.CODIGOMATINT  And
          L.CODIGOEMPRESA || L.CODIGOFL In (11,12,21,31,41,51,61,91,131,261,263) And
          M.CODIGOGRD BETWEEN 500 AND 510  )
    Group By        
           CODIGOEMPRESA,
           CODIGOFL,
           CODIGOGRD,
           Data  
    Having Sum(ValorInicial) > 0      
    Union All
    Select
      0 ValorInicial,
      nvl(Sum(im.Valortotalitensmovto),0) ValorEntrada,
      0 ValorSaida,
      0 ValorTransf,
      0 ValorSimplesRemessa,
      L.CODIGOEMPRESA,
      L.CODIGOFL,
      TO_CHAR(M.CODIGOGRD  ,'000') CODIGOGRD,
      mv.datamovto      
    FROM
      EST_ITENSDEESTOQUE I,
      EST_CADMATERIAL    M,
      CTR_CADLOCAL       L,
      CTR_CADEMP         C,
      CTR_EMPAUTORIZADAS E,
      CTR_FILIAL         CL,
      CTR_EMPAUTORIZADAS EL,
      
      Est_Itensmovto IM,
      Est_movto MV,
      Est_Historicomovto H
      
    WHERE
      EL.CODINTEMPAUT         = CL.CODINTEMPAUT AND
      E.CODINTEMPAUT          = C.CODINTEMPAUT  AND
      CL.CODIGOFL             = L.CODIGOFL      AND
      CL.CODIGOEMPRESA        = L.CODIGOEMPRESA AND
      C.CODIGOEMPRESA         = L.CODIGOEMPRESA AND
      L.CODIGOLOCAL           = I.CODIGOLOCAL   AND
      I.CODIGOMATINT          = M.CODIGOMATINT  AND
      L.CODIGOEMPRESA || L.CODIGOFL In (11,12,21,31,41,51,61,91,131,261,263) And
      M.CODIGOGRD BETWEEN 500 AND 510  And
      
      IM.Seqmovto = MV.Seqmovto And
      IM.Datamovto = Mv.Datamovto And
      H.Codigohismov = MV.Codigohismov And
      IM.Codigomatint = m.codigomatint And
      im.codigolocal = l.codigolocal And
      mv.codigoempresa = l.codigoempresa And
      mv.codigofl = l.codigofl And
      mv.datamovto Between ADD_MONTHS(Trunc(Sysdate,'rr'), -8) And Sysdate And
      To_char(mv.Datamovto,'dd') = '26' And
      'EN,EV,EI' Like '%' || h.tipohismov || '%' -- muda conforme tipo entrada, saida e transferencia 
  Group By       I.CODIGOMATINT,
      L.CODIGOEMPRESA,
      L.CODIGOFL,
      TO_CHAR(M.CODIGOGRD  ,'000') ,
      mv.datamovto      
Union All
    Select
      0 ValorInicial,
      0 ValorEntrada,
      0 ValorSaida,
      nvl(Sum(im.Valortotalitensmovto),0) ValorTransf,
      0 ValorSimplesRemessa,
      L.CODIGOEMPRESA,
      L.CODIGOFL,
      TO_CHAR(M.CODIGOGRD  ,'000') CODIGOGRD,
      mv.datamovto      
    FROM
      EST_ITENSDEESTOQUE I,
      EST_CADMATERIAL    M,
      CTR_CADLOCAL       L,
      CTR_CADEMP         C,
      CTR_EMPAUTORIZADAS E,
      CTR_FILIAL         CL,
      CTR_EMPAUTORIZADAS EL,
      
      Est_Itensmovto IM,
      Est_movto MV,
      Est_Historicomovto H
      
    WHERE
      EL.CODINTEMPAUT         = CL.CODINTEMPAUT AND
      E.CODINTEMPAUT          = C.CODINTEMPAUT  AND
      CL.CODIGOFL             = L.CODIGOFL      AND
      CL.CODIGOEMPRESA        = L.CODIGOEMPRESA AND
      C.CODIGOEMPRESA         = L.CODIGOEMPRESA AND
      L.CODIGOLOCAL           = I.CODIGOLOCAL   AND
      I.CODIGOMATINT          = M.CODIGOMATINT  AND
      L.CODIGOEMPRESA || L.CODIGOFL In (11,12,21,31,41,51,61,91,131,261,263) And
      M.CODIGOGRD BETWEEN 500 AND 510  And
      
      IM.Seqmovto = MV.Seqmovto And
      IM.Datamovto = Mv.Datamovto And
      H.Codigohismov = MV.Codigohismov And
      IM.Codigomatint = m.codigomatint And
      im.codigolocal = l.codigolocal And
      mv.codigoempresa = l.codigoempresa And
      mv.codigofl = l.codigofl And
      mv.datamovto Between ADD_MONTHS(Trunc(Sysdate,'rr'), -8) And Sysdate And
      To_char(mv.Datamovto,'dd') = '26' And
      'DS,TE' Like '%' || h.tipohismov || '%' -- muda conforme tipo entrada, saida e transferencia 
  Group By       
      L.CODIGOEMPRESA,
      L.CODIGOFL,
      TO_CHAR(M.CODIGOGRD  ,'000') ,
      mv.datamovto  
Union All
    Select
      0 ValorInicial,
      0 ValorEntrada,
      nvl(Sum(im.Valortotalitensmovto),0) ValorSaida,
      0 ValorTransf,
      0 ValorSimplesRemessa,
      L.CODIGOEMPRESA,
      L.CODIGOFL,
      TO_CHAR(M.CODIGOGRD  ,'000') CODIGOGRD,
      mv.datamovto      
    FROM
      EST_ITENSDEESTOQUE I,
      EST_CADMATERIAL    M,
      CTR_CADLOCAL       L,
      CTR_CADEMP         C,
      CTR_EMPAUTORIZADAS E,
      CTR_FILIAL         CL,
      CTR_EMPAUTORIZADAS EL,
      
      Est_Itensmovto IM,
      Est_movto MV,
      Est_Historicomovto H
      
    WHERE
      EL.CODINTEMPAUT         = CL.CODINTEMPAUT AND
      E.CODINTEMPAUT          = C.CODINTEMPAUT  AND
      CL.CODIGOFL             = L.CODIGOFL      AND
      CL.CODIGOEMPRESA        = L.CODIGOEMPRESA AND
      C.CODIGOEMPRESA         = L.CODIGOEMPRESA AND
      L.CODIGOLOCAL           = I.CODIGOLOCAL   AND
      I.CODIGOMATINT          = M.CODIGOMATINT  AND
      L.CODIGOEMPRESA || L.CODIGOFL In (11,12,21,31,41,51,61,91,131,261,263) And
      M.CODIGOGRD BETWEEN 500 AND 510  And
      
      IM.Seqmovto = MV.Seqmovto And
      IM.Datamovto = Mv.Datamovto And
      H.Codigohismov = MV.Codigohismov And
      IM.Codigomatint = m.codigomatint And
      im.codigolocal = l.codigolocal And
      mv.codigoempresa = l.codigoempresa And
      mv.codigofl = l.codigofl And
      mv.datamovto Between ADD_MONTHS(Trunc(Sysdate,'rr'), -8) And Sysdate And
      To_char(mv.Datamovto,'dd') = '26' And
      'DE,TS,SA,SV,SI,RA' Like '%' || h.tipohismov || '%' -- muda conforme tipo entrada, saida e transferencia 
  Group By      
      L.CODIGOEMPRESA,
      L.CODIGOFL,
      TO_CHAR(M.CODIGOGRD  ,'000') ,
      mv.datamovto          
Union All
    Select
      0 ValorInicial,
      0 ValorEntrada,
      0 ValorSaida,
      0 ValorTransf,
      nvl(Sum(im.Valortotalitensmovto),0) ValorSimplesRemessa,
      L.CODIGOEMPRESA,
      L.CODIGOFL,
      TO_CHAR(M.CODIGOGRD  ,'000') CODIGOGRD,
      mv.datamovto     
    FROM
      EST_ITENSDEESTOQUE I,
      EST_CADMATERIAL    M,
      CTR_CADLOCAL       L,
      CTR_CADEMP         C,
      CTR_EMPAUTORIZADAS E,
      CTR_FILIAL         CL,
      CTR_EMPAUTORIZADAS EL,
      
      Est_Itensmovto IM,
      Est_movto MV,
      Est_Historicomovto H,
      Bgm_Notafiscal N,
      Est_Itensnf NI
      
    WHERE
      EL.CODINTEMPAUT         = CL.CODINTEMPAUT AND
      E.CODINTEMPAUT          = C.CODINTEMPAUT  AND
      CL.CODIGOFL             = L.CODIGOFL      AND
      CL.CODIGOEMPRESA        = L.CODIGOEMPRESA AND
      C.CODIGOEMPRESA         = L.CODIGOEMPRESA AND
      L.CODIGOLOCAL           = I.CODIGOLOCAL   AND
      I.CODIGOMATINT          = M.CODIGOMATINT  AND
      L.CODIGOEMPRESA || L.CODIGOFL In (11,12,21,31,41,51,61,91,131,261,263) And
      M.CODIGOGRD BETWEEN 500 AND 510  And
      
      IM.Seqmovto = MV.Seqmovto And
      IM.Datamovto = Mv.Datamovto And
      H.Codigohismov = MV.Codigohismov And
      IM.Codigomatint = m.codigomatint And
      im.codigolocal = l.codigolocal And
      mv.codigoempresa = l.codigoempresa And
      mv.codigofl = l.codigofl And
      mv.datamovto Between ADD_MONTHS(Trunc(Sysdate,'rr'), -8) And Sysdate And
      To_char(mv.Datamovto,'dd') = '26' And
      h.tipohismov = 'SA' And
      n.codintnf = mv.codintnf And
      n.codintnf = ni.codintnf And
      n.tipooperacaonf = 'S'
      
  Group By       
      L.CODIGOEMPRESA,
      L.CODIGOFL,
      TO_CHAR(M.CODIGOGRD  ,'000') ,
      mv.datamovto                )
 Group By    CODIGOEMPRESA,
   CODIGOFL,CODIGOGRD,
   datamovto
      ) a
 Where d.codigogrd = a.codigogrd
      Group By    CODIGOEMPRESA, a.CODIGOGRD, d.descricaogrd,
   CODIGOFL,
   datamovto
