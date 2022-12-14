create or replace view pbi_qualidaderh as
Select Grupo, EmpFil, Funcionario, codfunc, chapafunc, nomefunc, ocorr, Data, pesoocorr, qtde, StatusFuncionario, codArea, DESCAREA, DESCFUNCAO
  From (Select Codocorr, descocorr, 'Absenteísmo' grupo From frq_ocorrencia
         Where codocorr In (3, 238, 504,505,506,507,521,526,527,534,535,541,542,552,553,560,579,580,581,582,583,584,592,593,601,602,608,609)
         Union All
        Select Codocorr, descocorr, 'Outras' grupo From frq_ocorrencia
         Where codocorr Not In (3, 238, 504,505,506,507,521,526,527,534,535,541,542,552,553,560,579,580,581,582,583,584,592,593,601,602,608,609)) O,
       (Select LPad(fu.codigoempresa,3,'0') || '/' || Lpad(Decode(fu.codigoempresa, 9, decode(fu.codigofl, 2, 1, fu.codigofl), fu.codigofl),3,'0') EmpFil,
               Fu.CodFunc || '-' || fu.Nomefunc Funcionario,
               fu.codfunc,
               fu.chapafunc,
               fu.nomefunc,
               f.codocorr,
               Lpad(f.codocorr,3,'0') ||' - '|| f.descocorr ocorr,
               Trunc(t.dthist) data,
               f.pesoocorr,
               1 qtde,
               Decode(fu.situacaofunc, 'A', 'Ativo', 'F', 'Afastado','Desligado') StatusFuncionario,
               fu.CODAREA, fu.DESCAREA, fu.DESCFUNCAO
          from flp_historico t,
               frq_ocorrencia f,
               PBI_vwFuncionarios fu
         where trunc(t.dthist) BETWEEN '01-jan-2017' AND trunc(Sysdate)--ADD_MONTHS(LAST_DAY(trunc(Sysdate))+0, -1)
           and f.codocorr = t.codocorr
         --   and fu.situacaofunc = 'A'
           and (f.codocorr In (2,3,28,84,85,86,89,209,221,222,233,236,238,241,263,264,292,294,299,314,344,
--                               500,501,--< novos
                               502,504,505,506,507,
--                               509,512,513,--< novos
                               514,519,
--                               520, --< novos
                               521,
--                               522,523,524, --< novos
                               525,526,527,
--                               529,--< novos
                               530,
--                               531,532,533, --< novos
                               534,535,
--                             536,537,--< novos
                               538,
--                               539,--< novos
                               541,542,
--                               544,--< novos
                               545,546,
--                               548,--< novos
                               549,
--                               550,551,--< novos
                               559,560,
--                               563,564,565,566,567,568,571,572,573,575,--< novos
                               579,580,581,582,583,584,
--                               585,586,588,591,--< novos
                               592,593,594,597,601,602,608,609)
            Or (fu.CODAREA = 40 And f.codocorr In (552,553)))
           and t.codintfunc = fu.codintfunc
           and f.pesoocorr is not Null
           And fu.codigoempresa < 100) r
  Where o.Codocorr = r.Codocorr
    And CODAREA In (20,30,40,80)

