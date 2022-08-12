Select MesAno, Nome, Empresa, SubCompetecia, t.Idcompetencia, t.Descricao Competencia
     , Max(NotaColaborador) NotaColaborador
     , Max(NotaRH) NotaRH
     , Max(NotaGestor) NotaGestor
     , pontoColaborador
     , pontoRH
     , pontogestor
     , ComentarioCol
     , ComentarioRH
     , ComentarioGestor
  From Niff_Ads_Competencias T
     , (Select MesAno, Nome, Empresa, SubCompetecia, Idcompetencia
             , Sum(NotaColaborador) NotaColaborador
             , Sum(NotaRH) NotaRH
             , Sum(NotaGestor) NotaGestor
             , Sum(pontoColaborador) pontoColaborador
             , Sum(pontoRH) pontoRH
             , Sum(pontogestor) pontogestor
             , Max(ComentarioCol) ComentarioCol
             , Max(ComentarioRH) ComentarioRH
             , Max(ComentarioGestor) ComentarioGestor                          
         From (Select lpad(A.mesreferencia,6,'0') mesano, c.Nome 
                    , Decode(a.Tipo, 'AA', a.notaavaliacao, 0) NotaColaborador
                    , Decode(a.Tipo, 'AR', a.notaavaliacao, 0) NotaRH
                    , Decode(a.Tipo, 'AG', a.notaavaliacao, 0) NotaGestor
                    , e.Codigoglobus || ' - ' || e.Nomeabreviado Empresa
                    , s.descricao SubCompetecia
                    , Decode(a.Tipo, 'AA', ia.avaliacao, 0) pontoColaborador
                    , Decode(a.Tipo, 'AR', ia.avaliacao, 0) pontoRH
                    , Decode(a.Tipo, 'AG', ia.avaliacao, 0) pontogestor
                    , Decode(a.Tipo, 'AA', ia.Comentario, '') ComentarioCol
                    , Decode(a.Tipo, 'AR', ia.Comentario, '') ComentarioRH
                    , Decode(a.Tipo, 'AG', ia.Comentario, '') ComentarioGestor                                                            
                    , s.Idcompetencia
                 From Niff_ADS_Avaliacao a, 
                      Niff_Ads_Colaboradores c,
                      Niff_Chm_Empresas e,
                      Niff_Ads_ItensAvaliacao ia,
                      NIFF_ADS_SubCompetencias s
                Where a.MesReferencia = '062018'
                  And a.tipo In ('AA','AR','AG')
                  And c.Idcolaborador = a.Idcolaborador
                  And a.Idempresa = e.Idempresa
                  And Substr(lpad(A.mesreferencia,6,'0'),3,4) = 2018
                  And ia.idautoavaliacao = a.Idautoavaliacao
                  And s.Idsubcomp = ia.Idsubcomp ) 
         Group By MesAno, Nome, Empresa, SubCompetecia, Idcompetencia) r
 Where t.Idcompetencia = r.Idcompetencia
 Group By MesAno, Nome, Empresa, SubCompetecia, t.Idcompetencia, t.Descricao
      , pontoColaborador
     , pontoRH
     , pontogestor
     , ComentarioCol
     , ComentarioRH
     , ComentarioGestor

 Order By Idcompetencia, SubCompetecia