-- antes de limpar campo NrCota desta tabela pegar o numero da solicitação para ajustar o status na tabela Cpr_Solicitacao
-- Se for poucos registros use pelo rowid e altere senão pegue o numero da solicitaçao e use o update no final deste arquivo
Select C.*, Rowid From  Cpr_Itenssolicitados C-- Set C.NRCOTA = Null
Where c.nrcota = 115231; 

Delete Cpr_Prazopagtosol C
Where c.nrcota = 115231;

Delete Cpr_Itensassociasoliccotacao C
Where c.nrcota = 115231; 

Delete CPR_FORNGRPCOMPRACOTACAOGLB7 C
Where C.NRCOTA = 115231;

Delete CPR_MARCAITEMCOTACAOGLB7 C
Where C.NRCOTA = 115231;

Delete CPR_ENDERECOITEMCOTACAOGLB7 C
Where C.NRCOTA = 115231;

Delete  CPR_ITEMCOTACAOGLB7 C
Where C.NRCOTA = 115231;

Delete CPR_UFGRPCOMPRACOTACAOGLB7 C
Where C.NRCOTA = 115231;

Delete  CPR_GRPCOMPRACOTACAOGLB7 C
Where C.NRCOTA = 115231;

Delete CPR_COTACAO_GLB7 C
Where C.NRCOTA = 115231; 

Delete CPR_ITENSCOTADOS c 
Where c.nrcota = 115231; 

Delete CPR_ASSOCIASOLICITACAOCOTACAO c
Where c.nrcota = 115231; 

Delete From  CPR_SERV_G4_G7_LOG A WHERE A.NRCOTA = 115231; 
Delete From  CPR_SERV_G4_G7 A WHERE A.NRCOTA = 115231;

Select C.*, Rowid From CPR_COTACAO C
Where c.nrcota = 115231; 

-- alterar o STATUSSOLIC para P
Select S.*, Rowid From Cpr_Solicitacao S
Where S.NUMEROSOLIC = 159306

-- limpa o numero cotacao e status para P
Update Cpr_Itenssolicitados I
Set I.STATUSITSOL = 'P'
, i.nrcota = Null
Where NUMEROSOLIC = 159306
And I.STATUSITSOL <> 'P'

 