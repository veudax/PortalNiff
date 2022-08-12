create or replace view pbi_dashconsumo as
Select EmpFil, Sum(Consumo) Consumo, Sum(previsto) previsto, Sum(TotalKm) TotalKm
  From (Select Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(Decode(e.Codigoempresa, 9, Decode(e.Codigofl, 2, 1, e.Codigofl), e.Codigofl), 3, '0') Empfil
             , Round(Sum(i.valortotalitensmovto) /1000,2) Consumo
             , 0 TotalKm
             , 0 Previsto
          From Est_Movto e, Est_Itensmovto i, Est_Cadmaterial c, est_requisicao r, frt_cadveiculos v, frt_tipodefrota f
         Where e.Seqmovto = i.Seqmovto
           And e.Datamovto = i.Datamovto
           And c.codigomatint = i.codigomatint
           And r.numerorq(+) = e.numerorq
           And f.codigotpfrota = v.codigotpfrota
           And v.codigoveic = r.codigoveic
           And c.Codigogrd In (500, 510)
           And e.Codigohismov In (4, 5, 7, 9, 11, 13, 15, 16, 18, 19, 20, 21, 23)
           And i.Statusitensmovto = 'F'
           And e.Codigoempresa || e.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
           And e.Datamovto Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and Sysdate
         Group By Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(Decode(e.Codigoempresa, 9, Decode(e.Codigofl, 2, 1, e.Codigofl), e.Codigofl), 3, '0')
         Union All
        Select Lpad(b.Codigoempresa, 3, '0') || '/' || Lpad(Decode(b.Codigoempresa, 9, Decode(b.Codigofl, 2, 1, b.Codigofl), b.Codigofl), 3, '0') Empfil
             , 0 Consumo
             , (Sum(b.Kmpercorridoveloc) /1000) Totalkm
             , 0 Previsto
          From Bgm_Velocimetro b, frt_cadveiculos v, frt_tipodefrota f
         Where b.Dataveloc Between to_date('01/'|| to_char(Sysdate,'mm/yyyy'),'dd/mm/yyyy') and Sysdate
           And f.codigotpfrota = v.codigotpfrota(+)
           And v.codigoveic = b.codigoveic
           And b.Codigoempresa ||  b.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
         Group By Lpad(b.Codigoempresa, 3, '0') || '/' || Lpad(Decode(b.Codigoempresa, 9, Decode(b.Codigofl, 2, 1, b.Codigofl), b.Codigofl), 3, '0')
         Union All
        Select e.codigoglobus empfil, 0 Consumo, 0 Totalkm, previsto
          From niff_ads_valoresmetas v, niff_chm_empresas e
         Where referencia = to_char(Sysdate,'yyyymm')
           And idmetas = 18
           And v.idempresa = e.Idempresa
) Group By EmpFil

