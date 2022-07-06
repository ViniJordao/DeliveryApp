using Delivery._2_Aplicação.Produto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Delivery._3_Domínio.Produto;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Delivery._3_Domínio.Estabelecimento;
using Delivery._2_Aplicação.Estabelecimento;

namespace Delivery._1_Apresentação.Recursos.Produto
{
    public partial class Produto : Form
    {
        ProdutoAppService controladorProdutos = new ProdutoAppService();
        Produtos produtos = new Produtos();
        EstabelecimentoAppService controladorEstabelecimento = new EstabelecimentoAppService(); 
        public Produto()
        {
            InitializeComponent();
            CarregarProdutos();
            CarregarEstabelecimentos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
            int id = Convert.ToInt32(textId.Text);
            string estabelecimentos = Convert.ToString(cbEstabelecimento.SelectedItem);
            string tipo = cbTipo.SelectedItem.ToString();
            string valor = Convert.ToString(numericValor.Text);

            controladorProdutos.Editar(id, new Produtos(estabelecimentos, tipo, valor));

            }
            catch (Exception)
            {

                MessageBox.Show("Insira os todos os campos", "Edição de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            CarregarProdutos();
            LimparCampos();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Hide();
            TelaPrincipal form1 = new TelaPrincipal();
            form1.ShowDialog();
            this.Close();
        }
        private void CarregarEstabelecimentos()
        {
            var listaEstabelecimento = controladorEstabelecimento.SelecionarTodos();
            foreach (var estabelecimento in listaEstabelecimento)
            {
                cbEstabelecimento.Items.Add(estabelecimento.nome);
            }
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
            int id = Convert.ToInt32(textId.Text);
            controladorProdutos.Excluir(id);

            }
            catch (Exception)
            {

                MessageBox.Show("Insira o Id para excluir", "Exclusão de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            CarregarProdutos();
            LimparCampos();
        }
     
    

        private void CarregarProdutos()
        {

            dtProdutos.Clear();
            List<Produtos> listaProdutos = controladorProdutos.SelecionarTodos();


            foreach (var produtos in listaProdutos)
            {
                DataRow registro = dtProdutos.NewRow();

                registro["Id"] = produtos.Id;
                registro["Estabelecimento"] = produtos.estabelecimento;
                registro["Tipo"] = produtos.tipoProduto;
                registro["Preço"] = produtos.valor;

                dtProdutos.Rows.Add(registro);
            }

        }
        private void textId_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Utlize o campo Id só para editar e excluir");
        }

        private void LimparCampos()
        {
            
           
        }

        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            try
            {
                string estabelecimentos = Convert.ToString(cbEstabelecimento.SelectedItem);
                string tipo = Convert.ToString(cbTipo.SelectedItem);
                string valor = Convert.ToString(numericValor.Text);

                controladorProdutos.InserirNovo(new Produtos(estabelecimentos, tipo, valor));
            }
            catch (Exception)
            {

                MessageBox.Show("Insira os todos os campos", "Cadastro de Produtos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            LimparCampos();
        }
    }
}
