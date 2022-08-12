Select Decode(o.dataaberturaos, Null, u.dataaberturaos, o.dataaberturaos) Data
     , Decode(o.numeroos, Null, u.numeroos, o.numeroos) os, o.codigoveic
     , decode(o.codigointernomaterial, Null, u.codigointernomaterial, o.codigointernomaterial) CodigoMAt
     , o.codintos
     , decode(o.km, Null, u.km, o.km) km
     , o.PecasObrigatorias, u.PecasUtilizadas 
  From pbi_osPecasObrig o
     , pbi_ospecasutilizadas u
 Where o.codintos = u.codintos(+)
   And o.codigomatint = u.codigomatint(+)
   And o.km = u.km(+)
   And o.numeroos = 357197
   And o.km = 15000   

