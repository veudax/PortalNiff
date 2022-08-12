Select * From 
(

Select fc_niff_longtovarchar(l.Idlog) descricao, l.data, l.tela, l.Idlog, u.nome, e.nomeabreviado
  From niff_chm_log l, niff_chm_usuarios u, niff_chm_empresas e
Where trunc(Data) = trunc(Sysdate)
 And u.Idusuario = l.Idusuario
 And u.idempresa = e.idempresa
-- And e.idempresa = 5
-- And tela In ('Principal - Login','Principal - Logoff')
-- And u.Idusuario = 1
)
--Where Descricao Like '%03/2019%'
Order By Idlog Desc
