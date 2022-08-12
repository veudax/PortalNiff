Insert Into flp_autoriza_usu_eve (usuario, codevento)
select 'FRFARIAS', t.codevento 
 from flp_autoriza_usu_eve   t
Where t.usuario = 'ELUGATO' 
  And t.codevento Not In (Select codevento From flp_autoriza_usu_eve Where usuario = 'FRFARIAS')
  And t.codevento In (480, 451, 453)