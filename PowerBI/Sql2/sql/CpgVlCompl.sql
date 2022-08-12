Select l.*, d.* From cpgdocto d, cpgvlrcompldoctos l
Where d.nrodoctocpg = '0000000699'
And codigoempresa = 2
And d.coddoctocpg = 816714
And l.coddoctocpg = d.coddoctocpg ;

Select l.*, d.* From cpgdocto d, cpgvlrcompldoctos l
Where
 d.nrodoctocpg = '0000085872'
And 
codigoempresa = 2
--And d.coddoctocpg = 816714
And l.coddoctocpg = d.coddoctocpg



