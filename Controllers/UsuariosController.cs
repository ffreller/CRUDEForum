using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CRUDForum.Models;

namespace CRUDForum.Controllers
{
    //Definindo a rota para a requisição do serviço

    public class UsuarioController : Controller
    {
        Usuarios usuario = new Usuarios();
        DAOUsuarios dao = new DAOUsuarios();
        
        [HttpGet]
        [Route("api/VerUsuario")]
        public IEnumerable<Usuarios> GetUsuario(){
            return dao.Listar();
        }
        [HttpGet("{id}", Name = "UsuarioAtual")]
        [Route("api/VerUsuario/{id}")]
        public Usuarios GetUsuario(int id){
            return dao.Listar().Where(x => x.id == id).FirstOrDefault();
        }
        [HttpPost]
        [Route("api/CadastrarUsuario")]
        public IActionResult CadastrarUsuario([FromBody] Usuarios usuario){
            dao.Cadastrar(usuario);
            return CreatedAtRoute("UsuarioAtual", new{id = usuario.id}, usuario); //redireciona a rota para o Get para mostrar o que foi cadastrado
        }
        [HttpPut("{id}")]
        [Route("api/AtualizarUsuario/{id}")]
        public IActionResult AtualizarUsuario([FromBody] Usuarios usuario, int id){
            usuario.id = id;
            dao.Atualizar(usuario);
            return CreatedAtRoute("UsuarioAtual", new{id = usuario.id}, usuario);
        }
        [HttpDelete("{id}")]
        [Route("api/ExcluirUsuario/{id}")]
        public IActionResult ExcluirUsuario(int id){
            dao.Excluir(id);
            return Ok(id);
        }
    }
}