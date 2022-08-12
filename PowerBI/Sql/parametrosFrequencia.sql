select Distinct cód_empresa,  CodParam, DescParam
  from (
select  decode(pd.codigoempresa, '1', 'VILA GALVÃO',
                                '2', 'ABC TRANSPORTES',
                                '3', 'RÁPIDO DOESTE',
                                '4', 'CISNE BRANCO',
                                '5', 'NIFF HOLDING',
                                '6', 'ARUJÁ',
                                '9', 'CAMPIBUS',
                                '13', 'RIBE TRANSPORTES',
                                '26', 'VIAÇÃO URBANA') Cód_EMPRESA,
       'Empresa' Agrup_por,
        to_char(pd.codidentparam) relacao,
        decode(pd.codigoempresa, '1', 'VILA GALVÃO',
                                 '2', 'ABC TRANSPORTES',
                                 '3', 'RÁPIDO DOESTE',
                                 '4', 'CISNE BRANCO',
                                 '5', 'NIFF HOLDING',
                                 '6', 'ARUJÁ',
                                 '9', 'CAMPIBUS',
                                 '13', 'RIBE TRANSPORTES',
                                 '26', 'VIAÇÃO URBANA') nome,
        pa.codparam,
        pa.descparam
  from frq_parametros pa, frq_paramdestino pd
 where pa.codparam = pd.codparam
   and pd.tipoparam = 1
   and pd.codigoempresa = 1
  
  
Union All

select decode(pd.codigoempresa, '1', 'VILA GALVÃO',
                                '2', 'ABC TRANSPORTES',
                                '3', 'RÁPIDO DOESTE',
                                '4', 'CISNE BRANCO',
                                '5', 'NIFF HOLDING',
                                '6', 'ARUJÁ',
                                '9', 'CAMPIBUS',
                                '13', 'RIBE TRANSPORTES',
                                '26', 'VIAÇÃO URBANA') Cód_EMPRESA,
       'Filial' Agrup_por,
        to_char(pd.codidentparam) relacao,
        decode(pd.codigoempresa, '1', 'VILA GALVÃO',
                                 '2', 'ABC TRANSPORTES',
                                 '3', 'RÁPIDO DOESTE',
                                 '4', 'CISNE BRANCO',
                                 '5', 'NIFF HOLDING',
                                 '6', 'ARUJÁ',
                                 '9', 'CAMPIBUS',
                                 '13', 'RIBE TRANSPORTES',
                                 '26', 'VIAÇÃO URBANA') nome,
        pa.codparam,
        pa.descparam
  from frq_parametros pa, frq_paramdestino pd
 where pa.codparam = pd.codparam
   and pd.tipoparam = 2
   and pd.codigoempresa = 1
   

Union All

select decode(pd.codigoempresa, '1', 'VILA GALVÃO',
                                '2', 'ABC TRANSPORTES',
                                '3', 'RÁPIDO DOESTE',
                                '4', 'CISNE BRANCO',
                                '5', 'NIFF HOLDING',
                                '6', 'ARUJÁ',
                                '9', 'CAMPIBUS',
                                '13', 'RIBE TRANSPORTES',
                                '26', 'VIAÇÃO URBANA') Cód_EMPRESA,
       'Área' Agrup_por,
        to_char(pd.codidentparam) relacao,
        ar.descarea nome,
        pa.codparam,
        pa.descparam
  from frq_parametros pa, frq_paramdestino pd, flp_area ar
 where pa.codparam = pd.codparam
   and pd.tipoparam = 1
   and pd.codidentparam = ar.codarea
   and pd.codigoempresa = 1
 

Union All

select decode(pd.codigoempresa, '1', 'VILA GALVÃO',
                                '2', 'ABC TRANSPORTES',
                                '3', 'RÁPIDO DOESTE',
                                '4', 'CISNE BRANCO',
                                '5', 'NIFF HOLDING',
                                '6', 'ARUJÁ',
                                '9', 'CAMPIBUS',
                                '13', 'RIBE TRANSPORTES',
                                '26', 'VIAÇÃO URBANA') Cód_EMPRESA,
       'Depto' Agrup_por,
        to_char(pd.codidentparam) relacao,
        de.descdepto nome,
        pa.codparam,
        pa.descparam
  from frq_parametros pa, frq_paramdestino pd, flp_depto de
 where pa.codparam = pd.codparam
   and pd.tipoparam = 4
   and pd.codidentparam = de.coddepto
   and pd.codigoempresa = 1
 

Union All

select decode(pd.codigoempresa, '1', 'VILA GALVÃO',
                                '2', 'ABC TRANSPORTES',
                                '3', 'RÁPIDO DOESTE',
                                '4', 'CISNE BRANCO',
                                '5', 'NIFF HOLDING',
                                '6', 'ARUJÁ',
                                '9', 'CAMPIBUS',
                                '13', 'RIBE TRANSPORTES',
                                '26', 'VIAÇÃO URBANA') Cód_EMPRESA,
       'Setor' Agrup_por,
        to_char(pd.codidentparam) relacao,
        sc.descsecao nome,
        pa.codparam,
        pa.descparam
  from frq_parametros pa, frq_paramdestino pd, flp_secao sc
 where pa.codparam = pd.codparam
   and pd.tipoparam = 5
   and pd.codidentparam = sc.codsecao
   and pd.codigoempresa = 1
   
   
Union All

select decode(pd.codigoempresa, '1', 'VILA GALVÃO',
                                '2', 'ABC TRANSPORTES',
                                '3', 'RÁPIDO DOESTE',
                                '4', 'CISNE BRANCO',
                                '5', 'NIFF HOLDING',
                                '6', 'ARUJÁ',
                                '9', 'CAMPIBUS',
                                '13', 'RIBE TRANSPORTES',
                                '26', 'VIAÇÃO URBANA') Cód_EMPRESA,
       'Seção' Agrup_por,
       to_char(pd.codidentparam) relacao,
        '' nome,
        pa.codparam,
        pa.descparam
  from frq_parametros pa, frq_paramdestino pd
 where pa.codparam = pd.codparam
   and pd.tipoparam = 6
   and pd.codigoempresa = 1
 

Union All

select decode(pd.codigoempresa, '1', 'VILA GALVÃO',
                                '2', 'ABC TRANSPORTES',
                                '3', 'RÁPIDO DOESTE',
                                '4', 'CISNE BRANCO',
                                '5', 'NIFF HOLDING',
                                '6', 'ARUJÁ',
                                '9', 'CAMPIBUS',
                                '13', 'RIBE TRANSPORTES',
                                '26', 'VIAÇÃO URBANA') Cód_EMPRESA,
       'Função' Agrup_por,
        to_char(pd.codidentparam) relacao,
        fc.descfuncao nome,
        pa.codparam,
        pa.descparam
  from frq_parametros pa, frq_paramdestino pd, flp_funcao fc
 where pa.codparam = pd.codparam
   and pd.tipoparam = 7
   and pd.codidentparam = fc.codfuncao
   and pd.codigoempresa = 1
 

Union All

select decode(pd.codigoempresa, '1', 'VILA GALVÃO',
                                '2', 'ABC TRANSPORTES',
                                '3', 'RÁPIDO DOESTE',
                                '4', 'CISNE BRANCO',
                                '5', 'NIFF HOLDING',
                                '6', 'ARUJÁ',
                                '9', 'CAMPIBUS',
                                '13', 'RIBE TRANSPORTES',
                                '26', 'VIAÇÃO URBANA') Cód_EMPRESA,
       'Sindicato' Agrup_por,
        to_char(pd.codidentparam) relacao,
        sd.descsindi nome,
        pa.codparam,
        pa.descparam
  from frq_parametros pa, frq_paramdestino pd, flp_sindicatos sd
 where pa.codparam = pd.codparam
   and pd.tipoparam = 8
   and pd.codidentparam = sd.codsindi
   and pd.codigoempresa = 1


Union All

select decode(pd.codigoempresa, '1', 'VILA GALVÃO',
                                '2', 'ABC TRANSPORTES',
                                '3', 'RÁPIDO DOESTE',
                                '4', 'CISNE BRANCO',
                                '5', 'NIFF HOLDING',
                                '6', 'ARUJÁ',
                                '9', 'CAMPIBUS',
                                '13', 'RIBE TRANSPORTES',
                                '26', 'VIAÇÃO URBANA') Cód_EMPRESA,
       'Funcionário' Agrup_por,
        fu.codfunc relacao,
        fu.nomefunc nome,
        pa.codparam,
        pa.descparam
  from frq_parametros pa, frq_paramdestino pd, flp_funcionarios fu
 where pa.codparam = pd.codparam
   and pd.tipoparam = 9
   and pd.codidentparam = fu.codintfunc
   and pd.codigoempresa = 1
   And pd.codigofl = 2)
   
order by 2,3
 
 