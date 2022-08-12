create or replace view pbi_linhas as
Select lPad(l.codigoempresa,3,'0') || '/' || lPad(l.codigofl,3,'0') empfil,
       t.codigo Terminal, ft.data,
       l.Codigolinha || '-' || l.nomelinha Linha,
       l.codintlinha,
       ff.CODFUNC || '-' || ff.nomefunc Fiscal,
       fi.CODFUNC || '-' || fi.nomefunc Inspetor,
       fc.CODFUNC || '-' || fc.nomefunc Coordenador,
       ff.codintfunc CodigoFiscal,
       fi.codintfunc CodigoInspetor,
       fc.Codintfunc CodigoCoordenador

  From bgm_cadlinhas l,
       niff_pbi_linhasporservico s,
       niff_pbi_terminallinhas t,
       niff_pbi_fiscaisdoterminal ft,
       niff_pbi_inspetoresfiscais i,
       niff_pbi_coordinspetores c,
       flp_funcionarios ff,
       flp_funcionarios fi,
       flp_funcionarios fc
Where s.codintlinha = l.codintlinha
  And t.idterminal = s.idterminal
  And ft.idterminal = t.idterminal
  And ft.codintfunc = ff.codintfunc
  And ft.codintfunc = i.codintfunc_fiscal
  And i.codintfunc_inspetor = fi.codintfunc
  And i.codintfunc_fiscal = ft.codintfunc
  And i.codintfunc_inspetor = c.codintfunc_inspetor
  And fc.codintfunc = c.codintfunc_coord
  And i.Data = c.Data
  And ft.Data = i.data

