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

            if (args.Length < 1)
            {
                Console.WriteLine("Uso incorreto!");
                return;
            }

            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            string downloadUrl = args[0];
            string destinationZip = "release.zip";
            string extractPath = AppDomain.CurrentDomain.BaseDirectory;
            string mainAppExe = Path.Combine(extractPath, "SampleApp.exe");

            try
            {
                Console.WriteLine($"Baixando nova versão de:\n{downloadUrl}");

                using (var client = new WebClient())
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        int totalBlocks = 30;
                        int filledBlocks = (int)((e.ProgressPercentage / 100.0) * totalBlocks);
                        string progressBar = new string('█', filledBlocks) + new string('░', totalBlocks - filledBlocks);

                        Console.Write($"\r[{progressBar}] {e.ProgressPercentage}%");
                    };

                    client.DownloadFileCompleted += (s, e) =>
                    {
                        Console.WriteLine("\nDownload concluído!");
                    };

                    client.DownloadFileAsync(new Uri(downloadUrl), destinationZip);

                    while (client.IsBusy)
                        Thread.Sleep(100);
                }

                Console.WriteLine($"Arquivo salvo em: {destinationZip}");

                Console.WriteLine("Extraindo atualização...");
                using (ZipArchive archive = ZipFile.OpenRead(destinationZip))
                {
                    foreach (var entry in archive.Entries)
                    {
                        try
                        {
                            string filePath = Path.Combine(extractPath, entry.FullName);

                            if (!filePath.StartsWith(extractPath, StringComparison.OrdinalIgnoreCase))
                                continue;

                            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                            // extrai primeiro para um .tmp
                            string tempFile = filePath + ".tmp";
                            entry.ExtractToFile(tempFile, true);

                            if (File.Exists(filePath))
                            {
                                // substitui com backup
                                string backupFile = filePath + ".bak";
                                File.Replace(tempFile, filePath, backupFile, true);
                                File.Delete(backupFile);
                            }
                            else
                            {
                                File.Move(tempFile, filePath);
                            }

                            Console.WriteLine($"Atualizado: {entry.FullName}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ERRO] {entry.FullName}: {ex.Message}");
                        }
                    }
                }

                Console.WriteLine("> Atualização aplicada com sucesso!");

                File.Delete(destinationZip);
                Console.WriteLine("Arquivo temporário removido.");

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