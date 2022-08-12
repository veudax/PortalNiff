Select codigointernomaterial, mesano , prefixoveic,
       Decode(maiorKm, menorKm, menorkm, 
       (maiorKm - MenorKm) / decode(qd , 1, 1, decode(qd, 2, 1, qd))) MediaKm,
       maiorkm, menorkm, qd
From (
Select m.codigointernomaterial, Count(*)qd, min(o.kmexecucaoos) menorKm, Max(o.Kmexecucaoos) maiorKm, 
To_char(e.datarq,'mm/yyyy') mesano, v.prefixoveic

  From man_OS o,
       est_requisicao e,
       Est_Cadmaterial m,
       est_itensrequisicao i,
       frt_cadveiculos v

Where e.codintos = o.codintos
  And e.codigoempresa = 6
  And e.datarq Between Trunc(SYSDATE,'rr') And Sysdate
  And i.codigomatint = m.codigomatint 
  And i.numerorq = e.numerorq
  And e.codigoveic = v.codigoveic
  And m.codigomatint = 247
And v.Codigoveic  = 2001
Group By m.codigointernomaterial, To_char(e.datarq,'mm/yyyy') , v.prefixoveic 
)
