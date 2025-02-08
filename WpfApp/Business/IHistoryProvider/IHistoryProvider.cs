using System.Collections.ObjectModel;

namespace Maiswan.Passwhat.WpfApp;

public interface IHistoryProvider
{
	public ReadOnlyObservableCollection<PasswordEntry> Passwords { get; }

	public void Add(PasswordEntry passwordEntry);
	public void Remove(PasswordEntry passwordEntry);
}
