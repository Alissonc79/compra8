using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Projeto_Compras
{
    class ClassConecta
    {
        public static string stringconexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projeto_Compras\DbCompras.mdf;Integrated Security=True";
        private static string str = stringconexao;
        private static SqlConnection con = null;

        public static SqlConnection ObterConexao()
        {
            con = new SqlConnection(str);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
            }
            catch (SqlException sqle)
            {
                con = null;
            }
            return con;
        }

        public static void FecharConexao()
        {
            if (con != null || con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}