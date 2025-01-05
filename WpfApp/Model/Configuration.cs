namespace Maiswan.Passwhat.WpfApp;

public record Configurations
{
	public bool IsCustomSetEnabled { get; init; } = true;
	public bool IsDigitEnabled { get; init; } = true;
	public bool IsLatinEnabled { get; init; } = true;
	public bool IsSymbolEnabled { get; init; } = true;
	public int Length { get; init; } = 8;
	public string Culture { get; init; } = "";
	public string CustomSet { get; init; } = "";
}
