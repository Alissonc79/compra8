using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Projeto_Compras
{
    class ClassFornecedor
    {
        public string cnpj { get; set; }
        public string razaosocial { get; set; }
        public string cep { get; set; }
        public string endereco { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string celular { get; set; }
        public string email { get; set; }

        public List<ClassFornecedor> listafornecedor()
        {
            List<ClassFornecedor> li = new List<ClassFornecedor>();
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Fornecedor";
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ClassFornecedor c = new ClassFornecedor();
                c.cnpj = dr["cnpj"].ToString();
                c.razaosocial = dr["razaosocial"].ToString();
                c.cep = dr["cep"].ToString();
                c.endereco = dr["endereco"].ToString();
                c.bairro = dr["bairro"].ToString();
                c.cidade = dr["cidade"].ToString();
                c.celular = dr["celular"].ToString();
                c.email = dr["email"].ToString();
                li.Add(c);
            }
            return li;
        }

        public void Inserir(string cnpj, string razaosocial, string cep, string endereco, string bairro, string cidade, string celular, string email)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO Fornecedor(cnpj,razaosocial,cep,endereco,bairro,cidade,celular,email) VALUES ('" + cnpj + "','" + razaosocial + "','" + cep + "','" + endereco + "','" + bairro + "','" + cidade + "','" + celular + "','" + email + "')";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            ClassConecta.FecharConexao();
        }

        public void Localiza(string cnpj)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM Fornecedor WHERE cnpj='" + cnpj + "'";
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cnpj = dr["cnpj"].ToString();
                razaosocial = dr["razaosocial"].ToString();
                cep = dr["cep"].ToString();
                endereco = dr["endereco"].ToString();
                bairro = dr["bairro"].ToString();
                cidade = dr["cidade"].ToString();
                celular = dr["celular"].ToString();
                email = dr["email"].ToString();
            }
        }

        public void Atualizar(string cnpj, string razaosocial, string cep, string endereco, string bairro, string cidade, string celular, string email)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE Fornecedor SET cnpj='" + cnpj + "',razaosocial='" + razaosocial + "',cep='" + cep + "',endereco='" + endereco + "',bairro='" + bairro + "',cidade='" + cidade + "',celular='" + celular + "',email='" + email + "') WHERE cnpj = '" + cnpj + "'";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            ClassConecta.FecharConexao();
        }

        public void Exclui(string cnpj)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "DELETE FROM Fornecedor WHERE cnpj='" + cnpj + "'";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            ClassConecta.FecharConexao();
        }
    }
}

