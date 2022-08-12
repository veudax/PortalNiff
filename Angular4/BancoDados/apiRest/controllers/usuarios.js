// https://jsao.io/2018/04/creating-a-rest-api-handling-get-requests/

const usuarios = require('../db_apis/usuarios.js');


async function getId(req, res, next) {
  try {
    const context = {};
    
    context.id = parseInt(req.params.id, 10);
    const rows = await usuarios.findIdUsuario(context);
 
    if (req.params.id) {
      if (rows.length === 1) {
        res.status(200).json(rows[0]);
      } else {
        res.status(404).end();
      }
    } else {
      res.status(200).json(rows);
    }
  } catch (err) {
    next(err);
  }
}
 
module.exports.getId = getId;

async function getLogin(req, res, next) {
  try {
    const context = {};
    
    context.usuarioAcesso = req.params.user;
    const rows = await usuarios.findUsuarioAcesso(context);
 
    console.log(rows);

    res.status(200).json(rows);
    
  } catch (err) {
    next(err);
  }
}
 
module.exports.getLogin = getLogin;

async function getTodosAtivos(req, res, next) {
  try {
    const context = {};
    
    const rows = await usuarios.findTodosAtivos();
 
    res.status(200).json(rows);

  } catch (err) {
    next(err);
  }
}
 
module.exports.getTodosAtivos = getTodosAtivos;

async function getTodosAtivosEmpresa(req, res, next) {
  try {
    const context = {};

    context.idEmpresa = req.params.id;
    const rows = await usuarios.findTodosAtivosDaEmpresa(context);    
 
    res.status(200).json(rows);
    
  } catch (err) {
    next(err);
  }
}
 
module.exports.getTodosAtivosEmpresa = getTodosAtivosEmpresa;

async function getIdFuncionario(req, res, next) {
  try {
    const context = {};
    
    context.codIntFunc = parseInt(req.params.id, 10);
    rows = await usuarios.findCodigoInternoFuncionarioGlobus(context);        
 
    if (req.params.id) {
      if (rows.length === 1) {
        res.status(200).json(rows[0]);
      } else {
        res.status(404).end();
      }
    } else {
      res.status(200).json(rows);
    }
  } catch (err) {
    next(err);
  }
}
 
module.exports.getIdFuncionario = getIdFuncionario;

