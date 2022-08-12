create or replace view pbi_utilizacaosuportte as
Select count(*) quantidade, trunc(Data) Data, u.usuarioacesso, u.Nome, to_char(Data, 'yyyy') Ano
  , to_char(Data, 'mm/yyyy') MesAno, to_char(Data, 'yyyymm') Anomes
  From NIFF_CHM_Log l, Niff_Chm_Usuarios u
Where u.Idusuario = l.Idusuario
  And ((fc_niff_LongToVarchar(l.idlog) Like '%logoff%' And trunc(Data) < '08-dec-2018')
   Or  (fc_niff_LongToVarchar(l.idlog) Like '%login%' And trunc(Data) > '08-dec-2018'))
Group By trunc(Data), u.usuarioacesso, u.Nome,
to_char(Data, 'yyyy'), to_char(Data, 'mm/yyyy'), to_char(Data, 'yyyymm')

