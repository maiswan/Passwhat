﻿namespace Maiswan.Passwhat.WpfApp;

public interface IPasswordStrengthCalculator
{
	public bool IsPasswordWeak(int length, int poolSize);

	public bool IsPasswordWeak(string password, string pool);
}

