using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Business
{
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
            var cadastroDatabase = new Database.Register();
            foreach (DataRow row in cadastroDatabase.List().Rows)
            {
                var cadastro = new Register();

                cadastro.id = Convert.ToInt32(row["id_cadastro"]);
                cadastro.nome = row["nome"].ToString();
                cadastro.cpf = row["cpf"].ToString();
                cadastro.email = row["email"].ToString();
                cadastro.senha = row["senha"].ToString();

                list.Add(cadastro);
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
