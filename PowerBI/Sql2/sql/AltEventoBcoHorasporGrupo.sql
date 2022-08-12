Select h.*, Rowid From frq_bancohoras_even h
Where codintfunc In (Select g.codintfunc From Vw_Funcionarios f, Flp_Funcionarios_Grupo g
                      Where codigoempresa =3
                        And f.codintfunc = g.codintfunc
                        And codgrupo In (44,46,74) )
  And codevento = 721                        
And competencia = '01-oct-2020'