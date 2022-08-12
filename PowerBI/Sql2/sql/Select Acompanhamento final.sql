  Select To_char(p.Numero) OSPreventiva, To_char(p.Data,'dd/mm/yyyy') DataPreventiva
       , p.Codigoveic, p.km, p.materialobrig, p.pecasobrig
       , Lpad(p.CodigoEmpresa,'3',0) || '/' || Lpad(p.Codigofl, '3',0) EmpFil
       , e.nomeEmpresa, v.prefixoveic
       , p.materialutil, p.pecasutil, To_Char(c.numero) OSCorretiva, To_Char(c.Data,'dd/mm/yyyy') DataCorretiva, c.material, c.pecasutil PecaUtilCor
       , p.qtdobrigatoria, p.qtdutilizada, c.qtdutilizada QtdCorr
    From pbi_niff_AcompOSPrev P
       , pbi_niff_acomposcor C
       , pbi_empresas e
       , frt_cadveiculos v
   Where c.Codigoveic(+) = p.codigoveic  
     And c.codigoempresa(+) = p.codigoempresa
     And c.codigofl(+) = p.codigofl
     And c.codintmaterial(+) = p.codintmaterialutil  
     And e.empFil = Lpad(p.CodigoEmpresa,'3',0) || '/' || Lpad(p.Codigofl, '3',0) 
     And v.codigoveic = p.codigoveic
     And p.CodigoEmpresa = 1
     And p.CodigoFl = 1
     And p.Km = 30000
     And p.numero = 359250
     And c.Data(+) >= p.Data
   Order By p.data, p.Numero, p.materialobrig, p.materialutil     
 
 
 
 
