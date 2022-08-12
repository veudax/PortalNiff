Update est_cadmaterial m
Set m.codsittributaria = 060, 
    m.codoperfiscalnoestado = 060, 
    m.codoperfiscalforaestado = 060 
Where m.codigomatint In (Select i.codigomatint
  From Bgm_Notafiscal f,
       est_itensnf i
 Where f.codintnf = i.codintnf
   And f.dataemissaonf >= '01-mar-2017'
   And i.codclassfisc In (1407,2407)
 );
 
Update Est_Cadmaterialavulso a
   Set a.codsittributaria = 060
 Where a.codigomatavulso In ( Select i.codigomatavulso
  From Bgm_Notafiscal f,
       Est_Nfservico i
 Where f.codintnf = i.codintnf
   And f.dataemissaonf >= '01-mar-2017'
   And i.codclassfisc In (1407,2407)
 );