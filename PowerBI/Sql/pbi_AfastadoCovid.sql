create or replace view pbi_afastadocovid as
Select f.CODFUNC, f.CHAPAFUNC, f.NOMEFUNC, f.DTNASCTOFUNC, f.DTADMFUNC, f.DESCFUNCAO, f.SALBASE, f.DTAPOSENTFUNC, f.codintfunc
     , Decode(f.SITUACAOFUNC, 'A', 'Ativo', 'Afastado') situacao
     , Max(InicioAfast) InicioAfast
     , (Case When Max(RetornoAfast) < Max(InicioAfast) then Null Else Max(RetornoAfast) End) As RetornoAfastamento
     , obsafast1, a.nrbeneficio
     , Max(r.aquiinifer) aquiinifer , max(r.aquifinfer) aquifinfer
     , trunc((months_between(sysdate, DTNASCTOFUNC))/12) AS idade
     , To_date('31/05/2020','dd/mm/yyyy') dtCompentencia
     , decode( a.nrbeneficio, null, decode( max(r.aquifinfer), Null
             , trunc((months_between('31-may-2020',DTADMFUNC )))
             , trunc((months_between('31-may-2020',max(r.aquifinfer) )))),0) Avos
     , decode(f.TPSALFUNCAO, 'M', f.SALBASE, f.SALBASE *
       Case When f.CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                            571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                            692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913) Then
           decode(f.JORNADAFUNC,7.20, 220, 155) Else 180 End ) Salario,a .codcondi
     , Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(Decode(f.Codigoempresa, 9, Decode(f.Codigofl, 2, 1, f.Codigofl), f.Codigofl), 3, '0') EmpFil
     , f.DESCAREA, desccondi
     , Decode(f.SITUACAOFUNC, 'A', 1, 0) QdtAtivo
     , Decode(f.SITUACAOFUNC, 'F', 1, 0) QdtAfatsado
  From vw_funcionarios f
     , (Select s.codintfunc, Max(s.aquiinifer) aquiinifer, max(s.aquifinfer) aquifinfer
          From Flp_Ferias s
         Where s.aquiinifer = (Select Max(aquiinifer) From Flp_Ferias Where codintfunc = s.codintfunc And USUEXCLFERIAS Is Null)
           And USUEXCLFERIAS Is Null
         Group By s.codintfunc) r
     , (Select a.obsafast1, a.codintfunc, Max(a.dtretafast) RetornoAfast, Max(a.dtafast) InicioAfast, a.nrbeneficio,a.codcondi, c.desccondi
          From flp_afastados a, flp_condicao c
         Where a.dtafast = (Select Max(dtafast) From flp_afastados Where codintfunc = a.codintfunc)
           And a.codcondi = c.codcondi
         Group By a.obsafast1, a.codintfunc, a.nrbeneficio,a .codcondi, c.desccondi) a
     --, flp_ferias r
 Where f.CODINTFUNC = a.codintfunc(+)
   And f.CODINTFUNC = r.CODINTFUNC(+)
   And f.SITUACAOFUNC <> 'D'
   And f.codigoempresa < 100
 Group By f.CODFUNC, f.CHAPAFUNC, f.NOMEFUNC, f.DTNASCTOFUNC, f.DTADMFUNC, f.DESCFUNCAO, f.SALBASE, f.DTAPOSENTFUNC
     , f.SITUACAOFUNC
     , obsafast1

, f.codintfunc, a.nrbeneficio,a .codcondi
, Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(Decode(f.Codigoempresa, 9, Decode(f.Codigofl, 2, 1, f.Codigofl), f.Codigofl), 3, '0')
     , f.DESCAREA, f.TPSALFUNCAO, desccondi,  f.JORNADAFUNC, f.CODFUNCAO

