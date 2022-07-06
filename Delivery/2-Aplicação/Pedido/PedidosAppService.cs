using Delivery._2_Aplicação.Cliente;
using Delivery._2_Aplicação.Estabelecimento;
using Delivery._2_Aplicação.Funcionario;
using Delivery._2_Aplicação.Produto;
using Delivery._2_Aplicação.Shared;
using Delivery._3_Domínio.Cliente;
using Delivery._3_Domínio.Estabelecimento;
using Delivery._3_Domínio.Funcionário;
using Delivery._3_Domínio.Pedido;
using Delivery._3_Domínio.Produto;
using Delivery._3_Domínio.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery._2_Aplicação.Pedido
{
    public class PedidosAppService : Controlador<Pedidos>
    {
        EstabelecimentoAppService controladorEstabelecimento = new EstabelecimentoAppService();
        ClienteAppService controladorCliente = new ClienteAppService();
        FuncionarioAppService controladorFuncionario = new FuncionarioAppService();
        ProdutoAppService controladorProduto = new ProdutoAppService();

        /*Criação da tabela
         CREATE TABLE [TBPedidos] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Cliente] text  NOT NULL
, [Estabelecimento] text  NOT NULL
, [Funcionario] text  NOT NULL
, [Produto] text  NOT NULL
); 
         */

        private const string sqlInserirCliente =
              @"INSERT INTO [TBPEDIDOS]
                (
                    [CLIENTE],       
                    [ESTABELECIMENTO],             
                    [FUNCIONARIO],                    
                    [PRODUTO]          
                )
            VALUES
                (
                    @CLIENTE,
                    @ESTABELECIMENTO,
                    @FUNCIONARIO,
                    @PRODUTO
                )";
        private const string sqlEditarCliente =
           @"UPDATE TBPEDIDOS
                    SET
                        [CLIENTE] = @ID_CLIENTE,
                        [ESTABELECIMENTO] = @ID_ESTABELECIMENTO,
                        [FUNCIONARIO] = @ID_FUNCIONARIO,
		                [PRODUTO] = @ID_PRODUTO
                    WHERE 
                        ID = @ID";

        private const string sqlExcluirCliente =
            @"DELETE 
	                FROM
                        TBPEDIDOS
                    WHERE 
                        ID = @ID";

        private const string sqlSelecionarClientePorId =
            @"SELECT
                        [ID],
		                [CLIENTE], 
                        [ESTABELECIMENTO], 
		                [FUNCIONARIO],
		                [PRODUTO]
	                FROM
                        TBPEDIDOS
                    WHERE 
                        ID = @ID";

        private const string sqlSelecionarTodosCliente =
            @"SELECT
                        [ID],
		                [CLIENTE], 
                        [ESTABELECIMENTO], 
		                [FUNCIONARIO],
		                [PRODUTO]
	                FROM
                        TBPEDIDOS ORDER BY ID;";

        private const string sqlExisteCliente =
            @"SELECT 
                COUNT(*) 
            FROM 
                [TBPEDIDOS]
            WHERE 
                [ID] = @ID";

        public override string InserirNovo(Pedidos registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = Db.Insert(sqlInserirCliente, ObtemParametrosPedidos(registro));
            }

            return resultadoValidacao;
        }

        public override string Editar(int id, Pedidos registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = id;
                Db.Update(sqlEditarCliente, ObtemParametrosPedidos(registro));
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

        public override Pedidos SelecionarPorId(int id)
        {
            return Db.Get(sqlSelecionarClientePorId, ConverterEmPedidos, AdicionarParametro("ID", id));
        }

        public override List<Pedidos> SelecionarTodos()
        {
            return Db.GetAll(sqlSelecionarTodosCliente, ConverterEmPedidos);
        }

        private Dictionary<string, object> ObtemParametrosPedidos(Pedidos pedido)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", pedido.Id);
            parametros.Add("CLIENTE", pedido.clienteId);
            parametros.Add("ESTABELECIMENTO", pedido.estabelecimentoId); ;
            parametros.Add("FUNCIONARIO", pedido.funcionariosId);
            parametros.Add("PRODUTO", pedido.produtosId);

            return parametros;
        }

        private Pedidos ConverterEmPedidos(IDataReader reader)
        {
            var id = Convert.ToInt32(reader["ID"]);
            var cliente = Convert.ToString(reader["CLIENTE"]);
            var estabelecimento = Convert.ToString(reader["ESTABELECIMENTO"]);
            var funcionario = Convert.ToString(reader["FUNCIONARIO"]);
            var produto = Convert.ToString(reader["PRODUTO"]);

            //string cliente = controladorCliente.SelecionarPorId(id_cliente);
            //string estabelecimento = controladorEstabelecimento.SelecionarPorId(id_estabelecimento);
            //string produto = controladorProduto.SelecionarPorId(id_produto);
            //string funcionario = controladorFuncionario.SelecionarPorId(id_funcionario);

            Pedidos pedidos = new Pedidos(cliente, estabelecimento, produto,funcionario);

            pedidos.Id = id;

            return pedidos;
        }
    }
}

