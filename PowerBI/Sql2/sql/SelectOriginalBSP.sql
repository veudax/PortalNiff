--Select original No BSP

SELECT DISTINCT '001/001 - EOVG DUTRA' EMPRESA, TO_CHAR(LEVE) LEVE, TO_CHAR(C.PESADA) PESADA, C.MECANICO_PES, A.*, 1 qtd
FROM

 (SELECT MOT.ABERTURA, to_char(MOT.OS) OS, DECODE(MOT.ORIGEM,2,'RA',3,'SOS')ORIGEM, MOT.CARRO, MOT.MOTORISTA, MEC.MECANICO_COR, MOT.DEFEITO
  FROM
    (select DISTINCT m.dataaberturaos ABERTURA, m.numeroos OS, m.codorigos ORIGEM, f.codfunc||'-'||f.nomefunc MOTORISTA, v.prefixoveic CARRO, d.descricaodefeito ||' - '|| p.descrcomplosprev DEFEITO
     from   man_os m, flp_funcionarios f, frt_cadveiculos v, man_osprevisto p, man_caddefeitos d
     where  m.dataaberturaos between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))
     and    m.codintfunc = f.codintfunc
     and    p.codintos = m.codintos
     and    p.codigodefeito = d.codigodefeito
     and    p.codigogrpdefeito = d.codigogrpservi
     and    m.codigoveic = v.codigoveic
     and    v.condicaoveic = 'A'
     and    m.codorigos in (2,3) and m.codigoempresa = 1 and m.codigofl = 1) mot, 

    (select DISTINCT m.dataaberturaos, m.numeroos, m.codorigos, f.codfunc||'-'||f.nomefunc MECANICO_COR, v.prefixoveic
     from  man_os m, man_osfuncionarios o, flp_funcionarios f, frt_cadveiculos v
     where m.dataaberturaos between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))
     and   o.codintos = m.codintos
     and   o.codintfunc = f.codintfunc
     and   m.codigoveic = v.codigoveic
     and    v.condicaoveic = 'A'      
     and   m.codorigos in (2,3) and m.codigoempresa = 1 and m.codigofl = 1) mec
  WHERE
   MOT.OS = MEC.NUMEROOS) a,
---------------------------------------

(SELECT MAX(M.DATAABERTURAOS) LEVE, V.PREFIXOVEIC CARRO
FROM   MAN_OS M, FRT_CADVEICULOS V, MAN_OSREALIZADO R
WHERE  V.CODIGOVEIC = M.CODIGOVEIC
AND    R.CODINTOS = M.CODINTOS
AND    M.TIPOOS = 'P'
AND    M.CONDICAOOS NOT IN ('AB')
AND    R.CODIGOPLANREV = 111      
AND    V.CONDICAOVEIC = 'A'   
AND    M.CODIGOEMPRESA = 1 AND M.CODIGOFL = 1
GROUP BY V.PREFIXOVEIC) b,

----------------------------------------
(SELECT DISTINCT C.PESADA, C.CARRO, F.CODFUNC||'-'|| F.NOMEFUNC MECANICO_PES
FROM MAN_OS M, FRT_CADVEICULOS V, MAN_OSFUNCIONARIOS O, FLP_FUNCIONARIOS F, MAN_OSREALIZADO R,
(SELECT MAX(M.DATAABERTURAOS) PESADA, V.PREFIXOVEIC CARRO
FROM   MAN_OS M, FRT_CADVEICULOS V, MAN_OSREALIZADO R
WHERE  V.CODIGOVEIC = M.CODIGOVEIC
AND    R.CODINTOS = M.CODINTOS
AND    M.TIPOOS = 'P'
AND    M.CONDICAOOS NOT IN ('AB')
AND    R.CODIGOPLANREV = 112      
AND    V.CONDICAOVEIC = 'A'   
AND    M.CODIGOEMPRESA = 1 AND M.CODIGOFL = 1
GROUP BY V.PREFIXOVEIC) c
WHERE
           o.codintos = m.codintos
       AND o.codintfunc = f.codintfunc
       AND m.codigoveic = v.codigoveic
       AND C.PESADA = M.DATAABERTURAOS
       AND C.CARRO  = V.PREFIXOVEIC
       AND    R.CODINTOS = M.CODINTOS
       AND    M.TIPOOS = 'P'
       AND    M.CONDICAOOS NOT IN ('AB')
       AND    R.CODIGOPLANREV = 112      
       AND    V.CONDICAOVEIC = 'A'   
       AND    M.CODIGOEMPRESA = 1 AND M.CODIGOFL = 1)C
       

WHERE A.CARRO = B.CARRO
AND   C.CARRO = A.CARRO     
