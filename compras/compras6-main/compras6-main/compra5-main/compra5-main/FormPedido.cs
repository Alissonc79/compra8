using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Compras
{
    public partial class FormPedido : Form
    {
        SqlConnection con = new SqlConnection(ClassConecta.stringconexao);

        public FormPedido()
        {
            InitializeComponent();
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            CarregaCbxProduto();
            txtTotalGeral.Text = "";
            dgvPedido.Columns.Clear();
            dgvPedido.Rows.Clear();
            con.Open();
            SqlCommand cmd = new SqlCommand("LocalizarPedido", con);
            cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtPedido.Text.Trim());
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int linhas = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                cbxFornecedor.Enabled = true;
                cbxFornecedor.Text = "";
                cbxFornecedor.Text = dt.Rows[0]["nomecliente"].ToString();
                txtTotalGeral.Text = dt.Rows[0]["valor_total"].ToString();
                cbxProduto.Enabled = true;
                txtIdProduto.Enabled = true;
                txtQuantidade.Enabled = true;
                txtPreco.Enabled = true;
                dgvPedido.Enabled = true;
                btnAdicionarProduto.Enabled = true;
                btnAtualizarProduto.Enabled = true;
                btnExcluirProduto.Enabled = true;
                txtTotalGeral.Enabled = true;
                btnPedido.Enabled = true;
                dgvPedido.Columns.Add("ID", "ID");
                dgvPedido.Columns.Add("Produto", "Produto");
                dgvPedido.Columns.Add("Quantidade", "Quantidade");
                dgvPedido.Columns.Add("Preço", "Preço");
                dgvPedido.Columns.Add("Total", "Total");
                for (int i = 0; i < linhas; i++)
                {
                    DataGridViewRow item = new DataGridViewRow();
                    item.CreateCells(dgvPedido);
                    item.Cells[0].Value = dt.Rows[i]["id_produto"].ToString();
                    item.Cells[1].Value = dt.Rows[i]["nomeproduto"].ToString();
                    item.Cells[2].Value = dt.Rows[i]["quantidade"].ToString();
                    item.Cells[3].Value = dt.Rows[i]["preco"].ToString();
                    item.Cells[4].Value = dt.Rows[i]["total"].ToString();
                    dgvPedido.Rows.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Nenhum pedido localizado com este ID!", "Não localizado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            con.Close();
        }

        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            var repetido = false;
            foreach (DataGridViewRow dr in dgvPedido.Rows)
            {
                if (txtIdProduto.Text == Convert.ToString(dr.Cells[0].Value))
                {
                    repetido = true;
                }
            }
            if (repetido == false)
            {
                DataGridViewRow item = new DataGridViewRow();
                item.CreateCells(dgvPedido);
                item.Cells[0].Value = txtIdProduto.Text;
                item.Cells[1].Value = cbxProduto.Text;
                item.Cells[2].Value = txtQuantidade.Text;
                item.Cells[3].Value = txtPreco.Text;
                item.Cells[4].Value = Convert.ToDecimal(txtPreco.Text) * Convert.ToDecimal(txtQuantidade.Text);
                dgvPedido.Rows.Add(item);
                cbxProduto.Text = "";
                txtIdProduto.Text = "";
                txtQuantidade.Text = "";
                txtPreco.Text = "";
                decimal soma = 0;
                foreach (DataGridViewRow dr in dgvPedido.Rows)
                    soma += Convert.ToDecimal(dr.Cells[4].Value);
                txtTotalGeral.Text = Convert.ToString(soma);
            }
            else
            {
                MessageBox.Show("Item já consta na lista de pedido!", "Item Repetido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtualizarProduto_Click(object sender, EventArgs e)
        {
            int linha = dgvPedido.CurrentRow.Index;
            dgvPedido.Rows[linha].Cells[0].Value = txtIdProduto.Text;
            dgvPedido.Rows[linha].Cells[1].Value = cbxProduto.Text;
            dgvPedido.Rows[linha].Cells[2].Value = txtQuantidade.Text;
            dgvPedido.Rows[linha].Cells[3].Value = txtPreco.Text;
            dgvPedido.Rows[linha].Cells[4].Value = Convert.ToDecimal(txtPreco.Text) * Convert.ToDecimal(txtQuantidade.Text);

            cbxProduto.Text = "";
            txtIdProduto.Text = "";
            txtQuantidade.Text = "";
            txtPreco.Text = "";
            decimal soma = 0;
            foreach (DataGridViewRow dr in dgvPedido.Rows)
                soma += Convert.ToDecimal(dr.Cells[4].Value);
            txtTotalGeral.Text = Convert.ToString(soma);
        }

        private void btnExcluirProduto_Click(object sender, EventArgs e)
        {
            int linha = dgvPedido.CurrentRow.Index;
            dgvPedido.Rows.RemoveAt(linha);
            dgvPedido.Refresh();

            cbxProduto.Text = "";
            txtIdProduto.Text = "";
            txtQuantidade.Text = "";
            txtPreco.Text = "";
            decimal soma = 0;
            foreach (DataGridViewRow dr in dgvPedido.Rows)
                soma += Convert.ToDecimal(dr.Cells[4].Value);
            txtTotalGeral.Text = Convert.ToString(soma);
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            cbxProduto.Enabled = true;
            CarregaCbxProduto();
            txtIdProduto.Enabled = true;
            txtQuantidade.Enabled = true;
            txtPreco.Enabled = true;
            dgvPedido.Enabled = true;
            btnAdicionarProduto.Enabled = true;
            btnAtualizarProduto.Enabled = true;
            btnExcluirProduto.Enabled = true;
            txtTotalGeral.Enabled = true;
            btnPedido.Enabled = true;
            dgvPedido.Columns.Add("Id", "Id");
            dgvPedido.Columns.Add("Produto", "Produto");
            dgvPedido.Columns.Add("Quantidade", "Quantidade");
            dgvPedido.Columns.Add("Valor", "Valor");
        }

        private void CarregaCbxProduto()
        {
            throw new NotImplementedException();
        }

        private void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand("InserirPedido", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cnpj_fornecedor", SqlDbType.NChar).Value = cbxFornecedor.SelectedValue;
            cmd.Parameters.AddWithValue("@valor_total", SqlDbType.Decimal).Value = Convert.ToDecimal(txtTotalGeral.Text);
            cmd.Parameters.AddWithValue("@situacao", SqlDbType.NChar).Value = "Aberta";
            cmd.ExecuteNonQuery();
            string idpedido = "SELECT IDENT_CURRENT('pedido') AS id_pedido";
            SqlCommand cmdpedido = new SqlCommand(idpedido, con);
            Int32 idpedido2 = Convert.ToInt32(cmdpedido.ExecuteScalar());
            foreach (DataGridViewRow dr in dgvPedido.Rows)
            {
                SqlCommand cmditens = new SqlCommand("ItensPedidos", con);
                cmditens.CommandType = CommandType.StoredProcedure;
                cmditens.Parameters.AddWithValue("@id_pedido", SqlDbType.Int).Value = idpedido2;
                cmditens.Parameters.AddWithValue("@id_produto", SqlDbType.Int).Value = Convert.ToInt32(dr.Cells[0].Value);
                cmditens.Parameters.AddWithValue("@quantidade", SqlDbType.Int).Value = Convert.ToInt32(dr.Cells[2].Value);
                cmditens.Parameters.AddWithValue("@preco", SqlDbType.Decimal).Value = Convert.ToDecimal(dr.Cells[3].Value);
                cmditens.Parameters.AddWithValue("@total", SqlDbType.Decimal).Value = Convert.ToDecimal(dr.Cells[4].Value);
                cmditens.ExecuteNonQuery();
            }
            con.Close();
            dgvPedido.Rows.Clear();
            dgvPedido.Refresh();
            txtTotalGeral.Text = "";
            MessageBox.Show("Pedido realizado com sucesso!", "Pedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAtualizarPedido_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE pedido SET valor_total = @valor_total WHERE Id = @Id", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtPedido.Text.Trim());
            cmd.Parameters.AddWithValue("@valor_total", SqlDbType.Decimal).Value = Convert.ToDecimal(txtTotalGeral.Text.Trim());
            cmd.ExecuteNonQuery();

            SqlCommand deletar_itens = new SqlCommand("DELETE FROM itens_pedidos WHERE id_pedido = @id_pedido", con);
            deletar_itens.CommandType = CommandType.Text;
            deletar_itens.Parameters.AddWithValue("@id_pedido", SqlDbType.Int).Value = Convert.ToInt32(txtPedido.Text.Trim());
            deletar_itens.ExecuteNonQuery();

            foreach (DataGridViewRow dr in dgvPedido.Rows)
            {
                SqlCommand cmditens = new SqlCommand("ItensPedidos", con);
                cmditens.CommandType = CommandType.StoredProcedure;
                cmditens.CommandType = CommandType.StoredProcedure;
                cmditens.Parameters.AddWithValue("@id_pedido", SqlDbType.Int).Value = Convert.ToInt32(txtPedido.Text);
                cmditens.Parameters.AddWithValue("@id_produto", SqlDbType.Int).Value = Convert.ToInt32(dr.Cells[0].Value);
                cmditens.Parameters.AddWithValue("@quantidade", SqlDbType.Int).Value = Convert.ToInt32(dr.Cells[2].Value);
                cmditens.Parameters.AddWithValue("@preco", SqlDbType.Decimal).Value = Convert.ToDecimal(dr.Cells[3].Value);
                cmditens.Parameters.AddWithValue("@total", SqlDbType.Decimal).Value = Convert.ToDecimal(dr.Cells[4].Value);
                cmditens.ExecuteNonQuery();
            }
            MessageBox.Show("Pedido atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            dgvPedido.Columns.Clear();
            dgvPedido.Rows.Clear();
            txtPedido.Text = "";
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand("InserirPedido", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_pedido", SqlDbType.Int).Value = Convert.ToInt32(txtPedido.Text);
            cmd.Parameters.AddWithValue("@cnpj_fornecedor", SqlDbType.NChar).Value = cbxFornecedor.SelectedValue;
            cmd.Parameters.AddWithValue("@valor_total", SqlDbType.Decimal).Value = Convert.ToDecimal(txtTotalGeral.Text);
            cmd.ExecuteNonQuery();
            foreach (DataGridViewRow dr in dgvPedido.Rows)
            {
                SqlCommand cmditens = new SqlCommand("ItensPedidos", con);
                cmditens.CommandType = CommandType.StoredProcedure;
                cmditens.Parameters.AddWithValue("@id_pedido", SqlDbType.Int).Value = Convert.ToInt32(txtPedido.Text);
                cmditens.Parameters.AddWithValue("@id_produto", SqlDbType.Int).Value = Convert.ToInt32(dr.Cells[0].Value);
                cmditens.Parameters.AddWithValue("@quantidade", SqlDbType.Int).Value = Convert.ToInt32(dr.Cells[2].Value);
                cmditens.Parameters.AddWithValue("@preco", SqlDbType.Decimal).Value = Convert.ToDecimal(dr.Cells[3].Value);
                cmditens.Parameters.AddWithValue("@total", SqlDbType.Decimal).Value = Convert.ToDecimal(dr.Cells[4].Value);
                cmditens.ExecuteNonQuery();
            }
            SqlCommand upped = new SqlCommand("UPDATE pedido SET situacao = 'Fechada' WHERE Id = @Id", con);
            upped.CommandType = CommandType.Text;
            upped.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = Convert.ToInt32(txtPedido.Text);
            upped.ExecuteNonQuery();
            con.Close();
            dgvPedido.Rows.Clear();
            dgvPedido.Refresh();
            txtTotalGeral.Text = "";
            txtPedido.Text = "";
            txtPreco.Text = "";
            txtQuantidade.Text = "";
            txtIdProduto.Text = "";
            cbxFornecedor.Text = "";
            cbxProduto.Text = "";
            MessageBox.Show("Pedido realizado com sucesso!", "Pedido", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void CarregaCbxSolicitante()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string pro = "SELECT * FROM usuario ORDER BY nome";
            SqlCommand cmd = new SqlCommand(pro, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(pro, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "usuario");
            cbxProduto.ValueMember = "id";
            cbxProduto.DisplayMember = "nome";
            cbxProduto.DataSource = ds.Tables["usuario"];
            con.Close();
        }

        private void FormPedido_Load(object sender, EventArgs e)
        {
            CarregaCbxSolicitante();
        }
    }
}

