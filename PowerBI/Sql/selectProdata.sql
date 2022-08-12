SELECT CUA.USR_ID,
          CUVP.LD_ID,
          LD_DESCSHORT,
          UDTM.LDR_ID,
          /* incluido */
          CUVP.CU_SECIDENTRY,
          CUVP.CU_SECIDEXIT,
          CPRC.ISS_ID_RELATED,
          CPRC.APP_ID_RELATED,
          /* já tinha */
          CASE
             WHEN     LD.LDT_CODE = 6
                  AND NVL (CUVP.CU_SECIDENTRY, 0) > 0
                  AND NVL (CUVP.CU_SECIDEXIT, 0) > 0
             THEN
                MERCURY.LIN_GETZONEEMTU (17001,
                                 CPRC.ISS_ID_RELATED,
                                 CPRC.APP_ID_RELATED,
                                 CUVP.LD_ID,
                                 CUVP.CU_SECIDENTRY,
                                 CUVP.CU_SECIDEXIT)
             ELSE
                CAST (
                   NVL (MERCURY.GET_SYSTEMPARAMETERS ('GENERAL', 'EMTUZONEDEFAULT'),
                        0) AS NUMBER)
          END
             EMTUZONE,
             LD.LDT_CODE,
          CASE
             WHEN     LD.LDT_CODE = 6
                  AND NVL (CUVP.CU_SECIDENTRY, 0) > 0
                  AND NVL (CUVP.CU_SECIDEXIT, 0) > 0
             THEN
                  MERCURY.LIN_GETZONEPRICE (09001,
                                    CPRC.ISS_ID_RELATED,
                                    CPRC.APP_ID_RELATED,
                                    CUVP.LD_ID, --- SELECT *FROM MERCURY.CARDUSAGEVALIDPASSENGERS
                                    CUVP.CU_SECIDENTRY,
                                    CUVP.CU_SECIDEXIT,
                                    1,
                                    UDTM.LDR_ID,
                                    CUVP.CU_DATETIME)
                * (CPRC.CPRC_PERCENTRECORD / 100)/ 100 /* incluido a divisão para trazer em moeda */
             ELSE
                  MERCURY.LIN_GETLINEPRICE (09001,
                                    CPRC.ISS_ID_RELATED,
                                    CPRC.APP_ID_RELATED,
                                    CUVP.LD_ID,
                                    UDTM.LDR_ID,
                                    CUVP.CU_DATETIME)
                * (CPRC.CPRC_PERCENTRECORD / 100) /100 /* incluido a divisão para trazer em moeda */
          END
             PRICE,

          /* incluido */
          niff_valorintegracao_prodata(CUVP.CUVP_PERUSECTR-1
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
                                      , CPRC.CPRC_PERCENTRECORD) Valor_Real,
                                      
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
                                      , CPRC.CPRC_PERCENTRECORD) = 5.70 Then 102 Else 100 End Emtuzone_Alterado,

           /* já tinha */
          CUVP.CU_DATETIME,
          UDF.UDF_RECEIVEDATE,
          CUVP.CU_VEHID,
          CUVP.ISS_ID ||'.'|| CUVP.CD_ID ||'.'|| CUVP.CRD_SNR AS CARD,
          CUVP.CUVP_PERUSECTR,
          CUA.APP_ID,
          APP.APP_DESCSHORT
     FROM MERCURY.CARDSXUSERSXAPPLICATIONS CUA
          INNER JOIN MERCURY.APPLICATIONS APP
             ON APP.ISS_ID = CUA.ISS_ID AND APP.APP_ID = CUA.APP_ID
          INNER JOIN MERCURY.CARDUSAGEVALIDPASSENGERS CUVP
             ON     CUA.ISS_ID = CUVP.ISS_ID
                AND CUA.CD_ID = CUVP.CD_ID
                AND CUA.CRD_SNR = CUVP.CRD_SNR
                AND CUA.APP_ISS_ID = CUVP.APP_ISS_ID
                AND CUA.APP_ID = CUVP.APP_ID
                AND CUVP.CUT_ID = 1
                AND CU_DATETIME BETWEEN TO_DATE ('01/08/2020 00:00:00','DD/MM/YYYY HH24:MI:SS') AND TO_DATE ('30/09/2020 23:59:59','DD/MM/YYYY HH24:MI:SS')
          INNER JOIN MERCURY.LINEDETAILS LD
             ON CUVP.LD_ID = LD.LD_ID
             AND CUVP.LD_ID IN (SELECT LD_ID FROM mercury.linedetails where ld_status= 'A' and lm_id in (select lm_id from mercury.linegroupsxlinemt where lg_id= 11))
          INNER JOIN MERCURY.USAGEDATATRIPMT UDTM
             ON CUVP.UDTM_ID = UDTM.UDTM_ID
          INNER JOIN MERCURY.USAGEDATASERVICE UDS
             ON UDTM.UDS_ID = UDS.UDS_ID
          INNER JOIN MERCURY.USAGEDATAFILE UDF
             ON UDS.UDF_ID = UDF.UDF_ID
          INNER JOIN MERCURY.CORRELATIONPRICE CPRC
             ON CPRC.ISS_ID = APP.ISS_ID AND CPRC.APP_ID = APP.APP_ID
    WHERE     CUA.ISS_ID = MERCURY.GET_SYSTEMPARAMETERS ('GENERAL', 'ISSUERID')
          AND CUA.APP_ID IN
                 (    SELECT REGEXP_SUBSTR(
                                MERCURY.GET_SYSTEMPARAMETERS ('GENERAL', 'APPVIEWUSEPERIODFARE'),
                                Trim(' [^,]+'),1, LEVEL)
                        FROM DUAL
                  CONNECT BY REGEXP_SUBSTR (
                                MERCURY.GET_SYSTEMPARAMETERS ('GENERAL', 'APPVIEWUSEPERIODFARE'),
                                Trim(' [^,]+'), 1, LEVEL)
                                IS NOT NULL)
          AND ( (-1 = (NVL ( MERCURY.GET_SYSTEMPARAMETERS ('GENERAL', 'TPIDVIEWUSEPERIOD'), -1)))
               OR (UDF.TP_ID = (NVL (MERCURY.GET_SYSTEMPARAMETERS ('GENERAL', 'TPIDVIEWUSEPERIOD'), -1))))
 /* incluido para teste */
--  and CUA.USR_ID = 281867
--And CUVP.ISS_ID ||'.'|| CUVP.CD_ID ||'.'|| CUVP.CRD_SNR = '40.17.7016335'
order by card, cu_datetime, CUVP.CUVP_PERUSECTR