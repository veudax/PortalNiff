using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class SuprimentosDAO
    {
        IDataReader dadosReader;

        #region Metas
        public List<Suprimentos.Metas> Listar(int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Suprimentos.Metas> _lista = new List<Suprimentos.Metas>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select m.id, m.idempresa, m.codintfunc, m.referencia, m.meta, m.idusuarioinc, m.idusuarioalt, m.datainc, m.dataalt");
                query.Append("     , i.Nome UsuarioInclusao, a.Nome UsuarioAlteracao");
                query.Append("  from Niff_Sup_MetasAprovadores m, Niff_chm_usuarios I, Niff_chm_usuarios a");
                query.Append(" Where m.idEmpresa = " + idEmpresa);
                query.Append("   and m.idusuarioinc = i.IdUsuario");
                query.Append("   and m.idusuarioalt = a.IdUsuario(+)");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Suprimentos.Metas _tipo = new Suprimentos.Metas();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dadosReader["id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dadosReader["IdEmpresa"].ToString());
                        _tipo.Referencia = Convert.ToInt32(dadosReader["referencia"].ToString());
                        _tipo.IdUsuarioInclusao = Convert.ToInt32(dadosReader["idusuarioinc"].ToString());
                        _tipo.Ano = Convert.ToInt32(_tipo.Referencia.ToString().Substring(0, 4));

                        try
                        {
                            _tipo.IdUsuarioAlteracao = Convert.ToInt32(dadosReader["idusuarioalt"].ToString());
                        }
                        catch { }

                        _tipo.ReferenciaFormatada = _tipo.Referencia.ToString().Substring(4, 2) + "/" + _tipo.Referencia.ToString().Substring(0, 4);
                        //_tipo.CodIntFunc = Convert.ToDecimal(dadosReader["CodIntFunc"].ToString());
                        _tipo.ValorMeta = Convert.ToDecimal(dadosReader["meta"].ToString());

                        _tipo.UsuarioInclusao = dadosReader["UsuarioInclusao"].ToString();
                        _tipo.UsuarioAlteracao = dadosReader["UsuarioAlteracao"].ToString();

                        _tipo.DataInclusao = Convert.ToDateTime(dadosReader["DataInc"].ToString());
                        try
                        {
                            _tipo.DataAlteracao = Convert.ToDateTime(dadosReader["DataAlt"].ToString());
                        }
                        catch { }
                        _lista.Add(_tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public List<Suprimentos.Metas> Listar(decimal CodIntFunc)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Suprimentos.Metas> _lista = new List<Suprimentos.Metas>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select m.id, m.idempresa, m.codintfunc, m.referencia, m.meta, m.idusuarioinc, m.idusuarioalt, m.datainc, m.dataalt");
                query.Append("     , f.NomeFunc, i.Nome UsuarioInclusao, a.Nome UsuarioAlteracao");
                query.Append("  from Niff_Sup_MetasAprovadores m, Flp_Funcionarios F, Niff_chm_usuarios I, Niff_chm_usuarios a");
                query.Append(" Where f.codintfunc = " + CodIntFunc);
                query.Append("   and m.CodIntFunc = f.CodIntFunc");
                query.Append("   and m.idusuarioinc = i.IdUsuario");
                query.Append("   and m.idusuarioalt = a.IdUsuario(+)");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Suprimentos.Metas _tipo = new Suprimentos.Metas();

                        _tipo.Existe = true;
                        _tipo.Id = Convert.ToInt32(dadosReader["id"].ToString());
                        _tipo.IdEmpresa = Convert.ToInt32(dadosReader["IdEmpresa"].ToString());
                        _tipo.Referencia = Convert.ToInt32(dadosReader["referencia"].ToString());
                        _tipo.IdUsuarioInclusao = Convert.ToInt32(dadosReader["idusuarioinc"].ToString());
                        _tipo.Ano = Convert.ToInt32(_tipo.Referencia.ToString().Substring(0, 4));

                        try
                        {
                            _tipo.IdUsuarioAlteracao = Convert.ToInt32(dadosReader["idusuarioalt"].ToString());
                        }
                        catch { }

                        _tipo.ReferenciaFormatada = _tipo.Referencia.ToString().Substring(4, 2) + "/" + _tipo.Referencia.ToString().Substring(0, 4);
                        //_tipo.CodIntFunc = Convert.ToDecimal(dadosReader["CodIntFunc"].ToString());
                        _tipo.ValorMeta = Convert.ToDecimal(dadosReader["meta"].ToString());

                        _tipo.Funcionario = dadosReader["NomeFunc"].ToString();
                        _tipo.UsuarioInclusao = dadosReader["UsuarioInclusao"].ToString();
                        _tipo.UsuarioAlteracao = dadosReader["UsuarioAlteracao"].ToString();

                        _tipo.DataInclusao = Convert.ToDateTime(dadosReader["DataInc"].ToString());
                        try
                        {
                            _tipo.DataAlteracao = Convert.ToDateTime(dadosReader["DataAlt"].ToString());
                        }
                        catch { }
                        _lista.Add(_tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public bool Gravar(List<Suprimentos.Metas> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;

            if (_lista.Count == 0)
                return true;

            try
            {
                foreach (var item in _lista)
                {
                    query.Clear();

                    if (item.Existe)
                    {
                        query.Append("Update Niff_Sup_MetasAprovadores");
                        query.Append("   set Meta = " + item.ValorMeta.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("     , IdUsuarioAlt = " + Publicas._usuario.Id);
                        query.Append("     , dataAlt = Sysdate");
                        query.Append(" Where Id = " + item.Id);
                    }
                    else
                    {
                        query.Append("Insert into Niff_Sup_MetasAprovadores");
                        query.Append(" ( id, idempresa,  referencia, meta, idusuarioinc, datainc)");
                        //query.Append(" ( id, idempresa, codintfunc, referencia, meta, idusuarioinc, datainc)");
                        query.Append(" Values ( (Select Nvl(Max(id),0) +1 next From Niff_Sup_MetasAprovadores) ");
                        query.Append("        , " + item.IdEmpresa);
                        //query.Append("        , " + item.CodIntFunc);
                        query.Append("        , " + item.Referencia);
                        query.Append("        , " + item.ValorMeta.ToString().Replace(".", "").Replace(",", "."));
                        query.Append("        , " + Publicas._usuario.Id);
                        query.Append("        , sysdate ");
                        query.Append(" )");
                    }
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                        break;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool ExcluirReferencia(int id)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Sup_MetasAprovadores");
                query.Append(" Where id = " + id);
                return sessao.ExecuteSqlTransaction(query.ToString());
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        public bool ExcluirTodos(decimal idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Delete Niff_Sup_MetasAprovadores");
                query.Append(" Where IdEmpresa = " + idEmpresa);
                return sessao.ExecuteSqlTransaction(query.ToString());
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }

        #endregion

        public List<Suprimentos.Pedidos> Listar(int idEmpresa, string referencia, string Status)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Suprimentos.Pedidos> _lista = new List<Suprimentos.Pedidos>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select c.numeropedido, c.datapedido, Decode(c.statuspedido,'A','Aberto','C','Cancelado','F','Fechado','Aprovado') Status");
                query.Append("     , SUBSTR( u.nome, 1, INSTR( u.nome,' ')-1 ) || Substr( u.nome, INSTR( u.nome, ' ', -1)) UsuarioGerou");
                query.Append("     , f.nrforn || ' - ' || f.rsocialforn Fornecedor");
                query.Append("     , decode(ua.Idusuario, 64, 'Mensal', 'Emergencial') TipoPedido");
                query.Append("     , SUBSTR(ua.nome, 1, INSTR(ua.nome, ' ') - 1) || Substr(ua.nome, INSTR(ua.nome, ' ', -1)) Aprovador");

                query.Append("  from cpr_pedidocompras c, Niff_Chm_Usuarios u, niff_chm_empresas e, Bgm_Fornecedor f");
                query.Append("     , cpr_itensdepedido i, est_cadmaterial m, flp_funcionarios fu, niff_chm_usuarios ua");

                query.Append(" Where u.usuarioacesso = c.usuariogeroupedido");
                query.Append("   and c.codigoforn = f.codigoforn");
                query.Append("   and e.codigoglobus = lpad(c.codigoempresa, 3, '0') || '/' || lpad(c.codigoFl, 3, '0')");
                query.Append("   and e.idempresa = " + idEmpresa);
                query.Append("   and To_char(c.datapedido,'yyyymm') = " + referencia);

                query.Append("   And i.codigomatint = m.codigomatint");
                query.Append("   And i.codintFunc = fu.codintfunc");
                query.Append("   And i.numeropedido = c.numeropedido");
                query.Append("   And ua.codfunc = fu.codintfunc");
                query.Append(" Group By c.numeropedido, c.datapedido, Decode(c.statuspedido,'A','Aberto','C','Cancelado','F','Fechado','Aprovado')");
                query.Append("     , u.nome, f.nrforn, f.rsocialforn, ua.nome, ua.Idusuario");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Suprimentos.Pedidos _tipo = new Suprimentos.Pedidos();

                        try
                        {
                            _tipo.Id = Convert.ToInt32(dadosReader["id"].ToString());
                        }
                        catch { }

                        _tipo.Existe = _tipo.Id != 0;
                        _tipo.IdEmpresa = idEmpresa;
                        _tipo.Referencia = Convert.ToInt32(referencia);
                        
                        _tipo.ReferenciaFormatada = _tipo.Referencia.ToString().Substring(4, 2) + "/" + _tipo.Referencia.ToString().Substring(0, 4);
                        _tipo.NumeroPedido = dadosReader["numeropedido"].ToString();
                        _tipo.Status = dadosReader["Status"].ToString();
                        _tipo.UsuarioInclusao = dadosReader["UsuarioGerou"].ToString();
                        _tipo.Fornecedor = dadosReader["Fornecedor"].ToString();
                        _tipo.Aprovador = dadosReader["aprovador"].ToString();


                        switch (dadosReader["TipoPedido"].ToString())
                        {
                            case "Emergencial": // quando nao gravado traz emergencial
                                _tipo.TipoPedido = Publicas.TipoPedido.Emergencial;
                                break;
                            case "Mensal":
                                _tipo.TipoPedido = Publicas.TipoPedido.Mensal;
                                break;
                            default:
                                _tipo.TipoPedido = Publicas.TipoPedido.Emergencial;
                                break;
                        }
                        
                        _tipo.DataPedido = Convert.ToDateTime(dadosReader["datapedido"].ToString());
                       
                        _lista.Add(_tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public List<Suprimentos.ItensPedido> ListarItens(int idEmpresa, string referencia, string Status)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Suprimentos.ItensPedido> _lista = new List<Suprimentos.ItensPedido>();
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                query.Append("Select i.qtdepedido qtde, i.codigomatint codmat, m.descricaomat material, i.valorunitariopedido valor, i.dataaprovpedido dataAprovado");
                query.Append("     , i.codintaprovador, i.codintfunc, f.nomefunc, m.codigogrd, c.numeropedido");
                query.Append("  from cpr_pedidocompras c, cpr_itensdepedido i, est_cadmaterial m, flp_funcionarios f, niff_chm_empresas e");
                query.Append(" Where i.codigomatint = m.codigomatint");
                query.Append("   and i.codintFunc = f.codintfunc(+) ");
                query.Append("   and i.numeropedido = c.numeropedido");
                query.Append("   and e.codigoglobus = lpad(c.codigoempresa, 3, '0') || '/' || lpad(c.codigoFl, 3, '0')");
                query.Append("   and e.idempresa = " + idEmpresa);
                query.Append("   and To_char(c.datapedido,'yyyymm') = " + referencia);

                if (!Publicas._usuario.Desenvolvedor && Publicas._usuario.UsuarioAcesso != "RMALVES")
                    query.Append("   and c.usuariogeroupedido = '" + Publicas._usuario.UsuarioAcesso + "'");

                query.Append(" Union all ");

                query.Append("Select o.qtdeitoutpedido qtde, o.codigomatavulso codmat, DESCRICAOMATAVULSO material, o.vlrunitariooutpedido valor, o.dataaprovoutpedido dataAprovado");
                query.Append("     , o.codintaprovador, o.codintfunc, f.nomefunc, m.codigogrd, c.numeropedido");
                query.Append("  from cpr_pedidocompras c, cpr_itensoutrospedido o, cpr_cadmaterialavulso m, flp_funcionarios f, niff_chm_empresas e");
                query.Append(" Where o.codigomatavulso = m.codigomatavulso");
                query.Append("   and o.codintFunc = f.codintfunc(+) ");
                query.Append("   and o.numeropedido = c.numeropedido");
                query.Append("   and e.codigoglobus = lpad(c.codigoempresa, 3, '0') || '/' || lpad(c.codigoFl, 3, '0')");
                query.Append("   and e.idempresa = " + idEmpresa);
                query.Append("   and To_char(c.datapedido,'yyyymm') = " + referencia);

                if (!Publicas._usuario.Desenvolvedor && Publicas._usuario.UsuarioAcesso != "RMALVES")
                    query.Append("   and c.usuariogeroupedido = '" + Publicas._usuario.UsuarioAcesso + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                dadosReader = executar.ExecuteQuery();

                using (dadosReader)
                {
                    while (dadosReader.Read())
                    {
                        Suprimentos.ItensPedido _tipo = new Suprimentos.ItensPedido();

                        //_tipo.Existe = true;
                        //_tipo.Id = Convert.ToInt32(dadosReader["id"].ToString());
                        _tipo.GrupoDespesa = Convert.ToInt32(dadosReader["codigogrd"].ToString());
                        _tipo.NumeroPedido = dadosReader["numeropedido"].ToString();
                        _tipo.Quantidade = Convert.ToDecimal(dadosReader["qtde"].ToString());
                        _tipo.ValorUnitario = Convert.ToDecimal(dadosReader["valor"].ToString());

                        _tipo.Aprovador = dadosReader["NomeFunc"].ToString();
                        _tipo.Material = dadosReader["Material"].ToString();

                        try
                        {
                            _tipo.DataAprovacao = Convert.ToDateTime(dadosReader["dataAprovado"].ToString());
                        }
                        catch { }

                        _lista.Add(_tipo);
                    }
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
            }
            finally
            {
                sessao.Desconectar();
            }
            return _lista;
        }

        public bool Gravar(List<Suprimentos.Pedidos> _lista)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            bool retorno = false;

            if (_lista.Count == 0)
                return true;

            try
            {
                foreach (var item in _lista)
                {
                    query.Clear();

                    if (item.Existe)
                    {
                        query.Append("Update Niff_Sup_Pedidos");
                        query.Append("   set tipopedido = '" + item.TipoPedido.ToString() + "'");
                        query.Append("     , idusuario = " + Publicas._usuario.Id);
                        query.Append(" Where Id = " + item.Id);
                    }
                    else
                    {
                        query.Append("Insert into Niff_Sup_Pedidos");
                        query.Append(" ( id, idempresa, idusuario, referencia, numeropedido, tipopedido )");
                        query.Append(" Values ( (Select Nvl(Max(id),0) +1 next From Niff_Sup_Pedidos) ");
                        query.Append("        , " + item.IdEmpresa);
                        query.Append("        , " + Publicas._usuario.Id);
                        query.Append("        , " + item.Referencia);
                        query.Append("        , " + item.NumeroPedido);
                        query.Append("        , '" + item.TipoPedido.ToString() + "'");
                        query.Append(" )");
                    }
                    retorno = sessao.ExecuteSqlTransaction(query.ToString());

                    if (!retorno)
                        break;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                return false;
            }
            finally
            {
                sessao.Desconectar();
            }
        }


    }
}
