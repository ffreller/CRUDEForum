using System;
using System.Collections.Generic;

namespace CRUDForum.Models
{
    public class Topicos
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public DateTime datacadastro { get; set; }

    }
}