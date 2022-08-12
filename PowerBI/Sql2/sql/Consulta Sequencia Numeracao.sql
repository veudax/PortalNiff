select Distinct n.Codigoempresa,
       n.Codigofl,
       n.Codtpdoc,
       To_Number(n.Nrdocsaida) Numeronfglobus,
       n.Seriesaida Serieglobus,
       n.Dtemissaosaida Emissaoglobus from esfsaida n
      Where 
n.Dtemissaosaida Between To_Date('01/05/2020', 'dd/mm/yyyy') And
       To_Date('31/05/2020', 'dd/mm/yyyy')
   And Lpad(n.Codigoempresa, 3, '0') || '/' || Lpad(n.Codigofl, 3, '0') In
       ('001/001', '001/004')
Order By numeronfglobus       
       