create or replace view pbi_acessobi as
select Trim(t.empfil) empFil, e.email
    from pbi_acessosporempresa t, Pbi_Emails e
Where e.id_email  = t.id_email

