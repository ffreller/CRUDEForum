using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CRUDForum.Models;

namespace CRUDForum.Controllers
{
    //Definindo a rota para a requisição do serviço

    public class PostagensController : Controller
    {
        Postagens postagem = new Postagens();
        DAOPostagens dao = new DAOPostagens();
        
        [HttpGet]
        [Route("api/VerPostagem")]
        public IEnumerable<Postagens> GetPostagem(){
            return dao.Listar();
        }
        [HttpGet("{id}", Name = "PostagemAtual")]
        [Route("api/VerPostagem/{id}")]
        public Postagens GetPostagem(int id){
            return dao.Listar().Where(x => x.id == id).FirstOrDefault();
        }
        [HttpPost]
        [Route("api/CadastrarPostagem")]
        public IActionResult CadastrarPostagem([FromBody] Postagens postagem){
            dao.Cadastrar(postagem);
            return CreatedAtRoute("PostagemAtual", new{id = postagem.id}, postagem); //redireciona a rota para o Get para mostrar o que foi cadastrado
        }
        [HttpPut("{id}")]
        [Route("api/AtualizarPostagem/{id}")]
        public IActionResult AtualizarPostagem([FromBody] Postagens postagem, int id){
            postagem.id = id;
            dao.Atualizar(postagem);
            return CreatedAtRoute("PostagemAtual", new{id = postagem.id}, postagem);
        }
        [HttpDelete("{id}")]
        [Route("api/ExcluirPostagem/{id}")]
        public IActionResult ExcluirPostagem(int id){
            dao.Excluir(id);
            return Ok(id);
        }
    }
}