create or replace procedure Pr_Niff_relHistoSalarial(pEmpresa in Number,
                                                     pFilial in Number,
                                                     pRegInicial in Varchar2,
                                                     pRegFinal in Varchar2,
                                                     pMesAnoInicial in Varchar2,
                                                     pMesAnoFinal in Varchar2,
                                                     pTipoFolha in Number) is

  Cursor cFicha is
    Select f.CODFUNCAO, 
           f.CODIGOEMPRESA, 
           f.CODIGOFL, 
           f.CODFUNC || ' ' || f.NOMEFUNC colaborador,      
           Lpad(e.codigoempresa,3,'0') || ' ' || ea.rsocialempresa Empresa,
           Lpad(e.codigofl,3,'0') || ' ' || ea.rsocialempresa Filial, 
           f.CODFUNCAO || ' ' || fc.descfuncao funcao, 
           '  ' || LPad(ev.codevento,5,'0') || ' ' || ev.desceven evento,
           fe.* 
    from vw_funcionarios f,
         flp_fichaeventos fe,
         ctr_empautorizadas ea,
         ctr_filial e,                                 
         flp_funcao fc, 
         flp_eventos ev
   where fe.codintfunc = f.codintfunc
     and fe.tipofolha = pTipoFolha
     and fe.competficha between pDataInicial and pDataFinal
     and f.codfunc between pRegInicial and pRegFinal
     and f.codigoempresa = pEmpresa
     and f.codigofl = pfilial    
     and e.codigoempresa = f.CODIGOEMPRESA
     and e.codigofl = f.CODIGOFL
     and ea.codintempaut = e.codintempaut
     and fc.codfuncao = f.CODFUNCAO 
     and ev.codevento = fe.codevento
     order by f.codigoempresa, f.codigofl, f.codfunc, fe.codevento, fe.competficha;
       
  vTemFichaEventoData varchar2(1); -- S/N    
  vCodIntFunc number;                         
  vCodEvento number;
  vData Date; 
  vContColuna number;     
  vIp Varchar2(25);
  
  
  Procedure Grava_Coluna1(Data Date, Referencia Number, Valor Number, Ip varchar2) is
  begin
    update Niff_Flprelhistfinc
       set Data1 = Data,
           Refer1 = Referencia,
           Valor1 = Valor,
           ReferTotal = Referencia,
           ValorTotal = Valor
      where ip = Ip
        and CodEvento = vCodEvento
        and CodigoEmpresa = pEmpresa
        and CodigoFl = pFilial
        and CodIntFunc = vCodIntFunc;
  end;
         
Procedure Grava_Coluna2(Data Date, Referencia Number, Valor Number, Ip varchar2) is
  begin
    update Niff_Flprelhistfinc
       set Data2 = Data,
           Refer2 = Referencia,
           Valor2 = Valor,
           ReferTotal = ReferTotal + Referencia,
           ValorTotal = ValorTotal + Valor
      where ip = Ip
        and CodEvento = vCodEvento
        and CodigoEmpresa = pEmpresa
        and CodigoFl = pFilial
        and CodIntFunc = vCodIntFunc;
  end;
  
Procedure Grava_Coluna3(Data Date, Referencia Number, Valor Number, Ip varchar2) is
  begin
    update Niff_Flprelhistfinc
       set Data3 = Data,
           Refer3 = Referencia,
           Valor3 = Valor,
           ReferTotal = ReferTotal + Referencia,
           ValorTotal = ValorTotal + Valor
      where ip = Ip
        and CodEvento = vCodEvento
        and CodigoEmpresa = pEmpresa
        and CodigoFl = pFilial
        and CodIntFunc = vCodIntFunc;
  end;
  
Procedure Grava_Coluna4(Data Date, Referencia Number, Valor Number, Ip varchar2) is
  begin
    update Niff_Flprelhistfinc
       set Data4 = Data,
           Refer4 = Referencia,
           Valor4 = Valor,
           ReferTotal = ReferTotal + Referencia,
           ValorTotal = ValorTotal + Valor
      where ip = Ip
        and CodEvento = vCodEvento
        and CodigoEmpresa = pEmpresa
        and CodigoFl = pFilial
        and CodIntFunc = vCodIntFunc;
  end;
  
Procedure Grava_Coluna5(Data Date, Referencia Number, Valor Number, Ip varchar2) is
  begin
    update Niff_Flprelhistfinc
       set Data5 = Data,
           Refer5 = Referencia,
           Valor5 = Valor,
           ReferTotal = ReferTotal + Referencia,
           ValorTotal = ValorTotal + Valor
      where ip = Ip
        and CodEvento = vCodEvento
        and CodigoEmpresa = pEmpresa
        and CodigoFl = pFilial
        and CodIntFunc = vCodIntFunc;
  end;        

Procedure Grava_Coluna6(Data Date, Referencia Number, Valor Number, Ip varchar2) is
  begin
    update Niff_Flprelhistfinc
       set Data6 = Data,
           Refer6 = Referencia,
           Valor6 = Valor,
           ReferTotal = ReferTotal + Referencia,
           ValorTotal = ValorTotal + Valor
      where ip = Ip
        and CodEvento = vCodEvento
        and CodigoEmpresa = pEmpresa
        and CodigoFl = pFilial
        and CodIntFunc = vCodIntFunc;
  end;        
     
Procedure Grava_Coluna7(Data Date, Referencia Number, Valor Number, Ip varchar2) is
  begin
    update Niff_Flprelhistfinc
       set Data7 = Data,
           Refer7 = Referencia,
           Valor7 = Valor,
           ReferTotal = ReferTotal + Referencia,
           ValorTotal = ValorTotal + Valor
      where ip = Ip
        and CodEvento = vCodEvento
        and CodigoEmpresa = pEmpresa
        and CodigoFl = pFilial
        and CodIntFunc = vCodIntFunc;
  end;        
    
Procedure Grava_Coluna8(Data Date, Referencia Number, Valor Number, Ip varchar2) is
  begin
    update Niff_Flprelhistfinc
       set Data8 = Data,
           Refer8 = Referencia,
           Valor8 = Valor,
           ReferTotal = ReferTotal + Referencia,
           ValorTotal = ValorTotal + Valor
      where ip = Ip
        and CodEvento = vCodEvento
        and CodigoEmpresa = pEmpresa
        and CodigoFl = pFilial
        and CodIntFunc = vCodIntFunc;
  end;        
  
Procedure Grava_Coluna9(Data Date, Referencia Number, Valor Number, Ip varchar2) is
  begin
    update Niff_Flprelhistfinc
       set Data9 = Data,
           Refer9 = Referencia,
           Valor9 = Valor,
           ReferTotal = ReferTotal + Referencia,
           ValorTotal = ValorTotal + Valor
      where ip = Ip
        and CodEvento = vCodEvento
        and CodigoEmpresa = pEmpresa
        and CodigoFl = pFilial
        and CodIntFunc = vCodIntFunc;
  end;        
 
Procedure Grava_Coluna10(Data Date, Referencia Number, Valor Number, Ip varchar2) is
  begin
    update Niff_Flprelhistfinc
       set Data10 = Data,
           Refer10 = Referencia,
           Valor10 = Valor,
           ReferTotal = ReferTotal + Referencia,
           ValorTotal = ValorTotal + Valor
      where ip = Ip
        and CodEvento = vCodEvento
        and CodigoEmpresa = pEmpresa
        and CodigoFl = pFilial
        and CodIntFunc = vCodIntFunc;
  end;        
      
Procedure Grava_Coluna11(Data Date, Referencia Number, Valor Number, Ip varchar2) is
  begin
    update Niff_Flprelhistfinc
       set Data11 = Data,
           Refer11 = Referencia,
           Valor11 = Valor,
           ReferTotal = ReferTotal + Referencia,
           ValorTotal = ValorTotal + Valor
      where ip = Ip
        and CodEvento = vCodEvento
        and CodigoEmpresa = pEmpresa
        and CodigoFl = pFilial
        and CodIntFunc = vCodIntFunc;
  end;        
                 
Procedure Grava_Coluna12(Data Date, Referencia Number, Valor Number, Ip varchar2) is
  begin
    update Niff_Flprelhistfinc
       set Data12 = Data,
           Refer12 = Referencia,
           Valor12 = Valor,
           ReferTotal = ReferTotal + Referencia,
           ValorTotal = ValorTotal + Valor
      where ip = Ip
        and CodEvento = vCodEvento
        and CodigoEmpresa = pEmpresa
        and CodigoFl = pFilial
        and CodIntFunc = vCodIntFunc;
  end;        

begin  
  vIp := func_trazterminal;
  
  /*delete Niff_Flprelhistfinc
   where ip = vIp;*/
                    
  vCodIntFunc := 0;
  vCodEvento  := 0;
  vContColuna := 1;   
  
  For rFicha In cFicha Loop                                                                 
            
    if vCodIntFunc = 0 then
       vCodIntFunc := rFicha.CodIntFunc; 
       vCodEvento := rFicha.Codevento;
       vTemFichaEventoData := 'N';
       vContColuna := 1;   
       vData := Last_day(pDataInicial+1);    
    else
       if vCodIntFunc = rFicha.CodIntFunc and
          vCodEvento = rFicha.Codevento then
         vTemFichaEventoData := 'S';       
       else
         vTemFichaEventoData := 'N';
         vCodIntFunc := rFicha.CodIntFunc; 
         vCodEvento := rFicha.Codevento;
         vContColuna := 1;   
         vData := Last_day(pDataInicial+1);    
       end if;
    end if;
        
            
    if vTemFichaEventoData = 'N' then      
      insert into Niff_FLPRelHistFinc          
       (ip, 
        codevento, 
        codigoempresa, 
        codigofl, 
        codintfunc, 
        codfuncao, 
        tipoFolha, 
        DataInicial, 
        DataFinal, 
        Colaborador,
        Empresa,
        Filial,
        Evento,
        Funcao) 
      values ( vIp,
             rFicha.CodEvento,
             rFicha.CodigoEmpresa,
             rFicha.CodigoFl,
             rFicha.CodIntFunc,
             rFicha.CodFuncao,
             pTipoFolha,
             pDataInicial,
             pDataFinal,
             rFicha.Colaborador,
             rFicha.Empresa,
             rFicha.Filial,
             rFicha.Evento,
             rFicha.Funcao);
    end if;  
   
    While vData <= pDataFinal loop
      if vData = rFicha.COMPETFICHA then
          case vContColuna      
            when 1 then Grava_Coluna1(vData, rFicha.Referencia, rFicha.ValorFicha, vIp);
            exit;
            when 2 then Grava_Coluna2(vData, rFicha.Referencia, rFicha.ValorFicha, vIp); 
            exit;
            when 3 then Grava_Coluna3(vData, rFicha.Referencia, rFicha.ValorFicha, vIp);
            exit;
            when 4 then Grava_Coluna4(vData, rFicha.Referencia, rFicha.ValorFicha, vIp);
            exit;
            when 5 then Grava_Coluna5(vData, rFicha.Referencia, rFicha.ValorFicha, vIp);
            exit;
            when 6 then Grava_Coluna6(vData, rFicha.Referencia, rFicha.ValorFicha, vIp);
            exit;
            when 7 then Grava_Coluna7(vData, rFicha.Referencia, rFicha.ValorFicha, vIp);
            exit;
            when 8 then Grava_Coluna8(vData, rFicha.Referencia, rFicha.ValorFicha, vIp);
            exit;
            when 9 then Grava_Coluna9(vData, rFicha.Referencia, rFicha.ValorFicha, vIp);
            exit;
            when 10 then Grava_Coluna10(vData, rFicha.Referencia, rFicha.ValorFicha, vIp);
            exit;
            when 11 then Grava_Coluna11(vData, rFicha.Referencia, rFicha.ValorFicha, vIp);
            exit;
            when 12 then Grava_Coluna12(vData, rFicha.Referencia, rFicha.ValorFicha, vIp);
            exit;
          end case;   
      /*else
        case vContColuna      
          when 1 then Grava_Coluna1(vData, 0, 0, vIp);
          when 2 then Grava_Coluna1(vData, 0, 0, vIp);
          when 3 then Grava_Coluna1(vData, 0, 0, vIp);
          when 4 then Grava_Coluna1(vData, 0, 0, vIp);
          when 5 then Grava_Coluna1(vData, 0, 0, vIp);
          when 6 then Grava_Coluna1(vData, 0, 0, vIp);
          when 7 then Grava_Coluna1(vData, 0, 0, vIp);
          when 8 then Grava_Coluna1(vData, 0, 0, vIp);
          when 9 then Grava_Coluna1(vData, 0, 0, vIp);
          when 10 then Grava_Coluna1(vData, 0, 0, vIp);
          when 11 then Grava_Coluna1(vData, 0, 0, vIp);
          when 12 then Grava_Coluna1(vData, 0, 0, vIp);
        end case;  */
      end if;            
    
      vData := Last_day(vData+1);    
      inc(vContColuna);
      if vContColuna > 12 then
        exit;
      end if;
    end loop;
              
  end loop;
end;
/
