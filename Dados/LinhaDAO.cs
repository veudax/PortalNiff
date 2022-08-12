using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class LinhaDAO
    {
        IDataReader linhaReader;

        public List<Linha> Listar(string empresaGlobus)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Linha> _lista = new List<Linha>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select l.codigolinha, l.codintlinha, l.nomelinha, p.flg_linha_disponivel ");
                query.Append("  From bgm_cadlinhas l, t_Trf_Parametros_Linha p ");
                query.Append(" Where p.codintlinha = l.codintlinha ");
                query.Append("   And p.flg_linha_disponivel = 'S'");
                query.Append("   And lPad(l.codigoempresa,3,'0') || '/' || lPad(l.codigofl, 3,'0') = '" + empresaGlobus + "'");
                
                Query executar = sessao.CreateQuery(query.ToString());

                linhaReader = executar.ExecuteQuery();

                Linha _categoria = new Linha();
                _categoria.Id = 0;

                _categoria.Nome = " - ";
                _categoria.Ativo = true;
                _categoria.Codigo = "0";

                _lista.Add(_categoria);

                using (linhaReader)
                {
                    while (linhaReader.Read())
                    {
                        if (!linhaReader["nomelinha"].ToString().Contains("RESERVA") &&
                            !linhaReader["nomelinha"].ToString().Contains("NEGREIRO") &&
                            !linhaReader["nomelinha"].ToString().Contains("FISCALIZAÇÃO"))
                        {

                            _categoria = new Linha();
                            _categoria.Id = Convert.ToInt32(linhaReader["codintlinha"].ToString());

                            _categoria.Nome = linhaReader["codigolinha"].ToString() + " - " + linhaReader["nomelinha"].ToString();
                            _categoria.Ativo = linhaReader["flg_linha_disponivel"].ToString() == "S";
                            _categoria.Codigo = linhaReader["codigolinha"].ToString();

                            _lista.Add(_categoria);
                        }
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

        public List<Linha> ListarPesquisa(string empresaGlobus)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<Linha> _lista = new List<Linha>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select l.codigolinha, l.codintlinha, l.nomelinha, p.flg_linha_disponivel ");
                query.Append("  From bgm_cadlinhas l, t_Trf_Parametros_Linha p ");
                query.Append(" Where p.codintlinha = l.codintlinha ");
                query.Append("   And p.flg_linha_disponivel = 'S'");
                query.Append("   And lPad(l.codigoempresa,3,'0') || '/' || lPad(l.codigofl, 3,'0') = '" + empresaGlobus + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                linhaReader = executar.ExecuteQuery();

                Linha _categoria = new Linha();
                _categoria.Id = 0;

                _categoria.Nome = " - ";
                _categoria.Ativo = true;
                _categoria.Codigo = "0";

                _lista.Add(_categoria);

                using (linhaReader)
                {
                    while (linhaReader.Read())
                    {

                            _categoria = new Linha();
                            _categoria.Id = Convert.ToInt32(linhaReader["codintlinha"].ToString());

                            _categoria.Nome = linhaReader["codigolinha"].ToString() + " - " + linhaReader["nomelinha"].ToString();
                            _categoria.Ativo = linhaReader["flg_linha_disponivel"].ToString() == "S";
                            _categoria.Codigo = linhaReader["codigolinha"].ToString();

                            _lista.Add(_categoria);
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

        public Linha Consultar(string empresaGlobus, string linha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            Linha _categoria = new Linha();

            try
            {
                query.Append("Select l.codigolinha, l.codintlinha, l.nomelinha, p.flg_linha_disponivel, l.FLG_MUNIC_INTEREST, l.CodigoEmpresa, l.CodigoFl ");
                query.Append("     , Decode(Nvl(l.Flg_Munic_Interest, 'Z'), 'M', 'Municipal', 'U', 'Intermunicipal', 'I', 'Interestadual', 'N', 'Internacional', 'G', 'Gratuita', 'T', 'Turistica', 'Nao cadastrado') Classificacao");
                query.Append("  From bgm_cadlinhas l, t_Trf_Parametros_Linha p ");
                query.Append(" Where p.codintlinha = l.codintlinha ");
                query.Append("   And p.flg_linha_disponivel = 'S'");
                query.Append("   And lPad(l.codigoempresa,3,'0') || '/' || lPad(l.codigofl, 3,'0') = '" + empresaGlobus + "'");
                query.Append("   And l.CodigoLinha = '" + linha + "'");

                Query executar = sessao.CreateQuery(query.ToString());

                linhaReader = executar.ExecuteQuery();

                using (linhaReader)
                {
                    if (linhaReader.Read())
                    {
                        _categoria.Id = Convert.ToInt32(linhaReader["codintlinha"].ToString());
                        _categoria.Existe = true;
                        _categoria.Nome = linhaReader["codigolinha"].ToString() + " - " + linhaReader["nomelinha"].ToString();
                        _categoria.Ativo = linhaReader["flg_linha_disponivel"].ToString() == "S";
                        _categoria.Codigo = linhaReader["codigolinha"].ToString();
                        _categoria.Classificacao = linhaReader["FLG_MUNIC_INTEREST"].ToString();
                        _categoria.DescricaoClassificacao  = linhaReader["Classificacao"].ToString();
                        _categoria.Empresa = Convert.ToInt32(linhaReader["CodigoEmpresa"].ToString()).ToString("000") + "/" +
                            Convert.ToInt32(linhaReader["CodigoFl"].ToString()).ToString("000");
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
            return _categoria;
        }

        public Linha Consultar(int codigo)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            Linha _categoria = new Linha();

            try
            {
                query.Append("Select l.codigolinha, l.codintlinha, l.nomelinha, p.flg_linha_disponivel, l.FLG_MUNIC_INTEREST ");
                query.Append("     , Decode(Nvl(l.Flg_Munic_Interest, 'Z'), 'M', 'Municipal', 'U', 'Intermunicipal', 'I', 'Interestadual', 'N', 'Internacional', 'G', 'Gratuita', 'T', 'Turistica', 'Nao cadastrado') Classificacao");
                query.Append("  From bgm_cadlinhas l, t_Trf_Parametros_Linha p ");
                query.Append(" Where p.codintlinha = l.codintlinha ");
                query.Append("   And l.codintlinha = " + codigo );

                Query executar = sessao.CreateQuery(query.ToString());

                linhaReader = executar.ExecuteQuery();

                using (linhaReader)
                {
                    if (linhaReader.Read())
                    {
                        _categoria.Id = Convert.ToInt32(linhaReader["codintlinha"].ToString());
                        _categoria.Existe = true;
                        _categoria.Nome = linhaReader["codigolinha"].ToString() + " - " + linhaReader["nomelinha"].ToString();
                        _categoria.Ativo = linhaReader["flg_linha_disponivel"].ToString() == "S";
                        _categoria.Codigo = linhaReader["codigolinha"].ToString();
                        _categoria.Classificacao = linhaReader["FLG_MUNIC_INTEREST"].ToString();
                        _categoria.DescricaoClassificacao = linhaReader["Classificacao"].ToString();

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
            return _categoria;
        }

        public Linha Consultar(string codigo, bool ativas)
        { 
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            Publicas.mensagemDeErro = string.Empty;
            Linha _categoria = new Linha();

            try
            {
                query.Append("Select l.codigolinha, l.codintlinha, l.nomelinha, p.flg_linha_disponivel, l.FLG_MUNIC_INTEREST, l.CodigoEmpresa, l.CodigoFl ");
                query.Append("     , Decode(Nvl(l.Flg_Munic_Interest, 'Z'), 'M', 'Municipal', 'U', 'Intermunicipal', 'I', 'Interestadual', 'N', 'Internacional', 'G', 'Gratuita', 'T', 'Turistica', 'Nao cadastrado') Classificacao");
                query.Append("  From bgm_cadlinhas l, t_Trf_Parametros_Linha p ");
                query.Append(" Where p.codintlinha = l.codintlinha ");
                query.Append("   And l.CodigoLinha = '" + codigo + "'");

                if (ativas)
                    query.Append("   And p.flg_linha_disponivel = 'S'");

                Query executar = sessao.CreateQuery(query.ToString());

                linhaReader = executar.ExecuteQuery();

                using (linhaReader)
                {
                    if (linhaReader.Read())
                    {
                        _categoria.Id = Convert.ToInt32(linhaReader["codintlinha"].ToString());
                        _categoria.Existe = true;
                        _categoria.Nome = linhaReader["codigolinha"].ToString() + " - " + linhaReader["nomelinha"].ToString();
                        _categoria.Ativo = linhaReader["flg_linha_disponivel"].ToString() == "S";
                        _categoria.Codigo = linhaReader["codigolinha"].ToString();
                        _categoria.Classificacao = linhaReader["FLG_MUNIC_INTEREST"].ToString();
                        _categoria.DescricaoClassificacao = linhaReader["Classificacao"].ToString();
                        _categoria.Empresa = Convert.ToInt32(linhaReader["CodigoEmpresa"].ToString()).ToString("000") + "/" +
                            Convert.ToInt32(linhaReader["CodigoFl"].ToString()).ToString("000");
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
            return _categoria;
        }

        public List<SecaoDaLinha> Listar (int IdLinha)
        {
            StringBuilder query = new StringBuilder();
            Sessao sessao = new Sessao();
            List<SecaoDaLinha> _lista = new List<SecaoDaLinha>();
            Publicas.mensagemDeErro = string.Empty;
            try
            {

                query.Append("Select Distinct s.cod_secao, s.cod_seq_secao, s.nom_secao, ls.cod_linha ");
                query.Append("  From t_trf_linha_secao ls, t_trf_secao s ");
                query.Append(" Where s.cod_seq_secao = ls.cod_seq_secao ");
                query.Append("   And ls.flg_disponivel = 'S'");
                query.Append("   And s.flg_disponivel = 'S'");
                query.Append("   And ls.Cod_linha = " + IdLinha);

                Query executar = sessao.CreateQuery(query.ToString());

                linhaReader = executar.ExecuteQuery();

                SecaoDaLinha _categoria = new SecaoDaLinha();
                _categoria.Id = 0;

                _categoria.Nome = " - ";
                _categoria.Codigo = "0";

                _lista.Add(_categoria);

                using (linhaReader)
                {
                    while (linhaReader.Read())
                    {                       

                        _categoria = new SecaoDaLinha();
                        _categoria.Id = Convert.ToInt32(linhaReader["cod_seq_secao"].ToString());
                        _categoria.IdLinha = Convert.ToInt32(linhaReader["cod_linha"].ToString());

                        _categoria.Nome = linhaReader["nom_secao"].ToString();
                        //_categoria.NomeExibicao = linhaReader["cod_secao"].ToString() + " - " + linhaReader["nom_secao"].ToString();
                        _categoria.NomeExibicao = linhaReader["nom_secao"].ToString();
                        _categoria.Codigo = linhaReader["cod_secao"].ToString();

                        _lista.Add(_categoria);
                        
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
    }
}
