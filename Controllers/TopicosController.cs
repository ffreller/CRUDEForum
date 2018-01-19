using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CRUDForum.Models;

namespace CRUDForum.Controllers
{
    //Definindo a rota para a requisição do serviço

    public class TopicosController : Controller
    {
        Topicos topico = new Topicos();
        DAOTopicos dao = new DAOTopicos();
        
        [HttpGet]
        [Route("api/VerTopico")]
        public IEnumerable<Topicos> GetTopico(){
            return dao.Listar();
        }
        [HttpGet("{id}", Name = "TopicoAtual")]
        [Route("api/VerTopico/{id}")]
        public Topicos GetTopico(int id){
            return dao.Listar().Where(x => x.id == id).FirstOrDefault();
        }
        [HttpPost]
        [Route("api/CadastrarTopico")]
        public IActionResult CadastrarTopico([FromBody] Topicos topico){
            dao.Cadastrar(topico);
            return CreatedAtRoute("TopicoAtual", new{id = topico.id}, topico); //redireciona a rota para o Get para mostrar o que foi cadastrado
        }
        [HttpPut("{id}")]
        [Route("api/AtualizarTopico/{id}")]
        public IActionResult AtualizarTopico([FromBody] Topicos topico, int id){
            topico.id = id;
            dao.Atualizar(topico);
            return CreatedAtRoute("TopicoAtual", new{id = topico.id}, topico);
        }
        [HttpDelete("{id}")]
        [Route("api/ExcluirTopico/{id}")]
        public IActionResult ExcluirTopico(int id){
            dao.Excluir(id);
            return Ok(id);
        }
    }
}