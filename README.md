<img width="1365" height="726" alt="image" src="https://github.com/user-attachments/assets/067adbc9-6182-4e4b-96b5-f8138b97ed6a" />



# Browser-WPF

## Descrição

Este projeto implementa um navegador web básico com interface limpa e moderna, oferecendo funcionalidades essenciais de navegação através de uma barra de endereços integrada.

## Tecnologias Utilizadas

- **.NET** (WPF - Windows Presentation Foundation)
- **Microsoft WebView2** - Motor de renderização web
- **C#** - Linguagem de programação
- **XAML** - Marcação da interface

## Pré-requisitos

### Software Necessário

1. **Visual Studio 2022** ou superior
2. **.NET Framework 4.7.2** ou **.NET 6/7/8**
3. **Microsoft Edge WebView2 Runtime** (geralmente já instalado no Windows 10/11)

### Pacotes NuGet

```xml
<PackageReference Include="Microsoft.Web.WebView2" Version="1.0.x" />
```

## Instalação

1. **Clone o repositório**:
```bash
git clone https://github.com/seu-usuario/browser-wpf.git
cd browser-wpf
```

2. **Restaure os pacotes NuGet**:
```bash
dotnet restore
```

3. **Compile e execute**:
```bash
dotnet run
```
ou pressione `F5` no Visual Studio.

## Como Usar

1. **Inicialização**:
   - O navegador abre automaticamente com o Google como página inicial
2. **Navegação**: 
   - Digite uma URL na barra de endereços
   - Pressione `Enter` para navegar
   - URLs sem protocolo recebem automaticamente "https://"
3. **Exemplos de URLs válidas**:
   - `google.com` → `https://google.com`
   - `https://github.com`
   - `http://example.com`

## Estrutura do Projeto

```
Browser_WPF/
├── MainWindow.xaml          # Interface do usuário
├── MainWindow.xaml.cs       # Lógica principal
├── App.xaml                 # Configuração da aplicação
├── App.xaml.cs              # Inicialização da aplicação
└── Browser_WPF.csproj       # Arquivo de projeto
```

## Configurações

### Aparência da Janela
```xml
WindowStyle="None"           <!-- Remove bordas padrão -->
AllowsTransparency="True"    <!-- Permite transparência -->
WindowStartupLocation="CenterScreen"  <!-- Centraliza na tela -->
```

## Solução de Problemas

### WebView2 Runtime não encontrado
```
Erro: WebView2 Runtime não está instalado
Solução: Baixe e instale em: https://developer.microsoft.com/microsoft-edge/webview2/
```

### Erro de navegação
- Verifique a conexão com a internet
- Confirme se a URL é válida
- Alguns sites podem bloquear incorporação via WebView2

## Contribuindo

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## Autor

[Bruno](https://github.com/BrunoSFreschi)

Link do Projeto: [link rapido](https://github.com/BrunoSFreschi/browser-wpf)

---
