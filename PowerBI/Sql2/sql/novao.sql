Select * From (
Select d.codigoempresa, f.codfunc || '-' || f.NOMEFUNC Funcionario, d.codparam || '-' || p.descparam param, 
       f.CODFUNCAO || '-' || f.DESCFUNCAO funcao, 'Parametrizado pela função'
  From Frq_Paramdestino d, Vw_Funcionarios f, Frq_Parametros p
 Where d.tipoparam = 7
   And f.codfuncao = d.codidentparam
   And p.codparam = d.codparam
   And d.codigoempresa = f.CODIGOEMPRESA
   And d.Codparam In (Select Distinct x.Codparam
                        From Frq_Parameventos x
                       Where x.Codparam Not In
                             (Select Distinct p.Codparam
                                From Frq_Parameventos p
                               Where p.Tipodecalculoparameve In
                                     (10, 11, 12, 13, 110, 111, 112, 113, 210, 211, 212, 213, 410, 411, 412, 413, 510, 511, 512, 513, 711, 712, 713, 714, 811, 812, 813, 814)))
 Union All
Select d.codigoempresa, f.codfunc || '-' || f.NOMEFUNC Funcionario, d.codparam || '-' || p.descparam param, 
       f.CODFUNCAO || '-' || f.DESCFUNCAO funcao, 'Parametrizado pelo funcionário'
  From Frq_Paramdestino d, Vw_Funcionarios f, Frq_Parametros p
 Where d.tipoparam = 9
   And f.CODINTFUNC = d.codidentparam
   And p.codparam = d.codparam
   And d.codigoempresa = f.CODIGOEMPRESA
   And d.Codparam In (Select Distinct x.Codparam
                        From Frq_Parameventos x
                       Where x.Codparam Not In
                             (Select Distinct p.Codparam
                                From Frq_Parameventos p
                               Where p.Tipodecalculoparameve In
                                     (10, 11, 12, 13, 110, 111, 112, 113, 210, 211, 212, 213, 410, 411, 412, 413, 510, 511, 512, 513, 711, 712, 713, 714, 811, 812, 813, 814)))
)                                     
Where codigoempresa = 26
Order By codigoempresa, funcionario, Param