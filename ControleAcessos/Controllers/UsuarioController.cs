
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//Models
using CadastroUsuario.Models;

namespace ControleAcessos.Controllers
{
    //Toda api do tipo webapi funciona com o protocolo Http
    //E as chamadas são via verbos.
    //Estes verbos são: Get, Post, Put, Delete, etc.
    [Authorize]
    public class UsuarioController : ApiController
    {
        public HttpResponseMessage Get()
        {
            //1-Estamos listando os usuários
            var usuarios = UsuarioModel.GetAll();
            //2-Estamos enviando os usuarios e informando ao cliente 
            //que o retorno está OK(sucesso!!)
            return Request.CreateResponse(HttpStatusCode.OK, usuarios);
        }

        public HttpResponseMessage Post(UsuarioModel usuario)
        {
            //1-Estamos adicionando um usuario vindo do front
            //2-Quando adicionamos, uma lista é carregada no retorno
            HttpResponseMessage response;

            try
            {
                var usuarios = UsuarioModel.Add(usuario);
                response = Request.CreateResponse(HttpStatusCode.OK, usuarios);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return response;
        }
    }
}
