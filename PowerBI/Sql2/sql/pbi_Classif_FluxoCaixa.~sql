Create Or Replace View pbi_Classif_FluxoCaixa As
  Select '01 - FOLHA DE PAGAMENTO' CLASS,
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (22129,22101,22113,22104,22112,22107,22122,22108,22120,22117,22103,22119,24580,23313,2102,
                          22106,54578,22139,22140,22141,22142,23204,22121,22118,24693,22116,24705,22105,22102,22114,
                          23318,23309,22116,22138,23131,22105,24711,22131)         
   Union All
  Select '02 - MANUTENCAO DA FROTA' CLASS,                          
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (22204,22205,22214,24621,24662,24667,22202,22203,22210,22207,22206,22201,24656)
   Union All
  Select '03 - GESTAO DA FROTA' CLASS,                          
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (21106,21108,21107,21112,21113,21127,24401,24402,24501,24502,24506,24517,24609,24658,54575,
                          54576,22208,22212,22213,22407,24615,24402,21102,24502,22209,24506,54575,21127)
   Union All
  Select '04 - DESPESAS C/ EQUIPAMENTOS E FERRAMENTAS' CLASS,                          
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (22211,24122,24608,24627,24628,54580,54581)
   Union All
  Select '05 - FROTA DE APOIO' CLASS,                          
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (22401,22403,22404,22405,22406)
   Union All
  Select '06 - IMPOSTOS' CLASS,                          
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (21101,21114,21115,21116,21118,21119,23305,23306,23307,23308,23319,24505,24561,24603,24622,
                          24657,24672,24688,24692,24710,23311,24714,24715,24720)
   Union All
  Select '07 - JUROS E MULTAS' CLASS,                          
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (23401,23402)
   Union All
  Select '08 - DESPESAS ADMINISTRATIVAS' CLASS,                          
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (22308,23102,23118,23123,23205,24604,24577,23106,23107,23112,23120,22215,23103,23104,23105,23108,23109,
                          23110,23111,23113,23114,23116,23117,23119,23121,23122,23127,23128,23132,23202,24203,24218,24220,24308,
                          24618,24643,24659,24660,54573,22301,22304,22218,22300,22306,24515,24611,24655,24665,23101,24510,23115,
                          23125,22302,22303,22132,24703,24653,22305,22307,24575,22110,23207,23107,24594,21109,22215,23209,24527,
                          22217,22409,23122,23132,24616,24633,24638,24654,24660,23115,23125,24575,23209,24633,24660,24515,24611,
                          24564,24655,23115,22132,24703,22307,21109,24616,24575,23209,24633,24660,24515,24611,24564,24655,23115,
                          22132,24703,22307,21109,24616,24575)     
   Union All
  Select '09 - DESPESAS JURIDICAS' CLASS,                          
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (814,22111,22125,23203,24212,24219,24509,23317)    
   Union All
  Select '10 - ORGAO GESTOR' CLASS,                          
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (21103,21125,24217,24689)
   Union All
  Select '11 - INVESTIMENTOS' CLASS,                          
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (24104,24105,24106,24109,24110,24113,24619,24646)              
   Union All
  Select '12 - OUTRAS DESPESAS' CLASS,                          
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (22128)
   Union All
  Select '13 - AUDITORIA' CLASS,                          
         D.CODTPDESPESA ||' - '||D.DESCTPDESPESA DESPESA,
         D.CODTPDESPESA         
    From Cpgtpdes d
   Where codtpdespesa in (23201)  