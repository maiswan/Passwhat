using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Reflection;

namespace Maiswan.Passwhat.WpfApp;

public partial class StatusViewModel : ObservableObject, IRecipient<LogEntryChangedMessage>
{
	public static Version? Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;

	[ObservableProperty]
	private LogEntry latestAction;

	public StatusViewModel()
	{
		WeakReferenceMessenger.Default.RegisterAll(this);
	}

	public void Receive(LogEntryChangedMessage message)
	{
		LatestAction = message.Value;
	}
}