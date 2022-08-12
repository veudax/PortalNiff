Select Idarquivei, a.numeronf, a.status, a.dataemissao, a. dataimportado, a.nomearquivo From niff_fis_arquivei a 
Where a.Idarquivei In (Select i.Idarquivei From niff_fis_itensarquivei i Where cst Is Null And cfop = 0 And csticms Is Null) 
--And numeronf = 127302
And a.status Not Like 'C%'
And idempresa = 3;

--Where a.numeronf = 7983;

--Select * From niff_fis_itensarquivei i  
--Where i.Idarquivei = 36567