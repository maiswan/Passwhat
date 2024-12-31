namespace Maiswan.Passwhat.WpfApp;

public interface IPasswordGenerator
{
	public char[] GeneratePassword(char[] characters, int length);
}
