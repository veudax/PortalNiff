Select * 
  From (Select Count(c.codigoveic) QtdEntrada, 
               To_date('01/' || To_char(decode(n.datasaidanf, null, n.dataemissaonf, n.datasaidanf),'mm/yyyy'),'dd/mm/yyyy') DataCompra
          From frt_cadveiculos v, 
               frt_compraveic c,
               Bgm_Notafiscal n
        Where v.codigoempresa = 1
          And v.condicaoveic <> 'V'
          And c.codigoveic = v.codigoveic
          And n.codintnf = c.codintnf
        Group By To_date('01/' || To_char(decode(n.datasaidanf, null, n.dataemissaonf, n.datasaidanf),'mm/yyyy'),'dd/mm/yyyy') ) a,
       (Select Count(*) qtdSaida, 
               To_date('01/' || To_char(decode(n.datasaidanf, null, n.dataemissaonf, n.datasaidanf),'mm/yyyy'),'dd/mm/yyyy') DataVenda
          From frt_cadveiculos v, 
               frt_vendaveiculos c,
               Bgm_Notafiscal n       
          Where v.codigoempresa = 1
            And v.condicaoveic = 'V'
            And c.codigoveic = v.codigoveic
            And n.codintnf = c.codintnf
         Group By To_date('01/' || To_char(decode(n.datasaidanf, null, n.dataemissaonf, n.datasaidanf),'mm/yyyy'),'dd/mm/yyyy') ) s
-- Where a.DataCompra <= s.DataVenda         