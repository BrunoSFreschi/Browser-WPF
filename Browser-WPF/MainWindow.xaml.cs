using Microsoft.Web.WebView2.Core;
using Microsoft.Win32;
using System.Management;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Browser_WPF;

public partial class MainWindow : Window
{
    private ManagementEventWatcher ThemeWatcher;

    public MainWindow()
    {
        this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

        this.Loaded += (s, e) => this.Focus();

        InitializeComponent();

        StartWebView();
        StartThemeWatcher();
        ApplyWindowsTheme(); // Aplica o tema inicial
    }

    private async void StartWebView()
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

    private void UrlEnter(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            NavigationService.NavegarPara(MeuNavegador, Url.Text);
        }
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        // Ctrl + X para ocultar/mostrar barra de URL
        if (e.Key == Key.X && Keyboard.Modifiers == ModifierKeys.Control)
        {
            ToggleUrlBar();
            e.Handled = true;
        }

        // Ctrl + R para recarregar
        if (e.Key == Key.R && Keyboard.Modifiers == ModifierKeys.Control)
        {
            NavigationService.Recarregar(MeuNavegador);
            e.Handled = true;
        }

        // Alt + Left para voltar
        if (e.Key == Key.Left && Keyboard.Modifiers == ModifierKeys.Alt)
        {
            NavigationService.Voltar(MeuNavegador);
            e.Handled = true;
        }

        // Alt + Right para avançar
        if (e.Key == Key.Right && Keyboard.Modifiers == ModifierKeys.Alt)
        {
            NavigationService.Avancar(MeuNavegador);
            e.Handled = true;
        }
    }

    private void ToggleUrlBar()
    {
        if (TopBar.Visibility == Visibility.Visible)
        {
            TopBar.Visibility = Visibility.Collapsed;
            UrlRow.Height = new GridLength(0);
        }
        else
        {
            TopBar.Visibility = Visibility.Visible;
            UrlRow.Height = GridLength.Auto;
        }
    }

    #region Gerenciamento de Tema

    private void ApplyWindowsTheme()
    {
        try
        {
            bool isDarkTheme = WindowsDarkTheme;

            if (isDarkTheme)
                ApplyDarkTheme();
            else
                ApplyLightTheme();
        }
        catch (Exception)
        {
            ApplyLightTheme();
        }
    }

    private void StartThemeWatcher()
    {
        try
        {
            var query = new WqlEventQuery(
                "SELECT * FROM RegistryValueChangeEvent WHERE " +
                "Hive='HKEY_CURRENT_USER' AND " +
                "KeyPath='Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Themes\\\\Personalize' AND " +
                "ValueName='AppsUseLightTheme'");

            ThemeWatcher = new ManagementEventWatcher(query);
            ThemeWatcher.EventArrived += OnThemeChanged;
            ThemeWatcher.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Não foi possível monitorar mudanças de tema: {ex.Message}");
        }
    }

    private void OnThemeChanged(object sender, EventArrivedEventArgs e)
    {
        Dispatcher.BeginInvoke(new Action(ApplyWindowsTheme));
    }

    private static bool WindowsDarkTheme // 0 = Dark theme, 1 = Light theme
    {
        get
        {
            try
            {
                using var getKeyTheme = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");

                if (getKeyTheme != null)
                {
                    var value = getKeyTheme.GetValue("AppsUseLightTheme");

                    if (value != null && value is int intValue)
                        return intValue == 0;
                }
            }
            catch { }

            return false; // Tema claro como padrão
        }
    }

    private void ApplyLightTheme()
    {
        Resources["DynamicBackground"] = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        Resources["DynamicForeground"] = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        Resources["DynamicTextBoxBackground"] = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        Resources["DynamicBorder"] = new SolidColorBrush(Color.FromRgb(204, 204, 204));
    }

    private void ApplyDarkTheme()
    {
        Resources["DynamicBackground"] = new SolidColorBrush(Color.FromRgb(45, 45, 48));
        Resources["DynamicForeground"] = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        Resources["DynamicTextBoxBackground"] = new SolidColorBrush(Color.FromRgb(60, 60, 60));
        Resources["DynamicBorder"] = new SolidColorBrush(Color.FromRgb(85, 85, 85));
    }

    #endregion

    protected override void OnClosed(EventArgs e)
    {
        ThemeWatcher?.Stop();
        ThemeWatcher?.Dispose();
        base.OnClosed(e);
    }
}