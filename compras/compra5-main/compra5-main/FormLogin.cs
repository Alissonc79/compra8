using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace Projeto_Compras
{
    public partial class FormLogin : Form
    {
        internal static string usuarioconectado;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = ClassConecta.ObterConexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM usuario WHERE login=@login AND senha=@senha";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@login", SqlDbType.NChar).Value = txtLogin.Text.Trim();
                cmd.Parameters.AddWithValue("@senha", SqlDbType.NChar).Value = txtSenha.Text.Trim();
                SqlDataReader usuario = cmd.ExecuteReader();
                if (usuario.HasRows)  
                {
                    usuarioconectado = txtLogin.Text;
                    this.Hide();
                    FormPrincipal pri = new FormPrincipal();
                    pri.Show();
                    ClassConecta.FecharConexao();
                }
                else
                {
                    MessageBox.Show("Login ou senha inválido! Por favor, tente novamente!", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLogin.Text = "";
                    txtSenha.Text = "";
                    txtLogin.Focus();
                    ClassConecta.FecharConexao();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtLogin.Focus();
        }
    }
}
