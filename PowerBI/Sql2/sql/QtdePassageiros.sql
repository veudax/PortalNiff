-- Pagantes
Select Sum(Decode(a.Cod_Tipopagtarifa,
                  '403',
                  0,
                  Decode(a.Cod_Tipopagtarifa, 'X', -1, '956', -1, 1)) *
           a.Qtd_Passag_Trans) Valor,
           To_Char(g.Dat_Prest_Contas,'yyyymm')
  From t_Arr_Guia g, t_Arr_Detalhe_Guia a, t_Trf_Tipopagto p 
 Where p.Cod_Tipopagto = a.Cod_Tipopagtarifa
   And g.Cod_Seq_Guia = a.Cod_Seq_Guia
   And a.Vlr_Receb <> 0
   And g.Dat_Prest_Contas Between To_Date('01/01/2018', 'dd/mm/yyyy') And 
       To_Date('31/12/2018', 'dd/mm/yyyy')
   And g.Cod_Empresa = 2
   And g.Codigofl = 1
   Group By  To_Char(g.Dat_Prest_Contas,'yyyymm');
   
--Integrações sem valor
Select Sum(Decode(a.Cod_Tipopagtarifa,
                  '403',
                  0,
                  Decode(a.Cod_Tipopagtarifa, 'X', -1, '956', -1, 1)) *
           a.Qtd_Passag_Trans) Valor,  To_Char(g.Dat_Prest_Contas,'yyyymm')
  From t_Arr_Guia g, t_Arr_Detalhe_Guia a, t_Trf_Tipopagto p
 Where p.Cod_Tipopagto = a.Cod_Tipopagtarifa
   And g.Cod_Seq_Guia = a.Cod_Seq_Guia
   And Upper(p.Nom_Descricao) Like '%INT%' 
   And a.Vlr_Receb > 0
   And g.Dat_Prest_Contas Between To_Date('01/01/2018', 'dd/mm/yyyy') And
       To_Date('31/12/2018', 'dd/mm/yyyy')
   And g.Cod_Empresa = 2
   And g.Codigofl = 1
   Group By  To_Char(g.Dat_Prest_Contas,'yyyymm');

-- Gratuidade
Select Sum(Decode(a.Cod_Tipopagtarifa,
                  '403',
                  0,
                  Decode(a.Cod_Tipopagtarifa, 'X', -1, '956', -1, 1)) *
           a.Qtd_Passag_Trans) Valor,  To_Char(g.Dat_Prest_Contas,'yyyymm')
  From t_Arr_Guia g, t_Arr_Detalhe_Guia a, t_Trf_Tipopagto p
 Where p.Cod_Tipopagto = a.Cod_Tipopagtarifa
   And g.Cod_Seq_Guia = a.Cod_Seq_Guia
   And Upper(p.Nom_Descricao) Not Like '%INT%'
   And a.Vlr_Receb = 0 
   And g.Dat_Prest_Contas Between To_Date('01/01/2018', 'dd/mm/yyyy') And
       To_Date('31/12/2018', 'dd/mm/yyyy')
   And g.Cod_Empresa = 2
   And g.Codigofl = 1
   Group By  To_Char(g.Dat_Prest_Contas,'yyyymm');
