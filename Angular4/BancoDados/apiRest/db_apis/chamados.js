// exemplo https://jsao.io/2018/04/creating-a-rest-api-handling-get-requests/

const database = require('../services/database.js');
const oracledb = require('oracledb');

const baseQuery = `Select IDCHAMADO, IDUSUARIO, IDCATEGORIA, IDTELA, ASSUNTO, IDEMPRESA, DATA         
, NUMERO, STATUS, ORIGEM, PRIORIDADE, NUMADEQFORN, DATAENTADEQ, AVALIACAO, DescricaoAvaliacao
, UsuarioAbertura, Categoria, IDCHAMADOASSOCIADO, AtendenteFoiCortez, DataAvaliacaoDoSolicitante
, SolicitanteAbriuCorretamente, SolicitanteRespDentroDePrazo, SolicitanteFoiCortez, AvaliacaoSolicitante 
, DescricaoAvaliacaoSolic, Dias, PRAZODESENVOLVIMENTO, LembrarDentreDeDias, MotivoLembrete  
, Reavaliar, Reavaliado, DataReavaliacao, IdUsuarioAcompanhamento, Lembrete
, IDEMPRESASOLICITANTE, TrocouCategoria 
  From (` ;
 
const baseQueryInterna = `Select c.IDCHAMADO, c.IDUSUARIO, c.IDCATEGORIA, c.IDTELA, c.ASSUNTO, c.IDEMPRESA, c.DATA         
, c.NUMERO, c.STATUS, c.ORIGEM, c.PRIORIDADE, c.NUMADEQFORN, c.DATAENTADEQ, c.AVALIACAO, c.DescricaoAvaliacao
, ua.Nome UsuarioAbertura, ct.descricao Categoria, c.IDCHAMADOASSOCIADO, c.AtendenteFoiCortez, c.DataAvaliacaoDoSolicitante
, c.SolicitanteAbriuCorretamente, c.SolicitanteRespDentroDePrazo, c.SolicitanteFoiCortez, c.AvaliacaoSolicitante 
, c.DescricaoAvaliacaoSolic, p.qtddiasretornochamado Dias, c.PRAZODESENVOLVIMENTO, c.LembrarDentreDeDias, c.MotivoLembrete  
, c.Reavaliar, c.Reavaliado, c.DataReavaliacao, c.IdUsuarioAcompanhamento
, c.IDEMPRESASOLICITANTE, c.TrocouCategoria
, (Select Data From niff_chm_lembretechamados l Where l.Idchamado = c.IdChamado And trunc(sysdate) = Data) Lembrete 
   From Niff_Chm_Chamado c, Niff_CHM_Usuarios ua, Niff_Chm_Parametros p, Niff_Chm_Categorias CT 
  Where c.idUsuario = ua.IdUsuario 
    and ct.Idcategoria = c.idcategoria 
    `;

async function listarChamados(context) {
    let query = baseQuery;
    const binds = {};

    query += baseQueryInterna;

    binds.idEmpresa = context.empresaUsuario;
    binds.idUsuario = context.idUsuario;

    if (context.status !== 'F' && context.status !== 'C')
      query += ' and c.idchamadoagrupado Is Null ';
  
    if (context.apenasDoUsuario === 'N') {

    
      if (context.ano !== '')  
      {
        binds.ano = context.ano;
        query += `and To_char(c.Data,'yyyy') = :ano`;
      }

      if (context.status === 'A'){
        query += ` and c.Status not in ('F','C')`;
      } else {
        if (context.status === 'F' || context.status === 'S'){
            query += ` and c.Status = 'F'`;

            if (context.status === 'S')
                query += ` and (Nvl(c.Avaliacao,0) = 0 Or nvl(Avaliacaosolicitante,0) = 0) `;
        }
        else {
            query += ` and c.Status = 'C'`;
        }
      }

      if (context.departamento !== 0){
        binds.departamento = context.departamento;
        binds.idColaborador = context.idColaborador;

        query += `  And ua.IdEmpresa = :idEmpresa 
                    And (ua.IdDepartamento = :departamento
                     or ua.Iddepartamento In (Select d.iddepartamento From niff_ads_colabdepartamento d Where d.idcolaborador = :idColaborador )
                     or ua.Iddepartamento In (Select iddepartamento From Niff_Chm_Usuarios Where Idusuario In (Select Idusuario From Niff_Ads_Colabdepartamento Where Iddepartamento = :departamento )))`;
      }
      else{

        query += `  And (ct.idcategoria In (Select idCategoria From Niff_Chm_Categautousuario Where Idusuario = :idUsuario )
                     or c.Idusuario = :idUsuario )`;

        if (context.tipoUsario === 'A' && context.empresaUsuario !== 19 && context.usuarioLogado != 'ELSILVA')
           query += ` and c.IdEmpresa = :idEmpresa `;
      }

      query += ' Order by IdChamado ';
      
    }
    else{
        query += `  And c.Status = 'N' 
                    And c.Idusuario != :idUsuario`;


        if (context.status !== 'F' && context.status !== 'C')
            query += ' and c.idchamadoagrupado Is Null ';
                        
        if (context.ano !== '')  
        {
            binds.ano = context.ano;
            query += `and To_char(c.Data,'yyyy') = :ano`;
        }
    
        if (context.status === 'A'){
            query += ` and c.Status not in ('F','C')`;
        } else {
            if (context.status === 'F' || context.status === 'S'){
                query += ` and c.Status = 'F'`;
    
                if (context.status === 'S')
                    query += ` and (Nvl(c.Avaliacao,0) = 0 Or nvl(Avaliacaosolicitante,0) = 0) `;
            }
            else {
                query += ` and c.Status = 'C'`;
            }
        }      

        if (context.departamento !== 0){
            binds.departamento = context.departamento;
            binds.idColaborador = context.idColaborador;
    
            query += `  And ua.IdEmpresa = :idEmpresa 
                        And (ua.IdDepartamento = :departamento
                            or ua.Iddepartamento In (Select d.iddepartamento From niff_ads_colabdepartamento d Where d.idcolaborador = :idColaborador )
                            or ua.Iddepartamento In (Select iddepartamento From Niff_Chm_Usuarios Where Idusuario In (Select Idusuario From Niff_Ads_Colabdepartamento Where Iddepartamento = :departamento )))`;
        }
        else{
            

            query += `  And (ct.idcategoria In (Select idCategoria From Niff_Chm_Categautousuario Where Idusuario = :idUsuario )
                        or c.Idusuario = :idUsuario )`;

            if (context.tipoUsario === 'A' && context.empresaUsuario !== 19 && context.usuarioLogado != 'ELSILVA')
                query += ` and c.IdEmpresa = :idEmpresa `;
        }

        query += ` Union ALL 

           Select c.IDCHAMADO, c.IDUSUARIO, c.IDCATEGORIA, c.IDTELA, c.ASSUNTO, c.IDEMPRESA, c.DATA         
            , c.NUMERO, c.STATUS, c.ORIGEM, c.PRIORIDADE, c.NUMADEQFORN, c.DATAENTADEQ, c.AVALIACAO, c.DescricaoAvaliacao
            , ua.Nome UsuarioAbertura, ct.descricao Categoria, c.IDCHAMADOASSOCIADO, c.AtendenteFoiCortez, c.DataAvaliacaoDoSolicitante
            , c.SolicitanteAbriuCorretamente, c.SolicitanteRespDentroDePrazo, c.SolicitanteFoiCortez, c.AvaliacaoSolicitante 
            , c.DescricaoAvaliacaoSolic, p.qtddiasretornochamado Dias, c.PRAZODESENVOLVIMENTO, c.LembrarDentreDeDias, c.MotivoLembrete  
            , c.Reavaliar, c.Reavaliado, c.DataReavaliacao, c.IdUsuarioAcompanhamento
            , c.IDEMPRESASOLICITANTE, c.TrocouCategoria
            , (Select Data From niff_chm_lembretechamados l Where l.Idchamado = c.IdChamado And trunc(sysdate) = Data) Lembrete 
                From Niff_Chm_Chamado c, Niff_CHM_Usuarios ua, Niff_Chm_Parametros p, Niff_Chm_Categorias CT, Niff_Chm_Histochamado h
                Where c.idUsuario = ua.IdUsuario 
                and ct.Idcategoria = c.idcategoria 
                And c.Idchamado = h.Idchamado
                And h.Privado = 'N'
                And h.Idusuario = :idUsuario
                `;

        if (context.status !== 'F' && context.status !== 'C')
            query += ' and c.idchamadoagrupado Is Null ';
        
        if (context.ano !== '')  
        {
            binds.ano = context.ano;
            query += `and To_char(c.Data,'yyyy') = :ano`;
        }
    
        if (context.status === 'A'){
            query += ` and c.Status not in ('F','C')`;
        } else {
            if (context.status === 'F' || context.status === 'S'){
                query += ` and c.Status = 'F'`;
    
                if (context.status === 'S')
                    query += ` and (Nvl(c.Avaliacao,0) = 0 Or nvl(Avaliacaosolicitante,0) = 0) `;
            }
            else {
                query += ` and c.Status = 'C'`;
            }
        }      

        if (context.departamento !== 0){
            binds.departamento = context.departamento;
            binds.idColaborador = context.idColaborador;
    
            query += `  And ua.IdEmpresa = :idEmpresa 
                        And (ua.IdDepartamento = :departamento
                            or ua.Iddepartamento In (Select d.iddepartamento From niff_ads_colabdepartamento d Where d.idcolaborador = :idColaborador )
                            or ua.Iddepartamento In (Select iddepartamento From Niff_Chm_Usuarios Where Idusuario In (Select Idusuario From Niff_Ads_Colabdepartamento Where Iddepartamento = :departamento )))`;
        }
        else{
            

            query += `  And (ct.idcategoria In (Select idCategoria From Niff_Chm_Categautousuario Where Idusuario = :idUsuario )
                        or c.Idusuario = :idUsuario )`;

            if (context.tipoUsario === 'A' && context.empresaUsuario !== 19 && context.usuarioLogado != 'ELSILVA')
                query += ` and c.IdEmpresa = :idEmpresa `;
        }            
        
    	query += ` group by c.IDCHAMADO, c.IDUSUARIO, c.IDCATEGORIA, c.IDTELA, c.ASSUNTO, c.IDEMPRESA, c.DATA         
        , c.NUMERO, c.STATUS, c.ORIGEM, c.PRIORIDADE, c.NUMADEQFORN, c.DATAENTADEQ, c.AVALIACAO, c.DescricaoAvaliacao
        , ua.Nome UsuarioAbertura, ct.descricao Categoria, c.IDCHAMADOASSOCIADO, c.AtendenteFoiCortez, c.DataAvaliacaoDoSolicitante
        , c.SolicitanteAbriuCorretamente, c.SolicitanteRespDentroDePrazo, c.SolicitanteFoiCortez, c.AvaliacaoSolicitante 
        , c.DescricaoAvaliacaoSolic, p.qtddiasretornochamado Dias, c.PRAZODESENVOLVIMENTO, c.LembrarDentreDeDias, c.MotivoLembrete  
        , c.Reavaliar, c.Reavaliado, c.DataReavaliacao, c.IdUsuarioAcompanhamento
        , c.IDEMPRESASOLICITANTE, c.TrocouCategoria 
          Order by IdChamado `;
                    
    }

    query += ` ) Select IDCHAMADO, IDUSUARIO, IDCATEGORIA, IDTELA, ASSUNTO, IDEMPRESA, DATA         
    , NUMERO, STATUS, ORIGEM, PRIORIDADE, NUMADEQFORN, DATAENTADEQ, AVALIACAO, DescricaoAvaliacao
    , UsuarioAbertura, Categoria, IDCHAMADOASSOCIADO, AtendenteFoiCortez, DataAvaliacaoDoSolicitante
    , SolicitanteAbriuCorretamente, SolicitanteRespDentroDePrazo, SolicitanteFoiCortez, AvaliacaoSolicitante 
    , DescricaoAvaliacaoSolic, Dias, PRAZODESENVOLVIMENTO, LembrarDentreDeDias, MotivoLembrete  
    , Reavaliar, Reavaliado, DataReavaliacao, IdUsuarioAcompanhamento, Lembrete
    , IDEMPRESASOLICITANTE, TrocouCategoria `;

    const result = await database.simpleExecute(query, binds);
   
    return result.rows;
  }
   
  module.exports.find = find;
  