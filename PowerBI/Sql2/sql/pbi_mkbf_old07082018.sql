create or replace view pbi_mkbf as
select x.EmpFil, x.data, sum(x.totalra)totalra, sum(x.totalsos)totalsos, sum(x.totalkm)totalkm, sum(x.consumo) consumo, 'X' Empresa
   From (Select Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0') Empfil
             , LAST_DAY(m.Dataaberturaos) Data
             , Count(Distinct m.Numeroos) Totalra
             , 0 Totalsos
             , 0 Totalkm
             , 0 Consumo
         From Man_Os m
        Where m.Dataaberturaos Between ADD_MONTHS(Trunc(Sysdate,'rr'), -36) and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
          And m.Codorigos In (2)
          And m.Codigoempresa ||  m.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
        Group By Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0')
              , LAST_DAY(m.Dataaberturaos)

        union all

       Select Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0') Empfil
            , LAST_DAY(m.Dataaberturaos) Data
            , 0 Totalra
            , Count(Distinct m.Numeroos) Totalsos
            , 0 Totalkm
            , 0 Consumo
         From Man_Os m
        Where m.Dataaberturaos Between Add_Months(Trunc(Sysdate, 'rr'), -36) And
              (To_Date(To_Char(Sysdate - 4, 'DD/MM/YYYY'), 'DD/MM/YYYY'))
          And m.Codorigos In (3)
          And m.Codigoempresa ||  m.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
        Group By Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0')
            , LAST_DAY(m.Dataaberturaos)

        union all

       Select Lpad(b.Codigoempresa, 3, '0') || '/' || Lpad(Decode(b.Codigoempresa, 9, Decode(b.Codigofl, 2, 1, b.Codigofl), b.Codigofl), 3, '0') Empfil
            , LAST_DAY(b.Dataveloc) Data
            , 0 Totalra
            , 0 Totalsos
            , Sum(b.Kmpercorridoveloc) Totalkm
            , 0 Consumo
         From Bgm_Velocimetro b
        Where b.Dataveloc Between Add_Months(Trunc(Sysdate, 'rr'), -36) And
              (To_Date(To_Char(Sysdate - 4, 'DD/MM/YYYY'), 'DD/MM/YYYY'))
          And b.Codigoempresa ||  b.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
        Group By Lpad(b.Codigoempresa, 3, '0') || '/' || Lpad(Decode(b.Codigoempresa, 9, Decode(b.Codigofl, 2, 1, b.Codigofl), b.Codigofl), 3, '0')
            , LAST_DAY(b.Dataveloc)

        union all

       Select Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(Decode(e.Codigoempresa, 9, Decode(e.Codigofl, 2, 1, e.Codigofl), e.Codigofl), 3, '0') Empfil
            , e.Datamovto Data
            , 0 Totalra
            , 0 Totalsos
            , 0 Totalkm
            , Sum(i.Valortotalitensmovto) Consumo
         From Est_Movto e, Est_Itensmovto i, Est_Cadmaterial c
        Where e.Seqmovto = i.Seqmovto
          And i.Codigomatint = c.Codigomatint
          And e.Datamovto = i.Datamovto
          And c.Codigogrd In (500, 510)
          And e.Codigohismov In (4, 5, 7, 9, 11, 13, 15, 16, 18, 19, 20, 21, 23)
          And i.Statusitensmovto = 'F'
          And e.Codigoempresa || e.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
          And e.Datamovto Between Add_Months(Trunc(Sysdate, 'rr'), -36) And
              (To_Date(To_Char(Sysdate - 4, 'DD/MM/YYYY'), 'DD/MM/YYYY'))
        Group By Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(Decode(e.Codigoempresa, 9, Decode(e.Codigofl, 2, 1, e.Codigofl), e.Codigofl), 3, '0')
            , e.Datamovto
      ) x
  group by x.EmpFil, x.Data

