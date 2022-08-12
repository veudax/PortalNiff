// exemplo https://jsao.io/2018/04/creating-a-rest-api-handling-get-requests/

const database = require('../services/database.js');
const oracledb = require('oracledb');

const baseQuery = `Select u.idUsuario 
, u.tipo as tipoUsuario
, u.nome
, u.ativo
, u.administrador
, u.ipMaquina
, u.nomeMaquina
, u.telefone
, u.ramal
, u.email
, u.usuarioAcesso
, u.senha
, u.cargo
, u.dtnascimento as dataNascimento
, u.foto
, u.acessaAgenda
, u.acessaChat
, u.permiteExcluirChat
, u.acessaBI
, u.permiteIncExcFotosFesta as permiteIncluirExcluirFoto
, u.emailAcessoPowerBI
, u.idEmpresa
, u.idDepartamento
, u.acessaSAC
, u.TipoUsuarioSAC
, u.CodFunc
, f.nomefunc
, f.chapafunc
, f.codfunc registro
, u.CPF
, u.EmailDepto
, u.AcessaDescontoBeneficio
, u.AcessaJuridico
, u.IdCargo
, u.AcessaCadastroJuridico
, u.PermiteAprovarComunicado
, u.PermiteReprovarComunicado
, u.PermiteCancelarComunicado
, u.PermiteAlterarComunicado
, u.AcessaDashBoardChamados
, u.AcessaAvaliacaoDesempenho
, u.AcessoDeRH
, u.AcessoDeGestor
, u.AcessoDeColaborador
, u.AcessoDeControladoria
, u.NaoNotificaCorridas
, u.AniversariantesApenasDaEmpresa
, u.VisualizaRadarCompleto
, u.VisualizaBancoHorasDoDepto
, u.ParticipaBolaoCopa
, u.AdministraBolaoCopa
, u.AdministraBiblioteca
, u.AdministraCorridas
, u.AcessaBSC
, u.AcessaMetasFinanceiras
, u.AcessaMetasOperacionais
, u.PermiteBuscarResultado
, u.PermiteAlterarBSC
, u.SoChamadosDesseUsuario
, u.AcessaRecebedoria
, u.PodeExportarSigomExcel
, u.AcessaOperacional
, u.AcessaCadastroOperacional
, u.AcessaDemonstrativo
, u.AcessaIQO
, u.PodeFinalizarChamado
, u.AssinaturaChamado
, u.DataAdmissao
, u.AlteraBSCIndicadoresManuais
, u.AcessaFinanceio
, u.AcessaCadastroFin

, u.AcessaContabilidade
, u.AcessaEscrituracaoFiscal
, u.AcessaRamais

, u.AcessaCadastroBenRateio
, u.AcessaBeneficioRateio
, u.AcessaCalculoRateioBen
, u.AcessaRateio
, u.AcessaDepartamentoPessoal
, u.ProcessaArquivei

, u.Desenvolvedor
, u.Coordenador
, u.Gerente

, u.AcessaDRE
, u.AcessaCadastroMetas
, u.PermiteReabrirDRE
, u.ApenasConsultaDre
, u.ApenasPrevistoDRE

, u.AcessaLalur
, u.AcessaCadastroLalur
, u.AcessaCalculoLalur

, u.AcessaDemonstrativoFluxoCaixa
, u.AcessaResumoFluxoCaixa

, u.SempreMostrarListaDeChamados
, u.AGENDALIBERACARROS
, u.ACESSAENDIVIDAMENTO
, u.AcessaParcelamento
, u.ReprocessaParcelamento
, u.AcessaCigam
, u.AcessaCTBNotasFicais

, d.descricao Departamento, e.nomeabreviado 

 From NIFF_CHM_Usuarios u, flp_funcionarios f, Niff_Chm_Departamento d, niff_chm_empresas e 
Where f.codintfunc(+) = u.codfunc 
  And d.iddepartamento(+) = u.Iddepartamento 
  And e.idempresa(+) = u.idempresa`;
 

async function findTodosAtivos() {
    let query = baseQuery;
    const binds = {};
   
    query += `\n and u.ativo = 'S'`;
        
    const result = await database.simpleExecute(query, binds);
   
    return result.rows;
  }
   
module.exports.findTodosAtivos = findTodosAtivos;

async function findTodosAtivosDaEmpresa(context) {
    let query = baseQuery;

    const binds = {};

    if (context.idEmpresa) {
        binds.idEmpresa = context.idEmpresa;
     
        query += `\n and u.idEmpresa = :idEmpresa`;      
    }

    query += `\n and u.ativo  = 'S'`;

    const result = await database.simpleExecute(query, binds);
    return result.rows;
  }
   
module.exports.findTodosAtivosDaEmpresa = findTodosAtivosDaEmpresa;

async function findIdUsuario(context) {
  let query = baseQuery;
  const binds = {};
 
  if (context.id) {
    binds.idUsuario = context.id;
 
    query += `\n and idUsuario = :idUsuario`;
  }
  
  const result = await database.simpleExecute(query, binds);
 
  return result.rows;
}
 
module.exports.findIdUsuario = findIdUsuario;

async function findUsuarioAcesso(context) {
    let query = baseQuery;
    const binds = {};
   
    if (context.usuarioAcesso) {
      binds.usuarioAcesso = context.usuarioAcesso;
   
      query += `\n and UsuarioAcesso = :usuarioAcesso`;
    }
    
    const result = await database.simpleExecute(query, binds);
   
    return result.rows;
  }
   
module.exports.findUsuarioAcesso = findUsuarioAcesso;
  
async function findCodigoInternoFuncionarioGlobus(context) {
    let query = baseQuery;
    const binds = {};
   
    if (context.codIntFunc) {
      binds.codIntFunc = context.codIntFunc;
   
      query += `\n and Codintfunc = :codIntFunc`;
    }
    
    const result = await database.simpleExecute(query, binds);
   
    return result.rows;
  }
   
module.exports.findCodigoInternoFuncionarioGlobus = findCodigoInternoFuncionarioGlobus;


// mudar daqui pa baixo
const createSql =
 `insert into niff_chm_categorias ( idCategoria, descricao, ativo, possuimodulos
  ) values (
    (select Max(nvl(IdCategoria,0))+1 from niff_chm_categorias),
    :descricao,
    :ativo,
    :possuimodulo
  ) returning idCategoria
  into :idCategoria`;
 
async function create(emp) {
  const categoria = Object.assign({}, emp);
 
  categoria.idCategoria = {
    dir: oracledb.BIND_OUT,
    type: oracledb.NUMBER
  }
 
  const result = await database.simpleExecute(createSql, categoria);
 
  categoria.idCategoria = result.outBinds.idCategoria[0];
 
  return categoria;
}
 
module.exports.create = create;

const updateSql =
 `update niff_chm_categorias
  set descricao = :descricao,
    ativo = :ativo,
    possuimodulo = :possuiModulo
  where idCategoria = :idCategoria`;
 
async function update(emp) {
  const categoria = Object.assign({}, emp);
  const result = await database.simpleExecute(updateSql, categoria);
 
  if (result.rowsAffected && result.rowsAffected === 1) {
    return categoria;
  } else {
    return null;
  }
}
 
module.exports.update = update;

const deleteSql =
 `begin
 
    delete from niff_chm_categorias
    where idCategoria = :idCategoria;
 
    :rowcount := sql%rowcount;
 
  end;`
 
async function del(id) {
  const binds = {
    idCategoria: id,
    rowcount: {
      dir: oracledb.BIND_OUT,
      type: oracledb.NUMBER
    }
  }
  const result = await database.simpleExecute(deleteSql, binds);
 
  return result.outBinds.rowcount === 1;
}
 
module.exports.delete = del;