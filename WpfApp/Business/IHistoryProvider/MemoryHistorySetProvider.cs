using System.Collections.ObjectModel;

namespace Maiswan.Passwhat.WpfApp;

public class MemoryHistorySetProvider : IHistoryProvider
{
	private readonly ObservableCollection<PasswordEntry> passwords = [];
	public ReadOnlyObservableCollection<PasswordEntry> Passwords { get; }

	public MemoryHistorySetProvider()
	{
		Passwords = new(passwords);
	}

	public void Add(PasswordEntry passwordEntry)
	{
		if (passwords.Contains(passwordEntry)) { return; }
		passwords.Add(passwordEntry);
	}

	public void Remove(PasswordEntry passwordEntry)
	{
		passwords.Remove(passwordEntry);
	}
}
