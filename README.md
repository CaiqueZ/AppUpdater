# 🔄 AppUpdater

**AppUpdater** é um sistema **universal de atualização automática** para aplicações em **.NET Framework (4.6.1+)**, compatível até com o **Windows 7**.  
Este projeto é **Open Source (MIT)** e inclui:

- 📚 **UpdaterLib** → Biblioteca em C# para integração com qualquer aplicação.  
- ⚙️ **AppUpdater.exe** → Executável responsável por aplicar as atualizações.  
- 🧪 **SampleApp** → Exemplo funcional que demonstra como usar a biblioteca.

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
4. O usuário pode receber uma **notificação** com as opções:  
   - ✅ Atualizar agora (fecha e substitui)  
   - ❌ Cancelar (aguarda o fechamento normal do app)

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
- Pode rodar **silenciosamente** ou com interface de notificação.  
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
