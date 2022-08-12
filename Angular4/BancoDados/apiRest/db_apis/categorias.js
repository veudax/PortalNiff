// exemplo https://jsao.io/2018/04/creating-a-rest-api-handling-get-requests/

const database = require('../services/database.js');
const oracledb = require('oracledb');

const baseQuery = `select idCategoria, descricao, ativo, possuimodulos, 'S' existe from niff_chm_categorias Order by idCategoria`;
 
async function find(context) {
  let query = baseQuery;
  const binds = {};
 
  if (context.id) {
    binds.idCategoria = context.id;
 
    query += `\n where IDCATEGORIA = :idCategoria`;
  }
  
  const result = await database.simpleExecute(query, binds);
 
  return result.rows;
}
 
module.exports.find = find;


async function proximo() {
  let query = `Select Nvl(max(idCategoria),0)+1 idcategoria, ' ' descricao, 'S' ativo, 'S' possuimodulos, 'N' existe from niff_chm_categorias`;
  const binds = {};
 
  const result = await database.simpleExecute(query, binds);

  return result.rows;
}
 
module.exports.proximo = proximo;


const createSql =
 `insert into niff_chm_categorias ( idCategoria, descricao, ativo, possuimodulos
  ) values (
    :idCategoria,
    :descricao,
    :ativo,
    :possuimodulos
  ) `;
//  returning idCategoria into :idCategoria`;
 
async function create(emp) {
  const categoria = Object.assign({}, emp);
 
  console.log(categoria);  

  const result = await database.simpleExecute(createSql, categoria);
 
  //categoria.idCategoria = result.outBinds.idCategoria[0];
 
  return categoria;
}
 
module.exports.create = create;

const updateSql =
 `update niff_chm_categorias
  set descricao = :descricao,
    ativo = :ativo,
    possuimodulos = :possuimodulos
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
 `delete from niff_chm_categorias where idCategoria = :idCategoria`;
 
async function del(id) {
  const binds = {
    idCategoria: id
    }
  
  await database.simpleExecute(deleteSql, binds);
 
}
 
module.exports.delete = del;