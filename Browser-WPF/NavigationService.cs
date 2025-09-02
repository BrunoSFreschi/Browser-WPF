using Microsoft.Web.WebView2.Wpf;
using System.Windows;

namespace Browser_WPF;

public static class NavigationService
{
    /// <summary>
    /// Navega para o endereço especificado no WebView2
    /// </summary>
    /// <param name="webView">Instância do WebView2</param>
    /// <param name="endereco">URL ou termo de busca</param>
    public static void NavegarPara(WebView2 webView, string endereco)
    {
        if (webView?.CoreWebView2 == null)
        {
            MessageBox.Show("WebView2 não está inicializado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(endereco))
        {
            return;
        }

        try
        {
            string urlFinal = ProcessarEndereco(endereco);
            webView.CoreWebView2.Navigate(urlFinal);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao navegar: {ex.Message}", "Erro de Navegação", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Processa o endereço digitado, adicionando protocolo se necessário
    /// </summary>
    /// <param name="endereco">Endereço digitado pelo usuário</param>
    /// <returns>URL válida</returns>
    private static string ProcessarEndereco(string endereco)
    {
        endereco = endereco.Trim();

        // Se já tem protocolo, retorna como está
        if (endereco.StartsWith("http://") || endereco.StartsWith("https://"))
        {
            return endereco;
        }

        // Se parece com uma URL (contém ponto), adiciona https
        if (endereco.Contains('.') && !endereco.Contains(' '))
        {
            return "https://" + endereco;
        }

        // Se não parece com URL, faz busca no Google
        return $"https://www.google.com/search?q={Uri.EscapeDataString(endereco)}";
    }

    /// <summary>
    /// Volta uma página no histórico
    /// </summary>
    /// <param name="webView">Instância do WebView2</param>
    public static void Voltar(WebView2 webView)
    {
        try
        {
            if (webView?.CoreWebView2 != null && webView.CoreWebView2.CanGoBack)
            {
                webView.CoreWebView2.GoBack();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao voltar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Avança uma página no histórico
    /// </summary>
    /// <param name="webView">Instância do WebView2</param>
    public static void Avancar(WebView2 webView)
    {
        try
        {
            if (webView?.CoreWebView2 != null && webView.CoreWebView2.CanGoForward)
            {
                webView.CoreWebView2.GoForward();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao avançar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Recarrega a página atual
    /// </summary>
    /// <param name="webView">Instância do WebView2</param>
    public static void Recarregar(WebView2 webView)
    {
        try
        {
            webView?.CoreWebView2?.Reload();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao recarregar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public static void UrlFocus(WebView2 webView)
    {
        try
        {
            webView?.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao focar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Para o carregamento da página atual
    /// </summary>
    /// <param name="webView">Instância do WebView2</param>
    public static void Parar(WebView2 webView)
    {
        try
        {
            webView?.CoreWebView2?.Stop();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao parar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}