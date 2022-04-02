using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Projeto_Compras
{
    class ClassProduto
    {

        public int Id { get; set; }
        public string nome { get; set; }
        public int quantidade { get; set; }

        public List<ClassProduto> listaproduto()
        {
            List<ClassProduto> li = new List<ClassProduto>();
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM produto";
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ClassProduto p = new ClassProduto();
                p.Id = (int)dr["Id"];
                p.nome = dr["nome"].ToString();
                p.quantidade = (int)dr["quantidade"];
                li.Add(p);
            }
            return li;
        }

        public void Inserir(string nome, int quantidade)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO produto(nome,quantidade,valor) VALUES ('" + nome + "'," + quantidade + "')";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            ClassConecta.FecharConexao();
        }

        public void Localiza(int id)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM produto WHERE Id='" + id + "'";
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                nome = dr["nome"].ToString();
                quantidade = (int)dr["quantidade"];
            }
        }

        public void Atualizar(int id, string nome, int quantidade)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE produto SET nome='" + nome + "',quantidade=" + quantidade + "' WHERE Id = '" + id + "'";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            ClassConecta.FecharConexao();
        }

        public void Exclui(int id)
        {
            SqlConnection con = ClassConecta.ObterConexao();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "DELETE FROM produto WHERE Id='" + id + "'";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            ClassConecta.FecharConexao();
        }
    }
}
