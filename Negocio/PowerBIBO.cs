using Dados;
using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PowerBIBO
    {
        #region Email de Acesso

        public List<PowerBI.EmailDeAcesso> Listar(bool apenasAtivos)
        {
            return new PowerBIDAO().Listar(apenasAtivos);
        }

        public PowerBI.EmailDeAcesso Consultar(string email)
        {
            return new PowerBIDAO().Consulta(email);
        }

        public PowerBI.EmailDeAcesso Consulta(int id)
        {
            return new PowerBIDAO().Consulta(id);
        }

        public bool Gravar(PowerBI.EmailDeAcesso tipo)
        {
            return new PowerBIDAO().Gravar(tipo);
        }

        public bool Excluir(int id)
        {
            return new PowerBIDAO().Excluir(id);
        }
        #endregion

        #region Empresas Autorizados por E-mail de Acesso
        public List<PowerBI.EmpresasAutorizadas> Listar(int id)
        {
            return new PowerBIDAO().Listar(id);
        }

        public bool Gravar(List<PowerBI.EmpresasAutorizadas> _lista)
        {
            return new PowerBIDAO().Gravar(_lista);
        }
        #endregion

        #region Usuarios Autorizados por E-mail de Acesso
        public List<PowerBI.UsuariosAutorizados> ListarUsuarios(int id)
        {
            return new PowerBIDAO().ListarUsuarios(id);
        }

        public bool Gravar(List<PowerBI.UsuariosAutorizados> _lista)
        {
            return new PowerBIDAO().Gravar(_lista);
        }

        public bool ExcluirUsuario(int id)
        {
            return new PowerBIDAO().ExcluirUsuario(id);
        }

        public bool ExcluirTodos(int id)
        {
            return new PowerBIDAO().ExcluirTodos(id);
        }
        #endregion

        #region Relatorios

        public List<PowerBI.Relatorios> ListarRelatorios(bool ativos)
        {
            return new PowerBIDAO().ListarRelatorios(ativos);
        }

        public PowerBI.Relatorios ConsultarRelatorios(int id)
        {
            return new PowerBIDAO().ConsultaRelatorios(id);
        }

        public bool Gravar(PowerBI.Relatorios tipo)
        {
            return new PowerBIDAO().Gravar(tipo);
        }

        public bool ExcluirRelatorios(int id)
        {
            return new PowerBIDAO().ExcluirRelatorios(id);
        }

        public int Proximo()
        {
            return new PowerBIDAO().Proximo();
        }
        #endregion

        #region Quantidade de acessos

        public PowerBI.Acessos ConsultarAcesso(int email, int relatorio, DateTime data)
        {
            return new PowerBIDAO().ConsultaAcesso(email, relatorio, data);
        }

        public List<PowerBI.Resumo> ResumoAcesso(DateTime dataInicio, DateTime dataFim)
        {
            List<PowerBI.Acessos> _lista = new PowerBIDAO().ResumoAcesso(dataInicio, dataFim);
            List<PowerBI.Resumo> _listaResumo = new List<PowerBI.Resumo>();

            foreach (var itemG in _lista.GroupBy(g => g.Nome))
            {
                PowerBI.Resumo _resumo = new PowerBI.Resumo();
                _resumo.Relatorio = itemG.Key;
                _resumo.Data = _lista.Max(m => m.Data);

                foreach (var item in _lista.Where(w => w.Nome == itemG.Key)
                                           .OrderBy(o => o.IdEmail))
                {
                    _resumo.Total = _resumo.Total + item.Quantidade;

                    switch (item.IdEmail)
                    {
                        case 1:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna1 = item.Quantidade;
                            _resumo.idEmailColuna1 = item.IdEmail;
                            break;
                        case 2:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna2 = item.Quantidade;
                            _resumo.idEmailColuna2 = item.IdEmail;
                            break;
                        case 3:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna3 = item.Quantidade;
                            _resumo.idEmailColuna3 = item.IdEmail;
                            break;
                        case 4:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna4 = item.Quantidade;
                            _resumo.idEmailColuna4 = item.IdEmail;
                            break;
                        case 5:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna5 = item.Quantidade;
                            _resumo.idEmailColuna5 = item.IdEmail;
                            break;
                        case 6:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna6 = item.Quantidade;
                            _resumo.idEmailColuna6 = item.IdEmail;
                            break;
                        case 7:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna7 = item.Quantidade;
                            _resumo.idEmailColuna7 = item.IdEmail;
                            break;
                        case 8:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna8 = item.Quantidade;
                            _resumo.idEmailColuna8 = item.IdEmail;
                            break;
                        case 9:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna9 = item.Quantidade;
                            _resumo.idEmailColuna9 = item.IdEmail;
                            break;
                        case 10:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna10 = item.Quantidade;
                            _resumo.idEmailColuna10 = item.IdEmail;
                            break;
                        case 11:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna11 = item.Quantidade;
                            _resumo.idEmailColuna11 = item.IdEmail;
                            break;
                        case 12:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna12 = item.Quantidade;
                            _resumo.idEmailColuna12 = item.IdEmail;
                            break;
                        case 13:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna13 = item.Quantidade;
                            _resumo.idEmailColuna13 = item.IdEmail;
                            break;
                        case 14:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna14 = item.Quantidade;
                            _resumo.idEmailColuna14 = item.IdEmail;
                            break;
                        case 15:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna15 = item.Quantidade;
                            _resumo.idEmailColuna15 = item.IdEmail;
                            break;
                        case 16:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna16 = item.Quantidade;
                            _resumo.idEmailColuna16 = item.IdEmail;
                            break;
                        case 17:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna17 = item.Quantidade;
                            _resumo.idEmailColuna17 = item.IdEmail;
                            break;
                        case 18:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna18 = item.Quantidade;
                            _resumo.idEmailColuna18 = item.IdEmail;
                            break;
                        case 19:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna19 = item.Quantidade;
                            _resumo.idEmailColuna19 = item.IdEmail;
                            break;
                        case 20:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna20 = item.Quantidade;
                            _resumo.idEmailColuna20 = item.IdEmail;
                            break;
                        case 21:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna21 = item.Quantidade;
                            _resumo.idEmailColuna21 = item.IdEmail;
                            break;
                        case 22:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna22 = item.Quantidade;
                            _resumo.idEmailColuna22 = item.IdEmail;
                            break;
                        case 23:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna23 = item.Quantidade;
                            _resumo.idEmailColuna23 = item.IdEmail;
                            break;
                        case 24:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna24 = item.Quantidade;
                            _resumo.idEmailColuna24 = item.IdEmail;
                            break;
                        case 25:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna25 = item.Quantidade;
                            _resumo.idEmailColuna25 = item.IdEmail;
                            break;
                        case 26:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna26 = item.Quantidade;
                            _resumo.idEmailColuna26 = item.IdEmail;
                            break;
                        case 27:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna27 = item.Quantidade;
                            _resumo.idEmailColuna27 = item.IdEmail;
                            break;
                        case 28:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna28 = item.Quantidade;
                            _resumo.idEmailColuna28 = item.IdEmail;
                            break;
                        case 29:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna29 = item.Quantidade;
                            _resumo.idEmailColuna29 = item.IdEmail;
                            break;
                        case 30:
                            if (item.Quantidade != 0)
                                _resumo.QuantidadeColuna30 = item.Quantidade;
                            _resumo.idEmailColuna30 = item.IdEmail;
                            break;
                    }
                }

                _listaResumo.Add(_resumo);
            }
            return _listaResumo;
        }

        public bool Gravar(PowerBI.Acessos tipo)
        {
            return new PowerBIDAO().Gravar(tipo);
        }

        public bool ExcluirAcessos(int id)
        {
            return new PowerBIDAO().ExcluirAcessos(id);
        }

        #endregion
    }
}
