using System;
using UpdaterLib;

namespace SampleApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("=== SampleApp ===");
            Console.WriteLine($"Versão atual: {UpdaterClient.GetLocalVersion()}");

            // Verifica se existe atualização
            if (await UpdaterClient.HasUpdate())
            {
                Console.WriteLine("⚠️ Nova versão disponível!");
                Console.WriteLine("Chamando AppUpdater...");

                // Executa o AppUpdater
                UpdaterClient.LaunchUpdater();

                // Fecha imediatamente para liberar os arquivos para substituição
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("✅ Você já está na versão mais recente!");
            }

            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}