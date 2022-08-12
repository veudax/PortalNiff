create or replace view pbi_operacional as
Select a."ID",a."IDEMPRESA",a."IDINDICADOR",a."CODINTLINHA",a."REFERENCIA",0 META,a."VALOR",a."PONTUACAO", SubStr(a.referencia,1,4) Ano, i.descricao, l.codigoLinha
    From niff_ope_Avaliacao a, Niff_Ope_Indicadores i, Bgm_Cadlinhas l
   Where i.Id = a.idindicador
     And l.codintlinha = a.codintlinha
  Union All
  Select m.Id, m.idempresa, m.Idindicador, s.codintlinha, To_char(c.data,'yyyymm') referencia, m.meta, 0 Valor, 0 Pontuacao, To_char(c.ano) ano, i.descricao, l.codigolinha
    From niff_ope_metas m, Niff_Ope_Indicadores i, bgm_cadlinhas L
       , (Select Data, ano
            From niff_calendario
           Where dia = 1) c
       , (Select codintlinha
            From niff_ope_setorlinhas
           Where vigencia = (Select Max(Vigencia)
                               From Niff_Ope_Setorlinhas l
                              Where l.Vigencia <= trunc(Sysdate))) s
  Where i.Id = m.idindicador
    And l.codintlinha = s.codintlinha
    And c.ano > 2017 And c.ano <= To_Char(Sysdate,'yyyy')

