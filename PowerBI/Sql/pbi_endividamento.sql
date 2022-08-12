create or replace view pbi_endividamento as
Select Sum(i.valoritemdoc) ValorBruto
    ,  Sum(fc_cpg_vlrliquido(d.coddoctocpg)) valorLiquido
    , i.codtpdespesa
    , tp.desctpdespesa
    , d.codtpdoc
    , to_char(d.vencimentoCpg,'yyyymm') anomes
    , to_char(d.vencimentoCpg,'yyyy') ano
    , to_char(d.vencimentoCpg,'mm/yyyy') mesAno
    , f.nfantasiaforn
    , f.codigoforn
    , pe.modalidade
    , e.codigoglobus EmpFil
    , Case When pe.modalidade Like 'Parcelamento%' Or pe.modalidade = 'Pert' Or pe.Modalidade = 'PMG' Then 'Parcelamento' Else 'P + Z' End Tipo
    , (Select Sum(fc_cpg_vlrliquido(dx.coddoctocpg))
            From cpgdocto dx, cpgitdoc ix, bgm_fornecedor fx, cpgtpdes tpx, niff_ctb_parametrosendividamento pex, niff_chm_empresas ex
           Where ix.coddoctocpg = dx.coddoctocpg
             And dx.codtpdoc In (Select codtpDoc From niff_ctb_parametrosendividamento Where idEmpresa = ex.Idempresa)
             And dx.statusdoctocpg <> 'C'
             And dx.vencimentoCpg >= Trunc(Sysdate)
             And fx.codigoforn = dx.codigoforn
             And ix.codtpdespesa = tpx.codtpdespesa
             And pex.codigoforn = fx.codigoforn
             And pex.codtpdespesa = tpx.codtpdespesa
             And lpad(dx.codigoempresa,3,'0') || '/' || lpad(dx.codigoFl,3,'0') = ex.codigoglobus
             And ex.Idempresa = pex.Idempresa

             And dx.codtpdoc = d.codtpdoc
             And dx.codigoforn = d.codigoforn
             And ix.codtpdespesa = tp.codtpdespesa
             And dx.codigoempresa = d.codigoempresa
             And dx.codigofl = d.codigofl
             And pex.modalidade = pe.modalidade)  valor

 From cpgdocto d, cpgitdoc i, bgm_fornecedor f, cpgtpdes tp, niff_ctb_parametrosendividamento pe, niff_chm_empresas e
Where i.coddoctocpg = d.coddoctocpg
  And d.codtpdoc In (Select codtpDoc From niff_ctb_parametrosendividamento Where idEmpresa = e.Idempresa)
  And d.statusdoctocpg <> 'C'
  And d.vencimentoCpg BETWEEN ADD_MONTHS(Trunc(Sysdate,'rr'), -12) AND ADD_MONTHS(Trunc(Sysdate,'rr'), +36)
  And f.codigoforn = d.codigoforn
  And i.codtpdespesa = tp.codtpdespesa
  And pe.codigoforn = f.codigoforn
  And pe.codtpdespesa = tp.codtpdespesa
  And lpad(d.codigoempresa,3,'0') || '/' || lpad(d.codigoFl,3,'0') = e.codigoglobus
  And e.Idempresa = pe.Idempresa
 Group By  to_char(d.vencimentoCpg,'yyyymm'), i.codtpdespesa, d.codtpdoc
    , to_char(d.vencimentoCpg,'yyyy')
    , to_char(d.vencimentoCpg,'mm')
    , to_char(d.vencimentoCpg,'mm/yyyy')
    , f.nfantasiaforn
    , f.codigoforn
    , tp.desctpdespesa
    , pe.modalidade
    , e.codigoglobus
    , d.codigoforn
    , tp.codtpdespesa
    , d.codigoempresa
    , d.codigofl
Union All

Select Sum(n.previsto) valorBruto
     , Sum(realizado) ValorLiquido
     , pe.codtpdespesa
     , tp.desctpdespesa
     , 'CTT' codtpdoc
     , referencia anoMes
     , Substr(referencia,1,4) Mes
     , Substr(referencia,5,2) || '/' || Substr(referencia,1,4)  MesAno
     , f.nfantasiaforn
     , f.codigoForn
     , n.modalidade
     , e.codigoglobus EmpFil
     , 'Bacen' Tipo
     , 0 Valor
  From niff_ctb_endividamento n, niff_ctb_parametrosendividamento pe, niff_chm_empresas e, bgm_fornecedor f, cpgtpdes tp
 Where n.tipo = 'Bacen'
   And e.Idempresa = n.Idempresa
   And pe.Idempresa = e.Idempresa
   And pe.codigoforn = n.codigoforn
   And pe.modalidade = n.modalidade
   And f.codigoforn = n.codigoforn
   And tp.codtpdespesa = pe.codtpdespesa

Group By pe.codtpdespesa
     , tp.desctpdespesa
     , referencia
     , Substr(referencia,1,4)
     , Substr(referencia,5,2) || '/' || Substr(referencia,1,4)
     , f.nfantasiaforn
     , n.modalidade
     , e.codigoglobus
     , f.codigoForn

