using System.IO;
using System.Text.Json;

namespace Maiswan.Passwhat.WpfApp;

public class JsonConfigurationProvider : IConfigurationProvider
{
	public Configurations Configuration { get; private set; } = new();

	public void Initialize(string source)
	{

		string text = File.ReadAllText(source);
		Configuration = JsonSerializer.Deserialize<Configurations>(text) ?? new();
	}
}
