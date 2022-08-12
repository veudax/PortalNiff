Select h.*, Rowid From FLP_HISTORICOSALARIAL h
Where h.codmotaltersal In (1497,1245,1240);

Update FLP_HISTORICOSALARIAL H
   Set H.codmotaltersal = 814
     , H.MOTIVOHISTSAL = 'INCLUSÃO DE ASSISTENCIA MEDICA'
Where h.codmotaltersal In (1457,1456,851,1441,1442,774,776,771,1439,732);

Select P.*, Rowid From FLP_PROAUTONOMOS_HISTSAL P
Where P.codmotaltersal In (820);
