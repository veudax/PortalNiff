Create Or Replace View Pbi_Radar_Manut_TrocaOleo As
       (SELECT /*+ RULE */ 
               EmpFil, 
               COUNT(PREFIXOVEIC) Vencidas,
               Data
          From (Select CODIGOVEIC, 
                       PREFIXOVEIC, 
                       P.CODIGOPLANREV, 
                       EmpFil
                  From (Select A.CODIGOVEIC, 
                               A.PREFIXOVEIC, 
                               LPAD(A.CodigoEmpresa,3,'0') || '/' || Lpad(A.CodigoFl,3,'0')  EmpFil,
                               A.CODGRPREV
                          From FRT_CADVEICULOS A, 
                               BGM_VELOCIMETRO B,
                               FRT_TIPODEFROTA F 
                         Where A.CODIGOEMPRESA = 1 
                           And A.CODIGOFL = 1 
                           And A.CODIGOTPFROTA = F.CODIGOTPFROTA       
                           And A.CODIGOVEIC    = B.CODIGOVEIC(+)       
                           AND ((B.DATAVELOC Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                           And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)) OR (B.DATAVELOC IS NULL)) 
                           And A.CONDICAOVEIC = 'A' 
                           And A.CODGRPREV = 7 
                         Group By A.CODIGOVEIC, 
                                   A.PREFIXOVEIC, 
                                   LPAD(A.CodigoEmpresa,3,'0') || '/' || Lpad(A.CodigoFl,3,'0'),
                                   A.CODGRPREV) V, 
                       MAN_PLANODEREVISAO P, 
                       MAN_KMREVISAO      F 
                 Where P.CODIGOPLANREV IN (114)    
                   And V.CODGRPREV     = F.CODGRPREV 
                   And F.CODIGOPLANREV = P.CODIGOPLANREV ) VEIC, 
               (Select C.CODIGOVEIC,  
                       D.CODIGOPLANREV, 
                       fcman_kmexcessoplano(C.CODIGOVEIC,SYSDATE,D.CODIGOPLANREV,1) AS KMEXCESSO,
                       MAX(C.CODINTOS) CODINTOS,
                       Last_Day(MAX(C.DATAFECHAMENTOOS))      AS DATA, 
                       MAX(H.QTDEEXECUCAOPLANO)     AS QTDEEXECUCAOPLANO 
                  From MAN_OSREALIZADO D, 
                       MAN_OS          C, 
                       MAN_QTDEEXECUCOESPLANO  H, 
                       MAN_KMREVISAO           W 
                 Where C.CODINTOS          = D.CODINTOS          
                   And H.QTDEEXECUCAOPLANO = W.SEQKMREVISAO  (+) 
                   And H.CODIGOPLANREV     = W.CODIGOPLANREV (+) 
                   And D.CODIGOPLANREV     = H.CODIGOPLANREV     
                   And C.CODIGOVEIC        = H.CODIGOVEIC 
                   And c.DATAFECHAMENTOOS Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                   And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)        
                   And C.CODIGOEMPRESA = 1 
                   And C.CODIGOFL = 1   
                   And D.CODIGOPLANREV = 114                     
                   And C.CODIGOVEIC IN ( Select CODIGOVEIC From FRT_CADVEICULOS Where CONDICAOVEIC = 'A' And CODIGOEMPRESA = 1 And CODIGOFL = 1  ) 
                 Group By C.CODIGOVEIC,  D.CODIGOPLANREV ) OS
         Where VEIC.CODIGOVEIC     = OS.CODIGOVEIC     (+)   
           And VEIC.CODIGOPLANREV  = OS.CODIGOPLANREV  (+)   
           And OS.KMEXCESSO > 0
         Group By EmpFil, Data)
         Union All
       (Select /*+ RULE */ 
               EmpFil,
               COUNT(PREFIXOVEIC) Vencidas,
               Data
          From (Select CODIGOVEIC, 
                       PREFIXOVEIC, 
                       P.CODIGOPLANREV, 
                       EmpFil
                  From (Select A.CODIGOVEIC, 
                               A.PREFIXOVEIC, 
                               A.CODGRPREV, 
                               LPAD(A.CodigoEmpresa,3,'0') || '/' || Lpad(A.CodigoFl,3,'0') Empfil
                          From FRT_CADVEICULOS A, 
                               BGM_VELOCIMETRO B,
                               FRT_TIPODEFROTA F 
                         Where A.CODIGOEMPRESA = 1 
                           And A.CODIGOFL = 2 
                           And A.CODIGOTPFROTA = F.CODIGOTPFROTA       
                           And A.CODIGOVEIC    = B.CODIGOVEIC(+)       
                           And ((B.DATAVELOC Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                           And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)) OR (B.DATAVELOC IS NULL)) 
                           And A.CONDICAOVEIC = 'A' 
                           And A.CODGRPREV = 150 
                         Group By A.CODIGOVEIC, 
                                  A.PREFIXOVEIC, 
                                  LPAD(A.CodigoEmpresa,3,'0') || '/' || Lpad(A.CodigoFl,3,'0'),
                                  A.CODGRPREV) V, 
                       MAN_PLANODEREVISAO P, 
                       MAN_KMREVISAO      F 
                 Where P.CODIGOPLANREV IN (508)    
                   And V.CODGRPREV     = F.CODGRPREV 
                   And F.CODIGOPLANREV = P.CODIGOPLANREV ) VEIC, 
               (Select C.CODIGOVEIC,  
                       D.CODIGOPLANREV, 
                       fcman_kmexcessoplano(C.CODIGOVEIC,SYSDATE,D.CODIGOPLANREV,1) AS KMEXCESSO,
                       MAX(C.CODINTOS) CODINTOS,
                       Last_Day(MAX(C.DATAFECHAMENTOOS))      AS DATA, 
                       MAX(H.QTDEEXECUCAOPLANO)     AS QTDEEXECUCAOPLANO 
                  From MAN_OSREALIZADO D, 
                       MAN_OS          C, 
                       MAN_QTDEEXECUCOESPLANO  H, 
                       MAN_KMREVISAO           W 
                 Where C.CODINTOS          = D.CODINTOS          
                   And H.QTDEEXECUCAOPLANO = W.SEQKMREVISAO  (+) 
                   And H.CODIGOPLANREV     = W.CODIGOPLANREV (+) 
                   And D.CODIGOPLANREV     = H.CODIGOPLANREV     
                   And C.CODIGOVEIC        = H.CODIGOVEIC  
                   And c.DATAFECHAMENTOOS Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                   And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)       
                   And C.CODIGOEMPRESA = 1 
                   And C.CODIGOFL = 2   
                   And D.CODIGOPLANREV IN (508)                     
                   And C.CODIGOVEIC IN ( Select CODIGOVEIC From FRT_CADVEICULOS Where CONDICAOVEIC = 'A' And CODIGOEMPRESA = 1 And CODIGOFL = 2) 
                 Group By  C.CODIGOVEIC,  D.CODIGOPLANREV ) OS
         Where VEIC.CODIGOVEIC     = OS.CODIGOVEIC     (+)   
           And VEIC.CODIGOPLANREV  = OS.CODIGOPLANREV  (+)   
           And OS.KMEXCESSO > 0
         Group By EmpFil, Data)     
         Union All
       (Select /*+ RULE */ 
               EmpFil,
               COUNT(PREFIXOVEIC) Vencidas,
               Os.Data
          From (Select CODIGOVEIC, 
                       PREFIXOVEIC, 
                       P.CODIGOPLANREV,
                       Empfil
                  From (Select A.CODIGOVEIC, 
                               A.PREFIXOVEIC, 
                               A.CODGRPREV,
                               LPAD(A.CodigoEmpresa,3,'0') || '/' || Lpad(A.CodigoFl,3,'0') EmpFil
                          From FRT_CADVEICULOS A, 
                               BGM_VELOCIMETRO B,
                               FRT_TIPODEFROTA F 
                         Where A.CODIGOEMPRESA = 6 
                           And A.CODIGOFL = 1 
                           And A.CODIGOTPFROTA = F.CODIGOTPFROTA       
                           And A.CODIGOVEIC    = B.CODIGOVEIC(+)       
                           And ((B.DATAVELOC Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                           And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)) OR (B.DATAVELOC IS NULL)) 
                           And A.CONDICAOVEIC = 'A' 
                           And A.CODGRPREV IN (600,8,5) 
                         Group By A.CODIGOVEIC, 
                                  A.PREFIXOVEIC, 
                                  LPAD(A.CodigoEmpresa,3,'0') || '/' || Lpad(A.CodigoFl,3,'0'),
                                  A.CODGRPREV) V, 
                       MAN_PLANODEREVISAO P, 
                       MAN_KMREVISAO      F 
                 Where P.CODIGOPLANREV IN (169)    
                   And V.CODGRPREV     = F.CODGRPREV 
                   And F.CODIGOPLANREV = P.CODIGOPLANREV ) VEIC, 
               (Select C.CODIGOVEIC,  
                       D.CODIGOPLANREV, 
                       fcman_kmexcessoplano(C.CODIGOVEIC,SYSDATE,D.CODIGOPLANREV,1) AS KMEXCESSO,
                       MAX(C.CODINTOS) CODINTOS,
                       Last_day(MAX(C.DATAFECHAMENTOOS)) AS DATA, 
                       MAX(H.QTDEEXECUCAOPLANO)     AS QTDEEXECUCAOPLANO 
                  From MAN_OSREALIZADO D, 
                       MAN_OS          C, 
                       MAN_QTDEEXECUCOESPLANO  H, 
                       MAN_KMREVISAO           W 
                 Where C.CODINTOS          = D.CODINTOS          
                   And H.QTDEEXECUCAOPLANO = W.SEQKMREVISAO  (+) 
                   And H.CODIGOPLANREV     = W.CODIGOPLANREV (+) 
                   And D.CODIGOPLANREV     = H.CODIGOPLANREV     
                   And C.CODIGOVEIC        = H.CODIGOVEIC      
                   And c.DATAFECHAMENTOOS Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                   And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)   
                   And C.CODIGOEMPRESA = 6 
                   And C.CODIGOFL = 1   
                   And D.CODIGOPLANREV IN (169)                     
                   And C.CODIGOVEIC In (Select CODIGOVEIC From FRT_CADVEICULOS Where CONDICAOVEIC = 'A' And CODIGOEMPRESA = 6 AND CODIGOFL = 1) 
                 Group By C.CODIGOVEIC, D.CODIGOPLANREV ) OS
         Where VEIC.CODIGOVEIC     = OS.CODIGOVEIC     (+)  
           And VEIC.CODIGOPLANREV  = OS.CODIGOPLANREV  (+)   
           And OS.KMEXCESSO > 0
         Group By Empfil, Data)     
         Union All
       (Select /*+ RULE */ 
               EmpFil,
               COUNT(PREFIXOVEIC) VENCIDA_15000,
               Data
          From (Select CODIGOVEIC, 
                       PREFIXOVEIC, 
                       P.CODIGOPLANREV,
                       Empfil
                  From (Select A.CODIGOVEIC, 
                               A.PREFIXOVEIC, 
                               A.CODGRPREV, 
                               LPAD(a.CodigoEmpresa,3,'0') || '/' || Lpad(a.CodigoFl,3,'0') EmpFil
                          From FRT_CADVEICULOS A, 
                               BGM_VELOCIMETRO B,
                               FRT_TIPODEFROTA F 
                         Where A.CODIGOEMPRESA = 26 
                           And A.CODIGOFL In (1,3)
                           And A.CODIGOTPFROTA = F.CODIGOTPFROTA       
                           And A.CODIGOVEIC    = B.CODIGOVEIC(+)       
                           And ((b.dataveloc Is Null) 
                            Or (b.Dataveloc Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                           And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)))
                           And A.CONDICAOVEIC = 'A' 
                           And A.CODGRPREV = 7 
                         Group By A.CODIGOVEIC, 
                                  A.PREFIXOVEIC, 
                                  LPAD(a.CodigoEmpresa,3,'0') || '/' || Lpad(a.CodigoFl,3,'0'),
                                  A.CODGRPREV) V, 
                       MAN_PLANODEREVISAO P, 
                       MAN_KMREVISAO      F 
                 Where P.CODIGOPLANREV IN (114)    
                   And V.CODGRPREV     = F.CODGRPREV 
                   And F.CODIGOPLANREV = P.CODIGOPLANREV ) VEIC, 
               (Select C.CODIGOVEIC,  
                       D.CODIGOPLANREV, 
                       fcman_kmexcessoplano(C.CODIGOVEIC,SYSDATE,D.CODIGOPLANREV,1) AS KMEXCESSO,
                       MAX(C.CODINTOS) CODINTOS,
                       Last_day(MAX(C.DATAFECHAMENTOOS))      AS DATA, 
                       MAX(H.QTDEEXECUCAOPLANO)     AS QTDEEXECUCAOPLANO 
                  From MAN_OSREALIZADO D, 
                       MAN_OS          C, 
                       MAN_QTDEEXECUCOESPLANO  H, 
                       MAN_KMREVISAO           W 
                 Where C.CODINTOS          = D.CODINTOS          
                   And H.QTDEEXECUCAOPLANO = W.SEQKMREVISAO  (+) 
                   And H.CODIGOPLANREV     = W.CODIGOPLANREV (+) 
                   And D.CODIGOPLANREV     = H.CODIGOPLANREV     
                   And C.CODIGOVEIC        = H.CODIGOVEIC   
                   And c.DATAFECHAMENTOOS Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                   And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)      
                   And C.CODIGOEMPRESA = 26 
                   And C.CODIGOFL In (1,3)
                   And D.CODIGOPLANREV = 114                     
                   And C.CODIGOVEIC IN (Select CODIGOVEIC From FRT_CADVEICULOS Where CONDICAOVEIC = 'A' And CODIGOEMPRESA = 26 And CODIGOFL = 1) 
                 Group By C.CODIGOVEIC, D.CODIGOPLANREV ) OS
         Where VEIC.CODIGOVEIC     = OS.CODIGOVEIC     (+)   
           And VEIC.CODIGOPLANREV  = OS.CODIGOPLANREV  (+)   
           And OS.KMEXCESSO > 0
         Group By EmpFil, Data)     
         Union All
       (Select /*+ RULE */ 
               EmpFil,
               COUNT(PREFIXOVEIC) Vencidas,
               Data
          From (Select CODIGOVEIC, 
                       PREFIXOVEIC, 
                       P.CODIGOPLANREV,
                       Empfil
                  From (Select A.CODIGOVEIC, 
                               A.PREFIXOVEIC, 
                               A.CODGRPREV, 
                               LPAD(a.CodigoEmpresa,3,'0') || '/' || Lpad(a.CodigoFl,3,'0') EmpFil
                          From FRT_CADVEICULOS A, 
                               BGM_VELOCIMETRO B,
                               FRT_TIPODEFROTA F 
                         Where A.CODIGOEMPRESA = 2 
                           And A.CODIGOFL = 1 
                           And A.CODIGOTPFROTA = F.CODIGOTPFROTA       
                           And A.CODIGOVEIC    = B.CODIGOVEIC(+)      
                           And ((b.dataveloc Is Null) 
                            Or (b.Dataveloc Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                           And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)))
                           And A.CONDICAOVEIC = 'A' 
                           And A.CODGRPREV = 30 
                 Group By A.CODIGOVEIC, 
                          A.PREFIXOVEIC, 
                          LPAD(a.CodigoEmpresa,3,'0') || '/' || Lpad(a.CodigoFl,3,'0'),
                          A.CODGRPREV) V, 
                       MAN_PLANODEREVISAO P, 
                       MAN_KMREVISAO      F 
                 Where P.CODIGOPLANREV IN (42)    
                   And V.CODGRPREV     = F.CODGRPREV 
                   And F.CODIGOPLANREV = P.CODIGOPLANREV ) VEIC, 
               (SELECT C.CODIGOVEIC,  
                       D.CODIGOPLANREV, 
                       fcman_kmexcessoplano(C.CODIGOVEIC,SYSDATE,D.CODIGOPLANREV,1) AS KMEXCESSO,
                       MAX(C.CODINTOS) CODINTOS,
                       Last_day(MAX(C.DATAFECHAMENTOOS))      AS DATA, 
                       MAX(H.QTDEEXECUCAOPLANO)     AS QTDEEXECUCAOPLANO 
                  From MAN_OSREALIZADO D, 
                       MAN_OS          C, 
                       MAN_QTDEEXECUCOESPLANO  H, 
                       MAN_KMREVISAO           W 
                 Where C.CODINTOS          = D.CODINTOS          
                   And H.QTDEEXECUCAOPLANO = W.SEQKMREVISAO  (+) 
                   And H.CODIGOPLANREV     = W.CODIGOPLANREV (+) 
                   And D.CODIGOPLANREV     = H.CODIGOPLANREV     
                   And C.CODIGOVEIC        = H.CODIGOVEIC    
                   And c.DATAFECHAMENTOOS Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                   And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)     
                   And C.CODIGOEMPRESA = 2 
                   And C.CODIGOFL = 1   
                   And D.CODIGOPLANREV IN (42)                     
                   And C.CODIGOVEIC In (Select CODIGOVEIC From FRT_CADVEICULOS Where CONDICAOVEIC = 'A' And CODIGOEMPRESA = 2 And CODIGOFL = 1  ) 
                 Group By C.CODIGOVEIC, D.CODIGOPLANREV   ) OS
         Where VEIC.CODIGOVEIC     = OS.CODIGOVEIC     (+)   
           And VEIC.CODIGOPLANREV  = OS.CODIGOPLANREV  (+)   
           And OS.KMEXCESSO > 0
         Group By EmpFil, Data)       
         Union All                                                        
       (SELECT /*+ RULE */ 
               EmpFil,
               COUNT(PREFIXOVEIC) Vencidas,
               Data
          From (Select CODIGOVEIC, 
                       PREFIXOVEIC, 
                       P.CODIGOPLANREV,
                       EmpFil
                  From (Select A.CODIGOVEIC, 
                               A.PREFIXOVEIC, 
                               A.CODGRPREV, 
                               LPAD(a.CodigoEmpresa,3,'0') || '/' || Lpad(a.CodigoFl,3,'0') EmpFil
                          From FRT_CADVEICULOS A, 
                               BGM_VELOCIMETRO B,
                               FRT_TIPODEFROTA F 
                         Where A.CODIGOEMPRESA = 4 
                           And A.CODIGOFL = 1 
                           And A.CODIGOTPFROTA = F.CODIGOTPFROTA       
                           And A.CODIGOVEIC    = B.CODIGOVEIC(+)       
                           And ((b.dataveloc Is Null) 
                            Or (b.Dataveloc Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                           And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))) 
                           And A.CONDICAOVEIC = 'A' 
                           And A.CODGRPREV IN (2,6) 
                         Group By A.CODIGOVEIC, 
                                  A.PREFIXOVEIC, 
                                  LPAD(a.CodigoEmpresa,3,'0') || '/' || Lpad(a.CodigoFl,3,'0'),
                                  A.CODGRPREV) V, 
                       MAN_PLANODEREVISAO P, 
                       MAN_KMREVISAO      F 
                 Where P.CODIGOPLANREV IN (49)    
                   And V.CODGRPREV     = F.CODGRPREV 
                   And F.CODIGOPLANREV = P.CODIGOPLANREV ) VEIC, 
               (Select C.CODIGOVEIC,  
                       D.CODIGOPLANREV, 
                       FCMAN_KMEXCESSOPLANO(C.CODIGOVEIC,SYSDATE,D.CODIGOPLANREV,1) AS KMEXCESSO,
                       MAX(C.CODINTOS) CODINTOS,
                       Last_day(MAX(C.DATAFECHAMENTOOS))      AS DATA, 
                       MAX(H.QTDEEXECUCAOPLANO)     AS QTDEEXECUCAOPLANO 
                  From MAN_OSREALIZADO D, 
                       MAN_OS          C, 
                       MAN_QTDEEXECUCOESPLANO  H, 
                       MAN_KMREVISAO           W 
                 Where C.CODINTOS          = D.CODINTOS          
                   And H.QTDEEXECUCAOPLANO = W.SEQKMREVISAO  (+) 
                   And H.CODIGOPLANREV     = W.CODIGOPLANREV (+) 
                   And D.CODIGOPLANREV     = H.CODIGOPLANREV     
                   And C.CODIGOVEIC        = H.CODIGOVEIC    
                   And c.DATAFECHAMENTOOS Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                   And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)     
                   And C.CODIGOEMPRESA = 4 
                   And C.CODIGOFL = 1   
                   And D.CODIGOPLANREV IN (49)                     
                   And C.CODIGOVEIC IN (Select CODIGOVEIC From FRT_CADVEICULOS Where CONDICAOVEIC = 'A' And CODIGOEMPRESA = 4 And CODIGOFL = 1 ) 
                 GROUP BY C.CODIGOVEIC, D.CODIGOPLANREV ) OS
         Where VEIC.CODIGOVEIC     = OS.CODIGOVEIC     (+)   
           And VEIC.CODIGOPLANREV  = OS.CODIGOPLANREV  (+)   
           And OS.KMEXCESSO > 0
         Group By EmpFil, Data)                                                               
         Union All
       (SELECT /*+ RULE */ 
               EmpFil,
               COUNT(PREFIXOVEIC) VENCIDA_15000,
               Data
          From (Select CODIGOVEIC, 
                       PREFIXOVEIC, 
                       P.CODIGOPLANREV, 
                       EmpFil
                  From (Select A.CODIGOVEIC, 
                               A.PREFIXOVEIC, 
                               A.CODGRPREV, 
                               LPAD(a.CodigoEmpresa,3,'0') || '/' || Lpad(a.CodigoFl,3,'0') EmpFil
                          From FRT_CADVEICULOS A, 
                               BGM_VELOCIMETRO B,
                               FRT_TIPODEFROTA F 
                         Where A.CODIGOEMPRESA = 9 
                           And A.CODIGOFL = 1
                           And A.CODIGOTPFROTA = F.CODIGOTPFROTA       
                           And A.CODIGOVEIC    = B.CODIGOVEIC(+)       
                           And ((b.dataveloc Is Null) 
                            Or (b.Dataveloc Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                           And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1))) 
                           And A.CONDICAOVEIC = 'A'
                           And A.CODGRPREV IN (1,5)   
                         Group By A.CODIGOVEIC, 
                                  A.PREFIXOVEIC, 
                                  LPAD(a.CodigoEmpresa,3,'0') || '/' || Lpad(a.CodigoFl,3,'0') ,
                                  A.CODGRPREV) V, 
                       MAN_PLANODEREVISAO P, 
                       MAN_KMREVISAO      F 
                 Where P.CODIGOPLANREV IN (86)    
                   And V.CODGRPREV     = F.CODGRPREV 
                   And F.CODIGOPLANREV = P.CODIGOPLANREV ) VEIC, 
               (Select C.CODIGOVEIC,  
                       D.CODIGOPLANREV, 
                       FCMAN_KMEXCESSOPLANO(C.CODIGOVEIC,SYSDATE,D.CODIGOPLANREV,1) AS KMEXCESSO,
                       MAX(C.CODINTOS) CODINTOS,
                       Last_day(MAX(C.DATAFECHAMENTOOS))      AS DATA, 
                       MAX(H.QTDEEXECUCAOPLANO)     AS QTDEEXECUCAOPLANO 
                  From MAN_OSREALIZADO D, 
                       MAN_OS          C, 
                       MAN_QTDEEXECUCOESPLANO  H, 
                       MAN_KMREVISAO           W 
                 Where C.CODINTOS          = D.CODINTOS          
                   And H.QTDEEXECUCAOPLANO = W.SEQKMREVISAO  (+) 
                   And H.CODIGOPLANREV     = W.CODIGOPLANREV (+) 
                   And D.CODIGOPLANREV     = H.CODIGOPLANREV     
                   And C.CODIGOVEIC        = H.CODIGOVEIC 
                   And c.DATAFECHAMENTOOS Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                   And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)        
                   And C.CODIGOEMPRESA = 9 
                   And C.CODIGOFL = 1   
                   And D.CODIGOPLANREV IN (86)                     
                   And C.CODIGOVEIC IN ( Select CODIGOVEIC From FRT_CADVEICULOS Where CONDICAOVEIC = 'A' And CODIGOEMPRESA = 9 And CODIGOFL = 1 ) 
                 Group By C.CODIGOVEIC, D.CODIGOPLANREV ) OS
         Where VEIC.CODIGOVEIC     = OS.CODIGOVEIC     (+)   
           And VEIC.CODIGOPLANREV  = OS.CODIGOPLANREV  (+)   
           And OS.KMEXCESSO > 0
         Group By EmpFil, Data)                                                               
         Union All
       (SELECT /*+ RULE */ 
               EmpFil,
               COUNT(PREFIXOVEIC) VENCIDA_15000,
               Data
          From (Select CODIGOVEIC, 
                       PREFIXOVEIC, 
                       P.CODIGOPLANREV,
                       EmpFil
                  From (Select A.CODIGOVEIC, 
                               A.PREFIXOVEIC, 
                               A.CODGRPREV, 
                               LPAD(a.CodigoEmpresa,3,'0') || '/' || Lpad(a.CodigoFl,3,'0') EmpFil
                          From FRT_CADVEICULOS A, 
                               BGM_VELOCIMETRO B,
                               FRT_TIPODEFROTA F 
                         Where A.CODIGOEMPRESA In (3,13) 
                           And A.CODIGOFL = 1 
                           And A.CODIGOTPFROTA = F.CODIGOTPFROTA       
                           And A.CODIGOVEIC    = B.CODIGOVEIC(+)       
                           And ((b.dataveloc Is Null) 
                            Or (b.Dataveloc Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                           And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1)))                     
                           And A.CONDICAOVEIC = 'A'
                           And A.CODGRPREV IN (6,10,11,12,13,16,17,18,19,21,22,23)  
                         Group By A.CODIGOVEIC, 
                                  A.PREFIXOVEIC, 
                                  LPAD(a.CodigoEmpresa,3,'0') || '/' || Lpad(a.CodigoFl,3,'0'),
                                  A.CODGRPREV) V, 
                       MAN_PLANODEREVISAO P, 
                       MAN_KMREVISAO      F 
                 Where P.CODIGOPLANREV IN (150)    
                   And V.CODGRPREV     = F.CODGRPREV 
                   And F.CODIGOPLANREV = P.CODIGOPLANREV ) VEIC, 
               (Select C.CODIGOVEIC,  
                       D.CODIGOPLANREV, 
                       FCMAN_KMEXCESSOPLANO(C.CODIGOVEIC,SYSDATE,D.CODIGOPLANREV,1) AS KMEXCESSO,
                       MAX(C.CODINTOS) CODINTOS,
                       Last_day(MAX(C.DATAFECHAMENTOOS))      AS DATA, 
                       MAX(H.QTDEEXECUCAOPLANO)     AS QTDEEXECUCAOPLANO 
                  From MAN_OSREALIZADO D, 
                       MAN_OS          C, 
                       MAN_QTDEEXECUCOESPLANO  H, 
                       MAN_KMREVISAO           W 
                 Where C.CODINTOS          = D.CODINTOS          
                   And H.QTDEEXECUCAOPLANO = W.SEQKMREVISAO  (+) 
                   And H.CODIGOPLANREV     = W.CODIGOPLANREV (+) 
                   And D.CODIGOPLANREV     = H.CODIGOPLANREV     
                   And C.CODIGOVEIC        = H.CODIGOVEIC       
                   And c.DATAFECHAMENTOOS Between ADD_MONTHS(Trunc(Sysdate,'rr'), -12)
                   And ADD_MONTHS(Last_day(Trunc(Sysdate)), -1) 
                   And C.CODIGOEMPRESA In (3,13) 
                   AND C.CODIGOFL = 1   
                   And D.CODIGOPLANREV IN (150)                     
                   And C.CODIGOVEIC In (Select CODIGOVEIC From FRT_CADVEICULOS Where CONDICAOVEIC = 'A' And CODIGOEMPRESA = 3 And CODIGOFL = 1 ) 
                 Group By C.CODIGOVEIC, D.CODIGOPLANREV ) OS
         Where VEIC.CODIGOVEIC     = OS.CODIGOVEIC     (+)   
           And VEIC.CODIGOPLANREV  = OS.CODIGOPLANREV  (+)   
           And OS.KMEXCESSO > 0
         Group By EmpFil, Data   )                                         