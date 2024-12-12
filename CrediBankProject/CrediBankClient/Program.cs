/*
 * Disciplina: Integração de Sistemas
 * Autor: Ricardo Isaías Serafim
 * Email: 2302605@estudante.uab.pt
 * Descrição: este programa implementa um cliente REST para interagir com o sistema CrediBank. Realiza requisições HTTP para obter um cheque digital com base na identificação da conta de crédito e no valor solicitado. O resultado é exibido diretamente na consola.
 */
 
using System; // Biblioteca para funcionalidades básicas como entrada e saída de dados
using System.Net.Http; // Biblioteca para realizar pedidos HTTP
using System.Threading.Tasks; // Biblioteca para suporte a operações assíncronas
using Newtonsoft.Json.Linq; // Biblioteca para utilização JSON

class Program
{
    static async Task Main(string[] args)
    {
        // URL base do sistema CrediBank
        string baseUrl = "https://credibank.intsis.utad.pt:8080";

        // Identificação da conta de crédito (16 dígitos) e o valor a debitar
        string creditAccountId = "1234567890123456"; 
        string value = "2302605"; // Valor do cheque digital solicitado

        // Construção do URL completa para a chamada REST
        string url = $"{baseUrl}/check/{creditAccountId}/ammount/{value}/";

        // Criação de um cliente HTTP para realizar a requisição
        using (HttpClient client = new HttpClient())
        {
            // Realiza uma chamada GET à API REST do sistema CrediBank
            HttpResponseMessage response = await client.GetAsync(url);

            // Verifica se a resposta foi bem-sucedida (código HTTP 200)
            if (response.IsSuccessStatusCode)
            {
                // Lê a resposta como string JSON
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Analisa o JSON para extrair a data e o ID do cheque digital
                var data = JArray.Parse(jsonResponse)[0];
                Console.WriteLine($"Date: {data["date"]}, Check ID: {data["checkID"]}");
            }
            else
            {
                // Exibe uma mensagem de erro se a API não responder corretamente
                Console.WriteLine("Error accessing CrediBank API");
            }
        }
    }
}