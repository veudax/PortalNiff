Select Distinct a.Codintos,
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
   And a.codorigos in (2,3)
	 And d.Apresentarelatorioveic = 'S'
	 And a.Codigoempresa = 1 
   And a.Codigofl = 1
	 And c.Datainicioexecservosrea Between (ADD_MONTHS(LAST_DAY(TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))+1, -1)) and (TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY'),'DD/MM/YYYY'))
	 And a.Codigoveic In (Select Codigoveic
													From Frt_Cadveiculos
												 Where Prefixoveic Between '0030441'
															/* P_VEICINI */
															 And '0030441' /* 
                                                         P_VEICFIN */
												)
			
	 And a.Codintos In (Select Codintos
												From Man_Os
											 Where Numeroos Between 316619 /* P_OSINI */
														 And 316619
											/* P_OSFIN */
											)
 Group By a.Codintos,
					h.Codintfunc,
					h.Dataexecosfunc,
					h.Horafinalosfunc,
					h.Horainicialosfunc