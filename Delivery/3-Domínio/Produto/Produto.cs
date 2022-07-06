using Delivery._1_Apresentação.Recursos.Estabelecimento;
using Delivery._3_Domínio.Estabelecimento;
using Delivery._3_Domínio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery._3_Domínio.Produto
{
    public class Produtos : EntidadeBase
    {
        public Produtos()
        {
        }

        public Produtos(string estabelecimento, string tipo, string valor)
        {
            this.estabelecimento = estabelecimento;
            this.tipoProduto = tipo;
            this.valor = valor;
        }

        public string estabelecimento { get; set; }

        public string tipoProduto { get; set; }

        public string valor { get; set; }

        public override string Validar()
        {
            string resultadoValidacao = "";
            if (resultadoValidacao == "")
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao; 
        }
    }
}