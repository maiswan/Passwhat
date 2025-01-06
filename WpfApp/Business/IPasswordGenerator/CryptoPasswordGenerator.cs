using System.Security.Cryptography;

namespace Maiswan.Passwhat.WpfApp;

public class CryptoPasswordGenerator : IPasswordGenerator
{
	public string GeneratePassword(IEnumerable<char> characters, int length)
	{
		ReadOnlySpan<char> uniquePool = characters
			.Distinct()
			.ToArray();

		return new(RandomNumberGenerator.GetItems(uniquePool, length));
	}
}
