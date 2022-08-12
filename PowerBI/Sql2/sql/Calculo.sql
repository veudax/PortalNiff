Select o.Kmexecucaoos
  From man_OS o,
       est_requisicao e,
       Est_Cadmaterial m,
       est_itensrequisicao i,
       frt_cadveiculos v
 Where e.codintos = o.codintos
   And e.codigoempresa = 6

                      And e.codigofl = 1
                         And v.codigoveic = 1992
   And m.codigomatint = 247
   And i.codigomatint = m.codigomatint 
   And i.numerorq = e.numerorq
   And e.codigoveic = v.codigoveic
   And e.datarq = ( Select Max(e.datarq)
                      From man_OS o,
                           est_requisicao e,
                           Est_Cadmaterial m,
                           est_itensrequisicao i,
                           frt_cadveiculos v
                      Where e.codintos = o.codintos
                      And e.codigoempresa = 6
                      And e.codigofl = 1
                       And To_char(e.datarq,'yyyymm') < '201703'
   And v.codigoveic = 1992
   And m.codigomatint = 247
                       And i.codigomatint = m.codigomatint 
                       And i.numerorq = e.numerorq
                       And e.codigoveic = v.codigoveic )
                           