Select f.nomefunc, m.descmtdeslig, q.*
From flp_quitacao q, flp_funcionarios f, flp_motivodeslig m
Where q.codintfunc = f.codintfunc
And f.codigoempresa =29
And q.dtcompetquita > '01-oct-2020'
And m.codmtdeslig = q.codmtdeslig
Order By nomefunc Desc;
/*
Select f.nomeFunc, r.Gozoinifer, r.Gozofinfer
From flp_ferias r, Flp_Funcionarios f
Where f.codintfunc = r.codintfunc
And f.codigoempresa = 29 
And r.Gozoinifer > '20-mar-2020'
And r.usuexclferias Is Null;  

Select f.Nomefunc, a.*
 From Flp_Afastados a, flp_funcionarios f
Where a.codintfunc = f.codintfunc
And f.codigoempresa = 29
Order By nomefunc Desc*/