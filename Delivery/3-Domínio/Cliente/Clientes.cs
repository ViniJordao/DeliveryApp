using Delivery._3_Domínio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Delivery._3_Domínio.Cliente
{
    public class Clientes : EntidadeBase
    {
        public Clientes()
        {
        }

        public Clientes(string nome, string cpf, string endereco, string telefone, string email)
        {
            this.nome = nome;
            this.cpf = cpf;
            this.endereco = endereco;
            this.telefone = telefone;
            this.email = email;
        }

        public string nome { get; set; }

        public string cpf { get; set; }

        public string endereco { get; set; }

        public string telefone { get; set; }

        public string email { get; set; }

        public override string Validar()
        {
            string resultadoValidacao = "";
            if (resultadoValidacao == "")
                resultadoValidacao = "ESTA_VALIDO";

            return resultadoValidacao;
        }
    }
}

