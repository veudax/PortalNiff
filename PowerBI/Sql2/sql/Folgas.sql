Select f.CODFUNC, 
       f.NomeFunc,
       e.empFil, e.nomeEmpresa,
       To_Char(Ac2.Competacumu, 'mm/yyyy') Mesano,
       7 FolgasFixas,
       Round(referacumu / 7.20) QtdFolgasCompl,
       7 - Round(referacumu / 7.20) FolgasRealizadas
  From Frq_Acumulado Ac2, Vw_Funcionarios f, Pbi_Empresas e
 Where Ac2.Codevento = 375
   And f.Codintfunc = Ac2.Codintfunc
   And e.empFil = lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
   And ((f.Codigoempresa <> 9 And
       Ac2.Competacumu Between
       To_Date('21/' ||
                 To_Char(Add_Months(Trunc(Sysdate), -1), 'mm/yyyy'),
                 'dd/mm/yyyy') And
       To_Date('20/' || To_Char(Trunc(Sysdate), 'mm/yyyy'), 'dd/mm/yyyy')) Or
       (f.Codigoempresa = 9 And
       Ac2.Competacumu Between
       To_Date('26/' ||
                 To_Char(Add_Months(Trunc(Sysdate), -1), 'mm/yyyy'),
                 'dd/mm/yyyy') And
       To_Date('25/' || To_Char(Trunc(Sysdate), 'mm/yyyy'), 'dd/mm/yyyy')))
 Group By f.CODFUNC
     , To_Char(Ac2.Competacumu, 'mm/yyyy')
     , f.NomeFunc
     , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
     , referacumu
     , e.empFil, e.nomeEmpresa
 Order By EmpFil, CodFunc     