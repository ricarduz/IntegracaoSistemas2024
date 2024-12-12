/*
 * Disciplina: Integração de Sistemas
 * Autor: Ricardo Isaías Serafim
 * Email: 2302605@estudante.uab.pt
 * Descrição: Este programa implementa um cliente gRPC para o sistema Euromil, realiza chamadas gRPC para registrar apostas, utilizando uma chave de aposta e um ID de cheque digital. O resultado da operação é exibido diretamente na consola.
 */

using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Euromil; // Namespace gerado pelo Protobuf, contendo as definições do serviço e mensagens

class Program
{
    static async Task Main(string[] args)
    {
        // Configuração do canal gRPC para estabelecer a comunicação com o servidor
        using var channel = GrpcChannel.ForAddress("https://euromil.intsis.utad.pt");
        
        // Criação de um cliente para o serviço Euromil gerado a partir do arquivo .proto
        var client = new Euromil.EuromilClient(channel);

        // Configuração do pedido com os parâmetros necessários (chave de aposta e cheque digital)
        var request = new RegisterRequest
        {
            Key = "12345", // Substituir pela chave real da aposta
            Checkid = "1234567890123456" // Substituir pelo ID real do cheque digital
        };

        try
        {
            // Envia o pedido ao servidor gRPC e aguarda a resposta
            var reply = await client.RegisterEuroMilAsync(request);

            // Exibe a resposta do servidor na consola
            Console.WriteLine($"Resposta: {reply.Message}");
        }
        catch (Exception ex)
        {
            // Trata erros de comunicação ou validação e exibe a mensagem de erro
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
