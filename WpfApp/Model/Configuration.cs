namespace Maiswan.Passwhat.WpfApp;

public record Configurations
{
	public int Length { get; init; } = 8;
	public string CustomSet { get; init; } = "";
	public bool IsLatinEnabled { get; init; } = true;
	public bool IsDigitEnabled { get; init; } = true;
	public bool IsSymbolEnabled { get; init; } = true;
	public bool IsCustomSetEnabled { get; init; } = true;
	public string Culture { get; init; } = "";
}
