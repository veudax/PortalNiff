Select NomeFunc, CodFunc, codintfunc
     , AquiIniAnterior, AquiFinAnterior
     , AquiIniAtual, AquiFinAtual
     , meses
     , TeveSuspensao, QtdeMesesSuspenso
     , DiasGozados, DESCAREA
     , Gozoinifer, Gozofinfer     
 From (
Select f.NomeFunc, f.CodFunc, f.codintfunc, f.DESCAREA
     , e.Aquiinifer AquiIniAnterior, e.Aquifinfer AquiFinAnterior
     , e.Gozoinifer, e.Gozofinfer
     , Case When e.DiasGozados < 30 Then
          e.Aquiinifer 
       Else
          e.AquiIniAtual
       End AquiIniAtual
     , Case When e.DiasGozados < 30 Then
          e.Aquifinfer
       Else
          e.AquiFinAtual
       End AquiFinAtual
     , Round(MONTHS_BETWEEN(Trunc(Sysdate), trunc(e.AquiIniAtual))) meses
     , e.TeveSuspensao
     , e.DiasGozados
     , e.QtdeMesesSuspenso
  From Vw_Funcionarios f
     , (Select Distinct s.Aquiinifer, s.Aquifinfer, s.codintFunc
             , s.Aquifinfer+1 AquiIniAtual, ADD_MONTHS(s.Aquifinfer,12) AquiFinAtual
             , Max(s.Gozoinifer) Gozoinifer, max(s.Gozofinfer) Gozofinfer
             , Nvl((Select Distinct 'S'
                  From Flp_Afastados a
                 Where a.codcondi In (36,38)
                   And a.CodintFunc = s.CodIntFunc),'N') TeveSuspensao
             , Nvl((Select Sum(Round(MONTHS_BETWEEN(Trunc(a.dtretafast), trunc(A.Dtafast)))) 
                  From Flp_Afastados a
                 Where a.codcondi In (36,38)
                   And a.CodintFunc = s.CodIntFunc),0) QtdeMesesSuspenso                   
             , (Select Sum(diasgozofer) dias
                  From flp_ferias
                 Where statusferias = 'N'
                   And Aquiinifer = s.AquiIniFer
                   And Aquifinfer = s.AquiFinFer
                   And codintfunc = s.CodintFunc) DiasGozados
          From Flp_ferias s
         Where s.statusferias = 'N'
           And (s.Aquiinifer, s.Aquifinfer, s.codintfunc) In (Select Max(Aquiinifer), Max(Aquifinfer),  f.codintfunc
                                                                From flp_ferias e, vw_funcionarios f
                                                               Where codigoempresa = &pEmpresa 
                                                                 And codigofl = &pFilial
                                                                 And statusferias = 'N'
                                                                 And e.codintfunc = f.CODINTFUNC
                                                                 And f.SITUACAOFUNC <> 'D'
                                                               Group By f.CodintFunc )
        Group By s.Aquiinifer, s.Aquifinfer, s.codintFunc) E
                                                           
  Where f.Codintfunc = e.codintFunc)
  Where AquiFinAtual <= Sysdate
  Order By descarea, codfunc