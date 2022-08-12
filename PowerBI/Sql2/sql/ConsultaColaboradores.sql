Select Distinct * From (
Select f.Codintfunc,
       Decode(c.Nome, Null, f.Nomefunc, c.Nome) Nomefunc,
       f.Codfunc,
       f.Chapafunc,
       f.Descarea,
       f.Situacaofunc,
       Decode(c.Codintfunc, Null, 'N', 'S') Cadastrado,
       f.Sexofunc,
       f.Salbase,
       f.Dtadmfunc,
       d.Nrdocto,
       f.Dtnasctofunc,
       Max(q.Dtdesligquita) Dtdesligquita
  From Vw_Funcionarios        f,
       Flp_Documentos         d,
       Flp_Quitacao           q,
       Niff_Ads_Colaboradores c,
       niff_ads_empresascolavalia e
 Where Situacaofunc = 'A'
   And d.Codintfunc = f.Codintfunc(+)
   And d.Tipodocto = 'CPF'
   And f.Codintfunc = q.Codintfunc(+)
   And f.Codintfunc = c.Codintfunc(+)
   And c.Participaavaliacao = 'S'
   And e.idcolaborador(+) = c.idcolaborador
   And Lpad(CodigoEmpresa, 3, '0') || '/' || Lpad(CodigoFl, 3, '0') = '009/002'
 Group By f.Codintfunc,
          Decode(c.Nome, Null, f.Nomefunc, c.Nome),
          f.Codfunc,
          f.Chapafunc,
          f.Descarea,
          f.Situacaofunc,
          f.Salbase,
          f.Dtadmfunc,
          d.Nrdocto,
          f.Dtnasctofunc,
          c.Codintfunc,
          f.Sexofunc
Union All          
Select c.codintfunc,
       c.Nome Nomefunc,
       f.Codfunc,
       f.Chapafunc,
       f.Descarea,
       f.Situacaofunc,
       Decode(c.Codintfunc, Null, 'N', 'S') Cadastrado,
       f.Sexofunc,
       f.Salbase,
       f.Dtadmfunc,
       d.Nrdocto,
       f.Dtnasctofunc,
       Max(q.Dtdesligquita) Dtdesligquita
  From Vw_Funcionarios        f,
       Flp_Documentos         d,
       Flp_Quitacao           q,
       Niff_Ads_Colaboradores c,
       niff_ads_empresascolavalia e
 Where Situacaofunc = 'A'
   And d.Codintfunc = f.Codintfunc(+)
   And d.Tipodocto = 'CPF'
   And f.Codintfunc = q.Codintfunc(+)
   And f.Codintfunc = c.Codintfunc(+)
   And c.Participaavaliacao = 'S'
   And e.idcolaborador(+) = c.idcolaborador
   And e.idempresa = 8
   And e.inicio <> '01-jan-0001'
   And (e.fim = '01-jan-0001' Or e.fim >= Sysdate)
 Group By f.Codintfunc,
          c.Nome,
          f.Codfunc,
          f.Chapafunc,
          f.Descarea,
          f.Situacaofunc,
          f.Salbase,
          f.Dtadmfunc,
          d.Nrdocto,
          f.Dtnasctofunc,
          c.Codintfunc,
          f.Sexofunc
)