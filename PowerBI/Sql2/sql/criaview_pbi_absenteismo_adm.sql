create or replace view pbi_absenteismo_adm as
select '001/001 - EOVG DUTRA' empresa, a.qtd_ocorr, a.dthist, situacaofunc
  From (select count(fu.nomefunc) qtd_ocorr, t.dthist, fu.situacaofunc
          from flp_historico t, frq_ocorrencia f, vw_funcionarios fu
         where  f.codocorr = t.codocorr
           and fu.codigoempresa = 1
           and fu.codigofl = 1
           And ((t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr in(505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527,601,602))
            Or (t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
           And f.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,902,903)))            
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20)
         group by t.dthist, fu.situacaofunc) a
UNION ALL
----------------------------------------------------------------------------
select '001/002 - EOVG LAVRAS' empresa,  a.qtd_ocorr, a.dthist, situacaofunc
  From (select count(fu.nomefunc) qtd_ocorr, t.dthist, fu.situacaofunc
          from flp_historico t, frq_ocorrencia f, vw_funcionarios fu
         where f.codocorr = t.codocorr
           and fu.codigoempresa = 1
           and fu.codigofl = 2
           And ((t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr in(505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527,601,602))
            Or (t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
           And f.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,902,903)))            
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20)
         group by t.dthist, fu.situacaofunc) a
UNION ALL
----------------------------------------------------------------------------
select '002/001 - ABC TRANSPORTES' empresa,  a.qtd_ocorr, a.dthist, situacaofunc
  From (select count(fu.nomefunc) qtd_ocorr, t.dthist, fu.situacaofunc
          from flp_historico t, frq_ocorrencia f, vw_funcionarios fu
         Where f.codocorr = t.codocorr
           and fu.codigoempresa = 2
           and fu.codigofl = 1
           And ((t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr in(505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527,601,602))
            Or (t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
           And f.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,902,903)))            
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20)
         group by t.dthist, fu.situacaofunc)a
UNION ALL
----------------------------------------------------------------------------
select '003/001 - RAPIDO D OESTE' empresa,  a.qtd_ocorr, a.dthist, situacaofunc
  From (select count(fu.nomefunc) qtd_ocorr, t.dthist, fu.situacaofunc
          from flp_historico t, frq_ocorrencia f, vw_funcionarios fu
         where t.dthist BETWEEN (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr = t.codocorr
           and fu.codigoempresa = 3
           and fu.codigofl = 1
           And ((t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr in(505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527,601,602))
            Or (t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
           And f.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,902,903)))            
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20)
         group by t.dthist, fu.situacaofunc) a
UNION ALL
----------------------------------------------------------------------------
select '004/001 - CISNE BRANCO' empresa,   a.qtd_ocorr, a.dthist, situacaofunc
  From (select count(fu.nomefunc) qtd_ocorr, t.dthist, fu.situacaofunc
          from flp_historico t, frq_ocorrencia f, vw_funcionarios fu
         where t.dthist BETWEEN (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr = t.codocorr
           and fu.codigoempresa = 4
           and fu.codigofl = 1
           And ((t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr in(505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527,601,602))
            Or (t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
           And f.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,902,903)))            
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20)
         group by t.dthist, fu.situacaofunc)a
UNION ALL
select '005/001 - NIFF' empresa,   a.qtd_ocorr, a.dthist, situacaofunc
  From (select count(fu.nomefunc) qtd_ocorr, t.dthist, fu.situacaofunc
          from flp_historico t, frq_ocorrencia f, vw_funcionarios fu
         where t.dthist BETWEEN (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr = t.codocorr
           and fu.codigoempresa = 5
           and fu.codigofl = 1
           And ((t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr in(505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527,601,602))
            Or (t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
           And f.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,902,903)))            
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20)
         group by t.dthist, fu.situacaofunc)a
UNION ALL
----------------------------------------------------------------------------
select '006/001 - VIAÇÃO ARUJÁ' empresa,  a.qtd_ocorr, a.dthist, situacaofunc
  From (select count(fu.nomefunc) qtd_ocorr, t.dthist, fu.situacaofunc
          from flp_historico t, frq_ocorrencia f, vw_funcionarios fu
         where t.dthist BETWEEN (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr = t.codocorr
           and fu.codigoempresa = 6
           and fu.codigofl = 1
           And ((t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr in(505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527,601,602))
            Or (t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
           And f.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,902,903)))            
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20)
         group by t.dthist, fu.situacaofunc)a
UNION ALL
----------------------------------------------------------------------------
select '009/001 - CAMPIBUS LTDA' empresa,  a.qtd_ocorr, a.dthist, situacaofunc
  From (select count(fu.nomefunc) qtd_ocorr, t.dthist, fu.situacaofunc
          from flp_historico t, frq_ocorrencia f, vw_funcionarios fu
         where t.dthist BETWEEN (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr = t.codocorr
           and fu.codigoempresa = 9
           and fu.codigofl = 2
           And ((t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr in(505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527,601,602))
            Or (t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
           And f.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,902,903)))            
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20)
         group by t.dthist, fu.situacaofunc)a
UNION ALL
----------------------------------------------------------------------------
select '013/001 - RIBE TRANSPORTES' empresa, a.qtd_ocorr, a.dthist, situacaofunc
  From (select count(fu.nomefunc) qtd_ocorr, t.dthist, fu.situacaofunc
          from flp_historico t, frq_ocorrencia f, vw_funcionarios fu
         where t.dthist BETWEEN (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr = t.codocorr
           and fu.codigoempresa = 13
           and fu.codigofl = 1
           And ((t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr in(505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527,601,602))
            Or (t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
           And f.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,902,903)))            
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20)
         group by t.dthist, fu.situacaofunc)a
UNION ALL
----------------------------------------------------------------------------
select '026/001 - VUG DUTRA' empresa,  a.qtd_ocorr, a.dthist, situacaofunc
  From (select count(fu.nomefunc) qtd_ocorr, t.dthist, fu.situacaofunc
          from flp_historico t, frq_ocorrencia f, vw_funcionarios fu
         where t.dthist BETWEEN (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr = t.codocorr
           and fu.codigoempresa = 26
           and fu.codigofl = 1
           And ((t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr in(505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527,601,602))
            Or (t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
           And f.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,902,903)))            
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20)
         group by t.dthist, fu.situacaofunc)a
UNION ALL
----------------------------------------------------------------------------
select '026/003 - VUG BEBEDOURO' empresa, a.qtd_ocorr, a.dthist, situacaofunc
  From (select count(fu.nomefunc) qtd_ocorr, t.dthist, fu.situacaofunc
          from flp_historico t, frq_ocorrencia f, vw_funcionarios fu
         where t.dthist BETWEEN (ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr = t.codocorr
           and fu.codigoempresa = 26
           and fu.codigofl = 3
           And ((t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and sysdate
           and f.codocorr in(505,504,506,507,521,526,534,535,541,542,579,582,579,560,584,580,581,583,592,593,527,601,602))
            Or (t.dthist BETWEEN ADD_MONTHS(Trunc(SYSDATE,'rr'), -24) and ADD_MONTHS(Trunc(SYSDATE,'rr'), -12)-1
           And f.codocorr In (2,3,10,28,117,120,173,209,222,230,233,322,344,374,902,903)))            
           and t.codintfunc = fu.codintfunc
           and fu.CODAREA in (20)
         group by t.dthist, fu.situacaofunc)a
