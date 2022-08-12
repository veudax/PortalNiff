using Classes;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using System.Xml.Serialization;


namespace Servico_Suportte
{
    public partial class AutomatizacaoSigomGlobus : Form
    {
        public AutomatizacaoSigomGlobus()
        {
            InitializeComponent();

            string ArquivoConexao = "PortalNiffConexao" + ".ini";
            string linhaConexao = "";

            StreamReader writer = null;

            if (Environment.MachineName.ToUpper().Contains("CORPTS") || Environment.MachineName.ToUpper().Contains("CORPRDP"))
                writer = new StreamReader(Path.GetDirectoryName(Application.ExecutablePath) + ArquivoConexao);
            else
            {
                if (Path.GetDirectoryName(Application.ExecutablePath).ToUpper().StartsWith("G:"))
                    writer = new StreamReader(Path.GetDirectoryName(Application.ExecutablePath) + ArquivoConexao);
                else
                    writer = new StreamReader(Publicas._caminhoPortal + ArquivoConexao);
            }

            linhaConexao = writer.ReadLine();
            writer.Close();

            if (linhaConexao != "")
                Publicas._conexaoString = linhaConexao;
        }

        #region atributos

        Classes.Empresa _empresa;
        List<Classes.Empresa> _listaEmpresas;
        List<Classes.Usuario> _listaUsuarios;
        List<Classes.ParametrosArquivei> _listaParametrosArquivei = new List<ParametrosArquivei>();
        List<Classes.Arquivei> _listaArquivei = new List<Arquivei>();
        List<Classes.ItensArquivei> _listaItensArquivei = new List<ItensArquivei>();
        List<Classes.ItensParametrosArquivei> _listaItensParametros = new List<ItensParametrosArquivei>();
        List<ArquivosEmpresaArquivei> _arquivosPorEmpresa = new List<ArquivosEmpresaArquivei>();
        List<Classes.ImportandoArquivei> _listaArquivosImportados;

        List<string> _xml = new List<string>();
        List<int> _empresaProcessada8h = new List<int>();
        List<int> _empresaProcessada10h = new List<int>();
        List<int> _empresaProcessada12h = new List<int>();
        List<int> _empresaProcessada14h = new List<int>();
        List<int> _empresaProcessada16h = new List<int>();
        List<int> _empresaProcessada18h = new List<int>();

        List<int> _empresaProcessada8hEmitidas = new List<int>();
        List<int> _empresaProcessada10hEmitidas = new List<int>();
        List<int> _empresaProcessada12hEmitidas = new List<int>();
        List<int> _empresaProcessada14hEmitidas = new List<int>();
        List<int> _empresaProcessada16hEmitidas = new List<int>();
        List<int> _empresaProcessada18hEmitidas = new List<int>();

        List<int> _empresaProcessadaDacte8h = new List<int>();
        List<int> _empresaProcessadaDacte10h = new List<int>();
        List<int> _empresaProcessadaDacte12h = new List<int>();
        List<int> _empresaProcessadaDacte14h = new List<int>();
        List<int> _empresaProcessadaDacte16h = new List<int>();
        List<int> _empresaProcessadaDacte18h = new List<int>();
        List<int> _empresaProcessadaNFSe8h = new List<int>();
        List<int> _empresaProcessadaNFSe10h = new List<int>();
        List<int> _empresaProcessadaNFSe12h = new List<int>();
        List<int> _empresaProcessadaNFSe14h = new List<int>();
        List<int> _empresaProcessadaNFSe16h = new List<int>();
        List<int> _empresaProcessadaNFSe18h = new List<int>();
        
        List<int> _empresaProcessadaNFSeGinfes8h = new List<int>();
        List<int> _empresaProcessadaNFSeGinfes10h = new List<int>();
        List<int> _empresaProcessadaNFSeGinfes12h = new List<int>();
        List<int> _empresaProcessadaNFSeGinfes14h = new List<int>();
        List<int> _empresaProcessadaNFSeGinfes16h = new List<int>();
        List<int> _empresaProcessadaNFSeGinfes18h = new List<int>();

        List<int> _empresaProcessadaNFSeCampinas8h = new List<int>();
        List<int> _empresaProcessadaNFSeCampinas10h = new List<int>();
        List<int> _empresaProcessadaNFSeCampinas12h = new List<int>();
        List<int> _empresaProcessadaNFSeCampinas14h = new List<int>();
        List<int> _empresaProcessadaNFSeCampinas16h = new List<int>();
        List<int> _empresaProcessadaNFSeCampinas18h = new List<int>();

        List<int> _empresaProcessadaNFSeTaubate8h = new List<int>();
        List<int> _empresaProcessadaNFSeTaubate10h = new List<int>();
        List<int> _empresaProcessadaNFSeTaubate12h = new List<int>();
        List<int> _empresaProcessadaNFSeTaubate14h = new List<int>();
        List<int> _empresaProcessadaNFSeTaubate16h = new List<int>();
        List<int> _empresaProcessadaNFSeTaubate18h = new List<int>();

        List<int> _empresaProcessadaNFSeGuarulhosExcel8h = new List<int>();
        List<int> _empresaProcessadaNFSeGuarulhosExcel10h = new List<int>();
        List<int> _empresaProcessadaNFSeGuarulhosExcel12h = new List<int>();
        List<int> _empresaProcessadaNFSeGuarulhosExcel14h = new List<int>();
        List<int> _empresaProcessadaNFSeGuarulhosExcel16h = new List<int>();
        List<int> _empresaProcessadaNFSeGuarulhosExcel18h = new List<int>();

        List<int> _empresaProcessadaNFSeRibeiraoExcel8h = new List<int>();
        List<int> _empresaProcessadaNFSeRibeiraoExcel10h = new List<int>();
        List<int> _empresaProcessadaNFSeRibeiraoExcel12h = new List<int>();
        List<int> _empresaProcessadaNFSeRibeiraoExcel14h = new List<int>();
        List<int> _empresaProcessadaNFSeRibeiraoExcel16h = new List<int>();
        List<int> _empresaProcessadaNFSeRibeiraoExcel18h = new List<int>();

        List<string> NFNovas = new List<string>();
        List<string> NFCanceladas = new List<string>();
        List<string> NFErros = new List<string>();
        List<string> CartaCorrecao = new List<string>();

        bool _arquiveiJaRenomeado = false;
        int quantidadeNFCadastradas = 0;
        int quantidadeProcessadoNovos = 0;
        int quantidadeComErroLeitura = 0;
        int quantidadesCanceladas = 0;
        int quantidadeCartaCorrecao = 0;

        string _pastaDiaria = "";
        string nomeArquivo = "";
        string _diretorio = "";
        #endregion

        private class ArquivosEmpresaArquivei
        {
            public int IdEmpresa { get; set; }
            public string Empresa { get; set; }
            public string NomeArquivo { get; set; }
            public string Acao { get; set; }
            public string DiretorioConfigurado { get; set; }
        }

        private void AutomatizacaoSigomGlobus_Load(object sender, EventArgs e)
        {
            Classes.Publicas.stringConexao = Classes.Publicas._conexaoString;
            Classes.Publicas.stringConexaoProdata = Classes.Publicas._conexaoStringProdataABC;
            Classes.Publicas.stringConexaoSigom = Classes.Publicas._conexaoStringSigom;
                       
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _listaEmpresas = new EmpresaBO().Listar(false);
            
            try
            {
                _listaUsuarios = new UsuarioBO().ListarUsuarios(true);
            }
            catch
            {
            }

            #region deve ser copiado no CorpBI01 Pasta C:\PortalNiff\Servico Gerado executável separado do excel. 
            /* copiar os arquivos abaixo que ficam na pasta c:\Sistemas\PortalNiff\Servico_Suportte\bin\Debug\
               Servico_Suportte.exe
               Servico_Suportte.pdb
               Negocio.dll
               Negocio.pdb
               Dados.dll
               Dados.pdb
               Classes.dll
               Classes.pdb

               Se houver atualização de componetes copiar todos os demais arquivos pela data. 
            */

            /*
            Sigom();

            Prodata();
            
            Arquivei();
            
            ArquiveiDacte();

            XmlNotaServicoSP();

            XmlNotaServicoGinfes();

            XmlNotaServicoCampinas();

            XmlNotaServicoTaubate();
            
           
            */
            #endregion

            #region Deve ser copiado no CorpAp01 Pasta d:\PortalNIFF\Servico\ Gerar executável separado dos itens acima
            /* copiar os arquivos abaixo que ficam na pasta c:\Sistemas\PortalNiff\Servico_Suportte\bin\Debug\
               Servico_Suportte.exe
               Servico_Suportte.pdb
               Negocio.dll
               Negocio.pdb
               Dados.dll
               Dados.pdb
               Classes.dll
               Classes.pdb

               Se houver atualização de componetes copiar todos os demais arquivos pela data. 

              a maquina precisa ter excel instalado. 
            */
            
            
            NFSeGuarulhos_Excel();

            NFSeRibeirao_Excel();
            

            #endregion


        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            NF_Xml_Emitidas();

            timer2.Stop();
        }

        private void Sigom()
        {
            DateTime _data = DateTime.Now.Date.AddDays(-5);
            //DateTime _data = Convert.ToDateTime("10/08/2020"); 
            DateTime _tempoExecucao;

            if (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(7).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(8).TimeOfDay)
            {
                return;
            }
            
            #region Sigom
            //while (_data <= Convert.ToDateTime("12/08/2020"))
            {

                foreach (var item in _listaEmpresas//.Where(w => w.IdEmpresa == 3)
                    .Where(w => w.IdEmpresa == 2 || w.IdEmpresa == 3 || /*w.IdEmpresa == 7 ||*/ w.IdEmpresa == 10)
                                                   .OrderBy(o => o.IdEmpresa))
                {
                    List<Classes.SIGOM> _lista = new List<Classes.SIGOM>();
                    _tempoExecucao = DateTime.Now;
                    _lista = new SIGOMBO().ListarResumo(item.IdEmpresa, _data, _data, "S");

                    if (_lista.Count() != 0)
                        continue;

                    Classes.Log _log = new Classes.Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Iniciou processo de comparação do SIGOM x GLOBUS para a empresa " + item.NomeAbreviado + " para a data " + _data.ToShortDateString();
                    _log.Tela = "Automação SIGOMxGLOBUS";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    int empresa = (item.IdEmpresa == 2 || item.IdEmpresa == 3 ? 3 :
                                (item.IdEmpresa == 10 ? 5 :
                                (item.IdEmpresa == 7 ? 26 : 0)));

                    List<Classes.SIGOM> _listaGlobus = new SIGOMBO().ListarGlobus(item.CodigoEmpresaGlobus, _data, _data);
                    List<Classes.SIGOM> _listaSigom = new SIGOMBO().ListarSigon(empresa, _data, _data);

                    foreach (var itemGlobus in _listaGlobus.Where(w => !w.Lido).OrderBy(o => o.CodigoImportacaoGlobus))
                    {
                        if (itemGlobus.TipoPagtoGlobus.ToUpper().StartsWith("INT"))
                        {
                            foreach (var itemSigom in _listaSigom.Where(w => !w.Lido &&
                                                                        w.CodigoLinha == itemGlobus.CodigoLinha &&
                                                                        w.Prefixo == itemGlobus.Prefixo &&
                                                                        w.TipoPagtoSigom.ToUpper() == "INTEGRACAO" &&
                                                                        w.IdTipoPagtoSigom == itemGlobus.CodigoImportacaoGlobus &&
                                                                        (w.InicioJornada == itemGlobus.InicioJornada || w.InicioJornadaSigom == itemGlobus.InicioJornada ||
                                                                        (w.InicioJornada >= itemGlobus.InicioJornada && w.InicioJornada <= itemGlobus.InicioJornada.AddMinutes(30)) ||
                                                                        (w.InicioJornadaSigom >= itemGlobus.InicioJornada && w.InicioJornadaSigom <= itemGlobus.InicioJornada.AddMinutes(30)) ||
                                                                        (itemGlobus.InicioJornada >= w.InicioJornada && itemGlobus.InicioJornada <= w.InicioJornada.AddMinutes(30)) ||
                                                                        (itemGlobus.InicioJornada >= w.InicioJornadaSigom && itemGlobus.InicioJornada <= w.InicioJornadaSigom.AddMinutes(30))
                                                                        ))
                                                                 .OrderBy(o => o.IdTipoPagtoGlobus))
                            {
                                _lista.Add(new Classes.SIGOM()
                                {
                                    CodigoLinha = itemGlobus.CodigoLinha,
                                    Prefixo = itemGlobus.Prefixo,
                                    InicioJornada = itemGlobus.InicioJornada,
                                    InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                                    FimJornadaSigom = itemSigom.FimJornadaSigom,
                                    FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                                    MotoristaSigom = itemSigom.MotoristaSigom,
                                    MotoristaGlobus = itemGlobus.MotoristaGlobus,
                                    IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                                    IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                                    TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                                    TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                                    QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                                    QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                                    ValorSigom = itemSigom.ValorSigom,
                                    ValorGlobus = itemGlobus.ValorGlobus,
                                    GuiaGlobus = itemGlobus.GuiaGlobus,
                                    CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                                    DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemSigom.QuantidadeGirosSigom,
                                    DiferencaValor = itemGlobus.ValorGlobus != itemSigom.ValorSigom,
                                    Tipo = "S"
                                });

                                itemGlobus.Lido = true;
                                itemSigom.Lido = true;

                            }
                        }
                        else
                        {
                            foreach (var itemSigom in _listaSigom.Where(w => !w.Lido &&
                                                                        w.CodigoLinha == itemGlobus.CodigoLinha &&
                                                                        w.Prefixo == itemGlobus.Prefixo &&
                                                                        w.IdTipoPagtoSigom == itemGlobus.CodigoImportacaoGlobus &&
                                                                        w.TipoPagtoSigom.ToUpper() != "INTEGRACAO" &&
                                                                        (w.InicioJornada == itemGlobus.InicioJornada || w.InicioJornadaSigom == itemGlobus.InicioJornada ||
                                                                        (w.InicioJornada >= itemGlobus.InicioJornada && w.InicioJornada <= itemGlobus.InicioJornada.AddMinutes(30)) ||
                                                                        (w.InicioJornadaSigom >= itemGlobus.InicioJornada && w.InicioJornadaSigom <= itemGlobus.InicioJornada.AddMinutes(30)) ||
                                                                        (itemGlobus.InicioJornada >= w.InicioJornada && itemGlobus.InicioJornada <= w.InicioJornada.AddMinutes(30)) ||
                                                                        (itemGlobus.InicioJornada >= w.InicioJornadaSigom && itemGlobus.InicioJornada <= w.InicioJornadaSigom.AddMinutes(30))
                                                                        ))
                                                                 .OrderBy(o => o.IdTipoPagtoGlobus))
                            {
                                _lista.Add(new Classes.SIGOM()
                                {
                                    CodigoLinha = itemGlobus.CodigoLinha,
                                    Prefixo = itemGlobus.Prefixo,
                                    InicioJornada = itemGlobus.InicioJornada,
                                    InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                                    FimJornadaSigom = itemSigom.FimJornadaSigom,
                                    FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                                    MotoristaSigom = itemSigom.MotoristaSigom,
                                    MotoristaGlobus = itemGlobus.MotoristaGlobus,
                                    IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                                    IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                                    TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                                    TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                                    QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                                    QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                                    ValorSigom = itemSigom.ValorSigom,
                                    ValorGlobus = itemGlobus.ValorGlobus,
                                    GuiaGlobus = itemGlobus.GuiaGlobus,
                                    CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                                    DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemSigom.QuantidadeGirosSigom,
                                    DiferencaValor = itemGlobus.ValorGlobus != itemSigom.ValorSigom,
                                    Tipo = "S"
                                });

                                itemGlobus.Lido = true;
                                itemSigom.Lido = true;
                            }
                        }

                    }

                    foreach (var itemGlobus in _listaGlobus.Where(w => !w.Lido).OrderBy(o => o.CodigoImportacaoGlobus))
                    {
                        if (itemGlobus.TipoPagtoGlobus.ToUpper().StartsWith("INT"))
                        {
                            foreach (var itemSigom in _listaSigom.Where(w => !w.Lido &&
                                                                        w.CodigoLinha == itemGlobus.CodigoLinha &&
                                                                        w.Prefixo == itemGlobus.Prefixo &&
                                                                        w.TipoPagtoSigom.ToUpper() == "INTEGRACAO" &&
                                                                        w.IdTipoPagtoSigom == itemGlobus.CodigoImportacaoGlobus &&
                                                                        w.QuantidadeGirosSigom == itemGlobus.QuantidadeGirosGlobus
                                                                        )
                                                                 .OrderBy(o => o.IdTipoPagtoGlobus))
                            {
                                _lista.Add(new Classes.SIGOM()
                                {
                                    CodigoLinha = itemGlobus.CodigoLinha,
                                    Prefixo = itemGlobus.Prefixo,
                                    InicioJornada = itemGlobus.InicioJornada,
                                    InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                                    FimJornadaSigom = itemSigom.FimJornadaSigom,
                                    FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                                    MotoristaSigom = itemSigom.MotoristaSigom,
                                    MotoristaGlobus = itemGlobus.MotoristaGlobus,
                                    IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                                    IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                                    TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                                    TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                                    QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                                    QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                                    ValorSigom = itemSigom.ValorSigom,
                                    ValorGlobus = itemGlobus.ValorGlobus,
                                    GuiaGlobus = itemGlobus.GuiaGlobus,
                                    CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                                    DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemSigom.QuantidadeGirosSigom,
                                    DiferencaValor = itemGlobus.ValorGlobus != itemSigom.ValorSigom,
                                    Tipo = "S"
                                });

                                itemGlobus.Lido = true;
                                itemSigom.Lido = true;
                            }
                        }
                        else
                        {
                            foreach (var itemSigom in _listaSigom.Where(w => !w.Lido &&
                                                                        w.CodigoLinha == itemGlobus.CodigoLinha &&
                                                                        w.Prefixo == itemGlobus.Prefixo &&
                                                                        w.IdTipoPagtoSigom == itemGlobus.CodigoImportacaoGlobus &&
                                                                        w.TipoPagtoSigom.ToUpper() != "INTEGRACAO" &&
                                                                        w.QuantidadeGirosSigom == itemGlobus.QuantidadeGirosGlobus
                                                                        )
                                                                 .OrderBy(o => o.IdTipoPagtoGlobus))
                            {
                                _lista.Add(new Classes.SIGOM()
                                {
                                    CodigoLinha = itemGlobus.CodigoLinha,
                                    Prefixo = itemGlobus.Prefixo,
                                    InicioJornada = itemGlobus.InicioJornada,
                                    InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                                    FimJornadaSigom = itemSigom.FimJornadaSigom,
                                    FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                                    MotoristaSigom = itemSigom.MotoristaSigom,
                                    MotoristaGlobus = itemGlobus.MotoristaGlobus,
                                    IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                                    IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                                    TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                                    TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                                    QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                                    QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                                    ValorSigom = itemSigom.ValorSigom,
                                    ValorGlobus = itemGlobus.ValorGlobus,
                                    GuiaGlobus = itemGlobus.GuiaGlobus,
                                    CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                                    DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemSigom.QuantidadeGirosSigom,
                                    DiferencaValor = itemGlobus.ValorGlobus != itemSigom.ValorSigom,
                                    Tipo = "S"
                                });

                                itemGlobus.Lido = true;
                                itemSigom.Lido = true;
                            }
                        }
                    }

                    foreach (var itemGlobus in _listaGlobus.Where(w => !w.Lido).OrderBy(o => o.CodigoImportacaoGlobus))
                    {
                        if (itemGlobus.TipoPagtoGlobus.ToUpper().StartsWith("INT"))
                        {
                            foreach (var itemSigom in _listaSigom.Where(w => !w.Lido &&
                                                                        w.CodigoLinha == itemGlobus.CodigoLinha &&
                                                                        w.Prefixo == itemGlobus.Prefixo &&
                                                                        w.TipoPagtoSigom.ToUpper() == "INTEGRACAO" &&
                                                                        w.IdTipoPagtoSigom == itemGlobus.CodigoImportacaoGlobus 
                                                                        )
                                                                 .OrderBy(o => o.IdTipoPagtoGlobus))
                            {
                                _lista.Add(new Classes.SIGOM()
                                {
                                    CodigoLinha = itemGlobus.CodigoLinha,
                                    Prefixo = itemGlobus.Prefixo,
                                    InicioJornada = itemGlobus.InicioJornada,
                                    InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                                    FimJornadaSigom = itemSigom.FimJornadaSigom,
                                    FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                                    MotoristaSigom = itemSigom.MotoristaSigom,
                                    MotoristaGlobus = itemGlobus.MotoristaGlobus,
                                    IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                                    IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                                    TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                                    TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                                    QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                                    QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                                    ValorSigom = itemSigom.ValorSigom,
                                    ValorGlobus = itemGlobus.ValorGlobus,
                                    GuiaGlobus = itemGlobus.GuiaGlobus,
                                    CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                                    DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemSigom.QuantidadeGirosSigom,
                                    DiferencaValor = itemGlobus.ValorGlobus != itemSigom.ValorSigom,
                                    Tipo = "S"
                                });

                                itemGlobus.Lido = true;
                                itemSigom.Lido = true;
                            }
                        }
                        else
                        {
                            foreach (var itemSigom in _listaSigom.Where(w => !w.Lido &&
                                                                        w.CodigoLinha == itemGlobus.CodigoLinha &&
                                                                        w.Prefixo == itemGlobus.Prefixo &&
                                                                        w.IdTipoPagtoSigom == itemGlobus.CodigoImportacaoGlobus &&
                                                                        w.TipoPagtoSigom.ToUpper() != "INTEGRACAO" 
                                                                        )
                                                                 .OrderBy(o => o.IdTipoPagtoGlobus))
                            {
                                _lista.Add(new Classes.SIGOM()
                                {
                                    CodigoLinha = itemGlobus.CodigoLinha,
                                    Prefixo = itemGlobus.Prefixo,
                                    InicioJornada = itemGlobus.InicioJornada,
                                    InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                                    FimJornadaSigom = itemSigom.FimJornadaSigom,
                                    FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                                    MotoristaSigom = itemSigom.MotoristaSigom,
                                    MotoristaGlobus = itemGlobus.MotoristaGlobus,
                                    IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                                    IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                                    TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                                    TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                                    QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                                    QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                                    ValorSigom = itemSigom.ValorSigom,
                                    ValorGlobus = itemGlobus.ValorGlobus,
                                    GuiaGlobus = itemGlobus.GuiaGlobus,
                                    CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                                    DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemSigom.QuantidadeGirosSigom,
                                    DiferencaValor = itemGlobus.ValorGlobus != itemSigom.ValorSigom,
                                    Tipo = "S"
                                });

                                itemGlobus.Lido = true;
                                itemSigom.Lido = true;
                            }
                        }
                    }

                    foreach (var itemGlobus in _listaGlobus.Where(w => !w.Lido).OrderBy(o => o.CodigoImportacaoGlobus))
                    {
                        _lista.Add(new Classes.SIGOM()
                        {
                            CodigoLinha = itemGlobus.CodigoLinha,
                            Prefixo = itemGlobus.Prefixo,
                            InicioJornada = itemGlobus.InicioJornada,
                            FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                            MotoristaGlobus = itemGlobus.MotoristaGlobus,
                            IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                            TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                            QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                            ValorGlobus = itemGlobus.ValorGlobus,
                            GuiaGlobus = itemGlobus.GuiaGlobus,
                            CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                            DiferencaValor = true,
                            DiferencaQuantidade = true,
                            Tipo = "S"
                        });
                    }

                    foreach (var itemSigom in _listaSigom.Where(w => !w.Lido))
                    {
                        Classes.Linha _linha = new LinhaBO().Consultar(itemSigom.CodigoLinha, true);
                        Classes.VeiculosGlobus _veic = new VeiculosGlobusBO().Consultar(item.CodigoEmpresaGlobus, itemSigom.Prefixo);

                        if (_linha.Empresa == item.CodigoEmpresaGlobus && _veic.Empresa == item.CodigoEmpresaGlobus)
                        {
                            _lista.Add(new Classes.SIGOM()
                            {
                                CodigoLinha = itemSigom.CodigoLinha,
                                Prefixo = itemSigom.Prefixo,
                                InicioJornada = itemSigom.InicioJornada,
                                InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                                FimJornadaSigom = itemSigom.FimJornadaSigom,
                                MotoristaSigom = itemSigom.MotoristaSigom,
                                IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                                TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                                QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                                ValorSigom = itemSigom.ValorSigom,
                                DiferencaValor = itemSigom.ValorSigom > 0,
                                DiferencaQuantidade = itemSigom.QuantidadeGirosSigom > 0,
                                Tipo = "S"
                            });
                        }
                    }

                    _lista.ForEach(u => u.IdEmpresa = item.IdEmpresa);

                    new SIGOMBO().Gravar(_lista);

                    foreach (var itemL in _lista.Where(w => w.TipoPagtoGlobus == null))
                    {
                        itemL.TipoPagtoGlobus = " ";
                    }
                    decimal _valorPaganteLinhaNoGlobus = 0;
                    decimal _valorPaganteLinhaNoSigom = 0;
                    decimal _valorPaganteVeiculoNoGlobus = 0;
                    decimal _valorPaganteVeiculoNoSigom = 0;

                    decimal _valorLinhaNoGlobus = 0;
                    decimal _valorLinhaNoSigom = 0;
                    decimal _valorVeiculoNoGlobus = 0;
                    decimal _valorVeiculoNoSigom = 0;
                    decimal _valorTipoPagtoNoGlobus = 0;
                    decimal _valorTipoPagtoNoSigom = 0;

                    int _qtdeLinhaNoGlobus = 0;
                    int _qtdeLinhaNoSigom = 0;
                    int _qtdeVeiculoNoGlobus = 0;
                    int _qtdeVeiculoNoSigom = 0;
                    int _qtdeTipoPagtoNoGlobus = 0;
                    int _qtdeTipoPagtoNoSigom = 0;

                    string textoEmail = "";
                    bool _temDiferenca = false;

                    foreach (var itemL in _lista.GroupBy(g => new { g.CodigoLinha })
                                           .Select(s => new
                                           {
                                               CodigoLinha = s.Key.CodigoLinha,
                                           })
                                           .OrderBy(o => o.CodigoLinha))
                    {

                        _valorPaganteLinhaNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.TipoPagtoGlobus.ToLower().Contains("pagant")).Sum(s => s.ValorGlobus);
                        _valorPaganteLinhaNoSigom = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.IdTipoPagtoSigom == 0).Sum(s => s.ValorSigom);
                        _valorLinhaNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha).Sum(s => s.ValorGlobus);
                        _valorLinhaNoSigom = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha).Sum(s => s.ValorSigom);

                        _qtdeLinhaNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha).Sum(s => s.QuantidadeGirosGlobus);
                        _qtdeLinhaNoSigom = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha).Sum(s => s.QuantidadeGirosSigom);

                        if (_qtdeLinhaNoGlobus != _qtdeLinhaNoSigom || _valorPaganteLinhaNoGlobus != _valorPaganteLinhaNoSigom || _valorLinhaNoGlobus != _valorLinhaNoSigom)
                        {
                            _temDiferenca = true;
                            
                            #region Modelo 2
                            textoEmail = textoEmail + "<tr><td><table style='width: 100%; border: 1px solid #cccccc; border-collapse: collapse;' cellpadding='0' cellspacing='0'>" +
                              "<tr><td></br>" +
                                "<p> <font style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 14px'/><b>Linha</b></font>&nbsp;<b>" + itemL.CodigoLinha + "</b></p>" +
                                "</td></tr>" +
                                "<tr><td>" +
                                "<table style='width:100%;''font-family:Century Gothic'; font-size:8pt;' cellpadding='0' cellspacing='0'>" +
                                    "<tr><td style='width:100%; font-size: 14px;' align='Left' text-align:'Center'><font color='4169e1'>" +
                                        "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' 'font-family:Century Gothic';font-size:10pt' cellpadding='0' cellspacing='0'>" +
                                            "<tr>" +
                                                "<td style='width: 33%; font-size: 14px;' align='Center'><font color='4169e1'; ><b>Quantidade</b></font></td>" +
                                                "<td style='width: 33%; font-size: 14px;' align='center'><font color='4169e1'; ><b>Valor (Só Dinheiro)</b></font></td>" +
                                                "<td style='width: 33%; font-size: 14px;' align='center'><font color='4169e1'; ><b>Valor (Todos) </b></font></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</td></tr>" +
                                    "<tr>" +
                                        "<td style='width:100%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' 'font-family:'Century Gothic';font-size:10pt' cellpadding='0' cellspacing='0'>" +
                                                "<tr>" +
                                                    "<td style='width: 33%; font-size: 14px;' align='Left' text-align:'Center'><font color='4169e1'></font>" +
                                                        "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                            "<tr>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Sigom</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Globus</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Diferença</font></td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                    "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                        "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                            "<tr>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Sigom</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Globus</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Diferença</font></td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                    "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                        "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                            "<tr>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Sigom</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Globus</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Diferença</font></td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                "</tr>" +
                                            "</table>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style='width:100%; font-size: 14px;' align='right'>" +
                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' 'font-family:'Century Gothic';font-size:10pt' cellpadding='0' cellspacing='0'>" +
                                                "<tr>" +
                                                     "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                        "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                            "<tr>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + _qtdeLinhaNoSigom + "</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + _qtdeLinhaNoGlobus + "</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + (_qtdeLinhaNoSigom- _qtdeLinhaNoGlobus) + "</font></td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                     "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                        "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                            "<tr>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorPaganteLinhaNoSigom) + "</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorPaganteLinhaNoGlobus) + "</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", (_valorPaganteLinhaNoSigom - _valorPaganteLinhaNoGlobus)) + "</font></td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                    "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                        "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                            "<tr>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorLinhaNoSigom) + "</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorLinhaNoGlobus) + "</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", (_valorLinhaNoSigom- _valorLinhaNoGlobus)) + "</font></td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                "</tr>" +
                                            "</table>" +
                                        "</td>" +
                                    "</tr>" +
                                "</table>" +
                                "</td></tr>";
                            #endregion

                            foreach (var itemV in _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha)
                                          .GroupBy(g => new { g.Prefixo })
                                          .Select(s => new
                                          {
                                              Prefixo = s.Key.Prefixo,
                                          })
                                          .OrderBy(o => o.Prefixo))
                            {
                                _valorPaganteVeiculoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo && w.TipoPagtoGlobus.ToLower().Contains("pagant")).Sum(s => s.ValorGlobus);
                                _valorPaganteVeiculoNoSigom = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo && w.IdTipoPagtoSigom == 0).Sum(s => s.ValorSigom);

                                _valorVeiculoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo).Sum(s => s.ValorGlobus);
                                _valorVeiculoNoSigom = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo).Sum(s => s.ValorSigom);

                                _qtdeVeiculoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo).Sum(s => s.QuantidadeGirosGlobus);
                                _qtdeVeiculoNoSigom = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo).Sum(s => s.QuantidadeGirosSigom);

                                if (_valorVeiculoNoGlobus != _valorVeiculoNoSigom || _valorPaganteVeiculoNoGlobus != _valorPaganteVeiculoNoSigom || _qtdeVeiculoNoGlobus != _qtdeVeiculoNoSigom )
                                {
                                    textoEmail = textoEmail + "<tr><td>" +
                                        "<p> <font style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 14px'/>" +
                                        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Prefixo</b></font><b>&nbsp;&nbsp;" + itemV.Prefixo + "</b></p>" +
                                        "</td></tr>" +
                                        "<tr><td>" +
                                        "<table style='width:100%;''font-family:Century Gothic'; font-size:8pt;' cellpadding='0' cellspacing='0'>" +
                                    
                                            "<tr>" +
                                                "<td style='width:100%; font-size: 14px;' align='right'>" +
                                                    "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' 'font-family:'Century Gothic';font-size:10pt' cellpadding='0' cellspacing='0'>" +
                                                        "<tr>" +
                                                             "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                                    "<tr>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + _qtdeVeiculoNoSigom + "</font></td>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + _qtdeVeiculoNoGlobus + "</font></td>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + (_qtdeVeiculoNoSigom - _qtdeVeiculoNoGlobus) + "</font></td>" +
                                                                    "</tr>" +
                                                                "</table>" +
                                                            "</td>" +
                                                            "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                                    "<tr>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorPaganteVeiculoNoSigom) + "</font></td>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorPaganteVeiculoNoGlobus) + "</font></td>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", (_valorPaganteVeiculoNoSigom - _valorPaganteVeiculoNoGlobus)) + "</font></td>" +
                                                                    "</tr>" +
                                                                "</table>" +
                                                            "</td>" +
                                                            "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                                    "<tr>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorVeiculoNoSigom) + "</font></td>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorVeiculoNoGlobus) + "</font></td>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", (_valorVeiculoNoSigom - _valorVeiculoNoGlobus)) + "</font></td>" +
                                                                    "</tr>" +
                                                                "</table>" +
                                                            "</td>" +
                                                        "</tr>" +
                                                    "</table>" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</td></tr>";

                                    bool _imprimiuCabecalho = false;

                                    // Tem globus
                                    foreach (var itemT in _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo && w.TipoPagtoGlobus != " ")
                                          .GroupBy(g => new { g.TipoPagtoGlobus })
                                          .Select(s => new
                                          {
                                              TipoPagtoGlobus = s.Key.TipoPagtoGlobus
                                          })
                                          .OrderBy(o => o.TipoPagtoGlobus))
                                    {
                                        
                                        _valorTipoPagtoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo && 
                                                            w.TipoPagtoGlobus == itemT.TipoPagtoGlobus).Sum(s => s.ValorGlobus);
                                        _valorTipoPagtoNoSigom = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo && 
                                                            w.TipoPagtoGlobus == itemT.TipoPagtoGlobus).Sum(s => s.ValorSigom);

                                        _qtdeTipoPagtoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo && 
                                                            w.TipoPagtoGlobus == itemT.TipoPagtoGlobus).Sum(s => s.QuantidadeGirosGlobus);
                                        _qtdeTipoPagtoNoSigom = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo && 
                                                            w.TipoPagtoGlobus == itemT.TipoPagtoGlobus).Sum(s => s.QuantidadeGirosSigom);

                                        if (_valorTipoPagtoNoSigom != _valorTipoPagtoNoGlobus || _qtdeTipoPagtoNoGlobus != _qtdeTipoPagtoNoSigom)
                                        {
                                            if (!_imprimiuCabecalho)
                                            {
                                                textoEmail = textoEmail + "<tr><td align='center'>" +
                                                      "<p> <font style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 14px'/>" +
                                                      "<b>Tipo de Pagamento com Diferenças</b></font></p>" +
                                                      "</td></tr>";

                                                _imprimiuCabecalho = true;

                                                textoEmail = textoEmail + "<tr><td>" +
			                                        "<table style='width:100%;' font-family:CenturyGothic';font-size:8pt'; cellpadding='0' cellspacing='0'>" +
			                                        "<tr>" +
				                                        "<td style='width:100%; font-size: 12px;' align='Left'><font color='4169e1'>" +
					                                        "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' font-family:CenturyGothic; font-size:10pt' cellpadding='0' cellspacing='0'>" +
						                                        "<tr>" +
							                                        "<td style='width: 40%; font-size: 13px;' align='right'><font color='4169e1';><b>Tipo</b></font></td>" +
							                                        "<td style='width: 30%; font-size: 13px;' align='Center'><font color='4169e1';><b>Quantidade</b></font></td>" +
							                                        "<td style='width: 30%; font-size: 13px;' align='center'><font color='4169e1';><b>Valor (Todos) </b></font></td>" + 
						                                        "</tr>" +
					                                        "</table>" +
				                                        "</td>" +
			                                        "</tr>" +
			                                        "<tr>" +
				                                        "<td style='width:100%; font-size: 12px;' align='Left'><font color='4169e1'>" +
					                                        "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' font-family:'CenturyGothic';font-size:'10pt' cellpadding='0' cellspacing='0'>" + 
						                                        "<tr>" +
                                                                    "<td style='width: 40%; font-size: 12px;' align='right'><font color='4169e1'><b>&nbsp;&nbsp;</b></font></td>" +
							                                        "<td style='width: 30%; font-size: 12px;' align='Left' text-align:'Center'><font color='4169e1'></font>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic; font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                           "<tr>" +
                                                                              "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>"+ "Sigom".PadLeft(10,' ') + "</font></td>" +
                                                                              "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>"+ "Globus".PadLeft(10, ' ') + "</font></td>" +
                                                                              "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>"+ "Diferença".PadLeft(10, ' ') + "</font></td>" +
                                                                           "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
							                                        "<td style='width:30%; font-size: 12px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic; font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                            "<tr>" +
                                                                                "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>Sigom</font></td>" +
                                                                                "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>Globus</font></td>" +
                                                                                "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>Diferença</font></td>" +
                                                                            "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
						                                        "</tr>" +
					                                        "</table>" +
				                                        "</td>" +
                                                    "</tr>";
                                            }

                                            textoEmail = textoEmail+ "<tr>" +
                                                        "<td style='width:100%; font-size: 12px;' align='Left'><font color='4169e1'>" +
                                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' font-family:'CenturyGothic';font-size:'10pt' cellpadding='0' cellspacing='0'>" +
                                                                "<tr>" +
                                                                    "<td style='width: 40%; font-size: 12px;' align='right'><font color='4169e1'><b>" + 
                                                                    itemT.TipoPagtoGlobus
                                                                     + "</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 12px;' align='Left' text-align:'Center'><font color='4169e1'></font>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic;font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                           "<tr>" +
                                                                              "<td style='width: 33%; font-size: 12px;' align='right'>"+ _qtdeTipoPagtoNoSigom.ToString().PadLeft(10,' ') +"</td>" +
                                                                              "<td style='width: 33%; font-size: 12px;' align='right'>"+ _qtdeTipoPagtoNoGlobus.ToString().PadLeft(10, ' ') + "</td>" +
                                                                              "<td style='width: 33%; font-size: 12px;' align='right'>"+ (_qtdeTipoPagtoNoSigom-_qtdeTipoPagtoNoGlobus).ToString().PadLeft(10, ' ') + "</td>" +
                                                                           "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                    "<td style='width:30%; font-size: 12px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic;font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                            "<tr>" +
                                                                                "<td style='width: 33%; font-size: 12px;' align='right'>" + String.Format("{0,12:N2}",_valorTipoPagtoNoSigom) + "</td>" +
                                                                                "<td style='width: 33%; font-size: 12px;' align='right'>" + String.Format("{0,12:N2}",_valorTipoPagtoNoGlobus) + "</td>" +
                                                                                "<td style='width: 33%; font-size: 12px;' align='right'>" + String.Format("{0,12:N2}", (_valorTipoPagtoNoSigom - _valorTipoPagtoNoGlobus)) + "</td>" +
                                                                            "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                "</tr>" +
                                                            "</table>" +
                                                        "</td>" +
                                                    "</tr>";
                                        }
                                    }

                                    // Não tem Globus Só Sigom
                                    foreach (var itemT in _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo && w.TipoPagtoGlobus == " ")
                                          .GroupBy(g => new { g.TipoPagtoSigom })
                                          .Select(s => new
                                          {
                                              TipoPagtoSigom = s.Key.TipoPagtoSigom
                                          })
                                          .OrderBy(o => o.TipoPagtoSigom))
                                    {

                                        _valorTipoPagtoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo &&
                                                            w.TipoPagtoSigom == itemT.TipoPagtoSigom).Sum(s => s.ValorGlobus);
                                        _valorTipoPagtoNoSigom = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo &&
                                                            w.TipoPagtoSigom == itemT.TipoPagtoSigom).Sum(s => s.ValorSigom);

                                        _qtdeTipoPagtoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo &&
                                                            w.TipoPagtoSigom == itemT.TipoPagtoSigom).Sum(s => s.QuantidadeGirosGlobus);
                                        _qtdeTipoPagtoNoSigom = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo &&
                                                            w.TipoPagtoSigom == itemT.TipoPagtoSigom).Sum(s => s.QuantidadeGirosSigom);

                                        if (_valorTipoPagtoNoSigom != _valorTipoPagtoNoGlobus || _qtdeTipoPagtoNoGlobus != _qtdeTipoPagtoNoSigom)
                                        {
                                            if (!_imprimiuCabecalho)
                                            {
                                                textoEmail = textoEmail + "<tr><td align='center'>" +
                                                      "<p> <font style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 14px'/>" +
                                                      "<b>Tipo de Pagamento com Diferenças</b></font></p>" +
                                                      "</td></tr>";

                                                _imprimiuCabecalho = true;

                                                textoEmail = textoEmail + "<tr><td>" +
                                                    "<table style='width:100%;' font-family:CenturyGothic';font-size:8pt'; cellpadding='0' cellspacing='0'>" +
                                                    "<tr>" +
                                                        "<td style='width:100%; font-size: 12px;' align='Left'><font color='4169e1'>" +
                                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' font-family:CenturyGothic; font-size:10pt' cellpadding='0' cellspacing='0'>" +
                                                                "<tr>" +
                                                                    "<td style='width: 40%; font-size: 13px;' align='right'><font color='4169e1';><b>Tipo</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 13px;' align='Center'><font color='4169e1';><b>Quantidade</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 13px;' align='center'><font color='4169e1';><b>Valor (Todos) </b></font></td>" +
                                                                "</tr>" +
                                                            "</table>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                    "<tr>" +
                                                        "<td style='width:100%; font-size: 12px;' align='Left'><font color='4169e1'>" +
                                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' font-family:'CenturyGothic';font-size:'10pt' cellpadding='0' cellspacing='0'>" +
                                                                "<tr>" +
                                                                    "<td style='width: 40%; font-size: 12px;' align='right'><font color='4169e1'><b>&nbsp;&nbsp;</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 12px;' align='Left' text-align:'Center'><font color='4169e1'></font>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic; font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                           "<tr>" +
                                                                              "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>" + "Sigom".PadLeft(10, ' ') + "</font></td>" +
                                                                              "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>" + "Globus".PadLeft(10, ' ') + "</font></td>" +
                                                                              "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>" + "Diferença".PadLeft(10, ' ') + "</font></td>" +
                                                                           "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                    "<td style='width:30%; font-size: 12px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic; font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                            "<tr>" +
                                                                                "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>Sigom</font></td>" +
                                                                                "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>Globus</font></td>" +
                                                                                "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>Diferença</font></td>" +
                                                                            "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                "</tr>" +
                                                            "</table>" +
                                                        "</td>" +
                                                    "</tr>";
                                            }

                                            textoEmail = textoEmail + "<tr>" +
                                                        "<td style='width:100%; font-size: 12px;' align='Left'><font color='4169e1'>" +
                                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' font-family:'CenturyGothic';font-size:'10pt' cellpadding='0' cellspacing='0'>" +
                                                                "<tr>" +
                                                                    "<td style='width: 40%; font-size: 12px;' align='right'><font color='4169e1'><b>" +
                                                                    itemT.TipoPagtoSigom 
                                                                     + "</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 12px;' align='Left' text-align:'Center'><font color='4169e1'></font>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic;font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                           "<tr>" +
                                                                              "<td style='width: 33%; font-size: 12px;' align='right'>" + _qtdeTipoPagtoNoSigom.ToString().PadLeft(10, ' ') + "</td>" +
                                                                              "<td style='width: 33%; font-size: 12px;' align='right'>" + _qtdeTipoPagtoNoGlobus.ToString().PadLeft(10, ' ') + "</td>" +
                                                                              "<td style='width: 33%; font-size: 12px;' align='right'>" + (_qtdeTipoPagtoNoSigom - _qtdeTipoPagtoNoGlobus).ToString().PadLeft(10, ' ') + "</td>" +
                                                                           "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                    "<td style='width:30%; font-size: 12px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic;font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                            "<tr>" +
                                                                                "<td style='width: 33%; font-size: 12px;' align='right'>" + String.Format("{0,12:N2}", _valorTipoPagtoNoSigom) + "</td>" +
                                                                                "<td style='width: 33%; font-size: 12px;' align='right'>" + String.Format("{0,12:N2}", _valorTipoPagtoNoGlobus) + "</td>" +
                                                                                "<td style='width: 33%; font-size: 12px;' align='right'>" + String.Format("{0,12:N2}", (_valorTipoPagtoNoSigom - _valorTipoPagtoNoGlobus)) + "</td>" +
                                                                            "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                "</tr>" +
                                                            "</table>" +
                                                        "</td>" +
                                                    "</tr>";
                                        }
                                    }
                                }
                            }

                            textoEmail = textoEmail + "</tr> </table> </td>  </table> </td> </tr> ";
                        }
                    }
                    

                    if (_temDiferenca)
                    {
                        string[] _dadosEmail = new string[50];
                        _dadosEmail[0] = textoEmail;
                        _dadosEmail[1] = _data.ToShortDateString();
                        _dadosEmail[2] = item.CodigoEmpresaGlobus + " " + item.NomeAbreviado;
                        _dadosEmail[3] = "Sigom";

                        string emailDestino = "";// "mdmunoz@niff.com.br; ";

                        foreach (var itemU in _listaUsuarios.Where(w => w.AcessaRecebedoria && w.RecebeEmailDasDiferencasDoSigonProdata))
                        {
                            emailDestino = emailDestino + itemU.Email + "; ";
                        }

                        Classes.Publicas.EnviarEmailSigom(_dadosEmail, emailDestino, "Diferenças Globus x Sigom");
                       
                    }

                    _log = new Classes.Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Finalizou processo de comparação do SIGOM x GLOBUS para a empresa " + item.NomeAbreviado
                         + " para a data " + _data.ToShortDateString() +
                        " demorou " + DateTime.Now.Subtract(_tempoExecucao).Minutes.ToString() + " minutos " +
                        (_temDiferenca ? " existem " : " não existem ") + "diferenças";
                    _log.Tela = "Automação SIGOMxGLOBUS";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                }
            
                _data = _data.AddDays(1);
            }
            #endregion
            //Close();
        }

        private void Prodata()
        {
            //DateTime _data = Convert.ToDateTime("10/08/2020");
            DateTime _data = DateTime.Now.Date.AddDays(-5);
            DateTime _tempoExecucao;

            if (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(6).AddMinutes(30).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(7).TimeOfDay)
            {
                return;
            }

            //while (_data <= Convert.ToDateTime("12/08/2020"))
            {

                foreach (var item in _listaEmpresas.Where(w => w.IdEmpresa == 4) // ABC
                                                   .OrderBy(o => o.IdEmpresa))
                {
                    List<Classes.SIGOM> _lista = new List<Classes.SIGOM>();
                    _tempoExecucao = DateTime.Now;
                    _lista = new SIGOMBO().ListarResumo(item.IdEmpresa, _data, _data, "P");

                    if (_lista.Count() != 0)
                        continue;

                    Classes.Log _log = new Classes.Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Iniciou processo de comparação do PRODATA x GLOBUS para a empresa " + item.NomeAbreviado + " para a data " + _data.ToShortDateString();
                    _log.Tela = "Automação PRODATAxGLOBUS";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    int empresa = 4;

                    List<Classes.SIGOM> _listaGlobus = new ProdataBO().ListarGlobus(item.CodigoEmpresaGlobus, _data, _data);
                    List<Classes.SIGOM> _listaSigom = new ProdataBO().ListarProdata(empresa, _data, _data);
                    List<Classes.SIGOM> _listaTipos = new ProdataBO().ListarTipos(item.CodigoEmpresaGlobus);

                    bool encontrou = false;
                    foreach (var itemGlobus in _listaGlobus.Where(w => !w.Lido).OrderBy(o => o.IdTipoPagtoGlobus)
                                                                               .OrderBy(o => o.InicioJornada)
                                                                               .OrderBy(o => o.Prefixo)
                                                                               .OrderBy(o => o.CodigoLinha))
                    {
                        encontrou = false;

                        foreach (var itemT in _listaTipos.Where(w => w.IdTipoPagtoGlobus == itemGlobus.IdTipoPagtoGlobus).OrderBy(o => o.TipoPagtoGlobus))
                        {

                            foreach (var itemSigom in _listaSigom.Where(w => !w.Lido &&
                                                                            w.CodigoLinha.Contains(itemGlobus.CodigoLinha) &&
                                                                            w.Prefixo.PadLeft(7, '0') == itemGlobus.Prefixo &&
                                                                            itemT.TipoPagtoGlobus == w.IdTipoPagtoSigom.ToString() &&
                                                                            w.ValorTarifa == itemGlobus.ValorTarifa &&
                                                                            (w.InicioJornada == itemGlobus.InicioJornada || w.InicioJornadaSigom == itemGlobus.InicioJornada ||
                                                                            (w.InicioJornada >= itemGlobus.InicioJornada && w.InicioJornada <= itemGlobus.InicioJornada.AddMinutes(30)) ||
                                                                            (w.InicioJornadaSigom >= itemGlobus.InicioJornada && w.InicioJornadaSigom <= itemGlobus.InicioJornada.AddMinutes(30)) ||
                                                                            (itemGlobus.InicioJornada >= w.InicioJornada && itemGlobus.InicioJornada <= w.InicioJornada.AddMinutes(30)) ||                                                                     
                                                                            (itemGlobus.InicioJornada >= w.InicioJornadaSigom && itemGlobus.InicioJornada <= w.InicioJornadaSigom.AddMinutes(30))
                                                                            ))
                                                                  .OrderBy(o => o.IdTipoPagtoSigom)
                                                                  .OrderBy(o => o.InicioJornada)
                                                                  .OrderBy(o => o.Prefixo)
                                                                  .OrderBy(o => o.CodigoLinha))
                            {
                                _lista.Add(new Classes.SIGOM()
                                {
                                    CodigoLinha = itemGlobus.CodigoLinha,
                                    Prefixo = itemGlobus.Prefixo,
                                    InicioJornada = itemGlobus.InicioJornada,
                                    InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                                    FimJornadaSigom = itemSigom.FimJornadaSigom,
                                    FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                                    MotoristaSigom = itemSigom.MotoristaSigom,
                                    MotoristaGlobus = itemGlobus.MotoristaGlobus,
                                    IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                                    IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                                    TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                                    TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                                    QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                                    QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                                    ValorSigom = itemSigom.ValorSigom,
                                    ValorGlobus = itemGlobus.ValorGlobus,
                                    GuiaGlobus = itemGlobus.GuiaGlobus,
                                    CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                                    DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemSigom.QuantidadeGirosSigom,
                                    DiferencaValor = itemGlobus.ValorGlobus != itemSigom.ValorSigom,
                                    Tipo = "P"
                                });

                                encontrou = true;
                                itemSigom.Lido = true;

                                if (itemSigom.QuantidadeGirosSigom == itemGlobus.QuantidadeGirosGlobus && itemSigom.ValorSigom == itemGlobus.ValorGlobus)
                                    break;
                            }

                            if (encontrou)
                            {
                                itemGlobus.Lido = true;
                                break;
                            }

                        }

                        /*if (!itemGlobus.Lido)
                        {
                            _lista.Add(new Classes.SIGOM()
                            {
                                CodigoLinha = itemGlobus.CodigoLinha,
                                Prefixo = itemGlobus.Prefixo,
                                InicioJornada = itemGlobus.InicioJornada,
                                FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                                MotoristaGlobus = itemGlobus.MotoristaGlobus,
                                IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                                TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                                QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                                ValorGlobus = itemGlobus.ValorGlobus,
                                GuiaGlobus = itemGlobus.GuiaGlobus,
                                CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                                DiferencaValor = true,
                                DiferencaQuantidade = true,
                                Tipo = "P"
                            });
                        }*/

                        foreach (var itemT in _listaTipos.Where(w => w.IdTipoPagtoGlobus == itemGlobus.IdTipoPagtoGlobus).OrderBy(o => o.TipoPagtoGlobus))
                        {

                            foreach (var itemSigom in _listaSigom.Where(w => !w.Lido &&
                                                                            w.CodigoLinha.Contains(itemGlobus.CodigoLinha) &&
                                                                            w.Prefixo.PadLeft(7, '0') == itemGlobus.Prefixo &&
                                                                            itemT.TipoPagtoGlobus == w.IdTipoPagtoSigom.ToString() &&
                                                                            w.ValorTarifa == itemGlobus.ValorTarifa &&
                                                                            w.QuantidadeGirosSigom == itemGlobus.QuantidadeGirosGlobus
                                                                            )
                                                                  .OrderBy(o => o.IdTipoPagtoSigom)
                                                                  .OrderBy(o => o.InicioJornada)
                                                                  .OrderBy(o => o.Prefixo)
                                                                  .OrderBy(o => o.CodigoLinha))
                            {
                                _lista.Add(new Classes.SIGOM()
                                {
                                    CodigoLinha = itemGlobus.CodigoLinha,
                                    Prefixo = itemGlobus.Prefixo,
                                    InicioJornada = itemGlobus.InicioJornada,
                                    InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                                    FimJornadaSigom = itemSigom.FimJornadaSigom,
                                    FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                                    MotoristaSigom = itemSigom.MotoristaSigom,
                                    MotoristaGlobus = itemGlobus.MotoristaGlobus,
                                    IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                                    IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                                    TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                                    TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                                    QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                                    QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                                    ValorSigom = itemSigom.ValorSigom,
                                    ValorGlobus = itemGlobus.ValorGlobus,
                                    GuiaGlobus = itemGlobus.GuiaGlobus,
                                    CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                                    DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemSigom.QuantidadeGirosSigom,
                                    DiferencaValor = itemGlobus.ValorGlobus != itemSigom.ValorSigom,
                                    Tipo = "P"
                                });

                                encontrou = true;
                                itemSigom.Lido = true;

                                if (itemSigom.QuantidadeGirosSigom == itemGlobus.QuantidadeGirosGlobus && itemSigom.ValorSigom == itemGlobus.ValorGlobus)
                                    break;
                            }

                            if (encontrou)
                            {
                                itemGlobus.Lido = true;
                                break;
                            }

                        }

                        foreach (var itemT in _listaTipos.Where(w => w.IdTipoPagtoGlobus == itemGlobus.IdTipoPagtoGlobus).OrderBy(o => o.TipoPagtoGlobus))
                        {

                            foreach (var itemSigom in _listaSigom.Where(w => !w.Lido &&
                                                                            w.CodigoLinha.Contains(itemGlobus.CodigoLinha) &&
                                                                            w.Prefixo.PadLeft(7, '0') == itemGlobus.Prefixo &&
                                                                            itemT.TipoPagtoGlobus == w.IdTipoPagtoSigom.ToString() &&
                                                                            w.ValorTarifa == itemGlobus.ValorTarifa
                                                                            )
                                                                  .OrderBy(o => o.IdTipoPagtoSigom)
                                                                  .OrderBy(o => o.InicioJornada)
                                                                  .OrderBy(o => o.Prefixo)
                                                                  .OrderBy(o => o.CodigoLinha))
                            {
                                _lista.Add(new Classes.SIGOM()
                                {
                                    CodigoLinha = itemGlobus.CodigoLinha,
                                    Prefixo = itemGlobus.Prefixo,
                                    InicioJornada = itemGlobus.InicioJornada,
                                    InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                                    FimJornadaSigom = itemSigom.FimJornadaSigom,
                                    FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                                    MotoristaSigom = itemSigom.MotoristaSigom,
                                    MotoristaGlobus = itemGlobus.MotoristaGlobus,
                                    IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                                    IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                                    TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                                    TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                                    QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                                    QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                                    ValorSigom = itemSigom.ValorSigom,
                                    ValorGlobus = itemGlobus.ValorGlobus,
                                    GuiaGlobus = itemGlobus.GuiaGlobus,
                                    CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                                    DiferencaQuantidade = itemGlobus.QuantidadeGirosGlobus != itemSigom.QuantidadeGirosSigom,
                                    DiferencaValor = itemGlobus.ValorGlobus != itemSigom.ValorSigom,
                                    Tipo = "P"
                                });

                                encontrou = true;
                                itemSigom.Lido = true;

                                if (itemSigom.QuantidadeGirosSigom == itemGlobus.QuantidadeGirosGlobus && itemSigom.ValorSigom == itemGlobus.ValorGlobus)
                                    break;
                            }

                            if (encontrou)
                            {
                                itemGlobus.Lido = true;
                                break;
                            }
                        }
                    }
                                       
                    foreach (var itemGlobus in _listaGlobus.Where(w => !w.Lido).OrderBy(o => o.CodigoImportacaoGlobus))
                    {
                        _lista.Add(new Classes.SIGOM()
                        {
                            CodigoLinha = itemGlobus.CodigoLinha,
                            Prefixo = itemGlobus.Prefixo,
                            InicioJornada = itemGlobus.InicioJornada,
                            FimJornadaGlobus = itemGlobus.FimJornadaGlobus,
                            MotoristaGlobus = itemGlobus.MotoristaGlobus,
                            IdTipoPagtoGlobus = itemGlobus.IdTipoPagtoGlobus,
                            TipoPagtoGlobus = itemGlobus.TipoPagtoGlobus,
                            QuantidadeGirosGlobus = itemGlobus.QuantidadeGirosGlobus,
                            ValorGlobus = itemGlobus.ValorGlobus,
                            GuiaGlobus = itemGlobus.GuiaGlobus,
                            CodigoImportacaoGlobus = itemGlobus.CodigoImportacaoGlobus,
                            DiferencaValor = true,
                            DiferencaQuantidade = true,
                            Tipo = "P"
                        });
                    }

                    foreach (var itemSigom in _listaSigom.Where(w => !w.Lido))
                    {
                        Classes.Linha _linha = new LinhaBO().Consultar(item.CodigoEmpresaGlobus, itemSigom.CodigoLinha.Replace("L","").Replace("l", "").Trim());

                        if (_linha.Empresa == item.CodigoEmpresaGlobus)
                        {
                            _lista.Add(new Classes.SIGOM()
                            {
                                CodigoLinha = itemSigom.CodigoLinha.Replace("L", "").Replace("l", "").Trim(),
                                Prefixo = itemSigom.Prefixo.PadLeft(7, '0'),
                                InicioJornada = itemSigom.InicioJornada,
                                InicioJornadaSigom = itemSigom.InicioJornadaSigom,
                                FimJornadaSigom = itemSigom.FimJornadaSigom,
                                MotoristaSigom = itemSigom.MotoristaSigom,
                                IdTipoPagtoSigom = itemSigom.IdTipoPagtoSigom,
                                TipoPagtoSigom = itemSigom.TipoPagtoSigom,
                                QuantidadeGirosSigom = itemSigom.QuantidadeGirosSigom,
                                ValorSigom = itemSigom.ValorSigom,
                                DiferencaValor = true,
                                DiferencaQuantidade = true,
                                Tipo = "P"
                            });
                        }
                    }

                    _lista.ForEach(u => u.IdEmpresa = item.IdEmpresa);

                    new SIGOMBO().Gravar(_lista);

                    foreach (var itemL in _lista.Where(w => w.TipoPagtoGlobus == null))
                    {
                        itemL.TipoPagtoGlobus = " ";
                    }
                    decimal _valorPaganteLinhaNoGlobus = 0;
                    decimal _valorPaganteLinhaNoProdata = 0;
                    decimal _valorPaganteVeiculoNoGlobus = 0;
                    decimal _valorPaganteVeiculoNoProdata = 0;

                    decimal _valorLinhaNoGlobus = 0;
                    decimal _valorLinhaNoProdata = 0;
                    decimal _valorVeiculoNoGlobus = 0;
                    decimal _valorVeiculoNoProdata = 0;
                    decimal _valorTipoPagtoNoGlobus = 0;
                    decimal _valorTipoPagtoNoProdata = 0;

                    int _qtdeLinhaNoGlobus = 0;
                    int _qtdeLinhaNoProdata = 0;
                    int _qtdeVeiculoNoGlobus = 0;
                    int _qtdeVeiculoNoProdata = 0;
                    int _qtdeTipoPagtoNoGlobus = 0;
                    int _qtdeTipoPagtoNoProdata = 0;

                    string textoEmail = "";
                    bool _temDiferenca = false;

                    foreach (var itemL in _lista.GroupBy(g => new { g.CodigoLinha })
                                           .Select(s => new
                                           {
                                               CodigoLinha = s.Key.CodigoLinha,
                                           })
                                           .OrderBy(o => o.CodigoLinha))
                    {

                        _valorPaganteLinhaNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.TipoPagtoGlobus.ToLower().Contains("liquido")).Sum(s => s.ValorGlobus);
                        _valorPaganteLinhaNoProdata = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.IdTipoPagtoSigom == 0).Sum(s => s.ValorSigom);
                        _valorLinhaNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha).Sum(s => s.ValorGlobus);
                        _valorLinhaNoProdata = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha).Sum(s => s.ValorSigom);

                        _qtdeLinhaNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha).Sum(s => s.QuantidadeGirosGlobus);
                        _qtdeLinhaNoProdata = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha).Sum(s => s.QuantidadeGirosSigom);

                        if (_qtdeLinhaNoGlobus != _qtdeLinhaNoProdata || _valorLinhaNoGlobus != _valorLinhaNoProdata )
                        {
                            _temDiferenca = true;
                            #region Modelo 1
                            //textoEmail = textoEmail + "<tr><td><table style='width: 100%; border: 1px solid #cccccc; border-collapse: collapse;' cellpadding='0' cellspacing='0'>" +
                            //  "<tr><td>" +
                            //    "<p> <font style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 14px'/><b>Linha</b></font>&nbsp;<b>" + itemL.CodigoLinha + "</b></p>" +
                            //    "</td></tr>" +
                            //    "<tr><td>" +
                            //    "<table style='width:100%;''font-family:Century Gothic'; font-size:8pt;' cellpadding='0' cellspacing='0'>" +
                            //        "<tr><td style='width:100%; font-size: 14px;' align='Left' text-align:'Center'><font color='4169e1'>" +
                            //            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' 'font-family:Century Gothic';font-size:10pt' cellpadding='0' cellspacing='0'>" +
                            //                "<tr>" +
                            //                    "<td style='width: 50%; font-size: 14px;' align='Center'><font color='4169e1'; ><b>Globus</b></font></td>" +
                            //                    "<td style='width: 50%; font-size: 14px;' align='center'><font color='4169e1'; ><b>Prodata</b></font></td>" +
                            //                "</tr>" +
                            //            "</table>" +
                            //        "</td></tr>" +
                            //        "<tr>" +
                            //            "<td style='width:100%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                            //                "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' 'font-family:'Century Gothic';font-size:10pt' cellpadding='0' cellspacing='0'>" +
                            //                    "<tr>" +
                            //                        "<td style='width: 50%; font-size: 14px;' align='Left' text-align:'Center'><font color='4169e1'></font>" +
                            //                            "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                            //                                "<tr>" +
                            //                                    "<td style='width: 50%; font-size: 14px;' align='right'><font color='4169e1'; >Quantidade</font></td>" +
                            //                                    "<td style='width: 50%; font-size: 14px;' align='right'><font color='4169e1'; >Valor</font></td>" +
                            //                                "</tr>" +
                            //                            "</table>" +
                            //                        "</td>" +
                            //                        "<td style='width:50%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                            //                            "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                            //                                "<tr>" +
                            //                                    "<td style='width: 50%; font-size: 14px;' align='right'><font color='4169e1'; >Quantidade</font></td>" +
                            //                                    "<td style='width: 50%; font-size: 14px;' align='right'><font color='4169e1'; >Valor</font></td>" +
                            //                                "</tr>" +
                            //                            "</table>" +
                            //                        "</td>" +
                            //                    "</tr>" +
                            //                "</table>" +
                            //            "</td>" +
                            //        "</tr>" +
                            //        "<tr>" +
                            //            "<td style='width:100%; font-size: 14px;' align='right'>" +
                            //                "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' 'font-family:'Century Gothic';font-size:10pt' cellpadding='0' cellspacing='0'>" +
                            //                    "<tr>" +
                            //                         "<td style='width:50%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                            //                            "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                            //                                "<tr>" +
                            //                                    "<td style='width: 50%; font-size: 14px;' align='right'>" + _qtdeLinhaNoGlobus + "</font></td>" +
                            //                                    "<td style='width: 50%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorLinhaNoGlobus) + "</font></td>" +
                            //                                "</tr>" +
                            //                            "</table>" +
                            //                        "</td>" +
                            //                        "<td style='width:50%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                            //                            "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                            //                                "<tr>" +
                            //                                    "<td style='width: 50%; font-size: 14px;' align='right'>" + _qtdeLinhaNoProdata + "</font></td>" +
                            //                                    "<td style='width: 50%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorLinhaNoProdata) + "</font></td>" +
                            //                                "</tr>" +
                            //                            "</table>" +
                            //                        "</td>" +
                            //                    "</tr>" +
                            //                "</table>" +
                            //            "</td>" +
                            //        "</tr>" +
                            //    "</table>" +
                            //    "</td></tr>";
                            #endregion

                            #region Modelo 2
                            textoEmail = textoEmail + "<tr><td><table style='width: 100%; border: 1px solid #cccccc; border-collapse: collapse;' cellpadding='0' cellspacing='0'>" +
                              "<tr><td> </br>" +
                                "<p> <font style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 14px'/><b>Linha</b></font>&nbsp;<b>" + itemL.CodigoLinha + "</b></p>" +
                                "</td></tr>" +
                                "<tr><td>" +
                                "<table style='width:100%;''font-family:Century Gothic'; font-size:8pt;' cellpadding='0' cellspacing='0'>" +
                                    "<tr><td style='width:100%; font-size: 14px;' align='Left' text-align:'Center'><font color='4169e1'>" +
                                        "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' 'font-family:Century Gothic';font-size:10pt' cellpadding='0' cellspacing='0'>" +
                                            "<tr>" +
                                                "<td style='width: 33%; font-size: 14px;' align='Center'><font color='4169e1'; ><b>Quantidade</b></font></td>" +
                                                "<td style='width: 33%; font-size: 14px;' align='center'><font color='4169e1'; ><b>Valor (Dinheiro)</b></font></td>" +
                                                "<td style='width: 33%; font-size: 14px;' align='center'><font color='4169e1'; ><b>Valor (Todos) </b></font></td>" +
                                            "</tr>" +
                                        "</table>" +
                                    "</td></tr>" +
                                    "<tr>" +
                                        "<td style='width:100%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' 'font-family:'Century Gothic';font-size:10pt' cellpadding='0' cellspacing='0'>" +
                                                "<tr>" +
                                                    "<td style='width: 33%; font-size: 14px;' align='Left' text-align:'Center'><font color='4169e1'></font>" +
                                                        "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                            "<tr>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Prodata</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Globus</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Diferença</font></td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                    "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                        "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                            "<tr>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Prodata</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Globus</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Diferença</font></td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                    "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                        "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                            "<tr>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Prodata</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Globus</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'><font color='4169e1'; >Diferença</font></td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                "</tr>" +
                                            "</table>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style='width:100%; font-size: 14px;' align='right'>" +
                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' 'font-family:'Century Gothic';font-size:10pt' cellpadding='0' cellspacing='0'>" +
                                                "<tr>" +
                                                     "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                        "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                            "<tr>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + _qtdeLinhaNoProdata + "</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + _qtdeLinhaNoGlobus + "</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + (_qtdeLinhaNoProdata - _qtdeLinhaNoGlobus) + "</font></td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                    "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                        "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                            "<tr>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorPaganteLinhaNoProdata) + "</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorPaganteLinhaNoGlobus) + "</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", (_valorPaganteLinhaNoProdata - _valorPaganteLinhaNoGlobus)) + "</font></td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                    "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                        "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                            "<tr>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorLinhaNoProdata) + "</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorLinhaNoGlobus) + "</font></td>" +
                                                                "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", (_valorLinhaNoProdata - _valorLinhaNoGlobus)) + "</font></td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                    "</td>" +
                                                "</tr>" +
                                            "</table>" +
                                        "</td>" +
                                    "</tr>" +
                                "</table>" +
                                "</td></tr>";
                            #endregion

                            foreach (var itemV in _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha)
                                          .GroupBy(g => new { g.Prefixo })
                                          .Select(s => new
                                          {
                                              Prefixo = s.Key.Prefixo,
                                          })
                                          .OrderBy(o => o.Prefixo))
                            {
                                _valorPaganteVeiculoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo && w.TipoPagtoGlobus.ToLower().Contains("liquido")).Sum(s => s.ValorGlobus);
                                _valorPaganteVeiculoNoProdata = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo && w.IdTipoPagtoSigom == 0).Sum(s => s.ValorSigom);

                                _valorVeiculoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo).Sum(s => s.ValorGlobus);
                                _valorVeiculoNoProdata = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo).Sum(s => s.ValorSigom);

                                _qtdeVeiculoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo).Sum(s => s.QuantidadeGirosGlobus);
                                _qtdeVeiculoNoProdata = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo).Sum(s => s.QuantidadeGirosSigom);

                                if (_valorVeiculoNoGlobus != _valorVeiculoNoProdata || _qtdeVeiculoNoGlobus != _qtdeVeiculoNoProdata)
                                {
                                    textoEmail = textoEmail + "<tr><td>" +
                                        "<p> <font style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 14px'/>" +
                                        "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Prefixo</b></font><b>&nbsp;&nbsp;" + itemV.Prefixo + "</b></p>" +
                                        "</td></tr>" +
                                        "<tr><td>" +
                                        "<table style='width:100%;''font-family:Century Gothic'; font-size:8pt;' cellpadding='0' cellspacing='0'>" +

                                            "<tr>" +
                                                "<td style='width:100%; font-size: 14px;' align='right'>" +
                                                    "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' 'font-family:'Century Gothic';font-size:10pt' cellpadding='0' cellspacing='0'>" +
                                                        "<tr>" +
                                                             "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                                    "<tr>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + _qtdeVeiculoNoProdata + "</font></td>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + _qtdeVeiculoNoGlobus + "</font></td>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + (_qtdeVeiculoNoProdata - _qtdeVeiculoNoGlobus) + "</font></td>" +
                                                                    "</tr>" +
                                                                "</table>" +
                                                            "</td>" +
                                                            "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                                    "<tr>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorPaganteVeiculoNoProdata) + "</font></td>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorPaganteVeiculoNoGlobus) + "</font></td>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", (_valorPaganteVeiculoNoProdata - _valorPaganteVeiculoNoGlobus)) + "</font></td>" +
                                                                    "</tr>" +
                                                                "</table>" +
                                                            "</td>" +

                                                            "<td style='width:33%; font-size: 14px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                "<table style='width: 100%;' 'font-family:Century Gothic'; font-size:10pt' border='0' cellpadding='0'cellspacing = '0'>" +
                                                                    "<tr>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorVeiculoNoProdata) + "</font></td>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", _valorVeiculoNoGlobus) + "</font></td>" +
                                                                        "<td style='width: 33%; font-size: 14px;' align='right'>" + String.Format("{0,12:N2}", (_valorVeiculoNoProdata - _valorVeiculoNoGlobus)) + "</font></td>" +
                                                                    "</tr>" +
                                                                "</table>" +
                                                            "</td>" +
                                                        "</tr>" +
                                                    "</table>" +
                                                "</td>" +
                                            "</tr>" +
                                        "</table>" +
                                        "</td></tr>";


                                    bool _imprimiuCabecalho = false;

                                    // Tem globus
                                    foreach (var itemT in _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo && w.TipoPagtoGlobus != " ")
                                          .GroupBy(g => new { g.TipoPagtoGlobus })
                                          .Select(s => new
                                          {
                                              TipoPagtoGlobus = s.Key.TipoPagtoGlobus
                                          })
                                          .OrderBy(o => o.TipoPagtoGlobus))
                                    {

                                        _valorTipoPagtoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo &&
                                                            w.TipoPagtoGlobus == itemT.TipoPagtoGlobus).Sum(s => s.ValorGlobus);
                                        _valorTipoPagtoNoProdata = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo &&
                                                            w.TipoPagtoGlobus == itemT.TipoPagtoGlobus).Sum(s => s.ValorSigom);

                                        _qtdeTipoPagtoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo &&
                                                            w.TipoPagtoGlobus == itemT.TipoPagtoGlobus).Sum(s => s.QuantidadeGirosGlobus);
                                        _qtdeTipoPagtoNoProdata = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo &&
                                                            w.TipoPagtoGlobus == itemT.TipoPagtoGlobus).Sum(s => s.QuantidadeGirosSigom);

                                        if (_valorTipoPagtoNoProdata != _valorTipoPagtoNoGlobus || _qtdeTipoPagtoNoGlobus != _qtdeTipoPagtoNoProdata)
                                        {
                                            if (!_imprimiuCabecalho)
                                            {
                                                textoEmail = textoEmail + "<tr><td align='center'>" +
                                                      "<p> <font style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 14px'/>" +
                                                      "<b>Tipo de Pagamento com Diferenças</b></font></p>" +
                                                      "</td></tr>";

                                                _imprimiuCabecalho = true;

                                                textoEmail = textoEmail + "<tr><td>" +
                                                    "<table style='width:100%;' font-family:CenturyGothic';font-size:8pt'; cellpadding='0' cellspacing='0'>" +
                                                    "<tr>" +
                                                        "<td style='width:100%; font-size: 12px;' align='Left'><font color='4169e1'>" +
                                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' font-family:CenturyGothic; font-size:10pt' cellpadding='0' cellspacing='0'>" +
                                                                "<tr>" +
                                                                    "<td style='width: 40%; font-size: 13px;' align='right'><font color='4169e1';><b>Tipo</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 13px;' align='Center'><font color='4169e1';><b>Quantidade</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 13px;' align='center'><font color='4169e1';><b>Valor (Todos) </b></font></td>" +
                                                                "</tr>" +
                                                            "</table>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                    "<tr>" +
                                                        "<td style='width:100%; font-size: 12px;' align='Left'><font color='4169e1'>" +
                                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' font-family:'CenturyGothic';font-size:'10pt' cellpadding='0' cellspacing='0'>" +
                                                                "<tr>" +
                                                                    "<td style='width: 40%; font-size: 12px;' align='right'><font color='4169e1'><b>&nbsp;&nbsp;</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 12px;' align='Left' text-align:'Center'><font color='4169e1'></font>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic; font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                           "<tr>" +
                                                                              "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>" + "Prodata".PadLeft(10, ' ') + "</font></td>" +
                                                                              "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>" + "Globus".PadLeft(10, ' ') + "</font></td>" +
                                                                              "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>" + "Diferença".PadLeft(10, ' ') + "</font></td>" +
                                                                           "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                    "<td style='width:30%; font-size: 12px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic; font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                            "<tr>" +
                                                                                "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>Prodata</font></td>" +
                                                                                "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>Globus</font></td>" +
                                                                                "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>Diferença</font></td>" +
                                                                            "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                "</tr>" +
                                                            "</table>" +
                                                        "</td>" +
                                                    "</tr>";
                                            }

                                            textoEmail = textoEmail + "<tr>" +
                                                        "<td style='width:100%; font-size: 12px;' align='Left'><font color='4169e1'>" +
                                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' font-family:'CenturyGothic';font-size:'10pt' cellpadding='0' cellspacing='0'>" +
                                                                "<tr>" +
                                                                    "<td style='width: 40%; font-size: 12px;' align='right'><font color='4169e1'><b>" +
                                                                    itemT.TipoPagtoGlobus
                                                                     + "</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 12px;' align='Left' text-align:'Center'><font color='4169e1'></font>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic;font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                           "<tr>" +
                                                                              "<td style='width: 33%; font-size: 12px;' align='right'>" + _qtdeTipoPagtoNoProdata.ToString().PadLeft(10, ' ') + "</td>" +
                                                                              "<td style='width: 33%; font-size: 12px;' align='right'>" + _qtdeTipoPagtoNoGlobus.ToString().PadLeft(10, ' ') + "</td>" +
                                                                              "<td style='width: 33%; font-size: 12px;' align='right'>" + (_qtdeTipoPagtoNoProdata - _qtdeTipoPagtoNoGlobus).ToString().PadLeft(10, ' ') + "</td>" +
                                                                           "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                    "<td style='width:30%; font-size: 12px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic;font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                            "<tr>" +
                                                                                "<td style='width: 33%; font-size: 12px;' align='right'>" + String.Format("{0,12:N2}", _valorTipoPagtoNoProdata) + "</td>" +
                                                                                "<td style='width: 33%; font-size: 12px;' align='right'>" + String.Format("{0,12:N2}", _valorTipoPagtoNoGlobus) + "</td>" +
                                                                                "<td style='width: 33%; font-size: 12px;' align='right'>" + String.Format("{0,12:N2}", (_valorTipoPagtoNoProdata - _valorTipoPagtoNoGlobus)) + "</td>" +
                                                                            "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                "</tr>" +
                                                            "</table>" +
                                                        "</td>" +
                                                    "</tr>";
                                        }
                                    }

                                    // Não tem Globus Só Sigom
                                    foreach (var itemT in _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo && w.TipoPagtoGlobus == " ")
                                          .GroupBy(g => new { g.TipoPagtoSigom })
                                          .Select(s => new
                                          {
                                              TipoPagtoSigom = s.Key.TipoPagtoSigom
                                          })
                                          .OrderBy(o => o.TipoPagtoSigom))
                                    {

                                        _valorTipoPagtoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo &&
                                                            w.TipoPagtoSigom == itemT.TipoPagtoSigom).Sum(s => s.ValorGlobus);
                                        _valorTipoPagtoNoProdata = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo &&
                                                            w.TipoPagtoSigom == itemT.TipoPagtoSigom).Sum(s => s.ValorSigom);

                                        _qtdeTipoPagtoNoGlobus = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo &&
                                                            w.TipoPagtoSigom == itemT.TipoPagtoSigom).Sum(s => s.QuantidadeGirosGlobus);
                                        _qtdeTipoPagtoNoProdata = _lista.Where(w => w.CodigoLinha == itemL.CodigoLinha && w.Prefixo == itemV.Prefixo &&
                                                            w.TipoPagtoSigom == itemT.TipoPagtoSigom).Sum(s => s.QuantidadeGirosSigom);

                                        if (_valorTipoPagtoNoProdata != _valorTipoPagtoNoGlobus || _qtdeTipoPagtoNoGlobus != _qtdeTipoPagtoNoProdata)
                                        {
                                            if (!_imprimiuCabecalho)
                                            {
                                                textoEmail = textoEmail + "<tr><td align='center'>" +
                                                      "<p> <font style = 'color: #4169e1; font-family: Arial, sans-serif; font-size: 14px'/>" +
                                                      "<b>Tipo de Pagamento com Diferenças</b></font></p>" +
                                                      "</td></tr>";

                                                _imprimiuCabecalho = true;

                                                textoEmail = textoEmail + "<tr><td>" +
                                                    "<table style='width:100%;' font-family:CenturyGothic';font-size:8pt'; cellpadding='0' cellspacing='0'>" +
                                                    "<tr>" +
                                                        "<td style='width:100%; font-size: 12px;' align='Left'><font color='4169e1'>" +
                                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' font-family:CenturyGothic; font-size:10pt' cellpadding='0' cellspacing='0'>" +
                                                                "<tr>" +
                                                                    "<td style='width: 40%; font-size: 13px;' align='right'><font color='4169e1';><b>Tipo</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 13px;' align='Center'><font color='4169e1';><b>Quantidade</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 13px;' align='center'><font color='4169e1';><b>Valor (Todos) </b></font></td>" +
                                                                "</tr>" +
                                                            "</table>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                    "<tr>" +
                                                        "<td style='width:100%; font-size: 12px;' align='Left'><font color='4169e1'>" +
                                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' font-family:'CenturyGothic';font-size:'10pt' cellpadding='0' cellspacing='0'>" +
                                                                "<tr>" +
                                                                    "<td style='width: 40%; font-size: 12px;' align='right'><font color='4169e1'><b>&nbsp;&nbsp;</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 12px;' align='Left' text-align:'Center'><font color='4169e1'></font>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic; font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                           "<tr>" +
                                                                              "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>" + "Prodata".PadLeft(10, ' ') + "</font></td>" +
                                                                              "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>" + "Globus".PadLeft(10, ' ') + "</font></td>" +
                                                                              "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>" + "Diferença".PadLeft(10, ' ') + "</font></td>" +
                                                                           "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                    "<td style='width:30%; font-size: 12px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic; font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                            "<tr>" +
                                                                                "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>Prodata</font></td>" +
                                                                                "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>Globus</font></td>" +
                                                                                "<td style='width: 33%; font-size: 13px;' align='right'><font color='4169e1'>Diferença</font></td>" +
                                                                            "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                "</tr>" +
                                                            "</table>" +
                                                        "</td>" +
                                                    "</tr>";
                                            }

                                            textoEmail = textoEmail + "<tr>" +
                                                        "<td style='width:100%; font-size: 12px;' align='Left'><font color='4169e1'>" +
                                                            "<table style='width:100%; border: 1px solid #cccccc; border-collapse: collapse;' font-family:'CenturyGothic';font-size:'10pt' cellpadding='0' cellspacing='0'>" +
                                                                "<tr>" +
                                                                    "<td style='width: 40%; font-size: 12px;' align='right'><font color='4169e1'><b>" +
                                                                    itemT.TipoPagtoSigom
                                                                     + "</b></font></td>" +
                                                                    "<td style='width: 30%; font-size: 12px;' align='Left' text-align:'Center'><font color='4169e1'></font>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic;font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                           "<tr>" +
                                                                              "<td style='width: 33%; font-size: 12px;' align='right'>" + _qtdeTipoPagtoNoProdata.ToString().PadLeft(10, ' ') + "</td>" +
                                                                              "<td style='width: 33%; font-size: 12px;' align='right'>" + _qtdeTipoPagtoNoGlobus.ToString().PadLeft(10, ' ') + "</td>" +
                                                                              "<td style='width: 33%; font-size: 12px;' align='right'>" + (_qtdeTipoPagtoNoProdata - _qtdeTipoPagtoNoGlobus).ToString().PadLeft(10, ' ') + "</td>" +
                                                                           "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                    "<td style='width:30%; font-size: 12px;' align='Left' text-align:'right'><font color='4169e1'>" +
                                                                        "<table style='width: 100%; font-family:CenturyGothic;font-size:10pt' border='0' cellpadding='0' cellspacing='0'>" +
                                                                            "<tr>" +
                                                                                "<td style='width: 33%; font-size: 12px;' align='right'>" + String.Format("{0,12:N2}", _valorTipoPagtoNoProdata) + "</td>" +
                                                                                "<td style='width: 33%; font-size: 12px;' align='right'>" + String.Format("{0,12:N2}", _valorTipoPagtoNoGlobus) + "</td>" +
                                                                                "<td style='width: 33%; font-size: 12px;' align='right'>" + String.Format("{0,12:N2}", (_valorTipoPagtoNoProdata - _valorTipoPagtoNoGlobus)) + "</td>" +
                                                                            "</tr>" +
                                                                        "</table>" +
                                                                    "</td>" +
                                                                "</tr>" +
                                                            "</table>" +
                                                        "</td>" +
                                                    "</tr>";
                                        }
                                    }
                                }
                            }

                            textoEmail = textoEmail + "</tr> </table> </td> </table> </td></tr>";
                        }
                    }

                    if (_temDiferenca)
                    {
                        string[] _dadosEmail = new string[50];
                        _dadosEmail[0] = textoEmail;
                        _dadosEmail[1] = _data.ToShortDateString();
                        _dadosEmail[2] = item.CodigoEmpresaGlobus + " " + item.NomeAbreviado;
                        _dadosEmail[3] = "Prodata";

                        string emailDestino = ""; // "mdmunoz@niff.com.br";

                        foreach (var itemU in _listaUsuarios.Where(w => w.AcessaRecebedoria && w.RecebeEmailDasDiferencasDoSigonProdata))
                        {
                            //emailDestino = emailDestino + itemU.Email + "; ";
                        }

                        Classes.Publicas.EnviarEmailSigom(_dadosEmail, emailDestino, "Diferenças Globus x Prodata");
                    }

                    _log = new Classes.Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Finalizou processo de comparação do PRODATA x GLOBUS para a empresa " + item.NomeAbreviado
                         + " para a data " + _data.ToShortDateString() +
                        " demorou " + DateTime.Now.Subtract(_tempoExecucao).Minutes.ToString() + " minutos " +
                        (_temDiferenca ? " existem " : " não existem ") + "diferenças";
                    _log.Tela = "Automação PRODATAxGLOBUS";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                }
                //Close();

                _data = _data.AddDays(1);
            }
            //Close();
        }

        #region Busca de Diretorios e SubDiretorios 
        private List<string> Diretorio(string inicial)
        {
            List<string> retorno = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(inicial);

            if (dirInfo.Exists)
            {
                DirectoryInfo[] subDir = dirInfo.GetDirectories();

                foreach (var item in subDir)
                {
                    retorno.AddRange(SubDiretorio(item, false));
                }
            }

            return retorno;
        }

        private List<string> DiretorioEnviados(string inicial)
        {
            List<string> retorno = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(inicial);

            if (dirInfo.Exists)
            {
                DirectoryInfo[] subDir = dirInfo.GetDirectories();

                foreach (var item in subDir)
                {
                    retorno.AddRange(SubDiretorioEmitidas(item, false));
                }
            }

            return retorno;
        }

        private List<string> SubDiretorio(DirectoryInfo dirInfo, bool entrouNoDir)
        {
            List<string> retorno = new List<string>();
            DirectoryInfo[] subDir = dirInfo.GetDirectories();
            int _mesAnterior = 0;
            int _anoAnterior = 0;

            if (subDir.Count() == 0)
            {
                FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

                foreach (FileSystemInfo file in files)
                {
                    if (file.Extension.ToLower().Contains("xml") || file.Extension.ToLower().Contains("xls"))
                    {
                        ImportandoArquivei _importandoArquivo = new ArquiveiBO().ConsultarArquivo(file.FullName, Path.GetFileName(file.FullName));

                        if (!_importandoArquivo.Existe) //importando
                            retorno.Add(file.FullName);
                    }
                }
            }

            foreach (var item in subDir)
            {
                if (item.Name == "Enviadas" || item.Name == "Emitidas" || item.Name == "Desconhecida" || item.Name == "Nao Realizada")
                    continue;

                try
                {
                    // busca mês anterior até o dia 20 do mês atual. o mesmo para o Ano.
                    if (DateTime.Now.Date >= Convert.ToDateTime("01/01/" + DateTime.Now.Year) && 
                        DateTime.Now.Date <= Convert.ToDateTime("20/01/" + DateTime.Now.Year))
                    {
                        _mesAnterior = 12;
                        _anoAnterior = DateTime.Now.AddYears(-1).Year;
                    }
                    else
                    {
                        _anoAnterior = DateTime.Now.Year;

                        if (DateTime.Now.Date <= Convert.ToDateTime("20/" + DateTime.Now.Month + "/" + DateTime.Now.Year))
                            _mesAnterior = DateTime.Now.AddMonths(-1).Month;
                        else
                            _mesAnterior = DateTime.Now.Month;
                    }

                    if (Convert.ToInt32(item.Name) == DateTime.Now.Year || // Ano Atual
                        Convert.ToInt32(item.Name) == _anoAnterior || // Ano Anterior
                        Convert.ToInt32(item.Name) == DateTime.Now.Month || // Mes Atual
                        Convert.ToInt32(item.Name) == _mesAnterior) // Mes Anterior
                    {
                        retorno.AddRange( SubDiretorio(item, true) );
                    }
                }
                catch
                {
                    retorno.AddRange( SubDiretorio(item, entrouNoDir) );
                    entrouNoDir = false;
                }
            }
            return retorno;
        }

        private List<string> SubDiretorioEmitidas(DirectoryInfo dirInfo, bool entrouNoDir)
        {
            List<string> retorno = new List<string>();
            DirectoryInfo[] subDir = dirInfo.GetDirectories();
            int _mesAnterior = 0;
            int _anoAnterior = 0;

            if (subDir.Count() == 0)
            {
                FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

                ImportandoArquivei _importandoArquivo = new ImportandoArquivei();

                foreach (FileSystemInfo file in files)
                {
                    if (file.Extension.ToLower().Contains("xml") || file.Extension.ToLower().Contains("xls"))
                    {
                        _importandoArquivo = new ArquiveiBO().ConsultarArquivo(file.FullName, Path.GetFileName(file.FullName));

                        if (!_importandoArquivo.Existe)
                            retorno.Add(file.FullName);
                    }
                }
            }

            foreach (var item in subDir)
            {
                if (item.Name == "Recebidas" || item.Name == "Recebidos" || item.Name == "Recebido" || item.Name == "Desconhecida" || item.Name == "Nao Realizada")
                    continue;
                try
                {
                    // busca mês anterior até o dia 20 do mês atual. o mesmo para o Ano.
                    if (DateTime.Now.Date >= Convert.ToDateTime("01/01/" + DateTime.Now.Year) &&
                        DateTime.Now.Date <= Convert.ToDateTime("20/01/" + DateTime.Now.Year))
                    {
                        _mesAnterior = 12;
                        _anoAnterior = DateTime.Now.AddYears(-1).Year;
                    }
                    else
                    {
                        _anoAnterior = DateTime.Now.Year;

                        if (DateTime.Now.Date <= Convert.ToDateTime("20/" + DateTime.Now.Month + "/" + DateTime.Now.Year))
                            _mesAnterior = DateTime.Now.AddMonths(-1).Month;
                        else
                            _mesAnterior = DateTime.Now.Month;
                    }

                    if (Convert.ToInt32(item.Name) == DateTime.Now.Year || // Ano Atual
                        Convert.ToInt32(item.Name) == _anoAnterior || // Ano Anterior
                        Convert.ToInt32(item.Name) == DateTime.Now.Month || // Mes Atual
                        Convert.ToInt32(item.Name) == _mesAnterior) // Mes Anterior
                    {
                        retorno.AddRange(SubDiretorioEmitidas(item, true));
                    }
                }
                catch
                {
                    retorno.AddRange(SubDiretorioEmitidas(item, entrouNoDir));
                    entrouNoDir = false;
                }
            }
            return retorno;
        }
        #endregion

        #region NFe - por XML 

        #region Notas Recebidas 
        private void Arquivei()
        {
            bool _processo8h = false;
            bool _processo10h = false;
            bool _processo12h = false;
            bool _processo14h = false;
            bool _processo16h = false;
            bool _processo18h = false;


            if (((DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).AddMinutes(30).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(08).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(10).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) && 
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(12).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(14).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(16).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(18).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay)) ||
                (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)) // só de semana
            {
                _empresaProcessada8h.Clear();
                _empresaProcessada10h.Clear();
                _empresaProcessada12h.Clear();
                _empresaProcessada14h.Clear();
                _empresaProcessada16h.Clear();
                _empresaProcessada18h.Clear();
                 return;
            }

            timer2.Start();
            timer2_Tick(this, new EventArgs());

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(8).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                !_processo10h)
            {
                _processo8h = true;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessada10h.Clear();
                _empresaProcessada12h.Clear();
                _empresaProcessada14h.Clear();
                _empresaProcessada16h.Clear();
                _empresaProcessada18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(10).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) && 
                !_processo12h)
            {
                _processo8h = false;
                _processo10h = true;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessada8h.Clear();
                _empresaProcessada12h.Clear();
                _empresaProcessada14h.Clear();
                _empresaProcessada16h.Clear();
                _empresaProcessada18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(12).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                !_processo14h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = true;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessada8h.Clear();
                _empresaProcessada10h.Clear();
                _empresaProcessada14h.Clear();
                _empresaProcessada16h.Clear();
                _empresaProcessada18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(14).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                !_processo16h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = true;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessada8h.Clear();
                _empresaProcessada10h.Clear();
                _empresaProcessada12h.Clear();
                _empresaProcessada16h.Clear();
                _empresaProcessada18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(16).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                !_processo18h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = true;
                _processo18h = false;

                _empresaProcessada8h.Clear();
                _empresaProcessada10h.Clear();
                _empresaProcessada12h.Clear();
                _empresaProcessada14h.Clear();
                _empresaProcessada18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(18).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay) &&
                !_processo8h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = true;

                _empresaProcessada8h.Clear();
                _empresaProcessada10h.Clear();
                _empresaProcessada12h.Clear();
                _empresaProcessada14h.Clear();
                _empresaProcessada16h.Clear();
            }

            _listaParametrosArquivei = new ParametrosArquiveiBO().Listar();
            _listaItensParametros = new ItensParametrosArquiveiBO().Listar(0);
            _arquivosPorEmpresa = new List<ArquivosEmpresaArquivei>();
            _pastaDiaria = DateTime.Now.ToString().Replace("/", "").Replace(" ", "_").Replace(":", "");
            List<string> _listaArquivos = new List<string>();

            DirectoryInfo dirInfo;

            foreach (var itemE in _listaEmpresas)//.Where(w => w.IdEmpresa == 2))
            {
                if (_processo8h)
                {
                    if (_empresaProcessada8h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo10h)
                {
                    if (_empresaProcessada10h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo12h)
                {
                    if (_empresaProcessada12h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo14h)
                {
                    if (_empresaProcessada14h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo16h)
                {
                    if (_empresaProcessada16h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                {
                    if (_empresaProcessada18h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }

                _empresa = new EmpresaBO().Consultar(itemE.IdEmpresa);
                foreach (var item in _listaParametrosArquivei.Where(w => w.IdEmpresa == itemE.IdEmpresa))
                {
                    dirInfo = new DirectoryInfo(item.Diretorio); // new DirectoryInfo(@"n:\controladoria\Contabilidade\EXERCÍCIO 2018\DECLARAÇÕES\ARQUIVEI\00472135000150\NFe\");
                    //dirInfo = new DirectoryInfo(@"c:/ARQUIVEI/35200211495265000124550010000003261050097457.xml");
                    //dirInfo = new DirectoryInfo(@"c:/ARQUIVEI/");

                    if (dirInfo.Exists)
                    {
                        #region Antigo
                        FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

                        if (files.Count() != 0) // configuraçao antiga
                        {
                            foreach (FileSystemInfo file in files)
                            {
                                if (!Path.GetExtension(file.FullName).ToUpper().Equals(".XML") && !Path.GetExtension(file.FullName).ToUpper().Equals(".XLSX"))
                                    continue;

                                if (!file.FullName.ToUpper().Contains("PROCESSADO") &&
                                    !file.FullName.ToUpper().Contains("INVALIDO") &&
                                    !file.FullName.ToUpper().Contains("CRDOWNLOAD") &&
                                    !file.FullName.ToUpper().Contains("NAOIMPORTADO") &&
                                    !file.FullName.Contains("~$") &&
                                    (file.FullName.Contains(".xls") || file.FullName.Contains(".xml")))
                                {
                                    //if (file.FullName.Contains("35191060812088000178550010002050121565130773.xml"))
                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei() { IdEmpresa = itemE.IdEmpresa
                                                                                       , NomeArquivo = file.FullName
                                                                                       , Acao = "R"// nao é mais excluido ou renomeado os arquivos
                                                                                       , Empresa = _empresa.NomeAbreviado
                                                                                       , DiretorioConfigurado = item.Diretorio });
                                }
                            }
                        }
                        #endregion

                        #region Novo

                        _listaArquivos = Diretorio(item.Diretorio);// @"n:\controladoria\Contabilidade\EXERCÍCIO 2018\DECLARAÇÕES\ARQUIVEI\00472135000150\NFe\");

                        foreach (var itemA in _listaArquivos)
                        {
                            if (!Path.GetExtension(itemA).ToUpper().Equals(".XML") && !Path.GetExtension(itemA).ToUpper().Equals(".XLSX"))
                                continue;

                            if (!itemA.ToUpper().Contains("PROCESSADO") &&
                                !itemA.ToUpper().Contains("INVALIDO") &&
                                !itemA.ToUpper().Contains("CRDOWNLOAD") &&
                                !itemA.ToUpper().Contains("NAOIMPORTADO") &&
                                !itemA.Contains("~$") &&
                                (itemA.Contains(".xls") || itemA.Contains(".xml")))
                            {

                                if (_arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.IdEmpresa &&
                                                                   w.NomeArquivo == itemA).Count() == 0)
                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei() { IdEmpresa = itemE.IdEmpresa
                                                                                          , NomeArquivo = itemA
                                                                                          , Acao = "R" // nao é mais excluido ou renomeado os arquivos
                                                                                          , Empresa = _empresa.NomeAbreviado
                                                                                          , DiretorioConfigurado = item.Diretorio
                                    });
                            }
                        }
                        #endregion
                    }
                }


                if (_processo8h)
                    _empresaProcessada8h.Add(itemE.IdEmpresa);
                else
                {
                    if (_processo10h)
                        _empresaProcessada10h.Add(itemE.IdEmpresa);
                    else
                    {
                        if (_processo12h)
                            _empresaProcessada12h.Add(itemE.IdEmpresa);
                        else
                        {
                            if (_processo14h)
                                _empresaProcessada14h.Add(itemE.IdEmpresa);
                            else
                            {
                                if (_processo16h)
                                    _empresaProcessada16h.Add(itemE.IdEmpresa);
                                else
                                    _empresaProcessada18h.Add(itemE.IdEmpresa);
                            }
                        }
                    }
                }
            }

            #region atributos
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            Excel.Range range = null;
            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();

            _listaItensArquivei = new List<ItensArquivei>();
            _listaArquivei = new List<Arquivei>();

            string str = "";
            int rCnt;
            int cCnt;
            int rw = 0;
            int cl = 0;
            int quantidadeArquivos = 0;
            int quantidadeEventos = 0;
            int quantidadesCanceladas = 0;
            int quantidadeCartaCorrecao = 0;

            string cnpjdestinatario = "";
            string iedestinatario = "";
            string enderecodestinatario = "";
            string bairrodestinatario = "";
            string cepdestinatario = "";
            string razaosocialdestinatario = "";
            string cnpjemitente = "";
            string ieemitente = "";
            string razaosocialemitente = "";
            DateTime dataemissao = DateTime.MinValue;
            int numeronf = 0;
            string modelonf = "";
            string serie = "";
            string naturezaoperacao = "";
            decimal valortotalnf = 0;
            decimal valorproduto = 0;
            decimal baseicms = 0;
            string dadosadicionais = "";
            string numeroenddestinatario = "";
            string tipo = "";
            string status = "";
            string operacao = "";
            List<string> NFCanceladas = new List<string>();
            List<string> CartaCorrecao = new List<string>();
            List<string> NFErros = new List<string>();

            #endregion

            #region Leitura/Gravação arquivo excel.
            int idArquivo = 1;
            Arquivei _cadastrado = null;
            ImportandoArquivei _importandoArquivo = null;
            _listaArquivosImportados = new List<ImportandoArquivei>();

            _xml = new List<string>();

            foreach (var itemE in _arquivosPorEmpresa.GroupBy(g => g.IdEmpresa))
            {
                quantidadeArquivos = 0;
                quantidadeEventos = 0;
                quantidadeNFCadastradas = 0;
                quantidadeProcessadoNovos = 0;
                quantidadeComErroLeitura = 0;
                quantidadesCanceladas = 0;
                quantidadeCartaCorrecao = 0;

                foreach (var item in _arquivosPorEmpresa.Where(w => w.NomeArquivo.Contains("xml") &&
                                                                    //w.NomeArquivo.ToUpper().Contains("ID") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                                    !w.NomeArquivo.Contains("~$") &&
                                                                    w.IdEmpresa == itemE.Key).OrderBy(o => o.IdEmpresa))
                {

                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Iniciou importação arquivei NFe da empresa " + item.Empresa;
                    _log.Tela = "Principal - Arquivei";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    _arquiveiJaRenomeado = false;

                    if (!Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XML") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    if (Path.GetFileName(item.NomeArquivo).StartsWith("ID"))
                    {
                        quantidadeArquivos++;
                        quantidadeEventos++;
                        XmlSerializer ser = new XmlSerializer(typeof(TNfeProc));
                        XmlTextReader reader = null;
                        try
                        {
                            reader = new XmlTextReader(item.NomeArquivo);
                        }
                        catch
                        {
                            //try
                            //{
                            //    File.Move(nomeArquivo, Path.GetDirectoryName(item.NomeArquivo) + @"\Processadas\" + _pastaDiaria + @"\" +
                            //        Path.GetFileName(item.NomeArquivo).Replace("xml", "xmlInvalido"));
                            //}
                            //catch { }

                            _log = new Log();
                            _log.IdUsuario = 1;
                            _log.Descricao = "A arquivo " + item.NomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                            _log.Tela = "Principal - Arquivei";

                            item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlInvalido");

                            try
                            {
                                new LogBO().Gravar(_log);
                            }
                            catch { }
                        }

                        string _xmlTag = "";
                        try
                        {
                            while (reader.Read())
                            {
                                switch (reader.NodeType)
                                {
                                    case XmlNodeType.Element:
                                        _xmlTag = _xmlTag + "<" + reader.Name + ">";
                                        break;
                                    case XmlNodeType.Text:
                                        _xmlTag = _xmlTag + reader.Value;
                                        break;
                                    case XmlNodeType.EndElement:
                                        _xmlTag = _xmlTag + "</" + reader.Name + ">";
                                        break;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            //try
                            //{
                            //    File.Move(nomeArquivo, Path.GetDirectoryName(item.NomeArquivo) + @"\Processadas\" + _pastaDiaria + @"\" +
                            //        Path.GetFileName(item.NomeArquivo).Replace("xml", "xmlNaoImportado"));
                            //}
                            //catch { }

                            _log = new Log();
                            _log.IdUsuario = 1;
                            _log.Descricao = "Arquivo " + item.NomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                            _log.Tela = "Principal - Arquivei";

                            try
                            {
                                new LogBO().Gravar(_log);
                            }
                            catch { }

                            item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlNaoImportado");

                        }

                        _xml.Add(_xmlTag);
                        reader.Close();

                        int posIni = _xmlTag.IndexOf("<chNFe>") + 7;
                        int posFim = _xmlTag.IndexOf("</chNFe>");
                        string chave = _xmlTag.Substring(posIni, posFim - posIni).Trim();
                        string evento = "";

                        posIni = _xmlTag.IndexOf("<xEvento>") + 9;
                        posFim = _xmlTag.IndexOf("</xEvento>");

                        try
                        {
                            evento = _xmlTag.Substring(posIni, posFim - posIni).Trim();
                        }
                        catch
                        {
                            posIni = _xmlTag.IndexOf("<descEvento>") + 12;
                            posFim = _xmlTag.IndexOf("</descEvento>");

                            try
                            {
                                evento = _xmlTag.Substring(posIni, posFim - posIni).Trim();
                            }
                            catch
                            {
                                evento = "";
                            }
                        }

                        Classes.Arquivei _arquiveiChaveCancelar = new ArquiveiBO().Consultar(item.IdEmpresa, chave, 0);

                        List<Arquivei> listaCancelar = new List<Arquivei>();
                        List<ItensArquivei> itensCartaCorrecao = new List<ItensArquivei>();

                        if (_arquiveiChaveCancelar.Existe)
                        {
                            if (evento.ToUpper().Contains("CANCEL"))
                            {
                                quantidadesCanceladas++;
                                _arquiveiChaveCancelar.Status = "Cancelada após importação";
                                listaCancelar.Add(_arquiveiChaveCancelar);
                                NFCanceladas.Add(_arquiveiChaveCancelar.NumeroNF.ToString().PadRight(11).Replace(" ", "&nbsp")
                                            + _arquiveiChaveCancelar.DataEmissao.ToShortDateString()
                                            + "&nbsp"
                                            + _arquiveiChaveCancelar.ChaveDeAcesso);
                                new ArquiveiBO().GravarStatus(listaCancelar);
                            }
                            else
                            {
                                quantidadeCartaCorrecao++;
                                CartaCorrecao.Add(_arquiveiChaveCancelar.NumeroNF.ToString().PadRight(11).Replace(" ", "&nbsp")
                                            + _arquiveiChaveCancelar.DataEmissao.ToShortDateString()
                                            + "&nbsp"
                                            + _arquiveiChaveCancelar.ChaveDeAcesso);
                                itensCartaCorrecao = new ArquiveiBO().Listar(_arquiveiChaveCancelar.Id, false);

                                itensCartaCorrecao.ForEach(u => u.CCe = _xmlTag.Substring(_xmlTag.IndexOf("<xCorrecao>") + 11,
                                _xmlTag.IndexOf("</xCorrecao>") - (_xmlTag.IndexOf("<xCorrecao>") + 11)));

                                if (!new ArquiveiBO().Gravar(itensCartaCorrecao))
                                    NFErros.Add(nomeArquivo);

                            }
                        }

                    }
                }

                foreach (var item in _arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.Key &&
                                                         w.NomeArquivo.Contains("xml") &&
                                                        !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                        !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                        !w.NomeArquivo.Contains("~$"))
                                                        .OrderByDescending(o => o.NomeArquivo).OrderBy(o => o.IdEmpresa))
                {
                    _diretorio = item.DiretorioConfigurado;
                    if (!Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XML") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    quantidadeArquivos++;

                    _importandoArquivo = new ImportandoArquivei();
                    _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                    nomeArquivo = item.NomeArquivo;

                    if (_importandoArquivo.Existe)
                    {
                        try
                        {
                            quantidadeNFCadastradas++;
                            try
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessadoAnteriormente");
                            }
                            catch
                            {

                            }
                            _arquiveiJaRenomeado = true;
                            continue; // vai para o próximo arquivo
                        }
                        catch { }
                    }
                    else
                    {
                        _importandoArquivo.Arquivo = nomeArquivo;
                        _importandoArquivo.IdUsuario = Publicas._idUsuario;
                        _importandoArquivo.Importando = true;

                        new ArquiveiBO().GravarImportando(_importandoArquivo);

                        _importandoArquivo = new ArquiveiBO().ConsultarArquivo(nomeArquivo, Path.GetFileName(nomeArquivo));
                        _listaArquivosImportados.Add(_importandoArquivo);
                    }

                    string chaveAcesso = "";

                    if (nomeArquivo.ToLower().Contains("xml"))
                    {
                        notifyIcon1.BalloonTipText = "Importando Arquivei pelo arquivo " + Path.GetFileName(nomeArquivo) + " da Empresa " + item.Empresa + "...";
                        idArquivo = Arquivei_Xml(nomeArquivo, idArquivo, item.IdEmpresa, _importandoArquivo);

                        item.NomeArquivo = nomeArquivo;
                    }
                    else
                    {
                        Publicas._mensagemSistema = "Importando Arquivei pelo arquivo Excel da Empresa " + item.Empresa + "...";
                        xlApp = new Excel.Application();

                        try
                        {
                            xlApp.DisplayAlerts = false;
                            xlWorkBook = xlApp.Workbooks.Open(nomeArquivo, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 1);
                            try
                            {
                                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
                            }
                            catch
                            {
                                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                            }

                            range = xlWorkSheet.UsedRange;
                            rw = range.Rows.Count;
                            cl = range.Columns.Count;
                            // linha um é o cabeçalho
                            str = (Convert.ToString((range.Cells[1, 1] as Excel.Range).Value2));
                        }
                        catch
                        {
                            new ArquiveiBO().ExcluirImportando(_importandoArquivo);
                            return;
                        }

                        if (!str.ToUpper().Contains("CHAVE"))
                        {

                            Log _log = new Log();
                            _log.IdUsuario = 1;
                            _log.Descricao = "Arquivo '" + nomeArquivo + "' inválido. Campo 'Chave de acesso' na posição errada.";
                            _log.Tela = "Principal - Importação Arquivei";

                            try
                            {
                                new LogBO().Gravar(_log);
                            }
                            catch { }

                            try
                            {
                                File.Move(nomeArquivo, Path.GetDirectoryName(nomeArquivo) + @"\Processadas\" + _pastaDiaria + @"\" +
                                Path.GetFileName(nomeArquivo).Replace("xlsx", "xlsxInvalido"));
                            }
                            catch
                            {

                            }
                            continue;
                        }

                        for (rCnt = 2; rCnt <= rw; rCnt++)
                        {
                            _arquivei = new Arquivei();
                            _arquivei.IdEmpresa = item.IdEmpresa;
                            _arquivei.NomeArquivo = nomeArquivo;
                            _arquivei.TipoArquivo = "EXCEL";

                            _itens = new ItensArquivei();

                            _cadastrado = new Arquivei();

                            for (cCnt = 1; cCnt <= cl; cCnt++)
                            {

                                try
                                {
                                    str = Convert.ToString((range.Cells[rCnt, cCnt] as Excel.Range).Value2);
                                    _itens.IdArquivei = idArquivo;

                                    if (cCnt == 1)
                                    {
                                        _cadastrado = new ArquiveiBO().Consultar(item.IdEmpresa, str.Replace(" ", ""), 0);

                                        if (_cadastrado.Existe)
                                            break;
                                    }

                                    switch (cCnt)
                                    {
                                        case 1: // Chave de Acesso

                                            if (str != chaveAcesso || chaveAcesso == "") // grava o anterior
                                            {
                                                _cadastrado = new ArquiveiBO().Consultar(item.IdEmpresa, str.Replace(" ", ""), 0);

                                                if (chaveAcesso == "")
                                                    chaveAcesso = str;
                                                else
                                                {
                                                    // Verificar se já foi importada
                                                    _cadastrado = new ArquiveiBO().Consultar(item.IdEmpresa, chaveAcesso.Replace(" ", ""), 0);

                                                    if (!_cadastrado.Existe)
                                                    {
                                                        _arquivei.Id = idArquivo;
                                                        _arquivei.RazaoSocialDestinatario = razaosocialdestinatario;
                                                        _arquivei.RazaoSocialEmitente = razaosocialemitente;
                                                        _arquivei.CNPJDestinatario = cnpjdestinatario;
                                                        _arquivei.CNPJEmitente = cnpjemitente;
                                                        _arquivei.IEDestinatario = iedestinatario;
                                                        _arquivei.IEEmitente = ieemitente;
                                                        _arquivei.EnderecoDestinatario = enderecodestinatario;
                                                        _arquivei.NumeroEndDestinatario = numeroenddestinatario;
                                                        _arquivei.BairroDestinatario = bairrodestinatario;
                                                        _arquivei.CEPDestinatario = cepdestinatario;
                                                        _arquivei.ChaveDeAcesso = chaveAcesso;
                                                        _arquivei.DataEmissao = dataemissao;
                                                        _arquivei.NumeroNF = numeronf;
                                                        _arquivei.ModeloNF = modelonf;
                                                        _arquivei.Serie = serie;
                                                        _arquivei.Tipo = tipo;
                                                        _arquivei.Status = status;
                                                        _arquivei.Operacao = operacao;
                                                        _arquivei.NaturezaOperacao = naturezaoperacao;
                                                        _arquivei.ValorProduto = valorproduto;
                                                        _arquivei.ValorTotalNF = valortotalnf;
                                                        _arquivei.BaseICMS = baseicms;
                                                        _arquivei.DadosAdicionais = dadosadicionais;
                                                        _itens.IdArquivei = idArquivo;

                                                        _listaArquivei.Add(_arquivei);
                                                        idArquivo++;
                                                    }
                                                    _cadastrado = new ArquiveiBO().Consultar(item.IdEmpresa, str.Replace(" ", ""), 0);

                                                    _arquivei = new Arquivei();
                                                }
                                            }
                                            chaveAcesso = str;
                                            break;
                                        case 2:
                                            razaosocialdestinatario = str;
                                            break;
                                        case 3:
                                            cnpjdestinatario = str;
                                            break;
                                        case 4:
                                            iedestinatario = str;
                                            break;
                                        case 5:
                                            enderecodestinatario = str;
                                            break;
                                        case 6:
                                            numeroenddestinatario = str;
                                            break;
                                        case 7:
                                            bairrodestinatario = str;
                                            break;
                                        case 8:
                                            cepdestinatario = str;
                                            break;
                                        case 9:
                                            razaosocialemitente = str;
                                            break;
                                        case 10:
                                            cnpjemitente = str;
                                            break;
                                        case 11:
                                            ieemitente = str;
                                            break;
                                        case 12:
                                            try
                                            {
                                                dataemissao = DateTime.FromOADate(double.Parse(str));
                                            }
                                            catch { }
                                            break;
                                        case 13:
                                            try
                                            {
                                                numeronf = Convert.ToInt32(str);
                                            }
                                            catch { }
                                            break;
                                        case 14:
                                            modelonf = str;
                                            break;
                                        case 15:
                                            serie = str;
                                            break;
                                        case 16:
                                            tipo = str;
                                            break;
                                        case 17:
                                            status = str;
                                            break;
                                        case 18:
                                            naturezaoperacao = str;
                                            break;
                                        case 19:
                                            operacao = str;
                                            break;
                                        case 20:
                                            try
                                            {
                                                valortotalnf = Convert.ToDecimal(str);
                                            }
                                            catch { }
                                            break;
                                        case 21:
                                            try
                                            {
                                                valorproduto = Convert.ToDecimal(str);
                                            }
                                            catch { }
                                            break;
                                        case 22:
                                            try
                                            {
                                                baseicms = Convert.ToDecimal(str);
                                            }
                                            catch { }
                                            break;
                                        case 23:
                                            try
                                            {
                                                _itens.ValorICMS = Convert.ToDecimal(str);
                                            }
                                            catch { }
                                            break;
                                        case 24:
                                            try
                                            {
                                                if (str.Length > 2)
                                                    str = str.Substring(0, 2);
                                                _itens.AliquotaICMS = Convert.ToDecimal(str);
                                            }
                                            catch { }
                                            break;
                                        case 25:
                                            try
                                            {
                                                _itens.ValorICMSSub = Convert.ToDecimal(str);
                                            }
                                            catch { }
                                            break;
                                        case 26:
                                            try
                                            {
                                                _itens.ValorIPI = Convert.ToDecimal(str);
                                            }
                                            catch { }
                                            break;
                                        case 27:
                                            try
                                            {
                                                _itens.Desconto = Convert.ToDecimal(str);
                                            }
                                            catch { }
                                            break;
                                        case 28:
                                            try
                                            {
                                                _itens.Seguro = Convert.ToDecimal(str);
                                            }
                                            catch { }
                                            break;
                                        case 29:
                                            try
                                            {
                                                _itens.OutrasDespesas = Convert.ToDecimal(str);
                                            }
                                            catch { }
                                            break;
                                        case 30:
                                            try
                                            {
                                                _itens.ValorFrete = Convert.ToDecimal(str);
                                            }
                                            catch { }
                                            break;
                                        case 31:
                                            _itens.CCe = str;
                                            break;
                                        case 32:
                                            dadosadicionais = str;
                                            break;
                                        case 33:
                                            _itens.CST = str;
                                            break;
                                        //case 34:
                                        //  _itens.CSTICMS = str;
                                        //break;
                                        case 34:
                                            try
                                            {
                                                _itens.CFOP = Convert.ToInt32(str);
                                            }
                                            catch { }
                                            break;
                                        case 35:
                                            try
                                            {
                                                _itens.ValorTotal = Convert.ToDecimal(str);
                                            }
                                            catch { }
                                            break;
                                    }
                                }
                                catch (Exception)
                                {
                                    break;
                                    //new Notificacoes.Mensagem(ex.Message, Publicas.TipoMensagem.Erro).ShowDialog();
                                }
                            }
                            if (!_cadastrado.Existe && _itens.CFOP == 0 && (_itens.CST == "" || _itens.CST == null) && !status.ToUpper().Contains("CANCELADA"))
                            {
                                _listaArquivei.Remove(_arquivei);

                                Log _log = new Log();
                                _log.IdUsuario = 1;
                                _log.Descricao = "Nota " + numeronf + " com o status " + status + " do arquivo " + nomeArquivo + " não possui item";
                                _log.Tela = "Principal - Arquivei";

                                try
                                {
                                    new LogBO().Gravar(_log);
                                }
                                catch { }
                            }
                            else
                            if (!_cadastrado.Existe) // && (_itens.CFOP != 0 || _itens.CST != "" && _itens.CST != null) && !status.ToUpper().Contains("CANCELADA")) // Só inclui o item se tiver os campos CFOP ou CST informado , canceladas pode gravar
                                _listaItensArquivei.Add(_itens);
                        }

                        if (_listaArquivei.Where(w => w.ChaveDeAcesso == chaveAcesso).Count() == 0 && chaveAcesso != "")
                        {
                            _cadastrado = new ArquiveiBO().Consultar(item.IdEmpresa, chaveAcesso.Replace(" ", ""), 0);

                            if (!_cadastrado.Existe)
                            {
                                _arquivei.IdEmpresa = item.IdEmpresa;
                                _arquivei.NomeArquivo = nomeArquivo;
                                _arquivei.Id = idArquivo;
                                _arquivei.RazaoSocialDestinatario = razaosocialdestinatario;
                                _arquivei.RazaoSocialEmitente = razaosocialemitente;
                                _arquivei.CNPJDestinatario = cnpjdestinatario;
                                _arquivei.CNPJEmitente = cnpjemitente;
                                _arquivei.IEDestinatario = iedestinatario;
                                _arquivei.IEEmitente = ieemitente;
                                _arquivei.EnderecoDestinatario = enderecodestinatario;
                                _arquivei.NumeroEndDestinatario = numeroenddestinatario;
                                _arquivei.BairroDestinatario = bairrodestinatario;
                                _arquivei.CEPDestinatario = cepdestinatario;
                                _arquivei.ChaveDeAcesso = chaveAcesso;
                                _arquivei.DataEmissao = dataemissao;
                                _arquivei.NumeroNF = numeronf;
                                _arquivei.ModeloNF = modelonf;
                                _arquivei.Serie = serie;
                                _arquivei.Tipo = tipo;
                                _arquivei.Status = status;
                                _arquivei.Operacao = operacao;
                                _arquivei.NaturezaOperacao = naturezaoperacao;
                                _arquivei.ValorProduto = valorproduto;
                                _arquivei.ValorTotalNF = valortotalnf;
                                _arquivei.BaseICMS = baseicms;
                                _arquivei.DadosAdicionais = dadosadicionais;
                                _itens.IdArquivei = idArquivo;
                                _listaArquivei.Add(_arquivei);

                                // para começar a leitura do novo arquivo se existir
                                idArquivo++;
                                chaveAcesso = "";
                            }
                        }
                    }

                    #region Ação a ser tomada com o arquivo Arquivei
                    if (item.Acao == "E")
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                            }
                            else
                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                        }
                        catch (IOException)
                        {

                        }
                    }
                    else
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                {
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                                }
                            }
                            else
                            {

                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                            }

                        }
                        catch
                        {

                        }
                    }
                    #endregion
                }

                #region Envia Email
                _empresa = new EmpresaBO().Consultar(itemE.Key);

                string[] _dadosEmail = new string[50];
                _dadosEmail[0] = quantidadeArquivos.ToString();
                _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                _dadosEmail[2] = _empresa.NomeAbreviado;
                if (NFNovas.Count() != 0)
                {
                    _dadosEmail[3] = "Numero NF".PadRight(11).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + "Chave de Acesso" + "</br>";
                    foreach (var item in NFNovas)
                    {
                        _dadosEmail[3] = _dadosEmail[3] + item + "</br>";
                    }
                }

                _dadosEmail[4] = quantidadeNFCadastradas.ToString();
                _dadosEmail[6] = quantidadeProcessadoNovos.ToString();
                _dadosEmail[7] = quantidadeComErroLeitura.ToString();
                _dadosEmail[8] = _diretorio;
                _dadosEmail[9] = quantidadesCanceladas.ToString();
                _dadosEmail[10] = quantidadeCartaCorrecao.ToString();

                if (NFCanceladas.Count() != 0)
                {
                    _dadosEmail[11] = "Numero NF".PadRight(11).Replace(" ", "&nbsp") + "Emissão".PadRight(11).Replace(" ", "&nbsp") + "Chave de Acesso" + "</br>";
                    foreach (var item in NFCanceladas)
                    {
                        _dadosEmail[11] = _dadosEmail[11] + item + "</br>";
                    }
                }

                if (CartaCorrecao.Count() != 0)
                {
                    _dadosEmail[12] = "Numero NF".PadRight(11).Replace(" ", "&nbsp") + "Emissão".PadRight(11).Replace(" ", "&nbsp") + "Chave de Acesso" + "</br>";
                    foreach (var item in CartaCorrecao)
                    {
                        _dadosEmail[12] = _dadosEmail[12] + item + "</br>";
                    }
                }

                if (NFErros.Count() != 0)
                {
                    _dadosEmail[13] = "Arquivo" + "</br>";
                    foreach (var item in NFErros)
                    {
                        _dadosEmail[13] = _dadosEmail[13] + item + "</br>";
                    }
                }

                string emailDestino = "";

                foreach (var itemU in _listaUsuarios.Where(w => w.AcessaEscrituracaoFiscal))
                {
                    emailDestino = emailDestino + itemU.Email + "; ";
                }

                if (NFErros.Count() != 0)
                    emailDestino = "fflopes@supportse.com.br;mdmunoz@supportse.com.br";


                if (quantidadeArquivos != 0)
                    Classes.Publicas.EnviarEmailArquivei(_dadosEmail, emailDestino, "Arquivei - NFe Recebidas " + (NFErros.Count() != 0 ? "Com Erros" : ""));

                #endregion

                NFNovas.Clear();
                NFCanceladas.Clear();
                CartaCorrecao.Clear();
                NFErros.Clear();
            }

            if (_listaArquivei.Count != 0)
                new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei);

            foreach (var item in _listaArquivosImportados)
            {
                new ArquiveiBO().GravarImportando(item);
            }

            //Close();
            #endregion

            
        }

        private int Arquivei_Xml(string nomeArquivo, int idArquivo, int idEmpresa, ImportandoArquivei _importandoArquivo)
        {            
            List<Classes.TNfeProc> _listaNfe = new List<Classes.TNfeProc>();
            //_xml = new List<string>();

            if (nomeArquivo.ToLower().Contains("xml"))
            {

                XmlSerializer ser = new XmlSerializer(typeof(TNfeProc));

                try
                {
                    XmlTextReader reader = new XmlTextReader(nomeArquivo);

                    try
                    {
                        if (!Path.GetFileName(nomeArquivo).StartsWith("ID"))
                        {
                            Classes.TNfeProc nota = (TNfeProc)ser.Deserialize(reader);
                            _listaNfe.Add(nota);

                            if (nota.NFe.infNFe.ide.nNF == null)
                            {
                                quantidadeComErroLeitura++;
                                NFErros.Add(nomeArquivo);
                                //try
                                //{
                                //    File.Move(nomeArquivo, Path.GetDirectoryName(nomeArquivo) + @"\Processadas\" + _pastaDiaria + @"\" +
                                //    Path.GetFileName(nomeArquivo).Replace("xml", "xmlNaoImportado"));
                                //}
                                //catch { }

                                Log _log = new Log();
                                _log.IdUsuario = 1;
                                _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                                _log.Tela = "Principal - Arquivei";

                                try
                                {
                                    new LogBO().Gravar(_log);
                                }
                                catch { }

                                nomeArquivo = nomeArquivo.Replace("xml", "xmlNaoImportado");

                                _listaArquivosImportados.Remove(_importandoArquivo);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        quantidadeComErroLeitura++;
                        NFErros.Add(nomeArquivo);
                        Log _log = new Log();
                        _log.IdUsuario = 1;
                        _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo." + Environment.NewLine + ex.Message;
                        _log.Tela = "Principal - Arquivei";

                        try
                        {
                            new LogBO().Gravar(_log);
                        }
                        catch { }

                        nomeArquivo = nomeArquivo.Replace("xml", "xmlInvalido");
                        _listaArquivosImportados.Remove(_importandoArquivo);
                    }

                    reader.Close();
                }
                catch
                {
                    quantidadeComErroLeitura++;
                    NFErros.Add(nomeArquivo);
                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                    _log.Tela = "Principal - Arquivei";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    nomeArquivo = nomeArquivo.Replace("xml", "xmlNaoImportado");
                    _listaArquivosImportados.Remove(_importandoArquivo);
                }
            }

            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();
            Arquivei _cadastrado = null;

            foreach (var item in _listaNfe)
            {

                _arquivei.Id = idArquivo;

                _arquivei.ChaveDeAcesso = item.NFe.infNFe.Id.Replace("NFe", "");

                _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.ChaveDeAcesso, 0);

                try
                {
                    _arquivei.NumeroNF = Convert.ToDecimal(item.NFe.infNFe.ide.nNF);
                }
                catch { }

                if (_cadastrado.Existe)
                {
                    quantidadeNFCadastradas++;
                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Nota Fiscal " + _arquivei.NumeroNF + " com a Chave de Acesso " + _arquivei.ChaveDeAcesso + " já cadastrada para a empresa " + idEmpresa;
                    _log.Tela = "Principal - Arquivei - Nota ja cadastrada";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    nomeArquivo = nomeArquivo.Replace("xml", "xmlCadastradoProcessado");
                    _arquiveiJaRenomeado = true;
                    break;
                }

                _arquivei.TipoDocto = "NFe";
                _arquivei.IdEmpresa = idEmpresa;
                _arquivei.NomeArquivo = nomeArquivo;

                _arquivei.RazaoSocialDestinatario = item.NFe.infNFe.dest.xNome;
                _arquivei.RazaoSocialEmitente = item.NFe.infNFe.emit.xNome;

                _arquivei.CNPJDestinatario = item.NFe.infNFe.dest.Item;
                _arquivei.CNPJEmitente = item.NFe.infNFe.emit.Item;

                try
                {
                    _arquivei.IEDestinatario = item.NFe.infNFe.dest.IE.ToString();
                }
                catch { }

                try
                {
                    _arquivei.IEEmitente = item.NFe.infNFe.emit.IE.ToString();
                }
                catch { }

                try
                {
                    _arquivei.EnderecoDestinatario = item.NFe.infNFe.dest.enderDest.xLgr;
                }
                catch { }
                try
                {
                    _arquivei.NumeroEndDestinatario = item.NFe.infNFe.dest.enderDest.nro.ToString();
                }
                catch { }
                try
                {
                    _arquivei.BairroDestinatario = item.NFe.infNFe.dest.enderDest.xBairro;
                }
                catch { }
                try
                {
                    _arquivei.CEPDestinatario = item.NFe.infNFe.dest.enderDest.CEP.ToString();
                }
                catch { }

                try
                {
                    string dataEmissao = item.NFe.infNFe.ide.dhEmi.Substring(0, 10);

                    _arquivei.DataEmissao = Convert.ToDateTime(dataEmissao);
                        //Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                }
                catch
                {
                    try
                    {
                        _arquivei.DataEmissao = Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                    }
                    catch { }
                }


                _arquivei.ModeloNF = item.NFe.infNFe.ide.mod.ToString().Replace("Item", "");
                _arquivei.Serie = item.NFe.infNFe.ide.serie;
                _arquivei.Tipo = (item.NFe.infNFe.ide.tpNF == TNFeInfNFeIdeTpNF.Item1 ? "Saída" : "Entrada");

                _arquivei.Status = item.protNFe.infProt.xMotivo;

                foreach (var itemC in _xml)
                {
                    if (itemC.Contains(_arquivei.ChaveDeAcesso) && itemC.Contains("Cancelamento"))
                    {
                        quantidadesCanceladas++;
                        NFCanceladas.Add(_arquivei.NumeroNF.ToString().PadRight(11).Replace(" ", "&nbsp")  
                            + _arquivei.DataEmissao.ToShortDateString()
                            + "&nbsp"
                            + _arquivei.ChaveDeAcesso);

                        _arquivei.Status = "Cancelada";
                    }
                }

                //_arquivei.Operacao = operacao;
                _arquivei.NaturezaOperacao = item.NFe.infNFe.ide.natOp;

                try
                {
                    _arquivei.ValorProduto = Convert.ToDecimal(item.NFe.infNFe.total.ICMSTot.vProd.Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorTotalNF = Convert.ToDecimal(item.NFe.infNFe.total.ICMSTot.vNF.Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.BaseICMS = Convert.ToDecimal(item.NFe.infNFe.total.ICMSTot.vBC.Replace(".", ","));
                }
                catch { }

                _arquivei.TipoArquivo = "XML";

                try
                {
                    _arquivei.DadosAdicionais = item.NFe.infNFe.infAdic.infCpl;
                }
                catch { }

                foreach (var itemP in item.NFe.infNFe.det)
                {
                    _itens = new ItensArquivei();
                    _itens.IdArquivei = idArquivo;

                    try
                    {
                        _itens.CFOP = Convert.ToInt32(itemP.prod.CFOP);
                    }
                    catch { }

                    try
                    {
                        _itens.NCM = itemP.prod.NCM;
                    }
                    catch { }

                    try
                    {
                        _itens.ValorTotal = Convert.ToDecimal(itemP.prod.vProd.Replace(".", ","));
                    }
                    catch { }
                    try
                    {
                        _itens.ValorFrete = Convert.ToDecimal(itemP.prod.vFrete.Replace(".", ","));
                    }
                    catch { }

                    foreach (var itemC in _xml)
                    {
                        if (itemC.Contains(_arquivei.ChaveDeAcesso) && itemC.Contains("Carta de C"))
                        {
                            quantidadeCartaCorrecao++;
                            CartaCorrecao.Add(_arquivei.NumeroNF.ToString().PadRight(11).Replace(" ", "&nbsp") 
                                + _arquivei.DataEmissao.ToShortDateString()
                                + "&nbsp"
                                + _arquivei.ChaveDeAcesso);

                            _itens.CCe = itemC.Substring(itemC.IndexOf("<xCorrecao>") + 11,
                                itemC.IndexOf("</xCorrecao>") - (itemC.IndexOf("<xCorrecao>") + 11));
                        }
                    }

                    try
                    {
                        _itens.Desconto = Convert.ToDecimal(itemP.prod.vDesc.Replace(".", ","));
                    }
                    catch { }

                    try
                    {
                        _itens.Seguro = Convert.ToDecimal(itemP.prod.vSeg.Replace(".", ","));
                    }
                    catch { }

                    try
                    {
                        _itens.OutrasDespesas = Convert.ToDecimal(itemP.prod.vOutro.Replace(".", ","));
                    }
                    catch { }

                    try
                    {
                        #region Cofins
                        try
                        {

                            object _cofins = itemP.imposto.COFINS.Item;

                            try
                            {
                                _itens.ValorCofins = Convert.ToDecimal(((TNFeInfNFeDetImpostoCOFINSCOFINSAliq)_cofins).vCOFINS.Replace(".", ","));
                            }
                            catch { }

                            try
                            {
                                _itens.AliquotaCofins = Convert.ToDecimal(((TNFeInfNFeDetImpostoCOFINSCOFINSAliq)_cofins).pCOFINS.Replace(".", ","));
                            }
                            catch { }

                            try
                            {
                                _itens.BaseCofins = Convert.ToDecimal(((TNFeInfNFeDetImpostoCOFINSCOFINSAliq)_cofins).vBC.Replace(".", ","));
                            }
                            catch { }

                        }
                        catch { }
                        #endregion 

                        #region Pis
                        try
                        {
                            object _pis = itemP.imposto.PIS.Item;

                            try
                            {
                                _itens.ValorPis = Convert.ToDecimal(((TNFeInfNFeDetImpostoPISPISAliq)_pis).vPIS.Replace(".", ","));
                            }
                            catch { }

                            try
                            {
                                _itens.AliquotaPis = Convert.ToDecimal(((TNFeInfNFeDetImpostoPISPISAliq)_pis).pPIS.Replace(".", ","));
                            }
                            catch { }

                            try
                            {
                                _itens.BasePis = Convert.ToDecimal(((TNFeInfNFeDetImpostoPISPISAliq)_pis).vBC.Replace(".", ","));
                            }
                            catch { }

                        }
                        catch { }
                        #endregion

                        object[] _ob = itemP.imposto.Items;
                        object _icms = null;
                        object _ipi = null;
                        object _iss = null;
                        object _ii = null;

                        foreach (var itemO in _ob)
                        {

                            #region ICMS 
                            try
                            {
                                _icms = ((TNFeInfNFeDetImpostoICMS)itemO).Item;

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS00CST)((TNFeInfNFeDetImpostoICMSICMS00)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS00)_icms).vICMS.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS00)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS10CST)((TNFeInfNFeDetImpostoICMSICMS10)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS10)_icms).vICMS.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS10)_icms).pICMS.Replace(".", ","));

                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS10)_icms).vICMSST.Replace(".", ","));
                                    _itens.AliquotaICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS10)_icms).pICMSST.Replace(".", ","));

                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS20CST)((TNFeInfNFeDetImpostoICMSICMS20)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS20)_icms).vICMS.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS20)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS30CST)((TNFeInfNFeDetImpostoICMSICMS30)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS30)_icms).vICMSST.Replace(".", ","));
                                    _itens.AliquotaICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS30)_icms).pICMSST.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS40CST)((TNFeInfNFeDetImpostoICMSICMS40)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS40)_icms).vICMSDeson.Replace(".", ","));
                                    _itens.AliquotaICMS = 0;
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS51CST)((TNFeInfNFeDetImpostoICMSICMS51)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS51)_icms).vICMS.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS51)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS60CST)((TNFeInfNFeDetImpostoICMSICMS60)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS60)_icms).vICMSEfet.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS60)_icms).pICMSEfet.Replace(".", ","));
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS60)_icms).vICMSSubstituto.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS70CST)((TNFeInfNFeDetImpostoICMSICMS70)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS70)_icms).vICMS.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS70)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS90CST)((TNFeInfNFeDetImpostoICMSICMS90)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS90)_icms).vICMS.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS90)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMSPartCST)((TNFeInfNFeDetImpostoICMSICMSPart)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSPart)_icms).vICMS.Replace(".", ","));
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSPart)_icms).vICMSST.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSPart)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN101)_icms).pCredSN.Replace(".", ","));
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN101)_icms).vCredICMSSN.Replace(".", ","));
                                    _itens.ValorICMSSub = 0;
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.ValorICMSSub = 0;
                                    _itens.AliquotaICMS = 0;
                                }
                                catch { }

                                try
                                {
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN201)_icms).vCredICMSSN.Replace(".", ","));
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN201)_icms).vICMSST.Replace(".", ",").Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN201)_icms).pCredSN.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.ValorICMS = 0;
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN202)_icms).vICMSST.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN202)_icms).pICMSST.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN500)_icms).vICMSEfet.Replace(".", ","));
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN500)_icms).vICMSSubstituto.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN500)_icms).pICMSEfet.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN900)_icms).vICMS.Replace(".", ","));
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN900)_icms).vICMSST.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN900)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMSSTCST)((TNFeInfNFeDetImpostoICMSICMSST)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSST)_icms).pICMSEfet.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSST)_icms).pST.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                            }
                            catch { }
                            #endregion

                            #region II
                            try
                            {
                                _ii = ((TNFeInfNFeDetImpostoII)itemO);
                                try
                                {
                                    _itens.ValorII = Convert.ToDecimal(((TNFeInfNFeDetImpostoII)_ii).vII.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.ValorIOF = Convert.ToDecimal(((TNFeInfNFeDetImpostoII)_ii).vIOF.Replace(".", ","));
                                }
                                catch { }
                            }
                            catch { }
                            #endregion

                            #region IPI
                            try
                            {
                                _ipi = ((TIpi)itemO).Item;

                                try
                                {
                                    _itens.ValorIPI = Convert.ToDecimal(((TIpiIPITrib)_ipi).vIPI.Replace(".", ","));

                                    string[] _ipiObjeto = ((TIpiIPITrib)_ipi).Items;

                                    try
                                    {
                                        _itens.BaseIPI = Convert.ToDecimal(_ipiObjeto[0].Replace(".", ","));
                                    }
                                    catch { }
                                    try
                                    {
                                        _itens.AliquotaIPI = Convert.ToDecimal(_ipiObjeto[1].Replace(".", ","));
                                    }
                                    catch { }

                                }
                                catch { }
                            }
                            catch { }
                            #endregion

                            #region ISS
                            try
                            {
                                _iss = ((TNFeInfNFeDetImpostoISSQN)itemO);

                                try
                                {
                                    _itens.ValorISS = Convert.ToDecimal(((TNFeInfNFeDetImpostoISSQN)_iss).vISSQN.Replace(".", ","));
                                }
                                catch { }
                                try
                                {
                                    _itens.BaseISS = Convert.ToDecimal(((TNFeInfNFeDetImpostoISSQN)_iss).vBC.Replace(".", ","));
                                }
                                catch { }

                                try
                                {
                                    _itens.AliquotaISS = Convert.ToDecimal(((TNFeInfNFeDetImpostoISSQN)_iss).vAliq.Replace(".", ","));
                                }
                                catch { }
                                continue;
                            }
                            catch { }
                            #endregion
                        }


                    }
                    catch { }

                    _itens.CSTICMS = _itens.CST;
                    _listaItensArquivei.Add(_itens);
                }

                NFNovas.Add(_arquivei.NumeroNF.ToString().PadRight(11).Replace(" ", "&nbsp") 
                    + _arquivei.DataEmissao.ToShortDateString()
                    + "&nbsp"
                    + _arquivei.ChaveDeAcesso);
                _listaArquivei.Add(_arquivei);
            }

            if (_listaArquivei.Count != 0)
            {
                quantidadeProcessadoNovos++;
                if (!new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei))
                    NFErros.Add(nomeArquivo);
            }
            _listaArquivei.Clear();
            _listaItensArquivei.Clear();

            idArquivo++;
            return idArquivo;
        }

        #endregion

        #region Emitidas
        private void NF_Xml_Emitidas()
        {
            
            _listaParametrosArquivei = new ParametrosArquiveiBO().Listar();
            _listaItensParametros = new ItensParametrosArquiveiBO().Listar(0);
            _arquivosPorEmpresa = new List<ArquivosEmpresaArquivei>();
            _pastaDiaria = DateTime.Now.ToString().Replace("/", "").Replace(" ", "_").Replace(":", "");
            List<string> _listaArquivos = new List<string>();


            #region atributos
            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();

            _listaItensArquivei = new List<ItensArquivei>();
            _listaArquivei = new List<Arquivei>();

            int quantidadeArquivos = 0;
            int quantidadeEventos = 0;
            int quantidadesCanceladas = 0;
            int quantidadeCartaCorrecao = 0;

            DateTime dataemissao = DateTime.MinValue;
            
            List<string> NFCanceladas = new List<string>();
            List<string> CartaCorrecao = new List<string>();
            List<string> NFErros = new List<string>();

            #endregion

            #region Leitura/Gravação arquivo excel.
            int idArquivo = 1;
            
            ImportandoArquivei _importandoArquivo = null;
            _listaArquivosImportados = new List<ImportandoArquivei>();

            _xml = new List<string>();

            foreach (var itemE in _listaEmpresas)//.Where(w => w.IdEmpresa == 2))
            {
                _empresa = new EmpresaBO().Consultar(itemE.IdEmpresa);

                Log _log = new Log();
                _log.IdUsuario = 1;
                _log.Descricao = "Iniciou importação arquivei NFe da empresa " + _empresa.NomeAbreviado;
                _log.Tela = "Principal - Arquivei";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }

                quantidadeArquivos = 0;
                quantidadeEventos = 0;
                quantidadeNFCadastradas = 0;
                quantidadeProcessadoNovos = 0;
                quantidadeComErroLeitura = 0;
                quantidadesCanceladas = 0;
                quantidadeCartaCorrecao = 0;

                NFNovas.Clear();
                NFCanceladas.Clear();
                CartaCorrecao.Clear();
                NFErros.Clear();

                List<Classes.XmlNFe_Globus> _listaNfeGlobus = new ArquiveiBO().ImportarNFe_XmlGlobus(itemE.IdEmpresa);

                foreach (var item in _listaNfeGlobus.Where(w => !w.Existe).OrderBy(o => o.IdNFEGlobus))
                {
                    quantidadeArquivos++;

                    _importandoArquivo = new ImportandoArquivei();
                    _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.ChaveAcesso + "_Globus", item.ChaveAcesso + "_Globus");

                    if (_importandoArquivo.Existe)
                    {
                        try
                        {
                            quantidadeNFCadastradas++;
                            continue; // vai para o próximo arquivo
                        }
                        catch { }
                    }

                    _importandoArquivo.Arquivo = item.ChaveAcesso + "_Globus";
                    _importandoArquivo.IdUsuario = Publicas._idUsuario;
                    _importandoArquivo.Importando = true;
                    _importandoArquivo.IdNFEGlobus = item.IdNFEGlobus;

                    new ArquiveiBO().GravarImportando(_importandoArquivo);

                    _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.ChaveAcesso + "_Globus", item.ChaveAcesso + "_Globus");
                    _listaArquivosImportados.Add(_importandoArquivo);

                    idArquivo = NFe_Xml_Enviadas(item.Xml, idArquivo, itemE.IdEmpresa, _importandoArquivo, item.ChaveAcesso + "_Globus", item.IdNFEGlobus, item.Status);
                }

                #region Envia Email
                if (_listaNfeGlobus.Where(w => !w.Existe).Count() > 0) 
                {
                    string[] _dadosEmail = new string[50];
                    _dadosEmail[0] = quantidadeArquivos.ToString();
                    _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                    _dadosEmail[2] = _empresa.NomeAbreviado;
                    if (NFNovas.Count() != 0)
                    {
                        _dadosEmail[3] = "Numero NF".PadRight(11).Replace(" ", "&nbsp") + "Emissão".PadRight(11).Replace(" ", "&nbsp") + "Chave de Acesso" + "</br>";
                        foreach (var item in NFNovas)
                        {
                            _dadosEmail[3] = _dadosEmail[3] + item + "</br>";
                        }
                    }

                    _dadosEmail[4] = quantidadeNFCadastradas.ToString();
                    _dadosEmail[6] = NFNovas.Count().ToString();
                    _dadosEmail[7] = NFErros.Count().ToString();
                    _dadosEmail[8] = _diretorio;
                    _dadosEmail[9] = NFCanceladas.Count().ToString();
                    _dadosEmail[10] = CartaCorrecao.Count().ToString();

                    if (NFCanceladas.Count() != 0)
                    {
                        _dadosEmail[11] = "Numero NF".PadRight(11).Replace(" ", "&nbsp") + "Emissão".PadRight(11).Replace(" ", "&nbsp") + "Chave de Acesso" + "</br>";
                        foreach (var item in NFCanceladas)
                        {
                            _dadosEmail[11] = _dadosEmail[11] + item + "</br>";
                        }
                    }

                    if (CartaCorrecao.Count() != 0)
                    {
                        _dadosEmail[12] = "Numero NF".PadRight(11).Replace(" ", "&nbsp") + "Emissão".PadRight(11).Replace(" ", "&nbsp") + "Chave de Acesso" + "</br>";
                        foreach (var item in CartaCorrecao)
                        {
                            _dadosEmail[12] = _dadosEmail[12] + item + "</br>";
                        }
                    }

                    if (NFErros.Count() != 0)
                    {
                        _dadosEmail[13] = "Arquivo" + "</br>";
                        foreach (var item in NFErros)
                        {
                            _dadosEmail[13] = _dadosEmail[13] + item + "</br>";
                        }
                    }

                    string emailDestino = "";

                    foreach (var itemU in _listaUsuarios.Where(w => w.AcessaEscrituracaoFiscal))
                    {
                        emailDestino = emailDestino + itemU.Email + "; ";
                    }

                    if (NFErros.Count() != 0)
                        emailDestino = "fflopes@supportse.com.br;mdmunoz@supportse.com.br";


                    if (quantidadeArquivos != 0)
                        Classes.Publicas.EnviarEmailArquivei(_dadosEmail, emailDestino, "Arquivei - NFe Emitidas " + (NFErros.Count() != 0 ? "Com Erros" : ""), true, false, true);
                }
                #endregion
                //fim Envia Email

                NFNovas.Clear();
                NFCanceladas.Clear();
                CartaCorrecao.Clear();
                NFErros.Clear();
            }

            if (_listaArquivei.Count != 0)
                new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei);

            foreach (var item in _listaArquivosImportados)
            {
                new ArquiveiBO().GravarImportando(item);
            }

            //Close();
            #endregion
        }
        
        private int NFe_Xml_Enviadas(string conteudoXml, int idArquivo, int idEmpresa, ImportandoArquivei _importandoArquivo, string nomeArquivo, decimal idNFeGlobus, string status)
        {
            List<Classes.TNfeProc> _listaNfe = new List<Classes.TNfeProc>();

            List<Classes.TNFe> _listaNfe_aux = new List<Classes.TNFe>();
            {

                XmlSerializer ser = new XmlSerializer(typeof(TNfeProc));

                try
                {
                    var valorSerealizado = new StringReader(conteudoXml);

                    try
                    {

                        Classes.TNfeProc nota = (TNfeProc)ser.Deserialize(valorSerealizado);
                        _listaNfe.Add(nota);

                        if (nota.NFe.infNFe.ide.nNF == null)
                        {
                            quantidadeComErroLeitura++;
                            NFErros.Add(nomeArquivo);

                            Log _log = new Log();
                            _log.IdUsuario = 1;
                            _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o xml.";
                            _log.Tela = "Principal - Arquivei";

                            try
                            {
                                new LogBO().Gravar(_log);
                            }
                            catch { }

                            _listaArquivosImportados.Remove(_importandoArquivo);
                        }

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            XmlSerializer ser_aux = new XmlSerializer(typeof(TNFe));
                            valorSerealizado = new StringReader(conteudoXml);
                            Classes.TNFe nota_aux = (TNFe)ser_aux.Deserialize(valorSerealizado);

                            if (nota_aux.infNFe.ide.nNF == null)
                            {
                                quantidadeComErroLeitura++;
                                NFErros.Add(nomeArquivo);

                                Log _log = new Log();
                                _log.IdUsuario = 1;
                                _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o xml." + Environment.NewLine + ex.Message;
                                _log.Tela = "Principal - Arquivei";

                                try
                                {
                                    new LogBO().Gravar(_log);
                                }
                                catch { }

                                _listaArquivosImportados.Remove(_importandoArquivo);
                            }
                            else
                            {
                                _listaNfe_aux.Add(nota_aux);

                                Classes.TNfeProc nota = new TNfeProc();
                                nota.NFe = nota_aux;

                                _listaNfe.Add(nota);
                            }
                        }
                        catch (Exception exx)
                        {

                            quantidadeComErroLeitura++;
                            NFErros.Add(nomeArquivo);
                            Log _log = new Log();
                            _log.IdUsuario = 1;
                            _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o xml." + Environment.NewLine + exx.Message;
                            _log.Tela = "Principal - Arquivei";

                            try
                            {
                                new LogBO().Gravar(_log);
                            }
                            catch { }

                            _listaArquivosImportados.Remove(_importandoArquivo);
                        }
                    }

                }
                catch
                {
                    quantidadeComErroLeitura++;
                    NFErros.Add(nomeArquivo);
                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o xml.";
                    _log.Tela = "Principal - Arquivei";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    _listaArquivosImportados.Remove(_importandoArquivo);
                }
            }

            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();
            Arquivei _cadastrado = null;

            foreach (var item in _listaNfe)
            {

                _arquivei.Id = idArquivo;
                _arquivei.IdNFEGlobus = idNFeGlobus;

                _arquivei.ChaveDeAcesso = item.NFe.infNFe.Id.Replace("NFe", "");

                _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.ChaveDeAcesso, 0);

                try
                {
                    _arquivei.NumeroNF = Convert.ToDecimal(item.NFe.infNFe.ide.nNF);
                }
                catch { }

                if (_cadastrado.Existe)
                {
                    quantidadeNFCadastradas++;
                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Nota Fiscal " + _arquivei.NumeroNF + " com a Chave de Acesso " + _arquivei.ChaveDeAcesso + " já cadastrada para a empresa " + idEmpresa;
                    _log.Tela = "Principal - Arquivei - Nota ja cadastrada";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                    break;
                }

                _arquivei.TipoDocto = "NFe";
                _arquivei.IdEmpresa = idEmpresa;
                _arquivei.NomeArquivo = nomeArquivo;

                _arquivei.RazaoSocialDestinatario = item.NFe.infNFe.dest.xNome;
                _arquivei.RazaoSocialEmitente = item.NFe.infNFe.emit.xNome;

                _arquivei.CNPJDestinatario = item.NFe.infNFe.dest.Item;
                _arquivei.CNPJEmitente = item.NFe.infNFe.emit.Item;

                try
                {
                    _arquivei.IEDestinatario = item.NFe.infNFe.dest.IE.ToString();
                }
                catch { }

                try
                {
                    _arquivei.IEEmitente = item.NFe.infNFe.emit.IE.ToString();
                }
                catch { }

                try
                {
                    _arquivei.EnderecoDestinatario = item.NFe.infNFe.dest.enderDest.xLgr;
                }
                catch { }
                try
                {
                    _arquivei.NumeroEndDestinatario = item.NFe.infNFe.dest.enderDest.nro.ToString();
                }
                catch { }
                try
                {
                    _arquivei.BairroDestinatario = item.NFe.infNFe.dest.enderDest.xBairro;
                }
                catch { }
                try
                {
                    _arquivei.CEPDestinatario = item.NFe.infNFe.dest.enderDest.CEP.ToString();
                }
                catch { }

                try
                {
                    string dataEmissao = item.NFe.infNFe.ide.dhEmi.Substring(0, 10);

                    _arquivei.DataEmissao = Convert.ToDateTime(dataEmissao);
                    //Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                }
                catch
                {
                    try
                    {
                        _arquivei.DataEmissao = Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                    }
                    catch { }
                }


                _arquivei.ModeloNF = item.NFe.infNFe.ide.mod.ToString().Replace("Item", "");
                _arquivei.Serie = item.NFe.infNFe.ide.serie;
                _arquivei.Tipo = (item.NFe.infNFe.ide.tpNF == TNFeInfNFeIdeTpNF.Item1 ? "Saída" : "Entrada");
                _arquivei.TipoProcessamento = "Emitidos";

                try
                {
                    _arquivei.Status = item.protNFe.infProt.xMotivo;
                }
                catch
                {
                    _arquivei.Status = status;
                }

                foreach (var itemC in _xml)
                {
                    if (itemC.Contains(_arquivei.ChaveDeAcesso) && itemC.Contains("Cancelamento"))
                    {
                        quantidadesCanceladas++;
                        NFCanceladas.Add(_arquivei.NumeroNF.ToString().PadRight(11).Replace(" ", "&nbsp") +
                                         _arquivei.DataEmissao.ToShortDateString().PadRight(11).Replace(" ", "&nbsp")
                                          + _arquivei.ChaveDeAcesso);

                        _arquivei.Status = "Cancelada";
                    }
                }

                //_arquivei.Operacao = operacao;
                _arquivei.NaturezaOperacao = item.NFe.infNFe.ide.natOp;

                try
                {
                    _arquivei.ValorProduto = Convert.ToDecimal(item.NFe.infNFe.total.ICMSTot.vProd.Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorTotalNF = Convert.ToDecimal(item.NFe.infNFe.total.ICMSTot.vNF.Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.BaseICMS = Convert.ToDecimal(item.NFe.infNFe.total.ICMSTot.vBC.Replace(".", ","));
                }
                catch { }

                _arquivei.TipoArquivo = "XML";

                try
                {
                    _arquivei.DadosAdicionais = item.NFe.infNFe.infAdic.infCpl;

                    if (_arquivei.DadosAdicionais == "" || _arquivei.DadosAdicionais == null)
                    {
                        foreach (var itemA in item.NFe.infNFe.infAdic.obsCont)
                        {
                            if (itemA.xCampo == "Dados adicionais")
                                _arquivei.DadosAdicionais = _arquivei.DadosAdicionais + itemA.xTexto;
                        }
                    }
                }
                catch { }

                foreach (var itemP in item.NFe.infNFe.det)
                {
                    _itens = new ItensArquivei();
                    _itens.IdArquivei = idArquivo;

                    try
                    {
                        _itens.CFOP = Convert.ToInt32(itemP.prod.CFOP);
                    }
                    catch { }

                    try
                    {
                        _itens.NCM = itemP.prod.NCM;
                    }
                    catch { }

                    try
                    {
                        _itens.ValorTotal = Convert.ToDecimal(itemP.prod.vProd.Replace(".", ","));
                    }
                    catch { }
                    try
                    {
                        _itens.ValorFrete = Convert.ToDecimal(itemP.prod.vFrete.Replace(".", ","));
                    }
                    catch { }

                    foreach (var itemC in _xml)
                    {
                        if (itemC.Contains(_arquivei.ChaveDeAcesso) && itemC.Contains("Carta de C"))
                        {
                            quantidadeCartaCorrecao++;
                            CartaCorrecao.Add(_arquivei.NumeroNF.ToString().PadRight(11).Replace(" ", "&nbsp") +
                                              _arquivei.DataEmissao.ToShortDateString().PadRight(11).Replace(" ", "&nbsp")
                                             + _arquivei.ChaveDeAcesso);

                            _itens.CCe = itemC.Substring(itemC.IndexOf("<xCorrecao>") + 11,
                                itemC.IndexOf("</xCorrecao>") - (itemC.IndexOf("<xCorrecao>") + 11));
                        }
                    }

                    try
                    {
                        _itens.Desconto = Convert.ToDecimal(itemP.prod.vDesc.Replace(".", ","));
                    }
                    catch { }

                    try
                    {
                        _itens.Seguro = Convert.ToDecimal(itemP.prod.vSeg.Replace(".", ","));
                    }
                    catch { }

                    try
                    {
                        _itens.OutrasDespesas = Convert.ToDecimal(itemP.prod.vOutro.Replace(".", ","));
                    }
                    catch { }

                    try
                    {
                        #region Cofins
                        try
                        {

                            object _cofins = itemP.imposto.COFINS.Item;

                            try
                            {
                                _itens.ValorCofins = Convert.ToDecimal(((TNFeInfNFeDetImpostoCOFINSCOFINSAliq)_cofins).vCOFINS.Replace(".", ","));
                            }
                            catch { }

                            try
                            {
                                _itens.AliquotaCofins = Convert.ToDecimal(((TNFeInfNFeDetImpostoCOFINSCOFINSAliq)_cofins).pCOFINS.Replace(".", ","));
                            }
                            catch { }

                            try
                            {
                                _itens.BaseCofins = Convert.ToDecimal(((TNFeInfNFeDetImpostoCOFINSCOFINSAliq)_cofins).vBC.Replace(".", ","));
                            }
                            catch { }

                        }
                        catch { }
                        #endregion 

                        #region Pis
                        try
                        {
                            object _pis = itemP.imposto.PIS.Item;

                            try
                            {
                                _itens.ValorPis = Convert.ToDecimal(((TNFeInfNFeDetImpostoPISPISAliq)_pis).vPIS.Replace(".", ","));
                            }
                            catch { }

                            try
                            {
                                _itens.AliquotaPis = Convert.ToDecimal(((TNFeInfNFeDetImpostoPISPISAliq)_pis).pPIS.Replace(".", ","));
                            }
                            catch { }

                            try
                            {
                                _itens.BasePis = Convert.ToDecimal(((TNFeInfNFeDetImpostoPISPISAliq)_pis).vBC.Replace(".", ","));
                            }
                            catch { }

                        }
                        catch { }
                        #endregion

                        object[] _ob = itemP.imposto.Items;
                        object _icms = null;
                        object _ipi = null;
                        object _iss = null;
                        object _ii = null;

                        foreach (var itemO in _ob)
                        {

                            #region ICMS 
                            try
                            {
                                _icms = ((TNFeInfNFeDetImpostoICMS)itemO).Item;

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS00CST)((TNFeInfNFeDetImpostoICMSICMS00)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS00)_icms).vICMS.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS00)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS10CST)((TNFeInfNFeDetImpostoICMSICMS10)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS10)_icms).vICMS.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS10)_icms).pICMS.Replace(".", ","));

                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS10)_icms).vICMSST.Replace(".", ","));
                                    _itens.AliquotaICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS10)_icms).pICMSST.Replace(".", ","));

                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS20CST)((TNFeInfNFeDetImpostoICMSICMS20)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS20)_icms).vICMS.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS20)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS30CST)((TNFeInfNFeDetImpostoICMSICMS30)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS30)_icms).vICMSST.Replace(".", ","));
                                    _itens.AliquotaICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS30)_icms).pICMSST.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS40CST)((TNFeInfNFeDetImpostoICMSICMS40)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS40)_icms).vICMSDeson.Replace(".", ","));
                                    _itens.AliquotaICMS = 0;
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS51CST)((TNFeInfNFeDetImpostoICMSICMS51)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS51)_icms).vICMS.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS51)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS60CST)((TNFeInfNFeDetImpostoICMSICMS60)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS60)_icms).vICMSEfet.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS60)_icms).pICMSEfet.Replace(".", ","));
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS60)_icms).vICMSSubstituto.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS70CST)((TNFeInfNFeDetImpostoICMSICMS70)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS70)_icms).vICMS.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS70)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMS90CST)((TNFeInfNFeDetImpostoICMSICMS90)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS90)_icms).vICMS.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMS90)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMSPartCST)((TNFeInfNFeDetImpostoICMSICMSPart)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSPart)_icms).vICMS.Replace(".", ","));
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSPart)_icms).vICMSST.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSPart)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN101)_icms).pCredSN.Replace(".", ","));
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN101)_icms).vCredICMSSN.Replace(".", ","));
                                    _itens.ValorICMSSub = 0;
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.ValorICMSSub = 0;
                                    _itens.AliquotaICMS = 0;
                                }
                                catch { }

                                try
                                {
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN201)_icms).vCredICMSSN.Replace(".", ","));
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN201)_icms).vICMSST.Replace(".", ",").Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN201)_icms).pCredSN.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.ValorICMS = 0;
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN202)_icms).vICMSST.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN202)_icms).pICMSST.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN500)_icms).vICMSEfet.Replace(".", ","));
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN500)_icms).vICMSSubstituto.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN500)_icms).pICMSEfet.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.ValorICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN900)_icms).vICMS.Replace(".", ","));
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN900)_icms).vICMSST.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSSN900)_icms).pICMS.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.CST = ((TNFeInfNFeDetImpostoICMSICMSSTCST)((TNFeInfNFeDetImpostoICMSICMSST)_icms).CST).ToString().Replace("Item", "");
                                    _itens.ValorICMSSub = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSST)_icms).pICMSEfet.Replace(".", ","));
                                    _itens.AliquotaICMS = Convert.ToDecimal(((TNFeInfNFeDetImpostoICMSICMSST)_icms).pST.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                            }
                            catch { }
                            #endregion

                            #region II
                            try
                            {
                                _ii = ((TNFeInfNFeDetImpostoII)itemO);
                                try
                                {
                                    _itens.ValorII = Convert.ToDecimal(((TNFeInfNFeDetImpostoII)_ii).vII.Replace(".", ","));
                                    continue;
                                }
                                catch { }

                                try
                                {
                                    _itens.ValorIOF = Convert.ToDecimal(((TNFeInfNFeDetImpostoII)_ii).vIOF.Replace(".", ","));
                                }
                                catch { }
                            }
                            catch { }
                            #endregion

                            #region IPI
                            try
                            {
                                _ipi = ((TIpi)itemO).Item;

                                try
                                {
                                    _itens.ValorIPI = Convert.ToDecimal(((TIpiIPITrib)_ipi).vIPI.Replace(".", ","));

                                    string[] _ipiObjeto = ((TIpiIPITrib)_ipi).Items;

                                    try
                                    {
                                        _itens.BaseIPI = Convert.ToDecimal(_ipiObjeto[0].Replace(".", ","));
                                    }
                                    catch { }
                                    try
                                    {
                                        _itens.AliquotaIPI = Convert.ToDecimal(_ipiObjeto[1].Replace(".", ","));
                                    }
                                    catch { }

                                }
                                catch { }
                            }
                            catch { }
                            #endregion

                            #region ISS
                            try
                            {
                                _iss = ((TNFeInfNFeDetImpostoISSQN)itemO);

                                try
                                {
                                    _itens.ValorISS = Convert.ToDecimal(((TNFeInfNFeDetImpostoISSQN)_iss).vISSQN.Replace(".", ","));
                                }
                                catch { }
                                try
                                {
                                    _itens.BaseISS = Convert.ToDecimal(((TNFeInfNFeDetImpostoISSQN)_iss).vBC.Replace(".", ","));
                                }
                                catch { }

                                try
                                {
                                    _itens.AliquotaISS = Convert.ToDecimal(((TNFeInfNFeDetImpostoISSQN)_iss).vAliq.Replace(".", ","));
                                }
                                catch { }
                                continue;
                            }
                            catch { }
                            #endregion
                        }


                    }
                    catch { }

                    _itens.CSTICMS = _itens.CST;
                    _listaItensArquivei.Add(_itens);
                }

                NFNovas.Add(_arquivei.NumeroNF.ToString().PadRight(11).Replace(" ", "&nbsp") 
                    +
                    _arquivei.DataEmissao.ToShortDateString().PadRight(11).Replace(" ", "&nbsp") 
                    + _arquivei.ChaveDeAcesso);
                _listaArquivei.Add(_arquivei);
            }

            if (_listaArquivei.Count != 0)
            {
               // quantidadeProcessadoNovos++;
                if (!new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei))
                    NFErros.Add(nomeArquivo);
            }
            _listaArquivei.Clear();
            _listaItensArquivei.Clear();

            idArquivo++;
            return idArquivo;
        }
        #endregion

        #endregion

        #region DACTe - por XML 
        private void ArquiveiDacte()
        {
            bool _processo8h = false;
            bool _processo10h = false;
            bool _processo12h = false;
            bool _processo14h = false;
            bool _processo16h = false;
            bool _processo18h = false;

            #region Horarios
            if (((DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).AddMinutes(00).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                 //(DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(8).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(10).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(12).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(14).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(16).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(18).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay)) ||
                (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)) // só de semana
            {
                _empresaProcessadaDacte8h.Clear();
                _empresaProcessadaDacte10h.Clear();
                _empresaProcessadaDacte12h.Clear();
                _empresaProcessadaDacte14h.Clear();
                _empresaProcessadaDacte16h.Clear();
                _empresaProcessadaDacte18h.Clear();
                return;
            }


            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(8).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                !_processo10h)
            {
                _processo8h = true;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaDacte10h.Clear();
                _empresaProcessadaDacte12h.Clear();
                _empresaProcessadaDacte14h.Clear();
                _empresaProcessadaDacte16h.Clear();
                _empresaProcessadaDacte18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(10).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                !_processo12h)
            {
                _processo8h = false;
                _processo10h = true;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaDacte8h.Clear();
                _empresaProcessadaDacte12h.Clear();
                _empresaProcessadaDacte14h.Clear();
                _empresaProcessadaDacte16h.Clear();
                _empresaProcessadaDacte18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(12).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                !_processo14h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = true;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaDacte8h.Clear();
                _empresaProcessadaDacte10h.Clear();
                _empresaProcessadaDacte14h.Clear();
                _empresaProcessadaDacte16h.Clear();
                _empresaProcessadaDacte18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(14).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                !_processo16h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = true;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaDacte8h.Clear();
                _empresaProcessadaDacte10h.Clear();
                _empresaProcessadaDacte12h.Clear();
                _empresaProcessadaDacte16h.Clear();
                _empresaProcessadaDacte18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(16).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                !_processo18h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = true;
                _processo18h = false;

                _empresaProcessadaDacte8h.Clear();
                _empresaProcessadaDacte10h.Clear();
                _empresaProcessadaDacte12h.Clear();
                _empresaProcessadaDacte14h.Clear();
                _empresaProcessadaDacte18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(18).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay) &&
                !_processo8h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = true;

                _empresaProcessadaDacte8h.Clear();
                _empresaProcessadaDacte10h.Clear();
                _empresaProcessadaDacte12h.Clear();
                _empresaProcessadaDacte14h.Clear();
                _empresaProcessadaDacte16h.Clear();
            }

            #endregion

            _listaParametrosArquivei = new ParametrosArquiveiBO().Listar();
            _listaItensParametros = new ItensParametrosArquiveiBO().Listar(0);
            _arquivosPorEmpresa = new List<ArquivosEmpresaArquivei>();
            _pastaDiaria = DateTime.Now.ToString().Replace("/", "").Replace(" ", "_").Replace(":", "");
            List<string> _listaArquivos = new List<string>();

            DirectoryInfo dirInfo;

            foreach (var itemE in _listaEmpresas)//.Where(w => w.IdEmpresa == 5))
            {
                if (_processo8h)
                {
                    if (_empresaProcessadaDacte8h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo10h)
                {
                    if (_empresaProcessadaDacte10h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo12h)
                {
                    if (_empresaProcessadaDacte12h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo14h)
                {
                    if (_empresaProcessadaDacte14h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo16h)
                {
                    if (_empresaProcessadaDacte16h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                {
                    if (_empresaProcessadaDacte18h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }

                _empresa = new EmpresaBO().Consultar(itemE.IdEmpresa);
                foreach (var item in _listaParametrosArquivei.Where(w => w.IdEmpresa == itemE.IdEmpresa && w.DiretorioDacte != ""))
                {
                    dirInfo = new DirectoryInfo(item.DiretorioDacte);
                    //dirInfo =  new DirectoryInfo(@"n:\arquivei\15698659000211\CTe\");

                    if (dirInfo.Exists)
                    {
                        #region Antigo
                        FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

                        if (files.Count() != 0) // configuraçao antiga
                        {
                            foreach (FileSystemInfo file in files)
                            {
                                if (!Path.GetExtension(file.FullName).ToUpper().Equals(".XML") && !Path.GetExtension(file.FullName).ToUpper().Equals(".XLSX"))
                                    continue;

                                if (!file.FullName.ToUpper().Contains("PROCESSADO") &&
                                    !file.FullName.ToUpper().Contains("INVALIDO") &&
                                    !file.FullName.ToUpper().Contains("CRDOWNLOAD") &&
                                    !file.FullName.ToUpper().Contains("NAOIMPORTADO") &&
                                    !file.FullName.Contains("~$") &&
                                    (file.FullName.Contains(".xls") || file.FullName.Contains(".xml")))
                                {

                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa
                                                                                           ,
                                        NomeArquivo = file.FullName
                                                                                           ,
                                        Acao = "R"// nao é mais excluido ou renomeado os arquivos
                                                                                           ,
                                        Empresa = _empresa.NomeAbreviado
                                                                                           ,
                                        DiretorioConfigurado = item.DiretorioDacte
                                    });
                                }
                            }
                        }
                        #endregion

                        #region Novo

                        _listaArquivos = Diretorio(item.DiretorioDacte);
                        //_listaArquivos = Diretorio(@"n:\arquivei\15698659000211\CTe\");

                        foreach (var itemA in _listaArquivos)
                        {
                            if (!Path.GetExtension(itemA).ToUpper().Equals(".XML") && !Path.GetExtension(itemA).ToUpper().Equals(".XLSX"))
                                continue;

                            if (!itemA.ToUpper().Contains("PROCESSADO") &&
                                !itemA.ToUpper().Contains("INVALIDO") &&
                                !itemA.ToUpper().Contains("CRDOWNLOAD") &&
                                !itemA.ToUpper().Contains("NAOIMPORTADO") &&
                                !itemA.Contains("~$") &&
                                (itemA.Contains(".xls") || itemA.Contains(".xml")))
                            {

                                if (_arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.IdEmpresa &&
                                                                   w.NomeArquivo == itemA).Count() == 0)
                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa
                                                                                          ,
                                        NomeArquivo = itemA
                                                                                          ,
                                        Acao = "R" // nao é mais excluido ou renomeado os arquivos
                                                                                          ,
                                        Empresa = _empresa.NomeAbreviado
                                                                                          ,
                                        DiretorioConfigurado = item.DiretorioDacte
                                    });
                            }
                        }
                        #endregion
                    }
                }

                Log _log = new Log();
                _log.IdUsuario = 1;
                _log.Descricao = "Iniciou importação arquivei Dacte empresa " + _empresa.NomeAbreviado;
                _log.Tela = "Principal - Arquivei";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }

                if (_processo8h)
                    _empresaProcessadaDacte8h.Add(itemE.IdEmpresa);
                else
                {
                    if (_processo10h)
                        _empresaProcessadaDacte10h.Add(itemE.IdEmpresa);
                    else
                    {
                        if (_processo12h)
                            _empresaProcessadaDacte12h.Add(itemE.IdEmpresa);
                        else
                        {
                            if (_processo14h)
                                _empresaProcessadaDacte14h.Add(itemE.IdEmpresa);
                            else
                            {
                                if (_processo16h)
                                    _empresaProcessadaDacte16h.Add(itemE.IdEmpresa);
                                else
                                    _empresaProcessadaDacte18h.Add(itemE.IdEmpresa);
                            }
                        }
                    }
                }
            }

            #region atributos
            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();

            _listaItensArquivei = new List<ItensArquivei>();
            _listaArquivei = new List<Arquivei>();

            int quantidadeArquivos = 0;
            int quantidadesCanceladas = 0;
            int quantidadeCartaCorrecao = 0;

            DateTime dataemissao = DateTime.MinValue;
            List<string> NFCanceladas = new List<string>();
            List<string> CartaCorrecao = new List<string>();

            #endregion

            #region Leitura/Gravação arquivo excel.
            int idArquivo = 1;
            ImportandoArquivei _importandoArquivo = null;
            _listaArquivosImportados = new List<ImportandoArquivei>();

            _xml = new List<string>();

            foreach (var itemE in _arquivosPorEmpresa.GroupBy(g => g.IdEmpresa))
            {
                quantidadeArquivos = 0;
                quantidadeNFCadastradas = 0;
                quantidadeProcessadoNovos = 0;
                quantidadeComErroLeitura = 0;
                quantidadesCanceladas = 0;
                quantidadeCartaCorrecao = 0;

                foreach (var item in _arquivosPorEmpresa.Where(w => !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                                    !w.NomeArquivo.Contains("~$") &&
                                                                    w.IdEmpresa == itemE.Key)
                                                        .OrderBy(o => o.IdEmpresa))
                {
                    _arquiveiJaRenomeado = false;

                    if (!Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XML") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    #region eventos de cancelamento e carta de correção

                    string idCte = "";
                    XmlSerializer ser = new XmlSerializer(typeof(procEventoCTe));
                    XmlTextReader reader = null;
                    try
                    {
                        reader = new XmlTextReader(item.NomeArquivo);
                        procEventoCTe nota = (procEventoCTe)ser.Deserialize(reader);
                        idCte = nota.eventoCTe.infEvento.Id;
                        reader.Close();
                    }
                    catch
                    {
                        
                    }

                    if (Path.GetFileName(item.NomeArquivo).StartsWith("ID") || idCte.StartsWith("ID"))
                    {
                        quantidadeArquivos++;

                        string _xmlTag = "";
                        try
                        {
                            reader = new XmlTextReader(item.NomeArquivo);
                            while (reader.Read())
                            {
                                switch (reader.NodeType)
                                {
                                    case XmlNodeType.Element:
                                        _xmlTag = _xmlTag + "<" + reader.Name + ">";
                                        break;
                                    case XmlNodeType.Text:
                                        _xmlTag = _xmlTag + reader.Value;
                                        break;
                                    case XmlNodeType.EndElement:
                                        _xmlTag = _xmlTag + "</" + reader.Name + ">";
                                        break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log _log = new Log();
                            _log.IdUsuario = 1;
                            _log.Descricao = "Arquivo " + item.NomeArquivo + " não foi importado. Erro ao ler o arquivo." + Environment.NewLine + ex.Message ;
                            _log.Tela = "Principal - Arquivei";

                            try
                            {
                                new LogBO().Gravar(_log);
                            }
                            catch { }

                            item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlNaoImportado");

                        }

                        _xml.Add(_xmlTag);
                        reader.Close();
                        string evento = "";
                        string chave = "";
                        try
                        {
                            int posIni = _xmlTag.IndexOf("<chCTe>") + 7;
                            int posFim = _xmlTag.IndexOf("</chCTe>");
                            chave = _xmlTag.Substring(posIni, posFim - posIni).Trim();

                            posIni = _xmlTag.IndexOf("<xEvento>") + 9;
                            posFim = _xmlTag.IndexOf("</xEvento>");

                            try
                            {
                                evento = _xmlTag.Substring(posIni, posFim - posIni).Trim();
                            }
                            catch
                            {
                                posIni = _xmlTag.IndexOf("<descEvento>") + 12;
                                posFim = _xmlTag.IndexOf("</descEvento>");

                                try
                                {
                                    evento = _xmlTag.Substring(posIni, posFim - posIni).Trim();
                                }
                                catch
                                {
                                    evento = "";
                                }
                            }
                        }
                        catch { }
                        Classes.Arquivei _arquiveiChaveCancelar = new ArquiveiBO().Consultar(item.IdEmpresa, chave, 0);

                        List<Arquivei> listaCancelar = new List<Arquivei>();
                        List<ItensArquivei> itensCartaCorrecao = new List<ItensArquivei>();

                        if (_arquiveiChaveCancelar.Existe)
                        {
                            if (evento.ToUpper().Contains("CANCEL"))
                            {
                                quantidadesCanceladas++;
                                _arquiveiChaveCancelar.Status = "Cancelada após importação";
                                listaCancelar.Add(_arquiveiChaveCancelar);
                                NFCanceladas.Add(_arquiveiChaveCancelar.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp")
                                            + _arquiveiChaveCancelar.DataEmissao.ToShortDateString()
                                            + "&nbsp"
                                            + _arquiveiChaveCancelar.ChaveDeAcesso);
                                new ArquiveiBO().GravarStatus(listaCancelar);
                            }
                            else
                            {
                                quantidadeCartaCorrecao++;
                                CartaCorrecao.Add(_arquiveiChaveCancelar.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp")
                                            + _arquiveiChaveCancelar.DataEmissao.ToShortDateString()
                                            + "&nbsp"
                                            + _arquiveiChaveCancelar.ChaveDeAcesso);
                                itensCartaCorrecao = new ArquiveiBO().Listar(_arquiveiChaveCancelar.Id, false);

                                itensCartaCorrecao.ForEach(u => u.CCe = "Chave da carta de correção " + idCte.Replace("ID",""));

                                if (!new ArquiveiBO().Gravar(itensCartaCorrecao))
                                    NFErros.Add(nomeArquivo);
                            }

                            _importandoArquivo = new ImportandoArquivei();
                            _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                            if (!_importandoArquivo.Existe)
                            {
                                _importandoArquivo.Arquivo = item.NomeArquivo;
                                _importandoArquivo.IdUsuario = Publicas._idUsuario;
                                _importandoArquivo.Importando = true;

                                new ArquiveiBO().GravarImportando(_importandoArquivo);

                                _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));
                                _listaArquivosImportados.Add(_importandoArquivo);

                                new ArquiveiBO().GravarImportando(_importandoArquivo);
                            }
                        }
                    }
                    #endregion
                }

                foreach (var item in _arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.Key &&
                                                        !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                        !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                        !w.NomeArquivo.Contains("~$"))
                                                        .OrderByDescending(o => o.NomeArquivo).OrderBy(o => o.IdEmpresa))
                {
                    _diretorio = item.DiretorioConfigurado;
                    if (!Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XML") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    quantidadeArquivos++;

                    _importandoArquivo = new ImportandoArquivei();
                    _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                    nomeArquivo = item.NomeArquivo;

                    if (_importandoArquivo.Existe)
                    {
                        try
                        {
                            quantidadeNFCadastradas++;
                            try
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessadoAnteriormente");
                            }
                            catch
                            {

                            }
                            _arquiveiJaRenomeado = true;
                            continue; // vai para o próximo arquivo
                        }
                        catch { }
                    }
                    else
                    {
                        _importandoArquivo.Arquivo = nomeArquivo;
                        _importandoArquivo.IdUsuario = Publicas._idUsuario;
                        _importandoArquivo.Importando = true;

                        new ArquiveiBO().GravarImportando(_importandoArquivo);

                       _importandoArquivo = new ArquiveiBO().ConsultarArquivo(nomeArquivo, Path.GetFileName(nomeArquivo));
                       _listaArquivosImportados.Add(_importandoArquivo);
                    }

                    if (nomeArquivo.ToLower().Contains("xml"))
                    {
                        notifyIcon1.BalloonTipText = "Importando Arquivei pelo arquivo " + Path.GetFileName(nomeArquivo) + " da Empresa " + item.Empresa + "...";
                        idArquivo = Arquivei_DacteXml(nomeArquivo, idArquivo, item.IdEmpresa, _importandoArquivo);

                        item.NomeArquivo = nomeArquivo;
                    }

                    #region Ação a ser tomada com o arquivo Arquivei
                    if (item.Acao == "E")
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                            }
                            else
                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                        }
                        catch (IOException )
                        {
                        }
                    }
                    else
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                {
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                                }
                            }
                            else
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                            }

                        }
                        catch
                        {
                        }
                    }
                    #endregion
                }

                #region Envia Email
                _empresa = new EmpresaBO().Consultar(itemE.Key);

                string[] _dadosEmail = new string[50];
                _dadosEmail[0] = quantidadeArquivos.ToString();
                _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                _dadosEmail[2] = _empresa.NomeAbreviado;
                if (NFNovas.Count() != 0)
                {
                    _dadosEmail[3] = "Numero Dacte".PadRight(13).Replace(" ", "&nbsp") 
                        + "Emissão".PadRight(11).Replace(" ", "&nbsp")
                        + "Chave de Acesso" + "</br>";
                    foreach (var item in NFNovas)
                    {
                        _dadosEmail[3] = _dadosEmail[3] + item + "</br>";
                    }
                }

                _dadosEmail[4] = quantidadeNFCadastradas.ToString();
                _dadosEmail[6] = quantidadeProcessadoNovos.ToString();
                _dadosEmail[7] = quantidadeComErroLeitura.ToString();
                _dadosEmail[8] = _diretorio;
                _dadosEmail[9] = quantidadesCanceladas.ToString();
                _dadosEmail[10] = quantidadeCartaCorrecao.ToString();

                if (NFCanceladas.Count() != 0)
                {
                    _dadosEmail[11] = "Numero Dacte".PadRight(13).Replace(" ", "&nbsp")
                        + "Emissão".PadRight(11).Replace(" ", "&nbsp")
                        + "Chave de Acesso" + "</br>";
                    foreach (var item in NFCanceladas)
                    {
                        _dadosEmail[11] = _dadosEmail[11] + item + "</br>";
                    }
                }

                if (CartaCorrecao.Count() != 0)
                {
                    _dadosEmail[12] = "Numero Dacte".PadRight(13).Replace(" ", "&nbsp")
                        + "Emissão".PadRight(11).Replace(" ", "&nbsp")
                        + "Chave de Acesso" + "</br>";
                    foreach (var item in CartaCorrecao)
                    {
                        _dadosEmail[12] = _dadosEmail[12] + item + "</br>";
                    }
                }

                if (NFErros.Count() != 0)
                {
                    _dadosEmail[13] = "Arquivo" + "</br>";
                    foreach (var item in NFErros)
                    {
                        _dadosEmail[13] = _dadosEmail[13] + item + "</br>";
                    }
                }
                string emailDestino = "";

                foreach (var itemU in _listaUsuarios.Where(w => w.AcessaEscrituracaoFiscal))
                {
                    emailDestino = emailDestino + itemU.Email + "; ";
                }

                if (NFErros.Count() != 0)
                    emailDestino = "fflopes@supportse.com.br;mdmunoz@supportse.com.br";


                if (quantidadeArquivos != 0)
                    Classes.Publicas.EnviarEmailArquivei(_dadosEmail, emailDestino, "Arquivei - DACTe Recebidos " + (NFErros.Count() != 0 ? "Com Erros" : ""), false);

                #endregion

                NFNovas.Clear();
                NFCanceladas.Clear();
                CartaCorrecao.Clear();
                NFErros.Clear();
            }

            if (_listaArquivei.Count != 0)
                new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei);

            foreach (var item in _listaArquivosImportados)
            {
                new ArquiveiBO().GravarImportando(item);
            }

            //Close();
            #endregion
        }

        private int Arquivei_DacteXml(string nomeArquivo, int idArquivo, int idEmpresa, ImportandoArquivei _importandoArquivo)
        {
            List<cteProc> _listaCte = new List<cteProc>();
            List<cteOSProc> _listaCteOs = new List<cteOSProc>();

            //_xml = new List<string>();

            if (nomeArquivo.ToLower().Contains("xml"))
            {

                XmlSerializer ser = new XmlSerializer(typeof(cteProc));
                XmlSerializer serOs = new XmlSerializer(typeof(cteOSProc));
                XmlSerializer serEvento = new XmlSerializer(typeof(procEventoCTe));

                try
                {
                    XmlTextReader reader = new XmlTextReader(nomeArquivo);

                    try
                    {
                        if (!Path.GetFileName(nomeArquivo).StartsWith("ID"))
                        {
                            cteProc nota = null;
                            cteOSProc notaOs = null;
                            procEventoCTe evento = null;

                            try
                            {
                                nota = (cteProc)ser.Deserialize(reader);
                            }
                            catch { }

                            if (nota != null && nota.CTe.infCte.ide.nCT != null)
                                _listaCte.Add(nota);
                            else
                            {
                                try
                                {
                                    notaOs = (cteOSProc)serOs.Deserialize(reader);
                                }
                                catch { }

                                if (notaOs != null && notaOs.CTeOS.infCte.ide.nCT != null)
                                    _listaCteOs.Add(notaOs);
                                else
                                {
                                    try
                                    {
                                        evento = (procEventoCTe)serEvento.Deserialize(reader);
                                    }
                                    catch
                                    {
                                        quantidadeComErroLeitura++;
                                        NFErros.Add(nomeArquivo);
                                        Log _log = new Log();
                                        _log.IdUsuario = 1;
                                        _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                                        _log.Tela = "Principal - Arquivei";

                                        try
                                        {
                                            new LogBO().Gravar(_log);
                                        }
                                        catch { }

                                        nomeArquivo = nomeArquivo.Replace("xml", "xmlNaoImportado");

                                        _listaArquivosImportados.Remove(_importandoArquivo);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        quantidadeComErroLeitura++;
                        NFErros.Add(nomeArquivo);
                        Log _log = new Log();
                        _log.IdUsuario = 1;
                        _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo." + Environment.NewLine + ex.Message;
                        _log.Tela = "Principal - Arquivei";

                        try
                        {
                            new LogBO().Gravar(_log);
                        }
                        catch { }

                        nomeArquivo = nomeArquivo.Replace("xml", "xmlInvalido");
                        _listaArquivosImportados.Remove(_importandoArquivo);
                    }

                    reader.Close();
                }
                catch
                {
                    quantidadeComErroLeitura++;
                    NFErros.Add(nomeArquivo);

                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                    _log.Tela = "Principal - Arquivei";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    nomeArquivo = nomeArquivo.Replace("xml", "xmlNaoImportado");
                    _listaArquivosImportados.Remove(_importandoArquivo);
                }
            }

            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();
            Arquivei _cadastrado = null;

            foreach (var item in _listaCte)
            {

                _arquivei.Id = idArquivo;

                _arquivei.ChaveDeAcesso = item.CTe.infCte.Id.Replace("CTe", "");

                _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.ChaveDeAcesso, 0);

                try
                {
                    _arquivei.NumeroNF = Convert.ToDecimal(item.CTe.infCte.ide.nCT);
                }
                catch { }

                if (_cadastrado.Existe)
                {
                    quantidadeNFCadastradas++;
                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "CTe " + _arquivei.NumeroNF + " com a Chave de Acesso " + _arquivei.ChaveDeAcesso + " já cadastrada para a empresa " + idEmpresa;
                    _log.Tela = "Principal - Arquivei - Dacte ja cadastrada";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                    
                    nomeArquivo = nomeArquivo.Replace("xml", "xmlCadastradoProcessado");
                    _arquiveiJaRenomeado = true;
                    break;
                }

                _arquivei.TipoDocto = "CTe";
                _arquivei.IdEmpresa = idEmpresa;
                _arquivei.NomeArquivo = nomeArquivo;

                // Cte
                _arquivei.RazaoSocialDestinatario = item.CTe.infCte.dest.xNome;
                _arquivei.RazaoSocialEmitente = item.CTe.infCte.emit.xNome;

                _arquivei.CNPJDestinatario = item.CTe.infCte.dest.Item;
                _arquivei.CNPJEmitente = item.CTe.infCte.emit.CNPJ;

                try
                {
                    _arquivei.IEDestinatario = item.CTe.infCte.dest.IE.ToString();
                }
                catch { }

                try
                {
                    _arquivei.IEEmitente = item.CTe.infCte.emit.IE.ToString();
                }
                catch { }

                try
                {
                    _arquivei.EnderecoDestinatario = item.CTe.infCte.dest.enderDest.xLgr;
                }
                catch { }
                try
                {
                    _arquivei.NumeroEndDestinatario = item.CTe.infCte.dest.enderDest.nro.ToString();
                }
                catch { }
                try
                {
                    _arquivei.BairroDestinatario = item.CTe.infCte.dest.enderDest.xBairro;
                }
                catch { }
                try
                {
                    _arquivei.CEPDestinatario = item.CTe.infCte.dest.enderDest.CEP.ToString();
                }
                catch { }

                try
                {
                    _arquivei.MunicipioDestino = item.CTe.infCte.ide.cMunFim;
                }
                catch { }
                try
                {
                    _arquivei.MunicipioOrigem = item.CTe.infCte.ide.cMunIni;
                }
                catch { }

                _arquivei.CNPJTomador = item.CTe.infCte.rem.Item;
                _arquivei.RazaoSocialTomador = item.CTe.infCte.rem.xNome;

                try
                {
                    _arquivei.IETomador = item.CTe.infCte.rem.IE.ToString();
                }
                catch { }

                try
                {
                    _arquivei.EnderecoTomador = item.CTe.infCte.rem.enderReme.xLgr;
                }
                catch { }
                try
                {
                    _arquivei.NumeroEndTomador = item.CTe.infCte.rem.enderReme.nro.ToString();
                }
                catch { }
                try
                {
                    _arquivei.BairroTomador = item.CTe.infCte.rem.enderReme.xBairro;
                }
                catch { }
                try
                {
                    _arquivei.CEPTomador = item.CTe.infCte.rem.enderReme.CEP.ToString();
                }
                catch { }

                try
                {
                    string dataEmissao = item.CTe.infCte.ide.dhEmi.Substring(0, 10);

                    _arquivei.DataEmissao = Convert.ToDateTime(dataEmissao);
                    //Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                }
                catch
                {
                    try
                    {
                        _arquivei.DataEmissao = Convert.ToDateTime(item.CTe.infCte.ide.dhEmi);
                    }
                    catch { }
                }


                _arquivei.ModeloNF = item.CTe.infCte.ide.mod.ToString().Replace("Item", "");
                _arquivei.Serie = item.CTe.infCte.ide.serie;
                _arquivei.Tipo = (item.CTe.infCte.ide.tpEmis == TCTeInfCteIdeTpEmis.Item1 ? "Saída" : "Entrada");

                _arquivei.Status = item.protCTe.infProt.xMotivo;

                foreach (var itemC in _xml)
                {
                    if (itemC.Contains(_arquivei.ChaveDeAcesso) && itemC.Contains("Cancelamento"))
                    {
                        quantidadesCanceladas++;
                        NFCanceladas.Add(_arquivei.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp") 
                            + _arquivei.DataEmissao.ToShortDateString()
                            + "&nbsp"
                            + _arquivei.ChaveDeAcesso);

                        _arquivei.Status = "Cancelada";
                    }
                }

                //_arquivei.Operacao = operacao;
                _arquivei.NaturezaOperacao = item.CTe.infCte.ide.natOp;

                try
                {
                    _arquivei.ValorProduto = Convert.ToDecimal(item.CTe.infCte.vPrest.vTPrest.Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorTotalNF = Convert.ToDecimal(item.CTe.infCte.vPrest.vTPrest.Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.BaseICMS = 0;
                }
                catch { }

                _arquivei.TipoArquivo = "XML";

                try
                {
                    _itens.CFOP = Convert.ToInt32(item.CTe.infCte.ide.CFOP);

                    TCTeInfCteInfCTeNorm _infCargas = ((TCTeInfCteInfCTeNorm)item.CTe.infCte.Item);
                    object _infNf = _infCargas.infDoc.Items;
                    string _aux = _infCargas.infCarga.xOutCat;

                    _arquivei.DadosAdicionais = item.CTe.infCte.compl.xObs + " " + _aux;

                    TImp _impostos = item.CTe.infCte.imp.ICMS;

                    object icms = _impostos.Item;

                    #region ICMS 
                    try
                    {

                        try
                        {
                            _itens.CST = ((TImpICMS00CST)((TImpICMS00)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = Convert.ToDecimal(((TImpICMS00)icms).vICMS.Replace(".", ","));
                            _itens.AliquotaICMS = Convert.ToDecimal(((TImpICMS00)icms).pICMS.Replace(".", ","));
                        }
                        catch { }

                        try
                        {
                            _itens.CST = ((TImpICMS20CST)((TImpICMS20)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = Convert.ToDecimal(((TImpICMS20)icms).vICMS.Replace(".", ","));
                            _itens.AliquotaICMS = Convert.ToDecimal(((TImpICMS20)icms).pICMS.Replace(".", ","));
                        }
                        catch { }

                        try
                        {
                            _itens.CST = ((TImpICMS45CST)((TImpICMS45)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = 0;
                            _itens.AliquotaICMS = 0;
                        }
                        catch { }

                        try
                        {
                            _itens.CST = ((TImpICMS45CST)((TImpICMS60)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = Convert.ToDecimal(((TImpICMS60)icms).vICMSSTRet.Replace(".", ","));
                            _itens.AliquotaICMS = Convert.ToDecimal(((TImpICMS60)icms).pICMSSTRet.Replace(".", ","));
                        }
                        catch { }

                        try
                        {
                            _itens.CST = ((TImpICMS90CST)((TImpICMS90)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = Convert.ToDecimal(((TImpICMS90)icms).vICMS.Replace(".", ","));
                            _itens.AliquotaICMS = Convert.ToDecimal(((TImpICMS90)icms).pICMS.Replace(".", ","));
                        }
                        catch { }

                        try
                        {
                            _itens.CST = ((TImpICMSOutraUFCST)((TImpICMSOutraUF)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = Convert.ToDecimal(((TImpICMSOutraUF)icms).vICMSOutraUF.Replace(".", ","));
                            _itens.AliquotaICMS = Convert.ToDecimal(((TImpICMSOutraUF)icms).pICMSOutraUF.Replace(".", ","));
                        }
                        catch { }

                        try
                        {
                            _itens.CST = ((TImpICMSSNCST)((TImpICMSSN)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = 0;
                            _itens.AliquotaICMS = 0;
                        }
                        catch { }
                    }
                    catch { }
                    #endregion

                    foreach (var itemC in _xml)
                    {
                        if (itemC.Contains(_arquivei.ChaveDeAcesso) && itemC.Contains("Carta de C"))
                        {
                            quantidadeCartaCorrecao++;
                            CartaCorrecao.Add(_arquivei.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp") 
                                + _arquivei.DataEmissao.ToShortDateString()
                                + "&nbsp"
                                + _arquivei.ChaveDeAcesso);
                            _itens.CCe = "carta de correção ";
                        }
                    }

                    _itens.ValorTotal = _arquivei.ValorProduto;
                    _itens.IdArquivei = _arquivei.Id;
                    _itens.CSTICMS = _itens.CST;
                    _listaItensArquivei.Add(_itens);

                }
                catch { }
                
                NFNovas.Add(_arquivei.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp") 
                    + _arquivei.DataEmissao.ToShortDateString()
                    + "&nbsp"
                    + _arquivei.ChaveDeAcesso);
                _listaArquivei.Add(_arquivei);
            }

            foreach (var item in _listaCteOs)
            {

                _arquivei.Id = idArquivo;

                _arquivei.ChaveDeAcesso = item.CTeOS.infCte.Id.Replace("CTe", "");

                _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.ChaveDeAcesso, 0);

                try
                {
                    _arquivei.NumeroNF = Convert.ToDecimal(item.CTeOS.infCte.ide.nCT);
                }
                catch { }

                if (_cadastrado.Existe)
                {
                    quantidadeNFCadastradas++;
                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "CTeOS " + _arquivei.NumeroNF + " com a Chave de Acesso " + _arquivei.ChaveDeAcesso + " já cadastrada para a empresa " + idEmpresa;
                    _log.Tela = "Principal - Arquivei - Dacte ja cadastrada";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    nomeArquivo = nomeArquivo.Replace("xml", "xmlCadastradoProcessado");
                    _arquiveiJaRenomeado = true;
                    break;
                }

                _arquivei.TipoDocto = "CTeOS";
                _arquivei.IdEmpresa = idEmpresa;
                _arquivei.NomeArquivo = nomeArquivo;

                // Cte
                _arquivei.RazaoSocialDestinatario = item.CTeOS.infCte.toma.xNome;
                _arquivei.RazaoSocialEmitente = item.CTeOS.infCte.emit.xNome;

                _arquivei.CNPJDestinatario = item.CTeOS.infCte.toma.Item;
                _arquivei.CNPJEmitente = item.CTeOS.infCte.emit.CNPJ;

                try
                {
                    _arquivei.IEDestinatario = item.CTeOS.infCte.toma.IE.ToString();
                }
                catch { }

                try
                {
                    _arquivei.IEEmitente = item.CTeOS.infCte.emit.IE.ToString();
                }
                catch { }

                try
                {
                    _arquivei.EnderecoDestinatario = item.CTeOS.infCte.toma.enderToma.xLgr;
                }
                catch { }
                try
                {
                    _arquivei.NumeroEndDestinatario = item.CTeOS.infCte.toma.enderToma.nro.ToString();
                }
                catch { }
                try
                {
                    _arquivei.BairroDestinatario = item.CTeOS.infCte.toma.enderToma.xBairro;
                }
                catch { }
                try
                {
                    _arquivei.CEPDestinatario = item.CTeOS.infCte.toma.enderToma.CEP.ToString();
                }
                catch { }

                try
                {
                    _arquivei.MunicipioDestino = item.CTeOS.infCte.ide.cMunFim;
                }
                catch { }
                try
                {
                    _arquivei.MunicipioOrigem = item.CTeOS.infCte.ide.cMunIni;
                }
                catch { }
                try
                {
                    string dataEmissao = item.CTeOS.infCte.ide.dhEmi.Substring(0, 10);

                    _arquivei.DataEmissao = Convert.ToDateTime(dataEmissao);
                    //Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                }
                catch
                {
                    try
                    {
                        _arquivei.DataEmissao = Convert.ToDateTime(item.CTeOS.infCte.ide.dhEmi);
                    }
                    catch { }
                }


                _arquivei.ModeloNF = item.CTeOS.infCte.ide.mod.ToString().Replace("Item", "");
                _arquivei.Serie = item.CTeOS.infCte.ide.serie;
                _arquivei.Tipo = (item.CTeOS.infCte.ide.tpEmis == TCTeOSInfCteIdeTpEmis.Item1 ? "Saída" : "Entrada");

                _arquivei.Status = item.protCTe.infProt.xMotivo;

                foreach (var itemC in _xml)
                {
                    if (itemC.Contains(_arquivei.ChaveDeAcesso) && itemC.Contains("Cancelamento"))
                    {
                        quantidadesCanceladas++;
                        NFCanceladas.Add(_arquivei.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp") 
                            + _arquivei.DataEmissao.ToShortDateString()
                            + "&nbsp"
                            + _arquivei.ChaveDeAcesso);

                        _arquivei.Status = "Cancelada";
                    }
                }

                //_arquivei.Operacao = operacao;
                _arquivei.NaturezaOperacao = item.CTeOS.infCte.ide.natOp;

                try
                {
                    _arquivei.ValorProduto = Convert.ToDecimal(item.CTeOS.infCte.vPrest.vTPrest.Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorTotalNF = Convert.ToDecimal(item.CTeOS.infCte.vPrest.vTPrest.Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.BaseICMS = 0;
                }
                catch { }

                _arquivei.TipoArquivo = "XML";

                try
                {
                    TCTeInfCteInfCTeNorm _infCargas = ((TCTeInfCteInfCTeNorm)item.CTeOS.infCte.Item);
                    object _infNf = _infCargas.infDoc.Items;
                    string _aux = _infCargas.infCarga.xOutCat;

                    _arquivei.DadosAdicionais = item.CTeOS.infCte.compl.xObs + " " + _aux;

                    TImpOS _impostos = item.CTeOS.infCte.imp.ICMS;

                    object icms = _impostos.Item;

                    #region ICMS 
                    try
                    {

                        try
                        {
                            _itens.CST = ((TImpOSICMS00CST)((TImpOSICMS00)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = Convert.ToDecimal(((TImpOSICMS00)icms).vICMS.Replace(".", ","));
                            _itens.AliquotaICMS = Convert.ToDecimal(((TImpOSICMS00)icms).pICMS.Replace(".", ","));
                        }
                        catch { }

                        try
                        {
                            _itens.CST = ((TImpOSICMS20CST)((TImpOSICMS20)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = Convert.ToDecimal(((TImpOSICMS20)icms).vICMS.Replace(".", ","));
                            _itens.AliquotaICMS = Convert.ToDecimal(((TImpOSICMS20)icms).pICMS.Replace(".", ","));
                        }
                        catch { }

                        try
                        {
                            _itens.CST = ((TImpOSICMS45CST)((TImpOSICMS45)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = 0;
                            _itens.AliquotaICMS = 0;
                        }
                        catch { }

                        try
                        {
                            _itens.CST = ((TImpOSICMS90CST)((TImpOSICMS90)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = Convert.ToDecimal(((TImpOSICMS90)icms).vICMS.Replace(".", ","));
                            _itens.AliquotaICMS = Convert.ToDecimal(((TImpOSICMS90)icms).pICMS.Replace(".", ","));
                        }
                        catch { }

                        try
                        {
                            _itens.CST = ((TImpOSICMSOutraUFCST)((TImpOSICMSOutraUF)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = Convert.ToDecimal(((TImpOSICMSOutraUF)icms).vICMSOutraUF.Replace(".", ","));
                            _itens.AliquotaICMS = Convert.ToDecimal(((TImpOSICMSOutraUF)icms).pICMSOutraUF.Replace(".", ","));
                        }
                        catch { }

                        try
                        {
                            _itens.CST = ((TImpOSICMSSNCST)((TImpOSICMSSN)icms).CST).ToString().Replace("Item", "");
                            _itens.ValorICMS = 0;
                            _itens.AliquotaICMS = 0;
                        }
                        catch { }
                    }
                    catch { }
                    #endregion

                    _itens.ValorTotal = _arquivei.ValorProduto;
                    _itens.IdArquivei = _arquivei.Id;
                    _itens.CSTICMS = _itens.CST;
                    _listaItensArquivei.Add(_itens);

                }
                catch { }

                NFNovas.Add(_arquivei.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp") 
                    +_arquivei.DataEmissao.ToShortDateString() + "&nbsp"
                    + _arquivei.ChaveDeAcesso);
                _listaArquivei.Add(_arquivei);
            }

            if (_listaArquivei.Count != 0)
            {
                quantidadeProcessadoNovos++;
                if (!new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei))
                    NFErros.Add(nomeArquivo);
            }
            _listaArquivei.Clear();
            _listaItensArquivei.Clear();

            idArquivo++;
            return idArquivo;
        }

        #endregion

        #region NFSe - por XML 

        private void XmlNotaServicoSP()

        {
            bool _processo8h = false;
            bool _processo10h = false;
            bool _processo12h = false;
            bool _processo14h = false;
            bool _processo16h = false;
            bool _processo18h = false;

            #region Horarios
            if (((DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).AddMinutes(30).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                 //(DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(8).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(10).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(12).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(14).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(16).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(18).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay)) ||
                (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)) // só de semana
            {
                _empresaProcessadaNFSe8h.Clear();
                _empresaProcessadaNFSe10h.Clear();
                _empresaProcessadaNFSe12h.Clear();
                _empresaProcessadaNFSe14h.Clear();
                _empresaProcessadaNFSe16h.Clear();
                _empresaProcessadaNFSe18h.Clear();
                return;
            }


            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(8).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                !_processo10h)
            {
                _processo8h = true;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSe10h.Clear();
                _empresaProcessadaNFSe12h.Clear();
                _empresaProcessadaNFSe14h.Clear();
                _empresaProcessadaNFSe16h.Clear();
                _empresaProcessadaNFSe18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(10).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                !_processo12h)
            {
                _processo8h = false;
                _processo10h = true;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSe8h.Clear();
                _empresaProcessadaNFSe12h.Clear();
                _empresaProcessadaNFSe14h.Clear();
                _empresaProcessadaNFSe16h.Clear();
                _empresaProcessadaNFSe18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(12).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                !_processo14h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = true;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSe8h.Clear();
                _empresaProcessadaNFSe10h.Clear();
                _empresaProcessadaNFSe14h.Clear();
                _empresaProcessadaNFSe16h.Clear();
                _empresaProcessadaNFSe18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(14).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                !_processo16h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = true;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSe8h.Clear();
                _empresaProcessadaNFSe10h.Clear();
                _empresaProcessadaNFSe12h.Clear();
                _empresaProcessadaNFSe16h.Clear();
                _empresaProcessadaNFSe18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(16).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                !_processo18h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = true;
                _processo18h = false;

                _empresaProcessadaNFSe8h.Clear();
                _empresaProcessadaNFSe10h.Clear();
                _empresaProcessadaNFSe12h.Clear();
                _empresaProcessadaNFSe14h.Clear();
                _empresaProcessadaNFSe18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(18).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay) &&
                !_processo8h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = true;

                _empresaProcessadaNFSe8h.Clear();
                _empresaProcessadaNFSe10h.Clear();
                _empresaProcessadaNFSe12h.Clear();
                _empresaProcessadaNFSe14h.Clear();
                _empresaProcessadaNFSe16h.Clear();
            }

            #endregion

            _listaParametrosArquivei = new ParametrosArquiveiBO().Listar();
            _listaItensParametros = new ItensParametrosArquiveiBO().Listar(0);
            _arquivosPorEmpresa = new List<ArquivosEmpresaArquivei>();
            _pastaDiaria = DateTime.Now.ToString().Replace("/", "").Replace(" ", "_").Replace(":", "");
            List<string> _listaArquivos = new List<string>();

            DirectoryInfo dirInfo;

            foreach (var itemE in _listaEmpresas) //.Where(w => w.IdEmpresa == 2))
            {
                if (_processo8h)
                {
                    if (_empresaProcessadaNFSe8h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo10h)
                {
                    if (_empresaProcessadaNFSe10h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo12h)
                {
                    if (_empresaProcessadaNFSe12h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo14h)
                {
                    if (_empresaProcessadaNFSe14h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo16h)
                {
                    if (_empresaProcessadaNFSe16h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                {
                    if (_empresaProcessadaNFSe18h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }

                _empresa = new EmpresaBO().Consultar(itemE.IdEmpresa);
                foreach (var item in _listaParametrosArquivei.Where(w => w.IdEmpresa == itemE.IdEmpresa && w.DiretorioNFSe != ""))
                {
                    dirInfo = new DirectoryInfo(item.DiretorioNFSe);
                    //dirInfo =  new DirectoryInfo(@"n:\arquivei\15698659000211\CTe\");

                    if (dirInfo.Exists)
                    {
                        #region Antigo
                        FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

                        if (files.Count() != 0) // configuraçao antiga
                        {
                            foreach (FileSystemInfo file in files)
                            {
                                if (!Path.GetExtension(file.FullName).ToUpper().Equals(".XML") && !Path.GetExtension(file.FullName).ToUpper().Equals(".XLSX"))
                                    continue;

                                if (!file.FullName.ToUpper().Contains("PROCESSADO") &&
                                    !file.FullName.ToUpper().Contains("INVALIDO") &&
                                    !file.FullName.ToUpper().Contains("CRDOWNLOAD") &&
                                    !file.FullName.ToUpper().Contains("NAOIMPORTADO") &&
                                   // file.FullName.ToUpper().Contains("PAULISTANA") &&
                                    !file.FullName.Contains("~$") &&
                                    (file.FullName.Contains(".xls") || file.FullName.Contains(".xml")))
                                {

                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa,
                                        NomeArquivo = file.FullName,
                                        Acao = "R", // nao é mais excluido ou renomeado os arquivos,
                                        Empresa = _empresa.NomeAbreviado,
                                        DiretorioConfigurado = item.DiretorioNFSe
                                    });
                                }
                            }
                        }
                        #endregion

                        #region Novo

                        _listaArquivos = Diretorio(item.DiretorioNFSe);
                        //_listaArquivos = Diretorio(@"n:\arquivei\15698659000211\CTe\");

                        foreach (var itemA in _listaArquivos)
                        {
                            if (!Path.GetExtension(itemA).ToUpper().Equals(".XML") && !Path.GetExtension(itemA).ToUpper().Equals(".XLSX"))
                                continue;

                            if (!itemA.ToUpper().Contains("PROCESSADO") &&
                                !itemA.ToUpper().Contains("INVALIDO") &&
                                !itemA.ToUpper().Contains("CRDOWNLOAD") &&
                                !itemA.ToUpper().Contains("NAOIMPORTADO") &&
                                //itemA.ToUpper().Contains("PAULISTANA") &&
                                !itemA.Contains("~$") &&
                                (itemA.Contains(".xls") || itemA.Contains(".xml")))
                            {

                                if (_arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.IdEmpresa &&
                                                                   w.NomeArquivo == itemA).Count() == 0)
                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa,
                                        NomeArquivo = itemA,
                                        Acao = "R", // nao é mais excluido ou renomeado os arquivos
                                        Empresa = _empresa.NomeAbreviado,
                                        DiretorioConfigurado = item.DiretorioNFSe
                                    });
                            }
                        }
                        #endregion
                    }
                }

                Log _log = new Log();
                _log.IdUsuario = 1;
                _log.Descricao = "Iniciou importação arquivei NFSe empresa " + _empresa.NomeAbreviado;
                _log.Tela = "Principal - Arquivei";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }

                if (_processo8h)
                    _empresaProcessadaNFSe8h.Add(itemE.IdEmpresa);
                else
                {
                    if (_processo10h)
                        _empresaProcessadaNFSe10h.Add(itemE.IdEmpresa);
                    else
                    {
                        if (_processo12h)
                            _empresaProcessadaNFSe12h.Add(itemE.IdEmpresa);
                        else
                        {
                            if (_processo14h)
                                _empresaProcessadaNFSe14h.Add(itemE.IdEmpresa);
                            else
                            {
                                if (_processo16h)
                                    _empresaProcessadaNFSe16h.Add(itemE.IdEmpresa);
                                else
                                    _empresaProcessadaNFSe18h.Add(itemE.IdEmpresa);
                            }
                        }
                    }
                }
            }

            #region atributos
            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();

            _listaItensArquivei = new List<ItensArquivei>();
            _listaArquivei = new List<Arquivei>();

            int quantidadeArquivos = 0;
            int quantidadesCanceladas = 0;
            int quantidadeCartaCorrecao = 0;

            DateTime dataemissao = DateTime.MinValue;
            List<string> NFCanceladas = new List<string>();
            List<string> CartaCorrecao = new List<string>();

            #endregion

            #region Leitura/Gravação arquivo excel.
            int idArquivo = 1;
            ImportandoArquivei _importandoArquivo = null;
            _listaArquivosImportados = new List<ImportandoArquivei>();

            _xml = new List<string>();

            foreach (var itemE in _arquivosPorEmpresa.GroupBy(g => g.IdEmpresa))
            {
                quantidadeArquivos = 0;
                quantidadeNFCadastradas = 0;
                quantidadeProcessadoNovos = 0;
                quantidadeComErroLeitura = 0;
                quantidadesCanceladas = 0;
                quantidadeCartaCorrecao = 0;

                foreach (var item in _arquivosPorEmpresa.Where(w => !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("GINFES") &&
                                                                    !w.NomeArquivo.Contains("~$") &&
                                                                 //   w.NomeArquivo.ToUpper().Contains("PAULISTANA") &&
                                                                    w.IdEmpresa == itemE.Key)
                                                        .OrderBy(o => o.IdEmpresa))
                {
                    _arquiveiJaRenomeado = false;

                    if (!Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XML") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    #region eventos de cancelamento e carta de correção

                    bool cancelada = false;
                    XmlSerializer ser = new XmlSerializer(typeof(tpNFe));
                    XmlTextReader reader = null;
                    tpNFe nota = new tpNFe();

                    try
                    {
                        reader = new XmlTextReader(item.NomeArquivo);
                        nota = (tpNFe)ser.Deserialize(reader);
                        string DatCancelamento = "";

                        try
                        {
                            DatCancelamento = nota.DataCancelamento.ToString().Substring(0, 10);

                            nota.DataCancelamento = Convert.ToDateTime(DatCancelamento);
                            //Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                        }
                        catch
                        {
                            try
                            {
                                nota.DataCancelamento = Convert.ToDateTime(nota.DataCancelamento.ToString());
                            }
                            catch { }
                        }

                        cancelada = nota.StatusNFe == tpStatusNFe.C;
                        reader.Close();
                    }
                    catch
                    {

                    }

                    if (cancelada)
                    {
                        quantidadeArquivos++;
                        
                        string chave = nota.ChaveNFe.CodigoVerificacao;
                        
                        Classes.Arquivei _arquiveiChaveCancelar = new ArquiveiBO().Consultar(item.IdEmpresa, chave, 0);

                        if (!_arquiveiChaveCancelar.Existe)
                            _arquiveiChaveCancelar = new ArquiveiBO().Consultar(item.IdEmpresa, nota.ChaveNFe.NumeroNFe.ToString(), nota.CPFCNPJPrestador.Item);

                        List<Arquivei> listaCancelar = new List<Arquivei>();
                        List<ItensArquivei> itensCartaCorrecao = new List<ItensArquivei>();

                        if (_arquiveiChaveCancelar.Existe)
                        {
                            
                                quantidadesCanceladas++;
                                _arquiveiChaveCancelar.Status = "Cancelada após importação";
                                _arquivei.DataCancelamento = nota.DataCancelamento;

                                listaCancelar.Add(_arquiveiChaveCancelar);
                            NFCanceladas.Add(_arquiveiChaveCancelar.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp")
                                            + _arquiveiChaveCancelar.DataEmissao.ToShortDateString()
                                            + "&nbsp"
                                            + _arquiveiChaveCancelar.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp")
                                            + _arquiveiChaveCancelar.ChaveDeAcesso);
                            new ArquiveiBO().GravarStatus(listaCancelar);
                            
                            _importandoArquivo = new ImportandoArquivei();
                            _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                            if (!_importandoArquivo.Existe)
                            {
                                _importandoArquivo.Arquivo = item.NomeArquivo;
                                _importandoArquivo.IdUsuario = Publicas._idUsuario;
                                _importandoArquivo.Importando = true;

                                new ArquiveiBO().GravarImportando(_importandoArquivo);

                                _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));
                                _listaArquivosImportados.Add(_importandoArquivo);

                                new ArquiveiBO().GravarImportando(_importandoArquivo);
                            }
                        }
                    }
                    #endregion
                }

                foreach (var item in _arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.Key &&
                                                        !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                        !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("GINFES") &&
                                                       // w.NomeArquivo.ToUpper().Contains("PAULISTANA") &&
                                                        !w.NomeArquivo.Contains("~$"))
                                                        .OrderByDescending(o => o.NomeArquivo).OrderBy(o => o.IdEmpresa))
                {
                    _diretorio = item.DiretorioConfigurado;
                    if (!Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XML") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    quantidadeArquivos++;

                    _importandoArquivo = new ImportandoArquivei();
                    _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                    nomeArquivo = item.NomeArquivo;

                    if (_importandoArquivo.Existe)
                    {
                        try
                        {
                            quantidadeNFCadastradas++;
                            try
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessadoAnteriormente");
                            }
                            catch
                            {

                            }
                            _arquiveiJaRenomeado = true;
                            continue; // vai para o próximo arquivo
                        }
                        catch { }
                    }
                    else
                    {
                        _importandoArquivo.Arquivo = nomeArquivo;
                        _importandoArquivo.IdUsuario = Publicas._idUsuario;
                        _importandoArquivo.Importando = true;

                        new ArquiveiBO().GravarImportando(_importandoArquivo);

                        _importandoArquivo = new ArquiveiBO().ConsultarArquivo(nomeArquivo, Path.GetFileName(nomeArquivo));
                        _listaArquivosImportados.Add(_importandoArquivo);
                    }

                    if (nomeArquivo.ToLower().Contains("xml"))
                    {
                        notifyIcon1.BalloonTipText = "Importando Arquivei pelo arquivo " + Path.GetFileName(nomeArquivo) + " da Empresa " + item.Empresa + "...";
                        idArquivo = XmlServicoSP(nomeArquivo, idArquivo, item.IdEmpresa, _importandoArquivo);

                        item.NomeArquivo = nomeArquivo;
                    }

                    #region Ação a ser tomada com o arquivo Arquivei
                    if (item.Acao == "E")
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                            }
                            else
                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                        }
                        catch (IOException)
                        {
                        }
                    }
                    else
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                {
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                                }
                            }
                            else
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                            }

                        }
                        catch
                        {
                        }
                    }
                    #endregion
                }

                #region Envia Email
                _empresa = new EmpresaBO().Consultar(itemE.Key);

                string[] _dadosEmail = new string[50];
                _dadosEmail[0] = quantidadeArquivos.ToString();
                _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                _dadosEmail[2] = _empresa.NomeAbreviado;
                if (NFNovas.Count() != 0)
                {
                    _dadosEmail[3] = "Numero NFSe".PadRight(12).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso"+
                         "</br>";
                    foreach (var item in NFNovas)
                    {
                        _dadosEmail[3] = _dadosEmail[3] + item + "</br>";
                    }
                }

                _dadosEmail[4] = quantidadeNFCadastradas.ToString();
                _dadosEmail[6] = quantidadeProcessadoNovos.ToString();
                _dadosEmail[7] = quantidadeComErroLeitura.ToString();
                _dadosEmail[8] = _diretorio;
                _dadosEmail[9] = quantidadesCanceladas.ToString();
                _dadosEmail[10] = quantidadeCartaCorrecao.ToString();

                if (NFCanceladas.Count() != 0)
                {
                    _dadosEmail[11] = "Numero NFSe".PadRight(12).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") +
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in NFCanceladas)
                    {
                        _dadosEmail[11] = _dadosEmail[11] + item + "</br>";
                    }
                }

                if (CartaCorrecao.Count() != 0)
                {
                    _dadosEmail[12] = "Numero NFSe".PadRight(12).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in CartaCorrecao)
                    {
                        _dadosEmail[12] = _dadosEmail[12] + item + "</br>";
                    }
                }

                string emailDestino = "";

                foreach (var itemU in _listaUsuarios.Where(w => w.AcessaEscrituracaoFiscal))
                {
                    emailDestino = emailDestino + itemU.Email + "; ";
                }

                if (NFErros.Count() != 0)
                    emailDestino = "fflopes@supportse.com.br;mdmunoz@supportse.com.br";


                if (quantidadeArquivos != 0)
                    Classes.Publicas.EnviarEmailArquivei(_dadosEmail, emailDestino, "Arquivei - NFSe Recebidas " + (NFErros.Count() != 0 ? "Com Erros" : ""), false, true);

                #endregion

                NFNovas.Clear();
                NFCanceladas.Clear();
                CartaCorrecao.Clear();
                NFErros.Clear();
            }

            if (_listaArquivei.Count != 0)
                new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei);

            foreach (var item in _listaArquivosImportados)
            {
                new ArquiveiBO().GravarImportando(item);
            }

            //Close();
            #endregion
        }

        private int XmlServicoSP(string nomeArquivo, int idArquivo, int idEmpresa, ImportandoArquivei _importandoArquivo)
        {
            List<tpNFe> _listaNfse = new List<tpNFe>();
            
            tpNFe nota = new tpNFe();

            if (nomeArquivo.ToLower().Contains("xml"))
            {
                XmlSerializer ser = new XmlSerializer(typeof(tpNFe));
               
                try
                {
                    XmlTextReader reader = new XmlTextReader(nomeArquivo);

                    try
                    {
                        if (!Path.GetFileName(nomeArquivo).StartsWith("ID"))
                        {
                           
                            try
                            {
                                nota = (tpNFe)ser.Deserialize(reader);
                            }
                            catch (Exception ex) {
                                
                            }

                            if (nota != null && nota.ChaveNFe.NumeroNFe.ToString() != null)
                                _listaNfse.Add(nota);
                            else
                            {
                                
                                quantidadeComErroLeitura++;
                                NFErros.Add(nomeArquivo);

                                Log _log = new Log();
                                _log.IdUsuario = 1;
                                _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                                _log.Tela = "Principal - Arquivei";

                                try
                                {
                                    new LogBO().Gravar(_log);
                                }
                                catch { }

                                nomeArquivo = nomeArquivo.Replace("xml", "xmlNaoImportado");
                                
                                _listaArquivosImportados.Remove(_importandoArquivo);                                    
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        quantidadeComErroLeitura++;
                        NFErros.Add(nomeArquivo);
                        Log _log = new Log();
                        _log.IdUsuario = 1;
                        _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo." + Environment.NewLine + ex.Message;
                        _log.Tela = "Principal - Arquivei";

                        try
                        {
                            new LogBO().Gravar(_log);
                        }
                        catch { }

                        nomeArquivo = nomeArquivo.Replace("xml", "xmlInvalido");
                        _listaArquivosImportados.Remove(_importandoArquivo);
                    }

                    reader.Close();
                }
                catch
                {
                    quantidadeComErroLeitura++;

                    NFErros.Add(nomeArquivo);
                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                    _log.Tela = "Principal - Arquivei";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    nomeArquivo = nomeArquivo.Replace("xml", "xmlNaoImportado");
                    _listaArquivosImportados.Remove(_importandoArquivo);
                }
            }
                        
            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();
            Arquivei _cadastrado = null;

            foreach (var item in _listaNfse)
            {

                _arquivei.Id = idArquivo;

                _arquivei.ChaveDeAcesso = item.ChaveNFe.CodigoVerificacao;
                _arquivei.CNPJEmitente = item.CPFCNPJPrestador.Item;

                try
                {
                    _arquivei.NumeroNF = Convert.ToDecimal(item.ChaveNFe.NumeroNFe);
                }
                catch { }

                if (_arquivei.ChaveDeAcesso != "")
                    _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.ChaveDeAcesso, 0);
                else
                    _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.NumeroNF.ToString(), _arquivei.CNPJEmitente);

                if (_cadastrado.Existe)
                {
                    quantidadeNFCadastradas++;
                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "NFSe " + _arquivei.NumeroNF + " com a Chave de Acesso " + _arquivei.ChaveDeAcesso + " já cadastrada para a empresa " + idEmpresa;
                    _log.Tela = "Principal - Arquivei - NFS-e ja cadastrada";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    nomeArquivo = nomeArquivo.Replace("xml", "xmlCadastradoProcessado");
                    _arquiveiJaRenomeado = true;
                    break;
                }

                _arquivei.IdEmpresa = idEmpresa;
                _arquivei.NomeArquivo = nomeArquivo;
                                
                _arquivei.RazaoSocialDestinatario = item.RazaoSocialTomador;
                _arquivei.RazaoSocialEmitente = item.RazaoSocialPrestador;

                _arquivei.CNPJDestinatario = item.CPFCNPJTomador.Item;
                
                try
                {
                    _arquivei.IEDestinatario = item.InscricaoEstadualTomador.ToString();
                }
                catch { }

                try
                {
                    _arquivei.IEEmitente = item.ChaveNFe.InscricaoPrestador.ToString();
                }
                catch { }

                try
                {
                    _arquivei.EnderecoDestinatario = item.EnderecoTomador.Logradouro;
                }
                catch { }
                try
                {
                    _arquivei.NumeroEndDestinatario = item.EnderecoTomador.NumeroEndereco.ToString();
                }
                catch { }
                try
                {
                    _arquivei.BairroDestinatario = item.EnderecoTomador.Bairro;
                }
                catch { }
                try
                {
                    _arquivei.CEPDestinatario = item.EnderecoTomador.CEP.ToString();
                }
                catch { }
                
                try
                {
                    string dataEmissao = item.DataEmissaoNFe.ToString().Substring(0, 10);

                    _arquivei.DataEmissao = Convert.ToDateTime(dataEmissao);
                    //Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                }
                catch
                {
                    try
                    {
                        _arquivei.DataEmissao = Convert.ToDateTime(item.DataEmissaoNFe.ToString());
                    }
                    catch { }
                }
                
                _arquivei.Tipo = "Entrada"; 
                _arquivei.Serie = "NFSe";
                _arquivei.Status = (item.StatusNFe.ToString() == "N" ? "Normal" :
                         (item.StatusNFe.ToString() == "C" ? "Cancelada" : "Enviada"));

                switch (item.OpcaoSimples.ToString())
                {
                    case "Item0" :
                        _arquivei.OpcaoSimples = "Não optante ";
                            break;
                    case "Item1":
                        _arquivei.OpcaoSimples = "Optante pelo Simples Federal 1% ";
                        break;
                    case "Item2":
                        _arquivei.OpcaoSimples = "Optante pelo Simples Federal 0,5% ";
                        break;
                    case "Item3":
                        _arquivei.OpcaoSimples = "Optante pelo Simples Municipal ";
                        break;
                    case "Item4":
                        _arquivei.OpcaoSimples = "Optante pelo Simples Nacional ";
                        break;
                }

                switch (item.TributacaoNFe.ToString())
                {
                    case "T": 
                        _arquivei.Tributacao = "Tributada em São Paulo";
                        break;
                    case "F":
                        _arquivei.Tributacao = "Tributada fora de São Paulo";
                        break;
                    case "A":
                        _arquivei.Tributacao = "Tributada em São Paulo, porém Isento";
                        break;
                    case "B":
                        _arquivei.Tributacao = "Tributada fora de São Paulo, porém Isento";
                        break;
                    case "D":
                        _arquivei.Tributacao = "Tributada em São Paulo com Isenção Parcial";
                        break;
                    case "M":
                        _arquivei.Tributacao = "Tributada em São Paulo, porém com indicação de Imunidade Subjetiva";
                        break;
                    case "N":
                        _arquivei.Tributacao = "Tributada fora São Paulo, porém com indicação de Imunidade Subjetiva";
                        break;
                    case "R":
                        _arquivei.Tributacao = "Tributada em São Paulo, porém com indicação de Imunidade Objetiva";
                        break;
                    case "S":
                        _arquivei.Tributacao = "Tributada fora de São Paulo, porém com indicação de Imunidade Objetiva";
                        break;
                    case "X":
                        _arquivei.Tributacao = "Tributada em São Paulo, porém com Exigibilidade Suspensa";
                        break;
                    case "V":
                        _arquivei.Tributacao = "Tributada fora de São Paulo, porém com Exigibilidade Suspensa";
                        break;
                    case "P":
                        _arquivei.Tributacao = "Exportação de Serviços";
                        break;
                }
                
                _arquivei.CodigoServico = item.CodigoServico.ToString();
                _arquivei.Discriminacao = item.Discriminacao;
                _arquivei.TipoDocto = "NFSe";
                
                try
                {
                    _arquivei.ValorServico = Convert.ToDecimal(item.ValorServicos.ToString().Replace(".", ","));
                    _arquivei.ValorTotalRecebido = Convert.ToDecimal(item.ValorTotalRecebido.ToString().Replace(".", ","));

                    if (_arquivei.ValorTotalRecebido > 0 && _arquivei.ValorServico < _arquivei.ValorTotalRecebido)
                        _arquivei.ValorServico = _arquivei.ValorTotalRecebido;
                }
                catch { }

                try
                {
                    _arquivei.ValorTotalNF = _arquivei.ValorServico;
                }
                catch { }

                try
                {
                    _arquivei.AliquotaServico = Convert.ToDecimal(item.AliquotaServicos.ToString().Replace(".", ",")) *100;
                }
                catch { }

                try
                {
                    _arquivei.ValorISS = Convert.ToDecimal(item.ValorISS.ToString().Replace(".", ","));
                }
                catch { }

                try
                {
                    _arquivei.ValorCredito = Convert.ToDecimal(item.ValorCredito.ToString().Replace(".", ","));
                }
                catch { }

                try
                {
                    _arquivei.ValorPis = Convert.ToDecimal(item.ValorPIS.ToString().Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorIR = Convert.ToDecimal(item.ValorIR.ToString().Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorCofins = Convert.ToDecimal(item.ValorCOFINS.ToString().Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorCSLL = Convert.ToDecimal(item.ValorCSLL.ToString().Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorINSS = Convert.ToDecimal(item.ValorINSS.ToString().Replace(".", ","));
                }
                catch { }

                _arquivei.ISSRetido = item.ISSRetido;

                _arquivei.TipoArquivo = "XML";

                if (!_arquivei.Status.ToUpper().Contains("CANC"))
                    NFNovas.Add(_arquivei.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") + 
                        _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                        _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                        _arquivei.ChaveDeAcesso);
                else
                    NFCanceladas.Add(_arquivei.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") + 
                        _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                        _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                        _arquivei.ChaveDeAcesso);

                _listaArquivei.Add(_arquivei);
            } 

            if (_listaArquivei.Count != 0)
            {
                quantidadeProcessadoNovos++;
                if (!new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei))
                    NFErros.Add(nomeArquivo);
            }
            _listaArquivei.Clear();
            _listaItensArquivei.Clear();

            idArquivo++;
            return idArquivo;
        }

        private void XmlNotaServicoGinfes()

        {
            bool _processo8h = false;
            bool _processo10h = false;
            bool _processo12h = false;
            bool _processo14h = false;
            bool _processo16h = false;
            bool _processo18h = false;

            #region Horarios
            if (((DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).AddMinutes(30).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                 //(DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(8).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(10).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(12).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(14).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(16).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(18).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay)) ||
                (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)) // só de semana 
            {
                _empresaProcessadaNFSeGinfes8h.Clear();
                _empresaProcessadaNFSeGinfes10h.Clear();
                _empresaProcessadaNFSeGinfes12h.Clear();
                _empresaProcessadaNFSeGinfes14h.Clear();
                _empresaProcessadaNFSeGinfes16h.Clear();
                _empresaProcessadaNFSeGinfes18h.Clear();
                return;
            }


            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(8).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                !_processo10h)
            {
                _processo8h = true;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeGinfes10h.Clear();
                _empresaProcessadaNFSeGinfes12h.Clear();
                _empresaProcessadaNFSeGinfes14h.Clear();
                _empresaProcessadaNFSeGinfes16h.Clear();
                _empresaProcessadaNFSeGinfes18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(10).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                !_processo12h)
            {
                _processo8h = false;
                _processo10h = true;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeGinfes8h.Clear();
                _empresaProcessadaNFSeGinfes12h.Clear();
                _empresaProcessadaNFSeGinfes14h.Clear();
                _empresaProcessadaNFSeGinfes16h.Clear();
                _empresaProcessadaNFSeGinfes18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(12).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                !_processo14h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = true;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeGinfes8h.Clear();
                _empresaProcessadaNFSeGinfes10h.Clear();
                _empresaProcessadaNFSeGinfes14h.Clear();
                _empresaProcessadaNFSeGinfes16h.Clear();
                _empresaProcessadaNFSeGinfes18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(14).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                !_processo16h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = true;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeGinfes8h.Clear();
                _empresaProcessadaNFSeGinfes10h.Clear();
                _empresaProcessadaNFSeGinfes12h.Clear();
                _empresaProcessadaNFSeGinfes16h.Clear();
                _empresaProcessadaNFSeGinfes18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(16).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                !_processo18h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = true;
                _processo18h = false;

                _empresaProcessadaNFSeGinfes8h.Clear();
                _empresaProcessadaNFSeGinfes10h.Clear();
                _empresaProcessadaNFSeGinfes12h.Clear();
                _empresaProcessadaNFSeGinfes14h.Clear();
                _empresaProcessadaNFSeGinfes18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(18).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay) &&
                !_processo8h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = true;

                _empresaProcessadaNFSeGinfes8h.Clear();
                _empresaProcessadaNFSeGinfes10h.Clear();
                _empresaProcessadaNFSeGinfes12h.Clear();
                _empresaProcessadaNFSeGinfes14h.Clear();
                _empresaProcessadaNFSeGinfes16h.Clear();
            }

            #endregion

            _listaParametrosArquivei = new ParametrosArquiveiBO().Listar();
            _listaItensParametros = new ItensParametrosArquiveiBO().Listar(0);
            _arquivosPorEmpresa = new List<ArquivosEmpresaArquivei>();
            _pastaDiaria = DateTime.Now.ToString().Replace("/", "").Replace(" ", "_").Replace(":", "");
            List<string> _listaArquivos = new List<string>();

            DirectoryInfo dirInfo;

            foreach (var itemE in _listaEmpresas) //.Where(w => w.IdEmpresa == 2))
            {
                if (_processo8h)
                {
                    if (_empresaProcessadaNFSeGinfes8h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo10h)
                {
                    if (_empresaProcessadaNFSeGinfes10h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo12h)
                {
                    if (_empresaProcessadaNFSeGinfes12h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo14h)
                {
                    if (_empresaProcessadaNFSeGinfes14h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo16h)
                {
                    if (_empresaProcessadaNFSeGinfes16h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                {
                    if (_empresaProcessadaNFSeGinfes18h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }

                _empresa = new EmpresaBO().Consultar(itemE.IdEmpresa);
                foreach (var item in _listaParametrosArquivei.Where(w => w.IdEmpresa == itemE.IdEmpresa && w.DiretorioNFSe != ""))
                {
                    dirInfo = new DirectoryInfo(item.DiretorioNFSe);
                    //dirInfo =  new DirectoryInfo(@"n:\arquivei\15698659000211\CTe\");

                    if (dirInfo.Exists)
                    {
                        #region Antigo
                        FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

                        if (files.Count() != 0) // configuraçao antiga
                        {
                            foreach (FileSystemInfo file in files)
                            {
                                if (!Path.GetExtension(file.FullName).ToUpper().Equals(".XML") && !Path.GetExtension(file.FullName).ToUpper().Equals(".XLSX"))
                                    continue;

                                if (!file.FullName.ToUpper().Contains("PROCESSADO") &&
                                    !file.FullName.ToUpper().Contains("INVALIDO") &&
                                    !file.FullName.ToUpper().Contains("CRDOWNLOAD") &&
                                    !file.FullName.ToUpper().Contains("NAOIMPORTADO") &&
                                    file.FullName.ToUpper().Contains("GINFES") &&
                                    !file.FullName.Contains("~$") &&
                                    (file.FullName.Contains(".xls") || file.FullName.Contains(".xml")))
                                {

                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa
                                                                                           ,
                                        NomeArquivo = file.FullName
                                                                                           ,
                                        Acao = "R"// nao é mais excluido ou renomeado os arquivos
                                                                                           ,
                                        Empresa = _empresa.NomeAbreviado
                                                                                           ,
                                        DiretorioConfigurado = item.DiretorioNFSe
                                    });
                                }
                            }
                        }
                        #endregion

                        #region Novo

                        _listaArquivos = Diretorio(item.DiretorioNFSe);
                        //_listaArquivos = Diretorio(@"n:\arquivei\15698659000211\CTe\");

                        foreach (var itemA in _listaArquivos)
                        {
                            if (!Path.GetExtension(itemA).ToUpper().Equals(".XML") && !Path.GetExtension(itemA).ToUpper().Equals(".XLSX"))
                                continue;

                            if (!itemA.ToUpper().Contains("PROCESSADO") &&
                                !itemA.ToUpper().Contains("INVALIDO") &&
                                !itemA.ToUpper().Contains("CRDOWNLOAD") &&
                                !itemA.ToUpper().Contains("NAOIMPORTADO") &&
                                itemA.ToUpper().Contains("GINFES") && //obrigatório
                                !itemA.Contains("~$") &&
                                (itemA.Contains(".xls") || itemA.Contains(".xml")))
                            {

                                if (_arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.IdEmpresa &&
                                                                   w.NomeArquivo == itemA).Count() == 0)
                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa
                                                                                          ,
                                        NomeArquivo = itemA
                                                                                          ,
                                        Acao = "R" // nao é mais excluido ou renomeado os arquivos
                                                                                          ,
                                        Empresa = _empresa.NomeAbreviado
                                                                                          ,
                                        DiretorioConfigurado = item.DiretorioNFSe
                                    });
                            }
                        }
                        #endregion
                    }
                }

                Log _log = new Log();
                _log.IdUsuario = 1;
                _log.Descricao = "Iniciou importação arquivei NFSe empresa " + _empresa.NomeAbreviado;
                _log.Tela = "Principal - Arquivei";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }

                if (_processo8h)
                    _empresaProcessadaNFSeGinfes8h.Add(itemE.IdEmpresa);
                else
                {
                    if (_processo10h)
                        _empresaProcessadaNFSeGinfes10h.Add(itemE.IdEmpresa);
                    else
                    {
                        if (_processo12h)
                            _empresaProcessadaNFSeGinfes12h.Add(itemE.IdEmpresa);
                        else
                        {
                            if (_processo14h)
                                _empresaProcessadaNFSeGinfes14h.Add(itemE.IdEmpresa);
                            else
                            {
                                if (_processo16h)
                                    _empresaProcessadaNFSeGinfes16h.Add(itemE.IdEmpresa);
                                else
                                    _empresaProcessadaNFSeGinfes18h.Add(itemE.IdEmpresa);
                            }
                        }
                    }
                }
            }

            #region atributos
            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();

            _listaItensArquivei = new List<ItensArquivei>();
            _listaArquivei = new List<Arquivei>();

            int quantidadeArquivos = 0;
            int quantidadesCanceladas = 0;
            int quantidadeCartaCorrecao = 0;

            DateTime dataemissao = DateTime.MinValue;
            List<string> NFCanceladas = new List<string>();
            List<string> CartaCorrecao = new List<string>();

            #endregion

            #region Leitura/Gravação arquivo excel.
            int idArquivo = 1;
            ImportandoArquivei _importandoArquivo = null;
            _listaArquivosImportados = new List<ImportandoArquivei>();

            _xml = new List<string>();

            foreach (var itemE in _arquivosPorEmpresa.GroupBy(g => g.IdEmpresa))
            {
                quantidadeArquivos = 0;
                quantidadeNFCadastradas = 0;
                quantidadeProcessadoNovos = 0;
                quantidadeComErroLeitura = 0;
                quantidadesCanceladas = 0;
                quantidadeCartaCorrecao = 0;

                foreach (var item in _arquivosPorEmpresa.Where(w => !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                                    w.NomeArquivo.ToUpper().Contains("GINFES") &&
                                                                    !w.NomeArquivo.Contains("~$") &&
                                                                    w.IdEmpresa == itemE.Key)
                                                        .OrderBy(o => o.IdEmpresa))
                {
                    _arquiveiJaRenomeado = false;

                    if (!Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XML") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    #region eventos de cancelamento e carta de correção

                    bool cancelada = false;
                    XmlSerializer ser = new XmlSerializer(typeof(tcNfse));
                    XmlTextReader reader = null;
                    tcNfse nota = new tcNfse();

                    try
                    {
                        reader = new XmlTextReader(item.NomeArquivo);
                        nota = (tcNfse)ser.Deserialize(reader);
                        string DatCancelamento = "";

                        /*
                        try
                        {
                            DatCancelamento = nota.DataCancelamento.ToString().Substring(0, 10);

                            nota.DataCancelamento = Convert.ToDateTime(DatCancelamento);
                            //Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                        }
                        catch
                        {
                            try
                            {
                                nota.DataCancelamento = Convert.ToDateTime(nota.DataCancelamento.ToString());
                            }
                            catch { }
                        }

                        cancelada = nota.StatusNFe == tpStatusNFe.C;
                        */
                        reader.Close();
                    }
                    catch
                    {

                    }

                    if (cancelada)
                    {
                        quantidadeArquivos++;

                        string chave = nota.InfNfse.CodigoVerificacao;

                        Classes.Arquivei _arquiveiChaveCancelar = new ArquiveiBO().Consultar(item.IdEmpresa, chave, 0);

                        if (!_arquiveiChaveCancelar.Existe)
                            _arquiveiChaveCancelar = new ArquiveiBO().Consultar(item.IdEmpresa, nota.InfNfse.Numero.ToString(), nota.InfNfse.PrestadorServico.IdentificacaoPrestador.Cnpj);

                        List<Arquivei> listaCancelar = new List<Arquivei>();
                        List<ItensArquivei> itensCartaCorrecao = new List<ItensArquivei>();

                        if (_arquiveiChaveCancelar.Existe)
                        {

                            quantidadesCanceladas++;
                            _arquiveiChaveCancelar.Status = "Cancelada após importação";
                            ///_arquivei.DataCancelamento = nota.DataCancelamento;

                            listaCancelar.Add(_arquiveiChaveCancelar);
                            NFCanceladas.Add(_arquiveiChaveCancelar.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") +
                        _arquiveiChaveCancelar.DataEmissao.ToShortDateString() + "&nbsp" +
                        _arquiveiChaveCancelar.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                        _arquiveiChaveCancelar.ChaveDeAcesso);
                            new ArquiveiBO().GravarStatus(listaCancelar);

                            _importandoArquivo = new ImportandoArquivei();
                            _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                            if (!_importandoArquivo.Existe)
                            {
                                _importandoArquivo.Arquivo = item.NomeArquivo;
                                _importandoArquivo.IdUsuario = Publicas._idUsuario;
                                _importandoArquivo.Importando = true;

                                new ArquiveiBO().GravarImportando(_importandoArquivo);

                                _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));
                                _listaArquivosImportados.Add(_importandoArquivo);

                                new ArquiveiBO().GravarImportando(_importandoArquivo);
                            }
                        }
                    }
                    #endregion
                }

                foreach (var item in _arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.Key &&
                                                        !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                        !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                        w.NomeArquivo.ToUpper().Contains("GINFES") &&
                                                        !w.NomeArquivo.Contains("~$"))
                                                        .OrderByDescending(o => o.NomeArquivo).OrderBy(o => o.IdEmpresa))
                {
                    _diretorio = item.DiretorioConfigurado;
                    if (!Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XML") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    quantidadeArquivos++;

                    _importandoArquivo = new ImportandoArquivei();
                    _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                    nomeArquivo = item.NomeArquivo;

                    if (_importandoArquivo.Existe)
                    {
                        try
                        {
                            quantidadeNFCadastradas++;
                            try
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessadoAnteriormente");
                            }
                            catch
                            {

                            }
                            _arquiveiJaRenomeado = true;
                            continue; // vai para o próximo arquivo
                        }
                        catch { }
                    }
                    else
                    {
                        _importandoArquivo.Arquivo = nomeArquivo;
                        _importandoArquivo.IdUsuario = Publicas._idUsuario;
                        _importandoArquivo.Importando = true;

                        new ArquiveiBO().GravarImportando(_importandoArquivo);

                        _importandoArquivo = new ArquiveiBO().ConsultarArquivo(nomeArquivo, Path.GetFileName(nomeArquivo));
                        _listaArquivosImportados.Add(_importandoArquivo);
                    }

                    if (nomeArquivo.ToLower().Contains("xml"))
                    {
                        notifyIcon1.BalloonTipText = "Importando Arquivei pelo arquivo " + Path.GetFileName(nomeArquivo) + " da Empresa " + item.Empresa + "...";
                        idArquivo = XmlServicoGinfes(nomeArquivo, idArquivo, item.IdEmpresa, _importandoArquivo);

                        item.NomeArquivo = nomeArquivo;
                    }

                    #region Ação a ser tomada com o arquivo Arquivei
                    if (item.Acao == "E")
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                            }
                            else
                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                        }
                        catch (IOException)
                        {
                        }
                    }
                    else
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                {
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                                }
                            }
                            else
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                            }

                        }
                        catch
                        {
                        }
                    }
                    #endregion
                }

                #region Envia Email
                _empresa = new EmpresaBO().Consultar(itemE.Key);

                string[] _dadosEmail = new string[50];
                _dadosEmail[0] = quantidadeArquivos.ToString();
                _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                _dadosEmail[2] = _empresa.NomeAbreviado;
                if (NFNovas.Count() != 0)
                {
                    _dadosEmail[3] = "Numero NFSe".PadRight(12).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in NFNovas)
                    {
                        _dadosEmail[3] = _dadosEmail[3] + item + "</br>";
                    }
                }

                _dadosEmail[4] = quantidadeNFCadastradas.ToString();
                _dadosEmail[6] = quantidadeProcessadoNovos.ToString();
                _dadosEmail[7] = quantidadeComErroLeitura.ToString();
                _dadosEmail[8] = _diretorio;
                _dadosEmail[9] = quantidadesCanceladas.ToString();
                _dadosEmail[10] = quantidadeCartaCorrecao.ToString();

                if (NFCanceladas.Count() != 0)
                {
                    _dadosEmail[11] = "Numero NFSe".PadRight(12).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in NFCanceladas)
                    {
                        _dadosEmail[11] = _dadosEmail[11] + item + "</br>";
                    }
                }

                if (CartaCorrecao.Count() != 0)
                {
                    _dadosEmail[12] = "Numero NFSe".PadRight(12).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in CartaCorrecao)
                    {
                        _dadosEmail[12] = _dadosEmail[12] + item + "</br>";
                    }
                }

                if (NFErros.Count() != 0)
                {
                    _dadosEmail[13] = "Arquivo" + "</br>";
                    foreach (var item in NFErros)
                    {
                        _dadosEmail[13] = _dadosEmail[13] + item + "</br>";
                    }
                }
                string emailDestino = "";

                foreach (var itemU in _listaUsuarios.Where(w => w.AcessaEscrituracaoFiscal))
                {
                    emailDestino = emailDestino + itemU.Email + "; ";
                }

                if (NFErros.Count() != 0)
                    emailDestino = "fflopes@supportse.com.br;mdmunoz@supportse.com.br";


                if (quantidadeArquivos != 0)
                    Classes.Publicas.EnviarEmailArquivei(_dadosEmail, emailDestino, "Arquivei - NFSe Recebidas " + (NFErros.Count() != 0 ? "Com Erros" : ""), false, true);

                #endregion

                NFNovas.Clear();
                NFCanceladas.Clear();
                CartaCorrecao.Clear();
                NFErros.Clear();
            }

            if (_listaArquivei.Count != 0)
                new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei);

            foreach (var item in _listaArquivosImportados)
            {
                new ArquiveiBO().GravarImportando(item);
            }

            //Close();
            #endregion
        }

        private int XmlServicoGinfes(string nomeArquivo, int idArquivo, int idEmpresa, ImportandoArquivei _importandoArquivo)
        {
            List<tcNfse> _listaNfse = new List<tcNfse>();

            tcNfse nota = new tcNfse();
                     
            if (nomeArquivo.ToLower().Contains("xml"))
            {
                XmlSerializer ser = new XmlSerializer(typeof(tcNfse));

                try
                {
                    XmlTextReader reader = new XmlTextReader(nomeArquivo);

                    try
                    {
                        if (!Path.GetFileName(nomeArquivo).StartsWith("ID"))
                        {

                            try
                            {
                                nota = (tcNfse)ser.Deserialize(reader);
                            }
                            catch (Exception ex)
                            {

                            }

                            if (nota != null && nota.InfNfse.Numero.ToString() != null)
                                _listaNfse.Add(nota);
                            else
                            {

                                quantidadeComErroLeitura++;
                                NFErros.Add(nomeArquivo);

                                Log _log = new Log();
                                _log.IdUsuario = 1;
                                _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                                _log.Tela = "Principal - Arquivei";

                                try
                                {
                                    new LogBO().Gravar(_log);
                                }
                                catch { }

                                nomeArquivo = nomeArquivo.Replace("xml", "xmlNaoImportado");

                                _listaArquivosImportados.Remove(_importandoArquivo);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        quantidadeComErroLeitura++;
                        NFErros.Add(nomeArquivo);

                        Log _log = new Log();
                        _log.IdUsuario = 1;
                        _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo." + Environment.NewLine + ex.Message;
                        _log.Tela = "Principal - Arquivei";

                        try
                        {
                            new LogBO().Gravar(_log);
                        }
                        catch { }

                        nomeArquivo = nomeArquivo.Replace("xml", "xmlInvalido");
                        _listaArquivosImportados.Remove(_importandoArquivo);
                    }

                    reader.Close();
                }
                catch
                {
                    quantidadeComErroLeitura++;
                    NFErros.Add(nomeArquivo);

                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                    _log.Tela = "Principal - Arquivei";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    nomeArquivo = nomeArquivo.Replace("xml", "xmlNaoImportado");
                    _listaArquivosImportados.Remove(_importandoArquivo);
                }
            }

            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();
            Arquivei _cadastrado = null;
                        
            foreach (var item in _listaNfse)
            {

                _arquivei.Id = idArquivo;

                _arquivei.ChaveDeAcesso = item.InfNfse.CodigoVerificacao;
                _arquivei.CNPJEmitente = item.InfNfse.PrestadorServico.IdentificacaoPrestador.Cnpj;

                try
                {
                    _arquivei.NumeroNF = Convert.ToDecimal(item.InfNfse.Numero);
                }
                catch { }
                
                if (_arquivei.ChaveDeAcesso != "")
                    _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.ChaveDeAcesso, 0);
                else
                    _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.NumeroNF.ToString(), _arquivei.CNPJEmitente);


                if (_cadastrado.Existe)
                {
                    quantidadeNFCadastradas++;
                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "NFSe " + _arquivei.NumeroNF + " com a Chave de Acesso " + _arquivei.ChaveDeAcesso + " já cadastrada para a empresa " + idEmpresa;
                    _log.Tela = "Principal - Arquivei - NFS-e ja cadastrada";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    nomeArquivo = nomeArquivo.Replace("xml", "xmlCadastradoProcessado");
                    _arquiveiJaRenomeado = true;
                    break;
                }

                _arquivei.IdEmpresa = idEmpresa;
                _arquivei.NomeArquivo = nomeArquivo;

                _arquivei.RazaoSocialDestinatario = item.InfNfse.TomadorServico.RazaoSocial;
                _arquivei.RazaoSocialEmitente = item.InfNfse.PrestadorServico.RazaoSocial;

                _arquivei.CNPJDestinatario = item.InfNfse.TomadorServico.IdentificacaoTomador.CpfCnpj.Item;
                
                try
                {
                    _arquivei.EnderecoDestinatario = item.InfNfse.TomadorServico.Endereco.Endereco;
                }
                catch { }
                try
                {
                    _arquivei.NumeroEndDestinatario = item.InfNfse.TomadorServico.Endereco.Numero.ToString();
                }
                catch { }
                try
                {
                    _arquivei.BairroDestinatario = item.InfNfse.TomadorServico.Endereco.Bairro;
                }
                catch { }
                try
                {
                    _arquivei.CEPDestinatario = item.InfNfse.TomadorServico.Endereco.Cep.ToString();
                }
                catch { }

                try
                {
                    string dataEmissao = item.InfNfse.DataEmissao.ToString().Substring(0, 10);

                    _arquivei.DataEmissao = Convert.ToDateTime(dataEmissao);
                    //Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                }
                catch
                {
                    try
                    {
                        _arquivei.DataEmissao = Convert.ToDateTime(item.InfNfse.DataEmissao.ToString());
                    }
                    catch { }
                }
                
                _arquivei.Tipo = "Entrada";
                _arquivei.Serie = "NFSe";                

                _arquivei.CodigoServico = item.InfNfse.Servico.CodigoTributacaoMunicipio;
                _arquivei.Discriminacao = item.InfNfse.Servico.Discriminacao;
                _arquivei.TipoDocto = "NFSe";

                try
                {
                    _arquivei.ValorServico = Convert.ToDecimal(item.InfNfse.Servico.Valores.ValorServicos.ToString().Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorTotalNF = _arquivei.ValorServico;
                }
                catch { }

                try
                {
                    _arquivei.AliquotaServico = Convert.ToDecimal(item.InfNfse.Servico.Valores.Aliquota.ToString().Replace(".", ","));
                }
                catch { }

                try
                {
                    _arquivei.ValorISS = Convert.ToDecimal(item.InfNfse.Servico.Valores.ValorIss.ToString().Replace(".", ","));
                }
                catch { }

                try
                {
                    _arquivei.ValorPis = Convert.ToDecimal(item.InfNfse.Servico.Valores.ValorPis.ToString().Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorIR = Convert.ToDecimal(item.InfNfse.Servico.Valores.ValorIr.ToString().Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorCofins = Convert.ToDecimal(item.InfNfse.Servico.Valores.ValorCofins.ToString().Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorCSLL = Convert.ToDecimal(item.InfNfse.Servico.Valores.ValorCsll.ToString().Replace(".", ","));
                }
                catch { }
                try
                {
                    _arquivei.ValorINSS = Convert.ToDecimal(item.InfNfse.Servico.Valores.ValorInss.ToString().Replace(".", ","));
                }
                catch { }
                
                _arquivei.ISSRetido = item.InfNfse.Servico.Valores.IssRetido.ToString() == "1";
                _arquivei.OpcaoSimples = (item.InfNfse.OptanteSimplesNacional.ToString() == "1" ? "Optante pelo Simples Nacional" : "Não optante ");
                _arquivei.TipoArquivo = "XML";
                _arquivei.Status = "Normal";

                switch (item.InfNfse.NaturezaOperacao.ToString())
                {
                    case "1":
                        _arquivei.Tributacao = "Tributação no municipal";
                        break;
                    case "2":
                        _arquivei.Tributacao = "Tributada fora do municipio";
                        break;
                    case "3":
                        _arquivei.Tributacao = "Isento";
                        break;
                    case "4":
                        _arquivei.Tributacao = "Imune";
                        break;
                    case "5":
                        _arquivei.Tributacao = "Exigibilidade suspensa por decisão judicial";
                        break;
                    case "6":
                        _arquivei.Tributacao = "Exigibilidade suspensa por prodecimento administrativo";
                        break;
                    
                }

                if (!_arquivei.Status.ToUpper().Contains("CANC"))
                     NFNovas.Add(_arquivei.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") + 
                        _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                        _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                        _arquivei.ChaveDeAcesso);
                else
                    NFCanceladas.Add(_arquivei.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") + 
                        _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                        _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                        _arquivei.ChaveDeAcesso);

                _listaArquivei.Add(_arquivei);
            }
    
            if (_listaArquivei.Count != 0)
            {
                quantidadeProcessadoNovos++;
                if (!new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei))
                    NFErros.Add(nomeArquivo);
            }
            _listaArquivei.Clear();
            _listaItensArquivei.Clear();

            idArquivo++;
            return idArquivo;
        }
        
        private void XmlNotaServicoCampinas()

        {
            bool _processo8h = false;
            bool _processo10h = false;
            bool _processo12h = false;
            bool _processo14h = false;
            bool _processo16h = false;
            bool _processo18h = false;

            #region Horarios
            if (((DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).AddMinutes(30).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                 //(DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(8).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(10).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(12).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(14).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(16).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(18).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay)) ||
                (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)) // só de semana 
            {
                _empresaProcessadaNFSeCampinas8h.Clear();
                _empresaProcessadaNFSeCampinas10h.Clear();
                _empresaProcessadaNFSeCampinas12h.Clear();
                _empresaProcessadaNFSeCampinas14h.Clear();
                _empresaProcessadaNFSeCampinas16h.Clear();
                _empresaProcessadaNFSeCampinas18h.Clear();
                return;
            }


            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(8).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                !_processo10h)
            {
                _processo8h = true;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeCampinas10h.Clear();
                _empresaProcessadaNFSeCampinas12h.Clear();
                _empresaProcessadaNFSeCampinas14h.Clear();
                _empresaProcessadaNFSeCampinas16h.Clear();
                _empresaProcessadaNFSeCampinas18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(10).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                !_processo12h)
            {
                _processo8h = false;
                _processo10h = true;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeCampinas8h.Clear();
                _empresaProcessadaNFSeCampinas12h.Clear();
                _empresaProcessadaNFSeCampinas14h.Clear();
                _empresaProcessadaNFSeCampinas16h.Clear();
                _empresaProcessadaNFSeCampinas18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(12).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                !_processo14h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = true;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeCampinas8h.Clear();
                _empresaProcessadaNFSeCampinas10h.Clear();
                _empresaProcessadaNFSeCampinas14h.Clear();
                _empresaProcessadaNFSeCampinas16h.Clear();
                _empresaProcessadaNFSeCampinas18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(14).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                !_processo16h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = true;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeCampinas8h.Clear();
                _empresaProcessadaNFSeCampinas10h.Clear();
                _empresaProcessadaNFSeCampinas12h.Clear();
                _empresaProcessadaNFSeCampinas16h.Clear();
                _empresaProcessadaNFSeCampinas18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(16).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                !_processo18h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = true;
                _processo18h = false;

                _empresaProcessadaNFSeCampinas8h.Clear();
                _empresaProcessadaNFSeCampinas10h.Clear();
                _empresaProcessadaNFSeCampinas12h.Clear();
                _empresaProcessadaNFSeCampinas14h.Clear();
                _empresaProcessadaNFSeCampinas18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(18).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay) &&
                !_processo8h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = true;

                _empresaProcessadaNFSeCampinas8h.Clear();
                _empresaProcessadaNFSeCampinas10h.Clear();
                _empresaProcessadaNFSeCampinas12h.Clear();
                _empresaProcessadaNFSeCampinas14h.Clear();
                _empresaProcessadaNFSeCampinas16h.Clear();
            }

            #endregion

            _listaParametrosArquivei = new ParametrosArquiveiBO().Listar();
            _listaItensParametros = new ItensParametrosArquiveiBO().Listar(0);
            _arquivosPorEmpresa = new List<ArquivosEmpresaArquivei>();
            _pastaDiaria = DateTime.Now.ToString().Replace("/", "").Replace(" ", "_").Replace(":", "");
            List<string> _listaArquivos = new List<string>();

            DirectoryInfo dirInfo;

            foreach (var itemE in _listaEmpresas) //.Where(w => w.IdEmpresa == 2))
            {
                if (_processo8h)
                {
                    if (_empresaProcessadaNFSeCampinas8h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo10h)
                {
                    if (_empresaProcessadaNFSeCampinas10h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo12h)
                {
                    if (_empresaProcessadaNFSeCampinas12h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo14h)
                {
                    if (_empresaProcessadaNFSeCampinas14h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo16h)
                {
                    if (_empresaProcessadaNFSeCampinas16h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                {
                    if (_empresaProcessadaNFSeCampinas18h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }

                _empresa = new EmpresaBO().Consultar(itemE.IdEmpresa);
                foreach (var item in _listaParametrosArquivei.Where(w => w.IdEmpresa == itemE.IdEmpresa && w.DiretorioNFSe != ""))
                {
                    dirInfo = new DirectoryInfo(item.DiretorioNFSe);
                    //dirInfo =  new DirectoryInfo(@"n:\arquivei\15698659000211\CTe\");

                    if (dirInfo.Exists)
                    {
                        #region Antigo
                        FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

                        if (files.Count() != 0) // configuraçao antiga
                        {
                            foreach (FileSystemInfo file in files)
                            {
                                if (!Path.GetExtension(file.FullName).ToUpper().Equals(".XML") && !Path.GetExtension(file.FullName).ToUpper().Equals(".XLSX"))
                                    continue;

                                if (!file.FullName.ToUpper().Contains("PROCESSADO") &&
                                    !file.FullName.ToUpper().Contains("INVALIDO") &&
                                    !file.FullName.ToUpper().Contains("CRDOWNLOAD") &&
                                    !file.FullName.ToUpper().Contains("NAOIMPORTADO") &&
                                    !file.FullName.ToUpper().Contains("GINFES") &&
                                    !file.FullName.ToUpper().Contains("PAULISTANA") &&
                                    !file.FullName.ToUpper().Contains("TAUBATE") &&
                                    file.FullName.ToUpper().Contains("CAMPINAS") &&
                                    !file.FullName.Contains("~$") &&
                                    (file.FullName.Contains(".xls") || file.FullName.Contains(".xml")))
                                {

                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa
                                                                                           ,
                                        NomeArquivo = file.FullName
                                                                                           ,
                                        Acao = "R"// nao é mais excluido ou renomeado os arquivos
                                                                                           ,
                                        Empresa = _empresa.NomeAbreviado
                                                                                           ,
                                        DiretorioConfigurado = item.DiretorioNFSe
                                    });
                                }
                            }
                        }
                        #endregion

                        #region Novo

                        _listaArquivos = Diretorio(item.DiretorioNFSe);
                        //_listaArquivos = Diretorio(@"n:\arquivei\15698659000211\CTe\");

                        foreach (var itemA in _listaArquivos)
                        {
                            if (!Path.GetExtension(itemA).ToUpper().Equals(".XML") && !Path.GetExtension(itemA).ToUpper().Equals(".XLSX"))
                                continue;

                            if (!itemA.ToUpper().Contains("PROCESSADO") &&
                                !itemA.ToUpper().Contains("INVALIDO") &&
                                !itemA.ToUpper().Contains("CRDOWNLOAD") &&
                                !itemA.ToUpper().Contains("NAOIMPORTADO") &&
                                !itemA.ToUpper().Contains("GINFES") &&
                                !itemA.ToUpper().Contains("PAULISTANA") &&
                                !itemA.ToUpper().Contains("TAUBATE") &&
                                 itemA.ToUpper().Contains("CAMPINAS") &&
                                !itemA.Contains("~$") &&
                                (itemA.Contains(".xls") || itemA.Contains(".xml")))
                            {

                                if (_arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.IdEmpresa &&
                                                                   w.NomeArquivo == itemA).Count() == 0)
                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa
                                                                                          ,
                                        NomeArquivo = itemA
                                                                                          ,
                                        Acao = "R" // nao é mais excluido ou renomeado os arquivos
                                                                                          ,
                                        Empresa = _empresa.NomeAbreviado
                                                                                          ,
                                        DiretorioConfigurado = item.DiretorioNFSe
                                    });
                            }
                        }
                        #endregion
                    }
                }

                Log _log = new Log();
                _log.IdUsuario = 1;
                _log.Descricao = "Iniciou importação arquivei NFSe empresa " + _empresa.NomeAbreviado;
                _log.Tela = "Principal - Arquivei";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }

                if (_processo8h)
                    _empresaProcessadaNFSeCampinas8h.Add(itemE.IdEmpresa);
                else
                {
                    if (_processo10h)
                        _empresaProcessadaNFSeCampinas10h.Add(itemE.IdEmpresa);
                    else
                    {
                        if (_processo12h)
                            _empresaProcessadaNFSeCampinas12h.Add(itemE.IdEmpresa);
                        else
                        {
                            if (_processo14h)
                                _empresaProcessadaNFSeCampinas14h.Add(itemE.IdEmpresa);
                            else
                            {
                                if (_processo16h)
                                    _empresaProcessadaNFSeCampinas16h.Add(itemE.IdEmpresa);
                                else
                                    _empresaProcessadaNFSeCampinas18h.Add(itemE.IdEmpresa);
                            }
                        }
                    }
                }
            }

            #region atributos
            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();

            _listaItensArquivei = new List<ItensArquivei>();
            _listaArquivei = new List<Arquivei>();

            int quantidadeArquivos = 0;
            int quantidadesCanceladas = 0;
            int quantidadeCartaCorrecao = 0;

            DateTime dataemissao = DateTime.MinValue;
            List<string> NFCanceladas = new List<string>();
            List<string> CartaCorrecao = new List<string>();

            #endregion

            #region Leitura/Gravação arquivo excel.
            int idArquivo = 1;
            ImportandoArquivei _importandoArquivo = null;
            _listaArquivosImportados = new List<ImportandoArquivei>();

            _xml = new List<string>();

            foreach (var itemE in _arquivosPorEmpresa.GroupBy(g => g.IdEmpresa))
            {
                quantidadeArquivos = 0;
                quantidadeNFCadastradas = 0;
                quantidadeProcessadoNovos = 0;
                quantidadeComErroLeitura = 0;
                quantidadesCanceladas = 0;
                quantidadeCartaCorrecao = 0;

                foreach (var item in _arquivosPorEmpresa.Where(w => !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("GINFES") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("PAULISTANA") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("TAUBATE") &&
                                                                    w.NomeArquivo.ToUpper().Contains("CAMPINAS") &&
                                                                    !w.NomeArquivo.Contains("~$") &&
                                                                    w.IdEmpresa == itemE.Key)
                                                        .OrderBy(o => o.IdEmpresa))
                {
                    _arquiveiJaRenomeado = false;

                    if (!Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XML") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    #region eventos de cancelamento e carta de correção

                    bool cancelada = false;
                    XmlSerializer ser = new XmlSerializer(typeof(NOTAS_FISCAIS));
                    XmlTextReader reader = null;
                    NOTAS_FISCAIS nota = new NOTAS_FISCAIS();

                    try
                    {
                        reader = new XmlTextReader(item.NomeArquivo);
                        nota = (NOTAS_FISCAIS)ser.Deserialize(reader);
                        string DatCancelamento = "";

                        foreach (var itemN in nota.NOTA_FISCAL)
                        {
                            try
                            {
                                DatCancelamento = itemN.DATA_HORA_CANCELAMENTO.ToString().Substring(0, 10);
                                
                            }
                            catch
                            {
                            }

                            cancelada = itemN.SITUACAO_NF.ToUpper().Contains("CANCE");


                            if (cancelada)
                            {
                                quantidadeArquivos++;

                                string chave = itemN.CODIGO_VERIFICACAO;

                                Classes.Arquivei _arquiveiChaveCancelar = new ArquiveiBO().Consultar(item.IdEmpresa, chave, 0);

                                if (!_arquiveiChaveCancelar.Existe)
                                    _arquiveiChaveCancelar = new ArquiveiBO().Consultar(item.IdEmpresa, itemN.NUM_NOTA.ToString(), itemN.PRESTADOR_CPF_CNPJ.ToString());

                                List<Arquivei> listaCancelar = new List<Arquivei>();
                                List<ItensArquivei> itensCartaCorrecao = new List<ItensArquivei>();

                                if (_arquiveiChaveCancelar.Existe)
                                {

                                    quantidadesCanceladas++;
                                    _arquiveiChaveCancelar.Status = "Cancelada após importação";
                                    try
                                    {
                                        _arquivei.DataCancelamento = Convert.ToDateTime(DatCancelamento);
                                    }
                                    catch { }

                                    listaCancelar.Add(_arquiveiChaveCancelar);
                                    NFCanceladas.Add(_arquiveiChaveCancelar.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") +
                                            _arquiveiChaveCancelar.DataEmissao.ToShortDateString() + "&nbsp" +
                                            _arquiveiChaveCancelar.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                            _arquiveiChaveCancelar.ChaveDeAcesso);
                                    new ArquiveiBO().GravarStatus(listaCancelar);

                                    _importandoArquivo = new ImportandoArquivei();
                                    _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                                    if (!_importandoArquivo.Existe)
                                    {
                                        _importandoArquivo.Arquivo = item.NomeArquivo;
                                        _importandoArquivo.IdUsuario = Publicas._idUsuario;
                                        _importandoArquivo.Importando = true;

                                        new ArquiveiBO().GravarImportando(_importandoArquivo);

                                        _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));
                                        _listaArquivosImportados.Add(_importandoArquivo);

                                        new ArquiveiBO().GravarImportando(_importandoArquivo);
                                    }
                                }
                            }
                        }

                        reader.Close();
                    }
                    catch
                    {

                    }

                    #endregion
                }

                foreach (var item in _arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.Key &&
                                                        !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                        !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("GINFES") &&
                                                        !w.NomeArquivo.ToUpper().Contains("PAULISTANA") &&
                                                        !w.NomeArquivo.ToUpper().Contains("TAUBATE") &&
                                                         w.NomeArquivo.ToUpper().Contains("CAMPINAS") &&
                                                        !w.NomeArquivo.Contains("~$"))
                                                        .OrderByDescending(o => o.NomeArquivo).OrderBy(o => o.IdEmpresa))
                {
                    _diretorio = item.DiretorioConfigurado;
                    if (!Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XML") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    quantidadeArquivos++;

                    _importandoArquivo = new ImportandoArquivei();
                    _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                    nomeArquivo = item.NomeArquivo;

                    if (_importandoArquivo.Existe)
                    {
                        try
                        {
                            quantidadeNFCadastradas++;
                            try
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessadoAnteriormente");
                            }
                            catch
                            {

                            }
                            _arquiveiJaRenomeado = true;
                            continue; // vai para o próximo arquivo
                        }
                        catch { }
                    }
                    else
                    {
                        _importandoArquivo.Arquivo = nomeArquivo;
                        _importandoArquivo.IdUsuario = Publicas._idUsuario;
                        _importandoArquivo.Importando = true;

                        new ArquiveiBO().GravarImportando(_importandoArquivo);

                        _importandoArquivo = new ArquiveiBO().ConsultarArquivo(nomeArquivo, Path.GetFileName(nomeArquivo));
                        _listaArquivosImportados.Add(_importandoArquivo);
                    }

                    if (nomeArquivo.ToLower().Contains("xml"))
                    {
                        notifyIcon1.BalloonTipText = "Importando Arquivei pelo arquivo " + Path.GetFileName(nomeArquivo) + " da Empresa " + item.Empresa + "...";
                        idArquivo = XmlServicoCampinas(nomeArquivo, idArquivo, item.IdEmpresa, _importandoArquivo);

                        item.NomeArquivo = nomeArquivo;
                    }

                    #region Ação a ser tomada com o arquivo Arquivei
                    if (item.Acao == "E")
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                            }
                            else
                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                        }
                        catch (IOException)
                        {
                        }
                    }
                    else
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                {
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                                }
                            }
                            else
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                            }

                        }
                        catch
                        {
                        }
                    }
                    #endregion
                }

                #region Envia Email
                _empresa = new EmpresaBO().Consultar(itemE.Key);

                string[] _dadosEmail = new string[50];
                _dadosEmail[0] = quantidadeArquivos.ToString();
                _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                _dadosEmail[2] = _empresa.NomeAbreviado;
                if (NFNovas.Count() != 0)
                {
                    _dadosEmail[3] = "Numero NFSe".PadRight(12).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in NFNovas)
                    {
                        _dadosEmail[3] = _dadosEmail[3] + item + "</br>";
                    }
                }

                _dadosEmail[4] = quantidadeNFCadastradas.ToString();
                _dadosEmail[6] = quantidadeProcessadoNovos.ToString();
                _dadosEmail[7] = quantidadeComErroLeitura.ToString();
                _dadosEmail[8] = _diretorio;
                _dadosEmail[9] = quantidadesCanceladas.ToString();
                _dadosEmail[10] = quantidadeCartaCorrecao.ToString();

                if (NFCanceladas.Count() != 0)
                {
                    _dadosEmail[11] = "Numero NFSe".PadRight(12).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in NFCanceladas)
                    {
                        _dadosEmail[11] = _dadosEmail[11] + item + "</br>";
                    }
                }

                if (CartaCorrecao.Count() != 0)
                {
                    _dadosEmail[12] = "Numero NFSe".PadRight(12).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in CartaCorrecao)
                    {
                        _dadosEmail[12] = _dadosEmail[12] + item + "</br>";
                    }
                }

                if (NFErros.Count() != 0)
                {
                    _dadosEmail[13] = "Arquivo" + "</br>";
                    foreach (var item in NFErros)
                    {
                        _dadosEmail[13] = _dadosEmail[13] + item + "</br>";
                    }
                }
                string emailDestino = "";

                foreach (var itemU in _listaUsuarios.Where(w => w.AcessaEscrituracaoFiscal))
                {
                    emailDestino = emailDestino + itemU.Email + "; ";
                }

                if (NFErros.Count() != 0)
                    emailDestino = "fflopes@supportse.com.br;mdmunoz@supportse.com.br";


                if (quantidadeArquivos != 0)
                    Classes.Publicas.EnviarEmailArquivei(_dadosEmail, emailDestino, "Arquivei - NFSe Recebidas " + (NFErros.Count() != 0 ? "Com Erros" : ""), false, true);

                #endregion

                NFNovas.Clear();
                NFCanceladas.Clear();
                CartaCorrecao.Clear();
                NFErros.Clear();
            }

            if (_listaArquivei.Count != 0)
                new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei);

            foreach (var item in _listaArquivosImportados)
            {
                new ArquiveiBO().GravarImportando(item);
            }

            //Close();
            #endregion
        }

        private int XmlServicoCampinas(string nomeArquivo, int idArquivo, int idEmpresa, ImportandoArquivei _importandoArquivo)
        {
            List<NOTAS_FISCAISNOTA_FISCAL> _listaNfse = new List<NOTAS_FISCAISNOTA_FISCAL>();
            
            NOTAS_FISCAIS nota = new NOTAS_FISCAIS();

            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();
            Arquivei _cadastrado = null;

            if (nomeArquivo.ToLower().Contains("xml"))
            {
                XmlSerializer ser = new XmlSerializer(typeof(NOTAS_FISCAIS));

                try
                {
                    XmlTextReader reader = new XmlTextReader(nomeArquivo);

                    try
                    {
                        if (!Path.GetFileName(nomeArquivo).StartsWith("ID"))
                        {

                            try
                            {
                                nota = (NOTAS_FISCAIS)ser.Deserialize(reader);
                            }
                            catch (Exception ex)
                            {

                            }

                            if (nota != null && nota.NOTA_FISCAL.Count() != 0)
                            {
                                foreach (var itemNota in nota.NOTA_FISCAL)
                                {
                                    _listaNfse.Add(itemNota);
                                    
                                    _arquivei = new Arquivei();
                                    _itens = new ItensArquivei();
                                    _cadastrado = null;

                                    foreach (var item in _listaNfse)
                                    {
                                        _arquivei.Id = idArquivo;

                                        _arquivei.ChaveDeAcesso = item.CODIGO_VERIFICACAO;
                                        _arquivei.CNPJEmitente = item.PRESTADOR_CPF_CNPJ.ToString();

                                        try
                                        {
                                            _arquivei.NumeroNF = Convert.ToDecimal(item.NUM_NOTA);
                                        }
                                        catch { }

                                        if (_arquivei.ChaveDeAcesso != "")
                                            _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.ChaveDeAcesso, 0);
                                        else
                                            _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.NumeroNF.ToString(), _arquivei.CNPJEmitente);
                                        
                                        if (_cadastrado.Existe)
                                        {
                                            quantidadeNFCadastradas++;
                                            Log _log = new Log();
                                            _log.IdUsuario = 1;
                                            _log.Descricao = "NFSe " + _arquivei.NumeroNF + " com a Chave de Acesso " + _arquivei.ChaveDeAcesso + " já cadastrada para a empresa " + idEmpresa;
                                            _log.Tela = "Principal - Arquivei - NFS-e ja cadastrada";

                                            try
                                            {
                                                new LogBO().Gravar(_log);
                                            }
                                            catch { }

                                            nomeArquivo = nomeArquivo.Replace("xml", "xmlCadastradoProcessado");
                                            _arquiveiJaRenomeado = true;
                                            break;
                                        }

                                        _arquivei.Status = item.SITUACAO_NF;
                                        _arquivei.IdEmpresa = idEmpresa;
                                        _arquivei.NomeArquivo = nomeArquivo;

                                        _arquivei.RazaoSocialDestinatario = item.TOMADOR_RAZAO_SOCIAL;
                                        _arquivei.RazaoSocialEmitente = item.PRESTADOR_RAZAO_SOCIAL;

                                        _arquivei.CNPJDestinatario = item.TOMADOR_CPF_CNPJ.ToString();
                                        
                                        try
                                        {
                                            _arquivei.EnderecoDestinatario = item.TOMADOR_LOGRADOURO.ToString();
                                        }
                                        catch { }
                                        try
                                        {
                                            _arquivei.NumeroEndDestinatario = item.TOMADOR_NUMERO;
                                        }
                                        catch { }
                                        try
                                        {
                                            _arquivei.BairroDestinatario = item.TOMADOR_BAIRRO;
                                        }
                                        catch { }
                                        try
                                        {
                                            _arquivei.CEPDestinatario = item.TOMADOR_CEP.ToString();
                                        }
                                        catch { }

                                        try
                                        {
                                            string dataEmissao = item.DATA_HORA_EMISSAO;

                                            _arquivei.DataEmissao = Convert.ToDateTime(dataEmissao);
                                            //Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                _arquivei.DataEmissao = Convert.ToDateTime(item.DATA_HORA_EMISSAO.ToString());
                                            }
                                            catch { }
                                        }
                                        
                                        _arquivei.Tipo = "Entrada";
                                        _arquivei.Serie = "NFSe";

                                        _arquivei.CodigoServico = item.COS_SERVICO.ToString();
                                        _arquivei.Discriminacao = item.DESCRICAO_SERVICO + Environment.NewLine + item.DESCRICAO_NOTA;
                                        _arquivei.TipoDocto = "NFSe";

                                        try
                                        {
                                            _arquivei.ValorServico = Convert.ToDecimal(item.VALOR_SERVICO.ToString().Replace(".", ","));
                                        }
                                        catch { }
                                        try
                                        {
                                            _arquivei.ValorTotalNF = _arquivei.ValorServico;
                                        }
                                        catch { }

                                        try
                                        {
                                            _arquivei.AliquotaServico = Convert.ToDecimal(item.ALIQ_RET.ToString().Replace(".", ","));
                                        }
                                        catch { }

                                        try
                                        {
                                            _arquivei.ValorISS = Convert.ToDecimal(item.VALOR_ISS_RET.ToString().Replace(".", ","));
                                        }
                                        catch { }

                                        try
                                        {
                                            _arquivei.ValorPis = Convert.ToDecimal(item.VALOR_PIS.ToString().Replace(".", ","));
                                        }
                                        catch { }
                                        try
                                        {
                                            _arquivei.ValorIR = Convert.ToDecimal(item.VALOR_IR.ToString().Replace(".", ","));
                                        }
                                        catch { }
                                        try
                                        {
                                            _arquivei.ValorCofins = Convert.ToDecimal(item.VALOR_COFINS.ToString().Replace(".", ","));
                                        }
                                        catch { }
                                        try
                                        {
                                            _arquivei.ValorCSLL = Convert.ToDecimal(item.VALOR_CSLL.ToString().Replace(".", ","));
                                        }
                                        catch { }
                                        try
                                        {
                                            _arquivei.ValorINSS = Convert.ToDecimal(item.VALOR_INSS.ToString().Replace(".", ","));
                                        }
                                        catch { }

                                        _arquivei.ISSRetido = _arquivei.ValorISS > 0;
                                        _arquivei.TipoArquivo = "XML";

                                        if (!_arquivei.Status.ToUpper().Contains("CANCE"))
                                            NFNovas.Add(_arquivei.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") + 
                                                        _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                                                        _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                        _arquivei.ChaveDeAcesso);
                                        else
                                            NFCanceladas.Add(_arquivei.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") + 
                                                        _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                                                        _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                        _arquivei.ChaveDeAcesso);

                                        _listaArquivei.Add(_arquivei);
                                    }

                                    if (_listaArquivei.Count != 0)
                                    {
                                        quantidadeProcessadoNovos++;
                                        if (!new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei))
                                            NFErros.Add(nomeArquivo);
                                    }

                                    _listaNfse.Clear();
                                    _listaArquivei.Clear();
                                    _listaItensArquivei.Clear();
                                    idArquivo++;
                                }
                                
                            }
                            else
                            {

                                quantidadeComErroLeitura++;
                                NFErros.Add(nomeArquivo);

                                Log _log = new Log();
                                _log.IdUsuario = 1;
                                _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                                _log.Tela = "Principal - Arquivei";

                                try
                                {
                                    new LogBO().Gravar(_log);
                                }
                                catch { }

                                nomeArquivo = nomeArquivo.Replace("xml", "xmlNaoImportado");

                                _listaArquivosImportados.Remove(_importandoArquivo);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        quantidadeComErroLeitura++;
                        NFErros.Add(nomeArquivo);

                        Log _log = new Log();
                        _log.IdUsuario = 1;
                        _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo." + Environment.NewLine + ex.Message;
                        _log.Tela = "Principal - Arquivei";

                        try
                        {
                            new LogBO().Gravar(_log);
                        }
                        catch { }

                        nomeArquivo = nomeArquivo.Replace("xml", "xmlInvalido");
                        _listaArquivosImportados.Remove(_importandoArquivo);
                    }

                    reader.Close();
                }
                catch
                {
                    quantidadeComErroLeitura++;
                    NFErros.Add(nomeArquivo);

                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                    _log.Tela = "Principal - Arquivei";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    nomeArquivo = nomeArquivo.Replace("xml", "xmlNaoImportado");
                    _listaArquivosImportados.Remove(_importandoArquivo);
                }
            }
            
            _listaArquivei.Clear();
            _listaItensArquivei.Clear();

            idArquivo++;
            return idArquivo;
        }

        private void XmlNotaServicoTaubate()

        {
            bool _processo8h = false;
            bool _processo10h = false;
            bool _processo12h = false;
            bool _processo14h = false;
            bool _processo16h = false;
            bool _processo18h = false;

            #region Horarios
            if (((DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).AddMinutes(30).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(08).AddMinutes(45).TimeOfDay) &&
                 // (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(08).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(10).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(12).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(14).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(16).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(18).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay)) ||
                (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)) // só de semana 
            {
                _empresaProcessadaNFSeTaubate8h.Clear();
                _empresaProcessadaNFSeTaubate10h.Clear();
                _empresaProcessadaNFSeTaubate12h.Clear();
                _empresaProcessadaNFSeTaubate14h.Clear();
                _empresaProcessadaNFSeTaubate16h.Clear();
                _empresaProcessadaNFSeTaubate18h.Clear();
                return;
            }


            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(8).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                !_processo10h)
            {
                _processo8h = true;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeTaubate10h.Clear();
                _empresaProcessadaNFSeTaubate12h.Clear();
                _empresaProcessadaNFSeTaubate14h.Clear();
                _empresaProcessadaNFSeTaubate16h.Clear();
                _empresaProcessadaNFSeTaubate18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(10).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                !_processo12h)
            {
                _processo8h = false;
                _processo10h = true;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeTaubate8h.Clear();
                _empresaProcessadaNFSeTaubate12h.Clear();
                _empresaProcessadaNFSeTaubate14h.Clear();
                _empresaProcessadaNFSeTaubate16h.Clear();
                _empresaProcessadaNFSeTaubate18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(12).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                !_processo14h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = true;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeTaubate8h.Clear();
                _empresaProcessadaNFSeTaubate10h.Clear();
                _empresaProcessadaNFSeTaubate14h.Clear();
                _empresaProcessadaNFSeTaubate16h.Clear();
                _empresaProcessadaNFSeTaubate18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(14).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                !_processo16h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = true;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeTaubate8h.Clear();
                _empresaProcessadaNFSeTaubate10h.Clear();
                _empresaProcessadaNFSeTaubate12h.Clear();
                _empresaProcessadaNFSeTaubate16h.Clear();
                _empresaProcessadaNFSeTaubate18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(16).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                !_processo18h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = true;
                _processo18h = false;

                _empresaProcessadaNFSeTaubate8h.Clear();
                _empresaProcessadaNFSeTaubate10h.Clear();
                _empresaProcessadaNFSeTaubate12h.Clear();
                _empresaProcessadaNFSeTaubate14h.Clear();
                _empresaProcessadaNFSeTaubate18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(18).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay) &&
                !_processo8h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = true;

                _empresaProcessadaNFSeTaubate8h.Clear();
                _empresaProcessadaNFSeTaubate10h.Clear();
                _empresaProcessadaNFSeTaubate12h.Clear();
                _empresaProcessadaNFSeTaubate14h.Clear();
                _empresaProcessadaNFSeTaubate16h.Clear();
            }

            #endregion

            _listaParametrosArquivei = new ParametrosArquiveiBO().Listar();
            _listaItensParametros = new ItensParametrosArquiveiBO().Listar(0);
            _arquivosPorEmpresa = new List<ArquivosEmpresaArquivei>();
            _pastaDiaria = DateTime.Now.ToString().Replace("/", "").Replace(" ", "_").Replace(":", "");
            List<string> _listaArquivos = new List<string>();

            DirectoryInfo dirInfo;

            foreach (var itemE in _listaEmpresas) //.Where(w => w.IdEmpresa == 2))
            {
                if (_processo8h)
                {
                    if (_empresaProcessadaNFSeTaubate8h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo10h)
                {
                    if (_empresaProcessadaNFSeTaubate10h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo12h)
                {
                    if (_empresaProcessadaNFSeTaubate12h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo14h)
                {
                    if (_empresaProcessadaNFSeTaubate14h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo16h)
                {
                    if (_empresaProcessadaNFSeTaubate16h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                {
                    if (_empresaProcessadaNFSeTaubate18h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }

                _empresa = new EmpresaBO().Consultar(itemE.IdEmpresa);
                foreach (var item in _listaParametrosArquivei.Where(w => w.IdEmpresa == itemE.IdEmpresa && w.DiretorioNFSe != ""))
                {
                    dirInfo = new DirectoryInfo(item.DiretorioNFSe);
                    //dirInfo =  new DirectoryInfo(@"n:\arquivei\15698659000211\CTe\");

                    if (dirInfo.Exists)
                    {
                        #region Antigo
                        FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

                        if (files.Count() != 0) // configuraçao antiga
                        {
                            foreach (FileSystemInfo file in files)
                            {
                                if (!Path.GetExtension(file.FullName).ToUpper().Equals(".XML") && !Path.GetExtension(file.FullName).ToUpper().Equals(".XLSX"))
                                    continue;

                                if (!file.FullName.ToUpper().Contains("PROCESSADO") &&
                                    !file.FullName.ToUpper().Contains("INVALIDO") &&
                                    !file.FullName.ToUpper().Contains("CRDOWNLOAD") &&
                                    !file.FullName.ToUpper().Contains("NAOIMPORTADO") &&
                                    !file.FullName.ToUpper().Contains("GINFES") &&
                                    !file.FullName.ToUpper().Contains("PAULISTANA") &&
                                    !file.FullName.ToUpper().Contains("CAMPINAS") &&
                                    !file.FullName.Contains("~$") &&
                                    (file.FullName.Contains(".xls") || file.FullName.Contains(".xml") ||
                                    file.FullName.ToUpper().Contains("TAUBATE")))
                                {

                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa
                                                                                           ,
                                        NomeArquivo = file.FullName
                                                                                           ,
                                        Acao = "R"// nao é mais excluido ou renomeado os arquivos
                                                                                           ,
                                        Empresa = _empresa.NomeAbreviado
                                                                                           ,
                                        DiretorioConfigurado = item.DiretorioNFSe
                                    });
                                }
                            }
                        }
                        #endregion

                        #region Novo

                        _listaArquivos = Diretorio(item.DiretorioNFSe);
                        //_listaArquivos = Diretorio(@"n:\arquivei\15698659000211\CTe\");

                        foreach (var itemA in _listaArquivos)
                        {
                            if (!Path.GetExtension(itemA).ToUpper().Equals(".XML") && !Path.GetExtension(itemA).ToUpper().Equals(".XLSX"))
                                continue;

                            if (!itemA.ToUpper().Contains("PROCESSADO") &&
                                !itemA.ToUpper().Contains("INVALIDO") &&
                                !itemA.ToUpper().Contains("CRDOWNLOAD") &&
                                !itemA.ToUpper().Contains("NAOIMPORTADO") &&
                                !itemA.ToUpper().Contains("GINFES") &&
                                !itemA.ToUpper().Contains("PAULISTANA")&&
                                !itemA.ToUpper().Contains("CAMPINAS") &&
                                !itemA.Contains("~$") &&
                                (itemA.Contains(".xls") || itemA.Contains(".xml") || 
                                 itemA.ToUpper().Contains("TAUBATE")))
                            {

                                if (_arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.IdEmpresa &&
                                                                   w.NomeArquivo == itemA).Count() == 0)
                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa
                                                                                          ,
                                        NomeArquivo = itemA
                                                                                          ,
                                        Acao = "R" // nao é mais excluido ou renomeado os arquivos
                                                                                          ,
                                        Empresa = _empresa.NomeAbreviado
                                                                                          ,
                                        DiretorioConfigurado = item.DiretorioNFSe
                                    });
                            }
                        }
                        #endregion
                    }
                }

                Log _log = new Log();
                _log.IdUsuario = 1;
                _log.Descricao = "Iniciou importação arquivei NFSe empresa " + _empresa.NomeAbreviado;
                _log.Tela = "Principal - Arquivei";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }

                if (_processo8h)
                    _empresaProcessadaNFSeTaubate8h.Add(itemE.IdEmpresa);
                else
                {
                    if (_processo10h)
                        _empresaProcessadaNFSeTaubate10h.Add(itemE.IdEmpresa);
                    else
                    {
                        if (_processo12h)
                            _empresaProcessadaNFSeTaubate12h.Add(itemE.IdEmpresa);
                        else
                        {
                            if (_processo14h)
                                _empresaProcessadaNFSeTaubate14h.Add(itemE.IdEmpresa);
                            else
                            {
                                if (_processo16h)
                                    _empresaProcessadaNFSeTaubate16h.Add(itemE.IdEmpresa);
                                else
                                    _empresaProcessadaNFSeTaubate18h.Add(itemE.IdEmpresa);
                            }
                        }
                    }
                }
            }

            #region atributos
            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();

            _listaItensArquivei = new List<ItensArquivei>();
            _listaArquivei = new List<Arquivei>();

            int quantidadeArquivos = 0;
            int quantidadesCanceladas = 0;
            int quantidadeCartaCorrecao = 0;

            DateTime dataemissao = DateTime.MinValue;
            List<string> NFCanceladas = new List<string>();
            List<string> CartaCorrecao = new List<string>();

            #endregion

            #region Leitura/Gravação arquivo excel.
            int idArquivo = 1;
            ImportandoArquivei _importandoArquivo = null;
            _listaArquivosImportados = new List<ImportandoArquivei>();

            _xml = new List<string>();

            foreach (var itemE in _arquivosPorEmpresa.GroupBy(g => g.IdEmpresa))
            {
                quantidadeArquivos = 0;
                quantidadeNFCadastradas = 0;
                quantidadeProcessadoNovos = 0;
                quantidadeComErroLeitura = 0;
                quantidadesCanceladas = 0;
                quantidadeCartaCorrecao = 0;

                foreach (var item in _arquivosPorEmpresa.Where(w => !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("GINFES") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("PAULISTANA") &&
                                                                     w.NomeArquivo.ToUpper().Contains("TAUBATE") &&
                                                                    !w.NomeArquivo.ToUpper().Contains("CAMPINAS") &&
                                                                    !w.NomeArquivo.Contains("~$") &&
                                                                    w.IdEmpresa == itemE.Key)
                                                        .OrderBy(o => o.IdEmpresa))
                {
                    _arquiveiJaRenomeado = false;

                    if (!Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XML") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    #region eventos de cancelamento e carta de correção

                    bool cancelada = false;
                    XmlSerializer ser = new XmlSerializer(typeof(SDTNotasExport));
                    XmlTextReader reader = null;
                    SDTNotasExport nota = new SDTNotasExport();

                    try
                    {
                        reader = new XmlTextReader(item.NomeArquivo);
                        nota = (SDTNotasExport)ser.Deserialize(reader);
                       
                        foreach (var itemN in nota.Reg20)
                        {
                            
                            //encontrar um nota cancelada para ver a situação
                            cancelada = itemN.SitNf == "2";

                            if (cancelada)
                            {
                                quantidadeArquivos++;

                                string chave = itemN.CodVernf;

                                Classes.Arquivei _arquiveiChaveCancelar = new ArquiveiBO().Consultar(item.IdEmpresa, chave, 0);

                                if (!_arquiveiChaveCancelar.Existe)
                                    _arquiveiChaveCancelar = new ArquiveiBO().Consultar(item.IdEmpresa,itemN.NumNf.ToString(), itemN.CpfCnpjPre);

                                List<Arquivei> listaCancelar = new List<Arquivei>();
                                List<ItensArquivei> itensCartaCorrecao = new List<ItensArquivei>();

                                if (_arquiveiChaveCancelar.Existe)
                                {

                                    quantidadesCanceladas++;
                                    _arquiveiChaveCancelar.Status = "Cancelada após importação";
                                    
                                    listaCancelar.Add(_arquiveiChaveCancelar);
                                    
                                    NFCanceladas.Add(_arquiveiChaveCancelar.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") +
                                            _arquiveiChaveCancelar.DataEmissao.ToShortDateString() + "&nbsp" +
                                            _arquiveiChaveCancelar.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                            _arquiveiChaveCancelar.ChaveDeAcesso);

                                    new ArquiveiBO().GravarStatus(listaCancelar);

                                    _importandoArquivo = new ImportandoArquivei();
                                    _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                                    if (!_importandoArquivo.Existe)
                                    {
                                        _importandoArquivo.Arquivo = item.NomeArquivo;
                                        _importandoArquivo.IdUsuario = Publicas._idUsuario;
                                        _importandoArquivo.Importando = true;

                                        new ArquiveiBO().GravarImportando(_importandoArquivo);

                                        _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));
                                        _listaArquivosImportados.Add(_importandoArquivo);

                                        new ArquiveiBO().GravarImportando(_importandoArquivo);
                                    }
                                }
                            }
                        }

                        reader.Close();
                    }
                    catch
                    {

                    }

                   
                    #endregion
                }

                foreach (var item in _arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.Key &&
                                                        !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                        !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("GINFES") &&
                                                        !w.NomeArquivo.ToUpper().Contains("PAULISTANA") &&
                                                         w.NomeArquivo.ToUpper().Contains("TAUBATE") &&
                                                        !w.NomeArquivo.ToUpper().Contains("CAMPINAS") &&
                                                        !w.NomeArquivo.Contains("~$"))
                                                        .OrderByDescending(o => o.NomeArquivo).OrderBy(o => o.IdEmpresa))
                {
                    _diretorio = item.DiretorioConfigurado;
                    if (!Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XML") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    quantidadeArquivos++;

                    _importandoArquivo = new ImportandoArquivei();
                    _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                    nomeArquivo = item.NomeArquivo;

                    if (_importandoArquivo.Existe)
                    {
                        try
                        {
                            quantidadeNFCadastradas++;
                            try
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessadoAnteriormente");
                            }
                            catch
                            {

                            }
                            _arquiveiJaRenomeado = true;
                            continue; // vai para o próximo arquivo
                        }
                        catch { }
                    }
                    else
                    {
                        _importandoArquivo.Arquivo = nomeArquivo;
                        _importandoArquivo.IdUsuario = Publicas._idUsuario;
                        _importandoArquivo.Importando = true;

                        new ArquiveiBO().GravarImportando(_importandoArquivo);

                        _importandoArquivo = new ArquiveiBO().ConsultarArquivo(nomeArquivo, Path.GetFileName(nomeArquivo));
                        _listaArquivosImportados.Add(_importandoArquivo);
                    }

                    if (nomeArquivo.ToLower().Contains("xml"))
                    {
                        notifyIcon1.BalloonTipText = "Importando Arquivei pelo arquivo " + Path.GetFileName(nomeArquivo) + " da Empresa " + item.Empresa + "...";
                        idArquivo = XmlServicoTaubate(nomeArquivo, idArquivo, item.IdEmpresa, _importandoArquivo);

                        item.NomeArquivo = nomeArquivo;
                    }

                    #region Ação a ser tomada com o arquivo Arquivei
                    if (item.Acao == "E")
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                            }
                            else
                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                        }
                        catch (IOException)
                        {
                        }
                    }
                    else
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                {
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                                }
                            }
                            else
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                            }

                        }
                        catch
                        {
                        }
                    }
                    #endregion
                }

                #region Envia Email
                _empresa = new EmpresaBO().Consultar(itemE.Key);

                string[] _dadosEmail = new string[50];
                _dadosEmail[0] = quantidadeArquivos.ToString();
                _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                _dadosEmail[2] = _empresa.NomeAbreviado;
                if (NFNovas.Count() != 0)
                {
                    _dadosEmail[3] = "Numero NFSe".PadRight(12).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in NFNovas)
                    {
                        _dadosEmail[3] = _dadosEmail[3] + item + "</br>";
                    }
                }

                _dadosEmail[4] = quantidadeNFCadastradas.ToString();
                _dadosEmail[6] = quantidadeProcessadoNovos.ToString();
                _dadosEmail[7] = quantidadeComErroLeitura.ToString();
                _dadosEmail[8] = _diretorio;
                _dadosEmail[9] = quantidadesCanceladas.ToString();
                _dadosEmail[10] = quantidadeCartaCorrecao.ToString();

                if (NFCanceladas.Count() != 0)
                {
                    _dadosEmail[11] = "Numero NFSe".PadRight(12).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in NFCanceladas)
                    {
                        _dadosEmail[11] = _dadosEmail[11] + item + "</br>";
                    }
                }

                if (CartaCorrecao.Count() != 0)
                {
                    _dadosEmail[12] = "Numero NFSe".PadRight(12).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in CartaCorrecao)
                    {
                        _dadosEmail[12] = _dadosEmail[12] + item + "</br>";
                    }
                }

                if (NFErros.Count() != 0)
                {
                    _dadosEmail[13] = "Arquivo" + "</br>";
                    foreach (var item in NFErros)
                    {
                        _dadosEmail[13] = _dadosEmail[13] + item + "</br>";
                    }
                }
                string emailDestino = "";

                foreach (var itemU in _listaUsuarios.Where(w => w.AcessaEscrituracaoFiscal))
                {
                    emailDestino = emailDestino + itemU.Email + "; ";
                }

                if (NFErros.Count() != 0)
                    emailDestino = "fflopes@supportse.com.br;mdmunoz@supportse.com.br";

                if (quantidadeArquivos != 0)
                    Classes.Publicas.EnviarEmailArquivei(_dadosEmail, emailDestino, "Arquivei - NFSe Recebidas " + (NFErros.Count() != 0 ? "Com Erros" : ""), false, true);

                #endregion

                NFNovas.Clear();
                NFCanceladas.Clear();
                CartaCorrecao.Clear();
                NFErros.Clear();
            }

            if (_listaArquivei.Count != 0)
                new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei);

            foreach (var item in _listaArquivosImportados)
            {
                new ArquiveiBO().GravarImportando(item);
            }

            //Close();
            #endregion
        }

        private int XmlServicoTaubate(string nomeArquivo, int idArquivo, int idEmpresa, ImportandoArquivei _importandoArquivo)
        {
            List<SDTNotasExportReg20Item> _listaNfse = new List<SDTNotasExportReg20Item>();
            List<SdtNotasExport_TipoDeArquivo_3Reg20Item> _listaNfse1 = new List<SdtNotasExport_TipoDeArquivo_3Reg20Item>();

            SDTNotasExport nota = new SDTNotasExport();
            SdtNotasExport_TipoDeArquivo_3 nota1 = new SdtNotasExport_TipoDeArquivo_3();

            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();
            Arquivei _cadastrado = null;

            if (nomeArquivo.ToLower().Contains("xml"))
            {
                XmlSerializer ser = new XmlSerializer(typeof(SDTNotasExport));

                try
                {
                    XmlTextReader reader = new XmlTextReader(nomeArquivo);

                    try
                    {
                        if (!Path.GetFileName(nomeArquivo).StartsWith("ID"))
                        {

                            try
                            {
                                nota = (SDTNotasExport)ser.Deserialize(reader);
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("SdtNotasExport_TipoDeArquivo_3") ||
                                    ex.InnerException.ToString().Contains("SdtNotasExport_TipoDeArquivo_3") ||
                                    ex.InnerException.ToString().Contains("Dados no nível raiz inválidos"))
                                {
                                    try
                                    {
                                        ser = new XmlSerializer(typeof(SdtNotasExport_TipoDeArquivo_3));
                                        nota1 = (SdtNotasExport_TipoDeArquivo_3)ser.Deserialize(reader);
                                        nota = null;
                                        foreach (var itemNota in nota1.Reg20)
                                        {
                                            _listaNfse1.Add(itemNota);

                                            _arquivei = new Arquivei();
                                            _itens = new ItensArquivei();
                                            _cadastrado = null;

                                            // melhora criar uma função para não ficar duplicado.
                                            foreach (var item in _listaNfse1)
                                            {
                                                _arquivei.Id = idArquivo;

                                                _arquivei.ChaveDeAcesso = item.CodVernf;
                                                _arquivei.CNPJEmitente = item.CpfCnpjPre.ToString();

                                                try
                                                {
                                                    _arquivei.NumeroNF = Convert.ToDecimal(item.NumNf);
                                                }
                                                catch { }

                                                if (_arquivei.ChaveDeAcesso != "")
                                                    _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.ChaveDeAcesso, 0);
                                                else
                                                    _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.NumeroNF.ToString(), _arquivei.CNPJEmitente);

                                                if (_cadastrado.Existe)
                                                {
                                                    quantidadeNFCadastradas++;
                                                    Log _log = new Log();
                                                    _log.IdUsuario = 1;
                                                    _log.Descricao = "NFSe " + _arquivei.NumeroNF + " com a Chave de Acesso " + _arquivei.ChaveDeAcesso + " já cadastrada para a empresa " + idEmpresa;
                                                    _log.Tela = "Principal - Arquivei - NFS-e ja cadastrada";

                                                    try
                                                    {
                                                        new LogBO().Gravar(_log);
                                                    }
                                                    catch { }

                                                    nomeArquivo = nomeArquivo.Replace("xml", "xmlCadastradoProcessado");
                                                    _arquiveiJaRenomeado = true;
                                                    break;
                                                }

                                                _arquivei.IdEmpresa = idEmpresa;
                                                _arquivei.NomeArquivo = nomeArquivo;

                                                _arquivei.Status = "Normal";


                                                try
                                                {
                                                    if (item.SitNf == "2")
                                                    {
                                                        NFCanceladas.Add(_arquivei.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") + 
                                                            _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                                                            _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                            _arquivei.ChaveDeAcesso );
                                                        _arquivei.Status = "Cancelada";
                                                        _arquivei.DataCancelamento = Convert.ToDateTime(item.DataCncNf);
                                                    }
                                                }
                                                catch { }

                                                _arquivei.RazaoSocialDestinatario = item.RazSocTom;
                                                _arquivei.RazaoSocialEmitente = item.RazSocPre;

                                                _arquivei.CNPJDestinatario = item.CpfCnpjTom.ToString();


                                                try
                                                {
                                                    _arquivei.EnderecoDestinatario = item.LogTom.ToString();
                                                }
                                                catch { }
                                                try
                                                {
                                                    _arquivei.NumeroEndDestinatario = item.NumEndTom.ToString();
                                                }
                                                catch { }
                                                try
                                                {
                                                    _arquivei.BairroDestinatario = item.BairroTom;
                                                }
                                                catch { }
                                                try
                                                {
                                                    _arquivei.CEPDestinatario = item.CepTom.ToString();
                                                }
                                                catch { }

                                                try
                                                {
                                                    string dataEmissao = item.DtEmiNf;

                                                    _arquivei.DataEmissao = Convert.ToDateTime(dataEmissao);
                                                    //Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        _arquivei.DataEmissao = Convert.ToDateTime(item.DtEmiNf.ToString());
                                                    }
                                                    catch { }
                                                }

                                                _arquivei.Tipo = "Entrada";
                                                _arquivei.Serie = "NFSe";

                                                _arquivei.CodigoServico = item.CodSrv.ToString();
                                                _arquivei.Discriminacao = item.DiscrSrv;

                                                if (_arquivei.DataCancelamento != DateTime.MinValue)
                                                    _arquivei.Discriminacao = _arquivei.Discriminacao + Environment.NewLine + item.MotivoCncNf;

                                                _arquivei.TipoDocto = "NFSe";

                                                try
                                                {
                                                    _arquivei.ValorServico = Convert.ToDecimal(item.VlNFS.ToString().Replace(".", ","));
                                                }
                                                catch { }
                                                try
                                                {
                                                    _arquivei.ValorTotalNF = _arquivei.ValorServico;
                                                }
                                                catch { }

                                                try
                                                {
                                                    _arquivei.AliquotaServico = Convert.ToDecimal(item.AlqIss.ToString().Replace(".", ","));
                                                }
                                                catch { }

                                                try
                                                {
                                                    _arquivei.ValorISS = Convert.ToDecimal(item.VlIssRet.ToString().Replace(".", ","));
                                                }
                                                catch { }

                                                _arquivei.ISSRetido = _arquivei.ValorISS > 0;
                                                _arquivei.TipoArquivo = "XML";

                                                if (_arquivei.Status != "Cancelada")
                                                    NFNovas.Add(_arquivei.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") + 
                                                                _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                                                                _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                                _arquivei.ChaveDeAcesso);
                                                else
                                                    NFCanceladas.Add(_arquivei.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") + 
                                                                _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                                                                _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                                _arquivei.ChaveDeAcesso);
                                                _listaArquivei.Add(_arquivei);
                                            }

                                            if (_listaArquivei.Count != 0)
                                            {
                                                quantidadeProcessadoNovos++;
                                                if (!new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei))
                                                    NFErros.Add(nomeArquivo);
                                            }

                                            _listaNfse1.Clear();
                                            _listaArquivei.Clear();
                                            _listaItensArquivei.Clear();
                                            idArquivo++;
                                        }
                                    }
                                    catch (Exception ex1)
                                    {

                                    }
                                }
                            }

                            if (nota != null && (nota.Reg20.Count() != 0 || nota.Reg20 == null) )
                            {
                                foreach (var itemNota in nota.Reg20)
                                {
                                    _listaNfse.Add(itemNota);

                                    _arquivei = new Arquivei();
                                    _itens = new ItensArquivei();
                                    _cadastrado = null;

                                    // melhora criar uma função para não ficar duplicado.
                                    foreach (var item in _listaNfse)
                                    {
                                        _arquivei.Id = idArquivo;

                                        _arquivei.ChaveDeAcesso = item.CodVernf;
                                        _arquivei.CNPJEmitente = item.CpfCnpjPre.ToString();

                                        try
                                        {
                                            _arquivei.NumeroNF = Convert.ToDecimal(item.NumNf);
                                        }
                                        catch { }
                                        
                                        if (_arquivei.ChaveDeAcesso != "")
                                            _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.ChaveDeAcesso, 0);
                                        else
                                            _cadastrado = new ArquiveiBO().Consultar(idEmpresa, _arquivei.NumeroNF.ToString(), _arquivei.CNPJEmitente);

                                        if (_cadastrado.Existe)
                                        {
                                            quantidadeNFCadastradas++;
                                            Log _log = new Log();
                                            _log.IdUsuario = 1;
                                            _log.Descricao = "NFSe " + _arquivei.NumeroNF + " com a Chave de Acesso " + _arquivei.ChaveDeAcesso + " já cadastrada para a empresa " + idEmpresa;
                                            _log.Tela = "Principal - Arquivei - NFS-e ja cadastrada";

                                            try
                                            {
                                                new LogBO().Gravar(_log);
                                            }
                                            catch { }

                                            nomeArquivo = nomeArquivo.Replace("xml", "xmlCadastradoProcessado");
                                            _arquiveiJaRenomeado = true;
                                            break;
                                        }

                                        _arquivei.IdEmpresa = idEmpresa;
                                        _arquivei.NomeArquivo = nomeArquivo;

                                        _arquivei.Status = "Normal";


                                        try
                                        {
                                            if (item.SitNf == "2")
                                            {
                                                NFCanceladas.Add(_arquivei.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") + 
                                                                _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                                                                _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                                _arquivei.ChaveDeAcesso);
                                                _arquivei.Status = "Cancelada";
                                                _arquivei.DataCancelamento = Convert.ToDateTime(item.DataCncNf);
                                            }
                                        }
                                        catch { }                                                                               

                                        _arquivei.RazaoSocialDestinatario = item.RazSocTom;
                                        _arquivei.RazaoSocialEmitente = item.RazSocPre;

                                        _arquivei.CNPJDestinatario = item.CpfCnpjTom.ToString();
                                        

                                        try
                                        {
                                            _arquivei.EnderecoDestinatario = item.LogTom.ToString();
                                        }
                                        catch { }
                                        try
                                        {
                                            _arquivei.NumeroEndDestinatario = item.NumEndTom.ToString();
                                        }
                                        catch { }
                                        try
                                        {
                                            _arquivei.BairroDestinatario = item.BairroTom;
                                        }
                                        catch { }
                                        try
                                        {
                                            _arquivei.CEPDestinatario = item.CepTom.ToString();
                                        }
                                        catch { }

                                        try
                                        {
                                            string dataEmissao = item.DtEmiNf;

                                            _arquivei.DataEmissao = Convert.ToDateTime(dataEmissao);
                                            //Convert.ToDateTime(item.NFe.infNFe.ide.dhEmi);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                _arquivei.DataEmissao = Convert.ToDateTime(item.DtEmiNf.ToString());
                                            }
                                            catch { }
                                        }

                                        _arquivei.Tipo = "Entrada";
                                        _arquivei.Serie = "NFSe";

                                        _arquivei.CodigoServico = item.CodSrv.ToString();
                                        _arquivei.Discriminacao = item.DiscrSrv;

                                        if (_arquivei.DataCancelamento != DateTime.MinValue)
                                            _arquivei.Discriminacao = _arquivei.Discriminacao + Environment.NewLine + item.MotivoCncNf;

                                        _arquivei.TipoDocto = "NFSe";

                                        try
                                        {
                                            _arquivei.ValorServico = Convert.ToDecimal(item.VlNFS.ToString().Replace(".", ","));
                                        }
                                        catch { }
                                        try
                                        {
                                            _arquivei.ValorTotalNF = _arquivei.ValorServico;
                                        }
                                        catch { }

                                        try
                                        {
                                            _arquivei.AliquotaServico = Convert.ToDecimal(item.AlqIss.ToString().Replace(".", ","));
                                        }
                                        catch { }

                                        try
                                        {
                                            _arquivei.ValorISS = Convert.ToDecimal(item.VlIssRet.ToString().Replace(".", ","));
                                        }
                                        catch { }
                                        
                                        _arquivei.ISSRetido = _arquivei.ValorISS > 0;
                                        _arquivei.TipoArquivo = "XML";

                                        if (_arquivei.Status != "Cancelada")
                                            NFNovas.Add(_arquivei.NumeroNF.ToString().PadRight(12).Replace(" ", "&nbsp") + 
                                                                _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                                                                _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                                _arquivei.ChaveDeAcesso);

                                        _listaArquivei.Add(_arquivei);
                                    }

                                    if (_listaArquivei.Count != 0)
                                    {
                                        quantidadeProcessadoNovos++;
                                        if (!new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei))
                                            NFErros.Add(nomeArquivo);
                                    }

                                    _listaNfse.Clear();
                                    _listaArquivei.Clear();
                                    _listaItensArquivei.Clear();
                                    idArquivo++;
                                }
                            }
                            else
                            {
                                if (nota1 == null && (nota1.Reg20.Count() == 0 || nota1.Reg20 == null))
                                {
                                    quantidadeComErroLeitura++;
                                    NFErros.Add(nomeArquivo);

                                    Log _log = new Log();
                                    _log.IdUsuario = 1;
                                    _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                                    _log.Tela = "Principal - Arquivei";

                                    try
                                    {
                                        new LogBO().Gravar(_log);
                                    }
                                    catch { }

                                    nomeArquivo = nomeArquivo.Replace("xml", "xmlNaoImportado");

                                    _listaArquivosImportados.Remove(_importandoArquivo);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        quantidadeComErroLeitura++;
                        NFErros.Add(nomeArquivo);

                        Log _log = new Log();
                        _log.IdUsuario = 1;
                        _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo." + Environment.NewLine + ex.Message;
                        _log.Tela = "Principal - Arquivei";

                        try
                        {
                            new LogBO().Gravar(_log);
                        }
                        catch { }

                        nomeArquivo = nomeArquivo.Replace("xml", "xmlInvalido");
                        _listaArquivosImportados.Remove(_importandoArquivo);
                    }

                    reader.Close();
                }
                catch
                {
                    quantidadeComErroLeitura++;
                    NFErros.Add(nomeArquivo);

                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Arquivo " + nomeArquivo + " não foi importado. Erro ao ler o arquivo.";
                    _log.Tela = "Principal - Arquivei";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }

                    nomeArquivo = nomeArquivo.Replace("xml", "xmlNaoImportado");
                    _listaArquivosImportados.Remove(_importandoArquivo);
                }
            }

            _listaArquivei.Clear();
            _listaItensArquivei.Clear();

            idArquivo++;
            return idArquivo;
        }

        #endregion
        
        #region NFSe - por Excel

        private void NFSeGuarulhos_Excel()
        { //inicio Excel Guarulhos
            bool _processo8h = false;
            bool _processo10h = false;
            bool _processo12h = false;
            bool _processo14h = false;
            bool _processo16h = false;
            bool _processo18h = false;

            #region Horarios
            if (((DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).AddMinutes(30).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(09).AddMinutes(59).TimeOfDay) &&
                 //(DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(08).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(10).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(12).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(14).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(14).AddMinutes(59).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(16).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(18).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay)) ||
                (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)) // só de semana
            {
                _empresaProcessadaNFSeGuarulhosExcel8h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel10h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel12h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel14h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel16h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel18h.Clear();
                return;
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(8).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                !_processo10h)
            {
                _processo8h = true;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeGuarulhosExcel10h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel12h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel14h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel16h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(10).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                !_processo12h)
            {
                _processo8h = false;
                _processo10h = true;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeGuarulhosExcel8h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel12h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel14h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel16h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel18h.Clear();
            }

            /*if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(12).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                !_processo14h)*/
            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(0).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(23).AddMinutes(59).TimeOfDay) &&
                !_processo14h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = true;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeGuarulhosExcel8h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel10h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel14h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel16h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(14).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(14).AddMinutes(59).TimeOfDay) &&
                !_processo16h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = true;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeGuarulhosExcel8h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel10h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel12h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel16h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(16).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                !_processo18h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = true;
                _processo18h = false;

                _empresaProcessadaNFSeGuarulhosExcel8h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel10h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel12h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel14h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(18).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay) &&
                !_processo8h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = true;

                _empresaProcessadaNFSeGuarulhosExcel8h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel10h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel12h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel14h.Clear();
                _empresaProcessadaNFSeGuarulhosExcel16h.Clear();
            }

            #endregion

            _listaParametrosArquivei = new ParametrosArquiveiBO().Listar();
            _listaItensParametros = new ItensParametrosArquiveiBO().Listar(0);
            _arquivosPorEmpresa = new List<ArquivosEmpresaArquivei>();
            _pastaDiaria = DateTime.Now.ToString().Replace("/", "").Replace(" ", "_").Replace(":", "");
            List<string> _listaArquivos = new List<string>();

            DirectoryInfo dirInfo;

            foreach (var itemE in _listaEmpresas)//.Where(w => w.IdEmpresa == 10))
            {
                if (_processo8h)
                {
                    if (_empresaProcessadaNFSeGuarulhosExcel8h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo10h)
                {
                    if (_empresaProcessadaNFSeGuarulhosExcel10h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo12h)
                {
                    if (_empresaProcessadaNFSeGuarulhosExcel12h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo14h)
                {
                    if (_empresaProcessadaNFSeGuarulhosExcel14h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo16h)
                {
                    if (_empresaProcessadaNFSeGuarulhosExcel16h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                {
                    if (_empresaProcessadaNFSeGuarulhosExcel18h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }

                _empresa = new EmpresaBO().Consultar(itemE.IdEmpresa);
                foreach (var item in _listaParametrosArquivei.Where(w => w.IdEmpresa == itemE.IdEmpresa && w.DiretorioNFSe != ""))
                {
                    dirInfo = new DirectoryInfo(item.DiretorioNFSe); // new DirectoryInfo(@"n:\controladoria\Contabilidade\EXERCÍCIO 2018\DECLARAÇÕES\ARQUIVEI\00472135000150\NFe\");
                    
                    if (dirInfo.Exists)
                    {
                        #region Antigo
                        FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

                        if (files.Count() != 0) // configuraçao antiga
                        {
                            foreach (FileSystemInfo file in files)
                            {
                                if (!Path.GetExtension(file.FullName).ToUpper().Equals(".XML") && !Path.GetExtension(file.FullName).ToUpper().Equals(".XLSX"))
                                    continue;

                                if (!file.FullName.ToUpper().Contains("PROCESSADO") &&
                                    !file.FullName.ToUpper().Contains("INVALIDO") &&
                                    !file.FullName.ToUpper().Contains("CRDOWNLOAD") &&
                                    !file.FullName.ToUpper().Contains("NAOIMPORTADO") &&
                                    !file.FullName.Contains("~$") &&
                                    (file.FullName.ToLower().Contains(".xls") || file.FullName.ToUpper().Contains(".XLSX")))
                                {
                                    //if (file.FullName.Contains("35191060812088000178550010002050121565130773.xml"))
                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa,
                                        NomeArquivo = file.FullName,
                                        Acao = "R", // nao é mais excluido ou renomeado os arquivos                                                                                       
                                        Empresa = _empresa.NomeAbreviado,
                                        DiretorioConfigurado = item.Diretorio
                                    });
                                }
                            }
                        }
                        #endregion

                        #region Novo

                        _listaArquivos = Diretorio(item.DiretorioNFSe);// @"n:\controladoria\Contabilidade\EXERCÍCIO 2018\DECLARAÇÕES\ARQUIVEI\00472135000150\NFe\");

                        foreach (var itemA in _listaArquivos)
                        {
                            if (!Path.GetExtension(itemA).ToLower().Equals(".xls") && !Path.GetExtension(itemA).ToUpper().Equals(".XLSX"))
                                continue;

                            if (!itemA.ToUpper().Contains("PROCESSADO") &&
                                !itemA.ToUpper().Contains("INVALIDO") &&
                                !itemA.ToUpper().Contains("CRDOWNLOAD") &&
                                !itemA.ToUpper().Contains("NAOIMPORTADO") &&
                                !itemA.Contains("~$") &&
                                (itemA.ToLower().Contains(".xls") || itemA.ToUpper().Contains(".XLSX")))
                            {

                                if (_arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.IdEmpresa &&
                                                                   w.NomeArquivo == itemA).Count() == 0)
                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa,
                                        NomeArquivo = itemA,
                                        Acao = "R", // nao é mais excluido ou renomeado os arquivos                                                   
                                        Empresa = _empresa.NomeAbreviado,
                                        DiretorioConfigurado = item.Diretorio
                                    });
                            }
                        }
                        #endregion
                    }
                }

                Log _log = new Log();
                _log.IdUsuario = 1;
                _log.Descricao = "Iniciou importação arquivei NFe da empresa " + _empresa.NomeAbreviado;
                _log.Tela = "Principal - Arquivei";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }

                if (_processo8h)
                    _empresaProcessadaNFSeGuarulhosExcel8h.Add(itemE.IdEmpresa);
                else
                {
                    if (_processo10h)
                        _empresaProcessadaNFSeGuarulhosExcel10h.Add(itemE.IdEmpresa);
                    else
                    {
                        if (_processo12h)
                            _empresaProcessadaNFSeGuarulhosExcel12h.Add(itemE.IdEmpresa);
                        else
                        {
                            if (_processo14h)
                                _empresaProcessadaNFSeGuarulhosExcel14h.Add(itemE.IdEmpresa);
                            else
                            {
                                if (_processo16h)
                                    _empresaProcessadaNFSeGuarulhosExcel16h.Add(itemE.IdEmpresa);
                                else
                                    _empresaProcessadaNFSeGuarulhosExcel18h.Add(itemE.IdEmpresa);
                            }
                        }
                    }
                }
            }

            #region atributos
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            Excel.Range range = null;
            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();

            _listaItensArquivei = new List<ItensArquivei>();
            _listaArquivei = new List<Arquivei>();

            string str = "";
            int rCnt;
            
            int rw = 0;
            int cl = 0;
            int quantidadeArquivos = 0;
            

            List<string> NFCanceladas = new List<string>();
            List<string> CartaCorrecao = new List<string>();
            List<string> NFErros = new List<string>();

            #endregion

            #region Leitura/Gravação arquivo excel.
            int idArquivo = 1;
            Arquivei _cadastrado = null;
            ImportandoArquivei _importandoArquivo = null;
            _listaArquivosImportados = new List<ImportandoArquivei>();

            _xml = new List<string>();

            foreach (var itemE in _arquivosPorEmpresa.GroupBy(g => g.IdEmpresa))
            {
                quantidadeArquivos = 0;
                //quantidadeEventos = 0;
                quantidadeNFCadastradas = 0;
                quantidadeProcessadoNovos = 0;
                quantidadeComErroLeitura = 0;
                quantidadesCanceladas = 0;
                quantidadeCartaCorrecao = 0;

                foreach (var item in _arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.Key &&
                                                         (w.NomeArquivo.ToLower().Contains(".xls") || w.NomeArquivo.ToUpper().Contains(".XLSX")) &&
                                                          !w.NomeArquivo.ToUpper().Contains("GUARULHOS") &&
                                                        !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                        !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                        !w.NomeArquivo.Contains("~$"))
                                                        .OrderByDescending(o => o.NomeArquivo).OrderBy(o => o.IdEmpresa))
                {
                    _diretorio = item.DiretorioConfigurado;
                    if (!Path.GetExtension(item.NomeArquivo).ToLower().Equals(".xls") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    quantidadeComErroLeitura++;
                    NFErros.Add(item.NomeArquivo);

                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Arquivo '" + item.NomeArquivo + "'com o nome invalido para a empresa " + item.IdEmpresa;
                    _log.Tela = "Principal - Arquivei";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                foreach (var item in _arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.Key &&
                                                         (w.NomeArquivo.ToLower().Contains(".xls") || w.NomeArquivo.ToUpper().Contains(".XLSX")) &&
                                                          w.NomeArquivo.ToUpper().Contains("GUARULHOS")  &&
                                                        !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                        !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                        !w.NomeArquivo.Contains("~$"))
                                                        .OrderByDescending(o => o.NomeArquivo).OrderBy(o => o.IdEmpresa))
                {
                    _diretorio = item.DiretorioConfigurado;
                    if (!Path.GetExtension(item.NomeArquivo).ToLower().Equals(".xls") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    quantidadeArquivos++;

                    _importandoArquivo = new ImportandoArquivei();
                    _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                    nomeArquivo = item.NomeArquivo;

                    if (_importandoArquivo.Existe)
                    {
                        try
                        {
                            quantidadeNFCadastradas++;
                            try
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xls", "xlsProcessadoAnteriormente");
                            }
                            catch
                            {

                            }
                            _arquiveiJaRenomeado = true;
                            continue; // vai para o próximo arquivo
                        }
                        catch { }
                    }
                    else
                    {
                        _importandoArquivo.Arquivo = nomeArquivo;
                        _importandoArquivo.IdUsuario = Publicas._idUsuario;
                        _importandoArquivo.Importando = true;

                        new ArquiveiBO().GravarImportando(_importandoArquivo);

                        _importandoArquivo = new ArquiveiBO().ConsultarArquivo(nomeArquivo, Path.GetFileName(nomeArquivo));
                        _listaArquivosImportados.Add(_importandoArquivo);
                    }
                                        
                    {
                        Publicas._mensagemSistema = "Importando Arquivei pelo arquivo Excel da Empresa " + item.Empresa + "...";
                        xlApp = new Excel.Application();

                        try
                        {
                            xlApp.DisplayAlerts = false;
                            xlWorkBook = xlApp.Workbooks.Open(nomeArquivo, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 1);
                            try
                            {
                                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
                            }
                            catch
                            {
                                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                            }

                            range = xlWorkSheet.UsedRange;
                            rw = range.Rows.Count;
                            cl = range.Columns.Count;
                            // linha um é o cabeçalho
                            str = (Convert.ToString((range.Cells[1, 1] as Excel.Range).Value2));
                        }
                        catch
                        {
                            new ArquiveiBO().ExcluirImportando(_importandoArquivo);
                            return;
                        }

                        for (rCnt = 2; rCnt <= rw; rCnt++)
                        {
                            _arquivei = new Arquivei();
                            _arquivei.IdEmpresa = item.IdEmpresa;
                            _arquivei.NomeArquivo = nomeArquivo;
                            _arquivei.TipoArquivo = "EXCEL";

                            _cadastrado = new Arquivei();
                            _arquivei.Tipo = "Entrada";
                            _arquivei.TipoDocto = "NFSe";
                            
                            _arquivei.NumeroNF = Convert.ToDecimal( Convert.ToString((range.Cells[rCnt, 2] as Excel.Range).Value2) );
                            _arquivei.Serie = Convert.ToString((range.Cells[rCnt, 3] as Excel.Range).Value2);
                            _arquivei.RazaoSocialEmitente = Convert.ToString((range.Cells[rCnt, 6] as Excel.Range).Value2);
                            _arquivei.IEEmitente = Convert.ToString((range.Cells[rCnt, 7] as Excel.Range).Value2);
                            _arquivei.CNPJEmitente = Convert.ToString((range.Cells[rCnt, 8] as Excel.Range).Value2);
                            _arquivei.ChaveDeAcesso = _arquivei.NumeroNF.ToString() + _arquivei.CNPJEmitente; // só para não ficar vazio;

                            _cadastrado = new ArquiveiBO().Consultar(item.IdEmpresa, _arquivei.NumeroNF.ToString(), _arquivei.CNPJEmitente);

                            if (_cadastrado.Existe)
                            {
                                if (Convert.ToString((range.Cells[rCnt, 27] as Excel.Range).Value2) == "2" && _cadastrado.Status != "Cancelada")
                                {
                                    _cadastrado.Status = "Cancelada";
                                    _listaArquivei.Add(_cadastrado);
                                    NFCanceladas.Add(_cadastrado.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp") + 
                                                     _cadastrado.DataEmissao.ToShortDateString() + "&nbsp" +
                                                     _cadastrado.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                     _cadastrado.ChaveDeAcesso);
                                    continue;
                                }

                                quantidadeNFCadastradas++;
                                Log _log = new Log();
                                _log.IdUsuario = 1;
                                _log.Descricao = "NFSe " + _arquivei.NumeroNF + " com a Chave de Acesso " + _arquivei.ChaveDeAcesso + " já cadastrada para a empresa " + item.IdEmpresa;
                                _log.Tela = "Principal - Arquivei - NFS-e ja cadastrada";

                                try
                                {
                                    new LogBO().Gravar(_log);
                                }
                                catch { }

                                //nomeArquivo = nomeArquivo.Replace("xls", "xlsCadastradoProcessado");
                                //_arquiveiJaRenomeado = true;
                                continue;
                            }
                            else
                            {
                            
                                if (_listaArquivei.Where(w => w.IdEmpresa == item.IdEmpresa &&
                                                              w.NumeroNF == _arquivei.NumeroNF &&
                                                              w.CNPJEmitente == _arquivei.CNPJEmitente).Count() != 0)
                                {
                                    //não inclui novamente para não duplicar
                                    continue;
                                }
                            }


                            #region tratativa data emissao
                            DateTime dataI;
                            string teste = "";
                            try
                            {
                                dataI = Convert.ToDateTime(Convert.ToString((range.Cells[rCnt, 23] as Excel.Range).Value2));
                            }
                            catch {
                                dataI = Convert.ToDateTime(Convert.ToString((range.Cells[rCnt, 23] as Excel.Range).Value));
                            }
                            int ano = dataI.Year;
                            int mes = 0;

                            #region mês
                            if (nomeArquivo.Contains(@"\01\"))
                                mes = 1;
                            if (nomeArquivo.Contains(@"\02\"))
                                mes = 2;
                            if (nomeArquivo.Contains(@"\03\"))
                                mes = 3;
                            if (nomeArquivo.Contains(@"\04\"))
                                mes = 4;
                            if (nomeArquivo.Contains(@"\05\"))
                                mes = 5;
                            if (nomeArquivo.Contains(@"\06\"))
                                mes = 6;
                            if (nomeArquivo.Contains(@"\07\"))
                                mes = 7;
                            if (nomeArquivo.Contains(@"\08\"))
                                mes = 8;
                            if (nomeArquivo.Contains(@"\09\"))
                                mes = 9;
                            if (nomeArquivo.Contains(@"\10\"))
                                mes = 10;
                            if (nomeArquivo.Contains(@"\11\"))
                                mes = 11;
                            if (nomeArquivo.Contains(@"\12\"))
                                mes = 12;
                            #endregion

                            teste = Convert.ToString((range.Cells[rCnt, 20] as Excel.Range).Value2) + "/" + mes + "/" + ano;

                            _arquivei.DataEmissao = Convert.ToDateTime(Convert.ToString((range.Cells[rCnt, 20] as Excel.Range).Value2) + "/" + mes + "/" + ano);
                            #endregion

                            _arquivei.CodigoServico = Convert.ToString((range.Cells[rCnt, 21] as Excel.Range).Value2);
                            _arquivei.ValorServico = Convert.ToDecimal(Convert.ToString((range.Cells[rCnt, 22] as Excel.Range).Value2).Replace(".", ","));
                            _arquivei.ValorTotalNF = _arquivei.ValorServico;
                            _arquivei.AliquotaServico = Convert.ToDecimal(Convert.ToString((range.Cells[rCnt, 25] as Excel.Range).Value2).Replace(".", ","));

                            try
                            {
                                _arquivei.ValorISS = Convert.ToDecimal(Convert.ToString((range.Cells[rCnt, 26] as Excel.Range).Value2).Replace(".", ","));
                            }
                            catch { }

                            maskedEditBox1.Text = _arquivei.CNPJEmitente;

                            //testar formatação
                            if (Convert.ToString((range.Cells[rCnt, 27] as Excel.Range).Value2) == "2")
                            {
                                _arquivei.Status = "Cancelada";
                                NFCanceladas.Add(_arquivei.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp") + 
                                                 _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                                                 _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                 _arquivei.ChaveDeAcesso);
                            }
                            else
                            {
                                _arquivei.Status = "Normal";
                                NFNovas.Add(_arquivei.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp") + 
                                                 _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                                                 _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                 _arquivei.ChaveDeAcesso);
                            }

                            _listaArquivei.Add(_arquivei);
                            idArquivo++;

                        }

                    }

                    try
                    {
                        xlWorkBook.Close(true, misValue, misValue);
                    }
                    catch
                    {
                    }

                    try
                    {
                        xlApp.Quit();
                    }
                    catch { }
                    #region Ação a ser tomada com o arquivo Arquivei
                    if (item.Acao == "E")
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                            }
                            else
                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                        }
                        catch (IOException)
                        {

                        }
                    }
                    else
                    {
                        try
                        {
                            if (nomeArquivo.ToLower().Contains("xml"))
                            {
                                if (!_arquiveiJaRenomeado)
                                {
                                    item.NomeArquivo = item.NomeArquivo.Replace("xml", "xmlProcessado");
                                }
                            }
                            else
                            {

                                item.NomeArquivo = item.NomeArquivo.Replace("xlsx", "xlsxProcessado");
                            }

                        }
                        catch
                        {

                        }
                    }
                    #endregion
                }

                #region Envia Email
                _empresa = new EmpresaBO().Consultar(itemE.Key);

                string[] _dadosEmail = new string[50];
                _dadosEmail[0] = quantidadeArquivos.ToString();
                _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                _dadosEmail[2] = _empresa.NomeAbreviado;

                if (NFNovas.Count() != 0)
                {
                    _dadosEmail[3] = "Numero NFSe".PadRight(13).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in NFNovas)
                    {
                        _dadosEmail[3] = _dadosEmail[3] + item + "</br>";
                    }
                }

                _dadosEmail[4] = quantidadeNFCadastradas.ToString();
                _dadosEmail[6] = NFNovas.Count().ToString();
                _dadosEmail[7] = quantidadeComErroLeitura.ToString();
                _dadosEmail[8] = _diretorio;
                _dadosEmail[9] = NFCanceladas.Count().ToString();
                _dadosEmail[10] = CartaCorrecao.Count().ToString();

                if (NFCanceladas.Count() != 0)
                {
                    _dadosEmail[11] = "Numero NFSe".PadRight(13).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in NFCanceladas)
                    {
                        _dadosEmail[11] = _dadosEmail[11] + item + "</br>";
                    }
                }

                if (CartaCorrecao.Count() != 0)
                {
                    _dadosEmail[12] = "Numero NFSe".PadRight(13).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in CartaCorrecao)
                    {
                        _dadosEmail[12] = _dadosEmail[12] + item + "</br>";
                    }
                }

                if (NFErros.Count() != 0)
                {
                    _dadosEmail[13] = "Arquivo" + "</br>";
                    foreach (var item in NFErros)
                    {
                        _dadosEmail[13] = _dadosEmail[13] + item + "</br>";
                    }
                }

                string emailDestino = "";

                foreach (var itemU in _listaUsuarios.Where(w => w.AcessaEscrituracaoFiscal))
                {
                    emailDestino = emailDestino + itemU.Email + "; ";
                }

                if (NFErros.Count() != 0)
                    emailDestino = "fflopes@supportse.com.br;mdmunoz@supportse.com.br";


                if (quantidadeArquivos != 0)
                    Classes.Publicas.EnviarEmailArquivei(_dadosEmail, emailDestino, "Arquivei - NFSe Recebidas - Excel" + (NFErros.Count() != 0 ? "Com Erros" : ""), false, true);

                #endregion

                NFNovas.Clear();
                NFCanceladas.Clear();
                CartaCorrecao.Clear();
                NFErros.Clear();

                if (_listaArquivei.Count != 0)
                    new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei);

                _listaArquivei.Clear();
            }

            if (_listaArquivei.Count != 0)
                new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei);

            foreach (var item in _listaArquivosImportados)
            {
                new ArquiveiBO().GravarImportando(item);
            }

            //Close();
            #endregion
        } //Fim Excel Guarulhos
        
        private void NFSeRibeirao_Excel()
        { //Inicio Excel Ribeirão
            bool _processo8h = false;
            bool _processo10h = false;
            bool _processo12h = false;
            bool _processo14h = false;
            bool _processo16h = false;
            bool _processo18h = false;

            #region Horarios
            if (((DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).AddMinutes(30).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(08).AddMinutes(45).TimeOfDay) &&
                 //(DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(08).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(08).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(10).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(12).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(14).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(16).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                 (DateTime.Now.TimeOfDay < DateTime.MinValue.AddHours(18).TimeOfDay || DateTime.Now.TimeOfDay > DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay)) ||
                (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)) // só de semana
            {
                _empresaProcessadaNFSeRibeiraoExcel8h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel10h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel12h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel14h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel16h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel18h.Clear();
                return;
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(8).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(9).AddMinutes(59).TimeOfDay) &&
                !_processo10h)
            {
                _processo8h = true;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeRibeiraoExcel10h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel12h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel14h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel16h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(10).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(10).AddMinutes(10).TimeOfDay) &&
                !_processo12h)
            {
                _processo8h = false;
                _processo10h = true;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeRibeiraoExcel8h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel12h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel14h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel16h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(12).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(12).AddMinutes(10).TimeOfDay) &&
                !_processo14h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = true;
                _processo14h = false;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeRibeiraoExcel8h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel10h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel14h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel16h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(14).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(14).AddMinutes(10).TimeOfDay) &&
                !_processo16h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = true;
                _processo16h = false;
                _processo18h = false;

                _empresaProcessadaNFSeRibeiraoExcel8h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel10h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel12h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel16h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(16).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(16).AddMinutes(10).TimeOfDay) &&
                !_processo18h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = true;
                _processo18h = false;

                _empresaProcessadaNFSeRibeiraoExcel8h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel10h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel12h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel14h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel18h.Clear();
            }

            if ((DateTime.Now.TimeOfDay >= DateTime.MinValue.AddHours(18).TimeOfDay &&
                 DateTime.Now.TimeOfDay <= DateTime.MinValue.AddHours(18).AddMinutes(10).TimeOfDay) &&
                !_processo8h)
            {
                _processo8h = false;
                _processo10h = false;
                _processo12h = false;
                _processo14h = false;
                _processo16h = false;
                _processo18h = true;

                _empresaProcessadaNFSeRibeiraoExcel8h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel10h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel12h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel14h.Clear();
                _empresaProcessadaNFSeRibeiraoExcel16h.Clear();
            }

            #endregion

            _listaParametrosArquivei = new ParametrosArquiveiBO().Listar();
            _listaItensParametros = new ItensParametrosArquiveiBO().Listar(0);
            _arquivosPorEmpresa = new List<ArquivosEmpresaArquivei>();
            _pastaDiaria = DateTime.Now.ToString().Replace("/", "").Replace(" ", "_").Replace(":", "");
            List<string> _listaArquivos = new List<string>();

            DirectoryInfo dirInfo;

            foreach (var itemE in _listaEmpresas)//.Where(w => w.IdEmpresa == 5))
            {
                if (_processo8h)
                {
                    if (_empresaProcessadaNFSeRibeiraoExcel8h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo10h)
                {
                    if (_empresaProcessadaNFSeRibeiraoExcel10h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo12h)
                {
                    if (_empresaProcessadaNFSeRibeiraoExcel12h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo14h)
                {
                    if (_empresaProcessadaNFSeRibeiraoExcel14h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                if (_processo16h)
                {
                    if (_empresaProcessadaNFSeRibeiraoExcel16h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }
                else
                {
                    if (_empresaProcessadaNFSeRibeiraoExcel18h.Where(w => w.Equals(itemE.IdEmpresa)).Count() != 0)
                        continue;
                }

                _empresa = new EmpresaBO().Consultar(itemE.IdEmpresa);
                foreach (var item in _listaParametrosArquivei.Where(w => w.IdEmpresa == itemE.IdEmpresa && w.DiretorioNFSe != ""))
                {
                    dirInfo = new DirectoryInfo(item.DiretorioNFSe); // new DirectoryInfo(@"n:\controladoria\Contabilidade\EXERCÍCIO 2018\DECLARAÇÕES\ARQUIVEI\00472135000150\NFe\");

                    if (dirInfo.Exists)
                    {
                        #region Antigo
                        FileSystemInfo[] files = dirInfo.GetFileSystemInfos();

                        if (files.Count() != 0) // configuraçao antiga
                        {
                            foreach (FileSystemInfo file in files)
                            {
                                if (!Path.GetExtension(file.FullName).ToUpper().Equals(".XML") && !Path.GetExtension(file.FullName).ToUpper().Equals(".XLSX"))
                                    continue;

                                if (!file.FullName.ToUpper().Contains("PROCESSADO") &&
                                    !file.FullName.ToUpper().Contains("INVALIDO") &&
                                    !file.FullName.ToUpper().Contains("CRDOWNLOAD") &&
                                    !file.FullName.ToUpper().Contains("NAOIMPORTADO") &&
                                    !file.FullName.Contains("~$") &&
                                    (file.FullName.ToLower().Contains(".xls") || file.FullName.ToUpper().Contains(".XLSX")))
                                {
                                    //if (file.FullName.Contains("35191060812088000178550010002050121565130773.xml"))
                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa,
                                        NomeArquivo = file.FullName,
                                        Acao = "R", // nao é mais excluido ou renomeado os arquivos                                                                                       
                                        Empresa = _empresa.NomeAbreviado,
                                        DiretorioConfigurado = item.Diretorio
                                    });
                                }
                            }
                        }
                        #endregion

                        #region Novo

                        _listaArquivos = Diretorio(item.DiretorioNFSe);// @"n:\controladoria\Contabilidade\EXERCÍCIO 2018\DECLARAÇÕES\ARQUIVEI\00472135000150\NFe\");

                        foreach (var itemA in _listaArquivos)
                        {
                            if (!Path.GetExtension(itemA).ToLower().Equals(".xls") && !Path.GetExtension(itemA).ToUpper().Equals(".XLSX"))
                                continue;

                            if (!itemA.ToUpper().Contains("PROCESSADO") &&
                                !itemA.ToUpper().Contains("INVALIDO") &&
                                !itemA.ToUpper().Contains("CRDOWNLOAD") &&
                                !itemA.ToUpper().Contains("NAOIMPORTADO") &&
                                !itemA.Contains("~$") &&
                                (itemA.ToLower().Contains(".xls") || itemA.ToUpper().Contains(".XLSX")))
                            {

                                if (_arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.IdEmpresa &&
                                                                   w.NomeArquivo == itemA).Count() == 0)
                                    _arquivosPorEmpresa.Add(new ArquivosEmpresaArquivei()
                                    {
                                        IdEmpresa = itemE.IdEmpresa,
                                        NomeArquivo = itemA,
                                        Acao = "R", // nao é mais excluido ou renomeado os arquivos                                                   
                                        Empresa = _empresa.NomeAbreviado,
                                        DiretorioConfigurado = item.Diretorio
                                    });
                            }
                        }
                        #endregion
                    }
                }

                Log _log = new Log();
                _log.IdUsuario = 1;
                _log.Descricao = "Iniciou importação arquivei NFe da empresa " + _empresa.NomeAbreviado;
                _log.Tela = "Principal - Arquivei";

                try
                {
                    new LogBO().Gravar(_log);
                }
                catch { }

                if (_processo8h)
                    _empresaProcessadaNFSeRibeiraoExcel8h.Add(itemE.IdEmpresa);
                else
                {
                    if (_processo10h)
                        _empresaProcessadaNFSeRibeiraoExcel10h.Add(itemE.IdEmpresa);
                    else
                    {
                        if (_processo12h)
                            _empresaProcessadaNFSeRibeiraoExcel12h.Add(itemE.IdEmpresa);
                        else
                        {
                            if (_processo14h)
                                _empresaProcessadaNFSeRibeiraoExcel14h.Add(itemE.IdEmpresa);
                            else
                            {
                                if (_processo16h)
                                    _empresaProcessadaNFSeRibeiraoExcel16h.Add(itemE.IdEmpresa);
                                else
                                    _empresaProcessadaNFSeRibeiraoExcel18h.Add(itemE.IdEmpresa);
                            }
                        }
                    }
                }
            }

            #region atributos
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            Excel.Range range = null;
            Arquivei _arquivei = new Arquivei();
            ItensArquivei _itens = new ItensArquivei();

            _listaItensArquivei = new List<ItensArquivei>();
            _listaArquivei = new List<Arquivei>();

            string str = "";
            int rCnt;

            int rw = 0;
            int cl = 0;
            int quantidadeArquivos = 0;


            List<string> NFCanceladas = new List<string>();
            List<string> CartaCorrecao = new List<string>();
            List<string> NFErros = new List<string>();

            #endregion

            #region Leitura/Gravação arquivo excel.
            int idArquivo = 1;
            Arquivei _cadastrado = null;
            ImportandoArquivei _importandoArquivo = null;
            _listaArquivosImportados = new List<ImportandoArquivei>();

            _xml = new List<string>();

            foreach (var itemE in _arquivosPorEmpresa.GroupBy(g => g.IdEmpresa))
            {
                quantidadeArquivos = 0;
                //quantidadeEventos = 0;
                quantidadeNFCadastradas = 0;
                quantidadeProcessadoNovos = 0;
                quantidadeComErroLeitura = 0;
                quantidadesCanceladas = 0;
                quantidadeCartaCorrecao = 0;

                NFNovas.Clear();
                NFCanceladas.Clear();
                CartaCorrecao.Clear();
                NFErros.Clear();

                foreach (var item in _arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.Key &&
                                                         (w.NomeArquivo.ToLower().Contains(".xls") || w.NomeArquivo.ToUpper().Contains(".XLSX")) &&
                                                          !w.NomeArquivo.ToUpper().Contains("RIBEIRAO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                        !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                        !w.NomeArquivo.Contains("~$"))
                                                        .OrderByDescending(o => o.NomeArquivo).OrderBy(o => o.IdEmpresa))
                {
                    _diretorio = item.DiretorioConfigurado;
                    if (!Path.GetExtension(item.NomeArquivo).ToLower().Equals(".xls") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    quantidadeComErroLeitura++;
                    NFErros.Add(item.NomeArquivo);

                    Log _log = new Log();
                    _log.IdUsuario = 1;
                    _log.Descricao = "Arquivo '" + item.NomeArquivo + "'com o nome invalido para a empresa " + item.IdEmpresa;
                    _log.Tela = "Principal - Arquivei";

                    try
                    {
                        new LogBO().Gravar(_log);
                    }
                    catch { }
                }

                foreach (var item in _arquivosPorEmpresa.Where(w => w.IdEmpresa == itemE.Key &&
                                                         (w.NomeArquivo.ToLower().Contains(".xls") || w.NomeArquivo.ToUpper().Contains(".XLSX")) &&
                                                          w.NomeArquivo.ToUpper().Contains("RIBEIRAO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("PROCESSADO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("INVALIDO") &&
                                                        !w.NomeArquivo.ToUpper().Contains("CRDOWNLOAD") &&
                                                        !w.NomeArquivo.ToUpper().Contains("NAOIMPORTADO") &&
                                                        !w.NomeArquivo.Contains("~$"))
                                                        .OrderByDescending(o => o.NomeArquivo).OrderBy(o => o.IdEmpresa))
                {
                    _diretorio = item.DiretorioConfigurado;
                    if (!Path.GetExtension(item.NomeArquivo).ToLower().Equals(".xls") && !Path.GetExtension(item.NomeArquivo).ToUpper().Equals(".XLSX"))
                        continue;

                    quantidadeArquivos++;

                    _importandoArquivo = new ImportandoArquivei();
                    _importandoArquivo = new ArquiveiBO().ConsultarArquivo(item.NomeArquivo, Path.GetFileName(item.NomeArquivo));

                    nomeArquivo = item.NomeArquivo;

                    if (_importandoArquivo.Existe)
                    {
                        try
                        {
                            quantidadeNFCadastradas++;
                            try
                            {
                                item.NomeArquivo = item.NomeArquivo.Replace("xls", "xlsProcessadoAnteriormente");
                            }
                            catch
                            {

                            }
                            _arquiveiJaRenomeado = true;
                            continue; // vai para o próximo arquivo
                        }
                        catch { }
                    }
                    else
                    {
                        _importandoArquivo.Arquivo = nomeArquivo;
                        _importandoArquivo.IdUsuario = Publicas._idUsuario;
                        _importandoArquivo.Importando = true;

                        new ArquiveiBO().GravarImportando(_importandoArquivo);

                        _importandoArquivo = new ArquiveiBO().ConsultarArquivo(nomeArquivo, Path.GetFileName(nomeArquivo));
                        _listaArquivosImportados.Add(_importandoArquivo);
                    }

                    {
                        Publicas._mensagemSistema = "Importando Arquivei pelo arquivo Excel da Empresa " + item.Empresa + "...";
                        xlApp = new Excel.Application();

                        try
                        {
                            xlApp.DisplayAlerts = false;
                            xlWorkBook = xlApp.Workbooks.Open(nomeArquivo, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 1);
                            try
                            {
                                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
                            }
                            catch
                            {
                                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                            }

                            range = xlWorkSheet.UsedRange;
                            rw = range.Rows.Count;
                            cl = range.Columns.Count;
                            // linha um é o cabeçalho
                            str = (Convert.ToString((range.Cells[1, 1] as Excel.Range).Value2));
                        }
                        catch
                        {
                            new ArquiveiBO().ExcluirImportando(_importandoArquivo);
                            return;
                        }

                        for (rCnt = 4; rCnt <= rw; rCnt++)
                        {
                            _arquivei = new Arquivei();
                            _arquivei.IdEmpresa = item.IdEmpresa;
                            _arquivei.NomeArquivo = nomeArquivo;
                            _arquivei.TipoArquivo = "EXCEL";

                            _cadastrado = new Arquivei();
                            _arquivei.Tipo = "Entrada";
                            _arquivei.TipoDocto = "NFSe";

                            _arquivei.NumeroNF = Convert.ToDecimal(Convert.ToString((range.Cells[rCnt, 1] as Excel.Range).Value2));
                            _arquivei.Serie = Convert.ToString((range.Cells[rCnt, 2] as Excel.Range).Value2);
                            //_arquivei.RazaoSocialEmitente = Convert.ToString((range.Cells[rCnt, 6] as Excel.Range).Value2);
                            //_arquivei.IEEmitente = Convert.ToString((range.Cells[rCnt, 7] as Excel.Range).Value2);
                            _arquivei.CNPJEmitente = Convert.ToString((range.Cells[rCnt, 11] as Excel.Range).Value2);
                            _arquivei.ChaveDeAcesso = _arquivei.NumeroNF.ToString() + _arquivei.CNPJEmitente; // só para não ficar vazio;

                            _cadastrado = new ArquiveiBO().Consultar(item.IdEmpresa, _arquivei.NumeroNF.ToString(), _arquivei.CNPJEmitente);

                            if (_cadastrado.Existe)
                            {
                                if (Convert.ToString((range.Cells[rCnt, 10] as Excel.Range).Value2) == "Cancelada" && _cadastrado.Status != "Cancelada")
                                {
                                    _cadastrado.Status = "Cancelada";
                                    _listaArquivei.Add(_cadastrado);
                                    NFCanceladas.Add(_cadastrado.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp") + 
                                                 _cadastrado.DataEmissao.ToShortDateString() + "&nbsp" +
                                                 _cadastrado.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                 _cadastrado.ChaveDeAcesso);
                                    continue;
                                }

                                quantidadeNFCadastradas++;
                                Log _log = new Log();
                                _log.IdUsuario = 1;
                                _log.Descricao = "NFSe " + _arquivei.NumeroNF + " com a Chave de Acesso " + _arquivei.ChaveDeAcesso + " já cadastrada para a empresa " + item.IdEmpresa;
                                _log.Tela = "Principal - Arquivei - NFS-e ja cadastrada";

                                try
                                {
                                    new LogBO().Gravar(_log);
                                }
                                catch { }

                               // if (!nomeArquivo.Contains("xlsCadastradoProcessado"))
                                //    nomeArquivo = nomeArquivo.Replace("xls", "xlsCadastradoProcessado");

                                //_arquiveiJaRenomeado = true;
                                continue;
                            }
                            else
                            {

                                if (_listaArquivei.Where(w => w.IdEmpresa == item.IdEmpresa &&
                                                              w.NumeroNF == _arquivei.NumeroNF &&
                                                              w.CNPJEmitente == _arquivei.CNPJEmitente).Count() != 0)
                                {
                                    //não inclui novamente para não duplicar
                                    continue;
                                }
                            }


                            #region tratativa data emissao
                            int mes = 0;

                            #region mês
                            if (nomeArquivo.Contains(@"\01\"))
                                mes = 1;
                            if (nomeArquivo.Contains(@"\02\"))
                                mes = 2;
                            if (nomeArquivo.Contains(@"\03\"))
                                mes = 3;
                            if (nomeArquivo.Contains(@"\04\"))
                                mes = 4;
                            if (nomeArquivo.Contains(@"\05\"))
                                mes = 5;
                            if (nomeArquivo.Contains(@"\06\"))
                                mes = 6;
                            if (nomeArquivo.Contains(@"\07\"))
                                mes = 7;
                            if (nomeArquivo.Contains(@"\08\"))
                                mes = 8;
                            if (nomeArquivo.Contains(@"\09\"))
                                mes = 9;
                            if (nomeArquivo.Contains(@"\10\"))
                                mes = 10;
                            if (nomeArquivo.Contains(@"\11\"))
                                mes = 11;
                            if (nomeArquivo.Contains(@"\12\"))
                                mes = 12;
                            #endregion
                            try
                            { 
                                // tenta a coluna 15
                                try
                                {
                                    _arquivei.DataEmissao =  Convert.ToDateTime(Convert.ToString((range.Cells[rCnt, 15] as Excel.Range).Value2));
                                }
                                catch
                                {
                                    _arquivei.DataEmissao = Convert.ToDateTime(Convert.ToString((range.Cells[rCnt, 15] as Excel.Range).Value));
                                }

                                //Verifica se o mes informado no arquivo é maior que o mes da pasta, se for tira um mes
                                if (_arquivei.DataEmissao.Month > mes)
                                    _arquivei.DataEmissao.AddMonths(-1);
                            }
                            catch
                            {
                                //Se não tiver usa a data e pega o mes pelo caminho do aquivo
                                int ano = (nomeArquivo.Contains(@"\"+ DateTime.Now.Year + @"\") ? DateTime.Now.Year : DateTime.Now.Year-1);
                                

                                _arquivei.DataEmissao = Convert.ToDateTime(Convert.ToString((range.Cells[rCnt, 4] as Excel.Range).Value2) + "/" + mes + "/" + ano);
                            }
                            #endregion

                            _arquivei.CodigoServico = Convert.ToString((range.Cells[rCnt, 7] as Excel.Range).Value2);
                            _arquivei.ValorServico = Convert.ToDecimal(Convert.ToString((range.Cells[rCnt, 5] as Excel.Range).Value2).Replace(".", ","));
                            _arquivei.ValorTotalNF = _arquivei.ValorServico;
                            _arquivei.AliquotaServico = Convert.ToDecimal(Convert.ToString((range.Cells[rCnt, 8] as Excel.Range).Value2).Replace(".", ","));

                            try
                            {
                                if (Convert.ToString((range.Cells[rCnt, 10] as Excel.Range).Value2) != "Não Retida")
                                    _arquivei.ValorISS = Convert.ToDecimal(Convert.ToString((range.Cells[rCnt, 9] as Excel.Range).Value2).Replace(".", ","));
                            }
                            catch { }

                            maskedEditBox1.Text = _arquivei.CNPJEmitente;

                            //testar formatação
                            if (Convert.ToString((range.Cells[rCnt, 10] as Excel.Range).Value2) == "Cancelada")
                            {
                                _arquivei.Status = "Cancelada";
                                NFCanceladas.Add(_arquivei.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp") + 
                                                _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                                                _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                _arquivei.ChaveDeAcesso);
                            }
                            else
                            {
                                _arquivei.Status = "Normal";
                                NFNovas.Add(_arquivei.NumeroNF.ToString().PadRight(13).Replace(" ", "&nbsp") + 
                                                _arquivei.DataEmissao.ToShortDateString() + "&nbsp" +
                                                _arquivei.CNPJEmitente.PadRight(18).Replace(" ", "&nbsp") +
                                                _arquivei.ChaveDeAcesso);
                            }

                            _listaArquivei.Add(_arquivei);
                            idArquivo++;

                        }

                    }
                    
                    try
                    {
                        xlWorkBook.Close(true, misValue, misValue);
                    }
                    catch
                    {
                    }

                    try
                    {
                        xlApp.Quit();
                    }
                    catch { }

                    #region Ação a ser tomada com o arquivo Arquivei
                    if (item.Acao == "E")
                    {
                        try
                        {
                                item.NomeArquivo = item.NomeArquivo.Replace("xls", "xlsProcessado");
                        }
                        catch (IOException)
                        {

                        }
                    }
                    else
                    {
                        try
                        {
                            item.NomeArquivo = item.NomeArquivo.Replace("xls", "xlsProcessado");
                            
                        }
                        catch
                        {

                        }
                    }
                    #endregion
                }

                #region Envia Email
                _empresa = new EmpresaBO().Consultar(itemE.Key);

                string[] _dadosEmail = new string[50];
                _dadosEmail[0] = quantidadeArquivos.ToString();
                _dadosEmail[1] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                _dadosEmail[2] = _empresa.NomeAbreviado;

                if (NFNovas.Count() != 0)
                {
                    _dadosEmail[3] = "Numero NFSe".PadRight(13).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(1).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in NFNovas)
                    {
                        _dadosEmail[3] = _dadosEmail[3] + item + "</br>";
                    }
                }

                _dadosEmail[4] = quantidadeNFCadastradas.ToString();
                _dadosEmail[6] = NFNovas.Count().ToString();
                _dadosEmail[7] = quantidadeComErroLeitura.ToString();
                _dadosEmail[8] = _diretorio;
                _dadosEmail[9] = NFCanceladas.Count().ToString();
                _dadosEmail[10] = CartaCorrecao.Count().ToString();

                if (NFCanceladas.Count() != 0)
                {
                    _dadosEmail[11] = "Numero NFSe".PadRight(13).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in NFCanceladas)
                    {
                        _dadosEmail[11] = _dadosEmail[11] + item + "</br>";
                    }
                }

                if (CartaCorrecao.Count() != 0)
                {
                    _dadosEmail[12] = "Numero NFSe".PadRight(13).Replace(" ", "&nbsp") +
                        "Emissão".PadRight(11).Replace(" ", "&nbsp") + 
                        "CNPJ Prestador".PadRight(18).Replace(" ", "&nbsp") +
                        "Chave de Acesso" +
                         "</br>";
                    foreach (var item in CartaCorrecao)
                    {
                        _dadosEmail[12] = _dadosEmail[12] + item + "</br>";
                    }
                }

                if (NFErros.Count() != 0)
                {
                    _dadosEmail[13] = "Arquivo" + "</br>";
                    foreach (var item in NFErros)
                    {
                        _dadosEmail[13] = _dadosEmail[13] + item + "</br>";
                    }
                }

                string emailDestino = "";

                foreach (var itemU in _listaUsuarios.Where(w => w.AcessaEscrituracaoFiscal))
                {
                    emailDestino = emailDestino + itemU.Email + "; ";
                }

                if (NFErros.Count() != 0)
                    emailDestino = "fflopes@supportse.com.br;mdmunoz@supportse.com.br";


                if (quantidadeArquivos != 0)
                    Classes.Publicas.EnviarEmailArquivei(_dadosEmail, emailDestino, "Arquivei - NFSe Recebidas - Excel" + (NFErros.Count() != 0 ? "Com Erros" : ""), false, true);

                #endregion

                NFNovas.Clear();
                NFCanceladas.Clear();
                CartaCorrecao.Clear();
                NFErros.Clear();

                if (_listaArquivei.Count != 0)
                    new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei);

                _listaArquivei.Clear();
            }

            if (_listaArquivei.Count != 0)
                new ArquiveiBO().Gravar(_listaArquivei, _listaItensArquivei);

            foreach (var item in _listaArquivosImportados)
            {
                new ArquiveiBO().GravarImportando(item);
            }

            //Close();
            #endregion
        } // Fim Excel Ribeirão
        
        #endregion


        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
