Select Distinct c.classcustoctb, c.codcusto, c.desccusto, 
    Case When SubStr(c.classcustoctb,3,1) = '0' Then '1'
         When SubStr(c.classcustoctb,5,1) = '0' Then '2'
         When SubStr(c.classcustoctb,7,2) = '00' Then '3'             
         When SubStr(c.classcustoctb,10,2) = '00' Then '4'
         When SubStr(c.classcustoctb,13,3) = '000' Then '5'             
         When SubStr(c.classcustoctb,17,4) = '0000' Then '6'             
         Else '7' End Grau
  From ctbcusto c, ctbsaldoccusto s
Where c.nroplano = 10
  And c.codcusto = s.codcusto
  And c.nroplano = s.nroplano
  And s.periodosaldo Between '201701' And '201812'
Order By c.classcustoctb