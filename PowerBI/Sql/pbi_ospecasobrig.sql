create or replace view pbi_ospecasobrig as
Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.kmexecucaoos, o.codigoveic, 5000 km,
       Null PecasObrigatorias,  Null codigomatint, Null codigointernomaterial,
       Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
       o.codigoEmpresa, o.Codigofl,
       o.dataaberturaos
  From Man_Os o
     , Man_Osprevisto p
 Where o.codintos = p.codintos
   And o.codigoempresa = 1
   And o.codigoFl = 1
   And o.condicaoos = 'FC'
   And o.tipoos = 'P'
   And p.codigoplanrev = 194
   And o.codigoveic In (Select codigoveic From pbi_veiculosepecasporkm x Where x.KM = 5000)
   And o.dataaberturaos In (Select Data From niff_calendario
                             Where Quadrimestre = '3 Quadrimestre'
                               And Data Between '01-jan-2018' And Sysdate)
 Union All
Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.kmexecucaoos, o.codigoveic, 15000 km,
       po.Descricaomat, po.codigomatint, po.codigointernomaterial,
       Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
       o.codigoEmpresa, o.Codigofl,
       o.dataaberturaos
  From Man_Os o
     , Man_Osprevisto p
     , pbi_veiculosepecasporkm po
 Where o.codintos = p.codintos
   And o.codigoempresa = 1
   And o.codigoFl = 1
   And o.condicaoos = 'FC'
   And o.tipoos = 'P'
   And p.codigoplanrev = 195
   And po.KM = 15000
   And o.codigoveic = po.codigoveic
   And o.dataaberturaos In (Select Data From niff_calendario
                             Where Quadrimestre = '3 Quadrimestre'
                               And Data Between '01-jan-2018' And Sysdate)
 Union All
Select Distinct o.codintos, o.numeroos, p.codigoplanrev, o.kmexecucaoos, o.codigoveic, 30000 km,
       po.Descricaomat, po.codigomatint, po.codigointernomaterial,
       Lpad(o.codigoempresa,3,'0') || '/' || Lpad(o.codigofl,3,'0') EmpFil,
       o.codigoEmpresa, o.Codigofl,
       o.dataaberturaos
  From Man_Os o
     , Man_Osprevisto p
     , pbi_veiculosepecasporkm po
 Where o.codintos = p.codintos
   And o.codigoempresa = 1
   And o.codigoFl = 1
   And o.condicaoos = 'FC'
   And o.tipoos = 'P'
   And p.codigoplanrev = 196
   And po.KM = 30000
   And o.codigoveic = po.codigoveic
   And o.dataaberturaos In (Select Data From niff_calendario
                             Where Quadrimestre = '3 Quadrimestre'
                               And Data Between '01-jan-2018' And Sysdate)

