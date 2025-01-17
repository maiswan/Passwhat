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

	private static readonly char[] Latin = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
	private static readonly char[] Digit = "0123456789".ToCharArray();
	private static readonly char[] Symbol = "`~!@#$%^&*()_+-=[]{}\\|;':\",./<>?".ToCharArray();

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

	public ObservableCollection<PasswordEntry> CopiedPasswords { get; } = [];

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

	[RelayCommand]
	private void CopyActivePassword()
	{
		PasswordEntry entry = new(Password, IsPasswordConfigWeak);
		CopyEntry(entry);
	}

	[RelayCommand]
	private void CopyEntry(PasswordEntry entry)
	{
		if (string.IsNullOrWhiteSpace(entry.Password)) { return; }

		Clipboard.SetText(entry.Password);
		LatestAction = new(LogAction.Copy, entry);

		if (CopiedPasswords.Contains(entry)) { return; }
		CopiedPasswords.Insert(0, entry);
	}

	[RelayCommand]
	private void Refresh()
	{
		List<char> pool = [];

		if (IsLatinEnabled) { pool.AddRange(Latin); }
		if (IsDigitEnabled) { pool.AddRange(Digit); }
		if (IsSymbolEnabled) { pool.AddRange(Symbol); }
		if (IsCustomSetEnabled) { pool.AddRange(CustomSet); }

		if (pool.Count == 0)
		{
			Password = "";
			return;
		}

		IEnumerable<char> uniquePool = pool.Distinct();

		Password = generator.GeneratePassword(uniquePool, Length);
		IsPasswordConfigWeak = strength.IsPasswordWeak(Length, uniquePool.Count());

		PasswordEntry entry = new(Password, IsPasswordConfigWeak);
		LatestAction = new(LogAction.Generation, entry);
	}

	[RelayCommand]
	private void RemoveEntry(PasswordEntry password)
	{
		CopiedPasswords.Remove(password);
	}
}