using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class PedidoGlobusDAO
    {
        IDataReader pedidoReader;

        public List<PedidoGlobus> Consultar(string pedido)
        {
            List<PedidoGlobus> _lista = new List<PedidoGlobus>();
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;

            try
            {

                query.Append("Select o.*,  m.codigomatavulso || ' - ' || m.descricaomatavulso material ");
                query.Append("  from cpr_itensoutrospedido o");
                query.Append("     , Est_Cadmaterialavulso m");
                query.Append(" Where numeropedido = " + pedido);
                query.Append("   And m.codigomatavulso = To_Char(o.Codigomatavulso)");
                Query executar = sessao.CreateQuery(query.ToString());

                pedidoReader = executar.ExecuteQuery();

                using (pedidoReader)
                {
                    while (pedidoReader.Read())
                    {
                        PedidoGlobus _pedido = new PedidoGlobus();
                        _pedido.Pedido = pedidoReader["NUMEROPEDIDO"].ToString();
                        _pedido.Descricao = pedidoReader["DESCITOUTPEDIDO"].ToString();
                        _pedido.Material = pedidoReader["material"].ToString();
                        _pedido.Quantidade = Convert.ToInt32(pedidoReader["QTDEITOUTPEDIDO"].ToString());
                        _pedido.ValorUnitario = Convert.ToDecimal(pedidoReader["VLRUNITARIOOUTPEDIDO"].ToString());
                        _pedido.Sequencial = Convert.ToInt32(pedidoReader["SEQITOUTPEDIDO"].ToString());
                        _pedido.Desconto  = Convert.ToInt32(pedidoReader["VLRDESCONTOOUTPEDIDO"].ToString());
                        _pedido.IPI = Convert.ToInt32(pedidoReader["VLRIPIOUTPEDIDO"].ToString());
                        _pedido.ICMS = Convert.ToInt32(pedidoReader["VLRICMSOUTPEDIDO"].ToString());
                        _pedido.Seguro = Convert.ToInt32(pedidoReader["VLRSEGUROOUTPEDIDO"].ToString());
                        _pedido.ISS = Convert.ToInt32(pedidoReader["VLRISSOUTPEDIDO"].ToString());
                        _pedido.Frete = Convert.ToInt32(pedidoReader["VLRFRETEOUTPEDIDO"].ToString());
                        _pedido.ICMSSubstituicao = Convert.ToInt32(pedidoReader["VLRICMSSUBSTOUTPEDIDO"].ToString());

                        switch (pedidoReader["SERVICO"].ToString())
                        {
                            case "P": _pedido.Tipo = Publicas.TipoPedidoGlobus.Produto;
                                break;
                            case "S": _pedido.Tipo = Publicas.TipoPedidoGlobus.Servico;
                                break;
                        }

                        _pedido.TipoOriginal = _pedido.Tipo;
                        _lista.Add(_pedido);
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

        public bool Gravar(List<PedidoGlobus> pedidos)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            bool retorno = true;
            Publicas.mensagemDeErro = string.Empty;

            try
            {
                foreach (PedidoGlobus item in pedidos)
                {
                    query.Clear();
                    query.Append("Update Cpr_Itensoutrospedido");
                    query.Append("   Set Servico = '" + (item.Tipo == Publicas.TipoPedidoGlobus.Servico ? "S" : "P") + "'");
                    query.Append(" Where NUMEROPEDIDO = " + item.Pedido);
                    query.Append("   and SEQITOUTPEDIDO = " + item.Sequencial.ToString());

                    retorno = sessao.ExecuteSqlTransaction(query.ToString());
                }
            }
            catch (Exception ex)
            {
                Publicas.mensagemDeErro = ex.Message;
                retorno = false;
            }
            finally
            {
                sessao.Desconectar();
            }
            return retorno;
        }
    }
}
