const http = require('http');
const express = require('express');
const webServerConfig = require('../config/web-server.js');
const router = require('./router.js');
const morgan = require('morgan');
const database = require('./database.js');

var load = require('express-load');
var cors = require('cors');
var bodyParser = require('body-parser');

let httpServer;

function initialize() {
  
  return new Promise((resolve, reject) => {
    const app = express();
    httpServer = http.createServer(app);
    
    app.use(cors());

    app.use(function(require, response, next ){
      response.header("Access-Control-Allow-Origin","*");
      response.header("Access-Control-Allow-Headers","Origin, X-Requested-Width, Content-Type, Accept");
      next();
    });

    app.use(bodyParser.json());
    app.use(bodyParser.urlencoded({extended : true}));

    app.use(morgan('combined'));
    // Mount the router at /api so all its routes start with /api
    app.use('/api', router);

    app.get('/', async (req, res) => {
        const result = await database.simpleExecute('select user, systimestamp from dual');
        const user = result.rows[0].USER;
        const date = result.rows[0].SYSTIMESTAMP;
   
        res.end(`DB user: ${user}\nDate: ${date}`);
      });

    httpServer.listen(webServerConfig.port)
      .on('listening', () => {
        console.log(`Web server listening on localhost:${webServerConfig.port}`);

        resolve();
      })
      .on('error', err => {
        reject(err);
      });
  });
}

module.exports.initialize = initialize;

function close() {
    return new Promise((resolve, reject) => {
      httpServer.close((err) => {
        if (err) {
          reject(err);
          return;
        }
  
        resolve();
      });
    });
  }
  
  module.exports.close = close;

