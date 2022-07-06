using Delivery._3_Domínio.Cliente;
using Delivery._3_Domínio.Estabelecimento;
using Delivery._3_Domínio.Funcionário;
using Delivery._3_Domínio.Produto;
using Delivery._3_Domínio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery._3_Domínio.Pedido
{
    public class Pedidos : EntidadeBase
    {
        public Pedidos()
        {
        }

        public Pedidos(string clienteId, string estabelecimentoId, string produtosId, string funcionariosId)
        {
            this.clienteId = clienteId;
            this.estabelecimentoId = estabelecimentoId;
            this.produtosId = produtosId;
            this.funcionariosId = funcionariosId;
        }

        public string clienteId { get; set; }
        public string estabelecimentoId { get; set; }

        public string produtosId { get; set; }

        public string funcionariosId { get; set; }

        public override string Validar()
        {
            string resultadoValidacao = "";
            if (resultadoValidacao == "")
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }
    }
}
