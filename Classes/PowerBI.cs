using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class PowerBI
    {
        public class EmailDeAcesso // Pbi_Emails
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string Nome { get; set; }
            public string Grupo { get; set; }
            public string Senha { get; set; }
            public DateTime Data { get; set; }
            public bool Ativo { get; set; }
            public bool Existe { get; set; }            
        }

        public class EmpresasAutorizadas // pbi_acessosporempresa
        {
            public int IdEmail { get; set; }
            public string CodigoEmpresa { get; set; }
            public string Empresa { get; set; }
            public bool Selecionado { get; set; }
            public bool SelAnterior { get; set; }
            public bool Existe { get; set; }
        }

        public class UsuariosAutorizados // Niff_Pbi_UsuariosPorEmail
        {
            public int Id { get; set; }
            public int IdEmail { get; set; }
            public int IdUsuario { get; set; }
            public string Nome { get; set; }
            public bool Existe { get; set; }
        }

        public class Relatorios // niff_pbi_Relatorios
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public bool Ativo { get; set; }
            public bool Existe { get; set; }
        }

        public class Acessos //niff_pbi_Acessos
        {
            public int Id { get; set; }
            public int IdEmail { get; set; }
            public string NomeEmail { get; set; }
            public int IdRelatorios { get; set; }
            public string Nome { get; set; }
            public DateTime Data { get; set; }
            public int Quantidade { get; set; }
            public bool Existe { get; set; }
        }

        public class Resumo
        {
            public int IdRelatorio { get; set; }
            public string Relatorio { get; set; }
            public int Total { get; set; }
            public DateTime Data { get; set; }
            public int? QuantidadeColuna1 { get; set; }
            public int? QuantidadeColuna2 { get; set; }
            public int? QuantidadeColuna3 { get; set; }
            public int? QuantidadeColuna4 { get; set; }
            public int? QuantidadeColuna5 { get; set; }
            public int? QuantidadeColuna6 { get; set; }
            public int? QuantidadeColuna7 { get; set; }
            public int? QuantidadeColuna8 { get; set; }
            public int? QuantidadeColuna9 { get; set; }
            public int? QuantidadeColuna10 { get; set; }
            public int? QuantidadeColuna11 { get; set; }
            public int? QuantidadeColuna12 { get; set; }
            public int? QuantidadeColuna13 { get; set; }
            public int? QuantidadeColuna14 { get; set; }
            public int? QuantidadeColuna15 { get; set; }
            public int? QuantidadeColuna16 { get; set; }
            public int? QuantidadeColuna17 { get; set; }
            public int? QuantidadeColuna18 { get; set; }
            public int? QuantidadeColuna19 { get; set; }
            public int? QuantidadeColuna20 { get; set; }
            public int? QuantidadeColuna21 { get; set; }
            public int? QuantidadeColuna22 { get; set; }
            public int? QuantidadeColuna23 { get; set; }
            public int? QuantidadeColuna24 { get; set; }
            public int? QuantidadeColuna25 { get; set; }
            public int? QuantidadeColuna26 { get; set; }
            public int? QuantidadeColuna27 { get; set; }
            public int? QuantidadeColuna28 { get; set; }
            public int? QuantidadeColuna29 { get; set; }
            public int? QuantidadeColuna30 { get; set; }

            public int idEmailColuna1 { get; set; }
            public int idEmailColuna2 { get; set; }
            public int idEmailColuna3 { get; set; }
            public int idEmailColuna4 { get; set; }
            public int idEmailColuna5 { get; set; }
            public int idEmailColuna6 { get; set; }
            public int idEmailColuna7 { get; set; }
            public int idEmailColuna8 { get; set; }
            public int idEmailColuna9 { get; set; }
            public int idEmailColuna10 { get; set; }
            public int idEmailColuna11 { get; set; }
            public int idEmailColuna12 { get; set; }
            public int idEmailColuna13 { get; set; }
            public int idEmailColuna14 { get; set; }
            public int idEmailColuna15 { get; set; }
            public int idEmailColuna16 { get; set; }
            public int idEmailColuna17 { get; set; }
            public int idEmailColuna18 { get; set; }
            public int idEmailColuna19 { get; set; }
            public int idEmailColuna20 { get; set; }
            public int idEmailColuna21 { get; set; }
            public int idEmailColuna22 { get; set; }
            public int idEmailColuna23 { get; set; }
            public int idEmailColuna24 { get; set; }
            public int idEmailColuna25 { get; set; }
            public int idEmailColuna26 { get; set; }
            public int idEmailColuna27 { get; set; }
            public int idEmailColuna28 { get; set; }
            public int idEmailColuna29 { get; set; }
            public int idEmailColuna30 { get; set; }

        }
    }
}
