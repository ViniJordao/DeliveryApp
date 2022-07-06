using Delivery._2_Aplicação.Estabelecimento;
using Delivery._3_Domínio.Estabelecimento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delivery._1_Apresentação.Recursos.Estabelecimento
{
    public partial class Estabelecimento : Form
    {
        EstabelecimentoAppService controladorEstabelecimentos = new EstabelecimentoAppService();
        Estabelecimentos estabelecimento = new Estabelecimentos();

        public Estabelecimento()
        {
            InitializeComponent();
            CarregarEstabelecimentos();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                AdicionarEstabelecimento();
            }
            catch (Exception)
            {

                MessageBox.Show("Insira os todos os campos", "Cadastro de Estabelecimento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
            int id = Convert.ToInt32(textId.Text);
            string nome = textNome.Text;
            string tipo = cbTipo.Text;
            string endereco = textEndereco.Text;
            string telefone = maskedTelefone.Text;

            controladorEstabelecimentos.Editar(id, new Estabelecimentos(nome, tipo, endereco, telefone));

            }
            catch (Exception)
            {

                MessageBox.Show("Insira os todos os campos", "Edição de Estabelecimento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            CarregarEstabelecimentos();
            LimparCampos();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textId.Text);
                controladorEstabelecimentos.Excluir(id);
            }
            catch (Exception)
            {

                MessageBox.Show("Insira o Id para excluir", "Exclusão de Estabelecimento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           

            CarregarEstabelecimentos();
            LimparCampos();
        }



        private void AdicionarEstabelecimento()
        {
            string nome = textNome.Text;
            string tipo = cbTipo.Text;
            string endereco = textEndereco.Text;
            string telefone = maskedTelefone.Text;

            controladorEstabelecimentos.InserirNovo(new Estabelecimentos(nome, tipo, endereco, telefone));

            CarregarEstabelecimentos();
            LimparCampos();
        }

        private void CarregarEstabelecimentos()
        {

            dtEstabelicimento.Clear();
            List<Estabelecimentos> listaEstabelecimentos = controladorEstabelecimentos.SelecionarTodos();


            foreach (var estabelecimentos in listaEstabelecimentos)
            {
                DataRow registro = dtEstabelicimento.NewRow();

                registro["Id"] = estabelecimentos.Id;
                registro["Nome"] = estabelecimentos.nome;
                registro["Tipo"] = estabelecimentos.tipo;
                registro["Endereço"] = estabelecimentos.endereco;
                registro["Telefone"] = estabelecimentos.telefone;
                dtEstabelicimento.Rows.Add(registro);
            }

        }

        private void textId_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Utlize o campo Id só para editar e excluir");
        }

        private void LimparCampos()
        {
            textNome.Clear();
            //cbTipo.Clear();
            textEndereco.Clear();
            maskedTelefone.Clear();
        }



        private void btnSair_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            TelaPrincipal form1 = new TelaPrincipal();
            form1.ShowDialog();
            this.Close();
        }
    }
}

