using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfNavigationDemo.Navigation;
using WpfNavigationDemo.ViewModels;

namespace WpfNavigationDemo;

public partial class App : Application
{
    private IHost _host = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<NavigationStore>();

                services.AddTransient<HomeViewModel>();
                services.AddTransient<SettingsViewModel>();
                services.AddSingleton<MainViewModel>();
                
                services.AddKeyedTransient<INavigationService>("Home", (sp, _) =>
                    new NavigationService<HomeViewModel>(
                        sp.GetRequiredService<NavigationStore>(),
                        () => sp.GetRequiredService<HomeViewModel>()));

                services.AddKeyedTransient<INavigationService>("Settings", (sp, _) =>
                    new NavigationService<SettingsViewModel>(
                        sp.GetRequiredService<NavigationStore>(),
                        () => sp.GetRequiredService<SettingsViewModel>()));
            })
            .Build();

        _host.StartAsync().GetAwaiter().GetResult();

        var store = _host.Services.GetRequiredService<NavigationStore>();
        store.CurrentViewModel = _host.Services.GetRequiredService<HomeViewModel>();

        var mainVm = _host.Services.GetRequiredService<MainViewModel>();

        var window = new MainWindow { DataContext = mainVm };
        MainWindow = window;
        window.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _host.StopAsync().GetAwaiter().GetResult();
        _host.Dispose();
        base.OnExit(e);
    }
}
