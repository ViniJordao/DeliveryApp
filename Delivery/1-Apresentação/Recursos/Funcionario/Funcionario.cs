using Delivery._2_Aplicação.Funcionario;
using Delivery._3_Domínio.Funcionário;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delivery._1_Apresentação.Recursos.Funcionario
{
    public partial class Funcionario : Form
    {
        FuncionarioAppService controladorFuncionarios = new FuncionarioAppService();
        Funcionarios funcionario = new Funcionarios();
        public Funcionario()
        {
            InitializeComponent();
            CarregarFuncionarios();
        }
        private void AdicionarFuncionarios()
        {
            string nome = textNome.Text;
            string cpf = maskedCPF.Text;
            string endereco = textEndereco.Text;
            string telefone = maskedTelefone.Text;
            string email = textEmail.Text;
            string cargo = textCargo.Text;
            int salario = Convert.ToInt32(numericSalario.Text);

            controladorFuncionarios.InserirNovo(new Funcionarios(nome, cpf, endereco, telefone, email,cargo,salario));

            CarregarFuncionarios();
            LimparCampos();
        }
        private void CarregarFuncionarios()
        {

            dtFuncionarios.Clear();
            List<Funcionarios> listaFuncionario = controladorFuncionarios.SelecionarTodos();


            foreach (var funcionario in listaFuncionario)
            {
                DataRow registro = dtFuncionarios.NewRow();

                registro["Id"] = funcionario.Id;
                registro["Nome"] = funcionario.nome;
                registro["CPF"] = funcionario.cpf;
                registro["Endereço"] = funcionario.endereco;
                registro["Telefone"] = funcionario.telefone;
                registro["E-mail"] = funcionario.email;
                registro["Cargo"] = funcionario.cargo;
                registro["Salário"] = funcionario.salario;
                

                dtFuncionarios.Rows.Add(registro);
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
         
        }

        private void textId_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Utlize o campo Id só para editar e excluir");
        }

        private void LimparCampos()
        {
            textNome.Clear();
            maskedCPF.Clear();
            textEndereco.Clear();
            maskedTelefone.Clear();
            textEmail.Clear(); 
            textCargo.Clear();
        }

        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            try
            {
                AdicionarFuncionarios();
            }
            catch (Exception)
            {

                MessageBox.Show("Insira os todos os campos", "Cadastro de Funcionários", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
            LimparCampos();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textId.Text);
                string nome = textNome.Text;
                string cpf = maskedCPF.Text;
                string endereco = textEndereco.Text;
                string telefone = maskedTelefone.Text;
                string email = textEmail.Text;
                string cargo = textCargo.Text;
                int salario = Convert.ToInt32(numericSalario.Text);

                controladorFuncionarios.Editar(id, new Funcionarios(nome, cpf, endereco, telefone, email, cargo, salario));
            }
            catch (Exception)
            {

                MessageBox.Show("Insira os todos os campos", "Edição de Funcionários", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        

            CarregarFuncionarios();
            LimparCampos();
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            try
            {
            int id = Convert.ToInt32(textId.Text);
            controladorFuncionarios.Excluir(id);

            }
            catch (Exception)
            {

                MessageBox.Show("Insira o Id para excluir", "Exclusão de Estabelecimento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            CarregarFuncionarios();
            LimparCampos();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Hide();
            TelaPrincipal form1 = new TelaPrincipal();
            form1.ShowDialog();
            this.Close();
        }
    }

}

