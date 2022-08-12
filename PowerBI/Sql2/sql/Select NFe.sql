Select e.codintnf, e.coddoctoesf, e.numeronf, e.serienf,
       e.dataemissao,  e.statusnf,  e.codclassfisc,  e.codtpdoc,
       e.dadosadicionais,
       e.basecalcicms, e.valoricms, e.aliquotaicms, e.icmsisentas, e.icmsoutras,
       e.valortotalnf, e.valorfrete, e.valorseguro, e.valor_desconto, e.valoroutrasdespesas,
       e.basecalcicms_subst, e.valoricms_subst, e.valorbaseipi, e.valoraliqipi, e.valoraliqipi, 
       n.chavedeacesso, n.status, n.mensagemrecibo, n.recibo, n.protocolo, n.data_protocolo,
       decode(e.codcli, Null, f.nrforn || ' - ' || f.Rsocialforn, c.nrcli || ' - ' || c.rsocialcli) ClienteFornecedor 
  From esfnotafiscal E,
       bgm_notafiscal_eletronica N,
       Bgm_Cliente C,
       bgm_fornecedor F
 Where n.numeronfe = 4403
   And n.codintnf_esfnf = e.codintnf
   And c.codcli(+) = e.codcli
   And f.codigoforn(+) = e.codigoforn







