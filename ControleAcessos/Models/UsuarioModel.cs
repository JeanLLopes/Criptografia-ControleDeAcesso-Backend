using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastroUsuario.Models
{
    //Classe Fake para o gerenciamento de usuários
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        //Incluindo novas propriedades..
        public decimal Salario { get; set; }
        public DateTime DataNascimento { get; set; }

        //Usuarios já cadastrados
        public static List<UsuarioModel> Usuarios { get; set; }
        //Adicionando os usuários na lista precadastrada
        public static IEnumerable<UsuarioModel> Add(UsuarioModel usuario)
        {
            if (Usuarios == null)
                Usuarios = new List<UsuarioModel>
                {
                    new UsuarioModel{Id = 1, Nome = "Fabio", 
                        Email = "fabison@ig.com", Telefone = "11111-1111",
                        Salario = 1000M, 
                        DataNascimento = DateTime.Now.AddYears(-30)
                        },
                    new UsuarioModel{Id = 2, Nome = "Maria", 
                        Email = "maria@ig.com", Telefone = "22222-2222",
                        Salario = 2000M, 
                        DataNascimento = DateTime.Now.AddYears(-20)}
                };

            Usuarios.Add(usuario);
            return Usuarios;
        }
        //Removendo os usuários na lista precadastrada
        public static IEnumerable<UsuarioModel> Remove(UsuarioModel usuario)
        {
            Usuarios.ToList().Remove(usuario);
            return Usuarios;
        }
        //Retornando os usuários
        public static IEnumerable<UsuarioModel> GetAll()
        {
            if (Usuarios == null)
                Usuarios = new List<UsuarioModel>
                {
                    new UsuarioModel{Id = 1, Nome = "Fabio", 
                        Email = "fabison@ig.com", Telefone = "11111-1111",
                        Salario = 1000M, 
                        DataNascimento = DateTime.Now.AddYears(-30)
                        },
                    new UsuarioModel{Id = 2, Nome = "Maria", 
                        Email = "maria@ig.com", Telefone = "22222-2222",
                        Salario = 2000M, 
                        DataNascimento = DateTime.Now.AddYears(-20)}
                };

            return Usuarios;
        }
    }
}
