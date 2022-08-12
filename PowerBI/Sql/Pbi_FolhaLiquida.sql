create or replace view pbi_folhaliquida as
Select Sum(FolhaLiquida) folhaLiquida, Sum(FGTS) FGTS, Sum(INSS) Inss, competFicha, EmpFil
     , To_char(competFicha, 'mm/yyyy') mesAno
     , To_char(competFicha, 'yyyymm') AnoMes
     , To_char(competFicha, 'yyyy') Ano
From niff_chm_empresas e
   , (
Select Sum(i.valorficha) FolhaLiquida, 0 FGTS, 0 INSS, competFicha
     , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
  From Flp_Fichaeventos i, flp_funcionarios f
 Where tipoFolha = 1
   And codEvento In (996,467)
   And competFicha BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -12) and sysdate
   And i.codintFunc = f.codIntFunc
   And f.codigoempresa < 100
 Group By competFicha
      , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
 Union All
Select 0 FolhaLiquida, Sum(i.valorficha) FGTS, 0 INSS, competFicha
     , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
  From Flp_Fichaeventos i, flp_funcionarios f
 Where tipoFolha = 1
   And codEvento In 993
   And competFicha BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -12) and sysdate
   And i.codintFunc = f.codIntFunc
 Group By competFicha
       , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
 Union All
Select 0 FolhaLiquida, 0 FGTS, Sum(i.valorficha) INSS, competFicha
     , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
  From Flp_Fichaeventos i, flp_funcionarios f
 Where tipoFolha = 1
   And codEvento In (451,485)
   And competFicha BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -12) and sysdate
   And i.codintFunc = f.codIntFunc
   And f.codigoempresa < 100
 Group By competFicha
       , lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0')
)
 Where e.codigoglobus = EmpFil
 Group By competFicha, EmpFil

