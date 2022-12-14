Create Or Replace View pbi_GruposTipopagto_BI As
Select T.COD_TIPOPAGTO CODIGO, 'DINHEIRO' GRUPO, T.NOM_DESCRICAO
  From T_TRF_TIPOPAGTO T
 Where T.COD_TIPOPAGTO In ('001','01','200','300','304','309','325','333','363','400','403','600','909','949','I','O','R','U','V','W')
 Union All
Select T.COD_TIPOPAGTO CODIGO, 'ESTUDANTE' GRUPO, T.NOM_DESCRICAO
  From T_TRF_TIPOPAGTO T
 Where T.COD_TIPOPAGTO In ('02','05','06','17','21','26','40','56','203','218','221','222','303','322','328','329','334','348',
'356','361','367','372','382','387','404','405','414','420','421','422','432','438','442','452','457','466','472','606','608',
'612','613','624','632','902','928','929','930','955','959','T')
 Union All
Select T.COD_TIPOPAGTO CODIGO, 'GRATUIDADE' GRUPO, T.NOM_DESCRICAO
  From T_TRF_TIPOPAGTO T
 Where T.COD_TIPOPAGTO In ('04','11','12','13','15','15S','16','19','22','23','32','33','34','43','45','47','49','50','51','53','54','57','58',
'204','205','207','208','209','210','211','212','214','215','220','226','308','317','318','319','320','321','323','324','330','331','332',
'339','340','341','343','344','346','402','417','603','604','605','607','611','614','616','617','618','619','622','623','628','629','630',
'633','634','900','903','904','905','911','913','914','920','921','922','923','924','925','926','927','931','933','934','935','956','C')
 Union All
Select T.COD_TIPOPAGTO CODIGO, 'VT' GRUPO, T.NOM_DESCRICAO
  From T_TRF_TIPOPAGTO T
 Where T.COD_TIPOPAGTO Not In ('04','11','12','13','15','15S','16','19','22','23','32','33','34','43','45','47','49','50','51','53','54','57','58',
'204','205','207','208','209','210','211','212','214','215','220','226','308','317','318','319','320','321','323','324','330','331','332',
'339','340','341','343','344','346','402','417','603','604','605','607','611','614','616','617','618','619','622','623','628','629','630',
'633','634','900','903','904','905','911','913','914','920','921','922','923','924','925','926','927','931','933','934','935','956','C',
'001','01','200','300','304','309','325','333','363','400','403','600','909','949','I','O','R','U','V','W',
'02','05','06','17','21','26','40','56','203','218','221','222','303','322','328','329','334','348',
'356','361','367','372','382','387','404','405','414','420','421','422','432','438','442','452','457','466','472','606','608',
'612','613','624','632','902','928','929','930','955','959','T','X')
 