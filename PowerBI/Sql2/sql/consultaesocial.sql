select a.* from bgm_configura_servico_windows a where a.chave in ('VERSAO_SERVICO_ESOCIAL','ULTIMA_EXECUCAO') order by 2;

SELECT * FROM ESO_MONITORA_SERVICO ORDER BY ID DESC;

SELECT 'F' TP, F.CODIGOEMPRESA, F.CODIGOFL,
       F.CODFUNC AS CODIGO, A.CODIGO CODINTERNO,
       F.NOMEFUNC AS DESCRICAO, F.SITUACAOFUNC SIT,
       A.PERIODO_APURACAO, A.NM_TABELA, 
       B.DS_CRITICA,
       B.DS_LOCAL_CRITICA,
       B.DT_EVENTO ULTIMO_EVENTO
  FROM ESO_HISTORICO_GERAR      A,
       ESO_HISTORICO_GERAR_CRIT B,
       FLP_FUNCIONARIOS         F
WHERE 
       B.DT_EVENTO = (SELECT MAX(B1.DT_EVENTO) FROM ESO_HISTORICO_GERAR A1, ESO_HISTORICO_GERAR_CRIT B1 WHERE A1.CODIGO = 

A.CODIGO    AND
                                                                                                              

A1.CODIGOEMPRESA = A.CODIGOEMPRESA AND
                                                                                                              A1.CODIGOFL = 

A.CODIGOFL AND              
                                                                                                              A1.TP_AMBIENTE 

= 1      AND 
                                                                                                              A1.ST_ENVIO    

= 4      AND 
                                                                                                              A1.NM_TABELA   

= A.NM_TABELA AND 
                                                                                                              A1.ID_GERAR    

= B1.ID_GERAR )
       
       
   AND A.NM_TABELA     IN ('S2200','S2205','S2206','S2230','S2250','S2299','S1298','S1299')
   AND A.TP_AMBIENTE   = 1
   AND A.ID_GERAR      = B.ID_GERAR
   AND F.CODINTFUNC    = A.CODIGO
   AND F.CODIGOEMPRESA = A.CODIGOEMPRESA
   AND F.CODIGOFL      = A.CODIGOFL
   AND A.ST_ENVIO      = 4
UNION ALL
SELECT 'F' TP,F.CODIGOEMPRESA, F.CODIGOFL,
       F.CODFUNC AS CODIGO, A.CODIGO CODINTERNO,
       F.NOMEFUNC AS DESCRICAO, F.SITUACAOFUNC SIT,
       A.PERIODO_APURACAO, A.NM_TABELA, 
       B.DS_CRITICA,
       B.DS_LOCAL_CRITICA,
       B.DT_EVENTO ULTIMO_EVENTO
  FROM ESO_HISTORICO_GERAR      A,
       ESO_HISTORICO_GERAR_CRIT B,
       FLP_FUNCIONARIOS         F
WHERE 
       B.DT_EVENTO = (SELECT MAX(B1.DT_EVENTO) FROM ESO_HISTORICO_GERAR A1, ESO_HISTORICO_GERAR_CRIT B1 WHERE A1.CODIGO = 

A.CODIGO    AND
                                                                                                              

A1.CODIGOEMPRESA = A.CODIGOEMPRESA AND
                                                                                                              A1.CODIGOFL = 

A.CODIGOFL AND              
                                                                                                              A1.TP_AMBIENTE 

= 1      AND 
                                                                                                              A1.ST_ENVIO    

= 4      AND 
                                                                                                              A1.NM_TABELA   

= A.NM_TABELA AND 
                                                                                                              A1.ID_GERAR    

= B1.ID_GERAR )
       
       
   AND A.NM_TABELA     IN ('S1200','S1210')
   AND A.TP_AMBIENTE   = 1
   AND A.ID_GERAR      = B.ID_GERAR
   AND F.CODINTFUNC    = A.CODIGO
   AND F.CODIGOEMPRESA = A.CODIGOEMPRESA
   AND F.CODIGOFL      = A.CODIGOFL
   AND A.TP_LOTACAO    = 'F'  
   AND A.ST_ENVIO      = 4
UNION ALL
SELECT 'A' TP,F.CODIGOEMPRESA, F.CODIGOFL, 
       F.CODPROAUT AS CODIGO, A.CODIGO CODINTERNO,
       F.NOMEPROAUT AS DESCRICAO, F.SITUACAOPROAUT  SIT,
       A.PERIODO_APURACAO, A.NM_TABELA, 
       B.DS_CRITICA,
       B.DS_LOCAL_CRITICA,
       B.DT_EVENTO ULTIMO_EVENTO
  FROM ESO_HISTORICO_GERAR      A,
       ESO_HISTORICO_GERAR_CRIT B,
       FLP_PROAUTONOMOS         F
WHERE 
       B.DT_EVENTO = (SELECT MAX(B1.DT_EVENTO) FROM ESO_HISTORICO_GERAR A1, ESO_HISTORICO_GERAR_CRIT B1 WHERE A1.CODIGO = 

A.CODIGO    AND
                                                                                                              

A1.CODIGOEMPRESA = A.CODIGOEMPRESA AND
                                                                                                              A1.CODIGOFL = 

A.CODIGOFL AND              
                                                                                                              A1.TP_AMBIENTE 

= 1      AND 
                                                                                                              A1.ST_ENVIO    

= 4      AND 
                                                                                                              A1.NM_TABELA   

= A.NM_TABELA AND 
                                                                                                              A1.ID_GERAR    

= B1.ID_GERAR )
       
       
   AND A.NM_TABELA     IN ('S1200','S1210')
   AND A.TP_AMBIENTE   = 1
   AND A.ID_GERAR      = B.ID_GERAR
   AND F.CODINTPROAUT    = A.CODIGO
   AND F.CODIGOEMPRESA = A.CODIGOEMPRESA
   AND F.CODIGOFL      = A.CODIGOFL
   AND A.TP_LOTACAO    NOT IN ('F')  
   AND A.ST_ENVIO      = 4
UNION ALL
SELECT 'G' TP,A.CODIGOEMPRESA, A.CODIGOFL,
       TO_CHAR(A.CODIGO), A.CODIGO CODINTERNO,
       C.DESCRICAO DESCRICAO, '' SIT,
       A.PERIODO_APURACAO, A.NM_TABELA, 
       B.DS_CRITICA,
       B.DS_LOCAL_CRITICA,
       B.DT_EVENTO ULTIMO_EVENTO
  FROM ESO_HISTORICO_GERAR      A,
       ESO_HISTORICO_GERAR_CRIT B,
       ESO_CAD_TABELA           C
WHERE 
       B.DT_EVENTO = (SELECT MAX(B1.DT_EVENTO) FROM ESO_HISTORICO_GERAR A1, ESO_HISTORICO_GERAR_CRIT B1 WHERE A1.CODIGO = 

A.CODIGO    AND
                                                                                                              

A1.CODIGOEMPRESA = A.CODIGOEMPRESA AND
                                                                                                              A1.CODIGOFL = 

A.CODIGOFL AND             
                                                                                                              A1.TP_AMBIENTE 

= 1      AND 
                                                                                                              A1.ST_ENVIO    

= 4      AND 
                                                                                                              A1.NM_TABELA   

= A.NM_TABELA AND 
                                                                                                              A1.ID_GERAR    

= B1.ID_GERAR )
       
       
   AND A.NM_TABELA=C.CODIGO
   AND A.NM_TABELA     IN ('S1000','S1005','S1010','S1020','S1030','S1050','S1070')
   AND A.TP_AMBIENTE   = 1
   AND A.ID_GERAR      = B.ID_GERAR
   AND A.ST_ENVIO      = 4
ORDER BY NM_TABELA, CODIGOEMPRESA, CODIGOFL, CODIGO;

SELECT A.NM_TABELA
     , (SELECT COUNT(*) 
        FROM ESO_HISTORICO_GERAR G 
        WHERE G.TP_AMBIENTE=A.TP_AMBIENTE AND G.NM_TABELA=A.NM_TABELA) TOTAL_REGISTROS
     , (SELECT SUM(DECODE(G.ST_ENVIO, 5, 1, 0)) 
        FROM ESO_HISTORICO_GERAR G 
        WHERE G.TP_AMBIENTE=A.TP_AMBIENTE AND G.NM_TABELA=A.NM_TABELA) ENVIADOS     
     , (SELECT SUM(DECODE(G.ST_ENVIO, 4, 1, 0)) 
        FROM ESO_HISTORICO_GERAR G 
        WHERE G.TP_AMBIENTE=A.TP_AMBIENTE AND G.NM_TABELA=A.NM_TABELA) COM_CRITICA
     , (SELECT NVL(SUM(DECODE(G.TP_STATUS, 1, 1, 0)),0) 
        FROM ESO_INFORMACAO_GERAR G 
        WHERE G.TP_AMBIENTE=A.TP_AMBIENTE AND G.NM_TABELA=A.NM_TABELA) AGUARGERARXML
     , (SELECT SUM(DECODE(G.ST_ENVIO, 2, 1, 0)) 
        FROM ESO_HISTORICO_GERAR G 
        WHERE G.TP_AMBIENTE=A.TP_AMBIENTE AND G.NM_TABELA=A.NM_TABELA) AGUARENVIO
     , (SELECT SUM(DECODE(G.ST_ENVIO, 3, 1, 0)) 
        FROM ESO_HISTORICO_GERAR G 
        WHERE G.TP_AMBIENTE=A.TP_AMBIENTE AND G.NM_TABELA=A.NM_TABELA) AGUARRETORNO
     , DECODE((SELECT COUNT(*) FROM BGM_CONFIGURA_SERVICO_WINDOWS W WHERE W.VALOR = A.NM_TABELA AND W.ID_SERV = 

'Praxio.Globus.Sped.eSocial.Gerar'), 1, NM_TABELA, 'N')    GERAR
     , DECODE((SELECT COUNT(*) FROM BGM_CONFIGURA_SERVICO_WINDOWS W WHERE W.VALOR = A.NM_TABELA AND W.ID_SERV = 

'Praxio.Globus.Sped.eSocial.Enviar'), 1, NM_TABELA, 'N')   ENVIAR
 FROM 
     ESO_HISTORICO_GERAR A 
 WHERE 
     A.TP_AMBIENTE = 1
 GROUP BY 
     A.TP_AMBIENTE
   , A.NM_TABELA;