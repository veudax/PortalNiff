Insert Into niff_ads_valoresmetas (id, 
idmetas, 
idEmpresa,
referencia, 
previsto, 
realizado, 
idusuariogerou, 
datagerou, 
idusuarioeditou, 
dataeditou, 
aplicoucontrato, 
motivoedicao, 
previstooriginal, 
realizadooriginal)
Select Rownum
     , IdMetas
     , IdEmpresa
     , Mes
     , valoresperado
     , realizado
     , 8
     , Sysdate
     , Null
     , Null
     , 'S'
     , Null
     , valoresperado
     , realizado
  From (Select Distinct i.Idmetas, SubStr(Lpad(a.mesreferencia,6,'0'),3,4)||substr(Lpad(a.mesreferencia,6,'0'),1,2) mes, i.valoresperado, i.realizado, a.Idempresa
          From niff_ads_avaliacao a
             , niff_ads_itensavaliacaometas i
         Where i.idautoavaliacao = a.Idautoavaliacao  
           And SubStr(Lpad(a.mesreferencia,6,'0'),3,4) = 2017  
        ) 