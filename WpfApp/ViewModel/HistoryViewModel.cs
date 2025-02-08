using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows;

namespace Maiswan.Passwhat.WpfApp;

public partial class HistoryViewModel : ObservableObject
{
	private readonly IHistoryProvider history;

	public ReadOnlyObservableCollection<PasswordEntry> Passwords { get; }

	public HistoryViewModel(IHistoryProvider history)
	{
		this.history = history;
		Passwords = history.Passwords;
	}

	[RelayCommand]
	private void Copy(PasswordEntry password)
	{
		Clipboard.SetText(password.Password);

		LogEntry entry = new(LogAction.Copy, password);
		WeakReferenceMessenger.Default.Send(new LogEntryChangedMessage(entry));
	}

	[RelayCommand]
	private void Remove(PasswordEntry password)
	{
		history.Remove(password);

		LogEntry entry = new(LogAction.Removal, password);
		WeakReferenceMessenger.Default.Send(new LogEntryChangedMessage(entry));
	}
}