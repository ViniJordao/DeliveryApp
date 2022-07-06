using Delivery._2_Aplicação.Cliente;
using Delivery._2_Aplicação.Estabelecimento;
using Delivery._2_Aplicação.Funcionario;
using Delivery._2_Aplicação.Pedido;
using Delivery._2_Aplicação.Produto;
using Delivery._3_Domínio.Pedido;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Delivery._1_Apresentação.Recursos.PedidoWinForms
{
    public partial class PedidoWinForm : Form
    {
        ClienteAppService controladorClientes = new ClienteAppService();
        ProdutoAppService controladorProdutos = new ProdutoAppService();
        EstabelecimentoAppService controladorEstabelecimento = new EstabelecimentoAppService();
        FuncionarioAppService controladorFuncionario = new FuncionarioAppService();
        PedidosAppService controladorPedidos = new PedidosAppService();
        Pedidos pedidos = new Pedidos();
        public PedidoWinForm()
        {
            InitializeComponent();
            CarregarPedidos();
            CarregarClientes();
            CarregarEstabelecimento();
            CarregarProdutos();
            CarregarFuncioanrios();
        }

        private void AdicionarPedidos()
        {
            string cliente = Convert.ToString(cbCliente.SelectedItem);
            string estabelecimento = Convert.ToString(cbEstabelecimento.SelectedItem);
            string produtos = Convert.ToString(cbProduto.SelectedItem);
            string funcionarios = Convert.ToString(cbFuncionario.SelectedItem);

            controladorPedidos.InserirNovo(new Pedidos(cliente, estabelecimento, produtos, funcionarios));

            CarregarPedidos();
        }
        private void CarregarFuncioanrios()
        {
            var listaFuncionario = controladorFuncionario.SelecionarTodos();
            foreach (var funcionario in listaFuncionario)
            {
                cbFuncionario.Items.Add(funcionario.nome);
            }
        }
        private void CarregarProdutos()
        {
            var listaProdutos = controladorProdutos.SelecionarTodos();
            foreach (var produto in listaProdutos)
            {
                cbProduto.Items.Add(produto.tipoProduto);
            }
        }
        private void CarregarEstabelecimento()
        {
            var listaEstabelecimento = controladorEstabelecimento.SelecionarTodos();
            foreach (var estabelecimento in listaEstabelecimento)
            {
                cbEstabelecimento.Items.Add(estabelecimento.nome);
            }
        }
        private void CarregarClientes()
        {
            var listaClientes = controladorClientes.SelecionarTodos();
            foreach (var clientes in listaClientes)
            {
                cbCliente.Items.Add(clientes.nome);
            }
        }
        private void CarregarPedidos()
        {

            dtPedidos.Clear();
            List<Pedidos> listaPedidos = controladorPedidos.SelecionarTodos();


            foreach (var pedidos in listaPedidos)
            {
                DataRow registro = dtPedidos.NewRow();

                registro["Id"] = pedidos.Id;
                registro["Cliente"] = pedidos.clienteId;
                registro["Estabelecimento"] = pedidos.estabelecimentoId;
                registro["Produto"] = pedidos.produtosId;
                registro["Funcionario"] = pedidos.funcionariosId;


                dtPedidos.Rows.Add(registro);
            }

        }



        private void textId_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Utlize o campo Id só para editar e excluir");
        }

        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            try
            {
            AdicionarPedidos();

            }
            catch (Exception)
            {

                MessageBox.Show("Insira os todos os campos", "Cadastro de Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;
            }
           
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (textId.Text == null) 
            {
                MessageBox.Show("Vazio");
            }
            else
            {
                EditarPedido();
            }
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            try
            {

            int id = Convert.ToInt32(textId.Text);
            controladorPedidos.Excluir(id);
            }
            catch (Exception)
            {

                MessageBox.Show("Insira o Id para excluir", "Exclusão de Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            CarregarPedidos();
        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            TelaPrincipal form1 = new TelaPrincipal();
            form1.ShowDialog();
            this.Close();
        }
        private void EditarPedido()
        {
            try
            {
            int id = Convert.ToInt32(textId.Text);
            string cliente = Convert.ToString(cbCliente.SelectedItem);
            string estabelecimento = Convert.ToString(cbEstabelecimento.SelectedItem);
            string produtos = Convert.ToString(cbProduto.SelectedItem);
            string funcionarios = Convert.ToString(cbFuncionario.SelectedItem);
            controladorPedidos.Editar(id, new Pedidos(cliente, estabelecimento, produtos, funcionarios));

            }
            catch (Exception)
            {

                MessageBox.Show("Insira os todos os campos", "Edição de Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
            }

            CarregarPedidos();
        }
    }
}
