using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

//PARA USAR AS CLAIMS USAMOS ESTE FRAMEWORK
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace ControleAcessos.Providers
{
    public class LGroupOAuthAuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        //PARA CLIENTES PUBLICOS NÃO É NECESSARIO VALIDAR O CLIENTE ID SÓ AUTORIZAMOS O RESOURCE OWNER ( CLIENTE FINAL )
        //EXPLICANDO MELHOR
        //RESOURCE OWNER É USUARIO FINAL DA SU APLICAÇÃO
        //CLIENTE É A SUA APLICAÇÃO
        //AUTHORIZATION SERVER: É RESPPONSAVEL POR GERAR O TOKEN
        //RESOURCE SERVER: SISTEMA RESPONSAVEL PELOS SEUS DADOS

        //NÃO VAMOS VALIDAR O CLIENTE ID   
        //POIS O CLIENTE ID ´UMA PAGINA EM ANGULAR QUE ESTA RODANDO EM LOCALHOST 
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() =>
            {
                //AQUI JA RETORNAMOS VALIDADO
                context.Validated();
            });
        }



        //SOMENTE PRECISAMO VALIDAR O USUARIO FINAL
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() =>
            {

                //AQUI NOS VAMOS VALIDAR O LOGIN E A SENHA
                var user = context.UserName;
                var password = context.Password;

                //AQUI PODERIAMOS IR NO BANCO PARA VALIDAR
                if (user.Equals("JLOPES") && password.Equals("12345"))
                {
                    context.SetError("Invalid_grant", "Credentials Invalid");
                }
                else
                {
                    //AQUI PODEMOS PEGAR AS CLAINS DO BANACO PARA SETAR O QUE O USUARIO É
                    var nome = new Claim(ClaimTypes.Name, "Jean Lopes");
                    var role = new Claim(ClaimTypes.Role, "Administrador");

                    var claims = new[]
                    {
                    nome,role
                };

                    //PAARA CRIA UMA IDENTIFICAÇÃO PRECISAMOS DE NO MONIMO UM A CLAIMS COM O NOME E UM TIOP DE AUTENTICAÇÃO
                    var identificacao = new ClaimsIdentity(claims, context.Options.AuthenticationType);


                    //CRIAMOS O TICKET DE AUTENTICAÇÃO
                    var token = new AuthenticationTicket(identificacao, null);

                    //ENVIA O TOKEN PARA O CONTEXTO DA SUA APLICAÇÃO
                    context.Validated(token);
                }
            });
            
        }
    }
}