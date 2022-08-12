--Select Sum(decode (idcoluna, 13, -1, 18, -1, 1) *  a.realizadobco) vaor 
Select idcoluna, a.realizadobco, decode (idcoluna, 13, -1, 18, -1, 1) *  a.realizadobco
From niff_fin_coldemonstrativo a
Where idcoluna In (13,14,17,18)
And  a.iddemonstrativo In (10,13,3)
And Data = '04-jul-2019'