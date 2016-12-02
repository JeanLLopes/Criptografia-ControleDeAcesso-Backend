using System;
using System.Threading.Tasks;

//Como temos o namespace Microsoft.Owin
//O metodo configuration será executado automaticamente por padrão
using Microsoft.Owin;
using Owin;

using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace ControleAcessos
{
    //A classe startup define o ciclo de vida da requisição
    //Automaticamente quando o sistema for carregado
    //a classe statup sera chamada e o metodo configuration
    //será invocado!!!
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Para que o ciclo de vida da aplication funcione
            //com  owin e webapi teremos que instalar os seguintes
            //pacotes:

            //Install-package Microsoft.aspnet.webapi.owin => funcionar com o webapi
            //Install-package Microsoft.owin.host.systemweb => funcionar com o iis
            //Install-package Microsoft.owin.cors => habilitar acessos a chamadas remotas de
                                                    //outros domínios
            
            //para trabalharmos com autenticação precisamos instalar os seguintes Pacotes
            //Install-Package Microsoft.Owin.Security

            // É um protocolo de autorização utilizado para API's
            //Install-Package Microsoft.Owin.Security.Oauth

            //Este é o objeto de configuração
            var configuration = new HttpConfiguration();

            //1 - configurar rota
            ConfigurarRota(configuration);

            //2 - configurar o retorno (de pascal para camelCase)
            ConfigurarFormatoRetorno(configuration);


            //5 - salvar as configurações
            app.UseWebApi(configuration);
        }

        public void ConfigurarFormatoRetorno(HttpConfiguration configuration)
        {
            //1-temos que remover o formato xml para sempre vir json
            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);

            //2-O processo de serialização deve ser do tipo camelcase para
            //estar de acordo com o formato do javascript
            //serialização => transformar algo em outro
            //exemplo: transformar uma classe c# em um json
            var settings = configuration.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public void ConfigurarRota(HttpConfiguration configuration)
        {

            //Habilitar o mapeamento das rotas
            configuration.MapHttpAttributeRoutes();

            //Definir uma rota padrão
            configuration.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional});
        }
    }
}
