using System;
using System.Collections.Generic;

namespace CRUDForum.Models
{
    public class Usuarios
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public DateTime datacadastro { get; set; }

    }
}