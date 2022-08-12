Select codbanco, codagencia, codcontabco, codcontactb
     , Sum(saldoIni) SaldoiniCTB
     , Sum(Resultado) Resultado
     , Sum(Debito) debitoCTB
     , Sum(credito) creditoCTB
     , Sum(saldoIni) + Sum(resultado) SaldoFimCTB
     , Sum(InicialBCO) SaldoIniBCO
     , Sum(debitoBCO)  DebitoBCO
     , Sum(CreditoBCo) creditoBCO
     , Sum(FinalBCO) SalfoFimBCO
  From (     
Select c.codbanco, c.codagencia, c.codcontabco, ctb.codcontactb
     , 0 saldoIni
     , Sum(s.vldebitosaldo) - Sum(s.vlcreditosaldo) resultado
     , Sum(s.vldebitosaldo) debito
     , Sum(s.vlcreditosaldo) credito
     , 0 InicialBCo
     , 0 DebitoBCo
     , 0 CreditoBco
     , 0 FinalBCo
  From bcoconta_contactb ctb,  Bcoconta C, ctbsaldo s
 Where ctb.nroplano =10
   And c.codbanco = ctb.codbanco
   And c.codagencia = ctb.codagencia
   And c.codcontabco = ctb.codcontabco
   And c.contacaixa = 'N'
   And s.nroplano = ctb.nroplano
   And s.periodosaldo = '201912'
   And s.codcontactb = ctb.codcontactb
   And c.codigoempresa = s.codigoempresa
 Group By c.codbanco, c.codagencia, c.codcontabco   , ctb.codcontactb
 
Union All

Select c.codbanco, c.codagencia, c.codcontabco, ctb.codcontactb
     , Sum(s.vldebantsaldo) - Sum(s.vlcredantsaldo) SaldoIni
     , 0 resultado 
     , 0 debito
     , 0 credito
     , 0 InicialBCo
     , 0 DebitoBCo
     , 0 CreditoBco
     , 0 FinalBCo
  From bcoconta_contactb ctb,  Bcoconta C, ctbsaldo s
 Where ctb.nroplano =10
   And c.codbanco = ctb.codbanco
   And c.codagencia = ctb.codagencia
   And c.codcontabco = ctb.codcontabco
   And c.contacaixa = 'N'
   And s.nroplano = ctb.nroplano
   And s.periodosaldo Between '201901' And '201912'
   And s.codcontactb = ctb.codcontactb
   And c.codigoempresa = s.codigoempresa
 Group By c.codbanco, c.codagencia, c.codcontabco   , ctb.codcontactb

Union All
Select codbanco, codagencia, codcontabco, 0
     , 0 SaldoIni
     , 0 Resutado
     , 0 debito
     , 0 credito
     , Sum(SaldoAnterior) InicialBCO
     , Sum(Debito) DebitoBCo
     , Sum(credito) CreditoBCo
--     , Sum(saldoinicialcontabco) saldoIniBcoBCO
--     , Sum(saldo_acm_ate_data) saldoAcum
     , Sum(SaldoAnterior) + Sum(Debito) +  Sum(credito) FinalBCo
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
)
Group By codbanco, codagencia, codcontabco, codcontactb