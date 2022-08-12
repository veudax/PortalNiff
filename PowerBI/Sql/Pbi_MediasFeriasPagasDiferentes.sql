create or replace view pbi_mediasferiaspagasdifer as
Select Codintfunc, Codfunc, Nomefunc, SalBaseHora, EmpFil, funcao, area, Situacao
     , AnoMesGozo, MesAnoGozo
     , aquiinifer, aquifinfer, gozoinifer, Gozofinfer, dtcompetfer
     , Round(Sum(Media),2) Media
     , Round(Sum(Media13),2) media13
--     , SalBaseAcrescido
--     , Sum(referencia)
     , Max(MediaPaga) MediaPaga, Max(Media13Paga) Media13Paga
 From (
Select  Distinct Codintfunc, Codfunc, Nomefunc, SalBaseHora, EmpFil, DESCFUNCAO funcao, DESCAREA area, Situacao
     , To_char(gozoinifer,'yyyymm') AnoMesGozo
     , To_char(gozoinifer,'mm/yyyy') MesAnoGozo
     , aquiinifer, aquifinfer, gozoinifer, Gozofinfer, dtcompetfer
     , referencia, ValorFicha, acrescimoeven
     , SalBaseHora * (acrescimoeven/100) SalBaseAcrescido
     , (((SalBaseHora * (acrescimoeven/100)) * referencia)/12) + ValorFicha Media
     , ((((SalBaseHora * (acrescimoeven/100)) * referencia)/12) + ValorFicha)/3 Media13
     , desceven, codevento
     , MediaPaga, Media13Paga
 From (
Select f.Codintfunc, f.Codfunc, f.Nomefunc, Decode(f.TPSALFUNCAO, 'H', f.SALBASE, Round((f.SALBASE/220),2)) SalBaseHora
     , f.DESCFUNCAO, f.DESCAREA, Decode(f.SITUACAOFUNC,'A','Ativo','Afastado') Situacao
     , ff.competficha, lpad(f.codigoempresa,3,'0') || '/' || lPad(Decode(f.codigoempresa,9, decode(f.codigofl, 2, 1, f.codigofl),f.codigofl),3,'0') EmpFil
     , fe.aquiinifer, fe.aquifinfer, fe.gozoinifer, fe.Gozofinfer, fe.dtcompetfer
     , ae.descagreven, aee.codevento, aee.operadoragr
     , ffe.referencia referencia
     , Decode(ffe.referencia, 0, Round((ffe.valorficha/12),2),0) valorFicha
     , e.acrescimoeven, e.desceven
     , (Select Sum(fc.valorficha)
          From flp_fichaeventos fc
         Where fc.codintfunc = f.CODINTFUNC
           And fc.tipofolha = ff.tipofolha
           And fc.competficha >= '01-Mar-2020'
           And fc.codevento = 14) MediaPaga
     , (Select Sum(fc.valorficha)
          From flp_fichaeventos fc
         Where fc.codintfunc = f.CODINTFUNC
           And fc.tipofolha = ff.tipofolha
           And fc.competficha >= '01-Mar-2020'
           And fc.codevento = 101) Media13Paga
  From vw_Funcionarios f
     , flp_ferias fe
--     , flp_ferias_eve fee
     , flp_agrupadestino ad
     , flp_agrupaeventos ae
     , flp_agrupaeven_even aee
     , flp_fichafinanceira ff
     , flp_fichaeventos ffe
     , flp_eventos e
 Where f.SITUACAOFUNC <> 'D'
   And fe.codintfunc = f.CODINTFUNC
   And fe.usuexclferias Is Null
   And fe.statusferias = 'N'
   --And fe.dtcompetfer = '22-apr-2020'

   And (fe.codintfunc, fe.dtcompetfer) In (Select f.codintfunc, f.dtcompetfer
                                             From flp_ferias f
                                            Where f.Gozoinifer >= '01-mar-2020'
                                             -- And codintfunc = 25564
                                              And f.usuexclferias Is Null
                                              And f.statusferias = 'N'
                                              And (f.codintfunc, f.dtcompetfer, f.Idfer) In (Select e.codintfunc, e.dtcompetfer, e.Idfer
                                                                                      From flp_ferias_eve e
                                                                                      Where (e.codintfunc, e.dtcompetfer, e.Idfer) In (Select ee.codintfunc, ee.dtcompetfer, Max(ee.Idfer)
                                                                                                                                         From flp_ferias_eve ee
                                                                                                                                        Where ee.codevento In (14,101)
                                                                                                                                        Group By ee.codintfunc, ee.dtcompetfer))
                                            Group By f.codintfunc, f.dtcompetfer )


   And ae.tipoagreven = 001
   And ad.codigoempresa = f.codigoempresa
   And ad.tipoagreven = ae.tipoagreven
   And ae.codagreven = ad.codagreven
   And aee.tipoagreven = ae.tipoagreven
   And aee.codagreven = ae.codagreven
   And ff.tipofolha = 1
   And ff.codintfunc = f.codintfunc
   And ff.competficha Between fe.aquiinifer And fe.aquifinfer
   And ffe.codintfunc = f.codintfunc
   And ffe.competficha = ff.competficha
   And ffe.tipofolha = ff.tipofolha
   And ffe.codevento = aee.codevento
   And e.codevento = aee.codevento

--   And f.CODIGOEMPRESA = 29
--   And f.codigofl =1
--   And f.CODINTFUNC = 22989
--   And e.codevento = 442
--   */

)
)
Group By Codintfunc, Codfunc, Nomefunc, SalBaseHora, EmpFil, funcao, area, Situacao
     , AnoMesGozo
     , MesAnoGozo
     , aquiinifer, aquifinfer, gozoinifer, Gozofinfer, dtcompetfer--, SalBaseAcrescido
Order By codfunc, aquiinifer, aquifinfer, gozoinifer, Gozofinfer, dtcompetfer

