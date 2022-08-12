using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AtendimentoBO
    {
        public List<int> Datas()
        {
            return new AtendimentoDAO().Datas();
        }

        public List<Atendimento> Listar(int empresa,
                                        Publicas.TelaPesquisaSAC telaQueChamou,
                                        int departamento = 0, int ano = 0, string status = "T")
        {
            return new AtendimentoDAO().Listar(empresa,
                                        telaQueChamou,
                                        departamento, ano, status);
        }

        public Atendimento Consultar(string codigo, int idEmpresa)
        {
            return new AtendimentoDAO().Consulta(codigo,idEmpresa);
        }

        public bool Gravar(Atendimento atendimento, Atendimento atendimentolog, List<Atendimento.Anexos> anexos)
        {
            return new AtendimentoDAO().Grava(atendimento, atendimentolog, anexos);
        }

        public bool Excluir(Atendimento atendimento)
        {
            return new AtendimentoDAO().Exclui(atendimento);
        }

        public int Proximo()
        {
            return new AtendimentoDAO().Proximo();
        }

        public string ProximoCodigo(Publicas.TipoCalculoCodigoSAC tipo, string separador, DateTime data, int empresa)
        {
            return new AtendimentoDAO().ProximoCodigo(tipo, separador, data, empresa);
        }

        public List<Classes.Atendimento.Anexos> Listar(int IdAtendimento)
        {
            return new AtendimentoDAO().Listar(IdAtendimento);
        }

        public bool Excluir(string NomeArquivo)
        {
            return new AtendimentoDAO().Excluir(NomeArquivo);
        }
    }
}
