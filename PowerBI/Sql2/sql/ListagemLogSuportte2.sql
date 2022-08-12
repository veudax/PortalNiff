Select Count( Distinct u.nome), u.nome, e.nomeabreviado, u.email, u.nomemaquina--, Count(*), trunc(Data)
  From niff_chm_log l, niff_chm_usuarios u, niff_chm_empresas e
Where trunc(Data) Between '01-feb-2019' And '14-feb-2019'
-- Where trunc(Data) = trunc(Sysdate)
 And u.Idusuario = l.Idusuario
 And u.idempresa = e.idempresa
 And tela Like '%login'
 Group By e.Nomeabreviado, u.nome,  u.email, u.nomemaquina--, trunc(Data)
