Select 'Exames Vencidos' Grupo,
       LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0') EMPFIL,
       Trunc(M.DATAFICHA) Data,
       Count(*) Quantidade
  From FLP_FUNCIONARIOS F,
       SRH_TIPOCONSULTA C,
       SRH_FICHAMEDICA FM,
     ( Select Max(FM.DATAFICHAMED) DATAFICHA, FM.CODINTFUNC
         From SRH_FICHAMEDICA FM
        Where FM.TIPOCONSULTA In ('02','03','04','07')
        Group By FM.CODINTFUNC ) M
 Where F.CODINTFUNC = M.CODINTFUNC 
   And F.CODINTFUNC = FM.CODINTFUNC
   And FM.DATAFICHAMED = M.DATAFICHA
   And C.CODTIPOCONS = FM.TIPOCONSULTA
   And f.situacaofunc = 'A'
   And f.codigoempresa < 100
   And Trunc(M.DataFicha) Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
   And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))
 Group By LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0'),
          Trunc(M.DATAFICHA)
 Union All
Select 'CNH Vencidas' Grupo, 
       LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0') EMPFIL,
       Trunc(D.CNHVENCTO) Data,
       Count(*) Quantidade
  From vwflp_doctos d, 
       Vw_Funcionarios f
 Where f.CODINTFUNC = d.CODINTFUNC
   And f.SITUACAOFUNC = 'A'
   And f.codigoempresa < 900 
   And Trunc(D.CNHVENCTO) Between (ADD_MONTHS(Trunc(Sysdate,'rr'), -12)) -- inicio de 1 ano atraz 
   And (ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))   
 Group By LPAD(F.CODIGOEMPRESA,3,'0') || '/' || Lpad(F.CodigoFl,3,'0'),
          Trunc(D.CNHVENCTO)
 Union All          
select 'Horas Extras (Oper+Manut)' Grupo, 
        EMPFIL,
        Data,
        a.qtd_ocorr Quantidade
  From (Select count(fu.nomefunc) qtd_ocorr, 
               trunc(t.dthist) Data, 
               LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(FU.CodigoFl,3,'0') EMPFIL
          From flp_historico t, frq_ocorrencia f, vw_funcionarios fu
         Where t.dthist BETWEEN '01-jan-2016' And Sysdate
           And f.codocorr = t.codocorr
           And fu.codigoempresa < 100 
           And fu.situacaofunc = 'A'
           And f.codocorr In (505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527)
           And t.codintfunc = fu.codintfunc
           And fu.CODAREA In (20,30,40)
         Group By Trunc(t.dthist), 
                  LPAD(FU.CODIGOEMPRESA,3,'0') || '/' || Lpad(FU.CodigoFl,3,'0')) a        