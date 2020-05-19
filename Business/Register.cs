using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Business
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
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string senha { get; set; }

        public List<Register> List()
        {

            // Cria variáveis para converter os dados para a tabela existente
            var list = new List<Register>();
            var registerDb = new Database.Register();
            foreach (DataRow row in registerDb.List().Rows)
            {
                var register = new Register();

                register.id = Convert.ToInt32(row["id"]);
                register.nome = row["nome"].ToString();
                register.cpf = row["cpf"].ToString();
                register.email = row["email"].ToString();
                register.senha = row["senha"].ToString();

                list.Add(register);
            }

            return list;
        }

        // Salva as respectivas colunas na tabela existente
        public void Save()
        {
            new Database.Register().Save(this.id, this.nome, this.cpf, this.email, this.senha);
        }
    }
}
