
Select *
  From esfentra n, Bgm_Notafiscal e
 Where n.chavedeacesso In (select a.chavedeacesso From niff_fis_arquivei a
where a.dataemissao Between '01-apr-2018' And '30-apr-2018')
And Sistema <> 'EST'
And n.coddoctoesf = e.coddoctoesf;

