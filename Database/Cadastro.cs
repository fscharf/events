using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Cadastro
    {
        private string sqlConnect()
        {
            return ConfigurationManager.AppSettings["sqlConnect"];
        }

        public DataTable Lista()
        {
            using (SqlConnection connection = new SqlConnection(sqlConnect()))
            {
                string queryString = "select * from cadastro";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public void Salvar(int id, string nome, string cpf, string email, string senha)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnect()))
            {
                string queryString = "insert into cadastro (nome, cpf, email, senha) values ('" + nome + "', '" + cpf + "', '" + email + "', '" + senha + "')";

                if (id != 0)
                {
                    queryString = "update cadastro set nome = '" + nome + "', '" + cpf + "', email = '" + email + "', senha = '" + senha + "' where id_cadastro = " + id;
                }

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
