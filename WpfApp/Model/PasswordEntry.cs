namespace Maiswan.Passwhat.WpfApp;

public readonly struct PasswordEntry
{
	public string Password { get; init; }

	public bool IsPasswordWeak { get; init; }

	public PasswordEntry(string password, bool isPasswordWeak)
	{
		Password = password;
		IsPasswordWeak = isPasswordWeak;
	}

	private const int DefaultPasswordLength = 8; // arbitrary limit

	public override string ToString()
	{
		if (Password.Length <= DefaultPasswordLength) { return Password; }

		return Password[0..DefaultPasswordLength] + "...";
	}
}
