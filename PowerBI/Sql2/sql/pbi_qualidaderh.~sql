create or replace view pbi_qualidaderh as
select LPad(fu.codigoempresa,3,'0') || '/' || Lpad(Decode(fu.codigoempresa, 9, decode(fu.codigofl, 2, 1, fu.codigofl), fu.codigofl),3,'0') EmpFil,
       Fu.CodFunc || '-' || fu.Nomefunc Funcionario,
       fu.codfunc,
       fu.chapafunc,
       fu.nomefunc,
       Lpad(f.codocorr,3,'0') ||' - '|| f.descocorr ocorr,
       Trunc(t.dthist) data,
       f.pesoocorr,
       1 qtde,
       Decode(fu.situacaofunc, 'A', 'Ativo', 'F', 'Afastado','Desligado') StatusFuncionario
  from flp_historico t,
       frq_ocorrencia f,
       flp_funcionarios fu
 where t.dthist BETWEEN '01-jan-2017' AND ADD_MONTHS(LAST_DAY(trunc(Sysdate))+0, -1)
   and f.codocorr = t.codocorr
--   and fu.situacaofunc = 'A'
   and f.codocorr in(2,3,28,84,85,86,89,209,221,222,233,236,241,263,264,292,294,299,314,344,502,506,507,514,519,521,525,530,534,535,538,545,546,549,559,560,583,584,592,593,594,597)
   and t.codintfunc = fu.codintfunc
   and f.pesoocorr is not Null
   And fu.codigoempresa < 100
   and (fu.codintfunc, to_char(fu.codigoempresa,'000') || '/' || to_char(fu.codigofl,'000')) In
                        (select distinct movto2.codintfunc, movto2.empFil
                           From (select movto.empFil,
                                        movto.codintfunc,
                                        movto.codfunc,
                                        movto.chapafunc,
                                        movto.nomefunc,
                                        SUM(MOVTO.QTDE) qtde
                                   From (select to_char(fu.codigoempresa,'000') || '/' || to_char(fu.codigofl,'000') EmpFil,
                                                fu.codintfunc,
                                                fu.codfunc,
                                                fu.chapafunc,
                                                fu.nomefunc,
                                                1 qtde
                                           From flp_historico t,
                                                frq_ocorrencia f,
                                                flp_funcionarios fu
                                          Where t.dthist BETWEEN '01-jan-2017' AND ADD_MONTHS(LAST_DAY(trunc(Sysdate))+0, -1)
                                            and f.codocorr = t.codocorr
--                                            and fu.situacaofunc = 'A'
                                            and f.codocorr in(2,3,28,84,85,86,89,209,221,222,233,236,241,263,264,292,294,299,314,344,502,506,507,514,519,525,530,534,538,559,560,583,545,535,521,546,549,584,592,593,594,597)
                                            and t.codintfunc = fu.codintfunc
                                            and f.pesoocorr is not null) movto
                                  Group By movto.empFil,
                                           movto.codintfunc,
                                           movto.codfunc,
                                           movto.chapafunc,
                                           movto.nomefunc
                                  Order By Sum(MOVTO.QTDE) Desc) movto2
                          Where rownum < 5000)

