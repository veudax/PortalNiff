Select Sum(m.vlmovtobco), c.codcontabco, 
c.Nomecontabco, a.codbanco 
     , a.nomeagencbco, b.codbanco, 
b.nomebanco, m.codigofl
     , M.CODCONTACTB ,
m.confirmadomovtobco, 
  m.conciliadomovtobco 
  From bcomovto m, bcoconta c, 
bcoagenc a, bcobanco b, bcohisto h 
 Where m.codigoempresa = 3 /* 
pEmp */ 
   And dtefetivamovtobco between 
TO_DATE('01/12/2009 00:00:00', 
'DD/MM/YYYY HH24:MI:SS') /* 
pDataIni */  And TO_DATE
('31/12/2019 00:00:00', 
'DD/MM/YYYY HH24:MI:SS') /* 
pDataFin */ 
   And m.codbanco = '237' /* pBanco 
*/ 
   And m.codagencia = '03376' /* 
pAgencia */ 
   And m.codcontabco = '3404' /* 
pConta */ 
   And c.ContaCaixa = 'N' 
   And m.statusMovtobco = 'N' 
   And h.codigoempresa  = 
m.codigoempresa 
   And h.codigofl       = m.codigofl 
   And h.codhistobco    = 
m.codhistobco 
   And b.codbanco       = a.codbanco 
   And a.codbanco       = c.codbanco 
   And c.codbanco       = m.codbanco 
   And a.codagencia     = 
c.codagencia 
   And c.codagencia     = 
m.codagencia 
   And c.codcontabco    = 
m.codcontabco 

And VLMOVTOBCO < 0
Group By c.codcontabco, 
c.Nomecontabco, a.codbanco 
     , a.nomeagencbco, b.codbanco, 
b.nomebanco, m.codigofl
     , M.CODCONTACTB 
     , m.vlmoeda, m.CodAgencia, 
m.confirmadomovtobco, 
  m.conciliadomovtobco 