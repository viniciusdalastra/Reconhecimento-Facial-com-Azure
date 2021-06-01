using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Model
{
    class ModelReconhecimento
    {
        public String identificador { get; set; }
        public DateTime data { get; set; }
        public decimal sequencia { get; set; }

        //identificador
        public String codigo_identificador;

        //pessoa
        public int codigo_pessoa;
        public int codigo_universitario_pessoa;
        public String nome_pessoa;
    }
}
