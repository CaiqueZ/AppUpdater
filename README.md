# 🔄 AppUpdater
<details>
**AppUpdater** é um sistema **universal de atualização automática** para aplicações em **.NET Framework (4.6.1+)**, compatível até com o **Windows 7**.
Este projeto é **Open Source (MIT)** e inclui:

- 📚 **UpdaterLib** → Biblioteca em C# para integração com qualquer aplicação.
- ⚙️ **AppUpdater.exe** → Executável responsável por aplicar as atualizações.
- 🧪 **SampleApp** → Exemplo funcional que demonstra como usar a biblioteca.

⚠️ **Atenção:**
Baixe os arquivos da **versão 1.0.0** na aba [Releases](https://github.com/CaiqueZ/AppUpdater/releases), ou `Baixe direto` abaixo:
- [release.zip](https://github.com/CaiqueZ/AppUpdater/releases/download/1.0.0/release.zip) → Contém o **SampleApp**
- [AppUpdater.exe](https://github.com/CaiqueZ/AppUpdater/releases/download/1.0.0/AppUpdater.exe) → Executável do atualizador
- **Para testar o funcionamento do programa, busque pela release 1.0.0**

---

## 📂 Estrutura do Projeto

```
AppUpdater/
 ├── UpdaterLib/    → Biblioteca de atualização (DLL)
 ├── AppUpdater/    → Atualizador (EXE)
 ├── SampleApp/     → Exemplo de aplicação usando a UpdaterLib
 └── README.md      → Este arquivo
```

---

## 🚀 Como funciona o fluxo de atualização

1. **O programa inicia** → Usa a `UpdaterLib` para verificar a versão mais recente online.
2. **Se houver update** → Chama o `AppUpdater.exe`.
3. O AppUpdater baixa a nova versão e:
   - Se o programa estiver **fechado** → já substitui os arquivos e inicia atualizado.
   - Se o programa estiver **aberto** → baixa e guarda em cache, aguardando o fechamento para aplicar.

---

## 📚 UpdaterLib (Biblioteca)

- Fornece métodos para verificar versão online/local.  
- Baixa arquivos necessários para atualização.  
- Pode ser facilmente integrada em qualquer aplicativo.

Exemplo de uso no seu programa:
```csharp
using UpdaterLib;

class Program {
    static void Main() {
        var updater = new Updater("https://meusite.com/version.txt", "MeuApp.exe");

        if (updater.HasUpdate()) {
            updater.RunUpdater(); // chama o AppUpdater.exe
        }
    }
}
```

---

## ⚙️ AppUpdater (Executável)

- Responsável por baixar, extrair e aplicar as atualizações.
- Sempre funciona em conjunto com a `UpdaterLib`.

---

## 🧪 SampleApp (Exemplo)

- Aplicativo simples de console que demonstra a integração com a `UpdaterLib`.
- Mostra na prática como verificar atualizações e chamar o AppUpdater.

---

## 🔧 Como compilar

1. Clone este repositório:
   ```sh
   git clone https://github.com/CaiqueZ/AppUpdater.git
   ```
2. Abra a solution no **Visual Studio 2019+**.
3. Compile a solution → será gerado:
   - `UpdaterLib.dll`
   - `AppUpdater.exe`
   - `SampleApp.exe`

---

## 📦 Como usar no seu projeto

1. Copie `UpdaterLib.dll` e `AppUpdater.exe` para a pasta do seu programa.
2. No Visual Studio, adicione referência à `UpdaterLib.dll`.
3. Use a API da biblioteca para integrar o sistema de update.

---

## 🛡️ Licença

Este projeto é licenciado sob a **MIT License** → você pode usar livremente em projetos pessoais ou comerciais.
Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 🤝 Contribuindo

- Pull requests são bem-vindos!
- Abra uma issue se tiver dúvidas, sugestões ou encontrar bugs.
- Dê uma ⭐ no repositório para apoiar o projeto!

---

## 📌 Créditos

Desenvolvido por **Caique Z** • [GitHub](https://github.com/CaiqueZ)
</details>
<details> <summary>🇺🇸 English</summary>

AppUpdater is a universal auto-update system for applications in .NET Framework (4.6.1+), compatible even with Windows 7.
This project is Open Source (MIT) and includes:

📚 UpdaterLib → C# library for integration with any application.

⚙️ AppUpdater.exe → Executable responsible for applying updates.

🧪 SampleApp → Functional example demonstrating how to use the library.

⚠️ Note:
Download the files of version 1.0.0 in the Releases
 tab, or Download directly below:

release.zip
 → Contains the SampleApp

AppUpdater.exe
 → Updater executable

To test the program, use release 1.0.0

📂 Project Structure
AppUpdater/
 ├── UpdaterLib/    → Update library (DLL)
 ├── AppUpdater/    → Updater (EXE)
 ├── SampleApp/     → Example application using UpdaterLib
 └── README.md      → This file

🚀 Update flow

Program starts → Uses UpdaterLib to check the latest version online.

If there is an update → Calls AppUpdater.exe.

The AppUpdater downloads the new version and:

If the program is closed → replaces files and starts updated.

If the program is open → downloads and caches, waiting for closure to apply.

📚 UpdaterLib (Library)

Provides methods to check online/local versions.

Downloads required files for update.

Can be easily integrated into any application.

Example usage in your program:

using UpdaterLib;

class Program {
    static void Main() {
        var updater = new Updater("https://mysite.com/version.txt", "MyApp.exe");

        if (updater.HasUpdate()) {
            updater.RunUpdater(); // calls AppUpdater.exe
        }
    }
}

⚙️ AppUpdater (Executable)

Responsible for downloading, extracting, and applying updates.

Always works together with UpdaterLib.

🧪 SampleApp (Example)

Simple console application demonstrating integration with UpdaterLib.

Shows in practice how to check for updates and call AppUpdater.

🔧 How to build

Clone this repository:

git clone https://github.com/CaiqueZ/AppUpdater.git


Open the solution in Visual Studio 2019+.

Build the solution → it will generate:

UpdaterLib.dll

AppUpdater.exe

SampleApp.exe

📦 How to use in your project

Copy UpdaterLib.dll and AppUpdater.exe to your program folder.

In Visual Studio, add a reference to UpdaterLib.dll.

Use the library API to integrate the update system.

🛡️ License

This project is licensed under the MIT License → you can freely use it in personal or commercial projects.
See the LICENSE
 file for more details.

🤝 Contributing

Pull requests are welcome!

Open an issue if you have questions, suggestions, or found bugs.

Give a ⭐ to support this project!

📌 Credits

Developed by Caique Z • GitHub

</details> ```
