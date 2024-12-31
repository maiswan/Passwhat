﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace Maiswan.Passwhat.WpfApp;
public partial class MainViewModel : ObservableObject, INotifyPropertyChanged
{
	public static Version? Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;
	public const double MinLength = 8;
	public const double MaxLength = 128;

	private const string UpperLatin = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	private const string LowerLatin = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	private const string Digit = "0123456789";
	private const string Symbol = "`~!@#$%^&*()_+-=[]{}\\|;':\",./<>?";

	[ObservableProperty]
	private string password = "";

	[ObservableProperty]
	private int length = 64;

	[ObservableProperty]
	private bool isLatinEnabled = true;
	[ObservableProperty]
	private bool isDigitEnabled = true;
	[ObservableProperty]
	private bool isSymbolEnabled = true;
	[ObservableProperty]
	private bool isCustomSetEnabled = false;
	[ObservableProperty]
	private string customSet = "0123456789abcdef";

	partial void OnLengthChanged(int value) => Refresh();
	partial void OnIsLatinEnabledChanged(bool value) => Refresh();
	partial void OnIsDigitEnabledChanged(bool value) => Refresh();
	partial void OnIsSymbolEnabledChanged(bool value) => Refresh();
	partial void OnIsCustomSetEnabledChanged(bool value) => Refresh();
	partial void OnCustomSetChanged(string value) => Refresh();

	[RelayCommand]
	private void Copy()
	{
		if (string.IsNullOrWhiteSpace(Password)) { return; }

		Clipboard.SetText(Password);
	}

	[RelayCommand]
	private void Refresh()
	{
		string pool = "";
		if (IsLatinEnabled) { pool += UpperLatin + LowerLatin; }
		if (IsDigitEnabled) { pool += Digit; }
		if (IsSymbolEnabled) { pool += Symbol; }
		if (IsCustomSetEnabled) { pool += CustomSet; }

		if (string.IsNullOrWhiteSpace(pool))
		{
			Password = "";
			return;
		}

		char[] uniquePool = pool.ToCharArray().Distinct().ToArray();

		Password = new(generator.GeneratePassword(uniquePool, Length));
	}

    public MainViewModel(IPasswordGenerator generator)
    {
		this.generator = generator;
		Refresh();
    }

	private readonly IPasswordGenerator generator;
}