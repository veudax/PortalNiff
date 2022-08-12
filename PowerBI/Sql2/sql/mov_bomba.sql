Select Sum(m.qtdesaidalitrosmovbomba) q, m.codigoBomba, last_day( m.datamovbomba )
from aba_cadbomba t, Aba_Movbomba m
Where m.codigobomba = t.codigobomba
And m.datamovbomba Between '01-jan-2018' And '31-jul-2018'
Group By m.codigobomba, last_day( m.datamovbomba )