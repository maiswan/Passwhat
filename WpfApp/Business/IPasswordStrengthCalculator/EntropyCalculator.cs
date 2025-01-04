namespace Maiswan.Passwhat.WpfApp;

public class EntropyCalculator : IPasswordStrengthCalculator
{
	private static bool IsPasswordWeak(double entropy)
	{
		return entropy < 80;
	}

	public bool IsPasswordWeak(int length, int poolSize)
	{
		double entropy = length * Math.Log2(poolSize);
		return IsPasswordWeak(entropy);
	}

	public bool IsPasswordWeak(int length, string pool)
	{
		return IsPasswordWeak(length, pool.Length);
	}

	public bool IsPasswordWeak(string password, string pool)
	{
		return IsPasswordWeak(password.Length, pool);
	}
}

