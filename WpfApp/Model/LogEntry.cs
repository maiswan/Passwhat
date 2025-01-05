namespace Maiswan.Passwhat.WpfApp;

public readonly struct LogEntry
{
	public LogAction Action { get; init; }

	public string Password { get; init; }

    public LogEntry(LogAction action, string password)
    {
		Action = action;
		Password = Truncate(password);
    }

    private const int DefaultPasswordLength = 8; // arbitrary limit

	private static string Truncate(string password, int length = DefaultPasswordLength)
	{
		if (password.Length <= length) { return password; }

		return password[0..DefaultPasswordLength] + "...";
	}
}