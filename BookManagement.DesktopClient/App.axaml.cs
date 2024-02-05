using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BookManagement.DesktopClient.Interfaces;
using BookManagement.DesktopClient.Services;
using BookManagement.DesktopClient.ViewModels;
using BookManagement.DesktopClient.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.DesktopClient;

public partial class App : Application
{
    public new static App? Current => Application.Current as App;
    public IServiceProvider? Services { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            ConfigureServices();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        services.AddSingleton<IBookApiService>(x =>
            new BookApiService(configuration["ApiUrl"] ?? throw new InvalidOperationException()));
        Services = services.BuildServiceProvider();
    }
}