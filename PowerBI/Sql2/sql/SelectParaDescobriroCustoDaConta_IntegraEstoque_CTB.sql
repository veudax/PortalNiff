SELECT 
  CF.NROPLANO 
NROPLANOCTDEBITO, 
  CF.CODCONTACTB 
CODCONTACTBDEBITO, 
  DECODE(NVL
(NFS.VLRUNITARIONFSERVICO,0), 
0 
                                        , NVL
(NFS.VALORNFSERVICO,0) - 
DECODE(NFS.CODIGOTIPOBEM, 
NULL, 0, NVL
(NFS.VLRDESCONTONFSERV,0)) 
                                        , NVL
(NFS.QTDENFSERVICO ,0) * (NVL
(NFS.VLRUNITARIONFSERVICO ,0) 
                                                            
        -  NVL
(NFS.VLRDESCONTONFSERV    ,0) 
                                                            
        +  NVL(NFS.VALORIPI             
,0) 
                                                            
        +  NVL(NFS.VALORSEGURO     
     ,0) 
                                                            
        +  NVL(NFS.VALORFRETE         
  ,0) 
                                                            
        +  DECODE
(NF.ATRIBUIICMSSUBTOTALNF, 
'S', NVL
(NFS.VALORICMSSUBSTITUICAO,0
), 0) 
                                                            
        +  NVL
(NFS.VLROUTRASDESPNFSERV  
,0))) AS VALORTOTAL, 
  0 DESCONTO, 
  NFS.CODCUSTO, 
  0 DIFERDEBCRED 
FROM 
  BGM_NOTAFISCAL NF, 
  EST_NFSERVICO  NFS, 
  BGM_CLIENTE    BF, 
  CRCCONTACTB_CLIENTE CF 
WHERE 
  CF.NROPLANO           = 10 /* 
PVPLANOCTB */       AND 
  NF.CODINTNF           = 
NFS.CODINTNF     AND 
  NF.CODCLI         = CF.CODCLI    
AND 
  CF.CODCLI         = BF.CODCLI    
AND 
  NF.LANCTOINTEGRADOCTB = 'N'  
          AND 
  NFS.CODINTNF          IN  (SELECT 
CODINTNFSAIDA FROM 
EST_MOVTO WHERE CODINTNF = 
288033 /* PCODINTNF */  AND 
CODINTNFSAIDA IS NOT NULL) 