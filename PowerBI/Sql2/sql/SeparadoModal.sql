Select Distinct tipoFrota, s.EmpFil, s.nomeEmpresa
From pbi_empresas s, (
select x.EmpFil, x.data, sum(x.totalra)totalra, sum(x.totalsos)totalsos, sum(x.totalkm)totalkm, sum(x.consumo) consumo, 'X' Empresa, tipoFrota
   From (Select Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0') Empfil
             , LAST_DAY(m.Dataaberturaos) Data
             , Count(Distinct m.Numeroos) Totalra
             , 0 Totalsos
             , 0 Totalkm
             , 0 Consumo
             , f.descricaotpfrota tipofrota
         From Man_Os m, frt_cadveiculos v, frt_tipodefrota f
        Where m.Dataaberturaos Between '01-aug-2018' and (TO_DATE(TO_CHAR(SYSDATE - 4,'DD/MM/YYYY'),'DD/MM/YYYY'))
          And f.codigotpfrota = v.codigotpfrota
          And v.codigoveic = m.codigoveic
          And f.codigotpfrota Not In (7,10,52,53)
          And m.Codorigos In (2)
          And m.Codigoempresa ||  m.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
        Group By Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0')
              , LAST_DAY(m.Dataaberturaos)
              , f.descricaotpfrota
        union all

       Select Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0') Empfil
            , LAST_DAY(m.Dataaberturaos) Data
            , 0 Totalra
            , Count(Distinct m.Numeroos) Totalsos
            , 0 Totalkm
            , 0 Consumo
            , f.descricaotpfrota tipofrota
         From Man_Os m, frt_cadveiculos v, frt_tipodefrota f
        Where m.Dataaberturaos Between '01-aug-2018' And
              (To_Date(To_Char(Sysdate - 4, 'DD/MM/YYYY'), 'DD/MM/YYYY'))
          And m.Codorigos In (3)
          And f.codigotpfrota = v.codigotpfrota
          And v.codigoveic = m.codigoveic
          And f.codigotpfrota Not In (7,10,52,53)
          And m.Codigoempresa ||  m.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
        Group By Lpad(m.Codigoempresa, 3, '0') || '/' || Lpad(Decode(m.Codigoempresa, 9, Decode(m.Codigofl, 2, 1, m.Codigofl), m.Codigofl), 3, '0')
            , LAST_DAY(m.Dataaberturaos)
            , f.descricaotpfrota
        union all

       Select Lpad(b.Codigoempresa, 3, '0') || '/' || Lpad(Decode(b.Codigoempresa, 9, Decode(b.Codigofl, 2, 1, b.Codigofl), b.Codigofl), 3, '0') Empfil
            , LAST_DAY(b.Dataveloc) Data
            , 0 Totalra
            , 0 Totalsos
            , Sum(b.Kmpercorridoveloc) Totalkm
            , 0 Consumo
            , f.descricaotpfrota tipofrota
         From Bgm_Velocimetro b, frt_cadveiculos v, frt_tipodefrota f
        Where b.Dataveloc Between '01-aug-2018' And
              (To_Date(To_Char(Sysdate - 4, 'DD/MM/YYYY'), 'DD/MM/YYYY'))
          And f.codigotpfrota Not In (7,10,52,53)
          And f.codigotpfrota = v.codigotpfrota
          And v.codigoveic = b.codigoveic
          And b.Codigoempresa ||  b.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
        Group By Lpad(b.Codigoempresa, 3, '0') || '/' || Lpad(Decode(b.Codigoempresa, 9, Decode(b.Codigofl, 2, 1, b.Codigofl), b.Codigofl), 3, '0')
            , LAST_DAY(b.Dataveloc)
            , f.descricaotpfrota
        union all

       Select Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(Decode(e.Codigoempresa, 9, Decode(e.Codigofl, 2, 1, e.Codigofl), e.Codigofl), 3, '0') Empfil
            , Last_day(e.Datamovto) Data
            , 0 Totalra
            , 0 Totalsos
            , 0 Totalkm
            , Sum(i.Valortotalitensmovto) Consumo
            , f.descricaotpfrota tipofrota
         From Est_Movto e, Est_Itensmovto i, Est_Cadmaterial c, est_requisicao r, man_os o, frt_cadveiculos v, frt_tipodefrota f
        Where e.Seqmovto = i.Seqmovto
          And e.Datamovto = i.Datamovto
          And c.codigomatint = i.codigomatint
          
          And r.numerorq(+) = e.numerorq
          And o.codintos(+) = r.codintos
          And f.codigotpfrota Not In (7,10,52,53)
          And f.codigotpfrota = v.codigotpfrota
          And v.codigoveic = o.codigoveic

          And c.Codigogrd In (500, 510)
          And e.Codigohismov In (4, 5, 7, 9, 11, 13, 15, 16, 18, 19, 20, 21, 23)
          And i.Statusitensmovto = 'F'
          And e.Codigoempresa || e.Codigofl In (11,12,21,31,41,51,61,91,131,261,263)
          And e.Datamovto Between '01-aug-2018' And
              (To_Date(To_Char(Sysdate - 4, 'DD/MM/YYYY'), 'DD/MM/YYYY'))
        Group By Lpad(e.Codigoempresa, 3, '0') || '/' || Lpad(Decode(e.Codigoempresa, 9, Decode(e.Codigofl, 2, 1, e.Codigofl), e.Codigofl), 3, '0')
            , Last_day(e.Datamovto)
            , f.descricaotpfrota
      ) x
  group by x.EmpFil, x.Data,  tipofrota
) x
Where s.empFil = x.empfil
Order By empfil