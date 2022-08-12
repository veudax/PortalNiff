create or replace view pbi_dre as
Select v.previsto, v.realizado
     , anoMes
     , MesAno
     , mes
     , Ano
     , Grupo
     , Idmetas, EmpFil
     , ordem
     , dataCorte
     , dissidio
     , Agrupador
     , Descricao
     , Nvl((Select Qtde
              From vw_niff_DiasUteis
             Where IdEmpresa = v.Idempresa
               And AnoMes = v.AnoMes),0) DiasUteisAtual
     , Nvl((Select Qtde
              From vw_niff_DiasUteis
             Where IdEmpresa = v.Idempresa
               And AnoMes = v.AnoMes-100),0) DiasUteisAnterior
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 45) RecBruta
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 45) RecBrutaOrcada
     , (Select Sum(previsto)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 21) OrcadoRecSemSub
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes-100
           And idmetas = 45) RecBrutaAnterior
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 21) ReceitaArr
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes-100
           And idmetas = 21) ReceitaArrAnterior
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 35) SubsidioRealizado
     , (Select Sum(previsto)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 35) SubsidioOrcado
     , (Select Sum(previsto)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 2) FolhaOperacaoPrevista
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 2) FolhaOperacao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 7) HorasExtrasOperacao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 57) salarioOperacao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes-100
           And idmetas = 57) salarioAnteriorOperacao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 65) DemissaoOperacao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 60) PlrOperacao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes-100
           And idmetas = 58) FeriasAnteriorOperacao
     , (Select Sum(previsto)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes-100
           And idmetas = 142) Ferias13Anterioroperacao

     , (Select Sum(Previsto)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 22) FolhaManutencaoPRevisto
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 22) FolhaManutencao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 72) salarioManutencao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes-100
           And idmetas = 72) salarioAnteriorManutencao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 80) DemissaoManutencao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 75) PlrManutencao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 3) HorasExtrasManutencao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes-100
           And idmetas = 73) FeriasAnteriorManutencao
     , (Select Sum(previsto)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes-100
           And idmetas = 143) Ferias13AnteriorManutencao

     , (Select Sum(Previsto)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 23) FolhaAdmPrevisto
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 23) FolhaAdm
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 102) salarioAdm
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes-100
           And idmetas = 102) salarioAnteriorAdm
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 110) DemissaoAmd
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 105) PlrAdm
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 25) HorasExtrasAdminitracao
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes-100
           And idmetas = 103) FeriasAnteriorAdm
     , (Select Sum(previsto)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes-100
           And idmetas = 144) Ferias13AnteriorAdm


     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 21) RecBrutaSemSubsidio
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 35) Subsidio
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes-100
           And idmetas = 21) RecBrutaSemSubsidioAnterior
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas In (2, 22, 23)) TotalFolha
     , (Select Sum(Previsto)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas In (2, 22, 23)) TotalFolhaPrevista

     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas In (95)) Diesel
     , (Select sum(r.PASSAGEIROS)
          From pbi_arr_receita r
         Where r.EmpFil = v.empFil
           And to_char(r.Data,'yyyymm') = v.AnoMes) PassageirosMesAtual
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 172)  GratuidadeMesAtual
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 174)  IntegracoesMesAtual
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 173) PagantesMesAtual

     , (Select sum(r.VLR_FATURAMENTO)
          From pbi_arr_receita r
         Where r.EmpFil = v.EmpFil
           And to_char(r.Data,'yyyymm') = v.AnoMes) FaturamentoMesAtual

/*     , (Select sum(r.PASSAGEIROS)
          From pbi_arr_receita r
         Where r.EmpFil = v.EmpFil
           And to_char(r.Data,'yyyymm') = v.AnoMes-100) PassageirosAnoAnterior
     , (Select sum(r.PASSAGEIROS)
          From pbi_dregratuidade r
         Where r.EmpFil = v.EmpFil
           And r.VLR_FATURAMENTO = 0
           And to_char(r.Data,'yyyymm') = v.AnoMes-100) GratuidadeAnoAnterior
     , (Select sum(r.PASSAGEIROS)
          From pbi_DreIntegracoes r
         Where r.EmpFil = v.EmpFil
           And r.VLR_FATURAMENTO = 0
           And to_char(r.Data,'yyyymm') = v.AnoMes-100) IntegracoesMesAnterior
     , (Select sum(r.PASSAGEIROS)
          From pbi_arr_receita r
         Where r.EmpFil = v.EmpFil
           And r.VLR_FATURAMENTO <> 0
           And to_char(r.Data,'yyyymm') = v.AnoMes-100) PagantesMesAnterior
*/
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 135) PneusNovos
     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 138) PneusRecaados

     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 44) KmRodado

     , (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.AnoMes
           And idmetas = 147) LitrosConsumidos

  From (
Select v.previsto, v.realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , m.descricao Grupo, m.Descricao Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , decode(m.IdMetas, 45, 0, 35, 1, 2, 4, 22, 5, 23, 9, 50, 12, 21, 0, 100,8, 112,10, 113,11, 132,20,0) ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
  From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) Mes
     , To_number(substr(v.referencia,1,4)) Ano
     , Decode(m.idMetas, 2, 'Folha Operacional', '') Grupo, 'Folha Operacional' Agrupador, 'Folha Operacional' Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 3 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (2)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus, v.idempresa, v.referencia
     , v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) Mes
     , To_number(substr(v.referencia,1,4)) Ano
     , Decode(m.idMetas, 22, 'Folha Manutenção', '') Grupo, 'Folha Manutenção' Agrupador, 'Folha Manutenção' Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 4 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (22)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus, v.idempresa, v.referencia
     , v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) Mes
     , To_number(substr(v.referencia,1,4)) Ano
     , Decode(m.idMetas, 23, 'Folha Administração', '') Grupo, 'Folha Administração' Agrupador, 'Folha Administração' Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 5 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (23)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus, v.idempresa, v.referencia
     , v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) Mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Diesel' Grupo, 'Diesel' Agrupador, 'Diesel' Descricao
     , 0 /*m.Idmetas*/, e.codigoglobus EmpFil
     , 6 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (95)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , e.codigoglobus, v.idempresa, v.referencia
     , v.Idempresa
     , d.dissidio
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) Mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Peças/Pneus' Grupo, 'Peças/Pneus' Agrupador, 'Peças/Pneus' Descricao
     , 0 /*m.Idmetas*/, e.codigoglobus EmpFil
     , 6 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (42,43)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , e.codigoglobus, v.idempresa, v.referencia
     , v.Idempresa
     , d.dissidio
 Union All
Select  Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , ' (-) Impostos + Consórcios' Grupo, ' (-) Impostos + Consórcios' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 2 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (36)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select  Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , ' (=) Margem de Contribuição' Grupo, ' (=) Margem de Contribuição' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 8 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (100)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select  Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , ' (=) Receita Liquida' Grupo, ' (=) Receita Liquida' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 3 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (34)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select  Sum(Decode(m.IdMetas,45, 1,-1) * v.previsto) previsto, Sum(Decode(m.IdMetas,45, 1,-1) * v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Outros' Grupo, 'Outros' Agrupador, 'Outros' Descricao
     , 0, e.codigoglobus EmpFil
     , 7 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (45,2,22,23,95,42,43,50,112)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio/*, m.descricao*/
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Salários' Grupo, 'Salários' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 6 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (57, 72, 102, 137)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Horas Extras' Grupo, 'Horas Extras' Agrupador, 'Horas Extras' Descricao
     , 0 Idmetas, e.codigoglobus EmpFil
     , 6 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (7, 3, 25)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     --, m.Idmetas
     , e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio--, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Benefícios' Grupo, 'Benefícios' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 6 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (64,79,109)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Encargos' Grupo, 'Encargos' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 6 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (59, 74, 104)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Provisões' Grupo, 'Provisões' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 6 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (58, 66, 73, 81, 103, 111)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Demissões' Grupo, 'Demissões' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 6 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (65,80,110)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Outros Pagtos' Grupo, 'Outros Pagtos' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 6 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (60,62,63,75,76,77,105,107,136)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Ferramentas' Grupo, 'Operacional' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 15 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (89,91,92)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Outros Custos Operacionais' Grupo, 'Operacional' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 16 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (96,97,68,160)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Outras Despesas com Pessoal' Grupo, 'Operacional' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 17 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (83,67,159,114,148,152,158,155)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Software e Programas' Grupo, 'Operacional' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 18 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (149,162)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
 Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Administrativas' Grupo, 'Administrativas' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 19 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (161,90,118,150,120,121,122,123,124,125,126,127,128,129,131,151,132)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Frota de Apoio' Grupo, 'Administrativas' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 21 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (86,157)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Consórcios' Grupo, 'Administrativas' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 22 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (53,54,55,56)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Predial' Grupo, 'Administrativas' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 23 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (115,116,117)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Processos' Grupo, 'Processos' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 11 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (112)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Processos Realizado' Grupo, 'Processos' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 11 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (112)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.previsto) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Processos Orçados' Grupo, 'Processos Orçados' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 24 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (112)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Tributarias' Grupo, 'Administrativas' Agrupador, m.descricao Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 25 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (153,154,134,51)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio, m.descricao
 Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'Total Geral' Grupo, 'Total Geral' Agrupador, 'Total Geral' Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 26 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (89,91,92,96,97,160,68,83,67,159,114,148,152,158,162,149,153,154,134,51,155,112,115,116,117,53,54,55,56,157,86,132,161,90,118,150,120,121,122,123,124,125,127,126,128,129,151,131)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio
Union All
Select Sum(v.previsto) previsto
     , Round((Sum(v.realizado) /
      (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.referencia
           And idmetas = 45) )*100,0) Realizado

     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , '% Sobre Receita' Grupo, '% Sobre Receita' Agrupador, '% Sobre Receita' Descricao
     , 0, e.codigoglobus EmpFil
     , 26 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (89,91,92,96,97,160,68,83,67,159,114,148,152,158,162,149,153,154,134,51,155,115,116,117,53,54,55,56,157,86,132,161,90,118,150,120,121,122,123,124,125,127,126,128,129,151,131)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio
Union All
Select Sum(v.previsto) previsto
     , Round((Sum(v.realizado) /
      (Select Sum(realizado)
          From niff_ads_valoresmetas
         Where idEmpresa = v.idEmpresa
           And referencia = v.referencia
           And idmetas = 45) )*100,0) Realizado

     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , '% Sobre Receita Processo' Grupo, '% Sobre Receita' Agrupador, '% Sobre Receita' Descricao
     , 0, e.codigoglobus EmpFil
     , 26 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas In (112)
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio
Union All
Select 0 previsto
     , decode(Sum(qtde), 0 , 0, Sum(km)/ Sum(QTDE)) Realizado
     , To_number(AnoMes) AnoMes
     , MEsAno
     , '00' mes
     , To_number(Ano) Ano
     , 'Modelo Chassi' Grupo, 'Modelo Chassi' Agrupador, DESCRICAOMODCHASSI Descricao
     , 0
     , EmpFil
     , 555 ordem
     , Sysdate DataCorte
     , 0 IdEmpresa
     , 0 dissidio
  From Pbi_ConsultaAbastecimentos
 Where To_number(Ano)  > 2017
 Group By To_number(AnoMes)
     , MEsAno
     , To_number(Ano)
     , DESCRICAOMODCHASSI
     , EmpFil
     , Sysdate
Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'MKBF Modal' Grupo, 'MKBF Modal' Agrupador, '- MKBF Empresa' Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 555 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas = 6
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio
Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'MKBF Modal' Grupo, 'MKBF Modal' Agrupador, 'Rodoviário' Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 556 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas = 165
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
   And v.Realizado > 0
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio
Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'MKBF Modal' Grupo, 'MKBF Modal' Agrupador, 'Suburbano' Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 557 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas = 167
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
   And v.Realizado > 0
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio
Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'MKBF Modal' Grupo, 'MKBF Modal' Agrupador, 'Urbano' Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 558 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas = 166
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
   And v.Realizado > 0
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio
Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'MKBF Modal' Grupo, 'MKBF Modal' Agrupador, 'Intermunicipal' Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 559 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas = 168
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
   And v.Realizado > 0
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio
Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'MKBF Modal' Grupo, 'MKBF Modal' Agrupador, 'Municipal' Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 560 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas = 169
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
   And v.Realizado > 0
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio
Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'MKBF Modal' Grupo, 'MKBF Modal' Agrupador, 'Escolar' Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 561 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas = 170
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
   And v.Realizado > 0
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio
Union All
Select Sum(v.previsto) previsto, Sum(v.realizado) realizado
     , to_number(v.referencia) anoMes
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
     , Substr(v.referencia,5,2) mes
     , To_number(substr(v.referencia,1,4)) Ano
     , 'MKBF Modal' Grupo, 'MKBF Modal' Agrupador, 'Fretamento' Descricao
     , m.Idmetas, e.codigoglobus EmpFil
     , 562 ordem
     , d.datafechamento dataCorte
     , v.Idempresa
     , d.dissidio
   From Niff_Ads_Valoresmetas v
     , Niff_Ads_Metas m
     , Niff_Chm_Empresas e
     , Niff_Ctb_Dre d
 Where v.Idmetas = m.Idmetas
   And v.idempresa = e.Idempresa
   And m.exibirnodre = 'S'
   And m.Idmetas = 171
   And v.idempresa = d.Idempresa
   And v.referencia = d.referencia
   And d.fechado = 'S'
   And v.referencia > 201712
   And v.Realizado > 0
Group By
      to_number(v.referencia)
     , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4)
     , To_number(substr(v.referencia,1,4))
     , d.datafechamento
     , m.Idmetas, e.codigoglobus , v.idempresa, v.referencia, v.Idempresa
     , d.dissidio


 ) v

