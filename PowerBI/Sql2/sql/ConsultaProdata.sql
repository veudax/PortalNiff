Select --SF.*,
  decode(CTDT.Ctd_SfId, 3, 3, 12, 3, CTDT.Ctd_SfId) Ctd_SfId, 
   decode(CTDT.Ctd_SfId, 3, 'DEFICIENTE', 12, 'DEFICIENTE', SF.Sf_Desc) Sf_Desc 
,Sum(CTDT.Ctd_Qty) Ctd_Qty, 
Sum(CTDT.Ctd_Value) Ctd_Value, 
Nvl(Sum(CTDT.Ctd_Value), 0) tarifa,
-- TP.TP_DESC, 
-- TP.Tp_Nameoncard, 
-- COL.Tp_Id, -- CodigoEmpresa, 
-- COL.Dp_Id, 
-- COL.Cde_Id, -- CodigoCaixa 
-- COL.Cse_Id, 
-- COL.Col_Id, -- Sessao da Coleta 
-- CTMT.Ctm_Srvid, -- Servço da Coleta 
-- CTMT.Ctm_Shift, 
-- CTMT.Ctm_ServiceTag, -- ServiceTag 
-- CX.Cde_Desc, -- Nome Caixa 
-- CX.Cse_RegUser, -- Usuario Caixa 
-- CTMT.Ctm_Csn, -- Numero da Coleta 
-- FormatCard(COL.Iss_Id, COL.Cd_Id, COL.Crd_Snr) CartaoOpr, -- Cartao do Operador 
 CTMT.Ctm_LmId, -- Codigo Interno da Linha 
 LD.Ld_DescShort, -- Codigo Externo da Linha 
 CTMT.Ctm_Vehid, -- Veiculo 
 CTMT.Ctm_StaDate, -- Inicio do serviço 
 CTMT.Ctm_EndDate, -- Termino do serviço 
 CTMT.Ctm_TotMon, -- Total_Financeiro, 
 CTMT.Ctm_TotMonBot, -- Total_Pagantes, 
-- Sum(Nvl(CTDT.Ctd_Qty,0)) TotPassag -- Total de passageiros 
(Nvl(Sum(CTDT.Ctd_Qty),0) * Nvl(Sum(CTDT.Ctd_Value),0)) Total 
From 
 Collects COL, 
 CollectTranMt CTMT, 
 CollectTranDt CTDT, 
 LineDetails LD, 
 TransportProviders TP, 
 StatisticalFamilies SF,
 ( Select C.Tp_Id, C.Cde_Id, C.Cde_Desc, C.Dp_Id, D.Dp_Desc, CS.Cse_RegUser, CS.Cse_Id 
     From CashDesks C, Depots D, CashSessions CS 
    Where C.Dp_Id = D.Dp_Id And C.Tp_Id = CS.Tp_Id(+) And C.Dp_Id = CS.Dp_Id(+) And C.Cde_Id = CS.Cde_Id(+) ) CX 
Where Col.Tp_ID = CTMT.Tp_ID 
 And Col.Dp_Id = CTMT.Dp_Id 
 And Col.Cde_Id = CTMT.Cde_Id 
 And Col.Cse_Id = CTMT.Cse_Id 
 And Col.Col_Id = CTMT.Col_Id 
 And COL.Tp_Id = CX.Tp_Id(+) 
 And COL.Dp_Id = CX.Dp_Id(+) 
 And COL.Cde_Id = CX.Cde_Id(+) 
 And COL.Cse_Id = CX.Cse_Id(+) 
 And CTMT.Tp_ID = CTDT.Tp_ID(+) 
 And CTMT.Dp_Id = CTDT.Dp_Id(+) 
 And CTMT.Cde_Id = CTDT.Cde_Id(+) 
 And CTMT.Cse_Id = CTDT.Cse_Id(+) 
 And CTMT.Col_Id = CTDT.Col_Id(+) 
 And CTMT.Ctm_SrvId = CTDT.Ctm_SrvId(+) 
 And LD.Ld_Id = CTMT.Ctm_LmId 
 And TP.Tp_Id = CTMT.Tp_Id 

And CTDT.Ctd_SfId = SF.Sf_Id 
--And col.col_id = 78
 
 And CTMT.Ctm_StaDate BetWeen To_Date('06/02/2019 00:00:00','DD/MM/YYYY HH24:MI:SS') And To_Date('06/02/2019 23:59:59','DD/MM/YYYY HH24:MI:SS') 
 And CTMT.Ctm_Vehid = '1036' 
  And  Ld_DescShort like 'L01%' 
Group By 
TP.TP_DESC, 
TP.Tp_Nameoncard, 
COL.Tp_Id, 
COL.Dp_Id, 
COL.Cde_Id, 
COL.Cse_Id, 
COL.Col_Id, 
CTMT.Ctm_Srvid, 
CTMT.Ctm_Shift, 
CTMT.Ctm_ServiceTag, 
CX.Cde_Desc, 
CX.Cse_RegUser, 
CTMT.Ctm_Csn, 
--FormatCard(COL.Iss_Id, COL.Cd_Id, COL.Crd_Snr), 
CTMT.Ctm_LmId, LD.Ld_DescShort, 
CTMT.Ctm_Vehid, 
CTMT.Ctm_StaDate, 
CTMT.Ctm_EndDate, 
CTMT.Ctm_TotMon, CTDT.Ctd_Value, --CTDT.Ctd_SeqNBr,
CTMT.Ctm_TotMonBot, decode(CTDT.Ctd_SfId, 3, 3, 12, 3, CTDT.Ctd_SfId)
, decode(CTDT.Ctd_SfId, 3, 'DEFICIENTE', 12, 'DEFICIENTE', SF.Sf_Desc)
Order By COL.Tp_Id, 
 CTMT.Ctm_StaDate, 
 --FormatCard(COL.Iss_Id, COL.Cd_Id, COL.Crd_Snr), 
 CTMT.Ctm_Vehid, 
 CTMT.Ctm_LmId