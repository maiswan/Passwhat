using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Maiswan.Passwhat.WpfApp;

public partial class MainViewModel : ObservableObject
{
	private static readonly char[] Latin = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
	private static readonly char[] Digit = "0123456789".ToCharArray();
	private static readonly char[] Symbol = "`~!@#$%^&*()_+-=[]{}\\|;':\",./<>?".ToCharArray();

	private readonly IPasswordGenerator generator;
	private readonly IPasswordStrengthCalculator strength;
	private readonly IHistoryProvider history;

	private readonly Configurations config;

	public MainViewModel(IPasswordGenerator generator, IPasswordStrengthCalculator strength, IConfigurationProvider config, IHistoryProvider history)
	{
		this.generator = generator;
		this.strength = strength;
		this.history = history;
		this.config = config.Configuration;
		this.config.PropertyChanged += (object? sender, PropertyChangedEventArgs e) => Refresh();
		Refresh();
	}

	public ObservableCollection<PasswordEntry> CopiedPasswords { get; } = [];

	[ObservableProperty]
	private bool isPasswordConfigWeak;

	[ObservableProperty]
	private string password = "";

	[RelayCommand]
	private void CopyActivePassword()
	{
		PasswordEntry entry = new(Password, IsPasswordConfigWeak);
		Clipboard.SetText(entry.Password);

		LogEntry action = new(LogAction.Copy, entry);
		WeakReferenceMessenger.Default.Send(new LogEntryChangedMessage(action));
		history.Add(entry);
	}

	[RelayCommand]
	private void Refresh()
	{
		List<char> pool = [];

		if (config.IsLatinEnabled) { pool.AddRange(Latin); }
		if (config.IsDigitEnabled) { pool.AddRange(Digit); }
		if (config.IsSymbolEnabled) { pool.AddRange(Symbol); }
		if (config.IsCustomSetEnabled) { pool.AddRange(config.CustomSet); }

		if (pool.Count == 0)
		{
			Password = "";
			return;
		}

		IEnumerable<char> uniquePool = pool.Distinct();

		Password = generator.GeneratePassword(uniquePool, config.Length);
		IsPasswordConfigWeak = strength.IsPasswordWeak(config.Length, uniquePool.Count());

		PasswordEntry entry = new(Password, IsPasswordConfigWeak);
		LogEntry action = new(LogAction.Generation, entry);
		WeakReferenceMessenger.Default.Send(new LogEntryChangedMessage(action));

		if (isFirstPassword && copyPasswordOnStartup)
		{
			CopyActivePassword();
			isFirstPassword = false;
		}
	}
}