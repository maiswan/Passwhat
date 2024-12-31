using System.Windows;

namespace Maiswan.Passwhat.WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetService(typeof(MainViewModel));
	}
}