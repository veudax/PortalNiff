Select Distinct f.codIntfunc, CodFunc, nomeFunc, f.DESCFUNCAO, decode(f.TPSALFUNCAO, 'M', f.SALBASE, f.SALBASE * 
       Case When f.CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                            571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                            692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913) Then    
           decode(f.JORNADAFUNC,7.20, 220, 155) Else 180 End ) Salario, Nvl((30- Nvl(r.Diasgozofer,0)),0) DiasTrabalhados
     , decode(Nvl(r.Diasgozofer,0), 0, 'Ativo', 'Tem Férias calculadade de ' || r.Diasgozofer || ' dias') Status
  From Vw_Funcionarios f
     , (Select fr.CodIntFunc, fr.Diasgozofer
          From Flp_Ferias fr
         Where (fr.Gozoinifer Between '01-aug-2020' And '31-aug-2020'
           Or fr.gozofinfer Between '01-aug-2020' And '31-aug-2020')
          And  fr.statusferias = 'N' ) r
 Where f.CODIGOEMPRESA = 9
   And f.CODIGOFL = 2
   And f.SITUACAOFUNC = 'A'
   And f.CODINTFUNC = r.codintfunc(+)
/*Union All  
Select f.codIntfunc, CodFunc, nomeFunc, f.DESCFUNCAO,  decode(f.TPSALFUNCAO, 'M', f.SALBASE, f.SALBASE * 
       Case When f.CODFUNCAO Not In (18, 58, 62, 377, 483, 484, 485, 518, 519, 550, 556, 564, 565,
                                            571, 574, 575, 614, 615, 616, 617, 622, 625, 671, 678, 686,
                                            692, 787, 794, 813, 838, 846 ,861, 880, 894, 906, 913) Then    
           decode(f.JORNADAFUNC,7.20, 220, 155) Else 180 End ) Salario, nvl(QrdDiasTrabalhados,0)
     , 'Afastado'             
 From Vw_Funcionarios f 
    , (Select codIntFunc
     , Case When af.dtretafast Is Null Then
          Case When af.dtafast > '01-aug-2020' Then
            af.dtafast - To_Date('01-aug-2020')
          Else
            To_date('31-aug-2020') - af.dtafast 
          End
       Else 
          Case When af.dtafast > '01-aug-2020' Then
              (af.dtafast - To_Date('01-aug-2020')) + (To_date('31-aug-2020') - af.dtretafast)
          Else
           0
           End End QrdDiasTrabalhados
      
  From Flp_Afastados af
 Where (af.dtafast Between '01-aug-2020' And '31-aug-2020' 
    Or af.dtretafast Between '01-aug-2020' And '31-aug-2020') ) a   
 Where f.CODIGOEMPRESA = 2
   And f.CODIGOFL = 1
   And f.SITUACAOFUNC = 'F'
   And f.CODINTFUNC = a.codintfunc(+)
*/