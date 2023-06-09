namespace Practice.Indications;

public record Biometric(float AlMean, float CctMean, float AcdMean, float LtMean,
    List<float> Al, List<float> Cct, List<float> Acd, List<float> Lt, List<float> Sd);