using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Projeto_Compras
{
    public partial class FormProduto : Form
    {
        public FormProduto()
        {
            InitializeComponent();
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Int32.Parse(txtId.Text);
                ClassProduto produto = new ClassProduto();
                produto.Localiza(id);
                txtNome.Text = produto.nome;
                txtQuantidade.Text = Convert.ToString(produto.quantidade);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                int qtde = Int32.Parse(txtQuantidade.Text);
                ClassProduto produto = new ClassProduto();
                produto.Inserir(txtNome.Text, qtde);
                MessageBox.Show("Produto cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<ClassProduto> pro = produto.listaproduto();
                dgvProduto.DataSource = pro;
                txtId.Text = "";
                txtNome.Text = "";
                txtQuantidade.Text = "";
                txtPreco.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Int32.Parse(txtId.Text);
                int qtde = Int32.Parse(txtQuantidade.Text);
                ClassProduto produto = new ClassProduto();
                produto.Atualizar(id, txtNome.Text, qtde);
                MessageBox.Show("Produto atualizado com sucesso!", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<ClassProduto> pro = produto.listaproduto();
                dgvProduto.DataSource = pro;
                txtId.Text = "";
                txtNome.Text = "";
                txtQuantidade.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Int32.Parse(txtId.Text);
                ClassProduto produto = new ClassProduto();
                produto.Exclui(id);
                MessageBox.Show("Produto excluído com sucesso!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<ClassProduto> pro = produto.listaproduto();
                dgvProduto.DataSource = pro;
                txtId.Text = "";
                txtNome.Text = "";
                txtQuantidade.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnLimpaCampos_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtQuantidade.Text = "";
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormProduto_Load(object sender, EventArgs e)
        {
            ClassProduto pro = new ClassProduto();
            List<ClassProduto> produtos = pro.listaproduto();
            dgvProduto.DataSource = produtos;
        }
    }
}
