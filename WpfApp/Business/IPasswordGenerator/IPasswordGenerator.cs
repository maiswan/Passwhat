namespace Maiswan.Passwhat.WpfApp;

public interface IPasswordGenerator
{
	public string GeneratePassword(IEnumerable<char> characters, int length);
}
