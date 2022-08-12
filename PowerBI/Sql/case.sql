Case When niff_valorintegracao_prodata(CUVP.CUVP_PERUSECTR-1
                                      , trunc(cu_datetime)
                                      , cu_dateTime
                                      , CUVP.ISS_ID ||'.'|| CUVP.CD_ID ||'.'|| CUVP.CRD_SNR
                                      , CUVP.ld_id
                                      , LD.LDT_CODE
                                      , NVL(CUVP.CU_SECIDENTRY,0)
                                      , NVL(CUVP.CU_SECIDEXIT,0)
                                      , CPRC.ISS_ID_RELATED
                                      , CPRC.APP_ID_RELATED
                                      , UDTM.LDR_ID
                                      , CPRC.CPRC_PERCENTRECORD) = 5.70 Then 102 Else 100 End Emtuzone_Alterado