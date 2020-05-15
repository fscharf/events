using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Business
{
    public class Cadastro
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string senha { get; set; }

        public List<Cadastro> Lista()
        {

            // Cria variáveis para converter os dados para a tabela existente
            var lista = new List<Cadastro>();
            var cadastroDatabase = new Database.Cadastro();
            foreach (DataRow row in cadastroDatabase.Lista().Rows)
            {
                var cadastro = new Cadastro();

                cadastro.id = Convert.ToInt32(row["id_cadastro"]);
                cadastro.nome = row["nome"].ToString();
                cadastro.cpf = row["cpf"].ToString();
                cadastro.email = row["email"].ToString();
                cadastro.senha = row["senha"].ToString();

                lista.Add(cadastro);
            }

            return lista;
        } 

        // Salva as respectivas colunas na tabela existente
        public void Salvar()
        {
            new Database.Cadastro().Salvar(this.id, this.nome, this.cpf, this.email, this.senha);
        }
    }
}
