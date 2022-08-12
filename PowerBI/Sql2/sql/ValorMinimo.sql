  Select CODIGOEMPRESA,
         CODIGOFL,
         CODIGOGRD,
         Ano,
         Material, 
         Sum(valorMin) ValorMin,
         Sum(valorMax) ValorMax
  From ( Select * 
           From ( Select Round(Sum(im.valoritensmovto)/Count(*),2) ValorMin,
                       0 ValorMax,
                       L.CODIGOEMPRESA,
                       L.CODIGOFL,
                       TO_CHAR(M.CODIGOGRD  ,'000') CODIGOGRD,
                       To_Char( mv.datamovto,'mm/yyyy') mesAno,
                       To_Char( mv.datamovto,'yyyymm') AnoMes,
                       To_Char( mv.datamovto,'yyyy') Ano,
                       m.codigointernomaterial || ' - ' || m.descricaomat Material
                  From EST_CADMATERIAL    M,
                       CTR_CADLOCAL       L,
                       Est_Itensmovto IM,
                       Est_movto MV,
                       Est_Historicomovto H
                 Where L.CODIGOEMPRESA || L.CODIGOFL In 11--(11,12,21,31,41,51,61,91,131,261,263)
                   And M.CODIGOGRD In (500, 510, 520, 540)
                   And IM.Seqmovto = MV.Seqmovto
                   And IM.Datamovto = Mv.Datamovto
                   And H.Codigohismov = MV.Codigohismov
                   And IM.Codigomatint = m.codigomatint
                   And im.codigolocal = l.codigolocal
                   And mv.codigoempresa = l.codigoempresa
                   And mv.codigofl = l.codigofl
                   And mv.datamovto Between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) And Sysdate
                   And 'EN,EI' Like '%' || h.tipohismov || '%' -- muda conforme tipo entrada, saida e transferencia
                   And m.codigointernomaterial = '01010001'
                 Group By L.CODIGOEMPRESA,
                          L.CODIGOFL,
                          TO_CHAR(M.CODIGOGRD  ,'000') ,
                          To_Char( mv.datamovto,'mm/yyyy'),
                          To_Char( mv.datamovto,'yyyymm'),
                          To_Char( mv.datamovto,'yyyy'),
                          m.codigointernomaterial || ' - ' || m.descricaomat ) x
         Where (CodigoEmpresa, CodigoFl, CodigoGrd, MesAno, Material ) In (Select CODIGOEMPRESA,
                                                                                  CODIGOFL,
                                                                                  CODIGOGRD,
                                                                                  mesAnoMin,
                                                                                  Material
                                                                             From pbi_PosicaoFinancPeriodo  )
         Union All
        Select * 
          From ( Select 0 ValorMin,
                        Round(Sum(im.valoritensmovto)/Count(*),2) valorMax,
                        L.CODIGOEMPRESA,
                        L.CODIGOFL,
                        TO_CHAR(M.CODIGOGRD  ,'000') CODIGOGRD,
                        To_Char( mv.datamovto,'mm/yyyy') mesAno,
                        To_Char( mv.datamovto,'yyyymm') AnoMes,
                        To_Char( mv.datamovto,'yyyy') Ano,
                        m.codigointernomaterial || ' - ' || m.descricaomat Material
                   From EST_CADMATERIAL    M,
                        CTR_CADLOCAL       L,
                        Est_Itensmovto IM,
                        Est_movto MV,
                        Est_Historicomovto H
                  Where L.CODIGOEMPRESA || L.CODIGOFL In 11--(11,12,21,31,41,51,61,91,131,261,263)
                    And M.CODIGOGRD In 540 --(500, 510, 520, 540)
                    And IM.Seqmovto = MV.Seqmovto
                    And IM.Datamovto = Mv.Datamovto
                    And H.Codigohismov = MV.Codigohismov
                    And IM.Codigomatint = m.codigomatint
                    And im.codigolocal = l.codigolocal
                    And mv.codigoempresa = l.codigoempresa
                    And mv.codigofl = l.codigofl
                    And mv.datamovto Between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) And Sysdate
                    And 'EN,EI' Like '%' || h.tipohismov || '%' -- muda conforme tipo entrada, saida e transferencia
--                    And m.codigointernomaterial = '01010001'
                  Group By L.CODIGOEMPRESA,
                           L.CODIGOFL,
                           TO_CHAR(M.CODIGOGRD  ,'000') ,
                           To_Char( mv.datamovto,'mm/yyyy'),
                           To_Char( mv.datamovto,'yyyymm'),
                           To_Char( mv.datamovto,'yyyy'),
                           m.codigointernomaterial || ' - ' || m.descricaomat ) x
         Where (CodigoEmpresa, CodigoFl, CodigoGrd, MesAno, Material ) In (Select CODIGOEMPRESA,
                                                                                  CODIGOFL,
                                                                                  CODIGOGRD,
                                                                                  mesAnoMax,
                                                                                  Material
                                                                             From pbi_PosicaoFinancPeriodo )
       )                                        
   Group By CODIGOEMPRESA,
            CODIGOFL,
            CODIGOGRD,
            Ano,
            Material 
