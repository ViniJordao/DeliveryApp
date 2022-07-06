using Delivery._2_Aplicação.Cliente;
using Delivery._3_Domínio.Cliente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delivery._1_Apresentação.Recursos.Cliente
{
    public partial class Cliente : Form
    {
        ClienteAppService controladorCliente = new ClienteAppService();
        Clientes cliente = new Clientes();
        public Cliente()
        {
            InitializeComponent();
            CarregarClientes();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                AdicionarCliente();

            }
            catch (Exception)
            {

                MessageBox.Show("Insira os todos os campos", "Cadastro de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
    
        }

        private void AdicionarCliente()
        {
            string nome = textNome.Text;
            string cpf = maskedCPF.Text;
            string endereco = textEndereco.Text;
            string telefone = maskedTelefone.Text;
            string email = textEmail.Text;

            controladorCliente.InserirNovo(new Clientes(nome, cpf, endereco, telefone, email));

            CarregarClientes();
            LimparCampos();
        }

        private void CarregarClientes()
        {

            dtClientes.Clear();
            List<Clientes> listaCliente = controladorCliente.SelecionarTodos();


            foreach (var cliente in listaCliente)
            {
                DataRow registro = dtClientes.NewRow();

                registro["Id"] = cliente.Id;
                registro["Nome"] = cliente.nome;
                registro["CPF"] = cliente.cpf;
                registro["Endereço"] = cliente.endereco;
                registro["Telefone"] = cliente.telefone;
                registro["Email"] = cliente.email;

                dtClientes.Rows.Add(registro);
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textId.Text);
                string nome = textNome.Text;
                string cpf = maskedCPF.Text;
                string endereco = textEndereco.Text;
                string telefone = maskedTelefone.Text;
                string email = textEmail.Text;

                controladorCliente.Editar(id, new Clientes(nome, cpf, endereco, telefone, email));
            }
            catch (Exception)
            {

                MessageBox.Show("Insira os todos os campos", "Edição de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           

            CarregarClientes();
            LimparCampos();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
            int id = Convert.ToInt32(textId.Text);
            controladorCliente.Excluir(id);

            }
            catch (Exception)
            {

                MessageBox.Show("Insira o Id que deseja excluir", "Exclusão de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            CarregarClientes();
            LimparCampos();
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
            textEmail.Clear(); ;
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
