Select t.chavedeacesso, Count(t.Chavedeacesso)
From niff_fis_arquivei t
Group By t.chavedeacesso
having Count(t.Chavedeacesso) > 1;

/*
Delete From niff_fis_itensarquivei i
Where i.Idarquivei In (Select max(idarquivei)
From niff_fis_arquivei t
Where (t.chavedeacesso,2) In 

(Select t.chavedeacesso, Count(t.Chavedeacesso)
From niff_fis_arquivei t
Group By t.chavedeacesso
having Count(t.Chavedeacesso) > 1)
Group By Chavedeacesso);

Delete From niff_fis_arquivei i
Where i.Idarquivei In (Select max(idarquivei)
From niff_fis_arquivei t
Where (t.chavedeacesso,2) In 

(Select t.chavedeacesso, Count(t.Chavedeacesso)
From niff_fis_arquivei t
Group By t.chavedeacesso
having Count(t.Chavedeacesso) > 1)
Group By Chavedeacesso)

*/