Select a.Codigoempresa,
			 a.Codigofl,
			 a.Codigoga,
			 d.Codigoveic,
			 d.Prefixoveic,
			 w.Descricaomodchassi,
			 a.Kmexecucaoos,
			 c.Datainicioexecservosrea,
			 a.Codintos,
			 a.Numeroos,
			 a.Horaaberturaos,
			 a.Condicaoos,
			 a.Codorigos,
			 b.Codorigos Codorigdef,
			 c.Codigogrpservi,
			 q.Descricaogrpservi,
			 f.Codigocadservi,
			 f.Descricaocadservi,
			 c.Descrcomplosrea,
			 c.Codigoplanrev,
			 e.Codigotpoperservi,
			 e.Descricaotpoperservi,
			 d.Codigoempresa Codigoempresaveic,
			 d.Codigofl Codigoflveic,
			 d.Codigoga Codigogaveic,
			 j.Codfunc,
			 j.Chapafunc,
			 Decode(r.Trabchapapar, 'S', j.Chapafunc, j.Codfunc) Cod_Motorista,
			 j.Nomefunc Nome_Motorista,
			 c.Usuario Usuario,
			 o.Codigolinha,
			 o.Nomelinha,
			 c.Vlrmobraservosrea m_Externa,
			 c.Vlrpecasservosrea p_Externa,
			 a.Atuali_Inicial,
			 Pl.Tipohordiakmplanrev,
			 c.Seqservosprev,
			 c.Seqservosreal,
			 a.Servicointextos,
			 a.Observacaoos,
			 Cont.Contador,
			 Decode(r.Trabchapapar, 'S', i.Chapafunc, i.Codfunc) Chapa,
			 i.Nomefunc Nome,
			 h.Valormoosfunc m_Interna,
			 h.Horafinalosfunc,
			 h.Horainicialosfunc,
			 (h.Tothorasosfunc * 60) +
			 ((((h.Tothorasosfunc * 60) +
			 Decode(To_Char(h.Horainicialosfunc, 'SS'),
								 '00',
								 0,
								 Abs((To_Number(To_Char(h.Horainicialosfunc, 'SS')) - 60))) +
			 To_Number(To_Char(h.Horafinalosfunc, 'SS'))) -
			 (Trunc(((h.Tothorasosfunc * 60) +
								Decode(To_Char(h.Horainicialosfunc, 'SS'),
												'00',
												0,
												Abs((To_Number(To_Char(h.Horainicialosfunc, 'SS')) - 60))) +
								To_Number(To_Char(h.Horafinalosfunc, 'SS'))) / 3600) * 3600)) -
			 Trunc((((h.Tothorasosfunc * 60) +
							Decode(To_Char(h.Horainicialosfunc, 'SS'),
											 '00',
											 0,
											 Abs((To_Number(To_Char(h.Horainicialosfunc, 'SS')) - 60))) +
							To_Number(To_Char(h.Horafinalosfunc, 'SS'))) -
							(Trunc(((h.Tothorasosfunc * 60) +
											Decode(To_Char(h.Horainicialosfunc, 'SS'),
															'00',
															0,
															Abs((To_Number(To_Char(h.Horainicialosfunc,
																										 
																										 'SS')) - 60))) +
											To_Number(To_Char(h.Horafinalosfunc, 'SS'))) / 3600) * 3600)) / 60) * 60) As Tothorasosfunc,
			 b.Codigoplanrev,
			 b.Codigogrpdefeito,
			 b.Codigodefeito,
			 c.Codintnf
	From Man_Os a,
			 Man_Osprevisto b,
			 Man_Osrealizado c,
			 Frt_Cadveiculos d,
			 Man_Tipooperacaoservi e,
			 Man_Cadastrodeservicos f,
			 Flp_Funcionarios j,
			 Frt_Modchassi w,
			 Bgm_Cadlinhas o,
			 Man_Grupodeservico q,
			 Man_Osfuncionarios h,
			 Flp_Funcionarios i,
			 Flp_Parametros s,
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
						And
							 
								d.Apresentarelatorioveic = 'S'
						And ((a.Codigoempresa = 1 And a.Codigofl = 1) Or
								(a.Codigoempresa = 1 And a.Codigofl = 2) Or
								(a.Codigoempresa = 1 And a.Codigofl = 3))
						And c.Datainicioexecservosrea Between To_Date('01/05/2017 
00:00:00',
																													'DD/MM/YYYY 
HH24:MI:SS') /* P_DATAINI */
								And To_Date('18/05/2017 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /* 
               P_DATAFIN */
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
	 And c.Codigotpoperservi = e.Codigotpoperservi(+)
	 And d.Codigomodchassi = w.Codigomodchassi(+)
	 And c.Codigogrpservi = f.Codigogrpservi
	 And c.Codigocadservi = f.Codigocadservi
	 And a.Codintfunc = j.Codintfunc(+)
	 And a.Codintlinha = o.Codintlinha(+)
	 And c.Codigogrpservi = q.Codigogrpservi
	 And j.Codigoempresa = r.Codigoempresa(+)
	 And c.Codigoplanrev = Pl.Codigoplanrev(+)
	 And a.Condicaoos In ('FP', 'FC', 'FE')
	 And d.Apresentarelatorioveic = 'S'
	 And ((a.Codigoempresa = 1 And a.Codigofl = 1) Or
			 (a.Codigoempresa = 1 And a.Codigofl = 2) Or
			 (a.Codigoempresa = 1 And a.Codigofl = 3))
	 And c.Datainicioexecservosrea Between To_Date('01/05/2017 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /* P_DATAINI */
			 And To_Date('18/05/2017 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /*       P_DATAFIN */
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
Union
Select a.Codigoempresa,
			 a.Codigofl,
			 a.Codigoga,
			 d.Codigoveic,
			 d.Prefixoveic,
			 w.Descricaomodchassi,
			 a.Kmexecucaoos,
			 c.Datainicioexecservosrea,
			 a.Codintos,
			 a.Numeroos,
			 a.Horaaberturaos,
			 a.Condicaoos,
			 a.Codorigos,
			 b.Codorigos Codorigdef,
			 c.Codigogrpservi,
			 q.Descricaogrpservi,
			 f.Codigocadservi,
			 f.Descricaocadservi,
			 c.Descrcomplosrea,
			 c.Codigoplanrev,
			 e.Codigotpoperservi,
			 e.Descricaotpoperservi,
			 d.Codigoempresa Codigoempresaveic,
			 d.Codigofl Codigoflveic,
			 d.Codigoga Codigogaveic,
			 j.Codfunc,
			 j.Chapafunc,
			 Decode(r.Trabchapapar, 'S', j.Chapafunc, j.Codfunc) Cod_Motorista,
			 j.Nomefunc Nome_Motorista,
			 c.Usuario Usuario,
			 o.Codigolinha,
			 o.Nomelinha,
			 c.Vlrmobraservosrea m_Externa,
			 c.Vlrpecasservosrea p_Externa,
			 a.Atuali_Inicial,
			 Pl.Tipohordiakmplanrev,
			 c.Seqservosprev,
			 c.Seqservosreal,
			 a.Servicointextos,
			 a.Observacaoos,
			 1 Contador,
			 '' Chapa,
			 '' Nome,
			 0 m_Interna,
			 To_Date('31/12/1899', 'DD/MM/YYYY') Horafinalosfunc,
			 To_Date('31/12/1899', 'DD/MM/YYYY') Horainicialosfunc,
			 0 Tothorasosfunc,
			 b.Codigoplanrev,
			 b.Codigogrpdefeito,
			 b.Codigodefeito,
			 c.Codintnf
	From Man_Os                 a,
			 Man_Osprevisto         b,
			 Man_Osrealizado        c,
			 Frt_Cadveiculos        d,
			 Man_Tipooperacaoservi  e,
			 Man_Cadastrodeservicos f,
			 Flp_Funcionarios       j,
			 Frt_Modchassi          w,
			 Bgm_Cadlinhas          o,
			 Man_Grupodeservico     q,
			 Flp_Parametros         r,
			 Man_Planoderevisao     Pl
 Where a.Servicointextos = 'E'
	 And a.Codintos = b.Codintos
	 And b.Seqservosprev = c.Seqservosprev
	 And b.Codintos = c.Codintos
	 And a.Codigoveic = d.Codigoveic
	 And c.Codigotpoperservi = e.Codigotpoperservi(+)
	 And d.Codigomodchassi = w.Codigomodchassi(+)
	 And c.Codigogrpservi = f.Codigogrpservi
	 And c.Codigocadservi = f.Codigocadservi
	 And a.Codintfunc = j.Codintfunc(+)
	 And a.Codintlinha = o.Codintlinha(+)
	 And c.Codigogrpservi = q.Codigogrpservi
	 And j.Codigoempresa = r.Codigoempresa(+)
	 And c.Codigoplanrev = Pl.Codigoplanrev(+)
	 And a.Condicaoos In ('FP', 'FC', 'FE')
	 And d.Apresentarelatorioveic = 'S'
	 And ((a.Codigoempresa = 1 And a.Codigofl = 1) Or
			 (a.Codigoempresa = 1 And a.Codigofl = 2) Or
			 (a.Codigoempresa = 1 And a.Codigofl = 3))
	 And c.Datainicioexecservosrea Between To_Date('01/05/2017 
00:00:00',
																								 'DD/MM/YYYY 
HH24:MI:SS') /* P_DATAINI */
			 And To_Date('18/05/2017 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /* 
      P_DATAFIN */
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
Union
Select a.Codigoempresa,
			 a.Codigofl,
			 a.Codigoga,
			 d.Codigoveic,
			 d.Prefixoveic,
			 w.Descricaomodchassi,
			 a.Kmexecucaoos,
			 c.Datainicioexecservosrea,
			 a.Codintos,
			 a.Numeroos,
			 a.Horaaberturaos,
			 a.Condicaoos,
			 a.Codorigos,
			 b.Codorigos Codorigdef,
			 c.Codigogrpservi,
			 q.Descricaogrpservi,
			 f.Codigocadservi,
			 f.Descricaocadservi,
			 c.Descrcomplosrea,
			 c.Codigoplanrev,
			 e.Codigotpoperservi,
			 e.Descricaotpoperservi,
			 d.Codigoempresa Codigoempresaveic,
			 d.Codigofl Codigoflveic,
			 d.Codigoga Codigogaveic,
			 j.Codproaut,
			 j.Codproaut,
			 Decode(r.Trabchapapar, 'S', j.Codproaut, j.Codproaut) Cod_Motorista,
			 j.Nomeproaut Nome_Motorista,
			 c.Usuario Usuario,
			 o.Codigolinha,
			 o.Nomelinha,
			 c.Vlrmobraservosrea m_Externa,
			 c.Vlrpecasservosrea p_Externa,
			 a.Atuali_Inicial,
			 Pl.Tipohordiakmplanrev,
			 c.Seqservosprev,
			 c.Seqservosreal,
			 a.Servicointextos,
			 a.Observacaoos,
			 Cont.Contador,
			 Decode(r.Trabchapapar, 'S', i.Codproaut, i.Codproaut) Chapa,
			 i.Nomeproaut Nome,
			 h.Valormoosfunc m_Interna,
			 h.Horafinalosfunc,
			 h.Horainicialosfunc,
			 (h.Tothorasosfunc * 60) +
			 ((((h.Tothorasosfunc * 60) +
			 Decode(To_Char(h.Horainicialosfunc, 'SS'),
								 '00',
								 0,
								 Abs((To_Number(To_Char(h.Horainicialosfunc, 'SS')) - 60))) +
			 To_Number(To_Char(h.Horafinalosfunc, 'SS'))) -
			 (Trunc(((h.Tothorasosfunc * 60) +
								Decode(To_Char(h.Horainicialosfunc, 'SS'),
												'00',
												0,
												Abs((To_Number(To_Char(h.Horainicialosfunc, 'SS')) - 60))) +
								To_Number(To_Char(h.Horafinalosfunc, 'SS'))) / 3600) * 3600)) -
			 Trunc((((h.Tothorasosfunc * 60) +
							Decode(To_Char(h.Horainicialosfunc, 'SS'),
											 '00',
											 0,
											 Abs((To_Number(To_Char(h.Horainicialosfunc, 'SS')) - 60))) +
							To_Number(To_Char(h.Horafinalosfunc, 'SS'))) -
							(Trunc(((h.Tothorasosfunc * 60) +
											Decode(To_Char(h.Horainicialosfunc, 'SS'),
															'00',
															0,
															Abs((To_Number(To_Char(h.Horainicialosfunc,
																										 
																										 'SS')) - 60))) +
											To_Number(To_Char(h.Horafinalosfunc, 'SS'))) / 3600) * 3600)) / 60) * 60) As Tothorasosfunc,
			 b.Codigoplanrev,
			 b.Codigogrpdefeito,
			 b.Codigodefeito,
			 c.Codintnf
	From Man_Os a,
			 Man_Osprevisto b,
			 Man_Osrealizado c,
			 Frt_Cadveiculos d,
			 Man_Tipooperacaoservi e,
			 Man_Cadastrodeservicos f,
			 Flp_Proautonomos j,
			 Frt_Modchassi w,
			 Bgm_Cadlinhas o,
			 Man_Grupodeservico q,
			 Man_Osfuncionarios h,
			 Flp_Proautonomos i,
			 Flp_Parametros s,
			 (Select Distinct a.Codintos,
												 h.Codintproaut,
												 h.Horafinalosfunc,
												 h.Horainicialosfunc,
												 Count(*) Contador
					 From Man_Os             a,
								Man_Osprevisto     b,
								Man_Osrealizado    c,
								Frt_Cadveiculos    d,
								Man_Osfuncionarios h,
								Flp_Proautonomos   j,
								Flp_Parametros     r
					Where a.Codintos = b.Codintos
						And b.Codintos = c.Codintos
						And b.Seqservosprev = c.Seqservosprev
						And c.Codintos = h.Codintos
						And c.Seqservosprev = h.Seqservosprev
						And c.Seqservosreal = h.Seqservosreal
						And a.Codigoveic = d.Codigoveic
						And h.Codintproaut = j.Codintproaut(+)
						And j.Codigoempresa = r.Codigoempresa(+)
						And a.Condicaoos In ('FP', 'FC', 'FE')
						And
							 
								d.Apresentarelatorioveic = 'S'
						And ((a.Codigoempresa = 1 And a.Codigofl = 1) Or
								(a.Codigoempresa = 1 And a.Codigofl = 2) Or
								(a.Codigoempresa = 1 And a.Codigofl = 3))
						And c.Datainicioexecservosrea Between To_Date('01/05/2017 
00:00:00',
																													'DD/MM/YYYY 
HH24:MI:SS') /* P_DATAINI */
								And To_Date('18/05/2017 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /* 
               P_DATAFIN */
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
									 h.Codintproaut,
									 h.Dataexecosfunc,
									 h.Horafinalosfunc,
									 h.Horainicialosfunc) Cont,
			 Flp_Parametros r,
			 Man_Planoderevisao Pl
 Where a.Codintos = Cont.Codintos
	 And i.Codintproaut = Cont.Codintproaut
	 And h.Horafinalosfunc = Cont.Horafinalosfunc
	 And h.Horainicialosfunc = Cont.Horainicialosfunc
	 And c.Codintos = h.Codintos(+)
	 And c.Seqservosreal = h.Seqservosreal(+)
	 And c.Seqservosprev = h.Seqservosprev(+)
	 And h.Codintproaut = i.Codintproaut(+)
	 And a.Servicointextos = 'I'
	 And i.Codigoempresa = s.Codigoempresa(+)
	 And a.Codintos = b.Codintos
	 And b.Seqservosprev = c.Seqservosprev
	 And b.Codintos = c.Codintos
	 And a.Codigoveic = d.Codigoveic
	 And c.Codigotpoperservi = e.Codigotpoperservi(+)
	 And d.Codigomodchassi = w.Codigomodchassi(+)
	 And c.Codigogrpservi = f.Codigogrpservi
	 And c.Codigocadservi = f.Codigocadservi
	 And a.Codintproaut = j.Codintproaut(+)
	 And a.Codintlinha = o.Codintlinha(+)
	 And c.Codigogrpservi = q.Codigogrpservi
	 And j.Codigoempresa = r.Codigoempresa(+)
	 And c.Codigoplanrev = Pl.Codigoplanrev(+)
	 And a.Condicaoos In ('FP', 'FC', 'FE')
	 And d.Apresentarelatorioveic = 'S'
	 And ((a.Codigoempresa = 1 And a.Codigofl = 1) Or
			 (a.Codigoempresa = 1 And a.Codigofl = 2) Or
			 (a.Codigoempresa = 1 And a.Codigofl = 3))
	 And c.Datainicioexecservosrea Between To_Date('01/05/2017 
00:00:00',
																								 'DD/MM/YYYY 
HH24:MI:SS') /* P_DATAINI */
			 And To_Date('18/05/2017 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /* 
      P_DATAFIN */
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
Union
Select a.Codigoempresa,
			 a.Codigofl,
			 a.Codigoga,
			 d.Codigoveic,
			 d.Prefixoveic,
			 w.Descricaomodchassi,
			 a.Kmexecucaoos,
			 c.Datainicioexecservosrea,
			 a.Codintos,
			 a.Numeroos,
			 a.Horaaberturaos,
			 a.Condicaoos,
			 a.Codorigos,
			 b.Codorigos Codorigdef,
			 c.Codigogrpservi,
			 q.Descricaogrpservi,
			 f.Codigocadservi,
			 f.Descricaocadservi,
			 c.Descrcomplosrea,
			 c.Codigoplanrev,
			 e.Codigotpoperservi,
			 e.Descricaotpoperservi,
			 d.Codigoempresa Codigoempresaveic,
			 d.Codigofl Codigoflveic,
			 d.Codigoga Codigogaveic,
			 j.Codproaut,
			 j.Codproaut,
			 Decode(r.Trabchapapar, 'S', j.Codproaut, j.Codproaut) Cod_Motorista,
			 j.Nomeproaut Nome_Motorista,
			 c.Usuario Usuario,
			 o.Codigolinha,
			 o.Nomelinha,
			 c.Vlrmobraservosrea m_Externa,
			 c.Vlrpecasservosrea p_Externa,
			 a.Atuali_Inicial,
			 Pl.Tipohordiakmplanrev,
			 c.Seqservosprev,
			 c.Seqservosreal,
			 a.Servicointextos,
			 a.Observacaoos,
			 1 Contador,
			 '' Chapa,
			 '' Nome,
			 0 m_Interna,
			 To_Date('31/12/1899', 'DD/MM/YYYY') Horafinalosfunc,
			 To_Date('31/12/1899', 'DD/MM/YYYY') Horainicialosfunc,
			 0 Tothorasosfunc,
			 b.Codigoplanrev,
			 b.Codigogrpdefeito,
			 b.Codigodefeito,
			 c.Codintnf
	From Man_Os                 a,
			 Man_Osprevisto         b,
			 Man_Osrealizado        c,
			 Frt_Cadveiculos        d,
			 Man_Tipooperacaoservi  e,
			 Man_Cadastrodeservicos f,
			 Flp_Proautonomos       j,
			 Frt_Modchassi          w,
			 Bgm_Cadlinhas          o,
			 Man_Grupodeservico     q,
			 Flp_Parametros         r,
			 Man_Planoderevisao     Pl
 Where a.Servicointextos = 'E'
	 And a.Codintos = b.Codintos
	 And b.Seqservosprev = c.Seqservosprev
	 And b.Codintos = c.Codintos
	 And a.Codigoveic = d.Codigoveic
	 And c.Codigotpoperservi = e.Codigotpoperservi(+)
	 And d.Codigomodchassi = w.Codigomodchassi(+)
	 And c.Codigogrpservi = f.Codigogrpservi
	 And c.Codigocadservi = f.Codigocadservi
	 And a.Codintproaut = j.Codintproaut(+)
	 And a.Codintlinha = o.Codintlinha(+)
	 And c.Codigogrpservi = q.Codigogrpservi
	 And j.Codigoempresa = r.Codigoempresa(+)
	 And c.Codigoplanrev = Pl.Codigoplanrev(+)
	 And a.Condicaoos In ('FP', 'FC', 'FE')
	 And d.Apresentarelatorioveic = 'S'
	 And ((a.Codigoempresa = 1 And a.Codigofl = 1) Or
			 (a.Codigoempresa = 1 And a.Codigofl = 2) Or
			 (a.Codigoempresa = 1 And a.Codigofl = 3))
	 And c.Datainicioexecservosrea Between To_Date('01/05/2017 
00:00:00',
																								 'DD/MM/YYYY 
HH24:MI:SS') /* P_DATAINI */
			 And To_Date('18/05/2017 00:00:00', 'DD/MM/YYYY HH24:MI:SS') /* 
      P_DATAFIN */
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