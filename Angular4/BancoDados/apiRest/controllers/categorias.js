// https://jsao.io/2018/04/creating-a-rest-api-handling-get-requests/

const categorias = require('../db_apis/categorias.js');

async function get(req, res, next) {
  try {
    const context = {};
 
    context.id = parseInt(req.params.id, 10);
 
    const rows = await categorias.find(context);
 
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
 
module.exports.get = get;

async function getProximo(req, res, next) {
  try {
    const rows = await categorias.proximo();
    res.status(200).json(rows);
  } catch (err) {
    next(err);
  }
}
 
module.exports.getProximo = getProximo;

function getCategoriaFromRec(req) {
    const categoria = {
      idCategoria: req.body.idCategoria,
      descricao: req.body.Descricao,
      ativo: req.body.Ativo,
      possuiModulos: req.body.PossuiModulos
    };
    return categoria;
  }
   
  async function post(req, res, next) {
    try {
      let categoria = getCategoriaFromRec(req);      
   
      categoria = await categorias.create(categoria);
   
      res.status(201).json(categoria);
    } catch (err) {
      next(err);
    }
  }
   
  module.exports.post = post;

  async function put(req, res, next) {
    try {
      let categoria = getCategoriaFromRec(req);
   
      categoria = await categorias.update(categoria);
   
      if (categoria !== null) {
        res.status(200).json(categoria);
      } else {
        res.status(404).end();
      }
    } catch (err) {
      next(err);
    }
  }
   
  module.exports.put = put;

  async function del(req, res, next) {
    try {
      let id = req.params.id;
      console.log(id);
      const success = await categorias.delete(id);
   
      if (success) {
        res.status(204).end();
      } else {
        res.status(404).end();
      }
    } catch (err) {
      next(err);
    }
  }
   
  module.exports.delete = del;
  