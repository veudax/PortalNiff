Update flp_funcionarios_complementar
   Set tipojornada_esocial = 3,
       desctipojornada_esocial = 'Revezamento' 
 Where codintfunc In (       
Select f.codintfunc 
  From flp_funcionarios_complementar c,
       flp_funcionarios f
 Where f.situacaofunc <> 'D'
--   And c.desctipojornada_esocial = 'Revezamento'
   And c.tipojornada_esocial  Is Null
   And c.codintfunc = f.codintfunc)