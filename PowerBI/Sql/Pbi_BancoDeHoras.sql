create or replace view pbi_bancohoras as
Select Nome, NomeFunc, Ano, AnoMes, MesAno, EmpFil, Area, Situacao, codintfunc
       , SaldoAntEmMinutos + creditoEmMinutos - (debitoEmMinutos + vlpagoEmMinutos) SaldoMinutos
       , mintoqtdhrsmin(SaldoAntEmMinutos + creditoEmMinutos - (debitoEmMinutos + vlpagoEmMinutos)) horas
       , Case When SaldoAntEmMinutos + creditoEmMinutos - (debitoEmMinutos + vlpagoEmMinutos) > 0 Then
            SaldoAntEmMinutos + creditoEmMinutos - (debitoEmMinutos + vlpagoEmMinutos)
         Else 0 End MinutosPositivos
       , Case When SaldoAntEmMinutos + creditoEmMinutos - debitoEmMinutos < 0 Then
            SaldoAntEmMinutos + creditoEmMinutos - (debitoEmMinutos + vlpagoEmMinutos)
         Else 0 End MinutosNegativos
       , Round(Case When SaldoAntEmMinutos + creditoEmMinutos - (debitoEmMinutos + vlpagoEmMinutos) > 0 Then
            Case When Empfil = '026/003' Then
            /* até 60 horas incrementa 60% no salariobasehora */
               Case When SaldoAntEmMinutos + creditoEmMinutos - (debitoEmMinutos + vlpagoEmMinutos) <= 3600 Then
                  Round(((salarioHora * 0.60) + salarioHora),2) * mintoqtdhrsmin(SaldoAntEmMinutos + creditoEmMinutos - (debitoEmMinutos + vlpagoEmMinutos))
               Else /* passou de 60horas a diferença incrementa 70 no salariobase horas*/
                  (Round(((salarioHora * 0.60) + salarioHora),2) * 60) +
                  (Round(((salarioHora * 0.70) + salarioHora),2) * (mintoqtdhrsmin(SaldoAntEmMinutos + creditoEmMinutos - (debitoEmMinutos + vlpagoEmMinutos)) - 60))
               End
            Else
               Case When Empfil = '029/00' Then
                 Round(((salarioHora * 0.60) + salarioHora),2) * mintoqtdhrsmin(SaldoAntEmMinutos + creditoEmMinutos - (debitoEmMinutos + vlpagoEmMinutos))
               Else
                 Round(((salarioHora * 0.50) + salarioHora),2) * mintoqtdhrsmin(SaldoAntEmMinutos + creditoEmMinutos - (debitoEmMinutos + vlpagoEmMinutos))
               End
            End
         Else
            0
         End,2) ValorExtra
    From (
    select f.Nomefunc, f.CODFUNC || '-' || f.NOMEFUNC nome, f.codintfunc
         , to_char(h.competencia,'yyyy') Ano
         , to_char(h.competencia,'yyyymm') AnoMes
         , to_char(h.competencia,'mm/yyyy') MesAno
         , Lpad(f.Codigoempresa, 3, '0') || '/' || Lpad(Decode(f.Codigoempresa, 9, Decode(f.Codigofl, 2, 1, f.Codigofl), f.Codigofl), 3, '0') EmpFil
         , f.DESCAREA Area
         , Decode(f.SITUACAOFUNC,'A','Ativo','F','Afastado','Desligado') Situacao
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
         , Case When trunc(abs(nvl(h.valorpago,0))) > 0 Then
            (trunc(nvl(h.valorpago,0)) *60) + (nvl(h.valorpago,0) - trunc(Abs(nvl(h.valorpago,0)))) *100
           Else
             nvl(h.valorpago,0) *100  End vlpagoEmMinutos
         , Decode(f.TPSALFUNCAO, 'M', f.SALBASE / 220, f.SALBASE) salarioHora
      from frq_bancohoras h, vw_funcionarios f
     Where h.competencia Between trunc(Sysdate,'rr') and Sysdate
       And codigoempresa || codigofl In (11,12,21,31,41,51,61,92,131,261,263,291)
       And f.SITUACAOFUNC <> 'D'
       And f.codintfunc = h.codintfunc )

