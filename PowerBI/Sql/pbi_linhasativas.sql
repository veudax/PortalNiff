create or replace view pbi_linhasativas as
Select l.codintlinha,
       lPad(l.codigoempresa,3,'0') || '/' || lPad(l.codigofl,3,'0') empfil,
       l.Codigolinha || '-' || l.nomelinha Linha
    From Bgm_Cadlinhas l,
         t_trf_parametros_linha p
   Where p.codintlinha = l.codintlinha
     And p.flg_linha_disponivel = 'S'

