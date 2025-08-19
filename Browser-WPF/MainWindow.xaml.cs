using System.Windows;

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

        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao inicializar WebView2: {ex.Message}\n\nCertifique-se de que o WebView2 Runtime está instalado.");
        }
    }

}