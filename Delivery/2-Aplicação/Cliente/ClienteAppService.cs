using Delivery._2_Aplicação.Shared;
using Delivery._3_Domínio.Cliente;
using Delivery._3_Domínio.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery._2_Aplicação.Cliente
{
    public class ClienteAppService : Controlador<Clientes>
    {
        /*Criação da tabela
         CREATE TABLE [TBClientes] (
   [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
 , [Nome] text NOT NULL
 , [CPF] text NOT NULL
 , [Endereco] text NOT NULL
 , [Telefone] text NOT NULL
 , [Email] text NOT NULL
 );  

         */
        private const string sqlInserirCliente =
              @"INSERT INTO [TBCLIENTES]
                (
                    [NOME],       
                    [CPF],             
                    [ENDERECO],                    
                    [TELEFONE], 
                    [EMAIL]            
                )
            VALUES
                (
                    @NOME,
                    @CPF,
                    @ENDERECO,
                    @TELEFONE,
                    @EMAIL
                )";
        private const string sqlEditarCliente =
           @"UPDATE TBCLIENTES
                    SET
                        [NOME] = @NOME,
                        [CPF] = @CPF,
                        [ENDERECO] = @ENDERECO,
		                [TELEFONE] = @TELEFONE,
		                [EMAIL] = @EMAIL
                    WHERE 
                        ID = @ID";

        private const string sqlExcluirCliente =
            @"DELETE 
	                FROM
                        TBCLIENTES
                    WHERE 
                        ID = @ID";

        private const string sqlSelecionarClientePorId =
            @"SELECT
                        [ID],
		                [NOME], 
                        [CPF], 
		                [ENDERECO],
		                [TELEFONE],
		                [EMAIL]
	                FROM
                        TBCLIENTES
                    WHERE 
                        ID = @ID";

        private const string sqlSelecionarTodosCliente =
            @"SELECT
                        [ID],
		                [NOME], 
                        [CPF], 
		                [ENDERECO],
		                [TELEFONE],
		                [EMAIL]
	                FROM
                        TBCLIENTES ORDER BY ID;";

        private const string sqlExisteCliente =
            @"SELECT 
                COUNT(*) 
            FROM 
                [TBCLIENTES]
            WHERE 
                [ID] = @ID";

        public override string InserirNovo(Clientes registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = Db.Insert(sqlInserirCliente, ObtemParametrosClientes(registro));
            }

            return resultadoValidacao;
        }

        public override string Editar(int id, Clientes registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = id;
                Db.Update(sqlEditarCliente, ObtemParametrosClientes(registro));
            }

            return resultadoValidacao;
        }

        public override bool Excluir(int id)
        {
            try
            {
                Db.Delete(sqlExcluirCliente, AdicionarParametro("ID", id));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public override bool Existe(int id)
        {
            return Db.Exists(sqlExisteCliente, AdicionarParametro("ID", id));
        }

        public override Clientes SelecionarPorId(int id)
        {
            return Db.Get(sqlSelecionarClientePorId, ConverterEmClientes, AdicionarParametro("ID", id));
        }

        public override List<Clientes> SelecionarTodos()
        {
            return Db.GetAll(sqlSelecionarTodosCliente, ConverterEmClientes);
        }

        private Dictionary<string, object> ObtemParametrosClientes(Clientes cliente)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", cliente.Id);
            parametros.Add("NOME", cliente.nome);
            parametros.Add("CPF", cliente.cpf);
            parametros.Add("ENDERECO", cliente.endereco);
            parametros.Add("TELEFONE", cliente.telefone);
            parametros.Add("EMAIL", cliente.email);

            return parametros;
        }

        private Clientes ConverterEmClientes(IDataReader reader)
        {
            int id = Convert.ToInt32(reader["ID"]);
            string nome = Convert.ToString(reader["NOME"]);
            string cpf = Convert.ToString(reader["CPF"]);
            string endereco = Convert.ToString(reader["ENDERECO"]);
            string telefone = Convert.ToString(reader["TELEFONE"]);
            string email = Convert.ToString(reader["EMAIL"]);

            Clientes cliente = new Clientes(nome, cpf, endereco, telefone, email);

            cliente.Id = id;

            return cliente;
        }
    }
}
