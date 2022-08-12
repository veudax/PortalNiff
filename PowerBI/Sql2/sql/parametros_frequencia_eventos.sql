Select codigoempresa, descricao, codparam From (
Select 'Função' tipo, f.codfuncao || '-' || f.descfuncao descricao, d.codparam, d.codigoempresa
  From Frq_Paramdestino d, flp_funcao f
 Where d.tipoparam = 7
   And f.codfuncao = d.codidentparam
   And d.Codparam In (Select Distinct x.Codparam
                        From Frq_Parameventos x
                       Where x.Codparam Not In
                             (Select Distinct p.Codparam
                                From Frq_Parameventos p
                               Where p.Tipodecalculoparameve In
                                     (10, 11, 12, 13, 110, 111, 112, 113, 210, 211, 212, 213, 410, 411, 412, 413, 510, 511, 512, 513, 711, 712, 713, 714, 811, 812, 813, 814)))
Union All
Select 'Funcionário' tipo, f.Codfunc || '-' || f.nomefunc, d.codparam, d.codigoempresa
  From Frq_Paramdestino d, flp_funcionarios f
 Where d.tipoparam = 9
   And f.CodintFunc = d.codidentparam
   And d.Codparam In (Select Distinct x.Codparam
                        From Frq_Parameventos x
                       Where x.Codparam Not In
                             (Select Distinct p.Codparam
                                From Frq_Parameventos p
                               Where p.Tipodecalculoparameve In
                                     (10, 11, 12, 13, 110, 111, 112, 113, 210, 211, 212, 213, 410, 411, 412, 413, 510, 511, 512, 513, 711, 712, 713, 714, 811, 812, 813, 814)))
) 
Where tipo = 'Funcionário'
Order By CodigoEmpresa, Tipo, CodParam, Descricao
                                     