namespace Maiswan.Passwhat.WpfApp;

public interface IConfigurationProvider
{
	public void Initialize(string source);

	public Configurations Configuration { get; }
}
