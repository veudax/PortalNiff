using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ParametrosArquiveiBO
    {
        public List<ParametrosArquivei> Listar()
        {
            return new ParametrosArquiveiDAO().Listar();
        }

        public ParametrosArquivei Consultar(int IdEmpresa)
        {
            return new ParametrosArquiveiDAO().Consultar(IdEmpresa);
        }

        public bool Gravar(ParametrosArquivei parametros, List<ItensParametrosArquivei> itens)
        {
            return new ParametrosArquiveiDAO().Grava(parametros, itens);
        }

        public bool Excluir(int id)
        {
            return new ParametrosArquiveiDAO().Exclui(id);
        }
    }
}
