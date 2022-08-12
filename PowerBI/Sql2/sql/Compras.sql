Select To_char(p.datapedido,'mm/yyyy') MesAno, u.nomeusuario, Count(*), A.NOMEFANTASIAEMPRESA
  From cpr_pedidocompras p
     , ctr_cadastrodeusuarios u
     , CTR_EMPAUTORIZADAS A
     , CTR_FILIAL F
Where p.datapedido Between '01-nov-2018' And Sysdate
  And p.usuariogeroupedido = u.usuario
  And A.CODINTEMPAUT = F.CODINTEMPAUT
  And F.CODIGOEMPRESA = P.CODIGOEMPRESA
  And F.CODIGOFL = P.CODIGOFL
Group By nomeusuario, To_char(p.datapedido,'mm/yyyy') ,  A.NOMEFANTASIAEMPRESA, P.codigoempresa, P.CODIGOFL
Order By P.codigoempresa, P.CODIGOFL, mesano