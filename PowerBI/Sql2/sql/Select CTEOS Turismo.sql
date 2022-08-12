Select o.chave_de_acesso, n.numeronf, n.codtpdoc, n.coddoctoesf From Bgm_Cte_Os o, tur_notafiscal n
Where o.empresa =4
And o.id_documento = n.codintnf