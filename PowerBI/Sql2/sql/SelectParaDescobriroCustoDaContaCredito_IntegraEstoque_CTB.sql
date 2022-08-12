SELECT nfs.codintnf, nfs.codcusto,
  NFS.NROPLANO                   
NROPLANOCTCREDITO, 
  NFS.CODCONTACTB                
CODCONTACTBCREDITO, 
  DECODE(NVL
(NFS.VLRUNITARIONFSERVICO,0), 
0 
                                            , NVL
(NFS.VALORNFSERVICO,0) - 
DECODE(NFS.CODIGOTIPOBEM, 
NULL, 0, NVL
(NFS.VLRDESCONTONFSERV,0)) 
                                            , NVL
(NFS.QTDENFSERVICO,0) * (NVL
(NFS.VLRUNITARIONFSERVICO ,0) 
                                                            
           -  NVL
(NFS.VLRDESCONTONFSERV    ,0) 
                                                            
           +  NVL(NFS.VALORIPI   ,0) 
                                                            
           +  NVL
(NFS.VALORSEGURO,0) 
                                                            
           +  NVL(NFS.VALORFRETE ,0) 
                                                            
           +  NVL
(NFS.VLROUTRASDESPNFSERV,0)
)) AS VALORTOTAL, 
  0                              DESCONTO 
FROM 
  EST_NFSERVICO  NFS, 
  BGM_NOTAFISCAL NFI 
WHERE 
  NFS.CODINTNF           = 
NFI.CODINTNF AND 
  NFI.LANCTOINTEGRADOCTB = 'N' 
       AND 
  NFS.ATRIBUIDOAOITEM    = 'N'      
  AND 
  NFS.CODINTNF           IN  (SELECT 
CODINTNFSAIDA FROM 
EST_MOVTO WHERE CODINTNF = 
288033 /* PCODINTNF */  AND 
CODINTNFSAIDA IS NOT NULL) 