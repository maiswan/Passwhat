﻿using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Maiswan.Passwhat.WpfApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public new static App Current => (App)Application.Current;

	public IServiceProvider Services { get; }

    public App()
    {
        Services = ConfigureServices();
        InitializeComponent();
    }

    private static ServiceProvider ConfigureServices()
    {
        ServiceCollection services = new();

        services.AddSingleton<IPasswordGenerator, CryptoPasswordGenerator>();
        services.AddTransient<MainViewModel>();

        return services.BuildServiceProvider();
    }
}