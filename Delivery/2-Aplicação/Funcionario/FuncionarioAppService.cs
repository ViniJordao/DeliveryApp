using Delivery._2_Aplicação.Shared;
using Delivery._3_Domínio.Funcionário;
using Delivery._3_Domínio.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery._2_Aplicação.Funcionario
{
    public class FuncionarioAppService : Controlador<Funcionarios>
    {
        /*Criação da tabela
         CREATE TABLE [TBFuncionario] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Nome] text NOT NULL
, [CPF] text NOT NULL
, [Endereco] text NOT NULL
, [Telefone] text NOT NULL
, [Email] text NOT NULL
, [Cargo] text NOT NULL
, [Salario] text NOT NULL
);
         */

        private const string sqlInserirFuncionario =
              @"INSERT INTO [TBFUNCIONARIO]
                (
                    [NOME],       
                    [CPF],             
                    [ENDERECO],                    
                    [TELEFONE], 
                    [EMAIL],
                    [CARGO],
                    [SALARIO]
                )
            VALUES
                (
                    @NOME,
                    @CPF,
                    @ENDERECO,
                    @TELEFONE,
                    @EMAIL,
                    @CARGO,
                    @SALARIO
                )";
        private const string sqlEditarFuncionario =
           @"UPDATE TBFUNCIONARIO
                    SET
                        [NOME] = @NOME,
                        [CPF] = @CPF,
                        [ENDERECO] = @ENDERECO,
		                [TELEFONE] = @TELEFONE,
		                [EMAIL] = @EMAIL,
                        [CARGO] = @CARGO,
                        [SALARIO] = @SALARIO
                    WHERE 
                        ID = @ID";

        private const string sqlExcluirFuncionario =
            @"DELETE 
	                FROM
                        TBFUNCIONARIO
                    WHERE 
                        ID = @ID";

        private const string sqlSelecionarFuncionarioPorId =
            @"SELECT
                        [ID],
		                [NOME], 
                        [CPF], 
		                [ENDERECO],
		                [TELEFONE],
		                [EMAIL],
                        [CARGO],
                        [SALARIO]
	                FROM
                        TBFUNCIONARIO
                    WHERE 
                        ID = @ID";

        private const string sqlSelecionarTodosFuncionario =
            @"SELECT
                        [ID],
		                [NOME], 
                        [CPF], 
		                [ENDERECO],
		                [TELEFONE],
		                [EMAIL],
                        [CARGO],
                        [SALARIO]
	                FROM
                        TBFUNCIONARIO ORDER BY ID;";

        private const string sqlExisteFuncionario =
            @"SELECT 
                COUNT(*) 
            FROM 
                [TBFUNCIONARIO]
            WHERE 
                [ID] = @ID";
       
        public override string InserirNovo(Funcionarios registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = Db.Insert(sqlInserirFuncionario, ObtemParametrosFuncionarios(registro));
            }

            return resultadoValidacao;
        }

        public override string Editar(int id,Funcionarios registro)
        {
            string resultadoValidacao = registro.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                registro.Id = id;
                Db.Update(sqlEditarFuncionario, ObtemParametrosFuncionarios(registro));
            }

            return resultadoValidacao;
        }

        public override bool Excluir(int id)
        {
            try
            {
                Db.Delete(sqlExcluirFuncionario, AdicionarParametro("ID", id));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public override bool Existe(int id)
        {
            return Db.Exists(sqlExisteFuncionario, AdicionarParametro("ID", id));
        }

        public override Funcionarios SelecionarPorId(int id)
        {
            return Db.Get(sqlSelecionarFuncionarioPorId, ConverterEmFuncionarios, AdicionarParametro("ID", id));
        }

        public override List<Funcionarios> SelecionarTodos()
        {
            return Db.GetAll(sqlSelecionarTodosFuncionario, ConverterEmFuncionarios);
        }

        private Dictionary<string, object> ObtemParametrosFuncionarios(Funcionarios funcionario)
        {
            var parametros = new Dictionary<string, object>();

            parametros.Add("ID", funcionario.Id);
            parametros.Add("NOME", funcionario.nome);
            parametros.Add("CPF", funcionario.cpf);
            parametros.Add("ENDERECO", funcionario.endereco);
            parametros.Add("TELEFONE", funcionario.telefone);
            parametros.Add("EMAIL", funcionario.email);
            parametros.Add("CARGO", funcionario.cargo);
            parametros.Add("SALARIO", funcionario.salario);

            return parametros;
        }

        private Funcionarios ConverterEmFuncionarios(IDataReader reader)
        {
            int id = Convert.ToInt32(reader["ID"]);
            string nome = Convert.ToString(reader["NOME"]);
            string cpf = Convert.ToString(reader["CPF"]);
            string endereco = Convert.ToString(reader["ENDERECO"]);
            string telefone = Convert.ToString(reader["TELEFONE"]);
            string email = Convert.ToString(reader["EMAIL"]);
            string cargo = Convert.ToString(reader["CARGO"]);
            int salario = Convert.ToInt32(reader["SALARIO"]);

            Funcionarios funcionario = new Funcionarios(nome, cpf, endereco, telefone, email,cargo,salario);

            funcionario.Id = id;

            return funcionario;
        }
    }
}
