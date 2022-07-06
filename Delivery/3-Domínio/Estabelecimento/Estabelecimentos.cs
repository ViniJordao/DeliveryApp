using Delivery._3_Domínio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery._3_Domínio.Estabelecimento
{
    public class Estabelecimentos : EntidadeBase
    {
        public Estabelecimentos()
        {
        }
        public Estabelecimentos(string nome)
        {
            this.nome = nome;
        }

        public Estabelecimentos(string nome, string tipo, string endereco, string telefone)
        {
            this.nome = nome;
            this.tipo = tipo;
            this.endereco = endereco;
            this.telefone = telefone;
        }

        public string nome { get; set; }
        public string tipo { get; set; }
        public string endereco { get; set; }

        public string telefone { get; set; }

        public override string Validar()
        {
            string resultadoValidacao = "";
            if (resultadoValidacao == "")
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao; ;
        }
    }
}