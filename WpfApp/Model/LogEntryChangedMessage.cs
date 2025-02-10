using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Maiswan.Passwhat.WpfApp;

public class LogEntryChangedMessage : ValueChangedMessage<LogEntry>
{
	public LogEntryChangedMessage(LogEntry entry) : base(entry)
	{
	}
}