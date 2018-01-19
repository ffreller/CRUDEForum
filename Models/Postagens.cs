using System;
using System.Collections.Generic;

namespace CRUDForum.Models
{
    public class Postagens
    {
        public int id { get; set; }
        public int idtopico { get; set; }
        public int idusuario{ get; set; }
        public string mensagem { get; set; }
        public DateTime datapublicacao { get; set; }

    }
}