using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Projeto_Compras
{
    public partial class FormFornecedor : Form
    {
        public FormFornecedor()
        {
            InitializeComponent();
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                string cnpj = txtCnpj.Text.Trim();
                ClassFornecedor fornecedor = new ClassFornecedor();
                fornecedor.Localiza(cnpj);
                txtRazaoSocial.Text = fornecedor.razaosocial;
                txtCep.Text = fornecedor.cep;
                txtEndereco.Text = fornecedor.endereco;
                txtCidade.Text = fornecedor.cidade;
                txtBairro.Text = fornecedor.bairro;
                txtCelular.Text = fornecedor.celular;
                txtEmail.Text = fornecedor.email;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnCep_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://viacep.com.br/ws/" + txtCep.Text + "/json");
            request.AllowAutoRedirect = false;
            HttpWebResponse ChecaServidor = (HttpWebResponse)request.GetResponse();
            if (ChecaServidor.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Servidor Indisponível!");
                return; //Sai da rotina e para e codificação
            }
            using (Stream webStream = ChecaServidor.GetResponseStream())
            {
                if (webStream != null)
                {
                    using (StreamReader responseReader = new StreamReader(webStream))
                    {
                        string response = responseReader.ReadToEnd();
                        response = Regex.Replace(response, "[{},]", string.Empty);
                        response = response.Replace("\"", "");

                        String[] substrings = response.Split('\n');

                        int cont = 0;
                        foreach (var substring in substrings)
                        {
                            if (cont == 1)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                if (valor[0] == "  erro")
                                {
                                    MessageBox.Show("CEP não encontrado!");
                                    txtCep.Focus();
                                    return;
                                }
                            }

                            //Endereço
                            if (cont == 2)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                txtEndereco.Text = valor[1];
                            }

                            //Bairro
                            if (cont == 4)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                txtBairro.Text = valor[1];
                            }

                            //Cidade
                            if (cont == 5)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                txtCidade.Text = valor[1];
                            }
                            cont++;
                        }
                    }
                }
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                ClassFornecedor fornecedor = new ClassFornecedor();
                fornecedor.Inserir(txtCnpj.Text, txtRazaoSocial.Text, txtCep.Text, txtEndereco.Text, txtBairro.Text, txtCidade.Text, txtCelular.Text, txtEmail.Text);
                MessageBox.Show("Fornecedor cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<ClassFornecedor> forn = fornecedor.listafornecedor();
                dgvFornecedor.DataSource = forn;
                txtCnpj.Text = "";
                txtRazaoSocial.Text = "";
                txtBairro.Text = "";
                txtEndereco.Text = "";
                txtCep.Text = "";
                txtCidade.Text = "";
                txtEmail.Text = "";
                txtCelular.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                String cnpj = txtCnpj.Text.Trim();
                ClassFornecedor fornecedor = new ClassFornecedor();
                fornecedor.Exclui(cnpj);
                MessageBox.Show("Fornecedor cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<ClassFornecedor> forn = fornecedor.listafornecedor();
                dgvFornecedor.DataSource = forn;
                txtCnpj.Text = "";
                txtRazaoSocial.Text = "";
                txtBairro.Text = "";
                txtEndereco.Text = "";
                txtCep.Text = "";
                txtCidade.Text = "";
                txtEmail.Text = "";
                txtCelular.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        private void btnLimpaCampos_Click(object sender, EventArgs e)
        {
            txtCnpj.Text = "";
            txtRazaoSocial.Text = "";
            txtBairro.Text = "";
            txtEndereco.Text = "";
            txtCep.Text = "";
            txtCidade.Text = "";
            txtEmail.Text = "";
            txtCelular.Text = "";
        }

        private void FormCompras_Load(object sender, EventArgs e)
        {

        }
    }
}
