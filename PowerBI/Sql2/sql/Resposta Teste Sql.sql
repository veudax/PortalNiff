a,b,c,d,e,f,g,h,i, j
--a)	Monte uma consulta para trazer os funcionários que estão afastados sem data de retorno informada. (Deve trazer do funcionário o Código e nome, a Descrição do cargo e a Data do afastamento e a descrição do motivo)

Select m.descricao, f.codigo, f.Nome, c.descricao cargo, a.dataafastado
  From T_Motivo M,
       T_Funcionario F,
       T_Afastamento A,
       T_Cargo C
 Where c.codigo = f.codcargo
   And m.codigo = a.codmotivo
   And f.codigo = a.codfunc
   And a.dataretorno Is Null;
   
-- b)	Monte uma consulta para trazer os funcionários que tiveram promoção no ano de 2016 (Deve trazer do funcionário o Código e nome, a descrição do cargo, a descrição do departamento)
Select F.Codigo, f.Nome, c.Descricao Cargo, d.descricao Deparmento
  From T_Funcionario F,
       T_Cargo C,
       T_Departamento D,
       T_Promocao P
 Where c.codigo = f.codcargo
   And d.codigo = f.coddepto       
   And p.codfunc = f.codigo
   And p.datapromocao Between '01-jan-2016' And '31-dec-2016'      
   -- or  And To_Char(p.datapromocao,'yyyy') = '2016'
   ;
   
--c) Monte uma consulta que traga os funcionários que devem ter uma promoção até o mês atual. (Deve trazer o valor anterior e o reajustado e a função anterior e a nova se houver, código e nome do funcionário)
--2,0
Select F.Codigo,
       f.Nome, 
       c.Descricao CargoAnterior, 
       f.dataadmissao + c.promoveacada, f.salario SalarioAnterior, 
       f.Salario + DECODE(C.TIPOAUMENTO, '%', ((C.Aumentosalariode/100) * f.Salario), C.Aumentosalariode) SalarioAtual,
       ca.Descricao CargoAtual 
  From T_Funcionario F,
       T_Promocao P, 
       T_Cargo C,
       t_Cargo Ca
 Where F.codcargo = c.codigo  
   And F.Codigo = p.codfunc(+)
   And c.codproxcargo = ca.codigo(+)  
   And f.datadesligamento Is Null
   And f.dataadmissao + c.promoveacada < Last_day(trunc(Sysdate)) 
   And (p.datapromocao < '01-jan-2018' Or p.datapromocao Is Null); 

--d)	Monte um comando para inserir um afastamento para o funcionário 15982 com o motivo Atestado.
Insert Into T_Afastamento (codfunc, dataafastado, codmotivo, Dataretorno)
Values (15982, '11-jan-2018', 84);


--e)	Monte um comando para alterar o TipoAumento para Valor e AumentoSalarioDe 260,45 do cargo Aux. Serv. Gerais
Update T_Cargo   
   Set TipoAumento = 'V'
     , AumentoSalarioDe = 260.45
 Where Codigo = 461;
 
 
--f)	Monte um comando para excluir o Funcionário ISIS BESENSKI CANUTO MENEZES.
Delete T_Promocao
 Where CodFunc In (Select Codigo From T_Funcionario 
                    Where Nome = 'ISIS BESENSKI CANUTO MENEZES');

Delete T_Afastamento 
 Where CodFunc In (Select Codigo From T_Funcionario 
                    Where Nome = 'ISIS BESENSKI CANUTO MENEZES');

Delete T_Funcionario f
 Where f.Nome = 'ISIS BESENSKI CANUTO MENEZES';
 
 
--g)	Corrija a consulta abaixo e descreva o(s) possível(is) erro(s) 
Select F.CODIGO, F.Nome, P.DATAPROMCAO -- Campo escrito errado
  From T_Funcionario F, -- falta a virgula
       T_Promocao P
 Where p.codfunc = f.codigo        ;
 
--h)	Quando a mensagem "ORA-02291: integrity constraint (GLOBUS.FK_MOTIVO_AFASTAMENTO) violated - parent key not found" é exibida qual o procedimento a ser executado dentro do Insert ou Update?  
-- Deve se informar o codigo do Motivo correto ao inserir ou alterar o Afastamento. 

--i)	Quando a mensagem "ORA-02292: integrity constraint violated - child record found" é exibida qual o procedimento a ser executado para o Delete?
-- Deve ser excluido os registros das tabelas filhas para que a tabela principal tenha seu registro excluído.

--j)	Quando estamos efetuando um Insert e a mensagem "ORA-00947: not enough values" é exibida qual o procedimento a ser executado?
-- Verificar qual valor nao foi informado no values;

Select Length(nome) From T_Funcionario
Select Trim(Nome) From T_Funcionario
Select LPAD(CodDepto, 6, '0') From T_Funcionario;

Select Max(DataPromocao) From T_Promocao P

Select (Case When salario < 3000 Then Salario * 0.25
       Else Salario * 0.15 End) salario
  From T_Funcionario