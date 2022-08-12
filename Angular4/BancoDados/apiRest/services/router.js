const express = require('express');
const router = new express.Router();
const categorias = require('../controllers/categorias.js');
const usuarios = require('../controllers/usuarios.js');

/*
router.route('/categorias/:id?')
  .get(categorias.get)
  .post(categorias.post)
  .put(categorias.put)
  .delete(categorias.delete);*/

router.route('/usuarios/codigoFuncionario/:id?')
  .get(usuarios.getIdFuncionario);

router.route('/usuarios/ativosEmpresa/:id?')
  .get(usuarios.getTodosAtivosEmpresa);

router.route('/usuarios/usuarioAcesso/:user')
  .get(usuarios.getLogin);
 
router.route('/usuarios/todosAtivos')
  .get(usuarios.getTodosAtivos);

router.route('/usuarios/:id?')
  .get(usuarios.getId);

  
router.route('/categorias/proximo')
.get(categorias.getProximo) ;

router.route('/categorias/:id?')
  .get(categorias.get)
  .post(categorias.post)
  .put(categorias.put)
  .delete(categorias.delete);

module.exports = router;