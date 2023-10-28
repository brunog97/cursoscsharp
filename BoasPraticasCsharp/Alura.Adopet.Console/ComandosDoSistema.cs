using Alura.Adopet.Console.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Adopet.Console
{
    internal class ComandosDoSistema
    {
        private Dictionary<string, IComando> comandosDoSistema = new()
        {
            {"help",new Help() },
            {"import",new Importacao() },
            {"list",new Listar() },
            {"show",new Show() },
        };

        public IComando? this[string key] => comandosDoSistema.ContainsKey(key) ? comandosDoSistema[key] : null;
    }
}
