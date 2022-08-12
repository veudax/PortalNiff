
Select d.*, Rowid From cpgdocto d
Where d.coddoctocpg in (
Select f.coddoctocpg From flp_integracaocpg_func f
Where 
trunc(f.dtintegra) = '12-jul-2017' 
And usuario = 'VDALMEIDA');
