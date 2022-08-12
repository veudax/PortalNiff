Select Sum(aquisicao) aquisicao
     , Sum(Correcao) Correcao
     , Sum(Baixa) Baixa
     , Sum(Depreciacao) Depreciacao
     , Sum(DepreciacaoAcumulada) DepreciacaoAcumulada
     , Sum(Correcao) - Sum(DepreciacaoAcumulada) SaldoATF
     ,  contaItem
     , decode(g.CodigoGrupo, Null, ContaItem, CodigoGrupo || ' G') grupo
--     , conta 
  From (Select a.codigogrupo, regexp_replace(LPAD(conta, 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','\1.\2.\3.\4') conta
          From niff_ctb_contasativo a, niff_chm_empresas e, Atfitem i
         Where e.Idempresa = a.Idempresa  
           And i.codigo = a.codigoativo 
           And e.codigoglobus = lpad(i.codigoempresa, 3, '0') || '/' || lpad(i.codigoFl, 3, '0')
           And e.idempresa = 5 ) g 
     , ( -- busca baixas totais
Select sum(i.aquisvalor) aquisicao
     , 0 correcao
     , Sum(m.depracumuladaemvlr) Baixa
     , 0 Depreciacao     
     , 0 DepreciacaoAcumulada      
     , regexp_replace(LPAD(Substr(i.conta,1,length(i.conta)-4)||'0000', 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','\1.\2.\3.\4')  contaItem
     , m.Data
     , conta
 From atfmovto m, atfitem i
Where m.codigoempresa = 3
  And m.codigofl = 1
  And m.Data between '01-oct-2019' And '31-oct-2019'
  And i.codigoempresa = m.codigoempresa
  And i.codigofl = i.codigofl
  And i.codigo = m.codigo

  And m.tipo = 'BT'

Group By Substr(i.conta,1,length(i.conta)-4)
     , m.Data
     , conta     
Union All

-- busca deprecia��es
Select sum(i.aquisvalor) aquisicao
     , Sum(m.valorcorrmesatu) correcao
     , 0 Baixa
     , Sum(m.deprnomesemvlr) depreciacao
     , Sum(m.depracumuladaemvlr) DepreciacaoAcumulada     
     , regexp_replace(LPAD(Substr(i.conta,1,length(i.conta)-4)||'0000', 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','\1.\2.\3.\4')  contaItem
     , m.Data
     , conta
 From atfmovto m, atfitem i
Where m.codigoempresa = 3
  And m.codigofl = 1
  And m.Data between '01-oct-2019' And '31-oct-2019'
  And i.codigoempresa = m.codigoempresa
  And i.codigofl = i.codigofl
  And i.codigo = m.codigo

  And m.tipo = 'DP'
  
Group By Substr(i.conta,1,length(i.conta)-4)
     , m.Data 
     , conta
     
Union All
-- traz itens que n�o tem deprecia��o mensal mas tem valor aquisitivo
Select sum(i.aquisvalor) aquisicao
     , Sum(i.aquisvalor) correcao
     , 0 Baixa
     , 0 depreciacao
     , 0 DepreciacaoAcumulada     
     , regexp_replace(LPAD(Substr(i.conta,1,length(i.conta)-4)||'0000', 10),'([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{4})','\1.\2.\3.\4')  contaItem
     , i.aquisdata
     , conta
 From atfitem i
Where i.codigoempresa = 3
  And i.codigofl = 1
  And i.iniciodeprec Is Null
  And i.aquisvalor > 0
  And i.taxadeprec = 0
  And i.databaixa Is Null

Group By Substr(i.conta,1,length(i.conta)-4)
, i.aquisdata
     , conta
 ) m
 Where m.contaItem = g.Conta(+)
 Group By  ContaItem
     , decode(g.CodigoGrupo, Null, ContaItem, CodigoGrupo || ' G') 
