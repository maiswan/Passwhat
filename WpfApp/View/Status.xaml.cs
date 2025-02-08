using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Maiswan.Passwhat.WpfApp;

/// <summary>
/// Interaction logic for MainOptions.xaml
/// </summary>
public partial class Status : UserControl
{
	public Status()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetRequiredService<StatusViewModel>();
	}
}
