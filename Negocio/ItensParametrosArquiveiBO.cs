using Classes;
using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ItensParametrosArquiveiBO
    {
        public List<ItensParametrosArquivei> Listar(int IdParametro)
        {
            return new ItensParametrosArquiveiDAO().Listar(IdParametro);
        }

        public bool Gravar(List<ItensParametrosArquivei> itens)
        {
            return new ItensParametrosArquiveiDAO().Grava(itens);
        }

        public bool Excluir(int id)
        {
            return new ItensParametrosArquiveiDAO().Exclui(id);
        }
    }
}
