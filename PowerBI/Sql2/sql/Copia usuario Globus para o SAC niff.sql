Select 'Insert into niff_chm_usuarios ' ||
       ' (IdUsuario, Tipo, Nome, Ativo, Administrador, usuarioAcesso, senha, cargo, dtnascimento, IdEmpresa, codfunc, cpf ) ' ||
       
       'Values ( SQ_NIFF_IDUsuario.Nextval, ''S'', ''' || NOMEFUNC || ''', ''S'', ''N'',''' || usuario || ''',''' || Substr(nrdocto,1,6) || 
       ''',''' || DESCFUNCAO || ''',''' || dtnasctofunc || ''',' || 
       Decode(SubStr(nomeUsuario,1,6),'(NIFF)', 1, 
       Decode(SubStr(nomeUsuario,1,6),'(EOVG)', 2,        
       Decode(SubStr(nomeUsuario,1,6),'(RPDO)', 5,               
       Decode(SubStr(nomeUsuario,1,7),'(CISNE)', 6, 8)))) || ',''' || CodFunc || ''',''' || nrdocto || ''');'                
       
  From (Select u.usuario, n.usuarioacesso, u.nomeusuario, f.Nomefunc, f.dtnasctofunc, f.DESCAREA, f.DESCFUNCAO, f.CODFUNC, d.nrdocto
          From ctr_cadastrodeusuarios u, niff_chm_usuarios n, vw_funcionarios F, flp_documentos d
         Where u.usuario = n.usuarioacesso(+)
           And u.ativo = 'S'
           And u.codintfunc = f.codintfunc
           And d.codintfunc = u.codintfunc
           And d.tipodocto = 'CPF'
           And u.usuario Not In (Select usuario From ctr_cadastrodeusuarios Where usuario Like '%2')
           And u.usuario Not In ('BGM','BGMSUPORTE','GLOBUS', 'ECF', 'MANAGER', 'PLANTAO', 'CCOVA', 'PLANTAOEOVG', 'ALMVUG', 'MANUTENCAO','MONITABC','MONITCAMP','PLANTAOVUG', 'PORTARIA', 'PORTARIACISNE', 'TERMINAL', 'TERMINALCAMP', 'TERMINALCISNE', 'TERMINALRPDO', 'TRAFEGORODOV', 'TRAFEGOURBANO', 'DIVINOLANDIA', 'AMBULATORIO09', 'APRENDIZEOVG')
       ) Where USUARIOACESSO Is Null