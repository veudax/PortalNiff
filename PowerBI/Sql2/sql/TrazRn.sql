Select Empfil,
       Data, 
       Sum(TotalRNHorario) TotalRNHorario,
       Sum(TotalRN) TotalRN,
       tipofrota
  From ( Select Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0') Empfil
              , LAST_DAY(m.Dataaberturaos) Data
              , Count(Distinct m.Numeroos) TotalRNHorario
              , 0 TotalRN
              , f.descricaotpfrota tipofrota
           From Man_Os m, frt_cadveiculos v, frt_tipodefrota f
           Where m.Dataaberturaos Between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
             And f.codigotpfrota = v.codigotpfrota
             And v.codigoveic = m.codigoveic
--          And f.codigotpfrota Not In (7,10,52,53)
             And m.Codorigos In (1)
             And To_char(m.horaaberturaos,'hh24:mi') Between '08:00' And '18:00'
             And m.Codigoempresa ||  m.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
           Group By Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0')
               , LAST_DAY(m.Dataaberturaos)
               , f.descricaotpfrota
           Union All
          Select Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0') Empfil
               , LAST_DAY(m.Dataaberturaos) Data
               , 0 TotalRNHorario
               , Count(Distinct m.Numeroos) TotalRN
               , f.descricaotpfrota tipofrota
            From Man_Os m, frt_cadveiculos v, frt_tipodefrota f
           Where m.Dataaberturaos Between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
             And f.codigotpfrota = v.codigotpfrota
             And v.codigoveic = m.codigoveic
--          And f.codigotpfrota Not In (7,10,52,53)
             And m.Codorigos In (1)
             And m.Codigoempresa ||  m.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
           Group By Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0')
               , LAST_DAY(m.Dataaberturaos)
               , f.descricaotpfrota           )
  Group By Empfil,
       Data, tipofrota               