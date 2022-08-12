Select Distinct nomearquivo From niff_fis_arquivei t
Where trunc(t.dataimportado) = '13-feb-2019';


Delete niff_fis_itensarquivei i
Where i.idarquivei In (Select t.Idarquivei from niff_fis_arquivei t Where tipoArquivo = 'XML');
--Where t.Nomearquivo Like '%N:\CONTROLADORIA\CONTABILIDADE\EXERCÍCIO 2018\DECLARAÇÕES\ARQUIVEI\EOVG MATRIZ\relatorio_avancado_nfe_13-02-2019.xlsx');
 

Delete niff_fis_arquivei t 
Where tipoArquivo = 'XML';

Delete niff_fis_importandoarquivei t
Where t.arquivo Like '%xml';