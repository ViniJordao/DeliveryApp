using Delivery._1_Apresentação.Recursos.Cliente;
using Delivery._1_Apresentação.Recursos.Estabelecimento;
using Delivery._1_Apresentação.Recursos.Funcionario;
using Delivery._1_Apresentação.Recursos.PedidoWinForms;
using Delivery._1_Apresentação.Recursos.Produto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delivery
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cliente cliente = new Cliente();
            cliente.Show();
        }

        private void btnFuncionarios_Click(object sender, EventArgs e)
        {
            this.Hide();
            Funcionario funcionario = new Funcionario();
            funcionario.Show();
        }

        private void btnEstabelecimentos_Click(object sender, EventArgs e)
        {
            this.Hide();
            Estabelecimento estabelecimento = new Estabelecimento();
            estabelecimento.Show();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            this.Hide();
            Produto produtos = new Produto();
            produtos.Show();
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            this.Hide();
            PedidoWinForm pedidos = new PedidoWinForm();
            pedidos.Show();
        }
    }
}
