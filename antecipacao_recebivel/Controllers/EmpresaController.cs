using antecipacao_recebivel.Models;
using antecipacao_recebivel.Rules;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace antecipacao_recebivel.Controllers
{
    public class EmpresaController : ControllerBase
    {
        //private readonly ActionsEmpresa _actionsEmpresa;

        //public EmpresaController(ActionsEmpresa actionsEmpresa)
        //{
        //    _actionsEmpresa = actionsEmpresa;
        //}

        
        [HttpPost]
        public IActionResult CadastrarEmpresa(IFormCollection form)
        {

            string nome = form["nome"];
            string cnpj = form["cnpj"];

            string caminhoArquivo = "./Dados/Empresa.json"; // Caminho do arquivo JSON
            //string fileName = "nome_do_arquivo.extensao";         
            //byte[] fileBytes = System.IO.File.ReadAllBytes("./Dados/Empresa.json");

            // Ler o conteúdo do arquivo            
            var json = System.IO.File.ReadAllText(caminhoArquivo);
            //foreach (Char line in json.empresas)
            //{
            //    var adadas = line;
            //}

            // Deserializar o JSON para um objeto          
            Empresa empres2 = JsonConvert.DeserializeObject<Empresa>(json);

            // Exibir os dados
            Console.WriteLine($"CNPJ: {empres2.cnpj}, Nome: {empres2.nome}");

            // Serializar novamente para salvar no arquivo
            string novoJson = JsonConvert.SerializeObject(empres2, Formatting.Indented);
            System.IO.File.WriteAllText(caminhoArquivo, novoJson);

            Console.WriteLine("Arquivo JSON atualizado.");

            return Ok("feito");
        }
    }
}
