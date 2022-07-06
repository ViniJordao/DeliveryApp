using Delivery._2_Aplicação.Estabelecimento;
using Delivery._2_Aplicação.Shared;
using Delivery._3_Domínio.Estabelecimento;
using Delivery._3_Domínio.Produto;
using Delivery._3_Domínio.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery._2_Aplicação.Produto
{
    public class ProdutoAppService : Controlador<Produtos>
    {
        /*Criação da tabela
         CREATE TABLE [TBProduto] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Estabelecimento] text  NOT NULL
, [Tipo] text  NOT NULL
, [Valor] text  NOT NULL
);

         */
        private const string sqlInserirProduto =
              @"INSERT INTO [TBPRODUTO]
                (
                    [ESTABELECIMENTO],       
                    [TIPO],             
                    [VALOR]           
                )
            VALUES
                (
                    @ESTABELECIMENTO,
                    @TIPO,
                    @VALOR
                )";
        private const string sqlEditarProduto =
           @"UPDATE TBPRODUTO
                    SET
                        [ESTABELECIMENTO] = @ESTABELECIMENTO,
                        [TIPO] = @TIPO,
                        [VALOR] = @VALOR
                    WHERE 
                        ID = @ID";

        private const string sqlExcluirProduto =
            @"DELETE 
	                FROM
                        TBPRODUTO
                    WHERE 
                        ID = @ID";

        private const string sqlSelecionarProdutoPorId =
         @"SELECT
                        [ID],
                        [ESTABELECIMENTO], 
		                [TIPO],
		                [VALOR]
	                FROM
                        TBPRODUTO
                    WHERE 
                        ID = @ID";


        private const string sqlSelecionarTodosProdutos =
            @"SELECT * FROM [TBPRODUTO]";
                   



        private const string sqlExisteProduto =
            @"SELECT 
                COUNT(*) 
            FROM 
                [TBPRODUTO]
            WHERE 
                [ID] = @ID";
      
        public override string InserirNovo(Produtos registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = Db.Insert(sqlInserirProduto, ObtemParametrosProdutos(registro));
            }

            return resultadoValidacao;
        }

        public override string Editar(int id, Produtos registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = id;
                Db.Update(sqlEditarProduto, ObtemParametrosProdutos(registro));
            }

            return resultadoValidacao;
        }

        public override bool Excluir(int id)
        {
            try
            {
                Db.Delete(sqlExcluirProduto, AdicionarParametro("ID", id));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public override bool Existe(int id)
        {
            return Db.Exists(sqlExisteProduto, AdicionarParametro("ID", id));
        }

        public override Produtos SelecionarPorId(int id)
        {
            return Db.Get(sqlSelecionarProdutoPorId, ConverterEmProdutos, AdicionarParametro("ID", id));
        }

        public override List<Produtos> SelecionarTodos()
        {
            return Db.GetAll(sqlSelecionarTodosProdutos, ConverterEmProdutos);
        }

        private Dictionary<string, object> ObtemParametrosProdutos(Produtos produto)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", produto.Id);
            parametros.Add("ESTABELECIMENTO", produto.estabelecimento);
            parametros.Add("TIPO", produto.tipoProduto);
            parametros.Add("VALOR", produto.valor);

            return parametros;
        }

        private Produtos ConverterEmProdutos(IDataReader reader)
        {

            //var tipo = Convert.ToString(reader["NOME"]);
            //var nome = Convert.ToString(reader["TIPO"]);
            //var endereco = Convert.ToString(reader["ENDERECO"]);
            //var telefone = Convert.ToString(reader["TELEFONE"]);
            
            //if (reader["ESTABELECIMENTO"] != DBNull.Value)
            //{
            //    //estabelecimentos = new Estabelecimentos(nome, tipo, endereco, telefone);
            //    estabelecimentos.Id = Convert.ToInt32(reader["ESTABELECIMENTO"]);
            //}
            var estabelecimentos = Convert.ToString(reader["ESTABELECIMENTO"]);
            var tipoProduto = Convert.ToString(reader["TIPO"]);

            var valor = Convert.ToString(reader["VALOR"]);

           Produtos produto = new Produtos(estabelecimentos, tipoProduto, valor);
            
            
            //produto.Id = Convert.ToInt32(reader["ID"]); 
           
          

          return produto;

        }
    }
}

