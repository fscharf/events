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
    /****************************************************
    //                          IMPORTANTE
    //      =============================================
    //      Tabelas ALUNO, VISITANTE não possuem FK
    //      nas tabelas ACESSO e USUARIO, causando uma divergência
    //      na lógica do C#.
    //      Ou seja, código-fonte possui referência ao novo database
    //      EVENTOFIEB porém NÃO está funcionando!
    //      Necessário revisão do database.
    //      =============================================
    *****************************************************/

    public class Register
    {
        // Busca a string de conexão com banco de dados
        private string SqlConnect()
        {
            return ConfigurationManager.AppSettings["sqlConnect"];
        }

        // Cria um método para tabela existente
        public DataTable List()
        {
            using (SqlConnection connection = new SqlConnection(SqlConnect()))
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

        // Efetua o cadastro nas colunas da tabela existente
        public void Save(int id, string nome, string cpf, string email, string senha)
        {
            using (SqlConnection connection = new SqlConnection(SqlConnect()))
            {
                string queryString = "insert into cadastro (nome, cpf, email, senha) values ('" + nome + "', '" + cpf + "', '" + email + "', '" + senha + "')";

                if (id != 0)
                {
                    queryString = "update cadastro set nome = '" + nome + "', '" + cpf + "', email = '" + email + "', senha = '" + senha + "' where id = " + id;
                }

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
