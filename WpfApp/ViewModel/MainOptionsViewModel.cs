namespace Maiswan.Passwhat.WpfApp;

public partial class MainOptionsViewModel
{
	public const double MinLength = 8;
	public const double MaxLength = 128;

	public Configurations Config { get; }

	public MainOptionsViewModel(IConfigurationProvider config)
	{
		Config = config.Configuration;
	}
}