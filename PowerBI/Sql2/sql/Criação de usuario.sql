select t.*, t.rowid from ctr_cadastrodeusuarios t
Where usuario = upper('BCAPARECIDO');

Select * From vw_funcionarios
--Where nomefunc Like 'MARIA ESTELA%';
--Where codigoempresa = 29 And codigofl = 1 
--And situacaofunc = 'A';  
--Where codfunc = '009735' And codigoempresa = 3;
Where codintfunc = 24108;

Select * From flp_documentos d
Where d.tipodocto = 'CPF' And codintfunc = 25891;