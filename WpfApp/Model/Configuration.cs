using CommunityToolkit.Mvvm.ComponentModel;

namespace Maiswan.Passwhat.WpfApp;

public partial class Configurations : ObservableObject
{
	[ObservableProperty]
	private bool isCustomSetEnabled = false;
	[ObservableProperty]
	private bool isDigitEnabled = false;
	[ObservableProperty]
	private bool isLatinEnabled = false;
	[ObservableProperty]
	private bool isSymbolEnabled = false;
	[ObservableProperty]
	private int length = 8;
	[ObservableProperty]
	private string culture = "";
	[ObservableProperty]
	private string customSet = "";
	[ObservableProperty]
	private bool copyPasswordOnStartup = false;
	[ObservableProperty]
	private bool suppressCloseDialog = false;
}
