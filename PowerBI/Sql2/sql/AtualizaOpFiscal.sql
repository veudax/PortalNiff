Update Est_Nfservico i
  Set i.codoperfiscal = 090
    , i.codsittributaria = 090
Where i.codintnf In (Select i.codintnf
                       From Bgm_Notafiscal f,
                            Est_Nfservico i
                      Where f.codintnf = i.codintnf
                        And f.dataemissaonf >= '01-mar-2017'
                        And i.codclassfisc In (1407,2407)
                        And i.codoperfiscal = 60)