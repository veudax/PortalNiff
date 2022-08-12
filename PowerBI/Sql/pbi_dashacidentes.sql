create or replace view pbi_dashacidentes as
Select EmpFil
     , Sum(qtd_ocorr) qtd_ocorr
     , 0 previsto --Round(Sum(previsto)/1000,0) previsto
     , Sum(TotalKm) TotalKm
     , Sum(MaxRealizado) MaxRealizado--Round(Sum(MaxRealizado)/1000,0) MaxRealizado
     , 1 MinRealizado-- Round(Sum(MinRealizado)/1000,0) MinRealizado
  From ( select lpad(fu.codigoempresa,3,'0') || '/' || lPad(Decode(fu.codigoempresa,9, decode(fu.codigofl, 2, 1, fu.codigofl),fu.codigofl),3,'0') EmpFil
              , count(fu.nomefunc) qtd_ocorr
              , 0 Previsto
              , 0 TotalKm
              , 0 MaxRealizado
              , 0 MinRealizado
           from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
          where t.dthist BETWEEN to_date('01/'|| to_char(Add_months(Sysdate,0),'mm/yyyy'),'dd/mm/yyyy') and Sysdate
            and f.codocorr = t.codocorr
            and fu.codigoempresa || fu.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
            and f.codocorr In (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m
                                Where m.Idmetas = i.Idmetas
                                  And Upper(m.descricao)  Like '%ACIDENTE%'
                                  And i.tipo = 'O')
            and t.codintfunc = fu.codintfunc
          group by t.dthist, f.codocorr || ' - ' || f.descocorr, fu.codigoempresa, fu.codigofl

          Union All
         Select e.codigoglobus empfil
              , 0 qtd_ocorr, previsto
              , 0 Totalkm
              , 0 MaxRealizado
              , 0 MinRealizado
           From niff_ads_valoresmetas v, niff_chm_empresas e
          Where referencia = to_char(Add_months(Sysdate,0),'yyyymm')
            And idmetas = 9
            And v.idempresa = e.Idempresa

          Union All

         Select Lpad(b.Codigoempresa, 3, '0') || '/' || Lpad(Decode(b.Codigoempresa, 9, Decode(b.Codigofl, 2, 1, b.Codigofl), b.Codigofl), 3, '0') Empfil
              , 0 qtd_ocorr
              , 0 previsto
              , Sum(b.Kmpercorridoveloc) Totalkm
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

         Select EmpFil
              , 0 qtd_ocorr
              , 0 previsto
              , 0 Totalkm
              , Max(qtd_ocorr) MaxRealizado
              , 0 MinRealizado
           From ( Select EmpFil
                       , Sum(qtd_ocorr) qtd_ocorr
                       , Last_day(trunc(dthist)) Data
                    From ( select lpad(fu.codigoempresa,3,'0') || '/' || lPad(Decode(fu.codigoempresa,9, decode(fu.codigofl, 2, 1, fu.codigofl),fu.codigofl),3,'0') EmpFil
                                , count(fu.nomefunc) qtd_ocorr
                                , t.dthist
                             from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
                            where t.dthist BETWEEN  Add_Months(Sysdate, -12) and Sysdate
                              and f.codocorr = t.codocorr
                              and fu.codigoempresa || fu.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
                              and f.codocorr In (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m
                                                  Where m.Idmetas = i.Idmetas
                                                    And Upper(m.descricao)  Like '%ACIDENTE%'
                                                    And i.tipo = 'O')
                              and t.codintfunc = fu.codintfunc
                            group by t.dthist, f.codocorr || ' - ' || f.descocorr, fu.codigoempresa, fu.codigofl )
                   Group By Empfil, Last_day(trunc(dthist)) )
          Group By empFil

          Union All

         Select EmpFil
              , 0 qtd_ocorr
              , 0 previsto
              , 0 Totalkm
              , 0 MaxRealizado
              , Min(qtd_ocorr) MinRealizado
           From ( Select EmpFil
                       , Sum(qtd_ocorr) qtd_ocorr
                       , Last_day(trunc(dthist)) Data
                    From ( select lpad(fu.codigoempresa,3,'0') || '/' || lPad(Decode(fu.codigoempresa,9, decode(fu.codigofl, 2, 1, fu.codigofl),fu.codigofl),3,'0') EmpFil
                                , count(fu.nomefunc) qtd_ocorr
                                , t.dthist
                             from flp_historico t, frq_ocorrencia f, flp_funcionarios fu
                            where t.dthist BETWEEN  Add_Months(Sysdate, -12) and Sysdate
                              and f.codocorr = t.codocorr
                              and fu.codigoempresa || fu.codigofl In (11,12,21,31,41,51,61,92,131,261,263)
                              and f.codocorr In (Select i.Codigo From niff_ads_metasbiitens i, niff_ads_metas m
                                                  Where m.Idmetas = i.Idmetas
                                                    And Upper(m.descricao)  Like '%ACIDENTE%'
                                                    And i.tipo = 'O')
                              and t.codintfunc = fu.codintfunc
                            group by t.dthist, f.codocorr || ' - ' || f.descocorr, fu.codigoempresa, fu.codigofl )
                   Group By Empfil, Last_day(trunc(dthist)) )
          Group By empFil

/* por km
          Union All
         Select e.codigoglobus empfil
              , 0 qtd_ocorr
              , 0 previsto
              , 0 Totalkm
              , Max(Realizado) MaxRealizado
              , 0 MinRealizado
           From niff_ads_valoresmetas v, niff_chm_empresas e
          Where referencia Between To_Char(Add_Months(Sysdate, -12), 'yyyymm') And  To_Char(Add_Months(Sysdate,0), 'yyyymm')
            And idmetas = 9
            And v.idempresa = e.Idempresa
          Group By e.codigoglobus

         Union All
         Select e.codigoglobus empfil
              , 0 qtd_ocorr
              , 0 previsto
              , 0 Totalkm
              , 0 MaxRealizado
              , Min(Realizado) MinRealizado
           From niff_ads_valoresmetas v, niff_chm_empresas e
          Where referencia Between To_Char(Add_Months(Sysdate, -12), 'yyyymm') And  To_Char(Add_Months(Sysdate,-1), 'yyyymm')
            And idmetas = 9
            And v.idempresa = e.Idempresa
          Group By e.codigoglobus
          */
           ) a
 Group By Empfil

