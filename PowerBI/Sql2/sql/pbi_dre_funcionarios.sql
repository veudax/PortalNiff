create or replace view pbi_dre_funcionarios as
Select to_number(v.referencia) anoMes
      , Substr(v.referencia,5,2) || '/' || substr(v.referencia,1,4) MesAno
      , To_number(substr(v.referencia,1,4)) Ano
      , Substr(v.referencia,5,2) mes
      , e.codigoglobus EmpFil

      , (Select Count(Distinct CodIntFunc)
           From ( Select  --fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                        f.CODINTFUNC, f.CODFUNC,
                        FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                    From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                   Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                     And To_char(Ff.Competficha,'yyyymm') = v.referencia
                     And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.referencia)
                     And Ff.Codintfunc = f.Codintfunc
                     And Ff.Tipofolha = 1
                   )
           Where sit = 'A') TotalFuncAnoAtual
      , (Select Count(Distinct CodIntFunc)
           From ( Select  fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                       , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                    From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                   Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                     And To_char(Ff.Competficha,'yyyymm') = v.Referencia - decode(Substr(v.referencia,5,2),01,89,01)
                     And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.Referencia - decode(Substr(v.referencia,5,2),01,89,01))
                     And Ff.Codintfunc = f.Codintfunc
                     And Ff.Tipofolha = 1
                  )
           Where sit = 'A') TotalFuncAnoAtualVariacao

     , (Select Count(Distinct CodIntFunc)
          From ( Select -- fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                       f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.referencia-100
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.referencia-100)
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A') TotalFuncAnoAnterior
     , (Select Count(Distinct CodIntFunc)
          From ( Select -- fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.referencia-200
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.referencia-200)
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A') TotalFunc2AnosAntes

     , (Select Count(Distinct CodIntFunc)
          From ( Select  fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.referencia
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.referencia)
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A' And CodArea = 20) TotalFuncAdmAnoAtual
     , (Select Count(Distinct CodIntFunc)
          From ( Select  fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.Referencia - decode(Substr(v.referencia,5,2),01,89,01)
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.Referencia - decode(Substr(v.referencia,5,2),01,89,01))
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A' And CodArea = 20) TotalFuncAdmAnoAtualVariacao

     , (Select Count(Distinct CodIntFunc)
          From ( Select fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.referencia-100
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.referencia-100)
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A' And CodArea = 20) TotalFuncAdmAnoAnterior
     , (Select Count(Distinct CodIntFunc)
          From ( Select fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0') = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.referencia-200
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.referencia-200)
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A' And CodArea = 20) TotalFuncAdm2AnosAntes

     , (Select Count(Distinct CodIntFunc)
          From ( Select  fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.referencia
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.referencia)
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A' And CodArea = 30) TotalFuncManAnoAtual

     , (Select Count(Distinct CodIntFunc)
          From ( Select  fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.Referencia - decode(Substr(v.referencia,5,2),01,89,01)
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.Referencia - decode(Substr(v.referencia,5,2),01,89,01))
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A' And CodArea = 30) TotalFuncManAnoAtualVariacao

     , (Select Count(Distinct CodIntFunc)
          From ( Select fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.referencia-100
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.referencia-100)
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A' And  CodArea = 30) TotalFuncManAnoAnterior
     , (Select Count(Distinct CodIntFunc)
          From ( Select fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.referencia-200
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.referencia-200)
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A' And CodArea = 30) TotalFuncMan2AnosAntes

     , (Select Count(Distinct CodIntFunc)
          From ( Select  fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.referencia
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.referencia)
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A' And CodArea = 40) TotalFuncOpAnoAtual
     , (Select Count(Distinct CodIntFunc)
          From ( Select  fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0')  = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.Referencia - decode(Substr(v.referencia,5,2),01,89,01)
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.Referencia - decode(Substr(v.referencia,5,2),01,89,01))
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A' And CodArea = 40) TotalFuncOpAnoAtualVariacao

     , (Select Count(Distinct CodIntFunc)
          From ( Select fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0') = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.referencia-100
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.referencia-100)
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A' And CodArea = 40) TotalFuncOpAnoAnterior
     , (Select Count(Distinct CodIntFunc)
          From ( Select fc_Niff_RetornaAreaAnterior(f.CodIntFunc, to_number(To_char(ff.competficha,'yyyymm'))) CodArea
                      , f.CODINTFUNC, f.CODFUNC,
                       FUNC_TRAZSITUACAOFUNC(ff.codintfunc, ff.competficha, 'F',Null, f.DTADMFUNC, f.SITUACAOFUNC, f.DTTRANSFFUNC) sit
                   From PBI_vwFuncionarios f, Flp_Fichafinanceira Ff
                  Where lpad(f.codigoempresa,3,'0') || '/' || lPad(f.codigofl,3,'0') = Decode(e.Idempresa, 8, '009/002', e.codigoglobus)
                    And To_char(Ff.Competficha,'yyyymm') = v.referencia-200
                    And (f.DTTRANSFFUNC Is Null Or To_Char(f.DTTRANSFFUNC,'yyyymm') <= v.referencia-200)
                    And Ff.Codintfunc = f.Codintfunc
                    And Ff.Tipofolha = 1
                  )
           Where sit = 'A' And CodArea = 40) TotalFuncOp2AnosAntes

  From Niff_Chm_Empresas e
     , Niff_Ctb_Dre v
 Where v.fechado = 'S'
   And v.idempresa = e.Idempresa

