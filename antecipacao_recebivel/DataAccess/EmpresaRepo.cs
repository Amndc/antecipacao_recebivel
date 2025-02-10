using antecipacao_recebivel.Data;
using antecipacao_recebivel.Models;
using Newtonsoft.Json;

namespace antecipacao_recebivel.DataAccess
{
    public class EmpresaRepo
    {
        private readonly DbContextRecebivel _DBrecebivel;


        public EmpresaRepo(DbContextRecebivel DBrecebivel)
        {
            _DBrecebivel = DBrecebivel;
        }

        public void Adicionar(Empresa empresa)
        {
            _DBrecebivel.Empresas.Add(empresa);
            _DBrecebivel.SaveChanges();
        }

        public Empresa ObterPorCNPJ(string cnpj)
        {
             string caminhoArquivo = "./Dados/Empresa.json"; // Caminho do arquivo JSON

            // Ler o conteúdo do arquivo
            var json = File.ReadAllText(caminhoArquivo);

            // Deserializar o JSON para um objeto
            Empresa empresa = JsonConvert.DeserializeObject<Empresa>(json);

            // Exibir os dados
            Console.WriteLine($"CNPJ: {empresa.cnpj}, Nome: {empresa.nome}");            

            // Serializar novamente para salvar no arquivo
            string novoJson = JsonConvert.SerializeObject(empresa, Formatting.Indented);
            File.WriteAllText(caminhoArquivo, novoJson);

            Console.WriteLine("Arquivo JSON atualizado.");
            return empresa;
            
        }

        //valida se empresa ja existe - Mudar pro banco?
        public bool existeEmpresa (string cnpj)
        {
            return _DBrecebivel.Empresas.Any(e => e.cnpj == cnpj);
        }
    }
}
