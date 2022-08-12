Select p.parcela, p.data, p.valor, p.Idcomunicado, a.Idcomunicado,
a.dataabertura, a.dataconfirmacao, a.processo, a.autor, a.cpfautor, a.pisautor,
decode(a.reembolso,'S','SIM','N�O') reembolso, a.resumo,
decode(a.seguro,'S','SIM','N�O') seguro, 
a.valortotal, a.valornotafiscal, a.observacao, a.favorecido, a.cpffavorecido,
a.banco, a.agencia, a.conta, e.Nome NomeEmpresa, v.Nome Vara, t.descricao Tipo,
u.nome Solicitante, a.centrocusto, Decode(a.notafiscal, 'S', 'SIM','N�O') TemNotaFiscal,
Lpad(a.referencia,6,'0') Referencia,
a.processo || '-' ||To_char(a.Dataabertura,'dd/mm/yyyy hh:mm:ss') grupo
 From niff_jur_comunicados a, niff_chm_empresas e, niff_jur_vara v, niff_jur_tipo t, niff_jur_parcela p,
 niff_chm_usuarios u
Where trunc(a.dataabertura) >= '07-feb-2017'
And a.solicitante Is Null
And a.dataconfirmacao Is Not Null
And a.idEmpresa = e.Idempresa
And a.idvara = v.idvara
And a.idtipo = t.Idtipo
And a.status = 'A'
And a.idusuario = u.idusuario
And a.Idcomunicado = p.Idcomunicado
Order By dataabertura, p.parcela

