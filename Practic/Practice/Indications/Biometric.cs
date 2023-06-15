namespace Practice.Indications;

public record Biometric(string AlMean, string CctMean, string AcdMean, string LtMean,
    List<string> Al, List<string> Cct, List<string> Acd, List<string> Lt, List<string> Sd);