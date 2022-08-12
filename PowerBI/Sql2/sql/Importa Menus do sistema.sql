Select a.*, 'Insert into niff_chm_telas (idtela, idmodulo, nome, caminho, ativo ) ' ||
' Values (SQ_NIFF_IDTela.NextVal, ' || 
'124, ''' || eliminacaracteresespeciais(caption) || ''', null, ' || '''S''); '
From ctr_menusdosistema a
Where sistema = 'FRE'
And caption <> '-'
And menuWeb = 'N'
And Length(a.Indicemenu)>2