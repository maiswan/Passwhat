using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;

namespace Maiswan.Passwhat.WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private readonly MainViewModel viewModel;
	private readonly Configurations config;

	public MainWindow()
	{
		InitializeComponent();
		viewModel = App.Current.Services.GetRequiredService<MainViewModel>();
		config = App.Current.Services.GetRequiredService<IConfigurationProvider>().Configuration;
		DataContext = viewModel;
	}

	private void Window_Closing(object sender, CancelEventArgs e)
	{
		if (viewModel.CopiedPasswords.Count == 0) { return; }
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