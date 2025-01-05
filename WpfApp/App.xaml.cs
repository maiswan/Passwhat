﻿using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace Maiswan.Passwhat.WpfApp;

public partial class App : Application
{
    private const string ConfigPath = "Defaults.json";

	public new static App Current => (App)Application.Current;

    public App()
    {
        Services = ConfigureServices();

		IConfigurationProvider config = Services.GetRequiredService<IConfigurationProvider>();
		config.Initialize(ConfigPath);

        InitializeComponent();
		InitializeCulture(config.Configuration.Culture);
	}

	public IServiceProvider Services { get; }

	private static ServiceProvider ConfigureServices()
    {
        ServiceCollection services = new();

        services.AddSingleton<IPasswordGenerator, CryptoPasswordGenerator>();
		services.AddSingleton<IPasswordStrengthCalculator, EntropyCalculator>();
		services.AddSingleton<IConfigurationProvider, JsonConfigurationProvider>();
        services.AddTransient<MainViewModel>();

        return services.BuildServiceProvider();
    }

	private static void InitializeCulture(string culture)
	{
		CultureInfo info = new(culture);

		Thread.CurrentThread.CurrentCulture = info;
		Thread.CurrentThread.CurrentUICulture = info;

		FrameworkElement.LanguageProperty.OverrideMetadata(
			typeof(FrameworkElement),
			new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag))
		);
	}
}
