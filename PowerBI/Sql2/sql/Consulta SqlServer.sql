use sys_sac;

select idUsuario, 'Insert into NIFF_CHM_USUARIOS (' +
'idusuario, tipo, nome, ativo, administrador, telefone, ramal, usuarioacesso, senha, ' +
'acessaagenda, acessachat, permiteexcluirchat, acessabi, permiteincexcfotosfesta, ' +
'idempresa, acessasac )' +
' Values ( SQ_NIFF_IDUsuario.NextVal, ''C'', ''' + nmUsuario + ''', ''' + icStatus + ''', ''N'', 0, 0, ' +
'''' 
+ dsUsuario + ''',''' + dsSenha + ''', ''N'', ''N'', ''N'', ''N'', ''N'', 4, ''N'');'
 from Usuario
where idEmpresa = 2
and dsEmail is null;

select * from Usuario;
select * from acesso;
select * from atendente;
select * from Atendimento;
select * from empresa;
select * from EMTUAtendimento;
select * from log;

Select 'insert into NIFF_CHM_TipoAtendimento ' +
'(idtipoatendimento, descricao, ativo) ' +
' values(' + Convert(varchar, idTpAtendimento) + ',''' 
+ dsTpAtendimento + ''',''' + icStatus + ''');'  
 from TipoAtendimento;

select * from Departamento;

Select 'insert into NIFF_CHM_Departamento ' +
'(IdDepartamento, descricao, ativo) ' +
' values(' + Convert(varchar, idDepartamento) + ',''' 
+ nmDepartamento + ''',''' + icStatus + ''');'  
 from Departamento;

select * from EMTUAtendimento;

Select 'insert into NIFF_CHM_EMTUAtendimento ' +
'(IdEmtu, Codigo, IdDepartamento, IdTpAtendimento, Titulo, descricao) ' +
' values(' + Convert(varchar, icOrdem) + ',''' + idEmtu + ''',' + Convert(varchar, idDepartamento) + 
',' + Convert(varchar, idTpAtendimento) + ',''' + dsTituloEmtu + ''',''' + 
dsDescricaoEmtu +  ''');'  
 from EMTUAtendimento;

 

select *, 'insert into NIFF_CHM_Atendimento ' +
'(idAtendimento, codigo, idUsuario, idTpAtendimento, RG, CPF, TextoAtendimento, TextoResposta, NOme, Endereco, Cidade,  ' +
'telefone, celular, email, DataAbertura, DataResposta, Status, Situacao, ' +
'Retorno, Retornou, CodigoLinha, Origem, IdEmtu) ' +
' values(' + Convert(varchar, idAtendimento) + ',''' 
+ CodSAC + ''',' 
+ case idUsuario
    When 3 then '11'
    When 2 then '9'
    When 4 then '12'
    when 5 then '13'
    when 6 then '14'
    when 7 then '15'
    when 8 then '16'
    when 9 then '17'
    when 10 then '18'
    when 14 then '29'
    when 15 then '39'
    when 16 then '40'
    when 17 then '20'
   when 18 then '21'
   when 19 then '41'
   when 20 then '23'
   when 21 then '42'
   when 22 then '24'
  when 23 then '43'
  else '26' end
+ ',' + Convert(varchar, a.idTpAtenDimento) + ',''' 
+ NrRG 
+ ''',''' 
+ NRCPF + ''',''' 
+ dsAtendimento + ''',''' + dsResposta + ''',''' + nmCliente+ ''',''' + dsEndereco + ''',''' 
+ dsCidade + ''',' 
+ 'EliminaNaoNumericos(trim(''' + nrdddtel+ nrteLefone + ''')),'
+ 'EliminaNaoNumericos(trim(''' + nrdddCel + nrcelular + ''')),''' 
+ DSemail  + ''',' 
+ ' To_date( ''' + Convert(varchar, dtAtendimento,23) + ''',''yyyy-mm-dd''), ' 
+ ' To_date( ''' + Convert(varchar, dtResposta,23) + ''',''yyyy-mm-dd''), ''' 
+ icstatus + ''',''' + icstatus2 + ''',''' + icRetorno + ''',''' 
+ icRetornou + ''',''' + Convert(varchar, codLinha) + ''',''' 
+ icComo + ''',' + Convert(varchar, e.icOrdem) + ');'
 from atendimento a, emtuAtendimento e
where dtFinalizar is null
and nrchapa is  null
and e.IdEmtu = a.IdEmtu
and a.icRetornou is not null

select 'Update niff_chm_atendimento ' + 
' set idUsuarioResponsavel = ' + 
case IdAtendente 
  when 3 then '12'
  when 6 then '9'
  when 7 then '18'
  when 1 then '14'
  when 10 then '24'
  when 5 then '17'
  when 8 then '11'
  when 9 then '23'
  when 11 then '26'
  when 4 then '15'
  else '13' end + 
' where idAtendimento = ' + convert(varchar, idAtendimento) + ';' 
from atendimento
where idAtendimento > 30036;