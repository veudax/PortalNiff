using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BancoDeHorasBO
    {
        public List<BancoDeHoras> Listar(int idColaborador, int idSupervisor, DateTime Inicio, DateTime Fim, int idDepartamento)
        {
            //List <BancoDeHoras> _horas = new BancoDeHorasDAO().Listar(39, idSupervisor, Inicio, Fim, idDepartamento);
            List<BancoDeHoras> _horas = new BancoDeHorasDAO().Listar(idColaborador, idSupervisor, Inicio, Fim, idDepartamento);
            BancoDeHoras _bancoHoras = new BancoDeHoras();
            List<BancoDeHoras> _horasCalculado = new List<BancoDeHoras>();

            DateTime _data;
            DateTime _entrada;
            DateTime _saida;
            DateTime _saidaAlmoco;
            DateTime _voltaAlmoco;
            DateTime _entrada1;
            DateTime _saida1;
            DateTime _antes24h;
            DateTime _depois24h;

            string _nome = "";
            bool _temHorarios = false;

            #region Variaveis para calculo
            DateTime _entradaSemana = DateTime.MinValue;
            DateTime _entradaMinima = DateTime.MinValue;
            DateTime _entradaMaxima = DateTime.MinValue;

            DateTime _saidaSemana = DateTime.MinValue;
            DateTime _saidaSexta = DateTime.MinValue;
            DateTime _saidaSemanaMaxima = DateTime.MinValue;
            DateTime _saidaSextaMaxima = DateTime.MinValue;

            DateTime _inicioAlmoco = DateTime.MinValue;
            DateTime _fimAlmoco = DateTime.MinValue;

            double ExtraEntrada = 0;
            double ExtraSaida = 0;
            double ExtraSaidaAlmoco = 0;
            double ExtraVoltaAlmoco = 0;

            double IncompletaEntrada = 0;
            double IncompletaSaida = 0;
            double IncompletaSaidaAlmoco = 0;
            double IncompletaVoltaAlmoco = 0;

            double Extras = 0;
            double ExtrasReal = 0;
            double Incompletas = 0;
            double IncompletasReal = 0;
            double Liquido = 0;
            double Extras1 = 0;
            double Incompletas1 = 0;
            double Liquido1 = 0;
            #endregion

            //int[] colaboradores = .ToArray();

            foreach (var col in _horas.GroupBy(g => new { g.IdColaborador, g.NomeColaborador })
                                      .Select(s => new { IdColaborador = s.Key.IdColaborador, NomeColaborador = s.Key.NomeColaborador }))
            {
                _data = Inicio;
                Extras = 0;
                Incompletas = 0;
                Liquido = 0;
                Extras1 = 0;
                Incompletas1 = 0;
                Liquido1 = 0;
                _nome = col.NomeColaborador;

                while (_data.Date <= Fim.Date)
                {
                    if (_data.Date == DateTime.Now.Date)
                    {
                        _data = _data.AddDays(1);
                        continue;
                    }

                    try
                    {
                        _temHorarios = false;

                        #region Variaveis para calculo
                        _entradaSemana = new DateTime(_data.Year, _data.Month, _data.Day, 08, 00, 00);
                        _entradaMinima = new DateTime(_data.Year, _data.Month, _data.Day, 07, 55, 00);
                        _entradaMaxima = new DateTime(_data.Year, _data.Month, _data.Day, 08, 05, 00);

                        _antes24h = new DateTime(_data.Year, _data.Month, _data.Day, 23, 59, 00);
                        _depois24h = new DateTime(_data.Year, _data.Month, _data.Day, 00, 00, 00);
                        _depois24h = _depois24h.AddDays(1);

                        _saidaSemana = new DateTime(_data.Year, _data.Month, _data.Day, 18, 00, 00);
                        _saidaSexta = new DateTime(_data.Year, _data.Month, _data.Day, 17, 00, 00);
                        _saidaSemanaMaxima = new DateTime(_data.Year, _data.Month, _data.Day, 18, 05, 00);
                        _saidaSextaMaxima = new DateTime(_data.Year, _data.Month, _data.Day, 17, 05, 00);

                        _inicioAlmoco = new DateTime(_data.Year, _data.Month, _data.Day, 12, 00, 00);
                        _fimAlmoco = new DateTime(_data.Year, _data.Month, _data.Day, 13, 00, 00);

                        ExtraEntrada = 0;
                        ExtraSaida = 0;
                        ExtraSaidaAlmoco = 0;
                        ExtraVoltaAlmoco = 0;

                        IncompletaEntrada = 0;
                        IncompletaSaida = 0;
                        IncompletaSaidaAlmoco = 0;
                        IncompletaVoltaAlmoco = 0;

                        #endregion

                        _entrada = DateTime.MinValue;
                        _saida = DateTime.MinValue;
                        _entrada1 = DateTime.MinValue;
                        _saida1 = DateTime.MinValue;
                        _saidaAlmoco = DateTime.MinValue;
                        _voltaAlmoco = DateTime.MinValue;

                        foreach (var item in _horas.Where(w => w.IdColaborador == col.IdColaborador && w.Data == _data.Date).OrderBy(o => o.Entrada))
                        {
                            _temHorarios = true;

                            if (_entrada == DateTime.MinValue)
                            {
                                _entrada = item.Entrada;
                                _saida = item.Saida;

                                if (_horas.Where(w => w.IdColaborador == col.IdColaborador && w.Data == _data.Date).OrderBy(o => o.Entrada).Count() != 1)
                                {
                                    if (item.SaidaAlmoco.ToShortTimeString() == "00:00" && _entrada <= _inicioAlmoco && _saida <= _fimAlmoco)
                                        _saidaAlmoco = item.Saida;
                                }

                                if (item.SaidaAlmoco.ToShortTimeString() == "00:00" && _entrada <= _inicioAlmoco &&
                                    _horas.Where(w => w.IdColaborador == col.IdColaborador && w.Data == _data.Date).OrderBy(o => o.Entrada).Count() == 1 &&
                                    _saidaAlmoco.ToShortTimeString() == "00:00" && _voltaAlmoco.ToShortTimeString() == "00:00")
                                {
                                    _saidaAlmoco = _inicioAlmoco.AddHours(1);
                                    _voltaAlmoco = _fimAlmoco;
                                }
                            }

                            //Verifica se e jornada extra
                            if (_saida.AddMinutes(15) < item.Entrada && (item.EntradaAlmoco.ToShortTimeString() != "00:00" || _voltaAlmoco.ToShortTimeString() != "00:00"))
                            {
                                _entrada1 = item.Entrada;
                                _saida1 = item.Saida;
                            }
                            else
                            {
                                if (_entrada > item.Entrada)
                                    _entrada = item.Entrada;
                                else
                                {
                                    if (_entrada < item.Entrada && item.EntradaAlmoco.ToShortTimeString() == "00:00" && _voltaAlmoco.ToShortTimeString() == "00:00")
                                        _voltaAlmoco = item.Entrada;
                                    else
                                    {
                                        if (_voltaAlmoco > item.Entrada && _entrada < item.Entrada && item.EntradaAlmoco.ToShortTimeString() == "00:00")
                                        {
                                            // Quando o atestado é na hora do almoço
                                            if (_saidaAlmoco < item.Entrada)
                                            {
                                                _saidaAlmoco = _inicioAlmoco;
                                                _voltaAlmoco = _inicioAlmoco.AddHours(1);
                                            }
                                            else
                                                _voltaAlmoco = item.Entrada;
                                        }
                                    }
                                }

                                if (_saida < item.Saida)
                                    _saida = item.Saida;

                                if ((_saidaAlmoco.ToShortTimeString() == "00:00" && item.SaidaAlmoco.ToShortTimeString() != "00:00")
                                    || (_entrada <= _saidaAlmoco && item.SaidaAlmoco.ToShortTimeString() != "00:00"))
                                    _saidaAlmoco = item.SaidaAlmoco;

                                if (_voltaAlmoco.ToShortTimeString() == "00:00" && item.EntradaAlmoco.ToShortTimeString() != "00:00")
                                    _voltaAlmoco = item.EntradaAlmoco;
                            }
                        }

                        if (_temHorarios)
                        {

                            #region Calculo Extras
                            Feriado _feriado = new FeriadoBO().Consultar(_data);
                            if (_data.DayOfWeek == DayOfWeek.Saturday || _data.DayOfWeek == DayOfWeek.Sunday || _feriado.Existe)
                                ExtraEntrada = _saida.Subtract(_entrada).TotalMinutes;
                            else
                            {
                                if (_entrada < _entradaMinima)
                                    ExtraEntrada = _entradaSemana.Subtract(_entrada).TotalMinutes;

                                if ((_data.DayOfWeek != DayOfWeek.Friday && _saida > _saidaSemanaMaxima) ||
                                    (_data.DayOfWeek == DayOfWeek.Friday && _saida > _saidaSextaMaxima))
                                {
                                    if (_data.DayOfWeek == DayOfWeek.Friday)
                                        ExtraSaida = _saida.Subtract(_saidaSexta).TotalMinutes;
                                    else
                                        ExtraSaida = _saida.Subtract(_saidaSemana).TotalMinutes;
                                }

                                if (_saidaAlmoco > _inicioAlmoco && _inicioAlmoco != DateTime.MinValue)
                                    ExtraSaidaAlmoco = _saidaAlmoco.Subtract(_inicioAlmoco).TotalMinutes;

                                if (_voltaAlmoco < _fimAlmoco && _voltaAlmoco != DateTime.MinValue)
                                    ExtraVoltaAlmoco = _fimAlmoco.Subtract(_voltaAlmoco).TotalMinutes;

                                if (_entrada1 != DateTime.MinValue)
                                {
                                    if (_saida1 > _entrada1)
                                        ExtraSaida = ExtraSaida + _saida1.Subtract(_entrada1).TotalMinutes;
                                    else
                                    {
                                        _saida1 = _saida1.AddDays(1);
                                        ExtraSaida = ExtraSaida + _antes24h.Subtract(_entrada1).TotalMinutes + _saida1.Subtract(_depois24h).TotalMinutes;
                                    }
                                }
                            }
                            #endregion

                            #region Calculo Incompletas
                            if (_data.DayOfWeek != DayOfWeek.Saturday && _data.DayOfWeek != DayOfWeek.Sunday && !_feriado.Existe)
                            {
                                if (_entrada > _entradaMaxima)
                                {
                                    if (_entrada > _inicioAlmoco)
                                    {
                                        IncompletaEntrada = _inicioAlmoco.Subtract(_entradaSemana).TotalMinutes;
                                        if (_entrada > _fimAlmoco)
                                            IncompletaEntrada = IncompletaEntrada + _entrada.Subtract(_fimAlmoco).TotalMinutes;
                                    }
                                    else
                                        IncompletaEntrada = _entrada.Subtract(_entradaSemana).TotalMinutes;
                                }

                                if ((_data.DayOfWeek != DayOfWeek.Friday && _saida < _saidaSemana) ||
                                    (_data.DayOfWeek == DayOfWeek.Friday && _saida < _saidaSexta))
                                {
                                    if (_data.DayOfWeek == DayOfWeek.Friday)
                                        IncompletaSaida = _saidaSexta.Subtract(_saida).TotalMinutes;
                                    else
                                        IncompletaSaida = _saidaSemana.Subtract(_saida).TotalMinutes;
                                }

                                if (_saidaAlmoco < _inicioAlmoco && _saidaAlmoco != DateTime.MinValue)
                                    IncompletaSaidaAlmoco = _inicioAlmoco.Subtract(_saidaAlmoco).TotalMinutes;

                                if (_voltaAlmoco > _fimAlmoco && _voltaAlmoco != DateTime.MinValue)
                                    IncompletaVoltaAlmoco = _voltaAlmoco.Subtract(_fimAlmoco).TotalMinutes;
                            }
                            #endregion

                            Extras = Extras + ExtraEntrada + ExtraSaida + ExtraSaidaAlmoco + ExtraVoltaAlmoco;
                            Incompletas = Incompletas + IncompletaEntrada + IncompletaSaida + IncompletaSaidaAlmoco + IncompletaVoltaAlmoco;

                            Liquido = (Extras > Incompletas ? Extras - Incompletas : (Incompletas > Extras ? Incompletas - Extras : 0));

                            #region Conferencia de valores
                            Extras1 = ExtraEntrada + ExtraSaida + ExtraSaidaAlmoco + ExtraVoltaAlmoco;
                            Incompletas1 = IncompletaEntrada + IncompletaSaida + IncompletaSaidaAlmoco + IncompletaVoltaAlmoco;
                            Liquido1 = (Extras1 > Incompletas1 ? Extras1 - Incompletas1 : (Incompletas1 > Extras1 ? Incompletas1 - Extras1 : 0));

                            if (Liquido1 >= 0 && Extras1 >= 0 && Incompletas1 >= 0)
                            {
                                if (_data.DayOfWeek == DayOfWeek.Saturday || _data.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    ExtrasReal = ExtrasReal + Extras1;
                                    IncompletasReal = IncompletasReal + Incompletas1;
                                }
                                else
                                {
                                    if (Extras1 > Incompletas1)
                                        ExtrasReal = ExtrasReal + Liquido1;
                                    else
                                    {
                                        if (Incompletas1 > Extras1)
                                            IncompletasReal = IncompletasReal + Liquido1;
                                    }
                                }
                            }

                            //_bancoHoras = new BancoDeHoras();

                            //_bancoHoras.IdColaborador = col.IdColaborador;
                            //_bancoHoras.NomeColaborador = _nome;
                            //_bancoHoras.Data = _data;
                            //_bancoHoras.Existe = true;
                            //_bancoHoras.TotalExtras = DateTime.MinValue.AddMinutes(Extras1).ToShortTimeString();
                            //_bancoHoras.TotalIncompletas = DateTime.MinValue.AddMinutes(Incompletas1).ToShortTimeString();
                            //_bancoHoras.TotalLiquido = DateTime.MinValue.AddMinutes(Liquido1).ToShortTimeString();
                            //_bancoHoras.Periodo = Inicio.ToShortDateString() + " até " + Fim.ToShortDateString();
                            //_bancoHoras.Tipo = _data.ToShortDateString() + " " + (Extras1 > Incompletas1 ? "Extras" : (Incompletas1 > Extras1 ? "Incompletas" : ""));
                            //_horasCalculado.Add(_bancoHoras);
                            #endregion
                        }

                    }
                    catch { }
                    _data = _data.AddDays(1);
                }

                if (_nome != "")
                {
                    _bancoHoras = new BancoDeHoras();

                    _bancoHoras.IdColaborador = col.IdColaborador;
                    _bancoHoras.NomeColaborador = _nome;
                    _bancoHoras.Data = _horas.Max(m => m.Data);
                    _bancoHoras.Existe = true;
                    _bancoHoras.TotalExtras = DateTime.MinValue.AddMinutes(ExtrasReal).ToShortTimeString();
                    _bancoHoras.TotalIncompletas = DateTime.MinValue.AddMinutes(IncompletasReal).ToShortTimeString();
                    _bancoHoras.Periodo = Inicio.ToShortDateString() + " até " + Fim.ToShortDateString();
                    Liquido = (ExtrasReal > IncompletasReal ? ExtrasReal - IncompletasReal : (IncompletasReal > ExtrasReal ? IncompletasReal - ExtrasReal : 0));

                    _bancoHoras.TotalLiquido = DateTime.MinValue.AddMinutes(Liquido).ToShortTimeString();

                    _bancoHoras.Tipo = (Extras > Incompletas ? "Extras" : (Incompletas > Extras ? "Incompletas" : ""));
                }

                if (_bancoHoras.Existe)
                    _horasCalculado.Add(_bancoHoras);

            }

            return _horasCalculado;
        }
    }
}
