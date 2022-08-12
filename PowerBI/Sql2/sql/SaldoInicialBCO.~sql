Select Sum(SaldoAnterior) Inicial
     , Sum(Debito) Debito
     , Sum(credito) Credito
     , Sum(saldoinicialcontabco) saldoIniBco
     , Sum(saldo_acm_ate_data) saldoAcum
     , Sum(SaldoAnterior) + Sum(Debito) +  Sum(credito) Final
     , codbanco, codagencia, codcontabco
  From (
SELECT SUM(VLMOVTOBCO) saldoAnterior
     , 0 Debito
     , 0 Credito
     , c.codbanco, c.codagencia, c.codcontabco, c.saldoinicialcontabco, c.saldo_acm_ate_data
  FROM BCOMOVTO M, Bcoconta C
 WHERE M.CODBANCO = c.Codbanco
   AND M.CODAGENCIA  = c.Codagencia
   AND M.CODCONTABCO = c.codcontabco
   AND M.DTEFETIVAMOVTOBCO < '01-dec-2019'
   AND M.STATUSMOVTOBCO = 'N' 
   AND M.CONCILIADOMOVTOBCO = 'S' 
   AND M.CODIGOEMPRESA = 3 
   And c.contacaixa = 'N'
Group By c.codbanco, c.codagencia, c.codcontabco, c.saldoinicialcontabco, c.saldo_acm_ate_data   

Union All

SELECT 0 saldoAnterior
     , SUM(VLMOVTOBCO) Debito
     , 0 Credito
     , c.codbanco, c.codagencia, c.codcontabco, 0 saldoinicialcontabco, 0 saldo_acm_ate_data
  FROM BCOMOVTO M, Bcoconta C
 WHERE m.codigoempresa = 3 /*pEmp */ 
   And trunc(dtefetivamovtobco) between '01-dec-2019' And '31-dec-2019'
And c.codbanco = 237
   And c.ContaCaixa = 'N' 
   And m.statusMovtobco = 'N' 
   And m.conciliadomovtobco = 'S'
   And c.codbanco       = m.codbanco 
   And c.codagencia     = m.codagencia 
   And c.codcontabco    = m.codcontabco 
And VLMOVTOBCO < 0
Group By c.codbanco, c.codagencia, c.codcontabco

Union All 


SELECT 0 saldoAnterior
     , 0 Debito
     , SUM(VLMOVTOBCO)  Credito
     , c.codbanco, c.codagencia, c.codcontabco, 0 saldoinicialcontabco, 0 saldo_acm_ate_data
  FROM BCOMOVTO M, Bcoconta C
 WHERE m.codigoempresa = 3 /*pEmp */ 
   And trunc(dtefetivamovtobco) between '01-dec-2019' And '31-dec-2019'
And c.codbanco = 237
   And c.ContaCaixa = 'N' 
   And m.statusMovtobco = 'N' 
   And m.conciliadomovtobco = 'S'
   And c.codbanco       = m.codbanco 
   And c.codagencia     = m.codagencia 
   And c.codcontabco    = m.codcontabco 
And VLMOVTOBCO > 0
Group By c.codbanco, c.codagencia, c.codcontabco
)
Group By codbanco, codagencia, codcontabco