using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Maiswan.Passwhat.WpfApp;

/// <summary>
/// Interaction logic for MainOptions.xaml
/// </summary>
public partial class MainOptions : UserControl
{
	public MainOptions()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetRequiredService<MainOptionsViewModel>();
	}
}
