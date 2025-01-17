namespace Maiswan.Passwhat.WpfApp;

public readonly struct LogEntry
{
	public LogAction Action { get; init; }

	public PasswordEntry Password { get; init; }

	public LogEntry(LogAction action, PasswordEntry password)
	{
		Action = action;
		Password = password;
	}
}