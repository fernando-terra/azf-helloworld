using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace foxtangosilva
{
    public static class hw_httptrigger
    {
        [FunctionName("hw_httptrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var res = new RetornoApi();

            string name = req.Query["name"];
            string age = req.Query["age"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            name = name ?? data?.name;
            age = age ?? data?.age;

            if(name != null)
            {
                var obj = BLL.HelloWorld(name, age);

                res.Resultado = obj;
                res.Sucesso = true;
                return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(res));
            }
            else
            {
                res.Sucesso = false;
                res.Mensagem = "Informe um nome válido!";
                return new BadRequestObjectResult(JsonConvert.SerializeObject(res));
            }            
        }
    }
}