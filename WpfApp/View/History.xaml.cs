using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Maiswan.Passwhat.WpfApp;

/// <summary>
/// Interaction logic for MainOptions.xaml
/// </summary>
public partial class History : UserControl
{
	public History()
	{
		InitializeComponent();
		DataContext = App.Current.Services.GetRequiredService<HistoryViewModel>();
	}
}
