create or replace view pbi_dashmkbf as
(
select EmpFil
     , sum(TotalRASOS)TotalRASOS
     , sum(totalkm)totalkm
     , Round(Sum(previsto)/1000,1) previsto
     , Round(Sum(MaxRealizado)/1000,1) MaxRealizado
     , Round(Sum(MinRealizado)/1000,1) MinRealizado
  From ( Select Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0') Empfil
              , Count(Distinct m.Numeroos) TotalRASOS
              , 0 Totalkm
              , 0 previsto
              , 0 MaxRealizado
              , 0 MinRealizado
           From Man_Os m, frt_cadveiculos v, frt_tipodefrota f
          Where m.Dataaberturaos Between to_date('01/'|| to_char(Add_months(Sysdate,0),'mm/yyyy'),'dd/mm/yyyy') and Sysdate
            And f.codigotpfrota = v.codigotpfrota
            And v.codigoveic = m.codigoveic
            And m.Codorigos In (2,3)
            And m.Codigoempresa ||  m.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
          Group By Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0')
              , LAST_DAY(m.Dataaberturaos)

          Union All
         Select Lpad(b.Codigoempresa, 3, '0') || '/' || Lpad(Decode(b.Codigoempresa, 9, Decode(b.Codigofl, 2, 1, b.Codigofl), b.Codigofl), 3, '0') Empfil
              , 0 Totalra
              , Sum(b.Kmpercorridoveloc) Totalkm
              , 0 previsto
              , 0 MaxRealizado
              , 0 MinRealizado
           From Bgm_Velocimetro b, frt_cadveiculos v, frt_tipodefrota f
          Where b.Dataveloc Between to_date('01/'|| to_char(Add_months(Sysdate,0),'mm/yyyy'),'dd/mm/yyyy') and Sysdate
            And f.codigotpfrota = v.codigotpfrota(+)
            And v.codigoveic = b.codigoveic
            And b.Codigoempresa ||  b.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
          Group By Lpad(b.Codigoempresa, 3, '0') || '/' || Lpad(Decode(b.Codigoempresa, 9, Decode(b.Codigofl, 2, 1, b.Codigofl), b.Codigofl), 3, '0')
              , LAST_DAY(b.Dataveloc)

          Union All
         Select e.codigoglobus empfil
              , 0 TotalRASOS
              , 0 Totalkm
              , previsto
              , 0 MaxRealizado
              , 0 MinRealizado
           From niff_ads_valoresmetas v, niff_chm_empresas e
          Where referencia = to_char(Add_months(Sysdate,0),'yyyymm')
            And idmetas = 6
            And v.idempresa = e.Idempresa

          Union All
         Select e.codigoglobus empfil
              , 0 TotalRASOS
              , 0 Totalkm
              , 0 previsto
              , Max(Realizado) MaxRealizado
              , 0 MinRealizado
           From niff_ads_valoresmetas v, niff_chm_empresas e
          Where referencia Between To_Char(Add_Months(Sysdate, -12), 'yyyymm') And  To_Char(Add_Months(Sysdate,-1), 'yyyymm')
            And idmetas = 6
            And v.idempresa = e.Idempresa
          Group By e.codigoglobus

          Union All
         Select e.codigoglobus empfil
              , 0 TotalRASOS
              , 0 Totalkm
              , 0 previsto
              , 0 MaxRealizado
              , Min(Realizado) MinRealizado
           From niff_ads_valoresmetas v, niff_chm_empresas e
          Where referencia Between To_Char(Add_Months(Sysdate, -12), 'yyyymm') And  To_Char(Add_Months(Sysdate,-1), 'yyyymm')
            And idmetas = 6
            And v.idempresa = e.Idempresa
            And v.Realizado > 0
          Group By e.codigoglobus
) Group By EmpFil
)

