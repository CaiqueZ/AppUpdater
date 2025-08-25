using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace UpdaterLib
{
    public static class UpdaterClient
    {
        // URL fixa do version.txt no repositório (RAW)
        private const string VersionUrl = "https://raw.githubusercontent.com/CaiqueZ/AppUpdater/main/version.txt";

        // URL estável para o asset 'release.zip' da ÚLTIMA release
        private const string DownloadUrl = "https://github.com/CaiqueZ/AppUpdater/releases/latest/download/release.zip";

        private const string UpdaterExe = "AppUpdater.exe";

        /// <summary>
        /// Obtém a versão local do executável em execução.
        /// </summary>
        public static string GetLocalVersion()
        {
            return Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "0.0.0.0";
        }

        /// <summary>
        /// Lê a versão online do version.txt (com no-cache para evitar cache intermediário).
        /// </summary>
        public static async Task<string> GetOnlineVersion()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true, NoStore = true };
                // querystring para “furar” caches agressivos
                var url = VersionUrl + "?t=" + DateTime.UtcNow.Ticks;
                var txt = await client.GetStringAsync(url);
                return txt;
            }
        }

        /// <summary>
        /// Compara versão local vs online.
        /// </summary>
        public static async Task<bool> HasUpdate()
        {
            try
            {
                string local = GetLocalVersion();
                string online = (await GetOnlineVersion()).Trim();
                return !local.Equals(online, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false; // sem internet, etc.
            }
        }

        /// <summary>
        /// Executa o AppUpdater.exe, passando a URL estável do ZIP.
        /// </summary>
        public static void LaunchUpdater()
        {
            string updaterPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UpdaterExe);
            if (!File.Exists(updaterPath))
            {
                Console.WriteLine("Erro: AppUpdater.exe não encontrado.");
                Console.WriteLine("Certifique-se de que o AppUpdater.exe está na mesma pasta do executável.");
                Environment.Exit(1);
            }

            Process.Start(updaterPath, $"\"{DownloadUrl}\"");
        }
    }
}