using Delivery._2_Aplicação.Shared;
using Delivery._3_Domínio.Estabelecimento;
using Delivery._3_Domínio.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery._2_Aplicação.Estabelecimento
{
    public class EstabelecimentoAppService : Controlador<Estabelecimentos>
    {
        /*Criação da tabela
         CREATE TABLE [TBEstabelecimento] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Nome] text NOT NULL
, [Tipo] text NOT NULL
, [Endereco] text NOT NULL
, [Telefone] text NOT NULL
);
         */
        private const string sqlInserirEstabelecimento =
              @"INSERT INTO [TBESTABELECIMENTO]
                (
                    [NOME],       
                    [TIPO],             
                    [ENDERECO],                    
                    [TELEFONE]         
                )
            VALUES
                (
                    @NOME,
                    @TIPO,
                    @ENDERECO,
                    @TELEFONE
                )";
        private const string sqlEditarEstabelecimento =
           @"UPDATE TBESTABELECIMENTO
                    SET
                        [NOME] = @NOME,
                        [TIPO] = @TIPO,
                        [ENDERECO] = @ENDERECO,
		                [TELEFONE] = @TELEFONE
                    WHERE 
                        ID = @ID";

        private const string sqlExcluirEstabelecimento =
            @"DELETE 
	                FROM
                        TBESTABELECIMENTO
                    WHERE 
                        ID = @ID";

        private const string sqlSelecionarEstabelecimentoPorId =
            @"SELECT
                        [ID],
		                [NOME], 
                        [TIPO], 
		                [ENDERECO],
		                [TELEFONE]
	                FROM
                        TBESTABELECIMENTO
                    WHERE 
                        ID = @ID";

        private const string sqlSelecionarTodosEstabelecimentos =
            @"SELECT
                        [ID],
		                [NOME], 
                        [TIPO], 
		                [ENDERECO],
		                [TELEFONE]
	                FROM
                        TBESTABELECIMENTO ORDER BY ID;";

        private const string sqlExisteEstabelecimento =
            @"SELECT 
                COUNT(*) 
            FROM 
                [TBESTABELECIMENTO]
            WHERE 
                [ID] = @ID";
      
        public override string InserirNovo(Estabelecimentos registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = Db.Insert(sqlInserirEstabelecimento, ObtemParametrosestabelecimentos(registro));
            }

            return resultadoValidacao;
        }

        public override string Editar(int id, Estabelecimentos registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = id;
                Db.Update(sqlEditarEstabelecimento, ObtemParametrosestabelecimentos(registro));
            }

            return resultadoValidacao;
        }

        public override bool Excluir(int id)
        {
            try
            {
                Db.Delete(sqlExcluirEstabelecimento, AdicionarParametro("ID", id));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public override bool Existe(int id)
        {
            return Db.Exists(sqlExisteEstabelecimento, AdicionarParametro("ID", id));
        }

        public override Estabelecimentos SelecionarPorId(int id)
        {
            return Db.Get(sqlSelecionarEstabelecimentoPorId, ConverterEmEstabelecimentos, AdicionarParametro("ID", id));
        }

        public override List<Estabelecimentos> SelecionarTodos()
        {
            return Db.GetAll(sqlSelecionarTodosEstabelecimentos, ConverterEmEstabelecimentos);
        }

        private Dictionary<string, object> ObtemParametrosestabelecimentos(Estabelecimentos estabelecimento)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", estabelecimento.Id);
            parametros.Add("NOME", estabelecimento.nome);
            parametros.Add("TIPO", estabelecimento.tipo);
            parametros.Add("ENDERECO", estabelecimento.endereco);
            parametros.Add("TELEFONE", estabelecimento.telefone);

            return parametros;
        }

        private Estabelecimentos ConverterEmEstabelecimentos(IDataReader reader)
        {
            int id = Convert.ToInt32(reader["ID"]);
            string nome = Convert.ToString(reader["NOME"]);
            string tipo = Convert.ToString(reader["TIPO"]);
            string endereco = Convert.ToString(reader["ENDERECO"]);
            string telefone = Convert.ToString(reader["TELEFONE"]);

            Estabelecimentos estabelecimento = new Estabelecimentos(nome, tipo, endereco, telefone);

            estabelecimento.Id = id;

            return estabelecimento;
        }
    }
}

