SELECT '001/001 - EOVG DUTRA' EMPRESA, to_char(A.LEVE) LEVE, TO_CHAR(A.PESADA) PESADA, Mecanico_Pesado, Mecanico_Leve, 
   A.*, 0 qtd
FROM
(
Select a.Dataaberturaos Abertura,
       a.Numeroos OS,
       j.Codfunc || '-' || j.nomefunc Motorista,
       d.prefixoveic Carro,
       df.descricaodefeito || '-' || b.Descrcomplosprev Defeito,
       i.codfunc || '-' || i.nomefunc Mecanico,
       Decode(a.codorigos, 2, 'RA', 3, 'SOS') Origem,
       c.CodigoCadServi CodigoServicoOS,
       d.codigoveic, ol.Leve, op.Pesada
	From Man_Os a,
			 Man_Osprevisto b,
			 Man_Osrealizado c,
			 Frt_Cadveiculos d,
			 Flp_Funcionarios j,
			 Man_Osfuncionarios h,
			 Flp_Funcionarios i,
			 Flp_Parametros s,
       Man_Caddefeitos Df,
       
       (Select Max(m.Dataaberturaos) Leve, v.codigoveic
          From Man_Os m, Frt_Cadveiculos v, Man_Osrealizado r
         Where v.Codigoveic = m.Codigoveic
           And r.Codintos = m.Codintos
           And m.Tipoos = 'P'
           And m.Condicaoos Not In ('AB')
           And r.Codigoplanrev = 111
           And v.Condicaoveic = 'A'
           And m.Codigoempresa = 1
           And m.Codigofl = 1
         Group By v.codigoveic) OL,
       
       (Select Max(m.Dataaberturaos) Pesada, v.codigoveic
          From Man_Os m, Frt_Cadveiculos v, Man_Osrealizado r
         Where v.Codigoveic = m.Codigoveic
           And r.Codintos = m.Codintos
           And m.Tipoos = 'P'
           And m.Condicaoos Not In ('AB')
           And r.Codigoplanrev = 112
           And v.Condicaoveic = 'A'
           And m.Codigoempresa = 1
           And m.Codigofl = 1
         Group By v.codigoveic) OP,
         
			 (Select Distinct a.Codintos,
												 h.Codintfunc,
												 h.Horafinalosfunc,
												 h.Horainicialosfunc,
												 Count(*) Contador
					 From Man_Os             a,
								Man_Osprevisto     b,
								Man_Osrealizado    c,
								Frt_Cadveiculos    d,
								Man_Osfuncionarios h,
								Flp_Funcionarios   j,
								Flp_Parametros     r
					Where a.Codintos = b.Codintos
						And b.Codintos = c.Codintos
						And b.Seqservosprev = c.Seqservosprev
						And c.Codintos = h.Codintos
						And c.Seqservosprev = h.Seqservosprev
						And c.Seqservosreal = h.Seqservosreal
						And a.Codigoveic = d.Codigoveic
						And h.Codintfunc = j.Codintfunc(+)
						And j.Codigoempresa = r.Codigoempresa(+)
						And a.Condicaoos In ('FP', 'FC', 'FE')
						And d.Apresentarelatorioveic = 'S'
						And a.Codigoempresa = 1 
            And a.Codigofl = 1
						And a.Dataaberturaos Between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))
					
					Group By a.Codintos,
									 h.Codintfunc,
									 h.Dataexecosfunc,
									 h.Horafinalosfunc,
									 h.Horainicialosfunc) Cont,
			 Flp_Parametros r,
			 Man_Planoderevisao Pl
 Where a.Codintos = Cont.Codintos
	 And i.Codintfunc = Cont.Codintfunc
	 And h.Horafinalosfunc = Cont.Horafinalosfunc
	 And h.Horainicialosfunc = Cont.Horainicialosfunc
	 And c.Codintos = h.Codintos(+)
	 And c.Seqservosreal = h.Seqservosreal(+)
	 And c.Seqservosprev = h.Seqservosprev(+)
	 And h.Codintfunc = i.Codintfunc(+)
	 And a.Servicointextos = 'I'
	 And i.Codigoempresa = s.Codigoempresa(+)
	 And a.Codintos = b.Codintos
	 And b.Seqservosprev = c.Seqservosprev
	 And b.Codintos = c.Codintos
	 And a.Codigoveic = d.Codigoveic
	 And a.Codintfunc = j.Codintfunc(+)
	 And j.Codigoempresa = r.Codigoempresa(+)
	 And c.Codigoplanrev = Pl.Codigoplanrev(+)
	 And a.Condicaoos In ('FP', 'FC', 'FE')
	 And d.Apresentarelatorioveic = 'S'
   And b.codigodefeito = df.codigodefeito
	 And a.Codigoempresa = 1 
   And a.Codigofl = 1
   And a.codorigos In (2,3)
   And d.condicaoveic = 'A'
   And op.CodigoVeic = d.CodigoVeic
   And ol.CodigoVeic = d.CodigoVeic
	 And a.dataaberturaos between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))
   )  a,

(Select Distinct b.Leve,
                b.Carro,
                f.Codfunc || '-' || f.Nomefunc Mecanico_Leve,
                r.CodigoCadServi CodigoServicoLeve, v.codigoveic  
  From Man_Os m,
       Frt_Cadveiculos v,
       Man_Osfuncionarios o,
       Flp_Funcionarios f,
       Man_Osrealizado r,
       (SELECT MAX(M.DATAABERTURAOS) LEVE, V.PREFIXOVEIC CARRO, v.codigoveic
          FROM   MAN_OS M, FRT_CADVEICULOS V, MAN_OSREALIZADO R
          WHERE  V.CODIGOVEIC = M.CODIGOVEIC
          AND    R.CODINTOS = M.CODINTOS
          AND    M.TIPOOS = 'P'
          AND    M.CONDICAOOS NOT IN ('AB')
          AND    R.CODIGOPLANREV = 111      
          AND    V.CONDICAOVEIC = 'A'   
          AND    M.CODIGOEMPRESA = 1 AND M.CODIGOFL = 1
          GROUP BY V.PREFIXOVEIC, v.codigoveic) b
 Where o.Codintos = m.Codintos
   And o.Codintfunc = f.Codintfunc
   And m.Codigoveic = v.Codigoveic
   And b.Leve = m.Dataaberturaos
   And b.Codigoveic = v.Codigoveic
   And r.Codintos = m.Codintos
   And m.Tipoos = 'P'
   And m.Condicaoos Not In ('AB')
   And r.Codigoplanrev = 111
   And v.Condicaoveic = 'A'
   And m.Codigoempresa = 1
   And m.Codigofl = 1 ) B,        

----------------------------------------
(
Select Distinct c.Pesada,
                c.Carro,
                f.Codfunc || '-' || f.Nomefunc Mecanico_Pesado,
                r.CodigoCadServi CodigoServicoPesado, v.codigoveic
  From Man_Os m,
       Frt_Cadveiculos v,
       Man_Osfuncionarios o,
       Flp_Funcionarios f,
       Man_Osrealizado r,
       (Select Max(m.Dataaberturaos) Pesada, v.codigoveic, v.Prefixoveic Carro
          From Man_Os m, Frt_Cadveiculos v, Man_Osrealizado r
         Where v.Codigoveic = m.Codigoveic
           And r.Codintos = m.Codintos
           And m.Tipoos = 'P'
           And m.Condicaoos Not In ('AB')
           And r.Codigoplanrev = 112
           And v.Condicaoveic = 'A'
           And m.Codigoempresa = 1
           And m.Codigofl = 1
         Group By v.Prefixoveic, v.codigoveic) c
 Where o.Codintos = m.Codintos
   And o.Codintfunc = f.Codintfunc
   And m.Codigoveic = v.Codigoveic
   And c.Pesada = m.Dataaberturaos
   And c.CodigoVeic = v.CodigoVeic
   And r.Codintos = m.Codintos
   And m.Tipoos = 'P'
   And m.Condicaoos Not In ('AB')
   And r.Codigoplanrev = 112
   And v.Condicaoveic = 'A'
   And m.Codigoempresa = 1
   And m.Codigofl = 1) c

WHERE C.codigoveic(+) = A.codigoveic   
  And B.codigoveic(+) = A.Codigoveic
  And B.CodigoServicoLeve(+) = A.CodigoServicoOS
  And c.CodigoServicoPesado(+) = A.CodigoServicoOS
  And a.Abertura Between '01-may-2017' And '15-may-2017'
--and Os = 316619    

