using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Pedido
    {
        public int Id { get; set; }

        public  Usuario Usuario { get; set; }

        public  DateTime DataDeCriacao { get; set; }

        public Pedido(int id, Usuario usuario, DateTime dataDeCriacao)
        {
            Id = id;
            Usuario = usuario;
            DataDeCriacao = dataDeCriacao;
        }

        public override string ToString()        
            => $"Id do Pedido:{Id}," +
                   $" Usuário:{Usuario.Nome}," +
                   $" Data de Criação:{DataDeCriacao:dd/MM/yyyy}";
        

    }
}
