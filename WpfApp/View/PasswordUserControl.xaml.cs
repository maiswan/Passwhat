using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Maiswan.Passwhat.WpfApp;

/// <summary>
/// Interaction logic for MainOptions.xaml
/// </summary>
public partial class PasswordUserControl : UserControl
{
	private readonly PasswordViewModel viewModel;

	public PasswordUserControl()
	{
		InitializeComponent();
		viewModel = App.Current.Services.GetRequiredService<PasswordViewModel>();
		DataContext = viewModel;
	}

	private void RegisterInputHandlers()
	{
		DependencyObject parent = this;
		Window root;
		while (parent is not Window)
		{
			parent = VisualTreeHelper.GetParent(parent);
		}

		root = (Window)parent;
		root.InputBindings.AddRange(InputBindings);
	}

	private void UserControl_Loaded(object sender, RoutedEventArgs e)
	{
		RegisterInputHandlers();
	}
}
