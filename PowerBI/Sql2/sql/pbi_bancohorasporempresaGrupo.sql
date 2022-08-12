create or replace view pbi_bancohorasporempresaGrupo as
Select Ano, AnoMes, MesAno, EmpFil, Area, Grupo
     , Sum(SaldoMinutos) SaldoMinutos, Sum(MinutosPositivos) MinutosPositivos, Sum(MinutosNegativos) MinutosNegativos
     , mintoqtdhrsmin(Sum(SaldoMinutos)) horas
     , mintoqtdhrsmin(Sum(MinutosPositivos)) horasPositivas
     ,  mintoqtdhrsmin(Sum(MinutosNegativos)) horasnegativas
  From (

Select Ano, AnoMes, MesAno, EmpFil, Area, Grupo
       , SaldoAntEmMinutos + creditoEmMinutos - debitoEmMinutos SaldoMinutos
       , mintoqtdhrsmin(SaldoAntEmMinutos + creditoEmMinutos - debitoEmMinutos) horas
       , Case When SaldoAntEmMinutos + creditoEmMinutos - debitoEmMinutos > 0 Then
            SaldoAntEmMinutos + creditoEmMinutos - debitoEmMinutos
         Else 0 End MinutosPositivos
       , Case When SaldoAntEmMinutos + creditoEmMinutos - debitoEmMinutos < 0 Then
            SaldoAntEmMinutos + creditoEmMinutos - debitoEmMinutos
         Else 0 End MinutosNegativos
    From (
    select f.Nomefunc, f.CODFUNC || '-' || f.NOMEFUNC nome, f.codintfunc
         , to_char(h.competencia,'yyyy') Ano
         , to_char(h.competencia,'yyyymm') AnoMes
         , to_char(h.competencia,'mm/yyyy') MesAno
         , Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(Decode(f.Codigoempresa, 9, Decode(f.Codigofl, 2, 1, f.Codigofl), f.Codigofl), 3, '0') EmpFil
         , f.DESCAREA Area
         , Decode(f.SITUACAOFUNC,'A','Ativo','F','Afastado','Desligado') Situacao
         , Decode(g.descgrupo, null, f.descarea, decode(g.descgrupo,'FISCALIZAÇÃO','FISCAL RODOVIARIO', g.descgrupo)) Grupo
         , Case When trunc(abs(h.saldoanterior)) > 0 Then
              Case When h.saldoanterior < 0 Then
                (trunc(h.saldoanterior) * 60) - (Abs(h.saldoanterior) - trunc(Abs(h.saldoanterior))) *100
              Else
                (trunc(h.saldoanterior) * 60) + (Abs(h.saldoanterior) - trunc(Abs(h.saldoanterior))) *100
              End
           Else
               h.saldoAnterior *100  End SaldoAntEmMinutos
         , Case When trunc(abs(h.debito)) > 0 Then
             (trunc(h.debito) *60) + (h.debito - trunc(Abs(h.debito))) *100
           Else
             h.debito *100  End debitoEmMinutos
         , Case When trunc(abs(h.credito)) > 0 Then
            (trunc(h.credito) *60) + (h.credito - trunc(Abs(h.credito))) *100
           Else
             h.credito *100  End creditoEmMinutos
      from frq_bancohoras h, PBI_vwFuncionarios f
         , Flp_Funcionarios_Grupo fg, Flp_Grupo g
     Where h.competencia Between trunc(Sysdate,'rr') and Sysdate
       And codigoempresa || codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
       And f.codintfunc = h.codintfunc
       And f.SITUACAOFUNC <> 'D'
       And fg.codgrupo = g.codgrupo(+) 
       And f.codintfunc = fg.codintfunc(+)
       )
) Group By Ano, AnoMes, MesAno, EmpFil, Area, grupo

