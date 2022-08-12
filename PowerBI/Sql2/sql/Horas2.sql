Select fu.Nomefunc,Max(Data),
       Sum(TExtra) extra, Sum(TIncompletas) Incompletas,
       Case When Sum(Textra) > Sum(TIncompletas) Then Sum(TExtra) - Sum(TIncompletas) Else 0 End Credito,
       Case When Sum(Textra) > Sum(TIncompletas) Then 0 Else Sum(TIncompletas) - Sum(TExtra) End Dev
From Flp_Funcionarios fu, 
 (Select Data, horas, extra, Incompletas, codintfunc,
       ConverteDecimalEmHorasString( extra ) ExtraF, 
       ConverteDecimalEmHorasString( Incompletas ) IncompletasF,
       (Case When extra > Incompletas Then extra - Incompletas
       Else 0 End) TExtra, 
       (Case When extra < Incompletas Then Incompletas - extra
       Else 0 End) TIncompletas
From (
Select Data, horas, tipo, codintfunc,
       decode(dataferiado, data, 0, 
       Case When (tipo = 1 Or tipo = 7) And horas = 0 Then 0 
            When (tipo = 1 Or tipo = 7) And horas > 0 Then horas
       Else eEntrada + eSaida + eSaidaAlmoco + eVoltaAlmoco End) Extra,
       Decode(dataferiado, data, 0,Case When (tipo = 1 Or tipo = 7) And horas = 0 
            Then 0 Else iEntrada + iSaida + iSaidaAlmoco + iVoltaAlmoco End)  Incompletas
From(            
Select Data, dataferiado, Round(QtdHr( entrada, Saida) - QtdHr( SaidaAlmoco, VoltaAlmoco),2) horas, tipo, codintfunc,
       -- Extras
       -- entrada
       Case When entrada < To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '07:55', 'dd/mm/yyyy hh24:mi')
            Then Round(QtdHr(entrada, To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '08:00', 'dd/mm/yyyy hh24:mi')),2)
            Else 0 End eEntrada,
       --saida
       Case When Saida > To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:05', '18:05'), 'dd/mm/yyyy hh24:mi') 
            Then Round(QtdHr(To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:00', '18:00'), 'dd/mm/yyyy hh24:mi'),saida  ),2) 
            Else 0 End  eSaida, 
       -- almoço

       Case When saidaAlmoco > To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '12:00', 'dd/mm/yyyy hh24:mi')
            Then Round(QtdHr(To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '12:00', 'dd/mm/yyyy hh24:mi'), saidaAlmoco ),2)
            Else 0 End eSaidaAlmoco,
       --saida
       Case When VoltaAlmoco < To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '13:00', 'dd/mm/yyyy hh24:mi') 
            Then Round(QtdHr(VoltaAlmoco, To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '13:00', 'dd/mm/yyyy hh24:mi') ),2) 
            Else 0 End  eVoltaAlmoco,

       -- incompletas
       -- entrada
       Case When entrada > To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '08:05', 'dd/mm/yyyy hh24:mi')
            Then Round(QtdHr(To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '08:00', 'dd/mm/yyyy hh24:mi'),entrada),2)
            Else 0 End iEntrada,
       --saida
       Case When Saida < To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:00', '18:00'), 'dd/mm/yyyy hh24:mi') 
            Then Round(QtdHr(saida, To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || decode(tipo, 6, '17:00', '18:00'), 'dd/mm/yyyy hh24:mi')  ),2) 
            Else 0 End  iSaida, 
       -- almoço

       Case When saidaAlmoco < To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '12:00', 'dd/mm/yyyy hh24:mi')
            Then Round(QtdHr(saidaAlmoco, To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '12:00', 'dd/mm/yyyy hh24:mi') ),2)
            Else 0 End iSaidaAlmoco,
       --saida
       Case When VoltaAlmoco > To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '13:00', 'dd/mm/yyyy hh24:mi') 
            Then Round(QtdHr(To_Date( To_char(Data,'dd/mm/yyyy') || ' ' || '13:00', 'dd/mm/yyyy hh24:mi'), VoltaAlmoco ),2) 
            Else 0 End  iVoltaAlmoco
  From (
Select Min(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.entradigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) entrada
     , Max(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.Intidigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) SaidaAlmoco
     , Max(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.Intfdigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) VoltaAlmoco
     , Max(To_Date(To_char(f.dtdigit,'dd/mm/yyyy') || ' ' || To_Char(f.saidadigit,'hh24:mi:ss'),'dd/mm/yyyy hh24:mi:ss')) Saida
     , f.dtdigit Data
     , to_char(dtdigit,'D') tipo
     , dataferiado
     , f.codintfunc
From frq_digitacaomovimento f, niff_ads_colaboradores c,
   ( Select dataferiado From finferia_empresafilial f Where codigoempresa = 5 And codigofl = 1) ff
Where --f.codintfunc = 24125
  f.codintfunc = c.codintfunc
  And c.Idcolaborador =7 --In (Select idcolaborador From Niff_Ads_Colaboradores s Where s.Idsuperior = 7)
  And f.dtdigit Between 
  '21-jan-2018' And '20-mar-2018'
  And statusdigit = 'N'
  And ff.dataferiado(+) = f.dtdigit
Group By f.dtdigit, dataferiado, f.codintfunc
)) )) x
Where fu.codintfunc = x.codintfunc
Group By fu.Nomefunc