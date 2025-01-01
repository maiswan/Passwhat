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

	public MainWindow()
	{
		InitializeComponent();
		viewModel = App.Current.Services.GetRequiredService<MainViewModel>();
		DataContext = viewModel;
	}

	private void Window_Closing(object sender, CancelEventArgs e)
	{
		if (viewModel.CopiedPasswords.Count == 0) { return; }

		MessageBoxResult result = MessageBox.Show(
			"There are passwords in the copy list. Do you want to exit?",
			"Passwhat",
			MessageBoxButton.YesNo,
			MessageBoxImage.Question
		);

		e.Cancel = result == MessageBoxResult.No;
    }
}