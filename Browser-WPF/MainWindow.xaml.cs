using Microsoft.Web.WebView2.Core;
using System.Windows;
using System.Windows.Input;

namespace Browser_WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        InicializarWebView();
    }

    private async void InicializarWebView()
    {
        try
        {
            await MeuNavegador.EnsureCoreWebView2Async(null);
         
            MeuNavegador.CoreWebView2.Navigate(Url.Text);
         
            MeuNavegador.CoreWebView2.NavigationStarting += AtualizaUrl;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao inicializar WebView2: {ex.Message}\n\nCertifique-se de que o WebView2 Runtime está instalado.");
        }
    }

    private void AtualizaUrl(object? sender, CoreWebView2NavigationStartingEventArgs e)
    {
        Dispatcher.Invoke(() =>
        {
            Url.Text = e.Uri;
        });
    }

    private void Url_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            NavegarPara(Url.Text);
    }

    private void NavegarPara(string endereco)
    {
        if (!string.IsNullOrEmpty(endereco))
        {
            if (!endereco.StartsWith("http://") && !endereco.StartsWith("https://"))
            {
                endereco = "https://" + endereco;
            }

            try
            {
                MeuNavegador.CoreWebView2?.Navigate(endereco);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao navegar: {ex.Message}");
            }
        }
    }
}