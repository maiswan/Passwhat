using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;

namespace Maiswan.Passwhat.WpfApp;

public partial class MainViewModel : ObservableObject
{
	public static Version? Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;
	public const double MinLength = 8;
	public const double MaxLength = 128;

	private const string UpperLatin = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	private const string LowerLatin = "abcdefghijklmnopqrstuvwxyz";
	private const string Digit = "0123456789";
	private const string Symbol = "`~!@#$%^&*()_+-=[]{}\\|;':\",./<>?";

	private readonly IPasswordGenerator generator;
	private readonly IPasswordStrengthCalculator strength;
	public MainViewModel(IPasswordGenerator generator, IPasswordStrengthCalculator strength, IConfigurationProvider config)
	{
		this.generator = generator;
		this.strength = strength;

		Length = config.Configuration.Length;
		CustomSet = config.Configuration.CustomSet;
		IsLatinEnabled = config.Configuration.IsLatinEnabled;
		IsDigitEnabled = config.Configuration.IsDigitEnabled;
		IsSymbolEnabled = config.Configuration.IsSymbolEnabled;
		IsCustomSetEnabled = config.Configuration.IsCustomSetEnabled;

		Refresh();
	}

	public ObservableCollection<string> CopiedPasswords { get; } = [];

	[ObservableProperty]
	private string customSet;
	partial void OnCustomSetChanged(string value) => Refresh();

	[ObservableProperty]
	private bool isLatinEnabled;
	partial void OnIsLatinEnabledChanged(bool value) => Refresh();

	[ObservableProperty]
	private bool isDigitEnabled;
	partial void OnIsDigitEnabledChanged(bool value) => Refresh();

	[ObservableProperty]
	private bool isSymbolEnabled;
	partial void OnIsSymbolEnabledChanged(bool value) => Refresh();

	[ObservableProperty]
	private bool isCustomSetEnabled;
	partial void OnIsCustomSetEnabledChanged(bool value) => Refresh();

	[ObservableProperty]
	private bool isPasswordConfigWeak;

	[ObservableProperty]
	private LogEntry latestAction = new();

	[ObservableProperty]
	private int length;
	partial void OnLengthChanged(int value) => Refresh();

	[ObservableProperty]
	private string password = "";

	[ObservableProperty]
	private string? selectedCopiedPassword;
	partial void OnSelectedCopiedPasswordChanged(string? value) => Copy(value);


	[RelayCommand]
	private void Copy(string? password)
	{
		password ??= Password;
		if (string.IsNullOrWhiteSpace(password)) { return; }

		Clipboard.SetText(password);
		LatestAction = new(LogAction.Copy, password);

		if (CopiedPasswords.Contains(password)) { return; }
		CopiedPasswords.Insert(0, Password);
	}

	[RelayCommand]
	private void Refresh()
	{
		string pool = "";
		if (IsLatinEnabled) { pool += UpperLatin + LowerLatin; }
		if (IsDigitEnabled) { pool += Digit; }
		if (IsSymbolEnabled) { pool += Symbol; }
		if (IsCustomSetEnabled) { pool += CustomSet; }

		if (string.IsNullOrWhiteSpace(pool))
		{
			Password = "";
			return;
		}

		char[] uniquePool = pool.ToCharArray().Distinct().ToArray();

		Password = new(generator.GeneratePassword(uniquePool, Length));
		IsPasswordConfigWeak = strength.IsPasswordWeak(Length, uniquePool.Length);
		LatestAction = new(LogAction.Generation, Password);
	}

	[RelayCommand]
	private void Remove(string? password)
	{
		if (password is null) { return; }
		CopiedPasswords.Remove(password);
	}
}