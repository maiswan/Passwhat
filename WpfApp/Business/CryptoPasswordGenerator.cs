using System.Security.Cryptography;

namespace Maiswan.Passwhat.WpfApp;

public class CryptoPasswordGenerator : IPasswordGenerator
{
	public char[] GeneratePassword(char[] characters, int length)
	{
		return RandomNumberGenerator.GetItems<char>(new(characters), length);
	}
}
