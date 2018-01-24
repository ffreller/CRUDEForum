using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CRUDForum.Models;
using System;

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
        public IActionResult GetUsuario(int id){
            var rs = new JsonResult(dao.Listar().Where(u => u.id == id).FirstOrDefault());
            rs.ContentType = "application/json";

            if(rs.Value == null){ //Json value verification
                rs.StatusCode = 204;
                rs.Value = "Sem resultados";
            }
            else
                rs.StatusCode = 200;
            return Json(rs);
        }
        [HttpPost]
        [Route("api/CadastrarUsuario")]
        public IActionResult CadastrarUsuario([FromBody] Usuarios usuario){
            JsonResult rs;
            try{
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                rs = new JsonResult(dao.Cadastrar(usuario));
                rs.ContentType = "application/json";
                if(!Convert.ToBoolean(rs.Value))
                {
                    rs.StatusCode = 404;
                    rs.Value = "Ocorreu um erro";
                }
                else
                    rs.StatusCode = 200;
            }
            catch(System.Exception ex)
            {
                rs = new JsonResult("");
                rs.StatusCode = 204;
                rs.ContentType = "application/json";
                rs.Value = ex.Message;
            }
            return Json(rs);
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