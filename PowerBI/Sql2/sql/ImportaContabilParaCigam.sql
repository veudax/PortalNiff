--Select * From (
Select 'INSERT INTO Niff_ReposContabil_aux (' ||
       'idrepos, codigoempresa, codigofl, codlanca, coditemlanca, data, ' ||
       'lote, origem, documento, situacao, modificado, usuariocriacao, datacriacao, ' ||
       'contacontabil, contrapartida, valor, codigocusto, debcred, status, historico ) ' ||
       'VALUES ((Select Max(idrepos)+1 From Niff_Reposcontabil_aux) ,' || l.codigoempresa || ',' || l.codigofl || ',' || l.codlanca || ',' || i.coditemlanca || ',''' || l.dtlanca || ''',''' || 
       l.lotelanca || ''',''' || l.sistema || ''',''' || l.documentolanca || ''',''' ||
       'I'',''' || l.lctomodificado || ''',''' ||
       L.USUARIO || ''',''' || Sysdate || ''',' || i.codcontactb || ',' || decode(i.contrapartitemlanca, Null, 'Null', i.contrapartitemlanca)  || ',' ||
       i.vritemlanca  || ',' || DECODE(i.Codcusto, Null, 'NULL', I.CODCUSTO)  || ',''' || i.debitocreditoitemlanca  || ''',' || '0,''' || 
       TiraEComercial( i.historicoitemlanca ) || ''');' descricao
  From ctblanca l, Ctbitlnc i
 Where l.codlanca = i.codlanca
   And l.codigoempresa = 16
   And l.dtlanca Between '01-aug-2018' And '31-aug-2018'
/*Union 
Select 'Execute pr_niff_ImportaCigam();' descricao
  From dual )
  Order By descricao Desc
  */