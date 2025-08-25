using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;

namespace AppUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== AppUpdater ===");

            // Verifica se foi passada a URL como argumento
            if (args.Length < 1)
            {
                Console.WriteLine("Uso incorreto!");
                return;
            }

            // Win7/legacy: força TLS 1.2 para downloads do GitHub
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            string downloadUrl = args[0];                        // URL recebida (arquivo .zip no GitHub)
            string destinationZip = "release.zip";               // Nome fixo do arquivo baixado
            string extractPath = AppDomain.CurrentDomain.BaseDirectory; // Pasta atual do programa
            string mainAppExe = Path.Combine(extractPath, "SampleApp.exe"); // Nome fixo do executável principal

            try
            {
                Console.WriteLine($"Baixando nova versão de:\n{downloadUrl}");

                using (var client = new WebClient())
                {
                    // Exibe progresso do download
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        int totalBlocks = 30; // tamanho fixo da barra
                        int filledBlocks = (int)((e.ProgressPercentage / 100.0) * totalBlocks);
                        string progressBar = new string('█', filledBlocks) + new string('░', totalBlocks - filledBlocks);

                        Console.Write($"\r[{progressBar}] {e.ProgressPercentage}%");
                    };


                    // Evento de finalização do download
                    client.DownloadFileCompleted += (s, e) =>
                    {
                        Console.WriteLine("\nDownload concluído!");
                    };

                    // Faz o download assíncrono do arquivo ZIP
                    client.DownloadFileAsync(new Uri(downloadUrl), destinationZip);

                    // Espera até terminar
                    while (client.IsBusy)
                        Thread.Sleep(100);
                }

                Console.WriteLine($"Arquivo salvo em: {destinationZip}");

                // Extrai o conteúdo do ZIP para a pasta da aplicação
                Console.WriteLine("Extraindo atualização...");
                using (ZipArchive archive = ZipFile.OpenRead(destinationZip))
                {
                    foreach (var entry in archive.Entries)
                    {
                        string filePath = Path.Combine(extractPath, entry.FullName);

                        // Protege contra paths inválidos (path traversal)
                        if (!filePath.StartsWith(extractPath, StringComparison.OrdinalIgnoreCase))
                            continue;

                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                        entry.ExtractToFile(filePath, true); // substitui arquivos existentes
                    }
                }

                Console.WriteLine("> Atualização aplicada com sucesso!");

                // Remove o arquivo temporário
                File.Delete(destinationZip);
                Console.WriteLine("Arquivo temporário removido.");

                // Reinicia o aplicativo principal
                if (File.Exists(mainAppExe))
                {
                    Console.WriteLine("Reiniciando aplicação...");
                    Process.Start(mainAppExe);
                }
                else
                {
                    Console.WriteLine("<!> Não foi possível encontrar SampleApp.exe para reiniciar.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao aplicar atualização:");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}