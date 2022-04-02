using Projeto_Compras;
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
    public partial class FormPrincipal : Form
    {
        SqlConnection con = new SqlConnection (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\compras\compras6-main\compras6-main\compra5-main\compra5-main\DbCompras.mdf;Integrated Security=True");
        public FormPrincipal()
        {
            InitializeComponent();
        }
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
           // DadosCadastrais();
            lblUsuario.Text = FormLogin.usuarioconectado;
        }

        private void DadosCadastrais()
        {
            //precisa fazer
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            FormUsuario usu = new FormUsuario();
            usu.Show();
        }

        private void btnFornecedor_Click(object sender, EventArgs e)
        {
            FormFornecedor forn = new FormFornecedor();
            forn.Show();
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            FormPedido ped = new FormPedido();
            ped.Show();
        }

        private void btnProduto_Click(object sender, EventArgs e)
        {
            FormProduto pro = new FormProduto();
            pro.Show();
        }
    }
}
