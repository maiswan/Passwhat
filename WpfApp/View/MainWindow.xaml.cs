using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;

namespace Maiswan.Passwhat.WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private readonly Configurations config;
	private readonly IHistoryProvider history;

	public MainWindow()
	{
		InitializeComponent();
		config = App.Current.Services.GetRequiredService<IConfigurationProvider>().Configuration;
		history = App.Current.Services.GetRequiredService<IHistoryProvider>();
	}

	private void Window_Closing(object sender, CancelEventArgs e)
	{
		if (history.Passwords.Count == 0) { return; }
		if (config.SuppressCloseDialog) { return; }

		MessageBoxResult result = MessageBox.Show(
			Properties.Resources.ExitConfirmation,
			Properties.Resources.AppTitle,
			MessageBoxButton.YesNo,
			MessageBoxImage.Question
		);

		e.Cancel = result == MessageBoxResult.No;
    }
}